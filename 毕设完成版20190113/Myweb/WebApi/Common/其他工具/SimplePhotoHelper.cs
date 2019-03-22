using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    /// <summary>
    /// 人员照片路径帮助类
    /// </summary>
    public class SimplePhotoHelper
    {
        /// <summary>
        /// 获取医生默认头像
        /// </summary>
        /// <param name="destPhotoPath">头像路径</param>
        /// <returns></returns>
        public static string GetDoctorPhotoPath(string destPhotoPath)
        {
            if (string.IsNullOrWhiteSpace(destPhotoPath))
            {
                return GlobalConstant.USER_PHOTO_DIR + "doctor.png";
            }
            return destPhotoPath;
        }
    }
}
