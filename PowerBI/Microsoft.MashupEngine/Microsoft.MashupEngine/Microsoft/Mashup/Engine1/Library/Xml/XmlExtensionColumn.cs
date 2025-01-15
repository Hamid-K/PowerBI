using System;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Xml
{
	// Token: 0x02000287 RID: 647
	internal class XmlExtensionColumn : XmlColumn
	{
		// Token: 0x06001A86 RID: 6790 RVA: 0x00035707 File Offset: 0x00033907
		public XmlExtensionColumn(string name)
			: base(name)
		{
			this.columnMap = new XmlColumnMap();
		}

		// Token: 0x17000CEC RID: 3308
		// (get) Token: 0x06001A87 RID: 6791 RVA: 0x0003571B File Offset: 0x0003391B
		public XmlColumnMap ColumnMap
		{
			get
			{
				return this.columnMap;
			}
		}

		// Token: 0x06001A88 RID: 6792 RVA: 0x00035723 File Offset: 0x00033923
		public override Value Reduce(XmlTableOptions options)
		{
			return XmlTableValue.FromRow(this.columnMap.ToRow(options));
		}

		// Token: 0x040007E0 RID: 2016
		private XmlColumnMap columnMap;
	}
}
