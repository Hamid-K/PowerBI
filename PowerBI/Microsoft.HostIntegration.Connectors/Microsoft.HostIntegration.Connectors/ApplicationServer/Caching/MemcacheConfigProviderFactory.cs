using System;
using Microsoft.Fabric.Common;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000126 RID: 294
	internal class MemcacheConfigProviderFactory
	{
		// Token: 0x06000874 RID: 2164 RVA: 0x0001E84A File Offset: 0x0001CA4A
		internal static IMemcacheShimConfigProvider GetProvider(bool isCloudProvider, object param)
		{
			if (isCloudProvider)
			{
				return MemcacheConfigProviderFactory.CreateInstance<IMemcacheShimConfigProvider>("Microsoft.ApplicationServer.Caching.AzureServerCommon.AzureMemcacheProvider, Microsoft.ApplicationServer.Caching.AzureServerCommon", param);
			}
			return MemcacheConfigProviderFactory.CreateInstance<IMemcacheShimConfigProvider>("Microsoft.ApplicationServer.Caching.MemcacheClientShim.MemcacheConfigProvider, Microsoft.ApplicationServer.Caching.MemcacheShim");
		}

		// Token: 0x06000875 RID: 2165 RVA: 0x0001E868 File Offset: 0x0001CA68
		private static T CreateInstance<T>(string typeStr)
		{
			T t;
			try
			{
				Type type = Type.GetType(typeStr, true);
				t = (T)((object)Activator.CreateInstance(type));
			}
			catch (Exception ex)
			{
				EventLogWriter.WriteError("MemcacheConfigProviderFactory", "Error creating instance of = {0}, Exception= {1}", new object[] { typeStr, ex });
				throw;
			}
			return t;
		}

		// Token: 0x06000876 RID: 2166 RVA: 0x0001E8C0 File Offset: 0x0001CAC0
		private static T CreateInstance<T>(string typeStr, object param)
		{
			T t;
			try
			{
				Type type = Type.GetType(typeStr, true);
				t = (T)((object)Activator.CreateInstance(type, new object[] { param }));
			}
			catch (Exception ex)
			{
				EventLogWriter.WriteError("MemcacheConfigProviderFactory", "Error creating instance of = {0}, Exception= {1}", new object[] { typeStr, ex });
				throw;
			}
			return t;
		}

		// Token: 0x0400067F RID: 1663
		internal const string AzureProviderType = "Microsoft.ApplicationServer.Caching.AzureServerCommon.AzureMemcacheProvider, Microsoft.ApplicationServer.Caching.AzureServerCommon";

		// Token: 0x04000680 RID: 1664
		internal const string OnPremiseProviderType = "Microsoft.ApplicationServer.Caching.MemcacheClientShim.MemcacheConfigProvider, Microsoft.ApplicationServer.Caching.MemcacheShim";

		// Token: 0x04000681 RID: 1665
		internal const string LogSource = "MemcacheConfigProviderFactory";
	}
}
