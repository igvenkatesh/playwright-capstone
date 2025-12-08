from pages.login_page import LoginPage

def test_invalid_login_shows_error(page):
	login = LoginPage(page)
	login.goto()
	login.login("wrongUser", "wrongPass")

	error = page.locator("#errorMessage")
	assert error.is_visible(), "Expected error message to be visible for invalid login"
	assert "Invalid credentials" in error.inner_text()
