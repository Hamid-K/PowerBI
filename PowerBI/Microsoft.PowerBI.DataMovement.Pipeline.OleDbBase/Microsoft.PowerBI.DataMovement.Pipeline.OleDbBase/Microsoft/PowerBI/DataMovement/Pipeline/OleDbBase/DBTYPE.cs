using System;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase
{
	// Token: 0x0200005B RID: 91
	[CLSCompliant(false)]
	public enum DBTYPE : ushort
	{
		// Token: 0x0400020C RID: 524
		EMPTY,
		// Token: 0x0400020D RID: 525
		NULL,
		// Token: 0x0400020E RID: 526
		I2,
		// Token: 0x0400020F RID: 527
		I4,
		// Token: 0x04000210 RID: 528
		R4,
		// Token: 0x04000211 RID: 529
		R8,
		// Token: 0x04000212 RID: 530
		CY,
		// Token: 0x04000213 RID: 531
		DATE,
		// Token: 0x04000214 RID: 532
		BSTR,
		// Token: 0x04000215 RID: 533
		IDISPATCH,
		// Token: 0x04000216 RID: 534
		ERROR,
		// Token: 0x04000217 RID: 535
		BOOL,
		// Token: 0x04000218 RID: 536
		VARIANT,
		// Token: 0x04000219 RID: 537
		IUNKNOWN,
		// Token: 0x0400021A RID: 538
		DECIMAL,
		// Token: 0x0400021B RID: 539
		UI1 = 17,
		// Token: 0x0400021C RID: 540
		ARRAY = 8192,
		// Token: 0x0400021D RID: 541
		BYREF = 16384,
		// Token: 0x0400021E RID: 542
		I1 = 16,
		// Token: 0x0400021F RID: 543
		UI2 = 18,
		// Token: 0x04000220 RID: 544
		UI4,
		// Token: 0x04000221 RID: 545
		I8,
		// Token: 0x04000222 RID: 546
		UI8,
		// Token: 0x04000223 RID: 547
		GUID = 72,
		// Token: 0x04000224 RID: 548
		VECTOR = 4096,
		// Token: 0x04000225 RID: 549
		RESERVED = 32768,
		// Token: 0x04000226 RID: 550
		BYTES = 128,
		// Token: 0x04000227 RID: 551
		STR,
		// Token: 0x04000228 RID: 552
		WSTR,
		// Token: 0x04000229 RID: 553
		NUMERIC,
		// Token: 0x0400022A RID: 554
		UDT,
		// Token: 0x0400022B RID: 555
		DBDATE,
		// Token: 0x0400022C RID: 556
		DBTIME,
		// Token: 0x0400022D RID: 557
		DBTIMESTAMP,
		// Token: 0x0400022E RID: 558
		HCHAPTER,
		// Token: 0x0400022F RID: 559
		XML = 141,
		// Token: 0x04000230 RID: 560
		DBTIME2 = 145,
		// Token: 0x04000231 RID: 561
		DBTIMESTAMPOFFSET,
		// Token: 0x04000232 RID: 562
		DBDURATION = 1600,
		// Token: 0x04000233 RID: 563
		FILETIME = 64,
		// Token: 0x04000234 RID: 564
		PROPVARIANT = 138,
		// Token: 0x04000235 RID: 565
		VARNUMERIC
	}
}
