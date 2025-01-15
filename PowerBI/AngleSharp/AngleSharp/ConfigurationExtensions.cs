using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using AngleSharp.Dom.Css;
using AngleSharp.Extensions;
using AngleSharp.Network;
using AngleSharp.Network.Default;
using AngleSharp.Services;
using AngleSharp.Services.Default;

namespace AngleSharp
{
	// Token: 0x0200000A RID: 10
	public static class ConfigurationExtensions
	{
		// Token: 0x06000047 RID: 71 RVA: 0x00002922 File Offset: 0x00000B22
		public static Configuration With(this IConfiguration configuration, object service)
		{
			if (configuration == null)
			{
				throw new ArgumentNullException("configuration");
			}
			if (service == null)
			{
				throw new ArgumentNullException("service");
			}
			return new Configuration(configuration.Services.Concat(service));
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002951 File Offset: 0x00000B51
		public static Configuration With(this IConfiguration configuration, IEnumerable<object> services)
		{
			if (configuration == null)
			{
				throw new ArgumentNullException("configuration");
			}
			if (services == null)
			{
				throw new ArgumentNullException("services");
			}
			return new Configuration(configuration.Services.Concat(services));
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002980 File Offset: 0x00000B80
		public static IConfiguration With<TService>(this IConfiguration configuration, Func<IBrowsingContext, TService> creator)
		{
			IEnumerable<object> enumerable = configuration.Services;
			IEnumerable<Func<IBrowsingContext, TService>> enumerable2 = configuration.Services.OfType<Func<IBrowsingContext, TService>>();
			if (enumerable2.Any<Func<IBrowsingContext, TService>>())
			{
				enumerable = enumerable.Except(enumerable2);
			}
			return new Configuration(enumerable.Concat(creator));
		}

		// Token: 0x0600004A RID: 74 RVA: 0x000029BC File Offset: 0x00000BBC
		public static Configuration SetCulture(this IConfiguration configuration, string name)
		{
			CultureInfo cultureInfo = new CultureInfo(name);
			return configuration.SetCulture(cultureInfo);
		}

		// Token: 0x0600004B RID: 75 RVA: 0x000029D7 File Offset: 0x00000BD7
		public static Configuration SetCulture(this IConfiguration configuration, CultureInfo culture)
		{
			return configuration.With(culture);
		}

		// Token: 0x0600004C RID: 76 RVA: 0x000029E0 File Offset: 0x00000BE0
		public static IConfiguration WithCss(this IConfiguration configuration, Action<CssStyleEngine> setup = null)
		{
			if (configuration == null)
			{
				throw new ArgumentNullException("configuration");
			}
			if (!configuration.GetServices<IStylingProvider>().Any<IStylingProvider>())
			{
				StylingService stylingService = new StylingService();
				CssStyleEngine cssStyleEngine = new CssStyleEngine();
				if (setup != null)
				{
					setup(cssStyleEngine);
				}
				stylingService.Register(cssStyleEngine);
				return configuration.With(stylingService);
			}
			return configuration;
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002A30 File Offset: 0x00000C30
		public static IConfiguration WithDefaultLoader(this IConfiguration configuration, Action<ConfigurationExtensions.LoaderSetup> setup = null, IEnumerable<IRequester> requesters = null)
		{
			if (configuration == null)
			{
				throw new ArgumentNullException("configuration");
			}
			IConfiguration configuration2 = configuration;
			IEnumerable<object> enumerable = requesters;
			if (requesters == null)
			{
				IRequester[] array = new IRequester[2];
				array[0] = new HttpRequester(null);
				enumerable = array;
				array[1] = new DataRequester();
			}
			configuration = configuration2.With(enumerable);
			ConfigurationExtensions.LoaderSetup config = new ConfigurationExtensions.LoaderSetup
			{
				Filter = null,
				IsNavigationEnabled = true,
				IsResourceLoadingEnabled = false
			};
			configuration.GetFactory<IServiceFactory>();
			if (setup != null)
			{
				setup(config);
			}
			if (config.IsNavigationEnabled)
			{
				configuration = configuration.With((IBrowsingContext ctx) => new DocumentLoader(ctx, config.Filter));
			}
			if (config.IsResourceLoadingEnabled)
			{
				configuration = configuration.With((IBrowsingContext ctx) => new ResourceLoader(ctx, config.Filter));
			}
			return configuration;
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002AF0 File Offset: 0x00000CF0
		public static IConfiguration WithLocaleBasedEncoding(this IConfiguration configuration)
		{
			if (configuration == null)
			{
				throw new ArgumentException("configuration");
			}
			if (!configuration.GetServices<IEncodingProvider>().Any<IEncodingProvider>())
			{
				LocaleEncodingProvider localeEncodingProvider = new LocaleEncodingProvider();
				return configuration.With(localeEncodingProvider);
			}
			return configuration;
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002B28 File Offset: 0x00000D28
		public static IConfiguration WithCookies(this IConfiguration configuration)
		{
			if (configuration == null)
			{
				throw new ArgumentNullException("configuration");
			}
			if (!configuration.GetServices<ICookieProvider>().Any<ICookieProvider>())
			{
				MemoryCookieProvider memoryCookieProvider = new MemoryCookieProvider();
				return configuration.With(memoryCookieProvider);
			}
			return configuration;
		}

		// Token: 0x0200041A RID: 1050
		public sealed class LoaderSetup
		{
			// Token: 0x17000A5D RID: 2653
			// (get) Token: 0x06002122 RID: 8482 RVA: 0x00058CF9 File Offset: 0x00056EF9
			// (set) Token: 0x06002123 RID: 8483 RVA: 0x00058D01 File Offset: 0x00056F01
			public bool IsNavigationEnabled { get; set; }

			// Token: 0x17000A5E RID: 2654
			// (get) Token: 0x06002124 RID: 8484 RVA: 0x00058D0A File Offset: 0x00056F0A
			// (set) Token: 0x06002125 RID: 8485 RVA: 0x00058D12 File Offset: 0x00056F12
			public bool IsResourceLoadingEnabled { get; set; }

			// Token: 0x17000A5F RID: 2655
			// (get) Token: 0x06002126 RID: 8486 RVA: 0x00058D1B File Offset: 0x00056F1B
			// (set) Token: 0x06002127 RID: 8487 RVA: 0x00058D23 File Offset: 0x00056F23
			public Predicate<IRequest> Filter { get; set; }
		}
	}
}
