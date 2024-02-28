using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicSchedule_PassionProject.Models
{
    public class MusicStudent
    {
        [Key]
        public int MStudentID { get; set; }

        public string MStudentFName { get; set; }

        public string MStudentLName { get; set; }

        //an instrument can be played by many students
        //a music can play one instrument
        [ForeignKey("MusicInstrument")]
        public int InstrumentID { get; set; }
        public virtual MusicInstrument MusicInstrument { get; set; }

        //[ForeignKey("Lesson")]
        //public int LessonId { get; set; }
        //public virtual Lesson Lesson { get; set; }
    }

    public class MusicStudentDto
    {
        public int MStudentID { get; set; }

        public string MStudentFName { get; set; }

        public string MStudentLName { get; set; }

        public int InstrumentID { get; set; }
    }
}