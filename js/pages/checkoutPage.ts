import { Page } from '@playwright/test';

// POM for Checkout page
export class CheckoutPage {
  constructor(private page: Page) { }

  // Proceed to checkout from cart
  async proceedToCheckout() {
    await this.page.click('#checkout');
  }

  // Fill payment details (dummy test card values)
  async fillPaymentDetails(card: string, expiry: string, cvv: string) {
    await this.page.fill('#cardNumber', card);
    await this.page.fill('#expiry', expiry);
    await this.page.fill('#cvv', cvv);
  }

  // Place the order
  async placeOrder() {
    await this.page.click('#placeOrder');
  }
}