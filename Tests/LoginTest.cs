using Microsoft.Playwright;
using Microsoft.Playwright.Xunit;
using Xunit;

namespace Tests
{
    [Collection("Playwright")]
    public class LoginTest : PageTest
    {
        [Fact]
        public async Task Login_ValidCredentials_ShouldRedirectToDashboard()
        {
            await Page.GotoAsync("http://localhost:3000/login");
            await Page.EvaluateAsync("localStorage.clear()");
            await Context.ClearCookiesAsync();

            await Page.FillAsync("input[type='text']", "Testman");
            await Page.FillAsync("input[type='password']", "Testman12!");

            await Page.ClickAsync("button:has-text('Logga in')");

            await Expect(Page).ToHaveURLAsync("http://localhost:3000/dashboard");
        }
    }
}