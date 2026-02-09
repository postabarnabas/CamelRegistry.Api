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
    [Fact]
    public async Task Get_Camels_Returns_Ok()
    {
        var response = await _client.GetAsync("/camels");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var camels = await response.Content.ReadFromJsonAsync<List<Camel>>();
        Assert.NotNull(camels);
    }
    [Fact]
    public async Task Post_Camel_With_Invalid_HumpCount_Returns_BadRequest()
    {
        var camel = new Camel
        {
            Name = "HibásTeve",
            Color = "Szürke",
            HumpCount = 3 
        };

        var response = await _client.PostAsJsonAsync("/camels", camel);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
}
