# Run .NET tests (PowerShell)
# Usage: .\run-dotnet-tests.ps1

Push-Location (Join-Path $PSScriptRoot "..\dotnet")
try {
  # dotnet test will build by default; use --no-build if you already built
  dotnet test --no-build
} finally {
  Pop-Location
}
