using System;
using System.Collections.Generic;
using System.Globalization;

namespace Microsoft.Owin.Security
{
	// Token: 0x02000020 RID: 32
	public class AuthenticationProperties
	{
		// Token: 0x0600018E RID: 398 RVA: 0x000045CB File Offset: 0x000027CB
		public AuthenticationProperties()
		{
			this._dictionary = new Dictionary<string, string>(StringComparer.Ordinal);
		}

		// Token: 0x0600018F RID: 399 RVA: 0x000045E3 File Offset: 0x000027E3
		public AuthenticationProperties(IDictionary<string, string> dictionary)
		{
			this._dictionary = dictionary ?? new Dictionary<string, string>(StringComparer.Ordinal);
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x06000190 RID: 400 RVA: 0x00004600 File Offset: 0x00002800
		public IDictionary<string, string> Dictionary
		{
			get
			{
				return this._dictionary;
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x06000191 RID: 401 RVA: 0x00004608 File Offset: 0x00002808
		// (set) Token: 0x06000192 RID: 402 RVA: 0x0000461C File Offset: 0x0000281C
		public bool IsPersistent
		{
			get
			{
				return this._dictionary.ContainsKey(".persistent");
			}
			set
			{
				if (this._dictionary.ContainsKey(".persistent"))
				{
					if (!value)
					{
						this._dictionary.Remove(".persistent");
						return;
					}
				}
				else if (value)
				{
					this._dictionary.Add(".persistent", string.Empty);
				}
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x06000193 RID: 403 RVA: 0x00004668 File Offset: 0x00002868
		// (set) Token: 0x06000194 RID: 404 RVA: 0x0000468C File Offset: 0x0000288C
		public string RedirectUri
		{
			get
			{
				string value;
				if (!this._dictionary.TryGetValue(".redirect", out value))
				{
					return null;
				}
				return value;
			}
			set
			{
				if (value != null)
				{
					this._dictionary[".redirect"] = value;
					return;
				}
				if (this._dictionary.ContainsKey(".redirect"))
				{
					this._dictionary.Remove(".redirect");
				}
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x06000195 RID: 405 RVA: 0x000046C8 File Offset: 0x000028C8
		// (set) Token: 0x06000196 RID: 406 RVA: 0x00004714 File Offset: 0x00002914
		public DateTimeOffset? IssuedUtc
		{
			get
			{
				string value;
				DateTimeOffset dateTimeOffset;
				if (this._dictionary.TryGetValue(".issued", out value) && DateTimeOffset.TryParseExact(value, "r", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind, out dateTimeOffset))
				{
					return new DateTimeOffset?(dateTimeOffset);
				}
				return null;
			}
			set
			{
				if (value != null)
				{
					this._dictionary[".issued"] = value.Value.ToString("r", CultureInfo.InvariantCulture);
					return;
				}
				if (this._dictionary.ContainsKey(".issued"))
				{
					this._dictionary.Remove(".issued");
				}
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x06000197 RID: 407 RVA: 0x00004778 File Offset: 0x00002978
		// (set) Token: 0x06000198 RID: 408 RVA: 0x000047C4 File Offset: 0x000029C4
		public DateTimeOffset? ExpiresUtc
		{
			get
			{
				string value;
				DateTimeOffset dateTimeOffset;
				if (this._dictionary.TryGetValue(".expires", out value) && DateTimeOffset.TryParseExact(value, "r", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind, out dateTimeOffset))
				{
					return new DateTimeOffset?(dateTimeOffset);
				}
				return null;
			}
			set
			{
				if (value != null)
				{
					this._dictionary[".expires"] = value.Value.ToString("r", CultureInfo.InvariantCulture);
					return;
				}
				if (this._dictionary.ContainsKey(".expires"))
				{
					this._dictionary.Remove(".expires");
				}
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000199 RID: 409 RVA: 0x00004828 File Offset: 0x00002A28
		// (set) Token: 0x0600019A RID: 410 RVA: 0x00004864 File Offset: 0x00002A64
		public bool? AllowRefresh
		{
			get
			{
				string value;
				bool refresh;
				if (this._dictionary.TryGetValue(".refresh", out value) && bool.TryParse(value, out refresh))
				{
					return new bool?(refresh);
				}
				return null;
			}
			set
			{
				if (value != null)
				{
					this._dictionary[".refresh"] = value.Value.ToString(CultureInfo.InvariantCulture);
					return;
				}
				if (this._dictionary.ContainsKey(".refresh"))
				{
					this._dictionary.Remove(".refresh");
				}
			}
		}

		// Token: 0x04000042 RID: 66
		internal const string IssuedUtcKey = ".issued";

		// Token: 0x04000043 RID: 67
		internal const string ExpiresUtcKey = ".expires";

		// Token: 0x04000044 RID: 68
		internal const string IsPersistentKey = ".persistent";

		// Token: 0x04000045 RID: 69
		internal const string RedirectUriKey = ".redirect";

		// Token: 0x04000046 RID: 70
		internal const string RefreshKey = ".refresh";

		// Token: 0x04000047 RID: 71
		internal const string UtcDateTimeFormat = "r";

		// Token: 0x04000048 RID: 72
		private readonly IDictionary<string, string> _dictionary;
	}
}
