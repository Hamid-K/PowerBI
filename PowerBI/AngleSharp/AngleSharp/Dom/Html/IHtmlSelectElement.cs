using System;
using AngleSharp.Attributes;
using AngleSharp.Dom.Css;
using AngleSharp.Dom.Events;

namespace AngleSharp.Dom.Html
{
	// Token: 0x020003E1 RID: 993
	[DomName("HTMLSelectElement")]
	public interface IHtmlSelectElement : IHtmlElement, IElement, INode, IEventTarget, IMarkupFormattable, IParentNode, IChildNode, INonDocumentTypeChildNode, IElementCssInlineStyle, IGlobalEventHandlers, IValidation
	{
		// Token: 0x170009C9 RID: 2505
		// (get) Token: 0x06001FA2 RID: 8098
		// (set) Token: 0x06001FA3 RID: 8099
		[DomName("autofocus")]
		bool Autofocus { get; set; }

		// Token: 0x170009CA RID: 2506
		// (get) Token: 0x06001FA4 RID: 8100
		// (set) Token: 0x06001FA5 RID: 8101
		[DomName("disabled")]
		bool IsDisabled { get; set; }

		// Token: 0x170009CB RID: 2507
		// (get) Token: 0x06001FA6 RID: 8102
		[DomName("form")]
		IHtmlFormElement Form { get; }

		// Token: 0x170009CC RID: 2508
		// (get) Token: 0x06001FA7 RID: 8103
		[DomName("labels")]
		INodeList Labels { get; }

		// Token: 0x170009CD RID: 2509
		// (get) Token: 0x06001FA8 RID: 8104
		// (set) Token: 0x06001FA9 RID: 8105
		[DomName("name")]
		string Name { get; set; }

		// Token: 0x170009CE RID: 2510
		// (get) Token: 0x06001FAA RID: 8106
		// (set) Token: 0x06001FAB RID: 8107
		[DomName("value")]
		string Value { get; set; }

		// Token: 0x170009CF RID: 2511
		// (get) Token: 0x06001FAC RID: 8108
		[DomName("type")]
		string Type { get; }

		// Token: 0x170009D0 RID: 2512
		// (get) Token: 0x06001FAD RID: 8109
		// (set) Token: 0x06001FAE RID: 8110
		[DomName("required")]
		bool IsRequired { get; set; }

		// Token: 0x170009D1 RID: 2513
		// (get) Token: 0x06001FAF RID: 8111
		[DomName("selectedOptions")]
		IHtmlCollection<IHtmlOptionElement> SelectedOptions { get; }

		// Token: 0x170009D2 RID: 2514
		// (get) Token: 0x06001FB0 RID: 8112
		// (set) Token: 0x06001FB1 RID: 8113
		[DomName("size")]
		int Size { get; set; }

		// Token: 0x170009D3 RID: 2515
		// (get) Token: 0x06001FB2 RID: 8114
		[DomName("options")]
		IHtmlOptionsCollection Options { get; }

		// Token: 0x170009D4 RID: 2516
		// (get) Token: 0x06001FB3 RID: 8115
		[DomName("length")]
		int Length { get; }

		// Token: 0x170009D5 RID: 2517
		// (get) Token: 0x06001FB4 RID: 8116
		// (set) Token: 0x06001FB5 RID: 8117
		[DomName("multiple")]
		bool IsMultiple { get; set; }

		// Token: 0x170009D6 RID: 2518
		// (get) Token: 0x06001FB6 RID: 8118
		[DomName("selectedIndex")]
		int SelectedIndex { get; }

		// Token: 0x170009D7 RID: 2519
		[DomAccessor(Accessors.Getter | Accessors.Setter)]
		IHtmlOptionElement this[int index] { get; set; }

		// Token: 0x06001FB9 RID: 8121
		[DomName("add")]
		void AddOption(IHtmlOptionElement element, IHtmlElement before = null);

		// Token: 0x06001FBA RID: 8122
		[DomName("add")]
		void AddOption(IHtmlOptionsGroupElement element, IHtmlElement before = null);

		// Token: 0x06001FBB RID: 8123
		[DomName("remove")]
		void RemoveOptionAt(int index);
	}
}
