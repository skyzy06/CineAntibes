using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;

namespace CineAntibes.Utils
{
    public class DateTools
    {
        /// <summary>
        /// Format a date that contains only the day of the month and the month
        /// </summary>
        /// <returns>DateTime for the input</returns>
        /// <param name="stringDate">The string with a date like dd MMMM or d MMMM</param>
        public static DateTime FormatReleaseDate(string stringDate)
        {
            try
            {
                DateTime formatedDate = DateTime.MaxValue;
                Boolean parseSuccess = DateTime.TryParseExact(stringDate, "d MMMM", App.CinemaCulture, DateTimeStyles.AssumeLocal, out formatedDate);
                if (!parseSuccess)
                {
                    formatedDate = DateTime.ParseExact(stringDate, "dd MMMM", App.CinemaCulture);
                }
                return formatedDate;
            }
            catch (Exception)
            {
                return DateTime.MinValue;
            }
        }

        /// <summary>
        /// Create a list of DateTime with all the sessions in one language (VF or VO)
        /// </summary>
        /// <returns>A list of DateTime</returns>
        /// <param name="jsonSession">A JToken that contains all the incomiong session in a language</param>
        public static List<DateTime> FormatSessions(JToken jsonSession)
        {
            try
            {
                List<DateTime> output = new List<DateTime>();
                string thisWeekSessions = jsonSession.Value<string>("Cette semaine");
                string nextWeekSessions = jsonSession.Value<string>("Dès la semaine prochaine");

                string[] splitedDaySessions = Regex.Split(thisWeekSessions.Trim(), "\n\n");

                for (int i = 0; i < splitedDaySessions.Length; i++)
                {
                    DateTime formatedDate = DateTime.MinValue;
                    string[] splitedDateAndHour = splitedDaySessions[i].Trim().Split('\n');
                    string[] splitedHourSessions = splitedDateAndHour[1].Split(' ');

                    for (int j = 0; j < splitedHourSessions.Length; j++)
                    {
                        string stringFullDate = splitedDateAndHour[0] + ' ' + splitedHourSessions[j];
                        Boolean parseSuccess = DateTime.TryParseExact(stringFullDate, "dddd d MMMM HH\"h\"mm", App.CinemaCulture, DateTimeStyles.AssumeLocal, out formatedDate);
                        if (!parseSuccess)
                        {
                            formatedDate = DateTime.ParseExact(stringFullDate, "dddd dd MMMM HH\"h\"mm", App.CinemaCulture);
                        }
                        output.Add(formatedDate);
                    }
                }

                return output;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
