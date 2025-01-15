using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Microsoft.DataIntegration.FuzzyMatchingCommon.Collections;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x02000033 RID: 51
	[Serializable]
	public class Comparer : ObjectDefinition, IXmlSerializable
	{
		// Token: 0x060001B6 RID: 438 RVA: 0x00007CFA File Offset: 0x00005EFA
		public Comparer()
		{
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x00007D2E File Offset: 0x00005F2E
		public Comparer(Comparer comparer)
			: base(comparer)
		{
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x00007D63 File Offset: 0x00005F63
		public Comparer(XmlReader reader)
		{
			this.ReadXml(reader);
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060001B9 RID: 441 RVA: 0x00007D9E File Offset: 0x00005F9E
		internal ComparerPool ObjectPool
		{
			get
			{
				if (this.m_objectPool == null)
				{
					this.m_objectPool = new ComparerPool
					{
						Comparer = this
					};
				}
				return this.m_objectPool;
			}
		}

		// Token: 0x060001BA RID: 442 RVA: 0x00007DC0 File Offset: 0x00005FC0
		XmlSchema IXmlSerializable.GetSchema()
		{
			return null;
		}

		// Token: 0x060001BB RID: 443 RVA: 0x00007DC4 File Offset: 0x00005FC4
		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.Load(FuzzyLookupXmlBuilder.CreateValidatingXmlReader(reader));
			XmlNamespaceManager xmlNamespaceManager = xmlDocument.CreateFL3XmlNamespaceManager();
			XmlNode xmlNode;
			if ((xmlNode = xmlDocument.SelectSingleNode("/ns:Comparer", xmlNamespaceManager)) != null || (xmlNode = xmlDocument.SelectSingleNode("/ns:Object", xmlNamespaceManager)) != null)
			{
				base.Name = xmlNode.Attributes.GetNamedItemOrDefault("name", null);
				XmlNode xmlNode2;
				if ((xmlNode2 = xmlNode.Attributes.GetNamedItem("assemblyName")) != null)
				{
					base.AssemblyName = xmlNode2.Value;
				}
				if ((xmlNode2 = xmlNode.Attributes.GetNamedItem("typeName")) != null)
				{
					base.TypeName = xmlNode2.Value;
				}
				if ((xmlNode2 = xmlNode.SelectSingleNode("/*/ns:Properties", xmlNamespaceManager)) != null)
				{
					CollectionSerialization.ReadXml<Property>(new XmlNodeReader(xmlNode2), "Properties", base.Properties);
				}
			}
			if ((xmlNode = xmlDocument.SelectSingleNode("/ns:Comparer", xmlNamespaceManager)) != null)
			{
				XmlNodeList xmlNodeList = xmlNode.SelectNodes("//ns:RecordBinding", xmlNamespaceManager);
				RecordBinding[] array = new RecordBinding[xmlNodeList.Count];
				for (int i = 0; i < xmlNodeList.Count; i++)
				{
					array[i] = new RecordBinding(new XmlNodeReader(xmlNodeList.get_ItemOf(i)));
				}
				if (1 == xmlNodeList.Count)
				{
					if (array[0].JoinSide != JoinSide.Undefined && array[0].JoinSide != JoinSide.Both)
					{
						throw new Exception("When only one RecordBinding element is defined, the joinSide attribute must be Both or Undefined.");
					}
					this.LeftRecordBinding = new RecordBinding(array[0])
					{
						JoinSide = JoinSide.Left
					};
					this.RightRecordBinding = new RecordBinding(array[0])
					{
						JoinSide = JoinSide.Right
					};
				}
				else if (2 == xmlNodeList.Count)
				{
					this.LeftRecordBinding = (this.RightRecordBinding = null);
					foreach (int num in new int[]
					{
						default(int),
						1
					})
					{
						if (array[num].JoinSide == JoinSide.Left)
						{
							this.LeftRecordBinding = new RecordBinding(array[num]);
						}
						else if (array[num].JoinSide == JoinSide.Right)
						{
							this.RightRecordBinding = new RecordBinding(array[num]);
						}
					}
					if (this.LeftRecordBinding == null || this.RightRecordBinding == null)
					{
						throw new Exception("When two RecordBinding elements are defined, one must have attribute joinSide=Left and the other joinSide=Right");
					}
				}
				else if (xmlNodeList.Count > 2)
				{
					throw new Exception("More than two RecordBindings were defined.");
				}
				foreach (object obj in xmlNode.SelectNodes("//ns:RecordBinding", xmlNamespaceManager))
				{
					RecordBinding recordBinding = new RecordBinding(new XmlNodeReader((XmlNode)obj));
					if (JoinSide.Left == recordBinding.JoinSide)
					{
						this.LeftRecordBinding = recordBinding;
					}
					else if (JoinSide.Right == recordBinding.JoinSide)
					{
						this.RightRecordBinding = recordBinding;
					}
					else
					{
						this.LeftRecordBinding = recordBinding;
						this.RightRecordBinding = recordBinding;
					}
				}
				this.Domains = xmlNode.SelectNodes("//ns:Domains/Domain", xmlNamespaceManager).ReadXmlObjects<DomainName>();
				this.ExactMatchDomains = xmlNode.SelectNodes("//ns:ExactMatchDomains/Domain", xmlNamespaceManager).ReadXmlObjects<DomainName>();
			}
		}

		// Token: 0x060001BC RID: 444 RVA: 0x000080B8 File Offset: 0x000062B8
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			writer.WriteStartElement("Comparer");
			if (!string.IsNullOrEmpty(base.Name))
			{
				writer.WriteAttributeString("name", base.Name);
			}
			if (!string.IsNullOrEmpty(base.AssemblyName))
			{
				writer.WriteAttributeString("assemblyName", base.AssemblyName);
			}
			if (!string.IsNullOrEmpty(base.TypeName))
			{
				writer.WriteAttributeString("typeName", base.TypeName);
			}
			CollectionSerialization.WriteXml<Property>(writer, "Properties", base.Properties);
			if (this.LeftRecordBinding.Count > 0)
			{
				this.LeftRecordBinding.WriteXml(writer);
			}
			if (this.RightRecordBinding.Count > 0)
			{
				this.RightRecordBinding.WriteXml(writer);
			}
			CollectionSerialization.WriteXml<DomainName>(writer, "Domains", this.Domains);
			CollectionSerialization.WriteXml<DomainName>(writer, "ExactMatchDomains", this.ExactMatchDomains);
			writer.WriteEndElement();
		}

		// Token: 0x0400006A RID: 106
		public RecordBinding LeftRecordBinding = new RecordBinding();

		// Token: 0x0400006B RID: 107
		public RecordBinding RightRecordBinding = new RecordBinding();

		// Token: 0x0400006C RID: 108
		public List<DomainName> Domains = new List<DomainName>();

		// Token: 0x0400006D RID: 109
		public List<DomainName> ExactMatchDomains = new List<DomainName>();

		// Token: 0x0400006E RID: 110
		[NonSerialized]
		internal ComparerPool m_objectPool;
	}
}
