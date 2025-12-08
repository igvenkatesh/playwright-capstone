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
		# Try multiple selectors to be resilient to small markup differences.
		username_selectors = ["input#username", "#username", "input[name='username']", "input[placeholder*='user']", "input[type='text']"]
		password_selectors = ["input#password", "#password", "input[name='password']", "input[type='password']"]
		submit_selectors = ["button[type=\"submit\"]", "button:has-text(\"Login\")", "text=Login", "form >> input[type=submit]"]

		filled = False
		for sel in username_selectors:
			try:
				locator = self.page.locator(sel).first
				locator.fill(username)
				filled = True
				break
			except Exception:
				continue

		if not filled:
			# As a last resort, try to set the value via JS into the first input
			try:
				self.page.eval_on_selector("input", "(el, value) => el.value = value", username)
			except Exception:
				pass

		filled = False
		for sel in password_selectors:
			try:
				locator = self.page.locator(sel).first
				locator.fill(password)
				filled = True
				break
			except Exception:
				continue

		# Submit with the first selector that works
		for sel in submit_selectors:
			try:
				self.page.locator(sel).first.click()
				return
			except Exception:
				continue

		# If no submit button found, try pressing Enter on password field
		try:
			self.page.keyboard.press("Enter")
		except Exception:
			pass
