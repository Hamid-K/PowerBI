using System;
using System.ServiceModel;

namespace Microsoft.Fabric.Common
{
	// Token: 0x02000419 RID: 1049
	internal abstract class SharedCommunicationObject : ISharedCommunicationObject, ICommunicationObject
	{
		// Token: 0x0600246A RID: 9322 RVA: 0x0006F9D4 File Offset: 0x0006DBD4
		protected SharedCommunicationObject()
		{
			this.m_state = CommunicationState.Created;
			this.m_isDisposed = false;
			this.m_isAborted = false;
			this.m_isFailed = false;
			this.m_refCount = 0;
			this.m_openContext = null;
			this.m_closeContext = null;
			this.m_faultEventArgs = null;
			this.m_lockObject = new object();
		}

		// Token: 0x17000740 RID: 1856
		// (get) Token: 0x0600246B RID: 9323
		protected abstract TimeSpan DefaultCloseTimeout { get; }

		// Token: 0x17000741 RID: 1857
		// (get) Token: 0x0600246C RID: 9324
		protected abstract TimeSpan DefaultOpenTimeout { get; }

		// Token: 0x0600246D RID: 9325
		protected abstract bool OnOpen();

		// Token: 0x0600246E RID: 9326
		protected abstract bool OnClose();

		// Token: 0x0600246F RID: 9327
		protected abstract void OnAbort();

		// Token: 0x14000032 RID: 50
		// (add) Token: 0x06002470 RID: 9328 RVA: 0x0006FA2A File Offset: 0x0006DC2A
		// (remove) Token: 0x06002471 RID: 9329 RVA: 0x0006FA43 File Offset: 0x0006DC43
		public event EventHandler Opening;

		// Token: 0x14000033 RID: 51
		// (add) Token: 0x06002472 RID: 9330 RVA: 0x0006FA5C File Offset: 0x0006DC5C
		// (remove) Token: 0x06002473 RID: 9331 RVA: 0x0006FA75 File Offset: 0x0006DC75
		public event EventHandler Opened;

		// Token: 0x14000034 RID: 52
		// (add) Token: 0x06002474 RID: 9332 RVA: 0x0006FA8E File Offset: 0x0006DC8E
		// (remove) Token: 0x06002475 RID: 9333 RVA: 0x0006FAA7 File Offset: 0x0006DCA7
		public event EventHandler Closing;

		// Token: 0x14000035 RID: 53
		// (add) Token: 0x06002476 RID: 9334 RVA: 0x0006FAC0 File Offset: 0x0006DCC0
		// (remove) Token: 0x06002477 RID: 9335 RVA: 0x0006FAD9 File Offset: 0x0006DCD9
		public event EventHandler Closed;

		// Token: 0x14000036 RID: 54
		// (add) Token: 0x06002478 RID: 9336 RVA: 0x0006FAF2 File Offset: 0x0006DCF2
		// (remove) Token: 0x06002479 RID: 9337 RVA: 0x0006FB0B File Offset: 0x0006DD0B
		public event EventHandler Faulted;

		// Token: 0x14000037 RID: 55
		// (add) Token: 0x0600247A RID: 9338 RVA: 0x0006FB24 File Offset: 0x0006DD24
		// (remove) Token: 0x0600247B RID: 9339 RVA: 0x0006FB3D File Offset: 0x0006DD3D
		public event EventHandler Failed;

		// Token: 0x17000742 RID: 1858
		// (get) Token: 0x0600247C RID: 9340 RVA: 0x0006FB56 File Offset: 0x0006DD56
		public CommunicationState State
		{
			get
			{
				return this.m_state;
			}
		}

		// Token: 0x17000743 RID: 1859
		// (get) Token: 0x0600247D RID: 9341 RVA: 0x0006FB5E File Offset: 0x0006DD5E
		public bool IsDisposed
		{
			get
			{
				return this.m_isDisposed;
			}
		}

		// Token: 0x17000744 RID: 1860
		// (get) Token: 0x0600247E RID: 9342 RVA: 0x0006FB66 File Offset: 0x0006DD66
		// (set) Token: 0x0600247F RID: 9343 RVA: 0x0006FB6E File Offset: 0x0006DD6E
		public bool IsFailed
		{
			get
			{
				return this.m_isFailed;
			}
			set
			{
				this.m_isFailed = value;
			}
		}

