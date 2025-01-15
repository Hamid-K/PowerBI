using System;
using System.Collections.Generic;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Infrastructure.DependencyResolution
{
	// Token: 0x020002B4 RID: 692
	public class ExecutionStrategyResolver<T> : IDbDependencyResolver where T : IDbExecutionStrategy
	{
		// Token: 0x060021D1 RID: 8657 RVA: 0x0005F145 File Offset: 0x0005D345
		public ExecutionStrategyResolver(string providerInvariantName, string serverName, Func<T> getExecutionStrategy)
		{
			Check.NotEmpty(providerInvariantName, "providerInvariantName");
			Check.NotNull<Func<T>>(getExecutionStrategy, "getExecutionStrategy");
			this._providerInvariantName = providerInvariantName;
			this._serverName = serverName;
			this._getExecutionStrategy = getExecutionStrategy;
		}

		// Token: 0x060021D2 RID: 8658 RVA: 0x0005F17C File Offset: 0x0005D37C
		public object GetService(Type type, object key)
		{
			if (!(type == typeof(Func<IDbExecutionStrategy>)))
			{
				return null;
			}
			ExecutionStrategyKey executionStrategyKey = key as ExecutionStrategyKey;
			if (executionStrategyKey == null)
			{
				throw new ArgumentException(Strings.DbDependencyResolver_InvalidKey(typeof(ExecutionStrategyKey).Name, "Func<IExecutionStrategy>"));
			}
			if (!executionStrategyKey.ProviderInvariantName.Equals(this._providerInvariantName, StringComparison.Ordinal))
			{
				return null;
			}
			if (this._serverName != null && !this._serverName.Equals(executionStrategyKey.ServerName, StringComparison.Ordinal))
			{
				return null;
			}
			return this._getExecutionStrategy;
		}

		// Token: 0x060021D3 RID: 8659 RVA: 0x0005F200 File Offset: 0x0005D400
		public IEnumerable<object> GetServices(Type type, object key)
		{
			return this.GetServiceAsServices(type, key);
		}

		// Token: 0x04000BC4 RID: 3012
		private readonly Func<T> _getExecutionStrategy;

		// Token: 0x04000BC5 RID: 3013
		private readonly string _providerInvariantName;

		// Token: 0x04000BC6 RID: 3014
		private readonly string _serverName;
	}
}
