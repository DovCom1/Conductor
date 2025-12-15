using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conductor.Model.Dto.Settings
{
    public class MicrophoneVolumeRequest
    {
        public Guid InterlocutorId { get; set; }

        public int MicrophoneVolume { get; set; }
    }
}
