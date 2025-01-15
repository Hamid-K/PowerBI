using System;
using System.Collections.Generic;
using System.Xml;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Xml
{
	// Token: 0x02000286 RID: 646
	internal class XmlElementsListValue : StreamedListValue
	{
		// Token: 0x06001A84 RID: 6788 RVA: 0x000356EB File Offset: 0x000338EB
		public XmlElementsListValue(XmlNodeList nodes)
		{
			this.nodes = nodes;
		}

		// Token: 0x06001A85 RID: 6789 RVA: 0x000356FA File Offset: 0x000338FA
		public override IEnumerator<IValueReference> GetEnumerator()
		{
			return new XmlElementsEnumerator(this.nodes);
		}

		// Token: 0x040007DF RID: 2015
		private readonly XmlNodeList nodes;
	}
}
