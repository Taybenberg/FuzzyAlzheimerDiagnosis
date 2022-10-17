using InferenceLibrary;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Documents;
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

            CognitiveAbilities.ItemsSource = new List<CognitiveAbility>
            {
                new CognitiveAbility("Selective attention", string.Empty),
                new CognitiveAbility("Sustained attention", string.Empty),
                new CognitiveAbility("Divided attention", string.Empty),
                new CognitiveAbility("Information processing speed", string.Empty),
                new CognitiveAbility("Short-term memory", string.Empty),
                new CognitiveAbility("Long-term memory", string.Empty),
                new CognitiveAbility("Verbal expression", string.Empty),
                new CognitiveAbility("Listening comprehension", string.Empty),
                new CognitiveAbility("Visual perception ", string.Empty),
                new CognitiveAbility("Perceptual organization", string.Empty),
                new CognitiveAbility("visoconstruction", string.Empty),
                new CognitiveAbility("Planning and graphic sequencing", string.Empty),
                new CognitiveAbility("Simple tasks of action and inhibition", string.Empty),
                new CognitiveAbility("Orientation", string.Empty),
                new CognitiveAbility("Motor, verbal and graphic sequence", string.Empty),
                new CognitiveAbility("Capacity of abstraction and reasoning", string.Empty),
                new CognitiveAbility("Instrumental and regular activities of daily living", string.Empty),
                new CognitiveAbility("Changes in behaviour and Social cognition", string.Empty),
            };
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

                if (ml < 0 || ml > 10
                || cs1 < 0 || cs1 > 10
                || cs2 < 0 || cs2 > 10)
                    throw new ArgumentException();

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
