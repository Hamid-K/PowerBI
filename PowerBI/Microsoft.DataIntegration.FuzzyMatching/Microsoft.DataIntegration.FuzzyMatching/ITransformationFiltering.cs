using System;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x020000FC RID: 252
	public interface ITransformationFiltering : IObjectReferenceContainer
	{
		// Token: 0x170001FA RID: 506
		// (get) Token: 0x06000A64 RID: 2660
		// (set) Token: 0x06000A65 RID: 2661
		ITransformationFilter TransformationFilter { get; set; }
	}
}
