namespace ClinicAppointmentScheduler.Models;

public class Patient : Person
{
    public int PatientId
    {
        get { return Id; }
        set { Id = value; }
    }

    public string Phone { get; set; }
    public string Email { get; set; }

    public Patient(int patientId, string fullName, string phone, string email)
        : base(patientId, fullName)
    {
        Phone = phone;
        Email = email;
    }

    public override string GetDetails()
    {
        return $"{PatientId}: {FullName} - {Phone} - {Email}";
    }

    public override string ToString()
    {
        return GetDetails();
    }
}