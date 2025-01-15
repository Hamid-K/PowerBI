using System;
using System.Runtime.InteropServices;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase
{
	// Token: 0x020000A6 RID: 166
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct DBLITERALINFO
	{
		// Token: 0x04000317 RID: 791
		public unsafe char* LiteralValue;

		// Token: 0x04000318 RID: 792
		public unsafe char* InvalidChars;

		// Token: 0x04000319 RID: 793
		public unsafe char* InvalidStartingChars;

		// Token: 0x0400031A RID: 794
		public DBLITERAL Literal;

		// Token: 0x0400031B RID: 795
		public int Supported;

		// Token: 0x0400031C RID: 796
		public uint MaxLength;
	}
}
