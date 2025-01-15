using System;
using System.Configuration;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Microsoft.BIServer.HostingEnvironment;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Library;
using Microsoft.ReportingServices.Portal.Interfaces.Configuration;

namespace Microsoft.ReportingServices.Portal.Services.Configuration
{
	// Token: 0x02000070 RID: 112
	[ExcludeFromCodeCoverage]
	internal sealed class RsConfigProvider : IRsConfigProvider
	{
		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000371 RID: 881 RVA: 0x000159FC File Offset: 0x00013BFC
		// (remove) Token: 0x06000372 RID: 882 RVA: 0x00015A34 File Offset: 0x00013C34
		public event EventHandler ConfigurationChanged;

		// Token: 0x06000373 RID: 883 RVA: 0x00015A6C File Offset: 0x00013C6C
		public RsConfigProvider(ILogger logger)
		{
			if (logger == null)
			{
				throw new ArgumentNullException("logger");
			}
			this._logger = logger;
			Sku.IgnoreExpirationCheck = true;
			RunningApplication runningApplication = RunningApplication.ReportServerWebApp;
			string text = ConfigurationManager.AppSettings["ReportServerPath"] ?? "..\\ReportServer\\";
			this._configFileManager = new RSConfigurationFileWatcherManager("rsreportserver.config", text);
			Globals.InitConfiguration(this._configFileManager, runningApplication);
		}

		// Token: 0x06000374 RID: 884 RVA: 0x00015AD4 File Offset: 0x00013CD4
		private void OnConfigurationChanged(object sender, ConfigurationChangeEventArgs configurationChangeEventArgs)
		{
			EventHandler configurationChanged = this.ConfigurationChanged;
			if (configurationChanged != null)
			{
				configurationChanged(this, EventArgs.Empty);
			}
		}

		// Token: 0x06000375 RID: 885 RVA: 0x00015AF8 File Offset: 0x00013CF8
		public IRSPortalConfiguration Load()
		{
			try
			{
				ServiceController.EnsureConfigurationFromDB();
			}
			catch (ReportCatalogException ex)
			{
				this._logger.Trace(TraceLevel.Error, ex.Message);
			}
			return this._configFileManager.GetConfiguration();
		}

		// Token: 0x06000376 RID: 886 RVA: 0x00015B3C File Offset: 0x00013D3C
		object IRsConfigProvider.Load()
		{
			return this.Load();
		}

		// Token: 0x040000FC RID: 252
		private readonly ILogger _logger;

		// Token: 0x040000FD RID: 253
		private readonly RSConfigurationFileManager _configFileManager;

		// Token: 0x040000FE RID: 254
		private const string DefaultRsInstallPath = "..\\ReportServer\\";
	}
}
