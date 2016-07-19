
USE [D:\RESPIRCARE_VSS\可穿戴移动呼吸监测治疗系统\03项目开发\06_源代码\SUPERSOFT\SUPERSOFT.DAL\DATABASE\SUPERSOFT.MDF]
GO
/****** Object:  Table [dbo].[Doctors]    Script Date: 2016/7/14 14:00:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Doctors](
	[Id] [uniqueidentifier] NOT NULL,
	[FirstName] [nvarchar](16) NULL,
	[LastName] [nvarchar](16) NULL,
 CONSTRAINT [PK__Doctors__3214EC0723E85D71] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Patients]    Script Date: 2016/7/14 14:00:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Patients](
	[Id] [uniqueidentifier] NOT NULL,
	[FirstName] [nvarchar](16) NOT NULL,
	[LastName] [nvarchar](16) NOT NULL,
	[DateOfBirth] [date] NULL,
	[Weight] [int] NULL,
	[Height] [int] NULL,
	[Gender] [bit] NULL,
	[Photo] [image] NULL,
	[EMail] [nvarchar](32) NULL,
	[TelephoneNumbers] [nvarchar](32) NULL,
	[PostalCode] [nvarchar](32) NULL,
	[Address] [nvarchar](128) NULL,
	[DoctorId] [uniqueidentifier] NULL,
	[Diagnosis] [nvarchar](128) NULL,
 CONSTRAINT [PK__Patients__3214EC073F400039] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PatientsProducts]    Script Date: 2016/7/14 14:00:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PatientsProducts](
	[Id] [uniqueidentifier] NOT NULL,
	[PatientId] [uniqueidentifier] NOT NULL,
	[ProductId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_PatientsProducts] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Products]    Script Date: 2016/7/14 14:00:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[Id] [uniqueidentifier] NOT NULL,
	[SerialNumber] [nvarchar](18) NULL,
	[ProductVersion] [nvarchar](32) NULL,
	[ProductModel] [int] NULL,
	[TotalWorkingTime] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ProductWorkingDetailedDatas]    Script Date: 2016/7/14 14:00:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ProductWorkingDetailedDatas](
	[Id] [uniqueidentifier] NOT NULL,
	[ProductWorkingSummaryDataId] [uniqueidentifier] NULL,
	[Content] [varbinary](max) NULL,
 CONSTRAINT [PK_ProductWorkingDetailedDatas] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ProductWorkingStatisticsDatas]    Script Date: 2016/7/14 14:00:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductWorkingStatisticsDatas](
	[Id] [uniqueidentifier] NOT NULL,
	[ProductId] [uniqueidentifier] NULL,
	[TherapyMode] [int] NOT NULL,
	[DataTime] [date] NOT NULL,
	[TotalUsage] [bigint] NULL,
	[CountAHI] [int] NULL,
	[CountAI] [int] NULL,
	[CountHI] [int] NULL,
	[CountSnore] [int] NULL,
	[CountPassive] [int] NULL,
	[PressureMax] [real] NULL,
	[PressureP95] [real] NULL,
	[PressureMedian] [real] NULL,
	[FlowMax] [real] NULL,
	[FlowP95] [real] NULL,
	[FlowMedian] [real] NULL,
	[LeakMax] [real] NULL,
	[LeakP95] [real] NULL,
	[LeakMedian] [real] NULL,
	[TidalVolumeMax] [real] NULL,
	[TidalVolumeP95] [real] NULL,
	[TidalVolumeMedian] [real] NULL,
	[MinuteVentilationMax] [int] NULL,
	[MinuteVentilationP95] [int] NULL,
	[MinuteVentilationMedian] [int] NULL,
	[SpO2Max] [int] NULL,
	[SpO2P95] [int] NULL,
	[SpO2Median] [int] NULL,
	[PulseRateMax] [int] NULL,
	[PulseRateP95] [int] NULL,
	[PulseRateMedian] [int] NULL,
	[RespiratoryRateMax] [int] NULL,
	[RespiratoryRateP95] [int] NULL,
	[RespiratoryRateMedian] [int] NULL,
	[IERatioMax] [real] NULL,
	[IERatioP95] [real] NULL,
	[IERatioMedian] [real] NULL,
	[IPAPMax] [real] NULL,
	[IPAPP95] [real] NULL,
	[IPAPMedian] [real] NULL,
	[EPAPMax] [real] NULL,
	[EPAPP95] [real] NULL,
	[EPAPMedian] [real] NULL,
 CONSTRAINT [PK_ProductWorkingStatisticsDatas] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ProductWorkingSummaryDatas]    Script Date: 2016/7/14 14:00:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductWorkingSummaryDatas](
	[Id] [uniqueidentifier] NOT NULL,
	[ProductId] [uniqueidentifier] NOT NULL,
	[FileName] [nvarchar](32) NOT NULL,
	[StartTime] [datetime] NOT NULL,
	[EndTime] [datetime] NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
	[ProductModel] [int] NOT NULL,
	[WorkingTime] [int] NOT NULL,
	[CurrentTime] [datetime] NOT NULL,
	[TherapyMode] [int] NOT NULL,
	[IPAP] [real] NULL,
	[EPAP] [real] NULL,
	[RiseTime] [int] NULL,
	[RespiratoryRate] [int] NULL,
	[InspireTime] [int] NULL,
	[ITrigger] [int] NULL,
	[ETrigger] [int] NULL,
	[Ramp] [int] NULL,
	[ExhaleTime] [int] NULL,
	[IPAPMax] [real] NULL,
	[EPAPMin] [real] NULL,
	[PSMax] [real] NULL,
	[PSMin] [real] NULL,
	[CPAP] [real] NULL,
	[CFlex] [int] NULL,
	[CPAPStart] [real] NULL,
	[CPAPMax] [real] NULL,
	[CPAPMin] [real] NULL,
	[Alert] [int] NULL,
	[Alert-Tube] [int] NULL,
	[Alert-Apnea] [int] NULL,
	[Alert-MinuteVentilation] [int] NULL,
	[Alert-HRate] [int] NULL,
	[Alert-LRate] [int] NULL,
	[Alert-Reserve1] [int] NULL,
	[Alert-Reserve2] [int] NULL,
	[Alert-Reserve3] [int] NULL,
	[Alert-Reserve4] [int] NULL,
	[Config-HumidifierLevel] [int] NULL,
	[Config-DataStore] [int] NULL,
	[Config-SmartStart] [int] NULL,
	[Config-PressureUnit] [int] NULL,
	[Config-Language] [int] NULL,
	[Config-Backlight] [int] NULL,
	[Config-MaskPressure] [int] NULL,
	[Config-ClinicalSet] [int] NULL,
	[Config-Reserve1] [int] NULL,
	[Config-Reserve2] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  View [dbo].[ViewPatientsProducts]    Script Date: 2016/7/14 14:00:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ViewPatientsProducts]
	AS SELECT pp.Id,pp.PatientId,
	 pa.FirstName  ,
	 pa.LastName  ,
	 pr.SerialNumber,
	 pr.ProductVersion,
	 pr.ProductModel,
	 pr.TotalWorkingTime

	 FROM [PatientsProducts] as pp left join [Patients]  as pa on pp.PatientId= pa.Id left join [Products] as pr on pp.ProductId=pr.Id

GO
/****** Object:  View [dbo].[ViewProductWorkingStatisticsDatas]    Script Date: 2016/7/14 14:00:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ViewProductWorkingStatisticsDatas]
	AS SELECT 	
	pp.PatientId,
	 pwsd.*
	
	 FROM [ProductWorkingStatisticsDatas] as pwsd left join [PatientsProducts] as pp   on pp.ProductId =pwsd.ProductId

GO
/****** Object:  View [dbo].[ViewProductWorkingSummaryDatas]    Script Date: 2016/7/14 14:00:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ViewProductWorkingSummaryDatas]
	AS SELECT
	pp.PatientId,
	pwsd.*
	
	 FROM [ProductWorkingSummaryDatas] as pwsd left join  [PatientsProducts] as pp on pwsd.ProductId =pp.ProductId

GO
/****** Object:  View [dbo].[ViewProductWorkingSummaryDetailedDatas]    Script Date: 2016/7/14 14:00:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ViewProductWorkingSummaryDetailedDatas]
	AS SELECT
	pp.PatientId,
	ps.*,
	pd.Content
	
	 FROM [ProductWorkingSummaryDatas] as ps left join  [ProductWorkingDetailedDatas] as pd on ps.Id =pd.ProductWorkingSummaryDataId left join PatientsProducts as pp on pp.ProductId =ps.ProductId

GO
ALTER TABLE [dbo].[Patients]  WITH CHECK ADD  CONSTRAINT [FK_Patients_Doctors] FOREIGN KEY([DoctorId])
REFERENCES [dbo].[Doctors] ([Id])
GO
ALTER TABLE [dbo].[Patients] CHECK CONSTRAINT [FK_Patients_Doctors]
GO
ALTER TABLE [dbo].[PatientsProducts]  WITH CHECK ADD  CONSTRAINT [FK_PatientsProducts_Patients] FOREIGN KEY([PatientId])
REFERENCES [dbo].[Patients] ([Id])
GO
ALTER TABLE [dbo].[PatientsProducts] CHECK CONSTRAINT [FK_PatientsProducts_Patients]
GO
ALTER TABLE [dbo].[PatientsProducts]  WITH CHECK ADD  CONSTRAINT [FK_PatientsProducts_Products] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Products] ([Id])
GO
ALTER TABLE [dbo].[PatientsProducts] CHECK CONSTRAINT [FK_PatientsProducts_Products]
GO
ALTER TABLE [dbo].[ProductWorkingDetailedDatas]  WITH CHECK ADD  CONSTRAINT [FK_ProductWorkingDetailedDatas_ProductWorkingSummaryDatas] FOREIGN KEY([ProductWorkingSummaryDataId])
REFERENCES [dbo].[ProductWorkingSummaryDatas] ([Id])
GO
ALTER TABLE [dbo].[ProductWorkingDetailedDatas] CHECK CONSTRAINT [FK_ProductWorkingDetailedDatas_ProductWorkingSummaryDatas]
GO
ALTER TABLE [dbo].[ProductWorkingStatisticsDatas]  WITH CHECK ADD  CONSTRAINT [FK_ProductWorkingStatisticsDatas_Products] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Products] ([Id])
GO
ALTER TABLE [dbo].[ProductWorkingStatisticsDatas] CHECK CONSTRAINT [FK_ProductWorkingStatisticsDatas_Products]
GO
ALTER TABLE [dbo].[ProductWorkingSummaryDatas]  WITH CHECK ADD  CONSTRAINT [FK_ProductWorkingSummaryDatas_Products] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Products] ([Id])
GO
ALTER TABLE [dbo].[ProductWorkingSummaryDatas] CHECK CONSTRAINT [FK_ProductWorkingSummaryDatas_Products]
GO
/****** Object:  StoredProcedure [dbo].[P_DeletePatientAllInfo]    Script Date: 2016/7/14 14:00:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[P_DeletePatientAllInfo]
	@patientId uniqueidentifier ,
	@result int output
AS
DECLARE @productId uniqueidentifier
BEGIN TRY 
	BEGIN TRAN 
		select @productId=pp.ProductId from PatientsProducts  pp where pp.PatientId =@patientId
		delete from PatientsProducts  where  PatientId=@patientId
		delete from ProductWorkingStatisticsDatas  where  ProductId=@productId
		delete from ProductWorkingDetailedDatas  where  ProductWorkingSummaryDataId in (select Id From ProductWorkingSummaryDatas where ProductId=@productId)
		delete from ProductWorkingSummaryDatas  where  ProductId=@productId
		delete from Products  where  Id=@productId
		delete from Patients  where  Id=@patientId
		set @result=0
	COMMIT TRAN 
END TRY 
BEGIN CATCH 
	ROLLBACK TRAN 
	set @result=@@Error
END CATCH 

RETURN 0

GO
/****** Object:  StoredProcedure [dbo].[P_DeletePatientUsageInfo]    Script Date: 2016/7/14 14:00:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[P_DeletePatientUsageInfo]
	@patientId uniqueidentifier ,
	@startTime DATETIME,
    @endTime DATETIME,
	@result int output
AS
DECLARE @productId uniqueidentifier
BEGIN TRY 
	BEGIN TRAN 
		select @productId=pp.ProductId from PatientsProducts  pp where pp.PatientId =@patientId
		delete from ProductWorkingStatisticsDatas  where  ProductId=@productId and DataTime >=@startTime and DataTime <=@endTime
		delete from ProductWorkingDetailedDatas  where  ProductWorkingSummaryDataId in (select Id From ProductWorkingSummaryDatas where ProductId=@productId  and StartTime >=dateadd(hour,12,@startTime) and EndTime <dateadd(hour,36,@endTime))
		delete from ProductWorkingSummaryDatas  where  ProductId=@productId and StartTime >=dateadd(hour,12,@startTime) and EndTime <dateadd(hour,36,@endTime)
		set @result=0
	COMMIT TRAN 
END TRY 
BEGIN CATCH 
	ROLLBACK TRAN 
	set @result=@@Error
END CATCH 

RETURN 0

GO
