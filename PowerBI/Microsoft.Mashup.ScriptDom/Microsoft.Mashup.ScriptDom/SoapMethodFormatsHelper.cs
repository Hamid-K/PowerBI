using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000173 RID: 371
	[Serializable]
	internal class SoapMethodFormatsHelper : OptionsHelper<SoapMethodFormat>
	{
		// Token: 0x06002120 RID: 8480 RVA: 0x0015C928 File Offset: 0x0015AB28
		private SoapMethodFormatsHelper()
		{
			base.AddOptionMapping(SoapMethodFormat.AllResults, "ALL_RESULTS");
			base.AddOptionMapping(SoapMethodFormat.RowsetsOnly, "ROWSETS_ONLY");
			base.AddOptionMapping(SoapMethodFormat.None, "NONE");
		}

		// Token: 0x04001904 RID: 6404
		internal static readonly SoapMethodFormatsHelper Instance = new SoapMethodFormatsHelper();
	}
}
