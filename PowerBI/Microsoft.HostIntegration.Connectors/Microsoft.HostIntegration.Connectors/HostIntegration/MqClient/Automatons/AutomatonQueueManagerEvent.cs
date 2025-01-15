using System;

namespace Microsoft.HostIntegration.MqClient.Automatons
{
	// Token: 0x02000A94 RID: 2708
	public enum AutomatonQueueManagerEvent
	{
		// Token: 0x0400432D RID: 17197
		Stop = -1,
		// Token: 0x0400432E RID: 17198
		Connect,
		// Token: 0x0400432F RID: 17199
		TcpDisconnected,
		// Token: 0x04004330 RID: 17200
		Attached,
		// Token: 0x04004331 RID: 17201
		AttachFailed,
		// Token: 0x04004332 RID: 17202
		Start,
		// Token: 0x04004333 RID: 17203
		ServerData,
		// Token: 0x04004334 RID: 17204
		InitialData,
		// Token: 0x04004335 RID: 17205
		InitialDataInfo,
		// Token: 0x04004336 RID: 17206
		ReSendInitialData,
		// Token: 0x04004337 RID: 17207
		SendUserId,
		// Token: 0x04004338 RID: 17208
		Authorize,
		// Token: 0x04004339 RID: 17209
		Rejected,
		// Token: 0x0400433A RID: 17210
		TcpFailed,
		// Token: 0x0400433B RID: 17211
		StatusData,
		// Token: 0x0400433C RID: 17212
		QmConnError,
		// Token: 0x0400433D RID: 17213
		XaOpenError,
		// Token: 0x0400433E RID: 17214
		DetachTcp,
		// Token: 0x0400433F RID: 17215
		Detached,
		// Token: 0x04004340 RID: 17216
		Disconnected,
		// Token: 0x04004341 RID: 17217
		SendSA,
		// Token: 0x04004342 RID: 17218
		SendConnect,
		// Token: 0x04004343 RID: 17219
		ClientData,
		// Token: 0x04004344 RID: 17220
		AsyncSendDone,
		// Token: 0x04004345 RID: 17221
		MessageToQ,
		// Token: 0x04004346 RID: 17222
		QAttach,
		// Token: 0x04004347 RID: 17223
		QDetach,
		// Token: 0x04004348 RID: 17224
		Disconnect,
		// Token: 0x04004349 RID: 17225
		MqCommand,
		// Token: 0x0400434A RID: 17226
		QueueDisconnected,
		// Token: 0x0400434B RID: 17227
		Enlist,
		// Token: 0x0400434C RID: 17228
		MessageMore,
		// Token: 0x0400434D RID: 17229
		MessageClose,
		// Token: 0x0400434E RID: 17230
		StartOpen,
		// Token: 0x0400434F RID: 17231
		StartClose,
		// Token: 0x04004350 RID: 17232
		StartDisconnect,
		// Token: 0x04004351 RID: 17233
		CheckMore,
		// Token: 0x04004352 RID: 17234
		XaReturnCode,
		// Token: 0x04004353 RID: 17235
		XaCommitRc,
		// Token: 0x04004354 RID: 17236
		XaRollbackRc,
		// Token: 0x04004355 RID: 17237
		SaSent,
		// Token: 0x04004356 RID: 17238
		FirstOpen,
		// Token: 0x04004357 RID: 17239
		FirstClose,
		// Token: 0x04004358 RID: 17240
		InFailedState,
		// Token: 0x04004359 RID: 17241
		UnknownHandle,
		// Token: 0x0400435A RID: 17242
		ChannelQuiesced,
		// Token: 0x0400435B RID: 17243
		InformQueues,
		// Token: 0x0400435C RID: 17244
		RmError
	}
}
