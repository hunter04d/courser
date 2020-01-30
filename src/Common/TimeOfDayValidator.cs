using FluentValidation;

namespace Common
{
    public class TimeOfDayValidator : AbstractValidator<TimeOfDay>
    {
        public TimeOfDayValidator()
        {
            RuleFor(day => day.Hour).InclusiveBetween((byte) 0, (byte) 23);
            RuleFor(day => day.Minute).InclusiveBetween((byte) 0, (byte) 59);
        }
    }
}
