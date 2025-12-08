package pages;

import com.microsoft.playwright.Page;

/**
 * POM for Checkout page.
 * Provides helpers for proceeding to checkout, filling in payment details, and
 * placing the order.
 */
public class CheckoutPage {
  private final Page page;

  public CheckoutPage(Page page) {
    this.page = page;
  }

  public void proceedToCheckout() {
    page.click("#checkout-button");
  }

  public void fillPaymentDetails() {
    // Fill card number
    page.fill("#card-number", "4111111111111111");
    // Fill expiry date
    page.fill("#card-expiry", "12/25");
    // Fill CVV
    page.fill("#card-cvv", "123");
  }

  public void placeOrder() {
    page.click("#place-order");
  }
}
