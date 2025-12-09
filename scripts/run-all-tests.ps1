# Run all language test helpers sequentially (PowerShell)
# Usage: .\run-all-tests.ps1

# This script will run Java, .NET, Python, and JS test helpers in order.
# It continues on failure but reports a summary exit code at the end.

$errors = @()

Write-Host "Running Java tests..."
try { & (Join-Path $PSScriptRoot 'run-java-tests.ps1') } catch { $errors += "java: $_" }

Write-Host "Running .NET tests..."
try { & (Join-Path $PSScriptRoot 'run-dotnet-tests.ps1') } catch { $errors += "dotnet: $_" }

Write-Host "Running Python tests..."
try { & (Join-Path $PSScriptRoot 'run-python-tests.ps1') } catch { $errors += "python: $_" }

Write-Host "Running JS tests..."
try { & (Join-Path $PSScriptRoot 'run-js-tests.ps1') } catch { $errors += "js: $_" }

if ($errors.Count -gt 0) {
  Write-Error "Some tasks failed:`n$($errors -join "`n")"
  exit 1
} else {
  Write-Host "All helpers completed successfully."
  exit 0
}
