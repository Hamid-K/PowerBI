using System;
using System.Runtime.InteropServices;

namespace Microsoft.Mashup.Engine1.Library.Parquet
{
	// Token: 0x02001F36 RID: 7990
	[StructLayout(LayoutKind.Explicit, Size = 12)]
	internal struct ParquetInterval
	{
		// Token: 0x06010CBA RID: 68794 RVA: 0x0039D76E File Offset: 0x0039B96E
		public ParquetInterval(uint months, uint days, uint milliseconds)
		{
			this.Months = months;
			this.Days = days;
			this.Milliseconds = milliseconds;
		}

		// Token: 0x040064AC RID: 25772
		[FieldOffset(0)]
		public readonly uint Months;

		// Token: 0x040064AD RID: 25773
		[FieldOffset(4)]
		public readonly uint Days;

		// Token: 0x040064AE RID: 25774
		[FieldOffset(8)]
		public readonly uint Milliseconds;
	}
}
