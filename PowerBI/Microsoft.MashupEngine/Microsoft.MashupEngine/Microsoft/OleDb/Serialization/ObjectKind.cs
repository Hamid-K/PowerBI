using System;

namespace Microsoft.OleDb.Serialization
{
	// Token: 0x02001FB8 RID: 8120
	internal enum ObjectKind
	{
		// Token: 0x04006530 RID: 25904
		Null = 1,
		// Token: 0x04006531 RID: 25905
		Int32,
		// Token: 0x04006532 RID: 25906
		Int64,
		// Token: 0x04006533 RID: 25907
		Double,
		// Token: 0x04006534 RID: 25908
		Decimal,
		// Token: 0x04006535 RID: 25909
		String,
		// Token: 0x04006536 RID: 25910
		Boolean,
		// Token: 0x04006537 RID: 25911
		DateTime,
		// Token: 0x04006538 RID: 25912
		DBNull,
		// Token: 0x04006539 RID: 25913
		Int16,
		// Token: 0x0400653A RID: 25914
		Byte,
		// Token: 0x0400653B RID: 25915
		Single,
		// Token: 0x0400653C RID: 25916
		Binary,
		// Token: 0x0400653D RID: 25917
		TimeSpan,
		// Token: 0x0400653E RID: 25918
		DateTimeOffset,
		// Token: 0x0400653F RID: 25919
		Guid,
		// Token: 0x04006540 RID: 25920
		Type,
		// Token: 0x04006541 RID: 25921
		SByte,
		// Token: 0x04006542 RID: 25922
		UInt16,
		// Token: 0x04006543 RID: 25923
		UInt32,
		// Token: 0x04006544 RID: 25924
		UInt64,
		// Token: 0x04006545 RID: 25925
		DataTable,
		// Token: 0x04006546 RID: 25926
		Object,
		// Token: 0x04006547 RID: 25927
		List,
		// Token: 0x04006548 RID: 25928
		Record
	}
}
