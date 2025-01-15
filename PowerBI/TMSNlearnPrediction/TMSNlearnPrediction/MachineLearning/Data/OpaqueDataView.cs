using System;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x020001C0 RID: 448
	public sealed class OpaqueDataView : IDataView, ISchematized
	{
		// Token: 0x06000A02 RID: 2562 RVA: 0x00035B57 File Offset: 0x00033D57
		public OpaqueDataView(IDataView source)
		{
			this._source = source;
		}

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x06000A03 RID: 2563 RVA: 0x00035B66 File Offset: 0x00033D66
		public bool CanShuffle
		{
			get
			{
				return this._source.CanShuffle;
			}
		}

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x06000A04 RID: 2564 RVA: 0x00035B73 File Offset: 0x00033D73
		public ISchema Schema
		{
			get
			{
				return this._source.Schema;
			}
		}

		// Token: 0x06000A05 RID: 2565 RVA: 0x00035B80 File Offset: 0x00033D80
		public long? GetRowCount(bool lazy = true)
		{
			return this._source.GetRowCount(lazy);
		}

		// Token: 0x06000A06 RID: 2566 RVA: 0x00035B8E File Offset: 0x00033D8E
		public IRowCursor GetRowCursor(Func<int, bool> predicate, IRandom rand = null)
		{
			return this._source.GetRowCursor(predicate, rand);
		}

		// Token: 0x06000A07 RID: 2567 RVA: 0x00035B9D File Offset: 0x00033D9D
		public IRowCursor[] GetRowCursorSet(out IRowCursorConsolidator consolidator, Func<int, bool> predicate, int n, IRandom rand = null)
		{
			return this._source.GetRowCursorSet(ref consolidator, predicate, n, rand);
		}

		// Token: 0x04000525 RID: 1317
		private readonly IDataView _source;
	}
}
