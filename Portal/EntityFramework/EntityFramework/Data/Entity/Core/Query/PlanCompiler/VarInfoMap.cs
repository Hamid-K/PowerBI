using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Query.InternalTrees;

namespace System.Data.Entity.Core.Query.PlanCompiler
{
	// Token: 0x02000378 RID: 888
	internal class VarInfoMap
	{
		// Token: 0x06002AE0 RID: 10976 RVA: 0x0008CD15 File Offset: 0x0008AF15
		internal VarInfoMap()
		{
			this.m_map = new Dictionary<Var, VarInfo>();
		}

		// Token: 0x06002AE1 RID: 10977 RVA: 0x0008CD28 File Offset: 0x0008AF28
		internal VarInfo CreateStructuredVarInfo(Var v, RowType newType, List<Var> newVars, List<EdmProperty> newProperties, bool newVarsIncludeNullSentinelVar)
		{
			VarInfo varInfo = new StructuredVarInfo(newType, newVars, newProperties, newVarsIncludeNullSentinelVar);
			this.m_map.Add(v, varInfo);
			return varInfo;
		}

		// Token: 0x06002AE2 RID: 10978 RVA: 0x0008CD4F File Offset: 0x0008AF4F
		internal VarInfo CreateStructuredVarInfo(Var v, RowType newType, List<Var> newVars, List<EdmProperty> newProperties)
		{
			return this.CreateStructuredVarInfo(v, newType, newVars, newProperties, false);
		}

		// Token: 0x06002AE3 RID: 10979 RVA: 0x0008CD60 File Offset: 0x0008AF60
		internal VarInfo CreateCollectionVarInfo(Var v, Var newVar)
		{
			VarInfo varInfo = new CollectionVarInfo(newVar);
			this.m_map.Add(v, varInfo);
			return varInfo;
		}

		// Token: 0x06002AE4 RID: 10980 RVA: 0x0008CD84 File Offset: 0x0008AF84
		internal VarInfo CreatePrimitiveTypeVarInfo(Var v, Var newVar)
		{
			PlanCompiler.Assert(TypeSemantics.IsScalarType(v.Type), "The current variable should be of primitive or enum type.");
			PlanCompiler.Assert(TypeSemantics.IsScalarType(newVar.Type), "The new variable should be of primitive or enum type.");
			VarInfo varInfo = new PrimitiveTypeVarInfo(newVar);
			this.m_map.Add(v, varInfo);
			return varInfo;
		}

		// Token: 0x06002AE5 RID: 10981 RVA: 0x0008CDD0 File Offset: 0x0008AFD0
		internal bool TryGetVarInfo(Var v, out VarInfo varInfo)
		{
			return this.m_map.TryGetValue(v, out varInfo);
		}

		// Token: 0x04000ED2 RID: 3794
		private readonly Dictionary<Var, VarInfo> m_map;
	}
}
