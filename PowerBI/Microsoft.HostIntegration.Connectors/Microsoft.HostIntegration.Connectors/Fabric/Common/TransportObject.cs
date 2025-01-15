using System;
using System.ServiceModel;
using System.Threading;

namespace Microsoft.Fabric.Common
{
	// Token: 0x0200043E RID: 1086
	internal abstract class TransportObject : ITransportObject
	{
		// Token: 0x14000039 RID: 57
		// (add) Token: 0x060025C8 RID: 9672 RVA: 0x00073936 File Offset: 0x00071B36
		// (remove) Token: 0x060025C9 RID: 9673 RVA: 0x0007394F File Offset: 0x00071B4F
		private event EventHandler m_eventHandler;

		// Token: 0x060025CA RID: 9674 RVA: 0x00073968 File Offset: 0x00071B68
		protected TransportObject()
		{
			this.m_lock = new object();
			this.m_state = TransportState.Created;
			this.m_openCount = 0;
			this.m_eventHandler = null;
			this.m_faultingException = null;
			this.m_openContext = null;
			this.m_closeContext = null;
		}

		// Token: 0x17000773 RID: 1907
		// (get) Token: 0x060025CB RID: 9675 RVA: 0x000739A5 File Offset: 0x00071BA5
		// (set) Token: 0x060025CC RID: 9676 RVA: 0x000739AD File Offset: 0x00071BAD
		public object Context
		{
			get
			{
				return this.m_context;
			}
			set
			{
				this.m_context = value;
			}
		}

		// Token: 0x17000774 RID: 1908
		// (get) Token: 0x060025CD RID: 9677 RVA: 0x000739B6 File Offset: 0x00071BB6
		protected object ThisLock
		{
			get
			{
				return this.m_lock;
			}
		}

