namespace Homework_02
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// Represents a student's point.
    /// </summary>
    public class Point
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Point"/> class.
        /// </summary>
        /// <param name="id">ID of the point.</param>
        /// <param name="mark">Point on a 100-point scale.</param>
        /// <param name="studentId">ID of the student.</param>
        /// <param name="subject">Name of the subject.</param>
        /// <param name="date">Date when student has got a point..</param>
        public Point(int id, float mark, int studentId, string subject, DateTime date)
        {
            this.Id = id;
            this.Mark = mark;
            this.Subject = subject;
            this.StudentId = studentId;
            this.Date = date;
        }

        /// <summary>
        /// Gets or sets an ID of the point.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets a point on a 100-point scale.
        /// </summary>
        public float Mark { get; set; }

        /// <summary>
        /// Gets or sets an ID of the student, who has got a point.
        /// </summary>
        public int StudentId { get; set; }

        /// <summary>
        /// Gets or sets a name of the subject.
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// Gets or sets a date when student has got a point.
        /// </summary>
        public DateTime Date { get; set; }
    }
}
