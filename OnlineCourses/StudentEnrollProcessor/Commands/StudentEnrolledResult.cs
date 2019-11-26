using System;
using System.Collections.Generic;
using System.Text;

namespace StudentEnrollProcessor.Commands
{
    public class StudentEnrolledResult
    {
        public string StudentEmail { get; set; }

        public bool EnrollSucceed { get; set; }
    }
}
