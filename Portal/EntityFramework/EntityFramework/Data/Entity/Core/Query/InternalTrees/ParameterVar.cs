using System;
using System.Data.Entity.Core.Metadata.Edm;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x020003CB RID: 971
	internal sealed class ParameterVar : Var
	{
		// Token: 0x06002E8D RID: 11917 RVA: 0x00094ACE File Offset: 0x00092CCE
		internal ParameterVar(int id, TypeUsage type, string paramName)
			: base(id, VarType.Parameter, type)
		{
			this.m_paramName = paramName;
		}

		// Token: 0x17000919 RID: 2329
		// (get) Token: 0x06002E8E RID: 11918 RVA: 0x00094AE0 File Offset: 0x00092CE0
		internal string ParameterName
		{
			get
			{
				return this.m_paramName;
			}
		}

		// Token: 0x06002E8F RID: 11919 RVA: 0x00094AE8 File Offset: 0x00092CE8
		internal override bool TryGetName(out string name)
		{
			name = this.ParameterName;
			return true;
		}

		// Token: 0x04000FB4 RID: 4020
		private readonly string m_paramName;
	}
}
