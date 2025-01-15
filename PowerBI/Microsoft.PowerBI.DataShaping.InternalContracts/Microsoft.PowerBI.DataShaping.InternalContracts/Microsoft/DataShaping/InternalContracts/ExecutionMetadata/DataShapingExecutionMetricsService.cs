using System;
using Microsoft.InfoNav.Data.Contracts.ExecutionMetadata;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.DataShaping.InternalContracts.ExecutionMetadata
{
	// Token: 0x02000029 RID: 41
	public sealed class DataShapingExecutionMetricsService : IDataShapingExecutionMetricsService, IExecutionMetricsService
	{
		// Token: 0x060000F8 RID: 248 RVA: 0x00003BAA File Offset: 0x00001DAA
		public DataShapingExecutionMetricsService(ExecutionMetricsKind requestedMetricsKind, int? maxEventCount)
		{
			this._coreService = new ExecutionMetricsService(maxEventCount);
			this.RequestedMetricsKind = requestedMetricsKind;
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000F9 RID: 249 RVA: 0x00003BC5 File Offset: 0x00001DC5
		public ExecutionMetricsKind RequestedMetricsKind { get; }

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000FA RID: 250 RVA: 0x00003BCD File Offset: 0x00001DCD
		public bool ExceededMaxEventCount
		{
			get
			{
				return this._coreService.ExceededMaxEventCount;
			}
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00003BDA File Offset: 0x00001DDA
		public void AttachExternalEvent(ExecutionEvent execEvent)
		{
			this._coreService.AttachExternalEvent(execEvent);
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00003BE8 File Offset: 0x00001DE8
		public ITimedEventTracker BeginEvent(string eventName, string componentName)
		{
			return this._coreService.BeginEvent(eventName, componentName);
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00003BF7 File Offset: 0x00001DF7
		public IInstantEventTracker FireInstantEvent(string eventName, string componentName, bool bypassMaxEventCount = false)
		{
			return this._coreService.FireInstantEvent(eventName, componentName, bypassMaxEventCount);
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00003C07 File Offset: 0x00001E07
		public ExecutionMetrics ToExecutionMetrics()
		{
			return this._coreService.ToExecutionMetrics();
		}

		// Token: 0x04000078 RID: 120
		private readonly ExecutionMetricsService _coreService;
	}
}
