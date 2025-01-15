using System;
using AngleSharp.Attributes;

namespace AngleSharp.Dom
{
	// Token: 0x02000183 RID: 387
	[DomName("DocumentType")]
	public interface IDocumentType : INode, IEventTarget, IMarkupFormattable, IChildNode
	{
		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x06000E0C RID: 3596
		[DomName("name")]
		string Name { get; }

		// Token: 0x170002A8 RID: 680
		// (get) Token: 0x06000E0D RID: 3597
		[DomName("publicId")]
		string PublicIdentifier { get; }

		// Token: 0x170002A9 RID: 681
		// (get) Token: 0x06000E0E RID: 3598
		[DomName("systemId")]
		string SystemIdentifier { get; }
	}
}
