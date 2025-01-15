using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Client.Materialization
{
	// Token: 0x020000FC RID: 252
	internal abstract class MaterializationPolicy
	{
		// Token: 0x06000AB5 RID: 2741 RVA: 0x000287F0 File Offset: 0x000269F0
		public virtual object CreateNewInstance(IEdmTypeReference edmTypeReference, Type type)
		{
			return Util.ActivatorCreateInstance(type, new object[0]);
		}
	}
}
