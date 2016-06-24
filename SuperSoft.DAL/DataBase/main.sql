/*
Navicat SQLite Data Transfer

Source Server         : SuperSoft
Source Server Version : 30714
Source Host           : :0

Target Server Type    : SQLite
Target Server Version : 30714
File Encoding         : 65001

Date: 2016-06-23 13:48:32
*/

PRAGMA foreign_keys = OFF;

-- ----------------------------
-- Table structure for Doctors
-- ----------------------------
DROP TABLE IF EXISTS "main"."Doctors";
CREATE TABLE [Doctors] (
    [Id] guid PRIMARY KEY NOT NULL,
    [FirstName] nvarchar(16) NOT NULL,
    [LastName] nvarchar(16) NOT NULL
);

-- ----------------------------
-- Records of Doctors
-- ----------------------------

-- ----------------------------
-- Table structure for Patients
-- ----------------------------
DROP TABLE IF EXISTS "main"."Patients";
CREATE TABLE [Patients] (
    [Id] guid PRIMARY KEY NOT NULL,
    [FirstName] nvarchar(16) NOT NULL,
    [LastName] nvarchar(16) NOT NULL,
    [DateOfBirth] datetime,
    [Weight] integer,
    [Height] integer,
    [Gender] bit,
    [Photo] image,
    [EMail] nvarchar(32),
    [TelephoneNumbers] nvarchar(32),
    [PostalCode] nvarchar(16),
    [Address] nvarchar(128),
    [Diagnosis] nvarchar(128),
    [DoctorId] guid
);

-- ----------------------------
-- Records of Patients
-- ----------------------------

-- ----------------------------
-- Table structure for PatientsProducts
-- ----------------------------
DROP TABLE IF EXISTS "main"."PatientsProducts";
CREATE TABLE [PatientsProducts] (
    [Id] guid PRIMARY KEY NOT NULL,
    [PatientId] guid NOT NULL,
    [ProductId] guid NOT NULL
);

-- ----------------------------
-- Records of PatientsProducts
-- ----------------------------

-- ----------------------------
-- Table structure for Products
-- ----------------------------
DROP TABLE IF EXISTS "main"."Products";
CREATE TABLE [Products] (
    [Id] guid PRIMARY KEY NOT NULL,
    [SerialNumber] nvarchar(18),
    [ProductVersion] nvarchar(32),
    [ProductModel] integer,
    [TotalWorkingTime] integer
);

-- ----------------------------
-- Records of Products
-- ----------------------------

-- ----------------------------
-- Table structure for ProductWorkingDetailedDatas
-- ----------------------------
DROP TABLE IF EXISTS "main"."ProductWorkingDetailedDatas";
CREATE TABLE [ProductWorkingDetailedDatas] (
    [Id] guid PRIMARY KEY NOT NULL,
    [ProductWorkingSummaryDataId] guid,
    [Content] blob
);

-- ----------------------------
-- Records of ProductWorkingDetailedDatas
-- ----------------------------

-- ----------------------------
-- Table structure for ProductWorkingStatisticsDatas
-- ----------------------------
DROP TABLE IF EXISTS "main"."ProductWorkingStatisticsDatas";
CREATE TABLE [ProductWorkingStatisticsDatas] (
    [Id] guid PRIMARY KEY NOT NULL,
    [ProductId] guid,
    [TherapyMode] integer NOT NULL,
    [DataTime] datetime NOT NULL,
    [TotalUsage] bigint,
    [CountAHI] integer,
    [CountAI] integer,
    [CountHI] integer,
    [CountSnore] integer,
    [CountPassive] integer,
    [PressureMax] real,
    [PressureP95] real,
    [PressureMedian] real,
    [FlowMax] real,
    [FlowP95] real,
    [FlowMedian] real,
    [LeakMax] real NOT NULL,
    [LeakP95] real,
    [LeakMedian] real,
    [TidalVolumeMax] real,
    [TidalVolumeP95] real,
    [TidalVolumeMedian] real,
    [MinuteVentilationMax] integer,
    [MinuteVentilationP95] integer,
    [MinuteVentilationMedian] integer,
    [SpO2Max] integer,
    [SpO2P95] integer,
    [SpO2Median] integer,
    [PulseRateMax] integer,
    [PulseRateP95] integer,
    [PulseRateMedian] integer,
    [RespiratoryRateMax] integer,
    [RespiratoryRateP95] integer,
    [RespiratoryRateMedian] integer,
    [IERatioMax] real,
    [IERatioP95] real,
    [IERatioMedian] real,
    [IPAPMax] real,
    [IPAPP95] real,
    [IPAPMedian] real,
    [EPAPMax] real,
    [EPAPP95] real,
    [EPAPMedian] real
);

