using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity.Core.EntityClient;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Linq;

namespace System.Data.Entity.Infrastructure
{
	// Token: 0x02000258 RID: 600
	internal class Net40DefaultDbProviderFactoryResolver : IDbProviderFactoryResolver
	{
		// Token: 0x06001ECB RID: 7883 RVA: 0x00055A37 File Offset: 0x00053C37
		public Net40DefaultDbProviderFactoryResolver()
			: this(new ProviderRowFinder())
		{
		}

		// Token: 0x06001ECC RID: 7884 RVA: 0x00055A44 File Offset: 0x00053C44
		public Net40DefaultDbProviderFactoryResolver(ProviderRowFinder finder)
		{
			this._finder = finder;
		}

		// Token: 0x06001ECD RID: 7885 RVA: 0x00055A7F File Offset: 0x00053C7F
		public DbProviderFactory ResolveProviderFactory(DbConnection connection)
		{
			Check.NotNull<DbConnection>(connection, "connection");
			return this.GetProviderFactory(connection, DbProviderFactories.GetFactoryClasses().Rows.OfType<DataRow>());
		}

		// Token: 0x06001ECE RID: 7886 RVA: 0x00055AA4 File Offset: 0x00053CA4
		public DbProviderFactory GetProviderFactory(DbConnection connection, IEnumerable<DataRow> dataRows)
		{
			Type type = connection.GetType();
			return this._cache.GetOrAdd(type, delegate(Type t)
			{
				DataRow dataRow;
				if ((dataRow = this._finder.FindRow(t, (DataRow r) => Net40DefaultDbProviderFactoryResolver.ExactMatch(r, t), dataRows)) == null && (dataRow = this._finder.FindRow(null, (DataRow r) => Net40DefaultDbProviderFactoryResolver.ExactMatch(r, t), dataRows)) == null)
				{
					dataRow = this._finder.FindRow(t, (DataRow r) => Net40DefaultDbProviderFactoryResolver.AssignableMatch(r, t), dataRows) ?? this._finder.FindRow(null, (DataRow r) => Net40DefaultDbProviderFactoryResolver.AssignableMatch(r, t), dataRows);
				}
				DataRow dataRow2 = dataRow;
				if (dataRow2 == null)
				{
					throw new NotSupportedException(Strings.ProviderNotFound(connection.ToString()));
				}
				return DbProviderFactories.GetFactory(dataRow2);
			});
		}

		// Token: 0x06001ECF RID: 7887 RVA: 0x00055AF0 File Offset: 0x00053CF0
		private static bool ExactMatch(DataRow row, Type connectionType)
		{
			return DbProviderFactories.GetFactory(row).CreateConnection().GetType() == connectionType;
		}

		// Token: 0x06001ED0 RID: 7888 RVA: 0x00055B08 File Offset: 0x00053D08
		private static bool AssignableMatch(DataRow row, Type connectionType)
		{
			return connectionType.IsInstanceOfType(DbProviderFactories.GetFactory(row).CreateConnection());
		}

		// Token: 0x04000B37 RID: 2871
		private readonly ConcurrentDictionary<Type, DbProviderFactory> _cache = new ConcurrentDictionary<Type, DbProviderFactory>(new KeyValuePair<Type, DbProviderFactory>[]
		{
			new KeyValuePair<Type, DbProviderFactory>(typeof(EntityConnection), EntityProviderFactory.Instance)
		});

		// Token: 0x04000B38 RID: 2872
		private readonly ProviderRowFinder _finder;
	}
}
