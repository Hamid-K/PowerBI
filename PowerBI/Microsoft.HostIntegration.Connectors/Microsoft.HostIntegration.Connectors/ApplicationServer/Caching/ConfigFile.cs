using System;
using System.Globalization;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020000F8 RID: 248
	internal class ConfigFile
	{
		// Token: 0x060006CE RID: 1742 RVA: 0x0001AC3A File Offset: 0x00018E3A
		internal static void ThrowException(int errorCode)
		{
			throw new DataCacheException("CONFIGURATION_MANAGER", errorCode, GlobalResourceLoader.GetString(CultureInfo.CurrentUICulture, errorCode));
		}

		// Token: 0x060006CF RID: 1743 RVA: 0x0001AC52 File Offset: 0x00018E52
		internal static void ThrowException(int errorCode, string param)
		{
			throw new DataCacheException("CONFIGURATION_MANAGER", errorCode, GlobalResourceLoader.GetString(CultureInfo.CurrentUICulture, DataCacheException.GetErrorString(errorCode), param));
		}

		// Token: 0x060006D0 RID: 1744 RVA: 0x0001AC70 File Offset: 0x00018E70
		internal static void ThrowException(int errorCode, string param1, string param2, string param3)
		{
			throw new DataCacheException("CONFIGURATION_MANAGER", errorCode, GlobalResourceLoader.GetString(CultureInfo.CurrentUICulture, errorCode, param1, param2, param3));
		}

		// Token: 0x060006D1 RID: 1745 RVA: 0x0001AC8B File Offset: 0x00018E8B
		internal static void ThrowException(int errorCode, string param1, string param2, string param3, string param4)
		{
			throw new DataCacheException("CONFIGURATION_MANAGER", errorCode, GlobalResourceLoader.GetString(CultureInfo.CurrentUICulture, errorCode, param1, param2, param3, param4));
		}

		// Token: 0x060006D2 RID: 1746 RVA: 0x0001ACA8 File Offset: 0x00018EA8
		internal static void ThrowException(int errorCode, Exception e)
		{
			throw new DataCacheException("CONFIGURATION_MANAGER", errorCode, GlobalResourceLoader.GetString(CultureInfo.CurrentUICulture, errorCode), e, false);
		}

		// Token: 0x060006D3 RID: 1747 RVA: 0x0001ACC2 File Offset: 0x00018EC2
		internal static void ThrowException(Exception e)
		{
			ConfigFile.ThrowException(9001, -1, e);
		}

		// Token: 0x060006D4 RID: 1748 RVA: 0x0001ACD0 File Offset: 0x00018ED0
		internal static void ThrowException(int errorCode, int subStatus, Exception e)
		{
			throw new DataCacheException("CONFIGURATION_MANAGER", errorCode, subStatus, GlobalResourceLoader.GetString(CultureInfo.CurrentUICulture, errorCode, e.Message), e, false);
		}

		// Token: 0x04000450 RID: 1104
		internal const string VELOCITY_GLOBAL_CONFIG_NAME_XML = "ClusterConfig.xml";

		// Token: 0x04000451 RID: 1105
		internal const string USE_MANAGEDCACHE_EMULATOR = "useDevelopmentCaching=true";
	}
}
