using System;
using antlr;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000119 RID: 281
	internal class ViewOptionHelper : OptionsHelper<ViewOptionKind>
	{
		// Token: 0x060014A4 RID: 5284 RVA: 0x00090869 File Offset: 0x0008EA69
		private ViewOptionHelper()
		{
			base.AddOptionMapping(ViewOptionKind.Encryption, "ENCRYPTION");
			base.AddOptionMapping(ViewOptionKind.SchemaBinding, "SCHEMABINDING");
			base.AddOptionMapping(ViewOptionKind.ViewMetadata, "VIEW_METADATA");
		}

		// Token: 0x060014A5 RID: 5285 RVA: 0x00090898 File Offset: 0x0008EA98
		protected override TSqlParseErrorException GetMatchingException(IToken token)
		{
			return new TSqlParseErrorException(TSql80ParserBaseInternal.CreateParseError("SQL46025", token, TSqlParserResource.SQL46025Message, new string[] { token.getText() }));
		}

		// Token: 0x04001119 RID: 4377
		internal static readonly ViewOptionHelper Instance = new ViewOptionHelper();
	}
}
