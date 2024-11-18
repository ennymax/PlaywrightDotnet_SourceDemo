using Microsoft.Playwright;


namespace sourcedemo.PageObject
{
    public class NavBar(IPage page)
    {
        // Locator for the cart badge
        private ILocator CartBadge => page.Locator("//a[contains(@class,'shopping_cart_link')]");

        // Method to click the cart badge
        public async Task ClickCartBadge()
        {
            await CartBadge.ClickAsync();
        }

        // Method to verify the Cart Count
        public async Task<bool> CartCount(string countText)
        {
            // Get the actual text of the element
            var actualText = await CartBadge.InnerTextAsync();

            // Check if the actual text contains the expected text
            return actualText.Contains(countText);
        }
    }
}
