using System;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x0200060E RID: 1550
	[ComVisible(false)]
	[ComEventInterface(typeof(HTMLOptionButtonElementEvents\u0000), typeof(HTMLOptionButtonElementEvents_EventProvider\u0000))]
	public interface HTMLOptionButtonElementEvents_Event
	{
		// Token: 0x140011DB RID: 4571
		// (add) Token: 0x06009657 RID: 38487
		// (remove) Token: 0x06009658 RID: 38488
		event HTMLOptionButtonElementEvents_onhelpEventHandler onhelp;

		// Token: 0x140011DC RID: 4572
		// (add) Token: 0x06009659 RID: 38489
		// (remove) Token: 0x0600965A RID: 38490
		event HTMLOptionButtonElementEvents_onclickEventHandler onclick;

		// Token: 0x140011DD RID: 4573
		// (add) Token: 0x0600965B RID: 38491
		// (remove) Token: 0x0600965C RID: 38492
		event HTMLOptionButtonElementEvents_ondblclickEventHandler ondblclick;

		// Token: 0x140011DE RID: 4574
		// (add) Token: 0x0600965D RID: 38493
		// (remove) Token: 0x0600965E RID: 38494
		event HTMLOptionButtonElementEvents_onkeypressEventHandler onkeypress;

		// Token: 0x140011DF RID: 4575
		// (add) Token: 0x0600965F RID: 38495
		// (remove) Token: 0x06009660 RID: 38496
		event HTMLOptionButtonElementEvents_onkeydownEventHandler onkeydown;

		// Token: 0x140011E0 RID: 4576
		// (add) Token: 0x06009661 RID: 38497
		// (remove) Token: 0x06009662 RID: 38498
		event HTMLOptionButtonElementEvents_onkeyupEventHandler onkeyup;

		// Token: 0x140011E1 RID: 4577
		// (add) Token: 0x06009663 RID: 38499
		// (remove) Token: 0x06009664 RID: 38500
		event HTMLOptionButtonElementEvents_onmouseoutEventHandler onmouseout;

		// Token: 0x140011E2 RID: 4578
		// (add) Token: 0x06009665 RID: 38501
		// (remove) Token: 0x06009666 RID: 38502
		event HTMLOptionButtonElementEvents_onmouseoverEventHandler onmouseover;

		// Token: 0x140011E3 RID: 4579
		// (add) Token: 0x06009667 RID: 38503
		// (remove) Token: 0x06009668 RID: 38504
		event HTMLOptionButtonElementEvents_onmousemoveEventHandler onmousemove;

		// Token: 0x140011E4 RID: 4580
		// (add) Token: 0x06009669 RID: 38505
		// (remove) Token: 0x0600966A RID: 38506
		event HTMLOptionButtonElementEvents_onmousedownEventHandler onmousedown;

		// Token: 0x140011E5 RID: 4581
		// (add) Token: 0x0600966B RID: 38507
		// (remove) Token: 0x0600966C RID: 38508
		event HTMLOptionButtonElementEvents_onmouseupEventHandler onmouseup;

		// Token: 0x140011E6 RID: 4582
		// (add) Token: 0x0600966D RID: 38509
		// (remove) Token: 0x0600966E RID: 38510
		event HTMLOptionButtonElementEvents_onselectstartEventHandler onselectstart;

		// Token: 0x140011E7 RID: 4583
		// (add) Token: 0x0600966F RID: 38511
		// (remove) Token: 0x06009670 RID: 38512
		event HTMLOptionButtonElementEvents_onfilterchangeEventHandler onfilterchange;

		// Token: 0x140011E8 RID: 4584
		// (add) Token: 0x06009671 RID: 38513
		// (remove) Token: 0x06009672 RID: 38514
		event HTMLOptionButtonElementEvents_ondragstartEventHandler ondragstart;

		// Token: 0x140011E9 RID: 4585
		// (add) Token: 0x06009673 RID: 38515
		// (remove) Token: 0x06009674 RID: 38516
		event HTMLOptionButtonElementEvents_onbeforeupdateEventHandler onbeforeupdate;

		// Token: 0x140011EA RID: 4586
		// (add) Token: 0x06009675 RID: 38517
		// (remove) Token: 0x06009676 RID: 38518
		event HTMLOptionButtonElementEvents_onafterupdateEventHandler onafterupdate;

		// Token: 0x140011EB RID: 4587
		// (add) Token: 0x06009677 RID: 38519
		// (remove) Token: 0x06009678 RID: 38520
		event HTMLOptionButtonElementEvents_onerrorupdateEventHandler onerrorupdate;

		// Token: 0x140011EC RID: 4588
		// (add) Token: 0x06009679 RID: 38521
		// (remove) Token: 0x0600967A RID: 38522
		event HTMLOptionButtonElementEvents_onrowexitEventHandler onrowexit;

		// Token: 0x140011ED RID: 4589
		// (add) Token: 0x0600967B RID: 38523
		// (remove) Token: 0x0600967C RID: 38524
		event HTMLOptionButtonElementEvents_onrowenterEventHandler onrowenter;

		// Token: 0x140011EE RID: 4590
		// (add) Token: 0x0600967D RID: 38525
		// (remove) Token: 0x0600967E RID: 38526
		event HTMLOptionButtonElementEvents_ondatasetchangedEventHandler ondatasetchanged;

		// Token: 0x140011EF RID: 4591
		// (add) Token: 0x0600967F RID: 38527
		// (remove) Token: 0x06009680 RID: 38528
		event HTMLOptionButtonElementEvents_ondataavailableEventHandler ondataavailable;

		// Token: 0x140011F0 RID: 4592
		// (add) Token: 0x06009681 RID: 38529
		// (remove) Token: 0x06009682 RID: 38530
		event HTMLOptionButtonElementEvents_ondatasetcompleteEventHandler ondatasetcomplete;

		// Token: 0x140011F1 RID: 4593
		// (add) Token: 0x06009683 RID: 38531
		// (remove) Token: 0x06009684 RID: 38532
		event HTMLOptionButtonElementEvents_onlosecaptureEventHandler onlosecapture;

		// Token: 0x140011F2 RID: 4594
		// (add) Token: 0x06009685 RID: 38533
		// (remove) Token: 0x06009686 RID: 38534
		event HTMLOptionButtonElementEvents_onpropertychangeEventHandler onpropertychange;

		// Token: 0x140011F3 RID: 4595
		// (add) Token: 0x06009687 RID: 38535
		// (remove) Token: 0x06009688 RID: 38536
		event HTMLOptionButtonElementEvents_onscrollEventHandler onscroll;

		// Token: 0x140011F4 RID: 4596
		// (add) Token: 0x06009689 RID: 38537
		// (remove) Token: 0x0600968A RID: 38538
		event HTMLOptionButtonElementEvents_onfocusEventHandler onfocus;

		// Token: 0x140011F5 RID: 4597
		// (add) Token: 0x0600968B RID: 38539
		// (remove) Token: 0x0600968C RID: 38540
		event HTMLOptionButtonElementEvents_onblurEventHandler onblur;

		// Token: 0x140011F6 RID: 4598
		// (add) Token: 0x0600968D RID: 38541
		// (remove) Token: 0x0600968E RID: 38542
		event HTMLOptionButtonElementEvents_onresizeEventHandler onresize;

		// Token: 0x140011F7 RID: 4599
		// (add) Token: 0x0600968F RID: 38543
		// (remove) Token: 0x06009690 RID: 38544
		event HTMLOptionButtonElementEvents_ondragEventHandler ondrag;

		// Token: 0x140011F8 RID: 4600
		// (add) Token: 0x06009691 RID: 38545
		// (remove) Token: 0x06009692 RID: 38546
		event HTMLOptionButtonElementEvents_ondragendEventHandler ondragend;

		// Token: 0x140011F9 RID: 4601
		// (add) Token: 0x06009693 RID: 38547
		// (remove) Token: 0x06009694 RID: 38548
		event HTMLOptionButtonElementEvents_ondragenterEventHandler ondragenter;

		// Token: 0x140011FA RID: 4602
		// (add) Token: 0x06009695 RID: 38549
		// (remove) Token: 0x06009696 RID: 38550
		event HTMLOptionButtonElementEvents_ondragoverEventHandler ondragover;

		// Token: 0x140011FB RID: 4603
		// (add) Token: 0x06009697 RID: 38551
		// (remove) Token: 0x06009698 RID: 38552
		event HTMLOptionButtonElementEvents_ondragleaveEventHandler ondragleave;

		// Token: 0x140011FC RID: 4604
		// (add) Token: 0x06009699 RID: 38553
		// (remove) Token: 0x0600969A RID: 38554
		event HTMLOptionButtonElementEvents_ondropEventHandler ondrop;

		// Token: 0x140011FD RID: 4605
		// (add) Token: 0x0600969B RID: 38555
		// (remove) Token: 0x0600969C RID: 38556
		event HTMLOptionButtonElementEvents_onbeforecutEventHandler onbeforecut;

		// Token: 0x140011FE RID: 4606
		// (add) Token: 0x0600969D RID: 38557
		// (remove) Token: 0x0600969E RID: 38558
		event HTMLOptionButtonElementEvents_oncutEventHandler oncut;

		// Token: 0x140011FF RID: 4607
		// (add) Token: 0x0600969F RID: 38559
		// (remove) Token: 0x060096A0 RID: 38560
		event HTMLOptionButtonElementEvents_onbeforecopyEventHandler onbeforecopy;

		// Token: 0x14001200 RID: 4608
		// (add) Token: 0x060096A1 RID: 38561
		// (remove) Token: 0x060096A2 RID: 38562
		event HTMLOptionButtonElementEvents_oncopyEventHandler oncopy;

		// Token: 0x14001201 RID: 4609
		// (add) Token: 0x060096A3 RID: 38563
		// (remove) Token: 0x060096A4 RID: 38564
		event HTMLOptionButtonElementEvents_onbeforepasteEventHandler onbeforepaste;

		// Token: 0x14001202 RID: 4610
		// (add) Token: 0x060096A5 RID: 38565
		// (remove) Token: 0x060096A6 RID: 38566
		event HTMLOptionButtonElementEvents_onpasteEventHandler onpaste;

		// Token: 0x14001203 RID: 4611
		// (add) Token: 0x060096A7 RID: 38567
		// (remove) Token: 0x060096A8 RID: 38568
		event HTMLOptionButtonElementEvents_oncontextmenuEventHandler oncontextmenu;

		// Token: 0x14001204 RID: 4612
		// (add) Token: 0x060096A9 RID: 38569
		// (remove) Token: 0x060096AA RID: 38570
		event HTMLOptionButtonElementEvents_onrowsdeleteEventHandler onrowsdelete;

		// Token: 0x14001205 RID: 4613
		// (add) Token: 0x060096AB RID: 38571
		// (remove) Token: 0x060096AC RID: 38572
		event HTMLOptionButtonElementEvents_onrowsinsertedEventHandler onrowsinserted;

		// Token: 0x14001206 RID: 4614
		// (add) Token: 0x060096AD RID: 38573
		// (remove) Token: 0x060096AE RID: 38574
		event HTMLOptionButtonElementEvents_oncellchangeEventHandler oncellchange;

		// Token: 0x14001207 RID: 4615
		// (add) Token: 0x060096AF RID: 38575
		// (remove) Token: 0x060096B0 RID: 38576
		event HTMLOptionButtonElementEvents_onreadystatechangeEventHandler onreadystatechange;

		// Token: 0x14001208 RID: 4616
		// (add) Token: 0x060096B1 RID: 38577
		// (remove) Token: 0x060096B2 RID: 38578
		event HTMLOptionButtonElementEvents_onbeforeeditfocusEventHandler onbeforeeditfocus;

		// Token: 0x14001209 RID: 4617
		// (add) Token: 0x060096B3 RID: 38579
		// (remove) Token: 0x060096B4 RID: 38580
		event HTMLOptionButtonElementEvents_onlayoutcompleteEventHandler onlayoutcomplete;

		// Token: 0x1400120A RID: 4618
		// (add) Token: 0x060096B5 RID: 38581
		// (remove) Token: 0x060096B6 RID: 38582
		event HTMLOptionButtonElementEvents_onpageEventHandler onpage;

		// Token: 0x1400120B RID: 4619
		// (add) Token: 0x060096B7 RID: 38583
		// (remove) Token: 0x060096B8 RID: 38584
		event HTMLOptionButtonElementEvents_onbeforedeactivateEventHandler onbeforedeactivate;

		// Token: 0x1400120C RID: 4620
		// (add) Token: 0x060096B9 RID: 38585
		// (remove) Token: 0x060096BA RID: 38586
		event HTMLOptionButtonElementEvents_onbeforeactivateEventHandler onbeforeactivate;

		// Token: 0x1400120D RID: 4621
		// (add) Token: 0x060096BB RID: 38587
		// (remove) Token: 0x060096BC RID: 38588
		event HTMLOptionButtonElementEvents_onmoveEventHandler onmove;

		// Token: 0x1400120E RID: 4622
		// (add) Token: 0x060096BD RID: 38589
		// (remove) Token: 0x060096BE RID: 38590
		event HTMLOptionButtonElementEvents_oncontrolselectEventHandler oncontrolselect;

		// Token: 0x1400120F RID: 4623
		// (add) Token: 0x060096BF RID: 38591
		// (remove) Token: 0x060096C0 RID: 38592
		event HTMLOptionButtonElementEvents_onmovestartEventHandler onmovestart;

		// Token: 0x14001210 RID: 4624
		// (add) Token: 0x060096C1 RID: 38593
		// (remove) Token: 0x060096C2 RID: 38594
		event HTMLOptionButtonElementEvents_onmoveendEventHandler onmoveend;

		// Token: 0x14001211 RID: 4625
		// (add) Token: 0x060096C3 RID: 38595
		// (remove) Token: 0x060096C4 RID: 38596
		event HTMLOptionButtonElementEvents_onresizestartEventHandler onresizestart;

		// Token: 0x14001212 RID: 4626
		// (add) Token: 0x060096C5 RID: 38597
		// (remove) Token: 0x060096C6 RID: 38598
		event HTMLOptionButtonElementEvents_onresizeendEventHandler onresizeend;

		// Token: 0x14001213 RID: 4627
		// (add) Token: 0x060096C7 RID: 38599
		// (remove) Token: 0x060096C8 RID: 38600
		event HTMLOptionButtonElementEvents_onmouseenterEventHandler onmouseenter;

		// Token: 0x14001214 RID: 4628
		// (add) Token: 0x060096C9 RID: 38601
		// (remove) Token: 0x060096CA RID: 38602
		event HTMLOptionButtonElementEvents_onmouseleaveEventHandler onmouseleave;

		// Token: 0x14001215 RID: 4629
		// (add) Token: 0x060096CB RID: 38603
		// (remove) Token: 0x060096CC RID: 38604
		event HTMLOptionButtonElementEvents_onmousewheelEventHandler onmousewheel;

		// Token: 0x14001216 RID: 4630
		// (add) Token: 0x060096CD RID: 38605
		// (remove) Token: 0x060096CE RID: 38606
		event HTMLOptionButtonElementEvents_onactivateEventHandler onactivate;

		// Token: 0x14001217 RID: 4631
		// (add) Token: 0x060096CF RID: 38607
		// (remove) Token: 0x060096D0 RID: 38608
		event HTMLOptionButtonElementEvents_ondeactivateEventHandler ondeactivate;

		// Token: 0x14001218 RID: 4632
		// (add) Token: 0x060096D1 RID: 38609
		// (remove) Token: 0x060096D2 RID: 38610
		event HTMLOptionButtonElementEvents_onfocusinEventHandler onfocusin;

		// Token: 0x14001219 RID: 4633
		// (add) Token: 0x060096D3 RID: 38611
		// (remove) Token: 0x060096D4 RID: 38612
		event HTMLOptionButtonElementEvents_onfocusoutEventHandler onfocusout;

		// Token: 0x1400121A RID: 4634
		// (add) Token: 0x060096D5 RID: 38613
		// (remove) Token: 0x060096D6 RID: 38614
		event HTMLOptionButtonElementEvents_onchangeEventHandler onchange;

		// Token: 0x1400121B RID: 4635
		// (add) Token: 0x060096D7 RID: 38615
		// (remove) Token: 0x060096D8 RID: 38616
		event HTMLOptionButtonElementEvents_onselectEventHandler onselect;

		// Token: 0x1400121C RID: 4636
		// (add) Token: 0x060096D9 RID: 38617
		// (remove) Token: 0x060096DA RID: 38618
		event HTMLOptionButtonElementEvents_onloadEventHandler onload;

		// Token: 0x1400121D RID: 4637
		// (add) Token: 0x060096DB RID: 38619
		// (remove) Token: 0x060096DC RID: 38620
		event HTMLOptionButtonElementEvents_onerrorEventHandler onerror;

		// Token: 0x1400121E RID: 4638
		// (add) Token: 0x060096DD RID: 38621
		// (remove) Token: 0x060096DE RID: 38622
		event HTMLOptionButtonElementEvents_onabortEventHandler onabort;
	}
}
