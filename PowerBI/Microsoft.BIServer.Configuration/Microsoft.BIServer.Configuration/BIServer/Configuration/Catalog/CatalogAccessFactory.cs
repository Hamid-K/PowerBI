using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.BIServer.Configuration.Exceptions;
using Microsoft.BIServer.HostingEnvironment;
using Microsoft.BIServer.HostingEnvironment.Storage;

namespace Microsoft.BIServer.Configuration.Catalog
{
	// Token: 0x0200003D RID: 61
	public static class CatalogAccessFactory
	{
		// Token: 0x060001F0 RID: 496 RVA: 0x00008344 File Offset: 0x00006544
		public static MeteredSqlConnection NewConnection()
		{
			string rsConnectionString = ConfigReader.Current.RsConnectionString;
			if (string.IsNullOrWhiteSpace(rsConnectionString))
			{
				throw new ConfigException("RS Connection String not set");
			}
			if (ConfigReader.Current.ConnectionType == CatalogConnectionType.Impersonate && !string.IsNullOrEmpty(ConfigReader.Current.CatalogDomain))
			{
				using (ImpersonationContext.EnterUserContext(new AccountCredentials(ConfigReader.Current.CatalogDomain, ConfigReader.Current.CatalogUser, ConfigReader.Current.CatalogCred, AccountType.WindowsUser)))
				{
					return new MeteredSqlConnection(rsConnectionString);
				}
			}
			return new MeteredSqlConnection(rsConnectionString);
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x000083E4 File Offset: 0x000065E4
		public static ScopedSqlTransaction NewTransaction(string name)
		{
			return CatalogAccessFactory.NewTransaction(CatalogAccessFactory.NewConnection(), name);
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x000083F1 File Offset: 0x000065F1
		public static ScopedSqlTransaction NewTransaction(MeteredSqlConnection connection, string name)
		{
			return new ScopedSqlTransaction(connection, name);
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x000083FC File Offset: 0x000065FC
		public static async Task<IList<TEntity>> QueryAsync<TEntity>(string storedProcedureName, object parameters)
		{
			IList<TEntity> list;
			using (ISqlAccess sqlAccess = CatalogAccessFactory.NewConnection())
			{
				list = (await sqlAccess.QueryAsync<TEntity>(storedProcedureName, parameters)).ToList<TEntity>();
			}
			return list;
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x0000844C File Offset: 0x0000664C
		public static async Task<TEntity> QueryFirstOrDefaultAsync<TEntity>(string storedProcedureName)
		{
			TEntity tentity;
			using (ISqlAccess sqlAccess = CatalogAccessFactory.NewConnection())
			{
				tentity = await sqlAccess.QueryFirstOrDefaultAsync<TEntity>(storedProcedureName);
			}
			return tentity;
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x00008494 File Offset: 0x00006694
		public static async Task<TEntity> QueryFirstOrDefaultAsync<TEntity>(string storedProcedureName, object parameters)
		{
			TEntity tentity;
			using (ISqlAccess sqlAccess = CatalogAccessFactory.NewConnection())
			{
				tentity = await sqlAccess.QueryFirstOrDefaultAsync<TEntity>(storedProcedureName, parameters);
			}
			return tentity;
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x000084E4 File Offset: 0x000066E4
		public static async Task<int> ExecuteAsync(string storedProcedureName, object parameters)
		{
			int num;
			using (ISqlAccess sqlAccess = CatalogAccessFactory.NewConnection())
			{
				num = await sqlAccess.ExecuteAsync(storedProcedureName, parameters);
			}
			return num;
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x00008534 File Offset: 0x00006734
		public static T ExecuteScalar<T>(string query, Dictionary<string, object> parameters)
		{
			T t;
			using (ISqlAccess sqlAccess = CatalogAccessFactory.NewConnection())
			{
				t = sqlAccess.ExecuteScalar<T>(query, parameters);
			}
			return t;
		}
	}
}
