using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.LoggerModels
{
    [Flags]
    public enum ExcptionType
    {
        [Description("No data found for your request.")]
        NoDataFound,
        [Description("Unknown error occurred please retry again or contact support using the error code: ")]
        UnknownError,
        [Description("The page or file you are requesting is not avalible or moved, If you think this an error please contact support with this error code: ")]
        HTTPError404,
        [Description("حدث خطأ غير معروف يرجى إعادة المحاولة مرة أخرى أو الاتصال بالدعم باستخدام رمز الخطأ: ")]
        UnknownErrorAr

    }
}
