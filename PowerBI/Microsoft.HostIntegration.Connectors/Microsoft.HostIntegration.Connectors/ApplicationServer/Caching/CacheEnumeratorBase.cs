using System;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000091 RID: 145
	internal class CacheEnumeratorBase
	{
		// Token: 0x06000345 RID: 837 RVA: 0x0001109D File Offset: 0x0000F29D
		public CacheEnumeratorBase(DataCache cache, string region, DataCacheTag[] tags, GetByTagsOperation op, IMonitoringListener listener)
		{
			this._myCache = cache;
			this._myRegion = region;
			this._myTags = tags;
			this._myOp = op;
			this._listener = listener;
		}

		// Token: 0x0400029A RID: 666
		protected DataCache _myCache;

		// Token: 0x0400029B RID: 667
		protected string _myRegion;

		// Token: 0x0400029C RID: 668
		protected DataCacheTag[] _myTags;

		// Token: 0x0400029D RID: 669
		protected GetByTagsOperation _myOp;

		// Token: 0x0400029E RID: 670
		protected object _myState;

		// Token: 0x0400029F RID: 671
		protected bool _isValid = true;

		// Token: 0x040002A0 RID: 672
		protected bool _more = true;

		// Token: 0x040002A1 RID: 673
		protected IMonitoringListener _listener;
	}
}
