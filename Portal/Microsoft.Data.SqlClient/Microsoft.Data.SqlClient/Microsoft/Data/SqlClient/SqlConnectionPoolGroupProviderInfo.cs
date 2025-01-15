using System;
using System.Security;
using Microsoft.Data.ProviderBase;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x02000069 RID: 105
	internal sealed class SqlConnectionPoolGroupProviderInfo : DbConnectionPoolGroupProviderInfo
	{
		// Token: 0x0600094F RID: 2383 RVA: 0x00017E24 File Offset: 0x00016024
		internal SqlConnectionPoolGroupProviderInfo(SqlConnectionString connectionOptions)
		{
			this._failoverPartner = connectionOptions.FailoverPartner;
			if (string.IsNullOrEmpty(this._failoverPartner))
			{
				this._failoverPartner = null;
			}
		}

		// Token: 0x17000693 RID: 1683
		// (get) Token: 0x06000950 RID: 2384 RVA: 0x00017E4C File Offset: 0x0001604C
		internal string FailoverPartner
		{
			get
			{
				return this._failoverPartner;
			}
		}

		// Token: 0x17000694 RID: 1684
		// (get) Token: 0x06000951 RID: 2385 RVA: 0x00017E54 File Offset: 0x00016054
		internal bool UseFailoverPartner
		{
			get
			{
				return this._useFailoverPartner;
			}
		}

		// Token: 0x06000952 RID: 2386 RVA: 0x00017E5C File Offset: 0x0001605C
		internal void AliasCheck(string server)
		{
			if (this._alias != server)
			{
				lock (this)
				{
					if (this._alias == null)
					{
						this._alias = server;
					}
					else if (this._alias != server)
					{
						SqlClientEventSource.Log.TryTraceEvent("SqlConnectionPoolGroupProviderInfo.AliasCheck | Info | Alias change detected. Clearing PoolGroup.");
						base.PoolGroup.Clear();
						this._alias = server;
					}
				}
			}
		}

		// Token: 0x06000953 RID: 2387 RVA: 0x00017EE0 File Offset: 0x000160E0
		internal void FailoverCheck(bool actualUseFailoverPartner, SqlConnectionString userConnectionOptions, string actualFailoverPartner)
		{
			if (this.UseFailoverPartner != actualUseFailoverPartner)
			{
				SqlClientEventSource.Log.TryTraceEvent<string>("SqlConnectionPoolGroupProviderInfo.FailoverCheck | Info | Failover detected. Failover partner '{0}'. Clearing PoolGroup", actualFailoverPartner);
				base.PoolGroup.Clear();
				this._useFailoverPartner = actualUseFailoverPartner;
			}
			if (!this._useFailoverPartner && this._failoverPartner != actualFailoverPartner)
			{
				PermissionSet permissionSet = this.CreateFailoverPermission(userConnectionOptions, actualFailoverPartner);
				lock (this)
				{
					if (this._failoverPartner != actualFailoverPartner)
					{
						this._failoverPartner = actualFailoverPartner;
						this._failoverPermissionSet = permissionSet;
					}
				}
			}
		}

		// Token: 0x06000954 RID: 2388 RVA: 0x00017F7C File Offset: 0x0001617C
		private PermissionSet CreateFailoverPermission(SqlConnectionString userConnectionOptions, string actualFailoverPartner)
		{
			string text;
			if (userConnectionOptions.ContainsKey("Failover Partner") && userConnectionOptions["Failover Partner"] == null)
			{
				text = "Data Source";
			}
			else
			{
				text = "Failover Partner";
			}
			string text2 = userConnectionOptions.ExpandKeyword(text, actualFailoverPartner);
			return new SqlConnectionString(text2).CreatePermissionSet();
		}

		// Token: 0x06000955 RID: 2389 RVA: 0x00017FC8 File Offset: 0x000161C8
		internal void FailoverPermissionDemand()
		{
			if (this._useFailoverPartner)
			{
				PermissionSet failoverPermissionSet = this._failoverPermissionSet;
				if (failoverPermissionSet != null)
				{
					failoverPermissionSet.Demand();
				}
			}
		}

		// Token: 0x04000190 RID: 400
		private string _alias;

		// Token: 0x04000191 RID: 401
		private string _failoverPartner;

		// Token: 0x04000192 RID: 402
		private bool _useFailoverPartner;

		// Token: 0x04000193 RID: 403
		private PermissionSet _failoverPermissionSet;
	}
}
