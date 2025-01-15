using System;
using System.Globalization;

namespace Microsoft.Owin
{
	// Token: 0x02000009 RID: 9
	public struct HostString : IEquatable<HostString>
	{
		// Token: 0x06000035 RID: 53 RVA: 0x0000251F File Offset: 0x0000071F
		public HostString(string value)
		{
			this._value = value;
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000036 RID: 54 RVA: 0x00002528 File Offset: 0x00000728
		public string Value
		{
			get
			{
				return this._value;
			}
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002530 File Offset: 0x00000730
		public override string ToString()
		{
			return this.ToUriComponent();
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002538 File Offset: 0x00000738
		public string ToUriComponent()
		{
			if (string.IsNullOrEmpty(this._value))
			{
				return string.Empty;
			}
			if (this._value.IndexOf('[') >= 0)
			{
				return this._value;
			}
			int index;
			if ((index = this._value.IndexOf(':')) >= 0 && index < this._value.Length - 1 && this._value.IndexOf(':', index + 1) >= 0)
			{
				return "[" + this._value + "]";
			}
			if (index >= 0)
			{
				string port = this._value.Substring(index);
				IdnMapping mapping = new IdnMapping();
				return mapping.GetAscii(this._value, 0, index) + port;
			}
			IdnMapping mapping2 = new IdnMapping();
			return mapping2.GetAscii(this._value);
		}

		// Token: 0x06000039 RID: 57 RVA: 0x000025F8 File Offset: 0x000007F8
		public static HostString FromUriComponent(string uriComponent)
		{
			int index;
			if (!string.IsNullOrEmpty(uriComponent) && uriComponent.IndexOf('[') < 0 && ((index = uriComponent.IndexOf(':')) < 0 || index >= uriComponent.Length - 1 || uriComponent.IndexOf(':', index + 1) < 0) && uriComponent.IndexOf("xn--", StringComparison.Ordinal) >= 0)
			{
				if (index >= 0)
				{
					string port = uriComponent.Substring(index);
					IdnMapping mapping = new IdnMapping();
					uriComponent = mapping.GetUnicode(uriComponent, 0, index) + port;
				}
				else
				{
					IdnMapping mapping2 = new IdnMapping();
					uriComponent = mapping2.GetUnicode(uriComponent);
				}
			}
			return new HostString(uriComponent);
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002687 File Offset: 0x00000887
		public static HostString FromUriComponent(Uri uri)
		{
			if (uri == null)
			{
				throw new ArgumentNullException("uri");
			}
			return new HostString(uri.GetComponents(UriComponents.Host | UriComponents.StrongPort | UriComponents.NormalizedHost, UriFormat.Unescaped));
		}

		// Token: 0x0600003B RID: 59 RVA: 0x000026AE File Offset: 0x000008AE
		public bool Equals(HostString other)
		{
			return string.Equals(this._value, other._value, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x0600003C RID: 60 RVA: 0x000026C2 File Offset: 0x000008C2
		public override bool Equals(object obj)
		{
			return obj != null && obj is HostString && this.Equals((HostString)obj);
		}

		// Token: 0x0600003D RID: 61 RVA: 0x000026DF File Offset: 0x000008DF
		public override int GetHashCode()
		{
			if (this._value == null)
			{
				return 0;
			}
			return StringComparer.OrdinalIgnoreCase.GetHashCode(this._value);
		}

		// Token: 0x0600003E RID: 62 RVA: 0x000026FB File Offset: 0x000008FB
		public static bool operator ==(HostString left, HostString right)
		{
			return left.Equals(right);
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002705 File Offset: 0x00000905
		public static bool operator !=(HostString left, HostString right)
		{
			return !left.Equals(right);
		}

		// Token: 0x04000014 RID: 20
		private readonly string _value;
	}
}