		// Token: 0x17000745 RID: 1861
		// (get) Token: 0x06002480 RID: 9344 RVA: 0x0006FB77 File Offset: 0x0006DD77
		protected object ThisLock
		{
			get
			{
				return this.m_lockObject;
			}
		}

		// Token: 0x17000746 RID: 1862
		// (get) Token: 0x06002481 RID: 9345 RVA: 0x0006FB80 File Offset: 0x0006DD80
		public TimeSpan RemainingOpenTime
		{
			get
			{
				OperationContext openContext = this.m_openContext;
				if (openContext == null)
				{
					return TimeSpan.MinValue;
				}
				return openContext.ExpirationTime.SafeRemainingDuration;
			}
		}

		// Token: 0x17000747 RID: 1863
		// (get) Token: 0x06002482 RID: 9346 RVA: 0x0006FBAC File Offset: 0x0006DDAC
		public bool IsOpenTimedOut
		{
			get
			{
				OperationContext openContext = this.m_openContext;
				return openContext == null || openContext.HasTimerExpired;
			}
		}

		// Token: 0x17000748 RID: 1864
		// (get) Token: 0x06002483 RID: 9347 RVA: 0x0006FBCC File Offset: 0x0006DDCC
		public TimeSpan RemainingCloseTime
		{
			get
			{
				OperationContext closeContext = this.m_closeContext;
				if (closeContext == null)
				{
					return TimeSpan.MinValue;
				}
				return closeContext.ExpirationTime.SafeRemainingDuration;
			}
		}

		// Token: 0x17000749 RID: 1865
		// (get) Token: 0x06002484 RID: 9348 RVA: 0x0006FBF8 File Offset: 0x0006DDF8
		public bool IsCloseTimedOut
		{
			get
			{
				OperationContext closeContext = this.m_closeContext;
				return closeContext == null || closeContext.HasTimerExpired;
			}
		}

		// Token: 0x06002485 RID: 9349 RVA: 0x0006FC17 File Offset: 0x0006DE17
		public void Open()
		{
			this.Open(this.DefaultOpenTimeout);
		}

		// Token: 0x06002486 RID: 9350 RVA: 0x0006FC25 File Offset: 0x0006DE25
		public void Open(TimeSpan timeout)
		{
			this.EndOpen(this.BeginOpen(timeout, null, null));
		}

		// Token: 0x06002487 RID: 9351 RVA: 0x0006FC36 File Offset: 0x0006DE36
		public void Close()
		{
			this.Close(this.DefaultCloseTimeout);
		}

		// Token: 0x06002488 RID: 9352 RVA: 0x0006FC44 File Offset: 0x0006DE44
		public void Close(TimeSpan timeout)
		{
			this.EndClose(this.BeginClose(timeout, null, null));
		}

		// Token: 0x06002489 RID: 9353 RVA: 0x0006FC55 File Offset: 0x0006DE55
		public void Dispose()
		{
			this.Dispose(this.DefaultCloseTimeout);
		}

		// Token: 0x0600248A RID: 9354 RVA: 0x0006FC63 File Offset: 0x0006DE63
		public void Dispose(TimeSpan timeout)
		{
			this.EndDispose(this.BeginDispose(timeout, null, null));
		}

		// Token: 0x0600248B RID: 9355 RVA: 0x0006FC74 File Offset: 0x0006DE74
		public IAsyncResult BeginOpen(AsyncCallback callback, object state)
		{
			return this.BeginOpen(this.DefaultOpenTimeout, callback, state);
		}

		// Token: 0x0600248C RID: 9356 RVA: 0x0006FC84 File Offset: 0x0006DE84
		public IAsyncResult BeginClose(AsyncCallback callback, object state)
		{
			return this.BeginClose(this.DefaultCloseTimeout, callback, state);
		}

		// Token: 0x0600248D RID: 9357 RVA: 0x0006FC94 File Offset: 0x0006DE94
		public IAsyncResult BeginDispose(AsyncCallback callback, object state)
		{
			return this.BeginDispose(this.DefaultCloseTimeout, callback, state);
		}

