using System;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x0200088A RID: 2186
	[ComEventInterface(typeof(HTMLControlElementEvents2\u0000), typeof(HTMLControlElementEvents2_EventProvider\u0000))]
	[ComVisible(false)]
	public interface HTMLControlElementEvents2_Event
	{
		// Token: 0x14001BC5 RID: 7109
		// (add) Token: 0x0600E494 RID: 58516
		// (remove) Token: 0x0600E495 RID: 58517
		event HTMLControlElementEvents2_onhelpEventHandler onhelp;

		// Token: 0x14001BC6 RID: 7110
		// (add) Token: 0x0600E496 RID: 58518
		// (remove) Token: 0x0600E497 RID: 58519
		event HTMLControlElementEvents2_onclickEventHandler onclick;

		// Token: 0x14001BC7 RID: 7111
		// (add) Token: 0x0600E498 RID: 58520
		// (remove) Token: 0x0600E499 RID: 58521
		event HTMLControlElementEvents2_ondblclickEventHandler ondblclick;

		// Token: 0x14001BC8 RID: 7112
		// (add) Token: 0x0600E49A RID: 58522
		// (remove) Token: 0x0600E49B RID: 58523
		event HTMLControlElementEvents2_onkeypressEventHandler onkeypress;

		// Token: 0x14001BC9 RID: 7113
		// (add) Token: 0x0600E49C RID: 58524
		// (remove) Token: 0x0600E49D RID: 58525
		event HTMLControlElementEvents2_onkeydownEventHandler onkeydown;

		// Token: 0x14001BCA RID: 7114
		// (add) Token: 0x0600E49E RID: 58526
		// (remove) Token: 0x0600E49F RID: 58527
		event HTMLControlElementEvents2_onkeyupEventHandler onkeyup;

		// Token: 0x14001BCB RID: 7115
		// (add) Token: 0x0600E4A0 RID: 58528
		// (remove) Token: 0x0600E4A1 RID: 58529
		event HTMLControlElementEvents2_onmouseoutEventHandler onmouseout;

		// Token: 0x14001BCC RID: 7116
		// (add) Token: 0x0600E4A2 RID: 58530
		// (remove) Token: 0x0600E4A3 RID: 58531
		event HTMLControlElementEvents2_onmouseoverEventHandler onmouseover;

		// Token: 0x14001BCD RID: 7117
		// (add) Token: 0x0600E4A4 RID: 58532
		// (remove) Token: 0x0600E4A5 RID: 58533
		event HTMLControlElementEvents2_onmousemoveEventHandler onmousemove;

		// Token: 0x14001BCE RID: 7118
		// (add) Token: 0x0600E4A6 RID: 58534
		// (remove) Token: 0x0600E4A7 RID: 58535
		event HTMLControlElementEvents2_onmousedownEventHandler onmousedown;

		// Token: 0x14001BCF RID: 7119
		// (add) Token: 0x0600E4A8 RID: 58536
		// (remove) Token: 0x0600E4A9 RID: 58537
		event HTMLControlElementEvents2_onmouseupEventHandler onmouseup;

		// Token: 0x14001BD0 RID: 7120
		// (add) Token: 0x0600E4AA RID: 58538
		// (remove) Token: 0x0600E4AB RID: 58539
		event HTMLControlElementEvents2_onselectstartEventHandler onselectstart;

		// Token: 0x14001BD1 RID: 7121
		// (add) Token: 0x0600E4AC RID: 58540
		// (remove) Token: 0x0600E4AD RID: 58541
		event HTMLControlElementEvents2_onfilterchangeEventHandler onfilterchange;

		// Token: 0x14001BD2 RID: 7122
		// (add) Token: 0x0600E4AE RID: 58542
		// (remove) Token: 0x0600E4AF RID: 58543
		event HTMLControlElementEvents2_ondragstartEventHandler ondragstart;

		// Token: 0x14001BD3 RID: 7123
		// (add) Token: 0x0600E4B0 RID: 58544
		// (remove) Token: 0x0600E4B1 RID: 58545
		event HTMLControlElementEvents2_onbeforeupdateEventHandler onbeforeupdate;

		// Token: 0x14001BD4 RID: 7124
		// (add) Token: 0x0600E4B2 RID: 58546
		// (remove) Token: 0x0600E4B3 RID: 58547
		event HTMLControlElementEvents2_onafterupdateEventHandler onafterupdate;

		// Token: 0x14001BD5 RID: 7125
		// (add) Token: 0x0600E4B4 RID: 58548
		// (remove) Token: 0x0600E4B5 RID: 58549
		event HTMLControlElementEvents2_onerrorupdateEventHandler onerrorupdate;

		// Token: 0x14001BD6 RID: 7126
		// (add) Token: 0x0600E4B6 RID: 58550
		// (remove) Token: 0x0600E4B7 RID: 58551
		event HTMLControlElementEvents2_onrowexitEventHandler onrowexit;

		// Token: 0x14001BD7 RID: 7127
		// (add) Token: 0x0600E4B8 RID: 58552
		// (remove) Token: 0x0600E4B9 RID: 58553
		event HTMLControlElementEvents2_onrowenterEventHandler onrowenter;

		// Token: 0x14001BD8 RID: 7128
		// (add) Token: 0x0600E4BA RID: 58554
		// (remove) Token: 0x0600E4BB RID: 58555
		event HTMLControlElementEvents2_ondatasetchangedEventHandler ondatasetchanged;

		// Token: 0x14001BD9 RID: 7129
		// (add) Token: 0x0600E4BC RID: 58556
		// (remove) Token: 0x0600E4BD RID: 58557
		event HTMLControlElementEvents2_ondataavailableEventHandler ondataavailable;

		// Token: 0x14001BDA RID: 7130
		// (add) Token: 0x0600E4BE RID: 58558
		// (remove) Token: 0x0600E4BF RID: 58559
		event HTMLControlElementEvents2_ondatasetcompleteEventHandler ondatasetcomplete;

		// Token: 0x14001BDB RID: 7131
		// (add) Token: 0x0600E4C0 RID: 58560
		// (remove) Token: 0x0600E4C1 RID: 58561
		event HTMLControlElementEvents2_onlosecaptureEventHandler onlosecapture;

		// Token: 0x14001BDC RID: 7132
		// (add) Token: 0x0600E4C2 RID: 58562
		// (remove) Token: 0x0600E4C3 RID: 58563
		event HTMLControlElementEvents2_onpropertychangeEventHandler onpropertychange;

		// Token: 0x14001BDD RID: 7133
		// (add) Token: 0x0600E4C4 RID: 58564
		// (remove) Token: 0x0600E4C5 RID: 58565
		event HTMLControlElementEvents2_onscrollEventHandler onscroll;

		// Token: 0x14001BDE RID: 7134
		// (add) Token: 0x0600E4C6 RID: 58566
		// (remove) Token: 0x0600E4C7 RID: 58567
		event HTMLControlElementEvents2_onfocusEventHandler onfocus;

		// Token: 0x14001BDF RID: 7135
		// (add) Token: 0x0600E4C8 RID: 58568
		// (remove) Token: 0x0600E4C9 RID: 58569
		event HTMLControlElementEvents2_onblurEventHandler onblur;

		// Token: 0x14001BE0 RID: 7136
		// (add) Token: 0x0600E4CA RID: 58570
		// (remove) Token: 0x0600E4CB RID: 58571
		event HTMLControlElementEvents2_onresizeEventHandler onresize;

		// Token: 0x14001BE1 RID: 7137
		// (add) Token: 0x0600E4CC RID: 58572
		// (remove) Token: 0x0600E4CD RID: 58573
		event HTMLControlElementEvents2_ondragEventHandler ondrag;

		// Token: 0x14001BE2 RID: 7138
		// (add) Token: 0x0600E4CE RID: 58574
		// (remove) Token: 0x0600E4CF RID: 58575
		event HTMLControlElementEvents2_ondragendEventHandler ondragend;

		// Token: 0x14001BE3 RID: 7139
		// (add) Token: 0x0600E4D0 RID: 58576
		// (remove) Token: 0x0600E4D1 RID: 58577
		event HTMLControlElementEvents2_ondragenterEventHandler ondragenter;

		// Token: 0x14001BE4 RID: 7140
		// (add) Token: 0x0600E4D2 RID: 58578
		// (remove) Token: 0x0600E4D3 RID: 58579
		event HTMLControlElementEvents2_ondragoverEventHandler ondragover;

		// Token: 0x14001BE5 RID: 7141
		// (add) Token: 0x0600E4D4 RID: 58580
		// (remove) Token: 0x0600E4D5 RID: 58581
		event HTMLControlElementEvents2_ondragleaveEventHandler ondragleave;

		// Token: 0x14001BE6 RID: 7142
		// (add) Token: 0x0600E4D6 RID: 58582
		// (remove) Token: 0x0600E4D7 RID: 58583
		event HTMLControlElementEvents2_ondropEventHandler ondrop;

		// Token: 0x14001BE7 RID: 7143
		// (add) Token: 0x0600E4D8 RID: 58584
		// (remove) Token: 0x0600E4D9 RID: 58585
		event HTMLControlElementEvents2_onbeforecutEventHandler onbeforecut;

		// Token: 0x14001BE8 RID: 7144
		// (add) Token: 0x0600E4DA RID: 58586
		// (remove) Token: 0x0600E4DB RID: 58587
		event HTMLControlElementEvents2_oncutEventHandler oncut;

		// Token: 0x14001BE9 RID: 7145
		// (add) Token: 0x0600E4DC RID: 58588
		// (remove) Token: 0x0600E4DD RID: 58589
		event HTMLControlElementEvents2_onbeforecopyEventHandler onbeforecopy;

		// Token: 0x14001BEA RID: 7146
		// (add) Token: 0x0600E4DE RID: 58590
		// (remove) Token: 0x0600E4DF RID: 58591
		event HTMLControlElementEvents2_oncopyEventHandler oncopy;

		// Token: 0x14001BEB RID: 7147
		// (add) Token: 0x0600E4E0 RID: 58592
		// (remove) Token: 0x0600E4E1 RID: 58593
		event HTMLControlElementEvents2_onbeforepasteEventHandler onbeforepaste;

		// Token: 0x14001BEC RID: 7148
		// (add) Token: 0x0600E4E2 RID: 58594
		// (remove) Token: 0x0600E4E3 RID: 58595
		event HTMLControlElementEvents2_onpasteEventHandler onpaste;

		// Token: 0x14001BED RID: 7149
		// (add) Token: 0x0600E4E4 RID: 58596
		// (remove) Token: 0x0600E4E5 RID: 58597
		event HTMLControlElementEvents2_oncontextmenuEventHandler oncontextmenu;

		// Token: 0x14001BEE RID: 7150
		// (add) Token: 0x0600E4E6 RID: 58598
		// (remove) Token: 0x0600E4E7 RID: 58599
		event HTMLControlElementEvents2_onrowsdeleteEventHandler onrowsdelete;

		// Token: 0x14001BEF RID: 7151
		// (add) Token: 0x0600E4E8 RID: 58600
		// (remove) Token: 0x0600E4E9 RID: 58601
		event HTMLControlElementEvents2_onrowsinsertedEventHandler onrowsinserted;

		// Token: 0x14001BF0 RID: 7152
		// (add) Token: 0x0600E4EA RID: 58602
		// (remove) Token: 0x0600E4EB RID: 58603
		event HTMLControlElementEvents2_oncellchangeEventHandler oncellchange;

		// Token: 0x14001BF1 RID: 7153
		// (add) Token: 0x0600E4EC RID: 58604
		// (remove) Token: 0x0600E4ED RID: 58605
		event HTMLControlElementEvents2_onreadystatechangeEventHandler onreadystatechange;

		// Token: 0x14001BF2 RID: 7154
		// (add) Token: 0x0600E4EE RID: 58606
		// (remove) Token: 0x0600E4EF RID: 58607
		event HTMLControlElementEvents2_onlayoutcompleteEventHandler onlayoutcomplete;

		// Token: 0x14001BF3 RID: 7155
		// (add) Token: 0x0600E4F0 RID: 58608
		// (remove) Token: 0x0600E4F1 RID: 58609
		event HTMLControlElementEvents2_onpageEventHandler onpage;

		// Token: 0x14001BF4 RID: 7156
		// (add) Token: 0x0600E4F2 RID: 58610
		// (remove) Token: 0x0600E4F3 RID: 58611
		event HTMLControlElementEvents2_onmouseenterEventHandler onmouseenter;

		// Token: 0x14001BF5 RID: 7157
		// (add) Token: 0x0600E4F4 RID: 58612
		// (remove) Token: 0x0600E4F5 RID: 58613
		event HTMLControlElementEvents2_onmouseleaveEventHandler onmouseleave;

		// Token: 0x14001BF6 RID: 7158
		// (add) Token: 0x0600E4F6 RID: 58614
		// (remove) Token: 0x0600E4F7 RID: 58615
		event HTMLControlElementEvents2_onactivateEventHandler onactivate;

		// Token: 0x14001BF7 RID: 7159
		// (add) Token: 0x0600E4F8 RID: 58616
		// (remove) Token: 0x0600E4F9 RID: 58617
		event HTMLControlElementEvents2_ondeactivateEventHandler ondeactivate;

		// Token: 0x14001BF8 RID: 7160
		// (add) Token: 0x0600E4FA RID: 58618
		// (remove) Token: 0x0600E4FB RID: 58619
		event HTMLControlElementEvents2_onbeforedeactivateEventHandler onbeforedeactivate;

		// Token: 0x14001BF9 RID: 7161
		// (add) Token: 0x0600E4FC RID: 58620
		// (remove) Token: 0x0600E4FD RID: 58621
		event HTMLControlElementEvents2_onbeforeactivateEventHandler onbeforeactivate;

		// Token: 0x14001BFA RID: 7162
		// (add) Token: 0x0600E4FE RID: 58622
		// (remove) Token: 0x0600E4FF RID: 58623
		event HTMLControlElementEvents2_onfocusinEventHandler onfocusin;

		// Token: 0x14001BFB RID: 7163
		// (add) Token: 0x0600E500 RID: 58624
		// (remove) Token: 0x0600E501 RID: 58625
		event HTMLControlElementEvents2_onfocusoutEventHandler onfocusout;

		// Token: 0x14001BFC RID: 7164
		// (add) Token: 0x0600E502 RID: 58626
		// (remove) Token: 0x0600E503 RID: 58627
		event HTMLControlElementEvents2_onmoveEventHandler onmove;

		// Token: 0x14001BFD RID: 7165
		// (add) Token: 0x0600E504 RID: 58628
		// (remove) Token: 0x0600E505 RID: 58629
		event HTMLControlElementEvents2_oncontrolselectEventHandler oncontrolselect;

		// Token: 0x14001BFE RID: 7166
		// (add) Token: 0x0600E506 RID: 58630
		// (remove) Token: 0x0600E507 RID: 58631
		event HTMLControlElementEvents2_onmovestartEventHandler onmovestart;

		// Token: 0x14001BFF RID: 7167
		// (add) Token: 0x0600E508 RID: 58632
		// (remove) Token: 0x0600E509 RID: 58633
		event HTMLControlElementEvents2_onmoveendEventHandler onmoveend;

		// Token: 0x14001C00 RID: 7168
		// (add) Token: 0x0600E50A RID: 58634
		// (remove) Token: 0x0600E50B RID: 58635
		event HTMLControlElementEvents2_onresizestartEventHandler onresizestart;

		// Token: 0x14001C01 RID: 7169
		// (add) Token: 0x0600E50C RID: 58636
		// (remove) Token: 0x0600E50D RID: 58637
		event HTMLControlElementEvents2_onresizeendEventHandler onresizeend;

		// Token: 0x14001C02 RID: 7170
		// (add) Token: 0x0600E50E RID: 58638
		// (remove) Token: 0x0600E50F RID: 58639
		event HTMLControlElementEvents2_onmousewheelEventHandler onmousewheel;
	}
}
