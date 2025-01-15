using System;
using Microsoft.Exchange.WebServices.Data;

namespace Microsoft.Exchange.WebServices.Autodiscover
{
	// Token: 0x02000014 RID: 20
	public sealed class ProtocolConnection
	{
		// Token: 0x060000D1 RID: 209 RVA: 0x00005542 File Offset: 0x00004542
		internal ProtocolConnection()
		{
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x0000554C File Offset: 0x0000454C
		internal static ProtocolConnection LoadFromXml(EwsXmlReader reader)
		{
			ProtocolConnection protocolConnection = new ProtocolConnection();
			do
			{
				reader.Read();
				string localName;
				if (reader.NodeType == 1 && (localName = reader.LocalName) != null)
				{
					if (!(localName == "EncryptionMethod"))
					{
						if (!(localName == "Hostname"))
						{
							if (localName == "Port")
							{
								protocolConnection.Port = reader.ReadElementValue<int>();
							}
						}
						else
						{
							protocolConnection.Hostname = reader.ReadElementValue<string>();
						}
					}
					else
					{
						protocolConnection.EncryptionMethod = reader.ReadElementValue<string>();
					}
				}
			}
			while (!reader.IsEndElement(XmlNamespace.Autodiscover, "ProtocolConnection"));
			return protocolConnection;
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x000055D9 File Offset: 0x000045D9
		internal ProtocolConnection(string encryptionMethod, string hostname, int port)
		{
			this.encryptionMethod = encryptionMethod;
			this.hostname = hostname;
			this.port = port;
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000D4 RID: 212 RVA: 0x000055F6 File Offset: 0x000045F6
		// (set) Token: 0x060000D5 RID: 213 RVA: 0x000055FE File Offset: 0x000045FE
		public string EncryptionMethod
		{
			get
			{
				return this.encryptionMethod;
			}
			set
			{
				this.encryptionMethod = value;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000D6 RID: 214 RVA: 0x00005607 File Offset: 0x00004607
		// (set) Token: 0x060000D7 RID: 215 RVA: 0x0000560F File Offset: 0x0000460F
		public string Hostname
		{
			get
			{
				return this.hostname;
			}
			set
			{
				this.hostname = value;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000D8 RID: 216 RVA: 0x00005618 File Offset: 0x00004618
		// (set) Token: 0x060000D9 RID: 217 RVA: 0x00005620 File Offset: 0x00004620
		public int Port
		{
			get
			{
				return this.port;
			}
			set
			{
				this.port = value;
			}
		}

		// Token: 0x04000058 RID: 88
		private string encryptionMethod;

		// Token: 0x04000059 RID: 89
		private string hostname;

		// Token: 0x0400005A RID: 90
		private int port;
	}
}
