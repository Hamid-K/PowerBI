using System;
using Microsoft.ReportingServices.Common;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020002A0 RID: 672
	internal abstract class InternalDynamicMemberLogic
	{
		// Token: 0x060019E8 RID: 6632
		public abstract bool MoveNext();

		// Token: 0x060019E9 RID: 6633 RVA: 0x00069017 File Offset: 0x00067217
		public int GetInstanceIndex()
		{
			return this.m_currentContext;
		}

		// Token: 0x060019EA RID: 6634
		public abstract bool SetInstanceIndex(int index);

		// Token: 0x060019EB RID: 6635
		internal abstract ScopeID GetScopeID();

		// Token: 0x060019EC RID: 6636
		internal abstract ScopeID GetLastScopeID();

		// Token: 0x060019ED RID: 6637
		internal abstract void SetScopeID(ScopeID scopeID);

		// Token: 0x060019EE RID: 6638
		public abstract void ResetContext();

		// Token: 0x17000EDE RID: 3806
		// (get) Token: 0x060019EF RID: 6639 RVA: 0x0006901F File Offset: 0x0006721F
		// (set) Token: 0x060019F0 RID: 6640 RVA: 0x00069027 File Offset: 0x00067227
		public bool IsNewContext
		{
			get
			{
				return this.m_isNewContext;
			}
			set
			{
				this.m_isNewContext = value;
			}
		}

		// Token: 0x04000CF1 RID: 3313
		protected int m_currentContext = -1;

		// Token: 0x04000CF2 RID: 3314
		protected bool m_isNewContext = true;
	}
}
