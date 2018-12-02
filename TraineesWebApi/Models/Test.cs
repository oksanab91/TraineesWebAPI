using System;
using System.Collections.Generic;

namespace Trainees.Models
{
    public partial class Test
    {
        public Test()
        {
            TestSubject = new HashSet<TestSubject>();
            TraineeTest = new HashSet<TraineeTest>();
        }

        public int TestId { get; set; }
        public string Name { get; set; }
        public string Descrition { get; set; }

        public ICollection<TestSubject> TestSubject { get; set; }
        public ICollection<TraineeTest> TraineeTest { get; set; }
    }
}
