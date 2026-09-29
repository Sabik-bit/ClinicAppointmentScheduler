using ClinicAppointmentScheduler.Models;
using ClinicAppointmentScheduler.Services;

Console.WriteLine("=================================");
Console.WriteLine("   Clinic Appointment Scheduler");
Console.WriteLine("=================================");

// =====================================
// PATIENT MANAGEMENT
// =====================================

PatientService patientService = new PatientService();

Patient patient1 = new Patient(
    1,
    "Alex Smith",
    "0400000000",
    "alex@example.com");

Patient patient2 = new Patient(
    2,
    "Emma Brown",
    "0411111111",
    "emma@example.com");

patientService.AddPatient(patient1);
patientService.AddPatient(patient2);

Console.WriteLine("\nRegistered Patients:");

foreach (Patient patient in patientService.GetAllPatients())
{
    Console.WriteLine(patient);
}

Console.WriteLine("\nSearching for Patient ID 2:");

Patient? foundPatient = patientService.FindPatient(2);

if (foundPatient != null)
{
    Console.WriteLine($"Patient found: {foundPatient.FullName}");
}
else
{
    Console.WriteLine("Patient not found.");
}


// =====================================
// APPOINTMENT MANAGEMENT
// =====================================

Console.WriteLine("\nAppointment Management:");

AppointmentService appointmentService = new AppointmentService();

Doctor doctor = new Doctor(
    1,
    "Dr Taylor",
    "General Practice");

Appointment appointment1 = new Appointment(
    1,
    patient1,
    doctor,
    new DateTime(2026, 9, 21, 10, 0, 0),
    "General Check-up");

appointmentService.AddAppointment(appointment1);

Console.WriteLine("\nScheduled Appointments:");

foreach (Appointment appointment in appointmentService.GetAllAppointments())
{
    Console.WriteLine(appointment);
}


// =====================================
// FIND APPOINTMENT
// =====================================

Console.WriteLine("\nSearching for Appointment ID 1:");

Appointment? foundAppointment =
    appointmentService.FindAppointment(1);

if (foundAppointment != null)
{
    Console.WriteLine($"Appointment found: {foundAppointment}");
}
else
{
    Console.WriteLine("Appointment not found.");
}


// =====================================
// FIND APPOINTMENTS BY PATIENT
// =====================================

Console.WriteLine("\nAppointments for Patient ID 1:");

List<Appointment> patientAppointments =
    appointmentService.GetAppointmentsByPatient(1);

foreach (Appointment appointment in patientAppointments)
{
    Console.WriteLine(appointment);
}


// =====================================
// RESCHEDULE APPOINTMENT
// =====================================

Console.WriteLine("\nRescheduling Appointment ID 1:");

DateTime newDateTime =
    new DateTime(2026, 9, 22, 11, 0, 0);

bool rescheduled =
    appointmentService.RescheduleAppointment(1, newDateTime);

if (rescheduled)
{
    Console.WriteLine("Appointment rescheduled successfully.");
    Console.WriteLine(
        $"New appointment time: {appointment1.AppointmentDateTime}");
}
else
{
    Console.WriteLine("Appointment not found.");
}


// =====================================
// CANCEL APPOINTMENT
// =====================================

Console.WriteLine("\nCancelling Appointment ID 1:");

bool cancelled =
    appointmentService.CancelAppointment(1);

if (cancelled)
{
    Console.WriteLine("Appointment cancelled successfully.");
    Console.WriteLine($"New status: {appointment1.Status}");
}
else
{
    Console.WriteLine("Appointment not found.");
}


// =====================================
// DOCTOR MANAGEMENT
// =====================================

Console.WriteLine("\nDoctor Management:");

DoctorService doctorService = new DoctorService();

Doctor doctor1 = new Doctor(
    1,
    "Dr Taylor",
    "General Practice");

Doctor doctor2 = new Doctor(
    2,
    "Dr Wilson",
    "Cardiology");

doctorService.AddDoctor(doctor1);
doctorService.AddDoctor(doctor2);

Console.WriteLine("\nRegistered Doctors:");

foreach (Doctor currentDoctor in doctorService.GetAllDoctors())
{
    Console.WriteLine(currentDoctor);
}


// =====================================
// FIND DOCTOR
// =====================================

Console.WriteLine("\nSearching for Doctor ID 2:");

Doctor? foundDoctor =
    doctorService.FindDoctor(2);

if (foundDoctor != null)
{
    Console.WriteLine($"Doctor found: {foundDoctor.FullName}");
}
else
{
    Console.WriteLine("Doctor not found.");
}

Console.WriteLine("\n=================================");
Console.WriteLine("       Program Completed");
Console.WriteLine("=================================");