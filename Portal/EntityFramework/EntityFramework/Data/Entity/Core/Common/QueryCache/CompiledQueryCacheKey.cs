using System;

namespace System.Data.Entity.Core.Common.QueryCache
{
	// Token: 0x02000628 RID: 1576
	internal sealed class CompiledQueryCacheKey : QueryCacheKey
	{
		// Token: 0x06004C15 RID: 19477 RVA: 0x0010BB8D File Offset: 0x00109D8D
		internal CompiledQueryCacheKey(Guid cacheIdentity)
		{
			this._cacheIdentity = cacheIdentity;
		}

		// Token: 0x06004C16 RID: 19478 RVA: 0x0010BB9C File Offset: 0x00109D9C
		public override bool Equals(object compareTo)
		{
			return !(typeof(CompiledQueryCacheKey) != compareTo.GetType()) && ((CompiledQueryCacheKey)compareTo)._cacheIdentity.Equals(this._cacheIdentity);
		}

		// Token: 0x06004C17 RID: 19479 RVA: 0x0010BBDC File Offset: 0x00109DDC
		public override int GetHashCode()
		{
			return this._cacheIdentity.GetHashCode();
		}

		// Token: 0x06004C18 RID: 19480 RVA: 0x0010BC00 File Offset: 0x00109E00
		public override string ToString()
		{
			return this._cacheIdentity.ToString();
		}

		// Token: 0x04001A8C RID: 6796
		private readonly Guid _cacheIdentity;
	}
}
