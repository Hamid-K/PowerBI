using System;
using System.Runtime.InteropServices;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x020000E0 RID: 224
	[Guid("efbaf140-7f42-11ce-be57-00aa0051fe20")]
	[InterfaceType(1)]
	[ComImport]
	internal interface IStemmer
	{
		// Token: 0x060008FC RID: 2300
		void Init([MarshalAs(8)] int ulMaxTokenSize, [MarshalAs(2)] out bool pfLicense);

		// Token: 0x060008FD RID: 2301
		void GenerateWordForms([MarshalAs(21)] string pwcInBuf, [MarshalAs(8)] int cwc, [MarshalAs(28)] IWordFormSink pStemSink);

		// Token: 0x060008FE RID: 2302
		void GetLicenseToUse([MarshalAs(21)] out string ppwcsLicense);
	}
}
