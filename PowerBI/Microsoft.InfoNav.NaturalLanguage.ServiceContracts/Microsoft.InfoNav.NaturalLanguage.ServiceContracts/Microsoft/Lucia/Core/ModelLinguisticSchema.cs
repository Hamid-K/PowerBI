using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using Microsoft.InfoNav;
using Microsoft.Lucia.Core.DomainModel;
using Microsoft.Lucia.Xml;

namespace Microsoft.Lucia.Core
{
	// Token: 0x0200006B RID: 107
	[DataContract(Name = "LinguisticSchema", Namespace = "http://schemas.microsoft.com/sqlserver/2016/01/linguisticschema")]
	[XmlRoot(ElementName = "LinguisticSchema", Namespace = "http://schemas.microsoft.com/sqlserver/2016/01/linguisticschema")]
	[XmlType(TypeName = "LinguisticSchema", Namespace = "http://schemas.microsoft.com/sqlserver/2016/01/linguisticschema")]
	public sealed class ModelLinguisticSchema : IModelLinguisticItem, IExtensibleDataObject
	{
		// Token: 0x060001A9 RID: 425 RVA: 0x0000473C File Offset: 0x0000293C
		public ModelLinguisticSchema()
		{
			this.Language = LanguageIdentifier.en_US;
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060001AA RID: 426 RVA: 0x0000474F File Offset: 0x0000294F
		// (set) Token: 0x060001AB RID: 427 RVA: 0x00004757 File Offset: 0x00002957
		[XmlIgnore]
		public LanguageIdentifier Language { get; set; }

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060001AC RID: 428 RVA: 0x00004760 File Offset: 0x00002960
		// (set) Token: 0x060001AD RID: 429 RVA: 0x00004770 File Offset: 0x00002970
		[DataMember(Name = "Language", IsRequired = true, EmitDefaultValue = false, Order = 10)]
		[XmlAttribute(AttributeName = "Language")]
		public string SerializableLanguage
		{
			get
			{
				return this.Language.ToLanguageName();
			}
			set
			{
				Contract.Check(!string.IsNullOrEmpty(value), "Language cannot be null or empty string");
				LanguageIdentifier languageIdentifier;
				if (!LanguageIdentifierUtil.TryAsLanguageIdentifier(value, out languageIdentifier))
				{
					this.Language = LanguageIdentifier.en_US;
					return;
				}
				this.Language = languageIdentifier;
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060001AE RID: 430 RVA: 0x000047AD File Offset: 0x000029AD
		// (set) Token: 0x060001AF RID: 431 RVA: 0x000047B5 File Offset: 0x000029B5
		[XmlAttribute(AttributeName = "DynamicImprovement")]
		public ModelDynamicImprovement DynamicImprovement { get; set; }

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060001B0 RID: 432 RVA: 0x000047C0 File Offset: 0x000029C0
		// (set) Token: 0x060001B1 RID: 433 RVA: 0x000047E4 File Offset: 0x000029E4
		[XmlIgnore]
		[DataMember(Name = "DynamicImprovement", IsRequired = false, EmitDefaultValue = true, Order = 20)]
		private string DataContractDynamicImprovement
		{
			get
			{
				return this.DynamicImprovement.ToString();
			}
			set
			{
				Contract.Check(!string.IsNullOrEmpty(value), "DynamicImprovement cannot be null or empty string");
				ModelDynamicImprovement modelDynamicImprovement;
				Enum.TryParse<ModelDynamicImprovement>(value, true, out modelDynamicImprovement);
				this.DynamicImprovement = modelDynamicImprovement;
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060001B2 RID: 434 RVA: 0x00004815 File Offset: 0x00002A15
		// (set) Token: 0x060001B3 RID: 435 RVA: 0x0000481D File Offset: 0x00002A1D
		[DataMember(Name = "SchemaReferences", IsRequired = false, EmitDefaultValue = false, Order = 30)]
		[XmlArray(ElementName = "SchemaReferences", IsNullable = false, Order = 30)]
		[XmlArrayItem(ElementName = "SchemaReference", IsNullable = false)]
		public List<ModelSchemaReference> SchemaReferences { get; set; }

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060001B4 RID: 436 RVA: 0x00004826 File Offset: 0x00002A26
		// (set) Token: 0x060001B5 RID: 437 RVA: 0x0000482E File Offset: 0x00002A2E
		[DataMember(Name = "Entities", IsRequired = false, EmitDefaultValue = false, Order = 40)]
		[XmlArray(ElementName = "Entities", IsNullable = false, Order = 40)]
		[XmlArrayItem(ElementName = "Entity", IsNullable = false)]
		public List<ModelLinguisticEntity> Entities { get; set; }

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x060001B6 RID: 438 RVA: 0x00004837 File Offset: 0x00002A37
		// (set) Token: 0x060001B7 RID: 439 RVA: 0x0000483F File Offset: 0x00002A3F
		ExtensionDataObject IExtensibleDataObject.ExtensionData { get; set; }

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x060001B8 RID: 440 RVA: 0x00004848 File Offset: 0x00002A48
		[XmlIgnore]
		public bool IsEmpty
		{
			get
			{
				return this.Entities.IsNullOrEmpty<ModelLinguisticEntity>();
			}
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x00004855 File Offset: 0x00002A55
		public bool ChangeLanguage(LanguageIdentifier language, bool forceImportContent)
		{
			if (language != this.Language)
			{
				if (!this.IsEmpty && !language.IsCompatibleWith(this.Language) && !forceImportContent)
				{
					return false;
				}
				this.Language = language;
			}
			return true;
		}

		// Token: 0x060001BA RID: 442 RVA: 0x00004883 File Offset: 0x00002A83
		public bool ShouldSerializeSchemaReferences()
		{
			return !this.SchemaReferences.IsNullOrEmpty<ModelSchemaReference>();
		}

		// Token: 0x060001BB RID: 443 RVA: 0x00004893 File Offset: 0x00002A93
		public bool ShouldSerializeEntities()
		{
			return !this.Entities.IsNullOrEmpty<ModelLinguisticEntity>();
		}

		// Token: 0x060001BC RID: 444 RVA: 0x000048A3 File Offset: 0x00002AA3
		public string ToXmlString(bool indent = false)
		{
			return ModelLinguisticSchema._xmlSerializer.ToXmlString(this, indent);
		}

		// Token: 0x060001BD RID: 445 RVA: 0x000048B1 File Offset: 0x00002AB1
		public string ToJsonString()
		{
			return ModelLinguisticSchema._jsonSerializer.ToJsonString(this);
		}

		// Token: 0x060001BE RID: 446 RVA: 0x000048BE File Offset: 0x00002ABE
		public void WriteTo(XmlWriter writer)
		{
			ModelLinguisticSchema._xmlSerializer.Serialize(writer, this);
		}

		// Token: 0x060001BF RID: 447 RVA: 0x000048CC File Offset: 0x00002ACC
		public static ModelLinguisticSchema Load(XmlReader reader)
		{
			return (ModelLinguisticSchema)ModelLinguisticSchema._xmlSerializer.Deserialize(reader);
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x000048DE File Offset: 0x00002ADE
		public static ModelLinguisticSchema FromJsonString(string schemaJson)
		{
			return ModelLinguisticSchema._jsonSerializer.FromJsonString(schemaJson);
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x000048EB File Offset: 0x00002AEB
		public static ModelLinguisticSchema FromXmlStringWithUpgrade(string xml, ITracer tracer = null)
		{
			return ModelLinguisticSchema.Load(LinguisticSchemaUpgrader.Upgrade(XmlReaderFactory.FromString(xml, null), null, tracer).CreateReader());
		}

		// Token: 0x0400024A RID: 586
		private const LanguageIdentifier DefaultLanguageIdentifier = LanguageIdentifier.en_US;

		// Token: 0x0400024B RID: 587
		public static readonly XNamespace XmlNamespace = LinguisticSchemaVersionInformation.LatestNamespace;

		// Token: 0x0400024C RID: 588
		public static readonly XName XmlRootElementName = ModelLinguisticSchema.XmlNamespace + "LinguisticSchema";

		// Token: 0x0400024D RID: 589
		private static readonly XmlSerializer _xmlSerializer = new XmlSerializer(typeof(ModelLinguisticSchema));

		// Token: 0x0400024E RID: 590
		private static readonly DataContractJsonSerializer _jsonSerializer = new DataContractJsonSerializer(typeof(ModelLinguisticSchema));
	}
}
