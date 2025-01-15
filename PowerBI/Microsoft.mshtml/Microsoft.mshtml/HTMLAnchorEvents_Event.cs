using System;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x0200039B RID: 923
	[ComEventInterface(typeof(HTMLAnchorEvents\u0000), typeof(HTMLAnchorEvents_EventProvider\u0000))]
	[ComVisible(false)]
	public interface HTMLAnchorEvents_Event
	{
		// Token: 0x1400067E RID: 1662
		// (add) Token: 0x06003F7F RID: 16255
		// (remove) Token: 0x06003F80 RID: 16256
		event HTMLAnchorEvents_onhelpEventHandler onhelp;

		// Token: 0x1400067F RID: 1663
		// (add) Token: 0x06003F81 RID: 16257
		// (remove) Token: 0x06003F82 RID: 16258
		event HTMLAnchorEvents_onclickEventHandler onclick;

		// Token: 0x14000680 RID: 1664
		// (add) Token: 0x06003F83 RID: 16259
		// (remove) Token: 0x06003F84 RID: 16260
		event HTMLAnchorEvents_ondblclickEventHandler ondblclick;

		// Token: 0x14000681 RID: 1665
		// (add) Token: 0x06003F85 RID: 16261
		// (remove) Token: 0x06003F86 RID: 16262
		event HTMLAnchorEvents_onkeypressEventHandler onkeypress;

		// Token: 0x14000682 RID: 1666
		// (add) Token: 0x06003F87 RID: 16263
		// (remove) Token: 0x06003F88 RID: 16264
		event HTMLAnchorEvents_onkeydownEventHandler onkeydown;

		// Token: 0x14000683 RID: 1667
		// (add) Token: 0x06003F89 RID: 16265
		// (remove) Token: 0x06003F8A RID: 16266
		event HTMLAnchorEvents_onkeyupEventHandler onkeyup;

		// Token: 0x14000684 RID: 1668
		// (add) Token: 0x06003F8B RID: 16267
		// (remove) Token: 0x06003F8C RID: 16268
		event HTMLAnchorEvents_onmouseoutEventHandler onmouseout;

		// Token: 0x14000685 RID: 1669
		// (add) Token: 0x06003F8D RID: 16269
		// (remove) Token: 0x06003F8E RID: 16270
		event HTMLAnchorEvents_onmouseoverEventHandler onmouseover;

		// Token: 0x14000686 RID: 1670
		// (add) Token: 0x06003F8F RID: 16271
		// (remove) Token: 0x06003F90 RID: 16272
		event HTMLAnchorEvents_onmousemoveEventHandler onmousemove;

		// Token: 0x14000687 RID: 1671
		// (add) Token: 0x06003F91 RID: 16273
		// (remove) Token: 0x06003F92 RID: 16274
		event HTMLAnchorEvents_onmousedownEventHandler onmousedown;

		// Token: 0x14000688 RID: 1672
		// (add) Token: 0x06003F93 RID: 16275
		// (remove) Token: 0x06003F94 RID: 16276
		event HTMLAnchorEvents_onmouseupEventHandler onmouseup;

		// Token: 0x14000689 RID: 1673
		// (add) Token: 0x06003F95 RID: 16277
		// (remove) Token: 0x06003F96 RID: 16278
		event HTMLAnchorEvents_onselectstartEventHandler onselectstart;

		// Token: 0x1400068A RID: 1674
		// (add) Token: 0x06003F97 RID: 16279
		// (remove) Token: 0x06003F98 RID: 16280
		event HTMLAnchorEvents_onfilterchangeEventHandler onfilterchange;

		// Token: 0x1400068B RID: 1675
		// (add) Token: 0x06003F99 RID: 16281
		// (remove) Token: 0x06003F9A RID: 16282
		event HTMLAnchorEvents_ondragstartEventHandler ondragstart;

		// Token: 0x1400068C RID: 1676
		// (add) Token: 0x06003F9B RID: 16283
		// (remove) Token: 0x06003F9C RID: 16284
		event HTMLAnchorEvents_onbeforeupdateEventHandler onbeforeupdate;

		// Token: 0x1400068D RID: 1677
		// (add) Token: 0x06003F9D RID: 16285
		// (remove) Token: 0x06003F9E RID: 16286
		event HTMLAnchorEvents_onafterupdateEventHandler onafterupdate;

		// Token: 0x1400068E RID: 1678
		// (add) Token: 0x06003F9F RID: 16287
		// (remove) Token: 0x06003FA0 RID: 16288
		event HTMLAnchorEvents_onerrorupdateEventHandler onerrorupdate;

		// Token: 0x1400068F RID: 1679
		// (add) Token: 0x06003FA1 RID: 16289
		// (remove) Token: 0x06003FA2 RID: 16290
		event HTMLAnchorEvents_onrowexitEventHandler onrowexit;

		// Token: 0x14000690 RID: 1680
		// (add) Token: 0x06003FA3 RID: 16291
		// (remove) Token: 0x06003FA4 RID: 16292
		event HTMLAnchorEvents_onrowenterEventHandler onrowenter;

		// Token: 0x14000691 RID: 1681
		// (add) Token: 0x06003FA5 RID: 16293
		// (remove) Token: 0x06003FA6 RID: 16294
		event HTMLAnchorEvents_ondatasetchangedEventHandler ondatasetchanged;

		// Token: 0x14000692 RID: 1682
		// (add) Token: 0x06003FA7 RID: 16295
		// (remove) Token: 0x06003FA8 RID: 16296
		event HTMLAnchorEvents_ondataavailableEventHandler ondataavailable;

		// Token: 0x14000693 RID: 1683
		// (add) Token: 0x06003FA9 RID: 16297
		// (remove) Token: 0x06003FAA RID: 16298
		event HTMLAnchorEvents_ondatasetcompleteEventHandler ondatasetcomplete;

		// Token: 0x14000694 RID: 1684
		// (add) Token: 0x06003FAB RID: 16299
		// (remove) Token: 0x06003FAC RID: 16300
		event HTMLAnchorEvents_onlosecaptureEventHandler onlosecapture;

		// Token: 0x14000695 RID: 1685
		// (add) Token: 0x06003FAD RID: 16301
		// (remove) Token: 0x06003FAE RID: 16302
		event HTMLAnchorEvents_onpropertychangeEventHandler onpropertychange;

		// Token: 0x14000696 RID: 1686
		// (add) Token: 0x06003FAF RID: 16303
		// (remove) Token: 0x06003FB0 RID: 16304
		event HTMLAnchorEvents_onscrollEventHandler onscroll;

		// Token: 0x14000697 RID: 1687
		// (add) Token: 0x06003FB1 RID: 16305
		// (remove) Token: 0x06003FB2 RID: 16306
		event HTMLAnchorEvents_onfocusEventHandler onfocus;

		// Token: 0x14000698 RID: 1688
		// (add) Token: 0x06003FB3 RID: 16307
		// (remove) Token: 0x06003FB4 RID: 16308
		event HTMLAnchorEvents_onblurEventHandler onblur;

		// Token: 0x14000699 RID: 1689
		// (add) Token: 0x06003FB5 RID: 16309
		// (remove) Token: 0x06003FB6 RID: 16310
		event HTMLAnchorEvents_onresizeEventHandler onresize;

		// Token: 0x1400069A RID: 1690
		// (add) Token: 0x06003FB7 RID: 16311
		// (remove) Token: 0x06003FB8 RID: 16312
		event HTMLAnchorEvents_ondragEventHandler ondrag;

		// Token: 0x1400069B RID: 1691
		// (add) Token: 0x06003FB9 RID: 16313
		// (remove) Token: 0x06003FBA RID: 16314
		event HTMLAnchorEvents_ondragendEventHandler ondragend;

		// Token: 0x1400069C RID: 1692
		// (add) Token: 0x06003FBB RID: 16315
		// (remove) Token: 0x06003FBC RID: 16316
		event HTMLAnchorEvents_ondragenterEventHandler ondragenter;

		// Token: 0x1400069D RID: 1693
		// (add) Token: 0x06003FBD RID: 16317
		// (remove) Token: 0x06003FBE RID: 16318
		event HTMLAnchorEvents_ondragoverEventHandler ondragover;

		// Token: 0x1400069E RID: 1694
		// (add) Token: 0x06003FBF RID: 16319
		// (remove) Token: 0x06003FC0 RID: 16320
		event HTMLAnchorEvents_ondragleaveEventHandler ondragleave;

		// Token: 0x1400069F RID: 1695
		// (add) Token: 0x06003FC1 RID: 16321
		// (remove) Token: 0x06003FC2 RID: 16322
		event HTMLAnchorEvents_ondropEventHandler ondrop;

		// Token: 0x140006A0 RID: 1696
		// (add) Token: 0x06003FC3 RID: 16323
		// (remove) Token: 0x06003FC4 RID: 16324
		event HTMLAnchorEvents_onbeforecutEventHandler onbeforecut;

		// Token: 0x140006A1 RID: 1697
		// (add) Token: 0x06003FC5 RID: 16325
		// (remove) Token: 0x06003FC6 RID: 16326
		event HTMLAnchorEvents_oncutEventHandler oncut;

		// Token: 0x140006A2 RID: 1698
		// (add) Token: 0x06003FC7 RID: 16327
		// (remove) Token: 0x06003FC8 RID: 16328
		event HTMLAnchorEvents_onbeforecopyEventHandler onbeforecopy;

		// Token: 0x140006A3 RID: 1699
		// (add) Token: 0x06003FC9 RID: 16329
		// (remove) Token: 0x06003FCA RID: 16330
		event HTMLAnchorEvents_oncopyEventHandler oncopy;

		// Token: 0x140006A4 RID: 1700
		// (add) Token: 0x06003FCB RID: 16331
		// (remove) Token: 0x06003FCC RID: 16332
		event HTMLAnchorEvents_onbeforepasteEventHandler onbeforepaste;

		// Token: 0x140006A5 RID: 1701
		// (add) Token: 0x06003FCD RID: 16333
		// (remove) Token: 0x06003FCE RID: 16334
		event HTMLAnchorEvents_onpasteEventHandler onpaste;

		// Token: 0x140006A6 RID: 1702
		// (add) Token: 0x06003FCF RID: 16335
		// (remove) Token: 0x06003FD0 RID: 16336
		event HTMLAnchorEvents_oncontextmenuEventHandler oncontextmenu;

		// Token: 0x140006A7 RID: 1703
		// (add) Token: 0x06003FD1 RID: 16337
		// (remove) Token: 0x06003FD2 RID: 16338
		event HTMLAnchorEvents_onrowsdeleteEventHandler onrowsdelete;

		// Token: 0x140006A8 RID: 1704
		// (add) Token: 0x06003FD3 RID: 16339
		// (remove) Token: 0x06003FD4 RID: 16340
		event HTMLAnchorEvents_onrowsinsertedEventHandler onrowsinserted;

		// Token: 0x140006A9 RID: 1705
		// (add) Token: 0x06003FD5 RID: 16341
		// (remove) Token: 0x06003FD6 RID: 16342
		event HTMLAnchorEvents_oncellchangeEventHandler oncellchange;

		// Token: 0x140006AA RID: 1706
		// (add) Token: 0x06003FD7 RID: 16343
		// (remove) Token: 0x06003FD8 RID: 16344
		event HTMLAnchorEvents_onreadystatechangeEventHandler onreadystatechange;

		// Token: 0x140006AB RID: 1707
		// (add) Token: 0x06003FD9 RID: 16345
		// (remove) Token: 0x06003FDA RID: 16346
		event HTMLAnchorEvents_onbeforeeditfocusEventHandler onbeforeeditfocus;

		// Token: 0x140006AC RID: 1708
		// (add) Token: 0x06003FDB RID: 16347
		// (remove) Token: 0x06003FDC RID: 16348
		event HTMLAnchorEvents_onlayoutcompleteEventHandler onlayoutcomplete;

		// Token: 0x140006AD RID: 1709
		// (add) Token: 0x06003FDD RID: 16349
		// (remove) Token: 0x06003FDE RID: 16350
		event HTMLAnchorEvents_onpageEventHandler onpage;

		// Token: 0x140006AE RID: 1710
		// (add) Token: 0x06003FDF RID: 16351
		// (remove) Token: 0x06003FE0 RID: 16352
		event HTMLAnchorEvents_onbeforedeactivateEventHandler onbeforedeactivate;

		// Token: 0x140006AF RID: 1711
		// (add) Token: 0x06003FE1 RID: 16353
		// (remove) Token: 0x06003FE2 RID: 16354
		event HTMLAnchorEvents_onbeforeactivateEventHandler onbeforeactivate;

		// Token: 0x140006B0 RID: 1712
		// (add) Token: 0x06003FE3 RID: 16355
		// (remove) Token: 0x06003FE4 RID: 16356
		event HTMLAnchorEvents_onmoveEventHandler onmove;

		// Token: 0x140006B1 RID: 1713
		// (add) Token: 0x06003FE5 RID: 16357
		// (remove) Token: 0x06003FE6 RID: 16358
		event HTMLAnchorEvents_oncontrolselectEventHandler oncontrolselect;

		// Token: 0x140006B2 RID: 1714
		// (add) Token: 0x06003FE7 RID: 16359
		// (remove) Token: 0x06003FE8 RID: 16360
		event HTMLAnchorEvents_onmovestartEventHandler onmovestart;

		// Token: 0x140006B3 RID: 1715
		// (add) Token: 0x06003FE9 RID: 16361
		// (remove) Token: 0x06003FEA RID: 16362
		event HTMLAnchorEvents_onmoveendEventHandler onmoveend;

		// Token: 0x140006B4 RID: 1716
		// (add) Token: 0x06003FEB RID: 16363
		// (remove) Token: 0x06003FEC RID: 16364
		event HTMLAnchorEvents_onresizestartEventHandler onresizestart;

		// Token: 0x140006B5 RID: 1717
		// (add) Token: 0x06003FED RID: 16365
		// (remove) Token: 0x06003FEE RID: 16366
		event HTMLAnchorEvents_onresizeendEventHandler onresizeend;

		// Token: 0x140006B6 RID: 1718
		// (add) Token: 0x06003FEF RID: 16367
		// (remove) Token: 0x06003FF0 RID: 16368
		event HTMLAnchorEvents_onmouseenterEventHandler onmouseenter;

		// Token: 0x140006B7 RID: 1719
		// (add) Token: 0x06003FF1 RID: 16369
		// (remove) Token: 0x06003FF2 RID: 16370
		event HTMLAnchorEvents_onmouseleaveEventHandler onmouseleave;

		// Token: 0x140006B8 RID: 1720
		// (add) Token: 0x06003FF3 RID: 16371
		// (remove) Token: 0x06003FF4 RID: 16372
		event HTMLAnchorEvents_onmousewheelEventHandler onmousewheel;

		// Token: 0x140006B9 RID: 1721
		// (add) Token: 0x06003FF5 RID: 16373
		// (remove) Token: 0x06003FF6 RID: 16374
		event HTMLAnchorEvents_onactivateEventHandler onactivate;

		// Token: 0x140006BA RID: 1722
		// (add) Token: 0x06003FF7 RID: 16375
		// (remove) Token: 0x06003FF8 RID: 16376
		event HTMLAnchorEvents_ondeactivateEventHandler ondeactivate;

		// Token: 0x140006BB RID: 1723
		// (add) Token: 0x06003FF9 RID: 16377
		// (remove) Token: 0x06003FFA RID: 16378
		event HTMLAnchorEvents_onfocusinEventHandler onfocusin;

		// Token: 0x140006BC RID: 1724
		// (add) Token: 0x06003FFB RID: 16379
		// (remove) Token: 0x06003FFC RID: 16380
		event HTMLAnchorEvents_onfocusoutEventHandler onfocusout;
	}
}
