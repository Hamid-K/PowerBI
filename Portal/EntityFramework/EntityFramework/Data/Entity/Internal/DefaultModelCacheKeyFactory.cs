using System;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Internal
{
	// Token: 0x020000FA RID: 250
	internal sealed class DefaultModelCacheKeyFactory
	{
		// Token: 0x06001256 RID: 4694 RVA: 0x00030360 File Offset: 0x0002E560
		public IDbModelCacheKey Create(DbContext context)
		{
			Check.NotNull<DbContext>(context, "context");
			string text = null;
			IDbModelCacheKeyProvider dbModelCacheKeyProvider = context as IDbModelCacheKeyProvider;
			if (dbModelCacheKeyProvider != null)
			{
				text = dbModelCacheKeyProvider.CacheKey;
			}
			return new DefaultModelCacheKey(context.GetType(), context.InternalContext.ProviderName, context.InternalContext.ProviderFactory.GetType(), text);
		}
	}
}
