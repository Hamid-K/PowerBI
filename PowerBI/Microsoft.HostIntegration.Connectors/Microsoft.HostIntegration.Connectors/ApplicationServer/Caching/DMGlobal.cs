using System;
using System.Globalization;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000212 RID: 530
	internal static class DMGlobal
	{
		// Token: 0x170003C0 RID: 960
		// (get) Token: 0x0600112B RID: 4395 RVA: 0x000383C6 File Offset: 0x000365C6
		public static object TempObject
		{
			get
			{
				return DMGlobal.DMTempObject;
			}
		}

		// Token: 0x0600112C RID: 4396 RVA: 0x000383CD File Offset: 0x000365CD
		internal static DataCacheException GetException(int errorCode)
		{
			return new DataCacheException("DataManager", errorCode, GlobalResourceLoader.GetString(CultureInfo.CurrentUICulture, errorCode));
		}

		// Token: 0x04000AEC RID: 2796
		private static readonly object DMTempObject = new object();

		// Token: 0x04000AED RID: 2797
		internal static DMSignalHandleContainer ArraySignalHandle = new DMSignalHandleContainer(ConfigManager.NumberOfSignalHandlerContainers);
	}
}
