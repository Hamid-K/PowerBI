using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Threading;
using Microsoft.HostIntegration.Automaton;
using Microsoft.HostIntegration.Tracing.MqClient;
using Microsoft.HostIntegration.XaClient;

namespace Microsoft.HostIntegration.MqClient.Automatons
{
	// Token: 0x02000A93 RID: 2707
	internal sealed class AutomatonQueueManagerContext : AutomatonContext
	{
		// Token: 0x17001475 RID: 5237
		// (get) Token: 0x06005411 RID: 21521 RVA: 0x0015601A File Offset: 0x0015421A
		// (set) Token: 0x06005412 RID: 21522 RVA: 0x00156022 File Offset: 0x00154222
		public QmAutomatonTracePoint TracePoint { get; set; }

		// Token: 0x17001476 RID: 5238
		// (get) Token: 0x06005413 RID: 21523 RVA: 0x0015602B File Offset: 0x0015422B
		// (set) Token: 0x06005414 RID: 21524 RVA: 0x00156033 File Offset: 0x00154233
		public QueueManagerTracePoint ParentTracePoint { get; set; }

		// Token: 0x17001477 RID: 5239
		// (get) Token: 0x06005415 RID: 21525 RVA: 0x0015603C File Offset: 0x0015423C
		// (set) Token: 0x06005416 RID: 21526 RVA: 0x00156044 File Offset: 0x00154244
		public int DeterminantForTcp { get; set; }

		// Token: 0x17001478 RID: 5240
		// (get) Token: 0x06005417 RID: 21527 RVA: 0x0015604D File Offset: 0x0015424D
		// (set) Token: 0x06005418 RID: 21528 RVA: 0x00156055 File Offset: 0x00154255
		public AutomatonTcp AutomatonTcp { get; set; }

		// Token: 0x17001479 RID: 5241
		// (get) Token: 0x06005419 RID: 21529 RVA: 0x0015605E File Offset: 0x0015425E
		// (set) Token: 0x0600541A RID: 21530 RVA: 0x00156066 File Offset: 0x00154266
		public bool ConnectionFoundToQueue { get; set; }

		// Token: 0x1700147A RID: 5242
		// (get) Token: 0x0600541B RID: 21531 RVA: 0x0015606F File Offset: 0x0015426F
		// (set) Token: 0x0600541C RID: 21532 RVA: 0x00156077 File Offset: 0x00154277
		public QueueManagerConnectionParameters ConnectionParameters { get; set; }

		// Token: 0x1700147B RID: 5243
		// (get) Token: 0x0600541D RID: 21533 RVA: 0x00156080 File Offset: 0x00154280
		// (set) Token: 0x0600541E RID: 21534 RVA: 0x00156088 File Offset: 0x00154288
		public int ResourceManagerId { get; set; }

		// Token: 0x1700147C RID: 5244
		// (get) Token: 0x0600541F RID: 21535 RVA: 0x00156091 File Offset: 0x00154291
		// (set) Token: 0x06005420 RID: 21536 RVA: 0x00156099 File Offset: 0x00154299
		public Xid Xid { get; set; }

		// Token: 0x1700147D RID: 5245
		// (get) Token: 0x06005421 RID: 21537 RVA: 0x001560A2 File Offset: 0x001542A2
		// (set) Token: 0x06005422 RID: 21538 RVA: 0x001560AA File Offset: 0x001542AA
		public XaReturnCode XaReturnCode { get; set; }

		// Token: 0x1700147E RID: 5246
		// (get) Token: 0x06005423 RID: 21539 RVA: 0x001560B3 File Offset: 0x001542B3
		// (set) Token: 0x06005424 RID: 21540 RVA: 0x001560BB File Offset: 0x001542BB
		public bool OverrodeUserId { get; set; }

		// Token: 0x1700147F RID: 5247
		// (get) Token: 0x06005425 RID: 21541 RVA: 0x001560C4 File Offset: 0x001542C4
		// (set) Token: 0x06005426 RID: 21542 RVA: 0x001560CC File Offset: 0x001542CC
		public EventWaitHandle ConnectedEvent { get; private set; }

