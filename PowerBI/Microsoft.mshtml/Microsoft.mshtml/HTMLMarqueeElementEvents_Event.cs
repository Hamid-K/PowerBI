using System;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x020006E5 RID: 1765
	[ComEventInterface(typeof(HTMLMarqueeElementEvents\u0000), typeof(HTMLMarqueeElementEvents_EventProvider\u0000))]
	[ComVisible(false)]
	public interface HTMLMarqueeElementEvents_Event
	{
		// Token: 0x1400152F RID: 5423
		// (add) Token: 0x0600AD5A RID: 44378
		// (remove) Token: 0x0600AD5B RID: 44379
		event HTMLMarqueeElementEvents_onhelpEventHandler onhelp;

		// Token: 0x14001530 RID: 5424
		// (add) Token: 0x0600AD5C RID: 44380
		// (remove) Token: 0x0600AD5D RID: 44381
		event HTMLMarqueeElementEvents_onclickEventHandler onclick;

		// Token: 0x14001531 RID: 5425
		// (add) Token: 0x0600AD5E RID: 44382
		// (remove) Token: 0x0600AD5F RID: 44383
		event HTMLMarqueeElementEvents_ondblclickEventHandler ondblclick;

		// Token: 0x14001532 RID: 5426
		// (add) Token: 0x0600AD60 RID: 44384
		// (remove) Token: 0x0600AD61 RID: 44385
		event HTMLMarqueeElementEvents_onkeypressEventHandler onkeypress;

		// Token: 0x14001533 RID: 5427
		// (add) Token: 0x0600AD62 RID: 44386
		// (remove) Token: 0x0600AD63 RID: 44387
		event HTMLMarqueeElementEvents_onkeydownEventHandler onkeydown;

		// Token: 0x14001534 RID: 5428
		// (add) Token: 0x0600AD64 RID: 44388
		// (remove) Token: 0x0600AD65 RID: 44389
		event HTMLMarqueeElementEvents_onkeyupEventHandler onkeyup;

		// Token: 0x14001535 RID: 5429
		// (add) Token: 0x0600AD66 RID: 44390
		// (remove) Token: 0x0600AD67 RID: 44391
		event HTMLMarqueeElementEvents_onmouseoutEventHandler onmouseout;

		// Token: 0x14001536 RID: 5430
		// (add) Token: 0x0600AD68 RID: 44392
		// (remove) Token: 0x0600AD69 RID: 44393
		event HTMLMarqueeElementEvents_onmouseoverEventHandler onmouseover;

		// Token: 0x14001537 RID: 5431
		// (add) Token: 0x0600AD6A RID: 44394
		// (remove) Token: 0x0600AD6B RID: 44395
		event HTMLMarqueeElementEvents_onmousemoveEventHandler onmousemove;

		// Token: 0x14001538 RID: 5432
		// (add) Token: 0x0600AD6C RID: 44396
		// (remove) Token: 0x0600AD6D RID: 44397
		event HTMLMarqueeElementEvents_onmousedownEventHandler onmousedown;

		// Token: 0x14001539 RID: 5433
		// (add) Token: 0x0600AD6E RID: 44398
		// (remove) Token: 0x0600AD6F RID: 44399
		event HTMLMarqueeElementEvents_onmouseupEventHandler onmouseup;

		// Token: 0x1400153A RID: 5434
		// (add) Token: 0x0600AD70 RID: 44400
		// (remove) Token: 0x0600AD71 RID: 44401
		event HTMLMarqueeElementEvents_onselectstartEventHandler onselectstart;

		// Token: 0x1400153B RID: 5435
		// (add) Token: 0x0600AD72 RID: 44402
		// (remove) Token: 0x0600AD73 RID: 44403
		event HTMLMarqueeElementEvents_onfilterchangeEventHandler onfilterchange;

		// Token: 0x1400153C RID: 5436
		// (add) Token: 0x0600AD74 RID: 44404
		// (remove) Token: 0x0600AD75 RID: 44405
		event HTMLMarqueeElementEvents_ondragstartEventHandler ondragstart;

		// Token: 0x1400153D RID: 5437
		// (add) Token: 0x0600AD76 RID: 44406
		// (remove) Token: 0x0600AD77 RID: 44407
		event HTMLMarqueeElementEvents_onbeforeupdateEventHandler onbeforeupdate;

		// Token: 0x1400153E RID: 5438
		// (add) Token: 0x0600AD78 RID: 44408
		// (remove) Token: 0x0600AD79 RID: 44409
		event HTMLMarqueeElementEvents_onafterupdateEventHandler onafterupdate;

		// Token: 0x1400153F RID: 5439
		// (add) Token: 0x0600AD7A RID: 44410
		// (remove) Token: 0x0600AD7B RID: 44411
		event HTMLMarqueeElementEvents_onerrorupdateEventHandler onerrorupdate;

		// Token: 0x14001540 RID: 5440
		// (add) Token: 0x0600AD7C RID: 44412
		// (remove) Token: 0x0600AD7D RID: 44413
		event HTMLMarqueeElementEvents_onrowexitEventHandler onrowexit;

		// Token: 0x14001541 RID: 5441
		// (add) Token: 0x0600AD7E RID: 44414
		// (remove) Token: 0x0600AD7F RID: 44415
		event HTMLMarqueeElementEvents_onrowenterEventHandler onrowenter;

		// Token: 0x14001542 RID: 5442
		// (add) Token: 0x0600AD80 RID: 44416
		// (remove) Token: 0x0600AD81 RID: 44417
		event HTMLMarqueeElementEvents_ondatasetchangedEventHandler ondatasetchanged;

		// Token: 0x14001543 RID: 5443
		// (add) Token: 0x0600AD82 RID: 44418
		// (remove) Token: 0x0600AD83 RID: 44419
		event HTMLMarqueeElementEvents_ondataavailableEventHandler ondataavailable;

		// Token: 0x14001544 RID: 5444
		// (add) Token: 0x0600AD84 RID: 44420
		// (remove) Token: 0x0600AD85 RID: 44421
		event HTMLMarqueeElementEvents_ondatasetcompleteEventHandler ondatasetcomplete;

		// Token: 0x14001545 RID: 5445
		// (add) Token: 0x0600AD86 RID: 44422
		// (remove) Token: 0x0600AD87 RID: 44423
		event HTMLMarqueeElementEvents_onlosecaptureEventHandler onlosecapture;

		// Token: 0x14001546 RID: 5446
		// (add) Token: 0x0600AD88 RID: 44424
		// (remove) Token: 0x0600AD89 RID: 44425
		event HTMLMarqueeElementEvents_onpropertychangeEventHandler onpropertychange;

		// Token: 0x14001547 RID: 5447
		// (add) Token: 0x0600AD8A RID: 44426
		// (remove) Token: 0x0600AD8B RID: 44427
		event HTMLMarqueeElementEvents_onscrollEventHandler onscroll;

		// Token: 0x14001548 RID: 5448
		// (add) Token: 0x0600AD8C RID: 44428
		// (remove) Token: 0x0600AD8D RID: 44429
		event HTMLMarqueeElementEvents_onfocusEventHandler onfocus;

		// Token: 0x14001549 RID: 5449
		// (add) Token: 0x0600AD8E RID: 44430
		// (remove) Token: 0x0600AD8F RID: 44431
		event HTMLMarqueeElementEvents_onblurEventHandler onblur;

		// Token: 0x1400154A RID: 5450
		// (add) Token: 0x0600AD90 RID: 44432
		// (remove) Token: 0x0600AD91 RID: 44433
		event HTMLMarqueeElementEvents_onresizeEventHandler onresize;

		// Token: 0x1400154B RID: 5451
		// (add) Token: 0x0600AD92 RID: 44434
		// (remove) Token: 0x0600AD93 RID: 44435
		event HTMLMarqueeElementEvents_ondragEventHandler ondrag;

		// Token: 0x1400154C RID: 5452
		// (add) Token: 0x0600AD94 RID: 44436
		// (remove) Token: 0x0600AD95 RID: 44437
		event HTMLMarqueeElementEvents_ondragendEventHandler ondragend;

		// Token: 0x1400154D RID: 5453
		// (add) Token: 0x0600AD96 RID: 44438
		// (remove) Token: 0x0600AD97 RID: 44439
		event HTMLMarqueeElementEvents_ondragenterEventHandler ondragenter;

		// Token: 0x1400154E RID: 5454
		// (add) Token: 0x0600AD98 RID: 44440
		// (remove) Token: 0x0600AD99 RID: 44441
		event HTMLMarqueeElementEvents_ondragoverEventHandler ondragover;

		// Token: 0x1400154F RID: 5455
		// (add) Token: 0x0600AD9A RID: 44442
		// (remove) Token: 0x0600AD9B RID: 44443
		event HTMLMarqueeElementEvents_ondragleaveEventHandler ondragleave;

		// Token: 0x14001550 RID: 5456
		// (add) Token: 0x0600AD9C RID: 44444
		// (remove) Token: 0x0600AD9D RID: 44445
		event HTMLMarqueeElementEvents_ondropEventHandler ondrop;

		// Token: 0x14001551 RID: 5457
		// (add) Token: 0x0600AD9E RID: 44446
		// (remove) Token: 0x0600AD9F RID: 44447
		event HTMLMarqueeElementEvents_onbeforecutEventHandler onbeforecut;

		// Token: 0x14001552 RID: 5458
		// (add) Token: 0x0600ADA0 RID: 44448
		// (remove) Token: 0x0600ADA1 RID: 44449
		event HTMLMarqueeElementEvents_oncutEventHandler oncut;

		// Token: 0x14001553 RID: 5459
		// (add) Token: 0x0600ADA2 RID: 44450
		// (remove) Token: 0x0600ADA3 RID: 44451
		event HTMLMarqueeElementEvents_onbeforecopyEventHandler onbeforecopy;

		// Token: 0x14001554 RID: 5460
		// (add) Token: 0x0600ADA4 RID: 44452
		// (remove) Token: 0x0600ADA5 RID: 44453
		event HTMLMarqueeElementEvents_oncopyEventHandler oncopy;

		// Token: 0x14001555 RID: 5461
		// (add) Token: 0x0600ADA6 RID: 44454
		// (remove) Token: 0x0600ADA7 RID: 44455
		event HTMLMarqueeElementEvents_onbeforepasteEventHandler onbeforepaste;

		// Token: 0x14001556 RID: 5462
		// (add) Token: 0x0600ADA8 RID: 44456
		// (remove) Token: 0x0600ADA9 RID: 44457
		event HTMLMarqueeElementEvents_onpasteEventHandler onpaste;

		// Token: 0x14001557 RID: 5463
		// (add) Token: 0x0600ADAA RID: 44458
		// (remove) Token: 0x0600ADAB RID: 44459
		event HTMLMarqueeElementEvents_oncontextmenuEventHandler oncontextmenu;

		// Token: 0x14001558 RID: 5464
		// (add) Token: 0x0600ADAC RID: 44460
		// (remove) Token: 0x0600ADAD RID: 44461
		event HTMLMarqueeElementEvents_onrowsdeleteEventHandler onrowsdelete;

		// Token: 0x14001559 RID: 5465
		// (add) Token: 0x0600ADAE RID: 44462
		// (remove) Token: 0x0600ADAF RID: 44463
		event HTMLMarqueeElementEvents_onrowsinsertedEventHandler onrowsinserted;

		// Token: 0x1400155A RID: 5466
		// (add) Token: 0x0600ADB0 RID: 44464
		// (remove) Token: 0x0600ADB1 RID: 44465
		event HTMLMarqueeElementEvents_oncellchangeEventHandler oncellchange;

		// Token: 0x1400155B RID: 5467
		// (add) Token: 0x0600ADB2 RID: 44466
		// (remove) Token: 0x0600ADB3 RID: 44467
		event HTMLMarqueeElementEvents_onreadystatechangeEventHandler onreadystatechange;

		// Token: 0x1400155C RID: 5468
		// (add) Token: 0x0600ADB4 RID: 44468
		// (remove) Token: 0x0600ADB5 RID: 44469
		event HTMLMarqueeElementEvents_onbeforeeditfocusEventHandler onbeforeeditfocus;

		// Token: 0x1400155D RID: 5469
		// (add) Token: 0x0600ADB6 RID: 44470
		// (remove) Token: 0x0600ADB7 RID: 44471
		event HTMLMarqueeElementEvents_onlayoutcompleteEventHandler onlayoutcomplete;

		// Token: 0x1400155E RID: 5470
		// (add) Token: 0x0600ADB8 RID: 44472
		// (remove) Token: 0x0600ADB9 RID: 44473
		event HTMLMarqueeElementEvents_onpageEventHandler onpage;

		// Token: 0x1400155F RID: 5471
		// (add) Token: 0x0600ADBA RID: 44474
		// (remove) Token: 0x0600ADBB RID: 44475
		event HTMLMarqueeElementEvents_onbeforedeactivateEventHandler onbeforedeactivate;

		// Token: 0x14001560 RID: 5472
		// (add) Token: 0x0600ADBC RID: 44476
		// (remove) Token: 0x0600ADBD RID: 44477
		event HTMLMarqueeElementEvents_onbeforeactivateEventHandler onbeforeactivate;

		// Token: 0x14001561 RID: 5473
		// (add) Token: 0x0600ADBE RID: 44478
		// (remove) Token: 0x0600ADBF RID: 44479
		event HTMLMarqueeElementEvents_onmoveEventHandler onmove;

		// Token: 0x14001562 RID: 5474
		// (add) Token: 0x0600ADC0 RID: 44480
		// (remove) Token: 0x0600ADC1 RID: 44481
		event HTMLMarqueeElementEvents_oncontrolselectEventHandler oncontrolselect;

		// Token: 0x14001563 RID: 5475
		// (add) Token: 0x0600ADC2 RID: 44482
		// (remove) Token: 0x0600ADC3 RID: 44483
		event HTMLMarqueeElementEvents_onmovestartEventHandler onmovestart;

		// Token: 0x14001564 RID: 5476
		// (add) Token: 0x0600ADC4 RID: 44484
		// (remove) Token: 0x0600ADC5 RID: 44485
		event HTMLMarqueeElementEvents_onmoveendEventHandler onmoveend;

		// Token: 0x14001565 RID: 5477
		// (add) Token: 0x0600ADC6 RID: 44486
		// (remove) Token: 0x0600ADC7 RID: 44487
		event HTMLMarqueeElementEvents_onresizestartEventHandler onresizestart;

		// Token: 0x14001566 RID: 5478
		// (add) Token: 0x0600ADC8 RID: 44488
		// (remove) Token: 0x0600ADC9 RID: 44489
		event HTMLMarqueeElementEvents_onresizeendEventHandler onresizeend;

		// Token: 0x14001567 RID: 5479
		// (add) Token: 0x0600ADCA RID: 44490
		// (remove) Token: 0x0600ADCB RID: 44491
		event HTMLMarqueeElementEvents_onmouseenterEventHandler onmouseenter;

		// Token: 0x14001568 RID: 5480
		// (add) Token: 0x0600ADCC RID: 44492
		// (remove) Token: 0x0600ADCD RID: 44493
		event HTMLMarqueeElementEvents_onmouseleaveEventHandler onmouseleave;

		// Token: 0x14001569 RID: 5481
		// (add) Token: 0x0600ADCE RID: 44494
		// (remove) Token: 0x0600ADCF RID: 44495
		event HTMLMarqueeElementEvents_onmousewheelEventHandler onmousewheel;

		// Token: 0x1400156A RID: 5482
		// (add) Token: 0x0600ADD0 RID: 44496
		// (remove) Token: 0x0600ADD1 RID: 44497
		event HTMLMarqueeElementEvents_onactivateEventHandler onactivate;

		// Token: 0x1400156B RID: 5483
		// (add) Token: 0x0600ADD2 RID: 44498
		// (remove) Token: 0x0600ADD3 RID: 44499
		event HTMLMarqueeElementEvents_ondeactivateEventHandler ondeactivate;

		// Token: 0x1400156C RID: 5484
		// (add) Token: 0x0600ADD4 RID: 44500
		// (remove) Token: 0x0600ADD5 RID: 44501
		event HTMLMarqueeElementEvents_onfocusinEventHandler onfocusin;

		// Token: 0x1400156D RID: 5485
		// (add) Token: 0x0600ADD6 RID: 44502
		// (remove) Token: 0x0600ADD7 RID: 44503
		event HTMLMarqueeElementEvents_onfocusoutEventHandler onfocusout;

		// Token: 0x1400156E RID: 5486
		// (add) Token: 0x0600ADD8 RID: 44504
		// (remove) Token: 0x0600ADD9 RID: 44505
		event HTMLMarqueeElementEvents_onchangeEventHandler onchange;

		// Token: 0x1400156F RID: 5487
		// (add) Token: 0x0600ADDA RID: 44506
		// (remove) Token: 0x0600ADDB RID: 44507
		event HTMLMarqueeElementEvents_onselectEventHandler onselect;

		// Token: 0x14001570 RID: 5488
		// (add) Token: 0x0600ADDC RID: 44508
		// (remove) Token: 0x0600ADDD RID: 44509
		event HTMLMarqueeElementEvents_onbounceEventHandler onbounce;

		// Token: 0x14001571 RID: 5489
		// (add) Token: 0x0600ADDE RID: 44510
		// (remove) Token: 0x0600ADDF RID: 44511
		event HTMLMarqueeElementEvents_onfinishEventHandler onfinish;

		// Token: 0x14001572 RID: 5490
		// (add) Token: 0x0600ADE0 RID: 44512
		// (remove) Token: 0x0600ADE1 RID: 44513
		event HTMLMarqueeElementEvents_onstartEventHandler onstart;
	}
}
