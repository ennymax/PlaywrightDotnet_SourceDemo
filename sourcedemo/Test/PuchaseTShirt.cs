using sourcedemo.PageObject;
using FluentAssertions;

namespace sourcedemo.Test;
[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class PuchaseTShirt : BasePage
{

    [Test]
    public async Task HasTitle()
    {
        // Initialize page objects
        LoginPage loginPage = new(Page);
        ProductPage productPage = new(Page);
        NavBar navBar = new(Page);
        CartDetailPage cartDetailPage = new(Page);
        CheckoutInformation checkoutInformation = new(Page);
        ChecoutCompletePage checoutCompletePage = new(Page);
        CheckoutOverviewPage checkoutOverviewPage = new(Page);

        // Perform login as a standard User
        await loginPage.LoginAsStandardUser();

        // Test User action on the Product Page
        await productPage.UserActionProductPage();

        // Verify cart count usings FluentAssertions
        bool isCountValid = await navBar.CartCount("1");
        isCountValid.Should().BeTrue("because the cart count should match the expected value of '1'");

        // Proceed to the cart detail page
        await navBar.ClickCartBadge();

        // Test user Action  on the Checkout Page
        await cartDetailPage.UserActionCheckoutPage();

        // Test user Action on Customer Details page
        await checkoutInformation.UserActionCustomerDetailPage();

        // Test User Action on Checkout overview page
        await checkoutOverviewPage.UserActionCheckoutOverviewPage();

        // Test User Action on the Order Details Page
        await checoutCompletePage.OrderDetailPageUserActionAsync();

    }
}
