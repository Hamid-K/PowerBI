using System;
using System.Collections.Generic;

namespace Microsoft.HostIntegration.Drda.Common
{
	// Token: 0x0200083D RID: 2109
	public class TelemetryExceptionEvent : TelemetryEvent
	{
		// Token: 0x06004308 RID: 17160 RVA: 0x000E0BD9 File Offset: 0x000DEDD9
		public TelemetryExceptionEvent(string serverClassName, string serverProductReleaseLevel, string dataProviderName, string clientProcessName, string sqlState, string sqlCode, string reasonCode)
			: base(serverClassName, serverProductReleaseLevel, dataProviderName, clientProcessName)
		{
			this.sqlState = sqlState;
			this.sqlCode = sqlCode;
			this.reasonCode = reasonCode;
		}

		// Token: 0x06004309 RID: 17161 RVA: 0x000E0BFE File Offset: 0x000DEDFE
		public TelemetryExceptionEvent()
		{
		}

		// Token: 0x17000FF9 RID: 4089
		// (get) Token: 0x0600430A RID: 17162 RVA: 0x000E0C06 File Offset: 0x000DEE06
		public override string EventName
		{
			get
			{
				return "DrdaExceptionEvent";
			}
		}

		// Token: 0x0600430B RID: 17163 RVA: 0x000E0C10 File Offset: 0x000DEE10
		public override void SetEventProperties(IDictionary<string, string> properties)
		{
			base.SetEventProperties(properties);
			properties.Add("SqlState", string.IsNullOrEmpty(this.sqlState) ? "null" : this.sqlState);
			properties.Add("SqlCode", string.IsNullOrEmpty(this.sqlCode) ? "null" : this.sqlCode);
			properties.Add("ReasonCode", string.IsNullOrEmpty(this.reasonCode) ? "null" : this.reasonCode);
		}

		// Token: 0x04002F55 RID: 12117
		private string sqlState;

		// Token: 0x04002F56 RID: 12118
		private string sqlCode;

		// Token: 0x04002F57 RID: 12119
		private string reasonCode;
	}
}
