using System;

namespace Microsoft.Mashup.Engine1.Library.GoogleAnalytics
{
	// Token: 0x02000B28 RID: 2856
	public class StringEqualityComparison : IComparable
	{
		// Token: 0x06004F26 RID: 20262 RVA: 0x00107614 File Offset: 0x00105814
		public StringEqualityComparison(string value, bool prefix)
		{
			this.Value = value;
			this.Prefix = prefix;
		}

		// Token: 0x06004F27 RID: 20263 RVA: 0x0010762C File Offset: 0x0010582C
		public int CompareTo(object obj)
		{
			StringEqualityComparison stringEqualityComparison = obj as StringEqualityComparison;
			return string.Compare(this.Value, stringEqualityComparison.Value, StringComparison.Ordinal);
		}

		// Token: 0x04002A8B RID: 10891
		public readonly string Value;

		// Token: 0x04002A8C RID: 10892
		public readonly bool Prefix;
	}
}
