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
        public static string CompanyName = "天來數位"; // 發票 title 用
        public static string AesKey = "D6E2543010DE06ED9B42734016A02890";  
        public static string Folder  = String.Format( @"{0}\delme", Environment.GetFolderPath( Environment.SpecialFolder.Desktop ) );
    }
}