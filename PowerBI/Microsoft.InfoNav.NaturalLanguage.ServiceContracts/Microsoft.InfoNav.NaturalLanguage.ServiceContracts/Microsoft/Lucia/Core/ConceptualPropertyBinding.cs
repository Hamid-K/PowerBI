using System;
using Microsoft.InfoNav;

namespace Microsoft.Lucia.Core
{
	// Token: 0x02000085 RID: 133
	public sealed class ConceptualPropertyBinding : ConceptualBinding
	{
		// Token: 0x0600025E RID: 606 RVA: 0x0000592A File Offset: 0x00003B2A
		public ConceptualPropertyBinding(string entity, string property, string variationSource = null, string variationSet = null, string schema = null)
			: base(entity, schema)
		{
			this.Property = property;
			this.VariationSource = variationSource;
			this.VariationSet = variationSet;
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x0600025F RID: 607 RVA: 0x0000594B File Offset: 0x00003B4B
		public string Property { get; }

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x06000260 RID: 608 RVA: 0x00005953 File Offset: 0x00003B53
		public string VariationSource { get; }

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x06000261 RID: 609 RVA: 0x0000595B File Offset: 0x00003B5B
		public string VariationSet { get; }

		// Token: 0x06000262 RID: 610 RVA: 0x00005964 File Offset: 0x00003B64
		public override bool Equals(ConceptualBinding other)
		{
			ConceptualPropertyBinding conceptualPropertyBinding = other as ConceptualPropertyBinding;
			bool? flag = Util.AreEqual<ConceptualPropertyBinding>(this, conceptualPropertyBinding);
			if (flag != null)
			{
				return flag.Value;
			}
			return ConceptualNameComparer.Instance.Equals(base.Entity, conceptualPropertyBinding.Entity) && ConceptualNameComparer.Instance.Equals(this.VariationSource, conceptualPropertyBinding.VariationSource) && ConceptualNameComparer.Instance.Equals(this.VariationSet, conceptualPropertyBinding.VariationSet) && ConceptualNameComparer.Instance.Equals(this.Property, conceptualPropertyBinding.Property);
		}

		// Token: 0x06000263 RID: 611 RVA: 0x000059F4 File Offset: 0x00003BF4
		protected override int GetHashCodeCore()
		{
			int num = ConceptualNameComparer.Instance.GetHashCode(this.Property);
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
