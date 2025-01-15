using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200045E RID: 1118
	[Serializable]
	internal abstract class WorkloadGroupStatement : TSqlStatement
	{
		// Token: 0x1700052F RID: 1327
		// (get) Token: 0x06003242 RID: 12866 RVA: 0x0016FFBC File Offset: 0x0016E1BC
		// (set) Token: 0x06003243 RID: 12867 RVA: 0x0016FFC4 File Offset: 0x0016E1C4
		public Identifier Name
		{
			get
			{
				return this._name;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._name = value;
			}
		}

		// Token: 0x17000530 RID: 1328
		// (get) Token: 0x06003244 RID: 12868 RVA: 0x0016FFD4 File Offset: 0x0016E1D4
		public IList<WorkloadGroupParameter> WorkloadGroupParameters
		{
			get
			{
				return this._workloadGroupParameters;
			}
		}

		// Token: 0x17000531 RID: 1329
		// (get) Token: 0x06003245 RID: 12869 RVA: 0x0016FFDC File Offset: 0x0016E1DC
		// (set) Token: 0x06003246 RID: 12870 RVA: 0x0016FFE4 File Offset: 0x0016E1E4
		public Identifier PoolName
		{
			get
			{
				return this._poolName;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._poolName = value;
			}
		}

		// Token: 0x06003247 RID: 12871 RVA: 0x0016FFF4 File Offset: 0x0016E1F4
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.Name != null)
			{
				this.Name.Accept(visitor);
			}
			int i = 0;
			int count = this.WorkloadGroupParameters.Count;
			while (i < count)
			{
				this.WorkloadGroupParameters[i].Accept(visitor);
				i++;
			}
			if (this.PoolName != null)
			{
				this.PoolName.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001EA0 RID: 7840
		private Identifier _name;

		// Token: 0x04001EA1 RID: 7841
		private List<WorkloadGroupParameter> _workloadGroupParameters = new List<WorkloadGroupParameter>();

		// Token: 0x04001EA2 RID: 7842
		private Identifier _poolName;
	}
}