		// Token: 0x0600248E RID: 9358 RVA: 0x0006FCA4 File Offset: 0x0006DEA4
		public IAsyncResult BeginOpen(TimeSpan timeout, AsyncCallback callback, object state)
		{
			bool flag;
			CommunicationState state2;
			OperationContext operationContext;
			lock (this.m_lockObject)
			{
				this.ThrowIfDisposed();
				this.m_refCount++;
				flag = this.m_openContext != null;
				state2 = this.m_state;
				switch (state2)
				{
				case CommunicationState.Opening:
					ReleaseAssert.IsTrue(this.m_openContext != null);
					operationContext = this.m_openContext;
					break;
				case CommunicationState.Opened:
					ReleaseAssert.IsTrue(this.m_openContext == null);
					operationContext = null;
					break;
				case CommunicationState.Closing:
					if (this.m_openContext == null)
					{
						this.m_openContext = new OperationContext(callback, state, timeout);
					}
					operationContext = this.m_openContext;
					break;
				default:
					ReleaseAssert.IsTrue(this.m_openContext == null);
					this.m_state = CommunicationState.Opening;
					operationContext = (this.m_openContext = new OperationContext(callback, state, timeout));
					break;
				}
			}
			if (operationContext == null)
			{
				return new SynchronousCompletionOperationContext(callback, state);
			}
			if (flag)
			{
				return operationContext.CreateSecondaryContext(callback, state, timeout);
			}
			if (state2 != CommunicationState.Closing)
			{
				this.PerformOpen(true);
			}
			if (!operationContext.IsCompleted)
			{
				operationContext.StartTimer();
			}
			return operationContext;
		}

		// Token: 0x0600248F RID: 9359 RVA: 0x0006FDC0 File Offset: 0x0006DFC0
		private void ProcessOpenResult(OperationContext context, bool opened)
		{
			OperationContext operationContext = null;
			bool flag = false;
			lock (this.m_lockObject)
			{
				if (!opened)
				{
					this.m_refCount--;
					ReleaseAssert.IsTrue(this.m_refCount >= 0);
				}
				if (context == this.m_openContext)
				{
					flag = true;
					this.m_openContext = null;
					if (this.m_isAborted)
					{
						string text = ((this.m_faultEventArgs != null) ? (": " + this.m_faultEventArgs) : string.Empty);
						throw new ObjectDisposedException(this.ToString() + " aborted" + text);
					}
					ReleaseAssert.IsTrue(this.m_state == CommunicationState.Opening || this.m_state == CommunicationState.Faulted);
					if (this.m_closeContext != null)
					{
						operationContext = this.m_closeContext;
						this.m_state = CommunicationState.Closing;
					}
					else if (opened)
					{
						this.m_state = CommunicationState.Opened;
					}
					else
					{
						this.m_state = CommunicationState.Faulted;
					}
				}
			}
			if (flag)
			{
				if (opened)
				{
					this.InvokeHandler(this.Opened);
				}
				else
				{
					this.InvokeHandler(this.Faulted);
				}
			}
			if (operationContext != null)
			{
				this.PerformClose(false);
			}
		}

		// Token: 0x06002490 RID: 9360 RVA: 0x0006FEDC File Offset: 0x0006E0DC
		public void EndOpen(IAsyncResult result)
		{
			OperationContext operationContext = (OperationContext)result;
			try
			{
				operationContext.End();
				this.ProcessOpenResult(operationContext, true);
			}
			catch
			{
				this.ProcessOpenResult(operationContext, false);
				throw;
			}
		}

		// Token: 0x06002491 RID: 9361 RVA: 0x0006FF1C File Offset: 0x0006E11C
		private void PerformOpen(bool invokeSynchronously)
		{
			try
			{
				this.InvokeHandler(this.Opening);
				if (this.OnOpen())
				{
					this.CompleteOpen(invokeSynchronously, null);
				}
			}
			catch (Exception ex)
			{
				this.CompleteOpen(invokeSynchronously, ex);
			}
		}

		// Token: 0x06002492 RID: 9362 RVA: 0x0006FF64 File Offset: 0x0006E164
		private void CompleteOpen(bool completedSynchronously, Exception e)
		{
			OperationContext openContext = this.m_openContext;
			if (openContext != null)
			{
				openContext.CompleteOperation(completedSynchronously, e);
			}
		}

		// Token: 0x06002493 RID: 9363 RVA: 0x0006FF83 File Offset: 0x0006E183
		public void CompleteOpenAsynchronously(Exception e)
		{
			this.CompleteOpen(false, e);
		}

