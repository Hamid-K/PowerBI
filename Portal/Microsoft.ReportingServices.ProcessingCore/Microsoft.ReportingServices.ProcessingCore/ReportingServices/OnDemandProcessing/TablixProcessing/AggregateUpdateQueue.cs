using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.OnDemandProcessing.TablixProcessing
{
	// Token: 0x020008D9 RID: 2265
	public class AggregateUpdateQueue : Queue<AggregateUpdateCollection>
	{
		// Token: 0x06007BCC RID: 31692 RVA: 0x001FCCD7 File Offset: 0x001FAED7
		internal AggregateUpdateQueue(AggregateUpdateCollection originalState)
		{
			this.m_originalState = originalState;
		}

		// Token: 0x17002893 RID: 10387
		// (get) Token: 0x06007BCD RID: 31693 RVA: 0x001FCCE6 File Offset: 0x001FAEE6
		public AggregateUpdateCollection OriginalState
		{
			get
			{
				return this.m_originalState;
			}
		}

		// Token: 0x04003D90 RID: 15760
		private AggregateUpdateCollection m_originalState;
	}
}
