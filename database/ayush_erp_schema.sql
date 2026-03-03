/* AYUSH ERP - Initial SQL Server schema aligned to requested modules */

CREATE TABLE dbo.Institution (
    InstitutionId INT IDENTITY(1,1) PRIMARY KEY,
    InstitutionCode NVARCHAR(50) NOT NULL UNIQUE,
    InstitutionName NVARCHAR(200) NOT NULL,
    DistrictCode NVARCHAR(20) NOT NULL,
    StateCode NVARCHAR(20) NOT NULL,
    IsActive BIT NOT NULL DEFAULT 1,
    CreatedAt DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME()
);

CREATE TABLE dbo.BudgetHead (
    BudgetHeadId INT IDENTITY(1,1) PRIMARY KEY,
    HeadCode NVARCHAR(50) NOT NULL UNIQUE,
    HeadName NVARCHAR(200) NOT NULL,
    Category NVARCHAR(100) NOT NULL,
    FiscalYear NVARCHAR(9) NOT NULL,
    CreatedAt DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME()
);

CREATE TABLE dbo.DemandRequest (
    DemandRequestId BIGINT IDENTITY(1,1) PRIMARY KEY,
    InstitutionId INT NOT NULL,
    BudgetHeadId INT NOT NULL,
    RequestedAmount DECIMAL(18,2) NOT NULL,
    Justification NVARCHAR(MAX) NULL,
    Status NVARCHAR(30) NOT NULL DEFAULT 'Submitted',
    RequestedOn DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME(),
    CONSTRAINT FK_DemandRequest_Institution FOREIGN KEY (InstitutionId) REFERENCES dbo.Institution(InstitutionId),
    CONSTRAINT FK_DemandRequest_BudgetHead FOREIGN KEY (BudgetHeadId) REFERENCES dbo.BudgetHead(BudgetHeadId)
);
CREATE INDEX IX_DemandRequest_Status_RequestedOn ON dbo.DemandRequest(Status, RequestedOn DESC);

CREATE TABLE dbo.ApprovalHistory (
    ApprovalHistoryId BIGINT IDENTITY(1,1) PRIMARY KEY,
    ModuleName NVARCHAR(50) NOT NULL,
    EntityId BIGINT NOT NULL,
    ApprovalLevel NVARCHAR(30) NOT NULL,
    Decision NVARCHAR(20) NOT NULL,
    Remarks NVARCHAR(1000) NULL,
    ActionByUserId NVARCHAR(100) NOT NULL,
    ActionOn DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME()
);
CREATE INDEX IX_ApprovalHistory_Module_Entity ON dbo.ApprovalHistory(ModuleName, EntityId);

CREATE TABLE dbo.Allocation (
    AllocationId BIGINT IDENTITY(1,1) PRIMARY KEY,
    DemandRequestId BIGINT NOT NULL,
    AllocatedAmount DECIMAL(18,2) NOT NULL,
    AllocationDate DATE NOT NULL,
    AllocationOrderNo NVARCHAR(100) NULL,
    CONSTRAINT FK_Allocation_DemandRequest FOREIGN KEY (DemandRequestId) REFERENCES dbo.DemandRequest(DemandRequestId)
);

CREATE TABLE dbo.Utilization (
    UtilizationId BIGINT IDENTITY(1,1) PRIMARY KEY,
    AllocationId BIGINT NOT NULL,
    UtilizedAmount DECIMAL(18,2) NOT NULL,
    UtilizationDate DATE NOT NULL,
    CertificatePath NVARCHAR(500) NULL,
    Remarks NVARCHAR(1000) NULL,
    CONSTRAINT FK_Utilization_Allocation FOREIGN KEY (AllocationId) REFERENCES dbo.Allocation(AllocationId)
);

CREATE TABLE dbo.Project (
    ProjectId BIGINT IDENTITY(1,1) PRIMARY KEY,
    InstitutionId INT NOT NULL,
    ProjectCode NVARCHAR(50) NOT NULL UNIQUE,
    ProjectName NVARCHAR(200) NOT NULL,
    SanctionedCost DECIMAL(18,2) NOT NULL,
    StartDate DATE NOT NULL,
    ExpectedEndDate DATE NOT NULL,
    Status NVARCHAR(30) NOT NULL DEFAULT 'Planned',
    CONSTRAINT FK_Project_Institution FOREIGN KEY (InstitutionId) REFERENCES dbo.Institution(InstitutionId)
);

