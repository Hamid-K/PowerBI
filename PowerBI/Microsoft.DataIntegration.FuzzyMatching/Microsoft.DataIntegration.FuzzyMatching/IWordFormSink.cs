using System;
using System.Runtime.InteropServices;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x020000DF RID: 223
	[Guid("fe77c330-7f42-11ce-be57-00aa0051fe20")]
	[InterfaceType(1)]
	[ComImport]
	internal interface IWordFormSink
	{
		// Token: 0x060008FA RID: 2298
		void PutAltWord([MarshalAs(21)] string pwcInBuf, [MarshalAs(8)] int cwc);

		// Token: 0x060008FB RID: 2299
		void PutWord([MarshalAs(21)] string pwcInBuf, [MarshalAs(8)] int cwc);
	}
}
