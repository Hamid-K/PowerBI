using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020000AA RID: 170
	[Serializable]
	internal class ResourcePoolAffinityHelper : OptionsHelper<ResourcePoolAffinityType>
	{
		// Token: 0x060002C2 RID: 706 RVA: 0x0000BB85 File Offset: 0x00009D85
		private ResourcePoolAffinityHelper()
		{
			base.AddOptionMapping(ResourcePoolAffinityType.Scheduler, "SCHEDULER");
			base.AddOptionMapping(ResourcePoolAffinityType.NumaNode, "NUMANODE");
		}

		// Token: 0x040003EE RID: 1006
		internal static readonly ResourcePoolAffinityHelper Instance = new ResourcePoolAffinityHelper();
	}
}
