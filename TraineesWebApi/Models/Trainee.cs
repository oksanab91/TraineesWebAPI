using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Trainees.Models
{
    public partial class Trainee
    {
        public Trainee()
        {
            TraineeTest = new HashSet<TraineeTest>();
        }

        public int TraineeId { get; set; }
        [Required]
        public string TraineeName { get; set; }

        public ICollection<TraineeTest> TraineeTest { get; set; }
    }
}
