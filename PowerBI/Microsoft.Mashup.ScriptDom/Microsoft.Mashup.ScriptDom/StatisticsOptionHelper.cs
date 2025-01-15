using System;
using antlr;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200011A RID: 282
	internal class StatisticsOptionHelper : OptionsHelper<StatisticsOptionKind>
	{
		// Token: 0x060014A7 RID: 5287 RVA: 0x000908D7 File Offset: 0x0008EAD7
		private StatisticsOptionHelper()
		{
			base.AddOptionMapping(StatisticsOptionKind.FullScan, "FULLSCAN");
			base.AddOptionMapping(StatisticsOptionKind.NoRecompute, "NORECOMPUTE");
			base.AddOptionMapping(StatisticsOptionKind.Resample, "RESAMPLE");
			base.AddOptionMapping(StatisticsOptionKind.Columns, "COLUMNS");
		}

		// Token: 0x060014A8 RID: 5288 RVA: 0x00090910 File Offset: 0x0008EB10
		protected override TSqlParseErrorException GetMatchingException(IToken token)
		{
			return new TSqlParseErrorException(TSql80ParserBaseInternal.CreateParseError("SQL46020", token, TSqlParserResource.SQL46020Message, new string[] { token.getText() }));
		}

		// Token: 0x0400111A RID: 4378
		internal static readonly StatisticsOptionHelper Instance = new StatisticsOptionHelper();
	}
}
