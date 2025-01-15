using System;
using System.Data.Entity.Core.Metadata.Edm;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x02000391 RID: 913
	internal sealed class ComputedVar : Var
	{
		// Token: 0x06002CB6 RID: 11446 RVA: 0x0008FFBB File Offset: 0x0008E1BB
		internal ComputedVar(int id, TypeUsage type)
			: base(id, VarType.Computed, type)
		{
		}
	}
}
