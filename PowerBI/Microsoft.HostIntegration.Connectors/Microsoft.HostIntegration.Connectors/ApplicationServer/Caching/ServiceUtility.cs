using System;
using System.Diagnostics;
using System.Globalization;
using System.ServiceProcess;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020001C7 RID: 455
	internal static class ServiceUtility
	{
		// Token: 0x06000EF9 RID: 3833 RVA: 0x00032F0F File Offset: 0x0003110F
		public static void StartServiceForCache(string serviceName, Func<ServiceBase> serviceCreator)
		{
			ServiceUtility.StartService(serviceName, serviceCreator, "Microsoft-Windows Server AppFabric Caching");
		}

		// Token: 0x06000EFA RID: 3834 RVA: 0x00032F20 File Offset: 0x00031120
		public static void StartService(string serviceName, Func<ServiceBase> serviceCreator, string eventSourceName)
		{
			if (serviceCreator == null)
			{
				throw new ArgumentNullException("serviceCreator");
			}
			try
			{
				ServiceBase[] array = new ServiceBase[] { serviceCreator() };
				ServiceBase.Run(array);
			}
			catch (Exception ex)
			{
				using (EventLog eventLog = new EventLog())
				{
					eventLog.Source = eventSourceName;
					string text = string.Format(CultureInfo.CurrentUICulture, GlobalResourceLoader.GetString(CultureInfo.CurrentUICulture, "ServiceStartupFailed"), new object[] { serviceName, ex });
					eventLog.WriteEntry(text, EventLogEntryType.Error);
				}
				throw;
			}
		}
	}
}
