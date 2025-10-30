using Conductor.Dto.Settings;
using Conductor.Model.Dto.Settings;
using Conductor.Model.Interfaces.Services;
using Conductor.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conductor.Model.Interfaces.Managers
{
    public interface ISettingsManager
    {
        Task<Result<EnemySettingsDto>> GetEnemySettings(Guid userId, Guid enemyId);

        Task<Result> SetEnemySettings(EnemySettingsRequest request);

        Task<Result<MicrophoneVolumeDto>> SetMicrophoneVolume(Guid userId, MicrophoneVolumeRequest request);

        Task<Result<MicrophoneVolumeDto>> GetMicrophoneVolume(Guid userId, Guid interlocutorId);

        Task<Result<WorkSettings>> SetMicrophoneWorkSettings(Guid userId, Guid interlocutorId, WorkSettings request);

        Task<Result<WorkSettings>> GetMicrophoneWorkSettings(Guid userId, Guid interlocutorId);

        Task<Result<WorkSettings>> SetCameraWorkSettings(Guid userId, Guid interlocutorId, WorkSettings request);

        Task<Result<WorkSettings>> GetCameraWorkSettings(Guid userId, Guid interlocutorId);
    }
}
