# Clinic Appointment Scheduler

## Project Description

The Clinic Appointment Scheduler is a C# application developed for ITS203 Object-Oriented Design and Programming.

The system is designed to manage patients, doctors, and clinic appointments. It currently supports patient and doctor management, appointment scheduling, appointment searching, cancellation, rescheduling, and doctor appointment conflict checking.

## Current Features

- Add and list patients
- Search for patients by ID
- Add and list doctors
- Search for doctors by ID
- Schedule appointments
- Search for appointments by ID
- Cancel appointments
- Reschedule appointments
- Prevent conflicting appointments for the same doctor

## Project Structure

- `Models/Patient.cs` - stores patient information
- `Models/Doctor.cs` - stores doctor information
- `Models/Appointment.cs` - stores appointment information
- `Services/PatientService.cs` - manages patient operations
- `Services/DoctorService.cs` - manages doctor operations
- `Services/AppointmentService.cs` - manages appointment operations
- `Program.cs` - runs and tests the application

## How to Run

1. Open the `ClinicAppointmentScheduler` folder in Visual Studio Code.
2. Open Terminal > New Terminal.
3. Build the project:

   `dotnet build`

4. Run the application:

   `dotnet run`

## Development Status

The application is under active development for ITS203 Assessment C. Additional object-oriented programming features, validation, exception handling, interface development, and testing will be added during the remaining development stages.

## References and Tools Used

- C#
- .NET
- Visual Studio Code
- Git
- GitHub