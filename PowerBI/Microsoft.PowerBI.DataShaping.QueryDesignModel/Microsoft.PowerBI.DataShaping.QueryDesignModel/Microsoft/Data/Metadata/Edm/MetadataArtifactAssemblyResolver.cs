using System;
using System.Collections.Generic;
using System.Reflection;

namespace Microsoft.Data.Metadata.Edm
{
	// Token: 0x020000B6 RID: 182
	internal abstract class MetadataArtifactAssemblyResolver
	{
		// Token: 0x06000BCB RID: 3019
		internal abstract bool TryResolveAssemblyReference(AssemblyName refernceName, out Assembly assembly);

		// Token: 0x06000BCC RID: 3020
		internal abstract IEnumerable<Assembly> GetWildcardAssemblies();
	}
}
