USE [STOCK_M1]
GO
EXEC sys.sp_dropextendedproperty @name=N'MS_DiagramPaneCount' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'View_OTHER_OPION_Report_SaleBy_Invoice'

GO
EXEC sys.sp_dropextendedproperty @name=N'MS_DiagramPane1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'View_OTHER_OPION_Report_SaleBy_Invoice'

GO
EXEC sys.sp_dropextendedproperty @name=N'MS_DiagramPaneCount' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'View_OTHER_OPION_Report_SaleBy_Detail'

GO
EXEC sys.sp_dropextendedproperty @name=N'MS_DiagramPane2' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'View_OTHER_OPION_Report_SaleBy_Detail'

GO
EXEC sys.sp_dropextendedproperty @name=N'MS_DiagramPane1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'View_OTHER_OPION_Report_SaleBy_Detail'

GO
EXEC sys.sp_dropextendedproperty @name=N'MS_DiagramPaneCount' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'View_OTHER_OPION_Report_OwedBy_Invoice'

GO
EXEC sys.sp_dropextendedproperty @name=N'MS_DiagramPane1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'View_OTHER_OPION_Report_OwedBy_Invoice'

GO
ALTER TABLE [dbo].[tbl_OTHER_OPTIONS_SEARCH] DROP CONSTRAINT [DF_tbl_OTHER_OPTIONS_SEARCH_SearchAdvanceOnly]
GO
ALTER TABLE [dbo].[tbl_OTHER_OPTIONS_SEARCH] DROP CONSTRAINT [DF_tbl_OTHER_OPTIONS_SEARCH_SearchDate]
GO
ALTER TABLE [dbo].[tbl_OTHER_OPTIONS_SEARCH] DROP CONSTRAINT [DF_tbl_OTHER_OPTIONS_SEARCH_SearchUser]
GO
ALTER TABLE [dbo].[tbl_OTHER_OPTIONS_SEARCH] DROP CONSTRAINT [DF_tbl_OTHER_OPTIONS_SEARCH_SearchStock]
GO
ALTER TABLE [dbo].[tbl_OTHER_OPTIONS_SEARCH] DROP CONSTRAINT [DF_tbl_OTHER_OPTIONS_SEARCH_SearchSupplier]
GO
ALTER TABLE [dbo].[tbl_OTHER_OPTIONS_SEARCH] DROP CONSTRAINT [DF_tbl_OTHER_OPTIONS_SEARCH_SearchCustomer]
GO
ALTER TABLE [dbo].[tbl_OTHER_OPTIONS_SEARCH] DROP CONSTRAINT [DF_tbl_OTHER_OPTIONS_SEARCH_SearchInvoice]
GO
ALTER TABLE [dbo].[tbl_OTHER_OPTIONS_SEARCH] DROP CONSTRAINT [DF_tbl_OTHER_OPTIONS_SEARCH_SearchCategory]
GO
ALTER TABLE [dbo].[tbl_OTHER_OPTIONS_SEARCH] DROP CONSTRAINT [DF_tbl_OTHER_OPTIONS_SEARCH_SearchProduct]
GO
/****** Object:  View [dbo].[View_OTHER_OPION_Report_SaleBy_Invoice]    Script Date: 2/19/2025 5:07:56 PM ******/
DROP VIEW [dbo].[View_OTHER_OPION_Report_SaleBy_Invoice]
GO
/****** Object:  View [dbo].[View_OTHER_OPION_Report_SaleBy_Detail]    Script Date: 2/19/2025 5:07:56 PM ******/
DROP VIEW [dbo].[View_OTHER_OPION_Report_SaleBy_Detail]
GO
/****** Object:  View [dbo].[View_OTHER_OPION_Report_OwedBy_Invoice]    Script Date: 2/19/2025 5:07:56 PM ******/
DROP VIEW [dbo].[View_OTHER_OPION_Report_OwedBy_Invoice]
GO
/****** Object:  Table [dbo].[tbl_OTHER_OPTIONS_SEARCH]    Script Date: 2/19/2025 5:07:56 PM ******/
DROP TABLE [dbo].[tbl_OTHER_OPTIONS_SEARCH]
GO
/****** Object:  Table [dbo].[tbl_OTHER_OPTIONS]    Script Date: 2/19/2025 5:07:56 PM ******/
DROP TABLE [dbo].[tbl_OTHER_OPTIONS]
GO
/****** Object:  StoredProcedure [dbo].[Pro_OTHER_OPTION_Report_SaleBy_Invoice]    Script Date: 2/19/2025 5:07:56 PM ******/
DROP PROCEDURE [dbo].[Pro_OTHER_OPTION_Report_SaleBy_Invoice]
GO
/****** Object:  StoredProcedure [dbo].[Pro_OTHER_OPTION_Report_SaleBy_Detail]    Script Date: 2/19/2025 5:07:56 PM ******/
DROP PROCEDURE [dbo].[Pro_OTHER_OPTION_Report_SaleBy_Detail]
GO
/****** Object:  StoredProcedure [dbo].[Pro_OTHER_OPTION_Report_OwedBy_Invoice]    Script Date: 2/19/2025 5:07:56 PM ******/
DROP PROCEDURE [dbo].[Pro_OTHER_OPTION_Report_OwedBy_Invoice]
GO
/****** Object:  StoredProcedure [dbo].[Pro_OTHER_OPTION_Report_OwedBy_Invoice]    Script Date: 2/19/2025 5:07:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


























CREATE PROCEDURE [dbo].[Pro_OTHER_OPTION_Report_OwedBy_Invoice]
 @ID As bigint
 ,@UserID as bigint
 ,@EmployeeID As bigint
 ,@RunID As varchar(100)
AS
BEGIN

if @ID =-2
 Begin
-----Block Select Blank--------------------------------
  -- Insert statements for procedure here
SELECT [customerOrderID]
      ,[orderDate]
      ,[Time]
      ,[totalAmount]
      ,[percentDiscount]
      ,[amtDiscount]
      ,[amtOrderExpend]
      ,[netAmount]
      ,[Rate_VAT]
      ,[TotalForVAT]
      ,[VAT]
      ,[grandTotal]
      ,[amtReceived]
      ,[amtReturn]
      ,[amtOwed]
      ,[usdTokhr]
      ,[usdToTHB]
      ,[InvoiceNo]
      ,[DescripitonTerm]
      ,[fullName]
      ,[idType]
      ,[Nationality]
      ,[Gender]
      ,[userID]
      ,[UserName]
	  ,[Mobile]
,'' As PrintBy
,getdate() As FromDate
,getdate() As ToDate
  FROM [dbo].[View_OTHER_OPION_Report_OwedBy_Invoice]
Where 1=-2 ;
-----Block Select Blank--------------------------------
 End
Else
 Begin
--Select from tbl_OTHER_OPTIONS_FILTER -----------
---Declare @ID As bigint;
---Declare @UserID as bigint;
Declare @para_ReportName As nvarchar(100);
Declare @para_Stored_Pro_Name As nvarchar(100);
---Declare @EmployeeID As bigint;
---Declare @RunID as varchar(100)
Declare @para_Calculate_Time As int;
Declare @paraEmployee_Set As int;
Declare @para01_cmdCalculate As int;
Declare @para02_cmdViewReportMS As int;
Declare @para03_cmdViewReportCR As int;
Declare @para04_cmdExport As int;
Declare @para05_cmdImport As int;
Declare @para06_cboGroup As nvarchar(50);
Declare @para07_cboName As nvarchar(50);
Declare @para08_0_TabMonthly As int;
Declare @para08_1_cboMonthly As datetime;
Declare @para08_2_Monthly_cboFrom As datetime;
Declare @para08_3_Monthly_cboTo As datetime;
Declare @para09_0_TabDate As int;
Declare @para09_1_Date As datetime;
Declare @para10_0_TabDate_From_To As int;
Declare @para10_1_Date_From_To_cboFrom As datetime;
Declare @para10_2_Date_From_To_cboTo As datetime;
Declare @para11_0_TabYearly As int;
Declare @para11_1_Yearly_cboYear As datetime;
Declare @para_12_Note As nvarchar(50);
Declare @para_13_Note As nvarchar(50);
Declare @para_14_Note As nvarchar(50);
Declare @para_15_Note As nvarchar(50);
Declare @para_16_Note As nvarchar(50);
Declare @para_17_Note As nvarchar(50);
Declare @para_18_Note As nvarchar(50);
Declare @para_19_Note As nvarchar(50);
Declare @para_20_Note As nvarchar(50);
Declare @para_21_Note As nvarchar(50);
Declare @para_22_Note As nvarchar(50);
Declare @para_23_Note As nvarchar(50);
Declare @para_24_Note As nvarchar(50);
Declare @para_25_Note As nvarchar(50);

SELECT 
--    @ID = [ID]
--    ,@UserID = [UserID]
      @para_ReportName=[para_ReportName]
      ,@para_Stored_Pro_Name=[para_Stored_Pro_Name]
--      ,@EmployeeID = [EmployeeID]
--  ,@RunID=[RunID]
      ,@para_Calculate_Time=[para_Calculate_Time]
      ,@paraEmployee_Set=[paraEmployee_Set]
      ,@para01_cmdCalculate=[para01_cmdCalculate]
      ,@para02_cmdViewReportMS=[para02_cmdViewReportMS]
      ,@para03_cmdViewReportCR=[para03_cmdViewReportCR]
      ,@para04_cmdExport=[para04_cmdExport]
      ,@para05_cmdImport=[para05_cmdImport]
      ,@para06_cboGroup=[para06_cboGroup]
      ,@para07_cboName=[para07_cboName]
      ,@para08_0_TabMonthly=[para08_0_TabMonthly]
      ,@para08_1_cboMonthly=[para08_1_cboMonthly]
      ,@para08_2_Monthly_cboFrom=[para08_2_Monthly_cboFrom]
      ,@para08_3_Monthly_cboTo=[para08_3_Monthly_cboTo]
      ,@para09_0_TabDate=[para09_0_TabDate]
      ,@para09_1_Date=[para09_1_Date]
      ,@para10_0_TabDate_From_To=[para10_0_TabDate_From_To]
      ,@para10_1_Date_From_To_cboFrom=[para10_1_Date_From_To_cboFrom]
      ,@para10_2_Date_From_To_cboTo=[para10_2_Date_From_To_cboTo]
      ,@para11_0_TabYearly=[para11_0_TabYearly]
      ,@para11_1_Yearly_cboYear=[para11_1_Yearly_cboYear]
      ,@para_12_Note=[para_12_Note]
      ,@para_13_Note=[para_13_Note]
      ,@para_14_Note=[para_14_Note]
      ,@para_15_Note=[para_15_Note]
      ,@para_16_Note=[para_16_Note]
      ,@para_17_Note=[para_17_Note]
      ,@para_18_Note=[para_18_Note]
      ,@para_19_Note=[para_19_Note]
      ,@para_20_Note=[para_20_Note]
      ,@para_21_Note=[para_21_Note]
      ,@para_22_Note=[para_22_Note]
      ,@para_23_Note=[para_23_Note]
      ,@para_24_Note=[para_24_Note]
      ,@para_25_Note=[para_25_Note]
  FROM [tbl_OTHER_OPTIONS_FILTER]
  Where [UserID]=@UserID;
--Select from tbl_OTHER_OPTIONS_FILTER -----------
-----Block Do Sth --------------------------------
Declare @FromDate As datetime;
Declare @ToDate As datetime;
Declare @PrintBy As nvarchar(30);

Set @FromDate = @para10_1_Date_From_To_cboFrom;
Set @ToDate = @para10_2_Date_From_To_cboTo;
Select @PrintBy = UserName FROM [dbo].[tbl_useraccount] WHERE [UserID] = @UserID;
SELECT [customerOrderID]
      ,[orderDate]
      ,[Time]
      ,[totalAmount]
      ,[percentDiscount]
      ,[amtDiscount]
      ,[amtOrderExpend]
      ,[netAmount]
      ,[Rate_VAT]
      ,[TotalForVAT]
      ,[VAT]
      ,[grandTotal]
      ,[amtReceived]
      ,[amtReturn]
      ,[amtOwed]
      ,[usdTokhr]
      ,[usdToTHB]
      ,[InvoiceNo]
      ,[DescripitonTerm]
      ,[fullName]
      ,[idType]
      ,[Nationality]
      ,[Gender]
      ,[userID]
      ,[UserName]
	  ,[Mobile]
 ,@PrintBy As PrintBy
 ,@FromDate As FromDate
 ,@ToDate As ToDate
  FROM [dbo].[View_OTHER_OPION_Report_OwedBy_Invoice]
Where [userID] IN(SELECT [EmployeeID] FROM [dbo].[tbl_OTHER_OPTIONS_Employee_Set] WHERE [UserID] = @UserID)
And [amtOwed] > 0
And CONVERT (DATETIME, REPLACE(CONVERT (VARCHAR(10), [orderDate], 111), '/', '-')) between @FromDate and @ToDate
-----Block Do Sth -------------------------------- 
 
 End

   END


























GO
/****** Object:  StoredProcedure [dbo].[Pro_OTHER_OPTION_Report_SaleBy_Detail]    Script Date: 2/19/2025 5:07:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




























CREATE PROCEDURE [dbo].[Pro_OTHER_OPTION_Report_SaleBy_Detail]
 @ID As bigint
 ,@UserID as bigint
 ,@EmployeeID As bigint
 ,@RunID As varchar(100)
AS
BEGIN

if @ID =-2
 Begin
-----Block Select Blank--------------------------------
  -- Insert statements for procedure here
SELECT [customerOrderID]
      ,[InvoiceNo]
      ,[fullName]
      ,[idType]
      ,[Nationality]
      ,[Gender]
      ,[userID]
      ,[UserName]
      ,[Mobile]
      ,[usdTokhr]
      ,[usdToTHB]
      ,[orderDetailsID]
      ,[prdID]
      ,[orderDate]
      ,[unitType]
      ,[unitPrice]
      ,[orderQty]
      ,[totalAmt]
      ,[PytStatus]
      ,[roomtableID]
      ,[ReservationID]
      ,[NumPrinted]
      ,[NumPrintedInv]
      ,[percDisc]
      ,[AmtDisc]
      ,[orderTime]
      ,[VoidDate]
      ,[BuyingTotal]
      ,[TaxNote]
      ,[OrderDetailDescription]
      ,[PrdNameEng]
      ,[PrdNameKh]
,'' As PrintBy
,getdate() As FromDate
,getdate() As ToDate
  FROM [dbo].[View_OTHER_OPION_Report_SaleBy_Detail]
Where 1=-2 ;
-----Block Select Blank--------------------------------
 End
Else
 Begin
--Select from tbl_OTHER_OPTIONS_FILTER -----------
---Declare @ID As bigint;
---Declare @UserID as bigint;
Declare @para_ReportName As nvarchar(100);
Declare @para_Stored_Pro_Name As nvarchar(100);
---Declare @EmployeeID As bigint;
---Declare @RunID as varchar(100)
Declare @para_Calculate_Time As int;
Declare @paraEmployee_Set As int;
Declare @para01_cmdCalculate As int;
Declare @para02_cmdViewReportMS As int;
Declare @para03_cmdViewReportCR As int;
Declare @para04_cmdExport As int;
Declare @para05_cmdImport As int;
Declare @para06_cboGroup As nvarchar(50);
Declare @para07_cboName As nvarchar(50);
Declare @para08_0_TabMonthly As int;
Declare @para08_1_cboMonthly As datetime;
Declare @para08_2_Monthly_cboFrom As datetime;
Declare @para08_3_Monthly_cboTo As datetime;
Declare @para09_0_TabDate As int;
Declare @para09_1_Date As datetime;
Declare @para10_0_TabDate_From_To As int;
Declare @para10_1_Date_From_To_cboFrom As datetime;
Declare @para10_2_Date_From_To_cboTo As datetime;
Declare @para11_0_TabYearly As int;
Declare @para11_1_Yearly_cboYear As datetime;
Declare @para_12_Note As nvarchar(50);
Declare @para_13_Note As nvarchar(50);
Declare @para_14_Note As nvarchar(50);
Declare @para_15_Note As nvarchar(50);
Declare @para_16_Note As nvarchar(50);
Declare @para_17_Note As nvarchar(50);
Declare @para_18_Note As nvarchar(50);
Declare @para_19_Note As nvarchar(50);
Declare @para_20_Note As nvarchar(50);
Declare @para_21_Note As nvarchar(50);
Declare @para_22_Note As nvarchar(50);
Declare @para_23_Note As nvarchar(50);
Declare @para_24_Note As nvarchar(50);
Declare @para_25_Note As nvarchar(50);

SELECT 
--    @ID = [ID]
--    ,@UserID = [UserID]
      @para_ReportName=[para_ReportName]
      ,@para_Stored_Pro_Name=[para_Stored_Pro_Name]
--      ,@EmployeeID = [EmployeeID]
--  ,@RunID=[RunID]
      ,@para_Calculate_Time=[para_Calculate_Time]
      ,@paraEmployee_Set=[paraEmployee_Set]
      ,@para01_cmdCalculate=[para01_cmdCalculate]
      ,@para02_cmdViewReportMS=[para02_cmdViewReportMS]
      ,@para03_cmdViewReportCR=[para03_cmdViewReportCR]
      ,@para04_cmdExport=[para04_cmdExport]
      ,@para05_cmdImport=[para05_cmdImport]
      ,@para06_cboGroup=[para06_cboGroup]
      ,@para07_cboName=[para07_cboName]
      ,@para08_0_TabMonthly=[para08_0_TabMonthly]
      ,@para08_1_cboMonthly=[para08_1_cboMonthly]
      ,@para08_2_Monthly_cboFrom=[para08_2_Monthly_cboFrom]
      ,@para08_3_Monthly_cboTo=[para08_3_Monthly_cboTo]
      ,@para09_0_TabDate=[para09_0_TabDate]
      ,@para09_1_Date=[para09_1_Date]
      ,@para10_0_TabDate_From_To=[para10_0_TabDate_From_To]
      ,@para10_1_Date_From_To_cboFrom=[para10_1_Date_From_To_cboFrom]
      ,@para10_2_Date_From_To_cboTo=[para10_2_Date_From_To_cboTo]
      ,@para11_0_TabYearly=[para11_0_TabYearly]
      ,@para11_1_Yearly_cboYear=[para11_1_Yearly_cboYear]
      ,@para_12_Note=[para_12_Note]
      ,@para_13_Note=[para_13_Note]
      ,@para_14_Note=[para_14_Note]
      ,@para_15_Note=[para_15_Note]
      ,@para_16_Note=[para_16_Note]
      ,@para_17_Note=[para_17_Note]
      ,@para_18_Note=[para_18_Note]
      ,@para_19_Note=[para_19_Note]
      ,@para_20_Note=[para_20_Note]
      ,@para_21_Note=[para_21_Note]
      ,@para_22_Note=[para_22_Note]
      ,@para_23_Note=[para_23_Note]
      ,@para_24_Note=[para_24_Note]
      ,@para_25_Note=[para_25_Note]
  FROM [tbl_OTHER_OPTIONS_FILTER]
  Where [UserID]=@UserID;
--Select from tbl_OTHER_OPTIONS_FILTER -----------
-----Block Do Sth --------------------------------
Declare @FromDate As datetime;
Declare @ToDate As datetime;
Declare @PrintBy As nvarchar(30);

Set @FromDate = @para10_1_Date_From_To_cboFrom;
Set @ToDate = @para10_2_Date_From_To_cboTo;
Select @PrintBy = UserName FROM [dbo].[tbl_useraccount] WHERE [UserID] = @UserID;
SELECT [customerOrderID]
      ,[InvoiceNo]
      ,[fullName]
      ,[idType]
      ,[Nationality]
      ,[Gender]
      ,[userID]
      ,[UserName]
      ,[Mobile]
      ,[usdTokhr]
      ,[usdToTHB]
      ,[orderDetailsID]
      ,[prdID]
      ,[orderDate]
      ,[unitType]
      ,[unitPrice]
      ,[orderQty]
      ,[totalAmt]
      ,[PytStatus]
      ,[roomtableID]
      ,[ReservationID]
      ,[NumPrinted]
      ,[NumPrintedInv]
      ,[percDisc]
      ,[AmtDisc]
      ,[orderTime]
      ,[VoidDate]
      ,[BuyingTotal]
      ,[TaxNote]
      ,[OrderDetailDescription]
      ,[PrdNameEng]
      ,[PrdNameKh]
 ,@PrintBy As PrintBy
 ,@FromDate As FromDate
 ,@ToDate As ToDate
  FROM [dbo].[View_OTHER_OPION_Report_SaleBy_Detail]
Where [userID] IN(SELECT [EmployeeID] FROM [dbo].[tbl_OTHER_OPTIONS_Employee_Set] WHERE [UserID] = @UserID)
And CONVERT (DATETIME, REPLACE(CONVERT (VARCHAR(10), [orderDate], 111), '/', '-')) between @FromDate and @ToDate
-----Block Do Sth -------------------------------- 
 
 End

   END




























GO
/****** Object:  StoredProcedure [dbo].[Pro_OTHER_OPTION_Report_SaleBy_Invoice]    Script Date: 2/19/2025 5:07:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



























CREATE PROCEDURE [dbo].[Pro_OTHER_OPTION_Report_SaleBy_Invoice]
 @ID As bigint
 ,@UserID as bigint
 ,@EmployeeID As bigint
 ,@RunID As varchar(100)
AS
BEGIN

if @ID =-2
 Begin
-----Block Select Blank--------------------------------
  -- Insert statements for procedure here
SELECT [customerOrderID]
      ,[orderDate]
      ,[Time]
      ,[totalAmount]
      ,[percentDiscount]
      ,[amtDiscount]
      ,[amtOrderExpend]
      ,[netAmount]
      ,[Rate_VAT]
      ,[TotalForVAT]
      ,[VAT]
      ,[grandTotal]
      ,[amtReceived]
      ,[amtReturn]
      ,[amtOwed]
      ,[usdTokhr]
      ,[usdToTHB]
      ,[InvoiceNo]
      ,[DescripitonTerm]
      ,[fullName]
      ,[idType]
      ,[Nationality]
      ,[Gender]
      ,[userID]
      ,[UserName]
	  ,[Mobile]
,'' As PrintBy
,getdate() As FromDate
,getdate() As ToDate
  FROM [dbo].[View_OTHER_OPION_Report_SaleBy_Invoice]
Where 1=-2 ;
-----Block Select Blank--------------------------------
 End
Else
 Begin
--Select from tbl_OTHER_OPTIONS_FILTER -----------
---Declare @ID As bigint;
---Declare @UserID as bigint;
Declare @para_ReportName As nvarchar(100);
Declare @para_Stored_Pro_Name As nvarchar(100);
---Declare @EmployeeID As bigint;
---Declare @RunID as varchar(100)
Declare @para_Calculate_Time As int;
Declare @paraEmployee_Set As int;
Declare @para01_cmdCalculate As int;
Declare @para02_cmdViewReportMS As int;
Declare @para03_cmdViewReportCR As int;
Declare @para04_cmdExport As int;
Declare @para05_cmdImport As int;
Declare @para06_cboGroup As nvarchar(50);
Declare @para07_cboName As nvarchar(50);
Declare @para08_0_TabMonthly As int;
Declare @para08_1_cboMonthly As datetime;
Declare @para08_2_Monthly_cboFrom As datetime;
Declare @para08_3_Monthly_cboTo As datetime;
Declare @para09_0_TabDate As int;
Declare @para09_1_Date As datetime;
Declare @para10_0_TabDate_From_To As int;
Declare @para10_1_Date_From_To_cboFrom As datetime;
Declare @para10_2_Date_From_To_cboTo As datetime;
Declare @para11_0_TabYearly As int;
Declare @para11_1_Yearly_cboYear As datetime;
Declare @para_12_Note As nvarchar(50);
Declare @para_13_Note As nvarchar(50);
Declare @para_14_Note As nvarchar(50);
Declare @para_15_Note As nvarchar(50);
Declare @para_16_Note As nvarchar(50);
Declare @para_17_Note As nvarchar(50);
Declare @para_18_Note As nvarchar(50);
Declare @para_19_Note As nvarchar(50);
Declare @para_20_Note As nvarchar(50);
Declare @para_21_Note As nvarchar(50);
Declare @para_22_Note As nvarchar(50);
Declare @para_23_Note As nvarchar(50);
Declare @para_24_Note As nvarchar(50);
Declare @para_25_Note As nvarchar(50);

SELECT 
--    @ID = [ID]
--    ,@UserID = [UserID]
      @para_ReportName=[para_ReportName]
      ,@para_Stored_Pro_Name=[para_Stored_Pro_Name]
--      ,@EmployeeID = [EmployeeID]
--  ,@RunID=[RunID]
      ,@para_Calculate_Time=[para_Calculate_Time]
      ,@paraEmployee_Set=[paraEmployee_Set]
      ,@para01_cmdCalculate=[para01_cmdCalculate]
      ,@para02_cmdViewReportMS=[para02_cmdViewReportMS]
      ,@para03_cmdViewReportCR=[para03_cmdViewReportCR]
      ,@para04_cmdExport=[para04_cmdExport]
      ,@para05_cmdImport=[para05_cmdImport]
      ,@para06_cboGroup=[para06_cboGroup]
      ,@para07_cboName=[para07_cboName]
      ,@para08_0_TabMonthly=[para08_0_TabMonthly]
      ,@para08_1_cboMonthly=[para08_1_cboMonthly]
      ,@para08_2_Monthly_cboFrom=[para08_2_Monthly_cboFrom]
      ,@para08_3_Monthly_cboTo=[para08_3_Monthly_cboTo]
      ,@para09_0_TabDate=[para09_0_TabDate]
      ,@para09_1_Date=[para09_1_Date]
      ,@para10_0_TabDate_From_To=[para10_0_TabDate_From_To]
      ,@para10_1_Date_From_To_cboFrom=[para10_1_Date_From_To_cboFrom]
      ,@para10_2_Date_From_To_cboTo=[para10_2_Date_From_To_cboTo]
      ,@para11_0_TabYearly=[para11_0_TabYearly]
      ,@para11_1_Yearly_cboYear=[para11_1_Yearly_cboYear]
      ,@para_12_Note=[para_12_Note]
      ,@para_13_Note=[para_13_Note]
      ,@para_14_Note=[para_14_Note]
      ,@para_15_Note=[para_15_Note]
      ,@para_16_Note=[para_16_Note]
      ,@para_17_Note=[para_17_Note]
      ,@para_18_Note=[para_18_Note]
      ,@para_19_Note=[para_19_Note]
      ,@para_20_Note=[para_20_Note]
      ,@para_21_Note=[para_21_Note]
      ,@para_22_Note=[para_22_Note]
      ,@para_23_Note=[para_23_Note]
      ,@para_24_Note=[para_24_Note]
      ,@para_25_Note=[para_25_Note]
  FROM [tbl_OTHER_OPTIONS_FILTER]
  Where [UserID]=@UserID;
--Select from tbl_OTHER_OPTIONS_FILTER -----------
-----Block Do Sth --------------------------------
Declare @FromDate As datetime;
Declare @ToDate As datetime;
Declare @PrintBy As nvarchar(30);

Set @FromDate = @para10_1_Date_From_To_cboFrom;
Set @ToDate = @para10_2_Date_From_To_cboTo;
Select @PrintBy = UserName FROM [dbo].[tbl_useraccount] WHERE [UserID] = @UserID;
SELECT [customerOrderID]
      ,[orderDate]
      ,[Time]
      ,[totalAmount]
      ,[percentDiscount]
      ,[amtDiscount]
      ,[amtOrderExpend]
      ,[netAmount]
      ,[Rate_VAT]
      ,[TotalForVAT]
      ,[VAT]
      ,[grandTotal]
      ,[amtReceived]
      ,[amtReturn]
      ,[amtOwed]
      ,[usdTokhr]
      ,[usdToTHB]
      ,[InvoiceNo]
      ,[DescripitonTerm]
      ,[fullName]
      ,[idType]
      ,[Nationality]
      ,[Gender]
      ,[userID]
      ,[UserName]
	   ,[Mobile]
 ,@PrintBy As PrintBy
 ,@FromDate As FromDate
 ,@ToDate As ToDate
  FROM [dbo].[View_OTHER_OPION_Report_SaleBy_Invoice]
Where [userID] IN(SELECT [EmployeeID] FROM [dbo].[tbl_OTHER_OPTIONS_Employee_Set] WHERE [UserID] = @UserID)
And CONVERT (DATETIME, REPLACE(CONVERT (VARCHAR(10), [orderDate], 111), '/', '-')) between @FromDate and @ToDate
-----Block Do Sth -------------------------------- 
 
 End

   END



























GO
/****** Object:  Table [dbo].[tbl_OTHER_OPTIONS]    Script Date: 2/19/2025 5:07:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_OTHER_OPTIONS](
	[ID] [bigint] NOT NULL,
	[UserID] [bigint] NULL,
	[para_ReportName] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[para_Stored_Pro_Name] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[EmployeeID] [bigint] NULL,
	[RunID] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[para_Calculate_Time] [int] NULL,
	[paraEmployee_Set] [int] NULL,
	[para01_cmdCalculate] [int] NULL,
	[para02_cmdViewReportMS] [int] NULL,
	[para03_cmdViewReportCR] [int] NULL,
	[para04_cmdExport] [int] NULL,
	[para05_cmdImport] [int] NULL,
	[para06_cboGroup] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[para07_cboName] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[para08_0_TabMonthly] [int] NULL,
	[para08_1_cboMonthly] [datetime] NULL,
	[para08_2_Monthly_cboFrom] [datetime] NULL,
	[para08_3_Monthly_cboTo] [datetime] NULL,
	[para09_0_TabDate] [int] NULL,
	[para09_1_Date] [datetime] NULL,
	[para10_0_TabDate_From_To] [int] NULL,
	[para10_1_Date_From_To_cboFrom] [datetime] NULL,
	[para10_2_Date_From_To_cboTo] [datetime] NULL,
	[para11_0_TabYearly] [int] NULL,
	[para11_1_Yearly_cboYear] [datetime] NULL,
	[para_12_Note] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[para_13_Note] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[para_14_Note] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[para_15_Note] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[para_16_Note] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[para_17_Note] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[para_18_Note] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[para_19_Note] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[para_20_Note] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[para_21_Note] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[para_22_Note] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[para_23_Note] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[para_24_Note] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[para_25_Note] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[para_26_IS_Customers] [int] NULL,
	[para_27_IS_PrdCategory] [int] NULL,
	[para_28_IS_products] [int] NULL,
	[para_29_IS_Sale] [int] NULL,
	[para_30_IS_StockType] [int] NULL,
	[para_31_IS_Suppliers] [int] NULL,
	[para_32_IS_Useraccount] [int] NULL,
 CONSTRAINT [PK_tbl_OTHER_OPTIONS] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_OTHER_OPTIONS_SEARCH]    Script Date: 2/19/2025 5:07:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_OTHER_OPTIONS_SEARCH](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[para_ReportName] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[SearchProduct] [bit] NULL,
	[SearchCategory] [bit] NULL,
	[SearchInvoice] [bit] NULL,
	[SearchCustomer] [bit] NULL,
	[SearchSupplier] [bit] NULL,
	[SearchStock] [bit] NULL,
	[SearchUser] [bit] NULL,
	[SearchDateOnly] [bit] NULL,
	[SearchAdvanceOnly] [bit] NULL,
 CONSTRAINT [PK_tbl_OTHER_OPTIONS_SEARCH] PRIMARY KEY CLUSTERED 
(
	[ID] ASC,
	[para_ReportName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  View [dbo].[View_OTHER_OPION_Report_OwedBy_Invoice]    Script Date: 2/19/2025 5:07:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[View_OTHER_OPION_Report_OwedBy_Invoice]
AS
SELECT        dbo.tbl_customerorder.customerOrderID, dbo.tbl_customerorder.orderDate, dbo.tbl_customerorder.Time, dbo.tbl_customerorder.totalAmount, dbo.tbl_customerorder.percentDiscount, dbo.tbl_customerorder.amtDiscount, 
                         dbo.tbl_customerorder.amtOrderExpend, dbo.tbl_customerorder.netAmount, dbo.tbl_customerorder.Rate_VAT, dbo.tbl_customerorder.TotalForVAT, dbo.tbl_customerorder.VAT, dbo.tbl_customerorder.grandTotal, 
                         dbo.tbl_customerorder.amtReceived, dbo.tbl_customerorder.amtReturn, dbo.tbl_customerorder.amtOwed, dbo.tbl_customerorder.usdTokhr, dbo.tbl_customerorder.usdToTHB, dbo.tbl_customerorder.InvoiceNo, 
                         dbo.tbl_customerorder.DescripitonTerm, dbo.tbl_customers.fullName, dbo.tbl_customers.idType, dbo.tbl_customers.Nationality, dbo.tbl_customers.Gender, dbo.tbl_customerorder.userID, dbo.tbl_useraccount.UserName, 
                         dbo.tbl_customers.Mobile
FROM            dbo.tbl_customerorder INNER JOIN
                         dbo.tbl_customers ON dbo.tbl_customerorder.customerID = dbo.tbl_customers.memberID INNER JOIN
                         dbo.tbl_useraccount ON dbo.tbl_customerorder.userID = dbo.tbl_useraccount.UserID
WHERE        (dbo.tbl_customerorder.amtOwed > 0)

GO
/****** Object:  View [dbo].[View_OTHER_OPION_Report_SaleBy_Detail]    Script Date: 2/19/2025 5:07:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[View_OTHER_OPION_Report_SaleBy_Detail]
AS
SELECT        dbo.tbl_customerorder.customerOrderID, dbo.tbl_customerorder.InvoiceNo, dbo.tbl_customers.fullName, dbo.tbl_customers.idType, dbo.tbl_customers.Nationality, dbo.tbl_customers.Gender, dbo.tbl_customerorder.userID, 
                         dbo.tbl_useraccount.UserName, dbo.tbl_customers.Mobile, dbo.tbl_customerorder.usdTokhr, dbo.tbl_customerorder.usdToTHB, dbo.tbl_orderdetails.orderDetailsID, dbo.tbl_orderdetails.prdID, dbo.tbl_orderdetails.orderDate, 
                         dbo.tbl_orderdetails.unitType, dbo.tbl_orderdetails.unitPrice, dbo.tbl_orderdetails.orderQty, dbo.tbl_orderdetails.totalAmt, dbo.tbl_orderdetails.PytStatus, dbo.tbl_orderdetails.roomtableID, dbo.tbl_orderdetails.ReservationID, 
                         dbo.tbl_orderdetails.NumPrinted, dbo.tbl_orderdetails.NumPrintedInv, dbo.tbl_orderdetails.percDisc, dbo.tbl_orderdetails.AmtDisc, dbo.tbl_orderdetails.orderTime, dbo.tbl_orderdetails.VoidDate, 
                         dbo.tbl_orderdetails.BuyingTotal, dbo.tbl_orderdetails.TaxNote, dbo.tbl_orderdetails.OrderDetailDescription, dbo.tbl_products.PrdNameEng, dbo.tbl_products.PrdNameKh
FROM            dbo.tbl_customerorder INNER JOIN
                         dbo.tbl_customers ON dbo.tbl_customerorder.customerID = dbo.tbl_customers.memberID INNER JOIN
                         dbo.tbl_useraccount ON dbo.tbl_customerorder.userID = dbo.tbl_useraccount.UserID INNER JOIN
                         dbo.tbl_orderdetails ON dbo.tbl_customerorder.customerOrderID = dbo.tbl_orderdetails.customerOrderID INNER JOIN
                         dbo.tbl_products ON dbo.tbl_orderdetails.prdID = dbo.tbl_products.ProductID

GO
/****** Object:  View [dbo].[View_OTHER_OPION_Report_SaleBy_Invoice]    Script Date: 2/19/2025 5:07:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[View_OTHER_OPION_Report_SaleBy_Invoice]
AS
SELECT        dbo.tbl_customerorder.customerOrderID, dbo.tbl_customerorder.orderDate, dbo.tbl_customerorder.Time, dbo.tbl_customerorder.totalAmount, dbo.tbl_customerorder.percentDiscount, dbo.tbl_customerorder.amtDiscount, 
                         dbo.tbl_customerorder.amtOrderExpend, dbo.tbl_customerorder.netAmount, dbo.tbl_customerorder.Rate_VAT, dbo.tbl_customerorder.TotalForVAT, dbo.tbl_customerorder.VAT, dbo.tbl_customerorder.grandTotal, 
                         dbo.tbl_customerorder.amtReceived, dbo.tbl_customerorder.amtReturn, dbo.tbl_customerorder.amtOwed, dbo.tbl_customerorder.usdTokhr, dbo.tbl_customerorder.usdToTHB, dbo.tbl_customerorder.InvoiceNo, 
                         dbo.tbl_customerorder.DescripitonTerm, dbo.tbl_customers.fullName, dbo.tbl_customers.idType, dbo.tbl_customers.Nationality, dbo.tbl_customers.Gender, dbo.tbl_customerorder.userID, dbo.tbl_useraccount.UserName, 
                         dbo.tbl_customers.Mobile
FROM            dbo.tbl_customerorder INNER JOIN
                         dbo.tbl_customers ON dbo.tbl_customerorder.customerID = dbo.tbl_customers.memberID INNER JOIN
                         dbo.tbl_useraccount ON dbo.tbl_customerorder.userID = dbo.tbl_useraccount.UserID

GO
INSERT [dbo].[tbl_OTHER_OPTIONS] ([ID], [UserID], [para_ReportName], [para_Stored_Pro_Name], [EmployeeID], [RunID], [para_Calculate_Time], [paraEmployee_Set], [para01_cmdCalculate], [para02_cmdViewReportMS], [para03_cmdViewReportCR], [para04_cmdExport], [para05_cmdImport], [para06_cboGroup], [para07_cboName], [para08_0_TabMonthly], [para08_1_cboMonthly], [para08_2_Monthly_cboFrom], [para08_3_Monthly_cboTo], [para09_0_TabDate], [para09_1_Date], [para10_0_TabDate_From_To], [para10_1_Date_From_To_cboFrom], [para10_2_Date_From_To_cboTo], [para11_0_TabYearly], [para11_1_Yearly_cboYear], [para_12_Note], [para_13_Note], [para_14_Note], [para_15_Note], [para_16_Note], [para_17_Note], [para_18_Note], [para_19_Note], [para_20_Note], [para_21_Note], [para_22_Note], [para_23_Note], [para_24_Note], [para_25_Note], [para_26_IS_Customers], [para_27_IS_PrdCategory], [para_28_IS_products], [para_29_IS_Sale], [para_30_IS_StockType], [para_31_IS_Suppliers], [para_32_IS_Useraccount]) VALUES (1, NULL, N'1.Pro_OTHER_OPTION_Product_Monthly_Run_One_Time', N'Pro_OTHER_OPTION_Product_Monthly_Run_One_Time', NULL, NULL, 1, 1, 1, 1, 0, 0, 0, N'0', N'1', 1, NULL, NULL, NULL, 0, NULL, 0, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, 0, 1, 0, 0, 0, 0)
INSERT [dbo].[tbl_OTHER_OPTIONS] ([ID], [UserID], [para_ReportName], [para_Stored_Pro_Name], [EmployeeID], [RunID], [para_Calculate_Time], [paraEmployee_Set], [para01_cmdCalculate], [para02_cmdViewReportMS], [para03_cmdViewReportCR], [para04_cmdExport], [para05_cmdImport], [para06_cboGroup], [para07_cboName], [para08_0_TabMonthly], [para08_1_cboMonthly], [para08_2_Monthly_cboFrom], [para08_3_Monthly_cboTo], [para09_0_TabDate], [para09_1_Date], [para10_0_TabDate_From_To], [para10_1_Date_From_To_cboFrom], [para10_2_Date_From_To_cboTo], [para11_0_TabYearly], [para11_1_Yearly_cboYear], [para_12_Note], [para_13_Note], [para_14_Note], [para_15_Note], [para_16_Note], [para_17_Note], [para_18_Note], [para_19_Note], [para_20_Note], [para_21_Note], [para_22_Note], [para_23_Note], [para_24_Note], [para_25_Note], [para_26_IS_Customers], [para_27_IS_PrdCategory], [para_28_IS_products], [para_29_IS_Sale], [para_30_IS_StockType], [para_31_IS_Suppliers], [para_32_IS_Useraccount]) VALUES (2, NULL, N'2.Pro_OTHER_OPTION_Product_List_Monthly_Run_Many_Time', N'Pro_OTHER_OPTION_Product_List_Monthly_Run_Many_Time', NULL, NULL, 2, 1, 1, 1, 0, 0, 0, N'0', N'1', 1, NULL, NULL, NULL, 0, NULL, 0, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, 1, 1, 1, 1, 1, 1)
INSERT [dbo].[tbl_OTHER_OPTIONS] ([ID], [UserID], [para_ReportName], [para_Stored_Pro_Name], [EmployeeID], [RunID], [para_Calculate_Time], [paraEmployee_Set], [para01_cmdCalculate], [para02_cmdViewReportMS], [para03_cmdViewReportCR], [para04_cmdExport], [para05_cmdImport], [para06_cboGroup], [para07_cboName], [para08_0_TabMonthly], [para08_1_cboMonthly], [para08_2_Monthly_cboFrom], [para08_3_Monthly_cboTo], [para09_0_TabDate], [para09_1_Date], [para10_0_TabDate_From_To], [para10_1_Date_From_To_cboFrom], [para10_2_Date_From_To_cboTo], [para11_0_TabYearly], [para11_1_Yearly_cboYear], [para_12_Note], [para_13_Note], [para_14_Note], [para_15_Note], [para_16_Note], [para_17_Note], [para_18_Note], [para_19_Note], [para_20_Note], [para_21_Note], [para_22_Note], [para_23_Note], [para_24_Note], [para_25_Note], [para_26_IS_Customers], [para_27_IS_PrdCategory], [para_28_IS_products], [para_29_IS_Sale], [para_30_IS_StockType], [para_31_IS_Suppliers], [para_32_IS_Useraccount]) VALUES (3, NULL, N'Report_Stock_Unit_product', N'Pro_Report_Stock_Unit_product', NULL, NULL, 1, 0, 1, 1, 0, 0, 0, N'1', N'1', 0, NULL, NULL, NULL, 1, NULL, 0, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, 0, 0, 0, 0, 0, 0)
INSERT [dbo].[tbl_OTHER_OPTIONS] ([ID], [UserID], [para_ReportName], [para_Stored_Pro_Name], [EmployeeID], [RunID], [para_Calculate_Time], [paraEmployee_Set], [para01_cmdCalculate], [para02_cmdViewReportMS], [para03_cmdViewReportCR], [para04_cmdExport], [para05_cmdImport], [para06_cboGroup], [para07_cboName], [para08_0_TabMonthly], [para08_1_cboMonthly], [para08_2_Monthly_cboFrom], [para08_3_Monthly_cboTo], [para09_0_TabDate], [para09_1_Date], [para10_0_TabDate_From_To], [para10_1_Date_From_To_cboFrom], [para10_2_Date_From_To_cboTo], [para11_0_TabYearly], [para11_1_Yearly_cboYear], [para_12_Note], [para_13_Note], [para_14_Note], [para_15_Note], [para_16_Note], [para_17_Note], [para_18_Note], [para_19_Note], [para_20_Note], [para_21_Note], [para_22_Note], [para_23_Note], [para_24_Note], [para_25_Note], [para_26_IS_Customers], [para_27_IS_PrdCategory], [para_28_IS_products], [para_29_IS_Sale], [para_30_IS_StockType], [para_31_IS_Suppliers], [para_32_IS_Useraccount]) VALUES (4, NULL, N'CustomerVisit_Day', N'Pro_OTHER_OPTION_CustomerVisit_By_Day', NULL, NULL, 1, 0, 1, 1, 0, 0, 0, N'1', N'1', 0, NULL, NULL, NULL, 0, NULL, 1, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, 0, 0, 0, 0, 0, 0)
INSERT [dbo].[tbl_OTHER_OPTIONS] ([ID], [UserID], [para_ReportName], [para_Stored_Pro_Name], [EmployeeID], [RunID], [para_Calculate_Time], [paraEmployee_Set], [para01_cmdCalculate], [para02_cmdViewReportMS], [para03_cmdViewReportCR], [para04_cmdExport], [para05_cmdImport], [para06_cboGroup], [para07_cboName], [para08_0_TabMonthly], [para08_1_cboMonthly], [para08_2_Monthly_cboFrom], [para08_3_Monthly_cboTo], [para09_0_TabDate], [para09_1_Date], [para10_0_TabDate_From_To], [para10_1_Date_From_To_cboFrom], [para10_2_Date_From_To_cboTo], [para11_0_TabYearly], [para11_1_Yearly_cboYear], [para_12_Note], [para_13_Note], [para_14_Note], [para_15_Note], [para_16_Note], [para_17_Note], [para_18_Note], [para_19_Note], [para_20_Note], [para_21_Note], [para_22_Note], [para_23_Note], [para_24_Note], [para_25_Note], [para_26_IS_Customers], [para_27_IS_PrdCategory], [para_28_IS_products], [para_29_IS_Sale], [para_30_IS_StockType], [para_31_IS_Suppliers], [para_32_IS_Useraccount]) VALUES (5, NULL, N'CustomerVisit_Month', N'Pro_OTHER_OPTION_CustomerVisit_By_Month', NULL, NULL, 1, 0, 1, 1, 0, 0, 0, N'1', N'1', 1, NULL, NULL, NULL, 0, NULL, 0, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, 0, 0, 0, 0, 0, 0)
INSERT [dbo].[tbl_OTHER_OPTIONS] ([ID], [UserID], [para_ReportName], [para_Stored_Pro_Name], [EmployeeID], [RunID], [para_Calculate_Time], [paraEmployee_Set], [para01_cmdCalculate], [para02_cmdViewReportMS], [para03_cmdViewReportCR], [para04_cmdExport], [para05_cmdImport], [para06_cboGroup], [para07_cboName], [para08_0_TabMonthly], [para08_1_cboMonthly], [para08_2_Monthly_cboFrom], [para08_3_Monthly_cboTo], [para09_0_TabDate], [para09_1_Date], [para10_0_TabDate_From_To], [para10_1_Date_From_To_cboFrom], [para10_2_Date_From_To_cboTo], [para11_0_TabYearly], [para11_1_Yearly_cboYear], [para_12_Note], [para_13_Note], [para_14_Note], [para_15_Note], [para_16_Note], [para_17_Note], [para_18_Note], [para_19_Note], [para_20_Note], [para_21_Note], [para_22_Note], [para_23_Note], [para_24_Note], [para_25_Note], [para_26_IS_Customers], [para_27_IS_PrdCategory], [para_28_IS_products], [para_29_IS_Sale], [para_30_IS_StockType], [para_31_IS_Suppliers], [para_32_IS_Useraccount]) VALUES (6, NULL, N'CustomerVisit_Year', N'Pro_OTHER_OPTION_CustomerVisit_By_Year', NULL, NULL, 1, 0, 1, 1, 0, 0, 0, N'1', N'1', 0, NULL, NULL, NULL, 0, NULL, 0, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, 0, 0, 0, 0, 0, 0)
INSERT [dbo].[tbl_OTHER_OPTIONS] ([ID], [UserID], [para_ReportName], [para_Stored_Pro_Name], [EmployeeID], [RunID], [para_Calculate_Time], [paraEmployee_Set], [para01_cmdCalculate], [para02_cmdViewReportMS], [para03_cmdViewReportCR], [para04_cmdExport], [para05_cmdImport], [para06_cboGroup], [para07_cboName], [para08_0_TabMonthly], [para08_1_cboMonthly], [para08_2_Monthly_cboFrom], [para08_3_Monthly_cboTo], [para09_0_TabDate], [para09_1_Date], [para10_0_TabDate_From_To], [para10_1_Date_From_To_cboFrom], [para10_2_Date_From_To_cboTo], [para11_0_TabYearly], [para11_1_Yearly_cboYear], [para_12_Note], [para_13_Note], [para_14_Note], [para_15_Note], [para_16_Note], [para_17_Note], [para_18_Note], [para_19_Note], [para_20_Note], [para_21_Note], [para_22_Note], [para_23_Note], [para_24_Note], [para_25_Note], [para_26_IS_Customers], [para_27_IS_PrdCategory], [para_28_IS_products], [para_29_IS_Sale], [para_30_IS_StockType], [para_31_IS_Suppliers], [para_32_IS_Useraccount]) VALUES (7, NULL, N'Report_Sale_Daily', N'Pro_OTHER_OPTION_A_SPORTS_Report_Sale_Daily', NULL, NULL, 1, 0, 1, 1, 0, 0, 0, N'1', N'1', 1, NULL, NULL, NULL, 0, NULL, 0, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, 0, 0, 0, 0, 0, 0)
INSERT [dbo].[tbl_OTHER_OPTIONS] ([ID], [UserID], [para_ReportName], [para_Stored_Pro_Name], [EmployeeID], [RunID], [para_Calculate_Time], [paraEmployee_Set], [para01_cmdCalculate], [para02_cmdViewReportMS], [para03_cmdViewReportCR], [para04_cmdExport], [para05_cmdImport], [para06_cboGroup], [para07_cboName], [para08_0_TabMonthly], [para08_1_cboMonthly], [para08_2_Monthly_cboFrom], [para08_3_Monthly_cboTo], [para09_0_TabDate], [para09_1_Date], [para10_0_TabDate_From_To], [para10_1_Date_From_To_cboFrom], [para10_2_Date_From_To_cboTo], [para11_0_TabYearly], [para11_1_Yearly_cboYear], [para_12_Note], [para_13_Note], [para_14_Note], [para_15_Note], [para_16_Note], [para_17_Note], [para_18_Note], [para_19_Note], [para_20_Note], [para_21_Note], [para_22_Note], [para_23_Note], [para_24_Note], [para_25_Note], [para_26_IS_Customers], [para_27_IS_PrdCategory], [para_28_IS_products], [para_29_IS_Sale], [para_30_IS_StockType], [para_31_IS_Suppliers], [para_32_IS_Useraccount]) VALUES (8, NULL, N'Report_Sale_By_Category', N'Pro_OTHER_OPTION_A_SPORTS_Report_Sale_By_Category', NULL, NULL, 1, 0, 1, 1, 0, 0, 0, N'1', N'1', 1, NULL, NULL, NULL, 0, NULL, 0, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, 1, 1, 0, 0, 0, 0)
INSERT [dbo].[tbl_OTHER_OPTIONS] ([ID], [UserID], [para_ReportName], [para_Stored_Pro_Name], [EmployeeID], [RunID], [para_Calculate_Time], [paraEmployee_Set], [para01_cmdCalculate], [para02_cmdViewReportMS], [para03_cmdViewReportCR], [para04_cmdExport], [para05_cmdImport], [para06_cboGroup], [para07_cboName], [para08_0_TabMonthly], [para08_1_cboMonthly], [para08_2_Monthly_cboFrom], [para08_3_Monthly_cboTo], [para09_0_TabDate], [para09_1_Date], [para10_0_TabDate_From_To], [para10_1_Date_From_To_cboFrom], [para10_2_Date_From_To_cboTo], [para11_0_TabYearly], [para11_1_Yearly_cboYear], [para_12_Note], [para_13_Note], [para_14_Note], [para_15_Note], [para_16_Note], [para_17_Note], [para_18_Note], [para_19_Note], [para_20_Note], [para_21_Note], [para_22_Note], [para_23_Note], [para_24_Note], [para_25_Note], [para_26_IS_Customers], [para_27_IS_PrdCategory], [para_28_IS_products], [para_29_IS_Sale], [para_30_IS_StockType], [para_31_IS_Suppliers], [para_32_IS_Useraccount]) VALUES (9, NULL, N'Week_To_Date_Sale_Report', N'Pro_OTHER_OPTION_A_SPORTS_Week_To_Date_Sale_Report', NULL, NULL, 1, 0, 1, 1, 0, 0, 0, N'1', N'1', 1, NULL, NULL, NULL, 0, NULL, 0, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, 1, 1, 0, 0, 0, 0)
INSERT [dbo].[tbl_OTHER_OPTIONS] ([ID], [UserID], [para_ReportName], [para_Stored_Pro_Name], [EmployeeID], [RunID], [para_Calculate_Time], [paraEmployee_Set], [para01_cmdCalculate], [para02_cmdViewReportMS], [para03_cmdViewReportCR], [para04_cmdExport], [para05_cmdImport], [para06_cboGroup], [para07_cboName], [para08_0_TabMonthly], [para08_1_cboMonthly], [para08_2_Monthly_cboFrom], [para08_3_Monthly_cboTo], [para09_0_TabDate], [para09_1_Date], [para10_0_TabDate_From_To], [para10_1_Date_From_To_cboFrom], [para10_2_Date_From_To_cboTo], [para11_0_TabYearly], [para11_1_Yearly_cboYear], [para_12_Note], [para_13_Note], [para_14_Note], [para_15_Note], [para_16_Note], [para_17_Note], [para_18_Note], [para_19_Note], [para_20_Note], [para_21_Note], [para_22_Note], [para_23_Note], [para_24_Note], [para_25_Note], [para_26_IS_Customers], [para_27_IS_PrdCategory], [para_28_IS_products], [para_29_IS_Sale], [para_30_IS_StockType], [para_31_IS_Suppliers], [para_32_IS_Useraccount]) VALUES (21, NULL, N'PARKING_REPORTS_DETAIL', N'Pro_OTHER_OPTION_PARKING_REPORTS_DETAIL', NULL, NULL, 1, 0, 1, 1, 0, 0, 0, N'1', N'1', 0, NULL, NULL, NULL, 0, NULL, 1, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, 1, 1, 0, 0, 0, 0)
INSERT [dbo].[tbl_OTHER_OPTIONS] ([ID], [UserID], [para_ReportName], [para_Stored_Pro_Name], [EmployeeID], [RunID], [para_Calculate_Time], [paraEmployee_Set], [para01_cmdCalculate], [para02_cmdViewReportMS], [para03_cmdViewReportCR], [para04_cmdExport], [para05_cmdImport], [para06_cboGroup], [para07_cboName], [para08_0_TabMonthly], [para08_1_cboMonthly], [para08_2_Monthly_cboFrom], [para08_3_Monthly_cboTo], [para09_0_TabDate], [para09_1_Date], [para10_0_TabDate_From_To], [para10_1_Date_From_To_cboFrom], [para10_2_Date_From_To_cboTo], [para11_0_TabYearly], [para11_1_Yearly_cboYear], [para_12_Note], [para_13_Note], [para_14_Note], [para_15_Note], [para_16_Note], [para_17_Note], [para_18_Note], [para_19_Note], [para_20_Note], [para_21_Note], [para_22_Note], [para_23_Note], [para_24_Note], [para_25_Note], [para_26_IS_Customers], [para_27_IS_PrdCategory], [para_28_IS_products], [para_29_IS_Sale], [para_30_IS_StockType], [para_31_IS_Suppliers], [para_32_IS_Useraccount]) VALUES (22, NULL, N'PARKING_REPORTS_SUMMARY', N'Pro_OTHER_OPTION_PARKING_REPORTS_DETAIL', NULL, NULL, 1, 0, 1, 1, 0, 0, 0, N'1', N'1', 0, NULL, NULL, NULL, 0, NULL, 1, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, 1, 1, 0, 0, 0, 0)
INSERT [dbo].[tbl_OTHER_OPTIONS] ([ID], [UserID], [para_ReportName], [para_Stored_Pro_Name], [EmployeeID], [RunID], [para_Calculate_Time], [paraEmployee_Set], [para01_cmdCalculate], [para02_cmdViewReportMS], [para03_cmdViewReportCR], [para04_cmdExport], [para05_cmdImport], [para06_cboGroup], [para07_cboName], [para08_0_TabMonthly], [para08_1_cboMonthly], [para08_2_Monthly_cboFrom], [para08_3_Monthly_cboTo], [para09_0_TabDate], [para09_1_Date], [para10_0_TabDate_From_To], [para10_1_Date_From_To_cboFrom], [para10_2_Date_From_To_cboTo], [para11_0_TabYearly], [para11_1_Yearly_cboYear], [para_12_Note], [para_13_Note], [para_14_Note], [para_15_Note], [para_16_Note], [para_17_Note], [para_18_Note], [para_19_Note], [para_20_Note], [para_21_Note], [para_22_Note], [para_23_Note], [para_24_Note], [para_25_Note], [para_26_IS_Customers], [para_27_IS_PrdCategory], [para_28_IS_products], [para_29_IS_Sale], [para_30_IS_StockType], [para_31_IS_Suppliers], [para_32_IS_Useraccount]) VALUES (23, NULL, N'PARKING_REPORTS_DETAIL_IN', N'Pro_OTHER_OPTION_PARKING_REPORTS_DETAIL', NULL, NULL, 1, 0, 1, 1, 0, 0, 0, N'1', N'1', 0, NULL, NULL, NULL, 0, NULL, 1, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, 1, 1, 0, 0, 0, 0)
INSERT [dbo].[tbl_OTHER_OPTIONS] ([ID], [UserID], [para_ReportName], [para_Stored_Pro_Name], [EmployeeID], [RunID], [para_Calculate_Time], [paraEmployee_Set], [para01_cmdCalculate], [para02_cmdViewReportMS], [para03_cmdViewReportCR], [para04_cmdExport], [para05_cmdImport], [para06_cboGroup], [para07_cboName], [para08_0_TabMonthly], [para08_1_cboMonthly], [para08_2_Monthly_cboFrom], [para08_3_Monthly_cboTo], [para09_0_TabDate], [para09_1_Date], [para10_0_TabDate_From_To], [para10_1_Date_From_To_cboFrom], [para10_2_Date_From_To_cboTo], [para11_0_TabYearly], [para11_1_Yearly_cboYear], [para_12_Note], [para_13_Note], [para_14_Note], [para_15_Note], [para_16_Note], [para_17_Note], [para_18_Note], [para_19_Note], [para_20_Note], [para_21_Note], [para_22_Note], [para_23_Note], [para_24_Note], [para_25_Note], [para_26_IS_Customers], [para_27_IS_PrdCategory], [para_28_IS_products], [para_29_IS_Sale], [para_30_IS_StockType], [para_31_IS_Suppliers], [para_32_IS_Useraccount]) VALUES (24, NULL, N'PARKING_REPORTS_DETAIL_OUT', N'Pro_OTHER_OPTION_PARKING_REPORTS_DETAIL', NULL, NULL, 1, 0, 1, 1, 0, 0, 0, N'1', N'1', 0, NULL, NULL, NULL, 0, NULL, 1, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, 1, 1, 0, 0, 0, 0)
INSERT [dbo].[tbl_OTHER_OPTIONS] ([ID], [UserID], [para_ReportName], [para_Stored_Pro_Name], [EmployeeID], [RunID], [para_Calculate_Time], [paraEmployee_Set], [para01_cmdCalculate], [para02_cmdViewReportMS], [para03_cmdViewReportCR], [para04_cmdExport], [para05_cmdImport], [para06_cboGroup], [para07_cboName], [para08_0_TabMonthly], [para08_1_cboMonthly], [para08_2_Monthly_cboFrom], [para08_3_Monthly_cboTo], [para09_0_TabDate], [para09_1_Date], [para10_0_TabDate_From_To], [para10_1_Date_From_To_cboFrom], [para10_2_Date_From_To_cboTo], [para11_0_TabYearly], [para11_1_Yearly_cboYear], [para_12_Note], [para_13_Note], [para_14_Note], [para_15_Note], [para_16_Note], [para_17_Note], [para_18_Note], [para_19_Note], [para_20_Note], [para_21_Note], [para_22_Note], [para_23_Note], [para_24_Note], [para_25_Note], [para_26_IS_Customers], [para_27_IS_PrdCategory], [para_28_IS_products], [para_29_IS_Sale], [para_30_IS_StockType], [para_31_IS_Suppliers], [para_32_IS_Useraccount]) VALUES (25, NULL, N'PARKING_REPORTS_DETAIL_INOUT', N'Pro_OTHER_OPTION_PARKING_REPORTS_DETAIL', NULL, NULL, 1, 0, 1, 1, 0, 0, 0, N'1', N'1', 0, NULL, NULL, NULL, 0, NULL, 1, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, 1, 1, 0, 0, 0, 0)
INSERT [dbo].[tbl_OTHER_OPTIONS] ([ID], [UserID], [para_ReportName], [para_Stored_Pro_Name], [EmployeeID], [RunID], [para_Calculate_Time], [paraEmployee_Set], [para01_cmdCalculate], [para02_cmdViewReportMS], [para03_cmdViewReportCR], [para04_cmdExport], [para05_cmdImport], [para06_cboGroup], [para07_cboName], [para08_0_TabMonthly], [para08_1_cboMonthly], [para08_2_Monthly_cboFrom], [para08_3_Monthly_cboTo], [para09_0_TabDate], [para09_1_Date], [para10_0_TabDate_From_To], [para10_1_Date_From_To_cboFrom], [para10_2_Date_From_To_cboTo], [para11_0_TabYearly], [para11_1_Yearly_cboYear], [para_12_Note], [para_13_Note], [para_14_Note], [para_15_Note], [para_16_Note], [para_17_Note], [para_18_Note], [para_19_Note], [para_20_Note], [para_21_Note], [para_22_Note], [para_23_Note], [para_24_Note], [para_25_Note], [para_26_IS_Customers], [para_27_IS_PrdCategory], [para_28_IS_products], [para_29_IS_Sale], [para_30_IS_StockType], [para_31_IS_Suppliers], [para_32_IS_Useraccount]) VALUES (26, NULL, N'PARKING_REPORTS_DETAIL_IN_PARKING_ONLY', N'Pro_OTHER_OPTION_PARKING_REPORTS_DETAIL', NULL, NULL, 1, 0, 1, 1, 0, 0, 0, N'1', N'1', 0, NULL, NULL, NULL, 0, NULL, 1, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, 1, 1, 0, 0, 0, 0)
INSERT [dbo].[tbl_OTHER_OPTIONS] ([ID], [UserID], [para_ReportName], [para_Stored_Pro_Name], [EmployeeID], [RunID], [para_Calculate_Time], [paraEmployee_Set], [para01_cmdCalculate], [para02_cmdViewReportMS], [para03_cmdViewReportCR], [para04_cmdExport], [para05_cmdImport], [para06_cboGroup], [para07_cboName], [para08_0_TabMonthly], [para08_1_cboMonthly], [para08_2_Monthly_cboFrom], [para08_3_Monthly_cboTo], [para09_0_TabDate], [para09_1_Date], [para10_0_TabDate_From_To], [para10_1_Date_From_To_cboFrom], [para10_2_Date_From_To_cboTo], [para11_0_TabYearly], [para11_1_Yearly_cboYear], [para_12_Note], [para_13_Note], [para_14_Note], [para_15_Note], [para_16_Note], [para_17_Note], [para_18_Note], [para_19_Note], [para_20_Note], [para_21_Note], [para_22_Note], [para_23_Note], [para_24_Note], [para_25_Note], [para_26_IS_Customers], [para_27_IS_PrdCategory], [para_28_IS_products], [para_29_IS_Sale], [para_30_IS_StockType], [para_31_IS_Suppliers], [para_32_IS_Useraccount]) VALUES (81, NULL, N'Pawn_Tax_Monthly', N'Pro_OTHER_OPTION_Pawn_Tax_Monthly,Pro_OTHER_OPTION_Pawn_Tax_Monthly_Expend', NULL, NULL, 1, 0, 1, 1, 0, 0, 0, N'1', N'1', 1, NULL, NULL, NULL, 0, NULL, 0, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, 1, 1, 0, 0, 0, 0)
INSERT [dbo].[tbl_OTHER_OPTIONS] ([ID], [UserID], [para_ReportName], [para_Stored_Pro_Name], [EmployeeID], [RunID], [para_Calculate_Time], [paraEmployee_Set], [para01_cmdCalculate], [para02_cmdViewReportMS], [para03_cmdViewReportCR], [para04_cmdExport], [para05_cmdImport], [para06_cboGroup], [para07_cboName], [para08_0_TabMonthly], [para08_1_cboMonthly], [para08_2_Monthly_cboFrom], [para08_3_Monthly_cboTo], [para09_0_TabDate], [para09_1_Date], [para10_0_TabDate_From_To], [para10_1_Date_From_To_cboFrom], [para10_2_Date_From_To_cboTo], [para11_0_TabYearly], [para11_1_Yearly_cboYear], [para_12_Note], [para_13_Note], [para_14_Note], [para_15_Note], [para_16_Note], [para_17_Note], [para_18_Note], [para_19_Note], [para_20_Note], [para_21_Note], [para_22_Note], [para_23_Note], [para_24_Note], [para_25_Note], [para_26_IS_Customers], [para_27_IS_PrdCategory], [para_28_IS_products], [para_29_IS_Sale], [para_30_IS_StockType], [para_31_IS_Suppliers], [para_32_IS_Useraccount]) VALUES (82, NULL, N'PAWN_TAX_FORM_Yearly', N'Pro_OTHER_OPTION_PAWN_TAX_FORM_Yearly', NULL, NULL, 1, 0, 1, 1, 0, 0, 0, N'1', N'1', 0, NULL, NULL, NULL, 0, NULL, 0, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, 1, 1, 0, 0, 0, 0)
INSERT [dbo].[tbl_OTHER_OPTIONS] ([ID], [UserID], [para_ReportName], [para_Stored_Pro_Name], [EmployeeID], [RunID], [para_Calculate_Time], [paraEmployee_Set], [para01_cmdCalculate], [para02_cmdViewReportMS], [para03_cmdViewReportCR], [para04_cmdExport], [para05_cmdImport], [para06_cboGroup], [para07_cboName], [para08_0_TabMonthly], [para08_1_cboMonthly], [para08_2_Monthly_cboFrom], [para08_3_Monthly_cboTo], [para09_0_TabDate], [para09_1_Date], [para10_0_TabDate_From_To], [para10_1_Date_From_To_cboFrom], [para10_2_Date_From_To_cboTo], [para11_0_TabYearly], [para11_1_Yearly_cboYear], [para_12_Note], [para_13_Note], [para_14_Note], [para_15_Note], [para_16_Note], [para_17_Note], [para_18_Note], [para_19_Note], [para_20_Note], [para_21_Note], [para_22_Note], [para_23_Note], [para_24_Note], [para_25_Note], [para_26_IS_Customers], [para_27_IS_PrdCategory], [para_28_IS_products], [para_29_IS_Sale], [para_30_IS_StockType], [para_31_IS_Suppliers], [para_32_IS_Useraccount]) VALUES (83, NULL, N'PAWN_TAX_FORM_P101', N'Pro_OTHER_OPTION_PAWN_TAX_FORM_TRS01', NULL, NULL, 1, 0, 1, 1, 0, 0, 0, N'1', N'1', 1, NULL, NULL, NULL, 0, NULL, 0, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, 1, 1, 0, 0, 0, 0)
INSERT [dbo].[tbl_OTHER_OPTIONS] ([ID], [UserID], [para_ReportName], [para_Stored_Pro_Name], [EmployeeID], [RunID], [para_Calculate_Time], [paraEmployee_Set], [para01_cmdCalculate], [para02_cmdViewReportMS], [para03_cmdViewReportCR], [para04_cmdExport], [para05_cmdImport], [para06_cboGroup], [para07_cboName], [para08_0_TabMonthly], [para08_1_cboMonthly], [para08_2_Monthly_cboFrom], [para08_3_Monthly_cboTo], [para09_0_TabDate], [para09_1_Date], [para10_0_TabDate_From_To], [para10_1_Date_From_To_cboFrom], [para10_2_Date_From_To_cboTo], [para11_0_TabYearly], [para11_1_Yearly_cboYear], [para_12_Note], [para_13_Note], [para_14_Note], [para_15_Note], [para_16_Note], [para_17_Note], [para_18_Note], [para_19_Note], [para_20_Note], [para_21_Note], [para_22_Note], [para_23_Note], [para_24_Note], [para_25_Note], [para_26_IS_Customers], [para_27_IS_PrdCategory], [para_28_IS_products], [para_29_IS_Sale], [para_30_IS_StockType], [para_31_IS_Suppliers], [para_32_IS_Useraccount]) VALUES (84, NULL, N'PAWN_TAX_FORM_TRS01', N'Pro_OTHER_OPTION_PAWN_TAX_FORM_TRS01', NULL, NULL, 1, 0, 1, 1, 0, 0, 0, N'1', N'1', 1, NULL, NULL, NULL, 0, NULL, 0, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, 1, 1, 0, 0, 0, 0)
INSERT [dbo].[tbl_OTHER_OPTIONS] ([ID], [UserID], [para_ReportName], [para_Stored_Pro_Name], [EmployeeID], [RunID], [para_Calculate_Time], [paraEmployee_Set], [para01_cmdCalculate], [para02_cmdViewReportMS], [para03_cmdViewReportCR], [para04_cmdExport], [para05_cmdImport], [para06_cboGroup], [para07_cboName], [para08_0_TabMonthly], [para08_1_cboMonthly], [para08_2_Monthly_cboFrom], [para08_3_Monthly_cboTo], [para09_0_TabDate], [para09_1_Date], [para10_0_TabDate_From_To], [para10_1_Date_From_To_cboFrom], [para10_2_Date_From_To_cboTo], [para11_0_TabYearly], [para11_1_Yearly_cboYear], [para_12_Note], [para_13_Note], [para_14_Note], [para_15_Note], [para_16_Note], [para_17_Note], [para_18_Note], [para_19_Note], [para_20_Note], [para_21_Note], [para_22_Note], [para_23_Note], [para_24_Note], [para_25_Note], [para_26_IS_Customers], [para_27_IS_PrdCategory], [para_28_IS_products], [para_29_IS_Sale], [para_30_IS_StockType], [para_31_IS_Suppliers], [para_32_IS_Useraccount]) VALUES (85, NULL, N'PAWN_TAX_FORM_Buy', N'Pro_OTHER_OPTION_PAWN_TAX_FORM_Buy', NULL, NULL, 1, 0, 1, 1, 0, 0, 0, N'1', N'1', 1, NULL, NULL, NULL, 0, NULL, 0, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, 1, 1, 0, 0, 0, 0)
INSERT [dbo].[tbl_OTHER_OPTIONS] ([ID], [UserID], [para_ReportName], [para_Stored_Pro_Name], [EmployeeID], [RunID], [para_Calculate_Time], [paraEmployee_Set], [para01_cmdCalculate], [para02_cmdViewReportMS], [para03_cmdViewReportCR], [para04_cmdExport], [para05_cmdImport], [para06_cboGroup], [para07_cboName], [para08_0_TabMonthly], [para08_1_cboMonthly], [para08_2_Monthly_cboFrom], [para08_3_Monthly_cboTo], [para09_0_TabDate], [para09_1_Date], [para10_0_TabDate_From_To], [para10_1_Date_From_To_cboFrom], [para10_2_Date_From_To_cboTo], [para11_0_TabYearly], [para11_1_Yearly_cboYear], [para_12_Note], [para_13_Note], [para_14_Note], [para_15_Note], [para_16_Note], [para_17_Note], [para_18_Note], [para_19_Note], [para_20_Note], [para_21_Note], [para_22_Note], [para_23_Note], [para_24_Note], [para_25_Note], [para_26_IS_Customers], [para_27_IS_PrdCategory], [para_28_IS_products], [para_29_IS_Sale], [para_30_IS_StockType], [para_31_IS_Suppliers], [para_32_IS_Useraccount]) VALUES (86, NULL, N'PAWN_TAX_FORM_Sale', N'Pro_OTHER_OPTION_PAWN_TAX_FORM_Sale', NULL, NULL, 1, 0, 1, 1, 0, 0, 0, N'1', N'1', 1, NULL, NULL, NULL, 0, NULL, 0, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, 1, 1, 0, 0, 0, 0)
INSERT [dbo].[tbl_OTHER_OPTIONS] ([ID], [UserID], [para_ReportName], [para_Stored_Pro_Name], [EmployeeID], [RunID], [para_Calculate_Time], [paraEmployee_Set], [para01_cmdCalculate], [para02_cmdViewReportMS], [para03_cmdViewReportCR], [para04_cmdExport], [para05_cmdImport], [para06_cboGroup], [para07_cboName], [para08_0_TabMonthly], [para08_1_cboMonthly], [para08_2_Monthly_cboFrom], [para08_3_Monthly_cboTo], [para09_0_TabDate], [para09_1_Date], [para10_0_TabDate_From_To], [para10_1_Date_From_To_cboFrom], [para10_2_Date_From_To_cboTo], [para11_0_TabYearly], [para11_1_Yearly_cboYear], [para_12_Note], [para_13_Note], [para_14_Note], [para_15_Note], [para_16_Note], [para_17_Note], [para_18_Note], [para_19_Note], [para_20_Note], [para_21_Note], [para_22_Note], [para_23_Note], [para_24_Note], [para_25_Note], [para_26_IS_Customers], [para_27_IS_PrdCategory], [para_28_IS_products], [para_29_IS_Sale], [para_30_IS_StockType], [para_31_IS_Suppliers], [para_32_IS_Useraccount]) VALUES (87, NULL, N'PAWN_TAX_FORM_MEF', N'Pro_OTHER_OPTION_PAWN_TAX_FORM_Sale', NULL, NULL, 1, 0, 1, 1, 0, 0, 0, N'1', N'1', 1, NULL, NULL, NULL, 0, NULL, 0, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, 1, 1, 0, 0, 0, 0)
INSERT [dbo].[tbl_OTHER_OPTIONS] ([ID], [UserID], [para_ReportName], [para_Stored_Pro_Name], [EmployeeID], [RunID], [para_Calculate_Time], [paraEmployee_Set], [para01_cmdCalculate], [para02_cmdViewReportMS], [para03_cmdViewReportCR], [para04_cmdExport], [para05_cmdImport], [para06_cboGroup], [para07_cboName], [para08_0_TabMonthly], [para08_1_cboMonthly], [para08_2_Monthly_cboFrom], [para08_3_Monthly_cboTo], [para09_0_TabDate], [para09_1_Date], [para10_0_TabDate_From_To], [para10_1_Date_From_To_cboFrom], [para10_2_Date_From_To_cboTo], [para11_0_TabYearly], [para11_1_Yearly_cboYear], [para_12_Note], [para_13_Note], [para_14_Note], [para_15_Note], [para_16_Note], [para_17_Note], [para_18_Note], [para_19_Note], [para_20_Note], [para_21_Note], [para_22_Note], [para_23_Note], [para_24_Note], [para_25_Note], [para_26_IS_Customers], [para_27_IS_PrdCategory], [para_28_IS_products], [para_29_IS_Sale], [para_30_IS_StockType], [para_31_IS_Suppliers], [para_32_IS_Useraccount]) VALUES (88, NULL, N'PAWN_TAX_FORM_POLICE', N'Pro_OTHER_OPTION_PAWN_TAX_FORM_Sale', NULL, NULL, 1, 0, 1, 1, 0, 0, 0, N'1', N'1', 1, NULL, NULL, NULL, 0, NULL, 0, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, 1, 1, 0, 0, 0, 0)
INSERT [dbo].[tbl_OTHER_OPTIONS] ([ID], [UserID], [para_ReportName], [para_Stored_Pro_Name], [EmployeeID], [RunID], [para_Calculate_Time], [paraEmployee_Set], [para01_cmdCalculate], [para02_cmdViewReportMS], [para03_cmdViewReportCR], [para04_cmdExport], [para05_cmdImport], [para06_cboGroup], [para07_cboName], [para08_0_TabMonthly], [para08_1_cboMonthly], [para08_2_Monthly_cboFrom], [para08_3_Monthly_cboTo], [para09_0_TabDate], [para09_1_Date], [para10_0_TabDate_From_To], [para10_1_Date_From_To_cboFrom], [para10_2_Date_From_To_cboTo], [para11_0_TabYearly], [para11_1_Yearly_cboYear], [para_12_Note], [para_13_Note], [para_14_Note], [para_15_Note], [para_16_Note], [para_17_Note], [para_18_Note], [para_19_Note], [para_20_Note], [para_21_Note], [para_22_Note], [para_23_Note], [para_24_Note], [para_25_Note], [para_26_IS_Customers], [para_27_IS_PrdCategory], [para_28_IS_products], [para_29_IS_Sale], [para_30_IS_StockType], [para_31_IS_Suppliers], [para_32_IS_Useraccount]) VALUES (89, NULL, N'Product_List_Total_Pro_In_Stock', N'Pro_OTHER_OPTION_Product_List_Total_In_Stock', NULL, NULL, 2, 1, 1, 1, 0, 0, 0, N'0', N'1', 1, NULL, NULL, NULL, 0, NULL, 0, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, 1, 1, 1, 1, 1, 1)
INSERT [dbo].[tbl_OTHER_OPTIONS] ([ID], [UserID], [para_ReportName], [para_Stored_Pro_Name], [EmployeeID], [RunID], [para_Calculate_Time], [paraEmployee_Set], [para01_cmdCalculate], [para02_cmdViewReportMS], [para03_cmdViewReportCR], [para04_cmdExport], [para05_cmdImport], [para06_cboGroup], [para07_cboName], [para08_0_TabMonthly], [para08_1_cboMonthly], [para08_2_Monthly_cboFrom], [para08_3_Monthly_cboTo], [para09_0_TabDate], [para09_1_Date], [para10_0_TabDate_From_To], [para10_1_Date_From_To_cboFrom], [para10_2_Date_From_To_cboTo], [para11_0_TabYearly], [para11_1_Yearly_cboYear], [para_12_Note], [para_13_Note], [para_14_Note], [para_15_Note], [para_16_Note], [para_17_Note], [para_18_Note], [para_19_Note], [para_20_Note], [para_21_Note], [para_22_Note], [para_23_Note], [para_24_Note], [para_25_Note], [para_26_IS_Customers], [para_27_IS_PrdCategory], [para_28_IS_products], [para_29_IS_Sale], [para_30_IS_StockType], [para_31_IS_Suppliers], [para_32_IS_Useraccount]) VALUES (90, NULL, N'Sale_Report', N'Pro_OTHER_OPTION_Report_SaleBy_Invoice', NULL, NULL, 1, 1, 1, 1, 0, 0, 0, N'0', N'1', 0, NULL, NULL, NULL, 0, NULL, 1, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tbl_OTHER_OPTIONS] ([ID], [UserID], [para_ReportName], [para_Stored_Pro_Name], [EmployeeID], [RunID], [para_Calculate_Time], [paraEmployee_Set], [para01_cmdCalculate], [para02_cmdViewReportMS], [para03_cmdViewReportCR], [para04_cmdExport], [para05_cmdImport], [para06_cboGroup], [para07_cboName], [para08_0_TabMonthly], [para08_1_cboMonthly], [para08_2_Monthly_cboFrom], [para08_3_Monthly_cboTo], [para09_0_TabDate], [para09_1_Date], [para10_0_TabDate_From_To], [para10_1_Date_From_To_cboFrom], [para10_2_Date_From_To_cboTo], [para11_0_TabYearly], [para11_1_Yearly_cboYear], [para_12_Note], [para_13_Note], [para_14_Note], [para_15_Note], [para_16_Note], [para_17_Note], [para_18_Note], [para_19_Note], [para_20_Note], [para_21_Note], [para_22_Note], [para_23_Note], [para_24_Note], [para_25_Note], [para_26_IS_Customers], [para_27_IS_PrdCategory], [para_28_IS_products], [para_29_IS_Sale], [para_30_IS_StockType], [para_31_IS_Suppliers], [para_32_IS_Useraccount]) VALUES (91, NULL, N'Sale_Report_Detail', N'Pro_OTHER_OPTION_Report_SaleBy_Detail', NULL, NULL, 1, 1, 1, 1, 0, 0, 0, N'0', N'1', 0, NULL, NULL, NULL, 0, NULL, 1, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tbl_OTHER_OPTIONS] ([ID], [UserID], [para_ReportName], [para_Stored_Pro_Name], [EmployeeID], [RunID], [para_Calculate_Time], [paraEmployee_Set], [para01_cmdCalculate], [para02_cmdViewReportMS], [para03_cmdViewReportCR], [para04_cmdExport], [para05_cmdImport], [para06_cboGroup], [para07_cboName], [para08_0_TabMonthly], [para08_1_cboMonthly], [para08_2_Monthly_cboFrom], [para08_3_Monthly_cboTo], [para09_0_TabDate], [para09_1_Date], [para10_0_TabDate_From_To], [para10_1_Date_From_To_cboFrom], [para10_2_Date_From_To_cboTo], [para11_0_TabYearly], [para11_1_Yearly_cboYear], [para_12_Note], [para_13_Note], [para_14_Note], [para_15_Note], [para_16_Note], [para_17_Note], [para_18_Note], [para_19_Note], [para_20_Note], [para_21_Note], [para_22_Note], [para_23_Note], [para_24_Note], [para_25_Note], [para_26_IS_Customers], [para_27_IS_PrdCategory], [para_28_IS_products], [para_29_IS_Sale], [para_30_IS_StockType], [para_31_IS_Suppliers], [para_32_IS_Useraccount]) VALUES (92, NULL, N'Customers', N'Pro_OTHER_OPTION_Report_CustomerList', NULL, NULL, 1, 1, 1, 1, 0, 0, 0, N'0', N'1', 0, NULL, NULL, NULL, 0, NULL, 1, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tbl_OTHER_OPTIONS] ([ID], [UserID], [para_ReportName], [para_Stored_Pro_Name], [EmployeeID], [RunID], [para_Calculate_Time], [paraEmployee_Set], [para01_cmdCalculate], [para02_cmdViewReportMS], [para03_cmdViewReportCR], [para04_cmdExport], [para05_cmdImport], [para06_cboGroup], [para07_cboName], [para08_0_TabMonthly], [para08_1_cboMonthly], [para08_2_Monthly_cboFrom], [para08_3_Monthly_cboTo], [para09_0_TabDate], [para09_1_Date], [para10_0_TabDate_From_To], [para10_1_Date_From_To_cboFrom], [para10_2_Date_From_To_cboTo], [para11_0_TabYearly], [para11_1_Yearly_cboYear], [para_12_Note], [para_13_Note], [para_14_Note], [para_15_Note], [para_16_Note], [para_17_Note], [para_18_Note], [para_19_Note], [para_20_Note], [para_21_Note], [para_22_Note], [para_23_Note], [para_24_Note], [para_25_Note], [para_26_IS_Customers], [para_27_IS_PrdCategory], [para_28_IS_products], [para_29_IS_Sale], [para_30_IS_StockType], [para_31_IS_Suppliers], [para_32_IS_Useraccount]) VALUES (93, NULL, N'Customer_Buy_Product', N'Pro_OTHER_OPTION_Report_CustomerBuy_Service', NULL, NULL, 1, 1, 1, 1, 0, 0, 0, N'0', N'1', 0, NULL, NULL, NULL, 0, NULL, 1, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tbl_OTHER_OPTIONS] ([ID], [UserID], [para_ReportName], [para_Stored_Pro_Name], [EmployeeID], [RunID], [para_Calculate_Time], [paraEmployee_Set], [para01_cmdCalculate], [para02_cmdViewReportMS], [para03_cmdViewReportCR], [para04_cmdExport], [para05_cmdImport], [para06_cboGroup], [para07_cboName], [para08_0_TabMonthly], [para08_1_cboMonthly], [para08_2_Monthly_cboFrom], [para08_3_Monthly_cboTo], [para09_0_TabDate], [para09_1_Date], [para10_0_TabDate_From_To], [para10_1_Date_From_To_cboFrom], [para10_2_Date_From_To_cboTo], [para11_0_TabYearly], [para11_1_Yearly_cboYear], [para_12_Note], [para_13_Note], [para_14_Note], [para_15_Note], [para_16_Note], [para_17_Note], [para_18_Note], [para_19_Note], [para_20_Note], [para_21_Note], [para_22_Note], [para_23_Note], [para_24_Note], [para_25_Note], [para_26_IS_Customers], [para_27_IS_PrdCategory], [para_28_IS_products], [para_29_IS_Sale], [para_30_IS_StockType], [para_31_IS_Suppliers], [para_32_IS_Useraccount]) VALUES (94, NULL, N'Daily_Invoice_Report', N'Pro_OTHER_OPTION_Report_Daily_Invoice', NULL, NULL, 1, 1, 1, 1, 0, 0, 0, N'0', N'1', 0, NULL, NULL, NULL, 0, NULL, 1, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tbl_OTHER_OPTIONS] ([ID], [UserID], [para_ReportName], [para_Stored_Pro_Name], [EmployeeID], [RunID], [para_Calculate_Time], [paraEmployee_Set], [para01_cmdCalculate], [para02_cmdViewReportMS], [para03_cmdViewReportCR], [para04_cmdExport], [para05_cmdImport], [para06_cboGroup], [para07_cboName], [para08_0_TabMonthly], [para08_1_cboMonthly], [para08_2_Monthly_cboFrom], [para08_3_Monthly_cboTo], [para09_0_TabDate], [para09_1_Date], [para10_0_TabDate_From_To], [para10_1_Date_From_To_cboFrom], [para10_2_Date_From_To_cboTo], [para11_0_TabYearly], [para11_1_Yearly_cboYear], [para_12_Note], [para_13_Note], [para_14_Note], [para_15_Note], [para_16_Note], [para_17_Note], [para_18_Note], [para_19_Note], [para_20_Note], [para_21_Note], [para_22_Note], [para_23_Note], [para_24_Note], [para_25_Note], [para_26_IS_Customers], [para_27_IS_PrdCategory], [para_28_IS_products], [para_29_IS_Sale], [para_30_IS_StockType], [para_31_IS_Suppliers], [para_32_IS_Useraccount]) VALUES (95, NULL, N'Daily_Invoice_Report_V2', N'Pro_OTHER_OPTION_Report_Daily_Invoice', NULL, NULL, 1, 1, 1, 1, 0, 0, 0, N'0', N'1', 0, NULL, NULL, NULL, 0, NULL, 1, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tbl_OTHER_OPTIONS] ([ID], [UserID], [para_ReportName], [para_Stored_Pro_Name], [EmployeeID], [RunID], [para_Calculate_Time], [paraEmployee_Set], [para01_cmdCalculate], [para02_cmdViewReportMS], [para03_cmdViewReportCR], [para04_cmdExport], [para05_cmdImport], [para06_cboGroup], [para07_cboName], [para08_0_TabMonthly], [para08_1_cboMonthly], [para08_2_Monthly_cboFrom], [para08_3_Monthly_cboTo], [para09_0_TabDate], [para09_1_Date], [para10_0_TabDate_From_To], [para10_1_Date_From_To_cboFrom], [para10_2_Date_From_To_cboTo], [para11_0_TabYearly], [para11_1_Yearly_cboYear], [para_12_Note], [para_13_Note], [para_14_Note], [para_15_Note], [para_16_Note], [para_17_Note], [para_18_Note], [para_19_Note], [para_20_Note], [para_21_Note], [para_22_Note], [para_23_Note], [para_24_Note], [para_25_Note], [para_26_IS_Customers], [para_27_IS_PrdCategory], [para_28_IS_products], [para_29_IS_Sale], [para_30_IS_StockType], [para_31_IS_Suppliers], [para_32_IS_Useraccount]) VALUES (96, NULL, N'Closing_Sales_Report', N'Pro_OTHER_OPTION_Report_Daily_Invoice', NULL, NULL, 1, 1, 1, 1, 0, 0, 0, N'0', N'1', 0, NULL, NULL, NULL, 0, NULL, 1, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[tbl_OTHER_OPTIONS] ([ID], [UserID], [para_ReportName], [para_Stored_Pro_Name], [EmployeeID], [RunID], [para_Calculate_Time], [paraEmployee_Set], [para01_cmdCalculate], [para02_cmdViewReportMS], [para03_cmdViewReportCR], [para04_cmdExport], [para05_cmdImport], [para06_cboGroup], [para07_cboName], [para08_0_TabMonthly], [para08_1_cboMonthly], [para08_2_Monthly_cboFrom], [para08_3_Monthly_cboTo], [para09_0_TabDate], [para09_1_Date], [para10_0_TabDate_From_To], [para10_1_Date_From_To_cboFrom], [para10_2_Date_From_To_cboTo], [para11_0_TabYearly], [para11_1_Yearly_cboYear], [para_12_Note], [para_13_Note], [para_14_Note], [para_15_Note], [para_16_Note], [para_17_Note], [para_18_Note], [para_19_Note], [para_20_Note], [para_21_Note], [para_22_Note], [para_23_Note], [para_24_Note], [para_25_Note], [para_26_IS_Customers], [para_27_IS_PrdCategory], [para_28_IS_products], [para_29_IS_Sale], [para_30_IS_StockType], [para_31_IS_Suppliers], [para_32_IS_Useraccount]) VALUES (97, NULL, N'Customer_Owed_Report', N'Pro_OTHER_OPTION_Report_OwedBy_Invoice', NULL, NULL, 1, 1, 1, 1, 0, 0, 0, N'0', N'1', 0, NULL, NULL, NULL, 0, NULL, 1, NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[tbl_OTHER_OPTIONS_SEARCH] ON 

INSERT [dbo].[tbl_OTHER_OPTIONS_SEARCH] ([ID], [para_ReportName], [SearchProduct], [SearchCategory], [SearchInvoice], [SearchCustomer], [SearchSupplier], [SearchStock], [SearchUser], [SearchDateOnly], [SearchAdvanceOnly]) VALUES (1, N'Sale_Report', 0, 0, 0, 0, 0, 0, 1, 0, 0)
INSERT [dbo].[tbl_OTHER_OPTIONS_SEARCH] ([ID], [para_ReportName], [SearchProduct], [SearchCategory], [SearchInvoice], [SearchCustomer], [SearchSupplier], [SearchStock], [SearchUser], [SearchDateOnly], [SearchAdvanceOnly]) VALUES (2, N'Sale_Report_Detail', 0, 0, 0, 0, 0, 0, 1, 0, 0)
INSERT [dbo].[tbl_OTHER_OPTIONS_SEARCH] ([ID], [para_ReportName], [SearchProduct], [SearchCategory], [SearchInvoice], [SearchCustomer], [SearchSupplier], [SearchStock], [SearchUser], [SearchDateOnly], [SearchAdvanceOnly]) VALUES (3, N'Customers', 0, 0, 0, 0, 0, 0, 1, 0, 0)
INSERT [dbo].[tbl_OTHER_OPTIONS_SEARCH] ([ID], [para_ReportName], [SearchProduct], [SearchCategory], [SearchInvoice], [SearchCustomer], [SearchSupplier], [SearchStock], [SearchUser], [SearchDateOnly], [SearchAdvanceOnly]) VALUES (4, N'Customer_Buy_Product', 0, 0, 0, 0, 0, 0, 1, 0, 0)
INSERT [dbo].[tbl_OTHER_OPTIONS_SEARCH] ([ID], [para_ReportName], [SearchProduct], [SearchCategory], [SearchInvoice], [SearchCustomer], [SearchSupplier], [SearchStock], [SearchUser], [SearchDateOnly], [SearchAdvanceOnly]) VALUES (10005, N'Daily_Invoice_Report', 0, 0, 0, 0, 0, 0, 1, 0, 0)
INSERT [dbo].[tbl_OTHER_OPTIONS_SEARCH] ([ID], [para_ReportName], [SearchProduct], [SearchCategory], [SearchInvoice], [SearchCustomer], [SearchSupplier], [SearchStock], [SearchUser], [SearchDateOnly], [SearchAdvanceOnly]) VALUES (10006, N'Daily_Invoice_Report_V2', 0, 0, 0, 0, 0, 0, 1, 0, 0)
INSERT [dbo].[tbl_OTHER_OPTIONS_SEARCH] ([ID], [para_ReportName], [SearchProduct], [SearchCategory], [SearchInvoice], [SearchCustomer], [SearchSupplier], [SearchStock], [SearchUser], [SearchDateOnly], [SearchAdvanceOnly]) VALUES (10007, N'Closing_Sales_Report', 0, 0, 0, 0, 0, 0, 1, 0, 0)
INSERT [dbo].[tbl_OTHER_OPTIONS_SEARCH] ([ID], [para_ReportName], [SearchProduct], [SearchCategory], [SearchInvoice], [SearchCustomer], [SearchSupplier], [SearchStock], [SearchUser], [SearchDateOnly], [SearchAdvanceOnly]) VALUES (10008, N'Customer_Owed_Report', 0, 0, 0, 0, 0, 0, 1, 0, 0)
SET IDENTITY_INSERT [dbo].[tbl_OTHER_OPTIONS_SEARCH] OFF
ALTER TABLE [dbo].[tbl_OTHER_OPTIONS_SEARCH] ADD  CONSTRAINT [DF_tbl_OTHER_OPTIONS_SEARCH_SearchProduct]  DEFAULT ((0)) FOR [SearchProduct]
GO
ALTER TABLE [dbo].[tbl_OTHER_OPTIONS_SEARCH] ADD  CONSTRAINT [DF_tbl_OTHER_OPTIONS_SEARCH_SearchCategory]  DEFAULT ((0)) FOR [SearchCategory]
GO
ALTER TABLE [dbo].[tbl_OTHER_OPTIONS_SEARCH] ADD  CONSTRAINT [DF_tbl_OTHER_OPTIONS_SEARCH_SearchInvoice]  DEFAULT ((0)) FOR [SearchInvoice]
GO
ALTER TABLE [dbo].[tbl_OTHER_OPTIONS_SEARCH] ADD  CONSTRAINT [DF_tbl_OTHER_OPTIONS_SEARCH_SearchCustomer]  DEFAULT ((0)) FOR [SearchCustomer]
GO
ALTER TABLE [dbo].[tbl_OTHER_OPTIONS_SEARCH] ADD  CONSTRAINT [DF_tbl_OTHER_OPTIONS_SEARCH_SearchSupplier]  DEFAULT ((0)) FOR [SearchSupplier]
GO
ALTER TABLE [dbo].[tbl_OTHER_OPTIONS_SEARCH] ADD  CONSTRAINT [DF_tbl_OTHER_OPTIONS_SEARCH_SearchStock]  DEFAULT ((0)) FOR [SearchStock]
GO
ALTER TABLE [dbo].[tbl_OTHER_OPTIONS_SEARCH] ADD  CONSTRAINT [DF_tbl_OTHER_OPTIONS_SEARCH_SearchUser]  DEFAULT ((0)) FOR [SearchUser]
GO
ALTER TABLE [dbo].[tbl_OTHER_OPTIONS_SEARCH] ADD  CONSTRAINT [DF_tbl_OTHER_OPTIONS_SEARCH_SearchDate]  DEFAULT ((0)) FOR [SearchDateOnly]
GO
ALTER TABLE [dbo].[tbl_OTHER_OPTIONS_SEARCH] ADD  CONSTRAINT [DF_tbl_OTHER_OPTIONS_SEARCH_SearchAdvanceOnly]  DEFAULT ((0)) FOR [SearchAdvanceOnly]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[66] 4[5] 2[29] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "tbl_customerorder"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 520
               Right = 289
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "tbl_customers"
            Begin Extent = 
               Top = 6
               Left = 327
               Bottom = 339
               Right = 527
            End
            DisplayFlags = 280
            TopColumn = 31
         End
         Begin Table = "tbl_useraccount"
            Begin Extent = 
               Top = 204
               Left = 565
               Bottom = 455
               Right = 739
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 3690
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'View_OTHER_OPION_Report_OwedBy_Invoice'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'View_OTHER_OPION_Report_OwedBy_Invoice'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[56] 4[38] 2[1] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "tbl_customerorder"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 309
               Right = 289
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "tbl_customers"
            Begin Extent = 
               Top = 138
               Left = 38
               Bottom = 268
               Right = 238
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "tbl_useraccount"
            Begin Extent = 
               Top = 138
               Left = 276
               Bottom = 268
               Right = 450
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "tbl_orderdetails"
            Begin Extent = 
               Top = 0
               Left = 444
               Bottom = 314
               Right = 671
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "tbl_products"
            Begin Extent = 
               Top = 6
               Left = 709
               Bottom = 321
               Right = 902
            End
            DisplayFlags = 280
            TopColumn = 3
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 2940
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
  ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'View_OTHER_OPION_Report_SaleBy_Detail'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N'       Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'View_OTHER_OPION_Report_SaleBy_Detail'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'View_OTHER_OPION_Report_SaleBy_Detail'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[61] 4[5] 2[19] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "tbl_customerorder"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 289
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "tbl_customers"
            Begin Extent = 
               Top = 138
               Left = 38
               Bottom = 324
               Right = 238
            End
            DisplayFlags = 280
            TopColumn = 17
         End
         Begin Table = "tbl_useraccount"
            Begin Extent = 
               Top = 138
               Left = 276
               Bottom = 268
               Right = 450
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'View_OTHER_OPION_Report_SaleBy_Invoice'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'View_OTHER_OPION_Report_SaleBy_Invoice'
GO
