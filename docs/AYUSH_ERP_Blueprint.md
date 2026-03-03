# AYUSH ERP – Solution Blueprint

## 1. Vision
AYUSH ERP is a centralized, role-based web portal for National AYUSH Mission institutions to manage finance, projects, patient services, HR, assets, and communications with full traceability.

## 2. Proposed Architecture (Clean Architecture for ASP.NET Core 8)

### Layers
- **Domain**
  - Core entities, enums, value objects, business rules
- **Application**
  - Use cases, DTOs, interfaces, validators, workflow orchestration
- **Infrastructure**
  - EF Core persistence, repositories, identity, file storage, reporting adapters, audit sink
- **Web**
  - ASP.NET Core MVC/Razor + REST APIs + JWT/Cookie authentication + role-based policies

### Cross-cutting concerns
- Serilog structured logging
- Audit trail for create/update/approve/reject/upload actions
- Encryption for sensitive fields
- Caching for dashboards and lookups
- Background jobs for monthly rollups and report snapshots

## 3. Role Matrix

| Role | Primary Modules | Privileges |
|---|---|---|
| State Budget Officer | BDAS | Review/approve demands, allocation, utilization analytics |
| Site Engineer | RTMS | Project creation, milestone updates, upload progress photos |
| AYUSH Doctor | PMS | Register patients, visits, diagnosis, prescriptions |
| HR Manager | RTSMS | Staff profile, shifts, attendance, leave, transfer |
| Asset & Store Manager | AMS | Asset lifecycle, maintenance, verification, disposal |
| District AYUSH Officer | BDAS, RTMS, e-Connect | District approvals, monitoring, communications |
| State Admin | All | User/role management, statewide dashboards, configuration |

## 4. Module Breakdown

### 4.1 BDAS (Budget & Demand Allocation)
Workflow:
1. Institution submits demand request
2. District AYUSH Officer reviews and recommends
3. State Budget Officer approves/rejects
4. Finance Authority final decision
5. Allocation released
6. Utilization certificate and evidence uploaded

Key KPIs:
- Allocation vs utilization percentage
- Pending approvals by level
- Category-wise demand trend (last 6 months)

### 4.2 RTMS (Real-Time Construction Monitoring)
- Project with sanctioned budget and timelines
- Milestone plan per project
- Daily/periodic progress entry with media uploads
- Auto delay flags when expected date crosses without completion
- Estimated vs actual cost delta

### 4.3 PMS (Patient Management)
- Global unique patient identifier
- Patient profile, allergy, blood group
- Visit, diagnosis, prescription records
- OPD count rollup monthly
- Feedback scoring for service quality dashboard

### 4.4 RTSMS (Staff Monitoring)
- Staff profiles and department mapping
- Shift planning and attendance punches
- Late arrival computation
- Leave workflow and posting/transfer history
- 30-day attendance analytics and monthly summaries

### 4.5 AMS (Asset Management)
- Unique asset tagging and category
- Department allocation
- Condition and lifecycle state changes
- Maintenance/work order history
- Transfer and disposal workflow with approval
- Depreciation and periodic physical verification logs

### 4.6 e-Connect (Communication)
- Hierarchical circular delivery (State→District→Institution)
- Priority and read receipts
- Threaded communication
- Structured report submissions
- Compliance monitoring

## 5. Dashboard Design

### Role dashboards
Each role dashboard should include:
- KPI cards
- Pending tasks grid
- Alerts panel
- Trend charts (line/bar/pie)
- Live refresh widgets

### State Admin composite dashboard
- Fund Utilization %
- Active Construction Projects
- Total Patients This Month
- Staff Attendance %
- Asset Health Distribution
- Communication compliance and unread urgent messages

## 6. Security Model

- Authentication: JWT for API + Cookie for MVC session
- Authorization: policy-based role authorization
- Password hashing: ASP.NET Core Identity defaults (PBKDF2/Argon option)
- Data protection: encrypt sensitive fields at rest
- Audit: action + entity + before/after + actor + timestamp + IP
- Session controls: timeout + refresh token revocation + lockout policy

## 7. Reporting Pack

Required exports (PDF/Excel):
- Budget reports
- Project delay reports
- Monthly OPD reports
- Attendance summaries
- Asset verification and depreciation
- Communication compliance report

## 8. Scalability & Deployment

- SQL Server with indexing and partitioning strategy for heavy log tables
- Horizontal app scaling via stateless web tier
- Background jobs for scheduled report generation
- Azure-ready deployment architecture
- Daily DB backup + PITR strategy
- Uptime objective 99%

## 9. Suggested Build Phases

1. Foundation (auth, roles, audit, shell dashboards)
2. BDAS + RTMS implementation
3. PMS + RTSMS implementation
4. AMS + e-Connect implementation
5. Reporting, optimization, security hardening, UAT

