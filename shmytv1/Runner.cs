using System;

namespace shmytv1
{
   public class Runner
    {
        //fala nenhum mal
        public static void WhatTimeIS()
        {
            SPEAKER.Speak(DateTime.Now.ToShortTimeString());
        }
        public static void WhatDateIS()
        {
            SPEAKER.Speak(DateTime.Now.ToShortDateString());
        }
    }
}