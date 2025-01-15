using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020000AE RID: 174
	[Serializable]
	internal class RetentionUnitHelper : OptionsHelper<TimeUnit>
	{
		// Token: 0x060002C6 RID: 710 RVA: 0x0000BC53 File Offset: 0x00009E53
		private RetentionUnitHelper()
		{
			base.AddOptionMapping(TimeUnit.Days, "DAYS");
			base.AddOptionMapping(TimeUnit.Hours, "HOURS");
			base.AddOptionMapping(TimeUnit.Minutes, "MINUTES");
		}

		// Token: 0x04000400 RID: 1024
		internal static readonly RetentionUnitHelper Instance = new RetentionUnitHelper();
	}
}
