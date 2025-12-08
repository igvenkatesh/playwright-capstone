import { Page } from '@playwright/test';

// Page Object Model (POM) for Login page
export class LoginPage {
  constructor(private page: Page) { }

  // Navigate to login page
  async goto() {
    await this.page.goto('/login');
  }

  // Perform login with username and password
  async login(username: string, password: string) {
    await this.page.fill('#username', username);       // Fill username input
    await this.page.fill('#password', password);       // Fill password input
    await this.page.click('button[type="submit"]');    // Click login button
  }
}