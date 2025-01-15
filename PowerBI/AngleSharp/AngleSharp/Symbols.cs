using System;
using System.Collections.Generic;

namespace AngleSharp
{
	// Token: 0x0200001E RID: 30
	internal static class Symbols
	{
		// Token: 0x04000181 RID: 385
		public const char EndOfFile = '\uffff';

		// Token: 0x04000182 RID: 386
		public const char Tilde = '~';

		// Token: 0x04000183 RID: 387
		public const char Pipe = '|';

		// Token: 0x04000184 RID: 388
		public const char Null = '\0';

		// Token: 0x04000185 RID: 389
		public const char Ampersand = '&';

		// Token: 0x04000186 RID: 390
		public const char Num = '#';

		// Token: 0x04000187 RID: 391
		public const char Dollar = '$';

		// Token: 0x04000188 RID: 392
		public const char Semicolon = ';';

		// Token: 0x04000189 RID: 393
		public const char Asterisk = '*';

		// Token: 0x0400018A RID: 394
		public const char Equality = '=';

		// Token: 0x0400018B RID: 395
		public const char Plus = '+';

		// Token: 0x0400018C RID: 396
		public const char Minus = '-';

		// Token: 0x0400018D RID: 397
		public const char Comma = ',';

		// Token: 0x0400018E RID: 398
		public const char Dot = '.';

		// Token: 0x0400018F RID: 399
		public const char Accent = '^';

		// Token: 0x04000190 RID: 400
		public const char At = '@';

		// Token: 0x04000191 RID: 401
		public const char LessThan = '<';

		// Token: 0x04000192 RID: 402
		public const char GreaterThan = '>';

		// Token: 0x04000193 RID: 403
		public const char SingleQuote = '\'';

		// Token: 0x04000194 RID: 404
		public const char DoubleQuote = '"';

		// Token: 0x04000195 RID: 405
		public const char CurvedQuote = '`';

		// Token: 0x04000196 RID: 406
		public const char QuestionMark = '?';

		// Token: 0x04000197 RID: 407
		public const char Tab = '\t';

		// Token: 0x04000198 RID: 408
		public const char LineFeed = '\n';

		// Token: 0x04000199 RID: 409
		public const char CarriageReturn = '\r';

		// Token: 0x0400019A RID: 410
		public const char FormFeed = '\f';

		// Token: 0x0400019B RID: 411
		public const char Space = ' ';

		// Token: 0x0400019C RID: 412
		public const char Solidus = '/';

		// Token: 0x0400019D RID: 413
		public const char NoBreakSpace = '\u00a0';

		// Token: 0x0400019E RID: 414
		public const char ReverseSolidus = '\\';

		// Token: 0x0400019F RID: 415
		public const char Colon = ':';

		// Token: 0x040001A0 RID: 416
		public const char ExclamationMark = '!';

		// Token: 0x040001A1 RID: 417
		public const char Replacement = '\ufffd';

		// Token: 0x040001A2 RID: 418
		public const char Underscore = '_';

		// Token: 0x040001A3 RID: 419
		public const char RoundBracketOpen = '(';

		// Token: 0x040001A4 RID: 420
		public const char RoundBracketClose = ')';

		// Token: 0x040001A5 RID: 421
		public const char SquareBracketOpen = '[';

		// Token: 0x040001A6 RID: 422
		public const char SquareBracketClose = ']';

		// Token: 0x040001A7 RID: 423
		public const char CurlyBracketOpen = '{';

		// Token: 0x040001A8 RID: 424
		public const char CurlyBracketClose = '}';

		// Token: 0x040001A9 RID: 425
		public const char Percent = '%';

		// Token: 0x040001AA RID: 426
		public const int MaximumCodepoint = 1114111;

		// Token: 0x040001AB RID: 427
		public static Dictionary<char, char> Punycode = new Dictionary<char, char>
		{
			{ '。', '.' },
			{ '．', '.' },
			{ 'Ｇ', 'g' },
			{ 'ｏ', 'o' },
			{ 'ｃ', 'c' },
			{ 'Ｘ', 'x' },
			{ '０', '0' },
			{ '１', '1' },
			{ '２', '2' },
			{ '５', '5' }
		};

		// Token: 0x040001AC RID: 428
		public static readonly string[] NewLines = new string[] { "\r\n", "\r", "\n" };
	}
}
