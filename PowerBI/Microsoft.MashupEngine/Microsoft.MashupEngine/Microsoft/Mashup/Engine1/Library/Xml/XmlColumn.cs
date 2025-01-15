using System;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Xml
{
	// Token: 0x02000283 RID: 643
	internal abstract class XmlColumn
	{
		// Token: 0x06001A6D RID: 6765 RVA: 0x000351D3 File Offset: 0x000333D3
		protected XmlColumn(string localName)
		{
			this.columnName = localName;
		}

		// Token: 0x17000CE9 RID: 3305
		// (get) Token: 0x06001A6E RID: 6766 RVA: 0x000351E2 File Offset: 0x000333E2
		public string ColumnName
		{
			get
			{
				return this.columnName;
			}
		}

		// Token: 0x06001A6F RID: 6767
		public abstract Value Reduce(XmlTableOptions options);

		// Token: 0x040007D5 RID: 2005
		private string columnName;
	}
}
