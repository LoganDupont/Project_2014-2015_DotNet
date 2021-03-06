﻿using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using p2groep11.Net.Models.Domain;

namespace p2groep11.Net
{
    public class MK : p2groep11.Net.Models.Domain.Parameter
    {

        public MK()
        {
            
        }

        public MK(String beschr)
        {
            this.Beschrijving = beschr;
        }
        public override double Execute(ClimateChart chart)
        {
            return chart.ColdestMonthMK;
        }

        public override string[] GiveOptAnswers(ClimateChart chart)
        {
            return chart.GetMonthsOfYear();
        }

        public override string GiveAnswer(ClimateChart chart)
        {
            return chart.ColdestMonthMK.ToString();
        }
    }
}
