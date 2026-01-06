using AutoMapper;
using AutoMapper.QueryableExtensions;
using EmployeesApi.Data;
using EmployeesApi.DTOs;
using EmployeesApi.Models;
using EmployeesApi.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EmployeesApi.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        public EmployeeRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task AddAsync(Employee employee)
        {
            _db.Employees.Add(employee);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _db.Employees.FindAsync(id);
            if (entity == null) return;
            _db.Employees.Remove(entity);
            await _db.SaveChangesAsync();
        }

        //private static IQueryable<T> ApplySort<T>(IQueryable<T> query, string? sortBy, string? sortDir)
        //{
        //    if (string.IsNullOrWhiteSpace(sortBy)) return query;
        //    var prop = typeof(T).GetProperty(sortBy, System.Reflection.BindingFlags.IgnoreCase | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
        //    if (prop == null) return query;

        //    return sortDir?.ToLower() == "desc"
        //        ? query.OrderByDescending(e => EF.Property<object>(e, prop.Name))
        //        : query.OrderBy(e => EF.Property<object>(e, prop.Name));
        //}

        public async Task<PagedResult<Employee>> GetAllAsync(string? search = null, string? department = null, string? sortBy = "Id", string? sortDir = "asc", int page = 1, int pageSize = 10)
        {
            var query = _db.Employees.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                var s = search.ToLower();
                query = query.Where(e => (!string.IsNullOrEmpty(e.Name) && e.Name.ToLower().Contains(s)) || (!string.IsNullOrEmpty(e.Surname) && e.Surname.ToLower().Contains(s)));
            }

            if (!string.IsNullOrWhiteSpace(department))
            {
                var d = department.ToLower();
                query = query.Where(e => !string.IsNullOrEmpty(e.Department.Name) && e.Department.Name.ToLower() == d);
            }

            var totalCount = await query.CountAsync();

            var employees = await query
                .Include(e => e.Department)
                .OrderBy(e => e.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResult<Employee>
            {
                Items = employees,
                TotalCount = totalCount,
                Page = page,
                PageSize = pageSize
            };
        }

        public async Task<int> GetCountAsync(string? search = null, string? department = null)
        {
            var query = _db.Employees.AsQueryable();

            if(!string.IsNullOrWhiteSpace(search))
            {
                var s = search.ToLower();
                query = query.Where(e => (!string.IsNullOrEmpty(e.Name) && e.Name.ToLower().Contains(s))
                                     || (!string.IsNullOrEmpty(e.Surname) && e.Surname.ToLower().Contains(s)));
            }
            if (!string.IsNullOrWhiteSpace(department))
            {
                var d = department.ToLower();
                query = query.Where(e => !string.IsNullOrEmpty(e.Department.Name) && e.Department.Name.ToLower() == d);
            }
            return await query.CountAsync();
        }
        public Task<Employee?> GetByIdAsync(int id) => _db.Employees.Include(e => e.Department).FirstOrDefaultAsync(e => e.Id == id);

        public async Task UpdateAsync(Employee employee)
        {
            _db.Employees.Update(employee);
            await _db.SaveChangesAsync();
        }
    }
}
