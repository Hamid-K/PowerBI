using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x02000016 RID: 22
	[Serializable]
	public class ConnectionCollection : List<Connection>, IXmlSerializable
	{
		// Token: 0x0600009E RID: 158 RVA: 0x00003F59 File Offset: 0x00002159
		public ConnectionCollection()
		{
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00003F61 File Offset: 0x00002161
		public ConnectionCollection(XmlReader reader)
		{
			this.ReadXml(reader);
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00003F70 File Offset: 0x00002170
		public bool ContainsConnection(string connectionName)
		{
			foreach (Connection connection in this)
			{
				if (connectionName.Equals(connection.Name))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00003FCC File Offset: 0x000021CC
		public Connection GetConnection(string connectionName)
		{
			foreach (Connection connection in this)
			{
				if (connectionName.Equals(connection.Name))
				{
					return connection;
				}
			}
			if (connectionName.Equals(ConnectionManager.ContextConnectionName))
			{
				return Connection.ContextConnection;
			}
			throw new Exception(string.Format("Connection named '{0}' was not found.", connectionName));
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x0000404C File Offset: 0x0000224C
		XmlSchema IXmlSerializable.GetSchema()
		{
			return null;
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00004050 File Offset: 0x00002250
		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			reader = FuzzyLookupXmlBuilder.CreateValidatingXmlReader(reader);
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.Load(reader);
			XmlNamespaceManager xmlNamespaceManager = xmlDocument.CreateFL3XmlNamespaceManager();
			XmlNode xmlNode = xmlDocument.SelectSingleNode("/ns:Connections", xmlNamespaceManager);
			if (xmlNode != null)
			{
				foreach (object obj in xmlNode.SelectNodes("//ns:Connection", xmlNamespaceManager))
				{
					XmlNode xmlNode2 = (XmlNode)obj;
					Connection connection = new Connection();
					connection.ReadXml(new XmlNodeReader(xmlNode2));
					base.Add(connection);
				}
			}
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x000040F4 File Offset: 0x000022F4
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			writer.WriteStartElement("Connections");
			foreach (Connection connection in this)
			{
				connection.WriteXml(writer);
			}
			writer.WriteEndElement();
		}
	}
}
