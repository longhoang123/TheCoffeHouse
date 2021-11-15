using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using Xamarin.Forms.Internals;
using System.IO;
using System.Security.Cryptography;

namespace TheCoffeHouse.Helpers
{
    public static class StringHelper
    {
        public static string[] ArrUnicode { get; } =
        { "á", "à", "ả", "ã", "ạ", "â", "ấ", "ầ", "ẩ", "ẫ", "ậ", "ă", "ắ", "ằ", "ẳ", "ẵ", "ặ",
            "đ",
            "é","è","ẻ","ẽ","ẹ","ê","ế","ề","ể","ễ","ệ",
            "í","ì","ỉ","ĩ","ị",
            "ó","ò","ỏ","õ","ọ","ô","ố","ồ","ổ","ỗ","ộ","ơ","ớ","ờ","ở","ỡ","ợ",
            "ú","ù","ủ","ũ","ụ","ư","ứ","ừ","ử","ữ","ự",
            "ý","ỳ","ỷ","ỹ","ỵ",};

        public static string[] ArrAlphabet { get; } =
        { "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a",
            "d",
            "e","e","e","e","e","e","e","e","e","e","e",
            "i","i","i","i","i",
            "o","o","o","o","o","o","o","o","o","o","o","o","o","o","o","o","o",
            "u","u","u","u","u","u","u","u","u","u","u",
            "y","y","y","y","y",};

        public static string RemoveUnicodeCharacter(string text)
        {
            for (var i = 0; i < ArrUnicode.Length; i++)
            {
                text = text.Replace(ArrUnicode[i], ArrAlphabet[i]);
                text = text.Replace(ArrUnicode[i].ToUpper(), ArrAlphabet[i].ToUpper());
            }
            return text;
        }

        public static StringContent KeyValuePairToStringContent(
            this IEnumerable<KeyValuePair<string, string>> inputEnumerator,
            string url = null)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append('{');
            foreach (var current in inputEnumerator)
            {
                if (stringBuilder.Length > 1)
                {
                    stringBuilder.Append(',');
                }

                stringBuilder.Append("\"" + current.Key + "\"");
                stringBuilder.Append(':');
                stringBuilder.Append("\"" + current.Value + "\"");
            }
            stringBuilder.Append('}');
#if DEBUG
            //write json to send
            FormatJsonThenWrite(stringBuilder.ToString(), $"Init input for: {url ?? ""}");
#endif
            var queryString = new StringContent(stringBuilder.ToString(), Encoding.UTF8, "application/json");
            return queryString;
        }

