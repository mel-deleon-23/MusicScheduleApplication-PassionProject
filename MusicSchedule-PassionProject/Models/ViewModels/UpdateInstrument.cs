using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MusicSchedule_PassionProject.Models.ViewModels
{
    public class UpdateInstrument
    {
        //This viewmodel is a class which stores information that we need to present to /MusicInstrument/Update/{}

        //the existing instrument info

        public MusicInstrumentDto SelectedInstrument { get; set; }

    }
}