CREATE TABLE dbo.Milestone (
    MilestoneId BIGINT IDENTITY(1,1) PRIMARY KEY,
    ProjectId BIGINT NOT NULL,
    MilestoneName NVARCHAR(200) NOT NULL,
    ExpectedDate DATE NOT NULL,
    WeightPercent DECIMAL(5,2) NOT NULL,
    IsCompleted BIT NOT NULL DEFAULT 0,
    CompletedOn DATE NULL,
    CONSTRAINT FK_Milestone_Project FOREIGN KEY (ProjectId) REFERENCES dbo.Project(ProjectId)
);

CREATE TABLE dbo.ProgressUpdate (
    ProgressUpdateId BIGINT IDENTITY(1,1) PRIMARY KEY,
    ProjectId BIGINT NOT NULL,
    MilestoneId BIGINT NULL,
    UpdateDate DATE NOT NULL,
    ProgressPercent DECIMAL(5,2) NOT NULL,
    ActualCostToDate DECIMAL(18,2) NULL,
    PhotoPath NVARCHAR(500) NULL,
    Remarks NVARCHAR(1000) NULL,
    CONSTRAINT FK_ProgressUpdate_Project FOREIGN KEY (ProjectId) REFERENCES dbo.Project(ProjectId),
    CONSTRAINT FK_ProgressUpdate_Milestone FOREIGN KEY (MilestoneId) REFERENCES dbo.Milestone(MilestoneId)
);
CREATE INDEX IX_ProgressUpdate_Project_UpdateDate ON dbo.ProgressUpdate(ProjectId, UpdateDate DESC);

CREATE TABLE dbo.DelayFlag (
    DelayFlagId BIGINT IDENTITY(1,1) PRIMARY KEY,
    ProjectId BIGINT NOT NULL,
    DetectedOn DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME(),
    DelayDays INT NOT NULL,
    Reason NVARCHAR(1000) NULL,
    IsResolved BIT NOT NULL DEFAULT 0,
    CONSTRAINT FK_DelayFlag_Project FOREIGN KEY (ProjectId) REFERENCES dbo.Project(ProjectId)
);

CREATE TABLE dbo.Patient (
    PatientId BIGINT IDENTITY(1,1) PRIMARY KEY,
    PatientUid NVARCHAR(30) NOT NULL UNIQUE,
    FullName NVARCHAR(200) NOT NULL,
    Gender NVARCHAR(20) NOT NULL,
    DateOfBirth DATE NULL,
    BloodGroup NVARCHAR(5) NULL,
    MobileNo NVARCHAR(20) NULL,
    Address NVARCHAR(500) NULL,
    RegisteredOn DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME()
);

CREATE TABLE dbo.Allergy (
    AllergyId BIGINT IDENTITY(1,1) PRIMARY KEY,
    PatientId BIGINT NOT NULL,
    AllergyName NVARCHAR(200) NOT NULL,
    Severity NVARCHAR(20) NULL,
    Notes NVARCHAR(500) NULL,
    CONSTRAINT FK_Allergy_Patient FOREIGN KEY (PatientId) REFERENCES dbo.Patient(PatientId)
);

CREATE TABLE dbo.Visit (
    VisitId BIGINT IDENTITY(1,1) PRIMARY KEY,
    PatientId BIGINT NOT NULL,
    VisitDate DATETIME2 NOT NULL,
    DoctorUserId NVARCHAR(100) NOT NULL,
    Symptoms NVARCHAR(1000) NULL,
    CONSTRAINT FK_Visit_Patient FOREIGN KEY (PatientId) REFERENCES dbo.Patient(PatientId)
);

CREATE TABLE dbo.Diagnosis (
    DiagnosisId BIGINT IDENTITY(1,1) PRIMARY KEY,
    VisitId BIGINT NOT NULL,
    DiagnosisText NVARCHAR(1000) NOT NULL,
    Notes NVARCHAR(1000) NULL,
    CONSTRAINT FK_Diagnosis_Visit FOREIGN KEY (VisitId) REFERENCES dbo.Visit(VisitId)
);

