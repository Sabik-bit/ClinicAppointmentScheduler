using ClinicAppointmentScheduler.Models;

Console.WriteLine("=================================");
Console.WriteLine("   Clinic Appointment Scheduler");
Console.WriteLine("=================================");
Console.WriteLine();

Patient patient = new Patient(
    1,
    "Alex Smith",
    "0400000000",
    "alex@example.com");

Doctor doctor = new Doctor(
    1,
    "Dr Taylor",
    "General Practice");

Appointment appointment = new Appointment(
    1,
    patient,
    doctor,
    DateTime.Today.AddDays(1).AddHours(10),
    "General consultation");

Console.WriteLine("Patient:");
Console.WriteLine(patient);
Console.WriteLine();

Console.WriteLine("Doctor:");
Console.WriteLine(doctor);
Console.WriteLine();

Console.WriteLine("Appointment:");
Console.WriteLine(appointment);
