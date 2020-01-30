using System;

namespace Application.Exceptions
{
    /// <summary>
    /// Invoked when entity was not found in the system
    /// </summary>
    public class NotFoundException : Exception
    {
        public NotFoundException(string name, object key)
            : base($"Entity \"{name}\" with key: ({key}) not found.")
        {
        }
    }
}
