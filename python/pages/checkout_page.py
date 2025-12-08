from playwright.sync_api import Page


class CheckoutPage:
	"""POM for Checkout page (synchronous Playwright).

	Helpers to proceed to checkout, fill payment details, and place the order.
	"""

	def __init__(self, page: Page):
		self.page = page

	def proceed_to_checkout(self) -> None:
		# Attempt common selectors, fall back to navigating to checkout page
		selectors = ["#checkout", "button:has-text(\"Checkout\")", "text=Checkout", "a:has-text(\"Checkout\")"]
		for sel in selectors:
			try:
				self.page.locator(sel).first.click()
				return
			except Exception:
				continue

		# If no selector worked, try navigating directly
		try:
			self.page.goto("/checkout")
		except Exception:
			pass

	def fill_payment_details(self, card: str, expiry: str, cvv: str) -> None:
		self.page.fill("#cardNumber", card)
		self.page.fill("#expiry", expiry)
		self.page.fill("#cvv", cvv)

	def place_order(self) -> None:
		self.page.click("#placeOrder")
