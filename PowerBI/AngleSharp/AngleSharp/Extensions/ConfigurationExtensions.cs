using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using AngleSharp.Commands;
using AngleSharp.Dom;
using AngleSharp.Network;
using AngleSharp.Services;
using AngleSharp.Services.Media;
using AngleSharp.Services.Scripting;
using AngleSharp.Services.Styling;

namespace AngleSharp.Extensions
{
	// Token: 0x020000E6 RID: 230
	internal static class ConfigurationExtensions
	{
		// Token: 0x060006EF RID: 1775 RVA: 0x00033330 File Offset: 0x00031530
		public static Encoding DefaultEncoding(this IConfiguration configuration)
		{
			IEncodingProvider provider = configuration.GetProvider<IEncodingProvider>();
			string language = configuration.GetLanguage();
			return ((provider != null) ? provider.Suggest(language) : null) ?? Encoding.UTF8;
		}

		// Token: 0x060006F0 RID: 1776 RVA: 0x00033360 File Offset: 0x00031560
		public static CultureInfo GetCulture(this IConfiguration configuration)
		{
			return configuration.GetService<CultureInfo>() ?? CultureInfo.CurrentUICulture;
		}

		// Token: 0x060006F1 RID: 1777 RVA: 0x00033374 File Offset: 0x00031574
		public static CultureInfo GetCultureFromLanguage(this IConfiguration configuration, string language)
		{
			CultureInfo cultureInfo;
			try
			{
				cultureInfo = new CultureInfo(language);
			}
			catch (CultureNotFoundException)
			{
				cultureInfo = configuration.GetCulture();
			}
			return cultureInfo;
		}

		// Token: 0x060006F2 RID: 1778 RVA: 0x000333A8 File Offset: 0x000315A8
		public static string GetLanguage(this IConfiguration configuration)
		{
			return configuration.GetCulture().Name;
		}

		// Token: 0x060006F3 RID: 1779 RVA: 0x000333B5 File Offset: 0x000315B5
		public static TFactory GetFactory<TFactory>(this IConfiguration configuration)
		{
			return configuration.GetServices<TFactory>().Single<TFactory>();
		}

		// Token: 0x060006F4 RID: 1780 RVA: 0x000333C2 File Offset: 0x000315C2
		public static TProvider GetProvider<TProvider>(this IConfiguration configuration)
		{
			return configuration.GetServices<TProvider>().SingleOrDefault<TProvider>();
		}

		// Token: 0x060006F5 RID: 1781 RVA: 0x000333CF File Offset: 0x000315CF
		public static TService GetService<TService>(this IConfiguration configuration)
		{
			return configuration.GetServices<TService>().FirstOrDefault<TService>();
		}

		// Token: 0x060006F6 RID: 1782 RVA: 0x000333DC File Offset: 0x000315DC
		public static IEnumerable<TService> GetServices<TService>(this IConfiguration configuration)
		{
			return configuration.Services.OfType<TService>();
		}

		// Token: 0x060006F7 RID: 1783 RVA: 0x000333EC File Offset: 0x000315EC
		public static IResourceService<TResource> GetResourceService<TResource>(this IConfiguration configuration, string type) where TResource : IResourceInfo
		{
			foreach (IResourceService<TResource> resourceService in configuration.GetServices<IResourceService<TResource>>())
			{
				if (resourceService.SupportsType(type))
				{
					return resourceService;
				}
			}
			return null;
		}

		// Token: 0x060006F8 RID: 1784 RVA: 0x00033444 File Offset: 0x00031644
		public static string GetCookie(this IConfiguration configuration, string origin)
		{
			ICookieProvider provider = configuration.GetProvider<ICookieProvider>();
			return ((provider != null) ? provider.GetCookie(origin) : null) ?? string.Empty;
		}

		// Token: 0x060006F9 RID: 1785 RVA: 0x00033462 File Offset: 0x00031662
		public static void SetCookie(this IConfiguration configuration, string origin, string value)
		{
			ICookieProvider provider = configuration.GetProvider<ICookieProvider>();
			if (provider == null)
			{
				return;
			}
			provider.SetCookie(origin, value);
		}

		// Token: 0x060006FA RID: 1786 RVA: 0x00033478 File Offset: 0x00031678
		public static ISpellCheckService GetSpellCheck(this IConfiguration configuration, string language)
		{
			ISpellCheckService spellCheckService = null;
			CultureInfo cultureFromLanguage = configuration.GetCultureFromLanguage(language);
			foreach (ISpellCheckService spellCheckService2 in configuration.GetServices<ISpellCheckService>())
			{
				if (spellCheckService2.Culture.Equals(cultureFromLanguage))
				{
					return spellCheckService2;
				}
				if (spellCheckService2.Culture.TwoLetterISOLanguageName.Is(cultureFromLanguage.TwoLetterISOLanguageName))
				{
					spellCheckService = spellCheckService2;
				}
			}
			return spellCheckService;
		}

		// Token: 0x060006FB RID: 1787 RVA: 0x000334FC File Offset: 0x000316FC
		public static ICssStyleEngine GetCssStyleEngine(this IConfiguration configuration)
		{
			return configuration.GetStyleEngine(MimeTypeNames.Css) as ICssStyleEngine;
		}

		// Token: 0x060006FC RID: 1788 RVA: 0x0003350E File Offset: 0x0003170E
		public static IStyleEngine GetStyleEngine(this IConfiguration configuration, string type)
		{
			IStylingProvider provider = configuration.GetProvider<IStylingProvider>();
			if (provider == null)
			{
				return null;
			}
			return provider.GetEngine(type);
		}

		// Token: 0x060006FD RID: 1789 RVA: 0x00033522 File Offset: 0x00031722
		public static bool IsScripting(this IConfiguration configuration)
		{
			return ((configuration != null) ? configuration.GetProvider<IScriptingProvider>() : null) != null;
		}

		// Token: 0x060006FE RID: 1790 RVA: 0x00033533 File Offset: 0x00031733
		public static IScriptEngine GetScriptEngine(this IConfiguration configuration, string type)
		{
			IScriptingProvider provider = configuration.GetProvider<IScriptingProvider>();
			if (provider == null)
			{
				return null;
			}
			return provider.GetEngine(type);
		}

		// Token: 0x060006FF RID: 1791 RVA: 0x00033547 File Offset: 0x00031747
		public static IBrowsingContext NewContext(this IConfiguration configuration, Sandboxes security = Sandboxes.None)
		{
			return configuration.GetFactory<IContextFactory>().Create(configuration, security);
		}

		// Token: 0x06000700 RID: 1792 RVA: 0x00033556 File Offset: 0x00031756
		public static IBrowsingContext FindContext(this IConfiguration configuration, string name)
		{
			return configuration.GetFactory<IContextFactory>().Find(name);
		}

		// Token: 0x06000701 RID: 1793 RVA: 0x00033564 File Offset: 0x00031764
		public static ICommand GetCommand(this IConfiguration configuration, string commandId)
		{
			ICommandProvider provider = configuration.GetProvider<ICommandProvider>();
			if (provider == null)
			{
				return null;
			}
			return provider.GetCommand(commandId);
		}
	}
}
