# Run Java tests (PowerShell)
# Usage: .\run-java-tests.ps1

# Try to set JAVA_HOME automatically if not set
if (-not $env:JAVA_HOME -or $env:JAVA_HOME -eq "") {
  $userJdkDir = Join-Path $env:USERPROFILE ".jdk"
  if (Test-Path $userJdkDir) {
    $jdk = Get-ChildItem $userJdkDir -Directory -ErrorAction SilentlyContinue | Sort-Object Name -Descending | Select-Object -First 1
    if ($jdk) { $env:JAVA_HOME = $jdk.FullName }
  }
}

if (-not $env:JAVA_HOME) {
  Write-Warning "JAVA_HOME is not set. Set it to JDK 21 path for best results."
} else {
  Write-Host "Using JAVA_HOME=$env:JAVA_HOME"
}

Push-Location (Join-Path $PSScriptRoot "..\java")
try {
  mvn clean test
} finally {
  Pop-Location
}
