using System;
using System.Collections.Generic;
using dotless.Core.Cache;
using dotless.Core.configuration;
using dotless.Core.Importers;
using dotless.Core.Input;
using dotless.Core.Loggers;
using dotless.Core.Parameters;
using dotless.Core.Parser;
using dotless.Core.Plugins;
using dotless.Core.Stylizers;
using Microsoft.Extensions.DependencyInjection;

namespace dotless.Core
{
	// Token: 0x02000003 RID: 3
	public class ContainerFactory
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600000A RID: 10 RVA: 0x000021E7 File Offset: 0x000003E7
		// (set) Token: 0x0600000B RID: 11 RVA: 0x000021EF File Offset: 0x000003EF
		protected IServiceCollection Container { get; set; }

		// Token: 0x0600000C RID: 12 RVA: 0x000021F8 File Offset: 0x000003F8
		public IServiceProvider GetContainer(DotlessConfiguration configuration)
		{
			return this.GetServices(configuration).BuildServiceProvider();
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002208 File Offset: 0x00000408
		public ServiceCollection GetServices(DotlessConfiguration configuration)
		{
			ServiceCollection serviceCollection = new ServiceCollection();
			this.RegisterServices(serviceCollection, configuration);
			return serviceCollection;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002224 File Offset: 0x00000424
		protected virtual void RegisterServices(IServiceCollection services, DotlessConfiguration configuration)
		{
			if (!configuration.Web)
			{
				this.RegisterLocalServices(services);
			}
			this.RegisterCoreServices(services, configuration);
			this.OverrideServices(services, configuration);
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002245 File Offset: 0x00000445
		protected virtual void OverrideServices(IServiceCollection services, DotlessConfiguration configuration)
		{
			if (configuration.Logger != null)
			{
				services.AddSingleton(typeof(ILogger), configuration.Logger);
			}
		}

		// Token: 0x06000010 RID: 16 RVA: 0x0000226C File Offset: 0x0000046C
		protected virtual void RegisterLocalServices(IServiceCollection services)
		{
			services.AddSingleton<ICache, InMemoryCache>();
			services.AddSingleton<IParameterSource, ConsoleArgumentParameterSource>();
			services.AddSingleton<ILogger, ConsoleLogger>();
			services.AddSingleton<IPathResolver, RelativePathResolver>();
		}

		// Token: 0x06000011 RID: 17 RVA: 0x0000228C File Offset: 0x0000048C
		protected virtual void RegisterCoreServices(IServiceCollection services, DotlessConfiguration configuration)
		{
			services.AddSingleton(configuration);
			services.AddSingleton<IStylizer, PlainStylizer>();
			services.AddTransient<IImporter, Importer>();
			services.AddTransient<Parser>();
			services.AddTransient<ILessEngine, LessEngine>();
			if (configuration.CacheEnabled)
			{
				services.Decorate<ILessEngine, CacheDecorator>();
			}
			if (!configuration.DisableParameters)
			{
				services.Decorate<ILessEngine, ParameterDecorator>();
			}
			services.AddSingleton(configuration.Plugins);
			services.AddSingleton(typeof(IFileReader), configuration.LessSource);
		}
	}
}
