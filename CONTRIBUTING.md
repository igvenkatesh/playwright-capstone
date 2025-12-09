# Contributing to Playwright Capstone

Thank you for contributing! This document explains how to add tests, follow repository conventions, and validate changes locally before opening a PR.

## Branching & PRs

- Create a topic branch from `main` (or the repo's main development branch):
  - `git checkout -b feat/add-new-test`
- Keep commits focused and atomic. Use conventional commit messages, e.g., `feat(java): add checkout test`.
- Open a PR and include the target language, quick description of what the test covers, and any special setup.

## Adding tests

1. Choose language directory: `java/`, `dotnet/`, `python/`, or `js/`.
2. Add Page Object Model (POM) classes under the language's `pages` folder for any new page interactions.
3. Add tests under the language's tests folder, following the existing patterns.
   - Java: JUnit 5 tests in `java/src/test/java/`
   - .NET: NUnit tests in `dotnet/Tests`
   - Python: pytest tests in `python/tests`
   - JS: Playwright Test in `js/tests`
4. Keep tests resilient: use tolerant selectors, guard UI assumptions with try/catch, and avoid depending on exact text where possible.

### POM / Test File Template Checklist

When adding a new Page Object or test file, follow this quick checklist to keep things consistent and reviewable:

- [ ] Create a POM file in the language's `pages` folder (e.g., `java/src/test/java/pages/`, `dotnet/Pages/`, `python/pages/`, `js/pages/`).
- [ ] Add a top-of-file doc comment describing the POM's purpose and public methods.
- [ ] For each public method, add a one-line comment or docstring describing intent and any fallback selectors used.
- [ ] Keep methods single-responsibility (one user action per method).
- [ ] Add at least one test that uses the new POM, placed in the language's test folder.
- [ ] Run that language's test helper locally (see `scripts/`), and confirm the test passes.
- [ ] Ensure tests are tolerant of minor UI differences (use try/catch or multiple selectors as appropriate).
- [ ] Add or update README/guide if the new test covers a new feature or requires special setup.

Example Javadoc-style header (Java):
```java
/**
 * POM for the login page.
 * Provides `navigateTo()` and `login(username,password)` helpers used by tests.
 */
public class LoginPage { ... }
```

Example Python docstring (Python):
```python
"""POM for login page.

Methods:
 - goto()
 - login(username, password)
"""
```

## Running tests locally (quick)

- Java

```powershell
# from repo root
scripts\run-java-tests.ps1
```

- .NET

```powershell
scripts\run-dotnet-tests.ps1
```

- Python

```powershell
scripts\run-python-tests.ps1
```

- JS

```powershell
scripts\run-js-tests.ps1
```

- All

```powershell
scripts\run-all-tests.ps1
```

## Test and style expectations

- Keep POM methods small and focused (one interaction per method).
- Tests should be readable: prefer `login.login("user","pass")` to inline sequences in the test body when possible.
- Use clear assertions with helpful messages.
- Avoid adding large binary files; do not commit browser artifacts under `node_modules` or similar.

## CI & Flaky tests

- If a test flakes, add retries or increase timeouts in the test config rather than marking it as skipped.
- Document flaky tests in the PR description and reference why the change is temporary.

## Troubleshooting

- If CI fails due to missing browser binaries, run the local script to install Playwright browsers for the language.
- For Java reserved keyword issues, rename methods (avoid `goto`).

## Style & Formatting

- Keep language idiomatic (Java code formatted with your chosen formatter; don't introduce wholesale reformatting in the PR).
- Add tests only for the change you make. Avoid unrelated file changes.

Thanks — we appreciate well-documented, tested contributions!