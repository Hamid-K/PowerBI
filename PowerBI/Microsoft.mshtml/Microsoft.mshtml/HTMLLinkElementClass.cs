using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000154 RID: 340
	[Guid("3050F277-98B5-11CF-BB82-00AA00BDCE0B")]
	[TypeLibType(2)]
	[ComSourceInterfaces("mshtml.HTMLLinkElementEvents\0mshtml.HTMLLinkElementEvents2\0\0")]
	[ClassInterface(0)]
	[ComImport]
	public class HTMLLinkElementClass : DispHTMLLinkElement, HTMLLinkElement, HTMLLinkElementEvents_Event, IHTMLElement, IHTMLElement2, IHTMLElement3, IHTMLElement4, IHTMLUniqueName, IHTMLDOMNode, IHTMLDOMNode2, IHTMLLinkElement, IHTMLLinkElement2, IHTMLLinkElement3, HTMLLinkElementEvents2_Event
	{
		// Token: 0x060015E7 RID: 5607
		[MethodImpl(4096, MethodCodeType = 3)]
		public extern HTMLLinkElementClass();

		// Token: 0x060015E8 RID: 5608
		[DispId(-2147417611)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void setAttribute([MarshalAs(19)] [In] string strAttributeName, [MarshalAs(27)] [In] object AttributeValue, [In] int lFlags = 1);

		// Token: 0x060015E9 RID: 5609
		[DispId(-2147417610)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		public virtual extern object getAttribute([MarshalAs(19)] [In] string strAttributeName, [In] int lFlags = 0);

		// Token: 0x060015EA RID: 5610
		[DispId(-2147417609)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool removeAttribute([MarshalAs(19)] [In] string strAttributeName, [In] int lFlags = 1);

		// Token: 0x170009B3 RID: 2483
		// (get) Token: 0x060015EC RID: 5612
		// (set) Token: 0x060015EB RID: 5611
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

		// Token: 0x170009B4 RID: 2484
		// (get) Token: 0x060015EE RID: 5614
		// (set) Token: 0x060015ED RID: 5613
		[DispId(-2147417110)]
		public virtual extern string id
		{
			[DispId(-2147417110)]
			[TypeLibFunc(4)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(4)]
			[DispId(-2147417110)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x170009B5 RID: 2485
		// (get) Token: 0x060015EF RID: 5615
		[DispId(-2147417108)]
		public virtual extern string tagName
		{
			[DispId(-2147417108)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170009B6 RID: 2486
		// (get) Token: 0x060015F0 RID: 5616
		[DispId(-2147418104)]
		public virtual extern IHTMLElement parentElement
		{
			[DispId(-2147418104)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170009B7 RID: 2487
		// (get) Token: 0x060015F1 RID: 5617
		[DispId(-2147418038)]
		public virtual extern IHTMLStyle style
		{
			[TypeLibFunc(1024)]
			[DispId(-2147418038)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170009B8 RID: 2488
		// (get) Token: 0x060015F3 RID: 5619
		// (set) Token: 0x060015F2 RID: 5618
		[DispId(-2147412099)]
		public virtual extern object onhelp
		{
			[DispId(-2147412099)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412099)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170009B9 RID: 2489
		// (get) Token: 0x060015F5 RID: 5621
		// (set) Token: 0x060015F4 RID: 5620
		[DispId(-2147412104)]
		public virtual extern object onclick
		{
			[TypeLibFunc(20)]
			[DispId(-2147412104)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412104)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170009BA RID: 2490
		// (get) Token: 0x060015F7 RID: 5623
		// (set) Token: 0x060015F6 RID: 5622
		[DispId(-2147412103)]
		public virtual extern object ondblclick
		{
			[TypeLibFunc(20)]
			[DispId(-2147412103)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412103)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170009BB RID: 2491
		// (get) Token: 0x060015F9 RID: 5625
		// (set) Token: 0x060015F8 RID: 5624
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

		// Token: 0x170009BC RID: 2492
		// (get) Token: 0x060015FB RID: 5627
		// (set) Token: 0x060015FA RID: 5626
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

		// Token: 0x170009BD RID: 2493
		// (get) Token: 0x060015FD RID: 5629
		// (set) Token: 0x060015FC RID: 5628
		[DispId(-2147412105)]
		public virtual extern object onkeypress
		{
			[TypeLibFunc(20)]
			[DispId(-2147412105)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412105)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170009BE RID: 2494
		// (get) Token: 0x060015FF RID: 5631
		// (set) Token: 0x060015FE RID: 5630
		[DispId(-2147412111)]
		public virtual extern object onmouseout
		{
			[DispId(-2147412111)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412111)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170009BF RID: 2495
		// (get) Token: 0x06001601 RID: 5633
		// (set) Token: 0x06001600 RID: 5632
		[DispId(-2147412112)]
		public virtual extern object onmouseover
		{
			[DispId(-2147412112)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412112)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170009C0 RID: 2496
		// (get) Token: 0x06001603 RID: 5635
		// (set) Token: 0x06001602 RID: 5634
		[DispId(-2147412108)]
		public virtual extern object onmousemove
		{
			[DispId(-2147412108)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412108)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170009C1 RID: 2497
		// (get) Token: 0x06001605 RID: 5637
		// (set) Token: 0x06001604 RID: 5636
		[DispId(-2147412110)]
		public virtual extern object onmousedown
		{
			[TypeLibFunc(20)]
			[DispId(-2147412110)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412110)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170009C2 RID: 2498
		// (get) Token: 0x06001607 RID: 5639
		// (set) Token: 0x06001606 RID: 5638
		[DispId(-2147412109)]
		public virtual extern object onmouseup
		{
			[TypeLibFunc(20)]
			[DispId(-2147412109)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412109)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170009C3 RID: 2499
		// (get) Token: 0x06001608 RID: 5640
		[DispId(-2147417094)]
		public virtual extern object document
		{
			[DispId(-2147417094)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x170009C4 RID: 2500
		// (get) Token: 0x0600160A RID: 5642
		// (set) Token: 0x06001609 RID: 5641
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

		// Token: 0x170009C5 RID: 2501
		// (get) Token: 0x0600160C RID: 5644
		// (set) Token: 0x0600160B RID: 5643
		[DispId(-2147413012)]
		public virtual extern string language
		{
			[DispId(-2147413012)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413012)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x170009C6 RID: 2502
		// (get) Token: 0x0600160E RID: 5646
		// (set) Token: 0x0600160D RID: 5645
		[DispId(-2147412075)]
		public virtual extern object onselectstart
		{
			[DispId(-2147412075)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412075)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x0600160F RID: 5647
		[DispId(-2147417093)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void scrollIntoView([MarshalAs(27)] [In] [Optional] object varargStart);

		// Token: 0x06001610 RID: 5648
		[DispId(-2147417092)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool contains([MarshalAs(28)] [In] IHTMLElement pChild);

		// Token: 0x170009C7 RID: 2503
		// (get) Token: 0x06001611 RID: 5649
		[DispId(-2147417088)]
		public virtual extern int sourceIndex
		{
			[TypeLibFunc(4)]
			[DispId(-2147417088)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170009C8 RID: 2504
		// (get) Token: 0x06001612 RID: 5650
		[DispId(-2147417087)]
		public virtual extern object recordNumber
		{
			[DispId(-2147417087)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x170009C9 RID: 2505
		// (get) Token: 0x06001614 RID: 5652
		// (set) Token: 0x06001613 RID: 5651
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

		// Token: 0x170009CA RID: 2506
		// (get) Token: 0x06001615 RID: 5653
		[DispId(-2147417104)]
		public virtual extern int offsetLeft
		{
			[DispId(-2147417104)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170009CB RID: 2507
		// (get) Token: 0x06001616 RID: 5654
		[DispId(-2147417103)]
		public virtual extern int offsetTop
		{
			[DispId(-2147417103)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170009CC RID: 2508
		// (get) Token: 0x06001617 RID: 5655
		[DispId(-2147417102)]
		public virtual extern int offsetWidth
		{
			[DispId(-2147417102)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170009CD RID: 2509
		// (get) Token: 0x06001618 RID: 5656
		[DispId(-2147417101)]
		public virtual extern int offsetHeight
		{
			[DispId(-2147417101)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170009CE RID: 2510
		// (get) Token: 0x06001619 RID: 5657
		[DispId(-2147417100)]
		public virtual extern IHTMLElement offsetParent
		{
			[DispId(-2147417100)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170009CF RID: 2511
		// (get) Token: 0x0600161B RID: 5659
		// (set) Token: 0x0600161A RID: 5658
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

		// Token: 0x170009D0 RID: 2512
		// (get) Token: 0x0600161D RID: 5661
		// (set) Token: 0x0600161C RID: 5660
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

		// Token: 0x170009D1 RID: 2513
		// (get) Token: 0x0600161F RID: 5663
		// (set) Token: 0x0600161E RID: 5662
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

		// Token: 0x170009D2 RID: 2514
		// (get) Token: 0x06001621 RID: 5665
		// (set) Token: 0x06001620 RID: 5664
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

		// Token: 0x06001622 RID: 5666
		[DispId(-2147417082)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void insertAdjacentHTML([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string html);

		// Token: 0x06001623 RID: 5667
		[DispId(-2147417081)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void insertAdjacentText([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string text);

		// Token: 0x170009D3 RID: 2515
		// (get) Token: 0x06001624 RID: 5668
		[DispId(-2147417080)]
		public virtual extern IHTMLElement parentTextEdit
		{
			[DispId(-2147417080)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170009D4 RID: 2516
		// (get) Token: 0x06001625 RID: 5669
		[DispId(-2147417078)]
		public virtual extern bool isTextEdit
		{
			[DispId(-2147417078)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x06001626 RID: 5670
		[DispId(-2147417079)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void click();

		// Token: 0x170009D5 RID: 2517
		// (get) Token: 0x06001627 RID: 5671
		[DispId(-2147417077)]
		public virtual extern IHTMLFiltersCollection filters
		{
			[DispId(-2147417077)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170009D6 RID: 2518
		// (get) Token: 0x06001629 RID: 5673
		// (set) Token: 0x06001628 RID: 5672
		[DispId(-2147412077)]
		public virtual extern object ondragstart
		{
			[TypeLibFunc(20)]
			[DispId(-2147412077)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412077)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x0600162A RID: 5674
		[DispId(-2147417076)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string toString();

		// Token: 0x170009D7 RID: 2519
		// (get) Token: 0x0600162C RID: 5676
		// (set) Token: 0x0600162B RID: 5675
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

		// Token: 0x170009D8 RID: 2520
		// (get) Token: 0x0600162E RID: 5678
		// (set) Token: 0x0600162D RID: 5677
		[DispId(-2147412090)]
		public virtual extern object onafterupdate
		{
			[TypeLibFunc(20)]
			[DispId(-2147412090)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412090)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170009D9 RID: 2521
		// (get) Token: 0x06001630 RID: 5680
		// (set) Token: 0x0600162F RID: 5679
		[DispId(-2147412074)]
		public virtual extern object onerrorupdate
		{
			[DispId(-2147412074)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412074)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170009DA RID: 2522
		// (get) Token: 0x06001632 RID: 5682
		// (set) Token: 0x06001631 RID: 5681
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

		// Token: 0x170009DB RID: 2523
		// (get) Token: 0x06001634 RID: 5684
		// (set) Token: 0x06001633 RID: 5683
		[DispId(-2147412093)]
		public virtual extern object onrowenter
		{
			[TypeLibFunc(20)]
			[DispId(-2147412093)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412093)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170009DC RID: 2524
		// (get) Token: 0x06001636 RID: 5686
		// (set) Token: 0x06001635 RID: 5685
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

		// Token: 0x170009DD RID: 2525
		// (get) Token: 0x06001638 RID: 5688
		// (set) Token: 0x06001637 RID: 5687
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

		// Token: 0x170009DE RID: 2526
		// (get) Token: 0x0600163A RID: 5690
		// (set) Token: 0x06001639 RID: 5689
		[DispId(-2147412070)]
		public virtual extern object ondatasetcomplete
		{
			[DispId(-2147412070)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412070)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170009DF RID: 2527
		// (get) Token: 0x0600163C RID: 5692
		// (set) Token: 0x0600163B RID: 5691
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

		// Token: 0x170009E0 RID: 2528
		// (get) Token: 0x0600163D RID: 5693
		[DispId(-2147417075)]
		public virtual extern object children
		{
			[DispId(-2147417075)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x170009E1 RID: 2529
		// (get) Token: 0x0600163E RID: 5694
		[DispId(-2147417074)]
		public virtual extern object all
		{
			[DispId(-2147417074)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x170009E2 RID: 2530
		// (get) Token: 0x0600163F RID: 5695
		[DispId(-2147417073)]
		public virtual extern string scopeName
		{
			[DispId(-2147417073)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x06001640 RID: 5696
		[DispId(-2147417072)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void setCapture([In] bool containerCapture = true);

		// Token: 0x06001641 RID: 5697
		[DispId(-2147417071)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void releaseCapture();

		// Token: 0x170009E3 RID: 2531
		// (get) Token: 0x06001643 RID: 5699
		// (set) Token: 0x06001642 RID: 5698
		[DispId(-2147412066)]
		public virtual extern object onlosecapture
		{
			[DispId(-2147412066)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412066)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x06001644 RID: 5700
		[DispId(-2147417070)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string componentFromPoint([In] int x, [In] int y);

		// Token: 0x06001645 RID: 5701
		[DispId(-2147417069)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void doScroll([MarshalAs(27)] [In] [Optional] object component);

		// Token: 0x170009E4 RID: 2532
		// (get) Token: 0x06001647 RID: 5703
		// (set) Token: 0x06001646 RID: 5702
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

		// Token: 0x170009E5 RID: 2533
		// (get) Token: 0x06001649 RID: 5705
		// (set) Token: 0x06001648 RID: 5704
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

		// Token: 0x170009E6 RID: 2534
		// (get) Token: 0x0600164B RID: 5707
		// (set) Token: 0x0600164A RID: 5706
		[DispId(-2147412062)]
		public virtual extern object ondragend
		{
			[TypeLibFunc(20)]
			[DispId(-2147412062)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412062)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170009E7 RID: 2535
		// (get) Token: 0x0600164D RID: 5709
		// (set) Token: 0x0600164C RID: 5708
		[DispId(-2147412061)]
		public virtual extern object ondragenter
		{
			[TypeLibFunc(20)]
			[DispId(-2147412061)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412061)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170009E8 RID: 2536
		// (get) Token: 0x0600164F RID: 5711
		// (set) Token: 0x0600164E RID: 5710
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

		// Token: 0x170009E9 RID: 2537
		// (get) Token: 0x06001651 RID: 5713
		// (set) Token: 0x06001650 RID: 5712
		[DispId(-2147412059)]
		public virtual extern object ondragleave
		{
			[DispId(-2147412059)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412059)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170009EA RID: 2538
		// (get) Token: 0x06001653 RID: 5715
		// (set) Token: 0x06001652 RID: 5714
		[DispId(-2147412058)]
		public virtual extern object ondrop
		{
			[TypeLibFunc(20)]
			[DispId(-2147412058)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412058)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170009EB RID: 2539
		// (get) Token: 0x06001655 RID: 5717
		// (set) Token: 0x06001654 RID: 5716
		[DispId(-2147412054)]
		public virtual extern object onbeforecut
		{
			[DispId(-2147412054)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412054)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170009EC RID: 2540
		// (get) Token: 0x06001657 RID: 5719
		// (set) Token: 0x06001656 RID: 5718
		[DispId(-2147412057)]
		public virtual extern object oncut
		{
			[TypeLibFunc(20)]
			[DispId(-2147412057)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412057)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170009ED RID: 2541
		// (get) Token: 0x06001659 RID: 5721
		// (set) Token: 0x06001658 RID: 5720
		[DispId(-2147412053)]
		public virtual extern object onbeforecopy
		{
			[TypeLibFunc(20)]
			[DispId(-2147412053)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412053)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170009EE RID: 2542
		// (get) Token: 0x0600165B RID: 5723
		// (set) Token: 0x0600165A RID: 5722
		[DispId(-2147412056)]
		public virtual extern object oncopy
		{
			[DispId(-2147412056)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412056)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170009EF RID: 2543
		// (get) Token: 0x0600165D RID: 5725
		// (set) Token: 0x0600165C RID: 5724
		[DispId(-2147412052)]
		public virtual extern object onbeforepaste
		{
			[DispId(-2147412052)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412052)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170009F0 RID: 2544
		// (get) Token: 0x0600165F RID: 5727
		// (set) Token: 0x0600165E RID: 5726
		[DispId(-2147412055)]
		public virtual extern object onpaste
		{
			[TypeLibFunc(20)]
			[DispId(-2147412055)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412055)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170009F1 RID: 2545
		// (get) Token: 0x06001660 RID: 5728
		[DispId(-2147417105)]
		public virtual extern IHTMLCurrentStyle currentStyle
		{
			[DispId(-2147417105)]
			[TypeLibFunc(1024)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170009F2 RID: 2546
		// (get) Token: 0x06001662 RID: 5730
		// (set) Token: 0x06001661 RID: 5729
		[DispId(-2147412065)]
		public virtual extern object onpropertychange
		{
			[DispId(-2147412065)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412065)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x06001663 RID: 5731
		[DispId(-2147417068)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLRectCollection getClientRects();

		// Token: 0x06001664 RID: 5732
		[DispId(-2147417067)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLRect getBoundingClientRect();

		// Token: 0x06001665 RID: 5733
		[DispId(-2147417608)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void setExpression([MarshalAs(19)] [In] string propname, [MarshalAs(19)] [In] string expression, [MarshalAs(19)] [In] string language = "");

		// Token: 0x06001666 RID: 5734
		[DispId(-2147417607)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		public virtual extern object getExpression([MarshalAs(19)] [In] string propname);

		// Token: 0x06001667 RID: 5735
		[DispId(-2147417606)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool removeExpression([MarshalAs(19)] [In] string propname);

		// Token: 0x170009F3 RID: 2547
		// (get) Token: 0x06001669 RID: 5737
		// (set) Token: 0x06001668 RID: 5736
		[DispId(-2147418097)]
		public virtual extern short tabIndex
		{
			[DispId(-2147418097)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147418097)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x0600166A RID: 5738
		[DispId(-2147416112)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void focus();

		// Token: 0x170009F4 RID: 2548
		// (get) Token: 0x0600166C RID: 5740
		// (set) Token: 0x0600166B RID: 5739
		[DispId(-2147416107)]
		public virtual extern string accessKey
		{
			[TypeLibFunc(20)]
			[DispId(-2147416107)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147416107)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x170009F5 RID: 2549
		// (get) Token: 0x0600166E RID: 5742
		// (set) Token: 0x0600166D RID: 5741
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

		// Token: 0x170009F6 RID: 2550
		// (get) Token: 0x06001670 RID: 5744
		// (set) Token: 0x0600166F RID: 5743
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

		// Token: 0x170009F7 RID: 2551
		// (get) Token: 0x06001672 RID: 5746
		// (set) Token: 0x06001671 RID: 5745
		[DispId(-2147412076)]
		public virtual extern object onresize
		{
			[DispId(-2147412076)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412076)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x06001673 RID: 5747
		[DispId(-2147416110)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void blur();

		// Token: 0x06001674 RID: 5748
		[DispId(-2147416095)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void addFilter([MarshalAs(25)] [In] object pUnk);

		// Token: 0x06001675 RID: 5749
		[DispId(-2147416094)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void removeFilter([MarshalAs(25)] [In] object pUnk);

		// Token: 0x170009F8 RID: 2552
		// (get) Token: 0x06001676 RID: 5750
		[DispId(-2147416093)]
		public virtual extern int clientHeight
		{
			[DispId(-2147416093)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170009F9 RID: 2553
		// (get) Token: 0x06001677 RID: 5751
		[DispId(-2147416092)]
		public virtual extern int clientWidth
		{
			[DispId(-2147416092)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170009FA RID: 2554
		// (get) Token: 0x06001678 RID: 5752
		[DispId(-2147416091)]
		public virtual extern int clientTop
		{
			[TypeLibFunc(20)]
			[DispId(-2147416091)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170009FB RID: 2555
		// (get) Token: 0x06001679 RID: 5753
		[DispId(-2147416090)]
		public virtual extern int clientLeft
		{
			[TypeLibFunc(20)]
			[DispId(-2147416090)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x0600167A RID: 5754
		[DispId(-2147417605)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool attachEvent([MarshalAs(19)] [In] string @event, [MarshalAs(26)] [In] object pdisp);

		// Token: 0x0600167B RID: 5755
		[DispId(-2147417604)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void detachEvent([MarshalAs(19)] [In] string @event, [MarshalAs(26)] [In] object pdisp);

		// Token: 0x170009FC RID: 2556
		// (get) Token: 0x0600167C RID: 5756
		[DispId(-2147412996)]
		public virtual extern object readyState
		{
			[DispId(-2147412996)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x170009FD RID: 2557
		// (get) Token: 0x0600167E RID: 5758
		// (set) Token: 0x0600167D RID: 5757
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

		// Token: 0x170009FE RID: 2558
		// (get) Token: 0x06001680 RID: 5760
		// (set) Token: 0x0600167F RID: 5759
		[DispId(-2147412050)]
		public virtual extern object onrowsdelete
		{
			[TypeLibFunc(20)]
			[DispId(-2147412050)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412050)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170009FF RID: 2559
		// (get) Token: 0x06001682 RID: 5762
		// (set) Token: 0x06001681 RID: 5761
		[DispId(-2147412049)]
		public virtual extern object onrowsinserted
		{
			[DispId(-2147412049)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412049)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17000A00 RID: 2560
		// (get) Token: 0x06001684 RID: 5764
		// (set) Token: 0x06001683 RID: 5763
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

		// Token: 0x17000A01 RID: 2561
		// (get) Token: 0x06001686 RID: 5766
		// (set) Token: 0x06001685 RID: 5765
		[DispId(-2147412995)]
		public virtual extern string dir
		{
			[TypeLibFunc(20)]
			[DispId(-2147412995)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412995)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x06001687 RID: 5767
		[DispId(-2147417056)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(26)]
		public virtual extern object createControlRange();

		// Token: 0x17000A02 RID: 2562
		// (get) Token: 0x06001688 RID: 5768
		[DispId(-2147417055)]
		public virtual extern int scrollHeight
		{
			[TypeLibFunc(20)]
			[DispId(-2147417055)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17000A03 RID: 2563
		// (get) Token: 0x06001689 RID: 5769
		[DispId(-2147417054)]
		public virtual extern int scrollWidth
		{
			[TypeLibFunc(20)]
			[DispId(-2147417054)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17000A04 RID: 2564
		// (get) Token: 0x0600168B RID: 5771
		// (set) Token: 0x0600168A RID: 5770
		[DispId(-2147417053)]
		public virtual extern int scrollTop
		{
			[TypeLibFunc(20)]
			[DispId(-2147417053)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[DispId(-2147417053)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x17000A05 RID: 2565
		// (get) Token: 0x0600168D RID: 5773
		// (set) Token: 0x0600168C RID: 5772
		[DispId(-2147417052)]
		public virtual extern int scrollLeft
		{
			[TypeLibFunc(20)]
			[DispId(-2147417052)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[DispId(-2147417052)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x0600168E RID: 5774
		[DispId(-2147417050)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void clearAttributes();

		// Token: 0x17000A06 RID: 2566
		// (get) Token: 0x06001690 RID: 5776
		// (set) Token: 0x0600168F RID: 5775
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

		// Token: 0x06001691 RID: 5777
		[DispId(-2147417043)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElement insertAdjacentElement([MarshalAs(19)] [In] string where, [MarshalAs(28)] [In] IHTMLElement insertedElement);

		// Token: 0x06001692 RID: 5778
		[DispId(-2147417047)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElement applyElement([MarshalAs(28)] [In] IHTMLElement apply, [MarshalAs(19)] [In] string where);

		// Token: 0x06001693 RID: 5779
		[DispId(-2147417042)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string getAdjacentText([MarshalAs(19)] [In] string where);

		// Token: 0x06001694 RID: 5780
		[DispId(-2147417041)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string replaceAdjacentText([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string newText);

		// Token: 0x17000A07 RID: 2567
		// (get) Token: 0x06001695 RID: 5781
		[DispId(-2147417040)]
		public virtual extern bool canHaveChildren
		{
			[DispId(-2147417040)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x06001696 RID: 5782
		[DispId(-2147417032)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern int addBehavior([MarshalAs(19)] [In] string bstrUrl, [MarshalAs(27)] [In] [Optional] ref object pvarFactory);

		// Token: 0x06001697 RID: 5783
		[DispId(-2147417031)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool removeBehavior([In] int cookie);

		// Token: 0x17000A08 RID: 2568
		// (get) Token: 0x06001698 RID: 5784
		[DispId(-2147417048)]
		public virtual extern IHTMLStyle runtimeStyle
		{
			[TypeLibFunc(1024)]
			[DispId(-2147417048)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17000A09 RID: 2569
		// (get) Token: 0x06001699 RID: 5785
		[DispId(-2147417030)]
		public virtual extern object behaviorUrns
		{
			[DispId(-2147417030)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17000A0A RID: 2570
		// (get) Token: 0x0600169B RID: 5787
		// (set) Token: 0x0600169A RID: 5786
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

		// Token: 0x17000A0B RID: 2571
		// (get) Token: 0x0600169D RID: 5789
		// (set) Token: 0x0600169C RID: 5788
		[DispId(-2147412043)]
		public virtual extern object onbeforeeditfocus
		{
			[TypeLibFunc(20)]
			[DispId(-2147412043)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412043)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17000A0C RID: 2572
		// (get) Token: 0x0600169E RID: 5790
		[DispId(-2147417028)]
		public virtual extern int readyStateValue
		{
			[TypeLibFunc(65)]
			[DispId(-2147417028)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x0600169F RID: 5791
		[DispId(-2147417027)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElementCollection getElementsByTagName([MarshalAs(19)] [In] string v);

		// Token: 0x060016A0 RID: 5792
		[DispId(-2147417016)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void mergeAttributes([MarshalAs(28)] [In] IHTMLElement mergeThis, [MarshalAs(27)] [In] [Optional] ref object pvarFlags);

		// Token: 0x17000A0D RID: 2573
		// (get) Token: 0x060016A1 RID: 5793
		[DispId(-2147417015)]
		public virtual extern bool isMultiLine
		{
			[DispId(-2147417015)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17000A0E RID: 2574
		// (get) Token: 0x060016A2 RID: 5794
		[DispId(-2147417014)]
		public virtual extern bool canHaveHTML
		{
			[DispId(-2147417014)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17000A0F RID: 2575
		// (get) Token: 0x060016A4 RID: 5796
		// (set) Token: 0x060016A3 RID: 5795
		[DispId(-2147412039)]
		public virtual extern object onlayoutcomplete
		{
			[DispId(-2147412039)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412039)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17000A10 RID: 2576
		// (get) Token: 0x060016A6 RID: 5798
		// (set) Token: 0x060016A5 RID: 5797
		[DispId(-2147412038)]
		public virtual extern object onpage
		{
			[DispId(-2147412038)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412038)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17000A11 RID: 2577
		// (get) Token: 0x060016A8 RID: 5800
		// (set) Token: 0x060016A7 RID: 5799
		[DispId(-2147417012)]
		public virtual extern bool inflateBlock
		{
			[TypeLibFunc(1089)]
			[DispId(-2147417012)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[TypeLibFunc(1089)]
			[DispId(-2147417012)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x17000A12 RID: 2578
		// (get) Token: 0x060016AA RID: 5802
		// (set) Token: 0x060016A9 RID: 5801
		[DispId(-2147412035)]
		public virtual extern object onbeforedeactivate
		{
			[TypeLibFunc(20)]
			[DispId(-2147412035)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412035)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x060016AB RID: 5803
		[DispId(-2147417011)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void setActive();

		// Token: 0x17000A13 RID: 2579
		// (get) Token: 0x060016AD RID: 5805
		// (set) Token: 0x060016AC RID: 5804
		[DispId(-2147412950)]
		public virtual extern string contentEditable
		{
			[DispId(-2147412950)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147412950)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17000A14 RID: 2580
		// (get) Token: 0x060016AE RID: 5806
		[DispId(-2147417010)]
		public virtual extern bool isContentEditable
		{
			[DispId(-2147417010)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17000A15 RID: 2581
		// (get) Token: 0x060016B0 RID: 5808
		// (set) Token: 0x060016AF RID: 5807
		[DispId(-2147412949)]
		public virtual extern bool hideFocus
		{
			[DispId(-2147412949)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412949)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x17000A16 RID: 2582
		// (get) Token: 0x060016B2 RID: 5810
		// (set) Token: 0x060016B1 RID: 5809
		[DispId(-2147418036)]
		public virtual extern bool disabled
		{
			[TypeLibFunc(20)]
			[DispId(-2147418036)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147418036)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x17000A17 RID: 2583
		// (get) Token: 0x060016B3 RID: 5811
		[DispId(-2147417007)]
		public virtual extern bool isDisabled
		{
			[DispId(-2147417007)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17000A18 RID: 2584
		// (get) Token: 0x060016B5 RID: 5813
		// (set) Token: 0x060016B4 RID: 5812
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

		// Token: 0x17000A19 RID: 2585
		// (get) Token: 0x060016B7 RID: 5815
		// (set) Token: 0x060016B6 RID: 5814
		[DispId(-2147412033)]
		public virtual extern object oncontrolselect
		{
			[TypeLibFunc(20)]
			[DispId(-2147412033)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412033)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x060016B8 RID: 5816
		[DispId(-2147417006)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool FireEvent([MarshalAs(19)] [In] string bstrEventName, [MarshalAs(27)] [In] [Optional] ref object pvarEventObject);

		// Token: 0x17000A1A RID: 2586
		// (get) Token: 0x060016BA RID: 5818
		// (set) Token: 0x060016B9 RID: 5817
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

		// Token: 0x17000A1B RID: 2587
		// (get) Token: 0x060016BC RID: 5820
		// (set) Token: 0x060016BB RID: 5819
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

		// Token: 0x17000A1C RID: 2588
		// (get) Token: 0x060016BE RID: 5822
		// (set) Token: 0x060016BD RID: 5821
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

		// Token: 0x17000A1D RID: 2589
		// (get) Token: 0x060016C0 RID: 5824
		// (set) Token: 0x060016BF RID: 5823
		[DispId(-2147412030)]
		public virtual extern object onmoveend
		{
			[TypeLibFunc(20)]
			[DispId(-2147412030)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412030)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17000A1E RID: 2590
		// (get) Token: 0x060016C2 RID: 5826
		// (set) Token: 0x060016C1 RID: 5825
		[DispId(-2147412027)]
		public virtual extern object onmouseenter
		{
			[TypeLibFunc(20)]
			[DispId(-2147412027)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412027)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17000A1F RID: 2591
		// (get) Token: 0x060016C4 RID: 5828
		// (set) Token: 0x060016C3 RID: 5827
		[DispId(-2147412026)]
		public virtual extern object onmouseleave
		{
			[DispId(-2147412026)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412026)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17000A20 RID: 2592
		// (get) Token: 0x060016C6 RID: 5830
		// (set) Token: 0x060016C5 RID: 5829
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

		// Token: 0x17000A21 RID: 2593
		// (get) Token: 0x060016C8 RID: 5832
		// (set) Token: 0x060016C7 RID: 5831
		[DispId(-2147412024)]
		public virtual extern object ondeactivate
		{
			[DispId(-2147412024)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412024)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x060016C9 RID: 5833
		[DispId(-2147417005)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool dragDrop();

		// Token: 0x17000A22 RID: 2594
		// (get) Token: 0x060016CA RID: 5834
		[DispId(-2147417004)]
		public virtual extern int glyphMode
		{
			[TypeLibFunc(1089)]
			[DispId(-2147417004)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17000A23 RID: 2595
		// (get) Token: 0x060016CC RID: 5836
		// (set) Token: 0x060016CB RID: 5835
		[DispId(-2147412036)]
		public virtual extern object onmousewheel
		{
			[TypeLibFunc(20)]
			[DispId(-2147412036)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412036)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x060016CD RID: 5837
		[DispId(-2147417000)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void normalize();

		// Token: 0x060016CE RID: 5838
		[DispId(-2147417003)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute getAttributeNode([MarshalAs(19)] [In] string bstrName);

		// Token: 0x060016CF RID: 5839
		[DispId(-2147417002)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute setAttributeNode([MarshalAs(28)] [In] IHTMLDOMAttribute pattr);

		// Token: 0x060016D0 RID: 5840
		[DispId(-2147417001)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute removeAttributeNode([MarshalAs(28)] [In] IHTMLDOMAttribute pattr);

		// Token: 0x17000A24 RID: 2596
		// (get) Token: 0x060016D2 RID: 5842
		// (set) Token: 0x060016D1 RID: 5841
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

		// Token: 0x17000A25 RID: 2597
		// (get) Token: 0x060016D4 RID: 5844
		// (set) Token: 0x060016D3 RID: 5843
		[DispId(-2147412021)]
		public virtual extern object onfocusin
		{
			[TypeLibFunc(20)]
			[DispId(-2147412021)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412021)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17000A26 RID: 2598
		// (get) Token: 0x060016D6 RID: 5846
		// (set) Token: 0x060016D5 RID: 5845
		[DispId(-2147412020)]
		public virtual extern object onfocusout
		{
			[DispId(-2147412020)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412020)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17000A27 RID: 2599
		// (get) Token: 0x060016D7 RID: 5847
		[DispId(-2147417058)]
		public virtual extern int uniqueNumber
		{
			[DispId(-2147417058)]
			[TypeLibFunc(64)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17000A28 RID: 2600
		// (get) Token: 0x060016D8 RID: 5848
		[DispId(-2147417057)]
		public virtual extern string uniqueID
		{
			[TypeLibFunc(64)]
			[DispId(-2147417057)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17000A29 RID: 2601
		// (get) Token: 0x060016D9 RID: 5849
		[DispId(-2147417066)]
		public virtual extern int nodeType
		{
			[DispId(-2147417066)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17000A2A RID: 2602
		// (get) Token: 0x060016DA RID: 5850
		[DispId(-2147417065)]
		public virtual extern IHTMLDOMNode parentNode
		{
			[DispId(-2147417065)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x060016DB RID: 5851
		[DispId(-2147417064)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool hasChildNodes();

		// Token: 0x17000A2B RID: 2603
		// (get) Token: 0x060016DC RID: 5852
		[DispId(-2147417063)]
		public virtual extern object childNodes
		{
			[DispId(-2147417063)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17000A2C RID: 2604
		// (get) Token: 0x060016DD RID: 5853
		[DispId(-2147417062)]
		public virtual extern object attributes
		{
			[DispId(-2147417062)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x060016DE RID: 5854
		[DispId(-2147417061)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode insertBefore([MarshalAs(28)] [In] IHTMLDOMNode newChild, [MarshalAs(27)] [In] [Optional] object refChild);

		// Token: 0x060016DF RID: 5855
		[DispId(-2147417060)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode removeChild([MarshalAs(28)] [In] IHTMLDOMNode oldChild);

		// Token: 0x060016E0 RID: 5856
		[DispId(-2147417059)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode replaceChild([MarshalAs(28)] [In] IHTMLDOMNode newChild, [MarshalAs(28)] [In] IHTMLDOMNode oldChild);

		// Token: 0x060016E1 RID: 5857
		[DispId(-2147417051)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode cloneNode([In] bool fDeep);

		// Token: 0x060016E2 RID: 5858
		[DispId(-2147417046)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode removeNode([In] bool fDeep = false);

		// Token: 0x060016E3 RID: 5859
		[DispId(-2147417044)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode swapNode([MarshalAs(28)] [In] IHTMLDOMNode otherNode);

		// Token: 0x060016E4 RID: 5860
		[DispId(-2147417045)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode replaceNode([MarshalAs(28)] [In] IHTMLDOMNode replacement);

		// Token: 0x060016E5 RID: 5861
		[DispId(-2147417039)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode appendChild([MarshalAs(28)] [In] IHTMLDOMNode newChild);

		// Token: 0x17000A2D RID: 2605
		// (get) Token: 0x060016E6 RID: 5862
		[DispId(-2147417038)]
		public virtual extern string nodeName
		{
			[DispId(-2147417038)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17000A2E RID: 2606
		// (get) Token: 0x060016E8 RID: 5864
		// (set) Token: 0x060016E7 RID: 5863
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

		// Token: 0x17000A2F RID: 2607
		// (get) Token: 0x060016E9 RID: 5865
		[DispId(-2147417036)]
		public virtual extern IHTMLDOMNode firstChild
		{
			[DispId(-2147417036)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17000A30 RID: 2608
		// (get) Token: 0x060016EA RID: 5866
		[DispId(-2147417035)]
		public virtual extern IHTMLDOMNode lastChild
		{
			[DispId(-2147417035)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17000A31 RID: 2609
		// (get) Token: 0x060016EB RID: 5867
		[DispId(-2147417034)]
		public virtual extern IHTMLDOMNode previousSibling
		{
			[DispId(-2147417034)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17000A32 RID: 2610
		// (get) Token: 0x060016EC RID: 5868
		[DispId(-2147417033)]
		public virtual extern IHTMLDOMNode nextSibling
		{
			[DispId(-2147417033)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17000A33 RID: 2611
		// (get) Token: 0x060016ED RID: 5869
		[DispId(-2147416999)]
		public virtual extern object ownerDocument
		{
			[DispId(-2147416999)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17000A34 RID: 2612
		// (get) Token: 0x060016EF RID: 5871
		// (set) Token: 0x060016EE RID: 5870
		[DispId(1005)]
		public virtual extern string href
		{
			[DispId(1005)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(1005)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17000A35 RID: 2613
		// (get) Token: 0x060016F1 RID: 5873
		// (set) Token: 0x060016F0 RID: 5872
		[DispId(1006)]
		public virtual extern string rel
		{
			[TypeLibFunc(20)]
			[DispId(1006)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(1006)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17000A36 RID: 2614
		// (get) Token: 0x060016F3 RID: 5875
		// (set) Token: 0x060016F2 RID: 5874
		[DispId(1007)]
		public virtual extern string rev
		{
			[TypeLibFunc(20)]
			[DispId(1007)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(1007)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17000A37 RID: 2615
		// (get) Token: 0x060016F5 RID: 5877
		// (set) Token: 0x060016F4 RID: 5876
		[DispId(1008)]
		public virtual extern string type
		{
			[TypeLibFunc(20)]
			[DispId(1008)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(1008)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17000A38 RID: 2616
		// (get) Token: 0x060016F7 RID: 5879
		// (set) Token: 0x060016F6 RID: 5878
		[DispId(-2147412080)]
		public virtual extern object onload
		{
			[TypeLibFunc(20)]
			[DispId(-2147412080)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412080)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17000A39 RID: 2617
		// (get) Token: 0x060016F9 RID: 5881
		// (set) Token: 0x060016F8 RID: 5880
		[DispId(-2147412083)]
		public virtual extern object onerror
		{
			[TypeLibFunc(20)]
			[DispId(-2147412083)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412083)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17000A3A RID: 2618
		// (get) Token: 0x060016FA RID: 5882
		[DispId(1014)]
		public virtual extern IHTMLStyleSheet styleSheet
		{
			[DispId(1014)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17000A3B RID: 2619
		// (get) Token: 0x060016FC RID: 5884
		// (set) Token: 0x060016FB RID: 5883
		[DispId(1016)]
		public virtual extern string media
		{
			[DispId(1016)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(1016)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17000A3C RID: 2620
		// (get) Token: 0x060016FE RID: 5886
		// (set) Token: 0x060016FD RID: 5885
		[DispId(1017)]
		public virtual extern string target
		{
			[DispId(1017)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(1017)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17000A3D RID: 2621
		// (get) Token: 0x06001700 RID: 5888
		// (set) Token: 0x060016FF RID: 5887
		[DispId(1018)]
		public virtual extern string charset
		{
			[TypeLibFunc(20)]
			[DispId(1018)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(1018)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17000A3E RID: 2622
		// (get) Token: 0x06001702 RID: 5890
		// (set) Token: 0x06001701 RID: 5889
		[DispId(1019)]
		public virtual extern string hreflang
		{
			[DispId(1019)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(1019)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x06001703 RID: 5891
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement_setAttribute([MarshalAs(19)] [In] string strAttributeName, [MarshalAs(27)] [In] object AttributeValue, [In] int lFlags = 1);

		// Token: 0x06001704 RID: 5892
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		public virtual extern object IHTMLElement_getAttribute([MarshalAs(19)] [In] string strAttributeName, [In] int lFlags = 0);

		// Token: 0x06001705 RID: 5893
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement_removeAttribute([MarshalAs(19)] [In] string strAttributeName, [In] int lFlags = 1);

		// Token: 0x17000A3F RID: 2623
		// (get) Token: 0x06001707 RID: 5895
		// (set) Token: 0x06001706 RID: 5894
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

		// Token: 0x17000A40 RID: 2624
		// (get) Token: 0x06001709 RID: 5897
		// (set) Token: 0x06001708 RID: 5896
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

		// Token: 0x17000A41 RID: 2625
		// (get) Token: 0x0600170A RID: 5898
		public virtual extern string IHTMLElement_tagName
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17000A42 RID: 2626
		// (get) Token: 0x0600170B RID: 5899
		public virtual extern IHTMLElement IHTMLElement_parentElement
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17000A43 RID: 2627
		// (get) Token: 0x0600170C RID: 5900
		public virtual extern IHTMLStyle IHTMLElement_style
		{
			[TypeLibFunc(1024)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17000A44 RID: 2628
		// (get) Token: 0x0600170E RID: 5902
		// (set) Token: 0x0600170D RID: 5901
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

		// Token: 0x17000A45 RID: 2629
		// (get) Token: 0x06001710 RID: 5904
		// (set) Token: 0x0600170F RID: 5903
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

		// Token: 0x17000A46 RID: 2630
		// (get) Token: 0x06001712 RID: 5906
		// (set) Token: 0x06001711 RID: 5905
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

		// Token: 0x17000A47 RID: 2631
		// (get) Token: 0x06001714 RID: 5908
		// (set) Token: 0x06001713 RID: 5907
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

		// Token: 0x17000A48 RID: 2632
		// (get) Token: 0x06001716 RID: 5910
		// (set) Token: 0x06001715 RID: 5909
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

		// Token: 0x17000A49 RID: 2633
		// (get) Token: 0x06001718 RID: 5912
		// (set) Token: 0x06001717 RID: 5911
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

		// Token: 0x17000A4A RID: 2634
		// (get) Token: 0x0600171A RID: 5914
		// (set) Token: 0x06001719 RID: 5913
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

		// Token: 0x17000A4B RID: 2635
		// (get) Token: 0x0600171C RID: 5916
		// (set) Token: 0x0600171B RID: 5915
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

		// Token: 0x17000A4C RID: 2636
		// (get) Token: 0x0600171E RID: 5918
		// (set) Token: 0x0600171D RID: 5917
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

		// Token: 0x17000A4D RID: 2637
		// (get) Token: 0x06001720 RID: 5920
		// (set) Token: 0x0600171F RID: 5919
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

		// Token: 0x17000A4E RID: 2638
		// (get) Token: 0x06001722 RID: 5922
		// (set) Token: 0x06001721 RID: 5921
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

		// Token: 0x17000A4F RID: 2639
		// (get) Token: 0x06001723 RID: 5923
		public virtual extern object IHTMLElement_document
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17000A50 RID: 2640
		// (get) Token: 0x06001725 RID: 5925
		// (set) Token: 0x06001724 RID: 5924
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

		// Token: 0x17000A51 RID: 2641
		// (get) Token: 0x06001727 RID: 5927
		// (set) Token: 0x06001726 RID: 5926
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

		// Token: 0x17000A52 RID: 2642
		// (get) Token: 0x06001729 RID: 5929
		// (set) Token: 0x06001728 RID: 5928
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

		// Token: 0x0600172A RID: 5930
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement_scrollIntoView([MarshalAs(27)] [In] [Optional] object varargStart);

		// Token: 0x0600172B RID: 5931
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement_contains([MarshalAs(28)] [In] IHTMLElement pChild);

		// Token: 0x17000A53 RID: 2643
		// (get) Token: 0x0600172C RID: 5932
		public virtual extern int IHTMLElement_sourceIndex
		{
			[TypeLibFunc(4)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17000A54 RID: 2644
		// (get) Token: 0x0600172D RID: 5933
		public virtual extern object IHTMLElement_recordNumber
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x17000A55 RID: 2645
		// (get) Token: 0x0600172F RID: 5935
		// (set) Token: 0x0600172E RID: 5934
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

		// Token: 0x17000A56 RID: 2646
		// (get) Token: 0x06001730 RID: 5936
		public virtual extern int IHTMLElement_offsetLeft
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17000A57 RID: 2647
		// (get) Token: 0x06001731 RID: 5937
		public virtual extern int IHTMLElement_offsetTop
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17000A58 RID: 2648
		// (get) Token: 0x06001732 RID: 5938
		public virtual extern int IHTMLElement_offsetWidth
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17000A59 RID: 2649
		// (get) Token: 0x06001733 RID: 5939
		public virtual extern int IHTMLElement_offsetHeight
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17000A5A RID: 2650
		// (get) Token: 0x06001734 RID: 5940
		public virtual extern IHTMLElement IHTMLElement_offsetParent
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17000A5B RID: 2651
		// (get) Token: 0x06001736 RID: 5942
		// (set) Token: 0x06001735 RID: 5941
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

		// Token: 0x17000A5C RID: 2652
		// (get) Token: 0x06001738 RID: 5944
		// (set) Token: 0x06001737 RID: 5943
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

		// Token: 0x17000A5D RID: 2653
		// (get) Token: 0x0600173A RID: 5946
		// (set) Token: 0x06001739 RID: 5945
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

		// Token: 0x17000A5E RID: 2654
		// (get) Token: 0x0600173C RID: 5948
		// (set) Token: 0x0600173B RID: 5947
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

		// Token: 0x0600173D RID: 5949
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement_insertAdjacentHTML([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string html);

		// Token: 0x0600173E RID: 5950
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement_insertAdjacentText([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string text);

		// Token: 0x17000A5F RID: 2655
		// (get) Token: 0x0600173F RID: 5951
		public virtual extern IHTMLElement IHTMLElement_parentTextEdit
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17000A60 RID: 2656
		// (get) Token: 0x06001740 RID: 5952
		public virtual extern bool IHTMLElement_isTextEdit
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x06001741 RID: 5953
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement_click();

		// Token: 0x17000A61 RID: 2657
		// (get) Token: 0x06001742 RID: 5954
		public virtual extern IHTMLFiltersCollection IHTMLElement_filters
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17000A62 RID: 2658
		// (get) Token: 0x06001744 RID: 5956
		// (set) Token: 0x06001743 RID: 5955
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

		// Token: 0x06001745 RID: 5957
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string IHTMLElement_toString();

		// Token: 0x17000A63 RID: 2659
		// (get) Token: 0x06001747 RID: 5959
		// (set) Token: 0x06001746 RID: 5958
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

		// Token: 0x17000A64 RID: 2660
		// (get) Token: 0x06001749 RID: 5961
		// (set) Token: 0x06001748 RID: 5960
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

		// Token: 0x17000A65 RID: 2661
		// (get) Token: 0x0600174B RID: 5963
		// (set) Token: 0x0600174A RID: 5962
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

		// Token: 0x17000A66 RID: 2662
		// (get) Token: 0x0600174D RID: 5965
		// (set) Token: 0x0600174C RID: 5964
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

		// Token: 0x17000A67 RID: 2663
		// (get) Token: 0x0600174F RID: 5967
		// (set) Token: 0x0600174E RID: 5966
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

		// Token: 0x17000A68 RID: 2664
		// (get) Token: 0x06001751 RID: 5969
		// (set) Token: 0x06001750 RID: 5968
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

		// Token: 0x17000A69 RID: 2665
		// (get) Token: 0x06001753 RID: 5971
		// (set) Token: 0x06001752 RID: 5970
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

		// Token: 0x17000A6A RID: 2666
		// (get) Token: 0x06001755 RID: 5973
		// (set) Token: 0x06001754 RID: 5972
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

		// Token: 0x17000A6B RID: 2667
		// (get) Token: 0x06001757 RID: 5975
		// (set) Token: 0x06001756 RID: 5974
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

		// Token: 0x17000A6C RID: 2668
		// (get) Token: 0x06001758 RID: 5976
		public virtual extern object IHTMLElement_children
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17000A6D RID: 2669
		// (get) Token: 0x06001759 RID: 5977
		public virtual extern object IHTMLElement_all
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17000A6E RID: 2670
		// (get) Token: 0x0600175A RID: 5978
		public virtual extern string IHTMLElement2_scopeName
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x0600175B RID: 5979
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_setCapture([In] bool containerCapture = true);

		// Token: 0x0600175C RID: 5980
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_releaseCapture();

		// Token: 0x17000A6F RID: 2671
		// (get) Token: 0x0600175E RID: 5982
		// (set) Token: 0x0600175D RID: 5981
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

		// Token: 0x0600175F RID: 5983
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string IHTMLElement2_componentFromPoint([In] int x, [In] int y);

		// Token: 0x06001760 RID: 5984
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_doScroll([MarshalAs(27)] [In] [Optional] object component);

		// Token: 0x17000A70 RID: 2672
		// (get) Token: 0x06001762 RID: 5986
		// (set) Token: 0x06001761 RID: 5985
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

		// Token: 0x17000A71 RID: 2673
		// (get) Token: 0x06001764 RID: 5988
		// (set) Token: 0x06001763 RID: 5987
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

		// Token: 0x17000A72 RID: 2674
		// (get) Token: 0x06001766 RID: 5990
		// (set) Token: 0x06001765 RID: 5989
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

		// Token: 0x17000A73 RID: 2675
		// (get) Token: 0x06001768 RID: 5992
		// (set) Token: 0x06001767 RID: 5991
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

		// Token: 0x17000A74 RID: 2676
		// (get) Token: 0x0600176A RID: 5994
		// (set) Token: 0x06001769 RID: 5993
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

		// Token: 0x17000A75 RID: 2677
		// (get) Token: 0x0600176C RID: 5996
		// (set) Token: 0x0600176B RID: 5995
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

		// Token: 0x17000A76 RID: 2678
		// (get) Token: 0x0600176E RID: 5998
		// (set) Token: 0x0600176D RID: 5997
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

		// Token: 0x17000A77 RID: 2679
		// (get) Token: 0x06001770 RID: 6000
		// (set) Token: 0x0600176F RID: 5999
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

		// Token: 0x17000A78 RID: 2680
		// (get) Token: 0x06001772 RID: 6002
		// (set) Token: 0x06001771 RID: 6001
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

		// Token: 0x17000A79 RID: 2681
		// (get) Token: 0x06001774 RID: 6004
		// (set) Token: 0x06001773 RID: 6003
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

		// Token: 0x17000A7A RID: 2682
		// (get) Token: 0x06001776 RID: 6006
		// (set) Token: 0x06001775 RID: 6005
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

		// Token: 0x17000A7B RID: 2683
		// (get) Token: 0x06001778 RID: 6008
		// (set) Token: 0x06001777 RID: 6007
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

		// Token: 0x17000A7C RID: 2684
		// (get) Token: 0x0600177A RID: 6010
		// (set) Token: 0x06001779 RID: 6009
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

		// Token: 0x17000A7D RID: 2685
		// (get) Token: 0x0600177B RID: 6011
		public virtual extern IHTMLCurrentStyle IHTMLElement2_currentStyle
		{
			[TypeLibFunc(1024)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17000A7E RID: 2686
		// (get) Token: 0x0600177D RID: 6013
		// (set) Token: 0x0600177C RID: 6012
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

		// Token: 0x0600177E RID: 6014
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLRectCollection IHTMLElement2_getClientRects();

		// Token: 0x0600177F RID: 6015
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLRect IHTMLElement2_getBoundingClientRect();

		// Token: 0x06001780 RID: 6016
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_setExpression([MarshalAs(19)] [In] string propname, [MarshalAs(19)] [In] string expression, [MarshalAs(19)] [In] string language = "");

		// Token: 0x06001781 RID: 6017
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		public virtual extern object IHTMLElement2_getExpression([MarshalAs(19)] [In] string propname);

		// Token: 0x06001782 RID: 6018
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement2_removeExpression([MarshalAs(19)] [In] string propname);

		// Token: 0x17000A7F RID: 2687
		// (get) Token: 0x06001784 RID: 6020
		// (set) Token: 0x06001783 RID: 6019
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

		// Token: 0x06001785 RID: 6021
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_focus();

		// Token: 0x17000A80 RID: 2688
		// (get) Token: 0x06001787 RID: 6023
		// (set) Token: 0x06001786 RID: 6022
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

		// Token: 0x17000A81 RID: 2689
		// (get) Token: 0x06001789 RID: 6025
		// (set) Token: 0x06001788 RID: 6024
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

		// Token: 0x17000A82 RID: 2690
		// (get) Token: 0x0600178B RID: 6027
		// (set) Token: 0x0600178A RID: 6026
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

		// Token: 0x17000A83 RID: 2691
		// (get) Token: 0x0600178D RID: 6029
		// (set) Token: 0x0600178C RID: 6028
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

		// Token: 0x0600178E RID: 6030
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_blur();

		// Token: 0x0600178F RID: 6031
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_addFilter([MarshalAs(25)] [In] object pUnk);

		// Token: 0x06001790 RID: 6032
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_removeFilter([MarshalAs(25)] [In] object pUnk);

		// Token: 0x17000A84 RID: 2692
		// (get) Token: 0x06001791 RID: 6033
		public virtual extern int IHTMLElement2_clientHeight
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17000A85 RID: 2693
		// (get) Token: 0x06001792 RID: 6034
		public virtual extern int IHTMLElement2_clientWidth
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17000A86 RID: 2694
		// (get) Token: 0x06001793 RID: 6035
		public virtual extern int IHTMLElement2_clientTop
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17000A87 RID: 2695
		// (get) Token: 0x06001794 RID: 6036
		public virtual extern int IHTMLElement2_clientLeft
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x06001795 RID: 6037
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement2_attachEvent([MarshalAs(19)] [In] string @event, [MarshalAs(26)] [In] object pdisp);

		// Token: 0x06001796 RID: 6038
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_detachEvent([MarshalAs(19)] [In] string @event, [MarshalAs(26)] [In] object pdisp);

		// Token: 0x17000A88 RID: 2696
		// (get) Token: 0x06001797 RID: 6039
		public virtual extern object IHTMLElement2_readyState
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x17000A89 RID: 2697
		// (get) Token: 0x06001799 RID: 6041
		// (set) Token: 0x06001798 RID: 6040
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

		// Token: 0x17000A8A RID: 2698
		// (get) Token: 0x0600179B RID: 6043
		// (set) Token: 0x0600179A RID: 6042
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

		// Token: 0x17000A8B RID: 2699
		// (get) Token: 0x0600179D RID: 6045
		// (set) Token: 0x0600179C RID: 6044
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

		// Token: 0x17000A8C RID: 2700
		// (get) Token: 0x0600179F RID: 6047
		// (set) Token: 0x0600179E RID: 6046
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

		// Token: 0x17000A8D RID: 2701
		// (get) Token: 0x060017A1 RID: 6049
		// (set) Token: 0x060017A0 RID: 6048
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

		// Token: 0x060017A2 RID: 6050
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(26)]
		public virtual extern object IHTMLElement2_createControlRange();

		// Token: 0x17000A8E RID: 2702
		// (get) Token: 0x060017A3 RID: 6051
		public virtual extern int IHTMLElement2_scrollHeight
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17000A8F RID: 2703
		// (get) Token: 0x060017A4 RID: 6052
		public virtual extern int IHTMLElement2_scrollWidth
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17000A90 RID: 2704
		// (get) Token: 0x060017A6 RID: 6054
		// (set) Token: 0x060017A5 RID: 6053
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

		// Token: 0x17000A91 RID: 2705
		// (get) Token: 0x060017A8 RID: 6056
		// (set) Token: 0x060017A7 RID: 6055
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

		// Token: 0x060017A9 RID: 6057
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_clearAttributes();

		// Token: 0x060017AA RID: 6058
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void mergeAttributes([MarshalAs(28)] [In] IHTMLElement mergeThis);

		// Token: 0x17000A92 RID: 2706
		// (get) Token: 0x060017AC RID: 6060
		// (set) Token: 0x060017AB RID: 6059
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

		// Token: 0x060017AD RID: 6061
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElement IHTMLElement2_insertAdjacentElement([MarshalAs(19)] [In] string where, [MarshalAs(28)] [In] IHTMLElement insertedElement);

		// Token: 0x060017AE RID: 6062
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElement IHTMLElement2_applyElement([MarshalAs(28)] [In] IHTMLElement apply, [MarshalAs(19)] [In] string where);

		// Token: 0x060017AF RID: 6063
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string IHTMLElement2_getAdjacentText([MarshalAs(19)] [In] string where);

		// Token: 0x060017B0 RID: 6064
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string IHTMLElement2_replaceAdjacentText([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string newText);

		// Token: 0x17000A93 RID: 2707
		// (get) Token: 0x060017B1 RID: 6065
		public virtual extern bool IHTMLElement2_canHaveChildren
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x060017B2 RID: 6066
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern int IHTMLElement2_addBehavior([MarshalAs(19)] [In] string bstrUrl, [MarshalAs(27)] [In] [Optional] ref object pvarFactory);

		// Token: 0x060017B3 RID: 6067
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement2_removeBehavior([In] int cookie);

		// Token: 0x17000A94 RID: 2708
		// (get) Token: 0x060017B4 RID: 6068
		public virtual extern IHTMLStyle IHTMLElement2_runtimeStyle
		{
			[TypeLibFunc(1024)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17000A95 RID: 2709
		// (get) Token: 0x060017B5 RID: 6069
		public virtual extern object IHTMLElement2_behaviorUrns
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17000A96 RID: 2710
		// (get) Token: 0x060017B7 RID: 6071
		// (set) Token: 0x060017B6 RID: 6070
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

		// Token: 0x17000A97 RID: 2711
		// (get) Token: 0x060017B9 RID: 6073
		// (set) Token: 0x060017B8 RID: 6072
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

		// Token: 0x17000A98 RID: 2712
		// (get) Token: 0x060017BA RID: 6074
		public virtual extern int IHTMLElement2_readyStateValue
		{
			[TypeLibFunc(65)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x060017BB RID: 6075
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElementCollection IHTMLElement2_getElementsByTagName([MarshalAs(19)] [In] string v);

		// Token: 0x060017BC RID: 6076
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void mergeAttributes([MarshalAs(28)] [In] IHTMLElement mergeThis, [MarshalAs(27)] [In] [Optional] ref object pvarFlags);

		// Token: 0x17000A99 RID: 2713
		// (get) Token: 0x060017BD RID: 6077
		public virtual extern bool IHTMLElement3_isMultiLine
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17000A9A RID: 2714
		// (get) Token: 0x060017BE RID: 6078
		public virtual extern bool IHTMLElement3_canHaveHTML
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17000A9B RID: 2715
		// (get) Token: 0x060017C0 RID: 6080
		// (set) Token: 0x060017BF RID: 6079
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

		// Token: 0x17000A9C RID: 2716
		// (get) Token: 0x060017C2 RID: 6082
		// (set) Token: 0x060017C1 RID: 6081
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

		// Token: 0x17000A9D RID: 2717
		// (get) Token: 0x060017C4 RID: 6084
		// (set) Token: 0x060017C3 RID: 6083
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

		// Token: 0x17000A9E RID: 2718
		// (get) Token: 0x060017C6 RID: 6086
		// (set) Token: 0x060017C5 RID: 6085
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

		// Token: 0x060017C7 RID: 6087
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement3_setActive();

		// Token: 0x17000A9F RID: 2719
		// (get) Token: 0x060017C9 RID: 6089
		// (set) Token: 0x060017C8 RID: 6088
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

		// Token: 0x17000AA0 RID: 2720
		// (get) Token: 0x060017CA RID: 6090
		public virtual extern bool IHTMLElement3_isContentEditable
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17000AA1 RID: 2721
		// (get) Token: 0x060017CC RID: 6092
		// (set) Token: 0x060017CB RID: 6091
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

		// Token: 0x17000AA2 RID: 2722
		// (get) Token: 0x060017CE RID: 6094
		// (set) Token: 0x060017CD RID: 6093
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

		// Token: 0x17000AA3 RID: 2723
		// (get) Token: 0x060017CF RID: 6095
		public virtual extern bool IHTMLElement3_isDisabled
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17000AA4 RID: 2724
		// (get) Token: 0x060017D1 RID: 6097
		// (set) Token: 0x060017D0 RID: 6096
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

		// Token: 0x17000AA5 RID: 2725
		// (get) Token: 0x060017D3 RID: 6099
		// (set) Token: 0x060017D2 RID: 6098
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

		// Token: 0x060017D4 RID: 6100
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement3_FireEvent([MarshalAs(19)] [In] string bstrEventName, [MarshalAs(27)] [In] [Optional] ref object pvarEventObject);

		// Token: 0x17000AA6 RID: 2726
		// (get) Token: 0x060017D6 RID: 6102
		// (set) Token: 0x060017D5 RID: 6101
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

		// Token: 0x17000AA7 RID: 2727
		// (get) Token: 0x060017D8 RID: 6104
		// (set) Token: 0x060017D7 RID: 6103
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

		// Token: 0x17000AA8 RID: 2728
		// (get) Token: 0x060017DA RID: 6106
		// (set) Token: 0x060017D9 RID: 6105
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

		// Token: 0x17000AA9 RID: 2729
		// (get) Token: 0x060017DC RID: 6108
		// (set) Token: 0x060017DB RID: 6107
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

		// Token: 0x17000AAA RID: 2730
		// (get) Token: 0x060017DE RID: 6110
		// (set) Token: 0x060017DD RID: 6109
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

		// Token: 0x17000AAB RID: 2731
		// (get) Token: 0x060017E0 RID: 6112
		// (set) Token: 0x060017DF RID: 6111
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

		// Token: 0x17000AAC RID: 2732
		// (get) Token: 0x060017E2 RID: 6114
		// (set) Token: 0x060017E1 RID: 6113
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

		// Token: 0x17000AAD RID: 2733
		// (get) Token: 0x060017E4 RID: 6116
		// (set) Token: 0x060017E3 RID: 6115
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

		// Token: 0x060017E5 RID: 6117
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement3_dragDrop();

		// Token: 0x17000AAE RID: 2734
		// (get) Token: 0x060017E6 RID: 6118
		public virtual extern int IHTMLElement3_glyphMode
		{
			[TypeLibFunc(1089)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17000AAF RID: 2735
		// (get) Token: 0x060017E8 RID: 6120
		// (set) Token: 0x060017E7 RID: 6119
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

		// Token: 0x060017E9 RID: 6121
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement4_normalize();

		// Token: 0x060017EA RID: 6122
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute IHTMLElement4_getAttributeNode([MarshalAs(19)] [In] string bstrName);

		// Token: 0x060017EB RID: 6123
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute IHTMLElement4_setAttributeNode([MarshalAs(28)] [In] IHTMLDOMAttribute pattr);

		// Token: 0x060017EC RID: 6124
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute IHTMLElement4_removeAttributeNode([MarshalAs(28)] [In] IHTMLDOMAttribute pattr);

		// Token: 0x17000AB0 RID: 2736
		// (get) Token: 0x060017EE RID: 6126
		// (set) Token: 0x060017ED RID: 6125
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

		// Token: 0x17000AB1 RID: 2737
		// (get) Token: 0x060017F0 RID: 6128
		// (set) Token: 0x060017EF RID: 6127
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

		// Token: 0x17000AB2 RID: 2738
		// (get) Token: 0x060017F2 RID: 6130
		// (set) Token: 0x060017F1 RID: 6129
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

		// Token: 0x17000AB3 RID: 2739
		// (get) Token: 0x060017F3 RID: 6131
		public virtual extern int IHTMLUniqueName_uniqueNumber
		{
			[TypeLibFunc(64)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17000AB4 RID: 2740
		// (get) Token: 0x060017F4 RID: 6132
		public virtual extern string IHTMLUniqueName_uniqueID
		{
			[TypeLibFunc(64)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17000AB5 RID: 2741
		// (get) Token: 0x060017F5 RID: 6133
		public virtual extern int IHTMLDOMNode_nodeType
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17000AB6 RID: 2742
		// (get) Token: 0x060017F6 RID: 6134
		public virtual extern IHTMLDOMNode IHTMLDOMNode_parentNode
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x060017F7 RID: 6135
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLDOMNode_hasChildNodes();

		// Token: 0x17000AB7 RID: 2743
		// (get) Token: 0x060017F8 RID: 6136
		public virtual extern object IHTMLDOMNode_childNodes
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17000AB8 RID: 2744
		// (get) Token: 0x060017F9 RID: 6137
		public virtual extern object IHTMLDOMNode_attributes
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x060017FA RID: 6138
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_insertBefore([MarshalAs(28)] [In] IHTMLDOMNode newChild, [MarshalAs(27)] [In] [Optional] object refChild);

		// Token: 0x060017FB RID: 6139
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_removeChild([MarshalAs(28)] [In] IHTMLDOMNode oldChild);

		// Token: 0x060017FC RID: 6140
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_replaceChild([MarshalAs(28)] [In] IHTMLDOMNode newChild, [MarshalAs(28)] [In] IHTMLDOMNode oldChild);

		// Token: 0x060017FD RID: 6141
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_cloneNode([In] bool fDeep);

		// Token: 0x060017FE RID: 6142
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_removeNode([In] bool fDeep = false);

		// Token: 0x060017FF RID: 6143
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_swapNode([MarshalAs(28)] [In] IHTMLDOMNode otherNode);

		// Token: 0x06001800 RID: 6144
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_replaceNode([MarshalAs(28)] [In] IHTMLDOMNode replacement);

		// Token: 0x06001801 RID: 6145
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_appendChild([MarshalAs(28)] [In] IHTMLDOMNode newChild);

		// Token: 0x17000AB9 RID: 2745
		// (get) Token: 0x06001802 RID: 6146
		public virtual extern string IHTMLDOMNode_nodeName
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17000ABA RID: 2746
		// (get) Token: 0x06001804 RID: 6148
		// (set) Token: 0x06001803 RID: 6147
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

		// Token: 0x17000ABB RID: 2747
		// (get) Token: 0x06001805 RID: 6149
		public virtual extern IHTMLDOMNode IHTMLDOMNode_firstChild
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17000ABC RID: 2748
		// (get) Token: 0x06001806 RID: 6150
		public virtual extern IHTMLDOMNode IHTMLDOMNode_lastChild
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17000ABD RID: 2749
		// (get) Token: 0x06001807 RID: 6151
		public virtual extern IHTMLDOMNode IHTMLDOMNode_previousSibling
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17000ABE RID: 2750
		// (get) Token: 0x06001808 RID: 6152
		public virtual extern IHTMLDOMNode IHTMLDOMNode_nextSibling
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17000ABF RID: 2751
		// (get) Token: 0x06001809 RID: 6153
		public virtual extern object IHTMLDOMNode2_ownerDocument
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17000AC0 RID: 2752
		// (get) Token: 0x0600180B RID: 6155
		// (set) Token: 0x0600180A RID: 6154
		public virtual extern string IHTMLLinkElement_href
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

		// Token: 0x17000AC1 RID: 2753
		// (get) Token: 0x0600180D RID: 6157
		// (set) Token: 0x0600180C RID: 6156
		public virtual extern string IHTMLLinkElement_rel
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

		// Token: 0x17000AC2 RID: 2754
		// (get) Token: 0x0600180F RID: 6159
		// (set) Token: 0x0600180E RID: 6158
		public virtual extern string IHTMLLinkElement_rev
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

		// Token: 0x17000AC3 RID: 2755
		// (get) Token: 0x06001811 RID: 6161
		// (set) Token: 0x06001810 RID: 6160
		public virtual extern string IHTMLLinkElement_type
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

		// Token: 0x17000AC4 RID: 2756
		// (get) Token: 0x06001812 RID: 6162
		public virtual extern string IHTMLLinkElement_readyState
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17000AC5 RID: 2757
		// (get) Token: 0x06001814 RID: 6164
		// (set) Token: 0x06001813 RID: 6163
		public virtual extern object IHTMLLinkElement_onreadystatechange
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

		// Token: 0x17000AC6 RID: 2758
		// (get) Token: 0x06001816 RID: 6166
		// (set) Token: 0x06001815 RID: 6165
		public virtual extern object IHTMLLinkElement_onload
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

		// Token: 0x17000AC7 RID: 2759
		// (get) Token: 0x06001818 RID: 6168
		// (set) Token: 0x06001817 RID: 6167
		public virtual extern object IHTMLLinkElement_onerror
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

		// Token: 0x17000AC8 RID: 2760
		// (get) Token: 0x06001819 RID: 6169
		public virtual extern IHTMLStyleSheet IHTMLLinkElement_styleSheet
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17000AC9 RID: 2761
		// (get) Token: 0x0600181B RID: 6171
		// (set) Token: 0x0600181A RID: 6170
		public virtual extern bool IHTMLLinkElement_disabled
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x17000ACA RID: 2762
		// (get) Token: 0x0600181D RID: 6173
		// (set) Token: 0x0600181C RID: 6172
		public virtual extern string IHTMLLinkElement_media
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

		// Token: 0x17000ACB RID: 2763
		// (get) Token: 0x0600181F RID: 6175
		// (set) Token: 0x0600181E RID: 6174
		public virtual extern string IHTMLLinkElement2_target
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

		// Token: 0x17000ACC RID: 2764
		// (get) Token: 0x06001821 RID: 6177
		// (set) Token: 0x06001820 RID: 6176
		public virtual extern string IHTMLLinkElement3_charset
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

		// Token: 0x17000ACD RID: 2765
		// (get) Token: 0x06001823 RID: 6179
		// (set) Token: 0x06001822 RID: 6178
		public virtual extern string IHTMLLinkElement3_hreflang
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

		// Token: 0x140000FB RID: 251
		// (add) Token: 0x06001824 RID: 6180
		// (remove) Token: 0x06001825 RID: 6181
		public virtual extern event HTMLLinkElementEvents_onhelpEventHandler HTMLLinkElementEvents_Event_onhelp;

		// Token: 0x140000FC RID: 252
		// (add) Token: 0x06001826 RID: 6182
		// (remove) Token: 0x06001827 RID: 6183
		public virtual extern event HTMLLinkElementEvents_onclickEventHandler HTMLLinkElementEvents_Event_onclick;

		// Token: 0x140000FD RID: 253
		// (add) Token: 0x06001828 RID: 6184
		// (remove) Token: 0x06001829 RID: 6185
		public virtual extern event HTMLLinkElementEvents_ondblclickEventHandler HTMLLinkElementEvents_Event_ondblclick;

		// Token: 0x140000FE RID: 254
		// (add) Token: 0x0600182A RID: 6186
		// (remove) Token: 0x0600182B RID: 6187
		public virtual extern event HTMLLinkElementEvents_onkeypressEventHandler HTMLLinkElementEvents_Event_onkeypress;

		// Token: 0x140000FF RID: 255
		// (add) Token: 0x0600182C RID: 6188
		// (remove) Token: 0x0600182D RID: 6189
		public virtual extern event HTMLLinkElementEvents_onkeydownEventHandler HTMLLinkElementEvents_Event_onkeydown;

		// Token: 0x14000100 RID: 256
		// (add) Token: 0x0600182E RID: 6190
		// (remove) Token: 0x0600182F RID: 6191
		public virtual extern event HTMLLinkElementEvents_onkeyupEventHandler HTMLLinkElementEvents_Event_onkeyup;

		// Token: 0x14000101 RID: 257
		// (add) Token: 0x06001830 RID: 6192
		// (remove) Token: 0x06001831 RID: 6193
		public virtual extern event HTMLLinkElementEvents_onmouseoutEventHandler HTMLLinkElementEvents_Event_onmouseout;

		// Token: 0x14000102 RID: 258
		// (add) Token: 0x06001832 RID: 6194
		// (remove) Token: 0x06001833 RID: 6195
		public virtual extern event HTMLLinkElementEvents_onmouseoverEventHandler HTMLLinkElementEvents_Event_onmouseover;

		// Token: 0x14000103 RID: 259
		// (add) Token: 0x06001834 RID: 6196
		// (remove) Token: 0x06001835 RID: 6197
		public virtual extern event HTMLLinkElementEvents_onmousemoveEventHandler HTMLLinkElementEvents_Event_onmousemove;

		// Token: 0x14000104 RID: 260
		// (add) Token: 0x06001836 RID: 6198
		// (remove) Token: 0x06001837 RID: 6199
		public virtual extern event HTMLLinkElementEvents_onmousedownEventHandler HTMLLinkElementEvents_Event_onmousedown;

		// Token: 0x14000105 RID: 261
		// (add) Token: 0x06001838 RID: 6200
		// (remove) Token: 0x06001839 RID: 6201
		public virtual extern event HTMLLinkElementEvents_onmouseupEventHandler HTMLLinkElementEvents_Event_onmouseup;

		// Token: 0x14000106 RID: 262
		// (add) Token: 0x0600183A RID: 6202
		// (remove) Token: 0x0600183B RID: 6203
		public virtual extern event HTMLLinkElementEvents_onselectstartEventHandler HTMLLinkElementEvents_Event_onselectstart;

		// Token: 0x14000107 RID: 263
		// (add) Token: 0x0600183C RID: 6204
		// (remove) Token: 0x0600183D RID: 6205
		public virtual extern event HTMLLinkElementEvents_onfilterchangeEventHandler HTMLLinkElementEvents_Event_onfilterchange;

		// Token: 0x14000108 RID: 264
		// (add) Token: 0x0600183E RID: 6206
		// (remove) Token: 0x0600183F RID: 6207
		public virtual extern event HTMLLinkElementEvents_ondragstartEventHandler HTMLLinkElementEvents_Event_ondragstart;

		// Token: 0x14000109 RID: 265
		// (add) Token: 0x06001840 RID: 6208
		// (remove) Token: 0x06001841 RID: 6209
		public virtual extern event HTMLLinkElementEvents_onbeforeupdateEventHandler HTMLLinkElementEvents_Event_onbeforeupdate;

		// Token: 0x1400010A RID: 266
		// (add) Token: 0x06001842 RID: 6210
		// (remove) Token: 0x06001843 RID: 6211
		public virtual extern event HTMLLinkElementEvents_onafterupdateEventHandler HTMLLinkElementEvents_Event_onafterupdate;

		// Token: 0x1400010B RID: 267
		// (add) Token: 0x06001844 RID: 6212
		// (remove) Token: 0x06001845 RID: 6213
		public virtual extern event HTMLLinkElementEvents_onerrorupdateEventHandler HTMLLinkElementEvents_Event_onerrorupdate;

		// Token: 0x1400010C RID: 268
		// (add) Token: 0x06001846 RID: 6214
		// (remove) Token: 0x06001847 RID: 6215
		public virtual extern event HTMLLinkElementEvents_onrowexitEventHandler HTMLLinkElementEvents_Event_onrowexit;

		// Token: 0x1400010D RID: 269
		// (add) Token: 0x06001848 RID: 6216
		// (remove) Token: 0x06001849 RID: 6217
		public virtual extern event HTMLLinkElementEvents_onrowenterEventHandler HTMLLinkElementEvents_Event_onrowenter;

		// Token: 0x1400010E RID: 270
		// (add) Token: 0x0600184A RID: 6218
		// (remove) Token: 0x0600184B RID: 6219
		public virtual extern event HTMLLinkElementEvents_ondatasetchangedEventHandler HTMLLinkElementEvents_Event_ondatasetchanged;

		// Token: 0x1400010F RID: 271
		// (add) Token: 0x0600184C RID: 6220
		// (remove) Token: 0x0600184D RID: 6221
		public virtual extern event HTMLLinkElementEvents_ondataavailableEventHandler HTMLLinkElementEvents_Event_ondataavailable;

		// Token: 0x14000110 RID: 272
		// (add) Token: 0x0600184E RID: 6222
		// (remove) Token: 0x0600184F RID: 6223
		public virtual extern event HTMLLinkElementEvents_ondatasetcompleteEventHandler HTMLLinkElementEvents_Event_ondatasetcomplete;

		// Token: 0x14000111 RID: 273
		// (add) Token: 0x06001850 RID: 6224
		// (remove) Token: 0x06001851 RID: 6225
		public virtual extern event HTMLLinkElementEvents_onlosecaptureEventHandler HTMLLinkElementEvents_Event_onlosecapture;

		// Token: 0x14000112 RID: 274
		// (add) Token: 0x06001852 RID: 6226
		// (remove) Token: 0x06001853 RID: 6227
		public virtual extern event HTMLLinkElementEvents_onpropertychangeEventHandler HTMLLinkElementEvents_Event_onpropertychange;

		// Token: 0x14000113 RID: 275
		// (add) Token: 0x06001854 RID: 6228
		// (remove) Token: 0x06001855 RID: 6229
		public virtual extern event HTMLLinkElementEvents_onscrollEventHandler HTMLLinkElementEvents_Event_onscroll;

		// Token: 0x14000114 RID: 276
		// (add) Token: 0x06001856 RID: 6230
		// (remove) Token: 0x06001857 RID: 6231
		public virtual extern event HTMLLinkElementEvents_onfocusEventHandler HTMLLinkElementEvents_Event_onfocus;

		// Token: 0x14000115 RID: 277
		// (add) Token: 0x06001858 RID: 6232
		// (remove) Token: 0x06001859 RID: 6233
		public virtual extern event HTMLLinkElementEvents_onblurEventHandler HTMLLinkElementEvents_Event_onblur;

		// Token: 0x14000116 RID: 278
		// (add) Token: 0x0600185A RID: 6234
		// (remove) Token: 0x0600185B RID: 6235
		public virtual extern event HTMLLinkElementEvents_onresizeEventHandler HTMLLinkElementEvents_Event_onresize;

		// Token: 0x14000117 RID: 279
		// (add) Token: 0x0600185C RID: 6236
		// (remove) Token: 0x0600185D RID: 6237
		public virtual extern event HTMLLinkElementEvents_ondragEventHandler HTMLLinkElementEvents_Event_ondrag;

		// Token: 0x14000118 RID: 280
		// (add) Token: 0x0600185E RID: 6238
		// (remove) Token: 0x0600185F RID: 6239
		public virtual extern event HTMLLinkElementEvents_ondragendEventHandler HTMLLinkElementEvents_Event_ondragend;

		// Token: 0x14000119 RID: 281
		// (add) Token: 0x06001860 RID: 6240
		// (remove) Token: 0x06001861 RID: 6241
		public virtual extern event HTMLLinkElementEvents_ondragenterEventHandler HTMLLinkElementEvents_Event_ondragenter;

		// Token: 0x1400011A RID: 282
		// (add) Token: 0x06001862 RID: 6242
		// (remove) Token: 0x06001863 RID: 6243
		public virtual extern event HTMLLinkElementEvents_ondragoverEventHandler HTMLLinkElementEvents_Event_ondragover;

		// Token: 0x1400011B RID: 283
		// (add) Token: 0x06001864 RID: 6244
		// (remove) Token: 0x06001865 RID: 6245
		public virtual extern event HTMLLinkElementEvents_ondragleaveEventHandler HTMLLinkElementEvents_Event_ondragleave;

		// Token: 0x1400011C RID: 284
		// (add) Token: 0x06001866 RID: 6246
		// (remove) Token: 0x06001867 RID: 6247
		public virtual extern event HTMLLinkElementEvents_ondropEventHandler HTMLLinkElementEvents_Event_ondrop;

		// Token: 0x1400011D RID: 285
		// (add) Token: 0x06001868 RID: 6248
		// (remove) Token: 0x06001869 RID: 6249
		public virtual extern event HTMLLinkElementEvents_onbeforecutEventHandler HTMLLinkElementEvents_Event_onbeforecut;

		// Token: 0x1400011E RID: 286
		// (add) Token: 0x0600186A RID: 6250
		// (remove) Token: 0x0600186B RID: 6251
		public virtual extern event HTMLLinkElementEvents_oncutEventHandler HTMLLinkElementEvents_Event_oncut;

		// Token: 0x1400011F RID: 287
		// (add) Token: 0x0600186C RID: 6252
		// (remove) Token: 0x0600186D RID: 6253
		public virtual extern event HTMLLinkElementEvents_onbeforecopyEventHandler HTMLLinkElementEvents_Event_onbeforecopy;

		// Token: 0x14000120 RID: 288
		// (add) Token: 0x0600186E RID: 6254
		// (remove) Token: 0x0600186F RID: 6255
		public virtual extern event HTMLLinkElementEvents_oncopyEventHandler HTMLLinkElementEvents_Event_oncopy;

		// Token: 0x14000121 RID: 289
		// (add) Token: 0x06001870 RID: 6256
		// (remove) Token: 0x06001871 RID: 6257
		public virtual extern event HTMLLinkElementEvents_onbeforepasteEventHandler HTMLLinkElementEvents_Event_onbeforepaste;

		// Token: 0x14000122 RID: 290
		// (add) Token: 0x06001872 RID: 6258
		// (remove) Token: 0x06001873 RID: 6259
		public virtual extern event HTMLLinkElementEvents_onpasteEventHandler HTMLLinkElementEvents_Event_onpaste;

		// Token: 0x14000123 RID: 291
		// (add) Token: 0x06001874 RID: 6260
		// (remove) Token: 0x06001875 RID: 6261
		public virtual extern event HTMLLinkElementEvents_oncontextmenuEventHandler HTMLLinkElementEvents_Event_oncontextmenu;

		// Token: 0x14000124 RID: 292
		// (add) Token: 0x06001876 RID: 6262
		// (remove) Token: 0x06001877 RID: 6263
		public virtual extern event HTMLLinkElementEvents_onrowsdeleteEventHandler HTMLLinkElementEvents_Event_onrowsdelete;

		// Token: 0x14000125 RID: 293
		// (add) Token: 0x06001878 RID: 6264
		// (remove) Token: 0x06001879 RID: 6265
		public virtual extern event HTMLLinkElementEvents_onrowsinsertedEventHandler HTMLLinkElementEvents_Event_onrowsinserted;

		// Token: 0x14000126 RID: 294
		// (add) Token: 0x0600187A RID: 6266
		// (remove) Token: 0x0600187B RID: 6267
		public virtual extern event HTMLLinkElementEvents_oncellchangeEventHandler HTMLLinkElementEvents_Event_oncellchange;

		// Token: 0x14000127 RID: 295
		// (add) Token: 0x0600187C RID: 6268
		// (remove) Token: 0x0600187D RID: 6269
		public virtual extern event HTMLLinkElementEvents_onreadystatechangeEventHandler HTMLLinkElementEvents_Event_onreadystatechange;

		// Token: 0x14000128 RID: 296
		// (add) Token: 0x0600187E RID: 6270
		// (remove) Token: 0x0600187F RID: 6271
		public virtual extern event HTMLLinkElementEvents_onbeforeeditfocusEventHandler HTMLLinkElementEvents_Event_onbeforeeditfocus;

		// Token: 0x14000129 RID: 297
		// (add) Token: 0x06001880 RID: 6272
		// (remove) Token: 0x06001881 RID: 6273
		public virtual extern event HTMLLinkElementEvents_onlayoutcompleteEventHandler HTMLLinkElementEvents_Event_onlayoutcomplete;

		// Token: 0x1400012A RID: 298
		// (add) Token: 0x06001882 RID: 6274
		// (remove) Token: 0x06001883 RID: 6275
		public virtual extern event HTMLLinkElementEvents_onpageEventHandler HTMLLinkElementEvents_Event_onpage;

		// Token: 0x1400012B RID: 299
		// (add) Token: 0x06001884 RID: 6276
		// (remove) Token: 0x06001885 RID: 6277
		public virtual extern event HTMLLinkElementEvents_onbeforedeactivateEventHandler HTMLLinkElementEvents_Event_onbeforedeactivate;

		// Token: 0x1400012C RID: 300
		// (add) Token: 0x06001886 RID: 6278
		// (remove) Token: 0x06001887 RID: 6279
		public virtual extern event HTMLLinkElementEvents_onbeforeactivateEventHandler HTMLLinkElementEvents_Event_onbeforeactivate;

		// Token: 0x1400012D RID: 301
		// (add) Token: 0x06001888 RID: 6280
		// (remove) Token: 0x06001889 RID: 6281
		public virtual extern event HTMLLinkElementEvents_onmoveEventHandler HTMLLinkElementEvents_Event_onmove;

		// Token: 0x1400012E RID: 302
		// (add) Token: 0x0600188A RID: 6282
		// (remove) Token: 0x0600188B RID: 6283
		public virtual extern event HTMLLinkElementEvents_oncontrolselectEventHandler HTMLLinkElementEvents_Event_oncontrolselect;

		// Token: 0x1400012F RID: 303
		// (add) Token: 0x0600188C RID: 6284
		// (remove) Token: 0x0600188D RID: 6285
		public virtual extern event HTMLLinkElementEvents_onmovestartEventHandler HTMLLinkElementEvents_Event_onmovestart;

		// Token: 0x14000130 RID: 304
		// (add) Token: 0x0600188E RID: 6286
		// (remove) Token: 0x0600188F RID: 6287
		public virtual extern event HTMLLinkElementEvents_onmoveendEventHandler HTMLLinkElementEvents_Event_onmoveend;

		// Token: 0x14000131 RID: 305
		// (add) Token: 0x06001890 RID: 6288
		// (remove) Token: 0x06001891 RID: 6289
		public virtual extern event HTMLLinkElementEvents_onresizestartEventHandler HTMLLinkElementEvents_Event_onresizestart;

		// Token: 0x14000132 RID: 306
		// (add) Token: 0x06001892 RID: 6290
		// (remove) Token: 0x06001893 RID: 6291
		public virtual extern event HTMLLinkElementEvents_onresizeendEventHandler HTMLLinkElementEvents_Event_onresizeend;

		// Token: 0x14000133 RID: 307
		// (add) Token: 0x06001894 RID: 6292
		// (remove) Token: 0x06001895 RID: 6293
		public virtual extern event HTMLLinkElementEvents_onmouseenterEventHandler HTMLLinkElementEvents_Event_onmouseenter;

		// Token: 0x14000134 RID: 308
		// (add) Token: 0x06001896 RID: 6294
		// (remove) Token: 0x06001897 RID: 6295
		public virtual extern event HTMLLinkElementEvents_onmouseleaveEventHandler HTMLLinkElementEvents_Event_onmouseleave;

		// Token: 0x14000135 RID: 309
		// (add) Token: 0x06001898 RID: 6296
		// (remove) Token: 0x06001899 RID: 6297
		public virtual extern event HTMLLinkElementEvents_onmousewheelEventHandler HTMLLinkElementEvents_Event_onmousewheel;

		// Token: 0x14000136 RID: 310
		// (add) Token: 0x0600189A RID: 6298
		// (remove) Token: 0x0600189B RID: 6299
		public virtual extern event HTMLLinkElementEvents_onactivateEventHandler HTMLLinkElementEvents_Event_onactivate;

		// Token: 0x14000137 RID: 311
		// (add) Token: 0x0600189C RID: 6300
		// (remove) Token: 0x0600189D RID: 6301
		public virtual extern event HTMLLinkElementEvents_ondeactivateEventHandler HTMLLinkElementEvents_Event_ondeactivate;

		// Token: 0x14000138 RID: 312
		// (add) Token: 0x0600189E RID: 6302
		// (remove) Token: 0x0600189F RID: 6303
		public virtual extern event HTMLLinkElementEvents_onfocusinEventHandler HTMLLinkElementEvents_Event_onfocusin;

		// Token: 0x14000139 RID: 313
		// (add) Token: 0x060018A0 RID: 6304
		// (remove) Token: 0x060018A1 RID: 6305
		public virtual extern event HTMLLinkElementEvents_onfocusoutEventHandler HTMLLinkElementEvents_Event_onfocusout;

		// Token: 0x1400013A RID: 314
		// (add) Token: 0x060018A2 RID: 6306
		// (remove) Token: 0x060018A3 RID: 6307
		public virtual extern event HTMLLinkElementEvents_onloadEventHandler HTMLLinkElementEvents_Event_onload;

		// Token: 0x1400013B RID: 315
		// (add) Token: 0x060018A4 RID: 6308
		// (remove) Token: 0x060018A5 RID: 6309
		public virtual extern event HTMLLinkElementEvents_onerrorEventHandler HTMLLinkElementEvents_Event_onerror;

		// Token: 0x1400013C RID: 316
		// (add) Token: 0x060018A6 RID: 6310
		// (remove) Token: 0x060018A7 RID: 6311
		public virtual extern event HTMLLinkElementEvents2_onhelpEventHandler HTMLLinkElementEvents2_Event_onhelp;

		// Token: 0x1400013D RID: 317
		// (add) Token: 0x060018A8 RID: 6312
		// (remove) Token: 0x060018A9 RID: 6313
		public virtual extern event HTMLLinkElementEvents2_onclickEventHandler HTMLLinkElementEvents2_Event_onclick;

		// Token: 0x1400013E RID: 318
		// (add) Token: 0x060018AA RID: 6314
		// (remove) Token: 0x060018AB RID: 6315
		public virtual extern event HTMLLinkElementEvents2_ondblclickEventHandler HTMLLinkElementEvents2_Event_ondblclick;

		// Token: 0x1400013F RID: 319
		// (add) Token: 0x060018AC RID: 6316
		// (remove) Token: 0x060018AD RID: 6317
		public virtual extern event HTMLLinkElementEvents2_onkeypressEventHandler HTMLLinkElementEvents2_Event_onkeypress;

		// Token: 0x14000140 RID: 320
		// (add) Token: 0x060018AE RID: 6318
		// (remove) Token: 0x060018AF RID: 6319
		public virtual extern event HTMLLinkElementEvents2_onkeydownEventHandler HTMLLinkElementEvents2_Event_onkeydown;

		// Token: 0x14000141 RID: 321
		// (add) Token: 0x060018B0 RID: 6320
		// (remove) Token: 0x060018B1 RID: 6321
		public virtual extern event HTMLLinkElementEvents2_onkeyupEventHandler HTMLLinkElementEvents2_Event_onkeyup;

		// Token: 0x14000142 RID: 322
		// (add) Token: 0x060018B2 RID: 6322
		// (remove) Token: 0x060018B3 RID: 6323
		public virtual extern event HTMLLinkElementEvents2_onmouseoutEventHandler HTMLLinkElementEvents2_Event_onmouseout;

		// Token: 0x14000143 RID: 323
		// (add) Token: 0x060018B4 RID: 6324
		// (remove) Token: 0x060018B5 RID: 6325
		public virtual extern event HTMLLinkElementEvents2_onmouseoverEventHandler HTMLLinkElementEvents2_Event_onmouseover;

		// Token: 0x14000144 RID: 324
		// (add) Token: 0x060018B6 RID: 6326
		// (remove) Token: 0x060018B7 RID: 6327
		public virtual extern event HTMLLinkElementEvents2_onmousemoveEventHandler HTMLLinkElementEvents2_Event_onmousemove;

		// Token: 0x14000145 RID: 325
		// (add) Token: 0x060018B8 RID: 6328
		// (remove) Token: 0x060018B9 RID: 6329
		public virtual extern event HTMLLinkElementEvents2_onmousedownEventHandler HTMLLinkElementEvents2_Event_onmousedown;

		// Token: 0x14000146 RID: 326
		// (add) Token: 0x060018BA RID: 6330
		// (remove) Token: 0x060018BB RID: 6331
		public virtual extern event HTMLLinkElementEvents2_onmouseupEventHandler HTMLLinkElementEvents2_Event_onmouseup;

		// Token: 0x14000147 RID: 327
		// (add) Token: 0x060018BC RID: 6332
		// (remove) Token: 0x060018BD RID: 6333
		public virtual extern event HTMLLinkElementEvents2_onselectstartEventHandler HTMLLinkElementEvents2_Event_onselectstart;

		// Token: 0x14000148 RID: 328
		// (add) Token: 0x060018BE RID: 6334
		// (remove) Token: 0x060018BF RID: 6335
		public virtual extern event HTMLLinkElementEvents2_onfilterchangeEventHandler HTMLLinkElementEvents2_Event_onfilterchange;

		// Token: 0x14000149 RID: 329
		// (add) Token: 0x060018C0 RID: 6336
		// (remove) Token: 0x060018C1 RID: 6337
		public virtual extern event HTMLLinkElementEvents2_ondragstartEventHandler HTMLLinkElementEvents2_Event_ondragstart;

		// Token: 0x1400014A RID: 330
		// (add) Token: 0x060018C2 RID: 6338
		// (remove) Token: 0x060018C3 RID: 6339
		public virtual extern event HTMLLinkElementEvents2_onbeforeupdateEventHandler HTMLLinkElementEvents2_Event_onbeforeupdate;

		// Token: 0x1400014B RID: 331
		// (add) Token: 0x060018C4 RID: 6340
		// (remove) Token: 0x060018C5 RID: 6341
		public virtual extern event HTMLLinkElementEvents2_onafterupdateEventHandler HTMLLinkElementEvents2_Event_onafterupdate;

		// Token: 0x1400014C RID: 332
		// (add) Token: 0x060018C6 RID: 6342
		// (remove) Token: 0x060018C7 RID: 6343
		public virtual extern event HTMLLinkElementEvents2_onerrorupdateEventHandler HTMLLinkElementEvents2_Event_onerrorupdate;

		// Token: 0x1400014D RID: 333
		// (add) Token: 0x060018C8 RID: 6344
		// (remove) Token: 0x060018C9 RID: 6345
		public virtual extern event HTMLLinkElementEvents2_onrowexitEventHandler HTMLLinkElementEvents2_Event_onrowexit;

		// Token: 0x1400014E RID: 334
		// (add) Token: 0x060018CA RID: 6346
		// (remove) Token: 0x060018CB RID: 6347
		public virtual extern event HTMLLinkElementEvents2_onrowenterEventHandler HTMLLinkElementEvents2_Event_onrowenter;

		// Token: 0x1400014F RID: 335
		// (add) Token: 0x060018CC RID: 6348
		// (remove) Token: 0x060018CD RID: 6349
		public virtual extern event HTMLLinkElementEvents2_ondatasetchangedEventHandler HTMLLinkElementEvents2_Event_ondatasetchanged;

		// Token: 0x14000150 RID: 336
		// (add) Token: 0x060018CE RID: 6350
		// (remove) Token: 0x060018CF RID: 6351
		public virtual extern event HTMLLinkElementEvents2_ondataavailableEventHandler HTMLLinkElementEvents2_Event_ondataavailable;

		// Token: 0x14000151 RID: 337
		// (add) Token: 0x060018D0 RID: 6352
		// (remove) Token: 0x060018D1 RID: 6353
		public virtual extern event HTMLLinkElementEvents2_ondatasetcompleteEventHandler HTMLLinkElementEvents2_Event_ondatasetcomplete;

		// Token: 0x14000152 RID: 338
		// (add) Token: 0x060018D2 RID: 6354
		// (remove) Token: 0x060018D3 RID: 6355
		public virtual extern event HTMLLinkElementEvents2_onlosecaptureEventHandler HTMLLinkElementEvents2_Event_onlosecapture;

		// Token: 0x14000153 RID: 339
		// (add) Token: 0x060018D4 RID: 6356
		// (remove) Token: 0x060018D5 RID: 6357
		public virtual extern event HTMLLinkElementEvents2_onpropertychangeEventHandler HTMLLinkElementEvents2_Event_onpropertychange;

		// Token: 0x14000154 RID: 340
		// (add) Token: 0x060018D6 RID: 6358
		// (remove) Token: 0x060018D7 RID: 6359
		public virtual extern event HTMLLinkElementEvents2_onscrollEventHandler HTMLLinkElementEvents2_Event_onscroll;

		// Token: 0x14000155 RID: 341
		// (add) Token: 0x060018D8 RID: 6360
		// (remove) Token: 0x060018D9 RID: 6361
		public virtual extern event HTMLLinkElementEvents2_onfocusEventHandler HTMLLinkElementEvents2_Event_onfocus;

		// Token: 0x14000156 RID: 342
		// (add) Token: 0x060018DA RID: 6362
		// (remove) Token: 0x060018DB RID: 6363
		public virtual extern event HTMLLinkElementEvents2_onblurEventHandler HTMLLinkElementEvents2_Event_onblur;

		// Token: 0x14000157 RID: 343
		// (add) Token: 0x060018DC RID: 6364
		// (remove) Token: 0x060018DD RID: 6365
		public virtual extern event HTMLLinkElementEvents2_onresizeEventHandler HTMLLinkElementEvents2_Event_onresize;

		// Token: 0x14000158 RID: 344
		// (add) Token: 0x060018DE RID: 6366
		// (remove) Token: 0x060018DF RID: 6367
		public virtual extern event HTMLLinkElementEvents2_ondragEventHandler HTMLLinkElementEvents2_Event_ondrag;

		// Token: 0x14000159 RID: 345
		// (add) Token: 0x060018E0 RID: 6368
		// (remove) Token: 0x060018E1 RID: 6369
		public virtual extern event HTMLLinkElementEvents2_ondragendEventHandler HTMLLinkElementEvents2_Event_ondragend;

		// Token: 0x1400015A RID: 346
		// (add) Token: 0x060018E2 RID: 6370
		// (remove) Token: 0x060018E3 RID: 6371
		public virtual extern event HTMLLinkElementEvents2_ondragenterEventHandler HTMLLinkElementEvents2_Event_ondragenter;

		// Token: 0x1400015B RID: 347
		// (add) Token: 0x060018E4 RID: 6372
		// (remove) Token: 0x060018E5 RID: 6373
		public virtual extern event HTMLLinkElementEvents2_ondragoverEventHandler HTMLLinkElementEvents2_Event_ondragover;

		// Token: 0x1400015C RID: 348
		// (add) Token: 0x060018E6 RID: 6374
		// (remove) Token: 0x060018E7 RID: 6375
		public virtual extern event HTMLLinkElementEvents2_ondragleaveEventHandler HTMLLinkElementEvents2_Event_ondragleave;

		// Token: 0x1400015D RID: 349
		// (add) Token: 0x060018E8 RID: 6376
		// (remove) Token: 0x060018E9 RID: 6377
		public virtual extern event HTMLLinkElementEvents2_ondropEventHandler HTMLLinkElementEvents2_Event_ondrop;

		// Token: 0x1400015E RID: 350
		// (add) Token: 0x060018EA RID: 6378
		// (remove) Token: 0x060018EB RID: 6379
		public virtual extern event HTMLLinkElementEvents2_onbeforecutEventHandler HTMLLinkElementEvents2_Event_onbeforecut;

		// Token: 0x1400015F RID: 351
		// (add) Token: 0x060018EC RID: 6380
		// (remove) Token: 0x060018ED RID: 6381
		public virtual extern event HTMLLinkElementEvents2_oncutEventHandler HTMLLinkElementEvents2_Event_oncut;

		// Token: 0x14000160 RID: 352
		// (add) Token: 0x060018EE RID: 6382
		// (remove) Token: 0x060018EF RID: 6383
		public virtual extern event HTMLLinkElementEvents2_onbeforecopyEventHandler HTMLLinkElementEvents2_Event_onbeforecopy;

		// Token: 0x14000161 RID: 353
		// (add) Token: 0x060018F0 RID: 6384
		// (remove) Token: 0x060018F1 RID: 6385
		public virtual extern event HTMLLinkElementEvents2_oncopyEventHandler HTMLLinkElementEvents2_Event_oncopy;

		// Token: 0x14000162 RID: 354
		// (add) Token: 0x060018F2 RID: 6386
		// (remove) Token: 0x060018F3 RID: 6387
		public virtual extern event HTMLLinkElementEvents2_onbeforepasteEventHandler HTMLLinkElementEvents2_Event_onbeforepaste;

		// Token: 0x14000163 RID: 355
		// (add) Token: 0x060018F4 RID: 6388
		// (remove) Token: 0x060018F5 RID: 6389
		public virtual extern event HTMLLinkElementEvents2_onpasteEventHandler HTMLLinkElementEvents2_Event_onpaste;

		// Token: 0x14000164 RID: 356
		// (add) Token: 0x060018F6 RID: 6390
		// (remove) Token: 0x060018F7 RID: 6391
		public virtual extern event HTMLLinkElementEvents2_oncontextmenuEventHandler HTMLLinkElementEvents2_Event_oncontextmenu;

		// Token: 0x14000165 RID: 357
		// (add) Token: 0x060018F8 RID: 6392
		// (remove) Token: 0x060018F9 RID: 6393
		public virtual extern event HTMLLinkElementEvents2_onrowsdeleteEventHandler HTMLLinkElementEvents2_Event_onrowsdelete;

		// Token: 0x14000166 RID: 358
		// (add) Token: 0x060018FA RID: 6394
		// (remove) Token: 0x060018FB RID: 6395
		public virtual extern event HTMLLinkElementEvents2_onrowsinsertedEventHandler HTMLLinkElementEvents2_Event_onrowsinserted;

		// Token: 0x14000167 RID: 359
		// (add) Token: 0x060018FC RID: 6396
		// (remove) Token: 0x060018FD RID: 6397
		public virtual extern event HTMLLinkElementEvents2_oncellchangeEventHandler HTMLLinkElementEvents2_Event_oncellchange;

		// Token: 0x14000168 RID: 360
		// (add) Token: 0x060018FE RID: 6398
		// (remove) Token: 0x060018FF RID: 6399
		public virtual extern event HTMLLinkElementEvents2_onreadystatechangeEventHandler HTMLLinkElementEvents2_Event_onreadystatechange;

		// Token: 0x14000169 RID: 361
		// (add) Token: 0x06001900 RID: 6400
		// (remove) Token: 0x06001901 RID: 6401
		public virtual extern event HTMLLinkElementEvents2_onlayoutcompleteEventHandler HTMLLinkElementEvents2_Event_onlayoutcomplete;

		// Token: 0x1400016A RID: 362
		// (add) Token: 0x06001902 RID: 6402
		// (remove) Token: 0x06001903 RID: 6403
		public virtual extern event HTMLLinkElementEvents2_onpageEventHandler HTMLLinkElementEvents2_Event_onpage;

		// Token: 0x1400016B RID: 363
		// (add) Token: 0x06001904 RID: 6404
		// (remove) Token: 0x06001905 RID: 6405
		public virtual extern event HTMLLinkElementEvents2_onmouseenterEventHandler HTMLLinkElementEvents2_Event_onmouseenter;

		// Token: 0x1400016C RID: 364
		// (add) Token: 0x06001906 RID: 6406
		// (remove) Token: 0x06001907 RID: 6407
		public virtual extern event HTMLLinkElementEvents2_onmouseleaveEventHandler HTMLLinkElementEvents2_Event_onmouseleave;

		// Token: 0x1400016D RID: 365
		// (add) Token: 0x06001908 RID: 6408
		// (remove) Token: 0x06001909 RID: 6409
		public virtual extern event HTMLLinkElementEvents2_onactivateEventHandler HTMLLinkElementEvents2_Event_onactivate;

		// Token: 0x1400016E RID: 366
		// (add) Token: 0x0600190A RID: 6410
		// (remove) Token: 0x0600190B RID: 6411
		public virtual extern event HTMLLinkElementEvents2_ondeactivateEventHandler HTMLLinkElementEvents2_Event_ondeactivate;

		// Token: 0x1400016F RID: 367
		// (add) Token: 0x0600190C RID: 6412
		// (remove) Token: 0x0600190D RID: 6413
		public virtual extern event HTMLLinkElementEvents2_onbeforedeactivateEventHandler HTMLLinkElementEvents2_Event_onbeforedeactivate;

		// Token: 0x14000170 RID: 368
		// (add) Token: 0x0600190E RID: 6414
		// (remove) Token: 0x0600190F RID: 6415
		public virtual extern event HTMLLinkElementEvents2_onbeforeactivateEventHandler HTMLLinkElementEvents2_Event_onbeforeactivate;

		// Token: 0x14000171 RID: 369
		// (add) Token: 0x06001910 RID: 6416
		// (remove) Token: 0x06001911 RID: 6417
		public virtual extern event HTMLLinkElementEvents2_onfocusinEventHandler HTMLLinkElementEvents2_Event_onfocusin;

		// Token: 0x14000172 RID: 370
		// (add) Token: 0x06001912 RID: 6418
		// (remove) Token: 0x06001913 RID: 6419
		public virtual extern event HTMLLinkElementEvents2_onfocusoutEventHandler HTMLLinkElementEvents2_Event_onfocusout;

		// Token: 0x14000173 RID: 371
		// (add) Token: 0x06001914 RID: 6420
		// (remove) Token: 0x06001915 RID: 6421
		public virtual extern event HTMLLinkElementEvents2_onmoveEventHandler HTMLLinkElementEvents2_Event_onmove;

		// Token: 0x14000174 RID: 372
		// (add) Token: 0x06001916 RID: 6422
		// (remove) Token: 0x06001917 RID: 6423
		public virtual extern event HTMLLinkElementEvents2_oncontrolselectEventHandler HTMLLinkElementEvents2_Event_oncontrolselect;

		// Token: 0x14000175 RID: 373
		// (add) Token: 0x06001918 RID: 6424
		// (remove) Token: 0x06001919 RID: 6425
		public virtual extern event HTMLLinkElementEvents2_onmovestartEventHandler HTMLLinkElementEvents2_Event_onmovestart;

		// Token: 0x14000176 RID: 374
		// (add) Token: 0x0600191A RID: 6426
		// (remove) Token: 0x0600191B RID: 6427
		public virtual extern event HTMLLinkElementEvents2_onmoveendEventHandler HTMLLinkElementEvents2_Event_onmoveend;

		// Token: 0x14000177 RID: 375
		// (add) Token: 0x0600191C RID: 6428
		// (remove) Token: 0x0600191D RID: 6429
		public virtual extern event HTMLLinkElementEvents2_onresizestartEventHandler HTMLLinkElementEvents2_Event_onresizestart;

		// Token: 0x14000178 RID: 376
		// (add) Token: 0x0600191E RID: 6430
		// (remove) Token: 0x0600191F RID: 6431
		public virtual extern event HTMLLinkElementEvents2_onresizeendEventHandler HTMLLinkElementEvents2_Event_onresizeend;

		// Token: 0x14000179 RID: 377
		// (add) Token: 0x06001920 RID: 6432
		// (remove) Token: 0x06001921 RID: 6433
		public virtual extern event HTMLLinkElementEvents2_onmousewheelEventHandler HTMLLinkElementEvents2_Event_onmousewheel;

		// Token: 0x1400017A RID: 378
		// (add) Token: 0x06001922 RID: 6434
		// (remove) Token: 0x06001923 RID: 6435
		public virtual extern event HTMLLinkElementEvents2_onloadEventHandler HTMLLinkElementEvents2_Event_onload;

		// Token: 0x1400017B RID: 379
		// (add) Token: 0x06001924 RID: 6436
		// (remove) Token: 0x06001925 RID: 6437
		public virtual extern event HTMLLinkElementEvents2_onerrorEventHandler HTMLLinkElementEvents2_Event_onerror;
	}
}
