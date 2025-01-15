using System;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x0200034F RID: 847
	[ComEventInterface(typeof(HTMLTextContainerEvents2\u0000), typeof(HTMLTextContainerEvents2_EventProvider\u0000))]
	[ComVisible(false)]
	public interface HTMLTextContainerEvents2_Event
	{
		// Token: 0x14000544 RID: 1348
		// (add) Token: 0x06003504 RID: 13572
		// (remove) Token: 0x06003505 RID: 13573
		event HTMLTextContainerEvents2_onhelpEventHandler onhelp;

		// Token: 0x14000545 RID: 1349
		// (add) Token: 0x06003506 RID: 13574
		// (remove) Token: 0x06003507 RID: 13575
		event HTMLTextContainerEvents2_onclickEventHandler onclick;

		// Token: 0x14000546 RID: 1350
		// (add) Token: 0x06003508 RID: 13576
		// (remove) Token: 0x06003509 RID: 13577
		event HTMLTextContainerEvents2_ondblclickEventHandler ondblclick;

		// Token: 0x14000547 RID: 1351
		// (add) Token: 0x0600350A RID: 13578
		// (remove) Token: 0x0600350B RID: 13579
		event HTMLTextContainerEvents2_onkeypressEventHandler onkeypress;

		// Token: 0x14000548 RID: 1352
		// (add) Token: 0x0600350C RID: 13580
		// (remove) Token: 0x0600350D RID: 13581
		event HTMLTextContainerEvents2_onkeydownEventHandler onkeydown;

		// Token: 0x14000549 RID: 1353
		// (add) Token: 0x0600350E RID: 13582
		// (remove) Token: 0x0600350F RID: 13583
		event HTMLTextContainerEvents2_onkeyupEventHandler onkeyup;

		// Token: 0x1400054A RID: 1354
		// (add) Token: 0x06003510 RID: 13584
		// (remove) Token: 0x06003511 RID: 13585
		event HTMLTextContainerEvents2_onmouseoutEventHandler onmouseout;

		// Token: 0x1400054B RID: 1355
		// (add) Token: 0x06003512 RID: 13586
		// (remove) Token: 0x06003513 RID: 13587
		event HTMLTextContainerEvents2_onmouseoverEventHandler onmouseover;

		// Token: 0x1400054C RID: 1356
		// (add) Token: 0x06003514 RID: 13588
		// (remove) Token: 0x06003515 RID: 13589
		event HTMLTextContainerEvents2_onmousemoveEventHandler onmousemove;

		// Token: 0x1400054D RID: 1357
		// (add) Token: 0x06003516 RID: 13590
		// (remove) Token: 0x06003517 RID: 13591
		event HTMLTextContainerEvents2_onmousedownEventHandler onmousedown;

		// Token: 0x1400054E RID: 1358
		// (add) Token: 0x06003518 RID: 13592
		// (remove) Token: 0x06003519 RID: 13593
		event HTMLTextContainerEvents2_onmouseupEventHandler onmouseup;

		// Token: 0x1400054F RID: 1359
		// (add) Token: 0x0600351A RID: 13594
		// (remove) Token: 0x0600351B RID: 13595
		event HTMLTextContainerEvents2_onselectstartEventHandler onselectstart;

		// Token: 0x14000550 RID: 1360
		// (add) Token: 0x0600351C RID: 13596
		// (remove) Token: 0x0600351D RID: 13597
		event HTMLTextContainerEvents2_onfilterchangeEventHandler onfilterchange;

		// Token: 0x14000551 RID: 1361
		// (add) Token: 0x0600351E RID: 13598
		// (remove) Token: 0x0600351F RID: 13599
		event HTMLTextContainerEvents2_ondragstartEventHandler ondragstart;

		// Token: 0x14000552 RID: 1362
		// (add) Token: 0x06003520 RID: 13600
		// (remove) Token: 0x06003521 RID: 13601
		event HTMLTextContainerEvents2_onbeforeupdateEventHandler onbeforeupdate;

		// Token: 0x14000553 RID: 1363
		// (add) Token: 0x06003522 RID: 13602
		// (remove) Token: 0x06003523 RID: 13603
		event HTMLTextContainerEvents2_onafterupdateEventHandler onafterupdate;

		// Token: 0x14000554 RID: 1364
		// (add) Token: 0x06003524 RID: 13604
		// (remove) Token: 0x06003525 RID: 13605
		event HTMLTextContainerEvents2_onerrorupdateEventHandler onerrorupdate;

		// Token: 0x14000555 RID: 1365
		// (add) Token: 0x06003526 RID: 13606
		// (remove) Token: 0x06003527 RID: 13607
		event HTMLTextContainerEvents2_onrowexitEventHandler onrowexit;

		// Token: 0x14000556 RID: 1366
		// (add) Token: 0x06003528 RID: 13608
		// (remove) Token: 0x06003529 RID: 13609
		event HTMLTextContainerEvents2_onrowenterEventHandler onrowenter;

		// Token: 0x14000557 RID: 1367
		// (add) Token: 0x0600352A RID: 13610
		// (remove) Token: 0x0600352B RID: 13611
		event HTMLTextContainerEvents2_ondatasetchangedEventHandler ondatasetchanged;

		// Token: 0x14000558 RID: 1368
		// (add) Token: 0x0600352C RID: 13612
		// (remove) Token: 0x0600352D RID: 13613
		event HTMLTextContainerEvents2_ondataavailableEventHandler ondataavailable;

		// Token: 0x14000559 RID: 1369
		// (add) Token: 0x0600352E RID: 13614
		// (remove) Token: 0x0600352F RID: 13615
		event HTMLTextContainerEvents2_ondatasetcompleteEventHandler ondatasetcomplete;

		// Token: 0x1400055A RID: 1370
		// (add) Token: 0x06003530 RID: 13616
		// (remove) Token: 0x06003531 RID: 13617
		event HTMLTextContainerEvents2_onlosecaptureEventHandler onlosecapture;

		// Token: 0x1400055B RID: 1371
		// (add) Token: 0x06003532 RID: 13618
		// (remove) Token: 0x06003533 RID: 13619
		event HTMLTextContainerEvents2_onpropertychangeEventHandler onpropertychange;

		// Token: 0x1400055C RID: 1372
		// (add) Token: 0x06003534 RID: 13620
		// (remove) Token: 0x06003535 RID: 13621
		event HTMLTextContainerEvents2_onscrollEventHandler onscroll;

		// Token: 0x1400055D RID: 1373
		// (add) Token: 0x06003536 RID: 13622
		// (remove) Token: 0x06003537 RID: 13623
		event HTMLTextContainerEvents2_onfocusEventHandler onfocus;

		// Token: 0x1400055E RID: 1374
		// (add) Token: 0x06003538 RID: 13624
		// (remove) Token: 0x06003539 RID: 13625
		event HTMLTextContainerEvents2_onblurEventHandler onblur;

		// Token: 0x1400055F RID: 1375
		// (add) Token: 0x0600353A RID: 13626
		// (remove) Token: 0x0600353B RID: 13627
		event HTMLTextContainerEvents2_onresizeEventHandler onresize;

		// Token: 0x14000560 RID: 1376
		// (add) Token: 0x0600353C RID: 13628
		// (remove) Token: 0x0600353D RID: 13629
		event HTMLTextContainerEvents2_ondragEventHandler ondrag;

		// Token: 0x14000561 RID: 1377
		// (add) Token: 0x0600353E RID: 13630
		// (remove) Token: 0x0600353F RID: 13631
		event HTMLTextContainerEvents2_ondragendEventHandler ondragend;

		// Token: 0x14000562 RID: 1378
		// (add) Token: 0x06003540 RID: 13632
		// (remove) Token: 0x06003541 RID: 13633
		event HTMLTextContainerEvents2_ondragenterEventHandler ondragenter;

		// Token: 0x14000563 RID: 1379
		// (add) Token: 0x06003542 RID: 13634
		// (remove) Token: 0x06003543 RID: 13635
		event HTMLTextContainerEvents2_ondragoverEventHandler ondragover;

		// Token: 0x14000564 RID: 1380
		// (add) Token: 0x06003544 RID: 13636
		// (remove) Token: 0x06003545 RID: 13637
		event HTMLTextContainerEvents2_ondragleaveEventHandler ondragleave;

		// Token: 0x14000565 RID: 1381
		// (add) Token: 0x06003546 RID: 13638
		// (remove) Token: 0x06003547 RID: 13639
		event HTMLTextContainerEvents2_ondropEventHandler ondrop;

		// Token: 0x14000566 RID: 1382
		// (add) Token: 0x06003548 RID: 13640
		// (remove) Token: 0x06003549 RID: 13641
		event HTMLTextContainerEvents2_onbeforecutEventHandler onbeforecut;

		// Token: 0x14000567 RID: 1383
		// (add) Token: 0x0600354A RID: 13642
		// (remove) Token: 0x0600354B RID: 13643
		event HTMLTextContainerEvents2_oncutEventHandler oncut;

		// Token: 0x14000568 RID: 1384
		// (add) Token: 0x0600354C RID: 13644
		// (remove) Token: 0x0600354D RID: 13645
		event HTMLTextContainerEvents2_onbeforecopyEventHandler onbeforecopy;

		// Token: 0x14000569 RID: 1385
		// (add) Token: 0x0600354E RID: 13646
		// (remove) Token: 0x0600354F RID: 13647
		event HTMLTextContainerEvents2_oncopyEventHandler oncopy;

		// Token: 0x1400056A RID: 1386
		// (add) Token: 0x06003550 RID: 13648
		// (remove) Token: 0x06003551 RID: 13649
		event HTMLTextContainerEvents2_onbeforepasteEventHandler onbeforepaste;

		// Token: 0x1400056B RID: 1387
		// (add) Token: 0x06003552 RID: 13650
		// (remove) Token: 0x06003553 RID: 13651
		event HTMLTextContainerEvents2_onpasteEventHandler onpaste;

		// Token: 0x1400056C RID: 1388
		// (add) Token: 0x06003554 RID: 13652
		// (remove) Token: 0x06003555 RID: 13653
		event HTMLTextContainerEvents2_oncontextmenuEventHandler oncontextmenu;

		// Token: 0x1400056D RID: 1389
		// (add) Token: 0x06003556 RID: 13654
		// (remove) Token: 0x06003557 RID: 13655
		event HTMLTextContainerEvents2_onrowsdeleteEventHandler onrowsdelete;

		// Token: 0x1400056E RID: 1390
		// (add) Token: 0x06003558 RID: 13656
		// (remove) Token: 0x06003559 RID: 13657
		event HTMLTextContainerEvents2_onrowsinsertedEventHandler onrowsinserted;

		// Token: 0x1400056F RID: 1391
		// (add) Token: 0x0600355A RID: 13658
		// (remove) Token: 0x0600355B RID: 13659
		event HTMLTextContainerEvents2_oncellchangeEventHandler oncellchange;

		// Token: 0x14000570 RID: 1392
		// (add) Token: 0x0600355C RID: 13660
		// (remove) Token: 0x0600355D RID: 13661
		event HTMLTextContainerEvents2_onreadystatechangeEventHandler onreadystatechange;

		// Token: 0x14000571 RID: 1393
		// (add) Token: 0x0600355E RID: 13662
		// (remove) Token: 0x0600355F RID: 13663
		event HTMLTextContainerEvents2_onlayoutcompleteEventHandler onlayoutcomplete;

		// Token: 0x14000572 RID: 1394
		// (add) Token: 0x06003560 RID: 13664
		// (remove) Token: 0x06003561 RID: 13665
		event HTMLTextContainerEvents2_onpageEventHandler onpage;

		// Token: 0x14000573 RID: 1395
		// (add) Token: 0x06003562 RID: 13666
		// (remove) Token: 0x06003563 RID: 13667
		event HTMLTextContainerEvents2_onmouseenterEventHandler onmouseenter;

		// Token: 0x14000574 RID: 1396
		// (add) Token: 0x06003564 RID: 13668
		// (remove) Token: 0x06003565 RID: 13669
		event HTMLTextContainerEvents2_onmouseleaveEventHandler onmouseleave;

		// Token: 0x14000575 RID: 1397
		// (add) Token: 0x06003566 RID: 13670
		// (remove) Token: 0x06003567 RID: 13671
		event HTMLTextContainerEvents2_onactivateEventHandler onactivate;

		// Token: 0x14000576 RID: 1398
		// (add) Token: 0x06003568 RID: 13672
		// (remove) Token: 0x06003569 RID: 13673
		event HTMLTextContainerEvents2_ondeactivateEventHandler ondeactivate;

		// Token: 0x14000577 RID: 1399
		// (add) Token: 0x0600356A RID: 13674
		// (remove) Token: 0x0600356B RID: 13675
		event HTMLTextContainerEvents2_onbeforedeactivateEventHandler onbeforedeactivate;

		// Token: 0x14000578 RID: 1400
		// (add) Token: 0x0600356C RID: 13676
		// (remove) Token: 0x0600356D RID: 13677
		event HTMLTextContainerEvents2_onbeforeactivateEventHandler onbeforeactivate;

		// Token: 0x14000579 RID: 1401
		// (add) Token: 0x0600356E RID: 13678
		// (remove) Token: 0x0600356F RID: 13679
		event HTMLTextContainerEvents2_onfocusinEventHandler onfocusin;

		// Token: 0x1400057A RID: 1402
		// (add) Token: 0x06003570 RID: 13680
		// (remove) Token: 0x06003571 RID: 13681
		event HTMLTextContainerEvents2_onfocusoutEventHandler onfocusout;

		// Token: 0x1400057B RID: 1403
		// (add) Token: 0x06003572 RID: 13682
		// (remove) Token: 0x06003573 RID: 13683
		event HTMLTextContainerEvents2_onmoveEventHandler onmove;

		// Token: 0x1400057C RID: 1404
		// (add) Token: 0x06003574 RID: 13684
		// (remove) Token: 0x06003575 RID: 13685
		event HTMLTextContainerEvents2_oncontrolselectEventHandler oncontrolselect;

		// Token: 0x1400057D RID: 1405
		// (add) Token: 0x06003576 RID: 13686
		// (remove) Token: 0x06003577 RID: 13687
		event HTMLTextContainerEvents2_onmovestartEventHandler onmovestart;

		// Token: 0x1400057E RID: 1406
		// (add) Token: 0x06003578 RID: 13688
		// (remove) Token: 0x06003579 RID: 13689
		event HTMLTextContainerEvents2_onmoveendEventHandler onmoveend;

		// Token: 0x1400057F RID: 1407
		// (add) Token: 0x0600357A RID: 13690
		// (remove) Token: 0x0600357B RID: 13691
		event HTMLTextContainerEvents2_onresizestartEventHandler onresizestart;

		// Token: 0x14000580 RID: 1408
		// (add) Token: 0x0600357C RID: 13692
		// (remove) Token: 0x0600357D RID: 13693
		event HTMLTextContainerEvents2_onresizeendEventHandler onresizeend;

		// Token: 0x14000581 RID: 1409
		// (add) Token: 0x0600357E RID: 13694
		// (remove) Token: 0x0600357F RID: 13695
		event HTMLTextContainerEvents2_onmousewheelEventHandler onmousewheel;

		// Token: 0x14000582 RID: 1410
		// (add) Token: 0x06003580 RID: 13696
		// (remove) Token: 0x06003581 RID: 13697
		event HTMLTextContainerEvents2_onchangeEventHandler onchange;

		// Token: 0x14000583 RID: 1411
		// (add) Token: 0x06003582 RID: 13698
		// (remove) Token: 0x06003583 RID: 13699
		event HTMLTextContainerEvents2_onselectEventHandler onselect;
	}
}
