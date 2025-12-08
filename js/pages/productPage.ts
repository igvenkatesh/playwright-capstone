import { Page } from '@playwright/test';

// POM for Product page
export class ProductPage {
  constructor(private page: Page) { }

  // Select product by visible text (e.g., "Laptop")
  async selectProductByName(name: string) {
    await this.page.click(`text=${name}`);
  }

  // Add selected product to cart
  async addToCart() {
    await this.page.click('#add-to-cart');
  }
}