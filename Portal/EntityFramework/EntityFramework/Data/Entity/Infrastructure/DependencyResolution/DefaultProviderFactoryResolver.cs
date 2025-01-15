using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity.Resources;
using System.Data.SqlClient;
using System.Linq;

namespace System.Data.Entity.Infrastructure.DependencyResolution
{
	// Token: 0x020002B2 RID: 690
	internal class DefaultProviderFactoryResolver : IDbDependencyResolver
	{
		// Token: 0x060021C9 RID: 8649 RVA: 0x0005EFCA File Offset: 0x0005D1CA
		public virtual object GetService(Type type, object key)
		{
			return DefaultProviderFactoryResolver.GetService(type, key, delegate(ArgumentException e, string n)
			{
				throw new ArgumentException(Strings.EntityClient_InvalidStoreProvider(n), e);
			});
		}

		// Token: 0x060021CA RID: 8650 RVA: 0x0005EFF4 File Offset: 0x0005D1F4
		private static object GetService(Type type, object key, Func<ArgumentException, string, object> handleFailedLookup)
		{
			if (type == typeof(DbProviderFactory))
			{
				string text = key as string;
				if (string.IsNullOrWhiteSpace(text))
				{
					throw new ArgumentException(Strings.DbDependencyResolver_NoProviderInvariantName(typeof(DbProviderFactory).Name));
				}
				try
				{
					return DbProviderFactories.GetFactory(text);
				}
				catch (ArgumentException ex)
				{
					if (string.Equals(text, "System.Data.SqlClient", StringComparison.OrdinalIgnoreCase))
					{
						return SqlClientFactory.Instance;
					}
					return handleFailedLookup(ex, text);
				}
			}
			return null;
		}

		// Token: 0x060021CB RID: 8651 RVA: 0x0005F07C File Offset: 0x0005D27C
		public IEnumerable<object> GetServices(Type type, object key)
		{
			object service = DefaultProviderFactoryResolver.GetService(type, key, (ArgumentException e, string n) => null);
			if (service != null)
			{
				return new object[] { service };
			}
			return Enumerable.Empty<object>();
		}
	}
}
