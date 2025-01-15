using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.InfoNav;

namespace Microsoft.Lucia.Core
{
	// Token: 0x02000068 RID: 104
	[DataContract(Name = "Entity", Namespace = "http://schemas.microsoft.com/sqlserver/2016/01/linguisticschema")]
	[XmlRoot(ElementName = "Entity", Namespace = "http://schemas.microsoft.com/sqlserver/2016/01/linguisticschema")]
	[XmlType(TypeName = "Entity", Namespace = "http://schemas.microsoft.com/sqlserver/2016/01/linguisticschema")]
	public sealed class ModelLinguisticEntity : IModelLinguisticItem, IExtensibleDataObject, IModelLinguisticSourcedItem
	{
		// Token: 0x1700005B RID: 91
		// (get) Token: 0x0600017F RID: 383 RVA: 0x0000448D File Offset: 0x0000268D
		// (set) Token: 0x06000180 RID: 384 RVA: 0x00004495 File Offset: 0x00002695
		[DataMember(Name = "Name", IsRequired = true, EmitDefaultValue = false, Order = 10)]
		[XmlAttribute(AttributeName = "Name")]
		public string Name { get; set; }

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000181 RID: 385 RVA: 0x0000449E File Offset: 0x0000269E
		// (set) Token: 0x06000182 RID: 386 RVA: 0x000044A6 File Offset: 0x000026A6
		[DataMember(Name = "ConceptualEntity", IsRequired = false, EmitDefaultValue = false, Order = 20)]
		[XmlAttribute(AttributeName = "ConceptualEntity")]
		public string ConceptualEntity { get; set; }

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000183 RID: 387 RVA: 0x000044AF File Offset: 0x000026AF
		// (set) Token: 0x06000184 RID: 388 RVA: 0x000044B7 File Offset: 0x000026B7
		[DataMember(Name = "ConceptualVariationSource", IsRequired = false, EmitDefaultValue = false, Order = 23)]
		[XmlAttribute(AttributeName = "ConceptualVariationSource")]
		public string ConceptualVariationSource { get; set; }

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000185 RID: 389 RVA: 0x000044C0 File Offset: 0x000026C0
		// (set) Token: 0x06000186 RID: 390 RVA: 0x000044C8 File Offset: 0x000026C8
		[DataMember(Name = "ConceptualVariationSet", IsRequired = false, EmitDefaultValue = false, Order = 26)]
		[XmlAttribute(AttributeName = "ConceptualVariationSet")]
		public string ConceptualVariationSet { get; set; }

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000187 RID: 391 RVA: 0x000044D1 File Offset: 0x000026D1
		// (set) Token: 0x06000188 RID: 392 RVA: 0x000044D9 File Offset: 0x000026D9
		[DataMember(Name = "ConceptualProperty", IsRequired = false, EmitDefaultValue = false, Order = 30)]
		[XmlAttribute(AttributeName = "ConceptualProperty")]
		public string ConceptualProperty { get; set; }

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000189 RID: 393 RVA: 0x000044E2 File Offset: 0x000026E2
		// (set) Token: 0x0600018A RID: 394 RVA: 0x000044EA File Offset: 0x000026EA
		[DataMember(Name = "ConceptualHierarchy", IsRequired = false, EmitDefaultValue = false, Order = 40)]
		[XmlAttribute(AttributeName = "ConceptualHierarchy")]
		public string ConceptualHierarchy { get; set; }

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x0600018B RID: 395 RVA: 0x000044F3 File Offset: 0x000026F3
		// (set) Token: 0x0600018C RID: 396 RVA: 0x000044FB File Offset: 0x000026FB
		[DataMember(Name = "ConceptualHierarchyLevel", IsRequired = false, EmitDefaultValue = false, Order = 50)]
		[XmlAttribute(AttributeName = "ConceptualHierarchyLevel")]
		public string ConceptualHierarchyLevel { get; set; }

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x0600018D RID: 397 RVA: 0x00004504 File Offset: 0x00002704
		// (set) Token: 0x0600018E RID: 398 RVA: 0x0000450C File Offset: 0x0000270C
		[XmlAttribute(AttributeName = "Source")]
		[DefaultValue(LinguisticItemSource.User)]
		public LinguisticItemSource Source { get; set; }

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x0600018F RID: 399 RVA: 0x00004515 File Offset: 0x00002715
		// (set) Token: 0x06000190 RID: 400 RVA: 0x0000451D File Offset: 0x0000271D
		[DataMember(Name = "Source", IsRequired = false, EmitDefaultValue = false, Order = 60)]
		[XmlIgnore]
		private int DataContractSource
		{
			get
			{
				return (int)this.Source;
			}
			set
			{
				this.Source = (LinguisticItemSource)value;
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000191 RID: 401 RVA: 0x00004528 File Offset: 0x00002728
		// (set) Token: 0x06000192 RID: 402 RVA: 0x00004556 File Offset: 0x00002756
		[XmlAttribute(AttributeName = "Weight")]
		[DefaultValue(1.0)]
		public double Weight
		{
			get
			{
				double? weight = this._weight;
				if (weight == null)
				{
					return 1.0;
				}
				return weight.GetValueOrDefault();
			}
			set
			{
				this._weight = new double?(value);
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000193 RID: 403 RVA: 0x00004564 File Offset: 0x00002764
		// (set) Token: 0x06000194 RID: 404 RVA: 0x0000456C File Offset: 0x0000276C
		[DataMember(Name = "Words", IsRequired = false, EmitDefaultValue = false, Order = 70)]
		[XmlArray(ElementName = "Words", IsNullable = false, Order = 0)]
		[XmlArrayItem(ElementName = "Word", IsNullable = false)]
		public List<Word> Words { get; set; }

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000195 RID: 405 RVA: 0x00004575 File Offset: 0x00002775
		// (set) Token: 0x06000196 RID: 406 RVA: 0x0000457D File Offset: 0x0000277D
		[DataMember(Name = "Visibility", IsRequired = false, EmitDefaultValue = false, Order = 100)]
		[XmlElement(ElementName = "Visibility", IsNullable = false, Order = 100)]
		public ModelLinguisticVisibility Visibility { get; set; }

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000197 RID: 407 RVA: 0x00004588 File Offset: 0x00002788
		// (set) Token: 0x06000198 RID: 408 RVA: 0x000045CA File Offset: 0x000027CA
		[DataMember(Name = "Weight", IsRequired = false, EmitDefaultValue = false, Order = 140)]
		[XmlIgnore]
		private double? DataContractWeight
		{
			get
			{
				double? weight = this._weight;
				double num = 1.0;
				if (!((weight.GetValueOrDefault() == num) & (weight != null)))
				{
					return this._weight;
				}
				return null;
			}
			set
			{
				this._weight = ((value == null) ? new double?(1.0) : value);
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000199 RID: 409 RVA: 0x000045EC File Offset: 0x000027EC
		// (set) Token: 0x0600019A RID: 410 RVA: 0x000045F4 File Offset: 0x000027F4
		ExtensionDataObject IExtensibleDataObject.ExtensionData { get; set; }

		// Token: 0x0600019B RID: 411 RVA: 0x000045FD File Offset: 0x000027FD
		public override string ToString()
		{
			return this.Name;
		}

		// Token: 0x0600019C RID: 412 RVA: 0x00004605 File Offset: 0x00002805
		public bool ShouldSerializeWords()
		{
			return !this.Words.IsNullOrEmpty<Word>();
		}

		// Token: 0x0600019D RID: 413 RVA: 0x00004618 File Offset: 0x00002818
		public bool ShouldSerializeVisibility()
		{
			return this.Visibility.ShouldSerialize();
		}

		// Token: 0x04000235 RID: 565
		private const double DefaultWeight = 1.0;

		// Token: 0x04000236 RID: 566
		private double? _weight;
	}
}
