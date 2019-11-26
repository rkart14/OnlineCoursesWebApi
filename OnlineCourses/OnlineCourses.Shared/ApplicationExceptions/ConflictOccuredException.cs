using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineCourses.Shared.ApplicationExceptions
{
    public class ConflictOccuredException : Exception
    {
        public ConflictOccuredException()
         : base()
        {

        }
        public ConflictOccuredException(string message)
            : base(message)
        {

        }
        public ConflictOccuredException(string message, Exception exception)
            : base(message, exception)
        {

        }
    }
}
