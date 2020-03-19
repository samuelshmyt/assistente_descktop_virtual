using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Speech.Synthesis;
namespace shmytv1
{
     public class SPEAKER
    {
        private static SpeechSynthesizer sp = new SpeechSynthesizer();
        public static void Speak(string text)
        {
            // caso ele esteja falando...
            if (sp.State == SynthesizerState.Speaking)
                sp.SpeakAsyncCancelAll();
            sp.SpeakAsync(text);
        }

      }
}