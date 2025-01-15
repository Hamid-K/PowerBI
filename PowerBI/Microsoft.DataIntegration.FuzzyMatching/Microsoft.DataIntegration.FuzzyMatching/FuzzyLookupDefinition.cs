using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Microsoft.DataIntegration.FuzzyMatchingCommon.Collections;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x0200002F RID: 47
	[Serializable]
	public class FuzzyLookupDefinition : ObjectDefinition, IXmlSerializable, IName
	{
		// Token: 0x1700004C RID: 76
		// (get) Token: 0x0600016D RID: 365 RVA: 0x00006465 File Offset: 0x00004665
		// (set) Token: 0x0600016E RID: 366 RVA: 0x0000646D File Offset: 0x0000466D
		public new string Name { get; set; }

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x0600016F RID: 367 RVA: 0x00006476 File Offset: 0x00004676
		// (set) Token: 0x06000170 RID: 368 RVA: 0x0000647E File Offset: 0x0000467E
		public string Description { get; set; }

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000171 RID: 369 RVA: 0x00006487 File Offset: 0x00004687
		// (set) Token: 0x06000172 RID: 370 RVA: 0x0000648F File Offset: 0x0000468F
		public string DomainManagerName { get; set; }

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000173 RID: 371 RVA: 0x00006498 File Offset: 0x00004698
		// (set) Token: 0x06000174 RID: 372 RVA: 0x000064A0 File Offset: 0x000046A0
		public string ReferenceRowsetName { get; set; }

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000175 RID: 373 RVA: 0x000064A9 File Offset: 0x000046A9
		// (set) Token: 0x06000176 RID: 374 RVA: 0x000064B1 File Offset: 0x000046B1
		public StateManager StateManager { get; set; }

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000177 RID: 375 RVA: 0x000064BA File Offset: 0x000046BA
		// (set) Token: 0x06000178 RID: 376 RVA: 0x000064C2 File Offset: 0x000046C2
		public LookupCollection Lookups { get; set; }

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000179 RID: 377 RVA: 0x000064CB File Offset: 0x000046CB
		// (set) Token: 0x0600017A RID: 378 RVA: 0x000064D3 File Offset: 0x000046D3
		public RecordBinding RecordBinding { get; set; }

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x0600017B RID: 379 RVA: 0x000064DC File Offset: 0x000046DC
		// (set) Token: 0x0600017C RID: 380 RVA: 0x000064E4 File Offset: 0x000046E4
		public List<Column> KeyColumns { get; set; }

		// Token: 0x0600017D RID: 381 RVA: 0x000064ED File Offset: 0x000046ED
		public FuzzyLookupDefinition()
		{
			this.ReferenceRowsetName = "default";
			this.StateManager = this.DefaultStateManager;
			this.Lookups = new LookupCollection();
			this.KeyColumns = new List<Column>();
		}

		// Token: 0x0600017E RID: 382 RVA: 0x00006522 File Offset: 0x00004722
		public FuzzyLookupDefinition(XmlReader definition)
			: this()
		{
			this.ReadXml(definition);
		}

		// Token: 0x0600017F RID: 383 RVA: 0x00006534 File Offset: 0x00004734
		public FuzzyLookupDefinition(FuzzyLookupDefinition definition)
		{
			this.Name = definition.Name;
			this.Description = definition.Description;
			this.DomainManagerName = definition.DomainManagerName;
			this.ReferenceRowsetName = definition.ReferenceRowsetName;
			this.StateManager = definition.StateManager;
			this.Lookups = new LookupCollection(definition.Lookups);
			this.RecordBinding = new RecordBinding(definition.RecordBinding);
			this.KeyColumns = new List<Column>(definition.KeyColumns);
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000180 RID: 384 RVA: 0x000065B8 File Offset: 0x000047B8
		private StateManager DefaultStateManager
		{
			get
			{
				StateManager stateManager = new StateManager();
				stateManager.TypeName = typeof(MainMemoryStateManager).FullName;
				List<Property> list = new List<Property>();
				list.Add(new Property
				{
					Name = "EnableReferenceContextCaching",
					Value = true
				});
				stateManager.Properties = list;
				return stateManager;
			}
		}

		// Token: 0x06000181 RID: 385 RVA: 0x0000660C File Offset: 0x0000480C
		public FuzzyLookupDefinition(RecordBinding referenceDomainBinding, params string[] keyColumnNames)
		{
			this.StateManager = this.DefaultStateManager;
			this.Lookups = new LookupCollection();
			this.RecordBinding = new RecordBinding(referenceDomainBinding);
			this.KeyColumns = new List<Column>();
			foreach (string text in keyColumnNames)
			{
				this.KeyColumns.Add(new Column
				{
					Name = text
				});
			}
		}

		// Token: 0x06000182 RID: 386 RVA: 0x00006678 File Offset: 0x00004878
		internal void Validate(DomainManager domainManager)
		{
			if (this.Lookups.Count == 0)
			{
				throw new Exception("Must define at least one lookup in the index definition.");
			}
			foreach (Lookup lookup in this.Lookups)
			{
				if ((lookup.Domains == null || lookup.Domains.Count == 0) && (lookup.ExactMatchDomains == null || lookup.ExactMatchDomains.Count == 0))
				{
					throw new Exception("Must define at least one Domain or ExactMatchDomain in Lookup.");
				}
			}
			foreach (Lookup lookup2 in this.Lookups)
			{
				lookup2.Validate();
				if (lookup2.ExactMatchDomains != null)
				{
					for (int i = 0; i < lookup2.ExactMatchDomains.Count; i++)
					{
						domainManager.GetDomainId(lookup2.ExactMatchDomains[i]);
					}
				}
			}
		}

		// Token: 0x06000183 RID: 387 RVA: 0x00006784 File Offset: 0x00004984
		XmlSchema IXmlSerializable.GetSchema()
		{
			return null;
		}

		// Token: 0x06000184 RID: 388 RVA: 0x00006788 File Offset: 0x00004988
		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			reader = FuzzyLookupXmlBuilder.CreateValidatingXmlReader(reader);
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.Load(reader);
			XmlNamespaceManager xmlNamespaceManager = xmlDocument.CreateFL3XmlNamespaceManager();
			XmlNode xmlNode = xmlDocument.SelectSingleNode("/ns:FuzzyLookup", xmlNamespaceManager);
			if (xmlNode != null)
			{
				this.Name = xmlNode.Attributes.GetNamedItemOrDefault("name", null);
				this.Description = xmlNode.Attributes.GetNamedItemOrDefault("description", null);
				this.DomainManagerName = xmlNode.Attributes.GetNamedItemOrDefault("domainManagerName", null);
				this.ReferenceRowsetName = xmlNode.Attributes.GetNamedItemOrDefault("referenceRowsetName", null);
				XmlNode xmlNode2;
				if ((xmlNode2 = xmlNode.SelectSingleNode("/*/ns:StateManager", xmlNamespaceManager)) != null)
				{
					this.StateManager = new StateManager(new XmlNodeReader(xmlNode2));
				}
				if ((xmlNode2 = xmlNode.SelectSingleNode("/*/ns:Properties", xmlNamespaceManager)) != null)
				{
					CollectionSerialization.ReadXml<Property>(new XmlNodeReader(xmlNode2), "Properties", base.Properties);
				}
				if ((xmlNode2 = xmlNode.SelectSingleNode("/*/ns:Lookups", xmlNamespaceManager)) != null)
				{
					CollectionSerialization.ReadXml<Lookup>(new XmlNodeReader(xmlNode2), "Lookups", this.Lookups);
				}
				if ((xmlNode2 = xmlNode.SelectSingleNode("/*/ns:KeyColumns", xmlNamespaceManager)) != null)
				{
					CollectionSerialization.ReadXml<Column>(new XmlNodeReader(xmlNode2), "KeyColumns", this.KeyColumns);
				}
				if ((xmlNode2 = xmlNode.SelectSingleNode("/*/ns:RecordBinding", xmlNamespaceManager)) != null)
				{
					this.RecordBinding = new RecordBinding();
					this.RecordBinding.ReadXml(new XmlNodeReader(xmlNode2));
				}
			}
		}

		// Token: 0x06000185 RID: 389 RVA: 0x000068E0 File Offset: 0x00004AE0
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			writer.WriteStartElement("FuzzyLookup");
			if (!string.IsNullOrEmpty(this.Name))
			{
				writer.WriteAttributeString("name", this.Name);
			}
			if (!string.IsNullOrEmpty(this.Description))
			{
				writer.WriteAttributeString("description", this.Description);
			}
			if (!string.IsNullOrEmpty(this.DomainManagerName))
			{
				writer.WriteAttributeString("domainManagerName", this.DomainManagerName);
			}
			if (!string.IsNullOrEmpty(this.ReferenceRowsetName))
			{
				writer.WriteAttributeString("referenceRowsetName", this.ReferenceRowsetName);
			}
			if (this.StateManager != null)
			{
				this.StateManager.WriteXml(writer);
			}
			CollectionSerialization.WriteXml<Property>(writer, "Properties", base.Properties);
			CollectionSerialization.WriteXml<Lookup>(writer, "Lookups", this.Lookups);
			CollectionSerialization.WriteXml<Column>(writer, "KeyColumns", this.KeyColumns);
			if (this.RecordBinding != null)
			{
				this.RecordBinding.WriteXml(writer);
			}
			writer.WriteEndElement();
		}
	}
}
