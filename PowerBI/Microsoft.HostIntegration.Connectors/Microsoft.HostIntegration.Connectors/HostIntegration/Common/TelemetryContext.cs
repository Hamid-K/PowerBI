using System;
using System.Collections.Generic;
using System.Reflection;

namespace Microsoft.HostIntegration.Common
{
	// Token: 0x020006F4 RID: 1780
	public class TelemetryContext : ApplicationInsightsTypeImplement<TelemetryContext>
	{
		// Token: 0x060038AA RID: 14506 RVA: 0x000BE247 File Offset: 0x000BC447
		static TelemetryContext()
		{
			ApplicationInsightsTypeImplement<TelemetryContext>.Type = TelemetryClient.Assembly.GetType("Microsoft.ApplicationInsights.DataContracts.TelemetryContext");
			TelemetryContext._propertiesInfo = ApplicationInsightsTypeImplement<TelemetryContext>.Type.GetProperty("Properties");
			TelemetryContext._instrumentationKeyInfo = ApplicationInsightsTypeImplement<TelemetryContext>.Type.GetProperty("InstrumentationKey");
		}

		// Token: 0x060038AB RID: 14507 RVA: 0x000BE285 File Offset: 0x000BC485
		internal TelemetryContext(object value)
		{
			if (value != null)
			{
				base.Value = value.GetType().GetProperty("Context").GetValue(value);
			}
		}

		// Token: 0x17000C98 RID: 3224
		// (get) Token: 0x060038AC RID: 14508 RVA: 0x000BE2AC File Offset: 0x000BC4AC
		public DeviceContext Device
		{
			get
			{
				if (this._device == null)
				{
					this._device = new DeviceContext(base.Value);
				}
				return this._device;
			}
		}

		// Token: 0x17000C99 RID: 3225
		// (get) Token: 0x060038AD RID: 14509 RVA: 0x000BE2CD File Offset: 0x000BC4CD
		// (set) Token: 0x060038AE RID: 14510 RVA: 0x000BE2E4 File Offset: 0x000BC4E4
		public string InstrumentationKey
		{
			get
			{
				return TelemetryContext._instrumentationKeyInfo.GetValue(base.Value) as string;
			}
			set
			{
				TelemetryContext._instrumentationKeyInfo.SetValue(base.Value, value);
			}
		}

		// Token: 0x17000C9A RID: 3226
		// (get) Token: 0x060038AF RID: 14511 RVA: 0x000BE2F7 File Offset: 0x000BC4F7
		public ComponentContext Component
		{
			get
			{
				if (this._component == null)
				{
					this._component = new ComponentContext(base.Value);
				}
				return this._component;
			}
		}

		// Token: 0x17000C9B RID: 3227
		// (get) Token: 0x060038B0 RID: 14512 RVA: 0x000BE318 File Offset: 0x000BC518
		public SessionContext Session
		{
			get
			{
				if (this._session == null)
				{
					this._session = new SessionContext(base.Value);
				}
				return this._session;
			}
		}

		// Token: 0x17000C9C RID: 3228
		// (get) Token: 0x060038B1 RID: 14513 RVA: 0x000BE339 File Offset: 0x000BC539
		public OperationContext Operation
		{
			get
			{
				if (this._operation == null)
				{
					this._operation = new OperationContext(base.Value);
				}
				return this._operation;
			}
		}

		// Token: 0x17000C9D RID: 3229
		// (get) Token: 0x060038B2 RID: 14514 RVA: 0x000BE35A File Offset: 0x000BC55A
		public UserContext User
		{
			get
			{
				if (this._user == null)
				{
					this._user = new UserContext(base.Value);
				}
				return this._user;
			}
		}

		// Token: 0x17000C9E RID: 3230
		// (get) Token: 0x060038B3 RID: 14515 RVA: 0x000BE37B File Offset: 0x000BC57B
		public IDictionary<string, string> Properties
		{
			get
			{
				return TelemetryContext._propertiesInfo.GetValue(base.Value) as IDictionary<string, string>;
			}
		}

		// Token: 0x040020E6 RID: 8422
		private DeviceContext _device;

		// Token: 0x040020E7 RID: 8423
		private ComponentContext _component;

		// Token: 0x040020E8 RID: 8424
		private SessionContext _session;

		// Token: 0x040020E9 RID: 8425
		private OperationContext _operation;

		// Token: 0x040020EA RID: 8426
		private UserContext _user;

		// Token: 0x040020EB RID: 8427
		private static readonly PropertyInfo _instrumentationKeyInfo;

		// Token: 0x040020EC RID: 8428
		private static PropertyInfo _propertiesInfo;
	}
}
