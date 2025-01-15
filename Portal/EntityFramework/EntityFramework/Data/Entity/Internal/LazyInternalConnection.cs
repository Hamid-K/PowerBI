using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Data.Entity.Core.EntityClient;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Infrastructure.DependencyResolution;
using System.Data.Entity.Infrastructure.Interception;
using System.Data.Entity.Resources;
using System.Linq;

namespace System.Data.Entity.Internal
{
	// Token: 0x02000123 RID: 291
	internal class LazyInternalConnection : InternalConnection
	{
		// Token: 0x06001443 RID: 5187 RVA: 0x00034875 File Offset: 0x00032A75
		public LazyInternalConnection(string nameOrConnectionString)
			: this(null, nameOrConnectionString)
		{
		}

		// Token: 0x06001444 RID: 5188 RVA: 0x0003487F File Offset: 0x00032A7F
		public LazyInternalConnection(DbContext context, string nameOrConnectionString)
			: base((context == null) ? null : new DbInterceptionContext().WithDbContext(context))
		{
			this._nameOrConnectionString = nameOrConnectionString;
			this.AppConfig = AppConfig.DefaultInstance;
		}

		// Token: 0x06001445 RID: 5189 RVA: 0x000348AA File Offset: 0x00032AAA
		public LazyInternalConnection(DbContext context, DbConnectionInfo connectionInfo)
			: base(new DbInterceptionContext().WithDbContext(context))
		{
			this._connectionInfo = connectionInfo;
			this.AppConfig = AppConfig.DefaultInstance;
		}

		// Token: 0x17000541 RID: 1345
		// (get) Token: 0x06001446 RID: 5190 RVA: 0x000348CF File Offset: 0x00032ACF
		public override DbConnection Connection
		{
			get
			{
				this.Initialize();
				return base.Connection;
			}
		}

		// Token: 0x17000542 RID: 1346
		// (get) Token: 0x06001447 RID: 5191 RVA: 0x000348DD File Offset: 0x00032ADD
		public override DbConnectionStringOrigin ConnectionStringOrigin
		{
			get
			{
				this.Initialize();
				return this._connectionStringOrigin;
			}
		}

		// Token: 0x17000543 RID: 1347
		// (get) Token: 0x06001448 RID: 5192 RVA: 0x000348EB File Offset: 0x00032AEB
		public override string ConnectionStringName
		{
			get
			{
				this.Initialize();
				return this._connectionStringName;
			}
		}

		// Token: 0x17000544 RID: 1348
		// (get) Token: 0x06001449 RID: 5193 RVA: 0x000348F9 File Offset: 0x00032AF9
		public override string ConnectionKey
		{
			get
			{
				this.Initialize();
				return base.ConnectionKey;
			}
		}

		// Token: 0x17000545 RID: 1349
		// (get) Token: 0x0600144A RID: 5194 RVA: 0x00034907 File Offset: 0x00032B07
		public override string OriginalConnectionString
		{
			get
			{
				this.Initialize();
				return base.OriginalConnectionString;
			}
		}

		// Token: 0x17000546 RID: 1350
		// (get) Token: 0x0600144B RID: 5195 RVA: 0x00034915 File Offset: 0x00032B15
		// (set) Token: 0x0600144C RID: 5196 RVA: 0x00034923 File Offset: 0x00032B23
		public override string ProviderName
		{
			get
			{
				this.Initialize();
				return base.ProviderName;
			}
			set
			{
				base.ProviderName = value;
			}
		}

		// Token: 0x17000547 RID: 1351
		// (get) Token: 0x0600144D RID: 5197 RVA: 0x0003492C File Offset: 0x00032B2C
		public override bool ConnectionHasModel
		{
			get
			{
				if (this._hasModel == null)
				{
					if (base.UnderlyingConnection == null)
					{
						string text = this._nameOrConnectionString;
						string text2;
						if (this._connectionInfo != null)
						{
							text = this._connectionInfo.GetConnectionString(this.AppConfig).ConnectionString;
						}
						else if (DbHelpers.TryGetConnectionName(this._nameOrConnectionString, out text2))
						{
							ConnectionStringSettings connectionStringSettings = LazyInternalConnection.FindConnectionInConfig(text2, this.AppConfig);
							if (connectionStringSettings == null && DbHelpers.TreatAsConnectionString(this._nameOrConnectionString))
							{
								throw Error.DbContext_ConnectionStringNotFound(text2);
							}
							if (connectionStringSettings != null)
							{
								text = connectionStringSettings.ConnectionString;
							}
						}
						this._hasModel = new bool?(DbHelpers.IsFullEFConnectionString(text));
					}
					else
					{
						this._hasModel = new bool?(base.UnderlyingConnection is EntityConnection);
					}
				}
				return this._hasModel.Value;
			}
		}

		// Token: 0x0600144E RID: 5198 RVA: 0x000349ED File Offset: 0x00032BED
		public override ObjectContext CreateObjectContextFromConnectionModel()
		{
			this.Initialize();
			return base.CreateObjectContextFromConnectionModel();
		}

		// Token: 0x0600144F RID: 5199 RVA: 0x000349FC File Offset: 0x00032BFC
		public override void Dispose()
		{
			if (base.UnderlyingConnection != null)
			{
				if (base.UnderlyingConnection is EntityConnection)
				{
					base.UnderlyingConnection.Dispose();
				}
				else
				{
					DbInterception.Dispatch.Connection.Dispose(base.UnderlyingConnection, base.InterceptionContext);
				}
				base.UnderlyingConnection = null;
			}
		}

