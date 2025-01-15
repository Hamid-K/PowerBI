using System;
using System.Globalization;
using JetBrains.Annotations;
using Microsoft.Cloud.Platform.Modularization.Internal;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Modularization
{
	// Token: 0x020000D5 RID: 213
	public sealed class WinAspNet<T> : IApplicationRootHost where T : ApplicationRoot, new()
	{
		// Token: 0x06000602 RID: 1538 RVA: 0x00015360 File Offset: 0x00013560
		public WinAspNet(T appRoot)
		{
			TraceSourceBase<ModularizationFrameworkTrace>.Tracer.Trace(TraceVerbosity.Verbose, "WinAspNet created");
			this.m_applicationRoot = appRoot;
			this.m_applicationRootMethodInvoker = new ApplicationRootMethodInvoker(this, this.m_applicationRoot, typeof(T).Name);
			this.m_stateSynchronizer = new BlockStateTransitionSynchronizer();
			this.m_shutdownRequestedLock = new object();
		}

		// Token: 0x06000603 RID: 1539 RVA: 0x000153C6 File Offset: 0x000135C6
		public void RunAsync()
		{
			this.m_stateSynchronizer.AdvanceToState(BlockState.Uninitialized, BlockState.Initializing, PulseOrNot.DontPulse);
			this.m_applicationRootMethodInvoker.Initialize(null, ApplicationSwitchesTypes.WebConfig);
			this.m_applicationRootMethodInvoker.Start(null);
			this.m_stateSynchronizer.AdvanceToState(BlockState.Initializing, BlockState.Started, PulseOrNot.Pulse);
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x06000604 RID: 1540 RVA: 0x000153FD File Offset: 0x000135FD
		public ApplicationRoot ApplicationRoot
		{
			get
			{
				return this.m_applicationRoot;
			}
		}

		// Token: 0x06000605 RID: 1541 RVA: 0x0001540C File Offset: 0x0001360C
		public void RequestShutdown(IBlock requestor)
		{
			TraceSourceBase<ModularizationFrameworkTrace>.Tracer.Trace(TraceVerbosity.Info, "RequestShutdown {0}", new object[] { (requestor == null) ? "" : (" (initiated by " + requestor.Name + ")") });
			bool flag = false;
			object shutdownRequestedLock = this.m_shutdownRequestedLock;
			lock (shutdownRequestedLock)
			{
				if (!this.m_shutdownRequested)
				{
					flag = true;
					this.m_shutdownRequested = true;
				}
				else
				{
					TraceSourceBase<ModularizationFrameworkTrace>.Tracer.Trace(TraceVerbosity.Warning, "RequestShutdown ignores the request for shutdown as one already started.");
				}
			}
			if (flag)
			{
				this.InvokeMethodAsynchronously(new Action(this.AsyncStopAndShutdown), WaitOrNot.DontWait, "WinAspNet<T>.RequestShutdown >>> AsyncStopAndShutdown");
			}
		}

		// Token: 0x06000606 RID: 1542 RVA: 0x000154C0 File Offset: 0x000136C0
		public void RequestShutdown(IBlock requestor, int returnCode)
		{
			TraceSourceBase<ModularizationFrameworkTrace>.Tracer.Trace(TraceVerbosity.Info, "RequestShutdown got return code {0}, which is ignored since WinAspNet cannot exit with an error code.", new object[] { returnCode });
			this.RequestShutdown(requestor);
		}

		// Token: 0x06000607 RID: 1543 RVA: 0x000154E8 File Offset: 0x000136E8
		public void WaitForStateToComplete(BlockState stateToWaitFor)
		{
			this.m_stateSynchronizer.WaitForState(stateToWaitFor);
		}

		// Token: 0x06000608 RID: 1544 RVA: 0x000154F6 File Offset: 0x000136F6
		public void AlertDebugger()
		{
			ExtendedDiagnostics.AlertDebugger(AlertDebuggerAction.WaitForDebuggerToGetAttached);
		}

		// Token: 0x06000609 RID: 1545 RVA: 0x000154FE File Offset: 0x000136FE
		public void InvokeCallbackIfInState([NotNull] Action callback, BlockState state)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<Action>(callback, "callback");
			if (!this.m_stateSynchronizer.TryInvokeCallbackIfInState(state, callback))
			{
				throw new IllegalBlockStateException(string.Format(CultureInfo.InvariantCulture, "Application Root is not in state {0}", new object[] { state }));
			}
		}

		// Token: 0x0600060A RID: 1546 RVA: 0x00015540 File Offset: 0x00013740
		private void AsyncStopAndShutdown()
		{
			TraceSourceBase<ModularizationFrameworkTrace>.Tracer.Trace(TraceVerbosity.Info, "AsyncStopAndShutdown started");
			this.m_stateSynchronizer.WaitForStateAndAdvanceToState(BlockState.Started, BlockState.Stopping, PulseOrNot.DontPulse);
			this.m_applicationRootMethodInvoker.Stop();
			this.m_applicationRootMethodInvoker.WaitForStopToComplete();
			this.m_applicationRootMethodInvoker.Shutdown();
			this.m_stateSynchronizer.AdvanceToState(BlockState.Stopping, BlockState.Uninitialized, PulseOrNot.Pulse);
		}

		// Token: 0x0600060B RID: 1547 RVA: 0x000152A6 File Offset: 0x000134A6
		private void InvokeMethodAsynchronously(Action f, WaitOrNot fWait, string methodName)
		{
			AsyncInvoker.InvokeMethodAsynchronously(f, fWait, methodName);
		}

		// Token: 0x04000217 RID: 535
		private T m_applicationRoot;

		// Token: 0x04000218 RID: 536
		private ApplicationRootMethodInvoker m_applicationRootMethodInvoker;

		// Token: 0x04000219 RID: 537
		private BlockStateTransitionSynchronizer m_stateSynchronizer;

		// Token: 0x0400021A RID: 538
		private object m_shutdownRequestedLock;

		// Token: 0x0400021B RID: 539
		private bool m_shutdownRequested;
	}
}
