using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x020003BB RID: 955
	internal abstract class NewEntityBaseOp : ScalarOp
	{
		// Token: 0x06002DCB RID: 11723 RVA: 0x000923CA File Offset: 0x000905CA
		internal NewEntityBaseOp(OpType opType, TypeUsage type, bool scoped, EntitySet entitySet, List<RelProperty> relProperties)
			: base(opType, type)
		{
			this.m_scoped = scoped;
			this.m_entitySet = entitySet;
			this.m_relProperties = relProperties;
		}

		// Token: 0x06002DCC RID: 11724 RVA: 0x000923EB File Offset: 0x000905EB
		protected NewEntityBaseOp(OpType opType)
			: base(opType)
		{
		}

		// Token: 0x17000900 RID: 2304
		// (get) Token: 0x06002DCD RID: 11725 RVA: 0x000923F4 File Offset: 0x000905F4
		internal bool Scoped
		{
			get
			{
				return this.m_scoped;
			}
		}

		// Token: 0x17000901 RID: 2305
		// (get) Token: 0x06002DCE RID: 11726 RVA: 0x000923FC File Offset: 0x000905FC
		internal EntitySet EntitySet
		{
			get
			{
				return this.m_entitySet;
			}
		}

		// Token: 0x17000902 RID: 2306
		// (get) Token: 0x06002DCF RID: 11727 RVA: 0x00092404 File Offset: 0x00090604
		internal List<RelProperty> RelationshipProperties
		{
			get
			{
				return this.m_relProperties;
			}
		}

		// Token: 0x04000F50 RID: 3920
		private readonly bool m_scoped;

		// Token: 0x04000F51 RID: 3921
		private readonly EntitySet m_entitySet;

		// Token: 0x04000F52 RID: 3922
		private readonly List<RelProperty> m_relProperties;
	}
}
