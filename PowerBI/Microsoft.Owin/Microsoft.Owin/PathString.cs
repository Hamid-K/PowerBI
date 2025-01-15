using System;
using System.Text;
using Microsoft.Owin.Infrastructure;

namespace Microsoft.Owin
{
	// Token: 0x02000016 RID: 22
	public struct PathString : IEquatable<PathString>
	{
		// Token: 0x06000118 RID: 280 RVA: 0x000031FA File Offset: 0x000013FA
		public PathString(string value)
		{
			if (!string.IsNullOrEmpty(value) && value[0] != '/')
			{
				throw new ArgumentException(Resources.Exception_PathMustStartWithSlash, "value");
			}
			this._value = value;
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000119 RID: 281 RVA: 0x00003226 File Offset: 0x00001426
		public string Value
		{
			get
			{
				return this._value;
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x0600011A RID: 282 RVA: 0x0000322E File Offset: 0x0000142E
		public bool HasValue
		{
			get
			{
				return !string.IsNullOrEmpty(this._value);
			}
		}

		// Token: 0x0600011B RID: 283 RVA: 0x0000323E File Offset: 0x0000143E
		public override string ToString()
		{
			return this.ToUriComponent();
		}

		// Token: 0x0600011C RID: 284 RVA: 0x00003248 File Offset: 0x00001448
		public string ToUriComponent()
		{
			if (!this.HasValue)
			{
				return string.Empty;
			}
			StringBuilder buffer = null;
			int start = 0;
			int count = 0;
			bool requiresEscaping = false;
			int i = 0;
			while (i < this._value.Length)
			{
				bool isPercentEncodedChar = PathStringHelper.IsPercentEncodedChar(this._value, i);
				if (PathStringHelper.IsValidPathChar(this._value[i]) || isPercentEncodedChar)
				{
					if (requiresEscaping)
					{
						if (buffer == null)
						{
							buffer = new StringBuilder(this._value.Length * 3);
						}
						buffer.Append(Uri.EscapeDataString(this._value.Substring(start, count)));
						requiresEscaping = false;
						start = i;
						count = 0;
					}
					if (isPercentEncodedChar)
					{
						count += 3;
						i += 3;
					}
					else
					{
						count++;
						i++;
					}
				}
				else
				{
					if (!requiresEscaping)
					{
						if (buffer == null)
						{
							buffer = new StringBuilder(this._value.Length * 3);
						}
						buffer.Append(this._value, start, count);
						requiresEscaping = true;
						start = i;
						count = 0;
					}
					count++;
					i++;
				}
			}
			if (count == this._value.Length && !requiresEscaping)
			{
				return this._value;
			}
			if (count > 0)
			{
				if (buffer == null)
				{
					buffer = new StringBuilder(this._value.Length * 3);
				}
				if (requiresEscaping)
				{
					buffer.Append(Uri.EscapeDataString(this._value.Substring(start, count)));
				}
				else
				{
					buffer.Append(this._value, start, count);
				}
			}
			return buffer.ToString();
		}

		// Token: 0x0600011D RID: 285 RVA: 0x0000339E File Offset: 0x0000159E
		public static PathString FromUriComponent(string uriComponent)
		{
			return new PathString(Uri.UnescapeDataString(uriComponent));
		}

		// Token: 0x0600011E RID: 286 RVA: 0x000033AB File Offset: 0x000015AB
		public static PathString FromUriComponent(Uri uri)
		{
			if (uri == null)
			{
				throw new ArgumentNullException("uri");
			}
			return new PathString("/" + uri.GetComponents(UriComponents.Path, UriFormat.Unescaped));
		}

		// Token: 0x0600011F RID: 287 RVA: 0x000033DC File Offset: 0x000015DC
		public bool StartsWithSegments(PathString other)
		{
			string value = this.Value ?? string.Empty;
			string value2 = other.Value ?? string.Empty;
			return value.StartsWith(value2, StringComparison.OrdinalIgnoreCase) && (value.Length == value2.Length || value[value2.Length] == '/');
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00003438 File Offset: 0x00001638
		public bool StartsWithSegments(PathString other, out PathString remaining)
		{
			string value = this.Value ?? string.Empty;
			string value2 = other.Value ?? string.Empty;
			if (value.StartsWith(value2, StringComparison.OrdinalIgnoreCase) && (value.Length == value2.Length || value[value2.Length] == '/'))
			{
				remaining = new PathString(value.Substring(value2.Length));
				return true;
			}
			remaining = PathString.Empty;
			return false;
		}

		// Token: 0x06000121 RID: 289 RVA: 0x000034B3 File Offset: 0x000016B3
		public PathString Add(PathString other)
		{
			return new PathString(this.Value + other.Value);
		}

		// Token: 0x06000122 RID: 290 RVA: 0x000034CC File Offset: 0x000016CC
		public string Add(QueryString other)
		{
			return this.ToUriComponent() + other.ToUriComponent();
		}

		// Token: 0x06000123 RID: 291 RVA: 0x000034E0 File Offset: 0x000016E0
		public bool Equals(PathString other)
		{
			return string.Equals(this._value, other._value, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06000124 RID: 292 RVA: 0x000034F4 File Offset: 0x000016F4
		public bool Equals(PathString other, StringComparison comparisonType)
		{
			return string.Equals(this._value, other._value, comparisonType);
		}

		// Token: 0x06000125 RID: 293 RVA: 0x00003508 File Offset: 0x00001708
		public override bool Equals(object obj)
		{
			return obj != null && obj is PathString && this.Equals((PathString)obj, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06000126 RID: 294 RVA: 0x00003526 File Offset: 0x00001726
		public override int GetHashCode()
		{
			if (this._value == null)
			{
				return 0;
			}
			return StringComparer.OrdinalIgnoreCase.GetHashCode(this._value);
		}

		// Token: 0x06000127 RID: 295 RVA: 0x00003542 File Offset: 0x00001742
		public static bool operator ==(PathString left, PathString right)
		{
			return left.Equals(right, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06000128 RID: 296 RVA: 0x0000354D File Offset: 0x0000174D
		public static bool operator !=(PathString left, PathString right)
		{
			return !left.Equals(right, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06000129 RID: 297 RVA: 0x0000355B File Offset: 0x0000175B
		public static PathString operator +(PathString left, PathString right)
		{
			return left.Add(right);
		}

		// Token: 0x0600012A RID: 298 RVA: 0x00003565 File Offset: 0x00001765
		public static string operator +(PathString left, QueryString right)
		{
			return left.Add(right);
		}

		// Token: 0x0400002D RID: 45
		public static readonly PathString Empty = new PathString(string.Empty);

		// Token: 0x0400002E RID: 46
		private readonly string _value;
	}
}
