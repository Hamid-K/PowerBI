using System;
using System.Globalization;
using Microsoft.Owin.Hosting.Engine;
using Microsoft.Owin.Hosting.Services;
using Microsoft.Owin.Hosting.Starter;
using Owin;

namespace Microsoft.Owin.Hosting
{
	// Token: 0x02000008 RID: 8
	public static class WebApp
	{
		// Token: 0x0600002D RID: 45 RVA: 0x00002CFC File Offset: 0x00000EFC
		public static IDisposable Start(string url, Action<IAppBuilder> startup)
		{
			return WebApp.Start(WebApp.BuildOptions(url), startup);
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002D0A File Offset: 0x00000F0A
		public static IDisposable Start(StartOptions options, Action<IAppBuilder> startup)
		{
			return WebApp.StartImplementation(WebApp.BuildServices(options), options, startup);
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002D19 File Offset: 0x00000F19
		public static IDisposable Start<TStartup>(string url)
		{
			return WebApp.Start<TStartup>(WebApp.BuildOptions(url));
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002D26 File Offset: 0x00000F26
		public static IDisposable Start<TStartup>(StartOptions options)
		{
			if (options == null)
			{
				throw new ArgumentNullException("options");
			}
			options.AppStartup = typeof(TStartup).AssemblyQualifiedName;
			return WebApp.Start(options);
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002D51 File Offset: 0x00000F51
		public static IDisposable Start(string url)
		{
			if (url == null)
			{
				throw new ArgumentNullException("url");
			}
			return WebApp.Start(WebApp.BuildOptions(url));
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002D6C File Offset: 0x00000F6C
		public static IDisposable Start(StartOptions options)
		{
			if (options == null)
			{
				throw new ArgumentNullException("options");
			}
			return WebApp.StartImplementation(WebApp.BuildServices(options), options);
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002D88 File Offset: 0x00000F88
		private static StartOptions BuildOptions(string url)
		{
			return new StartOptions(url);
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002D90 File Offset: 0x00000F90
		private static IServiceProvider BuildServices(StartOptions options)
		{
			if (options.Settings != null)
			{
				return ServicesFactory.Create(options.Settings);
			}
			return ServicesFactory.Create();
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002DAC File Offset: 0x00000FAC
		private static IDisposable StartImplementation(IServiceProvider services, StartOptions options)
		{
			if (services == null)
			{
				throw new ArgumentNullException("services");
			}
			if (options == null)
			{
				throw new ArgumentNullException("options");
			}
			IHostingStarter starter = services.GetService<IHostingStarter>();
			if (starter == null)
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Resources.Exception_FailedToResolveService, new object[] { "IHostingStarter" }));
			}
			return starter.Start(options);
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002E0C File Offset: 0x0000100C
		private static IDisposable StartImplementation(IServiceProvider services, StartOptions options, Action<IAppBuilder> startup)
		{
			if (services == null)
			{
				throw new ArgumentNullException("services");
			}
			if (options == null)
			{
				throw new ArgumentNullException("options");
			}
			if (startup == null)
			{
				throw new ArgumentNullException("startup");
			}
			if (string.IsNullOrWhiteSpace(options.AppStartup))
			{
				options.AppStartup = startup.Method.ReflectedType.FullName;
			}
			IHostingEngine engine = services.GetService<IHostingEngine>();
			if (engine == null)
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Resources.Exception_FailedToResolveService, new object[] { "IHostingEngine" }));
			}
			return engine.Start(new StartContext(options)
			{
				Startup = startup
			});
		}
	}
}
