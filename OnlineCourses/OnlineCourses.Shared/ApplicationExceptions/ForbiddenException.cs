using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineCourses.Shared.ApplicationExceptions
{
    public class ForbiddenException : Exception
    {
        public ForbiddenException()
         : base()
        {

        }
        public ForbiddenException(string message)
            : base(message)
        {

        }
        public ForbiddenException(string message, Exception exception)
            : base(message, exception)
        {

        }
    }
}
