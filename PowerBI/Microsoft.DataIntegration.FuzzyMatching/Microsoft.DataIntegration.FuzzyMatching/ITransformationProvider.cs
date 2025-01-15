using System;
using System.Collections.Generic;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x020000FA RID: 250
	public interface ITransformationProvider
	{
		// Token: 0x06000A61 RID: 2657
		void Match(ISession session, ITokenIdProvider tokenIdProvider, TokenSequence tokenSeq, out ArraySegment<TransformationMatch> transformationMatchList);

		// Token: 0x06000A62 RID: 2658
		IEnumerable<Transformation> Transformations(ITokenIdProvider tokenIdProvider);
	}
}
