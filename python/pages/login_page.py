from playwright.sync_api import Page


class LoginPage:
	"""Page Object Model for the login page (synchronous Playwright).

	Use `goto()` to navigate to the login page and `login()` to perform a
	simple username/password submit. Tests should use this class to keep
	selectors and navigation in one place.
	"""

	def __init__(self, page: Page):
		self.page = page

	def goto(self) -> None:
		self.page.goto("/login")

	def login(self, username: str, password: str) -> None:
		self.page.fill("#username", username)
		self.page.fill("#password", password)
		self.page.click('button[type="submit"]')
