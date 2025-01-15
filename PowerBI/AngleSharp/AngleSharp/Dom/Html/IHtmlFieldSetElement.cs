using System;
using AngleSharp.Attributes;
using AngleSharp.Dom.Css;
using AngleSharp.Dom.Events;

namespace AngleSharp.Dom.Html
{
	// Token: 0x020003BF RID: 959
	[DomName("HTMLFieldSetElement")]
	public interface IHtmlFieldSetElement : IHtmlElement, IElement, INode, IEventTarget, IMarkupFormattable, IParentNode, IChildNode, INonDocumentTypeChildNode, IElementCssInlineStyle, IGlobalEventHandlers, IValidation
	{
		// Token: 0x17000908 RID: 2312
		// (get) Token: 0x06001E3F RID: 7743
		// (set) Token: 0x06001E40 RID: 7744
		[DomName("disabled")]
		bool IsDisabled { get; set; }

		// Token: 0x17000909 RID: 2313
		// (get) Token: 0x06001E41 RID: 7745
		[DomName("form")]
		IHtmlFormElement Form { get; }

		// Token: 0x1700090A RID: 2314
		// (get) Token: 0x06001E42 RID: 7746
		// (set) Token: 0x06001E43 RID: 7747
		[DomName("name")]
		string Name { get; set; }

		// Token: 0x1700090B RID: 2315
		// (get) Token: 0x06001E44 RID: 7748
		[DomName("type")]
		string Type { get; }

		// Token: 0x1700090C RID: 2316
		// (get) Token: 0x06001E45 RID: 7749
		[DomName("elements")]
		IHtmlFormControlsCollection Elements { get; }
	}
}
