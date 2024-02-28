using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicSchedule_PassionProject.Models
{
    public class Lesson
    {
        [Key]
        public int LessonId { get; set; }

        public string LessonName { get; set; }

        public string LessonDescription { get; set; }

        public DateTime LessonTime { get; set; }

        public string LessonInstructor { get; set; }

        [ForeignKey("MusicStudent")]
        public int MStudentID { get; set; }
        public virtual MusicStudent MusicStudent { get; set; }

        //a lesson can be taken by many students
        //public ICollection<MusicStudent> Students { get; set; }

    }

    public class LessonDto
    {
        public int LessonId { get; set; }

        public string LessonName { get; set; }

        public string LessonDescription { get; set; }

        public DateTime LessonTime { get; set; }

        public string LessonInstructor { get; set; }
        public int MStudentID { get; set; }
    }
}