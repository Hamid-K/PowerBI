using System;
using System.Collections;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x020004E3 RID: 1251
	[ClassInterface(0)]
	[TypeLibType(2)]
	[DefaultMember("item")]
	[ComSourceInterfaces("mshtml.HTMLSelectElementEvents\0mshtml.HTMLSelectElementEvents2\0\0")]
	[Guid("3050F245-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public class HTMLSelectElementClass : DispHTMLSelectElement, HTMLSelectElement, HTMLSelectElementEvents_Event, IHTMLElement, IHTMLElement2, IHTMLElement3, IHTMLElement4, IHTMLUniqueName, IHTMLDOMNode, IHTMLDOMNode2, IHTMLDatabinding, IHTMLControlElement, IHTMLSelectElement, IHTMLSelectElement2, IHTMLSelectElement4, HTMLSelectElementEvents2_Event
	{
		// Token: 0x060080A3 RID: 32931
		[MethodImpl(4096, MethodCodeType = 3)]
		public extern HTMLSelectElementClass();

		// Token: 0x060080A4 RID: 32932
		[DispId(-2147417611)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void setAttribute([MarshalAs(19)] [In] string strAttributeName, [MarshalAs(27)] [In] object AttributeValue, [In] int lFlags = 1);

		// Token: 0x060080A5 RID: 32933
		[DispId(-2147417610)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		public virtual extern object getAttribute([MarshalAs(19)] [In] string strAttributeName, [In] int lFlags = 0);

		// Token: 0x060080A6 RID: 32934
		[DispId(-2147417609)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool removeAttribute([MarshalAs(19)] [In] string strAttributeName, [In] int lFlags = 1);

		// Token: 0x17002BD2 RID: 11218
		// (get) Token: 0x060080A8 RID: 32936
		// (set) Token: 0x060080A7 RID: 32935
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

		// Token: 0x17002BD3 RID: 11219
		// (get) Token: 0x060080AA RID: 32938
		// (set) Token: 0x060080A9 RID: 32937
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

		// Token: 0x17002BD4 RID: 11220
		// (get) Token: 0x060080AB RID: 32939
		[DispId(-2147417108)]
		public virtual extern string tagName
		{
			[DispId(-2147417108)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17002BD5 RID: 11221
		// (get) Token: 0x060080AC RID: 32940
		[DispId(-2147418104)]
		public virtual extern IHTMLElement parentElement
		{
			[DispId(-2147418104)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17002BD6 RID: 11222
		// (get) Token: 0x060080AD RID: 32941
		[DispId(-2147418038)]
		public virtual extern IHTMLStyle style
		{
			[TypeLibFunc(1024)]
			[DispId(-2147418038)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17002BD7 RID: 11223
		// (get) Token: 0x060080AF RID: 32943
		// (set) Token: 0x060080AE RID: 32942
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

		// Token: 0x17002BD8 RID: 11224
		// (get) Token: 0x060080B1 RID: 32945
		// (set) Token: 0x060080B0 RID: 32944
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

		// Token: 0x17002BD9 RID: 11225
		// (get) Token: 0x060080B3 RID: 32947
		// (set) Token: 0x060080B2 RID: 32946
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

		// Token: 0x17002BDA RID: 11226
		// (get) Token: 0x060080B5 RID: 32949
		// (set) Token: 0x060080B4 RID: 32948
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

		// Token: 0x17002BDB RID: 11227
		// (get) Token: 0x060080B7 RID: 32951
		// (set) Token: 0x060080B6 RID: 32950
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

		// Token: 0x17002BDC RID: 11228
		// (get) Token: 0x060080B9 RID: 32953
		// (set) Token: 0x060080B8 RID: 32952
		[DispId(-2147412105)]
		public virtual extern object onkeypress
		{
			[TypeLibFunc(20)]
			[DispId(-2147412105)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412105)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17002BDD RID: 11229
		// (get) Token: 0x060080BB RID: 32955
		// (set) Token: 0x060080BA RID: 32954
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

		// Token: 0x17002BDE RID: 11230
		// (get) Token: 0x060080BD RID: 32957
		// (set) Token: 0x060080BC RID: 32956
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

		// Token: 0x17002BDF RID: 11231
		// (get) Token: 0x060080BF RID: 32959
		// (set) Token: 0x060080BE RID: 32958
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

		// Token: 0x17002BE0 RID: 11232
		// (get) Token: 0x060080C1 RID: 32961
		// (set) Token: 0x060080C0 RID: 32960
		[DispId(-2147412110)]
		public virtual extern object onmousedown
		{
			[DispId(-2147412110)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412110)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17002BE1 RID: 11233
		// (get) Token: 0x060080C3 RID: 32963
		// (set) Token: 0x060080C2 RID: 32962
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

		// Token: 0x17002BE2 RID: 11234
		// (get) Token: 0x060080C4 RID: 32964
		[DispId(-2147417094)]
		public virtual extern object document
		{
			[DispId(-2147417094)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17002BE3 RID: 11235
		// (get) Token: 0x060080C6 RID: 32966
		// (set) Token: 0x060080C5 RID: 32965
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

		// Token: 0x17002BE4 RID: 11236
		// (get) Token: 0x060080C8 RID: 32968
		// (set) Token: 0x060080C7 RID: 32967
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

		// Token: 0x17002BE5 RID: 11237
		// (get) Token: 0x060080CA RID: 32970
		// (set) Token: 0x060080C9 RID: 32969
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

		// Token: 0x060080CB RID: 32971
		[DispId(-2147417093)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void scrollIntoView([MarshalAs(27)] [In] [Optional] object varargStart);

		// Token: 0x060080CC RID: 32972
		[DispId(-2147417092)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool contains([MarshalAs(28)] [In] IHTMLElement pChild);

		// Token: 0x17002BE6 RID: 11238
		// (get) Token: 0x060080CD RID: 32973
		[DispId(-2147417088)]
		public virtual extern int sourceIndex
		{
			[DispId(-2147417088)]
			[TypeLibFunc(4)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17002BE7 RID: 11239
		// (get) Token: 0x060080CE RID: 32974
		[DispId(-2147417087)]
		public virtual extern object recordNumber
		{
			[DispId(-2147417087)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x17002BE8 RID: 11240
		// (get) Token: 0x060080D0 RID: 32976
		// (set) Token: 0x060080CF RID: 32975
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

		// Token: 0x17002BE9 RID: 11241
		// (get) Token: 0x060080D1 RID: 32977
		[DispId(-2147417104)]
		public virtual extern int offsetLeft
		{
			[DispId(-2147417104)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17002BEA RID: 11242
		// (get) Token: 0x060080D2 RID: 32978
		[DispId(-2147417103)]
		public virtual extern int offsetTop
		{
			[DispId(-2147417103)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17002BEB RID: 11243
		// (get) Token: 0x060080D3 RID: 32979
		[DispId(-2147417102)]
		public virtual extern int offsetWidth
		{
			[DispId(-2147417102)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17002BEC RID: 11244
		// (get) Token: 0x060080D4 RID: 32980
		[DispId(-2147417101)]
		public virtual extern int offsetHeight
		{
			[DispId(-2147417101)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17002BED RID: 11245
		// (get) Token: 0x060080D5 RID: 32981
		[DispId(-2147417100)]
		public virtual extern IHTMLElement offsetParent
		{
			[DispId(-2147417100)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17002BEE RID: 11246
		// (get) Token: 0x060080D7 RID: 32983
		// (set) Token: 0x060080D6 RID: 32982
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

		// Token: 0x17002BEF RID: 11247
		// (get) Token: 0x060080D9 RID: 32985
		// (set) Token: 0x060080D8 RID: 32984
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

		// Token: 0x17002BF0 RID: 11248
		// (get) Token: 0x060080DB RID: 32987
		// (set) Token: 0x060080DA RID: 32986
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

		// Token: 0x17002BF1 RID: 11249
		// (get) Token: 0x060080DD RID: 32989
		// (set) Token: 0x060080DC RID: 32988
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

		// Token: 0x060080DE RID: 32990
		[DispId(-2147417082)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void insertAdjacentHTML([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string html);

		// Token: 0x060080DF RID: 32991
		[DispId(-2147417081)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void insertAdjacentText([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string text);

		// Token: 0x17002BF2 RID: 11250
		// (get) Token: 0x060080E0 RID: 32992
		[DispId(-2147417080)]
		public virtual extern IHTMLElement parentTextEdit
		{
			[DispId(-2147417080)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17002BF3 RID: 11251
		// (get) Token: 0x060080E1 RID: 32993
		[DispId(-2147417078)]
		public virtual extern bool isTextEdit
		{
			[DispId(-2147417078)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x060080E2 RID: 32994
		[DispId(-2147417079)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void click();

		// Token: 0x17002BF4 RID: 11252
		// (get) Token: 0x060080E3 RID: 32995
		[DispId(-2147417077)]
		public virtual extern IHTMLFiltersCollection filters
		{
			[DispId(-2147417077)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17002BF5 RID: 11253
		// (get) Token: 0x060080E5 RID: 32997
		// (set) Token: 0x060080E4 RID: 32996
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

		// Token: 0x060080E6 RID: 32998
		[DispId(-2147417076)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string toString();

		// Token: 0x17002BF6 RID: 11254
		// (get) Token: 0x060080E8 RID: 33000
		// (set) Token: 0x060080E7 RID: 32999
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

		// Token: 0x17002BF7 RID: 11255
		// (get) Token: 0x060080EA RID: 33002
		// (set) Token: 0x060080E9 RID: 33001
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

		// Token: 0x17002BF8 RID: 11256
		// (get) Token: 0x060080EC RID: 33004
		// (set) Token: 0x060080EB RID: 33003
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

		// Token: 0x17002BF9 RID: 11257
		// (get) Token: 0x060080EE RID: 33006
		// (set) Token: 0x060080ED RID: 33005
		[DispId(-2147412094)]
		public virtual extern object onrowexit
		{
			[TypeLibFunc(20)]
			[DispId(-2147412094)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412094)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17002BFA RID: 11258
		// (get) Token: 0x060080F0 RID: 33008
		// (set) Token: 0x060080EF RID: 33007
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

		// Token: 0x17002BFB RID: 11259
		// (get) Token: 0x060080F2 RID: 33010
		// (set) Token: 0x060080F1 RID: 33009
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

		// Token: 0x17002BFC RID: 11260
		// (get) Token: 0x060080F4 RID: 33012
		// (set) Token: 0x060080F3 RID: 33011
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

		// Token: 0x17002BFD RID: 11261
		// (get) Token: 0x060080F6 RID: 33014
		// (set) Token: 0x060080F5 RID: 33013
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

		// Token: 0x17002BFE RID: 11262
		// (get) Token: 0x060080F8 RID: 33016
		// (set) Token: 0x060080F7 RID: 33015
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

		// Token: 0x17002BFF RID: 11263
		// (get) Token: 0x060080F9 RID: 33017
		[DispId(-2147417075)]
		public virtual extern object children
		{
			[DispId(-2147417075)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17002C00 RID: 11264
		// (get) Token: 0x060080FA RID: 33018
		[DispId(-2147417074)]
		public virtual extern object all
		{
			[DispId(-2147417074)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17002C01 RID: 11265
		// (get) Token: 0x060080FB RID: 33019
		[DispId(-2147417073)]
		public virtual extern string scopeName
		{
			[DispId(-2147417073)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x060080FC RID: 33020
		[DispId(-2147417072)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void setCapture([In] bool containerCapture = true);

		// Token: 0x060080FD RID: 33021
		[DispId(-2147417071)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void releaseCapture();

		// Token: 0x17002C02 RID: 11266
		// (get) Token: 0x060080FF RID: 33023
		// (set) Token: 0x060080FE RID: 33022
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

		// Token: 0x06008100 RID: 33024
		[DispId(-2147417070)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string componentFromPoint([In] int x, [In] int y);

		// Token: 0x06008101 RID: 33025
		[DispId(-2147417069)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void doScroll([MarshalAs(27)] [In] [Optional] object component);

		// Token: 0x17002C03 RID: 11267
		// (get) Token: 0x06008103 RID: 33027
		// (set) Token: 0x06008102 RID: 33026
		[DispId(-2147412081)]
		public virtual extern object onscroll
		{
			[TypeLibFunc(20)]
			[DispId(-2147412081)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412081)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17002C04 RID: 11268
		// (get) Token: 0x06008105 RID: 33029
		// (set) Token: 0x06008104 RID: 33028
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

		// Token: 0x17002C05 RID: 11269
		// (get) Token: 0x06008107 RID: 33031
		// (set) Token: 0x06008106 RID: 33030
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

		// Token: 0x17002C06 RID: 11270
		// (get) Token: 0x06008109 RID: 33033
		// (set) Token: 0x06008108 RID: 33032
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

		// Token: 0x17002C07 RID: 11271
		// (get) Token: 0x0600810B RID: 33035
		// (set) Token: 0x0600810A RID: 33034
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

		// Token: 0x17002C08 RID: 11272
		// (get) Token: 0x0600810D RID: 33037
		// (set) Token: 0x0600810C RID: 33036
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

		// Token: 0x17002C09 RID: 11273
		// (get) Token: 0x0600810F RID: 33039
		// (set) Token: 0x0600810E RID: 33038
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

		// Token: 0x17002C0A RID: 11274
		// (get) Token: 0x06008111 RID: 33041
		// (set) Token: 0x06008110 RID: 33040
		[DispId(-2147412054)]
		public virtual extern object onbeforecut
		{
			[TypeLibFunc(20)]
			[DispId(-2147412054)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412054)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17002C0B RID: 11275
		// (get) Token: 0x06008113 RID: 33043
		// (set) Token: 0x06008112 RID: 33042
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

		// Token: 0x17002C0C RID: 11276
		// (get) Token: 0x06008115 RID: 33045
		// (set) Token: 0x06008114 RID: 33044
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

		// Token: 0x17002C0D RID: 11277
		// (get) Token: 0x06008117 RID: 33047
		// (set) Token: 0x06008116 RID: 33046
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

		// Token: 0x17002C0E RID: 11278
		// (get) Token: 0x06008119 RID: 33049
		// (set) Token: 0x06008118 RID: 33048
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

		// Token: 0x17002C0F RID: 11279
		// (get) Token: 0x0600811B RID: 33051
		// (set) Token: 0x0600811A RID: 33050
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

		// Token: 0x17002C10 RID: 11280
		// (get) Token: 0x0600811C RID: 33052
		[DispId(-2147417105)]
		public virtual extern IHTMLCurrentStyle currentStyle
		{
			[DispId(-2147417105)]
			[TypeLibFunc(1024)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17002C11 RID: 11281
		// (get) Token: 0x0600811E RID: 33054
		// (set) Token: 0x0600811D RID: 33053
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

		// Token: 0x0600811F RID: 33055
		[DispId(-2147417068)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLRectCollection getClientRects();

		// Token: 0x06008120 RID: 33056
		[DispId(-2147417067)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLRect getBoundingClientRect();

		// Token: 0x06008121 RID: 33057
		[DispId(-2147417608)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void setExpression([MarshalAs(19)] [In] string propname, [MarshalAs(19)] [In] string expression, [MarshalAs(19)] [In] string language = "");

		// Token: 0x06008122 RID: 33058
		[DispId(-2147417607)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		public virtual extern object getExpression([MarshalAs(19)] [In] string propname);

		// Token: 0x06008123 RID: 33059
		[DispId(-2147417606)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool removeExpression([MarshalAs(19)] [In] string propname);

		// Token: 0x17002C12 RID: 11282
		// (get) Token: 0x06008125 RID: 33061
		// (set) Token: 0x06008124 RID: 33060
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

		// Token: 0x06008126 RID: 33062
		[DispId(-2147416112)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void focus();

		// Token: 0x17002C13 RID: 11283
		// (get) Token: 0x06008128 RID: 33064
		// (set) Token: 0x06008127 RID: 33063
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

		// Token: 0x17002C14 RID: 11284
		// (get) Token: 0x0600812A RID: 33066
		// (set) Token: 0x06008129 RID: 33065
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

		// Token: 0x17002C15 RID: 11285
		// (get) Token: 0x0600812C RID: 33068
		// (set) Token: 0x0600812B RID: 33067
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

		// Token: 0x17002C16 RID: 11286
		// (get) Token: 0x0600812E RID: 33070
		// (set) Token: 0x0600812D RID: 33069
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

		// Token: 0x0600812F RID: 33071
		[DispId(-2147416110)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void blur();

		// Token: 0x06008130 RID: 33072
		[DispId(-2147416095)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void addFilter([MarshalAs(25)] [In] object pUnk);

		// Token: 0x06008131 RID: 33073
		[DispId(-2147416094)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void removeFilter([MarshalAs(25)] [In] object pUnk);

		// Token: 0x17002C17 RID: 11287
		// (get) Token: 0x06008132 RID: 33074
		[DispId(-2147416093)]
		public virtual extern int clientHeight
		{
			[DispId(-2147416093)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17002C18 RID: 11288
		// (get) Token: 0x06008133 RID: 33075
		[DispId(-2147416092)]
		public virtual extern int clientWidth
		{
			[TypeLibFunc(20)]
			[DispId(-2147416092)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17002C19 RID: 11289
		// (get) Token: 0x06008134 RID: 33076
		[DispId(-2147416091)]
		public virtual extern int clientTop
		{
			[DispId(-2147416091)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17002C1A RID: 11290
		// (get) Token: 0x06008135 RID: 33077
		[DispId(-2147416090)]
		public virtual extern int clientLeft
		{
			[TypeLibFunc(20)]
			[DispId(-2147416090)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x06008136 RID: 33078
		[DispId(-2147417605)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool attachEvent([MarshalAs(19)] [In] string @event, [MarshalAs(26)] [In] object pdisp);

		// Token: 0x06008137 RID: 33079
		[DispId(-2147417604)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void detachEvent([MarshalAs(19)] [In] string @event, [MarshalAs(26)] [In] object pdisp);

		// Token: 0x17002C1B RID: 11291
		// (get) Token: 0x06008138 RID: 33080
		[DispId(-2147412996)]
		public virtual extern object readyState
		{
			[DispId(-2147412996)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x17002C1C RID: 11292
		// (get) Token: 0x0600813A RID: 33082
		// (set) Token: 0x06008139 RID: 33081
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

		// Token: 0x17002C1D RID: 11293
		// (get) Token: 0x0600813C RID: 33084
		// (set) Token: 0x0600813B RID: 33083
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

		// Token: 0x17002C1E RID: 11294
		// (get) Token: 0x0600813E RID: 33086
		// (set) Token: 0x0600813D RID: 33085
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

		// Token: 0x17002C1F RID: 11295
		// (get) Token: 0x06008140 RID: 33088
		// (set) Token: 0x0600813F RID: 33087
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

		// Token: 0x17002C20 RID: 11296
		// (get) Token: 0x06008142 RID: 33090
		// (set) Token: 0x06008141 RID: 33089
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

		// Token: 0x06008143 RID: 33091
		[DispId(-2147417056)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(26)]
		public virtual extern object createControlRange();

		// Token: 0x17002C21 RID: 11297
		// (get) Token: 0x06008144 RID: 33092
		[DispId(-2147417055)]
		public virtual extern int scrollHeight
		{
			[TypeLibFunc(20)]
			[DispId(-2147417055)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17002C22 RID: 11298
		// (get) Token: 0x06008145 RID: 33093
		[DispId(-2147417054)]
		public virtual extern int scrollWidth
		{
			[TypeLibFunc(20)]
			[DispId(-2147417054)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17002C23 RID: 11299
		// (get) Token: 0x06008147 RID: 33095
		// (set) Token: 0x06008146 RID: 33094
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

		// Token: 0x17002C24 RID: 11300
		// (get) Token: 0x06008149 RID: 33097
		// (set) Token: 0x06008148 RID: 33096
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

		// Token: 0x0600814A RID: 33098
		[DispId(-2147417050)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void clearAttributes();

		// Token: 0x17002C25 RID: 11301
		// (get) Token: 0x0600814C RID: 33100
		// (set) Token: 0x0600814B RID: 33099
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

		// Token: 0x0600814D RID: 33101
		[DispId(-2147417043)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElement insertAdjacentElement([MarshalAs(19)] [In] string where, [MarshalAs(28)] [In] IHTMLElement insertedElement);

		// Token: 0x0600814E RID: 33102
		[DispId(-2147417047)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElement applyElement([MarshalAs(28)] [In] IHTMLElement apply, [MarshalAs(19)] [In] string where);

		// Token: 0x0600814F RID: 33103
		[DispId(-2147417042)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string getAdjacentText([MarshalAs(19)] [In] string where);

		// Token: 0x06008150 RID: 33104
		[DispId(-2147417041)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string replaceAdjacentText([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string newText);

		// Token: 0x17002C26 RID: 11302
		// (get) Token: 0x06008151 RID: 33105
		[DispId(-2147417040)]
		public virtual extern bool canHaveChildren
		{
			[DispId(-2147417040)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x06008152 RID: 33106
		[DispId(-2147417032)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern int addBehavior([MarshalAs(19)] [In] string bstrUrl, [MarshalAs(27)] [In] [Optional] ref object pvarFactory);

		// Token: 0x06008153 RID: 33107
		[DispId(-2147417031)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool removeBehavior([In] int cookie);

		// Token: 0x17002C27 RID: 11303
		// (get) Token: 0x06008154 RID: 33108
		[DispId(-2147417048)]
		public virtual extern IHTMLStyle runtimeStyle
		{
			[TypeLibFunc(1024)]
			[DispId(-2147417048)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17002C28 RID: 11304
		// (get) Token: 0x06008155 RID: 33109
		[DispId(-2147417030)]
		public virtual extern object behaviorUrns
		{
			[DispId(-2147417030)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17002C29 RID: 11305
		// (get) Token: 0x06008157 RID: 33111
		// (set) Token: 0x06008156 RID: 33110
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

		// Token: 0x17002C2A RID: 11306
		// (get) Token: 0x06008159 RID: 33113
		// (set) Token: 0x06008158 RID: 33112
		[DispId(-2147412043)]
		public virtual extern object onbeforeeditfocus
		{
			[DispId(-2147412043)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412043)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17002C2B RID: 11307
		// (get) Token: 0x0600815A RID: 33114
		[DispId(-2147417028)]
		public virtual extern int readyStateValue
		{
			[DispId(-2147417028)]
			[TypeLibFunc(65)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x0600815B RID: 33115
		[DispId(-2147417027)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElementCollection getElementsByTagName([MarshalAs(19)] [In] string v);

		// Token: 0x0600815C RID: 33116
		[DispId(-2147417016)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void mergeAttributes([MarshalAs(28)] [In] IHTMLElement mergeThis, [MarshalAs(27)] [In] [Optional] ref object pvarFlags);

		// Token: 0x17002C2C RID: 11308
		// (get) Token: 0x0600815D RID: 33117
		[DispId(-2147417015)]
		public virtual extern bool isMultiLine
		{
			[DispId(-2147417015)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17002C2D RID: 11309
		// (get) Token: 0x0600815E RID: 33118
		[DispId(-2147417014)]
		public virtual extern bool canHaveHTML
		{
			[DispId(-2147417014)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17002C2E RID: 11310
		// (get) Token: 0x06008160 RID: 33120
		// (set) Token: 0x0600815F RID: 33119
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

		// Token: 0x17002C2F RID: 11311
		// (get) Token: 0x06008162 RID: 33122
		// (set) Token: 0x06008161 RID: 33121
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

		// Token: 0x17002C30 RID: 11312
		// (get) Token: 0x06008164 RID: 33124
		// (set) Token: 0x06008163 RID: 33123
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

		// Token: 0x17002C31 RID: 11313
		// (get) Token: 0x06008166 RID: 33126
		// (set) Token: 0x06008165 RID: 33125
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

		// Token: 0x06008167 RID: 33127
		[DispId(-2147417011)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void setActive();

		// Token: 0x17002C32 RID: 11314
		// (get) Token: 0x06008169 RID: 33129
		// (set) Token: 0x06008168 RID: 33128
		[DispId(-2147412950)]
		public virtual extern string contentEditable
		{
			[TypeLibFunc(20)]
			[DispId(-2147412950)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412950)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17002C33 RID: 11315
		// (get) Token: 0x0600816A RID: 33130
		[DispId(-2147417010)]
		public virtual extern bool isContentEditable
		{
			[DispId(-2147417010)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17002C34 RID: 11316
		// (get) Token: 0x0600816C RID: 33132
		// (set) Token: 0x0600816B RID: 33131
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

		// Token: 0x17002C35 RID: 11317
		// (get) Token: 0x0600816E RID: 33134
		// (set) Token: 0x0600816D RID: 33133
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

		// Token: 0x17002C36 RID: 11318
		// (get) Token: 0x0600816F RID: 33135
		[DispId(-2147417007)]
		public virtual extern bool isDisabled
		{
			[DispId(-2147417007)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17002C37 RID: 11319
		// (get) Token: 0x06008171 RID: 33137
		// (set) Token: 0x06008170 RID: 33136
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

		// Token: 0x17002C38 RID: 11320
		// (get) Token: 0x06008173 RID: 33139
		// (set) Token: 0x06008172 RID: 33138
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

		// Token: 0x06008174 RID: 33140
		[DispId(-2147417006)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool FireEvent([MarshalAs(19)] [In] string bstrEventName, [MarshalAs(27)] [In] [Optional] ref object pvarEventObject);

		// Token: 0x17002C39 RID: 11321
		// (get) Token: 0x06008176 RID: 33142
		// (set) Token: 0x06008175 RID: 33141
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

		// Token: 0x17002C3A RID: 11322
		// (get) Token: 0x06008178 RID: 33144
		// (set) Token: 0x06008177 RID: 33143
		[DispId(-2147412028)]
		public virtual extern object onresizeend
		{
			[TypeLibFunc(20)]
			[DispId(-2147412028)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412028)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17002C3B RID: 11323
		// (get) Token: 0x0600817A RID: 33146
		// (set) Token: 0x06008179 RID: 33145
		[DispId(-2147412031)]
		public virtual extern object onmovestart
		{
			[DispId(-2147412031)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412031)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17002C3C RID: 11324
		// (get) Token: 0x0600817C RID: 33148
		// (set) Token: 0x0600817B RID: 33147
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

		// Token: 0x17002C3D RID: 11325
		// (get) Token: 0x0600817E RID: 33150
		// (set) Token: 0x0600817D RID: 33149
		[DispId(-2147412027)]
		public virtual extern object onmouseenter
		{
			[DispId(-2147412027)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412027)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17002C3E RID: 11326
		// (get) Token: 0x06008180 RID: 33152
		// (set) Token: 0x0600817F RID: 33151
		[DispId(-2147412026)]
		public virtual extern object onmouseleave
		{
			[TypeLibFunc(20)]
			[DispId(-2147412026)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412026)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17002C3F RID: 11327
		// (get) Token: 0x06008182 RID: 33154
		// (set) Token: 0x06008181 RID: 33153
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

		// Token: 0x17002C40 RID: 11328
		// (get) Token: 0x06008184 RID: 33156
		// (set) Token: 0x06008183 RID: 33155
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

		// Token: 0x06008185 RID: 33157
		[DispId(-2147417005)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool dragDrop();

		// Token: 0x17002C41 RID: 11329
		// (get) Token: 0x06008186 RID: 33158
		[DispId(-2147417004)]
		public virtual extern int glyphMode
		{
			[TypeLibFunc(1089)]
			[DispId(-2147417004)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17002C42 RID: 11330
		// (get) Token: 0x06008188 RID: 33160
		// (set) Token: 0x06008187 RID: 33159
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

		// Token: 0x06008189 RID: 33161
		[DispId(-2147417000)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void normalize();

		// Token: 0x0600818A RID: 33162
		[DispId(-2147417003)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute getAttributeNode([MarshalAs(19)] [In] string bstrName);

		// Token: 0x0600818B RID: 33163
		[DispId(-2147417002)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute setAttributeNode([MarshalAs(28)] [In] IHTMLDOMAttribute pattr);

		// Token: 0x0600818C RID: 33164
		[DispId(-2147417001)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute removeAttributeNode([MarshalAs(28)] [In] IHTMLDOMAttribute pattr);

		// Token: 0x17002C43 RID: 11331
		// (get) Token: 0x0600818E RID: 33166
		// (set) Token: 0x0600818D RID: 33165
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

		// Token: 0x17002C44 RID: 11332
		// (get) Token: 0x06008190 RID: 33168
		// (set) Token: 0x0600818F RID: 33167
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

		// Token: 0x17002C45 RID: 11333
		// (get) Token: 0x06008192 RID: 33170
		// (set) Token: 0x06008191 RID: 33169
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

		// Token: 0x17002C46 RID: 11334
		// (get) Token: 0x06008193 RID: 33171
		[DispId(-2147417058)]
		public virtual extern int uniqueNumber
		{
			[TypeLibFunc(64)]
			[DispId(-2147417058)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17002C47 RID: 11335
		// (get) Token: 0x06008194 RID: 33172
		[DispId(-2147417057)]
		public virtual extern string uniqueID
		{
			[DispId(-2147417057)]
			[TypeLibFunc(64)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17002C48 RID: 11336
		// (get) Token: 0x06008195 RID: 33173
		[DispId(-2147417066)]
		public virtual extern int nodeType
		{
			[DispId(-2147417066)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17002C49 RID: 11337
		// (get) Token: 0x06008196 RID: 33174
		[DispId(-2147417065)]
		public virtual extern IHTMLDOMNode parentNode
		{
			[DispId(-2147417065)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x06008197 RID: 33175
		[DispId(-2147417064)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool hasChildNodes();

		// Token: 0x17002C4A RID: 11338
		// (get) Token: 0x06008198 RID: 33176
		[DispId(-2147417063)]
		public virtual extern object childNodes
		{
			[DispId(-2147417063)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17002C4B RID: 11339
		// (get) Token: 0x06008199 RID: 33177
		[DispId(-2147417062)]
		public virtual extern object attributes
		{
			[DispId(-2147417062)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x0600819A RID: 33178
		[DispId(-2147417061)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode insertBefore([MarshalAs(28)] [In] IHTMLDOMNode newChild, [MarshalAs(27)] [In] [Optional] object refChild);

		// Token: 0x0600819B RID: 33179
		[DispId(-2147417060)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode removeChild([MarshalAs(28)] [In] IHTMLDOMNode oldChild);

		// Token: 0x0600819C RID: 33180
		[DispId(-2147417059)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode replaceChild([MarshalAs(28)] [In] IHTMLDOMNode newChild, [MarshalAs(28)] [In] IHTMLDOMNode oldChild);

		// Token: 0x0600819D RID: 33181
		[DispId(-2147417051)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode cloneNode([In] bool fDeep);

		// Token: 0x0600819E RID: 33182
		[DispId(-2147417046)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode removeNode([In] bool fDeep = false);

		// Token: 0x0600819F RID: 33183
		[DispId(-2147417044)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode swapNode([MarshalAs(28)] [In] IHTMLDOMNode otherNode);

		// Token: 0x060081A0 RID: 33184
		[DispId(-2147417045)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode replaceNode([MarshalAs(28)] [In] IHTMLDOMNode replacement);

		// Token: 0x060081A1 RID: 33185
		[DispId(-2147417039)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode appendChild([MarshalAs(28)] [In] IHTMLDOMNode newChild);

		// Token: 0x17002C4C RID: 11340
		// (get) Token: 0x060081A2 RID: 33186
		[DispId(-2147417038)]
		public virtual extern string nodeName
		{
			[DispId(-2147417038)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17002C4D RID: 11341
		// (get) Token: 0x060081A4 RID: 33188
		// (set) Token: 0x060081A3 RID: 33187
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

		// Token: 0x17002C4E RID: 11342
		// (get) Token: 0x060081A5 RID: 33189
		[DispId(-2147417036)]
		public virtual extern IHTMLDOMNode firstChild
		{
			[DispId(-2147417036)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17002C4F RID: 11343
		// (get) Token: 0x060081A6 RID: 33190
		[DispId(-2147417035)]
		public virtual extern IHTMLDOMNode lastChild
		{
			[DispId(-2147417035)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17002C50 RID: 11344
		// (get) Token: 0x060081A7 RID: 33191
		[DispId(-2147417034)]
		public virtual extern IHTMLDOMNode previousSibling
		{
			[DispId(-2147417034)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17002C51 RID: 11345
		// (get) Token: 0x060081A8 RID: 33192
		[DispId(-2147417033)]
		public virtual extern IHTMLDOMNode nextSibling
		{
			[DispId(-2147417033)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17002C52 RID: 11346
		// (get) Token: 0x060081A9 RID: 33193
		[DispId(-2147416999)]
		public virtual extern object ownerDocument
		{
			[DispId(-2147416999)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17002C53 RID: 11347
		// (get) Token: 0x060081AB RID: 33195
		// (set) Token: 0x060081AA RID: 33194
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

		// Token: 0x17002C54 RID: 11348
		// (get) Token: 0x060081AD RID: 33197
		// (set) Token: 0x060081AC RID: 33196
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

		// Token: 0x17002C55 RID: 11349
		// (get) Token: 0x060081AF RID: 33199
		// (set) Token: 0x060081AE RID: 33198
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

		// Token: 0x17002C56 RID: 11350
		// (get) Token: 0x060081B1 RID: 33201
		// (set) Token: 0x060081B0 RID: 33200
		[DispId(1002)]
		public virtual extern int size
		{
			[DispId(1002)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[TypeLibFunc(20)]
			[DispId(1002)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x17002C57 RID: 11351
		// (get) Token: 0x060081B3 RID: 33203
		// (set) Token: 0x060081B2 RID: 33202
		[DispId(1003)]
		public virtual extern bool multiple
		{
			[DispId(1003)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[TypeLibFunc(20)]
			[DispId(1003)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x17002C58 RID: 11352
		// (get) Token: 0x060081B5 RID: 33205
		// (set) Token: 0x060081B4 RID: 33204
		[DispId(-2147418112)]
		public virtual extern string name
		{
			[DispId(-2147418112)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147418112)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17002C59 RID: 11353
		// (get) Token: 0x060081B6 RID: 33206
		[DispId(1005)]
		public virtual extern object options
		{
			[DispId(1005)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17002C5A RID: 11354
		// (get) Token: 0x060081B8 RID: 33208
		// (set) Token: 0x060081B7 RID: 33207
		[DispId(-2147412082)]
		public virtual extern object onchange
		{
			[TypeLibFunc(20)]
			[DispId(-2147412082)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412082)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17002C5B RID: 11355
		// (get) Token: 0x060081BA RID: 33210
		// (set) Token: 0x060081B9 RID: 33209
		[DispId(1010)]
		public virtual extern int selectedIndex
		{
			[DispId(1010)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[DispId(1010)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x17002C5C RID: 11356
		// (get) Token: 0x060081BB RID: 33211
		[DispId(1012)]
		public virtual extern string type
		{
			[DispId(1012)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17002C5D RID: 11357
		// (get) Token: 0x060081BD RID: 33213
		// (set) Token: 0x060081BC RID: 33212
		[DispId(1011)]
		public virtual extern string value
		{
			[DispId(1011)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(1011)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17002C5E RID: 11358
		// (get) Token: 0x060081BE RID: 33214
		[DispId(-2147416108)]
		public virtual extern IHTMLFormElement form
		{
			[DispId(-2147416108)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x060081BF RID: 33215
		[DispId(1503)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void add([MarshalAs(28)] [In] IHTMLElement element, [MarshalAs(27)] [In] [Optional] object before);

		// Token: 0x060081C0 RID: 33216
		[DispId(1504)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void remove([In] int index = -1);

		// Token: 0x17002C5F RID: 11359
		// (get) Token: 0x060081C2 RID: 33218
		// (set) Token: 0x060081C1 RID: 33217
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

		// Token: 0x060081C3 RID: 33219
		[DispId(-4)]
		[TypeLibFunc(65)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(44, MarshalTypeRef = System.Runtime.InteropServices.CustomMarshalers.EnumeratorToEnumVariantMarshaler)]
		public virtual extern IEnumerator GetEnumerator();

		// Token: 0x060081C4 RID: 33220
		[DispId(0)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(26)]
		public virtual extern object item([MarshalAs(27)] [In] [Optional] object name, [MarshalAs(27)] [In] [Optional] object index);

		// Token: 0x060081C5 RID: 33221
		[DispId(1502)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(26)]
		public virtual extern object tags([MarshalAs(27)] [In] object tagName);

		// Token: 0x060081C6 RID: 33222
		[DispId(1505)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(26)]
		public virtual extern object urns([MarshalAs(27)] [In] object urn);

		// Token: 0x060081C7 RID: 33223
		[DispId(1506)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(26)]
		public virtual extern object namedItem([MarshalAs(19)] [In] string name);

		// Token: 0x060081C8 RID: 33224
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement_setAttribute([MarshalAs(19)] [In] string strAttributeName, [MarshalAs(27)] [In] object AttributeValue, [In] int lFlags = 1);

		// Token: 0x060081C9 RID: 33225
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		public virtual extern object IHTMLElement_getAttribute([MarshalAs(19)] [In] string strAttributeName, [In] int lFlags = 0);

		// Token: 0x060081CA RID: 33226
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement_removeAttribute([MarshalAs(19)] [In] string strAttributeName, [In] int lFlags = 1);

		// Token: 0x17002C60 RID: 11360
		// (get) Token: 0x060081CC RID: 33228
		// (set) Token: 0x060081CB RID: 33227
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

		// Token: 0x17002C61 RID: 11361
		// (get) Token: 0x060081CE RID: 33230
		// (set) Token: 0x060081CD RID: 33229
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

		// Token: 0x17002C62 RID: 11362
		// (get) Token: 0x060081CF RID: 33231
		public virtual extern string IHTMLElement_tagName
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17002C63 RID: 11363
		// (get) Token: 0x060081D0 RID: 33232
		public virtual extern IHTMLElement IHTMLElement_parentElement
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17002C64 RID: 11364
		// (get) Token: 0x060081D1 RID: 33233
		public virtual extern IHTMLStyle IHTMLElement_style
		{
			[TypeLibFunc(1024)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17002C65 RID: 11365
		// (get) Token: 0x060081D3 RID: 33235
		// (set) Token: 0x060081D2 RID: 33234
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

		// Token: 0x17002C66 RID: 11366
		// (get) Token: 0x060081D5 RID: 33237
		// (set) Token: 0x060081D4 RID: 33236
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

		// Token: 0x17002C67 RID: 11367
		// (get) Token: 0x060081D7 RID: 33239
		// (set) Token: 0x060081D6 RID: 33238
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

		// Token: 0x17002C68 RID: 11368
		// (get) Token: 0x060081D9 RID: 33241
		// (set) Token: 0x060081D8 RID: 33240
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

		// Token: 0x17002C69 RID: 11369
		// (get) Token: 0x060081DB RID: 33243
		// (set) Token: 0x060081DA RID: 33242
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

		// Token: 0x17002C6A RID: 11370
		// (get) Token: 0x060081DD RID: 33245
		// (set) Token: 0x060081DC RID: 33244
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

		// Token: 0x17002C6B RID: 11371
		// (get) Token: 0x060081DF RID: 33247
		// (set) Token: 0x060081DE RID: 33246
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

		// Token: 0x17002C6C RID: 11372
		// (get) Token: 0x060081E1 RID: 33249
		// (set) Token: 0x060081E0 RID: 33248
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

		// Token: 0x17002C6D RID: 11373
		// (get) Token: 0x060081E3 RID: 33251
		// (set) Token: 0x060081E2 RID: 33250
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

		// Token: 0x17002C6E RID: 11374
		// (get) Token: 0x060081E5 RID: 33253
		// (set) Token: 0x060081E4 RID: 33252
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

		// Token: 0x17002C6F RID: 11375
		// (get) Token: 0x060081E7 RID: 33255
		// (set) Token: 0x060081E6 RID: 33254
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

		// Token: 0x17002C70 RID: 11376
		// (get) Token: 0x060081E8 RID: 33256
		public virtual extern object IHTMLElement_document
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17002C71 RID: 11377
		// (get) Token: 0x060081EA RID: 33258
		// (set) Token: 0x060081E9 RID: 33257
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

		// Token: 0x17002C72 RID: 11378
		// (get) Token: 0x060081EC RID: 33260
		// (set) Token: 0x060081EB RID: 33259
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

		// Token: 0x17002C73 RID: 11379
		// (get) Token: 0x060081EE RID: 33262
		// (set) Token: 0x060081ED RID: 33261
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

		// Token: 0x060081EF RID: 33263
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement_scrollIntoView([MarshalAs(27)] [In] [Optional] object varargStart);

		// Token: 0x060081F0 RID: 33264
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement_contains([MarshalAs(28)] [In] IHTMLElement pChild);

		// Token: 0x17002C74 RID: 11380
		// (get) Token: 0x060081F1 RID: 33265
		public virtual extern int IHTMLElement_sourceIndex
		{
			[TypeLibFunc(4)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17002C75 RID: 11381
		// (get) Token: 0x060081F2 RID: 33266
		public virtual extern object IHTMLElement_recordNumber
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x17002C76 RID: 11382
		// (get) Token: 0x060081F4 RID: 33268
		// (set) Token: 0x060081F3 RID: 33267
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

		// Token: 0x17002C77 RID: 11383
		// (get) Token: 0x060081F5 RID: 33269
		public virtual extern int IHTMLElement_offsetLeft
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17002C78 RID: 11384
		// (get) Token: 0x060081F6 RID: 33270
		public virtual extern int IHTMLElement_offsetTop
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17002C79 RID: 11385
		// (get) Token: 0x060081F7 RID: 33271
		public virtual extern int IHTMLElement_offsetWidth
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17002C7A RID: 11386
		// (get) Token: 0x060081F8 RID: 33272
		public virtual extern int IHTMLElement_offsetHeight
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17002C7B RID: 11387
		// (get) Token: 0x060081F9 RID: 33273
		public virtual extern IHTMLElement IHTMLElement_offsetParent
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17002C7C RID: 11388
		// (get) Token: 0x060081FB RID: 33275
		// (set) Token: 0x060081FA RID: 33274
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

		// Token: 0x17002C7D RID: 11389
		// (get) Token: 0x060081FD RID: 33277
		// (set) Token: 0x060081FC RID: 33276
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

		// Token: 0x17002C7E RID: 11390
		// (get) Token: 0x060081FF RID: 33279
		// (set) Token: 0x060081FE RID: 33278
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

		// Token: 0x17002C7F RID: 11391
		// (get) Token: 0x06008201 RID: 33281
		// (set) Token: 0x06008200 RID: 33280
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

		// Token: 0x06008202 RID: 33282
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement_insertAdjacentHTML([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string html);

		// Token: 0x06008203 RID: 33283
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement_insertAdjacentText([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string text);

		// Token: 0x17002C80 RID: 11392
		// (get) Token: 0x06008204 RID: 33284
		public virtual extern IHTMLElement IHTMLElement_parentTextEdit
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17002C81 RID: 11393
		// (get) Token: 0x06008205 RID: 33285
		public virtual extern bool IHTMLElement_isTextEdit
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x06008206 RID: 33286
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement_click();

		// Token: 0x17002C82 RID: 11394
		// (get) Token: 0x06008207 RID: 33287
		public virtual extern IHTMLFiltersCollection IHTMLElement_filters
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17002C83 RID: 11395
		// (get) Token: 0x06008209 RID: 33289
		// (set) Token: 0x06008208 RID: 33288
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

		// Token: 0x0600820A RID: 33290
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string IHTMLElement_toString();

		// Token: 0x17002C84 RID: 11396
		// (get) Token: 0x0600820C RID: 33292
		// (set) Token: 0x0600820B RID: 33291
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

		// Token: 0x17002C85 RID: 11397
		// (get) Token: 0x0600820E RID: 33294
		// (set) Token: 0x0600820D RID: 33293
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

		// Token: 0x17002C86 RID: 11398
		// (get) Token: 0x06008210 RID: 33296
		// (set) Token: 0x0600820F RID: 33295
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

		// Token: 0x17002C87 RID: 11399
		// (get) Token: 0x06008212 RID: 33298
		// (set) Token: 0x06008211 RID: 33297
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

		// Token: 0x17002C88 RID: 11400
		// (get) Token: 0x06008214 RID: 33300
		// (set) Token: 0x06008213 RID: 33299
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

		// Token: 0x17002C89 RID: 11401
		// (get) Token: 0x06008216 RID: 33302
		// (set) Token: 0x06008215 RID: 33301
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

		// Token: 0x17002C8A RID: 11402
		// (get) Token: 0x06008218 RID: 33304
		// (set) Token: 0x06008217 RID: 33303
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

		// Token: 0x17002C8B RID: 11403
		// (get) Token: 0x0600821A RID: 33306
		// (set) Token: 0x06008219 RID: 33305
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

		// Token: 0x17002C8C RID: 11404
		// (get) Token: 0x0600821C RID: 33308
		// (set) Token: 0x0600821B RID: 33307
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

		// Token: 0x17002C8D RID: 11405
		// (get) Token: 0x0600821D RID: 33309
		public virtual extern object IHTMLElement_children
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17002C8E RID: 11406
		// (get) Token: 0x0600821E RID: 33310
		public virtual extern object IHTMLElement_all
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17002C8F RID: 11407
		// (get) Token: 0x0600821F RID: 33311
		public virtual extern string IHTMLElement2_scopeName
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x06008220 RID: 33312
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_setCapture([In] bool containerCapture = true);

		// Token: 0x06008221 RID: 33313
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_releaseCapture();

		// Token: 0x17002C90 RID: 11408
		// (get) Token: 0x06008223 RID: 33315
		// (set) Token: 0x06008222 RID: 33314
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

		// Token: 0x06008224 RID: 33316
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string IHTMLElement2_componentFromPoint([In] int x, [In] int y);

		// Token: 0x06008225 RID: 33317
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_doScroll([MarshalAs(27)] [In] [Optional] object component);

		// Token: 0x17002C91 RID: 11409
		// (get) Token: 0x06008227 RID: 33319
		// (set) Token: 0x06008226 RID: 33318
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

		// Token: 0x17002C92 RID: 11410
		// (get) Token: 0x06008229 RID: 33321
		// (set) Token: 0x06008228 RID: 33320
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

		// Token: 0x17002C93 RID: 11411
		// (get) Token: 0x0600822B RID: 33323
		// (set) Token: 0x0600822A RID: 33322
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

		// Token: 0x17002C94 RID: 11412
		// (get) Token: 0x0600822D RID: 33325
		// (set) Token: 0x0600822C RID: 33324
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

		// Token: 0x17002C95 RID: 11413
		// (get) Token: 0x0600822F RID: 33327
		// (set) Token: 0x0600822E RID: 33326
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

		// Token: 0x17002C96 RID: 11414
		// (get) Token: 0x06008231 RID: 33329
		// (set) Token: 0x06008230 RID: 33328
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

		// Token: 0x17002C97 RID: 11415
		// (get) Token: 0x06008233 RID: 33331
		// (set) Token: 0x06008232 RID: 33330
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

		// Token: 0x17002C98 RID: 11416
		// (get) Token: 0x06008235 RID: 33333
		// (set) Token: 0x06008234 RID: 33332
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

		// Token: 0x17002C99 RID: 11417
		// (get) Token: 0x06008237 RID: 33335
		// (set) Token: 0x06008236 RID: 33334
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

		// Token: 0x17002C9A RID: 11418
		// (get) Token: 0x06008239 RID: 33337
		// (set) Token: 0x06008238 RID: 33336
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

		// Token: 0x17002C9B RID: 11419
		// (get) Token: 0x0600823B RID: 33339
		// (set) Token: 0x0600823A RID: 33338
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

		// Token: 0x17002C9C RID: 11420
		// (get) Token: 0x0600823D RID: 33341
		// (set) Token: 0x0600823C RID: 33340
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

		// Token: 0x17002C9D RID: 11421
		// (get) Token: 0x0600823F RID: 33343
		// (set) Token: 0x0600823E RID: 33342
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

		// Token: 0x17002C9E RID: 11422
		// (get) Token: 0x06008240 RID: 33344
		public virtual extern IHTMLCurrentStyle IHTMLElement2_currentStyle
		{
			[TypeLibFunc(1024)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17002C9F RID: 11423
		// (get) Token: 0x06008242 RID: 33346
		// (set) Token: 0x06008241 RID: 33345
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

		// Token: 0x06008243 RID: 33347
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLRectCollection IHTMLElement2_getClientRects();

		// Token: 0x06008244 RID: 33348
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLRect IHTMLElement2_getBoundingClientRect();

		// Token: 0x06008245 RID: 33349
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_setExpression([MarshalAs(19)] [In] string propname, [MarshalAs(19)] [In] string expression, [MarshalAs(19)] [In] string language = "");

		// Token: 0x06008246 RID: 33350
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		public virtual extern object IHTMLElement2_getExpression([MarshalAs(19)] [In] string propname);

		// Token: 0x06008247 RID: 33351
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement2_removeExpression([MarshalAs(19)] [In] string propname);

		// Token: 0x17002CA0 RID: 11424
		// (get) Token: 0x06008249 RID: 33353
		// (set) Token: 0x06008248 RID: 33352
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

		// Token: 0x0600824A RID: 33354
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_focus();

		// Token: 0x17002CA1 RID: 11425
		// (get) Token: 0x0600824C RID: 33356
		// (set) Token: 0x0600824B RID: 33355
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

		// Token: 0x17002CA2 RID: 11426
		// (get) Token: 0x0600824E RID: 33358
		// (set) Token: 0x0600824D RID: 33357
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

		// Token: 0x17002CA3 RID: 11427
		// (get) Token: 0x06008250 RID: 33360
		// (set) Token: 0x0600824F RID: 33359
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

		// Token: 0x17002CA4 RID: 11428
		// (get) Token: 0x06008252 RID: 33362
		// (set) Token: 0x06008251 RID: 33361
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

		// Token: 0x06008253 RID: 33363
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_blur();

		// Token: 0x06008254 RID: 33364
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_addFilter([MarshalAs(25)] [In] object pUnk);

		// Token: 0x06008255 RID: 33365
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_removeFilter([MarshalAs(25)] [In] object pUnk);

		// Token: 0x17002CA5 RID: 11429
		// (get) Token: 0x06008256 RID: 33366
		public virtual extern int IHTMLElement2_clientHeight
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17002CA6 RID: 11430
		// (get) Token: 0x06008257 RID: 33367
		public virtual extern int IHTMLElement2_clientWidth
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17002CA7 RID: 11431
		// (get) Token: 0x06008258 RID: 33368
		public virtual extern int IHTMLElement2_clientTop
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17002CA8 RID: 11432
		// (get) Token: 0x06008259 RID: 33369
		public virtual extern int IHTMLElement2_clientLeft
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x0600825A RID: 33370
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement2_attachEvent([MarshalAs(19)] [In] string @event, [MarshalAs(26)] [In] object pdisp);

		// Token: 0x0600825B RID: 33371
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_detachEvent([MarshalAs(19)] [In] string @event, [MarshalAs(26)] [In] object pdisp);

		// Token: 0x17002CA9 RID: 11433
		// (get) Token: 0x0600825C RID: 33372
		public virtual extern object IHTMLElement2_readyState
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x17002CAA RID: 11434
		// (get) Token: 0x0600825E RID: 33374
		// (set) Token: 0x0600825D RID: 33373
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

		// Token: 0x17002CAB RID: 11435
		// (get) Token: 0x06008260 RID: 33376
		// (set) Token: 0x0600825F RID: 33375
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

		// Token: 0x17002CAC RID: 11436
		// (get) Token: 0x06008262 RID: 33378
		// (set) Token: 0x06008261 RID: 33377
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

		// Token: 0x17002CAD RID: 11437
		// (get) Token: 0x06008264 RID: 33380
		// (set) Token: 0x06008263 RID: 33379
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

		// Token: 0x17002CAE RID: 11438
		// (get) Token: 0x06008266 RID: 33382
		// (set) Token: 0x06008265 RID: 33381
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

		// Token: 0x06008267 RID: 33383
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(26)]
		public virtual extern object IHTMLElement2_createControlRange();

		// Token: 0x17002CAF RID: 11439
		// (get) Token: 0x06008268 RID: 33384
		public virtual extern int IHTMLElement2_scrollHeight
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17002CB0 RID: 11440
		// (get) Token: 0x06008269 RID: 33385
		public virtual extern int IHTMLElement2_scrollWidth
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17002CB1 RID: 11441
		// (get) Token: 0x0600826B RID: 33387
		// (set) Token: 0x0600826A RID: 33386
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

		// Token: 0x17002CB2 RID: 11442
		// (get) Token: 0x0600826D RID: 33389
		// (set) Token: 0x0600826C RID: 33388
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

		// Token: 0x0600826E RID: 33390
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_clearAttributes();

		// Token: 0x0600826F RID: 33391
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void mergeAttributes([MarshalAs(28)] [In] IHTMLElement mergeThis);

		// Token: 0x17002CB3 RID: 11443
		// (get) Token: 0x06008271 RID: 33393
		// (set) Token: 0x06008270 RID: 33392
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

		// Token: 0x06008272 RID: 33394
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElement IHTMLElement2_insertAdjacentElement([MarshalAs(19)] [In] string where, [MarshalAs(28)] [In] IHTMLElement insertedElement);

		// Token: 0x06008273 RID: 33395
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElement IHTMLElement2_applyElement([MarshalAs(28)] [In] IHTMLElement apply, [MarshalAs(19)] [In] string where);

		// Token: 0x06008274 RID: 33396
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string IHTMLElement2_getAdjacentText([MarshalAs(19)] [In] string where);

		// Token: 0x06008275 RID: 33397
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string IHTMLElement2_replaceAdjacentText([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string newText);

		// Token: 0x17002CB4 RID: 11444
		// (get) Token: 0x06008276 RID: 33398
		public virtual extern bool IHTMLElement2_canHaveChildren
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x06008277 RID: 33399
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern int IHTMLElement2_addBehavior([MarshalAs(19)] [In] string bstrUrl, [MarshalAs(27)] [In] [Optional] ref object pvarFactory);

		// Token: 0x06008278 RID: 33400
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement2_removeBehavior([In] int cookie);

		// Token: 0x17002CB5 RID: 11445
		// (get) Token: 0x06008279 RID: 33401
		public virtual extern IHTMLStyle IHTMLElement2_runtimeStyle
		{
			[TypeLibFunc(1024)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17002CB6 RID: 11446
		// (get) Token: 0x0600827A RID: 33402
		public virtual extern object IHTMLElement2_behaviorUrns
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17002CB7 RID: 11447
		// (get) Token: 0x0600827C RID: 33404
		// (set) Token: 0x0600827B RID: 33403
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

		// Token: 0x17002CB8 RID: 11448
		// (get) Token: 0x0600827E RID: 33406
		// (set) Token: 0x0600827D RID: 33405
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

		// Token: 0x17002CB9 RID: 11449
		// (get) Token: 0x0600827F RID: 33407
		public virtual extern int IHTMLElement2_readyStateValue
		{
			[TypeLibFunc(65)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x06008280 RID: 33408
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElementCollection IHTMLElement2_getElementsByTagName([MarshalAs(19)] [In] string v);

		// Token: 0x06008281 RID: 33409
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void mergeAttributes([MarshalAs(28)] [In] IHTMLElement mergeThis, [MarshalAs(27)] [In] [Optional] ref object pvarFlags);

		// Token: 0x17002CBA RID: 11450
		// (get) Token: 0x06008282 RID: 33410
		public virtual extern bool IHTMLElement3_isMultiLine
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17002CBB RID: 11451
		// (get) Token: 0x06008283 RID: 33411
		public virtual extern bool IHTMLElement3_canHaveHTML
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17002CBC RID: 11452
		// (get) Token: 0x06008285 RID: 33413
		// (set) Token: 0x06008284 RID: 33412
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

		// Token: 0x17002CBD RID: 11453
		// (get) Token: 0x06008287 RID: 33415
		// (set) Token: 0x06008286 RID: 33414
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

		// Token: 0x17002CBE RID: 11454
		// (get) Token: 0x06008289 RID: 33417
		// (set) Token: 0x06008288 RID: 33416
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

		// Token: 0x17002CBF RID: 11455
		// (get) Token: 0x0600828B RID: 33419
		// (set) Token: 0x0600828A RID: 33418
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

		// Token: 0x0600828C RID: 33420
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement3_setActive();

		// Token: 0x17002CC0 RID: 11456
		// (get) Token: 0x0600828E RID: 33422
		// (set) Token: 0x0600828D RID: 33421
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

		// Token: 0x17002CC1 RID: 11457
		// (get) Token: 0x0600828F RID: 33423
		public virtual extern bool IHTMLElement3_isContentEditable
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17002CC2 RID: 11458
		// (get) Token: 0x06008291 RID: 33425
		// (set) Token: 0x06008290 RID: 33424
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

		// Token: 0x17002CC3 RID: 11459
		// (get) Token: 0x06008293 RID: 33427
		// (set) Token: 0x06008292 RID: 33426
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

		// Token: 0x17002CC4 RID: 11460
		// (get) Token: 0x06008294 RID: 33428
		public virtual extern bool IHTMLElement3_isDisabled
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17002CC5 RID: 11461
		// (get) Token: 0x06008296 RID: 33430
		// (set) Token: 0x06008295 RID: 33429
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

		// Token: 0x17002CC6 RID: 11462
		// (get) Token: 0x06008298 RID: 33432
		// (set) Token: 0x06008297 RID: 33431
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

		// Token: 0x06008299 RID: 33433
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement3_FireEvent([MarshalAs(19)] [In] string bstrEventName, [MarshalAs(27)] [In] [Optional] ref object pvarEventObject);

		// Token: 0x17002CC7 RID: 11463
		// (get) Token: 0x0600829B RID: 33435
		// (set) Token: 0x0600829A RID: 33434
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

		// Token: 0x17002CC8 RID: 11464
		// (get) Token: 0x0600829D RID: 33437
		// (set) Token: 0x0600829C RID: 33436
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

		// Token: 0x17002CC9 RID: 11465
		// (get) Token: 0x0600829F RID: 33439
		// (set) Token: 0x0600829E RID: 33438
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

		// Token: 0x17002CCA RID: 11466
		// (get) Token: 0x060082A1 RID: 33441
		// (set) Token: 0x060082A0 RID: 33440
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

		// Token: 0x17002CCB RID: 11467
		// (get) Token: 0x060082A3 RID: 33443
		// (set) Token: 0x060082A2 RID: 33442
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

		// Token: 0x17002CCC RID: 11468
		// (get) Token: 0x060082A5 RID: 33445
		// (set) Token: 0x060082A4 RID: 33444
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

		// Token: 0x17002CCD RID: 11469
		// (get) Token: 0x060082A7 RID: 33447
		// (set) Token: 0x060082A6 RID: 33446
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

		// Token: 0x17002CCE RID: 11470
		// (get) Token: 0x060082A9 RID: 33449
		// (set) Token: 0x060082A8 RID: 33448
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

		// Token: 0x060082AA RID: 33450
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement3_dragDrop();

		// Token: 0x17002CCF RID: 11471
		// (get) Token: 0x060082AB RID: 33451
		public virtual extern int IHTMLElement3_glyphMode
		{
			[TypeLibFunc(1089)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17002CD0 RID: 11472
		// (get) Token: 0x060082AD RID: 33453
		// (set) Token: 0x060082AC RID: 33452
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

		// Token: 0x060082AE RID: 33454
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement4_normalize();

		// Token: 0x060082AF RID: 33455
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute IHTMLElement4_getAttributeNode([MarshalAs(19)] [In] string bstrName);

		// Token: 0x060082B0 RID: 33456
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute IHTMLElement4_setAttributeNode([MarshalAs(28)] [In] IHTMLDOMAttribute pattr);

		// Token: 0x060082B1 RID: 33457
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute IHTMLElement4_removeAttributeNode([MarshalAs(28)] [In] IHTMLDOMAttribute pattr);

		// Token: 0x17002CD1 RID: 11473
		// (get) Token: 0x060082B3 RID: 33459
		// (set) Token: 0x060082B2 RID: 33458
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

		// Token: 0x17002CD2 RID: 11474
		// (get) Token: 0x060082B5 RID: 33461
		// (set) Token: 0x060082B4 RID: 33460
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

		// Token: 0x17002CD3 RID: 11475
		// (get) Token: 0x060082B7 RID: 33463
		// (set) Token: 0x060082B6 RID: 33462
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

		// Token: 0x17002CD4 RID: 11476
		// (get) Token: 0x060082B8 RID: 33464
		public virtual extern int IHTMLUniqueName_uniqueNumber
		{
			[TypeLibFunc(64)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17002CD5 RID: 11477
		// (get) Token: 0x060082B9 RID: 33465
		public virtual extern string IHTMLUniqueName_uniqueID
		{
			[TypeLibFunc(64)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17002CD6 RID: 11478
		// (get) Token: 0x060082BA RID: 33466
		public virtual extern int IHTMLDOMNode_nodeType
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17002CD7 RID: 11479
		// (get) Token: 0x060082BB RID: 33467
		public virtual extern IHTMLDOMNode IHTMLDOMNode_parentNode
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x060082BC RID: 33468
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLDOMNode_hasChildNodes();

		// Token: 0x17002CD8 RID: 11480
		// (get) Token: 0x060082BD RID: 33469
		public virtual extern object IHTMLDOMNode_childNodes
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17002CD9 RID: 11481
		// (get) Token: 0x060082BE RID: 33470
		public virtual extern object IHTMLDOMNode_attributes
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x060082BF RID: 33471
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_insertBefore([MarshalAs(28)] [In] IHTMLDOMNode newChild, [MarshalAs(27)] [In] [Optional] object refChild);

		// Token: 0x060082C0 RID: 33472
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_removeChild([MarshalAs(28)] [In] IHTMLDOMNode oldChild);

		// Token: 0x060082C1 RID: 33473
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_replaceChild([MarshalAs(28)] [In] IHTMLDOMNode newChild, [MarshalAs(28)] [In] IHTMLDOMNode oldChild);

		// Token: 0x060082C2 RID: 33474
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_cloneNode([In] bool fDeep);

		// Token: 0x060082C3 RID: 33475
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_removeNode([In] bool fDeep = false);

		// Token: 0x060082C4 RID: 33476
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_swapNode([MarshalAs(28)] [In] IHTMLDOMNode otherNode);

		// Token: 0x060082C5 RID: 33477
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_replaceNode([MarshalAs(28)] [In] IHTMLDOMNode replacement);

		// Token: 0x060082C6 RID: 33478
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_appendChild([MarshalAs(28)] [In] IHTMLDOMNode newChild);

		// Token: 0x17002CDA RID: 11482
		// (get) Token: 0x060082C7 RID: 33479
		public virtual extern string IHTMLDOMNode_nodeName
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17002CDB RID: 11483
		// (get) Token: 0x060082C9 RID: 33481
		// (set) Token: 0x060082C8 RID: 33480
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

		// Token: 0x17002CDC RID: 11484
		// (get) Token: 0x060082CA RID: 33482
		public virtual extern IHTMLDOMNode IHTMLDOMNode_firstChild
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17002CDD RID: 11485
		// (get) Token: 0x060082CB RID: 33483
		public virtual extern IHTMLDOMNode IHTMLDOMNode_lastChild
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17002CDE RID: 11486
		// (get) Token: 0x060082CC RID: 33484
		public virtual extern IHTMLDOMNode IHTMLDOMNode_previousSibling
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17002CDF RID: 11487
		// (get) Token: 0x060082CD RID: 33485
		public virtual extern IHTMLDOMNode IHTMLDOMNode_nextSibling
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17002CE0 RID: 11488
		// (get) Token: 0x060082CE RID: 33486
		public virtual extern object IHTMLDOMNode2_ownerDocument
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17002CE1 RID: 11489
		// (get) Token: 0x060082D0 RID: 33488
		// (set) Token: 0x060082CF RID: 33487
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

		// Token: 0x17002CE2 RID: 11490
		// (get) Token: 0x060082D2 RID: 33490
		// (set) Token: 0x060082D1 RID: 33489
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

		// Token: 0x17002CE3 RID: 11491
		// (get) Token: 0x060082D4 RID: 33492
		// (set) Token: 0x060082D3 RID: 33491
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

		// Token: 0x17002CE4 RID: 11492
		// (get) Token: 0x060082D6 RID: 33494
		// (set) Token: 0x060082D5 RID: 33493
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

		// Token: 0x060082D7 RID: 33495
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLControlElement_focus();

		// Token: 0x17002CE5 RID: 11493
		// (get) Token: 0x060082D9 RID: 33497
		// (set) Token: 0x060082D8 RID: 33496
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

		// Token: 0x17002CE6 RID: 11494
		// (get) Token: 0x060082DB RID: 33499
		// (set) Token: 0x060082DA RID: 33498
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

		// Token: 0x17002CE7 RID: 11495
		// (get) Token: 0x060082DD RID: 33501
		// (set) Token: 0x060082DC RID: 33500
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

		// Token: 0x17002CE8 RID: 11496
		// (get) Token: 0x060082DF RID: 33503
		// (set) Token: 0x060082DE RID: 33502
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

		// Token: 0x060082E0 RID: 33504
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLControlElement_blur();

		// Token: 0x060082E1 RID: 33505
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLControlElement_addFilter([MarshalAs(25)] [In] object pUnk);

		// Token: 0x060082E2 RID: 33506
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLControlElement_removeFilter([MarshalAs(25)] [In] object pUnk);

		// Token: 0x17002CE9 RID: 11497
		// (get) Token: 0x060082E3 RID: 33507
		public virtual extern int IHTMLControlElement_clientHeight
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17002CEA RID: 11498
		// (get) Token: 0x060082E4 RID: 33508
		public virtual extern int IHTMLControlElement_clientWidth
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17002CEB RID: 11499
		// (get) Token: 0x060082E5 RID: 33509
		public virtual extern int IHTMLControlElement_clientTop
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17002CEC RID: 11500
		// (get) Token: 0x060082E6 RID: 33510
		public virtual extern int IHTMLControlElement_clientLeft
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17002CED RID: 11501
		// (get) Token: 0x060082E8 RID: 33512
		// (set) Token: 0x060082E7 RID: 33511
		public virtual extern int IHTMLSelectElement_size
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x17002CEE RID: 11502
		// (get) Token: 0x060082EA RID: 33514
		// (set) Token: 0x060082E9 RID: 33513
		public virtual extern bool IHTMLSelectElement_multiple
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x17002CEF RID: 11503
		// (get) Token: 0x060082EC RID: 33516
		// (set) Token: 0x060082EB RID: 33515
		public virtual extern string IHTMLSelectElement_name
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

		// Token: 0x17002CF0 RID: 11504
		// (get) Token: 0x060082ED RID: 33517
		public virtual extern object IHTMLSelectElement_options
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17002CF1 RID: 11505
		// (get) Token: 0x060082EF RID: 33519
		// (set) Token: 0x060082EE RID: 33518
		public virtual extern object IHTMLSelectElement_onchange
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17002CF2 RID: 11506
		// (get) Token: 0x060082F1 RID: 33521
		// (set) Token: 0x060082F0 RID: 33520
		public virtual extern int IHTMLSelectElement_selectedIndex
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x17002CF3 RID: 11507
		// (get) Token: 0x060082F2 RID: 33522
		public virtual extern string IHTMLSelectElement_type
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17002CF4 RID: 11508
		// (get) Token: 0x060082F4 RID: 33524
		// (set) Token: 0x060082F3 RID: 33523
		public virtual extern string IHTMLSelectElement_value
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

		// Token: 0x17002CF5 RID: 11509
		// (get) Token: 0x060082F6 RID: 33526
		// (set) Token: 0x060082F5 RID: 33525
		public virtual extern bool IHTMLSelectElement_disabled
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x17002CF6 RID: 11510
		// (get) Token: 0x060082F7 RID: 33527
		public virtual extern IHTMLFormElement IHTMLSelectElement_form
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x060082F8 RID: 33528
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLSelectElement_add([MarshalAs(28)] [In] IHTMLElement element, [MarshalAs(27)] [In] [Optional] object before);

		// Token: 0x060082F9 RID: 33529
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLSelectElement_remove([In] int index = -1);

		// Token: 0x17002CF7 RID: 11511
		// (get) Token: 0x060082FB RID: 33531
		// (set) Token: 0x060082FA RID: 33530
		public virtual extern int IHTMLSelectElement_length
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x060082FC RID: 33532
		[TypeLibFunc(65)]
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(44, MarshalTypeRef = System.Runtime.InteropServices.CustomMarshalers.EnumeratorToEnumVariantMarshaler)]
		public virtual extern IEnumerator IHTMLSelectElement_GetEnumerator();

		// Token: 0x060082FD RID: 33533
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(26)]
		public virtual extern object IHTMLSelectElement_item([MarshalAs(27)] [In] [Optional] object name, [MarshalAs(27)] [In] [Optional] object index);

		// Token: 0x060082FE RID: 33534
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(26)]
		public virtual extern object IHTMLSelectElement_tags([MarshalAs(27)] [In] object tagName);

		// Token: 0x060082FF RID: 33535
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(26)]
		public virtual extern object IHTMLSelectElement2_urns([MarshalAs(27)] [In] object urn);

		// Token: 0x06008300 RID: 33536
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(26)]
		public virtual extern object IHTMLSelectElement4_namedItem([MarshalAs(19)] [In] string name);

		// Token: 0x14000ECF RID: 3791
		// (add) Token: 0x06008301 RID: 33537
		// (remove) Token: 0x06008302 RID: 33538
		public virtual extern event HTMLSelectElementEvents_onhelpEventHandler HTMLSelectElementEvents_Event_onhelp;

		// Token: 0x14000ED0 RID: 3792
		// (add) Token: 0x06008303 RID: 33539
		// (remove) Token: 0x06008304 RID: 33540
		public virtual extern event HTMLSelectElementEvents_onclickEventHandler HTMLSelectElementEvents_Event_onclick;

		// Token: 0x14000ED1 RID: 3793
		// (add) Token: 0x06008305 RID: 33541
		// (remove) Token: 0x06008306 RID: 33542
		public virtual extern event HTMLSelectElementEvents_ondblclickEventHandler HTMLSelectElementEvents_Event_ondblclick;

		// Token: 0x14000ED2 RID: 3794
		// (add) Token: 0x06008307 RID: 33543
		// (remove) Token: 0x06008308 RID: 33544
		public virtual extern event HTMLSelectElementEvents_onkeypressEventHandler HTMLSelectElementEvents_Event_onkeypress;

		// Token: 0x14000ED3 RID: 3795
		// (add) Token: 0x06008309 RID: 33545
		// (remove) Token: 0x0600830A RID: 33546
		public virtual extern event HTMLSelectElementEvents_onkeydownEventHandler HTMLSelectElementEvents_Event_onkeydown;

		// Token: 0x14000ED4 RID: 3796
		// (add) Token: 0x0600830B RID: 33547
		// (remove) Token: 0x0600830C RID: 33548
		public virtual extern event HTMLSelectElementEvents_onkeyupEventHandler HTMLSelectElementEvents_Event_onkeyup;

		// Token: 0x14000ED5 RID: 3797
		// (add) Token: 0x0600830D RID: 33549
		// (remove) Token: 0x0600830E RID: 33550
		public virtual extern event HTMLSelectElementEvents_onmouseoutEventHandler HTMLSelectElementEvents_Event_onmouseout;

		// Token: 0x14000ED6 RID: 3798
		// (add) Token: 0x0600830F RID: 33551
		// (remove) Token: 0x06008310 RID: 33552
		public virtual extern event HTMLSelectElementEvents_onmouseoverEventHandler HTMLSelectElementEvents_Event_onmouseover;

		// Token: 0x14000ED7 RID: 3799
		// (add) Token: 0x06008311 RID: 33553
		// (remove) Token: 0x06008312 RID: 33554
		public virtual extern event HTMLSelectElementEvents_onmousemoveEventHandler HTMLSelectElementEvents_Event_onmousemove;

		// Token: 0x14000ED8 RID: 3800
		// (add) Token: 0x06008313 RID: 33555
		// (remove) Token: 0x06008314 RID: 33556
		public virtual extern event HTMLSelectElementEvents_onmousedownEventHandler HTMLSelectElementEvents_Event_onmousedown;

		// Token: 0x14000ED9 RID: 3801
		// (add) Token: 0x06008315 RID: 33557
		// (remove) Token: 0x06008316 RID: 33558
		public virtual extern event HTMLSelectElementEvents_onmouseupEventHandler HTMLSelectElementEvents_Event_onmouseup;

		// Token: 0x14000EDA RID: 3802
		// (add) Token: 0x06008317 RID: 33559
		// (remove) Token: 0x06008318 RID: 33560
		public virtual extern event HTMLSelectElementEvents_onselectstartEventHandler HTMLSelectElementEvents_Event_onselectstart;

		// Token: 0x14000EDB RID: 3803
		// (add) Token: 0x06008319 RID: 33561
		// (remove) Token: 0x0600831A RID: 33562
		public virtual extern event HTMLSelectElementEvents_onfilterchangeEventHandler HTMLSelectElementEvents_Event_onfilterchange;

		// Token: 0x14000EDC RID: 3804
		// (add) Token: 0x0600831B RID: 33563
		// (remove) Token: 0x0600831C RID: 33564
		public virtual extern event HTMLSelectElementEvents_ondragstartEventHandler HTMLSelectElementEvents_Event_ondragstart;

		// Token: 0x14000EDD RID: 3805
		// (add) Token: 0x0600831D RID: 33565
		// (remove) Token: 0x0600831E RID: 33566
		public virtual extern event HTMLSelectElementEvents_onbeforeupdateEventHandler HTMLSelectElementEvents_Event_onbeforeupdate;

		// Token: 0x14000EDE RID: 3806
		// (add) Token: 0x0600831F RID: 33567
		// (remove) Token: 0x06008320 RID: 33568
		public virtual extern event HTMLSelectElementEvents_onafterupdateEventHandler HTMLSelectElementEvents_Event_onafterupdate;

		// Token: 0x14000EDF RID: 3807
		// (add) Token: 0x06008321 RID: 33569
		// (remove) Token: 0x06008322 RID: 33570
		public virtual extern event HTMLSelectElementEvents_onerrorupdateEventHandler HTMLSelectElementEvents_Event_onerrorupdate;

		// Token: 0x14000EE0 RID: 3808
		// (add) Token: 0x06008323 RID: 33571
		// (remove) Token: 0x06008324 RID: 33572
		public virtual extern event HTMLSelectElementEvents_onrowexitEventHandler HTMLSelectElementEvents_Event_onrowexit;

		// Token: 0x14000EE1 RID: 3809
		// (add) Token: 0x06008325 RID: 33573
		// (remove) Token: 0x06008326 RID: 33574
		public virtual extern event HTMLSelectElementEvents_onrowenterEventHandler HTMLSelectElementEvents_Event_onrowenter;

		// Token: 0x14000EE2 RID: 3810
		// (add) Token: 0x06008327 RID: 33575
		// (remove) Token: 0x06008328 RID: 33576
		public virtual extern event HTMLSelectElementEvents_ondatasetchangedEventHandler HTMLSelectElementEvents_Event_ondatasetchanged;

		// Token: 0x14000EE3 RID: 3811
		// (add) Token: 0x06008329 RID: 33577
		// (remove) Token: 0x0600832A RID: 33578
		public virtual extern event HTMLSelectElementEvents_ondataavailableEventHandler HTMLSelectElementEvents_Event_ondataavailable;

		// Token: 0x14000EE4 RID: 3812
		// (add) Token: 0x0600832B RID: 33579
		// (remove) Token: 0x0600832C RID: 33580
		public virtual extern event HTMLSelectElementEvents_ondatasetcompleteEventHandler HTMLSelectElementEvents_Event_ondatasetcomplete;

		// Token: 0x14000EE5 RID: 3813
		// (add) Token: 0x0600832D RID: 33581
		// (remove) Token: 0x0600832E RID: 33582
		public virtual extern event HTMLSelectElementEvents_onlosecaptureEventHandler HTMLSelectElementEvents_Event_onlosecapture;

		// Token: 0x14000EE6 RID: 3814
		// (add) Token: 0x0600832F RID: 33583
		// (remove) Token: 0x06008330 RID: 33584
		public virtual extern event HTMLSelectElementEvents_onpropertychangeEventHandler HTMLSelectElementEvents_Event_onpropertychange;

		// Token: 0x14000EE7 RID: 3815
		// (add) Token: 0x06008331 RID: 33585
		// (remove) Token: 0x06008332 RID: 33586
		public virtual extern event HTMLSelectElementEvents_onscrollEventHandler HTMLSelectElementEvents_Event_onscroll;

		// Token: 0x14000EE8 RID: 3816
		// (add) Token: 0x06008333 RID: 33587
		// (remove) Token: 0x06008334 RID: 33588
		public virtual extern event HTMLSelectElementEvents_onfocusEventHandler HTMLSelectElementEvents_Event_onfocus;

		// Token: 0x14000EE9 RID: 3817
		// (add) Token: 0x06008335 RID: 33589
		// (remove) Token: 0x06008336 RID: 33590
		public virtual extern event HTMLSelectElementEvents_onblurEventHandler HTMLSelectElementEvents_Event_onblur;

		// Token: 0x14000EEA RID: 3818
		// (add) Token: 0x06008337 RID: 33591
		// (remove) Token: 0x06008338 RID: 33592
		public virtual extern event HTMLSelectElementEvents_onresizeEventHandler HTMLSelectElementEvents_Event_onresize;

		// Token: 0x14000EEB RID: 3819
		// (add) Token: 0x06008339 RID: 33593
		// (remove) Token: 0x0600833A RID: 33594
		public virtual extern event HTMLSelectElementEvents_ondragEventHandler HTMLSelectElementEvents_Event_ondrag;

		// Token: 0x14000EEC RID: 3820
		// (add) Token: 0x0600833B RID: 33595
		// (remove) Token: 0x0600833C RID: 33596
		public virtual extern event HTMLSelectElementEvents_ondragendEventHandler HTMLSelectElementEvents_Event_ondragend;

		// Token: 0x14000EED RID: 3821
		// (add) Token: 0x0600833D RID: 33597
		// (remove) Token: 0x0600833E RID: 33598
		public virtual extern event HTMLSelectElementEvents_ondragenterEventHandler HTMLSelectElementEvents_Event_ondragenter;

		// Token: 0x14000EEE RID: 3822
		// (add) Token: 0x0600833F RID: 33599
		// (remove) Token: 0x06008340 RID: 33600
		public virtual extern event HTMLSelectElementEvents_ondragoverEventHandler HTMLSelectElementEvents_Event_ondragover;

		// Token: 0x14000EEF RID: 3823
		// (add) Token: 0x06008341 RID: 33601
		// (remove) Token: 0x06008342 RID: 33602
		public virtual extern event HTMLSelectElementEvents_ondragleaveEventHandler HTMLSelectElementEvents_Event_ondragleave;

		// Token: 0x14000EF0 RID: 3824
		// (add) Token: 0x06008343 RID: 33603
		// (remove) Token: 0x06008344 RID: 33604
		public virtual extern event HTMLSelectElementEvents_ondropEventHandler HTMLSelectElementEvents_Event_ondrop;

		// Token: 0x14000EF1 RID: 3825
		// (add) Token: 0x06008345 RID: 33605
		// (remove) Token: 0x06008346 RID: 33606
		public virtual extern event HTMLSelectElementEvents_onbeforecutEventHandler HTMLSelectElementEvents_Event_onbeforecut;

		// Token: 0x14000EF2 RID: 3826
		// (add) Token: 0x06008347 RID: 33607
		// (remove) Token: 0x06008348 RID: 33608
		public virtual extern event HTMLSelectElementEvents_oncutEventHandler HTMLSelectElementEvents_Event_oncut;

		// Token: 0x14000EF3 RID: 3827
		// (add) Token: 0x06008349 RID: 33609
		// (remove) Token: 0x0600834A RID: 33610
		public virtual extern event HTMLSelectElementEvents_onbeforecopyEventHandler HTMLSelectElementEvents_Event_onbeforecopy;

		// Token: 0x14000EF4 RID: 3828
		// (add) Token: 0x0600834B RID: 33611
		// (remove) Token: 0x0600834C RID: 33612
		public virtual extern event HTMLSelectElementEvents_oncopyEventHandler HTMLSelectElementEvents_Event_oncopy;

		// Token: 0x14000EF5 RID: 3829
		// (add) Token: 0x0600834D RID: 33613
		// (remove) Token: 0x0600834E RID: 33614
		public virtual extern event HTMLSelectElementEvents_onbeforepasteEventHandler HTMLSelectElementEvents_Event_onbeforepaste;

		// Token: 0x14000EF6 RID: 3830
		// (add) Token: 0x0600834F RID: 33615
		// (remove) Token: 0x06008350 RID: 33616
		public virtual extern event HTMLSelectElementEvents_onpasteEventHandler HTMLSelectElementEvents_Event_onpaste;

		// Token: 0x14000EF7 RID: 3831
		// (add) Token: 0x06008351 RID: 33617
		// (remove) Token: 0x06008352 RID: 33618
		public virtual extern event HTMLSelectElementEvents_oncontextmenuEventHandler HTMLSelectElementEvents_Event_oncontextmenu;

		// Token: 0x14000EF8 RID: 3832
		// (add) Token: 0x06008353 RID: 33619
		// (remove) Token: 0x06008354 RID: 33620
		public virtual extern event HTMLSelectElementEvents_onrowsdeleteEventHandler HTMLSelectElementEvents_Event_onrowsdelete;

		// Token: 0x14000EF9 RID: 3833
		// (add) Token: 0x06008355 RID: 33621
		// (remove) Token: 0x06008356 RID: 33622
		public virtual extern event HTMLSelectElementEvents_onrowsinsertedEventHandler HTMLSelectElementEvents_Event_onrowsinserted;

		// Token: 0x14000EFA RID: 3834
		// (add) Token: 0x06008357 RID: 33623
		// (remove) Token: 0x06008358 RID: 33624
		public virtual extern event HTMLSelectElementEvents_oncellchangeEventHandler HTMLSelectElementEvents_Event_oncellchange;

		// Token: 0x14000EFB RID: 3835
		// (add) Token: 0x06008359 RID: 33625
		// (remove) Token: 0x0600835A RID: 33626
		public virtual extern event HTMLSelectElementEvents_onreadystatechangeEventHandler HTMLSelectElementEvents_Event_onreadystatechange;

		// Token: 0x14000EFC RID: 3836
		// (add) Token: 0x0600835B RID: 33627
		// (remove) Token: 0x0600835C RID: 33628
		public virtual extern event HTMLSelectElementEvents_onbeforeeditfocusEventHandler HTMLSelectElementEvents_Event_onbeforeeditfocus;

		// Token: 0x14000EFD RID: 3837
		// (add) Token: 0x0600835D RID: 33629
		// (remove) Token: 0x0600835E RID: 33630
		public virtual extern event HTMLSelectElementEvents_onlayoutcompleteEventHandler HTMLSelectElementEvents_Event_onlayoutcomplete;

		// Token: 0x14000EFE RID: 3838
		// (add) Token: 0x0600835F RID: 33631
		// (remove) Token: 0x06008360 RID: 33632
		public virtual extern event HTMLSelectElementEvents_onpageEventHandler HTMLSelectElementEvents_Event_onpage;

		// Token: 0x14000EFF RID: 3839
		// (add) Token: 0x06008361 RID: 33633
		// (remove) Token: 0x06008362 RID: 33634
		public virtual extern event HTMLSelectElementEvents_onbeforedeactivateEventHandler HTMLSelectElementEvents_Event_onbeforedeactivate;

		// Token: 0x14000F00 RID: 3840
		// (add) Token: 0x06008363 RID: 33635
		// (remove) Token: 0x06008364 RID: 33636
		public virtual extern event HTMLSelectElementEvents_onbeforeactivateEventHandler HTMLSelectElementEvents_Event_onbeforeactivate;

		// Token: 0x14000F01 RID: 3841
		// (add) Token: 0x06008365 RID: 33637
		// (remove) Token: 0x06008366 RID: 33638
		public virtual extern event HTMLSelectElementEvents_onmoveEventHandler HTMLSelectElementEvents_Event_onmove;

		// Token: 0x14000F02 RID: 3842
		// (add) Token: 0x06008367 RID: 33639
		// (remove) Token: 0x06008368 RID: 33640
		public virtual extern event HTMLSelectElementEvents_oncontrolselectEventHandler HTMLSelectElementEvents_Event_oncontrolselect;

		// Token: 0x14000F03 RID: 3843
		// (add) Token: 0x06008369 RID: 33641
		// (remove) Token: 0x0600836A RID: 33642
		public virtual extern event HTMLSelectElementEvents_onmovestartEventHandler HTMLSelectElementEvents_Event_onmovestart;

		// Token: 0x14000F04 RID: 3844
		// (add) Token: 0x0600836B RID: 33643
		// (remove) Token: 0x0600836C RID: 33644
		public virtual extern event HTMLSelectElementEvents_onmoveendEventHandler HTMLSelectElementEvents_Event_onmoveend;

		// Token: 0x14000F05 RID: 3845
		// (add) Token: 0x0600836D RID: 33645
		// (remove) Token: 0x0600836E RID: 33646
		public virtual extern event HTMLSelectElementEvents_onresizestartEventHandler HTMLSelectElementEvents_Event_onresizestart;

		// Token: 0x14000F06 RID: 3846
		// (add) Token: 0x0600836F RID: 33647
		// (remove) Token: 0x06008370 RID: 33648
		public virtual extern event HTMLSelectElementEvents_onresizeendEventHandler HTMLSelectElementEvents_Event_onresizeend;

		// Token: 0x14000F07 RID: 3847
		// (add) Token: 0x06008371 RID: 33649
		// (remove) Token: 0x06008372 RID: 33650
		public virtual extern event HTMLSelectElementEvents_onmouseenterEventHandler HTMLSelectElementEvents_Event_onmouseenter;

		// Token: 0x14000F08 RID: 3848
		// (add) Token: 0x06008373 RID: 33651
		// (remove) Token: 0x06008374 RID: 33652
		public virtual extern event HTMLSelectElementEvents_onmouseleaveEventHandler HTMLSelectElementEvents_Event_onmouseleave;

		// Token: 0x14000F09 RID: 3849
		// (add) Token: 0x06008375 RID: 33653
		// (remove) Token: 0x06008376 RID: 33654
		public virtual extern event HTMLSelectElementEvents_onmousewheelEventHandler HTMLSelectElementEvents_Event_onmousewheel;

		// Token: 0x14000F0A RID: 3850
		// (add) Token: 0x06008377 RID: 33655
		// (remove) Token: 0x06008378 RID: 33656
		public virtual extern event HTMLSelectElementEvents_onactivateEventHandler HTMLSelectElementEvents_Event_onactivate;

		// Token: 0x14000F0B RID: 3851
		// (add) Token: 0x06008379 RID: 33657
		// (remove) Token: 0x0600837A RID: 33658
		public virtual extern event HTMLSelectElementEvents_ondeactivateEventHandler HTMLSelectElementEvents_Event_ondeactivate;

		// Token: 0x14000F0C RID: 3852
		// (add) Token: 0x0600837B RID: 33659
		// (remove) Token: 0x0600837C RID: 33660
		public virtual extern event HTMLSelectElementEvents_onfocusinEventHandler HTMLSelectElementEvents_Event_onfocusin;

		// Token: 0x14000F0D RID: 3853
		// (add) Token: 0x0600837D RID: 33661
		// (remove) Token: 0x0600837E RID: 33662
		public virtual extern event HTMLSelectElementEvents_onfocusoutEventHandler HTMLSelectElementEvents_Event_onfocusout;

		// Token: 0x14000F0E RID: 3854
		// (add) Token: 0x0600837F RID: 33663
		// (remove) Token: 0x06008380 RID: 33664
		public virtual extern event HTMLSelectElementEvents_onchangeEventHandler HTMLSelectElementEvents_Event_onchange;

		// Token: 0x14000F0F RID: 3855
		// (add) Token: 0x06008381 RID: 33665
		// (remove) Token: 0x06008382 RID: 33666
		public virtual extern event HTMLSelectElementEvents2_onhelpEventHandler HTMLSelectElementEvents2_Event_onhelp;

		// Token: 0x14000F10 RID: 3856
		// (add) Token: 0x06008383 RID: 33667
		// (remove) Token: 0x06008384 RID: 33668
		public virtual extern event HTMLSelectElementEvents2_onclickEventHandler HTMLSelectElementEvents2_Event_onclick;

		// Token: 0x14000F11 RID: 3857
		// (add) Token: 0x06008385 RID: 33669
		// (remove) Token: 0x06008386 RID: 33670
		public virtual extern event HTMLSelectElementEvents2_ondblclickEventHandler HTMLSelectElementEvents2_Event_ondblclick;

		// Token: 0x14000F12 RID: 3858
		// (add) Token: 0x06008387 RID: 33671
		// (remove) Token: 0x06008388 RID: 33672
		public virtual extern event HTMLSelectElementEvents2_onkeypressEventHandler HTMLSelectElementEvents2_Event_onkeypress;

		// Token: 0x14000F13 RID: 3859
		// (add) Token: 0x06008389 RID: 33673
		// (remove) Token: 0x0600838A RID: 33674
		public virtual extern event HTMLSelectElementEvents2_onkeydownEventHandler HTMLSelectElementEvents2_Event_onkeydown;

		// Token: 0x14000F14 RID: 3860
		// (add) Token: 0x0600838B RID: 33675
		// (remove) Token: 0x0600838C RID: 33676
		public virtual extern event HTMLSelectElementEvents2_onkeyupEventHandler HTMLSelectElementEvents2_Event_onkeyup;

		// Token: 0x14000F15 RID: 3861
		// (add) Token: 0x0600838D RID: 33677
		// (remove) Token: 0x0600838E RID: 33678
		public virtual extern event HTMLSelectElementEvents2_onmouseoutEventHandler HTMLSelectElementEvents2_Event_onmouseout;

		// Token: 0x14000F16 RID: 3862
		// (add) Token: 0x0600838F RID: 33679
		// (remove) Token: 0x06008390 RID: 33680
		public virtual extern event HTMLSelectElementEvents2_onmouseoverEventHandler HTMLSelectElementEvents2_Event_onmouseover;

		// Token: 0x14000F17 RID: 3863
		// (add) Token: 0x06008391 RID: 33681
		// (remove) Token: 0x06008392 RID: 33682
		public virtual extern event HTMLSelectElementEvents2_onmousemoveEventHandler HTMLSelectElementEvents2_Event_onmousemove;

		// Token: 0x14000F18 RID: 3864
		// (add) Token: 0x06008393 RID: 33683
		// (remove) Token: 0x06008394 RID: 33684
		public virtual extern event HTMLSelectElementEvents2_onmousedownEventHandler HTMLSelectElementEvents2_Event_onmousedown;

		// Token: 0x14000F19 RID: 3865
		// (add) Token: 0x06008395 RID: 33685
		// (remove) Token: 0x06008396 RID: 33686
		public virtual extern event HTMLSelectElementEvents2_onmouseupEventHandler HTMLSelectElementEvents2_Event_onmouseup;

		// Token: 0x14000F1A RID: 3866
		// (add) Token: 0x06008397 RID: 33687
		// (remove) Token: 0x06008398 RID: 33688
		public virtual extern event HTMLSelectElementEvents2_onselectstartEventHandler HTMLSelectElementEvents2_Event_onselectstart;

		// Token: 0x14000F1B RID: 3867
		// (add) Token: 0x06008399 RID: 33689
		// (remove) Token: 0x0600839A RID: 33690
		public virtual extern event HTMLSelectElementEvents2_onfilterchangeEventHandler HTMLSelectElementEvents2_Event_onfilterchange;

		// Token: 0x14000F1C RID: 3868
		// (add) Token: 0x0600839B RID: 33691
		// (remove) Token: 0x0600839C RID: 33692
		public virtual extern event HTMLSelectElementEvents2_ondragstartEventHandler HTMLSelectElementEvents2_Event_ondragstart;

		// Token: 0x14000F1D RID: 3869
		// (add) Token: 0x0600839D RID: 33693
		// (remove) Token: 0x0600839E RID: 33694
		public virtual extern event HTMLSelectElementEvents2_onbeforeupdateEventHandler HTMLSelectElementEvents2_Event_onbeforeupdate;

		// Token: 0x14000F1E RID: 3870
		// (add) Token: 0x0600839F RID: 33695
		// (remove) Token: 0x060083A0 RID: 33696
		public virtual extern event HTMLSelectElementEvents2_onafterupdateEventHandler HTMLSelectElementEvents2_Event_onafterupdate;

		// Token: 0x14000F1F RID: 3871
		// (add) Token: 0x060083A1 RID: 33697
		// (remove) Token: 0x060083A2 RID: 33698
		public virtual extern event HTMLSelectElementEvents2_onerrorupdateEventHandler HTMLSelectElementEvents2_Event_onerrorupdate;

		// Token: 0x14000F20 RID: 3872
		// (add) Token: 0x060083A3 RID: 33699
		// (remove) Token: 0x060083A4 RID: 33700
		public virtual extern event HTMLSelectElementEvents2_onrowexitEventHandler HTMLSelectElementEvents2_Event_onrowexit;

		// Token: 0x14000F21 RID: 3873
		// (add) Token: 0x060083A5 RID: 33701
		// (remove) Token: 0x060083A6 RID: 33702
		public virtual extern event HTMLSelectElementEvents2_onrowenterEventHandler HTMLSelectElementEvents2_Event_onrowenter;

		// Token: 0x14000F22 RID: 3874
		// (add) Token: 0x060083A7 RID: 33703
		// (remove) Token: 0x060083A8 RID: 33704
		public virtual extern event HTMLSelectElementEvents2_ondatasetchangedEventHandler HTMLSelectElementEvents2_Event_ondatasetchanged;

		// Token: 0x14000F23 RID: 3875
		// (add) Token: 0x060083A9 RID: 33705
		// (remove) Token: 0x060083AA RID: 33706
		public virtual extern event HTMLSelectElementEvents2_ondataavailableEventHandler HTMLSelectElementEvents2_Event_ondataavailable;

		// Token: 0x14000F24 RID: 3876
		// (add) Token: 0x060083AB RID: 33707
		// (remove) Token: 0x060083AC RID: 33708
		public virtual extern event HTMLSelectElementEvents2_ondatasetcompleteEventHandler HTMLSelectElementEvents2_Event_ondatasetcomplete;

		// Token: 0x14000F25 RID: 3877
		// (add) Token: 0x060083AD RID: 33709
		// (remove) Token: 0x060083AE RID: 33710
		public virtual extern event HTMLSelectElementEvents2_onlosecaptureEventHandler HTMLSelectElementEvents2_Event_onlosecapture;

		// Token: 0x14000F26 RID: 3878
		// (add) Token: 0x060083AF RID: 33711
		// (remove) Token: 0x060083B0 RID: 33712
		public virtual extern event HTMLSelectElementEvents2_onpropertychangeEventHandler HTMLSelectElementEvents2_Event_onpropertychange;

		// Token: 0x14000F27 RID: 3879
		// (add) Token: 0x060083B1 RID: 33713
		// (remove) Token: 0x060083B2 RID: 33714
		public virtual extern event HTMLSelectElementEvents2_onscrollEventHandler HTMLSelectElementEvents2_Event_onscroll;

		// Token: 0x14000F28 RID: 3880
		// (add) Token: 0x060083B3 RID: 33715
		// (remove) Token: 0x060083B4 RID: 33716
		public virtual extern event HTMLSelectElementEvents2_onfocusEventHandler HTMLSelectElementEvents2_Event_onfocus;

		// Token: 0x14000F29 RID: 3881
		// (add) Token: 0x060083B5 RID: 33717
		// (remove) Token: 0x060083B6 RID: 33718
		public virtual extern event HTMLSelectElementEvents2_onblurEventHandler HTMLSelectElementEvents2_Event_onblur;

		// Token: 0x14000F2A RID: 3882
		// (add) Token: 0x060083B7 RID: 33719
		// (remove) Token: 0x060083B8 RID: 33720
		public virtual extern event HTMLSelectElementEvents2_onresizeEventHandler HTMLSelectElementEvents2_Event_onresize;

		// Token: 0x14000F2B RID: 3883
		// (add) Token: 0x060083B9 RID: 33721
		// (remove) Token: 0x060083BA RID: 33722
		public virtual extern event HTMLSelectElementEvents2_ondragEventHandler HTMLSelectElementEvents2_Event_ondrag;

		// Token: 0x14000F2C RID: 3884
		// (add) Token: 0x060083BB RID: 33723
		// (remove) Token: 0x060083BC RID: 33724
		public virtual extern event HTMLSelectElementEvents2_ondragendEventHandler HTMLSelectElementEvents2_Event_ondragend;

		// Token: 0x14000F2D RID: 3885
		// (add) Token: 0x060083BD RID: 33725
		// (remove) Token: 0x060083BE RID: 33726
		public virtual extern event HTMLSelectElementEvents2_ondragenterEventHandler HTMLSelectElementEvents2_Event_ondragenter;

		// Token: 0x14000F2E RID: 3886
		// (add) Token: 0x060083BF RID: 33727
		// (remove) Token: 0x060083C0 RID: 33728
		public virtual extern event HTMLSelectElementEvents2_ondragoverEventHandler HTMLSelectElementEvents2_Event_ondragover;

		// Token: 0x14000F2F RID: 3887
		// (add) Token: 0x060083C1 RID: 33729
		// (remove) Token: 0x060083C2 RID: 33730
		public virtual extern event HTMLSelectElementEvents2_ondragleaveEventHandler HTMLSelectElementEvents2_Event_ondragleave;

		// Token: 0x14000F30 RID: 3888
		// (add) Token: 0x060083C3 RID: 33731
		// (remove) Token: 0x060083C4 RID: 33732
		public virtual extern event HTMLSelectElementEvents2_ondropEventHandler HTMLSelectElementEvents2_Event_ondrop;

		// Token: 0x14000F31 RID: 3889
		// (add) Token: 0x060083C5 RID: 33733
		// (remove) Token: 0x060083C6 RID: 33734
		public virtual extern event HTMLSelectElementEvents2_onbeforecutEventHandler HTMLSelectElementEvents2_Event_onbeforecut;

		// Token: 0x14000F32 RID: 3890
		// (add) Token: 0x060083C7 RID: 33735
		// (remove) Token: 0x060083C8 RID: 33736
		public virtual extern event HTMLSelectElementEvents2_oncutEventHandler HTMLSelectElementEvents2_Event_oncut;

		// Token: 0x14000F33 RID: 3891
		// (add) Token: 0x060083C9 RID: 33737
		// (remove) Token: 0x060083CA RID: 33738
		public virtual extern event HTMLSelectElementEvents2_onbeforecopyEventHandler HTMLSelectElementEvents2_Event_onbeforecopy;

		// Token: 0x14000F34 RID: 3892
		// (add) Token: 0x060083CB RID: 33739
		// (remove) Token: 0x060083CC RID: 33740
		public virtual extern event HTMLSelectElementEvents2_oncopyEventHandler HTMLSelectElementEvents2_Event_oncopy;

		// Token: 0x14000F35 RID: 3893
		// (add) Token: 0x060083CD RID: 33741
		// (remove) Token: 0x060083CE RID: 33742
		public virtual extern event HTMLSelectElementEvents2_onbeforepasteEventHandler HTMLSelectElementEvents2_Event_onbeforepaste;

		// Token: 0x14000F36 RID: 3894
		// (add) Token: 0x060083CF RID: 33743
		// (remove) Token: 0x060083D0 RID: 33744
		public virtual extern event HTMLSelectElementEvents2_onpasteEventHandler HTMLSelectElementEvents2_Event_onpaste;

		// Token: 0x14000F37 RID: 3895
		// (add) Token: 0x060083D1 RID: 33745
		// (remove) Token: 0x060083D2 RID: 33746
		public virtual extern event HTMLSelectElementEvents2_oncontextmenuEventHandler HTMLSelectElementEvents2_Event_oncontextmenu;

		// Token: 0x14000F38 RID: 3896
		// (add) Token: 0x060083D3 RID: 33747
		// (remove) Token: 0x060083D4 RID: 33748
		public virtual extern event HTMLSelectElementEvents2_onrowsdeleteEventHandler HTMLSelectElementEvents2_Event_onrowsdelete;

		// Token: 0x14000F39 RID: 3897
		// (add) Token: 0x060083D5 RID: 33749
		// (remove) Token: 0x060083D6 RID: 33750
		public virtual extern event HTMLSelectElementEvents2_onrowsinsertedEventHandler HTMLSelectElementEvents2_Event_onrowsinserted;

		// Token: 0x14000F3A RID: 3898
		// (add) Token: 0x060083D7 RID: 33751
		// (remove) Token: 0x060083D8 RID: 33752
		public virtual extern event HTMLSelectElementEvents2_oncellchangeEventHandler HTMLSelectElementEvents2_Event_oncellchange;

		// Token: 0x14000F3B RID: 3899
		// (add) Token: 0x060083D9 RID: 33753
		// (remove) Token: 0x060083DA RID: 33754
		public virtual extern event HTMLSelectElementEvents2_onreadystatechangeEventHandler HTMLSelectElementEvents2_Event_onreadystatechange;

		// Token: 0x14000F3C RID: 3900
		// (add) Token: 0x060083DB RID: 33755
		// (remove) Token: 0x060083DC RID: 33756
		public virtual extern event HTMLSelectElementEvents2_onlayoutcompleteEventHandler HTMLSelectElementEvents2_Event_onlayoutcomplete;

		// Token: 0x14000F3D RID: 3901
		// (add) Token: 0x060083DD RID: 33757
		// (remove) Token: 0x060083DE RID: 33758
		public virtual extern event HTMLSelectElementEvents2_onpageEventHandler HTMLSelectElementEvents2_Event_onpage;

		// Token: 0x14000F3E RID: 3902
		// (add) Token: 0x060083DF RID: 33759
		// (remove) Token: 0x060083E0 RID: 33760
		public virtual extern event HTMLSelectElementEvents2_onmouseenterEventHandler HTMLSelectElementEvents2_Event_onmouseenter;

		// Token: 0x14000F3F RID: 3903
		// (add) Token: 0x060083E1 RID: 33761
		// (remove) Token: 0x060083E2 RID: 33762
		public virtual extern event HTMLSelectElementEvents2_onmouseleaveEventHandler HTMLSelectElementEvents2_Event_onmouseleave;

		// Token: 0x14000F40 RID: 3904
		// (add) Token: 0x060083E3 RID: 33763
		// (remove) Token: 0x060083E4 RID: 33764
		public virtual extern event HTMLSelectElementEvents2_onactivateEventHandler HTMLSelectElementEvents2_Event_onactivate;

		// Token: 0x14000F41 RID: 3905
		// (add) Token: 0x060083E5 RID: 33765
		// (remove) Token: 0x060083E6 RID: 33766
		public virtual extern event HTMLSelectElementEvents2_ondeactivateEventHandler HTMLSelectElementEvents2_Event_ondeactivate;

		// Token: 0x14000F42 RID: 3906
		// (add) Token: 0x060083E7 RID: 33767
		// (remove) Token: 0x060083E8 RID: 33768
		public virtual extern event HTMLSelectElementEvents2_onbeforedeactivateEventHandler HTMLSelectElementEvents2_Event_onbeforedeactivate;

		// Token: 0x14000F43 RID: 3907
		// (add) Token: 0x060083E9 RID: 33769
		// (remove) Token: 0x060083EA RID: 33770
		public virtual extern event HTMLSelectElementEvents2_onbeforeactivateEventHandler HTMLSelectElementEvents2_Event_onbeforeactivate;

		// Token: 0x14000F44 RID: 3908
		// (add) Token: 0x060083EB RID: 33771
		// (remove) Token: 0x060083EC RID: 33772
		public virtual extern event HTMLSelectElementEvents2_onfocusinEventHandler HTMLSelectElementEvents2_Event_onfocusin;

		// Token: 0x14000F45 RID: 3909
		// (add) Token: 0x060083ED RID: 33773
		// (remove) Token: 0x060083EE RID: 33774
		public virtual extern event HTMLSelectElementEvents2_onfocusoutEventHandler HTMLSelectElementEvents2_Event_onfocusout;

		// Token: 0x14000F46 RID: 3910
		// (add) Token: 0x060083EF RID: 33775
		// (remove) Token: 0x060083F0 RID: 33776
		public virtual extern event HTMLSelectElementEvents2_onmoveEventHandler HTMLSelectElementEvents2_Event_onmove;

		// Token: 0x14000F47 RID: 3911
		// (add) Token: 0x060083F1 RID: 33777
		// (remove) Token: 0x060083F2 RID: 33778
		public virtual extern event HTMLSelectElementEvents2_oncontrolselectEventHandler HTMLSelectElementEvents2_Event_oncontrolselect;

		// Token: 0x14000F48 RID: 3912
		// (add) Token: 0x060083F3 RID: 33779
		// (remove) Token: 0x060083F4 RID: 33780
		public virtual extern event HTMLSelectElementEvents2_onmovestartEventHandler HTMLSelectElementEvents2_Event_onmovestart;

		// Token: 0x14000F49 RID: 3913
		// (add) Token: 0x060083F5 RID: 33781
		// (remove) Token: 0x060083F6 RID: 33782
		public virtual extern event HTMLSelectElementEvents2_onmoveendEventHandler HTMLSelectElementEvents2_Event_onmoveend;

		// Token: 0x14000F4A RID: 3914
		// (add) Token: 0x060083F7 RID: 33783
		// (remove) Token: 0x060083F8 RID: 33784
		public virtual extern event HTMLSelectElementEvents2_onresizestartEventHandler HTMLSelectElementEvents2_Event_onresizestart;

		// Token: 0x14000F4B RID: 3915
		// (add) Token: 0x060083F9 RID: 33785
		// (remove) Token: 0x060083FA RID: 33786
		public virtual extern event HTMLSelectElementEvents2_onresizeendEventHandler HTMLSelectElementEvents2_Event_onresizeend;

		// Token: 0x14000F4C RID: 3916
		// (add) Token: 0x060083FB RID: 33787
		// (remove) Token: 0x060083FC RID: 33788
		public virtual extern event HTMLSelectElementEvents2_onmousewheelEventHandler HTMLSelectElementEvents2_Event_onmousewheel;

		// Token: 0x14000F4D RID: 3917
		// (add) Token: 0x060083FD RID: 33789
		// (remove) Token: 0x060083FE RID: 33790
		public virtual extern event HTMLSelectElementEvents2_onchangeEventHandler HTMLSelectElementEvents2_Event_onchange;
	}
}
