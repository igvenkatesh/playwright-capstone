# Run Python tests (PowerShell)
# Usage: .\run-python-tests.ps1

Push-Location (Join-Path $PSScriptRoot "..\python")
try {
  if (-not (Test-Path ".venv")) {
    Write-Host "Creating venv..."
    python -m venv .venv
  }

  Write-Host "Activating venv and installing requirements (if needed)..."
  .\.venv\Scripts\Activate.ps1
  try {
    pip install -r requirements.txt
  } catch {
    Write-Warning "Failed to install requirements; ensure internet access or preinstalled packages."
  }

  pytest -v
} finally {
  Pop-Location
}
