using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000078 RID: 120
	internal class DatabaseOptionKindHelper : OptionsHelper<DatabaseOptionKind>
	{
		// Token: 0x06000296 RID: 662 RVA: 0x0000B404 File Offset: 0x00009604
		private DatabaseOptionKindHelper()
		{
			base.AddOptionMapping(DatabaseOptionKind.CompatibilityLevel, "COMPATIBILITY_LEVEL", SqlVersionFlags.TSql90AndAbove);
			base.AddOptionMapping(DatabaseOptionKind.DefaultFullTextLanguage, "DEFAULT_FULLTEXT_LANGUAGE", SqlVersionFlags.TSql110);
			base.AddOptionMapping(DatabaseOptionKind.DefaultLanguage, "DEFAULT_LANGUAGE", SqlVersionFlags.TSql110);
			base.AddOptionMapping(DatabaseOptionKind.TwoDigitYearCutoff, "TWO_DIGIT_YEAR_CUTOFF", SqlVersionFlags.TSql110);
			base.AddOptionMapping(DatabaseOptionKind.Edition, "EDITION", SqlVersionFlags.TSql110);
		}

		// Token: 0x040002B6 RID: 694
		internal static readonly DatabaseOptionKindHelper Instance = new DatabaseOptionKindHelper();
	}
}
