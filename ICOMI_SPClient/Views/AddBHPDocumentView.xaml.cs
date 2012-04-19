using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Forms;
using ICOMI_SPService;
using ICOMI_DOMAIN;
using ICOMI_CONTENT;

namespace ICOMI_SPClient.Views
{
    /// <summary>
    /// Logique d'interaction pour AddBHPDocumentView.xaml
    /// </summary>
    public partial class AddBHPDocumentView
    {
        public event RoutedEventHandler ReturnHome; 
        public AddBHPDocumentView()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Configure open file dialog box
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.DefaultExt = ".pdf"; // Default file extension
            dlg.Filter = "PDF (.pdf)|*.pdf"; // Filter files by extension

            // Show open file dialog box
            Nullable<bool> result = dlg.ShowDialog();

            // Process open file dialog box results
            if (result == true)
            {
                // Open document
                FileTextBox.Text = dlg.FileName;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                this.ProgressInsert.Visibility = Visibility.Visible;
                var gSP = new GestionSharepoint();
                string fileTest = this.FileTextBox.Text;
                string LibraryName = "ICOMI_BPH";
                var dmDC = new DocumentMetadataDataContract();
                dmDC.Auteur = "Alexandre Equoy";
                dmDC.Commentaires = this.Commentaires.Text;
                dmDC.CreateDate = DateTime.Now;
                dmDC.DocumentVersion = 1;
                dmDC.Producteur = this.Producteur.Text;
                dmDC.RubriqueDocument = this.Rubrique.Text;
                dmDC.SousTypeDocument = this.SousType.Text;
                dmDC.TypeDocument = this.Type.Text;
                dmDC.RubriqueDocumentCode = RefObjectSPService.GetRubriques().Where(r => r.Libelle.Equals(this.Rubrique.Text)).First().Code;
                dmDC.SousTypeDocumentCode = RefObjectSPService.GetSousTypeDocument().Where(r => r.Libelle.Equals(this.SousType.Text)).First().Code;
                dmDC.TypeDocumentCode = RefObjectSPService.GetTypeDocument().Where(r => r.Libelle.Equals(this.Type.Text)).First().Code; ;
                dmDC.Titre = this.Titre.Text;

                var result = gSP.AddFile(LibraryName, dmDC, System.IO.File.ReadAllBytes(fileTest), ".PDF");
                if (result != null)
                    if (ReturnHome != null)
                        ReturnHome(sender, e);
                this.ProgressInsert.Visibility = Visibility.Hidden;
            }
            catch (Exception ex)
            {

            }
            finally
            {
                this.Commentaires.Text = string.Empty;
                this.Producteur.Text = string.Empty;
                this.Titre.Text = string.Empty;
            }

        }
    }
}
