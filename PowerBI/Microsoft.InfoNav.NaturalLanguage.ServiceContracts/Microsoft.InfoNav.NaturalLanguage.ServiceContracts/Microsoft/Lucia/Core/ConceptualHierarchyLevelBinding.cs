using System;
using Microsoft.InfoNav;

namespace Microsoft.Lucia.Core
{
	// Token: 0x02000087 RID: 135
	public sealed class ConceptualHierarchyLevelBinding : ConceptualBinding
	{
		// Token: 0x0600026A RID: 618 RVA: 0x00005B79 File Offset: 0x00003D79
		public ConceptualHierarchyLevelBinding(string entity, string variationSource, string variationSet, string hierarchy, string level, string schema = null)
			: base(entity, schema)
		{
			this.VariationSource = variationSource;
			this.VariationSet = variationSet;
			this.Hierarchy = hierarchy;
			this.HierarchyLevel = level;
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x0600026B RID: 619 RVA: 0x00005BA2 File Offset: 0x00003DA2
		public string VariationSource { get; }

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x0600026C RID: 620 RVA: 0x00005BAA File Offset: 0x00003DAA
		public string VariationSet { get; }

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x0600026D RID: 621 RVA: 0x00005BB2 File Offset: 0x00003DB2
		public string Hierarchy { get; }

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x0600026E RID: 622 RVA: 0x00005BBA File Offset: 0x00003DBA
		// (set) Token: 0x0600026F RID: 623 RVA: 0x00005BC2 File Offset: 0x00003DC2
		public string HierarchyLevel { get; set; }

		// Token: 0x06000270 RID: 624 RVA: 0x00005BCC File Offset: 0x00003DCC
		public override bool Equals(ConceptualBinding other)
		{
			ConceptualHierarchyLevelBinding conceptualHierarchyLevelBinding = other as ConceptualHierarchyLevelBinding;
			bool? flag = Util.AreEqual<ConceptualHierarchyLevelBinding>(this, conceptualHierarchyLevelBinding);
			if (flag != null)
			{
				return flag.Value;
			}
			return ConceptualNameComparer.Instance.Equals(base.Entity, conceptualHierarchyLevelBinding.Entity) && ConceptualNameComparer.Instance.Equals(this.VariationSource, conceptualHierarchyLevelBinding.VariationSource) && ConceptualNameComparer.Instance.Equals(this.VariationSet, conceptualHierarchyLevelBinding.VariationSet) && ConceptualNameComparer.Instance.Equals(this.Hierarchy, conceptualHierarchyLevelBinding.Hierarchy) && ConceptualNameComparer.Instance.Equals(this.HierarchyLevel, conceptualHierarchyLevelBinding.HierarchyLevel);
		}

		// Token: 0x06000271 RID: 625 RVA: 0x00005C74 File Offset: 0x00003E74
		protected override int GetHashCodeCore()
		{
			int num = Hashing.CombineHash(ConceptualNameComparer.Instance.GetHashCode(this.Hierarchy), ConceptualNameComparer.Instance.GetHashCode(this.HierarchyLevel));
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
