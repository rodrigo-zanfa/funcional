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
    public class CorrentistaControllerTests : IClassFixture<AppTestFixture>  // IntegrationTest
    {
        private readonly AppTestFixture _fixture;
        private readonly HttpClient _client;

        public CorrentistaControllerTests(AppTestFixture fixture)
        {
            _fixture = fixture;
            _client = fixture.CreateClient();
        }

        [Fact]
        public async Task GetCorrentistas_Found()
        {
            var response = await _client.GetAsync(ApiRoutes.Correntista.GetCorrentistas);

            var returned = await response.Content.ReadAsAsync<List<Correntista>>();

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            returned.Should().NotBeEmpty();
        }

        [Fact]
        public async Task GetCorrentistas_NotFound()
        {
            var response = await _client.GetAsync(ApiRoutes.Correntista.GetCorrentistas);

            var returned = await response.Content.ReadAsAsync<List<Correntista>>();

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);

            returned.Should().BeEmpty();
        }

        [Fact]
        public async Task GetCorrentistaCpf_Found()
        {
            var response = await _client.GetAsync(ApiRoutes.Correntista.GetCorrentistaCpf.Replace("{cpf}", "33713557802"));

            var returned = await response.Content.ReadAsAsync<Correntista>();

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            returned.Should().NotBeNull();
        }

        [Fact]
        public async Task GetCorrentistaCpf_NotFound()
        {
            var response = await _client.GetAsync(ApiRoutes.Correntista.GetCorrentistaCpf.Replace("{cpf}", "99999999999"));

            //var returned = await response.Content.ReadAsAsync<Correntista>();

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);

            //returned.Should().BeNull();
        }

        [Fact]
        public async Task PostCorrentista_CpfValido()
        {
            var json = new Correntista { Cpf = "27920152259", Nome = "Donizeti Caraça" };

            var response = await _client.PostAsJsonAsync(ApiRoutes.Correntista.PostCorrentista, json);

            var returned = await response.Content.ReadAsAsync<Correntista>();

            response.StatusCode.Should().Be(HttpStatusCode.Created);

            returned.Cpf.Should().Be("27920152259");
            returned.Nome.Should().Be("Donizeti Caraça");
        }

        [Fact]
        public async Task PostCorrentista_CpfDuplicado()
        {
            var json = new Correntista { Cpf = "32516143800", Nome = "Rodrigo Zanferrari" };

            var response = await _client.PostAsJsonAsync(ApiRoutes.Correntista.PostCorrentista, json);

            //var returned = await response.Content.ReadAsAsync<Correntista>();

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            //returned.Cpf.Should().Be("32516143800");
            //returned.Nome.Should().Be("Rodrigo Zanferrari");
        }

        [Fact]
        public async Task PutCorrentista_DadosValidos()
        {
            var json = new Correntista { Cpf = "27920152259", Nome = "Donizeti Caraça ALTERADO" };

            var response = await _client.PutAsJsonAsync(ApiRoutes.Correntista.PutCorrentista.Replace("{cpf}", "27920152259"), json);

            var returned = await response.Content.ReadAsAsync<Correntista>();

            response.StatusCode.Should().Be(HttpStatusCode.Created);

            returned.Cpf.Should().Be("27920152259");
            returned.Nome.Should().Be("Donizeti Caraça ALTERADO");
        }

        [Fact]
        public async Task PutCorrentista_DadosInvalidos()
        {
            var json = new Correntista { Cpf = "33333333333", Nome = "Donizeti Caraça ALTERADO" };

            var response = await _client.PutAsJsonAsync(ApiRoutes.Correntista.PutCorrentista.Replace("{cpf}", "27920152259"), json);

            //var returned = await response.Content.ReadAsAsync<Correntista>();

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            //returned.Cpf.Should().Be("27920152259");
            //returned.Nome.Should().Be("Donizeti Caraça ALTERADO");
        }
    }
}
