using System;
using System.Collections.Generic;
using System.Data;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x02000027 RID: 39
	[Serializable]
	public class TransformationProviderReference : ProviderReference<ITransformationProvider>, ITransformationProvider, ITransformationFiltering, IObjectReferenceContainer, IPairSpecificTransformationProvider
	{
		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000118 RID: 280 RVA: 0x00005456 File Offset: 0x00003656
		// (set) Token: 0x06000119 RID: 281 RVA: 0x00005477 File Offset: 0x00003677
		public ITransformationFilter TransformationFilter
		{
			get
			{
				if (this.m_interface is ITransformationFiltering)
				{
					return (this.m_interface as ITransformationFiltering).TransformationFilter;
				}
				return null;
			}
			set
			{
				if (this.m_interface is ITransformationFiltering)
				{
					(this.m_interface as ITransformationFiltering).TransformationFilter = value;
					return;
				}
				if (value != null)
				{
					throw new Exception("Referenced object does not implement ITransformationFiltering.");
				}
			}
		}

		// Token: 0x0600011A RID: 282 RVA: 0x000054A8 File Offset: 0x000036A8
		public void Match(ISession session, ITokenIdProvider tokenIdProvider, IDataRecord leftRecord, IDataRecord rightRecord, RecordBinding leftBinding, RecordBinding rightBinding, TokenSequence leftTokenSeq, TokenSequence rightTokenSeq, out ArraySegment<TransformationMatch> leftTransformationMatchList, out ArraySegment<TransformationMatch> rightTransformationMatchList)
		{
			if (this.m_interface is IPairSpecificTransformationProvider)
			{
				(this.m_interface as IPairSpecificTransformationProvider).Match(session, tokenIdProvider, leftRecord, rightRecord, leftBinding, rightBinding, leftTokenSeq, rightTokenSeq, out leftTransformationMatchList, out rightTransformationMatchList);
				return;
			}
			throw new Exception("Referenced object does not implement IPairSpecificTransformationProvider.");
		}

		// Token: 0x0600011B RID: 283 RVA: 0x000054EE File Offset: 0x000036EE
		public void Match(ISession session, ITokenIdProvider tokenIdProvider, TokenSequence tokenSeq, out ArraySegment<TransformationMatch> transformationMatchList)
		{
			this.m_interface.Match(session, tokenIdProvider, tokenSeq, out transformationMatchList);
		}

		// Token: 0x0600011C RID: 284 RVA: 0x00005500 File Offset: 0x00003700
		public IEnumerable<Transformation> Transformations(ITokenIdProvider tokenIdProvider)
		{
			return this.m_interface.Transformations(tokenIdProvider);
		}
	}
}
