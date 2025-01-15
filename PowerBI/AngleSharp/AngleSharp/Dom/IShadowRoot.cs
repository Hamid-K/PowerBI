using System;
using AngleSharp.Attributes;

namespace AngleSharp.Dom
{
	// Token: 0x0200019B RID: 411
	[DomName("ShadowRoot")]
	public interface IShadowRoot : IDocumentFragment, INode, IEventTarget, IMarkupFormattable, IParentNode, INonElementParentNode
	{
		// Token: 0x170002F3 RID: 755
		// (get) Token: 0x06000EAC RID: 3756
		[DomName("activeElement")]
		IElement ActiveElement { get; }

		// Token: 0x170002F4 RID: 756
		// (get) Token: 0x06000EAD RID: 3757
		[DomName("host")]
		IElement Host { get; }

		// Token: 0x170002F5 RID: 757
		// (get) Token: 0x06000EAE RID: 3758
		// (set) Token: 0x06000EAF RID: 3759
		[DomName("innerHTML")]
		string InnerHtml { get; set; }

		// Token: 0x170002F6 RID: 758
		// (get) Token: 0x06000EB0 RID: 3760
		[DomName("styleSheets")]
		IStyleSheetList StyleSheets { get; }
	}
}
