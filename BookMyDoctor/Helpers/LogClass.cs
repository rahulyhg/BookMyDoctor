﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace BookMyDoctor.Helpers
{
    public class LogClass
    {
        //FileStream fl;
        private readonly string page;
        public LogClass(string pageName)
        {
            string APP_PATH = System.Web.HttpContext.Current.Server.MapPath("~");
            page = pageName;
            //fl = new FileStream(APP_PATH+"\\log\\log_" + pageName + ".txt", FileMode.OpenOrCreate, FileAccess.ReadWrite);
            DateTime curDate = DateTime.Now;
            File.AppendAllText(APP_PATH + "\\ErrorLogs\\log_" + page + ".txt", Environment.NewLine + "=======================================================================================" + Environment.NewLine);
            string date = curDate.ToString("yyyy-MM-dd HH:mm:ss:ffff");
            string writeString = date + Environment.NewLine + "-------------------------------" + Environment.NewLine;
            File.AppendAllText(APP_PATH + "\\ErrorLogs\\log_" + page + ".txt", writeString);
        }

        ~LogClass()
        {
            //fl.Close();
        }

        public void Write(Exception ex)
        {
            //Byte[] btAr = Encoding.Default.GetBytes(logMsg+"\n");
            //fl.Write(btAr, 0, btAr.Length);
            string APP_PATH = System.Web.HttpContext.Current.Server.MapPath("~");
            string file = APP_PATH + "\\ErrorLogs\\log_" + page + ".txt";
            File.AppendAllText(file, "StackTrace:");
            File.AppendAllText(file, ex.StackTrace + Environment.NewLine);
            File.AppendAllText(file, Environment.NewLine + "-------------------------------" + Environment.NewLine);
            File.AppendAllText(file, "Message:");
            File.AppendAllText(file, ex.Message + Environment.NewLine);
            File.AppendAllText(file, Environment.NewLine + "-------------------------------" + Environment.NewLine);
            File.AppendAllText(file, "InnerException:");
            File.AppendAllText(file, ex.InnerException + Environment.NewLine);
            File.AppendAllText(file, Environment.NewLine + "-------------------------------" + Environment.NewLine);
            File.AppendAllText(file, "Source:");
            File.AppendAllText(file, ex.Source + Environment.NewLine);
            File.AppendAllText(file, Environment.NewLine + "-------------------------------" + Environment.NewLine);
            File.AppendAllText(file, Environment.NewLine + "=======================================================================================" + Environment.NewLine);
        }

        public void Write_all_data(string sync_data)
        {
            //Byte[] btAr = Encoding.Default.GetBytes(logMsg+"\n");
            //fl.Write(btAr, 0, btAr.Length);
            string APP_PATH = System.Web.HttpContext.Current.Server.MapPath("~");
            string file = APP_PATH + "\\ErrorLogs\\log_" + page + ".txt";
            File.AppendAllText(file, "DATA:");
            File.AppendAllText(file, sync_data + Environment.NewLine);

            File.AppendAllText(file, Environment.NewLine + "=======================================================================================" + Environment.NewLine);
        }
    }
}