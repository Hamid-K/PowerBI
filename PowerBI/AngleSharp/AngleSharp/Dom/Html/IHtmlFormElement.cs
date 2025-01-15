using System;
using System.Threading.Tasks;
using AngleSharp.Attributes;
using AngleSharp.Dom.Css;
using AngleSharp.Dom.Events;
using AngleSharp.Network;

namespace AngleSharp.Dom.Html
{
	// Token: 0x020003C1 RID: 961
	[DomName("HTMLFormElement")]
	public interface IHtmlFormElement : IHtmlElement, IElement, INode, IEventTarget, IMarkupFormattable, IParentNode, IChildNode, INonDocumentTypeChildNode, IElementCssInlineStyle, IGlobalEventHandlers
	{
		// Token: 0x1700090D RID: 2317
		// (get) Token: 0x06001E46 RID: 7750
		// (set) Token: 0x06001E47 RID: 7751
		[DomName("acceptCharset")]
		string AcceptCharset { get; set; }

		// Token: 0x1700090E RID: 2318
		// (get) Token: 0x06001E48 RID: 7752
		// (set) Token: 0x06001E49 RID: 7753
		[DomName("action")]
		string Action { get; set; }

		// Token: 0x1700090F RID: 2319
		// (get) Token: 0x06001E4A RID: 7754
		// (set) Token: 0x06001E4B RID: 7755
		[DomName("autocomplete")]
		string Autocomplete { get; set; }

		// Token: 0x17000910 RID: 2320
		// (get) Token: 0x06001E4C RID: 7756
		// (set) Token: 0x06001E4D RID: 7757
		[DomName("enctype")]
		string Enctype { get; set; }

		// Token: 0x17000911 RID: 2321
		// (get) Token: 0x06001E4E RID: 7758
		// (set) Token: 0x06001E4F RID: 7759
		[DomName("encoding")]
		string Encoding { get; set; }

		// Token: 0x17000912 RID: 2322
		// (get) Token: 0x06001E50 RID: 7760
		// (set) Token: 0x06001E51 RID: 7761
		[DomName("method")]
		string Method { get; set; }

		// Token: 0x17000913 RID: 2323
		// (get) Token: 0x06001E52 RID: 7762
		// (set) Token: 0x06001E53 RID: 7763
		[DomName("name")]
		string Name { get; set; }

		// Token: 0x17000914 RID: 2324
		// (get) Token: 0x06001E54 RID: 7764
		// (set) Token: 0x06001E55 RID: 7765
		[DomName("noValidate")]
		bool NoValidate { get; set; }

		// Token: 0x17000915 RID: 2325
		// (get) Token: 0x06001E56 RID: 7766
		// (set) Token: 0x06001E57 RID: 7767
		[DomName("target")]
		string Target { get; set; }

		// Token: 0x17000916 RID: 2326
		// (get) Token: 0x06001E58 RID: 7768
		[DomName("length")]
		int Length { get; }

		// Token: 0x17000917 RID: 2327
		// (get) Token: 0x06001E59 RID: 7769
		[DomName("elements")]
		IHtmlFormControlsCollection Elements { get; }

		// Token: 0x06001E5A RID: 7770
		[DomName("submit")]
		Task<IDocument> SubmitAsync();

		// Token: 0x06001E5B RID: 7771
		Task<IDocument> SubmitAsync(IHtmlElement sourceElement);

		// Token: 0x06001E5C RID: 7772
		DocumentRequest GetSubmission();

		// Token: 0x06001E5D RID: 7773
		DocumentRequest GetSubmission(IHtmlElement sourceElement);

		// Token: 0x06001E5E RID: 7774
		[DomName("reset")]
		void Reset();

		// Token: 0x06001E5F RID: 7775
		[DomName("checkValidity")]
		bool CheckValidity();

		// Token: 0x06001E60 RID: 7776
		[DomName("reportValidity")]
		bool ReportValidity();

		// Token: 0x17000918 RID: 2328
		[DomAccessor(Accessors.Getter)]
		IElement this[int index] { get; }

		// Token: 0x17000919 RID: 2329
		[DomAccessor(Accessors.Getter)]
		IElement this[string name] { get; }

		// Token: 0x06001E63 RID: 7779
		[DomName("requestAutocomplete")]
		void RequestAutocomplete();
	}
}
