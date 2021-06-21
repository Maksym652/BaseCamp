namespace WebApp.Core.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using WebApp.Core.Enums;

    /// <summary>
    /// Represents a student of the university.
    /// </summary>
    public class Student : User
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Student"/> class.
        /// </summary>
        /// <param name="id">Student id.</param>
        /// <param name="name">Student name.</param>
        /// <param name="group">Student group.</param>
        /// <param name="specialty">Student specialty.</param>
        /// <param name="isStudiedOnBudget">Bool value showing if student is studying on budget (for free).</param>
        public Student(string login, string password, int id, string name, int group, int specialty, bool isStudiedOnBudget) : base(login, password, UserRole.STUDENT)
        {
            this.Id = id;
            this.Name = name;
            this.Group = group;
            this.Specialty = specialty;
            this.IsStudyingOnBudget = isStudiedOnBudget;
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
