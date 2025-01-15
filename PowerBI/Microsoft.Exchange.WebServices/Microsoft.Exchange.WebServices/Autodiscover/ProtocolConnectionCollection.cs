using System;
using System.Collections.Generic;
using Microsoft.Exchange.WebServices.Data;

namespace Microsoft.Exchange.WebServices.Autodiscover
{
	// Token: 0x02000015 RID: 21
	public sealed class ProtocolConnectionCollection
	{
		// Token: 0x060000DA RID: 218 RVA: 0x00005629 File Offset: 0x00004629
		internal ProtocolConnectionCollection()
		{
			this.connections = new List<ProtocolConnection>();
		}

		// Token: 0x060000DB RID: 219 RVA: 0x0000563C File Offset: 0x0000463C
		internal static ProtocolConnectionCollection LoadFromXml(EwsXmlReader reader)
		{
			ProtocolConnectionCollection protocolConnectionCollection = new ProtocolConnectionCollection();
			do
			{
				reader.Read();
				if (reader.NodeType == 1 && reader.LocalName == "ProtocolConnection")
				{
					ProtocolConnection protocolConnection = ProtocolConnection.LoadFromXml(reader);
					if (protocolConnection != null)
					{
						protocolConnectionCollection.Connections.Add(protocolConnection);
					}
				}
			}
			while (!reader.IsEndElement(XmlNamespace.Autodiscover, "ProtocolConnections"));
			return protocolConnectionCollection;
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000DC RID: 220 RVA: 0x00005698 File Offset: 0x00004698
		// (set) Token: 0x060000DD RID: 221 RVA: 0x000056A0 File Offset: 0x000046A0
		public List<ProtocolConnection> Connections
		{
			get
			{
				return this.connections;
			}
			internal set
			{
				this.connections = value;
			}
		}

		// Token: 0x0400005B RID: 91
		private List<ProtocolConnection> connections;
	}
}
