package pages;

import com.microsoft.playwright.Page;

/**
 * POM for Product page.
 * Provides simple helpers to select a product by its visible name and add it to
 * cart.
 */
public class ProductPage {
  private final Page page;

  public ProductPage(Page page) {
    this.page = page;
  }

  /**
   * Select product by its visible text.
   *
   * @param name Product name to click
   */
  public void selectProductByName(String name) {
    page.click("text=" + name);
  }

  public void addToCart() {
    page.click("#add-to-cart");
  }
}