		// Token: 0x06002494 RID: 9364 RVA: 0x0006FF90 File Offset: 0x0006E190
		private IAsyncResult InternalBeginClose(bool forceClose, TimeSpan timeout, AsyncCallback callback, object state)
		{
			OperationContext operationContext = null;
			bool flag = false;
			bool flag2;
			CommunicationState state2;
			lock (this.m_lockObject)
			{
				if (this.m_refCount == 0)
				{
					EventLogWriter.WriteWarning("SharedCommunicationObject", "Close() without matching Open() for {0}", new object[] { this });
				}
				else if (forceClose)
				{
					if (!this.m_isDisposed)
					{
						this.m_isDisposed = true;
						flag = true;
					}
				}
				else
				{
					this.m_refCount--;
					ReleaseAssert.IsTrue(this.m_refCount >= 0);
					flag = this.m_refCount == 0 && !this.m_isDisposed;
				}
				flag2 = this.m_closeContext != null;
				state2 = this.m_state;
				if (flag)
				{
					switch (state2)
					{
					case CommunicationState.Opening:
						if (this.m_closeContext == null)
						{
							this.m_closeContext = new OperationContext(callback, state, timeout);
						}
						operationContext = this.m_closeContext;
						break;
					case CommunicationState.Opened:
						ReleaseAssert.IsTrue(this.m_closeContext == null);
						this.m_state = CommunicationState.Closing;
						operationContext = (this.m_closeContext = new OperationContext(callback, state, timeout));
						break;
					case CommunicationState.Closing:
						ReleaseAssert.IsTrue(this.m_closeContext != null);
						operationContext = this.m_closeContext;
						break;
					default:
						ReleaseAssert.IsTrue(this.m_closeContext == null);
						break;
					}
				}
			}
			if (operationContext == null)
			{
				return new SynchronousCompletionOperationContext(callback, state);
			}
			if (flag2)
			{
				return operationContext.CreateSecondaryContext(callback, state, timeout);
			}
			if (state2 != CommunicationState.Opening)
			{
				this.PerformClose(true);
			}
			if (!operationContext.IsCompleted)
			{
				operationContext.StartTimer();
			}
			return operationContext;
		}

		// Token: 0x06002495 RID: 9365 RVA: 0x00070124 File Offset: 0x0006E324
		private void ProcessCloseResult(OperationContext context, bool closed)
		{
			OperationContext operationContext = null;
			bool flag = false;
			bool flag2 = false;
			lock (this.m_lockObject)
			{
				if (context == this.m_closeContext)
				{
					flag = true;
					this.m_closeContext = null;
					if (this.m_isAborted)
					{
						string text = ((this.m_faultEventArgs != null) ? (": " + this.m_faultEventArgs) : string.Empty);
						throw new ObjectDisposedException(this.ToString() + " aborted" + text);
					}
					flag2 = this.m_isDisposed;
					ReleaseAssert.IsTrue(this.m_state == CommunicationState.Closing);
					if (closed)
					{
						this.m_state = CommunicationState.Closed;
					}
					else
					{
						this.m_state = CommunicationState.Faulted;
					}
					if (this.m_openContext != null)
					{
						operationContext = this.m_openContext;
						if (!flag2)
						{
							this.m_state = CommunicationState.Opening;
						}
					}
				}
			}
			if (flag)
			{
				if (closed)
				{
					this.InvokeHandler(this.Closed);
				}
				else
				{
					this.InvokeHandler(this.Faulted);
				}
			}
			if (operationContext != null)
			{
				if (flag2)
				{
					this.CompleteClose(false, new ObjectDisposedException(this.ToString()));
					return;
				}
				this.PerformOpen(false);
			}
		}

		// Token: 0x06002496 RID: 9366 RVA: 0x00070234 File Offset: 0x0006E434
		public void EndClose(IAsyncResult result)
		{
			OperationContext operationContext = (OperationContext)result;
			try
			{
				operationContext.End();
				this.ProcessCloseResult(operationContext, true);
			}
			catch
			{
				this.ProcessCloseResult(operationContext, false);
				throw;
			}
		}

		// Token: 0x06002497 RID: 9367 RVA: 0x00070274 File Offset: 0x0006E474
		private void PerformClose(bool invokeSynchronously)
		{
			try
			{
				this.InvokeHandler(this.Closing);
				if (this.OnClose())
				{
					this.CompleteClose(invokeSynchronously, null);
				}
			}
			catch (Exception ex)
			{
				this.CompleteClose(invokeSynchronously, ex);
			}
		}

