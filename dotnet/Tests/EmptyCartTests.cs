using System.Threading.Tasks;
using NUnit.Framework;
using PlaywrightCapstone.Pages;

namespace PlaywrightCapstone.Tests
{
  /// <summary>
  /// Validates that attempting to checkout with an empty cart does not proceed.
  /// The test accepts either an explicit error message or that the user remains
  /// on the cart page. This keeps the test resilient across sample apps.
  /// </summary>
  public class EmptyCartTests : BasePlaywrightTest
  {
    [Test]
    public async Task CheckoutBlockedWithEmptyCart()
    {
      // Navigate directly to the cart page
      await Page.GotoAsync("https://practice.expandtesting.com/cart");
      var checkout = new CheckoutPage(Page);

      // Attempt checkout using resilient selectors in CheckoutPage
      await checkout.ProceedToCheckoutAsync();

      // The demo app may show an error element, or may just stay on the cart page.
      var error = Page.Locator("#errorMessage");
      if (await error.CountAsync() > 0)
      {
        Assert.IsTrue(await error.IsVisibleAsync(), "Should show an error when attempting checkout with empty cart");
      }
      else
      {
        // Fallback: ensure we remain on the cart page (no successful checkout)
        Assert.IsTrue(Page.Url.Contains("/cart") || Page.Url.Contains("cart"), "Should remain on the cart page when checking out with an empty cart");
      }
    }
  }
}
