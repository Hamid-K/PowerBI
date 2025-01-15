using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020000BE RID: 190
	internal class SessionOptionUnitHelper : OptionsHelper<MemoryUnit>
	{
		// Token: 0x060002DA RID: 730 RVA: 0x0000C971 File Offset: 0x0000AB71
		private SessionOptionUnitHelper()
		{
			base.AddOptionMapping(MemoryUnit.KB, "KB");
			base.AddOptionMapping(MemoryUnit.MB, "MB");
		}

		// Token: 0x040005C5 RID: 1477
		internal static readonly SessionOptionUnitHelper Instance = new SessionOptionUnitHelper();
	}
}
