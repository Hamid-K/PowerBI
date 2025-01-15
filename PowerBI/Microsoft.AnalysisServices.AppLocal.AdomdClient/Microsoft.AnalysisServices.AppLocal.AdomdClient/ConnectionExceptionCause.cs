using System;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x0200000A RID: 10
	[Serializable]
	public enum ConnectionExceptionCause
	{
		// Token: 0x04000048 RID: 72
		Unspecified,
		// Token: 0x04000049 RID: 73
		AuthenticationFailed = 2,
		// Token: 0x0400004A RID: 74
		ServerHasMoved,
		// Token: 0x0400004B RID: 75
		LinkReferenceResolutionFailed,
		// Token: 0x0400004C RID: 76
		DataStreamingInterrupted,
		// Token: 0x0400004D RID: 77
		ConnectionNotOpen,
		// Token: 0x0400004E RID: 78
		InvalidSessionId,
		// Token: 0x0400004F RID: 79
		TransportProtocolError,
		// Token: 0x04000050 RID: 80
		ServerPausedOrScaling,
		// Token: 0x04000051 RID: 81
		ServerNotReady,
		// Token: 0x04000052 RID: 82
		XmlaEndpointDisabled,
		// Token: 0x04000053 RID: 83
		ServerNotFoundInConnectionMode
	}
}
