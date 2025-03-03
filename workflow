name: .NET Core Desktop

on:
  push:
    branches: [ "main" ]
  pull_request:
    branches: [ "main" ]

jobs:
  build:
    strategy:
      matrix:
        configuration: [Debug, Release]  # Build for Debug and Release configurations

    runs-on: windows-latest  # Use the latest available Windows runner

    env:
      Solution_Name: "your-solution-name.sln"  # Replace with your solution name, e.g., MyWpfApp.sln
      Test_Project_Path: "your-test-project-path"  # Replace with your test project path, e.g., MyWpfApp.Tests\MyWpfApp.Tests.csproj
      Wap_Project_Directory: "your-wap-project-directory-name"  # Replace with your WAP directory, e.g., MyWpfApp.Package
      Wap_Project_Path: "your-wap-project-path"  # Replace with the path to your WAP project, e.g., MyWpf.App.Package\MyWpfApp.Package.wapproj

    steps:
    - name: Checkout
      uses: actions/checkout@v4
      with:
        fetch-depth: 0  # Ensures we fetch the full history

    # Install .NET Core SDK
    - name: Install .NET Core
      uses: actions/setup-dotnet@v4
      with:
        dotnet-version: 8.0.x  # Use .NET 8.0.x, update as needed for your project version

    # Add MSBuild to PATH for building the project
    - name: Setup MSBuild.exe
      uses: microsoft/setup-msbuild@v2

    # Run unit tests
    - name: Execute unit tests
      run: dotnet test

    # Restore the application and populate the obj folder with RuntimeIdentifiers
    - name: Restore the application
      run: msbuild $env:Solution_Name /t:Restore /p:Configuration=$env:Configuration
      env:
        Configuration: ${{ matrix.configuration }}  # Uses the configuration from the matrix

    # Decode the Base64 encoded PFX certificate and save it as .pfx
    - name: Decode the pfx
      run: |
        $pfx_cert_byte = [System.Convert]::FromBase64String("${{ secrets.Base64_Encoded_Pfx }}")
        $certificatePath = Join-Path -Path $env:Wap_Project_Directory -ChildPath GitHubActionsWorkflow.pfx
        [IO.File]::WriteAllBytes("$certificatePath", $pfx_cert_byte)

    # Build the application and create the MSIX package
    - name: Create the app package
      run: msbuild $env:Wap_Project_Path /p:Configuration=$env:Configuration /p:UapAppxPackageBuildMode=$env:Appx_Package_Build_Mode /p:AppxBundle=$env:Appx_Bundle /p:PackageCertificateKeyFile=GitHubActionsWorkflow.pfx /p:PackageCertificatePassword=${{ secrets.Pfx_Key }}
      env:
        Appx_Bundle: Always  # Ensure an appx bundle is created
        Appx_Bundle_Platforms: x86|x64  # Build for both x86 and x64 platforms
        Appx_Package_Build_Mode: StoreUpload  # For uploading to the Microsoft Store (can be changed if needed)
        Configuration: ${{ matrix.configuration }}

    # Clean up and remove the PFX certificate after use
    - name: Remove the pfx
      run: Remove-Item -path $env:Wap_Project_Directory\GitHubActionsWorkflow.pfx

    # Upload the build artifacts (MSIX package)
    - name: Upload build artifacts
      uses: actions/upload-artifact@v4
      with:
        name: MSIX Package
        path: ${{ env.Wap_Project_Directory }}\AppPackages

