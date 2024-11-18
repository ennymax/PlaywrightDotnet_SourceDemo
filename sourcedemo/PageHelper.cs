using Microsoft.Playwright;

public class PageHelper(IPage page)
{

    private IPage _page = page;

   public static async Task<bool> VerifyTextContentAsync(ILocator locator, string expectedText)
    {
        // Ensure the expected text is not null or empty
        if (string.IsNullOrEmpty(expectedText))
            throw new ArgumentException("Expected text cannot be null or empty.", nameof(expectedText));

        // Get the actual text of the element
        var actualText = await locator.InnerTextAsync();

        // Check if the actual text contains the expected text
        return actualText.Contains(expectedText);
    }

    // Method to get text content from a locator
    public async Task<string> GetTextFromLocatorAsync(ILocator locator)
    {
        return await locator.TextContentAsync();  // Return the text content
    }

    public async Task<bool> IsElementVisibleAndTextNotEmptyAsync(ILocator locator)
    {
        // Check if the element is visible
        bool isVisible = await locator.IsVisibleAsync();

        // Check if the element's text is not empty
        string text = await locator.InnerTextAsync();
        bool isTextNotEmpty = !string.IsNullOrWhiteSpace(text);

        return isVisible && isTextNotEmpty; // Return true only if both conditions are satisfied
    }

    public async Task<decimal> GetNumericValueFromLocatorAsync(ILocator locator)
    {
        string text = await locator.InnerTextAsync();
        Console.WriteLine($"Extracted Text: {text}");

        text = text.Trim().Replace("$", "").Trim();  // Remove dollar sign
        text = new string(text.Where(c => char.IsDigit(c) || c == '.').ToArray()); // Only keep digits and the decimal point

        if (decimal.TryParse(text, out decimal value))
        {
            return value;
        }
        else
        {
            throw new InvalidOperationException($"Unable to parse the value: {text}");
        }

    }
}
