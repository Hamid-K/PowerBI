using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Query.InternalTrees;

namespace System.Data.Entity.Core.Query.PlanCompiler
{
	// Token: 0x02000376 RID: 886
	internal abstract class VarInfo
	{
		// Token: 0x170008A0 RID: 2208
		// (get) Token: 0x06002ADD RID: 10973
		internal abstract VarInfoKind Kind { get; }

		// Token: 0x170008A1 RID: 2209
		// (get) Token: 0x06002ADE RID: 10974 RVA: 0x0008CD0A File Offset: 0x0008AF0A
		internal virtual List<Var> NewVars
		{
			get
			{
				return null;
			}
		}
	}
}
