namespace VTools.BadDayPostAggregate.Commands;

public record CreateBadDayPostCommand(Guid Id, string Url, string InstagramId);