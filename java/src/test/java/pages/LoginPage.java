package pages;

import com.microsoft.playwright.Page;

/**
 * Page Object Model for the login page.
 * Use goto() to navigate to the login page and login() to perform
 * a simple username/password submit. Tests should rely on this POM rather
 * than using raw Playwright calls to keep behavior centralized.
 */
public class LoginPage {
  private final Page page;

  public LoginPage(Page page) {
    this.page = page;
  }

  public void navigateTo() {
    page.navigate("https://practice.expandtesting.com/login");
  }

  /**
   * Fill username and password fields and submit the form.
   *
   * @param username Login username
   * @param password Login password
   */
  public void login(String username, String password) {
    page.fill("#username", username);
    page.fill("#password", password);
    page.click("button[type=\"submit\"]");
  }
}
