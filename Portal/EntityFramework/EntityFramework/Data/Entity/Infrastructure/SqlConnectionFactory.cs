using System;
using System.Data.Common;
using System.Data.Entity.Infrastructure.Interception;
using System.Data.Entity.Internal;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Data.SqlClient;

namespace System.Data.Entity.Infrastructure
{
	// Token: 0x0200025E RID: 606
	public sealed class SqlConnectionFactory : IDbConnectionFactory
	{
		// Token: 0x06001EE6 RID: 7910 RVA: 0x00055D27 File Offset: 0x00053F27
		public SqlConnectionFactory()
		{
			this._baseConnectionString = "Data Source=.\\SQLEXPRESS; Integrated Security=True; MultipleActiveResultSets=True;";
		}

		// Token: 0x06001EE7 RID: 7911 RVA: 0x00055D3A File Offset: 0x00053F3A
		public SqlConnectionFactory(string baseConnectionString)
		{
			Check.NotNull<string>(baseConnectionString, "baseConnectionString");
			this._baseConnectionString = baseConnectionString;
		}

		// Token: 0x170006CC RID: 1740
		// (get) Token: 0x06001EE8 RID: 7912 RVA: 0x00055D55 File Offset: 0x00053F55
		// (set) Token: 0x06001EE9 RID: 7913 RVA: 0x00055D71 File Offset: 0x00053F71
		internal Func<string, DbProviderFactory> ProviderFactory
		{
			get
			{
				return this._providerFactoryCreator ?? new Func<string, DbProviderFactory>(DbConfiguration.DependencyResolver.GetService<DbProviderFactory>);
			}
			set
			{
				this._providerFactoryCreator = value;
			}
		}

		// Token: 0x170006CD RID: 1741
		// (get) Token: 0x06001EEA RID: 7914 RVA: 0x00055D7A File Offset: 0x00053F7A
		public string BaseConnectionString
		{
			get
			{
				return this._baseConnectionString;
			}
		}

		// Token: 0x06001EEB RID: 7915 RVA: 0x00055D84 File Offset: 0x00053F84
		public DbConnection CreateConnection(string nameOrConnectionString)
		{
			Check.NotEmpty(nameOrConnectionString, "nameOrConnectionString");
			string text = nameOrConnectionString;
			if (!DbHelpers.TreatAsConnectionString(nameOrConnectionString))
			{
				if (nameOrConnectionString.EndsWith(".mdf", true, null))
				{
					throw Error.SqlConnectionFactory_MdfNotSupported(nameOrConnectionString);
				}
				text = new SqlConnectionStringBuilder(this.BaseConnectionString)
				{
					InitialCatalog = nameOrConnectionString
				}.ConnectionString;
			}
			DbConnection dbConnection = null;
			try
			{
				dbConnection = this.ProviderFactory("System.Data.SqlClient").CreateConnection();
				DbInterception.Dispatch.Connection.SetConnectionString(dbConnection, new DbConnectionPropertyInterceptionContext<string>().WithValue(text));
			}
			catch
			{
				dbConnection = new SqlConnection();
				DbInterception.Dispatch.Connection.SetConnectionString(dbConnection, new DbConnectionPropertyInterceptionContext<string>().WithValue(text));
			}
			return dbConnection;
		}

		// Token: 0x04000B3F RID: 2879
		private readonly string _baseConnectionString;

		// Token: 0x04000B40 RID: 2880
		private Func<string, DbProviderFactory> _providerFactoryCreator;
	}
}
