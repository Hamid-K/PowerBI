using System;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.InfoNav;
using Microsoft.Lucia.Yaml;
using Newtonsoft.Json;

namespace Microsoft.Lucia.Core.DomainModel.Serialization
{
	// Token: 0x020001AA RID: 426
	public sealed class Entity : ICustomSerializationOptions, IStateItem
	{
		// Token: 0x17000299 RID: 665
		// (get) Token: 0x060008B3 RID: 2227 RVA: 0x00011729 File Offset: 0x0000F929
		// (set) Token: 0x060008B4 RID: 2228 RVA: 0x00011731 File Offset: 0x0000F931
		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
		public EntityDefinition Definition { get; set; }

		// Token: 0x1700029A RID: 666
		// (get) Token: 0x060008B5 RID: 2229 RVA: 0x0001173A File Offset: 0x0000F93A
		// (set) Token: 0x060008B6 RID: 2230 RVA: 0x00011742 File Offset: 0x0000F942
		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
		public State State { get; set; }

		// Token: 0x1700029B RID: 667
		// (get) Token: 0x060008B7 RID: 2231 RVA: 0x0001174B File Offset: 0x0000F94B
		// (set) Token: 0x060008B8 RID: 2232 RVA: 0x00011753 File Offset: 0x0000F953
		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
		[Obsolete]
		public bool Hidden { get; set; }

		// Token: 0x1700029C RID: 668
		// (get) Token: 0x060008B9 RID: 2233 RVA: 0x0001175C File Offset: 0x0000F95C
		// (set) Token: 0x060008BA RID: 2234 RVA: 0x00011764 File Offset: 0x0000F964
		[JsonProperty]
		public EnumProperty<EntityVisibility> Visibility { get; set; }

		// Token: 0x1700029D RID: 669
		// (get) Token: 0x060008BB RID: 2235 RVA: 0x0001176D File Offset: 0x0000F96D
		// (set) Token: 0x060008BC RID: 2236 RVA: 0x00011775 File Offset: 0x0000F975
		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
		[DefaultValue(1.0)]
		public double Weight { get; set; } = 1.0;

		// Token: 0x1700029E RID: 670
		// (get) Token: 0x060008BD RID: 2237 RVA: 0x0001177E File Offset: 0x0000F97E
		// (set) Token: 0x060008BE RID: 2238 RVA: 0x00011786 File Offset: 0x0000F986
		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string TemplateSchema { get; set; }

		// Token: 0x1700029F RID: 671
		// (get) Token: 0x060008BF RID: 2239 RVA: 0x0001178F File Offset: 0x0000F98F
		[JsonProperty]
		public List<Term> Terms { get; } = new TermList();

		// Token: 0x170002A0 RID: 672
		// (get) Token: 0x060008C0 RID: 2240 RVA: 0x00011797 File Offset: 0x0000F997
		[JsonProperty]
		public List<string> Units { get; } = new List<string>();

		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x060008C1 RID: 2241 RVA: 0x0001179F File Offset: 0x0000F99F
		// (set) Token: 0x060008C2 RID: 2242 RVA: 0x000117A7 File Offset: 0x0000F9A7
		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
		public EntitySemanticType? SemanticType { get; set; }

		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x060008C3 RID: 2243 RVA: 0x000117B0 File Offset: 0x0000F9B0
		// (set) Token: 0x060008C4 RID: 2244 RVA: 0x000117B8 File Offset: 0x0000F9B8
		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
		public EntityNameType NameType { get; set; }

		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x060008C5 RID: 2245 RVA: 0x000117C1 File Offset: 0x0000F9C1
		// (set) Token: 0x060008C6 RID: 2246 RVA: 0x000117C9 File Offset: 0x0000F9C9
		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
		public Instances Instances { get; set; }

		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x060008C7 RID: 2247 RVA: 0x000117D2 File Offset: 0x0000F9D2
		// (set) Token: 0x060008C8 RID: 2248 RVA: 0x000117DA File Offset: 0x0000F9DA
		[JsonProperty]
		public List<EntityReference> ImplicitGroupings { get; set; } = new List<EntityReference>();

		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x060008C9 RID: 2249 RVA: 0x000117E3 File Offset: 0x0000F9E3
		// (set) Token: 0x060008CA RID: 2250 RVA: 0x000117EB File Offset: 0x0000F9EB
		internal DataType? DerivedDataType { get; set; }

		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x060008CB RID: 2251 RVA: 0x000117F4 File Offset: 0x0000F9F4
		YamlSerializationOptions ICustomSerializationOptions.Options
		{
			get
			{
				return YamlSerializationOptions.LineBreakAfter;
			}
		}

		// Token: 0x060008CC RID: 2252 RVA: 0x000117F7 File Offset: 0x0000F9F7
		public bool ShouldSerializeTerms()
		{
			return this.Terms.Count > 0;
		}

		// Token: 0x060008CD RID: 2253 RVA: 0x00011807 File Offset: 0x0000FA07
		public bool ShouldSerializeUnits()
		{
			return this.Units.Count > 0;
		}

		// Token: 0x060008CE RID: 2254 RVA: 0x00011817 File Offset: 0x0000FA17
		public bool ShouldSerializeImplicitGroupings()
		{
			return this.ImplicitGroupings.Count > 0;
		}

		// Token: 0x060008CF RID: 2255 RVA: 0x00011828 File Offset: 0x0000FA28
		public bool ShouldSerializeVisibility()
		{
			return this.Visibility.ShouldSerialize();
		}
	}
}
