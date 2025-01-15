using System;
using System.Diagnostics;
using System.Security.Principal;
using Microsoft.Data.SqlClient;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.HostingInterfaces;
using RSRemoteRpcClient;
using Util;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x0200003D RID: 61
	internal sealed class ConnectionConfig : IDisposable
	{
		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060001B4 RID: 436 RVA: 0x00009DAC File Offset: 0x00007FAC
		internal static IRsUnmanagedCallback IRsUnmanagedCallback
		{
			get
			{
				IServiceProvider serviceProvider = AppDomain.CurrentDomain.DomainManager as IServiceProvider;
				if (serviceProvider != null)
				{
					return (IRsUnmanagedCallback)serviceProvider.GetService(typeof(IRsUnmanagedCallback));
				}
				return null;
			}
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x00009DE4 File Offset: 0x00007FE4
		internal ConnectionConfig()
		{
			SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder(Globals.Configuration.BaseConnectionString);
			int num = 256;
			if (ConnectionConfig._maxWorkers == 0 && ConnectionConfig.IRsUnmanagedCallback != null)
			{
				ConnectionConfig._maxWorkers = ConnectionConfig.IRsUnmanagedCallback.GetMaxWorkers();
			}
			if (ConnectionConfig._maxWorkers != 0)
			{
				num = ConnectionConfig._maxWorkers;
			}
			sqlConnectionStringBuilder.MaxPoolSize = num * 3;
			if (Globals.Configuration.MaxCatalogConnectionPoolSizePerProcess != 0)
			{
				sqlConnectionStringBuilder.MaxPoolSize = Globals.Configuration.MaxCatalogConnectionPoolSizePerProcess;
			}
			RSTrace.CatalogTrace.Trace(TraceLevel.Info, string.Format("Catalog max connection pool size: {0}", sqlConnectionStringBuilder.MaxPoolSize));
			sqlConnectionStringBuilder.ConnectTimeout = Globals.Configuration.ConnectionTimeout;
			this._connectionString = new SecureStringWrapper(sqlConnectionStringBuilder.ToString());
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060001B6 RID: 438 RVA: 0x00009EA8 File Offset: 0x000080A8
		internal SecureStringWrapper ConnectionString
		{
			get
			{
				return this._connectionString;
			}
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x00009EB0 File Offset: 0x000080B0
		internal WindowsImpersonationContext ImpersonateCatUser()
		{
			object catUserSync = this._catUserSync;
			WindowsImpersonationContext windowsImpersonationContext;
			lock (catUserSync)
			{
				if (this._catUser == null)
				{
					this._catUser = ConnectionConfig.GetImpersonationUser(Globals.Configuration.CatalogUser, Globals.Configuration.CatalogDomain, Globals.Configuration.CatalogCred);
				}
				windowsImpersonationContext = this._catUser.Impersonate();
			}
			return windowsImpersonationContext;
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x00009F28 File Offset: 0x00008128
		private static WindowsIdentity GetImpersonationUser(string userName, string domain, string userPwd)
		{
			SafeToken safeToken = null;
			WindowsIdentity windowsIdentity;
			try
			{
				safeToken = RemoteLogon.GetRemoteImpToken(Globals.TryRemoteLogon ? Globals.Configuration.RpcEndpoint : null, 2, Guid.Empty, userName, domain, userPwd);
				windowsIdentity = new WindowsIdentity(safeToken.DangerousGetHandle());
			}
			catch (Exception ex)
			{
				throw new ReportServerDatabaseLogonFailedException(ex);
			}
			finally
			{
				if (safeToken != null)
				{
					safeToken.Close();
				}
			}
			return windowsIdentity;
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x00009F98 File Offset: 0x00008198
		void IDisposable.Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x060001BA RID: 442 RVA: 0x00009FA1 File Offset: 0x000081A1
		private void Dispose(bool disposing)
		{
			if (disposing && this._connectionString != null)
			{
				((IDisposable)this._connectionString).Dispose();
				this._connectionString = null;
			}
		}

		// Token: 0x04000186 RID: 390
		private SecureStringWrapper _connectionString;

		// Token: 0x04000187 RID: 391
		private static int _maxWorkers;

		// Token: 0x04000188 RID: 392
		private WindowsIdentity _catUser;

		// Token: 0x04000189 RID: 393
		private object _catUserSync = new object();
	}
}
