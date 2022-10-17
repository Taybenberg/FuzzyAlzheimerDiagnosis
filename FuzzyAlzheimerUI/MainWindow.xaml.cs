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

            CognitiveAbilities.ItemsSource = new List<string>
            {
                "Короткочасна пам'ять",
                "Довготривала пам'ять",
                string.Empty,
                "Вибіркова увага",
                "Постійна увага",
                "Поділена увага",
                "Швидкість обробки інформації",
                "Словесне вираження",
                "Розуміння на слух",
                "Візуальне сприйняття",
                "Перцептивна організація",
                "Візоконструкція",
                "Планування та графічна послідовність",
                "Прості задачі дії і гальмування",
                "Орієнтація",
                "Рухова, словесно-графічна послідовність",
                "Здатність до абстракції та міркування",
                "Інструментальна та регулярна діяльність повсякденного життя",
                "Зміни в поведінці та соціальному пізнанні"
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
