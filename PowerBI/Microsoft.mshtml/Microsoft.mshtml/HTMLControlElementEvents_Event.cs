using System;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x0200084A RID: 2122
	[ComVisible(false)]
	[ComEventInterface(typeof(HTMLControlElementEvents\u0000), typeof(HTMLControlElementEvents_EventProvider\u0000))]
	public interface HTMLControlElementEvents_Event
	{
		// Token: 0x14001B86 RID: 7046
		// (add) Token: 0x0600E398 RID: 58264
		// (remove) Token: 0x0600E399 RID: 58265
		event HTMLControlElementEvents_onhelpEventHandler onhelp;

		// Token: 0x14001B87 RID: 7047
		// (add) Token: 0x0600E39A RID: 58266
		// (remove) Token: 0x0600E39B RID: 58267
		event HTMLControlElementEvents_onclickEventHandler onclick;

		// Token: 0x14001B88 RID: 7048
		// (add) Token: 0x0600E39C RID: 58268
		// (remove) Token: 0x0600E39D RID: 58269
		event HTMLControlElementEvents_ondblclickEventHandler ondblclick;

		// Token: 0x14001B89 RID: 7049
		// (add) Token: 0x0600E39E RID: 58270
		// (remove) Token: 0x0600E39F RID: 58271
		event HTMLControlElementEvents_onkeypressEventHandler onkeypress;

		// Token: 0x14001B8A RID: 7050
		// (add) Token: 0x0600E3A0 RID: 58272
		// (remove) Token: 0x0600E3A1 RID: 58273
		event HTMLControlElementEvents_onkeydownEventHandler onkeydown;

		// Token: 0x14001B8B RID: 7051
		// (add) Token: 0x0600E3A2 RID: 58274
		// (remove) Token: 0x0600E3A3 RID: 58275
		event HTMLControlElementEvents_onkeyupEventHandler onkeyup;

		// Token: 0x14001B8C RID: 7052
		// (add) Token: 0x0600E3A4 RID: 58276
		// (remove) Token: 0x0600E3A5 RID: 58277
		event HTMLControlElementEvents_onmouseoutEventHandler onmouseout;

		// Token: 0x14001B8D RID: 7053
		// (add) Token: 0x0600E3A6 RID: 58278
		// (remove) Token: 0x0600E3A7 RID: 58279
		event HTMLControlElementEvents_onmouseoverEventHandler onmouseover;

		// Token: 0x14001B8E RID: 7054
		// (add) Token: 0x0600E3A8 RID: 58280
		// (remove) Token: 0x0600E3A9 RID: 58281
		event HTMLControlElementEvents_onmousemoveEventHandler onmousemove;

		// Token: 0x14001B8F RID: 7055
		// (add) Token: 0x0600E3AA RID: 58282
		// (remove) Token: 0x0600E3AB RID: 58283
		event HTMLControlElementEvents_onmousedownEventHandler onmousedown;

		// Token: 0x14001B90 RID: 7056
		// (add) Token: 0x0600E3AC RID: 58284
		// (remove) Token: 0x0600E3AD RID: 58285
		event HTMLControlElementEvents_onmouseupEventHandler onmouseup;

		// Token: 0x14001B91 RID: 7057
		// (add) Token: 0x0600E3AE RID: 58286
		// (remove) Token: 0x0600E3AF RID: 58287
		event HTMLControlElementEvents_onselectstartEventHandler onselectstart;

		// Token: 0x14001B92 RID: 7058
		// (add) Token: 0x0600E3B0 RID: 58288
		// (remove) Token: 0x0600E3B1 RID: 58289
		event HTMLControlElementEvents_onfilterchangeEventHandler onfilterchange;

		// Token: 0x14001B93 RID: 7059
		// (add) Token: 0x0600E3B2 RID: 58290
		// (remove) Token: 0x0600E3B3 RID: 58291
		event HTMLControlElementEvents_ondragstartEventHandler ondragstart;

		// Token: 0x14001B94 RID: 7060
		// (add) Token: 0x0600E3B4 RID: 58292
		// (remove) Token: 0x0600E3B5 RID: 58293
		event HTMLControlElementEvents_onbeforeupdateEventHandler onbeforeupdate;

		// Token: 0x14001B95 RID: 7061
		// (add) Token: 0x0600E3B6 RID: 58294
		// (remove) Token: 0x0600E3B7 RID: 58295
		event HTMLControlElementEvents_onafterupdateEventHandler onafterupdate;

		// Token: 0x14001B96 RID: 7062
		// (add) Token: 0x0600E3B8 RID: 58296
		// (remove) Token: 0x0600E3B9 RID: 58297
		event HTMLControlElementEvents_onerrorupdateEventHandler onerrorupdate;

		// Token: 0x14001B97 RID: 7063
		// (add) Token: 0x0600E3BA RID: 58298
		// (remove) Token: 0x0600E3BB RID: 58299
		event HTMLControlElementEvents_onrowexitEventHandler onrowexit;

		// Token: 0x14001B98 RID: 7064
		// (add) Token: 0x0600E3BC RID: 58300
		// (remove) Token: 0x0600E3BD RID: 58301
		event HTMLControlElementEvents_onrowenterEventHandler onrowenter;

		// Token: 0x14001B99 RID: 7065
		// (add) Token: 0x0600E3BE RID: 58302
		// (remove) Token: 0x0600E3BF RID: 58303
		event HTMLControlElementEvents_ondatasetchangedEventHandler ondatasetchanged;

		// Token: 0x14001B9A RID: 7066
		// (add) Token: 0x0600E3C0 RID: 58304
		// (remove) Token: 0x0600E3C1 RID: 58305
		event HTMLControlElementEvents_ondataavailableEventHandler ondataavailable;

		// Token: 0x14001B9B RID: 7067
		// (add) Token: 0x0600E3C2 RID: 58306
		// (remove) Token: 0x0600E3C3 RID: 58307
		event HTMLControlElementEvents_ondatasetcompleteEventHandler ondatasetcomplete;

		// Token: 0x14001B9C RID: 7068
		// (add) Token: 0x0600E3C4 RID: 58308
		// (remove) Token: 0x0600E3C5 RID: 58309
		event HTMLControlElementEvents_onlosecaptureEventHandler onlosecapture;

		// Token: 0x14001B9D RID: 7069
		// (add) Token: 0x0600E3C6 RID: 58310
		// (remove) Token: 0x0600E3C7 RID: 58311
		event HTMLControlElementEvents_onpropertychangeEventHandler onpropertychange;

		// Token: 0x14001B9E RID: 7070
		// (add) Token: 0x0600E3C8 RID: 58312
		// (remove) Token: 0x0600E3C9 RID: 58313
		event HTMLControlElementEvents_onscrollEventHandler onscroll;

		// Token: 0x14001B9F RID: 7071
		// (add) Token: 0x0600E3CA RID: 58314
		// (remove) Token: 0x0600E3CB RID: 58315
		event HTMLControlElementEvents_onfocusEventHandler onfocus;

		// Token: 0x14001BA0 RID: 7072
		// (add) Token: 0x0600E3CC RID: 58316
		// (remove) Token: 0x0600E3CD RID: 58317
		event HTMLControlElementEvents_onblurEventHandler onblur;

		// Token: 0x14001BA1 RID: 7073
		// (add) Token: 0x0600E3CE RID: 58318
		// (remove) Token: 0x0600E3CF RID: 58319
		event HTMLControlElementEvents_onresizeEventHandler onresize;

		// Token: 0x14001BA2 RID: 7074
		// (add) Token: 0x0600E3D0 RID: 58320
		// (remove) Token: 0x0600E3D1 RID: 58321
		event HTMLControlElementEvents_ondragEventHandler ondrag;

		// Token: 0x14001BA3 RID: 7075
		// (add) Token: 0x0600E3D2 RID: 58322
		// (remove) Token: 0x0600E3D3 RID: 58323
		event HTMLControlElementEvents_ondragendEventHandler ondragend;

		// Token: 0x14001BA4 RID: 7076
		// (add) Token: 0x0600E3D4 RID: 58324
		// (remove) Token: 0x0600E3D5 RID: 58325
		event HTMLControlElementEvents_ondragenterEventHandler ondragenter;

		// Token: 0x14001BA5 RID: 7077
		// (add) Token: 0x0600E3D6 RID: 58326
		// (remove) Token: 0x0600E3D7 RID: 58327
		event HTMLControlElementEvents_ondragoverEventHandler ondragover;

		// Token: 0x14001BA6 RID: 7078
		// (add) Token: 0x0600E3D8 RID: 58328
		// (remove) Token: 0x0600E3D9 RID: 58329
		event HTMLControlElementEvents_ondragleaveEventHandler ondragleave;

		// Token: 0x14001BA7 RID: 7079
		// (add) Token: 0x0600E3DA RID: 58330
		// (remove) Token: 0x0600E3DB RID: 58331
		event HTMLControlElementEvents_ondropEventHandler ondrop;

		// Token: 0x14001BA8 RID: 7080
		// (add) Token: 0x0600E3DC RID: 58332
		// (remove) Token: 0x0600E3DD RID: 58333
		event HTMLControlElementEvents_onbeforecutEventHandler onbeforecut;

		// Token: 0x14001BA9 RID: 7081
		// (add) Token: 0x0600E3DE RID: 58334
		// (remove) Token: 0x0600E3DF RID: 58335
		event HTMLControlElementEvents_oncutEventHandler oncut;

		// Token: 0x14001BAA RID: 7082
		// (add) Token: 0x0600E3E0 RID: 58336
		// (remove) Token: 0x0600E3E1 RID: 58337
		event HTMLControlElementEvents_onbeforecopyEventHandler onbeforecopy;

		// Token: 0x14001BAB RID: 7083
		// (add) Token: 0x0600E3E2 RID: 58338
		// (remove) Token: 0x0600E3E3 RID: 58339
		event HTMLControlElementEvents_oncopyEventHandler oncopy;

		// Token: 0x14001BAC RID: 7084
		// (add) Token: 0x0600E3E4 RID: 58340
		// (remove) Token: 0x0600E3E5 RID: 58341
		event HTMLControlElementEvents_onbeforepasteEventHandler onbeforepaste;

		// Token: 0x14001BAD RID: 7085
		// (add) Token: 0x0600E3E6 RID: 58342
		// (remove) Token: 0x0600E3E7 RID: 58343
		event HTMLControlElementEvents_onpasteEventHandler onpaste;

		// Token: 0x14001BAE RID: 7086
		// (add) Token: 0x0600E3E8 RID: 58344
		// (remove) Token: 0x0600E3E9 RID: 58345
		event HTMLControlElementEvents_oncontextmenuEventHandler oncontextmenu;

		// Token: 0x14001BAF RID: 7087
		// (add) Token: 0x0600E3EA RID: 58346
		// (remove) Token: 0x0600E3EB RID: 58347
		event HTMLControlElementEvents_onrowsdeleteEventHandler onrowsdelete;

		// Token: 0x14001BB0 RID: 7088
		// (add) Token: 0x0600E3EC RID: 58348
		// (remove) Token: 0x0600E3ED RID: 58349
		event HTMLControlElementEvents_onrowsinsertedEventHandler onrowsinserted;

		// Token: 0x14001BB1 RID: 7089
		// (add) Token: 0x0600E3EE RID: 58350
		// (remove) Token: 0x0600E3EF RID: 58351
		event HTMLControlElementEvents_oncellchangeEventHandler oncellchange;

		// Token: 0x14001BB2 RID: 7090
		// (add) Token: 0x0600E3F0 RID: 58352
		// (remove) Token: 0x0600E3F1 RID: 58353
		event HTMLControlElementEvents_onreadystatechangeEventHandler onreadystatechange;

		// Token: 0x14001BB3 RID: 7091
		// (add) Token: 0x0600E3F2 RID: 58354
		// (remove) Token: 0x0600E3F3 RID: 58355
		event HTMLControlElementEvents_onbeforeeditfocusEventHandler onbeforeeditfocus;

		// Token: 0x14001BB4 RID: 7092
		// (add) Token: 0x0600E3F4 RID: 58356
		// (remove) Token: 0x0600E3F5 RID: 58357
		event HTMLControlElementEvents_onlayoutcompleteEventHandler onlayoutcomplete;

		// Token: 0x14001BB5 RID: 7093
		// (add) Token: 0x0600E3F6 RID: 58358
		// (remove) Token: 0x0600E3F7 RID: 58359
		event HTMLControlElementEvents_onpageEventHandler onpage;

		// Token: 0x14001BB6 RID: 7094
		// (add) Token: 0x0600E3F8 RID: 58360
		// (remove) Token: 0x0600E3F9 RID: 58361
		event HTMLControlElementEvents_onbeforedeactivateEventHandler onbeforedeactivate;

		// Token: 0x14001BB7 RID: 7095
		// (add) Token: 0x0600E3FA RID: 58362
		// (remove) Token: 0x0600E3FB RID: 58363
		event HTMLControlElementEvents_onbeforeactivateEventHandler onbeforeactivate;

		// Token: 0x14001BB8 RID: 7096
		// (add) Token: 0x0600E3FC RID: 58364
		// (remove) Token: 0x0600E3FD RID: 58365
		event HTMLControlElementEvents_onmoveEventHandler onmove;

		// Token: 0x14001BB9 RID: 7097
		// (add) Token: 0x0600E3FE RID: 58366
		// (remove) Token: 0x0600E3FF RID: 58367
		event HTMLControlElementEvents_oncontrolselectEventHandler oncontrolselect;

		// Token: 0x14001BBA RID: 7098
		// (add) Token: 0x0600E400 RID: 58368
		// (remove) Token: 0x0600E401 RID: 58369
		event HTMLControlElementEvents_onmovestartEventHandler onmovestart;

		// Token: 0x14001BBB RID: 7099
		// (add) Token: 0x0600E402 RID: 58370
		// (remove) Token: 0x0600E403 RID: 58371
		event HTMLControlElementEvents_onmoveendEventHandler onmoveend;

		// Token: 0x14001BBC RID: 7100
		// (add) Token: 0x0600E404 RID: 58372
		// (remove) Token: 0x0600E405 RID: 58373
		event HTMLControlElementEvents_onresizestartEventHandler onresizestart;

		// Token: 0x14001BBD RID: 7101
		// (add) Token: 0x0600E406 RID: 58374
		// (remove) Token: 0x0600E407 RID: 58375
		event HTMLControlElementEvents_onresizeendEventHandler onresizeend;

		// Token: 0x14001BBE RID: 7102
		// (add) Token: 0x0600E408 RID: 58376
		// (remove) Token: 0x0600E409 RID: 58377
		event HTMLControlElementEvents_onmouseenterEventHandler onmouseenter;

		// Token: 0x14001BBF RID: 7103
		// (add) Token: 0x0600E40A RID: 58378
		// (remove) Token: 0x0600E40B RID: 58379
		event HTMLControlElementEvents_onmouseleaveEventHandler onmouseleave;

		// Token: 0x14001BC0 RID: 7104
		// (add) Token: 0x0600E40C RID: 58380
		// (remove) Token: 0x0600E40D RID: 58381
		event HTMLControlElementEvents_onmousewheelEventHandler onmousewheel;

		// Token: 0x14001BC1 RID: 7105
		// (add) Token: 0x0600E40E RID: 58382
		// (remove) Token: 0x0600E40F RID: 58383
		event HTMLControlElementEvents_onactivateEventHandler onactivate;

		// Token: 0x14001BC2 RID: 7106
		// (add) Token: 0x0600E410 RID: 58384
		// (remove) Token: 0x0600E411 RID: 58385
		event HTMLControlElementEvents_ondeactivateEventHandler ondeactivate;

		// Token: 0x14001BC3 RID: 7107
		// (add) Token: 0x0600E412 RID: 58386
		// (remove) Token: 0x0600E413 RID: 58387
		event HTMLControlElementEvents_onfocusinEventHandler onfocusin;

		// Token: 0x14001BC4 RID: 7108
		// (add) Token: 0x0600E414 RID: 58388
		// (remove) Token: 0x0600E415 RID: 58389
		event HTMLControlElementEvents_onfocusoutEventHandler onfocusout;
	}
}
