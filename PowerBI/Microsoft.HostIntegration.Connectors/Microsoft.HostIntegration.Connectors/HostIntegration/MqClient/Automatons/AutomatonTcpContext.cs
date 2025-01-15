using System;
using System.Net.Security;
using System.Net.Sockets;
using System.Threading;
using Microsoft.HostIntegration.Automaton;
using Microsoft.HostIntegration.Tracing.MqClient;

namespace Microsoft.HostIntegration.MqClient.Automatons
{
	// Token: 0x02000ACD RID: 2765
	internal sealed class AutomatonTcpContext : AutomatonContext
	{
		// Token: 0x170014EF RID: 5359
		// (get) Token: 0x060057BD RID: 22461 RVA: 0x001694FA File Offset: 0x001676FA
		// (set) Token: 0x060057BE RID: 22462 RVA: 0x00169502 File Offset: 0x00167702
		public TcpAutomatonTracePoint TracePoint { get; set; }

		// Token: 0x170014F0 RID: 5360
		// (get) Token: 0x060057BF RID: 22463 RVA: 0x0016950B File Offset: 0x0016770B
		// (set) Token: 0x060057C0 RID: 22464 RVA: 0x00169513 File Offset: 0x00167713
		public ConnectionTracePoint ParentTracePoint { get; set; }

		// Token: 0x170014F1 RID: 5361
		// (get) Token: 0x060057C1 RID: 22465 RVA: 0x0016951C File Offset: 0x0016771C
		// (set) Token: 0x060057C2 RID: 22466 RVA: 0x00169524 File Offset: 0x00167724
		public bool ConnectionFoundToQueueManager { get; set; }

		// Token: 0x170014F2 RID: 5362
		// (get) Token: 0x060057C3 RID: 22467 RVA: 0x0016952D File Offset: 0x0016772D
		// (set) Token: 0x060057C4 RID: 22468 RVA: 0x00169535 File Offset: 0x00167735
		public TcpConnectionParameters ConnectionParameters { get; set; }

		// Token: 0x170014F3 RID: 5363
		// (get) Token: 0x060057C5 RID: 22469 RVA: 0x0016953E File Offset: 0x0016773E
		// (set) Token: 0x060057C6 RID: 22470 RVA: 0x00169546 File Offset: 0x00167746
		public EventWaitHandle ConnectedEvent { get; private set; }

		// Token: 0x170014F4 RID: 5364
		// (get) Token: 0x060057C7 RID: 22471 RVA: 0x0016954F File Offset: 0x0016774F
		// (set) Token: 0x060057C8 RID: 22472 RVA: 0x00169557 File Offset: 0x00167757
		public EventWaitHandle DisconnectedEvent { get; private set; }

		// Token: 0x170014F5 RID: 5365
		// (get) Token: 0x060057C9 RID: 22473 RVA: 0x00169560 File Offset: 0x00167760
		// (set) Token: 0x060057CA RID: 22474 RVA: 0x00169568 File Offset: 0x00167768
		public ReturnCode ConnectReturnCode { get; set; }

		// Token: 0x170014F6 RID: 5366
		// (get) Token: 0x060057CB RID: 22475 RVA: 0x00169571 File Offset: 0x00167771
		// (set) Token: 0x060057CC RID: 22476 RVA: 0x00169579 File Offset: 0x00167779
		public ReturnCode DisconnectReturnCode { get; set; }

		// Token: 0x170014F7 RID: 5367
		// (get) Token: 0x060057CD RID: 22477 RVA: 0x00169582 File Offset: 0x00167782
		// (set) Token: 0x060057CE RID: 22478 RVA: 0x0016958A File Offset: 0x0016778A
		public SslStream SslStream { get; set; }

		// Token: 0x170014F8 RID: 5368
		// (get) Token: 0x060057CF RID: 22479 RVA: 0x00169593 File Offset: 0x00167793
		// (set) Token: 0x060057D0 RID: 22480 RVA: 0x0016959B File Offset: 0x0016779B
		public TcpClient TcpClient { get; set; }

