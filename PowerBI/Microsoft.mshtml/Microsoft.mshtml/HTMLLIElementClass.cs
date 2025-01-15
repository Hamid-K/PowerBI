using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x020004B0 RID: 1200
	[ComSourceInterfaces("mshtml.HTMLElementEvents\0mshtml.HTMLElementEvents2\0\0")]
	[Guid("3050F273-98B5-11CF-BB82-00AA00BDCE0B")]
	[TypeLibType(2)]
	[ClassInterface(0)]
	[ComImport]
	public class HTMLLIElementClass : DispHTMLLIElement, HTMLLIElement, HTMLElementEvents_Event, IHTMLElement, IHTMLElement2, IHTMLElement3, IHTMLElement4, IHTMLUniqueName, IHTMLDOMNode, IHTMLDOMNode2, IHTMLLIElement, HTMLElementEvents2_Event
	{
		// Token: 0x06005580 RID: 21888
		[MethodImpl(4096, MethodCodeType = 3)]
		public extern HTMLLIElementClass();

		// Token: 0x06005581 RID: 21889
		[DispId(-2147417611)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void setAttribute([MarshalAs(19)] [In] string strAttributeName, [MarshalAs(27)] [In] object AttributeValue, [In] int lFlags = 1);

		// Token: 0x06005582 RID: 21890
		[DispId(-2147417610)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		public virtual extern object getAttribute([MarshalAs(19)] [In] string strAttributeName, [In] int lFlags = 0);

		// Token: 0x06005583 RID: 21891
		[DispId(-2147417609)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool removeAttribute([MarshalAs(19)] [In] string strAttributeName, [In] int lFlags = 1);

		// Token: 0x17001C37 RID: 7223
		// (get) Token: 0x06005585 RID: 21893
		// (set) Token: 0x06005584 RID: 21892
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

		// Token: 0x17001C38 RID: 7224
		// (get) Token: 0x06005587 RID: 21895
		// (set) Token: 0x06005586 RID: 21894
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

		// Token: 0x17001C39 RID: 7225
		// (get) Token: 0x06005588 RID: 21896
		[DispId(-2147417108)]
		public virtual extern string tagName
		{
			[DispId(-2147417108)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17001C3A RID: 7226
		// (get) Token: 0x06005589 RID: 21897
		[DispId(-2147418104)]
		public virtual extern IHTMLElement parentElement
		{
			[DispId(-2147418104)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17001C3B RID: 7227
		// (get) Token: 0x0600558A RID: 21898
		[DispId(-2147418038)]
		public virtual extern IHTMLStyle style
		{
			[TypeLibFunc(1024)]
			[DispId(-2147418038)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17001C3C RID: 7228
		// (get) Token: 0x0600558C RID: 21900
		// (set) Token: 0x0600558B RID: 21899
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

		// Token: 0x17001C3D RID: 7229
		// (get) Token: 0x0600558E RID: 21902
		// (set) Token: 0x0600558D RID: 21901
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

		// Token: 0x17001C3E RID: 7230
		// (get) Token: 0x06005590 RID: 21904
		// (set) Token: 0x0600558F RID: 21903
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

		// Token: 0x17001C3F RID: 7231
		// (get) Token: 0x06005592 RID: 21906
		// (set) Token: 0x06005591 RID: 21905
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

		// Token: 0x17001C40 RID: 7232
		// (get) Token: 0x06005594 RID: 21908
		// (set) Token: 0x06005593 RID: 21907
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

		// Token: 0x17001C41 RID: 7233
		// (get) Token: 0x06005596 RID: 21910
		// (set) Token: 0x06005595 RID: 21909
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

		// Token: 0x17001C42 RID: 7234
		// (get) Token: 0x06005598 RID: 21912
		// (set) Token: 0x06005597 RID: 21911
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

		// Token: 0x17001C43 RID: 7235
		// (get) Token: 0x0600559A RID: 21914
		// (set) Token: 0x06005599 RID: 21913
		[DispId(-2147412112)]
		public virtual extern object onmouseover
		{
			[TypeLibFunc(20)]
			[DispId(-2147412112)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412112)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17001C44 RID: 7236
		// (get) Token: 0x0600559C RID: 21916
		// (set) Token: 0x0600559B RID: 21915
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

		// Token: 0x17001C45 RID: 7237
		// (get) Token: 0x0600559E RID: 21918
		// (set) Token: 0x0600559D RID: 21917
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

		// Token: 0x17001C46 RID: 7238
		// (get) Token: 0x060055A0 RID: 21920
		// (set) Token: 0x0600559F RID: 21919
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

		// Token: 0x17001C47 RID: 7239
		// (get) Token: 0x060055A1 RID: 21921
		[DispId(-2147417094)]
		public virtual extern object document
		{
			[DispId(-2147417094)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17001C48 RID: 7240
		// (get) Token: 0x060055A3 RID: 21923
		// (set) Token: 0x060055A2 RID: 21922
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

		// Token: 0x17001C49 RID: 7241
		// (get) Token: 0x060055A5 RID: 21925
		// (set) Token: 0x060055A4 RID: 21924
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

		// Token: 0x17001C4A RID: 7242
		// (get) Token: 0x060055A7 RID: 21927
		// (set) Token: 0x060055A6 RID: 21926
		[DispId(-2147412075)]
		public virtual extern object onselectstart
		{
			[DispId(-2147412075)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412075)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x060055A8 RID: 21928
		[DispId(-2147417093)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void scrollIntoView([MarshalAs(27)] [In] [Optional] object varargStart);

		// Token: 0x060055A9 RID: 21929
		[DispId(-2147417092)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool contains([MarshalAs(28)] [In] IHTMLElement pChild);

		// Token: 0x17001C4B RID: 7243
		// (get) Token: 0x060055AA RID: 21930
		[DispId(-2147417088)]
		public virtual extern int sourceIndex
		{
			[DispId(-2147417088)]
			[TypeLibFunc(4)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17001C4C RID: 7244
		// (get) Token: 0x060055AB RID: 21931
		[DispId(-2147417087)]
		public virtual extern object recordNumber
		{
			[DispId(-2147417087)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x17001C4D RID: 7245
		// (get) Token: 0x060055AD RID: 21933
		// (set) Token: 0x060055AC RID: 21932
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

		// Token: 0x17001C4E RID: 7246
		// (get) Token: 0x060055AE RID: 21934
		[DispId(-2147417104)]
		public virtual extern int offsetLeft
		{
			[DispId(-2147417104)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17001C4F RID: 7247
		// (get) Token: 0x060055AF RID: 21935
		[DispId(-2147417103)]
		public virtual extern int offsetTop
		{
			[DispId(-2147417103)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17001C50 RID: 7248
		// (get) Token: 0x060055B0 RID: 21936
		[DispId(-2147417102)]
		public virtual extern int offsetWidth
		{
			[DispId(-2147417102)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17001C51 RID: 7249
		// (get) Token: 0x060055B1 RID: 21937
		[DispId(-2147417101)]
		public virtual extern int offsetHeight
		{
			[DispId(-2147417101)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17001C52 RID: 7250
		// (get) Token: 0x060055B2 RID: 21938
		[DispId(-2147417100)]
		public virtual extern IHTMLElement offsetParent
		{
			[DispId(-2147417100)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17001C53 RID: 7251
		// (get) Token: 0x060055B4 RID: 21940
		// (set) Token: 0x060055B3 RID: 21939
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

		// Token: 0x17001C54 RID: 7252
		// (get) Token: 0x060055B6 RID: 21942
		// (set) Token: 0x060055B5 RID: 21941
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

		// Token: 0x17001C55 RID: 7253
		// (get) Token: 0x060055B8 RID: 21944
		// (set) Token: 0x060055B7 RID: 21943
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

		// Token: 0x17001C56 RID: 7254
		// (get) Token: 0x060055BA RID: 21946
		// (set) Token: 0x060055B9 RID: 21945
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

		// Token: 0x060055BB RID: 21947
		[DispId(-2147417082)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void insertAdjacentHTML([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string html);

		// Token: 0x060055BC RID: 21948
		[DispId(-2147417081)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void insertAdjacentText([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string text);

		// Token: 0x17001C57 RID: 7255
		// (get) Token: 0x060055BD RID: 21949
		[DispId(-2147417080)]
		public virtual extern IHTMLElement parentTextEdit
		{
			[DispId(-2147417080)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17001C58 RID: 7256
		// (get) Token: 0x060055BE RID: 21950
		[DispId(-2147417078)]
		public virtual extern bool isTextEdit
		{
			[DispId(-2147417078)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x060055BF RID: 21951
		[DispId(-2147417079)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void click();

		// Token: 0x17001C59 RID: 7257
		// (get) Token: 0x060055C0 RID: 21952
		[DispId(-2147417077)]
		public virtual extern IHTMLFiltersCollection filters
		{
			[DispId(-2147417077)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17001C5A RID: 7258
		// (get) Token: 0x060055C2 RID: 21954
		// (set) Token: 0x060055C1 RID: 21953
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

		// Token: 0x060055C3 RID: 21955
		[DispId(-2147417076)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string toString();

		// Token: 0x17001C5B RID: 7259
		// (get) Token: 0x060055C5 RID: 21957
		// (set) Token: 0x060055C4 RID: 21956
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

		// Token: 0x17001C5C RID: 7260
		// (get) Token: 0x060055C7 RID: 21959
		// (set) Token: 0x060055C6 RID: 21958
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

		// Token: 0x17001C5D RID: 7261
		// (get) Token: 0x060055C9 RID: 21961
		// (set) Token: 0x060055C8 RID: 21960
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

		// Token: 0x17001C5E RID: 7262
		// (get) Token: 0x060055CB RID: 21963
		// (set) Token: 0x060055CA RID: 21962
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

		// Token: 0x17001C5F RID: 7263
		// (get) Token: 0x060055CD RID: 21965
		// (set) Token: 0x060055CC RID: 21964
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

		// Token: 0x17001C60 RID: 7264
		// (get) Token: 0x060055CF RID: 21967
		// (set) Token: 0x060055CE RID: 21966
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

		// Token: 0x17001C61 RID: 7265
		// (get) Token: 0x060055D1 RID: 21969
		// (set) Token: 0x060055D0 RID: 21968
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

		// Token: 0x17001C62 RID: 7266
		// (get) Token: 0x060055D3 RID: 21971
		// (set) Token: 0x060055D2 RID: 21970
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

		// Token: 0x17001C63 RID: 7267
		// (get) Token: 0x060055D5 RID: 21973
		// (set) Token: 0x060055D4 RID: 21972
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

		// Token: 0x17001C64 RID: 7268
		// (get) Token: 0x060055D6 RID: 21974
		[DispId(-2147417075)]
		public virtual extern object children
		{
			[DispId(-2147417075)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17001C65 RID: 7269
		// (get) Token: 0x060055D7 RID: 21975
		[DispId(-2147417074)]
		public virtual extern object all
		{
			[DispId(-2147417074)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17001C66 RID: 7270
		// (get) Token: 0x060055D8 RID: 21976
		[DispId(-2147417073)]
		public virtual extern string scopeName
		{
			[DispId(-2147417073)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x060055D9 RID: 21977
		[DispId(-2147417072)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void setCapture([In] bool containerCapture = true);

		// Token: 0x060055DA RID: 21978
		[DispId(-2147417071)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void releaseCapture();

		// Token: 0x17001C67 RID: 7271
		// (get) Token: 0x060055DC RID: 21980
		// (set) Token: 0x060055DB RID: 21979
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

		// Token: 0x060055DD RID: 21981
		[DispId(-2147417070)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string componentFromPoint([In] int x, [In] int y);

		// Token: 0x060055DE RID: 21982
		[DispId(-2147417069)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void doScroll([MarshalAs(27)] [In] [Optional] object component);

		// Token: 0x17001C68 RID: 7272
		// (get) Token: 0x060055E0 RID: 21984
		// (set) Token: 0x060055DF RID: 21983
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

		// Token: 0x17001C69 RID: 7273
		// (get) Token: 0x060055E2 RID: 21986
		// (set) Token: 0x060055E1 RID: 21985
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

		// Token: 0x17001C6A RID: 7274
		// (get) Token: 0x060055E4 RID: 21988
		// (set) Token: 0x060055E3 RID: 21987
		[DispId(-2147412062)]
		public virtual extern object ondragend
		{
			[DispId(-2147412062)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412062)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17001C6B RID: 7275
		// (get) Token: 0x060055E6 RID: 21990
		// (set) Token: 0x060055E5 RID: 21989
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

		// Token: 0x17001C6C RID: 7276
		// (get) Token: 0x060055E8 RID: 21992
		// (set) Token: 0x060055E7 RID: 21991
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

		// Token: 0x17001C6D RID: 7277
		// (get) Token: 0x060055EA RID: 21994
		// (set) Token: 0x060055E9 RID: 21993
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

		// Token: 0x17001C6E RID: 7278
		// (get) Token: 0x060055EC RID: 21996
		// (set) Token: 0x060055EB RID: 21995
		[DispId(-2147412058)]
		public virtual extern object ondrop
		{
			[TypeLibFunc(20)]
			[DispId(-2147412058)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412058)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17001C6F RID: 7279
		// (get) Token: 0x060055EE RID: 21998
		// (set) Token: 0x060055ED RID: 21997
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

		// Token: 0x17001C70 RID: 7280
		// (get) Token: 0x060055F0 RID: 22000
		// (set) Token: 0x060055EF RID: 21999
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

		// Token: 0x17001C71 RID: 7281
		// (get) Token: 0x060055F2 RID: 22002
		// (set) Token: 0x060055F1 RID: 22001
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

		// Token: 0x17001C72 RID: 7282
		// (get) Token: 0x060055F4 RID: 22004
		// (set) Token: 0x060055F3 RID: 22003
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

		// Token: 0x17001C73 RID: 7283
		// (get) Token: 0x060055F6 RID: 22006
		// (set) Token: 0x060055F5 RID: 22005
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

		// Token: 0x17001C74 RID: 7284
		// (get) Token: 0x060055F8 RID: 22008
		// (set) Token: 0x060055F7 RID: 22007
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

		// Token: 0x17001C75 RID: 7285
		// (get) Token: 0x060055F9 RID: 22009
		[DispId(-2147417105)]
		public virtual extern IHTMLCurrentStyle currentStyle
		{
			[TypeLibFunc(1024)]
			[DispId(-2147417105)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17001C76 RID: 7286
		// (get) Token: 0x060055FB RID: 22011
		// (set) Token: 0x060055FA RID: 22010
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

		// Token: 0x060055FC RID: 22012
		[DispId(-2147417068)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLRectCollection getClientRects();

		// Token: 0x060055FD RID: 22013
		[DispId(-2147417067)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLRect getBoundingClientRect();

		// Token: 0x060055FE RID: 22014
		[DispId(-2147417608)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void setExpression([MarshalAs(19)] [In] string propname, [MarshalAs(19)] [In] string expression, [MarshalAs(19)] [In] string language = "");

		// Token: 0x060055FF RID: 22015
		[DispId(-2147417607)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		public virtual extern object getExpression([MarshalAs(19)] [In] string propname);

		// Token: 0x06005600 RID: 22016
		[DispId(-2147417606)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool removeExpression([MarshalAs(19)] [In] string propname);

		// Token: 0x17001C77 RID: 7287
		// (get) Token: 0x06005602 RID: 22018
		// (set) Token: 0x06005601 RID: 22017
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

		// Token: 0x06005603 RID: 22019
		[DispId(-2147416112)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void focus();

		// Token: 0x17001C78 RID: 7288
		// (get) Token: 0x06005605 RID: 22021
		// (set) Token: 0x06005604 RID: 22020
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

		// Token: 0x17001C79 RID: 7289
		// (get) Token: 0x06005607 RID: 22023
		// (set) Token: 0x06005606 RID: 22022
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

		// Token: 0x17001C7A RID: 7290
		// (get) Token: 0x06005609 RID: 22025
		// (set) Token: 0x06005608 RID: 22024
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

		// Token: 0x17001C7B RID: 7291
		// (get) Token: 0x0600560B RID: 22027
		// (set) Token: 0x0600560A RID: 22026
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

		// Token: 0x0600560C RID: 22028
		[DispId(-2147416110)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void blur();

		// Token: 0x0600560D RID: 22029
		[DispId(-2147416095)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void addFilter([MarshalAs(25)] [In] object pUnk);

		// Token: 0x0600560E RID: 22030
		[DispId(-2147416094)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void removeFilter([MarshalAs(25)] [In] object pUnk);

		// Token: 0x17001C7C RID: 7292
		// (get) Token: 0x0600560F RID: 22031
		[DispId(-2147416093)]
		public virtual extern int clientHeight
		{
			[DispId(-2147416093)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17001C7D RID: 7293
		// (get) Token: 0x06005610 RID: 22032
		[DispId(-2147416092)]
		public virtual extern int clientWidth
		{
			[DispId(-2147416092)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17001C7E RID: 7294
		// (get) Token: 0x06005611 RID: 22033
		[DispId(-2147416091)]
		public virtual extern int clientTop
		{
			[TypeLibFunc(20)]
			[DispId(-2147416091)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17001C7F RID: 7295
		// (get) Token: 0x06005612 RID: 22034
		[DispId(-2147416090)]
		public virtual extern int clientLeft
		{
			[TypeLibFunc(20)]
			[DispId(-2147416090)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x06005613 RID: 22035
		[DispId(-2147417605)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool attachEvent([MarshalAs(19)] [In] string @event, [MarshalAs(26)] [In] object pdisp);

		// Token: 0x06005614 RID: 22036
		[DispId(-2147417604)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void detachEvent([MarshalAs(19)] [In] string @event, [MarshalAs(26)] [In] object pdisp);

		// Token: 0x17001C80 RID: 7296
		// (get) Token: 0x06005615 RID: 22037
		[DispId(-2147412996)]
		public virtual extern object readyState
		{
			[DispId(-2147412996)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x17001C81 RID: 7297
		// (get) Token: 0x06005617 RID: 22039
		// (set) Token: 0x06005616 RID: 22038
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

		// Token: 0x17001C82 RID: 7298
		// (get) Token: 0x06005619 RID: 22041
		// (set) Token: 0x06005618 RID: 22040
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

		// Token: 0x17001C83 RID: 7299
		// (get) Token: 0x0600561B RID: 22043
		// (set) Token: 0x0600561A RID: 22042
		[DispId(-2147412049)]
		public virtual extern object onrowsinserted
		{
			[DispId(-2147412049)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412049)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17001C84 RID: 7300
		// (get) Token: 0x0600561D RID: 22045
		// (set) Token: 0x0600561C RID: 22044
		[DispId(-2147412048)]
		public virtual extern object oncellchange
		{
			[DispId(-2147412048)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412048)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17001C85 RID: 7301
		// (get) Token: 0x0600561F RID: 22047
		// (set) Token: 0x0600561E RID: 22046
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

		// Token: 0x06005620 RID: 22048
		[DispId(-2147417056)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(26)]
		public virtual extern object createControlRange();

		// Token: 0x17001C86 RID: 7302
		// (get) Token: 0x06005621 RID: 22049
		[DispId(-2147417055)]
		public virtual extern int scrollHeight
		{
			[TypeLibFunc(20)]
			[DispId(-2147417055)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17001C87 RID: 7303
		// (get) Token: 0x06005622 RID: 22050
		[DispId(-2147417054)]
		public virtual extern int scrollWidth
		{
			[DispId(-2147417054)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17001C88 RID: 7304
		// (get) Token: 0x06005624 RID: 22052
		// (set) Token: 0x06005623 RID: 22051
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

		// Token: 0x17001C89 RID: 7305
		// (get) Token: 0x06005626 RID: 22054
		// (set) Token: 0x06005625 RID: 22053
		[DispId(-2147417052)]
		public virtual extern int scrollLeft
		{
			[TypeLibFunc(20)]
			[DispId(-2147417052)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147417052)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x06005627 RID: 22055
		[DispId(-2147417050)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void clearAttributes();

		// Token: 0x17001C8A RID: 7306
		// (get) Token: 0x06005629 RID: 22057
		// (set) Token: 0x06005628 RID: 22056
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

		// Token: 0x0600562A RID: 22058
		[DispId(-2147417043)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElement insertAdjacentElement([MarshalAs(19)] [In] string where, [MarshalAs(28)] [In] IHTMLElement insertedElement);

		// Token: 0x0600562B RID: 22059
		[DispId(-2147417047)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElement applyElement([MarshalAs(28)] [In] IHTMLElement apply, [MarshalAs(19)] [In] string where);

		// Token: 0x0600562C RID: 22060
		[DispId(-2147417042)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string getAdjacentText([MarshalAs(19)] [In] string where);

		// Token: 0x0600562D RID: 22061
		[DispId(-2147417041)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string replaceAdjacentText([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string newText);

		// Token: 0x17001C8B RID: 7307
		// (get) Token: 0x0600562E RID: 22062
		[DispId(-2147417040)]
		public virtual extern bool canHaveChildren
		{
			[DispId(-2147417040)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x0600562F RID: 22063
		[DispId(-2147417032)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern int addBehavior([MarshalAs(19)] [In] string bstrUrl, [MarshalAs(27)] [In] [Optional] ref object pvarFactory);

		// Token: 0x06005630 RID: 22064
		[DispId(-2147417031)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool removeBehavior([In] int cookie);

		// Token: 0x17001C8C RID: 7308
		// (get) Token: 0x06005631 RID: 22065
		[DispId(-2147417048)]
		public virtual extern IHTMLStyle runtimeStyle
		{
			[TypeLibFunc(1024)]
			[DispId(-2147417048)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17001C8D RID: 7309
		// (get) Token: 0x06005632 RID: 22066
		[DispId(-2147417030)]
		public virtual extern object behaviorUrns
		{
			[DispId(-2147417030)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17001C8E RID: 7310
		// (get) Token: 0x06005634 RID: 22068
		// (set) Token: 0x06005633 RID: 22067
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

		// Token: 0x17001C8F RID: 7311
		// (get) Token: 0x06005636 RID: 22070
		// (set) Token: 0x06005635 RID: 22069
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

		// Token: 0x17001C90 RID: 7312
		// (get) Token: 0x06005637 RID: 22071
		[DispId(-2147417028)]
		public virtual extern int readyStateValue
		{
			[DispId(-2147417028)]
			[TypeLibFunc(65)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x06005638 RID: 22072
		[DispId(-2147417027)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElementCollection getElementsByTagName([MarshalAs(19)] [In] string v);

		// Token: 0x06005639 RID: 22073
		[DispId(-2147417016)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void mergeAttributes([MarshalAs(28)] [In] IHTMLElement mergeThis, [MarshalAs(27)] [In] [Optional] ref object pvarFlags);

		// Token: 0x17001C91 RID: 7313
		// (get) Token: 0x0600563A RID: 22074
		[DispId(-2147417015)]
		public virtual extern bool isMultiLine
		{
			[DispId(-2147417015)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17001C92 RID: 7314
		// (get) Token: 0x0600563B RID: 22075
		[DispId(-2147417014)]
		public virtual extern bool canHaveHTML
		{
			[DispId(-2147417014)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17001C93 RID: 7315
		// (get) Token: 0x0600563D RID: 22077
		// (set) Token: 0x0600563C RID: 22076
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

		// Token: 0x17001C94 RID: 7316
		// (get) Token: 0x0600563F RID: 22079
		// (set) Token: 0x0600563E RID: 22078
		[DispId(-2147412038)]
		public virtual extern object onpage
		{
			[DispId(-2147412038)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412038)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17001C95 RID: 7317
		// (get) Token: 0x06005641 RID: 22081
		// (set) Token: 0x06005640 RID: 22080
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

		// Token: 0x17001C96 RID: 7318
		// (get) Token: 0x06005643 RID: 22083
		// (set) Token: 0x06005642 RID: 22082
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

		// Token: 0x06005644 RID: 22084
		[DispId(-2147417011)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void setActive();

		// Token: 0x17001C97 RID: 7319
		// (get) Token: 0x06005646 RID: 22086
		// (set) Token: 0x06005645 RID: 22085
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

		// Token: 0x17001C98 RID: 7320
		// (get) Token: 0x06005647 RID: 22087
		[DispId(-2147417010)]
		public virtual extern bool isContentEditable
		{
			[DispId(-2147417010)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17001C99 RID: 7321
		// (get) Token: 0x06005649 RID: 22089
		// (set) Token: 0x06005648 RID: 22088
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

		// Token: 0x17001C9A RID: 7322
		// (get) Token: 0x0600564B RID: 22091
		// (set) Token: 0x0600564A RID: 22090
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

		// Token: 0x17001C9B RID: 7323
		// (get) Token: 0x0600564C RID: 22092
		[DispId(-2147417007)]
		public virtual extern bool isDisabled
		{
			[DispId(-2147417007)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17001C9C RID: 7324
		// (get) Token: 0x0600564E RID: 22094
		// (set) Token: 0x0600564D RID: 22093
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

		// Token: 0x17001C9D RID: 7325
		// (get) Token: 0x06005650 RID: 22096
		// (set) Token: 0x0600564F RID: 22095
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

		// Token: 0x06005651 RID: 22097
		[DispId(-2147417006)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool FireEvent([MarshalAs(19)] [In] string bstrEventName, [MarshalAs(27)] [In] [Optional] ref object pvarEventObject);

		// Token: 0x17001C9E RID: 7326
		// (get) Token: 0x06005653 RID: 22099
		// (set) Token: 0x06005652 RID: 22098
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

		// Token: 0x17001C9F RID: 7327
		// (get) Token: 0x06005655 RID: 22101
		// (set) Token: 0x06005654 RID: 22100
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

		// Token: 0x17001CA0 RID: 7328
		// (get) Token: 0x06005657 RID: 22103
		// (set) Token: 0x06005656 RID: 22102
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

		// Token: 0x17001CA1 RID: 7329
		// (get) Token: 0x06005659 RID: 22105
		// (set) Token: 0x06005658 RID: 22104
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

		// Token: 0x17001CA2 RID: 7330
		// (get) Token: 0x0600565B RID: 22107
		// (set) Token: 0x0600565A RID: 22106
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

		// Token: 0x17001CA3 RID: 7331
		// (get) Token: 0x0600565D RID: 22109
		// (set) Token: 0x0600565C RID: 22108
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

		// Token: 0x17001CA4 RID: 7332
		// (get) Token: 0x0600565F RID: 22111
		// (set) Token: 0x0600565E RID: 22110
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

		// Token: 0x17001CA5 RID: 7333
		// (get) Token: 0x06005661 RID: 22113
		// (set) Token: 0x06005660 RID: 22112
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

		// Token: 0x06005662 RID: 22114
		[DispId(-2147417005)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool dragDrop();

		// Token: 0x17001CA6 RID: 7334
		// (get) Token: 0x06005663 RID: 22115
		[DispId(-2147417004)]
		public virtual extern int glyphMode
		{
			[TypeLibFunc(1089)]
			[DispId(-2147417004)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17001CA7 RID: 7335
		// (get) Token: 0x06005665 RID: 22117
		// (set) Token: 0x06005664 RID: 22116
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

		// Token: 0x06005666 RID: 22118
		[DispId(-2147417000)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void normalize();

		// Token: 0x06005667 RID: 22119
		[DispId(-2147417003)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute getAttributeNode([MarshalAs(19)] [In] string bstrName);

		// Token: 0x06005668 RID: 22120
		[DispId(-2147417002)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute setAttributeNode([MarshalAs(28)] [In] IHTMLDOMAttribute pattr);

		// Token: 0x06005669 RID: 22121
		[DispId(-2147417001)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute removeAttributeNode([MarshalAs(28)] [In] IHTMLDOMAttribute pattr);

		// Token: 0x17001CA8 RID: 7336
		// (get) Token: 0x0600566B RID: 22123
		// (set) Token: 0x0600566A RID: 22122
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

		// Token: 0x17001CA9 RID: 7337
		// (get) Token: 0x0600566D RID: 22125
		// (set) Token: 0x0600566C RID: 22124
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

		// Token: 0x17001CAA RID: 7338
		// (get) Token: 0x0600566F RID: 22127
		// (set) Token: 0x0600566E RID: 22126
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

		// Token: 0x17001CAB RID: 7339
		// (get) Token: 0x06005670 RID: 22128
		[DispId(-2147417058)]
		public virtual extern int uniqueNumber
		{
			[DispId(-2147417058)]
			[TypeLibFunc(64)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17001CAC RID: 7340
		// (get) Token: 0x06005671 RID: 22129
		[DispId(-2147417057)]
		public virtual extern string uniqueID
		{
			[DispId(-2147417057)]
			[TypeLibFunc(64)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17001CAD RID: 7341
		// (get) Token: 0x06005672 RID: 22130
		[DispId(-2147417066)]
		public virtual extern int nodeType
		{
			[DispId(-2147417066)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17001CAE RID: 7342
		// (get) Token: 0x06005673 RID: 22131
		[DispId(-2147417065)]
		public virtual extern IHTMLDOMNode parentNode
		{
			[DispId(-2147417065)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x06005674 RID: 22132
		[DispId(-2147417064)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool hasChildNodes();

		// Token: 0x17001CAF RID: 7343
		// (get) Token: 0x06005675 RID: 22133
		[DispId(-2147417063)]
		public virtual extern object childNodes
		{
			[DispId(-2147417063)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17001CB0 RID: 7344
		// (get) Token: 0x06005676 RID: 22134
		[DispId(-2147417062)]
		public virtual extern object attributes
		{
			[DispId(-2147417062)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x06005677 RID: 22135
		[DispId(-2147417061)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode insertBefore([MarshalAs(28)] [In] IHTMLDOMNode newChild, [MarshalAs(27)] [In] [Optional] object refChild);

		// Token: 0x06005678 RID: 22136
		[DispId(-2147417060)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode removeChild([MarshalAs(28)] [In] IHTMLDOMNode oldChild);

		// Token: 0x06005679 RID: 22137
		[DispId(-2147417059)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode replaceChild([MarshalAs(28)] [In] IHTMLDOMNode newChild, [MarshalAs(28)] [In] IHTMLDOMNode oldChild);

		// Token: 0x0600567A RID: 22138
		[DispId(-2147417051)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode cloneNode([In] bool fDeep);

		// Token: 0x0600567B RID: 22139
		[DispId(-2147417046)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode removeNode([In] bool fDeep = false);

		// Token: 0x0600567C RID: 22140
		[DispId(-2147417044)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode swapNode([MarshalAs(28)] [In] IHTMLDOMNode otherNode);

		// Token: 0x0600567D RID: 22141
		[DispId(-2147417045)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode replaceNode([MarshalAs(28)] [In] IHTMLDOMNode replacement);

		// Token: 0x0600567E RID: 22142
		[DispId(-2147417039)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode appendChild([MarshalAs(28)] [In] IHTMLDOMNode newChild);

		// Token: 0x17001CB1 RID: 7345
		// (get) Token: 0x0600567F RID: 22143
		[DispId(-2147417038)]
		public virtual extern string nodeName
		{
			[DispId(-2147417038)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17001CB2 RID: 7346
		// (get) Token: 0x06005681 RID: 22145
		// (set) Token: 0x06005680 RID: 22144
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

		// Token: 0x17001CB3 RID: 7347
		// (get) Token: 0x06005682 RID: 22146
		[DispId(-2147417036)]
		public virtual extern IHTMLDOMNode firstChild
		{
			[DispId(-2147417036)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17001CB4 RID: 7348
		// (get) Token: 0x06005683 RID: 22147
		[DispId(-2147417035)]
		public virtual extern IHTMLDOMNode lastChild
		{
			[DispId(-2147417035)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17001CB5 RID: 7349
		// (get) Token: 0x06005684 RID: 22148
		[DispId(-2147417034)]
		public virtual extern IHTMLDOMNode previousSibling
		{
			[DispId(-2147417034)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17001CB6 RID: 7350
		// (get) Token: 0x06005685 RID: 22149
		[DispId(-2147417033)]
		public virtual extern IHTMLDOMNode nextSibling
		{
			[DispId(-2147417033)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17001CB7 RID: 7351
		// (get) Token: 0x06005686 RID: 22150
		[DispId(-2147416999)]
		public virtual extern object ownerDocument
		{
			[DispId(-2147416999)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17001CB8 RID: 7352
		// (get) Token: 0x06005688 RID: 22152
		// (set) Token: 0x06005687 RID: 22151
		[DispId(-2147413095)]
		public virtual extern string type
		{
			[DispId(-2147413095)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413095)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17001CB9 RID: 7353
		// (get) Token: 0x0600568A RID: 22154
		// (set) Token: 0x06005689 RID: 22153
		[DispId(1001)]
		public virtual extern int value
		{
			[TypeLibFunc(20)]
			[DispId(1001)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[DispId(1001)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x0600568B RID: 22155
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement_setAttribute([MarshalAs(19)] [In] string strAttributeName, [MarshalAs(27)] [In] object AttributeValue, [In] int lFlags = 1);

		// Token: 0x0600568C RID: 22156
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		public virtual extern object IHTMLElement_getAttribute([MarshalAs(19)] [In] string strAttributeName, [In] int lFlags = 0);

		// Token: 0x0600568D RID: 22157
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement_removeAttribute([MarshalAs(19)] [In] string strAttributeName, [In] int lFlags = 1);

		// Token: 0x17001CBA RID: 7354
		// (get) Token: 0x0600568F RID: 22159
		// (set) Token: 0x0600568E RID: 22158
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

		// Token: 0x17001CBB RID: 7355
		// (get) Token: 0x06005691 RID: 22161
		// (set) Token: 0x06005690 RID: 22160
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

		// Token: 0x17001CBC RID: 7356
		// (get) Token: 0x06005692 RID: 22162
		public virtual extern string IHTMLElement_tagName
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17001CBD RID: 7357
		// (get) Token: 0x06005693 RID: 22163
		public virtual extern IHTMLElement IHTMLElement_parentElement
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17001CBE RID: 7358
		// (get) Token: 0x06005694 RID: 22164
		public virtual extern IHTMLStyle IHTMLElement_style
		{
			[TypeLibFunc(1024)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17001CBF RID: 7359
		// (get) Token: 0x06005696 RID: 22166
		// (set) Token: 0x06005695 RID: 22165
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

		// Token: 0x17001CC0 RID: 7360
		// (get) Token: 0x06005698 RID: 22168
		// (set) Token: 0x06005697 RID: 22167
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

		// Token: 0x17001CC1 RID: 7361
		// (get) Token: 0x0600569A RID: 22170
		// (set) Token: 0x06005699 RID: 22169
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

		// Token: 0x17001CC2 RID: 7362
		// (get) Token: 0x0600569C RID: 22172
		// (set) Token: 0x0600569B RID: 22171
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

		// Token: 0x17001CC3 RID: 7363
		// (get) Token: 0x0600569E RID: 22174
		// (set) Token: 0x0600569D RID: 22173
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

		// Token: 0x17001CC4 RID: 7364
		// (get) Token: 0x060056A0 RID: 22176
		// (set) Token: 0x0600569F RID: 22175
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

		// Token: 0x17001CC5 RID: 7365
		// (get) Token: 0x060056A2 RID: 22178
		// (set) Token: 0x060056A1 RID: 22177
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

		// Token: 0x17001CC6 RID: 7366
		// (get) Token: 0x060056A4 RID: 22180
		// (set) Token: 0x060056A3 RID: 22179
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

		// Token: 0x17001CC7 RID: 7367
		// (get) Token: 0x060056A6 RID: 22182
		// (set) Token: 0x060056A5 RID: 22181
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

		// Token: 0x17001CC8 RID: 7368
		// (get) Token: 0x060056A8 RID: 22184
		// (set) Token: 0x060056A7 RID: 22183
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

		// Token: 0x17001CC9 RID: 7369
		// (get) Token: 0x060056AA RID: 22186
		// (set) Token: 0x060056A9 RID: 22185
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

		// Token: 0x17001CCA RID: 7370
		// (get) Token: 0x060056AB RID: 22187
		public virtual extern object IHTMLElement_document
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17001CCB RID: 7371
		// (get) Token: 0x060056AD RID: 22189
		// (set) Token: 0x060056AC RID: 22188
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

		// Token: 0x17001CCC RID: 7372
		// (get) Token: 0x060056AF RID: 22191
		// (set) Token: 0x060056AE RID: 22190
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

		// Token: 0x17001CCD RID: 7373
		// (get) Token: 0x060056B1 RID: 22193
		// (set) Token: 0x060056B0 RID: 22192
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

		// Token: 0x060056B2 RID: 22194
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement_scrollIntoView([MarshalAs(27)] [In] [Optional] object varargStart);

		// Token: 0x060056B3 RID: 22195
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement_contains([MarshalAs(28)] [In] IHTMLElement pChild);

		// Token: 0x17001CCE RID: 7374
		// (get) Token: 0x060056B4 RID: 22196
		public virtual extern int IHTMLElement_sourceIndex
		{
			[TypeLibFunc(4)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17001CCF RID: 7375
		// (get) Token: 0x060056B5 RID: 22197
		public virtual extern object IHTMLElement_recordNumber
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x17001CD0 RID: 7376
		// (get) Token: 0x060056B7 RID: 22199
		// (set) Token: 0x060056B6 RID: 22198
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

		// Token: 0x17001CD1 RID: 7377
		// (get) Token: 0x060056B8 RID: 22200
		public virtual extern int IHTMLElement_offsetLeft
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17001CD2 RID: 7378
		// (get) Token: 0x060056B9 RID: 22201
		public virtual extern int IHTMLElement_offsetTop
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17001CD3 RID: 7379
		// (get) Token: 0x060056BA RID: 22202
		public virtual extern int IHTMLElement_offsetWidth
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17001CD4 RID: 7380
		// (get) Token: 0x060056BB RID: 22203
		public virtual extern int IHTMLElement_offsetHeight
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17001CD5 RID: 7381
		// (get) Token: 0x060056BC RID: 22204
		public virtual extern IHTMLElement IHTMLElement_offsetParent
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17001CD6 RID: 7382
		// (get) Token: 0x060056BE RID: 22206
		// (set) Token: 0x060056BD RID: 22205
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

		// Token: 0x17001CD7 RID: 7383
		// (get) Token: 0x060056C0 RID: 22208
		// (set) Token: 0x060056BF RID: 22207
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

		// Token: 0x17001CD8 RID: 7384
		// (get) Token: 0x060056C2 RID: 22210
		// (set) Token: 0x060056C1 RID: 22209
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

		// Token: 0x17001CD9 RID: 7385
		// (get) Token: 0x060056C4 RID: 22212
		// (set) Token: 0x060056C3 RID: 22211
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

		// Token: 0x060056C5 RID: 22213
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement_insertAdjacentHTML([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string html);

		// Token: 0x060056C6 RID: 22214
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement_insertAdjacentText([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string text);

		// Token: 0x17001CDA RID: 7386
		// (get) Token: 0x060056C7 RID: 22215
		public virtual extern IHTMLElement IHTMLElement_parentTextEdit
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17001CDB RID: 7387
		// (get) Token: 0x060056C8 RID: 22216
		public virtual extern bool IHTMLElement_isTextEdit
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x060056C9 RID: 22217
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement_click();

		// Token: 0x17001CDC RID: 7388
		// (get) Token: 0x060056CA RID: 22218
		public virtual extern IHTMLFiltersCollection IHTMLElement_filters
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17001CDD RID: 7389
		// (get) Token: 0x060056CC RID: 22220
		// (set) Token: 0x060056CB RID: 22219
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

		// Token: 0x060056CD RID: 22221
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string IHTMLElement_toString();

		// Token: 0x17001CDE RID: 7390
		// (get) Token: 0x060056CF RID: 22223
		// (set) Token: 0x060056CE RID: 22222
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

		// Token: 0x17001CDF RID: 7391
		// (get) Token: 0x060056D1 RID: 22225
		// (set) Token: 0x060056D0 RID: 22224
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

		// Token: 0x17001CE0 RID: 7392
		// (get) Token: 0x060056D3 RID: 22227
		// (set) Token: 0x060056D2 RID: 22226
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

		// Token: 0x17001CE1 RID: 7393
		// (get) Token: 0x060056D5 RID: 22229
		// (set) Token: 0x060056D4 RID: 22228
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

		// Token: 0x17001CE2 RID: 7394
		// (get) Token: 0x060056D7 RID: 22231
		// (set) Token: 0x060056D6 RID: 22230
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

		// Token: 0x17001CE3 RID: 7395
		// (get) Token: 0x060056D9 RID: 22233
		// (set) Token: 0x060056D8 RID: 22232
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

		// Token: 0x17001CE4 RID: 7396
		// (get) Token: 0x060056DB RID: 22235
		// (set) Token: 0x060056DA RID: 22234
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

		// Token: 0x17001CE5 RID: 7397
		// (get) Token: 0x060056DD RID: 22237
		// (set) Token: 0x060056DC RID: 22236
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

		// Token: 0x17001CE6 RID: 7398
		// (get) Token: 0x060056DF RID: 22239
		// (set) Token: 0x060056DE RID: 22238
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

		// Token: 0x17001CE7 RID: 7399
		// (get) Token: 0x060056E0 RID: 22240
		public virtual extern object IHTMLElement_children
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17001CE8 RID: 7400
		// (get) Token: 0x060056E1 RID: 22241
		public virtual extern object IHTMLElement_all
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17001CE9 RID: 7401
		// (get) Token: 0x060056E2 RID: 22242
		public virtual extern string IHTMLElement2_scopeName
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x060056E3 RID: 22243
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_setCapture([In] bool containerCapture = true);

		// Token: 0x060056E4 RID: 22244
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_releaseCapture();

		// Token: 0x17001CEA RID: 7402
		// (get) Token: 0x060056E6 RID: 22246
		// (set) Token: 0x060056E5 RID: 22245
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

		// Token: 0x060056E7 RID: 22247
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string IHTMLElement2_componentFromPoint([In] int x, [In] int y);

		// Token: 0x060056E8 RID: 22248
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_doScroll([MarshalAs(27)] [In] [Optional] object component);

		// Token: 0x17001CEB RID: 7403
		// (get) Token: 0x060056EA RID: 22250
		// (set) Token: 0x060056E9 RID: 22249
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

		// Token: 0x17001CEC RID: 7404
		// (get) Token: 0x060056EC RID: 22252
		// (set) Token: 0x060056EB RID: 22251
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

		// Token: 0x17001CED RID: 7405
		// (get) Token: 0x060056EE RID: 22254
		// (set) Token: 0x060056ED RID: 22253
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

		// Token: 0x17001CEE RID: 7406
		// (get) Token: 0x060056F0 RID: 22256
		// (set) Token: 0x060056EF RID: 22255
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

		// Token: 0x17001CEF RID: 7407
		// (get) Token: 0x060056F2 RID: 22258
		// (set) Token: 0x060056F1 RID: 22257
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

		// Token: 0x17001CF0 RID: 7408
		// (get) Token: 0x060056F4 RID: 22260
		// (set) Token: 0x060056F3 RID: 22259
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

		// Token: 0x17001CF1 RID: 7409
		// (get) Token: 0x060056F6 RID: 22262
		// (set) Token: 0x060056F5 RID: 22261
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

		// Token: 0x17001CF2 RID: 7410
		// (get) Token: 0x060056F8 RID: 22264
		// (set) Token: 0x060056F7 RID: 22263
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

		// Token: 0x17001CF3 RID: 7411
		// (get) Token: 0x060056FA RID: 22266
		// (set) Token: 0x060056F9 RID: 22265
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

		// Token: 0x17001CF4 RID: 7412
		// (get) Token: 0x060056FC RID: 22268
		// (set) Token: 0x060056FB RID: 22267
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

		// Token: 0x17001CF5 RID: 7413
		// (get) Token: 0x060056FE RID: 22270
		// (set) Token: 0x060056FD RID: 22269
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

		// Token: 0x17001CF6 RID: 7414
		// (get) Token: 0x06005700 RID: 22272
		// (set) Token: 0x060056FF RID: 22271
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

		// Token: 0x17001CF7 RID: 7415
		// (get) Token: 0x06005702 RID: 22274
		// (set) Token: 0x06005701 RID: 22273
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

		// Token: 0x17001CF8 RID: 7416
		// (get) Token: 0x06005703 RID: 22275
		public virtual extern IHTMLCurrentStyle IHTMLElement2_currentStyle
		{
			[TypeLibFunc(1024)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17001CF9 RID: 7417
		// (get) Token: 0x06005705 RID: 22277
		// (set) Token: 0x06005704 RID: 22276
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

		// Token: 0x06005706 RID: 22278
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLRectCollection IHTMLElement2_getClientRects();

		// Token: 0x06005707 RID: 22279
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLRect IHTMLElement2_getBoundingClientRect();

		// Token: 0x06005708 RID: 22280
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_setExpression([MarshalAs(19)] [In] string propname, [MarshalAs(19)] [In] string expression, [MarshalAs(19)] [In] string language = "");

		// Token: 0x06005709 RID: 22281
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		public virtual extern object IHTMLElement2_getExpression([MarshalAs(19)] [In] string propname);

		// Token: 0x0600570A RID: 22282
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement2_removeExpression([MarshalAs(19)] [In] string propname);

		// Token: 0x17001CFA RID: 7418
		// (get) Token: 0x0600570C RID: 22284
		// (set) Token: 0x0600570B RID: 22283
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

		// Token: 0x0600570D RID: 22285
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_focus();

		// Token: 0x17001CFB RID: 7419
		// (get) Token: 0x0600570F RID: 22287
		// (set) Token: 0x0600570E RID: 22286
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

		// Token: 0x17001CFC RID: 7420
		// (get) Token: 0x06005711 RID: 22289
		// (set) Token: 0x06005710 RID: 22288
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

		// Token: 0x17001CFD RID: 7421
		// (get) Token: 0x06005713 RID: 22291
		// (set) Token: 0x06005712 RID: 22290
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

		// Token: 0x17001CFE RID: 7422
		// (get) Token: 0x06005715 RID: 22293
		// (set) Token: 0x06005714 RID: 22292
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

		// Token: 0x06005716 RID: 22294
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_blur();

		// Token: 0x06005717 RID: 22295
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_addFilter([MarshalAs(25)] [In] object pUnk);

		// Token: 0x06005718 RID: 22296
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_removeFilter([MarshalAs(25)] [In] object pUnk);

		// Token: 0x17001CFF RID: 7423
		// (get) Token: 0x06005719 RID: 22297
		public virtual extern int IHTMLElement2_clientHeight
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17001D00 RID: 7424
		// (get) Token: 0x0600571A RID: 22298
		public virtual extern int IHTMLElement2_clientWidth
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17001D01 RID: 7425
		// (get) Token: 0x0600571B RID: 22299
		public virtual extern int IHTMLElement2_clientTop
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17001D02 RID: 7426
		// (get) Token: 0x0600571C RID: 22300
		public virtual extern int IHTMLElement2_clientLeft
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x0600571D RID: 22301
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement2_attachEvent([MarshalAs(19)] [In] string @event, [MarshalAs(26)] [In] object pdisp);

		// Token: 0x0600571E RID: 22302
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_detachEvent([MarshalAs(19)] [In] string @event, [MarshalAs(26)] [In] object pdisp);

		// Token: 0x17001D03 RID: 7427
		// (get) Token: 0x0600571F RID: 22303
		public virtual extern object IHTMLElement2_readyState
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x17001D04 RID: 7428
		// (get) Token: 0x06005721 RID: 22305
		// (set) Token: 0x06005720 RID: 22304
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

		// Token: 0x17001D05 RID: 7429
		// (get) Token: 0x06005723 RID: 22307
		// (set) Token: 0x06005722 RID: 22306
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

		// Token: 0x17001D06 RID: 7430
		// (get) Token: 0x06005725 RID: 22309
		// (set) Token: 0x06005724 RID: 22308
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

		// Token: 0x17001D07 RID: 7431
		// (get) Token: 0x06005727 RID: 22311
		// (set) Token: 0x06005726 RID: 22310
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

		// Token: 0x17001D08 RID: 7432
		// (get) Token: 0x06005729 RID: 22313
		// (set) Token: 0x06005728 RID: 22312
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

		// Token: 0x0600572A RID: 22314
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(26)]
		public virtual extern object IHTMLElement2_createControlRange();

		// Token: 0x17001D09 RID: 7433
		// (get) Token: 0x0600572B RID: 22315
		public virtual extern int IHTMLElement2_scrollHeight
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17001D0A RID: 7434
		// (get) Token: 0x0600572C RID: 22316
		public virtual extern int IHTMLElement2_scrollWidth
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17001D0B RID: 7435
		// (get) Token: 0x0600572E RID: 22318
		// (set) Token: 0x0600572D RID: 22317
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

		// Token: 0x17001D0C RID: 7436
		// (get) Token: 0x06005730 RID: 22320
		// (set) Token: 0x0600572F RID: 22319
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

		// Token: 0x06005731 RID: 22321
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_clearAttributes();

		// Token: 0x06005732 RID: 22322
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void mergeAttributes([MarshalAs(28)] [In] IHTMLElement mergeThis);

		// Token: 0x17001D0D RID: 7437
		// (get) Token: 0x06005734 RID: 22324
		// (set) Token: 0x06005733 RID: 22323
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

		// Token: 0x06005735 RID: 22325
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElement IHTMLElement2_insertAdjacentElement([MarshalAs(19)] [In] string where, [MarshalAs(28)] [In] IHTMLElement insertedElement);

		// Token: 0x06005736 RID: 22326
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElement IHTMLElement2_applyElement([MarshalAs(28)] [In] IHTMLElement apply, [MarshalAs(19)] [In] string where);

		// Token: 0x06005737 RID: 22327
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string IHTMLElement2_getAdjacentText([MarshalAs(19)] [In] string where);

		// Token: 0x06005738 RID: 22328
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string IHTMLElement2_replaceAdjacentText([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string newText);

		// Token: 0x17001D0E RID: 7438
		// (get) Token: 0x06005739 RID: 22329
		public virtual extern bool IHTMLElement2_canHaveChildren
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x0600573A RID: 22330
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern int IHTMLElement2_addBehavior([MarshalAs(19)] [In] string bstrUrl, [MarshalAs(27)] [In] [Optional] ref object pvarFactory);

		// Token: 0x0600573B RID: 22331
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement2_removeBehavior([In] int cookie);

		// Token: 0x17001D0F RID: 7439
		// (get) Token: 0x0600573C RID: 22332
		public virtual extern IHTMLStyle IHTMLElement2_runtimeStyle
		{
			[TypeLibFunc(1024)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17001D10 RID: 7440
		// (get) Token: 0x0600573D RID: 22333
		public virtual extern object IHTMLElement2_behaviorUrns
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17001D11 RID: 7441
		// (get) Token: 0x0600573F RID: 22335
		// (set) Token: 0x0600573E RID: 22334
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

		// Token: 0x17001D12 RID: 7442
		// (get) Token: 0x06005741 RID: 22337
		// (set) Token: 0x06005740 RID: 22336
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

		// Token: 0x17001D13 RID: 7443
		// (get) Token: 0x06005742 RID: 22338
		public virtual extern int IHTMLElement2_readyStateValue
		{
			[TypeLibFunc(65)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x06005743 RID: 22339
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElementCollection IHTMLElement2_getElementsByTagName([MarshalAs(19)] [In] string v);

		// Token: 0x06005744 RID: 22340
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void mergeAttributes([MarshalAs(28)] [In] IHTMLElement mergeThis, [MarshalAs(27)] [In] [Optional] ref object pvarFlags);

		// Token: 0x17001D14 RID: 7444
		// (get) Token: 0x06005745 RID: 22341
		public virtual extern bool IHTMLElement3_isMultiLine
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17001D15 RID: 7445
		// (get) Token: 0x06005746 RID: 22342
		public virtual extern bool IHTMLElement3_canHaveHTML
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17001D16 RID: 7446
		// (get) Token: 0x06005748 RID: 22344
		// (set) Token: 0x06005747 RID: 22343
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

		// Token: 0x17001D17 RID: 7447
		// (get) Token: 0x0600574A RID: 22346
		// (set) Token: 0x06005749 RID: 22345
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

		// Token: 0x17001D18 RID: 7448
		// (get) Token: 0x0600574C RID: 22348
		// (set) Token: 0x0600574B RID: 22347
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

		// Token: 0x17001D19 RID: 7449
		// (get) Token: 0x0600574E RID: 22350
		// (set) Token: 0x0600574D RID: 22349
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

		// Token: 0x0600574F RID: 22351
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement3_setActive();

		// Token: 0x17001D1A RID: 7450
		// (get) Token: 0x06005751 RID: 22353
		// (set) Token: 0x06005750 RID: 22352
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

		// Token: 0x17001D1B RID: 7451
		// (get) Token: 0x06005752 RID: 22354
		public virtual extern bool IHTMLElement3_isContentEditable
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17001D1C RID: 7452
		// (get) Token: 0x06005754 RID: 22356
		// (set) Token: 0x06005753 RID: 22355
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

		// Token: 0x17001D1D RID: 7453
		// (get) Token: 0x06005756 RID: 22358
		// (set) Token: 0x06005755 RID: 22357
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

		// Token: 0x17001D1E RID: 7454
		// (get) Token: 0x06005757 RID: 22359
		public virtual extern bool IHTMLElement3_isDisabled
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17001D1F RID: 7455
		// (get) Token: 0x06005759 RID: 22361
		// (set) Token: 0x06005758 RID: 22360
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

		// Token: 0x17001D20 RID: 7456
		// (get) Token: 0x0600575B RID: 22363
		// (set) Token: 0x0600575A RID: 22362
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

		// Token: 0x0600575C RID: 22364
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement3_FireEvent([MarshalAs(19)] [In] string bstrEventName, [MarshalAs(27)] [In] [Optional] ref object pvarEventObject);

		// Token: 0x17001D21 RID: 7457
		// (get) Token: 0x0600575E RID: 22366
		// (set) Token: 0x0600575D RID: 22365
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

		// Token: 0x17001D22 RID: 7458
		// (get) Token: 0x06005760 RID: 22368
		// (set) Token: 0x0600575F RID: 22367
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

		// Token: 0x17001D23 RID: 7459
		// (get) Token: 0x06005762 RID: 22370
		// (set) Token: 0x06005761 RID: 22369
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

		// Token: 0x17001D24 RID: 7460
		// (get) Token: 0x06005764 RID: 22372
		// (set) Token: 0x06005763 RID: 22371
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

		// Token: 0x17001D25 RID: 7461
		// (get) Token: 0x06005766 RID: 22374
		// (set) Token: 0x06005765 RID: 22373
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

		// Token: 0x17001D26 RID: 7462
		// (get) Token: 0x06005768 RID: 22376
		// (set) Token: 0x06005767 RID: 22375
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

		// Token: 0x17001D27 RID: 7463
		// (get) Token: 0x0600576A RID: 22378
		// (set) Token: 0x06005769 RID: 22377
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

		// Token: 0x17001D28 RID: 7464
		// (get) Token: 0x0600576C RID: 22380
		// (set) Token: 0x0600576B RID: 22379
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

		// Token: 0x0600576D RID: 22381
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement3_dragDrop();

		// Token: 0x17001D29 RID: 7465
		// (get) Token: 0x0600576E RID: 22382
		public virtual extern int IHTMLElement3_glyphMode
		{
			[TypeLibFunc(1089)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17001D2A RID: 7466
		// (get) Token: 0x06005770 RID: 22384
		// (set) Token: 0x0600576F RID: 22383
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

		// Token: 0x06005771 RID: 22385
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement4_normalize();

		// Token: 0x06005772 RID: 22386
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute IHTMLElement4_getAttributeNode([MarshalAs(19)] [In] string bstrName);

		// Token: 0x06005773 RID: 22387
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute IHTMLElement4_setAttributeNode([MarshalAs(28)] [In] IHTMLDOMAttribute pattr);

		// Token: 0x06005774 RID: 22388
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute IHTMLElement4_removeAttributeNode([MarshalAs(28)] [In] IHTMLDOMAttribute pattr);

		// Token: 0x17001D2B RID: 7467
		// (get) Token: 0x06005776 RID: 22390
		// (set) Token: 0x06005775 RID: 22389
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

		// Token: 0x17001D2C RID: 7468
		// (get) Token: 0x06005778 RID: 22392
		// (set) Token: 0x06005777 RID: 22391
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

		// Token: 0x17001D2D RID: 7469
		// (get) Token: 0x0600577A RID: 22394
		// (set) Token: 0x06005779 RID: 22393
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

		// Token: 0x17001D2E RID: 7470
		// (get) Token: 0x0600577B RID: 22395
		public virtual extern int IHTMLUniqueName_uniqueNumber
		{
			[TypeLibFunc(64)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17001D2F RID: 7471
		// (get) Token: 0x0600577C RID: 22396
		public virtual extern string IHTMLUniqueName_uniqueID
		{
			[TypeLibFunc(64)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17001D30 RID: 7472
		// (get) Token: 0x0600577D RID: 22397
		public virtual extern int IHTMLDOMNode_nodeType
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17001D31 RID: 7473
		// (get) Token: 0x0600577E RID: 22398
		public virtual extern IHTMLDOMNode IHTMLDOMNode_parentNode
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x0600577F RID: 22399
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLDOMNode_hasChildNodes();

		// Token: 0x17001D32 RID: 7474
		// (get) Token: 0x06005780 RID: 22400
		public virtual extern object IHTMLDOMNode_childNodes
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17001D33 RID: 7475
		// (get) Token: 0x06005781 RID: 22401
		public virtual extern object IHTMLDOMNode_attributes
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x06005782 RID: 22402
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_insertBefore([MarshalAs(28)] [In] IHTMLDOMNode newChild, [MarshalAs(27)] [In] [Optional] object refChild);

		// Token: 0x06005783 RID: 22403
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_removeChild([MarshalAs(28)] [In] IHTMLDOMNode oldChild);

		// Token: 0x06005784 RID: 22404
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_replaceChild([MarshalAs(28)] [In] IHTMLDOMNode newChild, [MarshalAs(28)] [In] IHTMLDOMNode oldChild);

		// Token: 0x06005785 RID: 22405
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_cloneNode([In] bool fDeep);

		// Token: 0x06005786 RID: 22406
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_removeNode([In] bool fDeep = false);

		// Token: 0x06005787 RID: 22407
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_swapNode([MarshalAs(28)] [In] IHTMLDOMNode otherNode);

		// Token: 0x06005788 RID: 22408
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_replaceNode([MarshalAs(28)] [In] IHTMLDOMNode replacement);

		// Token: 0x06005789 RID: 22409
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_appendChild([MarshalAs(28)] [In] IHTMLDOMNode newChild);

		// Token: 0x17001D34 RID: 7476
		// (get) Token: 0x0600578A RID: 22410
		public virtual extern string IHTMLDOMNode_nodeName
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17001D35 RID: 7477
		// (get) Token: 0x0600578C RID: 22412
		// (set) Token: 0x0600578B RID: 22411
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

		// Token: 0x17001D36 RID: 7478
		// (get) Token: 0x0600578D RID: 22413
		public virtual extern IHTMLDOMNode IHTMLDOMNode_firstChild
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17001D37 RID: 7479
		// (get) Token: 0x0600578E RID: 22414
		public virtual extern IHTMLDOMNode IHTMLDOMNode_lastChild
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17001D38 RID: 7480
		// (get) Token: 0x0600578F RID: 22415
		public virtual extern IHTMLDOMNode IHTMLDOMNode_previousSibling
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17001D39 RID: 7481
		// (get) Token: 0x06005790 RID: 22416
		public virtual extern IHTMLDOMNode IHTMLDOMNode_nextSibling
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17001D3A RID: 7482
		// (get) Token: 0x06005791 RID: 22417
		public virtual extern object IHTMLDOMNode2_ownerDocument
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17001D3B RID: 7483
		// (get) Token: 0x06005793 RID: 22419
		// (set) Token: 0x06005792 RID: 22418
		public virtual extern string IHTMLLIElement_type
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

		// Token: 0x17001D3C RID: 7484
		// (get) Token: 0x06005795 RID: 22421
		// (set) Token: 0x06005794 RID: 22420
		public virtual extern int IHTMLLIElement_value
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x1400096C RID: 2412
		// (add) Token: 0x06005796 RID: 22422
		// (remove) Token: 0x06005797 RID: 22423
		public virtual extern event HTMLElementEvents_onhelpEventHandler HTMLElementEvents_Event_onhelp;

		// Token: 0x1400096D RID: 2413
		// (add) Token: 0x06005798 RID: 22424
		// (remove) Token: 0x06005799 RID: 22425
		public virtual extern event HTMLElementEvents_onclickEventHandler HTMLElementEvents_Event_onclick;

		// Token: 0x1400096E RID: 2414
		// (add) Token: 0x0600579A RID: 22426
		// (remove) Token: 0x0600579B RID: 22427
		public virtual extern event HTMLElementEvents_ondblclickEventHandler HTMLElementEvents_Event_ondblclick;

		// Token: 0x1400096F RID: 2415
		// (add) Token: 0x0600579C RID: 22428
		// (remove) Token: 0x0600579D RID: 22429
		public virtual extern event HTMLElementEvents_onkeypressEventHandler HTMLElementEvents_Event_onkeypress;

		// Token: 0x14000970 RID: 2416
		// (add) Token: 0x0600579E RID: 22430
		// (remove) Token: 0x0600579F RID: 22431
		public virtual extern event HTMLElementEvents_onkeydownEventHandler HTMLElementEvents_Event_onkeydown;

		// Token: 0x14000971 RID: 2417
		// (add) Token: 0x060057A0 RID: 22432
		// (remove) Token: 0x060057A1 RID: 22433
		public virtual extern event HTMLElementEvents_onkeyupEventHandler HTMLElementEvents_Event_onkeyup;

		// Token: 0x14000972 RID: 2418
		// (add) Token: 0x060057A2 RID: 22434
		// (remove) Token: 0x060057A3 RID: 22435
		public virtual extern event HTMLElementEvents_onmouseoutEventHandler HTMLElementEvents_Event_onmouseout;

		// Token: 0x14000973 RID: 2419
		// (add) Token: 0x060057A4 RID: 22436
		// (remove) Token: 0x060057A5 RID: 22437
		public virtual extern event HTMLElementEvents_onmouseoverEventHandler HTMLElementEvents_Event_onmouseover;

		// Token: 0x14000974 RID: 2420
		// (add) Token: 0x060057A6 RID: 22438
		// (remove) Token: 0x060057A7 RID: 22439
		public virtual extern event HTMLElementEvents_onmousemoveEventHandler HTMLElementEvents_Event_onmousemove;

		// Token: 0x14000975 RID: 2421
		// (add) Token: 0x060057A8 RID: 22440
		// (remove) Token: 0x060057A9 RID: 22441
		public virtual extern event HTMLElementEvents_onmousedownEventHandler HTMLElementEvents_Event_onmousedown;

		// Token: 0x14000976 RID: 2422
		// (add) Token: 0x060057AA RID: 22442
		// (remove) Token: 0x060057AB RID: 22443
		public virtual extern event HTMLElementEvents_onmouseupEventHandler HTMLElementEvents_Event_onmouseup;

		// Token: 0x14000977 RID: 2423
		// (add) Token: 0x060057AC RID: 22444
		// (remove) Token: 0x060057AD RID: 22445
		public virtual extern event HTMLElementEvents_onselectstartEventHandler HTMLElementEvents_Event_onselectstart;

		// Token: 0x14000978 RID: 2424
		// (add) Token: 0x060057AE RID: 22446
		// (remove) Token: 0x060057AF RID: 22447
		public virtual extern event HTMLElementEvents_onfilterchangeEventHandler HTMLElementEvents_Event_onfilterchange;

		// Token: 0x14000979 RID: 2425
		// (add) Token: 0x060057B0 RID: 22448
		// (remove) Token: 0x060057B1 RID: 22449
		public virtual extern event HTMLElementEvents_ondragstartEventHandler HTMLElementEvents_Event_ondragstart;

		// Token: 0x1400097A RID: 2426
		// (add) Token: 0x060057B2 RID: 22450
		// (remove) Token: 0x060057B3 RID: 22451
		public virtual extern event HTMLElementEvents_onbeforeupdateEventHandler HTMLElementEvents_Event_onbeforeupdate;

		// Token: 0x1400097B RID: 2427
		// (add) Token: 0x060057B4 RID: 22452
		// (remove) Token: 0x060057B5 RID: 22453
		public virtual extern event HTMLElementEvents_onafterupdateEventHandler HTMLElementEvents_Event_onafterupdate;

		// Token: 0x1400097C RID: 2428
		// (add) Token: 0x060057B6 RID: 22454
		// (remove) Token: 0x060057B7 RID: 22455
		public virtual extern event HTMLElementEvents_onerrorupdateEventHandler HTMLElementEvents_Event_onerrorupdate;

		// Token: 0x1400097D RID: 2429
		// (add) Token: 0x060057B8 RID: 22456
		// (remove) Token: 0x060057B9 RID: 22457
		public virtual extern event HTMLElementEvents_onrowexitEventHandler HTMLElementEvents_Event_onrowexit;

		// Token: 0x1400097E RID: 2430
		// (add) Token: 0x060057BA RID: 22458
		// (remove) Token: 0x060057BB RID: 22459
		public virtual extern event HTMLElementEvents_onrowenterEventHandler HTMLElementEvents_Event_onrowenter;

		// Token: 0x1400097F RID: 2431
		// (add) Token: 0x060057BC RID: 22460
		// (remove) Token: 0x060057BD RID: 22461
		public virtual extern event HTMLElementEvents_ondatasetchangedEventHandler HTMLElementEvents_Event_ondatasetchanged;

		// Token: 0x14000980 RID: 2432
		// (add) Token: 0x060057BE RID: 22462
		// (remove) Token: 0x060057BF RID: 22463
		public virtual extern event HTMLElementEvents_ondataavailableEventHandler HTMLElementEvents_Event_ondataavailable;

		// Token: 0x14000981 RID: 2433
		// (add) Token: 0x060057C0 RID: 22464
		// (remove) Token: 0x060057C1 RID: 22465
		public virtual extern event HTMLElementEvents_ondatasetcompleteEventHandler HTMLElementEvents_Event_ondatasetcomplete;

		// Token: 0x14000982 RID: 2434
		// (add) Token: 0x060057C2 RID: 22466
		// (remove) Token: 0x060057C3 RID: 22467
		public virtual extern event HTMLElementEvents_onlosecaptureEventHandler HTMLElementEvents_Event_onlosecapture;

		// Token: 0x14000983 RID: 2435
		// (add) Token: 0x060057C4 RID: 22468
		// (remove) Token: 0x060057C5 RID: 22469
		public virtual extern event HTMLElementEvents_onpropertychangeEventHandler HTMLElementEvents_Event_onpropertychange;

		// Token: 0x14000984 RID: 2436
		// (add) Token: 0x060057C6 RID: 22470
		// (remove) Token: 0x060057C7 RID: 22471
		public virtual extern event HTMLElementEvents_onscrollEventHandler HTMLElementEvents_Event_onscroll;

		// Token: 0x14000985 RID: 2437
		// (add) Token: 0x060057C8 RID: 22472
		// (remove) Token: 0x060057C9 RID: 22473
		public virtual extern event HTMLElementEvents_onfocusEventHandler HTMLElementEvents_Event_onfocus;

		// Token: 0x14000986 RID: 2438
		// (add) Token: 0x060057CA RID: 22474
		// (remove) Token: 0x060057CB RID: 22475
		public virtual extern event HTMLElementEvents_onblurEventHandler HTMLElementEvents_Event_onblur;

		// Token: 0x14000987 RID: 2439
		// (add) Token: 0x060057CC RID: 22476
		// (remove) Token: 0x060057CD RID: 22477
		public virtual extern event HTMLElementEvents_onresizeEventHandler HTMLElementEvents_Event_onresize;

		// Token: 0x14000988 RID: 2440
		// (add) Token: 0x060057CE RID: 22478
		// (remove) Token: 0x060057CF RID: 22479
		public virtual extern event HTMLElementEvents_ondragEventHandler HTMLElementEvents_Event_ondrag;

		// Token: 0x14000989 RID: 2441
		// (add) Token: 0x060057D0 RID: 22480
		// (remove) Token: 0x060057D1 RID: 22481
		public virtual extern event HTMLElementEvents_ondragendEventHandler HTMLElementEvents_Event_ondragend;

		// Token: 0x1400098A RID: 2442
		// (add) Token: 0x060057D2 RID: 22482
		// (remove) Token: 0x060057D3 RID: 22483
		public virtual extern event HTMLElementEvents_ondragenterEventHandler HTMLElementEvents_Event_ondragenter;

		// Token: 0x1400098B RID: 2443
		// (add) Token: 0x060057D4 RID: 22484
		// (remove) Token: 0x060057D5 RID: 22485
		public virtual extern event HTMLElementEvents_ondragoverEventHandler HTMLElementEvents_Event_ondragover;

		// Token: 0x1400098C RID: 2444
		// (add) Token: 0x060057D6 RID: 22486
		// (remove) Token: 0x060057D7 RID: 22487
		public virtual extern event HTMLElementEvents_ondragleaveEventHandler HTMLElementEvents_Event_ondragleave;

		// Token: 0x1400098D RID: 2445
		// (add) Token: 0x060057D8 RID: 22488
		// (remove) Token: 0x060057D9 RID: 22489
		public virtual extern event HTMLElementEvents_ondropEventHandler HTMLElementEvents_Event_ondrop;

		// Token: 0x1400098E RID: 2446
		// (add) Token: 0x060057DA RID: 22490
		// (remove) Token: 0x060057DB RID: 22491
		public virtual extern event HTMLElementEvents_onbeforecutEventHandler HTMLElementEvents_Event_onbeforecut;

		// Token: 0x1400098F RID: 2447
		// (add) Token: 0x060057DC RID: 22492
		// (remove) Token: 0x060057DD RID: 22493
		public virtual extern event HTMLElementEvents_oncutEventHandler HTMLElementEvents_Event_oncut;

		// Token: 0x14000990 RID: 2448
		// (add) Token: 0x060057DE RID: 22494
		// (remove) Token: 0x060057DF RID: 22495
		public virtual extern event HTMLElementEvents_onbeforecopyEventHandler HTMLElementEvents_Event_onbeforecopy;

		// Token: 0x14000991 RID: 2449
		// (add) Token: 0x060057E0 RID: 22496
		// (remove) Token: 0x060057E1 RID: 22497
		public virtual extern event HTMLElementEvents_oncopyEventHandler HTMLElementEvents_Event_oncopy;

		// Token: 0x14000992 RID: 2450
		// (add) Token: 0x060057E2 RID: 22498
		// (remove) Token: 0x060057E3 RID: 22499
		public virtual extern event HTMLElementEvents_onbeforepasteEventHandler HTMLElementEvents_Event_onbeforepaste;

		// Token: 0x14000993 RID: 2451
		// (add) Token: 0x060057E4 RID: 22500
		// (remove) Token: 0x060057E5 RID: 22501
		public virtual extern event HTMLElementEvents_onpasteEventHandler HTMLElementEvents_Event_onpaste;

		// Token: 0x14000994 RID: 2452
		// (add) Token: 0x060057E6 RID: 22502
		// (remove) Token: 0x060057E7 RID: 22503
		public virtual extern event HTMLElementEvents_oncontextmenuEventHandler HTMLElementEvents_Event_oncontextmenu;

		// Token: 0x14000995 RID: 2453
		// (add) Token: 0x060057E8 RID: 22504
		// (remove) Token: 0x060057E9 RID: 22505
		public virtual extern event HTMLElementEvents_onrowsdeleteEventHandler HTMLElementEvents_Event_onrowsdelete;

		// Token: 0x14000996 RID: 2454
		// (add) Token: 0x060057EA RID: 22506
		// (remove) Token: 0x060057EB RID: 22507
		public virtual extern event HTMLElementEvents_onrowsinsertedEventHandler HTMLElementEvents_Event_onrowsinserted;

		// Token: 0x14000997 RID: 2455
		// (add) Token: 0x060057EC RID: 22508
		// (remove) Token: 0x060057ED RID: 22509
		public virtual extern event HTMLElementEvents_oncellchangeEventHandler HTMLElementEvents_Event_oncellchange;

		// Token: 0x14000998 RID: 2456
		// (add) Token: 0x060057EE RID: 22510
		// (remove) Token: 0x060057EF RID: 22511
		public virtual extern event HTMLElementEvents_onreadystatechangeEventHandler HTMLElementEvents_Event_onreadystatechange;

		// Token: 0x14000999 RID: 2457
		// (add) Token: 0x060057F0 RID: 22512
		// (remove) Token: 0x060057F1 RID: 22513
		public virtual extern event HTMLElementEvents_onbeforeeditfocusEventHandler HTMLElementEvents_Event_onbeforeeditfocus;

		// Token: 0x1400099A RID: 2458
		// (add) Token: 0x060057F2 RID: 22514
		// (remove) Token: 0x060057F3 RID: 22515
		public virtual extern event HTMLElementEvents_onlayoutcompleteEventHandler HTMLElementEvents_Event_onlayoutcomplete;

		// Token: 0x1400099B RID: 2459
		// (add) Token: 0x060057F4 RID: 22516
		// (remove) Token: 0x060057F5 RID: 22517
		public virtual extern event HTMLElementEvents_onpageEventHandler HTMLElementEvents_Event_onpage;

		// Token: 0x1400099C RID: 2460
		// (add) Token: 0x060057F6 RID: 22518
		// (remove) Token: 0x060057F7 RID: 22519
		public virtual extern event HTMLElementEvents_onbeforedeactivateEventHandler HTMLElementEvents_Event_onbeforedeactivate;

		// Token: 0x1400099D RID: 2461
		// (add) Token: 0x060057F8 RID: 22520
		// (remove) Token: 0x060057F9 RID: 22521
		public virtual extern event HTMLElementEvents_onbeforeactivateEventHandler HTMLElementEvents_Event_onbeforeactivate;

		// Token: 0x1400099E RID: 2462
		// (add) Token: 0x060057FA RID: 22522
		// (remove) Token: 0x060057FB RID: 22523
		public virtual extern event HTMLElementEvents_onmoveEventHandler HTMLElementEvents_Event_onmove;

		// Token: 0x1400099F RID: 2463
		// (add) Token: 0x060057FC RID: 22524
		// (remove) Token: 0x060057FD RID: 22525
		public virtual extern event HTMLElementEvents_oncontrolselectEventHandler HTMLElementEvents_Event_oncontrolselect;

		// Token: 0x140009A0 RID: 2464
		// (add) Token: 0x060057FE RID: 22526
		// (remove) Token: 0x060057FF RID: 22527
		public virtual extern event HTMLElementEvents_onmovestartEventHandler HTMLElementEvents_Event_onmovestart;

		// Token: 0x140009A1 RID: 2465
		// (add) Token: 0x06005800 RID: 22528
		// (remove) Token: 0x06005801 RID: 22529
		public virtual extern event HTMLElementEvents_onmoveendEventHandler HTMLElementEvents_Event_onmoveend;

		// Token: 0x140009A2 RID: 2466
		// (add) Token: 0x06005802 RID: 22530
		// (remove) Token: 0x06005803 RID: 22531
		public virtual extern event HTMLElementEvents_onresizestartEventHandler HTMLElementEvents_Event_onresizestart;

		// Token: 0x140009A3 RID: 2467
		// (add) Token: 0x06005804 RID: 22532
		// (remove) Token: 0x06005805 RID: 22533
		public virtual extern event HTMLElementEvents_onresizeendEventHandler HTMLElementEvents_Event_onresizeend;

		// Token: 0x140009A4 RID: 2468
		// (add) Token: 0x06005806 RID: 22534
		// (remove) Token: 0x06005807 RID: 22535
		public virtual extern event HTMLElementEvents_onmouseenterEventHandler HTMLElementEvents_Event_onmouseenter;

		// Token: 0x140009A5 RID: 2469
		// (add) Token: 0x06005808 RID: 22536
		// (remove) Token: 0x06005809 RID: 22537
		public virtual extern event HTMLElementEvents_onmouseleaveEventHandler HTMLElementEvents_Event_onmouseleave;

		// Token: 0x140009A6 RID: 2470
		// (add) Token: 0x0600580A RID: 22538
		// (remove) Token: 0x0600580B RID: 22539
		public virtual extern event HTMLElementEvents_onmousewheelEventHandler HTMLElementEvents_Event_onmousewheel;

		// Token: 0x140009A7 RID: 2471
		// (add) Token: 0x0600580C RID: 22540
		// (remove) Token: 0x0600580D RID: 22541
		public virtual extern event HTMLElementEvents_onactivateEventHandler HTMLElementEvents_Event_onactivate;

		// Token: 0x140009A8 RID: 2472
		// (add) Token: 0x0600580E RID: 22542
		// (remove) Token: 0x0600580F RID: 22543
		public virtual extern event HTMLElementEvents_ondeactivateEventHandler HTMLElementEvents_Event_ondeactivate;

		// Token: 0x140009A9 RID: 2473
		// (add) Token: 0x06005810 RID: 22544
		// (remove) Token: 0x06005811 RID: 22545
		public virtual extern event HTMLElementEvents_onfocusinEventHandler HTMLElementEvents_Event_onfocusin;

		// Token: 0x140009AA RID: 2474
		// (add) Token: 0x06005812 RID: 22546
		// (remove) Token: 0x06005813 RID: 22547
		public virtual extern event HTMLElementEvents_onfocusoutEventHandler HTMLElementEvents_Event_onfocusout;

		// Token: 0x140009AB RID: 2475
		// (add) Token: 0x06005814 RID: 22548
		// (remove) Token: 0x06005815 RID: 22549
		public virtual extern event HTMLElementEvents2_onhelpEventHandler HTMLElementEvents2_Event_onhelp;

		// Token: 0x140009AC RID: 2476
		// (add) Token: 0x06005816 RID: 22550
		// (remove) Token: 0x06005817 RID: 22551
		public virtual extern event HTMLElementEvents2_onclickEventHandler HTMLElementEvents2_Event_onclick;

		// Token: 0x140009AD RID: 2477
		// (add) Token: 0x06005818 RID: 22552
		// (remove) Token: 0x06005819 RID: 22553
		public virtual extern event HTMLElementEvents2_ondblclickEventHandler HTMLElementEvents2_Event_ondblclick;

		// Token: 0x140009AE RID: 2478
		// (add) Token: 0x0600581A RID: 22554
		// (remove) Token: 0x0600581B RID: 22555
		public virtual extern event HTMLElementEvents2_onkeypressEventHandler HTMLElementEvents2_Event_onkeypress;

		// Token: 0x140009AF RID: 2479
		// (add) Token: 0x0600581C RID: 22556
		// (remove) Token: 0x0600581D RID: 22557
		public virtual extern event HTMLElementEvents2_onkeydownEventHandler HTMLElementEvents2_Event_onkeydown;

		// Token: 0x140009B0 RID: 2480
		// (add) Token: 0x0600581E RID: 22558
		// (remove) Token: 0x0600581F RID: 22559
		public virtual extern event HTMLElementEvents2_onkeyupEventHandler HTMLElementEvents2_Event_onkeyup;

		// Token: 0x140009B1 RID: 2481
		// (add) Token: 0x06005820 RID: 22560
		// (remove) Token: 0x06005821 RID: 22561
		public virtual extern event HTMLElementEvents2_onmouseoutEventHandler HTMLElementEvents2_Event_onmouseout;

		// Token: 0x140009B2 RID: 2482
		// (add) Token: 0x06005822 RID: 22562
		// (remove) Token: 0x06005823 RID: 22563
		public virtual extern event HTMLElementEvents2_onmouseoverEventHandler HTMLElementEvents2_Event_onmouseover;

		// Token: 0x140009B3 RID: 2483
		// (add) Token: 0x06005824 RID: 22564
		// (remove) Token: 0x06005825 RID: 22565
		public virtual extern event HTMLElementEvents2_onmousemoveEventHandler HTMLElementEvents2_Event_onmousemove;

		// Token: 0x140009B4 RID: 2484
		// (add) Token: 0x06005826 RID: 22566
		// (remove) Token: 0x06005827 RID: 22567
		public virtual extern event HTMLElementEvents2_onmousedownEventHandler HTMLElementEvents2_Event_onmousedown;

		// Token: 0x140009B5 RID: 2485
		// (add) Token: 0x06005828 RID: 22568
		// (remove) Token: 0x06005829 RID: 22569
		public virtual extern event HTMLElementEvents2_onmouseupEventHandler HTMLElementEvents2_Event_onmouseup;

		// Token: 0x140009B6 RID: 2486
		// (add) Token: 0x0600582A RID: 22570
		// (remove) Token: 0x0600582B RID: 22571
		public virtual extern event HTMLElementEvents2_onselectstartEventHandler HTMLElementEvents2_Event_onselectstart;

		// Token: 0x140009B7 RID: 2487
		// (add) Token: 0x0600582C RID: 22572
		// (remove) Token: 0x0600582D RID: 22573
		public virtual extern event HTMLElementEvents2_onfilterchangeEventHandler HTMLElementEvents2_Event_onfilterchange;

		// Token: 0x140009B8 RID: 2488
		// (add) Token: 0x0600582E RID: 22574
		// (remove) Token: 0x0600582F RID: 22575
		public virtual extern event HTMLElementEvents2_ondragstartEventHandler HTMLElementEvents2_Event_ondragstart;

		// Token: 0x140009B9 RID: 2489
		// (add) Token: 0x06005830 RID: 22576
		// (remove) Token: 0x06005831 RID: 22577
		public virtual extern event HTMLElementEvents2_onbeforeupdateEventHandler HTMLElementEvents2_Event_onbeforeupdate;

		// Token: 0x140009BA RID: 2490
		// (add) Token: 0x06005832 RID: 22578
		// (remove) Token: 0x06005833 RID: 22579
		public virtual extern event HTMLElementEvents2_onafterupdateEventHandler HTMLElementEvents2_Event_onafterupdate;

		// Token: 0x140009BB RID: 2491
		// (add) Token: 0x06005834 RID: 22580
		// (remove) Token: 0x06005835 RID: 22581
		public virtual extern event HTMLElementEvents2_onerrorupdateEventHandler HTMLElementEvents2_Event_onerrorupdate;

		// Token: 0x140009BC RID: 2492
		// (add) Token: 0x06005836 RID: 22582
		// (remove) Token: 0x06005837 RID: 22583
		public virtual extern event HTMLElementEvents2_onrowexitEventHandler HTMLElementEvents2_Event_onrowexit;

		// Token: 0x140009BD RID: 2493
		// (add) Token: 0x06005838 RID: 22584
		// (remove) Token: 0x06005839 RID: 22585
		public virtual extern event HTMLElementEvents2_onrowenterEventHandler HTMLElementEvents2_Event_onrowenter;

		// Token: 0x140009BE RID: 2494
		// (add) Token: 0x0600583A RID: 22586
		// (remove) Token: 0x0600583B RID: 22587
		public virtual extern event HTMLElementEvents2_ondatasetchangedEventHandler HTMLElementEvents2_Event_ondatasetchanged;

		// Token: 0x140009BF RID: 2495
		// (add) Token: 0x0600583C RID: 22588
		// (remove) Token: 0x0600583D RID: 22589
		public virtual extern event HTMLElementEvents2_ondataavailableEventHandler HTMLElementEvents2_Event_ondataavailable;

		// Token: 0x140009C0 RID: 2496
		// (add) Token: 0x0600583E RID: 22590
		// (remove) Token: 0x0600583F RID: 22591
		public virtual extern event HTMLElementEvents2_ondatasetcompleteEventHandler HTMLElementEvents2_Event_ondatasetcomplete;

		// Token: 0x140009C1 RID: 2497
		// (add) Token: 0x06005840 RID: 22592
		// (remove) Token: 0x06005841 RID: 22593
		public virtual extern event HTMLElementEvents2_onlosecaptureEventHandler HTMLElementEvents2_Event_onlosecapture;

		// Token: 0x140009C2 RID: 2498
		// (add) Token: 0x06005842 RID: 22594
		// (remove) Token: 0x06005843 RID: 22595
		public virtual extern event HTMLElementEvents2_onpropertychangeEventHandler HTMLElementEvents2_Event_onpropertychange;

		// Token: 0x140009C3 RID: 2499
		// (add) Token: 0x06005844 RID: 22596
		// (remove) Token: 0x06005845 RID: 22597
		public virtual extern event HTMLElementEvents2_onscrollEventHandler HTMLElementEvents2_Event_onscroll;

		// Token: 0x140009C4 RID: 2500
		// (add) Token: 0x06005846 RID: 22598
		// (remove) Token: 0x06005847 RID: 22599
		public virtual extern event HTMLElementEvents2_onfocusEventHandler HTMLElementEvents2_Event_onfocus;

		// Token: 0x140009C5 RID: 2501
		// (add) Token: 0x06005848 RID: 22600
		// (remove) Token: 0x06005849 RID: 22601
		public virtual extern event HTMLElementEvents2_onblurEventHandler HTMLElementEvents2_Event_onblur;

		// Token: 0x140009C6 RID: 2502
		// (add) Token: 0x0600584A RID: 22602
		// (remove) Token: 0x0600584B RID: 22603
		public virtual extern event HTMLElementEvents2_onresizeEventHandler HTMLElementEvents2_Event_onresize;

		// Token: 0x140009C7 RID: 2503
		// (add) Token: 0x0600584C RID: 22604
		// (remove) Token: 0x0600584D RID: 22605
		public virtual extern event HTMLElementEvents2_ondragEventHandler HTMLElementEvents2_Event_ondrag;

		// Token: 0x140009C8 RID: 2504
		// (add) Token: 0x0600584E RID: 22606
		// (remove) Token: 0x0600584F RID: 22607
		public virtual extern event HTMLElementEvents2_ondragendEventHandler HTMLElementEvents2_Event_ondragend;

		// Token: 0x140009C9 RID: 2505
		// (add) Token: 0x06005850 RID: 22608
		// (remove) Token: 0x06005851 RID: 22609
		public virtual extern event HTMLElementEvents2_ondragenterEventHandler HTMLElementEvents2_Event_ondragenter;

		// Token: 0x140009CA RID: 2506
		// (add) Token: 0x06005852 RID: 22610
		// (remove) Token: 0x06005853 RID: 22611
		public virtual extern event HTMLElementEvents2_ondragoverEventHandler HTMLElementEvents2_Event_ondragover;

		// Token: 0x140009CB RID: 2507
		// (add) Token: 0x06005854 RID: 22612
		// (remove) Token: 0x06005855 RID: 22613
		public virtual extern event HTMLElementEvents2_ondragleaveEventHandler HTMLElementEvents2_Event_ondragleave;

		// Token: 0x140009CC RID: 2508
		// (add) Token: 0x06005856 RID: 22614
		// (remove) Token: 0x06005857 RID: 22615
		public virtual extern event HTMLElementEvents2_ondropEventHandler HTMLElementEvents2_Event_ondrop;

		// Token: 0x140009CD RID: 2509
		// (add) Token: 0x06005858 RID: 22616
		// (remove) Token: 0x06005859 RID: 22617
		public virtual extern event HTMLElementEvents2_onbeforecutEventHandler HTMLElementEvents2_Event_onbeforecut;

		// Token: 0x140009CE RID: 2510
		// (add) Token: 0x0600585A RID: 22618
		// (remove) Token: 0x0600585B RID: 22619
		public virtual extern event HTMLElementEvents2_oncutEventHandler HTMLElementEvents2_Event_oncut;

		// Token: 0x140009CF RID: 2511
		// (add) Token: 0x0600585C RID: 22620
		// (remove) Token: 0x0600585D RID: 22621
		public virtual extern event HTMLElementEvents2_onbeforecopyEventHandler HTMLElementEvents2_Event_onbeforecopy;

		// Token: 0x140009D0 RID: 2512
		// (add) Token: 0x0600585E RID: 22622
		// (remove) Token: 0x0600585F RID: 22623
		public virtual extern event HTMLElementEvents2_oncopyEventHandler HTMLElementEvents2_Event_oncopy;

		// Token: 0x140009D1 RID: 2513
		// (add) Token: 0x06005860 RID: 22624
		// (remove) Token: 0x06005861 RID: 22625
		public virtual extern event HTMLElementEvents2_onbeforepasteEventHandler HTMLElementEvents2_Event_onbeforepaste;

		// Token: 0x140009D2 RID: 2514
		// (add) Token: 0x06005862 RID: 22626
		// (remove) Token: 0x06005863 RID: 22627
		public virtual extern event HTMLElementEvents2_onpasteEventHandler HTMLElementEvents2_Event_onpaste;

		// Token: 0x140009D3 RID: 2515
		// (add) Token: 0x06005864 RID: 22628
		// (remove) Token: 0x06005865 RID: 22629
		public virtual extern event HTMLElementEvents2_oncontextmenuEventHandler HTMLElementEvents2_Event_oncontextmenu;

		// Token: 0x140009D4 RID: 2516
		// (add) Token: 0x06005866 RID: 22630
		// (remove) Token: 0x06005867 RID: 22631
		public virtual extern event HTMLElementEvents2_onrowsdeleteEventHandler HTMLElementEvents2_Event_onrowsdelete;

		// Token: 0x140009D5 RID: 2517
		// (add) Token: 0x06005868 RID: 22632
		// (remove) Token: 0x06005869 RID: 22633
		public virtual extern event HTMLElementEvents2_onrowsinsertedEventHandler HTMLElementEvents2_Event_onrowsinserted;

		// Token: 0x140009D6 RID: 2518
		// (add) Token: 0x0600586A RID: 22634
		// (remove) Token: 0x0600586B RID: 22635
		public virtual extern event HTMLElementEvents2_oncellchangeEventHandler HTMLElementEvents2_Event_oncellchange;

		// Token: 0x140009D7 RID: 2519
		// (add) Token: 0x0600586C RID: 22636
		// (remove) Token: 0x0600586D RID: 22637
		public virtual extern event HTMLElementEvents2_onreadystatechangeEventHandler HTMLElementEvents2_Event_onreadystatechange;

		// Token: 0x140009D8 RID: 2520
		// (add) Token: 0x0600586E RID: 22638
		// (remove) Token: 0x0600586F RID: 22639
		public virtual extern event HTMLElementEvents2_onlayoutcompleteEventHandler HTMLElementEvents2_Event_onlayoutcomplete;

		// Token: 0x140009D9 RID: 2521
		// (add) Token: 0x06005870 RID: 22640
		// (remove) Token: 0x06005871 RID: 22641
		public virtual extern event HTMLElementEvents2_onpageEventHandler HTMLElementEvents2_Event_onpage;

		// Token: 0x140009DA RID: 2522
		// (add) Token: 0x06005872 RID: 22642
		// (remove) Token: 0x06005873 RID: 22643
		public virtual extern event HTMLElementEvents2_onmouseenterEventHandler HTMLElementEvents2_Event_onmouseenter;

		// Token: 0x140009DB RID: 2523
		// (add) Token: 0x06005874 RID: 22644
		// (remove) Token: 0x06005875 RID: 22645
		public virtual extern event HTMLElementEvents2_onmouseleaveEventHandler HTMLElementEvents2_Event_onmouseleave;

		// Token: 0x140009DC RID: 2524
		// (add) Token: 0x06005876 RID: 22646
		// (remove) Token: 0x06005877 RID: 22647
		public virtual extern event HTMLElementEvents2_onactivateEventHandler HTMLElementEvents2_Event_onactivate;

		// Token: 0x140009DD RID: 2525
		// (add) Token: 0x06005878 RID: 22648
		// (remove) Token: 0x06005879 RID: 22649
		public virtual extern event HTMLElementEvents2_ondeactivateEventHandler HTMLElementEvents2_Event_ondeactivate;

		// Token: 0x140009DE RID: 2526
		// (add) Token: 0x0600587A RID: 22650
		// (remove) Token: 0x0600587B RID: 22651
		public virtual extern event HTMLElementEvents2_onbeforedeactivateEventHandler HTMLElementEvents2_Event_onbeforedeactivate;

		// Token: 0x140009DF RID: 2527
		// (add) Token: 0x0600587C RID: 22652
		// (remove) Token: 0x0600587D RID: 22653
		public virtual extern event HTMLElementEvents2_onbeforeactivateEventHandler HTMLElementEvents2_Event_onbeforeactivate;

		// Token: 0x140009E0 RID: 2528
		// (add) Token: 0x0600587E RID: 22654
		// (remove) Token: 0x0600587F RID: 22655
		public virtual extern event HTMLElementEvents2_onfocusinEventHandler HTMLElementEvents2_Event_onfocusin;

		// Token: 0x140009E1 RID: 2529
		// (add) Token: 0x06005880 RID: 22656
		// (remove) Token: 0x06005881 RID: 22657
		public virtual extern event HTMLElementEvents2_onfocusoutEventHandler HTMLElementEvents2_Event_onfocusout;

		// Token: 0x140009E2 RID: 2530
		// (add) Token: 0x06005882 RID: 22658
		// (remove) Token: 0x06005883 RID: 22659
		public virtual extern event HTMLElementEvents2_onmoveEventHandler HTMLElementEvents2_Event_onmove;

		// Token: 0x140009E3 RID: 2531
		// (add) Token: 0x06005884 RID: 22660
		// (remove) Token: 0x06005885 RID: 22661
		public virtual extern event HTMLElementEvents2_oncontrolselectEventHandler HTMLElementEvents2_Event_oncontrolselect;

		// Token: 0x140009E4 RID: 2532
		// (add) Token: 0x06005886 RID: 22662
		// (remove) Token: 0x06005887 RID: 22663
		public virtual extern event HTMLElementEvents2_onmovestartEventHandler HTMLElementEvents2_Event_onmovestart;

		// Token: 0x140009E5 RID: 2533
		// (add) Token: 0x06005888 RID: 22664
		// (remove) Token: 0x06005889 RID: 22665
		public virtual extern event HTMLElementEvents2_onmoveendEventHandler HTMLElementEvents2_Event_onmoveend;

		// Token: 0x140009E6 RID: 2534
		// (add) Token: 0x0600588A RID: 22666
		// (remove) Token: 0x0600588B RID: 22667
		public virtual extern event HTMLElementEvents2_onresizestartEventHandler HTMLElementEvents2_Event_onresizestart;

		// Token: 0x140009E7 RID: 2535
		// (add) Token: 0x0600588C RID: 22668
		// (remove) Token: 0x0600588D RID: 22669
		public virtual extern event HTMLElementEvents2_onresizeendEventHandler HTMLElementEvents2_Event_onresizeend;

		// Token: 0x140009E8 RID: 2536
		// (add) Token: 0x0600588E RID: 22670
		// (remove) Token: 0x0600588F RID: 22671
		public virtual extern event HTMLElementEvents2_onmousewheelEventHandler HTMLElementEvents2_Event_onmousewheel;
	}
}
