﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MusicSchedule_PassionProject.Models.ViewModels
{
    public class UpdateLesson
    {
        //This viewmodel is a class which stores information that we need to present to /MusicStudent/Update/{}

        //the existing music student info

        public LessonDto SelectedLesson { get; set; }

    }
}