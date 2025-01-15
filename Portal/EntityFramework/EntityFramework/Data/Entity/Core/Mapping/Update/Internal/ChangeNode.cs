using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;

namespace System.Data.Entity.Core.Mapping.Update.Internal
{
	// Token: 0x020005C1 RID: 1473
	internal class ChangeNode
	{
		// Token: 0x06004748 RID: 18248 RVA: 0x000FBEF0 File Offset: 0x000FA0F0
		internal ChangeNode(TypeUsage elementType)
		{
			this.m_elementType = elementType;
		}

		// Token: 0x17000E12 RID: 3602
		// (get) Token: 0x06004749 RID: 18249 RVA: 0x000FBF15 File Offset: 0x000FA115
		internal TypeUsage ElementType
		{
			get
			{
				return this.m_elementType;
			}
		}

		// Token: 0x17000E13 RID: 3603
		// (get) Token: 0x0600474A RID: 18250 RVA: 0x000FBF1D File Offset: 0x000FA11D
		internal List<PropagatorResult> Inserted
		{
			get
			{
				return this.m_inserted;
			}
		}

		// Token: 0x17000E14 RID: 3604
		// (get) Token: 0x0600474B RID: 18251 RVA: 0x000FBF25 File Offset: 0x000FA125
		internal List<PropagatorResult> Deleted
		{
			get
			{
				return this.m_deleted;
			}
		}

		// Token: 0x17000E15 RID: 3605
		// (get) Token: 0x0600474C RID: 18252 RVA: 0x000FBF2D File Offset: 0x000FA12D
		// (set) Token: 0x0600474D RID: 18253 RVA: 0x000FBF35 File Offset: 0x000FA135
		internal PropagatorResult Placeholder { get; set; }

		// Token: 0x0400194E RID: 6478
		private readonly TypeUsage m_elementType;

		// Token: 0x0400194F RID: 6479
		private readonly List<PropagatorResult> m_inserted = new List<PropagatorResult>();

		// Token: 0x04001950 RID: 6480
		private readonly List<PropagatorResult> m_deleted = new List<PropagatorResult>();
	}
}
