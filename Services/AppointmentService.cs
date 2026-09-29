using ClinicAppointmentScheduler.Models;

namespace ClinicAppointmentScheduler.Services;

public class AppointmentService
{
    private List<Appointment> appointments = new List<Appointment>();

    public void AddAppointment(Appointment appointment)
    {
        if (appointment == null)
        {
            throw new ArgumentNullException(nameof(appointment));
        }

        if (appointments.Any(a =>
            a.Doctor.DoctorId == appointment.Doctor.DoctorId &&
            a.AppointmentDateTime == appointment.AppointmentDateTime))
        {
            throw new InvalidOperationException(
                "Doctor already has an appointment at this time.");
        }

        appointments.Add(appointment);
    }

    public bool RescheduleAppointment(int appointmentId, DateTime newDateTime)
{
    Appointment? appointment = FindAppointment(appointmentId);

    if (appointment == null)
    {
        return false;
    }

    if (appointments.Any(a =>
        a.AppointmentId != appointmentId &&
        a.Doctor.DoctorId == appointment.Doctor.DoctorId &&
        a.AppointmentDateTime == newDateTime &&
        a.Status != "Cancelled"))
    {
        throw new InvalidOperationException(
            "Doctor already has an appointment at this time.");
    }

    appointment.AppointmentDateTime = newDateTime;
    return true;
}

    public List<Appointment> GetAllAppointments()
    {
        return appointments;
    }

    public Appointment? FindAppointment(int appointmentId)
    {
        return appointments.FirstOrDefault(
            appointment => appointment.AppointmentId == appointmentId);
    }

public List<Appointment> GetAppointmentsByPatient(int patientId)
{
    return appointments
        .Where(appointment => appointment.Patient.PatientId == patientId)
        .ToList();
}
    public bool CancelAppointment(int appointmentId)
    {
        Appointment? appointment = FindAppointment(appointmentId);

        if (appointment == null)
        {
            return false;
        }

        appointment.Status = "Cancelled";
        return true;
    }
}