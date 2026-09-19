using ClinicAppointmentScheduler.Models;

namespace ClinicAppointmentScheduler.Services;

public class DoctorService
{
    private List<Doctor> doctors = new List<Doctor>();

    public void AddDoctor(Doctor doctor)
    {
        if (doctor == null)
        {
            throw new ArgumentNullException(nameof(doctor));
        }

        if (doctors.Any(d => d.DoctorId == doctor.DoctorId))
        {
            throw new InvalidOperationException(
                "A doctor with this ID already exists.");
        }

        doctors.Add(doctor);
    }

    public List<Doctor> GetAllDoctors()
    {
        return doctors;
    }

    public Doctor? FindDoctor(int doctorId)
    {
        return doctors.FirstOrDefault(
            doctor => doctor.DoctorId == doctorId);
    }
}