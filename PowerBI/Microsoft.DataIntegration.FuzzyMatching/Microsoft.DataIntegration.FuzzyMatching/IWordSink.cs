using System;
using System.Runtime.InteropServices;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x020000DD RID: 221
	[Guid("CC907054-C058-101A-B554-08002B33B0E6")]
	[InterfaceType(1)]
	[ComImport]
	internal interface IWordSink
	{
		// Token: 0x060008F3 RID: 2291
		void PutWord([MarshalAs(8)] int cwc, [MarshalAs(21)] string pwcInBuf, [MarshalAs(8)] int cwcSrcLen, [MarshalAs(8)] int cwcSrcPos);

		// Token: 0x060008F4 RID: 2292
		void PutAltWord([MarshalAs(8)] int cwc, [MarshalAs(21)] string pwcInBuf, [MarshalAs(8)] int cwcSrcLen, [MarshalAs(8)] int cwcSrcPos);

		// Token: 0x060008F5 RID: 2293
		void StartAltPhrase();

		// Token: 0x060008F6 RID: 2294
		void EndAltPhrase();

		// Token: 0x060008F7 RID: 2295
		void PutBreak(WORDREP_BREAK_TYPE breakType);
	}
}
