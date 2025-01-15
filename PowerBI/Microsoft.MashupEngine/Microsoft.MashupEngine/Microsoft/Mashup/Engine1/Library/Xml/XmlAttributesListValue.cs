using System;
using System.Collections.Generic;
using System.Xml;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Xml
{
	// Token: 0x02000282 RID: 642
	internal class XmlAttributesListValue : StreamedListValue
	{
		// Token: 0x06001A6B RID: 6763 RVA: 0x000351B7 File Offset: 0x000333B7
		public XmlAttributesListValue(XmlAttributeCollection attributes)
		{
			this.attributes = attributes;
		}

		// Token: 0x06001A6C RID: 6764 RVA: 0x000351C6 File Offset: 0x000333C6
		public override IEnumerator<IValueReference> GetEnumerator()
		{
			return new XmlAttributesEnumerator(this.attributes);
		}

		// Token: 0x040007D4 RID: 2004
		private readonly XmlAttributeCollection attributes;
	}
}
