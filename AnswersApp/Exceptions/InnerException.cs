using System;

namespace AnswersApp.Exceptions
{
    public class InnerException: Exception
    {
        public InnerException(string message): base(message) { }
    }
}