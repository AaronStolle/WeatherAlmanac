using System;
using System.Collections.Generic;
using WeatherAlmanac.Core.DTO;
using WeatherAlmanac.Core.Interface;

namespace WeatherAlmanac.UI
{
    class MenuController
    {
        private ConsoleIO _ui;
        public IRecordService Service { get; set; }

        public MenuController(ConsoleIO ui) 
        {
            _ui = ui;
        }

        public ApplicationMode Setup()
        {
            _ui.Display("Enter Application Mode:");
            _ui.Display("1. Test");
            _ui.Display("2. Live");

            int mode = _ui.GetInt("");
            if (mode == 1)
            {
                return ApplicationMode.TEST;
            } else if (mode == 2)
            {
                return ApplicationMode.LIVE;
            } else
            {
                _ui.Error("Invalid mode.  Exiting.");
                Environment.Exit(0);
                return ApplicationMode.TEST; //?? The program has exited here.
            }
        }

        public void Run()
        {
            bool running = true;

            while(running)
            {
                switch(GetMenuChoice())
                {
                    case 1:
                        LoadRecord();
                        break;
                    case 2:
                        ViewRecordsByDateRange();
                        break;
                    case 3:
                        AddRecord();
                        break;
                    case 4:
                        EditRecord();
                        break;
                    case 5:
                        DeleteRecord();
                        break;
                    case 6:
                        //Quit
                        SaveAll();
                        running = false;                        
                        break;
                    default:
                        //Huh??
                        _ui.Error("Invalid menu option");
                        break;

                }

            }

        }

        public int GetMenuChoice()
        {
            DisplayMenu();
            return _ui.GetInt("Enter Choice");
        }
        public void DisplayMenu()
        {
            _ui.Display("Main Menu");
            _ui.Display("==================================");
            _ui.Display("1. Load a record");
            _ui.Display("2. View Records by Date Range");
            _ui.Display("3. Add Record");
            _ui.Display("4. Edit Record");
            _ui.Display("5. Delete Record");
            _ui.Display("6. Quit");
        }

        public void LoadRecord()
        {
            _ui.Display("Load Record");
            Result<DateRecord> ret;
            ret = Service.Get(_ui.GetDate("Enter the date: "));
            if (ret.Success)
                _ui.Display(ret.Data.ToString());
            else
                _ui.Display(ret.Message);
        }

        public void ViewRecordsByDateRange()
        {
            _ui.Display("View Records By Date Range");
            Result<List<DateRecord>> ret;
            DateTime start = _ui.GetDate("Enter the start date: ");
            DateTime end = _ui.GetDate("Enter the end date: ");
            ret = Service.LoadRange(start, end);
            if (ret.Success)
            {
                foreach (DateRecord record in ret.Data)
                {
                    _ui.Display(record.ToString());
                }                
            }                
            else
                _ui.Display(ret.Message);
        }

        public void AddRecord()
        {
            _ui.Display("Add Record");
            Result<DateRecord> ret;
            ret = Service.Add(_ui.GetRecord("Enter record: "));
            if (ret.Success)
                _ui.Display(ret.Data.ToString());
            else
                _ui.Display(ret.Message);

        }

        public void EditRecord() 
        {
            Result<DateRecord> returnCurrent, ret;
            returnCurrent = Service.Get(_ui.GetDate("Enter the date: "));
            ret = Service.Edit(_ui.EditRecord(returnCurrent.Data));

            if (ret.Success)
                _ui.Display(ret.Data.ToString());
            else
                _ui.Display(ret.Message);

        }

        public void DeleteRecord()
        {
            _ui.Display("Delete Record");
            Result<DateRecord> ret;
            ret = Service.Remove(_ui.GetDate("Enter record: "));
            if (ret.Success)
                _ui.Display(ret.Data.ToString());
            else
                _ui.Display(ret.Message);

        }
        public void SaveAll()
        {
            Service.WriteAll();
        }

    }
}
