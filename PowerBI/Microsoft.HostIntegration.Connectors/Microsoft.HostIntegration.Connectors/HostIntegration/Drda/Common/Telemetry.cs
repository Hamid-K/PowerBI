using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using Microsoft.HostIntegration.Common;
using Microsoft.Win32;

namespace Microsoft.HostIntegration.Drda.Common
{
	// Token: 0x02000839 RID: 2105
	public class Telemetry
	{
		// Token: 0x17000FF5 RID: 4085
		// (get) Token: 0x060042F6 RID: 17142 RVA: 0x000E07BF File Offset: 0x000DE9BF
		public static Telemetry Instance
		{
			get
			{
				return Telemetry.instance.Value;
			}
		}

		// Token: 0x17000FF6 RID: 4086
		// (get) Token: 0x060042F7 RID: 17143 RVA: 0x000E07CB File Offset: 0x000DE9CB
		// (set) Token: 0x060042F8 RID: 17144 RVA: 0x000E07D3 File Offset: 0x000DE9D3
		public bool TelemetryEnabled { get; private set; }

		// Token: 0x060042F9 RID: 17145 RVA: 0x000E07DC File Offset: 0x000DE9DC
		private Telemetry()
		{
			this.TelemetryEnabled = false;
			if (!this.CanDoTelemetry())
			{
				return;
			}
			this.telemetryClient = new TelemetryClient();
			this.telemetryClient.Context.Device.Id = this.installationGuid;
			this.telemetryClient.Context.Component.Version = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).FileVersion;
			this.telemetryClient.Context.Session.Id = Guid.NewGuid().ToString();
		}

		// Token: 0x060042FA RID: 17146 RVA: 0x000E0878 File Offset: 0x000DEA78
		private bool CanDoTelemetry()
		{
			RegistryKey registryKey = null;
			RegistryKey registryKey2 = null;
			try
			{
				registryKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64);
				bool flag = false;
				try
				{
					registryKey2 = registryKey.OpenSubKey("SOFTWARE\\Microsoft\\Host Integration Server\\Telemetry", true);
					flag = true;
				}
				catch
				{
					registryKey2 = registryKey.OpenSubKey("SOFTWARE\\Microsoft\\Host Integration Server\\Telemetry", false);
				}
				if (registryKey2 == null)
				{
					return false;
				}
				string text = (string)registryKey2.GetValue("Enabled");
				if (string.IsNullOrWhiteSpace(text))
				{
					return false;
				}
				bool flag2;
				if (!bool.TryParse(text, out flag2))
				{
					return false;
				}
				if (!flag2)
				{
					return false;
				}
				this.TelemetryEnabled = true;
				this.msInternalMachine = false;
				using (RegistryKey registryKey3 = registryKey.OpenSubKey("SOFTWARE\\Policies\\Microsoft\\SQMClient"))
				{
					if (registryKey3 != null)
					{
						object value = registryKey3.GetValue("MSFTInternal");
						if (value is int && (int)value == 1)
						{
							this.msInternalMachine = true;
						}
					}
				}
				this.installationGuid = (string)registryKey2.GetValue("InstallationGuid");
				if (flag && string.IsNullOrWhiteSpace(this.installationGuid))
				{
					this.installationGuid = Guid.NewGuid().ToString();
					registryKey2.SetValue("InstallationGuid", this.installationGuid);
				}
				return true;
			}
			catch
			{
			}
			finally
			{
				if (registryKey != null)
				{
					registryKey.Close();
				}
				if (registryKey2 != null)
				{
					registryKey2.Close();
				}
			}
			return this.TelemetryEnabled;
		}

		// Token: 0x060042FB RID: 17147 RVA: 0x000E0A34 File Offset: 0x000DEC34
		private void TrackGlobalProperties(IDictionary<string, string> properties, string featureName)
		{
			properties["IsMsInternal"] = (this.msInternalMachine ? "true" : "false");
			properties["InstallationGuid"] = this.installationGuid;
			properties["Feature"] = featureName;
		}

		// Token: 0x060042FC RID: 17148 RVA: 0x000E0A74 File Offset: 0x000DEC74
		public void TrackEvent(string featureName, ITelemetryEvent telemetryEvent)
		{
			if (this.telemetryClient == null)
			{
				return;
			}
			if (telemetryEvent == null)
			{
				return;
			}
			EventTelemetry eventTelemetry = new EventTelemetry(telemetryEvent.EventName);
			this.TrackGlobalProperties(eventTelemetry.Properties, featureName);
			telemetryEvent.SetEventProperties(eventTelemetry.Properties);
			this.telemetryClient.TrackEvent(eventTelemetry);
			this.TelemetryEnabled = false;
		}

		// Token: 0x060042FD RID: 17149 RVA: 0x000E0AC6 File Offset: 0x000DECC6
		public void Flush()
		{
			if (this.telemetryClient != null)
			{
				this.telemetryClient.Flush();
			}
		}

		// Token: 0x04002F4B RID: 12107
		private static readonly Lazy<Telemetry> instance = new Lazy<Telemetry>(() => new Telemetry());

		// Token: 0x04002F4C RID: 12108
		private readonly TelemetryClient telemetryClient;

		// Token: 0x04002F4E RID: 12110
		private bool msInternalMachine;

		// Token: 0x04002F4F RID: 12111
		private string installationGuid;
	}
}