		// Token: 0x06002498 RID: 9368 RVA: 0x000702BC File Offset: 0x0006E4BC
		private void CompleteClose(bool completedSynchronously, Exception e)
		{
			OperationContext closeContext = this.m_closeContext;
			if (closeContext != null)
			{
				closeContext.CompleteOperation(completedSynchronously, e);
			}
		}

		// Token: 0x06002499 RID: 9369 RVA: 0x000702DB File Offset: 0x0006E4DB
		public void CompleteCloseAsynchronously(Exception e)
		{
			this.CompleteClose(false, e);
		}

		// Token: 0x0600249A RID: 9370 RVA: 0x000702E5 File Offset: 0x0006E4E5
		public IAsyncResult BeginClose(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return this.InternalBeginClose(false, timeout, callback, state);
		}

		// Token: 0x0600249B RID: 9371 RVA: 0x000702F1 File Offset: 0x0006E4F1
		public IAsyncResult BeginDispose(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return this.InternalBeginClose(true, timeout, callback, state);
		}

		// Token: 0x0600249C RID: 9372 RVA: 0x000702FD File Offset: 0x0006E4FD
		public void EndDispose(IAsyncResult result)
		{
			this.EndClose(result);
		}

		// Token: 0x0600249D RID: 9373 RVA: 0x00070308 File Offset: 0x0006E508
		public void Abort()
		{
			OperationContext operationContext = null;
			OperationContext operationContext2 = null;
			CommunicationState state;
			lock (this.m_lockObject)
			{
				this.m_isDisposed = true;
				this.m_isAborted = true;
				state = this.m_state;
				operationContext = this.m_openContext;
				operationContext2 = this.m_closeContext;
				this.m_state = CommunicationState.Closed;
			}
			if (state == CommunicationState.Closed || state == CommunicationState.Created || state == CommunicationState.Faulted)
			{
				ReleaseAssert.IsTrue(operationContext == null && operationContext2 == null);
				return;
			}
			if (operationContext != null)
			{
				operationContext.CompleteOperation(false, new ObjectDisposedException(this.ToString()));
			}
			if (operationContext2 != null)
			{
				operationContext2.CompleteOperation(false, new ObjectDisposedException(this.ToString()));
			}
			this.OnAbort();
		}

		// Token: 0x0600249E RID: 9374 RVA: 0x000703B4 File Offset: 0x0006E5B4
		protected void ThrowIfDisposed()
		{
			if (this.m_isDisposed)
			{
				throw new ObjectDisposedException(this.ToString());
			}
		}

		// Token: 0x0600249F RID: 9375 RVA: 0x000703CA File Offset: 0x0006E5CA
		private void InvokeHandler(EventHandler handler)
		{
			this.InvokeHandler(handler, EventArgs.Empty);
		}

		// Token: 0x060024A0 RID: 9376 RVA: 0x000703D8 File Offset: 0x0006E5D8
		private void InvokeHandler(EventHandler handler, EventArgs arg)
		{
			if (handler != null)
			{
				handler(this, arg);
			}
		}

		// Token: 0x060024A1 RID: 9377 RVA: 0x000703E8 File Offset: 0x0006E5E8
		public void Fail(EventArgs arg)
		{
			bool flag;
			lock (this.m_lockObject)
			{
				if (this.m_isFailed)
				{
					return;
				}
				this.m_faultEventArgs = arg;
				if (this.m_state == CommunicationState.Opening)
				{
					flag = true;
				}
				else
				{
					if (this.m_state != CommunicationState.Opened)
					{
						return;
					}
					flag = false;
				}
				this.m_isFailed = true;
			}
			if (flag)
			{
				this.Abort();
				return;
			}
			this.InvokeHandler(this.Failed, arg);
		}

		// Token: 0x04001663 RID: 5731
		private const string logSource = "SharedCommunicationObject";

		// Token: 0x04001664 RID: 5732
		private CommunicationState m_state;

		// Token: 0x04001665 RID: 5733
		private bool m_isDisposed;

		// Token: 0x04001666 RID: 5734
		private bool m_isAborted;

		// Token: 0x04001667 RID: 5735
		private bool m_isFailed;

		// Token: 0x04001668 RID: 5736
		private int m_refCount;

		// Token: 0x04001669 RID: 5737
		private OperationContext m_openContext;

		// Token: 0x0400166A RID: 5738
		private OperationContext m_closeContext;

		// Token: 0x0400166B RID: 5739
		private EventArgs m_faultEventArgs;

		// Token: 0x0400166C RID: 5740
		private object m_lockObject;
	}
}
