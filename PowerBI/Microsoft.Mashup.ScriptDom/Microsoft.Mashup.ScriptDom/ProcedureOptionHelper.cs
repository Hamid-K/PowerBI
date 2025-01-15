using System;
using antlr;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200011C RID: 284
	[Serializable]
	internal class ProcedureOptionHelper : OptionsHelper<ProcedureOptionKind>
	{
		// Token: 0x060014AD RID: 5293 RVA: 0x000909A3 File Offset: 0x0008EBA3
		private ProcedureOptionHelper()
		{
			base.AddOptionMapping(ProcedureOptionKind.Encryption, "ENCRYPTION");
			base.AddOptionMapping(ProcedureOptionKind.Recompile, "RECOMPILE");
		}

		// Token: 0x060014AE RID: 5294 RVA: 0x000909C4 File Offset: 0x0008EBC4
		protected override TSqlParseErrorException GetMatchingException(IToken token)
		{
			return new TSqlParseErrorException(TSql80ParserBaseInternal.CreateParseError("SQL46006", token, TSqlParserResource.SQL46006Message, new string[] { token.getText() }));
		}

		// Token: 0x0400111C RID: 4380
		internal static readonly ProcedureOptionHelper Instance = new ProcedureOptionHelper();
	}
}
