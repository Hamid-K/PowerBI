using System;
using System.ComponentModel;
using System.Data.Common;
using System.Data.Entity.Utilities;
using System.Threading;
using System.Threading.Tasks;

namespace System.Data.Entity.Infrastructure.Interception
{
	// Token: 0x02000278 RID: 632
	public class DbCommandDispatcher
	{
		// Token: 0x170006E6 RID: 1766
		// (get) Token: 0x06001FE2 RID: 8162 RVA: 0x0005ADDC File Offset: 0x00058FDC
		internal InternalDispatcher<IDbCommandInterceptor> InternalDispatcher
		{
			get
			{
				return this._internalDispatcher;
			}
		}

		// Token: 0x06001FE3 RID: 8163 RVA: 0x0005ADE4 File Offset: 0x00058FE4
		internal DbCommandDispatcher()
		{
		}

		// Token: 0x06001FE4 RID: 8164 RVA: 0x0005ADF8 File Offset: 0x00058FF8
		public virtual int NonQuery(DbCommand command, DbCommandInterceptionContext interceptionContext)
		{
			Check.NotNull<DbCommand>(command, "command");
			Check.NotNull<DbCommandInterceptionContext>(interceptionContext, "interceptionContext");
			return this._internalDispatcher.Dispatch<DbCommand, DbCommandInterceptionContext<int>, int>(command, (DbCommand t, DbCommandInterceptionContext<int> c) => t.ExecuteNonQuery(), new DbCommandInterceptionContext<int>(interceptionContext), delegate(IDbCommandInterceptor i, DbCommand t, DbCommandInterceptionContext<int> c)
			{
				i.NonQueryExecuting(t, c);
			}, delegate(IDbCommandInterceptor i, DbCommand t, DbCommandInterceptionContext<int> c)
			{
				i.NonQueryExecuted(t, c);
			});
		}

		// Token: 0x06001FE5 RID: 8165 RVA: 0x0005AE8C File Offset: 0x0005908C
		public virtual object Scalar(DbCommand command, DbCommandInterceptionContext interceptionContext)
		{
			Check.NotNull<DbCommand>(command, "command");
			Check.NotNull<DbCommandInterceptionContext>(interceptionContext, "interceptionContext");
			return this._internalDispatcher.Dispatch<DbCommand, DbCommandInterceptionContext<object>, object>(command, (DbCommand t, DbCommandInterceptionContext<object> c) => t.ExecuteScalar(), new DbCommandInterceptionContext<object>(interceptionContext), delegate(IDbCommandInterceptor i, DbCommand t, DbCommandInterceptionContext<object> c)
			{
				i.ScalarExecuting(t, c);
			}, delegate(IDbCommandInterceptor i, DbCommand t, DbCommandInterceptionContext<object> c)
			{
				i.ScalarExecuted(t, c);
			});
		}

		// Token: 0x06001FE6 RID: 8166 RVA: 0x0005AF20 File Offset: 0x00059120
		public virtual DbDataReader Reader(DbCommand command, DbCommandInterceptionContext interceptionContext)
		{
			Check.NotNull<DbCommand>(command, "command");
			Check.NotNull<DbCommandInterceptionContext>(interceptionContext, "interceptionContext");
			return this._internalDispatcher.Dispatch<DbCommand, DbCommandInterceptionContext<DbDataReader>, DbDataReader>(command, (DbCommand t, DbCommandInterceptionContext<DbDataReader> c) => t.ExecuteReader(c.CommandBehavior), new DbCommandInterceptionContext<DbDataReader>(interceptionContext), delegate(IDbCommandInterceptor i, DbCommand t, DbCommandInterceptionContext<DbDataReader> c)
			{
				i.ReaderExecuting(t, c);
			}, delegate(IDbCommandInterceptor i, DbCommand t, DbCommandInterceptionContext<DbDataReader> c)
			{
				i.ReaderExecuted(t, c);
			});
		}

