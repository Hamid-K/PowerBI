using System;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000155 RID: 341
	[ComVisible(false)]
	[ComEventInterface(typeof(HTMLLinkElementEvents\u0000), typeof(HTMLLinkElementEvents_EventProvider\u0000))]
	public interface HTMLLinkElementEvents_Event
	{
		// Token: 0x1400017C RID: 380
		// (add) Token: 0x06001926 RID: 6438
		// (remove) Token: 0x06001927 RID: 6439
		event HTMLLinkElementEvents_onhelpEventHandler onhelp;

		// Token: 0x1400017D RID: 381
		// (add) Token: 0x06001928 RID: 6440
		// (remove) Token: 0x06001929 RID: 6441
		event HTMLLinkElementEvents_onclickEventHandler onclick;

		// Token: 0x1400017E RID: 382
		// (add) Token: 0x0600192A RID: 6442
		// (remove) Token: 0x0600192B RID: 6443
		event HTMLLinkElementEvents_ondblclickEventHandler ondblclick;

		// Token: 0x1400017F RID: 383
		// (add) Token: 0x0600192C RID: 6444
		// (remove) Token: 0x0600192D RID: 6445
		event HTMLLinkElementEvents_onkeypressEventHandler onkeypress;

		// Token: 0x14000180 RID: 384
		// (add) Token: 0x0600192E RID: 6446
		// (remove) Token: 0x0600192F RID: 6447
		event HTMLLinkElementEvents_onkeydownEventHandler onkeydown;

		// Token: 0x14000181 RID: 385
		// (add) Token: 0x06001930 RID: 6448
		// (remove) Token: 0x06001931 RID: 6449
		event HTMLLinkElementEvents_onkeyupEventHandler onkeyup;

		// Token: 0x14000182 RID: 386
		// (add) Token: 0x06001932 RID: 6450
		// (remove) Token: 0x06001933 RID: 6451
		event HTMLLinkElementEvents_onmouseoutEventHandler onmouseout;

		// Token: 0x14000183 RID: 387
		// (add) Token: 0x06001934 RID: 6452
		// (remove) Token: 0x06001935 RID: 6453
		event HTMLLinkElementEvents_onmouseoverEventHandler onmouseover;

		// Token: 0x14000184 RID: 388
		// (add) Token: 0x06001936 RID: 6454
		// (remove) Token: 0x06001937 RID: 6455
		event HTMLLinkElementEvents_onmousemoveEventHandler onmousemove;

		// Token: 0x14000185 RID: 389
		// (add) Token: 0x06001938 RID: 6456
		// (remove) Token: 0x06001939 RID: 6457
		event HTMLLinkElementEvents_onmousedownEventHandler onmousedown;

		// Token: 0x14000186 RID: 390
		// (add) Token: 0x0600193A RID: 6458
		// (remove) Token: 0x0600193B RID: 6459
		event HTMLLinkElementEvents_onmouseupEventHandler onmouseup;

		// Token: 0x14000187 RID: 391
		// (add) Token: 0x0600193C RID: 6460
		// (remove) Token: 0x0600193D RID: 6461
		event HTMLLinkElementEvents_onselectstartEventHandler onselectstart;

		// Token: 0x14000188 RID: 392
		// (add) Token: 0x0600193E RID: 6462
		// (remove) Token: 0x0600193F RID: 6463
		event HTMLLinkElementEvents_onfilterchangeEventHandler onfilterchange;

		// Token: 0x14000189 RID: 393
		// (add) Token: 0x06001940 RID: 6464
		// (remove) Token: 0x06001941 RID: 6465
		event HTMLLinkElementEvents_ondragstartEventHandler ondragstart;

		// Token: 0x1400018A RID: 394
		// (add) Token: 0x06001942 RID: 6466
		// (remove) Token: 0x06001943 RID: 6467
		event HTMLLinkElementEvents_onbeforeupdateEventHandler onbeforeupdate;

		// Token: 0x1400018B RID: 395
		// (add) Token: 0x06001944 RID: 6468
		// (remove) Token: 0x06001945 RID: 6469
		event HTMLLinkElementEvents_onafterupdateEventHandler onafterupdate;

		// Token: 0x1400018C RID: 396
		// (add) Token: 0x06001946 RID: 6470
		// (remove) Token: 0x06001947 RID: 6471
		event HTMLLinkElementEvents_onerrorupdateEventHandler onerrorupdate;

		// Token: 0x1400018D RID: 397
		// (add) Token: 0x06001948 RID: 6472
		// (remove) Token: 0x06001949 RID: 6473
		event HTMLLinkElementEvents_onrowexitEventHandler onrowexit;

		// Token: 0x1400018E RID: 398
		// (add) Token: 0x0600194A RID: 6474
		// (remove) Token: 0x0600194B RID: 6475
		event HTMLLinkElementEvents_onrowenterEventHandler onrowenter;

		// Token: 0x1400018F RID: 399
		// (add) Token: 0x0600194C RID: 6476
		// (remove) Token: 0x0600194D RID: 6477
		event HTMLLinkElementEvents_ondatasetchangedEventHandler ondatasetchanged;

		// Token: 0x14000190 RID: 400
		// (add) Token: 0x0600194E RID: 6478
		// (remove) Token: 0x0600194F RID: 6479
		event HTMLLinkElementEvents_ondataavailableEventHandler ondataavailable;

		// Token: 0x14000191 RID: 401
		// (add) Token: 0x06001950 RID: 6480
		// (remove) Token: 0x06001951 RID: 6481
		event HTMLLinkElementEvents_ondatasetcompleteEventHandler ondatasetcomplete;

		// Token: 0x14000192 RID: 402
		// (add) Token: 0x06001952 RID: 6482
		// (remove) Token: 0x06001953 RID: 6483
		event HTMLLinkElementEvents_onlosecaptureEventHandler onlosecapture;

		// Token: 0x14000193 RID: 403
		// (add) Token: 0x06001954 RID: 6484
		// (remove) Token: 0x06001955 RID: 6485
		event HTMLLinkElementEvents_onpropertychangeEventHandler onpropertychange;

		// Token: 0x14000194 RID: 404
		// (add) Token: 0x06001956 RID: 6486
		// (remove) Token: 0x06001957 RID: 6487
		event HTMLLinkElementEvents_onscrollEventHandler onscroll;

		// Token: 0x14000195 RID: 405
		// (add) Token: 0x06001958 RID: 6488
		// (remove) Token: 0x06001959 RID: 6489
		event HTMLLinkElementEvents_onfocusEventHandler onfocus;

		// Token: 0x14000196 RID: 406
		// (add) Token: 0x0600195A RID: 6490
		// (remove) Token: 0x0600195B RID: 6491
		event HTMLLinkElementEvents_onblurEventHandler onblur;

		// Token: 0x14000197 RID: 407
		// (add) Token: 0x0600195C RID: 6492
		// (remove) Token: 0x0600195D RID: 6493
		event HTMLLinkElementEvents_onresizeEventHandler onresize;

		// Token: 0x14000198 RID: 408
		// (add) Token: 0x0600195E RID: 6494
		// (remove) Token: 0x0600195F RID: 6495
		event HTMLLinkElementEvents_ondragEventHandler ondrag;

		// Token: 0x14000199 RID: 409
		// (add) Token: 0x06001960 RID: 6496
		// (remove) Token: 0x06001961 RID: 6497
		event HTMLLinkElementEvents_ondragendEventHandler ondragend;

		// Token: 0x1400019A RID: 410
		// (add) Token: 0x06001962 RID: 6498
		// (remove) Token: 0x06001963 RID: 6499
		event HTMLLinkElementEvents_ondragenterEventHandler ondragenter;

		// Token: 0x1400019B RID: 411
		// (add) Token: 0x06001964 RID: 6500
		// (remove) Token: 0x06001965 RID: 6501
		event HTMLLinkElementEvents_ondragoverEventHandler ondragover;

		// Token: 0x1400019C RID: 412
		// (add) Token: 0x06001966 RID: 6502
		// (remove) Token: 0x06001967 RID: 6503
		event HTMLLinkElementEvents_ondragleaveEventHandler ondragleave;

		// Token: 0x1400019D RID: 413
		// (add) Token: 0x06001968 RID: 6504
		// (remove) Token: 0x06001969 RID: 6505
		event HTMLLinkElementEvents_ondropEventHandler ondrop;

		// Token: 0x1400019E RID: 414
		// (add) Token: 0x0600196A RID: 6506
		// (remove) Token: 0x0600196B RID: 6507
		event HTMLLinkElementEvents_onbeforecutEventHandler onbeforecut;

		// Token: 0x1400019F RID: 415
		// (add) Token: 0x0600196C RID: 6508
		// (remove) Token: 0x0600196D RID: 6509
		event HTMLLinkElementEvents_oncutEventHandler oncut;

		// Token: 0x140001A0 RID: 416
		// (add) Token: 0x0600196E RID: 6510
		// (remove) Token: 0x0600196F RID: 6511
		event HTMLLinkElementEvents_onbeforecopyEventHandler onbeforecopy;

		// Token: 0x140001A1 RID: 417
		// (add) Token: 0x06001970 RID: 6512
		// (remove) Token: 0x06001971 RID: 6513
		event HTMLLinkElementEvents_oncopyEventHandler oncopy;

		// Token: 0x140001A2 RID: 418
		// (add) Token: 0x06001972 RID: 6514
		// (remove) Token: 0x06001973 RID: 6515
		event HTMLLinkElementEvents_onbeforepasteEventHandler onbeforepaste;

		// Token: 0x140001A3 RID: 419
		// (add) Token: 0x06001974 RID: 6516
		// (remove) Token: 0x06001975 RID: 6517
		event HTMLLinkElementEvents_onpasteEventHandler onpaste;

		// Token: 0x140001A4 RID: 420
		// (add) Token: 0x06001976 RID: 6518
		// (remove) Token: 0x06001977 RID: 6519
		event HTMLLinkElementEvents_oncontextmenuEventHandler oncontextmenu;

		// Token: 0x140001A5 RID: 421
		// (add) Token: 0x06001978 RID: 6520
		// (remove) Token: 0x06001979 RID: 6521
		event HTMLLinkElementEvents_onrowsdeleteEventHandler onrowsdelete;

		// Token: 0x140001A6 RID: 422
		// (add) Token: 0x0600197A RID: 6522
		// (remove) Token: 0x0600197B RID: 6523
		event HTMLLinkElementEvents_onrowsinsertedEventHandler onrowsinserted;

		// Token: 0x140001A7 RID: 423
		// (add) Token: 0x0600197C RID: 6524
		// (remove) Token: 0x0600197D RID: 6525
		event HTMLLinkElementEvents_oncellchangeEventHandler oncellchange;

		// Token: 0x140001A8 RID: 424
		// (add) Token: 0x0600197E RID: 6526
		// (remove) Token: 0x0600197F RID: 6527
		event HTMLLinkElementEvents_onreadystatechangeEventHandler onreadystatechange;

		// Token: 0x140001A9 RID: 425
		// (add) Token: 0x06001980 RID: 6528
		// (remove) Token: 0x06001981 RID: 6529
		event HTMLLinkElementEvents_onbeforeeditfocusEventHandler onbeforeeditfocus;

		// Token: 0x140001AA RID: 426
		// (add) Token: 0x06001982 RID: 6530
		// (remove) Token: 0x06001983 RID: 6531
		event HTMLLinkElementEvents_onlayoutcompleteEventHandler onlayoutcomplete;

		// Token: 0x140001AB RID: 427
		// (add) Token: 0x06001984 RID: 6532
		// (remove) Token: 0x06001985 RID: 6533
		event HTMLLinkElementEvents_onpageEventHandler onpage;

		// Token: 0x140001AC RID: 428
		// (add) Token: 0x06001986 RID: 6534
		// (remove) Token: 0x06001987 RID: 6535
		event HTMLLinkElementEvents_onbeforedeactivateEventHandler onbeforedeactivate;

		// Token: 0x140001AD RID: 429
		// (add) Token: 0x06001988 RID: 6536
		// (remove) Token: 0x06001989 RID: 6537
		event HTMLLinkElementEvents_onbeforeactivateEventHandler onbeforeactivate;

		// Token: 0x140001AE RID: 430
		// (add) Token: 0x0600198A RID: 6538
		// (remove) Token: 0x0600198B RID: 6539
		event HTMLLinkElementEvents_onmoveEventHandler onmove;

		// Token: 0x140001AF RID: 431
		// (add) Token: 0x0600198C RID: 6540
		// (remove) Token: 0x0600198D RID: 6541
		event HTMLLinkElementEvents_oncontrolselectEventHandler oncontrolselect;

		// Token: 0x140001B0 RID: 432
		// (add) Token: 0x0600198E RID: 6542
		// (remove) Token: 0x0600198F RID: 6543
		event HTMLLinkElementEvents_onmovestartEventHandler onmovestart;

		// Token: 0x140001B1 RID: 433
		// (add) Token: 0x06001990 RID: 6544
		// (remove) Token: 0x06001991 RID: 6545
		event HTMLLinkElementEvents_onmoveendEventHandler onmoveend;

		// Token: 0x140001B2 RID: 434
		// (add) Token: 0x06001992 RID: 6546
		// (remove) Token: 0x06001993 RID: 6547
		event HTMLLinkElementEvents_onresizestartEventHandler onresizestart;

		// Token: 0x140001B3 RID: 435
		// (add) Token: 0x06001994 RID: 6548
		// (remove) Token: 0x06001995 RID: 6549
		event HTMLLinkElementEvents_onresizeendEventHandler onresizeend;

		// Token: 0x140001B4 RID: 436
		// (add) Token: 0x06001996 RID: 6550
		// (remove) Token: 0x06001997 RID: 6551
		event HTMLLinkElementEvents_onmouseenterEventHandler onmouseenter;

		// Token: 0x140001B5 RID: 437
		// (add) Token: 0x06001998 RID: 6552
		// (remove) Token: 0x06001999 RID: 6553
		event HTMLLinkElementEvents_onmouseleaveEventHandler onmouseleave;

		// Token: 0x140001B6 RID: 438
		// (add) Token: 0x0600199A RID: 6554
		// (remove) Token: 0x0600199B RID: 6555
		event HTMLLinkElementEvents_onmousewheelEventHandler onmousewheel;

		// Token: 0x140001B7 RID: 439
		// (add) Token: 0x0600199C RID: 6556
		// (remove) Token: 0x0600199D RID: 6557
		event HTMLLinkElementEvents_onactivateEventHandler onactivate;

		// Token: 0x140001B8 RID: 440
		// (add) Token: 0x0600199E RID: 6558
		// (remove) Token: 0x0600199F RID: 6559
		event HTMLLinkElementEvents_ondeactivateEventHandler ondeactivate;

		// Token: 0x140001B9 RID: 441
		// (add) Token: 0x060019A0 RID: 6560
		// (remove) Token: 0x060019A1 RID: 6561
		event HTMLLinkElementEvents_onfocusinEventHandler onfocusin;

		// Token: 0x140001BA RID: 442
		// (add) Token: 0x060019A2 RID: 6562
		// (remove) Token: 0x060019A3 RID: 6563
		event HTMLLinkElementEvents_onfocusoutEventHandler onfocusout;

		// Token: 0x140001BB RID: 443
		// (add) Token: 0x060019A4 RID: 6564
		// (remove) Token: 0x060019A5 RID: 6565
		event HTMLLinkElementEvents_onloadEventHandler onload;

		// Token: 0x140001BC RID: 444
		// (add) Token: 0x060019A6 RID: 6566
		// (remove) Token: 0x060019A7 RID: 6567
		event HTMLLinkElementEvents_onerrorEventHandler onerror;
	}
}
