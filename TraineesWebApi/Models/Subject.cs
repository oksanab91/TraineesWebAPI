using System;
using System.Collections.Generic;

namespace Trainees.Models
{
    public partial class Subject
    {
        public Subject()
        {
            TestSubject = new HashSet<TestSubject>();
        }

        public string SubjectCode { get; set; }
        public string Name { get; set; }

        public ICollection<TestSubject> TestSubject { get; set; }
    }
}
