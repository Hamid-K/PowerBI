using System;
using System.ServiceModel;

namespace Microsoft.Fabric.Common
{
	// Token: 0x020003FA RID: 1018
	internal sealed class ChainedCloseContext : OperationContext
	{
		// Token: 0x060023A2 RID: 9122 RVA: 0x0006D368 File Offset: 0x0006B568
		public ChainedCloseContext(ICommunicationObject object1, ICommunicationObject object2, AsyncCallback callback, object state, TimeSpan timeout)
			: base(callback, state, timeout)
		{
			if (object1 == null)
			{
				throw new ArgumentNullException("object1");
			}
			if (object2 == null)
			{
				throw new ArgumentNullException("object2");
			}
			this.m_object1 = object1;
			this.m_object2 = object2;
			base.StartTimer();
			IAsyncResult asyncResult = this.m_object1.BeginClose(timeout, ChainedCloseContext.Callback1, this);
			if (asyncResult.CompletedSynchronously)
			{
				this.End1(asyncResult);
			}
		}

		// Token: 0x060023A3 RID: 9123 RVA: 0x0006D3D4 File Offset: 0x0006B5D4
		private static void StaticCallback1(IAsyncResult ar)
		{
			if (ar.CompletedSynchronously)
			{
				return;
			}
			ChainedCloseContext chainedCloseContext = (ChainedCloseContext)ar.AsyncState;
			chainedCloseContext.End1(ar);
		}

		// Token: 0x060023A4 RID: 9124 RVA: 0x0006D400 File Offset: 0x0006B600
		private void End1(IAsyncResult ar)
		{
			try
			{
				this.m_object1.EndClose(ar);
				IAsyncResult asyncResult = this.m_object2.BeginClose(base.ExpirationTime.SafeRemainingDuration, ChainedCloseContext.Callback2, this);
				if (asyncResult.CompletedSynchronously)
				{
					this.End2(asyncResult, ar.CompletedSynchronously);
				}
			}
			catch (Exception ex)
			{
				base.CompleteOperation(ar.CompletedSynchronously, ex);
			}
		}

		// Token: 0x060023A5 RID: 9125 RVA: 0x0006D470 File Offset: 0x0006B670
		private static void StaticCallback2(IAsyncResult ar)
		{
			if (ar.CompletedSynchronously)
			{
				return;
			}
			ChainedCloseContext chainedCloseContext = (ChainedCloseContext)ar.AsyncState;
			chainedCloseContext.End2(ar, false);
		}

		// Token: 0x060023A6 RID: 9126 RVA: 0x0006D49C File Offset: 0x0006B69C
		private void End2(IAsyncResult ar, bool completedSynchronously)
		{
			Exception ex = null;
			try
			{
				this.m_object2.EndClose(ar);
			}
			catch (Exception ex2)
			{
				ex = ex2;
			}
			base.CompleteOperation(completedSynchronously, ex);
		}

		// Token: 0x04001622 RID: 5666
		private ICommunicationObject m_object1;

		// Token: 0x04001623 RID: 5667
		private ICommunicationObject m_object2;

		// Token: 0x04001624 RID: 5668
		private static AsyncCallback Callback1 = new AsyncCallback(ChainedCloseContext.StaticCallback1);

		// Token: 0x04001625 RID: 5669
		private static AsyncCallback Callback2 = new AsyncCallback(ChainedCloseContext.StaticCallback2);
	}
}
