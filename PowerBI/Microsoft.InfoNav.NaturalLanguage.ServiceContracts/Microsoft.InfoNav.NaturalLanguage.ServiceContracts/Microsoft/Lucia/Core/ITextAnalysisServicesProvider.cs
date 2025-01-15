using System;
using Microsoft.InfoNav;

namespace Microsoft.Lucia.Core
{
	// Token: 0x02000126 RID: 294
	public interface ITextAnalysisServicesProvider : IDisposable
	{
		// Token: 0x060005E2 RID: 1506
		double GetUtilizedMemoryInMB();

		// Token: 0x060005E3 RID: 1507
		StringComparer GetValueComparer(LanguageIdentifier language);

		// Token: 0x060005E4 RID: 1508
		bool TryGetInflector(LanguageIdentifier language, out IInflector inflector);

		// Token: 0x060005E5 RID: 1509
		bool TryGetTextAnalyzer(LanguageIdentifier language, out ITextAnalyzer textAnalyzer);
	}
}
