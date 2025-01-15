using System;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000197 RID: 407
	[ComEventInterface(typeof(HTMLLinkElementEvents2\u0000), typeof(HTMLLinkElementEvents2_EventProvider\u0000))]
	[ComVisible(false)]
	public interface HTMLLinkElementEvents2_Event
	{
		// Token: 0x140001BD RID: 445
		// (add) Token: 0x06001A2A RID: 6698
		// (remove) Token: 0x06001A2B RID: 6699
		event HTMLLinkElementEvents2_onhelpEventHandler onhelp;

		// Token: 0x140001BE RID: 446
		// (add) Token: 0x06001A2C RID: 6700
		// (remove) Token: 0x06001A2D RID: 6701
		event HTMLLinkElementEvents2_onclickEventHandler onclick;

		// Token: 0x140001BF RID: 447
		// (add) Token: 0x06001A2E RID: 6702
		// (remove) Token: 0x06001A2F RID: 6703
		event HTMLLinkElementEvents2_ondblclickEventHandler ondblclick;

		// Token: 0x140001C0 RID: 448
		// (add) Token: 0x06001A30 RID: 6704
		// (remove) Token: 0x06001A31 RID: 6705
		event HTMLLinkElementEvents2_onkeypressEventHandler onkeypress;

		// Token: 0x140001C1 RID: 449
		// (add) Token: 0x06001A32 RID: 6706
		// (remove) Token: 0x06001A33 RID: 6707
		event HTMLLinkElementEvents2_onkeydownEventHandler onkeydown;

		// Token: 0x140001C2 RID: 450
		// (add) Token: 0x06001A34 RID: 6708
		// (remove) Token: 0x06001A35 RID: 6709
		event HTMLLinkElementEvents2_onkeyupEventHandler onkeyup;

		// Token: 0x140001C3 RID: 451
		// (add) Token: 0x06001A36 RID: 6710
		// (remove) Token: 0x06001A37 RID: 6711
		event HTMLLinkElementEvents2_onmouseoutEventHandler onmouseout;

		// Token: 0x140001C4 RID: 452
		// (add) Token: 0x06001A38 RID: 6712
		// (remove) Token: 0x06001A39 RID: 6713
		event HTMLLinkElementEvents2_onmouseoverEventHandler onmouseover;

		// Token: 0x140001C5 RID: 453
		// (add) Token: 0x06001A3A RID: 6714
		// (remove) Token: 0x06001A3B RID: 6715
		event HTMLLinkElementEvents2_onmousemoveEventHandler onmousemove;

		// Token: 0x140001C6 RID: 454
		// (add) Token: 0x06001A3C RID: 6716
		// (remove) Token: 0x06001A3D RID: 6717
		event HTMLLinkElementEvents2_onmousedownEventHandler onmousedown;

		// Token: 0x140001C7 RID: 455
		// (add) Token: 0x06001A3E RID: 6718
		// (remove) Token: 0x06001A3F RID: 6719
		event HTMLLinkElementEvents2_onmouseupEventHandler onmouseup;

		// Token: 0x140001C8 RID: 456
		// (add) Token: 0x06001A40 RID: 6720
		// (remove) Token: 0x06001A41 RID: 6721
		event HTMLLinkElementEvents2_onselectstartEventHandler onselectstart;

		// Token: 0x140001C9 RID: 457
		// (add) Token: 0x06001A42 RID: 6722
		// (remove) Token: 0x06001A43 RID: 6723
		event HTMLLinkElementEvents2_onfilterchangeEventHandler onfilterchange;

		// Token: 0x140001CA RID: 458
		// (add) Token: 0x06001A44 RID: 6724
		// (remove) Token: 0x06001A45 RID: 6725
		event HTMLLinkElementEvents2_ondragstartEventHandler ondragstart;

		// Token: 0x140001CB RID: 459
		// (add) Token: 0x06001A46 RID: 6726
		// (remove) Token: 0x06001A47 RID: 6727
		event HTMLLinkElementEvents2_onbeforeupdateEventHandler onbeforeupdate;

		// Token: 0x140001CC RID: 460
		// (add) Token: 0x06001A48 RID: 6728
		// (remove) Token: 0x06001A49 RID: 6729
		event HTMLLinkElementEvents2_onafterupdateEventHandler onafterupdate;

		// Token: 0x140001CD RID: 461
		// (add) Token: 0x06001A4A RID: 6730
		// (remove) Token: 0x06001A4B RID: 6731
		event HTMLLinkElementEvents2_onerrorupdateEventHandler onerrorupdate;

		// Token: 0x140001CE RID: 462
		// (add) Token: 0x06001A4C RID: 6732
		// (remove) Token: 0x06001A4D RID: 6733
		event HTMLLinkElementEvents2_onrowexitEventHandler onrowexit;

		// Token: 0x140001CF RID: 463
		// (add) Token: 0x06001A4E RID: 6734
		// (remove) Token: 0x06001A4F RID: 6735
		event HTMLLinkElementEvents2_onrowenterEventHandler onrowenter;

		// Token: 0x140001D0 RID: 464
		// (add) Token: 0x06001A50 RID: 6736
		// (remove) Token: 0x06001A51 RID: 6737
		event HTMLLinkElementEvents2_ondatasetchangedEventHandler ondatasetchanged;

		// Token: 0x140001D1 RID: 465
		// (add) Token: 0x06001A52 RID: 6738
		// (remove) Token: 0x06001A53 RID: 6739
		event HTMLLinkElementEvents2_ondataavailableEventHandler ondataavailable;

		// Token: 0x140001D2 RID: 466
		// (add) Token: 0x06001A54 RID: 6740
		// (remove) Token: 0x06001A55 RID: 6741
		event HTMLLinkElementEvents2_ondatasetcompleteEventHandler ondatasetcomplete;

		// Token: 0x140001D3 RID: 467
		// (add) Token: 0x06001A56 RID: 6742
		// (remove) Token: 0x06001A57 RID: 6743
		event HTMLLinkElementEvents2_onlosecaptureEventHandler onlosecapture;

		// Token: 0x140001D4 RID: 468
		// (add) Token: 0x06001A58 RID: 6744
		// (remove) Token: 0x06001A59 RID: 6745
		event HTMLLinkElementEvents2_onpropertychangeEventHandler onpropertychange;

		// Token: 0x140001D5 RID: 469
		// (add) Token: 0x06001A5A RID: 6746
		// (remove) Token: 0x06001A5B RID: 6747
		event HTMLLinkElementEvents2_onscrollEventHandler onscroll;

		// Token: 0x140001D6 RID: 470
		// (add) Token: 0x06001A5C RID: 6748
		// (remove) Token: 0x06001A5D RID: 6749
		event HTMLLinkElementEvents2_onfocusEventHandler onfocus;

		// Token: 0x140001D7 RID: 471
		// (add) Token: 0x06001A5E RID: 6750
		// (remove) Token: 0x06001A5F RID: 6751
		event HTMLLinkElementEvents2_onblurEventHandler onblur;

		// Token: 0x140001D8 RID: 472
		// (add) Token: 0x06001A60 RID: 6752
		// (remove) Token: 0x06001A61 RID: 6753
		event HTMLLinkElementEvents2_onresizeEventHandler onresize;

		// Token: 0x140001D9 RID: 473
		// (add) Token: 0x06001A62 RID: 6754
		// (remove) Token: 0x06001A63 RID: 6755
		event HTMLLinkElementEvents2_ondragEventHandler ondrag;

		// Token: 0x140001DA RID: 474
		// (add) Token: 0x06001A64 RID: 6756
		// (remove) Token: 0x06001A65 RID: 6757
		event HTMLLinkElementEvents2_ondragendEventHandler ondragend;

		// Token: 0x140001DB RID: 475
		// (add) Token: 0x06001A66 RID: 6758
		// (remove) Token: 0x06001A67 RID: 6759
		event HTMLLinkElementEvents2_ondragenterEventHandler ondragenter;

		// Token: 0x140001DC RID: 476
		// (add) Token: 0x06001A68 RID: 6760
		// (remove) Token: 0x06001A69 RID: 6761
		event HTMLLinkElementEvents2_ondragoverEventHandler ondragover;

		// Token: 0x140001DD RID: 477
		// (add) Token: 0x06001A6A RID: 6762
		// (remove) Token: 0x06001A6B RID: 6763
		event HTMLLinkElementEvents2_ondragleaveEventHandler ondragleave;

		// Token: 0x140001DE RID: 478
		// (add) Token: 0x06001A6C RID: 6764
		// (remove) Token: 0x06001A6D RID: 6765
		event HTMLLinkElementEvents2_ondropEventHandler ondrop;

		// Token: 0x140001DF RID: 479
		// (add) Token: 0x06001A6E RID: 6766
		// (remove) Token: 0x06001A6F RID: 6767
		event HTMLLinkElementEvents2_onbeforecutEventHandler onbeforecut;

		// Token: 0x140001E0 RID: 480
		// (add) Token: 0x06001A70 RID: 6768
		// (remove) Token: 0x06001A71 RID: 6769
		event HTMLLinkElementEvents2_oncutEventHandler oncut;

		// Token: 0x140001E1 RID: 481
		// (add) Token: 0x06001A72 RID: 6770
		// (remove) Token: 0x06001A73 RID: 6771
		event HTMLLinkElementEvents2_onbeforecopyEventHandler onbeforecopy;

		// Token: 0x140001E2 RID: 482
		// (add) Token: 0x06001A74 RID: 6772
		// (remove) Token: 0x06001A75 RID: 6773
		event HTMLLinkElementEvents2_oncopyEventHandler oncopy;

		// Token: 0x140001E3 RID: 483
		// (add) Token: 0x06001A76 RID: 6774
		// (remove) Token: 0x06001A77 RID: 6775
		event HTMLLinkElementEvents2_onbeforepasteEventHandler onbeforepaste;

		// Token: 0x140001E4 RID: 484
		// (add) Token: 0x06001A78 RID: 6776
		// (remove) Token: 0x06001A79 RID: 6777
		event HTMLLinkElementEvents2_onpasteEventHandler onpaste;

		// Token: 0x140001E5 RID: 485
		// (add) Token: 0x06001A7A RID: 6778
		// (remove) Token: 0x06001A7B RID: 6779
		event HTMLLinkElementEvents2_oncontextmenuEventHandler oncontextmenu;

		// Token: 0x140001E6 RID: 486
		// (add) Token: 0x06001A7C RID: 6780
		// (remove) Token: 0x06001A7D RID: 6781
		event HTMLLinkElementEvents2_onrowsdeleteEventHandler onrowsdelete;

		// Token: 0x140001E7 RID: 487
		// (add) Token: 0x06001A7E RID: 6782
		// (remove) Token: 0x06001A7F RID: 6783
		event HTMLLinkElementEvents2_onrowsinsertedEventHandler onrowsinserted;

		// Token: 0x140001E8 RID: 488
		// (add) Token: 0x06001A80 RID: 6784
		// (remove) Token: 0x06001A81 RID: 6785
		event HTMLLinkElementEvents2_oncellchangeEventHandler oncellchange;

		// Token: 0x140001E9 RID: 489
		// (add) Token: 0x06001A82 RID: 6786
		// (remove) Token: 0x06001A83 RID: 6787
		event HTMLLinkElementEvents2_onreadystatechangeEventHandler onreadystatechange;

		// Token: 0x140001EA RID: 490
		// (add) Token: 0x06001A84 RID: 6788
		// (remove) Token: 0x06001A85 RID: 6789
		event HTMLLinkElementEvents2_onlayoutcompleteEventHandler onlayoutcomplete;

		// Token: 0x140001EB RID: 491
		// (add) Token: 0x06001A86 RID: 6790
		// (remove) Token: 0x06001A87 RID: 6791
		event HTMLLinkElementEvents2_onpageEventHandler onpage;

		// Token: 0x140001EC RID: 492
		// (add) Token: 0x06001A88 RID: 6792
		// (remove) Token: 0x06001A89 RID: 6793
		event HTMLLinkElementEvents2_onmouseenterEventHandler onmouseenter;

		// Token: 0x140001ED RID: 493
		// (add) Token: 0x06001A8A RID: 6794
		// (remove) Token: 0x06001A8B RID: 6795
		event HTMLLinkElementEvents2_onmouseleaveEventHandler onmouseleave;

		// Token: 0x140001EE RID: 494
		// (add) Token: 0x06001A8C RID: 6796
		// (remove) Token: 0x06001A8D RID: 6797
		event HTMLLinkElementEvents2_onactivateEventHandler onactivate;

		// Token: 0x140001EF RID: 495
		// (add) Token: 0x06001A8E RID: 6798
		// (remove) Token: 0x06001A8F RID: 6799
		event HTMLLinkElementEvents2_ondeactivateEventHandler ondeactivate;

		// Token: 0x140001F0 RID: 496
		// (add) Token: 0x06001A90 RID: 6800
		// (remove) Token: 0x06001A91 RID: 6801
		event HTMLLinkElementEvents2_onbeforedeactivateEventHandler onbeforedeactivate;

		// Token: 0x140001F1 RID: 497
		// (add) Token: 0x06001A92 RID: 6802
		// (remove) Token: 0x06001A93 RID: 6803
		event HTMLLinkElementEvents2_onbeforeactivateEventHandler onbeforeactivate;

		// Token: 0x140001F2 RID: 498
		// (add) Token: 0x06001A94 RID: 6804
		// (remove) Token: 0x06001A95 RID: 6805
		event HTMLLinkElementEvents2_onfocusinEventHandler onfocusin;

		// Token: 0x140001F3 RID: 499
		// (add) Token: 0x06001A96 RID: 6806
		// (remove) Token: 0x06001A97 RID: 6807
		event HTMLLinkElementEvents2_onfocusoutEventHandler onfocusout;

		// Token: 0x140001F4 RID: 500
		// (add) Token: 0x06001A98 RID: 6808
		// (remove) Token: 0x06001A99 RID: 6809
		event HTMLLinkElementEvents2_onmoveEventHandler onmove;

		// Token: 0x140001F5 RID: 501
		// (add) Token: 0x06001A9A RID: 6810
		// (remove) Token: 0x06001A9B RID: 6811
		event HTMLLinkElementEvents2_oncontrolselectEventHandler oncontrolselect;

		// Token: 0x140001F6 RID: 502
		// (add) Token: 0x06001A9C RID: 6812
		// (remove) Token: 0x06001A9D RID: 6813
		event HTMLLinkElementEvents2_onmovestartEventHandler onmovestart;

		// Token: 0x140001F7 RID: 503
		// (add) Token: 0x06001A9E RID: 6814
		// (remove) Token: 0x06001A9F RID: 6815
		event HTMLLinkElementEvents2_onmoveendEventHandler onmoveend;

		// Token: 0x140001F8 RID: 504
		// (add) Token: 0x06001AA0 RID: 6816
		// (remove) Token: 0x06001AA1 RID: 6817
		event HTMLLinkElementEvents2_onresizestartEventHandler onresizestart;

		// Token: 0x140001F9 RID: 505
		// (add) Token: 0x06001AA2 RID: 6818
		// (remove) Token: 0x06001AA3 RID: 6819
		event HTMLLinkElementEvents2_onresizeendEventHandler onresizeend;

		// Token: 0x140001FA RID: 506
		// (add) Token: 0x06001AA4 RID: 6820
		// (remove) Token: 0x06001AA5 RID: 6821
		event HTMLLinkElementEvents2_onmousewheelEventHandler onmousewheel;

		// Token: 0x140001FB RID: 507
		// (add) Token: 0x06001AA6 RID: 6822
		// (remove) Token: 0x06001AA7 RID: 6823
		event HTMLLinkElementEvents2_onloadEventHandler onload;

		// Token: 0x140001FC RID: 508
		// (add) Token: 0x06001AA8 RID: 6824
		// (remove) Token: 0x06001AA9 RID: 6825
		event HTMLLinkElementEvents2_onerrorEventHandler onerror;
	}
}
