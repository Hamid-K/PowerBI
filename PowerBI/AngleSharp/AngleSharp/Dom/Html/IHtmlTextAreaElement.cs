using System;
using AngleSharp.Attributes;
using AngleSharp.Dom.Css;
using AngleSharp.Dom.Events;

namespace AngleSharp.Dom.Html
{
	// Token: 0x020003EF RID: 1007
	[DomName("HTMLTextAreaElement")]
	public interface IHtmlTextAreaElement : IHtmlElement, IElement, INode, IEventTarget, IMarkupFormattable, IParentNode, IChildNode, INonDocumentTypeChildNode, IElementCssInlineStyle, IGlobalEventHandlers, IValidation
	{
		// Token: 0x170009F3 RID: 2547
		// (get) Token: 0x06001FF7 RID: 8183
		// (set) Token: 0x06001FF8 RID: 8184
		[DomName("autofocus")]
		bool Autofocus { get; set; }

		// Token: 0x170009F4 RID: 2548
		// (get) Token: 0x06001FF9 RID: 8185
		// (set) Token: 0x06001FFA RID: 8186
		[DomName("disabled")]
		bool IsDisabled { get; set; }

		// Token: 0x170009F5 RID: 2549
		// (get) Token: 0x06001FFB RID: 8187
		[DomName("form")]
		IHtmlFormElement Form { get; }

		// Token: 0x170009F6 RID: 2550
		// (get) Token: 0x06001FFC RID: 8188
		[DomName("labels")]
		INodeList Labels { get; }

		// Token: 0x170009F7 RID: 2551
		// (get) Token: 0x06001FFD RID: 8189
		// (set) Token: 0x06001FFE RID: 8190
		[DomName("name")]
		string Name { get; set; }

		// Token: 0x170009F8 RID: 2552
		// (get) Token: 0x06001FFF RID: 8191
		[DomName("type")]
		string Type { get; }

		// Token: 0x170009F9 RID: 2553
		// (get) Token: 0x06002000 RID: 8192
		// (set) Token: 0x06002001 RID: 8193
		[DomName("required")]
		bool IsRequired { get; set; }

		// Token: 0x170009FA RID: 2554
		// (get) Token: 0x06002002 RID: 8194
		// (set) Token: 0x06002003 RID: 8195
		[DomName("readOnly")]
		bool IsReadOnly { get; set; }

		// Token: 0x170009FB RID: 2555
		// (get) Token: 0x06002004 RID: 8196
		// (set) Token: 0x06002005 RID: 8197
		[DomName("defaultValue")]
		string DefaultValue { get; set; }

		// Token: 0x170009FC RID: 2556
		// (get) Token: 0x06002006 RID: 8198
		// (set) Token: 0x06002007 RID: 8199
		[DomName("value")]
		string Value { get; set; }

		// Token: 0x170009FD RID: 2557
		// (get) Token: 0x06002008 RID: 8200
		// (set) Token: 0x06002009 RID: 8201
		[DomName("wrap")]
		string Wrap { get; set; }

		// Token: 0x170009FE RID: 2558
		// (get) Token: 0x0600200A RID: 8202
		[DomName("textLength")]
		int TextLength { get; }

		// Token: 0x170009FF RID: 2559
		// (get) Token: 0x0600200B RID: 8203
		// (set) Token: 0x0600200C RID: 8204
		[DomName("rows")]
		int Rows { get; set; }

		// Token: 0x17000A00 RID: 2560
		// (get) Token: 0x0600200D RID: 8205
		// (set) Token: 0x0600200E RID: 8206
		[DomName("cols")]
		int Columns { get; set; }

		// Token: 0x17000A01 RID: 2561
		// (get) Token: 0x0600200F RID: 8207
		// (set) Token: 0x06002010 RID: 8208
		[DomName("maxLength")]
		int MaxLength { get; set; }

		// Token: 0x17000A02 RID: 2562
		// (get) Token: 0x06002011 RID: 8209
		// (set) Token: 0x06002012 RID: 8210
		[DomName("placeholder")]
		string Placeholder { get; set; }

		// Token: 0x17000A03 RID: 2563
		// (get) Token: 0x06002013 RID: 8211
		[DomName("selectionDirection")]
		string SelectionDirection { get; }

		// Token: 0x17000A04 RID: 2564
		// (get) Token: 0x06002014 RID: 8212
		// (set) Token: 0x06002015 RID: 8213
		[DomName("dirName")]
		string DirectionName { get; set; }

		// Token: 0x17000A05 RID: 2565
		// (get) Token: 0x06002016 RID: 8214
		// (set) Token: 0x06002017 RID: 8215
		[DomName("selectionStart")]
		int SelectionStart { get; set; }

		// Token: 0x17000A06 RID: 2566
		// (get) Token: 0x06002018 RID: 8216
		// (set) Token: 0x06002019 RID: 8217
		[DomName("selectionEnd")]
		int SelectionEnd { get; set; }

		// Token: 0x0600201A RID: 8218
		[DomName("select")]
		void SelectAll();

		// Token: 0x0600201B RID: 8219
		[DomName("setSelectionRange")]
		void Select(int selectionStart, int selectionEnd, string selectionDirection = null);
	}
}
