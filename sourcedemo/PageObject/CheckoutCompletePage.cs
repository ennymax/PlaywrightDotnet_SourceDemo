using Microsoft.Playwright;
using FluentAssertions;


namespace sourcedemo.PageObject
{
    public class ChecoutCompletePage(IPage page)
    {
        private readonly string PageUrl = "https://www.saucedemo.com/checkout-complete.html";
        private readonly string Successmsg = "Your order has been dispatched, and will arrive";
        private ILocator SuccessImage => page.Locator("//img[contains(@data-test,'pony-express')]");
        private ILocator OrderCompleteMessage => page.Locator("//div[contains(@data-test,'complete-text')]");
        private ILocator BackToProductButton => page.Locator("//button[contains(@data-test,'back-to-products')]");

        PageHelper pageHelper = new(page);

        // Navigate Back To Product Page
        public async Task ClickBackToProductButton()
        {
            await BackToProductButton.ClickAsync();
        }

        public async Task<string> GetTextFromLocatorAsync(ILocator locator)
        {
            return await pageHelper.GetTextFromLocatorAsync(locator);
        }

        public async Task<bool> VerifySuccessImageAsync()
        {
            return await SuccessImage.IsVisibleAsync();
        }

        public async Task<bool> VerifyOrderMsgAsync()
        {
            string actualName = await GetTextFromLocatorAsync(OrderCompleteMessage);
            return actualName.Contains(Successmsg);
        }

        public async Task OrderDetailPageUserActionAsync()
        {
            // Validate Page Url
            page.Url.Should().Be(PageUrl);

            //Check Order message is displayed
            bool isOrdermsgValid = await VerifyOrderMsgAsync();
            isOrdermsgValid.Should().BeTrue("Order Message Failed");

            //Verify success message is displayed
            bool isSuccessmsgValid = await VerifySuccessImageAsync();
            isSuccessmsgValid.Should().BeTrue("Success Image was not displayed Failed");

            await ClickBackToProductButton();
        }
    }
}
