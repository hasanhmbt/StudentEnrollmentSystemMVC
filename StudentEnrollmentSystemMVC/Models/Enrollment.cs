﻿namespace StudentEnrollmentSystemMVC.Models;

public class Enrollment
{
    public int Id { get; set; }
    public int StudentId { get; set; }
    public int CourseId { get; set; }
    public Grade? Grade { get; set; }

    public Student Student { get; set; }
    public Course Course { get; set; }
}

public enum Grade
{
    A, B, C, D, F
}

