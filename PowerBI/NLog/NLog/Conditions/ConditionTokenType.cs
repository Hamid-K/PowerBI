using System;

namespace NLog.Conditions
{
	// Token: 0x020001B5 RID: 437
	internal enum ConditionTokenType
	{
		// Token: 0x04000531 RID: 1329
		EndOfInput,
		// Token: 0x04000532 RID: 1330
		BeginningOfInput,
		// Token: 0x04000533 RID: 1331
		Number,
		// Token: 0x04000534 RID: 1332
		String,
		// Token: 0x04000535 RID: 1333
		Keyword,
		// Token: 0x04000536 RID: 1334
		Whitespace,
		// Token: 0x04000537 RID: 1335
		FirstPunct,
		// Token: 0x04000538 RID: 1336
		LessThan,
		// Token: 0x04000539 RID: 1337
		GreaterThan,
		// Token: 0x0400053A RID: 1338
		LessThanOrEqualTo,
		// Token: 0x0400053B RID: 1339
		GreaterThanOrEqualTo,
		// Token: 0x0400053C RID: 1340
		EqualTo,
		// Token: 0x0400053D RID: 1341
		NotEqual,
		// Token: 0x0400053E RID: 1342
		LeftParen,
		// Token: 0x0400053F RID: 1343
		RightParen,
		// Token: 0x04000540 RID: 1344
		Dot,
		// Token: 0x04000541 RID: 1345
		Comma,
		// Token: 0x04000542 RID: 1346
		Not,
		// Token: 0x04000543 RID: 1347
		And,
		// Token: 0x04000544 RID: 1348
		Or,
		// Token: 0x04000545 RID: 1349
		Minus,
		// Token: 0x04000546 RID: 1350
		LastPunct,
		// Token: 0x04000547 RID: 1351
		Invalid,
		// Token: 0x04000548 RID: 1352
		ClosingCurlyBrace,
		// Token: 0x04000549 RID: 1353
		Colon,
		// Token: 0x0400054A RID: 1354
		Exclamation,
		// Token: 0x0400054B RID: 1355
		Ampersand,
		// Token: 0x0400054C RID: 1356
		Pipe
	}
}
