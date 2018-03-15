using BLL.LoggerModels;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;

namespace BLL
{
    public static class ExceptionHandler
    {
        public static string GetLog(Exception ex, string UserID, ExcptionType type)
        {
            DateTime date = DateTime.Now;
            string caller = string.Empty;
            int framescount = 0;
            foreach (var item in new StackTrace().GetFrames())
            {
                if(framescount!=0)
                caller += $"Method{framescount}: {item.GetMethod().Name }, Class{framescount}: { item.GetMethod().DeclaringType.Name }; ";
                framescount++;
                if (framescount > 2) break;
            }
            string ErrorCode = string.Format("{0}{1}{2}{3}{4}{5}", date.Second, date.Day, date.Millisecond, date.Minute, date.Month, date.Hour);
            Logger logger = new Logger()
            {
                Code = ErrorCode,
                CreateDate = DateTime.Now,
                Message = ex.Message,
                Exception = ex.ToString(),
                UserID = UserID,
                Thread = caller,
                UpdateDate = DateTime.Now
            };
            try
            {
                Models.DataStore<Logger>.Add(logger);
                //new NetMailService().SendErrorToEmail(ErrorCode, UserID, Thread, ex.Message, ex.ToString());
            }
            catch (Exception exDB)
            {
                using (System.IO.FileStream _fs = new FileStream(Path.GetFullPath("ErrorLog.txt"), FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite))
                {
                    using (StreamWriter sw = new StreamWriter(_fs))
                    {
                        sw.WriteLine("DateTime: " + DateTime.Now + ", code: " + ErrorCode + ",Error: " + ex.Message + ", User: " + UserID + ", Thread: " + caller + ", Exception: " + ex.ToString());
                        sw.WriteLine("DBerror: " + exDB);
                    }
                }
            }
            return GetCustomDescription(type) + logger.Code;
        }
        static string GetCustomDescription(object objEnum)
        {
            var fi = objEnum.GetType().GetField(objEnum.ToString());
            var attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
            return (attributes.Length > 0) ? attributes[0].Description : objEnum.ToString();
        }

    }
}
