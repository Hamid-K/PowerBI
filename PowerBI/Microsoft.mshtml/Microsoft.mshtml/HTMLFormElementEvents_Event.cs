using System;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x020001E8 RID: 488
	[ComEventInterface(typeof(HTMLFormElementEvents\u0000), typeof(HTMLFormElementEvents_EventProvider\u0000))]
	[ComVisible(false)]
	public interface HTMLFormElementEvents_Event
	{
		// Token: 0x1400027E RID: 638
		// (add) Token: 0x06002062 RID: 8290
		// (remove) Token: 0x06002063 RID: 8291
		event HTMLFormElementEvents_onhelpEventHandler onhelp;

		// Token: 0x1400027F RID: 639
		// (add) Token: 0x06002064 RID: 8292
		// (remove) Token: 0x06002065 RID: 8293
		event HTMLFormElementEvents_onclickEventHandler onclick;

		// Token: 0x14000280 RID: 640
		// (add) Token: 0x06002066 RID: 8294
		// (remove) Token: 0x06002067 RID: 8295
		event HTMLFormElementEvents_ondblclickEventHandler ondblclick;

		// Token: 0x14000281 RID: 641
		// (add) Token: 0x06002068 RID: 8296
		// (remove) Token: 0x06002069 RID: 8297
		event HTMLFormElementEvents_onkeypressEventHandler onkeypress;

		// Token: 0x14000282 RID: 642
		// (add) Token: 0x0600206A RID: 8298
		// (remove) Token: 0x0600206B RID: 8299
		event HTMLFormElementEvents_onkeydownEventHandler onkeydown;

		// Token: 0x14000283 RID: 643
		// (add) Token: 0x0600206C RID: 8300
		// (remove) Token: 0x0600206D RID: 8301
		event HTMLFormElementEvents_onkeyupEventHandler onkeyup;

		// Token: 0x14000284 RID: 644
		// (add) Token: 0x0600206E RID: 8302
		// (remove) Token: 0x0600206F RID: 8303
		event HTMLFormElementEvents_onmouseoutEventHandler onmouseout;

		// Token: 0x14000285 RID: 645
		// (add) Token: 0x06002070 RID: 8304
		// (remove) Token: 0x06002071 RID: 8305
		event HTMLFormElementEvents_onmouseoverEventHandler onmouseover;

		// Token: 0x14000286 RID: 646
		// (add) Token: 0x06002072 RID: 8306
		// (remove) Token: 0x06002073 RID: 8307
		event HTMLFormElementEvents_onmousemoveEventHandler onmousemove;

		// Token: 0x14000287 RID: 647
		// (add) Token: 0x06002074 RID: 8308
		// (remove) Token: 0x06002075 RID: 8309
		event HTMLFormElementEvents_onmousedownEventHandler onmousedown;

		// Token: 0x14000288 RID: 648
		// (add) Token: 0x06002076 RID: 8310
		// (remove) Token: 0x06002077 RID: 8311
		event HTMLFormElementEvents_onmouseupEventHandler onmouseup;

		// Token: 0x14000289 RID: 649
		// (add) Token: 0x06002078 RID: 8312
		// (remove) Token: 0x06002079 RID: 8313
		event HTMLFormElementEvents_onselectstartEventHandler onselectstart;

		// Token: 0x1400028A RID: 650
		// (add) Token: 0x0600207A RID: 8314
		// (remove) Token: 0x0600207B RID: 8315
		event HTMLFormElementEvents_onfilterchangeEventHandler onfilterchange;

		// Token: 0x1400028B RID: 651
		// (add) Token: 0x0600207C RID: 8316
		// (remove) Token: 0x0600207D RID: 8317
		event HTMLFormElementEvents_ondragstartEventHandler ondragstart;

		// Token: 0x1400028C RID: 652
		// (add) Token: 0x0600207E RID: 8318
		// (remove) Token: 0x0600207F RID: 8319
		event HTMLFormElementEvents_onbeforeupdateEventHandler onbeforeupdate;

		// Token: 0x1400028D RID: 653
		// (add) Token: 0x06002080 RID: 8320
		// (remove) Token: 0x06002081 RID: 8321
		event HTMLFormElementEvents_onafterupdateEventHandler onafterupdate;

		// Token: 0x1400028E RID: 654
		// (add) Token: 0x06002082 RID: 8322
		// (remove) Token: 0x06002083 RID: 8323
		event HTMLFormElementEvents_onerrorupdateEventHandler onerrorupdate;

		// Token: 0x1400028F RID: 655
		// (add) Token: 0x06002084 RID: 8324
		// (remove) Token: 0x06002085 RID: 8325
		event HTMLFormElementEvents_onrowexitEventHandler onrowexit;

		// Token: 0x14000290 RID: 656
		// (add) Token: 0x06002086 RID: 8326
		// (remove) Token: 0x06002087 RID: 8327
		event HTMLFormElementEvents_onrowenterEventHandler onrowenter;

		// Token: 0x14000291 RID: 657
		// (add) Token: 0x06002088 RID: 8328
		// (remove) Token: 0x06002089 RID: 8329
		event HTMLFormElementEvents_ondatasetchangedEventHandler ondatasetchanged;

		// Token: 0x14000292 RID: 658
		// (add) Token: 0x0600208A RID: 8330
		// (remove) Token: 0x0600208B RID: 8331
		event HTMLFormElementEvents_ondataavailableEventHandler ondataavailable;

		// Token: 0x14000293 RID: 659
		// (add) Token: 0x0600208C RID: 8332
		// (remove) Token: 0x0600208D RID: 8333
		event HTMLFormElementEvents_ondatasetcompleteEventHandler ondatasetcomplete;

		// Token: 0x14000294 RID: 660
		// (add) Token: 0x0600208E RID: 8334
		// (remove) Token: 0x0600208F RID: 8335
		event HTMLFormElementEvents_onlosecaptureEventHandler onlosecapture;

		// Token: 0x14000295 RID: 661
		// (add) Token: 0x06002090 RID: 8336
		// (remove) Token: 0x06002091 RID: 8337
		event HTMLFormElementEvents_onpropertychangeEventHandler onpropertychange;

		// Token: 0x14000296 RID: 662
		// (add) Token: 0x06002092 RID: 8338
		// (remove) Token: 0x06002093 RID: 8339
		event HTMLFormElementEvents_onscrollEventHandler onscroll;

		// Token: 0x14000297 RID: 663
		// (add) Token: 0x06002094 RID: 8340
		// (remove) Token: 0x06002095 RID: 8341
		event HTMLFormElementEvents_onfocusEventHandler onfocus;

		// Token: 0x14000298 RID: 664
		// (add) Token: 0x06002096 RID: 8342
		// (remove) Token: 0x06002097 RID: 8343
		event HTMLFormElementEvents_onblurEventHandler onblur;

		// Token: 0x14000299 RID: 665
		// (add) Token: 0x06002098 RID: 8344
		// (remove) Token: 0x06002099 RID: 8345
		event HTMLFormElementEvents_onresizeEventHandler onresize;

		// Token: 0x1400029A RID: 666
		// (add) Token: 0x0600209A RID: 8346
		// (remove) Token: 0x0600209B RID: 8347
		event HTMLFormElementEvents_ondragEventHandler ondrag;

		// Token: 0x1400029B RID: 667
		// (add) Token: 0x0600209C RID: 8348
		// (remove) Token: 0x0600209D RID: 8349
		event HTMLFormElementEvents_ondragendEventHandler ondragend;

		// Token: 0x1400029C RID: 668
		// (add) Token: 0x0600209E RID: 8350
		// (remove) Token: 0x0600209F RID: 8351
		event HTMLFormElementEvents_ondragenterEventHandler ondragenter;

		// Token: 0x1400029D RID: 669
		// (add) Token: 0x060020A0 RID: 8352
		// (remove) Token: 0x060020A1 RID: 8353
		event HTMLFormElementEvents_ondragoverEventHandler ondragover;

		// Token: 0x1400029E RID: 670
		// (add) Token: 0x060020A2 RID: 8354
		// (remove) Token: 0x060020A3 RID: 8355
		event HTMLFormElementEvents_ondragleaveEventHandler ondragleave;

		// Token: 0x1400029F RID: 671
		// (add) Token: 0x060020A4 RID: 8356
		// (remove) Token: 0x060020A5 RID: 8357
		event HTMLFormElementEvents_ondropEventHandler ondrop;

		// Token: 0x140002A0 RID: 672
		// (add) Token: 0x060020A6 RID: 8358
		// (remove) Token: 0x060020A7 RID: 8359
		event HTMLFormElementEvents_onbeforecutEventHandler onbeforecut;

		// Token: 0x140002A1 RID: 673
		// (add) Token: 0x060020A8 RID: 8360
		// (remove) Token: 0x060020A9 RID: 8361
		event HTMLFormElementEvents_oncutEventHandler oncut;

		// Token: 0x140002A2 RID: 674
		// (add) Token: 0x060020AA RID: 8362
		// (remove) Token: 0x060020AB RID: 8363
		event HTMLFormElementEvents_onbeforecopyEventHandler onbeforecopy;

		// Token: 0x140002A3 RID: 675
		// (add) Token: 0x060020AC RID: 8364
		// (remove) Token: 0x060020AD RID: 8365
		event HTMLFormElementEvents_oncopyEventHandler oncopy;

		// Token: 0x140002A4 RID: 676
		// (add) Token: 0x060020AE RID: 8366
		// (remove) Token: 0x060020AF RID: 8367
		event HTMLFormElementEvents_onbeforepasteEventHandler onbeforepaste;

		// Token: 0x140002A5 RID: 677
		// (add) Token: 0x060020B0 RID: 8368
		// (remove) Token: 0x060020B1 RID: 8369
		event HTMLFormElementEvents_onpasteEventHandler onpaste;

		// Token: 0x140002A6 RID: 678
		// (add) Token: 0x060020B2 RID: 8370
		// (remove) Token: 0x060020B3 RID: 8371
		event HTMLFormElementEvents_oncontextmenuEventHandler oncontextmenu;

		// Token: 0x140002A7 RID: 679
		// (add) Token: 0x060020B4 RID: 8372
		// (remove) Token: 0x060020B5 RID: 8373
		event HTMLFormElementEvents_onrowsdeleteEventHandler onrowsdelete;

		// Token: 0x140002A8 RID: 680
		// (add) Token: 0x060020B6 RID: 8374
		// (remove) Token: 0x060020B7 RID: 8375
		event HTMLFormElementEvents_onrowsinsertedEventHandler onrowsinserted;

		// Token: 0x140002A9 RID: 681
		// (add) Token: 0x060020B8 RID: 8376
		// (remove) Token: 0x060020B9 RID: 8377
		event HTMLFormElementEvents_oncellchangeEventHandler oncellchange;

		// Token: 0x140002AA RID: 682
		// (add) Token: 0x060020BA RID: 8378
		// (remove) Token: 0x060020BB RID: 8379
		event HTMLFormElementEvents_onreadystatechangeEventHandler onreadystatechange;

		// Token: 0x140002AB RID: 683
		// (add) Token: 0x060020BC RID: 8380
		// (remove) Token: 0x060020BD RID: 8381
		event HTMLFormElementEvents_onbeforeeditfocusEventHandler onbeforeeditfocus;

		// Token: 0x140002AC RID: 684
		// (add) Token: 0x060020BE RID: 8382
		// (remove) Token: 0x060020BF RID: 8383
		event HTMLFormElementEvents_onlayoutcompleteEventHandler onlayoutcomplete;

		// Token: 0x140002AD RID: 685
		// (add) Token: 0x060020C0 RID: 8384
		// (remove) Token: 0x060020C1 RID: 8385
		event HTMLFormElementEvents_onpageEventHandler onpage;

		// Token: 0x140002AE RID: 686
		// (add) Token: 0x060020C2 RID: 8386
		// (remove) Token: 0x060020C3 RID: 8387
		event HTMLFormElementEvents_onbeforedeactivateEventHandler onbeforedeactivate;

		// Token: 0x140002AF RID: 687
		// (add) Token: 0x060020C4 RID: 8388
		// (remove) Token: 0x060020C5 RID: 8389
		event HTMLFormElementEvents_onbeforeactivateEventHandler onbeforeactivate;

		// Token: 0x140002B0 RID: 688
		// (add) Token: 0x060020C6 RID: 8390
		// (remove) Token: 0x060020C7 RID: 8391
		event HTMLFormElementEvents_onmoveEventHandler onmove;

		// Token: 0x140002B1 RID: 689
		// (add) Token: 0x060020C8 RID: 8392
		// (remove) Token: 0x060020C9 RID: 8393
		event HTMLFormElementEvents_oncontrolselectEventHandler oncontrolselect;

		// Token: 0x140002B2 RID: 690
		// (add) Token: 0x060020CA RID: 8394
		// (remove) Token: 0x060020CB RID: 8395
		event HTMLFormElementEvents_onmovestartEventHandler onmovestart;

		// Token: 0x140002B3 RID: 691
		// (add) Token: 0x060020CC RID: 8396
		// (remove) Token: 0x060020CD RID: 8397
		event HTMLFormElementEvents_onmoveendEventHandler onmoveend;

		// Token: 0x140002B4 RID: 692
		// (add) Token: 0x060020CE RID: 8398
		// (remove) Token: 0x060020CF RID: 8399
		event HTMLFormElementEvents_onresizestartEventHandler onresizestart;

		// Token: 0x140002B5 RID: 693
		// (add) Token: 0x060020D0 RID: 8400
		// (remove) Token: 0x060020D1 RID: 8401
		event HTMLFormElementEvents_onresizeendEventHandler onresizeend;

		// Token: 0x140002B6 RID: 694
		// (add) Token: 0x060020D2 RID: 8402
		// (remove) Token: 0x060020D3 RID: 8403
		event HTMLFormElementEvents_onmouseenterEventHandler onmouseenter;

		// Token: 0x140002B7 RID: 695
		// (add) Token: 0x060020D4 RID: 8404
		// (remove) Token: 0x060020D5 RID: 8405
		event HTMLFormElementEvents_onmouseleaveEventHandler onmouseleave;

		// Token: 0x140002B8 RID: 696
		// (add) Token: 0x060020D6 RID: 8406
		// (remove) Token: 0x060020D7 RID: 8407
		event HTMLFormElementEvents_onmousewheelEventHandler onmousewheel;

		// Token: 0x140002B9 RID: 697
		// (add) Token: 0x060020D8 RID: 8408
		// (remove) Token: 0x060020D9 RID: 8409
		event HTMLFormElementEvents_onactivateEventHandler onactivate;

		// Token: 0x140002BA RID: 698
		// (add) Token: 0x060020DA RID: 8410
		// (remove) Token: 0x060020DB RID: 8411
		event HTMLFormElementEvents_ondeactivateEventHandler ondeactivate;

		// Token: 0x140002BB RID: 699
		// (add) Token: 0x060020DC RID: 8412
		// (remove) Token: 0x060020DD RID: 8413
		event HTMLFormElementEvents_onfocusinEventHandler onfocusin;

		// Token: 0x140002BC RID: 700
		// (add) Token: 0x060020DE RID: 8414
		// (remove) Token: 0x060020DF RID: 8415
		event HTMLFormElementEvents_onfocusoutEventHandler onfocusout;

		// Token: 0x140002BD RID: 701
		// (add) Token: 0x060020E0 RID: 8416
		// (remove) Token: 0x060020E1 RID: 8417
		event HTMLFormElementEvents_onsubmitEventHandler onsubmit;

		// Token: 0x140002BE RID: 702
		// (add) Token: 0x060020E2 RID: 8418
		// (remove) Token: 0x060020E3 RID: 8419
		event HTMLFormElementEvents_onresetEventHandler onreset;
	}
}
