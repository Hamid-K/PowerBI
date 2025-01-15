using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000BEC RID: 3052
	[Guid("3050F31A-98B5-11CF-BB82-00AA00BDCE0B")]
	[TypeLibType(2)]
	[ClassInterface(0)]
	[ComSourceInterfaces("mshtml.HTMLControlElementEvents\0mshtml.HTMLControlElementEvents2\0\0")]
	[ComImport]
	public class HTMLFrameSetSiteClass : DispHTMLFrameSetSite, HTMLFrameSetSite, HTMLControlElementEvents_Event, IHTMLElement, IHTMLElement2, IHTMLElement3, IHTMLElement4, IHTMLUniqueName, IHTMLDOMNode, IHTMLDOMNode2, IHTMLControlElement, IHTMLFrameSetElement, IHTMLFrameSetElement2, HTMLControlElementEvents2_Event
	{
		// Token: 0x06015382 RID: 86914
		[MethodImpl(4096, MethodCodeType = 3)]
		public extern HTMLFrameSetSiteClass();

		// Token: 0x06015383 RID: 86915
		[DispId(-2147417611)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void setAttribute([MarshalAs(19)] [In] string strAttributeName, [MarshalAs(27)] [In] object AttributeValue, [In] int lFlags = 1);

		// Token: 0x06015384 RID: 86916
		[DispId(-2147417610)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		public virtual extern object getAttribute([MarshalAs(19)] [In] string strAttributeName, [In] int lFlags = 0);

		// Token: 0x06015385 RID: 86917
		[DispId(-2147417609)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool removeAttribute([MarshalAs(19)] [In] string strAttributeName, [In] int lFlags = 1);

		// Token: 0x17007136 RID: 28982
		// (get) Token: 0x06015387 RID: 86919
		// (set) Token: 0x06015386 RID: 86918
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

		// Token: 0x17007137 RID: 28983
		// (get) Token: 0x06015389 RID: 86921
		// (set) Token: 0x06015388 RID: 86920
		[DispId(-2147417110)]
		public virtual extern string id
		{
			[DispId(-2147417110)]
			[TypeLibFunc(4)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147417110)]
			[TypeLibFunc(4)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17007138 RID: 28984
		// (get) Token: 0x0601538A RID: 86922
		[DispId(-2147417108)]
		public virtual extern string tagName
		{
			[DispId(-2147417108)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17007139 RID: 28985
		// (get) Token: 0x0601538B RID: 86923
		[DispId(-2147418104)]
		public virtual extern IHTMLElement parentElement
		{
			[DispId(-2147418104)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x1700713A RID: 28986
		// (get) Token: 0x0601538C RID: 86924
		[DispId(-2147418038)]
		public virtual extern IHTMLStyle style
		{
			[DispId(-2147418038)]
			[TypeLibFunc(1024)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x1700713B RID: 28987
		// (get) Token: 0x0601538E RID: 86926
		// (set) Token: 0x0601538D RID: 86925
		[DispId(-2147412099)]
		public virtual extern object onhelp
		{
			[TypeLibFunc(20)]
			[DispId(-2147412099)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412099)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x1700713C RID: 28988
		// (get) Token: 0x06015390 RID: 86928
		// (set) Token: 0x0601538F RID: 86927
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

		// Token: 0x1700713D RID: 28989
		// (get) Token: 0x06015392 RID: 86930
		// (set) Token: 0x06015391 RID: 86929
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

		// Token: 0x1700713E RID: 28990
		// (get) Token: 0x06015394 RID: 86932
		// (set) Token: 0x06015393 RID: 86931
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

		// Token: 0x1700713F RID: 28991
		// (get) Token: 0x06015396 RID: 86934
		// (set) Token: 0x06015395 RID: 86933
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

		// Token: 0x17007140 RID: 28992
		// (get) Token: 0x06015398 RID: 86936
		// (set) Token: 0x06015397 RID: 86935
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

		// Token: 0x17007141 RID: 28993
		// (get) Token: 0x0601539A RID: 86938
		// (set) Token: 0x06015399 RID: 86937
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

		// Token: 0x17007142 RID: 28994
		// (get) Token: 0x0601539C RID: 86940
		// (set) Token: 0x0601539B RID: 86939
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

		// Token: 0x17007143 RID: 28995
		// (get) Token: 0x0601539E RID: 86942
		// (set) Token: 0x0601539D RID: 86941
		[DispId(-2147412108)]
		public virtual extern object onmousemove
		{
			[TypeLibFunc(20)]
			[DispId(-2147412108)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412108)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17007144 RID: 28996
		// (get) Token: 0x060153A0 RID: 86944
		// (set) Token: 0x0601539F RID: 86943
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

		// Token: 0x17007145 RID: 28997
		// (get) Token: 0x060153A2 RID: 86946
		// (set) Token: 0x060153A1 RID: 86945
		[DispId(-2147412109)]
		public virtual extern object onmouseup
		{
			[DispId(-2147412109)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412109)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17007146 RID: 28998
		// (get) Token: 0x060153A3 RID: 86947
		[DispId(-2147417094)]
		public virtual extern object document
		{
			[DispId(-2147417094)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17007147 RID: 28999
		// (get) Token: 0x060153A5 RID: 86949
		// (set) Token: 0x060153A4 RID: 86948
		[DispId(-2147418043)]
		public virtual extern string title
		{
			[TypeLibFunc(20)]
			[DispId(-2147418043)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147418043)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17007148 RID: 29000
		// (get) Token: 0x060153A7 RID: 86951
		// (set) Token: 0x060153A6 RID: 86950
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

		// Token: 0x17007149 RID: 29001
		// (get) Token: 0x060153A9 RID: 86953
		// (set) Token: 0x060153A8 RID: 86952
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

		// Token: 0x060153AA RID: 86954
		[DispId(-2147417093)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void scrollIntoView([MarshalAs(27)] [In] [Optional] object varargStart);

		// Token: 0x060153AB RID: 86955
		[DispId(-2147417092)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool contains([MarshalAs(28)] [In] IHTMLElement pChild);

		// Token: 0x1700714A RID: 29002
		// (get) Token: 0x060153AC RID: 86956
		[DispId(-2147417088)]
		public virtual extern int sourceIndex
		{
			[TypeLibFunc(4)]
			[DispId(-2147417088)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700714B RID: 29003
		// (get) Token: 0x060153AD RID: 86957
		[DispId(-2147417087)]
		public virtual extern object recordNumber
		{
			[DispId(-2147417087)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x1700714C RID: 29004
		// (get) Token: 0x060153AF RID: 86959
		// (set) Token: 0x060153AE RID: 86958
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

		// Token: 0x1700714D RID: 29005
		// (get) Token: 0x060153B0 RID: 86960
		[DispId(-2147417104)]
		public virtual extern int offsetLeft
		{
			[DispId(-2147417104)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700714E RID: 29006
		// (get) Token: 0x060153B1 RID: 86961
		[DispId(-2147417103)]
		public virtual extern int offsetTop
		{
			[DispId(-2147417103)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700714F RID: 29007
		// (get) Token: 0x060153B2 RID: 86962
		[DispId(-2147417102)]
		public virtual extern int offsetWidth
		{
			[DispId(-2147417102)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17007150 RID: 29008
		// (get) Token: 0x060153B3 RID: 86963
		[DispId(-2147417101)]
		public virtual extern int offsetHeight
		{
			[DispId(-2147417101)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17007151 RID: 29009
		// (get) Token: 0x060153B4 RID: 86964
		[DispId(-2147417100)]
		public virtual extern IHTMLElement offsetParent
		{
			[DispId(-2147417100)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17007152 RID: 29010
		// (get) Token: 0x060153B6 RID: 86966
		// (set) Token: 0x060153B5 RID: 86965
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

		// Token: 0x17007153 RID: 29011
		// (get) Token: 0x060153B8 RID: 86968
		// (set) Token: 0x060153B7 RID: 86967
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

		// Token: 0x17007154 RID: 29012
		// (get) Token: 0x060153BA RID: 86970
		// (set) Token: 0x060153B9 RID: 86969
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

		// Token: 0x17007155 RID: 29013
		// (get) Token: 0x060153BC RID: 86972
		// (set) Token: 0x060153BB RID: 86971
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

		// Token: 0x060153BD RID: 86973
		[DispId(-2147417082)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void insertAdjacentHTML([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string html);

		// Token: 0x060153BE RID: 86974
		[DispId(-2147417081)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void insertAdjacentText([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string text);

		// Token: 0x17007156 RID: 29014
		// (get) Token: 0x060153BF RID: 86975
		[DispId(-2147417080)]
		public virtual extern IHTMLElement parentTextEdit
		{
			[DispId(-2147417080)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17007157 RID: 29015
		// (get) Token: 0x060153C0 RID: 86976
		[DispId(-2147417078)]
		public virtual extern bool isTextEdit
		{
			[DispId(-2147417078)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x060153C1 RID: 86977
		[DispId(-2147417079)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void click();

		// Token: 0x17007158 RID: 29016
		// (get) Token: 0x060153C2 RID: 86978
		[DispId(-2147417077)]
		public virtual extern IHTMLFiltersCollection filters
		{
			[DispId(-2147417077)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17007159 RID: 29017
		// (get) Token: 0x060153C4 RID: 86980
		// (set) Token: 0x060153C3 RID: 86979
		[DispId(-2147412077)]
		public virtual extern object ondragstart
		{
			[DispId(-2147412077)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412077)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x060153C5 RID: 86981
		[DispId(-2147417076)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string toString();

		// Token: 0x1700715A RID: 29018
		// (get) Token: 0x060153C7 RID: 86983
		// (set) Token: 0x060153C6 RID: 86982
		[DispId(-2147412091)]
		public virtual extern object onbeforeupdate
		{
			[TypeLibFunc(20)]
			[DispId(-2147412091)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412091)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x1700715B RID: 29019
		// (get) Token: 0x060153C9 RID: 86985
		// (set) Token: 0x060153C8 RID: 86984
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

		// Token: 0x1700715C RID: 29020
		// (get) Token: 0x060153CB RID: 86987
		// (set) Token: 0x060153CA RID: 86986
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

		// Token: 0x1700715D RID: 29021
		// (get) Token: 0x060153CD RID: 86989
		// (set) Token: 0x060153CC RID: 86988
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

		// Token: 0x1700715E RID: 29022
		// (get) Token: 0x060153CF RID: 86991
		// (set) Token: 0x060153CE RID: 86990
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

		// Token: 0x1700715F RID: 29023
		// (get) Token: 0x060153D1 RID: 86993
		// (set) Token: 0x060153D0 RID: 86992
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

		// Token: 0x17007160 RID: 29024
		// (get) Token: 0x060153D3 RID: 86995
		// (set) Token: 0x060153D2 RID: 86994
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

		// Token: 0x17007161 RID: 29025
		// (get) Token: 0x060153D5 RID: 86997
		// (set) Token: 0x060153D4 RID: 86996
		[DispId(-2147412070)]
		public virtual extern object ondatasetcomplete
		{
			[TypeLibFunc(20)]
			[DispId(-2147412070)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412070)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17007162 RID: 29026
		// (get) Token: 0x060153D7 RID: 86999
		// (set) Token: 0x060153D6 RID: 86998
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

		// Token: 0x17007163 RID: 29027
		// (get) Token: 0x060153D8 RID: 87000
		[DispId(-2147417075)]
		public virtual extern object children
		{
			[DispId(-2147417075)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17007164 RID: 29028
		// (get) Token: 0x060153D9 RID: 87001
		[DispId(-2147417074)]
		public virtual extern object all
		{
			[DispId(-2147417074)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17007165 RID: 29029
		// (get) Token: 0x060153DA RID: 87002
		[DispId(-2147417073)]
		public virtual extern string scopeName
		{
			[DispId(-2147417073)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x060153DB RID: 87003
		[DispId(-2147417072)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void setCapture([In] bool containerCapture = true);

		// Token: 0x060153DC RID: 87004
		[DispId(-2147417071)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void releaseCapture();

		// Token: 0x17007166 RID: 29030
		// (get) Token: 0x060153DE RID: 87006
		// (set) Token: 0x060153DD RID: 87005
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

		// Token: 0x060153DF RID: 87007
		[DispId(-2147417070)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string componentFromPoint([In] int x, [In] int y);

		// Token: 0x060153E0 RID: 87008
		[DispId(-2147417069)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void doScroll([MarshalAs(27)] [In] [Optional] object component);

		// Token: 0x17007167 RID: 29031
		// (get) Token: 0x060153E2 RID: 87010
		// (set) Token: 0x060153E1 RID: 87009
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

		// Token: 0x17007168 RID: 29032
		// (get) Token: 0x060153E4 RID: 87012
		// (set) Token: 0x060153E3 RID: 87011
		[DispId(-2147412063)]
		public virtual extern object ondrag
		{
			[DispId(-2147412063)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412063)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17007169 RID: 29033
		// (get) Token: 0x060153E6 RID: 87014
		// (set) Token: 0x060153E5 RID: 87013
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

		// Token: 0x1700716A RID: 29034
		// (get) Token: 0x060153E8 RID: 87016
		// (set) Token: 0x060153E7 RID: 87015
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

		// Token: 0x1700716B RID: 29035
		// (get) Token: 0x060153EA RID: 87018
		// (set) Token: 0x060153E9 RID: 87017
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

		// Token: 0x1700716C RID: 29036
		// (get) Token: 0x060153EC RID: 87020
		// (set) Token: 0x060153EB RID: 87019
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

		// Token: 0x1700716D RID: 29037
		// (get) Token: 0x060153EE RID: 87022
		// (set) Token: 0x060153ED RID: 87021
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

		// Token: 0x1700716E RID: 29038
		// (get) Token: 0x060153F0 RID: 87024
		// (set) Token: 0x060153EF RID: 87023
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

		// Token: 0x1700716F RID: 29039
		// (get) Token: 0x060153F2 RID: 87026
		// (set) Token: 0x060153F1 RID: 87025
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

		// Token: 0x17007170 RID: 29040
		// (get) Token: 0x060153F4 RID: 87028
		// (set) Token: 0x060153F3 RID: 87027
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

		// Token: 0x17007171 RID: 29041
		// (get) Token: 0x060153F6 RID: 87030
		// (set) Token: 0x060153F5 RID: 87029
		[DispId(-2147412056)]
		public virtual extern object oncopy
		{
			[DispId(-2147412056)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412056)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17007172 RID: 29042
		// (get) Token: 0x060153F8 RID: 87032
		// (set) Token: 0x060153F7 RID: 87031
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

		// Token: 0x17007173 RID: 29043
		// (get) Token: 0x060153FA RID: 87034
		// (set) Token: 0x060153F9 RID: 87033
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

		// Token: 0x17007174 RID: 29044
		// (get) Token: 0x060153FB RID: 87035
		[DispId(-2147417105)]
		public virtual extern IHTMLCurrentStyle currentStyle
		{
			[DispId(-2147417105)]
			[TypeLibFunc(1024)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17007175 RID: 29045
		// (get) Token: 0x060153FD RID: 87037
		// (set) Token: 0x060153FC RID: 87036
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

		// Token: 0x060153FE RID: 87038
		[DispId(-2147417068)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLRectCollection getClientRects();

		// Token: 0x060153FF RID: 87039
		[DispId(-2147417067)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLRect getBoundingClientRect();

		// Token: 0x06015400 RID: 87040
		[DispId(-2147417608)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void setExpression([MarshalAs(19)] [In] string propname, [MarshalAs(19)] [In] string expression, [MarshalAs(19)] [In] string language = "");

		// Token: 0x06015401 RID: 87041
		[DispId(-2147417607)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		public virtual extern object getExpression([MarshalAs(19)] [In] string propname);

		// Token: 0x06015402 RID: 87042
		[DispId(-2147417606)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool removeExpression([MarshalAs(19)] [In] string propname);

		// Token: 0x17007176 RID: 29046
		// (get) Token: 0x06015404 RID: 87044
		// (set) Token: 0x06015403 RID: 87043
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

		// Token: 0x06015405 RID: 87045
		[DispId(-2147416112)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void focus();

		// Token: 0x17007177 RID: 29047
		// (get) Token: 0x06015407 RID: 87047
		// (set) Token: 0x06015406 RID: 87046
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

		// Token: 0x17007178 RID: 29048
		// (get) Token: 0x06015409 RID: 87049
		// (set) Token: 0x06015408 RID: 87048
		[DispId(-2147412097)]
		public virtual extern object onblur
		{
			[DispId(-2147412097)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412097)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17007179 RID: 29049
		// (get) Token: 0x0601540B RID: 87051
		// (set) Token: 0x0601540A RID: 87050
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

		// Token: 0x1700717A RID: 29050
		// (get) Token: 0x0601540D RID: 87053
		// (set) Token: 0x0601540C RID: 87052
		[DispId(-2147412076)]
		public virtual extern object onresize
		{
			[DispId(-2147412076)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412076)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x0601540E RID: 87054
		[DispId(-2147416110)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void blur();

		// Token: 0x0601540F RID: 87055
		[DispId(-2147416095)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void addFilter([MarshalAs(25)] [In] object pUnk);

		// Token: 0x06015410 RID: 87056
		[DispId(-2147416094)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void removeFilter([MarshalAs(25)] [In] object pUnk);

		// Token: 0x1700717B RID: 29051
		// (get) Token: 0x06015411 RID: 87057
		[DispId(-2147416093)]
		public virtual extern int clientHeight
		{
			[TypeLibFunc(20)]
			[DispId(-2147416093)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700717C RID: 29052
		// (get) Token: 0x06015412 RID: 87058
		[DispId(-2147416092)]
		public virtual extern int clientWidth
		{
			[TypeLibFunc(20)]
			[DispId(-2147416092)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700717D RID: 29053
		// (get) Token: 0x06015413 RID: 87059
		[DispId(-2147416091)]
		public virtual extern int clientTop
		{
			[DispId(-2147416091)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700717E RID: 29054
		// (get) Token: 0x06015414 RID: 87060
		[DispId(-2147416090)]
		public virtual extern int clientLeft
		{
			[DispId(-2147416090)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x06015415 RID: 87061
		[DispId(-2147417605)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool attachEvent([MarshalAs(19)] [In] string @event, [MarshalAs(26)] [In] object pdisp);

		// Token: 0x06015416 RID: 87062
		[DispId(-2147417604)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void detachEvent([MarshalAs(19)] [In] string @event, [MarshalAs(26)] [In] object pdisp);

		// Token: 0x1700717F RID: 29055
		// (get) Token: 0x06015417 RID: 87063
		[DispId(-2147412996)]
		public virtual extern object readyState
		{
			[DispId(-2147412996)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x17007180 RID: 29056
		// (get) Token: 0x06015419 RID: 87065
		// (set) Token: 0x06015418 RID: 87064
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

		// Token: 0x17007181 RID: 29057
		// (get) Token: 0x0601541B RID: 87067
		// (set) Token: 0x0601541A RID: 87066
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

		// Token: 0x17007182 RID: 29058
		// (get) Token: 0x0601541D RID: 87069
		// (set) Token: 0x0601541C RID: 87068
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

		// Token: 0x17007183 RID: 29059
		// (get) Token: 0x0601541F RID: 87071
		// (set) Token: 0x0601541E RID: 87070
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

		// Token: 0x17007184 RID: 29060
		// (get) Token: 0x06015421 RID: 87073
		// (set) Token: 0x06015420 RID: 87072
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

		// Token: 0x06015422 RID: 87074
		[DispId(-2147417056)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(26)]
		public virtual extern object createControlRange();

		// Token: 0x17007185 RID: 29061
		// (get) Token: 0x06015423 RID: 87075
		[DispId(-2147417055)]
		public virtual extern int scrollHeight
		{
			[DispId(-2147417055)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17007186 RID: 29062
		// (get) Token: 0x06015424 RID: 87076
		[DispId(-2147417054)]
		public virtual extern int scrollWidth
		{
			[DispId(-2147417054)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17007187 RID: 29063
		// (get) Token: 0x06015426 RID: 87078
		// (set) Token: 0x06015425 RID: 87077
		[DispId(-2147417053)]
		public virtual extern int scrollTop
		{
			[DispId(-2147417053)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147417053)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x17007188 RID: 29064
		// (get) Token: 0x06015428 RID: 87080
		// (set) Token: 0x06015427 RID: 87079
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

		// Token: 0x06015429 RID: 87081
		[DispId(-2147417050)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void clearAttributes();

		// Token: 0x17007189 RID: 29065
		// (get) Token: 0x0601542B RID: 87083
		// (set) Token: 0x0601542A RID: 87082
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

		// Token: 0x0601542C RID: 87084
		[DispId(-2147417043)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElement insertAdjacentElement([MarshalAs(19)] [In] string where, [MarshalAs(28)] [In] IHTMLElement insertedElement);

		// Token: 0x0601542D RID: 87085
		[DispId(-2147417047)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElement applyElement([MarshalAs(28)] [In] IHTMLElement apply, [MarshalAs(19)] [In] string where);

		// Token: 0x0601542E RID: 87086
		[DispId(-2147417042)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string getAdjacentText([MarshalAs(19)] [In] string where);

		// Token: 0x0601542F RID: 87087
		[DispId(-2147417041)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string replaceAdjacentText([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string newText);

		// Token: 0x1700718A RID: 29066
		// (get) Token: 0x06015430 RID: 87088
		[DispId(-2147417040)]
		public virtual extern bool canHaveChildren
		{
			[DispId(-2147417040)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x06015431 RID: 87089
		[DispId(-2147417032)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern int addBehavior([MarshalAs(19)] [In] string bstrUrl, [MarshalAs(27)] [In] [Optional] ref object pvarFactory);

		// Token: 0x06015432 RID: 87090
		[DispId(-2147417031)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool removeBehavior([In] int cookie);

		// Token: 0x1700718B RID: 29067
		// (get) Token: 0x06015433 RID: 87091
		[DispId(-2147417048)]
		public virtual extern IHTMLStyle runtimeStyle
		{
			[TypeLibFunc(1024)]
			[DispId(-2147417048)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x1700718C RID: 29068
		// (get) Token: 0x06015434 RID: 87092
		[DispId(-2147417030)]
		public virtual extern object behaviorUrns
		{
			[DispId(-2147417030)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x1700718D RID: 29069
		// (get) Token: 0x06015436 RID: 87094
		// (set) Token: 0x06015435 RID: 87093
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

		// Token: 0x1700718E RID: 29070
		// (get) Token: 0x06015438 RID: 87096
		// (set) Token: 0x06015437 RID: 87095
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

		// Token: 0x1700718F RID: 29071
		// (get) Token: 0x06015439 RID: 87097
		[DispId(-2147417028)]
		public virtual extern int readyStateValue
		{
			[DispId(-2147417028)]
			[TypeLibFunc(65)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x0601543A RID: 87098
		[DispId(-2147417027)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElementCollection getElementsByTagName([MarshalAs(19)] [In] string v);

		// Token: 0x0601543B RID: 87099
		[DispId(-2147417016)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void mergeAttributes([MarshalAs(28)] [In] IHTMLElement mergeThis, [MarshalAs(27)] [In] [Optional] ref object pvarFlags);

		// Token: 0x17007190 RID: 29072
		// (get) Token: 0x0601543C RID: 87100
		[DispId(-2147417015)]
		public virtual extern bool isMultiLine
		{
			[DispId(-2147417015)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17007191 RID: 29073
		// (get) Token: 0x0601543D RID: 87101
		[DispId(-2147417014)]
		public virtual extern bool canHaveHTML
		{
			[DispId(-2147417014)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17007192 RID: 29074
		// (get) Token: 0x0601543F RID: 87103
		// (set) Token: 0x0601543E RID: 87102
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

		// Token: 0x17007193 RID: 29075
		// (get) Token: 0x06015441 RID: 87105
		// (set) Token: 0x06015440 RID: 87104
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

		// Token: 0x17007194 RID: 29076
		// (get) Token: 0x06015443 RID: 87107
		// (set) Token: 0x06015442 RID: 87106
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

		// Token: 0x17007195 RID: 29077
		// (get) Token: 0x06015445 RID: 87109
		// (set) Token: 0x06015444 RID: 87108
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

		// Token: 0x06015446 RID: 87110
		[DispId(-2147417011)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void setActive();

		// Token: 0x17007196 RID: 29078
		// (get) Token: 0x06015448 RID: 87112
		// (set) Token: 0x06015447 RID: 87111
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

		// Token: 0x17007197 RID: 29079
		// (get) Token: 0x06015449 RID: 87113
		[DispId(-2147417010)]
		public virtual extern bool isContentEditable
		{
			[DispId(-2147417010)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17007198 RID: 29080
		// (get) Token: 0x0601544B RID: 87115
		// (set) Token: 0x0601544A RID: 87114
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

		// Token: 0x17007199 RID: 29081
		// (get) Token: 0x0601544D RID: 87117
		// (set) Token: 0x0601544C RID: 87116
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

		// Token: 0x1700719A RID: 29082
		// (get) Token: 0x0601544E RID: 87118
		[DispId(-2147417007)]
		public virtual extern bool isDisabled
		{
			[DispId(-2147417007)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700719B RID: 29083
		// (get) Token: 0x06015450 RID: 87120
		// (set) Token: 0x0601544F RID: 87119
		[DispId(-2147412034)]
		public virtual extern object onmove
		{
			[TypeLibFunc(20)]
			[DispId(-2147412034)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412034)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x1700719C RID: 29084
		// (get) Token: 0x06015452 RID: 87122
		// (set) Token: 0x06015451 RID: 87121
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

		// Token: 0x06015453 RID: 87123
		[DispId(-2147417006)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool FireEvent([MarshalAs(19)] [In] string bstrEventName, [MarshalAs(27)] [In] [Optional] ref object pvarEventObject);

		// Token: 0x1700719D RID: 29085
		// (get) Token: 0x06015455 RID: 87125
		// (set) Token: 0x06015454 RID: 87124
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

		// Token: 0x1700719E RID: 29086
		// (get) Token: 0x06015457 RID: 87127
		// (set) Token: 0x06015456 RID: 87126
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

		// Token: 0x1700719F RID: 29087
		// (get) Token: 0x06015459 RID: 87129
		// (set) Token: 0x06015458 RID: 87128
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

		// Token: 0x170071A0 RID: 29088
		// (get) Token: 0x0601545B RID: 87131
		// (set) Token: 0x0601545A RID: 87130
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

		// Token: 0x170071A1 RID: 29089
		// (get) Token: 0x0601545D RID: 87133
		// (set) Token: 0x0601545C RID: 87132
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

		// Token: 0x170071A2 RID: 29090
		// (get) Token: 0x0601545F RID: 87135
		// (set) Token: 0x0601545E RID: 87134
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

		// Token: 0x170071A3 RID: 29091
		// (get) Token: 0x06015461 RID: 87137
		// (set) Token: 0x06015460 RID: 87136
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

		// Token: 0x170071A4 RID: 29092
		// (get) Token: 0x06015463 RID: 87139
		// (set) Token: 0x06015462 RID: 87138
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

		// Token: 0x06015464 RID: 87140
		[DispId(-2147417005)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool dragDrop();

		// Token: 0x170071A5 RID: 29093
		// (get) Token: 0x06015465 RID: 87141
		[DispId(-2147417004)]
		public virtual extern int glyphMode
		{
			[TypeLibFunc(1089)]
			[DispId(-2147417004)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170071A6 RID: 29094
		// (get) Token: 0x06015467 RID: 87143
		// (set) Token: 0x06015466 RID: 87142
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

		// Token: 0x06015468 RID: 87144
		[DispId(-2147417000)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void normalize();

		// Token: 0x06015469 RID: 87145
		[DispId(-2147417003)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute getAttributeNode([MarshalAs(19)] [In] string bstrName);

		// Token: 0x0601546A RID: 87146
		[DispId(-2147417002)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute setAttributeNode([MarshalAs(28)] [In] IHTMLDOMAttribute pattr);

		// Token: 0x0601546B RID: 87147
		[DispId(-2147417001)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute removeAttributeNode([MarshalAs(28)] [In] IHTMLDOMAttribute pattr);

		// Token: 0x170071A7 RID: 29095
		// (get) Token: 0x0601546D RID: 87149
		// (set) Token: 0x0601546C RID: 87148
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

		// Token: 0x170071A8 RID: 29096
		// (get) Token: 0x0601546F RID: 87151
		// (set) Token: 0x0601546E RID: 87150
		[DispId(-2147412021)]
		public virtual extern object onfocusin
		{
			[TypeLibFunc(20)]
			[DispId(-2147412021)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412021)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170071A9 RID: 29097
		// (get) Token: 0x06015471 RID: 87153
		// (set) Token: 0x06015470 RID: 87152
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

		// Token: 0x170071AA RID: 29098
		// (get) Token: 0x06015472 RID: 87154
		[DispId(-2147417058)]
		public virtual extern int uniqueNumber
		{
			[TypeLibFunc(64)]
			[DispId(-2147417058)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170071AB RID: 29099
		// (get) Token: 0x06015473 RID: 87155
		[DispId(-2147417057)]
		public virtual extern string uniqueID
		{
			[TypeLibFunc(64)]
			[DispId(-2147417057)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170071AC RID: 29100
		// (get) Token: 0x06015474 RID: 87156
		[DispId(-2147417066)]
		public virtual extern int nodeType
		{
			[DispId(-2147417066)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170071AD RID: 29101
		// (get) Token: 0x06015475 RID: 87157
		[DispId(-2147417065)]
		public virtual extern IHTMLDOMNode parentNode
		{
			[DispId(-2147417065)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x06015476 RID: 87158
		[DispId(-2147417064)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool hasChildNodes();

		// Token: 0x170071AE RID: 29102
		// (get) Token: 0x06015477 RID: 87159
		[DispId(-2147417063)]
		public virtual extern object childNodes
		{
			[DispId(-2147417063)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x170071AF RID: 29103
		// (get) Token: 0x06015478 RID: 87160
		[DispId(-2147417062)]
		public virtual extern object attributes
		{
			[DispId(-2147417062)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x06015479 RID: 87161
		[DispId(-2147417061)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode insertBefore([MarshalAs(28)] [In] IHTMLDOMNode newChild, [MarshalAs(27)] [In] [Optional] object refChild);

		// Token: 0x0601547A RID: 87162
		[DispId(-2147417060)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode removeChild([MarshalAs(28)] [In] IHTMLDOMNode oldChild);

		// Token: 0x0601547B RID: 87163
		[DispId(-2147417059)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode replaceChild([MarshalAs(28)] [In] IHTMLDOMNode newChild, [MarshalAs(28)] [In] IHTMLDOMNode oldChild);

		// Token: 0x0601547C RID: 87164
		[DispId(-2147417051)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode cloneNode([In] bool fDeep);

		// Token: 0x0601547D RID: 87165
		[DispId(-2147417046)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode removeNode([In] bool fDeep = false);

		// Token: 0x0601547E RID: 87166
		[DispId(-2147417044)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode swapNode([MarshalAs(28)] [In] IHTMLDOMNode otherNode);

		// Token: 0x0601547F RID: 87167
		[DispId(-2147417045)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode replaceNode([MarshalAs(28)] [In] IHTMLDOMNode replacement);

		// Token: 0x06015480 RID: 87168
		[DispId(-2147417039)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode appendChild([MarshalAs(28)] [In] IHTMLDOMNode newChild);

		// Token: 0x170071B0 RID: 29104
		// (get) Token: 0x06015481 RID: 87169
		[DispId(-2147417038)]
		public virtual extern string nodeName
		{
			[DispId(-2147417038)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170071B1 RID: 29105
		// (get) Token: 0x06015483 RID: 87171
		// (set) Token: 0x06015482 RID: 87170
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

		// Token: 0x170071B2 RID: 29106
		// (get) Token: 0x06015484 RID: 87172
		[DispId(-2147417036)]
		public virtual extern IHTMLDOMNode firstChild
		{
			[DispId(-2147417036)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170071B3 RID: 29107
		// (get) Token: 0x06015485 RID: 87173
		[DispId(-2147417035)]
		public virtual extern IHTMLDOMNode lastChild
		{
			[DispId(-2147417035)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170071B4 RID: 29108
		// (get) Token: 0x06015486 RID: 87174
		[DispId(-2147417034)]
		public virtual extern IHTMLDOMNode previousSibling
		{
			[DispId(-2147417034)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170071B5 RID: 29109
		// (get) Token: 0x06015487 RID: 87175
		[DispId(-2147417033)]
		public virtual extern IHTMLDOMNode nextSibling
		{
			[DispId(-2147417033)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170071B6 RID: 29110
		// (get) Token: 0x06015488 RID: 87176
		[DispId(-2147416999)]
		public virtual extern object ownerDocument
		{
			[DispId(-2147416999)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x170071B7 RID: 29111
		// (get) Token: 0x0601548A RID: 87178
		// (set) Token: 0x06015489 RID: 87177
		[DispId(1000)]
		public virtual extern string rows
		{
			[DispId(1000)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(1000)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x170071B8 RID: 29112
		// (get) Token: 0x0601548C RID: 87180
		// (set) Token: 0x0601548B RID: 87179
		[DispId(1001)]
		public virtual extern string cols
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

		// Token: 0x170071B9 RID: 29113
		// (get) Token: 0x0601548E RID: 87182
		// (set) Token: 0x0601548D RID: 87181
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

		// Token: 0x170071BA RID: 29114
		// (get) Token: 0x06015490 RID: 87184
		// (set) Token: 0x0601548F RID: 87183
		[DispId(1003)]
		public virtual extern object borderColor
		{
			[DispId(1003)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(1003)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170071BB RID: 29115
		// (get) Token: 0x06015492 RID: 87186
		// (set) Token: 0x06015491 RID: 87185
		[DispId(1004)]
		public virtual extern string frameBorder
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

		// Token: 0x170071BC RID: 29116
		// (get) Token: 0x06015494 RID: 87188
		// (set) Token: 0x06015493 RID: 87187
		[DispId(1005)]
		public virtual extern object frameSpacing
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

		// Token: 0x170071BD RID: 29117
		// (get) Token: 0x06015496 RID: 87190
		// (set) Token: 0x06015495 RID: 87189
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

		// Token: 0x170071BE RID: 29118
		// (get) Token: 0x06015498 RID: 87192
		// (set) Token: 0x06015497 RID: 87191
		[DispId(-2147412080)]
		public virtual extern object onload
		{
			[DispId(-2147412080)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412080)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170071BF RID: 29119
		// (get) Token: 0x0601549A RID: 87194
		// (set) Token: 0x06015499 RID: 87193
		[DispId(-2147412079)]
		public virtual extern object onunload
		{
			[TypeLibFunc(20)]
			[DispId(-2147412079)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412079)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170071C0 RID: 29120
		// (get) Token: 0x0601549C RID: 87196
		// (set) Token: 0x0601549B RID: 87195
		[DispId(-2147412073)]
		public virtual extern object onbeforeunload
		{
			[DispId(-2147412073)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412073)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170071C1 RID: 29121
		// (get) Token: 0x0601549E RID: 87198
		// (set) Token: 0x0601549D RID: 87197
		[DispId(-2147412046)]
		public virtual extern object onbeforeprint
		{
			[TypeLibFunc(20)]
			[DispId(-2147412046)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412046)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170071C2 RID: 29122
		// (get) Token: 0x060154A0 RID: 87200
		// (set) Token: 0x0601549F RID: 87199
		[DispId(-2147412045)]
		public virtual extern object onafterprint
		{
			[TypeLibFunc(20)]
			[DispId(-2147412045)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412045)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x060154A1 RID: 87201
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement_setAttribute([MarshalAs(19)] [In] string strAttributeName, [MarshalAs(27)] [In] object AttributeValue, [In] int lFlags = 1);

		// Token: 0x060154A2 RID: 87202
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		public virtual extern object IHTMLElement_getAttribute([MarshalAs(19)] [In] string strAttributeName, [In] int lFlags = 0);

		// Token: 0x060154A3 RID: 87203
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement_removeAttribute([MarshalAs(19)] [In] string strAttributeName, [In] int lFlags = 1);

		// Token: 0x170071C3 RID: 29123
		// (get) Token: 0x060154A5 RID: 87205
		// (set) Token: 0x060154A4 RID: 87204
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

		// Token: 0x170071C4 RID: 29124
		// (get) Token: 0x060154A7 RID: 87207
		// (set) Token: 0x060154A6 RID: 87206
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

		// Token: 0x170071C5 RID: 29125
		// (get) Token: 0x060154A8 RID: 87208
		public virtual extern string IHTMLElement_tagName
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170071C6 RID: 29126
		// (get) Token: 0x060154A9 RID: 87209
		public virtual extern IHTMLElement IHTMLElement_parentElement
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170071C7 RID: 29127
		// (get) Token: 0x060154AA RID: 87210
		public virtual extern IHTMLStyle IHTMLElement_style
		{
			[TypeLibFunc(1024)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170071C8 RID: 29128
		// (get) Token: 0x060154AC RID: 87212
		// (set) Token: 0x060154AB RID: 87211
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

		// Token: 0x170071C9 RID: 29129
		// (get) Token: 0x060154AE RID: 87214
		// (set) Token: 0x060154AD RID: 87213
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

		// Token: 0x170071CA RID: 29130
		// (get) Token: 0x060154B0 RID: 87216
		// (set) Token: 0x060154AF RID: 87215
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

		// Token: 0x170071CB RID: 29131
		// (get) Token: 0x060154B2 RID: 87218
		// (set) Token: 0x060154B1 RID: 87217
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

		// Token: 0x170071CC RID: 29132
		// (get) Token: 0x060154B4 RID: 87220
		// (set) Token: 0x060154B3 RID: 87219
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

		// Token: 0x170071CD RID: 29133
		// (get) Token: 0x060154B6 RID: 87222
		// (set) Token: 0x060154B5 RID: 87221
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

		// Token: 0x170071CE RID: 29134
		// (get) Token: 0x060154B8 RID: 87224
		// (set) Token: 0x060154B7 RID: 87223
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

		// Token: 0x170071CF RID: 29135
		// (get) Token: 0x060154BA RID: 87226
		// (set) Token: 0x060154B9 RID: 87225
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

		// Token: 0x170071D0 RID: 29136
		// (get) Token: 0x060154BC RID: 87228
		// (set) Token: 0x060154BB RID: 87227
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

		// Token: 0x170071D1 RID: 29137
		// (get) Token: 0x060154BE RID: 87230
		// (set) Token: 0x060154BD RID: 87229
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

		// Token: 0x170071D2 RID: 29138
		// (get) Token: 0x060154C0 RID: 87232
		// (set) Token: 0x060154BF RID: 87231
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

		// Token: 0x170071D3 RID: 29139
		// (get) Token: 0x060154C1 RID: 87233
		public virtual extern object IHTMLElement_document
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x170071D4 RID: 29140
		// (get) Token: 0x060154C3 RID: 87235
		// (set) Token: 0x060154C2 RID: 87234
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

		// Token: 0x170071D5 RID: 29141
		// (get) Token: 0x060154C5 RID: 87237
		// (set) Token: 0x060154C4 RID: 87236
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

		// Token: 0x170071D6 RID: 29142
		// (get) Token: 0x060154C7 RID: 87239
		// (set) Token: 0x060154C6 RID: 87238
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

		// Token: 0x060154C8 RID: 87240
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement_scrollIntoView([MarshalAs(27)] [In] [Optional] object varargStart);

		// Token: 0x060154C9 RID: 87241
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement_contains([MarshalAs(28)] [In] IHTMLElement pChild);

		// Token: 0x170071D7 RID: 29143
		// (get) Token: 0x060154CA RID: 87242
		public virtual extern int IHTMLElement_sourceIndex
		{
			[TypeLibFunc(4)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170071D8 RID: 29144
		// (get) Token: 0x060154CB RID: 87243
		public virtual extern object IHTMLElement_recordNumber
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x170071D9 RID: 29145
		// (get) Token: 0x060154CD RID: 87245
		// (set) Token: 0x060154CC RID: 87244
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

		// Token: 0x170071DA RID: 29146
		// (get) Token: 0x060154CE RID: 87246
		public virtual extern int IHTMLElement_offsetLeft
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170071DB RID: 29147
		// (get) Token: 0x060154CF RID: 87247
		public virtual extern int IHTMLElement_offsetTop
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170071DC RID: 29148
		// (get) Token: 0x060154D0 RID: 87248
		public virtual extern int IHTMLElement_offsetWidth
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170071DD RID: 29149
		// (get) Token: 0x060154D1 RID: 87249
		public virtual extern int IHTMLElement_offsetHeight
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170071DE RID: 29150
		// (get) Token: 0x060154D2 RID: 87250
		public virtual extern IHTMLElement IHTMLElement_offsetParent
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170071DF RID: 29151
		// (get) Token: 0x060154D4 RID: 87252
		// (set) Token: 0x060154D3 RID: 87251
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

		// Token: 0x170071E0 RID: 29152
		// (get) Token: 0x060154D6 RID: 87254
		// (set) Token: 0x060154D5 RID: 87253
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

		// Token: 0x170071E1 RID: 29153
		// (get) Token: 0x060154D8 RID: 87256
		// (set) Token: 0x060154D7 RID: 87255
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

		// Token: 0x170071E2 RID: 29154
		// (get) Token: 0x060154DA RID: 87258
		// (set) Token: 0x060154D9 RID: 87257
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

		// Token: 0x060154DB RID: 87259
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement_insertAdjacentHTML([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string html);

		// Token: 0x060154DC RID: 87260
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement_insertAdjacentText([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string text);

		// Token: 0x170071E3 RID: 29155
		// (get) Token: 0x060154DD RID: 87261
		public virtual extern IHTMLElement IHTMLElement_parentTextEdit
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170071E4 RID: 29156
		// (get) Token: 0x060154DE RID: 87262
		public virtual extern bool IHTMLElement_isTextEdit
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x060154DF RID: 87263
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement_click();

		// Token: 0x170071E5 RID: 29157
		// (get) Token: 0x060154E0 RID: 87264
		public virtual extern IHTMLFiltersCollection IHTMLElement_filters
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170071E6 RID: 29158
		// (get) Token: 0x060154E2 RID: 87266
		// (set) Token: 0x060154E1 RID: 87265
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

		// Token: 0x060154E3 RID: 87267
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string IHTMLElement_toString();

		// Token: 0x170071E7 RID: 29159
		// (get) Token: 0x060154E5 RID: 87269
		// (set) Token: 0x060154E4 RID: 87268
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

		// Token: 0x170071E8 RID: 29160
		// (get) Token: 0x060154E7 RID: 87271
		// (set) Token: 0x060154E6 RID: 87270
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

		// Token: 0x170071E9 RID: 29161
		// (get) Token: 0x060154E9 RID: 87273
		// (set) Token: 0x060154E8 RID: 87272
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

		// Token: 0x170071EA RID: 29162
		// (get) Token: 0x060154EB RID: 87275
		// (set) Token: 0x060154EA RID: 87274
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

		// Token: 0x170071EB RID: 29163
		// (get) Token: 0x060154ED RID: 87277
		// (set) Token: 0x060154EC RID: 87276
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

		// Token: 0x170071EC RID: 29164
		// (get) Token: 0x060154EF RID: 87279
		// (set) Token: 0x060154EE RID: 87278
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

		// Token: 0x170071ED RID: 29165
		// (get) Token: 0x060154F1 RID: 87281
		// (set) Token: 0x060154F0 RID: 87280
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

		// Token: 0x170071EE RID: 29166
		// (get) Token: 0x060154F3 RID: 87283
		// (set) Token: 0x060154F2 RID: 87282
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

		// Token: 0x170071EF RID: 29167
		// (get) Token: 0x060154F5 RID: 87285
		// (set) Token: 0x060154F4 RID: 87284
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

		// Token: 0x170071F0 RID: 29168
		// (get) Token: 0x060154F6 RID: 87286
		public virtual extern object IHTMLElement_children
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x170071F1 RID: 29169
		// (get) Token: 0x060154F7 RID: 87287
		public virtual extern object IHTMLElement_all
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x170071F2 RID: 29170
		// (get) Token: 0x060154F8 RID: 87288
		public virtual extern string IHTMLElement2_scopeName
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x060154F9 RID: 87289
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_setCapture([In] bool containerCapture = true);

		// Token: 0x060154FA RID: 87290
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_releaseCapture();

		// Token: 0x170071F3 RID: 29171
		// (get) Token: 0x060154FC RID: 87292
		// (set) Token: 0x060154FB RID: 87291
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

		// Token: 0x060154FD RID: 87293
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string IHTMLElement2_componentFromPoint([In] int x, [In] int y);

		// Token: 0x060154FE RID: 87294
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_doScroll([MarshalAs(27)] [In] [Optional] object component);

		// Token: 0x170071F4 RID: 29172
		// (get) Token: 0x06015500 RID: 87296
		// (set) Token: 0x060154FF RID: 87295
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

		// Token: 0x170071F5 RID: 29173
		// (get) Token: 0x06015502 RID: 87298
		// (set) Token: 0x06015501 RID: 87297
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

		// Token: 0x170071F6 RID: 29174
		// (get) Token: 0x06015504 RID: 87300
		// (set) Token: 0x06015503 RID: 87299
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

		// Token: 0x170071F7 RID: 29175
		// (get) Token: 0x06015506 RID: 87302
		// (set) Token: 0x06015505 RID: 87301
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

		// Token: 0x170071F8 RID: 29176
		// (get) Token: 0x06015508 RID: 87304
		// (set) Token: 0x06015507 RID: 87303
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

		// Token: 0x170071F9 RID: 29177
		// (get) Token: 0x0601550A RID: 87306
		// (set) Token: 0x06015509 RID: 87305
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

		// Token: 0x170071FA RID: 29178
		// (get) Token: 0x0601550C RID: 87308
		// (set) Token: 0x0601550B RID: 87307
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

		// Token: 0x170071FB RID: 29179
		// (get) Token: 0x0601550E RID: 87310
		// (set) Token: 0x0601550D RID: 87309
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

		// Token: 0x170071FC RID: 29180
		// (get) Token: 0x06015510 RID: 87312
		// (set) Token: 0x0601550F RID: 87311
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

		// Token: 0x170071FD RID: 29181
		// (get) Token: 0x06015512 RID: 87314
		// (set) Token: 0x06015511 RID: 87313
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

		// Token: 0x170071FE RID: 29182
		// (get) Token: 0x06015514 RID: 87316
		// (set) Token: 0x06015513 RID: 87315
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

		// Token: 0x170071FF RID: 29183
		// (get) Token: 0x06015516 RID: 87318
		// (set) Token: 0x06015515 RID: 87317
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

		// Token: 0x17007200 RID: 29184
		// (get) Token: 0x06015518 RID: 87320
		// (set) Token: 0x06015517 RID: 87319
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

		// Token: 0x17007201 RID: 29185
		// (get) Token: 0x06015519 RID: 87321
		public virtual extern IHTMLCurrentStyle IHTMLElement2_currentStyle
		{
			[TypeLibFunc(1024)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17007202 RID: 29186
		// (get) Token: 0x0601551B RID: 87323
		// (set) Token: 0x0601551A RID: 87322
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

		// Token: 0x0601551C RID: 87324
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLRectCollection IHTMLElement2_getClientRects();

		// Token: 0x0601551D RID: 87325
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLRect IHTMLElement2_getBoundingClientRect();

		// Token: 0x0601551E RID: 87326
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_setExpression([MarshalAs(19)] [In] string propname, [MarshalAs(19)] [In] string expression, [MarshalAs(19)] [In] string language = "");

		// Token: 0x0601551F RID: 87327
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		public virtual extern object IHTMLElement2_getExpression([MarshalAs(19)] [In] string propname);

		// Token: 0x06015520 RID: 87328
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement2_removeExpression([MarshalAs(19)] [In] string propname);

		// Token: 0x17007203 RID: 29187
		// (get) Token: 0x06015522 RID: 87330
		// (set) Token: 0x06015521 RID: 87329
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

		// Token: 0x06015523 RID: 87331
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_focus();

		// Token: 0x17007204 RID: 29188
		// (get) Token: 0x06015525 RID: 87333
		// (set) Token: 0x06015524 RID: 87332
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

		// Token: 0x17007205 RID: 29189
		// (get) Token: 0x06015527 RID: 87335
		// (set) Token: 0x06015526 RID: 87334
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

		// Token: 0x17007206 RID: 29190
		// (get) Token: 0x06015529 RID: 87337
		// (set) Token: 0x06015528 RID: 87336
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

		// Token: 0x17007207 RID: 29191
		// (get) Token: 0x0601552B RID: 87339
		// (set) Token: 0x0601552A RID: 87338
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

		// Token: 0x0601552C RID: 87340
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_blur();

		// Token: 0x0601552D RID: 87341
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_addFilter([MarshalAs(25)] [In] object pUnk);

		// Token: 0x0601552E RID: 87342
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_removeFilter([MarshalAs(25)] [In] object pUnk);

		// Token: 0x17007208 RID: 29192
		// (get) Token: 0x0601552F RID: 87343
		public virtual extern int IHTMLElement2_clientHeight
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17007209 RID: 29193
		// (get) Token: 0x06015530 RID: 87344
		public virtual extern int IHTMLElement2_clientWidth
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700720A RID: 29194
		// (get) Token: 0x06015531 RID: 87345
		public virtual extern int IHTMLElement2_clientTop
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700720B RID: 29195
		// (get) Token: 0x06015532 RID: 87346
		public virtual extern int IHTMLElement2_clientLeft
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x06015533 RID: 87347
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement2_attachEvent([MarshalAs(19)] [In] string @event, [MarshalAs(26)] [In] object pdisp);

		// Token: 0x06015534 RID: 87348
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_detachEvent([MarshalAs(19)] [In] string @event, [MarshalAs(26)] [In] object pdisp);

		// Token: 0x1700720C RID: 29196
		// (get) Token: 0x06015535 RID: 87349
		public virtual extern object IHTMLElement2_readyState
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x1700720D RID: 29197
		// (get) Token: 0x06015537 RID: 87351
		// (set) Token: 0x06015536 RID: 87350
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

		// Token: 0x1700720E RID: 29198
		// (get) Token: 0x06015539 RID: 87353
		// (set) Token: 0x06015538 RID: 87352
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

		// Token: 0x1700720F RID: 29199
		// (get) Token: 0x0601553B RID: 87355
		// (set) Token: 0x0601553A RID: 87354
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

		// Token: 0x17007210 RID: 29200
		// (get) Token: 0x0601553D RID: 87357
		// (set) Token: 0x0601553C RID: 87356
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

		// Token: 0x17007211 RID: 29201
		// (get) Token: 0x0601553F RID: 87359
		// (set) Token: 0x0601553E RID: 87358
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

		// Token: 0x06015540 RID: 87360
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(26)]
		public virtual extern object IHTMLElement2_createControlRange();

		// Token: 0x17007212 RID: 29202
		// (get) Token: 0x06015541 RID: 87361
		public virtual extern int IHTMLElement2_scrollHeight
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17007213 RID: 29203
		// (get) Token: 0x06015542 RID: 87362
		public virtual extern int IHTMLElement2_scrollWidth
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17007214 RID: 29204
		// (get) Token: 0x06015544 RID: 87364
		// (set) Token: 0x06015543 RID: 87363
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

		// Token: 0x17007215 RID: 29205
		// (get) Token: 0x06015546 RID: 87366
		// (set) Token: 0x06015545 RID: 87365
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

		// Token: 0x06015547 RID: 87367
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_clearAttributes();

		// Token: 0x06015548 RID: 87368
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void mergeAttributes([MarshalAs(28)] [In] IHTMLElement mergeThis);

		// Token: 0x17007216 RID: 29206
		// (get) Token: 0x0601554A RID: 87370
		// (set) Token: 0x06015549 RID: 87369
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

		// Token: 0x0601554B RID: 87371
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElement IHTMLElement2_insertAdjacentElement([MarshalAs(19)] [In] string where, [MarshalAs(28)] [In] IHTMLElement insertedElement);

		// Token: 0x0601554C RID: 87372
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElement IHTMLElement2_applyElement([MarshalAs(28)] [In] IHTMLElement apply, [MarshalAs(19)] [In] string where);

		// Token: 0x0601554D RID: 87373
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string IHTMLElement2_getAdjacentText([MarshalAs(19)] [In] string where);

		// Token: 0x0601554E RID: 87374
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string IHTMLElement2_replaceAdjacentText([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string newText);

		// Token: 0x17007217 RID: 29207
		// (get) Token: 0x0601554F RID: 87375
		public virtual extern bool IHTMLElement2_canHaveChildren
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x06015550 RID: 87376
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern int IHTMLElement2_addBehavior([MarshalAs(19)] [In] string bstrUrl, [MarshalAs(27)] [In] [Optional] ref object pvarFactory);

		// Token: 0x06015551 RID: 87377
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement2_removeBehavior([In] int cookie);

		// Token: 0x17007218 RID: 29208
		// (get) Token: 0x06015552 RID: 87378
		public virtual extern IHTMLStyle IHTMLElement2_runtimeStyle
		{
			[TypeLibFunc(1024)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17007219 RID: 29209
		// (get) Token: 0x06015553 RID: 87379
		public virtual extern object IHTMLElement2_behaviorUrns
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x1700721A RID: 29210
		// (get) Token: 0x06015555 RID: 87381
		// (set) Token: 0x06015554 RID: 87380
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

		// Token: 0x1700721B RID: 29211
		// (get) Token: 0x06015557 RID: 87383
		// (set) Token: 0x06015556 RID: 87382
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

		// Token: 0x1700721C RID: 29212
		// (get) Token: 0x06015558 RID: 87384
		public virtual extern int IHTMLElement2_readyStateValue
		{
			[TypeLibFunc(65)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x06015559 RID: 87385
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElementCollection IHTMLElement2_getElementsByTagName([MarshalAs(19)] [In] string v);

		// Token: 0x0601555A RID: 87386
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void mergeAttributes([MarshalAs(28)] [In] IHTMLElement mergeThis, [MarshalAs(27)] [In] [Optional] ref object pvarFlags);

		// Token: 0x1700721D RID: 29213
		// (get) Token: 0x0601555B RID: 87387
		public virtual extern bool IHTMLElement3_isMultiLine
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700721E RID: 29214
		// (get) Token: 0x0601555C RID: 87388
		public virtual extern bool IHTMLElement3_canHaveHTML
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700721F RID: 29215
		// (get) Token: 0x0601555E RID: 87390
		// (set) Token: 0x0601555D RID: 87389
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

		// Token: 0x17007220 RID: 29216
		// (get) Token: 0x06015560 RID: 87392
		// (set) Token: 0x0601555F RID: 87391
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

		// Token: 0x17007221 RID: 29217
		// (get) Token: 0x06015562 RID: 87394
		// (set) Token: 0x06015561 RID: 87393
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

		// Token: 0x17007222 RID: 29218
		// (get) Token: 0x06015564 RID: 87396
		// (set) Token: 0x06015563 RID: 87395
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

		// Token: 0x06015565 RID: 87397
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement3_setActive();

		// Token: 0x17007223 RID: 29219
		// (get) Token: 0x06015567 RID: 87399
		// (set) Token: 0x06015566 RID: 87398
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

		// Token: 0x17007224 RID: 29220
		// (get) Token: 0x06015568 RID: 87400
		public virtual extern bool IHTMLElement3_isContentEditable
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17007225 RID: 29221
		// (get) Token: 0x0601556A RID: 87402
		// (set) Token: 0x06015569 RID: 87401
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

		// Token: 0x17007226 RID: 29222
		// (get) Token: 0x0601556C RID: 87404
		// (set) Token: 0x0601556B RID: 87403
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

		// Token: 0x17007227 RID: 29223
		// (get) Token: 0x0601556D RID: 87405
		public virtual extern bool IHTMLElement3_isDisabled
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17007228 RID: 29224
		// (get) Token: 0x0601556F RID: 87407
		// (set) Token: 0x0601556E RID: 87406
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

		// Token: 0x17007229 RID: 29225
		// (get) Token: 0x06015571 RID: 87409
		// (set) Token: 0x06015570 RID: 87408
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

		// Token: 0x06015572 RID: 87410
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement3_FireEvent([MarshalAs(19)] [In] string bstrEventName, [MarshalAs(27)] [In] [Optional] ref object pvarEventObject);

		// Token: 0x1700722A RID: 29226
		// (get) Token: 0x06015574 RID: 87412
		// (set) Token: 0x06015573 RID: 87411
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

		// Token: 0x1700722B RID: 29227
		// (get) Token: 0x06015576 RID: 87414
		// (set) Token: 0x06015575 RID: 87413
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

		// Token: 0x1700722C RID: 29228
		// (get) Token: 0x06015578 RID: 87416
		// (set) Token: 0x06015577 RID: 87415
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

		// Token: 0x1700722D RID: 29229
		// (get) Token: 0x0601557A RID: 87418
		// (set) Token: 0x06015579 RID: 87417
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

		// Token: 0x1700722E RID: 29230
		// (get) Token: 0x0601557C RID: 87420
		// (set) Token: 0x0601557B RID: 87419
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

		// Token: 0x1700722F RID: 29231
		// (get) Token: 0x0601557E RID: 87422
		// (set) Token: 0x0601557D RID: 87421
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

		// Token: 0x17007230 RID: 29232
		// (get) Token: 0x06015580 RID: 87424
		// (set) Token: 0x0601557F RID: 87423
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

		// Token: 0x17007231 RID: 29233
		// (get) Token: 0x06015582 RID: 87426
		// (set) Token: 0x06015581 RID: 87425
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

		// Token: 0x06015583 RID: 87427
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement3_dragDrop();

		// Token: 0x17007232 RID: 29234
		// (get) Token: 0x06015584 RID: 87428
		public virtual extern int IHTMLElement3_glyphMode
		{
			[TypeLibFunc(1089)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17007233 RID: 29235
		// (get) Token: 0x06015586 RID: 87430
		// (set) Token: 0x06015585 RID: 87429
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

		// Token: 0x06015587 RID: 87431
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement4_normalize();

		// Token: 0x06015588 RID: 87432
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute IHTMLElement4_getAttributeNode([MarshalAs(19)] [In] string bstrName);

		// Token: 0x06015589 RID: 87433
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute IHTMLElement4_setAttributeNode([MarshalAs(28)] [In] IHTMLDOMAttribute pattr);

		// Token: 0x0601558A RID: 87434
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute IHTMLElement4_removeAttributeNode([MarshalAs(28)] [In] IHTMLDOMAttribute pattr);

		// Token: 0x17007234 RID: 29236
		// (get) Token: 0x0601558C RID: 87436
		// (set) Token: 0x0601558B RID: 87435
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

		// Token: 0x17007235 RID: 29237
		// (get) Token: 0x0601558E RID: 87438
		// (set) Token: 0x0601558D RID: 87437
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

		// Token: 0x17007236 RID: 29238
		// (get) Token: 0x06015590 RID: 87440
		// (set) Token: 0x0601558F RID: 87439
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

		// Token: 0x17007237 RID: 29239
		// (get) Token: 0x06015591 RID: 87441
		public virtual extern int IHTMLUniqueName_uniqueNumber
		{
			[TypeLibFunc(64)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17007238 RID: 29240
		// (get) Token: 0x06015592 RID: 87442
		public virtual extern string IHTMLUniqueName_uniqueID
		{
			[TypeLibFunc(64)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17007239 RID: 29241
		// (get) Token: 0x06015593 RID: 87443
		public virtual extern int IHTMLDOMNode_nodeType
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700723A RID: 29242
		// (get) Token: 0x06015594 RID: 87444
		public virtual extern IHTMLDOMNode IHTMLDOMNode_parentNode
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x06015595 RID: 87445
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLDOMNode_hasChildNodes();

		// Token: 0x1700723B RID: 29243
		// (get) Token: 0x06015596 RID: 87446
		public virtual extern object IHTMLDOMNode_childNodes
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x1700723C RID: 29244
		// (get) Token: 0x06015597 RID: 87447
		public virtual extern object IHTMLDOMNode_attributes
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x06015598 RID: 87448
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_insertBefore([MarshalAs(28)] [In] IHTMLDOMNode newChild, [MarshalAs(27)] [In] [Optional] object refChild);

		// Token: 0x06015599 RID: 87449
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_removeChild([MarshalAs(28)] [In] IHTMLDOMNode oldChild);

		// Token: 0x0601559A RID: 87450
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_replaceChild([MarshalAs(28)] [In] IHTMLDOMNode newChild, [MarshalAs(28)] [In] IHTMLDOMNode oldChild);

		// Token: 0x0601559B RID: 87451
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_cloneNode([In] bool fDeep);

		// Token: 0x0601559C RID: 87452
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_removeNode([In] bool fDeep = false);

		// Token: 0x0601559D RID: 87453
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_swapNode([MarshalAs(28)] [In] IHTMLDOMNode otherNode);

		// Token: 0x0601559E RID: 87454
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_replaceNode([MarshalAs(28)] [In] IHTMLDOMNode replacement);

		// Token: 0x0601559F RID: 87455
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_appendChild([MarshalAs(28)] [In] IHTMLDOMNode newChild);

		// Token: 0x1700723D RID: 29245
		// (get) Token: 0x060155A0 RID: 87456
		public virtual extern string IHTMLDOMNode_nodeName
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x1700723E RID: 29246
		// (get) Token: 0x060155A2 RID: 87458
		// (set) Token: 0x060155A1 RID: 87457
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

		// Token: 0x1700723F RID: 29247
		// (get) Token: 0x060155A3 RID: 87459
		public virtual extern IHTMLDOMNode IHTMLDOMNode_firstChild
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17007240 RID: 29248
		// (get) Token: 0x060155A4 RID: 87460
		public virtual extern IHTMLDOMNode IHTMLDOMNode_lastChild
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17007241 RID: 29249
		// (get) Token: 0x060155A5 RID: 87461
		public virtual extern IHTMLDOMNode IHTMLDOMNode_previousSibling
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17007242 RID: 29250
		// (get) Token: 0x060155A6 RID: 87462
		public virtual extern IHTMLDOMNode IHTMLDOMNode_nextSibling
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17007243 RID: 29251
		// (get) Token: 0x060155A7 RID: 87463
		public virtual extern object IHTMLDOMNode2_ownerDocument
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17007244 RID: 29252
		// (get) Token: 0x060155A9 RID: 87465
		// (set) Token: 0x060155A8 RID: 87464
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

		// Token: 0x060155AA RID: 87466
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLControlElement_focus();

		// Token: 0x17007245 RID: 29253
		// (get) Token: 0x060155AC RID: 87468
		// (set) Token: 0x060155AB RID: 87467
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

		// Token: 0x17007246 RID: 29254
		// (get) Token: 0x060155AE RID: 87470
		// (set) Token: 0x060155AD RID: 87469
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

		// Token: 0x17007247 RID: 29255
		// (get) Token: 0x060155B0 RID: 87472
		// (set) Token: 0x060155AF RID: 87471
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

		// Token: 0x17007248 RID: 29256
		// (get) Token: 0x060155B2 RID: 87474
		// (set) Token: 0x060155B1 RID: 87473
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

		// Token: 0x060155B3 RID: 87475
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLControlElement_blur();

		// Token: 0x060155B4 RID: 87476
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLControlElement_addFilter([MarshalAs(25)] [In] object pUnk);

		// Token: 0x060155B5 RID: 87477
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLControlElement_removeFilter([MarshalAs(25)] [In] object pUnk);

		// Token: 0x17007249 RID: 29257
		// (get) Token: 0x060155B6 RID: 87478
		public virtual extern int IHTMLControlElement_clientHeight
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700724A RID: 29258
		// (get) Token: 0x060155B7 RID: 87479
		public virtual extern int IHTMLControlElement_clientWidth
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700724B RID: 29259
		// (get) Token: 0x060155B8 RID: 87480
		public virtual extern int IHTMLControlElement_clientTop
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700724C RID: 29260
		// (get) Token: 0x060155B9 RID: 87481
		public virtual extern int IHTMLControlElement_clientLeft
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700724D RID: 29261
		// (get) Token: 0x060155BB RID: 87483
		// (set) Token: 0x060155BA RID: 87482
		public virtual extern string IHTMLFrameSetElement_rows
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

		// Token: 0x1700724E RID: 29262
		// (get) Token: 0x060155BD RID: 87485
		// (set) Token: 0x060155BC RID: 87484
		public virtual extern string IHTMLFrameSetElement_cols
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

		// Token: 0x1700724F RID: 29263
		// (get) Token: 0x060155BF RID: 87487
		// (set) Token: 0x060155BE RID: 87486
		public virtual extern object IHTMLFrameSetElement_border
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17007250 RID: 29264
		// (get) Token: 0x060155C1 RID: 87489
		// (set) Token: 0x060155C0 RID: 87488
		public virtual extern object IHTMLFrameSetElement_borderColor
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17007251 RID: 29265
		// (get) Token: 0x060155C3 RID: 87491
		// (set) Token: 0x060155C2 RID: 87490
		public virtual extern string IHTMLFrameSetElement_frameBorder
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17007252 RID: 29266
		// (get) Token: 0x060155C5 RID: 87493
		// (set) Token: 0x060155C4 RID: 87492
		public virtual extern object IHTMLFrameSetElement_frameSpacing
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17007253 RID: 29267
		// (get) Token: 0x060155C7 RID: 87495
		// (set) Token: 0x060155C6 RID: 87494
		public virtual extern string IHTMLFrameSetElement_name
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

		// Token: 0x17007254 RID: 29268
		// (get) Token: 0x060155C9 RID: 87497
		// (set) Token: 0x060155C8 RID: 87496
		public virtual extern object IHTMLFrameSetElement_onload
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17007255 RID: 29269
		// (get) Token: 0x060155CB RID: 87499
		// (set) Token: 0x060155CA RID: 87498
		public virtual extern object IHTMLFrameSetElement_onunload
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17007256 RID: 29270
		// (get) Token: 0x060155CD RID: 87501
		// (set) Token: 0x060155CC RID: 87500
		public virtual extern object IHTMLFrameSetElement_onbeforeunload
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17007257 RID: 29271
		// (get) Token: 0x060155CF RID: 87503
		// (set) Token: 0x060155CE RID: 87502
		public virtual extern object IHTMLFrameSetElement2_onbeforeprint
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17007258 RID: 29272
		// (get) Token: 0x060155D1 RID: 87505
		// (set) Token: 0x060155D0 RID: 87504
		public virtual extern object IHTMLFrameSetElement2_onafterprint
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x1400290A RID: 10506
		// (add) Token: 0x060155D2 RID: 87506
		// (remove) Token: 0x060155D3 RID: 87507
		public virtual extern event HTMLControlElementEvents_onhelpEventHandler HTMLControlElementEvents_Event_onhelp;

		// Token: 0x1400290B RID: 10507
		// (add) Token: 0x060155D4 RID: 87508
		// (remove) Token: 0x060155D5 RID: 87509
		public virtual extern event HTMLControlElementEvents_onclickEventHandler HTMLControlElementEvents_Event_onclick;

		// Token: 0x1400290C RID: 10508
		// (add) Token: 0x060155D6 RID: 87510
		// (remove) Token: 0x060155D7 RID: 87511
		public virtual extern event HTMLControlElementEvents_ondblclickEventHandler HTMLControlElementEvents_Event_ondblclick;

		// Token: 0x1400290D RID: 10509
		// (add) Token: 0x060155D8 RID: 87512
		// (remove) Token: 0x060155D9 RID: 87513
		public virtual extern event HTMLControlElementEvents_onkeypressEventHandler HTMLControlElementEvents_Event_onkeypress;

		// Token: 0x1400290E RID: 10510
		// (add) Token: 0x060155DA RID: 87514
		// (remove) Token: 0x060155DB RID: 87515
		public virtual extern event HTMLControlElementEvents_onkeydownEventHandler HTMLControlElementEvents_Event_onkeydown;

		// Token: 0x1400290F RID: 10511
		// (add) Token: 0x060155DC RID: 87516
		// (remove) Token: 0x060155DD RID: 87517
		public virtual extern event HTMLControlElementEvents_onkeyupEventHandler HTMLControlElementEvents_Event_onkeyup;

		// Token: 0x14002910 RID: 10512
		// (add) Token: 0x060155DE RID: 87518
		// (remove) Token: 0x060155DF RID: 87519
		public virtual extern event HTMLControlElementEvents_onmouseoutEventHandler HTMLControlElementEvents_Event_onmouseout;

		// Token: 0x14002911 RID: 10513
		// (add) Token: 0x060155E0 RID: 87520
		// (remove) Token: 0x060155E1 RID: 87521
		public virtual extern event HTMLControlElementEvents_onmouseoverEventHandler HTMLControlElementEvents_Event_onmouseover;

		// Token: 0x14002912 RID: 10514
		// (add) Token: 0x060155E2 RID: 87522
		// (remove) Token: 0x060155E3 RID: 87523
		public virtual extern event HTMLControlElementEvents_onmousemoveEventHandler HTMLControlElementEvents_Event_onmousemove;

		// Token: 0x14002913 RID: 10515
		// (add) Token: 0x060155E4 RID: 87524
		// (remove) Token: 0x060155E5 RID: 87525
		public virtual extern event HTMLControlElementEvents_onmousedownEventHandler HTMLControlElementEvents_Event_onmousedown;

		// Token: 0x14002914 RID: 10516
		// (add) Token: 0x060155E6 RID: 87526
		// (remove) Token: 0x060155E7 RID: 87527
		public virtual extern event HTMLControlElementEvents_onmouseupEventHandler HTMLControlElementEvents_Event_onmouseup;

		// Token: 0x14002915 RID: 10517
		// (add) Token: 0x060155E8 RID: 87528
		// (remove) Token: 0x060155E9 RID: 87529
		public virtual extern event HTMLControlElementEvents_onselectstartEventHandler HTMLControlElementEvents_Event_onselectstart;

		// Token: 0x14002916 RID: 10518
		// (add) Token: 0x060155EA RID: 87530
		// (remove) Token: 0x060155EB RID: 87531
		public virtual extern event HTMLControlElementEvents_onfilterchangeEventHandler HTMLControlElementEvents_Event_onfilterchange;

		// Token: 0x14002917 RID: 10519
		// (add) Token: 0x060155EC RID: 87532
		// (remove) Token: 0x060155ED RID: 87533
		public virtual extern event HTMLControlElementEvents_ondragstartEventHandler HTMLControlElementEvents_Event_ondragstart;

		// Token: 0x14002918 RID: 10520
		// (add) Token: 0x060155EE RID: 87534
		// (remove) Token: 0x060155EF RID: 87535
		public virtual extern event HTMLControlElementEvents_onbeforeupdateEventHandler HTMLControlElementEvents_Event_onbeforeupdate;

		// Token: 0x14002919 RID: 10521
		// (add) Token: 0x060155F0 RID: 87536
		// (remove) Token: 0x060155F1 RID: 87537
		public virtual extern event HTMLControlElementEvents_onafterupdateEventHandler HTMLControlElementEvents_Event_onafterupdate;

		// Token: 0x1400291A RID: 10522
		// (add) Token: 0x060155F2 RID: 87538
		// (remove) Token: 0x060155F3 RID: 87539
		public virtual extern event HTMLControlElementEvents_onerrorupdateEventHandler HTMLControlElementEvents_Event_onerrorupdate;

		// Token: 0x1400291B RID: 10523
		// (add) Token: 0x060155F4 RID: 87540
		// (remove) Token: 0x060155F5 RID: 87541
		public virtual extern event HTMLControlElementEvents_onrowexitEventHandler HTMLControlElementEvents_Event_onrowexit;

		// Token: 0x1400291C RID: 10524
		// (add) Token: 0x060155F6 RID: 87542
		// (remove) Token: 0x060155F7 RID: 87543
		public virtual extern event HTMLControlElementEvents_onrowenterEventHandler HTMLControlElementEvents_Event_onrowenter;

		// Token: 0x1400291D RID: 10525
		// (add) Token: 0x060155F8 RID: 87544
		// (remove) Token: 0x060155F9 RID: 87545
		public virtual extern event HTMLControlElementEvents_ondatasetchangedEventHandler HTMLControlElementEvents_Event_ondatasetchanged;

		// Token: 0x1400291E RID: 10526
		// (add) Token: 0x060155FA RID: 87546
		// (remove) Token: 0x060155FB RID: 87547
		public virtual extern event HTMLControlElementEvents_ondataavailableEventHandler HTMLControlElementEvents_Event_ondataavailable;

		// Token: 0x1400291F RID: 10527
		// (add) Token: 0x060155FC RID: 87548
		// (remove) Token: 0x060155FD RID: 87549
		public virtual extern event HTMLControlElementEvents_ondatasetcompleteEventHandler HTMLControlElementEvents_Event_ondatasetcomplete;

		// Token: 0x14002920 RID: 10528
		// (add) Token: 0x060155FE RID: 87550
		// (remove) Token: 0x060155FF RID: 87551
		public virtual extern event HTMLControlElementEvents_onlosecaptureEventHandler HTMLControlElementEvents_Event_onlosecapture;

		// Token: 0x14002921 RID: 10529
		// (add) Token: 0x06015600 RID: 87552
		// (remove) Token: 0x06015601 RID: 87553
		public virtual extern event HTMLControlElementEvents_onpropertychangeEventHandler HTMLControlElementEvents_Event_onpropertychange;

		// Token: 0x14002922 RID: 10530
		// (add) Token: 0x06015602 RID: 87554
		// (remove) Token: 0x06015603 RID: 87555
		public virtual extern event HTMLControlElementEvents_onscrollEventHandler HTMLControlElementEvents_Event_onscroll;

		// Token: 0x14002923 RID: 10531
		// (add) Token: 0x06015604 RID: 87556
		// (remove) Token: 0x06015605 RID: 87557
		public virtual extern event HTMLControlElementEvents_onfocusEventHandler HTMLControlElementEvents_Event_onfocus;

		// Token: 0x14002924 RID: 10532
		// (add) Token: 0x06015606 RID: 87558
		// (remove) Token: 0x06015607 RID: 87559
		public virtual extern event HTMLControlElementEvents_onblurEventHandler HTMLControlElementEvents_Event_onblur;

		// Token: 0x14002925 RID: 10533
		// (add) Token: 0x06015608 RID: 87560
		// (remove) Token: 0x06015609 RID: 87561
		public virtual extern event HTMLControlElementEvents_onresizeEventHandler HTMLControlElementEvents_Event_onresize;

		// Token: 0x14002926 RID: 10534
		// (add) Token: 0x0601560A RID: 87562
		// (remove) Token: 0x0601560B RID: 87563
		public virtual extern event HTMLControlElementEvents_ondragEventHandler HTMLControlElementEvents_Event_ondrag;

		// Token: 0x14002927 RID: 10535
		// (add) Token: 0x0601560C RID: 87564
		// (remove) Token: 0x0601560D RID: 87565
		public virtual extern event HTMLControlElementEvents_ondragendEventHandler HTMLControlElementEvents_Event_ondragend;

		// Token: 0x14002928 RID: 10536
		// (add) Token: 0x0601560E RID: 87566
		// (remove) Token: 0x0601560F RID: 87567
		public virtual extern event HTMLControlElementEvents_ondragenterEventHandler HTMLControlElementEvents_Event_ondragenter;

		// Token: 0x14002929 RID: 10537
		// (add) Token: 0x06015610 RID: 87568
		// (remove) Token: 0x06015611 RID: 87569
		public virtual extern event HTMLControlElementEvents_ondragoverEventHandler HTMLControlElementEvents_Event_ondragover;

		// Token: 0x1400292A RID: 10538
		// (add) Token: 0x06015612 RID: 87570
		// (remove) Token: 0x06015613 RID: 87571
		public virtual extern event HTMLControlElementEvents_ondragleaveEventHandler HTMLControlElementEvents_Event_ondragleave;

		// Token: 0x1400292B RID: 10539
		// (add) Token: 0x06015614 RID: 87572
		// (remove) Token: 0x06015615 RID: 87573
		public virtual extern event HTMLControlElementEvents_ondropEventHandler HTMLControlElementEvents_Event_ondrop;

		// Token: 0x1400292C RID: 10540
		// (add) Token: 0x06015616 RID: 87574
		// (remove) Token: 0x06015617 RID: 87575
		public virtual extern event HTMLControlElementEvents_onbeforecutEventHandler HTMLControlElementEvents_Event_onbeforecut;

		// Token: 0x1400292D RID: 10541
		// (add) Token: 0x06015618 RID: 87576
		// (remove) Token: 0x06015619 RID: 87577
		public virtual extern event HTMLControlElementEvents_oncutEventHandler HTMLControlElementEvents_Event_oncut;

		// Token: 0x1400292E RID: 10542
		// (add) Token: 0x0601561A RID: 87578
		// (remove) Token: 0x0601561B RID: 87579
		public virtual extern event HTMLControlElementEvents_onbeforecopyEventHandler HTMLControlElementEvents_Event_onbeforecopy;

		// Token: 0x1400292F RID: 10543
		// (add) Token: 0x0601561C RID: 87580
		// (remove) Token: 0x0601561D RID: 87581
		public virtual extern event HTMLControlElementEvents_oncopyEventHandler HTMLControlElementEvents_Event_oncopy;

		// Token: 0x14002930 RID: 10544
		// (add) Token: 0x0601561E RID: 87582
		// (remove) Token: 0x0601561F RID: 87583
		public virtual extern event HTMLControlElementEvents_onbeforepasteEventHandler HTMLControlElementEvents_Event_onbeforepaste;

		// Token: 0x14002931 RID: 10545
		// (add) Token: 0x06015620 RID: 87584
		// (remove) Token: 0x06015621 RID: 87585
		public virtual extern event HTMLControlElementEvents_onpasteEventHandler HTMLControlElementEvents_Event_onpaste;

		// Token: 0x14002932 RID: 10546
		// (add) Token: 0x06015622 RID: 87586
		// (remove) Token: 0x06015623 RID: 87587
		public virtual extern event HTMLControlElementEvents_oncontextmenuEventHandler HTMLControlElementEvents_Event_oncontextmenu;

		// Token: 0x14002933 RID: 10547
		// (add) Token: 0x06015624 RID: 87588
		// (remove) Token: 0x06015625 RID: 87589
		public virtual extern event HTMLControlElementEvents_onrowsdeleteEventHandler HTMLControlElementEvents_Event_onrowsdelete;

		// Token: 0x14002934 RID: 10548
		// (add) Token: 0x06015626 RID: 87590
		// (remove) Token: 0x06015627 RID: 87591
		public virtual extern event HTMLControlElementEvents_onrowsinsertedEventHandler HTMLControlElementEvents_Event_onrowsinserted;

		// Token: 0x14002935 RID: 10549
		// (add) Token: 0x06015628 RID: 87592
		// (remove) Token: 0x06015629 RID: 87593
		public virtual extern event HTMLControlElementEvents_oncellchangeEventHandler HTMLControlElementEvents_Event_oncellchange;

		// Token: 0x14002936 RID: 10550
		// (add) Token: 0x0601562A RID: 87594
		// (remove) Token: 0x0601562B RID: 87595
		public virtual extern event HTMLControlElementEvents_onreadystatechangeEventHandler HTMLControlElementEvents_Event_onreadystatechange;

		// Token: 0x14002937 RID: 10551
		// (add) Token: 0x0601562C RID: 87596
		// (remove) Token: 0x0601562D RID: 87597
		public virtual extern event HTMLControlElementEvents_onbeforeeditfocusEventHandler HTMLControlElementEvents_Event_onbeforeeditfocus;

		// Token: 0x14002938 RID: 10552
		// (add) Token: 0x0601562E RID: 87598
		// (remove) Token: 0x0601562F RID: 87599
		public virtual extern event HTMLControlElementEvents_onlayoutcompleteEventHandler HTMLControlElementEvents_Event_onlayoutcomplete;

		// Token: 0x14002939 RID: 10553
		// (add) Token: 0x06015630 RID: 87600
		// (remove) Token: 0x06015631 RID: 87601
		public virtual extern event HTMLControlElementEvents_onpageEventHandler HTMLControlElementEvents_Event_onpage;

		// Token: 0x1400293A RID: 10554
		// (add) Token: 0x06015632 RID: 87602
		// (remove) Token: 0x06015633 RID: 87603
		public virtual extern event HTMLControlElementEvents_onbeforedeactivateEventHandler HTMLControlElementEvents_Event_onbeforedeactivate;

		// Token: 0x1400293B RID: 10555
		// (add) Token: 0x06015634 RID: 87604
		// (remove) Token: 0x06015635 RID: 87605
		public virtual extern event HTMLControlElementEvents_onbeforeactivateEventHandler HTMLControlElementEvents_Event_onbeforeactivate;

		// Token: 0x1400293C RID: 10556
		// (add) Token: 0x06015636 RID: 87606
		// (remove) Token: 0x06015637 RID: 87607
		public virtual extern event HTMLControlElementEvents_onmoveEventHandler HTMLControlElementEvents_Event_onmove;

		// Token: 0x1400293D RID: 10557
		// (add) Token: 0x06015638 RID: 87608
		// (remove) Token: 0x06015639 RID: 87609
		public virtual extern event HTMLControlElementEvents_oncontrolselectEventHandler HTMLControlElementEvents_Event_oncontrolselect;

		// Token: 0x1400293E RID: 10558
		// (add) Token: 0x0601563A RID: 87610
		// (remove) Token: 0x0601563B RID: 87611
		public virtual extern event HTMLControlElementEvents_onmovestartEventHandler HTMLControlElementEvents_Event_onmovestart;

		// Token: 0x1400293F RID: 10559
		// (add) Token: 0x0601563C RID: 87612
		// (remove) Token: 0x0601563D RID: 87613
		public virtual extern event HTMLControlElementEvents_onmoveendEventHandler HTMLControlElementEvents_Event_onmoveend;

		// Token: 0x14002940 RID: 10560
		// (add) Token: 0x0601563E RID: 87614
		// (remove) Token: 0x0601563F RID: 87615
		public virtual extern event HTMLControlElementEvents_onresizestartEventHandler HTMLControlElementEvents_Event_onresizestart;

		// Token: 0x14002941 RID: 10561
		// (add) Token: 0x06015640 RID: 87616
		// (remove) Token: 0x06015641 RID: 87617
		public virtual extern event HTMLControlElementEvents_onresizeendEventHandler HTMLControlElementEvents_Event_onresizeend;

		// Token: 0x14002942 RID: 10562
		// (add) Token: 0x06015642 RID: 87618
		// (remove) Token: 0x06015643 RID: 87619
		public virtual extern event HTMLControlElementEvents_onmouseenterEventHandler HTMLControlElementEvents_Event_onmouseenter;

		// Token: 0x14002943 RID: 10563
		// (add) Token: 0x06015644 RID: 87620
		// (remove) Token: 0x06015645 RID: 87621
		public virtual extern event HTMLControlElementEvents_onmouseleaveEventHandler HTMLControlElementEvents_Event_onmouseleave;

		// Token: 0x14002944 RID: 10564
		// (add) Token: 0x06015646 RID: 87622
		// (remove) Token: 0x06015647 RID: 87623
		public virtual extern event HTMLControlElementEvents_onmousewheelEventHandler HTMLControlElementEvents_Event_onmousewheel;

		// Token: 0x14002945 RID: 10565
		// (add) Token: 0x06015648 RID: 87624
		// (remove) Token: 0x06015649 RID: 87625
		public virtual extern event HTMLControlElementEvents_onactivateEventHandler HTMLControlElementEvents_Event_onactivate;

		// Token: 0x14002946 RID: 10566
		// (add) Token: 0x0601564A RID: 87626
		// (remove) Token: 0x0601564B RID: 87627
		public virtual extern event HTMLControlElementEvents_ondeactivateEventHandler HTMLControlElementEvents_Event_ondeactivate;

		// Token: 0x14002947 RID: 10567
		// (add) Token: 0x0601564C RID: 87628
		// (remove) Token: 0x0601564D RID: 87629
		public virtual extern event HTMLControlElementEvents_onfocusinEventHandler HTMLControlElementEvents_Event_onfocusin;

		// Token: 0x14002948 RID: 10568
		// (add) Token: 0x0601564E RID: 87630
		// (remove) Token: 0x0601564F RID: 87631
		public virtual extern event HTMLControlElementEvents_onfocusoutEventHandler HTMLControlElementEvents_Event_onfocusout;

		// Token: 0x14002949 RID: 10569
		// (add) Token: 0x06015650 RID: 87632
		// (remove) Token: 0x06015651 RID: 87633
		public virtual extern event HTMLControlElementEvents2_onhelpEventHandler HTMLControlElementEvents2_Event_onhelp;

		// Token: 0x1400294A RID: 10570
		// (add) Token: 0x06015652 RID: 87634
		// (remove) Token: 0x06015653 RID: 87635
		public virtual extern event HTMLControlElementEvents2_onclickEventHandler HTMLControlElementEvents2_Event_onclick;

		// Token: 0x1400294B RID: 10571
		// (add) Token: 0x06015654 RID: 87636
		// (remove) Token: 0x06015655 RID: 87637
		public virtual extern event HTMLControlElementEvents2_ondblclickEventHandler HTMLControlElementEvents2_Event_ondblclick;

		// Token: 0x1400294C RID: 10572
		// (add) Token: 0x06015656 RID: 87638
		// (remove) Token: 0x06015657 RID: 87639
		public virtual extern event HTMLControlElementEvents2_onkeypressEventHandler HTMLControlElementEvents2_Event_onkeypress;

		// Token: 0x1400294D RID: 10573
		// (add) Token: 0x06015658 RID: 87640
		// (remove) Token: 0x06015659 RID: 87641
		public virtual extern event HTMLControlElementEvents2_onkeydownEventHandler HTMLControlElementEvents2_Event_onkeydown;

		// Token: 0x1400294E RID: 10574
		// (add) Token: 0x0601565A RID: 87642
		// (remove) Token: 0x0601565B RID: 87643
		public virtual extern event HTMLControlElementEvents2_onkeyupEventHandler HTMLControlElementEvents2_Event_onkeyup;

		// Token: 0x1400294F RID: 10575
		// (add) Token: 0x0601565C RID: 87644
		// (remove) Token: 0x0601565D RID: 87645
		public virtual extern event HTMLControlElementEvents2_onmouseoutEventHandler HTMLControlElementEvents2_Event_onmouseout;

		// Token: 0x14002950 RID: 10576
		// (add) Token: 0x0601565E RID: 87646
		// (remove) Token: 0x0601565F RID: 87647
		public virtual extern event HTMLControlElementEvents2_onmouseoverEventHandler HTMLControlElementEvents2_Event_onmouseover;

		// Token: 0x14002951 RID: 10577
		// (add) Token: 0x06015660 RID: 87648
		// (remove) Token: 0x06015661 RID: 87649
		public virtual extern event HTMLControlElementEvents2_onmousemoveEventHandler HTMLControlElementEvents2_Event_onmousemove;

		// Token: 0x14002952 RID: 10578
		// (add) Token: 0x06015662 RID: 87650
		// (remove) Token: 0x06015663 RID: 87651
		public virtual extern event HTMLControlElementEvents2_onmousedownEventHandler HTMLControlElementEvents2_Event_onmousedown;

		// Token: 0x14002953 RID: 10579
		// (add) Token: 0x06015664 RID: 87652
		// (remove) Token: 0x06015665 RID: 87653
		public virtual extern event HTMLControlElementEvents2_onmouseupEventHandler HTMLControlElementEvents2_Event_onmouseup;

		// Token: 0x14002954 RID: 10580
		// (add) Token: 0x06015666 RID: 87654
		// (remove) Token: 0x06015667 RID: 87655
		public virtual extern event HTMLControlElementEvents2_onselectstartEventHandler HTMLControlElementEvents2_Event_onselectstart;

		// Token: 0x14002955 RID: 10581
		// (add) Token: 0x06015668 RID: 87656
		// (remove) Token: 0x06015669 RID: 87657
		public virtual extern event HTMLControlElementEvents2_onfilterchangeEventHandler HTMLControlElementEvents2_Event_onfilterchange;

		// Token: 0x14002956 RID: 10582
		// (add) Token: 0x0601566A RID: 87658
		// (remove) Token: 0x0601566B RID: 87659
		public virtual extern event HTMLControlElementEvents2_ondragstartEventHandler HTMLControlElementEvents2_Event_ondragstart;

		// Token: 0x14002957 RID: 10583
		// (add) Token: 0x0601566C RID: 87660
		// (remove) Token: 0x0601566D RID: 87661
		public virtual extern event HTMLControlElementEvents2_onbeforeupdateEventHandler HTMLControlElementEvents2_Event_onbeforeupdate;

		// Token: 0x14002958 RID: 10584
		// (add) Token: 0x0601566E RID: 87662
		// (remove) Token: 0x0601566F RID: 87663
		public virtual extern event HTMLControlElementEvents2_onafterupdateEventHandler HTMLControlElementEvents2_Event_onafterupdate;

		// Token: 0x14002959 RID: 10585
		// (add) Token: 0x06015670 RID: 87664
		// (remove) Token: 0x06015671 RID: 87665
		public virtual extern event HTMLControlElementEvents2_onerrorupdateEventHandler HTMLControlElementEvents2_Event_onerrorupdate;

		// Token: 0x1400295A RID: 10586
		// (add) Token: 0x06015672 RID: 87666
		// (remove) Token: 0x06015673 RID: 87667
		public virtual extern event HTMLControlElementEvents2_onrowexitEventHandler HTMLControlElementEvents2_Event_onrowexit;

		// Token: 0x1400295B RID: 10587
		// (add) Token: 0x06015674 RID: 87668
		// (remove) Token: 0x06015675 RID: 87669
		public virtual extern event HTMLControlElementEvents2_onrowenterEventHandler HTMLControlElementEvents2_Event_onrowenter;

		// Token: 0x1400295C RID: 10588
		// (add) Token: 0x06015676 RID: 87670
		// (remove) Token: 0x06015677 RID: 87671
		public virtual extern event HTMLControlElementEvents2_ondatasetchangedEventHandler HTMLControlElementEvents2_Event_ondatasetchanged;

		// Token: 0x1400295D RID: 10589
		// (add) Token: 0x06015678 RID: 87672
		// (remove) Token: 0x06015679 RID: 87673
		public virtual extern event HTMLControlElementEvents2_ondataavailableEventHandler HTMLControlElementEvents2_Event_ondataavailable;

		// Token: 0x1400295E RID: 10590
		// (add) Token: 0x0601567A RID: 87674
		// (remove) Token: 0x0601567B RID: 87675
		public virtual extern event HTMLControlElementEvents2_ondatasetcompleteEventHandler HTMLControlElementEvents2_Event_ondatasetcomplete;

		// Token: 0x1400295F RID: 10591
		// (add) Token: 0x0601567C RID: 87676
		// (remove) Token: 0x0601567D RID: 87677
		public virtual extern event HTMLControlElementEvents2_onlosecaptureEventHandler HTMLControlElementEvents2_Event_onlosecapture;

		// Token: 0x14002960 RID: 10592
		// (add) Token: 0x0601567E RID: 87678
		// (remove) Token: 0x0601567F RID: 87679
		public virtual extern event HTMLControlElementEvents2_onpropertychangeEventHandler HTMLControlElementEvents2_Event_onpropertychange;

		// Token: 0x14002961 RID: 10593
		// (add) Token: 0x06015680 RID: 87680
		// (remove) Token: 0x06015681 RID: 87681
		public virtual extern event HTMLControlElementEvents2_onscrollEventHandler HTMLControlElementEvents2_Event_onscroll;

		// Token: 0x14002962 RID: 10594
		// (add) Token: 0x06015682 RID: 87682
		// (remove) Token: 0x06015683 RID: 87683
		public virtual extern event HTMLControlElementEvents2_onfocusEventHandler HTMLControlElementEvents2_Event_onfocus;

		// Token: 0x14002963 RID: 10595
		// (add) Token: 0x06015684 RID: 87684
		// (remove) Token: 0x06015685 RID: 87685
		public virtual extern event HTMLControlElementEvents2_onblurEventHandler HTMLControlElementEvents2_Event_onblur;

		// Token: 0x14002964 RID: 10596
		// (add) Token: 0x06015686 RID: 87686
		// (remove) Token: 0x06015687 RID: 87687
		public virtual extern event HTMLControlElementEvents2_onresizeEventHandler HTMLControlElementEvents2_Event_onresize;

		// Token: 0x14002965 RID: 10597
		// (add) Token: 0x06015688 RID: 87688
		// (remove) Token: 0x06015689 RID: 87689
		public virtual extern event HTMLControlElementEvents2_ondragEventHandler HTMLControlElementEvents2_Event_ondrag;

		// Token: 0x14002966 RID: 10598
		// (add) Token: 0x0601568A RID: 87690
		// (remove) Token: 0x0601568B RID: 87691
		public virtual extern event HTMLControlElementEvents2_ondragendEventHandler HTMLControlElementEvents2_Event_ondragend;

		// Token: 0x14002967 RID: 10599
		// (add) Token: 0x0601568C RID: 87692
		// (remove) Token: 0x0601568D RID: 87693
		public virtual extern event HTMLControlElementEvents2_ondragenterEventHandler HTMLControlElementEvents2_Event_ondragenter;

		// Token: 0x14002968 RID: 10600
		// (add) Token: 0x0601568E RID: 87694
		// (remove) Token: 0x0601568F RID: 87695
		public virtual extern event HTMLControlElementEvents2_ondragoverEventHandler HTMLControlElementEvents2_Event_ondragover;

		// Token: 0x14002969 RID: 10601
		// (add) Token: 0x06015690 RID: 87696
		// (remove) Token: 0x06015691 RID: 87697
		public virtual extern event HTMLControlElementEvents2_ondragleaveEventHandler HTMLControlElementEvents2_Event_ondragleave;

		// Token: 0x1400296A RID: 10602
		// (add) Token: 0x06015692 RID: 87698
		// (remove) Token: 0x06015693 RID: 87699
		public virtual extern event HTMLControlElementEvents2_ondropEventHandler HTMLControlElementEvents2_Event_ondrop;

		// Token: 0x1400296B RID: 10603
		// (add) Token: 0x06015694 RID: 87700
		// (remove) Token: 0x06015695 RID: 87701
		public virtual extern event HTMLControlElementEvents2_onbeforecutEventHandler HTMLControlElementEvents2_Event_onbeforecut;

		// Token: 0x1400296C RID: 10604
		// (add) Token: 0x06015696 RID: 87702
		// (remove) Token: 0x06015697 RID: 87703
		public virtual extern event HTMLControlElementEvents2_oncutEventHandler HTMLControlElementEvents2_Event_oncut;

		// Token: 0x1400296D RID: 10605
		// (add) Token: 0x06015698 RID: 87704
		// (remove) Token: 0x06015699 RID: 87705
		public virtual extern event HTMLControlElementEvents2_onbeforecopyEventHandler HTMLControlElementEvents2_Event_onbeforecopy;

		// Token: 0x1400296E RID: 10606
		// (add) Token: 0x0601569A RID: 87706
		// (remove) Token: 0x0601569B RID: 87707
		public virtual extern event HTMLControlElementEvents2_oncopyEventHandler HTMLControlElementEvents2_Event_oncopy;

		// Token: 0x1400296F RID: 10607
		// (add) Token: 0x0601569C RID: 87708
		// (remove) Token: 0x0601569D RID: 87709
		public virtual extern event HTMLControlElementEvents2_onbeforepasteEventHandler HTMLControlElementEvents2_Event_onbeforepaste;

		// Token: 0x14002970 RID: 10608
		// (add) Token: 0x0601569E RID: 87710
		// (remove) Token: 0x0601569F RID: 87711
		public virtual extern event HTMLControlElementEvents2_onpasteEventHandler HTMLControlElementEvents2_Event_onpaste;

		// Token: 0x14002971 RID: 10609
		// (add) Token: 0x060156A0 RID: 87712
		// (remove) Token: 0x060156A1 RID: 87713
		public virtual extern event HTMLControlElementEvents2_oncontextmenuEventHandler HTMLControlElementEvents2_Event_oncontextmenu;

		// Token: 0x14002972 RID: 10610
		// (add) Token: 0x060156A2 RID: 87714
		// (remove) Token: 0x060156A3 RID: 87715
		public virtual extern event HTMLControlElementEvents2_onrowsdeleteEventHandler HTMLControlElementEvents2_Event_onrowsdelete;

		// Token: 0x14002973 RID: 10611
		// (add) Token: 0x060156A4 RID: 87716
		// (remove) Token: 0x060156A5 RID: 87717
		public virtual extern event HTMLControlElementEvents2_onrowsinsertedEventHandler HTMLControlElementEvents2_Event_onrowsinserted;

		// Token: 0x14002974 RID: 10612
		// (add) Token: 0x060156A6 RID: 87718
		// (remove) Token: 0x060156A7 RID: 87719
		public virtual extern event HTMLControlElementEvents2_oncellchangeEventHandler HTMLControlElementEvents2_Event_oncellchange;

		// Token: 0x14002975 RID: 10613
		// (add) Token: 0x060156A8 RID: 87720
		// (remove) Token: 0x060156A9 RID: 87721
		public virtual extern event HTMLControlElementEvents2_onreadystatechangeEventHandler HTMLControlElementEvents2_Event_onreadystatechange;

		// Token: 0x14002976 RID: 10614
		// (add) Token: 0x060156AA RID: 87722
		// (remove) Token: 0x060156AB RID: 87723
		public virtual extern event HTMLControlElementEvents2_onlayoutcompleteEventHandler HTMLControlElementEvents2_Event_onlayoutcomplete;

		// Token: 0x14002977 RID: 10615
		// (add) Token: 0x060156AC RID: 87724
		// (remove) Token: 0x060156AD RID: 87725
		public virtual extern event HTMLControlElementEvents2_onpageEventHandler HTMLControlElementEvents2_Event_onpage;

		// Token: 0x14002978 RID: 10616
		// (add) Token: 0x060156AE RID: 87726
		// (remove) Token: 0x060156AF RID: 87727
		public virtual extern event HTMLControlElementEvents2_onmouseenterEventHandler HTMLControlElementEvents2_Event_onmouseenter;

		// Token: 0x14002979 RID: 10617
		// (add) Token: 0x060156B0 RID: 87728
		// (remove) Token: 0x060156B1 RID: 87729
		public virtual extern event HTMLControlElementEvents2_onmouseleaveEventHandler HTMLControlElementEvents2_Event_onmouseleave;

		// Token: 0x1400297A RID: 10618
		// (add) Token: 0x060156B2 RID: 87730
		// (remove) Token: 0x060156B3 RID: 87731
		public virtual extern event HTMLControlElementEvents2_onactivateEventHandler HTMLControlElementEvents2_Event_onactivate;

		// Token: 0x1400297B RID: 10619
		// (add) Token: 0x060156B4 RID: 87732
		// (remove) Token: 0x060156B5 RID: 87733
		public virtual extern event HTMLControlElementEvents2_ondeactivateEventHandler HTMLControlElementEvents2_Event_ondeactivate;

		// Token: 0x1400297C RID: 10620
		// (add) Token: 0x060156B6 RID: 87734
		// (remove) Token: 0x060156B7 RID: 87735
		public virtual extern event HTMLControlElementEvents2_onbeforedeactivateEventHandler HTMLControlElementEvents2_Event_onbeforedeactivate;

		// Token: 0x1400297D RID: 10621
		// (add) Token: 0x060156B8 RID: 87736
		// (remove) Token: 0x060156B9 RID: 87737
		public virtual extern event HTMLControlElementEvents2_onbeforeactivateEventHandler HTMLControlElementEvents2_Event_onbeforeactivate;

		// Token: 0x1400297E RID: 10622
		// (add) Token: 0x060156BA RID: 87738
		// (remove) Token: 0x060156BB RID: 87739
		public virtual extern event HTMLControlElementEvents2_onfocusinEventHandler HTMLControlElementEvents2_Event_onfocusin;

		// Token: 0x1400297F RID: 10623
		// (add) Token: 0x060156BC RID: 87740
		// (remove) Token: 0x060156BD RID: 87741
		public virtual extern event HTMLControlElementEvents2_onfocusoutEventHandler HTMLControlElementEvents2_Event_onfocusout;

		// Token: 0x14002980 RID: 10624
		// (add) Token: 0x060156BE RID: 87742
		// (remove) Token: 0x060156BF RID: 87743
		public virtual extern event HTMLControlElementEvents2_onmoveEventHandler HTMLControlElementEvents2_Event_onmove;

		// Token: 0x14002981 RID: 10625
		// (add) Token: 0x060156C0 RID: 87744
		// (remove) Token: 0x060156C1 RID: 87745
		public virtual extern event HTMLControlElementEvents2_oncontrolselectEventHandler HTMLControlElementEvents2_Event_oncontrolselect;

		// Token: 0x14002982 RID: 10626
		// (add) Token: 0x060156C2 RID: 87746
		// (remove) Token: 0x060156C3 RID: 87747
		public virtual extern event HTMLControlElementEvents2_onmovestartEventHandler HTMLControlElementEvents2_Event_onmovestart;

		// Token: 0x14002983 RID: 10627
		// (add) Token: 0x060156C4 RID: 87748
		// (remove) Token: 0x060156C5 RID: 87749
		public virtual extern event HTMLControlElementEvents2_onmoveendEventHandler HTMLControlElementEvents2_Event_onmoveend;

		// Token: 0x14002984 RID: 10628
		// (add) Token: 0x060156C6 RID: 87750
		// (remove) Token: 0x060156C7 RID: 87751
		public virtual extern event HTMLControlElementEvents2_onresizestartEventHandler HTMLControlElementEvents2_Event_onresizestart;

		// Token: 0x14002985 RID: 10629
		// (add) Token: 0x060156C8 RID: 87752
		// (remove) Token: 0x060156C9 RID: 87753
		public virtual extern event HTMLControlElementEvents2_onresizeendEventHandler HTMLControlElementEvents2_Event_onresizeend;

		// Token: 0x14002986 RID: 10630
		// (add) Token: 0x060156CA RID: 87754
		// (remove) Token: 0x060156CB RID: 87755
		public virtual extern event HTMLControlElementEvents2_onmousewheelEventHandler HTMLControlElementEvents2_Event_onmousewheel;
	}
}
