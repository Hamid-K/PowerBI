using System;
using System.Collections;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000D0C RID: 3340
	[Guid("0D04D285-6BEC-11CF-8B97-00AA00476DA6")]
	[ComSourceInterfaces("mshtml.HTMLFormElementEvents\0\0")]
	[TypeLibType(2)]
	[ClassInterface(0)]
	[DefaultMember("item")]
	[ComImport]
	public class OldHTMLFormElementClass : DispHTMLFormElement, OldHTMLFormElement, HTMLFormElementEvents_Event, IHTMLFormElement, IHTMLElement, IHTMLElement2
	{
		// Token: 0x060165E8 RID: 91624
		[MethodImpl(4096, MethodCodeType = 3)]
		public extern OldHTMLFormElementClass();

		// Token: 0x060165E9 RID: 91625
		[DispId(-2147417611)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void setAttribute([MarshalAs(19)] [In] string strAttributeName, [MarshalAs(27)] [In] object AttributeValue, [In] int lFlags = 1);

		// Token: 0x060165EA RID: 91626
		[DispId(-2147417610)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		public virtual extern object getAttribute([MarshalAs(19)] [In] string strAttributeName, [In] int lFlags = 0);

		// Token: 0x060165EB RID: 91627
		[DispId(-2147417609)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool removeAttribute([MarshalAs(19)] [In] string strAttributeName, [In] int lFlags = 1);

		// Token: 0x170076E6 RID: 30438
		// (get) Token: 0x060165ED RID: 91629
		// (set) Token: 0x060165EC RID: 91628
		[DispId(-2147417111)]
		public virtual extern string className
		{
			[TypeLibFunc(4)]
			[DispId(-2147417111)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147417111)]
			[TypeLibFunc(4)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x170076E7 RID: 30439
		// (get) Token: 0x060165EF RID: 91631
		// (set) Token: 0x060165EE RID: 91630
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

		// Token: 0x170076E8 RID: 30440
		// (get) Token: 0x060165F0 RID: 91632
		[DispId(-2147417108)]
		public virtual extern string tagName
		{
			[DispId(-2147417108)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170076E9 RID: 30441
		// (get) Token: 0x060165F1 RID: 91633
		[DispId(-2147418104)]
		public virtual extern IHTMLElement parentElement
		{
			[DispId(-2147418104)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170076EA RID: 30442
		// (get) Token: 0x060165F2 RID: 91634
		[DispId(-2147418038)]
		public virtual extern IHTMLStyle style
		{
			[DispId(-2147418038)]
			[TypeLibFunc(1024)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170076EB RID: 30443
		// (get) Token: 0x060165F4 RID: 91636
		// (set) Token: 0x060165F3 RID: 91635
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

		// Token: 0x170076EC RID: 30444
		// (get) Token: 0x060165F6 RID: 91638
		// (set) Token: 0x060165F5 RID: 91637
		[DispId(-2147412104)]
		public virtual extern object onclick
		{
			[DispId(-2147412104)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412104)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170076ED RID: 30445
		// (get) Token: 0x060165F8 RID: 91640
		// (set) Token: 0x060165F7 RID: 91639
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

		// Token: 0x170076EE RID: 30446
		// (get) Token: 0x060165FA RID: 91642
		// (set) Token: 0x060165F9 RID: 91641
		[DispId(-2147412107)]
		public virtual extern object onkeydown
		{
			[TypeLibFunc(20)]
			[DispId(-2147412107)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412107)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170076EF RID: 30447
		// (get) Token: 0x060165FC RID: 91644
		// (set) Token: 0x060165FB RID: 91643
		[DispId(-2147412106)]
		public virtual extern object onkeyup
		{
			[DispId(-2147412106)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412106)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170076F0 RID: 30448
		// (get) Token: 0x060165FE RID: 91646
		// (set) Token: 0x060165FD RID: 91645
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

		// Token: 0x170076F1 RID: 30449
		// (get) Token: 0x06016600 RID: 91648
		// (set) Token: 0x060165FF RID: 91647
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

		// Token: 0x170076F2 RID: 30450
		// (get) Token: 0x06016602 RID: 91650
		// (set) Token: 0x06016601 RID: 91649
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

		// Token: 0x170076F3 RID: 30451
		// (get) Token: 0x06016604 RID: 91652
		// (set) Token: 0x06016603 RID: 91651
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

		// Token: 0x170076F4 RID: 30452
		// (get) Token: 0x06016606 RID: 91654
		// (set) Token: 0x06016605 RID: 91653
		[DispId(-2147412110)]
		public virtual extern object onmousedown
		{
			[TypeLibFunc(20)]
			[DispId(-2147412110)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412110)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170076F5 RID: 30453
		// (get) Token: 0x06016608 RID: 91656
		// (set) Token: 0x06016607 RID: 91655
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

		// Token: 0x170076F6 RID: 30454
		// (get) Token: 0x06016609 RID: 91657
		[DispId(-2147417094)]
		public virtual extern object document
		{
			[DispId(-2147417094)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x170076F7 RID: 30455
		// (get) Token: 0x0601660B RID: 91659
		// (set) Token: 0x0601660A RID: 91658
		[DispId(-2147418043)]
		public virtual extern string title
		{
			[DispId(-2147418043)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147418043)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x170076F8 RID: 30456
		// (get) Token: 0x0601660D RID: 91661
		// (set) Token: 0x0601660C RID: 91660
		[DispId(-2147413012)]
		public virtual extern string language
		{
			[TypeLibFunc(20)]
			[DispId(-2147413012)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413012)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x170076F9 RID: 30457
		// (get) Token: 0x0601660F RID: 91663
		// (set) Token: 0x0601660E RID: 91662
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

		// Token: 0x06016610 RID: 91664
		[DispId(-2147417093)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void scrollIntoView([MarshalAs(27)] [In] [Optional] object varargStart);

		// Token: 0x06016611 RID: 91665
		[DispId(-2147417092)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool contains([MarshalAs(28)] [In] IHTMLElement pChild);

		// Token: 0x170076FA RID: 30458
		// (get) Token: 0x06016612 RID: 91666
		[DispId(-2147417088)]
		public virtual extern int sourceIndex
		{
			[TypeLibFunc(4)]
			[DispId(-2147417088)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170076FB RID: 30459
		// (get) Token: 0x06016613 RID: 91667
		[DispId(-2147417087)]
		public virtual extern object recordNumber
		{
			[DispId(-2147417087)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x170076FC RID: 30460
		// (get) Token: 0x06016615 RID: 91669
		// (set) Token: 0x06016614 RID: 91668
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

		// Token: 0x170076FD RID: 30461
		// (get) Token: 0x06016616 RID: 91670
		[DispId(-2147417104)]
		public virtual extern int offsetLeft
		{
			[DispId(-2147417104)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170076FE RID: 30462
		// (get) Token: 0x06016617 RID: 91671
		[DispId(-2147417103)]
		public virtual extern int offsetTop
		{
			[DispId(-2147417103)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170076FF RID: 30463
		// (get) Token: 0x06016618 RID: 91672
		[DispId(-2147417102)]
		public virtual extern int offsetWidth
		{
			[DispId(-2147417102)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17007700 RID: 30464
		// (get) Token: 0x06016619 RID: 91673
		[DispId(-2147417101)]
		public virtual extern int offsetHeight
		{
			[DispId(-2147417101)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17007701 RID: 30465
		// (get) Token: 0x0601661A RID: 91674
		[DispId(-2147417100)]
		public virtual extern IHTMLElement offsetParent
		{
			[DispId(-2147417100)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17007702 RID: 30466
		// (get) Token: 0x0601661C RID: 91676
		// (set) Token: 0x0601661B RID: 91675
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

		// Token: 0x17007703 RID: 30467
		// (get) Token: 0x0601661E RID: 91678
		// (set) Token: 0x0601661D RID: 91677
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

		// Token: 0x17007704 RID: 30468
		// (get) Token: 0x06016620 RID: 91680
		// (set) Token: 0x0601661F RID: 91679
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

		// Token: 0x17007705 RID: 30469
		// (get) Token: 0x06016622 RID: 91682
		// (set) Token: 0x06016621 RID: 91681
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

		// Token: 0x06016623 RID: 91683
		[DispId(-2147417082)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void insertAdjacentHTML([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string html);

		// Token: 0x06016624 RID: 91684
		[DispId(-2147417081)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void insertAdjacentText([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string text);

		// Token: 0x17007706 RID: 30470
		// (get) Token: 0x06016625 RID: 91685
		[DispId(-2147417080)]
		public virtual extern IHTMLElement parentTextEdit
		{
			[DispId(-2147417080)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17007707 RID: 30471
		// (get) Token: 0x06016626 RID: 91686
		[DispId(-2147417078)]
		public virtual extern bool isTextEdit
		{
			[DispId(-2147417078)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x06016627 RID: 91687
		[DispId(-2147417079)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void click();

		// Token: 0x17007708 RID: 30472
		// (get) Token: 0x06016628 RID: 91688
		[DispId(-2147417077)]
		public virtual extern IHTMLFiltersCollection filters
		{
			[DispId(-2147417077)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17007709 RID: 30473
		// (get) Token: 0x0601662A RID: 91690
		// (set) Token: 0x06016629 RID: 91689
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

		// Token: 0x0601662B RID: 91691
		[DispId(-2147417076)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string toString();

		// Token: 0x1700770A RID: 30474
		// (get) Token: 0x0601662D RID: 91693
		// (set) Token: 0x0601662C RID: 91692
		[DispId(-2147412091)]
		public virtual extern object onbeforeupdate
		{
			[TypeLibFunc(20)]
			[DispId(-2147412091)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412091)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x1700770B RID: 30475
		// (get) Token: 0x0601662F RID: 91695
		// (set) Token: 0x0601662E RID: 91694
		[DispId(-2147412090)]
		public virtual extern object onafterupdate
		{
			[DispId(-2147412090)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412090)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x1700770C RID: 30476
		// (get) Token: 0x06016631 RID: 91697
		// (set) Token: 0x06016630 RID: 91696
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

		// Token: 0x1700770D RID: 30477
		// (get) Token: 0x06016633 RID: 91699
		// (set) Token: 0x06016632 RID: 91698
		[DispId(-2147412094)]
		public virtual extern object onrowexit
		{
			[DispId(-2147412094)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412094)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x1700770E RID: 30478
		// (get) Token: 0x06016635 RID: 91701
		// (set) Token: 0x06016634 RID: 91700
		[DispId(-2147412093)]
		public virtual extern object onrowenter
		{
			[DispId(-2147412093)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412093)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x1700770F RID: 30479
		// (get) Token: 0x06016637 RID: 91703
		// (set) Token: 0x06016636 RID: 91702
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

		// Token: 0x17007710 RID: 30480
		// (get) Token: 0x06016639 RID: 91705
		// (set) Token: 0x06016638 RID: 91704
		[DispId(-2147412071)]
		public virtual extern object ondataavailable
		{
			[DispId(-2147412071)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412071)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17007711 RID: 30481
		// (get) Token: 0x0601663B RID: 91707
		// (set) Token: 0x0601663A RID: 91706
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

		// Token: 0x17007712 RID: 30482
		// (get) Token: 0x0601663D RID: 91709
		// (set) Token: 0x0601663C RID: 91708
		[DispId(-2147412069)]
		public virtual extern object onfilterchange
		{
			[TypeLibFunc(20)]
			[DispId(-2147412069)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412069)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17007713 RID: 30483
		// (get) Token: 0x0601663E RID: 91710
		[DispId(-2147417075)]
		public virtual extern object children
		{
			[DispId(-2147417075)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17007714 RID: 30484
		// (get) Token: 0x0601663F RID: 91711
		[DispId(-2147417074)]
		public virtual extern object all
		{
			[DispId(-2147417074)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17007715 RID: 30485
		// (get) Token: 0x06016640 RID: 91712
		[DispId(-2147417073)]
		public virtual extern string scopeName
		{
			[DispId(-2147417073)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x06016641 RID: 91713
		[DispId(-2147417072)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void setCapture([In] bool containerCapture = true);

		// Token: 0x06016642 RID: 91714
		[DispId(-2147417071)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void releaseCapture();

		// Token: 0x17007716 RID: 30486
		// (get) Token: 0x06016644 RID: 91716
		// (set) Token: 0x06016643 RID: 91715
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

		// Token: 0x06016645 RID: 91717
		[DispId(-2147417070)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string componentFromPoint([In] int x, [In] int y);

		// Token: 0x06016646 RID: 91718
		[DispId(-2147417069)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void doScroll([MarshalAs(27)] [In] [Optional] object component);

		// Token: 0x17007717 RID: 30487
		// (get) Token: 0x06016648 RID: 91720
		// (set) Token: 0x06016647 RID: 91719
		[DispId(-2147412081)]
		public virtual extern object onscroll
		{
			[DispId(-2147412081)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412081)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17007718 RID: 30488
		// (get) Token: 0x0601664A RID: 91722
		// (set) Token: 0x06016649 RID: 91721
		[DispId(-2147412063)]
		public virtual extern object ondrag
		{
			[DispId(-2147412063)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412063)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17007719 RID: 30489
		// (get) Token: 0x0601664C RID: 91724
		// (set) Token: 0x0601664B RID: 91723
		[DispId(-2147412062)]
		public virtual extern object ondragend
		{
			[TypeLibFunc(20)]
			[DispId(-2147412062)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412062)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x1700771A RID: 30490
		// (get) Token: 0x0601664E RID: 91726
		// (set) Token: 0x0601664D RID: 91725
		[DispId(-2147412061)]
		public virtual extern object ondragenter
		{
			[DispId(-2147412061)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412061)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x1700771B RID: 30491
		// (get) Token: 0x06016650 RID: 91728
		// (set) Token: 0x0601664F RID: 91727
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

		// Token: 0x1700771C RID: 30492
		// (get) Token: 0x06016652 RID: 91730
		// (set) Token: 0x06016651 RID: 91729
		[DispId(-2147412059)]
		public virtual extern object ondragleave
		{
			[TypeLibFunc(20)]
			[DispId(-2147412059)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412059)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x1700771D RID: 30493
		// (get) Token: 0x06016654 RID: 91732
		// (set) Token: 0x06016653 RID: 91731
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

		// Token: 0x1700771E RID: 30494
		// (get) Token: 0x06016656 RID: 91734
		// (set) Token: 0x06016655 RID: 91733
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

		// Token: 0x1700771F RID: 30495
		// (get) Token: 0x06016658 RID: 91736
		// (set) Token: 0x06016657 RID: 91735
		[DispId(-2147412057)]
		public virtual extern object oncut
		{
			[DispId(-2147412057)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412057)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17007720 RID: 30496
		// (get) Token: 0x0601665A RID: 91738
		// (set) Token: 0x06016659 RID: 91737
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

		// Token: 0x17007721 RID: 30497
		// (get) Token: 0x0601665C RID: 91740
		// (set) Token: 0x0601665B RID: 91739
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

		// Token: 0x17007722 RID: 30498
		// (get) Token: 0x0601665E RID: 91742
		// (set) Token: 0x0601665D RID: 91741
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

		// Token: 0x17007723 RID: 30499
		// (get) Token: 0x06016660 RID: 91744
		// (set) Token: 0x0601665F RID: 91743
		[DispId(-2147412055)]
		public virtual extern object onpaste
		{
			[TypeLibFunc(20)]
			[DispId(-2147412055)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412055)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17007724 RID: 30500
		// (get) Token: 0x06016661 RID: 91745
		[DispId(-2147417105)]
		public virtual extern IHTMLCurrentStyle currentStyle
		{
			[DispId(-2147417105)]
			[TypeLibFunc(1024)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17007725 RID: 30501
		// (get) Token: 0x06016663 RID: 91747
		// (set) Token: 0x06016662 RID: 91746
		[DispId(-2147412065)]
		public virtual extern object onpropertychange
		{
			[TypeLibFunc(20)]
			[DispId(-2147412065)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412065)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x06016664 RID: 91748
		[DispId(-2147417068)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLRectCollection getClientRects();

		// Token: 0x06016665 RID: 91749
		[DispId(-2147417067)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLRect getBoundingClientRect();

		// Token: 0x06016666 RID: 91750
		[DispId(-2147417608)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void setExpression([MarshalAs(19)] [In] string propname, [MarshalAs(19)] [In] string expression, [MarshalAs(19)] [In] string language = "");

		// Token: 0x06016667 RID: 91751
		[DispId(-2147417607)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		public virtual extern object getExpression([MarshalAs(19)] [In] string propname);

		// Token: 0x06016668 RID: 91752
		[DispId(-2147417606)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool removeExpression([MarshalAs(19)] [In] string propname);

		// Token: 0x17007726 RID: 30502
		// (get) Token: 0x0601666A RID: 91754
		// (set) Token: 0x06016669 RID: 91753
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

		// Token: 0x0601666B RID: 91755
		[DispId(-2147416112)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void focus();

		// Token: 0x17007727 RID: 30503
		// (get) Token: 0x0601666D RID: 91757
		// (set) Token: 0x0601666C RID: 91756
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

		// Token: 0x17007728 RID: 30504
		// (get) Token: 0x0601666F RID: 91759
		// (set) Token: 0x0601666E RID: 91758
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

		// Token: 0x17007729 RID: 30505
		// (get) Token: 0x06016671 RID: 91761
		// (set) Token: 0x06016670 RID: 91760
		[DispId(-2147412098)]
		public virtual extern object onfocus
		{
			[DispId(-2147412098)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412098)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x1700772A RID: 30506
		// (get) Token: 0x06016673 RID: 91763
		// (set) Token: 0x06016672 RID: 91762
		[DispId(-2147412076)]
		public virtual extern object onresize
		{
			[TypeLibFunc(20)]
			[DispId(-2147412076)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412076)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x06016674 RID: 91764
		[DispId(-2147416110)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void blur();

		// Token: 0x06016675 RID: 91765
		[DispId(-2147416095)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void addFilter([MarshalAs(25)] [In] object pUnk);

		// Token: 0x06016676 RID: 91766
		[DispId(-2147416094)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void removeFilter([MarshalAs(25)] [In] object pUnk);

		// Token: 0x1700772B RID: 30507
		// (get) Token: 0x06016677 RID: 91767
		[DispId(-2147416093)]
		public virtual extern int clientHeight
		{
			[DispId(-2147416093)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700772C RID: 30508
		// (get) Token: 0x06016678 RID: 91768
		[DispId(-2147416092)]
		public virtual extern int clientWidth
		{
			[DispId(-2147416092)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700772D RID: 30509
		// (get) Token: 0x06016679 RID: 91769
		[DispId(-2147416091)]
		public virtual extern int clientTop
		{
			[TypeLibFunc(20)]
			[DispId(-2147416091)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700772E RID: 30510
		// (get) Token: 0x0601667A RID: 91770
		[DispId(-2147416090)]
		public virtual extern int clientLeft
		{
			[DispId(-2147416090)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x0601667B RID: 91771
		[DispId(-2147417605)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool attachEvent([MarshalAs(19)] [In] string @event, [MarshalAs(26)] [In] object pdisp);

		// Token: 0x0601667C RID: 91772
		[DispId(-2147417604)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void detachEvent([MarshalAs(19)] [In] string @event, [MarshalAs(26)] [In] object pdisp);

		// Token: 0x1700772F RID: 30511
		// (get) Token: 0x0601667D RID: 91773
		[DispId(-2147412996)]
		public virtual extern object readyState
		{
			[DispId(-2147412996)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x17007730 RID: 30512
		// (get) Token: 0x0601667F RID: 91775
		// (set) Token: 0x0601667E RID: 91774
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

		// Token: 0x17007731 RID: 30513
		// (get) Token: 0x06016681 RID: 91777
		// (set) Token: 0x06016680 RID: 91776
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

		// Token: 0x17007732 RID: 30514
		// (get) Token: 0x06016683 RID: 91779
		// (set) Token: 0x06016682 RID: 91778
		[DispId(-2147412049)]
		public virtual extern object onrowsinserted
		{
			[TypeLibFunc(20)]
			[DispId(-2147412049)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412049)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17007733 RID: 30515
		// (get) Token: 0x06016685 RID: 91781
		// (set) Token: 0x06016684 RID: 91780
		[DispId(-2147412048)]
		public virtual extern object oncellchange
		{
			[TypeLibFunc(20)]
			[DispId(-2147412048)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412048)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17007734 RID: 30516
		// (get) Token: 0x06016687 RID: 91783
		// (set) Token: 0x06016686 RID: 91782
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

		// Token: 0x06016688 RID: 91784
		[DispId(-2147417056)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(26)]
		public virtual extern object createControlRange();

		// Token: 0x17007735 RID: 30517
		// (get) Token: 0x06016689 RID: 91785
		[DispId(-2147417055)]
		public virtual extern int scrollHeight
		{
			[DispId(-2147417055)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17007736 RID: 30518
		// (get) Token: 0x0601668A RID: 91786
		[DispId(-2147417054)]
		public virtual extern int scrollWidth
		{
			[TypeLibFunc(20)]
			[DispId(-2147417054)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17007737 RID: 30519
		// (get) Token: 0x0601668C RID: 91788
		// (set) Token: 0x0601668B RID: 91787
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

		// Token: 0x17007738 RID: 30520
		// (get) Token: 0x0601668E RID: 91790
		// (set) Token: 0x0601668D RID: 91789
		[DispId(-2147417052)]
		public virtual extern int scrollLeft
		{
			[DispId(-2147417052)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[DispId(-2147417052)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x0601668F RID: 91791
		[DispId(-2147417050)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void clearAttributes();

		// Token: 0x17007739 RID: 30521
		// (get) Token: 0x06016691 RID: 91793
		// (set) Token: 0x06016690 RID: 91792
		[DispId(-2147412047)]
		public virtual extern object oncontextmenu
		{
			[TypeLibFunc(20)]
			[DispId(-2147412047)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412047)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x06016692 RID: 91794
		[DispId(-2147417043)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElement insertAdjacentElement([MarshalAs(19)] [In] string where, [MarshalAs(28)] [In] IHTMLElement insertedElement);

		// Token: 0x06016693 RID: 91795
		[DispId(-2147417047)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElement applyElement([MarshalAs(28)] [In] IHTMLElement apply, [MarshalAs(19)] [In] string where);

		// Token: 0x06016694 RID: 91796
		[DispId(-2147417042)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string getAdjacentText([MarshalAs(19)] [In] string where);

		// Token: 0x06016695 RID: 91797
		[DispId(-2147417041)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string replaceAdjacentText([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string newText);

		// Token: 0x1700773A RID: 30522
		// (get) Token: 0x06016696 RID: 91798
		[DispId(-2147417040)]
		public virtual extern bool canHaveChildren
		{
			[DispId(-2147417040)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x06016697 RID: 91799
		[DispId(-2147417032)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern int addBehavior([MarshalAs(19)] [In] string bstrUrl, [MarshalAs(27)] [In] [Optional] ref object pvarFactory);

		// Token: 0x06016698 RID: 91800
		[DispId(-2147417031)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool removeBehavior([In] int cookie);

		// Token: 0x1700773B RID: 30523
		// (get) Token: 0x06016699 RID: 91801
		[DispId(-2147417048)]
		public virtual extern IHTMLStyle runtimeStyle
		{
			[TypeLibFunc(1024)]
			[DispId(-2147417048)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x1700773C RID: 30524
		// (get) Token: 0x0601669A RID: 91802
		[DispId(-2147417030)]
		public virtual extern object behaviorUrns
		{
			[DispId(-2147417030)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x1700773D RID: 30525
		// (get) Token: 0x0601669C RID: 91804
		// (set) Token: 0x0601669B RID: 91803
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

		// Token: 0x1700773E RID: 30526
		// (get) Token: 0x0601669E RID: 91806
		// (set) Token: 0x0601669D RID: 91805
		[DispId(-2147412043)]
		public virtual extern object onbeforeeditfocus
		{
			[TypeLibFunc(20)]
			[DispId(-2147412043)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412043)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x1700773F RID: 30527
		// (get) Token: 0x0601669F RID: 91807
		[DispId(-2147417028)]
		public virtual extern int readyStateValue
		{
			[DispId(-2147417028)]
			[TypeLibFunc(65)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x060166A0 RID: 91808
		[DispId(-2147417027)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElementCollection getElementsByTagName([MarshalAs(19)] [In] string v);

		// Token: 0x060166A1 RID: 91809
		[DispId(-2147417016)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void mergeAttributes([MarshalAs(28)] [In] IHTMLElement mergeThis, [MarshalAs(27)] [In] [Optional] ref object pvarFlags);

		// Token: 0x17007740 RID: 30528
		// (get) Token: 0x060166A2 RID: 91810
		[DispId(-2147417015)]
		public virtual extern bool isMultiLine
		{
			[DispId(-2147417015)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17007741 RID: 30529
		// (get) Token: 0x060166A3 RID: 91811
		[DispId(-2147417014)]
		public virtual extern bool canHaveHTML
		{
			[DispId(-2147417014)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17007742 RID: 30530
		// (get) Token: 0x060166A5 RID: 91813
		// (set) Token: 0x060166A4 RID: 91812
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

		// Token: 0x17007743 RID: 30531
		// (get) Token: 0x060166A7 RID: 91815
		// (set) Token: 0x060166A6 RID: 91814
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

		// Token: 0x17007744 RID: 30532
		// (get) Token: 0x060166A9 RID: 91817
		// (set) Token: 0x060166A8 RID: 91816
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

		// Token: 0x17007745 RID: 30533
		// (get) Token: 0x060166AB RID: 91819
		// (set) Token: 0x060166AA RID: 91818
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

		// Token: 0x060166AC RID: 91820
		[DispId(-2147417011)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void setActive();

		// Token: 0x17007746 RID: 30534
		// (get) Token: 0x060166AE RID: 91822
		// (set) Token: 0x060166AD RID: 91821
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

		// Token: 0x17007747 RID: 30535
		// (get) Token: 0x060166AF RID: 91823
		[DispId(-2147417010)]
		public virtual extern bool isContentEditable
		{
			[DispId(-2147417010)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17007748 RID: 30536
		// (get) Token: 0x060166B1 RID: 91825
		// (set) Token: 0x060166B0 RID: 91824
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

		// Token: 0x17007749 RID: 30537
		// (get) Token: 0x060166B3 RID: 91827
		// (set) Token: 0x060166B2 RID: 91826
		[DispId(-2147418036)]
		public virtual extern bool disabled
		{
			[DispId(-2147418036)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[DispId(-2147418036)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x1700774A RID: 30538
		// (get) Token: 0x060166B4 RID: 91828
		[DispId(-2147417007)]
		public virtual extern bool isDisabled
		{
			[DispId(-2147417007)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700774B RID: 30539
		// (get) Token: 0x060166B6 RID: 91830
		// (set) Token: 0x060166B5 RID: 91829
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

		// Token: 0x1700774C RID: 30540
		// (get) Token: 0x060166B8 RID: 91832
		// (set) Token: 0x060166B7 RID: 91831
		[DispId(-2147412033)]
		public virtual extern object oncontrolselect
		{
			[DispId(-2147412033)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412033)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x060166B9 RID: 91833
		[DispId(-2147417006)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool FireEvent([MarshalAs(19)] [In] string bstrEventName, [MarshalAs(27)] [In] [Optional] ref object pvarEventObject);

		// Token: 0x1700774D RID: 30541
		// (get) Token: 0x060166BB RID: 91835
		// (set) Token: 0x060166BA RID: 91834
		[DispId(-2147412029)]
		public virtual extern object onresizestart
		{
			[DispId(-2147412029)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412029)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x1700774E RID: 30542
		// (get) Token: 0x060166BD RID: 91837
		// (set) Token: 0x060166BC RID: 91836
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

		// Token: 0x1700774F RID: 30543
		// (get) Token: 0x060166BF RID: 91839
		// (set) Token: 0x060166BE RID: 91838
		[DispId(-2147412031)]
		public virtual extern object onmovestart
		{
			[TypeLibFunc(20)]
			[DispId(-2147412031)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412031)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17007750 RID: 30544
		// (get) Token: 0x060166C1 RID: 91841
		// (set) Token: 0x060166C0 RID: 91840
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

		// Token: 0x17007751 RID: 30545
		// (get) Token: 0x060166C3 RID: 91843
		// (set) Token: 0x060166C2 RID: 91842
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

		// Token: 0x17007752 RID: 30546
		// (get) Token: 0x060166C5 RID: 91845
		// (set) Token: 0x060166C4 RID: 91844
		[DispId(-2147412026)]
		public virtual extern object onmouseleave
		{
			[DispId(-2147412026)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412026)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17007753 RID: 30547
		// (get) Token: 0x060166C7 RID: 91847
		// (set) Token: 0x060166C6 RID: 91846
		[DispId(-2147412025)]
		public virtual extern object onactivate
		{
			[TypeLibFunc(20)]
			[DispId(-2147412025)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412025)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17007754 RID: 30548
		// (get) Token: 0x060166C9 RID: 91849
		// (set) Token: 0x060166C8 RID: 91848
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

		// Token: 0x060166CA RID: 91850
		[DispId(-2147417005)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool dragDrop();

		// Token: 0x17007755 RID: 30549
		// (get) Token: 0x060166CB RID: 91851
		[DispId(-2147417004)]
		public virtual extern int glyphMode
		{
			[DispId(-2147417004)]
			[TypeLibFunc(1089)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17007756 RID: 30550
		// (get) Token: 0x060166CD RID: 91853
		// (set) Token: 0x060166CC RID: 91852
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

		// Token: 0x060166CE RID: 91854
		[DispId(-2147417000)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void normalize();

		// Token: 0x060166CF RID: 91855
		[DispId(-2147417003)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute getAttributeNode([MarshalAs(19)] [In] string bstrName);

		// Token: 0x060166D0 RID: 91856
		[DispId(-2147417002)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute setAttributeNode([MarshalAs(28)] [In] IHTMLDOMAttribute pattr);

		// Token: 0x060166D1 RID: 91857
		[DispId(-2147417001)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute removeAttributeNode([MarshalAs(28)] [In] IHTMLDOMAttribute pattr);

		// Token: 0x17007757 RID: 30551
		// (get) Token: 0x060166D3 RID: 91859
		// (set) Token: 0x060166D2 RID: 91858
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

		// Token: 0x17007758 RID: 30552
		// (get) Token: 0x060166D5 RID: 91861
		// (set) Token: 0x060166D4 RID: 91860
		[DispId(-2147412021)]
		public virtual extern object onfocusin
		{
			[DispId(-2147412021)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412021)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17007759 RID: 30553
		// (get) Token: 0x060166D7 RID: 91863
		// (set) Token: 0x060166D6 RID: 91862
		[DispId(-2147412020)]
		public virtual extern object onfocusout
		{
			[TypeLibFunc(20)]
			[DispId(-2147412020)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412020)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x1700775A RID: 30554
		// (get) Token: 0x060166D8 RID: 91864
		[DispId(-2147417058)]
		public virtual extern int uniqueNumber
		{
			[DispId(-2147417058)]
			[TypeLibFunc(64)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700775B RID: 30555
		// (get) Token: 0x060166D9 RID: 91865
		[DispId(-2147417057)]
		public virtual extern string uniqueID
		{
			[TypeLibFunc(64)]
			[DispId(-2147417057)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x1700775C RID: 30556
		// (get) Token: 0x060166DA RID: 91866
		[DispId(-2147417066)]
		public virtual extern int nodeType
		{
			[DispId(-2147417066)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700775D RID: 30557
		// (get) Token: 0x060166DB RID: 91867
		[DispId(-2147417065)]
		public virtual extern IHTMLDOMNode parentNode
		{
			[DispId(-2147417065)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x060166DC RID: 91868
		[DispId(-2147417064)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool hasChildNodes();

		// Token: 0x1700775E RID: 30558
		// (get) Token: 0x060166DD RID: 91869
		[DispId(-2147417063)]
		public virtual extern object childNodes
		{
			[DispId(-2147417063)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x1700775F RID: 30559
		// (get) Token: 0x060166DE RID: 91870
		[DispId(-2147417062)]
		public virtual extern object attributes
		{
			[DispId(-2147417062)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x060166DF RID: 91871
		[DispId(-2147417061)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode insertBefore([MarshalAs(28)] [In] IHTMLDOMNode newChild, [MarshalAs(27)] [In] [Optional] object refChild);

		// Token: 0x060166E0 RID: 91872
		[DispId(-2147417060)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode removeChild([MarshalAs(28)] [In] IHTMLDOMNode oldChild);

		// Token: 0x060166E1 RID: 91873
		[DispId(-2147417059)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode replaceChild([MarshalAs(28)] [In] IHTMLDOMNode newChild, [MarshalAs(28)] [In] IHTMLDOMNode oldChild);

		// Token: 0x060166E2 RID: 91874
		[DispId(-2147417051)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode cloneNode([In] bool fDeep);

		// Token: 0x060166E3 RID: 91875
		[DispId(-2147417046)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode removeNode([In] bool fDeep = false);

		// Token: 0x060166E4 RID: 91876
		[DispId(-2147417044)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode swapNode([MarshalAs(28)] [In] IHTMLDOMNode otherNode);

		// Token: 0x060166E5 RID: 91877
		[DispId(-2147417045)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode replaceNode([MarshalAs(28)] [In] IHTMLDOMNode replacement);

		// Token: 0x060166E6 RID: 91878
		[DispId(-2147417039)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode appendChild([MarshalAs(28)] [In] IHTMLDOMNode newChild);

		// Token: 0x17007760 RID: 30560
		// (get) Token: 0x060166E7 RID: 91879
		[DispId(-2147417038)]
		public virtual extern string nodeName
		{
			[DispId(-2147417038)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17007761 RID: 30561
		// (get) Token: 0x060166E9 RID: 91881
		// (set) Token: 0x060166E8 RID: 91880
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

		// Token: 0x17007762 RID: 30562
		// (get) Token: 0x060166EA RID: 91882
		[DispId(-2147417036)]
		public virtual extern IHTMLDOMNode firstChild
		{
			[DispId(-2147417036)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17007763 RID: 30563
		// (get) Token: 0x060166EB RID: 91883
		[DispId(-2147417035)]
		public virtual extern IHTMLDOMNode lastChild
		{
			[DispId(-2147417035)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17007764 RID: 30564
		// (get) Token: 0x060166EC RID: 91884
		[DispId(-2147417034)]
		public virtual extern IHTMLDOMNode previousSibling
		{
			[DispId(-2147417034)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17007765 RID: 30565
		// (get) Token: 0x060166ED RID: 91885
		[DispId(-2147417033)]
		public virtual extern IHTMLDOMNode nextSibling
		{
			[DispId(-2147417033)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17007766 RID: 30566
		// (get) Token: 0x060166EE RID: 91886
		[DispId(-2147416999)]
		public virtual extern object ownerDocument
		{
			[DispId(-2147416999)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17007767 RID: 30567
		// (get) Token: 0x060166F0 RID: 91888
		// (set) Token: 0x060166EF RID: 91887
		[DispId(1001)]
		public virtual extern string action
		{
			[DispId(1001)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(1001)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17007768 RID: 30568
		// (get) Token: 0x060166F2 RID: 91890
		// (set) Token: 0x060166F1 RID: 91889
		[DispId(1003)]
		public virtual extern string encoding
		{
			[DispId(1003)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(1003)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17007769 RID: 30569
		// (get) Token: 0x060166F4 RID: 91892
		// (set) Token: 0x060166F3 RID: 91891
		[DispId(1004)]
		public virtual extern string method
		{
			[TypeLibFunc(20)]
			[DispId(1004)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(1004)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x1700776A RID: 30570
		// (get) Token: 0x060166F5 RID: 91893
		[DispId(1005)]
		public virtual extern object elements
		{
			[DispId(1005)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x1700776B RID: 30571
		// (get) Token: 0x060166F7 RID: 91895
		// (set) Token: 0x060166F6 RID: 91894
		[DispId(1006)]
		public virtual extern string target
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

		// Token: 0x1700776C RID: 30572
		// (get) Token: 0x060166F9 RID: 91897
		// (set) Token: 0x060166F8 RID: 91896
		[DispId(-2147418112)]
		public virtual extern string name
		{
			[TypeLibFunc(20)]
			[DispId(-2147418112)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147418112)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x1700776D RID: 30573
		// (get) Token: 0x060166FB RID: 91899
		// (set) Token: 0x060166FA RID: 91898
		[DispId(-2147412101)]
		public virtual extern object onsubmit
		{
			[TypeLibFunc(20)]
			[DispId(-2147412101)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412101)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x1700776E RID: 30574
		// (get) Token: 0x060166FD RID: 91901
		// (set) Token: 0x060166FC RID: 91900
		[DispId(-2147412100)]
		public virtual extern object onreset
		{
			[DispId(-2147412100)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412100)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x060166FE RID: 91902
		[DispId(1009)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void submit();

		// Token: 0x060166FF RID: 91903
		[DispId(1010)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void reset();

		// Token: 0x1700776F RID: 30575
		// (get) Token: 0x06016701 RID: 91905
		// (set) Token: 0x06016700 RID: 91904
		[DispId(1500)]
		public virtual extern int length
		{
			[DispId(1500)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[DispId(1500)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x06016702 RID: 91906
		[TypeLibFunc(65)]
		[DispId(-4)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(44, MarshalTypeRef = System.Runtime.InteropServices.CustomMarshalers.EnumeratorToEnumVariantMarshaler)]
		public virtual extern IEnumerator GetEnumerator();

		// Token: 0x06016703 RID: 91907
		[DispId(0)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(26)]
		public virtual extern object item([MarshalAs(27)] [In] [Optional] object name, [MarshalAs(27)] [In] [Optional] object index);

		// Token: 0x06016704 RID: 91908
		[DispId(1502)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(26)]
		public virtual extern object tags([MarshalAs(27)] [In] object tagName);

		// Token: 0x17007770 RID: 30576
		// (get) Token: 0x06016706 RID: 91910
		// (set) Token: 0x06016705 RID: 91909
		[DispId(1011)]
		public virtual extern string acceptCharset
		{
			[TypeLibFunc(20)]
			[DispId(1011)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(1011)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x06016707 RID: 91911
		[DispId(1505)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(26)]
		public virtual extern object urns([MarshalAs(27)] [In] object urn);

		// Token: 0x06016708 RID: 91912
		[DispId(1506)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(26)]
		public virtual extern object namedItem([MarshalAs(19)] [In] string name);

		// Token: 0x06016709 RID: 91913
		[DispId(1012)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void appendNameValuePair([MarshalAs(19)] [In] string name = "", [MarshalAs(19)] [In] string value = "");

		// Token: 0x0601670A RID: 91914
		[DispId(1013)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void appendNameFilePair([MarshalAs(19)] [In] string name = "", [MarshalAs(19)] [In] string filename = "");

		// Token: 0x0601670B RID: 91915
		[DispId(1014)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void appendItemSeparator();

		// Token: 0x17007771 RID: 30577
		// (get) Token: 0x0601670D RID: 91917
		// (set) Token: 0x0601670C RID: 91916
		public virtual extern string IHTMLFormElement_action
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

		// Token: 0x17007772 RID: 30578
		// (get) Token: 0x0601670F RID: 91919
		// (set) Token: 0x0601670E RID: 91918
		public virtual extern string IHTMLFormElement_dir
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

		// Token: 0x17007773 RID: 30579
		// (get) Token: 0x06016711 RID: 91921
		// (set) Token: 0x06016710 RID: 91920
		public virtual extern string IHTMLFormElement_encoding
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

		// Token: 0x17007774 RID: 30580
		// (get) Token: 0x06016713 RID: 91923
		// (set) Token: 0x06016712 RID: 91922
		public virtual extern string IHTMLFormElement_method
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

		// Token: 0x17007775 RID: 30581
		// (get) Token: 0x06016714 RID: 91924
		public virtual extern object IHTMLFormElement_elements
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17007776 RID: 30582
		// (get) Token: 0x06016716 RID: 91926
		// (set) Token: 0x06016715 RID: 91925
		public virtual extern string IHTMLFormElement_target
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

		// Token: 0x17007777 RID: 30583
		// (get) Token: 0x06016718 RID: 91928
		// (set) Token: 0x06016717 RID: 91927
		public virtual extern string IHTMLFormElement_name
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

		// Token: 0x17007778 RID: 30584
		// (get) Token: 0x0601671A RID: 91930
		// (set) Token: 0x06016719 RID: 91929
		public virtual extern object IHTMLFormElement_onsubmit
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

		// Token: 0x17007779 RID: 30585
		// (get) Token: 0x0601671C RID: 91932
		// (set) Token: 0x0601671B RID: 91931
		public virtual extern object IHTMLFormElement_onreset
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

		// Token: 0x0601671D RID: 91933
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLFormElement_submit();

		// Token: 0x0601671E RID: 91934
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLFormElement_reset();

		// Token: 0x1700777A RID: 30586
		// (get) Token: 0x06016720 RID: 91936
		// (set) Token: 0x0601671F RID: 91935
		public virtual extern int IHTMLFormElement_length
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x06016721 RID: 91937
		[TypeLibFunc(65)]
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(44, MarshalTypeRef = System.Runtime.InteropServices.CustomMarshalers.EnumeratorToEnumVariantMarshaler)]
		public virtual extern IEnumerator IHTMLFormElement_GetEnumerator();

		// Token: 0x06016722 RID: 91938
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(26)]
		public virtual extern object IHTMLFormElement_item([MarshalAs(27)] [In] [Optional] object name, [MarshalAs(27)] [In] [Optional] object index);

		// Token: 0x06016723 RID: 91939
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(26)]
		public virtual extern object IHTMLFormElement_tags([MarshalAs(27)] [In] object tagName);

		// Token: 0x06016724 RID: 91940
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement_setAttribute([MarshalAs(19)] [In] string strAttributeName, [MarshalAs(27)] [In] object AttributeValue, [In] int lFlags = 1);

		// Token: 0x06016725 RID: 91941
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		public virtual extern object IHTMLElement_getAttribute([MarshalAs(19)] [In] string strAttributeName, [In] int lFlags = 0);

		// Token: 0x06016726 RID: 91942
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement_removeAttribute([MarshalAs(19)] [In] string strAttributeName, [In] int lFlags = 1);

		// Token: 0x1700777B RID: 30587
		// (get) Token: 0x06016728 RID: 91944
		// (set) Token: 0x06016727 RID: 91943
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

		// Token: 0x1700777C RID: 30588
		// (get) Token: 0x0601672A RID: 91946
		// (set) Token: 0x06016729 RID: 91945
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

		// Token: 0x1700777D RID: 30589
		// (get) Token: 0x0601672B RID: 91947
		public virtual extern string IHTMLElement_tagName
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x1700777E RID: 30590
		// (get) Token: 0x0601672C RID: 91948
		public virtual extern IHTMLElement IHTMLElement_parentElement
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x1700777F RID: 30591
		// (get) Token: 0x0601672D RID: 91949
		public virtual extern IHTMLStyle IHTMLElement_style
		{
			[TypeLibFunc(1024)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17007780 RID: 30592
		// (get) Token: 0x0601672F RID: 91951
		// (set) Token: 0x0601672E RID: 91950
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

		// Token: 0x17007781 RID: 30593
		// (get) Token: 0x06016731 RID: 91953
		// (set) Token: 0x06016730 RID: 91952
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

		// Token: 0x17007782 RID: 30594
		// (get) Token: 0x06016733 RID: 91955
		// (set) Token: 0x06016732 RID: 91954
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

		// Token: 0x17007783 RID: 30595
		// (get) Token: 0x06016735 RID: 91957
		// (set) Token: 0x06016734 RID: 91956
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

		// Token: 0x17007784 RID: 30596
		// (get) Token: 0x06016737 RID: 91959
		// (set) Token: 0x06016736 RID: 91958
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

		// Token: 0x17007785 RID: 30597
		// (get) Token: 0x06016739 RID: 91961
		// (set) Token: 0x06016738 RID: 91960
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

		// Token: 0x17007786 RID: 30598
		// (get) Token: 0x0601673B RID: 91963
		// (set) Token: 0x0601673A RID: 91962
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

		// Token: 0x17007787 RID: 30599
		// (get) Token: 0x0601673D RID: 91965
		// (set) Token: 0x0601673C RID: 91964
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

		// Token: 0x17007788 RID: 30600
		// (get) Token: 0x0601673F RID: 91967
		// (set) Token: 0x0601673E RID: 91966
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

		// Token: 0x17007789 RID: 30601
		// (get) Token: 0x06016741 RID: 91969
		// (set) Token: 0x06016740 RID: 91968
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

		// Token: 0x1700778A RID: 30602
		// (get) Token: 0x06016743 RID: 91971
		// (set) Token: 0x06016742 RID: 91970
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

		// Token: 0x1700778B RID: 30603
		// (get) Token: 0x06016744 RID: 91972
		public virtual extern object IHTMLElement_document
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x1700778C RID: 30604
		// (get) Token: 0x06016746 RID: 91974
		// (set) Token: 0x06016745 RID: 91973
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

		// Token: 0x1700778D RID: 30605
		// (get) Token: 0x06016748 RID: 91976
		// (set) Token: 0x06016747 RID: 91975
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

		// Token: 0x1700778E RID: 30606
		// (get) Token: 0x0601674A RID: 91978
		// (set) Token: 0x06016749 RID: 91977
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

		// Token: 0x0601674B RID: 91979
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement_scrollIntoView([MarshalAs(27)] [In] [Optional] object varargStart);

		// Token: 0x0601674C RID: 91980
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement_contains([MarshalAs(28)] [In] IHTMLElement pChild);

		// Token: 0x1700778F RID: 30607
		// (get) Token: 0x0601674D RID: 91981
		public virtual extern int IHTMLElement_sourceIndex
		{
			[TypeLibFunc(4)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17007790 RID: 30608
		// (get) Token: 0x0601674E RID: 91982
		public virtual extern object IHTMLElement_recordNumber
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x17007791 RID: 30609
		// (get) Token: 0x06016750 RID: 91984
		// (set) Token: 0x0601674F RID: 91983
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

		// Token: 0x17007792 RID: 30610
		// (get) Token: 0x06016751 RID: 91985
		public virtual extern int IHTMLElement_offsetLeft
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17007793 RID: 30611
		// (get) Token: 0x06016752 RID: 91986
		public virtual extern int IHTMLElement_offsetTop
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17007794 RID: 30612
		// (get) Token: 0x06016753 RID: 91987
		public virtual extern int IHTMLElement_offsetWidth
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17007795 RID: 30613
		// (get) Token: 0x06016754 RID: 91988
		public virtual extern int IHTMLElement_offsetHeight
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17007796 RID: 30614
		// (get) Token: 0x06016755 RID: 91989
		public virtual extern IHTMLElement IHTMLElement_offsetParent
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17007797 RID: 30615
		// (get) Token: 0x06016757 RID: 91991
		// (set) Token: 0x06016756 RID: 91990
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

		// Token: 0x17007798 RID: 30616
		// (get) Token: 0x06016759 RID: 91993
		// (set) Token: 0x06016758 RID: 91992
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

		// Token: 0x17007799 RID: 30617
		// (get) Token: 0x0601675B RID: 91995
		// (set) Token: 0x0601675A RID: 91994
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

		// Token: 0x1700779A RID: 30618
		// (get) Token: 0x0601675D RID: 91997
		// (set) Token: 0x0601675C RID: 91996
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

		// Token: 0x0601675E RID: 91998
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement_insertAdjacentHTML([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string html);

		// Token: 0x0601675F RID: 91999
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement_insertAdjacentText([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string text);

		// Token: 0x1700779B RID: 30619
		// (get) Token: 0x06016760 RID: 92000
		public virtual extern IHTMLElement IHTMLElement_parentTextEdit
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x1700779C RID: 30620
		// (get) Token: 0x06016761 RID: 92001
		public virtual extern bool IHTMLElement_isTextEdit
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x06016762 RID: 92002
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement_click();

		// Token: 0x1700779D RID: 30621
		// (get) Token: 0x06016763 RID: 92003
		public virtual extern IHTMLFiltersCollection IHTMLElement_filters
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x1700779E RID: 30622
		// (get) Token: 0x06016765 RID: 92005
		// (set) Token: 0x06016764 RID: 92004
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

		// Token: 0x06016766 RID: 92006
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string IHTMLElement_toString();

		// Token: 0x1700779F RID: 30623
		// (get) Token: 0x06016768 RID: 92008
		// (set) Token: 0x06016767 RID: 92007
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

		// Token: 0x170077A0 RID: 30624
		// (get) Token: 0x0601676A RID: 92010
		// (set) Token: 0x06016769 RID: 92009
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

		// Token: 0x170077A1 RID: 30625
		// (get) Token: 0x0601676C RID: 92012
		// (set) Token: 0x0601676B RID: 92011
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

		// Token: 0x170077A2 RID: 30626
		// (get) Token: 0x0601676E RID: 92014
		// (set) Token: 0x0601676D RID: 92013
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

		// Token: 0x170077A3 RID: 30627
		// (get) Token: 0x06016770 RID: 92016
		// (set) Token: 0x0601676F RID: 92015
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

		// Token: 0x170077A4 RID: 30628
		// (get) Token: 0x06016772 RID: 92018
		// (set) Token: 0x06016771 RID: 92017
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

		// Token: 0x170077A5 RID: 30629
		// (get) Token: 0x06016774 RID: 92020
		// (set) Token: 0x06016773 RID: 92019
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

		// Token: 0x170077A6 RID: 30630
		// (get) Token: 0x06016776 RID: 92022
		// (set) Token: 0x06016775 RID: 92021
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

		// Token: 0x170077A7 RID: 30631
		// (get) Token: 0x06016778 RID: 92024
		// (set) Token: 0x06016777 RID: 92023
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

		// Token: 0x170077A8 RID: 30632
		// (get) Token: 0x06016779 RID: 92025
		public virtual extern object IHTMLElement_children
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x170077A9 RID: 30633
		// (get) Token: 0x0601677A RID: 92026
		public virtual extern object IHTMLElement_all
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x170077AA RID: 30634
		// (get) Token: 0x0601677B RID: 92027
		public virtual extern string IHTMLElement2_scopeName
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x0601677C RID: 92028
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_setCapture([In] bool containerCapture = true);

		// Token: 0x0601677D RID: 92029
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_releaseCapture();

		// Token: 0x170077AB RID: 30635
		// (get) Token: 0x0601677F RID: 92031
		// (set) Token: 0x0601677E RID: 92030
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

		// Token: 0x06016780 RID: 92032
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string IHTMLElement2_componentFromPoint([In] int x, [In] int y);

		// Token: 0x06016781 RID: 92033
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_doScroll([MarshalAs(27)] [In] [Optional] object component);

		// Token: 0x170077AC RID: 30636
		// (get) Token: 0x06016783 RID: 92035
		// (set) Token: 0x06016782 RID: 92034
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

		// Token: 0x170077AD RID: 30637
		// (get) Token: 0x06016785 RID: 92037
		// (set) Token: 0x06016784 RID: 92036
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

		// Token: 0x170077AE RID: 30638
		// (get) Token: 0x06016787 RID: 92039
		// (set) Token: 0x06016786 RID: 92038
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

		// Token: 0x170077AF RID: 30639
		// (get) Token: 0x06016789 RID: 92041
		// (set) Token: 0x06016788 RID: 92040
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

		// Token: 0x170077B0 RID: 30640
		// (get) Token: 0x0601678B RID: 92043
		// (set) Token: 0x0601678A RID: 92042
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

		// Token: 0x170077B1 RID: 30641
		// (get) Token: 0x0601678D RID: 92045
		// (set) Token: 0x0601678C RID: 92044
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

		// Token: 0x170077B2 RID: 30642
		// (get) Token: 0x0601678F RID: 92047
		// (set) Token: 0x0601678E RID: 92046
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

		// Token: 0x170077B3 RID: 30643
		// (get) Token: 0x06016791 RID: 92049
		// (set) Token: 0x06016790 RID: 92048
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

		// Token: 0x170077B4 RID: 30644
		// (get) Token: 0x06016793 RID: 92051
		// (set) Token: 0x06016792 RID: 92050
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

		// Token: 0x170077B5 RID: 30645
		// (get) Token: 0x06016795 RID: 92053
		// (set) Token: 0x06016794 RID: 92052
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

		// Token: 0x170077B6 RID: 30646
		// (get) Token: 0x06016797 RID: 92055
		// (set) Token: 0x06016796 RID: 92054
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

		// Token: 0x170077B7 RID: 30647
		// (get) Token: 0x06016799 RID: 92057
		// (set) Token: 0x06016798 RID: 92056
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

		// Token: 0x170077B8 RID: 30648
		// (get) Token: 0x0601679B RID: 92059
		// (set) Token: 0x0601679A RID: 92058
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

		// Token: 0x170077B9 RID: 30649
		// (get) Token: 0x0601679C RID: 92060
		public virtual extern IHTMLCurrentStyle IHTMLElement2_currentStyle
		{
			[TypeLibFunc(1024)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170077BA RID: 30650
		// (get) Token: 0x0601679E RID: 92062
		// (set) Token: 0x0601679D RID: 92061
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

		// Token: 0x0601679F RID: 92063
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLRectCollection IHTMLElement2_getClientRects();

		// Token: 0x060167A0 RID: 92064
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLRect IHTMLElement2_getBoundingClientRect();

		// Token: 0x060167A1 RID: 92065
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_setExpression([MarshalAs(19)] [In] string propname, [MarshalAs(19)] [In] string expression, [MarshalAs(19)] [In] string language = "");

		// Token: 0x060167A2 RID: 92066
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		public virtual extern object IHTMLElement2_getExpression([MarshalAs(19)] [In] string propname);

		// Token: 0x060167A3 RID: 92067
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement2_removeExpression([MarshalAs(19)] [In] string propname);

		// Token: 0x170077BB RID: 30651
		// (get) Token: 0x060167A5 RID: 92069
		// (set) Token: 0x060167A4 RID: 92068
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

		// Token: 0x060167A6 RID: 92070
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_focus();

		// Token: 0x170077BC RID: 30652
		// (get) Token: 0x060167A8 RID: 92072
		// (set) Token: 0x060167A7 RID: 92071
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

		// Token: 0x170077BD RID: 30653
		// (get) Token: 0x060167AA RID: 92074
		// (set) Token: 0x060167A9 RID: 92073
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

		// Token: 0x170077BE RID: 30654
		// (get) Token: 0x060167AC RID: 92076
		// (set) Token: 0x060167AB RID: 92075
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

		// Token: 0x170077BF RID: 30655
		// (get) Token: 0x060167AE RID: 92078
		// (set) Token: 0x060167AD RID: 92077
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

		// Token: 0x060167AF RID: 92079
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_blur();

		// Token: 0x060167B0 RID: 92080
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_addFilter([MarshalAs(25)] [In] object pUnk);

		// Token: 0x060167B1 RID: 92081
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_removeFilter([MarshalAs(25)] [In] object pUnk);

		// Token: 0x170077C0 RID: 30656
		// (get) Token: 0x060167B2 RID: 92082
		public virtual extern int IHTMLElement2_clientHeight
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170077C1 RID: 30657
		// (get) Token: 0x060167B3 RID: 92083
		public virtual extern int IHTMLElement2_clientWidth
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170077C2 RID: 30658
		// (get) Token: 0x060167B4 RID: 92084
		public virtual extern int IHTMLElement2_clientTop
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170077C3 RID: 30659
		// (get) Token: 0x060167B5 RID: 92085
		public virtual extern int IHTMLElement2_clientLeft
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x060167B6 RID: 92086
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement2_attachEvent([MarshalAs(19)] [In] string @event, [MarshalAs(26)] [In] object pdisp);

		// Token: 0x060167B7 RID: 92087
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_detachEvent([MarshalAs(19)] [In] string @event, [MarshalAs(26)] [In] object pdisp);

		// Token: 0x170077C4 RID: 30660
		// (get) Token: 0x060167B8 RID: 92088
		public virtual extern object IHTMLElement2_readyState
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x170077C5 RID: 30661
		// (get) Token: 0x060167BA RID: 92090
		// (set) Token: 0x060167B9 RID: 92089
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

		// Token: 0x170077C6 RID: 30662
		// (get) Token: 0x060167BC RID: 92092
		// (set) Token: 0x060167BB RID: 92091
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

		// Token: 0x170077C7 RID: 30663
		// (get) Token: 0x060167BE RID: 92094
		// (set) Token: 0x060167BD RID: 92093
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

		// Token: 0x170077C8 RID: 30664
		// (get) Token: 0x060167C0 RID: 92096
		// (set) Token: 0x060167BF RID: 92095
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

		// Token: 0x170077C9 RID: 30665
		// (get) Token: 0x060167C2 RID: 92098
		// (set) Token: 0x060167C1 RID: 92097
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

		// Token: 0x060167C3 RID: 92099
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(26)]
		public virtual extern object IHTMLElement2_createControlRange();

		// Token: 0x170077CA RID: 30666
		// (get) Token: 0x060167C4 RID: 92100
		public virtual extern int IHTMLElement2_scrollHeight
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170077CB RID: 30667
		// (get) Token: 0x060167C5 RID: 92101
		public virtual extern int IHTMLElement2_scrollWidth
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170077CC RID: 30668
		// (get) Token: 0x060167C7 RID: 92103
		// (set) Token: 0x060167C6 RID: 92102
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

		// Token: 0x170077CD RID: 30669
		// (get) Token: 0x060167C9 RID: 92105
		// (set) Token: 0x060167C8 RID: 92104
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

		// Token: 0x060167CA RID: 92106
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_clearAttributes();

		// Token: 0x060167CB RID: 92107
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void mergeAttributes([MarshalAs(28)] [In] IHTMLElement mergeThis);

		// Token: 0x170077CE RID: 30670
		// (get) Token: 0x060167CD RID: 92109
		// (set) Token: 0x060167CC RID: 92108
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

		// Token: 0x060167CE RID: 92110
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElement IHTMLElement2_insertAdjacentElement([MarshalAs(19)] [In] string where, [MarshalAs(28)] [In] IHTMLElement insertedElement);

		// Token: 0x060167CF RID: 92111
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElement IHTMLElement2_applyElement([MarshalAs(28)] [In] IHTMLElement apply, [MarshalAs(19)] [In] string where);

		// Token: 0x060167D0 RID: 92112
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string IHTMLElement2_getAdjacentText([MarshalAs(19)] [In] string where);

		// Token: 0x060167D1 RID: 92113
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string IHTMLElement2_replaceAdjacentText([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string newText);

		// Token: 0x170077CF RID: 30671
		// (get) Token: 0x060167D2 RID: 92114
		public virtual extern bool IHTMLElement2_canHaveChildren
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x060167D3 RID: 92115
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern int IHTMLElement2_addBehavior([MarshalAs(19)] [In] string bstrUrl, [MarshalAs(27)] [In] [Optional] ref object pvarFactory);

		// Token: 0x060167D4 RID: 92116
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement2_removeBehavior([In] int cookie);

		// Token: 0x170077D0 RID: 30672
		// (get) Token: 0x060167D5 RID: 92117
		public virtual extern IHTMLStyle IHTMLElement2_runtimeStyle
		{
			[TypeLibFunc(1024)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170077D1 RID: 30673
		// (get) Token: 0x060167D6 RID: 92118
		public virtual extern object IHTMLElement2_behaviorUrns
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x170077D2 RID: 30674
		// (get) Token: 0x060167D8 RID: 92120
		// (set) Token: 0x060167D7 RID: 92119
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

		// Token: 0x170077D3 RID: 30675
		// (get) Token: 0x060167DA RID: 92122
		// (set) Token: 0x060167D9 RID: 92121
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

		// Token: 0x170077D4 RID: 30676
		// (get) Token: 0x060167DB RID: 92123
		public virtual extern int IHTMLElement2_readyStateValue
		{
			[TypeLibFunc(65)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x060167DC RID: 92124
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElementCollection IHTMLElement2_getElementsByTagName([MarshalAs(19)] [In] string v);

		// Token: 0x14002B2E RID: 11054
		// (add) Token: 0x060167DD RID: 92125
		// (remove) Token: 0x060167DE RID: 92126
		public virtual extern event HTMLFormElementEvents_onhelpEventHandler HTMLFormElementEvents_Event_onhelp;

		// Token: 0x14002B2F RID: 11055
		// (add) Token: 0x060167DF RID: 92127
		// (remove) Token: 0x060167E0 RID: 92128
		public virtual extern event HTMLFormElementEvents_onclickEventHandler HTMLFormElementEvents_Event_onclick;

		// Token: 0x14002B30 RID: 11056
		// (add) Token: 0x060167E1 RID: 92129
		// (remove) Token: 0x060167E2 RID: 92130
		public virtual extern event HTMLFormElementEvents_ondblclickEventHandler HTMLFormElementEvents_Event_ondblclick;

		// Token: 0x14002B31 RID: 11057
		// (add) Token: 0x060167E3 RID: 92131
		// (remove) Token: 0x060167E4 RID: 92132
		public virtual extern event HTMLFormElementEvents_onkeypressEventHandler HTMLFormElementEvents_Event_onkeypress;

		// Token: 0x14002B32 RID: 11058
		// (add) Token: 0x060167E5 RID: 92133
		// (remove) Token: 0x060167E6 RID: 92134
		public virtual extern event HTMLFormElementEvents_onkeydownEventHandler HTMLFormElementEvents_Event_onkeydown;

		// Token: 0x14002B33 RID: 11059
		// (add) Token: 0x060167E7 RID: 92135
		// (remove) Token: 0x060167E8 RID: 92136
		public virtual extern event HTMLFormElementEvents_onkeyupEventHandler HTMLFormElementEvents_Event_onkeyup;

		// Token: 0x14002B34 RID: 11060
		// (add) Token: 0x060167E9 RID: 92137
		// (remove) Token: 0x060167EA RID: 92138
		public virtual extern event HTMLFormElementEvents_onmouseoutEventHandler HTMLFormElementEvents_Event_onmouseout;

		// Token: 0x14002B35 RID: 11061
		// (add) Token: 0x060167EB RID: 92139
		// (remove) Token: 0x060167EC RID: 92140
		public virtual extern event HTMLFormElementEvents_onmouseoverEventHandler HTMLFormElementEvents_Event_onmouseover;

		// Token: 0x14002B36 RID: 11062
		// (add) Token: 0x060167ED RID: 92141
		// (remove) Token: 0x060167EE RID: 92142
		public virtual extern event HTMLFormElementEvents_onmousemoveEventHandler HTMLFormElementEvents_Event_onmousemove;

		// Token: 0x14002B37 RID: 11063
		// (add) Token: 0x060167EF RID: 92143
		// (remove) Token: 0x060167F0 RID: 92144
		public virtual extern event HTMLFormElementEvents_onmousedownEventHandler HTMLFormElementEvents_Event_onmousedown;

		// Token: 0x14002B38 RID: 11064
		// (add) Token: 0x060167F1 RID: 92145
		// (remove) Token: 0x060167F2 RID: 92146
		public virtual extern event HTMLFormElementEvents_onmouseupEventHandler HTMLFormElementEvents_Event_onmouseup;

		// Token: 0x14002B39 RID: 11065
		// (add) Token: 0x060167F3 RID: 92147
		// (remove) Token: 0x060167F4 RID: 92148
		public virtual extern event HTMLFormElementEvents_onselectstartEventHandler HTMLFormElementEvents_Event_onselectstart;

		// Token: 0x14002B3A RID: 11066
		// (add) Token: 0x060167F5 RID: 92149
		// (remove) Token: 0x060167F6 RID: 92150
		public virtual extern event HTMLFormElementEvents_onfilterchangeEventHandler HTMLFormElementEvents_Event_onfilterchange;

		// Token: 0x14002B3B RID: 11067
		// (add) Token: 0x060167F7 RID: 92151
		// (remove) Token: 0x060167F8 RID: 92152
		public virtual extern event HTMLFormElementEvents_ondragstartEventHandler HTMLFormElementEvents_Event_ondragstart;

		// Token: 0x14002B3C RID: 11068
		// (add) Token: 0x060167F9 RID: 92153
		// (remove) Token: 0x060167FA RID: 92154
		public virtual extern event HTMLFormElementEvents_onbeforeupdateEventHandler HTMLFormElementEvents_Event_onbeforeupdate;

		// Token: 0x14002B3D RID: 11069
		// (add) Token: 0x060167FB RID: 92155
		// (remove) Token: 0x060167FC RID: 92156
		public virtual extern event HTMLFormElementEvents_onafterupdateEventHandler HTMLFormElementEvents_Event_onafterupdate;

		// Token: 0x14002B3E RID: 11070
		// (add) Token: 0x060167FD RID: 92157
		// (remove) Token: 0x060167FE RID: 92158
		public virtual extern event HTMLFormElementEvents_onerrorupdateEventHandler HTMLFormElementEvents_Event_onerrorupdate;

		// Token: 0x14002B3F RID: 11071
		// (add) Token: 0x060167FF RID: 92159
		// (remove) Token: 0x06016800 RID: 92160
		public virtual extern event HTMLFormElementEvents_onrowexitEventHandler HTMLFormElementEvents_Event_onrowexit;

		// Token: 0x14002B40 RID: 11072
		// (add) Token: 0x06016801 RID: 92161
		// (remove) Token: 0x06016802 RID: 92162
		public virtual extern event HTMLFormElementEvents_onrowenterEventHandler HTMLFormElementEvents_Event_onrowenter;

		// Token: 0x14002B41 RID: 11073
		// (add) Token: 0x06016803 RID: 92163
		// (remove) Token: 0x06016804 RID: 92164
		public virtual extern event HTMLFormElementEvents_ondatasetchangedEventHandler HTMLFormElementEvents_Event_ondatasetchanged;

		// Token: 0x14002B42 RID: 11074
		// (add) Token: 0x06016805 RID: 92165
		// (remove) Token: 0x06016806 RID: 92166
		public virtual extern event HTMLFormElementEvents_ondataavailableEventHandler HTMLFormElementEvents_Event_ondataavailable;

		// Token: 0x14002B43 RID: 11075
		// (add) Token: 0x06016807 RID: 92167
		// (remove) Token: 0x06016808 RID: 92168
		public virtual extern event HTMLFormElementEvents_ondatasetcompleteEventHandler HTMLFormElementEvents_Event_ondatasetcomplete;

		// Token: 0x14002B44 RID: 11076
		// (add) Token: 0x06016809 RID: 92169
		// (remove) Token: 0x0601680A RID: 92170
		public virtual extern event HTMLFormElementEvents_onlosecaptureEventHandler HTMLFormElementEvents_Event_onlosecapture;

		// Token: 0x14002B45 RID: 11077
		// (add) Token: 0x0601680B RID: 92171
		// (remove) Token: 0x0601680C RID: 92172
		public virtual extern event HTMLFormElementEvents_onpropertychangeEventHandler HTMLFormElementEvents_Event_onpropertychange;

		// Token: 0x14002B46 RID: 11078
		// (add) Token: 0x0601680D RID: 92173
		// (remove) Token: 0x0601680E RID: 92174
		public virtual extern event HTMLFormElementEvents_onscrollEventHandler HTMLFormElementEvents_Event_onscroll;

		// Token: 0x14002B47 RID: 11079
		// (add) Token: 0x0601680F RID: 92175
		// (remove) Token: 0x06016810 RID: 92176
		public virtual extern event HTMLFormElementEvents_onfocusEventHandler HTMLFormElementEvents_Event_onfocus;

		// Token: 0x14002B48 RID: 11080
		// (add) Token: 0x06016811 RID: 92177
		// (remove) Token: 0x06016812 RID: 92178
		public virtual extern event HTMLFormElementEvents_onblurEventHandler HTMLFormElementEvents_Event_onblur;

		// Token: 0x14002B49 RID: 11081
		// (add) Token: 0x06016813 RID: 92179
		// (remove) Token: 0x06016814 RID: 92180
		public virtual extern event HTMLFormElementEvents_onresizeEventHandler HTMLFormElementEvents_Event_onresize;

		// Token: 0x14002B4A RID: 11082
		// (add) Token: 0x06016815 RID: 92181
		// (remove) Token: 0x06016816 RID: 92182
		public virtual extern event HTMLFormElementEvents_ondragEventHandler HTMLFormElementEvents_Event_ondrag;

		// Token: 0x14002B4B RID: 11083
		// (add) Token: 0x06016817 RID: 92183
		// (remove) Token: 0x06016818 RID: 92184
		public virtual extern event HTMLFormElementEvents_ondragendEventHandler HTMLFormElementEvents_Event_ondragend;

		// Token: 0x14002B4C RID: 11084
		// (add) Token: 0x06016819 RID: 92185
		// (remove) Token: 0x0601681A RID: 92186
		public virtual extern event HTMLFormElementEvents_ondragenterEventHandler HTMLFormElementEvents_Event_ondragenter;

		// Token: 0x14002B4D RID: 11085
		// (add) Token: 0x0601681B RID: 92187
		// (remove) Token: 0x0601681C RID: 92188
		public virtual extern event HTMLFormElementEvents_ondragoverEventHandler HTMLFormElementEvents_Event_ondragover;

		// Token: 0x14002B4E RID: 11086
		// (add) Token: 0x0601681D RID: 92189
		// (remove) Token: 0x0601681E RID: 92190
		public virtual extern event HTMLFormElementEvents_ondragleaveEventHandler HTMLFormElementEvents_Event_ondragleave;

		// Token: 0x14002B4F RID: 11087
		// (add) Token: 0x0601681F RID: 92191
		// (remove) Token: 0x06016820 RID: 92192
		public virtual extern event HTMLFormElementEvents_ondropEventHandler HTMLFormElementEvents_Event_ondrop;

		// Token: 0x14002B50 RID: 11088
		// (add) Token: 0x06016821 RID: 92193
		// (remove) Token: 0x06016822 RID: 92194
		public virtual extern event HTMLFormElementEvents_onbeforecutEventHandler HTMLFormElementEvents_Event_onbeforecut;

		// Token: 0x14002B51 RID: 11089
		// (add) Token: 0x06016823 RID: 92195
		// (remove) Token: 0x06016824 RID: 92196
		public virtual extern event HTMLFormElementEvents_oncutEventHandler HTMLFormElementEvents_Event_oncut;

		// Token: 0x14002B52 RID: 11090
		// (add) Token: 0x06016825 RID: 92197
		// (remove) Token: 0x06016826 RID: 92198
		public virtual extern event HTMLFormElementEvents_onbeforecopyEventHandler HTMLFormElementEvents_Event_onbeforecopy;

		// Token: 0x14002B53 RID: 11091
		// (add) Token: 0x06016827 RID: 92199
		// (remove) Token: 0x06016828 RID: 92200
		public virtual extern event HTMLFormElementEvents_oncopyEventHandler HTMLFormElementEvents_Event_oncopy;

		// Token: 0x14002B54 RID: 11092
		// (add) Token: 0x06016829 RID: 92201
		// (remove) Token: 0x0601682A RID: 92202
		public virtual extern event HTMLFormElementEvents_onbeforepasteEventHandler HTMLFormElementEvents_Event_onbeforepaste;

		// Token: 0x14002B55 RID: 11093
		// (add) Token: 0x0601682B RID: 92203
		// (remove) Token: 0x0601682C RID: 92204
		public virtual extern event HTMLFormElementEvents_onpasteEventHandler HTMLFormElementEvents_Event_onpaste;

		// Token: 0x14002B56 RID: 11094
		// (add) Token: 0x0601682D RID: 92205
		// (remove) Token: 0x0601682E RID: 92206
		public virtual extern event HTMLFormElementEvents_oncontextmenuEventHandler HTMLFormElementEvents_Event_oncontextmenu;

		// Token: 0x14002B57 RID: 11095
		// (add) Token: 0x0601682F RID: 92207
		// (remove) Token: 0x06016830 RID: 92208
		public virtual extern event HTMLFormElementEvents_onrowsdeleteEventHandler HTMLFormElementEvents_Event_onrowsdelete;

		// Token: 0x14002B58 RID: 11096
		// (add) Token: 0x06016831 RID: 92209
		// (remove) Token: 0x06016832 RID: 92210
		public virtual extern event HTMLFormElementEvents_onrowsinsertedEventHandler HTMLFormElementEvents_Event_onrowsinserted;

		// Token: 0x14002B59 RID: 11097
		// (add) Token: 0x06016833 RID: 92211
		// (remove) Token: 0x06016834 RID: 92212
		public virtual extern event HTMLFormElementEvents_oncellchangeEventHandler HTMLFormElementEvents_Event_oncellchange;

		// Token: 0x14002B5A RID: 11098
		// (add) Token: 0x06016835 RID: 92213
		// (remove) Token: 0x06016836 RID: 92214
		public virtual extern event HTMLFormElementEvents_onreadystatechangeEventHandler HTMLFormElementEvents_Event_onreadystatechange;

		// Token: 0x14002B5B RID: 11099
		// (add) Token: 0x06016837 RID: 92215
		// (remove) Token: 0x06016838 RID: 92216
		public virtual extern event HTMLFormElementEvents_onbeforeeditfocusEventHandler HTMLFormElementEvents_Event_onbeforeeditfocus;

		// Token: 0x14002B5C RID: 11100
		// (add) Token: 0x06016839 RID: 92217
		// (remove) Token: 0x0601683A RID: 92218
		public virtual extern event HTMLFormElementEvents_onlayoutcompleteEventHandler HTMLFormElementEvents_Event_onlayoutcomplete;

		// Token: 0x14002B5D RID: 11101
		// (add) Token: 0x0601683B RID: 92219
		// (remove) Token: 0x0601683C RID: 92220
		public virtual extern event HTMLFormElementEvents_onpageEventHandler HTMLFormElementEvents_Event_onpage;

		// Token: 0x14002B5E RID: 11102
		// (add) Token: 0x0601683D RID: 92221
		// (remove) Token: 0x0601683E RID: 92222
		public virtual extern event HTMLFormElementEvents_onbeforedeactivateEventHandler HTMLFormElementEvents_Event_onbeforedeactivate;

		// Token: 0x14002B5F RID: 11103
		// (add) Token: 0x0601683F RID: 92223
		// (remove) Token: 0x06016840 RID: 92224
		public virtual extern event HTMLFormElementEvents_onbeforeactivateEventHandler HTMLFormElementEvents_Event_onbeforeactivate;

		// Token: 0x14002B60 RID: 11104
		// (add) Token: 0x06016841 RID: 92225
		// (remove) Token: 0x06016842 RID: 92226
		public virtual extern event HTMLFormElementEvents_onmoveEventHandler HTMLFormElementEvents_Event_onmove;

		// Token: 0x14002B61 RID: 11105
		// (add) Token: 0x06016843 RID: 92227
		// (remove) Token: 0x06016844 RID: 92228
		public virtual extern event HTMLFormElementEvents_oncontrolselectEventHandler HTMLFormElementEvents_Event_oncontrolselect;

		// Token: 0x14002B62 RID: 11106
		// (add) Token: 0x06016845 RID: 92229
		// (remove) Token: 0x06016846 RID: 92230
		public virtual extern event HTMLFormElementEvents_onmovestartEventHandler HTMLFormElementEvents_Event_onmovestart;

		// Token: 0x14002B63 RID: 11107
		// (add) Token: 0x06016847 RID: 92231
		// (remove) Token: 0x06016848 RID: 92232
		public virtual extern event HTMLFormElementEvents_onmoveendEventHandler HTMLFormElementEvents_Event_onmoveend;

		// Token: 0x14002B64 RID: 11108
		// (add) Token: 0x06016849 RID: 92233
		// (remove) Token: 0x0601684A RID: 92234
		public virtual extern event HTMLFormElementEvents_onresizestartEventHandler HTMLFormElementEvents_Event_onresizestart;

		// Token: 0x14002B65 RID: 11109
		// (add) Token: 0x0601684B RID: 92235
		// (remove) Token: 0x0601684C RID: 92236
		public virtual extern event HTMLFormElementEvents_onresizeendEventHandler HTMLFormElementEvents_Event_onresizeend;

		// Token: 0x14002B66 RID: 11110
		// (add) Token: 0x0601684D RID: 92237
		// (remove) Token: 0x0601684E RID: 92238
		public virtual extern event HTMLFormElementEvents_onmouseenterEventHandler HTMLFormElementEvents_Event_onmouseenter;

		// Token: 0x14002B67 RID: 11111
		// (add) Token: 0x0601684F RID: 92239
		// (remove) Token: 0x06016850 RID: 92240
		public virtual extern event HTMLFormElementEvents_onmouseleaveEventHandler HTMLFormElementEvents_Event_onmouseleave;

		// Token: 0x14002B68 RID: 11112
		// (add) Token: 0x06016851 RID: 92241
		// (remove) Token: 0x06016852 RID: 92242
		public virtual extern event HTMLFormElementEvents_onmousewheelEventHandler HTMLFormElementEvents_Event_onmousewheel;

		// Token: 0x14002B69 RID: 11113
		// (add) Token: 0x06016853 RID: 92243
		// (remove) Token: 0x06016854 RID: 92244
		public virtual extern event HTMLFormElementEvents_onactivateEventHandler HTMLFormElementEvents_Event_onactivate;

		// Token: 0x14002B6A RID: 11114
		// (add) Token: 0x06016855 RID: 92245
		// (remove) Token: 0x06016856 RID: 92246
		public virtual extern event HTMLFormElementEvents_ondeactivateEventHandler HTMLFormElementEvents_Event_ondeactivate;

		// Token: 0x14002B6B RID: 11115
		// (add) Token: 0x06016857 RID: 92247
		// (remove) Token: 0x06016858 RID: 92248
		public virtual extern event HTMLFormElementEvents_onfocusinEventHandler HTMLFormElementEvents_Event_onfocusin;

		// Token: 0x14002B6C RID: 11116
		// (add) Token: 0x06016859 RID: 92249
		// (remove) Token: 0x0601685A RID: 92250
		public virtual extern event HTMLFormElementEvents_onfocusoutEventHandler HTMLFormElementEvents_Event_onfocusout;

		// Token: 0x14002B6D RID: 11117
		// (add) Token: 0x0601685B RID: 92251
		// (remove) Token: 0x0601685C RID: 92252
		public virtual extern event HTMLFormElementEvents_onsubmitEventHandler HTMLFormElementEvents_Event_onsubmit;

		// Token: 0x14002B6E RID: 11118
		// (add) Token: 0x0601685D RID: 92253
		// (remove) Token: 0x0601685E RID: 92254
		public virtual extern event HTMLFormElementEvents_onresetEventHandler HTMLFormElementEvents_Event_onreset;
	}
}
