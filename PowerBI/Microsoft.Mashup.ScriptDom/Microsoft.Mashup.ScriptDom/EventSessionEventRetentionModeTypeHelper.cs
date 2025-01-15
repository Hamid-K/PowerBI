using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200009D RID: 157
	internal class EventSessionEventRetentionModeTypeHelper : OptionsHelper<EventSessionEventRetentionModeType>
	{
		// Token: 0x060002B4 RID: 692 RVA: 0x0000B963 File Offset: 0x00009B63
		private EventSessionEventRetentionModeTypeHelper()
		{
			base.AddOptionMapping(EventSessionEventRetentionModeType.AllowMultipleEventLoss, "ALLOW_MULTIPLE_EVENT_LOSS");
			base.AddOptionMapping(EventSessionEventRetentionModeType.AllowSingleEventLoss, "ALLOW_SINGLE_EVENT_LOSS");
			base.AddOptionMapping(EventSessionEventRetentionModeType.NoEventLoss, "NO_EVENT_LOSS");
		}

		// Token: 0x040003C9 RID: 969
		internal static readonly EventSessionEventRetentionModeTypeHelper Instance = new EventSessionEventRetentionModeTypeHelper();
	}
}
