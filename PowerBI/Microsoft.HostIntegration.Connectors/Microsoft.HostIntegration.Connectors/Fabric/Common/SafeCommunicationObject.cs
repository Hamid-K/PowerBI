using System;
using System.ServiceModel;

namespace Microsoft.Fabric.Common
{
	// Token: 0x02000444 RID: 1092
	internal abstract class SafeCommunicationObject<T> : TransportObject where T : class, ICommunicationObject
	{
		// Token: 0x06002640 RID: 9792 RVA: 0x000754CE File Offset: 0x000736CE
		protected SafeCommunicationObject(T wcfObject)
		{
			this.m_wcfObject = wcfObject;
		}

		// Token: 0x17000785 RID: 1925
		// (get) Token: 0x06002641 RID: 9793 RVA: 0x000754DD File Offset: 0x000736DD
		// (set) Token: 0x06002642 RID: 9794 RVA: 0x000754E5 File Offset: 0x000736E5
		protected T WCFObject
		{
			get
			{
				return this.m_wcfObject;
			}
			set
			{
				this.m_wcfObject = value;
			}
		}

		// Token: 0x06002643 RID: 9795 RVA: 0x000754EE File Offset: 0x000736EE
		private void WcfObjectFaulted(object sender, EventArgs args)
		{
			base.Fault(new CommunicationObjectFaultedException("The underlying WCF object faulted"));
		}

		// Token: 0x06002644 RID: 9796 RVA: 0x00075500 File Offset: 0x00073700
		protected override bool OnOpen()
		{
			IAsyncResult asyncResult = this.m_wcfObject.BeginOpen(TimeSpan.MaxValue, new AsyncCallback(SafeCommunicationObject<T>.StaticOpenCallback), this);
			return asyncResult.CompletedSynchronously;
		}

		// Token: 0x06002645 RID: 9797 RVA: 0x00075538 File Offset: 0x00073738
		private static void StaticOpenCallback(IAsyncResult ar)
		{
			SafeCommunicationObject<T> safeCommunicationObject = (SafeCommunicationObject<T>)ar.AsyncState;
			safeCommunicationObject.OpenCallback(ar);
		}

		// Token: 0x06002646 RID: 9798 RVA: 0x00075558 File Offset: 0x00073758
		private void OpenCallback(IAsyncResult ar)
		{
			Exception ex = null;
			try
			{
				this.m_wcfObject.EndOpen(ar);
			}
			catch (Exception ex2)
			{
				if (ar.CompletedSynchronously)
				{
					throw;
				}
				ex = ex2;
			}
			this.CompleteOpen(ar.CompletedSynchronously, ex);
		}

		// Token: 0x06002647 RID: 9799 RVA: 0x000755A8 File Offset: 0x000737A8
		protected void CompleteOpen(bool completedSynchronously, Exception openException)
		{
			EventLogWriter.WriteInfo("SafeCommunicationObject", "{0} open completed {1}", new object[] { this, openException });
			if (!completedSynchronously)
			{
				base.OnOpenCompleted(openException);
			}
			if (openException == null)
			{
				this.m_wcfObject.Faulted += this.WcfObjectFaulted;
				if (this.m_wcfObject.State == CommunicationState.Faulted)
				{
					this.WcfObjectFaulted(this.m_wcfObject, null);
				}
				this.m_wcfObject.Closed += this.WcfObjectClosed;
				if (this.m_wcfObject.State == CommunicationState.Closed)
				{
					this.WcfObjectClosed(this.m_wcfObject, null);
				}
			}
		}

		// Token: 0x06002648 RID: 9800 RVA: 0x00075668 File Offset: 0x00073868
		private void WcfObjectClosed(object sender, EventArgs args)
		{
			base.ForceClosure();
		}

		// Token: 0x06002649 RID: 9801 RVA: 0x00075670 File Offset: 0x00073870
		protected override bool OnClose()
		{
			EventLogWriter.WriteInfo("SafeCommunicationObject", "{0} closing underlying WCF object {1}", new object[] { this, this.m_wcfObject });
			IAsyncResult asyncResult = this.m_wcfObject.BeginClose(TimeSpan.MaxValue, new AsyncCallback(SafeCommunicationObject<T>.StaticCloseCallback), this);
			return asyncResult.CompletedSynchronously;
		}

		// Token: 0x0600264A RID: 9802 RVA: 0x000756D0 File Offset: 0x000738D0
		private static void StaticCloseCallback(IAsyncResult ar)
		{
			SafeCommunicationObject<T> safeCommunicationObject = (SafeCommunicationObject<T>)ar.AsyncState;
			safeCommunicationObject.CloseCallback(ar);
		}

		// Token: 0x0600264B RID: 9803 RVA: 0x000756F0 File Offset: 0x000738F0
		private void CloseCallback(IAsyncResult ar)
		{
			Exception ex = null;
			try
			{
				this.m_wcfObject.EndClose(ar);
			}
			catch (Exception ex2)
			{
				if (ar.CompletedSynchronously)
				{
					throw;
				}
				ex = ex2;
			}
			if (!ar.CompletedSynchronously)
			{
				base.OnCloseCompleted(ex);
			}
		}

		// Token: 0x0600264C RID: 9804 RVA: 0x00075744 File Offset: 0x00073944
		protected override void OnAbort()
		{
			this.m_wcfObject.Abort();
			base.OnAbort();
		}

		// Token: 0x0600264D RID: 9805 RVA: 0x00075760 File Offset: 0x00073960
		protected override void OnFault(Exception faultingException)
		{
			ReleaseAssert.IsTrue<CommunicationState, Exception>(this.m_wcfObject.State == CommunicationState.Faulted || this.m_wcfObject.State == CommunicationState.Closed || this.m_wcfObject.State == CommunicationState.Closing, "Incorrect WCF object state - {0}. FaultingException - {1}", this.m_wcfObject.State, faultingException);
			base.OnFault(faultingException);
		}

		// Token: 0x040016EA RID: 5866
		private const string LogSource = "SafeCommunicationObject";

		// Token: 0x040016EB RID: 5867
		private T m_wcfObject;
	}
}
