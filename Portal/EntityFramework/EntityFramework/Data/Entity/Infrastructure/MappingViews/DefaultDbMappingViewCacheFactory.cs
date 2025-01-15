using System;

namespace System.Data.Entity.Infrastructure.MappingViews
{
	// Token: 0x02000272 RID: 626
	internal class DefaultDbMappingViewCacheFactory : DbMappingViewCacheFactory
	{
		// Token: 0x06001F87 RID: 8071 RVA: 0x00059FA8 File Offset: 0x000581A8
		public DefaultDbMappingViewCacheFactory(Type cacheType)
		{
			this._cacheType = cacheType;
		}

		// Token: 0x06001F88 RID: 8072 RVA: 0x00059FB7 File Offset: 0x000581B7
		public override DbMappingViewCache Create(string conceptualModelContainerName, string storeModelContainerName)
		{
			return (DbMappingViewCache)Activator.CreateInstance(this._cacheType);
		}

		// Token: 0x06001F89 RID: 8073 RVA: 0x00059FC9 File Offset: 0x000581C9
		public override int GetHashCode()
		{
			return (this._cacheType.GetHashCode() * 397) ^ typeof(DefaultDbMappingViewCacheFactory).GetHashCode();
		}

		// Token: 0x06001F8A RID: 8074 RVA: 0x00059FEC File Offset: 0x000581EC
		public override bool Equals(object obj)
		{
			DefaultDbMappingViewCacheFactory defaultDbMappingViewCacheFactory = obj as DefaultDbMappingViewCacheFactory;
			return defaultDbMappingViewCacheFactory != null && defaultDbMappingViewCacheFactory._cacheType == this._cacheType;
		}

		// Token: 0x04000B6A RID: 2922
		private readonly Type _cacheType;
	}
}
