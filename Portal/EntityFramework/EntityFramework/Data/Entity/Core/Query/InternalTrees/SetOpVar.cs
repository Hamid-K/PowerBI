using System;
using System.Data.Entity.Core.Metadata.Edm;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x020003E3 RID: 995
	internal sealed class SetOpVar : Var
	{
		// Token: 0x06002F02 RID: 12034 RVA: 0x000953C9 File Offset: 0x000935C9
		internal SetOpVar(int id, TypeUsage type)
			: base(id, VarType.SetOp, type)
		{
		}
	}
}
