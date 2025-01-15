using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Microsoft.InfoNav;

namespace Microsoft.Lucia.Core
{
	// Token: 0x0200006D RID: 109
	[XmlRoot(ElementName = "LinguisticSchemasCacheEntry")]
	[XmlType(TypeName = "LinguisticSchemasCacheEntry")]
	public sealed class ModelLinguisticSchemasCacheEntry
	{
		// Token: 0x1700007C RID: 124
		// (get) Token: 0x060001D3 RID: 467 RVA: 0x00004A58 File Offset: 0x00002C58
		// (set) Token: 0x060001D4 RID: 468 RVA: 0x00004A60 File Offset: 0x00002C60
		[XmlArray(ElementName = "LinguisticSchemaCacheEntries", IsNullable = false)]
		[XmlArrayItem(ElementName = "LinguisticSchemaCacheEntry", IsNullable = false)]
		public List<ModelLinguisticSchemaCacheEntry> Schemas { get; set; }

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060001D5 RID: 469 RVA: 0x00004A69 File Offset: 0x00002C69
		// (set) Token: 0x060001D6 RID: 470 RVA: 0x00004A71 File Offset: 0x00002C71
		[XmlAttribute(AttributeName = "LastUpdatedTime")]
		public DateTime LastUpdatedTime { get; set; }

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060001D7 RID: 471 RVA: 0x00004A7A File Offset: 0x00002C7A
		// (set) Token: 0x060001D8 RID: 472 RVA: 0x00004A82 File Offset: 0x00002C82
		[XmlElement(ElementName = "BaseConceptualSchemaHash")]
		public long? BaseConceptualSchemaHash { get; set; }

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060001D9 RID: 473 RVA: 0x00004A8B File Offset: 0x00002C8B
		// (set) Token: 0x060001DA RID: 474 RVA: 0x00004A93 File Offset: 0x00002C93
		[XmlElement(ElementName = "BaseLinguisticSchemaHash")]
		public long? BaseLinguisticSchemaHash { get; set; }

		// Token: 0x060001DB RID: 475 RVA: 0x00004A9C File Offset: 0x00002C9C
		public static ModelLinguisticSchemasCacheEntry FromXmlString(string xml)
		{
			return ModelLinguisticSchemasCacheEntry._xmlSerializer.FromXmlString(xml);
		}

		// Token: 0x060001DC RID: 476 RVA: 0x00004AA9 File Offset: 0x00002CA9
		public string ToXmlString()
		{
			return ModelLinguisticSchemasCacheEntry._xmlSerializer.ToXmlString(this, false);
		}

		// Token: 0x04000259 RID: 601
		private static readonly XmlSerializer _xmlSerializer = new XmlSerializer(typeof(ModelLinguisticSchemasCacheEntry));
	}
}
