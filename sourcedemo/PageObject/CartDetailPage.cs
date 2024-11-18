using Microsoft.Playwright;
using FluentAssertions;

namespace sourcedemo.PageObject
{
    public class CartDetailPage
    {
         private readonly string PageUrl = "https://www.saucedemo.com/checkout-step-two.html";

        private readonly IPage _page;
        private readonly PageHelper _pageHelper;

        // Constructor
        public CartDetailPage(IPage page)
        {
            _page = page;
            _pageHelper = new PageHelper(page);
        }

        // Locators for the fields
        private ILocator CheckoutButton => _page.Locator("//button[contains(@id,'checkout')]");
        private ILocator InventoryNameLocator => _page.Locator("//div[contains(@class,'inventory_item_name')]");
        private ILocator InventoryPriceLocator => _page.Locator("//div[contains(@class,'inventory_item_price')]");
        private ILocator InventoryDescriptionLocator => _page.Locator("//div[contains(@class,'inventory_item_desc')]");

        // Method to click the checkout button
        public async Task CheckoutAsync()
        {
            await CheckoutButton.ClickAsync();
        }

        // Method to get text from a locator
        public async Task<string> GetTextFromLocatorAsync(ILocator locator)
        {
            return await _pageHelper.GetTextFromLocatorAsync(locator);
        }

        // Method to validate the shirt name
        public async Task<bool> ValidateShirtNameAsync(string expectedShirtName)
        {
            string actualName = await GetTextFromLocatorAsync(InventoryNameLocator);
            return actualName.Contains(expectedShirtName);
        }

        // Method to validate the shirt description
        public async Task<bool> ValidateShirtDescriptionAsync(string expectedDescription)
        {
            string actualDescription = await GetTextFromLocatorAsync(InventoryDescriptionLocator);
            return actualDescription.Contains(expectedDescription);
        }

        // Method to validate the shirt price
        public async Task<bool> ValidateShirtPriceAsync(string expectedPrice)
        {
            string actualPrice = await GetTextFromLocatorAsync(InventoryPriceLocator);
            return actualPrice.Contains(expectedPrice);
        }

        // Composite method to perform user checks and actions
        public async Task UserActionCheckoutPage()
        {
            // Check T-Shirt Name on the product page
            bool isTshirtNameValid = await ValidateShirtNameAsync(GlobalVariables.TshirtName);
            isTshirtNameValid.Should().BeTrue("The shirt name did not match the expected value.");

            // Check T-Shirt Description on the product page
            bool isTshirtDescriptionValid = await ValidateShirtDescriptionAsync(GlobalVariables.TshirtDescription);
            isTshirtDescriptionValid.Should().BeTrue("The shirt description did not match the expected value.");

            // Check T-Shirt Price on the product page
            bool isTshirtPriceValid = await ValidateShirtPriceAsync(GlobalVariables.TshirtPrice);
            isTshirtPriceValid.Should().BeTrue("The shirt price did not match the expected value.");

            // Proceed to checkout
            await CheckoutAsync();
        }
    }
}
