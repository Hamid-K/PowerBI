using System;
using System.IO;
using AngleSharp.Attributes;
using AngleSharp.Dom.Css;
using AngleSharp.Dom.Events;
using AngleSharp.Dom.Media;

namespace AngleSharp.Dom.Html
{
	// Token: 0x020003B5 RID: 949
	[DomName("HTMLCanvasElement")]
	public interface IHtmlCanvasElement : IHtmlElement, IElement, INode, IEventTarget, IMarkupFormattable, IParentNode, IChildNode, INonDocumentTypeChildNode, IElementCssInlineStyle, IGlobalEventHandlers
	{
		// Token: 0x170008E6 RID: 2278
		// (get) Token: 0x06001DF5 RID: 7669
		// (set) Token: 0x06001DF6 RID: 7670
		[DomName("width")]
		int Width { get; set; }

		// Token: 0x170008E7 RID: 2279
		// (get) Token: 0x06001DF7 RID: 7671
		// (set) Token: 0x06001DF8 RID: 7672
		[DomName("height")]
		int Height { get; set; }

		// Token: 0x06001DF9 RID: 7673
		[DomName("toDataURL")]
		string ToDataUrl(string type = null);

		// Token: 0x06001DFA RID: 7674
		[DomName("toBlob")]
		void ToBlob(Action<Stream> callback, string type = null);

		// Token: 0x06001DFB RID: 7675
		[DomName("getContext")]
		IRenderingContext GetContext(string contextId);

		// Token: 0x06001DFC RID: 7676
		[DomName("setContext")]
		void SetContext(IRenderingContext context);

		// Token: 0x06001DFD RID: 7677
		[DomName("probablySupportsContext")]
		bool IsSupportingContext(string contextId);
	}
}
