using System;
using System.Collections.Generic;
using System.Reflection;

namespace Microsoft.HostIntegration.Common
{
	// Token: 0x020006EF RID: 1775
	public class ExceptionTelemetry : ApplicationInsightsTypeImplement<ExceptionTelemetry>
	{
		// Token: 0x06003891 RID: 14481 RVA: 0x000BDE28 File Offset: 0x000BC028
		static ExceptionTelemetry()
		{
			ApplicationInsightsTypeImplement<ExceptionTelemetry>.Type = TelemetryClient.Assembly.GetType("Microsoft.ApplicationInsights.DataContracts.ExceptionTelemetry");
			ExceptionTelemetry._propertiesInfo = ApplicationInsightsTypeImplement<ExceptionTelemetry>.Type.GetProperty("Properties");
			ExceptionTelemetry._handledAtInfo = ApplicationInsightsTypeImplement<ExceptionTelemetry>.Type.GetProperty("HandledAt");
			ExceptionTelemetry._enumType = ApplicationInsightsTypeImplement<ExceptionTelemetry>.Type.Assembly.GetType("Microsoft.ApplicationInsights.DataContracts.ExceptionHandledAt");
		}

		// Token: 0x06003892 RID: 14482 RVA: 0x000BDE8A File Offset: 0x000BC08A
		public ExceptionTelemetry(Exception exception)
		{
			base.Value = Activator.CreateInstance(ApplicationInsightsTypeImplement<ExceptionTelemetry>.Type, new object[] { exception });
		}

		// Token: 0x17000C90 RID: 3216
		// (get) Token: 0x06003893 RID: 14483 RVA: 0x000BDEAC File Offset: 0x000BC0AC
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

		// Token: 0x17000C91 RID: 3217
		// (get) Token: 0x06003894 RID: 14484 RVA: 0x000BDECD File Offset: 0x000BC0CD
		public IDictionary<string, string> Properties
		{
			get
			{
				return ExceptionTelemetry._propertiesInfo.GetValue(base.Value) as IDictionary<string, string>;
			}
		}

		// Token: 0x17000C92 RID: 3218
		// (get) Token: 0x06003895 RID: 14485 RVA: 0x000BDEE4 File Offset: 0x000BC0E4
		// (set) Token: 0x06003896 RID: 14486 RVA: 0x000BDF1C File Offset: 0x000BC11C
		public ExceptionHandledAt HandledAt
		{
			get
			{
				object value = ExceptionTelemetry._handledAtInfo.GetValue(base.Value);
				return (ExceptionHandledAt)Enum.Parse(typeof(ExceptionHandledAt), value.ToString());
			}
			set
			{
				object obj = Enum.Parse(ExceptionTelemetry._enumType, value.ToString());
				ExceptionTelemetry._handledAtInfo.SetValue(base.Value, obj);
			}
		}

		// Token: 0x040020D2 RID: 8402
		private static PropertyInfo _propertiesInfo;

		// Token: 0x040020D3 RID: 8403
		private static PropertyInfo _handledAtInfo;

		// Token: 0x040020D4 RID: 8404
		private static Type _enumType;

		// Token: 0x040020D5 RID: 8405
		private TelemetryContext _context;
	}
}
