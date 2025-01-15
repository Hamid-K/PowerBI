using System;

namespace Microsoft.HostIntegration.MqClient.Automatons
{
	// Token: 0x02000ADA RID: 2778
	internal class Constants
	{
		// Token: 0x040044CB RID: 17611
		internal const int OpenOptionOutput = 16;

		// Token: 0x040044CC RID: 17612
		public const byte Faplevel = 12;

		// Token: 0x040044CD RID: 17613
		public const short MaximumMessageBatch = 50;

		// Token: 0x040044CE RID: 17614
		public const int MaximumSegmentSize = 32758;

		// Token: 0x040044CF RID: 17615
		public const int MaximumMessageSize = 104857600;

		// Token: 0x040044D0 RID: 17616
		public const int SequenceNumberWrap = 999999999;

		// Token: 0x040044D1 RID: 17617
		public const int HeartBeatInterval = 300;

		// Token: 0x040044D2 RID: 17618
		public const int ConversationsPerSocket = 10;

		// Token: 0x040044D3 RID: 17619
		public const int BufferAllocationSize = 32768;

		// Token: 0x040044D4 RID: 17620
		public const byte ByteOrderLittleEndian = 2;

		// Token: 0x040044D5 RID: 17621
		public const int MaximumLengthChannelName = 20;

		// Token: 0x040044D6 RID: 17622
		public const int MaximumLengthQueueManagerName = 48;

		// Token: 0x040044D7 RID: 17623
		public const int MaximumLengthQueueName = 48;

		// Token: 0x040044D8 RID: 17624
		public const int ProductIdLength = 12;

		// Token: 0x040044D9 RID: 17625
		public const int MaximumLengthUserId = 12;

		// Token: 0x040044DA RID: 17626
		public const int MaximumLengthPassword = 12;

		// Token: 0x040044DB RID: 17627
		public const int MaximumLengthLongUserId = 64;

		// Token: 0x040044DC RID: 17628
		public const int MaximumLengthApplicationName = 28;

		// Token: 0x040044DD RID: 17629
		public const int MaximumLengthMqmId = 48;

		// Token: 0x040044DE RID: 17630
		public const int MaximumLengthObjectName = 48;

		// Token: 0x040044DF RID: 17631
		public const int MaximumLengthObjectQueueManager = 48;

		// Token: 0x040044E0 RID: 17632
		public const int MaximumLengthDynamicQueueName = 48;

		// Token: 0x040044E1 RID: 17633
		public const int MaximumLengthMessageFormat = 8;

		// Token: 0x040044E2 RID: 17634
		public const int MaximumLengthMessageIdFConnect = 152;

		// Token: 0x040044E3 RID: 17635
		public const int MaximumLengthMessageReplyToQueue = 48;

		// Token: 0x040044E4 RID: 17636
		public const int MaximumLengthMessageReplyToQueueManager = 48;

		// Token: 0x040044E5 RID: 17637
		public const int MaximumLengthMessageApplicationIdData = 32;

		// Token: 0x040044E6 RID: 17638
		public const string ProductName = "MS HIS V11.0";

		// Token: 0x040044E7 RID: 17639
		public const int TshAscii = 541610836;

		// Token: 0x040044E8 RID: 17640
		public const int TshEbcdic = 1086907107;

		// Token: 0x040044E9 RID: 17641
		public const int TshmAscii = 1296585556;

		// Token: 0x040044EA RID: 17642
		public const int TshmEbcdic = -725032221;

		// Token: 0x040044EB RID: 17643
		public const int TshcAscii = 1128813396;

		// Token: 0x040044EC RID: 17644
		public const int TshcEbcdic = -1010244893;

		// Token: 0x040044ED RID: 17645
		public const int InitialDataAscii = 538985545;

		// Token: 0x040044EE RID: 17646
		public const int InitialDataEbcdic = 1077986505;

		// Token: 0x040044EF RID: 17647
		public const int UserIdAscii = 541346133;

