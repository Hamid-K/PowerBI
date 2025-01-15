using System;
using System.Configuration;

namespace System.Data.Entity.Internal.ConfigFile
{
	// Token: 0x0200014D RID: 333
	internal class EntityFrameworkSection : ConfigurationSection
	{
		// Token: 0x17000594 RID: 1428
		// (get) Token: 0x06001593 RID: 5523 RVA: 0x00038429 File Offset: 0x00036629
		// (set) Token: 0x06001594 RID: 5524 RVA: 0x0003843B File Offset: 0x0003663B
		[ConfigurationProperty("defaultConnectionFactory")]
		public virtual DefaultConnectionFactoryElement DefaultConnectionFactory
		{
			get
			{
				return (DefaultConnectionFactoryElement)base["defaultConnectionFactory"];
			}
			set
			{
				base["defaultConnectionFactory"] = value;
			}
		}

		// Token: 0x17000595 RID: 1429
		// (get) Token: 0x06001595 RID: 5525 RVA: 0x00038449 File Offset: 0x00036649
		// (set) Token: 0x06001596 RID: 5526 RVA: 0x0003845B File Offset: 0x0003665B
		[ConfigurationProperty("codeConfigurationType")]
		public virtual string ConfigurationTypeName
		{
			get
			{
				return (string)base["codeConfigurationType"];
			}
			set
			{
				base["codeConfigurationType"] = value;
			}
		}

		// Token: 0x17000596 RID: 1430
		// (get) Token: 0x06001597 RID: 5527 RVA: 0x00038469 File Offset: 0x00036669
		[ConfigurationProperty("providers")]
		public virtual ProviderCollection Providers
		{
			get
			{
				return (ProviderCollection)base["providers"];
			}
		}

		// Token: 0x17000597 RID: 1431
		// (get) Token: 0x06001598 RID: 5528 RVA: 0x0003847B File Offset: 0x0003667B
		[ConfigurationProperty("contexts")]
		public virtual ContextCollection Contexts
		{
			get
			{
				return (ContextCollection)base["contexts"];
			}
		}

		// Token: 0x17000598 RID: 1432
		// (get) Token: 0x06001599 RID: 5529 RVA: 0x0003848D File Offset: 0x0003668D
		[ConfigurationProperty("interceptors")]
		public virtual InterceptorsCollection Interceptors
		{
			get
			{
				return (InterceptorsCollection)base["interceptors"];
			}
		}

		// Token: 0x17000599 RID: 1433
		// (get) Token: 0x0600159A RID: 5530 RVA: 0x0003849F File Offset: 0x0003669F
		// (set) Token: 0x0600159B RID: 5531 RVA: 0x000384B1 File Offset: 0x000366B1
		[ConfigurationProperty("queryCache")]
		public virtual QueryCacheElement QueryCache
		{
			get
			{
				return (QueryCacheElement)base["queryCache"];
			}
			set
			{
				base["queryCache"] = value;
			}
		}

		// Token: 0x040009DE RID: 2526
		private const string DefaultConnectionFactoryKey = "defaultConnectionFactory";

		// Token: 0x040009DF RID: 2527
		private const string ContextsKey = "contexts";

		// Token: 0x040009E0 RID: 2528
		private const string ProviderKey = "providers";

		// Token: 0x040009E1 RID: 2529
		private const string ConfigurationTypeKey = "codeConfigurationType";

		// Token: 0x040009E2 RID: 2530
		private const string InterceptorsKey = "interceptors";

		// Token: 0x040009E3 RID: 2531
		private const string QueryCacheKey = "queryCache";
	}
}
