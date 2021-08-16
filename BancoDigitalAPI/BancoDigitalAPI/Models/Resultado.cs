﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BancoDigitalAPI.Models
{
    public class Resultado
    {
        //public string Acao { get; set; }

        public bool Sucesso
        {
            get { return Inconsistencias == null || Inconsistencias.Count == 0; }
        }

        public List<string> Inconsistencias { get; } = new List<string>();
    }
}
