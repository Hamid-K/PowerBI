using System;

namespace Microsoft.Mashup.Engine.Interface.Serialization
{
	// Token: 0x02000139 RID: 313
	internal enum ObjectKind
	{
		// Token: 0x0400034A RID: 842
		Null = 1,
		// Token: 0x0400034B RID: 843
		Int32,
		// Token: 0x0400034C RID: 844
		Int64,
		// Token: 0x0400034D RID: 845
		Double,
		// Token: 0x0400034E RID: 846
		Decimal,
		// Token: 0x0400034F RID: 847
		String,
		// Token: 0x04000350 RID: 848
		Boolean,
		// Token: 0x04000351 RID: 849
		DateTime,
		// Token: 0x04000352 RID: 850
		DBNull,
		// Token: 0x04000353 RID: 851
		Int16,
		// Token: 0x04000354 RID: 852
		Byte,
		// Token: 0x04000355 RID: 853
		Single,
		// Token: 0x04000356 RID: 854
		Binary,
		// Token: 0x04000357 RID: 855
		TimeSpan,
		// Token: 0x04000358 RID: 856
		DateTimeOffset,
		// Token: 0x04000359 RID: 857
		Guid,
		// Token: 0x0400035A RID: 858
		Type,
		// Token: 0x0400035B RID: 859
		SByte,
		// Token: 0x0400035C RID: 860
		UInt16,
		// Token: 0x0400035D RID: 861
		UInt32,
		// Token: 0x0400035E RID: 862
		UInt64,
		// Token: 0x0400035F RID: 863
		DataTable,
		// Token: 0x04000360 RID: 864
		Object,
		// Token: 0x04000361 RID: 865
		List,
		// Token: 0x04000362 RID: 866
		Record
	}
}
