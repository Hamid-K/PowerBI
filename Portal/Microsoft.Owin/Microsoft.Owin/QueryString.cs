using System;

namespace Microsoft.Owin
{
	// Token: 0x02000017 RID: 23
	public struct QueryString : IEquatable<QueryString>
	{
		// Token: 0x0600012C RID: 300 RVA: 0x00003580 File Offset: 0x00001780
		public QueryString(string value)
		{
			this._value = value;
		}

		// Token: 0x0600012D RID: 301 RVA: 0x00003589 File Offset: 0x00001789
		public QueryString(string name, string value)
		{
			this._value = Uri.EscapeDataString(name) + "=" + Uri.EscapeDataString(value);
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x0600012E RID: 302 RVA: 0x000035A7 File Offset: 0x000017A7
		public string Value
		{
			get
			{
				return this._value;
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x0600012F RID: 303 RVA: 0x000035AF File Offset: 0x000017AF
		public bool HasValue
		{
			get
			{
				return !string.IsNullOrWhiteSpace(this._value);
			}
		}

		// Token: 0x06000130 RID: 304 RVA: 0x000035BF File Offset: 0x000017BF
		public override string ToString()
		{
			return this.ToUriComponent();
		}

		// Token: 0x06000131 RID: 305 RVA: 0x000035C7 File Offset: 0x000017C7
		public string ToUriComponent()
		{
			if (!this.HasValue)
			{
				return string.Empty;
			}
			return "?" + this._value.Replace("#", "%23");
		}

		// Token: 0x06000132 RID: 306 RVA: 0x000035F6 File Offset: 0x000017F6
		public static QueryString FromUriComponent(string uriComponent)
		{
			if (string.IsNullOrEmpty(uriComponent))
			{
				return new QueryString(string.Empty);
			}
			if (uriComponent[0] != '?')
			{
				throw new ArgumentException(Resources.Exception_QueryStringMustStartWithDelimiter, "uriComponent");
			}
			return new QueryString(uriComponent.Substring(1));
		}

		// Token: 0x06000133 RID: 307 RVA: 0x00003632 File Offset: 0x00001832
		public static QueryString FromUriComponent(Uri uri)
		{
			if (uri == null)
			{
				throw new ArgumentNullException("uri");
			}
			return new QueryString(uri.GetComponents(UriComponents.Query, UriFormat.UriEscaped));
		}

		// Token: 0x06000134 RID: 308 RVA: 0x00003656 File Offset: 0x00001856
		public bool Equals(QueryString other)
		{
			return string.Equals(this._value, other._value);
		}

		// Token: 0x06000135 RID: 309 RVA: 0x00003669 File Offset: 0x00001869
		public override bool Equals(object obj)
		{
			return obj != null && obj is QueryString && this.Equals((QueryString)obj);
		}

		// Token: 0x06000136 RID: 310 RVA: 0x00003686 File Offset: 0x00001886
		public override int GetHashCode()
		{
			if (this._value == null)
			{
				return 0;
			}
			return this._value.GetHashCode();
		}

		// Token: 0x06000137 RID: 311 RVA: 0x0000369D File Offset: 0x0000189D
		public static bool operator ==(QueryString left, QueryString right)
		{
			return left.Equals(right);
		}

		// Token: 0x06000138 RID: 312 RVA: 0x000036A7 File Offset: 0x000018A7
		public static bool operator !=(QueryString left, QueryString right)
		{
			return !left.Equals(right);
		}

		// Token: 0x0400002F RID: 47
		public static readonly QueryString Empty = new QueryString(string.Empty);

		// Token: 0x04000030 RID: 48
		private readonly string _value;
	}
}
