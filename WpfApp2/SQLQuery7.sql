USE [D:\DATA ACCESS\WPFAPP2\WPFAPP2\BIN\DEBUG\WSMP.MDF]
GO

DECLARE	@return_value Int

EXEC	@return_value = [dbo].[usp_UpdatePatient]
		@OldPatient_Id = 1008,
		@Patient_Fname = N'Saranya',
		@Patient_Sname = N'Kutuva',
		@Patient_MedicareNo = N'43343434343',
		@Patient_Gender = N'Female',
		@Patient_DateOfBirth = N'19/07/1988',
		@Patient_Street_Address = N'20, Hornsey Street',
		@Patient_Suburb = N'Artarmon',
		@Patient_State = N'NSW',
		@Patient_PostCode = N'2064',
		@Patient_HomePhone = N'2383838383',
		@Patient_MobilePhone = N'0420988989',
		@Patient_Notes = N'Had a pain in Right Eye'

SELECT	@return_value as 'Return Value'

GO
