using Conductor.Model.Dto.Settings;
using Conductor.Model.Interfaces.Managers;
using Microsoft.AspNetCore.Mvc;

namespace ConductorApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SettingsController: ControllerBase
    {
        private readonly ISettingsManager _settingsManager;

        public SettingsController(ISettingsManager settingsManager)
        {
            _settingsManager = settingsManager;
        }

        [HttpGet("{userId}/enemies/{enemyId}")]
        public async Task<IActionResult> GetEnemySettings(Guid userId, Guid enemyId)
        {
            var result = await _settingsManager.GetEnemySettings(userId, enemyId);
            return !result.IsSuccess ? StatusCode(result.StatusCode, $"An error occurred: {result.Error}") : Ok(result.Data);
        }

        [HttpPost("enemy")]
        public async Task<IActionResult> SetEnemySettings([FromBody] EnemySettingsRequest request)
        {
            var result = await _settingsManager.SetEnemySettings(request);
            return !result.IsSuccess ? StatusCode(result.StatusCode, $"An error occurred: {result.Error}") : Ok();
        }

        [HttpPost("{userId}/setMicrophoneVolume")]
        public async Task<IActionResult> SetMicrophoneVolume(Guid userId, [FromBody] MicrophoneVolumeRequest request)
        {
            var result = await _settingsManager.SetMicrophoneVolume(userId, request);
            return !result.IsSuccess ? StatusCode(result.StatusCode, $"An error occurred: {result.Error}") : Ok(result.Data);
        }

        [HttpGet("{userId}/microphoneVolume/{interlocutorId}")]
        public async Task<IActionResult> GetMicrophoneVolume(Guid userId, Guid interlocutorId)
        {
            var result = await _settingsManager.GetMicrophoneVolume(userId, interlocutorId);
            return !result.IsSuccess ? StatusCode(result.StatusCode, $"An error occurred: {result.Error}") : Ok(result.Data);
        }

        [HttpPost("{userId}/turnMicrophone/{interlocutorId}")]
        public async Task<IActionResult> SetMicrophoneWorkSettings(Guid userId, Guid interlocutorId, [FromBody] WorkSettings request)
        {
            var result = await _settingsManager.SetMicrophoneWorkSettings(userId, interlocutorId, request);
            return !result.IsSuccess ? StatusCode(result.StatusCode, $"An error occurred: {result.Error}") : Ok(result.Data);
        }

        [HttpGet("{userId}/microphoneStatus/{interlocutorId}")]
        public async Task<IActionResult> GetMicrophoneWorkSettings(Guid userId, Guid interlocutorId)
        {
            var result = await _settingsManager.GetMicrophoneWorkSettings(userId, interlocutorId);
            return !result.IsSuccess ? StatusCode(result.StatusCode, $"An error occurred: {result.Error}") : Ok(result.Data);
        }

        [HttpPost("{userId}/turnVideo/{interlocutorId}")]
        public async Task<IActionResult> SetCameraWorkSettings(Guid userId, Guid interlocutorId, [FromBody] WorkSettings request)
        {
            var result = await _settingsManager.SetCameraWorkSettings(userId, interlocutorId, request);
            return !result.IsSuccess ? StatusCode(result.StatusCode, $"An error occurred: {result.Error}") : Ok(result.Data);
        }

        [HttpGet("{userId}/videoStatus/{interlocutorId}")]
        public async Task<IActionResult> GetCameraWorkSettings(Guid userId, Guid interlocutorId)
        {
            var result = await _settingsManager.GetCameraWorkSettings(userId, interlocutorId);
            return !result.IsSuccess ? StatusCode(result.StatusCode, $"An error occurred: {result.Error}") : Ok(result.Data);
        }
    }
}
