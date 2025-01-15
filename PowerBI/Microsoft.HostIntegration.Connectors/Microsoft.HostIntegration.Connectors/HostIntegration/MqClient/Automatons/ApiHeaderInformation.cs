using System;

namespace Microsoft.HostIntegration.MqClient.Automatons
{
	// Token: 0x02000A81 RID: 2689
	internal class ApiHeaderInformation
	{
		// Token: 0x17001445 RID: 5189
		// (get) Token: 0x06005392 RID: 21394 RVA: 0x00155278 File Offset: 0x00153478
		// (set) Token: 0x06005393 RID: 21395 RVA: 0x00155280 File Offset: 0x00153480
		public int ReplyLength { get; set; }

		// Token: 0x17001446 RID: 5190
		// (get) Token: 0x06005394 RID: 21396 RVA: 0x00155289 File Offset: 0x00153489
		// (set) Token: 0x06005395 RID: 21397 RVA: 0x00155291 File Offset: 0x00153491
		public CompletionCode CompletionCode { get; set; }

		// Token: 0x17001447 RID: 5191
		// (get) Token: 0x06005396 RID: 21398 RVA: 0x0015529A File Offset: 0x0015349A
		// (set) Token: 0x06005397 RID: 21399 RVA: 0x001552A2 File Offset: 0x001534A2
		public ReturnCode ReasonCode { get; set; }

		// Token: 0x17001448 RID: 5192
		// (get) Token: 0x06005398 RID: 21400 RVA: 0x001552AB File Offset: 0x001534AB
		// (set) Token: 0x06005399 RID: 21401 RVA: 0x001552B3 File Offset: 0x001534B3
		public int ObjectHandle { get; set; }
	}
}
