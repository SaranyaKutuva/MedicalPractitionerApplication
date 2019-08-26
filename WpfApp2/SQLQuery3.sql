USE [D:\DATA ACCESS\WPFAPP2\WPFAPP2\BIN\DEBUG\WSMP.MDF]
GO

DECLARE	@return_value Int

EXEC	@return_value = [dbo].[usp_InsertNewPractitioner]
		@Practitioner_Id = N'',
		@Practitioner_MedicalRegNo = N'',
		@Practitioner_Fname = N'',
		@Practitioner_Sname = N'',
		@Practitioner_desc = N'',
		@Practitioner_Gender = N'',
		@Practitioner_Dateofbirth = N'',
		@Practitioner_Street_address = N'',
		@Practitioner_Suburb = N'',
		@Practitioner_State = N'',
		@Practitioner_Postcode = N'',
		@Practitioner_HomePhone = N'',
		@Practitioner_MobilePhone = N'',
		@monday = N'',
		@tuesday = N'',
		@wednesday = N'',
		@thursday = N'',
		@friday = N'',
		@saturday = N'',
		@sunday = N''

SELECT	@return_value as 'Return Value'

GO
