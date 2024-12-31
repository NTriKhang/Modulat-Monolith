using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evently.Modules.Events.Application.Events
{
    internal static class EventValidatorErrors
    {
        public static string EventIdNotNull { get; } = "Event identifier can't be empty";
        public static string TitleNotNull { get; } = "Title cannot be empty";
        public static string DescriptionNotNulL { get; } = "Description cannot be empty";
        public static string LocationNotNull { get; } = "Location cannot be empty";
        public static string StartsAtNotNull { get; } = "Start date cannot be empty";
        /// <summary>
        /// Throw when: EndsAtUtc < StartsAtUtc
        /// </summary>
        public static string EndDateProceedStartDate { get; } = "The event end date precedes the start date";
        public static string PageSizeInvalid { get; } = "Page size must be greater than 0";
        public static string PageInvalid { get; } = "Page must be greater than 0";
    }
}
