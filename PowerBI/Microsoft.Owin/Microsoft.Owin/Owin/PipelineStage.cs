using System;

namespace Owin
{
	// Token: 0x02000003 RID: 3
	public enum PipelineStage
	{
		// Token: 0x04000002 RID: 2
		Authenticate,
		// Token: 0x04000003 RID: 3
		PostAuthenticate,
		// Token: 0x04000004 RID: 4
		Authorize,
		// Token: 0x04000005 RID: 5
		PostAuthorize,
		// Token: 0x04000006 RID: 6
		ResolveCache,
		// Token: 0x04000007 RID: 7
		PostResolveCache,
		// Token: 0x04000008 RID: 8
		MapHandler,
		// Token: 0x04000009 RID: 9
		PostMapHandler,
		// Token: 0x0400000A RID: 10
		AcquireState,
		// Token: 0x0400000B RID: 11
		PostAcquireState,
		// Token: 0x0400000C RID: 12
		PreHandlerExecute
	}
}
