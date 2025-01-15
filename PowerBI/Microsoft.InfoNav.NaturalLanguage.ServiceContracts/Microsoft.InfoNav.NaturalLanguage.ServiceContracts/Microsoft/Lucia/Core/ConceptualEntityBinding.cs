using System;
using Microsoft.InfoNav;

namespace Microsoft.Lucia.Core
{
	// Token: 0x02000084 RID: 132
	public sealed class ConceptualEntityBinding : ConceptualBinding
	{
		// Token: 0x0600025B RID: 603 RVA: 0x000058D6 File Offset: 0x00003AD6
		public ConceptualEntityBinding(string entity, string schema = null)
			: base(entity, schema)
		{
		}

		// Token: 0x0600025C RID: 604 RVA: 0x000058E0 File Offset: 0x00003AE0
		public override bool Equals(ConceptualBinding other)
		{
			ConceptualEntityBinding conceptualEntityBinding = other as ConceptualEntityBinding;
			bool? flag = Util.AreEqual<ConceptualEntityBinding>(this, conceptualEntityBinding);
			if (flag != null)
			{
				return flag.Value;
			}
			return ConceptualNameComparer.Instance.Equals(base.Entity, conceptualEntityBinding.Entity);
		}

		// Token: 0x0600025D RID: 605 RVA: 0x00005923 File Offset: 0x00003B23
		protected override int GetHashCodeCore()
		{
			return -48879;
		}
	}
}
