using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Xml
{
	// Token: 0x02000284 RID: 644
	internal class XmlColumnMap
	{
		// Token: 0x06001A70 RID: 6768 RVA: 0x000351EA File Offset: 0x000333EA
		public XmlColumnMap()
		{
			this.builder = default(KeysBuilder);
			this.columns = new List<XmlColumn>();
		}

		// Token: 0x06001A71 RID: 6769 RVA: 0x0003520C File Offset: 0x0003340C
		public RecordValue ToRow(XmlTableOptions options)
		{
			if (this.columns.Count == 0)
			{
				return RecordValue.Empty;
			}
			Value[] array = new Value[this.columns.Count];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = this.columns[i].Reduce(options);
			}
			return RecordValue.New(this.builder.ToKeys(), array);
		}

		// Token: 0x06001A72 RID: 6770 RVA: 0x00035271 File Offset: 0x00033471
		public void AddElement(string localName, string ns, RecordValue row, string primaryNs)
		{
			if (ns == primaryNs)
			{
				this.AddPrimaryElement(localName, row);
				return;
			}
			this.AddExtensionElement(localName, ns, row);
		}

		// Token: 0x06001A73 RID: 6771 RVA: 0x00035290 File Offset: 0x00033490
		private void AddPrimaryElement(string localName, RecordValue row)
		{
			XmlTableColumn xmlTableColumn = (XmlTableColumn)this.GetColumn(localName);
			if (xmlTableColumn == null)
			{
				xmlTableColumn = new XmlTableColumn(localName);
				this.AddColumn(localName, xmlTableColumn);
			}
			xmlTableColumn.AddRow(row);
		}

		// Token: 0x06001A74 RID: 6772 RVA: 0x000352C4 File Offset: 0x000334C4
		private void AddExtensionElement(string localName, string ns, RecordValue row)
		{
			string text = this.NamespaceColumn(ns);
			XmlExtensionColumn xmlExtensionColumn = (XmlExtensionColumn)this.GetColumn(text);
			if (xmlExtensionColumn == null)
			{
				xmlExtensionColumn = new XmlExtensionColumn(text);
				this.AddColumn(xmlExtensionColumn.ColumnName, xmlExtensionColumn);
			}
			xmlExtensionColumn.ColumnMap.AddPrimaryElement(localName, row);
		}

		// Token: 0x06001A75 RID: 6773 RVA: 0x0003530C File Offset: 0x0003350C
		public void AddText(string text)
		{
			XmlTextColumn xmlTextColumn = new XmlTextColumn("Element:Text", text);
			this.AddColumn(xmlTextColumn.ColumnName, xmlTextColumn);
		}

		// Token: 0x06001A76 RID: 6774 RVA: 0x00035332 File Offset: 0x00033532
		public void AddAttribute(string localName, string ns, string value)
		{
			if (ns.Length == 0)
			{
				this.AddPrimaryAttribute(localName, value);
				return;
			}
			this.AddExtensionAttribute(localName, ns, value);
		}

		// Token: 0x06001A77 RID: 6775 RVA: 0x00035350 File Offset: 0x00033550
		private void AddPrimaryAttribute(string localName, string value)
		{
			XmlTextColumn xmlTextColumn = new XmlTextColumn("Attribute:" + localName, value);
			this.AddColumn(xmlTextColumn.ColumnName, xmlTextColumn);
		}

		// Token: 0x06001A78 RID: 6776 RVA: 0x0003537C File Offset: 0x0003357C
		private void AddExtensionAttribute(string localName, string ns, string value)
		{
			string text = this.NamespaceColumn(ns);
			XmlExtensionColumn xmlExtensionColumn = (XmlExtensionColumn)this.GetColumn(text);
			if (xmlExtensionColumn == null)
			{
				xmlExtensionColumn = new XmlExtensionColumn(text);
				this.AddColumn(xmlExtensionColumn.ColumnName, xmlExtensionColumn);
			}
			xmlExtensionColumn.ColumnMap.AddPrimaryAttribute(localName, value);
		}

		// Token: 0x06001A79 RID: 6777 RVA: 0x000353C2 File Offset: 0x000335C2
		private string NamespaceColumn(string ns)
		{
			if (ns.Contains(":") && !ns.StartsWith("Attribute:", StringComparison.Ordinal) && !ns.StartsWith("Namespace:", StringComparison.Ordinal))
			{
				return ns;
			}
			return "Namespace:" + ns;
		}

		// Token: 0x06001A7A RID: 6778 RVA: 0x000353FA File Offset: 0x000335FA
		private void AddColumn(string name, XmlColumn column)
		{
			this.builder.Add(name);
			this.columns.Add(column);
		}

		// Token: 0x06001A7B RID: 6779 RVA: 0x00035414 File Offset: 0x00033614
		private XmlColumn GetColumn(string name)
		{
			int num = this.builder.IndexOf(name);
			if (num == -1)
			{
				return null;
			}
			return this.columns[num];
		}

		// Token: 0x040007D6 RID: 2006
		private KeysBuilder builder;

		// Token: 0x040007D7 RID: 2007
		private List<XmlColumn> columns;
	}
}
