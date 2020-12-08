using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Scala.Puntenkaart.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Initialise();
        }
        private void BtnReset_Click(object sender, RoutedEventArgs e)
        {
            Initialise();
        }

        void Initialise()
        {
            txtLanguages.Text = "0";
            txtScience.Text = "0";
            txtMath.Text = "0";
            lblLanguages.Content = "";
            lblScience.Content = "";
            lblMath.Content = "";
            lblPointAchieved.Content = "";
            lblPerCentAchieved.Content = "";
            lblReview.Content = "";
            txtScience.Focus();
        }

        private void cmbScience_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbScience.IsLoaded)
            {
                CalculateScores();
            }
        }

        private void cmbMath_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbMath.IsLoaded)
            {
                CalculateScores();
            }
        }

        private void cmbLanguages_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbLanguages.IsLoaded)
            {
                CalculateScores();
            }
        }



        private void txtScience_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtScience.IsLoaded)
            {
                CalculateScores();
            }
        }

        private void txtScience_GotFocus(object sender, RoutedEventArgs e)
        {
            txtScience.SelectAll();
        }

        private void txtMath_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtMath.IsLoaded)
            {
                CalculateScores();
            }
        }

        private void txtMath_GotFocus(object sender, RoutedEventArgs e)
        {
            txtMath.SelectAll();
        }

        private void txtLanguages_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtLanguages.IsLoaded)
            {
                CalculateScores();
            }
        }

        private void txtLanguages_GotFocus(object sender, RoutedEventArgs e)
        {
            txtLanguages.SelectAll();
        }
        void CalculateScores()
        {
            if (cmbScience.SelectedItem == null) return;
            if (cmbMath.SelectedItem == null) return;
            if (cmbLanguages.SelectedItem == null) return;

            int scienceBase = 0;
            int mathBase = 0;
            int languagesBase = 0;
            if (int.TryParse(txtScience.Text, out int waarde1))
            {
                scienceBase = waarde1;
            }
            else
            {
                txtScience.Text = "0";
                txtScience.SelectAll();
            }
            if (scienceBase > 10)
            {
                scienceBase = 10;
                txtScience.Text = "10";
                txtScience.SelectAll();
            }
            if (int.TryParse(txtMath.Text, out int waarde2))
            {
                mathBase = waarde2;
            }
            else
            {
                txtMath.Text = "0";
                txtMath.SelectAll();
            }
            if (mathBase > 10)
            {
                mathBase = 10;
                txtMath.Text = "10";
                txtMath.SelectAll();
            }
            if (int.TryParse(txtLanguages.Text, out int waarde3))
            {
                languagesBase = waarde3;
            }
            else
            {
                txtLanguages.Text = "0";
                txtLanguages.SelectAll();
            }
            if (languagesBase > 10)
            {
                languagesBase = 10;
                txtLanguages.Text = "10";
                txtLanguages.SelectAll();
            }

            bool scienceSuccess = true;
            bool mathSuccess = true;
            bool languagesSuccess = true;
            if (scienceBase < 5)
                scienceSuccess = false;
            if (mathBase < 5)
                mathSuccess = false;
            if (languagesBase < 5)
                languagesSuccess = false;
            bool onvoldoendes = !(scienceSuccess && mathSuccess && languagesSuccess);

            byte weightScience = 0;
            byte weightMath = 0;
            byte weightLanguages = 0;
            //if (byte.TryParse(cmbWetenschappen.SelectedValue.ToString(), out byte bwaarde1))
            //    gewichtWetenschappen = bwaarde1;

            weightScience = byte.Parse(cmbScience.SelectedValue.ToString());

            if (byte.TryParse(cmbMath.SelectedValue.ToString(), out byte bwaarde2))
                weightMath = bwaarde2;
            if (byte.TryParse(cmbLanguages.SelectedValue.ToString(), out byte bwaarde3))
                weightLanguages = bwaarde3;

            int scienceAfterWeighing = scienceBase * weightScience;
            int mathAfterWeighing = mathBase * weightMath;
            int languagesAfterWeighing = languagesBase * weightLanguages;

            int maxScience = 10 * weightScience;
            int maxMath = 10 * weightMath;
            int maxLanguages = 10 * weightLanguages;
            int maxTotaal = maxScience + maxMath + maxLanguages;

            int totalAfterWeighing = scienceAfterWeighing + mathAfterWeighing + languagesAfterWeighing;
            lblScience.Content = scienceAfterWeighing.ToString();
            lblMath.Content = mathAfterWeighing.ToString();
            lblLanguages.Content = languagesAfterWeighing.ToString();
            lblPointAchieved.Content = totalAfterWeighing.ToString() + "/" + maxTotaal.ToString();

            double percent = (double)totalAfterWeighing / maxTotaal * 100;
            lblPerCentAchieved.Content = percent.ToString("0.00") + " %";

            if (percent < 50)
            {
                lblReview.Content = "Niet geslaagd en niet toegelaten tot de herexamens";
            }
            else if (percent < 68 && onvoldoendes)
            {
                lblReview.Content = "Geslaagd op voldoende wijze maar met herexamens";
            }
            else if (percent < 68 && !onvoldoendes)
            {
                lblReview.Content = "Geslaagd op voldoende wijze";
            }
            else if (percent < 77)
            {
                lblReview.Content = "Geslaagd om onderscheiding";
            }
            else if (percent < 85)
            {
                lblReview.Content = "Geslaagd met grote onderscheiding";
            }
            else if (percent < 90)
            {
                lblReview.Content = "Geslaagd met grootste onderscheiding";
            }
            else
            {
                lblReview.Content = "Geslaagd met grootste onderscheiding en de gelukwensen van de examencommissie";
            }
        }
    }
}
