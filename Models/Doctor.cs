namespace ClinicAppointmentScheduler.Models;

public class Doctor : Person
{
    public int DoctorId
    {
        get { return Id; }
        set { Id = value; }
    }

    public string Speciality { get; set; }

    public Doctor(int doctorId, string fullName, string speciality)
        : base(doctorId, fullName)
    {
        Speciality = speciality;
    }

    public override string GetDetails()
    {
        return $"{DoctorId}: {FullName} - {Speciality}";
    }

    public override string ToString()
    {
        return GetDetails();
    }
}