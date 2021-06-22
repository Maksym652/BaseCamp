namespace WebApp.Api.Requests
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class CreatePointRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreatePointRequest"/> class.
        /// </summary>
        /// <param name="mark">Point on a 100-point scale.</param>
        /// <param name="studentId">ID of the student.</param>
        /// <param name="subject">Name of the subject.</param>
        /// <param name="task">Name of the task completed by the student.</param>
        public CreatePointRequest(float mark, int studentId, string subject, string task = "")
        {
            this.Mark = mark;
            this.Subject = subject;
            this.StudentId = studentId;
            this.Task = task;
        }

        public CreatePointRequest() { }

        /// <summary>
        /// Gets or sets a point on a 100-point scale.
        /// </summary>
        [Required]
        [Range(0, 100)]
        public float Mark { get; set; }

        /// <summary>
        /// Gets or sets an ID of the student, who has got a point.
        /// </summary>
        [Required]
        [Range(0, int.MaxValue)]
        public int StudentId { get; set; }

        /// <summary>
        /// Gets or sets a name of the subject.
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string Subject { get; set; }

        /// <summary>
        /// Gets or sets a name of the task, completed by student and rated by the Mark. For example "class work", "homework", "final test" etc.
        /// </summary>
        [MaxLength(100)]
        public string Task { get; set; }
    }
}
