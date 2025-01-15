using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using JetBrains.Annotations;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x020001B7 RID: 439
	public class CallGate
	{
		// Token: 0x06000B53 RID: 2899 RVA: 0x00027668 File Offset: 0x00025868
		private void CallSyncImpl(Action<object> function, object context, bool invokePendingFunctionCalls)
		{
			bool flag = false;
			this.EnterGateInvokeActionLeaveGate(delegate
			{
				Queue<CallGate.PendingFunctionCall> queue = null;
				if (invokePendingFunctionCalls)
				{
					queue = this.GrabPendingFunctionCalls();
				}
				bool flag2 = false;
				do
				{
					if (queue != null)
					{
						foreach (CallGate.PendingFunctionCall pendingFunctionCall in queue)
						{
							pendingFunctionCall.Invoke();
						}
						queue = null;
					}
					if (function != null)
					{
						function(context);
						function = null;
					}
					if (invokePendingFunctionCalls)
					{
						Interlocked.Decrement(ref this.m_invokePendingFunctionCallsScheduled);
						queue = this.GrabPendingFunctionCalls();
						flag2 = queue != null;
						if (flag2)
						{
							Interlocked.Increment(ref this.m_invokePendingFunctionCallsScheduled);
						}
					}
				}
				while (flag2);
			}, true, ref flag);
		}

		// Token: 0x06000B54 RID: 2900 RVA: 0x000276B0 File Offset: 0x000258B0
		private void CallAsyncImpl(Action<object> function, object context, CallGateAsyncOptions options)
		{
			object pendingFunctionCallsLock = this.m_pendingFunctionCallsLock;
			lock (pendingFunctionCallsLock)
			{
				if (this.m_pendingFunctionCalls == null)
				{
					this.m_pendingFunctionCalls = new Queue<CallGate.PendingFunctionCall>();
				}
				this.m_pendingFunctionCalls.Enqueue(new CallGate.PendingFunctionCall(function, context, options));
			}
			if (Interlocked.CompareExchange(ref this.m_invokePendingFunctionCallsScheduled, 1, 0) == 0)
			{
				AsyncInvoker.InvokeMethodAsynchronously(this.m_invokePendingFunctionCalls, WaitOrNot.DontWait, "Microsoft.Cloud.Platform.Utils.CallGate.CallAsyncImpl");
			}
		}

		// Token: 0x06000B55 RID: 2901 RVA: 0x00027730 File Offset: 0x00025930
		private void EnsureGateNotOurs()
		{
			if (this.gateOwnedBy == Thread.CurrentThread.ManagedThreadId)
			{
				CallgateAlreadyOwnedException ex = new CallgateAlreadyOwnedException();
				TraceSourceBase<UtilsTrace>.Tracer.TraceError("Throwing CallgateAlreadyOwnedException: {0}", new object[] { ex });
				throw ex;
			}
		}

		// Token: 0x06000B56 RID: 2902 RVA: 0x00009B3B File Offset: 0x00007D3B
		[Conditional("DEBUG")]
		private void AssertGateIsOurs()
		{
		}

		// Token: 0x06000B57 RID: 2903 RVA: 0x00027770 File Offset: 0x00025970
		private void OnGateOwnerChanged(bool taken)
		{
			if (taken)
			{
				this.gateOwnedBy = Thread.CurrentThread.ManagedThreadId;
				return;
			}
			this.gateOwnedBy = 0;
		}

		// Token: 0x06000B58 RID: 2904 RVA: 0x00027790 File Offset: 0x00025990
		private void EnterGateInvokeActionLeaveGate(Action action, bool waitIfTaken, ref bool success)
		{
			bool flag = false;
			try
			{
				if (waitIfTaken)
				{
					Monitor.Enter(this.m_gateOwnedLock, ref flag);
				}
				else
				{
					Monitor.TryEnter(this.m_gateOwnedLock, ref flag);
				}
				if (flag)
				{
					try
					{
						this.EnsureGateNotOurs();
						this.OnGateOwnerChanged(true);
						success = true;
						action();
					}
					finally
					{
						this.OnGateOwnerChanged(false);
					}
				}
			}
			finally
			{
				if (flag)
				{
					Monitor.Exit(this.m_gateOwnedLock);
				}
			}
		}

		// Token: 0x06000B59 RID: 2905 RVA: 0x00027810 File Offset: 0x00025A10
		private Queue<CallGate.PendingFunctionCall> GrabPendingFunctionCalls()
		{
			Queue<CallGate.PendingFunctionCall> queue = null;
			object pendingFunctionCallsLock = this.m_pendingFunctionCallsLock;
			lock (pendingFunctionCallsLock)
			{
				if (this.m_pendingFunctionCalls != null)
				{
					queue = this.m_pendingFunctionCalls;
					this.m_pendingFunctionCalls = null;
				}
			}
			return queue;
		}

		// Token: 0x06000B5A RID: 2906 RVA: 0x00027864 File Offset: 0x00025A64
		public CallGate()
		{
			this.m_invokePendingFunctionCalls = delegate
			{
				this.CallSyncImpl(null, null, true);
			};
		}

		// Token: 0x06000B5B RID: 2907 RVA: 0x00027894 File Offset: 0x00025A94
		public void CallSync(Action<object> function, object context, bool invokePendingFunctionCalls)
		{
			if (invokePendingFunctionCalls)
			{
				Interlocked.Increment(ref this.m_invokePendingFunctionCallsScheduled);
			}
			this.CallSyncImpl(function, context, invokePendingFunctionCalls);
		}

		// Token: 0x06000B5C RID: 2908 RVA: 0x000278AE File Offset: 0x00025AAE
		public void CallSync(Action<object> function, object context)
		{
			Interlocked.Increment(ref this.m_invokePendingFunctionCallsScheduled);
			this.CallSyncImpl(function, context, true);
		}

		// Token: 0x06000B5D RID: 2909 RVA: 0x000278C8 File Offset: 0x00025AC8
		public bool CallAsync([NotNull] Action<object> function, object context, CallGateAsyncOptions options)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<Action<object>>(function, "function");
			if (options.HasFlag(CallGateAsyncOptions.AllowSyncCall))
			{
				bool flag = false;
				this.EnterGateInvokeActionLeaveGate(delegate
				{
					function(context);
				}, false, ref flag);
				if (flag)
				{
					return true;
				}
			}
			this.CallAsyncImpl(function, context, options);
			return false;
		}

		// Token: 0x06000B5E RID: 2910 RVA: 0x0002793C File Offset: 0x00025B3C
		public void CallAsync(Action<object> function, object context)
		{
			this.CallAsyncImpl(function, context, CallGateAsyncOptions.None);
		}

		// Token: 0x0400046E RID: 1134
		private object m_pendingFunctionCallsLock = new object();

		// Token: 0x0400046F RID: 1135
		private Queue<CallGate.PendingFunctionCall> m_pendingFunctionCalls;

		// Token: 0x04000470 RID: 1136
		private object m_gateOwnedLock = new object();

		// Token: 0x04000471 RID: 1137
		private Action m_invokePendingFunctionCalls;

		// Token: 0x04000472 RID: 1138
		private int m_invokePendingFunctionCallsScheduled;

		// Token: 0x04000473 RID: 1139
		private int gateOwnedBy;

		// Token: 0x02000675 RID: 1653
		private class PendingFunctionCall
		{
			// Token: 0x06002D9A RID: 11674 RVA: 0x000A0EE0 File Offset: 0x0009F0E0
			internal PendingFunctionCall(Action<object> function, object context, CallGateAsyncOptions options)
			{
				this.m_function = function;
				this.m_context = context;
				this.m_options = options;
				this.m_capturedContextStack = null;
				if (options.HasFlag(CallGateAsyncOptions.PropagateCallContext))
				{
					this.m_capturedContextStack = UtilsContext.Current.CaptureStack();
				}
			}

			// Token: 0x06002D9B RID: 11675 RVA: 0x000A0F34 File Offset: 0x0009F134
			internal void Invoke()
			{
				if (this.m_options.HasFlag(CallGateAsyncOptions.PropagateCallContext))
				{
					ExtendedDiagnostics.EnsureOperation(this.m_capturedContextStack != null, "m_capturedContextStack should not be null");
					using (this.m_capturedContextStack.Restore())
					{
						this.m_function(this.m_context);
						return;
					}
				}
				UtilsContext.Current.RunWithClearContext(delegate
				{
					this.m_function(this.m_context);
				});
			}

			// Token: 0x0400123E RID: 4670
			private IContextStack m_capturedContextStack;

			// Token: 0x0400123F RID: 4671
			private Action<object> m_function;

			// Token: 0x04001240 RID: 4672
			private object m_context;

			// Token: 0x04001241 RID: 4673
			private CallGateAsyncOptions m_options;
		}
	}
}
