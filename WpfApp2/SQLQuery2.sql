USE [C:\SARANYA\WORKINGCOPY\WSMP21NOV2018_18_05\WPFAPP2\WPFAPP2\BIN\DEBUG\WSMP.MDF]
GO

DECLARE	@return_value Int

EXEC	@return_value = [dbo].[usp_InsertNewAppointment]
		@patient_Id = 1000,
		@practitioner_MedicalRegNo = N'365243546352435',
		@appointment_Date = N'1/12/2018',
		@appointment_Time = N'09:00'

SELECT	@return_value as 'Return Value'

GO
