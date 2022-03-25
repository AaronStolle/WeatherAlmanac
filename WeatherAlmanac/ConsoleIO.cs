using System;
using WeatherAlmanac.Core.DTO;

namespace WeatherAlmanac.UI
{
    class ConsoleIO
    {
        public int GetInt(string prompt)
        {
            int result = -1;
            bool valid = false;
            while (!valid)
            {
                Console.Write($"{prompt}: ");
                if (!int.TryParse(Console.ReadLine(), out result))
                {
                    Error("Please input a proper integer\n\n");
                }
                else
                {
                    valid = true;
                }
            }
            return result;
        }
        public void Display(string message)
        {
            Console.WriteLine(message);
        }
        public void Error(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Display(message);
            Console.ForegroundColor = ConsoleColor.White;
        }
        public void Warn(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Display(message);
            Console.ForegroundColor = ConsoleColor.White;
        }
        public DateTime GetDate(string prompt)
        {
            while (true)
            {
                Console.Write($"{prompt}: ");
                if (!DateTime.TryParse(Console.ReadLine(), out DateTime result))
                {
                    Error("Please input a proper date\n\n");
                }
                else
                {
                    return result;
                }
            }
        }
        public DateRecord GetRecord(string prompt)
        {
            bool valid = false;
            DateRecord record = new DateRecord();
            while (!valid)
            {
                record.Description = GetString("Enter Description: ");
                record.Date = GetDate("Enter Date: ");
                record.HighTemp = GetInt("Enter HighTemp: ");
                record.LowTemp = GetInt("Enter LowTemp: ");
                record.Humidity = GetDecimal("Enter Humidity: ");
                valid = true;
            }
            return record;
        }

        public string GetString(string prompt)
        {
            string result = "";
            bool valid = false;
            while (!valid)
            {
                Console.Write($"{prompt}: ");
                result = Console.ReadLine();
                if (string.IsNullOrEmpty(result))
                {
                    Error("Please input a proper string\n\n");
                }
                else
                {
                    valid = true;
                }
            }
            return result;
        }
        public decimal GetDecimal(string prompt)
        {
            decimal result = 0.1m;
            bool valid = false;
            while (!valid)
            {
                Console.Write($"{prompt}: ");
                if (!decimal.TryParse(Console.ReadLine(), out result))
                {
                    Error("Please input a proper decimal\n\n");
                }
                else
                {
                    valid = true;
                }
            }

            return result;
        }
        public DateRecord EditRecord(DateRecord record)
        {
            bool valid = false;
            DateRecord ret = new DateRecord();
            string holder;
            while (!valid)
            {
                Console.Write($"HighTemp {record.HighTemp}: ");
                holder = Console.ReadLine();

                if (string.IsNullOrEmpty(holder) || (!decimal.TryParse(holder, out decimal temp)))
                {
                    ret.HighTemp = record.HighTemp;
                }
                else
                {
                    ret.HighTemp = temp;
                }

                Console.Write($"Low {record.LowTemp}: ");
                holder = Console.ReadLine();

                if (string.IsNullOrEmpty(holder) || !decimal.TryParse(holder, out decimal lowTemp))
                {
                    ret.LowTemp = record.LowTemp;
                }
                else
                {
                    ret.LowTemp = lowTemp;
                }


                Console.Write($"Humidity {record.Humidity}: ");
                holder = Console.ReadLine();



                if (string.IsNullOrEmpty(holder) || (!decimal.TryParse(holder, out decimal humidity)))
                {
                    ret.Humidity = record.Humidity;
                }
                else
                {
                    ret.Humidity = humidity;
                }

                Display($"Old Description: {record.Description}");
                Display($"New Description: ");
                string description = Console.ReadLine();
                if (string.IsNullOrEmpty(description))
                {
                    ret.Description = record.Description;
                }
                else
                {
                    ret.Description = description;
                }

                ret.Date = record.Date;
                valid = true;
            }
            return ret;
        }
    }

}