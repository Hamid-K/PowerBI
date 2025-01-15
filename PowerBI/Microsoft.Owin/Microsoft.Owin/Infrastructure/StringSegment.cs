using System;
using System.CodeDom.Compiler;

namespace Microsoft.Owin.Infrastructure
{
	// Token: 0x02000038 RID: 56
	[GeneratedCode("App_Packages", "")]
	internal struct StringSegment : IEquatable<StringSegment>
	{
		// Token: 0x06000216 RID: 534 RVA: 0x00005A59 File Offset: 0x00003C59
		public StringSegment(string buffer, int offset, int count)
		{
			this._buffer = buffer;
			this._offset = offset;
			this._count = count;
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x06000217 RID: 535 RVA: 0x00005A70 File Offset: 0x00003C70
		public string Buffer
		{
			get
			{
				return this._buffer;
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x06000218 RID: 536 RVA: 0x00005A78 File Offset: 0x00003C78
		public int Offset
		{
			get
			{
				return this._offset;
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x06000219 RID: 537 RVA: 0x00005A80 File Offset: 0x00003C80
		public int Count
		{
			get
			{
				return this._count;
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x0600021A RID: 538 RVA: 0x00005A88 File Offset: 0x00003C88
		public string Value
		{
			get
			{
				if (this._offset != -1)
				{
					return this._buffer.Substring(this._offset, this._count);
				}
				return null;
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x0600021B RID: 539 RVA: 0x00005AAC File Offset: 0x00003CAC
		public bool HasValue
		{
			get
			{
				return this._offset != -1 && this._count != 0 && this._buffer != null;
			}
		}

		// Token: 0x0600021C RID: 540 RVA: 0x00005ACA File Offset: 0x00003CCA
		public bool Equals(StringSegment other)
		{
			return string.Equals(this._buffer, other._buffer) && this._offset == other._offset && this._count == other._count;
		}

		// Token: 0x0600021D RID: 541 RVA: 0x00005AFD File Offset: 0x00003CFD
		public override bool Equals(object obj)
		{
			return obj != null && obj is StringSegment && this.Equals((StringSegment)obj);
		}

		// Token: 0x0600021E RID: 542 RVA: 0x00005B1C File Offset: 0x00003D1C
		public override int GetHashCode()
		{
			int hashCode = ((this._buffer != null) ? this._buffer.GetHashCode() : 0);
			hashCode = (hashCode * 397) ^ this._offset;
			return (hashCode * 397) ^ this._count;
		}

		// Token: 0x0600021F RID: 543 RVA: 0x00005B5F File Offset: 0x00003D5F
		public static bool operator ==(StringSegment left, StringSegment right)
		{
			return left.Equals(right);
		}

		// Token: 0x06000220 RID: 544 RVA: 0x00005B69 File Offset: 0x00003D69
		public static bool operator !=(StringSegment left, StringSegment right)
		{
			return !left.Equals(right);
		}

		// Token: 0x06000221 RID: 545 RVA: 0x00005B78 File Offset: 0x00003D78
		public bool StartsWith(string text, StringComparison comparisonType)
		{
			if (text == null)
			{
				throw new ArgumentNullException("text");
			}
			int textLength = text.Length;
			return this.HasValue && this._count >= textLength && string.Compare(this._buffer, this._offset, text, 0, textLength, comparisonType) == 0;
		}

		// Token: 0x06000222 RID: 546 RVA: 0x00005BC8 File Offset: 0x00003DC8
		public bool EndsWith(string text, StringComparison comparisonType)
		{
			if (text == null)
			{
				throw new ArgumentNullException("text");
			}
			int textLength = text.Length;
			return this.HasValue && this._count >= textLength && string.Compare(this._buffer, this._offset + this._count - textLength, text, 0, textLength, comparisonType) == 0;
		}

		// Token: 0x06000223 RID: 547 RVA: 0x00005C20 File Offset: 0x00003E20
		public bool Equals(string text, StringComparison comparisonType)
		{
			if (text == null)
			{
				throw new ArgumentNullException("text");
			}
			int textLength = text.Length;
			return this.HasValue && this._count == textLength && string.Compare(this._buffer, this._offset, text, 0, textLength, comparisonType) == 0;
		}

		// Token: 0x06000224 RID: 548 RVA: 0x00005C6D File Offset: 0x00003E6D
		public string Substring(int offset, int length)
		{
			return this._buffer.Substring(this._offset + offset, length);
		}

		// Token: 0x06000225 RID: 549 RVA: 0x00005C83 File Offset: 0x00003E83
		public StringSegment Subsegment(int offset, int length)
		{
			return new StringSegment(this._buffer, this._offset + offset, length);
		}

		// Token: 0x06000226 RID: 550 RVA: 0x00005C99 File Offset: 0x00003E99
		public override string ToString()
		{
			return this.Value ?? string.Empty;
		}

		// Token: 0x04000069 RID: 105
		private readonly string _buffer;

		// Token: 0x0400006A RID: 106
		private readonly int _offset;

		// Token: 0x0400006B RID: 107
		private readonly int _count;
	}
}
