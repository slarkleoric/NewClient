﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace ArcFace.Core.Helper
{
    public static class Extends
    {
        private static readonly Type[] SimpleTypes =
        {
            typeof(int), typeof(byte), typeof(long), typeof(string), typeof(bool), typeof(double), typeof(float),
            typeof(decimal), typeof(Enum)
        };

        public static bool IsSimpleType(this Type type)
        {
            return SimpleTypes.Contains(type);
        }
        public static IDictionary<string, string> ToDictionary(this object source)
        {
            if (source == null)
                return new Dictionary<string, string>();
            var type = source.GetType();
            var dict = new Dictionary<string, string>();
            if (type.IsValueType)
                return dict;
            var props = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var prop in props)
            {
                dict.Add(prop.Name, prop.GetValue(source, null)?.ToString());
            }
            return dict;
        }

        public static string GetValue(this IDictionary<string, string> dict, string key)
        {
            string value;
            if (dict != null && dict.TryGetValue(key, out value))
                return value;
            return string.Empty;
        }

        /// <summary> 判断类型是否为Nullable类型 </summary>
        /// <param name="type"> 要处理的类型 </param>
        /// <returns> 是返回True，不是返回False </returns>
        public static bool IsNullableType(this Type type)
        {
            return ((type != null) && type.IsGenericType) && (type.GetGenericTypeDefinition() == typeof(Nullable<>));
        }

        /// <summary> 由类型的Nullable类型返回实际类型 </summary>
        /// <param name="type"> 要处理的类型对象 </param>
        /// <returns> </returns>
        public static Type GetNonNummableType(this Type type)
        {
            return IsNullableType(type) ? type.GetGenericArguments()[0] : type;
        }

        /// <summary> 通过类型转换器获取Nullable类型的基础类型 </summary>
        /// <param name="type"> 要处理的类型对象 </param>
        /// <returns> </returns>
        public static Type GetUnNullableType(this Type type)
        {
            if (!IsNullableType(type)) return type;
            var nullableConverter = new NullableConverter(type);
            return nullableConverter.UnderlyingType;
        }

        /// <summary> 对象转换为泛型 </summary>
        /// <param name="obj"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T CastTo<T>(this object obj)
        {
            return obj.CastTo(default(T));
        }

        /// <summary> 对象转换为泛型 </summary>
        /// <param name="obj"></param>
        /// <param name="def"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T CastTo<T>(this object obj, T def)
        {
            var value = obj.CastTo(typeof(T));
            if (value == null)
                return def;
            return (T)value;
        }

        /// <summary> 把对象类型转换为指定类型 </summary>
        /// <param name="obj"></param>
        /// <param name="conversionType"></param>
        /// <returns></returns>
        public static object CastTo(this object obj, Type conversionType)
        {
            if (obj == null)
            {
                return conversionType.IsGenericType ? Activator.CreateInstance(conversionType) : null;
            }
            if (conversionType.IsNullableType())
                conversionType = conversionType.GetUnNullableType();
            try
            {
                if (conversionType == obj.GetType())
                    return obj;
                if (conversionType.IsEnum)
                {
                    if (obj is string)
                        return Enum.Parse(conversionType, obj as string);
                    return Enum.ToObject(conversionType, obj);
                }
                if (!conversionType.IsInterface && conversionType.IsGenericType)
                {
                    var innerType = conversionType.GetGenericArguments()[0];
                    var innerValue = CastTo(obj, innerType);
                    return Activator.CreateInstance(conversionType, innerValue);
                }
                if (obj is string && conversionType == typeof(Guid))
                    return new Guid((string)obj);
                if (obj is string && conversionType == typeof(Version))
                    return new Version((string)obj);
                if (obj is string && conversionType == typeof(Color))
                {
                    var color = ColorConverter.ConvertFromString((string)obj);
                    return (Color?)color ?? Colors.Transparent;
                }
                if (!(obj is IConvertible))
                    return obj;
                return Convert.ChangeType(obj, conversionType);
            }
            catch
            {
                return null;
            }
        }

        /// <summary> 读取配置文件 </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="configName"></param>
        /// <param name="def"></param>
        /// <returns></returns>
        public static T Config<T>(this string configName, T def = default(T))
        {
            return ConfigHelper.GetAppSetting(null, def, supressKey: configName);
        }

        public static DateTime ToDateTime(this long ticks)
        {
            return new DateTime(1970, 1, 1).AddMilliseconds(ticks).ToLocalTime();
        }

        public static long TimeStamp(this DateTime time)
        {
            time = time.ToUniversalTime();
            return (long)(time - new DateTime(1970, 1, 1)).TotalMilliseconds;
        }

        /// <summary>
        /// 异常信息格式化
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="isHideStackTrace"></param>
        /// <returns></returns>
        public static string Format(this Exception ex, bool isHideStackTrace = false)
        {
            var sb = new StringBuilder();
            var count = 0;
            var appString = string.Empty;
            while (ex != null)
            {
                if (count > 0)
                {
                    appString += "  ";
                }
                sb.AppendLine($"{appString}异常消息：{ex.Message}");
                sb.AppendLine($"{appString}异常类型：{ex.GetType().FullName}");
                sb.AppendLine($"{appString}异常方法：{(ex.TargetSite == null ? null : ex.TargetSite.Name)}");
                sb.AppendLine($"{appString}异常源：{ex.Source}");
                if (!isHideStackTrace && ex.StackTrace != null)
                {
                    sb.AppendLine($"{appString}异常堆栈：{ex.StackTrace}");
                }
                if (ex.InnerException != null)
                {
                    sb.AppendLine($"{appString}内部异常：");
                    count++;
                }
                ex = ex.InnerException;
            }
            return sb.ToString();
        }

        /// <summary> 获取语言包文字 </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string L(this string key)
        {
            return Application.Current.FindResource(key)?.ToString();
        }

        public static T GetAttribute<T>(this PropertyInfo prop)
            where T : Attribute
        {
            var attr = prop.GetCustomAttributes(typeof(T), true).FirstOrDefault();
            return attr as T;
        }

        public static object Default(this Type type)
        {
            return type.IsValueType ? Activator.CreateInstance(type) : null;
        }

        public static double MmToPx(this double mm)
        {
            return (mm * 3.78D).Round();
        }

        public static double Round(this double number, int digits = 2)
        {
            return Math.Round(number, digits, MidpointRounding.AwayFromZero);
        }

        public static string HiddenPhone(this string phone)
        {
            if (string.IsNullOrWhiteSpace(phone)) return phone;
            if (phone.Length == 11)
                return phone.Substring(0, 3) + "****" + phone.Substring(phone.Length - 4, 4);
            return phone.Substring(0, 3) + "****";
        }
    }
}
