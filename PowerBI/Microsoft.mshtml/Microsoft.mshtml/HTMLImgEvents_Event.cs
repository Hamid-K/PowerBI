using System;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000280 RID: 640
	[ComVisible(false)]
	[ComEventInterface(typeof(HTMLImgEvents\u0000), typeof(HTMLImgEvents_EventProvider\u0000))]
	public interface HTMLImgEvents_Event
	{
		// Token: 0x140003FF RID: 1023
		// (add) Token: 0x06002D25 RID: 11557
		// (remove) Token: 0x06002D26 RID: 11558
		event HTMLImgEvents_onhelpEventHandler onhelp;

		// Token: 0x14000400 RID: 1024
		// (add) Token: 0x06002D27 RID: 11559
		// (remove) Token: 0x06002D28 RID: 11560
		event HTMLImgEvents_onclickEventHandler onclick;

		// Token: 0x14000401 RID: 1025
		// (add) Token: 0x06002D29 RID: 11561
		// (remove) Token: 0x06002D2A RID: 11562
		event HTMLImgEvents_ondblclickEventHandler ondblclick;

		// Token: 0x14000402 RID: 1026
		// (add) Token: 0x06002D2B RID: 11563
		// (remove) Token: 0x06002D2C RID: 11564
		event HTMLImgEvents_onkeypressEventHandler onkeypress;

		// Token: 0x14000403 RID: 1027
		// (add) Token: 0x06002D2D RID: 11565
		// (remove) Token: 0x06002D2E RID: 11566
		event HTMLImgEvents_onkeydownEventHandler onkeydown;

		// Token: 0x14000404 RID: 1028
		// (add) Token: 0x06002D2F RID: 11567
		// (remove) Token: 0x06002D30 RID: 11568
		event HTMLImgEvents_onkeyupEventHandler onkeyup;

		// Token: 0x14000405 RID: 1029
		// (add) Token: 0x06002D31 RID: 11569
		// (remove) Token: 0x06002D32 RID: 11570
		event HTMLImgEvents_onmouseoutEventHandler onmouseout;

		// Token: 0x14000406 RID: 1030
		// (add) Token: 0x06002D33 RID: 11571
		// (remove) Token: 0x06002D34 RID: 11572
		event HTMLImgEvents_onmouseoverEventHandler onmouseover;

		// Token: 0x14000407 RID: 1031
		// (add) Token: 0x06002D35 RID: 11573
		// (remove) Token: 0x06002D36 RID: 11574
		event HTMLImgEvents_onmousemoveEventHandler onmousemove;

		// Token: 0x14000408 RID: 1032
		// (add) Token: 0x06002D37 RID: 11575
		// (remove) Token: 0x06002D38 RID: 11576
		event HTMLImgEvents_onmousedownEventHandler onmousedown;

		// Token: 0x14000409 RID: 1033
		// (add) Token: 0x06002D39 RID: 11577
		// (remove) Token: 0x06002D3A RID: 11578
		event HTMLImgEvents_onmouseupEventHandler onmouseup;

		// Token: 0x1400040A RID: 1034
		// (add) Token: 0x06002D3B RID: 11579
		// (remove) Token: 0x06002D3C RID: 11580
		event HTMLImgEvents_onselectstartEventHandler onselectstart;

		// Token: 0x1400040B RID: 1035
		// (add) Token: 0x06002D3D RID: 11581
		// (remove) Token: 0x06002D3E RID: 11582
		event HTMLImgEvents_onfilterchangeEventHandler onfilterchange;

		// Token: 0x1400040C RID: 1036
		// (add) Token: 0x06002D3F RID: 11583
		// (remove) Token: 0x06002D40 RID: 11584
		event HTMLImgEvents_ondragstartEventHandler ondragstart;

		// Token: 0x1400040D RID: 1037
		// (add) Token: 0x06002D41 RID: 11585
		// (remove) Token: 0x06002D42 RID: 11586
		event HTMLImgEvents_onbeforeupdateEventHandler onbeforeupdate;

		// Token: 0x1400040E RID: 1038
		// (add) Token: 0x06002D43 RID: 11587
		// (remove) Token: 0x06002D44 RID: 11588
		event HTMLImgEvents_onafterupdateEventHandler onafterupdate;

		// Token: 0x1400040F RID: 1039
		// (add) Token: 0x06002D45 RID: 11589
		// (remove) Token: 0x06002D46 RID: 11590
		event HTMLImgEvents_onerrorupdateEventHandler onerrorupdate;

		// Token: 0x14000410 RID: 1040
		// (add) Token: 0x06002D47 RID: 11591
		// (remove) Token: 0x06002D48 RID: 11592
		event HTMLImgEvents_onrowexitEventHandler onrowexit;

		// Token: 0x14000411 RID: 1041
		// (add) Token: 0x06002D49 RID: 11593
		// (remove) Token: 0x06002D4A RID: 11594
		event HTMLImgEvents_onrowenterEventHandler onrowenter;

		// Token: 0x14000412 RID: 1042
		// (add) Token: 0x06002D4B RID: 11595
		// (remove) Token: 0x06002D4C RID: 11596
		event HTMLImgEvents_ondatasetchangedEventHandler ondatasetchanged;

		// Token: 0x14000413 RID: 1043
		// (add) Token: 0x06002D4D RID: 11597
		// (remove) Token: 0x06002D4E RID: 11598
		event HTMLImgEvents_ondataavailableEventHandler ondataavailable;

		// Token: 0x14000414 RID: 1044
		// (add) Token: 0x06002D4F RID: 11599
		// (remove) Token: 0x06002D50 RID: 11600
		event HTMLImgEvents_ondatasetcompleteEventHandler ondatasetcomplete;

		// Token: 0x14000415 RID: 1045
		// (add) Token: 0x06002D51 RID: 11601
		// (remove) Token: 0x06002D52 RID: 11602
		event HTMLImgEvents_onlosecaptureEventHandler onlosecapture;

		// Token: 0x14000416 RID: 1046
		// (add) Token: 0x06002D53 RID: 11603
		// (remove) Token: 0x06002D54 RID: 11604
		event HTMLImgEvents_onpropertychangeEventHandler onpropertychange;

		// Token: 0x14000417 RID: 1047
		// (add) Token: 0x06002D55 RID: 11605
		// (remove) Token: 0x06002D56 RID: 11606
		event HTMLImgEvents_onscrollEventHandler onscroll;

		// Token: 0x14000418 RID: 1048
		// (add) Token: 0x06002D57 RID: 11607
		// (remove) Token: 0x06002D58 RID: 11608
		event HTMLImgEvents_onfocusEventHandler onfocus;

		// Token: 0x14000419 RID: 1049
		// (add) Token: 0x06002D59 RID: 11609
		// (remove) Token: 0x06002D5A RID: 11610
		event HTMLImgEvents_onblurEventHandler onblur;

		// Token: 0x1400041A RID: 1050
		// (add) Token: 0x06002D5B RID: 11611
		// (remove) Token: 0x06002D5C RID: 11612
		event HTMLImgEvents_onresizeEventHandler onresize;

		// Token: 0x1400041B RID: 1051
		// (add) Token: 0x06002D5D RID: 11613
		// (remove) Token: 0x06002D5E RID: 11614
		event HTMLImgEvents_ondragEventHandler ondrag;

		// Token: 0x1400041C RID: 1052
		// (add) Token: 0x06002D5F RID: 11615
		// (remove) Token: 0x06002D60 RID: 11616
		event HTMLImgEvents_ondragendEventHandler ondragend;

		// Token: 0x1400041D RID: 1053
		// (add) Token: 0x06002D61 RID: 11617
		// (remove) Token: 0x06002D62 RID: 11618
		event HTMLImgEvents_ondragenterEventHandler ondragenter;

		// Token: 0x1400041E RID: 1054
		// (add) Token: 0x06002D63 RID: 11619
		// (remove) Token: 0x06002D64 RID: 11620
		event HTMLImgEvents_ondragoverEventHandler ondragover;

		// Token: 0x1400041F RID: 1055
		// (add) Token: 0x06002D65 RID: 11621
		// (remove) Token: 0x06002D66 RID: 11622
		event HTMLImgEvents_ondragleaveEventHandler ondragleave;

		// Token: 0x14000420 RID: 1056
		// (add) Token: 0x06002D67 RID: 11623
		// (remove) Token: 0x06002D68 RID: 11624
		event HTMLImgEvents_ondropEventHandler ondrop;

		// Token: 0x14000421 RID: 1057
		// (add) Token: 0x06002D69 RID: 11625
		// (remove) Token: 0x06002D6A RID: 11626
		event HTMLImgEvents_onbeforecutEventHandler onbeforecut;

		// Token: 0x14000422 RID: 1058
		// (add) Token: 0x06002D6B RID: 11627
		// (remove) Token: 0x06002D6C RID: 11628
		event HTMLImgEvents_oncutEventHandler oncut;

		// Token: 0x14000423 RID: 1059
		// (add) Token: 0x06002D6D RID: 11629
		// (remove) Token: 0x06002D6E RID: 11630
		event HTMLImgEvents_onbeforecopyEventHandler onbeforecopy;

		// Token: 0x14000424 RID: 1060
		// (add) Token: 0x06002D6F RID: 11631
		// (remove) Token: 0x06002D70 RID: 11632
		event HTMLImgEvents_oncopyEventHandler oncopy;

		// Token: 0x14000425 RID: 1061
		// (add) Token: 0x06002D71 RID: 11633
		// (remove) Token: 0x06002D72 RID: 11634
		event HTMLImgEvents_onbeforepasteEventHandler onbeforepaste;

		// Token: 0x14000426 RID: 1062
		// (add) Token: 0x06002D73 RID: 11635
		// (remove) Token: 0x06002D74 RID: 11636
		event HTMLImgEvents_onpasteEventHandler onpaste;

		// Token: 0x14000427 RID: 1063
		// (add) Token: 0x06002D75 RID: 11637
		// (remove) Token: 0x06002D76 RID: 11638
		event HTMLImgEvents_oncontextmenuEventHandler oncontextmenu;

		// Token: 0x14000428 RID: 1064
		// (add) Token: 0x06002D77 RID: 11639
		// (remove) Token: 0x06002D78 RID: 11640
		event HTMLImgEvents_onrowsdeleteEventHandler onrowsdelete;

		// Token: 0x14000429 RID: 1065
		// (add) Token: 0x06002D79 RID: 11641
		// (remove) Token: 0x06002D7A RID: 11642
		event HTMLImgEvents_onrowsinsertedEventHandler onrowsinserted;

		// Token: 0x1400042A RID: 1066
		// (add) Token: 0x06002D7B RID: 11643
		// (remove) Token: 0x06002D7C RID: 11644
		event HTMLImgEvents_oncellchangeEventHandler oncellchange;

		// Token: 0x1400042B RID: 1067
		// (add) Token: 0x06002D7D RID: 11645
		// (remove) Token: 0x06002D7E RID: 11646
		event HTMLImgEvents_onreadystatechangeEventHandler onreadystatechange;

		// Token: 0x1400042C RID: 1068
		// (add) Token: 0x06002D7F RID: 11647
		// (remove) Token: 0x06002D80 RID: 11648
		event HTMLImgEvents_onbeforeeditfocusEventHandler onbeforeeditfocus;

		// Token: 0x1400042D RID: 1069
		// (add) Token: 0x06002D81 RID: 11649
		// (remove) Token: 0x06002D82 RID: 11650
		event HTMLImgEvents_onlayoutcompleteEventHandler onlayoutcomplete;

		// Token: 0x1400042E RID: 1070
		// (add) Token: 0x06002D83 RID: 11651
		// (remove) Token: 0x06002D84 RID: 11652
		event HTMLImgEvents_onpageEventHandler onpage;

		// Token: 0x1400042F RID: 1071
		// (add) Token: 0x06002D85 RID: 11653
		// (remove) Token: 0x06002D86 RID: 11654
		event HTMLImgEvents_onbeforedeactivateEventHandler onbeforedeactivate;

		// Token: 0x14000430 RID: 1072
		// (add) Token: 0x06002D87 RID: 11655
		// (remove) Token: 0x06002D88 RID: 11656
		event HTMLImgEvents_onbeforeactivateEventHandler onbeforeactivate;

		// Token: 0x14000431 RID: 1073
		// (add) Token: 0x06002D89 RID: 11657
		// (remove) Token: 0x06002D8A RID: 11658
		event HTMLImgEvents_onmoveEventHandler onmove;

		// Token: 0x14000432 RID: 1074
		// (add) Token: 0x06002D8B RID: 11659
		// (remove) Token: 0x06002D8C RID: 11660
		event HTMLImgEvents_oncontrolselectEventHandler oncontrolselect;

		// Token: 0x14000433 RID: 1075
		// (add) Token: 0x06002D8D RID: 11661
		// (remove) Token: 0x06002D8E RID: 11662
		event HTMLImgEvents_onmovestartEventHandler onmovestart;

		// Token: 0x14000434 RID: 1076
		// (add) Token: 0x06002D8F RID: 11663
		// (remove) Token: 0x06002D90 RID: 11664
		event HTMLImgEvents_onmoveendEventHandler onmoveend;

		// Token: 0x14000435 RID: 1077
		// (add) Token: 0x06002D91 RID: 11665
		// (remove) Token: 0x06002D92 RID: 11666
		event HTMLImgEvents_onresizestartEventHandler onresizestart;

		// Token: 0x14000436 RID: 1078
		// (add) Token: 0x06002D93 RID: 11667
		// (remove) Token: 0x06002D94 RID: 11668
		event HTMLImgEvents_onresizeendEventHandler onresizeend;

		// Token: 0x14000437 RID: 1079
		// (add) Token: 0x06002D95 RID: 11669
		// (remove) Token: 0x06002D96 RID: 11670
		event HTMLImgEvents_onmouseenterEventHandler onmouseenter;

		// Token: 0x14000438 RID: 1080
		// (add) Token: 0x06002D97 RID: 11671
		// (remove) Token: 0x06002D98 RID: 11672
		event HTMLImgEvents_onmouseleaveEventHandler onmouseleave;

		// Token: 0x14000439 RID: 1081
		// (add) Token: 0x06002D99 RID: 11673
		// (remove) Token: 0x06002D9A RID: 11674
		event HTMLImgEvents_onmousewheelEventHandler onmousewheel;

		// Token: 0x1400043A RID: 1082
		// (add) Token: 0x06002D9B RID: 11675
		// (remove) Token: 0x06002D9C RID: 11676
		event HTMLImgEvents_onactivateEventHandler onactivate;

		// Token: 0x1400043B RID: 1083
		// (add) Token: 0x06002D9D RID: 11677
		// (remove) Token: 0x06002D9E RID: 11678
		event HTMLImgEvents_ondeactivateEventHandler ondeactivate;

		// Token: 0x1400043C RID: 1084
		// (add) Token: 0x06002D9F RID: 11679
		// (remove) Token: 0x06002DA0 RID: 11680
		event HTMLImgEvents_onfocusinEventHandler onfocusin;

		// Token: 0x1400043D RID: 1085
		// (add) Token: 0x06002DA1 RID: 11681
		// (remove) Token: 0x06002DA2 RID: 11682
		event HTMLImgEvents_onfocusoutEventHandler onfocusout;

		// Token: 0x1400043E RID: 1086
		// (add) Token: 0x06002DA3 RID: 11683
		// (remove) Token: 0x06002DA4 RID: 11684
		event HTMLImgEvents_onloadEventHandler onload;

		// Token: 0x1400043F RID: 1087
		// (add) Token: 0x06002DA5 RID: 11685
		// (remove) Token: 0x06002DA6 RID: 11686
		event HTMLImgEvents_onerrorEventHandler onerror;

		// Token: 0x14000440 RID: 1088
		// (add) Token: 0x06002DA7 RID: 11687
		// (remove) Token: 0x06002DA8 RID: 11688
		event HTMLImgEvents_onabortEventHandler onabort;
	}
}
