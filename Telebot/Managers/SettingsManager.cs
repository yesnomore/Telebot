﻿using IniParser;
using IniParser.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using Telebot.Contracts;

namespace Telebot.Managers
{
    public class SettingsManager : FileIniDataParser, ISettings
    {
        private const string FILE_NAME = "settings.ini";
        private readonly string filePath;

        private readonly IniData data;

        public SettingsManager()
        {
            filePath = AppDomain.CurrentDomain.BaseDirectory + FILE_NAME;

            if (!File.Exists(filePath)) {
                File.Create(filePath);
            }

            try {
                data = ReadFile(filePath);
            }
            catch {

            }
        }

        ~SettingsManager()
        {
            WriteFile(filePath, data);
        }

        public bool GetMonitorEnabled()
        {
            string s = data["Temperature.Monitor"]["Enabled"];

            if (!string.IsNullOrEmpty(s))
            {
                return bool.Parse(s);
            }

            return false;
        }

        public long GetChatId()
        {
            string s = data["Telegram"]["Chat.Id"];

            if (!string.IsNullOrEmpty(s))
            {
                return long.Parse(s);
            }

            return 0;
        }

        public float GetCPUTemperature()
        {
            string s = data["Temperature.Monitor"]["CPU_TEMPERATURE_WARNING"];

            if (!string.IsNullOrEmpty(s))
            {
                return float.Parse(s);
            }

            return 65.0f;
        }

        public Rectangle GetForm1Bounds()
        {
            string s = data["GUI"]["Form1.Bounds"];

            if (!string.IsNullOrEmpty(s))
            {
                return JsonConvert.DeserializeObject<Rectangle>(s);
            }

            return new Rectangle(0, 0, 150, 150);
        }

        public float GetGPUTemperature()
        {
            string s = data["Temperature.Monitor"]["GPU_TEMPERATURE_WARNING"];

            if (!string.IsNullOrEmpty(s))
            {
                return float.Parse(s);
            }

            return 65.0f;
        }

        public List<int> GetListView1ColumnsWidth()
        {
            string s = data["GUI"]["listview1.Columns.Width"];

            if (!string.IsNullOrEmpty(s))
            {
                return JsonConvert.DeserializeObject<List<int>>(s);
            }

            return new List<int>();
        }

        public string GetTelegramToken()
        {
            return data["Telegram"]["Token"];
        }

        public List<int> GetTelegramWhiteList()
        {
            string s = data["Telegram"]["WhiteList"];

            if (!string.IsNullOrEmpty(s))
            {
                return JsonConvert.DeserializeObject<List<int>>(s);
            }

            return new List<int>();
        }

        public void SetMonitorEnabled(bool enabled)
        {
            data["Temperature.Monitor"]["Enabled"] = enabled.ToString();
        }

        public void SetChatId(long id)
        {
            data["Telegram"]["Chat.Id"] = id.ToString();
        }

        public void SetCPUTemperature(float value)
        {
            data["Temperature.Monitor"]["CPU_TEMPERATURE_WARNING"] = value.ToString();
        }

        public void SetForm1Bounds(Rectangle bounds)
        {
            string s = JsonConvert.SerializeObject(bounds);
            data["GUI"]["Form1.Bounds"] = s;
        }

        public void SetGPUTemperature(float value)
        {
            data["Temperature.Monitor"]["GPU_TEMPERATURE_WARNING"] = value.ToString();
        }

        public void SetListView1ColumnsWidth(List<int> widths)
        {
            string s = JsonConvert.SerializeObject(widths);
            data["GUI"]["listview1.Columns.Width"] = s;
        }

        public void SetTelegramWhiteList(List<int> list)
        {
            throw new NotImplementedException();
        }
    }
}
