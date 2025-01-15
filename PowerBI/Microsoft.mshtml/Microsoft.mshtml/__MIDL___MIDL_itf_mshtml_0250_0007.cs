using System;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000C9F RID: 3231
	[StructLayout(0, Pack = 4)]
	public struct __MIDL___MIDL_itf_mshtml_0250_0007
	{
		// Token: 0x040003A6 RID: 934
		public uint dwSize;

		// Token: 0x040003A7 RID: 935
		public uint dwStyle;

		// Token: 0x040003A8 RID: 936
		public uint dwCount;

		// Token: 0x040003A9 RID: 937
		public uint dwSelection;

		// Token: 0x040003AA RID: 938
		public uint dwPageStart;

		// Token: 0x040003AB RID: 939
		public uint dwPageSize;

		// Token: 0x040003AC RID: 940
		[MarshalAs(30, SizeConst = 1)]
		public uint[] dwOffset;
	}
}
