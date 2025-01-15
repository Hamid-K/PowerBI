using System;

namespace Microsoft.InfoNav
{
	// Token: 0x02000048 RID: 72
	internal interface IOverridableConceptualSchema
	{
		// Token: 0x06000130 RID: 304
		IConceptualSchema OverrideConceptualCapabilities(ConceptualCapabilities newCapabilities);
	}
}
