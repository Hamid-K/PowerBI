using System;
using System.Collections.Generic;
using System.Xml;

namespace Microsoft.ReportingServices.ProgressivePackaging
{
	// Token: 0x02000007 RID: 7
	internal class ImageRequestMessageReader : ImageMessageReader<ImageRequestMessageElement>
	{
		// Token: 0x06000013 RID: 19 RVA: 0x00002294 File Offset: 0x00000494
		public ImageRequestMessageReader(XmlReader xmlReader)
		{
			this.m_xmlReader = xmlReader;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000022A3 File Offset: 0x000004A3
		public override IEnumerator<ImageRequestMessageElement> GetEnumerator()
		{
			while (this.m_xmlReader.Read())
			{
				if (this.m_xmlReader.NodeType == XmlNodeType.Element && "ExternalImage".Equals(this.m_xmlReader.Name, StringComparison.Ordinal))
				{
					using (XmlReader externalImageReader = this.m_xmlReader.ReadSubtree())
					{
						ImageRequestMessageElement imageRequestMessageElement = new ImageRequestMessageElement();
						imageRequestMessageElement.Read(externalImageReader);
						yield return imageRequestMessageElement;
					}
					XmlReader externalImageReader = null;
				}
			}
			yield break;
			yield break;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000022B2 File Offset: 0x000004B2
		public override void InternalDispose()
		{
			this.m_xmlReader.Close();
		}

		// Token: 0x04000006 RID: 6
		private XmlReader m_xmlReader;
	}
}
