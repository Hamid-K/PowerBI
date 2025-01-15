using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;
using Azure.Core;

namespace Azure
{
	// Token: 0x0200001D RID: 29
	[NullableContext(1)]
	[Nullable(0)]
	[JsonConverter(typeof(ETagConverter))]
	public readonly struct ETag : IEquatable<ETag>
	{
		// Token: 0x0600004C RID: 76 RVA: 0x00002416 File Offset: 0x00000616
		public ETag(string etag)
		{
			this._value = etag;
		}

		// Token: 0x0600004D RID: 77 RVA: 0x0000241F File Offset: 0x0000061F
		public static bool operator ==(ETag left, ETag right)
		{
			return left.Equals(right);
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002429 File Offset: 0x00000629
		public static bool operator !=(ETag left, ETag right)
		{
			return !left.Equals(right);
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002436 File Offset: 0x00000636
		public bool Equals(ETag other)
		{
			return string.Equals(this._value, other._value, StringComparison.Ordinal);
		}

		// Token: 0x06000050 RID: 80 RVA: 0x0000244A File Offset: 0x0000064A
		[NullableContext(2)]
		public bool Equals(string other)
		{
			return string.Equals(this._value, other, StringComparison.Ordinal);
		}

		// Token: 0x06000051 RID: 81 RVA: 0x0000245C File Offset: 0x0000065C
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			if (obj is ETag)
			{
				ETag etag = (ETag)obj;
				return this.Equals(etag);
			}
			return false;
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002481 File Offset: 0x00000681
		public override int GetHashCode()
		{
			return this._value.GetHashCodeOrdinal();
		}

		// Token: 0x06000053 RID: 83 RVA: 0x0000248E File Offset: 0x0000068E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString()
		{
			return this.ToString("G");
		}

		// Token: 0x06000054 RID: 84 RVA: 0x0000249C File Offset: 0x0000069C
		public string ToString(string format)
		{
			if (this._value == null)
			{
				return string.Empty;
			}
			string text;
			if (!(format == "H"))
			{
				if (!(format == "G"))
				{
					throw new ArgumentException("Invalid format string.");
				}
				text = this._value;
			}
			else
			{
				text = ((!ETag.IsValidQuotedFormat(this._value)) ? ("\"" + this._value + "\"") : this._value);
			}
			return text;
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002518 File Offset: 0x00000718
		internal static ETag Parse(string value)
		{
			if (value == ETag.All._value)
			{
				return ETag.All;
			}
			if (!ETag.IsValidQuotedFormat(value))
			{
				throw new ArgumentException("The value should be equal to * , be wrapped in quotes, or be wrapped in quotes prefixed by W/", "value");
			}
			if (value.StartsWith("W/\"", StringComparison.Ordinal))
			{
				return new ETag(value);
			}
			return new ETag(value.Trim(new char[] { '"' }));
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002580 File Offset: 0x00000780
		private static bool IsValidQuotedFormat(string value)
		{
			return ((value.StartsWith("\"", StringComparison.Ordinal) || value.StartsWith("W/\"", StringComparison.Ordinal)) && value.EndsWith("\"", StringComparison.Ordinal)) || value == ETag.All._value;
		}

		// Token: 0x04000031 RID: 49
		private const char QuoteCharacter = '"';

		// Token: 0x04000032 RID: 50
		private const string QuoteString = "\"";

		// Token: 0x04000033 RID: 51
		private const string WeakETagPrefix = "W/\"";

		// Token: 0x04000034 RID: 52
		private const string DefaultFormat = "G";

		// Token: 0x04000035 RID: 53
		private const string HeaderFormat = "H";

		// Token: 0x04000036 RID: 54
		[Nullable(2)]
		private readonly string _value;

		// Token: 0x04000037 RID: 55
		public static readonly ETag All = new ETag("*");
	}
}
