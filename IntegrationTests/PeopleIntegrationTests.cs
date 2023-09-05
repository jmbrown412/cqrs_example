using System.Text;
using cqrs_example;
using cqrs_example.Commands;
using Microsoft.AspNetCore.Mvc.Testing;
using Models;
using Newtonsoft.Json;

namespace IntegrationTests;

public class PeopleIntegrationTests : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly CustomWebApplicationFactory<Program> _factory;

    public PeopleIntegrationTests(CustomWebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task Test__AddPerson()
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var command = new CreatePersonCommand(
            "Jay",
            "Bee",
            Models.Gender.Male,
            new DateTime(1983, 04, 12),
            "USA",
            null,
            string.Empty
        );

        var objAsJson = JsonConvert.SerializeObject(command);
        var content = new StringContent(objAsJson, Encoding.UTF8, "application/json");
        var response = await client.PostAsync("people", content);

        // Assert
        response.EnsureSuccessStatusCode(); 
    }

    [Fact]
    public async Task Test__UpdatePerson()
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var command = new CreatePersonCommand(
            "Jay",
            "Bee",
            Models.Gender.Male,
            new DateTime(1983, 04, 12),
            "USA",
            null,
            string.Empty
        );

        var objAsJson = JsonConvert.SerializeObject(command);
        var content = new StringContent(objAsJson, Encoding.UTF8, "application/json");
        var response = await client.PostAsync("people", content);

        Console.WriteLine(response.Content);

        string personString = await response.Content.ReadAsStringAsync();
        Person? person = JsonConvert.DeserializeObject<Person?>(personString);

        // Assert
        response.EnsureSuccessStatusCode(); 

        // Act
        var updateCommand = new RecordBirthCommand(
            new DateTime(1983, 04, 12),
            "New birth location"
        );

        var updateJson = JsonConvert.SerializeObject(updateCommand);
        var updateContent = new StringContent(objAsJson, Encoding.UTF8, "application/json");
        var updateResponse = await client.PutAsync($"people/{person.Id}", content);

        // Assert
        response.EnsureSuccessStatusCode(); 
    }
}