		// Token: 0x17001480 RID: 5248
		// (get) Token: 0x06005427 RID: 21543 RVA: 0x001560D5 File Offset: 0x001542D5
		// (set) Token: 0x06005428 RID: 21544 RVA: 0x001560DD File Offset: 0x001542DD
		public EventWaitHandle DisconnectedEvent { get; private set; }

		// Token: 0x17001481 RID: 5249
		// (get) Token: 0x06005429 RID: 21545 RVA: 0x001560E6 File Offset: 0x001542E6
		// (set) Token: 0x0600542A RID: 21546 RVA: 0x001560EE File Offset: 0x001542EE
		public EventWaitHandle CommandEvent { get; private set; }

		// Token: 0x17001482 RID: 5250
		// (get) Token: 0x0600542B RID: 21547 RVA: 0x001560F7 File Offset: 0x001542F7
		// (set) Token: 0x0600542C RID: 21548 RVA: 0x001560FF File Offset: 0x001542FF
		public EventWaitHandle EnlistEvent { get; private set; }

		// Token: 0x17001483 RID: 5251
		// (get) Token: 0x0600542D RID: 21549 RVA: 0x00156108 File Offset: 0x00154308
		// (set) Token: 0x0600542E RID: 21550 RVA: 0x00156110 File Offset: 0x00154310
		public ReturnCode ConnectReturnCode { get; set; }

		// Token: 0x17001484 RID: 5252
		// (get) Token: 0x0600542F RID: 21551 RVA: 0x00156119 File Offset: 0x00154319
		// (set) Token: 0x06005430 RID: 21552 RVA: 0x00156121 File Offset: 0x00154321
		public ReturnCode DisconnectReturnCode { get; set; }

		// Token: 0x17001485 RID: 5253
		// (get) Token: 0x06005431 RID: 21553 RVA: 0x0015612A File Offset: 0x0015432A
		// (set) Token: 0x06005432 RID: 21554 RVA: 0x00156132 File Offset: 0x00154332
		public ReturnCode CommandReturnCode { get; set; }

		// Token: 0x17001486 RID: 5254
		// (get) Token: 0x06005433 RID: 21555 RVA: 0x0015613B File Offset: 0x0015433B
		// (set) Token: 0x06005434 RID: 21556 RVA: 0x00156143 File Offset: 0x00154343
		public ReturnCode EnlistReturnCode { get; set; }

		// Token: 0x17001487 RID: 5255
		// (get) Token: 0x06005435 RID: 21557 RVA: 0x0015614C File Offset: 0x0015434C
		// (set) Token: 0x06005436 RID: 21558 RVA: 0x00156154 File Offset: 0x00154354
		public AsynchronousConnectionMessage DataMessageFromTcp { get; set; }

		// Token: 0x17001488 RID: 5256
		// (get) Token: 0x06005437 RID: 21559 RVA: 0x0015615D File Offset: 0x0015435D
		// (set) Token: 0x06005438 RID: 21560 RVA: 0x00156165 File Offset: 0x00154365
		public int LastGetReplyObjectHandle { get; set; }

		// Token: 0x17001489 RID: 5257
		// (get) Token: 0x06005439 RID: 21561 RVA: 0x0015616E File Offset: 0x0015436E
		// (set) Token: 0x0600543A RID: 21562 RVA: 0x00156176 File Offset: 0x00154376
		public int GlobalMessageIndex { get; set; }

		// Token: 0x1700148A RID: 5258
		// (get) Token: 0x0600543B RID: 21563 RVA: 0x0015617F File Offset: 0x0015437F
		// (set) Token: 0x0600543C RID: 21564 RVA: 0x00156187 File Offset: 0x00154387
		public AsynchronousConnectionMessage DataMessageFromQueue { get; set; }

