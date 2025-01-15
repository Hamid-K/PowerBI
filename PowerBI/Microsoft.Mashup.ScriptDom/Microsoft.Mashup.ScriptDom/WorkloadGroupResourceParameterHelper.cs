using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020000F7 RID: 247
	[Serializable]
	internal class WorkloadGroupResourceParameterHelper : OptionsHelper<WorkloadGroupParameterType>
	{
		// Token: 0x06001488 RID: 5256 RVA: 0x00090250 File Offset: 0x0008E450
		private WorkloadGroupResourceParameterHelper()
		{
			base.AddOptionMapping(WorkloadGroupParameterType.RequestMaxMemoryGrantPercent, "REQUEST_MAX_MEMORY_GRANT_PERCENT");
			base.AddOptionMapping(WorkloadGroupParameterType.RequestMaxCpuTimeSec, "REQUEST_MAX_CPU_TIME_SEC");
			base.AddOptionMapping(WorkloadGroupParameterType.RequestMemoryGrantTimeoutSec, "REQUEST_MEMORY_GRANT_TIMEOUT_SEC");
			base.AddOptionMapping(WorkloadGroupParameterType.MaxDop, "MAX_DOP");
			base.AddOptionMapping(WorkloadGroupParameterType.GroupMaxRequests, "GROUP_MAX_REQUESTS");
			base.AddOptionMapping(WorkloadGroupParameterType.GroupMinMemoryPercent, "GROUP_MIN_MEMORY_PERCENT", SqlVersionFlags.TSql110);
		}

		// Token: 0x04000B02 RID: 2818
		internal static readonly WorkloadGroupResourceParameterHelper Instance = new WorkloadGroupResourceParameterHelper();
	}
}
