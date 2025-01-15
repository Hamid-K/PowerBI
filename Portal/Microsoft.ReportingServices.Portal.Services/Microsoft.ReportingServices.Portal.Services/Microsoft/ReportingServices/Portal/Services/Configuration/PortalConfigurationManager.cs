using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using Microsoft.BIServer.HostingEnvironment;
using Microsoft.BIServer.HostingEnvironment.HostingInfo;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Library;
using Microsoft.ReportingServices.Portal.Interfaces.Configuration;
using Microsoft.ReportingServices.Portal.Services.Extensions;

namespace Microsoft.ReportingServices.Portal.Services.Configuration
{
	// Token: 0x0200006F RID: 111
	internal sealed class PortalConfigurationManager : IPortalConfigurationManager, IDisposable
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000365 RID: 869 RVA: 0x00015470 File Offset: 0x00013670
		// (remove) Token: 0x06000366 RID: 870 RVA: 0x000154A8 File Offset: 0x000136A8
		public event EventHandler CurrentConfigurationChanged;

		// Token: 0x06000367 RID: 871 RVA: 0x000154E0 File Offset: 0x000136E0
		public PortalConfigurationManager(IRsConfigProvider configProvider, ILogger logger)
		{
			if (configProvider == null)
			{
				throw new ArgumentNullException("configProvider");
			}
			if (logger == null)
			{
				throw new ArgumentNullException("logger");
			}
			this._configProvider = configProvider;
			this._logger = logger;
			this._configProvider.ConfigurationChanged += this.OnConfigurationChanged;
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000368 RID: 872 RVA: 0x00015534 File Offset: 0x00013734
		public IPortalConfiguration Current
		{
			get
			{
				IPortalConfiguration portalConfiguration;
				if ((portalConfiguration = this._current) == null)
				{
					portalConfiguration = (this._current = this.LoadSettings());
				}
				return portalConfiguration;
			}
		}

		// Token: 0x06000369 RID: 873 RVA: 0x0001555A File Offset: 0x0001375A
		void IDisposable.Dispose()
		{
			this._configProvider.ConfigurationChanged -= this.OnConfigurationChanged;
		}

		// Token: 0x0600036A RID: 874 RVA: 0x00015574 File Offset: 0x00013774
		internal void OnConfigurationChanged(object sender, EventArgs e)
		{
			this._current = null;
			EventHandler currentConfigurationChanged = this.CurrentConfigurationChanged;
			if (currentConfigurationChanged != null)
			{
				currentConfigurationChanged(this, EventArgs.Empty);
			}
		}

		// Token: 0x0600036B RID: 875 RVA: 0x000155A0 File Offset: 0x000137A0
		private IPortalConfiguration LoadSettings()
		{
			IRSPortalConfiguration irsportalConfiguration = null;
			try
			{
				irsportalConfiguration = (IRSPortalConfiguration)this._configProvider.Load();
			}
			catch (Exception ex)
			{
				this._logger.Trace(TraceLevel.Error, "Unable to load RS Configuration, error: " + ex);
			}
			PortalConfiguration portalConfiguration = new PortalConfiguration
			{
				AppConfigured = PortalConfigurationManager.IsRSWebAppConfigured(irsportalConfiguration)
			};
			if (portalConfiguration.AppConfigured)
			{
				portalConfiguration.ReportServerWebAppVirtualDirectory = PortalConfigurationManager.GetVirtualRoot(RunningApplication.ReportServerWebApp, irsportalConfiguration.UrlConfiguration);
				portalConfiguration.RegisteredWebAppUrls = PortalConfigurationManager.GetUrls(RunningApplication.ReportServerWebApp, irsportalConfiguration.UrlConfiguration);
				portalConfiguration.RegisteredReportServerUrls = PortalConfigurationManager.GetUrls(RunningApplication.WebService, irsportalConfiguration.UrlConfiguration);
				portalConfiguration.ReportServerUrl = StaticConfig.Current.GetOrDefault("Hosting-url-ReportServerWebService", string.Empty);
				Uri uri = (string.IsNullOrWhiteSpace(portalConfiguration.ReportServerUrl) ? new Uri("http://localhost") : new Uri(portalConfiguration.ReportServerUrl));
				portalConfiguration.ReportServerHostName = uri.Host;
				portalConfiguration.PowerBIUrl = StaticConfig.Current.GetOrDefault("Hosting-url-PowerBIService", string.Empty);
				portalConfiguration.ReportServerVirtualDirectory = irsportalConfiguration.ReportServerVirtualDirectory;
				portalConfiguration.AuthenticationSchemes = PortalConfigurationManager.Convert(irsportalConfiguration.AuthenticationTypes);
				portalConfiguration.AuthenticationTypes = (int)((byte)irsportalConfiguration.AuthenticationTypes);
				portalConfiguration.AuthPersistence = irsportalConfiguration.AuthPersistence;
				portalConfiguration.MaxActiveReqForOneUser = irsportalConfiguration.MaxActiveReqForOneUser;
				portalConfiguration.InstanceID = ((Globals.Configuration != null) ? Globals.Configuration.InstanceID : null);
				portalConfiguration.InstallationId = ((Globals.Configuration != null) ? Globals.Configuration.InstallationID : Guid.Empty);
				portalConfiguration.FileSizeRestrictions = new FileSizeRestrictions();
				if (Globals.Configuration != null && Globals.Configuration.ConnectionAuth == RSBaseConfiguration.CatalogConnectionAuth.Windows && Globals.Configuration.ConnectionType == RSBaseConfiguration.CatalogConnectionType.Impersonate)
				{
					portalConfiguration.CatalogConfiguration = new CatalogConfiguration(Globals.Configuration.BaseConnectionString, Globals.Configuration.CatalogUser, Globals.Configuration.CatalogDomain, Globals.Configuration.CatalogCred);
				}
				else
				{
					string text = null;
					try
					{
						text = ((Globals.Configuration != null) ? Globals.Configuration.BaseConnectionString : null);
					}
					catch (ServerConfigurationErrorException)
					{
					}
					portalConfiguration.CatalogConfiguration = new CatalogConfiguration(text);
				}
				if (irsportalConfiguration.PassthroughCookies != null)
				{
					portalConfiguration.PassthroughCookies = irsportalConfiguration.PassthroughCookies.Cast<string>().ToArray<string>();
				}
				if (irsportalConfiguration.AuthenticationTypes.HasFlag(AuthenticationTypes.RSWindowsBasic))
				{
					portalConfiguration.BasicAuthenticationLogonType = irsportalConfiguration.LogonMethod.ToLogonType();
					portalConfiguration.BasicAuthenticationRealm = irsportalConfiguration.AuthRealm;
					portalConfiguration.BasicAuthenticationDomain = irsportalConfiguration.AuthDomain;
				}
				if (irsportalConfiguration.AuthenticationTypes.HasFlag(AuthenticationTypes.OAuth))
				{
					portalConfiguration.OAuthConfiguration = irsportalConfiguration.OAuthConfiguration;
				}
				portalConfiguration.LogClientIPAddress = StaticConfig.Current.GetOrDefault(ConfigSwitches.LogClientIPAddress.ToString(), false);
			}
			return portalConfiguration;
		}

		// Token: 0x0600036C RID: 876 RVA: 0x0001585C File Offset: 0x00013A5C
		private static bool IsRSWebAppConfigured(IRSPortalConfiguration config)
		{
			return config != null && config.UrlConfiguration != null && PortalConfigurationManager.IsRSWebAppConfigured(config.UrlConfiguration);
		}

		// Token: 0x0600036D RID: 877 RVA: 0x00015876 File Offset: 0x00013A76
		internal static bool IsRSWebAppConfigured(Dictionary<RunningApplication, UrlConfiguration> urlConfig)
		{
			return urlConfig.ContainsKey(RunningApplication.ReportServerWebApp) && urlConfig.ContainsKey(RunningApplication.WebService);
		}

		// Token: 0x0600036E RID: 878 RVA: 0x0001588C File Offset: 0x00013A8C
		internal static string[] GetUrls(RunningApplication app, Dictionary<RunningApplication, UrlConfiguration> urlConfig)
		{
			if (urlConfig == null)
			{
				throw new ArgumentNullException("urlConfig", "Did you forget to set your 'ReportServerPath' in the app.config?");
			}
			if (PortalConfigurationManager.IsRSWebAppConfigured(urlConfig))
			{
				return urlConfig[app].UrlReservations.Select((UrlReservation res) => res.UrlPrefix.TrimEnd(new char[] { '/' }) + "/").ToArray<string>();
			}
			return new string[0];
		}

		// Token: 0x0600036F RID: 879 RVA: 0x000158F0 File Offset: 0x00013AF0
		internal static string GetVirtualRoot(RunningApplication app, Dictionary<RunningApplication, UrlConfiguration> urlConfig)
		{
			if (urlConfig == null)
			{
				throw new ArgumentNullException("urlConfig", "Did you forget to set your 'ReportServerPath' in the app.config?");
			}
			if (PortalConfigurationManager.IsRSWebAppConfigured(urlConfig))
			{
				return urlConfig[app].VirtualRoot;
			}
			return string.Empty;
		}

		// Token: 0x06000370 RID: 880 RVA: 0x00015920 File Offset: 0x00013B20
		internal static AuthenticationSchemes Convert(AuthenticationTypes authenticationTypes)
		{
			int num = Enum.GetValues(typeof(AuthenticationTypes)).Cast<AuthenticationTypes>().ToArray<AuthenticationTypes>()
				.Max((AuthenticationTypes t) => (int)t);
			AuthenticationSchemes authenticationSchemes = AuthenticationSchemes.None;
			while (num != 0)
			{
				AuthenticationTypes authenticationTypes2 = authenticationTypes & (AuthenticationTypes)num;
				if (authenticationTypes2 <= AuthenticationTypes.Custom)
				{
					switch (authenticationTypes2)
					{
					case AuthenticationTypes.None:
						break;
					case AuthenticationTypes.RSWindowsNegotiate:
					case AuthenticationTypes.RSWindowsKerberos:
						authenticationSchemes |= AuthenticationSchemes.Negotiate;
						break;
					case AuthenticationTypes.RSWindowsNegotiate | AuthenticationTypes.RSWindowsKerberos:
					case AuthenticationTypes.RSWindowsNegotiate | AuthenticationTypes.RSWindowsNTLM:
					case AuthenticationTypes.RSWindowsKerberos | AuthenticationTypes.RSWindowsNTLM:
					case AuthenticationTypes.RSWindowsNegotiate | AuthenticationTypes.RSWindowsKerberos | AuthenticationTypes.RSWindowsNTLM:
						goto IL_00AD;
					case AuthenticationTypes.RSWindowsNTLM:
						authenticationSchemes |= AuthenticationSchemes.Ntlm;
						break;
					case AuthenticationTypes.RSWindowsBasic:
						authenticationSchemes |= AuthenticationSchemes.Basic;
						break;
					default:
						if (authenticationTypes2 != AuthenticationTypes.Custom)
						{
							goto IL_00AD;
						}
						goto IL_009D;
					}
				}
				else
				{
					if (authenticationTypes2 == AuthenticationTypes.RSForms)
					{
						throw new NotSupportedException();
					}
					if (authenticationTypes2 != AuthenticationTypes.OAuth)
					{
						goto IL_00AD;
					}
					goto IL_009D;
				}
				IL_00C3:
				num >>= 1;
				continue;
				IL_009D:
				authenticationSchemes |= AuthenticationSchemes.Anonymous;
				goto IL_00C3;
				IL_00AD:
				throw new ArgumentOutOfRangeException("authenticationTypes", authenticationTypes2, "Unknown Authentication Type");
			}
			return authenticationSchemes;
		}

		// Token: 0x040000F8 RID: 248
		private readonly IRsConfigProvider _configProvider;

		// Token: 0x040000F9 RID: 249
		private readonly ILogger _logger;

		// Token: 0x040000FA RID: 250
		private IPortalConfiguration _current;
	}
}
