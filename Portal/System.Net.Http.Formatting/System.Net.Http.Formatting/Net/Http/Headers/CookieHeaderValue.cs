using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Net.Http.Formatting;
using System.Text;
using System.Web.Http;

namespace System.Net.Http.Headers
{
	// Token: 0x02000026 RID: 38
	public class CookieHeaderValue : ICloneable
	{
		// Token: 0x0600015D RID: 349 RVA: 0x00005298 File Offset: 0x00003498
		public CookieHeaderValue(string name, string value)
		{
			CookieState cookieState = new CookieState(name, value);
			this.Cookies.Add(cookieState);
		}

		// Token: 0x0600015E RID: 350 RVA: 0x000052C0 File Offset: 0x000034C0
		public CookieHeaderValue(string name, NameValueCollection values)
		{
			CookieState cookieState = new CookieState(name, values);
			this.Cookies.Add(cookieState);
		}

		// Token: 0x0600015F RID: 351 RVA: 0x00004BD2 File Offset: 0x00002DD2
		protected CookieHeaderValue()
		{
		}

		// Token: 0x06000160 RID: 352 RVA: 0x000052E8 File Offset: 0x000034E8
		private CookieHeaderValue(CookieHeaderValue source)
		{
			if (source == null)
			{
				throw Error.ArgumentNull("source");
			}
			this.Expires = source.Expires;
			this.MaxAge = source.MaxAge;
			this.Domain = source.Domain;
			this.Path = source.Path;
			this.Secure = source.Secure;
			this.HttpOnly = source.HttpOnly;
			foreach (CookieState cookieState in source.Cookies)
			{
				this.Cookies.Add(cookieState.Clone<CookieState>());
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000161 RID: 353 RVA: 0x0000539C File Offset: 0x0000359C
		public Collection<CookieState> Cookies
		{
			get
			{
				if (this._cookies == null)
				{
					this._cookies = new Collection<CookieState>();
				}
				return this._cookies;
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000162 RID: 354 RVA: 0x000053B7 File Offset: 0x000035B7
		// (set) Token: 0x06000163 RID: 355 RVA: 0x000053BF File Offset: 0x000035BF
		public DateTimeOffset? Expires { get; set; }

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000164 RID: 356 RVA: 0x000053C8 File Offset: 0x000035C8
		// (set) Token: 0x06000165 RID: 357 RVA: 0x000053D0 File Offset: 0x000035D0
		public TimeSpan? MaxAge { get; set; }

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000166 RID: 358 RVA: 0x000053D9 File Offset: 0x000035D9
		// (set) Token: 0x06000167 RID: 359 RVA: 0x000053E1 File Offset: 0x000035E1
		public string Domain { get; set; }

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000168 RID: 360 RVA: 0x000053EA File Offset: 0x000035EA
		// (set) Token: 0x06000169 RID: 361 RVA: 0x000053F2 File Offset: 0x000035F2
		public string Path { get; set; }

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x0600016A RID: 362 RVA: 0x000053FB File Offset: 0x000035FB
		// (set) Token: 0x0600016B RID: 363 RVA: 0x00005403 File Offset: 0x00003603
		public bool Secure { get; set; }

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x0600016C RID: 364 RVA: 0x0000540C File Offset: 0x0000360C
		// (set) Token: 0x0600016D RID: 365 RVA: 0x00005414 File Offset: 0x00003614
		public bool HttpOnly { get; set; }

		// Token: 0x1700007A RID: 122
		public CookieState this[string name]
		{
			get
			{
				if (string.IsNullOrEmpty(name))
				{
					return null;
				}
				CookieState cookieState = this.Cookies.FirstOrDefault((CookieState c) => string.Equals(c.Name, name, StringComparison.OrdinalIgnoreCase));
				if (cookieState == null)
				{
					cookieState = new CookieState(name, string.Empty);
					this.Cookies.Add(cookieState);
				}
				return cookieState;
			}
		}

		// Token: 0x0600016F RID: 367 RVA: 0x00005484 File Offset: 0x00003684
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = true;
			foreach (CookieState cookieState in this.Cookies)
			{
				flag = CookieHeaderValue.AppendSegment(stringBuilder, flag, cookieState.ToString(), null);
			}
			if (this.Expires != null)
			{
				flag = CookieHeaderValue.AppendSegment(stringBuilder, flag, "expires", FormattingUtilities.DateToString(this.Expires.Value));
			}
			if (this.MaxAge != null)
			{
				flag = CookieHeaderValue.AppendSegment(stringBuilder, flag, "max-age", ((int)this.MaxAge.Value.TotalSeconds).ToString(NumberFormatInfo.InvariantInfo));
			}
			if (this.Domain != null)
			{
				flag = CookieHeaderValue.AppendSegment(stringBuilder, flag, "domain", this.Domain);
			}
			if (this.Path != null)
			{
				flag = CookieHeaderValue.AppendSegment(stringBuilder, flag, "path", this.Path);
			}
			if (this.Secure)
			{
				flag = CookieHeaderValue.AppendSegment(stringBuilder, flag, "secure", null);
			}
			if (this.HttpOnly)
			{
				flag = CookieHeaderValue.AppendSegment(stringBuilder, flag, "httponly", null);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000170 RID: 368 RVA: 0x000055C0 File Offset: 0x000037C0
		public object Clone()
		{
			return new CookieHeaderValue(this);
		}

		// Token: 0x06000171 RID: 369 RVA: 0x000055C8 File Offset: 0x000037C8
		public static bool TryParse(string input, out CookieHeaderValue parsedValue)
		{
			parsedValue = null;
			if (string.IsNullOrEmpty(input))
			{
				return false;
			}
			string[] array = input.Split(CookieHeaderValue.segmentSeparator);
			CookieHeaderValue cookieHeaderValue = new CookieHeaderValue();
			foreach (string text in array)
			{
				if (!CookieHeaderValue.ParseCookieSegment(cookieHeaderValue, text))
				{
					return false;
				}
			}
			if (cookieHeaderValue.Cookies.Count == 0)
			{
				return false;
			}
			parsedValue = cookieHeaderValue;
			return true;
		}

		// Token: 0x06000172 RID: 370 RVA: 0x00005624 File Offset: 0x00003824
		private static bool AppendSegment(StringBuilder builder, bool first, string name, string value)
		{
			if (first)
			{
				first = false;
			}
			else
			{
				builder.Append("; ");
			}
			builder.Append(name);
			if (value != null)
			{
				builder.Append("=");
				builder.Append(value);
			}
			return first;
		}

		// Token: 0x06000173 RID: 371 RVA: 0x0000565C File Offset: 0x0000385C
		private static bool ParseCookieSegment(CookieHeaderValue instance, string segment)
		{
			if (string.IsNullOrWhiteSpace(segment))
			{
				return true;
			}
			string[] array = segment.Split(CookieHeaderValue.nameValueSeparator, 2);
			if (array.Length < 1 || string.IsNullOrWhiteSpace(array[0]))
			{
				return false;
			}
			string text = array[0].Trim();
			if (string.Equals(text, "expires", StringComparison.OrdinalIgnoreCase))
			{
				DateTimeOffset dateTimeOffset;
				if (FormattingUtilities.TryParseDate(CookieHeaderValue.GetSegmentValue(array, null), out dateTimeOffset))
				{
					instance.Expires = new DateTimeOffset?(dateTimeOffset);
					return true;
				}
				return false;
			}
			else if (string.Equals(text, "max-age", StringComparison.OrdinalIgnoreCase))
			{
				int num;
				if (FormattingUtilities.TryParseInt32(CookieHeaderValue.GetSegmentValue(array, null), out num))
				{
					instance.MaxAge = new TimeSpan?(new TimeSpan(0, 0, num));
					return true;
				}
				return false;
			}
			else
			{
				if (string.Equals(text, "domain", StringComparison.OrdinalIgnoreCase))
				{
					instance.Domain = CookieHeaderValue.GetSegmentValue(array, null);
					return true;
				}
				if (string.Equals(text, "path", StringComparison.OrdinalIgnoreCase))
				{
					instance.Path = CookieHeaderValue.GetSegmentValue(array, "/");
					return true;
				}
				if (string.Equals(text, "secure", StringComparison.OrdinalIgnoreCase))
				{
					if (!string.IsNullOrWhiteSpace(CookieHeaderValue.GetSegmentValue(array, null)))
					{
						return false;
					}
					instance.Secure = true;
					return true;
				}
				else
				{
					if (!string.Equals(text, "httponly", StringComparison.OrdinalIgnoreCase))
					{
						string segmentValue = CookieHeaderValue.GetSegmentValue(array, null);
						bool flag;
						try
						{
							NameValueCollection nameValueCollection = new FormDataCollection(segmentValue).ReadAsNameValueCollection();
							CookieState cookieState = new CookieState(text, nameValueCollection);
							instance.Cookies.Add(cookieState);
							flag = true;
						}
						catch
						{
							flag = false;
						}
						return flag;
					}
					if (!string.IsNullOrWhiteSpace(CookieHeaderValue.GetSegmentValue(array, null)))
					{
						return false;
					}
					instance.HttpOnly = true;
					return true;
				}
			}
		}

		// Token: 0x06000174 RID: 372 RVA: 0x000057D8 File Offset: 0x000039D8
		private static string GetSegmentValue(string[] nameValuePair, string defaultValue)
		{
			if (nameValuePair.Length <= 1)
			{
				return defaultValue;
			}
			return FormattingUtilities.UnquoteToken(nameValuePair[1]);
		}

		// Token: 0x04000065 RID: 101
		private const string ExpiresToken = "expires";

		// Token: 0x04000066 RID: 102
		private const string MaxAgeToken = "max-age";

		// Token: 0x04000067 RID: 103
		private const string DomainToken = "domain";

		// Token: 0x04000068 RID: 104
		private const string PathToken = "path";

		// Token: 0x04000069 RID: 105
		private const string SecureToken = "secure";

		// Token: 0x0400006A RID: 106
		private const string HttpOnlyToken = "httponly";

		// Token: 0x0400006B RID: 107
		private const string DefaultPath = "/";

		// Token: 0x0400006C RID: 108
		private static readonly char[] segmentSeparator = new char[] { ';' };

		// Token: 0x0400006D RID: 109
		private static readonly char[] nameValueSeparator = new char[] { '=' };

		// Token: 0x0400006E RID: 110
		private Collection<CookieState> _cookies;
	}
}
