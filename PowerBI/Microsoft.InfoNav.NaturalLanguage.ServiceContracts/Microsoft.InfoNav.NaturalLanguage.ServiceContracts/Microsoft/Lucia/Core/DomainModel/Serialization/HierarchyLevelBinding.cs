using System;
using Microsoft.InfoNav;
using Newtonsoft.Json;

namespace Microsoft.Lucia.Core.DomainModel.Serialization
{
	// Token: 0x0200019E RID: 414
	public sealed class HierarchyLevelBinding : Binding, IConceptualEntityBinding
	{
		// Token: 0x17000282 RID: 642
		// (get) Token: 0x06000869 RID: 2153 RVA: 0x00010F9A File Offset: 0x0000F19A
		// (set) Token: 0x0600086A RID: 2154 RVA: 0x00010FA2 File Offset: 0x0000F1A2
		[JsonProperty(Required = Required.Always)]
		public string ConceptualEntity { get; set; }

		// Token: 0x17000283 RID: 643
		// (get) Token: 0x0600086B RID: 2155 RVA: 0x00010FAB File Offset: 0x0000F1AB
		// (set) Token: 0x0600086C RID: 2156 RVA: 0x00010FB3 File Offset: 0x0000F1B3
		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string VariationSource { get; set; }

		// Token: 0x17000284 RID: 644
		// (get) Token: 0x0600086D RID: 2157 RVA: 0x00010FBC File Offset: 0x0000F1BC
		// (set) Token: 0x0600086E RID: 2158 RVA: 0x00010FC4 File Offset: 0x0000F1C4
		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string VariationSet { get; set; }

		// Token: 0x17000285 RID: 645
		// (get) Token: 0x0600086F RID: 2159 RVA: 0x00010FCD File Offset: 0x0000F1CD
		// (set) Token: 0x06000870 RID: 2160 RVA: 0x00010FD5 File Offset: 0x0000F1D5
		[JsonProperty(Required = Required.Always)]
		public string Hierarchy { get; set; }

		// Token: 0x17000286 RID: 646
		// (get) Token: 0x06000871 RID: 2161 RVA: 0x00010FDE File Offset: 0x0000F1DE
		// (set) Token: 0x06000872 RID: 2162 RVA: 0x00010FE6 File Offset: 0x0000F1E6
		[JsonProperty(Required = Required.Always)]
		public string HierarchyLevel { get; set; }

		// Token: 0x06000873 RID: 2163 RVA: 0x00010FF0 File Offset: 0x0000F1F0
		public override bool Equals(Binding other)
		{
			HierarchyLevelBinding hierarchyLevelBinding = other as HierarchyLevelBinding;
			bool? flag = Util.AreEqual<HierarchyLevelBinding>(this, hierarchyLevelBinding);
			if (flag != null)
			{
				return flag.Value;
			}
			return ConceptualNameComparer.Instance.Equals(this.ConceptualEntity, hierarchyLevelBinding.ConceptualEntity) && ConceptualNameComparer.Instance.Equals(this.VariationSource, hierarchyLevelBinding.VariationSource) && ConceptualNameComparer.Instance.Equals(this.VariationSet, hierarchyLevelBinding.VariationSet) && ConceptualNameComparer.Instance.Equals(this.Hierarchy, hierarchyLevelBinding.Hierarchy) && ConceptualNameComparer.Instance.Equals(this.HierarchyLevel, hierarchyLevelBinding.HierarchyLevel);
		}

		// Token: 0x06000874 RID: 2164 RVA: 0x00011098 File Offset: 0x0000F298
		protected override int GetHashCodeCore()
		{
			int num = Hashing.CombineHash(ConceptualNameComparer.Instance.GetHashCode(this.ConceptualEntity), ConceptualNameComparer.Instance.GetHashCode(this.Hierarchy), ConceptualNameComparer.Instance.GetHashCode(this.HierarchyLevel));
			if (this.VariationSource != null)
			{
				num = Hashing.CombineHash(num, ConceptualNameComparer.Instance.GetHashCode(this.VariationSource));
			}
			if (this.VariationSet != null)
			{
				num = Hashing.CombineHash(num, ConceptualNameComparer.Instance.GetHashCode(this.VariationSet));
			}
			return num;
		}
	}
}
