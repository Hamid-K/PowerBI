using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Microsoft.DataIntegration.FuzzyMatchingCommon.Collections;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x0200000B RID: 11
	[Serializable]
	public class ComparisonDefinition : IXmlSerializable, IName
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000028 RID: 40 RVA: 0x00002A9C File Offset: 0x00000C9C
		// (set) Token: 0x06000029 RID: 41 RVA: 0x00002AA4 File Offset: 0x00000CA4
		public string Name { get; set; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600002A RID: 42 RVA: 0x00002AAD File Offset: 0x00000CAD
		// (set) Token: 0x0600002B RID: 43 RVA: 0x00002AB5 File Offset: 0x00000CB5
		public string Description { get; set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600002C RID: 44 RVA: 0x00002ABE File Offset: 0x00000CBE
		// (set) Token: 0x0600002D RID: 45 RVA: 0x00002AC6 File Offset: 0x00000CC6
		public List<string> Domains { get; set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600002E RID: 46 RVA: 0x00002ACF File Offset: 0x00000CCF
		// (set) Token: 0x0600002F RID: 47 RVA: 0x00002AD7 File Offset: 0x00000CD7
		public List<string> ExactMatchDomains { get; set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000030 RID: 48 RVA: 0x00002AE0 File Offset: 0x00000CE0
		// (set) Token: 0x06000031 RID: 49 RVA: 0x00002AE8 File Offset: 0x00000CE8
		public Comparer Comparer { get; set; }

		// Token: 0x06000032 RID: 50 RVA: 0x00002AF1 File Offset: 0x00000CF1
		public IFuzzyComparer CreateInstance(RecordBinding leftBindings, RecordBinding rightBindings)
		{
			IFuzzyComparer fuzzyComparer = (IFuzzyComparer)this.Comparer.CreateInstance();
			fuzzyComparer.Initialize(leftBindings, rightBindings, this.Domains, this.ExactMatchDomains);
			return fuzzyComparer;
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002B17 File Offset: 0x00000D17
		public ComparisonDefinition()
		{
			this.Domains = new List<string>();
			this.ExactMatchDomains = new List<string>();
			this.Comparer = new Comparer();
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002B40 File Offset: 0x00000D40
		public ComparisonDefinition(XmlReader reader)
			: this()
		{
			this.ReadXml(reader);
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002B4F File Offset: 0x00000D4F
		XmlSchema IXmlSerializable.GetSchema()
		{
			return null;
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002B54 File Offset: 0x00000D54
		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.Load(reader);
			XmlNamespaceManager xmlNamespaceManager = xmlDocument.CreateFL3XmlNamespaceManager();
			XmlNode xmlNode = xmlDocument.SelectSingleNode("/ns:Comparison", xmlNamespaceManager);
			if (xmlNode != null)
			{
				this.Name = xmlNode.Attributes.GetNamedItemOrDefault("name", null);
				this.Description = xmlNode.Attributes.GetNamedItemOrDefault("description", null);
				this.Domains = Enumerable.ToList<string>(Enumerable.Select<DomainName, string>(xmlNode.SelectNodes("//ns:Domains/Domain", xmlNamespaceManager).ReadXmlObjects<DomainName>(), (DomainName n) => n.Name));
				this.ExactMatchDomains = Enumerable.ToList<string>(Enumerable.Select<DomainName, string>(xmlNode.SelectNodes("//ns:ExactMatchDomains/Domain", xmlNamespaceManager).ReadXmlObjects<DomainName>(), (DomainName n) => n.Name));
				this.Comparer = xmlNode.SelectSingleNode("/*/ns:Comparer", xmlNamespaceManager).ReadXmlObject<Comparer>();
			}
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002C4C File Offset: 0x00000E4C
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			writer.WriteStartElement("Comparison");
			if (!string.IsNullOrEmpty(this.Name))
			{
				writer.WriteAttributeString("name", this.Name);
			}
			if (!string.IsNullOrEmpty(this.Description))
			{
				writer.WriteAttributeString("description", this.Description);
			}
			writer.WriteStartElement("Domains");
			foreach (string text in this.Domains)
			{
				writer.WriteElementString("Domain", text);
			}
			writer.WriteEndElement();
			writer.WriteStartElement("ExactMatchDomains");
			foreach (string text2 in this.Domains)
			{
				writer.WriteElementString("Domain", text2);
			}
			writer.WriteEndElement();
			if (this.Comparer != null)
			{
				this.Comparer.WriteXml(writer);
			}
			writer.WriteEndElement();
		}
	}
}
