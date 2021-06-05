namespace WebApp.Api.Requests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class CreateStudentRequest
    {
        /// <summary>
        /// Gets or sets student's name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets student's group.
        /// </summary>
        public int Group { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether student is studying on a budget (for free).
        /// </summary>
        public bool IsStudiedOnBudget { get; set; }

        /// <summary>
        /// Gets or sets student's specialty.
        /// </summary>
        public int Specialty { get; set; }

        CreateStudentRequest(string name, int group, int specialty, bool isStudyingOnBudget)
        {
            this.Name = name;
            this.Group = group;
            this.IsStudiedOnBudget = isStudyingOnBudget;
            this.Specialty = specialty;
        }
    }
}
