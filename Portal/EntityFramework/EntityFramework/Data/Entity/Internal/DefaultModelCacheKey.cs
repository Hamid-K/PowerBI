using System;
using System.Data.Entity.Infrastructure;

namespace System.Data.Entity.Internal
{
	// Token: 0x020000F9 RID: 249
	internal sealed class DefaultModelCacheKey : IDbModelCacheKey
	{
		// Token: 0x06001252 RID: 4690 RVA: 0x0003025F File Offset: 0x0002E45F
		public DefaultModelCacheKey(Type contextType, string providerName, Type providerType, string customKey)
		{
			this._contextType = contextType;
			this._providerName = providerName;
			this._providerType = providerType;
			this._customKey = customKey;
		}

		// Token: 0x06001253 RID: 4691 RVA: 0x00030284 File Offset: 0x0002E484
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			if (this == obj)
			{
				return true;
			}
			DefaultModelCacheKey defaultModelCacheKey = obj as DefaultModelCacheKey;
			return defaultModelCacheKey != null && this.Equals(defaultModelCacheKey);
		}

		// Token: 0x06001254 RID: 4692 RVA: 0x000302B0 File Offset: 0x0002E4B0
		public override int GetHashCode()
		{
			return (this._contextType.GetHashCode() * 397) ^ this._providerName.GetHashCode() ^ this._providerType.GetHashCode() ^ ((!string.IsNullOrWhiteSpace(this._customKey)) ? this._customKey.GetHashCode() : 0);
		}

		// Token: 0x06001255 RID: 4693 RVA: 0x00030304 File Offset: 0x0002E504
		private bool Equals(DefaultModelCacheKey other)
		{
			return this._contextType == other._contextType && string.Equals(this._providerName, other._providerName) && object.Equals(this._providerType, other._providerType) && string.Equals(this._customKey, other._customKey);
		}

		// Token: 0x04000915 RID: 2325
		private readonly Type _contextType;

		// Token: 0x04000916 RID: 2326
		private readonly string _providerName;

		// Token: 0x04000917 RID: 2327
		private readonly Type _providerType;

		// Token: 0x04000918 RID: 2328
		private readonly string _customKey;
	}
}
