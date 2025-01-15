using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Query.InternalTrees;

namespace System.Data.Entity.Core.Query.PlanCompiler
{
	// Token: 0x02000334 RID: 820
	internal class CollectionVarInfo : VarInfo
	{
		// Token: 0x06002713 RID: 10003 RVA: 0x0007146D File Offset: 0x0006F66D
		internal CollectionVarInfo(Var newVar)
		{
			this.m_newVars = new List<Var>();
			this.m_newVars.Add(newVar);
		}

		// Token: 0x17000844 RID: 2116
		// (get) Token: 0x06002714 RID: 10004 RVA: 0x0007148C File Offset: 0x0006F68C
		internal Var NewVar
		{
			get
			{
				return this.m_newVars[0];
			}
		}

		// Token: 0x17000845 RID: 2117
		// (get) Token: 0x06002715 RID: 10005 RVA: 0x0007149A File Offset: 0x0006F69A
		internal override VarInfoKind Kind
		{
			get
			{
				return VarInfoKind.CollectionVarInfo;
			}
		}

		// Token: 0x17000846 RID: 2118
		// (get) Token: 0x06002716 RID: 10006 RVA: 0x0007149D File Offset: 0x0006F69D
		internal override List<Var> NewVars
		{
			get
			{
				return this.m_newVars;
			}
		}

		// Token: 0x04000DA3 RID: 3491
		private readonly List<Var> m_newVars;
	}
}
