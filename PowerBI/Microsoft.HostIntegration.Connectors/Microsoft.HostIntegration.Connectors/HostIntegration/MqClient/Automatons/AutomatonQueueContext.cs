using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.HostIntegration.Automaton;
using Microsoft.HostIntegration.Tracing.MqClient;

namespace Microsoft.HostIntegration.MqClient.Automatons
{
	// Token: 0x02000A8F RID: 2703
	internal sealed class AutomatonQueueContext : AutomatonContext
	{
		// Token: 0x1700144F RID: 5199
		// (get) Token: 0x060053A8 RID: 21416 RVA: 0x00155322 File Offset: 0x00153522
		// (set) Token: 0x060053A9 RID: 21417 RVA: 0x0015532A File Offset: 0x0015352A
		public QAutomatonTracePoint TracePoint { get; set; }

		// Token: 0x17001450 RID: 5200
		// (get) Token: 0x060053AA RID: 21418 RVA: 0x00155333 File Offset: 0x00153533
		// (set) Token: 0x060053AB RID: 21419 RVA: 0x0015533B File Offset: 0x0015353B
		public QueueTracePoint ParentTracePoint { get; set; }

		// Token: 0x17001451 RID: 5201
		// (get) Token: 0x060053AC RID: 21420 RVA: 0x00155344 File Offset: 0x00153544
		// (set) Token: 0x060053AD RID: 21421 RVA: 0x0015534C File Offset: 0x0015354C
		public int DeterminantForQueueManager { get; set; }

		// Token: 0x17001452 RID: 5202
		// (get) Token: 0x060053AE RID: 21422 RVA: 0x00155355 File Offset: 0x00153555
		// (set) Token: 0x060053AF RID: 21423 RVA: 0x0015535D File Offset: 0x0015355D
		public AutomatonQueueManager AutomatonQueueManager { get; set; }

		// Token: 0x17001453 RID: 5203
		// (get) Token: 0x060053B0 RID: 21424 RVA: 0x00155366 File Offset: 0x00153566
		// (set) Token: 0x060053B1 RID: 21425 RVA: 0x0015536E File Offset: 0x0015356E
		public bool IsTransactional { get; set; }

		// Token: 0x17001454 RID: 5204
		// (get) Token: 0x060053B2 RID: 21426 RVA: 0x00155377 File Offset: 0x00153577
		// (set) Token: 0x060053B3 RID: 21427 RVA: 0x0015537F File Offset: 0x0015357F
		public int QueueManagerConversationId { get; set; }

		// Token: 0x17001455 RID: 5205
		// (get) Token: 0x060053B4 RID: 21428 RVA: 0x00155388 File Offset: 0x00153588
		// (set) Token: 0x060053B5 RID: 21429 RVA: 0x00155390 File Offset: 0x00153590
		public int UsedMaximumTransmissionSize { get; set; }

		// Token: 0x17001456 RID: 5206
		// (get) Token: 0x060053B6 RID: 21430 RVA: 0x00155399 File Offset: 0x00153599
		// (set) Token: 0x060053B7 RID: 21431 RVA: 0x001553A1 File Offset: 0x001535A1
		public int MaximumMessageSize { get; set; }

		// Token: 0x17001457 RID: 5207
		// (get) Token: 0x060053B8 RID: 21432 RVA: 0x001553AA File Offset: 0x001535AA
		// (set) Token: 0x060053B9 RID: 21433 RVA: 0x001553B2 File Offset: 0x001535B2
		public QueueConnectionParameters ConnectionParameters { get; set; }

		// Token: 0x17001458 RID: 5208
		// (get) Token: 0x060053BA RID: 21434 RVA: 0x001553BB File Offset: 0x001535BB
		// (set) Token: 0x060053BB RID: 21435 RVA: 0x001553C3 File Offset: 0x001535C3
		public EventWaitHandle OpenedEvent { get; private set; }

		// Token: 0x17001459 RID: 5209
		// (get) Token: 0x060053BC RID: 21436 RVA: 0x001553CC File Offset: 0x001535CC
		// (set) Token: 0x060053BD RID: 21437 RVA: 0x001553D4 File Offset: 0x001535D4
		public EventWaitHandle ClosedEvent { get; private set; }

		// Token: 0x1700145A RID: 5210
		// (get) Token: 0x060053BE RID: 21438 RVA: 0x001553DD File Offset: 0x001535DD
		// (set) Token: 0x060053BF RID: 21439 RVA: 0x001553E5 File Offset: 0x001535E5
		public EventWaitHandle SentEvent { get; private set; }