CREATE TABLE dbo.Prescription (
    PrescriptionId BIGINT IDENTITY(1,1) PRIMARY KEY,
    VisitId BIGINT NOT NULL,
    MedicineName NVARCHAR(200) NOT NULL,
    Dosage NVARCHAR(100) NOT NULL,
    DurationDays INT NOT NULL,
    Instructions NVARCHAR(1000) NULL,
    CONSTRAINT FK_Prescription_Visit FOREIGN KEY (VisitId) REFERENCES dbo.Visit(VisitId)
);

CREATE TABLE dbo.Feedback (
    FeedbackId BIGINT IDENTITY(1,1) PRIMARY KEY,
    VisitId BIGINT NOT NULL,
    Rating TINYINT NOT NULL,
    Comment NVARCHAR(1000) NULL,
    SubmittedOn DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME(),
    CONSTRAINT FK_Feedback_Visit FOREIGN KEY (VisitId) REFERENCES dbo.Visit(VisitId),
    CONSTRAINT CK_Feedback_Rating CHECK (Rating BETWEEN 1 AND 5)
);

CREATE TABLE dbo.Department (
    DepartmentId INT IDENTITY(1,1) PRIMARY KEY,
    DepartmentCode NVARCHAR(30) NOT NULL UNIQUE,
    DepartmentName NVARCHAR(200) NOT NULL
);

CREATE TABLE dbo.Staff (
    StaffId BIGINT IDENTITY(1,1) PRIMARY KEY,
    StaffCode NVARCHAR(50) NOT NULL UNIQUE,
    FullName NVARCHAR(200) NOT NULL,
    DepartmentId INT NOT NULL,
    Designation NVARCHAR(100) NOT NULL,
    JoiningDate DATE NOT NULL,
    IsActive BIT NOT NULL DEFAULT 1,
    CONSTRAINT FK_Staff_Department FOREIGN KEY (DepartmentId) REFERENCES dbo.Department(DepartmentId)
);

CREATE TABLE dbo.Shift (
    ShiftId INT IDENTITY(1,1) PRIMARY KEY,
    ShiftName NVARCHAR(20) NOT NULL UNIQUE,
    StartTime TIME NOT NULL,
    EndTime TIME NOT NULL
);

CREATE TABLE dbo.Attendance (
    AttendanceId BIGINT IDENTITY(1,1) PRIMARY KEY,
    StaffId BIGINT NOT NULL,
    ShiftId INT NOT NULL,
    AttendanceDate DATE NOT NULL,
    ClockIn DATETIME2 NULL,
    ClockOut DATETIME2 NULL,
    IsLate BIT NOT NULL DEFAULT 0,
    CONSTRAINT FK_Attendance_Staff FOREIGN KEY (StaffId) REFERENCES dbo.Staff(StaffId),
    CONSTRAINT FK_Attendance_Shift FOREIGN KEY (ShiftId) REFERENCES dbo.Shift(ShiftId),
    CONSTRAINT UQ_Attendance_StaffDate UNIQUE (StaffId, AttendanceDate)
);

CREATE TABLE dbo.LeaveApplication (
    LeaveApplicationId BIGINT IDENTITY(1,1) PRIMARY KEY,
    StaffId BIGINT NOT NULL,
    FromDate DATE NOT NULL,
    ToDate DATE NOT NULL,
    LeaveType NVARCHAR(50) NOT NULL,
    Status NVARCHAR(20) NOT NULL DEFAULT 'Applied',
    Reason NVARCHAR(1000) NULL,
    CONSTRAINT FK_LeaveApplication_Staff FOREIGN KEY (StaffId) REFERENCES dbo.Staff(StaffId)
);

CREATE TABLE dbo.PostingHistory (
    PostingHistoryId BIGINT IDENTITY(1,1) PRIMARY KEY,
    StaffId BIGINT NOT NULL,
    FromInstitutionId INT NULL,
    ToInstitutionId INT NOT NULL,
    EffectiveDate DATE NOT NULL,
    Remarks NVARCHAR(500) NULL,
    CONSTRAINT FK_PostingHistory_Staff FOREIGN KEY (StaffId) REFERENCES dbo.Staff(StaffId),
    CONSTRAINT FK_PostingHistory_FromInstitution FOREIGN KEY (FromInstitutionId) REFERENCES dbo.Institution(InstitutionId),
    CONSTRAINT FK_PostingHistory_ToInstitution FOREIGN KEY (ToInstitutionId) REFERENCES dbo.Institution(InstitutionId)
);