		// Token: 0x060025CE RID: 9678 RVA: 0x000739BE File Offset: 0x00071BBE
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x060025CF RID: 9679 RVA: 0x000739C7 File Offset: 0x00071BC7
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.Close();
			}
		}

		// Token: 0x17000775 RID: 1909
		// (get) Token: 0x060025D0 RID: 9680 RVA: 0x000739D2 File Offset: 0x00071BD2
		public bool IsValid
		{
			get
			{
				return this.m_state == TransportState.Opened;
			}
		}

		// Token: 0x17000776 RID: 1910
		// (get) Token: 0x060025D1 RID: 9681 RVA: 0x000739DD File Offset: 0x00071BDD
		public bool IsClosed
		{
			get
			{
				return this.m_state == TransportState.Closing || this.m_state == TransportState.Closed;
			}
		}

		// Token: 0x17000777 RID: 1911
		// (get) Token: 0x060025D2 RID: 9682 RVA: 0x000739F3 File Offset: 0x00071BF3
		public bool IsDead
		{
			get
			{
				return this.m_state == TransportState.Closing || this.m_state == TransportState.Closed || this.m_state == TransportState.Aborted || this.m_state == TransportState.Faulted;
			}
		}

		// Token: 0x060025D3 RID: 9683 RVA: 0x00073A1B File Offset: 0x00071C1B
		public int AddReference()
		{
			return Interlocked.Increment(ref this.m_references);
		}

		// Token: 0x060025D4 RID: 9684 RVA: 0x00073A28 File Offset: 0x00071C28
		public int ReleaseReference()
		{
			bool flag = false;
			int num = this.InternalRelease();
			if (num == 0 && this.m_openCount == 0 && (this.m_state == TransportState.Opened || this.m_state == TransportState.Opening))
			{
				lock (this.m_lock)
				{
					if (this.m_openCount == 0 && this.m_state == TransportState.Opened && this.InitiateClose())
					{
						this.m_state = TransportState.Closing;
						if (this.m_closeContext == null)
						{
							this.m_closeContext = new OperationContext(new AsyncCallback(TransportObject.ImplicitCloseCallback), null, TimeSpan.MaxValue);
						}
						flag = true;
					}
				}
			}
			if (flag)
			{
				this.PerformClose(true);
			}
			return num;
		}

		// Token: 0x17000778 RID: 1912
		// (get) Token: 0x060025D5 RID: 9685 RVA: 0x00073AD4 File Offset: 0x00071CD4
		public int References
		{
			get
			{
				return this.m_references;
			}
		}

		// Token: 0x060025D6 RID: 9686 RVA: 0x00073ADC File Offset: 0x00071CDC
		private int InternalRelease()
		{
			int i = this.m_references;
			while (i > 0)
			{
				int num = i;
				i = Interlocked.CompareExchange(ref this.m_references, num - 1, num);
				if (i == num)
				{
					break;
				}
			}
			return i;
		}

		// Token: 0x060025D7 RID: 9687 RVA: 0x00073B0D File Offset: 0x00071D0D
		public void WaitForOpen()
		{
			this.EndWaitForOpen(this.BeginWaitForOpen(null, null));
		}

		// Token: 0x060025D8 RID: 9688 RVA: 0x00073B20 File Offset: 0x00071D20
		public IAsyncResult BeginWaitForOpen(AsyncCallback callback, object state)
		{
			bool flag = false;
			bool flag2 = false;
			OperationContext operationContext = null;
			lock (this.m_lock)
			{
				switch (this.m_state)
				{
				case TransportState.Created:
					ReleaseAssert.IsTrue(this.m_openContext == null || !this.m_openContext.IsCompleted);
					if (this.m_openContext == null)
					{
						this.m_openContext = new OperationContext(callback, state, TimeSpan.MaxValue);
						flag2 = true;
					}
					break;
				case TransportState.Opening:
					ReleaseAssert.IsTrue(this.m_openContext != null);
					break;
				default:
					flag = this.m_openContext == null;
					break;
				}
				operationContext = this.m_openContext;
			}
			if (flag)
			{
				return new SynchronousCompletionOperationContext(callback, state);
			}
			if (!flag2)
			{
				return operationContext.CreateSecondaryContext(callback, state, TimeSpan.MaxValue);
			}
			return operationContext;
		}

		// Token: 0x060025D9 RID: 9689 RVA: 0x00073BF0 File Offset: 0x00071DF0
		public void EndWaitForOpen(IAsyncResult ar)
		{
			OperationContext operationContext = (OperationContext)ar;
			try
			{
				operationContext.End();
			}
			catch (Exception ex)
			{
				if (!Utility.IsCommunicationException(ex))
				{
					throw;
				}
			}
		}

		// Token: 0x060025DA RID: 9690 RVA: 0x00073C2C File Offset: 0x00071E2C
		public void WaitForClose()
		{
			this.EndWaitForClose(this.BeginWaitForClose(null, null));
		}

		// Token: 0x060025DB RID: 9691 RVA: 0x00073C3C File Offset: 0x00071E3C
		public IAsyncResult BeginWaitForClose(AsyncCallback callback, object state)
		{
			bool flag = false;
			bool flag2 = false;
			OperationContext operationContext = null;
			lock (this.m_lock)
			{
				switch (this.m_state)
				{
				case TransportState.Created:
				case TransportState.Opening:
				case TransportState.Opened:
					ReleaseAssert.IsTrue(this.m_closeContext == null || !this.m_closeContext.IsCompleted);
					if (this.m_closeContext == null)
					{
						this.m_closeContext = new OperationContext(callback, state, TimeSpan.MaxValue);
						flag2 = true;
					}
					break;
				case TransportState.Closing:
					ReleaseAssert.IsTrue(this.m_closeContext != null);
					break;
				default:
					flag = this.m_closeContext == null;
					break;
				}
				operationContext = this.m_closeContext;
			}
			if (flag)
			{
				return new SynchronousCompletionOperationContext(callback, state);
			}
			if (!flag2)
			{
				return operationContext.CreateSecondaryContext(callback, state, TimeSpan.MaxValue);
			}
			return operationContext;
		}

		// Token: 0x060025DC RID: 9692 RVA: 0x00073D14 File Offset: 0x00071F14
		public void EndWaitForClose(IAsyncResult ar)
		{
			OperationContext operationContext = (OperationContext)ar;
			try
			{
				operationContext.End();
			}
			catch (Exception ex)
			{
				if (!Utility.IsCommunicationException(ex))
				{
					throw;
				}
			}
		}

		// Token: 0x17000779 RID: 1913
		// (get) Token: 0x060025DD RID: 9693 RVA: 0x00073D50 File Offset: 0x00071F50
		public TransportState State
		{
			get
			{
				return this.m_state;
			}
		}

		// Token: 0x1400003A RID: 58
		// (add) Token: 0x060025DE RID: 9694 RVA: 0x00073D58 File Offset: 0x00071F58
		// (remove) Token: 0x060025DF RID: 9695 RVA: 0x00073D66 File Offset: 0x00071F66
		public event EventHandler StateChange
		{
			add
			{
				Utility.AddEventHandler(ref this.m_eventHandler, value);
			}
			remove
			{
				Utility.RemoveEventHandler(ref this.m_eventHandler, value);
			}
		}

		// Token: 0x1700077A RID: 1914
		// (get) Token: 0x060025E0 RID: 9696 RVA: 0x00073D74 File Offset: 0x00071F74
		public Exception FaultingException
		{
			get
			{
				return this.m_faultingException;
			}
		}

		// Token: 0x060025E1 RID: 9697 RVA: 0x000036A9 File Offset: 0x000018A9
		public void PoolAsyncResult(IAsyncResult ar)
		{
		}

		// Token: 0x060025E2 RID: 9698 RVA: 0x00073D7C File Offset: 0x00071F7C
		public void Open()
		{
			this.Open(TimeSpan.MaxValue);
		}

		// Token: 0x060025E3 RID: 9699 RVA: 0x00073D89 File Offset: 0x00071F89
		public void Open(TimeSpan timeout)
		{
			this.EndOpen(this.BeginOpen(timeout, null, null));
		}

		// Token: 0x060025E4 RID: 9700 RVA: 0x00073D9A File Offset: 0x00071F9A
		public IAsyncResult BeginOpen(AsyncCallback callback, object state)
		{
			return this.BeginOpen(TimeSpan.MaxValue, callback, state);
		}

		// Token: 0x060025E5 RID: 9701 RVA: 0x00073DAC File Offset: 0x00071FAC
		public IAsyncResult BeginOpen(TimeSpan timeout, AsyncCallback callback, object state)
		{
			OperationContext operationContext = null;
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			bool flag4 = false;
			lock (this.m_lock)
			{
				switch (this.m_state)
				{
				case TransportState.Created:
					ReleaseAssert.IsTrue(this.m_openContext == null || !this.m_openContext.IsCompleted);
					this.m_state = TransportState.Opening;
					if (this.m_openContext == null)
					{
						if (timeout == TimeSpan.MaxValue)
						{
							this.m_openContext = new OperationContext(callback, state, timeout);
						}
						else
						{
							this.m_openContext = new OperationContext(new AsyncCallback(TransportObject.ImplicitOpenCallback), this, TimeSpan.MaxValue);
							flag2 = true;
						}
					}
					else
					{
						flag2 = true;
					}
					flag3 = true;
					break;
				case TransportState.Opening:
					ReleaseAssert.IsTrue(this.m_openContext != null);
					break;
				case TransportState.Opened:
					ReleaseAssert.IsTrue(this.m_openContext == null);
					flag = true;
					break;
				default:
					flag4 = true;
					break;
				}
				if (!flag4)
				{
					operationContext = this.m_openContext;
					this.m_openCount++;
				}
			}
			if (flag4)
			{
				return new SynchronousCompletionOperationContext(callback, state);
			}
			if (flag)
			{
				operationContext = new OperationContext(callback, state, TimeSpan.MaxValue);
				operationContext.CompleteOperation(true, null);
				return operationContext;
			}
			if (!flag3)
			{
				OperationContext operationContext2 = operationContext.CreateSecondaryContext(callback, state, timeout);
				operationContext2.StartTimer();
				return operationContext2;
			}
			this.PerformOpen(true);
			if (!flag2)
			{
				return operationContext;
			}
			OperationContext operationContext3 = operationContext.CreateSecondaryContext(callback, state, timeout);
			operationContext3.StartTimer();
			return operationContext3;
		}

		// Token: 0x060025E6 RID: 9702 RVA: 0x00073F1C File Offset: 0x0007211C
		public void EndOpen(IAsyncResult ar)
		{
			OperationContext operationContext = (OperationContext)ar;
			try
			{
				operationContext.End();
			}
			catch
			{
				this.ProcessEndOpen(true, operationContext);
				throw;
			}
			this.ProcessEndOpen(false, operationContext);
		}

		// Token: 0x060025E7 RID: 9703 RVA: 0x00073F5C File Offset: 0x0007215C
		public void Close()
		{
			this.Close(TimeSpan.MaxValue);
		}

		// Token: 0x060025E8 RID: 9704 RVA: 0x00073F69 File Offset: 0x00072169
		public void Close(TimeSpan timeout)
		{
			this.EndClose(this.BeginClose(timeout, null, null));
		}

		// Token: 0x060025E9 RID: 9705 RVA: 0x00073F7A File Offset: 0x0007217A
		public IAsyncResult BeginClose(AsyncCallback callback, object state)
		{
			return this.BeginClose(TimeSpan.MaxValue, callback, state);
		}

		// Token: 0x060025EA RID: 9706 RVA: 0x00073F8C File Offset: 0x0007218C
		public IAsyncResult BeginClose(TimeSpan timeout, AsyncCallback callback, object state)
		{
			ReleaseAssert.IsTrue(this.m_openContext == null && this.m_state != TransportState.Created && this.m_state != TransportState.Opening);
			bool flag = true;
			OperationContext operationContext = null;
			bool flag2 = false;
			bool flag3 = false;
			lock (this.m_lock)
			{
				if (this.m_openCount > 0)
				{
					this.m_openCount--;
					if (this.m_openCount == 0 && this.m_state == TransportState.Opened && this.m_references == 0 && this.InitiateClose())
					{
						this.m_state = TransportState.Closing;
						if (this.m_closeContext == null)
						{
							if (timeout == TimeSpan.MaxValue)
							{
								this.m_closeContext = new OperationContext(callback, state, timeout);
							}
							else
							{
								this.m_closeContext = new OperationContext(new AsyncCallback(TransportObject.ImplicitCloseCallback), null, TimeSpan.MaxValue);
								flag3 = true;
							}
						}
						else
						{
							flag3 = true;
						}
						operationContext = this.m_closeContext;
						flag = false;
						flag2 = true;
					}
				}
				else if (this.m_state != TransportState.Closed)
				{
					throw new InvalidOperationException("Close without a matching successful Open with state: " + this.m_state);
				}
			}
			if (flag)
			{
				return new SynchronousCompletionOperationContext(callback, state);
			}
			if (flag2)
			{
				this.PerformClose(true);
			}
			if (!flag3)
			{
				return operationContext;
			}
			OperationContext operationContext2 = operationContext.CreateSecondaryContext(callback, state, timeout);
			operationContext2.StartTimer();
			return operationContext2;
		}

		// Token: 0x060025EB RID: 9707 RVA: 0x000740E4 File Offset: 0x000722E4
		public void EndClose(IAsyncResult ar)
		{
			OperationContext operationContext = (OperationContext)ar;
			operationContext.End();
		}

		// Token: 0x060025EC RID: 9708 RVA: 0x00074100 File Offset: 0x00072300
		public void ForceClosure()
		{
			bool flag = false;
			lock (this.m_lock)
			{
				flag = this.m_state == TransportState.Opened;
				if (this.m_state == TransportState.Opening || flag)
				{
					this.m_state = TransportState.Closing;
					if (this.m_closeContext == null)
					{
						this.m_closeContext = new OperationContext(new AsyncCallback(TransportObject.ImplicitCloseCallback), null, TimeSpan.MaxValue);
					}
				}
			}
			if (flag)
			{
				this.PerformClose(false);
			}
		}

		// Token: 0x060025ED RID: 9709 RVA: 0x00074184 File Offset: 0x00072384
		public void Abort()
		{
			Exception ex = new CommunicationObjectAbortedException();
			OperationContext openContext;
			OperationContext closeContext;
			lock (this.m_lock)
			{
				switch (this.m_state)
				{
				case TransportState.Closed:
				case TransportState.Aborted:
				case TransportState.Faulted:
					return;
				default:
					this.m_state = TransportState.Aborted;
					this.m_faultingException = ex;
					openContext = this.m_openContext;
					this.m_openContext = null;
					closeContext = this.m_closeContext;
					this.m_closeContext = null;
					break;
				}
			}
			this.OnAbort();
			this.InvokeCallbacks(openContext, false, null, closeContext, false, null, TransportObject.AbortedEvent);
		}

		// Token: 0x060025EE RID: 9710
		protected abstract bool OnOpen();

		// Token: 0x060025EF RID: 9711 RVA: 0x00074220 File Offset: 0x00072420
		protected virtual void OnOpened(Exception openException)
		{
			if (openException != null)
			{
				this.OnCleanup(openException);
			}
		}

		// Token: 0x060025F0 RID: 9712 RVA: 0x0007422C File Offset: 0x0007242C
		protected void OnOpenCompleted(Exception openException)
		{
			this.OnOpenCompleted(openException, false);
		}

		// Token: 0x060025F1 RID: 9713 RVA: 0x00074238 File Offset: 0x00072438
		protected void OnOpenCompleted(Exception openException, bool completedSynchronously)
		{
			bool flag = false;
			bool flag2 = false;
			OperationContext operationContext = null;
			OperationContext operationContext2 = null;
			lock (this.m_lock)
			{
				operationContext2 = this.m_openContext;
				this.m_openContext = null;
				if (this.m_state == TransportState.Opening)
				{
					flag2 = true;
					if (openException == null)
					{
						if (this.m_openCount > 0 || this.m_references > 0 || !this.InitiateClose())
						{
							this.m_state = TransportState.Opened;
						}
						else
						{
							if (this.m_closeContext == null)
							{
								this.m_closeContext = new OperationContext(new AsyncCallback(TransportObject.ImplicitCloseCallback), null, TimeSpan.MaxValue);
							}
							this.m_state = TransportState.Closing;
							flag = true;
						}
					}
					else
					{
						this.m_state = TransportState.Faulted;
						this.m_faultingException = openException;
						operationContext = this.m_closeContext;
						this.m_closeContext = null;
					}
				}
				else if (openException == null)
				{
					flag = this.m_state == TransportState.Closing;
					flag2 = true;
				}
				else
				{
					ReleaseAssert.IsTrue(this.m_state == TransportState.Aborted || this.m_state == TransportState.Faulted);
					operationContext = this.m_closeContext;
					this.m_closeContext = null;
					if (this.m_state == TransportState.Faulted && this.m_faultingException is CommunicationObjectFaultedException)
					{
						this.m_faultingException = openException;
					}
				}
			}
			this.OnOpened(openException);
			EventArgs eventArgs = (flag2 ? ((openException == null) ? TransportObject.OpenedEvent : TransportObject.FaultedEvent) : null);
			this.InvokeCallbacks(operationContext2, completedSynchronously, openException, operationContext, false, null, eventArgs);
			if (flag)
			{
				this.PerformClose(false);
			}
		}

		// Token: 0x060025F2 RID: 9714 RVA: 0x00074394 File Offset: 0x00072594
		private void InvokeCallbacks(OperationContext openContext, bool openCompletedSynchronously, Exception openException, OperationContext closeContext, bool closeCompletedSynchronously, Exception closeException, EventArgs eventType)
		{
			if (openContext != null)
			{
				ReleaseAssert.IsTrue(openContext.ExpirationTime == FileTime.MaxValue);
				openContext.CompleteOperation(openCompletedSynchronously, openException);
			}
			if (closeContext != null)
			{
				ReleaseAssert.IsTrue(closeContext.ExpirationTime == FileTime.MaxValue);
				closeContext.CompleteOperation(closeCompletedSynchronously, closeException);
			}
			if (eventType != null)
			{
				EventHandler eventHandler = this.m_eventHandler;
				if (eventHandler != null)
				{
					eventHandler(this, eventType);
				}
			}
		}

		// Token: 0x060025F3 RID: 9715 RVA: 0x000036A9 File Offset: 0x000018A9
		protected virtual void OnCleanup(Exception faultingException)
		{
		}

		// Token: 0x060025F4 RID: 9716 RVA: 0x00002B16 File Offset: 0x00000D16
		protected virtual bool InitiateClose()
		{
			return true;
		}

		// Token: 0x060025F5 RID: 9717
		protected abstract bool OnClose();

		// Token: 0x060025F6 RID: 9718 RVA: 0x000743FD File Offset: 0x000725FD
		protected virtual void OnClosed(Exception closeException)
		{
			this.OnCleanup(closeException);
		}

		// Token: 0x060025F7 RID: 9719 RVA: 0x00074406 File Offset: 0x00072606
		protected void OnCloseCompleted(Exception closeException)
		{
			this.OnCloseCompleted(closeException, false);
		}

		// Token: 0x060025F8 RID: 9720 RVA: 0x00074410 File Offset: 0x00072610
		protected void OnCloseCompleted(Exception closeException, bool completedSynchronously)
		{
			bool flag = false;
			OperationContext closeContext;
			lock (this.m_lock)
			{
				closeContext = this.m_closeContext;
				this.m_closeContext = null;
				if (this.m_state == TransportState.Closing)
				{
					flag = true;
					if (closeException == null)
					{
						this.m_state = TransportState.Closed;
					}
					else
					{
						this.m_state = TransportState.Faulted;
						this.m_faultingException = closeException;
					}
				}
				else if (this.m_state == TransportState.Faulted && closeException != null && this.m_faultingException is CommunicationObjectFaultedException)
				{
					this.m_faultingException = closeException;
				}
			}
			this.OnClosed(closeException);
			if (closeContext != null)
			{
				ReleaseAssert.IsTrue(closeContext.ExpirationTime == FileTime.MaxValue);
				closeContext.CompleteOperation(completedSynchronously, closeException);
			}
			if (flag)
			{
				EventHandler eventHandler = this.m_eventHandler;
				EventArgs eventArgs = ((closeException == null) ? TransportObject.ClosedEvent : TransportObject.FaultedEvent);
				if (eventHandler != null)
				{
					eventHandler(this, eventArgs);
				}
			}
		}

		// Token: 0x060025F9 RID: 9721 RVA: 0x000744E8 File Offset: 0x000726E8
		protected virtual void OnAbort()
		{
			this.OnCleanup(this.m_faultingException);
		}

		// Token: 0x060025FA RID: 9722 RVA: 0x000743FD File Offset: 0x000725FD
		protected virtual void OnFault(Exception faultingException)
		{
			this.OnCleanup(faultingException);
		}

		// Token: 0x060025FB RID: 9723 RVA: 0x000744F8 File Offset: 0x000726F8
		public void Fault(Exception faultingException)
		{
			OperationContext openContext;
			OperationContext closeContext;
			lock (this.m_lock)
			{
				switch (this.m_state)
				{
				case TransportState.Closing:
					if (faultingException is ObjectDisposedException)
					{
						return;
					}
					break;
				case TransportState.Closed:
				case TransportState.Aborted:
				case TransportState.Faulted:
					return;
				}
				this.m_state = TransportState.Faulted;
				this.m_faultingException = faultingException;
				openContext = this.m_openContext;
				this.m_openContext = null;
				closeContext = this.m_closeContext;
				this.m_closeContext = null;
			}
			this.OnFault(faultingException);
			this.InvokeCallbacks(openContext, false, faultingException, closeContext, false, faultingException, TransportObject.FaultedEvent);
		}

		// Token: 0x060025FC RID: 9724 RVA: 0x0007459C File Offset: 0x0007279C
		protected void ThrowIfInvalid()
		{
			TransportState state = this.m_state;
			if (state == TransportState.Opened)
			{
				return;
			}
			throw this.CreateInvalidTransportException(state);
		}

		// Token: 0x060025FD RID: 9725 RVA: 0x000745BC File Offset: 0x000727BC
		protected void ThrowDeadTransportException()
		{
			throw this.CreateDeadTransportException();
		}

		// Token: 0x060025FE RID: 9726 RVA: 0x000745C4 File Offset: 0x000727C4
		protected Exception CreateDeadTransportException()
		{
			Exception ex;
			switch (this.m_state)
			{
			case TransportState.Closing:
				ex = new ObjectDisposedException(this.ToString(), "Transport object is closing");
				break;
			case TransportState.Closed:
				ex = new ObjectDisposedException(this.ToString(), "Transport object was closed");
				break;
			case TransportState.Aborted:
				ex = new CommunicationObjectAbortedException("Transport object was aborted");
				break;
			case TransportState.Faulted:
				ex = new CommunicationObjectFaultedException("Transport object has faulted", this.m_faultingException);
				break;
			default:
				ReleaseAssert.Fail("Invalid state for dead transport: {0}", new object[] { this.m_state });
				ex = null;
				break;
			}
			return ex;
		}

		// Token: 0x060025FF RID: 9727 RVA: 0x0007465C File Offset: 0x0007285C
		protected Exception CreateInvalidTransportException(TransportState state)
		{
			switch (state)
			{
			case TransportState.Created:
				return new CommunicationException("Tranport object has not been opened");
			case TransportState.Opening:
				return new CommunicationException("Transport object is still opening");
			case TransportState.Closing:
				return new ObjectDisposedException(this.ToString(), "Transport object is closing");
			case TransportState.Closed:
				return new ObjectDisposedException(this.ToString(), "Transport object was closed");
			case TransportState.Aborted:
				return new CommunicationObjectAbortedException("Transport object was aborted");
			case TransportState.Faulted:
				return new CommunicationObjectFaultedException("Transport object has faulted", this.m_faultingException);
			}
			ReleaseAssert.Fail("State {0}", new object[] { this.m_state });
			return null;
		}

		// Token: 0x06002600 RID: 9728 RVA: 0x00074714 File Offset: 0x00072914
		private void PerformOpen(bool isOpeningThread)
		{
			bool flag = true;
			Exception ex = null;
			try
			{
				flag = this.OnOpen();
			}
			catch (Exception ex2)
			{
				ex = ex2;
			}
			if (flag)
			{
				this.OnOpenCompleted(ex, isOpeningThread);
			}
		}

		// Token: 0x06002601 RID: 9729 RVA: 0x00074750 File Offset: 0x00072950
		private void ProcessEndOpen(bool endFailed, OperationContext context)
		{
			Exception ex = null;
			bool flag = false;
			bool flag2 = context is SynchronousCompletionOperationContext;
			this.InternalRelease();
			lock (this.m_lock)
			{
				if (!flag2)
				{
					if (!endFailed)
					{
						ReleaseAssert.IsTrue(this.m_openContext == null);
					}
					else if (this.m_openCount > 0)
					{
						this.m_openCount--;
						if (this.m_openCount == 0 && this.m_state == TransportState.Opened && this.m_references == 0 && this.InitiateClose())
						{
							ReleaseAssert.IsTrue(this.m_openContext == null);
							if (this.m_closeContext == null)
							{
								this.m_closeContext = new OperationContext(new AsyncCallback(TransportObject.ImplicitCloseCallback), this, TimeSpan.MaxValue);
							}
							flag = true;
							this.m_state = TransportState.Closing;
						}
					}
					else
					{
						ex = new InvalidOperationException("Close without a matching successful Open with state: " + this.m_state);
					}
				}
				else
				{
					ReleaseAssert.IsTrue(!endFailed);
					switch (this.m_state)
					{
					case TransportState.Closing:
						ex = new ObjectDisposedException(this.ToString(), "Transport object is closing");
						break;
					case TransportState.Closed:
						ex = new ObjectDisposedException(this.ToString(), "Transport object was closed");
						break;
					case TransportState.Aborted:
						ex = new CommunicationObjectAbortedException("Transport object was aborted");
						break;
					case TransportState.Faulted:
						ex = new CommunicationObjectFaultedException("Transport object has faulted", this.m_faultingException);
						break;
					default:
						ReleaseAssert.Fail("State={0}", new object[] { this.m_state });
						break;
					}
				}
			}
			if (flag)
			{
				this.PerformClose(true);
			}
			if (ex != null)
			{
				throw ex;
			}
		}

		// Token: 0x06002602 RID: 9730 RVA: 0x0007490C File Offset: 0x00072B0C
		private static void ImplicitOpenCallback(IAsyncResult ar)
		{
			OperationContext operationContext = (OperationContext)ar;
			try
			{
				operationContext.End();
			}
			catch (Exception ex)
			{
				if (!Utility.IsCommunicationException(ex))
				{
					throw;
				}
			}
		}

		// Token: 0x06002603 RID: 9731 RVA: 0x00074948 File Offset: 0x00072B48
		private static void ImplicitCloseCallback(IAsyncResult ar)
		{
			OperationContext operationContext = (OperationContext)ar;
			try
			{
				operationContext.End();
			}
			catch (Exception ex)
			{
				if (!Utility.IsCommunicationException(ex))
				{
					throw;
				}
			}
		}

		// Token: 0x06002604 RID: 9732 RVA: 0x00074984 File Offset: 0x00072B84
		private void PerformClose(bool isClosingThread)
		{
			Exception ex = null;
			bool flag = true;
			try
			{
				flag = this.OnClose();
			}
			catch (Exception ex2)
			{
				ex = ex2;
			}
			if (flag)
			{
				this.OnCloseCompleted(ex, isClosingThread);
			}
		}

		// Token: 0x06002605 RID: 9733 RVA: 0x000749C0 File Offset: 0x00072BC0
		public static void CloseTransportAsynchronously(ITransportObject transport)
		{
			if (transport.State == TransportState.Faulted || transport.State == TransportState.Opening)
			{
				transport.Abort();
				return;
			}
			if (transport.State == TransportState.Opened)
			{
				transport.BeginClose(TimeSpan.MaxValue, new AsyncCallback(TransportObject.StaticCloseTransportCallback), transport);
			}
		}

		// Token: 0x06002606 RID: 9734 RVA: 0x00074A00 File Offset: 0x00072C00
		private static void StaticCloseTransportCallback(IAsyncResult ar)
		{
			ITransportObject transportObject = (ITransportObject)ar.AsyncState;
			try
			{
				transportObject.EndClose(ar);
			}
			catch (Exception ex)
			{
				if (!Utility.IsCommunicationException(ex))
				{
					throw;
				}
			}
		}

		// Token: 0x040016CD RID: 5837
		public static readonly EventArgs OpenedEvent = new EventArgs();

		// Token: 0x040016CE RID: 5838
		public static readonly EventArgs ClosedEvent = new EventArgs();

		// Token: 0x040016CF RID: 5839
		public static readonly EventArgs AbortedEvent = new EventArgs();

		// Token: 0x040016D0 RID: 5840
		public static readonly EventArgs FaultedEvent = new EventArgs();

		// Token: 0x040016D1 RID: 5841
		private object m_lock;

		// Token: 0x040016D2 RID: 5842
		private TransportState m_state;

		// Token: 0x040016D3 RID: 5843
		private int m_openCount;

		// Token: 0x040016D4 RID: 5844
		private int m_references;

		// Token: 0x040016D6 RID: 5846
		private Exception m_faultingException;

		// Token: 0x040016D7 RID: 5847
		private OperationContext m_openContext;

		// Token: 0x040016D8 RID: 5848
		private OperationContext m_closeContext;

		// Token: 0x040016D9 RID: 5849
		private object m_context;
	}
}
