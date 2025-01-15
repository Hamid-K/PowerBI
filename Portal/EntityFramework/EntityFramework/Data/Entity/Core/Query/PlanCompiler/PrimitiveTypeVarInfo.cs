using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Query.InternalTrees;

namespace System.Data.Entity.Core.Query.PlanCompiler
{
	// Token: 0x0200035B RID: 859
	internal class PrimitiveTypeVarInfo : VarInfo
	{
		// Token: 0x060029BA RID: 10682 RVA: 0x000877A0 File Offset: 0x000859A0
		internal PrimitiveTypeVarInfo(Var newVar)
		{
			this.m_newVars = new List<Var> { newVar };
		}

		// Token: 0x1700086F RID: 2159
		// (get) Token: 0x060029BB RID: 10683 RVA: 0x000877BA File Offset: 0x000859BA
		internal Var NewVar
		{
			get
			{
				return this.m_newVars[0];
			}
		}

		// Token: 0x17000870 RID: 2160
		// (get) Token: 0x060029BC RID: 10684 RVA: 0x000877C8 File Offset: 0x000859C8
		internal override VarInfoKind Kind
		{
			get
			{
				return VarInfoKind.PrimitiveTypeVarInfo;
			}
		}

		// Token: 0x17000871 RID: 2161
		// (get) Token: 0x060029BD RID: 10685 RVA: 0x000877CB File Offset: 0x000859CB
		internal override List<Var> NewVars
		{
			get
			{
				return this.m_newVars;
			}
		}

		// Token: 0x04000E63 RID: 3683
		private readonly List<Var> m_newVars;
	}
}
