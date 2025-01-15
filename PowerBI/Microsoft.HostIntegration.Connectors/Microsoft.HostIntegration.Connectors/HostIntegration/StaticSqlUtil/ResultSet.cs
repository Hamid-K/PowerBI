using System;
using System.Collections.Generic;
using System.Xml;

namespace Microsoft.HostIntegration.StaticSqlUtil
{
	// Token: 0x02000A7A RID: 2682
	public class ResultSet
	{
		// Token: 0x1700143A RID: 5178
		// (get) Token: 0x06005353 RID: 21331 RVA: 0x00152FCD File Offset: 0x001511CD
		public List<Column> Columns
		{
			get
			{
				return this._columns;
			}
		}

		// Token: 0x06005354 RID: 21332 RVA: 0x00152FD8 File Offset: 0x001511D8
		internal void SaveToXml(XmlWriter writer)
		{
			writer.WriteStartElement("resultSet");
			writer.WriteStartElement("columns");
			foreach (Column column in this._columns)
			{
				column.SaveToXml(writer);
			}
			writer.WriteEndElement();
			writer.WriteEndElement();
		}

		// Token: 0x06005355 RID: 21333 RVA: 0x0015304C File Offset: 0x0015124C
		internal void LoadFromXml(XmlElement resultsetElement, XmlNamespaceManager nsmgr)
		{
			foreach (object obj in resultsetElement.SelectNodes("descendant::drdastaticsql:columns/drdastaticsql:column", nsmgr))
			{
				XmlElement xmlElement = (XmlElement)obj;
				Column column = new Column();
				column.LoadFromXml(xmlElement, nsmgr);
				this._columns.Add(column);
			}
		}

		// Token: 0x06005356 RID: 21334 RVA: 0x001530C0 File Offset: 0x001512C0
		internal void LoadFromXmlV8(XmlElement resultsetElement)
		{
			foreach (object obj in resultsetElement.SelectNodes("Column"))
			{
				XmlElement xmlElement = (XmlElement)obj;
				Column column = new Column();
				column.LoadFromXmlV8(xmlElement);
				this._columns.Add(column);
			}
		}

		// Token: 0x06005357 RID: 21335 RVA: 0x00153130 File Offset: 0x00151330
		internal void SaveToXmlV8(XmlWriter writer)
		{
			writer.WriteStartElement("ResultSet");
			foreach (Column column in this._columns)
			{
				column.SaveToXmlV8(writer);
			}
			writer.WriteEndElement();
		}

		// Token: 0x0400425E RID: 16990
		private List<Column> _columns = new List<Column>();
	}
}