		// Token: 0x170014F9 RID: 5369
		// (get) Token: 0x060057D1 RID: 22481 RVA: 0x001695A4 File Offset: 0x001677A4
		// (set) Token: 0x060057D2 RID: 22482 RVA: 0x001695AC File Offset: 0x001677AC
		public int ZeroByteReads { get; set; }

		// Token: 0x170014FA RID: 5370
		// (get) Token: 0x060057D3 RID: 22483 RVA: 0x001695B5 File Offset: 0x001677B5
		// (set) Token: 0x060057D4 RID: 22484 RVA: 0x001695BD File Offset: 0x001677BD
		public AsynchronousConnectionMessage Attach { get; set; }

		// Token: 0x170014FB RID: 5371
		// (get) Token: 0x060057D5 RID: 22485 RVA: 0x001695C6 File Offset: 0x001677C6
		// (set) Token: 0x060057D6 RID: 22486 RVA: 0x001695CE File Offset: 0x001677CE
		public AsynchronousConnectionMessage Detach { get; set; }

		// Token: 0x170014FC RID: 5372
		// (get) Token: 0x060057D7 RID: 22487 RVA: 0x001695D7 File Offset: 0x001677D7
		// (set) Token: 0x060057D8 RID: 22488 RVA: 0x001695DF File Offset: 0x001677DF
		public ChannelParameters ChannelParameters { get; set; }

		// Token: 0x170014FD RID: 5373
		// (get) Token: 0x060057D9 RID: 22489 RVA: 0x001695E8 File Offset: 0x001677E8
		// (set) Token: 0x060057DA RID: 22490 RVA: 0x001695F0 File Offset: 0x001677F0
		public Exception TcpConnectException { get; set; }

		// Token: 0x170014FE RID: 5374
		// (get) Token: 0x060057DB RID: 22491 RVA: 0x001695F9 File Offset: 0x001677F9
		// (set) Token: 0x060057DC RID: 22492 RVA: 0x00169601 File Offset: 0x00167801
		public Exception TcpSendException { get; set; }

		// Token: 0x170014FF RID: 5375
		// (get) Token: 0x060057DD RID: 22493 RVA: 0x0016960A File Offset: 0x0016780A
		// (set) Token: 0x060057DE RID: 22494 RVA: 0x00169612 File Offset: 0x00167812
		public Exception TcpReceiveException { get; set; }

		// Token: 0x17001500 RID: 5376
		// (get) Token: 0x060057DF RID: 22495 RVA: 0x0016961B File Offset: 0x0016781B
		// (set) Token: 0x060057E0 RID: 22496 RVA: 0x00169623 File Offset: 0x00167823
		public byte[] EightBytes { get; private set; }

		// Token: 0x17001501 RID: 5377
		// (get) Token: 0x060057E1 RID: 22497 RVA: 0x0016962C File Offset: 0x0016782C
		// (set) Token: 0x060057E2 RID: 22498 RVA: 0x00169634 File Offset: 0x00167834
		public int NumberOfBytesRead { get; set; }

		// Token: 0x17001502 RID: 5378
		// (get) Token: 0x060057E3 RID: 22499 RVA: 0x0016963D File Offset: 0x0016783D
		// (set) Token: 0x060057E4 RID: 22500 RVA: 0x00169645 File Offset: 0x00167845
		public DynamicDataBuffer DataToQmAutomaton { get; set; }

		// Token: 0x17001503 RID: 5379
		// (get) Token: 0x060057E5 RID: 22501 RVA: 0x0016964E File Offset: 0x0016784E
		// (set) Token: 0x060057E6 RID: 22502 RVA: 0x00169656 File Offset: 0x00167856
		public AsynchronousConnectionMessage MessageFromQmAutomaton { get; set; }

