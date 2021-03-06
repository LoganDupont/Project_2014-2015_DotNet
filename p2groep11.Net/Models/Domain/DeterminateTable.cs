﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Owin.Security.Notifications;

namespace p2groep11.Net.Models.Domain
{
    public class DeterminateTable
    {
        public int DeterminateTableId { get; set; }
        public virtual ICollection<ClauseComponent> AllClauseComponents { get; set; }
        public String Name { get; set; }

        public DeterminateTable()
        {
            AllClauseComponents = new List<ClauseComponent>();
        }

        public DeterminateTable(String name)
        {
            AllClauseComponents = new List<ClauseComponent>();
            Name = name;
        }

        public String[] Determinate(ClimateChart chart)
        {
            return AllClauseComponents.ElementAt(AllClauseComponents.Count - 1).Determinate(chart);
        }

        public List<ClauseComponent> CorrectPath(ClimateChart chart)
        {
            return AllClauseComponents.ElementAt(AllClauseComponents.Count - 1).GiveCorrectPath(chart);
        }
        

    }
}