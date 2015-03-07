﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace p2groep11.Net.Models.Domain
{
    public class Clause : ClauseComponent
    {
        
        public ClauseComponent YesClause { get; set; }
        public ClauseComponent NoClause { get; set; }
        public String Name { get; private set; }
    
        public Parameter Par1;
        public Parameter Par2;
        public int Waarde;

        public Clause(String name, Parameter par1, int waarde)
        {
            this.Name = name;
            this.Par1 = par1;
            this.Waarde = waarde;
        }

        public Clause(String name, Parameter par1, Parameter par2)
        {
            this.Name = name;
            this.Par1 = par1;
            this.Par2 = par2;
        }

        public override string GetName()
        {
            return this.Name;
        }

        public override ClauseComponent GetYesClause()
        {
            return this.YesClause;
        }

        public override ClauseComponent GetNoClause()
        {
            return this.NoClause;
        }

        public override void Add(Boolean soort, ClauseComponent component)
        {
            if (soort)
            {
                YesClause = component;
            }
            else
            {
                NoClause = component;
            }
        }

        

        public override string Determinate(ClimateChart chart)
        {
            /*if (Par2.Execute(chart).Equals(null))    Hier is iets fout*/
                return Par1.Execute(chart) <= Waarde ? YesClause.Determinate(chart) : NoClause.Determinate(chart);
            /*else
            {
                return Par1.Execute(chart) <= Par2.Execute(chart) ? YesClause.Determinate(chart) : NoClause.Determinate(chart);
            }*/
            
        }
    }
}
