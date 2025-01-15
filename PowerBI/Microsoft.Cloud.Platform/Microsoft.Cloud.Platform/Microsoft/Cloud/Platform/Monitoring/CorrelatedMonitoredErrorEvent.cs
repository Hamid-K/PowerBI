using System;
using System.Text;

namespace Microsoft.Cloud.Platform.Monitoring
{
	// Token: 0x02000087 RID: 135
	internal class CorrelatedMonitoredErrorEvent : IPerElementActivityType, IWindowsEventLogId, IMonitoredEventHandlerVisitor
	{
		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x060003EB RID: 1003 RVA: 0x0000E75F File Offset: 0x0000C95F
		public MonitoredLowLevelErrorEvent LowLevelError
		{
			get
			{
				return this.m_lowLevelError;
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x060003EC RID: 1004 RVA: 0x0000E767 File Offset: 0x0000C967
		public MonitoredFlowErrorEvent FlowError
		{
			get
			{
				return this.m_flowError;
			}
		}

		// Token: 0x060003ED RID: 1005 RVA: 0x0000E76F File Offset: 0x0000C96F
		public CorrelatedMonitoredErrorEvent(MonitoredLowLevelErrorEvent lowLevelError, MonitoredFlowErrorEvent flowError)
		{
			this.m_lowLevelError = lowLevelError;
			this.m_flowError = flowError;
		}

		// Token: 0x060003EE RID: 1006 RVA: 0x0000E785 File Offset: 0x0000C985
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(this.m_lowLevelError.ToString());
			stringBuilder.AppendLine();
			stringBuilder.AppendLine();
			stringBuilder.Append(this.m_flowError.ToString());
			return stringBuilder.ToString();
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x060003EF RID: 1007 RVA: 0x000034FD File Offset: 0x000016FD
		public bool HasValidWindowsEventLogId
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x060003F0 RID: 1008 RVA: 0x0000E7C3 File Offset: 0x0000C9C3
		public PerElementActivityType PerElementActivityType
		{
			get
			{
				return this.FlowError.PerElementActivityType;
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x060003F1 RID: 1009 RVA: 0x0000E7D0 File Offset: 0x0000C9D0
		public int WindowsEventLogId
		{
			get
			{
				return this.LowLevelError.WindowsEventLogId;
			}
		}

		// Token: 0x060003F2 RID: 1010 RVA: 0x0000E7DD File Offset: 0x0000C9DD
		public void Visit(IMonitoredEventHandler eventHandler)
		{
			eventHandler.HandleCorrelatedFlowError(this);
		}

		// Token: 0x04000155 RID: 341
		private readonly MonitoredLowLevelErrorEvent m_lowLevelError;

		// Token: 0x04000156 RID: 342
		private readonly MonitoredFlowErrorEvent m_flowError;
	}
}
