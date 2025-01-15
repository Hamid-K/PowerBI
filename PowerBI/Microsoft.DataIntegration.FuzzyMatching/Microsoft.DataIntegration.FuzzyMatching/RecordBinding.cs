using System;
using System.Collections.Generic;
using System.Data;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Microsoft.DataIntegration.FuzzyMatchingCommon.Collections;
using Microsoft.DataIntegration.FuzzyMatchingCommon.Data;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x0200005F RID: 95
	[Serializable]
	public sealed class RecordBinding : List<DomainBinding>, IXmlSerializable, IName
	{
		// Token: 0x170000DA RID: 218
		// (get) Token: 0x060003B1 RID: 945 RVA: 0x00011CDC File Offset: 0x0000FEDC
		// (set) Token: 0x060003B2 RID: 946 RVA: 0x00011CE4 File Offset: 0x0000FEE4
		public string Name { get; set; }

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x060003B3 RID: 947 RVA: 0x00011CED File Offset: 0x0000FEED
		// (set) Token: 0x060003B4 RID: 948 RVA: 0x00011CF5 File Offset: 0x0000FEF5
		public string Description { get; set; }

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x060003B5 RID: 949 RVA: 0x00011CFE File Offset: 0x0000FEFE
		// (set) Token: 0x060003B6 RID: 950 RVA: 0x00011D06 File Offset: 0x0000FF06
		public JoinSide JoinSide { get; set; }

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x060003B7 RID: 951 RVA: 0x00011D0F File Offset: 0x0000FF0F
		// (set) Token: 0x060003B8 RID: 952 RVA: 0x00011D17 File Offset: 0x0000FF17
		public string RowsetName { get; set; }

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x060003B9 RID: 953 RVA: 0x00011D20 File Offset: 0x0000FF20
		// (set) Token: 0x060003BA RID: 954 RVA: 0x00011D28 File Offset: 0x0000FF28
		public int RidOrdinal { get; set; }

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x060003BB RID: 955 RVA: 0x00011D31 File Offset: 0x0000FF31
		// (set) Token: 0x060003BC RID: 956 RVA: 0x00011D39 File Offset: 0x0000FF39
		public DataTable Schema
		{
			get
			{
				return this.m_schema;
			}
			set
			{
				if (value != null)
				{
					this.m_schema = SchemaUtils.CloneAndSortSchema(value);
					return;
				}
				this.m_schema = null;
			}
		}

		// Token: 0x060003BD RID: 957 RVA: 0x00011D52 File Offset: 0x0000FF52
		public RecordBinding()
		{
			this.RowsetName = "default";
			this.RidOrdinal = -1;
		}

		// Token: 0x060003BE RID: 958 RVA: 0x00011D6C File Offset: 0x0000FF6C
		public RecordBinding(DataTable schema)
			: this()
		{
			this.Schema = schema;
		}

		// Token: 0x060003BF RID: 959 RVA: 0x00011D7B File Offset: 0x0000FF7B
		public RecordBinding(XmlReader reader)
			: this()
		{
			this.ReadXml(reader);
		}

		// Token: 0x060003C0 RID: 960 RVA: 0x00011D8C File Offset: 0x0000FF8C
		public RecordBinding(RecordBinding d)
		{
			this.Name = d.Name;
			this.Description = d.Description;
			this.JoinSide = d.JoinSide;
			this.RowsetName = d.RowsetName;
			this.RidOrdinal = d.RidOrdinal;
			this.Schema = d.Schema;
			base.AddRange(d);
		}

		// Token: 0x060003C1 RID: 961 RVA: 0x00011DF0 File Offset: 0x0000FFF0
		public void Bind(string domainName, params string[] columnNames)
		{
			DomainBinding domainBinding = base.Find((DomainBinding b) => b.DomainName.Equals(domainName));
			if (domainBinding != null)
			{
				domainBinding.Columns.Clear();
				foreach (string text in columnNames)
				{
					domainBinding.Columns.Add(new Column
					{
						Name = text
					});
				}
				return;
			}
			domainBinding = new DomainBinding
			{
				DomainName = domainName
			};
			foreach (string text2 in columnNames)
			{
				domainBinding.Columns.Add(new Column
				{
					Name = text2
				});
			}
			base.Add(domainBinding);
		}

		// Token: 0x060003C2 RID: 962 RVA: 0x00011EA0 File Offset: 0x000100A0
		public string[] GetBoundDomainNames()
		{
			string[] array = new string[base.Count];
			int num = 0;
			foreach (DomainBinding domainBinding in this)
			{
				array[num++] = domainBinding.DomainName;
			}
			return array;
		}

		// Token: 0x060003C3 RID: 963 RVA: 0x00011F04 File Offset: 0x00010104
		public DomainBinding GetDomainBinding(string domainName)
		{
			foreach (DomainBinding domainBinding in this)
			{
				if (domainBinding.DomainName.Equals(domainName))
				{
					return domainBinding;
				}
			}
			throw new Exception(string.Format("DomainBinding with name {0} not found.", domainName));
		}

		// Token: 0x060003C4 RID: 964 RVA: 0x00011F70 File Offset: 0x00010170
		public void Validate()
		{
			foreach (DomainBinding domainBinding in this)
			{
				domainBinding.Validate(this.Schema);
			}
		}

		// Token: 0x060003C5 RID: 965 RVA: 0x00011FC4 File Offset: 0x000101C4
		XmlSchema IXmlSerializable.GetSchema()
		{
			return null;
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x00011FC8 File Offset: 0x000101C8
		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.Load(reader);
			XmlNamespaceManager xmlNamespaceManager = xmlDocument.CreateFL3XmlNamespaceManager();
			XmlNode xmlNode = xmlDocument.SelectSingleNode("/ns:RecordBinding", xmlNamespaceManager);
			if (xmlNode != null)
			{
				this.Name = xmlNode.Attributes.GetNamedItemOrDefault("name", null);
				this.Description = xmlNode.Attributes.GetNamedItemOrDefault("description", null);
				XmlNode xmlNode2;
				if ((xmlNode2 = xmlNode.Attributes.GetNamedItem("joinSide")) != null)
				{
					this.JoinSide = (JoinSide)Enum.Parse(typeof(JoinSide), xmlNode2.Value);
				}
				if ((xmlNode2 = xmlNode.Attributes.GetNamedItem("rowsetName")) != null)
				{
					this.RowsetName = xmlNode2.Value;
				}
				if ((xmlNode2 = xmlNode.Attributes.GetNamedItem("ridOrdinal")) != null)
				{
					this.RidOrdinal = int.Parse(xmlNode2.Value);
				}
				foreach (object obj in xmlNode.SelectNodes("//ns:DomainBinding", xmlNamespaceManager))
				{
					XmlNode xmlNode3 = (XmlNode)obj;
					DomainBinding domainBinding = new DomainBinding();
					domainBinding.ReadXml(new XmlNodeReader(xmlNode3));
					base.Add(domainBinding);
				}
			}
		}

		// Token: 0x060003C7 RID: 967 RVA: 0x00012110 File Offset: 0x00010310
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			writer.WriteStartElement("RecordBinding");
			if (!string.IsNullOrEmpty(this.Name))
			{
				writer.WriteAttributeString("name", this.Name);
			}
			if (!string.IsNullOrEmpty(this.Description) && !string.IsNullOrEmpty(this.Description))
			{
				writer.WriteAttributeString("description", this.Description);
			}
			if (!string.IsNullOrEmpty(this.RowsetName))
			{
				writer.WriteAttributeString("rowsetName", this.RowsetName);
			}
			if (this.JoinSide != JoinSide.Undefined)
			{
				writer.WriteAttributeString("joinSide", this.JoinSide.ToString());
			}
			if (this.RidOrdinal >= 0)
			{
				writer.WriteAttributeString("ridOrdinal", this.RidOrdinal.ToString());
			}
			foreach (DomainBinding domainBinding in this)
			{
				domainBinding.WriteXml(writer);
			}
			writer.WriteEndElement();
		}

		// Token: 0x04000142 RID: 322
		private DataTable m_schema;
	}
}
