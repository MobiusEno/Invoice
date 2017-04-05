// File: Plusmore.Einvoice.Common.Sample / MyConfig.cs
// 
// Author: Pay
// Created: 2017-01-19 12:11
// 
// Modified: 2017-01-19 14:27

using System;

namespace Plusmore.Einvoice.Common.Sample.Helper
{
    public class MyConfig
    {
        public static string PrinterPort = "COM4";
        public static string CompanyName = "天來數位"; // 發票 title 用(預設 本程式沒用到)
        public static string AesKey = "6F42C5148D45357E77124DC9CD27225A";  
        public static string Folder  = String.Format( @"{0}\delme", Environment.GetFolderPath( Environment.SpecialFolder.Desktop ) );
    }
}