		// Token: 0x1700145B RID: 5211
		// (get) Token: 0x060053C0 RID: 21440 RVA: 0x001553EE File Offset: 0x001535EE
		// (set) Token: 0x060053C1 RID: 21441 RVA: 0x001553F6 File Offset: 0x001535F6
		public EventWaitHandle ReceivedEvent { get; private set; }

		// Token: 0x1700145C RID: 5212
		// (get) Token: 0x060053C2 RID: 21442 RVA: 0x001553FF File Offset: 0x001535FF
		// (set) Token: 0x060053C3 RID: 21443 RVA: 0x00155407 File Offset: 0x00153607
		public ReturnCode OpenReturnCode { get; set; }

		// Token: 0x1700145D RID: 5213
		// (get) Token: 0x060053C4 RID: 21444 RVA: 0x00155410 File Offset: 0x00153610
		// (set) Token: 0x060053C5 RID: 21445 RVA: 0x00155418 File Offset: 0x00153618
		public ReturnCode CloseReturnCode { get; set; }

		// Token: 0x1700145E RID: 5214
		// (get) Token: 0x060053C6 RID: 21446 RVA: 0x00155421 File Offset: 0x00153621
		// (set) Token: 0x060053C7 RID: 21447 RVA: 0x00155429 File Offset: 0x00153629
		public ReturnCode SendReturnCode { get; set; }

		// Token: 0x1700145F RID: 5215
		// (get) Token: 0x060053C8 RID: 21448 RVA: 0x00155432 File Offset: 0x00153632
		// (set) Token: 0x060053C9 RID: 21449 RVA: 0x0015543A File Offset: 0x0015363A
		public ReturnCode ReceiveReturnCode { get; set; }

		// Token: 0x17001460 RID: 5216
		// (get) Token: 0x060053CA RID: 21450 RVA: 0x00155443 File Offset: 0x00153643
		// (set) Token: 0x060053CB RID: 21451 RVA: 0x0015544B File Offset: 0x0015364B
		public OpenOption OpenOptions { get; set; }

		// Token: 0x17001461 RID: 5217
		// (get) Token: 0x060053CC RID: 21452 RVA: 0x00155454 File Offset: 0x00153654
		// (set) Token: 0x060053CD RID: 21453 RVA: 0x0015545C File Offset: 0x0015365C
		public bool OpenedWithReadAhead { get; set; }

		// Token: 0x17001462 RID: 5218
		// (get) Token: 0x060053CE RID: 21454 RVA: 0x00155465 File Offset: 0x00153665
		// (set) Token: 0x060053CF RID: 21455 RVA: 0x0015546D File Offset: 0x0015366D
		public string DynamicQueueNamePrefix { get; set; }

		// Token: 0x17001463 RID: 5219
		// (get) Token: 0x060053D0 RID: 21456 RVA: 0x00155476 File Offset: 0x00153676
		// (set) Token: 0x060053D1 RID: 21457 RVA: 0x0015547E File Offset: 0x0015367E
		public string ResolvedQueueName { get; set; }

		// Token: 0x17001464 RID: 5220
		// (get) Token: 0x060053D2 RID: 21458 RVA: 0x00155487 File Offset: 0x00153687
		// (set) Token: 0x060053D3 RID: 21459 RVA: 0x0015548F File Offset: 0x0015368F
		public Timer ReceiveTimer { get; set; }

		// Token: 0x17001465 RID: 5221
		// (get) Token: 0x060053D4 RID: 21460 RVA: 0x00155498 File Offset: 0x00153698
		// (set) Token: 0x060053D5 RID: 21461 RVA: 0x001554A0 File Offset: 0x001536A0
		public AsynchronousConnectionMessage MessageFromQueueManager { get; set; }

		// Token: 0x17001466 RID: 5222
		// (get) Token: 0x060053D6 RID: 21462 RVA: 0x001554A9 File Offset: 0x001536A9
		// (set) Token: 0x060053D7 RID: 21463 RVA: 0x001554B1 File Offset: 0x001536B1
		public AsyncMessageStatus CurrentAsyncMessageStatus { get; set; }

		// Token: 0x17001467 RID: 5223
		// (get) Token: 0x060053D8 RID: 21464 RVA: 0x001554BA File Offset: 0x001536BA
		// (set) Token: 0x060053D9 RID: 21465 RVA: 0x001554C2 File Offset: 0x001536C2
		public Queue<PassThroughData> SegmentsReceived { get; private set; }

		// Token: 0x17001468 RID: 5224
		// (get) Token: 0x060053DA RID: 21466 RVA: 0x001554CB File Offset: 0x001536CB
		// (set) Token: 0x060053DB RID: 21467 RVA: 0x001554D3 File Offset: 0x001536D3
		public int AsynchronouslyReceivedBytes { get; set; }

