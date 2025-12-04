namespace Conductor.Model.Dto.Calls.Rooms
{
    public record RoomInfoDto(Guid RoomId, string Name, List<Guid> Participants);
}
