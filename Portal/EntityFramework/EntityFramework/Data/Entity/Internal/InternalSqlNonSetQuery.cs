using System;
using System.Collections;
using System.Data.Entity.Infrastructure;

namespace System.Data.Entity.Internal
{
	// Token: 0x0200011E RID: 286
	internal class InternalSqlNonSetQuery : InternalSqlQuery
	{
		// Token: 0x06001421 RID: 5153 RVA: 0x000344B8 File Offset: 0x000326B8
		internal InternalSqlNonSetQuery(InternalContext internalContext, Type elementType, string sql, object[] parameters)
			: this(internalContext, elementType, sql, null, parameters)
		{
		}

		// Token: 0x06001422 RID: 5154 RVA: 0x000344D9 File Offset: 0x000326D9
		private InternalSqlNonSetQuery(InternalContext internalContext, Type elementType, string sql, bool? streaming, object[] parameters)
			: base(sql, streaming, parameters)
		{
			this._internalContext = internalContext;
			this._elementType = elementType;
		}

		// Token: 0x06001423 RID: 5155 RVA: 0x000344F4 File Offset: 0x000326F4
		public override InternalSqlQuery AsNoTracking()
		{
			return this;
		}

		// Token: 0x06001424 RID: 5156 RVA: 0x000344F8 File Offset: 0x000326F8
		public override InternalSqlQuery AsStreaming()
		{
			if (base.Streaming == null || !base.Streaming.Value)
			{
				return new InternalSqlNonSetQuery(this._internalContext, this._elementType, base.Sql, new bool?(true), base.Parameters);
			}
			return this;
		}

		// Token: 0x06001425 RID: 5157 RVA: 0x0003454A File Offset: 0x0003274A
		public override IEnumerator GetEnumerator()
		{
			return this._internalContext.ExecuteSqlQuery(this._elementType, base.Sql, base.Streaming, base.Parameters);
		}

		// Token: 0x06001426 RID: 5158 RVA: 0x0003456F File Offset: 0x0003276F
		public override IDbAsyncEnumerator GetAsyncEnumerator()
		{
			return this._internalContext.ExecuteSqlQueryAsync(this._elementType, base.Sql, base.Streaming, base.Parameters);
		}

		// Token: 0x0400097D RID: 2429
		private readonly InternalContext _internalContext;

		// Token: 0x0400097E RID: 2430
		private readonly Type _elementType;
	}
}
