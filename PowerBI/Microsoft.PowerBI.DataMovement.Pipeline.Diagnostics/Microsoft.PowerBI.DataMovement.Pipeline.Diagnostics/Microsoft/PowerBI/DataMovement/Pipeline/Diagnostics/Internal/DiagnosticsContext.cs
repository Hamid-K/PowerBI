using System;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Messaging;
using System.Threading.Tasks;

namespace Microsoft.PowerBI.DataMovement.Pipeline.Diagnostics.Internal
{
	// Token: 0x020000D7 RID: 215
	[NullableContext(1)]
	[Nullable(0)]
	internal static class DiagnosticsContext
	{
		// Token: 0x17000143 RID: 323
		// (get) Token: 0x060010C2 RID: 4290 RVA: 0x000462C8 File Offset: 0x000444C8
		internal static ITelemetryService TelemetryService
		{
			get
			{
				object obj = CallContext.LogicalGetData("Microsoft.PowerBI.DataMovement.Pipeline.Common.TelemetryService");
				if (obj == null)
				{
					return DiagnosticsContext.s_globalTelemetryService;
				}
				return (ITelemetryService)obj;
			}
		}

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x060010C3 RID: 4291 RVA: 0x000462F0 File Offset: 0x000444F0
		internal static ITracingService TracingService
		{
			get
			{
				object obj = CallContext.LogicalGetData("Microsoft.PowerBI.DataMovement.Pipeline.Common.TracingService");
				if (obj == null)
				{
					return DiagnosticsContext.s_globalTracingService;
				}
				return (ITracingService)obj;
			}
		}

		// Token: 0x060010C4 RID: 4292 RVA: 0x00046318 File Offset: 0x00044518
		internal static IDisposable SetupGlobal(ITelemetryService telemetryService, ITracingService tracingService)
		{
			RuntimeChecks.Check(telemetryService != null, "telemetryService != null");
			RuntimeChecks.Check(tracingService != null, "tracingService != null");
			object obj = DiagnosticsContext.s_globalDiagnosticsLocker;
			IDisposable disposable;
			lock (obj)
			{
				DiagnosticsContext.s_globalTelemetryService = telemetryService;
				DiagnosticsContext.s_globalTracingService = tracingService;
				DiagnosticsContext.s_hasGlobalDiagnostics = true;
				disposable = new DiagnosticsLifetimeManager(delegate
				{
					RuntimeChecks.Check(DiagnosticsContext.s_hasGlobalDiagnostics, "s_hasGlobalDiagnostics == true");
					DiagnosticsContext.s_hasGlobalDiagnostics = false;
					DiagnosticsContext.s_globalTracingService = DiagnosticsContext.s_voidTracingService;
					DiagnosticsContext.s_globalTelemetryService = DiagnosticsContext.s_voidTelemetryService;
				});
			}
			return disposable;
		}

		// Token: 0x060010C5 RID: 4293 RVA: 0x000463A8 File Offset: 0x000445A8
		internal static DiagnosticsContext.DiagnosticsContextExecutor CreateDiagnosticsContextExecutor(ITelemetryService callTelemetryService, ITracingService callTracingService, ActivityInfo externalParentActivityInfo = null)
		{
			return new DiagnosticsContext.DiagnosticsContextExecutor(callTelemetryService, callTracingService, externalParentActivityInfo);
		}

		// Token: 0x060010C6 RID: 4294 RVA: 0x000463B4 File Offset: 0x000445B4
		private static IDisposable SetupCallScope(ITelemetryService callTelemetryService, ITracingService callTracingService)
		{
			RuntimeChecks.Check(callTelemetryService != null, "callTelemetryService != null");
			RuntimeChecks.Check(callTracingService != null, "callTracingService != null");
			object originalCallContextTelemetryService = CallContext.LogicalGetData("Microsoft.PowerBI.DataMovement.Pipeline.Common.TelemetryService");
			object originalCallContextTracingService = CallContext.LogicalGetData("Microsoft.PowerBI.DataMovement.Pipeline.Common.TracingService");
			CallContext.LogicalSetData("Microsoft.PowerBI.DataMovement.Pipeline.Common.TelemetryService", callTelemetryService);
			CallContext.LogicalSetData("Microsoft.PowerBI.DataMovement.Pipeline.Common.TracingService", callTracingService);
			return new DiagnosticsLifetimeManager(delegate
			{
				CallContext.LogicalSetData("Microsoft.PowerBI.DataMovement.Pipeline.Common.TelemetryService", originalCallContextTelemetryService);
				CallContext.LogicalSetData("Microsoft.PowerBI.DataMovement.Pipeline.Common.TracingService", originalCallContextTracingService);
			});
		}

		// Token: 0x0400035C RID: 860
		private const string c_telemetryServiceKey = "Microsoft.PowerBI.DataMovement.Pipeline.Common.TelemetryService";

		// Token: 0x0400035D RID: 861
		private const string c_tracingServiceKey = "Microsoft.PowerBI.DataMovement.Pipeline.Common.TracingService";

		// Token: 0x0400035E RID: 862
		private static readonly ITelemetryService s_voidTelemetryService = new VoidTelemetryService();

