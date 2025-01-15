using System;
using AngleSharp.Attributes;
using AngleSharp.Dom.Xml;

namespace AngleSharp.Dom
{
	// Token: 0x0200018A RID: 394
	[DomName("DOMImplementation")]
	public interface IImplementation
	{
		// Token: 0x06000E42 RID: 3650
		[DomName("createDocument")]
		IXmlDocument CreateDocument(string namespaceUri, string qualifiedName, IDocumentType doctype);

		// Token: 0x06000E43 RID: 3651
		[DomName("createHTMLDocument")]
		IDocument CreateHtmlDocument(string title);

		// Token: 0x06000E44 RID: 3652
		[DomName("createDocumentType")]
		IDocumentType CreateDocumentType(string qualifiedName, string publicId, string systemId);

		// Token: 0x06000E45 RID: 3653
		[DomName("hasFeature")]
		bool HasFeature(string feature, string version = null);
	}
}
