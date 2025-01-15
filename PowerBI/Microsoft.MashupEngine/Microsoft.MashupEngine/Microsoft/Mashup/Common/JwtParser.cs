using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Mashup.Shims.Json;

namespace Microsoft.Mashup.Common
{
	// Token: 0x02001BFC RID: 7164
	public static class JwtParser
	{
		// Token: 0x0600B2E1 RID: 45793 RVA: 0x00246790 File Offset: 0x00244990
		public static bool TryParseData(string accessToken, out DateTime expires, out string signature, out string tokenAudience)
		{
			string[] array = accessToken.Split(new char[] { '.' });
			byte[] array2;
			Dictionary<string, object> dictionary;
			object obj;
			double num;
			if (array.Length == 3 && JwtParser.TryFromBase64Url(array[1], out array2) && JwtParser.TryDeserializeJson(array2, out dictionary) && dictionary.TryGetValue("exp", out obj) && JwtParser.TryGetDouble(obj, out num))
			{
				expires = JwtParser.unixEpoch.AddSeconds(num);
				signature = array[2];
				object obj2;
				tokenAudience = (dictionary.TryGetValue("aud", out obj2) ? obj2.ToString() : string.Empty);
				return true;
			}
			expires = default(DateTime);
			signature = null;
			tokenAudience = null;
			return false;
		}

		// Token: 0x0600B2E2 RID: 45794 RVA: 0x00246830 File Offset: 0x00244A30
		public static bool TryParseSubject(string accessToken, out string subject)
		{
			string[] array = accessToken.Split(new char[] { '.' });
			byte[] array2;
			Dictionary<string, object> dictionary;
			object obj;
			if (array.Length == 3 && JwtParser.TryFromBase64Url(array[1], out array2) && JwtParser.TryDeserializeJson(array2, out dictionary) && dictionary.TryGetValue("sub", out obj) && obj is string)
			{
				subject = (string)obj;
			}
			subject = null;
			return false;
		}

		// Token: 0x0600B2E3 RID: 45795 RVA: 0x00246890 File Offset: 0x00244A90
		private static bool TryFromBase64Url(string text, out byte[] binary)
		{
			StringBuilder stringBuilder = new StringBuilder(text);
			stringBuilder.Replace('-', '+');
			stringBuilder.Replace('_', '/');
			int num = stringBuilder.Length % 4;
			if (num != 2)
			{
				if (num == 3)
				{
					stringBuilder.Append("=");
				}
			}
			else
			{
				stringBuilder.Append("==");
			}
			bool flag;
			try
			{
				binary = Convert.FromBase64String(stringBuilder.ToString());
				flag = true;
			}
			catch (FormatException)
			{
				binary = null;
				flag = false;
			}
			return flag;
		}

		// Token: 0x0600B2E4 RID: 45796 RVA: 0x00246914 File Offset: 0x00244B14
		private static bool TryDeserializeJson(byte[] json, out Dictionary<string, object> obj)
		{
			bool flag;
			try
			{
				obj = Json.DeserializeObject<Dictionary<string, object>>(Encoding.UTF8.GetString(json));
				flag = obj != null;
			}
			catch (Exception ex)
			{
				if (!SafeExceptions.IsSafeException(ex))
				{
					throw;
				}
				obj = null;
				flag = false;
			}
			return flag;
		}

		// Token: 0x0600B2E5 RID: 45797 RVA: 0x0024695C File Offset: 0x00244B5C
		private static bool TryGetDouble(object obj, out double value)
		{
			if (obj != null)
			{
				TypeCode typeCode = Type.GetTypeCode(obj.GetType());
				if (typeCode == TypeCode.Int32)
				{
					value = (double)((int)obj);
					return true;
				}
				if (typeCode == TypeCode.Int64)
				{
					value = (double)((long)obj);
					return true;
				}
				if (typeCode == TypeCode.Double)
				{
					value = (double)obj;
					return true;
				}
			}
			value = 0.0;
			return false;
		}

		// Token: 0x04005B57 RID: 23383
		private static readonly DateTime unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
	}
}
