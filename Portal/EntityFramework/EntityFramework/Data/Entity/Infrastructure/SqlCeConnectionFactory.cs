using System;
using System.Data.Common;
using System.Data.Entity.Infrastructure.DependencyResolution;
using System.Data.Entity.Infrastructure.Interception;
using System.Data.Entity.Internal;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Globalization;
using System.IO;

namespace System.Data.Entity.Infrastructure
{
	// Token: 0x0200025D RID: 605
	public sealed class SqlCeConnectionFactory : IDbConnectionFactory
	{
		// Token: 0x06001EE0 RID: 7904 RVA: 0x00055BAD File Offset: 0x00053DAD
		public SqlCeConnectionFactory(string providerInvariantName)
		{
			Check.NotEmpty(providerInvariantName, "providerInvariantName");
			this._providerInvariantName = providerInvariantName;
			this._databaseDirectory = "|DataDirectory|";
			this._baseConnectionString = "";
		}

		// Token: 0x06001EE1 RID: 7905 RVA: 0x00055BE0 File Offset: 0x00053DE0
		public SqlCeConnectionFactory(string providerInvariantName, string databaseDirectory, string baseConnectionString)
		{
			Check.NotEmpty(providerInvariantName, "providerInvariantName");
			Check.NotNull<string>(databaseDirectory, "databaseDirectory");
			Check.NotNull<string>(baseConnectionString, "baseConnectionString");
			this._providerInvariantName = providerInvariantName;
			this._databaseDirectory = databaseDirectory;
			this._baseConnectionString = baseConnectionString;
		}

		// Token: 0x170006C9 RID: 1737
		// (get) Token: 0x06001EE2 RID: 7906 RVA: 0x00055C2C File Offset: 0x00053E2C
		public string DatabaseDirectory
		{
			get
			{
				return this._databaseDirectory;
			}
		}

		// Token: 0x170006CA RID: 1738
		// (get) Token: 0x06001EE3 RID: 7907 RVA: 0x00055C34 File Offset: 0x00053E34
		public string BaseConnectionString
		{
			get
			{
				return this._baseConnectionString;
			}
		}

		// Token: 0x170006CB RID: 1739
		// (get) Token: 0x06001EE4 RID: 7908 RVA: 0x00055C3C File Offset: 0x00053E3C
		public string ProviderInvariantName
		{
			get
			{
				return this._providerInvariantName;
			}
		}

		// Token: 0x06001EE5 RID: 7909 RVA: 0x00055C44 File Offset: 0x00053E44
		public DbConnection CreateConnection(string nameOrConnectionString)
		{
			Check.NotEmpty(nameOrConnectionString, "nameOrConnectionString");
			DbConnection dbConnection = DbConfiguration.DependencyResolver.GetService(this.ProviderInvariantName).CreateConnection();
			if (dbConnection == null)
			{
				throw Error.DbContext_ProviderReturnedNullConnection();
			}
			string text;
			if (DbHelpers.TreatAsConnectionString(nameOrConnectionString))
			{
				text = nameOrConnectionString;
			}
			else
			{
				if (!nameOrConnectionString.EndsWith(".sdf", true, null))
				{
					nameOrConnectionString += ".sdf";
				}
				string text2 = ((this.DatabaseDirectory.StartsWith("|", StringComparison.Ordinal) && this.DatabaseDirectory.EndsWith("|", StringComparison.Ordinal)) ? (this.DatabaseDirectory + nameOrConnectionString) : Path.Combine(this.DatabaseDirectory, nameOrConnectionString));
				text = string.Format(CultureInfo.InvariantCulture, "Data Source={0}; {1}", new object[] { text2, this.BaseConnectionString });
			}
			DbInterception.Dispatch.Connection.SetConnectionString(dbConnection, new DbConnectionPropertyInterceptionContext<string>().WithValue(text));
			return dbConnection;
		}

		// Token: 0x04000B3C RID: 2876
		private readonly string _databaseDirectory;

		// Token: 0x04000B3D RID: 2877
		private readonly string _baseConnectionString;

		// Token: 0x04000B3E RID: 2878
		private readonly string _providerInvariantName;
	}
}
