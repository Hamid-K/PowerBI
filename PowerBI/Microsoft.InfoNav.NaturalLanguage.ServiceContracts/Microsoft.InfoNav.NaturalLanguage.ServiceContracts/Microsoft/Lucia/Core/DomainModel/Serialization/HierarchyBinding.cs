using System;
using Microsoft.InfoNav;
using Newtonsoft.Json;

namespace Microsoft.Lucia.Core.DomainModel.Serialization
{
	// Token: 0x0200019D RID: 413
	public sealed class HierarchyBinding : Binding, IConceptualEntityBinding
	{
		// Token: 0x1700027E RID: 638
		// (get) Token: 0x0600085E RID: 2142 RVA: 0x00010E4A File Offset: 0x0000F04A
		// (set) Token: 0x0600085F RID: 2143 RVA: 0x00010E52 File Offset: 0x0000F052
		[JsonProperty(Required = Required.Always)]
		public string ConceptualEntity { get; set; }

		// Token: 0x1700027F RID: 639
		// (get) Token: 0x06000860 RID: 2144 RVA: 0x00010E5B File Offset: 0x0000F05B
		// (set) Token: 0x06000861 RID: 2145 RVA: 0x00010E63 File Offset: 0x0000F063
		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string VariationSource { get; set; }

		// Token: 0x17000280 RID: 640
		// (get) Token: 0x06000862 RID: 2146 RVA: 0x00010E6C File Offset: 0x0000F06C
		// (set) Token: 0x06000863 RID: 2147 RVA: 0x00010E74 File Offset: 0x0000F074
		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string VariationSet { get; set; }

		// Token: 0x17000281 RID: 641
		// (get) Token: 0x06000864 RID: 2148 RVA: 0x00010E7D File Offset: 0x0000F07D
		// (set) Token: 0x06000865 RID: 2149 RVA: 0x00010E85 File Offset: 0x0000F085
		[JsonProperty(Required = Required.Always)]
		public string Hierarchy { get; set; }

		// Token: 0x06000866 RID: 2150 RVA: 0x00010E90 File Offset: 0x0000F090
		public override bool Equals(Binding other)
		{
			HierarchyBinding hierarchyBinding = other as HierarchyBinding;
			bool? flag = Util.AreEqual<HierarchyBinding>(this, hierarchyBinding);
			if (flag != null)
			{
				return flag.Value;
			}
			return ConceptualNameComparer.Instance.Equals(this.ConceptualEntity, hierarchyBinding.ConceptualEntity) && ConceptualNameComparer.Instance.Equals(this.VariationSource, hierarchyBinding.VariationSource) && ConceptualNameComparer.Instance.Equals(this.VariationSet, hierarchyBinding.VariationSet) && ConceptualNameComparer.Instance.Equals(this.Hierarchy, hierarchyBinding.Hierarchy);
		}

		// Token: 0x06000867 RID: 2151 RVA: 0x00010F20 File Offset: 0x0000F120
		protected override int GetHashCodeCore()
		{
			int num = Hashing.CombineHash(ConceptualNameComparer.Instance.GetHashCode(this.ConceptualEntity), ConceptualNameComparer.Instance.GetHashCode(this.Hierarchy));
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
