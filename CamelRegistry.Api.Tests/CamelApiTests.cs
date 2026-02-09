using System.Net;
using System.Net.Http.Json;
using CamelRegistry.Api.Models;
using Xunit;

namespace CamelRegistry.Api.Tests;

public class CamelApiTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public CamelApiTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Post_Camel_Returns_Created()
    {
        var camel = new Camel
        {
            Name = "TesztTeve",
            Color = "Barna",
            HumpCount = 2
        };

        var response = await _client.PostAsJsonAsync("/camels", camel);

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);

        var createdCamel = await response.Content.ReadFromJsonAsync<Camel>();
        Assert.NotNull(createdCamel);
        Assert.True(createdCamel!.Id > 0);
    }
}
