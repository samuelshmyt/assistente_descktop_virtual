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
        "Que horas são",
        "Me diga as horas",
        "Poderia me dizer que horas são",
        "Poderia me dizer que as horas",
        "as horas"
        };
        public static IList<string> WhatDateIS = new List<string>()
        {
           "data de hoje",
           "Qual a data de hoje",
           "Você sabe me dizr a data de hoje"
        };
        public static IList<string> ShmytStartListening = new List<string>()
        {
           "Shmyt",
           "Shmyt você esta ai",
           "oi"
        };
        public static IList<string> ShmytStopListening = new List<string>()
        {
           "Pare de ouvir",
           "Pare de me ouvir",
           "Fique quieto"
        };
    }
}