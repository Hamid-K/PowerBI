using System;

namespace Microsoft.HostIntegration.MqClient.Automatons
{
	// Token: 0x02000ADC RID: 2780
	public enum SegmentType
	{
		// Token: 0x0400451D RID: 17693
		Unknown,
		// Token: 0x0400451E RID: 17694
		InitialData,
		// Token: 0x0400451F RID: 17695
		StatusData = 5,
		// Token: 0x04004520 RID: 17696
		UserId = 8,
		// Token: 0x04004521 RID: 17697
		Heartbeat,
		// Token: 0x04004522 RID: 17698
		ConAuth,
		// Token: 0x04004523 RID: 17699
		SocketAction = 12,
		// Token: 0x04004524 RID: 17700
		AsyncMessage,
		// Token: 0x04004525 RID: 17701
		RequestMessages,
		// Token: 0x04004526 RID: 17702
		Notification,
		// Token: 0x04004527 RID: 17703
		MqConnect = 129,
		// Token: 0x04004528 RID: 17704
		MqDisconnect,
		// Token: 0x04004529 RID: 17705
		MqOpen,
		// Token: 0x0400452A RID: 17706
		MqClose,
		// Token: 0x0400452B RID: 17707
		MqGet,
		// Token: 0x0400452C RID: 17708
		MqPut,
		// Token: 0x0400452D RID: 17709
		MqStat = 141,
		// Token: 0x0400452E RID: 17710
		MqConnectReply = 145,
		// Token: 0x0400452F RID: 17711
		MqDisconnectReply,
		// Token: 0x04004530 RID: 17712
		MqOpenReply,
		// Token: 0x04004531 RID: 17713
		MqCloseReply,
		// Token: 0x04004532 RID: 17714
		MqGetReply,
		// Token: 0x04004533 RID: 17715
		MqPutReply,
		// Token: 0x04004534 RID: 17716
		MqStatReply = 157,
		// Token: 0x04004535 RID: 17717
		XaStart = 161,
		// Token: 0x04004536 RID: 17718
		XaEnd,
		// Token: 0x04004537 RID: 17719
		XaOpen,
		// Token: 0x04004538 RID: 17720
		XaClose,
		// Token: 0x04004539 RID: 17721
		XaPrepare,
		// Token: 0x0400453A RID: 17722
		XaCommit,
		// Token: 0x0400453B RID: 17723
		XaRollback,
		// Token: 0x0400453C RID: 17724
		XaForget,
		// Token: 0x0400453D RID: 17725
		XaRecover,
		// Token: 0x0400453E RID: 17726
		XaStartReply = 177,
		// Token: 0x0400453F RID: 17727
		XaEndReply,
		// Token: 0x04004540 RID: 17728
		XaOpenReply,
		// Token: 0x04004541 RID: 17729
		XaCloseReply,
		// Token: 0x04004542 RID: 17730
		XaPrepareReply,
		// Token: 0x04004543 RID: 17731
		XaCommitReply,
		// Token: 0x04004544 RID: 17732
		XaRollbackReply,
		// Token: 0x04004545 RID: 17733
		XaForgetReply,
		// Token: 0x04004546 RID: 17734
		XaRecoverReply
	}
}
