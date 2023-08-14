using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EfCoreTutorial.Data.Context;
using EfCoreTutorial.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EfCoreTutorial.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StudentController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    public StudentController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        StudentFilter filter = new StudentFilter() { FirstName = "Sarah"};
        var students = _context.Students.AsQueryable();
        if (!String.IsNullOrEmpty(filter.FirstName))
        {
            students = students.Where(i => i.FirstName == filter.FirstName);
        }
        if (!String.IsNullOrEmpty(filter.LastName))
        {
            students = students.Where(i => i.LastName == filter.LastName);
        }
        if (filter.Number.HasValue)
        {
            students = students.Where(i => i.Number == filter.Number);
        }

        var list = await students.ToListAsync();
        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> Add()
    {
        
        var student1 = new Student()
        {
            FirstName = "Hakan",
            LastName = "Yılmaz",
            Number = 1,
            Address = new StudentAddress()
            {
                City = "İstanbul",
                Country = "Türkiye",
                District = "Kadıköy",
                FullAddress = "Kadıköy/İstanbul",
            }
        };
        await _context.Students.AddAsync(student1);
        await _context.SaveChangesAsync();
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var student = await _context.Students.FindAsync(id);
        if (student != null) _context.Students.Remove(student);
        await _context.SaveChangesAsync();
        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> Update()
    {
        var student = await _context.Students.FirstOrDefaultAsync();
        if (student != null)
        {
            student.FirstName = "HakanXxxX";
            student.LastName = "YılmazXxxX";
        }

        await _context.SaveChangesAsync();
        return Ok();
    }
}


