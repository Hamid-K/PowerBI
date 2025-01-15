using System;
using System.Xml;

namespace Microsoft.OData.Edm.Csdl.Parsing.Common
{
	// Token: 0x020001BC RID: 444
	internal abstract class XmlDocumentParser<TResult> : XmlDocumentParser
	{
		// Token: 0x06000CCC RID: 3276 RVA: 0x00025431 File Offset: 0x00023631
		internal XmlDocumentParser(XmlReader underlyingReader, string documentPath)
			: base(underlyingReader, documentPath)
		{
		}

		// Token: 0x17000421 RID: 1057
		// (get) Token: 0x06000CCD RID: 3277 RVA: 0x0002543B File Offset: 0x0002363B
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

		// Token: 0x06000CCE RID: 3278 RVA: 0x00025454 File Offset: 0x00023654
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

		// Token: 0x06000CCF RID: 3279
		protected abstract bool TryGetDocumentElementParser(Version artifactVersion, XmlElementInfo rootElement, out XmlElementParser<TResult> parser);
	}
}
