using System;

namespace Microsoft.Reporting.QueryDesign.ExpressionTrees.Translators.Internal
{
	// Token: 0x0200012B RID: 299
	internal sealed class DaxExternalContentChecker
	{
		// Token: 0x06001073 RID: 4211 RVA: 0x0002CFB5 File Offset: 0x0002B1B5
		private DaxExternalContentChecker(string text)
		{
			this._tokenizer = new DaxExternalContentTokenizer(text);
			this._openParenCount = 0;
		}

		// Token: 0x06001074 RID: 4212 RVA: 0x0002CFD0 File Offset: 0x0002B1D0
		internal static DaxExternalContentCheckerResult Check(string daxText)
		{
			return new DaxExternalContentChecker(daxText).Check();
		}

		// Token: 0x06001075 RID: 4213 RVA: 0x0002CFE0 File Offset: 0x0002B1E0
		private DaxExternalContentCheckerResult Check()
		{
			DaxExternalContentTokenType daxExternalContentTokenType;
			while ((daxExternalContentTokenType = this._tokenizer.NextToken()) != DaxExternalContentTokenType.EndOfInput)
			{
				if (daxExternalContentTokenType != DaxExternalContentTokenType.OpenParen)
				{
					if (daxExternalContentTokenType != DaxExternalContentTokenType.CloseParen)
					{
						switch (daxExternalContentTokenType)
						{
						case DaxExternalContentTokenType.StringStart:
							return this.Fail(DaxInvalidExternalContentErrorCode.UnclosedStringLiteral);
						case DaxExternalContentTokenType.BracketedIdentifierStart:
							return this.Fail(DaxInvalidExternalContentErrorCode.UnclosedBracketIdentifier);
						case DaxExternalContentTokenType.QuotedIdentifierStart:
							return this.Fail(DaxInvalidExternalContentErrorCode.UnclosedQuoteIdentifier);
						case DaxExternalContentTokenType.MultiLineCommentStart:
							return this.Fail(DaxInvalidExternalContentErrorCode.UnclosedMultiLineComment);
						}
					}
					else
					{
						if (this._openParenCount == 0)
						{
							return this.Fail(DaxInvalidExternalContentErrorCode.UnexpectedCloseParenthesis);
						}
						this._openParenCount--;
					}
				}
				else
				{
					this._openParenCount++;
				}
			}
			if (this._openParenCount != 0)
			{
				return this.Fail(DaxInvalidExternalContentErrorCode.UnclosedParenthesis);
			}
			return this.Succeed();
		}

		// Token: 0x06001076 RID: 4214 RVA: 0x0002D095 File Offset: 0x0002B295
		private DaxExternalContentCheckerResult Fail(DaxInvalidExternalContentErrorCode errorCode)
		{
			return DaxExternalContentCheckerResult.Unsafe(errorCode, this._tokenizer.LineNumber, this._tokenizer.PositionInLine);
		}

		// Token: 0x06001077 RID: 4215 RVA: 0x0002D0B3 File Offset: 0x0002B2B3
		private DaxExternalContentCheckerResult Succeed()
		{
			return DaxExternalContentCheckerResult.Safe(this._tokenizer.LineNumber);
		}

		// Token: 0x04000A87 RID: 2695
		private readonly DaxExternalContentTokenizer _tokenizer;

		// Token: 0x04000A88 RID: 2696
		private int _openParenCount;
	}
}
