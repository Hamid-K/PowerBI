using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.BIServer.Telemetry.Helpers;
using Microsoft.BIServer.Telemetry.Interfaces;
using Microsoft.ReportingServices.Editions;
using Microsoft.Win32;

namespace Microsoft.BIServer.HostingEnvironment
{
	// Token: 0x0200001C RID: 28
	public class BIServerTelemetryConfiguration : ITelemetryServiceConfiguration
	{
		// Token: 0x060000B6 RID: 182 RVA: 0x00003DC0 File Offset: 0x00001FC0
		public BIServerTelemetryConfiguration(string instanceName)
		{
			this._instanceName = instanceName;
			this.Build = TelemetryUtils.GetProductVersion();
			this.AdditionalProperties = new Dictionary<string, string>();
			this.AdditionalProperties.Add("IsPublicBuild", this.IsPublicBuild().ToString());
			SkuInfo skuInfo = new SkuStore().Load(instanceName);
			this.AdditionalProperties.Add("Edition", skuInfo.SkuType.GetStrings().ShortName);
			this.Product = skuInfo.SkuType.GetDetails().Product.GetStrings().FullName;
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000B7 RID: 183 RVA: 0x00003E5A File Offset: 0x0000205A
		public string InstrumentationKey
		{
			get
			{
				return "AIF-b36ef884-8be2-4360-a67c-37b9872d5007";
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000B8 RID: 184 RVA: 0x00003E64 File Offset: 0x00002064
		public bool IsTelemetryEnabled
		{
			get
			{
				return this.ReadCpeRegKey("CustomerFeedback") as int? == 1;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000B9 RID: 185 RVA: 0x00003E9E File Offset: 0x0000209E
		// (set) Token: 0x060000BA RID: 186 RVA: 0x00003EA6 File Offset: 0x000020A6
		public string Build { get; private set; }

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000BB RID: 187 RVA: 0x00003EAF File Offset: 0x000020AF
		// (set) Token: 0x060000BC RID: 188 RVA: 0x00003EB7 File Offset: 0x000020B7
		public string Product { get; private set; }

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000BD RID: 189 RVA: 0x00003EC0 File Offset: 0x000020C0
		// (set) Token: 0x060000BE RID: 190 RVA: 0x00003EC8 File Offset: 0x000020C8
		public Dictionary<string, string> AdditionalProperties { get; private set; }

		// Token: 0x060000BF RID: 191 RVA: 0x00003ED4 File Offset: 0x000020D4
		internal object ReadCpeRegKey(string keyName)
		{
			string text = "Software\\Microsoft\\Microsoft SQL Server\\{0}\\";
			object obj = null;
			RegistryKey registryKey = null;
			try
			{
				string text2 = string.Format(CultureInfo.InvariantCulture, text, this._instanceName + "\\CPE");
				registryKey = Registry.LocalMachine.OpenSubKey(text2);
				if (registryKey == null)
				{
					return null;
				}
				obj = registryKey.GetValue(keyName);
			}
			finally
			{
				if (registryKey != null)
				{
					registryKey.Close();
				}
			}
			return obj;
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00003F44 File Offset: 0x00002144
		private bool IsPublicBuild()
		{
			if (BIServerTelemetryConfiguration.InitialRun)
			{
				int? num = this.ReadCpeRegKey("PrivateBuild") as int?;
				BIServerTelemetryConfiguration._isPublicBuild = num == null || num != 1;
			}
			BIServerTelemetryConfiguration.InitialRun = BIServerTelemetryConfiguration.InitialRun && false;
			return BIServerTelemetryConfiguration._isPublicBuild;
		}

		// Token: 0x04000072 RID: 114
		private readonly string _instanceName;

		// Token: 0x04000073 RID: 115
		private static bool InitialRun = true;

		// Token: 0x04000074 RID: 116
		private static bool _isPublicBuild = true;

		// Token: 0x04000078 RID: 120
		private const string PRIVATE_BUILD = "PrivateBuild";
	}
}
