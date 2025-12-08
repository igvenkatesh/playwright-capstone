using System.Threading.Tasks;
using Microsoft.Playwright;

namespace PlaywrightCapstone.Pages
{
  /// <summary>
  /// Page Object Model for the checkout/cart page.
  /// Methods are resilient to small variations in demo app markup by attempting
  /// several common selectors before failing.
  /// </summary>
  public class CheckoutPage
  {
    private readonly IPage _page;

    public CheckoutPage(IPage page)
    {
      _page = page;
    }

    /// <summary>
    /// Attempts to trigger the checkout flow by trying several selectors.
    /// This helps the tests work against slightly different sample apps.
    /// </summary>
    public async Task ProceedToCheckoutAsync()
    {
      // try common selectors for checkout button to be resilient across sample apps
      if (await _page.Locator("#checkout").CountAsync() > 0)
      {
        await _page.ClickAsync("#checkout");
        return;
      }

      if (await _page.Locator("text=Checkout").CountAsync() > 0)
      {
        await _page.ClickAsync("text=Checkout");
        return;
      }

      // fallback: attempt to click button[type=submit] if present
      if (await _page.Locator("button[type=submit]").CountAsync() > 0)
      {
        await _page.ClickAsync("button[type=submit]");
        return;
      }
    }

    /// <summary>
    /// Fill payment fields with dummy data for tests that exercise payment flows.
    /// </summary>
    public async Task FillPaymentDetailsAsync(string card, string expiry, string cvv)
    {
      await _page.FillAsync("#cardNumber", card);
      await _page.FillAsync("#expiry", expiry);
      await _page.FillAsync("#cvv", cvv);
    }

    /// <summary>
    /// Click the final place-order button. Selector may vary by sample app.
    /// </summary>
    public async Task PlaceOrderAsync()
    {
      await _page.ClickAsync("#placeOrder");
    }
  }
}
