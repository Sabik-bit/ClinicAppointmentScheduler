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
        Title = "Clinic Appointment Scheduler - Patients";
        RefreshPatientList();
    }

    private void DoctorsButton_Click(object? sender, RoutedEventArgs e)
    {
        HideAllPanels();
        DoctorsPanel.IsVisible = true;
        Title = "Clinic Appointment Scheduler - Doctors";
        RefreshDoctorList();
    }

    private void AppointmentsButton_Click(object? sender, RoutedEventArgs e)
    {
        HideAllPanels();
        AppointmentsPanel.IsVisible = true;
        Title = "Clinic Appointment Scheduler - Appointments";
        RefreshAppointmentList();
    }

    // =========================
    // PATIENTS
    // =========================

    private void AddPatientButton_Click(object? sender, RoutedEventArgs e)
    {
        if (!int.TryParse(PatientIdTextBox.Text, out int patientId))
        {
            PatientStatusText.Text = "Please enter a valid Patient ID.";
            PatientStatusText.Foreground = Brushes.Red;
            return;
        }

        string name = PatientNameTextBox.Text?.Trim() ?? "";
        string phone = PatientPhoneTextBox.Text?.Trim() ?? "";
        string email = PatientEmailTextBox.Text?.Trim() ?? "";

        if (name == "" || phone == "")
        {
            PatientStatusText.Text = "Please enter patient name and phone.";
            PatientStatusText.Foreground = Brushes.Red;
            return;
        }

        if (patientService.FindPatient(patientId) != null)
        {
            PatientStatusText.Text = "Patient ID already exists.";
            PatientStatusText.Foreground = Brushes.Red;
            return;
        }

        try
        {
            Patient patient = new Patient(
                patientId,
                name,
                phone,
                email);

            patientService.AddPatient(patient);

            PatientStatusText.Text = "Patient added successfully.";
            PatientStatusText.Foreground = Brushes.Green;

            PatientIdTextBox.Text = "";
            PatientNameTextBox.Text = "";
            PatientPhoneTextBox.Text = "";
            PatientEmailTextBox.Text = "";

            RefreshPatientList();
        }
        catch (Exception ex)
        {
            PatientStatusText.Text = ex.Message;
            PatientStatusText.Foreground = Brushes.Red;
        }
    }

    private void RefreshPatientList()
    {
        PatientListPanel.Children.Clear();

        foreach (Patient patient in patientService.GetAllPatients())
        {
            StackPanel patientInfo = new StackPanel
            {
                Spacing = 4
            };

            patientInfo.Children.Add(new TextBlock
            {
                Text = patient.FullName + " (ID: " + patient.PatientId + ")",
                FontWeight = FontWeight.Bold,
                Foreground = Brushes.Black
            });

            patientInfo.Children.Add(new TextBlock
            {
                Text = "Phone: " + patient.Phone,
                Foreground = Brushes.Black
            });

            patientInfo.Children.Add(new TextBlock
            {
                Text = "Email: " + patient.Email,
                Foreground = Brushes.Black
            });

            Border patientCard = new Border
            {
                Background = Brushes.White,
                CornerRadius = new CornerRadius(8),
                Padding = new Thickness(15),
                Margin = new Thickness(0, 5),
                Child = patientInfo
            };

            PatientListPanel.Children.Add(patientCard);
        }
    }

    // =========================
    // DOCTORS
    // =========================

    private void AddDoctorButton_Click(object? sender, RoutedEventArgs e)
    {
        if (!int.TryParse(DoctorIdTextBox.Text, out int doctorId))
        {
            DoctorStatusText.Text = "Please enter a valid Doctor ID.";
            DoctorStatusText.Foreground = Brushes.Red;
            return;
        }

        string name = DoctorNameTextBox.Text?.Trim() ?? "";
        string speciality = DoctorSpecialityTextBox.Text?.Trim() ?? "";

        if (name == "" || speciality == "")
        {
            DoctorStatusText.Text = "Please complete all doctor fields.";
            DoctorStatusText.Foreground = Brushes.Red;
            return;
        }

        try
        {
            Doctor doctor = new Doctor(
                doctorId,
                name,
                speciality);

            doctorService.AddDoctor(doctor);

            DoctorStatusText.Text = "Doctor added successfully.";
            DoctorStatusText.Foreground = Brushes.Green;

            DoctorIdTextBox.Text = "";
            DoctorNameTextBox.Text = "";
            DoctorSpecialityTextBox.Text = "";

            RefreshDoctorList();
        }
        catch (Exception ex)
        {
            DoctorStatusText.Text = ex.Message;
            DoctorStatusText.Foreground = Brushes.Red;
        }
    }

    private void RefreshDoctorList()
    {
        DoctorListPanel.Children.Clear();

        foreach (Doctor doctor in doctorService.GetAllDoctors())
        {
            StackPanel doctorInfo = new StackPanel
            {
                Spacing = 4
            };

            doctorInfo.Children.Add(new TextBlock
            {
                Text = doctor.FullName + " (ID: " + doctor.DoctorId + ")",
                FontWeight = FontWeight.Bold,
                Foreground = Brushes.Black
            });

            doctorInfo.Children.Add(new TextBlock
            {
                Text = "Speciality: " + doctor.Speciality,
                Foreground = Brushes.Black
            });

            Border doctorCard = new Border
            {
                Background = Brushes.White,
                CornerRadius = new CornerRadius(8),
                Padding = new Thickness(15),
                Margin = new Thickness(0, 5),
                Child = doctorInfo
            };

            DoctorListPanel.Children.Add(doctorCard);
        }
    }

    // =========================
    // APPOINTMENTS
    // =========================

    private void AddAppointmentButton_Click(object? sender, RoutedEventArgs e)
    {
        if (!int.TryParse(
            AppointmentIdTextBox.Text,
            out int appointmentId))
        {
            AppointmentStatusText.Text =
                "Please enter a valid Appointment ID.";

            AppointmentStatusText.Foreground = Brushes.Red;
            return;
        }

        if (!int.TryParse(
            AppointmentPatientIdTextBox.Text,
            out int patientId))
        {
            AppointmentStatusText.Text =
                "Please enter a valid Patient ID.";

            AppointmentStatusText.Foreground = Brushes.Red;
            return;
        }

        if (!int.TryParse(
            AppointmentDoctorIdTextBox.Text,
            out int doctorId))
        {
            AppointmentStatusText.Text =
                "Please enter a valid Doctor ID.";

            AppointmentStatusText.Foreground = Brushes.Red;
            return;
        }

        if (appointmentService.FindAppointment(appointmentId) != null)
        {
            AppointmentStatusText.Text =
                "Appointment ID already exists.";

            AppointmentStatusText.Foreground = Brushes.Red;
            return;
        }

        Patient? patient = patientService.FindPatient(patientId);
        Doctor? doctor = doctorService.FindDoctor(doctorId);

        if (patient == null)
        {
            AppointmentStatusText.Text =
                "Patient ID was not found.";

            AppointmentStatusText.Foreground = Brushes.Red;
            return;
        }

        if (doctor == null)
        {
            AppointmentStatusText.Text =
                "Doctor ID was not found.";

            AppointmentStatusText.Foreground = Brushes.Red;
            return;
        }

        string date =
            AppointmentDateTextBox.Text?.Trim() ?? "";

        string time =
            AppointmentTimeTextBox.Text?.Trim() ?? "";

        string reason =
            AppointmentReasonTextBox.Text?.Trim() ?? "";

        if (!DateTime.TryParse(
            date + " " + time,
            out DateTime appointmentDateTime))
        {
            AppointmentStatusText.Text =
                "Please enter a valid date and time.";

            AppointmentStatusText.Foreground = Brushes.Red;
            return;
        }

        if (reason == "")
        {
            AppointmentStatusText.Text =
                "Please enter an appointment reason.";

            AppointmentStatusText.Foreground = Brushes.Red;
            return;
        }

        try
        {
            Appointment appointment = new Appointment(
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

            RefreshAppointmentList();
        }
        catch (Exception ex)
        {
            AppointmentStatusText.Text = ex.Message;
            AppointmentStatusText.Foreground = Brushes.Red;
        }
    }

    private void RefreshAppointmentList()
    {
        AppointmentListPanel.Children.Clear();

        foreach (Appointment appointment
                 in appointmentService.GetAllAppointments())
        {
            StackPanel appointmentInfo = new StackPanel
            {
                Spacing = 5
            };

            appointmentInfo.Children.Add(new TextBlock
            {
                Text = "Appointment ID: " +
                       appointment.AppointmentId,
                FontWeight = FontWeight.Bold,
                Foreground = Brushes.Black
            });

            appointmentInfo.Children.Add(new TextBlock
            {
                Text = "Patient: " +
                       appointment.Patient.FullName +
                       " (ID: " +
                       appointment.Patient.PatientId +
                       ")",
                Foreground = Brushes.Black
            });

            appointmentInfo.Children.Add(new TextBlock
            {
                Text = "Doctor: " +
                       appointment.Doctor.FullName +
                       " (ID: " +
                       appointment.Doctor.DoctorId +
                       ")",
                Foreground = Brushes.Black
            });

            appointmentInfo.Children.Add(new TextBlock
            {
                Text = "Date: " +
                       appointment.AppointmentDateTime
                           .ToShortDateString(),
                Foreground = Brushes.Black
            });

            appointmentInfo.Children.Add(new TextBlock
            {
                Text = "Time: " +
                       appointment.AppointmentDateTime
                           .ToShortTimeString(),
                Foreground = Brushes.Black
            });

            appointmentInfo.Children.Add(new TextBlock
            {
                Text = "Reason: " + appointment.Reason,
                Foreground = Brushes.Black
            });

            appointmentInfo.Children.Add(new TextBlock
            {
                Text = "Status: " + appointment.Status,
                FontWeight = FontWeight.Bold,
                Foreground =
                    appointment.Status == "Cancelled"
                    ? Brushes.Red
                    : Brushes.Green
            });

            Border appointmentCard = new Border
            {
                Background = Brushes.White,
                CornerRadius = new CornerRadius(8),
                Padding = new Thickness(15),
                Margin = new Thickness(0, 5),
                Child = appointmentInfo
            };

            AppointmentListPanel.Children.Add(
                appointmentCard);
        }
    }

    // =========================
    // CANCEL APPOINTMENT
    // =========================

    private void CancelAppointmentButton_Click(
        object? sender,
        RoutedEventArgs e)
    {
        if (!int.TryParse(
            CancelAppointmentIdTextBox.Text,
            out int appointmentId))
        {
            CancelStatusText.Text =
                "Please enter a valid Appointment ID.";

            CancelStatusText.Foreground = Brushes.Red;
            return;
        }

        Appointment? appointment =
            appointmentService.FindAppointment(
                appointmentId);

        if (appointment == null)
        {
            CancelStatusText.Text =
                "Appointment was not found.";

            CancelStatusText.Foreground = Brushes.Red;
            return;
        }

        if (appointment.Status == "Cancelled")
        {
            CancelStatusText.Text =
                "Appointment is already cancelled.";

            CancelStatusText.Foreground = Brushes.Red;
            return;
        }

        appointmentService.CancelAppointment(
            appointmentId);

        CancelStatusText.Text =
            "Appointment cancelled successfully.";

        CancelStatusText.Foreground = Brushes.Green;

        CancelAppointmentIdTextBox.Text = "";

        RefreshAppointmentList();
    }

    // =========================
    // RESCHEDULE APPOINTMENT
    // =========================

    private void RescheduleAppointmentButton_Click(
        object? sender,
        RoutedEventArgs e)
    {
        if (!int.TryParse(
            RescheduleAppointmentIdTextBox.Text,
            out int appointmentId))
        {
            RescheduleStatusText.Text =
                "Please enter a valid Appointment ID.";

            RescheduleStatusText.Foreground = Brushes.Red;
            return;
        }

        Appointment? appointment =
            appointmentService.FindAppointment(
                appointmentId);

        if (appointment == null)
        {
            RescheduleStatusText.Text =
                "Appointment was not found.";

            RescheduleStatusText.Foreground = Brushes.Red;
            return;
        }

        if (appointment.Status == "Cancelled")
        {
            RescheduleStatusText.Text =
                "Cancelled appointments cannot be rescheduled.";

            RescheduleStatusText.Foreground = Brushes.Red;
            return;
        }

        string newDate =
            RescheduleDateTextBox.Text?.Trim() ?? "";

        string newTime =
            RescheduleTimeTextBox.Text?.Trim() ?? "";

        if (!DateTime.TryParse(
            newDate + " " + newTime,
            out DateTime newDateTime))
        {
            RescheduleStatusText.Text =
                "Please enter a valid new date and time.";

            RescheduleStatusText.Foreground = Brushes.Red;
            return;
        }

        try
        {
            bool result =
                appointmentService.RescheduleAppointment(
                    appointmentId,
                    newDateTime);

            if (result)
            {
                RescheduleStatusText.Text =
                    "Appointment rescheduled successfully.";

                RescheduleStatusText.Foreground =
                    Brushes.Green;

                RescheduleAppointmentIdTextBox.Text = "";
                RescheduleDateTextBox.Text = "";
                RescheduleTimeTextBox.Text = "";

                RefreshAppointmentList();
            }
            else
            {
                RescheduleStatusText.Text =
                    "Appointment was not found.";

                RescheduleStatusText.Foreground =
                    Brushes.Red;
            }
        }
        catch (Exception ex)
        {
            RescheduleStatusText.Text = ex.Message;
            RescheduleStatusText.Foreground = Brushes.Red;
        }
    }
}