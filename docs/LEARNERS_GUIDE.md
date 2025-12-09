# Playwright Capstone — Learner Guide

This guide explains the project structure, core concepts (Playwright, Page Object Model), and step-by-step workflows to run and extend tests in this multi-language Playwright capstone. It's written for learners new to Playwright and cross-language test automation.

**Who this is for:** developers or testers learning Playwright-based automation, Page Object Model (POM), and multi-language test projects in Java, .NET, Python and JavaScript.

---

**Prerequisites**

- Git installed and a clone of this repository.
- On Windows PowerShell: use `powershell.exe` (the commands below are written for PowerShell).
- JDK 21 installed (for Java tests).
- .NET 8 SDK installed (for C# tests).
- Python 3.10+ (for pytest tests) and dependencies from `python/requirements.txt`.
- Node 18+ and Playwright browsers installed for the JS tests (see `js` folder).

---

**Project layout (high level)**

- `java/` — Maven Java project with Playwright (JUnit 5) tests and POMs in `src/test/java/pages`.
- `dotnet/` — .NET project with Playwright tests (NUnit) and POMs in `Pages/`.
- `python/` — pytest-based tests and POMs in `pages/`.
- `js/` — Playwright Test (TypeScript) project.
- `dashboard/` — static dashboard used by the capstone demo.

---

**Core Concepts**

- Playwright: a browser automation library that supports Chromium, Firefox, and WebKit. It can be used from multiple languages.
- Page Object Model (POM): an abstraction pattern that encapsulates page interactions into classes (e.g., `LoginPage`, `ProductPage`, `CheckoutPage`). Tests call POM methods for readability and reuse.
- Test tolerance: sample/demo apps differ in DOM and behavior. Tests in this repo use tolerant selectors and try/catch fallbacks to remain robust across variations.

---

**How the Java tests are organized**

- POMs are in `java/src/test/java/pages/` (e.g., `LoginPage.java`, `ProductPage.java`, `CheckoutPage.java`).
- Tests are JUnit 5 classes in `java/src/test/java` (e.g., `NegativeLoginTest.java`).
- `pom.xml` configures Playwright and JUnit. Java code targets JDK 21.

Run Java tests (PowerShell):

```powershell
$env:JAVA_HOME = 'C:\Users\<you>\.jdk\jdk-21.0.8'
cd .\java
mvn clean test
```

Notes:
- The tests open real browsers. First run may download Playwright artifacts.
- If Maven can't find `JAVA_HOME`, set it explicitly (as above).

---

**How the .NET tests are organized**

- POMs live in `dotnet/Pages` and tests in `dotnet/Tests`.
- Uses Playwright for .NET and NUnit.

Run .NET tests (PowerShell):

```powershell
cd .\dotnet
# Ensure Playwright browsers are installed, sometimes provided by the repo helpers
# If needed, run the included powershell script to install browsers: .\bin\Debug\net8.0\playwright.ps1
dotnet test --no-build
```

---

**How the Python tests are organized**

- POMs live in `python/pages` and tests in `python/tests`.
- Uses Playwright Python sync API and `pytest`.

Install Python deps and run tests:

```powershell
cd .\python
python -m venv .venv
.\.venv\Scripts\Activate.ps1
pip install -r requirements.txt
pytest -v
```

Notes:
- If Playwright browsers are missing, run `playwright install` inside the activated venv.

---

**How the JavaScript/TypeScript tests are organized**

- Playwright Test config is in `js/playwright.config.ts`.
- Pages in `js/pages` and tests in `js/tests`.

Run JS tests (PowerShell):

```powershell
cd .\js
npm install
# install Playwright browsers if needed
npx playwright install
npx playwright test
```

You can run a single test or a specific browser configuration using Playwright Test CLI flags. Example:

```powershell
npx playwright test tests/checkout.spec.ts --project=chromium
```

---

**Running all tests on this machine (quick checklist)**

1. Ensure system prerequisites are installed (JDK, .NET, Python, Node).
2. Open PowerShell and run per-language steps above in separate terminals or sequentially.

Optional unified commands (run from repo root):

```powershell
# Java
$env:JAVA_HOME = 'C:\Users\<you>\.jdk\jdk-21.0.8'; cd .\java; mvn test
# .NET
cd ..\dotnet; dotnet test --no-build
# Python
cd ..\python; .\.venv\Scripts\Activate.ps1; pytest -q
# JS
cd ..\js; npm ci; npx playwright test -q
```

---

**Troubleshooting & tips**

- Browser download issues:
  - Ensure network access for Playwright to download browser binaries.
  - For Java, Playwright may try to run an installer batch/script; if you see "The batch file cannot be found" in output, re-run the tests to allow Playwright to finish installation.

- Flaky tests (timeouts, navigation differences):
  - Increase timeouts or add retries in test config.
  - Use tolerant selectors (try `#errorMessage`, `.error`, `text=Cart is empty`), and guard interactions with try/catch.

- Reserved keywords (Java):
  - Avoid method names like `goto` in Java (reserved). Use `navigateTo()` instead.

- Test artifacts:
  - Maven surefire reports and compiled tests are created under `java/target/`.
  - .NET test results are found where `dotnet test` outputs them.

---

**Extending tests (how to add a new test)**

1. Pick a language directory (`java`, `dotnet`, `python`, or `js`).
2. Add a POM class in the language's `pages` folder if you need new page interactions.
3. Create a test file under the language's test folder following existing patterns.
4. Run the language test suite locally and iterate until stable.
5. Commit and push changes to a branch; open a PR for review.

---

**CI notes**

- The repository contains GitHub Actions workflow(s) under `.github/workflows/` for matrix testing. The CI will run tests per-language as configured.
- CI environments may use headless browsers and pre-installed SDKs; ensure local dev runs are stable before pushing.

---

**Further reading**

- Playwright docs: https://playwright.dev
- Page Object Model overview: https://martinfowler.com/bliki/PageObject.html
- JUnit 5 user guide: https://junit.org/junit5/docs/current/user-guide/

---

If you'd like, I can also:
- Add quick start shell scripts for Windows in `scripts/` to run per-language tests.
- Add badges or a short CONTRIBUTING guide for test authors.

Enjoy exploring the Playwright capstone! If you'd like me to add scripts or CI updates, tell me which language(s) to target.

---

**Helper scripts included**

This repo now contains PowerShell helper scripts under `scripts/` to simplify running tests locally on Windows:

- `scripts/run-java-tests.ps1` — runs `mvn clean test` for the Java project (attempts to set `JAVA_HOME` if unset).
- `scripts/run-dotnet-tests.ps1` — runs `dotnet test --no-build` in the `dotnet/` folder.
- `scripts/run-python-tests.ps1` — creates/activates a venv, installs `requirements.txt`, and runs `pytest`.
- `scripts/run-js-tests.ps1` — installs JS deps and Playwright browsers then runs `npx playwright test`.
- `scripts/run-all-tests.ps1` — runs the above helpers sequentially and reports failures.

Use these from PowerShell at the repository root. Example:

```powershell
.\scripts\run-all-tests.ps1
```

**CI & Dashboard publishing**

- A GitHub Actions workflow `.github/workflows/ci.yml` runs tests for each language and writes a Playwright JSON report into `dashboard/data/results.json` (for JS tests). The workflow also uploads test artifacts and publishes the `dashboard/` folder as an artifact suitable for GitHub Pages.
- The dashboard reads `dashboard/data/results.json` and renders a simple table and bar chart of pass/fail counts. If you run tests locally and want the dashboard to show local data, copy a Playwright JSON report into `dashboard/data/results.json` (or run the JS test with `--reporter=json=dashboard/data/results.json`).

**Commenting & Documentation Guidelines (for learners)**

- Keep POMs small and documented. Add a short file/class docstring or comment at the top that explains the purpose of the POM and the public methods it exposes.
- For methods, include a one-line comment (or docstring) explaining the intent and any important selectors or fallbacks.
- Examples in this repo:
  - Java POMs use Javadoc-style comments (see `java/src/test/java/pages/`).
  - .NET POMs include XML doc comments (see `dotnet/Pages/`).
  - Python POMs include triple-quoted docstrings (see `python/pages/`).
  - TypeScript POMs include top-of-file comments and inline comments for selectors (see `js/pages/`).

Why this matters:
- Well-commented POMs make tests easier to read and maintain — tests should describe *what* they verify; POMs describe *how* interactions are performed.

If you'd like, I can add a small checklist into `CONTRIBUTING.md` showing a short template for new POMs and test files (recommended docstring blocks). Would you like that? 
