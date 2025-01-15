using System;
using AngleSharp.Attributes;
using AngleSharp.Dom.Css;
using AngleSharp.Dom.Events;

namespace AngleSharp.Dom.Html
{
	// Token: 0x020003C9 RID: 969
	[DomName("HTMLKeygenElement")]
	public interface IHtmlKeygenElement : IHtmlElement, IElement, INode, IEventTarget, IMarkupFormattable, IParentNode, IChildNode, INonDocumentTypeChildNode, IElementCssInlineStyle, IGlobalEventHandlers, IValidation
	{
		// Token: 0x1700095B RID: 2395
		// (get) Token: 0x06001EDD RID: 7901
		// (set) Token: 0x06001EDE RID: 7902
		[DomName("autofocus")]
		bool Autofocus { get; set; }

		// Token: 0x1700095C RID: 2396
		// (get) Token: 0x06001EDF RID: 7903
		[DomName("labels")]
		INodeList Labels { get; }

		// Token: 0x1700095D RID: 2397
		// (get) Token: 0x06001EE0 RID: 7904
		// (set) Token: 0x06001EE1 RID: 7905
		[DomName("disabled")]
		bool IsDisabled { get; set; }

		// Token: 0x1700095E RID: 2398
		// (get) Token: 0x06001EE2 RID: 7906
		[DomName("form")]
		IHtmlFormElement Form { get; }

		// Token: 0x1700095F RID: 2399
		// (get) Token: 0x06001EE3 RID: 7907
		// (set) Token: 0x06001EE4 RID: 7908
		[DomName("name")]
		string Name { get; set; }

		// Token: 0x17000960 RID: 2400
		// (get) Token: 0x06001EE5 RID: 7909
		[DomName("type")]
		string Type { get; }

		// Token: 0x17000961 RID: 2401
		// (get) Token: 0x06001EE6 RID: 7910
		// (set) Token: 0x06001EE7 RID: 7911
		[DomName("keytype")]
		string KeyEncryption { get; set; }

		// Token: 0x17000962 RID: 2402
		// (get) Token: 0x06001EE8 RID: 7912
		// (set) Token: 0x06001EE9 RID: 7913
		[DomName("challenge")]
		string Challenge { get; set; }
	}
}
