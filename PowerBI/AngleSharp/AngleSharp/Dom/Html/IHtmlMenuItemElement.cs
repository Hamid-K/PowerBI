using System;
using AngleSharp.Attributes;
using AngleSharp.Dom.Css;
using AngleSharp.Dom.Events;

namespace AngleSharp.Dom.Html
{
	// Token: 0x020003D1 RID: 977
	[DomName("HTMLMenuItemElement")]
	public interface IHtmlMenuItemElement : IHtmlElement, IElement, INode, IEventTarget, IMarkupFormattable, IParentNode, IChildNode, INonDocumentTypeChildNode, IElementCssInlineStyle, IGlobalEventHandlers
	{
		// Token: 0x17000989 RID: 2441
		// (get) Token: 0x06001F28 RID: 7976
		[DomName("command")]
		IHtmlElement Command { get; }

		// Token: 0x1700098A RID: 2442
		// (get) Token: 0x06001F29 RID: 7977
		// (set) Token: 0x06001F2A RID: 7978
		[DomName("type")]
		string Type { get; set; }

		// Token: 0x1700098B RID: 2443
		// (get) Token: 0x06001F2B RID: 7979
		// (set) Token: 0x06001F2C RID: 7980
		[DomName("label")]
		string Label { get; set; }

		// Token: 0x1700098C RID: 2444
		// (get) Token: 0x06001F2D RID: 7981
		// (set) Token: 0x06001F2E RID: 7982
		[DomName("icon")]
		string Icon { get; set; }

		// Token: 0x1700098D RID: 2445
		// (get) Token: 0x06001F2F RID: 7983
		// (set) Token: 0x06001F30 RID: 7984
		[DomName("disabled")]
		bool IsDisabled { get; set; }

		// Token: 0x1700098E RID: 2446
		// (get) Token: 0x06001F31 RID: 7985
		// (set) Token: 0x06001F32 RID: 7986
		[DomName("checked")]
		bool IsChecked { get; set; }

		// Token: 0x1700098F RID: 2447
		// (get) Token: 0x06001F33 RID: 7987
		// (set) Token: 0x06001F34 RID: 7988
		[DomName("default")]
		bool IsDefault { get; set; }

		// Token: 0x17000990 RID: 2448
		// (get) Token: 0x06001F35 RID: 7989
		// (set) Token: 0x06001F36 RID: 7990
		[DomName("radiogroup")]
		string RadioGroup { get; set; }
	}
}
