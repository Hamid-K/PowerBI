using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000170 RID: 368
	[Serializable]
	internal class SetStatisticsOptionsHelper : OptionsHelper<SetStatisticsOptions>
	{
		// Token: 0x0600211E RID: 8478 RVA: 0x0015C8DC File Offset: 0x0015AADC
		private SetStatisticsOptionsHelper()
		{
			base.AddOptionMapping(SetStatisticsOptions.IO, "IO", SqlVersionFlags.TSqlAll);
			base.AddOptionMapping(SetStatisticsOptions.Profile, "PROFILE", SqlVersionFlags.TSqlAll);
			base.AddOptionMapping(SetStatisticsOptions.Time, "TIME", SqlVersionFlags.TSqlAll);
			base.AddOptionMapping(SetStatisticsOptions.Xml, "XML", SqlVersionFlags.TSql90AndAbove);
		}

		// Token: 0x040018F9 RID: 6393
		internal static readonly SetStatisticsOptionsHelper Instance = new SetStatisticsOptionsHelper();
	}
}
