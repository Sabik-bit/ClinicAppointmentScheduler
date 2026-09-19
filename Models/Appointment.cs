namespace ClinicAppointmentScheduler.Models;

public class Appointment
{
    public int AppointmentId { get; set; }
    public Patient Patient { get; set; }
    public Doctor Doctor { get; set; }
    public DateTime AppointmentDateTime { get; set; }
    public string Reason { get; set; }
    public string Status { get; set; }

    public Appointment(
        int appointmentId,
        Patient patient,
        Doctor doctor,
        DateTime appointmentDateTime,
        string reason)
    {
        AppointmentId = appointmentId;
        Patient = patient;
        Doctor = doctor;
        AppointmentDateTime = appointmentDateTime;
        Reason = reason;
        Status = "Scheduled";
    }

    public override string ToString()
    {
        return $"{AppointmentId}: {Patient.FullName} with {Doctor.FullName} on " +
               $"{AppointmentDateTime:g} - {Status}";
    }
}