        public static StringContent KeyValuePairToStringContent(
            this IEnumerable<KeyValuePair<string, object>> inputEnumerator,
            string url = null)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append('{');
            foreach (var current in inputEnumerator)
            {
                if (stringBuilder.Length > 1)
                {
                    stringBuilder.Append(',');
                }

                stringBuilder.Append("\"" + current.Key + "\"");
                stringBuilder.Append(':');
                stringBuilder.Append("\"" + current.Value + "\"");
            }
            stringBuilder.Append('}');
#if DEBUG
            //write json to send
            FormatJsonThenWrite(stringBuilder.ToString(), $"Init input for: {url ?? ""}");
#endif
            var queryString = new StringContent(stringBuilder.ToString(), Encoding.UTF8, "application/json");
            return queryString;
        }

        public static StringContent ObjectToStringContent<T>(this T objectToSend, string url = null)
        {
            var json = JsonConvert.SerializeObject(objectToSend,
                settings: new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
#if DEBUG
            //write json to send
            FormatJsonThenWrite(json, $"Init input for: {url ?? ""}");
#endif
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            return content;
        }

        /// <summary>
        /// ToYourObject customize parse jarray
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jArray"></param>
        /// <returns></returns>
        public static T ToYourObject<T>(this JArray jArray) where T : class
        {
            try
            {
                return jArray.ToObject<T>(new JsonSerializer { NullValueHandling = NullValueHandling.Ignore });
            }
            catch (Exception e)
            {
#if DEBUG
                Debug.WriteLine($"Can't parse with error: {e.Message}");
#endif
                return null;
            }
        }

        /// <summary>
        /// ToYourObject customize parse jobject
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jObject"></param>
        /// <returns></returns>
        public static T ToYourObject<T>(this JObject jObject) where T : class
        {
            try
            {
                return jObject.ToObject<T>(new JsonSerializer { NullValueHandling = NullValueHandling.Ignore });
            }
            catch (Exception e)
            {
#if DEBUG
                Debug.WriteLine($"Can't parse with error: {e.Message}");
#endif
                return null;
            }
        }

        public static void FormatJsonThenWrite(string str, string tag)
        {
            var jsonFormat = FormatJson(str);
#if DEBUG
            Debug.WriteLine($"{tag} \n {jsonFormat}");
#endif
        }

        public static string FormatJson(string str)
        {
            const string indentString = "    ";
            var indent = 0;
            var quoted = false;
            var sb = new StringBuilder();
            for (var i = 0; i < str.Length; i++)
            {
                var ch = str[i];
                switch (ch)
                {
                    case '{':
                    case '[':
                        sb.Append(ch);
                        if (!quoted)
                        {
                            sb.AppendLine();
                            Enumerable.Range(0, ++indent).ForEach(item => sb.Append(indentString));
                        }
                        break;
                    case '}':
                    case ']':
                        if (!quoted)
                        {
                            sb.AppendLine();
                            Enumerable.Range(0, --indent).ForEach(item => sb.Append(indentString));
                        }
                        sb.Append(ch);
                        break;
                    case '"':
                        sb.Append(ch);
                        var escaped = false;
                        var index = i;
                        while (index > 0 && str[--index] == '\\')
                            escaped = !escaped;
                        if (!escaped)
                            quoted = !quoted;
                        break;
                    case ',':
                        sb.Append(ch);
                        if (!quoted)
                        {
                            sb.AppendLine();
                            Enumerable.Range(0, indent).ForEach(item => sb.Append(indentString));
                        }
                        break;
                    case ':':
                        sb.Append(ch);
                        if (!quoted)
                            sb.Append(" ");
                        break;
                    default:
                        sb.Append(ch);
                        break;
                }
            }
            return sb.ToString();
        }

        public static bool StringToBool(this string input)
        {
            return !string.IsNullOrEmpty(input) && input.ToLower().Equals("true");
        }

        public static string EncryptString(this string plainText, string key)
        {
            byte[] iv = new byte[16];
            byte[] array;

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = iv;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter streamWriter = new StreamWriter((Stream)cryptoStream))
                        {
                            streamWriter.Write(plainText);
                        }

                        array = memoryStream.ToArray();
                    }
                }
            }

            return Convert.ToBase64String(array);
        }

        public static string DecryptString(this string cipherText, string key)
        {
            byte[] iv = new byte[16];
            byte[] buffer = Convert.FromBase64String(cipherText);

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = iv;
                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream(buffer))
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader streamReader = new StreamReader((Stream)cryptoStream))
                        {
                            return streamReader.ReadToEnd();
                        }
                    }
                }
            }
        }

        public static string GetAwardLargeImage(this string imageName)
        {
            if(imageName.Contains(".png"))
            {
                return AddImageWithSuffixes(imageName, "_Large", ".png");
            }
            else if(imageName.Contains(".PNG"))
            {
                return AddImageWithSuffixes(imageName, "_Large", ".PNG");
            }
            else
            {
                return imageName + "_Large";
            }
        }

        private static string AddImageWithSuffixes(string imageName, string suffixes, string fileExtension)
        {
            int index = imageName.IndexOf(fileExtension);

            if (index < 0)
                return imageName + suffixes;
            else
                return imageName.Insert(index, suffixes);
        }
    }
}
