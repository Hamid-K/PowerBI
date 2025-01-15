using System;

namespace Microsoft.Cis.Eventing
{
	// Token: 0x02000489 RID: 1161
	public class MessageEvent : RDEventBase
	{
		// Token: 0x0600285D RID: 10333 RVA: 0x00078F11 File Offset: 0x00077111
		public MessageEvent()
		{
		}

		// Token: 0x0600285E RID: 10334 RVA: 0x00079AF7 File Offset: 0x00077CF7
		public MessageEvent(string message)
		{
			this.Message = message;
		}

		// Token: 0x17000803 RID: 2051
		// (get) Token: 0x0600285F RID: 10335 RVA: 0x00079B06 File Offset: 0x00077D06
		// (set) Token: 0x06002860 RID: 10336 RVA: 0x00079B0E File Offset: 0x00077D0E
		[RDEventProperty]
		public string Message { get; set; }
	}
}
