using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using testApp.Models;
using testApp.Repositories;

namespace testApp.Services
{
    public class EmployeeService
    {
       // private static List<Employee> Employees = new List<Employee>();
        EmployeeRepository rep = new EmployeeRepository();
        public List<Employee> GetEmployees() 
        {
            return rep.Find();
        }
        public Employee GetEmployeeById(ObjectId id)
        {
            return rep.FindById(id);
        }
        public bool AddEmployee(Employee employee)
        {
            rep.AddEmployee(employee);
            return true;
        }
        public void UpdateEmployee(Employee employee)
        {
            rep.UpdateEmployee(employee);
        }
        public void DeleteEmployee(ObjectId id) 
        {
            rep.DeleteEmployee(id);
        }
    }
}