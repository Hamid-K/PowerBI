using System;
using System.Xml;

namespace Microsoft.OData.Edm.Csdl.Parsing.Common
{
	// Token: 0x02000192 RID: 402
	internal abstract class XmlDocumentParser<TResult> : XmlDocumentParser
	{
		// Token: 0x06000797 RID: 1943 RVA: 0x000125D8 File Offset: 0x000107D8
		internal XmlDocumentParser(XmlReader underlyingReader, string documentPath)
			: base(underlyingReader, documentPath)
		{
		}

		// Token: 0x1700032E RID: 814
		// (get) Token: 0x06000798 RID: 1944 RVA: 0x000125E2 File Offset: 0x000107E2
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

		// Token: 0x06000799 RID: 1945 RVA: 0x000125FC File Offset: 0x000107FC
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

		// Token: 0x0600079A RID: 1946
		protected abstract bool TryGetDocumentElementParser(Version artifactVersion, XmlElementInfo rootElement, out XmlElementParser<TResult> parser);
	}
}
