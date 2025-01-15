using System;
using System.ComponentModel;

namespace System.Data.Entity.Infrastructure.Interception
{
	// Token: 0x02000284 RID: 644
	public class DbDispatchers
	{
		// Token: 0x06002073 RID: 8307 RVA: 0x0005BFD0 File Offset: 0x0005A1D0
		internal DbDispatchers()
		{
		}

		// Token: 0x170006FB RID: 1787
		// (get) Token: 0x06002074 RID: 8308 RVA: 0x0005C030 File Offset: 0x0005A230
		internal virtual DbCommandTreeDispatcher CommandTree
		{
			get
			{
				return this._commandTreeDispatcher;
			}
		}

		// Token: 0x170006FC RID: 1788
		// (get) Token: 0x06002075 RID: 8309 RVA: 0x0005C038 File Offset: 0x0005A238
		public virtual DbCommandDispatcher Command
		{
			get
			{
				return this._commandDispatcher;
			}
		}

		// Token: 0x170006FD RID: 1789
		// (get) Token: 0x06002076 RID: 8310 RVA: 0x0005C040 File Offset: 0x0005A240
		public virtual DbTransactionDispatcher Transaction
		{
			get
			{
				return this._transactionDispatcher;
			}
		}

		// Token: 0x170006FE RID: 1790
		// (get) Token: 0x06002077 RID: 8311 RVA: 0x0005C048 File Offset: 0x0005A248
		public virtual DbConnectionDispatcher Connection
		{
			get
			{
				return this._dbConnectionDispatcher;
			}
		}

		// Token: 0x170006FF RID: 1791
		// (get) Token: 0x06002078 RID: 8312 RVA: 0x0005C050 File Offset: 0x0005A250
		internal virtual DbConfigurationDispatcher Configuration
		{
			get
			{
				return this._configurationDispatcher;
			}
		}

		// Token: 0x17000700 RID: 1792
		// (get) Token: 0x06002079 RID: 8313 RVA: 0x0005C058 File Offset: 0x0005A258
		internal virtual CancelableEntityConnectionDispatcher CancelableEntityConnection
		{
			get
			{
				return this._cancelableEntityConnectionDispatcher;
			}
		}

		// Token: 0x17000701 RID: 1793
		// (get) Token: 0x0600207A RID: 8314 RVA: 0x0005C060 File Offset: 0x0005A260
		internal virtual CancelableDbCommandDispatcher CancelableCommand
		{
			get
			{
				return this._cancelableCommandDispatcher;
			}
		}

		// Token: 0x0600207B RID: 8315 RVA: 0x0005C068 File Offset: 0x0005A268
		internal virtual void AddInterceptor(IDbInterceptor interceptor)
		{
			this._commandTreeDispatcher.InternalDispatcher.Add(interceptor);
			this._commandDispatcher.InternalDispatcher.Add(interceptor);
			this._transactionDispatcher.InternalDispatcher.Add(interceptor);
			this._dbConnectionDispatcher.InternalDispatcher.Add(interceptor);
			this._cancelableEntityConnectionDispatcher.InternalDispatcher.Add(interceptor);
			this._cancelableCommandDispatcher.InternalDispatcher.Add(interceptor);
			this._configurationDispatcher.InternalDispatcher.Add(interceptor);
		}

		// Token: 0x0600207C RID: 8316 RVA: 0x0005C0EC File Offset: 0x0005A2EC
		internal virtual void RemoveInterceptor(IDbInterceptor interceptor)
		{
			this._commandTreeDispatcher.InternalDispatcher.Remove(interceptor);
			this._commandDispatcher.InternalDispatcher.Remove(interceptor);
			this._transactionDispatcher.InternalDispatcher.Remove(interceptor);
			this._dbConnectionDispatcher.InternalDispatcher.Remove(interceptor);
			this._cancelableEntityConnectionDispatcher.InternalDispatcher.Remove(interceptor);
			this._cancelableCommandDispatcher.InternalDispatcher.Remove(interceptor);
			this._configurationDispatcher.InternalDispatcher.Remove(interceptor);
		}

		// Token: 0x0600207D RID: 8317 RVA: 0x0005C170 File Offset: 0x0005A370
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x0600207E RID: 8318 RVA: 0x0005C178 File Offset: 0x0005A378
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x0600207F RID: 8319 RVA: 0x0005C181 File Offset: 0x0005A381
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06002080 RID: 8320 RVA: 0x0005C189 File Offset: 0x0005A389
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new Type GetType()
		{
			return base.GetType();
		}

		// Token: 0x04000B7C RID: 2940
		private readonly DbCommandTreeDispatcher _commandTreeDispatcher = new DbCommandTreeDispatcher();

		// Token: 0x04000B7D RID: 2941
		private readonly DbCommandDispatcher _commandDispatcher = new DbCommandDispatcher();

		// Token: 0x04000B7E RID: 2942
		private readonly DbTransactionDispatcher _transactionDispatcher = new DbTransactionDispatcher();

		// Token: 0x04000B7F RID: 2943
		private readonly DbConnectionDispatcher _dbConnectionDispatcher = new DbConnectionDispatcher();

		// Token: 0x04000B80 RID: 2944
		private readonly DbConfigurationDispatcher _configurationDispatcher = new DbConfigurationDispatcher();

		// Token: 0x04000B81 RID: 2945
		private readonly CancelableEntityConnectionDispatcher _cancelableEntityConnectionDispatcher = new CancelableEntityConnectionDispatcher();

		// Token: 0x04000B82 RID: 2946
		private readonly CancelableDbCommandDispatcher _cancelableCommandDispatcher = new CancelableDbCommandDispatcher();
	}
}
