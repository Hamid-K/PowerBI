using System;
using System.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x0200017D RID: 381
	internal sealed class DataSourceResolverCancelationHelper<T> where T : class
	{
		// Token: 0x06000DF2 RID: 3570 RVA: 0x00032E9C File Offset: 0x0003109C
		internal DataSourceResolverCancelationHelper(Action<T> cancelAction)
		{
			this.m_cancelAction = cancelAction;
			this.m_syncRoot = new object();
		}

		// Token: 0x06000DF3 RID: 3571 RVA: 0x00032EB8 File Offset: 0x000310B8
		internal void SetCancelationTarget(T cancelationTarget)
		{
			object syncRoot = this.m_syncRoot;
			lock (syncRoot)
			{
				RSTrace.CatalogTrace.Assert(this.m_cancelationTarget == null, "A cancelation target can only be set if none exists. The cancelation helper cannot be used by multiple threads at the same time and has to be reset before being used again.");
				this.m_cancelationTarget = cancelationTarget;
			}
		}

		// Token: 0x06000DF4 RID: 3572 RVA: 0x00032F18 File Offset: 0x00031118
		internal void ResetAndThrowIfCanceled(ModelRetrievalAbortedException.CancelationTrigger trigger)
		{
			object syncRoot = this.m_syncRoot;
			lock (syncRoot)
			{
				try
				{
					this.ThrowIfCanceledInternal(trigger);
				}
				finally
				{
					this.m_cancelationTarget = default(T);
					this.m_canceled = false;
				}
			}
		}

		// Token: 0x06000DF5 RID: 3573 RVA: 0x00032F7C File Offset: 0x0003117C
		internal void Cancel()
		{
			object syncRoot = this.m_syncRoot;
			lock (syncRoot)
			{
				if (!this.m_canceled)
				{
					this.m_canceled = true;
					if (this.m_cancelationTarget != null)
					{
						try
						{
							this.m_cancelAction(this.m_cancelationTarget);
						}
						catch (Exception ex)
						{
							RSTrace.CatalogTrace.Trace(TraceLevel.Verbose, "Ignored exception thrown during cancelation: " + ex.Message);
						}
						this.m_cancelationTarget = default(T);
					}
				}
			}
		}

		// Token: 0x06000DF6 RID: 3574 RVA: 0x0003301C File Offset: 0x0003121C
		internal void ThrowIfCanceled(ModelRetrievalAbortedException.CancelationTrigger trigger)
		{
			object syncRoot = this.m_syncRoot;
			lock (syncRoot)
			{
				this.ThrowIfCanceledInternal(trigger);
			}
		}

		// Token: 0x06000DF7 RID: 3575 RVA: 0x00033060 File Offset: 0x00031260
		private void ThrowIfCanceledInternal(ModelRetrievalAbortedException.CancelationTrigger trigger)
		{
			if (this.m_canceled)
			{
				throw new ModelRetrievalAbortedException(trigger);
			}
		}

		// Token: 0x040005BB RID: 1467
		private readonly Action<T> m_cancelAction;

		// Token: 0x040005BC RID: 1468
		private readonly object m_syncRoot;

		// Token: 0x040005BD RID: 1469
		private T m_cancelationTarget;

		// Token: 0x040005BE RID: 1470
		private bool m_canceled;
	}
}
