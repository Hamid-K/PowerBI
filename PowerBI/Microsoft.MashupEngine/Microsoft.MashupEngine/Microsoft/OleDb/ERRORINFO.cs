using System;
using System.Runtime.InteropServices;

namespace Microsoft.OleDb
{
	// Token: 0x02001F0F RID: 7951
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct ERRORINFO
	{
		// Token: 0x04006452 RID: 25682
		public int hrError;

		// Token: 0x04006453 RID: 25683
		public uint dwMinor;

		// Token: 0x04006454 RID: 25684
		public Guid clsid;

		// Token: 0x04006455 RID: 25685
		public Guid iid;

		// Token: 0x04006456 RID: 25686
		public Guid dispid;
	}
}
