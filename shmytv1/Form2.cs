using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech.Synthesis;

namespace shmytv1
{
    public partial class SelecVoz : Form
    {
        private SpeechSynthesizer sp = new SpeechSynthesizer();
        public SelecVoz()
        {
            InitializeComponent();
            comboBox1.Items.Clear();
            foreach (InstalledVoice voice in sp.GetInstalledVoices())
            {
                comboBox1.Items.Add(voice.VoiceInfo.Name);
            }
            comboBox1.SelectedIndex = 0;
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void Selecvoz_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SPEAKER.SetVoice(comboBox1.SelectedItem.ToString());
            SPEAKER.Speak("a voz foi alterada", "feito", "como quizer");
        }
    }
}
