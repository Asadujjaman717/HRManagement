
using HrManagement.Data;
using HrManagement.Repository.IRepository;
using HrManagement.Repository;
using HrManagement.Models;

namespace HrManagement.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _Context;

        public UnitOfWork(AppDbContext context)
        {
            _Context = context;
            Company = new CompanyRepository(_Context);
            Designation = new DesignationRepository(_Context);
            Department = new DepartmentRepository(_Context);
            Shift = new ShiftRepository(_Context);
            Employee = new EmployeeRepository(_Context);
            Attendance = new AttendanceRepository(_Context);
            AttendanceSummary = new AttendanceSummaryRepository(_Context);
            Salary = new SalaryRepository(_Context);
           
        }

        public ICompanyRepository Company { get; private set; }
        public IDesignationRepository Designation { get; private set; }
        public IDepartmentRepository Department { get; private set; }
        public IShiftRepository Shift { get; private set; }
        public IEmployeeRepository Employee { get; private set; }
        public IAttendanceRepository Attendance { get; private set; }
        public IAttendanceSummaryRepository AttendanceSummary { get; private set; }
        public ISalaryRepository Salary { get; private set; }
  
        public async Task SaveAsync() => await _Context.SaveChangesAsync();
    }
}
