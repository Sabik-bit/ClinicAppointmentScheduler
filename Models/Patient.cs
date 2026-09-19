namespace ClinicAppointmentScheduler.Models;

public class Patient
{
    public int PatientId { get; set; }
    public string FullName { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }

    public Patient(int patientId, string fullName, string phone, string email)
    {
        PatientId = patientId;
        FullName = fullName;
        Phone = phone;
        Email = email;
    }

    public override string ToString()
    {
        return $"{PatientId}: {FullName} - {Phone} - {Email}";
    }
}
