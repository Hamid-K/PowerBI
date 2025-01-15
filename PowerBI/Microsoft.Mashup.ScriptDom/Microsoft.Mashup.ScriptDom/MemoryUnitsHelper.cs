using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000159 RID: 345
	internal class MemoryUnitsHelper : OptionsHelper<MemoryUnit>
	{
		// Token: 0x06002104 RID: 8452 RVA: 0x0015C4A4 File Offset: 0x0015A6A4
		private MemoryUnitsHelper()
		{
			base.AddOptionMapping(MemoryUnit.KB, "KB");
			base.AddOptionMapping(MemoryUnit.MB, "MB");
			base.AddOptionMapping(MemoryUnit.GB, "GB");
			base.AddOptionMapping(MemoryUnit.TB, "TB");
			base.AddOptionMapping(MemoryUnit.Percent, TSqlTokenType.PercentSign);
		}

		// Token: 0x0400188B RID: 6283
		internal static readonly MemoryUnitsHelper Instance = new MemoryUnitsHelper();
	}
}
