using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Microsoft.AnalysisServices.Tabular.DDL
{
	// Token: 0x02000124 RID: 292
	internal class ObjectRowsetSchema : IObjectRowsetSchema
	{
		// Token: 0x06001470 RID: 5232 RVA: 0x0008AC60 File Offset: 0x00088E60
		public ObjectRowsetSchema()
		{
			XNamespace xsd = XmlaConstants.XNS.xsd;
			this.sequence = new XElement(xsd + "sequence");
			this.xmlSchema = new XElement(xsd + "schema", new object[]
			{
				new XAttribute(XNamespace.Xmlns + "xs", "http://www.w3.org/2001/XMLSchema"),
				new XAttribute(XNamespace.Xmlns + "sql", "urn:schemas-microsoft-com:xml-sql"),
				new XElement(xsd + "element", new XElement(xsd + "complexType", new XElement(xsd + "sequence", new XElement(xsd + "element", new XAttribute("type", "row"))))),
				new XElement(xsd + "complexType", new object[]
				{
					new XAttribute("name", "row"),
					this.sequence
				})
			});
			this.orderedPropertyList = new List<string>();
			this.skippedProperties = new List<string>();
		}

		// Token: 0x06001471 RID: 5233 RVA: 0x0008AD89 File Offset: 0x00088F89
		public void AddXmlSchemaField(string propertyName, string propertyType)
		{
			ObjectRowsetSchema.AddXmlSchemaField(this.sequence, propertyName, propertyType, 0);
			this.orderedPropertyList.Add(propertyName);
		}

		// Token: 0x06001472 RID: 5234 RVA: 0x0008ADA5 File Offset: 0x00088FA5
		private static void AddXmlSchemaField(XElement seq, string name, string type)
		{
			ObjectRowsetSchema.AddXmlSchemaField(seq, name, type, 0);
		}

		// Token: 0x06001473 RID: 5235 RVA: 0x0008ADB0 File Offset: 0x00088FB0
		private static void AddXmlSchemaField(XElement seq, string name, string type, int minOccurs)
		{
			seq.Add(new XElement(XmlaConstants.XNS.xsd + "element", new object[]
			{
				new XAttribute("name", name),
				new XAttribute("type", type),
				new XAttribute(XmlaConstants.XNS.sql + "field", name),
				new XAttribute("minOccurs", minOccurs.ToString())
			}));
		}

		// Token: 0x1700055E RID: 1374
		// (get) Token: 0x06001474 RID: 5236 RVA: 0x0008AE34 File Offset: 0x00089034
		XElement IObjectRowsetSchema.XmlSchema
		{
			get
			{
				return this.xmlSchema;
			}
		}

		// Token: 0x1700055F RID: 1375
		// (get) Token: 0x06001475 RID: 5237 RVA: 0x0008AE3C File Offset: 0x0008903C
		IList<string> IObjectRowsetSchema.OrderedPropertyList
		{
			get
			{
				return this.orderedPropertyList;
			}
		}

		// Token: 0x04000305 RID: 773
		private XElement xmlSchema;

		// Token: 0x04000306 RID: 774
		private XElement sequence;

		// Token: 0x04000307 RID: 775
		private List<string> orderedPropertyList;

		// Token: 0x04000308 RID: 776
		private List<string> skippedProperties;
	}
}
