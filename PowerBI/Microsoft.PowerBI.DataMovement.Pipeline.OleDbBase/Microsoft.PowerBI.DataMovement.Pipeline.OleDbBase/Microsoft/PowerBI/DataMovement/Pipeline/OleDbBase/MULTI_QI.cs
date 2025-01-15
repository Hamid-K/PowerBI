using System;
using System.Runtime.InteropServices;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase
{
	// Token: 0x020000B0 RID: 176
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct MULTI_QI
	{
		// Token: 0x04000342 RID: 834
		public unsafe Guid* IID;

		// Token: 0x04000343 RID: 835
		public IntPtr Itf;

		// Token: 0x04000344 RID: 836
		public int Hr;
	}
}
