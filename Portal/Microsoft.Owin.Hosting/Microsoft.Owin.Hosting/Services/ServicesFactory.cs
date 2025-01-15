using System;
using System.Collections.Generic;
using Microsoft.Owin.Hosting.Builder;
using Microsoft.Owin.Hosting.Engine;
using Microsoft.Owin.Hosting.Loader;
using Microsoft.Owin.Hosting.ServerFactory;
using Microsoft.Owin.Hosting.Starter;
using Microsoft.Owin.Hosting.Tracing;
using Microsoft.Owin.Hosting.Utilities;
using Microsoft.Owin.Logging;

namespace Microsoft.Owin.Hosting.Services
{
	// Token: 0x0200001C RID: 28
	public static class ServicesFactory
	{
		// Token: 0x06000082 RID: 130 RVA: 0x00003B28 File Offset: 0x00001D28
		public static IServiceProvider Create(IDictionary<string, string> settings, Action<ServiceProvider> configuration)
		{
			if (settings == null)
			{
				throw new ArgumentNullException("settings");
			}
			if (configuration == null)
			{
				throw new ArgumentNullException("configuration");
			}
			ServiceProvider services = new ServiceProvider();
			ServicesFactory.DoCallback(settings, delegate(Type service, Type implementation)
			{
				services.Add(service, implementation);
			});
			configuration(services);
			return services;
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00003B86 File Offset: 0x00001D86
		public static IServiceProvider Create(string settingsFile, Action<ServiceProvider> configuration)
		{
			return ServicesFactory.Create(SettingsLoader.LoadFromSettingsFile(settingsFile), configuration);
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00003B94 File Offset: 0x00001D94
		public static IServiceProvider Create(Action<ServiceProvider> configuration)
		{
			return ServicesFactory.Create(SettingsLoader.LoadFromConfig(), configuration);
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00003BA1 File Offset: 0x00001DA1
		public static IServiceProvider Create(IDictionary<string, string> settings)
		{
			return ServicesFactory.Create(settings, ServicesFactory.NoConfiguration);
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00003BAE File Offset: 0x00001DAE
		public static IServiceProvider Create(string settingsFile)
		{
			return ServicesFactory.Create(settingsFile, ServicesFactory.NoConfiguration);
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00003BBB File Offset: 0x00001DBB
		public static IServiceProvider Create()
		{
			return ServicesFactory.Create(ServicesFactory.NoConfiguration);
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00003BC7 File Offset: 0x00001DC7
		public static void ForEach(IDictionary<string, string> settings, Action<Type, Type> callback)
		{
			ServicesFactory.DoCallback(settings, callback);
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00003BD0 File Offset: 0x00001DD0
		public static void ForEach(string settingsFile, Action<Type, Type> callback)
		{
			ServicesFactory.DoCallback(SettingsLoader.LoadFromSettingsFile(settingsFile), callback);
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00003BDE File Offset: 0x00001DDE
		public static void ForEach(Action<Type, Type> callback)
		{
			ServicesFactory.DoCallback(SettingsLoader.LoadFromConfig(), callback);
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00003BEC File Offset: 0x00001DEC
		private static void DoCallback(IDictionary<string, string> settings, Action<Type, Type> callback)
		{
			ServicesFactory.DoCallback(delegate(Type service, Type implementation)
			{
				string replacementNames;
				if (settings.TryGetValue(service.FullName, out replacementNames))
				{
					foreach (string replacementName in replacementNames.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries))
					{
						Type replacement = Type.GetType(replacementName);
						callback(service, replacement);
					}
					return;
				}
				callback(service, implementation);
			});
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00003C20 File Offset: 0x00001E20
		private static void DoCallback(Action<Type, Type> callback)
		{
			callback(typeof(IHostingStarter), typeof(HostingStarter));
			callback(typeof(IHostingStarterFactory), typeof(HostingStarterFactory));
			callback(typeof(IHostingStarterActivator), typeof(HostingStarterActivator));
			callback(typeof(IHostingEngine), typeof(HostingEngine));
			callback(typeof(ITraceOutputFactory), typeof(TraceOutputFactory));
			callback(typeof(IAppLoader), typeof(AppLoader));
			callback(typeof(IAppLoaderFactory), typeof(AppLoaderFactory));
			callback(typeof(IAppActivator), typeof(AppActivator));
			callback(typeof(IAppBuilderFactory), typeof(AppBuilderFactory));
			callback(typeof(IServerFactoryLoader), typeof(ServerFactoryLoader));
			callback(typeof(IServerFactoryActivator), typeof(ServerFactoryActivator));
			callback(typeof(ILoggerFactory), typeof(ServicesFactory.InjectableDiagnosticsLoggerFactory));
		}

		// Token: 0x04000036 RID: 54
		private static readonly Action<ServiceProvider> NoConfiguration = delegate(ServiceProvider _)
		{
		};

		// Token: 0x02000042 RID: 66
		private class InjectableDiagnosticsLoggerFactory : DiagnosticsLoggerFactory
		{
		}
	}
}
