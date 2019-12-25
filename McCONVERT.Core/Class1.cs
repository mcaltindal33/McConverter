using System;
using System.Collections.Generic;
using System.Globalization;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;

public static class ConvertTo
{
    public static byte[] HexToBytes(this string data)
    {
        try
        {
            byte[] array = new byte[data.Length / 2];
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = Convert.ToByte(data.Substring(2 * i, 2), 16);
            }
            return array;
        }
        catch (Exception)
        {
            return new byte[1];
        }
    }
    public static decimal HexToDecimal(this string data)
    {
        try
        {
            data = data.Replace("x", string.Empty);
            long result = 0L;
            long.TryParse(data, NumberStyles.HexNumber, null, out result);
            return result;
        }
        catch (Exception)
        {
            return decimal.Zero;
        }
    }
    public static string DecimalToHex(this decimal data)
    {
        try
        {
            string result = string.Empty;
            if (data > decimal.One)
            {
                StringBuilder stringBuilder = new StringBuilder();
                while (data > decimal.One)
                {
                    decimal value = data % 16m;
                    data /= 16m;
                    stringBuilder.Insert(0, ((int)value).ToString("X"));
                }
                result = stringBuilder.ToString();
            }
            else
            {
                result = "0" + data.ToString();
            }
            if (result.Length % 2 == 1)
            {
                result = "0" + result;
            }
            return result;
        }
        catch (Exception)
        {
            return "00";
        }
    }
    public static string DecimalToHex(this string data)
    {
        try
        {
            string result = string.Empty;
            decimal value = default(decimal);
            decimal.TryParse(data, out value);
            if (value > decimal.One)
            {
                StringBuilder stringBuilder = new StringBuilder();
                while (value > decimal.One)
                {
                    decimal values = value % 16m;
                    value /= 16m;
                    stringBuilder.Insert(0, ((int)values).ToString("X"));
                }
                result = stringBuilder.ToString();
            }
            else
            {
                result = "0" + data.ToString();
            }
            if (result.Length % 2 == 1)
            {
                result = "0" + result;
            }
            return result;
        }
        catch (Exception)
        {
            return "00";
        }
    }
    public static string HexToAscii(this string data)
    {
        string result = string.Empty;
        try
        {
            for (int i = 0; i < data.Length; i += 2)
            {
                string hx = string.Empty;
                hx = data.Substring(i, 2);
                if (data.Length >= i + 2)
                    result += Convert.ToChar(Convert.ToUInt32(hx, 16));
            }
            return result;
        }
        catch (Exception)
        {
            return result;
        }
    }
    public static string AsciiToHex(this string data)
    {
        string result = string.Empty;
        try
        {
            foreach (char item in data)
            {
                int n = item;
                result += $"{Convert.ToUInt32(n.ToString()):X2}";
            }
            return result;
        }
        catch (Exception)
        {
            return result;
        }
    }
    public static string AsciiToHex(this int data)
    {
        string result = string.Empty;
        try
        {
            foreach (char item in data.ToString())
            {
                int n = item;
                result += $"{Convert.ToUInt32(n.ToString()):X2}";
            }
            return result;
        }
        catch (Exception)
        {
            return result;
        }
    }
    public static string ByteToHex(this string data)
    {
        try
        {
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                stringBuilder.Append(((byte)data[i]).ToString("X2"));
            }
            return stringBuilder.ToString();
        }
        catch (Exception)
        {
            return string.Empty;
        }
    }
    public static string ByteToHex(this byte[] data)
    {
        try
        {
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                byte b = data[i];
                stringBuilder.Append(b.ToString("X2"));
            }
            return stringBuilder.ToString();
        }
        catch (Exception)
        {
            return string.Empty;
        }
    }
    public static string HexToBinary(this string data)
    {
        return string.Join(string.Empty, from c in data select Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0'));
    }
    public static decimal BinaryToDecimal(this string data)
    {
        try
        {
            double num = 0.0;
            int result = 0;
            int.TryParse(data, out result);
            int length = result.ToString().Length;
            for (int i = 0; i < length; i++)
            {
                int num2 = result % 10;
                num += (double)num2 * Math.Pow(2.0, i);
                result /= 10;
            }
            return Convert.ToDecimal(num);
        }
        catch (Exception)
        {
            return 0;
        }
    }
    public static float HexToFloat(this string data)
    {
        try
        {
            return BitConverter.ToSingle(BitConverter.GetBytes(uint.Parse(data, NumberStyles.AllowHexSpecifier)), 0);
        }
        catch (Exception)
        {
            return 0f;
        }
    }
    public static string ToCelsius(this string data)
    {
        return data + " °C";
    }
    public static int ToInt32(this string data)
    {
        int result = 0;
        int.TryParse(data.Replace(".", ""), out result);
        return result;
    }
    public static int ToInt32(this object data)
    {
        int result = 0;
        int.TryParse(data.ToString().Replace(".", ""), out result);
        return result;
    }
    public static short ToInt16(this string data)
    {
        short result = 0;
        short.TryParse(data.Replace(".", ""), out result);
        return result;
    }
    public static short ToInt16(this object data)
    {
        short result = 0;
        short.TryParse(data.ToString().Replace(".", ""), out result);
        return result;
    }
    public static decimal ToDecimalN2(this string data)
    {
        decimal result = default(decimal);
        decimal.TryParse(data.Replace(".", ""), out result);
        return Convert.ToDecimal(result.ToString("N2"));
    }
    public static double ToDoubleN2(this string data)
    {
        double result = default(double);
        double.TryParse(data.Replace(".", ""), out result);
        return Convert.ToDouble(result.ToString("N2"));
    }
    public static double ToDoubleN2(this object data)
    {
        double result = default(double);
        double.TryParse(data.ToString(), out result);
        return Convert.ToDouble(result.ToString("N2"));
    }
    public static Tuple<bool, double> ToDoubleN2Special(this string data)
    {
        double value = default(double);
        bool item = double.TryParse(data, out value);
        return new Tuple<bool, double>(item, Convert.ToDouble(value.ToString("N2")));
    }

    public static Tuple<bool, decimal> ToDeimalN2Special(this string data)
    {
        decimal value = default(decimal);
        bool item = decimal.TryParse(data, out value);
        return new Tuple<bool, decimal>(item, Convert.ToDecimal(value.ToString("N2")));
    }
    public static decimal ToDescimalN3(this string data)
    {
        decimal value = default(decimal);
        decimal.TryParse(data, out value);
        return Convert.ToDecimal(value.ToString("N3"));
    }
    public static double ToDoubleN3(this string data)
    {
        double value = default(double);
        double.TryParse(data, out value);
        return Convert.ToDouble(value.ToString("N3"));
    }
    public static double ToDoubleN3(this object data)
    {
        double value = default(double);
        double.TryParse(data.ToString(), out value);
        return Convert.ToDouble(value.ToString("N3"));
    }
    public static string ToCurrencyN2(this string data)
    {
        return Convert.ToDecimal(data).ToString("C2");
    }
    public static string ToCurrencyN3(this string data)
    {
        return Convert.ToDecimal(data).ToString("C3");
    }
    public static string ToStringN2(this decimal data)
    {
        return data.ToString("N2");
    }
    public static string ToStringN0(this decimal data)
    {
        return data.ToString("N0");
    }
    public static string ToStringN3(this decimal data)
    {
        return data.ToString("N3");
    }
    public static string ToStringN2(this double data)
    {
        return data.ToString("N2");
    }
    public static string ToStringN0(this double data)
    {
        return data.ToString("N0");
    }
    public static string ToStringN1(this double data)
    {
        return data.ToString("N1");
    }
    public static string ToStringN3(this double data)
    {
        return data.ToString("N3");
    }
    public static byte[] ByteDonustur(this string data)
    {
        UnicodeEncoding unicodeEncoding = new UnicodeEncoding();
        return unicodeEncoding.GetBytes(data);
    }
    public static byte[] ByteCevir(this string data)
    {
        char[] array = data.ToCharArray();
        byte[] array2 = new byte[array.Length];
        for (int i = 0; i < array2.Length; i++)
        {
            array2[i] = Convert.ToByte(array[i]);
        }
        return array2;
    }
    public static byte ToByte(this string data)
    {
        byte value = 0;
        byte.TryParse(data, out value);
        return value;
    }
    public static double ToDouble(this string data)
    {
        double value = 0.0;
        double.TryParse(data, out value);
        return value;
    }
    public static double ToDouble(this object data)
    {
        double value = 0.0;
        double.TryParse(data.ToString(), out value);
        return value;
    }
    public static double ToDouble(this decimal data)
    {
        double value = 0.0;
        double.TryParse(data.ToString(), out value);
        return value;
    }
    public static decimal ToDecimal(this string data)
    {
        decimal value = 0;
        decimal.TryParse(data, out value);
        return value;
    }
    public static decimal ToDecimal(this object data)
    {
        decimal value = 0m;
        decimal.TryParse(data.ToString(), out value);
        return value;
    }
    public static decimal ToDecimal(this double data)
    {
        decimal value = 0m;
        decimal.TryParse(data.ToString(), out value);
        return value;
    }

    public static DateTime ToDateTime(this string data)
    {
        DateTime value;
        DateTime.TryParse(data, out value);
        return Convert.ToDateTime(value.ToString("dd.MM.yyyy HH:mm:ss"));
    }
    public static DateTime ToSalesDateTime(this string data)
    {
        string s = string.Empty;
        try
        {
            int v = 0;
            if (int.TryParse(data.Trim(), out v))
            {
                //150126 -- 020310
                for (int i = 0; i < 3; i++)
                {
                    s = s + "." + data.Substring(0, 2);
                    data = data.Substring(2, data.Length - 2);
                }
                s = s.TrimStart('.') + " ";
                for (int i = 0; i < 3; i++)
                {
                    s = s + data.Substring(0, 2) + ":";
                    data = data.Substring(2, data.Length - 2);
                }
                s = s.TrimEnd(':');
                s = "20" + s;
                DateTime d = DateTime.Now;
                if (DateTime.TryParse(s, out d))
                {
                    return DateTime.Parse(s);
                }
                else
                    return DateTime.Now;
            }
            else
                return DateTime.Now;
        }
        catch (Exception)
        {
            return DateTime.MinValue;
        }
    }

    public static DateTime ToDateTime(this string data, string format)
    {
        DateTime value;
        DateTime.TryParse(data, out value);
        return Convert.ToDateTime(value.ToString(format));
    }
    public static string ToDateTime(this DateTime date)
    {
        return date.ToString("yyyy-MM-ddTHH:mm:ss");
    }
    public static string ToSha256(this string data)
    {
        using (SHA256 sha = SHA256.Create())
        {
            if (data == null)
                data = "";
            return string.Join("", from c in sha.ComputeHash(Encoding.UTF8.GetBytes(data)) select c.ToString("x2"));
        }
    }
    public static bool ToBoolean(this string data)
    {

        if (data == null || data == "" || data.Trim().Length == 0)
        {
            data = "false";
        }
        bool value = false;
        bool.TryParse(data, out value);
        return value;
    }
    public static bool ToBoolean(this object data)
    {

        if (data == null || data.ToString() == "")
        {
            data = "false";
        }
        bool value = false;
        bool.TryParse(data.ToString(), out value);
        return value;
    }
    public static Guid ToGuid(this string data)
    {
        if (data == null || data == "" || data.Trim().Length == 0)
        {
            data = "00000000-0000-0000-0000-000000000000";
        }
        Guid value;
        Guid.TryParse(data, out value);
        return value;
    }
    public static bool IsNumerical(this string data)
    {
        if (data == null || data == "")
            data = "0";
        return Regex.Match(data, "^[-+]*[0-9,\\.]+$").Success;
    }
    public static long HexToLong(this string data)
    {
        if (data == null || data == "" || data.Trim().Length == 0)
            data = "0";
        return long.Parse(data, NumberStyles.HexNumber);
    }
    public static double DecimalToDouble(this decimal data)
    {
        double value = 0.0;
        double.TryParse(data.ToString(), out value);
        return value;
    }
    public static double RateToPrice(this double data, double rate)
    {
        double value = data * rate / 100;
        value = data - value;
        return value;
    }
    public static double PriceToRate(this double data, double price)
    {
        double value = 100 - (data * 100 / price);
        return value.ToDoubleN2();
    }
}
