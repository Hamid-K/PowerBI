using System;
using antlr;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020000D3 RID: 211
	internal abstract class TSql110ParserBaseInternal : TSql100ParserBaseInternal
	{
		// Token: 0x06000A41 RID: 2625 RVA: 0x00020C1B File Offset: 0x0001EE1B
		protected TSql110ParserBaseInternal(TokenBuffer tokenBuf, int k)
			: base(tokenBuf, k)
		{
		}

		// Token: 0x06000A42 RID: 2626 RVA: 0x00020C25 File Offset: 0x0001EE25
		protected TSql110ParserBaseInternal(ParserSharedInputState state, int k)
			: base(state, k)
		{
		}

		// Token: 0x06000A43 RID: 2627 RVA: 0x00020C2F File Offset: 0x0001EE2F
		protected TSql110ParserBaseInternal(TokenStream lexer, int k)
			: base(lexer, k)
		{
		}

		// Token: 0x06000A44 RID: 2628 RVA: 0x00020C39 File Offset: 0x0001EE39
		public TSql110ParserBaseInternal(bool initialQuotedIdentifiersOn)
			: base(initialQuotedIdentifiersOn)
		{
		}

		// Token: 0x06000A45 RID: 2629 RVA: 0x00020C42 File Offset: 0x0001EE42
		private static bool isFollowingDelimiter(WindowDelimiter delimiter)
		{
			return delimiter != null && (delimiter.WindowDelimiterType == WindowDelimiterType.ValueFollowing || delimiter.WindowDelimiterType == WindowDelimiterType.UnboundedFollowing);
		}

		// Token: 0x06000A46 RID: 2630 RVA: 0x00020C5D File Offset: 0x0001EE5D
		private static bool isPrecedingDelimiter(WindowDelimiter delimiter)
		{
			return delimiter != null && (delimiter.WindowDelimiterType == WindowDelimiterType.ValuePreceding || delimiter.WindowDelimiterType == WindowDelimiterType.UnboundedPreceding);
		}

		// Token: 0x06000A47 RID: 2631 RVA: 0x00020C78 File Offset: 0x0001EE78
		protected static void CheckWindowFrame(WindowFrameClause windowFrameClause)
		{
			bool flag = windowFrameClause.Top != null && (windowFrameClause.Top.WindowDelimiterType == WindowDelimiterType.ValuePreceding || windowFrameClause.Top.WindowDelimiterType == WindowDelimiterType.ValueFollowing);
			bool flag2 = windowFrameClause.Bottom != null && (windowFrameClause.Bottom.WindowDelimiterType == WindowDelimiterType.ValuePreceding || windowFrameClause.Bottom.WindowDelimiterType == WindowDelimiterType.ValueFollowing);
			if (windowFrameClause.WindowFrameType == WindowFrameType.Range && (flag || flag2))
			{
				TSql80ParserBaseInternal.ThrowParseErrorException("SQL46099", windowFrameClause, TSqlParserResource.SQL46099Message, new string[0]);
			}
			if (windowFrameClause.Top == null)
			{
				TSql80ParserBaseInternal.ThrowParseErrorException("SQL46100", windowFrameClause, TSqlParserResource.SQL46100Message, new string[0]);
			}
			if (windowFrameClause.Bottom == null && TSql110ParserBaseInternal.isFollowingDelimiter(windowFrameClause.Top))
			{
				TSql80ParserBaseInternal.ThrowParseErrorException("SQL46100", windowFrameClause, TSqlParserResource.SQL46100Message, new string[0]);
			}
			if (TSql110ParserBaseInternal.isFollowingDelimiter(windowFrameClause.Top) && TSql110ParserBaseInternal.isPrecedingDelimiter(windowFrameClause.Bottom))
			{
				TSql80ParserBaseInternal.ThrowParseErrorException("SQL46100", windowFrameClause, TSqlParserResource.SQL46100Message, new string[0]);
			}
			if (windowFrameClause.Top.WindowDelimiterType == WindowDelimiterType.UnboundedFollowing || (windowFrameClause.Bottom != null && windowFrameClause.Bottom.WindowDelimiterType == WindowDelimiterType.UnboundedPreceding))
			{
				TSql80ParserBaseInternal.ThrowParseErrorException("SQL46100", windowFrameClause, TSqlParserResource.SQL46100Message, new string[0]);
			}
			if (TSql110ParserBaseInternal.isFollowingDelimiter(windowFrameClause.Top) && windowFrameClause.Bottom != null && windowFrameClause.Bottom.WindowDelimiterType == WindowDelimiterType.CurrentRow)
			{
				TSql80ParserBaseInternal.ThrowParseErrorException("SQL46100", windowFrameClause, TSqlParserResource.SQL46100Message, new string[0]);
			}
			if (windowFrameClause.Top.WindowDelimiterType == WindowDelimiterType.CurrentRow && TSql110ParserBaseInternal.isPrecedingDelimiter(windowFrameClause.Bottom))
			{
				TSql80ParserBaseInternal.ThrowParseErrorException("SQL46100", windowFrameClause, TSqlParserResource.SQL46100Message, new string[0]);
			}
		}

		// Token: 0x06000A48 RID: 2632 RVA: 0x00020E1C File Offset: 0x0001F01C
		protected string[] OptionValidForCreateDatabase()
		{
			return TSql110ParserBaseInternal._optionsValidForCreateDatabase;
		}

		// Token: 0x04000725 RID: 1829
		private static string[] _optionsValidForCreateDatabase = new string[] { "CONTAINMENT", "DEFAULT_LANGUAGE", "DEFAULT_FULLTEXT_LANGUAGE", "FILESTREAM", "NESTED_TRIGGERS", "TRANSFORM_NOISE_WORDS", "TWO_DIGIT_YEAR_CUTOFF" };
	}
}
