using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020000AB RID: 171
	[Serializable]
	internal class ResourcePoolParameterHelper : OptionsHelper<ResourcePoolParameterType>
	{
		// Token: 0x060002C4 RID: 708 RVA: 0x0000BBB4 File Offset: 0x00009DB4
		private ResourcePoolParameterHelper()
		{
			base.AddOptionMapping(ResourcePoolParameterType.MaxCpuPercent, "MAX_CPU_PERCENT");
			base.AddOptionMapping(ResourcePoolParameterType.MaxMemoryPercent, "MAX_MEMORY_PERCENT");
			base.AddOptionMapping(ResourcePoolParameterType.MinCpuPercent, "MIN_CPU_PERCENT");
			base.AddOptionMapping(ResourcePoolParameterType.MinMemoryPercent, "MIN_MEMORY_PERCENT");
			base.AddOptionMapping(ResourcePoolParameterType.CapCpuPercent, "CAP_CPU_PERCENT", SqlVersionFlags.TSql110);
			base.AddOptionMapping(ResourcePoolParameterType.TargetMemoryPercent, "TARGET_MEMORY_PERCENT", SqlVersionFlags.TSql110);
			base.AddOptionMapping(ResourcePoolParameterType.MinIoPercent, "MIN_IO_PERCENT", SqlVersionFlags.TSql110);
			base.AddOptionMapping(ResourcePoolParameterType.MaxIoPercent, "MAX_IO_PERCENT", SqlVersionFlags.TSql110);
			base.AddOptionMapping(ResourcePoolParameterType.CapIoPercent, "CAP_IO_PERCENT", SqlVersionFlags.TSql110);
			base.AddOptionMapping(ResourcePoolParameterType.Affinity, "AFFINITY", SqlVersionFlags.TSql110);
		}

		// Token: 0x040003EF RID: 1007
		internal static readonly ResourcePoolParameterHelper Instance = new ResourcePoolParameterHelper();
	}
}
