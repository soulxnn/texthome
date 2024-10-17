using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Microsoft.Win32;

namespace texthome
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            string text = TextInput.Text;
            if (string.IsNullOrWhiteSpace(text))
            {
                FeedbackText.Text = "Пожалуйста, введите текст!";
                return;
            }

            string selectedFormat = (FormatComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();
            if (string.IsNullOrEmpty(selectedFormat))
            {
                FeedbackText.Text = "Выберите формат файла!";
                return;
            }

            // Диалог для выбора места сохранения файла
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = selectedFormat == "txt" ? "Text files (*.txt)|*.txt" : "Word documents (*.doc)|*.doc",
                DefaultExt = selectedFormat,
                AddExtension = true
            };

            bool? result = saveFileDialog.ShowDialog();
            if (result == true)
            {
                try
                {
                    string filePath = saveFileDialog.FileName;

                    // Сохранение файла в выбранном формате
                    File.WriteAllText(filePath, text);
                    FeedbackText.Text = $"Файл сохранен: {filePath}";
                    FeedbackText.Foreground = new SolidColorBrush(Colors.Green);
                }
                catch (Exception ex)
                {
                    FeedbackText.Text = $"Ошибка сохранения файла: {ex.Message}";
                    FeedbackText.Foreground = new SolidColorBrush(Colors.Red);
                }
            }
        }
    }
}