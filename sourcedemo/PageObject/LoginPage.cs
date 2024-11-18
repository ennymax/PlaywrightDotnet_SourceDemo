using Microsoft.Playwright;

namespace sourcedemo.PageObject
{
    public class LoginPage(IPage page)
    {
        private string? username;
        private string? password;

        // Locators for the fields
        private ILocator Username => page.Locator("[data-test='username']");  // Corrected for username
        private ILocator Password => page.Locator("[data-test='password']");  // Password locator
        private ILocator LoginButton => page.Locator("[data-test='login-button']");  // Corrected for login button

        // Method to login as a Standard User
        public async Task LoginAsStandardUser()
        {
            username = Environment.GetEnvironmentVariable("username") ?? throw new ArgumentException("The username is not set.");
            password = Environment.GetEnvironmentVariable("password") ?? throw new ArgumentException("The password is not set.");
            
            await Username.FillAsync(username);
            await Password.FillAsync(password);

            // Click the login button
            await LoginButton.ClickAsync();
        }
    }
}
