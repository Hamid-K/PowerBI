using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000B23 RID: 2851
	[ClassInterface(0)]
	[ComSourceInterfaces("mshtml.HTMLObjectElementEvents\0mshtml.HTMLObjectElementEvents2\0\0")]
	[Guid("3050F24E-98B5-11CF-BB82-00AA00BDCE0B")]
	[TypeLibType(2)]
	[ComImport]
	public class HTMLObjectElementClass : DispHTMLObjectElement, HTMLObjectElement, HTMLObjectElementEvents_Event, IHTMLElement, IHTMLElement2, IHTMLElement3, IHTMLElement4, IHTMLUniqueName, IHTMLDOMNode, IHTMLDOMNode2, IHTMLDatabinding, IHTMLControlElement, IHTMLObjectElement, IHTMLObjectElement2, IHTMLObjectElement3, HTMLObjectElementEvents2_Event
	{
		// Token: 0x060128AA RID: 75946
		[MethodImpl(4096, MethodCodeType = 3)]
		public extern HTMLObjectElementClass();

		// Token: 0x060128AB RID: 75947
		[DispId(-2147417611)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void setAttribute([MarshalAs(19)] [In] string strAttributeName, [MarshalAs(27)] [In] object AttributeValue, [In] int lFlags = 1);

		// Token: 0x060128AC RID: 75948
		[DispId(-2147417610)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		public virtual extern object getAttribute([MarshalAs(19)] [In] string strAttributeName, [In] int lFlags = 0);

		// Token: 0x060128AD RID: 75949
		[DispId(-2147417609)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool removeAttribute([MarshalAs(19)] [In] string strAttributeName, [In] int lFlags = 1);

		// Token: 0x170061FB RID: 25083
		// (get) Token: 0x060128AF RID: 75951
		// (set) Token: 0x060128AE RID: 75950
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

		// Token: 0x170061FC RID: 25084
		// (get) Token: 0x060128B1 RID: 75953
		// (set) Token: 0x060128B0 RID: 75952
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

		// Token: 0x170061FD RID: 25085
		// (get) Token: 0x060128B2 RID: 75954
		[DispId(-2147417108)]
		public virtual extern string tagName
		{
			[DispId(-2147417108)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170061FE RID: 25086
		// (get) Token: 0x060128B3 RID: 75955
		[DispId(-2147418104)]
		public virtual extern IHTMLElement parentElement
		{
			[DispId(-2147418104)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170061FF RID: 25087
		// (get) Token: 0x060128B4 RID: 75956
		[DispId(-2147418038)]
		public virtual extern IHTMLStyle style
		{
			[TypeLibFunc(1024)]
			[DispId(-2147418038)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17006200 RID: 25088
		// (get) Token: 0x060128B6 RID: 75958
		// (set) Token: 0x060128B5 RID: 75957
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

		// Token: 0x17006201 RID: 25089
		// (get) Token: 0x060128B8 RID: 75960
		// (set) Token: 0x060128B7 RID: 75959
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

		// Token: 0x17006202 RID: 25090
		// (get) Token: 0x060128BA RID: 75962
		// (set) Token: 0x060128B9 RID: 75961
		[DispId(-2147412103)]
		public virtual extern object ondblclick
		{
			[DispId(-2147412103)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412103)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17006203 RID: 25091
		// (get) Token: 0x060128BC RID: 75964
		// (set) Token: 0x060128BB RID: 75963
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

		// Token: 0x17006204 RID: 25092
		// (get) Token: 0x060128BE RID: 75966
		// (set) Token: 0x060128BD RID: 75965
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

		// Token: 0x17006205 RID: 25093
		// (get) Token: 0x060128C0 RID: 75968
		// (set) Token: 0x060128BF RID: 75967
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

		// Token: 0x17006206 RID: 25094
		// (get) Token: 0x060128C2 RID: 75970
		// (set) Token: 0x060128C1 RID: 75969
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

		// Token: 0x17006207 RID: 25095
		// (get) Token: 0x060128C4 RID: 75972
		// (set) Token: 0x060128C3 RID: 75971
		[DispId(-2147412112)]
		public virtual extern object onmouseover
		{
			[DispId(-2147412112)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412112)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17006208 RID: 25096
		// (get) Token: 0x060128C6 RID: 75974
		// (set) Token: 0x060128C5 RID: 75973
		[DispId(-2147412108)]
		public virtual extern object onmousemove
		{
			[DispId(-2147412108)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412108)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17006209 RID: 25097
		// (get) Token: 0x060128C8 RID: 75976
		// (set) Token: 0x060128C7 RID: 75975
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

		// Token: 0x1700620A RID: 25098
		// (get) Token: 0x060128CA RID: 75978
		// (set) Token: 0x060128C9 RID: 75977
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

		// Token: 0x1700620B RID: 25099
		// (get) Token: 0x060128CB RID: 75979
		[DispId(-2147417094)]
		public virtual extern object document
		{
			[DispId(-2147417094)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x1700620C RID: 25100
		// (get) Token: 0x060128CD RID: 75981
		// (set) Token: 0x060128CC RID: 75980
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

		// Token: 0x1700620D RID: 25101
		// (get) Token: 0x060128CF RID: 75983
		// (set) Token: 0x060128CE RID: 75982
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

		// Token: 0x1700620E RID: 25102
		// (get) Token: 0x060128D1 RID: 75985
		// (set) Token: 0x060128D0 RID: 75984
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

		// Token: 0x060128D2 RID: 75986
		[DispId(-2147417093)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void scrollIntoView([MarshalAs(27)] [In] [Optional] object varargStart);

		// Token: 0x060128D3 RID: 75987
		[DispId(-2147417092)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool contains([MarshalAs(28)] [In] IHTMLElement pChild);

		// Token: 0x1700620F RID: 25103
		// (get) Token: 0x060128D4 RID: 75988
		[DispId(-2147417088)]
		public virtual extern int sourceIndex
		{
			[DispId(-2147417088)]
			[TypeLibFunc(4)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17006210 RID: 25104
		// (get) Token: 0x060128D5 RID: 75989
		[DispId(-2147417087)]
		public virtual extern object recordNumber
		{
			[DispId(-2147417087)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x17006211 RID: 25105
		// (get) Token: 0x060128D7 RID: 75991
		// (set) Token: 0x060128D6 RID: 75990
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

		// Token: 0x17006212 RID: 25106
		// (get) Token: 0x060128D8 RID: 75992
		[DispId(-2147417104)]
		public virtual extern int offsetLeft
		{
			[DispId(-2147417104)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17006213 RID: 25107
		// (get) Token: 0x060128D9 RID: 75993
		[DispId(-2147417103)]
		public virtual extern int offsetTop
		{
			[DispId(-2147417103)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17006214 RID: 25108
		// (get) Token: 0x060128DA RID: 75994
		[DispId(-2147417102)]
		public virtual extern int offsetWidth
		{
			[DispId(-2147417102)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17006215 RID: 25109
		// (get) Token: 0x060128DB RID: 75995
		[DispId(-2147417101)]
		public virtual extern int offsetHeight
		{
			[DispId(-2147417101)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17006216 RID: 25110
		// (get) Token: 0x060128DC RID: 75996
		[DispId(-2147417100)]
		public virtual extern IHTMLElement offsetParent
		{
			[DispId(-2147417100)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17006217 RID: 25111
		// (get) Token: 0x060128DE RID: 75998
		// (set) Token: 0x060128DD RID: 75997
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

		// Token: 0x17006218 RID: 25112
		// (get) Token: 0x060128E0 RID: 76000
		// (set) Token: 0x060128DF RID: 75999
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

		// Token: 0x17006219 RID: 25113
		// (get) Token: 0x060128E2 RID: 76002
		// (set) Token: 0x060128E1 RID: 76001
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

		// Token: 0x1700621A RID: 25114
		// (get) Token: 0x060128E4 RID: 76004
		// (set) Token: 0x060128E3 RID: 76003
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

		// Token: 0x060128E5 RID: 76005
		[DispId(-2147417082)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void insertAdjacentHTML([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string html);

		// Token: 0x060128E6 RID: 76006
		[DispId(-2147417081)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void insertAdjacentText([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string text);

		// Token: 0x1700621B RID: 25115
		// (get) Token: 0x060128E7 RID: 76007
		[DispId(-2147417080)]
		public virtual extern IHTMLElement parentTextEdit
		{
			[DispId(-2147417080)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x1700621C RID: 25116
		// (get) Token: 0x060128E8 RID: 76008
		[DispId(-2147417078)]
		public virtual extern bool isTextEdit
		{
			[DispId(-2147417078)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x060128E9 RID: 76009
		[DispId(-2147417079)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void click();

		// Token: 0x1700621D RID: 25117
		// (get) Token: 0x060128EA RID: 76010
		[DispId(-2147417077)]
		public virtual extern IHTMLFiltersCollection filters
		{
			[DispId(-2147417077)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x1700621E RID: 25118
		// (get) Token: 0x060128EC RID: 76012
		// (set) Token: 0x060128EB RID: 76011
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

		// Token: 0x060128ED RID: 76013
		[DispId(-2147417076)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string toString();

		// Token: 0x1700621F RID: 25119
		// (get) Token: 0x060128EF RID: 76015
		// (set) Token: 0x060128EE RID: 76014
		[DispId(-2147412091)]
		public virtual extern object onbeforeupdate
		{
			[DispId(-2147412091)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412091)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17006220 RID: 25120
		// (get) Token: 0x060128F1 RID: 76017
		// (set) Token: 0x060128F0 RID: 76016
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

		// Token: 0x17006221 RID: 25121
		// (get) Token: 0x060128F3 RID: 76019
		// (set) Token: 0x060128F2 RID: 76018
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

		// Token: 0x17006222 RID: 25122
		// (get) Token: 0x060128F5 RID: 76021
		// (set) Token: 0x060128F4 RID: 76020
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

		// Token: 0x17006223 RID: 25123
		// (get) Token: 0x060128F7 RID: 76023
		// (set) Token: 0x060128F6 RID: 76022
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

		// Token: 0x17006224 RID: 25124
		// (get) Token: 0x060128F9 RID: 76025
		// (set) Token: 0x060128F8 RID: 76024
		[DispId(-2147412072)]
		public virtual extern object ondatasetchanged
		{
			[TypeLibFunc(20)]
			[DispId(-2147412072)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412072)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17006225 RID: 25125
		// (get) Token: 0x060128FB RID: 76027
		// (set) Token: 0x060128FA RID: 76026
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

		// Token: 0x17006226 RID: 25126
		// (get) Token: 0x060128FD RID: 76029
		// (set) Token: 0x060128FC RID: 76028
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

		// Token: 0x17006227 RID: 25127
		// (get) Token: 0x060128FF RID: 76031
		// (set) Token: 0x060128FE RID: 76030
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

		// Token: 0x17006228 RID: 25128
		// (get) Token: 0x06012900 RID: 76032
		[DispId(-2147417075)]
		public virtual extern object children
		{
			[DispId(-2147417075)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17006229 RID: 25129
		// (get) Token: 0x06012901 RID: 76033
		[DispId(-2147417074)]
		public virtual extern object all
		{
			[DispId(-2147417074)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x1700622A RID: 25130
		// (get) Token: 0x06012902 RID: 76034
		[DispId(-2147417073)]
		public virtual extern string scopeName
		{
			[DispId(-2147417073)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x06012903 RID: 76035
		[DispId(-2147417072)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void setCapture([In] bool containerCapture = true);

		// Token: 0x06012904 RID: 76036
		[DispId(-2147417071)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void releaseCapture();

		// Token: 0x1700622B RID: 25131
		// (get) Token: 0x06012906 RID: 76038
		// (set) Token: 0x06012905 RID: 76037
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

		// Token: 0x06012907 RID: 76039
		[DispId(-2147417070)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string componentFromPoint([In] int x, [In] int y);

		// Token: 0x06012908 RID: 76040
		[DispId(-2147417069)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void doScroll([MarshalAs(27)] [In] [Optional] object component);

		// Token: 0x1700622C RID: 25132
		// (get) Token: 0x0601290A RID: 76042
		// (set) Token: 0x06012909 RID: 76041
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

		// Token: 0x1700622D RID: 25133
		// (get) Token: 0x0601290C RID: 76044
		// (set) Token: 0x0601290B RID: 76043
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

		// Token: 0x1700622E RID: 25134
		// (get) Token: 0x0601290E RID: 76046
		// (set) Token: 0x0601290D RID: 76045
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

		// Token: 0x1700622F RID: 25135
		// (get) Token: 0x06012910 RID: 76048
		// (set) Token: 0x0601290F RID: 76047
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

		// Token: 0x17006230 RID: 25136
		// (get) Token: 0x06012912 RID: 76050
		// (set) Token: 0x06012911 RID: 76049
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

		// Token: 0x17006231 RID: 25137
		// (get) Token: 0x06012914 RID: 76052
		// (set) Token: 0x06012913 RID: 76051
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

		// Token: 0x17006232 RID: 25138
		// (get) Token: 0x06012916 RID: 76054
		// (set) Token: 0x06012915 RID: 76053
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

		// Token: 0x17006233 RID: 25139
		// (get) Token: 0x06012918 RID: 76056
		// (set) Token: 0x06012917 RID: 76055
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

		// Token: 0x17006234 RID: 25140
		// (get) Token: 0x0601291A RID: 76058
		// (set) Token: 0x06012919 RID: 76057
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

		// Token: 0x17006235 RID: 25141
		// (get) Token: 0x0601291C RID: 76060
		// (set) Token: 0x0601291B RID: 76059
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

		// Token: 0x17006236 RID: 25142
		// (get) Token: 0x0601291E RID: 76062
		// (set) Token: 0x0601291D RID: 76061
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

		// Token: 0x17006237 RID: 25143
		// (get) Token: 0x06012920 RID: 76064
		// (set) Token: 0x0601291F RID: 76063
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

		// Token: 0x17006238 RID: 25144
		// (get) Token: 0x06012922 RID: 76066
		// (set) Token: 0x06012921 RID: 76065
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

		// Token: 0x17006239 RID: 25145
		// (get) Token: 0x06012923 RID: 76067
		[DispId(-2147417105)]
		public virtual extern IHTMLCurrentStyle currentStyle
		{
			[DispId(-2147417105)]
			[TypeLibFunc(1024)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x1700623A RID: 25146
		// (get) Token: 0x06012925 RID: 76069
		// (set) Token: 0x06012924 RID: 76068
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

		// Token: 0x06012926 RID: 76070
		[DispId(-2147417068)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLRectCollection getClientRects();

		// Token: 0x06012927 RID: 76071
		[DispId(-2147417067)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLRect getBoundingClientRect();

		// Token: 0x06012928 RID: 76072
		[DispId(-2147417608)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void setExpression([MarshalAs(19)] [In] string propname, [MarshalAs(19)] [In] string expression, [MarshalAs(19)] [In] string language = "");

		// Token: 0x06012929 RID: 76073
		[DispId(-2147417607)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		public virtual extern object getExpression([MarshalAs(19)] [In] string propname);

		// Token: 0x0601292A RID: 76074
		[DispId(-2147417606)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool removeExpression([MarshalAs(19)] [In] string propname);

		// Token: 0x1700623B RID: 25147
		// (get) Token: 0x0601292C RID: 76076
		// (set) Token: 0x0601292B RID: 76075
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

		// Token: 0x0601292D RID: 76077
		[DispId(-2147416112)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void focus();

		// Token: 0x1700623C RID: 25148
		// (get) Token: 0x0601292F RID: 76079
		// (set) Token: 0x0601292E RID: 76078
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

		// Token: 0x1700623D RID: 25149
		// (get) Token: 0x06012931 RID: 76081
		// (set) Token: 0x06012930 RID: 76080
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

		// Token: 0x1700623E RID: 25150
		// (get) Token: 0x06012933 RID: 76083
		// (set) Token: 0x06012932 RID: 76082
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

		// Token: 0x1700623F RID: 25151
		// (get) Token: 0x06012935 RID: 76085
		// (set) Token: 0x06012934 RID: 76084
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

		// Token: 0x06012936 RID: 76086
		[DispId(-2147416110)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void blur();

		// Token: 0x06012937 RID: 76087
		[DispId(-2147416095)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void addFilter([MarshalAs(25)] [In] object pUnk);

		// Token: 0x06012938 RID: 76088
		[DispId(-2147416094)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void removeFilter([MarshalAs(25)] [In] object pUnk);

		// Token: 0x17006240 RID: 25152
		// (get) Token: 0x06012939 RID: 76089
		[DispId(-2147416093)]
		public virtual extern int clientHeight
		{
			[DispId(-2147416093)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17006241 RID: 25153
		// (get) Token: 0x0601293A RID: 76090
		[DispId(-2147416092)]
		public virtual extern int clientWidth
		{
			[TypeLibFunc(20)]
			[DispId(-2147416092)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17006242 RID: 25154
		// (get) Token: 0x0601293B RID: 76091
		[DispId(-2147416091)]
		public virtual extern int clientTop
		{
			[DispId(-2147416091)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17006243 RID: 25155
		// (get) Token: 0x0601293C RID: 76092
		[DispId(-2147416090)]
		public virtual extern int clientLeft
		{
			[TypeLibFunc(20)]
			[DispId(-2147416090)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x0601293D RID: 76093
		[DispId(-2147417605)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool attachEvent([MarshalAs(19)] [In] string @event, [MarshalAs(26)] [In] object pdisp);

		// Token: 0x0601293E RID: 76094
		[DispId(-2147417604)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void detachEvent([MarshalAs(19)] [In] string @event, [MarshalAs(26)] [In] object pdisp);

		// Token: 0x17006244 RID: 25156
		// (get) Token: 0x0601293F RID: 76095
		[DispId(-2147412996)]
		public virtual extern object readyState
		{
			[DispId(-2147412996)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x17006245 RID: 25157
		// (get) Token: 0x06012941 RID: 76097
		// (set) Token: 0x06012940 RID: 76096
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

		// Token: 0x17006246 RID: 25158
		// (get) Token: 0x06012943 RID: 76099
		// (set) Token: 0x06012942 RID: 76098
		[DispId(-2147412050)]
		public virtual extern object onrowsdelete
		{
			[DispId(-2147412050)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412050)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17006247 RID: 25159
		// (get) Token: 0x06012945 RID: 76101
		// (set) Token: 0x06012944 RID: 76100
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

		// Token: 0x17006248 RID: 25160
		// (get) Token: 0x06012947 RID: 76103
		// (set) Token: 0x06012946 RID: 76102
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

		// Token: 0x17006249 RID: 25161
		// (get) Token: 0x06012949 RID: 76105
		// (set) Token: 0x06012948 RID: 76104
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

		// Token: 0x0601294A RID: 76106
		[DispId(-2147417056)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(26)]
		public virtual extern object createControlRange();

		// Token: 0x1700624A RID: 25162
		// (get) Token: 0x0601294B RID: 76107
		[DispId(-2147417055)]
		public virtual extern int scrollHeight
		{
			[DispId(-2147417055)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700624B RID: 25163
		// (get) Token: 0x0601294C RID: 76108
		[DispId(-2147417054)]
		public virtual extern int scrollWidth
		{
			[DispId(-2147417054)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700624C RID: 25164
		// (get) Token: 0x0601294E RID: 76110
		// (set) Token: 0x0601294D RID: 76109
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

		// Token: 0x1700624D RID: 25165
		// (get) Token: 0x06012950 RID: 76112
		// (set) Token: 0x0601294F RID: 76111
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

		// Token: 0x06012951 RID: 76113
		[DispId(-2147417050)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void clearAttributes();

		// Token: 0x1700624E RID: 25166
		// (get) Token: 0x06012953 RID: 76115
		// (set) Token: 0x06012952 RID: 76114
		[DispId(-2147412047)]
		public virtual extern object oncontextmenu
		{
			[TypeLibFunc(20)]
			[DispId(-2147412047)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412047)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x06012954 RID: 76116
		[DispId(-2147417043)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElement insertAdjacentElement([MarshalAs(19)] [In] string where, [MarshalAs(28)] [In] IHTMLElement insertedElement);

		// Token: 0x06012955 RID: 76117
		[DispId(-2147417047)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElement applyElement([MarshalAs(28)] [In] IHTMLElement apply, [MarshalAs(19)] [In] string where);

		// Token: 0x06012956 RID: 76118
		[DispId(-2147417042)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string getAdjacentText([MarshalAs(19)] [In] string where);

		// Token: 0x06012957 RID: 76119
		[DispId(-2147417041)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string replaceAdjacentText([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string newText);

		// Token: 0x1700624F RID: 25167
		// (get) Token: 0x06012958 RID: 76120
		[DispId(-2147417040)]
		public virtual extern bool canHaveChildren
		{
			[DispId(-2147417040)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x06012959 RID: 76121
		[DispId(-2147417032)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern int addBehavior([MarshalAs(19)] [In] string bstrUrl, [MarshalAs(27)] [In] [Optional] ref object pvarFactory);

		// Token: 0x0601295A RID: 76122
		[DispId(-2147417031)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool removeBehavior([In] int cookie);

		// Token: 0x17006250 RID: 25168
		// (get) Token: 0x0601295B RID: 76123
		[DispId(-2147417048)]
		public virtual extern IHTMLStyle runtimeStyle
		{
			[TypeLibFunc(1024)]
			[DispId(-2147417048)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17006251 RID: 25169
		// (get) Token: 0x0601295C RID: 76124
		[DispId(-2147417030)]
		public virtual extern object behaviorUrns
		{
			[DispId(-2147417030)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17006252 RID: 25170
		// (get) Token: 0x0601295E RID: 76126
		// (set) Token: 0x0601295D RID: 76125
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

		// Token: 0x17006253 RID: 25171
		// (get) Token: 0x06012960 RID: 76128
		// (set) Token: 0x0601295F RID: 76127
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

		// Token: 0x17006254 RID: 25172
		// (get) Token: 0x06012961 RID: 76129
		[DispId(-2147417028)]
		public virtual extern int readyStateValue
		{
			[DispId(-2147417028)]
			[TypeLibFunc(65)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x06012962 RID: 76130
		[DispId(-2147417027)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElementCollection getElementsByTagName([MarshalAs(19)] [In] string v);

		// Token: 0x06012963 RID: 76131
		[DispId(-2147417016)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void mergeAttributes([MarshalAs(28)] [In] IHTMLElement mergeThis, [MarshalAs(27)] [In] [Optional] ref object pvarFlags);

		// Token: 0x17006255 RID: 25173
		// (get) Token: 0x06012964 RID: 76132
		[DispId(-2147417015)]
		public virtual extern bool isMultiLine
		{
			[DispId(-2147417015)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17006256 RID: 25174
		// (get) Token: 0x06012965 RID: 76133
		[DispId(-2147417014)]
		public virtual extern bool canHaveHTML
		{
			[DispId(-2147417014)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17006257 RID: 25175
		// (get) Token: 0x06012967 RID: 76135
		// (set) Token: 0x06012966 RID: 76134
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

		// Token: 0x17006258 RID: 25176
		// (get) Token: 0x06012969 RID: 76137
		// (set) Token: 0x06012968 RID: 76136
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

		// Token: 0x17006259 RID: 25177
		// (get) Token: 0x0601296B RID: 76139
		// (set) Token: 0x0601296A RID: 76138
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

		// Token: 0x1700625A RID: 25178
		// (get) Token: 0x0601296D RID: 76141
		// (set) Token: 0x0601296C RID: 76140
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

		// Token: 0x0601296E RID: 76142
		[DispId(-2147417011)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void setActive();

		// Token: 0x1700625B RID: 25179
		// (get) Token: 0x06012970 RID: 76144
		// (set) Token: 0x0601296F RID: 76143
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

		// Token: 0x1700625C RID: 25180
		// (get) Token: 0x06012971 RID: 76145
		[DispId(-2147417010)]
		public virtual extern bool isContentEditable
		{
			[DispId(-2147417010)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700625D RID: 25181
		// (get) Token: 0x06012973 RID: 76147
		// (set) Token: 0x06012972 RID: 76146
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

		// Token: 0x1700625E RID: 25182
		// (get) Token: 0x06012975 RID: 76149
		// (set) Token: 0x06012974 RID: 76148
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

		// Token: 0x1700625F RID: 25183
		// (get) Token: 0x06012976 RID: 76150
		[DispId(-2147417007)]
		public virtual extern bool isDisabled
		{
			[DispId(-2147417007)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17006260 RID: 25184
		// (get) Token: 0x06012978 RID: 76152
		// (set) Token: 0x06012977 RID: 76151
		[DispId(-2147412034)]
		public virtual extern object onmove
		{
			[DispId(-2147412034)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412034)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17006261 RID: 25185
		// (get) Token: 0x0601297A RID: 76154
		// (set) Token: 0x06012979 RID: 76153
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

		// Token: 0x0601297B RID: 76155
		[DispId(-2147417006)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool FireEvent([MarshalAs(19)] [In] string bstrEventName, [MarshalAs(27)] [In] [Optional] ref object pvarEventObject);

		// Token: 0x17006262 RID: 25186
		// (get) Token: 0x0601297D RID: 76157
		// (set) Token: 0x0601297C RID: 76156
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

		// Token: 0x17006263 RID: 25187
		// (get) Token: 0x0601297F RID: 76159
		// (set) Token: 0x0601297E RID: 76158
		[DispId(-2147412028)]
		public virtual extern object onresizeend
		{
			[DispId(-2147412028)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412028)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17006264 RID: 25188
		// (get) Token: 0x06012981 RID: 76161
		// (set) Token: 0x06012980 RID: 76160
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

		// Token: 0x17006265 RID: 25189
		// (get) Token: 0x06012983 RID: 76163
		// (set) Token: 0x06012982 RID: 76162
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

		// Token: 0x17006266 RID: 25190
		// (get) Token: 0x06012985 RID: 76165
		// (set) Token: 0x06012984 RID: 76164
		[DispId(-2147412027)]
		public virtual extern object onmouseenter
		{
			[TypeLibFunc(20)]
			[DispId(-2147412027)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412027)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17006267 RID: 25191
		// (get) Token: 0x06012987 RID: 76167
		// (set) Token: 0x06012986 RID: 76166
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

		// Token: 0x17006268 RID: 25192
		// (get) Token: 0x06012989 RID: 76169
		// (set) Token: 0x06012988 RID: 76168
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

		// Token: 0x17006269 RID: 25193
		// (get) Token: 0x0601298B RID: 76171
		// (set) Token: 0x0601298A RID: 76170
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

		// Token: 0x0601298C RID: 76172
		[DispId(-2147417005)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool dragDrop();

		// Token: 0x1700626A RID: 25194
		// (get) Token: 0x0601298D RID: 76173
		[DispId(-2147417004)]
		public virtual extern int glyphMode
		{
			[DispId(-2147417004)]
			[TypeLibFunc(1089)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700626B RID: 25195
		// (get) Token: 0x0601298F RID: 76175
		// (set) Token: 0x0601298E RID: 76174
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

		// Token: 0x06012990 RID: 76176
		[DispId(-2147417000)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void normalize();

		// Token: 0x06012991 RID: 76177
		[DispId(-2147417003)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute getAttributeNode([MarshalAs(19)] [In] string bstrName);

		// Token: 0x06012992 RID: 76178
		[DispId(-2147417002)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute setAttributeNode([MarshalAs(28)] [In] IHTMLDOMAttribute pattr);

		// Token: 0x06012993 RID: 76179
		[DispId(-2147417001)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute removeAttributeNode([MarshalAs(28)] [In] IHTMLDOMAttribute pattr);

		// Token: 0x1700626C RID: 25196
		// (get) Token: 0x06012995 RID: 76181
		// (set) Token: 0x06012994 RID: 76180
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

		// Token: 0x1700626D RID: 25197
		// (get) Token: 0x06012997 RID: 76183
		// (set) Token: 0x06012996 RID: 76182
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

		// Token: 0x1700626E RID: 25198
		// (get) Token: 0x06012999 RID: 76185
		// (set) Token: 0x06012998 RID: 76184
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

		// Token: 0x1700626F RID: 25199
		// (get) Token: 0x0601299A RID: 76186
		[DispId(-2147417058)]
		public virtual extern int uniqueNumber
		{
			[DispId(-2147417058)]
			[TypeLibFunc(64)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17006270 RID: 25200
		// (get) Token: 0x0601299B RID: 76187
		[DispId(-2147417057)]
		public virtual extern string uniqueID
		{
			[DispId(-2147417057)]
			[TypeLibFunc(64)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17006271 RID: 25201
		// (get) Token: 0x0601299C RID: 76188
		[DispId(-2147417066)]
		public virtual extern int nodeType
		{
			[DispId(-2147417066)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17006272 RID: 25202
		// (get) Token: 0x0601299D RID: 76189
		[DispId(-2147417065)]
		public virtual extern IHTMLDOMNode parentNode
		{
			[DispId(-2147417065)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x0601299E RID: 76190
		[DispId(-2147417064)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool hasChildNodes();

		// Token: 0x17006273 RID: 25203
		// (get) Token: 0x0601299F RID: 76191
		[DispId(-2147417063)]
		public virtual extern object childNodes
		{
			[DispId(-2147417063)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17006274 RID: 25204
		// (get) Token: 0x060129A0 RID: 76192
		[DispId(-2147417062)]
		public virtual extern object attributes
		{
			[DispId(-2147417062)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x060129A1 RID: 76193
		[DispId(-2147417061)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode insertBefore([MarshalAs(28)] [In] IHTMLDOMNode newChild, [MarshalAs(27)] [In] [Optional] object refChild);

		// Token: 0x060129A2 RID: 76194
		[DispId(-2147417060)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode removeChild([MarshalAs(28)] [In] IHTMLDOMNode oldChild);

		// Token: 0x060129A3 RID: 76195
		[DispId(-2147417059)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode replaceChild([MarshalAs(28)] [In] IHTMLDOMNode newChild, [MarshalAs(28)] [In] IHTMLDOMNode oldChild);

		// Token: 0x060129A4 RID: 76196
		[DispId(-2147417051)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode cloneNode([In] bool fDeep);

		// Token: 0x060129A5 RID: 76197
		[DispId(-2147417046)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode removeNode([In] bool fDeep = false);

		// Token: 0x060129A6 RID: 76198
		[DispId(-2147417044)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode swapNode([MarshalAs(28)] [In] IHTMLDOMNode otherNode);

		// Token: 0x060129A7 RID: 76199
		[DispId(-2147417045)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode replaceNode([MarshalAs(28)] [In] IHTMLDOMNode replacement);

		// Token: 0x060129A8 RID: 76200
		[DispId(-2147417039)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode appendChild([MarshalAs(28)] [In] IHTMLDOMNode newChild);

		// Token: 0x17006275 RID: 25205
		// (get) Token: 0x060129A9 RID: 76201
		[DispId(-2147417038)]
		public virtual extern string nodeName
		{
			[DispId(-2147417038)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17006276 RID: 25206
		// (get) Token: 0x060129AB RID: 76203
		// (set) Token: 0x060129AA RID: 76202
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

		// Token: 0x17006277 RID: 25207
		// (get) Token: 0x060129AC RID: 76204
		[DispId(-2147417036)]
		public virtual extern IHTMLDOMNode firstChild
		{
			[DispId(-2147417036)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17006278 RID: 25208
		// (get) Token: 0x060129AD RID: 76205
		[DispId(-2147417035)]
		public virtual extern IHTMLDOMNode lastChild
		{
			[DispId(-2147417035)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17006279 RID: 25209
		// (get) Token: 0x060129AE RID: 76206
		[DispId(-2147417034)]
		public virtual extern IHTMLDOMNode previousSibling
		{
			[DispId(-2147417034)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x1700627A RID: 25210
		// (get) Token: 0x060129AF RID: 76207
		[DispId(-2147417033)]
		public virtual extern IHTMLDOMNode nextSibling
		{
			[DispId(-2147417033)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x1700627B RID: 25211
		// (get) Token: 0x060129B0 RID: 76208
		[DispId(-2147416999)]
		public virtual extern object ownerDocument
		{
			[DispId(-2147416999)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x1700627C RID: 25212
		// (get) Token: 0x060129B2 RID: 76210
		// (set) Token: 0x060129B1 RID: 76209
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

		// Token: 0x1700627D RID: 25213
		// (get) Token: 0x060129B4 RID: 76212
		// (set) Token: 0x060129B3 RID: 76211
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

		// Token: 0x1700627E RID: 25214
		// (get) Token: 0x060129B6 RID: 76214
		// (set) Token: 0x060129B5 RID: 76213
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

		// Token: 0x1700627F RID: 25215
		// (get) Token: 0x060129B7 RID: 76215
		[DispId(-2147415111)]
		public virtual extern object @object
		{
			[DispId(-2147415111)]
			[TypeLibFunc(64)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17006280 RID: 25216
		// (get) Token: 0x060129B9 RID: 76217
		// (set) Token: 0x060129B8 RID: 76216
		[DispId(-2147415107)]
		public virtual extern object recordset
		{
			[TypeLibFunc(64)]
			[DispId(-2147415107)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
			[TypeLibFunc(64)]
			[DispId(-2147415107)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(26)]
			set;
		}

		// Token: 0x17006281 RID: 25217
		// (get) Token: 0x060129BB RID: 76219
		// (set) Token: 0x060129BA RID: 76218
		[DispId(-2147418039)]
		public virtual extern string align
		{
			[DispId(-2147418039)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147418039)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17006282 RID: 25218
		// (get) Token: 0x060129BD RID: 76221
		// (set) Token: 0x060129BC RID: 76220
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

		// Token: 0x17006283 RID: 25219
		// (get) Token: 0x060129BF RID: 76223
		// (set) Token: 0x060129BE RID: 76222
		[DispId(-2147415106)]
		public virtual extern string codeBase
		{
			[DispId(-2147415106)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147415106)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17006284 RID: 25220
		// (get) Token: 0x060129C1 RID: 76225
		// (set) Token: 0x060129C0 RID: 76224
		[DispId(-2147415105)]
		public virtual extern string codeType
		{
			[TypeLibFunc(20)]
			[DispId(-2147415105)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147415105)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17006285 RID: 25221
		// (get) Token: 0x060129C3 RID: 76227
		// (set) Token: 0x060129C2 RID: 76226
		[DispId(-2147415104)]
		public virtual extern string code
		{
			[TypeLibFunc(20)]
			[DispId(-2147415104)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147415104)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17006286 RID: 25222
		// (get) Token: 0x060129C4 RID: 76228
		[DispId(-2147418110)]
		public virtual extern string BaseHref
		{
			[DispId(-2147418110)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17006287 RID: 25223
		// (get) Token: 0x060129C6 RID: 76230
		// (set) Token: 0x060129C5 RID: 76229
		[DispId(-2147415103)]
		public virtual extern string type
		{
			[DispId(-2147415103)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147415103)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17006288 RID: 25224
		// (get) Token: 0x060129C7 RID: 76231
		[DispId(-2147416108)]
		public virtual extern IHTMLFormElement form
		{
			[DispId(-2147416108)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17006289 RID: 25225
		// (get) Token: 0x060129C9 RID: 76233
		// (set) Token: 0x060129C8 RID: 76232
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

		// Token: 0x1700628A RID: 25226
		// (get) Token: 0x060129CB RID: 76235
		// (set) Token: 0x060129CA RID: 76234
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

		// Token: 0x1700628B RID: 25227
		// (get) Token: 0x060129CD RID: 76237
		// (set) Token: 0x060129CC RID: 76236
		[DispId(-2147412083)]
		public virtual extern object onerror
		{
			[DispId(-2147412083)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412083)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x1700628C RID: 25228
		// (get) Token: 0x060129CF RID: 76239
		// (set) Token: 0x060129CE RID: 76238
		[DispId(-2147415101)]
		public virtual extern string altHtml
		{
			[DispId(-2147415101)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147415101)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x1700628D RID: 25229
		// (get) Token: 0x060129D1 RID: 76241
		// (set) Token: 0x060129D0 RID: 76240
		[DispId(-2147415100)]
		public virtual extern int vspace
		{
			[DispId(-2147415100)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[DispId(-2147415100)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x1700628E RID: 25230
		// (get) Token: 0x060129D3 RID: 76243
		// (set) Token: 0x060129D2 RID: 76242
		[DispId(-2147415099)]
		public virtual extern int hspace
		{
			[DispId(-2147415099)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[DispId(-2147415099)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x060129D4 RID: 76244
		[DispId(-2147415098)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(26)]
		public virtual extern object namedRecordset([MarshalAs(19)] [In] string dataMember, [MarshalAs(27)] [In] [Optional] ref object hierarchy);

		// Token: 0x1700628F RID: 25231
		// (get) Token: 0x060129D6 RID: 76246
		// (set) Token: 0x060129D5 RID: 76245
		[DispId(-2147415110)]
		public virtual extern string classid
		{
			[TypeLibFunc(64)]
			[DispId(-2147415110)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147415110)]
			[TypeLibFunc(64)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17006290 RID: 25232
		// (get) Token: 0x060129D8 RID: 76248
		// (set) Token: 0x060129D7 RID: 76247
		[DispId(-2147415109)]
		public virtual extern string data
		{
			[TypeLibFunc(64)]
			[DispId(-2147415109)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147415109)]
			[TypeLibFunc(64)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17006291 RID: 25233
		// (get) Token: 0x060129DA RID: 76250
		// (set) Token: 0x060129D9 RID: 76249
		[DispId(-2147415097)]
		public virtual extern string archive
		{
			[DispId(-2147415097)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147415097)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17006292 RID: 25234
		// (get) Token: 0x060129DC RID: 76252
		// (set) Token: 0x060129DB RID: 76251
		[DispId(-2147415096)]
		public virtual extern string alt
		{
			[DispId(-2147415096)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147415096)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17006293 RID: 25235
		// (get) Token: 0x060129DE RID: 76254
		// (set) Token: 0x060129DD RID: 76253
		[DispId(-2147415095)]
		public virtual extern bool declare
		{
			[DispId(-2147415095)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147415095)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x17006294 RID: 25236
		// (get) Token: 0x060129E0 RID: 76256
		// (set) Token: 0x060129DF RID: 76255
		[DispId(-2147415094)]
		public virtual extern string standby
		{
			[TypeLibFunc(20)]
			[DispId(-2147415094)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147415094)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17006295 RID: 25237
		// (get) Token: 0x060129E2 RID: 76258
		// (set) Token: 0x060129E1 RID: 76257
		[DispId(-2147415093)]
		public virtual extern object border
		{
			[DispId(-2147415093)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147415093)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17006296 RID: 25238
		// (get) Token: 0x060129E4 RID: 76260
		// (set) Token: 0x060129E3 RID: 76259
		[DispId(-2147415092)]
		public virtual extern string useMap
		{
			[TypeLibFunc(20)]
			[DispId(-2147415092)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147415092)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x060129E5 RID: 76261
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement_setAttribute([MarshalAs(19)] [In] string strAttributeName, [MarshalAs(27)] [In] object AttributeValue, [In] int lFlags = 1);

		// Token: 0x060129E6 RID: 76262
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		public virtual extern object IHTMLElement_getAttribute([MarshalAs(19)] [In] string strAttributeName, [In] int lFlags = 0);

		// Token: 0x060129E7 RID: 76263
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement_removeAttribute([MarshalAs(19)] [In] string strAttributeName, [In] int lFlags = 1);

		// Token: 0x17006297 RID: 25239
		// (get) Token: 0x060129E9 RID: 76265
		// (set) Token: 0x060129E8 RID: 76264
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

		// Token: 0x17006298 RID: 25240
		// (get) Token: 0x060129EB RID: 76267
		// (set) Token: 0x060129EA RID: 76266
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

		// Token: 0x17006299 RID: 25241
		// (get) Token: 0x060129EC RID: 76268
		public virtual extern string IHTMLElement_tagName
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x1700629A RID: 25242
		// (get) Token: 0x060129ED RID: 76269
		public virtual extern IHTMLElement IHTMLElement_parentElement
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x1700629B RID: 25243
		// (get) Token: 0x060129EE RID: 76270
		public virtual extern IHTMLStyle IHTMLElement_style
		{
			[TypeLibFunc(1024)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x1700629C RID: 25244
		// (get) Token: 0x060129F0 RID: 76272
		// (set) Token: 0x060129EF RID: 76271
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

		// Token: 0x1700629D RID: 25245
		// (get) Token: 0x060129F2 RID: 76274
		// (set) Token: 0x060129F1 RID: 76273
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

		// Token: 0x1700629E RID: 25246
		// (get) Token: 0x060129F4 RID: 76276
		// (set) Token: 0x060129F3 RID: 76275
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

		// Token: 0x1700629F RID: 25247
		// (get) Token: 0x060129F6 RID: 76278
		// (set) Token: 0x060129F5 RID: 76277
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

		// Token: 0x170062A0 RID: 25248
		// (get) Token: 0x060129F8 RID: 76280
		// (set) Token: 0x060129F7 RID: 76279
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

		// Token: 0x170062A1 RID: 25249
		// (get) Token: 0x060129FA RID: 76282
		// (set) Token: 0x060129F9 RID: 76281
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

		// Token: 0x170062A2 RID: 25250
		// (get) Token: 0x060129FC RID: 76284
		// (set) Token: 0x060129FB RID: 76283
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

		// Token: 0x170062A3 RID: 25251
		// (get) Token: 0x060129FE RID: 76286
		// (set) Token: 0x060129FD RID: 76285
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

		// Token: 0x170062A4 RID: 25252
		// (get) Token: 0x06012A00 RID: 76288
		// (set) Token: 0x060129FF RID: 76287
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

		// Token: 0x170062A5 RID: 25253
		// (get) Token: 0x06012A02 RID: 76290
		// (set) Token: 0x06012A01 RID: 76289
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

		// Token: 0x170062A6 RID: 25254
		// (get) Token: 0x06012A04 RID: 76292
		// (set) Token: 0x06012A03 RID: 76291
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

		// Token: 0x170062A7 RID: 25255
		// (get) Token: 0x06012A05 RID: 76293
		public virtual extern object IHTMLElement_document
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x170062A8 RID: 25256
		// (get) Token: 0x06012A07 RID: 76295
		// (set) Token: 0x06012A06 RID: 76294
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

		// Token: 0x170062A9 RID: 25257
		// (get) Token: 0x06012A09 RID: 76297
		// (set) Token: 0x06012A08 RID: 76296
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

		// Token: 0x170062AA RID: 25258
		// (get) Token: 0x06012A0B RID: 76299
		// (set) Token: 0x06012A0A RID: 76298
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

		// Token: 0x06012A0C RID: 76300
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement_scrollIntoView([MarshalAs(27)] [In] [Optional] object varargStart);

		// Token: 0x06012A0D RID: 76301
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement_contains([MarshalAs(28)] [In] IHTMLElement pChild);

		// Token: 0x170062AB RID: 25259
		// (get) Token: 0x06012A0E RID: 76302
		public virtual extern int IHTMLElement_sourceIndex
		{
			[TypeLibFunc(4)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170062AC RID: 25260
		// (get) Token: 0x06012A0F RID: 76303
		public virtual extern object IHTMLElement_recordNumber
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x170062AD RID: 25261
		// (get) Token: 0x06012A11 RID: 76305
		// (set) Token: 0x06012A10 RID: 76304
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

		// Token: 0x170062AE RID: 25262
		// (get) Token: 0x06012A12 RID: 76306
		public virtual extern int IHTMLElement_offsetLeft
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170062AF RID: 25263
		// (get) Token: 0x06012A13 RID: 76307
		public virtual extern int IHTMLElement_offsetTop
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170062B0 RID: 25264
		// (get) Token: 0x06012A14 RID: 76308
		public virtual extern int IHTMLElement_offsetWidth
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170062B1 RID: 25265
		// (get) Token: 0x06012A15 RID: 76309
		public virtual extern int IHTMLElement_offsetHeight
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170062B2 RID: 25266
		// (get) Token: 0x06012A16 RID: 76310
		public virtual extern IHTMLElement IHTMLElement_offsetParent
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170062B3 RID: 25267
		// (get) Token: 0x06012A18 RID: 76312
		// (set) Token: 0x06012A17 RID: 76311
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

		// Token: 0x170062B4 RID: 25268
		// (get) Token: 0x06012A1A RID: 76314
		// (set) Token: 0x06012A19 RID: 76313
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

		// Token: 0x170062B5 RID: 25269
		// (get) Token: 0x06012A1C RID: 76316
		// (set) Token: 0x06012A1B RID: 76315
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

		// Token: 0x170062B6 RID: 25270
		// (get) Token: 0x06012A1E RID: 76318
		// (set) Token: 0x06012A1D RID: 76317
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

		// Token: 0x06012A1F RID: 76319
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement_insertAdjacentHTML([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string html);

		// Token: 0x06012A20 RID: 76320
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement_insertAdjacentText([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string text);

		// Token: 0x170062B7 RID: 25271
		// (get) Token: 0x06012A21 RID: 76321
		public virtual extern IHTMLElement IHTMLElement_parentTextEdit
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170062B8 RID: 25272
		// (get) Token: 0x06012A22 RID: 76322
		public virtual extern bool IHTMLElement_isTextEdit
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x06012A23 RID: 76323
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement_click();

		// Token: 0x170062B9 RID: 25273
		// (get) Token: 0x06012A24 RID: 76324
		public virtual extern IHTMLFiltersCollection IHTMLElement_filters
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170062BA RID: 25274
		// (get) Token: 0x06012A26 RID: 76326
		// (set) Token: 0x06012A25 RID: 76325
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

		// Token: 0x06012A27 RID: 76327
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string IHTMLElement_toString();

		// Token: 0x170062BB RID: 25275
		// (get) Token: 0x06012A29 RID: 76329
		// (set) Token: 0x06012A28 RID: 76328
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

		// Token: 0x170062BC RID: 25276
		// (get) Token: 0x06012A2B RID: 76331
		// (set) Token: 0x06012A2A RID: 76330
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

		// Token: 0x170062BD RID: 25277
		// (get) Token: 0x06012A2D RID: 76333
		// (set) Token: 0x06012A2C RID: 76332
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

		// Token: 0x170062BE RID: 25278
		// (get) Token: 0x06012A2F RID: 76335
		// (set) Token: 0x06012A2E RID: 76334
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

		// Token: 0x170062BF RID: 25279
		// (get) Token: 0x06012A31 RID: 76337
		// (set) Token: 0x06012A30 RID: 76336
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

		// Token: 0x170062C0 RID: 25280
		// (get) Token: 0x06012A33 RID: 76339
		// (set) Token: 0x06012A32 RID: 76338
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

		// Token: 0x170062C1 RID: 25281
		// (get) Token: 0x06012A35 RID: 76341
		// (set) Token: 0x06012A34 RID: 76340
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

		// Token: 0x170062C2 RID: 25282
		// (get) Token: 0x06012A37 RID: 76343
		// (set) Token: 0x06012A36 RID: 76342
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

		// Token: 0x170062C3 RID: 25283
		// (get) Token: 0x06012A39 RID: 76345
		// (set) Token: 0x06012A38 RID: 76344
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

		// Token: 0x170062C4 RID: 25284
		// (get) Token: 0x06012A3A RID: 76346
		public virtual extern object IHTMLElement_children
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x170062C5 RID: 25285
		// (get) Token: 0x06012A3B RID: 76347
		public virtual extern object IHTMLElement_all
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x170062C6 RID: 25286
		// (get) Token: 0x06012A3C RID: 76348
		public virtual extern string IHTMLElement2_scopeName
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x06012A3D RID: 76349
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_setCapture([In] bool containerCapture = true);

		// Token: 0x06012A3E RID: 76350
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_releaseCapture();

		// Token: 0x170062C7 RID: 25287
		// (get) Token: 0x06012A40 RID: 76352
		// (set) Token: 0x06012A3F RID: 76351
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

		// Token: 0x06012A41 RID: 76353
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string IHTMLElement2_componentFromPoint([In] int x, [In] int y);

		// Token: 0x06012A42 RID: 76354
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_doScroll([MarshalAs(27)] [In] [Optional] object component);

		// Token: 0x170062C8 RID: 25288
		// (get) Token: 0x06012A44 RID: 76356
		// (set) Token: 0x06012A43 RID: 76355
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

		// Token: 0x170062C9 RID: 25289
		// (get) Token: 0x06012A46 RID: 76358
		// (set) Token: 0x06012A45 RID: 76357
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

		// Token: 0x170062CA RID: 25290
		// (get) Token: 0x06012A48 RID: 76360
		// (set) Token: 0x06012A47 RID: 76359
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

		// Token: 0x170062CB RID: 25291
		// (get) Token: 0x06012A4A RID: 76362
		// (set) Token: 0x06012A49 RID: 76361
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

		// Token: 0x170062CC RID: 25292
		// (get) Token: 0x06012A4C RID: 76364
		// (set) Token: 0x06012A4B RID: 76363
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

		// Token: 0x170062CD RID: 25293
		// (get) Token: 0x06012A4E RID: 76366
		// (set) Token: 0x06012A4D RID: 76365
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

		// Token: 0x170062CE RID: 25294
		// (get) Token: 0x06012A50 RID: 76368
		// (set) Token: 0x06012A4F RID: 76367
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

		// Token: 0x170062CF RID: 25295
		// (get) Token: 0x06012A52 RID: 76370
		// (set) Token: 0x06012A51 RID: 76369
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

		// Token: 0x170062D0 RID: 25296
		// (get) Token: 0x06012A54 RID: 76372
		// (set) Token: 0x06012A53 RID: 76371
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

		// Token: 0x170062D1 RID: 25297
		// (get) Token: 0x06012A56 RID: 76374
		// (set) Token: 0x06012A55 RID: 76373
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

		// Token: 0x170062D2 RID: 25298
		// (get) Token: 0x06012A58 RID: 76376
		// (set) Token: 0x06012A57 RID: 76375
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

		// Token: 0x170062D3 RID: 25299
		// (get) Token: 0x06012A5A RID: 76378
		// (set) Token: 0x06012A59 RID: 76377
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

		// Token: 0x170062D4 RID: 25300
		// (get) Token: 0x06012A5C RID: 76380
		// (set) Token: 0x06012A5B RID: 76379
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

		// Token: 0x170062D5 RID: 25301
		// (get) Token: 0x06012A5D RID: 76381
		public virtual extern IHTMLCurrentStyle IHTMLElement2_currentStyle
		{
			[TypeLibFunc(1024)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170062D6 RID: 25302
		// (get) Token: 0x06012A5F RID: 76383
		// (set) Token: 0x06012A5E RID: 76382
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

		// Token: 0x06012A60 RID: 76384
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLRectCollection IHTMLElement2_getClientRects();

		// Token: 0x06012A61 RID: 76385
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLRect IHTMLElement2_getBoundingClientRect();

		// Token: 0x06012A62 RID: 76386
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_setExpression([MarshalAs(19)] [In] string propname, [MarshalAs(19)] [In] string expression, [MarshalAs(19)] [In] string language = "");

		// Token: 0x06012A63 RID: 76387
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		public virtual extern object IHTMLElement2_getExpression([MarshalAs(19)] [In] string propname);

		// Token: 0x06012A64 RID: 76388
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement2_removeExpression([MarshalAs(19)] [In] string propname);

		// Token: 0x170062D7 RID: 25303
		// (get) Token: 0x06012A66 RID: 76390
		// (set) Token: 0x06012A65 RID: 76389
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

		// Token: 0x06012A67 RID: 76391
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_focus();

		// Token: 0x170062D8 RID: 25304
		// (get) Token: 0x06012A69 RID: 76393
		// (set) Token: 0x06012A68 RID: 76392
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

		// Token: 0x170062D9 RID: 25305
		// (get) Token: 0x06012A6B RID: 76395
		// (set) Token: 0x06012A6A RID: 76394
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

		// Token: 0x170062DA RID: 25306
		// (get) Token: 0x06012A6D RID: 76397
		// (set) Token: 0x06012A6C RID: 76396
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

		// Token: 0x170062DB RID: 25307
		// (get) Token: 0x06012A6F RID: 76399
		// (set) Token: 0x06012A6E RID: 76398
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

		// Token: 0x06012A70 RID: 76400
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_blur();

		// Token: 0x06012A71 RID: 76401
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_addFilter([MarshalAs(25)] [In] object pUnk);

		// Token: 0x06012A72 RID: 76402
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_removeFilter([MarshalAs(25)] [In] object pUnk);

		// Token: 0x170062DC RID: 25308
		// (get) Token: 0x06012A73 RID: 76403
		public virtual extern int IHTMLElement2_clientHeight
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170062DD RID: 25309
		// (get) Token: 0x06012A74 RID: 76404
		public virtual extern int IHTMLElement2_clientWidth
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170062DE RID: 25310
		// (get) Token: 0x06012A75 RID: 76405
		public virtual extern int IHTMLElement2_clientTop
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170062DF RID: 25311
		// (get) Token: 0x06012A76 RID: 76406
		public virtual extern int IHTMLElement2_clientLeft
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x06012A77 RID: 76407
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement2_attachEvent([MarshalAs(19)] [In] string @event, [MarshalAs(26)] [In] object pdisp);

		// Token: 0x06012A78 RID: 76408
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_detachEvent([MarshalAs(19)] [In] string @event, [MarshalAs(26)] [In] object pdisp);

		// Token: 0x170062E0 RID: 25312
		// (get) Token: 0x06012A79 RID: 76409
		public virtual extern object IHTMLElement2_readyState
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x170062E1 RID: 25313
		// (get) Token: 0x06012A7B RID: 76411
		// (set) Token: 0x06012A7A RID: 76410
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

		// Token: 0x170062E2 RID: 25314
		// (get) Token: 0x06012A7D RID: 76413
		// (set) Token: 0x06012A7C RID: 76412
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

		// Token: 0x170062E3 RID: 25315
		// (get) Token: 0x06012A7F RID: 76415
		// (set) Token: 0x06012A7E RID: 76414
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

		// Token: 0x170062E4 RID: 25316
		// (get) Token: 0x06012A81 RID: 76417
		// (set) Token: 0x06012A80 RID: 76416
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

		// Token: 0x170062E5 RID: 25317
		// (get) Token: 0x06012A83 RID: 76419
		// (set) Token: 0x06012A82 RID: 76418
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

		// Token: 0x06012A84 RID: 76420
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(26)]
		public virtual extern object IHTMLElement2_createControlRange();

		// Token: 0x170062E6 RID: 25318
		// (get) Token: 0x06012A85 RID: 76421
		public virtual extern int IHTMLElement2_scrollHeight
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170062E7 RID: 25319
		// (get) Token: 0x06012A86 RID: 76422
		public virtual extern int IHTMLElement2_scrollWidth
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170062E8 RID: 25320
		// (get) Token: 0x06012A88 RID: 76424
		// (set) Token: 0x06012A87 RID: 76423
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

		// Token: 0x170062E9 RID: 25321
		// (get) Token: 0x06012A8A RID: 76426
		// (set) Token: 0x06012A89 RID: 76425
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

		// Token: 0x06012A8B RID: 76427
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_clearAttributes();

		// Token: 0x06012A8C RID: 76428
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void mergeAttributes([MarshalAs(28)] [In] IHTMLElement mergeThis);

		// Token: 0x170062EA RID: 25322
		// (get) Token: 0x06012A8E RID: 76430
		// (set) Token: 0x06012A8D RID: 76429
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

		// Token: 0x06012A8F RID: 76431
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElement IHTMLElement2_insertAdjacentElement([MarshalAs(19)] [In] string where, [MarshalAs(28)] [In] IHTMLElement insertedElement);

		// Token: 0x06012A90 RID: 76432
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElement IHTMLElement2_applyElement([MarshalAs(28)] [In] IHTMLElement apply, [MarshalAs(19)] [In] string where);

		// Token: 0x06012A91 RID: 76433
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string IHTMLElement2_getAdjacentText([MarshalAs(19)] [In] string where);

		// Token: 0x06012A92 RID: 76434
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string IHTMLElement2_replaceAdjacentText([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string newText);

		// Token: 0x170062EB RID: 25323
		// (get) Token: 0x06012A93 RID: 76435
		public virtual extern bool IHTMLElement2_canHaveChildren
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x06012A94 RID: 76436
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern int IHTMLElement2_addBehavior([MarshalAs(19)] [In] string bstrUrl, [MarshalAs(27)] [In] [Optional] ref object pvarFactory);

		// Token: 0x06012A95 RID: 76437
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement2_removeBehavior([In] int cookie);

		// Token: 0x170062EC RID: 25324
		// (get) Token: 0x06012A96 RID: 76438
		public virtual extern IHTMLStyle IHTMLElement2_runtimeStyle
		{
			[TypeLibFunc(1024)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170062ED RID: 25325
		// (get) Token: 0x06012A97 RID: 76439
		public virtual extern object IHTMLElement2_behaviorUrns
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x170062EE RID: 25326
		// (get) Token: 0x06012A99 RID: 76441
		// (set) Token: 0x06012A98 RID: 76440
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

		// Token: 0x170062EF RID: 25327
		// (get) Token: 0x06012A9B RID: 76443
		// (set) Token: 0x06012A9A RID: 76442
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

		// Token: 0x170062F0 RID: 25328
		// (get) Token: 0x06012A9C RID: 76444
		public virtual extern int IHTMLElement2_readyStateValue
		{
			[TypeLibFunc(65)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x06012A9D RID: 76445
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElementCollection IHTMLElement2_getElementsByTagName([MarshalAs(19)] [In] string v);

		// Token: 0x06012A9E RID: 76446
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void mergeAttributes([MarshalAs(28)] [In] IHTMLElement mergeThis, [MarshalAs(27)] [In] [Optional] ref object pvarFlags);

		// Token: 0x170062F1 RID: 25329
		// (get) Token: 0x06012A9F RID: 76447
		public virtual extern bool IHTMLElement3_isMultiLine
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170062F2 RID: 25330
		// (get) Token: 0x06012AA0 RID: 76448
		public virtual extern bool IHTMLElement3_canHaveHTML
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170062F3 RID: 25331
		// (get) Token: 0x06012AA2 RID: 76450
		// (set) Token: 0x06012AA1 RID: 76449
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

		// Token: 0x170062F4 RID: 25332
		// (get) Token: 0x06012AA4 RID: 76452
		// (set) Token: 0x06012AA3 RID: 76451
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

		// Token: 0x170062F5 RID: 25333
		// (get) Token: 0x06012AA6 RID: 76454
		// (set) Token: 0x06012AA5 RID: 76453
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

		// Token: 0x170062F6 RID: 25334
		// (get) Token: 0x06012AA8 RID: 76456
		// (set) Token: 0x06012AA7 RID: 76455
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

		// Token: 0x06012AA9 RID: 76457
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement3_setActive();

		// Token: 0x170062F7 RID: 25335
		// (get) Token: 0x06012AAB RID: 76459
		// (set) Token: 0x06012AAA RID: 76458
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

		// Token: 0x170062F8 RID: 25336
		// (get) Token: 0x06012AAC RID: 76460
		public virtual extern bool IHTMLElement3_isContentEditable
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170062F9 RID: 25337
		// (get) Token: 0x06012AAE RID: 76462
		// (set) Token: 0x06012AAD RID: 76461
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

		// Token: 0x170062FA RID: 25338
		// (get) Token: 0x06012AB0 RID: 76464
		// (set) Token: 0x06012AAF RID: 76463
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

		// Token: 0x170062FB RID: 25339
		// (get) Token: 0x06012AB1 RID: 76465
		public virtual extern bool IHTMLElement3_isDisabled
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170062FC RID: 25340
		// (get) Token: 0x06012AB3 RID: 76467
		// (set) Token: 0x06012AB2 RID: 76466
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

		// Token: 0x170062FD RID: 25341
		// (get) Token: 0x06012AB5 RID: 76469
		// (set) Token: 0x06012AB4 RID: 76468
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

		// Token: 0x06012AB6 RID: 76470
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement3_FireEvent([MarshalAs(19)] [In] string bstrEventName, [MarshalAs(27)] [In] [Optional] ref object pvarEventObject);

		// Token: 0x170062FE RID: 25342
		// (get) Token: 0x06012AB8 RID: 76472
		// (set) Token: 0x06012AB7 RID: 76471
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

		// Token: 0x170062FF RID: 25343
		// (get) Token: 0x06012ABA RID: 76474
		// (set) Token: 0x06012AB9 RID: 76473
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

		// Token: 0x17006300 RID: 25344
		// (get) Token: 0x06012ABC RID: 76476
		// (set) Token: 0x06012ABB RID: 76475
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

		// Token: 0x17006301 RID: 25345
		// (get) Token: 0x06012ABE RID: 76478
		// (set) Token: 0x06012ABD RID: 76477
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

		// Token: 0x17006302 RID: 25346
		// (get) Token: 0x06012AC0 RID: 76480
		// (set) Token: 0x06012ABF RID: 76479
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

		// Token: 0x17006303 RID: 25347
		// (get) Token: 0x06012AC2 RID: 76482
		// (set) Token: 0x06012AC1 RID: 76481
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

		// Token: 0x17006304 RID: 25348
		// (get) Token: 0x06012AC4 RID: 76484
		// (set) Token: 0x06012AC3 RID: 76483
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

		// Token: 0x17006305 RID: 25349
		// (get) Token: 0x06012AC6 RID: 76486
		// (set) Token: 0x06012AC5 RID: 76485
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

		// Token: 0x06012AC7 RID: 76487
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement3_dragDrop();

		// Token: 0x17006306 RID: 25350
		// (get) Token: 0x06012AC8 RID: 76488
		public virtual extern int IHTMLElement3_glyphMode
		{
			[TypeLibFunc(1089)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17006307 RID: 25351
		// (get) Token: 0x06012ACA RID: 76490
		// (set) Token: 0x06012AC9 RID: 76489
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

		// Token: 0x06012ACB RID: 76491
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement4_normalize();

		// Token: 0x06012ACC RID: 76492
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute IHTMLElement4_getAttributeNode([MarshalAs(19)] [In] string bstrName);

		// Token: 0x06012ACD RID: 76493
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute IHTMLElement4_setAttributeNode([MarshalAs(28)] [In] IHTMLDOMAttribute pattr);

		// Token: 0x06012ACE RID: 76494
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute IHTMLElement4_removeAttributeNode([MarshalAs(28)] [In] IHTMLDOMAttribute pattr);

		// Token: 0x17006308 RID: 25352
		// (get) Token: 0x06012AD0 RID: 76496
		// (set) Token: 0x06012ACF RID: 76495
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

		// Token: 0x17006309 RID: 25353
		// (get) Token: 0x06012AD2 RID: 76498
		// (set) Token: 0x06012AD1 RID: 76497
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

		// Token: 0x1700630A RID: 25354
		// (get) Token: 0x06012AD4 RID: 76500
		// (set) Token: 0x06012AD3 RID: 76499
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

		// Token: 0x1700630B RID: 25355
		// (get) Token: 0x06012AD5 RID: 76501
		public virtual extern int IHTMLUniqueName_uniqueNumber
		{
			[TypeLibFunc(64)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700630C RID: 25356
		// (get) Token: 0x06012AD6 RID: 76502
		public virtual extern string IHTMLUniqueName_uniqueID
		{
			[TypeLibFunc(64)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x1700630D RID: 25357
		// (get) Token: 0x06012AD7 RID: 76503
		public virtual extern int IHTMLDOMNode_nodeType
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700630E RID: 25358
		// (get) Token: 0x06012AD8 RID: 76504
		public virtual extern IHTMLDOMNode IHTMLDOMNode_parentNode
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x06012AD9 RID: 76505
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLDOMNode_hasChildNodes();

		// Token: 0x1700630F RID: 25359
		// (get) Token: 0x06012ADA RID: 76506
		public virtual extern object IHTMLDOMNode_childNodes
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17006310 RID: 25360
		// (get) Token: 0x06012ADB RID: 76507
		public virtual extern object IHTMLDOMNode_attributes
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x06012ADC RID: 76508
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_insertBefore([MarshalAs(28)] [In] IHTMLDOMNode newChild, [MarshalAs(27)] [In] [Optional] object refChild);

		// Token: 0x06012ADD RID: 76509
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_removeChild([MarshalAs(28)] [In] IHTMLDOMNode oldChild);

		// Token: 0x06012ADE RID: 76510
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_replaceChild([MarshalAs(28)] [In] IHTMLDOMNode newChild, [MarshalAs(28)] [In] IHTMLDOMNode oldChild);

		// Token: 0x06012ADF RID: 76511
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_cloneNode([In] bool fDeep);

		// Token: 0x06012AE0 RID: 76512
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_removeNode([In] bool fDeep = false);

		// Token: 0x06012AE1 RID: 76513
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_swapNode([MarshalAs(28)] [In] IHTMLDOMNode otherNode);

		// Token: 0x06012AE2 RID: 76514
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_replaceNode([MarshalAs(28)] [In] IHTMLDOMNode replacement);

		// Token: 0x06012AE3 RID: 76515
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_appendChild([MarshalAs(28)] [In] IHTMLDOMNode newChild);

		// Token: 0x17006311 RID: 25361
		// (get) Token: 0x06012AE4 RID: 76516
		public virtual extern string IHTMLDOMNode_nodeName
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17006312 RID: 25362
		// (get) Token: 0x06012AE6 RID: 76518
		// (set) Token: 0x06012AE5 RID: 76517
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

		// Token: 0x17006313 RID: 25363
		// (get) Token: 0x06012AE7 RID: 76519
		public virtual extern IHTMLDOMNode IHTMLDOMNode_firstChild
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17006314 RID: 25364
		// (get) Token: 0x06012AE8 RID: 76520
		public virtual extern IHTMLDOMNode IHTMLDOMNode_lastChild
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17006315 RID: 25365
		// (get) Token: 0x06012AE9 RID: 76521
		public virtual extern IHTMLDOMNode IHTMLDOMNode_previousSibling
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17006316 RID: 25366
		// (get) Token: 0x06012AEA RID: 76522
		public virtual extern IHTMLDOMNode IHTMLDOMNode_nextSibling
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17006317 RID: 25367
		// (get) Token: 0x06012AEB RID: 76523
		public virtual extern object IHTMLDOMNode2_ownerDocument
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17006318 RID: 25368
		// (get) Token: 0x06012AED RID: 76525
		// (set) Token: 0x06012AEC RID: 76524
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

		// Token: 0x17006319 RID: 25369
		// (get) Token: 0x06012AEF RID: 76527
		// (set) Token: 0x06012AEE RID: 76526
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

		// Token: 0x1700631A RID: 25370
		// (get) Token: 0x06012AF1 RID: 76529
		// (set) Token: 0x06012AF0 RID: 76528
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

		// Token: 0x1700631B RID: 25371
		// (get) Token: 0x06012AF3 RID: 76531
		// (set) Token: 0x06012AF2 RID: 76530
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

		// Token: 0x06012AF4 RID: 76532
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLControlElement_focus();

		// Token: 0x1700631C RID: 25372
		// (get) Token: 0x06012AF6 RID: 76534
		// (set) Token: 0x06012AF5 RID: 76533
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

		// Token: 0x1700631D RID: 25373
		// (get) Token: 0x06012AF8 RID: 76536
		// (set) Token: 0x06012AF7 RID: 76535
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

		// Token: 0x1700631E RID: 25374
		// (get) Token: 0x06012AFA RID: 76538
		// (set) Token: 0x06012AF9 RID: 76537
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

		// Token: 0x1700631F RID: 25375
		// (get) Token: 0x06012AFC RID: 76540
		// (set) Token: 0x06012AFB RID: 76539
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

		// Token: 0x06012AFD RID: 76541
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLControlElement_blur();

		// Token: 0x06012AFE RID: 76542
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLControlElement_addFilter([MarshalAs(25)] [In] object pUnk);

		// Token: 0x06012AFF RID: 76543
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLControlElement_removeFilter([MarshalAs(25)] [In] object pUnk);

		// Token: 0x17006320 RID: 25376
		// (get) Token: 0x06012B00 RID: 76544
		public virtual extern int IHTMLControlElement_clientHeight
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17006321 RID: 25377
		// (get) Token: 0x06012B01 RID: 76545
		public virtual extern int IHTMLControlElement_clientWidth
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17006322 RID: 25378
		// (get) Token: 0x06012B02 RID: 76546
		public virtual extern int IHTMLControlElement_clientTop
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17006323 RID: 25379
		// (get) Token: 0x06012B03 RID: 76547
		public virtual extern int IHTMLControlElement_clientLeft
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17006324 RID: 25380
		// (get) Token: 0x06012B04 RID: 76548
		public virtual extern object IHTMLObjectElement_object
		{
			[TypeLibFunc(64)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17006325 RID: 25381
		// (get) Token: 0x06012B05 RID: 76549
		public virtual extern string IHTMLObjectElement_classid
		{
			[TypeLibFunc(64)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17006326 RID: 25382
		// (get) Token: 0x06012B06 RID: 76550
		public virtual extern string IHTMLObjectElement_data
		{
			[TypeLibFunc(64)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17006327 RID: 25383
		// (get) Token: 0x06012B08 RID: 76552
		// (set) Token: 0x06012B07 RID: 76551
		public virtual extern object IHTMLObjectElement_recordset
		{
			[TypeLibFunc(64)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
			[TypeLibFunc(64)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(26)]
			[param: In]
			set;
		}

		// Token: 0x17006328 RID: 25384
		// (get) Token: 0x06012B0A RID: 76554
		// (set) Token: 0x06012B09 RID: 76553
		public virtual extern string IHTMLObjectElement_align
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

		// Token: 0x17006329 RID: 25385
		// (get) Token: 0x06012B0C RID: 76556
		// (set) Token: 0x06012B0B RID: 76555
		public virtual extern string IHTMLObjectElement_name
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

		// Token: 0x1700632A RID: 25386
		// (get) Token: 0x06012B0E RID: 76558
		// (set) Token: 0x06012B0D RID: 76557
		public virtual extern string IHTMLObjectElement_codeBase
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

		// Token: 0x1700632B RID: 25387
		// (get) Token: 0x06012B10 RID: 76560
		// (set) Token: 0x06012B0F RID: 76559
		public virtual extern string IHTMLObjectElement_codeType
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

		// Token: 0x1700632C RID: 25388
		// (get) Token: 0x06012B12 RID: 76562
		// (set) Token: 0x06012B11 RID: 76561
		public virtual extern string IHTMLObjectElement_code
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

		// Token: 0x1700632D RID: 25389
		// (get) Token: 0x06012B13 RID: 76563
		public virtual extern string IHTMLObjectElement_BaseHref
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x1700632E RID: 25390
		// (get) Token: 0x06012B15 RID: 76565
		// (set) Token: 0x06012B14 RID: 76564
		public virtual extern string IHTMLObjectElement_type
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

		// Token: 0x1700632F RID: 25391
		// (get) Token: 0x06012B16 RID: 76566
		public virtual extern IHTMLFormElement IHTMLObjectElement_form
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17006330 RID: 25392
		// (get) Token: 0x06012B18 RID: 76568
		// (set) Token: 0x06012B17 RID: 76567
		public virtual extern object IHTMLObjectElement_width
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17006331 RID: 25393
		// (get) Token: 0x06012B1A RID: 76570
		// (set) Token: 0x06012B19 RID: 76569
		public virtual extern object IHTMLObjectElement_height
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17006332 RID: 25394
		// (get) Token: 0x06012B1B RID: 76571
		public virtual extern int IHTMLObjectElement_readyState
		{
			[TypeLibFunc(64)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17006333 RID: 25395
		// (get) Token: 0x06012B1D RID: 76573
		// (set) Token: 0x06012B1C RID: 76572
		public virtual extern object IHTMLObjectElement_onreadystatechange
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17006334 RID: 25396
		// (get) Token: 0x06012B1F RID: 76575
		// (set) Token: 0x06012B1E RID: 76574
		public virtual extern object IHTMLObjectElement_onerror
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17006335 RID: 25397
		// (get) Token: 0x06012B21 RID: 76577
		// (set) Token: 0x06012B20 RID: 76576
		public virtual extern string IHTMLObjectElement_altHtml
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17006336 RID: 25398
		// (get) Token: 0x06012B23 RID: 76579
		// (set) Token: 0x06012B22 RID: 76578
		public virtual extern int IHTMLObjectElement_vspace
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x17006337 RID: 25399
		// (get) Token: 0x06012B25 RID: 76581
		// (set) Token: 0x06012B24 RID: 76580
		public virtual extern int IHTMLObjectElement_hspace
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x06012B26 RID: 76582
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(26)]
		public virtual extern object IHTMLObjectElement2_namedRecordset([MarshalAs(19)] [In] string dataMember, [MarshalAs(27)] [In] [Optional] ref object hierarchy);

		// Token: 0x17006338 RID: 25400
		// (get) Token: 0x06012B28 RID: 76584
		// (set) Token: 0x06012B27 RID: 76583
		public virtual extern string IHTMLObjectElement2_classid
		{
			[TypeLibFunc(64)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(64)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17006339 RID: 25401
		// (get) Token: 0x06012B2A RID: 76586
		// (set) Token: 0x06012B29 RID: 76585
		public virtual extern string IHTMLObjectElement2_data
		{
			[TypeLibFunc(64)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(64)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x1700633A RID: 25402
		// (get) Token: 0x06012B2C RID: 76588
		// (set) Token: 0x06012B2B RID: 76587
		public virtual extern string IHTMLObjectElement3_archive
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

		// Token: 0x1700633B RID: 25403
		// (get) Token: 0x06012B2E RID: 76590
		// (set) Token: 0x06012B2D RID: 76589
		public virtual extern string IHTMLObjectElement3_alt
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

		// Token: 0x1700633C RID: 25404
		// (get) Token: 0x06012B30 RID: 76592
		// (set) Token: 0x06012B2F RID: 76591
		public virtual extern bool IHTMLObjectElement3_declare
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x1700633D RID: 25405
		// (get) Token: 0x06012B32 RID: 76594
		// (set) Token: 0x06012B31 RID: 76593
		public virtual extern string IHTMLObjectElement3_standby
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

		// Token: 0x1700633E RID: 25406
		// (get) Token: 0x06012B34 RID: 76596
		// (set) Token: 0x06012B33 RID: 76595
		public virtual extern object IHTMLObjectElement3_border
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x1700633F RID: 25407
		// (get) Token: 0x06012B36 RID: 76598
		// (set) Token: 0x06012B35 RID: 76597
		public virtual extern string IHTMLObjectElement3_useMap
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

		// Token: 0x140023E0 RID: 9184
		// (add) Token: 0x06012B37 RID: 76599
		// (remove) Token: 0x06012B38 RID: 76600
		public virtual extern event HTMLObjectElementEvents_onbeforeupdateEventHandler HTMLObjectElementEvents_Event_onbeforeupdate;

		// Token: 0x140023E1 RID: 9185
		// (add) Token: 0x06012B39 RID: 76601
		// (remove) Token: 0x06012B3A RID: 76602
		public virtual extern event HTMLObjectElementEvents_onafterupdateEventHandler HTMLObjectElementEvents_Event_onafterupdate;

		// Token: 0x140023E2 RID: 9186
		// (add) Token: 0x06012B3B RID: 76603
		// (remove) Token: 0x06012B3C RID: 76604
		public virtual extern event HTMLObjectElementEvents_onerrorupdateEventHandler HTMLObjectElementEvents_Event_onerrorupdate;

		// Token: 0x140023E3 RID: 9187
		// (add) Token: 0x06012B3D RID: 76605
		// (remove) Token: 0x06012B3E RID: 76606
		public virtual extern event HTMLObjectElementEvents_onrowexitEventHandler HTMLObjectElementEvents_Event_onrowexit;

		// Token: 0x140023E4 RID: 9188
		// (add) Token: 0x06012B3F RID: 76607
		// (remove) Token: 0x06012B40 RID: 76608
		public virtual extern event HTMLObjectElementEvents_onrowenterEventHandler HTMLObjectElementEvents_Event_onrowenter;

		// Token: 0x140023E5 RID: 9189
		// (add) Token: 0x06012B41 RID: 76609
		// (remove) Token: 0x06012B42 RID: 76610
		public virtual extern event HTMLObjectElementEvents_ondatasetchangedEventHandler HTMLObjectElementEvents_Event_ondatasetchanged;

		// Token: 0x140023E6 RID: 9190
		// (add) Token: 0x06012B43 RID: 76611
		// (remove) Token: 0x06012B44 RID: 76612
		public virtual extern event HTMLObjectElementEvents_ondataavailableEventHandler HTMLObjectElementEvents_Event_ondataavailable;

		// Token: 0x140023E7 RID: 9191
		// (add) Token: 0x06012B45 RID: 76613
		// (remove) Token: 0x06012B46 RID: 76614
		public virtual extern event HTMLObjectElementEvents_ondatasetcompleteEventHandler HTMLObjectElementEvents_Event_ondatasetcomplete;

		// Token: 0x140023E8 RID: 9192
		// (add) Token: 0x06012B47 RID: 76615
		// (remove) Token: 0x06012B48 RID: 76616
		public virtual extern event HTMLObjectElementEvents_onerrorEventHandler HTMLObjectElementEvents_Event_onerror;

		// Token: 0x140023E9 RID: 9193
		// (add) Token: 0x06012B49 RID: 76617
		// (remove) Token: 0x06012B4A RID: 76618
		public virtual extern event HTMLObjectElementEvents_onrowsdeleteEventHandler HTMLObjectElementEvents_Event_onrowsdelete;

		// Token: 0x140023EA RID: 9194
		// (add) Token: 0x06012B4B RID: 76619
		// (remove) Token: 0x06012B4C RID: 76620
		public virtual extern event HTMLObjectElementEvents_onrowsinsertedEventHandler HTMLObjectElementEvents_Event_onrowsinserted;

		// Token: 0x140023EB RID: 9195
		// (add) Token: 0x06012B4D RID: 76621
		// (remove) Token: 0x06012B4E RID: 76622
		public virtual extern event HTMLObjectElementEvents_oncellchangeEventHandler HTMLObjectElementEvents_Event_oncellchange;

		// Token: 0x140023EC RID: 9196
		// (add) Token: 0x06012B4F RID: 76623
		// (remove) Token: 0x06012B50 RID: 76624
		public virtual extern event HTMLObjectElementEvents_onreadystatechangeEventHandler HTMLObjectElementEvents_Event_onreadystatechange;

		// Token: 0x140023ED RID: 9197
		// (add) Token: 0x06012B51 RID: 76625
		// (remove) Token: 0x06012B52 RID: 76626
		public virtual extern event HTMLObjectElementEvents2_onbeforeupdateEventHandler HTMLObjectElementEvents2_Event_onbeforeupdate;

		// Token: 0x140023EE RID: 9198
		// (add) Token: 0x06012B53 RID: 76627
		// (remove) Token: 0x06012B54 RID: 76628
		public virtual extern event HTMLObjectElementEvents2_onafterupdateEventHandler HTMLObjectElementEvents2_Event_onafterupdate;

		// Token: 0x140023EF RID: 9199
		// (add) Token: 0x06012B55 RID: 76629
		// (remove) Token: 0x06012B56 RID: 76630
		public virtual extern event HTMLObjectElementEvents2_onerrorupdateEventHandler HTMLObjectElementEvents2_Event_onerrorupdate;

		// Token: 0x140023F0 RID: 9200
		// (add) Token: 0x06012B57 RID: 76631
		// (remove) Token: 0x06012B58 RID: 76632
		public virtual extern event HTMLObjectElementEvents2_onrowexitEventHandler HTMLObjectElementEvents2_Event_onrowexit;

		// Token: 0x140023F1 RID: 9201
		// (add) Token: 0x06012B59 RID: 76633
		// (remove) Token: 0x06012B5A RID: 76634
		public virtual extern event HTMLObjectElementEvents2_onrowenterEventHandler HTMLObjectElementEvents2_Event_onrowenter;

		// Token: 0x140023F2 RID: 9202
		// (add) Token: 0x06012B5B RID: 76635
		// (remove) Token: 0x06012B5C RID: 76636
		public virtual extern event HTMLObjectElementEvents2_ondatasetchangedEventHandler HTMLObjectElementEvents2_Event_ondatasetchanged;

		// Token: 0x140023F3 RID: 9203
		// (add) Token: 0x06012B5D RID: 76637
		// (remove) Token: 0x06012B5E RID: 76638
		public virtual extern event HTMLObjectElementEvents2_ondataavailableEventHandler HTMLObjectElementEvents2_Event_ondataavailable;

		// Token: 0x140023F4 RID: 9204
		// (add) Token: 0x06012B5F RID: 76639
		// (remove) Token: 0x06012B60 RID: 76640
		public virtual extern event HTMLObjectElementEvents2_ondatasetcompleteEventHandler HTMLObjectElementEvents2_Event_ondatasetcomplete;

		// Token: 0x140023F5 RID: 9205
		// (add) Token: 0x06012B61 RID: 76641
		// (remove) Token: 0x06012B62 RID: 76642
		public virtual extern event HTMLObjectElementEvents2_onerrorEventHandler HTMLObjectElementEvents2_Event_onerror;

		// Token: 0x140023F6 RID: 9206
		// (add) Token: 0x06012B63 RID: 76643
		// (remove) Token: 0x06012B64 RID: 76644
		public virtual extern event HTMLObjectElementEvents2_onrowsdeleteEventHandler HTMLObjectElementEvents2_Event_onrowsdelete;

		// Token: 0x140023F7 RID: 9207
		// (add) Token: 0x06012B65 RID: 76645
		// (remove) Token: 0x06012B66 RID: 76646
		public virtual extern event HTMLObjectElementEvents2_onrowsinsertedEventHandler HTMLObjectElementEvents2_Event_onrowsinserted;

		// Token: 0x140023F8 RID: 9208
		// (add) Token: 0x06012B67 RID: 76647
		// (remove) Token: 0x06012B68 RID: 76648
		public virtual extern event HTMLObjectElementEvents2_oncellchangeEventHandler HTMLObjectElementEvents2_Event_oncellchange;

		// Token: 0x140023F9 RID: 9209
		// (add) Token: 0x06012B69 RID: 76649
		// (remove) Token: 0x06012B6A RID: 76650
		public virtual extern event HTMLObjectElementEvents2_onreadystatechangeEventHandler HTMLObjectElementEvents2_Event_onreadystatechange;
	}
}
