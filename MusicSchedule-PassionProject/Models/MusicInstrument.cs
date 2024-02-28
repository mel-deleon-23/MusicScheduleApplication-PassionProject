using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicSchedule_PassionProject.Models
{
    public class MusicInstrument
    {
        [Key]
        public int InstrumentID { get; set; }

        public string InstrumentType { get; set; }

    }

    public class MusicInstrumentDto
    {
        public int InstrumentID { get; set; }

        public string InstrumentType { get; set; }
    }
}