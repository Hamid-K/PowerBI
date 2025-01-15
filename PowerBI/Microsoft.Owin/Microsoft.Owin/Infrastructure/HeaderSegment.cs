using System;
using System.CodeDom.Compiler;

namespace Microsoft.Owin.Infrastructure
{
	// Token: 0x02000036 RID: 54
	[GeneratedCode("App_Packages", "")]
	internal struct HeaderSegment : IEquatable<HeaderSegment>
	{
		// Token: 0x06000205 RID: 517 RVA: 0x000058FF File Offset: 0x00003AFF
		public HeaderSegment(StringSegment formatting, StringSegment data)
		{
			this._formatting = formatting;
			this._data = data;
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x06000206 RID: 518 RVA: 0x0000590F File Offset: 0x00003B0F
		public StringSegment Formatting
		{
			get
			{
				return this._formatting;
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x06000207 RID: 519 RVA: 0x00005917 File Offset: 0x00003B17
		public StringSegment Data
		{
			get
			{
				return this._data;
			}
		}

		// Token: 0x06000208 RID: 520 RVA: 0x00005920 File Offset: 0x00003B20
		public bool Equals(HeaderSegment other)
		{
			return this._formatting.Equals(other._formatting) && this._data.Equals(other._data);
		}

		// Token: 0x06000209 RID: 521 RVA: 0x00005959 File Offset: 0x00003B59
		public override bool Equals(object obj)
		{
			return obj != null && obj is HeaderSegment && this.Equals((HeaderSegment)obj);
		}

		// Token: 0x0600020A RID: 522 RVA: 0x00005978 File Offset: 0x00003B78
		public override int GetHashCode()
		{
			return (this._formatting.GetHashCode() * 397) ^ this._data.GetHashCode();
		}

		// Token: 0x0600020B RID: 523 RVA: 0x000059B4 File Offset: 0x00003BB4
		public static bool operator ==(HeaderSegment left, HeaderSegment right)
		{
			return left.Equals(right);
		}

		// Token: 0x0600020C RID: 524 RVA: 0x000059BE File Offset: 0x00003BBE
		public static bool operator !=(HeaderSegment left, HeaderSegment right)
		{
			return !left.Equals(right);
		}

		// Token: 0x04000066 RID: 102
		private readonly StringSegment _formatting;

		// Token: 0x04000067 RID: 103
		private readonly StringSegment _data;
	}
}
