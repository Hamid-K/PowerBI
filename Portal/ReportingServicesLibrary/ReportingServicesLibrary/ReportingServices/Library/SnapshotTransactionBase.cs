using System;
using System.Diagnostics;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x0200028B RID: 651
	internal abstract class SnapshotTransactionBase : ISnapshotTransaction, IDisposable
	{
		// Token: 0x060017AE RID: 6062 RVA: 0x0005FB3C File Offset: 0x0005DD3C
		public void Dispose()
		{
			if (!this.m_finished)
			{
				this.Rollback();
			}
		}

		// Token: 0x170006BC RID: 1724
		// (get) Token: 0x060017AF RID: 6063 RVA: 0x0005FB4C File Offset: 0x0005DD4C
		// (set) Token: 0x060017B0 RID: 6064 RVA: 0x0005FB54 File Offset: 0x0005DD54
		public bool IsRootTransaction
		{
			[DebuggerStepThrough]
			get
			{
				return this.m_isRoot;
			}
			[DebuggerStepThrough]
			set
			{
				this.m_isRoot = value;
			}
		}

		// Token: 0x060017B1 RID: 6065 RVA: 0x0005FB60 File Offset: 0x0005DD60
		public void Commit()
		{
			if (this.IsRootTransaction && !this.m_finished)
			{
				try
				{
					this.CloseStreams();
					this.CommitInternal();
				}
				finally
				{
					this.m_finished = true;
				}
			}
		}

		// Token: 0x060017B2 RID: 6066 RVA: 0x0005FBA4 File Offset: 0x0005DDA4
		public void Rollback()
		{
			if (this.IsRootTransaction && !this.m_finished)
			{
				try
				{
					this.CloseStreams();
					this.RollbackInternal();
				}
				finally
				{
					this.m_finished = true;
				}
			}
		}

		// Token: 0x060017B3 RID: 6067
		protected abstract void CloseStreams();

		// Token: 0x060017B4 RID: 6068
		protected abstract void CommitInternal();

		// Token: 0x060017B5 RID: 6069
		protected abstract void RollbackInternal();

		// Token: 0x0400088E RID: 2190
		protected bool m_finished;

		// Token: 0x0400088F RID: 2191
		protected bool m_isRoot;
	}
}
