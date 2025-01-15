using System;
using System.Collections.Generic;
using Microsoft.InfoNav.Common;

namespace Microsoft.Lucia.Core
{
	// Token: 0x02000128 RID: 296
	public interface ITextNormalizationServiceProvider
	{
		// Token: 0x170001EA RID: 490
		// (get) Token: 0x060005F0 RID: 1520
		string TokenSeparator { get; }

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x060005F1 RID: 1521
		string Quote { get; }

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x060005F2 RID: 1522
		string OpenParen { get; }

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x060005F3 RID: 1523
		string CloseParen { get; }

		// Token: 0x060005F4 RID: 1524
		string Concat(params string[] values);

		// Token: 0x060005F5 RID: 1525
		string JoinWithTokenSeparator(IEnumerable<string> tokens);

		// Token: 0x060005F6 RID: 1526
		string GetDisplayStringForTerm(string term, bool hasOpenQuote);

		// Token: 0x060005F7 RID: 1527
		string GetQuotedString(string text);

		// Token: 0x060005F8 RID: 1528
		char NormalizeForIndexing(char c);

		// Token: 0x060005F9 RID: 1529
		string NormalizeForIndexing(string text);

		// Token: 0x060005FA RID: 1530
		string NormalizeForTokenizedIndexing(string text);

		// Token: 0x060005FB RID: 1531
		string FormatAsQuestion(string text);

		// Token: 0x060005FC RID: 1532
		string GetString(long value);

		// Token: 0x060005FD RID: 1533
		string GetString(decimal value);

		// Token: 0x060005FE RID: 1534
		string GetString(DateTime value);

		// Token: 0x060005FF RID: 1535
		global::System.ValueTuple<string, IList<IRange>> CombineNouns(IList<string> nouns);
	}
}
