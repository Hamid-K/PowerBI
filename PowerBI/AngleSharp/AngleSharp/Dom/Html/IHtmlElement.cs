using System;
using AngleSharp.Attributes;
using AngleSharp.Dom.Css;
using AngleSharp.Dom.Events;

namespace AngleSharp.Dom.Html
{
	// Token: 0x020003BD RID: 957
	[DomName("HTMLElement")]
	public interface IHtmlElement : IElement, INode, IEventTarget, IMarkupFormattable, IParentNode, IChildNode, INonDocumentTypeChildNode, IElementCssInlineStyle, IGlobalEventHandlers
	{
		// Token: 0x170008F4 RID: 2292
		// (get) Token: 0x06001E17 RID: 7703
		// (set) Token: 0x06001E18 RID: 7704
		[DomName("lang")]
		string Language { get; set; }

		// Token: 0x170008F5 RID: 2293
		// (get) Token: 0x06001E19 RID: 7705
		// (set) Token: 0x06001E1A RID: 7706
		[DomName("title")]
		string Title { get; set; }

		// Token: 0x170008F6 RID: 2294
		// (get) Token: 0x06001E1B RID: 7707
		// (set) Token: 0x06001E1C RID: 7708
		[DomName("dir")]
		string Direction { get; set; }

		// Token: 0x170008F7 RID: 2295
		// (get) Token: 0x06001E1D RID: 7709
		[DomName("dataset")]
		IStringMap Dataset { get; }

		// Token: 0x170008F8 RID: 2296
		// (get) Token: 0x06001E1E RID: 7710
		// (set) Token: 0x06001E1F RID: 7711
		[DomName("translate")]
		bool IsTranslated { get; set; }

		// Token: 0x170008F9 RID: 2297
		// (get) Token: 0x06001E20 RID: 7712
		// (set) Token: 0x06001E21 RID: 7713
		[DomName("tabIndex")]
		int TabIndex { get; set; }

		// Token: 0x170008FA RID: 2298
		// (get) Token: 0x06001E22 RID: 7714
		// (set) Token: 0x06001E23 RID: 7715
		[DomName("spellcheck")]
		bool IsSpellChecked { get; set; }

		// Token: 0x170008FB RID: 2299
		// (get) Token: 0x06001E24 RID: 7716
		// (set) Token: 0x06001E25 RID: 7717
		[DomName("contentEditable")]
		string ContentEditable { get; set; }

		// Token: 0x170008FC RID: 2300
		// (get) Token: 0x06001E26 RID: 7718
		[DomName("isContentEditable")]
		bool IsContentEditable { get; }

		// Token: 0x170008FD RID: 2301
		// (get) Token: 0x06001E27 RID: 7719
		// (set) Token: 0x06001E28 RID: 7720
		[DomName("hidden")]
		bool IsHidden { get; set; }

		// Token: 0x170008FE RID: 2302
		// (get) Token: 0x06001E29 RID: 7721
		// (set) Token: 0x06001E2A RID: 7722
		[DomName("draggable")]
		bool IsDraggable { get; set; }

		// Token: 0x170008FF RID: 2303
		// (get) Token: 0x06001E2B RID: 7723
		// (set) Token: 0x06001E2C RID: 7724
		[DomName("accessKey")]
		string AccessKey { get; set; }

		// Token: 0x17000900 RID: 2304
		// (get) Token: 0x06001E2D RID: 7725
		[DomName("accessKeyLabel")]
		string AccessKeyLabel { get; }

		// Token: 0x17000901 RID: 2305
		// (get) Token: 0x06001E2E RID: 7726
		// (set) Token: 0x06001E2F RID: 7727
		[DomName("contextMenu")]
		IHtmlMenuElement ContextMenu { get; set; }

		// Token: 0x17000902 RID: 2306
		// (get) Token: 0x06001E30 RID: 7728
		[DomName("dropzone")]
		[DomPutForwards("value")]
		ISettableTokenList DropZone { get; }

		// Token: 0x17000903 RID: 2307
		// (get) Token: 0x06001E31 RID: 7729
		// (set) Token: 0x06001E32 RID: 7730
		[DomName("innerText")]
		string InnerText { get; set; }

		// Token: 0x06001E33 RID: 7731
		[DomName("click")]
		void DoClick();

		// Token: 0x06001E34 RID: 7732
		[DomName("focus")]
		void DoFocus();

		// Token: 0x06001E35 RID: 7733
		[DomName("blur")]
		void DoBlur();

		// Token: 0x06001E36 RID: 7734
		[DomName("forceSpellCheck")]
		void DoSpellCheck();
	}
}
