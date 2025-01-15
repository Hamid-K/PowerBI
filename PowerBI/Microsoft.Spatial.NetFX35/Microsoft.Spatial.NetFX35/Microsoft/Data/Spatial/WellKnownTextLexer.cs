using System;
using System.IO;
using Microsoft.Spatial;

namespace Microsoft.Data.Spatial
{
	// Token: 0x0200004A RID: 74
	internal class WellKnownTextLexer : TextLexerBase
	{
		// Token: 0x060001ED RID: 493 RVA: 0x000058E2 File Offset: 0x00003AE2
		public WellKnownTextLexer(TextReader text)
			: base(text)
		{
		}

		// Token: 0x060001EE RID: 494 RVA: 0x000058EC File Offset: 0x00003AEC
		protected override bool MatchTokenType(char nextChar, int? activeTokenType, out int tokenType)
		{
			switch (nextChar)
			{
			case '\t':
			case '\n':
			case '\r':
				break;
			case '\v':
			case '\f':
				goto IL_0111;
			default:
				switch (nextChar)
				{
				case ' ':
					goto IL_00E5;
				case '!':
				case '"':
				case '#':
				case '$':
				case '%':
				case '&':
				case '\'':
				case '*':
				case '/':
				case ':':
				case '<':
				case '>':
				case '?':
				case '@':
				case 'A':
				case 'B':
				case 'C':
				case 'D':
					goto IL_0111;
				case '(':
					tokenType = 4;
					return true;
				case ')':
					tokenType = 5;
					return true;
				case '+':
				case '-':
				case '0':
				case '1':
				case '2':
				case '3':
				case '4':
				case '5':
				case '6':
				case '7':
				case '8':
				case '9':
					tokenType = 2;
					return false;
				case ',':
					tokenType = 7;
					return true;
				case '.':
					tokenType = 6;
					return true;
				case ';':
					tokenType = 3;
					return true;
				case '=':
					tokenType = 1;
					return true;
				case 'E':
					break;
				default:
					if (nextChar != 'e')
					{
						goto IL_0111;
					}
					break;
				}
				if (activeTokenType == 2)
				{
					tokenType = 2;
				}
				else
				{
					tokenType = 0;
				}
				return false;
			}
			IL_00E5:
			tokenType = 8;
			return false;
			IL_0111:
			if ((nextChar >= 'A' && nextChar <= 'Z') || (nextChar >= 'a' && nextChar <= 'z'))
			{
				tokenType = 0;
				return false;
			}
			throw new FormatException(Strings.WellKnownText_UnexpectedCharacter(nextChar));
		}
	}
}
