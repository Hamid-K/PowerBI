using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000125 RID: 293
	[DebuggerDisplay("SearchLexer ({text} @ {textPos} [{token}])")]
	internal sealed class SearchLexer : ExpressionLexer
	{
		// Token: 0x06000D7B RID: 3451 RVA: 0x000284DE File Offset: 0x000266DE
		internal SearchLexer(string expression)
			: base(expression, true, false)
		{
		}

		// Token: 0x06000D7C RID: 3452 RVA: 0x000284EC File Offset: 0x000266EC
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
					throw ExpressionLexer.ParseError(Strings.ExpressionLexer_InvalidCharacter(this.token.Text.get_Chars(index), this.token.Position + index, this.Text));
				}
				this.token.Kind = ExpressionTokenKind.StringLiteral;
			}
			return this.token;
		}

		// Token: 0x06000D7D RID: 3453 RVA: 0x00028737 File Offset: 0x00026937
		private static bool IsValidSearchTermChar(char val)
		{
			return !char.IsWhiteSpace(val) && val != ')';
		}

		// Token: 0x06000D7E RID: 3454 RVA: 0x0002874C File Offset: 0x0002694C
		private void NextCharWithEscape()
		{
			this.isEscape = false;
			base.NextChar();
			char? ch = this.ch;
			if (((ch != null) ? new int?((int)ch.GetValueOrDefault()) : default(int?)) == 92)
			{
				this.isEscape = true;
				base.NextChar();
				if (this.ch == null || "\\\"".IndexOf(this.ch.Value) < 0)
				{
					throw ExpressionLexer.ParseError(Strings.ExpressionLexer_InvalidEscapeSequence(this.ch, this.textPos, this.Text));
				}
			}
		}

		// Token: 0x06000D7F RID: 3455 RVA: 0x00028800 File Offset: 0x00026A00
		private void AdvanceToNextOccuranceOfWithEscape(char endingValue)
		{
			this.NextCharWithEscape();
			while (this.ch != null)
			{
				char? ch = this.ch;
				if (((ch != null) ? new int?((int)ch.GetValueOrDefault()) : default(int?)) == (int)endingValue && !this.isEscape)
				{
					break;
				}
				this.NextCharWithEscape();
			}
		}

		// Token: 0x06000D80 RID: 3456 RVA: 0x00028870 File Offset: 0x00026A70
		// Note: this type is marked as 'beforefieldinit'.
		static SearchLexer()
		{
			HashSet<string> hashSet = new HashSet<string>(StringComparer.Ordinal);
			hashSet.Add("AND");
			hashSet.Add("OR");
			hashSet.Add("NOT");
			SearchLexer.KeyWords = hashSet;
		}

		// Token: 0x04000727 RID: 1831
		internal static readonly Regex InvalidWordPattern = new Regex("([^\\p{L}\\p{Nl}])");

		// Token: 0x04000728 RID: 1832
		private const char EscapeChar = '\\';

		// Token: 0x04000729 RID: 1833
		private const string EscapeSequenceSet = "\\\"";

		// Token: 0x0400072A RID: 1834
		private static readonly HashSet<string> KeyWords;

		// Token: 0x0400072B RID: 1835
		private bool isEscape;
	}
}
