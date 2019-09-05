using System;
using System.Runtime.Serialization;
using System.Text;

namespace Stryker.Core.Exceptions
{
    /// <summary>
    /// Represents errors which are related to roslyn compilation errors that we cannot fix, 
    /// but the user might also might not be able to fix
    /// </summary>
    [Serializable]
    public class StrykerCompilationException : Exception
    {
        public StrykerCompilationException()
            : base()
        {
        }

        public StrykerCompilationException(string message)
            : base(message)
        {
        }

        public StrykerCompilationException(string message, string details)
            : base(message)
        {
            Details = details;
        }

        public StrykerCompilationException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected StrykerCompilationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public string Details { get; }

        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.AppendLine();
            builder.AppendLine();
            builder.AppendLine(Message);
            builder.AppendLine();
            if (!string.IsNullOrEmpty(Details))
            {
                builder.AppendLine(Details);
            }
            return builder.ToString();
        }

    }
}