		// Token: 0x1700148B RID: 5259
		// (get) Token: 0x0600543D RID: 21565 RVA: 0x00156190 File Offset: 0x00154390
		// (set) Token: 0x0600543E RID: 21566 RVA: 0x00156198 File Offset: 0x00154398
		public byte FapLevel { get; set; }

		// Token: 0x1700148C RID: 5260
		// (get) Token: 0x0600543F RID: 21567 RVA: 0x001561A1 File Offset: 0x001543A1
		// (set) Token: 0x06005440 RID: 21568 RVA: 0x001561A9 File Offset: 0x001543A9
		public short MaximumMessageBatch { get; set; }

		// Token: 0x1700148D RID: 5261
		// (get) Token: 0x06005441 RID: 21569 RVA: 0x001561B2 File Offset: 0x001543B2
		// (set) Token: 0x06005442 RID: 21570 RVA: 0x001561BA File Offset: 0x001543BA
		public int MaximumTransmissionSize { get; set; }

		// Token: 0x1700148E RID: 5262
		// (get) Token: 0x06005443 RID: 21571 RVA: 0x001561C3 File Offset: 0x001543C3
		// (set) Token: 0x06005444 RID: 21572 RVA: 0x001561CB File Offset: 0x001543CB
		public int MaximumMessageSize { get; set; }

		// Token: 0x1700148F RID: 5263
		// (get) Token: 0x06005445 RID: 21573 RVA: 0x001561D4 File Offset: 0x001543D4
		// (set) Token: 0x06005446 RID: 21574 RVA: 0x001561DC File Offset: 0x001543DC
		public int SequenceNumberWrap { get; set; }

		// Token: 0x17001490 RID: 5264
		// (get) Token: 0x06005447 RID: 21575 RVA: 0x001561E5 File Offset: 0x001543E5
		// (set) Token: 0x06005448 RID: 21576 RVA: 0x001561ED File Offset: 0x001543ED
		public int HeartBeatInterval { get; set; }

		// Token: 0x17001491 RID: 5265
		// (get) Token: 0x06005449 RID: 21577 RVA: 0x001561F6 File Offset: 0x001543F6
		// (set) Token: 0x0600544A RID: 21578 RVA: 0x001561FE File Offset: 0x001543FE
		public CapabilityFlag2 CapabilityFlag2 { get; set; }

		// Token: 0x17001492 RID: 5266
		// (get) Token: 0x0600544B RID: 21579 RVA: 0x00156207 File Offset: 0x00154407
		// (set) Token: 0x0600544C RID: 21580 RVA: 0x0015620F File Offset: 0x0015440F
		public int NumberOfConversationsPerSocket { get; set; }

		// Token: 0x17001493 RID: 5267
		// (get) Token: 0x0600544D RID: 21581 RVA: 0x00156218 File Offset: 0x00154418
		// (set) Token: 0x0600544E RID: 21582 RVA: 0x00156220 File Offset: 0x00154420
		public int UsedMaximumTransmissionSize { get; set; }

		// Token: 0x17001494 RID: 5268
		// (get) Token: 0x0600544F RID: 21583 RVA: 0x00156229 File Offset: 0x00154429
		// (set) Token: 0x06005450 RID: 21584 RVA: 0x00156231 File Offset: 0x00154431
		public byte ServerFapLevel { get; set; }

		// Token: 0x17001495 RID: 5269
		// (get) Token: 0x06005451 RID: 21585 RVA: 0x0015623A File Offset: 0x0015443A
		// (set) Token: 0x06005452 RID: 21586 RVA: 0x00156242 File Offset: 0x00154442
		public short ServerMaximumMessageBatch { get; set; }

		// Token: 0x17001496 RID: 5270
		// (get) Token: 0x06005453 RID: 21587 RVA: 0x0015624B File Offset: 0x0015444B
		// (set) Token: 0x06005454 RID: 21588 RVA: 0x00156253 File Offset: 0x00154453
		public int ServerMaximumTransmissionSize { get; set; }