CREATE TABLE dbo.AssetCategory (
    AssetCategoryId INT IDENTITY(1,1) PRIMARY KEY,
    CategoryCode NVARCHAR(30) NOT NULL UNIQUE,
    CategoryName NVARCHAR(200) NOT NULL,
    DefaultDepreciationRate DECIMAL(6,3) NULL
);

CREATE TABLE dbo.Asset (
    AssetId BIGINT IDENTITY(1,1) PRIMARY KEY,
    AssetCode NVARCHAR(50) NOT NULL UNIQUE,
    AssetName NVARCHAR(200) NOT NULL,
    AssetCategoryId INT NOT NULL,
    InstitutionId INT NOT NULL,
    DepartmentId INT NULL,
    PurchaseDate DATE NOT NULL,
    PurchaseValue DECIMAL(18,2) NOT NULL,
    ConditionStatus NVARCHAR(30) NOT NULL DEFAULT 'Good',
    LifecycleStatus NVARCHAR(30) NOT NULL DEFAULT 'Active',
    CONSTRAINT FK_Asset_AssetCategory FOREIGN KEY (AssetCategoryId) REFERENCES dbo.AssetCategory(AssetCategoryId),
    CONSTRAINT FK_Asset_Institution FOREIGN KEY (InstitutionId) REFERENCES dbo.Institution(InstitutionId),
    CONSTRAINT FK_Asset_Department FOREIGN KEY (DepartmentId) REFERENCES dbo.Department(DepartmentId)
);

CREATE TABLE dbo.MaintenanceRecord (
    MaintenanceRecordId BIGINT IDENTITY(1,1) PRIMARY KEY,
    AssetId BIGINT NOT NULL,
    MaintenanceDate DATE NOT NULL,
    VendorName NVARCHAR(200) NULL,
    Cost DECIMAL(18,2) NULL,
    Notes NVARCHAR(1000) NULL,
    CONSTRAINT FK_MaintenanceRecord_Asset FOREIGN KEY (AssetId) REFERENCES dbo.Asset(AssetId)
);

CREATE TABLE dbo.WorkOrder (
    WorkOrderId BIGINT IDENTITY(1,1) PRIMARY KEY,
    AssetId BIGINT NOT NULL,
    WorkOrderNo NVARCHAR(50) NOT NULL UNIQUE,
    IssueDate DATE NOT NULL,
    Status NVARCHAR(20) NOT NULL DEFAULT 'Open',
    Description NVARCHAR(1000) NULL,
    CONSTRAINT FK_WorkOrder_Asset FOREIGN KEY (AssetId) REFERENCES dbo.Asset(AssetId)
);

CREATE TABLE dbo.AssetTransfer (
    AssetTransferId BIGINT IDENTITY(1,1) PRIMARY KEY,
    AssetId BIGINT NOT NULL,
    FromDepartmentId INT NULL,
    ToDepartmentId INT NOT NULL,
    RequestedOn DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME(),
    ApprovedOn DATETIME2 NULL,
    Status NVARCHAR(20) NOT NULL DEFAULT 'Pending',
    CONSTRAINT FK_AssetTransfer_Asset FOREIGN KEY (AssetId) REFERENCES dbo.Asset(AssetId),
    CONSTRAINT FK_AssetTransfer_FromDepartment FOREIGN KEY (FromDepartmentId) REFERENCES dbo.Department(DepartmentId),
    CONSTRAINT FK_AssetTransfer_ToDepartment FOREIGN KEY (ToDepartmentId) REFERENCES dbo.Department(DepartmentId)
);

CREATE TABLE dbo.Depreciation (
    DepreciationId BIGINT IDENTITY(1,1) PRIMARY KEY,
    AssetId BIGINT NOT NULL,
    FiscalYear NVARCHAR(9) NOT NULL,
    OpeningValue DECIMAL(18,2) NOT NULL,
    DepreciationAmount DECIMAL(18,2) NOT NULL,
    ClosingValue DECIMAL(18,2) NOT NULL,
    CalculatedOn DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME(),
    CONSTRAINT FK_Depreciation_Asset FOREIGN KEY (AssetId) REFERENCES dbo.Asset(AssetId),
    CONSTRAINT UQ_Depreciation_Asset_Year UNIQUE (AssetId, FiscalYear)
);

