using System;
using System.Collections.Generic;
using System.Reflection;

namespace Microsoft.AspNet.OData.Interfaces
{
	// Token: 0x0200005B RID: 91
	internal interface IWebApiAssembliesResolver
	{
		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x0600027F RID: 639
		IEnumerable<Assembly> Assemblies { get; }
	}
}
