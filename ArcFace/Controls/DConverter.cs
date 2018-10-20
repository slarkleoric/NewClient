using ArcFace.Core;
using ArcFaceClient.Converter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcFaceClient.Controls
{
    /// <summary>
    /// 常用转换器的静态引用
    /// 使用实例：Converter={x:Static local:DConverter.BooleanToVisibilityConverter}
    /// </summary>
    public sealed class DConverter
    {
        public static BooleanToVisibilityConverter BooleanToVisibilityConverter =>
            Singleton<BooleanToVisibilityConverter>.Instance ?? (Singleton<BooleanToVisibilityConverter>.Instance =
                new BooleanToVisibilityConverter());

        //public static ThicknessToDoubleConverter ThicknessToDoubleConverter =>
        //    Singleton<ThicknessToDoubleConverter>.Instance ?? (Singleton<ThicknessToDoubleConverter>.Instance =
        //        new ThicknessToDoubleConverter());

        //public static PercentToAngleConverter PercentToAngleConverter =>
        //    Singleton<PercentToAngleConverter>.Instance ?? (Singleton<PercentToAngleConverter>.Instance =
        //        new PercentToAngleConverter());

        //public static ImageSourceConverter ImageSourceConverter =>
        //    Singleton<ImageSourceConverter>.Instance ?? (Singleton<ImageSourceConverter>.Instance =
        //        new ImageSourceConverter());
    }
}
