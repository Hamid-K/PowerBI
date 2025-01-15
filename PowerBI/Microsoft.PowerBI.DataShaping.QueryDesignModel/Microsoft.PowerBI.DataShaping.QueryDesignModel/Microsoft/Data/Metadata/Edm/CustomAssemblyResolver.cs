using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Reflection;

namespace Microsoft.Data.Metadata.Edm
{
	// Token: 0x0200006E RID: 110
	internal class CustomAssemblyResolver : MetadataArtifactAssemblyResolver
	{
		// Token: 0x06000918 RID: 2328 RVA: 0x00014B35 File Offset: 0x00012D35
		internal CustomAssemblyResolver(Func<IEnumerable<Assembly>> wildcardAssemblyEnumerator, Func<AssemblyName, Assembly> referenceResolver)
		{
			this._wildcardAssemblyEnumerator = wildcardAssemblyEnumerator;
			this._referenceResolver = referenceResolver;
		}

		// Token: 0x06000919 RID: 2329 RVA: 0x00014B4B File Offset: 0x00012D4B
		internal override bool TryResolveAssemblyReference(AssemblyName refernceName, out Assembly assembly)
		{
			assembly = this._referenceResolver(refernceName);
			return assembly != null;
		}

		// Token: 0x0600091A RID: 2330 RVA: 0x00014B63 File Offset: 0x00012D63
		internal override IEnumerable<Assembly> GetWildcardAssemblies()
		{
			IEnumerable<Assembly> enumerable = this._wildcardAssemblyEnumerator();
			if (enumerable == null)
			{
				throw EntityUtil.InvalidOperation(Strings.WildcardEnumeratorReturnedNull);
			}
			return enumerable;
		}

		// Token: 0x0400071F RID: 1823
		private Func<AssemblyName, Assembly> _referenceResolver;

		// Token: 0x04000720 RID: 1824
		private Func<IEnumerable<Assembly>> _wildcardAssemblyEnumerator;
	}
}
