using System;
using System.Collections.Generic;
using Microsoft.Lucia.Core.Text;

namespace Microsoft.Lucia.Core
{
	// Token: 0x02000127 RID: 295
	public interface ITextAnalyzer
	{
		// Token: 0x060005E6 RID: 1510
		bool TryAnalyzeText(TextAnalysisContext context, string text, out TextAnalysisResult analysisResult);

		// Token: 0x060005E7 RID: 1511
		bool TryPluralizeAsNoun(IReadOnlyList<IStemmerToken> tokens, out IList<string> pluralizedValues);

		// Token: 0x060005E8 RID: 1512
		bool IsNounPlural(IReadOnlyList<IStemmerToken> tokens);

		// Token: 0x060005E9 RID: 1513
		string StemAsNoun(TextAnalysisContext context, string text, NounStemmingOption nounStemmingOption = NounStemmingOption.HeadNounAndAgreement);

		// Token: 0x060005EA RID: 1514
		string StemAsNoun(TextAnalysisContext context, TextAnalysisResult text, NounStemmingOption nounStemmingOption = NounStemmingOption.HeadNounAndAgreement);

		// Token: 0x060005EB RID: 1515
		bool TryGetHeadNounIndex(IReadOnlyList<IStemmerToken> tokens, out int headNounIndex);

		// Token: 0x060005EC RID: 1516
		bool IsContentWord(IStemmerToken token);

		// Token: 0x060005ED RID: 1517
		bool IsVerbWithParticle(IReadOnlyList<IStemmerToken> tokens);

		// Token: 0x060005EE RID: 1518
		bool TryGetVerbParticle(IReadOnlyList<IStemmerToken> tokens, out string particle);

		// Token: 0x060005EF RID: 1519
		bool TryGetVerbRoot(IReadOnlyList<IStemmerToken> tokens, out string root);
	}
}
