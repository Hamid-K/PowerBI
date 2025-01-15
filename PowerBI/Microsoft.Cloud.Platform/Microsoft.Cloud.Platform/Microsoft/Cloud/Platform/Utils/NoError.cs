using System;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x02000250 RID: 592
	[Serializable]
	public sealed class NoError : IMonitoredError, IContainsPrivateInformation
	{
		// Token: 0x06000F40 RID: 3904 RVA: 0x00034568 File Offset: 0x00032768
		public NoError()
		{
			this.ErrorCorrelationId = ErrorCorrelationId.NoError();
			this.MonitoringScope = new MonitoringScopeId(string.Empty);
			this.ErrorEventId = 0L;
			this.ErrorEventName = string.Empty;
			this.ErrorEventsKitId = 0L;
			this.ErrorEventsKitName = string.Empty;
		}

		// Token: 0x06000F41 RID: 3905 RVA: 0x0000E568 File Offset: 0x0000C768
		public bool IsFatal()
		{
			return false;
		}

		// Token: 0x06000F42 RID: 3906 RVA: 0x0000E568 File Offset: 0x0000C768
		public bool IsBenign()
		{
			return false;
		}

		// Token: 0x06000F43 RID: 3907 RVA: 0x0000E568 File Offset: 0x0000C768
		public bool IsPermanent()
		{
			return false;
		}

		// Token: 0x1700022E RID: 558
		// (get) Token: 0x06000F44 RID: 3908 RVA: 0x0000E56B File Offset: 0x0000C76B
		public string ErrorShortName
		{
			get
			{
				return base.GetType().Name;
			}
		}

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x06000F45 RID: 3909 RVA: 0x000345BC File Offset: 0x000327BC
		// (set) Token: 0x06000F46 RID: 3910 RVA: 0x000345C4 File Offset: 0x000327C4
		public ErrorCorrelationId ErrorCorrelationId { get; private set; }

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x06000F47 RID: 3911 RVA: 0x000345CD File Offset: 0x000327CD
		// (set) Token: 0x06000F48 RID: 3912 RVA: 0x000345D5 File Offset: 0x000327D5
		public MonitoringScopeId MonitoringScope { get; set; }

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x06000F49 RID: 3913 RVA: 0x000345DE File Offset: 0x000327DE
		// (set) Token: 0x06000F4A RID: 3914 RVA: 0x000345E6 File Offset: 0x000327E6
		public long ErrorEventId { get; set; }

		// Token: 0x17000232 RID: 562
		// (get) Token: 0x06000F4B RID: 3915 RVA: 0x000345EF File Offset: 0x000327EF
		// (set) Token: 0x06000F4C RID: 3916 RVA: 0x000345F7 File Offset: 0x000327F7
		public string ErrorEventName { get; set; }

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x06000F4D RID: 3917 RVA: 0x00034600 File Offset: 0x00032800
		// (set) Token: 0x06000F4E RID: 3918 RVA: 0x00034608 File Offset: 0x00032808
		public long ErrorEventsKitId { get; set; }

		// Token: 0x17000234 RID: 564
		// (get) Token: 0x06000F4F RID: 3919 RVA: 0x00034611 File Offset: 0x00032811
		// (set) Token: 0x06000F50 RID: 3920 RVA: 0x00034619 File Offset: 0x00032819
		public string ErrorEventsKitName { get; set; }

		// Token: 0x06000F51 RID: 3921 RVA: 0x0001A112 File Offset: 0x00018312
		public string ToPrivateString()
		{
			return string.Empty;
		}

		// Token: 0x06000F52 RID: 3922 RVA: 0x0001A112 File Offset: 0x00018312
		public string ToInternalString()
		{
			return string.Empty;
		}

		// Token: 0x06000F53 RID: 3923 RVA: 0x0001A112 File Offset: 0x00018312
		public string ToOriginalString()
		{
			return string.Empty;
		}
	}
}
