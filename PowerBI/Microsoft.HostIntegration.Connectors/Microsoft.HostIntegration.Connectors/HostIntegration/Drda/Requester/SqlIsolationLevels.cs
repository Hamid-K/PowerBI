using System;

namespace Microsoft.HostIntegration.Drda.Requester
{
	// Token: 0x020008F8 RID: 2296
	public enum SqlIsolationLevels : ushort
	{
		// Token: 0x0400359D RID: 13725
		ReadUncommitted = 9281,
		// Token: 0x0400359E RID: 13726
		ReadCommitted,
		// Token: 0x0400359F RID: 13727
		RepeatableRead,
		// Token: 0x040035A0 RID: 13728
		Serializable
	}
}