		// Token: 0x0400035F RID: 863
		private static readonly ITracingService s_voidTracingService = new VoidTracingService();

		// Token: 0x04000360 RID: 864
		private static bool s_hasGlobalDiagnostics;

		// Token: 0x04000361 RID: 865
		private static readonly object s_globalDiagnosticsLocker = new object();

		// Token: 0x04000362 RID: 866
		private static ITelemetryService s_globalTelemetryService = DiagnosticsContext.s_voidTelemetryService;

		// Token: 0x04000363 RID: 867
		private static ITracingService s_globalTracingService = DiagnosticsContext.s_voidTracingService;

		// Token: 0x020000E8 RID: 232
		[Nullable(0)]
		internal sealed class DiagnosticsContextExecutor
		{
			// Token: 0x06001132 RID: 4402 RVA: 0x00046BAA File Offset: 0x00044DAA
			internal DiagnosticsContextExecutor(ITelemetryService hostTelemetryService, ITracingService hostTracingService, ActivityInfo externalParentActivityInfo = null)
			{
				RuntimeChecks.Check(hostTelemetryService != null, "hostTelemetryService != null");
				RuntimeChecks.Check(hostTracingService != null, "hostTracingService != null");
				this.m_externalParentActivityInfo = externalParentActivityInfo;
				this.m_hostTelemetryService = hostTelemetryService;
				this.m_hostTracingService = hostTracingService;
			}

			// Token: 0x1700014D RID: 333
			// (get) Token: 0x06001133 RID: 4403 RVA: 0x00046BE3 File Offset: 0x00044DE3
			internal ITelemetryService TelemetryService
			{
				get
				{
					return this.m_hostTelemetryService;
				}
			}

			// Token: 0x1700014E RID: 334
			// (get) Token: 0x06001134 RID: 4404 RVA: 0x00046BEB File Offset: 0x00044DEB
			internal ITracingService TracingService
			{
				get
				{
					return this.m_hostTracingService;
				}
			}

			// Token: 0x06001135 RID: 4405 RVA: 0x00046BF4 File Offset: 0x00044DF4
			internal async Task ExecuteInTopLevelActivity(PipelineActivityType pipelineActivityType, Func<Task> action)
			{
				using (this.CreateParentActivityScopeIfPresent())
				{
					using (DiagnosticsContext.SetupCallScope(this.m_hostTelemetryService, this.m_hostTracingService))
					{
						await this.TelemetryService.ExecuteInTopLevelActivity(pipelineActivityType, action);
					}
					IDisposable callScope = null;
				}
				IDisposable parentActivityScope = null;
			}

			// Token: 0x06001136 RID: 4406 RVA: 0x00046C48 File Offset: 0x00044E48
			internal async Task<T> ExecuteInTopLevelActivity<[Nullable(2)] T>(PipelineActivityType pipelineActivityType, Func<Task<T>> action)
			{
				T t;
				using (this.CreateParentActivityScopeIfPresent())
				{
					using (DiagnosticsContext.SetupCallScope(this.m_hostTelemetryService, this.m_hostTracingService))
					{
						t = await this.TelemetryService.ExecuteInTopLevelActivity<T>(pipelineActivityType, action);
					}
				}
				return t;
			}

			// Token: 0x06001137 RID: 4407 RVA: 0x00046C9C File Offset: 0x00044E9C
			internal void ExecuteInTopLevelActivity(PipelineActivityType pipelineActivityType, Action action)
			{
				using (this.CreateParentActivityScopeIfPresent())
				{
					using (DiagnosticsContext.SetupCallScope(this.m_hostTelemetryService, this.m_hostTracingService))
					{
						this.TelemetryService.ExecuteInTopLevelActivity(pipelineActivityType, action);
					}
				}
			}

			// Token: 0x06001138 RID: 4408 RVA: 0x00046D04 File Offset: 0x00044F04
			internal T ExecuteInTopLevelActivity<[Nullable(2)] T>(PipelineActivityType pipelineActivityType, Func<T> action)
			{
				T t;
				using (this.CreateParentActivityScopeIfPresent())
				{
					using (DiagnosticsContext.SetupCallScope(this.m_hostTelemetryService, this.m_hostTracingService))
					{
						t = this.TelemetryService.ExecuteInTopLevelActivity<T>(pipelineActivityType, action);
					}
				}
				return t;
			}

			// Token: 0x06001139 RID: 4409 RVA: 0x00046D6C File Offset: 0x00044F6C
			private IDisposable CreateParentActivityScopeIfPresent()
			{
				if (this.m_externalParentActivityInfo == null)
				{
					return null;
				}
				return this.TelemetryService.SetExternalActivity(this.m_externalParentActivityInfo);
			}

			// Token: 0x04000374 RID: 884
			private readonly ITelemetryService m_hostTelemetryService;

			// Token: 0x04000375 RID: 885
			private readonly ITracingService m_hostTracingService;

			// Token: 0x04000376 RID: 886
			private readonly ActivityInfo m_externalParentActivityInfo;
		}
	}
}
