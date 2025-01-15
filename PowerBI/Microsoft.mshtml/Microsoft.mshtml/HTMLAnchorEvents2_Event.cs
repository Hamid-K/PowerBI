using System;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x020003DB RID: 987
	[ComVisible(false)]
	[ComEventInterface(typeof(HTMLAnchorEvents2\u0000), typeof(HTMLAnchorEvents2_EventProvider\u0000))]
	public interface HTMLAnchorEvents2_Event
	{
		// Token: 0x140006BD RID: 1725
		// (add) Token: 0x0600407B RID: 16507
		// (remove) Token: 0x0600407C RID: 16508
		event HTMLAnchorEvents2_onhelpEventHandler onhelp;

		// Token: 0x140006BE RID: 1726
		// (add) Token: 0x0600407D RID: 16509
		// (remove) Token: 0x0600407E RID: 16510
		event HTMLAnchorEvents2_onclickEventHandler onclick;

		// Token: 0x140006BF RID: 1727
		// (add) Token: 0x0600407F RID: 16511
		// (remove) Token: 0x06004080 RID: 16512
		event HTMLAnchorEvents2_ondblclickEventHandler ondblclick;

		// Token: 0x140006C0 RID: 1728
		// (add) Token: 0x06004081 RID: 16513
		// (remove) Token: 0x06004082 RID: 16514
		event HTMLAnchorEvents2_onkeypressEventHandler onkeypress;

		// Token: 0x140006C1 RID: 1729
		// (add) Token: 0x06004083 RID: 16515
		// (remove) Token: 0x06004084 RID: 16516
		event HTMLAnchorEvents2_onkeydownEventHandler onkeydown;

		// Token: 0x140006C2 RID: 1730
		// (add) Token: 0x06004085 RID: 16517
		// (remove) Token: 0x06004086 RID: 16518
		event HTMLAnchorEvents2_onkeyupEventHandler onkeyup;

		// Token: 0x140006C3 RID: 1731
		// (add) Token: 0x06004087 RID: 16519
		// (remove) Token: 0x06004088 RID: 16520
		event HTMLAnchorEvents2_onmouseoutEventHandler onmouseout;

		// Token: 0x140006C4 RID: 1732
		// (add) Token: 0x06004089 RID: 16521
		// (remove) Token: 0x0600408A RID: 16522
		event HTMLAnchorEvents2_onmouseoverEventHandler onmouseover;

		// Token: 0x140006C5 RID: 1733
		// (add) Token: 0x0600408B RID: 16523
		// (remove) Token: 0x0600408C RID: 16524
		event HTMLAnchorEvents2_onmousemoveEventHandler onmousemove;

		// Token: 0x140006C6 RID: 1734
		// (add) Token: 0x0600408D RID: 16525
		// (remove) Token: 0x0600408E RID: 16526
		event HTMLAnchorEvents2_onmousedownEventHandler onmousedown;

		// Token: 0x140006C7 RID: 1735
		// (add) Token: 0x0600408F RID: 16527
		// (remove) Token: 0x06004090 RID: 16528
		event HTMLAnchorEvents2_onmouseupEventHandler onmouseup;

		// Token: 0x140006C8 RID: 1736
		// (add) Token: 0x06004091 RID: 16529
		// (remove) Token: 0x06004092 RID: 16530
		event HTMLAnchorEvents2_onselectstartEventHandler onselectstart;

		// Token: 0x140006C9 RID: 1737
		// (add) Token: 0x06004093 RID: 16531
		// (remove) Token: 0x06004094 RID: 16532
		event HTMLAnchorEvents2_onfilterchangeEventHandler onfilterchange;

		// Token: 0x140006CA RID: 1738
		// (add) Token: 0x06004095 RID: 16533
		// (remove) Token: 0x06004096 RID: 16534
		event HTMLAnchorEvents2_ondragstartEventHandler ondragstart;

		// Token: 0x140006CB RID: 1739
		// (add) Token: 0x06004097 RID: 16535
		// (remove) Token: 0x06004098 RID: 16536
		event HTMLAnchorEvents2_onbeforeupdateEventHandler onbeforeupdate;

		// Token: 0x140006CC RID: 1740
		// (add) Token: 0x06004099 RID: 16537
		// (remove) Token: 0x0600409A RID: 16538
		event HTMLAnchorEvents2_onafterupdateEventHandler onafterupdate;

		// Token: 0x140006CD RID: 1741
		// (add) Token: 0x0600409B RID: 16539
		// (remove) Token: 0x0600409C RID: 16540
		event HTMLAnchorEvents2_onerrorupdateEventHandler onerrorupdate;

		// Token: 0x140006CE RID: 1742
		// (add) Token: 0x0600409D RID: 16541
		// (remove) Token: 0x0600409E RID: 16542
		event HTMLAnchorEvents2_onrowexitEventHandler onrowexit;

		// Token: 0x140006CF RID: 1743
		// (add) Token: 0x0600409F RID: 16543
		// (remove) Token: 0x060040A0 RID: 16544
		event HTMLAnchorEvents2_onrowenterEventHandler onrowenter;

		// Token: 0x140006D0 RID: 1744
		// (add) Token: 0x060040A1 RID: 16545
		// (remove) Token: 0x060040A2 RID: 16546
		event HTMLAnchorEvents2_ondatasetchangedEventHandler ondatasetchanged;

		// Token: 0x140006D1 RID: 1745
		// (add) Token: 0x060040A3 RID: 16547
		// (remove) Token: 0x060040A4 RID: 16548
		event HTMLAnchorEvents2_ondataavailableEventHandler ondataavailable;

		// Token: 0x140006D2 RID: 1746
		// (add) Token: 0x060040A5 RID: 16549
		// (remove) Token: 0x060040A6 RID: 16550
		event HTMLAnchorEvents2_ondatasetcompleteEventHandler ondatasetcomplete;

		// Token: 0x140006D3 RID: 1747
		// (add) Token: 0x060040A7 RID: 16551
		// (remove) Token: 0x060040A8 RID: 16552
		event HTMLAnchorEvents2_onlosecaptureEventHandler onlosecapture;

		// Token: 0x140006D4 RID: 1748
		// (add) Token: 0x060040A9 RID: 16553
		// (remove) Token: 0x060040AA RID: 16554
		event HTMLAnchorEvents2_onpropertychangeEventHandler onpropertychange;

		// Token: 0x140006D5 RID: 1749
		// (add) Token: 0x060040AB RID: 16555
		// (remove) Token: 0x060040AC RID: 16556
		event HTMLAnchorEvents2_onscrollEventHandler onscroll;

		// Token: 0x140006D6 RID: 1750
		// (add) Token: 0x060040AD RID: 16557
		// (remove) Token: 0x060040AE RID: 16558
		event HTMLAnchorEvents2_onfocusEventHandler onfocus;

		// Token: 0x140006D7 RID: 1751
		// (add) Token: 0x060040AF RID: 16559
		// (remove) Token: 0x060040B0 RID: 16560
		event HTMLAnchorEvents2_onblurEventHandler onblur;

		// Token: 0x140006D8 RID: 1752
		// (add) Token: 0x060040B1 RID: 16561
		// (remove) Token: 0x060040B2 RID: 16562
		event HTMLAnchorEvents2_onresizeEventHandler onresize;

		// Token: 0x140006D9 RID: 1753
		// (add) Token: 0x060040B3 RID: 16563
		// (remove) Token: 0x060040B4 RID: 16564
		event HTMLAnchorEvents2_ondragEventHandler ondrag;

		// Token: 0x140006DA RID: 1754
		// (add) Token: 0x060040B5 RID: 16565
		// (remove) Token: 0x060040B6 RID: 16566
		event HTMLAnchorEvents2_ondragendEventHandler ondragend;

		// Token: 0x140006DB RID: 1755
		// (add) Token: 0x060040B7 RID: 16567
		// (remove) Token: 0x060040B8 RID: 16568
		event HTMLAnchorEvents2_ondragenterEventHandler ondragenter;

		// Token: 0x140006DC RID: 1756
		// (add) Token: 0x060040B9 RID: 16569
		// (remove) Token: 0x060040BA RID: 16570
		event HTMLAnchorEvents2_ondragoverEventHandler ondragover;

		// Token: 0x140006DD RID: 1757
		// (add) Token: 0x060040BB RID: 16571
		// (remove) Token: 0x060040BC RID: 16572
		event HTMLAnchorEvents2_ondragleaveEventHandler ondragleave;

		// Token: 0x140006DE RID: 1758
		// (add) Token: 0x060040BD RID: 16573
		// (remove) Token: 0x060040BE RID: 16574
		event HTMLAnchorEvents2_ondropEventHandler ondrop;

		// Token: 0x140006DF RID: 1759
		// (add) Token: 0x060040BF RID: 16575
		// (remove) Token: 0x060040C0 RID: 16576
		event HTMLAnchorEvents2_onbeforecutEventHandler onbeforecut;

		// Token: 0x140006E0 RID: 1760
		// (add) Token: 0x060040C1 RID: 16577
		// (remove) Token: 0x060040C2 RID: 16578
		event HTMLAnchorEvents2_oncutEventHandler oncut;

		// Token: 0x140006E1 RID: 1761
		// (add) Token: 0x060040C3 RID: 16579
		// (remove) Token: 0x060040C4 RID: 16580
		event HTMLAnchorEvents2_onbeforecopyEventHandler onbeforecopy;

		// Token: 0x140006E2 RID: 1762
		// (add) Token: 0x060040C5 RID: 16581
		// (remove) Token: 0x060040C6 RID: 16582
		event HTMLAnchorEvents2_oncopyEventHandler oncopy;

		// Token: 0x140006E3 RID: 1763
		// (add) Token: 0x060040C7 RID: 16583
		// (remove) Token: 0x060040C8 RID: 16584
		event HTMLAnchorEvents2_onbeforepasteEventHandler onbeforepaste;

		// Token: 0x140006E4 RID: 1764
		// (add) Token: 0x060040C9 RID: 16585
		// (remove) Token: 0x060040CA RID: 16586
		event HTMLAnchorEvents2_onpasteEventHandler onpaste;

		// Token: 0x140006E5 RID: 1765
		// (add) Token: 0x060040CB RID: 16587
		// (remove) Token: 0x060040CC RID: 16588
		event HTMLAnchorEvents2_oncontextmenuEventHandler oncontextmenu;

		// Token: 0x140006E6 RID: 1766
		// (add) Token: 0x060040CD RID: 16589
		// (remove) Token: 0x060040CE RID: 16590
		event HTMLAnchorEvents2_onrowsdeleteEventHandler onrowsdelete;

		// Token: 0x140006E7 RID: 1767
		// (add) Token: 0x060040CF RID: 16591
		// (remove) Token: 0x060040D0 RID: 16592
		event HTMLAnchorEvents2_onrowsinsertedEventHandler onrowsinserted;

		// Token: 0x140006E8 RID: 1768
		// (add) Token: 0x060040D1 RID: 16593
		// (remove) Token: 0x060040D2 RID: 16594
		event HTMLAnchorEvents2_oncellchangeEventHandler oncellchange;

		// Token: 0x140006E9 RID: 1769
		// (add) Token: 0x060040D3 RID: 16595
		// (remove) Token: 0x060040D4 RID: 16596
		event HTMLAnchorEvents2_onreadystatechangeEventHandler onreadystatechange;

		// Token: 0x140006EA RID: 1770
		// (add) Token: 0x060040D5 RID: 16597
		// (remove) Token: 0x060040D6 RID: 16598
		event HTMLAnchorEvents2_onlayoutcompleteEventHandler onlayoutcomplete;

		// Token: 0x140006EB RID: 1771
		// (add) Token: 0x060040D7 RID: 16599
		// (remove) Token: 0x060040D8 RID: 16600
		event HTMLAnchorEvents2_onpageEventHandler onpage;

		// Token: 0x140006EC RID: 1772
		// (add) Token: 0x060040D9 RID: 16601
		// (remove) Token: 0x060040DA RID: 16602
		event HTMLAnchorEvents2_onmouseenterEventHandler onmouseenter;

		// Token: 0x140006ED RID: 1773
		// (add) Token: 0x060040DB RID: 16603
		// (remove) Token: 0x060040DC RID: 16604
		event HTMLAnchorEvents2_onmouseleaveEventHandler onmouseleave;

		// Token: 0x140006EE RID: 1774
		// (add) Token: 0x060040DD RID: 16605
		// (remove) Token: 0x060040DE RID: 16606
		event HTMLAnchorEvents2_onactivateEventHandler onactivate;

		// Token: 0x140006EF RID: 1775
		// (add) Token: 0x060040DF RID: 16607
		// (remove) Token: 0x060040E0 RID: 16608
		event HTMLAnchorEvents2_ondeactivateEventHandler ondeactivate;

		// Token: 0x140006F0 RID: 1776
		// (add) Token: 0x060040E1 RID: 16609
		// (remove) Token: 0x060040E2 RID: 16610
		event HTMLAnchorEvents2_onbeforedeactivateEventHandler onbeforedeactivate;

		// Token: 0x140006F1 RID: 1777
		// (add) Token: 0x060040E3 RID: 16611
		// (remove) Token: 0x060040E4 RID: 16612
		event HTMLAnchorEvents2_onbeforeactivateEventHandler onbeforeactivate;

		// Token: 0x140006F2 RID: 1778
		// (add) Token: 0x060040E5 RID: 16613
		// (remove) Token: 0x060040E6 RID: 16614
		event HTMLAnchorEvents2_onfocusinEventHandler onfocusin;

		// Token: 0x140006F3 RID: 1779
		// (add) Token: 0x060040E7 RID: 16615
		// (remove) Token: 0x060040E8 RID: 16616
		event HTMLAnchorEvents2_onfocusoutEventHandler onfocusout;

		// Token: 0x140006F4 RID: 1780
		// (add) Token: 0x060040E9 RID: 16617
		// (remove) Token: 0x060040EA RID: 16618
		event HTMLAnchorEvents2_onmoveEventHandler onmove;

		// Token: 0x140006F5 RID: 1781
		// (add) Token: 0x060040EB RID: 16619
		// (remove) Token: 0x060040EC RID: 16620
		event HTMLAnchorEvents2_oncontrolselectEventHandler oncontrolselect;

		// Token: 0x140006F6 RID: 1782
		// (add) Token: 0x060040ED RID: 16621
		// (remove) Token: 0x060040EE RID: 16622
		event HTMLAnchorEvents2_onmovestartEventHandler onmovestart;

		// Token: 0x140006F7 RID: 1783
		// (add) Token: 0x060040EF RID: 16623
		// (remove) Token: 0x060040F0 RID: 16624
		event HTMLAnchorEvents2_onmoveendEventHandler onmoveend;

		// Token: 0x140006F8 RID: 1784
		// (add) Token: 0x060040F1 RID: 16625
		// (remove) Token: 0x060040F2 RID: 16626
		event HTMLAnchorEvents2_onresizestartEventHandler onresizestart;

		// Token: 0x140006F9 RID: 1785
		// (add) Token: 0x060040F3 RID: 16627
		// (remove) Token: 0x060040F4 RID: 16628
		event HTMLAnchorEvents2_onresizeendEventHandler onresizeend;

		// Token: 0x140006FA RID: 1786
		// (add) Token: 0x060040F5 RID: 16629
		// (remove) Token: 0x060040F6 RID: 16630
		event HTMLAnchorEvents2_onmousewheelEventHandler onmousewheel;
	}
}
