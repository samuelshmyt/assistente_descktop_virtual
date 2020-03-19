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


        public Form1()
        {
            InitializeComponent();
        }
        private void audioLevel(object s, AudioLevelUpdatedEventArgs e)
        {
            this.progressBar1.Maximum = 100;
            this.progressBar1.Value = e.AudioLevel;
        }
        // metodo que é chamado quando algo é reconhecido
        private void rec(object s, SpeechRecognizedEventArgs e)
        {
             MessageBox.Show(e.Result.Text);
        }
        private void LoadSpeech()
        {
            try
            {
                engine = new SpeechRecognitionEngine();// instancia
                engine.SetInputToDefaultAudioDevice();// microfone
                string[] words = { "olá", "boa noite" };
                //Choices c_commandsOfSystem = new Choices();
                //c_commandsOfSystem.Add(GrammarRules.WhatTimeIS.ToArray());
                //GrammarBuilder gb_comandOfSystem = new GrammarBuilder();// 4:22
                //gb_comandOfSystem.Append(c_commandsOfSystem);

                // carregar gramatica substituido por choise 
                engine.LoadGrammar(new Grammar(new GrammarBuilder(new Choices(words))));
                // Chamar o evento do reconhecimento
                engine.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(rec);
                //barra de progresso
                engine.AudioLevelUpdated += new EventHandler<AudioLevelUpdatedEventArgs>(audioLevel);
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

        }

    }
}


