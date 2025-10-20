namespace Conductor.Dto.Settings;

public record EnemySettingsDto(NotificationSettings NotificationSettings);

public enum NotificationSettings
{
    AllNotifications,
    OnlyMessages,
    Nothing
}