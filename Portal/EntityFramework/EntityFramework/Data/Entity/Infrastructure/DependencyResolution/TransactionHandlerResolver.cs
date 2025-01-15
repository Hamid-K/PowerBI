using System;
using System.Collections.Generic;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Infrastructure.DependencyResolution
{
	// Token: 0x020002BF RID: 703
	public class TransactionHandlerResolver : IDbDependencyResolver
	{
		// Token: 0x06002212 RID: 8722 RVA: 0x0005FCD8 File Offset: 0x0005DED8
		public TransactionHandlerResolver(Func<TransactionHandler> transactionHandlerFactory, string providerInvariantName, string serverName)
		{
			Check.NotNull<Func<TransactionHandler>>(transactionHandlerFactory, "transactionHandlerFactory");
			this._providerInvariantName = providerInvariantName;
			this._serverName = serverName;
			this._transactionHandlerFactory = transactionHandlerFactory;
		}

		// Token: 0x06002213 RID: 8723 RVA: 0x0005FD04 File Offset: 0x0005DF04
		public object GetService(Type type, object key)
		{
			if (!(type == typeof(Func<TransactionHandler>)))
			{
				return null;
			}
			ExecutionStrategyKey executionStrategyKey = key as ExecutionStrategyKey;
			if (executionStrategyKey == null)
			{
				throw new ArgumentException(Strings.DbDependencyResolver_InvalidKey(typeof(ExecutionStrategyKey).Name, "Func<TransactionHandler>"));
			}
			if (this._providerInvariantName != null && !executionStrategyKey.ProviderInvariantName.Equals(this._providerInvariantName, StringComparison.Ordinal))
			{
				return null;
			}
			if (this._serverName != null && !this._serverName.Equals(executionStrategyKey.ServerName, StringComparison.Ordinal))
			{
				return null;
			}
			return this._transactionHandlerFactory;
		}

		// Token: 0x06002214 RID: 8724 RVA: 0x0005FD90 File Offset: 0x0005DF90
		public IEnumerable<object> GetServices(Type type, object key)
		{
			return this.GetServiceAsServices(type, key);
		}

		// Token: 0x06002215 RID: 8725 RVA: 0x0005FD9C File Offset: 0x0005DF9C
		public override bool Equals(object obj)
		{
			TransactionHandlerResolver transactionHandlerResolver = obj as TransactionHandlerResolver;
			return transactionHandlerResolver != null && (this._transactionHandlerFactory == transactionHandlerResolver._transactionHandlerFactory && this._providerInvariantName == transactionHandlerResolver._providerInvariantName) && this._serverName == transactionHandlerResolver._serverName;
		}

		// Token: 0x06002216 RID: 8726 RVA: 0x0005FDF0 File Offset: 0x0005DFF0
		public override int GetHashCode()
		{
			int num = this._transactionHandlerFactory.GetHashCode();
			if (this._providerInvariantName != null)
			{
				num = num * 41 + this._providerInvariantName.GetHashCode();
			}
			if (this._serverName != null)
			{
				num = num * 41 + this._serverName.GetHashCode();
			}
			return num;
		}

		// Token: 0x04000BD9 RID: 3033
		private readonly Func<TransactionHandler> _transactionHandlerFactory;

		// Token: 0x04000BDA RID: 3034
		private readonly string _providerInvariantName;

		// Token: 0x04000BDB RID: 3035
		private readonly string _serverName;
	}
}
