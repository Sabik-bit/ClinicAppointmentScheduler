using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;
using System;

namespace GUI;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        ShowDashboard();
    }

    // =========================
    // NAVIGATION
    // =========================

    private void HideAllPanels()
    {
        DashboardPanel.IsVisible = false;
        PatientsPanel.IsVisible = false;
        DoctorsPanel.IsVisible = false;
        AppointmentsPanel.IsVisible = false;
    }

    private void ShowDashboard()
    {
        HideAllPanels();

        DashboardPanel.IsVisible = true;
        Title = "Clinic Appointment Scheduler";
    }

    private void DashboardButton_Click(object? sender, RoutedEventArgs e)
    {
        ShowDashboard();
    }

    private void PatientsButton_Click(object? sender, RoutedEventArgs e)
    {
        HideAllPanels();

        PatientsPanel.IsVisible = true;
        Title = "Clinic Appointment Scheduler - Patients";
    }

    private void DoctorsButton_Click(object? sender, RoutedEventArgs e)
    {
        HideAllPanels();

        DoctorsPanel.IsVisible = true;
        Title = "Clinic Appointment Scheduler - Doctors";
    }

    private void AppointmentsButton_Click(object? sender, RoutedEventArgs e)
    {
        HideAllPanels();

        AppointmentsPanel.IsVisible = true;
        Title = "Clinic Appointment Scheduler - Appointments";
    }

    // =========================
    // ADD PATIENT
    // =========================

    private void AddPatientButton_Click(object? sender, RoutedEventArgs e)
    {
        string patientId = PatientIdTextBox.Text ?? "";
        string patientName = PatientNameTextBox.Text ?? "";
        string patientPhone = PatientPhoneTextBox.Text ?? "";
        string patientEmail = PatientEmailTextBox.Text ?? "";

        if (patientId == "" || patientName == "")
        {
            PatientStatusText.Text =
                "Please enter Patient ID and Full Name.";

            PatientStatusText.Foreground = Brushes.Red;
            return;
        }

        NoPatientsText.IsVisible = false;

        StackPanel patientInformation = new StackPanel
        {
            Spacing = 4
        };

        patientInformation.Children.Add(new TextBlock
        {
            Text = patientName + " (ID: " + patientId + ")",
            FontWeight = FontWeight.Bold,
            Foreground = Brushes.Black
        });

        patientInformation.Children.Add(new TextBlock
        {
            Text = "Phone: " + patientPhone,
            Foreground = Brushes.Black
        });

        patientInformation.Children.Add(new TextBlock
        {
            Text = "Email: " + patientEmail,
            Foreground = Brushes.Black
        });

        Border patientCard = new Border
        {
            Background = Brushes.White,
            CornerRadius = new CornerRadius(6),
            Padding = new Thickness(12),
            Margin = new Thickness(0, 4),
            Child = patientInformation
        };

        PatientListPanel.Children.Add(patientCard);

        PatientStatusText.Text =
            "Patient '" + patientName + "' added successfully.";

        PatientStatusText.Foreground = Brushes.Green;

        PatientIdTextBox.Text = "";
        PatientNameTextBox.Text = "";
        PatientPhoneTextBox.Text = "";
        PatientEmailTextBox.Text = "";
    }

    // =========================
    // ADD DOCTOR
    // =========================

    private void AddDoctorButton_Click(object? sender, RoutedEventArgs e)
    {
        string doctorId = DoctorIdTextBox.Text ?? "";
        string doctorName = DoctorNameTextBox.Text ?? "";
        string speciality = DoctorSpecialityTextBox.Text ?? "";

        if (doctorId == "" || doctorName == "")
        {
            DoctorStatusText.Text =
                "Please enter Doctor ID and Doctor Name.";

            DoctorStatusText.Foreground = Brushes.Red;
            return;
        }

        NoDoctorsText.IsVisible = false;

        StackPanel doctorInformation = new StackPanel
        {
            Spacing = 4
        };

        doctorInformation.Children.Add(new TextBlock
        {
            Text = doctorName + " (ID: " + doctorId + ")",
            FontWeight = FontWeight.Bold,
            Foreground = Brushes.Black
        });

        doctorInformation.Children.Add(new TextBlock
        {
            Text = "Speciality: " + speciality,
            Foreground = Brushes.Black
        });

        Border doctorCard = new Border
        {
            Background = Brushes.White,
            CornerRadius = new CornerRadius(6),
            Padding = new Thickness(12),
            Margin = new Thickness(0, 4),
            Child = doctorInformation
        };

        DoctorListPanel.Children.Add(doctorCard);

        DoctorStatusText.Text =
            "Doctor '" + doctorName + "' added successfully.";

        DoctorStatusText.Foreground = Brushes.Green;

        DoctorIdTextBox.Text = "";
        DoctorNameTextBox.Text = "";
        DoctorSpecialityTextBox.Text = "";
    }

    // =========================
    // ADD APPOINTMENT
    // =========================

    private void AddAppointmentButton_Click(object? sender, RoutedEventArgs e)
    {
        string appointmentId =
            AppointmentIdTextBox.Text ?? "";

        string patientName =
            AppointmentPatientTextBox.Text ?? "";

        string doctorName =
            AppointmentDoctorTextBox.Text ?? "";

        string appointmentDate =
            AppointmentDateTextBox.Text ?? "";

        // Check if all fields are completed
        if (appointmentId == "" ||
            patientName == "" ||
            doctorName == "" ||
            appointmentDate == "")
        {
            AppointmentStatusText.Text =
                "Please complete all appointment fields.";

            AppointmentStatusText.Foreground = Brushes.Red;
            return;
        }

        // Check if the date entered is valid
        DateTime validDate;

        if (!DateTime.TryParse(appointmentDate, out validDate))
        {
            AppointmentStatusText.Text =
                "Please enter a valid appointment date.";

            AppointmentStatusText.Foreground = Brushes.Red;
            return;
        }

        NoAppointmentsText.IsVisible = false;

        StackPanel appointmentInformation = new StackPanel
        {
            Spacing = 4
        };

        appointmentInformation.Children.Add(new TextBlock
        {
            Text = "Appointment ID: " + appointmentId,
            FontWeight = FontWeight.Bold,
            Foreground = Brushes.Black
        });

        appointmentInformation.Children.Add(new TextBlock
        {
            Text = "Patient: " + patientName,
            Foreground = Brushes.Black
        });

        appointmentInformation.Children.Add(new TextBlock
        {
            Text = "Doctor: " + doctorName,
            Foreground = Brushes.Black
        });

        appointmentInformation.Children.Add(new TextBlock
        {
            Text = "Date: " + validDate.ToShortDateString(),
            Foreground = Brushes.Black
        });

        Border appointmentCard = new Border
        {
            Background = Brushes.White,
            CornerRadius = new CornerRadius(6),
            Padding = new Thickness(12),
            Margin = new Thickness(0, 4),
            Child = appointmentInformation
        };

        AppointmentListPanel.Children.Add(appointmentCard);

        AppointmentStatusText.Text =
            "Appointment added successfully.";

        AppointmentStatusText.Foreground = Brushes.Green;

        AppointmentIdTextBox.Text = "";
        AppointmentPatientTextBox.Text = "";
        AppointmentDoctorTextBox.Text = "";
        AppointmentDateTextBox.Text = "";
    }
}