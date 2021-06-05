namespace WebApp.Api.Responses
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using WebApp.Core.Models;

    /// <summary>
    /// .
    /// </summary>
    public class StudentResponse
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StudentResponse"/> class.
        /// </summary>
        /// <param name="id">Student id.</param>
        /// <param name="name">Student name.</param>
        /// <param name="group">Student group.</param>
        /// <param name="specialty">Student specialty.</param>
        /// <param name="isStudiedOnBudget">Bool value showing if student is studying on budget (for free).</param>
        public StudentResponse(int id, string name, int group, int specialty, bool isStudiedOnBudget)
        {
            this.Id = id;
            this.Name = name;
            this.Group = group;
            this.Specialty = specialty;
            this.IsStudyingOnBudget = isStudiedOnBudget;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StudentResponse"/> class from Student object.
        /// </summary>
        /// <param name="st">Student object.</param>
        public StudentResponse(Student st)
        {
            this.Id = st.Id;
            this.Name = st.Name;
            this.Group = st.Group;
            this.Specialty = st.Specialty;
            this.IsStudyingOnBudget = st.IsStudyingOnBudget;
        }

        /// <summary>
        /// Gets or sets student's ID.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets student's name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets student's group.
        /// </summary>
        public int Group { get; set; }

        /// <summary>
        /// Gets or sets student's specialty.
        /// </summary>
        public int Specialty { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether student is studying on a budget (for free).
        /// </summary>
        public bool IsStudyingOnBudget { get; set; }
    }
}
