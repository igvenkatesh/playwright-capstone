import { test, expect } from '@playwright/test';
import { LoginPage } from '../pages/loginPage';

// Negative test: invalid login should show error
test('Invalid login shows error', async ({ page }) => {
  const login = new LoginPage(page);

  await login.goto();
  await login.login('wrongUser', 'wrongPass'); // Invalid credentials

  // Validate error message
  await expect(page.locator('#errorMessage')).toContainText('Invalid credentials');
});