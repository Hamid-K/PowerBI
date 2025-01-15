using System;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x020000A9 RID: 169
	public class MetadataValue : ReportObject, IXmlSerializable
	{
		// Token: 0x06000751 RID: 1873 RVA: 0x0001B140 File Offset: 0x00019340
		public MetadataValue()
		{
		}

		// Token: 0x06000752 RID: 1874 RVA: 0x0001B148 File Offset: 0x00019348
		internal MetadataValue(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x06000753 RID: 1875 RVA: 0x0001B151 File Offset: 0x00019351
		public MetadataValue(string value)
		{
			this.Value = value;
		}

		// Token: 0x17000264 RID: 612
		// (get) Token: 0x06000754 RID: 1876 RVA: 0x0001B160 File Offset: 0x00019360
		// (set) Token: 0x06000755 RID: 1877 RVA: 0x0001B173 File Offset: 0x00019373
		public string Value
		{
			get
			{
				return (string)base.PropertyStore.GetObject(0);
			}
			set
			{
				base.PropertyStore.SetObject(0, value);
			}
		}

		// Token: 0x06000756 RID: 1878 RVA: 0x0001B182 File Offset: 0x00019382
		public override object DeepClone()
		{
			return new MetadataValue(this.Value);
		}

		// Token: 0x06000757 RID: 1879 RVA: 0x0001B18F File Offset: 0x0001938F
		public XmlSchema GetSchema()
		{
			return null;
		}

		// Token: 0x06000758 RID: 1880 RVA: 0x0001B192 File Offset: 0x00019392
		public void ReadXml(XmlReader reader)
		{
			reader.MoveToContent();
			this.Value = reader.ReadElementContentAsString();
		}

		// Token: 0x06000759 RID: 1881 RVA: 0x0001B1A7 File Offset: 0x000193A7
		public void WriteXml(XmlWriter writer)
		{
			writer.WriteString(this.Value);
		}

		// Token: 0x0200035C RID: 860
		internal class Definition : DefinitionStore<MetadataValue, MetadataValue.Definition.Properties>
		{
			// Token: 0x0200047B RID: 1147
			internal enum Properties
			{
				// Token: 0x04000AB2 RID: 2738
				Value
			}
		}
	}
}
