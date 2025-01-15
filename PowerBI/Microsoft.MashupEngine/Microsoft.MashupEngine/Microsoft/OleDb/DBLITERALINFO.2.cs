using System;
using System.Runtime.InteropServices;

namespace Microsoft.OleDb
{
	// Token: 0x02001EEB RID: 7915
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct DBLITERALINFO
	{
		// Token: 0x04006438 RID: 25656
		public unsafe char* literalValue;

		// Token: 0x04006439 RID: 25657
		public unsafe char* invalidChars;

		// Token: 0x0400643A RID: 25658
		public unsafe char* invalidStartingChars;

		// Token: 0x0400643B RID: 25659
		public DBLITERAL literal;

		// Token: 0x0400643C RID: 25660
		public int supported;

		// Token: 0x0400643D RID: 25661
		public uint maxLength;
	}
}
