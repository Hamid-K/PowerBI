using System;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x0200022A RID: 554
	[ComVisible(false)]
	[ComEventInterface(typeof(HTMLFormElementEvents2\u0000), typeof(HTMLFormElementEvents2_EventProvider\u0000))]
	public interface HTMLFormElementEvents2_Event
	{
		// Token: 0x140002BF RID: 703
		// (add) Token: 0x06002166 RID: 8550
		// (remove) Token: 0x06002167 RID: 8551
		event HTMLFormElementEvents2_onhelpEventHandler onhelp;

		// Token: 0x140002C0 RID: 704
		// (add) Token: 0x06002168 RID: 8552
		// (remove) Token: 0x06002169 RID: 8553
		event HTMLFormElementEvents2_onclickEventHandler onclick;

		// Token: 0x140002C1 RID: 705
		// (add) Token: 0x0600216A RID: 8554
		// (remove) Token: 0x0600216B RID: 8555
		event HTMLFormElementEvents2_ondblclickEventHandler ondblclick;

		// Token: 0x140002C2 RID: 706
		// (add) Token: 0x0600216C RID: 8556
		// (remove) Token: 0x0600216D RID: 8557
		event HTMLFormElementEvents2_onkeypressEventHandler onkeypress;

		// Token: 0x140002C3 RID: 707
		// (add) Token: 0x0600216E RID: 8558
		// (remove) Token: 0x0600216F RID: 8559
		event HTMLFormElementEvents2_onkeydownEventHandler onkeydown;

		// Token: 0x140002C4 RID: 708
		// (add) Token: 0x06002170 RID: 8560
		// (remove) Token: 0x06002171 RID: 8561
		event HTMLFormElementEvents2_onkeyupEventHandler onkeyup;

		// Token: 0x140002C5 RID: 709
		// (add) Token: 0x06002172 RID: 8562
		// (remove) Token: 0x06002173 RID: 8563
		event HTMLFormElementEvents2_onmouseoutEventHandler onmouseout;

		// Token: 0x140002C6 RID: 710
		// (add) Token: 0x06002174 RID: 8564
		// (remove) Token: 0x06002175 RID: 8565
		event HTMLFormElementEvents2_onmouseoverEventHandler onmouseover;

		// Token: 0x140002C7 RID: 711
		// (add) Token: 0x06002176 RID: 8566
		// (remove) Token: 0x06002177 RID: 8567
		event HTMLFormElementEvents2_onmousemoveEventHandler onmousemove;

		// Token: 0x140002C8 RID: 712
		// (add) Token: 0x06002178 RID: 8568
		// (remove) Token: 0x06002179 RID: 8569
		event HTMLFormElementEvents2_onmousedownEventHandler onmousedown;

		// Token: 0x140002C9 RID: 713
		// (add) Token: 0x0600217A RID: 8570
		// (remove) Token: 0x0600217B RID: 8571
		event HTMLFormElementEvents2_onmouseupEventHandler onmouseup;

		// Token: 0x140002CA RID: 714
		// (add) Token: 0x0600217C RID: 8572
		// (remove) Token: 0x0600217D RID: 8573
		event HTMLFormElementEvents2_onselectstartEventHandler onselectstart;

		// Token: 0x140002CB RID: 715
		// (add) Token: 0x0600217E RID: 8574
		// (remove) Token: 0x0600217F RID: 8575
		event HTMLFormElementEvents2_onfilterchangeEventHandler onfilterchange;

		// Token: 0x140002CC RID: 716
		// (add) Token: 0x06002180 RID: 8576
		// (remove) Token: 0x06002181 RID: 8577
		event HTMLFormElementEvents2_ondragstartEventHandler ondragstart;

		// Token: 0x140002CD RID: 717
		// (add) Token: 0x06002182 RID: 8578
		// (remove) Token: 0x06002183 RID: 8579
		event HTMLFormElementEvents2_onbeforeupdateEventHandler onbeforeupdate;

		// Token: 0x140002CE RID: 718
		// (add) Token: 0x06002184 RID: 8580
		// (remove) Token: 0x06002185 RID: 8581
		event HTMLFormElementEvents2_onafterupdateEventHandler onafterupdate;

		// Token: 0x140002CF RID: 719
		// (add) Token: 0x06002186 RID: 8582
		// (remove) Token: 0x06002187 RID: 8583
		event HTMLFormElementEvents2_onerrorupdateEventHandler onerrorupdate;

		// Token: 0x140002D0 RID: 720
		// (add) Token: 0x06002188 RID: 8584
		// (remove) Token: 0x06002189 RID: 8585
		event HTMLFormElementEvents2_onrowexitEventHandler onrowexit;

		// Token: 0x140002D1 RID: 721
		// (add) Token: 0x0600218A RID: 8586
		// (remove) Token: 0x0600218B RID: 8587
		event HTMLFormElementEvents2_onrowenterEventHandler onrowenter;

		// Token: 0x140002D2 RID: 722
		// (add) Token: 0x0600218C RID: 8588
		// (remove) Token: 0x0600218D RID: 8589
		event HTMLFormElementEvents2_ondatasetchangedEventHandler ondatasetchanged;

		// Token: 0x140002D3 RID: 723
		// (add) Token: 0x0600218E RID: 8590
		// (remove) Token: 0x0600218F RID: 8591
		event HTMLFormElementEvents2_ondataavailableEventHandler ondataavailable;

		// Token: 0x140002D4 RID: 724
		// (add) Token: 0x06002190 RID: 8592
		// (remove) Token: 0x06002191 RID: 8593
		event HTMLFormElementEvents2_ondatasetcompleteEventHandler ondatasetcomplete;

		// Token: 0x140002D5 RID: 725
		// (add) Token: 0x06002192 RID: 8594
		// (remove) Token: 0x06002193 RID: 8595
		event HTMLFormElementEvents2_onlosecaptureEventHandler onlosecapture;

		// Token: 0x140002D6 RID: 726
		// (add) Token: 0x06002194 RID: 8596
		// (remove) Token: 0x06002195 RID: 8597
		event HTMLFormElementEvents2_onpropertychangeEventHandler onpropertychange;

		// Token: 0x140002D7 RID: 727
		// (add) Token: 0x06002196 RID: 8598
		// (remove) Token: 0x06002197 RID: 8599
		event HTMLFormElementEvents2_onscrollEventHandler onscroll;

		// Token: 0x140002D8 RID: 728
		// (add) Token: 0x06002198 RID: 8600
		// (remove) Token: 0x06002199 RID: 8601
		event HTMLFormElementEvents2_onfocusEventHandler onfocus;

		// Token: 0x140002D9 RID: 729
		// (add) Token: 0x0600219A RID: 8602
		// (remove) Token: 0x0600219B RID: 8603
		event HTMLFormElementEvents2_onblurEventHandler onblur;

		// Token: 0x140002DA RID: 730
		// (add) Token: 0x0600219C RID: 8604
		// (remove) Token: 0x0600219D RID: 8605
		event HTMLFormElementEvents2_onresizeEventHandler onresize;

		// Token: 0x140002DB RID: 731
		// (add) Token: 0x0600219E RID: 8606
		// (remove) Token: 0x0600219F RID: 8607
		event HTMLFormElementEvents2_ondragEventHandler ondrag;

		// Token: 0x140002DC RID: 732
		// (add) Token: 0x060021A0 RID: 8608
		// (remove) Token: 0x060021A1 RID: 8609
		event HTMLFormElementEvents2_ondragendEventHandler ondragend;

		// Token: 0x140002DD RID: 733
		// (add) Token: 0x060021A2 RID: 8610
		// (remove) Token: 0x060021A3 RID: 8611
		event HTMLFormElementEvents2_ondragenterEventHandler ondragenter;

		// Token: 0x140002DE RID: 734
		// (add) Token: 0x060021A4 RID: 8612
		// (remove) Token: 0x060021A5 RID: 8613
		event HTMLFormElementEvents2_ondragoverEventHandler ondragover;

		// Token: 0x140002DF RID: 735
		// (add) Token: 0x060021A6 RID: 8614
		// (remove) Token: 0x060021A7 RID: 8615
		event HTMLFormElementEvents2_ondragleaveEventHandler ondragleave;

		// Token: 0x140002E0 RID: 736
		// (add) Token: 0x060021A8 RID: 8616
		// (remove) Token: 0x060021A9 RID: 8617
		event HTMLFormElementEvents2_ondropEventHandler ondrop;

		// Token: 0x140002E1 RID: 737
		// (add) Token: 0x060021AA RID: 8618
		// (remove) Token: 0x060021AB RID: 8619
		event HTMLFormElementEvents2_onbeforecutEventHandler onbeforecut;

		// Token: 0x140002E2 RID: 738
		// (add) Token: 0x060021AC RID: 8620
		// (remove) Token: 0x060021AD RID: 8621
		event HTMLFormElementEvents2_oncutEventHandler oncut;

		// Token: 0x140002E3 RID: 739
		// (add) Token: 0x060021AE RID: 8622
		// (remove) Token: 0x060021AF RID: 8623
		event HTMLFormElementEvents2_onbeforecopyEventHandler onbeforecopy;

		// Token: 0x140002E4 RID: 740
		// (add) Token: 0x060021B0 RID: 8624
		// (remove) Token: 0x060021B1 RID: 8625
		event HTMLFormElementEvents2_oncopyEventHandler oncopy;

		// Token: 0x140002E5 RID: 741
		// (add) Token: 0x060021B2 RID: 8626
		// (remove) Token: 0x060021B3 RID: 8627
		event HTMLFormElementEvents2_onbeforepasteEventHandler onbeforepaste;

		// Token: 0x140002E6 RID: 742
		// (add) Token: 0x060021B4 RID: 8628
		// (remove) Token: 0x060021B5 RID: 8629
		event HTMLFormElementEvents2_onpasteEventHandler onpaste;

		// Token: 0x140002E7 RID: 743
		// (add) Token: 0x060021B6 RID: 8630
		// (remove) Token: 0x060021B7 RID: 8631
		event HTMLFormElementEvents2_oncontextmenuEventHandler oncontextmenu;

		// Token: 0x140002E8 RID: 744
		// (add) Token: 0x060021B8 RID: 8632
		// (remove) Token: 0x060021B9 RID: 8633
		event HTMLFormElementEvents2_onrowsdeleteEventHandler onrowsdelete;

		// Token: 0x140002E9 RID: 745
		// (add) Token: 0x060021BA RID: 8634
		// (remove) Token: 0x060021BB RID: 8635
		event HTMLFormElementEvents2_onrowsinsertedEventHandler onrowsinserted;

		// Token: 0x140002EA RID: 746
		// (add) Token: 0x060021BC RID: 8636
		// (remove) Token: 0x060021BD RID: 8637
		event HTMLFormElementEvents2_oncellchangeEventHandler oncellchange;

		// Token: 0x140002EB RID: 747
		// (add) Token: 0x060021BE RID: 8638
		// (remove) Token: 0x060021BF RID: 8639
		event HTMLFormElementEvents2_onreadystatechangeEventHandler onreadystatechange;

		// Token: 0x140002EC RID: 748
		// (add) Token: 0x060021C0 RID: 8640
		// (remove) Token: 0x060021C1 RID: 8641
		event HTMLFormElementEvents2_onlayoutcompleteEventHandler onlayoutcomplete;

		// Token: 0x140002ED RID: 749
		// (add) Token: 0x060021C2 RID: 8642
		// (remove) Token: 0x060021C3 RID: 8643
		event HTMLFormElementEvents2_onpageEventHandler onpage;

		// Token: 0x140002EE RID: 750
		// (add) Token: 0x060021C4 RID: 8644
		// (remove) Token: 0x060021C5 RID: 8645
		event HTMLFormElementEvents2_onmouseenterEventHandler onmouseenter;

		// Token: 0x140002EF RID: 751
		// (add) Token: 0x060021C6 RID: 8646
		// (remove) Token: 0x060021C7 RID: 8647
		event HTMLFormElementEvents2_onmouseleaveEventHandler onmouseleave;

		// Token: 0x140002F0 RID: 752
		// (add) Token: 0x060021C8 RID: 8648
		// (remove) Token: 0x060021C9 RID: 8649
		event HTMLFormElementEvents2_onactivateEventHandler onactivate;

		// Token: 0x140002F1 RID: 753
		// (add) Token: 0x060021CA RID: 8650
		// (remove) Token: 0x060021CB RID: 8651
		event HTMLFormElementEvents2_ondeactivateEventHandler ondeactivate;

		// Token: 0x140002F2 RID: 754
		// (add) Token: 0x060021CC RID: 8652
		// (remove) Token: 0x060021CD RID: 8653
		event HTMLFormElementEvents2_onbeforedeactivateEventHandler onbeforedeactivate;

		// Token: 0x140002F3 RID: 755
		// (add) Token: 0x060021CE RID: 8654
		// (remove) Token: 0x060021CF RID: 8655
		event HTMLFormElementEvents2_onbeforeactivateEventHandler onbeforeactivate;

		// Token: 0x140002F4 RID: 756
		// (add) Token: 0x060021D0 RID: 8656
		// (remove) Token: 0x060021D1 RID: 8657
		event HTMLFormElementEvents2_onfocusinEventHandler onfocusin;

		// Token: 0x140002F5 RID: 757
		// (add) Token: 0x060021D2 RID: 8658
		// (remove) Token: 0x060021D3 RID: 8659
		event HTMLFormElementEvents2_onfocusoutEventHandler onfocusout;

		// Token: 0x140002F6 RID: 758
		// (add) Token: 0x060021D4 RID: 8660
		// (remove) Token: 0x060021D5 RID: 8661
		event HTMLFormElementEvents2_onmoveEventHandler onmove;

		// Token: 0x140002F7 RID: 759
		// (add) Token: 0x060021D6 RID: 8662
		// (remove) Token: 0x060021D7 RID: 8663
		event HTMLFormElementEvents2_oncontrolselectEventHandler oncontrolselect;

		// Token: 0x140002F8 RID: 760
		// (add) Token: 0x060021D8 RID: 8664
		// (remove) Token: 0x060021D9 RID: 8665
		event HTMLFormElementEvents2_onmovestartEventHandler onmovestart;

		// Token: 0x140002F9 RID: 761
		// (add) Token: 0x060021DA RID: 8666
		// (remove) Token: 0x060021DB RID: 8667
		event HTMLFormElementEvents2_onmoveendEventHandler onmoveend;

		// Token: 0x140002FA RID: 762
		// (add) Token: 0x060021DC RID: 8668
		// (remove) Token: 0x060021DD RID: 8669
		event HTMLFormElementEvents2_onresizestartEventHandler onresizestart;

		// Token: 0x140002FB RID: 763
		// (add) Token: 0x060021DE RID: 8670
		// (remove) Token: 0x060021DF RID: 8671
		event HTMLFormElementEvents2_onresizeendEventHandler onresizeend;

		// Token: 0x140002FC RID: 764
		// (add) Token: 0x060021E0 RID: 8672
		// (remove) Token: 0x060021E1 RID: 8673
		event HTMLFormElementEvents2_onmousewheelEventHandler onmousewheel;

		// Token: 0x140002FD RID: 765
		// (add) Token: 0x060021E2 RID: 8674
		// (remove) Token: 0x060021E3 RID: 8675
		event HTMLFormElementEvents2_onsubmitEventHandler onsubmit;

		// Token: 0x140002FE RID: 766
		// (add) Token: 0x060021E4 RID: 8676
		// (remove) Token: 0x060021E5 RID: 8677
		event HTMLFormElementEvents2_onresetEventHandler onreset;
	}
}
