using System;
using System.Xml.Serialization;
using Microsoft.InfoNav;
using Microsoft.Lucia.Core.DomainModel.Serialization;
using Newtonsoft.Json;

namespace Microsoft.Lucia.Core
{
	// Token: 0x0200006C RID: 108
	[XmlRoot(ElementName = "LinguisticSchemaCacheEntry")]
	[XmlType(TypeName = "LinguisticSchemaCacheEntry")]
	public sealed class ModelLinguisticSchemaCacheEntry
	{
		// Token: 0x17000075 RID: 117
		// (get) Token: 0x060001C3 RID: 451 RVA: 0x00004960 File Offset: 0x00002B60
		// (set) Token: 0x060001C4 RID: 452 RVA: 0x00004968 File Offset: 0x00002B68
		[XmlAttribute(AttributeName = "Source")]
		public LinguisticSchemaSource Source { get; set; }

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x060001C5 RID: 453 RVA: 0x00004971 File Offset: 0x00002B71
		// (set) Token: 0x060001C6 RID: 454 RVA: 0x00004979 File Offset: 0x00002B79
		[XmlIgnore]
		public LsdlDocument ValidLsdl { get; set; }

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x060001C7 RID: 455 RVA: 0x00004982 File Offset: 0x00002B82
		// (set) Token: 0x060001C8 RID: 456 RVA: 0x00004996 File Offset: 0x00002B96
		[XmlElement(ElementName = "ValidLsdl", IsNullable = true)]
		public string SerializableValidLsdl
		{
			get
			{
				LsdlDocument validLsdl = this.ValidLsdl;
				if (validLsdl == null)
				{
					return null;
				}
				return validLsdl.ToJsonString(Formatting.None);
			}
			set
			{
				this.ValidLsdl = ModelLinguisticSchemaCacheEntry.Deserialize(value);
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060001C9 RID: 457 RVA: 0x000049A4 File Offset: 0x00002BA4
		// (set) Token: 0x060001CA RID: 458 RVA: 0x000049AC File Offset: 0x00002BAC
		[XmlIgnore]
		public LsdlDocument InvalidLsdl { get; set; }

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060001CB RID: 459 RVA: 0x000049B5 File Offset: 0x00002BB5
		// (set) Token: 0x060001CC RID: 460 RVA: 0x000049C9 File Offset: 0x00002BC9
		[XmlElement(ElementName = "InvalidLsdl", IsNullable = true)]
		public string SerializableInvalidLsdl
		{
			get
			{
				LsdlDocument invalidLsdl = this.InvalidLsdl;
				if (invalidLsdl == null)
				{
					return null;
				}
				return invalidLsdl.ToJsonString(Formatting.None);
			}
			set
			{
				this.InvalidLsdl = ModelLinguisticSchemaCacheEntry.Deserialize(value);
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060001CD RID: 461 RVA: 0x000049D7 File Offset: 0x00002BD7
		// (set) Token: 0x060001CE RID: 462 RVA: 0x000049DF File Offset: 0x00002BDF
		[XmlAttribute]
		public ModelDynamicImprovement DynamicImprovement { get; set; }

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x060001CF RID: 463 RVA: 0x000049E8 File Offset: 0x00002BE8
		// (set) Token: 0x060001D0 RID: 464 RVA: 0x000049F0 File Offset: 0x00002BF0
		[XmlAttribute]
		public LsdlDynamicImprovement LsdlDynamicImprovement { get; set; }

		// Token: 0x060001D1 RID: 465 RVA: 0x000049FC File Offset: 0x00002BFC
		private static LsdlDocument Deserialize(string jsonString)
		{
			if (string.IsNullOrEmpty(jsonString))
			{
				return null;
			}
			LsdlDocument lsdlDocument;
			try
			{
				lsdlDocument = LsdlDocument.FromJsonString(jsonString, true);
			}
			catch (Exception ex) when (!ex.IsStoppingException())
			{
				lsdlDocument = null;
			}
			return lsdlDocument;
		}
	}
}
