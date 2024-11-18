using Microsoft.Playwright;
using FluentAssertions;


namespace sourcedemo.PageObject
{
    public class CheckoutOverviewPage(IPage page)
    {
        private readonly string PageUrl = "https://www.saucedemo.com/checkout-step-two.html";

        private ILocator TshirtName => page.Locator("//div[contains(@data-test,'inventory-item-name')]");
        private ILocator TshirtDescription => page.Locator("//div[contains(@data-test,'inventory-item-desc')]");
        private ILocator TshirtPrice => page.Locator("//div[contains(@data-test,'inventory-item-price')]");
        private ILocator PaymentInformation => page.Locator("//div[contains(@data-test,'payment-info-value')]");
        private ILocator ShippingInformation => page.Locator("//div[@data-test='shipping-info-value']");
        private ILocator SubTotal => page.Locator("//div[contains(@data-test,'subtotal-label')]");
        private ILocator Tax => page.Locator("//div[contains(@data-test,'tax-label')]");
        private ILocator Total => page.Locator("//div[@data-test='total-label']");
        private ILocator FinishButton => page.Locator("//button[@data-test='finish']");

        PageHelper pageHelper = new(page);

        public async Task<bool> VerifyTshirtNameAsync(string expectedName)
        {
            string actualName = await GetTextFromLocatorAsync(TshirtName);
            return actualName.Contains(expectedName);
        }

        public async Task<bool> VerifyTshirtDescriptionAsync(string expectedName)
        {
            string actualName = await GetTextFromLocatorAsync(TshirtDescription);
            return actualName.Contains(expectedName);
        }

        public async Task<bool> VerifyTshirtPriceAsync(string expectedName)
        {
            string actualName = await GetTextFromLocatorAsync(TshirtPrice);
            return actualName.Contains(expectedName);
        }

        public async Task<string> GetTextFromLocatorAsync(ILocator locator)
        {
            return await pageHelper.GetTextFromLocatorAsync(locator);
        }

        public async Task<bool> VerifyPaymentInforAsync()
        {
            return await pageHelper.IsElementVisibleAndTextNotEmptyAsync(PaymentInformation);

        }


        public async Task<bool> VerifyShippingInformationAsync()
        {
            return await pageHelper.IsElementVisibleAndTextNotEmptyAsync(ShippingInformation);
        }

        public async Task<bool> VerifyTotalOrderAmount()
        {
            decimal subtotal = await pageHelper.GetNumericValueFromLocatorAsync(SubTotal);
            decimal tax = await pageHelper.GetNumericValueFromLocatorAsync(Tax);
            decimal expectedTotal = subtotal + tax;  // Calculate expected total

            // Retrieve the displayed total from the page
            decimal displayedTotal = await pageHelper.GetNumericValueFromLocatorAsync(Total);
            return expectedTotal.Equals(displayedTotal);
        }

        public async Task ClickFinishOrderButton()
        {
            await FinishButton.ClickAsync();
        }


        public async Task UserActionCheckoutOverviewPage()
        {
            // Validate Page Url
            page.Url.Should().Be(PageUrl);

            // Check TshirtName on the Checkout page
            bool isTshirtNameValid_Checkout = await VerifyTshirtNameAsync(GlobalVariables.TshirtName);
            isTshirtNameValid_Checkout.Should().BeTrue("Shirt name verification on checkout page failed.");

            // Check Tshirt Description on the Checkout page
            bool isTshirtDescriptionValid_Checkout = await VerifyTshirtDescriptionAsync(GlobalVariables.TshirtDescription);
            isTshirtDescriptionValid_Checkout.Should().BeTrue("Shirt description verification on checkout page failed.");

            // Check Tshirt Price on the Checkout page
            bool isTshirtPriceValid_Checkout = await VerifyTshirtPriceAsync(GlobalVariables.TshirtPrice);
            isTshirtPriceValid_Checkout.Should().BeTrue("Shirt price verification on checkout page failed.");

            // Verify payment and shipping information
            //Check payment information is displayed
            bool isPaymentValid = await VerifyPaymentInforAsync();
            isPaymentValid.Should().BeTrue("Payment information validation failed.");

            //Check Shipping information is displayed
            bool isShippingValid = await VerifyShippingInformationAsync();
            isShippingValid.Should().BeTrue("Shipping information validation failed.");

            // Verify the total order amount is displayed
            bool isTotalOrderValid = await VerifyTotalOrderAmount();
            isTotalOrderValid.Should().BeTrue("The total order amount is not correct.");

            // Finish the order
            await ClickFinishOrderButton();
        }
    }
}
