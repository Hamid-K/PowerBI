using System;
using System.Reflection;

namespace Microsoft.HostIntegration.Common
{
	// Token: 0x020006F5 RID: 1781
	public class DeviceContext : ApplicationInsightsTypeImplement<DeviceContext>
	{
		// Token: 0x060038B4 RID: 14516 RVA: 0x000BE392 File Offset: 0x000BC592
		static DeviceContext()
		{
			ApplicationInsightsTypeImplement<DeviceContext>.Type = TelemetryClient.Assembly.GetType("Microsoft.ApplicationInsights.Extensibility.Implementation.DeviceContext");
			DeviceContext._idInfo = ApplicationInsightsTypeImplement<DeviceContext>.Type.GetProperty("Id");
		}

		// Token: 0x060038B5 RID: 14517 RVA: 0x000BE3BC File Offset: 0x000BC5BC
		internal DeviceContext(object value)
		{
			if (value != null)
			{
				base.Value = value.GetType().GetProperty("Device").GetValue(value);
			}
		}

		// Token: 0x17000C9F RID: 3231
		// (get) Token: 0x060038B6 RID: 14518 RVA: 0x000BE3E3 File Offset: 0x000BC5E3
		// (set) Token: 0x060038B7 RID: 14519 RVA: 0x000BE3FA File Offset: 0x000BC5FA
		public string Id
		{
			get
			{
				return DeviceContext._idInfo.GetValue(base.Value) as string;
			}
			set
			{
				DeviceContext._idInfo.SetValue(base.Value, value);
			}
		}

		// Token: 0x040020ED RID: 8429
		private static PropertyInfo _idInfo;
	}
}
