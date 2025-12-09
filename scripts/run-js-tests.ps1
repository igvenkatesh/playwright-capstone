# Run JavaScript/TypeScript Playwright tests (PowerShell)
# Usage: .\run-js-tests.ps1

Push-Location (Join-Path $PSScriptRoot "..\js")
try {
  if (-not (Test-Path "node_modules")) {
    npm ci
  }
  # Ensure Playwright browsers are installed
  npx playwright install
  npx playwright test
} finally {
  Pop-Location
}
