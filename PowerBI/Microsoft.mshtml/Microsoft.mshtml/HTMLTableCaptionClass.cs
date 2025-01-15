using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x020009DE RID: 2526
	[Guid("3050F2EC-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComSourceInterfaces("mshtml.HTMLTextContainerEvents\0mshtml.HTMLTextContainerEvents2\0\0")]
	[TypeLibType(2)]
	[ClassInterface(0)]
	[ComImport]
	public class HTMLTableCaptionClass : DispHTMLTableCaption, HTMLTableCaption, HTMLTextContainerEvents_Event, IHTMLElement, IHTMLElement2, IHTMLElement3, IHTMLElement4, IHTMLUniqueName, IHTMLDOMNode, IHTMLDOMNode2, IHTMLControlElement, IHTMLTextContainer, IHTMLTableCaption, HTMLTextContainerEvents2_Event
	{
		// Token: 0x0600F441 RID: 62529
		[MethodImpl(4096, MethodCodeType = 3)]
		public extern HTMLTableCaptionClass();

		// Token: 0x0600F442 RID: 62530
		[DispId(-2147417611)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void setAttribute([MarshalAs(19)] [In] string strAttributeName, [MarshalAs(27)] [In] object AttributeValue, [In] int lFlags = 1);

		// Token: 0x0600F443 RID: 62531
		[DispId(-2147417610)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		public virtual extern object getAttribute([MarshalAs(19)] [In] string strAttributeName, [In] int lFlags = 0);

		// Token: 0x0600F444 RID: 62532
		[DispId(-2147417609)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool removeAttribute([MarshalAs(19)] [In] string strAttributeName, [In] int lFlags = 1);

		// Token: 0x17004F91 RID: 20369
		// (get) Token: 0x0600F446 RID: 62534
		// (set) Token: 0x0600F445 RID: 62533
		[DispId(-2147417111)]
		public virtual extern string className
		{
			[TypeLibFunc(4)]
			[DispId(-2147417111)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(4)]
			[DispId(-2147417111)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17004F92 RID: 20370
		// (get) Token: 0x0600F448 RID: 62536
		// (set) Token: 0x0600F447 RID: 62535
		[DispId(-2147417110)]
		public virtual extern string id
		{
			[TypeLibFunc(4)]
			[DispId(-2147417110)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147417110)]
			[TypeLibFunc(4)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17004F93 RID: 20371
		// (get) Token: 0x0600F449 RID: 62537
		[DispId(-2147417108)]
		public virtual extern string tagName
		{
			[DispId(-2147417108)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17004F94 RID: 20372
		// (get) Token: 0x0600F44A RID: 62538
		[DispId(-2147418104)]
		public virtual extern IHTMLElement parentElement
		{
			[DispId(-2147418104)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17004F95 RID: 20373
		// (get) Token: 0x0600F44B RID: 62539
		[DispId(-2147418038)]
		public virtual extern IHTMLStyle style
		{
			[TypeLibFunc(1024)]
			[DispId(-2147418038)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17004F96 RID: 20374
		// (get) Token: 0x0600F44D RID: 62541
		// (set) Token: 0x0600F44C RID: 62540
		[DispId(-2147412099)]
		public virtual extern object onhelp
		{
			[TypeLibFunc(20)]
			[DispId(-2147412099)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412099)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17004F97 RID: 20375
		// (get) Token: 0x0600F44F RID: 62543
		// (set) Token: 0x0600F44E RID: 62542
		[DispId(-2147412104)]
		public virtual extern object onclick
		{
			[DispId(-2147412104)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412104)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17004F98 RID: 20376
		// (get) Token: 0x0600F451 RID: 62545
		// (set) Token: 0x0600F450 RID: 62544
		[DispId(-2147412103)]
		public virtual extern object ondblclick
		{
			[DispId(-2147412103)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412103)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17004F99 RID: 20377
		// (get) Token: 0x0600F453 RID: 62547
		// (set) Token: 0x0600F452 RID: 62546
		[DispId(-2147412107)]
		public virtual extern object onkeydown
		{
			[DispId(-2147412107)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412107)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17004F9A RID: 20378
		// (get) Token: 0x0600F455 RID: 62549
		// (set) Token: 0x0600F454 RID: 62548
		[DispId(-2147412106)]
		public virtual extern object onkeyup
		{
			[TypeLibFunc(20)]
			[DispId(-2147412106)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412106)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17004F9B RID: 20379
		// (get) Token: 0x0600F457 RID: 62551
		// (set) Token: 0x0600F456 RID: 62550
		[DispId(-2147412105)]
		public virtual extern object onkeypress
		{
			[DispId(-2147412105)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412105)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17004F9C RID: 20380
		// (get) Token: 0x0600F459 RID: 62553
		// (set) Token: 0x0600F458 RID: 62552
		[DispId(-2147412111)]
		public virtual extern object onmouseout
		{
			[TypeLibFunc(20)]
			[DispId(-2147412111)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412111)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17004F9D RID: 20381
		// (get) Token: 0x0600F45B RID: 62555
		// (set) Token: 0x0600F45A RID: 62554
		[DispId(-2147412112)]
		public virtual extern object onmouseover
		{
			[TypeLibFunc(20)]
			[DispId(-2147412112)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412112)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17004F9E RID: 20382
		// (get) Token: 0x0600F45D RID: 62557
		// (set) Token: 0x0600F45C RID: 62556
		[DispId(-2147412108)]
		public virtual extern object onmousemove
		{
			[TypeLibFunc(20)]
			[DispId(-2147412108)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412108)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17004F9F RID: 20383
		// (get) Token: 0x0600F45F RID: 62559
		// (set) Token: 0x0600F45E RID: 62558
		[DispId(-2147412110)]
		public virtual extern object onmousedown
		{
			[DispId(-2147412110)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412110)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17004FA0 RID: 20384
		// (get) Token: 0x0600F461 RID: 62561
		// (set) Token: 0x0600F460 RID: 62560
		[DispId(-2147412109)]
		public virtual extern object onmouseup
		{
			[DispId(-2147412109)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412109)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17004FA1 RID: 20385
		// (get) Token: 0x0600F462 RID: 62562
		[DispId(-2147417094)]
		public virtual extern object document
		{
			[DispId(-2147417094)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17004FA2 RID: 20386
		// (get) Token: 0x0600F464 RID: 62564
		// (set) Token: 0x0600F463 RID: 62563
		[DispId(-2147418043)]
		public virtual extern string title
		{
			[DispId(-2147418043)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147418043)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17004FA3 RID: 20387
		// (get) Token: 0x0600F466 RID: 62566
		// (set) Token: 0x0600F465 RID: 62565
		[DispId(-2147413012)]
		public virtual extern string language
		{
			[TypeLibFunc(20)]
			[DispId(-2147413012)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147413012)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17004FA4 RID: 20388
		// (get) Token: 0x0600F468 RID: 62568
		// (set) Token: 0x0600F467 RID: 62567
		[DispId(-2147412075)]
		public virtual extern object onselectstart
		{
			[TypeLibFunc(20)]
			[DispId(-2147412075)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412075)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x0600F469 RID: 62569
		[DispId(-2147417093)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void scrollIntoView([MarshalAs(27)] [In] [Optional] object varargStart);

		// Token: 0x0600F46A RID: 62570
		[DispId(-2147417092)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool contains([MarshalAs(28)] [In] IHTMLElement pChild);

		// Token: 0x17004FA5 RID: 20389
		// (get) Token: 0x0600F46B RID: 62571
		[DispId(-2147417088)]
		public virtual extern int sourceIndex
		{
			[TypeLibFunc(4)]
			[DispId(-2147417088)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17004FA6 RID: 20390
		// (get) Token: 0x0600F46C RID: 62572
		[DispId(-2147417087)]
		public virtual extern object recordNumber
		{
			[DispId(-2147417087)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x17004FA7 RID: 20391
		// (get) Token: 0x0600F46E RID: 62574
		// (set) Token: 0x0600F46D RID: 62573
		[DispId(-2147413103)]
		public virtual extern string lang
		{
			[DispId(-2147413103)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147413103)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17004FA8 RID: 20392
		// (get) Token: 0x0600F46F RID: 62575
		[DispId(-2147417104)]
		public virtual extern int offsetLeft
		{
			[DispId(-2147417104)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17004FA9 RID: 20393
		// (get) Token: 0x0600F470 RID: 62576
		[DispId(-2147417103)]
		public virtual extern int offsetTop
		{
			[DispId(-2147417103)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17004FAA RID: 20394
		// (get) Token: 0x0600F471 RID: 62577
		[DispId(-2147417102)]
		public virtual extern int offsetWidth
		{
			[DispId(-2147417102)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17004FAB RID: 20395
		// (get) Token: 0x0600F472 RID: 62578
		[DispId(-2147417101)]
		public virtual extern int offsetHeight
		{
			[DispId(-2147417101)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17004FAC RID: 20396
		// (get) Token: 0x0600F473 RID: 62579
		[DispId(-2147417100)]
		public virtual extern IHTMLElement offsetParent
		{
			[DispId(-2147417100)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17004FAD RID: 20397
		// (get) Token: 0x0600F475 RID: 62581
		// (set) Token: 0x0600F474 RID: 62580
		[DispId(-2147417086)]
		public virtual extern string innerHTML
		{
			[DispId(-2147417086)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147417086)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17004FAE RID: 20398
		// (get) Token: 0x0600F477 RID: 62583
		// (set) Token: 0x0600F476 RID: 62582
		[DispId(-2147417085)]
		public virtual extern string innerText
		{
			[DispId(-2147417085)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147417085)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17004FAF RID: 20399
		// (get) Token: 0x0600F479 RID: 62585
		// (set) Token: 0x0600F478 RID: 62584
		[DispId(-2147417084)]
		public virtual extern string outerHTML
		{
			[DispId(-2147417084)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147417084)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17004FB0 RID: 20400
		// (get) Token: 0x0600F47B RID: 62587
		// (set) Token: 0x0600F47A RID: 62586
		[DispId(-2147417083)]
		public virtual extern string outerText
		{
			[DispId(-2147417083)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147417083)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x0600F47C RID: 62588
		[DispId(-2147417082)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void insertAdjacentHTML([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string html);

		// Token: 0x0600F47D RID: 62589
		[DispId(-2147417081)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void insertAdjacentText([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string text);

		// Token: 0x17004FB1 RID: 20401
		// (get) Token: 0x0600F47E RID: 62590
		[DispId(-2147417080)]
		public virtual extern IHTMLElement parentTextEdit
		{
			[DispId(-2147417080)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17004FB2 RID: 20402
		// (get) Token: 0x0600F47F RID: 62591
		[DispId(-2147417078)]
		public virtual extern bool isTextEdit
		{
			[DispId(-2147417078)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x0600F480 RID: 62592
		[DispId(-2147417079)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void click();

		// Token: 0x17004FB3 RID: 20403
		// (get) Token: 0x0600F481 RID: 62593
		[DispId(-2147417077)]
		public virtual extern IHTMLFiltersCollection filters
		{
			[DispId(-2147417077)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17004FB4 RID: 20404
		// (get) Token: 0x0600F483 RID: 62595
		// (set) Token: 0x0600F482 RID: 62594
		[DispId(-2147412077)]
		public virtual extern object ondragstart
		{
			[TypeLibFunc(20)]
			[DispId(-2147412077)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412077)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x0600F484 RID: 62596
		[DispId(-2147417076)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string toString();

		// Token: 0x17004FB5 RID: 20405
		// (get) Token: 0x0600F486 RID: 62598
		// (set) Token: 0x0600F485 RID: 62597
		[DispId(-2147412091)]
		public virtual extern object onbeforeupdate
		{
			[DispId(-2147412091)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412091)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17004FB6 RID: 20406
		// (get) Token: 0x0600F488 RID: 62600
		// (set) Token: 0x0600F487 RID: 62599
		[DispId(-2147412090)]
		public virtual extern object onafterupdate
		{
			[DispId(-2147412090)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412090)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17004FB7 RID: 20407
		// (get) Token: 0x0600F48A RID: 62602
		// (set) Token: 0x0600F489 RID: 62601
		[DispId(-2147412074)]
		public virtual extern object onerrorupdate
		{
			[DispId(-2147412074)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412074)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17004FB8 RID: 20408
		// (get) Token: 0x0600F48C RID: 62604
		// (set) Token: 0x0600F48B RID: 62603
		[DispId(-2147412094)]
		public virtual extern object onrowexit
		{
			[DispId(-2147412094)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412094)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17004FB9 RID: 20409
		// (get) Token: 0x0600F48E RID: 62606
		// (set) Token: 0x0600F48D RID: 62605
		[DispId(-2147412093)]
		public virtual extern object onrowenter
		{
			[DispId(-2147412093)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412093)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17004FBA RID: 20410
		// (get) Token: 0x0600F490 RID: 62608
		// (set) Token: 0x0600F48F RID: 62607
		[DispId(-2147412072)]
		public virtual extern object ondatasetchanged
		{
			[DispId(-2147412072)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412072)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17004FBB RID: 20411
		// (get) Token: 0x0600F492 RID: 62610
		// (set) Token: 0x0600F491 RID: 62609
		[DispId(-2147412071)]
		public virtual extern object ondataavailable
		{
			[TypeLibFunc(20)]
			[DispId(-2147412071)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412071)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17004FBC RID: 20412
		// (get) Token: 0x0600F494 RID: 62612
		// (set) Token: 0x0600F493 RID: 62611
		[DispId(-2147412070)]
		public virtual extern object ondatasetcomplete
		{
			[TypeLibFunc(20)]
			[DispId(-2147412070)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412070)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17004FBD RID: 20413
		// (get) Token: 0x0600F496 RID: 62614
		// (set) Token: 0x0600F495 RID: 62613
		[DispId(-2147412069)]
		public virtual extern object onfilterchange
		{
			[DispId(-2147412069)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412069)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17004FBE RID: 20414
		// (get) Token: 0x0600F497 RID: 62615
		[DispId(-2147417075)]
		public virtual extern object children
		{
			[DispId(-2147417075)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17004FBF RID: 20415
		// (get) Token: 0x0600F498 RID: 62616
		[DispId(-2147417074)]
		public virtual extern object all
		{
			[DispId(-2147417074)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17004FC0 RID: 20416
		// (get) Token: 0x0600F499 RID: 62617
		[DispId(-2147417073)]
		public virtual extern string scopeName
		{
			[DispId(-2147417073)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x0600F49A RID: 62618
		[DispId(-2147417072)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void setCapture([In] bool containerCapture = true);

		// Token: 0x0600F49B RID: 62619
		[DispId(-2147417071)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void releaseCapture();

		// Token: 0x17004FC1 RID: 20417
		// (get) Token: 0x0600F49D RID: 62621
		// (set) Token: 0x0600F49C RID: 62620
		[DispId(-2147412066)]
		public virtual extern object onlosecapture
		{
			[DispId(-2147412066)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412066)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x0600F49E RID: 62622
		[DispId(-2147417070)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string componentFromPoint([In] int x, [In] int y);

		// Token: 0x0600F49F RID: 62623
		[DispId(-2147417069)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void doScroll([MarshalAs(27)] [In] [Optional] object component);

		// Token: 0x17004FC2 RID: 20418
		// (get) Token: 0x0600F4A1 RID: 62625
		// (set) Token: 0x0600F4A0 RID: 62624
		[DispId(-2147412081)]
		public virtual extern object onscroll
		{
			[DispId(-2147412081)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412081)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17004FC3 RID: 20419
		// (get) Token: 0x0600F4A3 RID: 62627
		// (set) Token: 0x0600F4A2 RID: 62626
		[DispId(-2147412063)]
		public virtual extern object ondrag
		{
			[TypeLibFunc(20)]
			[DispId(-2147412063)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412063)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17004FC4 RID: 20420
		// (get) Token: 0x0600F4A5 RID: 62629
		// (set) Token: 0x0600F4A4 RID: 62628
		[DispId(-2147412062)]
		public virtual extern object ondragend
		{
			[DispId(-2147412062)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412062)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17004FC5 RID: 20421
		// (get) Token: 0x0600F4A7 RID: 62631
		// (set) Token: 0x0600F4A6 RID: 62630
		[DispId(-2147412061)]
		public virtual extern object ondragenter
		{
			[DispId(-2147412061)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412061)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17004FC6 RID: 20422
		// (get) Token: 0x0600F4A9 RID: 62633
		// (set) Token: 0x0600F4A8 RID: 62632
		[DispId(-2147412060)]
		public virtual extern object ondragover
		{
			[TypeLibFunc(20)]
			[DispId(-2147412060)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412060)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17004FC7 RID: 20423
		// (get) Token: 0x0600F4AB RID: 62635
		// (set) Token: 0x0600F4AA RID: 62634
		[DispId(-2147412059)]
		public virtual extern object ondragleave
		{
			[TypeLibFunc(20)]
			[DispId(-2147412059)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412059)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17004FC8 RID: 20424
		// (get) Token: 0x0600F4AD RID: 62637
		// (set) Token: 0x0600F4AC RID: 62636
		[DispId(-2147412058)]
		public virtual extern object ondrop
		{
			[DispId(-2147412058)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412058)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17004FC9 RID: 20425
		// (get) Token: 0x0600F4AF RID: 62639
		// (set) Token: 0x0600F4AE RID: 62638
		[DispId(-2147412054)]
		public virtual extern object onbeforecut
		{
			[DispId(-2147412054)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412054)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17004FCA RID: 20426
		// (get) Token: 0x0600F4B1 RID: 62641
		// (set) Token: 0x0600F4B0 RID: 62640
		[DispId(-2147412057)]
		public virtual extern object oncut
		{
			[DispId(-2147412057)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412057)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17004FCB RID: 20427
		// (get) Token: 0x0600F4B3 RID: 62643
		// (set) Token: 0x0600F4B2 RID: 62642
		[DispId(-2147412053)]
		public virtual extern object onbeforecopy
		{
			[DispId(-2147412053)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412053)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17004FCC RID: 20428
		// (get) Token: 0x0600F4B5 RID: 62645
		// (set) Token: 0x0600F4B4 RID: 62644
		[DispId(-2147412056)]
		public virtual extern object oncopy
		{
			[TypeLibFunc(20)]
			[DispId(-2147412056)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412056)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17004FCD RID: 20429
		// (get) Token: 0x0600F4B7 RID: 62647
		// (set) Token: 0x0600F4B6 RID: 62646
		[DispId(-2147412052)]
		public virtual extern object onbeforepaste
		{
			[TypeLibFunc(20)]
			[DispId(-2147412052)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412052)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17004FCE RID: 20430
		// (get) Token: 0x0600F4B9 RID: 62649
		// (set) Token: 0x0600F4B8 RID: 62648
		[DispId(-2147412055)]
		public virtual extern object onpaste
		{
			[DispId(-2147412055)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412055)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17004FCF RID: 20431
		// (get) Token: 0x0600F4BA RID: 62650
		[DispId(-2147417105)]
		public virtual extern IHTMLCurrentStyle currentStyle
		{
			[DispId(-2147417105)]
			[TypeLibFunc(1024)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17004FD0 RID: 20432
		// (get) Token: 0x0600F4BC RID: 62652
		// (set) Token: 0x0600F4BB RID: 62651
		[DispId(-2147412065)]
		public virtual extern object onpropertychange
		{
			[DispId(-2147412065)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412065)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x0600F4BD RID: 62653
		[DispId(-2147417068)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLRectCollection getClientRects();

		// Token: 0x0600F4BE RID: 62654
		[DispId(-2147417067)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLRect getBoundingClientRect();

		// Token: 0x0600F4BF RID: 62655
		[DispId(-2147417608)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void setExpression([MarshalAs(19)] [In] string propname, [MarshalAs(19)] [In] string expression, [MarshalAs(19)] [In] string language = "");

		// Token: 0x0600F4C0 RID: 62656
		[DispId(-2147417607)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		public virtual extern object getExpression([MarshalAs(19)] [In] string propname);

		// Token: 0x0600F4C1 RID: 62657
		[DispId(-2147417606)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool removeExpression([MarshalAs(19)] [In] string propname);

		// Token: 0x17004FD1 RID: 20433
		// (get) Token: 0x0600F4C3 RID: 62659
		// (set) Token: 0x0600F4C2 RID: 62658
		[DispId(-2147418097)]
		public virtual extern short tabIndex
		{
			[TypeLibFunc(20)]
			[DispId(-2147418097)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147418097)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x0600F4C4 RID: 62660
		[DispId(-2147416112)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void focus();

		// Token: 0x17004FD2 RID: 20434
		// (get) Token: 0x0600F4C6 RID: 62662
		// (set) Token: 0x0600F4C5 RID: 62661
		[DispId(-2147416107)]
		public virtual extern string accessKey
		{
			[DispId(-2147416107)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147416107)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17004FD3 RID: 20435
		// (get) Token: 0x0600F4C8 RID: 62664
		// (set) Token: 0x0600F4C7 RID: 62663
		[DispId(-2147412097)]
		public virtual extern object onblur
		{
			[DispId(-2147412097)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412097)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17004FD4 RID: 20436
		// (get) Token: 0x0600F4CA RID: 62666
		// (set) Token: 0x0600F4C9 RID: 62665
		[DispId(-2147412098)]
		public virtual extern object onfocus
		{
			[TypeLibFunc(20)]
			[DispId(-2147412098)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412098)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17004FD5 RID: 20437
		// (get) Token: 0x0600F4CC RID: 62668
		// (set) Token: 0x0600F4CB RID: 62667
		[DispId(-2147412076)]
		public virtual extern object onresize
		{
			[TypeLibFunc(20)]
			[DispId(-2147412076)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412076)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x0600F4CD RID: 62669
		[DispId(-2147416110)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void blur();

		// Token: 0x0600F4CE RID: 62670
		[DispId(-2147416095)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void addFilter([MarshalAs(25)] [In] object pUnk);

		// Token: 0x0600F4CF RID: 62671
		[DispId(-2147416094)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void removeFilter([MarshalAs(25)] [In] object pUnk);

		// Token: 0x17004FD6 RID: 20438
		// (get) Token: 0x0600F4D0 RID: 62672
		[DispId(-2147416093)]
		public virtual extern int clientHeight
		{
			[DispId(-2147416093)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17004FD7 RID: 20439
		// (get) Token: 0x0600F4D1 RID: 62673
		[DispId(-2147416092)]
		public virtual extern int clientWidth
		{
			[TypeLibFunc(20)]
			[DispId(-2147416092)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17004FD8 RID: 20440
		// (get) Token: 0x0600F4D2 RID: 62674
		[DispId(-2147416091)]
		public virtual extern int clientTop
		{
			[DispId(-2147416091)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17004FD9 RID: 20441
		// (get) Token: 0x0600F4D3 RID: 62675
		[DispId(-2147416090)]
		public virtual extern int clientLeft
		{
			[DispId(-2147416090)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x0600F4D4 RID: 62676
		[DispId(-2147417605)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool attachEvent([MarshalAs(19)] [In] string @event, [MarshalAs(26)] [In] object pdisp);

		// Token: 0x0600F4D5 RID: 62677
		[DispId(-2147417604)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void detachEvent([MarshalAs(19)] [In] string @event, [MarshalAs(26)] [In] object pdisp);

		// Token: 0x17004FDA RID: 20442
		// (get) Token: 0x0600F4D6 RID: 62678
		[DispId(-2147412996)]
		public virtual extern object readyState
		{
			[DispId(-2147412996)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x17004FDB RID: 20443
		// (get) Token: 0x0600F4D8 RID: 62680
		// (set) Token: 0x0600F4D7 RID: 62679
		[DispId(-2147412087)]
		public virtual extern object onreadystatechange
		{
			[TypeLibFunc(20)]
			[DispId(-2147412087)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412087)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17004FDC RID: 20444
		// (get) Token: 0x0600F4DA RID: 62682
		// (set) Token: 0x0600F4D9 RID: 62681
		[DispId(-2147412050)]
		public virtual extern object onrowsdelete
		{
			[DispId(-2147412050)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412050)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17004FDD RID: 20445
		// (get) Token: 0x0600F4DC RID: 62684
		// (set) Token: 0x0600F4DB RID: 62683
		[DispId(-2147412049)]
		public virtual extern object onrowsinserted
		{
			[TypeLibFunc(20)]
			[DispId(-2147412049)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412049)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17004FDE RID: 20446
		// (get) Token: 0x0600F4DE RID: 62686
		// (set) Token: 0x0600F4DD RID: 62685
		[DispId(-2147412048)]
		public virtual extern object oncellchange
		{
			[TypeLibFunc(20)]
			[DispId(-2147412048)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412048)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17004FDF RID: 20447
		// (get) Token: 0x0600F4E0 RID: 62688
		// (set) Token: 0x0600F4DF RID: 62687
		[DispId(-2147412995)]
		public virtual extern string dir
		{
			[DispId(-2147412995)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147412995)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x0600F4E1 RID: 62689
		[DispId(-2147417056)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(26)]
		public virtual extern object createControlRange();

		// Token: 0x17004FE0 RID: 20448
		// (get) Token: 0x0600F4E2 RID: 62690
		[DispId(-2147417055)]
		public virtual extern int scrollHeight
		{
			[TypeLibFunc(20)]
			[DispId(-2147417055)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17004FE1 RID: 20449
		// (get) Token: 0x0600F4E3 RID: 62691
		[DispId(-2147417054)]
		public virtual extern int scrollWidth
		{
			[TypeLibFunc(20)]
			[DispId(-2147417054)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17004FE2 RID: 20450
		// (get) Token: 0x0600F4E5 RID: 62693
		// (set) Token: 0x0600F4E4 RID: 62692
		[DispId(-2147417053)]
		public virtual extern int scrollTop
		{
			[DispId(-2147417053)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[DispId(-2147417053)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x17004FE3 RID: 20451
		// (get) Token: 0x0600F4E7 RID: 62695
		// (set) Token: 0x0600F4E6 RID: 62694
		[DispId(-2147417052)]
		public virtual extern int scrollLeft
		{
			[DispId(-2147417052)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147417052)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x0600F4E8 RID: 62696
		[DispId(-2147417050)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void clearAttributes();

		// Token: 0x17004FE4 RID: 20452
		// (get) Token: 0x0600F4EA RID: 62698
		// (set) Token: 0x0600F4E9 RID: 62697
		[DispId(-2147412047)]
		public virtual extern object oncontextmenu
		{
			[DispId(-2147412047)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412047)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x0600F4EB RID: 62699
		[DispId(-2147417043)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElement insertAdjacentElement([MarshalAs(19)] [In] string where, [MarshalAs(28)] [In] IHTMLElement insertedElement);

		// Token: 0x0600F4EC RID: 62700
		[DispId(-2147417047)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElement applyElement([MarshalAs(28)] [In] IHTMLElement apply, [MarshalAs(19)] [In] string where);

		// Token: 0x0600F4ED RID: 62701
		[DispId(-2147417042)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string getAdjacentText([MarshalAs(19)] [In] string where);

		// Token: 0x0600F4EE RID: 62702
		[DispId(-2147417041)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string replaceAdjacentText([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string newText);

		// Token: 0x17004FE5 RID: 20453
		// (get) Token: 0x0600F4EF RID: 62703
		[DispId(-2147417040)]
		public virtual extern bool canHaveChildren
		{
			[DispId(-2147417040)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x0600F4F0 RID: 62704
		[DispId(-2147417032)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern int addBehavior([MarshalAs(19)] [In] string bstrUrl, [MarshalAs(27)] [In] [Optional] ref object pvarFactory);

		// Token: 0x0600F4F1 RID: 62705
		[DispId(-2147417031)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool removeBehavior([In] int cookie);

		// Token: 0x17004FE6 RID: 20454
		// (get) Token: 0x0600F4F2 RID: 62706
		[DispId(-2147417048)]
		public virtual extern IHTMLStyle runtimeStyle
		{
			[TypeLibFunc(1024)]
			[DispId(-2147417048)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17004FE7 RID: 20455
		// (get) Token: 0x0600F4F3 RID: 62707
		[DispId(-2147417030)]
		public virtual extern object behaviorUrns
		{
			[DispId(-2147417030)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17004FE8 RID: 20456
		// (get) Token: 0x0600F4F5 RID: 62709
		// (set) Token: 0x0600F4F4 RID: 62708
		[DispId(-2147417029)]
		public virtual extern string tagUrn
		{
			[DispId(-2147417029)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147417029)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17004FE9 RID: 20457
		// (get) Token: 0x0600F4F7 RID: 62711
		// (set) Token: 0x0600F4F6 RID: 62710
		[DispId(-2147412043)]
		public virtual extern object onbeforeeditfocus
		{
			[DispId(-2147412043)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412043)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17004FEA RID: 20458
		// (get) Token: 0x0600F4F8 RID: 62712
		[DispId(-2147417028)]
		public virtual extern int readyStateValue
		{
			[TypeLibFunc(65)]
			[DispId(-2147417028)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x0600F4F9 RID: 62713
		[DispId(-2147417027)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElementCollection getElementsByTagName([MarshalAs(19)] [In] string v);

		// Token: 0x0600F4FA RID: 62714
		[DispId(-2147417016)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void mergeAttributes([MarshalAs(28)] [In] IHTMLElement mergeThis, [MarshalAs(27)] [In] [Optional] ref object pvarFlags);

		// Token: 0x17004FEB RID: 20459
		// (get) Token: 0x0600F4FB RID: 62715
		[DispId(-2147417015)]
		public virtual extern bool isMultiLine
		{
			[DispId(-2147417015)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17004FEC RID: 20460
		// (get) Token: 0x0600F4FC RID: 62716
		[DispId(-2147417014)]
		public virtual extern bool canHaveHTML
		{
			[DispId(-2147417014)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17004FED RID: 20461
		// (get) Token: 0x0600F4FE RID: 62718
		// (set) Token: 0x0600F4FD RID: 62717
		[DispId(-2147412039)]
		public virtual extern object onlayoutcomplete
		{
			[TypeLibFunc(20)]
			[DispId(-2147412039)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412039)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17004FEE RID: 20462
		// (get) Token: 0x0600F500 RID: 62720
		// (set) Token: 0x0600F4FF RID: 62719
		[DispId(-2147412038)]
		public virtual extern object onpage
		{
			[TypeLibFunc(20)]
			[DispId(-2147412038)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412038)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17004FEF RID: 20463
		// (get) Token: 0x0600F502 RID: 62722
		// (set) Token: 0x0600F501 RID: 62721
		[DispId(-2147417012)]
		public virtual extern bool inflateBlock
		{
			[DispId(-2147417012)]
			[TypeLibFunc(1089)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[TypeLibFunc(1089)]
			[DispId(-2147417012)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x17004FF0 RID: 20464
		// (get) Token: 0x0600F504 RID: 62724
		// (set) Token: 0x0600F503 RID: 62723
		[DispId(-2147412035)]
		public virtual extern object onbeforedeactivate
		{
			[DispId(-2147412035)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412035)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x0600F505 RID: 62725
		[DispId(-2147417011)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void setActive();

		// Token: 0x17004FF1 RID: 20465
		// (get) Token: 0x0600F507 RID: 62727
		// (set) Token: 0x0600F506 RID: 62726
		[DispId(-2147412950)]
		public virtual extern string contentEditable
		{
			[DispId(-2147412950)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412950)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17004FF2 RID: 20466
		// (get) Token: 0x0600F508 RID: 62728
		[DispId(-2147417010)]
		public virtual extern bool isContentEditable
		{
			[DispId(-2147417010)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17004FF3 RID: 20467
		// (get) Token: 0x0600F50A RID: 62730
		// (set) Token: 0x0600F509 RID: 62729
		[DispId(-2147412949)]
		public virtual extern bool hideFocus
		{
			[DispId(-2147412949)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[DispId(-2147412949)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x17004FF4 RID: 20468
		// (get) Token: 0x0600F50C RID: 62732
		// (set) Token: 0x0600F50B RID: 62731
		[DispId(-2147418036)]
		public virtual extern bool disabled
		{
			[TypeLibFunc(20)]
			[DispId(-2147418036)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[DispId(-2147418036)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x17004FF5 RID: 20469
		// (get) Token: 0x0600F50D RID: 62733
		[DispId(-2147417007)]
		public virtual extern bool isDisabled
		{
			[DispId(-2147417007)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17004FF6 RID: 20470
		// (get) Token: 0x0600F50F RID: 62735
		// (set) Token: 0x0600F50E RID: 62734
		[DispId(-2147412034)]
		public virtual extern object onmove
		{
			[DispId(-2147412034)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412034)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17004FF7 RID: 20471
		// (get) Token: 0x0600F511 RID: 62737
		// (set) Token: 0x0600F510 RID: 62736
		[DispId(-2147412033)]
		public virtual extern object oncontrolselect
		{
			[TypeLibFunc(20)]
			[DispId(-2147412033)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412033)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x0600F512 RID: 62738
		[DispId(-2147417006)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool FireEvent([MarshalAs(19)] [In] string bstrEventName, [MarshalAs(27)] [In] [Optional] ref object pvarEventObject);

		// Token: 0x17004FF8 RID: 20472
		// (get) Token: 0x0600F514 RID: 62740
		// (set) Token: 0x0600F513 RID: 62739
		[DispId(-2147412029)]
		public virtual extern object onresizestart
		{
			[DispId(-2147412029)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412029)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17004FF9 RID: 20473
		// (get) Token: 0x0600F516 RID: 62742
		// (set) Token: 0x0600F515 RID: 62741
		[DispId(-2147412028)]
		public virtual extern object onresizeend
		{
			[DispId(-2147412028)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412028)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17004FFA RID: 20474
		// (get) Token: 0x0600F518 RID: 62744
		// (set) Token: 0x0600F517 RID: 62743
		[DispId(-2147412031)]
		public virtual extern object onmovestart
		{
			[DispId(-2147412031)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412031)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17004FFB RID: 20475
		// (get) Token: 0x0600F51A RID: 62746
		// (set) Token: 0x0600F519 RID: 62745
		[DispId(-2147412030)]
		public virtual extern object onmoveend
		{
			[DispId(-2147412030)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412030)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17004FFC RID: 20476
		// (get) Token: 0x0600F51C RID: 62748
		// (set) Token: 0x0600F51B RID: 62747
		[DispId(-2147412027)]
		public virtual extern object onmouseenter
		{
			[DispId(-2147412027)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412027)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17004FFD RID: 20477
		// (get) Token: 0x0600F51E RID: 62750
		// (set) Token: 0x0600F51D RID: 62749
		[DispId(-2147412026)]
		public virtual extern object onmouseleave
		{
			[TypeLibFunc(20)]
			[DispId(-2147412026)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412026)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17004FFE RID: 20478
		// (get) Token: 0x0600F520 RID: 62752
		// (set) Token: 0x0600F51F RID: 62751
		[DispId(-2147412025)]
		public virtual extern object onactivate
		{
			[DispId(-2147412025)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412025)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17004FFF RID: 20479
		// (get) Token: 0x0600F522 RID: 62754
		// (set) Token: 0x0600F521 RID: 62753
		[DispId(-2147412024)]
		public virtual extern object ondeactivate
		{
			[TypeLibFunc(20)]
			[DispId(-2147412024)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412024)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x0600F523 RID: 62755
		[DispId(-2147417005)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool dragDrop();

		// Token: 0x17005000 RID: 20480
		// (get) Token: 0x0600F524 RID: 62756
		[DispId(-2147417004)]
		public virtual extern int glyphMode
		{
			[TypeLibFunc(1089)]
			[DispId(-2147417004)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17005001 RID: 20481
		// (get) Token: 0x0600F526 RID: 62758
		// (set) Token: 0x0600F525 RID: 62757
		[DispId(-2147412036)]
		public virtual extern object onmousewheel
		{
			[DispId(-2147412036)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412036)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x0600F527 RID: 62759
		[DispId(-2147417000)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void normalize();

		// Token: 0x0600F528 RID: 62760
		[DispId(-2147417003)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute getAttributeNode([MarshalAs(19)] [In] string bstrName);

		// Token: 0x0600F529 RID: 62761
		[DispId(-2147417002)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute setAttributeNode([MarshalAs(28)] [In] IHTMLDOMAttribute pattr);

		// Token: 0x0600F52A RID: 62762
		[DispId(-2147417001)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute removeAttributeNode([MarshalAs(28)] [In] IHTMLDOMAttribute pattr);

		// Token: 0x17005002 RID: 20482
		// (get) Token: 0x0600F52C RID: 62764
		// (set) Token: 0x0600F52B RID: 62763
		[DispId(-2147412022)]
		public virtual extern object onbeforeactivate
		{
			[DispId(-2147412022)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412022)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17005003 RID: 20483
		// (get) Token: 0x0600F52E RID: 62766
		// (set) Token: 0x0600F52D RID: 62765
		[DispId(-2147412021)]
		public virtual extern object onfocusin
		{
			[DispId(-2147412021)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412021)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17005004 RID: 20484
		// (get) Token: 0x0600F530 RID: 62768
		// (set) Token: 0x0600F52F RID: 62767
		[DispId(-2147412020)]
		public virtual extern object onfocusout
		{
			[TypeLibFunc(20)]
			[DispId(-2147412020)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412020)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17005005 RID: 20485
		// (get) Token: 0x0600F531 RID: 62769
		[DispId(-2147417058)]
		public virtual extern int uniqueNumber
		{
			[DispId(-2147417058)]
			[TypeLibFunc(64)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17005006 RID: 20486
		// (get) Token: 0x0600F532 RID: 62770
		[DispId(-2147417057)]
		public virtual extern string uniqueID
		{
			[DispId(-2147417057)]
			[TypeLibFunc(64)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17005007 RID: 20487
		// (get) Token: 0x0600F533 RID: 62771
		[DispId(-2147417066)]
		public virtual extern int nodeType
		{
			[DispId(-2147417066)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17005008 RID: 20488
		// (get) Token: 0x0600F534 RID: 62772
		[DispId(-2147417065)]
		public virtual extern IHTMLDOMNode parentNode
		{
			[DispId(-2147417065)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x0600F535 RID: 62773
		[DispId(-2147417064)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool hasChildNodes();

		// Token: 0x17005009 RID: 20489
		// (get) Token: 0x0600F536 RID: 62774
		[DispId(-2147417063)]
		public virtual extern object childNodes
		{
			[DispId(-2147417063)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x1700500A RID: 20490
		// (get) Token: 0x0600F537 RID: 62775
		[DispId(-2147417062)]
		public virtual extern object attributes
		{
			[DispId(-2147417062)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x0600F538 RID: 62776
		[DispId(-2147417061)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode insertBefore([MarshalAs(28)] [In] IHTMLDOMNode newChild, [MarshalAs(27)] [In] [Optional] object refChild);

		// Token: 0x0600F539 RID: 62777
		[DispId(-2147417060)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode removeChild([MarshalAs(28)] [In] IHTMLDOMNode oldChild);

		// Token: 0x0600F53A RID: 62778
		[DispId(-2147417059)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode replaceChild([MarshalAs(28)] [In] IHTMLDOMNode newChild, [MarshalAs(28)] [In] IHTMLDOMNode oldChild);

		// Token: 0x0600F53B RID: 62779
		[DispId(-2147417051)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode cloneNode([In] bool fDeep);

		// Token: 0x0600F53C RID: 62780
		[DispId(-2147417046)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode removeNode([In] bool fDeep = false);

		// Token: 0x0600F53D RID: 62781
		[DispId(-2147417044)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode swapNode([MarshalAs(28)] [In] IHTMLDOMNode otherNode);

		// Token: 0x0600F53E RID: 62782
		[DispId(-2147417045)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode replaceNode([MarshalAs(28)] [In] IHTMLDOMNode replacement);

		// Token: 0x0600F53F RID: 62783
		[DispId(-2147417039)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode appendChild([MarshalAs(28)] [In] IHTMLDOMNode newChild);

		// Token: 0x1700500B RID: 20491
		// (get) Token: 0x0600F540 RID: 62784
		[DispId(-2147417038)]
		public virtual extern string nodeName
		{
			[DispId(-2147417038)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x1700500C RID: 20492
		// (get) Token: 0x0600F542 RID: 62786
		// (set) Token: 0x0600F541 RID: 62785
		[DispId(-2147417037)]
		public virtual extern object nodeValue
		{
			[DispId(-2147417037)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147417037)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x1700500D RID: 20493
		// (get) Token: 0x0600F543 RID: 62787
		[DispId(-2147417036)]
		public virtual extern IHTMLDOMNode firstChild
		{
			[DispId(-2147417036)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x1700500E RID: 20494
		// (get) Token: 0x0600F544 RID: 62788
		[DispId(-2147417035)]
		public virtual extern IHTMLDOMNode lastChild
		{
			[DispId(-2147417035)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x1700500F RID: 20495
		// (get) Token: 0x0600F545 RID: 62789
		[DispId(-2147417034)]
		public virtual extern IHTMLDOMNode previousSibling
		{
			[DispId(-2147417034)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17005010 RID: 20496
		// (get) Token: 0x0600F546 RID: 62790
		[DispId(-2147417033)]
		public virtual extern IHTMLDOMNode nextSibling
		{
			[DispId(-2147417033)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17005011 RID: 20497
		// (get) Token: 0x0600F547 RID: 62791
		[DispId(-2147416999)]
		public virtual extern object ownerDocument
		{
			[DispId(-2147416999)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17005012 RID: 20498
		// (get) Token: 0x0600F549 RID: 62793
		// (set) Token: 0x0600F548 RID: 62792
		[DispId(-2147418040)]
		public virtual extern string align
		{
			[DispId(-2147418040)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147418040)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17005013 RID: 20499
		// (get) Token: 0x0600F54B RID: 62795
		// (set) Token: 0x0600F54A RID: 62794
		[DispId(-2147413081)]
		public virtual extern string vAlign
		{
			[DispId(-2147413081)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147413081)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x0600F54C RID: 62796
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement_setAttribute([MarshalAs(19)] [In] string strAttributeName, [MarshalAs(27)] [In] object AttributeValue, [In] int lFlags = 1);

		// Token: 0x0600F54D RID: 62797
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		public virtual extern object IHTMLElement_getAttribute([MarshalAs(19)] [In] string strAttributeName, [In] int lFlags = 0);

		// Token: 0x0600F54E RID: 62798
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement_removeAttribute([MarshalAs(19)] [In] string strAttributeName, [In] int lFlags = 1);

		// Token: 0x17005014 RID: 20500
		// (get) Token: 0x0600F550 RID: 62800
		// (set) Token: 0x0600F54F RID: 62799
		public virtual extern string IHTMLElement_className
		{
			[TypeLibFunc(4)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(4)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17005015 RID: 20501
		// (get) Token: 0x0600F552 RID: 62802
		// (set) Token: 0x0600F551 RID: 62801
		public virtual extern string IHTMLElement_id
		{
			[TypeLibFunc(4)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(4)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17005016 RID: 20502
		// (get) Token: 0x0600F553 RID: 62803
		public virtual extern string IHTMLElement_tagName
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17005017 RID: 20503
		// (get) Token: 0x0600F554 RID: 62804
		public virtual extern IHTMLElement IHTMLElement_parentElement
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17005018 RID: 20504
		// (get) Token: 0x0600F555 RID: 62805
		public virtual extern IHTMLStyle IHTMLElement_style
		{
			[TypeLibFunc(1024)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17005019 RID: 20505
		// (get) Token: 0x0600F557 RID: 62807
		// (set) Token: 0x0600F556 RID: 62806
		public virtual extern object IHTMLElement_onhelp
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x1700501A RID: 20506
		// (get) Token: 0x0600F559 RID: 62809
		// (set) Token: 0x0600F558 RID: 62808
		public virtual extern object IHTMLElement_onclick
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x1700501B RID: 20507
		// (get) Token: 0x0600F55B RID: 62811
		// (set) Token: 0x0600F55A RID: 62810
		public virtual extern object IHTMLElement_ondblclick
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x1700501C RID: 20508
		// (get) Token: 0x0600F55D RID: 62813
		// (set) Token: 0x0600F55C RID: 62812
		public virtual extern object IHTMLElement_onkeydown
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x1700501D RID: 20509
		// (get) Token: 0x0600F55F RID: 62815
		// (set) Token: 0x0600F55E RID: 62814
		public virtual extern object IHTMLElement_onkeyup
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x1700501E RID: 20510
		// (get) Token: 0x0600F561 RID: 62817
		// (set) Token: 0x0600F560 RID: 62816
		public virtual extern object IHTMLElement_onkeypress
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x1700501F RID: 20511
		// (get) Token: 0x0600F563 RID: 62819
		// (set) Token: 0x0600F562 RID: 62818
		public virtual extern object IHTMLElement_onmouseout
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17005020 RID: 20512
		// (get) Token: 0x0600F565 RID: 62821
		// (set) Token: 0x0600F564 RID: 62820
		public virtual extern object IHTMLElement_onmouseover
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17005021 RID: 20513
		// (get) Token: 0x0600F567 RID: 62823
		// (set) Token: 0x0600F566 RID: 62822
		public virtual extern object IHTMLElement_onmousemove
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17005022 RID: 20514
		// (get) Token: 0x0600F569 RID: 62825
		// (set) Token: 0x0600F568 RID: 62824
		public virtual extern object IHTMLElement_onmousedown
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17005023 RID: 20515
		// (get) Token: 0x0600F56B RID: 62827
		// (set) Token: 0x0600F56A RID: 62826
		public virtual extern object IHTMLElement_onmouseup
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17005024 RID: 20516
		// (get) Token: 0x0600F56C RID: 62828
		public virtual extern object IHTMLElement_document
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17005025 RID: 20517
		// (get) Token: 0x0600F56E RID: 62830
		// (set) Token: 0x0600F56D RID: 62829
		public virtual extern string IHTMLElement_title
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17005026 RID: 20518
		// (get) Token: 0x0600F570 RID: 62832
		// (set) Token: 0x0600F56F RID: 62831
		public virtual extern string IHTMLElement_language
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17005027 RID: 20519
		// (get) Token: 0x0600F572 RID: 62834
		// (set) Token: 0x0600F571 RID: 62833
		public virtual extern object IHTMLElement_onselectstart
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x0600F573 RID: 62835
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement_scrollIntoView([MarshalAs(27)] [In] [Optional] object varargStart);

		// Token: 0x0600F574 RID: 62836
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement_contains([MarshalAs(28)] [In] IHTMLElement pChild);

		// Token: 0x17005028 RID: 20520
		// (get) Token: 0x0600F575 RID: 62837
		public virtual extern int IHTMLElement_sourceIndex
		{
			[TypeLibFunc(4)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17005029 RID: 20521
		// (get) Token: 0x0600F576 RID: 62838
		public virtual extern object IHTMLElement_recordNumber
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x1700502A RID: 20522
		// (get) Token: 0x0600F578 RID: 62840
		// (set) Token: 0x0600F577 RID: 62839
		public virtual extern string IHTMLElement_lang
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x1700502B RID: 20523
		// (get) Token: 0x0600F579 RID: 62841
		public virtual extern int IHTMLElement_offsetLeft
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700502C RID: 20524
		// (get) Token: 0x0600F57A RID: 62842
		public virtual extern int IHTMLElement_offsetTop
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700502D RID: 20525
		// (get) Token: 0x0600F57B RID: 62843
		public virtual extern int IHTMLElement_offsetWidth
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700502E RID: 20526
		// (get) Token: 0x0600F57C RID: 62844
		public virtual extern int IHTMLElement_offsetHeight
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700502F RID: 20527
		// (get) Token: 0x0600F57D RID: 62845
		public virtual extern IHTMLElement IHTMLElement_offsetParent
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17005030 RID: 20528
		// (get) Token: 0x0600F57F RID: 62847
		// (set) Token: 0x0600F57E RID: 62846
		public virtual extern string IHTMLElement_innerHTML
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17005031 RID: 20529
		// (get) Token: 0x0600F581 RID: 62849
		// (set) Token: 0x0600F580 RID: 62848
		public virtual extern string IHTMLElement_innerText
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17005032 RID: 20530
		// (get) Token: 0x0600F583 RID: 62851
		// (set) Token: 0x0600F582 RID: 62850
		public virtual extern string IHTMLElement_outerHTML
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17005033 RID: 20531
		// (get) Token: 0x0600F585 RID: 62853
		// (set) Token: 0x0600F584 RID: 62852
		public virtual extern string IHTMLElement_outerText
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x0600F586 RID: 62854
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement_insertAdjacentHTML([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string html);

		// Token: 0x0600F587 RID: 62855
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement_insertAdjacentText([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string text);

		// Token: 0x17005034 RID: 20532
		// (get) Token: 0x0600F588 RID: 62856
		public virtual extern IHTMLElement IHTMLElement_parentTextEdit
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17005035 RID: 20533
		// (get) Token: 0x0600F589 RID: 62857
		public virtual extern bool IHTMLElement_isTextEdit
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x0600F58A RID: 62858
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement_click();

		// Token: 0x17005036 RID: 20534
		// (get) Token: 0x0600F58B RID: 62859
		public virtual extern IHTMLFiltersCollection IHTMLElement_filters
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17005037 RID: 20535
		// (get) Token: 0x0600F58D RID: 62861
		// (set) Token: 0x0600F58C RID: 62860
		public virtual extern object IHTMLElement_ondragstart
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x0600F58E RID: 62862
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string IHTMLElement_toString();

		// Token: 0x17005038 RID: 20536
		// (get) Token: 0x0600F590 RID: 62864
		// (set) Token: 0x0600F58F RID: 62863
		public virtual extern object IHTMLElement_onbeforeupdate
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17005039 RID: 20537
		// (get) Token: 0x0600F592 RID: 62866
		// (set) Token: 0x0600F591 RID: 62865
		public virtual extern object IHTMLElement_onafterupdate
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x1700503A RID: 20538
		// (get) Token: 0x0600F594 RID: 62868
		// (set) Token: 0x0600F593 RID: 62867
		public virtual extern object IHTMLElement_onerrorupdate
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x1700503B RID: 20539
		// (get) Token: 0x0600F596 RID: 62870
		// (set) Token: 0x0600F595 RID: 62869
		public virtual extern object IHTMLElement_onrowexit
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x1700503C RID: 20540
		// (get) Token: 0x0600F598 RID: 62872
		// (set) Token: 0x0600F597 RID: 62871
		public virtual extern object IHTMLElement_onrowenter
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x1700503D RID: 20541
		// (get) Token: 0x0600F59A RID: 62874
		// (set) Token: 0x0600F599 RID: 62873
		public virtual extern object IHTMLElement_ondatasetchanged
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x1700503E RID: 20542
		// (get) Token: 0x0600F59C RID: 62876
		// (set) Token: 0x0600F59B RID: 62875
		public virtual extern object IHTMLElement_ondataavailable
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x1700503F RID: 20543
		// (get) Token: 0x0600F59E RID: 62878
		// (set) Token: 0x0600F59D RID: 62877
		public virtual extern object IHTMLElement_ondatasetcomplete
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17005040 RID: 20544
		// (get) Token: 0x0600F5A0 RID: 62880
		// (set) Token: 0x0600F59F RID: 62879
		public virtual extern object IHTMLElement_onfilterchange
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17005041 RID: 20545
		// (get) Token: 0x0600F5A1 RID: 62881
		public virtual extern object IHTMLElement_children
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17005042 RID: 20546
		// (get) Token: 0x0600F5A2 RID: 62882
		public virtual extern object IHTMLElement_all
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17005043 RID: 20547
		// (get) Token: 0x0600F5A3 RID: 62883
		public virtual extern string IHTMLElement2_scopeName
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x0600F5A4 RID: 62884
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_setCapture([In] bool containerCapture = true);

		// Token: 0x0600F5A5 RID: 62885
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_releaseCapture();

		// Token: 0x17005044 RID: 20548
		// (get) Token: 0x0600F5A7 RID: 62887
		// (set) Token: 0x0600F5A6 RID: 62886
		public virtual extern object IHTMLElement2_onlosecapture
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x0600F5A8 RID: 62888
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string IHTMLElement2_componentFromPoint([In] int x, [In] int y);

		// Token: 0x0600F5A9 RID: 62889
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_doScroll([MarshalAs(27)] [In] [Optional] object component);

		// Token: 0x17005045 RID: 20549
		// (get) Token: 0x0600F5AB RID: 62891
		// (set) Token: 0x0600F5AA RID: 62890
		public virtual extern object IHTMLElement2_onscroll
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17005046 RID: 20550
		// (get) Token: 0x0600F5AD RID: 62893
		// (set) Token: 0x0600F5AC RID: 62892
		public virtual extern object IHTMLElement2_ondrag
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17005047 RID: 20551
		// (get) Token: 0x0600F5AF RID: 62895
		// (set) Token: 0x0600F5AE RID: 62894
		public virtual extern object IHTMLElement2_ondragend
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17005048 RID: 20552
		// (get) Token: 0x0600F5B1 RID: 62897
		// (set) Token: 0x0600F5B0 RID: 62896
		public virtual extern object IHTMLElement2_ondragenter
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17005049 RID: 20553
		// (get) Token: 0x0600F5B3 RID: 62899
		// (set) Token: 0x0600F5B2 RID: 62898
		public virtual extern object IHTMLElement2_ondragover
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x1700504A RID: 20554
		// (get) Token: 0x0600F5B5 RID: 62901
		// (set) Token: 0x0600F5B4 RID: 62900
		public virtual extern object IHTMLElement2_ondragleave
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x1700504B RID: 20555
		// (get) Token: 0x0600F5B7 RID: 62903
		// (set) Token: 0x0600F5B6 RID: 62902
		public virtual extern object IHTMLElement2_ondrop
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x1700504C RID: 20556
		// (get) Token: 0x0600F5B9 RID: 62905
		// (set) Token: 0x0600F5B8 RID: 62904
		public virtual extern object IHTMLElement2_onbeforecut
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x1700504D RID: 20557
		// (get) Token: 0x0600F5BB RID: 62907
		// (set) Token: 0x0600F5BA RID: 62906
		public virtual extern object IHTMLElement2_oncut
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x1700504E RID: 20558
		// (get) Token: 0x0600F5BD RID: 62909
		// (set) Token: 0x0600F5BC RID: 62908
		public virtual extern object IHTMLElement2_onbeforecopy
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x1700504F RID: 20559
		// (get) Token: 0x0600F5BF RID: 62911
		// (set) Token: 0x0600F5BE RID: 62910
		public virtual extern object IHTMLElement2_oncopy
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17005050 RID: 20560
		// (get) Token: 0x0600F5C1 RID: 62913
		// (set) Token: 0x0600F5C0 RID: 62912
		public virtual extern object IHTMLElement2_onbeforepaste
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17005051 RID: 20561
		// (get) Token: 0x0600F5C3 RID: 62915
		// (set) Token: 0x0600F5C2 RID: 62914
		public virtual extern object IHTMLElement2_onpaste
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17005052 RID: 20562
		// (get) Token: 0x0600F5C4 RID: 62916
		public virtual extern IHTMLCurrentStyle IHTMLElement2_currentStyle
		{
			[TypeLibFunc(1024)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17005053 RID: 20563
		// (get) Token: 0x0600F5C6 RID: 62918
		// (set) Token: 0x0600F5C5 RID: 62917
		public virtual extern object IHTMLElement2_onpropertychange
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x0600F5C7 RID: 62919
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLRectCollection IHTMLElement2_getClientRects();

		// Token: 0x0600F5C8 RID: 62920
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLRect IHTMLElement2_getBoundingClientRect();

		// Token: 0x0600F5C9 RID: 62921
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_setExpression([MarshalAs(19)] [In] string propname, [MarshalAs(19)] [In] string expression, [MarshalAs(19)] [In] string language = "");

		// Token: 0x0600F5CA RID: 62922
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		public virtual extern object IHTMLElement2_getExpression([MarshalAs(19)] [In] string propname);

		// Token: 0x0600F5CB RID: 62923
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement2_removeExpression([MarshalAs(19)] [In] string propname);

		// Token: 0x17005054 RID: 20564
		// (get) Token: 0x0600F5CD RID: 62925
		// (set) Token: 0x0600F5CC RID: 62924
		public virtual extern short IHTMLElement2_tabIndex
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x0600F5CE RID: 62926
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_focus();

		// Token: 0x17005055 RID: 20565
		// (get) Token: 0x0600F5D0 RID: 62928
		// (set) Token: 0x0600F5CF RID: 62927
		public virtual extern string IHTMLElement2_accessKey
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17005056 RID: 20566
		// (get) Token: 0x0600F5D2 RID: 62930
		// (set) Token: 0x0600F5D1 RID: 62929
		public virtual extern object IHTMLElement2_onblur
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17005057 RID: 20567
		// (get) Token: 0x0600F5D4 RID: 62932
		// (set) Token: 0x0600F5D3 RID: 62931
		public virtual extern object IHTMLElement2_onfocus
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17005058 RID: 20568
		// (get) Token: 0x0600F5D6 RID: 62934
		// (set) Token: 0x0600F5D5 RID: 62933
		public virtual extern object IHTMLElement2_onresize
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x0600F5D7 RID: 62935
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_blur();

		// Token: 0x0600F5D8 RID: 62936
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_addFilter([MarshalAs(25)] [In] object pUnk);

		// Token: 0x0600F5D9 RID: 62937
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_removeFilter([MarshalAs(25)] [In] object pUnk);

		// Token: 0x17005059 RID: 20569
		// (get) Token: 0x0600F5DA RID: 62938
		public virtual extern int IHTMLElement2_clientHeight
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700505A RID: 20570
		// (get) Token: 0x0600F5DB RID: 62939
		public virtual extern int IHTMLElement2_clientWidth
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700505B RID: 20571
		// (get) Token: 0x0600F5DC RID: 62940
		public virtual extern int IHTMLElement2_clientTop
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700505C RID: 20572
		// (get) Token: 0x0600F5DD RID: 62941
		public virtual extern int IHTMLElement2_clientLeft
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x0600F5DE RID: 62942
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement2_attachEvent([MarshalAs(19)] [In] string @event, [MarshalAs(26)] [In] object pdisp);

		// Token: 0x0600F5DF RID: 62943
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_detachEvent([MarshalAs(19)] [In] string @event, [MarshalAs(26)] [In] object pdisp);

		// Token: 0x1700505D RID: 20573
		// (get) Token: 0x0600F5E0 RID: 62944
		public virtual extern object IHTMLElement2_readyState
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x1700505E RID: 20574
		// (get) Token: 0x0600F5E2 RID: 62946
		// (set) Token: 0x0600F5E1 RID: 62945
		public virtual extern object IHTMLElement2_onreadystatechange
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x1700505F RID: 20575
		// (get) Token: 0x0600F5E4 RID: 62948
		// (set) Token: 0x0600F5E3 RID: 62947
		public virtual extern object IHTMLElement2_onrowsdelete
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17005060 RID: 20576
		// (get) Token: 0x0600F5E6 RID: 62950
		// (set) Token: 0x0600F5E5 RID: 62949
		public virtual extern object IHTMLElement2_onrowsinserted
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17005061 RID: 20577
		// (get) Token: 0x0600F5E8 RID: 62952
		// (set) Token: 0x0600F5E7 RID: 62951
		public virtual extern object IHTMLElement2_oncellchange
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17005062 RID: 20578
		// (get) Token: 0x0600F5EA RID: 62954
		// (set) Token: 0x0600F5E9 RID: 62953
		public virtual extern string IHTMLElement2_dir
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x0600F5EB RID: 62955
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(26)]
		public virtual extern object IHTMLElement2_createControlRange();

		// Token: 0x17005063 RID: 20579
		// (get) Token: 0x0600F5EC RID: 62956
		public virtual extern int IHTMLElement2_scrollHeight
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17005064 RID: 20580
		// (get) Token: 0x0600F5ED RID: 62957
		public virtual extern int IHTMLElement2_scrollWidth
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17005065 RID: 20581
		// (get) Token: 0x0600F5EF RID: 62959
		// (set) Token: 0x0600F5EE RID: 62958
		public virtual extern int IHTMLElement2_scrollTop
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x17005066 RID: 20582
		// (get) Token: 0x0600F5F1 RID: 62961
		// (set) Token: 0x0600F5F0 RID: 62960
		public virtual extern int IHTMLElement2_scrollLeft
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x0600F5F2 RID: 62962
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_clearAttributes();

		// Token: 0x0600F5F3 RID: 62963
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void mergeAttributes([MarshalAs(28)] [In] IHTMLElement mergeThis);

		// Token: 0x17005067 RID: 20583
		// (get) Token: 0x0600F5F5 RID: 62965
		// (set) Token: 0x0600F5F4 RID: 62964
		public virtual extern object IHTMLElement2_oncontextmenu
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x0600F5F6 RID: 62966
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElement IHTMLElement2_insertAdjacentElement([MarshalAs(19)] [In] string where, [MarshalAs(28)] [In] IHTMLElement insertedElement);

		// Token: 0x0600F5F7 RID: 62967
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElement IHTMLElement2_applyElement([MarshalAs(28)] [In] IHTMLElement apply, [MarshalAs(19)] [In] string where);

		// Token: 0x0600F5F8 RID: 62968
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string IHTMLElement2_getAdjacentText([MarshalAs(19)] [In] string where);

		// Token: 0x0600F5F9 RID: 62969
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string IHTMLElement2_replaceAdjacentText([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string newText);

		// Token: 0x17005068 RID: 20584
		// (get) Token: 0x0600F5FA RID: 62970
		public virtual extern bool IHTMLElement2_canHaveChildren
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x0600F5FB RID: 62971
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern int IHTMLElement2_addBehavior([MarshalAs(19)] [In] string bstrUrl, [MarshalAs(27)] [In] [Optional] ref object pvarFactory);

		// Token: 0x0600F5FC RID: 62972
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement2_removeBehavior([In] int cookie);

		// Token: 0x17005069 RID: 20585
		// (get) Token: 0x0600F5FD RID: 62973
		public virtual extern IHTMLStyle IHTMLElement2_runtimeStyle
		{
			[TypeLibFunc(1024)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x1700506A RID: 20586
		// (get) Token: 0x0600F5FE RID: 62974
		public virtual extern object IHTMLElement2_behaviorUrns
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x1700506B RID: 20587
		// (get) Token: 0x0600F600 RID: 62976
		// (set) Token: 0x0600F5FF RID: 62975
		public virtual extern string IHTMLElement2_tagUrn
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x1700506C RID: 20588
		// (get) Token: 0x0600F602 RID: 62978
		// (set) Token: 0x0600F601 RID: 62977
		public virtual extern object IHTMLElement2_onbeforeeditfocus
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x1700506D RID: 20589
		// (get) Token: 0x0600F603 RID: 62979
		public virtual extern int IHTMLElement2_readyStateValue
		{
			[TypeLibFunc(65)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x0600F604 RID: 62980
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElementCollection IHTMLElement2_getElementsByTagName([MarshalAs(19)] [In] string v);

		// Token: 0x0600F605 RID: 62981
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void mergeAttributes([MarshalAs(28)] [In] IHTMLElement mergeThis, [MarshalAs(27)] [In] [Optional] ref object pvarFlags);

		// Token: 0x1700506E RID: 20590
		// (get) Token: 0x0600F606 RID: 62982
		public virtual extern bool IHTMLElement3_isMultiLine
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700506F RID: 20591
		// (get) Token: 0x0600F607 RID: 62983
		public virtual extern bool IHTMLElement3_canHaveHTML
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17005070 RID: 20592
		// (get) Token: 0x0600F609 RID: 62985
		// (set) Token: 0x0600F608 RID: 62984
		public virtual extern object IHTMLElement3_onlayoutcomplete
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17005071 RID: 20593
		// (get) Token: 0x0600F60B RID: 62987
		// (set) Token: 0x0600F60A RID: 62986
		public virtual extern object IHTMLElement3_onpage
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17005072 RID: 20594
		// (get) Token: 0x0600F60D RID: 62989
		// (set) Token: 0x0600F60C RID: 62988
		public virtual extern bool IHTMLElement3_inflateBlock
		{
			[TypeLibFunc(1089)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[TypeLibFunc(1089)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x17005073 RID: 20595
		// (get) Token: 0x0600F60F RID: 62991
		// (set) Token: 0x0600F60E RID: 62990
		public virtual extern object IHTMLElement3_onbeforedeactivate
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x0600F610 RID: 62992
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement3_setActive();

		// Token: 0x17005074 RID: 20596
		// (get) Token: 0x0600F612 RID: 62994
		// (set) Token: 0x0600F611 RID: 62993
		public virtual extern string IHTMLElement3_contentEditable
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17005075 RID: 20597
		// (get) Token: 0x0600F613 RID: 62995
		public virtual extern bool IHTMLElement3_isContentEditable
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17005076 RID: 20598
		// (get) Token: 0x0600F615 RID: 62997
		// (set) Token: 0x0600F614 RID: 62996
		public virtual extern bool IHTMLElement3_hideFocus
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x17005077 RID: 20599
		// (get) Token: 0x0600F617 RID: 62999
		// (set) Token: 0x0600F616 RID: 62998
		public virtual extern bool IHTMLElement3_disabled
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x17005078 RID: 20600
		// (get) Token: 0x0600F618 RID: 63000
		public virtual extern bool IHTMLElement3_isDisabled
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17005079 RID: 20601
		// (get) Token: 0x0600F61A RID: 63002
		// (set) Token: 0x0600F619 RID: 63001
		public virtual extern object IHTMLElement3_onmove
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x1700507A RID: 20602
		// (get) Token: 0x0600F61C RID: 63004
		// (set) Token: 0x0600F61B RID: 63003
		public virtual extern object IHTMLElement3_oncontrolselect
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x0600F61D RID: 63005
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement3_FireEvent([MarshalAs(19)] [In] string bstrEventName, [MarshalAs(27)] [In] [Optional] ref object pvarEventObject);

		// Token: 0x1700507B RID: 20603
		// (get) Token: 0x0600F61F RID: 63007
		// (set) Token: 0x0600F61E RID: 63006
		public virtual extern object IHTMLElement3_onresizestart
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x1700507C RID: 20604
		// (get) Token: 0x0600F621 RID: 63009
		// (set) Token: 0x0600F620 RID: 63008
		public virtual extern object IHTMLElement3_onresizeend
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x1700507D RID: 20605
		// (get) Token: 0x0600F623 RID: 63011
		// (set) Token: 0x0600F622 RID: 63010
		public virtual extern object IHTMLElement3_onmovestart
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x1700507E RID: 20606
		// (get) Token: 0x0600F625 RID: 63013
		// (set) Token: 0x0600F624 RID: 63012
		public virtual extern object IHTMLElement3_onmoveend
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x1700507F RID: 20607
		// (get) Token: 0x0600F627 RID: 63015
		// (set) Token: 0x0600F626 RID: 63014
		public virtual extern object IHTMLElement3_onmouseenter
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17005080 RID: 20608
		// (get) Token: 0x0600F629 RID: 63017
		// (set) Token: 0x0600F628 RID: 63016
		public virtual extern object IHTMLElement3_onmouseleave
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17005081 RID: 20609
		// (get) Token: 0x0600F62B RID: 63019
		// (set) Token: 0x0600F62A RID: 63018
		public virtual extern object IHTMLElement3_onactivate
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17005082 RID: 20610
		// (get) Token: 0x0600F62D RID: 63021
		// (set) Token: 0x0600F62C RID: 63020
		public virtual extern object IHTMLElement3_ondeactivate
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x0600F62E RID: 63022
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement3_dragDrop();

		// Token: 0x17005083 RID: 20611
		// (get) Token: 0x0600F62F RID: 63023
		public virtual extern int IHTMLElement3_glyphMode
		{
			[TypeLibFunc(1089)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17005084 RID: 20612
		// (get) Token: 0x0600F631 RID: 63025
		// (set) Token: 0x0600F630 RID: 63024
		public virtual extern object IHTMLElement4_onmousewheel
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x0600F632 RID: 63026
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement4_normalize();

		// Token: 0x0600F633 RID: 63027
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute IHTMLElement4_getAttributeNode([MarshalAs(19)] [In] string bstrName);

		// Token: 0x0600F634 RID: 63028
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute IHTMLElement4_setAttributeNode([MarshalAs(28)] [In] IHTMLDOMAttribute pattr);

		// Token: 0x0600F635 RID: 63029
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute IHTMLElement4_removeAttributeNode([MarshalAs(28)] [In] IHTMLDOMAttribute pattr);

		// Token: 0x17005085 RID: 20613
		// (get) Token: 0x0600F637 RID: 63031
		// (set) Token: 0x0600F636 RID: 63030
		public virtual extern object IHTMLElement4_onbeforeactivate
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17005086 RID: 20614
		// (get) Token: 0x0600F639 RID: 63033
		// (set) Token: 0x0600F638 RID: 63032
		public virtual extern object IHTMLElement4_onfocusin
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17005087 RID: 20615
		// (get) Token: 0x0600F63B RID: 63035
		// (set) Token: 0x0600F63A RID: 63034
		public virtual extern object IHTMLElement4_onfocusout
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17005088 RID: 20616
		// (get) Token: 0x0600F63C RID: 63036
		public virtual extern int IHTMLUniqueName_uniqueNumber
		{
			[TypeLibFunc(64)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17005089 RID: 20617
		// (get) Token: 0x0600F63D RID: 63037
		public virtual extern string IHTMLUniqueName_uniqueID
		{
			[TypeLibFunc(64)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x1700508A RID: 20618
		// (get) Token: 0x0600F63E RID: 63038
		public virtual extern int IHTMLDOMNode_nodeType
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700508B RID: 20619
		// (get) Token: 0x0600F63F RID: 63039
		public virtual extern IHTMLDOMNode IHTMLDOMNode_parentNode
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x0600F640 RID: 63040
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLDOMNode_hasChildNodes();

		// Token: 0x1700508C RID: 20620
		// (get) Token: 0x0600F641 RID: 63041
		public virtual extern object IHTMLDOMNode_childNodes
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x1700508D RID: 20621
		// (get) Token: 0x0600F642 RID: 63042
		public virtual extern object IHTMLDOMNode_attributes
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x0600F643 RID: 63043
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_insertBefore([MarshalAs(28)] [In] IHTMLDOMNode newChild, [MarshalAs(27)] [In] [Optional] object refChild);

		// Token: 0x0600F644 RID: 63044
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_removeChild([MarshalAs(28)] [In] IHTMLDOMNode oldChild);

		// Token: 0x0600F645 RID: 63045
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_replaceChild([MarshalAs(28)] [In] IHTMLDOMNode newChild, [MarshalAs(28)] [In] IHTMLDOMNode oldChild);

		// Token: 0x0600F646 RID: 63046
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_cloneNode([In] bool fDeep);

		// Token: 0x0600F647 RID: 63047
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_removeNode([In] bool fDeep = false);

		// Token: 0x0600F648 RID: 63048
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_swapNode([MarshalAs(28)] [In] IHTMLDOMNode otherNode);

		// Token: 0x0600F649 RID: 63049
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_replaceNode([MarshalAs(28)] [In] IHTMLDOMNode replacement);

		// Token: 0x0600F64A RID: 63050
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_appendChild([MarshalAs(28)] [In] IHTMLDOMNode newChild);

		// Token: 0x1700508E RID: 20622
		// (get) Token: 0x0600F64B RID: 63051
		public virtual extern string IHTMLDOMNode_nodeName
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x1700508F RID: 20623
		// (get) Token: 0x0600F64D RID: 63053
		// (set) Token: 0x0600F64C RID: 63052
		public virtual extern object IHTMLDOMNode_nodeValue
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17005090 RID: 20624
		// (get) Token: 0x0600F64E RID: 63054
		public virtual extern IHTMLDOMNode IHTMLDOMNode_firstChild
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17005091 RID: 20625
		// (get) Token: 0x0600F64F RID: 63055
		public virtual extern IHTMLDOMNode IHTMLDOMNode_lastChild
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17005092 RID: 20626
		// (get) Token: 0x0600F650 RID: 63056
		public virtual extern IHTMLDOMNode IHTMLDOMNode_previousSibling
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17005093 RID: 20627
		// (get) Token: 0x0600F651 RID: 63057
		public virtual extern IHTMLDOMNode IHTMLDOMNode_nextSibling
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17005094 RID: 20628
		// (get) Token: 0x0600F652 RID: 63058
		public virtual extern object IHTMLDOMNode2_ownerDocument
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17005095 RID: 20629
		// (get) Token: 0x0600F654 RID: 63060
		// (set) Token: 0x0600F653 RID: 63059
		public virtual extern short IHTMLControlElement_tabIndex
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x0600F655 RID: 63061
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLControlElement_focus();

		// Token: 0x17005096 RID: 20630
		// (get) Token: 0x0600F657 RID: 63063
		// (set) Token: 0x0600F656 RID: 63062
		public virtual extern string IHTMLControlElement_accessKey
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17005097 RID: 20631
		// (get) Token: 0x0600F659 RID: 63065
		// (set) Token: 0x0600F658 RID: 63064
		public virtual extern object IHTMLControlElement_onblur
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17005098 RID: 20632
		// (get) Token: 0x0600F65B RID: 63067
		// (set) Token: 0x0600F65A RID: 63066
		public virtual extern object IHTMLControlElement_onfocus
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17005099 RID: 20633
		// (get) Token: 0x0600F65D RID: 63069
		// (set) Token: 0x0600F65C RID: 63068
		public virtual extern object IHTMLControlElement_onresize
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x0600F65E RID: 63070
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLControlElement_blur();

		// Token: 0x0600F65F RID: 63071
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLControlElement_addFilter([MarshalAs(25)] [In] object pUnk);

		// Token: 0x0600F660 RID: 63072
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLControlElement_removeFilter([MarshalAs(25)] [In] object pUnk);

		// Token: 0x1700509A RID: 20634
		// (get) Token: 0x0600F661 RID: 63073
		public virtual extern int IHTMLControlElement_clientHeight
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700509B RID: 20635
		// (get) Token: 0x0600F662 RID: 63074
		public virtual extern int IHTMLControlElement_clientWidth
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700509C RID: 20636
		// (get) Token: 0x0600F663 RID: 63075
		public virtual extern int IHTMLControlElement_clientTop
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700509D RID: 20637
		// (get) Token: 0x0600F664 RID: 63076
		public virtual extern int IHTMLControlElement_clientLeft
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x0600F665 RID: 63077
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(26)]
		public virtual extern object IHTMLTextContainer_createControlRange();

		// Token: 0x1700509E RID: 20638
		// (get) Token: 0x0600F666 RID: 63078
		public virtual extern int IHTMLTextContainer_scrollHeight
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700509F RID: 20639
		// (get) Token: 0x0600F667 RID: 63079
		public virtual extern int IHTMLTextContainer_scrollWidth
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170050A0 RID: 20640
		// (get) Token: 0x0600F669 RID: 63081
		// (set) Token: 0x0600F668 RID: 63080
		public virtual extern int IHTMLTextContainer_scrollTop
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x170050A1 RID: 20641
		// (get) Token: 0x0600F66B RID: 63083
		// (set) Token: 0x0600F66A RID: 63082
		public virtual extern int IHTMLTextContainer_scrollLeft
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x170050A2 RID: 20642
		// (get) Token: 0x0600F66D RID: 63085
		// (set) Token: 0x0600F66C RID: 63084
		public virtual extern object IHTMLTextContainer_onscroll
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x170050A3 RID: 20643
		// (get) Token: 0x0600F66F RID: 63087
		// (set) Token: 0x0600F66E RID: 63086
		public virtual extern string IHTMLTableCaption_align
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x170050A4 RID: 20644
		// (get) Token: 0x0600F671 RID: 63089
		// (set) Token: 0x0600F670 RID: 63088
		public virtual extern string IHTMLTableCaption_vAlign
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x14001DF7 RID: 7671
		// (add) Token: 0x0600F672 RID: 63090
		// (remove) Token: 0x0600F673 RID: 63091
		public virtual extern event HTMLTextContainerEvents_onhelpEventHandler HTMLTextContainerEvents_Event_onhelp;

		// Token: 0x14001DF8 RID: 7672
		// (add) Token: 0x0600F674 RID: 63092
		// (remove) Token: 0x0600F675 RID: 63093
		public virtual extern event HTMLTextContainerEvents_onclickEventHandler HTMLTextContainerEvents_Event_onclick;

		// Token: 0x14001DF9 RID: 7673
		// (add) Token: 0x0600F676 RID: 63094
		// (remove) Token: 0x0600F677 RID: 63095
		public virtual extern event HTMLTextContainerEvents_ondblclickEventHandler HTMLTextContainerEvents_Event_ondblclick;

		// Token: 0x14001DFA RID: 7674
		// (add) Token: 0x0600F678 RID: 63096
		// (remove) Token: 0x0600F679 RID: 63097
		public virtual extern event HTMLTextContainerEvents_onkeypressEventHandler HTMLTextContainerEvents_Event_onkeypress;

		// Token: 0x14001DFB RID: 7675
		// (add) Token: 0x0600F67A RID: 63098
		// (remove) Token: 0x0600F67B RID: 63099
		public virtual extern event HTMLTextContainerEvents_onkeydownEventHandler HTMLTextContainerEvents_Event_onkeydown;

		// Token: 0x14001DFC RID: 7676
		// (add) Token: 0x0600F67C RID: 63100
		// (remove) Token: 0x0600F67D RID: 63101
		public virtual extern event HTMLTextContainerEvents_onkeyupEventHandler HTMLTextContainerEvents_Event_onkeyup;

		// Token: 0x14001DFD RID: 7677
		// (add) Token: 0x0600F67E RID: 63102
		// (remove) Token: 0x0600F67F RID: 63103
		public virtual extern event HTMLTextContainerEvents_onmouseoutEventHandler HTMLTextContainerEvents_Event_onmouseout;

		// Token: 0x14001DFE RID: 7678
		// (add) Token: 0x0600F680 RID: 63104
		// (remove) Token: 0x0600F681 RID: 63105
		public virtual extern event HTMLTextContainerEvents_onmouseoverEventHandler HTMLTextContainerEvents_Event_onmouseover;

		// Token: 0x14001DFF RID: 7679
		// (add) Token: 0x0600F682 RID: 63106
		// (remove) Token: 0x0600F683 RID: 63107
		public virtual extern event HTMLTextContainerEvents_onmousemoveEventHandler HTMLTextContainerEvents_Event_onmousemove;

		// Token: 0x14001E00 RID: 7680
		// (add) Token: 0x0600F684 RID: 63108
		// (remove) Token: 0x0600F685 RID: 63109
		public virtual extern event HTMLTextContainerEvents_onmousedownEventHandler HTMLTextContainerEvents_Event_onmousedown;

		// Token: 0x14001E01 RID: 7681
		// (add) Token: 0x0600F686 RID: 63110
		// (remove) Token: 0x0600F687 RID: 63111
		public virtual extern event HTMLTextContainerEvents_onmouseupEventHandler HTMLTextContainerEvents_Event_onmouseup;

		// Token: 0x14001E02 RID: 7682
		// (add) Token: 0x0600F688 RID: 63112
		// (remove) Token: 0x0600F689 RID: 63113
		public virtual extern event HTMLTextContainerEvents_onselectstartEventHandler HTMLTextContainerEvents_Event_onselectstart;

		// Token: 0x14001E03 RID: 7683
		// (add) Token: 0x0600F68A RID: 63114
		// (remove) Token: 0x0600F68B RID: 63115
		public virtual extern event HTMLTextContainerEvents_onfilterchangeEventHandler HTMLTextContainerEvents_Event_onfilterchange;

		// Token: 0x14001E04 RID: 7684
		// (add) Token: 0x0600F68C RID: 63116
		// (remove) Token: 0x0600F68D RID: 63117
		public virtual extern event HTMLTextContainerEvents_ondragstartEventHandler HTMLTextContainerEvents_Event_ondragstart;

		// Token: 0x14001E05 RID: 7685
		// (add) Token: 0x0600F68E RID: 63118
		// (remove) Token: 0x0600F68F RID: 63119
		public virtual extern event HTMLTextContainerEvents_onbeforeupdateEventHandler HTMLTextContainerEvents_Event_onbeforeupdate;

		// Token: 0x14001E06 RID: 7686
		// (add) Token: 0x0600F690 RID: 63120
		// (remove) Token: 0x0600F691 RID: 63121
		public virtual extern event HTMLTextContainerEvents_onafterupdateEventHandler HTMLTextContainerEvents_Event_onafterupdate;

		// Token: 0x14001E07 RID: 7687
		// (add) Token: 0x0600F692 RID: 63122
		// (remove) Token: 0x0600F693 RID: 63123
		public virtual extern event HTMLTextContainerEvents_onerrorupdateEventHandler HTMLTextContainerEvents_Event_onerrorupdate;

		// Token: 0x14001E08 RID: 7688
		// (add) Token: 0x0600F694 RID: 63124
		// (remove) Token: 0x0600F695 RID: 63125
		public virtual extern event HTMLTextContainerEvents_onrowexitEventHandler HTMLTextContainerEvents_Event_onrowexit;

		// Token: 0x14001E09 RID: 7689
		// (add) Token: 0x0600F696 RID: 63126
		// (remove) Token: 0x0600F697 RID: 63127
		public virtual extern event HTMLTextContainerEvents_onrowenterEventHandler HTMLTextContainerEvents_Event_onrowenter;

		// Token: 0x14001E0A RID: 7690
		// (add) Token: 0x0600F698 RID: 63128
		// (remove) Token: 0x0600F699 RID: 63129
		public virtual extern event HTMLTextContainerEvents_ondatasetchangedEventHandler HTMLTextContainerEvents_Event_ondatasetchanged;

		// Token: 0x14001E0B RID: 7691
		// (add) Token: 0x0600F69A RID: 63130
		// (remove) Token: 0x0600F69B RID: 63131
		public virtual extern event HTMLTextContainerEvents_ondataavailableEventHandler HTMLTextContainerEvents_Event_ondataavailable;

		// Token: 0x14001E0C RID: 7692
		// (add) Token: 0x0600F69C RID: 63132
		// (remove) Token: 0x0600F69D RID: 63133
		public virtual extern event HTMLTextContainerEvents_ondatasetcompleteEventHandler HTMLTextContainerEvents_Event_ondatasetcomplete;

		// Token: 0x14001E0D RID: 7693
		// (add) Token: 0x0600F69E RID: 63134
		// (remove) Token: 0x0600F69F RID: 63135
		public virtual extern event HTMLTextContainerEvents_onlosecaptureEventHandler HTMLTextContainerEvents_Event_onlosecapture;

		// Token: 0x14001E0E RID: 7694
		// (add) Token: 0x0600F6A0 RID: 63136
		// (remove) Token: 0x0600F6A1 RID: 63137
		public virtual extern event HTMLTextContainerEvents_onpropertychangeEventHandler HTMLTextContainerEvents_Event_onpropertychange;

		// Token: 0x14001E0F RID: 7695
		// (add) Token: 0x0600F6A2 RID: 63138
		// (remove) Token: 0x0600F6A3 RID: 63139
		public virtual extern event HTMLTextContainerEvents_onscrollEventHandler HTMLTextContainerEvents_Event_onscroll;

		// Token: 0x14001E10 RID: 7696
		// (add) Token: 0x0600F6A4 RID: 63140
		// (remove) Token: 0x0600F6A5 RID: 63141
		public virtual extern event HTMLTextContainerEvents_onfocusEventHandler HTMLTextContainerEvents_Event_onfocus;

		// Token: 0x14001E11 RID: 7697
		// (add) Token: 0x0600F6A6 RID: 63142
		// (remove) Token: 0x0600F6A7 RID: 63143
		public virtual extern event HTMLTextContainerEvents_onblurEventHandler HTMLTextContainerEvents_Event_onblur;

		// Token: 0x14001E12 RID: 7698
		// (add) Token: 0x0600F6A8 RID: 63144
		// (remove) Token: 0x0600F6A9 RID: 63145
		public virtual extern event HTMLTextContainerEvents_onresizeEventHandler HTMLTextContainerEvents_Event_onresize;

		// Token: 0x14001E13 RID: 7699
		// (add) Token: 0x0600F6AA RID: 63146
		// (remove) Token: 0x0600F6AB RID: 63147
		public virtual extern event HTMLTextContainerEvents_ondragEventHandler HTMLTextContainerEvents_Event_ondrag;

		// Token: 0x14001E14 RID: 7700
		// (add) Token: 0x0600F6AC RID: 63148
		// (remove) Token: 0x0600F6AD RID: 63149
		public virtual extern event HTMLTextContainerEvents_ondragendEventHandler HTMLTextContainerEvents_Event_ondragend;

		// Token: 0x14001E15 RID: 7701
		// (add) Token: 0x0600F6AE RID: 63150
		// (remove) Token: 0x0600F6AF RID: 63151
		public virtual extern event HTMLTextContainerEvents_ondragenterEventHandler HTMLTextContainerEvents_Event_ondragenter;

		// Token: 0x14001E16 RID: 7702
		// (add) Token: 0x0600F6B0 RID: 63152
		// (remove) Token: 0x0600F6B1 RID: 63153
		public virtual extern event HTMLTextContainerEvents_ondragoverEventHandler HTMLTextContainerEvents_Event_ondragover;

		// Token: 0x14001E17 RID: 7703
		// (add) Token: 0x0600F6B2 RID: 63154
		// (remove) Token: 0x0600F6B3 RID: 63155
		public virtual extern event HTMLTextContainerEvents_ondragleaveEventHandler HTMLTextContainerEvents_Event_ondragleave;

		// Token: 0x14001E18 RID: 7704
		// (add) Token: 0x0600F6B4 RID: 63156
		// (remove) Token: 0x0600F6B5 RID: 63157
		public virtual extern event HTMLTextContainerEvents_ondropEventHandler HTMLTextContainerEvents_Event_ondrop;

		// Token: 0x14001E19 RID: 7705
		// (add) Token: 0x0600F6B6 RID: 63158
		// (remove) Token: 0x0600F6B7 RID: 63159
		public virtual extern event HTMLTextContainerEvents_onbeforecutEventHandler HTMLTextContainerEvents_Event_onbeforecut;

		// Token: 0x14001E1A RID: 7706
		// (add) Token: 0x0600F6B8 RID: 63160
		// (remove) Token: 0x0600F6B9 RID: 63161
		public virtual extern event HTMLTextContainerEvents_oncutEventHandler HTMLTextContainerEvents_Event_oncut;

		// Token: 0x14001E1B RID: 7707
		// (add) Token: 0x0600F6BA RID: 63162
		// (remove) Token: 0x0600F6BB RID: 63163
		public virtual extern event HTMLTextContainerEvents_onbeforecopyEventHandler HTMLTextContainerEvents_Event_onbeforecopy;

		// Token: 0x14001E1C RID: 7708
		// (add) Token: 0x0600F6BC RID: 63164
		// (remove) Token: 0x0600F6BD RID: 63165
		public virtual extern event HTMLTextContainerEvents_oncopyEventHandler HTMLTextContainerEvents_Event_oncopy;

		// Token: 0x14001E1D RID: 7709
		// (add) Token: 0x0600F6BE RID: 63166
		// (remove) Token: 0x0600F6BF RID: 63167
		public virtual extern event HTMLTextContainerEvents_onbeforepasteEventHandler HTMLTextContainerEvents_Event_onbeforepaste;

		// Token: 0x14001E1E RID: 7710
		// (add) Token: 0x0600F6C0 RID: 63168
		// (remove) Token: 0x0600F6C1 RID: 63169
		public virtual extern event HTMLTextContainerEvents_onpasteEventHandler HTMLTextContainerEvents_Event_onpaste;

		// Token: 0x14001E1F RID: 7711
		// (add) Token: 0x0600F6C2 RID: 63170
		// (remove) Token: 0x0600F6C3 RID: 63171
		public virtual extern event HTMLTextContainerEvents_oncontextmenuEventHandler HTMLTextContainerEvents_Event_oncontextmenu;

		// Token: 0x14001E20 RID: 7712
		// (add) Token: 0x0600F6C4 RID: 63172
		// (remove) Token: 0x0600F6C5 RID: 63173
		public virtual extern event HTMLTextContainerEvents_onrowsdeleteEventHandler HTMLTextContainerEvents_Event_onrowsdelete;

		// Token: 0x14001E21 RID: 7713
		// (add) Token: 0x0600F6C6 RID: 63174
		// (remove) Token: 0x0600F6C7 RID: 63175
		public virtual extern event HTMLTextContainerEvents_onrowsinsertedEventHandler HTMLTextContainerEvents_Event_onrowsinserted;

		// Token: 0x14001E22 RID: 7714
		// (add) Token: 0x0600F6C8 RID: 63176
		// (remove) Token: 0x0600F6C9 RID: 63177
		public virtual extern event HTMLTextContainerEvents_oncellchangeEventHandler HTMLTextContainerEvents_Event_oncellchange;

		// Token: 0x14001E23 RID: 7715
		// (add) Token: 0x0600F6CA RID: 63178
		// (remove) Token: 0x0600F6CB RID: 63179
		public virtual extern event HTMLTextContainerEvents_onreadystatechangeEventHandler HTMLTextContainerEvents_Event_onreadystatechange;

		// Token: 0x14001E24 RID: 7716
		// (add) Token: 0x0600F6CC RID: 63180
		// (remove) Token: 0x0600F6CD RID: 63181
		public virtual extern event HTMLTextContainerEvents_onbeforeeditfocusEventHandler HTMLTextContainerEvents_Event_onbeforeeditfocus;

		// Token: 0x14001E25 RID: 7717
		// (add) Token: 0x0600F6CE RID: 63182
		// (remove) Token: 0x0600F6CF RID: 63183
		public virtual extern event HTMLTextContainerEvents_onlayoutcompleteEventHandler HTMLTextContainerEvents_Event_onlayoutcomplete;

		// Token: 0x14001E26 RID: 7718
		// (add) Token: 0x0600F6D0 RID: 63184
		// (remove) Token: 0x0600F6D1 RID: 63185
		public virtual extern event HTMLTextContainerEvents_onpageEventHandler HTMLTextContainerEvents_Event_onpage;

		// Token: 0x14001E27 RID: 7719
		// (add) Token: 0x0600F6D2 RID: 63186
		// (remove) Token: 0x0600F6D3 RID: 63187
		public virtual extern event HTMLTextContainerEvents_onbeforedeactivateEventHandler HTMLTextContainerEvents_Event_onbeforedeactivate;

		// Token: 0x14001E28 RID: 7720
		// (add) Token: 0x0600F6D4 RID: 63188
		// (remove) Token: 0x0600F6D5 RID: 63189
		public virtual extern event HTMLTextContainerEvents_onbeforeactivateEventHandler HTMLTextContainerEvents_Event_onbeforeactivate;

		// Token: 0x14001E29 RID: 7721
		// (add) Token: 0x0600F6D6 RID: 63190
		// (remove) Token: 0x0600F6D7 RID: 63191
		public virtual extern event HTMLTextContainerEvents_onmoveEventHandler HTMLTextContainerEvents_Event_onmove;

		// Token: 0x14001E2A RID: 7722
		// (add) Token: 0x0600F6D8 RID: 63192
		// (remove) Token: 0x0600F6D9 RID: 63193
		public virtual extern event HTMLTextContainerEvents_oncontrolselectEventHandler HTMLTextContainerEvents_Event_oncontrolselect;

		// Token: 0x14001E2B RID: 7723
		// (add) Token: 0x0600F6DA RID: 63194
		// (remove) Token: 0x0600F6DB RID: 63195
		public virtual extern event HTMLTextContainerEvents_onmovestartEventHandler HTMLTextContainerEvents_Event_onmovestart;

		// Token: 0x14001E2C RID: 7724
		// (add) Token: 0x0600F6DC RID: 63196
		// (remove) Token: 0x0600F6DD RID: 63197
		public virtual extern event HTMLTextContainerEvents_onmoveendEventHandler HTMLTextContainerEvents_Event_onmoveend;

		// Token: 0x14001E2D RID: 7725
		// (add) Token: 0x0600F6DE RID: 63198
		// (remove) Token: 0x0600F6DF RID: 63199
		public virtual extern event HTMLTextContainerEvents_onresizestartEventHandler HTMLTextContainerEvents_Event_onresizestart;

		// Token: 0x14001E2E RID: 7726
		// (add) Token: 0x0600F6E0 RID: 63200
		// (remove) Token: 0x0600F6E1 RID: 63201
		public virtual extern event HTMLTextContainerEvents_onresizeendEventHandler HTMLTextContainerEvents_Event_onresizeend;

		// Token: 0x14001E2F RID: 7727
		// (add) Token: 0x0600F6E2 RID: 63202
		// (remove) Token: 0x0600F6E3 RID: 63203
		public virtual extern event HTMLTextContainerEvents_onmouseenterEventHandler HTMLTextContainerEvents_Event_onmouseenter;

		// Token: 0x14001E30 RID: 7728
		// (add) Token: 0x0600F6E4 RID: 63204
		// (remove) Token: 0x0600F6E5 RID: 63205
		public virtual extern event HTMLTextContainerEvents_onmouseleaveEventHandler HTMLTextContainerEvents_Event_onmouseleave;

		// Token: 0x14001E31 RID: 7729
		// (add) Token: 0x0600F6E6 RID: 63206
		// (remove) Token: 0x0600F6E7 RID: 63207
		public virtual extern event HTMLTextContainerEvents_onmousewheelEventHandler HTMLTextContainerEvents_Event_onmousewheel;

		// Token: 0x14001E32 RID: 7730
		// (add) Token: 0x0600F6E8 RID: 63208
		// (remove) Token: 0x0600F6E9 RID: 63209
		public virtual extern event HTMLTextContainerEvents_onactivateEventHandler HTMLTextContainerEvents_Event_onactivate;

		// Token: 0x14001E33 RID: 7731
		// (add) Token: 0x0600F6EA RID: 63210
		// (remove) Token: 0x0600F6EB RID: 63211
		public virtual extern event HTMLTextContainerEvents_ondeactivateEventHandler HTMLTextContainerEvents_Event_ondeactivate;

		// Token: 0x14001E34 RID: 7732
		// (add) Token: 0x0600F6EC RID: 63212
		// (remove) Token: 0x0600F6ED RID: 63213
		public virtual extern event HTMLTextContainerEvents_onfocusinEventHandler HTMLTextContainerEvents_Event_onfocusin;

		// Token: 0x14001E35 RID: 7733
		// (add) Token: 0x0600F6EE RID: 63214
		// (remove) Token: 0x0600F6EF RID: 63215
		public virtual extern event HTMLTextContainerEvents_onfocusoutEventHandler HTMLTextContainerEvents_Event_onfocusout;

		// Token: 0x14001E36 RID: 7734
		// (add) Token: 0x0600F6F0 RID: 63216
		// (remove) Token: 0x0600F6F1 RID: 63217
		public virtual extern event HTMLTextContainerEvents_onchangeEventHandler onchange;

		// Token: 0x14001E37 RID: 7735
		// (add) Token: 0x0600F6F2 RID: 63218
		// (remove) Token: 0x0600F6F3 RID: 63219
		public virtual extern event HTMLTextContainerEvents_onselectEventHandler onselect;

		// Token: 0x14001E38 RID: 7736
		// (add) Token: 0x0600F6F4 RID: 63220
		// (remove) Token: 0x0600F6F5 RID: 63221
		public virtual extern event HTMLTextContainerEvents2_onhelpEventHandler HTMLTextContainerEvents2_Event_onhelp;

		// Token: 0x14001E39 RID: 7737
		// (add) Token: 0x0600F6F6 RID: 63222
		// (remove) Token: 0x0600F6F7 RID: 63223
		public virtual extern event HTMLTextContainerEvents2_onclickEventHandler HTMLTextContainerEvents2_Event_onclick;

		// Token: 0x14001E3A RID: 7738
		// (add) Token: 0x0600F6F8 RID: 63224
		// (remove) Token: 0x0600F6F9 RID: 63225
		public virtual extern event HTMLTextContainerEvents2_ondblclickEventHandler HTMLTextContainerEvents2_Event_ondblclick;

		// Token: 0x14001E3B RID: 7739
		// (add) Token: 0x0600F6FA RID: 63226
		// (remove) Token: 0x0600F6FB RID: 63227
		public virtual extern event HTMLTextContainerEvents2_onkeypressEventHandler HTMLTextContainerEvents2_Event_onkeypress;

		// Token: 0x14001E3C RID: 7740
		// (add) Token: 0x0600F6FC RID: 63228
		// (remove) Token: 0x0600F6FD RID: 63229
		public virtual extern event HTMLTextContainerEvents2_onkeydownEventHandler HTMLTextContainerEvents2_Event_onkeydown;

		// Token: 0x14001E3D RID: 7741
		// (add) Token: 0x0600F6FE RID: 63230
		// (remove) Token: 0x0600F6FF RID: 63231
		public virtual extern event HTMLTextContainerEvents2_onkeyupEventHandler HTMLTextContainerEvents2_Event_onkeyup;

		// Token: 0x14001E3E RID: 7742
		// (add) Token: 0x0600F700 RID: 63232
		// (remove) Token: 0x0600F701 RID: 63233
		public virtual extern event HTMLTextContainerEvents2_onmouseoutEventHandler HTMLTextContainerEvents2_Event_onmouseout;

		// Token: 0x14001E3F RID: 7743
		// (add) Token: 0x0600F702 RID: 63234
		// (remove) Token: 0x0600F703 RID: 63235
		public virtual extern event HTMLTextContainerEvents2_onmouseoverEventHandler HTMLTextContainerEvents2_Event_onmouseover;

		// Token: 0x14001E40 RID: 7744
		// (add) Token: 0x0600F704 RID: 63236
		// (remove) Token: 0x0600F705 RID: 63237
		public virtual extern event HTMLTextContainerEvents2_onmousemoveEventHandler HTMLTextContainerEvents2_Event_onmousemove;

		// Token: 0x14001E41 RID: 7745
		// (add) Token: 0x0600F706 RID: 63238
		// (remove) Token: 0x0600F707 RID: 63239
		public virtual extern event HTMLTextContainerEvents2_onmousedownEventHandler HTMLTextContainerEvents2_Event_onmousedown;

		// Token: 0x14001E42 RID: 7746
		// (add) Token: 0x0600F708 RID: 63240
		// (remove) Token: 0x0600F709 RID: 63241
		public virtual extern event HTMLTextContainerEvents2_onmouseupEventHandler HTMLTextContainerEvents2_Event_onmouseup;

		// Token: 0x14001E43 RID: 7747
		// (add) Token: 0x0600F70A RID: 63242
		// (remove) Token: 0x0600F70B RID: 63243
		public virtual extern event HTMLTextContainerEvents2_onselectstartEventHandler HTMLTextContainerEvents2_Event_onselectstart;

		// Token: 0x14001E44 RID: 7748
		// (add) Token: 0x0600F70C RID: 63244
		// (remove) Token: 0x0600F70D RID: 63245
		public virtual extern event HTMLTextContainerEvents2_onfilterchangeEventHandler HTMLTextContainerEvents2_Event_onfilterchange;

		// Token: 0x14001E45 RID: 7749
		// (add) Token: 0x0600F70E RID: 63246
		// (remove) Token: 0x0600F70F RID: 63247
		public virtual extern event HTMLTextContainerEvents2_ondragstartEventHandler HTMLTextContainerEvents2_Event_ondragstart;

		// Token: 0x14001E46 RID: 7750
		// (add) Token: 0x0600F710 RID: 63248
		// (remove) Token: 0x0600F711 RID: 63249
		public virtual extern event HTMLTextContainerEvents2_onbeforeupdateEventHandler HTMLTextContainerEvents2_Event_onbeforeupdate;

		// Token: 0x14001E47 RID: 7751
		// (add) Token: 0x0600F712 RID: 63250
		// (remove) Token: 0x0600F713 RID: 63251
		public virtual extern event HTMLTextContainerEvents2_onafterupdateEventHandler HTMLTextContainerEvents2_Event_onafterupdate;

		// Token: 0x14001E48 RID: 7752
		// (add) Token: 0x0600F714 RID: 63252
		// (remove) Token: 0x0600F715 RID: 63253
		public virtual extern event HTMLTextContainerEvents2_onerrorupdateEventHandler HTMLTextContainerEvents2_Event_onerrorupdate;

		// Token: 0x14001E49 RID: 7753
		// (add) Token: 0x0600F716 RID: 63254
		// (remove) Token: 0x0600F717 RID: 63255
		public virtual extern event HTMLTextContainerEvents2_onrowexitEventHandler HTMLTextContainerEvents2_Event_onrowexit;

		// Token: 0x14001E4A RID: 7754
		// (add) Token: 0x0600F718 RID: 63256
		// (remove) Token: 0x0600F719 RID: 63257
		public virtual extern event HTMLTextContainerEvents2_onrowenterEventHandler HTMLTextContainerEvents2_Event_onrowenter;

		// Token: 0x14001E4B RID: 7755
		// (add) Token: 0x0600F71A RID: 63258
		// (remove) Token: 0x0600F71B RID: 63259
		public virtual extern event HTMLTextContainerEvents2_ondatasetchangedEventHandler HTMLTextContainerEvents2_Event_ondatasetchanged;

		// Token: 0x14001E4C RID: 7756
		// (add) Token: 0x0600F71C RID: 63260
		// (remove) Token: 0x0600F71D RID: 63261
		public virtual extern event HTMLTextContainerEvents2_ondataavailableEventHandler HTMLTextContainerEvents2_Event_ondataavailable;

		// Token: 0x14001E4D RID: 7757
		// (add) Token: 0x0600F71E RID: 63262
		// (remove) Token: 0x0600F71F RID: 63263
		public virtual extern event HTMLTextContainerEvents2_ondatasetcompleteEventHandler HTMLTextContainerEvents2_Event_ondatasetcomplete;

		// Token: 0x14001E4E RID: 7758
		// (add) Token: 0x0600F720 RID: 63264
		// (remove) Token: 0x0600F721 RID: 63265
		public virtual extern event HTMLTextContainerEvents2_onlosecaptureEventHandler HTMLTextContainerEvents2_Event_onlosecapture;

		// Token: 0x14001E4F RID: 7759
		// (add) Token: 0x0600F722 RID: 63266
		// (remove) Token: 0x0600F723 RID: 63267
		public virtual extern event HTMLTextContainerEvents2_onpropertychangeEventHandler HTMLTextContainerEvents2_Event_onpropertychange;

		// Token: 0x14001E50 RID: 7760
		// (add) Token: 0x0600F724 RID: 63268
		// (remove) Token: 0x0600F725 RID: 63269
		public virtual extern event HTMLTextContainerEvents2_onscrollEventHandler HTMLTextContainerEvents2_Event_onscroll;

		// Token: 0x14001E51 RID: 7761
		// (add) Token: 0x0600F726 RID: 63270
		// (remove) Token: 0x0600F727 RID: 63271
		public virtual extern event HTMLTextContainerEvents2_onfocusEventHandler HTMLTextContainerEvents2_Event_onfocus;

		// Token: 0x14001E52 RID: 7762
		// (add) Token: 0x0600F728 RID: 63272
		// (remove) Token: 0x0600F729 RID: 63273
		public virtual extern event HTMLTextContainerEvents2_onblurEventHandler HTMLTextContainerEvents2_Event_onblur;

		// Token: 0x14001E53 RID: 7763
		// (add) Token: 0x0600F72A RID: 63274
		// (remove) Token: 0x0600F72B RID: 63275
		public virtual extern event HTMLTextContainerEvents2_onresizeEventHandler HTMLTextContainerEvents2_Event_onresize;

		// Token: 0x14001E54 RID: 7764
		// (add) Token: 0x0600F72C RID: 63276
		// (remove) Token: 0x0600F72D RID: 63277
		public virtual extern event HTMLTextContainerEvents2_ondragEventHandler HTMLTextContainerEvents2_Event_ondrag;

		// Token: 0x14001E55 RID: 7765
		// (add) Token: 0x0600F72E RID: 63278
		// (remove) Token: 0x0600F72F RID: 63279
		public virtual extern event HTMLTextContainerEvents2_ondragendEventHandler HTMLTextContainerEvents2_Event_ondragend;

		// Token: 0x14001E56 RID: 7766
		// (add) Token: 0x0600F730 RID: 63280
		// (remove) Token: 0x0600F731 RID: 63281
		public virtual extern event HTMLTextContainerEvents2_ondragenterEventHandler HTMLTextContainerEvents2_Event_ondragenter;

		// Token: 0x14001E57 RID: 7767
		// (add) Token: 0x0600F732 RID: 63282
		// (remove) Token: 0x0600F733 RID: 63283
		public virtual extern event HTMLTextContainerEvents2_ondragoverEventHandler HTMLTextContainerEvents2_Event_ondragover;

		// Token: 0x14001E58 RID: 7768
		// (add) Token: 0x0600F734 RID: 63284
		// (remove) Token: 0x0600F735 RID: 63285
		public virtual extern event HTMLTextContainerEvents2_ondragleaveEventHandler HTMLTextContainerEvents2_Event_ondragleave;

		// Token: 0x14001E59 RID: 7769
		// (add) Token: 0x0600F736 RID: 63286
		// (remove) Token: 0x0600F737 RID: 63287
		public virtual extern event HTMLTextContainerEvents2_ondropEventHandler HTMLTextContainerEvents2_Event_ondrop;

		// Token: 0x14001E5A RID: 7770
		// (add) Token: 0x0600F738 RID: 63288
		// (remove) Token: 0x0600F739 RID: 63289
		public virtual extern event HTMLTextContainerEvents2_onbeforecutEventHandler HTMLTextContainerEvents2_Event_onbeforecut;

		// Token: 0x14001E5B RID: 7771
		// (add) Token: 0x0600F73A RID: 63290
		// (remove) Token: 0x0600F73B RID: 63291
		public virtual extern event HTMLTextContainerEvents2_oncutEventHandler HTMLTextContainerEvents2_Event_oncut;

		// Token: 0x14001E5C RID: 7772
		// (add) Token: 0x0600F73C RID: 63292
		// (remove) Token: 0x0600F73D RID: 63293
		public virtual extern event HTMLTextContainerEvents2_onbeforecopyEventHandler HTMLTextContainerEvents2_Event_onbeforecopy;

		// Token: 0x14001E5D RID: 7773
		// (add) Token: 0x0600F73E RID: 63294
		// (remove) Token: 0x0600F73F RID: 63295
		public virtual extern event HTMLTextContainerEvents2_oncopyEventHandler HTMLTextContainerEvents2_Event_oncopy;

		// Token: 0x14001E5E RID: 7774
		// (add) Token: 0x0600F740 RID: 63296
		// (remove) Token: 0x0600F741 RID: 63297
		public virtual extern event HTMLTextContainerEvents2_onbeforepasteEventHandler HTMLTextContainerEvents2_Event_onbeforepaste;

		// Token: 0x14001E5F RID: 7775
		// (add) Token: 0x0600F742 RID: 63298
		// (remove) Token: 0x0600F743 RID: 63299
		public virtual extern event HTMLTextContainerEvents2_onpasteEventHandler HTMLTextContainerEvents2_Event_onpaste;

		// Token: 0x14001E60 RID: 7776
		// (add) Token: 0x0600F744 RID: 63300
		// (remove) Token: 0x0600F745 RID: 63301
		public virtual extern event HTMLTextContainerEvents2_oncontextmenuEventHandler HTMLTextContainerEvents2_Event_oncontextmenu;

		// Token: 0x14001E61 RID: 7777
		// (add) Token: 0x0600F746 RID: 63302
		// (remove) Token: 0x0600F747 RID: 63303
		public virtual extern event HTMLTextContainerEvents2_onrowsdeleteEventHandler HTMLTextContainerEvents2_Event_onrowsdelete;

		// Token: 0x14001E62 RID: 7778
		// (add) Token: 0x0600F748 RID: 63304
		// (remove) Token: 0x0600F749 RID: 63305
		public virtual extern event HTMLTextContainerEvents2_onrowsinsertedEventHandler HTMLTextContainerEvents2_Event_onrowsinserted;

		// Token: 0x14001E63 RID: 7779
		// (add) Token: 0x0600F74A RID: 63306
		// (remove) Token: 0x0600F74B RID: 63307
		public virtual extern event HTMLTextContainerEvents2_oncellchangeEventHandler HTMLTextContainerEvents2_Event_oncellchange;

		// Token: 0x14001E64 RID: 7780
		// (add) Token: 0x0600F74C RID: 63308
		// (remove) Token: 0x0600F74D RID: 63309
		public virtual extern event HTMLTextContainerEvents2_onreadystatechangeEventHandler HTMLTextContainerEvents2_Event_onreadystatechange;

		// Token: 0x14001E65 RID: 7781
		// (add) Token: 0x0600F74E RID: 63310
		// (remove) Token: 0x0600F74F RID: 63311
		public virtual extern event HTMLTextContainerEvents2_onlayoutcompleteEventHandler HTMLTextContainerEvents2_Event_onlayoutcomplete;

		// Token: 0x14001E66 RID: 7782
		// (add) Token: 0x0600F750 RID: 63312
		// (remove) Token: 0x0600F751 RID: 63313
		public virtual extern event HTMLTextContainerEvents2_onpageEventHandler HTMLTextContainerEvents2_Event_onpage;

		// Token: 0x14001E67 RID: 7783
		// (add) Token: 0x0600F752 RID: 63314
		// (remove) Token: 0x0600F753 RID: 63315
		public virtual extern event HTMLTextContainerEvents2_onmouseenterEventHandler HTMLTextContainerEvents2_Event_onmouseenter;

		// Token: 0x14001E68 RID: 7784
		// (add) Token: 0x0600F754 RID: 63316
		// (remove) Token: 0x0600F755 RID: 63317
		public virtual extern event HTMLTextContainerEvents2_onmouseleaveEventHandler HTMLTextContainerEvents2_Event_onmouseleave;

		// Token: 0x14001E69 RID: 7785
		// (add) Token: 0x0600F756 RID: 63318
		// (remove) Token: 0x0600F757 RID: 63319
		public virtual extern event HTMLTextContainerEvents2_onactivateEventHandler HTMLTextContainerEvents2_Event_onactivate;

		// Token: 0x14001E6A RID: 7786
		// (add) Token: 0x0600F758 RID: 63320
		// (remove) Token: 0x0600F759 RID: 63321
		public virtual extern event HTMLTextContainerEvents2_ondeactivateEventHandler HTMLTextContainerEvents2_Event_ondeactivate;

		// Token: 0x14001E6B RID: 7787
		// (add) Token: 0x0600F75A RID: 63322
		// (remove) Token: 0x0600F75B RID: 63323
		public virtual extern event HTMLTextContainerEvents2_onbeforedeactivateEventHandler HTMLTextContainerEvents2_Event_onbeforedeactivate;

		// Token: 0x14001E6C RID: 7788
		// (add) Token: 0x0600F75C RID: 63324
		// (remove) Token: 0x0600F75D RID: 63325
		public virtual extern event HTMLTextContainerEvents2_onbeforeactivateEventHandler HTMLTextContainerEvents2_Event_onbeforeactivate;

		// Token: 0x14001E6D RID: 7789
		// (add) Token: 0x0600F75E RID: 63326
		// (remove) Token: 0x0600F75F RID: 63327
		public virtual extern event HTMLTextContainerEvents2_onfocusinEventHandler HTMLTextContainerEvents2_Event_onfocusin;

		// Token: 0x14001E6E RID: 7790
		// (add) Token: 0x0600F760 RID: 63328
		// (remove) Token: 0x0600F761 RID: 63329
		public virtual extern event HTMLTextContainerEvents2_onfocusoutEventHandler HTMLTextContainerEvents2_Event_onfocusout;

		// Token: 0x14001E6F RID: 7791
		// (add) Token: 0x0600F762 RID: 63330
		// (remove) Token: 0x0600F763 RID: 63331
		public virtual extern event HTMLTextContainerEvents2_onmoveEventHandler HTMLTextContainerEvents2_Event_onmove;

		// Token: 0x14001E70 RID: 7792
		// (add) Token: 0x0600F764 RID: 63332
		// (remove) Token: 0x0600F765 RID: 63333
		public virtual extern event HTMLTextContainerEvents2_oncontrolselectEventHandler HTMLTextContainerEvents2_Event_oncontrolselect;

		// Token: 0x14001E71 RID: 7793
		// (add) Token: 0x0600F766 RID: 63334
		// (remove) Token: 0x0600F767 RID: 63335
		public virtual extern event HTMLTextContainerEvents2_onmovestartEventHandler HTMLTextContainerEvents2_Event_onmovestart;

		// Token: 0x14001E72 RID: 7794
		// (add) Token: 0x0600F768 RID: 63336
		// (remove) Token: 0x0600F769 RID: 63337
		public virtual extern event HTMLTextContainerEvents2_onmoveendEventHandler HTMLTextContainerEvents2_Event_onmoveend;

		// Token: 0x14001E73 RID: 7795
		// (add) Token: 0x0600F76A RID: 63338
		// (remove) Token: 0x0600F76B RID: 63339
		public virtual extern event HTMLTextContainerEvents2_onresizestartEventHandler HTMLTextContainerEvents2_Event_onresizestart;

		// Token: 0x14001E74 RID: 7796
		// (add) Token: 0x0600F76C RID: 63340
		// (remove) Token: 0x0600F76D RID: 63341
		public virtual extern event HTMLTextContainerEvents2_onresizeendEventHandler HTMLTextContainerEvents2_Event_onresizeend;

		// Token: 0x14001E75 RID: 7797
		// (add) Token: 0x0600F76E RID: 63342
		// (remove) Token: 0x0600F76F RID: 63343
		public virtual extern event HTMLTextContainerEvents2_onmousewheelEventHandler HTMLTextContainerEvents2_Event_onmousewheel;

		// Token: 0x14001E76 RID: 7798
		// (add) Token: 0x0600F770 RID: 63344
		// (remove) Token: 0x0600F771 RID: 63345
		public virtual extern event HTMLTextContainerEvents2_onchangeEventHandler HTMLTextContainerEvents2_Event_onchange;

		// Token: 0x14001E77 RID: 7799
		// (add) Token: 0x0600F772 RID: 63346
		// (remove) Token: 0x0600F773 RID: 63347
		public virtual extern event HTMLTextContainerEvents2_onselectEventHandler HTMLTextContainerEvents2_Event_onselect;
	}
}
