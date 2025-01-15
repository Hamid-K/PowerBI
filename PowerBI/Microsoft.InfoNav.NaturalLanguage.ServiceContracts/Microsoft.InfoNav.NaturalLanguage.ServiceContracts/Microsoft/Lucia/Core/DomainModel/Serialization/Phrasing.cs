using System;
using Newtonsoft.Json;

namespace Microsoft.Lucia.Core.DomainModel.Serialization
{
	// Token: 0x020001B8 RID: 440
	public sealed class Phrasing : SharedPhrasingProperties
	{
		// Token: 0x170002BD RID: 701
		// (get) Token: 0x0600090D RID: 2317 RVA: 0x00011ABA File Offset: 0x0000FCBA
		// (set) Token: 0x0600090E RID: 2318 RVA: 0x00011AC7 File Offset: 0x0000FCC7
		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
		public AttributePhrasingProperties Attribute
		{
			get
			{
				return this.Properties as AttributePhrasingProperties;
			}
			set
			{
				this.Properties = value;
			}
		}

		// Token: 0x170002BE RID: 702
		// (get) Token: 0x0600090F RID: 2319 RVA: 0x00011AD0 File Offset: 0x0000FCD0
		// (set) Token: 0x06000910 RID: 2320 RVA: 0x00011ADD File Offset: 0x0000FCDD
		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
		public NamePhrasingProperties Name
		{
			get
			{
				return this.Properties as NamePhrasingProperties;
			}
			set
			{
				this.Properties = value;
			}
		}

		// Token: 0x170002BF RID: 703
		// (get) Token: 0x06000911 RID: 2321 RVA: 0x00011AE6 File Offset: 0x0000FCE6
		// (set) Token: 0x06000912 RID: 2322 RVA: 0x00011AF3 File Offset: 0x0000FCF3
		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
		public AdjectivePhrasingProperties Adjective
		{
			get
			{
				return this.Properties as AdjectivePhrasingProperties;
			}
			set
			{
				this.Properties = value;
			}
		}

		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x06000913 RID: 2323 RVA: 0x00011AFC File Offset: 0x0000FCFC
		// (set) Token: 0x06000914 RID: 2324 RVA: 0x00011B09 File Offset: 0x0000FD09
		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
		public DynamicAdjectivePhrasingProperties DynamicAdjective
		{
			get
			{
				return this.Properties as DynamicAdjectivePhrasingProperties;
			}
			set
			{
				this.Properties = value;
			}
		}

		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x06000915 RID: 2325 RVA: 0x00011B12 File Offset: 0x0000FD12
		// (set) Token: 0x06000916 RID: 2326 RVA: 0x00011B1F File Offset: 0x0000FD1F
		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
		public NounPhrasingProperties Noun
		{
			get
			{
				return this.Properties as NounPhrasingProperties;
			}
			set
			{
				this.Properties = value;
			}
		}

		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x06000917 RID: 2327 RVA: 0x00011B28 File Offset: 0x0000FD28
		// (set) Token: 0x06000918 RID: 2328 RVA: 0x00011B35 File Offset: 0x0000FD35
		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
		public DynamicNounPhrasingProperties DynamicNoun
		{
			get
			{
				return this.Properties as DynamicNounPhrasingProperties;
			}
			set
			{
				this.Properties = value;
			}
		}

		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x06000919 RID: 2329 RVA: 0x00011B3E File Offset: 0x0000FD3E
		// (set) Token: 0x0600091A RID: 2330 RVA: 0x00011B4B File Offset: 0x0000FD4B
		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
		public PrepositionPhrasingProperties Preposition
		{
			get
			{
				return this.Properties as PrepositionPhrasingProperties;
			}
			set
			{
				this.Properties = value;
			}
		}

		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x0600091B RID: 2331 RVA: 0x00011B54 File Offset: 0x0000FD54
		// (set) Token: 0x0600091C RID: 2332 RVA: 0x00011B61 File Offset: 0x0000FD61
		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
		public VerbPhrasingProperties Verb
		{
			get
			{
				return this.Properties as VerbPhrasingProperties;
			}
			set
			{
				this.Properties = value;
			}
		}

		// Token: 0x170002C5 RID: 709
		// (get) Token: 0x0600091D RID: 2333 RVA: 0x00011B6A File Offset: 0x0000FD6A
		// (set) Token: 0x0600091E RID: 2334 RVA: 0x00011B72 File Offset: 0x0000FD72
		[JsonIgnore]
		public PhrasingProperties Properties { get; set; }
	}
}
