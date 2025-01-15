using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime;
using JetBrains.Annotations;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Modularization
{
	// Token: 0x020000A2 RID: 162
	public abstract class ApplicationRoot : BlockHost
	{
		// Token: 0x06000480 RID: 1152 RVA: 0x00010868 File Offset: 0x0000EA68
		protected ApplicationRoot(string name)
			: base("ApplicationRoot:" + name)
		{
			Library.Initialize();
			ExtendedEnvironment.OnCrash += this.CrashHandler;
			Anchor.Tweaks.RegisterTweaksFile(ExtendedAssembly.GetExecutingAssembly(base.GetType()));
			Anchor.Tweaks.RegisterTweaksFile(Tweaks.GetFileNameWithTweakExtension("ApplicationRoot"));
			this.m_name = name;
			this.m_locker = new object();
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x06000481 RID: 1153 RVA: 0x000108D7 File Offset: 0x0000EAD7
		protected IApplicationRootHost ApplicationRootHost
		{
			get
			{
				return this.m_applicationHost;
			}
		}

		// Token: 0x06000482 RID: 1154 RVA: 0x000108DF File Offset: 0x0000EADF
		protected new void AddBlock(IBlock block)
		{
			base.AddBlock(block, false);
		}

		// Token: 0x06000483 RID: 1155 RVA: 0x000108EC File Offset: 0x0000EAEC
		protected new void AddBlockIfStarted(IBlock block)
		{
			this.m_applicationHost.InvokeCallbackIfInState(delegate
			{
				this.AddBlockIfStarted(block);
			}, BlockState.Started);
		}

		// Token: 0x06000484 RID: 1156 RVA: 0x00010928 File Offset: 0x0000EB28
		protected new void RemoveBlockIfStarted(IBlock block)
		{
			this.m_applicationHost.InvokeCallbackIfInState(delegate
			{
				this.RemoveBlockIfStarted(block);
			}, BlockState.Started);
		}

		// Token: 0x06000485 RID: 1157 RVA: 0x00010964 File Offset: 0x0000EB64
		protected new void AddBlocks(IEnumerable<IBlock> blocksToAdd)
		{
			foreach (IBlock block in blocksToAdd)
			{
				base.AddBlock(block, false);
			}
		}

		// Token: 0x06000486 RID: 1158 RVA: 0x000109B0 File Offset: 0x0000EBB0
		protected virtual void ValidateRuntimeRequirements()
		{
			if (!string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable("BI_ONEBOX")))
			{
				Console.WriteLine("Skipping ValidateRuntimeRequirements for type {0}", base.GetType().FullName);
				return;
			}
			if (Environment.ProcessorCount != 1 && !GCSettings.IsServerGC)
			{
				ProcessWellKnownHost wellKnownHost = CurrentProcess.WellKnownHost;
				if (!CurrentProcess.IsWatchdogProcess && wellKnownHost != ProcessWellKnownHost.MSTest && wellKnownHost != ProcessWellKnownHost.WebDev && wellKnownHost != ProcessWellKnownHost.AzureWorker && wellKnownHost != ProcessWellKnownHost.AzureWeb && wellKnownHost != ProcessWellKnownHost.UIAF)
				{
					string text = string.Format(CultureInfo.CurrentCulture, "Server GC must be enabled. Please make sure that on the same directory as the executable ({0}) there is an app.config file (called {0}.config) that includes the following as a sub-element of the <configuration> root element: <runtime><gcServer enabled='true'/></runtime>.", new object[] { CurrentProcess.MainModuleShortFileName });
					TraceSourceBase<ModularizationFrameworkTrace>.Tracer.Trace(TraceVerbosity.Error, "{0}", new object[] { text });
				}
			}
		}

		// Token: 0x06000487 RID: 1159 RVA: 0x00010A4C File Offset: 0x0000EC4C
		protected virtual ActivityFactory GetActivityFactoryInstance()
		{
			return new ActivityFactory();
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x06000488 RID: 1160 RVA: 0x00010A53 File Offset: 0x0000EC53
		public string Name
		{
			get
			{
				return this.m_name;
			}
		}

		// Token: 0x06000489 RID: 1161 RVA: 0x00010A5B File Offset: 0x0000EC5B
		public void Initialize(IApplicationRootHost host, ApplicationSwitchesTypes appSwitchesType)
		{
			this.Initialize(host, new string[] { "" }, appSwitchesType);
		}

		// Token: 0x0600048A RID: 1162 RVA: 0x00010A74 File Offset: 0x0000EC74
		public void Initialize([NotNull] IApplicationRootHost host, string[] args, ApplicationSwitchesTypes appSwitchesType)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<IApplicationRootHost>(host, "host");
			this.m_applicationHost = host;
			ApplicationSwitches applicationSwitches = new ApplicationSwitches(args, appSwitchesType);
			applicationSwitches.RegisterSwitch("breakOnStart", "break", "Set this flag to offer the JIT debugger a chance to run on start", ParameterType.Boolean, false, "false");
			if (bool.Parse(applicationSwitches["breakOnStart"]))
			{
				host.AlertDebugger();
			}
			else
			{
				string value = ApplicationRoot.s_breakOnStartTweak.Value;
				if (!string.IsNullOrWhiteSpace(value))
				{
					string processName = CurrentProcess.MainModuleShortFileName;
					if ((from s in value.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries)
						select s.Trim()).ToArray<string>().FirstPosition((string s) => s.Equals(processName, StringComparison.OrdinalIgnoreCase)) >= 0)
					{
						host.AlertDebugger();
					}
				}
			}
			this.ValidateRuntimeRequirements();
			this.AddBlock(applicationSwitches);
			this.AddBlock(this.GetActivityFactoryInstance());
			base.Initialize(applicationSwitches);
		}

		// Token: 0x0600048B RID: 1163 RVA: 0x00010B6C File Offset: 0x0000ED6C
		protected override void OnRequestShutdown(IBlock requestor, int returnCode)
		{
			bool flag = false;
			object locker = this.m_locker;
			lock (locker)
			{
				if (!this.m_shutdownStarted)
				{
					this.m_shutdownStarted = true;
					flag = true;
				}
			}
			if (flag)
			{
				this.m_applicationHost.RequestShutdown(requestor, returnCode);
			}
		}

		// Token: 0x0600048C RID: 1164 RVA: 0x00010BCC File Offset: 0x0000EDCC
		protected override void OnPostShutdown()
		{
			if (ApplicationRoot.s_garbageCollectionOnShutdownTweakTweak.Value)
			{
				TraceSourceBase<ModularizationFrameworkTrace>.Tracer.Trace(TraceVerbosity.Verbose, "Force an immediate garbage collection of all generations (GarbageCollectionOnShutdown tweak is set).");
				ExtendedGC.CollectEverything();
			}
		}

		// Token: 0x0600048D RID: 1165 RVA: 0x00009B3B File Offset: 0x00007D3B
		protected virtual void OnCrash(object sender, CrashEventArgs args)
		{
		}

		// Token: 0x0600048E RID: 1166 RVA: 0x00010BEF File Offset: 0x0000EDEF
		protected override void OnShutdown()
		{
			base.OnShutdown();
			ExtendedEnvironment.OnCrash -= this.CrashHandler;
		}

		// Token: 0x0600048F RID: 1167 RVA: 0x00010C08 File Offset: 0x0000EE08
		private void CrashHandler(object sender, CrashEventArgs args)
		{
			this.OnCrash(sender, args);
			if (ApplicationRoot.s_failFastOnUnhandledExceptionsEnabledTweak.Value)
			{
				TraceSourceBase<ModularizationFrameworkTrace>.Tracer.Trace(TraceVerbosity.Fatal, "Process terminated (FailFastOnUnhandledExceptionEnabled tweak is set)");
				ExtendedDiagnostics.TerminateCurrentProcess(1);
			}
		}

		// Token: 0x0400018A RID: 394
		private readonly string m_name;

		// Token: 0x0400018B RID: 395
		private object m_locker;

		// Token: 0x0400018C RID: 396
		private bool m_shutdownStarted;

		// Token: 0x0400018D RID: 397
		private IApplicationRootHost m_applicationHost;

		// Token: 0x0400018E RID: 398
		private const string c_failFastOnUnhandledExceptionsEnabledTweakName = "Microsoft.Cloud.Platform.Utils.ApplicationRoot.FailFastOnUnhandledExceptionsEnabled";

		// Token: 0x0400018F RID: 399
		private static Tweak<bool> s_failFastOnUnhandledExceptionsEnabledTweak = Anchor.Tweaks.RegisterTweak<bool>("Microsoft.Cloud.Platform.Utils.ApplicationRoot.FailFastOnUnhandledExceptionsEnabled", "When set, unhandled exceptions result in a fail-fast (no UI presented)", false);

		// Token: 0x04000190 RID: 400
		private const string c_garbageCollectionOnShutdownTweakName = "Microsoft.Cloud.Platform.Utils.ApplicationRoot.GarbageCollectionOnShutdown";

		// Token: 0x04000191 RID: 401
		private static Tweak<bool> s_garbageCollectionOnShutdownTweakTweak = Anchor.Tweaks.RegisterTweak<bool>("Microsoft.Cloud.Platform.Utils.ApplicationRoot.GarbageCollectionOnShutdown", "When set, garbage collection (and wait for pending finalizers to complete) will be forced on application root shutdown.", false);

		// Token: 0x04000192 RID: 402
		private const string c_breakOnStartTweakName = "Microsoft.Cloud.Platform.Utils.ApplicationRoot.BreakOnStart";

		// Token: 0x04000193 RID: 403
		private static Tweak<string> s_breakOnStartTweak = Anchor.Tweaks.RegisterTweak<string>("Microsoft.Cloud.Platform.Utils.ApplicationRoot.BreakOnStart", "A semicolon-separated list of process names (e.g., notepad.exe) whose startup sequence we want to debug", "");
	}
}
