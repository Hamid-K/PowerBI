using System;
using System.Xml;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x02000091 RID: 145
	internal class XmlDataFeed : DataFeed
	{
		// Token: 0x06000BAC RID: 2988 RVA: 0x000222C1 File Offset: 0x000204C1
		internal XmlDataFeed(XmlReader source)
		{
			this._source = source;
		}

		// Token: 0x04000302 RID: 770
		internal XmlReader _source;
	}
}
