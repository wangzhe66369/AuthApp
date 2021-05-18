using System;

namespace AuthApp.ServiceExtensions
{
    public static class DateTimeOffsetExtension
    {
        public static int GetCurrentAge(this DateTimeOffset dateTimeOffset)
        {
            var currentDate = DateTimeOffset.Now;
            int age = currentDate.Year - dateTimeOffset.Year;

            if (currentDate < dateTimeOffset.AddYears(age))
            {
                age--;
            }

            return age;
        }
    }
}