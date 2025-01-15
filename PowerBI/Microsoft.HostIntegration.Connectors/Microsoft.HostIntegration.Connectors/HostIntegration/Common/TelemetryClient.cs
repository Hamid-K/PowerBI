using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Microsoft.Win32;

namespace Microsoft.HostIntegration.Common
{
	// Token: 0x020006F1 RID: 1777
	public class TelemetryClient
	{
		// Token: 0x17000C93 RID: 3219
		// (get) Token: 0x06003897 RID: 14487 RVA: 0x000BDF52 File Offset: 0x000BC152
		// (set) Token: 0x06003898 RID: 14488 RVA: 0x000BDF59 File Offset: 0x000BC159
		internal static Type Type { get; set; } = TelemetryClient._appInsight.Value.GetType("Microsoft.ApplicationInsights.TelemetryClient");

		// Token: 0x0600389A RID: 14490 RVA: 0x000BE00A File Offset: 0x000BC20A
		public TelemetryClient()
		{
			if (TelemetryClient._appInsight.Value != null)
			{
				this._telemetryClient = Activator.CreateInstance(TelemetryClient.Type);
			}
		}

		// Token: 0x17000C94 RID: 3220
		// (get) Token: 0x0600389B RID: 14491 RVA: 0x000BE034 File Offset: 0x000BC234
		internal static Assembly Assembly
		{
			get
			{
				return TelemetryClient._appInsight.Value;
			}
		}

		// Token: 0x17000C95 RID: 3221
		// (get) Token: 0x0600389C RID: 14492 RVA: 0x000BE040 File Offset: 0x000BC240
		public TelemetryContext Context
		{
			get
			{
				if (this._context == null)
				{
					this._context = new TelemetryContext(this._telemetryClient);
					this._context.InstrumentationKey = "e2d7eaeb-9e87-4ebb-ba14-91839bbc6951";
				}
				return this._context;
			}
		}

		// Token: 0x0600389D RID: 14493 RVA: 0x000BE071 File Offset: 0x000BC271
		public void Flush()
		{
			TelemetryClient._flushInfo.Invoke(this._telemetryClient, TelemetryClient._emptyArray);
		}

		// Token: 0x0600389E RID: 14494 RVA: 0x000BE08C File Offset: 0x000BC28C
		public void TrackEvent(EventTelemetry telemetry)
		{
			if (TelemetryClient._trackEventInfo == null)
			{
				TelemetryClient._trackEventInfo = TelemetryClient.Type.GetMethod("TrackEvent", new Type[] { ApplicationInsightsTypeImplement<EventTelemetry>.Type });
			}
			TelemetryClient._trackEventInfo.Invoke(this._telemetryClient, new object[] { telemetry.Value });
		}

		// Token: 0x0600389F RID: 14495 RVA: 0x000BE0E8 File Offset: 0x000BC2E8
		public void TrackException(ExceptionTelemetry telemetry)
		{
			if (TelemetryClient._trackExceptionInfo == null)
			{
				TelemetryClient._trackExceptionInfo = TelemetryClient.Type.GetMethod("TrackException", new Type[] { ApplicationInsightsTypeImplement<ExceptionTelemetry>.Type });
			}
			TelemetryClient._trackExceptionInfo.Invoke(this._telemetryClient, new object[] { telemetry.Value });
		}

		// Token: 0x060038A0 RID: 14496 RVA: 0x000BE144 File Offset: 0x000BC344
		public void TrackMetric(string name, double value, IDictionary<string, string> properties = null)
		{
			TelemetryClient._trackMetricInfo.Invoke(this._telemetryClient, new object[] { name, value, properties });
		}

		// Token: 0x060038A1 RID: 14497 RVA: 0x000BE170 File Offset: 0x000BC370
		private static Assembly LoadAppInsightsAssembly()
		{
			string text = null;
			using (RegistryKey registryKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64))
			{
				RegistryKey registryKey2 = registryKey.OpenSubKey("SOFTWARE\\Microsoft\\Host Integration Server");
				if (registryKey2 != null)
				{
					text = (string)registryKey2.GetValue("InstallPath");
					if (!text.EndsWith("\\"))
					{
						text += "\\";
					}
					registryKey2.Close();
				}
				registryKey.Close();
			}
			if (!string.IsNullOrEmpty(text))
			{
				return Assembly.LoadFrom(text + "System\\Microsoft.ApplicationInsights.dll");
			}
			throw new FileNotFoundException("Exception while looking for Microsoft.ApplicationInsights assembly.");
		}

		// Token: 0x040020DA RID: 8410
		private static readonly Lazy<Assembly> _appInsight = new Lazy<Assembly>(() => TelemetryClient.LoadAppInsightsAssembly());

		// Token: 0x040020DB RID: 8411
		private static MethodInfo _flushInfo = TelemetryClient.Type.GetMethod("Flush", new Type[0]);

		// Token: 0x040020DC RID: 8412
		private static MethodInfo _trackEventInfo;

		// Token: 0x040020DD RID: 8413
		private static MethodInfo _trackExceptionInfo;

		// Token: 0x040020DE RID: 8414
		private static MethodInfo _trackMetricInfo = TelemetryClient.Type.GetMethod("TrackMetric", new Type[]
		{
			typeof(string),
			typeof(double),
			typeof(IDictionary<string, string>)
		});

		// Token: 0x040020DF RID: 8415
		private static object[] _emptyArray = new object[0];

		// Token: 0x040020E1 RID: 8417
		private object _telemetryClient;

		// Token: 0x040020E2 RID: 8418
		private TelemetryContext _context;
	}
}
