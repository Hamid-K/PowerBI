using System;

namespace Microsoft.HostIntegration.MqClient
{
	// Token: 0x02000B2C RID: 2860
	public class CustomMqClientException : ApplicationException
	{
		// Token: 0x17001598 RID: 5528
		// (get) Token: 0x06005A0D RID: 23053 RVA: 0x00173CE4 File Offset: 0x00171EE4
		// (set) Token: 0x06005A0E RID: 23054 RVA: 0x00173CEC File Offset: 0x00171EEC
		public int ReasonCode { get; private set; }

		// Token: 0x06005A0F RID: 23055 RVA: 0x00173CF5 File Offset: 0x00171EF5
		internal CustomMqClientException(string message)
			: base(message)
		{
			this.ReasonCode = -1;
		}

		// Token: 0x06005A10 RID: 23056 RVA: 0x00173D05 File Offset: 0x00171F05
		internal CustomMqClientException(string message, int reasonCode)
			: base(message)
		{
			if (reasonCode < 1000000)
			{
				this.ReasonCode = reasonCode;
				return;
			}
			this.ReasonCode = -1;
		}

		// Token: 0x06005A11 RID: 23057 RVA: 0x00173D25 File Offset: 0x00171F25
		internal CustomMqClientException(string message, Exception inner)
			: base(message, inner)
		{
			this.ReasonCode = -1;
		}

		// Token: 0x06005A12 RID: 23058 RVA: 0x00173D36 File Offset: 0x00171F36
		internal CustomMqClientException(string message, int reasonCode, Exception inner)
			: base(message, inner)
		{
			if (reasonCode < 1000000)
			{
				this.ReasonCode = reasonCode;
				return;
			}
			this.ReasonCode = -1;
		}

		// Token: 0x0400473F RID: 18239
		public const int NonWebsphereMqReasonCode = -1;
	}
}
