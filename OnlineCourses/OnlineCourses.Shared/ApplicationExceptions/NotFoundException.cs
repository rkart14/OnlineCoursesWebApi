using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineCourses.Shared.ApplicationExceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException()
            : base()
        {

        }
        public NotFoundException(string message)
            : base(message)
        {

        }
        public NotFoundException(string message, Exception exception)
            : base(message, exception)
        {

        }
    }
}
