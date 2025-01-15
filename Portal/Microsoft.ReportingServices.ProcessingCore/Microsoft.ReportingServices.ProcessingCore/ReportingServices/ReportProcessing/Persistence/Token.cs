using System;

namespace Microsoft.ReportingServices.ReportProcessing.Persistence
{
	// Token: 0x0200079C RID: 1948
	internal enum Token : byte
	{
		// Token: 0x0400367D RID: 13949
		Null,
		// Token: 0x0400367E RID: 13950
		Object,
		// Token: 0x0400367F RID: 13951
		EndObject,
		// Token: 0x04003680 RID: 13952
		Reference,
		// Token: 0x04003681 RID: 13953
		Enum,
		// Token: 0x04003682 RID: 13954
		TypedArray,
		// Token: 0x04003683 RID: 13955
		Array,
		// Token: 0x04003684 RID: 13956
		Declaration,
		// Token: 0x04003685 RID: 13957
		DataFieldInfo,
		// Token: 0x04003686 RID: 13958
		Guid = 239,
		// Token: 0x04003687 RID: 13959
		String,
		// Token: 0x04003688 RID: 13960
		DateTime,
		// Token: 0x04003689 RID: 13961
		TimeSpan,
		// Token: 0x0400368A RID: 13962
		Char,
		// Token: 0x0400368B RID: 13963
		Boolean,
		// Token: 0x0400368C RID: 13964
		Int16,
		// Token: 0x0400368D RID: 13965
		Int32,
		// Token: 0x0400368E RID: 13966
		Int64,
		// Token: 0x0400368F RID: 13967
		UInt16,
		// Token: 0x04003690 RID: 13968
		UInt32,
		// Token: 0x04003691 RID: 13969
		UInt64,
		// Token: 0x04003692 RID: 13970
		Byte,
		// Token: 0x04003693 RID: 13971
		SByte,
		// Token: 0x04003694 RID: 13972
		Single,
		// Token: 0x04003695 RID: 13973
		Double,
		// Token: 0x04003696 RID: 13974
		Decimal
	}
}
