using System;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x02000062 RID: 98
	public interface IComponentManager
	{
		// Token: 0x060003D3 RID: 979
		int RegisterComponent(Type t);

		// Token: 0x060003D4 RID: 980
		Type GetComponentType(int componentId);
	}
}
