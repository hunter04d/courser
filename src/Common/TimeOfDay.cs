using System;
using System.Collections.Generic;

namespace Common
{
    /// <summary>
    /// Primitive type that describes a time of day
    /// </summary>
    public class TimeOfDay : IEquatable<TimeOfDay>, IComparable<TimeOfDay>
    {
        public TimeOfDay()
        {
        }

        public TimeOfDay(byte hour, byte minute)
        {
            Hour = hour;
            Minute = minute;
        }

        /// <summary>
        /// Hour of the time
        /// </summary>
        public byte Hour { get;  set; }
        
        /// <summary>
        /// Minute of the time
        /// </summary>
        public byte Minute { get; set; }

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

        public static bool operator <(TimeOfDay? left, TimeOfDay? right) =>
            Comparer<TimeOfDay>.Default.Compare(left, right) < 0;

        public static bool operator >(TimeOfDay? left, TimeOfDay? right) =>
            Comparer<TimeOfDay>.Default.Compare(left, right) > 0;

        public static bool operator <=(TimeOfDay? left, TimeOfDay? right) =>
            Comparer<TimeOfDay>.Default.Compare(left, right) <= 0;

        public static bool operator >=(TimeOfDay? left, TimeOfDay? right) =>
            Comparer<TimeOfDay>.Default.Compare(left, right) >= 0;

        public int CompareTo(TimeOfDay? other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;
            if (Hour == other.Hour)
            {
                return Minute.CompareTo(other.Minute);
            }

            return Hour.CompareTo(other.Hour);
        }
    }
}
