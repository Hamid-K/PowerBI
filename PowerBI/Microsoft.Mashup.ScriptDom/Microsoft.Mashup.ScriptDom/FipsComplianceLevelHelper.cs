using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200014A RID: 330
	internal class FipsComplianceLevelHelper : OptionsHelper<FipsComplianceLevel>
	{
		// Token: 0x060014DC RID: 5340 RVA: 0x00091387 File Offset: 0x0008F587
		private FipsComplianceLevelHelper()
		{
			base.AddOptionMapping(FipsComplianceLevel.Off, TSqlTokenType.Off);
			base.AddOptionMapping(FipsComplianceLevel.Entry, "ENTRY");
			base.AddOptionMapping(FipsComplianceLevel.Intermediate, "INTERMEDIATE");
			base.AddOptionMapping(FipsComplianceLevel.Full, TSqlTokenType.Full);
		}

		// Token: 0x040011F1 RID: 4593
		internal static readonly FipsComplianceLevelHelper Instance = new FipsComplianceLevelHelper();
	}
}
