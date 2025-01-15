using System;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x0200001D RID: 29
	public interface IObjectReferenceContainer
	{
		// Token: 0x060000C9 RID: 201
		void AcquireReferences();

		// Token: 0x060000CA RID: 202
		void UpdateReferences();

		// Token: 0x060000CB RID: 203
		void ReleaseReferences();
	}
}
