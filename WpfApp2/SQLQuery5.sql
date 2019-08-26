USE [D:\DATA ACCESS\WSMP20NOV2018-22_00\WPFAPP2\WPFAPP2\BIN\DEBUG\WSMP.MDF]
GO

DECLARE	@return_value Int

EXEC	@return_value = [dbo].[usp_GetAllAppointments]

SELECT	@return_value as 'Return Value'

GO
