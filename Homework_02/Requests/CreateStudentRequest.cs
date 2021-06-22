namespace WebApp.Api.Requests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class CreateStudentRequest
    {
        /// <summary>
        /// Gets or sets student's login.
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// Gets or sets student's password.
        /// </summary>
        public string Password { get; set; }

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

        CreateStudentRequest() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateStudentRequest"/> class.
        /// </summary>
        public CreateStudentRequest(string login, string password, string name, int group, int specialty, bool isStudyingOnBudget)
        {
            this.Login = login;
            this.Password = password;
            this.Name = name;
            this.Group = group;
            this.IsStudiedOnBudget = isStudyingOnBudget;
            this.Specialty = specialty;
        }
    }
}
