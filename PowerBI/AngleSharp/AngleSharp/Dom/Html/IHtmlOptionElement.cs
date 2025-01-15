using System;
using AngleSharp.Attributes;
using AngleSharp.Dom.Css;
using AngleSharp.Dom.Events;

namespace AngleSharp.Dom.Html
{
	// Token: 0x020003D6 RID: 982
	[DomName("HTMLOptionElement")]
	public interface IHtmlOptionElement : IHtmlElement, IElement, INode, IEventTarget, IMarkupFormattable, IParentNode, IChildNode, INonDocumentTypeChildNode, IElementCssInlineStyle, IGlobalEventHandlers
	{
		// Token: 0x170009A6 RID: 2470
		// (get) Token: 0x06001F5E RID: 8030
		// (set) Token: 0x06001F5F RID: 8031
		[DomName("disabled")]
		bool IsDisabled { get; set; }

		// Token: 0x170009A7 RID: 2471
		// (get) Token: 0x06001F60 RID: 8032
		[DomName("form")]
		IHtmlFormElement Form { get; }

		// Token: 0x170009A8 RID: 2472
		// (get) Token: 0x06001F61 RID: 8033
		// (set) Token: 0x06001F62 RID: 8034
		[DomName("label")]
		string Label { get; set; }

		// Token: 0x170009A9 RID: 2473
		// (get) Token: 0x06001F63 RID: 8035
		// (set) Token: 0x06001F64 RID: 8036
		[DomName("defaultSelected")]
		bool IsDefaultSelected { get; set; }

		// Token: 0x170009AA RID: 2474
		// (get) Token: 0x06001F65 RID: 8037
		// (set) Token: 0x06001F66 RID: 8038
		[DomName("selected")]
		bool IsSelected { get; set; }

		// Token: 0x170009AB RID: 2475
		// (get) Token: 0x06001F67 RID: 8039
		// (set) Token: 0x06001F68 RID: 8040
		[DomName("value")]
		string Value { get; set; }

		// Token: 0x170009AC RID: 2476
		// (get) Token: 0x06001F69 RID: 8041
		// (set) Token: 0x06001F6A RID: 8042
		[DomName("text")]
		string Text { get; set; }

		// Token: 0x170009AD RID: 2477
		// (get) Token: 0x06001F6B RID: 8043
		[DomName("index")]
		int Index { get; }
	}
}
