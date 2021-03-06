﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using WebGrease.Css.Extensions;

namespace p2groep11.Net.Models.Domain
{
    public class ClimateChart
    {
        public int ClimateChartID { get; set; }
        public string Location { get; set; }
        public int BeginPeriod { get; set; }
        public int EndPeriod { get; set; }
        public virtual Country Country { get; set; }
        public virtual List<Month> Months { get; set; }
        public int[] SedimentArray { get; set; }
        public double[] TempArray { get; set; }
        public bool AboveEquator { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public String BCord { get; set; }
        public String LCord { get; set; }
       

        private void CalculateBCord()
        {
            String cord = "";
            if (Latitude < 0)
            {
                cord += ToDecimalDegrees(Latitude) + " ZB ";
            }
            else
            {
                cord += ToDecimalDegrees(Latitude) + " NB ";
            }
            BCord = cord;
        }

        private void CalculateLCord()
        {
            String cord = "";
            if (Longitude < 0)
            {
                cord += ToDecimalDegrees(Longitude) + " `WL ";
            }
            else
            {
                cord += ToDecimalDegrees(Longitude) + " OL ";
            }
            LCord = cord;
        }
        private String ToDecimalDegrees(double n)
        {
            String degrees="";
            double longitude = Math.Abs(n);
            degrees += Math.Truncate(n) + "° " + Math.Truncate(longitude*60%60) + "' " + Math.Truncate(longitude*3600%60)+'"';
            return degrees;
            
        }
        public ClimateChart()
        {
            Months = new List<Month>();
            SedimentArray = new int[12];
            TempArray = new double[12];
        }

        public ClimateChart(string loc, int begin, int end, double[] temperatures, int[] sediments, double latitude, double longitude)
        {
            Location = loc;
            BeginPeriod = begin;
            EndPeriod = end;
            Months = new List<Month>();
            SedimentArray = sediments;
            TempArray = temperatures;
            MakeMonthsList(temperatures, sediments);
            Latitude = latitude;
            Longitude = longitude;
            AboveEquator = latitude > 0;
            CalculateBCord();
            CalculateLCord();

        }

        public double HottestMonth //TW
        {
            get
            {
                return Months.Select(month => month.AverTemp).Max();
            }
        }
        public int AverageYearTemp //TJ
        {
            get
            {
                return (int)(Months.Average(month => month.AverTemp));
            }
        }

        public double ColdestMonth  //TK
        {
            get
            {
                return Months.Select(m => m.AverTemp).Min();
            }
        }

        public int ColdestMonthMK  //MK
        {
            get
            {
                string MK = "";
                foreach (Month m in Months)
                {
                    if (m.AverTemp == ColdestMonth)
                    {
                        MK = m.MonthProp.ToString();
                    }
                }
                return CalcMonthInt(MK);
            }
        }

        public int HottestMonthMW  //MW
        {
            get
            {
                string MW = "";
                foreach (Month m in Months)
                {
                    if (m.AverTemp == HottestMonth)
                    {
                        MW = m.MonthProp.ToString();
                    }
                }
                return CalcMonthInt(MW);
            }
        }

        public int CalcMonthInt(string month)
        {
            switch (month)
            { //Jan,Feb,Mrt,Apr,Mei,Jun,Jul,Aug,Sep,Okt,Nov,Dec
                case "Jan":
                    return 1;
                case "Feb":
                    return 2;
                case "Mrt":
                    return 3;
                case "Apr":
                    return 4;
                case "Mei":
                    return 5;
                case "Jun":
                    return 6;
                case "Jul":
                    return 7;
                case "Aug":
                    return 8;
                case "Sep":
                    return 9;
                case "Okt":
                    return 10;
                case "Nov":
                    return 11;
                case "Dec":
                    return 12;
                default:
                    return 0;
            }
        }

        public int TotalRainOfYear //NJ
        {
            get
            {
                return Months.Sum(m => m.Sediment);
            }
        }

        public int TotalDryMonths //D
        {
            get
            {
                return Months.Count(m => (m.AverTemp*2) > m.Sediment);
            }
        }

        public int RainInSummer //NZ
        {
            get
            {
                if (AboveEquator)
                {
                    var count = 0;
                    for (var i = 3; i < 9; i++)
                    {
                        count += Months[i].Sediment;
                    }
                    return count;
                }
                var count2 = 0;
                for (var i = 9; i < 12; i++)
                {
                    count2 += Months[i].Sediment;
                }
                for (var i = 0; i < 3; i++)
                {
                    count2 += Months[i].Sediment;
                }
                return count2;
            }
        }

        public int RainInWinter //NW
        {
            get
            {
                if (!AboveEquator)
                {
                    var count = 0;
                    for (var i = 3; i < 9; i++)
                    {
                        count += Months[i].Sediment;
                    }
                    return count;
                }
                var count2 = 0;
                for (var i = 9; i < 12; i++)
                {
                    count2 += Months[i].Sediment;
                }
                for (var i = 0; i < 3; i++)
                {
                    count2 += Months[i].Sediment;
                }
                return count2;
            }
        }

        public int MonthsAbove10Degree //TM
        {
            get
            {
                return Months.Count(m => m.AverTemp >= 10);
            }
        }

        private void MakeMonthsList(double[] temperatures,int[] sediments)
        {
            if (temperatures.Length != 12 || sediments.Length != 12)
                throw new ArgumentException("Temperatures and sediments have to contain 12 values");
            if(sediments.Min()<0)
                throw new ArgumentException("Sediments have to be greater than 0");
            int counter = 0;
            foreach (MonthsOfTheYear month in Enum.GetValues(typeof(MonthsOfTheYear)))
            {
                Months.Add(new Month(month,temperatures[counter],sediments[counter]));
                counter++;
            }
        }

        public int CalculateMaxForChart()
        {  
            return (int) Math.Max(Months.Max(m => m.AverTemp), Months.Max(m => m.Sediment));;
        }

        public int CalculateMinForChart()
        {
            return (int) Math.Min(Months.Min(m => m.AverTemp), Months.Min(m => m.Sediment));
        }

        public string[] GetMonthsOfYear()
        {
            List<string> theMonths = new List<string>();
            foreach (MonthsOfTheYear month in Enum.GetValues(typeof(MonthsOfTheYear)))
            {
                theMonths.Add(month.ToString());
            }
            return theMonths.ToArray();
        }

        public string[] GetAllTemperatures()
        {
            return Months.Select(m => m.AverTemp.ToString()).Distinct().ToArray();
        }

        public string[] GetAllSediments()
        {
            return SedimentArray.Select(t => t.ToString()).ToArray();
        }

        public string[] AmountOfMonths()
        {
            int[] maandenInts = new int[13];
            for (int i = 0; i < 13; i++)
            {
                maandenInts[i] = i;
            }
            return maandenInts.Select(t => t.ToString()).ToArray();
        }

        public string[] TotalRainfallInts()
        {
            string[] maandenStr = new string[2];
            maandenStr[0] = RainInSummer.ToString();
            maandenStr[1] = RainInWinter.ToString();
            return maandenStr;
        }

    }
}
