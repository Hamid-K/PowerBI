using System;

namespace Microsoft.Data.Common
{
	// Token: 0x0200017E RID: 382
	internal class DbConnectionPoolKey : ICloneable
	{
		// Token: 0x06001CC3 RID: 7363 RVA: 0x0007550E File Offset: 0x0007370E
		internal DbConnectionPoolKey(string connectionString)
		{
			this._connectionString = connectionString;
		}

		// Token: 0x06001CC4 RID: 7364 RVA: 0x0007551D File Offset: 0x0007371D
		protected DbConnectionPoolKey(DbConnectionPoolKey key)
		{
			this._connectionString = key.ConnectionString;
		}

		// Token: 0x06001CC5 RID: 7365 RVA: 0x00075531 File Offset: 0x00073731
		public virtual object Clone()
		{
			return new DbConnectionPoolKey(this);
		}

		// Token: 0x17000A0B RID: 2571
		// (get) Token: 0x06001CC6 RID: 7366 RVA: 0x00075539 File Offset: 0x00073739
		// (set) Token: 0x06001CC7 RID: 7367 RVA: 0x00075541 File Offset: 0x00073741
		internal virtual string ConnectionString
		{
			get
			{
				return this._connectionString;
			}
			set
			{
				this._connectionString = value;
			}
		}

		// Token: 0x06001CC8 RID: 7368 RVA: 0x0007554C File Offset: 0x0007374C
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			DbConnectionPoolKey dbConnectionPoolKey = obj as DbConnectionPoolKey;
			return dbConnectionPoolKey != null && this._connectionString == dbConnectionPoolKey._connectionString;
		}

		// Token: 0x06001CC9 RID: 7369 RVA: 0x0007557B File Offset: 0x0007377B
		public override int GetHashCode()
		{
			if (this._connectionString != null)
			{
				return this._connectionString.GetHashCode();
			}
			return 0;
		}

		// Token: 0x04000C12 RID: 3090
		private string _connectionString;
	}
}
