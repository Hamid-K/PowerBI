using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x020004B9 RID: 1209
	[Guid("3050F27E-98B5-11CF-BB82-00AA00BDCE0B")]
	[ClassInterface(0)]
	[TypeLibType(2)]
	[ComSourceInterfaces("mshtml.HTMLElementEvents\0mshtml.HTMLTextContainerEvents\0mshtml.HTMLElementEvents2\0mshtml.HTMLTextContainerEvents2\0\0")]
	[ComImport]
	public class HTMLDivElementClass : DispHTMLDivElement, HTMLDivElement, HTMLElementEvents_Event, IHTMLElement, IHTMLElement2, IHTMLElement3, IHTMLElement4, IHTMLUniqueName, IHTMLDOMNode, IHTMLDOMNode2, IHTMLDatabinding, IHTMLDivElement, IHTMLControlElement, IHTMLTextContainer, HTMLTextContainerEvents_Event, HTMLElementEvents2_Event, HTMLTextContainerEvents2_Event
	{
		// Token: 0x06005DCA RID: 24010
		[MethodImpl(4096, MethodCodeType = 3)]
		public extern HTMLDivElementClass();

		// Token: 0x06005DCB RID: 24011
		[DispId(-2147417611)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void setAttribute([MarshalAs(19)] [In] string strAttributeName, [MarshalAs(27)] [In] object AttributeValue, [In] int lFlags = 1);

		// Token: 0x06005DCC RID: 24012
		[DispId(-2147417610)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		public virtual extern object getAttribute([MarshalAs(19)] [In] string strAttributeName, [In] int lFlags = 0);

		// Token: 0x06005DCD RID: 24013
		[DispId(-2147417609)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool removeAttribute([MarshalAs(19)] [In] string strAttributeName, [In] int lFlags = 1);

		// Token: 0x17001F54 RID: 8020
		// (get) Token: 0x06005DCF RID: 24015
		// (set) Token: 0x06005DCE RID: 24014
		[DispId(-2147417111)]
		public virtual extern string className
		{
			[DispId(-2147417111)]
			[TypeLibFunc(4)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147417111)]
			[TypeLibFunc(4)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17001F55 RID: 8021
		// (get) Token: 0x06005DD1 RID: 24017
		// (set) Token: 0x06005DD0 RID: 24016
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

		// Token: 0x17001F56 RID: 8022
		// (get) Token: 0x06005DD2 RID: 24018
		[DispId(-2147417108)]
		public virtual extern string tagName
		{
			[DispId(-2147417108)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17001F57 RID: 8023
		// (get) Token: 0x06005DD3 RID: 24019
		[DispId(-2147418104)]
		public virtual extern IHTMLElement parentElement
		{
			[DispId(-2147418104)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17001F58 RID: 8024
		// (get) Token: 0x06005DD4 RID: 24020
		[DispId(-2147418038)]
		public virtual extern IHTMLStyle style
		{
			[DispId(-2147418038)]
			[TypeLibFunc(1024)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17001F59 RID: 8025
		// (get) Token: 0x06005DD6 RID: 24022
		// (set) Token: 0x06005DD5 RID: 24021
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

		// Token: 0x17001F5A RID: 8026
		// (get) Token: 0x06005DD8 RID: 24024
		// (set) Token: 0x06005DD7 RID: 24023
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

		// Token: 0x17001F5B RID: 8027
		// (get) Token: 0x06005DDA RID: 24026
		// (set) Token: 0x06005DD9 RID: 24025
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

		// Token: 0x17001F5C RID: 8028
		// (get) Token: 0x06005DDC RID: 24028
		// (set) Token: 0x06005DDB RID: 24027
		[DispId(-2147412107)]
		public virtual extern object onkeydown
		{
			[TypeLibFunc(20)]
			[DispId(-2147412107)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412107)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17001F5D RID: 8029
		// (get) Token: 0x06005DDE RID: 24030
		// (set) Token: 0x06005DDD RID: 24029
		[DispId(-2147412106)]
		public virtual extern object onkeyup
		{
			[DispId(-2147412106)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412106)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17001F5E RID: 8030
		// (get) Token: 0x06005DE0 RID: 24032
		// (set) Token: 0x06005DDF RID: 24031
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

		// Token: 0x17001F5F RID: 8031
		// (get) Token: 0x06005DE2 RID: 24034
		// (set) Token: 0x06005DE1 RID: 24033
		[DispId(-2147412111)]
		public virtual extern object onmouseout
		{
			[TypeLibFunc(20)]
			[DispId(-2147412111)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412111)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17001F60 RID: 8032
		// (get) Token: 0x06005DE4 RID: 24036
		// (set) Token: 0x06005DE3 RID: 24035
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

		// Token: 0x17001F61 RID: 8033
		// (get) Token: 0x06005DE6 RID: 24038
		// (set) Token: 0x06005DE5 RID: 24037
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

		// Token: 0x17001F62 RID: 8034
		// (get) Token: 0x06005DE8 RID: 24040
		// (set) Token: 0x06005DE7 RID: 24039
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

		// Token: 0x17001F63 RID: 8035
		// (get) Token: 0x06005DEA RID: 24042
		// (set) Token: 0x06005DE9 RID: 24041
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

		// Token: 0x17001F64 RID: 8036
		// (get) Token: 0x06005DEB RID: 24043
		[DispId(-2147417094)]
		public virtual extern object document
		{
			[DispId(-2147417094)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17001F65 RID: 8037
		// (get) Token: 0x06005DED RID: 24045
		// (set) Token: 0x06005DEC RID: 24044
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

		// Token: 0x17001F66 RID: 8038
		// (get) Token: 0x06005DEF RID: 24047
		// (set) Token: 0x06005DEE RID: 24046
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

		// Token: 0x17001F67 RID: 8039
		// (get) Token: 0x06005DF1 RID: 24049
		// (set) Token: 0x06005DF0 RID: 24048
		[DispId(-2147412075)]
		public virtual extern object onselectstart
		{
			[TypeLibFunc(20)]
			[DispId(-2147412075)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412075)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x06005DF2 RID: 24050
		[DispId(-2147417093)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void scrollIntoView([MarshalAs(27)] [In] [Optional] object varargStart);

		// Token: 0x06005DF3 RID: 24051
		[DispId(-2147417092)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool contains([MarshalAs(28)] [In] IHTMLElement pChild);

		// Token: 0x17001F68 RID: 8040
		// (get) Token: 0x06005DF4 RID: 24052
		[DispId(-2147417088)]
		public virtual extern int sourceIndex
		{
			[TypeLibFunc(4)]
			[DispId(-2147417088)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17001F69 RID: 8041
		// (get) Token: 0x06005DF5 RID: 24053
		[DispId(-2147417087)]
		public virtual extern object recordNumber
		{
			[DispId(-2147417087)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x17001F6A RID: 8042
		// (get) Token: 0x06005DF7 RID: 24055
		// (set) Token: 0x06005DF6 RID: 24054
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

		// Token: 0x17001F6B RID: 8043
		// (get) Token: 0x06005DF8 RID: 24056
		[DispId(-2147417104)]
		public virtual extern int offsetLeft
		{
			[DispId(-2147417104)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17001F6C RID: 8044
		// (get) Token: 0x06005DF9 RID: 24057
		[DispId(-2147417103)]
		public virtual extern int offsetTop
		{
			[DispId(-2147417103)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17001F6D RID: 8045
		// (get) Token: 0x06005DFA RID: 24058
		[DispId(-2147417102)]
		public virtual extern int offsetWidth
		{
			[DispId(-2147417102)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17001F6E RID: 8046
		// (get) Token: 0x06005DFB RID: 24059
		[DispId(-2147417101)]
		public virtual extern int offsetHeight
		{
			[DispId(-2147417101)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17001F6F RID: 8047
		// (get) Token: 0x06005DFC RID: 24060
		[DispId(-2147417100)]
		public virtual extern IHTMLElement offsetParent
		{
			[DispId(-2147417100)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17001F70 RID: 8048
		// (get) Token: 0x06005DFE RID: 24062
		// (set) Token: 0x06005DFD RID: 24061
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

		// Token: 0x17001F71 RID: 8049
		// (get) Token: 0x06005E00 RID: 24064
		// (set) Token: 0x06005DFF RID: 24063
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

		// Token: 0x17001F72 RID: 8050
		// (get) Token: 0x06005E02 RID: 24066
		// (set) Token: 0x06005E01 RID: 24065
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

		// Token: 0x17001F73 RID: 8051
		// (get) Token: 0x06005E04 RID: 24068
		// (set) Token: 0x06005E03 RID: 24067
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

		// Token: 0x06005E05 RID: 24069
		[DispId(-2147417082)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void insertAdjacentHTML([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string html);

		// Token: 0x06005E06 RID: 24070
		[DispId(-2147417081)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void insertAdjacentText([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string text);

		// Token: 0x17001F74 RID: 8052
		// (get) Token: 0x06005E07 RID: 24071
		[DispId(-2147417080)]
		public virtual extern IHTMLElement parentTextEdit
		{
			[DispId(-2147417080)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17001F75 RID: 8053
		// (get) Token: 0x06005E08 RID: 24072
		[DispId(-2147417078)]
		public virtual extern bool isTextEdit
		{
			[DispId(-2147417078)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x06005E09 RID: 24073
		[DispId(-2147417079)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void click();

		// Token: 0x17001F76 RID: 8054
		// (get) Token: 0x06005E0A RID: 24074
		[DispId(-2147417077)]
		public virtual extern IHTMLFiltersCollection filters
		{
			[DispId(-2147417077)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17001F77 RID: 8055
		// (get) Token: 0x06005E0C RID: 24076
		// (set) Token: 0x06005E0B RID: 24075
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

		// Token: 0x06005E0D RID: 24077
		[DispId(-2147417076)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string toString();

		// Token: 0x17001F78 RID: 8056
		// (get) Token: 0x06005E0F RID: 24079
		// (set) Token: 0x06005E0E RID: 24078
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

		// Token: 0x17001F79 RID: 8057
		// (get) Token: 0x06005E11 RID: 24081
		// (set) Token: 0x06005E10 RID: 24080
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

		// Token: 0x17001F7A RID: 8058
		// (get) Token: 0x06005E13 RID: 24083
		// (set) Token: 0x06005E12 RID: 24082
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

		// Token: 0x17001F7B RID: 8059
		// (get) Token: 0x06005E15 RID: 24085
		// (set) Token: 0x06005E14 RID: 24084
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

		// Token: 0x17001F7C RID: 8060
		// (get) Token: 0x06005E17 RID: 24087
		// (set) Token: 0x06005E16 RID: 24086
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

		// Token: 0x17001F7D RID: 8061
		// (get) Token: 0x06005E19 RID: 24089
		// (set) Token: 0x06005E18 RID: 24088
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

		// Token: 0x17001F7E RID: 8062
		// (get) Token: 0x06005E1B RID: 24091
		// (set) Token: 0x06005E1A RID: 24090
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

		// Token: 0x17001F7F RID: 8063
		// (get) Token: 0x06005E1D RID: 24093
		// (set) Token: 0x06005E1C RID: 24092
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

		// Token: 0x17001F80 RID: 8064
		// (get) Token: 0x06005E1F RID: 24095
		// (set) Token: 0x06005E1E RID: 24094
		[DispId(-2147412069)]
		public virtual extern object onfilterchange
		{
			[TypeLibFunc(20)]
			[DispId(-2147412069)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412069)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17001F81 RID: 8065
		// (get) Token: 0x06005E20 RID: 24096
		[DispId(-2147417075)]
		public virtual extern object children
		{
			[DispId(-2147417075)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17001F82 RID: 8066
		// (get) Token: 0x06005E21 RID: 24097
		[DispId(-2147417074)]
		public virtual extern object all
		{
			[DispId(-2147417074)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17001F83 RID: 8067
		// (get) Token: 0x06005E22 RID: 24098
		[DispId(-2147417073)]
		public virtual extern string scopeName
		{
			[DispId(-2147417073)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x06005E23 RID: 24099
		[DispId(-2147417072)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void setCapture([In] bool containerCapture = true);

		// Token: 0x06005E24 RID: 24100
		[DispId(-2147417071)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void releaseCapture();

		// Token: 0x17001F84 RID: 8068
		// (get) Token: 0x06005E26 RID: 24102
		// (set) Token: 0x06005E25 RID: 24101
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

		// Token: 0x06005E27 RID: 24103
		[DispId(-2147417070)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string componentFromPoint([In] int x, [In] int y);

		// Token: 0x06005E28 RID: 24104
		[DispId(-2147417069)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void doScroll([MarshalAs(27)] [In] [Optional] object component);

		// Token: 0x17001F85 RID: 8069
		// (get) Token: 0x06005E2A RID: 24106
		// (set) Token: 0x06005E29 RID: 24105
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

		// Token: 0x17001F86 RID: 8070
		// (get) Token: 0x06005E2C RID: 24108
		// (set) Token: 0x06005E2B RID: 24107
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

		// Token: 0x17001F87 RID: 8071
		// (get) Token: 0x06005E2E RID: 24110
		// (set) Token: 0x06005E2D RID: 24109
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

		// Token: 0x17001F88 RID: 8072
		// (get) Token: 0x06005E30 RID: 24112
		// (set) Token: 0x06005E2F RID: 24111
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

		// Token: 0x17001F89 RID: 8073
		// (get) Token: 0x06005E32 RID: 24114
		// (set) Token: 0x06005E31 RID: 24113
		[DispId(-2147412060)]
		public virtual extern object ondragover
		{
			[DispId(-2147412060)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412060)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17001F8A RID: 8074
		// (get) Token: 0x06005E34 RID: 24116
		// (set) Token: 0x06005E33 RID: 24115
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

		// Token: 0x17001F8B RID: 8075
		// (get) Token: 0x06005E36 RID: 24118
		// (set) Token: 0x06005E35 RID: 24117
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

		// Token: 0x17001F8C RID: 8076
		// (get) Token: 0x06005E38 RID: 24120
		// (set) Token: 0x06005E37 RID: 24119
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

		// Token: 0x17001F8D RID: 8077
		// (get) Token: 0x06005E3A RID: 24122
		// (set) Token: 0x06005E39 RID: 24121
		[DispId(-2147412057)]
		public virtual extern object oncut
		{
			[TypeLibFunc(20)]
			[DispId(-2147412057)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412057)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17001F8E RID: 8078
		// (get) Token: 0x06005E3C RID: 24124
		// (set) Token: 0x06005E3B RID: 24123
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

		// Token: 0x17001F8F RID: 8079
		// (get) Token: 0x06005E3E RID: 24126
		// (set) Token: 0x06005E3D RID: 24125
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

		// Token: 0x17001F90 RID: 8080
		// (get) Token: 0x06005E40 RID: 24128
		// (set) Token: 0x06005E3F RID: 24127
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

		// Token: 0x17001F91 RID: 8081
		// (get) Token: 0x06005E42 RID: 24130
		// (set) Token: 0x06005E41 RID: 24129
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

		// Token: 0x17001F92 RID: 8082
		// (get) Token: 0x06005E43 RID: 24131
		[DispId(-2147417105)]
		public virtual extern IHTMLCurrentStyle currentStyle
		{
			[DispId(-2147417105)]
			[TypeLibFunc(1024)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17001F93 RID: 8083
		// (get) Token: 0x06005E45 RID: 24133
		// (set) Token: 0x06005E44 RID: 24132
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

		// Token: 0x06005E46 RID: 24134
		[DispId(-2147417068)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLRectCollection getClientRects();

		// Token: 0x06005E47 RID: 24135
		[DispId(-2147417067)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLRect getBoundingClientRect();

		// Token: 0x06005E48 RID: 24136
		[DispId(-2147417608)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void setExpression([MarshalAs(19)] [In] string propname, [MarshalAs(19)] [In] string expression, [MarshalAs(19)] [In] string language = "");

		// Token: 0x06005E49 RID: 24137
		[DispId(-2147417607)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		public virtual extern object getExpression([MarshalAs(19)] [In] string propname);

		// Token: 0x06005E4A RID: 24138
		[DispId(-2147417606)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool removeExpression([MarshalAs(19)] [In] string propname);

		// Token: 0x17001F94 RID: 8084
		// (get) Token: 0x06005E4C RID: 24140
		// (set) Token: 0x06005E4B RID: 24139
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

		// Token: 0x06005E4D RID: 24141
		[DispId(-2147416112)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void focus();

		// Token: 0x17001F95 RID: 8085
		// (get) Token: 0x06005E4F RID: 24143
		// (set) Token: 0x06005E4E RID: 24142
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

		// Token: 0x17001F96 RID: 8086
		// (get) Token: 0x06005E51 RID: 24145
		// (set) Token: 0x06005E50 RID: 24144
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

		// Token: 0x17001F97 RID: 8087
		// (get) Token: 0x06005E53 RID: 24147
		// (set) Token: 0x06005E52 RID: 24146
		[DispId(-2147412098)]
		public virtual extern object onfocus
		{
			[TypeLibFunc(20)]
			[DispId(-2147412098)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412098)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17001F98 RID: 8088
		// (get) Token: 0x06005E55 RID: 24149
		// (set) Token: 0x06005E54 RID: 24148
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

		// Token: 0x06005E56 RID: 24150
		[DispId(-2147416110)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void blur();

		// Token: 0x06005E57 RID: 24151
		[DispId(-2147416095)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void addFilter([MarshalAs(25)] [In] object pUnk);

		// Token: 0x06005E58 RID: 24152
		[DispId(-2147416094)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void removeFilter([MarshalAs(25)] [In] object pUnk);

		// Token: 0x17001F99 RID: 8089
		// (get) Token: 0x06005E59 RID: 24153
		[DispId(-2147416093)]
		public virtual extern int clientHeight
		{
			[TypeLibFunc(20)]
			[DispId(-2147416093)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17001F9A RID: 8090
		// (get) Token: 0x06005E5A RID: 24154
		[DispId(-2147416092)]
		public virtual extern int clientWidth
		{
			[DispId(-2147416092)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17001F9B RID: 8091
		// (get) Token: 0x06005E5B RID: 24155
		[DispId(-2147416091)]
		public virtual extern int clientTop
		{
			[DispId(-2147416091)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17001F9C RID: 8092
		// (get) Token: 0x06005E5C RID: 24156
		[DispId(-2147416090)]
		public virtual extern int clientLeft
		{
			[TypeLibFunc(20)]
			[DispId(-2147416090)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x06005E5D RID: 24157
		[DispId(-2147417605)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool attachEvent([MarshalAs(19)] [In] string @event, [MarshalAs(26)] [In] object pdisp);

		// Token: 0x06005E5E RID: 24158
		[DispId(-2147417604)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void detachEvent([MarshalAs(19)] [In] string @event, [MarshalAs(26)] [In] object pdisp);

		// Token: 0x17001F9D RID: 8093
		// (get) Token: 0x06005E5F RID: 24159
		[DispId(-2147412996)]
		public virtual extern object readyState
		{
			[DispId(-2147412996)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x17001F9E RID: 8094
		// (get) Token: 0x06005E61 RID: 24161
		// (set) Token: 0x06005E60 RID: 24160
		[DispId(-2147412087)]
		public virtual extern object onreadystatechange
		{
			[TypeLibFunc(20)]
			[DispId(-2147412087)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412087)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17001F9F RID: 8095
		// (get) Token: 0x06005E63 RID: 24163
		// (set) Token: 0x06005E62 RID: 24162
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

		// Token: 0x17001FA0 RID: 8096
		// (get) Token: 0x06005E65 RID: 24165
		// (set) Token: 0x06005E64 RID: 24164
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

		// Token: 0x17001FA1 RID: 8097
		// (get) Token: 0x06005E67 RID: 24167
		// (set) Token: 0x06005E66 RID: 24166
		[DispId(-2147412048)]
		public virtual extern object oncellchange
		{
			[DispId(-2147412048)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412048)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17001FA2 RID: 8098
		// (get) Token: 0x06005E69 RID: 24169
		// (set) Token: 0x06005E68 RID: 24168
		[DispId(-2147412995)]
		public virtual extern string dir
		{
			[TypeLibFunc(20)]
			[DispId(-2147412995)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147412995)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x06005E6A RID: 24170
		[DispId(-2147417056)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(26)]
		public virtual extern object createControlRange();

		// Token: 0x17001FA3 RID: 8099
		// (get) Token: 0x06005E6B RID: 24171
		[DispId(-2147417055)]
		public virtual extern int scrollHeight
		{
			[TypeLibFunc(20)]
			[DispId(-2147417055)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17001FA4 RID: 8100
		// (get) Token: 0x06005E6C RID: 24172
		[DispId(-2147417054)]
		public virtual extern int scrollWidth
		{
			[TypeLibFunc(20)]
			[DispId(-2147417054)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17001FA5 RID: 8101
		// (get) Token: 0x06005E6E RID: 24174
		// (set) Token: 0x06005E6D RID: 24173
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

		// Token: 0x17001FA6 RID: 8102
		// (get) Token: 0x06005E70 RID: 24176
		// (set) Token: 0x06005E6F RID: 24175
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

		// Token: 0x06005E71 RID: 24177
		[DispId(-2147417050)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void clearAttributes();

		// Token: 0x17001FA7 RID: 8103
		// (get) Token: 0x06005E73 RID: 24179
		// (set) Token: 0x06005E72 RID: 24178
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

		// Token: 0x06005E74 RID: 24180
		[DispId(-2147417043)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElement insertAdjacentElement([MarshalAs(19)] [In] string where, [MarshalAs(28)] [In] IHTMLElement insertedElement);

		// Token: 0x06005E75 RID: 24181
		[DispId(-2147417047)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElement applyElement([MarshalAs(28)] [In] IHTMLElement apply, [MarshalAs(19)] [In] string where);

		// Token: 0x06005E76 RID: 24182
		[DispId(-2147417042)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string getAdjacentText([MarshalAs(19)] [In] string where);

		// Token: 0x06005E77 RID: 24183
		[DispId(-2147417041)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string replaceAdjacentText([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string newText);

		// Token: 0x17001FA8 RID: 8104
		// (get) Token: 0x06005E78 RID: 24184
		[DispId(-2147417040)]
		public virtual extern bool canHaveChildren
		{
			[DispId(-2147417040)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x06005E79 RID: 24185
		[DispId(-2147417032)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern int addBehavior([MarshalAs(19)] [In] string bstrUrl, [MarshalAs(27)] [In] [Optional] ref object pvarFactory);

		// Token: 0x06005E7A RID: 24186
		[DispId(-2147417031)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool removeBehavior([In] int cookie);

		// Token: 0x17001FA9 RID: 8105
		// (get) Token: 0x06005E7B RID: 24187
		[DispId(-2147417048)]
		public virtual extern IHTMLStyle runtimeStyle
		{
			[DispId(-2147417048)]
			[TypeLibFunc(1024)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17001FAA RID: 8106
		// (get) Token: 0x06005E7C RID: 24188
		[DispId(-2147417030)]
		public virtual extern object behaviorUrns
		{
			[DispId(-2147417030)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17001FAB RID: 8107
		// (get) Token: 0x06005E7E RID: 24190
		// (set) Token: 0x06005E7D RID: 24189
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

		// Token: 0x17001FAC RID: 8108
		// (get) Token: 0x06005E80 RID: 24192
		// (set) Token: 0x06005E7F RID: 24191
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

		// Token: 0x17001FAD RID: 8109
		// (get) Token: 0x06005E81 RID: 24193
		[DispId(-2147417028)]
		public virtual extern int readyStateValue
		{
			[TypeLibFunc(65)]
			[DispId(-2147417028)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x06005E82 RID: 24194
		[DispId(-2147417027)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElementCollection getElementsByTagName([MarshalAs(19)] [In] string v);

		// Token: 0x06005E83 RID: 24195
		[DispId(-2147417016)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void mergeAttributes([MarshalAs(28)] [In] IHTMLElement mergeThis, [MarshalAs(27)] [In] [Optional] ref object pvarFlags);

		// Token: 0x17001FAE RID: 8110
		// (get) Token: 0x06005E84 RID: 24196
		[DispId(-2147417015)]
		public virtual extern bool isMultiLine
		{
			[DispId(-2147417015)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17001FAF RID: 8111
		// (get) Token: 0x06005E85 RID: 24197
		[DispId(-2147417014)]
		public virtual extern bool canHaveHTML
		{
			[DispId(-2147417014)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17001FB0 RID: 8112
		// (get) Token: 0x06005E87 RID: 24199
		// (set) Token: 0x06005E86 RID: 24198
		[DispId(-2147412039)]
		public virtual extern object onlayoutcomplete
		{
			[TypeLibFunc(20)]
			[DispId(-2147412039)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412039)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17001FB1 RID: 8113
		// (get) Token: 0x06005E89 RID: 24201
		// (set) Token: 0x06005E88 RID: 24200
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

		// Token: 0x17001FB2 RID: 8114
		// (get) Token: 0x06005E8B RID: 24203
		// (set) Token: 0x06005E8A RID: 24202
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

		// Token: 0x17001FB3 RID: 8115
		// (get) Token: 0x06005E8D RID: 24205
		// (set) Token: 0x06005E8C RID: 24204
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

		// Token: 0x06005E8E RID: 24206
		[DispId(-2147417011)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void setActive();

		// Token: 0x17001FB4 RID: 8116
		// (get) Token: 0x06005E90 RID: 24208
		// (set) Token: 0x06005E8F RID: 24207
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

		// Token: 0x17001FB5 RID: 8117
		// (get) Token: 0x06005E91 RID: 24209
		[DispId(-2147417010)]
		public virtual extern bool isContentEditable
		{
			[DispId(-2147417010)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17001FB6 RID: 8118
		// (get) Token: 0x06005E93 RID: 24211
		// (set) Token: 0x06005E92 RID: 24210
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

		// Token: 0x17001FB7 RID: 8119
		// (get) Token: 0x06005E95 RID: 24213
		// (set) Token: 0x06005E94 RID: 24212
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

		// Token: 0x17001FB8 RID: 8120
		// (get) Token: 0x06005E96 RID: 24214
		[DispId(-2147417007)]
		public virtual extern bool isDisabled
		{
			[DispId(-2147417007)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17001FB9 RID: 8121
		// (get) Token: 0x06005E98 RID: 24216
		// (set) Token: 0x06005E97 RID: 24215
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

		// Token: 0x17001FBA RID: 8122
		// (get) Token: 0x06005E9A RID: 24218
		// (set) Token: 0x06005E99 RID: 24217
		[DispId(-2147412033)]
		public virtual extern object oncontrolselect
		{
			[DispId(-2147412033)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412033)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x06005E9B RID: 24219
		[DispId(-2147417006)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool FireEvent([MarshalAs(19)] [In] string bstrEventName, [MarshalAs(27)] [In] [Optional] ref object pvarEventObject);

		// Token: 0x17001FBB RID: 8123
		// (get) Token: 0x06005E9D RID: 24221
		// (set) Token: 0x06005E9C RID: 24220
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

		// Token: 0x17001FBC RID: 8124
		// (get) Token: 0x06005E9F RID: 24223
		// (set) Token: 0x06005E9E RID: 24222
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

		// Token: 0x17001FBD RID: 8125
		// (get) Token: 0x06005EA1 RID: 24225
		// (set) Token: 0x06005EA0 RID: 24224
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

		// Token: 0x17001FBE RID: 8126
		// (get) Token: 0x06005EA3 RID: 24227
		// (set) Token: 0x06005EA2 RID: 24226
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

		// Token: 0x17001FBF RID: 8127
		// (get) Token: 0x06005EA5 RID: 24229
		// (set) Token: 0x06005EA4 RID: 24228
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

		// Token: 0x17001FC0 RID: 8128
		// (get) Token: 0x06005EA7 RID: 24231
		// (set) Token: 0x06005EA6 RID: 24230
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

		// Token: 0x17001FC1 RID: 8129
		// (get) Token: 0x06005EA9 RID: 24233
		// (set) Token: 0x06005EA8 RID: 24232
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

		// Token: 0x17001FC2 RID: 8130
		// (get) Token: 0x06005EAB RID: 24235
		// (set) Token: 0x06005EAA RID: 24234
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

		// Token: 0x06005EAC RID: 24236
		[DispId(-2147417005)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool dragDrop();

		// Token: 0x17001FC3 RID: 8131
		// (get) Token: 0x06005EAD RID: 24237
		[DispId(-2147417004)]
		public virtual extern int glyphMode
		{
			[DispId(-2147417004)]
			[TypeLibFunc(1089)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17001FC4 RID: 8132
		// (get) Token: 0x06005EAF RID: 24239
		// (set) Token: 0x06005EAE RID: 24238
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

		// Token: 0x06005EB0 RID: 24240
		[DispId(-2147417000)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void normalize();

		// Token: 0x06005EB1 RID: 24241
		[DispId(-2147417003)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute getAttributeNode([MarshalAs(19)] [In] string bstrName);

		// Token: 0x06005EB2 RID: 24242
		[DispId(-2147417002)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute setAttributeNode([MarshalAs(28)] [In] IHTMLDOMAttribute pattr);

		// Token: 0x06005EB3 RID: 24243
		[DispId(-2147417001)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute removeAttributeNode([MarshalAs(28)] [In] IHTMLDOMAttribute pattr);

		// Token: 0x17001FC5 RID: 8133
		// (get) Token: 0x06005EB5 RID: 24245
		// (set) Token: 0x06005EB4 RID: 24244
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

		// Token: 0x17001FC6 RID: 8134
		// (get) Token: 0x06005EB7 RID: 24247
		// (set) Token: 0x06005EB6 RID: 24246
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

		// Token: 0x17001FC7 RID: 8135
		// (get) Token: 0x06005EB9 RID: 24249
		// (set) Token: 0x06005EB8 RID: 24248
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

		// Token: 0x17001FC8 RID: 8136
		// (get) Token: 0x06005EBA RID: 24250
		[DispId(-2147417058)]
		public virtual extern int uniqueNumber
		{
			[DispId(-2147417058)]
			[TypeLibFunc(64)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17001FC9 RID: 8137
		// (get) Token: 0x06005EBB RID: 24251
		[DispId(-2147417057)]
		public virtual extern string uniqueID
		{
			[TypeLibFunc(64)]
			[DispId(-2147417057)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17001FCA RID: 8138
		// (get) Token: 0x06005EBC RID: 24252
		[DispId(-2147417066)]
		public virtual extern int nodeType
		{
			[DispId(-2147417066)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17001FCB RID: 8139
		// (get) Token: 0x06005EBD RID: 24253
		[DispId(-2147417065)]
		public virtual extern IHTMLDOMNode parentNode
		{
			[DispId(-2147417065)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x06005EBE RID: 24254
		[DispId(-2147417064)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool hasChildNodes();

		// Token: 0x17001FCC RID: 8140
		// (get) Token: 0x06005EBF RID: 24255
		[DispId(-2147417063)]
		public virtual extern object childNodes
		{
			[DispId(-2147417063)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17001FCD RID: 8141
		// (get) Token: 0x06005EC0 RID: 24256
		[DispId(-2147417062)]
		public virtual extern object attributes
		{
			[DispId(-2147417062)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x06005EC1 RID: 24257
		[DispId(-2147417061)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode insertBefore([MarshalAs(28)] [In] IHTMLDOMNode newChild, [MarshalAs(27)] [In] [Optional] object refChild);

		// Token: 0x06005EC2 RID: 24258
		[DispId(-2147417060)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode removeChild([MarshalAs(28)] [In] IHTMLDOMNode oldChild);

		// Token: 0x06005EC3 RID: 24259
		[DispId(-2147417059)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode replaceChild([MarshalAs(28)] [In] IHTMLDOMNode newChild, [MarshalAs(28)] [In] IHTMLDOMNode oldChild);

		// Token: 0x06005EC4 RID: 24260
		[DispId(-2147417051)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode cloneNode([In] bool fDeep);

		// Token: 0x06005EC5 RID: 24261
		[DispId(-2147417046)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode removeNode([In] bool fDeep = false);

		// Token: 0x06005EC6 RID: 24262
		[DispId(-2147417044)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode swapNode([MarshalAs(28)] [In] IHTMLDOMNode otherNode);

		// Token: 0x06005EC7 RID: 24263
		[DispId(-2147417045)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode replaceNode([MarshalAs(28)] [In] IHTMLDOMNode replacement);

		// Token: 0x06005EC8 RID: 24264
		[DispId(-2147417039)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode appendChild([MarshalAs(28)] [In] IHTMLDOMNode newChild);

		// Token: 0x17001FCE RID: 8142
		// (get) Token: 0x06005EC9 RID: 24265
		[DispId(-2147417038)]
		public virtual extern string nodeName
		{
			[DispId(-2147417038)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17001FCF RID: 8143
		// (get) Token: 0x06005ECB RID: 24267
		// (set) Token: 0x06005ECA RID: 24266
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

		// Token: 0x17001FD0 RID: 8144
		// (get) Token: 0x06005ECC RID: 24268
		[DispId(-2147417036)]
		public virtual extern IHTMLDOMNode firstChild
		{
			[DispId(-2147417036)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17001FD1 RID: 8145
		// (get) Token: 0x06005ECD RID: 24269
		[DispId(-2147417035)]
		public virtual extern IHTMLDOMNode lastChild
		{
			[DispId(-2147417035)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17001FD2 RID: 8146
		// (get) Token: 0x06005ECE RID: 24270
		[DispId(-2147417034)]
		public virtual extern IHTMLDOMNode previousSibling
		{
			[DispId(-2147417034)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17001FD3 RID: 8147
		// (get) Token: 0x06005ECF RID: 24271
		[DispId(-2147417033)]
		public virtual extern IHTMLDOMNode nextSibling
		{
			[DispId(-2147417033)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17001FD4 RID: 8148
		// (get) Token: 0x06005ED0 RID: 24272
		[DispId(-2147416999)]
		public virtual extern object ownerDocument
		{
			[DispId(-2147416999)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17001FD5 RID: 8149
		// (get) Token: 0x06005ED2 RID: 24274
		// (set) Token: 0x06005ED1 RID: 24273
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

		// Token: 0x17001FD6 RID: 8150
		// (get) Token: 0x06005ED4 RID: 24276
		// (set) Token: 0x06005ED3 RID: 24275
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

		// Token: 0x17001FD7 RID: 8151
		// (get) Token: 0x06005ED6 RID: 24278
		// (set) Token: 0x06005ED5 RID: 24277
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

		// Token: 0x17001FD8 RID: 8152
		// (get) Token: 0x06005ED8 RID: 24280
		// (set) Token: 0x06005ED7 RID: 24279
		[DispId(-2147418040)]
		public virtual extern string align
		{
			[TypeLibFunc(20)]
			[DispId(-2147418040)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147418040)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17001FD9 RID: 8153
		// (get) Token: 0x06005EDA RID: 24282
		// (set) Token: 0x06005ED9 RID: 24281
		[DispId(-2147413107)]
		public virtual extern bool noWrap
		{
			[TypeLibFunc(20)]
			[DispId(-2147413107)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413107)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x06005EDB RID: 24283
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement_setAttribute([MarshalAs(19)] [In] string strAttributeName, [MarshalAs(27)] [In] object AttributeValue, [In] int lFlags = 1);

		// Token: 0x06005EDC RID: 24284
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		public virtual extern object IHTMLElement_getAttribute([MarshalAs(19)] [In] string strAttributeName, [In] int lFlags = 0);

		// Token: 0x06005EDD RID: 24285
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement_removeAttribute([MarshalAs(19)] [In] string strAttributeName, [In] int lFlags = 1);

		// Token: 0x17001FDA RID: 8154
		// (get) Token: 0x06005EDF RID: 24287
		// (set) Token: 0x06005EDE RID: 24286
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

		// Token: 0x17001FDB RID: 8155
		// (get) Token: 0x06005EE1 RID: 24289
		// (set) Token: 0x06005EE0 RID: 24288
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

		// Token: 0x17001FDC RID: 8156
		// (get) Token: 0x06005EE2 RID: 24290
		public virtual extern string IHTMLElement_tagName
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17001FDD RID: 8157
		// (get) Token: 0x06005EE3 RID: 24291
		public virtual extern IHTMLElement IHTMLElement_parentElement
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17001FDE RID: 8158
		// (get) Token: 0x06005EE4 RID: 24292
		public virtual extern IHTMLStyle IHTMLElement_style
		{
			[TypeLibFunc(1024)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17001FDF RID: 8159
		// (get) Token: 0x06005EE6 RID: 24294
		// (set) Token: 0x06005EE5 RID: 24293
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

		// Token: 0x17001FE0 RID: 8160
		// (get) Token: 0x06005EE8 RID: 24296
		// (set) Token: 0x06005EE7 RID: 24295
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

		// Token: 0x17001FE1 RID: 8161
		// (get) Token: 0x06005EEA RID: 24298
		// (set) Token: 0x06005EE9 RID: 24297
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

		// Token: 0x17001FE2 RID: 8162
		// (get) Token: 0x06005EEC RID: 24300
		// (set) Token: 0x06005EEB RID: 24299
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

		// Token: 0x17001FE3 RID: 8163
		// (get) Token: 0x06005EEE RID: 24302
		// (set) Token: 0x06005EED RID: 24301
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

		// Token: 0x17001FE4 RID: 8164
		// (get) Token: 0x06005EF0 RID: 24304
		// (set) Token: 0x06005EEF RID: 24303
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

		// Token: 0x17001FE5 RID: 8165
		// (get) Token: 0x06005EF2 RID: 24306
		// (set) Token: 0x06005EF1 RID: 24305
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

		// Token: 0x17001FE6 RID: 8166
		// (get) Token: 0x06005EF4 RID: 24308
		// (set) Token: 0x06005EF3 RID: 24307
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

		// Token: 0x17001FE7 RID: 8167
		// (get) Token: 0x06005EF6 RID: 24310
		// (set) Token: 0x06005EF5 RID: 24309
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

		// Token: 0x17001FE8 RID: 8168
		// (get) Token: 0x06005EF8 RID: 24312
		// (set) Token: 0x06005EF7 RID: 24311
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

		// Token: 0x17001FE9 RID: 8169
		// (get) Token: 0x06005EFA RID: 24314
		// (set) Token: 0x06005EF9 RID: 24313
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

		// Token: 0x17001FEA RID: 8170
		// (get) Token: 0x06005EFB RID: 24315
		public virtual extern object IHTMLElement_document
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17001FEB RID: 8171
		// (get) Token: 0x06005EFD RID: 24317
		// (set) Token: 0x06005EFC RID: 24316
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

		// Token: 0x17001FEC RID: 8172
		// (get) Token: 0x06005EFF RID: 24319
		// (set) Token: 0x06005EFE RID: 24318
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

		// Token: 0x17001FED RID: 8173
		// (get) Token: 0x06005F01 RID: 24321
		// (set) Token: 0x06005F00 RID: 24320
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

		// Token: 0x06005F02 RID: 24322
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement_scrollIntoView([MarshalAs(27)] [In] [Optional] object varargStart);

		// Token: 0x06005F03 RID: 24323
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement_contains([MarshalAs(28)] [In] IHTMLElement pChild);

		// Token: 0x17001FEE RID: 8174
		// (get) Token: 0x06005F04 RID: 24324
		public virtual extern int IHTMLElement_sourceIndex
		{
			[TypeLibFunc(4)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17001FEF RID: 8175
		// (get) Token: 0x06005F05 RID: 24325
		public virtual extern object IHTMLElement_recordNumber
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x17001FF0 RID: 8176
		// (get) Token: 0x06005F07 RID: 24327
		// (set) Token: 0x06005F06 RID: 24326
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

		// Token: 0x17001FF1 RID: 8177
		// (get) Token: 0x06005F08 RID: 24328
		public virtual extern int IHTMLElement_offsetLeft
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17001FF2 RID: 8178
		// (get) Token: 0x06005F09 RID: 24329
		public virtual extern int IHTMLElement_offsetTop
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17001FF3 RID: 8179
		// (get) Token: 0x06005F0A RID: 24330
		public virtual extern int IHTMLElement_offsetWidth
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17001FF4 RID: 8180
		// (get) Token: 0x06005F0B RID: 24331
		public virtual extern int IHTMLElement_offsetHeight
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17001FF5 RID: 8181
		// (get) Token: 0x06005F0C RID: 24332
		public virtual extern IHTMLElement IHTMLElement_offsetParent
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17001FF6 RID: 8182
		// (get) Token: 0x06005F0E RID: 24334
		// (set) Token: 0x06005F0D RID: 24333
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

		// Token: 0x17001FF7 RID: 8183
		// (get) Token: 0x06005F10 RID: 24336
		// (set) Token: 0x06005F0F RID: 24335
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

		// Token: 0x17001FF8 RID: 8184
		// (get) Token: 0x06005F12 RID: 24338
		// (set) Token: 0x06005F11 RID: 24337
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

		// Token: 0x17001FF9 RID: 8185
		// (get) Token: 0x06005F14 RID: 24340
		// (set) Token: 0x06005F13 RID: 24339
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

		// Token: 0x06005F15 RID: 24341
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement_insertAdjacentHTML([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string html);

		// Token: 0x06005F16 RID: 24342
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement_insertAdjacentText([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string text);

		// Token: 0x17001FFA RID: 8186
		// (get) Token: 0x06005F17 RID: 24343
		public virtual extern IHTMLElement IHTMLElement_parentTextEdit
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17001FFB RID: 8187
		// (get) Token: 0x06005F18 RID: 24344
		public virtual extern bool IHTMLElement_isTextEdit
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x06005F19 RID: 24345
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement_click();

		// Token: 0x17001FFC RID: 8188
		// (get) Token: 0x06005F1A RID: 24346
		public virtual extern IHTMLFiltersCollection IHTMLElement_filters
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17001FFD RID: 8189
		// (get) Token: 0x06005F1C RID: 24348
		// (set) Token: 0x06005F1B RID: 24347
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

		// Token: 0x06005F1D RID: 24349
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string IHTMLElement_toString();

		// Token: 0x17001FFE RID: 8190
		// (get) Token: 0x06005F1F RID: 24351
		// (set) Token: 0x06005F1E RID: 24350
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

		// Token: 0x17001FFF RID: 8191
		// (get) Token: 0x06005F21 RID: 24353
		// (set) Token: 0x06005F20 RID: 24352
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

		// Token: 0x17002000 RID: 8192
		// (get) Token: 0x06005F23 RID: 24355
		// (set) Token: 0x06005F22 RID: 24354
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

		// Token: 0x17002001 RID: 8193
		// (get) Token: 0x06005F25 RID: 24357
		// (set) Token: 0x06005F24 RID: 24356
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

		// Token: 0x17002002 RID: 8194
		// (get) Token: 0x06005F27 RID: 24359
		// (set) Token: 0x06005F26 RID: 24358
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

		// Token: 0x17002003 RID: 8195
		// (get) Token: 0x06005F29 RID: 24361
		// (set) Token: 0x06005F28 RID: 24360
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

		// Token: 0x17002004 RID: 8196
		// (get) Token: 0x06005F2B RID: 24363
		// (set) Token: 0x06005F2A RID: 24362
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

		// Token: 0x17002005 RID: 8197
		// (get) Token: 0x06005F2D RID: 24365
		// (set) Token: 0x06005F2C RID: 24364
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

		// Token: 0x17002006 RID: 8198
		// (get) Token: 0x06005F2F RID: 24367
		// (set) Token: 0x06005F2E RID: 24366
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

		// Token: 0x17002007 RID: 8199
		// (get) Token: 0x06005F30 RID: 24368
		public virtual extern object IHTMLElement_children
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17002008 RID: 8200
		// (get) Token: 0x06005F31 RID: 24369
		public virtual extern object IHTMLElement_all
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17002009 RID: 8201
		// (get) Token: 0x06005F32 RID: 24370
		public virtual extern string IHTMLElement2_scopeName
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x06005F33 RID: 24371
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_setCapture([In] bool containerCapture = true);

		// Token: 0x06005F34 RID: 24372
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_releaseCapture();

		// Token: 0x1700200A RID: 8202
		// (get) Token: 0x06005F36 RID: 24374
		// (set) Token: 0x06005F35 RID: 24373
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

		// Token: 0x06005F37 RID: 24375
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string IHTMLElement2_componentFromPoint([In] int x, [In] int y);

		// Token: 0x06005F38 RID: 24376
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_doScroll([MarshalAs(27)] [In] [Optional] object component);

		// Token: 0x1700200B RID: 8203
		// (get) Token: 0x06005F3A RID: 24378
		// (set) Token: 0x06005F39 RID: 24377
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

		// Token: 0x1700200C RID: 8204
		// (get) Token: 0x06005F3C RID: 24380
		// (set) Token: 0x06005F3B RID: 24379
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

		// Token: 0x1700200D RID: 8205
		// (get) Token: 0x06005F3E RID: 24382
		// (set) Token: 0x06005F3D RID: 24381
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

		// Token: 0x1700200E RID: 8206
		// (get) Token: 0x06005F40 RID: 24384
		// (set) Token: 0x06005F3F RID: 24383
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

		// Token: 0x1700200F RID: 8207
		// (get) Token: 0x06005F42 RID: 24386
		// (set) Token: 0x06005F41 RID: 24385
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

		// Token: 0x17002010 RID: 8208
		// (get) Token: 0x06005F44 RID: 24388
		// (set) Token: 0x06005F43 RID: 24387
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

		// Token: 0x17002011 RID: 8209
		// (get) Token: 0x06005F46 RID: 24390
		// (set) Token: 0x06005F45 RID: 24389
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

		// Token: 0x17002012 RID: 8210
		// (get) Token: 0x06005F48 RID: 24392
		// (set) Token: 0x06005F47 RID: 24391
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

		// Token: 0x17002013 RID: 8211
		// (get) Token: 0x06005F4A RID: 24394
		// (set) Token: 0x06005F49 RID: 24393
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

		// Token: 0x17002014 RID: 8212
		// (get) Token: 0x06005F4C RID: 24396
		// (set) Token: 0x06005F4B RID: 24395
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

		// Token: 0x17002015 RID: 8213
		// (get) Token: 0x06005F4E RID: 24398
		// (set) Token: 0x06005F4D RID: 24397
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

		// Token: 0x17002016 RID: 8214
		// (get) Token: 0x06005F50 RID: 24400
		// (set) Token: 0x06005F4F RID: 24399
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

		// Token: 0x17002017 RID: 8215
		// (get) Token: 0x06005F52 RID: 24402
		// (set) Token: 0x06005F51 RID: 24401
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

		// Token: 0x17002018 RID: 8216
		// (get) Token: 0x06005F53 RID: 24403
		public virtual extern IHTMLCurrentStyle IHTMLElement2_currentStyle
		{
			[TypeLibFunc(1024)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17002019 RID: 8217
		// (get) Token: 0x06005F55 RID: 24405
		// (set) Token: 0x06005F54 RID: 24404
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

		// Token: 0x06005F56 RID: 24406
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLRectCollection IHTMLElement2_getClientRects();

		// Token: 0x06005F57 RID: 24407
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLRect IHTMLElement2_getBoundingClientRect();

		// Token: 0x06005F58 RID: 24408
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_setExpression([MarshalAs(19)] [In] string propname, [MarshalAs(19)] [In] string expression, [MarshalAs(19)] [In] string language = "");

		// Token: 0x06005F59 RID: 24409
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		public virtual extern object IHTMLElement2_getExpression([MarshalAs(19)] [In] string propname);

		// Token: 0x06005F5A RID: 24410
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement2_removeExpression([MarshalAs(19)] [In] string propname);

		// Token: 0x1700201A RID: 8218
		// (get) Token: 0x06005F5C RID: 24412
		// (set) Token: 0x06005F5B RID: 24411
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

		// Token: 0x06005F5D RID: 24413
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_focus();

		// Token: 0x1700201B RID: 8219
		// (get) Token: 0x06005F5F RID: 24415
		// (set) Token: 0x06005F5E RID: 24414
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

		// Token: 0x1700201C RID: 8220
		// (get) Token: 0x06005F61 RID: 24417
		// (set) Token: 0x06005F60 RID: 24416
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

		// Token: 0x1700201D RID: 8221
		// (get) Token: 0x06005F63 RID: 24419
		// (set) Token: 0x06005F62 RID: 24418
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

		// Token: 0x1700201E RID: 8222
		// (get) Token: 0x06005F65 RID: 24421
		// (set) Token: 0x06005F64 RID: 24420
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

		// Token: 0x06005F66 RID: 24422
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_blur();

		// Token: 0x06005F67 RID: 24423
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_addFilter([MarshalAs(25)] [In] object pUnk);

		// Token: 0x06005F68 RID: 24424
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_removeFilter([MarshalAs(25)] [In] object pUnk);

		// Token: 0x1700201F RID: 8223
		// (get) Token: 0x06005F69 RID: 24425
		public virtual extern int IHTMLElement2_clientHeight
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17002020 RID: 8224
		// (get) Token: 0x06005F6A RID: 24426
		public virtual extern int IHTMLElement2_clientWidth
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17002021 RID: 8225
		// (get) Token: 0x06005F6B RID: 24427
		public virtual extern int IHTMLElement2_clientTop
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17002022 RID: 8226
		// (get) Token: 0x06005F6C RID: 24428
		public virtual extern int IHTMLElement2_clientLeft
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x06005F6D RID: 24429
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement2_attachEvent([MarshalAs(19)] [In] string @event, [MarshalAs(26)] [In] object pdisp);

		// Token: 0x06005F6E RID: 24430
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_detachEvent([MarshalAs(19)] [In] string @event, [MarshalAs(26)] [In] object pdisp);

		// Token: 0x17002023 RID: 8227
		// (get) Token: 0x06005F6F RID: 24431
		public virtual extern object IHTMLElement2_readyState
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x17002024 RID: 8228
		// (get) Token: 0x06005F71 RID: 24433
		// (set) Token: 0x06005F70 RID: 24432
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

		// Token: 0x17002025 RID: 8229
		// (get) Token: 0x06005F73 RID: 24435
		// (set) Token: 0x06005F72 RID: 24434
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

		// Token: 0x17002026 RID: 8230
		// (get) Token: 0x06005F75 RID: 24437
		// (set) Token: 0x06005F74 RID: 24436
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

		// Token: 0x17002027 RID: 8231
		// (get) Token: 0x06005F77 RID: 24439
		// (set) Token: 0x06005F76 RID: 24438
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

		// Token: 0x17002028 RID: 8232
		// (get) Token: 0x06005F79 RID: 24441
		// (set) Token: 0x06005F78 RID: 24440
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

		// Token: 0x06005F7A RID: 24442
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(26)]
		public virtual extern object IHTMLElement2_createControlRange();

		// Token: 0x17002029 RID: 8233
		// (get) Token: 0x06005F7B RID: 24443
		public virtual extern int IHTMLElement2_scrollHeight
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700202A RID: 8234
		// (get) Token: 0x06005F7C RID: 24444
		public virtual extern int IHTMLElement2_scrollWidth
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700202B RID: 8235
		// (get) Token: 0x06005F7E RID: 24446
		// (set) Token: 0x06005F7D RID: 24445
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

		// Token: 0x1700202C RID: 8236
		// (get) Token: 0x06005F80 RID: 24448
		// (set) Token: 0x06005F7F RID: 24447
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

		// Token: 0x06005F81 RID: 24449
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_clearAttributes();

		// Token: 0x06005F82 RID: 24450
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void mergeAttributes([MarshalAs(28)] [In] IHTMLElement mergeThis);

		// Token: 0x1700202D RID: 8237
		// (get) Token: 0x06005F84 RID: 24452
		// (set) Token: 0x06005F83 RID: 24451
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

		// Token: 0x06005F85 RID: 24453
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElement IHTMLElement2_insertAdjacentElement([MarshalAs(19)] [In] string where, [MarshalAs(28)] [In] IHTMLElement insertedElement);

		// Token: 0x06005F86 RID: 24454
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElement IHTMLElement2_applyElement([MarshalAs(28)] [In] IHTMLElement apply, [MarshalAs(19)] [In] string where);

		// Token: 0x06005F87 RID: 24455
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string IHTMLElement2_getAdjacentText([MarshalAs(19)] [In] string where);

		// Token: 0x06005F88 RID: 24456
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string IHTMLElement2_replaceAdjacentText([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string newText);

		// Token: 0x1700202E RID: 8238
		// (get) Token: 0x06005F89 RID: 24457
		public virtual extern bool IHTMLElement2_canHaveChildren
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x06005F8A RID: 24458
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern int IHTMLElement2_addBehavior([MarshalAs(19)] [In] string bstrUrl, [MarshalAs(27)] [In] [Optional] ref object pvarFactory);

		// Token: 0x06005F8B RID: 24459
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement2_removeBehavior([In] int cookie);

		// Token: 0x1700202F RID: 8239
		// (get) Token: 0x06005F8C RID: 24460
		public virtual extern IHTMLStyle IHTMLElement2_runtimeStyle
		{
			[TypeLibFunc(1024)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17002030 RID: 8240
		// (get) Token: 0x06005F8D RID: 24461
		public virtual extern object IHTMLElement2_behaviorUrns
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17002031 RID: 8241
		// (get) Token: 0x06005F8F RID: 24463
		// (set) Token: 0x06005F8E RID: 24462
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

		// Token: 0x17002032 RID: 8242
		// (get) Token: 0x06005F91 RID: 24465
		// (set) Token: 0x06005F90 RID: 24464
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

		// Token: 0x17002033 RID: 8243
		// (get) Token: 0x06005F92 RID: 24466
		public virtual extern int IHTMLElement2_readyStateValue
		{
			[TypeLibFunc(65)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x06005F93 RID: 24467
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElementCollection IHTMLElement2_getElementsByTagName([MarshalAs(19)] [In] string v);

		// Token: 0x06005F94 RID: 24468
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void mergeAttributes([MarshalAs(28)] [In] IHTMLElement mergeThis, [MarshalAs(27)] [In] [Optional] ref object pvarFlags);

		// Token: 0x17002034 RID: 8244
		// (get) Token: 0x06005F95 RID: 24469
		public virtual extern bool IHTMLElement3_isMultiLine
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17002035 RID: 8245
		// (get) Token: 0x06005F96 RID: 24470
		public virtual extern bool IHTMLElement3_canHaveHTML
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17002036 RID: 8246
		// (get) Token: 0x06005F98 RID: 24472
		// (set) Token: 0x06005F97 RID: 24471
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

		// Token: 0x17002037 RID: 8247
		// (get) Token: 0x06005F9A RID: 24474
		// (set) Token: 0x06005F99 RID: 24473
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

		// Token: 0x17002038 RID: 8248
		// (get) Token: 0x06005F9C RID: 24476
		// (set) Token: 0x06005F9B RID: 24475
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

		// Token: 0x17002039 RID: 8249
		// (get) Token: 0x06005F9E RID: 24478
		// (set) Token: 0x06005F9D RID: 24477
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

		// Token: 0x06005F9F RID: 24479
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement3_setActive();

		// Token: 0x1700203A RID: 8250
		// (get) Token: 0x06005FA1 RID: 24481
		// (set) Token: 0x06005FA0 RID: 24480
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

		// Token: 0x1700203B RID: 8251
		// (get) Token: 0x06005FA2 RID: 24482
		public virtual extern bool IHTMLElement3_isContentEditable
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700203C RID: 8252
		// (get) Token: 0x06005FA4 RID: 24484
		// (set) Token: 0x06005FA3 RID: 24483
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

		// Token: 0x1700203D RID: 8253
		// (get) Token: 0x06005FA6 RID: 24486
		// (set) Token: 0x06005FA5 RID: 24485
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

		// Token: 0x1700203E RID: 8254
		// (get) Token: 0x06005FA7 RID: 24487
		public virtual extern bool IHTMLElement3_isDisabled
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700203F RID: 8255
		// (get) Token: 0x06005FA9 RID: 24489
		// (set) Token: 0x06005FA8 RID: 24488
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

		// Token: 0x17002040 RID: 8256
		// (get) Token: 0x06005FAB RID: 24491
		// (set) Token: 0x06005FAA RID: 24490
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

		// Token: 0x06005FAC RID: 24492
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement3_FireEvent([MarshalAs(19)] [In] string bstrEventName, [MarshalAs(27)] [In] [Optional] ref object pvarEventObject);

		// Token: 0x17002041 RID: 8257
		// (get) Token: 0x06005FAE RID: 24494
		// (set) Token: 0x06005FAD RID: 24493
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

		// Token: 0x17002042 RID: 8258
		// (get) Token: 0x06005FB0 RID: 24496
		// (set) Token: 0x06005FAF RID: 24495
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

		// Token: 0x17002043 RID: 8259
		// (get) Token: 0x06005FB2 RID: 24498
		// (set) Token: 0x06005FB1 RID: 24497
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

		// Token: 0x17002044 RID: 8260
		// (get) Token: 0x06005FB4 RID: 24500
		// (set) Token: 0x06005FB3 RID: 24499
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

		// Token: 0x17002045 RID: 8261
		// (get) Token: 0x06005FB6 RID: 24502
		// (set) Token: 0x06005FB5 RID: 24501
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

		// Token: 0x17002046 RID: 8262
		// (get) Token: 0x06005FB8 RID: 24504
		// (set) Token: 0x06005FB7 RID: 24503
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

		// Token: 0x17002047 RID: 8263
		// (get) Token: 0x06005FBA RID: 24506
		// (set) Token: 0x06005FB9 RID: 24505
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

		// Token: 0x17002048 RID: 8264
		// (get) Token: 0x06005FBC RID: 24508
		// (set) Token: 0x06005FBB RID: 24507
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

		// Token: 0x06005FBD RID: 24509
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement3_dragDrop();

		// Token: 0x17002049 RID: 8265
		// (get) Token: 0x06005FBE RID: 24510
		public virtual extern int IHTMLElement3_glyphMode
		{
			[TypeLibFunc(1089)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700204A RID: 8266
		// (get) Token: 0x06005FC0 RID: 24512
		// (set) Token: 0x06005FBF RID: 24511
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

		// Token: 0x06005FC1 RID: 24513
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement4_normalize();

		// Token: 0x06005FC2 RID: 24514
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute IHTMLElement4_getAttributeNode([MarshalAs(19)] [In] string bstrName);

		// Token: 0x06005FC3 RID: 24515
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute IHTMLElement4_setAttributeNode([MarshalAs(28)] [In] IHTMLDOMAttribute pattr);

		// Token: 0x06005FC4 RID: 24516
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute IHTMLElement4_removeAttributeNode([MarshalAs(28)] [In] IHTMLDOMAttribute pattr);

		// Token: 0x1700204B RID: 8267
		// (get) Token: 0x06005FC6 RID: 24518
		// (set) Token: 0x06005FC5 RID: 24517
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

		// Token: 0x1700204C RID: 8268
		// (get) Token: 0x06005FC8 RID: 24520
		// (set) Token: 0x06005FC7 RID: 24519
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

		// Token: 0x1700204D RID: 8269
		// (get) Token: 0x06005FCA RID: 24522
		// (set) Token: 0x06005FC9 RID: 24521
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

		// Token: 0x1700204E RID: 8270
		// (get) Token: 0x06005FCB RID: 24523
		public virtual extern int IHTMLUniqueName_uniqueNumber
		{
			[TypeLibFunc(64)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700204F RID: 8271
		// (get) Token: 0x06005FCC RID: 24524
		public virtual extern string IHTMLUniqueName_uniqueID
		{
			[TypeLibFunc(64)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17002050 RID: 8272
		// (get) Token: 0x06005FCD RID: 24525
		public virtual extern int IHTMLDOMNode_nodeType
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17002051 RID: 8273
		// (get) Token: 0x06005FCE RID: 24526
		public virtual extern IHTMLDOMNode IHTMLDOMNode_parentNode
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x06005FCF RID: 24527
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLDOMNode_hasChildNodes();

		// Token: 0x17002052 RID: 8274
		// (get) Token: 0x06005FD0 RID: 24528
		public virtual extern object IHTMLDOMNode_childNodes
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17002053 RID: 8275
		// (get) Token: 0x06005FD1 RID: 24529
		public virtual extern object IHTMLDOMNode_attributes
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x06005FD2 RID: 24530
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_insertBefore([MarshalAs(28)] [In] IHTMLDOMNode newChild, [MarshalAs(27)] [In] [Optional] object refChild);

		// Token: 0x06005FD3 RID: 24531
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_removeChild([MarshalAs(28)] [In] IHTMLDOMNode oldChild);

		// Token: 0x06005FD4 RID: 24532
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_replaceChild([MarshalAs(28)] [In] IHTMLDOMNode newChild, [MarshalAs(28)] [In] IHTMLDOMNode oldChild);

		// Token: 0x06005FD5 RID: 24533
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_cloneNode([In] bool fDeep);

		// Token: 0x06005FD6 RID: 24534
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_removeNode([In] bool fDeep = false);

		// Token: 0x06005FD7 RID: 24535
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_swapNode([MarshalAs(28)] [In] IHTMLDOMNode otherNode);

		// Token: 0x06005FD8 RID: 24536
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_replaceNode([MarshalAs(28)] [In] IHTMLDOMNode replacement);

		// Token: 0x06005FD9 RID: 24537
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_appendChild([MarshalAs(28)] [In] IHTMLDOMNode newChild);

		// Token: 0x17002054 RID: 8276
		// (get) Token: 0x06005FDA RID: 24538
		public virtual extern string IHTMLDOMNode_nodeName
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17002055 RID: 8277
		// (get) Token: 0x06005FDC RID: 24540
		// (set) Token: 0x06005FDB RID: 24539
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

		// Token: 0x17002056 RID: 8278
		// (get) Token: 0x06005FDD RID: 24541
		public virtual extern IHTMLDOMNode IHTMLDOMNode_firstChild
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17002057 RID: 8279
		// (get) Token: 0x06005FDE RID: 24542
		public virtual extern IHTMLDOMNode IHTMLDOMNode_lastChild
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17002058 RID: 8280
		// (get) Token: 0x06005FDF RID: 24543
		public virtual extern IHTMLDOMNode IHTMLDOMNode_previousSibling
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17002059 RID: 8281
		// (get) Token: 0x06005FE0 RID: 24544
		public virtual extern IHTMLDOMNode IHTMLDOMNode_nextSibling
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x1700205A RID: 8282
		// (get) Token: 0x06005FE1 RID: 24545
		public virtual extern object IHTMLDOMNode2_ownerDocument
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x1700205B RID: 8283
		// (get) Token: 0x06005FE3 RID: 24547
		// (set) Token: 0x06005FE2 RID: 24546
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

		// Token: 0x1700205C RID: 8284
		// (get) Token: 0x06005FE5 RID: 24549
		// (set) Token: 0x06005FE4 RID: 24548
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

		// Token: 0x1700205D RID: 8285
		// (get) Token: 0x06005FE7 RID: 24551
		// (set) Token: 0x06005FE6 RID: 24550
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

		// Token: 0x1700205E RID: 8286
		// (get) Token: 0x06005FE9 RID: 24553
		// (set) Token: 0x06005FE8 RID: 24552
		public virtual extern string IHTMLDivElement_align
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

		// Token: 0x1700205F RID: 8287
		// (get) Token: 0x06005FEB RID: 24555
		// (set) Token: 0x06005FEA RID: 24554
		public virtual extern bool IHTMLDivElement_noWrap
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x17002060 RID: 8288
		// (get) Token: 0x06005FED RID: 24557
		// (set) Token: 0x06005FEC RID: 24556
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

		// Token: 0x06005FEE RID: 24558
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLControlElement_focus();

		// Token: 0x17002061 RID: 8289
		// (get) Token: 0x06005FF0 RID: 24560
		// (set) Token: 0x06005FEF RID: 24559
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

		// Token: 0x17002062 RID: 8290
		// (get) Token: 0x06005FF2 RID: 24562
		// (set) Token: 0x06005FF1 RID: 24561
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

		// Token: 0x17002063 RID: 8291
		// (get) Token: 0x06005FF4 RID: 24564
		// (set) Token: 0x06005FF3 RID: 24563
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

		// Token: 0x17002064 RID: 8292
		// (get) Token: 0x06005FF6 RID: 24566
		// (set) Token: 0x06005FF5 RID: 24565
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

		// Token: 0x06005FF7 RID: 24567
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLControlElement_blur();

		// Token: 0x06005FF8 RID: 24568
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLControlElement_addFilter([MarshalAs(25)] [In] object pUnk);

		// Token: 0x06005FF9 RID: 24569
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLControlElement_removeFilter([MarshalAs(25)] [In] object pUnk);

		// Token: 0x17002065 RID: 8293
		// (get) Token: 0x06005FFA RID: 24570
		public virtual extern int IHTMLControlElement_clientHeight
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17002066 RID: 8294
		// (get) Token: 0x06005FFB RID: 24571
		public virtual extern int IHTMLControlElement_clientWidth
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17002067 RID: 8295
		// (get) Token: 0x06005FFC RID: 24572
		public virtual extern int IHTMLControlElement_clientTop
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17002068 RID: 8296
		// (get) Token: 0x06005FFD RID: 24573
		public virtual extern int IHTMLControlElement_clientLeft
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x06005FFE RID: 24574
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(26)]
		public virtual extern object IHTMLTextContainer_createControlRange();

		// Token: 0x17002069 RID: 8297
		// (get) Token: 0x06005FFF RID: 24575
		public virtual extern int IHTMLTextContainer_scrollHeight
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700206A RID: 8298
		// (get) Token: 0x06006000 RID: 24576
		public virtual extern int IHTMLTextContainer_scrollWidth
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700206B RID: 8299
		// (get) Token: 0x06006002 RID: 24578
		// (set) Token: 0x06006001 RID: 24577
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

		// Token: 0x1700206C RID: 8300
		// (get) Token: 0x06006004 RID: 24580
		// (set) Token: 0x06006003 RID: 24579
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

		// Token: 0x1700206D RID: 8301
		// (get) Token: 0x06006006 RID: 24582
		// (set) Token: 0x06006005 RID: 24581
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

		// Token: 0x14000A66 RID: 2662
		// (add) Token: 0x06006007 RID: 24583
		// (remove) Token: 0x06006008 RID: 24584
		public virtual extern event HTMLElementEvents_onhelpEventHandler HTMLElementEvents_Event_onhelp;

		// Token: 0x14000A67 RID: 2663
		// (add) Token: 0x06006009 RID: 24585
		// (remove) Token: 0x0600600A RID: 24586
		public virtual extern event HTMLElementEvents_onclickEventHandler HTMLElementEvents_Event_onclick;

		// Token: 0x14000A68 RID: 2664
		// (add) Token: 0x0600600B RID: 24587
		// (remove) Token: 0x0600600C RID: 24588
		public virtual extern event HTMLElementEvents_ondblclickEventHandler HTMLElementEvents_Event_ondblclick;

		// Token: 0x14000A69 RID: 2665
		// (add) Token: 0x0600600D RID: 24589
		// (remove) Token: 0x0600600E RID: 24590
		public virtual extern event HTMLElementEvents_onkeypressEventHandler HTMLElementEvents_Event_onkeypress;

		// Token: 0x14000A6A RID: 2666
		// (add) Token: 0x0600600F RID: 24591
		// (remove) Token: 0x06006010 RID: 24592
		public virtual extern event HTMLElementEvents_onkeydownEventHandler HTMLElementEvents_Event_onkeydown;

		// Token: 0x14000A6B RID: 2667
		// (add) Token: 0x06006011 RID: 24593
		// (remove) Token: 0x06006012 RID: 24594
		public virtual extern event HTMLElementEvents_onkeyupEventHandler HTMLElementEvents_Event_onkeyup;

		// Token: 0x14000A6C RID: 2668
		// (add) Token: 0x06006013 RID: 24595
		// (remove) Token: 0x06006014 RID: 24596
		public virtual extern event HTMLElementEvents_onmouseoutEventHandler HTMLElementEvents_Event_onmouseout;

		// Token: 0x14000A6D RID: 2669
		// (add) Token: 0x06006015 RID: 24597
		// (remove) Token: 0x06006016 RID: 24598
		public virtual extern event HTMLElementEvents_onmouseoverEventHandler HTMLElementEvents_Event_onmouseover;

		// Token: 0x14000A6E RID: 2670
		// (add) Token: 0x06006017 RID: 24599
		// (remove) Token: 0x06006018 RID: 24600
		public virtual extern event HTMLElementEvents_onmousemoveEventHandler HTMLElementEvents_Event_onmousemove;

		// Token: 0x14000A6F RID: 2671
		// (add) Token: 0x06006019 RID: 24601
		// (remove) Token: 0x0600601A RID: 24602
		public virtual extern event HTMLElementEvents_onmousedownEventHandler HTMLElementEvents_Event_onmousedown;

		// Token: 0x14000A70 RID: 2672
		// (add) Token: 0x0600601B RID: 24603
		// (remove) Token: 0x0600601C RID: 24604
		public virtual extern event HTMLElementEvents_onmouseupEventHandler HTMLElementEvents_Event_onmouseup;

		// Token: 0x14000A71 RID: 2673
		// (add) Token: 0x0600601D RID: 24605
		// (remove) Token: 0x0600601E RID: 24606
		public virtual extern event HTMLElementEvents_onselectstartEventHandler HTMLElementEvents_Event_onselectstart;

		// Token: 0x14000A72 RID: 2674
		// (add) Token: 0x0600601F RID: 24607
		// (remove) Token: 0x06006020 RID: 24608
		public virtual extern event HTMLElementEvents_onfilterchangeEventHandler HTMLElementEvents_Event_onfilterchange;

		// Token: 0x14000A73 RID: 2675
		// (add) Token: 0x06006021 RID: 24609
		// (remove) Token: 0x06006022 RID: 24610
		public virtual extern event HTMLElementEvents_ondragstartEventHandler HTMLElementEvents_Event_ondragstart;

		// Token: 0x14000A74 RID: 2676
		// (add) Token: 0x06006023 RID: 24611
		// (remove) Token: 0x06006024 RID: 24612
		public virtual extern event HTMLElementEvents_onbeforeupdateEventHandler HTMLElementEvents_Event_onbeforeupdate;

		// Token: 0x14000A75 RID: 2677
		// (add) Token: 0x06006025 RID: 24613
		// (remove) Token: 0x06006026 RID: 24614
		public virtual extern event HTMLElementEvents_onafterupdateEventHandler HTMLElementEvents_Event_onafterupdate;

		// Token: 0x14000A76 RID: 2678
		// (add) Token: 0x06006027 RID: 24615
		// (remove) Token: 0x06006028 RID: 24616
		public virtual extern event HTMLElementEvents_onerrorupdateEventHandler HTMLElementEvents_Event_onerrorupdate;

		// Token: 0x14000A77 RID: 2679
		// (add) Token: 0x06006029 RID: 24617
		// (remove) Token: 0x0600602A RID: 24618
		public virtual extern event HTMLElementEvents_onrowexitEventHandler HTMLElementEvents_Event_onrowexit;

		// Token: 0x14000A78 RID: 2680
		// (add) Token: 0x0600602B RID: 24619
		// (remove) Token: 0x0600602C RID: 24620
		public virtual extern event HTMLElementEvents_onrowenterEventHandler HTMLElementEvents_Event_onrowenter;

		// Token: 0x14000A79 RID: 2681
		// (add) Token: 0x0600602D RID: 24621
		// (remove) Token: 0x0600602E RID: 24622
		public virtual extern event HTMLElementEvents_ondatasetchangedEventHandler HTMLElementEvents_Event_ondatasetchanged;

		// Token: 0x14000A7A RID: 2682
		// (add) Token: 0x0600602F RID: 24623
		// (remove) Token: 0x06006030 RID: 24624
		public virtual extern event HTMLElementEvents_ondataavailableEventHandler HTMLElementEvents_Event_ondataavailable;

		// Token: 0x14000A7B RID: 2683
		// (add) Token: 0x06006031 RID: 24625
		// (remove) Token: 0x06006032 RID: 24626
		public virtual extern event HTMLElementEvents_ondatasetcompleteEventHandler HTMLElementEvents_Event_ondatasetcomplete;

		// Token: 0x14000A7C RID: 2684
		// (add) Token: 0x06006033 RID: 24627
		// (remove) Token: 0x06006034 RID: 24628
		public virtual extern event HTMLElementEvents_onlosecaptureEventHandler HTMLElementEvents_Event_onlosecapture;

		// Token: 0x14000A7D RID: 2685
		// (add) Token: 0x06006035 RID: 24629
		// (remove) Token: 0x06006036 RID: 24630
		public virtual extern event HTMLElementEvents_onpropertychangeEventHandler HTMLElementEvents_Event_onpropertychange;

		// Token: 0x14000A7E RID: 2686
		// (add) Token: 0x06006037 RID: 24631
		// (remove) Token: 0x06006038 RID: 24632
		public virtual extern event HTMLElementEvents_onscrollEventHandler HTMLElementEvents_Event_onscroll;

		// Token: 0x14000A7F RID: 2687
		// (add) Token: 0x06006039 RID: 24633
		// (remove) Token: 0x0600603A RID: 24634
		public virtual extern event HTMLElementEvents_onfocusEventHandler HTMLElementEvents_Event_onfocus;

		// Token: 0x14000A80 RID: 2688
		// (add) Token: 0x0600603B RID: 24635
		// (remove) Token: 0x0600603C RID: 24636
		public virtual extern event HTMLElementEvents_onblurEventHandler HTMLElementEvents_Event_onblur;

		// Token: 0x14000A81 RID: 2689
		// (add) Token: 0x0600603D RID: 24637
		// (remove) Token: 0x0600603E RID: 24638
		public virtual extern event HTMLElementEvents_onresizeEventHandler HTMLElementEvents_Event_onresize;

		// Token: 0x14000A82 RID: 2690
		// (add) Token: 0x0600603F RID: 24639
		// (remove) Token: 0x06006040 RID: 24640
		public virtual extern event HTMLElementEvents_ondragEventHandler HTMLElementEvents_Event_ondrag;

		// Token: 0x14000A83 RID: 2691
		// (add) Token: 0x06006041 RID: 24641
		// (remove) Token: 0x06006042 RID: 24642
		public virtual extern event HTMLElementEvents_ondragendEventHandler HTMLElementEvents_Event_ondragend;

		// Token: 0x14000A84 RID: 2692
		// (add) Token: 0x06006043 RID: 24643
		// (remove) Token: 0x06006044 RID: 24644
		public virtual extern event HTMLElementEvents_ondragenterEventHandler HTMLElementEvents_Event_ondragenter;

		// Token: 0x14000A85 RID: 2693
		// (add) Token: 0x06006045 RID: 24645
		// (remove) Token: 0x06006046 RID: 24646
		public virtual extern event HTMLElementEvents_ondragoverEventHandler HTMLElementEvents_Event_ondragover;

		// Token: 0x14000A86 RID: 2694
		// (add) Token: 0x06006047 RID: 24647
		// (remove) Token: 0x06006048 RID: 24648
		public virtual extern event HTMLElementEvents_ondragleaveEventHandler HTMLElementEvents_Event_ondragleave;

		// Token: 0x14000A87 RID: 2695
		// (add) Token: 0x06006049 RID: 24649
		// (remove) Token: 0x0600604A RID: 24650
		public virtual extern event HTMLElementEvents_ondropEventHandler HTMLElementEvents_Event_ondrop;

		// Token: 0x14000A88 RID: 2696
		// (add) Token: 0x0600604B RID: 24651
		// (remove) Token: 0x0600604C RID: 24652
		public virtual extern event HTMLElementEvents_onbeforecutEventHandler HTMLElementEvents_Event_onbeforecut;

		// Token: 0x14000A89 RID: 2697
		// (add) Token: 0x0600604D RID: 24653
		// (remove) Token: 0x0600604E RID: 24654
		public virtual extern event HTMLElementEvents_oncutEventHandler HTMLElementEvents_Event_oncut;

		// Token: 0x14000A8A RID: 2698
		// (add) Token: 0x0600604F RID: 24655
		// (remove) Token: 0x06006050 RID: 24656
		public virtual extern event HTMLElementEvents_onbeforecopyEventHandler HTMLElementEvents_Event_onbeforecopy;

		// Token: 0x14000A8B RID: 2699
		// (add) Token: 0x06006051 RID: 24657
		// (remove) Token: 0x06006052 RID: 24658
		public virtual extern event HTMLElementEvents_oncopyEventHandler HTMLElementEvents_Event_oncopy;

		// Token: 0x14000A8C RID: 2700
		// (add) Token: 0x06006053 RID: 24659
		// (remove) Token: 0x06006054 RID: 24660
		public virtual extern event HTMLElementEvents_onbeforepasteEventHandler HTMLElementEvents_Event_onbeforepaste;

		// Token: 0x14000A8D RID: 2701
		// (add) Token: 0x06006055 RID: 24661
		// (remove) Token: 0x06006056 RID: 24662
		public virtual extern event HTMLElementEvents_onpasteEventHandler HTMLElementEvents_Event_onpaste;

		// Token: 0x14000A8E RID: 2702
		// (add) Token: 0x06006057 RID: 24663
		// (remove) Token: 0x06006058 RID: 24664
		public virtual extern event HTMLElementEvents_oncontextmenuEventHandler HTMLElementEvents_Event_oncontextmenu;

		// Token: 0x14000A8F RID: 2703
		// (add) Token: 0x06006059 RID: 24665
		// (remove) Token: 0x0600605A RID: 24666
		public virtual extern event HTMLElementEvents_onrowsdeleteEventHandler HTMLElementEvents_Event_onrowsdelete;

		// Token: 0x14000A90 RID: 2704
		// (add) Token: 0x0600605B RID: 24667
		// (remove) Token: 0x0600605C RID: 24668
		public virtual extern event HTMLElementEvents_onrowsinsertedEventHandler HTMLElementEvents_Event_onrowsinserted;

		// Token: 0x14000A91 RID: 2705
		// (add) Token: 0x0600605D RID: 24669
		// (remove) Token: 0x0600605E RID: 24670
		public virtual extern event HTMLElementEvents_oncellchangeEventHandler HTMLElementEvents_Event_oncellchange;

		// Token: 0x14000A92 RID: 2706
		// (add) Token: 0x0600605F RID: 24671
		// (remove) Token: 0x06006060 RID: 24672
		public virtual extern event HTMLElementEvents_onreadystatechangeEventHandler HTMLElementEvents_Event_onreadystatechange;

		// Token: 0x14000A93 RID: 2707
		// (add) Token: 0x06006061 RID: 24673
		// (remove) Token: 0x06006062 RID: 24674
		public virtual extern event HTMLElementEvents_onbeforeeditfocusEventHandler HTMLElementEvents_Event_onbeforeeditfocus;

		// Token: 0x14000A94 RID: 2708
		// (add) Token: 0x06006063 RID: 24675
		// (remove) Token: 0x06006064 RID: 24676
		public virtual extern event HTMLElementEvents_onlayoutcompleteEventHandler HTMLElementEvents_Event_onlayoutcomplete;

		// Token: 0x14000A95 RID: 2709
		// (add) Token: 0x06006065 RID: 24677
		// (remove) Token: 0x06006066 RID: 24678
		public virtual extern event HTMLElementEvents_onpageEventHandler HTMLElementEvents_Event_onpage;

		// Token: 0x14000A96 RID: 2710
		// (add) Token: 0x06006067 RID: 24679
		// (remove) Token: 0x06006068 RID: 24680
		public virtual extern event HTMLElementEvents_onbeforedeactivateEventHandler HTMLElementEvents_Event_onbeforedeactivate;

		// Token: 0x14000A97 RID: 2711
		// (add) Token: 0x06006069 RID: 24681
		// (remove) Token: 0x0600606A RID: 24682
		public virtual extern event HTMLElementEvents_onbeforeactivateEventHandler HTMLElementEvents_Event_onbeforeactivate;

		// Token: 0x14000A98 RID: 2712
		// (add) Token: 0x0600606B RID: 24683
		// (remove) Token: 0x0600606C RID: 24684
		public virtual extern event HTMLElementEvents_onmoveEventHandler HTMLElementEvents_Event_onmove;

		// Token: 0x14000A99 RID: 2713
		// (add) Token: 0x0600606D RID: 24685
		// (remove) Token: 0x0600606E RID: 24686
		public virtual extern event HTMLElementEvents_oncontrolselectEventHandler HTMLElementEvents_Event_oncontrolselect;

		// Token: 0x14000A9A RID: 2714
		// (add) Token: 0x0600606F RID: 24687
		// (remove) Token: 0x06006070 RID: 24688
		public virtual extern event HTMLElementEvents_onmovestartEventHandler HTMLElementEvents_Event_onmovestart;

		// Token: 0x14000A9B RID: 2715
		// (add) Token: 0x06006071 RID: 24689
		// (remove) Token: 0x06006072 RID: 24690
		public virtual extern event HTMLElementEvents_onmoveendEventHandler HTMLElementEvents_Event_onmoveend;

		// Token: 0x14000A9C RID: 2716
		// (add) Token: 0x06006073 RID: 24691
		// (remove) Token: 0x06006074 RID: 24692
		public virtual extern event HTMLElementEvents_onresizestartEventHandler HTMLElementEvents_Event_onresizestart;

		// Token: 0x14000A9D RID: 2717
		// (add) Token: 0x06006075 RID: 24693
		// (remove) Token: 0x06006076 RID: 24694
		public virtual extern event HTMLElementEvents_onresizeendEventHandler HTMLElementEvents_Event_onresizeend;

		// Token: 0x14000A9E RID: 2718
		// (add) Token: 0x06006077 RID: 24695
		// (remove) Token: 0x06006078 RID: 24696
		public virtual extern event HTMLElementEvents_onmouseenterEventHandler HTMLElementEvents_Event_onmouseenter;

		// Token: 0x14000A9F RID: 2719
		// (add) Token: 0x06006079 RID: 24697
		// (remove) Token: 0x0600607A RID: 24698
		public virtual extern event HTMLElementEvents_onmouseleaveEventHandler HTMLElementEvents_Event_onmouseleave;

		// Token: 0x14000AA0 RID: 2720
		// (add) Token: 0x0600607B RID: 24699
		// (remove) Token: 0x0600607C RID: 24700
		public virtual extern event HTMLElementEvents_onmousewheelEventHandler HTMLElementEvents_Event_onmousewheel;

		// Token: 0x14000AA1 RID: 2721
		// (add) Token: 0x0600607D RID: 24701
		// (remove) Token: 0x0600607E RID: 24702
		public virtual extern event HTMLElementEvents_onactivateEventHandler HTMLElementEvents_Event_onactivate;

		// Token: 0x14000AA2 RID: 2722
		// (add) Token: 0x0600607F RID: 24703
		// (remove) Token: 0x06006080 RID: 24704
		public virtual extern event HTMLElementEvents_ondeactivateEventHandler HTMLElementEvents_Event_ondeactivate;

		// Token: 0x14000AA3 RID: 2723
		// (add) Token: 0x06006081 RID: 24705
		// (remove) Token: 0x06006082 RID: 24706
		public virtual extern event HTMLElementEvents_onfocusinEventHandler HTMLElementEvents_Event_onfocusin;

		// Token: 0x14000AA4 RID: 2724
		// (add) Token: 0x06006083 RID: 24707
		// (remove) Token: 0x06006084 RID: 24708
		public virtual extern event HTMLElementEvents_onfocusoutEventHandler HTMLElementEvents_Event_onfocusout;

		// Token: 0x14000AA5 RID: 2725
		// (add) Token: 0x06006085 RID: 24709
		// (remove) Token: 0x06006086 RID: 24710
		public virtual extern event HTMLTextContainerEvents_onhelpEventHandler HTMLTextContainerEvents_Event_onhelp;

		// Token: 0x14000AA6 RID: 2726
		// (add) Token: 0x06006087 RID: 24711
		// (remove) Token: 0x06006088 RID: 24712
		public virtual extern event HTMLTextContainerEvents_onclickEventHandler HTMLTextContainerEvents_Event_onclick;

		// Token: 0x14000AA7 RID: 2727
		// (add) Token: 0x06006089 RID: 24713
		// (remove) Token: 0x0600608A RID: 24714
		public virtual extern event HTMLTextContainerEvents_ondblclickEventHandler HTMLTextContainerEvents_Event_ondblclick;

		// Token: 0x14000AA8 RID: 2728
		// (add) Token: 0x0600608B RID: 24715
		// (remove) Token: 0x0600608C RID: 24716
		public virtual extern event HTMLTextContainerEvents_onkeypressEventHandler HTMLTextContainerEvents_Event_onkeypress;

		// Token: 0x14000AA9 RID: 2729
		// (add) Token: 0x0600608D RID: 24717
		// (remove) Token: 0x0600608E RID: 24718
		public virtual extern event HTMLTextContainerEvents_onkeydownEventHandler HTMLTextContainerEvents_Event_onkeydown;

		// Token: 0x14000AAA RID: 2730
		// (add) Token: 0x0600608F RID: 24719
		// (remove) Token: 0x06006090 RID: 24720
		public virtual extern event HTMLTextContainerEvents_onkeyupEventHandler HTMLTextContainerEvents_Event_onkeyup;

		// Token: 0x14000AAB RID: 2731
		// (add) Token: 0x06006091 RID: 24721
		// (remove) Token: 0x06006092 RID: 24722
		public virtual extern event HTMLTextContainerEvents_onmouseoutEventHandler HTMLTextContainerEvents_Event_onmouseout;

		// Token: 0x14000AAC RID: 2732
		// (add) Token: 0x06006093 RID: 24723
		// (remove) Token: 0x06006094 RID: 24724
		public virtual extern event HTMLTextContainerEvents_onmouseoverEventHandler HTMLTextContainerEvents_Event_onmouseover;

		// Token: 0x14000AAD RID: 2733
		// (add) Token: 0x06006095 RID: 24725
		// (remove) Token: 0x06006096 RID: 24726
		public virtual extern event HTMLTextContainerEvents_onmousemoveEventHandler HTMLTextContainerEvents_Event_onmousemove;

		// Token: 0x14000AAE RID: 2734
		// (add) Token: 0x06006097 RID: 24727
		// (remove) Token: 0x06006098 RID: 24728
		public virtual extern event HTMLTextContainerEvents_onmousedownEventHandler HTMLTextContainerEvents_Event_onmousedown;

		// Token: 0x14000AAF RID: 2735
		// (add) Token: 0x06006099 RID: 24729
		// (remove) Token: 0x0600609A RID: 24730
		public virtual extern event HTMLTextContainerEvents_onmouseupEventHandler HTMLTextContainerEvents_Event_onmouseup;

		// Token: 0x14000AB0 RID: 2736
		// (add) Token: 0x0600609B RID: 24731
		// (remove) Token: 0x0600609C RID: 24732
		public virtual extern event HTMLTextContainerEvents_onselectstartEventHandler HTMLTextContainerEvents_Event_onselectstart;

		// Token: 0x14000AB1 RID: 2737
		// (add) Token: 0x0600609D RID: 24733
		// (remove) Token: 0x0600609E RID: 24734
		public virtual extern event HTMLTextContainerEvents_onfilterchangeEventHandler HTMLTextContainerEvents_Event_onfilterchange;

		// Token: 0x14000AB2 RID: 2738
		// (add) Token: 0x0600609F RID: 24735
		// (remove) Token: 0x060060A0 RID: 24736
		public virtual extern event HTMLTextContainerEvents_ondragstartEventHandler HTMLTextContainerEvents_Event_ondragstart;

		// Token: 0x14000AB3 RID: 2739
		// (add) Token: 0x060060A1 RID: 24737
		// (remove) Token: 0x060060A2 RID: 24738
		public virtual extern event HTMLTextContainerEvents_onbeforeupdateEventHandler HTMLTextContainerEvents_Event_onbeforeupdate;

		// Token: 0x14000AB4 RID: 2740
		// (add) Token: 0x060060A3 RID: 24739
		// (remove) Token: 0x060060A4 RID: 24740
		public virtual extern event HTMLTextContainerEvents_onafterupdateEventHandler HTMLTextContainerEvents_Event_onafterupdate;

		// Token: 0x14000AB5 RID: 2741
		// (add) Token: 0x060060A5 RID: 24741
		// (remove) Token: 0x060060A6 RID: 24742
		public virtual extern event HTMLTextContainerEvents_onerrorupdateEventHandler HTMLTextContainerEvents_Event_onerrorupdate;

		// Token: 0x14000AB6 RID: 2742
		// (add) Token: 0x060060A7 RID: 24743
		// (remove) Token: 0x060060A8 RID: 24744
		public virtual extern event HTMLTextContainerEvents_onrowexitEventHandler HTMLTextContainerEvents_Event_onrowexit;

		// Token: 0x14000AB7 RID: 2743
		// (add) Token: 0x060060A9 RID: 24745
		// (remove) Token: 0x060060AA RID: 24746
		public virtual extern event HTMLTextContainerEvents_onrowenterEventHandler HTMLTextContainerEvents_Event_onrowenter;

		// Token: 0x14000AB8 RID: 2744
		// (add) Token: 0x060060AB RID: 24747
		// (remove) Token: 0x060060AC RID: 24748
		public virtual extern event HTMLTextContainerEvents_ondatasetchangedEventHandler HTMLTextContainerEvents_Event_ondatasetchanged;

		// Token: 0x14000AB9 RID: 2745
		// (add) Token: 0x060060AD RID: 24749
		// (remove) Token: 0x060060AE RID: 24750
		public virtual extern event HTMLTextContainerEvents_ondataavailableEventHandler HTMLTextContainerEvents_Event_ondataavailable;

		// Token: 0x14000ABA RID: 2746
		// (add) Token: 0x060060AF RID: 24751
		// (remove) Token: 0x060060B0 RID: 24752
		public virtual extern event HTMLTextContainerEvents_ondatasetcompleteEventHandler HTMLTextContainerEvents_Event_ondatasetcomplete;

		// Token: 0x14000ABB RID: 2747
		// (add) Token: 0x060060B1 RID: 24753
		// (remove) Token: 0x060060B2 RID: 24754
		public virtual extern event HTMLTextContainerEvents_onlosecaptureEventHandler HTMLTextContainerEvents_Event_onlosecapture;

		// Token: 0x14000ABC RID: 2748
		// (add) Token: 0x060060B3 RID: 24755
		// (remove) Token: 0x060060B4 RID: 24756
		public virtual extern event HTMLTextContainerEvents_onpropertychangeEventHandler HTMLTextContainerEvents_Event_onpropertychange;

		// Token: 0x14000ABD RID: 2749
		// (add) Token: 0x060060B5 RID: 24757
		// (remove) Token: 0x060060B6 RID: 24758
		public virtual extern event HTMLTextContainerEvents_onscrollEventHandler HTMLTextContainerEvents_Event_onscroll;

		// Token: 0x14000ABE RID: 2750
		// (add) Token: 0x060060B7 RID: 24759
		// (remove) Token: 0x060060B8 RID: 24760
		public virtual extern event HTMLTextContainerEvents_onfocusEventHandler HTMLTextContainerEvents_Event_onfocus;

		// Token: 0x14000ABF RID: 2751
		// (add) Token: 0x060060B9 RID: 24761
		// (remove) Token: 0x060060BA RID: 24762
		public virtual extern event HTMLTextContainerEvents_onblurEventHandler HTMLTextContainerEvents_Event_onblur;

		// Token: 0x14000AC0 RID: 2752
		// (add) Token: 0x060060BB RID: 24763
		// (remove) Token: 0x060060BC RID: 24764
		public virtual extern event HTMLTextContainerEvents_onresizeEventHandler HTMLTextContainerEvents_Event_onresize;

		// Token: 0x14000AC1 RID: 2753
		// (add) Token: 0x060060BD RID: 24765
		// (remove) Token: 0x060060BE RID: 24766
		public virtual extern event HTMLTextContainerEvents_ondragEventHandler HTMLTextContainerEvents_Event_ondrag;

		// Token: 0x14000AC2 RID: 2754
		// (add) Token: 0x060060BF RID: 24767
		// (remove) Token: 0x060060C0 RID: 24768
		public virtual extern event HTMLTextContainerEvents_ondragendEventHandler HTMLTextContainerEvents_Event_ondragend;

		// Token: 0x14000AC3 RID: 2755
		// (add) Token: 0x060060C1 RID: 24769
		// (remove) Token: 0x060060C2 RID: 24770
		public virtual extern event HTMLTextContainerEvents_ondragenterEventHandler HTMLTextContainerEvents_Event_ondragenter;

		// Token: 0x14000AC4 RID: 2756
		// (add) Token: 0x060060C3 RID: 24771
		// (remove) Token: 0x060060C4 RID: 24772
		public virtual extern event HTMLTextContainerEvents_ondragoverEventHandler HTMLTextContainerEvents_Event_ondragover;

		// Token: 0x14000AC5 RID: 2757
		// (add) Token: 0x060060C5 RID: 24773
		// (remove) Token: 0x060060C6 RID: 24774
		public virtual extern event HTMLTextContainerEvents_ondragleaveEventHandler HTMLTextContainerEvents_Event_ondragleave;

		// Token: 0x14000AC6 RID: 2758
		// (add) Token: 0x060060C7 RID: 24775
		// (remove) Token: 0x060060C8 RID: 24776
		public virtual extern event HTMLTextContainerEvents_ondropEventHandler HTMLTextContainerEvents_Event_ondrop;

		// Token: 0x14000AC7 RID: 2759
		// (add) Token: 0x060060C9 RID: 24777
		// (remove) Token: 0x060060CA RID: 24778
		public virtual extern event HTMLTextContainerEvents_onbeforecutEventHandler HTMLTextContainerEvents_Event_onbeforecut;

		// Token: 0x14000AC8 RID: 2760
		// (add) Token: 0x060060CB RID: 24779
		// (remove) Token: 0x060060CC RID: 24780
		public virtual extern event HTMLTextContainerEvents_oncutEventHandler HTMLTextContainerEvents_Event_oncut;

		// Token: 0x14000AC9 RID: 2761
		// (add) Token: 0x060060CD RID: 24781
		// (remove) Token: 0x060060CE RID: 24782
		public virtual extern event HTMLTextContainerEvents_onbeforecopyEventHandler HTMLTextContainerEvents_Event_onbeforecopy;

		// Token: 0x14000ACA RID: 2762
		// (add) Token: 0x060060CF RID: 24783
		// (remove) Token: 0x060060D0 RID: 24784
		public virtual extern event HTMLTextContainerEvents_oncopyEventHandler HTMLTextContainerEvents_Event_oncopy;

		// Token: 0x14000ACB RID: 2763
		// (add) Token: 0x060060D1 RID: 24785
		// (remove) Token: 0x060060D2 RID: 24786
		public virtual extern event HTMLTextContainerEvents_onbeforepasteEventHandler HTMLTextContainerEvents_Event_onbeforepaste;

		// Token: 0x14000ACC RID: 2764
		// (add) Token: 0x060060D3 RID: 24787
		// (remove) Token: 0x060060D4 RID: 24788
		public virtual extern event HTMLTextContainerEvents_onpasteEventHandler HTMLTextContainerEvents_Event_onpaste;

		// Token: 0x14000ACD RID: 2765
		// (add) Token: 0x060060D5 RID: 24789
		// (remove) Token: 0x060060D6 RID: 24790
		public virtual extern event HTMLTextContainerEvents_oncontextmenuEventHandler HTMLTextContainerEvents_Event_oncontextmenu;

		// Token: 0x14000ACE RID: 2766
		// (add) Token: 0x060060D7 RID: 24791
		// (remove) Token: 0x060060D8 RID: 24792
		public virtual extern event HTMLTextContainerEvents_onrowsdeleteEventHandler HTMLTextContainerEvents_Event_onrowsdelete;

		// Token: 0x14000ACF RID: 2767
		// (add) Token: 0x060060D9 RID: 24793
		// (remove) Token: 0x060060DA RID: 24794
		public virtual extern event HTMLTextContainerEvents_onrowsinsertedEventHandler HTMLTextContainerEvents_Event_onrowsinserted;

		// Token: 0x14000AD0 RID: 2768
		// (add) Token: 0x060060DB RID: 24795
		// (remove) Token: 0x060060DC RID: 24796
		public virtual extern event HTMLTextContainerEvents_oncellchangeEventHandler HTMLTextContainerEvents_Event_oncellchange;

		// Token: 0x14000AD1 RID: 2769
		// (add) Token: 0x060060DD RID: 24797
		// (remove) Token: 0x060060DE RID: 24798
		public virtual extern event HTMLTextContainerEvents_onreadystatechangeEventHandler HTMLTextContainerEvents_Event_onreadystatechange;

		// Token: 0x14000AD2 RID: 2770
		// (add) Token: 0x060060DF RID: 24799
		// (remove) Token: 0x060060E0 RID: 24800
		public virtual extern event HTMLTextContainerEvents_onbeforeeditfocusEventHandler HTMLTextContainerEvents_Event_onbeforeeditfocus;

		// Token: 0x14000AD3 RID: 2771
		// (add) Token: 0x060060E1 RID: 24801
		// (remove) Token: 0x060060E2 RID: 24802
		public virtual extern event HTMLTextContainerEvents_onlayoutcompleteEventHandler HTMLTextContainerEvents_Event_onlayoutcomplete;

		// Token: 0x14000AD4 RID: 2772
		// (add) Token: 0x060060E3 RID: 24803
		// (remove) Token: 0x060060E4 RID: 24804
		public virtual extern event HTMLTextContainerEvents_onpageEventHandler HTMLTextContainerEvents_Event_onpage;

		// Token: 0x14000AD5 RID: 2773
		// (add) Token: 0x060060E5 RID: 24805
		// (remove) Token: 0x060060E6 RID: 24806
		public virtual extern event HTMLTextContainerEvents_onbeforedeactivateEventHandler HTMLTextContainerEvents_Event_onbeforedeactivate;

		// Token: 0x14000AD6 RID: 2774
		// (add) Token: 0x060060E7 RID: 24807
		// (remove) Token: 0x060060E8 RID: 24808
		public virtual extern event HTMLTextContainerEvents_onbeforeactivateEventHandler HTMLTextContainerEvents_Event_onbeforeactivate;

		// Token: 0x14000AD7 RID: 2775
		// (add) Token: 0x060060E9 RID: 24809
		// (remove) Token: 0x060060EA RID: 24810
		public virtual extern event HTMLTextContainerEvents_onmoveEventHandler HTMLTextContainerEvents_Event_onmove;

		// Token: 0x14000AD8 RID: 2776
		// (add) Token: 0x060060EB RID: 24811
		// (remove) Token: 0x060060EC RID: 24812
		public virtual extern event HTMLTextContainerEvents_oncontrolselectEventHandler HTMLTextContainerEvents_Event_oncontrolselect;

		// Token: 0x14000AD9 RID: 2777
		// (add) Token: 0x060060ED RID: 24813
		// (remove) Token: 0x060060EE RID: 24814
		public virtual extern event HTMLTextContainerEvents_onmovestartEventHandler HTMLTextContainerEvents_Event_onmovestart;

		// Token: 0x14000ADA RID: 2778
		// (add) Token: 0x060060EF RID: 24815
		// (remove) Token: 0x060060F0 RID: 24816
		public virtual extern event HTMLTextContainerEvents_onmoveendEventHandler HTMLTextContainerEvents_Event_onmoveend;

		// Token: 0x14000ADB RID: 2779
		// (add) Token: 0x060060F1 RID: 24817
		// (remove) Token: 0x060060F2 RID: 24818
		public virtual extern event HTMLTextContainerEvents_onresizestartEventHandler HTMLTextContainerEvents_Event_onresizestart;

		// Token: 0x14000ADC RID: 2780
		// (add) Token: 0x060060F3 RID: 24819
		// (remove) Token: 0x060060F4 RID: 24820
		public virtual extern event HTMLTextContainerEvents_onresizeendEventHandler HTMLTextContainerEvents_Event_onresizeend;

		// Token: 0x14000ADD RID: 2781
		// (add) Token: 0x060060F5 RID: 24821
		// (remove) Token: 0x060060F6 RID: 24822
		public virtual extern event HTMLTextContainerEvents_onmouseenterEventHandler HTMLTextContainerEvents_Event_onmouseenter;

		// Token: 0x14000ADE RID: 2782
		// (add) Token: 0x060060F7 RID: 24823
		// (remove) Token: 0x060060F8 RID: 24824
		public virtual extern event HTMLTextContainerEvents_onmouseleaveEventHandler HTMLTextContainerEvents_Event_onmouseleave;

		// Token: 0x14000ADF RID: 2783
		// (add) Token: 0x060060F9 RID: 24825
		// (remove) Token: 0x060060FA RID: 24826
		public virtual extern event HTMLTextContainerEvents_onmousewheelEventHandler HTMLTextContainerEvents_Event_onmousewheel;

		// Token: 0x14000AE0 RID: 2784
		// (add) Token: 0x060060FB RID: 24827
		// (remove) Token: 0x060060FC RID: 24828
		public virtual extern event HTMLTextContainerEvents_onactivateEventHandler HTMLTextContainerEvents_Event_onactivate;

		// Token: 0x14000AE1 RID: 2785
		// (add) Token: 0x060060FD RID: 24829
		// (remove) Token: 0x060060FE RID: 24830
		public virtual extern event HTMLTextContainerEvents_ondeactivateEventHandler HTMLTextContainerEvents_Event_ondeactivate;

		// Token: 0x14000AE2 RID: 2786
		// (add) Token: 0x060060FF RID: 24831
		// (remove) Token: 0x06006100 RID: 24832
		public virtual extern event HTMLTextContainerEvents_onfocusinEventHandler HTMLTextContainerEvents_Event_onfocusin;

		// Token: 0x14000AE3 RID: 2787
		// (add) Token: 0x06006101 RID: 24833
		// (remove) Token: 0x06006102 RID: 24834
		public virtual extern event HTMLTextContainerEvents_onfocusoutEventHandler HTMLTextContainerEvents_Event_onfocusout;

		// Token: 0x14000AE4 RID: 2788
		// (add) Token: 0x06006103 RID: 24835
		// (remove) Token: 0x06006104 RID: 24836
		public virtual extern event HTMLTextContainerEvents_onchangeEventHandler onchange;

		// Token: 0x14000AE5 RID: 2789
		// (add) Token: 0x06006105 RID: 24837
		// (remove) Token: 0x06006106 RID: 24838
		public virtual extern event HTMLTextContainerEvents_onselectEventHandler onselect;

		// Token: 0x14000AE6 RID: 2790
		// (add) Token: 0x06006107 RID: 24839
		// (remove) Token: 0x06006108 RID: 24840
		public virtual extern event HTMLElementEvents2_onhelpEventHandler HTMLElementEvents2_Event_onhelp;

		// Token: 0x14000AE7 RID: 2791
		// (add) Token: 0x06006109 RID: 24841
		// (remove) Token: 0x0600610A RID: 24842
		public virtual extern event HTMLElementEvents2_onclickEventHandler HTMLElementEvents2_Event_onclick;

		// Token: 0x14000AE8 RID: 2792
		// (add) Token: 0x0600610B RID: 24843
		// (remove) Token: 0x0600610C RID: 24844
		public virtual extern event HTMLElementEvents2_ondblclickEventHandler HTMLElementEvents2_Event_ondblclick;

		// Token: 0x14000AE9 RID: 2793
		// (add) Token: 0x0600610D RID: 24845
		// (remove) Token: 0x0600610E RID: 24846
		public virtual extern event HTMLElementEvents2_onkeypressEventHandler HTMLElementEvents2_Event_onkeypress;

		// Token: 0x14000AEA RID: 2794
		// (add) Token: 0x0600610F RID: 24847
		// (remove) Token: 0x06006110 RID: 24848
		public virtual extern event HTMLElementEvents2_onkeydownEventHandler HTMLElementEvents2_Event_onkeydown;

		// Token: 0x14000AEB RID: 2795
		// (add) Token: 0x06006111 RID: 24849
		// (remove) Token: 0x06006112 RID: 24850
		public virtual extern event HTMLElementEvents2_onkeyupEventHandler HTMLElementEvents2_Event_onkeyup;

		// Token: 0x14000AEC RID: 2796
		// (add) Token: 0x06006113 RID: 24851
		// (remove) Token: 0x06006114 RID: 24852
		public virtual extern event HTMLElementEvents2_onmouseoutEventHandler HTMLElementEvents2_Event_onmouseout;

		// Token: 0x14000AED RID: 2797
		// (add) Token: 0x06006115 RID: 24853
		// (remove) Token: 0x06006116 RID: 24854
		public virtual extern event HTMLElementEvents2_onmouseoverEventHandler HTMLElementEvents2_Event_onmouseover;

		// Token: 0x14000AEE RID: 2798
		// (add) Token: 0x06006117 RID: 24855
		// (remove) Token: 0x06006118 RID: 24856
		public virtual extern event HTMLElementEvents2_onmousemoveEventHandler HTMLElementEvents2_Event_onmousemove;

		// Token: 0x14000AEF RID: 2799
		// (add) Token: 0x06006119 RID: 24857
		// (remove) Token: 0x0600611A RID: 24858
		public virtual extern event HTMLElementEvents2_onmousedownEventHandler HTMLElementEvents2_Event_onmousedown;

		// Token: 0x14000AF0 RID: 2800
		// (add) Token: 0x0600611B RID: 24859
		// (remove) Token: 0x0600611C RID: 24860
		public virtual extern event HTMLElementEvents2_onmouseupEventHandler HTMLElementEvents2_Event_onmouseup;

		// Token: 0x14000AF1 RID: 2801
		// (add) Token: 0x0600611D RID: 24861
		// (remove) Token: 0x0600611E RID: 24862
		public virtual extern event HTMLElementEvents2_onselectstartEventHandler HTMLElementEvents2_Event_onselectstart;

		// Token: 0x14000AF2 RID: 2802
		// (add) Token: 0x0600611F RID: 24863
		// (remove) Token: 0x06006120 RID: 24864
		public virtual extern event HTMLElementEvents2_onfilterchangeEventHandler HTMLElementEvents2_Event_onfilterchange;

		// Token: 0x14000AF3 RID: 2803
		// (add) Token: 0x06006121 RID: 24865
		// (remove) Token: 0x06006122 RID: 24866
		public virtual extern event HTMLElementEvents2_ondragstartEventHandler HTMLElementEvents2_Event_ondragstart;

		// Token: 0x14000AF4 RID: 2804
		// (add) Token: 0x06006123 RID: 24867
		// (remove) Token: 0x06006124 RID: 24868
		public virtual extern event HTMLElementEvents2_onbeforeupdateEventHandler HTMLElementEvents2_Event_onbeforeupdate;

		// Token: 0x14000AF5 RID: 2805
		// (add) Token: 0x06006125 RID: 24869
		// (remove) Token: 0x06006126 RID: 24870
		public virtual extern event HTMLElementEvents2_onafterupdateEventHandler HTMLElementEvents2_Event_onafterupdate;

		// Token: 0x14000AF6 RID: 2806
		// (add) Token: 0x06006127 RID: 24871
		// (remove) Token: 0x06006128 RID: 24872
		public virtual extern event HTMLElementEvents2_onerrorupdateEventHandler HTMLElementEvents2_Event_onerrorupdate;

		// Token: 0x14000AF7 RID: 2807
		// (add) Token: 0x06006129 RID: 24873
		// (remove) Token: 0x0600612A RID: 24874
		public virtual extern event HTMLElementEvents2_onrowexitEventHandler HTMLElementEvents2_Event_onrowexit;

		// Token: 0x14000AF8 RID: 2808
		// (add) Token: 0x0600612B RID: 24875
		// (remove) Token: 0x0600612C RID: 24876
		public virtual extern event HTMLElementEvents2_onrowenterEventHandler HTMLElementEvents2_Event_onrowenter;

		// Token: 0x14000AF9 RID: 2809
		// (add) Token: 0x0600612D RID: 24877
		// (remove) Token: 0x0600612E RID: 24878
		public virtual extern event HTMLElementEvents2_ondatasetchangedEventHandler HTMLElementEvents2_Event_ondatasetchanged;

		// Token: 0x14000AFA RID: 2810
		// (add) Token: 0x0600612F RID: 24879
		// (remove) Token: 0x06006130 RID: 24880
		public virtual extern event HTMLElementEvents2_ondataavailableEventHandler HTMLElementEvents2_Event_ondataavailable;

		// Token: 0x14000AFB RID: 2811
		// (add) Token: 0x06006131 RID: 24881
		// (remove) Token: 0x06006132 RID: 24882
		public virtual extern event HTMLElementEvents2_ondatasetcompleteEventHandler HTMLElementEvents2_Event_ondatasetcomplete;

		// Token: 0x14000AFC RID: 2812
		// (add) Token: 0x06006133 RID: 24883
		// (remove) Token: 0x06006134 RID: 24884
		public virtual extern event HTMLElementEvents2_onlosecaptureEventHandler HTMLElementEvents2_Event_onlosecapture;

		// Token: 0x14000AFD RID: 2813
		// (add) Token: 0x06006135 RID: 24885
		// (remove) Token: 0x06006136 RID: 24886
		public virtual extern event HTMLElementEvents2_onpropertychangeEventHandler HTMLElementEvents2_Event_onpropertychange;

		// Token: 0x14000AFE RID: 2814
		// (add) Token: 0x06006137 RID: 24887
		// (remove) Token: 0x06006138 RID: 24888
		public virtual extern event HTMLElementEvents2_onscrollEventHandler HTMLElementEvents2_Event_onscroll;

		// Token: 0x14000AFF RID: 2815
		// (add) Token: 0x06006139 RID: 24889
		// (remove) Token: 0x0600613A RID: 24890
		public virtual extern event HTMLElementEvents2_onfocusEventHandler HTMLElementEvents2_Event_onfocus;

		// Token: 0x14000B00 RID: 2816
		// (add) Token: 0x0600613B RID: 24891
		// (remove) Token: 0x0600613C RID: 24892
		public virtual extern event HTMLElementEvents2_onblurEventHandler HTMLElementEvents2_Event_onblur;

		// Token: 0x14000B01 RID: 2817
		// (add) Token: 0x0600613D RID: 24893
		// (remove) Token: 0x0600613E RID: 24894
		public virtual extern event HTMLElementEvents2_onresizeEventHandler HTMLElementEvents2_Event_onresize;

		// Token: 0x14000B02 RID: 2818
		// (add) Token: 0x0600613F RID: 24895
		// (remove) Token: 0x06006140 RID: 24896
		public virtual extern event HTMLElementEvents2_ondragEventHandler HTMLElementEvents2_Event_ondrag;

		// Token: 0x14000B03 RID: 2819
		// (add) Token: 0x06006141 RID: 24897
		// (remove) Token: 0x06006142 RID: 24898
		public virtual extern event HTMLElementEvents2_ondragendEventHandler HTMLElementEvents2_Event_ondragend;

		// Token: 0x14000B04 RID: 2820
		// (add) Token: 0x06006143 RID: 24899
		// (remove) Token: 0x06006144 RID: 24900
		public virtual extern event HTMLElementEvents2_ondragenterEventHandler HTMLElementEvents2_Event_ondragenter;

		// Token: 0x14000B05 RID: 2821
		// (add) Token: 0x06006145 RID: 24901
		// (remove) Token: 0x06006146 RID: 24902
		public virtual extern event HTMLElementEvents2_ondragoverEventHandler HTMLElementEvents2_Event_ondragover;

		// Token: 0x14000B06 RID: 2822
		// (add) Token: 0x06006147 RID: 24903
		// (remove) Token: 0x06006148 RID: 24904
		public virtual extern event HTMLElementEvents2_ondragleaveEventHandler HTMLElementEvents2_Event_ondragleave;

		// Token: 0x14000B07 RID: 2823
		// (add) Token: 0x06006149 RID: 24905
		// (remove) Token: 0x0600614A RID: 24906
		public virtual extern event HTMLElementEvents2_ondropEventHandler HTMLElementEvents2_Event_ondrop;

		// Token: 0x14000B08 RID: 2824
		// (add) Token: 0x0600614B RID: 24907
		// (remove) Token: 0x0600614C RID: 24908
		public virtual extern event HTMLElementEvents2_onbeforecutEventHandler HTMLElementEvents2_Event_onbeforecut;

		// Token: 0x14000B09 RID: 2825
		// (add) Token: 0x0600614D RID: 24909
		// (remove) Token: 0x0600614E RID: 24910
		public virtual extern event HTMLElementEvents2_oncutEventHandler HTMLElementEvents2_Event_oncut;

		// Token: 0x14000B0A RID: 2826
		// (add) Token: 0x0600614F RID: 24911
		// (remove) Token: 0x06006150 RID: 24912
		public virtual extern event HTMLElementEvents2_onbeforecopyEventHandler HTMLElementEvents2_Event_onbeforecopy;

		// Token: 0x14000B0B RID: 2827
		// (add) Token: 0x06006151 RID: 24913
		// (remove) Token: 0x06006152 RID: 24914
		public virtual extern event HTMLElementEvents2_oncopyEventHandler HTMLElementEvents2_Event_oncopy;

		// Token: 0x14000B0C RID: 2828
		// (add) Token: 0x06006153 RID: 24915
		// (remove) Token: 0x06006154 RID: 24916
		public virtual extern event HTMLElementEvents2_onbeforepasteEventHandler HTMLElementEvents2_Event_onbeforepaste;

		// Token: 0x14000B0D RID: 2829
		// (add) Token: 0x06006155 RID: 24917
		// (remove) Token: 0x06006156 RID: 24918
		public virtual extern event HTMLElementEvents2_onpasteEventHandler HTMLElementEvents2_Event_onpaste;

		// Token: 0x14000B0E RID: 2830
		// (add) Token: 0x06006157 RID: 24919
		// (remove) Token: 0x06006158 RID: 24920
		public virtual extern event HTMLElementEvents2_oncontextmenuEventHandler HTMLElementEvents2_Event_oncontextmenu;

		// Token: 0x14000B0F RID: 2831
		// (add) Token: 0x06006159 RID: 24921
		// (remove) Token: 0x0600615A RID: 24922
		public virtual extern event HTMLElementEvents2_onrowsdeleteEventHandler HTMLElementEvents2_Event_onrowsdelete;

		// Token: 0x14000B10 RID: 2832
		// (add) Token: 0x0600615B RID: 24923
		// (remove) Token: 0x0600615C RID: 24924
		public virtual extern event HTMLElementEvents2_onrowsinsertedEventHandler HTMLElementEvents2_Event_onrowsinserted;

		// Token: 0x14000B11 RID: 2833
		// (add) Token: 0x0600615D RID: 24925
		// (remove) Token: 0x0600615E RID: 24926
		public virtual extern event HTMLElementEvents2_oncellchangeEventHandler HTMLElementEvents2_Event_oncellchange;

		// Token: 0x14000B12 RID: 2834
		// (add) Token: 0x0600615F RID: 24927
		// (remove) Token: 0x06006160 RID: 24928
		public virtual extern event HTMLElementEvents2_onreadystatechangeEventHandler HTMLElementEvents2_Event_onreadystatechange;

		// Token: 0x14000B13 RID: 2835
		// (add) Token: 0x06006161 RID: 24929
		// (remove) Token: 0x06006162 RID: 24930
		public virtual extern event HTMLElementEvents2_onlayoutcompleteEventHandler HTMLElementEvents2_Event_onlayoutcomplete;

		// Token: 0x14000B14 RID: 2836
		// (add) Token: 0x06006163 RID: 24931
		// (remove) Token: 0x06006164 RID: 24932
		public virtual extern event HTMLElementEvents2_onpageEventHandler HTMLElementEvents2_Event_onpage;

		// Token: 0x14000B15 RID: 2837
		// (add) Token: 0x06006165 RID: 24933
		// (remove) Token: 0x06006166 RID: 24934
		public virtual extern event HTMLElementEvents2_onmouseenterEventHandler HTMLElementEvents2_Event_onmouseenter;

		// Token: 0x14000B16 RID: 2838
		// (add) Token: 0x06006167 RID: 24935
		// (remove) Token: 0x06006168 RID: 24936
		public virtual extern event HTMLElementEvents2_onmouseleaveEventHandler HTMLElementEvents2_Event_onmouseleave;

		// Token: 0x14000B17 RID: 2839
		// (add) Token: 0x06006169 RID: 24937
		// (remove) Token: 0x0600616A RID: 24938
		public virtual extern event HTMLElementEvents2_onactivateEventHandler HTMLElementEvents2_Event_onactivate;

		// Token: 0x14000B18 RID: 2840
		// (add) Token: 0x0600616B RID: 24939
		// (remove) Token: 0x0600616C RID: 24940
		public virtual extern event HTMLElementEvents2_ondeactivateEventHandler HTMLElementEvents2_Event_ondeactivate;

		// Token: 0x14000B19 RID: 2841
		// (add) Token: 0x0600616D RID: 24941
		// (remove) Token: 0x0600616E RID: 24942
		public virtual extern event HTMLElementEvents2_onbeforedeactivateEventHandler HTMLElementEvents2_Event_onbeforedeactivate;

		// Token: 0x14000B1A RID: 2842
		// (add) Token: 0x0600616F RID: 24943
		// (remove) Token: 0x06006170 RID: 24944
		public virtual extern event HTMLElementEvents2_onbeforeactivateEventHandler HTMLElementEvents2_Event_onbeforeactivate;

		// Token: 0x14000B1B RID: 2843
		// (add) Token: 0x06006171 RID: 24945
		// (remove) Token: 0x06006172 RID: 24946
		public virtual extern event HTMLElementEvents2_onfocusinEventHandler HTMLElementEvents2_Event_onfocusin;

		// Token: 0x14000B1C RID: 2844
		// (add) Token: 0x06006173 RID: 24947
		// (remove) Token: 0x06006174 RID: 24948
		public virtual extern event HTMLElementEvents2_onfocusoutEventHandler HTMLElementEvents2_Event_onfocusout;

		// Token: 0x14000B1D RID: 2845
		// (add) Token: 0x06006175 RID: 24949
		// (remove) Token: 0x06006176 RID: 24950
		public virtual extern event HTMLElementEvents2_onmoveEventHandler HTMLElementEvents2_Event_onmove;

		// Token: 0x14000B1E RID: 2846
		// (add) Token: 0x06006177 RID: 24951
		// (remove) Token: 0x06006178 RID: 24952
		public virtual extern event HTMLElementEvents2_oncontrolselectEventHandler HTMLElementEvents2_Event_oncontrolselect;

		// Token: 0x14000B1F RID: 2847
		// (add) Token: 0x06006179 RID: 24953
		// (remove) Token: 0x0600617A RID: 24954
		public virtual extern event HTMLElementEvents2_onmovestartEventHandler HTMLElementEvents2_Event_onmovestart;

		// Token: 0x14000B20 RID: 2848
		// (add) Token: 0x0600617B RID: 24955
		// (remove) Token: 0x0600617C RID: 24956
		public virtual extern event HTMLElementEvents2_onmoveendEventHandler HTMLElementEvents2_Event_onmoveend;

		// Token: 0x14000B21 RID: 2849
		// (add) Token: 0x0600617D RID: 24957
		// (remove) Token: 0x0600617E RID: 24958
		public virtual extern event HTMLElementEvents2_onresizestartEventHandler HTMLElementEvents2_Event_onresizestart;

		// Token: 0x14000B22 RID: 2850
		// (add) Token: 0x0600617F RID: 24959
		// (remove) Token: 0x06006180 RID: 24960
		public virtual extern event HTMLElementEvents2_onresizeendEventHandler HTMLElementEvents2_Event_onresizeend;

		// Token: 0x14000B23 RID: 2851
		// (add) Token: 0x06006181 RID: 24961
		// (remove) Token: 0x06006182 RID: 24962
		public virtual extern event HTMLElementEvents2_onmousewheelEventHandler HTMLElementEvents2_Event_onmousewheel;

		// Token: 0x14000B24 RID: 2852
		// (add) Token: 0x06006183 RID: 24963
		// (remove) Token: 0x06006184 RID: 24964
		public virtual extern event HTMLTextContainerEvents2_onhelpEventHandler HTMLTextContainerEvents2_Event_onhelp;

		// Token: 0x14000B25 RID: 2853
		// (add) Token: 0x06006185 RID: 24965
		// (remove) Token: 0x06006186 RID: 24966
		public virtual extern event HTMLTextContainerEvents2_onclickEventHandler HTMLTextContainerEvents2_Event_onclick;

		// Token: 0x14000B26 RID: 2854
		// (add) Token: 0x06006187 RID: 24967
		// (remove) Token: 0x06006188 RID: 24968
		public virtual extern event HTMLTextContainerEvents2_ondblclickEventHandler HTMLTextContainerEvents2_Event_ondblclick;

		// Token: 0x14000B27 RID: 2855
		// (add) Token: 0x06006189 RID: 24969
		// (remove) Token: 0x0600618A RID: 24970
		public virtual extern event HTMLTextContainerEvents2_onkeypressEventHandler HTMLTextContainerEvents2_Event_onkeypress;

		// Token: 0x14000B28 RID: 2856
		// (add) Token: 0x0600618B RID: 24971
		// (remove) Token: 0x0600618C RID: 24972
		public virtual extern event HTMLTextContainerEvents2_onkeydownEventHandler HTMLTextContainerEvents2_Event_onkeydown;

		// Token: 0x14000B29 RID: 2857
		// (add) Token: 0x0600618D RID: 24973
		// (remove) Token: 0x0600618E RID: 24974
		public virtual extern event HTMLTextContainerEvents2_onkeyupEventHandler HTMLTextContainerEvents2_Event_onkeyup;

		// Token: 0x14000B2A RID: 2858
		// (add) Token: 0x0600618F RID: 24975
		// (remove) Token: 0x06006190 RID: 24976
		public virtual extern event HTMLTextContainerEvents2_onmouseoutEventHandler HTMLTextContainerEvents2_Event_onmouseout;

		// Token: 0x14000B2B RID: 2859
		// (add) Token: 0x06006191 RID: 24977
		// (remove) Token: 0x06006192 RID: 24978
		public virtual extern event HTMLTextContainerEvents2_onmouseoverEventHandler HTMLTextContainerEvents2_Event_onmouseover;

		// Token: 0x14000B2C RID: 2860
		// (add) Token: 0x06006193 RID: 24979
		// (remove) Token: 0x06006194 RID: 24980
		public virtual extern event HTMLTextContainerEvents2_onmousemoveEventHandler HTMLTextContainerEvents2_Event_onmousemove;

		// Token: 0x14000B2D RID: 2861
		// (add) Token: 0x06006195 RID: 24981
		// (remove) Token: 0x06006196 RID: 24982
		public virtual extern event HTMLTextContainerEvents2_onmousedownEventHandler HTMLTextContainerEvents2_Event_onmousedown;

		// Token: 0x14000B2E RID: 2862
		// (add) Token: 0x06006197 RID: 24983
		// (remove) Token: 0x06006198 RID: 24984
		public virtual extern event HTMLTextContainerEvents2_onmouseupEventHandler HTMLTextContainerEvents2_Event_onmouseup;

		// Token: 0x14000B2F RID: 2863
		// (add) Token: 0x06006199 RID: 24985
		// (remove) Token: 0x0600619A RID: 24986
		public virtual extern event HTMLTextContainerEvents2_onselectstartEventHandler HTMLTextContainerEvents2_Event_onselectstart;

		// Token: 0x14000B30 RID: 2864
		// (add) Token: 0x0600619B RID: 24987
		// (remove) Token: 0x0600619C RID: 24988
		public virtual extern event HTMLTextContainerEvents2_onfilterchangeEventHandler HTMLTextContainerEvents2_Event_onfilterchange;

		// Token: 0x14000B31 RID: 2865
		// (add) Token: 0x0600619D RID: 24989
		// (remove) Token: 0x0600619E RID: 24990
		public virtual extern event HTMLTextContainerEvents2_ondragstartEventHandler HTMLTextContainerEvents2_Event_ondragstart;

		// Token: 0x14000B32 RID: 2866
		// (add) Token: 0x0600619F RID: 24991
		// (remove) Token: 0x060061A0 RID: 24992
		public virtual extern event HTMLTextContainerEvents2_onbeforeupdateEventHandler HTMLTextContainerEvents2_Event_onbeforeupdate;

		// Token: 0x14000B33 RID: 2867
		// (add) Token: 0x060061A1 RID: 24993
		// (remove) Token: 0x060061A2 RID: 24994
		public virtual extern event HTMLTextContainerEvents2_onafterupdateEventHandler HTMLTextContainerEvents2_Event_onafterupdate;

		// Token: 0x14000B34 RID: 2868
		// (add) Token: 0x060061A3 RID: 24995
		// (remove) Token: 0x060061A4 RID: 24996
		public virtual extern event HTMLTextContainerEvents2_onerrorupdateEventHandler HTMLTextContainerEvents2_Event_onerrorupdate;

		// Token: 0x14000B35 RID: 2869
		// (add) Token: 0x060061A5 RID: 24997
		// (remove) Token: 0x060061A6 RID: 24998
		public virtual extern event HTMLTextContainerEvents2_onrowexitEventHandler HTMLTextContainerEvents2_Event_onrowexit;

		// Token: 0x14000B36 RID: 2870
		// (add) Token: 0x060061A7 RID: 24999
		// (remove) Token: 0x060061A8 RID: 25000
		public virtual extern event HTMLTextContainerEvents2_onrowenterEventHandler HTMLTextContainerEvents2_Event_onrowenter;

		// Token: 0x14000B37 RID: 2871
		// (add) Token: 0x060061A9 RID: 25001
		// (remove) Token: 0x060061AA RID: 25002
		public virtual extern event HTMLTextContainerEvents2_ondatasetchangedEventHandler HTMLTextContainerEvents2_Event_ondatasetchanged;

		// Token: 0x14000B38 RID: 2872
		// (add) Token: 0x060061AB RID: 25003
		// (remove) Token: 0x060061AC RID: 25004
		public virtual extern event HTMLTextContainerEvents2_ondataavailableEventHandler HTMLTextContainerEvents2_Event_ondataavailable;

		// Token: 0x14000B39 RID: 2873
		// (add) Token: 0x060061AD RID: 25005
		// (remove) Token: 0x060061AE RID: 25006
		public virtual extern event HTMLTextContainerEvents2_ondatasetcompleteEventHandler HTMLTextContainerEvents2_Event_ondatasetcomplete;

		// Token: 0x14000B3A RID: 2874
		// (add) Token: 0x060061AF RID: 25007
		// (remove) Token: 0x060061B0 RID: 25008
		public virtual extern event HTMLTextContainerEvents2_onlosecaptureEventHandler HTMLTextContainerEvents2_Event_onlosecapture;

		// Token: 0x14000B3B RID: 2875
		// (add) Token: 0x060061B1 RID: 25009
		// (remove) Token: 0x060061B2 RID: 25010
		public virtual extern event HTMLTextContainerEvents2_onpropertychangeEventHandler HTMLTextContainerEvents2_Event_onpropertychange;

		// Token: 0x14000B3C RID: 2876
		// (add) Token: 0x060061B3 RID: 25011
		// (remove) Token: 0x060061B4 RID: 25012
		public virtual extern event HTMLTextContainerEvents2_onscrollEventHandler HTMLTextContainerEvents2_Event_onscroll;

		// Token: 0x14000B3D RID: 2877
		// (add) Token: 0x060061B5 RID: 25013
		// (remove) Token: 0x060061B6 RID: 25014
		public virtual extern event HTMLTextContainerEvents2_onfocusEventHandler HTMLTextContainerEvents2_Event_onfocus;

		// Token: 0x14000B3E RID: 2878
		// (add) Token: 0x060061B7 RID: 25015
		// (remove) Token: 0x060061B8 RID: 25016
		public virtual extern event HTMLTextContainerEvents2_onblurEventHandler HTMLTextContainerEvents2_Event_onblur;

		// Token: 0x14000B3F RID: 2879
		// (add) Token: 0x060061B9 RID: 25017
		// (remove) Token: 0x060061BA RID: 25018
		public virtual extern event HTMLTextContainerEvents2_onresizeEventHandler HTMLTextContainerEvents2_Event_onresize;

		// Token: 0x14000B40 RID: 2880
		// (add) Token: 0x060061BB RID: 25019
		// (remove) Token: 0x060061BC RID: 25020
		public virtual extern event HTMLTextContainerEvents2_ondragEventHandler HTMLTextContainerEvents2_Event_ondrag;

		// Token: 0x14000B41 RID: 2881
		// (add) Token: 0x060061BD RID: 25021
		// (remove) Token: 0x060061BE RID: 25022
		public virtual extern event HTMLTextContainerEvents2_ondragendEventHandler HTMLTextContainerEvents2_Event_ondragend;

		// Token: 0x14000B42 RID: 2882
		// (add) Token: 0x060061BF RID: 25023
		// (remove) Token: 0x060061C0 RID: 25024
		public virtual extern event HTMLTextContainerEvents2_ondragenterEventHandler HTMLTextContainerEvents2_Event_ondragenter;

		// Token: 0x14000B43 RID: 2883
		// (add) Token: 0x060061C1 RID: 25025
		// (remove) Token: 0x060061C2 RID: 25026
		public virtual extern event HTMLTextContainerEvents2_ondragoverEventHandler HTMLTextContainerEvents2_Event_ondragover;

		// Token: 0x14000B44 RID: 2884
		// (add) Token: 0x060061C3 RID: 25027
		// (remove) Token: 0x060061C4 RID: 25028
		public virtual extern event HTMLTextContainerEvents2_ondragleaveEventHandler HTMLTextContainerEvents2_Event_ondragleave;

		// Token: 0x14000B45 RID: 2885
		// (add) Token: 0x060061C5 RID: 25029
		// (remove) Token: 0x060061C6 RID: 25030
		public virtual extern event HTMLTextContainerEvents2_ondropEventHandler HTMLTextContainerEvents2_Event_ondrop;

		// Token: 0x14000B46 RID: 2886
		// (add) Token: 0x060061C7 RID: 25031
		// (remove) Token: 0x060061C8 RID: 25032
		public virtual extern event HTMLTextContainerEvents2_onbeforecutEventHandler HTMLTextContainerEvents2_Event_onbeforecut;

		// Token: 0x14000B47 RID: 2887
		// (add) Token: 0x060061C9 RID: 25033
		// (remove) Token: 0x060061CA RID: 25034
		public virtual extern event HTMLTextContainerEvents2_oncutEventHandler HTMLTextContainerEvents2_Event_oncut;

		// Token: 0x14000B48 RID: 2888
		// (add) Token: 0x060061CB RID: 25035
		// (remove) Token: 0x060061CC RID: 25036
		public virtual extern event HTMLTextContainerEvents2_onbeforecopyEventHandler HTMLTextContainerEvents2_Event_onbeforecopy;

		// Token: 0x14000B49 RID: 2889
		// (add) Token: 0x060061CD RID: 25037
		// (remove) Token: 0x060061CE RID: 25038
		public virtual extern event HTMLTextContainerEvents2_oncopyEventHandler HTMLTextContainerEvents2_Event_oncopy;

		// Token: 0x14000B4A RID: 2890
		// (add) Token: 0x060061CF RID: 25039
		// (remove) Token: 0x060061D0 RID: 25040
		public virtual extern event HTMLTextContainerEvents2_onbeforepasteEventHandler HTMLTextContainerEvents2_Event_onbeforepaste;

		// Token: 0x14000B4B RID: 2891
		// (add) Token: 0x060061D1 RID: 25041
		// (remove) Token: 0x060061D2 RID: 25042
		public virtual extern event HTMLTextContainerEvents2_onpasteEventHandler HTMLTextContainerEvents2_Event_onpaste;

		// Token: 0x14000B4C RID: 2892
		// (add) Token: 0x060061D3 RID: 25043
		// (remove) Token: 0x060061D4 RID: 25044
		public virtual extern event HTMLTextContainerEvents2_oncontextmenuEventHandler HTMLTextContainerEvents2_Event_oncontextmenu;

		// Token: 0x14000B4D RID: 2893
		// (add) Token: 0x060061D5 RID: 25045
		// (remove) Token: 0x060061D6 RID: 25046
		public virtual extern event HTMLTextContainerEvents2_onrowsdeleteEventHandler HTMLTextContainerEvents2_Event_onrowsdelete;

		// Token: 0x14000B4E RID: 2894
		// (add) Token: 0x060061D7 RID: 25047
		// (remove) Token: 0x060061D8 RID: 25048
		public virtual extern event HTMLTextContainerEvents2_onrowsinsertedEventHandler HTMLTextContainerEvents2_Event_onrowsinserted;

		// Token: 0x14000B4F RID: 2895
		// (add) Token: 0x060061D9 RID: 25049
		// (remove) Token: 0x060061DA RID: 25050
		public virtual extern event HTMLTextContainerEvents2_oncellchangeEventHandler HTMLTextContainerEvents2_Event_oncellchange;

		// Token: 0x14000B50 RID: 2896
		// (add) Token: 0x060061DB RID: 25051
		// (remove) Token: 0x060061DC RID: 25052
		public virtual extern event HTMLTextContainerEvents2_onreadystatechangeEventHandler HTMLTextContainerEvents2_Event_onreadystatechange;

		// Token: 0x14000B51 RID: 2897
		// (add) Token: 0x060061DD RID: 25053
		// (remove) Token: 0x060061DE RID: 25054
		public virtual extern event HTMLTextContainerEvents2_onlayoutcompleteEventHandler HTMLTextContainerEvents2_Event_onlayoutcomplete;

		// Token: 0x14000B52 RID: 2898
		// (add) Token: 0x060061DF RID: 25055
		// (remove) Token: 0x060061E0 RID: 25056
		public virtual extern event HTMLTextContainerEvents2_onpageEventHandler HTMLTextContainerEvents2_Event_onpage;

		// Token: 0x14000B53 RID: 2899
		// (add) Token: 0x060061E1 RID: 25057
		// (remove) Token: 0x060061E2 RID: 25058
		public virtual extern event HTMLTextContainerEvents2_onmouseenterEventHandler HTMLTextContainerEvents2_Event_onmouseenter;

		// Token: 0x14000B54 RID: 2900
		// (add) Token: 0x060061E3 RID: 25059
		// (remove) Token: 0x060061E4 RID: 25060
		public virtual extern event HTMLTextContainerEvents2_onmouseleaveEventHandler HTMLTextContainerEvents2_Event_onmouseleave;

		// Token: 0x14000B55 RID: 2901
		// (add) Token: 0x060061E5 RID: 25061
		// (remove) Token: 0x060061E6 RID: 25062
		public virtual extern event HTMLTextContainerEvents2_onactivateEventHandler HTMLTextContainerEvents2_Event_onactivate;

		// Token: 0x14000B56 RID: 2902
		// (add) Token: 0x060061E7 RID: 25063
		// (remove) Token: 0x060061E8 RID: 25064
		public virtual extern event HTMLTextContainerEvents2_ondeactivateEventHandler HTMLTextContainerEvents2_Event_ondeactivate;

		// Token: 0x14000B57 RID: 2903
		// (add) Token: 0x060061E9 RID: 25065
		// (remove) Token: 0x060061EA RID: 25066
		public virtual extern event HTMLTextContainerEvents2_onbeforedeactivateEventHandler HTMLTextContainerEvents2_Event_onbeforedeactivate;

		// Token: 0x14000B58 RID: 2904
		// (add) Token: 0x060061EB RID: 25067
		// (remove) Token: 0x060061EC RID: 25068
		public virtual extern event HTMLTextContainerEvents2_onbeforeactivateEventHandler HTMLTextContainerEvents2_Event_onbeforeactivate;

		// Token: 0x14000B59 RID: 2905
		// (add) Token: 0x060061ED RID: 25069
		// (remove) Token: 0x060061EE RID: 25070
		public virtual extern event HTMLTextContainerEvents2_onfocusinEventHandler HTMLTextContainerEvents2_Event_onfocusin;

		// Token: 0x14000B5A RID: 2906
		// (add) Token: 0x060061EF RID: 25071
		// (remove) Token: 0x060061F0 RID: 25072
		public virtual extern event HTMLTextContainerEvents2_onfocusoutEventHandler HTMLTextContainerEvents2_Event_onfocusout;

		// Token: 0x14000B5B RID: 2907
		// (add) Token: 0x060061F1 RID: 25073
		// (remove) Token: 0x060061F2 RID: 25074
		public virtual extern event HTMLTextContainerEvents2_onmoveEventHandler HTMLTextContainerEvents2_Event_onmove;

		// Token: 0x14000B5C RID: 2908
		// (add) Token: 0x060061F3 RID: 25075
		// (remove) Token: 0x060061F4 RID: 25076
		public virtual extern event HTMLTextContainerEvents2_oncontrolselectEventHandler HTMLTextContainerEvents2_Event_oncontrolselect;

		// Token: 0x14000B5D RID: 2909
		// (add) Token: 0x060061F5 RID: 25077
		// (remove) Token: 0x060061F6 RID: 25078
		public virtual extern event HTMLTextContainerEvents2_onmovestartEventHandler HTMLTextContainerEvents2_Event_onmovestart;

		// Token: 0x14000B5E RID: 2910
		// (add) Token: 0x060061F7 RID: 25079
		// (remove) Token: 0x060061F8 RID: 25080
		public virtual extern event HTMLTextContainerEvents2_onmoveendEventHandler HTMLTextContainerEvents2_Event_onmoveend;

		// Token: 0x14000B5F RID: 2911
		// (add) Token: 0x060061F9 RID: 25081
		// (remove) Token: 0x060061FA RID: 25082
		public virtual extern event HTMLTextContainerEvents2_onresizestartEventHandler HTMLTextContainerEvents2_Event_onresizestart;

		// Token: 0x14000B60 RID: 2912
		// (add) Token: 0x060061FB RID: 25083
		// (remove) Token: 0x060061FC RID: 25084
		public virtual extern event HTMLTextContainerEvents2_onresizeendEventHandler HTMLTextContainerEvents2_Event_onresizeend;

		// Token: 0x14000B61 RID: 2913
		// (add) Token: 0x060061FD RID: 25085
		// (remove) Token: 0x060061FE RID: 25086
		public virtual extern event HTMLTextContainerEvents2_onmousewheelEventHandler HTMLTextContainerEvents2_Event_onmousewheel;

		// Token: 0x14000B62 RID: 2914
		// (add) Token: 0x060061FF RID: 25087
		// (remove) Token: 0x06006200 RID: 25088
		public virtual extern event HTMLTextContainerEvents2_onchangeEventHandler HTMLTextContainerEvents2_Event_onchange;

		// Token: 0x14000B63 RID: 2915
		// (add) Token: 0x06006201 RID: 25089
		// (remove) Token: 0x06006202 RID: 25090
		public virtual extern event HTMLTextContainerEvents2_onselectEventHandler HTMLTextContainerEvents2_Event_onselect;
	}
}
