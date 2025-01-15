using System;
using System.Reflection;

namespace Microsoft.HostIntegration.Common
{
	// Token: 0x020006F7 RID: 1783
	public class SessionContext : ApplicationInsightsTypeImplement<SessionContext>
	{
		// Token: 0x060038BC RID: 14524 RVA: 0x000BE488 File Offset: 0x000BC688
		static SessionContext()
		{
			ApplicationInsightsTypeImplement<SessionContext>.Type = TelemetryClient.Assembly.GetType("Microsoft.ApplicationInsights.Extensibility.Implementation.SessionContext");
			SessionContext._idInfo = ApplicationInsightsTypeImplement<SessionContext>.Type.GetProperty("Id");
		}

		// Token: 0x060038BD RID: 14525 RVA: 0x000BE4B2 File Offset: 0x000BC6B2
		internal SessionContext(object value)
		{
			if (value != null)
			{
				base.Value = value.GetType().GetProperty("Session").GetValue(value);
			}
		}

		// Token: 0x17000CA1 RID: 3233
		// (get) Token: 0x060038BE RID: 14526 RVA: 0x000BE4D9 File Offset: 0x000BC6D9
		// (set) Token: 0x060038BF RID: 14527 RVA: 0x000BE4F0 File Offset: 0x000BC6F0
		public string Id
		{
			get
			{
				return SessionContext._idInfo.GetValue(base.Value) as string;
			}
			set
			{
				SessionContext._idInfo.SetValue(base.Value, value);
			}
		}

		// Token: 0x040020EF RID: 8431
		private static PropertyInfo _idInfo;
	}
}
