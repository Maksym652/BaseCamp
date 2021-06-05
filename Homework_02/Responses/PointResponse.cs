namespace WebApp.Api.Responses
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using WebApp.Core.Models;

    public class PointResponse
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PointResponse"/> class.
        /// </summary>
        /// <param name="mark">Point on a 100-point scale.</param>
        /// <param name="studentId">ID of the student.</param>
        /// <param name="subject">Name of the subject.</param>
        /// <param name="date">Date when student has got a point.</param>
        /// <param name="task">Name of completed by student task, raited with this point.</param
        public PointResponse(float mark, int studentId, string subject, DateTime date, string task = "")
        {
            this.Mark = mark;
            this.Subject = subject;
            this.StudentId = studentId;
            this.Date = date;
            this.Task = task;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PointResponse"/> class from Point object.
        /// </summary>
        /// <param name="point">Point object</param>
        public PointResponse(Point point)
        {
            this.Mark = point.Mark;
            this.Subject = point.Subject;
            this.StudentId = point.StudentId;
            this.Date = point.Date;
            this.Task = point.Task;
        }

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

        /// <summary>
        /// Gets or sets a name of the task, completed by student and rated by the Mark. For example "class work", "homework", "final test" etc.
        /// </summary>
        public string Task { get; set; }
    }
}
