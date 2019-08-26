USE [C:\USERS\KKUTU\SOURCE\REPOS\WPFAPP2\WPFAPP2\BIN\DEBUG\WSMP.MDF]
GO

DECLARE	@return_value Int

EXEC	@return_value = [dbo].[usp_InsertNewPractitioner]
		@Practitioner_Id = N'2',
		@Practitioner_MedicalRegNo = N'3434343434',
		@Practitioner_Fname = N'Nithi',
		@Practitioner_Sname = N'Kutuva',
		@Practitioner_desc = N'Doctor Gp',
		@Practitioner_Gender = N'Female',
		@Practitioner_Dateofbirth = N'1987-09-09',
		@Practitioner_Street_address = N'23, Barton Road',
		@Practitioner_Suburb = N'Castle Hill',
		@Practitioner_State = N'NSW',
		@Practitioner_Postcode = N'2078',
		@Practitioner_HomePhone = N'0237834747',
		@Practitioner_MobilePhone = N'0420662968',
		@monday = N'1',
		@tuesday = N'1',
		@wednesday = N'0',
		@thursday = N'1',
		@friday = N'0',
		@saturday = N'1',
		@sunday = N'0'

SELECT	@return_value as 'Return Value'

GO
