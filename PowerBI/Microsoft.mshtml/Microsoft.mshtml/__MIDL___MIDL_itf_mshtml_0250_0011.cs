using System;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000CAA RID: 3242
	[ComConversionLoss]
	[StructLayout(0, Pack = 4)]
	public struct __MIDL___MIDL_itf_mshtml_0250_0011
	{
		// Token: 0x040003E7 RID: 999
		public uint cbSize;

		// Token: 0x040003E8 RID: 1000
		public uint fType;

		// Token: 0x040003E9 RID: 1001
		public uint fState;

		// Token: 0x040003EA RID: 1002
		public uint wID;

		// Token: 0x040003EB RID: 1003
		[ComConversionLoss]
		[ComAliasName("mshtml.wireHBITMAP")]
		public IntPtr hbmpChecked;

		// Token: 0x040003EC RID: 1004
		[ComConversionLoss]
		[ComAliasName("mshtml.wireHBITMAP")]
		public IntPtr hbmpUnchecked;

		// Token: 0x040003ED RID: 1005
		public uint dwItemData;

		// Token: 0x040003EE RID: 1006
		[MarshalAs(30, SizeConst = 80)]
		public ushort[] szString;

		// Token: 0x040003EF RID: 1007
		[ComConversionLoss]
		[ComAliasName("mshtml.wireHBITMAP")]
		public IntPtr hbmpItem;
	}
}
