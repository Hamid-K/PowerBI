using System;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase.Serialization
{
	// Token: 0x020000DA RID: 218
	internal enum ObjectKind
	{
		// Token: 0x040003B6 RID: 950
		Null = 1,
		// Token: 0x040003B7 RID: 951
		Int32,
		// Token: 0x040003B8 RID: 952
		Int64,
		// Token: 0x040003B9 RID: 953
		Double,
		// Token: 0x040003BA RID: 954
		Decimal,
		// Token: 0x040003BB RID: 955
		String,
		// Token: 0x040003BC RID: 956
		Boolean,
		// Token: 0x040003BD RID: 957
		DateTime,
		// Token: 0x040003BE RID: 958
		DBNull,
		// Token: 0x040003BF RID: 959
		Int16,
		// Token: 0x040003C0 RID: 960
		Byte,
		// Token: 0x040003C1 RID: 961
		Single,
		// Token: 0x040003C2 RID: 962
		Binary,
		// Token: 0x040003C3 RID: 963
		TimeSpan,
		// Token: 0x040003C4 RID: 964
		DateTimeOffset,
		// Token: 0x040003C5 RID: 965
		Guid,
		// Token: 0x040003C6 RID: 966
		Type,
		// Token: 0x040003C7 RID: 967
		SByte,
		// Token: 0x040003C8 RID: 968
		UInt16,
		// Token: 0x040003C9 RID: 969
		UInt32,
		// Token: 0x040003CA RID: 970
		UInt64,
		// Token: 0x040003CB RID: 971
		DataTable,
		// Token: 0x040003CC RID: 972
		Object,
		// Token: 0x040003CD RID: 973
		List,
		// Token: 0x040003CE RID: 974
		Record
	}
}
