using BLL.LoggerModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public static class ExceptionHandler
    {
        public static string GetLog(Exception ex, string UserID, string Thread, ExcptionType type, string AppDataPath)
        {
            DateTime date = DateTime.Now;
            string ErrorCode = string.Format("{0}{1}{2}{3}{4}{5}", date.Second, date.Day, date.Millisecond, date.Minute, date.Month, date.Hour);
            Logger logger = new Logger()
            {
                Code = ErrorCode,
                CreateDate = DateTime.Now,
                Message = ex.Message,
                Exception = ex.ToString(),
                UserID = UserID,
                Thread = Thread,
                UpdateDate = DateTime.Now
            };
            try
            {
                Models.DataStore<Logger>.Add(logger);
                //new NetMailService().SendErrorToEmail(ErrorCode, UserID, Thread, ex.Message, ex.ToString());
            }
            catch (Exception exDB)
            {
                using (System.IO.FileStream _fs = new FileStream(AppDataPath + @"\ErrorLog.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite))
                {
                    using (StreamWriter sw = new StreamWriter(_fs))
                    {
                        sw.WriteLine("DateTime: " + DateTime.Now + ", code: " + ErrorCode + ",Error: " + ex.Message + ", User: " + UserID + ", Thread: " + Thread + ", Exception: " + ex.ToString());
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
