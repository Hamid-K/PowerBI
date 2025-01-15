using System;
using System.Reflection;

namespace Microsoft.HostIntegration.Common
{
	// Token: 0x020006F6 RID: 1782
	public class ComponentContext : ApplicationInsightsTypeImplement<ComponentContext>
	{
		// Token: 0x060038B8 RID: 14520 RVA: 0x000BE40D File Offset: 0x000BC60D
		static ComponentContext()
		{
			ApplicationInsightsTypeImplement<ComponentContext>.Type = TelemetryClient.Assembly.GetType("Microsoft.ApplicationInsights.Extensibility.Implementation.ComponentContext");
			ComponentContext._versionInfo = ApplicationInsightsTypeImplement<ComponentContext>.Type.GetProperty("Version");
		}

		// Token: 0x060038B9 RID: 14521 RVA: 0x000BE437 File Offset: 0x000BC637
		internal ComponentContext(object value)
		{
			if (value != null)
			{
				base.Value = value.GetType().GetProperty("Component").GetValue(value);
			}
		}

		// Token: 0x17000CA0 RID: 3232
		// (get) Token: 0x060038BA RID: 14522 RVA: 0x000BE45E File Offset: 0x000BC65E
		// (set) Token: 0x060038BB RID: 14523 RVA: 0x000BE475 File Offset: 0x000BC675
		public string Version
		{
			get
			{
				return ComponentContext._versionInfo.GetValue(base.Value) as string;
			}
			set
			{
				ComponentContext._versionInfo.SetValue(base.Value, value);
			}
		}

		// Token: 0x040020EE RID: 8430
		private static PropertyInfo _versionInfo;
	}
}
