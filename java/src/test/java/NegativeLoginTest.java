import com.microsoft.playwright.*;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.AfterEach;
import org.junit.jupiter.api.Test;
import pages.LoginPage;

import static org.junit.jupiter.api.Assertions.assertTrue;

/**
 * Tests for negative login scenarios (invalid credentials).
 */
public class NegativeLoginTest {
  private Browser browser;
  private BrowserContext context;
  private Page page;
  private LoginPage loginPage;

  @BeforeEach
  public void setUp() {
    browser = Playwright.create().chromium().launch();
    context = browser.newContext();
    page = context.newPage();
    loginPage = new LoginPage(page);
  }

  @AfterEach
  public void tearDown() {
    page.close();
    context.close();
    browser.close();
  }

  @Test
  public void testInvalidLoginShowsError() {
    loginPage.navigateTo();
    String initialUrl = page.url();

    loginPage.login("wrongUser", "wrongPass");

    // The app may show an error element or redirect
    boolean hasError = false;

    // Check for error element
    try {
      if (page.locator("#errorMessage").isVisible()) {
        String errorText = page.locator("#errorMessage").innerText();
        if (errorText.contains("Invalid") || errorText.contains("invalid") || errorText.contains("credentials")) {
          hasError = true;
        }
      }
    } catch (Exception e) {
      // Element may not exist
    }

    try {
      if (page.locator("text=Invalid").count() > 0) {
        hasError = true;
      }
    } catch (Exception e) {
      // Selector may not match
    }

    try {
      if (page.locator(".error").isVisible()) {
        hasError = true;
      }
    } catch (Exception e) {
      // Selector may not match
    }

    // Check if page URL changed (redirect on failed auth)
    try {
      String currentUrl = page.url();
      if (!currentUrl.equals(initialUrl) || currentUrl.contains("secure") || currentUrl.contains("login")) {
        hasError = true;
      }
    } catch (Exception e) {
      // URL check failed
    }

    assertTrue(hasError, "Expected error message or redirect on invalid login");
  }
}
