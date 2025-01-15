using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Microsoft.PowerBI.DataMovement.Pipeline.Diagnostics
{
	// Token: 0x02000013 RID: 19
	[NullableContext(1)]
	public interface ITelemetryService
	{
		// Token: 0x06000097 RID: 151
		ActivityInfo GetCurrentActivityInfo();

		// Token: 0x06000098 RID: 152
		IDisposable SetExternalActivity(ActivityInfo externalActivity);

		// Token: 0x06000099 RID: 153
		Task ExecuteInTopLevelActivity(PipelineActivityType pipelineActivityType, Func<Task> action);

		// Token: 0x0600009A RID: 154
		Task ExecuteInActivity(PipelineActivityType pipelineActivityType, Func<Task> action);

		// Token: 0x0600009B RID: 155
		Task<T> ExecuteInTopLevelActivity<[Nullable(2)] T>(PipelineActivityType pipelineActivityType, Func<Task<T>> action);

		// Token: 0x0600009C RID: 156
		Task<T> ExecuteInActivity<[Nullable(2)] T>(PipelineActivityType pipelineActivityType, Func<Task<T>> action);

		// Token: 0x0600009D RID: 157
		void ExecuteInTopLevelActivity(PipelineActivityType pipelineActivityType, Action action);

		// Token: 0x0600009E RID: 158
		void ExecuteInActivity(PipelineActivityType pipelineActivityType, Action action);

		// Token: 0x0600009F RID: 159
		T ExecuteInTopLevelActivity<[Nullable(2)] T>(PipelineActivityType pipelineActivityType, Func<T> action);

		// Token: 0x060000A0 RID: 160
		T ExecuteInActivity<[Nullable(2)] T>(PipelineActivityType pipelineActivityType, Func<T> action);

		// Token: 0x060000A1 RID: 161
		void LogRemoteActivityExecutionInfo(IReadOnlyList<ActivityExecutionInfo> activityExecutionInfos);

		// Token: 0x060000A2 RID: 162
		void SetPipelineId(Guid pipelineId);
	}
}
