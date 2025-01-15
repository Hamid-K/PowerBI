using System;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x0200030D RID: 781
	[ComEventInterface(typeof(HTMLTextContainerEvents\u0000), typeof(HTMLTextContainerEvents_EventProvider\u0000))]
	[ComVisible(false)]
	public interface HTMLTextContainerEvents_Event
	{
		// Token: 0x14000503 RID: 1283
		// (add) Token: 0x06003400 RID: 13312
		// (remove) Token: 0x06003401 RID: 13313
		event HTMLTextContainerEvents_onhelpEventHandler onhelp;

		// Token: 0x14000504 RID: 1284
		// (add) Token: 0x06003402 RID: 13314
		// (remove) Token: 0x06003403 RID: 13315
		event HTMLTextContainerEvents_onclickEventHandler onclick;

		// Token: 0x14000505 RID: 1285
		// (add) Token: 0x06003404 RID: 13316
		// (remove) Token: 0x06003405 RID: 13317
		event HTMLTextContainerEvents_ondblclickEventHandler ondblclick;

		// Token: 0x14000506 RID: 1286
		// (add) Token: 0x06003406 RID: 13318
		// (remove) Token: 0x06003407 RID: 13319
		event HTMLTextContainerEvents_onkeypressEventHandler onkeypress;

		// Token: 0x14000507 RID: 1287
		// (add) Token: 0x06003408 RID: 13320
		// (remove) Token: 0x06003409 RID: 13321
		event HTMLTextContainerEvents_onkeydownEventHandler onkeydown;

		// Token: 0x14000508 RID: 1288
		// (add) Token: 0x0600340A RID: 13322
		// (remove) Token: 0x0600340B RID: 13323
		event HTMLTextContainerEvents_onkeyupEventHandler onkeyup;

		// Token: 0x14000509 RID: 1289
		// (add) Token: 0x0600340C RID: 13324
		// (remove) Token: 0x0600340D RID: 13325
		event HTMLTextContainerEvents_onmouseoutEventHandler onmouseout;

		// Token: 0x1400050A RID: 1290
		// (add) Token: 0x0600340E RID: 13326
		// (remove) Token: 0x0600340F RID: 13327
		event HTMLTextContainerEvents_onmouseoverEventHandler onmouseover;

		// Token: 0x1400050B RID: 1291
		// (add) Token: 0x06003410 RID: 13328
		// (remove) Token: 0x06003411 RID: 13329
		event HTMLTextContainerEvents_onmousemoveEventHandler onmousemove;

		// Token: 0x1400050C RID: 1292
		// (add) Token: 0x06003412 RID: 13330
		// (remove) Token: 0x06003413 RID: 13331
		event HTMLTextContainerEvents_onmousedownEventHandler onmousedown;

		// Token: 0x1400050D RID: 1293
		// (add) Token: 0x06003414 RID: 13332
		// (remove) Token: 0x06003415 RID: 13333
		event HTMLTextContainerEvents_onmouseupEventHandler onmouseup;

		// Token: 0x1400050E RID: 1294
		// (add) Token: 0x06003416 RID: 13334
		// (remove) Token: 0x06003417 RID: 13335
		event HTMLTextContainerEvents_onselectstartEventHandler onselectstart;

		// Token: 0x1400050F RID: 1295
		// (add) Token: 0x06003418 RID: 13336
		// (remove) Token: 0x06003419 RID: 13337
		event HTMLTextContainerEvents_onfilterchangeEventHandler onfilterchange;

		// Token: 0x14000510 RID: 1296
		// (add) Token: 0x0600341A RID: 13338
		// (remove) Token: 0x0600341B RID: 13339
		event HTMLTextContainerEvents_ondragstartEventHandler ondragstart;

		// Token: 0x14000511 RID: 1297
		// (add) Token: 0x0600341C RID: 13340
		// (remove) Token: 0x0600341D RID: 13341
		event HTMLTextContainerEvents_onbeforeupdateEventHandler onbeforeupdate;

		// Token: 0x14000512 RID: 1298
		// (add) Token: 0x0600341E RID: 13342
		// (remove) Token: 0x0600341F RID: 13343
		event HTMLTextContainerEvents_onafterupdateEventHandler onafterupdate;

		// Token: 0x14000513 RID: 1299
		// (add) Token: 0x06003420 RID: 13344
		// (remove) Token: 0x06003421 RID: 13345
		event HTMLTextContainerEvents_onerrorupdateEventHandler onerrorupdate;

		// Token: 0x14000514 RID: 1300
		// (add) Token: 0x06003422 RID: 13346
		// (remove) Token: 0x06003423 RID: 13347
		event HTMLTextContainerEvents_onrowexitEventHandler onrowexit;

		// Token: 0x14000515 RID: 1301
		// (add) Token: 0x06003424 RID: 13348
		// (remove) Token: 0x06003425 RID: 13349
		event HTMLTextContainerEvents_onrowenterEventHandler onrowenter;

		// Token: 0x14000516 RID: 1302
		// (add) Token: 0x06003426 RID: 13350
		// (remove) Token: 0x06003427 RID: 13351
		event HTMLTextContainerEvents_ondatasetchangedEventHandler ondatasetchanged;

		// Token: 0x14000517 RID: 1303
		// (add) Token: 0x06003428 RID: 13352
		// (remove) Token: 0x06003429 RID: 13353
		event HTMLTextContainerEvents_ondataavailableEventHandler ondataavailable;

		// Token: 0x14000518 RID: 1304
		// (add) Token: 0x0600342A RID: 13354
		// (remove) Token: 0x0600342B RID: 13355
		event HTMLTextContainerEvents_ondatasetcompleteEventHandler ondatasetcomplete;

		// Token: 0x14000519 RID: 1305
		// (add) Token: 0x0600342C RID: 13356
		// (remove) Token: 0x0600342D RID: 13357
		event HTMLTextContainerEvents_onlosecaptureEventHandler onlosecapture;

		// Token: 0x1400051A RID: 1306
		// (add) Token: 0x0600342E RID: 13358
		// (remove) Token: 0x0600342F RID: 13359
		event HTMLTextContainerEvents_onpropertychangeEventHandler onpropertychange;

		// Token: 0x1400051B RID: 1307
		// (add) Token: 0x06003430 RID: 13360
		// (remove) Token: 0x06003431 RID: 13361
		event HTMLTextContainerEvents_onscrollEventHandler onscroll;

		// Token: 0x1400051C RID: 1308
		// (add) Token: 0x06003432 RID: 13362
		// (remove) Token: 0x06003433 RID: 13363
		event HTMLTextContainerEvents_onfocusEventHandler onfocus;

		// Token: 0x1400051D RID: 1309
		// (add) Token: 0x06003434 RID: 13364
		// (remove) Token: 0x06003435 RID: 13365
		event HTMLTextContainerEvents_onblurEventHandler onblur;

		// Token: 0x1400051E RID: 1310
		// (add) Token: 0x06003436 RID: 13366
		// (remove) Token: 0x06003437 RID: 13367
		event HTMLTextContainerEvents_onresizeEventHandler onresize;

		// Token: 0x1400051F RID: 1311
		// (add) Token: 0x06003438 RID: 13368
		// (remove) Token: 0x06003439 RID: 13369
		event HTMLTextContainerEvents_ondragEventHandler ondrag;

		// Token: 0x14000520 RID: 1312
		// (add) Token: 0x0600343A RID: 13370
		// (remove) Token: 0x0600343B RID: 13371
		event HTMLTextContainerEvents_ondragendEventHandler ondragend;

		// Token: 0x14000521 RID: 1313
		// (add) Token: 0x0600343C RID: 13372
		// (remove) Token: 0x0600343D RID: 13373
		event HTMLTextContainerEvents_ondragenterEventHandler ondragenter;

		// Token: 0x14000522 RID: 1314
		// (add) Token: 0x0600343E RID: 13374
		// (remove) Token: 0x0600343F RID: 13375
		event HTMLTextContainerEvents_ondragoverEventHandler ondragover;

		// Token: 0x14000523 RID: 1315
		// (add) Token: 0x06003440 RID: 13376
		// (remove) Token: 0x06003441 RID: 13377
		event HTMLTextContainerEvents_ondragleaveEventHandler ondragleave;

		// Token: 0x14000524 RID: 1316
		// (add) Token: 0x06003442 RID: 13378
		// (remove) Token: 0x06003443 RID: 13379
		event HTMLTextContainerEvents_ondropEventHandler ondrop;

		// Token: 0x14000525 RID: 1317
		// (add) Token: 0x06003444 RID: 13380
		// (remove) Token: 0x06003445 RID: 13381
		event HTMLTextContainerEvents_onbeforecutEventHandler onbeforecut;

		// Token: 0x14000526 RID: 1318
		// (add) Token: 0x06003446 RID: 13382
		// (remove) Token: 0x06003447 RID: 13383
		event HTMLTextContainerEvents_oncutEventHandler oncut;

		// Token: 0x14000527 RID: 1319
		// (add) Token: 0x06003448 RID: 13384
		// (remove) Token: 0x06003449 RID: 13385
		event HTMLTextContainerEvents_onbeforecopyEventHandler onbeforecopy;

		// Token: 0x14000528 RID: 1320
		// (add) Token: 0x0600344A RID: 13386
		// (remove) Token: 0x0600344B RID: 13387
		event HTMLTextContainerEvents_oncopyEventHandler oncopy;

		// Token: 0x14000529 RID: 1321
		// (add) Token: 0x0600344C RID: 13388
		// (remove) Token: 0x0600344D RID: 13389
		event HTMLTextContainerEvents_onbeforepasteEventHandler onbeforepaste;

		// Token: 0x1400052A RID: 1322
		// (add) Token: 0x0600344E RID: 13390
		// (remove) Token: 0x0600344F RID: 13391
		event HTMLTextContainerEvents_onpasteEventHandler onpaste;

		// Token: 0x1400052B RID: 1323
		// (add) Token: 0x06003450 RID: 13392
		// (remove) Token: 0x06003451 RID: 13393
		event HTMLTextContainerEvents_oncontextmenuEventHandler oncontextmenu;

		// Token: 0x1400052C RID: 1324
		// (add) Token: 0x06003452 RID: 13394
		// (remove) Token: 0x06003453 RID: 13395
		event HTMLTextContainerEvents_onrowsdeleteEventHandler onrowsdelete;

		// Token: 0x1400052D RID: 1325
		// (add) Token: 0x06003454 RID: 13396
		// (remove) Token: 0x06003455 RID: 13397
		event HTMLTextContainerEvents_onrowsinsertedEventHandler onrowsinserted;

		// Token: 0x1400052E RID: 1326
		// (add) Token: 0x06003456 RID: 13398
		// (remove) Token: 0x06003457 RID: 13399
		event HTMLTextContainerEvents_oncellchangeEventHandler oncellchange;

		// Token: 0x1400052F RID: 1327
		// (add) Token: 0x06003458 RID: 13400
		// (remove) Token: 0x06003459 RID: 13401
		event HTMLTextContainerEvents_onreadystatechangeEventHandler onreadystatechange;

		// Token: 0x14000530 RID: 1328
		// (add) Token: 0x0600345A RID: 13402
		// (remove) Token: 0x0600345B RID: 13403
		event HTMLTextContainerEvents_onbeforeeditfocusEventHandler onbeforeeditfocus;

		// Token: 0x14000531 RID: 1329
		// (add) Token: 0x0600345C RID: 13404
		// (remove) Token: 0x0600345D RID: 13405
		event HTMLTextContainerEvents_onlayoutcompleteEventHandler onlayoutcomplete;

		// Token: 0x14000532 RID: 1330
		// (add) Token: 0x0600345E RID: 13406
		// (remove) Token: 0x0600345F RID: 13407
		event HTMLTextContainerEvents_onpageEventHandler onpage;

		// Token: 0x14000533 RID: 1331
		// (add) Token: 0x06003460 RID: 13408
		// (remove) Token: 0x06003461 RID: 13409
		event HTMLTextContainerEvents_onbeforedeactivateEventHandler onbeforedeactivate;

		// Token: 0x14000534 RID: 1332
		// (add) Token: 0x06003462 RID: 13410
		// (remove) Token: 0x06003463 RID: 13411
		event HTMLTextContainerEvents_onbeforeactivateEventHandler onbeforeactivate;

		// Token: 0x14000535 RID: 1333
		// (add) Token: 0x06003464 RID: 13412
		// (remove) Token: 0x06003465 RID: 13413
		event HTMLTextContainerEvents_onmoveEventHandler onmove;

		// Token: 0x14000536 RID: 1334
		// (add) Token: 0x06003466 RID: 13414
		// (remove) Token: 0x06003467 RID: 13415
		event HTMLTextContainerEvents_oncontrolselectEventHandler oncontrolselect;

		// Token: 0x14000537 RID: 1335
		// (add) Token: 0x06003468 RID: 13416
		// (remove) Token: 0x06003469 RID: 13417
		event HTMLTextContainerEvents_onmovestartEventHandler onmovestart;

		// Token: 0x14000538 RID: 1336
		// (add) Token: 0x0600346A RID: 13418
		// (remove) Token: 0x0600346B RID: 13419
		event HTMLTextContainerEvents_onmoveendEventHandler onmoveend;

		// Token: 0x14000539 RID: 1337
		// (add) Token: 0x0600346C RID: 13420
		// (remove) Token: 0x0600346D RID: 13421
		event HTMLTextContainerEvents_onresizestartEventHandler onresizestart;

		// Token: 0x1400053A RID: 1338
		// (add) Token: 0x0600346E RID: 13422
		// (remove) Token: 0x0600346F RID: 13423
		event HTMLTextContainerEvents_onresizeendEventHandler onresizeend;

		// Token: 0x1400053B RID: 1339
		// (add) Token: 0x06003470 RID: 13424
		// (remove) Token: 0x06003471 RID: 13425
		event HTMLTextContainerEvents_onmouseenterEventHandler onmouseenter;

		// Token: 0x1400053C RID: 1340
		// (add) Token: 0x06003472 RID: 13426
		// (remove) Token: 0x06003473 RID: 13427
		event HTMLTextContainerEvents_onmouseleaveEventHandler onmouseleave;

		// Token: 0x1400053D RID: 1341
		// (add) Token: 0x06003474 RID: 13428
		// (remove) Token: 0x06003475 RID: 13429
		event HTMLTextContainerEvents_onmousewheelEventHandler onmousewheel;

		// Token: 0x1400053E RID: 1342
		// (add) Token: 0x06003476 RID: 13430
		// (remove) Token: 0x06003477 RID: 13431
		event HTMLTextContainerEvents_onactivateEventHandler onactivate;

		// Token: 0x1400053F RID: 1343
		// (add) Token: 0x06003478 RID: 13432
		// (remove) Token: 0x06003479 RID: 13433
		event HTMLTextContainerEvents_ondeactivateEventHandler ondeactivate;

		// Token: 0x14000540 RID: 1344
		// (add) Token: 0x0600347A RID: 13434
		// (remove) Token: 0x0600347B RID: 13435
		event HTMLTextContainerEvents_onfocusinEventHandler onfocusin;

		// Token: 0x14000541 RID: 1345
		// (add) Token: 0x0600347C RID: 13436
		// (remove) Token: 0x0600347D RID: 13437
		event HTMLTextContainerEvents_onfocusoutEventHandler onfocusout;

		// Token: 0x14000542 RID: 1346
		// (add) Token: 0x0600347E RID: 13438
		// (remove) Token: 0x0600347F RID: 13439
		event HTMLTextContainerEvents_onchangeEventHandler onchange;

		// Token: 0x14000543 RID: 1347
		// (add) Token: 0x06003480 RID: 13440
		// (remove) Token: 0x06003481 RID: 13441
		event HTMLTextContainerEvents_onselectEventHandler onselect;
	}
}
