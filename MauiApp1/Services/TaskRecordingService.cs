using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.Services
{
    public class TaskRecordingService
    {
        public void Record(DateTimeOffset startDatetime, string taskDetails)
        {
            var endDatetime = DateTimeOffset.Now;
            SaveTimestampToFile(startDatetime, endDatetime, taskDetails);
        }

        private string FormatText(DateTimeOffset startDatetime, DateTimeOffset endDatetime, string taskDetails)
        {
            var timeDifference = endDatetime.AddMinutes(1) - startDatetime;
            return $"({timeDifference.Minutes})[{startDatetime}-{endDatetime}] Task: {taskDetails}";
        }

        private async void SaveTimestampToFile(DateTimeOffset startDatetime, DateTimeOffset endDatetime, string taskDetails)
        {
            string formattedTimestamp = FormatText(startDatetime, endDatetime, taskDetails);

            try
            {
                var path = @"c:\EffectiveJ-Records";

                if (!Directory.Exists(path))
                {
                    DirectoryInfo di = Directory.CreateDirectory(path);
                }

                //TEMP: create one file for each record, please check the TODO below
                var fullPath = Path.Combine(path, $"records-{DateTimeOffset.Now.ToString("yyyy-MM-dd-HH-mm-ss")}.txt");

                //TODO: investigate why this has been append more than once
                File.AppendAllText(fullPath, formattedTimestamp + Environment.NewLine);
            }
            catch (Exception ex)
            {
                // Handle any exceptions that may occur during file operations
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
