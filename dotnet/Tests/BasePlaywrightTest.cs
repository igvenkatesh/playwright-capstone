using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;
using Microsoft.Playwright;
using NUnit.Framework;

namespace PlaywrightCapstone.Tests
{
  /// <summary>
  /// Base test class that sets up Playwright for NUnit tests.
  /// Responsibilities:
  /// - Ensure Playwright browsers are installed (runs the generated `playwright.ps1` if not present).
  /// - Launch a headless browser and provide a page instance for tests.
  /// </summary>
  public class BasePlaywrightTest
  {
    protected IPlaywright Playwright { get; private set; }
    protected IBrowser Browser { get; private set; }
    protected IPage Page { get; private set; }

    /// <summary>
    /// Test setup runs before each test. It attempts to ensure the Playwright
    /// browsers are available (by checking the user's ms-playwright folder) and
    /// will run the local generated `playwright.ps1` installer if needed.
    /// After that it creates a Playwright instance and launches a headless browser.
    /// </summary>
    [SetUp]
    public async Task SetUp()
    {
      // Ensure browsers are installed so BrowserType.LaunchAsync won't fail with "Executable doesn't exist"
      await EnsurePlaywrightBrowsersInstalledAsync();

      // Create Playwright and launch a browser for the test.
      Playwright = await Microsoft.Playwright.Playwright.CreateAsync();
      Browser = await Playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = true });
      Page = await Browser.NewPageAsync();
    }

    /// <summary>
    /// Tear down closes the browser and disposes Playwright.
    /// </summary>
    [TearDown]
    public async Task TearDown()
    {
      if (Browser != null)
        await Browser.CloseAsync();
      Playwright?.Dispose();
    }

    /// <summary>
    /// If Playwright browsers are not present in the user's AppData location,
    /// attempts to run the generated `playwright.ps1` found in the test binary
    /// output folder. This mirrors the manual step shown in repository docs / CI.
    ///</summary>
    private async Task EnsurePlaywrightBrowsersInstalledAsync()
    {
      try
      {
        var userProfile = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        var msPlaywrightPath = Path.Combine(userProfile, "AppData", "Local", "ms-playwright");

        // If ms-playwright folder exists and contains content, assume browsers are installed.
        if (Directory.Exists(msPlaywrightPath) && Directory.EnumerateFileSystemEntries(msPlaywrightPath).Any())
          return;

        // Determine the expected generated installer script location next to the test binary.
        var assemblyBin = AppContext.BaseDirectory; // runtime bin folder
        var scriptPath = Path.Combine(assemblyBin, "playwright.ps1");

        if (!File.Exists(scriptPath))
        {
          // No script available locally — skip automatic install. The project README or CI should run it.
          return;
        }

        // Run PowerShell to execute the installer script: `powershell.exe -NoProfile -ExecutionPolicy Bypass -File <script> install`
        var psi = new ProcessStartInfo
        {
          FileName = "powershell.exe",
          Arguments = $"-NoProfile -ExecutionPolicy Bypass -File \"{scriptPath}\" install",
          RedirectStandardOutput = true,
          RedirectStandardError = true,
          UseShellExecute = false,
          CreateNoWindow = true
        };

        using var proc = Process.Start(psi);
        if (proc == null)
          throw new InvalidOperationException("Failed to start PowerShell to install Playwright browsers.");

        var stdout = await proc.StandardOutput.ReadToEndAsync();
        var stderr = await proc.StandardError.ReadToEndAsync();
        proc.WaitForExit();

        if (proc.ExitCode != 0)
        {
          // If installation failed, surface an informative exception — tests can still be run manually after running the script.
          throw new Exception($"Playwright installer script failed (exit {proc.ExitCode}).\nSTDOUT: {stdout}\nSTDERR: {stderr}");
        }
      }
      catch
      {
        // Swallow exceptions to avoid blocking tests in environments where installer is not desired.
        // The calling code will surface browser launch errors if browsers are not available.
      }
    }
  }
}
