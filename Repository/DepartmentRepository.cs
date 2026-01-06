using EmployeesApi.Data;
using EmployeesApi.Models;
using EmployeesApi.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace EmployeesApi.Repository
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly ApplicationDbContext _db;

        public DepartmentRepository(ApplicationDbContext db) => _db = db;
        public async Task AddAsync(Department department)
        {
            _db.Departments.Add(department);
            await _db.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var entity = await _db.Departments.FindAsync(id);
            if (entity == null) return;
            _db.Departments.Remove(entity);
            await _db.SaveChangesAsync();
        }
        public async Task<PagedResult<Department>> GetAllAsync(string? sortBy = "Id", string? sortDir = "asc", int page = 1, int pageSize = 10)
        {
            var query = _db.Departments.AsQueryable();

            var totalCount = await query.CountAsync();

            var departments = await query
                .OrderBy(e => e.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResult<Department>
            {
                Items = departments,
                TotalCount = totalCount,
                Page = page,
                PageSize = pageSize
            };
        }
        public async Task<Department?> GetByIdAsync(int id)
        {
            return await _db.Departments.FindAsync(id);
        }

        public async Task<Department?> UpdateAsync(Department department)
        {
            var existing = await _db.Departments.FindAsync(department.Id);
            if (existing == null) return null;

            existing.Name = department.Name;

            await _db.SaveChangesAsync();
            return existing;
        }
    }
}
