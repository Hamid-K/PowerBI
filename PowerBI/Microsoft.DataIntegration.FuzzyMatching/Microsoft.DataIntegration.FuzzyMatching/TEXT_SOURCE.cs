using System;
using System.Runtime.InteropServices;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x020000E2 RID: 226
	[StructLayout(0, CharSet = 3)]
	internal struct TEXT_SOURCE
	{
		// Token: 0x04000383 RID: 899
		[MarshalAs(38)]
		public delFillTextBuffer pfnFillTextBuffer;

		// Token: 0x04000384 RID: 900
		[MarshalAs(21)]
		public string awcBuffer;

		// Token: 0x04000385 RID: 901
		[MarshalAs(8)]
		public int iEnd;

		// Token: 0x04000386 RID: 902
		[MarshalAs(8)]
		public int iCur;
	}
}
