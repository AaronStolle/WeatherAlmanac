using System;
using System.Collections.Generic;
using WeatherAlmanac.Core.DTO;
using WeatherAlmanac.Core.Interface;

namespace WeatherAlmanac.BLL
{
    class RecordService : IRecordService
    {
        private IRecordRepository _repo;

        public RecordService(IRecordRepository repo)
        {
            _repo = repo;
        }

        public Result<DateRecord> Add(DateRecord record)
        {
            Result<DateRecord> result = new Result<DateRecord>();
            Result<List<DateRecord>> records = _repo.GetAll();


            if (records.Data.Contains(record))
            {
                result.Success = false;
                result.Message = "Duplicate record";
            }
            if (record.Date > DateTime.Now)
            {
                result.Success = false;
                result.Message = "Date can't be in the future";
            }
            if (record.Humidity < 0 || record.Humidity > 100)
            {
                result.Success = false;
                result.Message = "Humidity must be between 0-100";
            }
            if (record.LowTemp < -50 || record.HighTemp > 140)
            {
                result.Success = false;
                result.Message = "Temperature must be between -50-140";
            }
            else
            {
                result = _repo.Add(record);
            }return result;
        }

        public Result<DateRecord> Edit(DateRecord record)
        {
            Result<DateRecord> result;
            result = _repo.Edit(record);
            return result;
        }

        public Result<DateRecord> Get(DateTime date)
        {
            Result<List<DateRecord>> records = _repo.GetAll();
            Result<DateRecord> result = new Result<DateRecord>();

            foreach (DateRecord record in records.Data)
            {
                if (record.Date == date)
                {

                    result.Data = record;
                    result.Success = true;
                    result.Message = "";
                    return result;
                }
            }
            result.Success = false;
            result.Message = "Record doesn't exist";
            result.Data = null;
            return result;

        }

        public Result<List<DateRecord>> LoadRange(DateTime start, DateTime end)
        {
            Result<List<DateRecord>> records = _repo.GetAll();
            Result<List<DateRecord>> recordList = new();
            recordList.Data = new List<DateRecord>();
            recordList.Success = false;
            recordList.Message = "There was no record for this date!";
            foreach (DateRecord record in records.Data)
            {
                if (record.Date <= end && record.Date >= start)
                {

                    recordList.Data.Add(record);
                    recordList.Success = true;
                    recordList.Message = "";                    
                }
            }
            
            return recordList;
        }

        public Result<DateRecord> Remove(DateTime date)
        {
            Result<DateRecord> result;
            result = _repo.Remove(date);
            return result;
        }
        public void WriteAll()
        {
            _repo.WriteAll();
        }
    }
}
