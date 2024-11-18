using Microsoft.Playwright;

namespace sourcedemo.PageObject
{
    // GlobalVariables class should be static and separate from ProductPage for reusability
    public static class GlobalVariables
    {
        // Declare the global variables to hold the shirt details

        public static string? TshirtName { get; set; }
        public static string? TshirtPrice { get; set; }
        public static string? TshirtDescription { get; set; }
    }

    // ProductPage class for handling product actions and details retrieval
    public class ProductPage(IPage page)
    {
        // Locators for the fields (You may need to adjust the XPath selectors)

         private readonly string PageUrl = "https://www.saucedemo.com/checkout-step-two.html";
        private ILocator AddToCartButton => page.Locator("(//button[contains(.,'Add to cart')])[3]");  // third item button
        private ILocator InventoryName => page.Locator("(//div[@data-test='inventory-item-name'])[3]");  // Inventory name
        private ILocator InventoryPrice => page.Locator("(//div[@data-test='inventory-item-price'])[3]");  // Inventory price
        private ILocator InventoryDescription => page.Locator("(//div[contains(@class,'inventory_item_desc')])[6]");  // Inventory description

        // Method to get the shirt details and store them in GlobalVariables
        public async Task GetShirtDetails()
        {
            // Retrieve text for shirt details and store them in global variables
            GlobalVariables.TshirtName = await InventoryName.InnerTextAsync();
            GlobalVariables.TshirtPrice = await InventoryPrice.InnerTextAsync();
            GlobalVariables.TshirtDescription = await InventoryDescription.InnerTextAsync();
        }

        // Method to add the t-shirt to the cart
        public async Task AddTshirtToCart()
        {
            // Click the Add to Cart button
            await AddToCartButton.ClickAsync();
        }

        // Test User action on the Product Page
        public async Task UserActionProductPage()
        {
        await GetShirtDetails();
        await AddTshirtToCart();
        }
    }
}
