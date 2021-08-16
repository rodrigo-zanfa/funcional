using BancoDigitalAPI.Models;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BancoDigitalAPI.Tests
{
    public class ContaCorrenteControllerTests : IClassFixture<AppTestFixture>  // IntegrationTest
    {
        private readonly AppTestFixture _fixture;
        private readonly HttpClient _client;

        public ContaCorrenteControllerTests(AppTestFixture fixture)
        {
            _fixture = fixture;
            _client = fixture.CreateClient();
        }

        [Fact]
        public async Task GetContasCorrentes_Found()
        {
            var response = await _client.GetAsync(ApiRoutes.ContaCorrente.GetContasCorrentes);

            var returned = await response.Content.ReadAsAsync<List<ContaCorrente>>();

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            returned.Should().NotBeEmpty();
        }

        [Fact]
        public async Task GetContasCorrentes_NotFound()
        {
            var response = await _client.GetAsync(ApiRoutes.ContaCorrente.GetContasCorrentes);

            var returned = await response.Content.ReadAsAsync<List<ContaCorrente>>();

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);

            returned.Should().BeEmpty();
        }

        [Fact]
        public async Task GetContasCorrentesCpf_Found()
        {
            var response = await _client.GetAsync(ApiRoutes.ContaCorrente.GetContasCorrentesCpf.Replace("{cpf}", "32516143800"));

            var returned = await response.Content.ReadAsAsync<List<ContaCorrente>>();

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            returned.Should().NotBeNull();
        }

        [Fact]
        public async Task GetContasCorrentesCpf_NotFound()
        {
            var response = await _client.GetAsync(ApiRoutes.ContaCorrente.GetContasCorrentesCpf.Replace("{cpf}", "99999999999"));

            //var returned = await response.Content.ReadAsAsync<List<ContaCorrente>>();

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);

            //returned.Should().BeNull();
        }
    }
}
