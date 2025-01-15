using System;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000B8C RID: 2956
	[ComVisible(false)]
	[ComEventInterface(typeof(HTMLFrameSiteEvents2\u0000), typeof(HTMLFrameSiteEvents2_EventProvider\u0000))]
	public interface HTMLFrameSiteEvents2_Event
	{
		// Token: 0x140025CD RID: 9677
		// (add) Token: 0x060136EE RID: 79598
		// (remove) Token: 0x060136EF RID: 79599
		event HTMLFrameSiteEvents2_onhelpEventHandler onhelp;

		// Token: 0x140025CE RID: 9678
		// (add) Token: 0x060136F0 RID: 79600
		// (remove) Token: 0x060136F1 RID: 79601
		event HTMLFrameSiteEvents2_onclickEventHandler onclick;

		// Token: 0x140025CF RID: 9679
		// (add) Token: 0x060136F2 RID: 79602
		// (remove) Token: 0x060136F3 RID: 79603
		event HTMLFrameSiteEvents2_ondblclickEventHandler ondblclick;

		// Token: 0x140025D0 RID: 9680
		// (add) Token: 0x060136F4 RID: 79604
		// (remove) Token: 0x060136F5 RID: 79605
		event HTMLFrameSiteEvents2_onkeypressEventHandler onkeypress;

		// Token: 0x140025D1 RID: 9681
		// (add) Token: 0x060136F6 RID: 79606
		// (remove) Token: 0x060136F7 RID: 79607
		event HTMLFrameSiteEvents2_onkeydownEventHandler onkeydown;

		// Token: 0x140025D2 RID: 9682
		// (add) Token: 0x060136F8 RID: 79608
		// (remove) Token: 0x060136F9 RID: 79609
		event HTMLFrameSiteEvents2_onkeyupEventHandler onkeyup;

		// Token: 0x140025D3 RID: 9683
		// (add) Token: 0x060136FA RID: 79610
		// (remove) Token: 0x060136FB RID: 79611
		event HTMLFrameSiteEvents2_onmouseoutEventHandler onmouseout;

		// Token: 0x140025D4 RID: 9684
		// (add) Token: 0x060136FC RID: 79612
		// (remove) Token: 0x060136FD RID: 79613
		event HTMLFrameSiteEvents2_onmouseoverEventHandler onmouseover;

		// Token: 0x140025D5 RID: 9685
		// (add) Token: 0x060136FE RID: 79614
		// (remove) Token: 0x060136FF RID: 79615
		event HTMLFrameSiteEvents2_onmousemoveEventHandler onmousemove;

		// Token: 0x140025D6 RID: 9686
		// (add) Token: 0x06013700 RID: 79616
		// (remove) Token: 0x06013701 RID: 79617
		event HTMLFrameSiteEvents2_onmousedownEventHandler onmousedown;

		// Token: 0x140025D7 RID: 9687
		// (add) Token: 0x06013702 RID: 79618
		// (remove) Token: 0x06013703 RID: 79619
		event HTMLFrameSiteEvents2_onmouseupEventHandler onmouseup;

		// Token: 0x140025D8 RID: 9688
		// (add) Token: 0x06013704 RID: 79620
		// (remove) Token: 0x06013705 RID: 79621
		event HTMLFrameSiteEvents2_onselectstartEventHandler onselectstart;

		// Token: 0x140025D9 RID: 9689
		// (add) Token: 0x06013706 RID: 79622
		// (remove) Token: 0x06013707 RID: 79623
		event HTMLFrameSiteEvents2_onfilterchangeEventHandler onfilterchange;

		// Token: 0x140025DA RID: 9690
		// (add) Token: 0x06013708 RID: 79624
		// (remove) Token: 0x06013709 RID: 79625
		event HTMLFrameSiteEvents2_ondragstartEventHandler ondragstart;

		// Token: 0x140025DB RID: 9691
		// (add) Token: 0x0601370A RID: 79626
		// (remove) Token: 0x0601370B RID: 79627
		event HTMLFrameSiteEvents2_onbeforeupdateEventHandler onbeforeupdate;

		// Token: 0x140025DC RID: 9692
		// (add) Token: 0x0601370C RID: 79628
		// (remove) Token: 0x0601370D RID: 79629
		event HTMLFrameSiteEvents2_onafterupdateEventHandler onafterupdate;

		// Token: 0x140025DD RID: 9693
		// (add) Token: 0x0601370E RID: 79630
		// (remove) Token: 0x0601370F RID: 79631
		event HTMLFrameSiteEvents2_onerrorupdateEventHandler onerrorupdate;

		// Token: 0x140025DE RID: 9694
		// (add) Token: 0x06013710 RID: 79632
		// (remove) Token: 0x06013711 RID: 79633
		event HTMLFrameSiteEvents2_onrowexitEventHandler onrowexit;

		// Token: 0x140025DF RID: 9695
		// (add) Token: 0x06013712 RID: 79634
		// (remove) Token: 0x06013713 RID: 79635
		event HTMLFrameSiteEvents2_onrowenterEventHandler onrowenter;

		// Token: 0x140025E0 RID: 9696
		// (add) Token: 0x06013714 RID: 79636
		// (remove) Token: 0x06013715 RID: 79637
		event HTMLFrameSiteEvents2_ondatasetchangedEventHandler ondatasetchanged;

		// Token: 0x140025E1 RID: 9697
		// (add) Token: 0x06013716 RID: 79638
		// (remove) Token: 0x06013717 RID: 79639
		event HTMLFrameSiteEvents2_ondataavailableEventHandler ondataavailable;

		// Token: 0x140025E2 RID: 9698
		// (add) Token: 0x06013718 RID: 79640
		// (remove) Token: 0x06013719 RID: 79641
		event HTMLFrameSiteEvents2_ondatasetcompleteEventHandler ondatasetcomplete;

		// Token: 0x140025E3 RID: 9699
		// (add) Token: 0x0601371A RID: 79642
		// (remove) Token: 0x0601371B RID: 79643
		event HTMLFrameSiteEvents2_onlosecaptureEventHandler onlosecapture;

		// Token: 0x140025E4 RID: 9700
		// (add) Token: 0x0601371C RID: 79644
		// (remove) Token: 0x0601371D RID: 79645
		event HTMLFrameSiteEvents2_onpropertychangeEventHandler onpropertychange;

		// Token: 0x140025E5 RID: 9701
		// (add) Token: 0x0601371E RID: 79646
		// (remove) Token: 0x0601371F RID: 79647
		event HTMLFrameSiteEvents2_onscrollEventHandler onscroll;

		// Token: 0x140025E6 RID: 9702
		// (add) Token: 0x06013720 RID: 79648
		// (remove) Token: 0x06013721 RID: 79649
		event HTMLFrameSiteEvents2_onfocusEventHandler onfocus;

		// Token: 0x140025E7 RID: 9703
		// (add) Token: 0x06013722 RID: 79650
		// (remove) Token: 0x06013723 RID: 79651
		event HTMLFrameSiteEvents2_onblurEventHandler onblur;

		// Token: 0x140025E8 RID: 9704
		// (add) Token: 0x06013724 RID: 79652
		// (remove) Token: 0x06013725 RID: 79653
		event HTMLFrameSiteEvents2_onresizeEventHandler onresize;

		// Token: 0x140025E9 RID: 9705
		// (add) Token: 0x06013726 RID: 79654
		// (remove) Token: 0x06013727 RID: 79655
		event HTMLFrameSiteEvents2_ondragEventHandler ondrag;

		// Token: 0x140025EA RID: 9706
		// (add) Token: 0x06013728 RID: 79656
		// (remove) Token: 0x06013729 RID: 79657
		event HTMLFrameSiteEvents2_ondragendEventHandler ondragend;

		// Token: 0x140025EB RID: 9707
		// (add) Token: 0x0601372A RID: 79658
		// (remove) Token: 0x0601372B RID: 79659
		event HTMLFrameSiteEvents2_ondragenterEventHandler ondragenter;

		// Token: 0x140025EC RID: 9708
		// (add) Token: 0x0601372C RID: 79660
		// (remove) Token: 0x0601372D RID: 79661
		event HTMLFrameSiteEvents2_ondragoverEventHandler ondragover;

		// Token: 0x140025ED RID: 9709
		// (add) Token: 0x0601372E RID: 79662
		// (remove) Token: 0x0601372F RID: 79663
		event HTMLFrameSiteEvents2_ondragleaveEventHandler ondragleave;

		// Token: 0x140025EE RID: 9710
		// (add) Token: 0x06013730 RID: 79664
		// (remove) Token: 0x06013731 RID: 79665
		event HTMLFrameSiteEvents2_ondropEventHandler ondrop;

		// Token: 0x140025EF RID: 9711
		// (add) Token: 0x06013732 RID: 79666
		// (remove) Token: 0x06013733 RID: 79667
		event HTMLFrameSiteEvents2_onbeforecutEventHandler onbeforecut;

		// Token: 0x140025F0 RID: 9712
		// (add) Token: 0x06013734 RID: 79668
		// (remove) Token: 0x06013735 RID: 79669
		event HTMLFrameSiteEvents2_oncutEventHandler oncut;

		// Token: 0x140025F1 RID: 9713
		// (add) Token: 0x06013736 RID: 79670
		// (remove) Token: 0x06013737 RID: 79671
		event HTMLFrameSiteEvents2_onbeforecopyEventHandler onbeforecopy;

		// Token: 0x140025F2 RID: 9714
		// (add) Token: 0x06013738 RID: 79672
		// (remove) Token: 0x06013739 RID: 79673
		event HTMLFrameSiteEvents2_oncopyEventHandler oncopy;

		// Token: 0x140025F3 RID: 9715
		// (add) Token: 0x0601373A RID: 79674
		// (remove) Token: 0x0601373B RID: 79675
		event HTMLFrameSiteEvents2_onbeforepasteEventHandler onbeforepaste;

		// Token: 0x140025F4 RID: 9716
		// (add) Token: 0x0601373C RID: 79676
		// (remove) Token: 0x0601373D RID: 79677
		event HTMLFrameSiteEvents2_onpasteEventHandler onpaste;

		// Token: 0x140025F5 RID: 9717
		// (add) Token: 0x0601373E RID: 79678
		// (remove) Token: 0x0601373F RID: 79679
		event HTMLFrameSiteEvents2_oncontextmenuEventHandler oncontextmenu;

		// Token: 0x140025F6 RID: 9718
		// (add) Token: 0x06013740 RID: 79680
		// (remove) Token: 0x06013741 RID: 79681
		event HTMLFrameSiteEvents2_onrowsdeleteEventHandler onrowsdelete;

		// Token: 0x140025F7 RID: 9719
		// (add) Token: 0x06013742 RID: 79682
		// (remove) Token: 0x06013743 RID: 79683
		event HTMLFrameSiteEvents2_onrowsinsertedEventHandler onrowsinserted;

		// Token: 0x140025F8 RID: 9720
		// (add) Token: 0x06013744 RID: 79684
		// (remove) Token: 0x06013745 RID: 79685
		event HTMLFrameSiteEvents2_oncellchangeEventHandler oncellchange;

		// Token: 0x140025F9 RID: 9721
		// (add) Token: 0x06013746 RID: 79686
		// (remove) Token: 0x06013747 RID: 79687
		event HTMLFrameSiteEvents2_onreadystatechangeEventHandler onreadystatechange;

		// Token: 0x140025FA RID: 9722
		// (add) Token: 0x06013748 RID: 79688
		// (remove) Token: 0x06013749 RID: 79689
		event HTMLFrameSiteEvents2_onlayoutcompleteEventHandler onlayoutcomplete;

		// Token: 0x140025FB RID: 9723
		// (add) Token: 0x0601374A RID: 79690
		// (remove) Token: 0x0601374B RID: 79691
		event HTMLFrameSiteEvents2_onpageEventHandler onpage;

		// Token: 0x140025FC RID: 9724
		// (add) Token: 0x0601374C RID: 79692
		// (remove) Token: 0x0601374D RID: 79693
		event HTMLFrameSiteEvents2_onmouseenterEventHandler onmouseenter;

		// Token: 0x140025FD RID: 9725
		// (add) Token: 0x0601374E RID: 79694
		// (remove) Token: 0x0601374F RID: 79695
		event HTMLFrameSiteEvents2_onmouseleaveEventHandler onmouseleave;

		// Token: 0x140025FE RID: 9726
		// (add) Token: 0x06013750 RID: 79696
		// (remove) Token: 0x06013751 RID: 79697
		event HTMLFrameSiteEvents2_onactivateEventHandler onactivate;

		// Token: 0x140025FF RID: 9727
		// (add) Token: 0x06013752 RID: 79698
		// (remove) Token: 0x06013753 RID: 79699
		event HTMLFrameSiteEvents2_ondeactivateEventHandler ondeactivate;

		// Token: 0x14002600 RID: 9728
		// (add) Token: 0x06013754 RID: 79700
		// (remove) Token: 0x06013755 RID: 79701
		event HTMLFrameSiteEvents2_onbeforedeactivateEventHandler onbeforedeactivate;

		// Token: 0x14002601 RID: 9729
		// (add) Token: 0x06013756 RID: 79702
		// (remove) Token: 0x06013757 RID: 79703
		event HTMLFrameSiteEvents2_onbeforeactivateEventHandler onbeforeactivate;

		// Token: 0x14002602 RID: 9730
		// (add) Token: 0x06013758 RID: 79704
		// (remove) Token: 0x06013759 RID: 79705
		event HTMLFrameSiteEvents2_onfocusinEventHandler onfocusin;

		// Token: 0x14002603 RID: 9731
		// (add) Token: 0x0601375A RID: 79706
		// (remove) Token: 0x0601375B RID: 79707
		event HTMLFrameSiteEvents2_onfocusoutEventHandler onfocusout;

		// Token: 0x14002604 RID: 9732
		// (add) Token: 0x0601375C RID: 79708
		// (remove) Token: 0x0601375D RID: 79709
		event HTMLFrameSiteEvents2_onmoveEventHandler onmove;

		// Token: 0x14002605 RID: 9733
		// (add) Token: 0x0601375E RID: 79710
		// (remove) Token: 0x0601375F RID: 79711
		event HTMLFrameSiteEvents2_oncontrolselectEventHandler oncontrolselect;

		// Token: 0x14002606 RID: 9734
		// (add) Token: 0x06013760 RID: 79712
		// (remove) Token: 0x06013761 RID: 79713
		event HTMLFrameSiteEvents2_onmovestartEventHandler onmovestart;

		// Token: 0x14002607 RID: 9735
		// (add) Token: 0x06013762 RID: 79714
		// (remove) Token: 0x06013763 RID: 79715
		event HTMLFrameSiteEvents2_onmoveendEventHandler onmoveend;

		// Token: 0x14002608 RID: 9736
		// (add) Token: 0x06013764 RID: 79716
		// (remove) Token: 0x06013765 RID: 79717
		event HTMLFrameSiteEvents2_onresizestartEventHandler onresizestart;

		// Token: 0x14002609 RID: 9737
		// (add) Token: 0x06013766 RID: 79718
		// (remove) Token: 0x06013767 RID: 79719
		event HTMLFrameSiteEvents2_onresizeendEventHandler onresizeend;

		// Token: 0x1400260A RID: 9738
		// (add) Token: 0x06013768 RID: 79720
		// (remove) Token: 0x06013769 RID: 79721
		event HTMLFrameSiteEvents2_onmousewheelEventHandler onmousewheel;

		// Token: 0x1400260B RID: 9739
		// (add) Token: 0x0601376A RID: 79722
		// (remove) Token: 0x0601376B RID: 79723
		event HTMLFrameSiteEvents2_onloadEventHandler onload;
	}
}
