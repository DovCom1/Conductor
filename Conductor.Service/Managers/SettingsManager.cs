using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Conductor.Dto.Settings;
using Conductor.Model.Constants;
using Conductor.Model.Dto.Settings;
using Conductor.Model.Interfaces.Managers;
using Conductor.Model.Interfaces.Services;
using Conductor.Models;
using Conductor.Models.Interfaces.Managers;

namespace Conductor.Managers
{
    public class SettingsManager : ISettingsManager
    {
        private readonly IRouteService routeService;

        public SettingsManager(IRouteService routeService)
        {
            this.routeService = routeService;
        }

        public async Task<Result<EnemySettingsDto>> GetEnemySettings(Guid userId, Guid enemyId)
        {
            return await routeService.GetAsync<EnemySettingsDto>(Constants.SettingsServiceName, 
                $"/api/settings/{userId}/enemies/{enemyId}");
        }

        public async Task<Result> SetEnemySettings(EnemySettingsRequest request)
        {
            return await routeService.PostAsync<EnemySettingsRequest>(request, Constants.SettingsServiceName,
                $"/api/settings/enemy");
        }

        public async Task<Result<MicrophoneVolumeDto>> SetMicrophoneVolume(Guid userId, MicrophoneVolumeRequest request)
        {
            return await routeService.PostAsync<MicrophoneVolumeRequest, MicrophoneVolumeDto>(request, Constants.SettingsServiceName,
                $"/api/settings/{userId}/set-microphone-volume");
        }

        public async Task<Result<MicrophoneVolumeDto>> GetMicrophoneVolume(Guid userId, Guid interlocutorId)
        {
            return await routeService.GetAsync<MicrophoneVolumeDto>(Constants.SettingsServiceName,
                $"/api/settings/{userId}/microphone-volume/{interlocutorId}");
        }

        public async Task<Result<WorkSettings>> SetMicrophoneWorkSettings(Guid userId, Guid interlocutorId, WorkSettings request)
        {
            return await routeService.PostAsync<WorkSettings, WorkSettings>(request, Constants.SettingsServiceName,
                $"/api/settings/{userId}/turn-microphone/{interlocutorId}");
        }

        public async Task<Result<WorkSettings>> GetMicrophoneWorkSettings(Guid userId, Guid interlocutorId)
        {
            return await routeService.GetAsync<WorkSettings>(Constants.SettingsServiceName,
                $"/api/settings/{userId}/microphone-status/{interlocutorId}");
        }

        public async Task<Result<WorkSettings>> SetCameraWorkSettings(Guid userId, Guid interlocutorId, WorkSettings request)
        {
            return await routeService.PostAsync<WorkSettings, WorkSettings>(request, Constants.SettingsServiceName,
                $"/api/settings/{userId}/turn-video/{interlocutorId}");
        }

        public async Task<Result<WorkSettings>> GetCameraWorkSettings(Guid userId, Guid interlocutorId)
        {
            return await routeService.GetAsync<WorkSettings>(Constants.SettingsServiceName,
                $"/api/settings/{userId}/video-status/{interlocutorId}");
        }
    }
}
