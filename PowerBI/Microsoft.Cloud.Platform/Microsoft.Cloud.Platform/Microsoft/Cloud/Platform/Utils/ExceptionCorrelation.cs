using System;
using System.Globalization;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x02000253 RID: 595
	[Serializable]
	public class ExceptionCorrelation : IMonitoredError, IContainsPrivateInformation
	{
		// Token: 0x17000235 RID: 565
		// (get) Token: 0x06000F63 RID: 3939 RVA: 0x000348BF File Offset: 0x00032ABF
		// (set) Token: 0x06000F64 RID: 3940 RVA: 0x000348C7 File Offset: 0x00032AC7
		public MonitoringScopeId MonitoringScope
		{
			get
			{
				return this.m_monitoringScope;
			}
			set
			{
				this.m_monitoringScope = value;
			}
		}

		// Token: 0x06000F65 RID: 3941 RVA: 0x000348D0 File Offset: 0x00032AD0
		public ExceptionCorrelation()
			: this(null)
		{
		}

		// Token: 0x06000F66 RID: 3942 RVA: 0x000348DC File Offset: 0x00032ADC
		public ExceptionCorrelation(Exception inner)
		{
			this.MonitoringScope = null;
			this.m_errorCorrelationId = null;
			while (this.m_errorCorrelationId == null && inner != null)
			{
				IMonitoredError monitoredError = inner as IMonitoredError;
				if (monitoredError != null)
				{
					this.m_errorCorrelationId = ErrorCorrelationId.NewCorrelatedError(monitoredError.ErrorCorrelationId);
					this.MonitoringScope = monitoredError.MonitoringScope;
					this.ErrorEventId = monitoredError.ErrorEventId;
					this.ErrorEventName = monitoredError.ErrorEventName;
					this.ErrorEventsKitId = monitoredError.ErrorEventsKitId;
					this.ErrorEventsKitName = monitoredError.ErrorEventsKitName;
				}
				inner = inner.InnerException;
			}
			if (this.m_errorCorrelationId == null)
			{
				this.m_errorCorrelationId = ErrorCorrelationId.NewRootError();
			}
		}

		// Token: 0x06000F67 RID: 3943 RVA: 0x0003497C File Offset: 0x00032B7C
		public override string ToString()
		{
			if (this.MonitoringScope == null)
			{
				return this.m_errorCorrelationId.ToString();
			}
			return string.Concat(new string[]
			{
				" ErrorCorrelationId=",
				this.m_errorCorrelationId.ToString(),
				Environment.NewLine,
				"MonitoringScope=",
				this.MonitoringScope.ToString(),
				Environment.NewLine,
				"ErrorEventName=",
				this.ErrorEventName,
				Environment.NewLine,
				"ErrorEventsKitName=",
				this.ErrorEventsKitName,
				Environment.NewLine,
				"ErrorEventId=",
				this.ErrorEventId.ToString(CultureInfo.InvariantCulture),
				Environment.NewLine,
				"ErrorEventsKitId=",
				this.ErrorEventsKitId.ToString(CultureInfo.InvariantCulture)
			});
		}

		// Token: 0x06000F68 RID: 3944 RVA: 0x0000E609 File Offset: 0x0000C809
		public string ToPrivateString()
		{
			return this.ToString();
		}

		// Token: 0x06000F69 RID: 3945 RVA: 0x00034A63 File Offset: 0x00032C63
		public string ToInternalString()
		{
			return this.ToOriginalString();
		}

		// Token: 0x06000F6A RID: 3946 RVA: 0x0000E609 File Offset: 0x0000C809
		public string ToOriginalString()
		{
			return this.ToString();
		}

		// Token: 0x06000F6B RID: 3947 RVA: 0x0000E568 File Offset: 0x0000C768
		public bool IsFatal()
		{
			return false;
		}

		// Token: 0x06000F6C RID: 3948 RVA: 0x0000E568 File Offset: 0x0000C768
		public bool IsBenign()
		{
			return false;
		}

		// Token: 0x06000F6D RID: 3949 RVA: 0x0000E568 File Offset: 0x0000C768
		public bool IsPermanent()
		{
			return false;
		}

		// Token: 0x17000236 RID: 566
		// (get) Token: 0x06000F6E RID: 3950 RVA: 0x0000E56B File Offset: 0x0000C76B
		public string ErrorShortName
		{
			get
			{
				return base.GetType().Name;
			}
		}

		// Token: 0x17000237 RID: 567
		// (get) Token: 0x06000F6F RID: 3951 RVA: 0x00034A6B File Offset: 0x00032C6B
		public ErrorCorrelationId ErrorCorrelationId
		{
			get
			{
				return this.m_errorCorrelationId;
			}
		}

		// Token: 0x17000238 RID: 568
		// (get) Token: 0x06000F70 RID: 3952 RVA: 0x00034A73 File Offset: 0x00032C73
		// (set) Token: 0x06000F71 RID: 3953 RVA: 0x00034A7B File Offset: 0x00032C7B
		public long ErrorEventId { get; set; }

		// Token: 0x17000239 RID: 569
		// (get) Token: 0x06000F72 RID: 3954 RVA: 0x00034A84 File Offset: 0x00032C84
		// (set) Token: 0x06000F73 RID: 3955 RVA: 0x00034A8C File Offset: 0x00032C8C
		public string ErrorEventName { get; set; }

		// Token: 0x1700023A RID: 570
		// (get) Token: 0x06000F74 RID: 3956 RVA: 0x00034A95 File Offset: 0x00032C95
		// (set) Token: 0x06000F75 RID: 3957 RVA: 0x00034A9D File Offset: 0x00032C9D
		public long ErrorEventsKitId { get; set; }

		// Token: 0x1700023B RID: 571
		// (get) Token: 0x06000F76 RID: 3958 RVA: 0x00034AA6 File Offset: 0x00032CA6
		// (set) Token: 0x06000F77 RID: 3959 RVA: 0x00034AAE File Offset: 0x00032CAE
		public string ErrorEventsKitName { get; set; }

		// Token: 0x040005D5 RID: 1493
		private readonly ErrorCorrelationId m_errorCorrelationId;

		// Token: 0x040005D6 RID: 1494
		private MonitoringScopeId m_monitoringScope;
	}
}
