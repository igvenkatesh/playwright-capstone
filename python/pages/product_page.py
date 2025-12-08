from playwright.sync_api import Page

class ProductPage:
	"""POM for Product page (synchronous Playwright).

	Provides simple helpers to select a product by its visible name and add it to cart.
	"""

	def __init__(self, page: Page):
		self.page = page

	def select_product_by_name(self, name: str) -> None:
		# Click the product by its visible text
		self.page.click(f"text={name}")

	def add_to_cart(self) -> None:
		self.page.click("#add-to-cart")
