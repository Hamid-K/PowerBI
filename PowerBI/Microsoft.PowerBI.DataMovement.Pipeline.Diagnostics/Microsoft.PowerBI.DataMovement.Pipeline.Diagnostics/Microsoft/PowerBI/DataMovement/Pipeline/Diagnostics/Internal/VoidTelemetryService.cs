using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Microsoft.PowerBI.DataMovement.Pipeline.Diagnostics.Internal
{
	// Token: 0x020000E4 RID: 228
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class VoidTelemetryService : MarshalByRefObject, ITelemetryService
	{
		// Token: 0x06001114 RID: 4372 RVA: 0x000469AE File Offset: 0x00044BAE
		public ActivityInfo GetCurrentActivityInfo()
		{
			return ActivityInfo.Empty;
		}

		// Token: 0x06001115 RID: 4373 RVA: 0x000469B5 File Offset: 0x00044BB5
		public IDisposable SetExternalActivity(ActivityInfo parentActivity)
		{
			return new DiagnosticsLifetimeManager(delegate
			{
			});
		}

		// Token: 0x06001116 RID: 4374 RVA: 0x000469DC File Offset: 0x00044BDC
		public async Task ExecuteInTopLevelActivity(PipelineActivityType activityType, Func<Task> action)
		{
			await action();
		}

		// Token: 0x06001117 RID: 4375 RVA: 0x00046A20 File Offset: 0x00044C20
		public async Task ExecuteInActivity(PipelineActivityType pipelineActivityType, Func<Task> action)
		{
			await action();
		}

		// Token: 0x06001118 RID: 4376 RVA: 0x00046A64 File Offset: 0x00044C64
		public async Task<T> ExecuteInTopLevelActivity<[Nullable(2)] T>(PipelineActivityType activityType, Func<Task<T>> action)
		{
			return await action();
		}

		// Token: 0x06001119 RID: 4377 RVA: 0x00046AA8 File Offset: 0x00044CA8
		public async Task<T> ExecuteInActivity<[Nullable(2)] T>(PipelineActivityType pipelineActivityType, Func<Task<T>> action)
		{
			return await action();
		}

		// Token: 0x0600111A RID: 4378 RVA: 0x00046AEB File Offset: 0x00044CEB
		public void ExecuteInTopLevelActivity(PipelineActivityType activityType, Action action)
		{
			action();
		}

		// Token: 0x0600111B RID: 4379 RVA: 0x00046AF3 File Offset: 0x00044CF3
		public void ExecuteInActivity(PipelineActivityType pipelineActivityType, Action action)
		{
			action();
		}

		// Token: 0x0600111C RID: 4380 RVA: 0x00046AFB File Offset: 0x00044CFB
		public T ExecuteInTopLevelActivity<[Nullable(2)] T>(PipelineActivityType activityType, Func<T> action)
		{
			return action();
		}

		// Token: 0x0600111D RID: 4381 RVA: 0x00046B03 File Offset: 0x00044D03
		public T ExecuteInActivity<[Nullable(2)] T>(PipelineActivityType pipelineActivityType, Func<T> action)
		{
			return action();
		}

		// Token: 0x0600111E RID: 4382 RVA: 0x00046B0B File Offset: 0x00044D0B
		public void LogRemoteActivityExecutionInfo(IReadOnlyList<ActivityExecutionInfo> activityExecutionInfos)
		{
		}

		// Token: 0x0600111F RID: 4383 RVA: 0x00046B0D File Offset: 0x00044D0D
		public void SetPipelineId(Guid pipelineId)
		{
		}
	}
}
