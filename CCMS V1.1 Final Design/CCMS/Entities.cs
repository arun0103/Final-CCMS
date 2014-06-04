using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CCMS
{

    public class Faculty
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Contact { get; set; }
        public bool Active { get; set; }
    }
    public class Class
    {
        public string ClassName { get; set; }
        public string Year { get; set; }
        public string Semester { get; set; }
        public string Section { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModifiedDate { get; set; }

    }

    public class Routine
    {
        public int Fid { get; set; }
        public int ClassId { get; set; }
        public string EnrollYear { get; set; }
        public string Semester { get; set; }
        public string SubjectID { get; set; }
        public string SectionName { get; set; }
    }

    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Contact { get; set; }
        public bool Active { get; set; }
        public string Role { get; set; }
        public string Password { get; set; }
    }
}