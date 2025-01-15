using System;
using Microsoft.InfoNav.Data.Contracts.ExecutionMetadata;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.DataShaping.InternalContracts.ExecutionMetadata
{
	// Token: 0x0200002D RID: 45
	public sealed class NoOpDataShapingExecutionMetricsService : IDataShapingExecutionMetricsService, IExecutionMetricsService
	{
		// Token: 0x06000101 RID: 257 RVA: 0x00003C26 File Offset: 0x00001E26
		private NoOpDataShapingExecutionMetricsService()
		{
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000102 RID: 258 RVA: 0x00003C2E File Offset: 0x00001E2E
		public static NoOpDataShapingExecutionMetricsService Instance
		{
			get
			{
				return NoOpDataShapingExecutionMetricsService._instance;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000103 RID: 259 RVA: 0x00003C35 File Offset: 0x00001E35
		public ExecutionMetricsKind RequestedMetricsKind
		{
			get
			{
				return ExecutionMetricsKind.None;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000104 RID: 260 RVA: 0x00003C38 File Offset: 0x00001E38
		public bool ExceededMaxEventCount
		{
			get
			{
				return NoOpExecutionMetricsService.Instance.ExceededMaxEventCount;
			}
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00003C44 File Offset: 0x00001E44
		public void AttachExternalEvent(ExecutionEvent execEvent)
		{
			NoOpExecutionMetricsService.Instance.AttachExternalEvent(execEvent);
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00003C51 File Offset: 0x00001E51
		public ITimedEventTracker BeginEvent(string eventName, string componentName)
		{
			return NoOpExecutionMetricsService.Instance.BeginEvent(eventName, componentName);
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00003C5F File Offset: 0x00001E5F
		public IInstantEventTracker FireInstantEvent(string eventName, string componentName, bool bypassMaxEventCount = false)
		{
			return NoOpExecutionMetricsService.Instance.FireInstantEvent(eventName, componentName, bypassMaxEventCount);
		}

		// Token: 0x06000108 RID: 264 RVA: 0x00003C6E File Offset: 0x00001E6E
		public ExecutionMetrics ToExecutionMetrics()
		{
			return NoOpExecutionMetricsService.Instance.ToExecutionMetrics();
		}

		// Token: 0x04000085 RID: 133
		private static readonly NoOpDataShapingExecutionMetricsService _instance = new NoOpDataShapingExecutionMetricsService();
	}
}
