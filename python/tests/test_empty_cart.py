from pages.checkout_page import CheckoutPage

def test_checkout_blocked_with_empty_cart(page):
	page.goto("/cart")
	checkout = CheckoutPage(page)

	checkout.proceed_to_checkout()

	error = page.locator("#errorMessage")
	assert error.is_visible(), "Expected cart-empty error to be visible"
	assert "Cart is empty" in error.inner_text()
