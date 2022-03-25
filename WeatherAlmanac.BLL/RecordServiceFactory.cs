using System;
using WeatherAlmanac.Core.DTO;
using WeatherAlmanac.Core.Interface;
using WeatherAlmanac.DAL;

namespace WeatherAlmanac.BLL
{
    public class RecordServiceFactory
    {
        public static IRecordService GetRecordService(ApplicationMode mode)
        {
            if (mode == ApplicationMode.TEST)
            {
                return new RecordService(new MockRecordRepository());
            } else
            {
                return new RecordService(new LiveRecordRepository());
            }
        }
    }
}
