using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000100 RID: 256
	internal class AttachModeHelper : OptionsHelper<AttachMode>
	{
		// Token: 0x06001490 RID: 5264 RVA: 0x00090382 File Offset: 0x0008E582
		private AttachModeHelper()
		{
			base.AddOptionMapping(AttachMode.Attach, "ATTACH");
			base.AddOptionMapping(AttachMode.AttachRebuildLog, "ATTACH_REBUILD_LOG");
			base.AddOptionMapping(AttachMode.AttachForceRebuildLog, "ATTACH_FORCE_REBUILD_LOG");
			base.AddOptionMapping(AttachMode.Load, "LOAD");
		}

		// Token: 0x04000B2E RID: 2862
		internal static readonly AttachModeHelper Instance = new AttachModeHelper();
	}
}
