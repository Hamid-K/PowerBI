using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure.Interception;
using System.Data.Entity.Internal.MockingProxies;
using System.Data.Entity.Utilities;
using System.Linq;
using System.Reflection;

namespace System.Data.Entity.Internal
{
	// Token: 0x020000ED RID: 237
	internal class ClonedObjectContext : IDisposable
	{
		// Token: 0x060011FD RID: 4605 RVA: 0x0002E9A4 File Offset: 0x0002CBA4
		protected ClonedObjectContext()
		{
		}

		// Token: 0x060011FE RID: 4606 RVA: 0x0002E9AC File Offset: 0x0002CBAC
		public ClonedObjectContext(ObjectContextProxy objectContext, DbConnection connection, string connectionString, bool transferLoadedAssemblies = true)
		{
			if (connection == null || connection.State != ConnectionState.Open)
			{
				connection = connection ?? objectContext.Connection.StoreConnection;
				connection = DbProviderServices.GetProviderServices(connection).CloneDbConnection(connection);
				DbInterception.Dispatch.Connection.SetConnectionString(connection, new DbConnectionPropertyInterceptionContext<string>().WithValue(connectionString));
				this._connectionCloned = true;
			}
			this._clonedEntityConnection = objectContext.Connection.CreateNew(connection);
			this._objectContext = objectContext.CreateNew(this._clonedEntityConnection);
			this._objectContext.CopyContextOptions(objectContext);
			if (!string.IsNullOrWhiteSpace(objectContext.DefaultContainerName))
			{
				this._objectContext.DefaultContainerName = objectContext.DefaultContainerName;
			}
			if (transferLoadedAssemblies)
			{
				this.TransferLoadedAssemblies(objectContext);
			}
		}

		// Token: 0x1700048E RID: 1166
		// (get) Token: 0x060011FF RID: 4607 RVA: 0x0002EA66 File Offset: 0x0002CC66
		public virtual ObjectContextProxy ObjectContext
		{
			get
			{
				return this._objectContext;
			}
		}

		// Token: 0x1700048F RID: 1167
		// (get) Token: 0x06001200 RID: 4608 RVA: 0x0002EA6E File Offset: 0x0002CC6E
		public virtual DbConnection Connection
		{
			get
			{
				return this._objectContext.Connection.StoreConnection;
			}
		}

		// Token: 0x06001201 RID: 4609 RVA: 0x0002EA80 File Offset: 0x0002CC80
		private void TransferLoadedAssemblies(ObjectContextProxy source)
		{
			IEnumerable<GlobalItem> objectItemCollection = source.GetObjectItemCollection();
			foreach (Assembly assembly in (from i in objectItemCollection
				where i is EntityType || i is ComplexType
				select source.GetClrType((StructuralType)i).Assembly()).Union(from i in objectItemCollection.OfType<EnumType>()
				select source.GetClrType(i).Assembly()).Distinct<Assembly>())
			{
				this._objectContext.LoadFromAssembly(assembly);
			}
		}

		// Token: 0x06001202 RID: 4610 RVA: 0x0002EB3C File Offset: 0x0002CD3C
		public void Dispose()
		{
			if (this._objectContext != null)
			{
				ObjectContextProxy objectContext = this._objectContext;
				DbConnection connection = this.Connection;
				this._objectContext = null;
				objectContext.Dispose();
				this._clonedEntityConnection.Dispose();
				if (this._connectionCloned)
				{
					DbInterception.Dispatch.Connection.Dispose(connection, new DbInterceptionContext());
				}
			}
		}

		// Token: 0x040008F7 RID: 2295
		private ObjectContextProxy _objectContext;

		// Token: 0x040008F8 RID: 2296
		private readonly bool _connectionCloned;

		// Token: 0x040008F9 RID: 2297
		private readonly EntityConnectionProxy _clonedEntityConnection;
	}
}
