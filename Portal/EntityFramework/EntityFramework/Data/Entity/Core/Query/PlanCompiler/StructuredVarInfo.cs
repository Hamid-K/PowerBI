using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Query.InternalTrees;

namespace System.Data.Entity.Core.Query.PlanCompiler
{
	// Token: 0x0200036C RID: 876
	internal class StructuredVarInfo : VarInfo
	{
		// Token: 0x06002A80 RID: 10880 RVA: 0x0008B7BC File Offset: 0x000899BC
		internal StructuredVarInfo(RowType newType, List<Var> newVars, List<EdmProperty> newTypeProperties, bool newVarsIncludeNullSentinelVar)
		{
			PlanCompiler.Assert(newVars.Count == newTypeProperties.Count, "count mismatch");
			this.m_newVars = newVars;
			this.m_newProperties = newTypeProperties;
			this.m_newType = newType;
			this.m_newVarsIncludeNullSentinelVar = newVarsIncludeNullSentinelVar;
			this.m_newTypeUsage = TypeUsage.Create(newType);
		}

		// Token: 0x17000882 RID: 2178
		// (get) Token: 0x06002A81 RID: 10881 RVA: 0x0008B810 File Offset: 0x00089A10
		internal override VarInfoKind Kind
		{
			get
			{
				return VarInfoKind.StructuredTypeVarInfo;
			}
		}

		// Token: 0x17000883 RID: 2179
		// (get) Token: 0x06002A82 RID: 10882 RVA: 0x0008B813 File Offset: 0x00089A13
		internal override List<Var> NewVars
		{
			get
			{
				return this.m_newVars;
			}
		}

		// Token: 0x17000884 RID: 2180
		// (get) Token: 0x06002A83 RID: 10883 RVA: 0x0008B81B File Offset: 0x00089A1B
		internal List<EdmProperty> Fields
		{
			get
			{
				return this.m_newProperties;
			}
		}

		// Token: 0x17000885 RID: 2181
		// (get) Token: 0x06002A84 RID: 10884 RVA: 0x0008B823 File Offset: 0x00089A23
		internal bool NewVarsIncludeNullSentinelVar
		{
			get
			{
				return this.m_newVarsIncludeNullSentinelVar;
			}
		}

		// Token: 0x06002A85 RID: 10885 RVA: 0x0008B82B File Offset: 0x00089A2B
		internal bool TryGetVar(EdmProperty p, out Var v)
		{
			if (this.m_propertyToVarMap == null)
			{
				this.InitPropertyToVarMap();
			}
			return this.m_propertyToVarMap.TryGetValue(p, out v);
		}

		// Token: 0x17000886 RID: 2182
		// (get) Token: 0x06002A86 RID: 10886 RVA: 0x0008B848 File Offset: 0x00089A48
		internal RowType NewType
		{
			get
			{
				return this.m_newType;
			}
		}

		// Token: 0x17000887 RID: 2183
		// (get) Token: 0x06002A87 RID: 10887 RVA: 0x0008B850 File Offset: 0x00089A50
		internal TypeUsage NewTypeUsage
		{
			get
			{
				return this.m_newTypeUsage;
			}
		}

		// Token: 0x06002A88 RID: 10888 RVA: 0x0008B858 File Offset: 0x00089A58
		private void InitPropertyToVarMap()
		{
			if (this.m_propertyToVarMap == null)
			{
				this.m_propertyToVarMap = new Dictionary<EdmProperty, Var>();
				IEnumerator<Var> enumerator = this.m_newVars.GetEnumerator();
				foreach (EdmProperty edmProperty in this.m_newProperties)
				{
					enumerator.MoveNext();
					this.m_propertyToVarMap.Add(edmProperty, enumerator.Current);
				}
				enumerator.Dispose();
			}
		}

		// Token: 0x04000EA4 RID: 3748
		private Dictionary<EdmProperty, Var> m_propertyToVarMap;

		// Token: 0x04000EA5 RID: 3749
		private readonly List<Var> m_newVars;

		// Token: 0x04000EA6 RID: 3750
		private readonly bool m_newVarsIncludeNullSentinelVar;

		// Token: 0x04000EA7 RID: 3751
		private readonly List<EdmProperty> m_newProperties;

		// Token: 0x04000EA8 RID: 3752
		private readonly RowType m_newType;

		// Token: 0x04000EA9 RID: 3753
		private readonly TypeUsage m_newTypeUsage;
	}
}
