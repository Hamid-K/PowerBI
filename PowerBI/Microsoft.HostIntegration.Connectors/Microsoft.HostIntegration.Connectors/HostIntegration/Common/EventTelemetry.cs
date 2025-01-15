using System;
using System.Collections.Generic;
using System.Reflection;

namespace Microsoft.HostIntegration.Common
{
	// Token: 0x020006EE RID: 1774
	public class EventTelemetry : ApplicationInsightsTypeImplement<EventTelemetry>
	{
		// Token: 0x0600388C RID: 14476 RVA: 0x000BDD77 File Offset: 0x000BBF77
		static EventTelemetry()
		{
			ApplicationInsightsTypeImplement<EventTelemetry>.Type = TelemetryClient.Assembly.GetType("Microsoft.ApplicationInsights.DataContracts.EventTelemetry");
			EventTelemetry._propertiesInfo = ApplicationInsightsTypeImplement<EventTelemetry>.Type.GetProperty("Properties");
			EventTelemetry._metricsInfo = ApplicationInsightsTypeImplement<EventTelemetry>.Type.GetProperty("Metrics");
		}

		// Token: 0x0600388D RID: 14477 RVA: 0x000BDDB5 File Offset: 0x000BBFB5
		public EventTelemetry(string name)
		{
			base.Value = Activator.CreateInstance(ApplicationInsightsTypeImplement<EventTelemetry>.Type, new object[] { name });
		}

		// Token: 0x17000C8D RID: 3213
		// (get) Token: 0x0600388E RID: 14478 RVA: 0x000BDDD7 File Offset: 0x000BBFD7
		public TelemetryContext Context
		{
			get
			{
				if (this._context == null)
				{
					this._context = new TelemetryContext(base.Value);
				}
				return this._context;
			}
		}

		// Token: 0x17000C8E RID: 3214
		// (get) Token: 0x0600388F RID: 14479 RVA: 0x000BDDF8 File Offset: 0x000BBFF8
		public IDictionary<string, string> Properties
		{
			get
			{
				return EventTelemetry._propertiesInfo.GetValue(base.Value) as IDictionary<string, string>;
			}
		}

		// Token: 0x17000C8F RID: 3215
		// (get) Token: 0x06003890 RID: 14480 RVA: 0x000BDE0F File Offset: 0x000BC00F
		public IDictionary<string, double> Metrics
		{
			get
			{
				return EventTelemetry._metricsInfo.GetValue(base.Value) as IDictionary<string, double>;
			}
		}

		// Token: 0x040020CF RID: 8399
		private static PropertyInfo _propertiesInfo;

		// Token: 0x040020D0 RID: 8400
		private static PropertyInfo _metricsInfo;

		// Token: 0x040020D1 RID: 8401
		private TelemetryContext _context;
	}
}
