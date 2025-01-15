using System;
using System.Data.Common;
using System.Data.Entity.Utilities;
using System.Globalization;

namespace System.Data.Entity.Infrastructure
{
	// Token: 0x02000255 RID: 597
	public sealed class LocalDbConnectionFactory : IDbConnectionFactory
	{
		// Token: 0x06001EC2 RID: 7874 RVA: 0x000558F6 File Offset: 0x00053AF6
		public LocalDbConnectionFactory()
			: this("mssqllocaldb")
		{
		}

		// Token: 0x06001EC3 RID: 7875 RVA: 0x00055903 File Offset: 0x00053B03
		public LocalDbConnectionFactory(string localDbVersion)
		{
			Check.NotEmpty(localDbVersion, "localDbVersion");
			this._localDbVersion = localDbVersion;
			this._baseConnectionString = "Integrated Security=True; MultipleActiveResultSets=True;";
		}

		// Token: 0x06001EC4 RID: 7876 RVA: 0x00055929 File Offset: 0x00053B29
		public LocalDbConnectionFactory(string localDbVersion, string baseConnectionString)
		{
			Check.NotEmpty(localDbVersion, "localDbVersion");
			Check.NotNull<string>(baseConnectionString, "baseConnectionString");
			this._localDbVersion = localDbVersion;
			this._baseConnectionString = baseConnectionString;
		}

		// Token: 0x170006C5 RID: 1733
		// (get) Token: 0x06001EC5 RID: 7877 RVA: 0x00055957 File Offset: 0x00053B57
		public string BaseConnectionString
		{
			get
			{
				return this._baseConnectionString;
			}
		}

		// Token: 0x06001EC6 RID: 7878 RVA: 0x00055960 File Offset: 0x00053B60
		public DbConnection CreateConnection(string nameOrConnectionString)
		{
			Check.NotEmpty(nameOrConnectionString, "nameOrConnectionString");
			string text = " ";
			if (!string.IsNullOrEmpty(AppDomain.CurrentDomain.GetData("DataDirectory") as string))
			{
				text = string.Format(CultureInfo.InvariantCulture, " AttachDbFilename=|DataDirectory|{0}.mdf; ", new object[] { nameOrConnectionString });
			}
			return new SqlConnectionFactory(string.Format(CultureInfo.InvariantCulture, "Data Source=(localdb)\\{1};{0};{2}", new object[] { this._baseConnectionString, this._localDbVersion, text })).CreateConnection(nameOrConnectionString);
		}

		// Token: 0x04000B33 RID: 2867
		private readonly string _baseConnectionString;

		// Token: 0x04000B34 RID: 2868
		private readonly string _localDbVersion;
	}
}
