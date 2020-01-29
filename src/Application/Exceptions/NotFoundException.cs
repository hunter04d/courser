using System;

namespace Application.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string name, object key)
            : base($"Entity \"{name}\" with key: ({key}) not found.")
        {
        }
    }
}
