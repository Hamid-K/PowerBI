using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000699 RID: 1689
	[ClassInterface(0)]
	[ComSourceInterfaces("mshtml.HTMLInputTextElementEvents\0mshtml.HTMLInputTextElementEvents2\0\0")]
	[Guid("3050F2DF-98B5-11CF-BB82-00AA00BDCE0B")]
	[TypeLibType(2)]
	[ComImport]
	public class HTMLRichtextElementClass : DispHTMLRichtextElement, HTMLRichtextElement, HTMLInputTextElementEvents_Event, IHTMLElement, IHTMLElement2, IHTMLElement3, IHTMLElement4, IHTMLUniqueName, IHTMLDOMNode, IHTMLDOMNode2, IHTMLDatabinding, IHTMLControlElement, IHTMLTextContainer, IHTMLTextAreaElement, HTMLInputTextElementEvents2_Event
	{
		// Token: 0x06009F3C RID: 40764
		[MethodImpl(4096, MethodCodeType = 3)]
		public extern HTMLRichtextElementClass();

		// Token: 0x06009F3D RID: 40765
		[DispId(-2147417611)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void setAttribute([MarshalAs(19)] [In] string strAttributeName, [MarshalAs(27)] [In] object AttributeValue, [In] int lFlags = 1);

		// Token: 0x06009F3E RID: 40766
		[DispId(-2147417610)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		public virtual extern object getAttribute([MarshalAs(19)] [In] string strAttributeName, [In] int lFlags = 0);

		// Token: 0x06009F3F RID: 40767
		[DispId(-2147417609)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool removeAttribute([MarshalAs(19)] [In] string strAttributeName, [In] int lFlags = 1);

		// Token: 0x1700339D RID: 13213
		// (get) Token: 0x06009F41 RID: 40769
		// (set) Token: 0x06009F40 RID: 40768
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

		// Token: 0x1700339E RID: 13214
		// (get) Token: 0x06009F43 RID: 40771
		// (set) Token: 0x06009F42 RID: 40770
		[DispId(-2147417110)]
		public virtual extern string id
		{
			[TypeLibFunc(4)]
			[DispId(-2147417110)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(4)]
			[DispId(-2147417110)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x1700339F RID: 13215
		// (get) Token: 0x06009F44 RID: 40772
		[DispId(-2147417108)]
		public virtual extern string tagName
		{
			[DispId(-2147417108)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170033A0 RID: 13216
		// (get) Token: 0x06009F45 RID: 40773
		[DispId(-2147418104)]
		public virtual extern IHTMLElement parentElement
		{
			[DispId(-2147418104)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170033A1 RID: 13217
		// (get) Token: 0x06009F46 RID: 40774
		[DispId(-2147418038)]
		public virtual extern IHTMLStyle style
		{
			[DispId(-2147418038)]
			[TypeLibFunc(1024)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170033A2 RID: 13218
		// (get) Token: 0x06009F48 RID: 40776
		// (set) Token: 0x06009F47 RID: 40775
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

		// Token: 0x170033A3 RID: 13219
		// (get) Token: 0x06009F4A RID: 40778
		// (set) Token: 0x06009F49 RID: 40777
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

		// Token: 0x170033A4 RID: 13220
		// (get) Token: 0x06009F4C RID: 40780
		// (set) Token: 0x06009F4B RID: 40779
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

		// Token: 0x170033A5 RID: 13221
		// (get) Token: 0x06009F4E RID: 40782
		// (set) Token: 0x06009F4D RID: 40781
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

		// Token: 0x170033A6 RID: 13222
		// (get) Token: 0x06009F50 RID: 40784
		// (set) Token: 0x06009F4F RID: 40783
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

		// Token: 0x170033A7 RID: 13223
		// (get) Token: 0x06009F52 RID: 40786
		// (set) Token: 0x06009F51 RID: 40785
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

		// Token: 0x170033A8 RID: 13224
		// (get) Token: 0x06009F54 RID: 40788
		// (set) Token: 0x06009F53 RID: 40787
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

		// Token: 0x170033A9 RID: 13225
		// (get) Token: 0x06009F56 RID: 40790
		// (set) Token: 0x06009F55 RID: 40789
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

		// Token: 0x170033AA RID: 13226
		// (get) Token: 0x06009F58 RID: 40792
		// (set) Token: 0x06009F57 RID: 40791
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

		// Token: 0x170033AB RID: 13227
		// (get) Token: 0x06009F5A RID: 40794
		// (set) Token: 0x06009F59 RID: 40793
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

		// Token: 0x170033AC RID: 13228
		// (get) Token: 0x06009F5C RID: 40796
		// (set) Token: 0x06009F5B RID: 40795
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

		// Token: 0x170033AD RID: 13229
		// (get) Token: 0x06009F5D RID: 40797
		[DispId(-2147417094)]
		public virtual extern object document
		{
			[DispId(-2147417094)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x170033AE RID: 13230
		// (get) Token: 0x06009F5F RID: 40799
		// (set) Token: 0x06009F5E RID: 40798
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

		// Token: 0x170033AF RID: 13231
		// (get) Token: 0x06009F61 RID: 40801
		// (set) Token: 0x06009F60 RID: 40800
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

		// Token: 0x170033B0 RID: 13232
		// (get) Token: 0x06009F63 RID: 40803
		// (set) Token: 0x06009F62 RID: 40802
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

		// Token: 0x06009F64 RID: 40804
		[DispId(-2147417093)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void scrollIntoView([MarshalAs(27)] [In] [Optional] object varargStart);

		// Token: 0x06009F65 RID: 40805
		[DispId(-2147417092)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool contains([MarshalAs(28)] [In] IHTMLElement pChild);

		// Token: 0x170033B1 RID: 13233
		// (get) Token: 0x06009F66 RID: 40806
		[DispId(-2147417088)]
		public virtual extern int sourceIndex
		{
			[TypeLibFunc(4)]
			[DispId(-2147417088)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170033B2 RID: 13234
		// (get) Token: 0x06009F67 RID: 40807
		[DispId(-2147417087)]
		public virtual extern object recordNumber
		{
			[DispId(-2147417087)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x170033B3 RID: 13235
		// (get) Token: 0x06009F69 RID: 40809
		// (set) Token: 0x06009F68 RID: 40808
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

		// Token: 0x170033B4 RID: 13236
		// (get) Token: 0x06009F6A RID: 40810
		[DispId(-2147417104)]
		public virtual extern int offsetLeft
		{
			[DispId(-2147417104)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170033B5 RID: 13237
		// (get) Token: 0x06009F6B RID: 40811
		[DispId(-2147417103)]
		public virtual extern int offsetTop
		{
			[DispId(-2147417103)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170033B6 RID: 13238
		// (get) Token: 0x06009F6C RID: 40812
		[DispId(-2147417102)]
		public virtual extern int offsetWidth
		{
			[DispId(-2147417102)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170033B7 RID: 13239
		// (get) Token: 0x06009F6D RID: 40813
		[DispId(-2147417101)]
		public virtual extern int offsetHeight
		{
			[DispId(-2147417101)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170033B8 RID: 13240
		// (get) Token: 0x06009F6E RID: 40814
		[DispId(-2147417100)]
		public virtual extern IHTMLElement offsetParent
		{
			[DispId(-2147417100)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170033B9 RID: 13241
		// (get) Token: 0x06009F70 RID: 40816
		// (set) Token: 0x06009F6F RID: 40815
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

		// Token: 0x170033BA RID: 13242
		// (get) Token: 0x06009F72 RID: 40818
		// (set) Token: 0x06009F71 RID: 40817
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

		// Token: 0x170033BB RID: 13243
		// (get) Token: 0x06009F74 RID: 40820
		// (set) Token: 0x06009F73 RID: 40819
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

		// Token: 0x170033BC RID: 13244
		// (get) Token: 0x06009F76 RID: 40822
		// (set) Token: 0x06009F75 RID: 40821
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

		// Token: 0x06009F77 RID: 40823
		[DispId(-2147417082)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void insertAdjacentHTML([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string html);

		// Token: 0x06009F78 RID: 40824
		[DispId(-2147417081)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void insertAdjacentText([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string text);

		// Token: 0x170033BD RID: 13245
		// (get) Token: 0x06009F79 RID: 40825
		[DispId(-2147417080)]
		public virtual extern IHTMLElement parentTextEdit
		{
			[DispId(-2147417080)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170033BE RID: 13246
		// (get) Token: 0x06009F7A RID: 40826
		[DispId(-2147417078)]
		public virtual extern bool isTextEdit
		{
			[DispId(-2147417078)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x06009F7B RID: 40827
		[DispId(-2147417079)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void click();

		// Token: 0x170033BF RID: 13247
		// (get) Token: 0x06009F7C RID: 40828
		[DispId(-2147417077)]
		public virtual extern IHTMLFiltersCollection filters
		{
			[DispId(-2147417077)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170033C0 RID: 13248
		// (get) Token: 0x06009F7E RID: 40830
		// (set) Token: 0x06009F7D RID: 40829
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

		// Token: 0x06009F7F RID: 40831
		[DispId(-2147417076)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string toString();

		// Token: 0x170033C1 RID: 13249
		// (get) Token: 0x06009F81 RID: 40833
		// (set) Token: 0x06009F80 RID: 40832
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

		// Token: 0x170033C2 RID: 13250
		// (get) Token: 0x06009F83 RID: 40835
		// (set) Token: 0x06009F82 RID: 40834
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

		// Token: 0x170033C3 RID: 13251
		// (get) Token: 0x06009F85 RID: 40837
		// (set) Token: 0x06009F84 RID: 40836
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

		// Token: 0x170033C4 RID: 13252
		// (get) Token: 0x06009F87 RID: 40839
		// (set) Token: 0x06009F86 RID: 40838
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

		// Token: 0x170033C5 RID: 13253
		// (get) Token: 0x06009F89 RID: 40841
		// (set) Token: 0x06009F88 RID: 40840
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

		// Token: 0x170033C6 RID: 13254
		// (get) Token: 0x06009F8B RID: 40843
		// (set) Token: 0x06009F8A RID: 40842
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

		// Token: 0x170033C7 RID: 13255
		// (get) Token: 0x06009F8D RID: 40845
		// (set) Token: 0x06009F8C RID: 40844
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

		// Token: 0x170033C8 RID: 13256
		// (get) Token: 0x06009F8F RID: 40847
		// (set) Token: 0x06009F8E RID: 40846
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

		// Token: 0x170033C9 RID: 13257
		// (get) Token: 0x06009F91 RID: 40849
		// (set) Token: 0x06009F90 RID: 40848
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

		// Token: 0x170033CA RID: 13258
		// (get) Token: 0x06009F92 RID: 40850
		[DispId(-2147417075)]
		public virtual extern object children
		{
			[DispId(-2147417075)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x170033CB RID: 13259
		// (get) Token: 0x06009F93 RID: 40851
		[DispId(-2147417074)]
		public virtual extern object all
		{
			[DispId(-2147417074)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x170033CC RID: 13260
		// (get) Token: 0x06009F94 RID: 40852
		[DispId(-2147417073)]
		public virtual extern string scopeName
		{
			[DispId(-2147417073)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x06009F95 RID: 40853
		[DispId(-2147417072)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void setCapture([In] bool containerCapture = true);

		// Token: 0x06009F96 RID: 40854
		[DispId(-2147417071)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void releaseCapture();

		// Token: 0x170033CD RID: 13261
		// (get) Token: 0x06009F98 RID: 40856
		// (set) Token: 0x06009F97 RID: 40855
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

		// Token: 0x06009F99 RID: 40857
		[DispId(-2147417070)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string componentFromPoint([In] int x, [In] int y);

		// Token: 0x06009F9A RID: 40858
		[DispId(-2147417069)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void doScroll([MarshalAs(27)] [In] [Optional] object component);

		// Token: 0x170033CE RID: 13262
		// (get) Token: 0x06009F9C RID: 40860
		// (set) Token: 0x06009F9B RID: 40859
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

		// Token: 0x170033CF RID: 13263
		// (get) Token: 0x06009F9E RID: 40862
		// (set) Token: 0x06009F9D RID: 40861
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

		// Token: 0x170033D0 RID: 13264
		// (get) Token: 0x06009FA0 RID: 40864
		// (set) Token: 0x06009F9F RID: 40863
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

		// Token: 0x170033D1 RID: 13265
		// (get) Token: 0x06009FA2 RID: 40866
		// (set) Token: 0x06009FA1 RID: 40865
		[DispId(-2147412061)]
		public virtual extern object ondragenter
		{
			[TypeLibFunc(20)]
			[DispId(-2147412061)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412061)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170033D2 RID: 13266
		// (get) Token: 0x06009FA4 RID: 40868
		// (set) Token: 0x06009FA3 RID: 40867
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

		// Token: 0x170033D3 RID: 13267
		// (get) Token: 0x06009FA6 RID: 40870
		// (set) Token: 0x06009FA5 RID: 40869
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

		// Token: 0x170033D4 RID: 13268
		// (get) Token: 0x06009FA8 RID: 40872
		// (set) Token: 0x06009FA7 RID: 40871
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

		// Token: 0x170033D5 RID: 13269
		// (get) Token: 0x06009FAA RID: 40874
		// (set) Token: 0x06009FA9 RID: 40873
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

		// Token: 0x170033D6 RID: 13270
		// (get) Token: 0x06009FAC RID: 40876
		// (set) Token: 0x06009FAB RID: 40875
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

		// Token: 0x170033D7 RID: 13271
		// (get) Token: 0x06009FAE RID: 40878
		// (set) Token: 0x06009FAD RID: 40877
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

		// Token: 0x170033D8 RID: 13272
		// (get) Token: 0x06009FB0 RID: 40880
		// (set) Token: 0x06009FAF RID: 40879
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

		// Token: 0x170033D9 RID: 13273
		// (get) Token: 0x06009FB2 RID: 40882
		// (set) Token: 0x06009FB1 RID: 40881
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

		// Token: 0x170033DA RID: 13274
		// (get) Token: 0x06009FB4 RID: 40884
		// (set) Token: 0x06009FB3 RID: 40883
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

		// Token: 0x170033DB RID: 13275
		// (get) Token: 0x06009FB5 RID: 40885
		[DispId(-2147417105)]
		public virtual extern IHTMLCurrentStyle currentStyle
		{
			[TypeLibFunc(1024)]
			[DispId(-2147417105)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170033DC RID: 13276
		// (get) Token: 0x06009FB7 RID: 40887
		// (set) Token: 0x06009FB6 RID: 40886
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

		// Token: 0x06009FB8 RID: 40888
		[DispId(-2147417068)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLRectCollection getClientRects();

		// Token: 0x06009FB9 RID: 40889
		[DispId(-2147417067)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLRect getBoundingClientRect();

		// Token: 0x06009FBA RID: 40890
		[DispId(-2147417608)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void setExpression([MarshalAs(19)] [In] string propname, [MarshalAs(19)] [In] string expression, [MarshalAs(19)] [In] string language = "");

		// Token: 0x06009FBB RID: 40891
		[DispId(-2147417607)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		public virtual extern object getExpression([MarshalAs(19)] [In] string propname);

		// Token: 0x06009FBC RID: 40892
		[DispId(-2147417606)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool removeExpression([MarshalAs(19)] [In] string propname);

		// Token: 0x170033DD RID: 13277
		// (get) Token: 0x06009FBE RID: 40894
		// (set) Token: 0x06009FBD RID: 40893
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

		// Token: 0x06009FBF RID: 40895
		[DispId(-2147416112)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void focus();

		// Token: 0x170033DE RID: 13278
		// (get) Token: 0x06009FC1 RID: 40897
		// (set) Token: 0x06009FC0 RID: 40896
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

		// Token: 0x170033DF RID: 13279
		// (get) Token: 0x06009FC3 RID: 40899
		// (set) Token: 0x06009FC2 RID: 40898
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

		// Token: 0x170033E0 RID: 13280
		// (get) Token: 0x06009FC5 RID: 40901
		// (set) Token: 0x06009FC4 RID: 40900
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

		// Token: 0x170033E1 RID: 13281
		// (get) Token: 0x06009FC7 RID: 40903
		// (set) Token: 0x06009FC6 RID: 40902
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

		// Token: 0x06009FC8 RID: 40904
		[DispId(-2147416110)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void blur();

		// Token: 0x06009FC9 RID: 40905
		[DispId(-2147416095)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void addFilter([MarshalAs(25)] [In] object pUnk);

		// Token: 0x06009FCA RID: 40906
		[DispId(-2147416094)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void removeFilter([MarshalAs(25)] [In] object pUnk);

		// Token: 0x170033E2 RID: 13282
		// (get) Token: 0x06009FCB RID: 40907
		[DispId(-2147416093)]
		public virtual extern int clientHeight
		{
			[DispId(-2147416093)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170033E3 RID: 13283
		// (get) Token: 0x06009FCC RID: 40908
		[DispId(-2147416092)]
		public virtual extern int clientWidth
		{
			[TypeLibFunc(20)]
			[DispId(-2147416092)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170033E4 RID: 13284
		// (get) Token: 0x06009FCD RID: 40909
		[DispId(-2147416091)]
		public virtual extern int clientTop
		{
			[DispId(-2147416091)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170033E5 RID: 13285
		// (get) Token: 0x06009FCE RID: 40910
		[DispId(-2147416090)]
		public virtual extern int clientLeft
		{
			[TypeLibFunc(20)]
			[DispId(-2147416090)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x06009FCF RID: 40911
		[DispId(-2147417605)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool attachEvent([MarshalAs(19)] [In] string @event, [MarshalAs(26)] [In] object pdisp);

		// Token: 0x06009FD0 RID: 40912
		[DispId(-2147417604)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void detachEvent([MarshalAs(19)] [In] string @event, [MarshalAs(26)] [In] object pdisp);

		// Token: 0x170033E6 RID: 13286
		// (get) Token: 0x06009FD1 RID: 40913
		[DispId(-2147412996)]
		public virtual extern object readyState
		{
			[DispId(-2147412996)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x170033E7 RID: 13287
		// (get) Token: 0x06009FD3 RID: 40915
		// (set) Token: 0x06009FD2 RID: 40914
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

		// Token: 0x170033E8 RID: 13288
		// (get) Token: 0x06009FD5 RID: 40917
		// (set) Token: 0x06009FD4 RID: 40916
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

		// Token: 0x170033E9 RID: 13289
		// (get) Token: 0x06009FD7 RID: 40919
		// (set) Token: 0x06009FD6 RID: 40918
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

		// Token: 0x170033EA RID: 13290
		// (get) Token: 0x06009FD9 RID: 40921
		// (set) Token: 0x06009FD8 RID: 40920
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

		// Token: 0x170033EB RID: 13291
		// (get) Token: 0x06009FDB RID: 40923
		// (set) Token: 0x06009FDA RID: 40922
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

		// Token: 0x06009FDC RID: 40924
		[DispId(-2147417056)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(26)]
		public virtual extern object createControlRange();

		// Token: 0x170033EC RID: 13292
		// (get) Token: 0x06009FDD RID: 40925
		[DispId(-2147417055)]
		public virtual extern int scrollHeight
		{
			[DispId(-2147417055)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170033ED RID: 13293
		// (get) Token: 0x06009FDE RID: 40926
		[DispId(-2147417054)]
		public virtual extern int scrollWidth
		{
			[TypeLibFunc(20)]
			[DispId(-2147417054)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170033EE RID: 13294
		// (get) Token: 0x06009FE0 RID: 40928
		// (set) Token: 0x06009FDF RID: 40927
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

		// Token: 0x170033EF RID: 13295
		// (get) Token: 0x06009FE2 RID: 40930
		// (set) Token: 0x06009FE1 RID: 40929
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

		// Token: 0x06009FE3 RID: 40931
		[DispId(-2147417050)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void clearAttributes();

		// Token: 0x170033F0 RID: 13296
		// (get) Token: 0x06009FE5 RID: 40933
		// (set) Token: 0x06009FE4 RID: 40932
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

		// Token: 0x06009FE6 RID: 40934
		[DispId(-2147417043)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElement insertAdjacentElement([MarshalAs(19)] [In] string where, [MarshalAs(28)] [In] IHTMLElement insertedElement);

		// Token: 0x06009FE7 RID: 40935
		[DispId(-2147417047)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElement applyElement([MarshalAs(28)] [In] IHTMLElement apply, [MarshalAs(19)] [In] string where);

		// Token: 0x06009FE8 RID: 40936
		[DispId(-2147417042)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string getAdjacentText([MarshalAs(19)] [In] string where);

		// Token: 0x06009FE9 RID: 40937
		[DispId(-2147417041)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string replaceAdjacentText([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string newText);

		// Token: 0x170033F1 RID: 13297
		// (get) Token: 0x06009FEA RID: 40938
		[DispId(-2147417040)]
		public virtual extern bool canHaveChildren
		{
			[DispId(-2147417040)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x06009FEB RID: 40939
		[DispId(-2147417032)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern int addBehavior([MarshalAs(19)] [In] string bstrUrl, [MarshalAs(27)] [In] [Optional] ref object pvarFactory);

		// Token: 0x06009FEC RID: 40940
		[DispId(-2147417031)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool removeBehavior([In] int cookie);

		// Token: 0x170033F2 RID: 13298
		// (get) Token: 0x06009FED RID: 40941
		[DispId(-2147417048)]
		public virtual extern IHTMLStyle runtimeStyle
		{
			[TypeLibFunc(1024)]
			[DispId(-2147417048)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170033F3 RID: 13299
		// (get) Token: 0x06009FEE RID: 40942
		[DispId(-2147417030)]
		public virtual extern object behaviorUrns
		{
			[DispId(-2147417030)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x170033F4 RID: 13300
		// (get) Token: 0x06009FF0 RID: 40944
		// (set) Token: 0x06009FEF RID: 40943
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

		// Token: 0x170033F5 RID: 13301
		// (get) Token: 0x06009FF2 RID: 40946
		// (set) Token: 0x06009FF1 RID: 40945
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

		// Token: 0x170033F6 RID: 13302
		// (get) Token: 0x06009FF3 RID: 40947
		[DispId(-2147417028)]
		public virtual extern int readyStateValue
		{
			[TypeLibFunc(65)]
			[DispId(-2147417028)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x06009FF4 RID: 40948
		[DispId(-2147417027)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElementCollection getElementsByTagName([MarshalAs(19)] [In] string v);

		// Token: 0x06009FF5 RID: 40949
		[DispId(-2147417016)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void mergeAttributes([MarshalAs(28)] [In] IHTMLElement mergeThis, [MarshalAs(27)] [In] [Optional] ref object pvarFlags);

		// Token: 0x170033F7 RID: 13303
		// (get) Token: 0x06009FF6 RID: 40950
		[DispId(-2147417015)]
		public virtual extern bool isMultiLine
		{
			[DispId(-2147417015)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170033F8 RID: 13304
		// (get) Token: 0x06009FF7 RID: 40951
		[DispId(-2147417014)]
		public virtual extern bool canHaveHTML
		{
			[DispId(-2147417014)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170033F9 RID: 13305
		// (get) Token: 0x06009FF9 RID: 40953
		// (set) Token: 0x06009FF8 RID: 40952
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

		// Token: 0x170033FA RID: 13306
		// (get) Token: 0x06009FFB RID: 40955
		// (set) Token: 0x06009FFA RID: 40954
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

		// Token: 0x170033FB RID: 13307
		// (get) Token: 0x06009FFD RID: 40957
		// (set) Token: 0x06009FFC RID: 40956
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

		// Token: 0x170033FC RID: 13308
		// (get) Token: 0x06009FFF RID: 40959
		// (set) Token: 0x06009FFE RID: 40958
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

		// Token: 0x0600A000 RID: 40960
		[DispId(-2147417011)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void setActive();

		// Token: 0x170033FD RID: 13309
		// (get) Token: 0x0600A002 RID: 40962
		// (set) Token: 0x0600A001 RID: 40961
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

		// Token: 0x170033FE RID: 13310
		// (get) Token: 0x0600A003 RID: 40963
		[DispId(-2147417010)]
		public virtual extern bool isContentEditable
		{
			[DispId(-2147417010)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170033FF RID: 13311
		// (get) Token: 0x0600A005 RID: 40965
		// (set) Token: 0x0600A004 RID: 40964
		[DispId(-2147412949)]
		public virtual extern bool hideFocus
		{
			[TypeLibFunc(20)]
			[DispId(-2147412949)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412949)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x17003400 RID: 13312
		// (get) Token: 0x0600A007 RID: 40967
		// (set) Token: 0x0600A006 RID: 40966
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

		// Token: 0x17003401 RID: 13313
		// (get) Token: 0x0600A008 RID: 40968
		[DispId(-2147417007)]
		public virtual extern bool isDisabled
		{
			[DispId(-2147417007)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17003402 RID: 13314
		// (get) Token: 0x0600A00A RID: 40970
		// (set) Token: 0x0600A009 RID: 40969
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

		// Token: 0x17003403 RID: 13315
		// (get) Token: 0x0600A00C RID: 40972
		// (set) Token: 0x0600A00B RID: 40971
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

		// Token: 0x0600A00D RID: 40973
		[DispId(-2147417006)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool FireEvent([MarshalAs(19)] [In] string bstrEventName, [MarshalAs(27)] [In] [Optional] ref object pvarEventObject);

		// Token: 0x17003404 RID: 13316
		// (get) Token: 0x0600A00F RID: 40975
		// (set) Token: 0x0600A00E RID: 40974
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

		// Token: 0x17003405 RID: 13317
		// (get) Token: 0x0600A011 RID: 40977
		// (set) Token: 0x0600A010 RID: 40976
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

		// Token: 0x17003406 RID: 13318
		// (get) Token: 0x0600A013 RID: 40979
		// (set) Token: 0x0600A012 RID: 40978
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

		// Token: 0x17003407 RID: 13319
		// (get) Token: 0x0600A015 RID: 40981
		// (set) Token: 0x0600A014 RID: 40980
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

		// Token: 0x17003408 RID: 13320
		// (get) Token: 0x0600A017 RID: 40983
		// (set) Token: 0x0600A016 RID: 40982
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

		// Token: 0x17003409 RID: 13321
		// (get) Token: 0x0600A019 RID: 40985
		// (set) Token: 0x0600A018 RID: 40984
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

		// Token: 0x1700340A RID: 13322
		// (get) Token: 0x0600A01B RID: 40987
		// (set) Token: 0x0600A01A RID: 40986
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

		// Token: 0x1700340B RID: 13323
		// (get) Token: 0x0600A01D RID: 40989
		// (set) Token: 0x0600A01C RID: 40988
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

		// Token: 0x0600A01E RID: 40990
		[DispId(-2147417005)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool dragDrop();

		// Token: 0x1700340C RID: 13324
		// (get) Token: 0x0600A01F RID: 40991
		[DispId(-2147417004)]
		public virtual extern int glyphMode
		{
			[DispId(-2147417004)]
			[TypeLibFunc(1089)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700340D RID: 13325
		// (get) Token: 0x0600A021 RID: 40993
		// (set) Token: 0x0600A020 RID: 40992
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

		// Token: 0x0600A022 RID: 40994
		[DispId(-2147417000)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void normalize();

		// Token: 0x0600A023 RID: 40995
		[DispId(-2147417003)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute getAttributeNode([MarshalAs(19)] [In] string bstrName);

		// Token: 0x0600A024 RID: 40996
		[DispId(-2147417002)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute setAttributeNode([MarshalAs(28)] [In] IHTMLDOMAttribute pattr);

		// Token: 0x0600A025 RID: 40997
		[DispId(-2147417001)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute removeAttributeNode([MarshalAs(28)] [In] IHTMLDOMAttribute pattr);

		// Token: 0x1700340E RID: 13326
		// (get) Token: 0x0600A027 RID: 40999
		// (set) Token: 0x0600A026 RID: 40998
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

		// Token: 0x1700340F RID: 13327
		// (get) Token: 0x0600A029 RID: 41001
		// (set) Token: 0x0600A028 RID: 41000
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

		// Token: 0x17003410 RID: 13328
		// (get) Token: 0x0600A02B RID: 41003
		// (set) Token: 0x0600A02A RID: 41002
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

		// Token: 0x17003411 RID: 13329
		// (get) Token: 0x0600A02C RID: 41004
		[DispId(-2147417058)]
		public virtual extern int uniqueNumber
		{
			[TypeLibFunc(64)]
			[DispId(-2147417058)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17003412 RID: 13330
		// (get) Token: 0x0600A02D RID: 41005
		[DispId(-2147417057)]
		public virtual extern string uniqueID
		{
			[DispId(-2147417057)]
			[TypeLibFunc(64)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17003413 RID: 13331
		// (get) Token: 0x0600A02E RID: 41006
		[DispId(-2147417066)]
		public virtual extern int nodeType
		{
			[DispId(-2147417066)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17003414 RID: 13332
		// (get) Token: 0x0600A02F RID: 41007
		[DispId(-2147417065)]
		public virtual extern IHTMLDOMNode parentNode
		{
			[DispId(-2147417065)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x0600A030 RID: 41008
		[DispId(-2147417064)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool hasChildNodes();

		// Token: 0x17003415 RID: 13333
		// (get) Token: 0x0600A031 RID: 41009
		[DispId(-2147417063)]
		public virtual extern object childNodes
		{
			[DispId(-2147417063)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17003416 RID: 13334
		// (get) Token: 0x0600A032 RID: 41010
		[DispId(-2147417062)]
		public virtual extern object attributes
		{
			[DispId(-2147417062)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x0600A033 RID: 41011
		[DispId(-2147417061)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode insertBefore([MarshalAs(28)] [In] IHTMLDOMNode newChild, [MarshalAs(27)] [In] [Optional] object refChild);

		// Token: 0x0600A034 RID: 41012
		[DispId(-2147417060)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode removeChild([MarshalAs(28)] [In] IHTMLDOMNode oldChild);

		// Token: 0x0600A035 RID: 41013
		[DispId(-2147417059)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode replaceChild([MarshalAs(28)] [In] IHTMLDOMNode newChild, [MarshalAs(28)] [In] IHTMLDOMNode oldChild);

		// Token: 0x0600A036 RID: 41014
		[DispId(-2147417051)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode cloneNode([In] bool fDeep);

		// Token: 0x0600A037 RID: 41015
		[DispId(-2147417046)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode removeNode([In] bool fDeep = false);

		// Token: 0x0600A038 RID: 41016
		[DispId(-2147417044)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode swapNode([MarshalAs(28)] [In] IHTMLDOMNode otherNode);

		// Token: 0x0600A039 RID: 41017
		[DispId(-2147417045)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode replaceNode([MarshalAs(28)] [In] IHTMLDOMNode replacement);

		// Token: 0x0600A03A RID: 41018
		[DispId(-2147417039)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode appendChild([MarshalAs(28)] [In] IHTMLDOMNode newChild);

		// Token: 0x17003417 RID: 13335
		// (get) Token: 0x0600A03B RID: 41019
		[DispId(-2147417038)]
		public virtual extern string nodeName
		{
			[DispId(-2147417038)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17003418 RID: 13336
		// (get) Token: 0x0600A03D RID: 41021
		// (set) Token: 0x0600A03C RID: 41020
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

		// Token: 0x17003419 RID: 13337
		// (get) Token: 0x0600A03E RID: 41022
		[DispId(-2147417036)]
		public virtual extern IHTMLDOMNode firstChild
		{
			[DispId(-2147417036)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x1700341A RID: 13338
		// (get) Token: 0x0600A03F RID: 41023
		[DispId(-2147417035)]
		public virtual extern IHTMLDOMNode lastChild
		{
			[DispId(-2147417035)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x1700341B RID: 13339
		// (get) Token: 0x0600A040 RID: 41024
		[DispId(-2147417034)]
		public virtual extern IHTMLDOMNode previousSibling
		{
			[DispId(-2147417034)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x1700341C RID: 13340
		// (get) Token: 0x0600A041 RID: 41025
		[DispId(-2147417033)]
		public virtual extern IHTMLDOMNode nextSibling
		{
			[DispId(-2147417033)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x1700341D RID: 13341
		// (get) Token: 0x0600A042 RID: 41026
		[DispId(-2147416999)]
		public virtual extern object ownerDocument
		{
			[DispId(-2147416999)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x1700341E RID: 13342
		// (get) Token: 0x0600A044 RID: 41028
		// (set) Token: 0x0600A043 RID: 41027
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

		// Token: 0x1700341F RID: 13343
		// (get) Token: 0x0600A046 RID: 41030
		// (set) Token: 0x0600A045 RID: 41029
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

		// Token: 0x17003420 RID: 13344
		// (get) Token: 0x0600A048 RID: 41032
		// (set) Token: 0x0600A047 RID: 41031
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

		// Token: 0x17003421 RID: 13345
		// (get) Token: 0x0600A049 RID: 41033
		[DispId(2000)]
		public virtual extern string type
		{
			[DispId(2000)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17003422 RID: 13346
		// (get) Token: 0x0600A04B RID: 41035
		// (set) Token: 0x0600A04A RID: 41034
		[DispId(-2147413011)]
		public virtual extern string value
		{
			[DispId(-2147413011)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147413011)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17003423 RID: 13347
		// (get) Token: 0x0600A04D RID: 41037
		// (set) Token: 0x0600A04C RID: 41036
		[DispId(-2147418112)]
		public virtual extern string name
		{
			[TypeLibFunc(20)]
			[DispId(-2147418112)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147418112)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17003424 RID: 13348
		// (get) Token: 0x0600A04F RID: 41039
		// (set) Token: 0x0600A04E RID: 41038
		[DispId(2001)]
		public virtual extern object status
		{
			[DispId(2001)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(2001)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17003425 RID: 13349
		// (get) Token: 0x0600A050 RID: 41040
		[DispId(-2147416108)]
		public virtual extern IHTMLFormElement form
		{
			[DispId(-2147416108)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17003426 RID: 13350
		// (get) Token: 0x0600A052 RID: 41042
		// (set) Token: 0x0600A051 RID: 41041
		[DispId(-2147413029)]
		public virtual extern string defaultValue
		{
			[DispId(-2147413029)]
			[TypeLibFunc(84)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(84)]
			[DispId(-2147413029)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x0600A053 RID: 41043
		[DispId(7005)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void select();

		// Token: 0x17003427 RID: 13351
		// (get) Token: 0x0600A055 RID: 41045
		// (set) Token: 0x0600A054 RID: 41044
		[DispId(-2147412082)]
		public virtual extern object onchange
		{
			[DispId(-2147412082)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412082)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17003428 RID: 13352
		// (get) Token: 0x0600A057 RID: 41047
		// (set) Token: 0x0600A056 RID: 41046
		[DispId(-2147412102)]
		public virtual extern object onselect
		{
			[DispId(-2147412102)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412102)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17003429 RID: 13353
		// (get) Token: 0x0600A059 RID: 41049
		// (set) Token: 0x0600A058 RID: 41048
		[DispId(7004)]
		public virtual extern bool readOnly
		{
			[TypeLibFunc(20)]
			[DispId(7004)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[DispId(7004)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x1700342A RID: 13354
		// (get) Token: 0x0600A05B RID: 41051
		// (set) Token: 0x0600A05A RID: 41050
		[DispId(7001)]
		public virtual extern int rows
		{
			[TypeLibFunc(20)]
			[DispId(7001)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[DispId(7001)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x1700342B RID: 13355
		// (get) Token: 0x0600A05D RID: 41053
		// (set) Token: 0x0600A05C RID: 41052
		[DispId(7002)]
		public virtual extern int cols
		{
			[TypeLibFunc(20)]
			[DispId(7002)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[DispId(7002)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x1700342C RID: 13356
		// (get) Token: 0x0600A05F RID: 41055
		// (set) Token: 0x0600A05E RID: 41054
		[DispId(7003)]
		public virtual extern string wrap
		{
			[TypeLibFunc(20)]
			[DispId(7003)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(7003)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x0600A060 RID: 41056
		[DispId(7006)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLTxtRange createTextRange();

		// Token: 0x0600A061 RID: 41057
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement_setAttribute([MarshalAs(19)] [In] string strAttributeName, [MarshalAs(27)] [In] object AttributeValue, [In] int lFlags = 1);

		// Token: 0x0600A062 RID: 41058
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		public virtual extern object IHTMLElement_getAttribute([MarshalAs(19)] [In] string strAttributeName, [In] int lFlags = 0);

		// Token: 0x0600A063 RID: 41059
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement_removeAttribute([MarshalAs(19)] [In] string strAttributeName, [In] int lFlags = 1);

		// Token: 0x1700342D RID: 13357
		// (get) Token: 0x0600A065 RID: 41061
		// (set) Token: 0x0600A064 RID: 41060
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

		// Token: 0x1700342E RID: 13358
		// (get) Token: 0x0600A067 RID: 41063
		// (set) Token: 0x0600A066 RID: 41062
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

		// Token: 0x1700342F RID: 13359
		// (get) Token: 0x0600A068 RID: 41064
		public virtual extern string IHTMLElement_tagName
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17003430 RID: 13360
		// (get) Token: 0x0600A069 RID: 41065
		public virtual extern IHTMLElement IHTMLElement_parentElement
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17003431 RID: 13361
		// (get) Token: 0x0600A06A RID: 41066
		public virtual extern IHTMLStyle IHTMLElement_style
		{
			[TypeLibFunc(1024)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17003432 RID: 13362
		// (get) Token: 0x0600A06C RID: 41068
		// (set) Token: 0x0600A06B RID: 41067
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

		// Token: 0x17003433 RID: 13363
		// (get) Token: 0x0600A06E RID: 41070
		// (set) Token: 0x0600A06D RID: 41069
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

		// Token: 0x17003434 RID: 13364
		// (get) Token: 0x0600A070 RID: 41072
		// (set) Token: 0x0600A06F RID: 41071
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

		// Token: 0x17003435 RID: 13365
		// (get) Token: 0x0600A072 RID: 41074
		// (set) Token: 0x0600A071 RID: 41073
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

		// Token: 0x17003436 RID: 13366
		// (get) Token: 0x0600A074 RID: 41076
		// (set) Token: 0x0600A073 RID: 41075
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

		// Token: 0x17003437 RID: 13367
		// (get) Token: 0x0600A076 RID: 41078
		// (set) Token: 0x0600A075 RID: 41077
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

		// Token: 0x17003438 RID: 13368
		// (get) Token: 0x0600A078 RID: 41080
		// (set) Token: 0x0600A077 RID: 41079
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

		// Token: 0x17003439 RID: 13369
		// (get) Token: 0x0600A07A RID: 41082
		// (set) Token: 0x0600A079 RID: 41081
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

		// Token: 0x1700343A RID: 13370
		// (get) Token: 0x0600A07C RID: 41084
		// (set) Token: 0x0600A07B RID: 41083
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

		// Token: 0x1700343B RID: 13371
		// (get) Token: 0x0600A07E RID: 41086
		// (set) Token: 0x0600A07D RID: 41085
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

		// Token: 0x1700343C RID: 13372
		// (get) Token: 0x0600A080 RID: 41088
		// (set) Token: 0x0600A07F RID: 41087
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

		// Token: 0x1700343D RID: 13373
		// (get) Token: 0x0600A081 RID: 41089
		public virtual extern object IHTMLElement_document
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x1700343E RID: 13374
		// (get) Token: 0x0600A083 RID: 41091
		// (set) Token: 0x0600A082 RID: 41090
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

		// Token: 0x1700343F RID: 13375
		// (get) Token: 0x0600A085 RID: 41093
		// (set) Token: 0x0600A084 RID: 41092
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

		// Token: 0x17003440 RID: 13376
		// (get) Token: 0x0600A087 RID: 41095
		// (set) Token: 0x0600A086 RID: 41094
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

		// Token: 0x0600A088 RID: 41096
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement_scrollIntoView([MarshalAs(27)] [In] [Optional] object varargStart);

		// Token: 0x0600A089 RID: 41097
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement_contains([MarshalAs(28)] [In] IHTMLElement pChild);

		// Token: 0x17003441 RID: 13377
		// (get) Token: 0x0600A08A RID: 41098
		public virtual extern int IHTMLElement_sourceIndex
		{
			[TypeLibFunc(4)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17003442 RID: 13378
		// (get) Token: 0x0600A08B RID: 41099
		public virtual extern object IHTMLElement_recordNumber
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x17003443 RID: 13379
		// (get) Token: 0x0600A08D RID: 41101
		// (set) Token: 0x0600A08C RID: 41100
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

		// Token: 0x17003444 RID: 13380
		// (get) Token: 0x0600A08E RID: 41102
		public virtual extern int IHTMLElement_offsetLeft
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17003445 RID: 13381
		// (get) Token: 0x0600A08F RID: 41103
		public virtual extern int IHTMLElement_offsetTop
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17003446 RID: 13382
		// (get) Token: 0x0600A090 RID: 41104
		public virtual extern int IHTMLElement_offsetWidth
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17003447 RID: 13383
		// (get) Token: 0x0600A091 RID: 41105
		public virtual extern int IHTMLElement_offsetHeight
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17003448 RID: 13384
		// (get) Token: 0x0600A092 RID: 41106
		public virtual extern IHTMLElement IHTMLElement_offsetParent
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17003449 RID: 13385
		// (get) Token: 0x0600A094 RID: 41108
		// (set) Token: 0x0600A093 RID: 41107
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

		// Token: 0x1700344A RID: 13386
		// (get) Token: 0x0600A096 RID: 41110
		// (set) Token: 0x0600A095 RID: 41109
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

		// Token: 0x1700344B RID: 13387
		// (get) Token: 0x0600A098 RID: 41112
		// (set) Token: 0x0600A097 RID: 41111
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

		// Token: 0x1700344C RID: 13388
		// (get) Token: 0x0600A09A RID: 41114
		// (set) Token: 0x0600A099 RID: 41113
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

		// Token: 0x0600A09B RID: 41115
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement_insertAdjacentHTML([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string html);

		// Token: 0x0600A09C RID: 41116
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement_insertAdjacentText([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string text);

		// Token: 0x1700344D RID: 13389
		// (get) Token: 0x0600A09D RID: 41117
		public virtual extern IHTMLElement IHTMLElement_parentTextEdit
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x1700344E RID: 13390
		// (get) Token: 0x0600A09E RID: 41118
		public virtual extern bool IHTMLElement_isTextEdit
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x0600A09F RID: 41119
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement_click();

		// Token: 0x1700344F RID: 13391
		// (get) Token: 0x0600A0A0 RID: 41120
		public virtual extern IHTMLFiltersCollection IHTMLElement_filters
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17003450 RID: 13392
		// (get) Token: 0x0600A0A2 RID: 41122
		// (set) Token: 0x0600A0A1 RID: 41121
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

		// Token: 0x0600A0A3 RID: 41123
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string IHTMLElement_toString();

		// Token: 0x17003451 RID: 13393
		// (get) Token: 0x0600A0A5 RID: 41125
		// (set) Token: 0x0600A0A4 RID: 41124
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

		// Token: 0x17003452 RID: 13394
		// (get) Token: 0x0600A0A7 RID: 41127
		// (set) Token: 0x0600A0A6 RID: 41126
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

		// Token: 0x17003453 RID: 13395
		// (get) Token: 0x0600A0A9 RID: 41129
		// (set) Token: 0x0600A0A8 RID: 41128
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

		// Token: 0x17003454 RID: 13396
		// (get) Token: 0x0600A0AB RID: 41131
		// (set) Token: 0x0600A0AA RID: 41130
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

		// Token: 0x17003455 RID: 13397
		// (get) Token: 0x0600A0AD RID: 41133
		// (set) Token: 0x0600A0AC RID: 41132
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

		// Token: 0x17003456 RID: 13398
		// (get) Token: 0x0600A0AF RID: 41135
		// (set) Token: 0x0600A0AE RID: 41134
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

		// Token: 0x17003457 RID: 13399
		// (get) Token: 0x0600A0B1 RID: 41137
		// (set) Token: 0x0600A0B0 RID: 41136
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

		// Token: 0x17003458 RID: 13400
		// (get) Token: 0x0600A0B3 RID: 41139
		// (set) Token: 0x0600A0B2 RID: 41138
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

		// Token: 0x17003459 RID: 13401
		// (get) Token: 0x0600A0B5 RID: 41141
		// (set) Token: 0x0600A0B4 RID: 41140
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

		// Token: 0x1700345A RID: 13402
		// (get) Token: 0x0600A0B6 RID: 41142
		public virtual extern object IHTMLElement_children
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x1700345B RID: 13403
		// (get) Token: 0x0600A0B7 RID: 41143
		public virtual extern object IHTMLElement_all
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x1700345C RID: 13404
		// (get) Token: 0x0600A0B8 RID: 41144
		public virtual extern string IHTMLElement2_scopeName
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x0600A0B9 RID: 41145
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_setCapture([In] bool containerCapture = true);

		// Token: 0x0600A0BA RID: 41146
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_releaseCapture();

		// Token: 0x1700345D RID: 13405
		// (get) Token: 0x0600A0BC RID: 41148
		// (set) Token: 0x0600A0BB RID: 41147
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

		// Token: 0x0600A0BD RID: 41149
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string IHTMLElement2_componentFromPoint([In] int x, [In] int y);

		// Token: 0x0600A0BE RID: 41150
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_doScroll([MarshalAs(27)] [In] [Optional] object component);

		// Token: 0x1700345E RID: 13406
		// (get) Token: 0x0600A0C0 RID: 41152
		// (set) Token: 0x0600A0BF RID: 41151
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

		// Token: 0x1700345F RID: 13407
		// (get) Token: 0x0600A0C2 RID: 41154
		// (set) Token: 0x0600A0C1 RID: 41153
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

		// Token: 0x17003460 RID: 13408
		// (get) Token: 0x0600A0C4 RID: 41156
		// (set) Token: 0x0600A0C3 RID: 41155
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

		// Token: 0x17003461 RID: 13409
		// (get) Token: 0x0600A0C6 RID: 41158
		// (set) Token: 0x0600A0C5 RID: 41157
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

		// Token: 0x17003462 RID: 13410
		// (get) Token: 0x0600A0C8 RID: 41160
		// (set) Token: 0x0600A0C7 RID: 41159
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

		// Token: 0x17003463 RID: 13411
		// (get) Token: 0x0600A0CA RID: 41162
		// (set) Token: 0x0600A0C9 RID: 41161
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

		// Token: 0x17003464 RID: 13412
		// (get) Token: 0x0600A0CC RID: 41164
		// (set) Token: 0x0600A0CB RID: 41163
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

		// Token: 0x17003465 RID: 13413
		// (get) Token: 0x0600A0CE RID: 41166
		// (set) Token: 0x0600A0CD RID: 41165
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

		// Token: 0x17003466 RID: 13414
		// (get) Token: 0x0600A0D0 RID: 41168
		// (set) Token: 0x0600A0CF RID: 41167
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

		// Token: 0x17003467 RID: 13415
		// (get) Token: 0x0600A0D2 RID: 41170
		// (set) Token: 0x0600A0D1 RID: 41169
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

		// Token: 0x17003468 RID: 13416
		// (get) Token: 0x0600A0D4 RID: 41172
		// (set) Token: 0x0600A0D3 RID: 41171
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

		// Token: 0x17003469 RID: 13417
		// (get) Token: 0x0600A0D6 RID: 41174
		// (set) Token: 0x0600A0D5 RID: 41173
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

		// Token: 0x1700346A RID: 13418
		// (get) Token: 0x0600A0D8 RID: 41176
		// (set) Token: 0x0600A0D7 RID: 41175
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

		// Token: 0x1700346B RID: 13419
		// (get) Token: 0x0600A0D9 RID: 41177
		public virtual extern IHTMLCurrentStyle IHTMLElement2_currentStyle
		{
			[TypeLibFunc(1024)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x1700346C RID: 13420
		// (get) Token: 0x0600A0DB RID: 41179
		// (set) Token: 0x0600A0DA RID: 41178
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

		// Token: 0x0600A0DC RID: 41180
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLRectCollection IHTMLElement2_getClientRects();

		// Token: 0x0600A0DD RID: 41181
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLRect IHTMLElement2_getBoundingClientRect();

		// Token: 0x0600A0DE RID: 41182
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_setExpression([MarshalAs(19)] [In] string propname, [MarshalAs(19)] [In] string expression, [MarshalAs(19)] [In] string language = "");

		// Token: 0x0600A0DF RID: 41183
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		public virtual extern object IHTMLElement2_getExpression([MarshalAs(19)] [In] string propname);

		// Token: 0x0600A0E0 RID: 41184
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement2_removeExpression([MarshalAs(19)] [In] string propname);

		// Token: 0x1700346D RID: 13421
		// (get) Token: 0x0600A0E2 RID: 41186
		// (set) Token: 0x0600A0E1 RID: 41185
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

		// Token: 0x0600A0E3 RID: 41187
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_focus();

		// Token: 0x1700346E RID: 13422
		// (get) Token: 0x0600A0E5 RID: 41189
		// (set) Token: 0x0600A0E4 RID: 41188
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

		// Token: 0x1700346F RID: 13423
		// (get) Token: 0x0600A0E7 RID: 41191
		// (set) Token: 0x0600A0E6 RID: 41190
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

		// Token: 0x17003470 RID: 13424
		// (get) Token: 0x0600A0E9 RID: 41193
		// (set) Token: 0x0600A0E8 RID: 41192
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

		// Token: 0x17003471 RID: 13425
		// (get) Token: 0x0600A0EB RID: 41195
		// (set) Token: 0x0600A0EA RID: 41194
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

		// Token: 0x0600A0EC RID: 41196
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_blur();

		// Token: 0x0600A0ED RID: 41197
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_addFilter([MarshalAs(25)] [In] object pUnk);

		// Token: 0x0600A0EE RID: 41198
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_removeFilter([MarshalAs(25)] [In] object pUnk);

		// Token: 0x17003472 RID: 13426
		// (get) Token: 0x0600A0EF RID: 41199
		public virtual extern int IHTMLElement2_clientHeight
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17003473 RID: 13427
		// (get) Token: 0x0600A0F0 RID: 41200
		public virtual extern int IHTMLElement2_clientWidth
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17003474 RID: 13428
		// (get) Token: 0x0600A0F1 RID: 41201
		public virtual extern int IHTMLElement2_clientTop
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17003475 RID: 13429
		// (get) Token: 0x0600A0F2 RID: 41202
		public virtual extern int IHTMLElement2_clientLeft
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x0600A0F3 RID: 41203
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement2_attachEvent([MarshalAs(19)] [In] string @event, [MarshalAs(26)] [In] object pdisp);

		// Token: 0x0600A0F4 RID: 41204
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_detachEvent([MarshalAs(19)] [In] string @event, [MarshalAs(26)] [In] object pdisp);

		// Token: 0x17003476 RID: 13430
		// (get) Token: 0x0600A0F5 RID: 41205
		public virtual extern object IHTMLElement2_readyState
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x17003477 RID: 13431
		// (get) Token: 0x0600A0F7 RID: 41207
		// (set) Token: 0x0600A0F6 RID: 41206
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

		// Token: 0x17003478 RID: 13432
		// (get) Token: 0x0600A0F9 RID: 41209
		// (set) Token: 0x0600A0F8 RID: 41208
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

		// Token: 0x17003479 RID: 13433
		// (get) Token: 0x0600A0FB RID: 41211
		// (set) Token: 0x0600A0FA RID: 41210
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

		// Token: 0x1700347A RID: 13434
		// (get) Token: 0x0600A0FD RID: 41213
		// (set) Token: 0x0600A0FC RID: 41212
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

		// Token: 0x1700347B RID: 13435
		// (get) Token: 0x0600A0FF RID: 41215
		// (set) Token: 0x0600A0FE RID: 41214
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

		// Token: 0x0600A100 RID: 41216
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(26)]
		public virtual extern object IHTMLElement2_createControlRange();

		// Token: 0x1700347C RID: 13436
		// (get) Token: 0x0600A101 RID: 41217
		public virtual extern int IHTMLElement2_scrollHeight
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700347D RID: 13437
		// (get) Token: 0x0600A102 RID: 41218
		public virtual extern int IHTMLElement2_scrollWidth
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700347E RID: 13438
		// (get) Token: 0x0600A104 RID: 41220
		// (set) Token: 0x0600A103 RID: 41219
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

		// Token: 0x1700347F RID: 13439
		// (get) Token: 0x0600A106 RID: 41222
		// (set) Token: 0x0600A105 RID: 41221
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

		// Token: 0x0600A107 RID: 41223
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_clearAttributes();

		// Token: 0x0600A108 RID: 41224
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void mergeAttributes([MarshalAs(28)] [In] IHTMLElement mergeThis);

		// Token: 0x17003480 RID: 13440
		// (get) Token: 0x0600A10A RID: 41226
		// (set) Token: 0x0600A109 RID: 41225
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

		// Token: 0x0600A10B RID: 41227
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElement IHTMLElement2_insertAdjacentElement([MarshalAs(19)] [In] string where, [MarshalAs(28)] [In] IHTMLElement insertedElement);

		// Token: 0x0600A10C RID: 41228
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElement IHTMLElement2_applyElement([MarshalAs(28)] [In] IHTMLElement apply, [MarshalAs(19)] [In] string where);

		// Token: 0x0600A10D RID: 41229
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string IHTMLElement2_getAdjacentText([MarshalAs(19)] [In] string where);

		// Token: 0x0600A10E RID: 41230
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string IHTMLElement2_replaceAdjacentText([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string newText);

		// Token: 0x17003481 RID: 13441
		// (get) Token: 0x0600A10F RID: 41231
		public virtual extern bool IHTMLElement2_canHaveChildren
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x0600A110 RID: 41232
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern int IHTMLElement2_addBehavior([MarshalAs(19)] [In] string bstrUrl, [MarshalAs(27)] [In] [Optional] ref object pvarFactory);

		// Token: 0x0600A111 RID: 41233
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement2_removeBehavior([In] int cookie);

		// Token: 0x17003482 RID: 13442
		// (get) Token: 0x0600A112 RID: 41234
		public virtual extern IHTMLStyle IHTMLElement2_runtimeStyle
		{
			[TypeLibFunc(1024)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17003483 RID: 13443
		// (get) Token: 0x0600A113 RID: 41235
		public virtual extern object IHTMLElement2_behaviorUrns
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17003484 RID: 13444
		// (get) Token: 0x0600A115 RID: 41237
		// (set) Token: 0x0600A114 RID: 41236
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

		// Token: 0x17003485 RID: 13445
		// (get) Token: 0x0600A117 RID: 41239
		// (set) Token: 0x0600A116 RID: 41238
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

		// Token: 0x17003486 RID: 13446
		// (get) Token: 0x0600A118 RID: 41240
		public virtual extern int IHTMLElement2_readyStateValue
		{
			[TypeLibFunc(65)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x0600A119 RID: 41241
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElementCollection IHTMLElement2_getElementsByTagName([MarshalAs(19)] [In] string v);

		// Token: 0x0600A11A RID: 41242
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void mergeAttributes([MarshalAs(28)] [In] IHTMLElement mergeThis, [MarshalAs(27)] [In] [Optional] ref object pvarFlags);

		// Token: 0x17003487 RID: 13447
		// (get) Token: 0x0600A11B RID: 41243
		public virtual extern bool IHTMLElement3_isMultiLine
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17003488 RID: 13448
		// (get) Token: 0x0600A11C RID: 41244
		public virtual extern bool IHTMLElement3_canHaveHTML
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17003489 RID: 13449
		// (get) Token: 0x0600A11E RID: 41246
		// (set) Token: 0x0600A11D RID: 41245
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

		// Token: 0x1700348A RID: 13450
		// (get) Token: 0x0600A120 RID: 41248
		// (set) Token: 0x0600A11F RID: 41247
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

		// Token: 0x1700348B RID: 13451
		// (get) Token: 0x0600A122 RID: 41250
		// (set) Token: 0x0600A121 RID: 41249
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

		// Token: 0x1700348C RID: 13452
		// (get) Token: 0x0600A124 RID: 41252
		// (set) Token: 0x0600A123 RID: 41251
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

		// Token: 0x0600A125 RID: 41253
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement3_setActive();

		// Token: 0x1700348D RID: 13453
		// (get) Token: 0x0600A127 RID: 41255
		// (set) Token: 0x0600A126 RID: 41254
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

		// Token: 0x1700348E RID: 13454
		// (get) Token: 0x0600A128 RID: 41256
		public virtual extern bool IHTMLElement3_isContentEditable
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700348F RID: 13455
		// (get) Token: 0x0600A12A RID: 41258
		// (set) Token: 0x0600A129 RID: 41257
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

		// Token: 0x17003490 RID: 13456
		// (get) Token: 0x0600A12C RID: 41260
		// (set) Token: 0x0600A12B RID: 41259
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

		// Token: 0x17003491 RID: 13457
		// (get) Token: 0x0600A12D RID: 41261
		public virtual extern bool IHTMLElement3_isDisabled
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17003492 RID: 13458
		// (get) Token: 0x0600A12F RID: 41263
		// (set) Token: 0x0600A12E RID: 41262
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

		// Token: 0x17003493 RID: 13459
		// (get) Token: 0x0600A131 RID: 41265
		// (set) Token: 0x0600A130 RID: 41264
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

		// Token: 0x0600A132 RID: 41266
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement3_FireEvent([MarshalAs(19)] [In] string bstrEventName, [MarshalAs(27)] [In] [Optional] ref object pvarEventObject);

		// Token: 0x17003494 RID: 13460
		// (get) Token: 0x0600A134 RID: 41268
		// (set) Token: 0x0600A133 RID: 41267
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

		// Token: 0x17003495 RID: 13461
		// (get) Token: 0x0600A136 RID: 41270
		// (set) Token: 0x0600A135 RID: 41269
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

		// Token: 0x17003496 RID: 13462
		// (get) Token: 0x0600A138 RID: 41272
		// (set) Token: 0x0600A137 RID: 41271
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

		// Token: 0x17003497 RID: 13463
		// (get) Token: 0x0600A13A RID: 41274
		// (set) Token: 0x0600A139 RID: 41273
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

		// Token: 0x17003498 RID: 13464
		// (get) Token: 0x0600A13C RID: 41276
		// (set) Token: 0x0600A13B RID: 41275
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

		// Token: 0x17003499 RID: 13465
		// (get) Token: 0x0600A13E RID: 41278
		// (set) Token: 0x0600A13D RID: 41277
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

		// Token: 0x1700349A RID: 13466
		// (get) Token: 0x0600A140 RID: 41280
		// (set) Token: 0x0600A13F RID: 41279
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

		// Token: 0x1700349B RID: 13467
		// (get) Token: 0x0600A142 RID: 41282
		// (set) Token: 0x0600A141 RID: 41281
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

		// Token: 0x0600A143 RID: 41283
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement3_dragDrop();

		// Token: 0x1700349C RID: 13468
		// (get) Token: 0x0600A144 RID: 41284
		public virtual extern int IHTMLElement3_glyphMode
		{
			[TypeLibFunc(1089)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700349D RID: 13469
		// (get) Token: 0x0600A146 RID: 41286
		// (set) Token: 0x0600A145 RID: 41285
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

		// Token: 0x0600A147 RID: 41287
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement4_normalize();

		// Token: 0x0600A148 RID: 41288
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute IHTMLElement4_getAttributeNode([MarshalAs(19)] [In] string bstrName);

		// Token: 0x0600A149 RID: 41289
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute IHTMLElement4_setAttributeNode([MarshalAs(28)] [In] IHTMLDOMAttribute pattr);

		// Token: 0x0600A14A RID: 41290
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute IHTMLElement4_removeAttributeNode([MarshalAs(28)] [In] IHTMLDOMAttribute pattr);

		// Token: 0x1700349E RID: 13470
		// (get) Token: 0x0600A14C RID: 41292
		// (set) Token: 0x0600A14B RID: 41291
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

		// Token: 0x1700349F RID: 13471
		// (get) Token: 0x0600A14E RID: 41294
		// (set) Token: 0x0600A14D RID: 41293
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

		// Token: 0x170034A0 RID: 13472
		// (get) Token: 0x0600A150 RID: 41296
		// (set) Token: 0x0600A14F RID: 41295
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

		// Token: 0x170034A1 RID: 13473
		// (get) Token: 0x0600A151 RID: 41297
		public virtual extern int IHTMLUniqueName_uniqueNumber
		{
			[TypeLibFunc(64)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170034A2 RID: 13474
		// (get) Token: 0x0600A152 RID: 41298
		public virtual extern string IHTMLUniqueName_uniqueID
		{
			[TypeLibFunc(64)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170034A3 RID: 13475
		// (get) Token: 0x0600A153 RID: 41299
		public virtual extern int IHTMLDOMNode_nodeType
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170034A4 RID: 13476
		// (get) Token: 0x0600A154 RID: 41300
		public virtual extern IHTMLDOMNode IHTMLDOMNode_parentNode
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x0600A155 RID: 41301
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLDOMNode_hasChildNodes();

		// Token: 0x170034A5 RID: 13477
		// (get) Token: 0x0600A156 RID: 41302
		public virtual extern object IHTMLDOMNode_childNodes
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x170034A6 RID: 13478
		// (get) Token: 0x0600A157 RID: 41303
		public virtual extern object IHTMLDOMNode_attributes
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x0600A158 RID: 41304
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_insertBefore([MarshalAs(28)] [In] IHTMLDOMNode newChild, [MarshalAs(27)] [In] [Optional] object refChild);

		// Token: 0x0600A159 RID: 41305
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_removeChild([MarshalAs(28)] [In] IHTMLDOMNode oldChild);

		// Token: 0x0600A15A RID: 41306
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_replaceChild([MarshalAs(28)] [In] IHTMLDOMNode newChild, [MarshalAs(28)] [In] IHTMLDOMNode oldChild);

		// Token: 0x0600A15B RID: 41307
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_cloneNode([In] bool fDeep);

		// Token: 0x0600A15C RID: 41308
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_removeNode([In] bool fDeep = false);

		// Token: 0x0600A15D RID: 41309
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_swapNode([MarshalAs(28)] [In] IHTMLDOMNode otherNode);

		// Token: 0x0600A15E RID: 41310
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_replaceNode([MarshalAs(28)] [In] IHTMLDOMNode replacement);

		// Token: 0x0600A15F RID: 41311
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_appendChild([MarshalAs(28)] [In] IHTMLDOMNode newChild);

		// Token: 0x170034A7 RID: 13479
		// (get) Token: 0x0600A160 RID: 41312
		public virtual extern string IHTMLDOMNode_nodeName
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170034A8 RID: 13480
		// (get) Token: 0x0600A162 RID: 41314
		// (set) Token: 0x0600A161 RID: 41313
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

		// Token: 0x170034A9 RID: 13481
		// (get) Token: 0x0600A163 RID: 41315
		public virtual extern IHTMLDOMNode IHTMLDOMNode_firstChild
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170034AA RID: 13482
		// (get) Token: 0x0600A164 RID: 41316
		public virtual extern IHTMLDOMNode IHTMLDOMNode_lastChild
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170034AB RID: 13483
		// (get) Token: 0x0600A165 RID: 41317
		public virtual extern IHTMLDOMNode IHTMLDOMNode_previousSibling
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170034AC RID: 13484
		// (get) Token: 0x0600A166 RID: 41318
		public virtual extern IHTMLDOMNode IHTMLDOMNode_nextSibling
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170034AD RID: 13485
		// (get) Token: 0x0600A167 RID: 41319
		public virtual extern object IHTMLDOMNode2_ownerDocument
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x170034AE RID: 13486
		// (get) Token: 0x0600A169 RID: 41321
		// (set) Token: 0x0600A168 RID: 41320
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

		// Token: 0x170034AF RID: 13487
		// (get) Token: 0x0600A16B RID: 41323
		// (set) Token: 0x0600A16A RID: 41322
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

		// Token: 0x170034B0 RID: 13488
		// (get) Token: 0x0600A16D RID: 41325
		// (set) Token: 0x0600A16C RID: 41324
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

		// Token: 0x170034B1 RID: 13489
		// (get) Token: 0x0600A16F RID: 41327
		// (set) Token: 0x0600A16E RID: 41326
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

		// Token: 0x0600A170 RID: 41328
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLControlElement_focus();

		// Token: 0x170034B2 RID: 13490
		// (get) Token: 0x0600A172 RID: 41330
		// (set) Token: 0x0600A171 RID: 41329
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

		// Token: 0x170034B3 RID: 13491
		// (get) Token: 0x0600A174 RID: 41332
		// (set) Token: 0x0600A173 RID: 41331
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

		// Token: 0x170034B4 RID: 13492
		// (get) Token: 0x0600A176 RID: 41334
		// (set) Token: 0x0600A175 RID: 41333
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

		// Token: 0x170034B5 RID: 13493
		// (get) Token: 0x0600A178 RID: 41336
		// (set) Token: 0x0600A177 RID: 41335
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

		// Token: 0x0600A179 RID: 41337
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLControlElement_blur();

		// Token: 0x0600A17A RID: 41338
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLControlElement_addFilter([MarshalAs(25)] [In] object pUnk);

		// Token: 0x0600A17B RID: 41339
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLControlElement_removeFilter([MarshalAs(25)] [In] object pUnk);

		// Token: 0x170034B6 RID: 13494
		// (get) Token: 0x0600A17C RID: 41340
		public virtual extern int IHTMLControlElement_clientHeight
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170034B7 RID: 13495
		// (get) Token: 0x0600A17D RID: 41341
		public virtual extern int IHTMLControlElement_clientWidth
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170034B8 RID: 13496
		// (get) Token: 0x0600A17E RID: 41342
		public virtual extern int IHTMLControlElement_clientTop
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170034B9 RID: 13497
		// (get) Token: 0x0600A17F RID: 41343
		public virtual extern int IHTMLControlElement_clientLeft
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x0600A180 RID: 41344
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(26)]
		public virtual extern object IHTMLTextContainer_createControlRange();

		// Token: 0x170034BA RID: 13498
		// (get) Token: 0x0600A181 RID: 41345
		public virtual extern int IHTMLTextContainer_scrollHeight
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170034BB RID: 13499
		// (get) Token: 0x0600A182 RID: 41346
		public virtual extern int IHTMLTextContainer_scrollWidth
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170034BC RID: 13500
		// (get) Token: 0x0600A184 RID: 41348
		// (set) Token: 0x0600A183 RID: 41347
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

		// Token: 0x170034BD RID: 13501
		// (get) Token: 0x0600A186 RID: 41350
		// (set) Token: 0x0600A185 RID: 41349
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

		// Token: 0x170034BE RID: 13502
		// (get) Token: 0x0600A188 RID: 41352
		// (set) Token: 0x0600A187 RID: 41351
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

		// Token: 0x170034BF RID: 13503
		// (get) Token: 0x0600A189 RID: 41353
		public virtual extern string IHTMLTextAreaElement_type
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170034C0 RID: 13504
		// (get) Token: 0x0600A18B RID: 41355
		// (set) Token: 0x0600A18A RID: 41354
		public virtual extern string IHTMLTextAreaElement_value
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

		// Token: 0x170034C1 RID: 13505
		// (get) Token: 0x0600A18D RID: 41357
		// (set) Token: 0x0600A18C RID: 41356
		public virtual extern string IHTMLTextAreaElement_name
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

		// Token: 0x170034C2 RID: 13506
		// (get) Token: 0x0600A18F RID: 41359
		// (set) Token: 0x0600A18E RID: 41358
		public virtual extern object IHTMLTextAreaElement_status
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x170034C3 RID: 13507
		// (get) Token: 0x0600A191 RID: 41361
		// (set) Token: 0x0600A190 RID: 41360
		public virtual extern bool IHTMLTextAreaElement_disabled
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x170034C4 RID: 13508
		// (get) Token: 0x0600A192 RID: 41362
		public virtual extern IHTMLFormElement IHTMLTextAreaElement_form
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170034C5 RID: 13509
		// (get) Token: 0x0600A194 RID: 41364
		// (set) Token: 0x0600A193 RID: 41363
		public virtual extern string IHTMLTextAreaElement_defaultValue
		{
			[TypeLibFunc(84)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(84)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x0600A195 RID: 41365
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLTextAreaElement_select();

		// Token: 0x170034C6 RID: 13510
		// (get) Token: 0x0600A197 RID: 41367
		// (set) Token: 0x0600A196 RID: 41366
		public virtual extern object IHTMLTextAreaElement_onchange
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x170034C7 RID: 13511
		// (get) Token: 0x0600A199 RID: 41369
		// (set) Token: 0x0600A198 RID: 41368
		public virtual extern object IHTMLTextAreaElement_onselect
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x170034C8 RID: 13512
		// (get) Token: 0x0600A19B RID: 41371
		// (set) Token: 0x0600A19A RID: 41370
		public virtual extern bool IHTMLTextAreaElement_readOnly
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x170034C9 RID: 13513
		// (get) Token: 0x0600A19D RID: 41373
		// (set) Token: 0x0600A19C RID: 41372
		public virtual extern int IHTMLTextAreaElement_rows
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x170034CA RID: 13514
		// (get) Token: 0x0600A19F RID: 41375
		// (set) Token: 0x0600A19E RID: 41374
		public virtual extern int IHTMLTextAreaElement_cols
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x170034CB RID: 13515
		// (get) Token: 0x0600A1A1 RID: 41377
		// (set) Token: 0x0600A1A0 RID: 41376
		public virtual extern string IHTMLTextAreaElement_wrap
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

		// Token: 0x0600A1A2 RID: 41378
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLTxtRange IHTMLTextAreaElement_createTextRange();

		// Token: 0x14001366 RID: 4966
		// (add) Token: 0x0600A1A3 RID: 41379
		// (remove) Token: 0x0600A1A4 RID: 41380
		public virtual extern event HTMLInputTextElementEvents_onhelpEventHandler HTMLInputTextElementEvents_Event_onhelp;

		// Token: 0x14001367 RID: 4967
		// (add) Token: 0x0600A1A5 RID: 41381
		// (remove) Token: 0x0600A1A6 RID: 41382
		public virtual extern event HTMLInputTextElementEvents_onclickEventHandler HTMLInputTextElementEvents_Event_onclick;

		// Token: 0x14001368 RID: 4968
		// (add) Token: 0x0600A1A7 RID: 41383
		// (remove) Token: 0x0600A1A8 RID: 41384
		public virtual extern event HTMLInputTextElementEvents_ondblclickEventHandler HTMLInputTextElementEvents_Event_ondblclick;

		// Token: 0x14001369 RID: 4969
		// (add) Token: 0x0600A1A9 RID: 41385
		// (remove) Token: 0x0600A1AA RID: 41386
		public virtual extern event HTMLInputTextElementEvents_onkeypressEventHandler HTMLInputTextElementEvents_Event_onkeypress;

		// Token: 0x1400136A RID: 4970
		// (add) Token: 0x0600A1AB RID: 41387
		// (remove) Token: 0x0600A1AC RID: 41388
		public virtual extern event HTMLInputTextElementEvents_onkeydownEventHandler HTMLInputTextElementEvents_Event_onkeydown;

		// Token: 0x1400136B RID: 4971
		// (add) Token: 0x0600A1AD RID: 41389
		// (remove) Token: 0x0600A1AE RID: 41390
		public virtual extern event HTMLInputTextElementEvents_onkeyupEventHandler HTMLInputTextElementEvents_Event_onkeyup;

		// Token: 0x1400136C RID: 4972
		// (add) Token: 0x0600A1AF RID: 41391
		// (remove) Token: 0x0600A1B0 RID: 41392
		public virtual extern event HTMLInputTextElementEvents_onmouseoutEventHandler HTMLInputTextElementEvents_Event_onmouseout;

		// Token: 0x1400136D RID: 4973
		// (add) Token: 0x0600A1B1 RID: 41393
		// (remove) Token: 0x0600A1B2 RID: 41394
		public virtual extern event HTMLInputTextElementEvents_onmouseoverEventHandler HTMLInputTextElementEvents_Event_onmouseover;

		// Token: 0x1400136E RID: 4974
		// (add) Token: 0x0600A1B3 RID: 41395
		// (remove) Token: 0x0600A1B4 RID: 41396
		public virtual extern event HTMLInputTextElementEvents_onmousemoveEventHandler HTMLInputTextElementEvents_Event_onmousemove;

		// Token: 0x1400136F RID: 4975
		// (add) Token: 0x0600A1B5 RID: 41397
		// (remove) Token: 0x0600A1B6 RID: 41398
		public virtual extern event HTMLInputTextElementEvents_onmousedownEventHandler HTMLInputTextElementEvents_Event_onmousedown;

		// Token: 0x14001370 RID: 4976
		// (add) Token: 0x0600A1B7 RID: 41399
		// (remove) Token: 0x0600A1B8 RID: 41400
		public virtual extern event HTMLInputTextElementEvents_onmouseupEventHandler HTMLInputTextElementEvents_Event_onmouseup;

		// Token: 0x14001371 RID: 4977
		// (add) Token: 0x0600A1B9 RID: 41401
		// (remove) Token: 0x0600A1BA RID: 41402
		public virtual extern event HTMLInputTextElementEvents_onselectstartEventHandler HTMLInputTextElementEvents_Event_onselectstart;

		// Token: 0x14001372 RID: 4978
		// (add) Token: 0x0600A1BB RID: 41403
		// (remove) Token: 0x0600A1BC RID: 41404
		public virtual extern event HTMLInputTextElementEvents_onfilterchangeEventHandler HTMLInputTextElementEvents_Event_onfilterchange;

		// Token: 0x14001373 RID: 4979
		// (add) Token: 0x0600A1BD RID: 41405
		// (remove) Token: 0x0600A1BE RID: 41406
		public virtual extern event HTMLInputTextElementEvents_ondragstartEventHandler HTMLInputTextElementEvents_Event_ondragstart;

		// Token: 0x14001374 RID: 4980
		// (add) Token: 0x0600A1BF RID: 41407
		// (remove) Token: 0x0600A1C0 RID: 41408
		public virtual extern event HTMLInputTextElementEvents_onbeforeupdateEventHandler HTMLInputTextElementEvents_Event_onbeforeupdate;

		// Token: 0x14001375 RID: 4981
		// (add) Token: 0x0600A1C1 RID: 41409
		// (remove) Token: 0x0600A1C2 RID: 41410
		public virtual extern event HTMLInputTextElementEvents_onafterupdateEventHandler HTMLInputTextElementEvents_Event_onafterupdate;

		// Token: 0x14001376 RID: 4982
		// (add) Token: 0x0600A1C3 RID: 41411
		// (remove) Token: 0x0600A1C4 RID: 41412
		public virtual extern event HTMLInputTextElementEvents_onerrorupdateEventHandler HTMLInputTextElementEvents_Event_onerrorupdate;

		// Token: 0x14001377 RID: 4983
		// (add) Token: 0x0600A1C5 RID: 41413
		// (remove) Token: 0x0600A1C6 RID: 41414
		public virtual extern event HTMLInputTextElementEvents_onrowexitEventHandler HTMLInputTextElementEvents_Event_onrowexit;

		// Token: 0x14001378 RID: 4984
		// (add) Token: 0x0600A1C7 RID: 41415
		// (remove) Token: 0x0600A1C8 RID: 41416
		public virtual extern event HTMLInputTextElementEvents_onrowenterEventHandler HTMLInputTextElementEvents_Event_onrowenter;

		// Token: 0x14001379 RID: 4985
		// (add) Token: 0x0600A1C9 RID: 41417
		// (remove) Token: 0x0600A1CA RID: 41418
		public virtual extern event HTMLInputTextElementEvents_ondatasetchangedEventHandler HTMLInputTextElementEvents_Event_ondatasetchanged;

		// Token: 0x1400137A RID: 4986
		// (add) Token: 0x0600A1CB RID: 41419
		// (remove) Token: 0x0600A1CC RID: 41420
		public virtual extern event HTMLInputTextElementEvents_ondataavailableEventHandler HTMLInputTextElementEvents_Event_ondataavailable;

		// Token: 0x1400137B RID: 4987
		// (add) Token: 0x0600A1CD RID: 41421
		// (remove) Token: 0x0600A1CE RID: 41422
		public virtual extern event HTMLInputTextElementEvents_ondatasetcompleteEventHandler HTMLInputTextElementEvents_Event_ondatasetcomplete;

		// Token: 0x1400137C RID: 4988
		// (add) Token: 0x0600A1CF RID: 41423
		// (remove) Token: 0x0600A1D0 RID: 41424
		public virtual extern event HTMLInputTextElementEvents_onlosecaptureEventHandler HTMLInputTextElementEvents_Event_onlosecapture;

		// Token: 0x1400137D RID: 4989
		// (add) Token: 0x0600A1D1 RID: 41425
		// (remove) Token: 0x0600A1D2 RID: 41426
		public virtual extern event HTMLInputTextElementEvents_onpropertychangeEventHandler HTMLInputTextElementEvents_Event_onpropertychange;

		// Token: 0x1400137E RID: 4990
		// (add) Token: 0x0600A1D3 RID: 41427
		// (remove) Token: 0x0600A1D4 RID: 41428
		public virtual extern event HTMLInputTextElementEvents_onscrollEventHandler HTMLInputTextElementEvents_Event_onscroll;

		// Token: 0x1400137F RID: 4991
		// (add) Token: 0x0600A1D5 RID: 41429
		// (remove) Token: 0x0600A1D6 RID: 41430
		public virtual extern event HTMLInputTextElementEvents_onfocusEventHandler HTMLInputTextElementEvents_Event_onfocus;

		// Token: 0x14001380 RID: 4992
		// (add) Token: 0x0600A1D7 RID: 41431
		// (remove) Token: 0x0600A1D8 RID: 41432
		public virtual extern event HTMLInputTextElementEvents_onblurEventHandler HTMLInputTextElementEvents_Event_onblur;

		// Token: 0x14001381 RID: 4993
		// (add) Token: 0x0600A1D9 RID: 41433
		// (remove) Token: 0x0600A1DA RID: 41434
		public virtual extern event HTMLInputTextElementEvents_onresizeEventHandler HTMLInputTextElementEvents_Event_onresize;

		// Token: 0x14001382 RID: 4994
		// (add) Token: 0x0600A1DB RID: 41435
		// (remove) Token: 0x0600A1DC RID: 41436
		public virtual extern event HTMLInputTextElementEvents_ondragEventHandler HTMLInputTextElementEvents_Event_ondrag;

		// Token: 0x14001383 RID: 4995
		// (add) Token: 0x0600A1DD RID: 41437
		// (remove) Token: 0x0600A1DE RID: 41438
		public virtual extern event HTMLInputTextElementEvents_ondragendEventHandler HTMLInputTextElementEvents_Event_ondragend;

		// Token: 0x14001384 RID: 4996
		// (add) Token: 0x0600A1DF RID: 41439
		// (remove) Token: 0x0600A1E0 RID: 41440
		public virtual extern event HTMLInputTextElementEvents_ondragenterEventHandler HTMLInputTextElementEvents_Event_ondragenter;

		// Token: 0x14001385 RID: 4997
		// (add) Token: 0x0600A1E1 RID: 41441
		// (remove) Token: 0x0600A1E2 RID: 41442
		public virtual extern event HTMLInputTextElementEvents_ondragoverEventHandler HTMLInputTextElementEvents_Event_ondragover;

		// Token: 0x14001386 RID: 4998
		// (add) Token: 0x0600A1E3 RID: 41443
		// (remove) Token: 0x0600A1E4 RID: 41444
		public virtual extern event HTMLInputTextElementEvents_ondragleaveEventHandler HTMLInputTextElementEvents_Event_ondragleave;

		// Token: 0x14001387 RID: 4999
		// (add) Token: 0x0600A1E5 RID: 41445
		// (remove) Token: 0x0600A1E6 RID: 41446
		public virtual extern event HTMLInputTextElementEvents_ondropEventHandler HTMLInputTextElementEvents_Event_ondrop;

		// Token: 0x14001388 RID: 5000
		// (add) Token: 0x0600A1E7 RID: 41447
		// (remove) Token: 0x0600A1E8 RID: 41448
		public virtual extern event HTMLInputTextElementEvents_onbeforecutEventHandler HTMLInputTextElementEvents_Event_onbeforecut;

		// Token: 0x14001389 RID: 5001
		// (add) Token: 0x0600A1E9 RID: 41449
		// (remove) Token: 0x0600A1EA RID: 41450
		public virtual extern event HTMLInputTextElementEvents_oncutEventHandler HTMLInputTextElementEvents_Event_oncut;

		// Token: 0x1400138A RID: 5002
		// (add) Token: 0x0600A1EB RID: 41451
		// (remove) Token: 0x0600A1EC RID: 41452
		public virtual extern event HTMLInputTextElementEvents_onbeforecopyEventHandler HTMLInputTextElementEvents_Event_onbeforecopy;

		// Token: 0x1400138B RID: 5003
		// (add) Token: 0x0600A1ED RID: 41453
		// (remove) Token: 0x0600A1EE RID: 41454
		public virtual extern event HTMLInputTextElementEvents_oncopyEventHandler HTMLInputTextElementEvents_Event_oncopy;

		// Token: 0x1400138C RID: 5004
		// (add) Token: 0x0600A1EF RID: 41455
		// (remove) Token: 0x0600A1F0 RID: 41456
		public virtual extern event HTMLInputTextElementEvents_onbeforepasteEventHandler HTMLInputTextElementEvents_Event_onbeforepaste;

		// Token: 0x1400138D RID: 5005
		// (add) Token: 0x0600A1F1 RID: 41457
		// (remove) Token: 0x0600A1F2 RID: 41458
		public virtual extern event HTMLInputTextElementEvents_onpasteEventHandler HTMLInputTextElementEvents_Event_onpaste;

		// Token: 0x1400138E RID: 5006
		// (add) Token: 0x0600A1F3 RID: 41459
		// (remove) Token: 0x0600A1F4 RID: 41460
		public virtual extern event HTMLInputTextElementEvents_oncontextmenuEventHandler HTMLInputTextElementEvents_Event_oncontextmenu;

		// Token: 0x1400138F RID: 5007
		// (add) Token: 0x0600A1F5 RID: 41461
		// (remove) Token: 0x0600A1F6 RID: 41462
		public virtual extern event HTMLInputTextElementEvents_onrowsdeleteEventHandler HTMLInputTextElementEvents_Event_onrowsdelete;

		// Token: 0x14001390 RID: 5008
		// (add) Token: 0x0600A1F7 RID: 41463
		// (remove) Token: 0x0600A1F8 RID: 41464
		public virtual extern event HTMLInputTextElementEvents_onrowsinsertedEventHandler HTMLInputTextElementEvents_Event_onrowsinserted;

		// Token: 0x14001391 RID: 5009
		// (add) Token: 0x0600A1F9 RID: 41465
		// (remove) Token: 0x0600A1FA RID: 41466
		public virtual extern event HTMLInputTextElementEvents_oncellchangeEventHandler HTMLInputTextElementEvents_Event_oncellchange;

		// Token: 0x14001392 RID: 5010
		// (add) Token: 0x0600A1FB RID: 41467
		// (remove) Token: 0x0600A1FC RID: 41468
		public virtual extern event HTMLInputTextElementEvents_onreadystatechangeEventHandler HTMLInputTextElementEvents_Event_onreadystatechange;

		// Token: 0x14001393 RID: 5011
		// (add) Token: 0x0600A1FD RID: 41469
		// (remove) Token: 0x0600A1FE RID: 41470
		public virtual extern event HTMLInputTextElementEvents_onbeforeeditfocusEventHandler HTMLInputTextElementEvents_Event_onbeforeeditfocus;

		// Token: 0x14001394 RID: 5012
		// (add) Token: 0x0600A1FF RID: 41471
		// (remove) Token: 0x0600A200 RID: 41472
		public virtual extern event HTMLInputTextElementEvents_onlayoutcompleteEventHandler HTMLInputTextElementEvents_Event_onlayoutcomplete;

		// Token: 0x14001395 RID: 5013
		// (add) Token: 0x0600A201 RID: 41473
		// (remove) Token: 0x0600A202 RID: 41474
		public virtual extern event HTMLInputTextElementEvents_onpageEventHandler HTMLInputTextElementEvents_Event_onpage;

		// Token: 0x14001396 RID: 5014
		// (add) Token: 0x0600A203 RID: 41475
		// (remove) Token: 0x0600A204 RID: 41476
		public virtual extern event HTMLInputTextElementEvents_onbeforedeactivateEventHandler HTMLInputTextElementEvents_Event_onbeforedeactivate;

		// Token: 0x14001397 RID: 5015
		// (add) Token: 0x0600A205 RID: 41477
		// (remove) Token: 0x0600A206 RID: 41478
		public virtual extern event HTMLInputTextElementEvents_onbeforeactivateEventHandler HTMLInputTextElementEvents_Event_onbeforeactivate;

		// Token: 0x14001398 RID: 5016
		// (add) Token: 0x0600A207 RID: 41479
		// (remove) Token: 0x0600A208 RID: 41480
		public virtual extern event HTMLInputTextElementEvents_onmoveEventHandler HTMLInputTextElementEvents_Event_onmove;

		// Token: 0x14001399 RID: 5017
		// (add) Token: 0x0600A209 RID: 41481
		// (remove) Token: 0x0600A20A RID: 41482
		public virtual extern event HTMLInputTextElementEvents_oncontrolselectEventHandler HTMLInputTextElementEvents_Event_oncontrolselect;

		// Token: 0x1400139A RID: 5018
		// (add) Token: 0x0600A20B RID: 41483
		// (remove) Token: 0x0600A20C RID: 41484
		public virtual extern event HTMLInputTextElementEvents_onmovestartEventHandler HTMLInputTextElementEvents_Event_onmovestart;

		// Token: 0x1400139B RID: 5019
		// (add) Token: 0x0600A20D RID: 41485
		// (remove) Token: 0x0600A20E RID: 41486
		public virtual extern event HTMLInputTextElementEvents_onmoveendEventHandler HTMLInputTextElementEvents_Event_onmoveend;

		// Token: 0x1400139C RID: 5020
		// (add) Token: 0x0600A20F RID: 41487
		// (remove) Token: 0x0600A210 RID: 41488
		public virtual extern event HTMLInputTextElementEvents_onresizestartEventHandler HTMLInputTextElementEvents_Event_onresizestart;

		// Token: 0x1400139D RID: 5021
		// (add) Token: 0x0600A211 RID: 41489
		// (remove) Token: 0x0600A212 RID: 41490
		public virtual extern event HTMLInputTextElementEvents_onresizeendEventHandler HTMLInputTextElementEvents_Event_onresizeend;

		// Token: 0x1400139E RID: 5022
		// (add) Token: 0x0600A213 RID: 41491
		// (remove) Token: 0x0600A214 RID: 41492
		public virtual extern event HTMLInputTextElementEvents_onmouseenterEventHandler HTMLInputTextElementEvents_Event_onmouseenter;

		// Token: 0x1400139F RID: 5023
		// (add) Token: 0x0600A215 RID: 41493
		// (remove) Token: 0x0600A216 RID: 41494
		public virtual extern event HTMLInputTextElementEvents_onmouseleaveEventHandler HTMLInputTextElementEvents_Event_onmouseleave;

		// Token: 0x140013A0 RID: 5024
		// (add) Token: 0x0600A217 RID: 41495
		// (remove) Token: 0x0600A218 RID: 41496
		public virtual extern event HTMLInputTextElementEvents_onmousewheelEventHandler HTMLInputTextElementEvents_Event_onmousewheel;

		// Token: 0x140013A1 RID: 5025
		// (add) Token: 0x0600A219 RID: 41497
		// (remove) Token: 0x0600A21A RID: 41498
		public virtual extern event HTMLInputTextElementEvents_onactivateEventHandler HTMLInputTextElementEvents_Event_onactivate;

		// Token: 0x140013A2 RID: 5026
		// (add) Token: 0x0600A21B RID: 41499
		// (remove) Token: 0x0600A21C RID: 41500
		public virtual extern event HTMLInputTextElementEvents_ondeactivateEventHandler HTMLInputTextElementEvents_Event_ondeactivate;

		// Token: 0x140013A3 RID: 5027
		// (add) Token: 0x0600A21D RID: 41501
		// (remove) Token: 0x0600A21E RID: 41502
		public virtual extern event HTMLInputTextElementEvents_onfocusinEventHandler HTMLInputTextElementEvents_Event_onfocusin;

		// Token: 0x140013A4 RID: 5028
		// (add) Token: 0x0600A21F RID: 41503
		// (remove) Token: 0x0600A220 RID: 41504
		public virtual extern event HTMLInputTextElementEvents_onfocusoutEventHandler HTMLInputTextElementEvents_Event_onfocusout;

		// Token: 0x140013A5 RID: 5029
		// (add) Token: 0x0600A221 RID: 41505
		// (remove) Token: 0x0600A222 RID: 41506
		public virtual extern event HTMLInputTextElementEvents_onchangeEventHandler HTMLInputTextElementEvents_Event_onchange;

		// Token: 0x140013A6 RID: 5030
		// (add) Token: 0x0600A223 RID: 41507
		// (remove) Token: 0x0600A224 RID: 41508
		public virtual extern event HTMLInputTextElementEvents_onselectEventHandler HTMLInputTextElementEvents_Event_onselect;

		// Token: 0x140013A7 RID: 5031
		// (add) Token: 0x0600A225 RID: 41509
		// (remove) Token: 0x0600A226 RID: 41510
		public virtual extern event HTMLInputTextElementEvents_onloadEventHandler onload;

		// Token: 0x140013A8 RID: 5032
		// (add) Token: 0x0600A227 RID: 41511
		// (remove) Token: 0x0600A228 RID: 41512
		public virtual extern event HTMLInputTextElementEvents_onerrorEventHandler onerror;

		// Token: 0x140013A9 RID: 5033
		// (add) Token: 0x0600A229 RID: 41513
		// (remove) Token: 0x0600A22A RID: 41514
		public virtual extern event HTMLInputTextElementEvents_onabortEventHandler onabort;

		// Token: 0x140013AA RID: 5034
		// (add) Token: 0x0600A22B RID: 41515
		// (remove) Token: 0x0600A22C RID: 41516
		public virtual extern event HTMLInputTextElementEvents2_onhelpEventHandler HTMLInputTextElementEvents2_Event_onhelp;

		// Token: 0x140013AB RID: 5035
		// (add) Token: 0x0600A22D RID: 41517
		// (remove) Token: 0x0600A22E RID: 41518
		public virtual extern event HTMLInputTextElementEvents2_onclickEventHandler HTMLInputTextElementEvents2_Event_onclick;

		// Token: 0x140013AC RID: 5036
		// (add) Token: 0x0600A22F RID: 41519
		// (remove) Token: 0x0600A230 RID: 41520
		public virtual extern event HTMLInputTextElementEvents2_ondblclickEventHandler HTMLInputTextElementEvents2_Event_ondblclick;

		// Token: 0x140013AD RID: 5037
		// (add) Token: 0x0600A231 RID: 41521
		// (remove) Token: 0x0600A232 RID: 41522
		public virtual extern event HTMLInputTextElementEvents2_onkeypressEventHandler HTMLInputTextElementEvents2_Event_onkeypress;

		// Token: 0x140013AE RID: 5038
		// (add) Token: 0x0600A233 RID: 41523
		// (remove) Token: 0x0600A234 RID: 41524
		public virtual extern event HTMLInputTextElementEvents2_onkeydownEventHandler HTMLInputTextElementEvents2_Event_onkeydown;

		// Token: 0x140013AF RID: 5039
		// (add) Token: 0x0600A235 RID: 41525
		// (remove) Token: 0x0600A236 RID: 41526
		public virtual extern event HTMLInputTextElementEvents2_onkeyupEventHandler HTMLInputTextElementEvents2_Event_onkeyup;

		// Token: 0x140013B0 RID: 5040
		// (add) Token: 0x0600A237 RID: 41527
		// (remove) Token: 0x0600A238 RID: 41528
		public virtual extern event HTMLInputTextElementEvents2_onmouseoutEventHandler HTMLInputTextElementEvents2_Event_onmouseout;

		// Token: 0x140013B1 RID: 5041
		// (add) Token: 0x0600A239 RID: 41529
		// (remove) Token: 0x0600A23A RID: 41530
		public virtual extern event HTMLInputTextElementEvents2_onmouseoverEventHandler HTMLInputTextElementEvents2_Event_onmouseover;

		// Token: 0x140013B2 RID: 5042
		// (add) Token: 0x0600A23B RID: 41531
		// (remove) Token: 0x0600A23C RID: 41532
		public virtual extern event HTMLInputTextElementEvents2_onmousemoveEventHandler HTMLInputTextElementEvents2_Event_onmousemove;

		// Token: 0x140013B3 RID: 5043
		// (add) Token: 0x0600A23D RID: 41533
		// (remove) Token: 0x0600A23E RID: 41534
		public virtual extern event HTMLInputTextElementEvents2_onmousedownEventHandler HTMLInputTextElementEvents2_Event_onmousedown;

		// Token: 0x140013B4 RID: 5044
		// (add) Token: 0x0600A23F RID: 41535
		// (remove) Token: 0x0600A240 RID: 41536
		public virtual extern event HTMLInputTextElementEvents2_onmouseupEventHandler HTMLInputTextElementEvents2_Event_onmouseup;

		// Token: 0x140013B5 RID: 5045
		// (add) Token: 0x0600A241 RID: 41537
		// (remove) Token: 0x0600A242 RID: 41538
		public virtual extern event HTMLInputTextElementEvents2_onselectstartEventHandler HTMLInputTextElementEvents2_Event_onselectstart;

		// Token: 0x140013B6 RID: 5046
		// (add) Token: 0x0600A243 RID: 41539
		// (remove) Token: 0x0600A244 RID: 41540
		public virtual extern event HTMLInputTextElementEvents2_onfilterchangeEventHandler HTMLInputTextElementEvents2_Event_onfilterchange;

		// Token: 0x140013B7 RID: 5047
		// (add) Token: 0x0600A245 RID: 41541
		// (remove) Token: 0x0600A246 RID: 41542
		public virtual extern event HTMLInputTextElementEvents2_ondragstartEventHandler HTMLInputTextElementEvents2_Event_ondragstart;

		// Token: 0x140013B8 RID: 5048
		// (add) Token: 0x0600A247 RID: 41543
		// (remove) Token: 0x0600A248 RID: 41544
		public virtual extern event HTMLInputTextElementEvents2_onbeforeupdateEventHandler HTMLInputTextElementEvents2_Event_onbeforeupdate;

		// Token: 0x140013B9 RID: 5049
		// (add) Token: 0x0600A249 RID: 41545
		// (remove) Token: 0x0600A24A RID: 41546
		public virtual extern event HTMLInputTextElementEvents2_onafterupdateEventHandler HTMLInputTextElementEvents2_Event_onafterupdate;

		// Token: 0x140013BA RID: 5050
		// (add) Token: 0x0600A24B RID: 41547
		// (remove) Token: 0x0600A24C RID: 41548
		public virtual extern event HTMLInputTextElementEvents2_onerrorupdateEventHandler HTMLInputTextElementEvents2_Event_onerrorupdate;

		// Token: 0x140013BB RID: 5051
		// (add) Token: 0x0600A24D RID: 41549
		// (remove) Token: 0x0600A24E RID: 41550
		public virtual extern event HTMLInputTextElementEvents2_onrowexitEventHandler HTMLInputTextElementEvents2_Event_onrowexit;

		// Token: 0x140013BC RID: 5052
		// (add) Token: 0x0600A24F RID: 41551
		// (remove) Token: 0x0600A250 RID: 41552
		public virtual extern event HTMLInputTextElementEvents2_onrowenterEventHandler HTMLInputTextElementEvents2_Event_onrowenter;

		// Token: 0x140013BD RID: 5053
		// (add) Token: 0x0600A251 RID: 41553
		// (remove) Token: 0x0600A252 RID: 41554
		public virtual extern event HTMLInputTextElementEvents2_ondatasetchangedEventHandler HTMLInputTextElementEvents2_Event_ondatasetchanged;

		// Token: 0x140013BE RID: 5054
		// (add) Token: 0x0600A253 RID: 41555
		// (remove) Token: 0x0600A254 RID: 41556
		public virtual extern event HTMLInputTextElementEvents2_ondataavailableEventHandler HTMLInputTextElementEvents2_Event_ondataavailable;

		// Token: 0x140013BF RID: 5055
		// (add) Token: 0x0600A255 RID: 41557
		// (remove) Token: 0x0600A256 RID: 41558
		public virtual extern event HTMLInputTextElementEvents2_ondatasetcompleteEventHandler HTMLInputTextElementEvents2_Event_ondatasetcomplete;

		// Token: 0x140013C0 RID: 5056
		// (add) Token: 0x0600A257 RID: 41559
		// (remove) Token: 0x0600A258 RID: 41560
		public virtual extern event HTMLInputTextElementEvents2_onlosecaptureEventHandler HTMLInputTextElementEvents2_Event_onlosecapture;

		// Token: 0x140013C1 RID: 5057
		// (add) Token: 0x0600A259 RID: 41561
		// (remove) Token: 0x0600A25A RID: 41562
		public virtual extern event HTMLInputTextElementEvents2_onpropertychangeEventHandler HTMLInputTextElementEvents2_Event_onpropertychange;

		// Token: 0x140013C2 RID: 5058
		// (add) Token: 0x0600A25B RID: 41563
		// (remove) Token: 0x0600A25C RID: 41564
		public virtual extern event HTMLInputTextElementEvents2_onscrollEventHandler HTMLInputTextElementEvents2_Event_onscroll;

		// Token: 0x140013C3 RID: 5059
		// (add) Token: 0x0600A25D RID: 41565
		// (remove) Token: 0x0600A25E RID: 41566
		public virtual extern event HTMLInputTextElementEvents2_onfocusEventHandler HTMLInputTextElementEvents2_Event_onfocus;

		// Token: 0x140013C4 RID: 5060
		// (add) Token: 0x0600A25F RID: 41567
		// (remove) Token: 0x0600A260 RID: 41568
		public virtual extern event HTMLInputTextElementEvents2_onblurEventHandler HTMLInputTextElementEvents2_Event_onblur;

		// Token: 0x140013C5 RID: 5061
		// (add) Token: 0x0600A261 RID: 41569
		// (remove) Token: 0x0600A262 RID: 41570
		public virtual extern event HTMLInputTextElementEvents2_onresizeEventHandler HTMLInputTextElementEvents2_Event_onresize;

		// Token: 0x140013C6 RID: 5062
		// (add) Token: 0x0600A263 RID: 41571
		// (remove) Token: 0x0600A264 RID: 41572
		public virtual extern event HTMLInputTextElementEvents2_ondragEventHandler HTMLInputTextElementEvents2_Event_ondrag;

		// Token: 0x140013C7 RID: 5063
		// (add) Token: 0x0600A265 RID: 41573
		// (remove) Token: 0x0600A266 RID: 41574
		public virtual extern event HTMLInputTextElementEvents2_ondragendEventHandler HTMLInputTextElementEvents2_Event_ondragend;

		// Token: 0x140013C8 RID: 5064
		// (add) Token: 0x0600A267 RID: 41575
		// (remove) Token: 0x0600A268 RID: 41576
		public virtual extern event HTMLInputTextElementEvents2_ondragenterEventHandler HTMLInputTextElementEvents2_Event_ondragenter;

		// Token: 0x140013C9 RID: 5065
		// (add) Token: 0x0600A269 RID: 41577
		// (remove) Token: 0x0600A26A RID: 41578
		public virtual extern event HTMLInputTextElementEvents2_ondragoverEventHandler HTMLInputTextElementEvents2_Event_ondragover;

		// Token: 0x140013CA RID: 5066
		// (add) Token: 0x0600A26B RID: 41579
		// (remove) Token: 0x0600A26C RID: 41580
		public virtual extern event HTMLInputTextElementEvents2_ondragleaveEventHandler HTMLInputTextElementEvents2_Event_ondragleave;

		// Token: 0x140013CB RID: 5067
		// (add) Token: 0x0600A26D RID: 41581
		// (remove) Token: 0x0600A26E RID: 41582
		public virtual extern event HTMLInputTextElementEvents2_ondropEventHandler HTMLInputTextElementEvents2_Event_ondrop;

		// Token: 0x140013CC RID: 5068
		// (add) Token: 0x0600A26F RID: 41583
		// (remove) Token: 0x0600A270 RID: 41584
		public virtual extern event HTMLInputTextElementEvents2_onbeforecutEventHandler HTMLInputTextElementEvents2_Event_onbeforecut;

		// Token: 0x140013CD RID: 5069
		// (add) Token: 0x0600A271 RID: 41585
		// (remove) Token: 0x0600A272 RID: 41586
		public virtual extern event HTMLInputTextElementEvents2_oncutEventHandler HTMLInputTextElementEvents2_Event_oncut;

		// Token: 0x140013CE RID: 5070
		// (add) Token: 0x0600A273 RID: 41587
		// (remove) Token: 0x0600A274 RID: 41588
		public virtual extern event HTMLInputTextElementEvents2_onbeforecopyEventHandler HTMLInputTextElementEvents2_Event_onbeforecopy;

		// Token: 0x140013CF RID: 5071
		// (add) Token: 0x0600A275 RID: 41589
		// (remove) Token: 0x0600A276 RID: 41590
		public virtual extern event HTMLInputTextElementEvents2_oncopyEventHandler HTMLInputTextElementEvents2_Event_oncopy;

		// Token: 0x140013D0 RID: 5072
		// (add) Token: 0x0600A277 RID: 41591
		// (remove) Token: 0x0600A278 RID: 41592
		public virtual extern event HTMLInputTextElementEvents2_onbeforepasteEventHandler HTMLInputTextElementEvents2_Event_onbeforepaste;

		// Token: 0x140013D1 RID: 5073
		// (add) Token: 0x0600A279 RID: 41593
		// (remove) Token: 0x0600A27A RID: 41594
		public virtual extern event HTMLInputTextElementEvents2_onpasteEventHandler HTMLInputTextElementEvents2_Event_onpaste;

		// Token: 0x140013D2 RID: 5074
		// (add) Token: 0x0600A27B RID: 41595
		// (remove) Token: 0x0600A27C RID: 41596
		public virtual extern event HTMLInputTextElementEvents2_oncontextmenuEventHandler HTMLInputTextElementEvents2_Event_oncontextmenu;

		// Token: 0x140013D3 RID: 5075
		// (add) Token: 0x0600A27D RID: 41597
		// (remove) Token: 0x0600A27E RID: 41598
		public virtual extern event HTMLInputTextElementEvents2_onrowsdeleteEventHandler HTMLInputTextElementEvents2_Event_onrowsdelete;

		// Token: 0x140013D4 RID: 5076
		// (add) Token: 0x0600A27F RID: 41599
		// (remove) Token: 0x0600A280 RID: 41600
		public virtual extern event HTMLInputTextElementEvents2_onrowsinsertedEventHandler HTMLInputTextElementEvents2_Event_onrowsinserted;

		// Token: 0x140013D5 RID: 5077
		// (add) Token: 0x0600A281 RID: 41601
		// (remove) Token: 0x0600A282 RID: 41602
		public virtual extern event HTMLInputTextElementEvents2_oncellchangeEventHandler HTMLInputTextElementEvents2_Event_oncellchange;

		// Token: 0x140013D6 RID: 5078
		// (add) Token: 0x0600A283 RID: 41603
		// (remove) Token: 0x0600A284 RID: 41604
		public virtual extern event HTMLInputTextElementEvents2_onreadystatechangeEventHandler HTMLInputTextElementEvents2_Event_onreadystatechange;

		// Token: 0x140013D7 RID: 5079
		// (add) Token: 0x0600A285 RID: 41605
		// (remove) Token: 0x0600A286 RID: 41606
		public virtual extern event HTMLInputTextElementEvents2_onlayoutcompleteEventHandler HTMLInputTextElementEvents2_Event_onlayoutcomplete;

		// Token: 0x140013D8 RID: 5080
		// (add) Token: 0x0600A287 RID: 41607
		// (remove) Token: 0x0600A288 RID: 41608
		public virtual extern event HTMLInputTextElementEvents2_onpageEventHandler HTMLInputTextElementEvents2_Event_onpage;

		// Token: 0x140013D9 RID: 5081
		// (add) Token: 0x0600A289 RID: 41609
		// (remove) Token: 0x0600A28A RID: 41610
		public virtual extern event HTMLInputTextElementEvents2_onmouseenterEventHandler HTMLInputTextElementEvents2_Event_onmouseenter;

		// Token: 0x140013DA RID: 5082
		// (add) Token: 0x0600A28B RID: 41611
		// (remove) Token: 0x0600A28C RID: 41612
		public virtual extern event HTMLInputTextElementEvents2_onmouseleaveEventHandler HTMLInputTextElementEvents2_Event_onmouseleave;

		// Token: 0x140013DB RID: 5083
		// (add) Token: 0x0600A28D RID: 41613
		// (remove) Token: 0x0600A28E RID: 41614
		public virtual extern event HTMLInputTextElementEvents2_onactivateEventHandler HTMLInputTextElementEvents2_Event_onactivate;

		// Token: 0x140013DC RID: 5084
		// (add) Token: 0x0600A28F RID: 41615
		// (remove) Token: 0x0600A290 RID: 41616
		public virtual extern event HTMLInputTextElementEvents2_ondeactivateEventHandler HTMLInputTextElementEvents2_Event_ondeactivate;

		// Token: 0x140013DD RID: 5085
		// (add) Token: 0x0600A291 RID: 41617
		// (remove) Token: 0x0600A292 RID: 41618
		public virtual extern event HTMLInputTextElementEvents2_onbeforedeactivateEventHandler HTMLInputTextElementEvents2_Event_onbeforedeactivate;

		// Token: 0x140013DE RID: 5086
		// (add) Token: 0x0600A293 RID: 41619
		// (remove) Token: 0x0600A294 RID: 41620
		public virtual extern event HTMLInputTextElementEvents2_onbeforeactivateEventHandler HTMLInputTextElementEvents2_Event_onbeforeactivate;

		// Token: 0x140013DF RID: 5087
		// (add) Token: 0x0600A295 RID: 41621
		// (remove) Token: 0x0600A296 RID: 41622
		public virtual extern event HTMLInputTextElementEvents2_onfocusinEventHandler HTMLInputTextElementEvents2_Event_onfocusin;

		// Token: 0x140013E0 RID: 5088
		// (add) Token: 0x0600A297 RID: 41623
		// (remove) Token: 0x0600A298 RID: 41624
		public virtual extern event HTMLInputTextElementEvents2_onfocusoutEventHandler HTMLInputTextElementEvents2_Event_onfocusout;

		// Token: 0x140013E1 RID: 5089
		// (add) Token: 0x0600A299 RID: 41625
		// (remove) Token: 0x0600A29A RID: 41626
		public virtual extern event HTMLInputTextElementEvents2_onmoveEventHandler HTMLInputTextElementEvents2_Event_onmove;

		// Token: 0x140013E2 RID: 5090
		// (add) Token: 0x0600A29B RID: 41627
		// (remove) Token: 0x0600A29C RID: 41628
		public virtual extern event HTMLInputTextElementEvents2_oncontrolselectEventHandler HTMLInputTextElementEvents2_Event_oncontrolselect;

		// Token: 0x140013E3 RID: 5091
		// (add) Token: 0x0600A29D RID: 41629
		// (remove) Token: 0x0600A29E RID: 41630
		public virtual extern event HTMLInputTextElementEvents2_onmovestartEventHandler HTMLInputTextElementEvents2_Event_onmovestart;

		// Token: 0x140013E4 RID: 5092
		// (add) Token: 0x0600A29F RID: 41631
		// (remove) Token: 0x0600A2A0 RID: 41632
		public virtual extern event HTMLInputTextElementEvents2_onmoveendEventHandler HTMLInputTextElementEvents2_Event_onmoveend;

		// Token: 0x140013E5 RID: 5093
		// (add) Token: 0x0600A2A1 RID: 41633
		// (remove) Token: 0x0600A2A2 RID: 41634
		public virtual extern event HTMLInputTextElementEvents2_onresizestartEventHandler HTMLInputTextElementEvents2_Event_onresizestart;

		// Token: 0x140013E6 RID: 5094
		// (add) Token: 0x0600A2A3 RID: 41635
		// (remove) Token: 0x0600A2A4 RID: 41636
		public virtual extern event HTMLInputTextElementEvents2_onresizeendEventHandler HTMLInputTextElementEvents2_Event_onresizeend;

		// Token: 0x140013E7 RID: 5095
		// (add) Token: 0x0600A2A5 RID: 41637
		// (remove) Token: 0x0600A2A6 RID: 41638
		public virtual extern event HTMLInputTextElementEvents2_onmousewheelEventHandler HTMLInputTextElementEvents2_Event_onmousewheel;

		// Token: 0x140013E8 RID: 5096
		// (add) Token: 0x0600A2A7 RID: 41639
		// (remove) Token: 0x0600A2A8 RID: 41640
		public virtual extern event HTMLInputTextElementEvents2_onchangeEventHandler HTMLInputTextElementEvents2_Event_onchange;

		// Token: 0x140013E9 RID: 5097
		// (add) Token: 0x0600A2A9 RID: 41641
		// (remove) Token: 0x0600A2AA RID: 41642
		public virtual extern event HTMLInputTextElementEvents2_onselectEventHandler HTMLInputTextElementEvents2_Event_onselect;

		// Token: 0x140013EA RID: 5098
		// (add) Token: 0x0600A2AB RID: 41643
		// (remove) Token: 0x0600A2AC RID: 41644
		public virtual extern event HTMLInputTextElementEvents2_onloadEventHandler HTMLInputTextElementEvents2_Event_onload;

		// Token: 0x140013EB RID: 5099
		// (add) Token: 0x0600A2AD RID: 41645
		// (remove) Token: 0x0600A2AE RID: 41646
		public virtual extern event HTMLInputTextElementEvents2_onerrorEventHandler HTMLInputTextElementEvents2_Event_onerror;

		// Token: 0x140013EC RID: 5100
		// (add) Token: 0x0600A2AF RID: 41647
		// (remove) Token: 0x0600A2B0 RID: 41648
		public virtual extern event HTMLInputTextElementEvents2_onabortEventHandler HTMLInputTextElementEvents2_Event_onabort;
	}
}
