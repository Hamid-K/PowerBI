using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x0200078B RID: 1931
	[ComSourceInterfaces("mshtml.HTMLElementEvents\0mshtml.HTMLElementEvents2\0\0")]
	[Guid("3050F279-98B5-11CF-BB82-00AA00BDCE0B")]
	[TypeLibType(2)]
	[ClassInterface(0)]
	[ComImport]
	public class HTMLNextIdElementClass : DispHTMLNextIdElement, HTMLNextIdElement, HTMLElementEvents_Event, IHTMLElement, IHTMLElement2, IHTMLElement3, IHTMLElement4, IHTMLUniqueName, IHTMLDOMNode, IHTMLDOMNode2, IHTMLNextIdElement, HTMLElementEvents2_Event
	{
		// Token: 0x0600C940 RID: 51520
		[MethodImpl(4096, MethodCodeType = 3)]
		public extern HTMLNextIdElementClass();

		// Token: 0x0600C941 RID: 51521
		[DispId(-2147417611)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void setAttribute([MarshalAs(19)] [In] string strAttributeName, [MarshalAs(27)] [In] object AttributeValue, [In] int lFlags = 1);

		// Token: 0x0600C942 RID: 51522
		[DispId(-2147417610)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		public virtual extern object getAttribute([MarshalAs(19)] [In] string strAttributeName, [In] int lFlags = 0);

		// Token: 0x0600C943 RID: 51523
		[DispId(-2147417609)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool removeAttribute([MarshalAs(19)] [In] string strAttributeName, [In] int lFlags = 1);

		// Token: 0x1700421B RID: 16923
		// (get) Token: 0x0600C945 RID: 51525
		// (set) Token: 0x0600C944 RID: 51524
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

		// Token: 0x1700421C RID: 16924
		// (get) Token: 0x0600C947 RID: 51527
		// (set) Token: 0x0600C946 RID: 51526
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

		// Token: 0x1700421D RID: 16925
		// (get) Token: 0x0600C948 RID: 51528
		[DispId(-2147417108)]
		public virtual extern string tagName
		{
			[DispId(-2147417108)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x1700421E RID: 16926
		// (get) Token: 0x0600C949 RID: 51529
		[DispId(-2147418104)]
		public virtual extern IHTMLElement parentElement
		{
			[DispId(-2147418104)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x1700421F RID: 16927
		// (get) Token: 0x0600C94A RID: 51530
		[DispId(-2147418038)]
		public virtual extern IHTMLStyle style
		{
			[DispId(-2147418038)]
			[TypeLibFunc(1024)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17004220 RID: 16928
		// (get) Token: 0x0600C94C RID: 51532
		// (set) Token: 0x0600C94B RID: 51531
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

		// Token: 0x17004221 RID: 16929
		// (get) Token: 0x0600C94E RID: 51534
		// (set) Token: 0x0600C94D RID: 51533
		[DispId(-2147412104)]
		public virtual extern object onclick
		{
			[TypeLibFunc(20)]
			[DispId(-2147412104)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412104)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17004222 RID: 16930
		// (get) Token: 0x0600C950 RID: 51536
		// (set) Token: 0x0600C94F RID: 51535
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

		// Token: 0x17004223 RID: 16931
		// (get) Token: 0x0600C952 RID: 51538
		// (set) Token: 0x0600C951 RID: 51537
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

		// Token: 0x17004224 RID: 16932
		// (get) Token: 0x0600C954 RID: 51540
		// (set) Token: 0x0600C953 RID: 51539
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

		// Token: 0x17004225 RID: 16933
		// (get) Token: 0x0600C956 RID: 51542
		// (set) Token: 0x0600C955 RID: 51541
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

		// Token: 0x17004226 RID: 16934
		// (get) Token: 0x0600C958 RID: 51544
		// (set) Token: 0x0600C957 RID: 51543
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

		// Token: 0x17004227 RID: 16935
		// (get) Token: 0x0600C95A RID: 51546
		// (set) Token: 0x0600C959 RID: 51545
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

		// Token: 0x17004228 RID: 16936
		// (get) Token: 0x0600C95C RID: 51548
		// (set) Token: 0x0600C95B RID: 51547
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

		// Token: 0x17004229 RID: 16937
		// (get) Token: 0x0600C95E RID: 51550
		// (set) Token: 0x0600C95D RID: 51549
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

		// Token: 0x1700422A RID: 16938
		// (get) Token: 0x0600C960 RID: 51552
		// (set) Token: 0x0600C95F RID: 51551
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

		// Token: 0x1700422B RID: 16939
		// (get) Token: 0x0600C961 RID: 51553
		[DispId(-2147417094)]
		public virtual extern object document
		{
			[DispId(-2147417094)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x1700422C RID: 16940
		// (get) Token: 0x0600C963 RID: 51555
		// (set) Token: 0x0600C962 RID: 51554
		[DispId(-2147418043)]
		public virtual extern string title
		{
			[TypeLibFunc(20)]
			[DispId(-2147418043)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147418043)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x1700422D RID: 16941
		// (get) Token: 0x0600C965 RID: 51557
		// (set) Token: 0x0600C964 RID: 51556
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

		// Token: 0x1700422E RID: 16942
		// (get) Token: 0x0600C967 RID: 51559
		// (set) Token: 0x0600C966 RID: 51558
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

		// Token: 0x0600C968 RID: 51560
		[DispId(-2147417093)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void scrollIntoView([MarshalAs(27)] [In] [Optional] object varargStart);

		// Token: 0x0600C969 RID: 51561
		[DispId(-2147417092)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool contains([MarshalAs(28)] [In] IHTMLElement pChild);

		// Token: 0x1700422F RID: 16943
		// (get) Token: 0x0600C96A RID: 51562
		[DispId(-2147417088)]
		public virtual extern int sourceIndex
		{
			[DispId(-2147417088)]
			[TypeLibFunc(4)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17004230 RID: 16944
		// (get) Token: 0x0600C96B RID: 51563
		[DispId(-2147417087)]
		public virtual extern object recordNumber
		{
			[DispId(-2147417087)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x17004231 RID: 16945
		// (get) Token: 0x0600C96D RID: 51565
		// (set) Token: 0x0600C96C RID: 51564
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

		// Token: 0x17004232 RID: 16946
		// (get) Token: 0x0600C96E RID: 51566
		[DispId(-2147417104)]
		public virtual extern int offsetLeft
		{
			[DispId(-2147417104)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17004233 RID: 16947
		// (get) Token: 0x0600C96F RID: 51567
		[DispId(-2147417103)]
		public virtual extern int offsetTop
		{
			[DispId(-2147417103)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17004234 RID: 16948
		// (get) Token: 0x0600C970 RID: 51568
		[DispId(-2147417102)]
		public virtual extern int offsetWidth
		{
			[DispId(-2147417102)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17004235 RID: 16949
		// (get) Token: 0x0600C971 RID: 51569
		[DispId(-2147417101)]
		public virtual extern int offsetHeight
		{
			[DispId(-2147417101)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17004236 RID: 16950
		// (get) Token: 0x0600C972 RID: 51570
		[DispId(-2147417100)]
		public virtual extern IHTMLElement offsetParent
		{
			[DispId(-2147417100)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17004237 RID: 16951
		// (get) Token: 0x0600C974 RID: 51572
		// (set) Token: 0x0600C973 RID: 51571
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

		// Token: 0x17004238 RID: 16952
		// (get) Token: 0x0600C976 RID: 51574
		// (set) Token: 0x0600C975 RID: 51573
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

		// Token: 0x17004239 RID: 16953
		// (get) Token: 0x0600C978 RID: 51576
		// (set) Token: 0x0600C977 RID: 51575
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

		// Token: 0x1700423A RID: 16954
		// (get) Token: 0x0600C97A RID: 51578
		// (set) Token: 0x0600C979 RID: 51577
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

		// Token: 0x0600C97B RID: 51579
		[DispId(-2147417082)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void insertAdjacentHTML([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string html);

		// Token: 0x0600C97C RID: 51580
		[DispId(-2147417081)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void insertAdjacentText([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string text);

		// Token: 0x1700423B RID: 16955
		// (get) Token: 0x0600C97D RID: 51581
		[DispId(-2147417080)]
		public virtual extern IHTMLElement parentTextEdit
		{
			[DispId(-2147417080)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x1700423C RID: 16956
		// (get) Token: 0x0600C97E RID: 51582
		[DispId(-2147417078)]
		public virtual extern bool isTextEdit
		{
			[DispId(-2147417078)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x0600C97F RID: 51583
		[DispId(-2147417079)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void click();

		// Token: 0x1700423D RID: 16957
		// (get) Token: 0x0600C980 RID: 51584
		[DispId(-2147417077)]
		public virtual extern IHTMLFiltersCollection filters
		{
			[DispId(-2147417077)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x1700423E RID: 16958
		// (get) Token: 0x0600C982 RID: 51586
		// (set) Token: 0x0600C981 RID: 51585
		[DispId(-2147412077)]
		public virtual extern object ondragstart
		{
			[DispId(-2147412077)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412077)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x0600C983 RID: 51587
		[DispId(-2147417076)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string toString();

		// Token: 0x1700423F RID: 16959
		// (get) Token: 0x0600C985 RID: 51589
		// (set) Token: 0x0600C984 RID: 51588
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

		// Token: 0x17004240 RID: 16960
		// (get) Token: 0x0600C987 RID: 51591
		// (set) Token: 0x0600C986 RID: 51590
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

		// Token: 0x17004241 RID: 16961
		// (get) Token: 0x0600C989 RID: 51593
		// (set) Token: 0x0600C988 RID: 51592
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

		// Token: 0x17004242 RID: 16962
		// (get) Token: 0x0600C98B RID: 51595
		// (set) Token: 0x0600C98A RID: 51594
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

		// Token: 0x17004243 RID: 16963
		// (get) Token: 0x0600C98D RID: 51597
		// (set) Token: 0x0600C98C RID: 51596
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

		// Token: 0x17004244 RID: 16964
		// (get) Token: 0x0600C98F RID: 51599
		// (set) Token: 0x0600C98E RID: 51598
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

		// Token: 0x17004245 RID: 16965
		// (get) Token: 0x0600C991 RID: 51601
		// (set) Token: 0x0600C990 RID: 51600
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

		// Token: 0x17004246 RID: 16966
		// (get) Token: 0x0600C993 RID: 51603
		// (set) Token: 0x0600C992 RID: 51602
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

		// Token: 0x17004247 RID: 16967
		// (get) Token: 0x0600C995 RID: 51605
		// (set) Token: 0x0600C994 RID: 51604
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

		// Token: 0x17004248 RID: 16968
		// (get) Token: 0x0600C996 RID: 51606
		[DispId(-2147417075)]
		public virtual extern object children
		{
			[DispId(-2147417075)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17004249 RID: 16969
		// (get) Token: 0x0600C997 RID: 51607
		[DispId(-2147417074)]
		public virtual extern object all
		{
			[DispId(-2147417074)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x1700424A RID: 16970
		// (get) Token: 0x0600C998 RID: 51608
		[DispId(-2147417073)]
		public virtual extern string scopeName
		{
			[DispId(-2147417073)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x0600C999 RID: 51609
		[DispId(-2147417072)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void setCapture([In] bool containerCapture = true);

		// Token: 0x0600C99A RID: 51610
		[DispId(-2147417071)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void releaseCapture();

		// Token: 0x1700424B RID: 16971
		// (get) Token: 0x0600C99C RID: 51612
		// (set) Token: 0x0600C99B RID: 51611
		[DispId(-2147412066)]
		public virtual extern object onlosecapture
		{
			[TypeLibFunc(20)]
			[DispId(-2147412066)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412066)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x0600C99D RID: 51613
		[DispId(-2147417070)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string componentFromPoint([In] int x, [In] int y);

		// Token: 0x0600C99E RID: 51614
		[DispId(-2147417069)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void doScroll([MarshalAs(27)] [In] [Optional] object component);

		// Token: 0x1700424C RID: 16972
		// (get) Token: 0x0600C9A0 RID: 51616
		// (set) Token: 0x0600C99F RID: 51615
		[DispId(-2147412081)]
		public virtual extern object onscroll
		{
			[TypeLibFunc(20)]
			[DispId(-2147412081)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412081)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x1700424D RID: 16973
		// (get) Token: 0x0600C9A2 RID: 51618
		// (set) Token: 0x0600C9A1 RID: 51617
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

		// Token: 0x1700424E RID: 16974
		// (get) Token: 0x0600C9A4 RID: 51620
		// (set) Token: 0x0600C9A3 RID: 51619
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

		// Token: 0x1700424F RID: 16975
		// (get) Token: 0x0600C9A6 RID: 51622
		// (set) Token: 0x0600C9A5 RID: 51621
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

		// Token: 0x17004250 RID: 16976
		// (get) Token: 0x0600C9A8 RID: 51624
		// (set) Token: 0x0600C9A7 RID: 51623
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

		// Token: 0x17004251 RID: 16977
		// (get) Token: 0x0600C9AA RID: 51626
		// (set) Token: 0x0600C9A9 RID: 51625
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

		// Token: 0x17004252 RID: 16978
		// (get) Token: 0x0600C9AC RID: 51628
		// (set) Token: 0x0600C9AB RID: 51627
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

		// Token: 0x17004253 RID: 16979
		// (get) Token: 0x0600C9AE RID: 51630
		// (set) Token: 0x0600C9AD RID: 51629
		[DispId(-2147412054)]
		public virtual extern object onbeforecut
		{
			[TypeLibFunc(20)]
			[DispId(-2147412054)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412054)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17004254 RID: 16980
		// (get) Token: 0x0600C9B0 RID: 51632
		// (set) Token: 0x0600C9AF RID: 51631
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

		// Token: 0x17004255 RID: 16981
		// (get) Token: 0x0600C9B2 RID: 51634
		// (set) Token: 0x0600C9B1 RID: 51633
		[DispId(-2147412053)]
		public virtual extern object onbeforecopy
		{
			[DispId(-2147412053)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412053)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17004256 RID: 16982
		// (get) Token: 0x0600C9B4 RID: 51636
		// (set) Token: 0x0600C9B3 RID: 51635
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

		// Token: 0x17004257 RID: 16983
		// (get) Token: 0x0600C9B6 RID: 51638
		// (set) Token: 0x0600C9B5 RID: 51637
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

		// Token: 0x17004258 RID: 16984
		// (get) Token: 0x0600C9B8 RID: 51640
		// (set) Token: 0x0600C9B7 RID: 51639
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

		// Token: 0x17004259 RID: 16985
		// (get) Token: 0x0600C9B9 RID: 51641
		[DispId(-2147417105)]
		public virtual extern IHTMLCurrentStyle currentStyle
		{
			[DispId(-2147417105)]
			[TypeLibFunc(1024)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x1700425A RID: 16986
		// (get) Token: 0x0600C9BB RID: 51643
		// (set) Token: 0x0600C9BA RID: 51642
		[DispId(-2147412065)]
		public virtual extern object onpropertychange
		{
			[TypeLibFunc(20)]
			[DispId(-2147412065)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412065)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x0600C9BC RID: 51644
		[DispId(-2147417068)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLRectCollection getClientRects();

		// Token: 0x0600C9BD RID: 51645
		[DispId(-2147417067)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLRect getBoundingClientRect();

		// Token: 0x0600C9BE RID: 51646
		[DispId(-2147417608)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void setExpression([MarshalAs(19)] [In] string propname, [MarshalAs(19)] [In] string expression, [MarshalAs(19)] [In] string language = "");

		// Token: 0x0600C9BF RID: 51647
		[DispId(-2147417607)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		public virtual extern object getExpression([MarshalAs(19)] [In] string propname);

		// Token: 0x0600C9C0 RID: 51648
		[DispId(-2147417606)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool removeExpression([MarshalAs(19)] [In] string propname);

		// Token: 0x1700425B RID: 16987
		// (get) Token: 0x0600C9C2 RID: 51650
		// (set) Token: 0x0600C9C1 RID: 51649
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

		// Token: 0x0600C9C3 RID: 51651
		[DispId(-2147416112)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void focus();

		// Token: 0x1700425C RID: 16988
		// (get) Token: 0x0600C9C5 RID: 51653
		// (set) Token: 0x0600C9C4 RID: 51652
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

		// Token: 0x1700425D RID: 16989
		// (get) Token: 0x0600C9C7 RID: 51655
		// (set) Token: 0x0600C9C6 RID: 51654
		[DispId(-2147412097)]
		public virtual extern object onblur
		{
			[TypeLibFunc(20)]
			[DispId(-2147412097)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412097)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x1700425E RID: 16990
		// (get) Token: 0x0600C9C9 RID: 51657
		// (set) Token: 0x0600C9C8 RID: 51656
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

		// Token: 0x1700425F RID: 16991
		// (get) Token: 0x0600C9CB RID: 51659
		// (set) Token: 0x0600C9CA RID: 51658
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

		// Token: 0x0600C9CC RID: 51660
		[DispId(-2147416110)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void blur();

		// Token: 0x0600C9CD RID: 51661
		[DispId(-2147416095)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void addFilter([MarshalAs(25)] [In] object pUnk);

		// Token: 0x0600C9CE RID: 51662
		[DispId(-2147416094)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void removeFilter([MarshalAs(25)] [In] object pUnk);

		// Token: 0x17004260 RID: 16992
		// (get) Token: 0x0600C9CF RID: 51663
		[DispId(-2147416093)]
		public virtual extern int clientHeight
		{
			[TypeLibFunc(20)]
			[DispId(-2147416093)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17004261 RID: 16993
		// (get) Token: 0x0600C9D0 RID: 51664
		[DispId(-2147416092)]
		public virtual extern int clientWidth
		{
			[TypeLibFunc(20)]
			[DispId(-2147416092)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17004262 RID: 16994
		// (get) Token: 0x0600C9D1 RID: 51665
		[DispId(-2147416091)]
		public virtual extern int clientTop
		{
			[DispId(-2147416091)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17004263 RID: 16995
		// (get) Token: 0x0600C9D2 RID: 51666
		[DispId(-2147416090)]
		public virtual extern int clientLeft
		{
			[TypeLibFunc(20)]
			[DispId(-2147416090)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x0600C9D3 RID: 51667
		[DispId(-2147417605)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool attachEvent([MarshalAs(19)] [In] string @event, [MarshalAs(26)] [In] object pdisp);

		// Token: 0x0600C9D4 RID: 51668
		[DispId(-2147417604)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void detachEvent([MarshalAs(19)] [In] string @event, [MarshalAs(26)] [In] object pdisp);

		// Token: 0x17004264 RID: 16996
		// (get) Token: 0x0600C9D5 RID: 51669
		[DispId(-2147412996)]
		public virtual extern object readyState
		{
			[DispId(-2147412996)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x17004265 RID: 16997
		// (get) Token: 0x0600C9D7 RID: 51671
		// (set) Token: 0x0600C9D6 RID: 51670
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

		// Token: 0x17004266 RID: 16998
		// (get) Token: 0x0600C9D9 RID: 51673
		// (set) Token: 0x0600C9D8 RID: 51672
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

		// Token: 0x17004267 RID: 16999
		// (get) Token: 0x0600C9DB RID: 51675
		// (set) Token: 0x0600C9DA RID: 51674
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

		// Token: 0x17004268 RID: 17000
		// (get) Token: 0x0600C9DD RID: 51677
		// (set) Token: 0x0600C9DC RID: 51676
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

		// Token: 0x17004269 RID: 17001
		// (get) Token: 0x0600C9DF RID: 51679
		// (set) Token: 0x0600C9DE RID: 51678
		[DispId(-2147412995)]
		public virtual extern string dir
		{
			[DispId(-2147412995)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412995)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x0600C9E0 RID: 51680
		[DispId(-2147417056)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(26)]
		public virtual extern object createControlRange();

		// Token: 0x1700426A RID: 17002
		// (get) Token: 0x0600C9E1 RID: 51681
		[DispId(-2147417055)]
		public virtual extern int scrollHeight
		{
			[DispId(-2147417055)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700426B RID: 17003
		// (get) Token: 0x0600C9E2 RID: 51682
		[DispId(-2147417054)]
		public virtual extern int scrollWidth
		{
			[DispId(-2147417054)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700426C RID: 17004
		// (get) Token: 0x0600C9E4 RID: 51684
		// (set) Token: 0x0600C9E3 RID: 51683
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

		// Token: 0x1700426D RID: 17005
		// (get) Token: 0x0600C9E6 RID: 51686
		// (set) Token: 0x0600C9E5 RID: 51685
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

		// Token: 0x0600C9E7 RID: 51687
		[DispId(-2147417050)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void clearAttributes();

		// Token: 0x1700426E RID: 17006
		// (get) Token: 0x0600C9E9 RID: 51689
		// (set) Token: 0x0600C9E8 RID: 51688
		[DispId(-2147412047)]
		public virtual extern object oncontextmenu
		{
			[DispId(-2147412047)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412047)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x0600C9EA RID: 51690
		[DispId(-2147417043)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElement insertAdjacentElement([MarshalAs(19)] [In] string where, [MarshalAs(28)] [In] IHTMLElement insertedElement);

		// Token: 0x0600C9EB RID: 51691
		[DispId(-2147417047)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElement applyElement([MarshalAs(28)] [In] IHTMLElement apply, [MarshalAs(19)] [In] string where);

		// Token: 0x0600C9EC RID: 51692
		[DispId(-2147417042)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string getAdjacentText([MarshalAs(19)] [In] string where);

		// Token: 0x0600C9ED RID: 51693
		[DispId(-2147417041)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string replaceAdjacentText([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string newText);

		// Token: 0x1700426F RID: 17007
		// (get) Token: 0x0600C9EE RID: 51694
		[DispId(-2147417040)]
		public virtual extern bool canHaveChildren
		{
			[DispId(-2147417040)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x0600C9EF RID: 51695
		[DispId(-2147417032)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern int addBehavior([MarshalAs(19)] [In] string bstrUrl, [MarshalAs(27)] [In] [Optional] ref object pvarFactory);

		// Token: 0x0600C9F0 RID: 51696
		[DispId(-2147417031)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool removeBehavior([In] int cookie);

		// Token: 0x17004270 RID: 17008
		// (get) Token: 0x0600C9F1 RID: 51697
		[DispId(-2147417048)]
		public virtual extern IHTMLStyle runtimeStyle
		{
			[DispId(-2147417048)]
			[TypeLibFunc(1024)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17004271 RID: 17009
		// (get) Token: 0x0600C9F2 RID: 51698
		[DispId(-2147417030)]
		public virtual extern object behaviorUrns
		{
			[DispId(-2147417030)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17004272 RID: 17010
		// (get) Token: 0x0600C9F4 RID: 51700
		// (set) Token: 0x0600C9F3 RID: 51699
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

		// Token: 0x17004273 RID: 17011
		// (get) Token: 0x0600C9F6 RID: 51702
		// (set) Token: 0x0600C9F5 RID: 51701
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

		// Token: 0x17004274 RID: 17012
		// (get) Token: 0x0600C9F7 RID: 51703
		[DispId(-2147417028)]
		public virtual extern int readyStateValue
		{
			[DispId(-2147417028)]
			[TypeLibFunc(65)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x0600C9F8 RID: 51704
		[DispId(-2147417027)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElementCollection getElementsByTagName([MarshalAs(19)] [In] string v);

		// Token: 0x0600C9F9 RID: 51705
		[DispId(-2147417016)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void mergeAttributes([MarshalAs(28)] [In] IHTMLElement mergeThis, [MarshalAs(27)] [In] [Optional] ref object pvarFlags);

		// Token: 0x17004275 RID: 17013
		// (get) Token: 0x0600C9FA RID: 51706
		[DispId(-2147417015)]
		public virtual extern bool isMultiLine
		{
			[DispId(-2147417015)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17004276 RID: 17014
		// (get) Token: 0x0600C9FB RID: 51707
		[DispId(-2147417014)]
		public virtual extern bool canHaveHTML
		{
			[DispId(-2147417014)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17004277 RID: 17015
		// (get) Token: 0x0600C9FD RID: 51709
		// (set) Token: 0x0600C9FC RID: 51708
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

		// Token: 0x17004278 RID: 17016
		// (get) Token: 0x0600C9FF RID: 51711
		// (set) Token: 0x0600C9FE RID: 51710
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

		// Token: 0x17004279 RID: 17017
		// (get) Token: 0x0600CA01 RID: 51713
		// (set) Token: 0x0600CA00 RID: 51712
		[DispId(-2147417012)]
		public virtual extern bool inflateBlock
		{
			[TypeLibFunc(1089)]
			[DispId(-2147417012)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[DispId(-2147417012)]
			[TypeLibFunc(1089)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x1700427A RID: 17018
		// (get) Token: 0x0600CA03 RID: 51715
		// (set) Token: 0x0600CA02 RID: 51714
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

		// Token: 0x0600CA04 RID: 51716
		[DispId(-2147417011)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void setActive();

		// Token: 0x1700427B RID: 17019
		// (get) Token: 0x0600CA06 RID: 51718
		// (set) Token: 0x0600CA05 RID: 51717
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

		// Token: 0x1700427C RID: 17020
		// (get) Token: 0x0600CA07 RID: 51719
		[DispId(-2147417010)]
		public virtual extern bool isContentEditable
		{
			[DispId(-2147417010)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700427D RID: 17021
		// (get) Token: 0x0600CA09 RID: 51721
		// (set) Token: 0x0600CA08 RID: 51720
		[DispId(-2147412949)]
		public virtual extern bool hideFocus
		{
			[TypeLibFunc(20)]
			[DispId(-2147412949)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[DispId(-2147412949)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x1700427E RID: 17022
		// (get) Token: 0x0600CA0B RID: 51723
		// (set) Token: 0x0600CA0A RID: 51722
		[DispId(-2147418036)]
		public virtual extern bool disabled
		{
			[DispId(-2147418036)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147418036)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x1700427F RID: 17023
		// (get) Token: 0x0600CA0C RID: 51724
		[DispId(-2147417007)]
		public virtual extern bool isDisabled
		{
			[DispId(-2147417007)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17004280 RID: 17024
		// (get) Token: 0x0600CA0E RID: 51726
		// (set) Token: 0x0600CA0D RID: 51725
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

		// Token: 0x17004281 RID: 17025
		// (get) Token: 0x0600CA10 RID: 51728
		// (set) Token: 0x0600CA0F RID: 51727
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

		// Token: 0x0600CA11 RID: 51729
		[DispId(-2147417006)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool FireEvent([MarshalAs(19)] [In] string bstrEventName, [MarshalAs(27)] [In] [Optional] ref object pvarEventObject);

		// Token: 0x17004282 RID: 17026
		// (get) Token: 0x0600CA13 RID: 51731
		// (set) Token: 0x0600CA12 RID: 51730
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

		// Token: 0x17004283 RID: 17027
		// (get) Token: 0x0600CA15 RID: 51733
		// (set) Token: 0x0600CA14 RID: 51732
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

		// Token: 0x17004284 RID: 17028
		// (get) Token: 0x0600CA17 RID: 51735
		// (set) Token: 0x0600CA16 RID: 51734
		[DispId(-2147412031)]
		public virtual extern object onmovestart
		{
			[TypeLibFunc(20)]
			[DispId(-2147412031)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412031)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17004285 RID: 17029
		// (get) Token: 0x0600CA19 RID: 51737
		// (set) Token: 0x0600CA18 RID: 51736
		[DispId(-2147412030)]
		public virtual extern object onmoveend
		{
			[TypeLibFunc(20)]
			[DispId(-2147412030)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412030)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17004286 RID: 17030
		// (get) Token: 0x0600CA1B RID: 51739
		// (set) Token: 0x0600CA1A RID: 51738
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

		// Token: 0x17004287 RID: 17031
		// (get) Token: 0x0600CA1D RID: 51741
		// (set) Token: 0x0600CA1C RID: 51740
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

		// Token: 0x17004288 RID: 17032
		// (get) Token: 0x0600CA1F RID: 51743
		// (set) Token: 0x0600CA1E RID: 51742
		[DispId(-2147412025)]
		public virtual extern object onactivate
		{
			[TypeLibFunc(20)]
			[DispId(-2147412025)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412025)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17004289 RID: 17033
		// (get) Token: 0x0600CA21 RID: 51745
		// (set) Token: 0x0600CA20 RID: 51744
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

		// Token: 0x0600CA22 RID: 51746
		[DispId(-2147417005)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool dragDrop();

		// Token: 0x1700428A RID: 17034
		// (get) Token: 0x0600CA23 RID: 51747
		[DispId(-2147417004)]
		public virtual extern int glyphMode
		{
			[TypeLibFunc(1089)]
			[DispId(-2147417004)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700428B RID: 17035
		// (get) Token: 0x0600CA25 RID: 51749
		// (set) Token: 0x0600CA24 RID: 51748
		[DispId(-2147412036)]
		public virtual extern object onmousewheel
		{
			[DispId(-2147412036)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412036)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x0600CA26 RID: 51750
		[DispId(-2147417000)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void normalize();

		// Token: 0x0600CA27 RID: 51751
		[DispId(-2147417003)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute getAttributeNode([MarshalAs(19)] [In] string bstrName);

		// Token: 0x0600CA28 RID: 51752
		[DispId(-2147417002)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute setAttributeNode([MarshalAs(28)] [In] IHTMLDOMAttribute pattr);

		// Token: 0x0600CA29 RID: 51753
		[DispId(-2147417001)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute removeAttributeNode([MarshalAs(28)] [In] IHTMLDOMAttribute pattr);

		// Token: 0x1700428C RID: 17036
		// (get) Token: 0x0600CA2B RID: 51755
		// (set) Token: 0x0600CA2A RID: 51754
		[DispId(-2147412022)]
		public virtual extern object onbeforeactivate
		{
			[TypeLibFunc(20)]
			[DispId(-2147412022)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412022)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x1700428D RID: 17037
		// (get) Token: 0x0600CA2D RID: 51757
		// (set) Token: 0x0600CA2C RID: 51756
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

		// Token: 0x1700428E RID: 17038
		// (get) Token: 0x0600CA2F RID: 51759
		// (set) Token: 0x0600CA2E RID: 51758
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

		// Token: 0x1700428F RID: 17039
		// (get) Token: 0x0600CA30 RID: 51760
		[DispId(-2147417058)]
		public virtual extern int uniqueNumber
		{
			[TypeLibFunc(64)]
			[DispId(-2147417058)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17004290 RID: 17040
		// (get) Token: 0x0600CA31 RID: 51761
		[DispId(-2147417057)]
		public virtual extern string uniqueID
		{
			[DispId(-2147417057)]
			[TypeLibFunc(64)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17004291 RID: 17041
		// (get) Token: 0x0600CA32 RID: 51762
		[DispId(-2147417066)]
		public virtual extern int nodeType
		{
			[DispId(-2147417066)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17004292 RID: 17042
		// (get) Token: 0x0600CA33 RID: 51763
		[DispId(-2147417065)]
		public virtual extern IHTMLDOMNode parentNode
		{
			[DispId(-2147417065)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x0600CA34 RID: 51764
		[DispId(-2147417064)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool hasChildNodes();

		// Token: 0x17004293 RID: 17043
		// (get) Token: 0x0600CA35 RID: 51765
		[DispId(-2147417063)]
		public virtual extern object childNodes
		{
			[DispId(-2147417063)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17004294 RID: 17044
		// (get) Token: 0x0600CA36 RID: 51766
		[DispId(-2147417062)]
		public virtual extern object attributes
		{
			[DispId(-2147417062)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x0600CA37 RID: 51767
		[DispId(-2147417061)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode insertBefore([MarshalAs(28)] [In] IHTMLDOMNode newChild, [MarshalAs(27)] [In] [Optional] object refChild);

		// Token: 0x0600CA38 RID: 51768
		[DispId(-2147417060)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode removeChild([MarshalAs(28)] [In] IHTMLDOMNode oldChild);

		// Token: 0x0600CA39 RID: 51769
		[DispId(-2147417059)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode replaceChild([MarshalAs(28)] [In] IHTMLDOMNode newChild, [MarshalAs(28)] [In] IHTMLDOMNode oldChild);

		// Token: 0x0600CA3A RID: 51770
		[DispId(-2147417051)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode cloneNode([In] bool fDeep);

		// Token: 0x0600CA3B RID: 51771
		[DispId(-2147417046)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode removeNode([In] bool fDeep = false);

		// Token: 0x0600CA3C RID: 51772
		[DispId(-2147417044)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode swapNode([MarshalAs(28)] [In] IHTMLDOMNode otherNode);

		// Token: 0x0600CA3D RID: 51773
		[DispId(-2147417045)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode replaceNode([MarshalAs(28)] [In] IHTMLDOMNode replacement);

		// Token: 0x0600CA3E RID: 51774
		[DispId(-2147417039)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode appendChild([MarshalAs(28)] [In] IHTMLDOMNode newChild);

		// Token: 0x17004295 RID: 17045
		// (get) Token: 0x0600CA3F RID: 51775
		[DispId(-2147417038)]
		public virtual extern string nodeName
		{
			[DispId(-2147417038)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17004296 RID: 17046
		// (get) Token: 0x0600CA41 RID: 51777
		// (set) Token: 0x0600CA40 RID: 51776
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

		// Token: 0x17004297 RID: 17047
		// (get) Token: 0x0600CA42 RID: 51778
		[DispId(-2147417036)]
		public virtual extern IHTMLDOMNode firstChild
		{
			[DispId(-2147417036)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17004298 RID: 17048
		// (get) Token: 0x0600CA43 RID: 51779
		[DispId(-2147417035)]
		public virtual extern IHTMLDOMNode lastChild
		{
			[DispId(-2147417035)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17004299 RID: 17049
		// (get) Token: 0x0600CA44 RID: 51780
		[DispId(-2147417034)]
		public virtual extern IHTMLDOMNode previousSibling
		{
			[DispId(-2147417034)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x1700429A RID: 17050
		// (get) Token: 0x0600CA45 RID: 51781
		[DispId(-2147417033)]
		public virtual extern IHTMLDOMNode nextSibling
		{
			[DispId(-2147417033)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x1700429B RID: 17051
		// (get) Token: 0x0600CA46 RID: 51782
		[DispId(-2147416999)]
		public virtual extern object ownerDocument
		{
			[DispId(-2147416999)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x1700429C RID: 17052
		// (get) Token: 0x0600CA48 RID: 51784
		// (set) Token: 0x0600CA47 RID: 51783
		[DispId(1012)]
		public virtual extern string n
		{
			[DispId(1012)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(1012)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x0600CA49 RID: 51785
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement_setAttribute([MarshalAs(19)] [In] string strAttributeName, [MarshalAs(27)] [In] object AttributeValue, [In] int lFlags = 1);

		// Token: 0x0600CA4A RID: 51786
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		public virtual extern object IHTMLElement_getAttribute([MarshalAs(19)] [In] string strAttributeName, [In] int lFlags = 0);

		// Token: 0x0600CA4B RID: 51787
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement_removeAttribute([MarshalAs(19)] [In] string strAttributeName, [In] int lFlags = 1);

		// Token: 0x1700429D RID: 17053
		// (get) Token: 0x0600CA4D RID: 51789
		// (set) Token: 0x0600CA4C RID: 51788
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

		// Token: 0x1700429E RID: 17054
		// (get) Token: 0x0600CA4F RID: 51791
		// (set) Token: 0x0600CA4E RID: 51790
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

		// Token: 0x1700429F RID: 17055
		// (get) Token: 0x0600CA50 RID: 51792
		public virtual extern string IHTMLElement_tagName
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170042A0 RID: 17056
		// (get) Token: 0x0600CA51 RID: 51793
		public virtual extern IHTMLElement IHTMLElement_parentElement
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170042A1 RID: 17057
		// (get) Token: 0x0600CA52 RID: 51794
		public virtual extern IHTMLStyle IHTMLElement_style
		{
			[TypeLibFunc(1024)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170042A2 RID: 17058
		// (get) Token: 0x0600CA54 RID: 51796
		// (set) Token: 0x0600CA53 RID: 51795
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

		// Token: 0x170042A3 RID: 17059
		// (get) Token: 0x0600CA56 RID: 51798
		// (set) Token: 0x0600CA55 RID: 51797
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

		// Token: 0x170042A4 RID: 17060
		// (get) Token: 0x0600CA58 RID: 51800
		// (set) Token: 0x0600CA57 RID: 51799
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

		// Token: 0x170042A5 RID: 17061
		// (get) Token: 0x0600CA5A RID: 51802
		// (set) Token: 0x0600CA59 RID: 51801
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

		// Token: 0x170042A6 RID: 17062
		// (get) Token: 0x0600CA5C RID: 51804
		// (set) Token: 0x0600CA5B RID: 51803
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

		// Token: 0x170042A7 RID: 17063
		// (get) Token: 0x0600CA5E RID: 51806
		// (set) Token: 0x0600CA5D RID: 51805
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

		// Token: 0x170042A8 RID: 17064
		// (get) Token: 0x0600CA60 RID: 51808
		// (set) Token: 0x0600CA5F RID: 51807
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

		// Token: 0x170042A9 RID: 17065
		// (get) Token: 0x0600CA62 RID: 51810
		// (set) Token: 0x0600CA61 RID: 51809
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

		// Token: 0x170042AA RID: 17066
		// (get) Token: 0x0600CA64 RID: 51812
		// (set) Token: 0x0600CA63 RID: 51811
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

		// Token: 0x170042AB RID: 17067
		// (get) Token: 0x0600CA66 RID: 51814
		// (set) Token: 0x0600CA65 RID: 51813
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

		// Token: 0x170042AC RID: 17068
		// (get) Token: 0x0600CA68 RID: 51816
		// (set) Token: 0x0600CA67 RID: 51815
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

		// Token: 0x170042AD RID: 17069
		// (get) Token: 0x0600CA69 RID: 51817
		public virtual extern object IHTMLElement_document
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x170042AE RID: 17070
		// (get) Token: 0x0600CA6B RID: 51819
		// (set) Token: 0x0600CA6A RID: 51818
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

		// Token: 0x170042AF RID: 17071
		// (get) Token: 0x0600CA6D RID: 51821
		// (set) Token: 0x0600CA6C RID: 51820
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

		// Token: 0x170042B0 RID: 17072
		// (get) Token: 0x0600CA6F RID: 51823
		// (set) Token: 0x0600CA6E RID: 51822
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

		// Token: 0x0600CA70 RID: 51824
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement_scrollIntoView([MarshalAs(27)] [In] [Optional] object varargStart);

		// Token: 0x0600CA71 RID: 51825
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement_contains([MarshalAs(28)] [In] IHTMLElement pChild);

		// Token: 0x170042B1 RID: 17073
		// (get) Token: 0x0600CA72 RID: 51826
		public virtual extern int IHTMLElement_sourceIndex
		{
			[TypeLibFunc(4)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170042B2 RID: 17074
		// (get) Token: 0x0600CA73 RID: 51827
		public virtual extern object IHTMLElement_recordNumber
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x170042B3 RID: 17075
		// (get) Token: 0x0600CA75 RID: 51829
		// (set) Token: 0x0600CA74 RID: 51828
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

		// Token: 0x170042B4 RID: 17076
		// (get) Token: 0x0600CA76 RID: 51830
		public virtual extern int IHTMLElement_offsetLeft
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170042B5 RID: 17077
		// (get) Token: 0x0600CA77 RID: 51831
		public virtual extern int IHTMLElement_offsetTop
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170042B6 RID: 17078
		// (get) Token: 0x0600CA78 RID: 51832
		public virtual extern int IHTMLElement_offsetWidth
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170042B7 RID: 17079
		// (get) Token: 0x0600CA79 RID: 51833
		public virtual extern int IHTMLElement_offsetHeight
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170042B8 RID: 17080
		// (get) Token: 0x0600CA7A RID: 51834
		public virtual extern IHTMLElement IHTMLElement_offsetParent
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170042B9 RID: 17081
		// (get) Token: 0x0600CA7C RID: 51836
		// (set) Token: 0x0600CA7B RID: 51835
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

		// Token: 0x170042BA RID: 17082
		// (get) Token: 0x0600CA7E RID: 51838
		// (set) Token: 0x0600CA7D RID: 51837
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

		// Token: 0x170042BB RID: 17083
		// (get) Token: 0x0600CA80 RID: 51840
		// (set) Token: 0x0600CA7F RID: 51839
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

		// Token: 0x170042BC RID: 17084
		// (get) Token: 0x0600CA82 RID: 51842
		// (set) Token: 0x0600CA81 RID: 51841
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

		// Token: 0x0600CA83 RID: 51843
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement_insertAdjacentHTML([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string html);

		// Token: 0x0600CA84 RID: 51844
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement_insertAdjacentText([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string text);

		// Token: 0x170042BD RID: 17085
		// (get) Token: 0x0600CA85 RID: 51845
		public virtual extern IHTMLElement IHTMLElement_parentTextEdit
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170042BE RID: 17086
		// (get) Token: 0x0600CA86 RID: 51846
		public virtual extern bool IHTMLElement_isTextEdit
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x0600CA87 RID: 51847
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement_click();

		// Token: 0x170042BF RID: 17087
		// (get) Token: 0x0600CA88 RID: 51848
		public virtual extern IHTMLFiltersCollection IHTMLElement_filters
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170042C0 RID: 17088
		// (get) Token: 0x0600CA8A RID: 51850
		// (set) Token: 0x0600CA89 RID: 51849
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

		// Token: 0x0600CA8B RID: 51851
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string IHTMLElement_toString();

		// Token: 0x170042C1 RID: 17089
		// (get) Token: 0x0600CA8D RID: 51853
		// (set) Token: 0x0600CA8C RID: 51852
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

		// Token: 0x170042C2 RID: 17090
		// (get) Token: 0x0600CA8F RID: 51855
		// (set) Token: 0x0600CA8E RID: 51854
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

		// Token: 0x170042C3 RID: 17091
		// (get) Token: 0x0600CA91 RID: 51857
		// (set) Token: 0x0600CA90 RID: 51856
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

		// Token: 0x170042C4 RID: 17092
		// (get) Token: 0x0600CA93 RID: 51859
		// (set) Token: 0x0600CA92 RID: 51858
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

		// Token: 0x170042C5 RID: 17093
		// (get) Token: 0x0600CA95 RID: 51861
		// (set) Token: 0x0600CA94 RID: 51860
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

		// Token: 0x170042C6 RID: 17094
		// (get) Token: 0x0600CA97 RID: 51863
		// (set) Token: 0x0600CA96 RID: 51862
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

		// Token: 0x170042C7 RID: 17095
		// (get) Token: 0x0600CA99 RID: 51865
		// (set) Token: 0x0600CA98 RID: 51864
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

		// Token: 0x170042C8 RID: 17096
		// (get) Token: 0x0600CA9B RID: 51867
		// (set) Token: 0x0600CA9A RID: 51866
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

		// Token: 0x170042C9 RID: 17097
		// (get) Token: 0x0600CA9D RID: 51869
		// (set) Token: 0x0600CA9C RID: 51868
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

		// Token: 0x170042CA RID: 17098
		// (get) Token: 0x0600CA9E RID: 51870
		public virtual extern object IHTMLElement_children
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x170042CB RID: 17099
		// (get) Token: 0x0600CA9F RID: 51871
		public virtual extern object IHTMLElement_all
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x170042CC RID: 17100
		// (get) Token: 0x0600CAA0 RID: 51872
		public virtual extern string IHTMLElement2_scopeName
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x0600CAA1 RID: 51873
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_setCapture([In] bool containerCapture = true);

		// Token: 0x0600CAA2 RID: 51874
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_releaseCapture();

		// Token: 0x170042CD RID: 17101
		// (get) Token: 0x0600CAA4 RID: 51876
		// (set) Token: 0x0600CAA3 RID: 51875
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

		// Token: 0x0600CAA5 RID: 51877
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string IHTMLElement2_componentFromPoint([In] int x, [In] int y);

		// Token: 0x0600CAA6 RID: 51878
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_doScroll([MarshalAs(27)] [In] [Optional] object component);

		// Token: 0x170042CE RID: 17102
		// (get) Token: 0x0600CAA8 RID: 51880
		// (set) Token: 0x0600CAA7 RID: 51879
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

		// Token: 0x170042CF RID: 17103
		// (get) Token: 0x0600CAAA RID: 51882
		// (set) Token: 0x0600CAA9 RID: 51881
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

		// Token: 0x170042D0 RID: 17104
		// (get) Token: 0x0600CAAC RID: 51884
		// (set) Token: 0x0600CAAB RID: 51883
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

		// Token: 0x170042D1 RID: 17105
		// (get) Token: 0x0600CAAE RID: 51886
		// (set) Token: 0x0600CAAD RID: 51885
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

		// Token: 0x170042D2 RID: 17106
		// (get) Token: 0x0600CAB0 RID: 51888
		// (set) Token: 0x0600CAAF RID: 51887
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

		// Token: 0x170042D3 RID: 17107
		// (get) Token: 0x0600CAB2 RID: 51890
		// (set) Token: 0x0600CAB1 RID: 51889
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

		// Token: 0x170042D4 RID: 17108
		// (get) Token: 0x0600CAB4 RID: 51892
		// (set) Token: 0x0600CAB3 RID: 51891
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

		// Token: 0x170042D5 RID: 17109
		// (get) Token: 0x0600CAB6 RID: 51894
		// (set) Token: 0x0600CAB5 RID: 51893
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

		// Token: 0x170042D6 RID: 17110
		// (get) Token: 0x0600CAB8 RID: 51896
		// (set) Token: 0x0600CAB7 RID: 51895
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

		// Token: 0x170042D7 RID: 17111
		// (get) Token: 0x0600CABA RID: 51898
		// (set) Token: 0x0600CAB9 RID: 51897
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

		// Token: 0x170042D8 RID: 17112
		// (get) Token: 0x0600CABC RID: 51900
		// (set) Token: 0x0600CABB RID: 51899
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

		// Token: 0x170042D9 RID: 17113
		// (get) Token: 0x0600CABE RID: 51902
		// (set) Token: 0x0600CABD RID: 51901
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

		// Token: 0x170042DA RID: 17114
		// (get) Token: 0x0600CAC0 RID: 51904
		// (set) Token: 0x0600CABF RID: 51903
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

		// Token: 0x170042DB RID: 17115
		// (get) Token: 0x0600CAC1 RID: 51905
		public virtual extern IHTMLCurrentStyle IHTMLElement2_currentStyle
		{
			[TypeLibFunc(1024)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170042DC RID: 17116
		// (get) Token: 0x0600CAC3 RID: 51907
		// (set) Token: 0x0600CAC2 RID: 51906
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

		// Token: 0x0600CAC4 RID: 51908
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLRectCollection IHTMLElement2_getClientRects();

		// Token: 0x0600CAC5 RID: 51909
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLRect IHTMLElement2_getBoundingClientRect();

		// Token: 0x0600CAC6 RID: 51910
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_setExpression([MarshalAs(19)] [In] string propname, [MarshalAs(19)] [In] string expression, [MarshalAs(19)] [In] string language = "");

		// Token: 0x0600CAC7 RID: 51911
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		public virtual extern object IHTMLElement2_getExpression([MarshalAs(19)] [In] string propname);

		// Token: 0x0600CAC8 RID: 51912
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement2_removeExpression([MarshalAs(19)] [In] string propname);

		// Token: 0x170042DD RID: 17117
		// (get) Token: 0x0600CACA RID: 51914
		// (set) Token: 0x0600CAC9 RID: 51913
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

		// Token: 0x0600CACB RID: 51915
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_focus();

		// Token: 0x170042DE RID: 17118
		// (get) Token: 0x0600CACD RID: 51917
		// (set) Token: 0x0600CACC RID: 51916
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

		// Token: 0x170042DF RID: 17119
		// (get) Token: 0x0600CACF RID: 51919
		// (set) Token: 0x0600CACE RID: 51918
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

		// Token: 0x170042E0 RID: 17120
		// (get) Token: 0x0600CAD1 RID: 51921
		// (set) Token: 0x0600CAD0 RID: 51920
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

		// Token: 0x170042E1 RID: 17121
		// (get) Token: 0x0600CAD3 RID: 51923
		// (set) Token: 0x0600CAD2 RID: 51922
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

		// Token: 0x0600CAD4 RID: 51924
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_blur();

		// Token: 0x0600CAD5 RID: 51925
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_addFilter([MarshalAs(25)] [In] object pUnk);

		// Token: 0x0600CAD6 RID: 51926
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_removeFilter([MarshalAs(25)] [In] object pUnk);

		// Token: 0x170042E2 RID: 17122
		// (get) Token: 0x0600CAD7 RID: 51927
		public virtual extern int IHTMLElement2_clientHeight
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170042E3 RID: 17123
		// (get) Token: 0x0600CAD8 RID: 51928
		public virtual extern int IHTMLElement2_clientWidth
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170042E4 RID: 17124
		// (get) Token: 0x0600CAD9 RID: 51929
		public virtual extern int IHTMLElement2_clientTop
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170042E5 RID: 17125
		// (get) Token: 0x0600CADA RID: 51930
		public virtual extern int IHTMLElement2_clientLeft
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x0600CADB RID: 51931
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement2_attachEvent([MarshalAs(19)] [In] string @event, [MarshalAs(26)] [In] object pdisp);

		// Token: 0x0600CADC RID: 51932
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_detachEvent([MarshalAs(19)] [In] string @event, [MarshalAs(26)] [In] object pdisp);

		// Token: 0x170042E6 RID: 17126
		// (get) Token: 0x0600CADD RID: 51933
		public virtual extern object IHTMLElement2_readyState
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x170042E7 RID: 17127
		// (get) Token: 0x0600CADF RID: 51935
		// (set) Token: 0x0600CADE RID: 51934
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

		// Token: 0x170042E8 RID: 17128
		// (get) Token: 0x0600CAE1 RID: 51937
		// (set) Token: 0x0600CAE0 RID: 51936
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

		// Token: 0x170042E9 RID: 17129
		// (get) Token: 0x0600CAE3 RID: 51939
		// (set) Token: 0x0600CAE2 RID: 51938
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

		// Token: 0x170042EA RID: 17130
		// (get) Token: 0x0600CAE5 RID: 51941
		// (set) Token: 0x0600CAE4 RID: 51940
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

		// Token: 0x170042EB RID: 17131
		// (get) Token: 0x0600CAE7 RID: 51943
		// (set) Token: 0x0600CAE6 RID: 51942
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

		// Token: 0x0600CAE8 RID: 51944
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(26)]
		public virtual extern object IHTMLElement2_createControlRange();

		// Token: 0x170042EC RID: 17132
		// (get) Token: 0x0600CAE9 RID: 51945
		public virtual extern int IHTMLElement2_scrollHeight
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170042ED RID: 17133
		// (get) Token: 0x0600CAEA RID: 51946
		public virtual extern int IHTMLElement2_scrollWidth
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170042EE RID: 17134
		// (get) Token: 0x0600CAEC RID: 51948
		// (set) Token: 0x0600CAEB RID: 51947
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

		// Token: 0x170042EF RID: 17135
		// (get) Token: 0x0600CAEE RID: 51950
		// (set) Token: 0x0600CAED RID: 51949
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

		// Token: 0x0600CAEF RID: 51951
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_clearAttributes();

		// Token: 0x0600CAF0 RID: 51952
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void mergeAttributes([MarshalAs(28)] [In] IHTMLElement mergeThis);

		// Token: 0x170042F0 RID: 17136
		// (get) Token: 0x0600CAF2 RID: 51954
		// (set) Token: 0x0600CAF1 RID: 51953
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

		// Token: 0x0600CAF3 RID: 51955
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElement IHTMLElement2_insertAdjacentElement([MarshalAs(19)] [In] string where, [MarshalAs(28)] [In] IHTMLElement insertedElement);

		// Token: 0x0600CAF4 RID: 51956
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElement IHTMLElement2_applyElement([MarshalAs(28)] [In] IHTMLElement apply, [MarshalAs(19)] [In] string where);

		// Token: 0x0600CAF5 RID: 51957
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string IHTMLElement2_getAdjacentText([MarshalAs(19)] [In] string where);

		// Token: 0x0600CAF6 RID: 51958
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string IHTMLElement2_replaceAdjacentText([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string newText);

		// Token: 0x170042F1 RID: 17137
		// (get) Token: 0x0600CAF7 RID: 51959
		public virtual extern bool IHTMLElement2_canHaveChildren
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x0600CAF8 RID: 51960
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern int IHTMLElement2_addBehavior([MarshalAs(19)] [In] string bstrUrl, [MarshalAs(27)] [In] [Optional] ref object pvarFactory);

		// Token: 0x0600CAF9 RID: 51961
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement2_removeBehavior([In] int cookie);

		// Token: 0x170042F2 RID: 17138
		// (get) Token: 0x0600CAFA RID: 51962
		public virtual extern IHTMLStyle IHTMLElement2_runtimeStyle
		{
			[TypeLibFunc(1024)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170042F3 RID: 17139
		// (get) Token: 0x0600CAFB RID: 51963
		public virtual extern object IHTMLElement2_behaviorUrns
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x170042F4 RID: 17140
		// (get) Token: 0x0600CAFD RID: 51965
		// (set) Token: 0x0600CAFC RID: 51964
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

		// Token: 0x170042F5 RID: 17141
		// (get) Token: 0x0600CAFF RID: 51967
		// (set) Token: 0x0600CAFE RID: 51966
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

		// Token: 0x170042F6 RID: 17142
		// (get) Token: 0x0600CB00 RID: 51968
		public virtual extern int IHTMLElement2_readyStateValue
		{
			[TypeLibFunc(65)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x0600CB01 RID: 51969
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElementCollection IHTMLElement2_getElementsByTagName([MarshalAs(19)] [In] string v);

		// Token: 0x0600CB02 RID: 51970
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void mergeAttributes([MarshalAs(28)] [In] IHTMLElement mergeThis, [MarshalAs(27)] [In] [Optional] ref object pvarFlags);

		// Token: 0x170042F7 RID: 17143
		// (get) Token: 0x0600CB03 RID: 51971
		public virtual extern bool IHTMLElement3_isMultiLine
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170042F8 RID: 17144
		// (get) Token: 0x0600CB04 RID: 51972
		public virtual extern bool IHTMLElement3_canHaveHTML
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170042F9 RID: 17145
		// (get) Token: 0x0600CB06 RID: 51974
		// (set) Token: 0x0600CB05 RID: 51973
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

		// Token: 0x170042FA RID: 17146
		// (get) Token: 0x0600CB08 RID: 51976
		// (set) Token: 0x0600CB07 RID: 51975
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

		// Token: 0x170042FB RID: 17147
		// (get) Token: 0x0600CB0A RID: 51978
		// (set) Token: 0x0600CB09 RID: 51977
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

		// Token: 0x170042FC RID: 17148
		// (get) Token: 0x0600CB0C RID: 51980
		// (set) Token: 0x0600CB0B RID: 51979
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

		// Token: 0x0600CB0D RID: 51981
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement3_setActive();

		// Token: 0x170042FD RID: 17149
		// (get) Token: 0x0600CB0F RID: 51983
		// (set) Token: 0x0600CB0E RID: 51982
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

		// Token: 0x170042FE RID: 17150
		// (get) Token: 0x0600CB10 RID: 51984
		public virtual extern bool IHTMLElement3_isContentEditable
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170042FF RID: 17151
		// (get) Token: 0x0600CB12 RID: 51986
		// (set) Token: 0x0600CB11 RID: 51985
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

		// Token: 0x17004300 RID: 17152
		// (get) Token: 0x0600CB14 RID: 51988
		// (set) Token: 0x0600CB13 RID: 51987
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

		// Token: 0x17004301 RID: 17153
		// (get) Token: 0x0600CB15 RID: 51989
		public virtual extern bool IHTMLElement3_isDisabled
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17004302 RID: 17154
		// (get) Token: 0x0600CB17 RID: 51991
		// (set) Token: 0x0600CB16 RID: 51990
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

		// Token: 0x17004303 RID: 17155
		// (get) Token: 0x0600CB19 RID: 51993
		// (set) Token: 0x0600CB18 RID: 51992
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

		// Token: 0x0600CB1A RID: 51994
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement3_FireEvent([MarshalAs(19)] [In] string bstrEventName, [MarshalAs(27)] [In] [Optional] ref object pvarEventObject);

		// Token: 0x17004304 RID: 17156
		// (get) Token: 0x0600CB1C RID: 51996
		// (set) Token: 0x0600CB1B RID: 51995
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

		// Token: 0x17004305 RID: 17157
		// (get) Token: 0x0600CB1E RID: 51998
		// (set) Token: 0x0600CB1D RID: 51997
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

		// Token: 0x17004306 RID: 17158
		// (get) Token: 0x0600CB20 RID: 52000
		// (set) Token: 0x0600CB1F RID: 51999
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

		// Token: 0x17004307 RID: 17159
		// (get) Token: 0x0600CB22 RID: 52002
		// (set) Token: 0x0600CB21 RID: 52001
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

		// Token: 0x17004308 RID: 17160
		// (get) Token: 0x0600CB24 RID: 52004
		// (set) Token: 0x0600CB23 RID: 52003
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

		// Token: 0x17004309 RID: 17161
		// (get) Token: 0x0600CB26 RID: 52006
		// (set) Token: 0x0600CB25 RID: 52005
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

		// Token: 0x1700430A RID: 17162
		// (get) Token: 0x0600CB28 RID: 52008
		// (set) Token: 0x0600CB27 RID: 52007
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

		// Token: 0x1700430B RID: 17163
		// (get) Token: 0x0600CB2A RID: 52010
		// (set) Token: 0x0600CB29 RID: 52009
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

		// Token: 0x0600CB2B RID: 52011
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement3_dragDrop();

		// Token: 0x1700430C RID: 17164
		// (get) Token: 0x0600CB2C RID: 52012
		public virtual extern int IHTMLElement3_glyphMode
		{
			[TypeLibFunc(1089)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700430D RID: 17165
		// (get) Token: 0x0600CB2E RID: 52014
		// (set) Token: 0x0600CB2D RID: 52013
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

		// Token: 0x0600CB2F RID: 52015
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement4_normalize();

		// Token: 0x0600CB30 RID: 52016
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute IHTMLElement4_getAttributeNode([MarshalAs(19)] [In] string bstrName);

		// Token: 0x0600CB31 RID: 52017
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute IHTMLElement4_setAttributeNode([MarshalAs(28)] [In] IHTMLDOMAttribute pattr);

		// Token: 0x0600CB32 RID: 52018
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute IHTMLElement4_removeAttributeNode([MarshalAs(28)] [In] IHTMLDOMAttribute pattr);

		// Token: 0x1700430E RID: 17166
		// (get) Token: 0x0600CB34 RID: 52020
		// (set) Token: 0x0600CB33 RID: 52019
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

		// Token: 0x1700430F RID: 17167
		// (get) Token: 0x0600CB36 RID: 52022
		// (set) Token: 0x0600CB35 RID: 52021
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

		// Token: 0x17004310 RID: 17168
		// (get) Token: 0x0600CB38 RID: 52024
		// (set) Token: 0x0600CB37 RID: 52023
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

		// Token: 0x17004311 RID: 17169
		// (get) Token: 0x0600CB39 RID: 52025
		public virtual extern int IHTMLUniqueName_uniqueNumber
		{
			[TypeLibFunc(64)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17004312 RID: 17170
		// (get) Token: 0x0600CB3A RID: 52026
		public virtual extern string IHTMLUniqueName_uniqueID
		{
			[TypeLibFunc(64)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17004313 RID: 17171
		// (get) Token: 0x0600CB3B RID: 52027
		public virtual extern int IHTMLDOMNode_nodeType
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17004314 RID: 17172
		// (get) Token: 0x0600CB3C RID: 52028
		public virtual extern IHTMLDOMNode IHTMLDOMNode_parentNode
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x0600CB3D RID: 52029
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLDOMNode_hasChildNodes();

		// Token: 0x17004315 RID: 17173
		// (get) Token: 0x0600CB3E RID: 52030
		public virtual extern object IHTMLDOMNode_childNodes
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17004316 RID: 17174
		// (get) Token: 0x0600CB3F RID: 52031
		public virtual extern object IHTMLDOMNode_attributes
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x0600CB40 RID: 52032
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_insertBefore([MarshalAs(28)] [In] IHTMLDOMNode newChild, [MarshalAs(27)] [In] [Optional] object refChild);

		// Token: 0x0600CB41 RID: 52033
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_removeChild([MarshalAs(28)] [In] IHTMLDOMNode oldChild);

		// Token: 0x0600CB42 RID: 52034
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_replaceChild([MarshalAs(28)] [In] IHTMLDOMNode newChild, [MarshalAs(28)] [In] IHTMLDOMNode oldChild);

		// Token: 0x0600CB43 RID: 52035
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_cloneNode([In] bool fDeep);

		// Token: 0x0600CB44 RID: 52036
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_removeNode([In] bool fDeep = false);

		// Token: 0x0600CB45 RID: 52037
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_swapNode([MarshalAs(28)] [In] IHTMLDOMNode otherNode);

		// Token: 0x0600CB46 RID: 52038
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_replaceNode([MarshalAs(28)] [In] IHTMLDOMNode replacement);

		// Token: 0x0600CB47 RID: 52039
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_appendChild([MarshalAs(28)] [In] IHTMLDOMNode newChild);

		// Token: 0x17004317 RID: 17175
		// (get) Token: 0x0600CB48 RID: 52040
		public virtual extern string IHTMLDOMNode_nodeName
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17004318 RID: 17176
		// (get) Token: 0x0600CB4A RID: 52042
		// (set) Token: 0x0600CB49 RID: 52041
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

		// Token: 0x17004319 RID: 17177
		// (get) Token: 0x0600CB4B RID: 52043
		public virtual extern IHTMLDOMNode IHTMLDOMNode_firstChild
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x1700431A RID: 17178
		// (get) Token: 0x0600CB4C RID: 52044
		public virtual extern IHTMLDOMNode IHTMLDOMNode_lastChild
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x1700431B RID: 17179
		// (get) Token: 0x0600CB4D RID: 52045
		public virtual extern IHTMLDOMNode IHTMLDOMNode_previousSibling
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x1700431C RID: 17180
		// (get) Token: 0x0600CB4E RID: 52046
		public virtual extern IHTMLDOMNode IHTMLDOMNode_nextSibling
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x1700431D RID: 17181
		// (get) Token: 0x0600CB4F RID: 52047
		public virtual extern object IHTMLDOMNode2_ownerDocument
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x1700431E RID: 17182
		// (get) Token: 0x0600CB51 RID: 52049
		// (set) Token: 0x0600CB50 RID: 52048
		public virtual extern string IHTMLNextIdElement_n
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

		// Token: 0x140018A4 RID: 6308
		// (add) Token: 0x0600CB52 RID: 52050
		// (remove) Token: 0x0600CB53 RID: 52051
		public virtual extern event HTMLElementEvents_onhelpEventHandler HTMLElementEvents_Event_onhelp;

		// Token: 0x140018A5 RID: 6309
		// (add) Token: 0x0600CB54 RID: 52052
		// (remove) Token: 0x0600CB55 RID: 52053
		public virtual extern event HTMLElementEvents_onclickEventHandler HTMLElementEvents_Event_onclick;

		// Token: 0x140018A6 RID: 6310
		// (add) Token: 0x0600CB56 RID: 52054
		// (remove) Token: 0x0600CB57 RID: 52055
		public virtual extern event HTMLElementEvents_ondblclickEventHandler HTMLElementEvents_Event_ondblclick;

		// Token: 0x140018A7 RID: 6311
		// (add) Token: 0x0600CB58 RID: 52056
		// (remove) Token: 0x0600CB59 RID: 52057
		public virtual extern event HTMLElementEvents_onkeypressEventHandler HTMLElementEvents_Event_onkeypress;

		// Token: 0x140018A8 RID: 6312
		// (add) Token: 0x0600CB5A RID: 52058
		// (remove) Token: 0x0600CB5B RID: 52059
		public virtual extern event HTMLElementEvents_onkeydownEventHandler HTMLElementEvents_Event_onkeydown;

		// Token: 0x140018A9 RID: 6313
		// (add) Token: 0x0600CB5C RID: 52060
		// (remove) Token: 0x0600CB5D RID: 52061
		public virtual extern event HTMLElementEvents_onkeyupEventHandler HTMLElementEvents_Event_onkeyup;

		// Token: 0x140018AA RID: 6314
		// (add) Token: 0x0600CB5E RID: 52062
		// (remove) Token: 0x0600CB5F RID: 52063
		public virtual extern event HTMLElementEvents_onmouseoutEventHandler HTMLElementEvents_Event_onmouseout;

		// Token: 0x140018AB RID: 6315
		// (add) Token: 0x0600CB60 RID: 52064
		// (remove) Token: 0x0600CB61 RID: 52065
		public virtual extern event HTMLElementEvents_onmouseoverEventHandler HTMLElementEvents_Event_onmouseover;

		// Token: 0x140018AC RID: 6316
		// (add) Token: 0x0600CB62 RID: 52066
		// (remove) Token: 0x0600CB63 RID: 52067
		public virtual extern event HTMLElementEvents_onmousemoveEventHandler HTMLElementEvents_Event_onmousemove;

		// Token: 0x140018AD RID: 6317
		// (add) Token: 0x0600CB64 RID: 52068
		// (remove) Token: 0x0600CB65 RID: 52069
		public virtual extern event HTMLElementEvents_onmousedownEventHandler HTMLElementEvents_Event_onmousedown;

		// Token: 0x140018AE RID: 6318
		// (add) Token: 0x0600CB66 RID: 52070
		// (remove) Token: 0x0600CB67 RID: 52071
		public virtual extern event HTMLElementEvents_onmouseupEventHandler HTMLElementEvents_Event_onmouseup;

		// Token: 0x140018AF RID: 6319
		// (add) Token: 0x0600CB68 RID: 52072
		// (remove) Token: 0x0600CB69 RID: 52073
		public virtual extern event HTMLElementEvents_onselectstartEventHandler HTMLElementEvents_Event_onselectstart;

		// Token: 0x140018B0 RID: 6320
		// (add) Token: 0x0600CB6A RID: 52074
		// (remove) Token: 0x0600CB6B RID: 52075
		public virtual extern event HTMLElementEvents_onfilterchangeEventHandler HTMLElementEvents_Event_onfilterchange;

		// Token: 0x140018B1 RID: 6321
		// (add) Token: 0x0600CB6C RID: 52076
		// (remove) Token: 0x0600CB6D RID: 52077
		public virtual extern event HTMLElementEvents_ondragstartEventHandler HTMLElementEvents_Event_ondragstart;

		// Token: 0x140018B2 RID: 6322
		// (add) Token: 0x0600CB6E RID: 52078
		// (remove) Token: 0x0600CB6F RID: 52079
		public virtual extern event HTMLElementEvents_onbeforeupdateEventHandler HTMLElementEvents_Event_onbeforeupdate;

		// Token: 0x140018B3 RID: 6323
		// (add) Token: 0x0600CB70 RID: 52080
		// (remove) Token: 0x0600CB71 RID: 52081
		public virtual extern event HTMLElementEvents_onafterupdateEventHandler HTMLElementEvents_Event_onafterupdate;

		// Token: 0x140018B4 RID: 6324
		// (add) Token: 0x0600CB72 RID: 52082
		// (remove) Token: 0x0600CB73 RID: 52083
		public virtual extern event HTMLElementEvents_onerrorupdateEventHandler HTMLElementEvents_Event_onerrorupdate;

		// Token: 0x140018B5 RID: 6325
		// (add) Token: 0x0600CB74 RID: 52084
		// (remove) Token: 0x0600CB75 RID: 52085
		public virtual extern event HTMLElementEvents_onrowexitEventHandler HTMLElementEvents_Event_onrowexit;

		// Token: 0x140018B6 RID: 6326
		// (add) Token: 0x0600CB76 RID: 52086
		// (remove) Token: 0x0600CB77 RID: 52087
		public virtual extern event HTMLElementEvents_onrowenterEventHandler HTMLElementEvents_Event_onrowenter;

		// Token: 0x140018B7 RID: 6327
		// (add) Token: 0x0600CB78 RID: 52088
		// (remove) Token: 0x0600CB79 RID: 52089
		public virtual extern event HTMLElementEvents_ondatasetchangedEventHandler HTMLElementEvents_Event_ondatasetchanged;

		// Token: 0x140018B8 RID: 6328
		// (add) Token: 0x0600CB7A RID: 52090
		// (remove) Token: 0x0600CB7B RID: 52091
		public virtual extern event HTMLElementEvents_ondataavailableEventHandler HTMLElementEvents_Event_ondataavailable;

		// Token: 0x140018B9 RID: 6329
		// (add) Token: 0x0600CB7C RID: 52092
		// (remove) Token: 0x0600CB7D RID: 52093
		public virtual extern event HTMLElementEvents_ondatasetcompleteEventHandler HTMLElementEvents_Event_ondatasetcomplete;

		// Token: 0x140018BA RID: 6330
		// (add) Token: 0x0600CB7E RID: 52094
		// (remove) Token: 0x0600CB7F RID: 52095
		public virtual extern event HTMLElementEvents_onlosecaptureEventHandler HTMLElementEvents_Event_onlosecapture;

		// Token: 0x140018BB RID: 6331
		// (add) Token: 0x0600CB80 RID: 52096
		// (remove) Token: 0x0600CB81 RID: 52097
		public virtual extern event HTMLElementEvents_onpropertychangeEventHandler HTMLElementEvents_Event_onpropertychange;

		// Token: 0x140018BC RID: 6332
		// (add) Token: 0x0600CB82 RID: 52098
		// (remove) Token: 0x0600CB83 RID: 52099
		public virtual extern event HTMLElementEvents_onscrollEventHandler HTMLElementEvents_Event_onscroll;

		// Token: 0x140018BD RID: 6333
		// (add) Token: 0x0600CB84 RID: 52100
		// (remove) Token: 0x0600CB85 RID: 52101
		public virtual extern event HTMLElementEvents_onfocusEventHandler HTMLElementEvents_Event_onfocus;

		// Token: 0x140018BE RID: 6334
		// (add) Token: 0x0600CB86 RID: 52102
		// (remove) Token: 0x0600CB87 RID: 52103
		public virtual extern event HTMLElementEvents_onblurEventHandler HTMLElementEvents_Event_onblur;

		// Token: 0x140018BF RID: 6335
		// (add) Token: 0x0600CB88 RID: 52104
		// (remove) Token: 0x0600CB89 RID: 52105
		public virtual extern event HTMLElementEvents_onresizeEventHandler HTMLElementEvents_Event_onresize;

		// Token: 0x140018C0 RID: 6336
		// (add) Token: 0x0600CB8A RID: 52106
		// (remove) Token: 0x0600CB8B RID: 52107
		public virtual extern event HTMLElementEvents_ondragEventHandler HTMLElementEvents_Event_ondrag;

		// Token: 0x140018C1 RID: 6337
		// (add) Token: 0x0600CB8C RID: 52108
		// (remove) Token: 0x0600CB8D RID: 52109
		public virtual extern event HTMLElementEvents_ondragendEventHandler HTMLElementEvents_Event_ondragend;

		// Token: 0x140018C2 RID: 6338
		// (add) Token: 0x0600CB8E RID: 52110
		// (remove) Token: 0x0600CB8F RID: 52111
		public virtual extern event HTMLElementEvents_ondragenterEventHandler HTMLElementEvents_Event_ondragenter;

		// Token: 0x140018C3 RID: 6339
		// (add) Token: 0x0600CB90 RID: 52112
		// (remove) Token: 0x0600CB91 RID: 52113
		public virtual extern event HTMLElementEvents_ondragoverEventHandler HTMLElementEvents_Event_ondragover;

		// Token: 0x140018C4 RID: 6340
		// (add) Token: 0x0600CB92 RID: 52114
		// (remove) Token: 0x0600CB93 RID: 52115
		public virtual extern event HTMLElementEvents_ondragleaveEventHandler HTMLElementEvents_Event_ondragleave;

		// Token: 0x140018C5 RID: 6341
		// (add) Token: 0x0600CB94 RID: 52116
		// (remove) Token: 0x0600CB95 RID: 52117
		public virtual extern event HTMLElementEvents_ondropEventHandler HTMLElementEvents_Event_ondrop;

		// Token: 0x140018C6 RID: 6342
		// (add) Token: 0x0600CB96 RID: 52118
		// (remove) Token: 0x0600CB97 RID: 52119
		public virtual extern event HTMLElementEvents_onbeforecutEventHandler HTMLElementEvents_Event_onbeforecut;

		// Token: 0x140018C7 RID: 6343
		// (add) Token: 0x0600CB98 RID: 52120
		// (remove) Token: 0x0600CB99 RID: 52121
		public virtual extern event HTMLElementEvents_oncutEventHandler HTMLElementEvents_Event_oncut;

		// Token: 0x140018C8 RID: 6344
		// (add) Token: 0x0600CB9A RID: 52122
		// (remove) Token: 0x0600CB9B RID: 52123
		public virtual extern event HTMLElementEvents_onbeforecopyEventHandler HTMLElementEvents_Event_onbeforecopy;

		// Token: 0x140018C9 RID: 6345
		// (add) Token: 0x0600CB9C RID: 52124
		// (remove) Token: 0x0600CB9D RID: 52125
		public virtual extern event HTMLElementEvents_oncopyEventHandler HTMLElementEvents_Event_oncopy;

		// Token: 0x140018CA RID: 6346
		// (add) Token: 0x0600CB9E RID: 52126
		// (remove) Token: 0x0600CB9F RID: 52127
		public virtual extern event HTMLElementEvents_onbeforepasteEventHandler HTMLElementEvents_Event_onbeforepaste;

		// Token: 0x140018CB RID: 6347
		// (add) Token: 0x0600CBA0 RID: 52128
		// (remove) Token: 0x0600CBA1 RID: 52129
		public virtual extern event HTMLElementEvents_onpasteEventHandler HTMLElementEvents_Event_onpaste;

		// Token: 0x140018CC RID: 6348
		// (add) Token: 0x0600CBA2 RID: 52130
		// (remove) Token: 0x0600CBA3 RID: 52131
		public virtual extern event HTMLElementEvents_oncontextmenuEventHandler HTMLElementEvents_Event_oncontextmenu;

		// Token: 0x140018CD RID: 6349
		// (add) Token: 0x0600CBA4 RID: 52132
		// (remove) Token: 0x0600CBA5 RID: 52133
		public virtual extern event HTMLElementEvents_onrowsdeleteEventHandler HTMLElementEvents_Event_onrowsdelete;

		// Token: 0x140018CE RID: 6350
		// (add) Token: 0x0600CBA6 RID: 52134
		// (remove) Token: 0x0600CBA7 RID: 52135
		public virtual extern event HTMLElementEvents_onrowsinsertedEventHandler HTMLElementEvents_Event_onrowsinserted;

		// Token: 0x140018CF RID: 6351
		// (add) Token: 0x0600CBA8 RID: 52136
		// (remove) Token: 0x0600CBA9 RID: 52137
		public virtual extern event HTMLElementEvents_oncellchangeEventHandler HTMLElementEvents_Event_oncellchange;

		// Token: 0x140018D0 RID: 6352
		// (add) Token: 0x0600CBAA RID: 52138
		// (remove) Token: 0x0600CBAB RID: 52139
		public virtual extern event HTMLElementEvents_onreadystatechangeEventHandler HTMLElementEvents_Event_onreadystatechange;

		// Token: 0x140018D1 RID: 6353
		// (add) Token: 0x0600CBAC RID: 52140
		// (remove) Token: 0x0600CBAD RID: 52141
		public virtual extern event HTMLElementEvents_onbeforeeditfocusEventHandler HTMLElementEvents_Event_onbeforeeditfocus;

		// Token: 0x140018D2 RID: 6354
		// (add) Token: 0x0600CBAE RID: 52142
		// (remove) Token: 0x0600CBAF RID: 52143
		public virtual extern event HTMLElementEvents_onlayoutcompleteEventHandler HTMLElementEvents_Event_onlayoutcomplete;

		// Token: 0x140018D3 RID: 6355
		// (add) Token: 0x0600CBB0 RID: 52144
		// (remove) Token: 0x0600CBB1 RID: 52145
		public virtual extern event HTMLElementEvents_onpageEventHandler HTMLElementEvents_Event_onpage;

		// Token: 0x140018D4 RID: 6356
		// (add) Token: 0x0600CBB2 RID: 52146
		// (remove) Token: 0x0600CBB3 RID: 52147
		public virtual extern event HTMLElementEvents_onbeforedeactivateEventHandler HTMLElementEvents_Event_onbeforedeactivate;

		// Token: 0x140018D5 RID: 6357
		// (add) Token: 0x0600CBB4 RID: 52148
		// (remove) Token: 0x0600CBB5 RID: 52149
		public virtual extern event HTMLElementEvents_onbeforeactivateEventHandler HTMLElementEvents_Event_onbeforeactivate;

		// Token: 0x140018D6 RID: 6358
		// (add) Token: 0x0600CBB6 RID: 52150
		// (remove) Token: 0x0600CBB7 RID: 52151
		public virtual extern event HTMLElementEvents_onmoveEventHandler HTMLElementEvents_Event_onmove;

		// Token: 0x140018D7 RID: 6359
		// (add) Token: 0x0600CBB8 RID: 52152
		// (remove) Token: 0x0600CBB9 RID: 52153
		public virtual extern event HTMLElementEvents_oncontrolselectEventHandler HTMLElementEvents_Event_oncontrolselect;

		// Token: 0x140018D8 RID: 6360
		// (add) Token: 0x0600CBBA RID: 52154
		// (remove) Token: 0x0600CBBB RID: 52155
		public virtual extern event HTMLElementEvents_onmovestartEventHandler HTMLElementEvents_Event_onmovestart;

		// Token: 0x140018D9 RID: 6361
		// (add) Token: 0x0600CBBC RID: 52156
		// (remove) Token: 0x0600CBBD RID: 52157
		public virtual extern event HTMLElementEvents_onmoveendEventHandler HTMLElementEvents_Event_onmoveend;

		// Token: 0x140018DA RID: 6362
		// (add) Token: 0x0600CBBE RID: 52158
		// (remove) Token: 0x0600CBBF RID: 52159
		public virtual extern event HTMLElementEvents_onresizestartEventHandler HTMLElementEvents_Event_onresizestart;

		// Token: 0x140018DB RID: 6363
		// (add) Token: 0x0600CBC0 RID: 52160
		// (remove) Token: 0x0600CBC1 RID: 52161
		public virtual extern event HTMLElementEvents_onresizeendEventHandler HTMLElementEvents_Event_onresizeend;

		// Token: 0x140018DC RID: 6364
		// (add) Token: 0x0600CBC2 RID: 52162
		// (remove) Token: 0x0600CBC3 RID: 52163
		public virtual extern event HTMLElementEvents_onmouseenterEventHandler HTMLElementEvents_Event_onmouseenter;

		// Token: 0x140018DD RID: 6365
		// (add) Token: 0x0600CBC4 RID: 52164
		// (remove) Token: 0x0600CBC5 RID: 52165
		public virtual extern event HTMLElementEvents_onmouseleaveEventHandler HTMLElementEvents_Event_onmouseleave;

		// Token: 0x140018DE RID: 6366
		// (add) Token: 0x0600CBC6 RID: 52166
		// (remove) Token: 0x0600CBC7 RID: 52167
		public virtual extern event HTMLElementEvents_onmousewheelEventHandler HTMLElementEvents_Event_onmousewheel;

		// Token: 0x140018DF RID: 6367
		// (add) Token: 0x0600CBC8 RID: 52168
		// (remove) Token: 0x0600CBC9 RID: 52169
		public virtual extern event HTMLElementEvents_onactivateEventHandler HTMLElementEvents_Event_onactivate;

		// Token: 0x140018E0 RID: 6368
		// (add) Token: 0x0600CBCA RID: 52170
		// (remove) Token: 0x0600CBCB RID: 52171
		public virtual extern event HTMLElementEvents_ondeactivateEventHandler HTMLElementEvents_Event_ondeactivate;

		// Token: 0x140018E1 RID: 6369
		// (add) Token: 0x0600CBCC RID: 52172
		// (remove) Token: 0x0600CBCD RID: 52173
		public virtual extern event HTMLElementEvents_onfocusinEventHandler HTMLElementEvents_Event_onfocusin;

		// Token: 0x140018E2 RID: 6370
		// (add) Token: 0x0600CBCE RID: 52174
		// (remove) Token: 0x0600CBCF RID: 52175
		public virtual extern event HTMLElementEvents_onfocusoutEventHandler HTMLElementEvents_Event_onfocusout;

		// Token: 0x140018E3 RID: 6371
		// (add) Token: 0x0600CBD0 RID: 52176
		// (remove) Token: 0x0600CBD1 RID: 52177
		public virtual extern event HTMLElementEvents2_onhelpEventHandler HTMLElementEvents2_Event_onhelp;

		// Token: 0x140018E4 RID: 6372
		// (add) Token: 0x0600CBD2 RID: 52178
		// (remove) Token: 0x0600CBD3 RID: 52179
		public virtual extern event HTMLElementEvents2_onclickEventHandler HTMLElementEvents2_Event_onclick;

		// Token: 0x140018E5 RID: 6373
		// (add) Token: 0x0600CBD4 RID: 52180
		// (remove) Token: 0x0600CBD5 RID: 52181
		public virtual extern event HTMLElementEvents2_ondblclickEventHandler HTMLElementEvents2_Event_ondblclick;

		// Token: 0x140018E6 RID: 6374
		// (add) Token: 0x0600CBD6 RID: 52182
		// (remove) Token: 0x0600CBD7 RID: 52183
		public virtual extern event HTMLElementEvents2_onkeypressEventHandler HTMLElementEvents2_Event_onkeypress;

		// Token: 0x140018E7 RID: 6375
		// (add) Token: 0x0600CBD8 RID: 52184
		// (remove) Token: 0x0600CBD9 RID: 52185
		public virtual extern event HTMLElementEvents2_onkeydownEventHandler HTMLElementEvents2_Event_onkeydown;

		// Token: 0x140018E8 RID: 6376
		// (add) Token: 0x0600CBDA RID: 52186
		// (remove) Token: 0x0600CBDB RID: 52187
		public virtual extern event HTMLElementEvents2_onkeyupEventHandler HTMLElementEvents2_Event_onkeyup;

		// Token: 0x140018E9 RID: 6377
		// (add) Token: 0x0600CBDC RID: 52188
		// (remove) Token: 0x0600CBDD RID: 52189
		public virtual extern event HTMLElementEvents2_onmouseoutEventHandler HTMLElementEvents2_Event_onmouseout;

		// Token: 0x140018EA RID: 6378
		// (add) Token: 0x0600CBDE RID: 52190
		// (remove) Token: 0x0600CBDF RID: 52191
		public virtual extern event HTMLElementEvents2_onmouseoverEventHandler HTMLElementEvents2_Event_onmouseover;

		// Token: 0x140018EB RID: 6379
		// (add) Token: 0x0600CBE0 RID: 52192
		// (remove) Token: 0x0600CBE1 RID: 52193
		public virtual extern event HTMLElementEvents2_onmousemoveEventHandler HTMLElementEvents2_Event_onmousemove;

		// Token: 0x140018EC RID: 6380
		// (add) Token: 0x0600CBE2 RID: 52194
		// (remove) Token: 0x0600CBE3 RID: 52195
		public virtual extern event HTMLElementEvents2_onmousedownEventHandler HTMLElementEvents2_Event_onmousedown;

		// Token: 0x140018ED RID: 6381
		// (add) Token: 0x0600CBE4 RID: 52196
		// (remove) Token: 0x0600CBE5 RID: 52197
		public virtual extern event HTMLElementEvents2_onmouseupEventHandler HTMLElementEvents2_Event_onmouseup;

		// Token: 0x140018EE RID: 6382
		// (add) Token: 0x0600CBE6 RID: 52198
		// (remove) Token: 0x0600CBE7 RID: 52199
		public virtual extern event HTMLElementEvents2_onselectstartEventHandler HTMLElementEvents2_Event_onselectstart;

		// Token: 0x140018EF RID: 6383
		// (add) Token: 0x0600CBE8 RID: 52200
		// (remove) Token: 0x0600CBE9 RID: 52201
		public virtual extern event HTMLElementEvents2_onfilterchangeEventHandler HTMLElementEvents2_Event_onfilterchange;

		// Token: 0x140018F0 RID: 6384
		// (add) Token: 0x0600CBEA RID: 52202
		// (remove) Token: 0x0600CBEB RID: 52203
		public virtual extern event HTMLElementEvents2_ondragstartEventHandler HTMLElementEvents2_Event_ondragstart;

		// Token: 0x140018F1 RID: 6385
		// (add) Token: 0x0600CBEC RID: 52204
		// (remove) Token: 0x0600CBED RID: 52205
		public virtual extern event HTMLElementEvents2_onbeforeupdateEventHandler HTMLElementEvents2_Event_onbeforeupdate;

		// Token: 0x140018F2 RID: 6386
		// (add) Token: 0x0600CBEE RID: 52206
		// (remove) Token: 0x0600CBEF RID: 52207
		public virtual extern event HTMLElementEvents2_onafterupdateEventHandler HTMLElementEvents2_Event_onafterupdate;

		// Token: 0x140018F3 RID: 6387
		// (add) Token: 0x0600CBF0 RID: 52208
		// (remove) Token: 0x0600CBF1 RID: 52209
		public virtual extern event HTMLElementEvents2_onerrorupdateEventHandler HTMLElementEvents2_Event_onerrorupdate;

		// Token: 0x140018F4 RID: 6388
		// (add) Token: 0x0600CBF2 RID: 52210
		// (remove) Token: 0x0600CBF3 RID: 52211
		public virtual extern event HTMLElementEvents2_onrowexitEventHandler HTMLElementEvents2_Event_onrowexit;

		// Token: 0x140018F5 RID: 6389
		// (add) Token: 0x0600CBF4 RID: 52212
		// (remove) Token: 0x0600CBF5 RID: 52213
		public virtual extern event HTMLElementEvents2_onrowenterEventHandler HTMLElementEvents2_Event_onrowenter;

		// Token: 0x140018F6 RID: 6390
		// (add) Token: 0x0600CBF6 RID: 52214
		// (remove) Token: 0x0600CBF7 RID: 52215
		public virtual extern event HTMLElementEvents2_ondatasetchangedEventHandler HTMLElementEvents2_Event_ondatasetchanged;

		// Token: 0x140018F7 RID: 6391
		// (add) Token: 0x0600CBF8 RID: 52216
		// (remove) Token: 0x0600CBF9 RID: 52217
		public virtual extern event HTMLElementEvents2_ondataavailableEventHandler HTMLElementEvents2_Event_ondataavailable;

		// Token: 0x140018F8 RID: 6392
		// (add) Token: 0x0600CBFA RID: 52218
		// (remove) Token: 0x0600CBFB RID: 52219
		public virtual extern event HTMLElementEvents2_ondatasetcompleteEventHandler HTMLElementEvents2_Event_ondatasetcomplete;

		// Token: 0x140018F9 RID: 6393
		// (add) Token: 0x0600CBFC RID: 52220
		// (remove) Token: 0x0600CBFD RID: 52221
		public virtual extern event HTMLElementEvents2_onlosecaptureEventHandler HTMLElementEvents2_Event_onlosecapture;

		// Token: 0x140018FA RID: 6394
		// (add) Token: 0x0600CBFE RID: 52222
		// (remove) Token: 0x0600CBFF RID: 52223
		public virtual extern event HTMLElementEvents2_onpropertychangeEventHandler HTMLElementEvents2_Event_onpropertychange;

		// Token: 0x140018FB RID: 6395
		// (add) Token: 0x0600CC00 RID: 52224
		// (remove) Token: 0x0600CC01 RID: 52225
		public virtual extern event HTMLElementEvents2_onscrollEventHandler HTMLElementEvents2_Event_onscroll;

		// Token: 0x140018FC RID: 6396
		// (add) Token: 0x0600CC02 RID: 52226
		// (remove) Token: 0x0600CC03 RID: 52227
		public virtual extern event HTMLElementEvents2_onfocusEventHandler HTMLElementEvents2_Event_onfocus;

		// Token: 0x140018FD RID: 6397
		// (add) Token: 0x0600CC04 RID: 52228
		// (remove) Token: 0x0600CC05 RID: 52229
		public virtual extern event HTMLElementEvents2_onblurEventHandler HTMLElementEvents2_Event_onblur;

		// Token: 0x140018FE RID: 6398
		// (add) Token: 0x0600CC06 RID: 52230
		// (remove) Token: 0x0600CC07 RID: 52231
		public virtual extern event HTMLElementEvents2_onresizeEventHandler HTMLElementEvents2_Event_onresize;

		// Token: 0x140018FF RID: 6399
		// (add) Token: 0x0600CC08 RID: 52232
		// (remove) Token: 0x0600CC09 RID: 52233
		public virtual extern event HTMLElementEvents2_ondragEventHandler HTMLElementEvents2_Event_ondrag;

		// Token: 0x14001900 RID: 6400
		// (add) Token: 0x0600CC0A RID: 52234
		// (remove) Token: 0x0600CC0B RID: 52235
		public virtual extern event HTMLElementEvents2_ondragendEventHandler HTMLElementEvents2_Event_ondragend;

		// Token: 0x14001901 RID: 6401
		// (add) Token: 0x0600CC0C RID: 52236
		// (remove) Token: 0x0600CC0D RID: 52237
		public virtual extern event HTMLElementEvents2_ondragenterEventHandler HTMLElementEvents2_Event_ondragenter;

		// Token: 0x14001902 RID: 6402
		// (add) Token: 0x0600CC0E RID: 52238
		// (remove) Token: 0x0600CC0F RID: 52239
		public virtual extern event HTMLElementEvents2_ondragoverEventHandler HTMLElementEvents2_Event_ondragover;

		// Token: 0x14001903 RID: 6403
		// (add) Token: 0x0600CC10 RID: 52240
		// (remove) Token: 0x0600CC11 RID: 52241
		public virtual extern event HTMLElementEvents2_ondragleaveEventHandler HTMLElementEvents2_Event_ondragleave;

		// Token: 0x14001904 RID: 6404
		// (add) Token: 0x0600CC12 RID: 52242
		// (remove) Token: 0x0600CC13 RID: 52243
		public virtual extern event HTMLElementEvents2_ondropEventHandler HTMLElementEvents2_Event_ondrop;

		// Token: 0x14001905 RID: 6405
		// (add) Token: 0x0600CC14 RID: 52244
		// (remove) Token: 0x0600CC15 RID: 52245
		public virtual extern event HTMLElementEvents2_onbeforecutEventHandler HTMLElementEvents2_Event_onbeforecut;

		// Token: 0x14001906 RID: 6406
		// (add) Token: 0x0600CC16 RID: 52246
		// (remove) Token: 0x0600CC17 RID: 52247
		public virtual extern event HTMLElementEvents2_oncutEventHandler HTMLElementEvents2_Event_oncut;

		// Token: 0x14001907 RID: 6407
		// (add) Token: 0x0600CC18 RID: 52248
		// (remove) Token: 0x0600CC19 RID: 52249
		public virtual extern event HTMLElementEvents2_onbeforecopyEventHandler HTMLElementEvents2_Event_onbeforecopy;

		// Token: 0x14001908 RID: 6408
		// (add) Token: 0x0600CC1A RID: 52250
		// (remove) Token: 0x0600CC1B RID: 52251
		public virtual extern event HTMLElementEvents2_oncopyEventHandler HTMLElementEvents2_Event_oncopy;

		// Token: 0x14001909 RID: 6409
		// (add) Token: 0x0600CC1C RID: 52252
		// (remove) Token: 0x0600CC1D RID: 52253
		public virtual extern event HTMLElementEvents2_onbeforepasteEventHandler HTMLElementEvents2_Event_onbeforepaste;

		// Token: 0x1400190A RID: 6410
		// (add) Token: 0x0600CC1E RID: 52254
		// (remove) Token: 0x0600CC1F RID: 52255
		public virtual extern event HTMLElementEvents2_onpasteEventHandler HTMLElementEvents2_Event_onpaste;

		// Token: 0x1400190B RID: 6411
		// (add) Token: 0x0600CC20 RID: 52256
		// (remove) Token: 0x0600CC21 RID: 52257
		public virtual extern event HTMLElementEvents2_oncontextmenuEventHandler HTMLElementEvents2_Event_oncontextmenu;

		// Token: 0x1400190C RID: 6412
		// (add) Token: 0x0600CC22 RID: 52258
		// (remove) Token: 0x0600CC23 RID: 52259
		public virtual extern event HTMLElementEvents2_onrowsdeleteEventHandler HTMLElementEvents2_Event_onrowsdelete;

		// Token: 0x1400190D RID: 6413
		// (add) Token: 0x0600CC24 RID: 52260
		// (remove) Token: 0x0600CC25 RID: 52261
		public virtual extern event HTMLElementEvents2_onrowsinsertedEventHandler HTMLElementEvents2_Event_onrowsinserted;

		// Token: 0x1400190E RID: 6414
		// (add) Token: 0x0600CC26 RID: 52262
		// (remove) Token: 0x0600CC27 RID: 52263
		public virtual extern event HTMLElementEvents2_oncellchangeEventHandler HTMLElementEvents2_Event_oncellchange;

		// Token: 0x1400190F RID: 6415
		// (add) Token: 0x0600CC28 RID: 52264
		// (remove) Token: 0x0600CC29 RID: 52265
		public virtual extern event HTMLElementEvents2_onreadystatechangeEventHandler HTMLElementEvents2_Event_onreadystatechange;

		// Token: 0x14001910 RID: 6416
		// (add) Token: 0x0600CC2A RID: 52266
		// (remove) Token: 0x0600CC2B RID: 52267
		public virtual extern event HTMLElementEvents2_onlayoutcompleteEventHandler HTMLElementEvents2_Event_onlayoutcomplete;

		// Token: 0x14001911 RID: 6417
		// (add) Token: 0x0600CC2C RID: 52268
		// (remove) Token: 0x0600CC2D RID: 52269
		public virtual extern event HTMLElementEvents2_onpageEventHandler HTMLElementEvents2_Event_onpage;

		// Token: 0x14001912 RID: 6418
		// (add) Token: 0x0600CC2E RID: 52270
		// (remove) Token: 0x0600CC2F RID: 52271
		public virtual extern event HTMLElementEvents2_onmouseenterEventHandler HTMLElementEvents2_Event_onmouseenter;

		// Token: 0x14001913 RID: 6419
		// (add) Token: 0x0600CC30 RID: 52272
		// (remove) Token: 0x0600CC31 RID: 52273
		public virtual extern event HTMLElementEvents2_onmouseleaveEventHandler HTMLElementEvents2_Event_onmouseleave;

		// Token: 0x14001914 RID: 6420
		// (add) Token: 0x0600CC32 RID: 52274
		// (remove) Token: 0x0600CC33 RID: 52275
		public virtual extern event HTMLElementEvents2_onactivateEventHandler HTMLElementEvents2_Event_onactivate;

		// Token: 0x14001915 RID: 6421
		// (add) Token: 0x0600CC34 RID: 52276
		// (remove) Token: 0x0600CC35 RID: 52277
		public virtual extern event HTMLElementEvents2_ondeactivateEventHandler HTMLElementEvents2_Event_ondeactivate;

		// Token: 0x14001916 RID: 6422
		// (add) Token: 0x0600CC36 RID: 52278
		// (remove) Token: 0x0600CC37 RID: 52279
		public virtual extern event HTMLElementEvents2_onbeforedeactivateEventHandler HTMLElementEvents2_Event_onbeforedeactivate;

		// Token: 0x14001917 RID: 6423
		// (add) Token: 0x0600CC38 RID: 52280
		// (remove) Token: 0x0600CC39 RID: 52281
		public virtual extern event HTMLElementEvents2_onbeforeactivateEventHandler HTMLElementEvents2_Event_onbeforeactivate;

		// Token: 0x14001918 RID: 6424
		// (add) Token: 0x0600CC3A RID: 52282
		// (remove) Token: 0x0600CC3B RID: 52283
		public virtual extern event HTMLElementEvents2_onfocusinEventHandler HTMLElementEvents2_Event_onfocusin;

		// Token: 0x14001919 RID: 6425
		// (add) Token: 0x0600CC3C RID: 52284
		// (remove) Token: 0x0600CC3D RID: 52285
		public virtual extern event HTMLElementEvents2_onfocusoutEventHandler HTMLElementEvents2_Event_onfocusout;

		// Token: 0x1400191A RID: 6426
		// (add) Token: 0x0600CC3E RID: 52286
		// (remove) Token: 0x0600CC3F RID: 52287
		public virtual extern event HTMLElementEvents2_onmoveEventHandler HTMLElementEvents2_Event_onmove;

		// Token: 0x1400191B RID: 6427
		// (add) Token: 0x0600CC40 RID: 52288
		// (remove) Token: 0x0600CC41 RID: 52289
		public virtual extern event HTMLElementEvents2_oncontrolselectEventHandler HTMLElementEvents2_Event_oncontrolselect;

		// Token: 0x1400191C RID: 6428
		// (add) Token: 0x0600CC42 RID: 52290
		// (remove) Token: 0x0600CC43 RID: 52291
		public virtual extern event HTMLElementEvents2_onmovestartEventHandler HTMLElementEvents2_Event_onmovestart;

		// Token: 0x1400191D RID: 6429
		// (add) Token: 0x0600CC44 RID: 52292
		// (remove) Token: 0x0600CC45 RID: 52293
		public virtual extern event HTMLElementEvents2_onmoveendEventHandler HTMLElementEvents2_Event_onmoveend;

		// Token: 0x1400191E RID: 6430
		// (add) Token: 0x0600CC46 RID: 52294
		// (remove) Token: 0x0600CC47 RID: 52295
		public virtual extern event HTMLElementEvents2_onresizestartEventHandler HTMLElementEvents2_Event_onresizestart;

		// Token: 0x1400191F RID: 6431
		// (add) Token: 0x0600CC48 RID: 52296
		// (remove) Token: 0x0600CC49 RID: 52297
		public virtual extern event HTMLElementEvents2_onresizeendEventHandler HTMLElementEvents2_Event_onresizeend;

		// Token: 0x14001920 RID: 6432
		// (add) Token: 0x0600CC4A RID: 52298
		// (remove) Token: 0x0600CC4B RID: 52299
		public virtual extern event HTMLElementEvents2_onmousewheelEventHandler HTMLElementEvents2_Event_onmousewheel;
	}
}
