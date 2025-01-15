using System;
using System.Xml;

namespace Microsoft.Data.Edm.Csdl.Internal.Parsing.Common
{
	// Token: 0x02000151 RID: 337
	internal abstract class XmlDocumentParser<TResult> : XmlDocumentParser
	{
		// Token: 0x0600066D RID: 1645 RVA: 0x00010334 File Offset: 0x0000E534
		internal XmlDocumentParser(XmlReader underlyingReader, string documentPath)
			: base(underlyingReader, documentPath)
		{
		}

		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x0600066E RID: 1646 RVA: 0x0001033E File Offset: 0x0000E53E
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

		// Token: 0x0600066F RID: 1647 RVA: 0x00010358 File Offset: 0x0000E558
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

		// Token: 0x06000670 RID: 1648
		protected abstract bool TryGetDocumentElementParser(Version artifactVersion, XmlElementInfo rootElement, out XmlElementParser<TResult> parser);
	}
}
