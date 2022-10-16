using InferenceLibrary;
using System;
using System.Text.RegularExpressions;
using System.Windows;
using Wpf.Ui.Appearance;
using Wpf.Ui.Controls;

namespace FuzzyAlzheimerUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : UiWindow
    {
        public MainWindow()
        {
            ModelCacheManager.InitializeModel();
            InitializeComponent();
        }

        private void ThemeButton_Click(object sender, RoutedEventArgs e)
        {
            Theme.Apply(Theme.GetAppTheme() is ThemeType.Dark ? ThemeType.Light : ThemeType.Dark);
        }

        private static readonly Regex _regex = new Regex("[^0-9,-]+"); //regex that matches disallowed text
        private void PreviewTextInputEvent(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            e.Handled = _regex.IsMatch(e.Text);
        }

        private void AcceptButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var ml = double.Parse(ML_Value.Text);
                var cs1 = double.Parse(CS1_Value.Text);
                var cs2 = double.Parse(CS2_Value.Text);

                var crispInput = new double[]
                {
                    ml,
                    cs1,
                    cs2
                };

                var result = MamdaniInference.Process(crispInput);

                Crisp_Value.Text = Math.Round(result.crispResult, 2).ToString();
                Word_Value.Text = GetSeverity(result.wordResult.term);
            }
            catch
            {
                var mb = new Wpf.Ui.Controls.MessageBox();
                mb.ButtonLeftName = "Гаразд";
                mb.ButtonRightName = "Скасувати";
                mb.Show("Помилка", "Сталася помилка. Перевірте коректність введених значень.");
            }
        }

        private string GetSeverity(Severity severity) =>
            severity switch
            {
                Severity.Normal => "Низький",
                Severity.Mild => "Легкий",
                Severity.Moderate => "Помірний",
                Severity.Severe => "Тяжкий",
                _ => throw new NotImplementedException()
            };
    }
}
