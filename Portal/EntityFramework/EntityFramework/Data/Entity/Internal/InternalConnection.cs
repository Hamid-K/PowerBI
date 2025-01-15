using System;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Data.Entity.Core.EntityClient;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Infrastructure.Interception;
using System.Data.Entity.Utilities;
using System.Globalization;
using System.Linq;

namespace System.Data.Entity.Internal
{
	// Token: 0x0200011C RID: 284
	internal abstract class InternalConnection : IInternalConnection, IDisposable
	{
		// Token: 0x06001395 RID: 5013 RVA: 0x00032F41 File Offset: 0x00031141
		public InternalConnection(DbInterceptionContext interceptionContext)
		{
			this.InterceptionContext = interceptionContext ?? new DbInterceptionContext();
		}

		// Token: 0x1700050C RID: 1292
		// (get) Token: 0x06001396 RID: 5014 RVA: 0x00032F59 File Offset: 0x00031159
		// (set) Token: 0x06001397 RID: 5015 RVA: 0x00032F61 File Offset: 0x00031161
		private protected DbInterceptionContext InterceptionContext { protected get; private set; }

		// Token: 0x1700050D RID: 1293
		// (get) Token: 0x06001398 RID: 5016 RVA: 0x00032F6C File Offset: 0x0003116C
		public virtual DbConnection Connection
		{
			get
			{
				EntityConnection entityConnection = this.UnderlyingConnection as EntityConnection;
				if (entityConnection == null)
				{
					return this.UnderlyingConnection;
				}
				return entityConnection.StoreConnection;
			}
		}

		// Token: 0x1700050E RID: 1294
		// (get) Token: 0x06001399 RID: 5017 RVA: 0x00032F98 File Offset: 0x00031198
		public virtual string ConnectionKey
		{
			get
			{
				string text;
				if ((text = this._key) == null)
				{
					text = (this._key = string.Format(CultureInfo.InvariantCulture, "{0};{1}", new object[]
					{
						this.UnderlyingConnection.GetType(),
						this.OriginalConnectionString
					}));
				}
				return text;
			}
		}

		// Token: 0x1700050F RID: 1295
		// (get) Token: 0x0600139A RID: 5018 RVA: 0x00032FE4 File Offset: 0x000311E4
		public virtual bool ConnectionHasModel
		{
			get
			{
				return this.UnderlyingConnection is EntityConnection;
			}
		}

		// Token: 0x17000510 RID: 1296
		// (get) Token: 0x0600139B RID: 5019
		public abstract DbConnectionStringOrigin ConnectionStringOrigin { get; }

		// Token: 0x17000511 RID: 1297
		// (get) Token: 0x0600139C RID: 5020 RVA: 0x00032FF4 File Offset: 0x000311F4
		// (set) Token: 0x0600139D RID: 5021 RVA: 0x00032FFC File Offset: 0x000311FC
		public virtual AppConfig AppConfig { get; set; }

		// Token: 0x17000512 RID: 1298
		// (get) Token: 0x0600139E RID: 5022 RVA: 0x00033008 File Offset: 0x00031208
		// (set) Token: 0x0600139F RID: 5023 RVA: 0x0003303E File Offset: 0x0003123E
		public virtual string ProviderName
		{
			get
			{
				string text;
				if ((text = this._providerName) == null)
				{
					text = (this._providerName = ((this.UnderlyingConnection == null) ? null : this.Connection.GetProviderInvariantName()));
				}
				return text;
			}
			set
			{
				this._providerName = value;
			}
		}

		// Token: 0x17000513 RID: 1299
		// (get) Token: 0x060013A0 RID: 5024 RVA: 0x00033047 File Offset: 0x00031247
		public virtual string ConnectionStringName
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000514 RID: 1300
		// (get) Token: 0x060013A1 RID: 5025 RVA: 0x0003304C File Offset: 0x0003124C
		public virtual string OriginalConnectionString
		{
			get
			{
				string text = ((this.UnderlyingConnection is EntityConnection) ? this.UnderlyingConnection.Database : DbInterception.Dispatch.Connection.GetDatabase(this.UnderlyingConnection, this.InterceptionContext));
				string text2 = ((this.UnderlyingConnection is EntityConnection) ? this.UnderlyingConnection.DataSource : DbInterception.Dispatch.Connection.GetDataSource(this.UnderlyingConnection, this.InterceptionContext));
				if (!string.Equals(this._originalDatabaseName, text, StringComparison.OrdinalIgnoreCase) || !string.Equals(this._originalDataSource, text2, StringComparison.OrdinalIgnoreCase))
				{
					this.OnConnectionInitialized();
				}
				return this._originalConnectionString;
			}
		}

		// Token: 0x060013A2 RID: 5026 RVA: 0x000330F0 File Offset: 0x000312F0
		public virtual ObjectContext CreateObjectContextFromConnectionModel()
		{
			ObjectContext objectContext = new ObjectContext((EntityConnection)this.UnderlyingConnection);
			ReadOnlyCollection<EntityContainer> items = objectContext.MetadataWorkspace.GetItems<EntityContainer>(DataSpace.CSpace);
			if (items.Count == 1)
			{
				objectContext.DefaultContainerName = items.Single<EntityContainer>().Name;
			}
			return objectContext;
		}

		// Token: 0x060013A3 RID: 5027
		public abstract void Dispose();

		// Token: 0x17000515 RID: 1301
		// (get) Token: 0x060013A4 RID: 5028 RVA: 0x00033136 File Offset: 0x00031336
		// (set) Token: 0x060013A5 RID: 5029 RVA: 0x0003313E File Offset: 0x0003133E
		protected DbConnection UnderlyingConnection { get; set; }

		// Token: 0x060013A6 RID: 5030 RVA: 0x00033148 File Offset: 0x00031348
		protected void OnConnectionInitialized()
		{
			this._originalConnectionString = InternalConnection.GetStoreConnectionString(this.UnderlyingConnection);
			try
			{
				this._originalDatabaseName = ((this.UnderlyingConnection is EntityConnection) ? this.UnderlyingConnection.Database : DbInterception.Dispatch.Connection.GetDatabase(this.UnderlyingConnection, this.InterceptionContext));
			}
			catch (NotImplementedException)
			{
			}
			try
			{
				this._originalDataSource = ((this.UnderlyingConnection is EntityConnection) ? this.UnderlyingConnection.DataSource : DbInterception.Dispatch.Connection.GetDataSource(this.UnderlyingConnection, this.InterceptionContext));
			}
			catch (NotImplementedException)
			{
			}
		}

		// Token: 0x060013A7 RID: 5031 RVA: 0x00033204 File Offset: 0x00031404
		public static string GetStoreConnectionString(DbConnection connection)
		{
			EntityConnection entityConnection = connection as EntityConnection;
			string text;
			if (entityConnection != null)
			{
				connection = entityConnection.StoreConnection;
				text = ((connection != null) ? DbInterception.Dispatch.Connection.GetConnectionString(connection, new DbInterceptionContext()) : null);
			}
			else
			{
				text = DbInterception.Dispatch.Connection.GetConnectionString(connection, new DbInterceptionContext());
			}
			return text;
		}

		// Token: 0x04000958 RID: 2392
		private string _key;

		// Token: 0x04000959 RID: 2393
		private string _providerName;

		// Token: 0x0400095A RID: 2394
		private string _originalConnectionString;

		// Token: 0x0400095B RID: 2395
		private string _originalDatabaseName;

		// Token: 0x0400095C RID: 2396
		private string _originalDataSource;
	}
}
