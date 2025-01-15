using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000A03 RID: 2563
	[TypeLibType(2)]
	[ComSourceInterfaces("mshtml.HTMLTableEvents\0mshtml.HTMLTableEvents2\0\0")]
	[Guid("3050F26B-98B5-11CF-BB82-00AA00BDCE0B")]
	[ClassInterface(0)]
	[ComImport]
	public class HTMLTableClass : DispHTMLTable, HTMLTable, HTMLTableEvents_Event, IHTMLElement, IHTMLElement2, IHTMLElement3, IHTMLElement4, IHTMLUniqueName, IHTMLDOMNode, IHTMLDOMNode2, IHTMLDatabinding, IHTMLControlElement, IHTMLTable, IHTMLTable2, IHTMLTable3, HTMLTableEvents2_Event
	{
		// Token: 0x06010558 RID: 66904
		[MethodImpl(4096, MethodCodeType = 3)]
		public extern HTMLTableClass();

		// Token: 0x06010559 RID: 66905
		[DispId(-2147417611)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void setAttribute([MarshalAs(19)] [In] string strAttributeName, [MarshalAs(27)] [In] object AttributeValue, [In] int lFlags = 1);

		// Token: 0x0601055A RID: 66906
		[DispId(-2147417610)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		public virtual extern object getAttribute([MarshalAs(19)] [In] string strAttributeName, [In] int lFlags = 0);

		// Token: 0x0601055B RID: 66907
		[DispId(-2147417609)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool removeAttribute([MarshalAs(19)] [In] string strAttributeName, [In] int lFlags = 1);

		// Token: 0x17005639 RID: 22073
		// (get) Token: 0x0601055D RID: 66909
		// (set) Token: 0x0601055C RID: 66908
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

		// Token: 0x1700563A RID: 22074
		// (get) Token: 0x0601055F RID: 66911
		// (set) Token: 0x0601055E RID: 66910
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

		// Token: 0x1700563B RID: 22075
		// (get) Token: 0x06010560 RID: 66912
		[DispId(-2147417108)]
		public virtual extern string tagName
		{
			[DispId(-2147417108)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x1700563C RID: 22076
		// (get) Token: 0x06010561 RID: 66913
		[DispId(-2147418104)]
		public virtual extern IHTMLElement parentElement
		{
			[DispId(-2147418104)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x1700563D RID: 22077
		// (get) Token: 0x06010562 RID: 66914
		[DispId(-2147418038)]
		public virtual extern IHTMLStyle style
		{
			[TypeLibFunc(1024)]
			[DispId(-2147418038)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x1700563E RID: 22078
		// (get) Token: 0x06010564 RID: 66916
		// (set) Token: 0x06010563 RID: 66915
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

		// Token: 0x1700563F RID: 22079
		// (get) Token: 0x06010566 RID: 66918
		// (set) Token: 0x06010565 RID: 66917
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

		// Token: 0x17005640 RID: 22080
		// (get) Token: 0x06010568 RID: 66920
		// (set) Token: 0x06010567 RID: 66919
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

		// Token: 0x17005641 RID: 22081
		// (get) Token: 0x0601056A RID: 66922
		// (set) Token: 0x06010569 RID: 66921
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

		// Token: 0x17005642 RID: 22082
		// (get) Token: 0x0601056C RID: 66924
		// (set) Token: 0x0601056B RID: 66923
		[DispId(-2147412106)]
		public virtual extern object onkeyup
		{
			[TypeLibFunc(20)]
			[DispId(-2147412106)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412106)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17005643 RID: 22083
		// (get) Token: 0x0601056E RID: 66926
		// (set) Token: 0x0601056D RID: 66925
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

		// Token: 0x17005644 RID: 22084
		// (get) Token: 0x06010570 RID: 66928
		// (set) Token: 0x0601056F RID: 66927
		[DispId(-2147412111)]
		public virtual extern object onmouseout
		{
			[DispId(-2147412111)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412111)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17005645 RID: 22085
		// (get) Token: 0x06010572 RID: 66930
		// (set) Token: 0x06010571 RID: 66929
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

		// Token: 0x17005646 RID: 22086
		// (get) Token: 0x06010574 RID: 66932
		// (set) Token: 0x06010573 RID: 66931
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

		// Token: 0x17005647 RID: 22087
		// (get) Token: 0x06010576 RID: 66934
		// (set) Token: 0x06010575 RID: 66933
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

		// Token: 0x17005648 RID: 22088
		// (get) Token: 0x06010578 RID: 66936
		// (set) Token: 0x06010577 RID: 66935
		[DispId(-2147412109)]
		public virtual extern object onmouseup
		{
			[TypeLibFunc(20)]
			[DispId(-2147412109)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412109)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17005649 RID: 22089
		// (get) Token: 0x06010579 RID: 66937
		[DispId(-2147417094)]
		public virtual extern object document
		{
			[DispId(-2147417094)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x1700564A RID: 22090
		// (get) Token: 0x0601057B RID: 66939
		// (set) Token: 0x0601057A RID: 66938
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

		// Token: 0x1700564B RID: 22091
		// (get) Token: 0x0601057D RID: 66941
		// (set) Token: 0x0601057C RID: 66940
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

		// Token: 0x1700564C RID: 22092
		// (get) Token: 0x0601057F RID: 66943
		// (set) Token: 0x0601057E RID: 66942
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

		// Token: 0x06010580 RID: 66944
		[DispId(-2147417093)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void scrollIntoView([MarshalAs(27)] [In] [Optional] object varargStart);

		// Token: 0x06010581 RID: 66945
		[DispId(-2147417092)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool contains([MarshalAs(28)] [In] IHTMLElement pChild);

		// Token: 0x1700564D RID: 22093
		// (get) Token: 0x06010582 RID: 66946
		[DispId(-2147417088)]
		public virtual extern int sourceIndex
		{
			[TypeLibFunc(4)]
			[DispId(-2147417088)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700564E RID: 22094
		// (get) Token: 0x06010583 RID: 66947
		[DispId(-2147417087)]
		public virtual extern object recordNumber
		{
			[DispId(-2147417087)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x1700564F RID: 22095
		// (get) Token: 0x06010585 RID: 66949
		// (set) Token: 0x06010584 RID: 66948
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

		// Token: 0x17005650 RID: 22096
		// (get) Token: 0x06010586 RID: 66950
		[DispId(-2147417104)]
		public virtual extern int offsetLeft
		{
			[DispId(-2147417104)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17005651 RID: 22097
		// (get) Token: 0x06010587 RID: 66951
		[DispId(-2147417103)]
		public virtual extern int offsetTop
		{
			[DispId(-2147417103)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17005652 RID: 22098
		// (get) Token: 0x06010588 RID: 66952
		[DispId(-2147417102)]
		public virtual extern int offsetWidth
		{
			[DispId(-2147417102)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17005653 RID: 22099
		// (get) Token: 0x06010589 RID: 66953
		[DispId(-2147417101)]
		public virtual extern int offsetHeight
		{
			[DispId(-2147417101)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17005654 RID: 22100
		// (get) Token: 0x0601058A RID: 66954
		[DispId(-2147417100)]
		public virtual extern IHTMLElement offsetParent
		{
			[DispId(-2147417100)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17005655 RID: 22101
		// (get) Token: 0x0601058C RID: 66956
		// (set) Token: 0x0601058B RID: 66955
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

		// Token: 0x17005656 RID: 22102
		// (get) Token: 0x0601058E RID: 66958
		// (set) Token: 0x0601058D RID: 66957
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

		// Token: 0x17005657 RID: 22103
		// (get) Token: 0x06010590 RID: 66960
		// (set) Token: 0x0601058F RID: 66959
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

		// Token: 0x17005658 RID: 22104
		// (get) Token: 0x06010592 RID: 66962
		// (set) Token: 0x06010591 RID: 66961
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

		// Token: 0x06010593 RID: 66963
		[DispId(-2147417082)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void insertAdjacentHTML([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string html);

		// Token: 0x06010594 RID: 66964
		[DispId(-2147417081)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void insertAdjacentText([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string text);

		// Token: 0x17005659 RID: 22105
		// (get) Token: 0x06010595 RID: 66965
		[DispId(-2147417080)]
		public virtual extern IHTMLElement parentTextEdit
		{
			[DispId(-2147417080)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x1700565A RID: 22106
		// (get) Token: 0x06010596 RID: 66966
		[DispId(-2147417078)]
		public virtual extern bool isTextEdit
		{
			[DispId(-2147417078)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x06010597 RID: 66967
		[DispId(-2147417079)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void click();

		// Token: 0x1700565B RID: 22107
		// (get) Token: 0x06010598 RID: 66968
		[DispId(-2147417077)]
		public virtual extern IHTMLFiltersCollection filters
		{
			[DispId(-2147417077)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x1700565C RID: 22108
		// (get) Token: 0x0601059A RID: 66970
		// (set) Token: 0x06010599 RID: 66969
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

		// Token: 0x0601059B RID: 66971
		[DispId(-2147417076)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string toString();

		// Token: 0x1700565D RID: 22109
		// (get) Token: 0x0601059D RID: 66973
		// (set) Token: 0x0601059C RID: 66972
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

		// Token: 0x1700565E RID: 22110
		// (get) Token: 0x0601059F RID: 66975
		// (set) Token: 0x0601059E RID: 66974
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

		// Token: 0x1700565F RID: 22111
		// (get) Token: 0x060105A1 RID: 66977
		// (set) Token: 0x060105A0 RID: 66976
		[DispId(-2147412074)]
		public virtual extern object onerrorupdate
		{
			[TypeLibFunc(20)]
			[DispId(-2147412074)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412074)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17005660 RID: 22112
		// (get) Token: 0x060105A3 RID: 66979
		// (set) Token: 0x060105A2 RID: 66978
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

		// Token: 0x17005661 RID: 22113
		// (get) Token: 0x060105A5 RID: 66981
		// (set) Token: 0x060105A4 RID: 66980
		[DispId(-2147412093)]
		public virtual extern object onrowenter
		{
			[TypeLibFunc(20)]
			[DispId(-2147412093)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412093)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17005662 RID: 22114
		// (get) Token: 0x060105A7 RID: 66983
		// (set) Token: 0x060105A6 RID: 66982
		[DispId(-2147412072)]
		public virtual extern object ondatasetchanged
		{
			[DispId(-2147412072)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412072)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17005663 RID: 22115
		// (get) Token: 0x060105A9 RID: 66985
		// (set) Token: 0x060105A8 RID: 66984
		[DispId(-2147412071)]
		public virtual extern object ondataavailable
		{
			[DispId(-2147412071)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412071)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17005664 RID: 22116
		// (get) Token: 0x060105AB RID: 66987
		// (set) Token: 0x060105AA RID: 66986
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

		// Token: 0x17005665 RID: 22117
		// (get) Token: 0x060105AD RID: 66989
		// (set) Token: 0x060105AC RID: 66988
		[DispId(-2147412069)]
		public virtual extern object onfilterchange
		{
			[DispId(-2147412069)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412069)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17005666 RID: 22118
		// (get) Token: 0x060105AE RID: 66990
		[DispId(-2147417075)]
		public virtual extern object children
		{
			[DispId(-2147417075)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17005667 RID: 22119
		// (get) Token: 0x060105AF RID: 66991
		[DispId(-2147417074)]
		public virtual extern object all
		{
			[DispId(-2147417074)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17005668 RID: 22120
		// (get) Token: 0x060105B0 RID: 66992
		[DispId(-2147417073)]
		public virtual extern string scopeName
		{
			[DispId(-2147417073)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x060105B1 RID: 66993
		[DispId(-2147417072)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void setCapture([In] bool containerCapture = true);

		// Token: 0x060105B2 RID: 66994
		[DispId(-2147417071)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void releaseCapture();

		// Token: 0x17005669 RID: 22121
		// (get) Token: 0x060105B4 RID: 66996
		// (set) Token: 0x060105B3 RID: 66995
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

		// Token: 0x060105B5 RID: 66997
		[DispId(-2147417070)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string componentFromPoint([In] int x, [In] int y);

		// Token: 0x060105B6 RID: 66998
		[DispId(-2147417069)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void doScroll([MarshalAs(27)] [In] [Optional] object component);

		// Token: 0x1700566A RID: 22122
		// (get) Token: 0x060105B8 RID: 67000
		// (set) Token: 0x060105B7 RID: 66999
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

		// Token: 0x1700566B RID: 22123
		// (get) Token: 0x060105BA RID: 67002
		// (set) Token: 0x060105B9 RID: 67001
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

		// Token: 0x1700566C RID: 22124
		// (get) Token: 0x060105BC RID: 67004
		// (set) Token: 0x060105BB RID: 67003
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

		// Token: 0x1700566D RID: 22125
		// (get) Token: 0x060105BE RID: 67006
		// (set) Token: 0x060105BD RID: 67005
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

		// Token: 0x1700566E RID: 22126
		// (get) Token: 0x060105C0 RID: 67008
		// (set) Token: 0x060105BF RID: 67007
		[DispId(-2147412060)]
		public virtual extern object ondragover
		{
			[TypeLibFunc(20)]
			[DispId(-2147412060)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412060)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x1700566F RID: 22127
		// (get) Token: 0x060105C2 RID: 67010
		// (set) Token: 0x060105C1 RID: 67009
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

		// Token: 0x17005670 RID: 22128
		// (get) Token: 0x060105C4 RID: 67012
		// (set) Token: 0x060105C3 RID: 67011
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

		// Token: 0x17005671 RID: 22129
		// (get) Token: 0x060105C6 RID: 67014
		// (set) Token: 0x060105C5 RID: 67013
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

		// Token: 0x17005672 RID: 22130
		// (get) Token: 0x060105C8 RID: 67016
		// (set) Token: 0x060105C7 RID: 67015
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

		// Token: 0x17005673 RID: 22131
		// (get) Token: 0x060105CA RID: 67018
		// (set) Token: 0x060105C9 RID: 67017
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

		// Token: 0x17005674 RID: 22132
		// (get) Token: 0x060105CC RID: 67020
		// (set) Token: 0x060105CB RID: 67019
		[DispId(-2147412056)]
		public virtual extern object oncopy
		{
			[TypeLibFunc(20)]
			[DispId(-2147412056)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412056)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17005675 RID: 22133
		// (get) Token: 0x060105CE RID: 67022
		// (set) Token: 0x060105CD RID: 67021
		[DispId(-2147412052)]
		public virtual extern object onbeforepaste
		{
			[DispId(-2147412052)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412052)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17005676 RID: 22134
		// (get) Token: 0x060105D0 RID: 67024
		// (set) Token: 0x060105CF RID: 67023
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

		// Token: 0x17005677 RID: 22135
		// (get) Token: 0x060105D1 RID: 67025
		[DispId(-2147417105)]
		public virtual extern IHTMLCurrentStyle currentStyle
		{
			[DispId(-2147417105)]
			[TypeLibFunc(1024)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17005678 RID: 22136
		// (get) Token: 0x060105D3 RID: 67027
		// (set) Token: 0x060105D2 RID: 67026
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

		// Token: 0x060105D4 RID: 67028
		[DispId(-2147417068)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLRectCollection getClientRects();

		// Token: 0x060105D5 RID: 67029
		[DispId(-2147417067)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLRect getBoundingClientRect();

		// Token: 0x060105D6 RID: 67030
		[DispId(-2147417608)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void setExpression([MarshalAs(19)] [In] string propname, [MarshalAs(19)] [In] string expression, [MarshalAs(19)] [In] string language = "");

		// Token: 0x060105D7 RID: 67031
		[DispId(-2147417607)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		public virtual extern object getExpression([MarshalAs(19)] [In] string propname);

		// Token: 0x060105D8 RID: 67032
		[DispId(-2147417606)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool removeExpression([MarshalAs(19)] [In] string propname);

		// Token: 0x17005679 RID: 22137
		// (get) Token: 0x060105DA RID: 67034
		// (set) Token: 0x060105D9 RID: 67033
		[DispId(-2147418097)]
		public virtual extern short tabIndex
		{
			[DispId(-2147418097)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[DispId(-2147418097)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x060105DB RID: 67035
		[DispId(-2147416112)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void focus();

		// Token: 0x1700567A RID: 22138
		// (get) Token: 0x060105DD RID: 67037
		// (set) Token: 0x060105DC RID: 67036
		[DispId(-2147416107)]
		public virtual extern string accessKey
		{
			[TypeLibFunc(20)]
			[DispId(-2147416107)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147416107)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x1700567B RID: 22139
		// (get) Token: 0x060105DF RID: 67039
		// (set) Token: 0x060105DE RID: 67038
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

		// Token: 0x1700567C RID: 22140
		// (get) Token: 0x060105E1 RID: 67041
		// (set) Token: 0x060105E0 RID: 67040
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

		// Token: 0x1700567D RID: 22141
		// (get) Token: 0x060105E3 RID: 67043
		// (set) Token: 0x060105E2 RID: 67042
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

		// Token: 0x060105E4 RID: 67044
		[DispId(-2147416110)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void blur();

		// Token: 0x060105E5 RID: 67045
		[DispId(-2147416095)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void addFilter([MarshalAs(25)] [In] object pUnk);

		// Token: 0x060105E6 RID: 67046
		[DispId(-2147416094)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void removeFilter([MarshalAs(25)] [In] object pUnk);

		// Token: 0x1700567E RID: 22142
		// (get) Token: 0x060105E7 RID: 67047
		[DispId(-2147416093)]
		public virtual extern int clientHeight
		{
			[DispId(-2147416093)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700567F RID: 22143
		// (get) Token: 0x060105E8 RID: 67048
		[DispId(-2147416092)]
		public virtual extern int clientWidth
		{
			[DispId(-2147416092)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17005680 RID: 22144
		// (get) Token: 0x060105E9 RID: 67049
		[DispId(-2147416091)]
		public virtual extern int clientTop
		{
			[TypeLibFunc(20)]
			[DispId(-2147416091)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17005681 RID: 22145
		// (get) Token: 0x060105EA RID: 67050
		[DispId(-2147416090)]
		public virtual extern int clientLeft
		{
			[TypeLibFunc(20)]
			[DispId(-2147416090)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x060105EB RID: 67051
		[DispId(-2147417605)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool attachEvent([MarshalAs(19)] [In] string @event, [MarshalAs(26)] [In] object pdisp);

		// Token: 0x060105EC RID: 67052
		[DispId(-2147417604)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void detachEvent([MarshalAs(19)] [In] string @event, [MarshalAs(26)] [In] object pdisp);

		// Token: 0x17005682 RID: 22146
		// (get) Token: 0x060105ED RID: 67053
		[DispId(-2147412996)]
		public virtual extern object readyState
		{
			[DispId(-2147412996)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x17005683 RID: 22147
		// (get) Token: 0x060105EF RID: 67055
		// (set) Token: 0x060105EE RID: 67054
		[DispId(-2147412087)]
		public virtual extern object onreadystatechange
		{
			[DispId(-2147412087)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412087)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17005684 RID: 22148
		// (get) Token: 0x060105F1 RID: 67057
		// (set) Token: 0x060105F0 RID: 67056
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

		// Token: 0x17005685 RID: 22149
		// (get) Token: 0x060105F3 RID: 67059
		// (set) Token: 0x060105F2 RID: 67058
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

		// Token: 0x17005686 RID: 22150
		// (get) Token: 0x060105F5 RID: 67061
		// (set) Token: 0x060105F4 RID: 67060
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

		// Token: 0x17005687 RID: 22151
		// (get) Token: 0x060105F7 RID: 67063
		// (set) Token: 0x060105F6 RID: 67062
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

		// Token: 0x060105F8 RID: 67064
		[DispId(-2147417056)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(26)]
		public virtual extern object createControlRange();

		// Token: 0x17005688 RID: 22152
		// (get) Token: 0x060105F9 RID: 67065
		[DispId(-2147417055)]
		public virtual extern int scrollHeight
		{
			[TypeLibFunc(20)]
			[DispId(-2147417055)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17005689 RID: 22153
		// (get) Token: 0x060105FA RID: 67066
		[DispId(-2147417054)]
		public virtual extern int scrollWidth
		{
			[TypeLibFunc(20)]
			[DispId(-2147417054)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700568A RID: 22154
		// (get) Token: 0x060105FC RID: 67068
		// (set) Token: 0x060105FB RID: 67067
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

		// Token: 0x1700568B RID: 22155
		// (get) Token: 0x060105FE RID: 67070
		// (set) Token: 0x060105FD RID: 67069
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

		// Token: 0x060105FF RID: 67071
		[DispId(-2147417050)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void clearAttributes();

		// Token: 0x1700568C RID: 22156
		// (get) Token: 0x06010601 RID: 67073
		// (set) Token: 0x06010600 RID: 67072
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

		// Token: 0x06010602 RID: 67074
		[DispId(-2147417043)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElement insertAdjacentElement([MarshalAs(19)] [In] string where, [MarshalAs(28)] [In] IHTMLElement insertedElement);

		// Token: 0x06010603 RID: 67075
		[DispId(-2147417047)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElement applyElement([MarshalAs(28)] [In] IHTMLElement apply, [MarshalAs(19)] [In] string where);

		// Token: 0x06010604 RID: 67076
		[DispId(-2147417042)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string getAdjacentText([MarshalAs(19)] [In] string where);

		// Token: 0x06010605 RID: 67077
		[DispId(-2147417041)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string replaceAdjacentText([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string newText);

		// Token: 0x1700568D RID: 22157
		// (get) Token: 0x06010606 RID: 67078
		[DispId(-2147417040)]
		public virtual extern bool canHaveChildren
		{
			[DispId(-2147417040)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x06010607 RID: 67079
		[DispId(-2147417032)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern int addBehavior([MarshalAs(19)] [In] string bstrUrl, [MarshalAs(27)] [In] [Optional] ref object pvarFactory);

		// Token: 0x06010608 RID: 67080
		[DispId(-2147417031)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool removeBehavior([In] int cookie);

		// Token: 0x1700568E RID: 22158
		// (get) Token: 0x06010609 RID: 67081
		[DispId(-2147417048)]
		public virtual extern IHTMLStyle runtimeStyle
		{
			[TypeLibFunc(1024)]
			[DispId(-2147417048)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x1700568F RID: 22159
		// (get) Token: 0x0601060A RID: 67082
		[DispId(-2147417030)]
		public virtual extern object behaviorUrns
		{
			[DispId(-2147417030)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17005690 RID: 22160
		// (get) Token: 0x0601060C RID: 67084
		// (set) Token: 0x0601060B RID: 67083
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

		// Token: 0x17005691 RID: 22161
		// (get) Token: 0x0601060E RID: 67086
		// (set) Token: 0x0601060D RID: 67085
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

		// Token: 0x17005692 RID: 22162
		// (get) Token: 0x0601060F RID: 67087
		[DispId(-2147417028)]
		public virtual extern int readyStateValue
		{
			[DispId(-2147417028)]
			[TypeLibFunc(65)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x06010610 RID: 67088
		[DispId(-2147417027)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElementCollection getElementsByTagName([MarshalAs(19)] [In] string v);

		// Token: 0x06010611 RID: 67089
		[DispId(-2147417016)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void mergeAttributes([MarshalAs(28)] [In] IHTMLElement mergeThis, [MarshalAs(27)] [In] [Optional] ref object pvarFlags);

		// Token: 0x17005693 RID: 22163
		// (get) Token: 0x06010612 RID: 67090
		[DispId(-2147417015)]
		public virtual extern bool isMultiLine
		{
			[DispId(-2147417015)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17005694 RID: 22164
		// (get) Token: 0x06010613 RID: 67091
		[DispId(-2147417014)]
		public virtual extern bool canHaveHTML
		{
			[DispId(-2147417014)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17005695 RID: 22165
		// (get) Token: 0x06010615 RID: 67093
		// (set) Token: 0x06010614 RID: 67092
		[DispId(-2147412039)]
		public virtual extern object onlayoutcomplete
		{
			[DispId(-2147412039)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412039)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17005696 RID: 22166
		// (get) Token: 0x06010617 RID: 67095
		// (set) Token: 0x06010616 RID: 67094
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

		// Token: 0x17005697 RID: 22167
		// (get) Token: 0x06010619 RID: 67097
		// (set) Token: 0x06010618 RID: 67096
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

		// Token: 0x17005698 RID: 22168
		// (get) Token: 0x0601061B RID: 67099
		// (set) Token: 0x0601061A RID: 67098
		[DispId(-2147412035)]
		public virtual extern object onbeforedeactivate
		{
			[TypeLibFunc(20)]
			[DispId(-2147412035)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412035)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x0601061C RID: 67100
		[DispId(-2147417011)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void setActive();

		// Token: 0x17005699 RID: 22169
		// (get) Token: 0x0601061E RID: 67102
		// (set) Token: 0x0601061D RID: 67101
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

		// Token: 0x1700569A RID: 22170
		// (get) Token: 0x0601061F RID: 67103
		[DispId(-2147417010)]
		public virtual extern bool isContentEditable
		{
			[DispId(-2147417010)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700569B RID: 22171
		// (get) Token: 0x06010621 RID: 67105
		// (set) Token: 0x06010620 RID: 67104
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

		// Token: 0x1700569C RID: 22172
		// (get) Token: 0x06010623 RID: 67107
		// (set) Token: 0x06010622 RID: 67106
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

		// Token: 0x1700569D RID: 22173
		// (get) Token: 0x06010624 RID: 67108
		[DispId(-2147417007)]
		public virtual extern bool isDisabled
		{
			[DispId(-2147417007)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700569E RID: 22174
		// (get) Token: 0x06010626 RID: 67110
		// (set) Token: 0x06010625 RID: 67109
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

		// Token: 0x1700569F RID: 22175
		// (get) Token: 0x06010628 RID: 67112
		// (set) Token: 0x06010627 RID: 67111
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

		// Token: 0x06010629 RID: 67113
		[DispId(-2147417006)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool FireEvent([MarshalAs(19)] [In] string bstrEventName, [MarshalAs(27)] [In] [Optional] ref object pvarEventObject);

		// Token: 0x170056A0 RID: 22176
		// (get) Token: 0x0601062B RID: 67115
		// (set) Token: 0x0601062A RID: 67114
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

		// Token: 0x170056A1 RID: 22177
		// (get) Token: 0x0601062D RID: 67117
		// (set) Token: 0x0601062C RID: 67116
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

		// Token: 0x170056A2 RID: 22178
		// (get) Token: 0x0601062F RID: 67119
		// (set) Token: 0x0601062E RID: 67118
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

		// Token: 0x170056A3 RID: 22179
		// (get) Token: 0x06010631 RID: 67121
		// (set) Token: 0x06010630 RID: 67120
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

		// Token: 0x170056A4 RID: 22180
		// (get) Token: 0x06010633 RID: 67123
		// (set) Token: 0x06010632 RID: 67122
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

		// Token: 0x170056A5 RID: 22181
		// (get) Token: 0x06010635 RID: 67125
		// (set) Token: 0x06010634 RID: 67124
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

		// Token: 0x170056A6 RID: 22182
		// (get) Token: 0x06010637 RID: 67127
		// (set) Token: 0x06010636 RID: 67126
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

		// Token: 0x170056A7 RID: 22183
		// (get) Token: 0x06010639 RID: 67129
		// (set) Token: 0x06010638 RID: 67128
		[DispId(-2147412024)]
		public virtual extern object ondeactivate
		{
			[DispId(-2147412024)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412024)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x0601063A RID: 67130
		[DispId(-2147417005)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool dragDrop();

		// Token: 0x170056A8 RID: 22184
		// (get) Token: 0x0601063B RID: 67131
		[DispId(-2147417004)]
		public virtual extern int glyphMode
		{
			[DispId(-2147417004)]
			[TypeLibFunc(1089)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170056A9 RID: 22185
		// (get) Token: 0x0601063D RID: 67133
		// (set) Token: 0x0601063C RID: 67132
		[DispId(-2147412036)]
		public virtual extern object onmousewheel
		{
			[TypeLibFunc(20)]
			[DispId(-2147412036)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412036)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x0601063E RID: 67134
		[DispId(-2147417000)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void normalize();

		// Token: 0x0601063F RID: 67135
		[DispId(-2147417003)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute getAttributeNode([MarshalAs(19)] [In] string bstrName);

		// Token: 0x06010640 RID: 67136
		[DispId(-2147417002)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute setAttributeNode([MarshalAs(28)] [In] IHTMLDOMAttribute pattr);

		// Token: 0x06010641 RID: 67137
		[DispId(-2147417001)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute removeAttributeNode([MarshalAs(28)] [In] IHTMLDOMAttribute pattr);

		// Token: 0x170056AA RID: 22186
		// (get) Token: 0x06010643 RID: 67139
		// (set) Token: 0x06010642 RID: 67138
		[DispId(-2147412022)]
		public virtual extern object onbeforeactivate
		{
			[DispId(-2147412022)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412022)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170056AB RID: 22187
		// (get) Token: 0x06010645 RID: 67141
		// (set) Token: 0x06010644 RID: 67140
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

		// Token: 0x170056AC RID: 22188
		// (get) Token: 0x06010647 RID: 67143
		// (set) Token: 0x06010646 RID: 67142
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

		// Token: 0x170056AD RID: 22189
		// (get) Token: 0x06010648 RID: 67144
		[DispId(-2147417058)]
		public virtual extern int uniqueNumber
		{
			[TypeLibFunc(64)]
			[DispId(-2147417058)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170056AE RID: 22190
		// (get) Token: 0x06010649 RID: 67145
		[DispId(-2147417057)]
		public virtual extern string uniqueID
		{
			[DispId(-2147417057)]
			[TypeLibFunc(64)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170056AF RID: 22191
		// (get) Token: 0x0601064A RID: 67146
		[DispId(-2147417066)]
		public virtual extern int nodeType
		{
			[DispId(-2147417066)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170056B0 RID: 22192
		// (get) Token: 0x0601064B RID: 67147
		[DispId(-2147417065)]
		public virtual extern IHTMLDOMNode parentNode
		{
			[DispId(-2147417065)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x0601064C RID: 67148
		[DispId(-2147417064)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool hasChildNodes();

		// Token: 0x170056B1 RID: 22193
		// (get) Token: 0x0601064D RID: 67149
		[DispId(-2147417063)]
		public virtual extern object childNodes
		{
			[DispId(-2147417063)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x170056B2 RID: 22194
		// (get) Token: 0x0601064E RID: 67150
		[DispId(-2147417062)]
		public virtual extern object attributes
		{
			[DispId(-2147417062)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x0601064F RID: 67151
		[DispId(-2147417061)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode insertBefore([MarshalAs(28)] [In] IHTMLDOMNode newChild, [MarshalAs(27)] [In] [Optional] object refChild);

		// Token: 0x06010650 RID: 67152
		[DispId(-2147417060)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode removeChild([MarshalAs(28)] [In] IHTMLDOMNode oldChild);

		// Token: 0x06010651 RID: 67153
		[DispId(-2147417059)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode replaceChild([MarshalAs(28)] [In] IHTMLDOMNode newChild, [MarshalAs(28)] [In] IHTMLDOMNode oldChild);

		// Token: 0x06010652 RID: 67154
		[DispId(-2147417051)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode cloneNode([In] bool fDeep);

		// Token: 0x06010653 RID: 67155
		[DispId(-2147417046)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode removeNode([In] bool fDeep = false);

		// Token: 0x06010654 RID: 67156
		[DispId(-2147417044)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode swapNode([MarshalAs(28)] [In] IHTMLDOMNode otherNode);

		// Token: 0x06010655 RID: 67157
		[DispId(-2147417045)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode replaceNode([MarshalAs(28)] [In] IHTMLDOMNode replacement);

		// Token: 0x06010656 RID: 67158
		[DispId(-2147417039)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode appendChild([MarshalAs(28)] [In] IHTMLDOMNode newChild);

		// Token: 0x170056B3 RID: 22195
		// (get) Token: 0x06010657 RID: 67159
		[DispId(-2147417038)]
		public virtual extern string nodeName
		{
			[DispId(-2147417038)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170056B4 RID: 22196
		// (get) Token: 0x06010659 RID: 67161
		// (set) Token: 0x06010658 RID: 67160
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

		// Token: 0x170056B5 RID: 22197
		// (get) Token: 0x0601065A RID: 67162
		[DispId(-2147417036)]
		public virtual extern IHTMLDOMNode firstChild
		{
			[DispId(-2147417036)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170056B6 RID: 22198
		// (get) Token: 0x0601065B RID: 67163
		[DispId(-2147417035)]
		public virtual extern IHTMLDOMNode lastChild
		{
			[DispId(-2147417035)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170056B7 RID: 22199
		// (get) Token: 0x0601065C RID: 67164
		[DispId(-2147417034)]
		public virtual extern IHTMLDOMNode previousSibling
		{
			[DispId(-2147417034)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170056B8 RID: 22200
		// (get) Token: 0x0601065D RID: 67165
		[DispId(-2147417033)]
		public virtual extern IHTMLDOMNode nextSibling
		{
			[DispId(-2147417033)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170056B9 RID: 22201
		// (get) Token: 0x0601065E RID: 67166
		[DispId(-2147416999)]
		public virtual extern object ownerDocument
		{
			[DispId(-2147416999)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x170056BA RID: 22202
		// (get) Token: 0x06010660 RID: 67168
		// (set) Token: 0x0601065F RID: 67167
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

		// Token: 0x170056BB RID: 22203
		// (get) Token: 0x06010662 RID: 67170
		// (set) Token: 0x06010661 RID: 67169
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

		// Token: 0x170056BC RID: 22204
		// (get) Token: 0x06010664 RID: 67172
		// (set) Token: 0x06010663 RID: 67171
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

		// Token: 0x170056BD RID: 22205
		// (get) Token: 0x06010666 RID: 67174
		// (set) Token: 0x06010665 RID: 67173
		[DispId(1001)]
		public virtual extern int cols
		{
			[DispId(1001)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[DispId(1001)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x170056BE RID: 22206
		// (get) Token: 0x06010668 RID: 67176
		// (set) Token: 0x06010667 RID: 67175
		[DispId(1002)]
		public virtual extern object border
		{
			[DispId(1002)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(1002)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170056BF RID: 22207
		// (get) Token: 0x0601066A RID: 67178
		// (set) Token: 0x06010669 RID: 67177
		[DispId(1004)]
		public virtual extern string frame
		{
			[DispId(1004)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(1004)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x170056C0 RID: 22208
		// (get) Token: 0x0601066C RID: 67180
		// (set) Token: 0x0601066B RID: 67179
		[DispId(1003)]
		public virtual extern string rules
		{
			[DispId(1003)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(1003)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x170056C1 RID: 22209
		// (get) Token: 0x0601066E RID: 67182
		// (set) Token: 0x0601066D RID: 67181
		[DispId(1005)]
		public virtual extern object cellSpacing
		{
			[DispId(1005)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(1005)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170056C2 RID: 22210
		// (get) Token: 0x06010670 RID: 67184
		// (set) Token: 0x0601066F RID: 67183
		[DispId(1006)]
		public virtual extern object cellPadding
		{
			[DispId(1006)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(1006)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170056C3 RID: 22211
		// (get) Token: 0x06010672 RID: 67186
		// (set) Token: 0x06010671 RID: 67185
		[DispId(-2147413111)]
		public virtual extern string background
		{
			[DispId(-2147413111)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147413111)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x170056C4 RID: 22212
		// (get) Token: 0x06010674 RID: 67188
		// (set) Token: 0x06010673 RID: 67187
		[DispId(-501)]
		public virtual extern object bgColor
		{
			[DispId(-501)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-501)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170056C5 RID: 22213
		// (get) Token: 0x06010676 RID: 67190
		// (set) Token: 0x06010675 RID: 67189
		[DispId(-2147413084)]
		public virtual extern object borderColor
		{
			[DispId(-2147413084)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147413084)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170056C6 RID: 22214
		// (get) Token: 0x06010678 RID: 67192
		// (set) Token: 0x06010677 RID: 67191
		[DispId(-2147413083)]
		public virtual extern object borderColorLight
		{
			[DispId(-2147413083)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147413083)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170056C7 RID: 22215
		// (get) Token: 0x0601067A RID: 67194
		// (set) Token: 0x06010679 RID: 67193
		[DispId(-2147413082)]
		public virtual extern object borderColorDark
		{
			[DispId(-2147413082)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147413082)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170056C8 RID: 22216
		// (get) Token: 0x0601067C RID: 67196
		// (set) Token: 0x0601067B RID: 67195
		[DispId(-2147418039)]
		public virtual extern string align
		{
			[DispId(-2147418039)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147418039)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x0601067D RID: 67197
		[DispId(1015)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void refresh();

		// Token: 0x170056C9 RID: 22217
		// (get) Token: 0x0601067E RID: 67198
		[DispId(1016)]
		public virtual extern IHTMLElementCollection rows
		{
			[DispId(1016)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170056CA RID: 22218
		// (get) Token: 0x06010680 RID: 67200
		// (set) Token: 0x0601067F RID: 67199
		[DispId(-2147418107)]
		public virtual extern object width
		{
			[DispId(-2147418107)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147418107)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170056CB RID: 22219
		// (get) Token: 0x06010682 RID: 67202
		// (set) Token: 0x06010681 RID: 67201
		[DispId(-2147418106)]
		public virtual extern object height
		{
			[DispId(-2147418106)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147418106)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170056CC RID: 22220
		// (get) Token: 0x06010684 RID: 67204
		// (set) Token: 0x06010683 RID: 67203
		[DispId(1017)]
		public virtual extern int dataPageSize
		{
			[DispId(1017)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[DispId(1017)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x06010685 RID: 67205
		[DispId(1018)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void nextPage();

		// Token: 0x06010686 RID: 67206
		[DispId(1019)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void previousPage();

		// Token: 0x170056CD RID: 22221
		// (get) Token: 0x06010687 RID: 67207
		[DispId(1020)]
		public virtual extern IHTMLTableSection tHead
		{
			[DispId(1020)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170056CE RID: 22222
		// (get) Token: 0x06010688 RID: 67208
		[DispId(1021)]
		public virtual extern IHTMLTableSection tFoot
		{
			[DispId(1021)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170056CF RID: 22223
		// (get) Token: 0x06010689 RID: 67209
		[DispId(1024)]
		public virtual extern IHTMLElementCollection tBodies
		{
			[DispId(1024)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170056D0 RID: 22224
		// (get) Token: 0x0601068A RID: 67210
		[DispId(1025)]
		public virtual extern IHTMLTableCaption caption
		{
			[DispId(1025)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x0601068B RID: 67211
		[DispId(1026)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(26)]
		public virtual extern object createTHead();

		// Token: 0x0601068C RID: 67212
		[DispId(1027)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void deleteTHead();

		// Token: 0x0601068D RID: 67213
		[DispId(1028)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(26)]
		public virtual extern object createTFoot();

		// Token: 0x0601068E RID: 67214
		[DispId(1029)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void deleteTFoot();

		// Token: 0x0601068F RID: 67215
		[DispId(1030)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLTableCaption createCaption();

		// Token: 0x06010690 RID: 67216
		[DispId(1031)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void deleteCaption();

		// Token: 0x06010691 RID: 67217
		[DispId(1032)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(26)]
		public virtual extern object insertRow([In] int index = -1);

		// Token: 0x06010692 RID: 67218
		[DispId(1033)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void deleteRow([In] int index = -1);

		// Token: 0x06010693 RID: 67219
		[DispId(1035)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void firstPage();

		// Token: 0x06010694 RID: 67220
		[DispId(1036)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void lastPage();

		// Token: 0x170056D1 RID: 22225
		// (get) Token: 0x06010695 RID: 67221
		[DispId(1037)]
		public virtual extern IHTMLElementCollection cells
		{
			[DispId(1037)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x06010696 RID: 67222
		[DispId(1038)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(26)]
		public virtual extern object moveRow([In] int indexFrom = -1, [In] int indexTo = -1);

		// Token: 0x170056D2 RID: 22226
		// (get) Token: 0x06010698 RID: 67224
		// (set) Token: 0x06010697 RID: 67223
		[DispId(1039)]
		public virtual extern string summary
		{
			[DispId(1039)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(1039)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x06010699 RID: 67225
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement_setAttribute([MarshalAs(19)] [In] string strAttributeName, [MarshalAs(27)] [In] object AttributeValue, [In] int lFlags = 1);

		// Token: 0x0601069A RID: 67226
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		public virtual extern object IHTMLElement_getAttribute([MarshalAs(19)] [In] string strAttributeName, [In] int lFlags = 0);

		// Token: 0x0601069B RID: 67227
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement_removeAttribute([MarshalAs(19)] [In] string strAttributeName, [In] int lFlags = 1);

		// Token: 0x170056D3 RID: 22227
		// (get) Token: 0x0601069D RID: 67229
		// (set) Token: 0x0601069C RID: 67228
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

		// Token: 0x170056D4 RID: 22228
		// (get) Token: 0x0601069F RID: 67231
		// (set) Token: 0x0601069E RID: 67230
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

		// Token: 0x170056D5 RID: 22229
		// (get) Token: 0x060106A0 RID: 67232
		public virtual extern string IHTMLElement_tagName
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170056D6 RID: 22230
		// (get) Token: 0x060106A1 RID: 67233
		public virtual extern IHTMLElement IHTMLElement_parentElement
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170056D7 RID: 22231
		// (get) Token: 0x060106A2 RID: 67234
		public virtual extern IHTMLStyle IHTMLElement_style
		{
			[TypeLibFunc(1024)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170056D8 RID: 22232
		// (get) Token: 0x060106A4 RID: 67236
		// (set) Token: 0x060106A3 RID: 67235
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

		// Token: 0x170056D9 RID: 22233
		// (get) Token: 0x060106A6 RID: 67238
		// (set) Token: 0x060106A5 RID: 67237
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

		// Token: 0x170056DA RID: 22234
		// (get) Token: 0x060106A8 RID: 67240
		// (set) Token: 0x060106A7 RID: 67239
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

		// Token: 0x170056DB RID: 22235
		// (get) Token: 0x060106AA RID: 67242
		// (set) Token: 0x060106A9 RID: 67241
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

		// Token: 0x170056DC RID: 22236
		// (get) Token: 0x060106AC RID: 67244
		// (set) Token: 0x060106AB RID: 67243
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

		// Token: 0x170056DD RID: 22237
		// (get) Token: 0x060106AE RID: 67246
		// (set) Token: 0x060106AD RID: 67245
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

		// Token: 0x170056DE RID: 22238
		// (get) Token: 0x060106B0 RID: 67248
		// (set) Token: 0x060106AF RID: 67247
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

		// Token: 0x170056DF RID: 22239
		// (get) Token: 0x060106B2 RID: 67250
		// (set) Token: 0x060106B1 RID: 67249
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

		// Token: 0x170056E0 RID: 22240
		// (get) Token: 0x060106B4 RID: 67252
		// (set) Token: 0x060106B3 RID: 67251
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

		// Token: 0x170056E1 RID: 22241
		// (get) Token: 0x060106B6 RID: 67254
		// (set) Token: 0x060106B5 RID: 67253
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

		// Token: 0x170056E2 RID: 22242
		// (get) Token: 0x060106B8 RID: 67256
		// (set) Token: 0x060106B7 RID: 67255
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

		// Token: 0x170056E3 RID: 22243
		// (get) Token: 0x060106B9 RID: 67257
		public virtual extern object IHTMLElement_document
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x170056E4 RID: 22244
		// (get) Token: 0x060106BB RID: 67259
		// (set) Token: 0x060106BA RID: 67258
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

		// Token: 0x170056E5 RID: 22245
		// (get) Token: 0x060106BD RID: 67261
		// (set) Token: 0x060106BC RID: 67260
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

		// Token: 0x170056E6 RID: 22246
		// (get) Token: 0x060106BF RID: 67263
		// (set) Token: 0x060106BE RID: 67262
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

		// Token: 0x060106C0 RID: 67264
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement_scrollIntoView([MarshalAs(27)] [In] [Optional] object varargStart);

		// Token: 0x060106C1 RID: 67265
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement_contains([MarshalAs(28)] [In] IHTMLElement pChild);

		// Token: 0x170056E7 RID: 22247
		// (get) Token: 0x060106C2 RID: 67266
		public virtual extern int IHTMLElement_sourceIndex
		{
			[TypeLibFunc(4)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170056E8 RID: 22248
		// (get) Token: 0x060106C3 RID: 67267
		public virtual extern object IHTMLElement_recordNumber
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x170056E9 RID: 22249
		// (get) Token: 0x060106C5 RID: 67269
		// (set) Token: 0x060106C4 RID: 67268
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

		// Token: 0x170056EA RID: 22250
		// (get) Token: 0x060106C6 RID: 67270
		public virtual extern int IHTMLElement_offsetLeft
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170056EB RID: 22251
		// (get) Token: 0x060106C7 RID: 67271
		public virtual extern int IHTMLElement_offsetTop
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170056EC RID: 22252
		// (get) Token: 0x060106C8 RID: 67272
		public virtual extern int IHTMLElement_offsetWidth
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170056ED RID: 22253
		// (get) Token: 0x060106C9 RID: 67273
		public virtual extern int IHTMLElement_offsetHeight
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170056EE RID: 22254
		// (get) Token: 0x060106CA RID: 67274
		public virtual extern IHTMLElement IHTMLElement_offsetParent
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170056EF RID: 22255
		// (get) Token: 0x060106CC RID: 67276
		// (set) Token: 0x060106CB RID: 67275
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

		// Token: 0x170056F0 RID: 22256
		// (get) Token: 0x060106CE RID: 67278
		// (set) Token: 0x060106CD RID: 67277
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

		// Token: 0x170056F1 RID: 22257
		// (get) Token: 0x060106D0 RID: 67280
		// (set) Token: 0x060106CF RID: 67279
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

		// Token: 0x170056F2 RID: 22258
		// (get) Token: 0x060106D2 RID: 67282
		// (set) Token: 0x060106D1 RID: 67281
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

		// Token: 0x060106D3 RID: 67283
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement_insertAdjacentHTML([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string html);

		// Token: 0x060106D4 RID: 67284
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement_insertAdjacentText([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string text);

		// Token: 0x170056F3 RID: 22259
		// (get) Token: 0x060106D5 RID: 67285
		public virtual extern IHTMLElement IHTMLElement_parentTextEdit
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170056F4 RID: 22260
		// (get) Token: 0x060106D6 RID: 67286
		public virtual extern bool IHTMLElement_isTextEdit
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x060106D7 RID: 67287
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement_click();

		// Token: 0x170056F5 RID: 22261
		// (get) Token: 0x060106D8 RID: 67288
		public virtual extern IHTMLFiltersCollection IHTMLElement_filters
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170056F6 RID: 22262
		// (get) Token: 0x060106DA RID: 67290
		// (set) Token: 0x060106D9 RID: 67289
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

		// Token: 0x060106DB RID: 67291
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string IHTMLElement_toString();

		// Token: 0x170056F7 RID: 22263
		// (get) Token: 0x060106DD RID: 67293
		// (set) Token: 0x060106DC RID: 67292
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

		// Token: 0x170056F8 RID: 22264
		// (get) Token: 0x060106DF RID: 67295
		// (set) Token: 0x060106DE RID: 67294
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

		// Token: 0x170056F9 RID: 22265
		// (get) Token: 0x060106E1 RID: 67297
		// (set) Token: 0x060106E0 RID: 67296
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

		// Token: 0x170056FA RID: 22266
		// (get) Token: 0x060106E3 RID: 67299
		// (set) Token: 0x060106E2 RID: 67298
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

		// Token: 0x170056FB RID: 22267
		// (get) Token: 0x060106E5 RID: 67301
		// (set) Token: 0x060106E4 RID: 67300
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

		// Token: 0x170056FC RID: 22268
		// (get) Token: 0x060106E7 RID: 67303
		// (set) Token: 0x060106E6 RID: 67302
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

		// Token: 0x170056FD RID: 22269
		// (get) Token: 0x060106E9 RID: 67305
		// (set) Token: 0x060106E8 RID: 67304
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

		// Token: 0x170056FE RID: 22270
		// (get) Token: 0x060106EB RID: 67307
		// (set) Token: 0x060106EA RID: 67306
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

		// Token: 0x170056FF RID: 22271
		// (get) Token: 0x060106ED RID: 67309
		// (set) Token: 0x060106EC RID: 67308
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

		// Token: 0x17005700 RID: 22272
		// (get) Token: 0x060106EE RID: 67310
		public virtual extern object IHTMLElement_children
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17005701 RID: 22273
		// (get) Token: 0x060106EF RID: 67311
		public virtual extern object IHTMLElement_all
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17005702 RID: 22274
		// (get) Token: 0x060106F0 RID: 67312
		public virtual extern string IHTMLElement2_scopeName
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x060106F1 RID: 67313
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_setCapture([In] bool containerCapture = true);

		// Token: 0x060106F2 RID: 67314
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_releaseCapture();

		// Token: 0x17005703 RID: 22275
		// (get) Token: 0x060106F4 RID: 67316
		// (set) Token: 0x060106F3 RID: 67315
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

		// Token: 0x060106F5 RID: 67317
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string IHTMLElement2_componentFromPoint([In] int x, [In] int y);

		// Token: 0x060106F6 RID: 67318
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_doScroll([MarshalAs(27)] [In] [Optional] object component);

		// Token: 0x17005704 RID: 22276
		// (get) Token: 0x060106F8 RID: 67320
		// (set) Token: 0x060106F7 RID: 67319
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

		// Token: 0x17005705 RID: 22277
		// (get) Token: 0x060106FA RID: 67322
		// (set) Token: 0x060106F9 RID: 67321
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

		// Token: 0x17005706 RID: 22278
		// (get) Token: 0x060106FC RID: 67324
		// (set) Token: 0x060106FB RID: 67323
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

		// Token: 0x17005707 RID: 22279
		// (get) Token: 0x060106FE RID: 67326
		// (set) Token: 0x060106FD RID: 67325
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

		// Token: 0x17005708 RID: 22280
		// (get) Token: 0x06010700 RID: 67328
		// (set) Token: 0x060106FF RID: 67327
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

		// Token: 0x17005709 RID: 22281
		// (get) Token: 0x06010702 RID: 67330
		// (set) Token: 0x06010701 RID: 67329
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

		// Token: 0x1700570A RID: 22282
		// (get) Token: 0x06010704 RID: 67332
		// (set) Token: 0x06010703 RID: 67331
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

		// Token: 0x1700570B RID: 22283
		// (get) Token: 0x06010706 RID: 67334
		// (set) Token: 0x06010705 RID: 67333
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

		// Token: 0x1700570C RID: 22284
		// (get) Token: 0x06010708 RID: 67336
		// (set) Token: 0x06010707 RID: 67335
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

		// Token: 0x1700570D RID: 22285
		// (get) Token: 0x0601070A RID: 67338
		// (set) Token: 0x06010709 RID: 67337
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

		// Token: 0x1700570E RID: 22286
		// (get) Token: 0x0601070C RID: 67340
		// (set) Token: 0x0601070B RID: 67339
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

		// Token: 0x1700570F RID: 22287
		// (get) Token: 0x0601070E RID: 67342
		// (set) Token: 0x0601070D RID: 67341
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

		// Token: 0x17005710 RID: 22288
		// (get) Token: 0x06010710 RID: 67344
		// (set) Token: 0x0601070F RID: 67343
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

		// Token: 0x17005711 RID: 22289
		// (get) Token: 0x06010711 RID: 67345
		public virtual extern IHTMLCurrentStyle IHTMLElement2_currentStyle
		{
			[TypeLibFunc(1024)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17005712 RID: 22290
		// (get) Token: 0x06010713 RID: 67347
		// (set) Token: 0x06010712 RID: 67346
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

		// Token: 0x06010714 RID: 67348
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLRectCollection IHTMLElement2_getClientRects();

		// Token: 0x06010715 RID: 67349
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLRect IHTMLElement2_getBoundingClientRect();

		// Token: 0x06010716 RID: 67350
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_setExpression([MarshalAs(19)] [In] string propname, [MarshalAs(19)] [In] string expression, [MarshalAs(19)] [In] string language = "");

		// Token: 0x06010717 RID: 67351
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		public virtual extern object IHTMLElement2_getExpression([MarshalAs(19)] [In] string propname);

		// Token: 0x06010718 RID: 67352
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement2_removeExpression([MarshalAs(19)] [In] string propname);

		// Token: 0x17005713 RID: 22291
		// (get) Token: 0x0601071A RID: 67354
		// (set) Token: 0x06010719 RID: 67353
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

		// Token: 0x0601071B RID: 67355
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_focus();

		// Token: 0x17005714 RID: 22292
		// (get) Token: 0x0601071D RID: 67357
		// (set) Token: 0x0601071C RID: 67356
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

		// Token: 0x17005715 RID: 22293
		// (get) Token: 0x0601071F RID: 67359
		// (set) Token: 0x0601071E RID: 67358
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

		// Token: 0x17005716 RID: 22294
		// (get) Token: 0x06010721 RID: 67361
		// (set) Token: 0x06010720 RID: 67360
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

		// Token: 0x17005717 RID: 22295
		// (get) Token: 0x06010723 RID: 67363
		// (set) Token: 0x06010722 RID: 67362
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

		// Token: 0x06010724 RID: 67364
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_blur();

		// Token: 0x06010725 RID: 67365
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_addFilter([MarshalAs(25)] [In] object pUnk);

		// Token: 0x06010726 RID: 67366
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_removeFilter([MarshalAs(25)] [In] object pUnk);

		// Token: 0x17005718 RID: 22296
		// (get) Token: 0x06010727 RID: 67367
		public virtual extern int IHTMLElement2_clientHeight
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17005719 RID: 22297
		// (get) Token: 0x06010728 RID: 67368
		public virtual extern int IHTMLElement2_clientWidth
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700571A RID: 22298
		// (get) Token: 0x06010729 RID: 67369
		public virtual extern int IHTMLElement2_clientTop
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700571B RID: 22299
		// (get) Token: 0x0601072A RID: 67370
		public virtual extern int IHTMLElement2_clientLeft
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x0601072B RID: 67371
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement2_attachEvent([MarshalAs(19)] [In] string @event, [MarshalAs(26)] [In] object pdisp);

		// Token: 0x0601072C RID: 67372
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_detachEvent([MarshalAs(19)] [In] string @event, [MarshalAs(26)] [In] object pdisp);

		// Token: 0x1700571C RID: 22300
		// (get) Token: 0x0601072D RID: 67373
		public virtual extern object IHTMLElement2_readyState
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x1700571D RID: 22301
		// (get) Token: 0x0601072F RID: 67375
		// (set) Token: 0x0601072E RID: 67374
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

		// Token: 0x1700571E RID: 22302
		// (get) Token: 0x06010731 RID: 67377
		// (set) Token: 0x06010730 RID: 67376
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

		// Token: 0x1700571F RID: 22303
		// (get) Token: 0x06010733 RID: 67379
		// (set) Token: 0x06010732 RID: 67378
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

		// Token: 0x17005720 RID: 22304
		// (get) Token: 0x06010735 RID: 67381
		// (set) Token: 0x06010734 RID: 67380
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

		// Token: 0x17005721 RID: 22305
		// (get) Token: 0x06010737 RID: 67383
		// (set) Token: 0x06010736 RID: 67382
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

		// Token: 0x06010738 RID: 67384
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(26)]
		public virtual extern object IHTMLElement2_createControlRange();

		// Token: 0x17005722 RID: 22306
		// (get) Token: 0x06010739 RID: 67385
		public virtual extern int IHTMLElement2_scrollHeight
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17005723 RID: 22307
		// (get) Token: 0x0601073A RID: 67386
		public virtual extern int IHTMLElement2_scrollWidth
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17005724 RID: 22308
		// (get) Token: 0x0601073C RID: 67388
		// (set) Token: 0x0601073B RID: 67387
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

		// Token: 0x17005725 RID: 22309
		// (get) Token: 0x0601073E RID: 67390
		// (set) Token: 0x0601073D RID: 67389
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

		// Token: 0x0601073F RID: 67391
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_clearAttributes();

		// Token: 0x06010740 RID: 67392
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void mergeAttributes([MarshalAs(28)] [In] IHTMLElement mergeThis);

		// Token: 0x17005726 RID: 22310
		// (get) Token: 0x06010742 RID: 67394
		// (set) Token: 0x06010741 RID: 67393
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

		// Token: 0x06010743 RID: 67395
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElement IHTMLElement2_insertAdjacentElement([MarshalAs(19)] [In] string where, [MarshalAs(28)] [In] IHTMLElement insertedElement);

		// Token: 0x06010744 RID: 67396
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElement IHTMLElement2_applyElement([MarshalAs(28)] [In] IHTMLElement apply, [MarshalAs(19)] [In] string where);

		// Token: 0x06010745 RID: 67397
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string IHTMLElement2_getAdjacentText([MarshalAs(19)] [In] string where);

		// Token: 0x06010746 RID: 67398
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string IHTMLElement2_replaceAdjacentText([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string newText);

		// Token: 0x17005727 RID: 22311
		// (get) Token: 0x06010747 RID: 67399
		public virtual extern bool IHTMLElement2_canHaveChildren
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x06010748 RID: 67400
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern int IHTMLElement2_addBehavior([MarshalAs(19)] [In] string bstrUrl, [MarshalAs(27)] [In] [Optional] ref object pvarFactory);

		// Token: 0x06010749 RID: 67401
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement2_removeBehavior([In] int cookie);

		// Token: 0x17005728 RID: 22312
		// (get) Token: 0x0601074A RID: 67402
		public virtual extern IHTMLStyle IHTMLElement2_runtimeStyle
		{
			[TypeLibFunc(1024)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17005729 RID: 22313
		// (get) Token: 0x0601074B RID: 67403
		public virtual extern object IHTMLElement2_behaviorUrns
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x1700572A RID: 22314
		// (get) Token: 0x0601074D RID: 67405
		// (set) Token: 0x0601074C RID: 67404
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

		// Token: 0x1700572B RID: 22315
		// (get) Token: 0x0601074F RID: 67407
		// (set) Token: 0x0601074E RID: 67406
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

		// Token: 0x1700572C RID: 22316
		// (get) Token: 0x06010750 RID: 67408
		public virtual extern int IHTMLElement2_readyStateValue
		{
			[TypeLibFunc(65)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x06010751 RID: 67409
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElementCollection IHTMLElement2_getElementsByTagName([MarshalAs(19)] [In] string v);

		// Token: 0x06010752 RID: 67410
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void mergeAttributes([MarshalAs(28)] [In] IHTMLElement mergeThis, [MarshalAs(27)] [In] [Optional] ref object pvarFlags);

		// Token: 0x1700572D RID: 22317
		// (get) Token: 0x06010753 RID: 67411
		public virtual extern bool IHTMLElement3_isMultiLine
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700572E RID: 22318
		// (get) Token: 0x06010754 RID: 67412
		public virtual extern bool IHTMLElement3_canHaveHTML
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700572F RID: 22319
		// (get) Token: 0x06010756 RID: 67414
		// (set) Token: 0x06010755 RID: 67413
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

		// Token: 0x17005730 RID: 22320
		// (get) Token: 0x06010758 RID: 67416
		// (set) Token: 0x06010757 RID: 67415
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

		// Token: 0x17005731 RID: 22321
		// (get) Token: 0x0601075A RID: 67418
		// (set) Token: 0x06010759 RID: 67417
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

		// Token: 0x17005732 RID: 22322
		// (get) Token: 0x0601075C RID: 67420
		// (set) Token: 0x0601075B RID: 67419
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

		// Token: 0x0601075D RID: 67421
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement3_setActive();

		// Token: 0x17005733 RID: 22323
		// (get) Token: 0x0601075F RID: 67423
		// (set) Token: 0x0601075E RID: 67422
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

		// Token: 0x17005734 RID: 22324
		// (get) Token: 0x06010760 RID: 67424
		public virtual extern bool IHTMLElement3_isContentEditable
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17005735 RID: 22325
		// (get) Token: 0x06010762 RID: 67426
		// (set) Token: 0x06010761 RID: 67425
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

		// Token: 0x17005736 RID: 22326
		// (get) Token: 0x06010764 RID: 67428
		// (set) Token: 0x06010763 RID: 67427
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

		// Token: 0x17005737 RID: 22327
		// (get) Token: 0x06010765 RID: 67429
		public virtual extern bool IHTMLElement3_isDisabled
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17005738 RID: 22328
		// (get) Token: 0x06010767 RID: 67431
		// (set) Token: 0x06010766 RID: 67430
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

		// Token: 0x17005739 RID: 22329
		// (get) Token: 0x06010769 RID: 67433
		// (set) Token: 0x06010768 RID: 67432
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

		// Token: 0x0601076A RID: 67434
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement3_FireEvent([MarshalAs(19)] [In] string bstrEventName, [MarshalAs(27)] [In] [Optional] ref object pvarEventObject);

		// Token: 0x1700573A RID: 22330
		// (get) Token: 0x0601076C RID: 67436
		// (set) Token: 0x0601076B RID: 67435
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

		// Token: 0x1700573B RID: 22331
		// (get) Token: 0x0601076E RID: 67438
		// (set) Token: 0x0601076D RID: 67437
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

		// Token: 0x1700573C RID: 22332
		// (get) Token: 0x06010770 RID: 67440
		// (set) Token: 0x0601076F RID: 67439
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

		// Token: 0x1700573D RID: 22333
		// (get) Token: 0x06010772 RID: 67442
		// (set) Token: 0x06010771 RID: 67441
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

		// Token: 0x1700573E RID: 22334
		// (get) Token: 0x06010774 RID: 67444
		// (set) Token: 0x06010773 RID: 67443
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

		// Token: 0x1700573F RID: 22335
		// (get) Token: 0x06010776 RID: 67446
		// (set) Token: 0x06010775 RID: 67445
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

		// Token: 0x17005740 RID: 22336
		// (get) Token: 0x06010778 RID: 67448
		// (set) Token: 0x06010777 RID: 67447
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

		// Token: 0x17005741 RID: 22337
		// (get) Token: 0x0601077A RID: 67450
		// (set) Token: 0x06010779 RID: 67449
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

		// Token: 0x0601077B RID: 67451
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement3_dragDrop();

		// Token: 0x17005742 RID: 22338
		// (get) Token: 0x0601077C RID: 67452
		public virtual extern int IHTMLElement3_glyphMode
		{
			[TypeLibFunc(1089)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17005743 RID: 22339
		// (get) Token: 0x0601077E RID: 67454
		// (set) Token: 0x0601077D RID: 67453
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

		// Token: 0x0601077F RID: 67455
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement4_normalize();

		// Token: 0x06010780 RID: 67456
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute IHTMLElement4_getAttributeNode([MarshalAs(19)] [In] string bstrName);

		// Token: 0x06010781 RID: 67457
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute IHTMLElement4_setAttributeNode([MarshalAs(28)] [In] IHTMLDOMAttribute pattr);

		// Token: 0x06010782 RID: 67458
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute IHTMLElement4_removeAttributeNode([MarshalAs(28)] [In] IHTMLDOMAttribute pattr);

		// Token: 0x17005744 RID: 22340
		// (get) Token: 0x06010784 RID: 67460
		// (set) Token: 0x06010783 RID: 67459
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

		// Token: 0x17005745 RID: 22341
		// (get) Token: 0x06010786 RID: 67462
		// (set) Token: 0x06010785 RID: 67461
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

		// Token: 0x17005746 RID: 22342
		// (get) Token: 0x06010788 RID: 67464
		// (set) Token: 0x06010787 RID: 67463
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

		// Token: 0x17005747 RID: 22343
		// (get) Token: 0x06010789 RID: 67465
		public virtual extern int IHTMLUniqueName_uniqueNumber
		{
			[TypeLibFunc(64)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17005748 RID: 22344
		// (get) Token: 0x0601078A RID: 67466
		public virtual extern string IHTMLUniqueName_uniqueID
		{
			[TypeLibFunc(64)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17005749 RID: 22345
		// (get) Token: 0x0601078B RID: 67467
		public virtual extern int IHTMLDOMNode_nodeType
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700574A RID: 22346
		// (get) Token: 0x0601078C RID: 67468
		public virtual extern IHTMLDOMNode IHTMLDOMNode_parentNode
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x0601078D RID: 67469
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLDOMNode_hasChildNodes();

		// Token: 0x1700574B RID: 22347
		// (get) Token: 0x0601078E RID: 67470
		public virtual extern object IHTMLDOMNode_childNodes
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x1700574C RID: 22348
		// (get) Token: 0x0601078F RID: 67471
		public virtual extern object IHTMLDOMNode_attributes
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x06010790 RID: 67472
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_insertBefore([MarshalAs(28)] [In] IHTMLDOMNode newChild, [MarshalAs(27)] [In] [Optional] object refChild);

		// Token: 0x06010791 RID: 67473
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_removeChild([MarshalAs(28)] [In] IHTMLDOMNode oldChild);

		// Token: 0x06010792 RID: 67474
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_replaceChild([MarshalAs(28)] [In] IHTMLDOMNode newChild, [MarshalAs(28)] [In] IHTMLDOMNode oldChild);

		// Token: 0x06010793 RID: 67475
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_cloneNode([In] bool fDeep);

		// Token: 0x06010794 RID: 67476
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_removeNode([In] bool fDeep = false);

		// Token: 0x06010795 RID: 67477
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_swapNode([MarshalAs(28)] [In] IHTMLDOMNode otherNode);

		// Token: 0x06010796 RID: 67478
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_replaceNode([MarshalAs(28)] [In] IHTMLDOMNode replacement);

		// Token: 0x06010797 RID: 67479
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_appendChild([MarshalAs(28)] [In] IHTMLDOMNode newChild);

		// Token: 0x1700574D RID: 22349
		// (get) Token: 0x06010798 RID: 67480
		public virtual extern string IHTMLDOMNode_nodeName
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x1700574E RID: 22350
		// (get) Token: 0x0601079A RID: 67482
		// (set) Token: 0x06010799 RID: 67481
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

		// Token: 0x1700574F RID: 22351
		// (get) Token: 0x0601079B RID: 67483
		public virtual extern IHTMLDOMNode IHTMLDOMNode_firstChild
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17005750 RID: 22352
		// (get) Token: 0x0601079C RID: 67484
		public virtual extern IHTMLDOMNode IHTMLDOMNode_lastChild
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17005751 RID: 22353
		// (get) Token: 0x0601079D RID: 67485
		public virtual extern IHTMLDOMNode IHTMLDOMNode_previousSibling
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17005752 RID: 22354
		// (get) Token: 0x0601079E RID: 67486
		public virtual extern IHTMLDOMNode IHTMLDOMNode_nextSibling
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17005753 RID: 22355
		// (get) Token: 0x0601079F RID: 67487
		public virtual extern object IHTMLDOMNode2_ownerDocument
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17005754 RID: 22356
		// (get) Token: 0x060107A1 RID: 67489
		// (set) Token: 0x060107A0 RID: 67488
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

		// Token: 0x17005755 RID: 22357
		// (get) Token: 0x060107A3 RID: 67491
		// (set) Token: 0x060107A2 RID: 67490
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

		// Token: 0x17005756 RID: 22358
		// (get) Token: 0x060107A5 RID: 67493
		// (set) Token: 0x060107A4 RID: 67492
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

		// Token: 0x17005757 RID: 22359
		// (get) Token: 0x060107A7 RID: 67495
		// (set) Token: 0x060107A6 RID: 67494
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

		// Token: 0x060107A8 RID: 67496
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLControlElement_focus();

		// Token: 0x17005758 RID: 22360
		// (get) Token: 0x060107AA RID: 67498
		// (set) Token: 0x060107A9 RID: 67497
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

		// Token: 0x17005759 RID: 22361
		// (get) Token: 0x060107AC RID: 67500
		// (set) Token: 0x060107AB RID: 67499
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

		// Token: 0x1700575A RID: 22362
		// (get) Token: 0x060107AE RID: 67502
		// (set) Token: 0x060107AD RID: 67501
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

		// Token: 0x1700575B RID: 22363
		// (get) Token: 0x060107B0 RID: 67504
		// (set) Token: 0x060107AF RID: 67503
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

		// Token: 0x060107B1 RID: 67505
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLControlElement_blur();

		// Token: 0x060107B2 RID: 67506
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLControlElement_addFilter([MarshalAs(25)] [In] object pUnk);

		// Token: 0x060107B3 RID: 67507
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLControlElement_removeFilter([MarshalAs(25)] [In] object pUnk);

		// Token: 0x1700575C RID: 22364
		// (get) Token: 0x060107B4 RID: 67508
		public virtual extern int IHTMLControlElement_clientHeight
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700575D RID: 22365
		// (get) Token: 0x060107B5 RID: 67509
		public virtual extern int IHTMLControlElement_clientWidth
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700575E RID: 22366
		// (get) Token: 0x060107B6 RID: 67510
		public virtual extern int IHTMLControlElement_clientTop
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700575F RID: 22367
		// (get) Token: 0x060107B7 RID: 67511
		public virtual extern int IHTMLControlElement_clientLeft
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17005760 RID: 22368
		// (get) Token: 0x060107B9 RID: 67513
		// (set) Token: 0x060107B8 RID: 67512
		public virtual extern int IHTMLTable_cols
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x17005761 RID: 22369
		// (get) Token: 0x060107BB RID: 67515
		// (set) Token: 0x060107BA RID: 67514
		public virtual extern object IHTMLTable_border
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17005762 RID: 22370
		// (get) Token: 0x060107BD RID: 67517
		// (set) Token: 0x060107BC RID: 67516
		public virtual extern string IHTMLTable_frame
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17005763 RID: 22371
		// (get) Token: 0x060107BF RID: 67519
		// (set) Token: 0x060107BE RID: 67518
		public virtual extern string IHTMLTable_rules
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17005764 RID: 22372
		// (get) Token: 0x060107C1 RID: 67521
		// (set) Token: 0x060107C0 RID: 67520
		public virtual extern object IHTMLTable_cellSpacing
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17005765 RID: 22373
		// (get) Token: 0x060107C3 RID: 67523
		// (set) Token: 0x060107C2 RID: 67522
		public virtual extern object IHTMLTable_cellPadding
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17005766 RID: 22374
		// (get) Token: 0x060107C5 RID: 67525
		// (set) Token: 0x060107C4 RID: 67524
		public virtual extern string IHTMLTable_background
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17005767 RID: 22375
		// (get) Token: 0x060107C7 RID: 67527
		// (set) Token: 0x060107C6 RID: 67526
		public virtual extern object IHTMLTable_bgColor
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17005768 RID: 22376
		// (get) Token: 0x060107C9 RID: 67529
		// (set) Token: 0x060107C8 RID: 67528
		public virtual extern object IHTMLTable_borderColor
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17005769 RID: 22377
		// (get) Token: 0x060107CB RID: 67531
		// (set) Token: 0x060107CA RID: 67530
		public virtual extern object IHTMLTable_borderColorLight
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x1700576A RID: 22378
		// (get) Token: 0x060107CD RID: 67533
		// (set) Token: 0x060107CC RID: 67532
		public virtual extern object IHTMLTable_borderColorDark
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x1700576B RID: 22379
		// (get) Token: 0x060107CF RID: 67535
		// (set) Token: 0x060107CE RID: 67534
		public virtual extern string IHTMLTable_align
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x060107D0 RID: 67536
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLTable_refresh();

		// Token: 0x1700576C RID: 22380
		// (get) Token: 0x060107D1 RID: 67537
		public virtual extern IHTMLElementCollection IHTMLTable_rows
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x1700576D RID: 22381
		// (get) Token: 0x060107D3 RID: 67539
		// (set) Token: 0x060107D2 RID: 67538
		public virtual extern object IHTMLTable_width
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x1700576E RID: 22382
		// (get) Token: 0x060107D5 RID: 67541
		// (set) Token: 0x060107D4 RID: 67540
		public virtual extern object IHTMLTable_height
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x1700576F RID: 22383
		// (get) Token: 0x060107D7 RID: 67543
		// (set) Token: 0x060107D6 RID: 67542
		public virtual extern int IHTMLTable_dataPageSize
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x060107D8 RID: 67544
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLTable_nextPage();

		// Token: 0x060107D9 RID: 67545
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLTable_previousPage();

		// Token: 0x17005770 RID: 22384
		// (get) Token: 0x060107DA RID: 67546
		public virtual extern IHTMLTableSection IHTMLTable_tHead
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17005771 RID: 22385
		// (get) Token: 0x060107DB RID: 67547
		public virtual extern IHTMLTableSection IHTMLTable_tFoot
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17005772 RID: 22386
		// (get) Token: 0x060107DC RID: 67548
		public virtual extern IHTMLElementCollection IHTMLTable_tBodies
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17005773 RID: 22387
		// (get) Token: 0x060107DD RID: 67549
		public virtual extern IHTMLTableCaption IHTMLTable_caption
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x060107DE RID: 67550
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(26)]
		public virtual extern object IHTMLTable_createTHead();

		// Token: 0x060107DF RID: 67551
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLTable_deleteTHead();

		// Token: 0x060107E0 RID: 67552
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(26)]
		public virtual extern object IHTMLTable_createTFoot();

		// Token: 0x060107E1 RID: 67553
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLTable_deleteTFoot();

		// Token: 0x060107E2 RID: 67554
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLTableCaption IHTMLTable_createCaption();

		// Token: 0x060107E3 RID: 67555
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLTable_deleteCaption();

		// Token: 0x060107E4 RID: 67556
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(26)]
		public virtual extern object IHTMLTable_insertRow([In] int index = -1);

		// Token: 0x060107E5 RID: 67557
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLTable_deleteRow([In] int index = -1);

		// Token: 0x17005774 RID: 22388
		// (get) Token: 0x060107E6 RID: 67558
		public virtual extern string IHTMLTable_readyState
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17005775 RID: 22389
		// (get) Token: 0x060107E8 RID: 67560
		// (set) Token: 0x060107E7 RID: 67559
		public virtual extern object IHTMLTable_onreadystatechange
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x060107E9 RID: 67561
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLTable2_firstPage();

		// Token: 0x060107EA RID: 67562
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLTable2_lastPage();

		// Token: 0x17005776 RID: 22390
		// (get) Token: 0x060107EB RID: 67563
		public virtual extern IHTMLElementCollection IHTMLTable2_cells
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x060107EC RID: 67564
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(26)]
		public virtual extern object IHTMLTable2_moveRow([In] int indexFrom = -1, [In] int indexTo = -1);

		// Token: 0x17005777 RID: 22391
		// (get) Token: 0x060107EE RID: 67566
		// (set) Token: 0x060107ED RID: 67565
		public virtual extern string IHTMLTable3_summary
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

		// Token: 0x14001F73 RID: 8051
		// (add) Token: 0x060107EF RID: 67567
		// (remove) Token: 0x060107F0 RID: 67568
		public virtual extern event HTMLTableEvents_onhelpEventHandler HTMLTableEvents_Event_onhelp;

		// Token: 0x14001F74 RID: 8052
		// (add) Token: 0x060107F1 RID: 67569
		// (remove) Token: 0x060107F2 RID: 67570
		public virtual extern event HTMLTableEvents_onclickEventHandler HTMLTableEvents_Event_onclick;

		// Token: 0x14001F75 RID: 8053
		// (add) Token: 0x060107F3 RID: 67571
		// (remove) Token: 0x060107F4 RID: 67572
		public virtual extern event HTMLTableEvents_ondblclickEventHandler HTMLTableEvents_Event_ondblclick;

		// Token: 0x14001F76 RID: 8054
		// (add) Token: 0x060107F5 RID: 67573
		// (remove) Token: 0x060107F6 RID: 67574
		public virtual extern event HTMLTableEvents_onkeypressEventHandler HTMLTableEvents_Event_onkeypress;

		// Token: 0x14001F77 RID: 8055
		// (add) Token: 0x060107F7 RID: 67575
		// (remove) Token: 0x060107F8 RID: 67576
		public virtual extern event HTMLTableEvents_onkeydownEventHandler HTMLTableEvents_Event_onkeydown;

		// Token: 0x14001F78 RID: 8056
		// (add) Token: 0x060107F9 RID: 67577
		// (remove) Token: 0x060107FA RID: 67578
		public virtual extern event HTMLTableEvents_onkeyupEventHandler HTMLTableEvents_Event_onkeyup;

		// Token: 0x14001F79 RID: 8057
		// (add) Token: 0x060107FB RID: 67579
		// (remove) Token: 0x060107FC RID: 67580
		public virtual extern event HTMLTableEvents_onmouseoutEventHandler HTMLTableEvents_Event_onmouseout;

		// Token: 0x14001F7A RID: 8058
		// (add) Token: 0x060107FD RID: 67581
		// (remove) Token: 0x060107FE RID: 67582
		public virtual extern event HTMLTableEvents_onmouseoverEventHandler HTMLTableEvents_Event_onmouseover;

		// Token: 0x14001F7B RID: 8059
		// (add) Token: 0x060107FF RID: 67583
		// (remove) Token: 0x06010800 RID: 67584
		public virtual extern event HTMLTableEvents_onmousemoveEventHandler HTMLTableEvents_Event_onmousemove;

		// Token: 0x14001F7C RID: 8060
		// (add) Token: 0x06010801 RID: 67585
		// (remove) Token: 0x06010802 RID: 67586
		public virtual extern event HTMLTableEvents_onmousedownEventHandler HTMLTableEvents_Event_onmousedown;

		// Token: 0x14001F7D RID: 8061
		// (add) Token: 0x06010803 RID: 67587
		// (remove) Token: 0x06010804 RID: 67588
		public virtual extern event HTMLTableEvents_onmouseupEventHandler HTMLTableEvents_Event_onmouseup;

		// Token: 0x14001F7E RID: 8062
		// (add) Token: 0x06010805 RID: 67589
		// (remove) Token: 0x06010806 RID: 67590
		public virtual extern event HTMLTableEvents_onselectstartEventHandler HTMLTableEvents_Event_onselectstart;

		// Token: 0x14001F7F RID: 8063
		// (add) Token: 0x06010807 RID: 67591
		// (remove) Token: 0x06010808 RID: 67592
		public virtual extern event HTMLTableEvents_onfilterchangeEventHandler HTMLTableEvents_Event_onfilterchange;

		// Token: 0x14001F80 RID: 8064
		// (add) Token: 0x06010809 RID: 67593
		// (remove) Token: 0x0601080A RID: 67594
		public virtual extern event HTMLTableEvents_ondragstartEventHandler HTMLTableEvents_Event_ondragstart;

		// Token: 0x14001F81 RID: 8065
		// (add) Token: 0x0601080B RID: 67595
		// (remove) Token: 0x0601080C RID: 67596
		public virtual extern event HTMLTableEvents_onbeforeupdateEventHandler HTMLTableEvents_Event_onbeforeupdate;

		// Token: 0x14001F82 RID: 8066
		// (add) Token: 0x0601080D RID: 67597
		// (remove) Token: 0x0601080E RID: 67598
		public virtual extern event HTMLTableEvents_onafterupdateEventHandler HTMLTableEvents_Event_onafterupdate;

		// Token: 0x14001F83 RID: 8067
		// (add) Token: 0x0601080F RID: 67599
		// (remove) Token: 0x06010810 RID: 67600
		public virtual extern event HTMLTableEvents_onerrorupdateEventHandler HTMLTableEvents_Event_onerrorupdate;

		// Token: 0x14001F84 RID: 8068
		// (add) Token: 0x06010811 RID: 67601
		// (remove) Token: 0x06010812 RID: 67602
		public virtual extern event HTMLTableEvents_onrowexitEventHandler HTMLTableEvents_Event_onrowexit;

		// Token: 0x14001F85 RID: 8069
		// (add) Token: 0x06010813 RID: 67603
		// (remove) Token: 0x06010814 RID: 67604
		public virtual extern event HTMLTableEvents_onrowenterEventHandler HTMLTableEvents_Event_onrowenter;

		// Token: 0x14001F86 RID: 8070
		// (add) Token: 0x06010815 RID: 67605
		// (remove) Token: 0x06010816 RID: 67606
		public virtual extern event HTMLTableEvents_ondatasetchangedEventHandler HTMLTableEvents_Event_ondatasetchanged;

		// Token: 0x14001F87 RID: 8071
		// (add) Token: 0x06010817 RID: 67607
		// (remove) Token: 0x06010818 RID: 67608
		public virtual extern event HTMLTableEvents_ondataavailableEventHandler HTMLTableEvents_Event_ondataavailable;

		// Token: 0x14001F88 RID: 8072
		// (add) Token: 0x06010819 RID: 67609
		// (remove) Token: 0x0601081A RID: 67610
		public virtual extern event HTMLTableEvents_ondatasetcompleteEventHandler HTMLTableEvents_Event_ondatasetcomplete;

		// Token: 0x14001F89 RID: 8073
		// (add) Token: 0x0601081B RID: 67611
		// (remove) Token: 0x0601081C RID: 67612
		public virtual extern event HTMLTableEvents_onlosecaptureEventHandler HTMLTableEvents_Event_onlosecapture;

		// Token: 0x14001F8A RID: 8074
		// (add) Token: 0x0601081D RID: 67613
		// (remove) Token: 0x0601081E RID: 67614
		public virtual extern event HTMLTableEvents_onpropertychangeEventHandler HTMLTableEvents_Event_onpropertychange;

		// Token: 0x14001F8B RID: 8075
		// (add) Token: 0x0601081F RID: 67615
		// (remove) Token: 0x06010820 RID: 67616
		public virtual extern event HTMLTableEvents_onscrollEventHandler HTMLTableEvents_Event_onscroll;

		// Token: 0x14001F8C RID: 8076
		// (add) Token: 0x06010821 RID: 67617
		// (remove) Token: 0x06010822 RID: 67618
		public virtual extern event HTMLTableEvents_onfocusEventHandler HTMLTableEvents_Event_onfocus;

		// Token: 0x14001F8D RID: 8077
		// (add) Token: 0x06010823 RID: 67619
		// (remove) Token: 0x06010824 RID: 67620
		public virtual extern event HTMLTableEvents_onblurEventHandler HTMLTableEvents_Event_onblur;

		// Token: 0x14001F8E RID: 8078
		// (add) Token: 0x06010825 RID: 67621
		// (remove) Token: 0x06010826 RID: 67622
		public virtual extern event HTMLTableEvents_onresizeEventHandler HTMLTableEvents_Event_onresize;

		// Token: 0x14001F8F RID: 8079
		// (add) Token: 0x06010827 RID: 67623
		// (remove) Token: 0x06010828 RID: 67624
		public virtual extern event HTMLTableEvents_ondragEventHandler HTMLTableEvents_Event_ondrag;

		// Token: 0x14001F90 RID: 8080
		// (add) Token: 0x06010829 RID: 67625
		// (remove) Token: 0x0601082A RID: 67626
		public virtual extern event HTMLTableEvents_ondragendEventHandler HTMLTableEvents_Event_ondragend;

		// Token: 0x14001F91 RID: 8081
		// (add) Token: 0x0601082B RID: 67627
		// (remove) Token: 0x0601082C RID: 67628
		public virtual extern event HTMLTableEvents_ondragenterEventHandler HTMLTableEvents_Event_ondragenter;

		// Token: 0x14001F92 RID: 8082
		// (add) Token: 0x0601082D RID: 67629
		// (remove) Token: 0x0601082E RID: 67630
		public virtual extern event HTMLTableEvents_ondragoverEventHandler HTMLTableEvents_Event_ondragover;

		// Token: 0x14001F93 RID: 8083
		// (add) Token: 0x0601082F RID: 67631
		// (remove) Token: 0x06010830 RID: 67632
		public virtual extern event HTMLTableEvents_ondragleaveEventHandler HTMLTableEvents_Event_ondragleave;

		// Token: 0x14001F94 RID: 8084
		// (add) Token: 0x06010831 RID: 67633
		// (remove) Token: 0x06010832 RID: 67634
		public virtual extern event HTMLTableEvents_ondropEventHandler HTMLTableEvents_Event_ondrop;

		// Token: 0x14001F95 RID: 8085
		// (add) Token: 0x06010833 RID: 67635
		// (remove) Token: 0x06010834 RID: 67636
		public virtual extern event HTMLTableEvents_onbeforecutEventHandler HTMLTableEvents_Event_onbeforecut;

		// Token: 0x14001F96 RID: 8086
		// (add) Token: 0x06010835 RID: 67637
		// (remove) Token: 0x06010836 RID: 67638
		public virtual extern event HTMLTableEvents_oncutEventHandler HTMLTableEvents_Event_oncut;

		// Token: 0x14001F97 RID: 8087
		// (add) Token: 0x06010837 RID: 67639
		// (remove) Token: 0x06010838 RID: 67640
		public virtual extern event HTMLTableEvents_onbeforecopyEventHandler HTMLTableEvents_Event_onbeforecopy;

		// Token: 0x14001F98 RID: 8088
		// (add) Token: 0x06010839 RID: 67641
		// (remove) Token: 0x0601083A RID: 67642
		public virtual extern event HTMLTableEvents_oncopyEventHandler HTMLTableEvents_Event_oncopy;

		// Token: 0x14001F99 RID: 8089
		// (add) Token: 0x0601083B RID: 67643
		// (remove) Token: 0x0601083C RID: 67644
		public virtual extern event HTMLTableEvents_onbeforepasteEventHandler HTMLTableEvents_Event_onbeforepaste;

		// Token: 0x14001F9A RID: 8090
		// (add) Token: 0x0601083D RID: 67645
		// (remove) Token: 0x0601083E RID: 67646
		public virtual extern event HTMLTableEvents_onpasteEventHandler HTMLTableEvents_Event_onpaste;

		// Token: 0x14001F9B RID: 8091
		// (add) Token: 0x0601083F RID: 67647
		// (remove) Token: 0x06010840 RID: 67648
		public virtual extern event HTMLTableEvents_oncontextmenuEventHandler HTMLTableEvents_Event_oncontextmenu;

		// Token: 0x14001F9C RID: 8092
		// (add) Token: 0x06010841 RID: 67649
		// (remove) Token: 0x06010842 RID: 67650
		public virtual extern event HTMLTableEvents_onrowsdeleteEventHandler HTMLTableEvents_Event_onrowsdelete;

		// Token: 0x14001F9D RID: 8093
		// (add) Token: 0x06010843 RID: 67651
		// (remove) Token: 0x06010844 RID: 67652
		public virtual extern event HTMLTableEvents_onrowsinsertedEventHandler HTMLTableEvents_Event_onrowsinserted;

		// Token: 0x14001F9E RID: 8094
		// (add) Token: 0x06010845 RID: 67653
		// (remove) Token: 0x06010846 RID: 67654
		public virtual extern event HTMLTableEvents_oncellchangeEventHandler HTMLTableEvents_Event_oncellchange;

		// Token: 0x14001F9F RID: 8095
		// (add) Token: 0x06010847 RID: 67655
		// (remove) Token: 0x06010848 RID: 67656
		public virtual extern event HTMLTableEvents_onreadystatechangeEventHandler HTMLTableEvents_Event_onreadystatechange;

		// Token: 0x14001FA0 RID: 8096
		// (add) Token: 0x06010849 RID: 67657
		// (remove) Token: 0x0601084A RID: 67658
		public virtual extern event HTMLTableEvents_onbeforeeditfocusEventHandler HTMLTableEvents_Event_onbeforeeditfocus;

		// Token: 0x14001FA1 RID: 8097
		// (add) Token: 0x0601084B RID: 67659
		// (remove) Token: 0x0601084C RID: 67660
		public virtual extern event HTMLTableEvents_onlayoutcompleteEventHandler HTMLTableEvents_Event_onlayoutcomplete;

		// Token: 0x14001FA2 RID: 8098
		// (add) Token: 0x0601084D RID: 67661
		// (remove) Token: 0x0601084E RID: 67662
		public virtual extern event HTMLTableEvents_onpageEventHandler HTMLTableEvents_Event_onpage;

		// Token: 0x14001FA3 RID: 8099
		// (add) Token: 0x0601084F RID: 67663
		// (remove) Token: 0x06010850 RID: 67664
		public virtual extern event HTMLTableEvents_onbeforedeactivateEventHandler HTMLTableEvents_Event_onbeforedeactivate;

		// Token: 0x14001FA4 RID: 8100
		// (add) Token: 0x06010851 RID: 67665
		// (remove) Token: 0x06010852 RID: 67666
		public virtual extern event HTMLTableEvents_onbeforeactivateEventHandler HTMLTableEvents_Event_onbeforeactivate;

		// Token: 0x14001FA5 RID: 8101
		// (add) Token: 0x06010853 RID: 67667
		// (remove) Token: 0x06010854 RID: 67668
		public virtual extern event HTMLTableEvents_onmoveEventHandler HTMLTableEvents_Event_onmove;

		// Token: 0x14001FA6 RID: 8102
		// (add) Token: 0x06010855 RID: 67669
		// (remove) Token: 0x06010856 RID: 67670
		public virtual extern event HTMLTableEvents_oncontrolselectEventHandler HTMLTableEvents_Event_oncontrolselect;

		// Token: 0x14001FA7 RID: 8103
		// (add) Token: 0x06010857 RID: 67671
		// (remove) Token: 0x06010858 RID: 67672
		public virtual extern event HTMLTableEvents_onmovestartEventHandler HTMLTableEvents_Event_onmovestart;

		// Token: 0x14001FA8 RID: 8104
		// (add) Token: 0x06010859 RID: 67673
		// (remove) Token: 0x0601085A RID: 67674
		public virtual extern event HTMLTableEvents_onmoveendEventHandler HTMLTableEvents_Event_onmoveend;

		// Token: 0x14001FA9 RID: 8105
		// (add) Token: 0x0601085B RID: 67675
		// (remove) Token: 0x0601085C RID: 67676
		public virtual extern event HTMLTableEvents_onresizestartEventHandler HTMLTableEvents_Event_onresizestart;

		// Token: 0x14001FAA RID: 8106
		// (add) Token: 0x0601085D RID: 67677
		// (remove) Token: 0x0601085E RID: 67678
		public virtual extern event HTMLTableEvents_onresizeendEventHandler HTMLTableEvents_Event_onresizeend;

		// Token: 0x14001FAB RID: 8107
		// (add) Token: 0x0601085F RID: 67679
		// (remove) Token: 0x06010860 RID: 67680
		public virtual extern event HTMLTableEvents_onmouseenterEventHandler HTMLTableEvents_Event_onmouseenter;

		// Token: 0x14001FAC RID: 8108
		// (add) Token: 0x06010861 RID: 67681
		// (remove) Token: 0x06010862 RID: 67682
		public virtual extern event HTMLTableEvents_onmouseleaveEventHandler HTMLTableEvents_Event_onmouseleave;

		// Token: 0x14001FAD RID: 8109
		// (add) Token: 0x06010863 RID: 67683
		// (remove) Token: 0x06010864 RID: 67684
		public virtual extern event HTMLTableEvents_onmousewheelEventHandler HTMLTableEvents_Event_onmousewheel;

		// Token: 0x14001FAE RID: 8110
		// (add) Token: 0x06010865 RID: 67685
		// (remove) Token: 0x06010866 RID: 67686
		public virtual extern event HTMLTableEvents_onactivateEventHandler HTMLTableEvents_Event_onactivate;

		// Token: 0x14001FAF RID: 8111
		// (add) Token: 0x06010867 RID: 67687
		// (remove) Token: 0x06010868 RID: 67688
		public virtual extern event HTMLTableEvents_ondeactivateEventHandler HTMLTableEvents_Event_ondeactivate;

		// Token: 0x14001FB0 RID: 8112
		// (add) Token: 0x06010869 RID: 67689
		// (remove) Token: 0x0601086A RID: 67690
		public virtual extern event HTMLTableEvents_onfocusinEventHandler HTMLTableEvents_Event_onfocusin;

		// Token: 0x14001FB1 RID: 8113
		// (add) Token: 0x0601086B RID: 67691
		// (remove) Token: 0x0601086C RID: 67692
		public virtual extern event HTMLTableEvents_onfocusoutEventHandler HTMLTableEvents_Event_onfocusout;

		// Token: 0x14001FB2 RID: 8114
		// (add) Token: 0x0601086D RID: 67693
		// (remove) Token: 0x0601086E RID: 67694
		public virtual extern event HTMLTableEvents2_onhelpEventHandler HTMLTableEvents2_Event_onhelp;

		// Token: 0x14001FB3 RID: 8115
		// (add) Token: 0x0601086F RID: 67695
		// (remove) Token: 0x06010870 RID: 67696
		public virtual extern event HTMLTableEvents2_onclickEventHandler HTMLTableEvents2_Event_onclick;

		// Token: 0x14001FB4 RID: 8116
		// (add) Token: 0x06010871 RID: 67697
		// (remove) Token: 0x06010872 RID: 67698
		public virtual extern event HTMLTableEvents2_ondblclickEventHandler HTMLTableEvents2_Event_ondblclick;

		// Token: 0x14001FB5 RID: 8117
		// (add) Token: 0x06010873 RID: 67699
		// (remove) Token: 0x06010874 RID: 67700
		public virtual extern event HTMLTableEvents2_onkeypressEventHandler HTMLTableEvents2_Event_onkeypress;

		// Token: 0x14001FB6 RID: 8118
		// (add) Token: 0x06010875 RID: 67701
		// (remove) Token: 0x06010876 RID: 67702
		public virtual extern event HTMLTableEvents2_onkeydownEventHandler HTMLTableEvents2_Event_onkeydown;

		// Token: 0x14001FB7 RID: 8119
		// (add) Token: 0x06010877 RID: 67703
		// (remove) Token: 0x06010878 RID: 67704
		public virtual extern event HTMLTableEvents2_onkeyupEventHandler HTMLTableEvents2_Event_onkeyup;

		// Token: 0x14001FB8 RID: 8120
		// (add) Token: 0x06010879 RID: 67705
		// (remove) Token: 0x0601087A RID: 67706
		public virtual extern event HTMLTableEvents2_onmouseoutEventHandler HTMLTableEvents2_Event_onmouseout;

		// Token: 0x14001FB9 RID: 8121
		// (add) Token: 0x0601087B RID: 67707
		// (remove) Token: 0x0601087C RID: 67708
		public virtual extern event HTMLTableEvents2_onmouseoverEventHandler HTMLTableEvents2_Event_onmouseover;

		// Token: 0x14001FBA RID: 8122
		// (add) Token: 0x0601087D RID: 67709
		// (remove) Token: 0x0601087E RID: 67710
		public virtual extern event HTMLTableEvents2_onmousemoveEventHandler HTMLTableEvents2_Event_onmousemove;

		// Token: 0x14001FBB RID: 8123
		// (add) Token: 0x0601087F RID: 67711
		// (remove) Token: 0x06010880 RID: 67712
		public virtual extern event HTMLTableEvents2_onmousedownEventHandler HTMLTableEvents2_Event_onmousedown;

		// Token: 0x14001FBC RID: 8124
		// (add) Token: 0x06010881 RID: 67713
		// (remove) Token: 0x06010882 RID: 67714
		public virtual extern event HTMLTableEvents2_onmouseupEventHandler HTMLTableEvents2_Event_onmouseup;

		// Token: 0x14001FBD RID: 8125
		// (add) Token: 0x06010883 RID: 67715
		// (remove) Token: 0x06010884 RID: 67716
		public virtual extern event HTMLTableEvents2_onselectstartEventHandler HTMLTableEvents2_Event_onselectstart;

		// Token: 0x14001FBE RID: 8126
		// (add) Token: 0x06010885 RID: 67717
		// (remove) Token: 0x06010886 RID: 67718
		public virtual extern event HTMLTableEvents2_onfilterchangeEventHandler HTMLTableEvents2_Event_onfilterchange;

		// Token: 0x14001FBF RID: 8127
		// (add) Token: 0x06010887 RID: 67719
		// (remove) Token: 0x06010888 RID: 67720
		public virtual extern event HTMLTableEvents2_ondragstartEventHandler HTMLTableEvents2_Event_ondragstart;

		// Token: 0x14001FC0 RID: 8128
		// (add) Token: 0x06010889 RID: 67721
		// (remove) Token: 0x0601088A RID: 67722
		public virtual extern event HTMLTableEvents2_onbeforeupdateEventHandler HTMLTableEvents2_Event_onbeforeupdate;

		// Token: 0x14001FC1 RID: 8129
		// (add) Token: 0x0601088B RID: 67723
		// (remove) Token: 0x0601088C RID: 67724
		public virtual extern event HTMLTableEvents2_onafterupdateEventHandler HTMLTableEvents2_Event_onafterupdate;

		// Token: 0x14001FC2 RID: 8130
		// (add) Token: 0x0601088D RID: 67725
		// (remove) Token: 0x0601088E RID: 67726
		public virtual extern event HTMLTableEvents2_onerrorupdateEventHandler HTMLTableEvents2_Event_onerrorupdate;

		// Token: 0x14001FC3 RID: 8131
		// (add) Token: 0x0601088F RID: 67727
		// (remove) Token: 0x06010890 RID: 67728
		public virtual extern event HTMLTableEvents2_onrowexitEventHandler HTMLTableEvents2_Event_onrowexit;

		// Token: 0x14001FC4 RID: 8132
		// (add) Token: 0x06010891 RID: 67729
		// (remove) Token: 0x06010892 RID: 67730
		public virtual extern event HTMLTableEvents2_onrowenterEventHandler HTMLTableEvents2_Event_onrowenter;

		// Token: 0x14001FC5 RID: 8133
		// (add) Token: 0x06010893 RID: 67731
		// (remove) Token: 0x06010894 RID: 67732
		public virtual extern event HTMLTableEvents2_ondatasetchangedEventHandler HTMLTableEvents2_Event_ondatasetchanged;

		// Token: 0x14001FC6 RID: 8134
		// (add) Token: 0x06010895 RID: 67733
		// (remove) Token: 0x06010896 RID: 67734
		public virtual extern event HTMLTableEvents2_ondataavailableEventHandler HTMLTableEvents2_Event_ondataavailable;

		// Token: 0x14001FC7 RID: 8135
		// (add) Token: 0x06010897 RID: 67735
		// (remove) Token: 0x06010898 RID: 67736
		public virtual extern event HTMLTableEvents2_ondatasetcompleteEventHandler HTMLTableEvents2_Event_ondatasetcomplete;

		// Token: 0x14001FC8 RID: 8136
		// (add) Token: 0x06010899 RID: 67737
		// (remove) Token: 0x0601089A RID: 67738
		public virtual extern event HTMLTableEvents2_onlosecaptureEventHandler HTMLTableEvents2_Event_onlosecapture;

		// Token: 0x14001FC9 RID: 8137
		// (add) Token: 0x0601089B RID: 67739
		// (remove) Token: 0x0601089C RID: 67740
		public virtual extern event HTMLTableEvents2_onpropertychangeEventHandler HTMLTableEvents2_Event_onpropertychange;

		// Token: 0x14001FCA RID: 8138
		// (add) Token: 0x0601089D RID: 67741
		// (remove) Token: 0x0601089E RID: 67742
		public virtual extern event HTMLTableEvents2_onscrollEventHandler HTMLTableEvents2_Event_onscroll;

		// Token: 0x14001FCB RID: 8139
		// (add) Token: 0x0601089F RID: 67743
		// (remove) Token: 0x060108A0 RID: 67744
		public virtual extern event HTMLTableEvents2_onfocusEventHandler HTMLTableEvents2_Event_onfocus;

		// Token: 0x14001FCC RID: 8140
		// (add) Token: 0x060108A1 RID: 67745
		// (remove) Token: 0x060108A2 RID: 67746
		public virtual extern event HTMLTableEvents2_onblurEventHandler HTMLTableEvents2_Event_onblur;

		// Token: 0x14001FCD RID: 8141
		// (add) Token: 0x060108A3 RID: 67747
		// (remove) Token: 0x060108A4 RID: 67748
		public virtual extern event HTMLTableEvents2_onresizeEventHandler HTMLTableEvents2_Event_onresize;

		// Token: 0x14001FCE RID: 8142
		// (add) Token: 0x060108A5 RID: 67749
		// (remove) Token: 0x060108A6 RID: 67750
		public virtual extern event HTMLTableEvents2_ondragEventHandler HTMLTableEvents2_Event_ondrag;

		// Token: 0x14001FCF RID: 8143
		// (add) Token: 0x060108A7 RID: 67751
		// (remove) Token: 0x060108A8 RID: 67752
		public virtual extern event HTMLTableEvents2_ondragendEventHandler HTMLTableEvents2_Event_ondragend;

		// Token: 0x14001FD0 RID: 8144
		// (add) Token: 0x060108A9 RID: 67753
		// (remove) Token: 0x060108AA RID: 67754
		public virtual extern event HTMLTableEvents2_ondragenterEventHandler HTMLTableEvents2_Event_ondragenter;

		// Token: 0x14001FD1 RID: 8145
		// (add) Token: 0x060108AB RID: 67755
		// (remove) Token: 0x060108AC RID: 67756
		public virtual extern event HTMLTableEvents2_ondragoverEventHandler HTMLTableEvents2_Event_ondragover;

		// Token: 0x14001FD2 RID: 8146
		// (add) Token: 0x060108AD RID: 67757
		// (remove) Token: 0x060108AE RID: 67758
		public virtual extern event HTMLTableEvents2_ondragleaveEventHandler HTMLTableEvents2_Event_ondragleave;

		// Token: 0x14001FD3 RID: 8147
		// (add) Token: 0x060108AF RID: 67759
		// (remove) Token: 0x060108B0 RID: 67760
		public virtual extern event HTMLTableEvents2_ondropEventHandler HTMLTableEvents2_Event_ondrop;

		// Token: 0x14001FD4 RID: 8148
		// (add) Token: 0x060108B1 RID: 67761
		// (remove) Token: 0x060108B2 RID: 67762
		public virtual extern event HTMLTableEvents2_onbeforecutEventHandler HTMLTableEvents2_Event_onbeforecut;

		// Token: 0x14001FD5 RID: 8149
		// (add) Token: 0x060108B3 RID: 67763
		// (remove) Token: 0x060108B4 RID: 67764
		public virtual extern event HTMLTableEvents2_oncutEventHandler HTMLTableEvents2_Event_oncut;

		// Token: 0x14001FD6 RID: 8150
		// (add) Token: 0x060108B5 RID: 67765
		// (remove) Token: 0x060108B6 RID: 67766
		public virtual extern event HTMLTableEvents2_onbeforecopyEventHandler HTMLTableEvents2_Event_onbeforecopy;

		// Token: 0x14001FD7 RID: 8151
		// (add) Token: 0x060108B7 RID: 67767
		// (remove) Token: 0x060108B8 RID: 67768
		public virtual extern event HTMLTableEvents2_oncopyEventHandler HTMLTableEvents2_Event_oncopy;

		// Token: 0x14001FD8 RID: 8152
		// (add) Token: 0x060108B9 RID: 67769
		// (remove) Token: 0x060108BA RID: 67770
		public virtual extern event HTMLTableEvents2_onbeforepasteEventHandler HTMLTableEvents2_Event_onbeforepaste;

		// Token: 0x14001FD9 RID: 8153
		// (add) Token: 0x060108BB RID: 67771
		// (remove) Token: 0x060108BC RID: 67772
		public virtual extern event HTMLTableEvents2_onpasteEventHandler HTMLTableEvents2_Event_onpaste;

		// Token: 0x14001FDA RID: 8154
		// (add) Token: 0x060108BD RID: 67773
		// (remove) Token: 0x060108BE RID: 67774
		public virtual extern event HTMLTableEvents2_oncontextmenuEventHandler HTMLTableEvents2_Event_oncontextmenu;

		// Token: 0x14001FDB RID: 8155
		// (add) Token: 0x060108BF RID: 67775
		// (remove) Token: 0x060108C0 RID: 67776
		public virtual extern event HTMLTableEvents2_onrowsdeleteEventHandler HTMLTableEvents2_Event_onrowsdelete;

		// Token: 0x14001FDC RID: 8156
		// (add) Token: 0x060108C1 RID: 67777
		// (remove) Token: 0x060108C2 RID: 67778
		public virtual extern event HTMLTableEvents2_onrowsinsertedEventHandler HTMLTableEvents2_Event_onrowsinserted;

		// Token: 0x14001FDD RID: 8157
		// (add) Token: 0x060108C3 RID: 67779
		// (remove) Token: 0x060108C4 RID: 67780
		public virtual extern event HTMLTableEvents2_oncellchangeEventHandler HTMLTableEvents2_Event_oncellchange;

		// Token: 0x14001FDE RID: 8158
		// (add) Token: 0x060108C5 RID: 67781
		// (remove) Token: 0x060108C6 RID: 67782
		public virtual extern event HTMLTableEvents2_onreadystatechangeEventHandler HTMLTableEvents2_Event_onreadystatechange;

		// Token: 0x14001FDF RID: 8159
		// (add) Token: 0x060108C7 RID: 67783
		// (remove) Token: 0x060108C8 RID: 67784
		public virtual extern event HTMLTableEvents2_onlayoutcompleteEventHandler HTMLTableEvents2_Event_onlayoutcomplete;

		// Token: 0x14001FE0 RID: 8160
		// (add) Token: 0x060108C9 RID: 67785
		// (remove) Token: 0x060108CA RID: 67786
		public virtual extern event HTMLTableEvents2_onpageEventHandler HTMLTableEvents2_Event_onpage;

		// Token: 0x14001FE1 RID: 8161
		// (add) Token: 0x060108CB RID: 67787
		// (remove) Token: 0x060108CC RID: 67788
		public virtual extern event HTMLTableEvents2_onmouseenterEventHandler HTMLTableEvents2_Event_onmouseenter;

		// Token: 0x14001FE2 RID: 8162
		// (add) Token: 0x060108CD RID: 67789
		// (remove) Token: 0x060108CE RID: 67790
		public virtual extern event HTMLTableEvents2_onmouseleaveEventHandler HTMLTableEvents2_Event_onmouseleave;

		// Token: 0x14001FE3 RID: 8163
		// (add) Token: 0x060108CF RID: 67791
		// (remove) Token: 0x060108D0 RID: 67792
		public virtual extern event HTMLTableEvents2_onactivateEventHandler HTMLTableEvents2_Event_onactivate;

		// Token: 0x14001FE4 RID: 8164
		// (add) Token: 0x060108D1 RID: 67793
		// (remove) Token: 0x060108D2 RID: 67794
		public virtual extern event HTMLTableEvents2_ondeactivateEventHandler HTMLTableEvents2_Event_ondeactivate;

		// Token: 0x14001FE5 RID: 8165
		// (add) Token: 0x060108D3 RID: 67795
		// (remove) Token: 0x060108D4 RID: 67796
		public virtual extern event HTMLTableEvents2_onbeforedeactivateEventHandler HTMLTableEvents2_Event_onbeforedeactivate;

		// Token: 0x14001FE6 RID: 8166
		// (add) Token: 0x060108D5 RID: 67797
		// (remove) Token: 0x060108D6 RID: 67798
		public virtual extern event HTMLTableEvents2_onbeforeactivateEventHandler HTMLTableEvents2_Event_onbeforeactivate;

		// Token: 0x14001FE7 RID: 8167
		// (add) Token: 0x060108D7 RID: 67799
		// (remove) Token: 0x060108D8 RID: 67800
		public virtual extern event HTMLTableEvents2_onfocusinEventHandler HTMLTableEvents2_Event_onfocusin;

		// Token: 0x14001FE8 RID: 8168
		// (add) Token: 0x060108D9 RID: 67801
		// (remove) Token: 0x060108DA RID: 67802
		public virtual extern event HTMLTableEvents2_onfocusoutEventHandler HTMLTableEvents2_Event_onfocusout;

		// Token: 0x14001FE9 RID: 8169
		// (add) Token: 0x060108DB RID: 67803
		// (remove) Token: 0x060108DC RID: 67804
		public virtual extern event HTMLTableEvents2_onmoveEventHandler HTMLTableEvents2_Event_onmove;

		// Token: 0x14001FEA RID: 8170
		// (add) Token: 0x060108DD RID: 67805
		// (remove) Token: 0x060108DE RID: 67806
		public virtual extern event HTMLTableEvents2_oncontrolselectEventHandler HTMLTableEvents2_Event_oncontrolselect;

		// Token: 0x14001FEB RID: 8171
		// (add) Token: 0x060108DF RID: 67807
		// (remove) Token: 0x060108E0 RID: 67808
		public virtual extern event HTMLTableEvents2_onmovestartEventHandler HTMLTableEvents2_Event_onmovestart;

		// Token: 0x14001FEC RID: 8172
		// (add) Token: 0x060108E1 RID: 67809
		// (remove) Token: 0x060108E2 RID: 67810
		public virtual extern event HTMLTableEvents2_onmoveendEventHandler HTMLTableEvents2_Event_onmoveend;

		// Token: 0x14001FED RID: 8173
		// (add) Token: 0x060108E3 RID: 67811
		// (remove) Token: 0x060108E4 RID: 67812
		public virtual extern event HTMLTableEvents2_onresizestartEventHandler HTMLTableEvents2_Event_onresizestart;

		// Token: 0x14001FEE RID: 8174
		// (add) Token: 0x060108E5 RID: 67813
		// (remove) Token: 0x060108E6 RID: 67814
		public virtual extern event HTMLTableEvents2_onresizeendEventHandler HTMLTableEvents2_Event_onresizeend;

		// Token: 0x14001FEF RID: 8175
		// (add) Token: 0x060108E7 RID: 67815
		// (remove) Token: 0x060108E8 RID: 67816
		public virtual extern event HTMLTableEvents2_onmousewheelEventHandler HTMLTableEvents2_Event_onmousewheel;
	}
}
