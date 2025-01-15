using System;

namespace Microsoft.Mashup.Engine1.Library.Parquet
{
	// Token: 0x02001F58 RID: 8024
	public enum ParquetRecordReaderState
	{
		// Token: 0x04006533 RID: 25907
		Start,
		// Token: 0x04006534 RID: 25908
		End,
		// Token: 0x04006535 RID: 25909
		RecordStart,
		// Token: 0x04006536 RID: 25910
		RecordEnd,
		// Token: 0x04006537 RID: 25911
		NestedRecordStart,
		// Token: 0x04006538 RID: 25912
		NestedRecordEnd,
		// Token: 0x04006539 RID: 25913
		NestedListStart,
		// Token: 0x0400653A RID: 25914
		NestedListEnd,
		// Token: 0x0400653B RID: 25915
		Primitive
	}
}
