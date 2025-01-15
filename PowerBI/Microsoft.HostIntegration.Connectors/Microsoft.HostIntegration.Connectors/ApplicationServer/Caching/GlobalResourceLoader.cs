using System;
using System.Globalization;
using System.Resources;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000322 RID: 802
	internal sealed class GlobalResourceLoader
	{
		// Token: 0x1700060C RID: 1548
		// (get) Token: 0x06001D11 RID: 7441 RVA: 0x0005833E File Offset: 0x0005653E
		private static ResourceManager Resource
		{
			get
			{
				GlobalResourceLoader.GetInstance();
				return GlobalResourceLoader._resourceManager;
			}
		}

		// Token: 0x06001D12 RID: 7442 RVA: 0x0005834B File Offset: 0x0005654B
		private static void ThrowInitializationException()
		{
			throw new DataCacheException("RESOURCE MANAGER", 7001, "Resource manager is not initialized", true);
		}

		// Token: 0x06001D13 RID: 7443 RVA: 0x00058362 File Offset: 0x00056562
		private GlobalResourceLoader()
		{
			GlobalResourceLoader._resourceManager = new ResourceManager(this._baseName, base.GetType().Module.Assembly);
		}

		// Token: 0x06001D14 RID: 7444 RVA: 0x00058398 File Offset: 0x00056598
		private static GlobalResourceLoader GetInstance()
		{
			object syncroot;
			if (GlobalResourceLoader._reader == null && (syncroot = GlobalResourceLoader._syncroot) != null)
			{
				lock (syncroot)
				{
					GlobalResourceLoader._reader = GlobalResourceLoader._reader ?? new GlobalResourceLoader();
					GlobalResourceLoader._syncroot = null;
				}
			}
			if (GlobalResourceLoader._resourceManager == null)
			{
				GlobalResourceLoader.ThrowInitializationException();
			}
			return GlobalResourceLoader._reader;
		}

		// Token: 0x06001D15 RID: 7445 RVA: 0x00058414 File Offset: 0x00056614
		public static string GetString(CultureInfo culture, string name, object arg0, object arg1, object arg2, object arg3)
		{
			culture = culture ?? CultureInfo.CurrentUICulture;
			try
			{
				return string.Format(CultureInfo.CurrentCulture, GlobalResourceLoader.Resource.GetString(name, culture), new object[] { arg0, arg1, arg2, arg3 });
			}
			catch (FormatException ex)
			{
				GlobalResourceLoader.ProcessException(ex);
			}
			return null;
		}

		// Token: 0x06001D16 RID: 7446 RVA: 0x0005847C File Offset: 0x0005667C
		public static string GetString(CultureInfo culture, string name, object arg0, object arg1, object arg2)
		{
			culture = culture ?? CultureInfo.CurrentUICulture;
			try
			{
				return string.Format(CultureInfo.CurrentCulture, GlobalResourceLoader.Resource.GetString(name, culture), new object[] { arg0, arg1, arg2 });
			}
			catch (FormatException ex)
			{
				GlobalResourceLoader.ProcessException(ex);
			}
			return null;
		}

		// Token: 0x06001D17 RID: 7447 RVA: 0x000584E0 File Offset: 0x000566E0
		public static string GetString(CultureInfo culture, string name, object arg0, object arg1)
		{
			culture = culture ?? CultureInfo.CurrentUICulture;
			try
			{
				if (GlobalResourceLoader.Resource != null)
				{
					return string.Format(CultureInfo.CurrentCulture, GlobalResourceLoader.Resource.GetString(name, culture), new object[] { arg0, arg1 });
				}
			}
			catch (FormatException ex)
			{
				GlobalResourceLoader.ProcessException(ex);
			}
			return null;
		}

		// Token: 0x06001D18 RID: 7448 RVA: 0x00058548 File Offset: 0x00056748
		public static string GetString(CultureInfo culture, string name, object arg)
		{
			culture = culture ?? CultureInfo.CurrentUICulture;
			try
			{
				if (GlobalResourceLoader.Resource != null)
				{
					return string.Format(CultureInfo.CurrentCulture, GlobalResourceLoader.Resource.GetString(name, culture), new object[] { arg });
				}
			}
			catch (FormatException ex)
			{
				GlobalResourceLoader.ProcessException(ex);
			}
			return null;
		}

		// Token: 0x06001D19 RID: 7449 RVA: 0x000585AC File Offset: 0x000567AC
		public static string GetString(CultureInfo culture, string name)
		{
			if (GlobalResourceLoader.Resource != null)
			{
				return GlobalResourceLoader.Resource.GetString(name, culture);
			}
			return null;
		}

		// Token: 0x06001D1A RID: 7450 RVA: 0x000585C3 File Offset: 0x000567C3
		public static string GetString(CultureInfo culture, int errorcode, object arg0, object arg1, object arg2, object arg3)
		{
			return GlobalResourceLoader.GetString(culture, DataCacheException.GetErrorString(errorcode), arg0, arg1, arg2, arg3);
		}

		// Token: 0x06001D1B RID: 7451 RVA: 0x000585D7 File Offset: 0x000567D7
		public static string GetString(CultureInfo culture, int errorcode, object arg0, object arg1, object arg2)
		{
			return GlobalResourceLoader.GetString(culture, DataCacheException.GetErrorString(errorcode), arg0, arg1, arg2);
		}

		// Token: 0x06001D1C RID: 7452 RVA: 0x000585E9 File Offset: 0x000567E9
		public static string GetString(CultureInfo culture, int errorcode, object arg0, object arg1)
		{
			return GlobalResourceLoader.GetString(culture, DataCacheException.GetErrorString(errorcode), arg0, arg1);
		}

		// Token: 0x06001D1D RID: 7453 RVA: 0x000585F9 File Offset: 0x000567F9
		public static string GetString(CultureInfo culture, int errorcode, object arg)
		{
			return GlobalResourceLoader.GetString(culture, DataCacheException.GetErrorString(errorcode), arg);
		}

		// Token: 0x06001D1E RID: 7454 RVA: 0x00058608 File Offset: 0x00056808
		public static string GetString(CultureInfo culture, int errorcode)
		{
			return GlobalResourceLoader.GetString(culture, DataCacheException.GetErrorString(errorcode));
		}

		// Token: 0x06001D1F RID: 7455 RVA: 0x00058616 File Offset: 0x00056816
		private static void ProcessException(FormatException exception)
		{
			throw new DataCacheException("RESOURCE MANAGER", 7002, exception.Message, exception, true);
		}

		// Token: 0x04001029 RID: 4137
		private string _baseName = "GlobalResourceStrings";

		// Token: 0x0400102A RID: 4138
		private static ResourceManager _resourceManager;

		// Token: 0x0400102B RID: 4139
		private static volatile GlobalResourceLoader _reader;

		// Token: 0x0400102C RID: 4140
		private static volatile object _syncroot = new object();
	}
}
