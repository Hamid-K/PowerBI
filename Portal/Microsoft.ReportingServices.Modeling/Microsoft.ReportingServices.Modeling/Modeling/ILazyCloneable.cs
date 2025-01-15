using System;

namespace Microsoft.ReportingServices.Modeling
{
	// Token: 0x02000039 RID: 57
	internal interface ILazyCloneable<T>
	{
		// Token: 0x06000250 RID: 592
		T CloneFor(SemanticModel newModel);
	}
}
