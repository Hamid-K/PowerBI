using System;
using System.Xml;

namespace Microsoft.OData.Edm.Csdl.Parsing.Common
{
	// Token: 0x020001AF RID: 431
	internal abstract class XmlDocumentParser<TResult> : XmlDocumentParser
	{
		// Token: 0x06000C1A RID: 3098 RVA: 0x00023268 File Offset: 0x00021468
		internal XmlDocumentParser(XmlReader underlyingReader, string documentPath)
			: base(underlyingReader, documentPath)
		{
		}

		// Token: 0x170003D7 RID: 983
		// (get) Token: 0x06000C1B RID: 3099 RVA: 0x00023272 File Offset: 0x00021472
		internal new XmlElementValue<TResult> Result
		{
			get
			{
				if (base.Result != null)
				{
					return (XmlElementValue<TResult>)base.Result;
				}
				return null;
			}
		}

		// Token: 0x06000C1C RID: 3100 RVA: 0x0002328C File Offset: 0x0002148C
		protected sealed override bool TryGetRootElementParser(Version artifactVersion, XmlElementInfo rootElement, out XmlElementParser parser)
		{
			XmlElementParser<TResult> xmlElementParser;
			if (this.TryGetDocumentElementParser(artifactVersion, rootElement, out xmlElementParser))
			{
				parser = xmlElementParser;
				return true;
			}
			parser = null;
			return false;
		}

		// Token: 0x06000C1D RID: 3101
		protected abstract bool TryGetDocumentElementParser(Version artifactVersion, XmlElementInfo rootElement, out XmlElementParser<TResult> parser);
	}
}