		// Token: 0x17001469 RID: 5225
		// (get) Token: 0x060053DC RID: 21468 RVA: 0x001554DC File Offset: 0x001536DC
		// (set) Token: 0x060053DD RID: 21469 RVA: 0x001554E4 File Offset: 0x001536E4
		public int BytesToReceiveAsynchronously { get; set; }

		// Token: 0x1700146A RID: 5226
		// (get) Token: 0x060053DE RID: 21470 RVA: 0x001554ED File Offset: 0x001536ED
		// (set) Token: 0x060053DF RID: 21471 RVA: 0x001554F5 File Offset: 0x001536F5
		public bool FollowUpRequest { get; set; }

		// Token: 0x1700146B RID: 5227
		// (get) Token: 0x060053E0 RID: 21472 RVA: 0x001554FE File Offset: 0x001536FE
		// (set) Token: 0x060053E1 RID: 21473 RVA: 0x00155506 File Offset: 0x00153706
		public Queue<ReceiveMessage> MessagesReceived { get; private set; }

		// Token: 0x1700146C RID: 5228
		// (get) Token: 0x060053E2 RID: 21474 RVA: 0x0015550F File Offset: 0x0015370F
		// (set) Token: 0x060053E3 RID: 21475 RVA: 0x00155517 File Offset: 0x00153717
		public NotificationInformation Notification { get; set; }

		// Token: 0x1700146D RID: 5229
		// (get) Token: 0x060053E4 RID: 21476 RVA: 0x00155520 File Offset: 0x00153720
		// (set) Token: 0x060053E5 RID: 21477 RVA: 0x00155528 File Offset: 0x00153728
		public SendMessage ClientData { get; set; }

		// Token: 0x1700146E RID: 5230
		// (get) Token: 0x060053E6 RID: 21478 RVA: 0x00155531 File Offset: 0x00153731
		// (set) Token: 0x060053E7 RID: 21479 RVA: 0x00155539 File Offset: 0x00153739
		public ReceiveMessage DataForClient { get; set; }

		// Token: 0x1700146F RID: 5231
		// (get) Token: 0x060053E8 RID: 21480 RVA: 0x00155542 File Offset: 0x00153742
		// (set) Token: 0x060053E9 RID: 21481 RVA: 0x0015554A File Offset: 0x0015374A
		public bool OriginalReceiveOptionsIncluded { get; set; }

		// Token: 0x17001470 RID: 5232
		// (get) Token: 0x060053EA RID: 21482 RVA: 0x00155553 File Offset: 0x00153753
		// (set) Token: 0x060053EB RID: 21483 RVA: 0x0015555B File Offset: 0x0015375B
		public ReceiveOptions ReceiveOptions { get; set; }

		// Token: 0x17001471 RID: 5233
		// (get) Token: 0x060053EC RID: 21484 RVA: 0x00155564 File Offset: 0x00153764
		// (set) Token: 0x060053ED RID: 21485 RVA: 0x0015556C File Offset: 0x0015376C
		public bool CloseCalled { get; set; }

		// Token: 0x17001472 RID: 5234
		// (get) Token: 0x060053EE RID: 21486 RVA: 0x00155575 File Offset: 0x00153775
		// (set) Token: 0x060053EF RID: 21487 RVA: 0x0015557D File Offset: 0x0015377D
		public bool PutPerformed { get; set; }

		// Token: 0x17001473 RID: 5235
		// (get) Token: 0x060053F0 RID: 21488 RVA: 0x00155586 File Offset: 0x00153786
		// (set) Token: 0x060053F1 RID: 21489 RVA: 0x0015558E File Offset: 0x0015378E
		public bool GetPerformed { get; set; }

		// Token: 0x17001474 RID: 5236
		// (get) Token: 0x060053F2 RID: 21490 RVA: 0x00155597 File Offset: 0x00153797
		// (set) Token: 0x060053F3 RID: 21491 RVA: 0x0015559F File Offset: 0x0015379F
		public bool InFailedState { get; set; }

		// Token: 0x060053F4 RID: 21492 RVA: 0x001555A8 File Offset: 0x001537A8
		public AutomatonQueueContext()
		{
			this.OpenedEvent = new EventWaitHandle(false, EventResetMode.AutoReset);
			this.ClosedEvent = new EventWaitHandle(false, EventResetMode.AutoReset);
			this.SentEvent = new EventWaitHandle(false, EventResetMode.AutoReset);
			this.ReceivedEvent = new EventWaitHandle(false, EventResetMode.AutoReset);
			this.SegmentsReceived = new Queue<PassThroughData>();
			this.MessagesReceived = new Queue<ReceiveMessage>();
			this.BytesToReceiveAsynchronously = 102400;
		}
	}
}
