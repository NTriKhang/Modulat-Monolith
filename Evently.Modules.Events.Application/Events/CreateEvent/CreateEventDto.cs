namespace Evently.Modules.Events.Application.Events.CreateEvent
{
    public sealed record CreateEventDto
    {
        public Guid CategoryId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Location { set; get; }
        public DateTime StartsAtUtc { get; set; }
        public DateTime? EndsAtUtc { get; set; }
    }
}

