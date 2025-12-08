import { defineConfig, devices } from '@playwright/test';

// Playwright configuration file
export default defineConfig({
  // Directory containing test files (relative to this config file)
  testDir: './tests',
  testMatch: ['checkout.spec.ts'], // only run this file

  use: {
    // Base URL of the demo application
    baseURL: 'https://practice.expandtesting.com',
    // Run tests headless by default
    headless: true,
    // Capture screenshots only when tests fail
    screenshot: 'only-on-failure',
    // Retain video recordings on failure
    video: 'retain-on-failure'
  },

  // Run tests across multiple browsers
  projects: [
    { name: 'Chrome', use: { ...devices['Desktop Chrome'] } },
    { name: 'Firefox', use: { ...devices['Desktop Firefox'] } },
    { name: 'Edge', use: { ...devices['Desktop Edge'] } }
  ],

  // Reporters configuration
  reporter: [
    ['list'], // shows results in console
    ['json', { outputFile: '../dashboard/data/results.json' }] // writes JSON results for dashboard
  ]
});