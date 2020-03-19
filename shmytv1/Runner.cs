using System;

namespace shmytv1
{
   
   public class Runner
    {
       private static string _retorno = "";
        //fala nenhum mal
        public static void WhatTimeIS()
        {
            _retorno = DateTime.Now.ToShortTimeString();
            SPEAKER.Speak(_retorno);
        }
        public static void WhatDateIS()
        {
           _retorno = DateTime.Now.ToShortDateString();
           SPEAKER.Speak(_retorno);
        }
        public string resposta()
        {
            return _retorno;
        }
        public void setresposta(string r)
        {
            _retorno = r;
        }
    }
}