		// Token: 0x17001504 RID: 5380
		// (get) Token: 0x060057E7 RID: 22503 RVA: 0x0016965F File Offset: 0x0016785F
		// (set) Token: 0x060057E8 RID: 22504 RVA: 0x00169667 File Offset: 0x00167867
		public IAsyncResult AsynchronousResult { get; set; }

		// Token: 0x17001505 RID: 5381
		// (get) Token: 0x060057E9 RID: 22505 RVA: 0x00169670 File Offset: 0x00167870
		// (set) Token: 0x060057EA RID: 22506 RVA: 0x00169678 File Offset: 0x00167878
		public ChannelQueueManager ChannelQueueManager { get; set; }

		// Token: 0x17001506 RID: 5382
		// (get) Token: 0x060057EB RID: 22507 RVA: 0x00169681 File Offset: 0x00167881
		// (set) Token: 0x060057EC RID: 22508 RVA: 0x00169689 File Offset: 0x00167889
		public Timer SendTimer { get; set; }

		// Token: 0x17001507 RID: 5383
		// (get) Token: 0x060057ED RID: 22509 RVA: 0x00169692 File Offset: 0x00167892
		// (set) Token: 0x060057EE RID: 22510 RVA: 0x0016969A File Offset: 0x0016789A
		public Timer ReceiveTimer { get; set; }

		// Token: 0x17001508 RID: 5384
		// (get) Token: 0x060057EF RID: 22511 RVA: 0x001696A3 File Offset: 0x001678A3
		// (set) Token: 0x060057F0 RID: 22512 RVA: 0x001696AB File Offset: 0x001678AB
		public DateTime LastSentTime { get; set; }

		// Token: 0x17001509 RID: 5385
		// (get) Token: 0x060057F1 RID: 22513 RVA: 0x001696B4 File Offset: 0x001678B4
		// (set) Token: 0x060057F2 RID: 22514 RVA: 0x001696BC File Offset: 0x001678BC
		public DateTime LastReceivedTime { get; set; }

		// Token: 0x1700150A RID: 5386
		// (get) Token: 0x060057F3 RID: 22515 RVA: 0x001696C5 File Offset: 0x001678C5
		// (set) Token: 0x060057F4 RID: 22516 RVA: 0x001696CD File Offset: 0x001678CD
		public bool In60Seconds { get; set; }

		// Token: 0x1700150B RID: 5387
		// (get) Token: 0x060057F5 RID: 22517 RVA: 0x001696D6 File Offset: 0x001678D6
		// (set) Token: 0x060057F6 RID: 22518 RVA: 0x001696DE File Offset: 0x001678DE
		public TimeSpan HeartbeatTimespan { get; set; }

		// Token: 0x1700150C RID: 5388
		// (get) Token: 0x060057F7 RID: 22519 RVA: 0x001696E7 File Offset: 0x001678E7
		// (set) Token: 0x060057F8 RID: 22520 RVA: 0x001696EF File Offset: 0x001678EF
		public TimeSpan SixtySeconds { get; private set; }

		// Token: 0x1700150D RID: 5389
		// (get) Token: 0x060057F9 RID: 22521 RVA: 0x001696F8 File Offset: 0x001678F8
		// (set) Token: 0x060057FA RID: 22522 RVA: 0x00169700 File Offset: 0x00167900
		public DynamicDataBuffer HeartbeatBuffer { get; private set; }

		// Token: 0x060057FB RID: 22523 RVA: 0x0016970C File Offset: 0x0016790C
		public AutomatonTcpContext()
		{
			this.EightBytes = new byte[8];
			this.ConnectedEvent = new EventWaitHandle(false, EventResetMode.AutoReset);
			this.DisconnectedEvent = new EventWaitHandle(false, EventResetMode.AutoReset);
			this.SixtySeconds = new TimeSpan(0, 1, 0);
			this.HeartbeatBuffer = new DynamicDataBuffer();
			SegmentHelpers.GenerateHeartbeat(this.HeartbeatBuffer);
		}
	}
}
