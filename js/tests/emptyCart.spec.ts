import { test, expect } from '@playwright/test';
import { CheckoutPage } from '../pages/checkoutPage';

// Negative test: checkout blocked with empty cart
test('Checkout blocked with empty cart', async ({ page }) => {
  await page.goto('/cart'); // Navigate directly to cart
  const checkout = new CheckoutPage(page);

  await checkout.proceedToCheckout(); // Try to checkout with empty cart

  // Validate error message
  await expect(page.locator('#errorMessage')).toContainText('Cart is empty');
});