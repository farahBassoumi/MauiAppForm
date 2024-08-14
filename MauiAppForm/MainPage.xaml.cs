using Microsoft.Maui.Controls;
using System.Text.Json;
using System.Text.RegularExpressions;
using MauiAppForm.Services;
namespace MauiAppForm
{
    public partial class MainPage : ContentPage
    {
        private readonly string _dataFilePath = @"C:\Users\FARAH\OneDrive\Bureau\data.txt";
        private readonly IPrintService _printService;

        public MainPage()
        {
            InitializeComponent();
            _printService = App.Services.GetService<IPrintService>();

            if (_printService == null)
            {
                // Handle the case when the service is not found
                throw new Exception("PrintService is not registered");
            }

           // _printService = App.Services.GetService<IPrintService>();
        }

        private void OnSubmitButtonClicked(object sender, EventArgs e)
        {
            if (!ValidateForm(out string errorMessage))
            {
                DisplayAlert("Validation Error", errorMessage, "OK");
                return;
            }

            SaveFormDataAsJson();
            DisplayAlert("Form Submitted", $"Name: {FullNameEntry.Text}\nDOB: {DateOfBirthPicker.Date.ToShortDateString()}\nGender: {GenderPicker.SelectedItem?.ToString()}\nMobile: {MobileNumberEntry.Text}\nEmail: {EmailEntry.Text}", "OK");
        }

        private bool ValidateForm(out string errorMessage)
        {
            errorMessage = string.Empty;

            if (string.IsNullOrWhiteSpace(FullNameEntry.Text))
            {
                errorMessage = "Full Name is required.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(GenderPicker.SelectedItem?.ToString()))

            {
                errorMessage = "Gender is required.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(MobileNumberEntry.Text))
            {
                errorMessage = "Mobile Number is required.";
                return false;
            }
            else if (!Regex.IsMatch(MobileNumberEntry.Text, @"^\d+$"))
            {
                errorMessage = "Mobile number must be numeric.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(EmailEntry.Text))
            {
                errorMessage = "Email is required.";
                return false;
            }
            else if (!Regex.IsMatch(EmailEntry.Text, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                errorMessage = "Email is not valid.";
                return false;
            }

            if (DateOfBirthPicker.Date > DateTime.Today)
            {
                errorMessage = "Date of Birth cannot be a future date.";
                return false;
            }

            return true;
        }

        private void SaveFormDataAsJson()
        {
            // Get the user's desktop path
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            // Define the file name based on the user's full name
            string fileName = FullNameEntry.Text.Replace(" ", "_") + ".txt";

            // Combine the desktop path with the file name to get the full file path
            string filePath = Path.Combine(desktopPath, fileName);

            var formData = new
            {
                FullName = FullNameEntry.Text,
                DateOfBirth = DateOfBirthPicker.Date.ToString("yyyy-MM-dd"),
                Gender = GenderPicker.SelectedItem?.ToString(),
                MobileNumber = MobileNumberEntry.Text,
                Email = EmailEntry.Text
            };

            // Serialize data to JSON
            var options = new JsonSerializerOptions { WriteIndented = true };
            var jsonString = JsonSerializer.Serialize(formData, options);


            File.AppendAllText(filePath, Environment.NewLine + jsonString);

            var printService = DependencyService.Get<IPrintService>();

           _printService.Print(jsonString);

        }

        private int count = 0;
        private const int MinValue = 1;
        private const int MaxValue = 10;

        private void OnMinusClicked(object sender, EventArgs e)
        {
            if (count > MinValue)
            {
                count--;
                UpdateCountLabel();
            }
        }

        private void OnPlusClicked(object sender, EventArgs e)
        {
            if (count < MaxValue)
            {
                count++;
                UpdateCountLabel();
            }
        }

        private void UpdateCountLabel()
        {
            countLabel.Text = count == 0 ? "1-10" : count.ToString();
            countLabel.TextColor = count == 0 ? Colors.Gray : Colors.Black;
        }

    }
}
