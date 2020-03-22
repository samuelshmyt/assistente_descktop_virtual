using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Microsoft.Speech.Recognition;//houve
using System.Speech.Synthesis;


// para sintese é presiso o speechSDK.2

namespace shmytv1
{
    public partial class Form1 : Form
    {
        private SpeechRecognitionEngine engine; // variavel de voz 
        private bool isShymtListering = true;
        private SelecVoz selectVoice = null;
        private SpeechSynthesizer synthesizer = new SpeechSynthesizer(); //sintetizador
        private Browser browser;
        private Video mediaPlay;

        public Form1()
        {
            InitializeComponent();
        }
        #region
        private void speakStarted(object s, SpeakStartedEventArgs e)
        {
            LBLShmyt.Text = "Sistema: -";
        }
        private void speakProgress(object s, SpeakProgressEventArgs e)
        {
            LBLShmyt.Text += e.Text + " ";
        }
        
        
        private void Speak(String text)
        {
            synthesizer.SpeakAsync(text);

        }
        private void SpeakRand(params string[] texts)
        {
            Random r = new Random();
            Speak(texts[r.Next(0, texts.Length)]);


        } 

        #endregion
        private void Normalwindow()
        {
            if (this.WindowState == FormWindowState.Maximized || this.WindowState == FormWindowState.Minimized)
            {
                this.WindowState = FormWindowState.Normal;
                Speak("Normalizando a janela");//, "como quiser", "tudo bem", "Vou fazer isto");
            }
            else
            {
                Speak("já está Normalizada");//, "a janela já está Normalizada", "já fiz isso");
            }
        }
        private void Maximawindow()
        {
            if (this.WindowState == FormWindowState.Normal || this.WindowState == FormWindowState.Minimized)
            {
                this.WindowState = FormWindowState.Maximized;
                Speak("Maximizando a janela");//, "como quiser", "tudo bem", "Vou fazer isto");
            }
            else
            {
                Speak("já está maximizado");//, "a janela já está maximizada", "já fiz isso");
            }
        }
        private void Minimizewindow()
        {
            if (this.WindowState == FormWindowState.Normal || this.WindowState == FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Minimized;
                Speak("minimizando a janela");//, "como quiser", "tudo bem", "Vou fazer isto");
            }
            else
            {
                Speak("já está minimizada");//, "a janela já está minimizada", "já fiz isso");;
            }
        }
        private void audioLevel(object s, AudioLevelUpdatedEventArgs e)
        {
            this.progressBar1.Maximum = 100;
            this.progressBar1.Value = e.AudioLevel;
        }
        public void rej(object s, SpeechRecognitionRejectedEventArgs e)
        {
            this.label1.ForeColor = Color.Red;
        }
        // metodo que é chamado quando algo é reconhecido
        private void rec(object s, SpeechRecognizedEventArgs e)
        {
            Runner r = new Runner();
            //MessageBox.Show(e.Result.Text);
            string speech = e.Result.Text; // string reconecida
            float conf = e.Result.Confidence;
            // criar log
            // ***********************************************                     
            this.label1.Text = ":>" + speech;
            if(conf > 0.35f)
            {
                
                this.label1.ForeColor = Color.Yellow;
                if(GrammarRules.ShmytStopListening.Any(x => x == speech))
                {
                    r.setresposta("mandou parar");
                    isShymtListering = false;
                    Speak("Sistema desligado");//, "Tá bem, desliquei", "Ok quando quiser é so chamar");
                }
                else if (GrammarRules.ShmytStartListening.Any(x => x == speech))
                {
                    r.setresposta("mandou continuar");
                    isShymtListering = true;
                    Speak("Sim mestre, o que deseja");//, "Pronta pra te atender", "Tava dormindo, diga o que mandas");
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
                            else if (GrammarRules.MinimizeWindow.Any(x => x == speech))
                            {
                                Minimizewindow();
                            }
                            else if (GrammarRules.MaximizaWindow.Any(x => x == speech))
                            {
                                Maximawindow();
                            }
                            else if (GrammarRules.NormalizaWindow.Any(x => x == speech))
                            {
                                Normalwindow();
                            }
                            else if (GrammarRules.ChangeVoice.Any(x => x == speech))
                            {
                                //if(selectVoice == null)
                                selectVoice = new SelecVoz();
                                selectVoice.Show();
                            }
                            else if (GrammarRules.OpenProgram.Any(x => x == speech))
                            {
                                switch (speech)
                                {
                                    case "Navegador":
                                        browser = new Browser();
                                        browser.Show();
                                        break;
                                    case "Video":
                                        mediaPlay = new Video();
                                        mediaPlay.Show();
                                        break;
                                }
                            }
                            else if (GrammarRules.MediaPlayComands.Any(x => x == speech))
                            {
                                switch (speech)
                                {
                                    case "Abrir arquivo":
                                        if (mediaPlay != null)
                                        {
                                            mediaPlay.OpenFile();
                                            Speak("Selecione um arquivo");
                                        } else
                                        {
                                            Speak("Media player não está aberto");
                                        }
                                        break;
                                }
                            }
                            break;
                    }
                }
               
            }

