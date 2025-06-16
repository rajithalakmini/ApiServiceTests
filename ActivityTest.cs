using System.Threading.Tasks;
using RestSharp;
using Xunit;
namespace ApiServiceTests;

public class ActivityTest
{
    private readonly RestClient _client;

    public ActivityTest()
    {
        _client = new RestClient("https://fakerestapi.azurewebsites.net/api/v1"); // Replace with your Swagger base URL
    }

    [Fact]
    public async Task GetActivityById_ShouldReturnSuccess()
    {
        // Arrange
        var request = new RestRequest("Activities", Method.Get);

        // Act
        var response = await _client.ExecuteAsync(request);

        // Assert
        Assert.True(response.IsSuccessful, "The GET request was successful.");
        Assert.Equal(200, (int)response.StatusCode); // Validate status code
        Assert.Contains("id", response.Content); // Validate response content
    }
    [Fact]
    public async Task CreateActivity_ShouldReturnSuccess()
    {
        // Arrange
        var request = new RestRequest("/Activities", Method.Post);
        request.AddJsonBody(new
        {
            id = 32,
            title = "Test Activity 1",
            dueDate = "2025-06-16T08:02:56.947Z",
            completed = false
        });

        // Act
        var response = await _client.ExecuteAsync(request);
        //Debug output
        System.Console.WriteLine($"Status: {response.StatusCode}, Content: {response.Content}");

        // Assert
        Assert.True(response.IsSuccessful, $"The POST request was successful.Status: {response.StatusCode}, Content: {response.Content}");
        Assert.Equal(200, (int)response.StatusCode); // Validate status code
        Assert.Contains("32", response.Content); // Validate response content
    }

    [Fact]
    public async Task GetActivityByID_ShouldReturnSuccess()
    {
        // Arrange
        var request = new RestRequest("/Activities/1", Method.Get);

        // Act
        var response = await _client.ExecuteAsync(request);
        //Debug output
        System.Console.WriteLine($"Status: {response.StatusCode}, Content: {response.Content}");

        // Asserts
        Assert.True(response.IsSuccessful, $"The GET request was successful.Status: {response.StatusCode}, Content: {response.Content}");
        Assert.Equal(200, (int)response.StatusCode); // Validate status code
        Assert.Contains("id", response.Content); // Validate response content

    }
    [Fact]
    public async Task GetActivityWrongByID_ShouldReturnSuccess()
    {
        // Arrange
        var request = new RestRequest("/Activities/40", Method.Get);

        // Act
        var response = await _client.ExecuteAsync(request);
        //Debug output
        System.Console.WriteLine($"Status: {response.StatusCode}, Content: {response.Content}");

        // Asserts
        Assert.False(response.IsSuccessful, $"Expected failure for invalid Activity ID. Status: {response.StatusCode}, Content: {response.Content}");
        Assert.Equal(404, (int)response.StatusCode); // Validate status code
        Assert.Contains("40", response.Content, StringComparison.OrdinalIgnoreCase); // Validate response content

    }
    [Fact]
    public async Task UpdateActivity_ShouldReturnSuccess()
    {
        // Arrange
        var request = new RestRequest("/Activities/1", Method.Put);
        request.AddJsonBody(new
        {
            id = 1,
            title = "Updated Activity",
            dueDate = "2025-06-16T08:02:56.947Z",
            completed = true
        });

        // Act
        var response = await _client.ExecuteAsync(request);
        //Debug output
        System.Console.WriteLine($"Status: {response.StatusCode}, Content: {response.Content}");

        // Assert
        Assert.True(response.IsSuccessful, $"The PUT request was successful.Status: {response.StatusCode}, Content: {response.Content}");
        Assert.Equal(200, (int)response.StatusCode); // Validate status code
        Assert.Contains("Updated Activity", response.Content); // Validate response content
    }
    [Fact]
    public async Task DeleteActivity_ShouldReturnSuccess()
    {
        // Arrange
        var request = new RestRequest("/Activities/1", Method.Delete);

        // Act
        var response = await _client.ExecuteAsync(request);
        //Debug output
        System.Console.WriteLine($"Status: {response.StatusCode}, Content: {response.Content}");

        // Assert
        Assert.True(response.IsSuccessful, $"The DELETE request was successful.Status: {response.StatusCode}, Content: {response.Content}");
        Assert.Equal(200, (int)response.StatusCode); // Validate status code
        //Assert.Contains("1", response.Content); // Validate response content
    }
}