using System;
using Microsoft.InfoNav;
using Newtonsoft.Json;

namespace Microsoft.Lucia.Core.DomainModel.Serialization
{
	// Token: 0x0200019C RID: 412
	public sealed class ConceptualPropertyBinding : Binding, IConceptualEntityBinding
	{
		// Token: 0x1700027A RID: 634
		// (get) Token: 0x06000853 RID: 2131 RVA: 0x00010CF9 File Offset: 0x0000EEF9
		// (set) Token: 0x06000854 RID: 2132 RVA: 0x00010D01 File Offset: 0x0000EF01
		[JsonProperty(Required = Required.Always)]
		public string ConceptualEntity { get; set; }

		// Token: 0x1700027B RID: 635
		// (get) Token: 0x06000855 RID: 2133 RVA: 0x00010D0A File Offset: 0x0000EF0A
		// (set) Token: 0x06000856 RID: 2134 RVA: 0x00010D12 File Offset: 0x0000EF12
		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string VariationSource { get; set; }

		// Token: 0x1700027C RID: 636
		// (get) Token: 0x06000857 RID: 2135 RVA: 0x00010D1B File Offset: 0x0000EF1B
		// (set) Token: 0x06000858 RID: 2136 RVA: 0x00010D23 File Offset: 0x0000EF23
		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string VariationSet { get; set; }

		// Token: 0x1700027D RID: 637
		// (get) Token: 0x06000859 RID: 2137 RVA: 0x00010D2C File Offset: 0x0000EF2C
		// (set) Token: 0x0600085A RID: 2138 RVA: 0x00010D34 File Offset: 0x0000EF34
		[JsonProperty(Required = Required.Always)]
		public string ConceptualProperty { get; set; }

		// Token: 0x0600085B RID: 2139 RVA: 0x00010D40 File Offset: 0x0000EF40
		public override bool Equals(Binding other)
		{
			ConceptualPropertyBinding conceptualPropertyBinding = other as ConceptualPropertyBinding;
			bool? flag = Util.AreEqual<ConceptualPropertyBinding>(this, conceptualPropertyBinding);
			if (flag != null)
			{
				return flag.Value;
			}
			return ConceptualNameComparer.Instance.Equals(this.ConceptualEntity, conceptualPropertyBinding.ConceptualEntity) && ConceptualNameComparer.Instance.Equals(this.VariationSource, conceptualPropertyBinding.VariationSource) && ConceptualNameComparer.Instance.Equals(this.VariationSet, conceptualPropertyBinding.VariationSet) && ConceptualNameComparer.Instance.Equals(this.ConceptualProperty, conceptualPropertyBinding.ConceptualProperty);
		}

		// Token: 0x0600085C RID: 2140 RVA: 0x00010DD0 File Offset: 0x0000EFD0
		protected override int GetHashCodeCore()
		{
			int num = Hashing.CombineHash(ConceptualNameComparer.Instance.GetHashCode(this.ConceptualEntity), ConceptualNameComparer.Instance.GetHashCode(this.ConceptualProperty));
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

		// Token: 0x04000721 RID: 1825
		internal const string ConceptualPropertyCommonName = "Column";

		// Token: 0x04000722 RID: 1826
		internal const string ConceptualPropertyMeasureCommonName = "Measure";
	}
}
