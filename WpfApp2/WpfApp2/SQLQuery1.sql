USE [C:\SARANYA\WORKINGCOPY\WSMP20NOV2018-19_30\WPFAPP2\WPFAPP2\BIN\DEBUG\WSMP.MDF]
GO

DECLARE	@return_value Int

EXEC	@return_value = [dbo].[usp_GetAllPractitionerNamesAndIDs]

SELECT	@return_value as 'Return Value'

GO