		// Token: 0x17000548 RID: 1352
		// (get) Token: 0x06001450 RID: 5200 RVA: 0x00034A4D File Offset: 0x00032C4D
		internal bool IsInitialized
		{
			get
			{
				return base.UnderlyingConnection != null;
			}
		}

		// Token: 0x06001451 RID: 5201 RVA: 0x00034A58 File Offset: 0x00032C58
		private void Initialize()
		{
			if (base.UnderlyingConnection == null)
			{
				string text;
				if (this._connectionInfo != null)
				{
					ConnectionStringSettings connectionString = this._connectionInfo.GetConnectionString(this.AppConfig);
					this.InitializeFromConnectionStringSetting(connectionString);
					this._connectionStringOrigin = DbConnectionStringOrigin.DbContextInfo;
					this._connectionStringName = connectionString.Name;
				}
				else if (!DbHelpers.TryGetConnectionName(this._nameOrConnectionString, out text) || !this.TryInitializeFromAppConfig(text, this.AppConfig))
				{
					if (text != null && DbHelpers.TreatAsConnectionString(this._nameOrConnectionString))
					{
						throw Error.DbContext_ConnectionStringNotFound(text);
					}
					if (DbHelpers.IsFullEFConnectionString(this._nameOrConnectionString))
					{
						base.UnderlyingConnection = new EntityConnection(this._nameOrConnectionString);
					}
					else if (base.ProviderName != null)
					{
						this.CreateConnectionFromProviderName(base.ProviderName);
					}
					else
					{
						base.UnderlyingConnection = DbConfiguration.DependencyResolver.GetService<IDbConnectionFactory>().CreateConnection(text ?? this._nameOrConnectionString);
						if (base.UnderlyingConnection == null)
						{
							throw Error.DbContext_ConnectionFactoryReturnedNullConnection();
						}
					}
					if (text != null)
					{
						this._connectionStringOrigin = DbConnectionStringOrigin.Convention;
						this._connectionStringName = text;
					}
					else
					{
						this._connectionStringOrigin = DbConnectionStringOrigin.UserCode;
					}
				}
				base.OnConnectionInitialized();
			}
		}

		// Token: 0x06001452 RID: 5202 RVA: 0x00034B68 File Offset: 0x00032D68
		private bool TryInitializeFromAppConfig(string name, AppConfig config)
		{
			ConnectionStringSettings connectionStringSettings = LazyInternalConnection.FindConnectionInConfig(name, config);
			if (connectionStringSettings != null)
			{
				this.InitializeFromConnectionStringSetting(connectionStringSettings);
				this._connectionStringOrigin = DbConnectionStringOrigin.Configuration;
				this._connectionStringName = connectionStringSettings.Name;
				return true;
			}
			return false;
		}

		// Token: 0x06001453 RID: 5203 RVA: 0x00034BA0 File Offset: 0x00032DA0
		private static ConnectionStringSettings FindConnectionInConfig(string name, AppConfig config)
		{
			List<string> list = new List<string> { name };
			int num = name.LastIndexOf('.');
			if (num >= 0 && num + 1 < name.Length)
			{
				list.Add(name.Substring(num + 1));
			}
			return (from c in list
				where config.GetConnectionString(c) != null
				select config.GetConnectionString(c)).FirstOrDefault<ConnectionStringSettings>();
		}

		// Token: 0x06001454 RID: 5204 RVA: 0x00034C18 File Offset: 0x00032E18
		private void InitializeFromConnectionStringSetting(ConnectionStringSettings appConfigConnection)
		{
			string providerName = appConfigConnection.ProviderName;
			if (string.IsNullOrWhiteSpace(providerName))
			{
				throw Error.DbContext_ProviderNameMissing(appConfigConnection.Name);
			}
			if (string.Equals(providerName, "System.Data.EntityClient", StringComparison.OrdinalIgnoreCase))
			{
				base.UnderlyingConnection = new EntityConnection(appConfigConnection.ConnectionString);
				return;
			}
			this.CreateConnectionFromProviderName(providerName);
			DbInterception.Dispatch.Connection.SetConnectionString(base.UnderlyingConnection, new DbConnectionPropertyInterceptionContext<string>().WithValue(appConfigConnection.ConnectionString));
		}

		// Token: 0x06001455 RID: 5205 RVA: 0x00034C8C File Offset: 0x00032E8C
		private void CreateConnectionFromProviderName(string providerInvariantName)
		{
			DbProviderFactory service = DbConfiguration.DependencyResolver.GetService(providerInvariantName);
			base.UnderlyingConnection = service.CreateConnection();
			if (base.UnderlyingConnection == null)
			{
				throw Error.DbContext_ProviderReturnedNullConnection();
			}
		}

		// Token: 0x04000988 RID: 2440
		private readonly string _nameOrConnectionString;

		// Token: 0x04000989 RID: 2441
		private DbConnectionStringOrigin _connectionStringOrigin;

		// Token: 0x0400098A RID: 2442
		private string _connectionStringName;

		// Token: 0x0400098B RID: 2443
		private readonly DbConnectionInfo _connectionInfo;

		// Token: 0x0400098C RID: 2444
		private bool? _hasModel;
	}
}
