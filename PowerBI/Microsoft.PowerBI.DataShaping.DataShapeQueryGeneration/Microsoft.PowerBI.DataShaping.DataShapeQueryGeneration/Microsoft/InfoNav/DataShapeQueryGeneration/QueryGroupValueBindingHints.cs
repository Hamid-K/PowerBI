using System;
using System.Collections.Generic;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x020000AD RID: 173
	internal class QueryGroupValueBindingHints
	{
		// Token: 0x06000661 RID: 1633 RVA: 0x000189C3 File Offset: 0x00016BC3
		internal QueryGroupValueBindingHints(IConceptualColumn field, IReadOnlyList<int> selectIndicesWithThisIdentity = null, bool isIdentityKey = false, bool isOrderByKey = false, bool isProjected = false)
		{
			this.Field = field;
			this.SelectIndicesWithThisIdentity = selectIndicesWithThisIdentity;
			this.IsIdentityKey = isIdentityKey;
			this.IsOrderByKey = isOrderByKey;
			this.IsProjected = isProjected;
		}

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x06000662 RID: 1634 RVA: 0x000189F0 File Offset: 0x00016BF0
		internal IConceptualColumn Field { get; }

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x06000663 RID: 1635 RVA: 0x000189F8 File Offset: 0x00016BF8
		internal IReadOnlyList<int> SelectIndicesWithThisIdentity { get; }

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x06000664 RID: 1636 RVA: 0x00018A00 File Offset: 0x00016C00
		internal bool IsIdentityKey { get; }

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x06000665 RID: 1637 RVA: 0x00018A08 File Offset: 0x00016C08
		internal bool IsOrderByKey { get; }

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x06000666 RID: 1638 RVA: 0x00018A10 File Offset: 0x00016C10
		internal bool IsProjected { get; }
	}
}