		// Token: 0x17001497 RID: 5271
		// (get) Token: 0x06005455 RID: 21589 RVA: 0x0015625C File Offset: 0x0015445C
		// (set) Token: 0x06005456 RID: 21590 RVA: 0x00156264 File Offset: 0x00154464
		public int ServerMaximumMessageSize { get; set; }

		// Token: 0x17001498 RID: 5272
		// (get) Token: 0x06005457 RID: 21591 RVA: 0x0015626D File Offset: 0x0015446D
		// (set) Token: 0x06005458 RID: 21592 RVA: 0x00156275 File Offset: 0x00154475
		public int ServerSequenceNumberWrap { get; set; }

		// Token: 0x17001499 RID: 5273
		// (get) Token: 0x06005459 RID: 21593 RVA: 0x0015627E File Offset: 0x0015447E
		// (set) Token: 0x0600545A RID: 21594 RVA: 0x00156286 File Offset: 0x00154486
		public int ServerHeartBeatInterval { get; set; }

		// Token: 0x1700149A RID: 5274
		// (get) Token: 0x0600545B RID: 21595 RVA: 0x0015628F File Offset: 0x0015448F
		// (set) Token: 0x0600545C RID: 21596 RVA: 0x00156297 File Offset: 0x00154497
		public int ServerNumericEncoding { get; set; }

		// Token: 0x1700149B RID: 5275
		// (get) Token: 0x0600545D RID: 21597 RVA: 0x001562A0 File Offset: 0x001544A0
		// (set) Token: 0x0600545E RID: 21598 RVA: 0x001562A8 File Offset: 0x001544A8
		public short ServerCcsid { get; set; }

		// Token: 0x1700149C RID: 5276
		// (get) Token: 0x0600545F RID: 21599 RVA: 0x001562B1 File Offset: 0x001544B1
		// (set) Token: 0x06005460 RID: 21600 RVA: 0x001562B9 File Offset: 0x001544B9
		public InitializationErrorFlag1 ServerErrorFlag1 { get; set; }

		// Token: 0x1700149D RID: 5277
		// (get) Token: 0x06005461 RID: 21601 RVA: 0x001562C2 File Offset: 0x001544C2
		// (set) Token: 0x06005462 RID: 21602 RVA: 0x001562CA File Offset: 0x001544CA
		public InitializationErrorFlag2 ServerErrorFlag2 { get; set; }

		// Token: 0x1700149E RID: 5278
		// (get) Token: 0x06005463 RID: 21603 RVA: 0x001562D3 File Offset: 0x001544D3
		// (set) Token: 0x06005464 RID: 21604 RVA: 0x001562DB File Offset: 0x001544DB
		public CapabilityFlag2 ServerCapabilityFlag2 { get; set; }

		// Token: 0x1700149F RID: 5279
		// (get) Token: 0x06005465 RID: 21605 RVA: 0x001562E4 File Offset: 0x001544E4
		// (set) Token: 0x06005466 RID: 21606 RVA: 0x001562EC File Offset: 0x001544EC
		public string MqmId { get; set; }

		// Token: 0x170014A0 RID: 5280
		// (get) Token: 0x06005467 RID: 21607 RVA: 0x001562F5 File Offset: 0x001544F5
		// (set) Token: 0x06005468 RID: 21608 RVA: 0x001562FD File Offset: 0x001544FD
		public int ServerNumberOfConversationsPerSocket { get; set; }

		// Token: 0x170014A1 RID: 5281
		// (get) Token: 0x06005469 RID: 21609 RVA: 0x00156306 File Offset: 0x00154506
		// (set) Token: 0x0600546A RID: 21610 RVA: 0x0015630E File Offset: 0x0015450E
		public bool SrvConSecurityBitSet { get; set; }

		// Token: 0x170014A2 RID: 5282
		// (get) Token: 0x0600546B RID: 21611 RVA: 0x00156317 File Offset: 0x00154517
		// (set) Token: 0x0600546C RID: 21612 RVA: 0x0015631F File Offset: 0x0015451F
		public bool IsInitialData { get; set; }

