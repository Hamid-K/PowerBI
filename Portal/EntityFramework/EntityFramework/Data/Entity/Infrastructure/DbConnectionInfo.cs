using System;
using System.ComponentModel;
using System.Configuration;
using System.Data.Entity.Internal;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Infrastructure
{
	// Token: 0x02000222 RID: 546
	[Serializable]
	public class DbConnectionInfo
	{
		// Token: 0x06001C9A RID: 7322 RVA: 0x00051E35 File Offset: 0x00050035
		public DbConnectionInfo(string connectionName)
		{
			Check.NotEmpty(connectionName, "connectionName");
			this._connectionName = connectionName;
		}

		// Token: 0x06001C9B RID: 7323 RVA: 0x00051E50 File Offset: 0x00050050
		public DbConnectionInfo(string connectionString, string providerInvariantName)
		{
			Check.NotEmpty(connectionString, "connectionString");
			Check.NotEmpty(providerInvariantName, "providerInvariantName");
			this._connectionString = connectionString;
			this._providerInvariantName = providerInvariantName;
		}

		// Token: 0x06001C9C RID: 7324 RVA: 0x00051E7E File Offset: 0x0005007E
		internal ConnectionStringSettings GetConnectionString(AppConfig config)
		{
			if (this._connectionName == null)
			{
				return new ConnectionStringSettings(null, this._connectionString, this._providerInvariantName);
			}
			ConnectionStringSettings connectionString = config.GetConnectionString(this._connectionName);
			if (connectionString == null)
			{
				throw Error.DbConnectionInfo_ConnectionStringNotFound(this._connectionName);
			}
			return connectionString;
		}

		// Token: 0x06001C9D RID: 7325 RVA: 0x00051EB6 File Offset: 0x000500B6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x06001C9E RID: 7326 RVA: 0x00051EBE File Offset: 0x000500BE
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x06001C9F RID: 7327 RVA: 0x00051EC7 File Offset: 0x000500C7
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06001CA0 RID: 7328 RVA: 0x00051ECF File Offset: 0x000500CF
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new Type GetType()
		{
			return base.GetType();
		}

		// Token: 0x04000AFA RID: 2810
		private readonly string _connectionName;

		// Token: 0x04000AFB RID: 2811
		private readonly string _connectionString;

		// Token: 0x04000AFC RID: 2812
		private readonly string _providerInvariantName;
	}
}
