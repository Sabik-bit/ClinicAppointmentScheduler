using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;
using ClinicAppointmentScheduler.Models;
using ClinicAppointmentScheduler.Services;
using System;

namespace GUI;

public partial class MainWindow : Window
{
    private PatientService patientService = new PatientService();
    private DoctorService doctorService = new DoctorService();
    private AppointmentService appointmentService = new AppointmentService();

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
    }

    private void DoctorsButton_Click(object? sender, RoutedEventArgs e)
    {
        HideAllPanels();
        DoctorsPanel.IsVisible = true;
    }

    private void AppointmentsButton_Click(object? sender, RoutedEventArgs e)
    {
        HideAllPanels();
        AppointmentsPanel.IsVisible = true;
    }

    // =========================
    // PATIENT
    // =========================

    private void AddPatientButton_Click(object? sender, RoutedEventArgs e)
    {
        try
        {
            if (!int.TryParse(PatientIdTextBox.Text, out int patientId))
            {
                throw new ArgumentException("Patient ID must be a number.");
            }

            string name = PatientNameTextBox.Text?.Trim() ?? "";
            string phone = PatientPhoneTextBox.Text?.Trim() ?? "";
            string email = PatientEmailTextBox.Text?.Trim() ?? "";

            if (patientService.FindPatient(patientId) != null)
            {
                throw new InvalidOperationException(
                    "Patient ID already exists.");
            }

            Patient patient =
                new Patient(patientId, name, phone, email);

            patientService.AddPatient(patient);

            PatientStatusText.Text =
                "Patient added successfully.";

            PatientStatusText.Foreground = Brushes.Green;

            PatientIdTextBox.Text = "";
            PatientNameTextBox.Text = "";
            PatientPhoneTextBox.Text = "";
            PatientEmailTextBox.Text = "";

            RefreshPatients();
        }
        catch (Exception ex)
        {
            PatientStatusText.Text = ex.Message;
            PatientStatusText.Foreground = Brushes.Red;
        }
    }

    private void RefreshPatients()
    {
        PatientListPanel.Children.Clear();

        if (patientService.GetAllPatients().Count == 0)
        {
            PatientListPanel.Children.Add(new TextBlock
            {
                Text = "No patients registered yet."
            });

            return;
        }

        foreach (Patient patient in patientService.GetAllPatients())
        {
            Border card = new Border
            {
                Background = Brushes.White,
                CornerRadius = new CornerRadius(6),
                Padding = new Thickness(12),
                Margin = new Thickness(0, 4)
            };

            card.Child = new TextBlock
            {
                Text = patient.GetDetails(),
                TextWrapping = TextWrapping.Wrap
            };

            PatientListPanel.Children.Add(card);
        }
    }

    // =========================
    // DOCTOR
    // =========================

    private void AddDoctorButton_Click(object? sender, RoutedEventArgs e)
    {
        try
        {
            if (!int.TryParse(DoctorIdTextBox.Text, out int doctorId))
            {
                throw new ArgumentException(
                    "Doctor ID must be a number.");
            }

            string name =
                DoctorNameTextBox.Text?.Trim() ?? "";

            string speciality =
                DoctorSpecialityTextBox.Text?.Trim() ?? "";

            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException(
                    "Doctor name cannot be empty.");
            }

            Doctor doctor =
                new Doctor(doctorId, name, speciality);

            doctorService.AddDoctor(doctor);

            DoctorStatusText.Text =
                "Doctor added successfully.";

            DoctorStatusText.Foreground = Brushes.Green;

            DoctorIdTextBox.Text = "";
            DoctorNameTextBox.Text = "";
            DoctorSpecialityTextBox.Text = "";

            RefreshDoctors();
        }
        catch (Exception ex)
        {
            DoctorStatusText.Text = ex.Message;
            DoctorStatusText.Foreground = Brushes.Red;
        }
    }

    private void RefreshDoctors()
    {
        DoctorListPanel.Children.Clear();

        if (doctorService.GetAllDoctors().Count == 0)
        {
            DoctorListPanel.Children.Add(new TextBlock
            {
                Text = "No doctors registered yet."
            });

            return;
        }

        foreach (Doctor doctor in doctorService.GetAllDoctors())
        {
            Border card = new Border
            {
                Background = Brushes.White,
                CornerRadius = new CornerRadius(6),
                Padding = new Thickness(12),
                Margin = new Thickness(0, 4)
            };

            card.Child = new TextBlock
            {
                Text = doctor.GetDetails(),
                TextWrapping = TextWrapping.Wrap
            };

            DoctorListPanel.Children.Add(card);
        }
    }

    // =========================
    // BOOK APPOINTMENT
    // =========================

    private void AddAppointmentButton_Click(
        object? sender,
        RoutedEventArgs e)
    {
        try
        {
            if (!int.TryParse(
                AppointmentIdTextBox.Text,
                out int appointmentId))
            {
                throw new ArgumentException(
                    "Appointment ID must be a number.");
            }

            if (appointmentService.FindAppointment(appointmentId) != null)
            {
                throw new InvalidOperationException(
                    "Appointment ID already exists.");
            }

            if (!int.TryParse(
                AppointmentPatientIdTextBox.Text,
                out int patientId))
            {
                throw new ArgumentException(
                    "Patient ID must be a number.");
            }

            if (!int.TryParse(
                AppointmentDoctorIdTextBox.Text,
                out int doctorId))
            {
                throw new ArgumentException(
                    "Doctor ID must be a number.");
            }

            Patient? patient =
                patientService.FindPatient(patientId);

            if (patient == null)
            {
                throw new InvalidOperationException(
                    "Patient was not found. Register the patient first.");
            }

            Doctor? doctor =
                doctorService.FindDoctor(doctorId);

            if (doctor == null)
            {
                throw new InvalidOperationException(
                    "Doctor was not found. Register the doctor first.");
            }

            string date =
                AppointmentDateTextBox.Text?.Trim() ?? "";

            string time =
                AppointmentTimeTextBox.Text?.Trim() ?? "";

            if (!DateTime.TryParse(
                date + " " + time,
                out DateTime appointmentDateTime))
            {
                throw new ArgumentException(
                    "Please enter a valid appointment date and time.");
            }

            string reason =
                AppointmentReasonTextBox.Text?.Trim() ?? "";

            if (string.IsNullOrWhiteSpace(reason))
            {
                reason = "General appointment";
            }

            Appointment appointment =
                new Appointment(
                    appointmentId,
                    patient,
                    doctor,
                    appointmentDateTime,
                    reason);

            appointmentService.AddAppointment(appointment);

            AppointmentStatusText.Text =
                "Appointment booked successfully.";

            AppointmentStatusText.Foreground = Brushes.Green;

            AppointmentIdTextBox.Text = "";
            AppointmentPatientIdTextBox.Text = "";
            AppointmentDoctorIdTextBox.Text = "";
            AppointmentDateTextBox.Text = "";
            AppointmentTimeTextBox.Text = "";
            AppointmentReasonTextBox.Text = "";

            RefreshAppointments();
        }
        catch (Exception ex)
        {
            AppointmentStatusText.Text = ex.Message;
            AppointmentStatusText.Foreground = Brushes.Red;
        }
    }

    // =========================
    // CANCEL APPOINTMENT
    // =========================

    private void CancelAppointmentButton_Click(
        object? sender,
        RoutedEventArgs e)
    {
        try
        {
            if (!int.TryParse(
                CancelAppointmentIdTextBox.Text,
                out int appointmentId))
            {
                throw new ArgumentException(
                    "Enter a valid appointment ID.");
            }

            bool cancelled =
                appointmentService.CancelAppointment(appointmentId);

            if (!cancelled)
            {
                throw new InvalidOperationException(
                    "Appointment was not found.");
            }

            CancelStatusText.Text =
                "Appointment cancelled successfully.";

            CancelStatusText.Foreground = Brushes.Green;

            CancelAppointmentIdTextBox.Text = "";

            RefreshAppointments();
        }
        catch (Exception ex)
        {
            CancelStatusText.Text = ex.Message;
            CancelStatusText.Foreground = Brushes.Red;
        }
    }

    // =========================
    // RESCHEDULE APPOINTMENT
    // =========================

    private void RescheduleAppointmentButton_Click(
        object? sender,
        RoutedEventArgs e)
    {
        try
        {
            if (!int.TryParse(
                RescheduleAppointmentIdTextBox.Text,
                out int appointmentId))
            {
                throw new ArgumentException(
                    "Enter a valid appointment ID.");
            }

            string newDate =
                RescheduleDateTextBox.Text?.Trim() ?? "";

            string newTime =
                RescheduleTimeTextBox.Text?.Trim() ?? "";

            if (!DateTime.TryParse(
                newDate + " " + newTime,
                out DateTime newDateTime))
            {
                throw new ArgumentException(
                    "Enter a valid new date and time.");
            }

            bool rescheduled =
                appointmentService.RescheduleAppointment(
                    appointmentId,
                    newDateTime);

            if (!rescheduled)
            {
                throw new InvalidOperationException(
                    "Appointment was not found.");
            }

            RescheduleStatusText.Text =
                "Appointment rescheduled successfully.";

            RescheduleStatusText.Foreground = Brushes.Green;

            RescheduleAppointmentIdTextBox.Text = "";
            RescheduleDateTextBox.Text = "";
            RescheduleTimeTextBox.Text = "";

            RefreshAppointments();
        }
        catch (Exception ex)
        {
            RescheduleStatusText.Text = ex.Message;
            RescheduleStatusText.Foreground = Brushes.Red;
        }
    }

    // =========================
    // DISPLAY APPOINTMENTS
    // =========================

    private void RefreshAppointments()
    {
        AppointmentListPanel.Children.Clear();

        if (appointmentService.GetAllAppointments().Count == 0)
        {
            AppointmentListPanel.Children.Add(new TextBlock
            {
                Text = "No appointments booked yet."
            });

            return;
        }

        foreach (
            Appointment appointment
            in appointmentService.GetAllAppointments())
        {
            StackPanel information = new StackPanel
            {
                Spacing = 4
            };

            information.Children.Add(new TextBlock
            {
                Text =
                    "Appointment ID: " +
                    appointment.AppointmentId,
                FontWeight = FontWeight.Bold
            });

            information.Children.Add(new TextBlock
            {
                Text =
                    "Patient: " +
                    appointment.Patient.FullName +
                    " (ID: " +
                    appointment.Patient.PatientId +
                    ")"
            });

            information.Children.Add(new TextBlock
            {
                Text =
                    "Doctor: " +
                    appointment.Doctor.FullName +
                    " (ID: " +
                    appointment.Doctor.DoctorId +
                    ")"
            });

            information.Children.Add(new TextBlock
            {
                Text =
                    "Date: " +
                    appointment.AppointmentDateTime.ToShortDateString()
            });

            information.Children.Add(new TextBlock
            {
                Text =
                    "Time: " +
                    appointment.AppointmentDateTime.ToShortTimeString()
            });

            information.Children.Add(new TextBlock
            {
                Text =
                    "Reason: " +
                    appointment.Reason
            });

            TextBlock status = new TextBlock
            {
                Text =
                    "Status: " +
                    appointment.Status,
                FontWeight = FontWeight.Bold
            };

            if (appointment.Status == "Cancelled")
            {
                status.Foreground = Brushes.Red;
            }
            else
            {
                status.Foreground = Brushes.Green;
            }

            information.Children.Add(status);

            Border card = new Border
            {
                Background = Brushes.White,
                BorderBrush = Brushes.LightGray,
                BorderThickness = new Thickness(1),
                CornerRadius = new CornerRadius(6),
                Padding = new Thickness(12),
                Margin = new Thickness(0, 4),
                Child = information
            };

            AppointmentListPanel.Children.Add(card);
        }
    }
}