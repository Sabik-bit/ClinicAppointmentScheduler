namespace ClinicAppointmentScheduler.Models;

public class Doctor
{
    public int DoctorId { get; set; }
    public string FullName { get; set; }
    public string Speciality { get; set; }

    public Doctor(int doctorId, string fullName, string speciality)
    {
        DoctorId = doctorId;
        FullName = fullName;
        Speciality = speciality;
    }

    public override string ToString()
    {
        return $"{DoctorId}: {FullName} - {Speciality}";
    }
}
