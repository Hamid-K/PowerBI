using System;
using AngleSharp.Attributes;

namespace AngleSharp.Dom
{
	// Token: 0x0200018B RID: 395
	[DomName("LinkImport")]
	[DomNoInterfaceObject]
	public interface ILinkImport
	{
		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x06000E46 RID: 3654
		[DomName("import")]
		IDocument Import { get; }
	}
}
