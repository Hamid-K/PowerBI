using System;
using System.Reflection;

namespace Microsoft.HostIntegration.Common
{
	// Token: 0x020006FA RID: 1786
	public class TelemetryConfiguration : ApplicationInsightsTypeImplement<TelemetryConfiguration>
	{
		// Token: 0x060038CA RID: 14538 RVA: 0x000BE638 File Offset: 0x000BC838
		static TelemetryConfiguration()
		{
			ApplicationInsightsTypeImplement<TelemetryConfiguration>.Type = TelemetryClient.Assembly.GetType("Microsoft.ApplicationInsights.Extensibility.TelemetryConfiguration");
			TelemetryConfiguration._instrumentationKeyInfo = ApplicationInsightsTypeImplement<TelemetryConfiguration>.Type.GetProperty("InstrumentationKey");
		}

		// Token: 0x060038CB RID: 14539 RVA: 0x000BE687 File Offset: 0x000BC887
		private static TelemetryConfiguration GetActiveTelemetryConfiguration()
		{
			Assembly assembly = TelemetryClient.Assembly;
			return new TelemetryConfiguration
			{
				Value = ApplicationInsightsTypeImplement<TelemetryConfiguration>.Type.GetProperty("Active").GetValue(null)
			};
		}

		// Token: 0x17000CA5 RID: 3237
		// (get) Token: 0x060038CC RID: 14540 RVA: 0x000BE6AF File Offset: 0x000BC8AF
		public static TelemetryConfiguration Active
		{
			get
			{
				return TelemetryConfiguration._active.Value;
			}
		}

		// Token: 0x17000CA6 RID: 3238
		// (get) Token: 0x060038CD RID: 14541 RVA: 0x000BE6BB File Offset: 0x000BC8BB
		// (set) Token: 0x060038CE RID: 14542 RVA: 0x000BE6D2 File Offset: 0x000BC8D2
		public string InstrumentationKey
		{
			get
			{
				return TelemetryConfiguration._instrumentationKeyInfo.GetValue(base.Value) as string;
			}
			set
			{
				TelemetryConfiguration._instrumentationKeyInfo.SetValue(base.Value, value);
			}
		}

		// Token: 0x040020F3 RID: 8435
		private static readonly Lazy<TelemetryConfiguration> _active = new Lazy<TelemetryConfiguration>(() => TelemetryConfiguration.GetActiveTelemetryConfiguration());

		// Token: 0x040020F4 RID: 8436
		private static PropertyInfo _instrumentationKeyInfo;
	}
}
