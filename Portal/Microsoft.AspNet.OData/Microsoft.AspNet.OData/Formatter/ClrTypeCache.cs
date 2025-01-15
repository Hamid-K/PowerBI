using System;
using System.Collections.Concurrent;
using Microsoft.OData.Edm;

namespace Microsoft.AspNet.OData.Formatter
{
	// Token: 0x0200018E RID: 398
	internal class ClrTypeCache
	{
		// Token: 0x06000D03 RID: 3331 RVA: 0x00033F08 File Offset: 0x00032108
		public IEdmTypeReference GetEdmType(Type clrType, IEdmModel model)
		{
			IEdmTypeReference edmTypeReference;
			if (!this._cache.TryGetValue(clrType, out edmTypeReference))
			{
				edmTypeReference = model.GetEdmTypeReference(clrType);
				this._cache[clrType] = edmTypeReference;
			}
			return edmTypeReference;
		}

		// Token: 0x040003B5 RID: 949
		private ConcurrentDictionary<Type, IEdmTypeReference> _cache = new ConcurrentDictionary<Type, IEdmTypeReference>();
	}
}
