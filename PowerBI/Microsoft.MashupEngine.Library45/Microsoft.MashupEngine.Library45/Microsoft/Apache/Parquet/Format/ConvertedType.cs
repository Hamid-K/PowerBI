using System;

namespace Microsoft.Apache.Parquet.Format
{
	// Token: 0x02002005 RID: 8197
	internal enum ConvertedType
	{
		// Token: 0x04006797 RID: 26519
		UTF8,
		// Token: 0x04006798 RID: 26520
		MAP,
		// Token: 0x04006799 RID: 26521
		MAP_KEY_VALUE,
		// Token: 0x0400679A RID: 26522
		LIST,
		// Token: 0x0400679B RID: 26523
		ENUM,
		// Token: 0x0400679C RID: 26524
		DECIMAL,
		// Token: 0x0400679D RID: 26525
		DATE,
		// Token: 0x0400679E RID: 26526
		TIME_MILLIS,
		// Token: 0x0400679F RID: 26527
		TIME_MICROS,
		// Token: 0x040067A0 RID: 26528
		TIMESTAMP_MILLIS,
		// Token: 0x040067A1 RID: 26529
		TIMESTAMP_MICROS,
		// Token: 0x040067A2 RID: 26530
		UINT_8,
		// Token: 0x040067A3 RID: 26531
		UINT_16,
		// Token: 0x040067A4 RID: 26532
		UINT_32,
		// Token: 0x040067A5 RID: 26533
		UINT_64,
		// Token: 0x040067A6 RID: 26534
		INT_8,
		// Token: 0x040067A7 RID: 26535
		INT_16,
		// Token: 0x040067A8 RID: 26536
		INT_32,
		// Token: 0x040067A9 RID: 26537
		INT_64,
		// Token: 0x040067AA RID: 26538
		JSON,
		// Token: 0x040067AB RID: 26539
		BSON,
		// Token: 0x040067AC RID: 26540
		INTERVAL
	}
}