            //********************************************************************************************************************
            string date = DateTime.Now.Day.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Year.ToString();
            string log_filename = "log\\" + date + ".txt";

            StreamWriter sw = File.AppendText(log_filename);

            if (File.Exists(log_filename))
            {
                sw.WriteLine(speech + "=" + r.resposta());
            }
            else
            {
                sw.WriteLine(speech + "=" + r.resposta());

            }
            sw.Close();

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
               // string[] words = { "olá", "boa noite" };
                 // video 03
                Choices c_commandsOfSystem = new Choices();
                c_commandsOfSystem.Add(GrammarRules.WhatTimeIS.ToArray());
                c_commandsOfSystem.Add(GrammarRules.WhatDateIS.ToArray());
                // comando pare de ouvir e o comando pra voltar a ouvir ->> shmyt
                c_commandsOfSystem.Add(GrammarRules.ShmytStopListening.ToArray());
                c_commandsOfSystem.Add(GrammarRules.ShmytStartListening.ToArray());
                c_commandsOfSystem.Add(GrammarRules.MinimizeWindow.ToArray());
                c_commandsOfSystem.Add(GrammarRules.MaximizaWindow.ToArray());
                c_commandsOfSystem.Add(GrammarRules.NormalizaWindow.ToArray());
                c_commandsOfSystem.Add(GrammarRules.ChangeVoice.ToArray());
                c_commandsOfSystem.Add(GrammarRules.OpenProgram.ToArray());
                c_commandsOfSystem.Add(GrammarRules.MediaPlayComands.ToArray());

                GrammarBuilder gb_comandOfSystem = new GrammarBuilder();// 4:22
                gb_comandOfSystem.Append(c_commandsOfSystem);

                Grammar g_comandsOfSystem = new Grammar(gb_comandOfSystem);
                g_comandsOfSystem.Name = "sys";

                engine.LoadGrammar(g_comandsOfSystem);//carrega gramatica na memoria


                // carregar gramatica substituido por choise 
                // engine.LoadGrammar(new Grammar(new GrammarBuilder(new Choices(words))));
                // Chamar o evento do reconhecimento comentado pelo video 03
               
                #region SpeechRecognition Events 
                engine.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(rec);
                //barra de progresso
                engine.AudioLevelUpdated += new EventHandler<AudioLevelUpdatedEventArgs>(audioLevel);
                engine.SpeechRecognitionRejected += new EventHandler<SpeechRecognitionRejectedEventArgs>(rej);
                //engine.SpeechRecognitionRejected += new EventHandler<SpeechRecognitionRejectedEventArgs>(rej);
                #endregion 

                #region SpeechRecognition Events 
                synthesizer.SpeakStarted += new EventHandler<SpeakStartedEventArgs>(speakStarted);
                synthesizer.SpeakProgress += new EventHandler<SpeakProgressEventArgs>(speakProgress);
                #endregion 

                // inicia o reconhecimento
                engine.RecognizeAsync(RecognizeMode.Multiple);

                SPEAKER.Speak("estou carregando as configurações");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu erro no LoadSpeech() " + ex.Message);
            }

        }

        private void Synthesizer_SpeakStarted(object sender, SpeakStartedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            LoadSpeech();
            SPEAKER.Speak("Estou pronta");
        }

    }
}


