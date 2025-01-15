using System;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;
using System.Linq;

namespace System.Data.Entity.Utilities
{
	// Token: 0x02000079 RID: 121
	internal static class DbProviderManifestExtensions
	{
		// Token: 0x06000439 RID: 1081 RVA: 0x0000FB54 File Offset: 0x0000DD54
		public static PrimitiveType GetStoreTypeFromName(this DbProviderManifest providerManifest, string name)
		{
			PrimitiveType primitiveType = providerManifest.GetStoreTypes().SingleOrDefault((PrimitiveType p) => string.Equals(p.Name, name, StringComparison.OrdinalIgnoreCase));
			if (primitiveType == null)
			{
				throw Error.StoreTypeNotFound(name, providerManifest.NamespaceName);
			}
			return primitiveType;
		}
	}
}
