using System;
using System.Collections.Generic;

namespace Trainees.Models
{
    public partial class TraineeTest
    {
        public int TraineeTestId { get; set; }
        public int TestId { get; set; }
        public int TraineeId { get; set; }
        public string TestStatus { get; set; }

        public Test TestNavigation { get; set; }
        public Trainee TraineeNavigation { get; set; }
    }
}
