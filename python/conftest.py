import pytest
from playwright.sync_api import sync_playwright

@pytest.fixture(scope="session")
def playwright_context():
    with sync_playwright() as p:
        browser = p.chromium.launch(headless=True)
        context = browser.new_context(base_url="https://practice.expandtesting.com")
        yield context
        browser.close()

@pytest.fixture
def page(playwright_context):
    return playwright_context.new_page()