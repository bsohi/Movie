using System.Text;
using System.ComponentModel.DataAnnotations;
using System;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.IO;
using System.IO.Compression;

namespace Common.Infrastructure
{
    public static class Extensions
    {
        public static string GetEnumDisplayName<T>(T value) where T : struct
        {
            // Get the MemberInfo object for supplied enum value
            var memberInfo = value.GetType().GetMember(value.ToString());
            if (memberInfo.Length != 1)
                return null;

            // Get DisplayAttibute on the supplied enum value
            var displayAttribute = memberInfo[0].GetCustomAttributes(typeof(DisplayAttribute), false)
                                   as DisplayAttribute[];
            if (displayAttribute == null || displayAttribute.Length != 1)
                return null;

            return displayAttribute[0].Name;
        }
        public static decimal ToDecimal(this object obj)
        {
            decimal lDecimal = 0;
            Decimal.TryParse(obj.ToString(), out lDecimal);
            return lDecimal;
        }
        public static int ToInt(this object obj)
        {
            int lInt = 0;
            Int32.TryParse(obj.ToString(), out lInt);
            return lInt;
        }
        public static bool ToBool(this string obj)
        {
            bool lReturn = false;
            if (obj == "1")
            {
                lReturn = true;
            }
            return lReturn;
        }
        public static double ToDouble(this object obj)
        {
            double.TryParse(obj.ToString(), out double outDouble);
            return outDouble;
        }
        
        public static string RemoveImageBytesWhenLogging(this string serializedObj)
        {
            try
            {
                string json = $"[{serializedObj ?? string.Empty}]";
                var result = JArray.Parse(json);
                if (result != null)
                {
                    result.Descendants()
                    .OfType<JProperty>()
                    .Where(attr => (attr.Name.Equals("FileContentBase64String") || attr.Name.Equals("CreatedBy") || attr.Name.Equals("UpdatedBy")))
                    .ToList() // you should call ToList because you're about to changing the result, which is not possible if it is IEnumerable
                    .ForEach(attr => attr.Remove()); // removing unwanted attributes
                    json = result.ToString(Newtonsoft.Json.Formatting.None); // backing result to json
                }
                return json;
            }
            catch(Exception ex)
            {
                return serializedObj;
            }
        }
        public static string ShrinkJsonSize(this string json, bool isMobile)
        {
            if (isMobile || Encoding.UTF8.GetByteCount(json) > 31000)
            {
                var buffer = Encoding.UTF8.GetBytes(json);
                var memoryStream = new MemoryStream();
                using (var stream = new GZipStream(memoryStream, CompressionMode.Compress, true))
                {
                    stream.Write(buffer, 0, buffer.Length);
                }
                memoryStream.Position = 0;
                var compressed = new byte[memoryStream.Length];
                memoryStream.Read(compressed, 0, compressed.Length);
                var gZipBuffer = new byte[compressed.Length + 4];
                Buffer.BlockCopy(compressed, 0, gZipBuffer, 4, compressed.Length);
                Buffer.BlockCopy(BitConverter.GetBytes(buffer.Length), 0, gZipBuffer, 0, 4);
                return Convert.ToBase64String(gZipBuffer);
            }
            return json;
        }

        public static string UnShrinkCompressedJson(this string compressedText)
        {
            byte[] gZipBuffer = Convert.FromBase64String(compressedText);
            using (var memoryStream = new MemoryStream())
            {
                int dataLength = BitConverter.ToInt32(gZipBuffer, 0);
                memoryStream.Write(gZipBuffer, 4, gZipBuffer.Length - 4);

                var buffer = new byte[dataLength];

                memoryStream.Position = 0;
                using (var gZipStream = new GZipStream(memoryStream, CompressionMode.Decompress))
                {
                    gZipStream.Read(buffer, 0, buffer.Length);
                }

                return Encoding.UTF8.GetString(buffer);
            }
        }

        public static string RemovePropertiesWhenLogging(this string serializedObj)
        {
            try
            {
                string json = $"[{serializedObj ?? string.Empty}]";
                var result = JArray.Parse(json);
                if (result != null)
                {
                    result.Descendants()
                    .OfType<JProperty>()
                    .Where(attr => (attr.Name.Equals("Password") || attr.Name.Equals("HashedPassword") || attr.Name.Equals("Claims") || attr.Name.Equals("AuthenticationToken")))
                    .ToList() // you should call ToList because you're about to changing the result, which is not possible if it is IEnumerable
                    .ForEach(attr => attr.Remove()); // removing unwanted attributes
                    json = result.ToString(Newtonsoft.Json.Formatting.None); // backing result to json
                }
                return json;
            }
            catch (Exception ex)
            {
                return serializedObj;
            }
        }
    }
}