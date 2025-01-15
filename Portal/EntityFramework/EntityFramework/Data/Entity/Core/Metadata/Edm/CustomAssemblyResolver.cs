using System;
using System.Collections.Generic;
using System.Data.Entity.Resources;
using System.Reflection;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x02000496 RID: 1174
	internal class CustomAssemblyResolver : MetadataArtifactAssemblyResolver
	{
		// Token: 0x060039F9 RID: 14841 RVA: 0x000C0003 File Offset: 0x000BE203
		internal CustomAssemblyResolver(Func<IEnumerable<Assembly>> wildcardAssemblyEnumerator, Func<AssemblyName, Assembly> referenceResolver)
		{
			this._wildcardAssemblyEnumerator = wildcardAssemblyEnumerator;
			this._referenceResolver = referenceResolver;
		}

		// Token: 0x060039FA RID: 14842 RVA: 0x000C0019 File Offset: 0x000BE219
		internal override bool TryResolveAssemblyReference(AssemblyName referenceName, out Assembly assembly)
		{
			assembly = this._referenceResolver(referenceName);
			return assembly != null;
		}

		// Token: 0x060039FB RID: 14843 RVA: 0x000C0031 File Offset: 0x000BE231
		internal override IEnumerable<Assembly> GetWildcardAssemblies()
		{
			IEnumerable<Assembly> enumerable = this._wildcardAssemblyEnumerator();
			if (enumerable == null)
			{
				throw new InvalidOperationException(Strings.WildcardEnumeratorReturnedNull);
			}
			return enumerable;
		}

		// Token: 0x04001358 RID: 4952
		private readonly Func<AssemblyName, Assembly> _referenceResolver;

		// Token: 0x04001359 RID: 4953
		private readonly Func<IEnumerable<Assembly>> _wildcardAssemblyEnumerator;
	}
}
