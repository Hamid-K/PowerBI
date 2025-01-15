using System;
using System.Runtime.InteropServices;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x020000E3 RID: 227
	[Guid("D53552C8-77E3-101A-B552-08002B33B0E6")]
	[InterfaceType(1)]
	[ComImport]
	internal interface IWordBreaker
	{
		// Token: 0x06000903 RID: 2307
		void Init([MarshalAs(2)] bool fQuery, [MarshalAs(8)] int maxTokenSize, [MarshalAs(2)] out bool pfLicense);

		// Token: 0x06000904 RID: 2308
		void BreakText([MarshalAs(27)] ref TEXT_SOURCE pTextSource, [MarshalAs(28)] IWordSink pWordSink, [MarshalAs(28)] IPhraseSink pPhraseSink);

		// Token: 0x06000905 RID: 2309
		void GetLicenseToUse([MarshalAs(21)] out string ppwcsLicense);
	}
}
