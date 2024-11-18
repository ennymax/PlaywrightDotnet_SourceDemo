using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;

namespace sourcedemo
{
    public abstract class BasePage : PageTest
    {
        protected new IBrowser? Browser { get; private set; }
        protected new IBrowserContext? Context { get; private set; }
        protected new IPage? Page { get; private set; }

        private string? baseUrl;

        private readonly BrowserTypeLaunchOptions _launchOptions = new()
        {
            Headless = false, // Set to false if debugging
            SlowMo = 100     // Slow down operations for better observation
        };

        [SetUp]
        public async Task BaseSetup()
        {
            baseUrl = Environment.GetEnvironmentVariable("url") ?? throw new ArgumentException("The URL is not set.");

            // Initialize Playwright
            var playwright = await Microsoft.Playwright.Playwright.CreateAsync();

            // Launch browser
            Browser = await playwright.Chromium.LaunchAsync(_launchOptions);

            // Configure Browser Context in incognito mode
            Context = await Browser.NewContextAsync(new BrowserNewContextOptions
            {
                ViewportSize = new() { Width = 1280, Height = 720 },
                UserAgent = "PlaywrightTest/1.0",
                IgnoreHTTPSErrors = true, // Useful for testing insecure environments
            });
            // Create a new page
            Page = await Context.NewPageAsync();

            // Navigate to the base URL
            await Page.GotoAsync(baseUrl);
        }

        [TearDown]
        public async Task BaseTeardown()
        {
            // Gracefully clean up resources
            if (Page != null) await Page.CloseAsync();
            if (Context != null) await Context.CloseAsync();
            if (Browser != null) await Browser.CloseAsync();
        }
    }
}
