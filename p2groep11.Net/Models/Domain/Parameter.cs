﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace p2groep11.Net.Models.Domain
{
    public abstract class Parameter
    {
        public int ParameterId { get; set; }
        public string Beschrijving { get; set; }
        public string Answer { get; set; } 

//De vraag zelf

        public abstract int Execute(ClimateChart chart);
        public abstract string[] GiveOptAnswers(ClimateChart chart);
    }
}