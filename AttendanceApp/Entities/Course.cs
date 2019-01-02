namespace AttendanceApp.Entities
{
    class Course
    {
       
        public int CourseId { get; }
        public string Name { get; set; }

        public Course(int courseId, string courseName)
        {
            this.CourseId = courseId;
            this.Name = courseName;
        }

        public override string ToString() => this.Name;
    }
}
