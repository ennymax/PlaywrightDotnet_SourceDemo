using Microsoft.Playwright;

namespace sourcedemo.PageObject
{
    public class CheckoutInformation
    {
        private readonly string PageUrl = "https://www.saucedemo.com/checkout-step-two.html";

        private readonly IPage _page;
        private RandomUserGenerator? _randomUserGenerator;

        // Locators for Customer Information
        private ILocator FirstName => _page.Locator("//input[contains(@data-test,'firstName')]");
        private ILocator LastName => _page.Locator("//input[contains(@data-test,'lastName')]");
        private ILocator ZipCode => _page.Locator("//input[contains(@data-test,'postalCode')]");
        private ILocator ContinueButton => _page.Locator("//input[@data-test='continue']");

        // Constructor
        public CheckoutInformation(IPage page)
        {
            _page = page;
        }

        // Method to click the Continue button
        public async Task ContinueAsync()
        {
            await ContinueButton.ClickAsync();
        }

        // Method to fill customer information
        public async Task CustomerDetailsAsync(string firstname, string lastname, string zipcode)
        {
            await FirstName.FillAsync(firstname);
            await LastName.FillAsync(lastname);
            await ZipCode.FillAsync(zipcode);
        }

        public async Task UserActionCustomerDetailPage()
        {
            _randomUserGenerator = new RandomUserGenerator();
            // Generate random user data
            var person = _randomUserGenerator.GenerateRandomPerson();

            // Fill customer details with the generated data
            await CustomerDetailsAsync(person.FirstName, person.LastName, person.ZipCode);
            await ContinueAsync();
        }
    }
}
