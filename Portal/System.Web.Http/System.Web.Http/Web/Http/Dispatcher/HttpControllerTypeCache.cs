using System;
using System.Collections.Generic;
using System.Linq;

namespace System.Web.Http.Dispatcher
{
	// Token: 0x02000081 RID: 129
	internal sealed class HttpControllerTypeCache
	{
		// Token: 0x06000337 RID: 823 RVA: 0x000095CB File Offset: 0x000077CB
		public HttpControllerTypeCache(HttpConfiguration configuration)
		{
			if (configuration == null)
			{
				throw Error.ArgumentNull("configuration");
			}
			this._configuration = configuration;
			this._cache = new Lazy<Dictionary<string, ILookup<string, Type>>>(new Func<Dictionary<string, ILookup<string, Type>>>(this.InitializeCache));
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x06000338 RID: 824 RVA: 0x000095FF File Offset: 0x000077FF
		internal Dictionary<string, ILookup<string, Type>> Cache
		{
			get
			{
				return this._cache.Value;
			}
		}

		// Token: 0x06000339 RID: 825 RVA: 0x0000960C File Offset: 0x0000780C
		public ICollection<Type> GetControllerTypes(string controllerName)
		{
			if (string.IsNullOrEmpty(controllerName))
			{
				throw Error.ArgumentNullOrEmpty("controllerName");
			}
			HashSet<Type> hashSet = new HashSet<Type>();
			ILookup<string, Type> lookup;
			if (this._cache.Value.TryGetValue(controllerName, out lookup))
			{
				foreach (IGrouping<string, Type> grouping in lookup)
				{
					hashSet.UnionWith(grouping);
				}
			}
			return hashSet;
		}

		// Token: 0x0600033A RID: 826 RVA: 0x00009684 File Offset: 0x00007884
		private Dictionary<string, ILookup<string, Type>> InitializeCache()
		{
			IAssembliesResolver assembliesResolver = this._configuration.Services.GetAssembliesResolver();
			return this._configuration.Services.GetHttpControllerTypeResolver().GetControllerTypes(assembliesResolver).GroupBy((Type t) => t.Name.Substring(0, t.Name.Length - DefaultHttpControllerSelector.ControllerSuffix.Length), StringComparer.OrdinalIgnoreCase)
				.ToDictionary((IGrouping<string, Type> g) => g.Key, (IGrouping<string, Type> g) => g.ToLookup((Type t) => t.Namespace ?? string.Empty, StringComparer.OrdinalIgnoreCase), StringComparer.OrdinalIgnoreCase);
		}

		// Token: 0x040000B2 RID: 178
		private readonly HttpConfiguration _configuration;

		// Token: 0x040000B3 RID: 179
		private readonly Lazy<Dictionary<string, ILookup<string, Type>>> _cache;
	}
}