		// Token: 0x06001FE7 RID: 8167 RVA: 0x0005AFB4 File Offset: 0x000591B4
		public virtual Task<int> NonQueryAsync(DbCommand command, DbCommandInterceptionContext interceptionContext, CancellationToken cancellationToken)
		{
			Check.NotNull<DbCommand>(command, "command");
			Check.NotNull<DbCommandInterceptionContext>(interceptionContext, "interceptionContext");
			return this._internalDispatcher.DispatchAsync<DbCommand, DbCommandInterceptionContext<int>, int>(command, (DbCommand t, DbCommandInterceptionContext<int> c, CancellationToken ct) => t.ExecuteNonQueryAsync(ct), new DbCommandInterceptionContext<int>(interceptionContext).AsAsync(), delegate(IDbCommandInterceptor i, DbCommand t, DbCommandInterceptionContext<int> c)
			{
				i.NonQueryExecuting(t, c);
			}, delegate(IDbCommandInterceptor i, DbCommand t, DbCommandInterceptionContext<int> c)
			{
				i.NonQueryExecuted(t, c);
			}, cancellationToken);
		}

		// Token: 0x06001FE8 RID: 8168 RVA: 0x0005B050 File Offset: 0x00059250
		public virtual Task<object> ScalarAsync(DbCommand command, DbCommandInterceptionContext interceptionContext, CancellationToken cancellationToken)
		{
			Check.NotNull<DbCommand>(command, "command");
			Check.NotNull<DbCommandInterceptionContext>(interceptionContext, "interceptionContext");
			return this._internalDispatcher.DispatchAsync<DbCommand, DbCommandInterceptionContext<object>, object>(command, (DbCommand t, DbCommandInterceptionContext<object> c, CancellationToken ct) => t.ExecuteScalarAsync(ct), new DbCommandInterceptionContext<object>(interceptionContext).AsAsync(), delegate(IDbCommandInterceptor i, DbCommand t, DbCommandInterceptionContext<object> c)
			{
				i.ScalarExecuting(t, c);
			}, delegate(IDbCommandInterceptor i, DbCommand t, DbCommandInterceptionContext<object> c)
			{
				i.ScalarExecuted(t, c);
			}, cancellationToken);
		}

		// Token: 0x06001FE9 RID: 8169 RVA: 0x0005B0EC File Offset: 0x000592EC
		public virtual Task<DbDataReader> ReaderAsync(DbCommand command, DbCommandInterceptionContext interceptionContext, CancellationToken cancellationToken)
		{
			Check.NotNull<DbCommand>(command, "command");
			Check.NotNull<DbCommandInterceptionContext>(interceptionContext, "interceptionContext");
			return this._internalDispatcher.DispatchAsync<DbCommand, DbCommandInterceptionContext<DbDataReader>, DbDataReader>(command, (DbCommand t, DbCommandInterceptionContext<DbDataReader> c, CancellationToken ct) => t.ExecuteReaderAsync(c.CommandBehavior, ct), new DbCommandInterceptionContext<DbDataReader>(interceptionContext).AsAsync(), delegate(IDbCommandInterceptor i, DbCommand t, DbCommandInterceptionContext<DbDataReader> c)
			{
				i.ReaderExecuting(t, c);
			}, delegate(IDbCommandInterceptor i, DbCommand t, DbCommandInterceptionContext<DbDataReader> c)
			{
				i.ReaderExecuted(t, c);
			}, cancellationToken);
		}

		// Token: 0x06001FEA RID: 8170 RVA: 0x0005B186 File Offset: 0x00059386
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x06001FEB RID: 8171 RVA: 0x0005B18E File Offset: 0x0005938E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x06001FEC RID: 8172 RVA: 0x0005B197 File Offset: 0x00059397
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06001FED RID: 8173 RVA: 0x0005B19F File Offset: 0x0005939F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new Type GetType()
		{
			return base.GetType();
		}

		// Token: 0x04000B75 RID: 2933
		private readonly InternalDispatcher<IDbCommandInterceptor> _internalDispatcher = new InternalDispatcher<IDbCommandInterceptor>();
	}
}
