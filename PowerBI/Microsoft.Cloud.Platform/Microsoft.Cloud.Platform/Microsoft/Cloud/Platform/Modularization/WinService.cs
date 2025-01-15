using System;
using System.ComponentModel;
using System.Globalization;
using System.ServiceProcess;
using System.Threading;
using JetBrains.Annotations;
using Microsoft.Cloud.Platform.Modularization.Internal;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Modularization
{
	// Token: 0x020000D9 RID: 217
	[DesignerCategory("Code")]
	public class WinService<T> : ServiceBase, IApplicationRootHost where T : ApplicationRoot, new()
	{
		// Token: 0x0600061A RID: 1562 RVA: 0x00015878 File Offset: 0x00013A78
		public WinService(T appRoot, string[] cmd)
		{
			this.m_applicationRoot = appRoot;
			this.m_applicationRootMethodInvoker = new ApplicationRootMethodInvoker(this, this.m_applicationRoot, typeof(T).Name);
			this.m_cmdArgs = cmd;
			base.ServiceName = this.m_applicationRoot.Name;
			base.CanStop = true;
			base.CanShutdown = true;
			base.AutoLog = true;
			string text = string.Join(" ", this.m_cmdArgs);
			this.m_stateSynchronizer = new BlockStateTransitionSynchronizer();
			TraceSourceBase<ModularizationFrameworkTrace>.Tracer.Trace(TraceVerbosity.Verbose, "WinService {0} created. Command Line inputted: {1}", new object[] { base.ServiceName, text });
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x0600061B RID: 1563 RVA: 0x0001593F File Offset: 0x00013B3F
		public ApplicationRoot ApplicationRoot
		{
			get
			{
				return this.m_applicationRoot;
			}
		}

		// Token: 0x0600061C RID: 1564 RVA: 0x0001594C File Offset: 0x00013B4C
		public void RequestShutdown(IBlock requestor)
		{
			TraceSourceBase<ModularizationFrameworkTrace>.Tracer.Trace(TraceVerbosity.Info, "RequestShutdown {0}", new object[] { (requestor == null) ? "" : (" (initiated by " + requestor.Name + ")") });
			AsyncInvoker.InvokeMethodAsynchronously(new Action(base.Stop), WaitOrNot.DontWait, "Stop");
		}

		// Token: 0x0600061D RID: 1565 RVA: 0x000159A8 File Offset: 0x00013BA8
		public void RequestShutdown(IBlock requestor, int returnCode)
		{
			TraceSourceBase<ModularizationFrameworkTrace>.Tracer.Trace(TraceVerbosity.Info, "RequestShutdown got return code {0}, which is ignored since WinService cannot exit with an error code.", new object[] { returnCode });
			this.RequestShutdown(requestor);
		}

		// Token: 0x0600061E RID: 1566 RVA: 0x000159D0 File Offset: 0x00013BD0
		public void WaitForStateToComplete(BlockState stateToWaitFor)
		{
			this.m_stateSynchronizer.WaitForState(stateToWaitFor);
		}

		// Token: 0x0600061F RID: 1567 RVA: 0x000159DE File Offset: 0x00013BDE
		public void AlertDebugger()
		{
			ExtendedDiagnostics.AlertDebugger(AlertDebuggerAction.TweakBased);
		}

		// Token: 0x06000620 RID: 1568 RVA: 0x000159E6 File Offset: 0x00013BE6
		public void InvokeCallbackIfInState([NotNull] Action callback, BlockState state)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<Action>(callback, "callback");
			if (!this.m_stateSynchronizer.TryInvokeCallbackIfInState(state, callback))
			{
				throw new IllegalBlockStateException(string.Format(CultureInfo.InvariantCulture, "Application Root is not in state {0}", new object[] { state }));
			}
		}

		// Token: 0x06000621 RID: 1569 RVA: 0x00015A26 File Offset: 0x00013C26
		protected override void OnStart(string[] args)
		{
			TopLevelHandler.Run(this, delegate
			{
				TraceSourceBase<ModularizationFrameworkTrace>.Tracer.Trace(TraceVerbosity.Info, "Service {0} - OnStart", new object[] { base.ServiceName });
				this.m_stateSynchronizer.AdvanceToState(BlockState.Uninitialized, BlockState.Initializing, PulseOrNot.DontPulse);
				this.InvokeMethodAsynchronously(new Action(this.AsyncInitialize), true, "WinService<T>.OnStart >>> AsyncInitialize");
				base.RequestAdditionalTime(this.m_additionalTime_ms);
				this.InvokeMethodAsynchronously(new Action(this.AsyncStart), false, "WinService<T>.OnStart >>> AsyncStart");
			});
		}

		// Token: 0x06000622 RID: 1570 RVA: 0x00015A3B File Offset: 0x00013C3B
		protected override void OnStop()
		{
			TopLevelHandler.Run(this, delegate
			{
				TraceSourceBase<ModularizationFrameworkTrace>.Tracer.Trace(TraceVerbosity.Info, "Service {0} OnStop", new object[] { base.ServiceName });
				this.InvokeMethodAsynchronously(new Action(this.AsyncStopAndWait), true, "WinService<T>.OnStop >>> AsyncStopAndWait");
				TraceSourceBase<ModularizationFrameworkTrace>.Tracer.Trace(TraceVerbosity.Info, "Service {0} OnShutdown", new object[] { base.ServiceName });
				this.InvokeMethodAsynchronously(new Action(this.AsyncShutdown), true, "WinService<T>.OnStop >>> AsyncShutdown");
			});
		}

		// Token: 0x06000623 RID: 1571 RVA: 0x00015A50 File Offset: 0x00013C50
		protected override void OnShutdown()
		{
			TraceSourceBase<ModularizationFrameworkTrace>.Tracer.Trace(TraceVerbosity.Info, "System shuting down. Service {0} Stop", new object[] { base.ServiceName });
			base.Stop();
		}

		// Token: 0x06000624 RID: 1572 RVA: 0x00015A77 File Offset: 0x00013C77
		private void AsyncInitialize()
		{
			TraceSourceBase<ModularizationFrameworkTrace>.Tracer.Trace(TraceVerbosity.Verbose, "Service {0} AsyncInitialize", new object[] { base.ServiceName });
			this.m_applicationRootMethodInvoker.Initialize(this.m_cmdArgs, ApplicationSwitchesTypes.CommandLine | ApplicationSwitchesTypes.AppConfig);
		}

		// Token: 0x06000625 RID: 1573 RVA: 0x00015AAA File Offset: 0x00013CAA
		private void AsyncStart()
		{
			TraceSourceBase<ModularizationFrameworkTrace>.Tracer.Trace(TraceVerbosity.Verbose, "Service {0} AsyncStart", new object[] { base.ServiceName });
			this.m_applicationRootMethodInvoker.Start(delegate
			{
				this.m_stateSynchronizer.AdvanceToState(BlockState.Initializing, BlockState.Started, PulseOrNot.Pulse);
			});
		}

		// Token: 0x06000626 RID: 1574 RVA: 0x00015AE4 File Offset: 0x00013CE4
		private void AsyncStopAndWait()
		{
			TraceSourceBase<ModularizationFrameworkTrace>.Tracer.Trace(TraceVerbosity.Verbose, "Service {0} AsyncStopAndWait", new object[] { base.ServiceName });
			this.m_stateSynchronizer.WaitForStateAndAdvanceToState(BlockState.Started, BlockState.Stopping, PulseOrNot.DontPulse);
			this.m_applicationRootMethodInvoker.Stop();
			this.m_applicationRootMethodInvoker.WaitForStopToComplete();
		}

		// Token: 0x06000627 RID: 1575 RVA: 0x00015B34 File Offset: 0x00013D34
		private void AsyncShutdown()
		{
			TraceSourceBase<ModularizationFrameworkTrace>.Tracer.Trace(TraceVerbosity.Verbose, "Service {0} AsyncShutdown", new object[] { base.ServiceName });
			this.m_applicationRootMethodInvoker.Shutdown();
			this.m_stateSynchronizer.AdvanceToState(BlockState.Stopping, BlockState.Uninitialized, PulseOrNot.Pulse);
		}

		// Token: 0x06000628 RID: 1576 RVA: 0x00015B6E File Offset: 0x00013D6E
		private void InvokeMethodAsynchronously(Action f, bool fPumpScmAndWait, string methodName)
		{
			if (fPumpScmAndWait)
			{
				AsyncInvoker.InvokeMethodAsynchronously(f, delegate(WaitHandle waitHandle)
				{
					do
					{
						base.RequestAdditionalTime(this.m_additionalTime_ms);
					}
					while (!waitHandle.WaitOne(this.m_asyncPollingPeriod_ms, false));
				}, methodName);
				return;
			}
			AsyncInvoker.InvokeMethodAsynchronously(f, WaitOrNot.DontWait, methodName);
		}

		// Token: 0x04000221 RID: 545
		private T m_applicationRoot;

		// Token: 0x04000222 RID: 546
		private ApplicationRootMethodInvoker m_applicationRootMethodInvoker;

		// Token: 0x04000223 RID: 547
		private string[] m_cmdArgs;

		// Token: 0x04000224 RID: 548
		private int m_additionalTime_ms = 15000;

		// Token: 0x04000225 RID: 549
		private int m_asyncPollingPeriod_ms = 10000;

		// Token: 0x04000226 RID: 550
		private BlockStateTransitionSynchronizer m_stateSynchronizer;
	}
}
