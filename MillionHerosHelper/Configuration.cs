using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MillionHerosHelper
{
    static class Config
    {
        public static string OCR_API_KEY { get; set; }
        public static string OCR_SECRET_KEY { get; set; }

        public static int CutX { get; set; }
        public static int CutY { get; set; }
        public static int CutWidth { get; set; }
        public static int CutHeight { get; set; }
        public static bool UseEmulator { get; set; }
        public static bool RemoveUselessInfo { get; set; }
        public static bool ShowABC { set; get; }
        public static bool UseSoGou { set; get; }
        public static HLMode HighlightMode { set; get; }


        public static void LoadConfig()
        {
            OCR_API_KEY = INIOperation.ReadValue("config.ini", "BaiDuOCR", "API_KEY", "0kEPZddCBO5cUD0Lf1yTN91O");
            OCR_SECRET_KEY = INIOperation.ReadValue("config.ini", "BaiDuOCR", "SECRET_KEY", "fEkXWR4CINttqCQVAQejX5cXgQKrVbnW");

            string _CutX = INIOperation.ReadValue("config.ini", "CutScreenShot", "StartX", "80");
            string _CutY = INIOperation.ReadValue("config.ini", "CutScreenShot", "StartY", "270");
            string _CutWidth = INIOperation.ReadValue("config.ini", "CutScreenShot", "Width", "900");
            string _CutHeight = INIOperation.ReadValue("config.ini", "CutScreenShot", "Heigh", "980");
            string _UseEmulator = INIOperation.ReadValue("config.ini", "CutScreenShot", "UseEmulator", Boolean.FalseString);
            string _RemoveUselessInfo = INIOperation.ReadValue("config.ini", "Other", "RemoveUselessInfo", Boolean.TrueString);
            string _ShowABC = INIOperation.ReadValue("config.ini", "Other", "ShowABC", Boolean.TrueString);
            string _UseSoGou = INIOperation.ReadValue("config.ini", "Other", "UseSoGou", Boolean.FalseString);

            string _HighlightMode = INIOperation.ReadValue("config.ini", "Other", "HighLightMode", "1");

            Int32.TryParse(_CutX, out int temp);
            CutX = temp;
            Int32.TryParse(_CutY, out temp);
            CutY = temp;
            Int32.TryParse(_CutWidth, out temp);
            CutWidth = temp;
            Int32.TryParse(_CutHeight, out temp);
            CutHeight = temp;

            Boolean.TryParse(_UseEmulator, out bool _ue);
            UseEmulator = _ue;
            Boolean.TryParse(_RemoveUselessInfo, out bool _rul);
            RemoveUselessInfo = _rul;
            Boolean.TryParse(_ShowABC, out bool _sabc);
            ShowABC = _sabc;
            Boolean.TryParse(_UseSoGou, out bool _usg);
            UseSoGou = _usg;
            Int32.TryParse(_HighlightMode, out int _hlm);
            HighlightMode = (HLMode)_hlm;

        }

        public static void SaveConfig()
        {
            INIOperation.WriteValue("config.ini", "BaiDuOCR", "API_KEY", OCR_API_KEY);
            INIOperation.WriteValue("config.ini", "BaiDuOCR", "SECRET_KEY", OCR_SECRET_KEY);

            INIOperation.WriteValue("config.ini", "CutScreenShot", "StartX", CutX.ToString());
            INIOperation.WriteValue("config.ini", "CutScreenShot", "StartY", CutY.ToString());
            INIOperation.WriteValue("config.ini", "CutScreenShot", "Width", CutWidth.ToString());
            INIOperation.WriteValue("config.ini", "CutScreenShot", "Heigh", CutHeight.ToString());
            INIOperation.WriteValue("config.ini", "CutScreenShot", "UseEmulator", UseEmulator.ToString());
            INIOperation.WriteValue("config.ini", "Other", "RemoveUselessInfo", RemoveUselessInfo.ToString());
            INIOperation.WriteValue("config.ini", "Other", "ShowABC", ShowABC.ToString());
            INIOperation.WriteValue("config.ini", "Other", "UseSoGou", UseSoGou.ToString());

            INIOperation.WriteValue("config.ini", "Other", "HighLightMode", ((int)HighlightMode).ToString());
        }
    }
}
