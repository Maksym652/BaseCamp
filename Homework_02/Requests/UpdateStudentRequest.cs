using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Api.Requests
{
    public class UpdateStudentRequest
    {
        public string NewPassword { get; set; }

        /// <summary>
        /// Gets or sets student's group.
        /// </summary>
        public int Group { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether student is studying on a budget (for free).
        /// </summary>
        public bool IsStudiedOnBudget { get; set; }


        UpdateStudentRequest(string newPassword, int group, bool isStudyingOnBudget)
        {
            this.NewPassword = newPassword;
            this.Group = group;
            this.IsStudiedOnBudget = isStudyingOnBudget;
        }

        UpdateStudentRequest() { }
    }
}
