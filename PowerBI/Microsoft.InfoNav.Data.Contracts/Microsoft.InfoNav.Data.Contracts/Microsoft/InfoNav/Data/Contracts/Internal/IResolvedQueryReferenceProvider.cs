using System;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x0200021F RID: 543
	internal interface IResolvedQueryReferenceProvider
	{
		// Token: 0x06000FCD RID: 4045
		bool TryParameterRef(string name, out ResolvedQueryParameterRefExpression letRef);

		// Token: 0x06000FCE RID: 4046
		bool TryLetRef(string name, out ResolvedQueryLetRefExpression letRef);

		// Token: 0x06000FCF RID: 4047
		bool TrySourceRef(string name, out ResolvedQueryExpression sourceRef);
	}
}
