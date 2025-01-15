using System;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Reflection;

namespace System.Data.Entity.Infrastructure.DependencyResolution
{
	// Token: 0x020002BA RID: 698
	internal class ProviderServicesFactory
	{
		// Token: 0x060021F9 RID: 8697 RVA: 0x0005F6F8 File Offset: 0x0005D8F8
		public virtual DbProviderServices TryGetInstance(string providerTypeName)
		{
			Type type = Type.GetType(providerTypeName, false);
			if (!(type == null))
			{
				return ProviderServicesFactory.GetInstance(type);
			}
			return null;
		}

		// Token: 0x060021FA RID: 8698 RVA: 0x0005F71E File Offset: 0x0005D91E
		public virtual DbProviderServices GetInstance(string providerTypeName, string providerInvariantName)
		{
			Type type = Type.GetType(providerTypeName, false);
			if (type == null)
			{
				throw new InvalidOperationException(Strings.EF6Providers_ProviderTypeMissing(providerTypeName, providerInvariantName));
			}
			return ProviderServicesFactory.GetInstance(type);
		}

		// Token: 0x060021FB RID: 8699 RVA: 0x0005F744 File Offset: 0x0005D944
		private static DbProviderServices GetInstance(Type providerType)
		{
			PropertyInfo propertyInfo = providerType.GetStaticProperty("Instance") ?? providerType.GetField("Instance", BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
			if (propertyInfo == null)
			{
				throw new InvalidOperationException(Strings.EF6Providers_InstanceMissing(providerType.AssemblyQualifiedName));
			}
			DbProviderServices dbProviderServices = propertyInfo.GetValue() as DbProviderServices;
			if (dbProviderServices == null)
			{
				throw new InvalidOperationException(Strings.EF6Providers_NotDbProviderServices(providerType.AssemblyQualifiedName));
			}
			return dbProviderServices;
		}
	}
}
