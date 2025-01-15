using System;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000B4B RID: 2891
	[ComVisible(false)]
	[ComEventInterface(typeof(HTMLFrameSiteEvents\u0000), typeof(HTMLFrameSiteEvents_EventProvider\u0000))]
	public interface HTMLFrameSiteEvents_Event
	{
		// Token: 0x1400258D RID: 9613
		// (add) Token: 0x060135EE RID: 79342
		// (remove) Token: 0x060135EF RID: 79343
		event HTMLFrameSiteEvents_onhelpEventHandler onhelp;

		// Token: 0x1400258E RID: 9614
		// (add) Token: 0x060135F0 RID: 79344
		// (remove) Token: 0x060135F1 RID: 79345
		event HTMLFrameSiteEvents_onclickEventHandler onclick;

		// Token: 0x1400258F RID: 9615
		// (add) Token: 0x060135F2 RID: 79346
		// (remove) Token: 0x060135F3 RID: 79347
		event HTMLFrameSiteEvents_ondblclickEventHandler ondblclick;

		// Token: 0x14002590 RID: 9616
		// (add) Token: 0x060135F4 RID: 79348
		// (remove) Token: 0x060135F5 RID: 79349
		event HTMLFrameSiteEvents_onkeypressEventHandler onkeypress;

		// Token: 0x14002591 RID: 9617
		// (add) Token: 0x060135F6 RID: 79350
		// (remove) Token: 0x060135F7 RID: 79351
		event HTMLFrameSiteEvents_onkeydownEventHandler onkeydown;

		// Token: 0x14002592 RID: 9618
		// (add) Token: 0x060135F8 RID: 79352
		// (remove) Token: 0x060135F9 RID: 79353
		event HTMLFrameSiteEvents_onkeyupEventHandler onkeyup;

		// Token: 0x14002593 RID: 9619
		// (add) Token: 0x060135FA RID: 79354
		// (remove) Token: 0x060135FB RID: 79355
		event HTMLFrameSiteEvents_onmouseoutEventHandler onmouseout;

		// Token: 0x14002594 RID: 9620
		// (add) Token: 0x060135FC RID: 79356
		// (remove) Token: 0x060135FD RID: 79357
		event HTMLFrameSiteEvents_onmouseoverEventHandler onmouseover;

		// Token: 0x14002595 RID: 9621
		// (add) Token: 0x060135FE RID: 79358
		// (remove) Token: 0x060135FF RID: 79359
		event HTMLFrameSiteEvents_onmousemoveEventHandler onmousemove;

		// Token: 0x14002596 RID: 9622
		// (add) Token: 0x06013600 RID: 79360
		// (remove) Token: 0x06013601 RID: 79361
		event HTMLFrameSiteEvents_onmousedownEventHandler onmousedown;

		// Token: 0x14002597 RID: 9623
		// (add) Token: 0x06013602 RID: 79362
		// (remove) Token: 0x06013603 RID: 79363
		event HTMLFrameSiteEvents_onmouseupEventHandler onmouseup;

		// Token: 0x14002598 RID: 9624
		// (add) Token: 0x06013604 RID: 79364
		// (remove) Token: 0x06013605 RID: 79365
		event HTMLFrameSiteEvents_onselectstartEventHandler onselectstart;

		// Token: 0x14002599 RID: 9625
		// (add) Token: 0x06013606 RID: 79366
		// (remove) Token: 0x06013607 RID: 79367
		event HTMLFrameSiteEvents_onfilterchangeEventHandler onfilterchange;

		// Token: 0x1400259A RID: 9626
		// (add) Token: 0x06013608 RID: 79368
		// (remove) Token: 0x06013609 RID: 79369
		event HTMLFrameSiteEvents_ondragstartEventHandler ondragstart;

		// Token: 0x1400259B RID: 9627
		// (add) Token: 0x0601360A RID: 79370
		// (remove) Token: 0x0601360B RID: 79371
		event HTMLFrameSiteEvents_onbeforeupdateEventHandler onbeforeupdate;

		// Token: 0x1400259C RID: 9628
		// (add) Token: 0x0601360C RID: 79372
		// (remove) Token: 0x0601360D RID: 79373
		event HTMLFrameSiteEvents_onafterupdateEventHandler onafterupdate;

		// Token: 0x1400259D RID: 9629
		// (add) Token: 0x0601360E RID: 79374
		// (remove) Token: 0x0601360F RID: 79375
		event HTMLFrameSiteEvents_onerrorupdateEventHandler onerrorupdate;

		// Token: 0x1400259E RID: 9630
		// (add) Token: 0x06013610 RID: 79376
		// (remove) Token: 0x06013611 RID: 79377
		event HTMLFrameSiteEvents_onrowexitEventHandler onrowexit;

		// Token: 0x1400259F RID: 9631
		// (add) Token: 0x06013612 RID: 79378
		// (remove) Token: 0x06013613 RID: 79379
		event HTMLFrameSiteEvents_onrowenterEventHandler onrowenter;

		// Token: 0x140025A0 RID: 9632
		// (add) Token: 0x06013614 RID: 79380
		// (remove) Token: 0x06013615 RID: 79381
		event HTMLFrameSiteEvents_ondatasetchangedEventHandler ondatasetchanged;

		// Token: 0x140025A1 RID: 9633
		// (add) Token: 0x06013616 RID: 79382
		// (remove) Token: 0x06013617 RID: 79383
		event HTMLFrameSiteEvents_ondataavailableEventHandler ondataavailable;

		// Token: 0x140025A2 RID: 9634
		// (add) Token: 0x06013618 RID: 79384
		// (remove) Token: 0x06013619 RID: 79385
		event HTMLFrameSiteEvents_ondatasetcompleteEventHandler ondatasetcomplete;

		// Token: 0x140025A3 RID: 9635
		// (add) Token: 0x0601361A RID: 79386
		// (remove) Token: 0x0601361B RID: 79387
		event HTMLFrameSiteEvents_onlosecaptureEventHandler onlosecapture;

		// Token: 0x140025A4 RID: 9636
		// (add) Token: 0x0601361C RID: 79388
		// (remove) Token: 0x0601361D RID: 79389
		event HTMLFrameSiteEvents_onpropertychangeEventHandler onpropertychange;

		// Token: 0x140025A5 RID: 9637
		// (add) Token: 0x0601361E RID: 79390
		// (remove) Token: 0x0601361F RID: 79391
		event HTMLFrameSiteEvents_onscrollEventHandler onscroll;

		// Token: 0x140025A6 RID: 9638
		// (add) Token: 0x06013620 RID: 79392
		// (remove) Token: 0x06013621 RID: 79393
		event HTMLFrameSiteEvents_onfocusEventHandler onfocus;

		// Token: 0x140025A7 RID: 9639
		// (add) Token: 0x06013622 RID: 79394
		// (remove) Token: 0x06013623 RID: 79395
		event HTMLFrameSiteEvents_onblurEventHandler onblur;

		// Token: 0x140025A8 RID: 9640
		// (add) Token: 0x06013624 RID: 79396
		// (remove) Token: 0x06013625 RID: 79397
		event HTMLFrameSiteEvents_onresizeEventHandler onresize;

		// Token: 0x140025A9 RID: 9641
		// (add) Token: 0x06013626 RID: 79398
		// (remove) Token: 0x06013627 RID: 79399
		event HTMLFrameSiteEvents_ondragEventHandler ondrag;

		// Token: 0x140025AA RID: 9642
		// (add) Token: 0x06013628 RID: 79400
		// (remove) Token: 0x06013629 RID: 79401
		event HTMLFrameSiteEvents_ondragendEventHandler ondragend;

		// Token: 0x140025AB RID: 9643
		// (add) Token: 0x0601362A RID: 79402
		// (remove) Token: 0x0601362B RID: 79403
		event HTMLFrameSiteEvents_ondragenterEventHandler ondragenter;

		// Token: 0x140025AC RID: 9644
		// (add) Token: 0x0601362C RID: 79404
		// (remove) Token: 0x0601362D RID: 79405
		event HTMLFrameSiteEvents_ondragoverEventHandler ondragover;

		// Token: 0x140025AD RID: 9645
		// (add) Token: 0x0601362E RID: 79406
		// (remove) Token: 0x0601362F RID: 79407
		event HTMLFrameSiteEvents_ondragleaveEventHandler ondragleave;

		// Token: 0x140025AE RID: 9646
		// (add) Token: 0x06013630 RID: 79408
		// (remove) Token: 0x06013631 RID: 79409
		event HTMLFrameSiteEvents_ondropEventHandler ondrop;

		// Token: 0x140025AF RID: 9647
		// (add) Token: 0x06013632 RID: 79410
		// (remove) Token: 0x06013633 RID: 79411
		event HTMLFrameSiteEvents_onbeforecutEventHandler onbeforecut;

		// Token: 0x140025B0 RID: 9648
		// (add) Token: 0x06013634 RID: 79412
		// (remove) Token: 0x06013635 RID: 79413
		event HTMLFrameSiteEvents_oncutEventHandler oncut;

		// Token: 0x140025B1 RID: 9649
		// (add) Token: 0x06013636 RID: 79414
		// (remove) Token: 0x06013637 RID: 79415
		event HTMLFrameSiteEvents_onbeforecopyEventHandler onbeforecopy;

		// Token: 0x140025B2 RID: 9650
		// (add) Token: 0x06013638 RID: 79416
		// (remove) Token: 0x06013639 RID: 79417
		event HTMLFrameSiteEvents_oncopyEventHandler oncopy;

		// Token: 0x140025B3 RID: 9651
		// (add) Token: 0x0601363A RID: 79418
		// (remove) Token: 0x0601363B RID: 79419
		event HTMLFrameSiteEvents_onbeforepasteEventHandler onbeforepaste;

		// Token: 0x140025B4 RID: 9652
		// (add) Token: 0x0601363C RID: 79420
		// (remove) Token: 0x0601363D RID: 79421
		event HTMLFrameSiteEvents_onpasteEventHandler onpaste;

		// Token: 0x140025B5 RID: 9653
		// (add) Token: 0x0601363E RID: 79422
		// (remove) Token: 0x0601363F RID: 79423
		event HTMLFrameSiteEvents_oncontextmenuEventHandler oncontextmenu;

		// Token: 0x140025B6 RID: 9654
		// (add) Token: 0x06013640 RID: 79424
		// (remove) Token: 0x06013641 RID: 79425
		event HTMLFrameSiteEvents_onrowsdeleteEventHandler onrowsdelete;

		// Token: 0x140025B7 RID: 9655
		// (add) Token: 0x06013642 RID: 79426
		// (remove) Token: 0x06013643 RID: 79427
		event HTMLFrameSiteEvents_onrowsinsertedEventHandler onrowsinserted;

		// Token: 0x140025B8 RID: 9656
		// (add) Token: 0x06013644 RID: 79428
		// (remove) Token: 0x06013645 RID: 79429
		event HTMLFrameSiteEvents_oncellchangeEventHandler oncellchange;

		// Token: 0x140025B9 RID: 9657
		// (add) Token: 0x06013646 RID: 79430
		// (remove) Token: 0x06013647 RID: 79431
		event HTMLFrameSiteEvents_onreadystatechangeEventHandler onreadystatechange;

		// Token: 0x140025BA RID: 9658
		// (add) Token: 0x06013648 RID: 79432
		// (remove) Token: 0x06013649 RID: 79433
		event HTMLFrameSiteEvents_onbeforeeditfocusEventHandler onbeforeeditfocus;

		// Token: 0x140025BB RID: 9659
		// (add) Token: 0x0601364A RID: 79434
		// (remove) Token: 0x0601364B RID: 79435
		event HTMLFrameSiteEvents_onlayoutcompleteEventHandler onlayoutcomplete;

		// Token: 0x140025BC RID: 9660
		// (add) Token: 0x0601364C RID: 79436
		// (remove) Token: 0x0601364D RID: 79437
		event HTMLFrameSiteEvents_onpageEventHandler onpage;

		// Token: 0x140025BD RID: 9661
		// (add) Token: 0x0601364E RID: 79438
		// (remove) Token: 0x0601364F RID: 79439
		event HTMLFrameSiteEvents_onbeforedeactivateEventHandler onbeforedeactivate;

		// Token: 0x140025BE RID: 9662
		// (add) Token: 0x06013650 RID: 79440
		// (remove) Token: 0x06013651 RID: 79441
		event HTMLFrameSiteEvents_onbeforeactivateEventHandler onbeforeactivate;

		// Token: 0x140025BF RID: 9663
		// (add) Token: 0x06013652 RID: 79442
		// (remove) Token: 0x06013653 RID: 79443
		event HTMLFrameSiteEvents_onmoveEventHandler onmove;

		// Token: 0x140025C0 RID: 9664
		// (add) Token: 0x06013654 RID: 79444
		// (remove) Token: 0x06013655 RID: 79445
		event HTMLFrameSiteEvents_oncontrolselectEventHandler oncontrolselect;

		// Token: 0x140025C1 RID: 9665
		// (add) Token: 0x06013656 RID: 79446
		// (remove) Token: 0x06013657 RID: 79447
		event HTMLFrameSiteEvents_onmovestartEventHandler onmovestart;

		// Token: 0x140025C2 RID: 9666
		// (add) Token: 0x06013658 RID: 79448
		// (remove) Token: 0x06013659 RID: 79449
		event HTMLFrameSiteEvents_onmoveendEventHandler onmoveend;

		// Token: 0x140025C3 RID: 9667
		// (add) Token: 0x0601365A RID: 79450
		// (remove) Token: 0x0601365B RID: 79451
		event HTMLFrameSiteEvents_onresizestartEventHandler onresizestart;

		// Token: 0x140025C4 RID: 9668
		// (add) Token: 0x0601365C RID: 79452
		// (remove) Token: 0x0601365D RID: 79453
		event HTMLFrameSiteEvents_onresizeendEventHandler onresizeend;

		// Token: 0x140025C5 RID: 9669
		// (add) Token: 0x0601365E RID: 79454
		// (remove) Token: 0x0601365F RID: 79455
		event HTMLFrameSiteEvents_onmouseenterEventHandler onmouseenter;

		// Token: 0x140025C6 RID: 9670
		// (add) Token: 0x06013660 RID: 79456
		// (remove) Token: 0x06013661 RID: 79457
		event HTMLFrameSiteEvents_onmouseleaveEventHandler onmouseleave;

		// Token: 0x140025C7 RID: 9671
		// (add) Token: 0x06013662 RID: 79458
		// (remove) Token: 0x06013663 RID: 79459
		event HTMLFrameSiteEvents_onmousewheelEventHandler onmousewheel;

		// Token: 0x140025C8 RID: 9672
		// (add) Token: 0x06013664 RID: 79460
		// (remove) Token: 0x06013665 RID: 79461
		event HTMLFrameSiteEvents_onactivateEventHandler onactivate;

		// Token: 0x140025C9 RID: 9673
		// (add) Token: 0x06013666 RID: 79462
		// (remove) Token: 0x06013667 RID: 79463
		event HTMLFrameSiteEvents_ondeactivateEventHandler ondeactivate;

		// Token: 0x140025CA RID: 9674
		// (add) Token: 0x06013668 RID: 79464
		// (remove) Token: 0x06013669 RID: 79465
		event HTMLFrameSiteEvents_onfocusinEventHandler onfocusin;

		// Token: 0x140025CB RID: 9675
		// (add) Token: 0x0601366A RID: 79466
		// (remove) Token: 0x0601366B RID: 79467
		event HTMLFrameSiteEvents_onfocusoutEventHandler onfocusout;

		// Token: 0x140025CC RID: 9676
		// (add) Token: 0x0601366C RID: 79468
		// (remove) Token: 0x0601366D RID: 79469
		event HTMLFrameSiteEvents_onloadEventHandler onload;
	}
}
