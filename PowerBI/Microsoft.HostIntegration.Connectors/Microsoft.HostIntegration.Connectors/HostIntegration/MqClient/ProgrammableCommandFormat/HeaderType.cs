using System;

namespace Microsoft.HostIntegration.MqClient.ProgrammableCommandFormat
{
	// Token: 0x02000B5E RID: 2910
	public enum HeaderType
	{
		// Token: 0x04004868 RID: 18536
		Command = 1,
		// Token: 0x04004869 RID: 18537
		ExtendedCommand = 16,
		// Token: 0x0400486A RID: 18538
		Response = 2,
		// Token: 0x0400486B RID: 18539
		ExtendedResponse = 17,
		// Token: 0x0400486C RID: 18540
		Item,
		// Token: 0x0400486D RID: 18541
		Summary,
		// Token: 0x0400486E RID: 18542
		UserDefined = 8
	}
}
