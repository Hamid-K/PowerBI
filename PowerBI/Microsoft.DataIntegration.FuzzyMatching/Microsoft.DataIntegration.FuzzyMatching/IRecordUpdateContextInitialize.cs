using System;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x02000072 RID: 114
	public interface IRecordUpdateContextInitialize
	{
		// Token: 0x060004BF RID: 1215
		void Initialize(IRowsetManager rowsetManager, IDomainManager domainManager, ITokenIdProvider tokenIdProvider, RecordBinding recordBinding);
	}
}
