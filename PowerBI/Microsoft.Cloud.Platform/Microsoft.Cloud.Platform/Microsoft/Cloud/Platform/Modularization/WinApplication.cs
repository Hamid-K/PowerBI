using System;
using System.Globalization;
using JetBrains.Annotations;
using Microsoft.Cloud.Platform.Modularization.Internal;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Modularization
{
	// Token: 0x020000D3 RID: 211
	public class WinApplication<T> : IApplicationRootHost where T : ApplicationRoot, new()
	{
		// Token: 0x060005F2 RID: 1522 RVA: 0x00014FB8 File Offset: 0x000131B8
		internal WinApplication(T applicationRoot)
		{
			TraceSourceBase<ModularizationFrameworkTrace>.Tracer.TraceVerbose("WinApplication created");
			this.m_applicationRoot = applicationRoot;
			this.m_applicationRootMethodInvoker = new ApplicationRootMethodInvoker(this, this.m_applicationRoot, typeof(T).Name);
			this.m_stateSynchronizer = new BlockStateTransitionSynchronizer();
			this.m_shutdownRequestedLock = new object();
			this.m_consoleControlDel = new NativeMethods.ConsoleCtrlDelegate(this.ConsoleEventHandler);
			this.m_returnCode = 0;
		}

		// Token: 0x060005F3 RID: 1523 RVA: 0x00015038 File Offset: 0x00013238
		public int Run(string[] cmdArgs)
		{
			this.m_args = cmdArgs;
			if (NativeMethods.SetConsoleCtrlHandler(this.m_consoleControlDel, 1) == 0)
			{
				TraceSourceBase<ModularizationFrameworkTrace>.Tracer.TraceWarning("WinApplication.Run failed to register the console control handler");
				ExtendedDiagnostics.AlertDebuggerIfAttached();
			}
			Console.CancelKeyPress += delegate(object sender, ConsoleCancelEventArgs controlCancelEventArgs)
			{
				TraceSourceBase<ModularizationFrameworkTrace>.Tracer.TraceWarning("Request for application shutdown received by Console.CancelKeyPress");
				AsyncInvoker.InvokeMethodAsynchronously(new Action(this.ShutdownRequestedByUser), WaitOrNot.DontWait, "ShutdownRequestedByUser");
				controlCancelEventArgs.Cancel = !Array.Exists<string>(cmdArgs, (string element) => element == "-AllowControlCancelRequestsToPropagate");
			};
			this.m_stateSynchronizer.AdvanceToState(BlockState.Uninitialized, BlockState.Initializing, PulseOrNot.DontPulse);
			this.InitializeAndStart();
			this.m_stateSynchronizer.WaitForState(BlockState.Uninitialized);
			return this.m_returnCode;
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x060005F4 RID: 1524 RVA: 0x000150BE File Offset: 0x000132BE
		public ApplicationRoot ApplicationRoot
		{
			get
			{
				return this.m_applicationRoot;
			}
		}

		// Token: 0x060005F5 RID: 1525 RVA: 0x000150CB File Offset: 0x000132CB
		public void RequestShutdown(IBlock requestor)
		{
			this.RequestShutdown(requestor, 0);
		}

		// Token: 0x060005F6 RID: 1526 RVA: 0x000150D8 File Offset: 0x000132D8
		public void RequestShutdown(IBlock requestor, int returnCode)
		{
			TraceSourceBase<ModularizationFrameworkTrace>.Tracer.Trace(TraceVerbosity.Info, "RequestShutdown {0} with return code {1}", new object[]
			{
				(requestor == null) ? "" : (" (initiated by " + requestor.Name + ")"),
				returnCode
			});
			this.m_returnCode = returnCode;
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
				this.InvokeMethodAsynchronously(new Action(this.AsyncStopAndShutdown), WaitOrNot.DontWait, "WinApplication<T>.RequestShutdown >>> AsyncStopAndShutdown");
			}
		}

		// Token: 0x060005F7 RID: 1527 RVA: 0x0001519C File Offset: 0x0001339C
		public void WaitForStateToComplete(BlockState stateToWaitFor)
		{
			this.m_stateSynchronizer.WaitForState(stateToWaitFor);
		}

		// Token: 0x060005F8 RID: 1528 RVA: 0x000151AA File Offset: 0x000133AA
		public void AlertDebugger()
		{
			ExtendedDiagnostics.AlertDebugger(AlertDebuggerAction.LaunchManagedDebugger);
		}

		// Token: 0x060005F9 RID: 1529 RVA: 0x000151B2 File Offset: 0x000133B2
		public void InvokeCallbackIfInState([NotNull] Action callback, BlockState state)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<Action>(callback, "callback");
			if (!this.m_stateSynchronizer.TryInvokeCallbackIfInState(state, callback))
			{
				throw new IllegalBlockStateException(string.Format(CultureInfo.InvariantCulture, "Application Root is not in state {0}", new object[] { state }));
			}
		}

		// Token: 0x060005FA RID: 1530 RVA: 0x000151F4 File Offset: 0x000133F4
		private void InitializeAndStart()
		{
			TraceSourceBase<ModularizationFrameworkTrace>.Tracer.Trace(TraceVerbosity.Info, "InitializeAndStart started");
			this.m_applicationRootMethodInvoker.Initialize(this.m_args, ApplicationSwitchesTypes.CommandLine | ApplicationSwitchesTypes.AppConfig | ApplicationSwitchesTypes.ActivationData);
			this.m_applicationRootMethodInvoker.Start(delegate
			{
				this.m_stateSynchronizer.AdvanceToState(BlockState.Initializing, BlockState.Started, PulseOrNot.Pulse);
			});
			TraceSourceBase<ModularizationFrameworkTrace>.Tracer.Trace(TraceVerbosity.Info, "InitializeAndStart completes");
		}

		// Token: 0x060005FB RID: 1531 RVA: 0x0001524C File Offset: 0x0001344C
		private void AsyncStopAndShutdown()
		{
			TraceSourceBase<ModularizationFrameworkTrace>.Tracer.Trace(TraceVerbosity.Info, "AsyncStopAndShutdown started");
			this.m_stateSynchronizer.WaitForStateAndAdvanceToState(BlockState.Started, BlockState.Stopping, PulseOrNot.DontPulse);
			this.m_applicationRootMethodInvoker.Stop();
			this.m_applicationRootMethodInvoker.WaitForStopToComplete();
			this.m_applicationRootMethodInvoker.Shutdown();
			this.m_stateSynchronizer.AdvanceToState(BlockState.Stopping, BlockState.Uninitialized, PulseOrNot.Pulse);
		}

		// Token: 0x060005FC RID: 1532 RVA: 0x000152A6 File Offset: 0x000134A6
		private void InvokeMethodAsynchronously(Action f, WaitOrNot fWait, string methodName)
		{
			AsyncInvoker.InvokeMethodAsynchronously(f, fWait, methodName);
		}

		// Token: 0x060005FD RID: 1533 RVA: 0x000152B0 File Offset: 0x000134B0
		private int ConsoleEventHandler(NativeMethods.CtrlTypes ctrlType)
		{
			TraceSourceBase<ModularizationFrameworkTrace>.Tracer.TraceWarning("Request for application shutdown received by ConsoleEventHandler");
			AsyncInvoker.InvokeMethodAsynchronously(new Action(this.ShutdownRequestedByUser), WaitOrNot.DontWait, "ShutdownRequestedByUser");
			if (this.m_args == null)
			{
				return 1;
			}
			if (!Array.Exists<string>(this.m_args, (string element) => element == "-AllowControlCancelRequestsToPropagate"))
			{
				return 1;
			}
			return 0;
		}

		// Token: 0x060005FE RID: 1534 RVA: 0x0001531C File Offset: 0x0001351C
		private void ShutdownRequestedByUser()
		{
			TraceSourceBase<ModularizationFrameworkTrace>.Tracer.Trace(TraceVerbosity.Info, "Shutdown requested by user");
			this.m_applicationRoot.RequestShutdown(null);
		}

		// Token: 0x060005FF RID: 1535 RVA: 0x0001533F File Offset: 0x0001353F
		internal void WaitForStartedState()
		{
			this.m_stateSynchronizer.WaitForState(BlockState.Started);
		}

		// Token: 0x0400020C RID: 524
		private T m_applicationRoot;

		// Token: 0x0400020D RID: 525
		private ApplicationRootMethodInvoker m_applicationRootMethodInvoker;

		// Token: 0x0400020E RID: 526
		private string[] m_args;

		// Token: 0x0400020F RID: 527
		private BlockStateTransitionSynchronizer m_stateSynchronizer;

		// Token: 0x04000210 RID: 528
		private bool m_shutdownRequested;

		// Token: 0x04000211 RID: 529
		private object m_shutdownRequestedLock;

		// Token: 0x04000212 RID: 530
		private int m_returnCode;

		// Token: 0x04000213 RID: 531
		private NativeMethods.ConsoleCtrlDelegate m_consoleControlDel;

		// Token: 0x04000214 RID: 532
		public const string AllowControlCancelRequestsToPropagate = "-AllowControlCancelRequestsToPropagate";
	}
}
