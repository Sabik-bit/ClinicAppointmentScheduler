using ClinicAppointmentScheduler.Models;

namespace ClinicAppointmentScheduler.Services;

public class PatientService
{
    private readonly List<Patient> patients = new();

    public void AddPatient(Patient patient)
    {
        if (string.IsNullOrWhiteSpace(patient.FullName))
        {
            throw new ArgumentException("Patient name cannot be empty.");
        }

        if (string.IsNullOrWhiteSpace(patient.Phone))
        {
            throw new ArgumentException("Patient phone cannot be empty.");
        }

        patients.Add(patient);
    }

    public List<Patient> GetAllPatients()
    {
        return patients;
    }

    public Patient? FindPatient(int patientId)
    {
        return patients.FirstOrDefault(
            patient => patient.PatientId == patientId);
    }
}