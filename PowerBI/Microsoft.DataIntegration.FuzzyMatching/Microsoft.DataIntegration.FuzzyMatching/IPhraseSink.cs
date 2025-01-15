using System;
using System.Runtime.InteropServices;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x020000DE RID: 222
	[Guid("CC906FF0-C058-101A-B554-08002B33B0E6")]
	[InterfaceType(1)]
	[ComImport]
	internal interface IPhraseSink
	{
		// Token: 0x060008F8 RID: 2296
		void PutSmallPhrase([MarshalAs(21)] string pwcNoun, [MarshalAs(8)] int cwcNoun, [MarshalAs(21)] string pwcModifier, [MarshalAs(8)] int cwcModifier, [MarshalAs(8)] int ulAttachmentType);

		// Token: 0x060008F9 RID: 2297
		void PutPhrase([MarshalAs(21)] string pwcPhrase, [MarshalAs(8)] int cwcPhrase);
	}
}
