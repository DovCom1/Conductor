using Conductor.Dto.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conductor.Model.Dto.Settings
{
    public class EnemySettingsRequest
    {
        public Guid UserId { get; set; }
        public Guid EnemyId { get; set; }
        public NotificationSettings NotificationSettings { get; set; }
    }
}
