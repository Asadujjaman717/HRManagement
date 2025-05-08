namespace HrManagement.Repository.IRepository
{
    public interface IUnitOfWork
    {
        ICompanyRepository Company  { get; }
        IDesignationRepository Designation { get; }
        IDepartmentRepository Department { get; }
        IShiftRepository Shift { get; }
        IEmployeeRepository Employee { get; }
        IAttendanceRepository Attendance { get; }
        IAttendanceSummaryRepository AttendanceSummary { get; }
        ISalaryRepository Salary { get; }
        //IReportRepository ReportRepository { get; }
        Task SaveAsync();

    }
}
