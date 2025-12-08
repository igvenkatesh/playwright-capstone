using System.Threading.Tasks;
using NUnit.Framework;
using PlaywrightCapstone.Pages;

namespace PlaywrightCapstone.Tests
{
  /// <summary>
  /// Negative login test validates that invalid credentials do not allow access.
  /// The test accepts either an explicit error message element or the app
  /// remaining on the login page (some sample apps vary in behavior).
  /// </summary>
  public class NegativeLoginTests : BasePlaywrightTest
  {
    [Test]
    public async Task InvalidLoginShowsError()
    {
      var login = new LoginPage(Page);

      // Navigate and submit invalid credentials
      await login.GotoAsync();
      await login.LoginAsync("wrongUser", "wrongPass");

      // Some demo apps show an element with id `errorMessage`, others simply stay
      // on the login page. We accept both behaviors to keep the test robust.
      var error = Page.Locator("#errorMessage");
      if (await error.CountAsync() > 0)
      {
        Assert.IsTrue(await error.IsVisibleAsync(), "Error message should be visible for invalid login");
      }
      else
      {
        // Fallback: ensure we are still on the login page (no redirect)
        Assert.IsTrue(Page.Url.Contains("/login") || Page.Url.Contains("login"), "Should remain on login page after invalid credentials");
      }
    }
  }
}
