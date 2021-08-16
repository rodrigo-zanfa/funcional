using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BancoDigitalAPI
{
    public static class ApiRoutes
    {
        private static readonly string url = "https://localhost:44381/api/";

        public static class Correntista
        {
            private static readonly string controllerUrl = string.Concat(url, "Correntista");

            public static readonly string GetCorrentistas = controllerUrl;

            public static readonly string GetCorrentistaCpf = string.Concat(controllerUrl, "/{cpf}");

            public static readonly string PostCorrentista = controllerUrl;

            public static readonly string PutCorrentista = string.Concat(controllerUrl, "/{cpf}");
        }

        public static class ContaCorrente
        {
            private static readonly string controllerUrl = string.Concat(url, "ContaCorrente");

            public static readonly string GetContasCorrentes = controllerUrl;

            public static readonly string GetContasCorrentesCpf = string.Concat(controllerUrl, "/cpf/{cpf}");
        }
    }
}