-- ----------------------------
-- Records of ProductWorkingStatisticsDatas
-- ----------------------------

-- ----------------------------
-- Table structure for ProductWorkingSummaryDatas
-- ----------------------------
DROP TABLE IF EXISTS "main"."ProductWorkingSummaryDatas";
CREATE TABLE [ProductWorkingSummaryDatas] (
    [Id] guid PRIMARY KEY NOT NULL,
    [ProductId] guid NOT NULL,
    [FileName] nvarchar(32) NOT NULL,
    [StartTime] datetime NOT NULL,
    [EndTime] datetime NOT NULL,
    [ProductVersion] nvarchar(32) NOT NULL,
    [ProductModel] integer NOT NULL,
    [WorkingTime] integer NOT NULL,
    [CurrentTime] datetime NOT NULL,
    [TherapyMode] integer NOT NULL,
    [IPAP] real,
    [EPAP] real,
    [RiseTime] integer,
    [RespiratoryRate] integer,
    [InspireTime] integer,
    [ITrigger] integer,
    [ETrigger] integer,
    [Ramp] integer,
    [ExhaleTime] integer,
    [IPAPMax] real,
    [EPAPMin] real,
    [PSMax] real,
    [PSMin] real,
    [CPAP] real,
    [CFlex] integer,
    [CPAPStart] real,
    [CPAPMax] real,
    [CPAPMin] real,
    [Alert] integer,
    [Alert-Tube] integer,
    [Alert-Apnea] integer,
    [Alert-MinuteVentilation] integer,
    [Alert-HRate] integer,
    [Alert-LRate] integer,
    [Alert-Reserve1] integer,
    [Alert-Reserve2] integer,
    [Alert-Reserve3] integer,
    [Alert-Reserve4] integer,
    [Config-HumidifierLevel] integer,
    [Config-DataStore] integer,
    [Config-SmartStart] integer,
    [Config-PressureUnit] integer,
    [Config-Language] integer,
    [Config-Backlight] integer,
    [Config-MaskPressure] integer,
    [Config-ClinicalSet] integer,
    [Config-Reserve1] integer,
    [Config-Reserve2] integer
);

-- ----------------------------
-- Records of ProductWorkingSummaryDatas
-- ----------------------------

-- ----------------------------
-- View structure for ViewPatientsProducts
-- ----------------------------
DROP VIEW IF EXISTS "main"."ViewPatientsProducts";
CREATE VIEW ViewPatientsProducts AS SELECT
	pp.Id,
	pp.PatientId,
	pa.FirstName,
	pa.LastName,
	pr.SerialNumber,
	pr.ProductVersion,
	pr.ProductModel,
	pr.TotalWorkingTime
FROM
	PatientsProducts AS pp
LEFT JOIN Patients AS pa ON pp.PatientId = pa.Id
LEFT JOIN Products AS pr ON pp.ProductId = pr.Id;

-- ----------------------------
-- View structure for ViewProductWorkingStatisticsDatas
-- ----------------------------
DROP VIEW IF EXISTS "main"."ViewProductWorkingStatisticsDatas";
CREATE VIEW ViewProductWorkingStatisticsDatas AS SELECT
	pp.PatientId,
	pwsd.*
FROM
	ProductWorkingStatisticsDatas  AS pwsd
LEFT JOIN PatientsProducts  AS pp ON pp.ProductId = pwsd.ProductId;

-- ----------------------------
-- View structure for ViewProductWorkingSummaryDatas
-- ----------------------------
DROP VIEW IF EXISTS "main"."ViewProductWorkingSummaryDatas";
CREATE VIEW ViewProductWorkingSummaryDatas
	AS SELECT
	pp.PatientId,
	pwsd.*
	
	 FROM ProductWorkingSummaryDatas as pwsd left join  PatientsProducts as pp on pwsd.ProductId =pp.ProductId;

-- ----------------------------
-- View structure for ViewProductWorkingSummaryDetailedDatas
-- ----------------------------
DROP VIEW IF EXISTS "main"."ViewProductWorkingSummaryDetailedDatas";
CREATE VIEW ViewProductWorkingSummaryDetailedDatas AS SELECT
	pp.PatientId,
	ps.*, pd.Content
FROM
	ProductWorkingSummaryDatas AS ps
LEFT JOIN ProductWorkingDetailedDatas AS pd ON ps.Id = pd.ProductWorkingSummaryDataId
LEFT JOIN PatientsProducts AS pp ON pp.ProductId = ps.ProductId;
