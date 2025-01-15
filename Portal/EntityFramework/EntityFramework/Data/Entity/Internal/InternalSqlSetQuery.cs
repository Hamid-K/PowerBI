using System;
using System.Collections;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Internal.Linq;

namespace System.Data.Entity.Internal
{
	// Token: 0x02000120 RID: 288
	internal class InternalSqlSetQuery : InternalSqlQuery
	{
		// Token: 0x06001430 RID: 5168 RVA: 0x000345D4 File Offset: 0x000327D4
		internal InternalSqlSetQuery(IInternalSet set, string sql, bool isNoTracking, object[] parameters)
			: this(set, sql, isNoTracking, null, parameters)
		{
		}

		// Token: 0x06001431 RID: 5169 RVA: 0x000345F5 File Offset: 0x000327F5
		private InternalSqlSetQuery(IInternalSet set, string sql, bool isNoTracking, bool? streaming, object[] parameters)
			: base(sql, streaming, parameters)
		{
			this._set = set;
			this._isNoTracking = isNoTracking;
		}

		// Token: 0x06001432 RID: 5170 RVA: 0x00034610 File Offset: 0x00032810
		public override InternalSqlQuery AsNoTracking()
		{
			if (!this._isNoTracking)
			{
				return new InternalSqlSetQuery(this._set, base.Sql, true, base.Streaming, base.Parameters);
			}
			return this;
		}

		// Token: 0x1700053C RID: 1340
		// (get) Token: 0x06001433 RID: 5171 RVA: 0x0003463A File Offset: 0x0003283A
		public bool IsNoTracking
		{
			get
			{
				return this._isNoTracking;
			}
		}

		// Token: 0x06001434 RID: 5172 RVA: 0x00034644 File Offset: 0x00032844
		public override InternalSqlQuery AsStreaming()
		{
			if (base.Streaming == null || !base.Streaming.Value)
			{
				return new InternalSqlSetQuery(this._set, base.Sql, this._isNoTracking, new bool?(true), base.Parameters);
			}
			return this;
		}

		// Token: 0x06001435 RID: 5173 RVA: 0x00034696 File Offset: 0x00032896
		public override IEnumerator GetEnumerator()
		{
			return this._set.ExecuteSqlQuery(base.Sql, this._isNoTracking, base.Streaming, base.Parameters);
		}

		// Token: 0x06001436 RID: 5174 RVA: 0x000346BB File Offset: 0x000328BB
		public override IDbAsyncEnumerator GetAsyncEnumerator()
		{
			return this._set.ExecuteSqlQueryAsync(base.Sql, this._isNoTracking, base.Streaming, base.Parameters);
		}

		// Token: 0x04000982 RID: 2434
		private readonly IInternalSet _set;

		// Token: 0x04000983 RID: 2435
		private readonly bool _isNoTracking;
	}
}
