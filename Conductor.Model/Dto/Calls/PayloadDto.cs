using Conductor.Model.Dto.Calls.Rooms;

namespace Conductor.Model.Dto.Calls
{
    public class PayloadDto(
        RoomInfoDto? RoomInfo, 
        ErrorDto? Error, 
        UserDataDto? UserData, 
        ParticipantMediaStateDto? ParticipantMediaState,
        object? Sdp,
        object? Candidate,
        string? Emotion);
    
}
