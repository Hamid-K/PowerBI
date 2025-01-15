using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace Microsoft.OData.UriParser
{
	// Token: 0x0200016A RID: 362
	[DebuggerDisplay("SearchLexer ({text} @ {textPos} [{token}])")]
	internal sealed class SearchLexer : ExpressionLexer
	{
		// Token: 0x06001253 RID: 4691 RVA: 0x00037E12 File Offset: 0x00036012
		internal SearchLexer(string expression)
			: base(expression, true, false)
		{
		}

		// Token: 0x06001254 RID: 4692 RVA: 0x00037E20 File Offset: 0x00036020
		[SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Justification = "This parser method is all about the switch statement and would be harder to maintain if it were broken up.")]
		protected override ExpressionToken NextTokenImplementation(out Exception error)
		{
			error = null;
			base.ParseWhitespace();
			int textPos = this.textPos;
			char? ch = this.ch;
			ExpressionTokenKind expressionTokenKind;
			if (ch != null)
			{
				char valueOrDefault = ch.GetValueOrDefault();
				if (valueOrDefault != '"')
				{
					if (valueOrDefault == '(')
					{
						base.NextChar();
						expressionTokenKind = ExpressionTokenKind.OpenParen;
						goto IL_00D7;
					}
					if (valueOrDefault == ')')
					{
						base.NextChar();
						expressionTokenKind = ExpressionTokenKind.CloseParen;
						goto IL_00D7;
					}
				}
				else
				{
					char value = this.ch.Value;
					this.AdvanceToNextOccuranceOfWithEscape(value);
					if (this.textPos == this.TextLen)
					{
						throw ExpressionLexer.ParseError(Strings.ExpressionLexer_UnterminatedStringLiteral(this.textPos, this.Text));
					}
					base.NextChar();
					expressionTokenKind = ExpressionTokenKind.StringLiteral;
					goto IL_00D7;
				}
			}
			if (this.textPos == this.TextLen)
			{
				expressionTokenKind = ExpressionTokenKind.End;
			}
			else
			{
				expressionTokenKind = ExpressionTokenKind.Identifier;
				do
				{
					base.NextChar();
				}
				while (this.ch != null && SearchLexer.IsValidSearchTermChar(this.ch.Value));
			}
			IL_00D7:
			this.token.Kind = expressionTokenKind;
			this.token.Text = this.Text.Substring(textPos, this.textPos - textPos);
			this.token.Position = textPos;
			if (this.token.Kind == ExpressionTokenKind.StringLiteral)
			{
				this.token.Text = this.token.Text.Substring(1, this.token.Text.Length - 2).Replace("\\\\", "\\").Replace("\\\"", "\"");
				if (string.IsNullOrEmpty(this.token.Text))
				{
					throw ExpressionLexer.ParseError(Strings.ExpressionToken_IdentifierExpected(this.token.Position));
				}
			}
			if (this.token.Kind == ExpressionTokenKind.Identifier && !SearchLexer.KeyWords.Contains(this.token.Text))
			{
				Match match = SearchLexer.InvalidWordPattern.Match(this.token.Text);
				if (match.Success)
				{
					int index = match.Groups[0].Index;
					throw ExpressionLexer.ParseError(Strings.ExpressionLexer_InvalidCharacter(this.token.Text[index], this.token.Position + index, this.Text));
				}
				this.token.Kind = ExpressionTokenKind.StringLiteral;
			}
			return this.token;
		}

		// Token: 0x06001255 RID: 4693 RVA: 0x0003806B File Offset: 0x0003626B
		private static bool IsValidSearchTermChar(char val)
		{
			return !char.IsWhiteSpace(val) && val != ')';
		}

		// Token: 0x06001256 RID: 4694 RVA: 0x00038080 File Offset: 0x00036280
		private void NextCharWithEscape()
		{
			this.isEscape = false;
			base.NextChar();
			char? ch = this.ch;
			if (((ch != null) ? new int?((int)ch.GetValueOrDefault()) : null) == 92)
			{
				this.isEscape = true;
				base.NextChar();
				if (this.ch == null || "\\\"".IndexOf(this.ch.Value) < 0)
				{
					throw ExpressionLexer.ParseError(Strings.ExpressionLexer_InvalidEscapeSequence(this.ch, this.textPos, this.Text));
				}
			}
		}

		// Token: 0x06001257 RID: 4695 RVA: 0x00038134 File Offset: 0x00036334
		private void AdvanceToNextOccuranceOfWithEscape(char endingValue)
		{
			this.NextCharWithEscape();
			while (this.ch != null)
			{
				char? ch = this.ch;
				if (((ch != null) ? new int?((int)ch.GetValueOrDefault()) : null) == (int)endingValue && !this.isEscape)
				{
					break;
				}
				this.NextCharWithEscape();
			}
		}

		// Token: 0x04000848 RID: 2120
		internal static readonly Regex InvalidWordPattern = new Regex("([^\\p{L}\\p{Nl}])");

		// Token: 0x04000849 RID: 2121
		private const char EscapeChar = '\\';

		// Token: 0x0400084A RID: 2122
		private const string EscapeSequenceSet = "\\\"";

		// Token: 0x0400084B RID: 2123
		private static readonly HashSet<string> KeyWords = new HashSet<string>(StringComparer.Ordinal) { "AND", "OR", "NOT" };

		// Token: 0x0400084C RID: 2124
		private bool isEscape;
	}
}