		// Token: 0x040044F0 RID: 17648
		public const int ConAuthAscii = 1414873411;

		// Token: 0x040044F1 RID: 17649
		public const int FConnectOptionAscii = 1330529094;

		// Token: 0x040044F2 RID: 17650
		public const int ObjectDescriptorAscii = 538985551;

		// Token: 0x040044F3 RID: 17651
		public const int FopaAscii = 1095782214;

		// Token: 0x040044F4 RID: 17652
		public const int MqStatAscii = 1413567571;

		// Token: 0x040044F5 RID: 17653
		public const int LengthOfTsh = 28;

		// Token: 0x040044F6 RID: 17654
		public const int LengthOfTshm = 36;

		// Token: 0x040044F7 RID: 17655
		public const int LengthOfTshc = 28;

		// Token: 0x040044F8 RID: 17656
		public const int LengthOfFirstInitialData = 208;

		// Token: 0x040044F9 RID: 17657
		public const int LengthOfSidLessUserId = 104;

		// Token: 0x040044FA RID: 17658
		public const int LengthOfFixedConAuth = 24;

		// Token: 0x040044FB RID: 17659
		public const int LengthOfApiHeader = 16;

		// Token: 0x040044FC RID: 17660
		public const int LengthOfMqConnect = 332;

		// Token: 0x040044FD RID: 17661
		public const int LengthOfObjectDescriptor = 400;

		// Token: 0x040044FE RID: 17662
		public const int LengthOfOpenOptions = 4;

		// Token: 0x040044FF RID: 17663
		public const int ApiLengthOfOpenResponse = 1500;

		// Token: 0x04004500 RID: 17664
		public const int LengthOfCloseOptions = 4;

		// Token: 0x04004501 RID: 17665
		public const int LengthOfFopa = 28;

		// Token: 0x04004502 RID: 17666
		public const int LengthOfMessageDescriptor = 364;

		// Token: 0x04004503 RID: 17667
		public const int LengthOfPutMessageOptions = 128;

		// Token: 0x04004504 RID: 17668
		public const int LengthOfGetMessageOptions = 72;

		// Token: 0x04004505 RID: 17669
		public const int LengthOfMqPutGet = 4;

		// Token: 0x04004506 RID: 17670
		public const int MinimumLengthOfFirstRequestMessage = 64;

		// Token: 0x04004507 RID: 17671
		public const int LengthOfSecondRequestMessage = 40;

		// Token: 0x04004508 RID: 17672
		public const int LengthOfSocketAction = 20;

		// Token: 0x04004509 RID: 17673
		public const int LengthOfMqStat = 228;

		// Token: 0x0400450A RID: 17674
		public const int GlobalMessageIndexOffsetFromTshm = 12;

		// Token: 0x0400450B RID: 17675
		public const int StatusDataTypeOffsetFromTshm = 4;

		// Token: 0x0400450C RID: 17676
		public const int ObjectHandleOffsetFromTshmInAsyncNotification = 4;

		// Token: 0x0400450D RID: 17677
		public const int LengthOfXaOpen = 16;

		// Token: 0x0400450E RID: 17678
		public const int LengthOfXaClose = 16;

		// Token: 0x0400450F RID: 17679
		public const int LengthOfXaStart = 16;

		// Token: 0x04004510 RID: 17680
		public const int LengthOfXaEnd = 16;

		// Token: 0x04004511 RID: 17681
		public const int LengthOfXaPrepare = 16;

		// Token: 0x04004512 RID: 17682
		public const int LengthOfXaCommit = 16;

		// Token: 0x04004513 RID: 17683
		public const int LengthOfXaRollback = 16;

		// Token: 0x04004514 RID: 17684
		public const int LengthOfXaForget = 16;

		// Token: 0x04004515 RID: 17685
		public const int LengthOfXaRecover = 20;

		// Token: 0x04004516 RID: 17686
		public const int MinimumLengthOfUidSid = 28;
	}
}