CREATE TABLE dbo.VerificationLog (
    VerificationLogId BIGINT IDENTITY(1,1) PRIMARY KEY,
    AssetId BIGINT NOT NULL,
    VerifiedOn DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME(),
    VerifiedByUserId NVARCHAR(100) NOT NULL,
    PhysicalStatus NVARCHAR(30) NOT NULL,
    Remarks NVARCHAR(1000) NULL,
    CONSTRAINT FK_VerificationLog_Asset FOREIGN KEY (AssetId) REFERENCES dbo.Asset(AssetId)
);

CREATE TABLE dbo.Message (
    MessageId BIGINT IDENTITY(1,1) PRIMARY KEY,
    SenderUserId NVARCHAR(100) NOT NULL,
    Subject NVARCHAR(300) NOT NULL,
    Body NVARCHAR(MAX) NOT NULL,
    Priority NVARCHAR(10) NOT NULL,
    ParentMessageId BIGINT NULL,
    SentOn DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME(),
    CONSTRAINT FK_Message_Parent FOREIGN KEY (ParentMessageId) REFERENCES dbo.Message(MessageId)
);

CREATE TABLE dbo.MessageRecipient (
    MessageRecipientId BIGINT IDENTITY(1,1) PRIMARY KEY,
    MessageId BIGINT NOT NULL,
    RecipientUserId NVARCHAR(100) NOT NULL,
    RecipientScope NVARCHAR(30) NOT NULL,
    CONSTRAINT FK_MessageRecipient_Message FOREIGN KEY (MessageId) REFERENCES dbo.Message(MessageId)
);

CREATE TABLE dbo.Attachment (
    AttachmentId BIGINT IDENTITY(1,1) PRIMARY KEY,
    ModuleName NVARCHAR(30) NOT NULL,
    EntityId BIGINT NOT NULL,
    FileName NVARCHAR(260) NOT NULL,
    FilePath NVARCHAR(500) NOT NULL,
    ContentType NVARCHAR(100) NULL,
    UploadedByUserId NVARCHAR(100) NOT NULL,
    UploadedOn DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME()
);
CREATE INDEX IX_Attachment_Module_Entity ON dbo.Attachment(ModuleName, EntityId);

CREATE TABLE dbo.ReadReceipt (
    ReadReceiptId BIGINT IDENTITY(1,1) PRIMARY KEY,
    MessageId BIGINT NOT NULL,
    UserId NVARCHAR(100) NOT NULL,
    ReadOn DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME(),
    CONSTRAINT FK_ReadReceipt_Message FOREIGN KEY (MessageId) REFERENCES dbo.Message(MessageId),
    CONSTRAINT UQ_ReadReceipt_Message_User UNIQUE (MessageId, UserId)
);

CREATE TABLE dbo.ReportSubmission (
    ReportSubmissionId BIGINT IDENTITY(1,1) PRIMARY KEY,
    SubmittedByUserId NVARCHAR(100) NOT NULL,
    ReportType NVARCHAR(100) NOT NULL,
    ReportingPeriod NVARCHAR(20) NOT NULL,
    PayloadJson NVARCHAR(MAX) NOT NULL,
    SubmittedOn DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME(),
    Status NVARCHAR(20) NOT NULL DEFAULT 'Submitted'
);

CREATE TABLE dbo.AuditLog (
    AuditLogId BIGINT IDENTITY(1,1) PRIMARY KEY,
    ModuleName NVARCHAR(50) NOT NULL,
    EntityName NVARCHAR(100) NOT NULL,
    EntityId NVARCHAR(100) NOT NULL,
    ActionName NVARCHAR(50) NOT NULL,
    OldValues NVARCHAR(MAX) NULL,
    NewValues NVARCHAR(MAX) NULL,
    ActionByUserId NVARCHAR(100) NOT NULL,
    ActionByRole NVARCHAR(100) NULL,
    IPAddress NVARCHAR(64) NULL,
    ActionOn DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME()
);
CREATE INDEX IX_AuditLog_Module_ActionOn ON dbo.AuditLog(ModuleName, ActionOn DESC);

