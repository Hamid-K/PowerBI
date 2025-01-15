using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x0200027F RID: 639
	[TypeLibType(2)]
	[ClassInterface(0)]
	[ComSourceInterfaces("mshtml.HTMLImgEvents\0mshtml.HTMLImgEvents2\0\0")]
	[Guid("3050F241-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public class HTMLImgClass : DispHTMLImg, HTMLImg, HTMLImgEvents_Event, IHTMLElement, IHTMLElement2, IHTMLElement3, IHTMLElement4, IHTMLUniqueName, IHTMLDOMNode, IHTMLDOMNode2, IHTMLDatabinding, IHTMLControlElement, IHTMLImgElement, IHTMLImgElement2, HTMLImgEvents2_Event
	{
		// Token: 0x06002990 RID: 10640
		[MethodImpl(4096, MethodCodeType = 3)]
		public extern HTMLImgClass();

		// Token: 0x06002991 RID: 10641
		[DispId(-2147417611)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void setAttribute([MarshalAs(19)] [In] string strAttributeName, [MarshalAs(27)] [In] object AttributeValue, [In] int lFlags = 1);

		// Token: 0x06002992 RID: 10642
		[DispId(-2147417610)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		public virtual extern object getAttribute([MarshalAs(19)] [In] string strAttributeName, [In] int lFlags = 0);

		// Token: 0x06002993 RID: 10643
		[DispId(-2147417609)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool removeAttribute([MarshalAs(19)] [In] string strAttributeName, [In] int lFlags = 1);

		// Token: 0x17000ED5 RID: 3797
		// (get) Token: 0x06002995 RID: 10645
		// (set) Token: 0x06002994 RID: 10644
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

		// Token: 0x17000ED6 RID: 3798
		// (get) Token: 0x06002997 RID: 10647
		// (set) Token: 0x06002996 RID: 10646
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

		// Token: 0x17000ED7 RID: 3799
		// (get) Token: 0x06002998 RID: 10648
		[DispId(-2147417108)]
		public virtual extern string tagName
		{
			[DispId(-2147417108)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17000ED8 RID: 3800
		// (get) Token: 0x06002999 RID: 10649
		[DispId(-2147418104)]
		public virtual extern IHTMLElement parentElement
		{
			[DispId(-2147418104)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17000ED9 RID: 3801
		// (get) Token: 0x0600299A RID: 10650
		[DispId(-2147418038)]
		public virtual extern IHTMLStyle style
		{
			[DispId(-2147418038)]
			[TypeLibFunc(1024)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17000EDA RID: 3802
		// (get) Token: 0x0600299C RID: 10652
		// (set) Token: 0x0600299B RID: 10651
		[DispId(-2147412099)]
		public virtual extern object onhelp
		{
			[DispId(-2147412099)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412099)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17000EDB RID: 3803
		// (get) Token: 0x0600299E RID: 10654
		// (set) Token: 0x0600299D RID: 10653
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

		// Token: 0x17000EDC RID: 3804
		// (get) Token: 0x060029A0 RID: 10656
		// (set) Token: 0x0600299F RID: 10655
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

		// Token: 0x17000EDD RID: 3805
		// (get) Token: 0x060029A2 RID: 10658
		// (set) Token: 0x060029A1 RID: 10657
		[DispId(-2147412107)]
		public virtual extern object onkeydown
		{
			[DispId(-2147412107)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412107)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17000EDE RID: 3806
		// (get) Token: 0x060029A4 RID: 10660
		// (set) Token: 0x060029A3 RID: 10659
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

		// Token: 0x17000EDF RID: 3807
		// (get) Token: 0x060029A6 RID: 10662
		// (set) Token: 0x060029A5 RID: 10661
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

		// Token: 0x17000EE0 RID: 3808
		// (get) Token: 0x060029A8 RID: 10664
		// (set) Token: 0x060029A7 RID: 10663
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

		// Token: 0x17000EE1 RID: 3809
		// (get) Token: 0x060029AA RID: 10666
		// (set) Token: 0x060029A9 RID: 10665
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

		// Token: 0x17000EE2 RID: 3810
		// (get) Token: 0x060029AC RID: 10668
		// (set) Token: 0x060029AB RID: 10667
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

		// Token: 0x17000EE3 RID: 3811
		// (get) Token: 0x060029AE RID: 10670
		// (set) Token: 0x060029AD RID: 10669
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

		// Token: 0x17000EE4 RID: 3812
		// (get) Token: 0x060029B0 RID: 10672
		// (set) Token: 0x060029AF RID: 10671
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

		// Token: 0x17000EE5 RID: 3813
		// (get) Token: 0x060029B1 RID: 10673
		[DispId(-2147417094)]
		public virtual extern object document
		{
			[DispId(-2147417094)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17000EE6 RID: 3814
		// (get) Token: 0x060029B3 RID: 10675
		// (set) Token: 0x060029B2 RID: 10674
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

		// Token: 0x17000EE7 RID: 3815
		// (get) Token: 0x060029B5 RID: 10677
		// (set) Token: 0x060029B4 RID: 10676
		[DispId(-2147413012)]
		public virtual extern string language
		{
			[DispId(-2147413012)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147413012)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17000EE8 RID: 3816
		// (get) Token: 0x060029B7 RID: 10679
		// (set) Token: 0x060029B6 RID: 10678
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

		// Token: 0x060029B8 RID: 10680
		[DispId(-2147417093)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void scrollIntoView([MarshalAs(27)] [In] [Optional] object varargStart);

		// Token: 0x060029B9 RID: 10681
		[DispId(-2147417092)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool contains([MarshalAs(28)] [In] IHTMLElement pChild);

		// Token: 0x17000EE9 RID: 3817
		// (get) Token: 0x060029BA RID: 10682
		[DispId(-2147417088)]
		public virtual extern int sourceIndex
		{
			[DispId(-2147417088)]
			[TypeLibFunc(4)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17000EEA RID: 3818
		// (get) Token: 0x060029BB RID: 10683
		[DispId(-2147417087)]
		public virtual extern object recordNumber
		{
			[DispId(-2147417087)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x17000EEB RID: 3819
		// (get) Token: 0x060029BD RID: 10685
		// (set) Token: 0x060029BC RID: 10684
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

		// Token: 0x17000EEC RID: 3820
		// (get) Token: 0x060029BE RID: 10686
		[DispId(-2147417104)]
		public virtual extern int offsetLeft
		{
			[DispId(-2147417104)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17000EED RID: 3821
		// (get) Token: 0x060029BF RID: 10687
		[DispId(-2147417103)]
		public virtual extern int offsetTop
		{
			[DispId(-2147417103)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17000EEE RID: 3822
		// (get) Token: 0x060029C0 RID: 10688
		[DispId(-2147417102)]
		public virtual extern int offsetWidth
		{
			[DispId(-2147417102)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17000EEF RID: 3823
		// (get) Token: 0x060029C1 RID: 10689
		[DispId(-2147417101)]
		public virtual extern int offsetHeight
		{
			[DispId(-2147417101)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17000EF0 RID: 3824
		// (get) Token: 0x060029C2 RID: 10690
		[DispId(-2147417100)]
		public virtual extern IHTMLElement offsetParent
		{
			[DispId(-2147417100)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17000EF1 RID: 3825
		// (get) Token: 0x060029C4 RID: 10692
		// (set) Token: 0x060029C3 RID: 10691
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

		// Token: 0x17000EF2 RID: 3826
		// (get) Token: 0x060029C6 RID: 10694
		// (set) Token: 0x060029C5 RID: 10693
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

		// Token: 0x17000EF3 RID: 3827
		// (get) Token: 0x060029C8 RID: 10696
		// (set) Token: 0x060029C7 RID: 10695
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

		// Token: 0x17000EF4 RID: 3828
		// (get) Token: 0x060029CA RID: 10698
		// (set) Token: 0x060029C9 RID: 10697
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

		// Token: 0x060029CB RID: 10699
		[DispId(-2147417082)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void insertAdjacentHTML([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string html);

		// Token: 0x060029CC RID: 10700
		[DispId(-2147417081)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void insertAdjacentText([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string text);

		// Token: 0x17000EF5 RID: 3829
		// (get) Token: 0x060029CD RID: 10701
		[DispId(-2147417080)]
		public virtual extern IHTMLElement parentTextEdit
		{
			[DispId(-2147417080)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17000EF6 RID: 3830
		// (get) Token: 0x060029CE RID: 10702
		[DispId(-2147417078)]
		public virtual extern bool isTextEdit
		{
			[DispId(-2147417078)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x060029CF RID: 10703
		[DispId(-2147417079)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void click();

		// Token: 0x17000EF7 RID: 3831
		// (get) Token: 0x060029D0 RID: 10704
		[DispId(-2147417077)]
		public virtual extern IHTMLFiltersCollection filters
		{
			[DispId(-2147417077)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17000EF8 RID: 3832
		// (get) Token: 0x060029D2 RID: 10706
		// (set) Token: 0x060029D1 RID: 10705
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

		// Token: 0x060029D3 RID: 10707
		[DispId(-2147417076)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string toString();

		// Token: 0x17000EF9 RID: 3833
		// (get) Token: 0x060029D5 RID: 10709
		// (set) Token: 0x060029D4 RID: 10708
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

		// Token: 0x17000EFA RID: 3834
		// (get) Token: 0x060029D7 RID: 10711
		// (set) Token: 0x060029D6 RID: 10710
		[DispId(-2147412090)]
		public virtual extern object onafterupdate
		{
			[TypeLibFunc(20)]
			[DispId(-2147412090)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412090)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17000EFB RID: 3835
		// (get) Token: 0x060029D9 RID: 10713
		// (set) Token: 0x060029D8 RID: 10712
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

		// Token: 0x17000EFC RID: 3836
		// (get) Token: 0x060029DB RID: 10715
		// (set) Token: 0x060029DA RID: 10714
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

		// Token: 0x17000EFD RID: 3837
		// (get) Token: 0x060029DD RID: 10717
		// (set) Token: 0x060029DC RID: 10716
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

		// Token: 0x17000EFE RID: 3838
		// (get) Token: 0x060029DF RID: 10719
		// (set) Token: 0x060029DE RID: 10718
		[DispId(-2147412072)]
		public virtual extern object ondatasetchanged
		{
			[TypeLibFunc(20)]
			[DispId(-2147412072)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412072)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17000EFF RID: 3839
		// (get) Token: 0x060029E1 RID: 10721
		// (set) Token: 0x060029E0 RID: 10720
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

		// Token: 0x17000F00 RID: 3840
		// (get) Token: 0x060029E3 RID: 10723
		// (set) Token: 0x060029E2 RID: 10722
		[DispId(-2147412070)]
		public virtual extern object ondatasetcomplete
		{
			[DispId(-2147412070)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412070)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17000F01 RID: 3841
		// (get) Token: 0x060029E5 RID: 10725
		// (set) Token: 0x060029E4 RID: 10724
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

		// Token: 0x17000F02 RID: 3842
		// (get) Token: 0x060029E6 RID: 10726
		[DispId(-2147417075)]
		public virtual extern object children
		{
			[DispId(-2147417075)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17000F03 RID: 3843
		// (get) Token: 0x060029E7 RID: 10727
		[DispId(-2147417074)]
		public virtual extern object all
		{
			[DispId(-2147417074)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17000F04 RID: 3844
		// (get) Token: 0x060029E8 RID: 10728
		[DispId(-2147417073)]
		public virtual extern string scopeName
		{
			[DispId(-2147417073)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x060029E9 RID: 10729
		[DispId(-2147417072)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void setCapture([In] bool containerCapture = true);

		// Token: 0x060029EA RID: 10730
		[DispId(-2147417071)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void releaseCapture();

		// Token: 0x17000F05 RID: 3845
		// (get) Token: 0x060029EC RID: 10732
		// (set) Token: 0x060029EB RID: 10731
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

		// Token: 0x060029ED RID: 10733
		[DispId(-2147417070)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string componentFromPoint([In] int x, [In] int y);

		// Token: 0x060029EE RID: 10734
		[DispId(-2147417069)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void doScroll([MarshalAs(27)] [In] [Optional] object component);

		// Token: 0x17000F06 RID: 3846
		// (get) Token: 0x060029F0 RID: 10736
		// (set) Token: 0x060029EF RID: 10735
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

		// Token: 0x17000F07 RID: 3847
		// (get) Token: 0x060029F2 RID: 10738
		// (set) Token: 0x060029F1 RID: 10737
		[DispId(-2147412063)]
		public virtual extern object ondrag
		{
			[TypeLibFunc(20)]
			[DispId(-2147412063)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412063)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17000F08 RID: 3848
		// (get) Token: 0x060029F4 RID: 10740
		// (set) Token: 0x060029F3 RID: 10739
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

		// Token: 0x17000F09 RID: 3849
		// (get) Token: 0x060029F6 RID: 10742
		// (set) Token: 0x060029F5 RID: 10741
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

		// Token: 0x17000F0A RID: 3850
		// (get) Token: 0x060029F8 RID: 10744
		// (set) Token: 0x060029F7 RID: 10743
		[DispId(-2147412060)]
		public virtual extern object ondragover
		{
			[DispId(-2147412060)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412060)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17000F0B RID: 3851
		// (get) Token: 0x060029FA RID: 10746
		// (set) Token: 0x060029F9 RID: 10745
		[DispId(-2147412059)]
		public virtual extern object ondragleave
		{
			[DispId(-2147412059)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412059)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17000F0C RID: 3852
		// (get) Token: 0x060029FC RID: 10748
		// (set) Token: 0x060029FB RID: 10747
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

		// Token: 0x17000F0D RID: 3853
		// (get) Token: 0x060029FE RID: 10750
		// (set) Token: 0x060029FD RID: 10749
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

		// Token: 0x17000F0E RID: 3854
		// (get) Token: 0x06002A00 RID: 10752
		// (set) Token: 0x060029FF RID: 10751
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

		// Token: 0x17000F0F RID: 3855
		// (get) Token: 0x06002A02 RID: 10754
		// (set) Token: 0x06002A01 RID: 10753
		[DispId(-2147412053)]
		public virtual extern object onbeforecopy
		{
			[TypeLibFunc(20)]
			[DispId(-2147412053)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412053)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17000F10 RID: 3856
		// (get) Token: 0x06002A04 RID: 10756
		// (set) Token: 0x06002A03 RID: 10755
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

		// Token: 0x17000F11 RID: 3857
		// (get) Token: 0x06002A06 RID: 10758
		// (set) Token: 0x06002A05 RID: 10757
		[DispId(-2147412052)]
		public virtual extern object onbeforepaste
		{
			[TypeLibFunc(20)]
			[DispId(-2147412052)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412052)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17000F12 RID: 3858
		// (get) Token: 0x06002A08 RID: 10760
		// (set) Token: 0x06002A07 RID: 10759
		[DispId(-2147412055)]
		public virtual extern object onpaste
		{
			[DispId(-2147412055)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412055)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17000F13 RID: 3859
		// (get) Token: 0x06002A09 RID: 10761
		[DispId(-2147417105)]
		public virtual extern IHTMLCurrentStyle currentStyle
		{
			[DispId(-2147417105)]
			[TypeLibFunc(1024)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17000F14 RID: 3860
		// (get) Token: 0x06002A0B RID: 10763
		// (set) Token: 0x06002A0A RID: 10762
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

		// Token: 0x06002A0C RID: 10764
		[DispId(-2147417068)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLRectCollection getClientRects();

		// Token: 0x06002A0D RID: 10765
		[DispId(-2147417067)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLRect getBoundingClientRect();

		// Token: 0x06002A0E RID: 10766
		[DispId(-2147417608)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void setExpression([MarshalAs(19)] [In] string propname, [MarshalAs(19)] [In] string expression, [MarshalAs(19)] [In] string language = "");

		// Token: 0x06002A0F RID: 10767
		[DispId(-2147417607)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		public virtual extern object getExpression([MarshalAs(19)] [In] string propname);

		// Token: 0x06002A10 RID: 10768
		[DispId(-2147417606)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool removeExpression([MarshalAs(19)] [In] string propname);

		// Token: 0x17000F15 RID: 3861
		// (get) Token: 0x06002A12 RID: 10770
		// (set) Token: 0x06002A11 RID: 10769
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

		// Token: 0x06002A13 RID: 10771
		[DispId(-2147416112)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void focus();

		// Token: 0x17000F16 RID: 3862
		// (get) Token: 0x06002A15 RID: 10773
		// (set) Token: 0x06002A14 RID: 10772
		[DispId(-2147416107)]
		public virtual extern string accessKey
		{
			[DispId(-2147416107)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147416107)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17000F17 RID: 3863
		// (get) Token: 0x06002A17 RID: 10775
		// (set) Token: 0x06002A16 RID: 10774
		[DispId(-2147412097)]
		public virtual extern object onblur
		{
			[TypeLibFunc(20)]
			[DispId(-2147412097)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412097)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17000F18 RID: 3864
		// (get) Token: 0x06002A19 RID: 10777
		// (set) Token: 0x06002A18 RID: 10776
		[DispId(-2147412098)]
		public virtual extern object onfocus
		{
			[DispId(-2147412098)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412098)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17000F19 RID: 3865
		// (get) Token: 0x06002A1B RID: 10779
		// (set) Token: 0x06002A1A RID: 10778
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

		// Token: 0x06002A1C RID: 10780
		[DispId(-2147416110)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void blur();

		// Token: 0x06002A1D RID: 10781
		[DispId(-2147416095)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void addFilter([MarshalAs(25)] [In] object pUnk);

		// Token: 0x06002A1E RID: 10782
		[DispId(-2147416094)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void removeFilter([MarshalAs(25)] [In] object pUnk);

		// Token: 0x17000F1A RID: 3866
		// (get) Token: 0x06002A1F RID: 10783
		[DispId(-2147416093)]
		public virtual extern int clientHeight
		{
			[DispId(-2147416093)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17000F1B RID: 3867
		// (get) Token: 0x06002A20 RID: 10784
		[DispId(-2147416092)]
		public virtual extern int clientWidth
		{
			[DispId(-2147416092)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17000F1C RID: 3868
		// (get) Token: 0x06002A21 RID: 10785
		[DispId(-2147416091)]
		public virtual extern int clientTop
		{
			[TypeLibFunc(20)]
			[DispId(-2147416091)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17000F1D RID: 3869
		// (get) Token: 0x06002A22 RID: 10786
		[DispId(-2147416090)]
		public virtual extern int clientLeft
		{
			[DispId(-2147416090)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x06002A23 RID: 10787
		[DispId(-2147417605)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool attachEvent([MarshalAs(19)] [In] string @event, [MarshalAs(26)] [In] object pdisp);

		// Token: 0x06002A24 RID: 10788
		[DispId(-2147417604)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void detachEvent([MarshalAs(19)] [In] string @event, [MarshalAs(26)] [In] object pdisp);

		// Token: 0x17000F1E RID: 3870
		// (get) Token: 0x06002A25 RID: 10789
		[DispId(-2147412996)]
		public virtual extern object readyState
		{
			[DispId(-2147412996)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x17000F1F RID: 3871
		// (get) Token: 0x06002A27 RID: 10791
		// (set) Token: 0x06002A26 RID: 10790
		[DispId(-2147412087)]
		public virtual extern object onreadystatechange
		{
			[DispId(-2147412087)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412087)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17000F20 RID: 3872
		// (get) Token: 0x06002A29 RID: 10793
		// (set) Token: 0x06002A28 RID: 10792
		[DispId(-2147412050)]
		public virtual extern object onrowsdelete
		{
			[TypeLibFunc(20)]
			[DispId(-2147412050)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412050)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17000F21 RID: 3873
		// (get) Token: 0x06002A2B RID: 10795
		// (set) Token: 0x06002A2A RID: 10794
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

		// Token: 0x17000F22 RID: 3874
		// (get) Token: 0x06002A2D RID: 10797
		// (set) Token: 0x06002A2C RID: 10796
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

		// Token: 0x17000F23 RID: 3875
		// (get) Token: 0x06002A2F RID: 10799
		// (set) Token: 0x06002A2E RID: 10798
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

		// Token: 0x06002A30 RID: 10800
		[DispId(-2147417056)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(26)]
		public virtual extern object createControlRange();

		// Token: 0x17000F24 RID: 3876
		// (get) Token: 0x06002A31 RID: 10801
		[DispId(-2147417055)]
		public virtual extern int scrollHeight
		{
			[DispId(-2147417055)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17000F25 RID: 3877
		// (get) Token: 0x06002A32 RID: 10802
		[DispId(-2147417054)]
		public virtual extern int scrollWidth
		{
			[DispId(-2147417054)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17000F26 RID: 3878
		// (get) Token: 0x06002A34 RID: 10804
		// (set) Token: 0x06002A33 RID: 10803
		[DispId(-2147417053)]
		public virtual extern int scrollTop
		{
			[TypeLibFunc(20)]
			[DispId(-2147417053)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147417053)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x17000F27 RID: 3879
		// (get) Token: 0x06002A36 RID: 10806
		// (set) Token: 0x06002A35 RID: 10805
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

		// Token: 0x06002A37 RID: 10807
		[DispId(-2147417050)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void clearAttributes();

		// Token: 0x17000F28 RID: 3880
		// (get) Token: 0x06002A39 RID: 10809
		// (set) Token: 0x06002A38 RID: 10808
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

		// Token: 0x06002A3A RID: 10810
		[DispId(-2147417043)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElement insertAdjacentElement([MarshalAs(19)] [In] string where, [MarshalAs(28)] [In] IHTMLElement insertedElement);

		// Token: 0x06002A3B RID: 10811
		[DispId(-2147417047)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElement applyElement([MarshalAs(28)] [In] IHTMLElement apply, [MarshalAs(19)] [In] string where);

		// Token: 0x06002A3C RID: 10812
		[DispId(-2147417042)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string getAdjacentText([MarshalAs(19)] [In] string where);

		// Token: 0x06002A3D RID: 10813
		[DispId(-2147417041)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string replaceAdjacentText([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string newText);

		// Token: 0x17000F29 RID: 3881
		// (get) Token: 0x06002A3E RID: 10814
		[DispId(-2147417040)]
		public virtual extern bool canHaveChildren
		{
			[DispId(-2147417040)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x06002A3F RID: 10815
		[DispId(-2147417032)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern int addBehavior([MarshalAs(19)] [In] string bstrUrl, [MarshalAs(27)] [In] [Optional] ref object pvarFactory);

		// Token: 0x06002A40 RID: 10816
		[DispId(-2147417031)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool removeBehavior([In] int cookie);

		// Token: 0x17000F2A RID: 3882
		// (get) Token: 0x06002A41 RID: 10817
		[DispId(-2147417048)]
		public virtual extern IHTMLStyle runtimeStyle
		{
			[DispId(-2147417048)]
			[TypeLibFunc(1024)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17000F2B RID: 3883
		// (get) Token: 0x06002A42 RID: 10818
		[DispId(-2147417030)]
		public virtual extern object behaviorUrns
		{
			[DispId(-2147417030)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17000F2C RID: 3884
		// (get) Token: 0x06002A44 RID: 10820
		// (set) Token: 0x06002A43 RID: 10819
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

		// Token: 0x17000F2D RID: 3885
		// (get) Token: 0x06002A46 RID: 10822
		// (set) Token: 0x06002A45 RID: 10821
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

		// Token: 0x17000F2E RID: 3886
		// (get) Token: 0x06002A47 RID: 10823
		[DispId(-2147417028)]
		public virtual extern int readyStateValue
		{
			[DispId(-2147417028)]
			[TypeLibFunc(65)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x06002A48 RID: 10824
		[DispId(-2147417027)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElementCollection getElementsByTagName([MarshalAs(19)] [In] string v);

		// Token: 0x06002A49 RID: 10825
		[DispId(-2147417016)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void mergeAttributes([MarshalAs(28)] [In] IHTMLElement mergeThis, [MarshalAs(27)] [In] [Optional] ref object pvarFlags);

		// Token: 0x17000F2F RID: 3887
		// (get) Token: 0x06002A4A RID: 10826
		[DispId(-2147417015)]
		public virtual extern bool isMultiLine
		{
			[DispId(-2147417015)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17000F30 RID: 3888
		// (get) Token: 0x06002A4B RID: 10827
		[DispId(-2147417014)]
		public virtual extern bool canHaveHTML
		{
			[DispId(-2147417014)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17000F31 RID: 3889
		// (get) Token: 0x06002A4D RID: 10829
		// (set) Token: 0x06002A4C RID: 10828
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

		// Token: 0x17000F32 RID: 3890
		// (get) Token: 0x06002A4F RID: 10831
		// (set) Token: 0x06002A4E RID: 10830
		[DispId(-2147412038)]
		public virtual extern object onpage
		{
			[TypeLibFunc(20)]
			[DispId(-2147412038)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412038)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17000F33 RID: 3891
		// (get) Token: 0x06002A51 RID: 10833
		// (set) Token: 0x06002A50 RID: 10832
		[DispId(-2147417012)]
		public virtual extern bool inflateBlock
		{
			[DispId(-2147417012)]
			[TypeLibFunc(1089)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[DispId(-2147417012)]
			[TypeLibFunc(1089)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x17000F34 RID: 3892
		// (get) Token: 0x06002A53 RID: 10835
		// (set) Token: 0x06002A52 RID: 10834
		[DispId(-2147412035)]
		public virtual extern object onbeforedeactivate
		{
			[DispId(-2147412035)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412035)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x06002A54 RID: 10836
		[DispId(-2147417011)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void setActive();

		// Token: 0x17000F35 RID: 3893
		// (get) Token: 0x06002A56 RID: 10838
		// (set) Token: 0x06002A55 RID: 10837
		[DispId(-2147412950)]
		public virtual extern string contentEditable
		{
			[TypeLibFunc(20)]
			[DispId(-2147412950)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147412950)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17000F36 RID: 3894
		// (get) Token: 0x06002A57 RID: 10839
		[DispId(-2147417010)]
		public virtual extern bool isContentEditable
		{
			[DispId(-2147417010)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17000F37 RID: 3895
		// (get) Token: 0x06002A59 RID: 10841
		// (set) Token: 0x06002A58 RID: 10840
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

		// Token: 0x17000F38 RID: 3896
		// (get) Token: 0x06002A5B RID: 10843
		// (set) Token: 0x06002A5A RID: 10842
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

		// Token: 0x17000F39 RID: 3897
		// (get) Token: 0x06002A5C RID: 10844
		[DispId(-2147417007)]
		public virtual extern bool isDisabled
		{
			[DispId(-2147417007)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17000F3A RID: 3898
		// (get) Token: 0x06002A5E RID: 10846
		// (set) Token: 0x06002A5D RID: 10845
		[DispId(-2147412034)]
		public virtual extern object onmove
		{
			[TypeLibFunc(20)]
			[DispId(-2147412034)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412034)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17000F3B RID: 3899
		// (get) Token: 0x06002A60 RID: 10848
		// (set) Token: 0x06002A5F RID: 10847
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

		// Token: 0x06002A61 RID: 10849
		[DispId(-2147417006)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool FireEvent([MarshalAs(19)] [In] string bstrEventName, [MarshalAs(27)] [In] [Optional] ref object pvarEventObject);

		// Token: 0x17000F3C RID: 3900
		// (get) Token: 0x06002A63 RID: 10851
		// (set) Token: 0x06002A62 RID: 10850
		[DispId(-2147412029)]
		public virtual extern object onresizestart
		{
			[TypeLibFunc(20)]
			[DispId(-2147412029)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412029)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17000F3D RID: 3901
		// (get) Token: 0x06002A65 RID: 10853
		// (set) Token: 0x06002A64 RID: 10852
		[DispId(-2147412028)]
		public virtual extern object onresizeend
		{
			[TypeLibFunc(20)]
			[DispId(-2147412028)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412028)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17000F3E RID: 3902
		// (get) Token: 0x06002A67 RID: 10855
		// (set) Token: 0x06002A66 RID: 10854
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

		// Token: 0x17000F3F RID: 3903
		// (get) Token: 0x06002A69 RID: 10857
		// (set) Token: 0x06002A68 RID: 10856
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

		// Token: 0x17000F40 RID: 3904
		// (get) Token: 0x06002A6B RID: 10859
		// (set) Token: 0x06002A6A RID: 10858
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

		// Token: 0x17000F41 RID: 3905
		// (get) Token: 0x06002A6D RID: 10861
		// (set) Token: 0x06002A6C RID: 10860
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

		// Token: 0x17000F42 RID: 3906
		// (get) Token: 0x06002A6F RID: 10863
		// (set) Token: 0x06002A6E RID: 10862
		[DispId(-2147412025)]
		public virtual extern object onactivate
		{
			[DispId(-2147412025)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412025)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17000F43 RID: 3907
		// (get) Token: 0x06002A71 RID: 10865
		// (set) Token: 0x06002A70 RID: 10864
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

		// Token: 0x06002A72 RID: 10866
		[DispId(-2147417005)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool dragDrop();

		// Token: 0x17000F44 RID: 3908
		// (get) Token: 0x06002A73 RID: 10867
		[DispId(-2147417004)]
		public virtual extern int glyphMode
		{
			[TypeLibFunc(1089)]
			[DispId(-2147417004)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17000F45 RID: 3909
		// (get) Token: 0x06002A75 RID: 10869
		// (set) Token: 0x06002A74 RID: 10868
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

		// Token: 0x06002A76 RID: 10870
		[DispId(-2147417000)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void normalize();

		// Token: 0x06002A77 RID: 10871
		[DispId(-2147417003)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute getAttributeNode([MarshalAs(19)] [In] string bstrName);

		// Token: 0x06002A78 RID: 10872
		[DispId(-2147417002)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute setAttributeNode([MarshalAs(28)] [In] IHTMLDOMAttribute pattr);

		// Token: 0x06002A79 RID: 10873
		[DispId(-2147417001)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute removeAttributeNode([MarshalAs(28)] [In] IHTMLDOMAttribute pattr);

		// Token: 0x17000F46 RID: 3910
		// (get) Token: 0x06002A7B RID: 10875
		// (set) Token: 0x06002A7A RID: 10874
		[DispId(-2147412022)]
		public virtual extern object onbeforeactivate
		{
			[TypeLibFunc(20)]
			[DispId(-2147412022)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412022)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17000F47 RID: 3911
		// (get) Token: 0x06002A7D RID: 10877
		// (set) Token: 0x06002A7C RID: 10876
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

		// Token: 0x17000F48 RID: 3912
		// (get) Token: 0x06002A7F RID: 10879
		// (set) Token: 0x06002A7E RID: 10878
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

		// Token: 0x17000F49 RID: 3913
		// (get) Token: 0x06002A80 RID: 10880
		[DispId(-2147417058)]
		public virtual extern int uniqueNumber
		{
			[DispId(-2147417058)]
			[TypeLibFunc(64)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17000F4A RID: 3914
		// (get) Token: 0x06002A81 RID: 10881
		[DispId(-2147417057)]
		public virtual extern string uniqueID
		{
			[DispId(-2147417057)]
			[TypeLibFunc(64)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17000F4B RID: 3915
		// (get) Token: 0x06002A82 RID: 10882
		[DispId(-2147417066)]
		public virtual extern int nodeType
		{
			[DispId(-2147417066)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17000F4C RID: 3916
		// (get) Token: 0x06002A83 RID: 10883
		[DispId(-2147417065)]
		public virtual extern IHTMLDOMNode parentNode
		{
			[DispId(-2147417065)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x06002A84 RID: 10884
		[DispId(-2147417064)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool hasChildNodes();

		// Token: 0x17000F4D RID: 3917
		// (get) Token: 0x06002A85 RID: 10885
		[DispId(-2147417063)]
		public virtual extern object childNodes
		{
			[DispId(-2147417063)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17000F4E RID: 3918
		// (get) Token: 0x06002A86 RID: 10886
		[DispId(-2147417062)]
		public virtual extern object attributes
		{
			[DispId(-2147417062)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x06002A87 RID: 10887
		[DispId(-2147417061)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode insertBefore([MarshalAs(28)] [In] IHTMLDOMNode newChild, [MarshalAs(27)] [In] [Optional] object refChild);

		// Token: 0x06002A88 RID: 10888
		[DispId(-2147417060)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode removeChild([MarshalAs(28)] [In] IHTMLDOMNode oldChild);

		// Token: 0x06002A89 RID: 10889
		[DispId(-2147417059)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode replaceChild([MarshalAs(28)] [In] IHTMLDOMNode newChild, [MarshalAs(28)] [In] IHTMLDOMNode oldChild);

		// Token: 0x06002A8A RID: 10890
		[DispId(-2147417051)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode cloneNode([In] bool fDeep);

		// Token: 0x06002A8B RID: 10891
		[DispId(-2147417046)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode removeNode([In] bool fDeep = false);

		// Token: 0x06002A8C RID: 10892
		[DispId(-2147417044)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode swapNode([MarshalAs(28)] [In] IHTMLDOMNode otherNode);

		// Token: 0x06002A8D RID: 10893
		[DispId(-2147417045)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode replaceNode([MarshalAs(28)] [In] IHTMLDOMNode replacement);

		// Token: 0x06002A8E RID: 10894
		[DispId(-2147417039)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode appendChild([MarshalAs(28)] [In] IHTMLDOMNode newChild);

		// Token: 0x17000F4F RID: 3919
		// (get) Token: 0x06002A8F RID: 10895
		[DispId(-2147417038)]
		public virtual extern string nodeName
		{
			[DispId(-2147417038)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17000F50 RID: 3920
		// (get) Token: 0x06002A91 RID: 10897
		// (set) Token: 0x06002A90 RID: 10896
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

		// Token: 0x17000F51 RID: 3921
		// (get) Token: 0x06002A92 RID: 10898
		[DispId(-2147417036)]
		public virtual extern IHTMLDOMNode firstChild
		{
			[DispId(-2147417036)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17000F52 RID: 3922
		// (get) Token: 0x06002A93 RID: 10899
		[DispId(-2147417035)]
		public virtual extern IHTMLDOMNode lastChild
		{
			[DispId(-2147417035)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17000F53 RID: 3923
		// (get) Token: 0x06002A94 RID: 10900
		[DispId(-2147417034)]
		public virtual extern IHTMLDOMNode previousSibling
		{
			[DispId(-2147417034)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17000F54 RID: 3924
		// (get) Token: 0x06002A95 RID: 10901
		[DispId(-2147417033)]
		public virtual extern IHTMLDOMNode nextSibling
		{
			[DispId(-2147417033)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17000F55 RID: 3925
		// (get) Token: 0x06002A96 RID: 10902
		[DispId(-2147416999)]
		public virtual extern object ownerDocument
		{
			[DispId(-2147416999)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17000F56 RID: 3926
		// (get) Token: 0x06002A98 RID: 10904
		// (set) Token: 0x06002A97 RID: 10903
		[DispId(-2147417091)]
		public virtual extern string dataFld
		{
			[DispId(-2147417091)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147417091)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17000F57 RID: 3927
		// (get) Token: 0x06002A9A RID: 10906
		// (set) Token: 0x06002A99 RID: 10905
		[DispId(-2147417090)]
		public virtual extern string dataSrc
		{
			[DispId(-2147417090)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147417090)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17000F58 RID: 3928
		// (get) Token: 0x06002A9C RID: 10908
		// (set) Token: 0x06002A9B RID: 10907
		[DispId(-2147417089)]
		public virtual extern string dataFormatAs
		{
			[DispId(-2147417089)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147417089)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17000F59 RID: 3929
		// (get) Token: 0x06002A9E RID: 10910
		// (set) Token: 0x06002A9D RID: 10909
		[DispId(2002)]
		public virtual extern bool isMap
		{
			[DispId(2002)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[TypeLibFunc(20)]
			[DispId(2002)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x17000F5A RID: 3930
		// (get) Token: 0x06002AA0 RID: 10912
		// (set) Token: 0x06002A9F RID: 10911
		[DispId(2008)]
		public virtual extern string useMap
		{
			[DispId(2008)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(2008)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17000F5B RID: 3931
		// (get) Token: 0x06002AA1 RID: 10913
		[DispId(2010)]
		public virtual extern string mimeType
		{
			[DispId(2010)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17000F5C RID: 3932
		// (get) Token: 0x06002AA2 RID: 10914
		[DispId(2011)]
		public virtual extern string fileSize
		{
			[DispId(2011)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17000F5D RID: 3933
		// (get) Token: 0x06002AA3 RID: 10915
		[DispId(2012)]
		public virtual extern string fileCreatedDate
		{
			[DispId(2012)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17000F5E RID: 3934
		// (get) Token: 0x06002AA4 RID: 10916
		[DispId(2013)]
		public virtual extern string fileModifiedDate
		{
			[DispId(2013)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17000F5F RID: 3935
		// (get) Token: 0x06002AA5 RID: 10917
		[DispId(2014)]
		public virtual extern string fileUpdatedDate
		{
			[DispId(2014)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17000F60 RID: 3936
		// (get) Token: 0x06002AA6 RID: 10918
		[DispId(2015)]
		public virtual extern string protocol
		{
			[DispId(2015)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17000F61 RID: 3937
		// (get) Token: 0x06002AA7 RID: 10919
		[DispId(2016)]
		public virtual extern string href
		{
			[DispId(2016)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17000F62 RID: 3938
		// (get) Token: 0x06002AA8 RID: 10920
		[DispId(2017)]
		public virtual extern string nameProp
		{
			[DispId(2017)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17000F63 RID: 3939
		// (get) Token: 0x06002AAA RID: 10922
		// (set) Token: 0x06002AA9 RID: 10921
		[DispId(1004)]
		public virtual extern object border
		{
			[TypeLibFunc(20)]
			[DispId(1004)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(1004)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17000F64 RID: 3940
		// (get) Token: 0x06002AAC RID: 10924
		// (set) Token: 0x06002AAB RID: 10923
		[DispId(1005)]
		public virtual extern int vspace
		{
			[TypeLibFunc(20)]
			[DispId(1005)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[DispId(1005)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x17000F65 RID: 3941
		// (get) Token: 0x06002AAE RID: 10926
		// (set) Token: 0x06002AAD RID: 10925
		[DispId(1006)]
		public virtual extern int hspace
		{
			[TypeLibFunc(20)]
			[DispId(1006)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[TypeLibFunc(20)]
			[DispId(1006)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x17000F66 RID: 3942
		// (get) Token: 0x06002AB0 RID: 10928
		// (set) Token: 0x06002AAF RID: 10927
		[DispId(1002)]
		public virtual extern string alt
		{
			[DispId(1002)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(1002)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17000F67 RID: 3943
		// (get) Token: 0x06002AB2 RID: 10930
		// (set) Token: 0x06002AB1 RID: 10929
		[DispId(1003)]
		public virtual extern string src
		{
			[TypeLibFunc(20)]
			[DispId(1003)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(1003)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17000F68 RID: 3944
		// (get) Token: 0x06002AB4 RID: 10932
		// (set) Token: 0x06002AB3 RID: 10931
		[DispId(1007)]
		public virtual extern string lowsrc
		{
			[DispId(1007)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(1007)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17000F69 RID: 3945
		// (get) Token: 0x06002AB6 RID: 10934
		// (set) Token: 0x06002AB5 RID: 10933
		[DispId(1008)]
		public virtual extern string vrml
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

		// Token: 0x17000F6A RID: 3946
		// (get) Token: 0x06002AB8 RID: 10936
		// (set) Token: 0x06002AB7 RID: 10935
		[DispId(1009)]
		public virtual extern string dynsrc
		{
			[TypeLibFunc(20)]
			[DispId(1009)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(1009)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17000F6B RID: 3947
		// (get) Token: 0x06002AB9 RID: 10937
		[DispId(1010)]
		public virtual extern bool complete
		{
			[DispId(1010)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17000F6C RID: 3948
		// (get) Token: 0x06002ABB RID: 10939
		// (set) Token: 0x06002ABA RID: 10938
		[DispId(1011)]
		public virtual extern object loop
		{
			[TypeLibFunc(20)]
			[DispId(1011)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(1011)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17000F6D RID: 3949
		// (get) Token: 0x06002ABD RID: 10941
		// (set) Token: 0x06002ABC RID: 10940
		[DispId(-2147418039)]
		public virtual extern string align
		{
			[DispId(-2147418039)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147418039)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17000F6E RID: 3950
		// (get) Token: 0x06002ABF RID: 10943
		// (set) Token: 0x06002ABE RID: 10942
		[DispId(-2147412080)]
		public virtual extern object onload
		{
			[TypeLibFunc(20)]
			[DispId(-2147412080)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412080)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17000F6F RID: 3951
		// (get) Token: 0x06002AC1 RID: 10945
		// (set) Token: 0x06002AC0 RID: 10944
		[DispId(-2147412083)]
		public virtual extern object onerror
		{
			[DispId(-2147412083)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412083)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17000F70 RID: 3952
		// (get) Token: 0x06002AC3 RID: 10947
		// (set) Token: 0x06002AC2 RID: 10946
		[DispId(-2147412084)]
		public virtual extern object onabort
		{
			[DispId(-2147412084)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412084)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17000F71 RID: 3953
		// (get) Token: 0x06002AC5 RID: 10949
		// (set) Token: 0x06002AC4 RID: 10948
		[DispId(-2147418112)]
		public virtual extern string name
		{
			[DispId(-2147418112)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147418112)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17000F72 RID: 3954
		// (get) Token: 0x06002AC7 RID: 10951
		// (set) Token: 0x06002AC6 RID: 10950
		[DispId(-2147418107)]
		public virtual extern int width
		{
			[DispId(-2147418107)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[DispId(-2147418107)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x17000F73 RID: 3955
		// (get) Token: 0x06002AC9 RID: 10953
		// (set) Token: 0x06002AC8 RID: 10952
		[DispId(-2147418106)]
		public virtual extern int height
		{
			[DispId(-2147418106)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[DispId(-2147418106)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x17000F74 RID: 3956
		// (get) Token: 0x06002ACB RID: 10955
		// (set) Token: 0x06002ACA RID: 10954
		[DispId(1013)]
		public virtual extern string Start
		{
			[TypeLibFunc(20)]
			[DispId(1013)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(1013)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17000F75 RID: 3957
		// (get) Token: 0x06002ACD RID: 10957
		// (set) Token: 0x06002ACC RID: 10956
		[DispId(2019)]
		public virtual extern string longDesc
		{
			[DispId(2019)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(2019)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x06002ACE RID: 10958
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement_setAttribute([MarshalAs(19)] [In] string strAttributeName, [MarshalAs(27)] [In] object AttributeValue, [In] int lFlags = 1);

		// Token: 0x06002ACF RID: 10959
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		public virtual extern object IHTMLElement_getAttribute([MarshalAs(19)] [In] string strAttributeName, [In] int lFlags = 0);

		// Token: 0x06002AD0 RID: 10960
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement_removeAttribute([MarshalAs(19)] [In] string strAttributeName, [In] int lFlags = 1);

		// Token: 0x17000F76 RID: 3958
		// (get) Token: 0x06002AD2 RID: 10962
		// (set) Token: 0x06002AD1 RID: 10961
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

		// Token: 0x17000F77 RID: 3959
		// (get) Token: 0x06002AD4 RID: 10964
		// (set) Token: 0x06002AD3 RID: 10963
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

		// Token: 0x17000F78 RID: 3960
		// (get) Token: 0x06002AD5 RID: 10965
		public virtual extern string IHTMLElement_tagName
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17000F79 RID: 3961
		// (get) Token: 0x06002AD6 RID: 10966
		public virtual extern IHTMLElement IHTMLElement_parentElement
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17000F7A RID: 3962
		// (get) Token: 0x06002AD7 RID: 10967
		public virtual extern IHTMLStyle IHTMLElement_style
		{
			[TypeLibFunc(1024)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17000F7B RID: 3963
		// (get) Token: 0x06002AD9 RID: 10969
		// (set) Token: 0x06002AD8 RID: 10968
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

		// Token: 0x17000F7C RID: 3964
		// (get) Token: 0x06002ADB RID: 10971
		// (set) Token: 0x06002ADA RID: 10970
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

		// Token: 0x17000F7D RID: 3965
		// (get) Token: 0x06002ADD RID: 10973
		// (set) Token: 0x06002ADC RID: 10972
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

		// Token: 0x17000F7E RID: 3966
		// (get) Token: 0x06002ADF RID: 10975
		// (set) Token: 0x06002ADE RID: 10974
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

		// Token: 0x17000F7F RID: 3967
		// (get) Token: 0x06002AE1 RID: 10977
		// (set) Token: 0x06002AE0 RID: 10976
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

		// Token: 0x17000F80 RID: 3968
		// (get) Token: 0x06002AE3 RID: 10979
		// (set) Token: 0x06002AE2 RID: 10978
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

		// Token: 0x17000F81 RID: 3969
		// (get) Token: 0x06002AE5 RID: 10981
		// (set) Token: 0x06002AE4 RID: 10980
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

		// Token: 0x17000F82 RID: 3970
		// (get) Token: 0x06002AE7 RID: 10983
		// (set) Token: 0x06002AE6 RID: 10982
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

		// Token: 0x17000F83 RID: 3971
		// (get) Token: 0x06002AE9 RID: 10985
		// (set) Token: 0x06002AE8 RID: 10984
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

		// Token: 0x17000F84 RID: 3972
		// (get) Token: 0x06002AEB RID: 10987
		// (set) Token: 0x06002AEA RID: 10986
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

		// Token: 0x17000F85 RID: 3973
		// (get) Token: 0x06002AED RID: 10989
		// (set) Token: 0x06002AEC RID: 10988
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

		// Token: 0x17000F86 RID: 3974
		// (get) Token: 0x06002AEE RID: 10990
		public virtual extern object IHTMLElement_document
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17000F87 RID: 3975
		// (get) Token: 0x06002AF0 RID: 10992
		// (set) Token: 0x06002AEF RID: 10991
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

		// Token: 0x17000F88 RID: 3976
		// (get) Token: 0x06002AF2 RID: 10994
		// (set) Token: 0x06002AF1 RID: 10993
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

		// Token: 0x17000F89 RID: 3977
		// (get) Token: 0x06002AF4 RID: 10996
		// (set) Token: 0x06002AF3 RID: 10995
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

		// Token: 0x06002AF5 RID: 10997
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement_scrollIntoView([MarshalAs(27)] [In] [Optional] object varargStart);

		// Token: 0x06002AF6 RID: 10998
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement_contains([MarshalAs(28)] [In] IHTMLElement pChild);

		// Token: 0x17000F8A RID: 3978
		// (get) Token: 0x06002AF7 RID: 10999
		public virtual extern int IHTMLElement_sourceIndex
		{
			[TypeLibFunc(4)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17000F8B RID: 3979
		// (get) Token: 0x06002AF8 RID: 11000
		public virtual extern object IHTMLElement_recordNumber
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x17000F8C RID: 3980
		// (get) Token: 0x06002AFA RID: 11002
		// (set) Token: 0x06002AF9 RID: 11001
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

		// Token: 0x17000F8D RID: 3981
		// (get) Token: 0x06002AFB RID: 11003
		public virtual extern int IHTMLElement_offsetLeft
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17000F8E RID: 3982
		// (get) Token: 0x06002AFC RID: 11004
		public virtual extern int IHTMLElement_offsetTop
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17000F8F RID: 3983
		// (get) Token: 0x06002AFD RID: 11005
		public virtual extern int IHTMLElement_offsetWidth
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17000F90 RID: 3984
		// (get) Token: 0x06002AFE RID: 11006
		public virtual extern int IHTMLElement_offsetHeight
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17000F91 RID: 3985
		// (get) Token: 0x06002AFF RID: 11007
		public virtual extern IHTMLElement IHTMLElement_offsetParent
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17000F92 RID: 3986
		// (get) Token: 0x06002B01 RID: 11009
		// (set) Token: 0x06002B00 RID: 11008
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

		// Token: 0x17000F93 RID: 3987
		// (get) Token: 0x06002B03 RID: 11011
		// (set) Token: 0x06002B02 RID: 11010
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

		// Token: 0x17000F94 RID: 3988
		// (get) Token: 0x06002B05 RID: 11013
		// (set) Token: 0x06002B04 RID: 11012
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

		// Token: 0x17000F95 RID: 3989
		// (get) Token: 0x06002B07 RID: 11015
		// (set) Token: 0x06002B06 RID: 11014
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

		// Token: 0x06002B08 RID: 11016
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement_insertAdjacentHTML([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string html);

		// Token: 0x06002B09 RID: 11017
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement_insertAdjacentText([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string text);

		// Token: 0x17000F96 RID: 3990
		// (get) Token: 0x06002B0A RID: 11018
		public virtual extern IHTMLElement IHTMLElement_parentTextEdit
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17000F97 RID: 3991
		// (get) Token: 0x06002B0B RID: 11019
		public virtual extern bool IHTMLElement_isTextEdit
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x06002B0C RID: 11020
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement_click();

		// Token: 0x17000F98 RID: 3992
		// (get) Token: 0x06002B0D RID: 11021
		public virtual extern IHTMLFiltersCollection IHTMLElement_filters
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17000F99 RID: 3993
		// (get) Token: 0x06002B0F RID: 11023
		// (set) Token: 0x06002B0E RID: 11022
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

		// Token: 0x06002B10 RID: 11024
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string IHTMLElement_toString();

		// Token: 0x17000F9A RID: 3994
		// (get) Token: 0x06002B12 RID: 11026
		// (set) Token: 0x06002B11 RID: 11025
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

		// Token: 0x17000F9B RID: 3995
		// (get) Token: 0x06002B14 RID: 11028
		// (set) Token: 0x06002B13 RID: 11027
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

		// Token: 0x17000F9C RID: 3996
		// (get) Token: 0x06002B16 RID: 11030
		// (set) Token: 0x06002B15 RID: 11029
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

		// Token: 0x17000F9D RID: 3997
		// (get) Token: 0x06002B18 RID: 11032
		// (set) Token: 0x06002B17 RID: 11031
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

		// Token: 0x17000F9E RID: 3998
		// (get) Token: 0x06002B1A RID: 11034
		// (set) Token: 0x06002B19 RID: 11033
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

		// Token: 0x17000F9F RID: 3999
		// (get) Token: 0x06002B1C RID: 11036
		// (set) Token: 0x06002B1B RID: 11035
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

		// Token: 0x17000FA0 RID: 4000
		// (get) Token: 0x06002B1E RID: 11038
		// (set) Token: 0x06002B1D RID: 11037
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

		// Token: 0x17000FA1 RID: 4001
		// (get) Token: 0x06002B20 RID: 11040
		// (set) Token: 0x06002B1F RID: 11039
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

		// Token: 0x17000FA2 RID: 4002
		// (get) Token: 0x06002B22 RID: 11042
		// (set) Token: 0x06002B21 RID: 11041
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

		// Token: 0x17000FA3 RID: 4003
		// (get) Token: 0x06002B23 RID: 11043
		public virtual extern object IHTMLElement_children
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17000FA4 RID: 4004
		// (get) Token: 0x06002B24 RID: 11044
		public virtual extern object IHTMLElement_all
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17000FA5 RID: 4005
		// (get) Token: 0x06002B25 RID: 11045
		public virtual extern string IHTMLElement2_scopeName
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x06002B26 RID: 11046
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_setCapture([In] bool containerCapture = true);

		// Token: 0x06002B27 RID: 11047
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_releaseCapture();

		// Token: 0x17000FA6 RID: 4006
		// (get) Token: 0x06002B29 RID: 11049
		// (set) Token: 0x06002B28 RID: 11048
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

		// Token: 0x06002B2A RID: 11050
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string IHTMLElement2_componentFromPoint([In] int x, [In] int y);

		// Token: 0x06002B2B RID: 11051
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_doScroll([MarshalAs(27)] [In] [Optional] object component);

		// Token: 0x17000FA7 RID: 4007
		// (get) Token: 0x06002B2D RID: 11053
		// (set) Token: 0x06002B2C RID: 11052
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

		// Token: 0x17000FA8 RID: 4008
		// (get) Token: 0x06002B2F RID: 11055
		// (set) Token: 0x06002B2E RID: 11054
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

		// Token: 0x17000FA9 RID: 4009
		// (get) Token: 0x06002B31 RID: 11057
		// (set) Token: 0x06002B30 RID: 11056
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

		// Token: 0x17000FAA RID: 4010
		// (get) Token: 0x06002B33 RID: 11059
		// (set) Token: 0x06002B32 RID: 11058
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

		// Token: 0x17000FAB RID: 4011
		// (get) Token: 0x06002B35 RID: 11061
		// (set) Token: 0x06002B34 RID: 11060
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

		// Token: 0x17000FAC RID: 4012
		// (get) Token: 0x06002B37 RID: 11063
		// (set) Token: 0x06002B36 RID: 11062
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

		// Token: 0x17000FAD RID: 4013
		// (get) Token: 0x06002B39 RID: 11065
		// (set) Token: 0x06002B38 RID: 11064
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

		// Token: 0x17000FAE RID: 4014
		// (get) Token: 0x06002B3B RID: 11067
		// (set) Token: 0x06002B3A RID: 11066
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

		// Token: 0x17000FAF RID: 4015
		// (get) Token: 0x06002B3D RID: 11069
		// (set) Token: 0x06002B3C RID: 11068
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

		// Token: 0x17000FB0 RID: 4016
		// (get) Token: 0x06002B3F RID: 11071
		// (set) Token: 0x06002B3E RID: 11070
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

		// Token: 0x17000FB1 RID: 4017
		// (get) Token: 0x06002B41 RID: 11073
		// (set) Token: 0x06002B40 RID: 11072
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

		// Token: 0x17000FB2 RID: 4018
		// (get) Token: 0x06002B43 RID: 11075
		// (set) Token: 0x06002B42 RID: 11074
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

		// Token: 0x17000FB3 RID: 4019
		// (get) Token: 0x06002B45 RID: 11077
		// (set) Token: 0x06002B44 RID: 11076
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

		// Token: 0x17000FB4 RID: 4020
		// (get) Token: 0x06002B46 RID: 11078
		public virtual extern IHTMLCurrentStyle IHTMLElement2_currentStyle
		{
			[TypeLibFunc(1024)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17000FB5 RID: 4021
		// (get) Token: 0x06002B48 RID: 11080
		// (set) Token: 0x06002B47 RID: 11079
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

		// Token: 0x06002B49 RID: 11081
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLRectCollection IHTMLElement2_getClientRects();

		// Token: 0x06002B4A RID: 11082
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLRect IHTMLElement2_getBoundingClientRect();

		// Token: 0x06002B4B RID: 11083
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_setExpression([MarshalAs(19)] [In] string propname, [MarshalAs(19)] [In] string expression, [MarshalAs(19)] [In] string language = "");

		// Token: 0x06002B4C RID: 11084
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		public virtual extern object IHTMLElement2_getExpression([MarshalAs(19)] [In] string propname);

		// Token: 0x06002B4D RID: 11085
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement2_removeExpression([MarshalAs(19)] [In] string propname);

		// Token: 0x17000FB6 RID: 4022
		// (get) Token: 0x06002B4F RID: 11087
		// (set) Token: 0x06002B4E RID: 11086
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

		// Token: 0x06002B50 RID: 11088
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_focus();

		// Token: 0x17000FB7 RID: 4023
		// (get) Token: 0x06002B52 RID: 11090
		// (set) Token: 0x06002B51 RID: 11089
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

		// Token: 0x17000FB8 RID: 4024
		// (get) Token: 0x06002B54 RID: 11092
		// (set) Token: 0x06002B53 RID: 11091
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

		// Token: 0x17000FB9 RID: 4025
		// (get) Token: 0x06002B56 RID: 11094
		// (set) Token: 0x06002B55 RID: 11093
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

		// Token: 0x17000FBA RID: 4026
		// (get) Token: 0x06002B58 RID: 11096
		// (set) Token: 0x06002B57 RID: 11095
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

		// Token: 0x06002B59 RID: 11097
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_blur();

		// Token: 0x06002B5A RID: 11098
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_addFilter([MarshalAs(25)] [In] object pUnk);

		// Token: 0x06002B5B RID: 11099
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_removeFilter([MarshalAs(25)] [In] object pUnk);

		// Token: 0x17000FBB RID: 4027
		// (get) Token: 0x06002B5C RID: 11100
		public virtual extern int IHTMLElement2_clientHeight
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17000FBC RID: 4028
		// (get) Token: 0x06002B5D RID: 11101
		public virtual extern int IHTMLElement2_clientWidth
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17000FBD RID: 4029
		// (get) Token: 0x06002B5E RID: 11102
		public virtual extern int IHTMLElement2_clientTop
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17000FBE RID: 4030
		// (get) Token: 0x06002B5F RID: 11103
		public virtual extern int IHTMLElement2_clientLeft
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x06002B60 RID: 11104
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement2_attachEvent([MarshalAs(19)] [In] string @event, [MarshalAs(26)] [In] object pdisp);

		// Token: 0x06002B61 RID: 11105
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_detachEvent([MarshalAs(19)] [In] string @event, [MarshalAs(26)] [In] object pdisp);

		// Token: 0x17000FBF RID: 4031
		// (get) Token: 0x06002B62 RID: 11106
		public virtual extern object IHTMLElement2_readyState
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x17000FC0 RID: 4032
		// (get) Token: 0x06002B64 RID: 11108
		// (set) Token: 0x06002B63 RID: 11107
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

		// Token: 0x17000FC1 RID: 4033
		// (get) Token: 0x06002B66 RID: 11110
		// (set) Token: 0x06002B65 RID: 11109
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

		// Token: 0x17000FC2 RID: 4034
		// (get) Token: 0x06002B68 RID: 11112
		// (set) Token: 0x06002B67 RID: 11111
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

		// Token: 0x17000FC3 RID: 4035
		// (get) Token: 0x06002B6A RID: 11114
		// (set) Token: 0x06002B69 RID: 11113
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

		// Token: 0x17000FC4 RID: 4036
		// (get) Token: 0x06002B6C RID: 11116
		// (set) Token: 0x06002B6B RID: 11115
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

		// Token: 0x06002B6D RID: 11117
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(26)]
		public virtual extern object IHTMLElement2_createControlRange();

		// Token: 0x17000FC5 RID: 4037
		// (get) Token: 0x06002B6E RID: 11118
		public virtual extern int IHTMLElement2_scrollHeight
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17000FC6 RID: 4038
		// (get) Token: 0x06002B6F RID: 11119
		public virtual extern int IHTMLElement2_scrollWidth
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17000FC7 RID: 4039
		// (get) Token: 0x06002B71 RID: 11121
		// (set) Token: 0x06002B70 RID: 11120
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

		// Token: 0x17000FC8 RID: 4040
		// (get) Token: 0x06002B73 RID: 11123
		// (set) Token: 0x06002B72 RID: 11122
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

		// Token: 0x06002B74 RID: 11124
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_clearAttributes();

		// Token: 0x06002B75 RID: 11125
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void mergeAttributes([MarshalAs(28)] [In] IHTMLElement mergeThis);

		// Token: 0x17000FC9 RID: 4041
		// (get) Token: 0x06002B77 RID: 11127
		// (set) Token: 0x06002B76 RID: 11126
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

		// Token: 0x06002B78 RID: 11128
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElement IHTMLElement2_insertAdjacentElement([MarshalAs(19)] [In] string where, [MarshalAs(28)] [In] IHTMLElement insertedElement);

		// Token: 0x06002B79 RID: 11129
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElement IHTMLElement2_applyElement([MarshalAs(28)] [In] IHTMLElement apply, [MarshalAs(19)] [In] string where);

		// Token: 0x06002B7A RID: 11130
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string IHTMLElement2_getAdjacentText([MarshalAs(19)] [In] string where);

		// Token: 0x06002B7B RID: 11131
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string IHTMLElement2_replaceAdjacentText([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string newText);

		// Token: 0x17000FCA RID: 4042
		// (get) Token: 0x06002B7C RID: 11132
		public virtual extern bool IHTMLElement2_canHaveChildren
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x06002B7D RID: 11133
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern int IHTMLElement2_addBehavior([MarshalAs(19)] [In] string bstrUrl, [MarshalAs(27)] [In] [Optional] ref object pvarFactory);

		// Token: 0x06002B7E RID: 11134
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement2_removeBehavior([In] int cookie);

		// Token: 0x17000FCB RID: 4043
		// (get) Token: 0x06002B7F RID: 11135
		public virtual extern IHTMLStyle IHTMLElement2_runtimeStyle
		{
			[TypeLibFunc(1024)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17000FCC RID: 4044
		// (get) Token: 0x06002B80 RID: 11136
		public virtual extern object IHTMLElement2_behaviorUrns
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17000FCD RID: 4045
		// (get) Token: 0x06002B82 RID: 11138
		// (set) Token: 0x06002B81 RID: 11137
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

		// Token: 0x17000FCE RID: 4046
		// (get) Token: 0x06002B84 RID: 11140
		// (set) Token: 0x06002B83 RID: 11139
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

		// Token: 0x17000FCF RID: 4047
		// (get) Token: 0x06002B85 RID: 11141
		public virtual extern int IHTMLElement2_readyStateValue
		{
			[TypeLibFunc(65)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x06002B86 RID: 11142
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElementCollection IHTMLElement2_getElementsByTagName([MarshalAs(19)] [In] string v);

		// Token: 0x06002B87 RID: 11143
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void mergeAttributes([MarshalAs(28)] [In] IHTMLElement mergeThis, [MarshalAs(27)] [In] [Optional] ref object pvarFlags);

		// Token: 0x17000FD0 RID: 4048
		// (get) Token: 0x06002B88 RID: 11144
		public virtual extern bool IHTMLElement3_isMultiLine
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17000FD1 RID: 4049
		// (get) Token: 0x06002B89 RID: 11145
		public virtual extern bool IHTMLElement3_canHaveHTML
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17000FD2 RID: 4050
		// (get) Token: 0x06002B8B RID: 11147
		// (set) Token: 0x06002B8A RID: 11146
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

		// Token: 0x17000FD3 RID: 4051
		// (get) Token: 0x06002B8D RID: 11149
		// (set) Token: 0x06002B8C RID: 11148
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

		// Token: 0x17000FD4 RID: 4052
		// (get) Token: 0x06002B8F RID: 11151
		// (set) Token: 0x06002B8E RID: 11150
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

		// Token: 0x17000FD5 RID: 4053
		// (get) Token: 0x06002B91 RID: 11153
		// (set) Token: 0x06002B90 RID: 11152
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

		// Token: 0x06002B92 RID: 11154
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement3_setActive();

		// Token: 0x17000FD6 RID: 4054
		// (get) Token: 0x06002B94 RID: 11156
		// (set) Token: 0x06002B93 RID: 11155
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

		// Token: 0x17000FD7 RID: 4055
		// (get) Token: 0x06002B95 RID: 11157
		public virtual extern bool IHTMLElement3_isContentEditable
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17000FD8 RID: 4056
		// (get) Token: 0x06002B97 RID: 11159
		// (set) Token: 0x06002B96 RID: 11158
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

		// Token: 0x17000FD9 RID: 4057
		// (get) Token: 0x06002B99 RID: 11161
		// (set) Token: 0x06002B98 RID: 11160
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

		// Token: 0x17000FDA RID: 4058
		// (get) Token: 0x06002B9A RID: 11162
		public virtual extern bool IHTMLElement3_isDisabled
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17000FDB RID: 4059
		// (get) Token: 0x06002B9C RID: 11164
		// (set) Token: 0x06002B9B RID: 11163
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

		// Token: 0x17000FDC RID: 4060
		// (get) Token: 0x06002B9E RID: 11166
		// (set) Token: 0x06002B9D RID: 11165
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

		// Token: 0x06002B9F RID: 11167
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement3_FireEvent([MarshalAs(19)] [In] string bstrEventName, [MarshalAs(27)] [In] [Optional] ref object pvarEventObject);

		// Token: 0x17000FDD RID: 4061
		// (get) Token: 0x06002BA1 RID: 11169
		// (set) Token: 0x06002BA0 RID: 11168
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

		// Token: 0x17000FDE RID: 4062
		// (get) Token: 0x06002BA3 RID: 11171
		// (set) Token: 0x06002BA2 RID: 11170
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

		// Token: 0x17000FDF RID: 4063
		// (get) Token: 0x06002BA5 RID: 11173
		// (set) Token: 0x06002BA4 RID: 11172
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

		// Token: 0x17000FE0 RID: 4064
		// (get) Token: 0x06002BA7 RID: 11175
		// (set) Token: 0x06002BA6 RID: 11174
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

		// Token: 0x17000FE1 RID: 4065
		// (get) Token: 0x06002BA9 RID: 11177
		// (set) Token: 0x06002BA8 RID: 11176
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

		// Token: 0x17000FE2 RID: 4066
		// (get) Token: 0x06002BAB RID: 11179
		// (set) Token: 0x06002BAA RID: 11178
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

		// Token: 0x17000FE3 RID: 4067
		// (get) Token: 0x06002BAD RID: 11181
		// (set) Token: 0x06002BAC RID: 11180
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

		// Token: 0x17000FE4 RID: 4068
		// (get) Token: 0x06002BAF RID: 11183
		// (set) Token: 0x06002BAE RID: 11182
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

		// Token: 0x06002BB0 RID: 11184
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement3_dragDrop();

		// Token: 0x17000FE5 RID: 4069
		// (get) Token: 0x06002BB1 RID: 11185
		public virtual extern int IHTMLElement3_glyphMode
		{
			[TypeLibFunc(1089)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17000FE6 RID: 4070
		// (get) Token: 0x06002BB3 RID: 11187
		// (set) Token: 0x06002BB2 RID: 11186
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

		// Token: 0x06002BB4 RID: 11188
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement4_normalize();

		// Token: 0x06002BB5 RID: 11189
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute IHTMLElement4_getAttributeNode([MarshalAs(19)] [In] string bstrName);

		// Token: 0x06002BB6 RID: 11190
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute IHTMLElement4_setAttributeNode([MarshalAs(28)] [In] IHTMLDOMAttribute pattr);

		// Token: 0x06002BB7 RID: 11191
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute IHTMLElement4_removeAttributeNode([MarshalAs(28)] [In] IHTMLDOMAttribute pattr);

		// Token: 0x17000FE7 RID: 4071
		// (get) Token: 0x06002BB9 RID: 11193
		// (set) Token: 0x06002BB8 RID: 11192
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

		// Token: 0x17000FE8 RID: 4072
		// (get) Token: 0x06002BBB RID: 11195
		// (set) Token: 0x06002BBA RID: 11194
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

		// Token: 0x17000FE9 RID: 4073
		// (get) Token: 0x06002BBD RID: 11197
		// (set) Token: 0x06002BBC RID: 11196
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

		// Token: 0x17000FEA RID: 4074
		// (get) Token: 0x06002BBE RID: 11198
		public virtual extern int IHTMLUniqueName_uniqueNumber
		{
			[TypeLibFunc(64)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17000FEB RID: 4075
		// (get) Token: 0x06002BBF RID: 11199
		public virtual extern string IHTMLUniqueName_uniqueID
		{
			[TypeLibFunc(64)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17000FEC RID: 4076
		// (get) Token: 0x06002BC0 RID: 11200
		public virtual extern int IHTMLDOMNode_nodeType
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17000FED RID: 4077
		// (get) Token: 0x06002BC1 RID: 11201
		public virtual extern IHTMLDOMNode IHTMLDOMNode_parentNode
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x06002BC2 RID: 11202
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLDOMNode_hasChildNodes();

		// Token: 0x17000FEE RID: 4078
		// (get) Token: 0x06002BC3 RID: 11203
		public virtual extern object IHTMLDOMNode_childNodes
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17000FEF RID: 4079
		// (get) Token: 0x06002BC4 RID: 11204
		public virtual extern object IHTMLDOMNode_attributes
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x06002BC5 RID: 11205
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_insertBefore([MarshalAs(28)] [In] IHTMLDOMNode newChild, [MarshalAs(27)] [In] [Optional] object refChild);

		// Token: 0x06002BC6 RID: 11206
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_removeChild([MarshalAs(28)] [In] IHTMLDOMNode oldChild);

		// Token: 0x06002BC7 RID: 11207
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_replaceChild([MarshalAs(28)] [In] IHTMLDOMNode newChild, [MarshalAs(28)] [In] IHTMLDOMNode oldChild);

		// Token: 0x06002BC8 RID: 11208
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_cloneNode([In] bool fDeep);

		// Token: 0x06002BC9 RID: 11209
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_removeNode([In] bool fDeep = false);

		// Token: 0x06002BCA RID: 11210
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_swapNode([MarshalAs(28)] [In] IHTMLDOMNode otherNode);

		// Token: 0x06002BCB RID: 11211
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_replaceNode([MarshalAs(28)] [In] IHTMLDOMNode replacement);

		// Token: 0x06002BCC RID: 11212
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_appendChild([MarshalAs(28)] [In] IHTMLDOMNode newChild);

		// Token: 0x17000FF0 RID: 4080
		// (get) Token: 0x06002BCD RID: 11213
		public virtual extern string IHTMLDOMNode_nodeName
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17000FF1 RID: 4081
		// (get) Token: 0x06002BCF RID: 11215
		// (set) Token: 0x06002BCE RID: 11214
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

		// Token: 0x17000FF2 RID: 4082
		// (get) Token: 0x06002BD0 RID: 11216
		public virtual extern IHTMLDOMNode IHTMLDOMNode_firstChild
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17000FF3 RID: 4083
		// (get) Token: 0x06002BD1 RID: 11217
		public virtual extern IHTMLDOMNode IHTMLDOMNode_lastChild
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17000FF4 RID: 4084
		// (get) Token: 0x06002BD2 RID: 11218
		public virtual extern IHTMLDOMNode IHTMLDOMNode_previousSibling
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17000FF5 RID: 4085
		// (get) Token: 0x06002BD3 RID: 11219
		public virtual extern IHTMLDOMNode IHTMLDOMNode_nextSibling
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17000FF6 RID: 4086
		// (get) Token: 0x06002BD4 RID: 11220
		public virtual extern object IHTMLDOMNode2_ownerDocument
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17000FF7 RID: 4087
		// (get) Token: 0x06002BD6 RID: 11222
		// (set) Token: 0x06002BD5 RID: 11221
		public virtual extern string IHTMLDatabinding_dataFld
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17000FF8 RID: 4088
		// (get) Token: 0x06002BD8 RID: 11224
		// (set) Token: 0x06002BD7 RID: 11223
		public virtual extern string IHTMLDatabinding_dataSrc
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17000FF9 RID: 4089
		// (get) Token: 0x06002BDA RID: 11226
		// (set) Token: 0x06002BD9 RID: 11225
		public virtual extern string IHTMLDatabinding_dataFormatAs
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17000FFA RID: 4090
		// (get) Token: 0x06002BDC RID: 11228
		// (set) Token: 0x06002BDB RID: 11227
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

		// Token: 0x06002BDD RID: 11229
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLControlElement_focus();

		// Token: 0x17000FFB RID: 4091
		// (get) Token: 0x06002BDF RID: 11231
		// (set) Token: 0x06002BDE RID: 11230
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

		// Token: 0x17000FFC RID: 4092
		// (get) Token: 0x06002BE1 RID: 11233
		// (set) Token: 0x06002BE0 RID: 11232
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

		// Token: 0x17000FFD RID: 4093
		// (get) Token: 0x06002BE3 RID: 11235
		// (set) Token: 0x06002BE2 RID: 11234
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

		// Token: 0x17000FFE RID: 4094
		// (get) Token: 0x06002BE5 RID: 11237
		// (set) Token: 0x06002BE4 RID: 11236
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

		// Token: 0x06002BE6 RID: 11238
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLControlElement_blur();

		// Token: 0x06002BE7 RID: 11239
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLControlElement_addFilter([MarshalAs(25)] [In] object pUnk);

		// Token: 0x06002BE8 RID: 11240
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLControlElement_removeFilter([MarshalAs(25)] [In] object pUnk);

		// Token: 0x17000FFF RID: 4095
		// (get) Token: 0x06002BE9 RID: 11241
		public virtual extern int IHTMLControlElement_clientHeight
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17001000 RID: 4096
		// (get) Token: 0x06002BEA RID: 11242
		public virtual extern int IHTMLControlElement_clientWidth
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17001001 RID: 4097
		// (get) Token: 0x06002BEB RID: 11243
		public virtual extern int IHTMLControlElement_clientTop
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17001002 RID: 4098
		// (get) Token: 0x06002BEC RID: 11244
		public virtual extern int IHTMLControlElement_clientLeft
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17001003 RID: 4099
		// (get) Token: 0x06002BEE RID: 11246
		// (set) Token: 0x06002BED RID: 11245
		public virtual extern bool IHTMLImgElement_isMap
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x17001004 RID: 4100
		// (get) Token: 0x06002BF0 RID: 11248
		// (set) Token: 0x06002BEF RID: 11247
		public virtual extern string IHTMLImgElement_useMap
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

		// Token: 0x17001005 RID: 4101
		// (get) Token: 0x06002BF1 RID: 11249
		public virtual extern string IHTMLImgElement_mimeType
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17001006 RID: 4102
		// (get) Token: 0x06002BF2 RID: 11250
		public virtual extern string IHTMLImgElement_fileSize
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17001007 RID: 4103
		// (get) Token: 0x06002BF3 RID: 11251
		public virtual extern string IHTMLImgElement_fileCreatedDate
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17001008 RID: 4104
		// (get) Token: 0x06002BF4 RID: 11252
		public virtual extern string IHTMLImgElement_fileModifiedDate
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17001009 RID: 4105
		// (get) Token: 0x06002BF5 RID: 11253
		public virtual extern string IHTMLImgElement_fileUpdatedDate
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x1700100A RID: 4106
		// (get) Token: 0x06002BF6 RID: 11254
		public virtual extern string IHTMLImgElement_protocol
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x1700100B RID: 4107
		// (get) Token: 0x06002BF7 RID: 11255
		public virtual extern string IHTMLImgElement_href
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x1700100C RID: 4108
		// (get) Token: 0x06002BF8 RID: 11256
		public virtual extern string IHTMLImgElement_nameProp
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x1700100D RID: 4109
		// (get) Token: 0x06002BFA RID: 11258
		// (set) Token: 0x06002BF9 RID: 11257
		public virtual extern object IHTMLImgElement_border
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

		// Token: 0x1700100E RID: 4110
		// (get) Token: 0x06002BFC RID: 11260
		// (set) Token: 0x06002BFB RID: 11259
		public virtual extern int IHTMLImgElement_vspace
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x1700100F RID: 4111
		// (get) Token: 0x06002BFE RID: 11262
		// (set) Token: 0x06002BFD RID: 11261
		public virtual extern int IHTMLImgElement_hspace
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x17001010 RID: 4112
		// (get) Token: 0x06002C00 RID: 11264
		// (set) Token: 0x06002BFF RID: 11263
		public virtual extern string IHTMLImgElement_alt
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

		// Token: 0x17001011 RID: 4113
		// (get) Token: 0x06002C02 RID: 11266
		// (set) Token: 0x06002C01 RID: 11265
		public virtual extern string IHTMLImgElement_src
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

		// Token: 0x17001012 RID: 4114
		// (get) Token: 0x06002C04 RID: 11268
		// (set) Token: 0x06002C03 RID: 11267
		public virtual extern string IHTMLImgElement_lowsrc
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

		// Token: 0x17001013 RID: 4115
		// (get) Token: 0x06002C06 RID: 11270
		// (set) Token: 0x06002C05 RID: 11269
		public virtual extern string IHTMLImgElement_vrml
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

		// Token: 0x17001014 RID: 4116
		// (get) Token: 0x06002C08 RID: 11272
		// (set) Token: 0x06002C07 RID: 11271
		public virtual extern string IHTMLImgElement_dynsrc
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

		// Token: 0x17001015 RID: 4117
		// (get) Token: 0x06002C09 RID: 11273
		public virtual extern string IHTMLImgElement_readyState
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17001016 RID: 4118
		// (get) Token: 0x06002C0A RID: 11274
		public virtual extern bool IHTMLImgElement_complete
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17001017 RID: 4119
		// (get) Token: 0x06002C0C RID: 11276
		// (set) Token: 0x06002C0B RID: 11275
		public virtual extern object IHTMLImgElement_loop
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

		// Token: 0x17001018 RID: 4120
		// (get) Token: 0x06002C0E RID: 11278
		// (set) Token: 0x06002C0D RID: 11277
		public virtual extern string IHTMLImgElement_align
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

		// Token: 0x17001019 RID: 4121
		// (get) Token: 0x06002C10 RID: 11280
		// (set) Token: 0x06002C0F RID: 11279
		public virtual extern object IHTMLImgElement_onload
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

		// Token: 0x1700101A RID: 4122
		// (get) Token: 0x06002C12 RID: 11282
		// (set) Token: 0x06002C11 RID: 11281
		public virtual extern object IHTMLImgElement_onerror
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

		// Token: 0x1700101B RID: 4123
		// (get) Token: 0x06002C14 RID: 11284
		// (set) Token: 0x06002C13 RID: 11283
		public virtual extern object IHTMLImgElement_onabort
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

		// Token: 0x1700101C RID: 4124
		// (get) Token: 0x06002C16 RID: 11286
		// (set) Token: 0x06002C15 RID: 11285
		public virtual extern string IHTMLImgElement_name
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

		// Token: 0x1700101D RID: 4125
		// (get) Token: 0x06002C18 RID: 11288
		// (set) Token: 0x06002C17 RID: 11287
		public virtual extern int IHTMLImgElement_width
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x1700101E RID: 4126
		// (get) Token: 0x06002C1A RID: 11290
		// (set) Token: 0x06002C19 RID: 11289
		public virtual extern int IHTMLImgElement_height
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x1700101F RID: 4127
		// (get) Token: 0x06002C1C RID: 11292
		// (set) Token: 0x06002C1B RID: 11291
		public virtual extern string IHTMLImgElement_Start
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

		// Token: 0x17001020 RID: 4128
		// (get) Token: 0x06002C1E RID: 11294
		// (set) Token: 0x06002C1D RID: 11293
		public virtual extern string IHTMLImgElement2_longDesc
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

		// Token: 0x1400037C RID: 892
		// (add) Token: 0x06002C1F RID: 11295
		// (remove) Token: 0x06002C20 RID: 11296
		public virtual extern event HTMLImgEvents_onhelpEventHandler HTMLImgEvents_Event_onhelp;

		// Token: 0x1400037D RID: 893
		// (add) Token: 0x06002C21 RID: 11297
		// (remove) Token: 0x06002C22 RID: 11298
		public virtual extern event HTMLImgEvents_onclickEventHandler HTMLImgEvents_Event_onclick;

		// Token: 0x1400037E RID: 894
		// (add) Token: 0x06002C23 RID: 11299
		// (remove) Token: 0x06002C24 RID: 11300
		public virtual extern event HTMLImgEvents_ondblclickEventHandler HTMLImgEvents_Event_ondblclick;

		// Token: 0x1400037F RID: 895
		// (add) Token: 0x06002C25 RID: 11301
		// (remove) Token: 0x06002C26 RID: 11302
		public virtual extern event HTMLImgEvents_onkeypressEventHandler HTMLImgEvents_Event_onkeypress;

		// Token: 0x14000380 RID: 896
		// (add) Token: 0x06002C27 RID: 11303
		// (remove) Token: 0x06002C28 RID: 11304
		public virtual extern event HTMLImgEvents_onkeydownEventHandler HTMLImgEvents_Event_onkeydown;

		// Token: 0x14000381 RID: 897
		// (add) Token: 0x06002C29 RID: 11305
		// (remove) Token: 0x06002C2A RID: 11306
		public virtual extern event HTMLImgEvents_onkeyupEventHandler HTMLImgEvents_Event_onkeyup;

		// Token: 0x14000382 RID: 898
		// (add) Token: 0x06002C2B RID: 11307
		// (remove) Token: 0x06002C2C RID: 11308
		public virtual extern event HTMLImgEvents_onmouseoutEventHandler HTMLImgEvents_Event_onmouseout;

		// Token: 0x14000383 RID: 899
		// (add) Token: 0x06002C2D RID: 11309
		// (remove) Token: 0x06002C2E RID: 11310
		public virtual extern event HTMLImgEvents_onmouseoverEventHandler HTMLImgEvents_Event_onmouseover;

		// Token: 0x14000384 RID: 900
		// (add) Token: 0x06002C2F RID: 11311
		// (remove) Token: 0x06002C30 RID: 11312
		public virtual extern event HTMLImgEvents_onmousemoveEventHandler HTMLImgEvents_Event_onmousemove;

		// Token: 0x14000385 RID: 901
		// (add) Token: 0x06002C31 RID: 11313
		// (remove) Token: 0x06002C32 RID: 11314
		public virtual extern event HTMLImgEvents_onmousedownEventHandler HTMLImgEvents_Event_onmousedown;

		// Token: 0x14000386 RID: 902
		// (add) Token: 0x06002C33 RID: 11315
		// (remove) Token: 0x06002C34 RID: 11316
		public virtual extern event HTMLImgEvents_onmouseupEventHandler HTMLImgEvents_Event_onmouseup;

		// Token: 0x14000387 RID: 903
		// (add) Token: 0x06002C35 RID: 11317
		// (remove) Token: 0x06002C36 RID: 11318
		public virtual extern event HTMLImgEvents_onselectstartEventHandler HTMLImgEvents_Event_onselectstart;

		// Token: 0x14000388 RID: 904
		// (add) Token: 0x06002C37 RID: 11319
		// (remove) Token: 0x06002C38 RID: 11320
		public virtual extern event HTMLImgEvents_onfilterchangeEventHandler HTMLImgEvents_Event_onfilterchange;

		// Token: 0x14000389 RID: 905
		// (add) Token: 0x06002C39 RID: 11321
		// (remove) Token: 0x06002C3A RID: 11322
		public virtual extern event HTMLImgEvents_ondragstartEventHandler HTMLImgEvents_Event_ondragstart;

		// Token: 0x1400038A RID: 906
		// (add) Token: 0x06002C3B RID: 11323
		// (remove) Token: 0x06002C3C RID: 11324
		public virtual extern event HTMLImgEvents_onbeforeupdateEventHandler HTMLImgEvents_Event_onbeforeupdate;

		// Token: 0x1400038B RID: 907
		// (add) Token: 0x06002C3D RID: 11325
		// (remove) Token: 0x06002C3E RID: 11326
		public virtual extern event HTMLImgEvents_onafterupdateEventHandler HTMLImgEvents_Event_onafterupdate;

		// Token: 0x1400038C RID: 908
		// (add) Token: 0x06002C3F RID: 11327
		// (remove) Token: 0x06002C40 RID: 11328
		public virtual extern event HTMLImgEvents_onerrorupdateEventHandler HTMLImgEvents_Event_onerrorupdate;

		// Token: 0x1400038D RID: 909
		// (add) Token: 0x06002C41 RID: 11329
		// (remove) Token: 0x06002C42 RID: 11330
		public virtual extern event HTMLImgEvents_onrowexitEventHandler HTMLImgEvents_Event_onrowexit;

		// Token: 0x1400038E RID: 910
		// (add) Token: 0x06002C43 RID: 11331
		// (remove) Token: 0x06002C44 RID: 11332
		public virtual extern event HTMLImgEvents_onrowenterEventHandler HTMLImgEvents_Event_onrowenter;

		// Token: 0x1400038F RID: 911
		// (add) Token: 0x06002C45 RID: 11333
		// (remove) Token: 0x06002C46 RID: 11334
		public virtual extern event HTMLImgEvents_ondatasetchangedEventHandler HTMLImgEvents_Event_ondatasetchanged;

		// Token: 0x14000390 RID: 912
		// (add) Token: 0x06002C47 RID: 11335
		// (remove) Token: 0x06002C48 RID: 11336
		public virtual extern event HTMLImgEvents_ondataavailableEventHandler HTMLImgEvents_Event_ondataavailable;

		// Token: 0x14000391 RID: 913
		// (add) Token: 0x06002C49 RID: 11337
		// (remove) Token: 0x06002C4A RID: 11338
		public virtual extern event HTMLImgEvents_ondatasetcompleteEventHandler HTMLImgEvents_Event_ondatasetcomplete;

		// Token: 0x14000392 RID: 914
		// (add) Token: 0x06002C4B RID: 11339
		// (remove) Token: 0x06002C4C RID: 11340
		public virtual extern event HTMLImgEvents_onlosecaptureEventHandler HTMLImgEvents_Event_onlosecapture;

		// Token: 0x14000393 RID: 915
		// (add) Token: 0x06002C4D RID: 11341
		// (remove) Token: 0x06002C4E RID: 11342
		public virtual extern event HTMLImgEvents_onpropertychangeEventHandler HTMLImgEvents_Event_onpropertychange;

		// Token: 0x14000394 RID: 916
		// (add) Token: 0x06002C4F RID: 11343
		// (remove) Token: 0x06002C50 RID: 11344
		public virtual extern event HTMLImgEvents_onscrollEventHandler HTMLImgEvents_Event_onscroll;

		// Token: 0x14000395 RID: 917
		// (add) Token: 0x06002C51 RID: 11345
		// (remove) Token: 0x06002C52 RID: 11346
		public virtual extern event HTMLImgEvents_onfocusEventHandler HTMLImgEvents_Event_onfocus;

		// Token: 0x14000396 RID: 918
		// (add) Token: 0x06002C53 RID: 11347
		// (remove) Token: 0x06002C54 RID: 11348
		public virtual extern event HTMLImgEvents_onblurEventHandler HTMLImgEvents_Event_onblur;

		// Token: 0x14000397 RID: 919
		// (add) Token: 0x06002C55 RID: 11349
		// (remove) Token: 0x06002C56 RID: 11350
		public virtual extern event HTMLImgEvents_onresizeEventHandler HTMLImgEvents_Event_onresize;

		// Token: 0x14000398 RID: 920
		// (add) Token: 0x06002C57 RID: 11351
		// (remove) Token: 0x06002C58 RID: 11352
		public virtual extern event HTMLImgEvents_ondragEventHandler HTMLImgEvents_Event_ondrag;

		// Token: 0x14000399 RID: 921
		// (add) Token: 0x06002C59 RID: 11353
		// (remove) Token: 0x06002C5A RID: 11354
		public virtual extern event HTMLImgEvents_ondragendEventHandler HTMLImgEvents_Event_ondragend;

		// Token: 0x1400039A RID: 922
		// (add) Token: 0x06002C5B RID: 11355
		// (remove) Token: 0x06002C5C RID: 11356
		public virtual extern event HTMLImgEvents_ondragenterEventHandler HTMLImgEvents_Event_ondragenter;

		// Token: 0x1400039B RID: 923
		// (add) Token: 0x06002C5D RID: 11357
		// (remove) Token: 0x06002C5E RID: 11358
		public virtual extern event HTMLImgEvents_ondragoverEventHandler HTMLImgEvents_Event_ondragover;

		// Token: 0x1400039C RID: 924
		// (add) Token: 0x06002C5F RID: 11359
		// (remove) Token: 0x06002C60 RID: 11360
		public virtual extern event HTMLImgEvents_ondragleaveEventHandler HTMLImgEvents_Event_ondragleave;

		// Token: 0x1400039D RID: 925
		// (add) Token: 0x06002C61 RID: 11361
		// (remove) Token: 0x06002C62 RID: 11362
		public virtual extern event HTMLImgEvents_ondropEventHandler HTMLImgEvents_Event_ondrop;

		// Token: 0x1400039E RID: 926
		// (add) Token: 0x06002C63 RID: 11363
		// (remove) Token: 0x06002C64 RID: 11364
		public virtual extern event HTMLImgEvents_onbeforecutEventHandler HTMLImgEvents_Event_onbeforecut;

		// Token: 0x1400039F RID: 927
		// (add) Token: 0x06002C65 RID: 11365
		// (remove) Token: 0x06002C66 RID: 11366
		public virtual extern event HTMLImgEvents_oncutEventHandler HTMLImgEvents_Event_oncut;

		// Token: 0x140003A0 RID: 928
		// (add) Token: 0x06002C67 RID: 11367
		// (remove) Token: 0x06002C68 RID: 11368
		public virtual extern event HTMLImgEvents_onbeforecopyEventHandler HTMLImgEvents_Event_onbeforecopy;

		// Token: 0x140003A1 RID: 929
		// (add) Token: 0x06002C69 RID: 11369
		// (remove) Token: 0x06002C6A RID: 11370
		public virtual extern event HTMLImgEvents_oncopyEventHandler HTMLImgEvents_Event_oncopy;

		// Token: 0x140003A2 RID: 930
		// (add) Token: 0x06002C6B RID: 11371
		// (remove) Token: 0x06002C6C RID: 11372
		public virtual extern event HTMLImgEvents_onbeforepasteEventHandler HTMLImgEvents_Event_onbeforepaste;

		// Token: 0x140003A3 RID: 931
		// (add) Token: 0x06002C6D RID: 11373
		// (remove) Token: 0x06002C6E RID: 11374
		public virtual extern event HTMLImgEvents_onpasteEventHandler HTMLImgEvents_Event_onpaste;

		// Token: 0x140003A4 RID: 932
		// (add) Token: 0x06002C6F RID: 11375
		// (remove) Token: 0x06002C70 RID: 11376
		public virtual extern event HTMLImgEvents_oncontextmenuEventHandler HTMLImgEvents_Event_oncontextmenu;

		// Token: 0x140003A5 RID: 933
		// (add) Token: 0x06002C71 RID: 11377
		// (remove) Token: 0x06002C72 RID: 11378
		public virtual extern event HTMLImgEvents_onrowsdeleteEventHandler HTMLImgEvents_Event_onrowsdelete;

		// Token: 0x140003A6 RID: 934
		// (add) Token: 0x06002C73 RID: 11379
		// (remove) Token: 0x06002C74 RID: 11380
		public virtual extern event HTMLImgEvents_onrowsinsertedEventHandler HTMLImgEvents_Event_onrowsinserted;

		// Token: 0x140003A7 RID: 935
		// (add) Token: 0x06002C75 RID: 11381
		// (remove) Token: 0x06002C76 RID: 11382
		public virtual extern event HTMLImgEvents_oncellchangeEventHandler HTMLImgEvents_Event_oncellchange;

		// Token: 0x140003A8 RID: 936
		// (add) Token: 0x06002C77 RID: 11383
		// (remove) Token: 0x06002C78 RID: 11384
		public virtual extern event HTMLImgEvents_onreadystatechangeEventHandler HTMLImgEvents_Event_onreadystatechange;

		// Token: 0x140003A9 RID: 937
		// (add) Token: 0x06002C79 RID: 11385
		// (remove) Token: 0x06002C7A RID: 11386
		public virtual extern event HTMLImgEvents_onbeforeeditfocusEventHandler HTMLImgEvents_Event_onbeforeeditfocus;

		// Token: 0x140003AA RID: 938
		// (add) Token: 0x06002C7B RID: 11387
		// (remove) Token: 0x06002C7C RID: 11388
		public virtual extern event HTMLImgEvents_onlayoutcompleteEventHandler HTMLImgEvents_Event_onlayoutcomplete;

		// Token: 0x140003AB RID: 939
		// (add) Token: 0x06002C7D RID: 11389
		// (remove) Token: 0x06002C7E RID: 11390
		public virtual extern event HTMLImgEvents_onpageEventHandler HTMLImgEvents_Event_onpage;

		// Token: 0x140003AC RID: 940
		// (add) Token: 0x06002C7F RID: 11391
		// (remove) Token: 0x06002C80 RID: 11392
		public virtual extern event HTMLImgEvents_onbeforedeactivateEventHandler HTMLImgEvents_Event_onbeforedeactivate;

		// Token: 0x140003AD RID: 941
		// (add) Token: 0x06002C81 RID: 11393
		// (remove) Token: 0x06002C82 RID: 11394
		public virtual extern event HTMLImgEvents_onbeforeactivateEventHandler HTMLImgEvents_Event_onbeforeactivate;

		// Token: 0x140003AE RID: 942
		// (add) Token: 0x06002C83 RID: 11395
		// (remove) Token: 0x06002C84 RID: 11396
		public virtual extern event HTMLImgEvents_onmoveEventHandler HTMLImgEvents_Event_onmove;

		// Token: 0x140003AF RID: 943
		// (add) Token: 0x06002C85 RID: 11397
		// (remove) Token: 0x06002C86 RID: 11398
		public virtual extern event HTMLImgEvents_oncontrolselectEventHandler HTMLImgEvents_Event_oncontrolselect;

		// Token: 0x140003B0 RID: 944
		// (add) Token: 0x06002C87 RID: 11399
		// (remove) Token: 0x06002C88 RID: 11400
		public virtual extern event HTMLImgEvents_onmovestartEventHandler HTMLImgEvents_Event_onmovestart;

		// Token: 0x140003B1 RID: 945
		// (add) Token: 0x06002C89 RID: 11401
		// (remove) Token: 0x06002C8A RID: 11402
		public virtual extern event HTMLImgEvents_onmoveendEventHandler HTMLImgEvents_Event_onmoveend;

		// Token: 0x140003B2 RID: 946
		// (add) Token: 0x06002C8B RID: 11403
		// (remove) Token: 0x06002C8C RID: 11404
		public virtual extern event HTMLImgEvents_onresizestartEventHandler HTMLImgEvents_Event_onresizestart;

		// Token: 0x140003B3 RID: 947
		// (add) Token: 0x06002C8D RID: 11405
		// (remove) Token: 0x06002C8E RID: 11406
		public virtual extern event HTMLImgEvents_onresizeendEventHandler HTMLImgEvents_Event_onresizeend;

		// Token: 0x140003B4 RID: 948
		// (add) Token: 0x06002C8F RID: 11407
		// (remove) Token: 0x06002C90 RID: 11408
		public virtual extern event HTMLImgEvents_onmouseenterEventHandler HTMLImgEvents_Event_onmouseenter;

		// Token: 0x140003B5 RID: 949
		// (add) Token: 0x06002C91 RID: 11409
		// (remove) Token: 0x06002C92 RID: 11410
		public virtual extern event HTMLImgEvents_onmouseleaveEventHandler HTMLImgEvents_Event_onmouseleave;

		// Token: 0x140003B6 RID: 950
		// (add) Token: 0x06002C93 RID: 11411
		// (remove) Token: 0x06002C94 RID: 11412
		public virtual extern event HTMLImgEvents_onmousewheelEventHandler HTMLImgEvents_Event_onmousewheel;

		// Token: 0x140003B7 RID: 951
		// (add) Token: 0x06002C95 RID: 11413
		// (remove) Token: 0x06002C96 RID: 11414
		public virtual extern event HTMLImgEvents_onactivateEventHandler HTMLImgEvents_Event_onactivate;

		// Token: 0x140003B8 RID: 952
		// (add) Token: 0x06002C97 RID: 11415
		// (remove) Token: 0x06002C98 RID: 11416
		public virtual extern event HTMLImgEvents_ondeactivateEventHandler HTMLImgEvents_Event_ondeactivate;

		// Token: 0x140003B9 RID: 953
		// (add) Token: 0x06002C99 RID: 11417
		// (remove) Token: 0x06002C9A RID: 11418
		public virtual extern event HTMLImgEvents_onfocusinEventHandler HTMLImgEvents_Event_onfocusin;

		// Token: 0x140003BA RID: 954
		// (add) Token: 0x06002C9B RID: 11419
		// (remove) Token: 0x06002C9C RID: 11420
		public virtual extern event HTMLImgEvents_onfocusoutEventHandler HTMLImgEvents_Event_onfocusout;

		// Token: 0x140003BB RID: 955
		// (add) Token: 0x06002C9D RID: 11421
		// (remove) Token: 0x06002C9E RID: 11422
		public virtual extern event HTMLImgEvents_onloadEventHandler HTMLImgEvents_Event_onload;

		// Token: 0x140003BC RID: 956
		// (add) Token: 0x06002C9F RID: 11423
		// (remove) Token: 0x06002CA0 RID: 11424
		public virtual extern event HTMLImgEvents_onerrorEventHandler HTMLImgEvents_Event_onerror;

		// Token: 0x140003BD RID: 957
		// (add) Token: 0x06002CA1 RID: 11425
		// (remove) Token: 0x06002CA2 RID: 11426
		public virtual extern event HTMLImgEvents_onabortEventHandler HTMLImgEvents_Event_onabort;

		// Token: 0x140003BE RID: 958
		// (add) Token: 0x06002CA3 RID: 11427
		// (remove) Token: 0x06002CA4 RID: 11428
		public virtual extern event HTMLImgEvents2_onhelpEventHandler HTMLImgEvents2_Event_onhelp;

		// Token: 0x140003BF RID: 959
		// (add) Token: 0x06002CA5 RID: 11429
		// (remove) Token: 0x06002CA6 RID: 11430
		public virtual extern event HTMLImgEvents2_onclickEventHandler HTMLImgEvents2_Event_onclick;

		// Token: 0x140003C0 RID: 960
		// (add) Token: 0x06002CA7 RID: 11431
		// (remove) Token: 0x06002CA8 RID: 11432
		public virtual extern event HTMLImgEvents2_ondblclickEventHandler HTMLImgEvents2_Event_ondblclick;

		// Token: 0x140003C1 RID: 961
		// (add) Token: 0x06002CA9 RID: 11433
		// (remove) Token: 0x06002CAA RID: 11434
		public virtual extern event HTMLImgEvents2_onkeypressEventHandler HTMLImgEvents2_Event_onkeypress;

		// Token: 0x140003C2 RID: 962
		// (add) Token: 0x06002CAB RID: 11435
		// (remove) Token: 0x06002CAC RID: 11436
		public virtual extern event HTMLImgEvents2_onkeydownEventHandler HTMLImgEvents2_Event_onkeydown;

		// Token: 0x140003C3 RID: 963
		// (add) Token: 0x06002CAD RID: 11437
		// (remove) Token: 0x06002CAE RID: 11438
		public virtual extern event HTMLImgEvents2_onkeyupEventHandler HTMLImgEvents2_Event_onkeyup;

		// Token: 0x140003C4 RID: 964
		// (add) Token: 0x06002CAF RID: 11439
		// (remove) Token: 0x06002CB0 RID: 11440
		public virtual extern event HTMLImgEvents2_onmouseoutEventHandler HTMLImgEvents2_Event_onmouseout;

		// Token: 0x140003C5 RID: 965
		// (add) Token: 0x06002CB1 RID: 11441
		// (remove) Token: 0x06002CB2 RID: 11442
		public virtual extern event HTMLImgEvents2_onmouseoverEventHandler HTMLImgEvents2_Event_onmouseover;

		// Token: 0x140003C6 RID: 966
		// (add) Token: 0x06002CB3 RID: 11443
		// (remove) Token: 0x06002CB4 RID: 11444
		public virtual extern event HTMLImgEvents2_onmousemoveEventHandler HTMLImgEvents2_Event_onmousemove;

		// Token: 0x140003C7 RID: 967
		// (add) Token: 0x06002CB5 RID: 11445
		// (remove) Token: 0x06002CB6 RID: 11446
		public virtual extern event HTMLImgEvents2_onmousedownEventHandler HTMLImgEvents2_Event_onmousedown;

		// Token: 0x140003C8 RID: 968
		// (add) Token: 0x06002CB7 RID: 11447
		// (remove) Token: 0x06002CB8 RID: 11448
		public virtual extern event HTMLImgEvents2_onmouseupEventHandler HTMLImgEvents2_Event_onmouseup;

		// Token: 0x140003C9 RID: 969
		// (add) Token: 0x06002CB9 RID: 11449
		// (remove) Token: 0x06002CBA RID: 11450
		public virtual extern event HTMLImgEvents2_onselectstartEventHandler HTMLImgEvents2_Event_onselectstart;

		// Token: 0x140003CA RID: 970
		// (add) Token: 0x06002CBB RID: 11451
		// (remove) Token: 0x06002CBC RID: 11452
		public virtual extern event HTMLImgEvents2_onfilterchangeEventHandler HTMLImgEvents2_Event_onfilterchange;

		// Token: 0x140003CB RID: 971
		// (add) Token: 0x06002CBD RID: 11453
		// (remove) Token: 0x06002CBE RID: 11454
		public virtual extern event HTMLImgEvents2_ondragstartEventHandler HTMLImgEvents2_Event_ondragstart;

		// Token: 0x140003CC RID: 972
		// (add) Token: 0x06002CBF RID: 11455
		// (remove) Token: 0x06002CC0 RID: 11456
		public virtual extern event HTMLImgEvents2_onbeforeupdateEventHandler HTMLImgEvents2_Event_onbeforeupdate;

		// Token: 0x140003CD RID: 973
		// (add) Token: 0x06002CC1 RID: 11457
		// (remove) Token: 0x06002CC2 RID: 11458
		public virtual extern event HTMLImgEvents2_onafterupdateEventHandler HTMLImgEvents2_Event_onafterupdate;

		// Token: 0x140003CE RID: 974
		// (add) Token: 0x06002CC3 RID: 11459
		// (remove) Token: 0x06002CC4 RID: 11460
		public virtual extern event HTMLImgEvents2_onerrorupdateEventHandler HTMLImgEvents2_Event_onerrorupdate;

		// Token: 0x140003CF RID: 975
		// (add) Token: 0x06002CC5 RID: 11461
		// (remove) Token: 0x06002CC6 RID: 11462
		public virtual extern event HTMLImgEvents2_onrowexitEventHandler HTMLImgEvents2_Event_onrowexit;

		// Token: 0x140003D0 RID: 976
		// (add) Token: 0x06002CC7 RID: 11463
		// (remove) Token: 0x06002CC8 RID: 11464
		public virtual extern event HTMLImgEvents2_onrowenterEventHandler HTMLImgEvents2_Event_onrowenter;

		// Token: 0x140003D1 RID: 977
		// (add) Token: 0x06002CC9 RID: 11465
		// (remove) Token: 0x06002CCA RID: 11466
		public virtual extern event HTMLImgEvents2_ondatasetchangedEventHandler HTMLImgEvents2_Event_ondatasetchanged;

		// Token: 0x140003D2 RID: 978
		// (add) Token: 0x06002CCB RID: 11467
		// (remove) Token: 0x06002CCC RID: 11468
		public virtual extern event HTMLImgEvents2_ondataavailableEventHandler HTMLImgEvents2_Event_ondataavailable;

		// Token: 0x140003D3 RID: 979
		// (add) Token: 0x06002CCD RID: 11469
		// (remove) Token: 0x06002CCE RID: 11470
		public virtual extern event HTMLImgEvents2_ondatasetcompleteEventHandler HTMLImgEvents2_Event_ondatasetcomplete;

		// Token: 0x140003D4 RID: 980
		// (add) Token: 0x06002CCF RID: 11471
		// (remove) Token: 0x06002CD0 RID: 11472
		public virtual extern event HTMLImgEvents2_onlosecaptureEventHandler HTMLImgEvents2_Event_onlosecapture;

		// Token: 0x140003D5 RID: 981
		// (add) Token: 0x06002CD1 RID: 11473
		// (remove) Token: 0x06002CD2 RID: 11474
		public virtual extern event HTMLImgEvents2_onpropertychangeEventHandler HTMLImgEvents2_Event_onpropertychange;

		// Token: 0x140003D6 RID: 982
		// (add) Token: 0x06002CD3 RID: 11475
		// (remove) Token: 0x06002CD4 RID: 11476
		public virtual extern event HTMLImgEvents2_onscrollEventHandler HTMLImgEvents2_Event_onscroll;

		// Token: 0x140003D7 RID: 983
		// (add) Token: 0x06002CD5 RID: 11477
		// (remove) Token: 0x06002CD6 RID: 11478
		public virtual extern event HTMLImgEvents2_onfocusEventHandler HTMLImgEvents2_Event_onfocus;

		// Token: 0x140003D8 RID: 984
		// (add) Token: 0x06002CD7 RID: 11479
		// (remove) Token: 0x06002CD8 RID: 11480
		public virtual extern event HTMLImgEvents2_onblurEventHandler HTMLImgEvents2_Event_onblur;

		// Token: 0x140003D9 RID: 985
		// (add) Token: 0x06002CD9 RID: 11481
		// (remove) Token: 0x06002CDA RID: 11482
		public virtual extern event HTMLImgEvents2_onresizeEventHandler HTMLImgEvents2_Event_onresize;

		// Token: 0x140003DA RID: 986
		// (add) Token: 0x06002CDB RID: 11483
		// (remove) Token: 0x06002CDC RID: 11484
		public virtual extern event HTMLImgEvents2_ondragEventHandler HTMLImgEvents2_Event_ondrag;

		// Token: 0x140003DB RID: 987
		// (add) Token: 0x06002CDD RID: 11485
		// (remove) Token: 0x06002CDE RID: 11486
		public virtual extern event HTMLImgEvents2_ondragendEventHandler HTMLImgEvents2_Event_ondragend;

		// Token: 0x140003DC RID: 988
		// (add) Token: 0x06002CDF RID: 11487
		// (remove) Token: 0x06002CE0 RID: 11488
		public virtual extern event HTMLImgEvents2_ondragenterEventHandler HTMLImgEvents2_Event_ondragenter;

		// Token: 0x140003DD RID: 989
		// (add) Token: 0x06002CE1 RID: 11489
		// (remove) Token: 0x06002CE2 RID: 11490
		public virtual extern event HTMLImgEvents2_ondragoverEventHandler HTMLImgEvents2_Event_ondragover;

		// Token: 0x140003DE RID: 990
		// (add) Token: 0x06002CE3 RID: 11491
		// (remove) Token: 0x06002CE4 RID: 11492
		public virtual extern event HTMLImgEvents2_ondragleaveEventHandler HTMLImgEvents2_Event_ondragleave;

		// Token: 0x140003DF RID: 991
		// (add) Token: 0x06002CE5 RID: 11493
		// (remove) Token: 0x06002CE6 RID: 11494
		public virtual extern event HTMLImgEvents2_ondropEventHandler HTMLImgEvents2_Event_ondrop;

		// Token: 0x140003E0 RID: 992
		// (add) Token: 0x06002CE7 RID: 11495
		// (remove) Token: 0x06002CE8 RID: 11496
		public virtual extern event HTMLImgEvents2_onbeforecutEventHandler HTMLImgEvents2_Event_onbeforecut;

		// Token: 0x140003E1 RID: 993
		// (add) Token: 0x06002CE9 RID: 11497
		// (remove) Token: 0x06002CEA RID: 11498
		public virtual extern event HTMLImgEvents2_oncutEventHandler HTMLImgEvents2_Event_oncut;

		// Token: 0x140003E2 RID: 994
		// (add) Token: 0x06002CEB RID: 11499
		// (remove) Token: 0x06002CEC RID: 11500
		public virtual extern event HTMLImgEvents2_onbeforecopyEventHandler HTMLImgEvents2_Event_onbeforecopy;

		// Token: 0x140003E3 RID: 995
		// (add) Token: 0x06002CED RID: 11501
		// (remove) Token: 0x06002CEE RID: 11502
		public virtual extern event HTMLImgEvents2_oncopyEventHandler HTMLImgEvents2_Event_oncopy;

		// Token: 0x140003E4 RID: 996
		// (add) Token: 0x06002CEF RID: 11503
		// (remove) Token: 0x06002CF0 RID: 11504
		public virtual extern event HTMLImgEvents2_onbeforepasteEventHandler HTMLImgEvents2_Event_onbeforepaste;

		// Token: 0x140003E5 RID: 997
		// (add) Token: 0x06002CF1 RID: 11505
		// (remove) Token: 0x06002CF2 RID: 11506
		public virtual extern event HTMLImgEvents2_onpasteEventHandler HTMLImgEvents2_Event_onpaste;

		// Token: 0x140003E6 RID: 998
		// (add) Token: 0x06002CF3 RID: 11507
		// (remove) Token: 0x06002CF4 RID: 11508
		public virtual extern event HTMLImgEvents2_oncontextmenuEventHandler HTMLImgEvents2_Event_oncontextmenu;

		// Token: 0x140003E7 RID: 999
		// (add) Token: 0x06002CF5 RID: 11509
		// (remove) Token: 0x06002CF6 RID: 11510
		public virtual extern event HTMLImgEvents2_onrowsdeleteEventHandler HTMLImgEvents2_Event_onrowsdelete;

		// Token: 0x140003E8 RID: 1000
		// (add) Token: 0x06002CF7 RID: 11511
		// (remove) Token: 0x06002CF8 RID: 11512
		public virtual extern event HTMLImgEvents2_onrowsinsertedEventHandler HTMLImgEvents2_Event_onrowsinserted;

		// Token: 0x140003E9 RID: 1001
		// (add) Token: 0x06002CF9 RID: 11513
		// (remove) Token: 0x06002CFA RID: 11514
		public virtual extern event HTMLImgEvents2_oncellchangeEventHandler HTMLImgEvents2_Event_oncellchange;

		// Token: 0x140003EA RID: 1002
		// (add) Token: 0x06002CFB RID: 11515
		// (remove) Token: 0x06002CFC RID: 11516
		public virtual extern event HTMLImgEvents2_onreadystatechangeEventHandler HTMLImgEvents2_Event_onreadystatechange;

		// Token: 0x140003EB RID: 1003
		// (add) Token: 0x06002CFD RID: 11517
		// (remove) Token: 0x06002CFE RID: 11518
		public virtual extern event HTMLImgEvents2_onlayoutcompleteEventHandler HTMLImgEvents2_Event_onlayoutcomplete;

		// Token: 0x140003EC RID: 1004
		// (add) Token: 0x06002CFF RID: 11519
		// (remove) Token: 0x06002D00 RID: 11520
		public virtual extern event HTMLImgEvents2_onpageEventHandler HTMLImgEvents2_Event_onpage;

		// Token: 0x140003ED RID: 1005
		// (add) Token: 0x06002D01 RID: 11521
		// (remove) Token: 0x06002D02 RID: 11522
		public virtual extern event HTMLImgEvents2_onmouseenterEventHandler HTMLImgEvents2_Event_onmouseenter;

		// Token: 0x140003EE RID: 1006
		// (add) Token: 0x06002D03 RID: 11523
		// (remove) Token: 0x06002D04 RID: 11524
		public virtual extern event HTMLImgEvents2_onmouseleaveEventHandler HTMLImgEvents2_Event_onmouseleave;

		// Token: 0x140003EF RID: 1007
		// (add) Token: 0x06002D05 RID: 11525
		// (remove) Token: 0x06002D06 RID: 11526
		public virtual extern event HTMLImgEvents2_onactivateEventHandler HTMLImgEvents2_Event_onactivate;

		// Token: 0x140003F0 RID: 1008
		// (add) Token: 0x06002D07 RID: 11527
		// (remove) Token: 0x06002D08 RID: 11528
		public virtual extern event HTMLImgEvents2_ondeactivateEventHandler HTMLImgEvents2_Event_ondeactivate;

		// Token: 0x140003F1 RID: 1009
		// (add) Token: 0x06002D09 RID: 11529
		// (remove) Token: 0x06002D0A RID: 11530
		public virtual extern event HTMLImgEvents2_onbeforedeactivateEventHandler HTMLImgEvents2_Event_onbeforedeactivate;

		// Token: 0x140003F2 RID: 1010
		// (add) Token: 0x06002D0B RID: 11531
		// (remove) Token: 0x06002D0C RID: 11532
		public virtual extern event HTMLImgEvents2_onbeforeactivateEventHandler HTMLImgEvents2_Event_onbeforeactivate;

		// Token: 0x140003F3 RID: 1011
		// (add) Token: 0x06002D0D RID: 11533
		// (remove) Token: 0x06002D0E RID: 11534
		public virtual extern event HTMLImgEvents2_onfocusinEventHandler HTMLImgEvents2_Event_onfocusin;

		// Token: 0x140003F4 RID: 1012
		// (add) Token: 0x06002D0F RID: 11535
		// (remove) Token: 0x06002D10 RID: 11536
		public virtual extern event HTMLImgEvents2_onfocusoutEventHandler HTMLImgEvents2_Event_onfocusout;

		// Token: 0x140003F5 RID: 1013
		// (add) Token: 0x06002D11 RID: 11537
		// (remove) Token: 0x06002D12 RID: 11538
		public virtual extern event HTMLImgEvents2_onmoveEventHandler HTMLImgEvents2_Event_onmove;

		// Token: 0x140003F6 RID: 1014
		// (add) Token: 0x06002D13 RID: 11539
		// (remove) Token: 0x06002D14 RID: 11540
		public virtual extern event HTMLImgEvents2_oncontrolselectEventHandler HTMLImgEvents2_Event_oncontrolselect;

		// Token: 0x140003F7 RID: 1015
		// (add) Token: 0x06002D15 RID: 11541
		// (remove) Token: 0x06002D16 RID: 11542
		public virtual extern event HTMLImgEvents2_onmovestartEventHandler HTMLImgEvents2_Event_onmovestart;

		// Token: 0x140003F8 RID: 1016
		// (add) Token: 0x06002D17 RID: 11543
		// (remove) Token: 0x06002D18 RID: 11544
		public virtual extern event HTMLImgEvents2_onmoveendEventHandler HTMLImgEvents2_Event_onmoveend;

		// Token: 0x140003F9 RID: 1017
		// (add) Token: 0x06002D19 RID: 11545
		// (remove) Token: 0x06002D1A RID: 11546
		public virtual extern event HTMLImgEvents2_onresizestartEventHandler HTMLImgEvents2_Event_onresizestart;

		// Token: 0x140003FA RID: 1018
		// (add) Token: 0x06002D1B RID: 11547
		// (remove) Token: 0x06002D1C RID: 11548
		public virtual extern event HTMLImgEvents2_onresizeendEventHandler HTMLImgEvents2_Event_onresizeend;

		// Token: 0x140003FB RID: 1019
		// (add) Token: 0x06002D1D RID: 11549
		// (remove) Token: 0x06002D1E RID: 11550
		public virtual extern event HTMLImgEvents2_onmousewheelEventHandler HTMLImgEvents2_Event_onmousewheel;

		// Token: 0x140003FC RID: 1020
		// (add) Token: 0x06002D1F RID: 11551
		// (remove) Token: 0x06002D20 RID: 11552
		public virtual extern event HTMLImgEvents2_onloadEventHandler HTMLImgEvents2_Event_onload;

		// Token: 0x140003FD RID: 1021
		// (add) Token: 0x06002D21 RID: 11553
		// (remove) Token: 0x06002D22 RID: 11554
		public virtual extern event HTMLImgEvents2_onerrorEventHandler HTMLImgEvents2_Event_onerror;

		// Token: 0x140003FE RID: 1022
		// (add) Token: 0x06002D23 RID: 11555
		// (remove) Token: 0x06002D24 RID: 11556
		public virtual extern event HTMLImgEvents2_onabortEventHandler HTMLImgEvents2_Event_onabort;
	}
}
