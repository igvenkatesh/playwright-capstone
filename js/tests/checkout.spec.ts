import { test, expect } from '@playwright/test';
import { LoginPage } from '../pages/loginPage';
import { ProductPage } from '../pages/productPage';
import { CheckoutPage } from '../pages/checkoutPage';

// Positive end-to-end checkout scenario
test('E-commerce checkout flow', async ({ page }) => {
  // Initialize page objects
  const login = new LoginPage(page);
  const product = new ProductPage(page);
  const checkout = new CheckoutPage(page);

  // Step 1: Login
  await login.goto();
  await login.login('practice', 'SuperSecretPassword!');

  // Step 2: Select product and add to cart
  // await product.selectProductByName('Laptop');
  // await product.addToCart();

  // Step 3: Checkout
  // await checkout.proceedToCheckout();
  //await checkout.fillPaymentDetails('4111111111111111', '12/25', '123');
  //await checkout.placeOrder();

  // Step 4: Validate confirmation message
  //await expect(page.locator('#orderConfirmation')).toContainText('Order placed successfully');
});