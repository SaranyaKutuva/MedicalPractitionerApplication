USE [D:\DATA ACCESS\WSMP20NOV2018-22_00\WPFAPP2\WPFAPP2\BIN\DEBUG\WSMP.MDF]
GO

DECLARE	@return_value Int

EXEC	@return_value = [dbo].[usp_GetTableFieldNames]
		@TableName = N'Appointment'

SELECT	@return_value as 'Return Value'

GO
