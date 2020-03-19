using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Speech.Recognition;//houve


// para sintese é presiso o speechSDK.2

namespace shmytv1
{
    public partial class Form1 : Form
    {
        private SpeechRecognitionEngine engine; // variavel de voz 
        private bool isShymtListering = true;

        public Form1()
        {
            InitializeComponent();
        }
        private void audioLevel(object s, AudioLevelUpdatedEventArgs e)
        {
            this.progressBar1.Maximum = 100;
            this.progressBar1.Value = e.AudioLevel;
        }
        public void rej(object s, SpeechRecognizedEventArgs e)
        {
            this.label1.ForeColor = Color.Red;
        }
        // metodo que é chamado quando algo é reconhecido
        private void rec(object s, SpeechRecognizedEventArgs e)
        {
            //MessageBox.Show(e.Result.Text);
            string speech = e.Result.Text; // string reconecida
            float conf = e.Result.Confidence;

            if(conf > 0.35f)
            {
                
                this.label1.ForeColor = Color.Yellow;
                if(GrammarRules.ShmytStopListening.Any(x => x == speech))
                {
                    isShymtListering = false;
                }
                else if (GrammarRules.ShmytStartListening.Any(x => x == speech))
                {
                    isShymtListering = true;
                } else if (isShymtListering == true)
                {
                    switch (e.Result.Grammar.Name)
                    {
                        case "sys":
                            //se o speech == comparação ("que horas são?)
                            if (GrammarRules.WhatTimeIS.Any(x => x == speech))
                            {
                                Runner.WhatTimeIS();
                            }
                            else if (GrammarRules.WhatDateIS.Any(x => x == speech))
                            {
                                Runner.WhatDateIS();
                            }
                            break;
                    }
                }
               
            }
            // else
            //{
            // this.label1.ForeColor = Color.White;
            //}
           // this.label1.ForeColor = Color.White;
        }

        private void LoadSpeech()
        {
            try
            {
                engine = new SpeechRecognitionEngine();// instancia
                engine.SetInputToDefaultAudioDevice();// microfone
                string[] words = { "olá", "boa noite" };
                 // video 03
                Choices c_commandsOfSystem = new Choices();
                c_commandsOfSystem.Add(GrammarRules.WhatTimeIS.ToArray());
                c_commandsOfSystem.Add(GrammarRules.WhatDateIS.ToArray());
                // comando pare de ouvir e o comando pra voltar a ouvir ->> shmyt
                c_commandsOfSystem.Add(GrammarRules.ShmytStopListening.ToArray());
                c_commandsOfSystem.Add(GrammarRules.ShmytStartListening.ToArray());

                GrammarBuilder gb_comandOfSystem = new GrammarBuilder();// 4:22
                gb_comandOfSystem.Append(c_commandsOfSystem);

                Grammar g_comandsOfSystem = new Grammar(gb_comandOfSystem);
                g_comandsOfSystem.Name = "sys";

                engine.LoadGrammar(g_comandsOfSystem);//carrega gramatica na memoria


                // carregar gramatica substituido por choise 
               // engine.LoadGrammar(new Grammar(new GrammarBuilder(new Choices(words))));
                // Chamar o evento do reconhecimento comentado pelo video 03
                engine.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(rec);
                //barra de progresso
                engine.AudioLevelUpdated += new EventHandler<AudioLevelUpdatedEventArgs>(audioLevel);
               // engine.SpeechRecognitionRejected += new EventHandler<SpeechRecognitionRejectedEventArgs>(rej);
                // engine.SpeechRecognitionRejected += new EventHandler<SpeechRecognitionRejectedEventArgs>(rej);
                // inicia o reconhecimento
                engine.RecognizeAsync(RecognizeMode.Multiple);

                //SPEAKER.Speak("estou carregando as configurações");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu erro no LoadSpeech() " + ex.Message);
            }

        }
        private void Form1_Load(object sender, EventArgs e)
        {
            LoadSpeech();
            SPEAKER.Speak("já carreguei os arquivos, estou pronto");
        }

    }
}


