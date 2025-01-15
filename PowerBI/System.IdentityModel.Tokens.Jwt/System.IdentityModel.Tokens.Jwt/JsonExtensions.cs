using System;
using Microsoft.IdentityModel.Json;
using Microsoft.IdentityModel.Logging;

namespace System.IdentityModel.Tokens.Jwt
{
	// Token: 0x02000005 RID: 5
	public static class JsonExtensions
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000009 RID: 9 RVA: 0x00002050 File Offset: 0x00000250
		// (set) Token: 0x0600000A RID: 10 RVA: 0x00002057 File Offset: 0x00000257
		public static Serializer Serializer
		{
			get
			{
				return JsonExtensions._serializer;
			}
			set
			{
				if (value == null)
				{
					throw LogHelper.LogExceptionMessage(new ArgumentNullException("value"));
				}
				JsonExtensions._serializer = value;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000B RID: 11 RVA: 0x00002073 File Offset: 0x00000273
		// (set) Token: 0x0600000C RID: 12 RVA: 0x0000207A File Offset: 0x0000027A
		public static Deserializer Deserializer
		{
			get
			{
				return JsonExtensions._deserializer;
			}
			set
			{
				if (value == null)
				{
					throw LogHelper.LogExceptionMessage(new ArgumentNullException("value"));
				}
				JsonExtensions._deserializer = value;
			}
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002096 File Offset: 0x00000296
		public static string SerializeToJson(object value)
		{
			return JsonExtensions.Serializer(value);
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000020A3 File Offset: 0x000002A3
		public static T DeserializeFromJson<T>(string jsonString) where T : class
		{
			return JsonExtensions.Deserializer(jsonString, typeof(T)) as T;
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000020C4 File Offset: 0x000002C4
		public static JwtHeader DeserializeJwtHeader(string jsonString)
		{
			return JsonExtensions.Deserializer(jsonString, typeof(JwtHeader)) as JwtHeader;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000020E0 File Offset: 0x000002E0
		public static JwtPayload DeserializeJwtPayload(string jsonString)
		{
			return JsonExtensions.Deserializer(jsonString, typeof(JwtPayload)) as JwtPayload;
		}

		// Token: 0x04000004 RID: 4
		private static Serializer _serializer = new Serializer(JsonConvert.SerializeObject);

		// Token: 0x04000005 RID: 5
		private static Deserializer _deserializer = new Deserializer(JsonConvert.DeserializeObject);
	}
}
