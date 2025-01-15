using System;
using SAP.Middleware.Connector;

namespace Microsoft.Mashup.SapBwProvider
{
	// Token: 0x0200001B RID: 27
	public class SapBwUser
	{
		// Token: 0x06000151 RID: 337 RVA: 0x00005793 File Offset: 0x00003993
		private SapBwUser(string user, SapBwDecimalNotation decimalNotation, string dateFormat, string timeFormat)
		{
			this.User = user;
			this.DecimalNotation = decimalNotation;
			this.DateFormat = dateFormat;
			this.TimeFormat = timeFormat;
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000152 RID: 338 RVA: 0x000057B8 File Offset: 0x000039B8
		// (set) Token: 0x06000153 RID: 339 RVA: 0x000057C0 File Offset: 0x000039C0
		public string User { get; private set; }

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000154 RID: 340 RVA: 0x000057C9 File Offset: 0x000039C9
		// (set) Token: 0x06000155 RID: 341 RVA: 0x000057D1 File Offset: 0x000039D1
		public SapBwDecimalNotation DecimalNotation { get; private set; }

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000156 RID: 342 RVA: 0x000057DA File Offset: 0x000039DA
		// (set) Token: 0x06000157 RID: 343 RVA: 0x000057E2 File Offset: 0x000039E2
		public string DateFormat { get; private set; }

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000158 RID: 344 RVA: 0x000057EB File Offset: 0x000039EB
		// (set) Token: 0x06000159 RID: 345 RVA: 0x000057F3 File Offset: 0x000039F3
		public string TimeFormat { get; private set; }

		// Token: 0x0600015A RID: 346 RVA: 0x000057FC File Offset: 0x000039FC
		public static SapBwUser New(SapBwConnection connection)
		{
			string text;
			IRfcStructure rfcStructure;
			if (connection.TryGetUserName(out text) && SapBwUser.TryGetUserDefaults(connection, text, out rfcStructure))
			{
				return new SapBwUser(null, Utils.ToDecimalNotation(rfcStructure["DCPFM"].GetString()), Utils.ToDateFormat(rfcStructure["DATFM"].GetString()), Utils.ToTimeFormat(rfcStructure["TIMEFM"].GetString()));
			}
			SapBwUser sapBwUser;
			if (SapBwUser.TryGetUser(connection, out sapBwUser))
			{
				return sapBwUser;
			}
			return new SapBwUser(null, Utils.CurrentCultureDecimalNotation(), Utils.ToDateFormat(null), Utils.ToTimeFormat(null));
		}

		// Token: 0x0600015B RID: 347 RVA: 0x00005888 File Offset: 0x00003A88
		private static bool TryGetUserDefaults(SapBwConnection connection, string username, out IRfcStructure defaults)
		{
			string cacheKey = connection.GetCacheKey("BAPI_USER_GET_DETAIL", "DEFAULTS", username, false);
			if (connection.Provider.Structures.TryGetValue(cacheKey, out defaults))
			{
				return true;
			}
			IRfcFunction function = connection.GetFunction("BAPI_USER_GET_DETAIL", false);
			function.SetValue("USERNAME", username);
			function.SetValue("CACHE_RESULTS", 'X');
			if (connection.TryInvokeStatelessFunction(function, false, username))
			{
				defaults = function.GetStructure("DEFAULTS");
				if (defaults != null)
				{
					connection.Provider.Structures[cacheKey] = defaults;
					return true;
				}
			}
			defaults = null;
			return false;
		}

		// Token: 0x0600015C RID: 348 RVA: 0x0000591C File Offset: 0x00003B1C
		private static bool TryGetUser(SapBwConnection connection, out SapBwUser user)
		{
			object[] array;
			if (new TableQuery(connection)
			{
				Table = "USR01",
				Fields = "BNAME,DCPFM,DATFM,TIMEFM",
				Where = "BNAME = SY-UNAME"
			}.TryExtractSingleRowUsingCache(false, out array))
			{
				user = new SapBwUser(array[0] as string, Utils.ToDecimalNotation(array[1] as string), Utils.ToDateFormat(array[2] as string), Utils.ToTimeFormat(array[3] as string));
				return true;
			}
			user = null;
			return false;
		}
	}
}
