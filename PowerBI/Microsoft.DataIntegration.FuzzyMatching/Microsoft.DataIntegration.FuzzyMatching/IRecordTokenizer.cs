using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.DataIntegration.FuzzyMatchingCommon;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x020000E8 RID: 232
	public interface IRecordTokenizer
	{
		// Token: 0x06000924 RID: 2340
		void Prepare(DataTable schema, DomainBinding domainBinding, out TokenizerContext context);

		// Token: 0x06000925 RID: 2341
		IEnumerable<StringExtent> Tokenize(TokenizerContext tokenizerContext, IDataRecord record);
	}
}
