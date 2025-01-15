using System;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library
{
	// Token: 0x0200024C RID: 588
	public static class EngineHostExtensions
	{
		// Token: 0x06001941 RID: 6465 RVA: 0x00031D04 File Offset: 0x0002FF04
		public static T Hook<T>(this IEngineHost host, Func<T> service) where T : class
		{
			IEngineHook engineHook = host.QueryService<IEngineHook>();
			if (engineHook != null)
			{
				return engineHook.Hook<T>(service);
			}
			return service();
		}

		// Token: 0x06001942 RID: 6466 RVA: 0x00031D29 File Offset: 0x0002FF29
		public static IPersistentCache GetPersistentCache(this IEngineHost host)
		{
			ICacheSets cacheSets = host.QueryService<ICacheSets>();
			if (cacheSets == null)
			{
				return null;
			}
			ICacheSet data = cacheSets.Data;
			if (data == null)
			{
				return null;
			}
			return data.PersistentCache;
		}

		// Token: 0x06001943 RID: 6467 RVA: 0x00031D47 File Offset: 0x0002FF47
		public static IPersistentCache GetMetadataCache(this IEngineHost host)
		{
			ICacheSets cacheSets = host.QueryService<ICacheSets>();
			if (cacheSets == null)
			{
				return null;
			}
			ICacheSet metadata = cacheSets.Metadata;
			if (metadata == null)
			{
				return null;
			}
			return metadata.PersistentCache;
		}

		// Token: 0x06001944 RID: 6468 RVA: 0x00031D65 File Offset: 0x0002FF65
		public static IClearableTransientCache GetTransientCache(this IEngineHost host)
		{
			return host.QueryService<IClearableTransientCache>();
		}

		// Token: 0x06001945 RID: 6469 RVA: 0x00031D6D File Offset: 0x0002FF6D
		public static bool ThrowOnVolatileFunctions(this IEngineHost engineHost)
		{
			IFoldingFailureService foldingFailureService = engineHost.QueryService<IFoldingFailureService>();
			return foldingFailureService != null && foldingFailureService.ThrowOnVolatileFunctions;
		}

		// Token: 0x06001946 RID: 6470 RVA: 0x00031D80 File Offset: 0x0002FF80
		public static void CheckVolatileFunctionsAllowed(this IEngineHost engineHost)
		{
			if (engineHost.ThrowOnVolatileFunctions())
			{
				throw ValueException.NewExpressionError<Message0>(Strings.VolatileFunctionsNotSupported, null, null);
			}
		}
	}
}
