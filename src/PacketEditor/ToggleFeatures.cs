using System;
using System.Windows.Forms;

namespace PacketEditor
{
    public partial class ToggleFeatures : Form
    {
        public ToggleFeatures()
        {
            InitializeComponent();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            Geldwäsche.MissionToggled = !Geldwäsche.MissionToggled;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            Geldwäsche.WorkToggled = !Geldwäsche.WorkToggled;
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            Geldwäsche.DuelToggled = !Geldwäsche.DuelToggled;
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            Geldwäsche.WantedToggled = !Geldwäsche.WantedToggled;
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            Geldwäsche.GangwarsToggled = !Geldwäsche.GangwarsToggled;
        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            Geldwäsche.SurvivalToggled = !Geldwäsche.SurvivalToggled;
        }
    }
}
