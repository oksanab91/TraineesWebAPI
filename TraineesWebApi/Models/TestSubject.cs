using System;
using System.Collections.Generic;

namespace Trainees.Models
{
    public partial class TestSubject
    {
        public int TestSubjectId { get; set; }
        public int TestId { get; set; }
        public string SubjectCode { get; set; }

        public Subject SubjectCodeNavigation { get; set; }
        public Test TestNavigation { get; set; }
    }
}
