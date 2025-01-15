using System;
using System.Collections.Generic;
using System.Reflection;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x020004CF RID: 1231
	internal abstract class MetadataArtifactAssemblyResolver
	{
		// Token: 0x06003CF8 RID: 15608
		internal abstract bool TryResolveAssemblyReference(AssemblyName referenceName, out Assembly assembly);

		// Token: 0x06003CF9 RID: 15609
		internal abstract IEnumerable<Assembly> GetWildcardAssemblies();
	}
}
