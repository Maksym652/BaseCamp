namespace WebApp.Api.Requests
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Threading.Tasks;

    public class UpdatePointRequest
    {
        public UpdatePointRequest(float mark, string task = "")
        {
            this.Mark = mark;
            this.Task = task;
        }

        public UpdatePointRequest() { }

        /// <summary>
        /// Gets or sets a point on a 100-point scale.
        /// </summary>
        [Required]
        [Range(0,100)]
        public float Mark { get; set; }

        /// <summary>
        /// Gets or sets a name of the task, completed by student and rated by the Mark. For example "class work", "homework", "final test" etc.
        /// </summary>
        [MaxLength(100)]
        public string Task { get; set; }
    }
}
