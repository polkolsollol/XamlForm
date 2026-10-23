using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace formularzXaml
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void dpDataUrodzenia_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dpDataUrodzenia.SelectedDate.HasValue)
            {
                DateTime data = dpDataUrodzenia.SelectedDate.Value;

                if (data > DateTime.Today)
                {
                    dpDataUrodzenia.BorderBrush = new SolidColorBrush(Color.FromRgb(0xCC, 0x33, 0x33));
                    dpDataUrodzenia.ToolTip = "Data urodzenia nie może być w przyszłości.";
                }
                else if (DateTime.Today.Year - data.Year > 120)
                {
                    dpDataUrodzenia.BorderBrush = new SolidColorBrush(Color.FromRgb(0xCC, 0x33, 0x33));
                    dpDataUrodzenia.ToolTip = "Podana data jest zbyt odległa.";
                }
                else
                {
                    dpDataUrodzenia.BorderBrush = new SolidColorBrush(Color.FromRgb(0x2E, 0x8B, 0x57));
                    dpDataUrodzenia.ToolTip = null;
                }
            }
        }

        private void txtOpis_TextChanged(object sender, TextChangedEventArgs e)
        {
            int pozostalo = 300 - txtOpis.Text.Length;

            if (pozostalo < 30)
            {
                txtOpis.BorderBrush = new SolidColorBrush(Color.FromRgb(0xCC, 0x88, 0x00));
                txtOpis.ToolTip = $"Pozostało znaków: {pozostalo}";
            }
            else
            {
                txtOpis.BorderBrush = new SolidColorBrush(Color.FromRgb(0x7B, 0x9C, 0xDE));
                txtOpis.ToolTip = null;
            }
        }

        private void btnZatwierdz_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtImie.Text) ||
                string.IsNullOrWhiteSpace(txtNazwisko.Text) ||
                string.IsNullOrWhiteSpace(txtEmail.Text) ||
                pbHaslo.Password.Length == 0)
            {
                MessageBox.Show("Wypełnij wszystkie wymagane pola.", "Błąd",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!txtEmail.Text.Contains("@") || !txtEmail.Text.Contains("."))
            {
                MessageBox.Show("Podaj poprawny adres e-mail.", "Błąd",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!dpDataUrodzenia.SelectedDate.HasValue)
            {
                MessageBox.Show("Podaj datę urodzenia.", "Błąd",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            DateTime data = dpDataUrodzenia.SelectedDate.Value;

            if (data > DateTime.Today || DateTime.Today.Year - data.Year > 120)
            {
                MessageBox.Show("Podaj prawidłową datę urodzenia.", "Błąd",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            string plec = rbMezczyzna.IsChecked == true ? "Mężczyzna" :
                          rbKobieta.IsChecked == true   ? "Kobieta"   : "Nie wybrano";

            string kraj = (cbKraj.SelectedItem as ComboBoxItem)?.Content?.ToString() ?? "Nie wybrano";

            var sb = new StringBuilder();
            sb.AppendLine($"Imię:           {txtImie.Text}");
            sb.AppendLine($"Nazwisko:       {txtNazwisko.Text}");
            sb.AppendLine($"E-mail:         {txtEmail.Text}");
            sb.AppendLine($"Płeć:           {plec}");
            sb.AppendLine($"Kraj:           {kraj}");
            sb.AppendLine($"Data urodzenia: {data:dd.MM.yyyy}");
            if (!string.IsNullOrWhiteSpace(txtOpis.Text))
                sb.AppendLine($"O mnie:         {txtOpis.Text}");

            MessageBox.Show(sb.ToString(), "Zarejestrowano pomyślnie",
                MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void btnWyczysc_Click(object sender, RoutedEventArgs e)
        {
            txtImie.Clear();
            txtNazwisko.Clear();
            txtEmail.Clear();
            pbHaslo.Clear();
            rbMezczyzna.IsChecked   = false;
            rbKobieta.IsChecked     = false;
            cbKraj.SelectedIndex    = -1;
            dpDataUrodzenia.SelectedDate = null;
            dpDataUrodzenia.BorderBrush = new SolidColorBrush(Color.FromRgb(0x7B, 0x9C, 0xDE));
            txtOpis.Clear();
            txtOpis.BorderBrush = new SolidColorBrush(Color.FromRgb(0x7B, 0x9C, 0xDE));
        }
    }
}
