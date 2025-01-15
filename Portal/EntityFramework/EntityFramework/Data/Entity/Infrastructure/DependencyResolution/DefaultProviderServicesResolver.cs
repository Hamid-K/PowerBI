using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Resources;
using System.Linq;

namespace System.Data.Entity.Infrastructure.DependencyResolution
{
	// Token: 0x020002B3 RID: 691
	internal class DefaultProviderServicesResolver : IDbDependencyResolver
	{
		// Token: 0x060021CD RID: 8653 RVA: 0x0005F0CD File Offset: 0x0005D2CD
		public virtual object GetService(Type type, object key)
		{
			if (type == typeof(DbProviderServices))
			{
				throw new InvalidOperationException(Strings.EF6Providers_NoProviderFound(DefaultProviderServicesResolver.CheckKey(key)));
			}
			return null;
		}

		// Token: 0x060021CE RID: 8654 RVA: 0x0005F0F3 File Offset: 0x0005D2F3
		private static string CheckKey(object key)
		{
			string text = key as string;
			if (string.IsNullOrWhiteSpace(text))
			{
				throw new ArgumentException(Strings.DbDependencyResolver_NoProviderInvariantName(typeof(DbProviderServices).Name));
			}
			return text;
		}

		// Token: 0x060021CF RID: 8655 RVA: 0x0005F11D File Offset: 0x0005D31D
		public virtual IEnumerable<object> GetServices(Type type, object key)
		{
			if (type == typeof(DbProviderServices))
			{
				DefaultProviderServicesResolver.CheckKey(key);
			}
			return Enumerable.Empty<object>();
		}
	}
}
