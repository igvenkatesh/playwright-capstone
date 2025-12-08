from playwright.sync_api import Page


class CheckoutPage:
	"""POM for Checkout page (synchronous Playwright).

	Helpers to proceed to checkout, fill payment details, and place the order.
	"""

	def __init__(self, page: Page):
		self.page = page

	def proceed_to_checkout(self) -> None:
		self.page.click("#checkout")

	def fill_payment_details(self, card: str, expiry: str, cvv: str) -> None:
		self.page.fill("#cardNumber", card)
		self.page.fill("#expiry", expiry)
		self.page.fill("#cvv", cvv)

	def place_order(self) -> None:
		self.page.click("#placeOrder")
