using System;

namespace Microsoft.AnalysisServices
{
	// Token: 0x0200001E RID: 30
	[Serializable]
	public enum ConnectionExceptionCause
	{
		// Token: 0x04000094 RID: 148
		Unspecified,
		// Token: 0x04000095 RID: 149
		[Obsolete("Deprecated; not used anymore!")]
		IncompatibleVersion,
		// Token: 0x04000096 RID: 150
		AuthenticationFailed,
		// Token: 0x04000097 RID: 151
		ServerHasMoved,
		// Token: 0x04000098 RID: 152
		LinkReferenceResolutionFailed,
		// Token: 0x04000099 RID: 153
		DataStreamingInterrupted,
		// Token: 0x0400009A RID: 154
		ConnectionNotOpen,
		// Token: 0x0400009B RID: 155
		InvalidSessionId,
		// Token: 0x0400009C RID: 156
		TransportProtocolError,
		// Token: 0x0400009D RID: 157
		ServerPausedOrScaling,
		// Token: 0x0400009E RID: 158
		ServerNotReady,
		// Token: 0x0400009F RID: 159
		XmlaEndpointDisabled,
		// Token: 0x040000A0 RID: 160
		ServerNotFoundInConnectionMode
	}
}
