using ClinicAppointmentScheduler.Models;
using ClinicAppointmentScheduler.Services;

Console.WriteLine("=================================");
Console.WriteLine("   Clinic Appointment Scheduler");
Console.WriteLine("=================================");

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

Console.WriteLine("\nSearching for Appointment ID 1:");

Appointment? foundAppointment = appointmentService.FindAppointment(1);

if (foundAppointment != null)
{
    Console.WriteLine($"Appointment found: {foundAppointment}");
}

Console.WriteLine("\nCancelling Appointment ID 1:");

bool cancelled = appointmentService.CancelAppointment(1);

if (cancelled)
{
    Console.WriteLine("Appointment cancelled successfully.");
    Console.WriteLine($"New status: {appointment1.Status}");
}
else
{
    Console.WriteLine("Appointment not found.");
}