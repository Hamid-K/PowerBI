using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity.Resources;

namespace System.Data.Entity.Infrastructure.DependencyResolution
{
	// Token: 0x020002B8 RID: 696
	internal class InvariantNameResolver : IDbDependencyResolver
	{
		// Token: 0x060021F1 RID: 8689 RVA: 0x0005F5E3 File Offset: 0x0005D7E3
		public InvariantNameResolver(DbProviderFactory providerFactory, string invariantName)
		{
			this._invariantName = new ProviderInvariantName(invariantName);
			this._providerFactoryType = providerFactory.GetType();
		}

		// Token: 0x060021F2 RID: 8690 RVA: 0x0005F604 File Offset: 0x0005D804
		public virtual object GetService(Type type, object key)
		{
			if (type == typeof(IProviderInvariantName))
			{
				if (!(key is DbProviderFactory))
				{
					throw new ArgumentException(Strings.DbDependencyResolver_InvalidKey(typeof(DbProviderFactory).Name, typeof(IProviderInvariantName)));
				}
				if (key.GetType() == this._providerFactoryType)
				{
					return this._invariantName;
				}
			}
			return null;
		}

		// Token: 0x060021F3 RID: 8691 RVA: 0x0005F66C File Offset: 0x0005D86C
		public override bool Equals(object obj)
		{
			InvariantNameResolver invariantNameResolver = obj as InvariantNameResolver;
			return invariantNameResolver != null && this._providerFactoryType == invariantNameResolver._providerFactoryType && this._invariantName.Name == invariantNameResolver._invariantName.Name;
		}

		// Token: 0x060021F4 RID: 8692 RVA: 0x0005F6B5 File Offset: 0x0005D8B5
		public override int GetHashCode()
		{
			return this._invariantName.Name.GetHashCode();
		}

		// Token: 0x060021F5 RID: 8693 RVA: 0x0005F6C7 File Offset: 0x0005D8C7
		public IEnumerable<object> GetServices(Type type, object key)
		{
			return this.GetServiceAsServices(type, key);
		}

		// Token: 0x04000BCC RID: 3020
		private readonly IProviderInvariantName _invariantName;

		// Token: 0x04000BCD RID: 3021
		private readonly Type _providerFactoryType;
	}
}
