



USE MASTER
GO
IF EXISTS	(SELECT * 
	FROM master.dbo.sysdatabases
	WHERE name = (N'Wsmp'))


	DROP DATABASE Wsmp; 
	GO

	CREATE DATABASE Wsmp;
	GO
	USE Wsmp;
	GO
	CREATE TABLE [dbo].[Patient](
	[Patient_Id] INT IDENTITY(1000,1),
	[Patient_Fname] [nvarchar](30) NULL,
	[Patient_Lname] [nvarchar](30) NULL,
	[Patient_MedicareNo] [nvarchar](15) NULL,
	[Patient_Gender] [nvarchar](12) NULL,
	[Patient_DateOfBirth] [datetime] NULL,
	[Patient_Street_Address] [nvarchar](50) NULL,
	[Patient_Suburb] [nvarchar](20) NULL,
	[Patient_State] [nvarchar](3) NULL,
	[Patient_PostCode] [nvarchar](4) NULL,
	[Patient_HomePhone] [nvarchar](10) NULL,
	[Patient_MobilePhone] [nvarchar](10) NULL,
	[Patient_Notes] [nvarchar](MAX) NULL,
 CONSTRAINT [patient_pk] PRIMARY KEY (Patient_Id))
Go

CREATE TABLE [dbo].[Practitioner](
	[Practitioner_Id] [nchar](10) NOT NULL,
	[Practitioner_MedicalRegnno] [nvarchar](15) NOT NULL,
	[Practitioner_Fname] [nvarchar](30) NOT NULL,
	[Practitioner_Lname] [nvarchar](30) NOT NULL,
	[Practitioner_Desc] [nvarchar](20) NULL,
	[Practitioner_Gender] [nvarchar](12) NULL,
	[Practitioner_Dateofbirth] [datetime] NULL,
	[Practitioner_Street_address] [nvarchar](50) NULL,
	[Practitioner_Suburb] [nvarchar](20) NULL,
	[Practitioner_State] [nvarchar](3) NULL,
	[Practitioner_Postcode] [nvarchar](4) NULL,
	[Practitioner_HomePhone] [nvarchar](10) NOT NULL,
	[Practitioner_MobilePhone] [nvarchar](10) NOT NULL,
	[Monday] [nchar](1) NULL,
	[Tuesday] [nchar](1) NULL,
	[Wednesday] [nchar](1) NULL,
	[Thursday] [nchar](1) NULL,
	[Friday] [nchar](1) NULL,
	[Saturday] [nchar](1) NULL,
	[Sunday] [nchar](1) NULL,
 CONSTRAINT [Practitioner_pk] PRIMARY KEY (Practitioner_MedicalRegnno))
Go
CREATE TABLE [dbo].[Appointment](
	[Patient_Id] [INT] NOT NULL,
	[Practitioner_MedicalRegnno] [nvarchar](15) NOT NULL,
	[Appointment_Date] [datetime] NOT NULL,
	[Appointment_Time] [time](7) NOT NULL,
 CONSTRAINT [Appointment_pk] PRIMARY KEY(
	[Patient_Id] ,
	[Practitioner_MedicalRegnno],
	[Appointment_Date],
	[Appointment_Time]))
Go
CREATE TABLE [dbo].[TimeSlot](
	[Appointment_Time] [] (20) NOT NULL,
	[StartTime] [time] (7) Null,
	[EndTime] [time] (7) Null,
	 CONSTRAINT  [TimeSlot_Pk] Primary Key(Appointment_Time))

--TABLE : APPOINTMENT
ALTER TABLE Appointment ADD CONSTRAINT Appointment_Patient_Fk FOREIGN KEY(Patient_Id) REFERENCES Patient;
ALTER TABLE Appointment ADD CONSTRAINT Appointment_Practitioner_Fk FOREIGN KEY(Practitioner_MedicalRegnno) REFERENCES Practitioner;
ALTER TABLE Appointment ADD CONSTRAINT Appointment_Timeslot__Fk FOREIGN KEY (Appointment_Time) REFERENCES TimeSlot;
