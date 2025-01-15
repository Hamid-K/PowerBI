using System;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x02000026 RID: 38
	[Serializable]
	public class TransformationFilterReference : ProviderReference<ITransformationFilter>, ITransformationFilter
	{
		// Token: 0x06000112 RID: 274 RVA: 0x000053FD File Offset: 0x000035FD
		public bool AllowTransformations(ISession session, int fromTokenIndex)
		{
			return this.m_interface.AllowTransformations(session, fromTokenIndex);
		}

		// Token: 0x06000113 RID: 275 RVA: 0x0000540C File Offset: 0x0000360C
		public void Prepare(ISession session, ITokenIdProvider tokenIdProvider, TokenSequence tokenSequence)
		{
			this.m_interface.Prepare(session, tokenIdProvider, tokenSequence);
		}

		// Token: 0x06000114 RID: 276 RVA: 0x0000541C File Offset: 0x0000361C
		public bool AllowTransformation(ISession session, int fromTokenIndex, Transformation transformation)
		{
			return this.m_interface.AllowTransformation(session, fromTokenIndex, transformation);
		}

		// Token: 0x06000115 RID: 277 RVA: 0x0000542C File Offset: 0x0000362C
		public void FilterTransformations(ISession session, int fromTokenIndex, ArraySegment<TransformationMatch> transformationMatchList, out ArraySegment<TransformationMatch> filteredTransformationMatchList)
		{
			this.m_interface.FilterTransformations(session, fromTokenIndex, transformationMatchList, out filteredTransformationMatchList);
		}

		// Token: 0x06000116 RID: 278 RVA: 0x0000543E File Offset: 0x0000363E
		public void FilterTransformations(ISession session, ArraySegment<TransformationMatch> transformationMatchList, out ArraySegment<TransformationMatch> filteredTransformationMatchList)
		{
			this.m_interface.FilterTransformations(session, transformationMatchList, out filteredTransformationMatchList);
		}
	}
}
