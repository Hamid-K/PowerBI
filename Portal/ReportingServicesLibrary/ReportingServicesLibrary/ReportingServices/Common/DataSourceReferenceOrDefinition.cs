using System;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.Common
{
	// Token: 0x0200035B RID: 859
	internal sealed class DataSourceReferenceOrDefinition : IXmlSerializable
	{
		// Token: 0x1700080F RID: 2063
		// (get) Token: 0x06001C6D RID: 7277 RVA: 0x00073064 File Offset: 0x00071264
		// (set) Token: 0x06001C6E RID: 7278 RVA: 0x0007306C File Offset: 0x0007126C
		public string Reference { get; set; }

		// Token: 0x17000810 RID: 2064
		// (get) Token: 0x06001C6F RID: 7279 RVA: 0x00073075 File Offset: 0x00071275
		// (set) Token: 0x06001C70 RID: 7280 RVA: 0x0007307D File Offset: 0x0007127D
		public DataSourceDefinition2 Definition { get; set; }

		// Token: 0x17000811 RID: 2065
		// (get) Token: 0x06001C71 RID: 7281 RVA: 0x00073086 File Offset: 0x00071286
		public bool IsReference
		{
			get
			{
				return this.Reference != null;
			}
		}

		// Token: 0x06001C72 RID: 7282 RVA: 0x0000289C File Offset: 0x00000A9C
		public XmlSchema GetSchema()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001C73 RID: 7283 RVA: 0x00073094 File Offset: 0x00071294
		public void ReadXml(XmlReader reader)
		{
			if (reader.LocalName == "DataSourceReference")
			{
				this.Reference = reader.ReadElementContentAsString();
				return;
			}
			DataSourceDefinition2 dataSourceDefinition = new DataSourceDefinition2();
			dataSourceDefinition.ReadXml(reader);
			this.Definition = dataSourceDefinition;
		}

		// Token: 0x06001C74 RID: 7284 RVA: 0x000730D4 File Offset: 0x000712D4
		public void WriteXml(XmlWriter writer)
		{
			if (this.IsReference)
			{
				writer.WriteElementString("DataSourceReference", this.Reference);
				return;
			}
			this.Definition.WriteXml(writer);
		}

		// Token: 0x04000BC7 RID: 3015
		internal const string DATASOURCEREFERENCE = "DataSourceReference";
	}
}