		// Token: 0x170014A3 RID: 5283
		// (get) Token: 0x0600546D RID: 21613 RVA: 0x00156328 File Offset: 0x00154528
		// (set) Token: 0x0600546E RID: 21614 RVA: 0x00156330 File Offset: 0x00154530
		public bool IsStatusData { get; set; }

		// Token: 0x170014A4 RID: 5284
		// (get) Token: 0x0600546F RID: 21615 RVA: 0x00156339 File Offset: 0x00154539
		// (set) Token: 0x06005470 RID: 21616 RVA: 0x00156341 File Offset: 0x00154541
		public InitialDataStatus InitialDataStatus { get; set; }

		// Token: 0x170014A5 RID: 5285
		// (get) Token: 0x06005471 RID: 21617 RVA: 0x0015634A File Offset: 0x0015454A
		// (set) Token: 0x06005472 RID: 21618 RVA: 0x00156352 File Offset: 0x00154552
		public RejectedReason RejectedReason { get; set; }

		// Token: 0x170014A6 RID: 5286
		// (get) Token: 0x06005473 RID: 21619 RVA: 0x0015635B File Offset: 0x0015455B
		// (set) Token: 0x06005474 RID: 21620 RVA: 0x00156363 File Offset: 0x00154563
		public StatusDataType StatusDataType { get; set; }

		// Token: 0x170014A7 RID: 5287
		// (get) Token: 0x06005475 RID: 21621 RVA: 0x0015636C File Offset: 0x0015456C
		// (set) Token: 0x06005476 RID: 21622 RVA: 0x00156374 File Offset: 0x00154574
		public WindowsIdentity WindowsIdentity { get; set; }

		// Token: 0x170014A8 RID: 5288
		// (get) Token: 0x06005477 RID: 21623 RVA: 0x0015637D File Offset: 0x0015457D
		// (set) Token: 0x06005478 RID: 21624 RVA: 0x00156385 File Offset: 0x00154585
		public Queue<PassThroughData> QueuedOpens { get; private set; }

		// Token: 0x170014A9 RID: 5289
		// (get) Token: 0x06005479 RID: 21625 RVA: 0x0015638E File Offset: 0x0015458E
		// (set) Token: 0x0600547A RID: 21626 RVA: 0x00156396 File Offset: 0x00154596
		public PassThroughData CurrentOpen { get; set; }

		// Token: 0x170014AA RID: 5290
		// (get) Token: 0x0600547B RID: 21627 RVA: 0x0015639F File Offset: 0x0015459F
		// (set) Token: 0x0600547C RID: 21628 RVA: 0x001563A7 File Offset: 0x001545A7
		public Queue<PassThroughData> QueuedCloses { get; private set; }

		// Token: 0x170014AB RID: 5291
		// (get) Token: 0x0600547D RID: 21629 RVA: 0x001563B0 File Offset: 0x001545B0
		// (set) Token: 0x0600547E RID: 21630 RVA: 0x001563B8 File Offset: 0x001545B8
		public PassThroughData CurrentClose { get; set; }

		// Token: 0x170014AC RID: 5292
		// (get) Token: 0x0600547F RID: 21631 RVA: 0x001563C1 File Offset: 0x001545C1
		// (set) Token: 0x06005480 RID: 21632 RVA: 0x001563C9 File Offset: 0x001545C9
		public AsynchronousConnectionMessage MessageToQueue { get; set; }

		// Token: 0x170014AD RID: 5293
		// (get) Token: 0x06005481 RID: 21633 RVA: 0x001563D2 File Offset: 0x001545D2
		// (set) Token: 0x06005482 RID: 21634 RVA: 0x001563DA File Offset: 0x001545DA
		public WrappedPooledQueueManager WrappedQueueManager { get; set; }

		// Token: 0x170014AE RID: 5294
		// (get) Token: 0x06005483 RID: 21635 RVA: 0x001563E3 File Offset: 0x001545E3
		// (set) Token: 0x06005484 RID: 21636 RVA: 0x001563EB File Offset: 0x001545EB
		public bool InFailedState { get; set; }

