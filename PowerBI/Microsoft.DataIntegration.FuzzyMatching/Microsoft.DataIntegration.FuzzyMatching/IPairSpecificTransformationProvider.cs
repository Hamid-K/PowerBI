using System;
using System.Data;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x020000FB RID: 251
	public interface IPairSpecificTransformationProvider
	{
		// Token: 0x06000A63 RID: 2659
		void Match(ISession session, ITokenIdProvider tokenIdProvider, IDataRecord leftRecord, IDataRecord rightRecord, RecordBinding leftBinding, RecordBinding rightBinding, TokenSequence leftTokenSeq, TokenSequence rightTokenSeq, out ArraySegment<TransformationMatch> leftTransformationMatchList, out ArraySegment<TransformationMatch> rightTransformationMatchList);
	}
}
