using Castle.Core.Logging;
using JsonPlaceholderAPI.Models;
using JsonPlaceholderAPI.Services;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.Protected;
using System.Net;
using System.Text.Json;

namespace JsonPlaceholderService.Tests
{
    public class JsonPlaceholderServiceTests
    {
        private readonly Mock<HttpMessageHandler> _httpMessageHandlerMock;
        private readonly HttpClient _httpClient;
        private readonly Mock<ILogger<JsonPlaceholderAPI.Services.JsonPlaceholderService>> _loggerMock;
        private readonly JsonPlaceholderAPI.Services.JsonPlaceholderService _service;


        public JsonPlaceholderServiceTests()
        {
            _httpMessageHandlerMock = new Mock<HttpMessageHandler>();

            _httpClient = new HttpClient(_httpMessageHandlerMock.Object)
            {
                BaseAddress = new System.Uri("https://jsonplaceholder.typicode.com/")
            };

            _loggerMock = new Mock<ILogger<JsonPlaceholderAPI.Services.JsonPlaceholderService>>();

            _service = new JsonPlaceholderAPI.Services.JsonPlaceholderService(new HttpClientFactoryMock(_httpClient), _loggerMock.Object);
        }

        [Fact]
        public async Task GetPostsAsyncRetornarListaComOk()
        {
            var fakePosts = new List<Post>
            {
                new Post { Id = 1, Title = "Post 1", Body = "Conteúdo do post 1" },
                new Post { Id = 1, Title = "Post 2", Body = "Conteúdo do post 2" }
            };

            var jsonResponse = JsonSerializer.Serialize(fakePosts);

            _httpMessageHandlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(jsonResponse)
                });

            var result = await _service.GetPostsAsync();

            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Equal("Post 1", result[0].Title);
        }

        [Fact]
        public async Task GetPostsAsyncRetornarListaVaziaComErro()
        {
            _httpMessageHandlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.InternalServerError
                });

            var result = await _service.GetPostsAsync();

            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetPostsAsyncRegistrarErroNoLogException()
        {
            _httpMessageHandlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ThrowsAsync(new HttpRequestException("Erro simulado"));

            var result = await _service.GetPostsAsync();

            Assert.NotNull(result);
            Assert.Empty(result);
            _loggerMock.Verify(
                x => x.Log(
                    LogLevel.Error,
                    It.IsAny<EventId>(),
                    It.IsAny<It.IsAnyType>(),
                    It.IsAny<Exception>(),
                    (Func<It.IsAnyType, Exception?, string>)It.IsAny<object>()),
                Times.Once
            );
        }
    }

    public class HttpClientFactoryMock : IHttpClientFactory
    {
        private readonly HttpClient _client;

        public HttpClientFactoryMock(HttpClient client)
        {
            _client = client;
        }

        public HttpClient CreateClient(string name)
        {
            return _client;
        }
    }
}
