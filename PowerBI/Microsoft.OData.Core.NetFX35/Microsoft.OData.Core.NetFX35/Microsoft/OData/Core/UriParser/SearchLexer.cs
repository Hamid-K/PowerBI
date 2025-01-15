using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;
using Microsoft.OData.Core.UriParser.TreeNodeKinds;

namespace Microsoft.OData.Core.UriParser
{
	// Token: 0x0200021D RID: 541
	[DebuggerDisplay("SearchLexer ({text} @ {textPos} [{token}])")]
	internal sealed class SearchLexer : ExpressionLexer
	{
		// Token: 0x060013A6 RID: 5030 RVA: 0x00048698 File Offset: 0x00046898
		internal SearchLexer(string expression)
			: base(expression, true, false)
		{
		}

		// Token: 0x060013A7 RID: 5031 RVA: 0x000486A4 File Offset: 0x000468A4
		[SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Justification = "This parser method is all about the switch statement and would be harder to maintain if it were broken up.")]
		protected override ExpressionToken NextTokenImplementation(out Exception error)
		{
			error = null;
			base.ParseWhitespace();
			int textPos = this.textPos;
			char? ch = this.ch;
			char valueOrDefault = ch.GetValueOrDefault();
			ExpressionTokenKind expressionTokenKind;
			if (ch != null)
			{
				if (valueOrDefault != '"')
				{
					switch (valueOrDefault)
					{
					case '(':
						base.NextChar();
						expressionTokenKind = ExpressionTokenKind.OpenParen;
						goto IL_00DF;
					case ')':
						base.NextChar();
						expressionTokenKind = ExpressionTokenKind.CloseParen;
						goto IL_00DF;
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
					goto IL_00DF;
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
			IL_00DF:
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

		// Token: 0x060013A8 RID: 5032 RVA: 0x000488F4 File Offset: 0x00046AF4
		private static bool IsValidSearchTermChar(char val)
		{
			return !char.IsWhiteSpace(val) && val != ')';
		}

		// Token: 0x060013A9 RID: 5033 RVA: 0x00048908 File Offset: 0x00046B08
		private void NextCharWithEscape()
		{
			this.isEscape = false;
			base.NextChar();
			if (this.ch == '\\')
			{
				this.isEscape = true;
				base.NextChar();
				if (this.ch == null || "\\\"".IndexOf(this.ch.Value) < 0)
				{
					throw ExpressionLexer.ParseError(Strings.ExpressionLexer_InvalidEscapeSequence(this.ch, this.textPos, this.Text));
				}
			}
		}

		// Token: 0x060013AA RID: 5034 RVA: 0x0004899C File Offset: 0x00046B9C
		private void AdvanceToNextOccuranceOfWithEscape(char endingValue)
		{
			this.NextCharWithEscape();
			while (this.ch != null && (!(this.ch == endingValue) || this.isEscape))
			{
				this.NextCharWithEscape();
			}
		}

		// Token: 0x060013AB RID: 5035 RVA: 0x000489EC File Offset: 0x00046BEC
		// Note: this type is marked as 'beforefieldinit'.
		static SearchLexer()
		{
			HashSet<string> hashSet = new HashSet<string>(StringComparer.Ordinal);
			hashSet.Add("AND");
			hashSet.Add("OR");
			hashSet.Add("NOT");
			SearchLexer.KeyWords = hashSet;
		}

		// Token: 0x04000853 RID: 2131
		private const char EscapeChar = '\\';

		// Token: 0x04000854 RID: 2132
		private const string EscapeSequenceSet = "\\\"";

		// Token: 0x04000855 RID: 2133
		internal static readonly Regex InvalidWordPattern = new Regex("([^\\p{L}\\p{Nl}])");

		// Token: 0x04000856 RID: 2134
		private static readonly HashSet<string> KeyWords;

		// Token: 0x04000857 RID: 2135
		private bool isEscape;
	}
}
