using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace Application.Exceptions
{
    /// <summary>
    /// Represents the bad request to the system
    /// </summary>
    public class BadRequestException : Exception
    {
        public static BadRequestException From(Exception e)
        {
            return new BadRequestException(e.Message);
        }

        public BadRequestException(string error) : base($"bad request: {error}")
        {
            var builder = ImmutableDictionary.CreateBuilder<string, string[]>();
            builder.Add(string.Empty, new[] {error});
            Errors = builder.ToImmutable();
        }

        public BadRequestException(IDictionary<string, string[]> errors) :
            base($"bad request: {string.Join(", ", errors.Values.SelectMany(s => s))}")
        {
            Errors = errors;
        }


        public IDictionary<string, string[]> Errors { get; }
    }
}
