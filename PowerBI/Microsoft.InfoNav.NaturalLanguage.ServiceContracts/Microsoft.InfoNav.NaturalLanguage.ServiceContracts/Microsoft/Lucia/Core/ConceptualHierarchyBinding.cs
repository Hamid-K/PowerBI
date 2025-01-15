using System;
using Microsoft.InfoNav;

namespace Microsoft.Lucia.Core
{
	// Token: 0x02000086 RID: 134
	public sealed class ConceptualHierarchyBinding : ConceptualBinding
	{
		// Token: 0x06000264 RID: 612 RVA: 0x00005A51 File Offset: 0x00003C51
		public ConceptualHierarchyBinding(string entity, string variationSource, string variationSet, string hierarchy, string schema = null)
			: base(entity, schema)
		{
			this.VariationSource = variationSource;
			this.VariationSet = variationSet;
			this.Hierarchy = hierarchy;
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x06000265 RID: 613 RVA: 0x00005A72 File Offset: 0x00003C72
		public string VariationSource { get; }

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x06000266 RID: 614 RVA: 0x00005A7A File Offset: 0x00003C7A
		public string VariationSet { get; }

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x06000267 RID: 615 RVA: 0x00005A82 File Offset: 0x00003C82
		public string Hierarchy { get; }

		// Token: 0x06000268 RID: 616 RVA: 0x00005A8C File Offset: 0x00003C8C
		public override bool Equals(ConceptualBinding other)
		{
			ConceptualHierarchyBinding conceptualHierarchyBinding = other as ConceptualHierarchyBinding;
			bool? flag = Util.AreEqual<ConceptualHierarchyBinding>(this, conceptualHierarchyBinding);
			if (flag != null)
			{
				return flag.Value;
			}
			return ConceptualNameComparer.Instance.Equals(base.Entity, conceptualHierarchyBinding.Entity) && ConceptualNameComparer.Instance.Equals(this.VariationSource, conceptualHierarchyBinding.VariationSource) && ConceptualNameComparer.Instance.Equals(this.VariationSet, conceptualHierarchyBinding.VariationSet) && ConceptualNameComparer.Instance.Equals(this.Hierarchy, conceptualHierarchyBinding.Hierarchy);
		}

		// Token: 0x06000269 RID: 617 RVA: 0x00005B1C File Offset: 0x00003D1C
		protected override int GetHashCodeCore()
		{
			int num = ConceptualNameComparer.Instance.GetHashCode(this.Hierarchy);
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
