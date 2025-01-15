using System;
using System.Collections;
using System.Data.Entity.Infrastructure;

namespace System.Data.Entity.Internal
{
	// Token: 0x0200011F RID: 287
	internal abstract class InternalSqlQuery : IEnumerable, IDbAsyncEnumerable
	{
		// Token: 0x06001427 RID: 5159 RVA: 0x00034594 File Offset: 0x00032794
		internal InternalSqlQuery(string sql, bool? streaming, object[] parameters)
		{
			this._sql = sql;
			this._parameters = parameters;
			this._streaming = streaming;
		}

		// Token: 0x17000539 RID: 1337
		// (get) Token: 0x06001428 RID: 5160 RVA: 0x000345B1 File Offset: 0x000327B1
		public string Sql
		{
			get
			{
				return this._sql;
			}
		}

		// Token: 0x1700053A RID: 1338
		// (get) Token: 0x06001429 RID: 5161 RVA: 0x000345B9 File Offset: 0x000327B9
		internal bool? Streaming
		{
			get
			{
				return this._streaming;
			}
		}

		// Token: 0x1700053B RID: 1339
		// (get) Token: 0x0600142A RID: 5162 RVA: 0x000345C1 File Offset: 0x000327C1
		public object[] Parameters
		{
			get
			{
				return this._parameters;
			}
		}

		// Token: 0x0600142B RID: 5163
		public abstract InternalSqlQuery AsNoTracking();

		// Token: 0x0600142C RID: 5164
		public abstract InternalSqlQuery AsStreaming();

		// Token: 0x0600142D RID: 5165
		public abstract IEnumerator GetEnumerator();

		// Token: 0x0600142E RID: 5166
		public abstract IDbAsyncEnumerator GetAsyncEnumerator();

		// Token: 0x0600142F RID: 5167 RVA: 0x000345C9 File Offset: 0x000327C9
		public override string ToString()
		{
			return this.Sql;
		}

		// Token: 0x0400097F RID: 2431
		private readonly string _sql;

		// Token: 0x04000980 RID: 2432
		private readonly object[] _parameters;

		// Token: 0x04000981 RID: 2433
		private readonly bool? _streaming;
	}
}
