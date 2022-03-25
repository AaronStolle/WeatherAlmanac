using System;
using System.Collections.Generic;
using System.IO;
using WeatherAlmanac.Core.DTO;
using WeatherAlmanac.Core.Interface;

namespace WeatherAlmanac.DAL
{
    public class LiveRecordRepository : IRecordRepository
    {
        private List<DateRecord> _records;

        public LiveRecordRepository()
        {
            string path = Directory.GetCurrentDirectory() + @"\Weather.csv";
            if (File.Exists(path))
            {
                List<DateRecord> records = new List<DateRecord>();
                
                using (StreamReader sr = new StreamReader(path))
                {
                    string CurrentLine = sr.ReadLine();
                    CurrentLine = sr.ReadLine();

                    while(CurrentLine != null)
                    {
                        DateRecord record = new DateRecord();
                        string[] columns = CurrentLine.Split(',');

                        record.Date = DateTime.Parse(columns[0]);
                        record.HighTemp = int.Parse(columns[1]);
                        record.LowTemp = int.Parse(columns[2]);
                        record.Humidity = decimal.Parse(columns[3]);
                        record.Description = columns[4];

                        records.Add(record);

                        CurrentLine = sr.ReadLine();
                    }
                    _records = records;
                }
            }
            else
            {
                Console.WriteLine($"File at {path} not found.");
            }
        }

        public Result<DateRecord> Add(DateRecord record)
        {
            Result<DateRecord> result = new Result<DateRecord>();
            _records.Add(record);
            result.Message = $"Added {record}";
            result.Success = true;
            result.Data = record;
            return result;
        }

        public Result<DateRecord> Edit(DateRecord record)
        {
            Result<DateRecord> result = new Result<DateRecord>();
            foreach (DateRecord item in _records)
            {
                if (item.Date == record.Date)
                {
                    item.HighTemp = record.HighTemp;
                    item.LowTemp = record.LowTemp;
                    item.Humidity = record.Humidity;
                    item.Description = record.Description;
                }
            }
            return result;
        }

        public Result<List<DateRecord>> GetAll()
        {
            Result<List<DateRecord>> result = new Result<List<DateRecord>>();
            result.Success = true;
            result.Message = "";
            result.Data = new List<DateRecord>(_records);
            return result;
        }

        public Result<DateRecord> Remove(DateTime date)
        {
            Result<DateRecord> result = new Result<DateRecord>();
            for (int i = 0; i < _records.Count; i++)
            {
                if (_records[i].Date == date)
                {
                    result.Data = _records[i];
                    result.Success = true;
                    result.Message = $"Removed date {date}";
                    _records.RemoveAt(i);
                }
            }
            return result;
        }

        public void WriteAll()
        {
            string path = Directory.GetCurrentDirectory() + @"\Weather.csv";
            
            using (StreamWriter sw = new StreamWriter(path))
            {
                foreach (DateRecord record in _records)
                {
                    sw.WriteLine();
                    sw.Write($"{record.Date},{record.HighTemp},{record.LowTemp},{record.Humidity},{record.Description}");
                }
            }
            
        }

    }
}

