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