		// Token: 0x170014AF RID: 5295
		// (get) Token: 0x06005485 RID: 21637 RVA: 0x001563F4 File Offset: 0x001545F4
		// (set) Token: 0x06005486 RID: 21638 RVA: 0x001563FC File Offset: 0x001545FC
		public int FailedPutCount { get; set; }

		// Token: 0x170014B0 RID: 5296
		// (get) Token: 0x06005487 RID: 21639 RVA: 0x00156405 File Offset: 0x00154605
		// (set) Token: 0x06005488 RID: 21640 RVA: 0x0015640D File Offset: 0x0015460D
		public int WarningPutCount { get; set; }

		// Token: 0x170014B1 RID: 5297
		// (get) Token: 0x06005489 RID: 21641 RVA: 0x00156416 File Offset: 0x00154616
		// (set) Token: 0x0600548A RID: 21642 RVA: 0x0015641E File Offset: 0x0015461E
		public int SuccessfulPutCount { get; set; }

		// Token: 0x170014B2 RID: 5298
		// (get) Token: 0x0600548B RID: 21643 RVA: 0x00156427 File Offset: 0x00154627
		// (set) Token: 0x0600548C RID: 21644 RVA: 0x0015642F File Offset: 0x0015462F
		public MqCommandType CommandType { get; set; }

		// Token: 0x170014B3 RID: 5299
		// (get) Token: 0x0600548D RID: 21645 RVA: 0x00156438 File Offset: 0x00154638
		// (set) Token: 0x0600548E RID: 21646 RVA: 0x00156440 File Offset: 0x00154640
		public bool MqCommandProcessed { get; set; }

		// Token: 0x170014B4 RID: 5300
		// (get) Token: 0x0600548F RID: 21647 RVA: 0x00156449 File Offset: 0x00154649
		// (set) Token: 0x06005490 RID: 21648 RVA: 0x00156451 File Offset: 0x00154651
		public XaFlags XaFlags { get; set; }

		// Token: 0x170014B5 RID: 5301
		// (get) Token: 0x06005491 RID: 21649 RVA: 0x0015645A File Offset: 0x0015465A
		// (set) Token: 0x06005492 RID: 21650 RVA: 0x00156462 File Offset: 0x00154662
		public int MaximumNumberOfXids { get; set; }

		// Token: 0x170014B6 RID: 5302
		// (get) Token: 0x06005493 RID: 21651 RVA: 0x0015646B File Offset: 0x0015466B
		// (set) Token: 0x06005494 RID: 21652 RVA: 0x00156473 File Offset: 0x00154673
		public Xid[] RecoveredXids { get; set; }

		// Token: 0x170014B7 RID: 5303
		// (get) Token: 0x06005495 RID: 21653 RVA: 0x0015647C File Offset: 0x0015467C
		// (set) Token: 0x06005496 RID: 21654 RVA: 0x00156484 File Offset: 0x00154684
		public bool DisconnectRequested { get; set; }

		// Token: 0x170014B8 RID: 5304
		// (get) Token: 0x06005497 RID: 21655 RVA: 0x0015648D File Offset: 0x0015468D
		// (set) Token: 0x06005498 RID: 21656 RVA: 0x00156495 File Offset: 0x00154695
		public bool EnlistQueued { get; set; }

		// Token: 0x06005499 RID: 21657 RVA: 0x001564A0 File Offset: 0x001546A0
		public AutomatonQueueManagerContext()
		{
			this.ConnectedEvent = new EventWaitHandle(false, EventResetMode.AutoReset);
			this.DisconnectedEvent = new EventWaitHandle(false, EventResetMode.AutoReset);
			this.CommandEvent = new EventWaitHandle(false, EventResetMode.AutoReset);
			this.EnlistEvent = new EventWaitHandle(false, EventResetMode.AutoReset);
			this.QueuedOpens = new Queue<PassThroughData>();
			this.QueuedCloses = new Queue<PassThroughData>();
			this.XaFlags = XaFlags.None;
		}
	}
}
