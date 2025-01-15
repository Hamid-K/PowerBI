using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020001B0 RID: 432
	[Serializable]
	internal abstract class ExecutableEntity : TSqlFragment
	{
		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x0600222B RID: 8747 RVA: 0x0015F158 File Offset: 0x0015D358
		public IList<ExecuteParameter> Parameters
		{
			get
			{
				return this._parameters;
			}
		}

		// Token: 0x0600222C RID: 8748 RVA: 0x0015F160 File Offset: 0x0015D360
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			int i = 0;
			int count = this.Parameters.Count;
			while (i < count)
			{
				this.Parameters[i].Accept(visitor);
				i++;
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001A13 RID: 6675
		private List<ExecuteParameter> _parameters = new List<ExecuteParameter>();
	}
}
