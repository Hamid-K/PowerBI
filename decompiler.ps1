Param(
    [string]$DnSpyConsolePath,
    [string]$InputFolder,
    [string]$OutputFolder,
    [int]$Threads = 0
)

# 1. Print usage if required args are missing.
if (-not $DnSpyConsolePath -or -not $InputFolder -or -not $OutputFolder) {
    Write-Host ""
    Write-Host "Usage:"
    Write-Host "    .\$(Split-Path -Leaf $PSCommandPath) -DnSpyConsolePath <PathToDnSpyConsoleExe> -InputFolder <DLLsFolder> -OutputFolder <OutputDir> [-Threads <N>]"
    Write-Host ""
    Write-Host "Example:"
    Write-Host "    .\$(Split-Path -Leaf $PSCommandPath) -DnSpyConsolePath 'C:\Tools\dnSpy\dnSpy.Console.exe' -InputFolder 'C:\Dlls' -OutputFolder 'C:\Decompiled' -Threads 4"
    Write-Host ""
    exit 1
}

# 2. Validate the dnSpy console path.
if (!(Test-Path -LiteralPath $DnSpyConsolePath)) {
    Write-Host "ERROR: dnSpy console path not found at: $DnSpyConsolePath"
    exit 1
}

# 3. Validate the input folder.
if (!(Test-Path -LiteralPath $InputFolder)) {
    Write-Host "ERROR: Input folder not found at: $InputFolder"
    exit 1
}

# 4. Ensure the output folder exists (create if missing).
if (!(Test-Path -LiteralPath $OutputFolder)) {
    Write-Host "Creating output folder: $OutputFolder"
    New-Item -ItemType Directory -Path $OutputFolder | Out-Null
}

# 5. Decompile each DLL in the input folder.
Get-ChildItem -Path $InputFolder -Filter '*.dll' | ForEach-Object {
    $dllPath = $_.FullName
    $dllName = $_.BaseName  # e.g. "MyLibrary" from "MyLibrary.dll"
    
    # Optional: Skip known non-.NET DLLs (like native DLLs) by checking size or other heuristics
    # A robust check is to catch errors from dnSpy, but as a quick approach:
    # try {
    #     [Reflection.AssemblyName]::GetAssemblyName($dllPath) | Out-Null
    # } catch {
    #     Write-Host "Skipping non-.NET file: $dllPath"
    #     return
    # }

    Write-Host "`nDecompiling $dllPath..."

    # Build argument list for dnSpy.Console
    $cmdArgs = @()
    $cmdArgs += '-o'
    $cmdArgs += (Join-Path $OutputFolder $dllName)

    # If user specified a valid Threads count, pass it
    if ($Threads -gt 0) {
        $cmdArgs += '--threads'
        $cmdArgs += $Threads
    }

    # Use a custom solution name to avoid name collision with the folder
    # If you don't want a .sln at all, replace this with '--no-sln'.
    $cmdArgs += '--sln-name'
    $cmdArgs += ("$dllName-sln")

    # Finally, the path to the DLL
    $cmdArgs += $dllPath

    # Run dnSpy.Console
    & $DnSpyConsolePath $cmdArgs
}

Write-Host "`nAll done!"