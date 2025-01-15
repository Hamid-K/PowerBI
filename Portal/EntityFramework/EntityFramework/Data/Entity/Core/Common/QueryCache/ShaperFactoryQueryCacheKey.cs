using System;
using System.Data.Entity.Core.Objects;

namespace System.Data.Entity.Core.Common.QueryCache
{
	// Token: 0x0200062F RID: 1583
	internal class ShaperFactoryQueryCacheKey<T> : QueryCacheKey
	{
		// Token: 0x06004C3E RID: 19518 RVA: 0x0010C6FD File Offset: 0x0010A8FD
		internal ShaperFactoryQueryCacheKey(string columnMapKey, MergeOption mergeOption, bool streaming, bool isValueLayer)
		{
			this._columnMapKey = columnMapKey;
			this._mergeOption = mergeOption;
			this._isValueLayer = isValueLayer;
			this._streaming = streaming;
		}

		// Token: 0x06004C3F RID: 19519 RVA: 0x0010C724 File Offset: 0x0010A924
		public override bool Equals(object obj)
		{
			ShaperFactoryQueryCacheKey<T> shaperFactoryQueryCacheKey = obj as ShaperFactoryQueryCacheKey<T>;
			return shaperFactoryQueryCacheKey != null && (this._columnMapKey.Equals(shaperFactoryQueryCacheKey._columnMapKey, QueryCacheKey._stringComparison) && this._mergeOption == shaperFactoryQueryCacheKey._mergeOption && this._isValueLayer == shaperFactoryQueryCacheKey._isValueLayer) && this._streaming == shaperFactoryQueryCacheKey._streaming;
		}

		// Token: 0x06004C40 RID: 19520 RVA: 0x0010C781 File Offset: 0x0010A981
		public override int GetHashCode()
		{
			return this._columnMapKey.GetHashCode();
		}

		// Token: 0x04001AB1 RID: 6833
		private readonly string _columnMapKey;

		// Token: 0x04001AB2 RID: 6834
		private readonly MergeOption _mergeOption;

		// Token: 0x04001AB3 RID: 6835
		private readonly bool _isValueLayer;

		// Token: 0x04001AB4 RID: 6836
		private readonly bool _streaming;
	}
}
