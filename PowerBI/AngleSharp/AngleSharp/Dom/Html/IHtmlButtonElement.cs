using System;
using AngleSharp.Attributes;
using AngleSharp.Dom.Css;
using AngleSharp.Dom.Events;

namespace AngleSharp.Dom.Html
{
	// Token: 0x020003B4 RID: 948
	[DomName("HTMLButtonElement")]
	public interface IHtmlButtonElement : IHtmlElement, IElement, INode, IEventTarget, IMarkupFormattable, IParentNode, IChildNode, INonDocumentTypeChildNode, IElementCssInlineStyle, IGlobalEventHandlers, IValidation
	{
		// Token: 0x170008DA RID: 2266
		// (get) Token: 0x06001DDF RID: 7647
		// (set) Token: 0x06001DE0 RID: 7648
		[DomName("autofocus")]
		bool Autofocus { get; set; }

		// Token: 0x170008DB RID: 2267
		// (get) Token: 0x06001DE1 RID: 7649
		// (set) Token: 0x06001DE2 RID: 7650
		[DomName("disabled")]
		bool IsDisabled { get; set; }

		// Token: 0x170008DC RID: 2268
		// (get) Token: 0x06001DE3 RID: 7651
		[DomName("form")]
		IHtmlFormElement Form { get; }

		// Token: 0x170008DD RID: 2269
		// (get) Token: 0x06001DE4 RID: 7652
		[DomName("labels")]
		INodeList Labels { get; }

		// Token: 0x170008DE RID: 2270
		// (get) Token: 0x06001DE5 RID: 7653
		// (set) Token: 0x06001DE6 RID: 7654
		[DomName("name")]
		string Name { get; set; }

		// Token: 0x170008DF RID: 2271
		// (get) Token: 0x06001DE7 RID: 7655
		// (set) Token: 0x06001DE8 RID: 7656
		[DomName("type")]
		string Type { get; set; }

		// Token: 0x170008E0 RID: 2272
		// (get) Token: 0x06001DE9 RID: 7657
		// (set) Token: 0x06001DEA RID: 7658
		[DomName("value")]
		string Value { get; set; }

		// Token: 0x170008E1 RID: 2273
		// (get) Token: 0x06001DEB RID: 7659
		// (set) Token: 0x06001DEC RID: 7660
		[DomName("formAction")]
		string FormAction { get; set; }

		// Token: 0x170008E2 RID: 2274
		// (get) Token: 0x06001DED RID: 7661
		// (set) Token: 0x06001DEE RID: 7662
		[DomName("formEncType")]
		string FormEncType { get; set; }

		// Token: 0x170008E3 RID: 2275
		// (get) Token: 0x06001DEF RID: 7663
		// (set) Token: 0x06001DF0 RID: 7664
		[DomName("formMethod")]
		string FormMethod { get; set; }

		// Token: 0x170008E4 RID: 2276
		// (get) Token: 0x06001DF1 RID: 7665
		// (set) Token: 0x06001DF2 RID: 7666
		[DomName("formNoValidate")]
		bool FormNoValidate { get; set; }

		// Token: 0x170008E5 RID: 2277
		// (get) Token: 0x06001DF3 RID: 7667
		// (set) Token: 0x06001DF4 RID: 7668
		[DomName("formTarget")]
		string FormTarget { get; set; }
	}
}
