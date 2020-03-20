using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace shmytv1
{
    public class GrammarRules
    {
        public static IList<string> WhatTimeIS = new List<string>()
        { 
           "Sistema hora"
        };
        public static IList<string> WhatDateIS = new List<string>()
        {
           "Sistema data"
        };
        public static IList<string> ShmytStartListening = new List<string>()
        {
           "Sistema liga"
        };
        public static IList<string> ShmytStopListening = new List<string>()
        {
           "Sistema desliga"
        };
        // comandos
        public static IList<string> MinimizeWindow = new List<string>()
        {
           "Tamanho Minimo"
        };
        public static IList<string> MaximizaWindow = new List<string>()
        {
           "Tamanho Maximo"
        };
        public static IList<string> NormalizaWindow = new List<string>()
        {
           "Tamanho normal"
        };
        public static IList<string> ChangeVoice = new List<string>()
        {
           "Alterar voz"
        };
    }
}