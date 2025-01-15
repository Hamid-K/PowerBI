using System;

namespace Microsoft.Data.Experimental.OData.Query
{
	// Token: 0x02000027 RID: 39
	public sealed class KeywordSegmentQueryToken : SegmentQueryToken
	{
		// Token: 0x0600009B RID: 155 RVA: 0x0000490C File Offset: 0x00002B0C
		public KeywordSegmentQueryToken(KeywordKind keyword, SegmentQueryToken parent)
			: base(QueryTokenUtils.GetNameFromKeywordKind(keyword), parent, null)
		{
			this.keyword = keyword;
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600009C RID: 156 RVA: 0x00004923 File Offset: 0x00002B23
		public override QueryTokenKind Kind
		{
			get
			{
				return QueryTokenKind.KeywordSegment;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x0600009D RID: 157 RVA: 0x00004927 File Offset: 0x00002B27
		public KeywordKind Keyword
		{
			get
			{
				return this.keyword;
			}
		}

		// Token: 0x04000137 RID: 311
		private readonly KeywordKind keyword;
	}
}
