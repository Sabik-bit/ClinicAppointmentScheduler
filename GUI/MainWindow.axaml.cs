using Avalonia.Controls;
using Avalonia.Interactivity;

namespace GUI;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    // Open Patient Management
    private void PatientsButton_Click(object? sender, RoutedEventArgs e)
    {
        DashboardPanel.IsVisible = false;
        PatientsPanel.IsVisible = true;

        Title = "Clinic Appointment Scheduler - Patients";
    }

    // Return to Dashboard
    private void DashboardButton_Click(object? sender, RoutedEventArgs e)
    {
        PatientsPanel.IsVisible = false;
        DashboardPanel.IsVisible = true;

        Title = "Clinic Appointment Scheduler";
    }

    // Add a new patient
    private void AddPatientButton_Click(object? sender, RoutedEventArgs e)
    {
        string patientId = PatientIdTextBox.Text?.Trim() ?? "";
        string patientName = PatientNameTextBox.Text?.Trim() ?? "";
        string patientPhone = PatientPhoneTextBox.Text?.Trim() ?? "";
        string patientEmail = PatientEmailTextBox.Text?.Trim() ?? "";

        // Check empty fields
        if (string.IsNullOrWhiteSpace(patientId) ||
            string.IsNullOrWhiteSpace(patientName) ||
            string.IsNullOrWhiteSpace(patientPhone) ||
            string.IsNullOrWhiteSpace(patientEmail))
        {
            PatientStatusText.Text = "Please complete all patient fields.";
            PatientStatusText.Foreground =
                Avalonia.Media.Brushes.Red;

            return;
        }

        // Check Patient ID
        if (!int.TryParse(patientId, out int id))
        {
            PatientStatusText.Text =
                "Patient ID must be a number.";

            PatientStatusText.Foreground =
                Avalonia.Media.Brushes.Red;

            return;
        }

        // Hide default message
        NoPatientsText.IsVisible = false;

        // Create patient display
        Border patientCard = new Border
        {
            Background = Avalonia.Media.Brushes.White,
            CornerRadius = new Avalonia.CornerRadius(6),
            Padding = new Avalonia.Thickness(12),
            Margin = new Avalonia.Thickness(0, 3)
        };

        StackPanel patientInformation = new StackPanel
        {
            Spacing = 4
        };

        TextBlock nameText = new TextBlock
        {
            Text = $"{patientName} (ID: {id})",
            FontWeight = Avalonia.Media.FontWeight.Bold,
            Foreground = Avalonia.Media.Brushes.DarkSlateGray
        };

        TextBlock phoneText = new TextBlock
        {
            Text = $"Phone: {patientPhone}"
        };

        TextBlock emailText = new TextBlock
        {
            Text = $"Email: {patientEmail}"
        };

        patientInformation.Children.Add(nameText);
        patientInformation.Children.Add(phoneText);
        patientInformation.Children.Add(emailText);

        patientCard.Child = patientInformation;

        PatientListPanel.Children.Add(patientCard);

        // Success message
        PatientStatusText.Text =
            $"Patient '{patientName}' added successfully.";

        PatientStatusText.Foreground =
            Avalonia.Media.Brushes.Green;

        // Clear fields
        PatientIdTextBox.Text = "";
        PatientNameTextBox.Text = "";
        PatientPhoneTextBox.Text = "";
        PatientEmailTextBox.Text = "";
    }
}