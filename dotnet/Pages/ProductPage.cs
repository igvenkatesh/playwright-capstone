using System.Threading.Tasks;
using Microsoft.Playwright;

namespace PlaywrightCapstone.Pages
{
  /// <summary>
  /// Page Object Model for product pages. Keep selectors centralized here so
  /// if the demo app markup changes, only this class needs updates.
  /// </summary>
  public class ProductPage
  {
    private readonly IPage _page;

    public ProductPage(IPage page)
    {
      _page = page;
    }

    /// <summary>
    /// Click a product by visible name text. Example: "Laptop".
    /// </summary>
    public async Task SelectProductByNameAsync(string name)
    {
      await _page.ClickAsync($"text={name}");
    }

    /// <summary>
    /// Add the currently selected product to cart using a standard id-based selector.
    /// </summary>
    public async Task AddToCartAsync()
    {
      await _page.ClickAsync("#add-to-cart");
    }
  }
}
