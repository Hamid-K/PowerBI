using System;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x020000BA RID: 186
	[ComEventInterface(typeof(HTMLElementEvents\u0000), typeof(HTMLElementEvents_EventProvider\u0000))]
	[ComVisible(false)]
	public interface HTMLElementEvents_Event
	{
		// Token: 0x1400007E RID: 126
		// (add) Token: 0x060011C1 RID: 4545
		// (remove) Token: 0x060011C2 RID: 4546
		event HTMLElementEvents_onhelpEventHandler onhelp;

		// Token: 0x1400007F RID: 127
		// (add) Token: 0x060011C3 RID: 4547
		// (remove) Token: 0x060011C4 RID: 4548
		event HTMLElementEvents_onclickEventHandler onclick;

		// Token: 0x14000080 RID: 128
		// (add) Token: 0x060011C5 RID: 4549
		// (remove) Token: 0x060011C6 RID: 4550
		event HTMLElementEvents_ondblclickEventHandler ondblclick;

		// Token: 0x14000081 RID: 129
		// (add) Token: 0x060011C7 RID: 4551
		// (remove) Token: 0x060011C8 RID: 4552
		event HTMLElementEvents_onkeypressEventHandler onkeypress;

		// Token: 0x14000082 RID: 130
		// (add) Token: 0x060011C9 RID: 4553
		// (remove) Token: 0x060011CA RID: 4554
		event HTMLElementEvents_onkeydownEventHandler onkeydown;

		// Token: 0x14000083 RID: 131
		// (add) Token: 0x060011CB RID: 4555
		// (remove) Token: 0x060011CC RID: 4556
		event HTMLElementEvents_onkeyupEventHandler onkeyup;

		// Token: 0x14000084 RID: 132
		// (add) Token: 0x060011CD RID: 4557
		// (remove) Token: 0x060011CE RID: 4558
		event HTMLElementEvents_onmouseoutEventHandler onmouseout;

		// Token: 0x14000085 RID: 133
		// (add) Token: 0x060011CF RID: 4559
		// (remove) Token: 0x060011D0 RID: 4560
		event HTMLElementEvents_onmouseoverEventHandler onmouseover;

		// Token: 0x14000086 RID: 134
		// (add) Token: 0x060011D1 RID: 4561
		// (remove) Token: 0x060011D2 RID: 4562
		event HTMLElementEvents_onmousemoveEventHandler onmousemove;

		// Token: 0x14000087 RID: 135
		// (add) Token: 0x060011D3 RID: 4563
		// (remove) Token: 0x060011D4 RID: 4564
		event HTMLElementEvents_onmousedownEventHandler onmousedown;

		// Token: 0x14000088 RID: 136
		// (add) Token: 0x060011D5 RID: 4565
		// (remove) Token: 0x060011D6 RID: 4566
		event HTMLElementEvents_onmouseupEventHandler onmouseup;

		// Token: 0x14000089 RID: 137
		// (add) Token: 0x060011D7 RID: 4567
		// (remove) Token: 0x060011D8 RID: 4568
		event HTMLElementEvents_onselectstartEventHandler onselectstart;

		// Token: 0x1400008A RID: 138
		// (add) Token: 0x060011D9 RID: 4569
		// (remove) Token: 0x060011DA RID: 4570
		event HTMLElementEvents_onfilterchangeEventHandler onfilterchange;

		// Token: 0x1400008B RID: 139
		// (add) Token: 0x060011DB RID: 4571
		// (remove) Token: 0x060011DC RID: 4572
		event HTMLElementEvents_ondragstartEventHandler ondragstart;

		// Token: 0x1400008C RID: 140
		// (add) Token: 0x060011DD RID: 4573
		// (remove) Token: 0x060011DE RID: 4574
		event HTMLElementEvents_onbeforeupdateEventHandler onbeforeupdate;

		// Token: 0x1400008D RID: 141
		// (add) Token: 0x060011DF RID: 4575
		// (remove) Token: 0x060011E0 RID: 4576
		event HTMLElementEvents_onafterupdateEventHandler onafterupdate;

		// Token: 0x1400008E RID: 142
		// (add) Token: 0x060011E1 RID: 4577
		// (remove) Token: 0x060011E2 RID: 4578
		event HTMLElementEvents_onerrorupdateEventHandler onerrorupdate;

		// Token: 0x1400008F RID: 143
		// (add) Token: 0x060011E3 RID: 4579
		// (remove) Token: 0x060011E4 RID: 4580
		event HTMLElementEvents_onrowexitEventHandler onrowexit;

		// Token: 0x14000090 RID: 144
		// (add) Token: 0x060011E5 RID: 4581
		// (remove) Token: 0x060011E6 RID: 4582
		event HTMLElementEvents_onrowenterEventHandler onrowenter;

		// Token: 0x14000091 RID: 145
		// (add) Token: 0x060011E7 RID: 4583
		// (remove) Token: 0x060011E8 RID: 4584
		event HTMLElementEvents_ondatasetchangedEventHandler ondatasetchanged;

		// Token: 0x14000092 RID: 146
		// (add) Token: 0x060011E9 RID: 4585
		// (remove) Token: 0x060011EA RID: 4586
		event HTMLElementEvents_ondataavailableEventHandler ondataavailable;

		// Token: 0x14000093 RID: 147
		// (add) Token: 0x060011EB RID: 4587
		// (remove) Token: 0x060011EC RID: 4588
		event HTMLElementEvents_ondatasetcompleteEventHandler ondatasetcomplete;

		// Token: 0x14000094 RID: 148
		// (add) Token: 0x060011ED RID: 4589
		// (remove) Token: 0x060011EE RID: 4590
		event HTMLElementEvents_onlosecaptureEventHandler onlosecapture;

		// Token: 0x14000095 RID: 149
		// (add) Token: 0x060011EF RID: 4591
		// (remove) Token: 0x060011F0 RID: 4592
		event HTMLElementEvents_onpropertychangeEventHandler onpropertychange;

		// Token: 0x14000096 RID: 150
		// (add) Token: 0x060011F1 RID: 4593
		// (remove) Token: 0x060011F2 RID: 4594
		event HTMLElementEvents_onscrollEventHandler onscroll;

		// Token: 0x14000097 RID: 151
		// (add) Token: 0x060011F3 RID: 4595
		// (remove) Token: 0x060011F4 RID: 4596
		event HTMLElementEvents_onfocusEventHandler onfocus;

		// Token: 0x14000098 RID: 152
		// (add) Token: 0x060011F5 RID: 4597
		// (remove) Token: 0x060011F6 RID: 4598
		event HTMLElementEvents_onblurEventHandler onblur;

		// Token: 0x14000099 RID: 153
		// (add) Token: 0x060011F7 RID: 4599
		// (remove) Token: 0x060011F8 RID: 4600
		event HTMLElementEvents_onresizeEventHandler onresize;

		// Token: 0x1400009A RID: 154
		// (add) Token: 0x060011F9 RID: 4601
		// (remove) Token: 0x060011FA RID: 4602
		event HTMLElementEvents_ondragEventHandler ondrag;

		// Token: 0x1400009B RID: 155
		// (add) Token: 0x060011FB RID: 4603
		// (remove) Token: 0x060011FC RID: 4604
		event HTMLElementEvents_ondragendEventHandler ondragend;

		// Token: 0x1400009C RID: 156
		// (add) Token: 0x060011FD RID: 4605
		// (remove) Token: 0x060011FE RID: 4606
		event HTMLElementEvents_ondragenterEventHandler ondragenter;

		// Token: 0x1400009D RID: 157
		// (add) Token: 0x060011FF RID: 4607
		// (remove) Token: 0x06001200 RID: 4608
		event HTMLElementEvents_ondragoverEventHandler ondragover;

		// Token: 0x1400009E RID: 158
		// (add) Token: 0x06001201 RID: 4609
		// (remove) Token: 0x06001202 RID: 4610
		event HTMLElementEvents_ondragleaveEventHandler ondragleave;

		// Token: 0x1400009F RID: 159
		// (add) Token: 0x06001203 RID: 4611
		// (remove) Token: 0x06001204 RID: 4612
		event HTMLElementEvents_ondropEventHandler ondrop;

		// Token: 0x140000A0 RID: 160
		// (add) Token: 0x06001205 RID: 4613
		// (remove) Token: 0x06001206 RID: 4614
		event HTMLElementEvents_onbeforecutEventHandler onbeforecut;

		// Token: 0x140000A1 RID: 161
		// (add) Token: 0x06001207 RID: 4615
		// (remove) Token: 0x06001208 RID: 4616
		event HTMLElementEvents_oncutEventHandler oncut;

		// Token: 0x140000A2 RID: 162
		// (add) Token: 0x06001209 RID: 4617
		// (remove) Token: 0x0600120A RID: 4618
		event HTMLElementEvents_onbeforecopyEventHandler onbeforecopy;

		// Token: 0x140000A3 RID: 163
		// (add) Token: 0x0600120B RID: 4619
		// (remove) Token: 0x0600120C RID: 4620
		event HTMLElementEvents_oncopyEventHandler oncopy;

		// Token: 0x140000A4 RID: 164
		// (add) Token: 0x0600120D RID: 4621
		// (remove) Token: 0x0600120E RID: 4622
		event HTMLElementEvents_onbeforepasteEventHandler onbeforepaste;

		// Token: 0x140000A5 RID: 165
		// (add) Token: 0x0600120F RID: 4623
		// (remove) Token: 0x06001210 RID: 4624
		event HTMLElementEvents_onpasteEventHandler onpaste;

		// Token: 0x140000A6 RID: 166
		// (add) Token: 0x06001211 RID: 4625
		// (remove) Token: 0x06001212 RID: 4626
		event HTMLElementEvents_oncontextmenuEventHandler oncontextmenu;

		// Token: 0x140000A7 RID: 167
		// (add) Token: 0x06001213 RID: 4627
		// (remove) Token: 0x06001214 RID: 4628
		event HTMLElementEvents_onrowsdeleteEventHandler onrowsdelete;

		// Token: 0x140000A8 RID: 168
		// (add) Token: 0x06001215 RID: 4629
		// (remove) Token: 0x06001216 RID: 4630
		event HTMLElementEvents_onrowsinsertedEventHandler onrowsinserted;

		// Token: 0x140000A9 RID: 169
		// (add) Token: 0x06001217 RID: 4631
		// (remove) Token: 0x06001218 RID: 4632
		event HTMLElementEvents_oncellchangeEventHandler oncellchange;

		// Token: 0x140000AA RID: 170
		// (add) Token: 0x06001219 RID: 4633
		// (remove) Token: 0x0600121A RID: 4634
		event HTMLElementEvents_onreadystatechangeEventHandler onreadystatechange;

		// Token: 0x140000AB RID: 171
		// (add) Token: 0x0600121B RID: 4635
		// (remove) Token: 0x0600121C RID: 4636
		event HTMLElementEvents_onbeforeeditfocusEventHandler onbeforeeditfocus;

		// Token: 0x140000AC RID: 172
		// (add) Token: 0x0600121D RID: 4637
		// (remove) Token: 0x0600121E RID: 4638
		event HTMLElementEvents_onlayoutcompleteEventHandler onlayoutcomplete;

		// Token: 0x140000AD RID: 173
		// (add) Token: 0x0600121F RID: 4639
		// (remove) Token: 0x06001220 RID: 4640
		event HTMLElementEvents_onpageEventHandler onpage;

		// Token: 0x140000AE RID: 174
		// (add) Token: 0x06001221 RID: 4641
		// (remove) Token: 0x06001222 RID: 4642
		event HTMLElementEvents_onbeforedeactivateEventHandler onbeforedeactivate;

		// Token: 0x140000AF RID: 175
		// (add) Token: 0x06001223 RID: 4643
		// (remove) Token: 0x06001224 RID: 4644
		event HTMLElementEvents_onbeforeactivateEventHandler onbeforeactivate;

		// Token: 0x140000B0 RID: 176
		// (add) Token: 0x06001225 RID: 4645
		// (remove) Token: 0x06001226 RID: 4646
		event HTMLElementEvents_onmoveEventHandler onmove;

		// Token: 0x140000B1 RID: 177
		// (add) Token: 0x06001227 RID: 4647
		// (remove) Token: 0x06001228 RID: 4648
		event HTMLElementEvents_oncontrolselectEventHandler oncontrolselect;

		// Token: 0x140000B2 RID: 178
		// (add) Token: 0x06001229 RID: 4649
		// (remove) Token: 0x0600122A RID: 4650
		event HTMLElementEvents_onmovestartEventHandler onmovestart;

		// Token: 0x140000B3 RID: 179
		// (add) Token: 0x0600122B RID: 4651
		// (remove) Token: 0x0600122C RID: 4652
		event HTMLElementEvents_onmoveendEventHandler onmoveend;

		// Token: 0x140000B4 RID: 180
		// (add) Token: 0x0600122D RID: 4653
		// (remove) Token: 0x0600122E RID: 4654
		event HTMLElementEvents_onresizestartEventHandler onresizestart;

		// Token: 0x140000B5 RID: 181
		// (add) Token: 0x0600122F RID: 4655
		// (remove) Token: 0x06001230 RID: 4656
		event HTMLElementEvents_onresizeendEventHandler onresizeend;

		// Token: 0x140000B6 RID: 182
		// (add) Token: 0x06001231 RID: 4657
		// (remove) Token: 0x06001232 RID: 4658
		event HTMLElementEvents_onmouseenterEventHandler onmouseenter;

		// Token: 0x140000B7 RID: 183
		// (add) Token: 0x06001233 RID: 4659
		// (remove) Token: 0x06001234 RID: 4660
		event HTMLElementEvents_onmouseleaveEventHandler onmouseleave;

		// Token: 0x140000B8 RID: 184
		// (add) Token: 0x06001235 RID: 4661
		// (remove) Token: 0x06001236 RID: 4662
		event HTMLElementEvents_onmousewheelEventHandler onmousewheel;

		// Token: 0x140000B9 RID: 185
		// (add) Token: 0x06001237 RID: 4663
		// (remove) Token: 0x06001238 RID: 4664
		event HTMLElementEvents_onactivateEventHandler onactivate;

		// Token: 0x140000BA RID: 186
		// (add) Token: 0x06001239 RID: 4665
		// (remove) Token: 0x0600123A RID: 4666
		event HTMLElementEvents_ondeactivateEventHandler ondeactivate;

		// Token: 0x140000BB RID: 187
		// (add) Token: 0x0600123B RID: 4667
		// (remove) Token: 0x0600123C RID: 4668
		event HTMLElementEvents_onfocusinEventHandler onfocusin;

		// Token: 0x140000BC RID: 188
		// (add) Token: 0x0600123D RID: 4669
		// (remove) Token: 0x0600123E RID: 4670
		event HTMLElementEvents_onfocusoutEventHandler onfocusout;
	}
}
