using System;
using antlr;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200011B RID: 283
	[Serializable]
	internal class TriggerOptionHelper : OptionsHelper<TriggerOptionKind>
	{
		// Token: 0x060014AA RID: 5290 RVA: 0x0009094F File Offset: 0x0008EB4F
		private TriggerOptionHelper()
		{
			base.AddOptionMapping(TriggerOptionKind.Encryption, "ENCRYPTION");
		}

		// Token: 0x060014AB RID: 5291 RVA: 0x00090964 File Offset: 0x0008EB64
		protected override TSqlParseErrorException GetMatchingException(IToken token)
		{
			return new TSqlParseErrorException(TSql80ParserBaseInternal.CreateParseError("SQL46007", token, TSqlParserResource.SQL46007Message, new string[] { token.getText() }));
		}

		// Token: 0x0400111B RID: 4379
		internal static readonly TriggerOptionHelper Instance = new TriggerOptionHelper();
	}
}
