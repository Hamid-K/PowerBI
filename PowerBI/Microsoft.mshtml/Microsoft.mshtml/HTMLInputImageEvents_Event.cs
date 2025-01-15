using System;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000D61 RID: 3425
	[ComEventInterface(typeof(HTMLInputImageEvents\u0000), typeof(HTMLInputImageEvents_EventProvider\u0000))]
	[ComVisible(false)]
	public interface HTMLInputImageEvents_Event
	{
		// Token: 0x14002D00 RID: 11520
		// (add) Token: 0x060173A6 RID: 95142
		// (remove) Token: 0x060173A7 RID: 95143
		event HTMLInputImageEvents_onhelpEventHandler onhelp;

		// Token: 0x14002D01 RID: 11521
		// (add) Token: 0x060173A8 RID: 95144
		// (remove) Token: 0x060173A9 RID: 95145
		event HTMLInputImageEvents_onclickEventHandler onclick;

		// Token: 0x14002D02 RID: 11522
		// (add) Token: 0x060173AA RID: 95146
		// (remove) Token: 0x060173AB RID: 95147
		event HTMLInputImageEvents_ondblclickEventHandler ondblclick;

		// Token: 0x14002D03 RID: 11523
		// (add) Token: 0x060173AC RID: 95148
		// (remove) Token: 0x060173AD RID: 95149
		event HTMLInputImageEvents_onkeypressEventHandler onkeypress;

		// Token: 0x14002D04 RID: 11524
		// (add) Token: 0x060173AE RID: 95150
		// (remove) Token: 0x060173AF RID: 95151
		event HTMLInputImageEvents_onkeydownEventHandler onkeydown;

		// Token: 0x14002D05 RID: 11525
		// (add) Token: 0x060173B0 RID: 95152
		// (remove) Token: 0x060173B1 RID: 95153
		event HTMLInputImageEvents_onkeyupEventHandler onkeyup;

		// Token: 0x14002D06 RID: 11526
		// (add) Token: 0x060173B2 RID: 95154
		// (remove) Token: 0x060173B3 RID: 95155
		event HTMLInputImageEvents_onmouseoutEventHandler onmouseout;

		// Token: 0x14002D07 RID: 11527
		// (add) Token: 0x060173B4 RID: 95156
		// (remove) Token: 0x060173B5 RID: 95157
		event HTMLInputImageEvents_onmouseoverEventHandler onmouseover;

		// Token: 0x14002D08 RID: 11528
		// (add) Token: 0x060173B6 RID: 95158
		// (remove) Token: 0x060173B7 RID: 95159
		event HTMLInputImageEvents_onmousemoveEventHandler onmousemove;

		// Token: 0x14002D09 RID: 11529
		// (add) Token: 0x060173B8 RID: 95160
		// (remove) Token: 0x060173B9 RID: 95161
		event HTMLInputImageEvents_onmousedownEventHandler onmousedown;

		// Token: 0x14002D0A RID: 11530
		// (add) Token: 0x060173BA RID: 95162
		// (remove) Token: 0x060173BB RID: 95163
		event HTMLInputImageEvents_onmouseupEventHandler onmouseup;

		// Token: 0x14002D0B RID: 11531
		// (add) Token: 0x060173BC RID: 95164
		// (remove) Token: 0x060173BD RID: 95165
		event HTMLInputImageEvents_onselectstartEventHandler onselectstart;

		// Token: 0x14002D0C RID: 11532
		// (add) Token: 0x060173BE RID: 95166
		// (remove) Token: 0x060173BF RID: 95167
		event HTMLInputImageEvents_onfilterchangeEventHandler onfilterchange;

		// Token: 0x14002D0D RID: 11533
		// (add) Token: 0x060173C0 RID: 95168
		// (remove) Token: 0x060173C1 RID: 95169
		event HTMLInputImageEvents_ondragstartEventHandler ondragstart;

		// Token: 0x14002D0E RID: 11534
		// (add) Token: 0x060173C2 RID: 95170
		// (remove) Token: 0x060173C3 RID: 95171
		event HTMLInputImageEvents_onbeforeupdateEventHandler onbeforeupdate;

		// Token: 0x14002D0F RID: 11535
		// (add) Token: 0x060173C4 RID: 95172
		// (remove) Token: 0x060173C5 RID: 95173
		event HTMLInputImageEvents_onafterupdateEventHandler onafterupdate;

		// Token: 0x14002D10 RID: 11536
		// (add) Token: 0x060173C6 RID: 95174
		// (remove) Token: 0x060173C7 RID: 95175
		event HTMLInputImageEvents_onerrorupdateEventHandler onerrorupdate;

		// Token: 0x14002D11 RID: 11537
		// (add) Token: 0x060173C8 RID: 95176
		// (remove) Token: 0x060173C9 RID: 95177
		event HTMLInputImageEvents_onrowexitEventHandler onrowexit;

		// Token: 0x14002D12 RID: 11538
		// (add) Token: 0x060173CA RID: 95178
		// (remove) Token: 0x060173CB RID: 95179
		event HTMLInputImageEvents_onrowenterEventHandler onrowenter;

		// Token: 0x14002D13 RID: 11539
		// (add) Token: 0x060173CC RID: 95180
		// (remove) Token: 0x060173CD RID: 95181
		event HTMLInputImageEvents_ondatasetchangedEventHandler ondatasetchanged;

		// Token: 0x14002D14 RID: 11540
		// (add) Token: 0x060173CE RID: 95182
		// (remove) Token: 0x060173CF RID: 95183
		event HTMLInputImageEvents_ondataavailableEventHandler ondataavailable;

		// Token: 0x14002D15 RID: 11541
		// (add) Token: 0x060173D0 RID: 95184
		// (remove) Token: 0x060173D1 RID: 95185
		event HTMLInputImageEvents_ondatasetcompleteEventHandler ondatasetcomplete;

		// Token: 0x14002D16 RID: 11542
		// (add) Token: 0x060173D2 RID: 95186
		// (remove) Token: 0x060173D3 RID: 95187
		event HTMLInputImageEvents_onlosecaptureEventHandler onlosecapture;

		// Token: 0x14002D17 RID: 11543
		// (add) Token: 0x060173D4 RID: 95188
		// (remove) Token: 0x060173D5 RID: 95189
		event HTMLInputImageEvents_onpropertychangeEventHandler onpropertychange;

		// Token: 0x14002D18 RID: 11544
		// (add) Token: 0x060173D6 RID: 95190
		// (remove) Token: 0x060173D7 RID: 95191
		event HTMLInputImageEvents_onscrollEventHandler onscroll;

		// Token: 0x14002D19 RID: 11545
		// (add) Token: 0x060173D8 RID: 95192
		// (remove) Token: 0x060173D9 RID: 95193
		event HTMLInputImageEvents_onfocusEventHandler onfocus;

		// Token: 0x14002D1A RID: 11546
		// (add) Token: 0x060173DA RID: 95194
		// (remove) Token: 0x060173DB RID: 95195
		event HTMLInputImageEvents_onblurEventHandler onblur;

		// Token: 0x14002D1B RID: 11547
		// (add) Token: 0x060173DC RID: 95196
		// (remove) Token: 0x060173DD RID: 95197
		event HTMLInputImageEvents_onresizeEventHandler onresize;

		// Token: 0x14002D1C RID: 11548
		// (add) Token: 0x060173DE RID: 95198
		// (remove) Token: 0x060173DF RID: 95199
		event HTMLInputImageEvents_ondragEventHandler ondrag;

		// Token: 0x14002D1D RID: 11549
		// (add) Token: 0x060173E0 RID: 95200
		// (remove) Token: 0x060173E1 RID: 95201
		event HTMLInputImageEvents_ondragendEventHandler ondragend;

		// Token: 0x14002D1E RID: 11550
		// (add) Token: 0x060173E2 RID: 95202
		// (remove) Token: 0x060173E3 RID: 95203
		event HTMLInputImageEvents_ondragenterEventHandler ondragenter;

		// Token: 0x14002D1F RID: 11551
		// (add) Token: 0x060173E4 RID: 95204
		// (remove) Token: 0x060173E5 RID: 95205
		event HTMLInputImageEvents_ondragoverEventHandler ondragover;

		// Token: 0x14002D20 RID: 11552
		// (add) Token: 0x060173E6 RID: 95206
		// (remove) Token: 0x060173E7 RID: 95207
		event HTMLInputImageEvents_ondragleaveEventHandler ondragleave;

		// Token: 0x14002D21 RID: 11553
		// (add) Token: 0x060173E8 RID: 95208
		// (remove) Token: 0x060173E9 RID: 95209
		event HTMLInputImageEvents_ondropEventHandler ondrop;

		// Token: 0x14002D22 RID: 11554
		// (add) Token: 0x060173EA RID: 95210
		// (remove) Token: 0x060173EB RID: 95211
		event HTMLInputImageEvents_onbeforecutEventHandler onbeforecut;

		// Token: 0x14002D23 RID: 11555
		// (add) Token: 0x060173EC RID: 95212
		// (remove) Token: 0x060173ED RID: 95213
		event HTMLInputImageEvents_oncutEventHandler oncut;

		// Token: 0x14002D24 RID: 11556
		// (add) Token: 0x060173EE RID: 95214
		// (remove) Token: 0x060173EF RID: 95215
		event HTMLInputImageEvents_onbeforecopyEventHandler onbeforecopy;

		// Token: 0x14002D25 RID: 11557
		// (add) Token: 0x060173F0 RID: 95216
		// (remove) Token: 0x060173F1 RID: 95217
		event HTMLInputImageEvents_oncopyEventHandler oncopy;

		// Token: 0x14002D26 RID: 11558
		// (add) Token: 0x060173F2 RID: 95218
		// (remove) Token: 0x060173F3 RID: 95219
		event HTMLInputImageEvents_onbeforepasteEventHandler onbeforepaste;

		// Token: 0x14002D27 RID: 11559
		// (add) Token: 0x060173F4 RID: 95220
		// (remove) Token: 0x060173F5 RID: 95221
		event HTMLInputImageEvents_onpasteEventHandler onpaste;

		// Token: 0x14002D28 RID: 11560
		// (add) Token: 0x060173F6 RID: 95222
		// (remove) Token: 0x060173F7 RID: 95223
		event HTMLInputImageEvents_oncontextmenuEventHandler oncontextmenu;

		// Token: 0x14002D29 RID: 11561
		// (add) Token: 0x060173F8 RID: 95224
		// (remove) Token: 0x060173F9 RID: 95225
		event HTMLInputImageEvents_onrowsdeleteEventHandler onrowsdelete;

		// Token: 0x14002D2A RID: 11562
		// (add) Token: 0x060173FA RID: 95226
		// (remove) Token: 0x060173FB RID: 95227
		event HTMLInputImageEvents_onrowsinsertedEventHandler onrowsinserted;

		// Token: 0x14002D2B RID: 11563
		// (add) Token: 0x060173FC RID: 95228
		// (remove) Token: 0x060173FD RID: 95229
		event HTMLInputImageEvents_oncellchangeEventHandler oncellchange;

		// Token: 0x14002D2C RID: 11564
		// (add) Token: 0x060173FE RID: 95230
		// (remove) Token: 0x060173FF RID: 95231
		event HTMLInputImageEvents_onreadystatechangeEventHandler onreadystatechange;

		// Token: 0x14002D2D RID: 11565
		// (add) Token: 0x06017400 RID: 95232
		// (remove) Token: 0x06017401 RID: 95233
		event HTMLInputImageEvents_onbeforeeditfocusEventHandler onbeforeeditfocus;

		// Token: 0x14002D2E RID: 11566
		// (add) Token: 0x06017402 RID: 95234
		// (remove) Token: 0x06017403 RID: 95235
		event HTMLInputImageEvents_onlayoutcompleteEventHandler onlayoutcomplete;

		// Token: 0x14002D2F RID: 11567
		// (add) Token: 0x06017404 RID: 95236
		// (remove) Token: 0x06017405 RID: 95237
		event HTMLInputImageEvents_onpageEventHandler onpage;

		// Token: 0x14002D30 RID: 11568
		// (add) Token: 0x06017406 RID: 95238
		// (remove) Token: 0x06017407 RID: 95239
		event HTMLInputImageEvents_onbeforedeactivateEventHandler onbeforedeactivate;

		// Token: 0x14002D31 RID: 11569
		// (add) Token: 0x06017408 RID: 95240
		// (remove) Token: 0x06017409 RID: 95241
		event HTMLInputImageEvents_onbeforeactivateEventHandler onbeforeactivate;

		// Token: 0x14002D32 RID: 11570
		// (add) Token: 0x0601740A RID: 95242
		// (remove) Token: 0x0601740B RID: 95243
		event HTMLInputImageEvents_onmoveEventHandler onmove;

		// Token: 0x14002D33 RID: 11571
		// (add) Token: 0x0601740C RID: 95244
		// (remove) Token: 0x0601740D RID: 95245
		event HTMLInputImageEvents_oncontrolselectEventHandler oncontrolselect;

		// Token: 0x14002D34 RID: 11572
		// (add) Token: 0x0601740E RID: 95246
		// (remove) Token: 0x0601740F RID: 95247
		event HTMLInputImageEvents_onmovestartEventHandler onmovestart;

		// Token: 0x14002D35 RID: 11573
		// (add) Token: 0x06017410 RID: 95248
		// (remove) Token: 0x06017411 RID: 95249
		event HTMLInputImageEvents_onmoveendEventHandler onmoveend;

		// Token: 0x14002D36 RID: 11574
		// (add) Token: 0x06017412 RID: 95250
		// (remove) Token: 0x06017413 RID: 95251
		event HTMLInputImageEvents_onresizestartEventHandler onresizestart;

		// Token: 0x14002D37 RID: 11575
		// (add) Token: 0x06017414 RID: 95252
		// (remove) Token: 0x06017415 RID: 95253
		event HTMLInputImageEvents_onresizeendEventHandler onresizeend;

		// Token: 0x14002D38 RID: 11576
		// (add) Token: 0x06017416 RID: 95254
		// (remove) Token: 0x06017417 RID: 95255
		event HTMLInputImageEvents_onmouseenterEventHandler onmouseenter;

		// Token: 0x14002D39 RID: 11577
		// (add) Token: 0x06017418 RID: 95256
		// (remove) Token: 0x06017419 RID: 95257
		event HTMLInputImageEvents_onmouseleaveEventHandler onmouseleave;

		// Token: 0x14002D3A RID: 11578
		// (add) Token: 0x0601741A RID: 95258
		// (remove) Token: 0x0601741B RID: 95259
		event HTMLInputImageEvents_onmousewheelEventHandler onmousewheel;

		// Token: 0x14002D3B RID: 11579
		// (add) Token: 0x0601741C RID: 95260
		// (remove) Token: 0x0601741D RID: 95261
		event HTMLInputImageEvents_onactivateEventHandler onactivate;

		// Token: 0x14002D3C RID: 11580
		// (add) Token: 0x0601741E RID: 95262
		// (remove) Token: 0x0601741F RID: 95263
		event HTMLInputImageEvents_ondeactivateEventHandler ondeactivate;

		// Token: 0x14002D3D RID: 11581
		// (add) Token: 0x06017420 RID: 95264
		// (remove) Token: 0x06017421 RID: 95265
		event HTMLInputImageEvents_onfocusinEventHandler onfocusin;

		// Token: 0x14002D3E RID: 11582
		// (add) Token: 0x06017422 RID: 95266
		// (remove) Token: 0x06017423 RID: 95267
		event HTMLInputImageEvents_onfocusoutEventHandler onfocusout;

		// Token: 0x14002D3F RID: 11583
		// (add) Token: 0x06017424 RID: 95268
		// (remove) Token: 0x06017425 RID: 95269
		event HTMLInputImageEvents_onloadEventHandler onload;

		// Token: 0x14002D40 RID: 11584
		// (add) Token: 0x06017426 RID: 95270
		// (remove) Token: 0x06017427 RID: 95271
		event HTMLInputImageEvents_onerrorEventHandler onerror;

		// Token: 0x14002D41 RID: 11585
		// (add) Token: 0x06017428 RID: 95272
		// (remove) Token: 0x06017429 RID: 95273
		event HTMLInputImageEvents_onabortEventHandler onabort;
	}
}
