using System;

namespace Domain.Entities
{
    public class TimeOfDay : IEquatable<TimeOfDay>
    {
        public TimeOfDay(byte hour, byte minute)
        {
            Hour = hour;
            Minute = minute;
        }

        public byte Hour { get; private set; }
        public byte Minute { get; private set; }

        public bool Equals(TimeOfDay? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Hour == other.Hour && Minute == other.Minute;
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((TimeOfDay) obj);
        }

        public override int GetHashCode() => HashCode.Combine(Hour, Minute);

        public static bool operator ==(TimeOfDay? left, TimeOfDay? right) => Equals(left, right);

        public static bool operator !=(TimeOfDay? left, TimeOfDay? right) => !Equals(left, right);
    }
}
