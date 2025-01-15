using System;
using antlr;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200004C RID: 76
	internal class ComputeFunctionTypeHelper : OptionsHelper<ComputeFunctionType>
	{
		// Token: 0x060001E3 RID: 483 RVA: 0x00006274 File Offset: 0x00004474
		private ComputeFunctionTypeHelper()
		{
			base.AddOptionMapping(ComputeFunctionType.Count, "COUNT");
			base.AddOptionMapping(ComputeFunctionType.CountBig, "COUNT_BIG");
			base.AddOptionMapping(ComputeFunctionType.Max, "MAX");
			base.AddOptionMapping(ComputeFunctionType.Min, "MIN");
			base.AddOptionMapping(ComputeFunctionType.Sum, "SUM");
			base.AddOptionMapping(ComputeFunctionType.Avg, "AVG");
			base.AddOptionMapping(ComputeFunctionType.Var, "VAR");
			base.AddOptionMapping(ComputeFunctionType.Varp, "VARP");
			base.AddOptionMapping(ComputeFunctionType.Stdev, "STDEV");
			base.AddOptionMapping(ComputeFunctionType.Stdevp, "STDEVP");
			base.AddOptionMapping(ComputeFunctionType.ChecksumAgg, "CHECKSUM_AGG");
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x00006310 File Offset: 0x00004510
		protected override TSqlParseErrorException GetMatchingException(IToken token)
		{
			return new TSqlParseErrorException(TSql80ParserBaseInternal.CreateParseError("SQL46024", token, TSqlParserResource.SQL46024Message, new string[] { token.getText() }));
		}

		// Token: 0x04000159 RID: 345
		internal static readonly ComputeFunctionTypeHelper Instance = new ComputeFunctionTypeHelper();
	}
}
