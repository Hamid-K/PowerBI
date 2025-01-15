using System;
using antlr;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000091 RID: 145
	internal class GroupByOptionHelper : OptionsHelper<GroupByOption>
	{
		// Token: 0x060002A7 RID: 679 RVA: 0x0000B7C9 File Offset: 0x000099C9
		private GroupByOptionHelper()
		{
			base.AddOptionMapping(GroupByOption.Cube, "CUBE");
			base.AddOptionMapping(GroupByOption.Rollup, "ROLLUP");
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x0000B7EC File Offset: 0x000099EC
		protected override TSqlParseErrorException GetMatchingException(IToken token)
		{
			return new TSqlParseErrorException(TSql80ParserBaseInternal.CreateParseError("SQL46023", token, TSqlParserResource.SQL46023Message, new string[] { token.getText() }));
		}

		// Token: 0x0400039A RID: 922
		internal static readonly GroupByOptionHelper Instance = new GroupByOptionHelper();
	}
}
