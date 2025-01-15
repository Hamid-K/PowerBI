using System;

namespace Microsoft.Mashup.Engine1.Language.Query
{
	// Token: 0x02001800 RID: 6144
	public enum QueryKind
	{
		// Token: 0x0400520C RID: 21004
		DataSource,
		// Token: 0x0400520D RID: 21005
		ProjectColumns,
		// Token: 0x0400520E RID: 21006
		SelectRows,
		// Token: 0x0400520F RID: 21007
		AddColumns,
		// Token: 0x04005210 RID: 21008
		SkipTake,
		// Token: 0x04005211 RID: 21009
		Sort,
		// Token: 0x04005212 RID: 21010
		Distinct,
		// Token: 0x04005213 RID: 21011
		Combine,
		// Token: 0x04005214 RID: 21012
		Group,
		// Token: 0x04005215 RID: 21013
		Join,
		// Token: 0x04005216 RID: 21014
		NestedJoin,
		// Token: 0x04005217 RID: 21015
		ExpandListColumn,
		// Token: 0x04005218 RID: 21016
		ExpandRecordColumn,
		// Token: 0x04005219 RID: 21017
		Unpivot,
		// Token: 0x0400521A RID: 21018
		Pivot
	}
}
