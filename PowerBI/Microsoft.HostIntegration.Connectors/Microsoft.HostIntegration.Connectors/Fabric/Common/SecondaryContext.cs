using System;

namespace Microsoft.Fabric.Common
{
	// Token: 0x020003F6 RID: 1014
	internal class SecondaryContext : OperationContext
	{
		// Token: 0x06002388 RID: 9096 RVA: 0x0006CFCF File Offset: 0x0006B1CF
		public SecondaryContext(OperationContext primaryContext, AsyncCallback callback, object state, TimeSpan timeout)
			: base(callback, state, timeout)
		{
			this.m_primaryContext = primaryContext;
		}

		// Token: 0x17000727 RID: 1831
		// (get) Token: 0x06002389 RID: 9097 RVA: 0x0006CFE2 File Offset: 0x0006B1E2
		public OperationContext PrimaryContext
		{
			get
			{
				return this.m_primaryContext;
			}
		}

		// Token: 0x04001617 RID: 5655
		private OperationContext m_primaryContext;
	}
}
