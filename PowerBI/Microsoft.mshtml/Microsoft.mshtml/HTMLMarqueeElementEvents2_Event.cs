using System;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x0200072A RID: 1834
	[ComEventInterface(typeof(HTMLMarqueeElementEvents2\u0000), typeof(HTMLMarqueeElementEvents2_EventProvider\u0000))]
	[ComVisible(false)]
	public interface HTMLMarqueeElementEvents2_Event
	{
		// Token: 0x14001573 RID: 5491
		// (add) Token: 0x0600AE6A RID: 44650
		// (remove) Token: 0x0600AE6B RID: 44651
		event HTMLMarqueeElementEvents2_onhelpEventHandler onhelp;

		// Token: 0x14001574 RID: 5492
		// (add) Token: 0x0600AE6C RID: 44652
		// (remove) Token: 0x0600AE6D RID: 44653
		event HTMLMarqueeElementEvents2_onclickEventHandler onclick;

		// Token: 0x14001575 RID: 5493
		// (add) Token: 0x0600AE6E RID: 44654
		// (remove) Token: 0x0600AE6F RID: 44655
		event HTMLMarqueeElementEvents2_ondblclickEventHandler ondblclick;

		// Token: 0x14001576 RID: 5494
		// (add) Token: 0x0600AE70 RID: 44656
		// (remove) Token: 0x0600AE71 RID: 44657
		event HTMLMarqueeElementEvents2_onkeypressEventHandler onkeypress;

		// Token: 0x14001577 RID: 5495
		// (add) Token: 0x0600AE72 RID: 44658
		// (remove) Token: 0x0600AE73 RID: 44659
		event HTMLMarqueeElementEvents2_onkeydownEventHandler onkeydown;

		// Token: 0x14001578 RID: 5496
		// (add) Token: 0x0600AE74 RID: 44660
		// (remove) Token: 0x0600AE75 RID: 44661
		event HTMLMarqueeElementEvents2_onkeyupEventHandler onkeyup;

		// Token: 0x14001579 RID: 5497
		// (add) Token: 0x0600AE76 RID: 44662
		// (remove) Token: 0x0600AE77 RID: 44663
		event HTMLMarqueeElementEvents2_onmouseoutEventHandler onmouseout;

		// Token: 0x1400157A RID: 5498
		// (add) Token: 0x0600AE78 RID: 44664
		// (remove) Token: 0x0600AE79 RID: 44665
		event HTMLMarqueeElementEvents2_onmouseoverEventHandler onmouseover;

		// Token: 0x1400157B RID: 5499
		// (add) Token: 0x0600AE7A RID: 44666
		// (remove) Token: 0x0600AE7B RID: 44667
		event HTMLMarqueeElementEvents2_onmousemoveEventHandler onmousemove;

		// Token: 0x1400157C RID: 5500
		// (add) Token: 0x0600AE7C RID: 44668
		// (remove) Token: 0x0600AE7D RID: 44669
		event HTMLMarqueeElementEvents2_onmousedownEventHandler onmousedown;

		// Token: 0x1400157D RID: 5501
		// (add) Token: 0x0600AE7E RID: 44670
		// (remove) Token: 0x0600AE7F RID: 44671
		event HTMLMarqueeElementEvents2_onmouseupEventHandler onmouseup;

		// Token: 0x1400157E RID: 5502
		// (add) Token: 0x0600AE80 RID: 44672
		// (remove) Token: 0x0600AE81 RID: 44673
		event HTMLMarqueeElementEvents2_onselectstartEventHandler onselectstart;

		// Token: 0x1400157F RID: 5503
		// (add) Token: 0x0600AE82 RID: 44674
		// (remove) Token: 0x0600AE83 RID: 44675
		event HTMLMarqueeElementEvents2_onfilterchangeEventHandler onfilterchange;

		// Token: 0x14001580 RID: 5504
		// (add) Token: 0x0600AE84 RID: 44676
		// (remove) Token: 0x0600AE85 RID: 44677
		event HTMLMarqueeElementEvents2_ondragstartEventHandler ondragstart;

		// Token: 0x14001581 RID: 5505
		// (add) Token: 0x0600AE86 RID: 44678
		// (remove) Token: 0x0600AE87 RID: 44679
		event HTMLMarqueeElementEvents2_onbeforeupdateEventHandler onbeforeupdate;

		// Token: 0x14001582 RID: 5506
		// (add) Token: 0x0600AE88 RID: 44680
		// (remove) Token: 0x0600AE89 RID: 44681
		event HTMLMarqueeElementEvents2_onafterupdateEventHandler onafterupdate;

		// Token: 0x14001583 RID: 5507
		// (add) Token: 0x0600AE8A RID: 44682
		// (remove) Token: 0x0600AE8B RID: 44683
		event HTMLMarqueeElementEvents2_onerrorupdateEventHandler onerrorupdate;

		// Token: 0x14001584 RID: 5508
		// (add) Token: 0x0600AE8C RID: 44684
		// (remove) Token: 0x0600AE8D RID: 44685
		event HTMLMarqueeElementEvents2_onrowexitEventHandler onrowexit;

		// Token: 0x14001585 RID: 5509
		// (add) Token: 0x0600AE8E RID: 44686
		// (remove) Token: 0x0600AE8F RID: 44687
		event HTMLMarqueeElementEvents2_onrowenterEventHandler onrowenter;

		// Token: 0x14001586 RID: 5510
		// (add) Token: 0x0600AE90 RID: 44688
		// (remove) Token: 0x0600AE91 RID: 44689
		event HTMLMarqueeElementEvents2_ondatasetchangedEventHandler ondatasetchanged;

		// Token: 0x14001587 RID: 5511
		// (add) Token: 0x0600AE92 RID: 44690
		// (remove) Token: 0x0600AE93 RID: 44691
		event HTMLMarqueeElementEvents2_ondataavailableEventHandler ondataavailable;

		// Token: 0x14001588 RID: 5512
		// (add) Token: 0x0600AE94 RID: 44692
		// (remove) Token: 0x0600AE95 RID: 44693
		event HTMLMarqueeElementEvents2_ondatasetcompleteEventHandler ondatasetcomplete;

		// Token: 0x14001589 RID: 5513
		// (add) Token: 0x0600AE96 RID: 44694
		// (remove) Token: 0x0600AE97 RID: 44695
		event HTMLMarqueeElementEvents2_onlosecaptureEventHandler onlosecapture;

		// Token: 0x1400158A RID: 5514
		// (add) Token: 0x0600AE98 RID: 44696
		// (remove) Token: 0x0600AE99 RID: 44697
		event HTMLMarqueeElementEvents2_onpropertychangeEventHandler onpropertychange;

		// Token: 0x1400158B RID: 5515
		// (add) Token: 0x0600AE9A RID: 44698
		// (remove) Token: 0x0600AE9B RID: 44699
		event HTMLMarqueeElementEvents2_onscrollEventHandler onscroll;

		// Token: 0x1400158C RID: 5516
		// (add) Token: 0x0600AE9C RID: 44700
		// (remove) Token: 0x0600AE9D RID: 44701
		event HTMLMarqueeElementEvents2_onfocusEventHandler onfocus;

		// Token: 0x1400158D RID: 5517
		// (add) Token: 0x0600AE9E RID: 44702
		// (remove) Token: 0x0600AE9F RID: 44703
		event HTMLMarqueeElementEvents2_onblurEventHandler onblur;

		// Token: 0x1400158E RID: 5518
		// (add) Token: 0x0600AEA0 RID: 44704
		// (remove) Token: 0x0600AEA1 RID: 44705
		event HTMLMarqueeElementEvents2_onresizeEventHandler onresize;

		// Token: 0x1400158F RID: 5519
		// (add) Token: 0x0600AEA2 RID: 44706
		// (remove) Token: 0x0600AEA3 RID: 44707
		event HTMLMarqueeElementEvents2_ondragEventHandler ondrag;

		// Token: 0x14001590 RID: 5520
		// (add) Token: 0x0600AEA4 RID: 44708
		// (remove) Token: 0x0600AEA5 RID: 44709
		event HTMLMarqueeElementEvents2_ondragendEventHandler ondragend;

		// Token: 0x14001591 RID: 5521
		// (add) Token: 0x0600AEA6 RID: 44710
		// (remove) Token: 0x0600AEA7 RID: 44711
		event HTMLMarqueeElementEvents2_ondragenterEventHandler ondragenter;

		// Token: 0x14001592 RID: 5522
		// (add) Token: 0x0600AEA8 RID: 44712
		// (remove) Token: 0x0600AEA9 RID: 44713
		event HTMLMarqueeElementEvents2_ondragoverEventHandler ondragover;

		// Token: 0x14001593 RID: 5523
		// (add) Token: 0x0600AEAA RID: 44714
		// (remove) Token: 0x0600AEAB RID: 44715
		event HTMLMarqueeElementEvents2_ondragleaveEventHandler ondragleave;

		// Token: 0x14001594 RID: 5524
		// (add) Token: 0x0600AEAC RID: 44716
		// (remove) Token: 0x0600AEAD RID: 44717
		event HTMLMarqueeElementEvents2_ondropEventHandler ondrop;

		// Token: 0x14001595 RID: 5525
		// (add) Token: 0x0600AEAE RID: 44718
		// (remove) Token: 0x0600AEAF RID: 44719
		event HTMLMarqueeElementEvents2_onbeforecutEventHandler onbeforecut;

		// Token: 0x14001596 RID: 5526
		// (add) Token: 0x0600AEB0 RID: 44720
		// (remove) Token: 0x0600AEB1 RID: 44721
		event HTMLMarqueeElementEvents2_oncutEventHandler oncut;

		// Token: 0x14001597 RID: 5527
		// (add) Token: 0x0600AEB2 RID: 44722
		// (remove) Token: 0x0600AEB3 RID: 44723
		event HTMLMarqueeElementEvents2_onbeforecopyEventHandler onbeforecopy;

		// Token: 0x14001598 RID: 5528
		// (add) Token: 0x0600AEB4 RID: 44724
		// (remove) Token: 0x0600AEB5 RID: 44725
		event HTMLMarqueeElementEvents2_oncopyEventHandler oncopy;

		// Token: 0x14001599 RID: 5529
		// (add) Token: 0x0600AEB6 RID: 44726
		// (remove) Token: 0x0600AEB7 RID: 44727
		event HTMLMarqueeElementEvents2_onbeforepasteEventHandler onbeforepaste;

		// Token: 0x1400159A RID: 5530
		// (add) Token: 0x0600AEB8 RID: 44728
		// (remove) Token: 0x0600AEB9 RID: 44729
		event HTMLMarqueeElementEvents2_onpasteEventHandler onpaste;

		// Token: 0x1400159B RID: 5531
		// (add) Token: 0x0600AEBA RID: 44730
		// (remove) Token: 0x0600AEBB RID: 44731
		event HTMLMarqueeElementEvents2_oncontextmenuEventHandler oncontextmenu;

		// Token: 0x1400159C RID: 5532
		// (add) Token: 0x0600AEBC RID: 44732
		// (remove) Token: 0x0600AEBD RID: 44733
		event HTMLMarqueeElementEvents2_onrowsdeleteEventHandler onrowsdelete;

		// Token: 0x1400159D RID: 5533
		// (add) Token: 0x0600AEBE RID: 44734
		// (remove) Token: 0x0600AEBF RID: 44735
		event HTMLMarqueeElementEvents2_onrowsinsertedEventHandler onrowsinserted;

		// Token: 0x1400159E RID: 5534
		// (add) Token: 0x0600AEC0 RID: 44736
		// (remove) Token: 0x0600AEC1 RID: 44737
		event HTMLMarqueeElementEvents2_oncellchangeEventHandler oncellchange;

		// Token: 0x1400159F RID: 5535
		// (add) Token: 0x0600AEC2 RID: 44738
		// (remove) Token: 0x0600AEC3 RID: 44739
		event HTMLMarqueeElementEvents2_onreadystatechangeEventHandler onreadystatechange;

		// Token: 0x140015A0 RID: 5536
		// (add) Token: 0x0600AEC4 RID: 44740
		// (remove) Token: 0x0600AEC5 RID: 44741
		event HTMLMarqueeElementEvents2_onlayoutcompleteEventHandler onlayoutcomplete;

		// Token: 0x140015A1 RID: 5537
		// (add) Token: 0x0600AEC6 RID: 44742
		// (remove) Token: 0x0600AEC7 RID: 44743
		event HTMLMarqueeElementEvents2_onpageEventHandler onpage;

		// Token: 0x140015A2 RID: 5538
		// (add) Token: 0x0600AEC8 RID: 44744
		// (remove) Token: 0x0600AEC9 RID: 44745
		event HTMLMarqueeElementEvents2_onmouseenterEventHandler onmouseenter;

		// Token: 0x140015A3 RID: 5539
		// (add) Token: 0x0600AECA RID: 44746
		// (remove) Token: 0x0600AECB RID: 44747
		event HTMLMarqueeElementEvents2_onmouseleaveEventHandler onmouseleave;

		// Token: 0x140015A4 RID: 5540
		// (add) Token: 0x0600AECC RID: 44748
		// (remove) Token: 0x0600AECD RID: 44749
		event HTMLMarqueeElementEvents2_onactivateEventHandler onactivate;

		// Token: 0x140015A5 RID: 5541
		// (add) Token: 0x0600AECE RID: 44750
		// (remove) Token: 0x0600AECF RID: 44751
		event HTMLMarqueeElementEvents2_ondeactivateEventHandler ondeactivate;

		// Token: 0x140015A6 RID: 5542
		// (add) Token: 0x0600AED0 RID: 44752
		// (remove) Token: 0x0600AED1 RID: 44753
		event HTMLMarqueeElementEvents2_onbeforedeactivateEventHandler onbeforedeactivate;

		// Token: 0x140015A7 RID: 5543
		// (add) Token: 0x0600AED2 RID: 44754
		// (remove) Token: 0x0600AED3 RID: 44755
		event HTMLMarqueeElementEvents2_onbeforeactivateEventHandler onbeforeactivate;

		// Token: 0x140015A8 RID: 5544
		// (add) Token: 0x0600AED4 RID: 44756
		// (remove) Token: 0x0600AED5 RID: 44757
		event HTMLMarqueeElementEvents2_onfocusinEventHandler onfocusin;

		// Token: 0x140015A9 RID: 5545
		// (add) Token: 0x0600AED6 RID: 44758
		// (remove) Token: 0x0600AED7 RID: 44759
		event HTMLMarqueeElementEvents2_onfocusoutEventHandler onfocusout;

		// Token: 0x140015AA RID: 5546
		// (add) Token: 0x0600AED8 RID: 44760
		// (remove) Token: 0x0600AED9 RID: 44761
		event HTMLMarqueeElementEvents2_onmoveEventHandler onmove;

		// Token: 0x140015AB RID: 5547
		// (add) Token: 0x0600AEDA RID: 44762
		// (remove) Token: 0x0600AEDB RID: 44763
		event HTMLMarqueeElementEvents2_oncontrolselectEventHandler oncontrolselect;

		// Token: 0x140015AC RID: 5548
		// (add) Token: 0x0600AEDC RID: 44764
		// (remove) Token: 0x0600AEDD RID: 44765
		event HTMLMarqueeElementEvents2_onmovestartEventHandler onmovestart;

		// Token: 0x140015AD RID: 5549
		// (add) Token: 0x0600AEDE RID: 44766
		// (remove) Token: 0x0600AEDF RID: 44767
		event HTMLMarqueeElementEvents2_onmoveendEventHandler onmoveend;

		// Token: 0x140015AE RID: 5550
		// (add) Token: 0x0600AEE0 RID: 44768
		// (remove) Token: 0x0600AEE1 RID: 44769
		event HTMLMarqueeElementEvents2_onresizestartEventHandler onresizestart;

		// Token: 0x140015AF RID: 5551
		// (add) Token: 0x0600AEE2 RID: 44770
		// (remove) Token: 0x0600AEE3 RID: 44771
		event HTMLMarqueeElementEvents2_onresizeendEventHandler onresizeend;

		// Token: 0x140015B0 RID: 5552
		// (add) Token: 0x0600AEE4 RID: 44772
		// (remove) Token: 0x0600AEE5 RID: 44773
		event HTMLMarqueeElementEvents2_onmousewheelEventHandler onmousewheel;

		// Token: 0x140015B1 RID: 5553
		// (add) Token: 0x0600AEE6 RID: 44774
		// (remove) Token: 0x0600AEE7 RID: 44775
		event HTMLMarqueeElementEvents2_onchangeEventHandler onchange;

		// Token: 0x140015B2 RID: 5554
		// (add) Token: 0x0600AEE8 RID: 44776
		// (remove) Token: 0x0600AEE9 RID: 44777
		event HTMLMarqueeElementEvents2_onselectEventHandler onselect;

		// Token: 0x140015B3 RID: 5555
		// (add) Token: 0x0600AEEA RID: 44778
		// (remove) Token: 0x0600AEEB RID: 44779
		event HTMLMarqueeElementEvents2_onbounceEventHandler onbounce;

		// Token: 0x140015B4 RID: 5556
		// (add) Token: 0x0600AEEC RID: 44780
		// (remove) Token: 0x0600AEED RID: 44781
		event HTMLMarqueeElementEvents2_onfinishEventHandler onfinish;

		// Token: 0x140015B5 RID: 5557
		// (add) Token: 0x0600AEEE RID: 44782
		// (remove) Token: 0x0600AEEF RID: 44783
		event HTMLMarqueeElementEvents2_onstartEventHandler onstart;
	}
}
