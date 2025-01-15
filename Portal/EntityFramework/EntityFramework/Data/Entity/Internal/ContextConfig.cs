using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.Entity.Internal.ConfigFile;
using System.Data.Entity.Resources;
using System.Linq;

namespace System.Data.Entity.Internal
{
	// Token: 0x020000F0 RID: 240
	internal class ContextConfig
	{
		// Token: 0x06001212 RID: 4626 RVA: 0x0002EE0B File Offset: 0x0002D00B
		public ContextConfig()
		{
		}

		// Token: 0x06001213 RID: 4627 RVA: 0x0002EE1E File Offset: 0x0002D01E
		public ContextConfig(EntityFrameworkSection entityFrameworkSettings)
		{
			this._entityFrameworkSettings = entityFrameworkSettings;
		}

		// Token: 0x06001214 RID: 4628 RVA: 0x0002EE38 File Offset: 0x0002D038
		public virtual int? TryGetCommandTimeout(Type contextType)
		{
			Func<ContextElement, int?> <>9__2;
			return this._commandTimeouts.GetOrAdd(contextType, delegate(Type requiredContextType)
			{
				IEnumerable<ContextElement> enumerable = from e in this._entityFrameworkSettings.Contexts.OfType<ContextElement>()
					where e.CommandTimeout != null
					select e;
				Func<ContextElement, int?> func;
				if ((func = <>9__2) == null)
				{
					func = (<>9__2 = (ContextElement e) => ContextConfig.TryGetCommandTimeout(contextType, e.ContextTypeName, e.CommandTimeout.Value));
				}
				return enumerable.Select(func).FirstOrDefault((int? i) => i != null);
			});
		}

		// Token: 0x06001215 RID: 4629 RVA: 0x0002EE78 File Offset: 0x0002D078
		private static int? TryGetCommandTimeout(Type requiredContextType, string contextTypeName, int commandTimeout)
		{
			try
			{
				if (Type.GetType(contextTypeName, true) == requiredContextType)
				{
					return new int?(commandTimeout);
				}
			}
			catch (Exception ex)
			{
				throw new InvalidOperationException(Strings.Database_InitializationException, ex);
			}
			return null;
		}

		// Token: 0x04000902 RID: 2306
		private readonly EntityFrameworkSection _entityFrameworkSettings;

		// Token: 0x04000903 RID: 2307
		private readonly ConcurrentDictionary<Type, int?> _commandTimeouts = new ConcurrentDictionary<Type, int?>();
	}
}
