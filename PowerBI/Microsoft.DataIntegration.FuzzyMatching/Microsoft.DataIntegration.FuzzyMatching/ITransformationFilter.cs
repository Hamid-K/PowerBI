using System;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x020000FD RID: 253
	public interface ITransformationFilter
	{
		// Token: 0x06000A66 RID: 2662
		void Prepare(ISession session, ITokenIdProvider tokenIdProvider, TokenSequence tokenSequence);

		// Token: 0x06000A67 RID: 2663
		bool AllowTransformations(ISession session, int fromTokenIndex);

		// Token: 0x06000A68 RID: 2664
		bool AllowTransformation(ISession session, int fromTokenIndex, Transformation transformation);

		// Token: 0x06000A69 RID: 2665
		void FilterTransformations(ISession session, int fromTokenIndex, ArraySegment<TransformationMatch> transformationMatchList, out ArraySegment<TransformationMatch> filteredTransformationMatchList);

		// Token: 0x06000A6A RID: 2666
		void FilterTransformations(ISession session, ArraySegment<TransformationMatch> transformationMatchList, out ArraySegment<TransformationMatch> filteredTransformationMatchList);
	}
}
