using System.Threading.Tasks;
using Microsoft.Playwright;

namespace PlaywrightCapstone.Pages
{
  public class LoginPage
  {
    private readonly IPage _page;

    public LoginPage(IPage page)
    {
      _page = page;
    }

      /// <summary>
      /// Page Object Model for the login page.
      /// Use `GotoAsync` to navigate to the login page and `LoginAsync` to perform
      /// a simple username/password submit. Tests should rely on this POM rather
      /// than using raw Playwright calls to keep behavior centralized.
      /// </summary>
      public async Task GotoAsync()
      {
          await _page.GotoAsync("https://practice.expandtesting.com/login");
      }

    public async Task LoginAsync(string username, string password)
    {
      await _page.FillAsync("#username", username);
      await _page.FillAsync("#password", password);
      await _page.ClickAsync("button[type=\"submit\"]");
    }
      /// <summary>
      /// Fill username and password fields and submit the form.
      /// </summary>
      /// <param name="username">Login username</param>
      /// <param name="password">Login password</param>
      public async Task LoginAsync(string username, string password)
      {
          // Fill the username and password input fields then click submit.
          await _page.FillAsync("#username", username);
          await _page.FillAsync("#password", password);
          await _page.ClickAsync("button[type=\"submit\"]");
      }
  }
}
