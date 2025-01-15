using System;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.EventsKit
{
	// Token: 0x02000357 RID: 855
	public class FlattenedMonitoredError : IMonitoredError, IContainsPrivateInformation
	{
		// Token: 0x0600194B RID: 6475 RVA: 0x0000E568 File Offset: 0x0000C768
		public bool IsFatal()
		{
			return false;
		}

		// Token: 0x0600194C RID: 6476 RVA: 0x0000E568 File Offset: 0x0000C768
		public bool IsBenign()
		{
			return false;
		}

		// Token: 0x0600194D RID: 6477 RVA: 0x0000E568 File Offset: 0x0000C768
		public bool IsPermanent()
		{
			return false;
		}

		// Token: 0x1700036B RID: 875
		// (get) Token: 0x0600194E RID: 6478 RVA: 0x0000E56B File Offset: 0x0000C76B
		public string ErrorShortName
		{
			get
			{
				return base.GetType().Name;
			}
		}

		// Token: 0x1700036C RID: 876
		// (get) Token: 0x0600194F RID: 6479 RVA: 0x0005E1E4 File Offset: 0x0005C3E4
		// (set) Token: 0x06001950 RID: 6480 RVA: 0x0005E1EC File Offset: 0x0005C3EC
		public ErrorCorrelationId ErrorCorrelationId { get; private set; }

		// Token: 0x1700036D RID: 877
		// (get) Token: 0x06001951 RID: 6481 RVA: 0x0005E1F5 File Offset: 0x0005C3F5
		// (set) Token: 0x06001952 RID: 6482 RVA: 0x0005E1FD File Offset: 0x0005C3FD
		public MonitoringScopeId MonitoringScope { get; set; }

		// Token: 0x1700036E RID: 878
		// (get) Token: 0x06001953 RID: 6483 RVA: 0x0005E206 File Offset: 0x0005C406
		// (set) Token: 0x06001954 RID: 6484 RVA: 0x0005E20E File Offset: 0x0005C40E
		public long ErrorEventId { get; set; }

		// Token: 0x1700036F RID: 879
		// (get) Token: 0x06001955 RID: 6485 RVA: 0x0005E217 File Offset: 0x0005C417
		// (set) Token: 0x06001956 RID: 6486 RVA: 0x0005E21F File Offset: 0x0005C41F
		public string ErrorEventName { get; set; }

		// Token: 0x17000370 RID: 880
		// (get) Token: 0x06001957 RID: 6487 RVA: 0x0005E228 File Offset: 0x0005C428
		// (set) Token: 0x06001958 RID: 6488 RVA: 0x0005E230 File Offset: 0x0005C430
		public long ErrorEventsKitId { get; set; }

		// Token: 0x17000371 RID: 881
		// (get) Token: 0x06001959 RID: 6489 RVA: 0x0005E239 File Offset: 0x0005C439
		// (set) Token: 0x0600195A RID: 6490 RVA: 0x0005E241 File Offset: 0x0005C441
		public string ErrorEventsKitName { get; set; }

		// Token: 0x0600195B RID: 6491 RVA: 0x0005E24A File Offset: 0x0005C44A
		public override string ToString()
		{
			return this.m_toString;
		}

		// Token: 0x0600195C RID: 6492 RVA: 0x0000E609 File Offset: 0x0000C809
		public string ToPrivateString()
		{
			return this.ToString();
		}

		// Token: 0x0600195D RID: 6493 RVA: 0x0005E252 File Offset: 0x0005C452
		public string ToInternalString()
		{
			return this.ToOriginalString();
		}

		// Token: 0x0600195E RID: 6494 RVA: 0x0005E24A File Offset: 0x0005C44A
		public string ToOriginalString()
		{
			return this.m_toString;
		}

		// Token: 0x0600195F RID: 6495 RVA: 0x0005E25A File Offset: 0x0005C45A
		public FlattenedMonitoredError(long correlationId, int sequenceNumber, string text, string innerMessage, string monitoringScope)
		{
			this.ErrorCorrelationId = new ErrorCorrelationId(correlationId, sequenceNumber);
			this.m_toString = text;
			this.m_innerMessage = innerMessage;
			this.MonitoringScope = new MonitoringScopeId(monitoringScope);
		}

		// Token: 0x06001960 RID: 6496 RVA: 0x0005E28B File Offset: 0x0005C48B
		public string InnerMessage()
		{
			return this.m_innerMessage;
		}

		// Token: 0x040008C2 RID: 2242
		private readonly string m_toString;

		// Token: 0x040008C3 RID: 2243
		private readonly string m_innerMessage;
	}
}
