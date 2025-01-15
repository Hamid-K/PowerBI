using System;
using AngleSharp.Attributes;
using AngleSharp.Dom.Css;
using AngleSharp.Dom.Events;

namespace AngleSharp.Dom.Html
{
	// Token: 0x020003BA RID: 954
	[DomName("HTMLDialogElement")]
	public interface IHtmlDialogElement : IHtmlElement, IElement, INode, IEventTarget, IMarkupFormattable, IParentNode, IChildNode, INonDocumentTypeChildNode, IElementCssInlineStyle, IGlobalEventHandlers
	{
		// Token: 0x170008F2 RID: 2290
		// (get) Token: 0x06001E10 RID: 7696
		// (set) Token: 0x06001E11 RID: 7697
		[DomName("open")]
		bool Open { get; set; }

		// Token: 0x170008F3 RID: 2291
		// (get) Token: 0x06001E12 RID: 7698
		// (set) Token: 0x06001E13 RID: 7699
		[DomName("returnValue")]
		string ReturnValue { get; set; }

		// Token: 0x06001E14 RID: 7700
		[DomName("show")]
		void Show(IElement anchor = null);

		// Token: 0x06001E15 RID: 7701
		[DomName("showModal")]
		void ShowModal(IElement anchor = null);

		// Token: 0x06001E16 RID: 7702
		[DomName("close")]
		void Close(string returnValue = null);
	}
}
