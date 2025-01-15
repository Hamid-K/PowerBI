using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200009F RID: 159
	internal class EventSessionMemoryPartitionModeTypeHelper : OptionsHelper<EventSessionMemoryPartitionModeType>
	{
		// Token: 0x060002B6 RID: 694 RVA: 0x0000B99B File Offset: 0x00009B9B
		private EventSessionMemoryPartitionModeTypeHelper()
		{
			base.AddOptionMapping(EventSessionMemoryPartitionModeType.None, "NONE");
			base.AddOptionMapping(EventSessionMemoryPartitionModeType.PerCpu, "PER_CPU");
			base.AddOptionMapping(EventSessionMemoryPartitionModeType.PerNode, "PER_NODE");
		}

		// Token: 0x040003CF RID: 975
		internal static readonly EventSessionMemoryPartitionModeTypeHelper Instance = new EventSessionMemoryPartitionModeTypeHelper();
	}
}
