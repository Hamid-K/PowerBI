using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000584 RID: 1412
	[ClassInterface(0)]
	[ComSourceInterfaces("mshtml.HTMLInputTextElementEvents\0mshtml.HTMLInputTextElementEvents2\0mshtml.HTMLOptionButtonElementEvents\0mshtml.HTMLButtonElementEvents\0\0")]
	[TypeLibType(2)]
	[Guid("3050F5D8-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public class HTMLInputElementClass : DispHTMLInputElement, HTMLInputElement, HTMLInputTextElementEvents_Event, IHTMLElement, IHTMLElement2, IHTMLElement3, IHTMLElement4, IHTMLUniqueName, IHTMLDOMNode, IHTMLDOMNode2, IHTMLDatabinding, IHTMLControlElement, IHTMLInputElement, IHTMLInputElement2, IHTMLInputTextElement, IHTMLInputHiddenElement, IHTMLInputButtonElement, IHTMLInputFileElement, IHTMLOptionButtonElement, IHTMLInputImage, HTMLInputTextElementEvents2_Event, HTMLOptionButtonElementEvents_Event, HTMLButtonElementEvents_Event
	{
		// Token: 0x06008EFA RID: 36602
		[MethodImpl(4096, MethodCodeType = 3)]
		public extern HTMLInputElementClass();

		// Token: 0x06008EFB RID: 36603
		[DispId(-2147417611)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void setAttribute([MarshalAs(19)] [In] string strAttributeName, [MarshalAs(27)] [In] object AttributeValue, [In] int lFlags = 1);

		// Token: 0x06008EFC RID: 36604
		[DispId(-2147417610)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		public virtual extern object getAttribute([MarshalAs(19)] [In] string strAttributeName, [In] int lFlags = 0);

		// Token: 0x06008EFD RID: 36605
		[DispId(-2147417609)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool removeAttribute([MarshalAs(19)] [In] string strAttributeName, [In] int lFlags = 1);

		// Token: 0x17002FAB RID: 12203
		// (get) Token: 0x06008EFF RID: 36607
		// (set) Token: 0x06008EFE RID: 36606
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

		// Token: 0x17002FAC RID: 12204
		// (get) Token: 0x06008F01 RID: 36609
		// (set) Token: 0x06008F00 RID: 36608
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

		// Token: 0x17002FAD RID: 12205
		// (get) Token: 0x06008F02 RID: 36610
		[DispId(-2147417108)]
		public virtual extern string tagName
		{
			[DispId(-2147417108)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17002FAE RID: 12206
		// (get) Token: 0x06008F03 RID: 36611
		[DispId(-2147418104)]
		public virtual extern IHTMLElement parentElement
		{
			[DispId(-2147418104)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17002FAF RID: 12207
		// (get) Token: 0x06008F04 RID: 36612
		[DispId(-2147418038)]
		public virtual extern IHTMLStyle style
		{
			[TypeLibFunc(1024)]
			[DispId(-2147418038)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17002FB0 RID: 12208
		// (get) Token: 0x06008F06 RID: 36614
		// (set) Token: 0x06008F05 RID: 36613
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

		// Token: 0x17002FB1 RID: 12209
		// (get) Token: 0x06008F08 RID: 36616
		// (set) Token: 0x06008F07 RID: 36615
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

		// Token: 0x17002FB2 RID: 12210
		// (get) Token: 0x06008F0A RID: 36618
		// (set) Token: 0x06008F09 RID: 36617
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

		// Token: 0x17002FB3 RID: 12211
		// (get) Token: 0x06008F0C RID: 36620
		// (set) Token: 0x06008F0B RID: 36619
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

		// Token: 0x17002FB4 RID: 12212
		// (get) Token: 0x06008F0E RID: 36622
		// (set) Token: 0x06008F0D RID: 36621
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

		// Token: 0x17002FB5 RID: 12213
		// (get) Token: 0x06008F10 RID: 36624
		// (set) Token: 0x06008F0F RID: 36623
		[DispId(-2147412105)]
		public virtual extern object onkeypress
		{
			[DispId(-2147412105)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412105)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17002FB6 RID: 12214
		// (get) Token: 0x06008F12 RID: 36626
		// (set) Token: 0x06008F11 RID: 36625
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

		// Token: 0x17002FB7 RID: 12215
		// (get) Token: 0x06008F14 RID: 36628
		// (set) Token: 0x06008F13 RID: 36627
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

		// Token: 0x17002FB8 RID: 12216
		// (get) Token: 0x06008F16 RID: 36630
		// (set) Token: 0x06008F15 RID: 36629
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

		// Token: 0x17002FB9 RID: 12217
		// (get) Token: 0x06008F18 RID: 36632
		// (set) Token: 0x06008F17 RID: 36631
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

		// Token: 0x17002FBA RID: 12218
		// (get) Token: 0x06008F1A RID: 36634
		// (set) Token: 0x06008F19 RID: 36633
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

		// Token: 0x17002FBB RID: 12219
		// (get) Token: 0x06008F1B RID: 36635
		[DispId(-2147417094)]
		public virtual extern object document
		{
			[DispId(-2147417094)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17002FBC RID: 12220
		// (get) Token: 0x06008F1D RID: 36637
		// (set) Token: 0x06008F1C RID: 36636
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

		// Token: 0x17002FBD RID: 12221
		// (get) Token: 0x06008F1F RID: 36639
		// (set) Token: 0x06008F1E RID: 36638
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

		// Token: 0x17002FBE RID: 12222
		// (get) Token: 0x06008F21 RID: 36641
		// (set) Token: 0x06008F20 RID: 36640
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

		// Token: 0x06008F22 RID: 36642
		[DispId(-2147417093)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void scrollIntoView([MarshalAs(27)] [In] [Optional] object varargStart);

		// Token: 0x06008F23 RID: 36643
		[DispId(-2147417092)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool contains([MarshalAs(28)] [In] IHTMLElement pChild);

		// Token: 0x17002FBF RID: 12223
		// (get) Token: 0x06008F24 RID: 36644
		[DispId(-2147417088)]
		public virtual extern int sourceIndex
		{
			[DispId(-2147417088)]
			[TypeLibFunc(4)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17002FC0 RID: 12224
		// (get) Token: 0x06008F25 RID: 36645
		[DispId(-2147417087)]
		public virtual extern object recordNumber
		{
			[DispId(-2147417087)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x17002FC1 RID: 12225
		// (get) Token: 0x06008F27 RID: 36647
		// (set) Token: 0x06008F26 RID: 36646
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

		// Token: 0x17002FC2 RID: 12226
		// (get) Token: 0x06008F28 RID: 36648
		[DispId(-2147417104)]
		public virtual extern int offsetLeft
		{
			[DispId(-2147417104)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17002FC3 RID: 12227
		// (get) Token: 0x06008F29 RID: 36649
		[DispId(-2147417103)]
		public virtual extern int offsetTop
		{
			[DispId(-2147417103)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17002FC4 RID: 12228
		// (get) Token: 0x06008F2A RID: 36650
		[DispId(-2147417102)]
		public virtual extern int offsetWidth
		{
			[DispId(-2147417102)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17002FC5 RID: 12229
		// (get) Token: 0x06008F2B RID: 36651
		[DispId(-2147417101)]
		public virtual extern int offsetHeight
		{
			[DispId(-2147417101)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17002FC6 RID: 12230
		// (get) Token: 0x06008F2C RID: 36652
		[DispId(-2147417100)]
		public virtual extern IHTMLElement offsetParent
		{
			[DispId(-2147417100)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17002FC7 RID: 12231
		// (get) Token: 0x06008F2E RID: 36654
		// (set) Token: 0x06008F2D RID: 36653
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

		// Token: 0x17002FC8 RID: 12232
		// (get) Token: 0x06008F30 RID: 36656
		// (set) Token: 0x06008F2F RID: 36655
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

		// Token: 0x17002FC9 RID: 12233
		// (get) Token: 0x06008F32 RID: 36658
		// (set) Token: 0x06008F31 RID: 36657
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

		// Token: 0x17002FCA RID: 12234
		// (get) Token: 0x06008F34 RID: 36660
		// (set) Token: 0x06008F33 RID: 36659
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

		// Token: 0x06008F35 RID: 36661
		[DispId(-2147417082)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void insertAdjacentHTML([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string html);

		// Token: 0x06008F36 RID: 36662
		[DispId(-2147417081)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void insertAdjacentText([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string text);

		// Token: 0x17002FCB RID: 12235
		// (get) Token: 0x06008F37 RID: 36663
		[DispId(-2147417080)]
		public virtual extern IHTMLElement parentTextEdit
		{
			[DispId(-2147417080)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17002FCC RID: 12236
		// (get) Token: 0x06008F38 RID: 36664
		[DispId(-2147417078)]
		public virtual extern bool isTextEdit
		{
			[DispId(-2147417078)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x06008F39 RID: 36665
		[DispId(-2147417079)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void click();

		// Token: 0x17002FCD RID: 12237
		// (get) Token: 0x06008F3A RID: 36666
		[DispId(-2147417077)]
		public virtual extern IHTMLFiltersCollection filters
		{
			[DispId(-2147417077)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17002FCE RID: 12238
		// (get) Token: 0x06008F3C RID: 36668
		// (set) Token: 0x06008F3B RID: 36667
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

		// Token: 0x06008F3D RID: 36669
		[DispId(-2147417076)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string toString();

		// Token: 0x17002FCF RID: 12239
		// (get) Token: 0x06008F3F RID: 36671
		// (set) Token: 0x06008F3E RID: 36670
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

		// Token: 0x17002FD0 RID: 12240
		// (get) Token: 0x06008F41 RID: 36673
		// (set) Token: 0x06008F40 RID: 36672
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

		// Token: 0x17002FD1 RID: 12241
		// (get) Token: 0x06008F43 RID: 36675
		// (set) Token: 0x06008F42 RID: 36674
		[DispId(-2147412074)]
		public virtual extern object onerrorupdate
		{
			[TypeLibFunc(20)]
			[DispId(-2147412074)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412074)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17002FD2 RID: 12242
		// (get) Token: 0x06008F45 RID: 36677
		// (set) Token: 0x06008F44 RID: 36676
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

		// Token: 0x17002FD3 RID: 12243
		// (get) Token: 0x06008F47 RID: 36679
		// (set) Token: 0x06008F46 RID: 36678
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

		// Token: 0x17002FD4 RID: 12244
		// (get) Token: 0x06008F49 RID: 36681
		// (set) Token: 0x06008F48 RID: 36680
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

		// Token: 0x17002FD5 RID: 12245
		// (get) Token: 0x06008F4B RID: 36683
		// (set) Token: 0x06008F4A RID: 36682
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

		// Token: 0x17002FD6 RID: 12246
		// (get) Token: 0x06008F4D RID: 36685
		// (set) Token: 0x06008F4C RID: 36684
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

		// Token: 0x17002FD7 RID: 12247
		// (get) Token: 0x06008F4F RID: 36687
		// (set) Token: 0x06008F4E RID: 36686
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

		// Token: 0x17002FD8 RID: 12248
		// (get) Token: 0x06008F50 RID: 36688
		[DispId(-2147417075)]
		public virtual extern object children
		{
			[DispId(-2147417075)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17002FD9 RID: 12249
		// (get) Token: 0x06008F51 RID: 36689
		[DispId(-2147417074)]
		public virtual extern object all
		{
			[DispId(-2147417074)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17002FDA RID: 12250
		// (get) Token: 0x06008F52 RID: 36690
		[DispId(-2147417073)]
		public virtual extern string scopeName
		{
			[DispId(-2147417073)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x06008F53 RID: 36691
		[DispId(-2147417072)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void setCapture([In] bool containerCapture = true);

		// Token: 0x06008F54 RID: 36692
		[DispId(-2147417071)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void releaseCapture();

		// Token: 0x17002FDB RID: 12251
		// (get) Token: 0x06008F56 RID: 36694
		// (set) Token: 0x06008F55 RID: 36693
		[DispId(-2147412066)]
		public virtual extern object onlosecapture
		{
			[TypeLibFunc(20)]
			[DispId(-2147412066)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412066)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x06008F57 RID: 36695
		[DispId(-2147417070)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string componentFromPoint([In] int x, [In] int y);

		// Token: 0x06008F58 RID: 36696
		[DispId(-2147417069)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void doScroll([MarshalAs(27)] [In] [Optional] object component);

		// Token: 0x17002FDC RID: 12252
		// (get) Token: 0x06008F5A RID: 36698
		// (set) Token: 0x06008F59 RID: 36697
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

		// Token: 0x17002FDD RID: 12253
		// (get) Token: 0x06008F5C RID: 36700
		// (set) Token: 0x06008F5B RID: 36699
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

		// Token: 0x17002FDE RID: 12254
		// (get) Token: 0x06008F5E RID: 36702
		// (set) Token: 0x06008F5D RID: 36701
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

		// Token: 0x17002FDF RID: 12255
		// (get) Token: 0x06008F60 RID: 36704
		// (set) Token: 0x06008F5F RID: 36703
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

		// Token: 0x17002FE0 RID: 12256
		// (get) Token: 0x06008F62 RID: 36706
		// (set) Token: 0x06008F61 RID: 36705
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

		// Token: 0x17002FE1 RID: 12257
		// (get) Token: 0x06008F64 RID: 36708
		// (set) Token: 0x06008F63 RID: 36707
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

		// Token: 0x17002FE2 RID: 12258
		// (get) Token: 0x06008F66 RID: 36710
		// (set) Token: 0x06008F65 RID: 36709
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

		// Token: 0x17002FE3 RID: 12259
		// (get) Token: 0x06008F68 RID: 36712
		// (set) Token: 0x06008F67 RID: 36711
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

		// Token: 0x17002FE4 RID: 12260
		// (get) Token: 0x06008F6A RID: 36714
		// (set) Token: 0x06008F69 RID: 36713
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

		// Token: 0x17002FE5 RID: 12261
		// (get) Token: 0x06008F6C RID: 36716
		// (set) Token: 0x06008F6B RID: 36715
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

		// Token: 0x17002FE6 RID: 12262
		// (get) Token: 0x06008F6E RID: 36718
		// (set) Token: 0x06008F6D RID: 36717
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

		// Token: 0x17002FE7 RID: 12263
		// (get) Token: 0x06008F70 RID: 36720
		// (set) Token: 0x06008F6F RID: 36719
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

		// Token: 0x17002FE8 RID: 12264
		// (get) Token: 0x06008F72 RID: 36722
		// (set) Token: 0x06008F71 RID: 36721
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

		// Token: 0x17002FE9 RID: 12265
		// (get) Token: 0x06008F73 RID: 36723
		[DispId(-2147417105)]
		public virtual extern IHTMLCurrentStyle currentStyle
		{
			[DispId(-2147417105)]
			[TypeLibFunc(1024)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17002FEA RID: 12266
		// (get) Token: 0x06008F75 RID: 36725
		// (set) Token: 0x06008F74 RID: 36724
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

		// Token: 0x06008F76 RID: 36726
		[DispId(-2147417068)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLRectCollection getClientRects();

		// Token: 0x06008F77 RID: 36727
		[DispId(-2147417067)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLRect getBoundingClientRect();

		// Token: 0x06008F78 RID: 36728
		[DispId(-2147417608)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void setExpression([MarshalAs(19)] [In] string propname, [MarshalAs(19)] [In] string expression, [MarshalAs(19)] [In] string language = "");

		// Token: 0x06008F79 RID: 36729
		[DispId(-2147417607)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		public virtual extern object getExpression([MarshalAs(19)] [In] string propname);

		// Token: 0x06008F7A RID: 36730
		[DispId(-2147417606)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool removeExpression([MarshalAs(19)] [In] string propname);

		// Token: 0x17002FEB RID: 12267
		// (get) Token: 0x06008F7C RID: 36732
		// (set) Token: 0x06008F7B RID: 36731
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

		// Token: 0x06008F7D RID: 36733
		[DispId(-2147416112)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void focus();

		// Token: 0x17002FEC RID: 12268
		// (get) Token: 0x06008F7F RID: 36735
		// (set) Token: 0x06008F7E RID: 36734
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

		// Token: 0x17002FED RID: 12269
		// (get) Token: 0x06008F81 RID: 36737
		// (set) Token: 0x06008F80 RID: 36736
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

		// Token: 0x17002FEE RID: 12270
		// (get) Token: 0x06008F83 RID: 36739
		// (set) Token: 0x06008F82 RID: 36738
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

		// Token: 0x17002FEF RID: 12271
		// (get) Token: 0x06008F85 RID: 36741
		// (set) Token: 0x06008F84 RID: 36740
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

		// Token: 0x06008F86 RID: 36742
		[DispId(-2147416110)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void blur();

		// Token: 0x06008F87 RID: 36743
		[DispId(-2147416095)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void addFilter([MarshalAs(25)] [In] object pUnk);

		// Token: 0x06008F88 RID: 36744
		[DispId(-2147416094)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void removeFilter([MarshalAs(25)] [In] object pUnk);

		// Token: 0x17002FF0 RID: 12272
		// (get) Token: 0x06008F89 RID: 36745
		[DispId(-2147416093)]
		public virtual extern int clientHeight
		{
			[DispId(-2147416093)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17002FF1 RID: 12273
		// (get) Token: 0x06008F8A RID: 36746
		[DispId(-2147416092)]
		public virtual extern int clientWidth
		{
			[DispId(-2147416092)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17002FF2 RID: 12274
		// (get) Token: 0x06008F8B RID: 36747
		[DispId(-2147416091)]
		public virtual extern int clientTop
		{
			[DispId(-2147416091)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17002FF3 RID: 12275
		// (get) Token: 0x06008F8C RID: 36748
		[DispId(-2147416090)]
		public virtual extern int clientLeft
		{
			[TypeLibFunc(20)]
			[DispId(-2147416090)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x06008F8D RID: 36749
		[DispId(-2147417605)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool attachEvent([MarshalAs(19)] [In] string @event, [MarshalAs(26)] [In] object pdisp);

		// Token: 0x06008F8E RID: 36750
		[DispId(-2147417604)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void detachEvent([MarshalAs(19)] [In] string @event, [MarshalAs(26)] [In] object pdisp);

		// Token: 0x17002FF4 RID: 12276
		// (get) Token: 0x06008F8F RID: 36751
		[DispId(-2147412996)]
		public virtual extern object readyState
		{
			[DispId(-2147412996)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x17002FF5 RID: 12277
		// (get) Token: 0x06008F91 RID: 36753
		// (set) Token: 0x06008F90 RID: 36752
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

		// Token: 0x17002FF6 RID: 12278
		// (get) Token: 0x06008F93 RID: 36755
		// (set) Token: 0x06008F92 RID: 36754
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

		// Token: 0x17002FF7 RID: 12279
		// (get) Token: 0x06008F95 RID: 36757
		// (set) Token: 0x06008F94 RID: 36756
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

		// Token: 0x17002FF8 RID: 12280
		// (get) Token: 0x06008F97 RID: 36759
		// (set) Token: 0x06008F96 RID: 36758
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

		// Token: 0x17002FF9 RID: 12281
		// (get) Token: 0x06008F99 RID: 36761
		// (set) Token: 0x06008F98 RID: 36760
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

		// Token: 0x06008F9A RID: 36762
		[DispId(-2147417056)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(26)]
		public virtual extern object createControlRange();

		// Token: 0x17002FFA RID: 12282
		// (get) Token: 0x06008F9B RID: 36763
		[DispId(-2147417055)]
		public virtual extern int scrollHeight
		{
			[TypeLibFunc(20)]
			[DispId(-2147417055)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17002FFB RID: 12283
		// (get) Token: 0x06008F9C RID: 36764
		[DispId(-2147417054)]
		public virtual extern int scrollWidth
		{
			[TypeLibFunc(20)]
			[DispId(-2147417054)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17002FFC RID: 12284
		// (get) Token: 0x06008F9E RID: 36766
		// (set) Token: 0x06008F9D RID: 36765
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

		// Token: 0x17002FFD RID: 12285
		// (get) Token: 0x06008FA0 RID: 36768
		// (set) Token: 0x06008F9F RID: 36767
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

		// Token: 0x06008FA1 RID: 36769
		[DispId(-2147417050)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void clearAttributes();

		// Token: 0x17002FFE RID: 12286
		// (get) Token: 0x06008FA3 RID: 36771
		// (set) Token: 0x06008FA2 RID: 36770
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

		// Token: 0x06008FA4 RID: 36772
		[DispId(-2147417043)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElement insertAdjacentElement([MarshalAs(19)] [In] string where, [MarshalAs(28)] [In] IHTMLElement insertedElement);

		// Token: 0x06008FA5 RID: 36773
		[DispId(-2147417047)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElement applyElement([MarshalAs(28)] [In] IHTMLElement apply, [MarshalAs(19)] [In] string where);

		// Token: 0x06008FA6 RID: 36774
		[DispId(-2147417042)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string getAdjacentText([MarshalAs(19)] [In] string where);

		// Token: 0x06008FA7 RID: 36775
		[DispId(-2147417041)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string replaceAdjacentText([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string newText);

		// Token: 0x17002FFF RID: 12287
		// (get) Token: 0x06008FA8 RID: 36776
		[DispId(-2147417040)]
		public virtual extern bool canHaveChildren
		{
			[DispId(-2147417040)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x06008FA9 RID: 36777
		[DispId(-2147417032)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern int addBehavior([MarshalAs(19)] [In] string bstrUrl, [MarshalAs(27)] [In] [Optional] ref object pvarFactory);

		// Token: 0x06008FAA RID: 36778
		[DispId(-2147417031)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool removeBehavior([In] int cookie);

		// Token: 0x17003000 RID: 12288
		// (get) Token: 0x06008FAB RID: 36779
		[DispId(-2147417048)]
		public virtual extern IHTMLStyle runtimeStyle
		{
			[TypeLibFunc(1024)]
			[DispId(-2147417048)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17003001 RID: 12289
		// (get) Token: 0x06008FAC RID: 36780
		[DispId(-2147417030)]
		public virtual extern object behaviorUrns
		{
			[DispId(-2147417030)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17003002 RID: 12290
		// (get) Token: 0x06008FAE RID: 36782
		// (set) Token: 0x06008FAD RID: 36781
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

		// Token: 0x17003003 RID: 12291
		// (get) Token: 0x06008FB0 RID: 36784
		// (set) Token: 0x06008FAF RID: 36783
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

		// Token: 0x17003004 RID: 12292
		// (get) Token: 0x06008FB1 RID: 36785
		[DispId(-2147417028)]
		public virtual extern int readyStateValue
		{
			[DispId(-2147417028)]
			[TypeLibFunc(65)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x06008FB2 RID: 36786
		[DispId(-2147417027)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElementCollection getElementsByTagName([MarshalAs(19)] [In] string v);

		// Token: 0x06008FB3 RID: 36787
		[DispId(-2147417016)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void mergeAttributes([MarshalAs(28)] [In] IHTMLElement mergeThis, [MarshalAs(27)] [In] [Optional] ref object pvarFlags);

		// Token: 0x17003005 RID: 12293
		// (get) Token: 0x06008FB4 RID: 36788
		[DispId(-2147417015)]
		public virtual extern bool isMultiLine
		{
			[DispId(-2147417015)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17003006 RID: 12294
		// (get) Token: 0x06008FB5 RID: 36789
		[DispId(-2147417014)]
		public virtual extern bool canHaveHTML
		{
			[DispId(-2147417014)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17003007 RID: 12295
		// (get) Token: 0x06008FB7 RID: 36791
		// (set) Token: 0x06008FB6 RID: 36790
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

		// Token: 0x17003008 RID: 12296
		// (get) Token: 0x06008FB9 RID: 36793
		// (set) Token: 0x06008FB8 RID: 36792
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

		// Token: 0x17003009 RID: 12297
		// (get) Token: 0x06008FBB RID: 36795
		// (set) Token: 0x06008FBA RID: 36794
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

		// Token: 0x1700300A RID: 12298
		// (get) Token: 0x06008FBD RID: 36797
		// (set) Token: 0x06008FBC RID: 36796
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

		// Token: 0x06008FBE RID: 36798
		[DispId(-2147417011)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void setActive();

		// Token: 0x1700300B RID: 12299
		// (get) Token: 0x06008FC0 RID: 36800
		// (set) Token: 0x06008FBF RID: 36799
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

		// Token: 0x1700300C RID: 12300
		// (get) Token: 0x06008FC1 RID: 36801
		[DispId(-2147417010)]
		public virtual extern bool isContentEditable
		{
			[DispId(-2147417010)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700300D RID: 12301
		// (get) Token: 0x06008FC3 RID: 36803
		// (set) Token: 0x06008FC2 RID: 36802
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

		// Token: 0x1700300E RID: 12302
		// (get) Token: 0x06008FC5 RID: 36805
		// (set) Token: 0x06008FC4 RID: 36804
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

		// Token: 0x1700300F RID: 12303
		// (get) Token: 0x06008FC6 RID: 36806
		[DispId(-2147417007)]
		public virtual extern bool isDisabled
		{
			[DispId(-2147417007)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17003010 RID: 12304
		// (get) Token: 0x06008FC8 RID: 36808
		// (set) Token: 0x06008FC7 RID: 36807
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

		// Token: 0x17003011 RID: 12305
		// (get) Token: 0x06008FCA RID: 36810
		// (set) Token: 0x06008FC9 RID: 36809
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

		// Token: 0x06008FCB RID: 36811
		[DispId(-2147417006)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool FireEvent([MarshalAs(19)] [In] string bstrEventName, [MarshalAs(27)] [In] [Optional] ref object pvarEventObject);

		// Token: 0x17003012 RID: 12306
		// (get) Token: 0x06008FCD RID: 36813
		// (set) Token: 0x06008FCC RID: 36812
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

		// Token: 0x17003013 RID: 12307
		// (get) Token: 0x06008FCF RID: 36815
		// (set) Token: 0x06008FCE RID: 36814
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

		// Token: 0x17003014 RID: 12308
		// (get) Token: 0x06008FD1 RID: 36817
		// (set) Token: 0x06008FD0 RID: 36816
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

		// Token: 0x17003015 RID: 12309
		// (get) Token: 0x06008FD3 RID: 36819
		// (set) Token: 0x06008FD2 RID: 36818
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

		// Token: 0x17003016 RID: 12310
		// (get) Token: 0x06008FD5 RID: 36821
		// (set) Token: 0x06008FD4 RID: 36820
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

		// Token: 0x17003017 RID: 12311
		// (get) Token: 0x06008FD7 RID: 36823
		// (set) Token: 0x06008FD6 RID: 36822
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

		// Token: 0x17003018 RID: 12312
		// (get) Token: 0x06008FD9 RID: 36825
		// (set) Token: 0x06008FD8 RID: 36824
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

		// Token: 0x17003019 RID: 12313
		// (get) Token: 0x06008FDB RID: 36827
		// (set) Token: 0x06008FDA RID: 36826
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

		// Token: 0x06008FDC RID: 36828
		[DispId(-2147417005)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool dragDrop();

		// Token: 0x1700301A RID: 12314
		// (get) Token: 0x06008FDD RID: 36829
		[DispId(-2147417004)]
		public virtual extern int glyphMode
		{
			[TypeLibFunc(1089)]
			[DispId(-2147417004)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700301B RID: 12315
		// (get) Token: 0x06008FDF RID: 36831
		// (set) Token: 0x06008FDE RID: 36830
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

		// Token: 0x06008FE0 RID: 36832
		[DispId(-2147417000)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void normalize();

		// Token: 0x06008FE1 RID: 36833
		[DispId(-2147417003)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute getAttributeNode([MarshalAs(19)] [In] string bstrName);

		// Token: 0x06008FE2 RID: 36834
		[DispId(-2147417002)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute setAttributeNode([MarshalAs(28)] [In] IHTMLDOMAttribute pattr);

		// Token: 0x06008FE3 RID: 36835
		[DispId(-2147417001)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute removeAttributeNode([MarshalAs(28)] [In] IHTMLDOMAttribute pattr);

		// Token: 0x1700301C RID: 12316
		// (get) Token: 0x06008FE5 RID: 36837
		// (set) Token: 0x06008FE4 RID: 36836
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

		// Token: 0x1700301D RID: 12317
		// (get) Token: 0x06008FE7 RID: 36839
		// (set) Token: 0x06008FE6 RID: 36838
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

		// Token: 0x1700301E RID: 12318
		// (get) Token: 0x06008FE9 RID: 36841
		// (set) Token: 0x06008FE8 RID: 36840
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

		// Token: 0x1700301F RID: 12319
		// (get) Token: 0x06008FEA RID: 36842
		[DispId(-2147417058)]
		public virtual extern int uniqueNumber
		{
			[DispId(-2147417058)]
			[TypeLibFunc(64)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17003020 RID: 12320
		// (get) Token: 0x06008FEB RID: 36843
		[DispId(-2147417057)]
		public virtual extern string uniqueID
		{
			[DispId(-2147417057)]
			[TypeLibFunc(64)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17003021 RID: 12321
		// (get) Token: 0x06008FEC RID: 36844
		[DispId(-2147417066)]
		public virtual extern int nodeType
		{
			[DispId(-2147417066)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17003022 RID: 12322
		// (get) Token: 0x06008FED RID: 36845
		[DispId(-2147417065)]
		public virtual extern IHTMLDOMNode parentNode
		{
			[DispId(-2147417065)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x06008FEE RID: 36846
		[DispId(-2147417064)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool hasChildNodes();

		// Token: 0x17003023 RID: 12323
		// (get) Token: 0x06008FEF RID: 36847
		[DispId(-2147417063)]
		public virtual extern object childNodes
		{
			[DispId(-2147417063)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17003024 RID: 12324
		// (get) Token: 0x06008FF0 RID: 36848
		[DispId(-2147417062)]
		public virtual extern object attributes
		{
			[DispId(-2147417062)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x06008FF1 RID: 36849
		[DispId(-2147417061)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode insertBefore([MarshalAs(28)] [In] IHTMLDOMNode newChild, [MarshalAs(27)] [In] [Optional] object refChild);

		// Token: 0x06008FF2 RID: 36850
		[DispId(-2147417060)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode removeChild([MarshalAs(28)] [In] IHTMLDOMNode oldChild);

		// Token: 0x06008FF3 RID: 36851
		[DispId(-2147417059)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode replaceChild([MarshalAs(28)] [In] IHTMLDOMNode newChild, [MarshalAs(28)] [In] IHTMLDOMNode oldChild);

		// Token: 0x06008FF4 RID: 36852
		[DispId(-2147417051)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode cloneNode([In] bool fDeep);

		// Token: 0x06008FF5 RID: 36853
		[DispId(-2147417046)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode removeNode([In] bool fDeep = false);

		// Token: 0x06008FF6 RID: 36854
		[DispId(-2147417044)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode swapNode([MarshalAs(28)] [In] IHTMLDOMNode otherNode);

		// Token: 0x06008FF7 RID: 36855
		[DispId(-2147417045)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode replaceNode([MarshalAs(28)] [In] IHTMLDOMNode replacement);

		// Token: 0x06008FF8 RID: 36856
		[DispId(-2147417039)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode appendChild([MarshalAs(28)] [In] IHTMLDOMNode newChild);

		// Token: 0x17003025 RID: 12325
		// (get) Token: 0x06008FF9 RID: 36857
		[DispId(-2147417038)]
		public virtual extern string nodeName
		{
			[DispId(-2147417038)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17003026 RID: 12326
		// (get) Token: 0x06008FFB RID: 36859
		// (set) Token: 0x06008FFA RID: 36858
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

		// Token: 0x17003027 RID: 12327
		// (get) Token: 0x06008FFC RID: 36860
		[DispId(-2147417036)]
		public virtual extern IHTMLDOMNode firstChild
		{
			[DispId(-2147417036)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17003028 RID: 12328
		// (get) Token: 0x06008FFD RID: 36861
		[DispId(-2147417035)]
		public virtual extern IHTMLDOMNode lastChild
		{
			[DispId(-2147417035)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17003029 RID: 12329
		// (get) Token: 0x06008FFE RID: 36862
		[DispId(-2147417034)]
		public virtual extern IHTMLDOMNode previousSibling
		{
			[DispId(-2147417034)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x1700302A RID: 12330
		// (get) Token: 0x06008FFF RID: 36863
		[DispId(-2147417033)]
		public virtual extern IHTMLDOMNode nextSibling
		{
			[DispId(-2147417033)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x1700302B RID: 12331
		// (get) Token: 0x06009000 RID: 36864
		[DispId(-2147416999)]
		public virtual extern object ownerDocument
		{
			[DispId(-2147416999)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x1700302C RID: 12332
		// (get) Token: 0x06009002 RID: 36866
		// (set) Token: 0x06009001 RID: 36865
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

		// Token: 0x1700302D RID: 12333
		// (get) Token: 0x06009004 RID: 36868
		// (set) Token: 0x06009003 RID: 36867
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

		// Token: 0x1700302E RID: 12334
		// (get) Token: 0x06009006 RID: 36870
		// (set) Token: 0x06009005 RID: 36869
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

		// Token: 0x1700302F RID: 12335
		// (get) Token: 0x06009008 RID: 36872
		// (set) Token: 0x06009007 RID: 36871
		[DispId(2000)]
		public virtual extern string type
		{
			[DispId(2000)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(2000)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17003030 RID: 12336
		// (get) Token: 0x0600900A RID: 36874
		// (set) Token: 0x06009009 RID: 36873
		[DispId(-2147413011)]
		public virtual extern string value
		{
			[TypeLibFunc(20)]
			[DispId(-2147413011)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147413011)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17003031 RID: 12337
		// (get) Token: 0x0600900C RID: 36876
		// (set) Token: 0x0600900B RID: 36875
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

		// Token: 0x17003032 RID: 12338
		// (get) Token: 0x0600900E RID: 36878
		// (set) Token: 0x0600900D RID: 36877
		[DispId(2001)]
		public virtual extern bool status
		{
			[DispId(2001)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[DispId(2001)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x17003033 RID: 12339
		// (get) Token: 0x0600900F RID: 36879
		[DispId(-2147416108)]
		public virtual extern IHTMLFormElement form
		{
			[DispId(-2147416108)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17003034 RID: 12340
		// (get) Token: 0x06009011 RID: 36881
		// (set) Token: 0x06009010 RID: 36880
		[DispId(2002)]
		public virtual extern int size
		{
			[TypeLibFunc(20)]
			[DispId(2002)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[TypeLibFunc(20)]
			[DispId(2002)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x17003035 RID: 12341
		// (get) Token: 0x06009013 RID: 36883
		// (set) Token: 0x06009012 RID: 36882
		[DispId(2003)]
		public virtual extern int maxLength
		{
			[TypeLibFunc(20)]
			[DispId(2003)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[DispId(2003)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x06009014 RID: 36884
		[DispId(2004)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void select();

		// Token: 0x17003036 RID: 12342
		// (get) Token: 0x06009016 RID: 36886
		// (set) Token: 0x06009015 RID: 36885
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

		// Token: 0x17003037 RID: 12343
		// (get) Token: 0x06009018 RID: 36888
		// (set) Token: 0x06009017 RID: 36887
		[DispId(-2147412102)]
		public virtual extern object onselect
		{
			[TypeLibFunc(20)]
			[DispId(-2147412102)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412102)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17003038 RID: 12344
		// (get) Token: 0x0600901A RID: 36890
		// (set) Token: 0x06009019 RID: 36889
		[DispId(-2147413029)]
		public virtual extern string defaultValue
		{
			[TypeLibFunc(84)]
			[DispId(-2147413029)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(84)]
			[DispId(-2147413029)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17003039 RID: 12345
		// (get) Token: 0x0600901C RID: 36892
		// (set) Token: 0x0600901B RID: 36891
		[DispId(2005)]
		public virtual extern bool readOnly
		{
			[TypeLibFunc(20)]
			[DispId(2005)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[TypeLibFunc(20)]
			[DispId(2005)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x0600901D RID: 36893
		[DispId(2006)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLTxtRange createTextRange();

		// Token: 0x1700303A RID: 12346
		// (get) Token: 0x0600901F RID: 36895
		// (set) Token: 0x0600901E RID: 36894
		[DispId(2007)]
		public virtual extern bool indeterminate
		{
			[TypeLibFunc(4)]
			[DispId(2007)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[DispId(2007)]
			[TypeLibFunc(4)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x1700303B RID: 12347
		// (get) Token: 0x06009021 RID: 36897
		// (set) Token: 0x06009020 RID: 36896
		[DispId(2008)]
		public virtual extern bool defaultChecked
		{
			[TypeLibFunc(4)]
			[DispId(2008)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[TypeLibFunc(4)]
			[DispId(2008)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x1700303C RID: 12348
		// (get) Token: 0x06009023 RID: 36899
		// (set) Token: 0x06009022 RID: 36898
		[DispId(2009)]
		public virtual extern bool @checked
		{
			[TypeLibFunc(4)]
			[DispId(2009)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[TypeLibFunc(4)]
			[DispId(2009)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x1700303D RID: 12349
		// (get) Token: 0x06009025 RID: 36901
		// (set) Token: 0x06009024 RID: 36900
		[DispId(2012)]
		public virtual extern object border
		{
			[TypeLibFunc(20)]
			[DispId(2012)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(2012)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x1700303E RID: 12350
		// (get) Token: 0x06009027 RID: 36903
		// (set) Token: 0x06009026 RID: 36902
		[DispId(2013)]
		public virtual extern int vspace
		{
			[TypeLibFunc(20)]
			[DispId(2013)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[TypeLibFunc(20)]
			[DispId(2013)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x1700303F RID: 12351
		// (get) Token: 0x06009029 RID: 36905
		// (set) Token: 0x06009028 RID: 36904
		[DispId(2014)]
		public virtual extern int hspace
		{
			[DispId(2014)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[TypeLibFunc(20)]
			[DispId(2014)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x17003040 RID: 12352
		// (get) Token: 0x0600902B RID: 36907
		// (set) Token: 0x0600902A RID: 36906
		[DispId(2010)]
		public virtual extern string alt
		{
			[TypeLibFunc(20)]
			[DispId(2010)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(2010)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17003041 RID: 12353
		// (get) Token: 0x0600902D RID: 36909
		// (set) Token: 0x0600902C RID: 36908
		[DispId(2011)]
		public virtual extern string src
		{
			[TypeLibFunc(20)]
			[DispId(2011)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(2011)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17003042 RID: 12354
		// (get) Token: 0x0600902F RID: 36911
		// (set) Token: 0x0600902E RID: 36910
		[DispId(2015)]
		public virtual extern string lowsrc
		{
			[TypeLibFunc(20)]
			[DispId(2015)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(2015)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17003043 RID: 12355
		// (get) Token: 0x06009031 RID: 36913
		// (set) Token: 0x06009030 RID: 36912
		[DispId(2016)]
		public virtual extern string vrml
		{
			[TypeLibFunc(20)]
			[DispId(2016)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(2016)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17003044 RID: 12356
		// (get) Token: 0x06009033 RID: 36915
		// (set) Token: 0x06009032 RID: 36914
		[DispId(2017)]
		public virtual extern string dynsrc
		{
			[TypeLibFunc(20)]
			[DispId(2017)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(2017)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17003045 RID: 12357
		// (get) Token: 0x06009034 RID: 36916
		[DispId(2018)]
		public virtual extern bool complete
		{
			[DispId(2018)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17003046 RID: 12358
		// (get) Token: 0x06009036 RID: 36918
		// (set) Token: 0x06009035 RID: 36917
		[DispId(2019)]
		public virtual extern object loop
		{
			[TypeLibFunc(20)]
			[DispId(2019)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(2019)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17003047 RID: 12359
		// (get) Token: 0x06009038 RID: 36920
		// (set) Token: 0x06009037 RID: 36919
		[DispId(-2147418039)]
		public virtual extern string align
		{
			[TypeLibFunc(20)]
			[DispId(-2147418039)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147418039)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17003048 RID: 12360
		// (get) Token: 0x0600903A RID: 36922
		// (set) Token: 0x06009039 RID: 36921
		[DispId(-2147412080)]
		public virtual extern object onload
		{
			[TypeLibFunc(20)]
			[DispId(-2147412080)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412080)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17003049 RID: 12361
		// (get) Token: 0x0600903C RID: 36924
		// (set) Token: 0x0600903B RID: 36923
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

		// Token: 0x1700304A RID: 12362
		// (get) Token: 0x0600903E RID: 36926
		// (set) Token: 0x0600903D RID: 36925
		[DispId(-2147412084)]
		public virtual extern object onabort
		{
			[TypeLibFunc(20)]
			[DispId(-2147412084)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412084)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x1700304B RID: 12363
		// (get) Token: 0x06009040 RID: 36928
		// (set) Token: 0x0600903F RID: 36927
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

		// Token: 0x1700304C RID: 12364
		// (get) Token: 0x06009042 RID: 36930
		// (set) Token: 0x06009041 RID: 36929
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

		// Token: 0x1700304D RID: 12365
		// (get) Token: 0x06009044 RID: 36932
		// (set) Token: 0x06009043 RID: 36931
		[DispId(2020)]
		public virtual extern string Start
		{
			[TypeLibFunc(20)]
			[DispId(2020)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(2020)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x1700304E RID: 12366
		// (get) Token: 0x06009046 RID: 36934
		// (set) Token: 0x06009045 RID: 36933
		[DispId(2022)]
		public virtual extern string accept
		{
			[TypeLibFunc(20)]
			[DispId(2022)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(2022)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x1700304F RID: 12367
		// (get) Token: 0x06009048 RID: 36936
		// (set) Token: 0x06009047 RID: 36935
		[DispId(2023)]
		public virtual extern string useMap
		{
			[DispId(2023)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(2023)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x06009049 RID: 36937
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement_setAttribute([MarshalAs(19)] [In] string strAttributeName, [MarshalAs(27)] [In] object AttributeValue, [In] int lFlags = 1);

		// Token: 0x0600904A RID: 36938
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		public virtual extern object IHTMLElement_getAttribute([MarshalAs(19)] [In] string strAttributeName, [In] int lFlags = 0);

		// Token: 0x0600904B RID: 36939
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement_removeAttribute([MarshalAs(19)] [In] string strAttributeName, [In] int lFlags = 1);

		// Token: 0x17003050 RID: 12368
		// (get) Token: 0x0600904D RID: 36941
		// (set) Token: 0x0600904C RID: 36940
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

		// Token: 0x17003051 RID: 12369
		// (get) Token: 0x0600904F RID: 36943
		// (set) Token: 0x0600904E RID: 36942
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

		// Token: 0x17003052 RID: 12370
		// (get) Token: 0x06009050 RID: 36944
		public virtual extern string IHTMLElement_tagName
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17003053 RID: 12371
		// (get) Token: 0x06009051 RID: 36945
		public virtual extern IHTMLElement IHTMLElement_parentElement
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17003054 RID: 12372
		// (get) Token: 0x06009052 RID: 36946
		public virtual extern IHTMLStyle IHTMLElement_style
		{
			[TypeLibFunc(1024)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17003055 RID: 12373
		// (get) Token: 0x06009054 RID: 36948
		// (set) Token: 0x06009053 RID: 36947
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

		// Token: 0x17003056 RID: 12374
		// (get) Token: 0x06009056 RID: 36950
		// (set) Token: 0x06009055 RID: 36949
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

		// Token: 0x17003057 RID: 12375
		// (get) Token: 0x06009058 RID: 36952
		// (set) Token: 0x06009057 RID: 36951
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

		// Token: 0x17003058 RID: 12376
		// (get) Token: 0x0600905A RID: 36954
		// (set) Token: 0x06009059 RID: 36953
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

		// Token: 0x17003059 RID: 12377
		// (get) Token: 0x0600905C RID: 36956
		// (set) Token: 0x0600905B RID: 36955
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

		// Token: 0x1700305A RID: 12378
		// (get) Token: 0x0600905E RID: 36958
		// (set) Token: 0x0600905D RID: 36957
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

		// Token: 0x1700305B RID: 12379
		// (get) Token: 0x06009060 RID: 36960
		// (set) Token: 0x0600905F RID: 36959
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

		// Token: 0x1700305C RID: 12380
		// (get) Token: 0x06009062 RID: 36962
		// (set) Token: 0x06009061 RID: 36961
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

		// Token: 0x1700305D RID: 12381
		// (get) Token: 0x06009064 RID: 36964
		// (set) Token: 0x06009063 RID: 36963
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

		// Token: 0x1700305E RID: 12382
		// (get) Token: 0x06009066 RID: 36966
		// (set) Token: 0x06009065 RID: 36965
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

		// Token: 0x1700305F RID: 12383
		// (get) Token: 0x06009068 RID: 36968
		// (set) Token: 0x06009067 RID: 36967
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

		// Token: 0x17003060 RID: 12384
		// (get) Token: 0x06009069 RID: 36969
		public virtual extern object IHTMLElement_document
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17003061 RID: 12385
		// (get) Token: 0x0600906B RID: 36971
		// (set) Token: 0x0600906A RID: 36970
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

		// Token: 0x17003062 RID: 12386
		// (get) Token: 0x0600906D RID: 36973
		// (set) Token: 0x0600906C RID: 36972
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

		// Token: 0x17003063 RID: 12387
		// (get) Token: 0x0600906F RID: 36975
		// (set) Token: 0x0600906E RID: 36974
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

		// Token: 0x06009070 RID: 36976
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement_scrollIntoView([MarshalAs(27)] [In] [Optional] object varargStart);

		// Token: 0x06009071 RID: 36977
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement_contains([MarshalAs(28)] [In] IHTMLElement pChild);

		// Token: 0x17003064 RID: 12388
		// (get) Token: 0x06009072 RID: 36978
		public virtual extern int IHTMLElement_sourceIndex
		{
			[TypeLibFunc(4)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17003065 RID: 12389
		// (get) Token: 0x06009073 RID: 36979
		public virtual extern object IHTMLElement_recordNumber
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x17003066 RID: 12390
		// (get) Token: 0x06009075 RID: 36981
		// (set) Token: 0x06009074 RID: 36980
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

		// Token: 0x17003067 RID: 12391
		// (get) Token: 0x06009076 RID: 36982
		public virtual extern int IHTMLElement_offsetLeft
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17003068 RID: 12392
		// (get) Token: 0x06009077 RID: 36983
		public virtual extern int IHTMLElement_offsetTop
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17003069 RID: 12393
		// (get) Token: 0x06009078 RID: 36984
		public virtual extern int IHTMLElement_offsetWidth
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700306A RID: 12394
		// (get) Token: 0x06009079 RID: 36985
		public virtual extern int IHTMLElement_offsetHeight
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700306B RID: 12395
		// (get) Token: 0x0600907A RID: 36986
		public virtual extern IHTMLElement IHTMLElement_offsetParent
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x1700306C RID: 12396
		// (get) Token: 0x0600907C RID: 36988
		// (set) Token: 0x0600907B RID: 36987
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

		// Token: 0x1700306D RID: 12397
		// (get) Token: 0x0600907E RID: 36990
		// (set) Token: 0x0600907D RID: 36989
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

		// Token: 0x1700306E RID: 12398
		// (get) Token: 0x06009080 RID: 36992
		// (set) Token: 0x0600907F RID: 36991
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

		// Token: 0x1700306F RID: 12399
		// (get) Token: 0x06009082 RID: 36994
		// (set) Token: 0x06009081 RID: 36993
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

		// Token: 0x06009083 RID: 36995
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement_insertAdjacentHTML([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string html);

		// Token: 0x06009084 RID: 36996
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement_insertAdjacentText([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string text);

		// Token: 0x17003070 RID: 12400
		// (get) Token: 0x06009085 RID: 36997
		public virtual extern IHTMLElement IHTMLElement_parentTextEdit
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17003071 RID: 12401
		// (get) Token: 0x06009086 RID: 36998
		public virtual extern bool IHTMLElement_isTextEdit
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x06009087 RID: 36999
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement_click();

		// Token: 0x17003072 RID: 12402
		// (get) Token: 0x06009088 RID: 37000
		public virtual extern IHTMLFiltersCollection IHTMLElement_filters
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17003073 RID: 12403
		// (get) Token: 0x0600908A RID: 37002
		// (set) Token: 0x06009089 RID: 37001
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

		// Token: 0x0600908B RID: 37003
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string IHTMLElement_toString();

		// Token: 0x17003074 RID: 12404
		// (get) Token: 0x0600908D RID: 37005
		// (set) Token: 0x0600908C RID: 37004
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

		// Token: 0x17003075 RID: 12405
		// (get) Token: 0x0600908F RID: 37007
		// (set) Token: 0x0600908E RID: 37006
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

		// Token: 0x17003076 RID: 12406
		// (get) Token: 0x06009091 RID: 37009
		// (set) Token: 0x06009090 RID: 37008
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

		// Token: 0x17003077 RID: 12407
		// (get) Token: 0x06009093 RID: 37011
		// (set) Token: 0x06009092 RID: 37010
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

		// Token: 0x17003078 RID: 12408
		// (get) Token: 0x06009095 RID: 37013
		// (set) Token: 0x06009094 RID: 37012
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

		// Token: 0x17003079 RID: 12409
		// (get) Token: 0x06009097 RID: 37015
		// (set) Token: 0x06009096 RID: 37014
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

		// Token: 0x1700307A RID: 12410
		// (get) Token: 0x06009099 RID: 37017
		// (set) Token: 0x06009098 RID: 37016
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

		// Token: 0x1700307B RID: 12411
		// (get) Token: 0x0600909B RID: 37019
		// (set) Token: 0x0600909A RID: 37018
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

		// Token: 0x1700307C RID: 12412
		// (get) Token: 0x0600909D RID: 37021
		// (set) Token: 0x0600909C RID: 37020
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

		// Token: 0x1700307D RID: 12413
		// (get) Token: 0x0600909E RID: 37022
		public virtual extern object IHTMLElement_children
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x1700307E RID: 12414
		// (get) Token: 0x0600909F RID: 37023
		public virtual extern object IHTMLElement_all
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x1700307F RID: 12415
		// (get) Token: 0x060090A0 RID: 37024
		public virtual extern string IHTMLElement2_scopeName
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x060090A1 RID: 37025
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_setCapture([In] bool containerCapture = true);

		// Token: 0x060090A2 RID: 37026
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_releaseCapture();

		// Token: 0x17003080 RID: 12416
		// (get) Token: 0x060090A4 RID: 37028
		// (set) Token: 0x060090A3 RID: 37027
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

		// Token: 0x060090A5 RID: 37029
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string IHTMLElement2_componentFromPoint([In] int x, [In] int y);

		// Token: 0x060090A6 RID: 37030
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_doScroll([MarshalAs(27)] [In] [Optional] object component);

		// Token: 0x17003081 RID: 12417
		// (get) Token: 0x060090A8 RID: 37032
		// (set) Token: 0x060090A7 RID: 37031
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

		// Token: 0x17003082 RID: 12418
		// (get) Token: 0x060090AA RID: 37034
		// (set) Token: 0x060090A9 RID: 37033
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

		// Token: 0x17003083 RID: 12419
		// (get) Token: 0x060090AC RID: 37036
		// (set) Token: 0x060090AB RID: 37035
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

		// Token: 0x17003084 RID: 12420
		// (get) Token: 0x060090AE RID: 37038
		// (set) Token: 0x060090AD RID: 37037
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

		// Token: 0x17003085 RID: 12421
		// (get) Token: 0x060090B0 RID: 37040
		// (set) Token: 0x060090AF RID: 37039
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

		// Token: 0x17003086 RID: 12422
		// (get) Token: 0x060090B2 RID: 37042
		// (set) Token: 0x060090B1 RID: 37041
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

		// Token: 0x17003087 RID: 12423
		// (get) Token: 0x060090B4 RID: 37044
		// (set) Token: 0x060090B3 RID: 37043
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

		// Token: 0x17003088 RID: 12424
		// (get) Token: 0x060090B6 RID: 37046
		// (set) Token: 0x060090B5 RID: 37045
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

		// Token: 0x17003089 RID: 12425
		// (get) Token: 0x060090B8 RID: 37048
		// (set) Token: 0x060090B7 RID: 37047
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

		// Token: 0x1700308A RID: 12426
		// (get) Token: 0x060090BA RID: 37050
		// (set) Token: 0x060090B9 RID: 37049
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

		// Token: 0x1700308B RID: 12427
		// (get) Token: 0x060090BC RID: 37052
		// (set) Token: 0x060090BB RID: 37051
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

		// Token: 0x1700308C RID: 12428
		// (get) Token: 0x060090BE RID: 37054
		// (set) Token: 0x060090BD RID: 37053
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

		// Token: 0x1700308D RID: 12429
		// (get) Token: 0x060090C0 RID: 37056
		// (set) Token: 0x060090BF RID: 37055
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

		// Token: 0x1700308E RID: 12430
		// (get) Token: 0x060090C1 RID: 37057
		public virtual extern IHTMLCurrentStyle IHTMLElement2_currentStyle
		{
			[TypeLibFunc(1024)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x1700308F RID: 12431
		// (get) Token: 0x060090C3 RID: 37059
		// (set) Token: 0x060090C2 RID: 37058
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

		// Token: 0x060090C4 RID: 37060
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLRectCollection IHTMLElement2_getClientRects();

		// Token: 0x060090C5 RID: 37061
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLRect IHTMLElement2_getBoundingClientRect();

		// Token: 0x060090C6 RID: 37062
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_setExpression([MarshalAs(19)] [In] string propname, [MarshalAs(19)] [In] string expression, [MarshalAs(19)] [In] string language = "");

		// Token: 0x060090C7 RID: 37063
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		public virtual extern object IHTMLElement2_getExpression([MarshalAs(19)] [In] string propname);

		// Token: 0x060090C8 RID: 37064
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement2_removeExpression([MarshalAs(19)] [In] string propname);

		// Token: 0x17003090 RID: 12432
		// (get) Token: 0x060090CA RID: 37066
		// (set) Token: 0x060090C9 RID: 37065
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

		// Token: 0x060090CB RID: 37067
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_focus();

		// Token: 0x17003091 RID: 12433
		// (get) Token: 0x060090CD RID: 37069
		// (set) Token: 0x060090CC RID: 37068
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

		// Token: 0x17003092 RID: 12434
		// (get) Token: 0x060090CF RID: 37071
		// (set) Token: 0x060090CE RID: 37070
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

		// Token: 0x17003093 RID: 12435
		// (get) Token: 0x060090D1 RID: 37073
		// (set) Token: 0x060090D0 RID: 37072
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

		// Token: 0x17003094 RID: 12436
		// (get) Token: 0x060090D3 RID: 37075
		// (set) Token: 0x060090D2 RID: 37074
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

		// Token: 0x060090D4 RID: 37076
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_blur();

		// Token: 0x060090D5 RID: 37077
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_addFilter([MarshalAs(25)] [In] object pUnk);

		// Token: 0x060090D6 RID: 37078
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_removeFilter([MarshalAs(25)] [In] object pUnk);

		// Token: 0x17003095 RID: 12437
		// (get) Token: 0x060090D7 RID: 37079
		public virtual extern int IHTMLElement2_clientHeight
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17003096 RID: 12438
		// (get) Token: 0x060090D8 RID: 37080
		public virtual extern int IHTMLElement2_clientWidth
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17003097 RID: 12439
		// (get) Token: 0x060090D9 RID: 37081
		public virtual extern int IHTMLElement2_clientTop
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17003098 RID: 12440
		// (get) Token: 0x060090DA RID: 37082
		public virtual extern int IHTMLElement2_clientLeft
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x060090DB RID: 37083
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement2_attachEvent([MarshalAs(19)] [In] string @event, [MarshalAs(26)] [In] object pdisp);

		// Token: 0x060090DC RID: 37084
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_detachEvent([MarshalAs(19)] [In] string @event, [MarshalAs(26)] [In] object pdisp);

		// Token: 0x17003099 RID: 12441
		// (get) Token: 0x060090DD RID: 37085
		public virtual extern object IHTMLElement2_readyState
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x1700309A RID: 12442
		// (get) Token: 0x060090DF RID: 37087
		// (set) Token: 0x060090DE RID: 37086
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

		// Token: 0x1700309B RID: 12443
		// (get) Token: 0x060090E1 RID: 37089
		// (set) Token: 0x060090E0 RID: 37088
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

		// Token: 0x1700309C RID: 12444
		// (get) Token: 0x060090E3 RID: 37091
		// (set) Token: 0x060090E2 RID: 37090
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

		// Token: 0x1700309D RID: 12445
		// (get) Token: 0x060090E5 RID: 37093
		// (set) Token: 0x060090E4 RID: 37092
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

		// Token: 0x1700309E RID: 12446
		// (get) Token: 0x060090E7 RID: 37095
		// (set) Token: 0x060090E6 RID: 37094
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

		// Token: 0x060090E8 RID: 37096
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(26)]
		public virtual extern object IHTMLElement2_createControlRange();

		// Token: 0x1700309F RID: 12447
		// (get) Token: 0x060090E9 RID: 37097
		public virtual extern int IHTMLElement2_scrollHeight
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170030A0 RID: 12448
		// (get) Token: 0x060090EA RID: 37098
		public virtual extern int IHTMLElement2_scrollWidth
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170030A1 RID: 12449
		// (get) Token: 0x060090EC RID: 37100
		// (set) Token: 0x060090EB RID: 37099
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

		// Token: 0x170030A2 RID: 12450
		// (get) Token: 0x060090EE RID: 37102
		// (set) Token: 0x060090ED RID: 37101
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

		// Token: 0x060090EF RID: 37103
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_clearAttributes();

		// Token: 0x060090F0 RID: 37104
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void mergeAttributes([MarshalAs(28)] [In] IHTMLElement mergeThis);

		// Token: 0x170030A3 RID: 12451
		// (get) Token: 0x060090F2 RID: 37106
		// (set) Token: 0x060090F1 RID: 37105
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

		// Token: 0x060090F3 RID: 37107
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElement IHTMLElement2_insertAdjacentElement([MarshalAs(19)] [In] string where, [MarshalAs(28)] [In] IHTMLElement insertedElement);

		// Token: 0x060090F4 RID: 37108
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElement IHTMLElement2_applyElement([MarshalAs(28)] [In] IHTMLElement apply, [MarshalAs(19)] [In] string where);

		// Token: 0x060090F5 RID: 37109
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string IHTMLElement2_getAdjacentText([MarshalAs(19)] [In] string where);

		// Token: 0x060090F6 RID: 37110
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string IHTMLElement2_replaceAdjacentText([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string newText);

		// Token: 0x170030A4 RID: 12452
		// (get) Token: 0x060090F7 RID: 37111
		public virtual extern bool IHTMLElement2_canHaveChildren
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x060090F8 RID: 37112
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern int IHTMLElement2_addBehavior([MarshalAs(19)] [In] string bstrUrl, [MarshalAs(27)] [In] [Optional] ref object pvarFactory);

		// Token: 0x060090F9 RID: 37113
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement2_removeBehavior([In] int cookie);

		// Token: 0x170030A5 RID: 12453
		// (get) Token: 0x060090FA RID: 37114
		public virtual extern IHTMLStyle IHTMLElement2_runtimeStyle
		{
			[TypeLibFunc(1024)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170030A6 RID: 12454
		// (get) Token: 0x060090FB RID: 37115
		public virtual extern object IHTMLElement2_behaviorUrns
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x170030A7 RID: 12455
		// (get) Token: 0x060090FD RID: 37117
		// (set) Token: 0x060090FC RID: 37116
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

		// Token: 0x170030A8 RID: 12456
		// (get) Token: 0x060090FF RID: 37119
		// (set) Token: 0x060090FE RID: 37118
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

		// Token: 0x170030A9 RID: 12457
		// (get) Token: 0x06009100 RID: 37120
		public virtual extern int IHTMLElement2_readyStateValue
		{
			[TypeLibFunc(65)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x06009101 RID: 37121
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElementCollection IHTMLElement2_getElementsByTagName([MarshalAs(19)] [In] string v);

		// Token: 0x06009102 RID: 37122
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void mergeAttributes([MarshalAs(28)] [In] IHTMLElement mergeThis, [MarshalAs(27)] [In] [Optional] ref object pvarFlags);

		// Token: 0x170030AA RID: 12458
		// (get) Token: 0x06009103 RID: 37123
		public virtual extern bool IHTMLElement3_isMultiLine
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170030AB RID: 12459
		// (get) Token: 0x06009104 RID: 37124
		public virtual extern bool IHTMLElement3_canHaveHTML
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170030AC RID: 12460
		// (get) Token: 0x06009106 RID: 37126
		// (set) Token: 0x06009105 RID: 37125
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

		// Token: 0x170030AD RID: 12461
		// (get) Token: 0x06009108 RID: 37128
		// (set) Token: 0x06009107 RID: 37127
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

		// Token: 0x170030AE RID: 12462
		// (get) Token: 0x0600910A RID: 37130
		// (set) Token: 0x06009109 RID: 37129
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

		// Token: 0x170030AF RID: 12463
		// (get) Token: 0x0600910C RID: 37132
		// (set) Token: 0x0600910B RID: 37131
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

		// Token: 0x0600910D RID: 37133
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement3_setActive();

		// Token: 0x170030B0 RID: 12464
		// (get) Token: 0x0600910F RID: 37135
		// (set) Token: 0x0600910E RID: 37134
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

		// Token: 0x170030B1 RID: 12465
		// (get) Token: 0x06009110 RID: 37136
		public virtual extern bool IHTMLElement3_isContentEditable
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170030B2 RID: 12466
		// (get) Token: 0x06009112 RID: 37138
		// (set) Token: 0x06009111 RID: 37137
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

		// Token: 0x170030B3 RID: 12467
		// (get) Token: 0x06009114 RID: 37140
		// (set) Token: 0x06009113 RID: 37139
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

		// Token: 0x170030B4 RID: 12468
		// (get) Token: 0x06009115 RID: 37141
		public virtual extern bool IHTMLElement3_isDisabled
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170030B5 RID: 12469
		// (get) Token: 0x06009117 RID: 37143
		// (set) Token: 0x06009116 RID: 37142
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

		// Token: 0x170030B6 RID: 12470
		// (get) Token: 0x06009119 RID: 37145
		// (set) Token: 0x06009118 RID: 37144
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

		// Token: 0x0600911A RID: 37146
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement3_FireEvent([MarshalAs(19)] [In] string bstrEventName, [MarshalAs(27)] [In] [Optional] ref object pvarEventObject);

		// Token: 0x170030B7 RID: 12471
		// (get) Token: 0x0600911C RID: 37148
		// (set) Token: 0x0600911B RID: 37147
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

		// Token: 0x170030B8 RID: 12472
		// (get) Token: 0x0600911E RID: 37150
		// (set) Token: 0x0600911D RID: 37149
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

		// Token: 0x170030B9 RID: 12473
		// (get) Token: 0x06009120 RID: 37152
		// (set) Token: 0x0600911F RID: 37151
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

		// Token: 0x170030BA RID: 12474
		// (get) Token: 0x06009122 RID: 37154
		// (set) Token: 0x06009121 RID: 37153
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

		// Token: 0x170030BB RID: 12475
		// (get) Token: 0x06009124 RID: 37156
		// (set) Token: 0x06009123 RID: 37155
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

		// Token: 0x170030BC RID: 12476
		// (get) Token: 0x06009126 RID: 37158
		// (set) Token: 0x06009125 RID: 37157
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

		// Token: 0x170030BD RID: 12477
		// (get) Token: 0x06009128 RID: 37160
		// (set) Token: 0x06009127 RID: 37159
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

		// Token: 0x170030BE RID: 12478
		// (get) Token: 0x0600912A RID: 37162
		// (set) Token: 0x06009129 RID: 37161
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

		// Token: 0x0600912B RID: 37163
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement3_dragDrop();

		// Token: 0x170030BF RID: 12479
		// (get) Token: 0x0600912C RID: 37164
		public virtual extern int IHTMLElement3_glyphMode
		{
			[TypeLibFunc(1089)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170030C0 RID: 12480
		// (get) Token: 0x0600912E RID: 37166
		// (set) Token: 0x0600912D RID: 37165
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

		// Token: 0x0600912F RID: 37167
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement4_normalize();

		// Token: 0x06009130 RID: 37168
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute IHTMLElement4_getAttributeNode([MarshalAs(19)] [In] string bstrName);

		// Token: 0x06009131 RID: 37169
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute IHTMLElement4_setAttributeNode([MarshalAs(28)] [In] IHTMLDOMAttribute pattr);

		// Token: 0x06009132 RID: 37170
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute IHTMLElement4_removeAttributeNode([MarshalAs(28)] [In] IHTMLDOMAttribute pattr);

		// Token: 0x170030C1 RID: 12481
		// (get) Token: 0x06009134 RID: 37172
		// (set) Token: 0x06009133 RID: 37171
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

		// Token: 0x170030C2 RID: 12482
		// (get) Token: 0x06009136 RID: 37174
		// (set) Token: 0x06009135 RID: 37173
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

		// Token: 0x170030C3 RID: 12483
		// (get) Token: 0x06009138 RID: 37176
		// (set) Token: 0x06009137 RID: 37175
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

		// Token: 0x170030C4 RID: 12484
		// (get) Token: 0x06009139 RID: 37177
		public virtual extern int IHTMLUniqueName_uniqueNumber
		{
			[TypeLibFunc(64)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170030C5 RID: 12485
		// (get) Token: 0x0600913A RID: 37178
		public virtual extern string IHTMLUniqueName_uniqueID
		{
			[TypeLibFunc(64)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170030C6 RID: 12486
		// (get) Token: 0x0600913B RID: 37179
		public virtual extern int IHTMLDOMNode_nodeType
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170030C7 RID: 12487
		// (get) Token: 0x0600913C RID: 37180
		public virtual extern IHTMLDOMNode IHTMLDOMNode_parentNode
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x0600913D RID: 37181
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLDOMNode_hasChildNodes();

		// Token: 0x170030C8 RID: 12488
		// (get) Token: 0x0600913E RID: 37182
		public virtual extern object IHTMLDOMNode_childNodes
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x170030C9 RID: 12489
		// (get) Token: 0x0600913F RID: 37183
		public virtual extern object IHTMLDOMNode_attributes
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x06009140 RID: 37184
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_insertBefore([MarshalAs(28)] [In] IHTMLDOMNode newChild, [MarshalAs(27)] [In] [Optional] object refChild);

		// Token: 0x06009141 RID: 37185
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_removeChild([MarshalAs(28)] [In] IHTMLDOMNode oldChild);

		// Token: 0x06009142 RID: 37186
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_replaceChild([MarshalAs(28)] [In] IHTMLDOMNode newChild, [MarshalAs(28)] [In] IHTMLDOMNode oldChild);

		// Token: 0x06009143 RID: 37187
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_cloneNode([In] bool fDeep);

		// Token: 0x06009144 RID: 37188
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_removeNode([In] bool fDeep = false);

		// Token: 0x06009145 RID: 37189
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_swapNode([MarshalAs(28)] [In] IHTMLDOMNode otherNode);

		// Token: 0x06009146 RID: 37190
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_replaceNode([MarshalAs(28)] [In] IHTMLDOMNode replacement);

		// Token: 0x06009147 RID: 37191
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_appendChild([MarshalAs(28)] [In] IHTMLDOMNode newChild);

		// Token: 0x170030CA RID: 12490
		// (get) Token: 0x06009148 RID: 37192
		public virtual extern string IHTMLDOMNode_nodeName
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170030CB RID: 12491
		// (get) Token: 0x0600914A RID: 37194
		// (set) Token: 0x06009149 RID: 37193
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

		// Token: 0x170030CC RID: 12492
		// (get) Token: 0x0600914B RID: 37195
		public virtual extern IHTMLDOMNode IHTMLDOMNode_firstChild
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170030CD RID: 12493
		// (get) Token: 0x0600914C RID: 37196
		public virtual extern IHTMLDOMNode IHTMLDOMNode_lastChild
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170030CE RID: 12494
		// (get) Token: 0x0600914D RID: 37197
		public virtual extern IHTMLDOMNode IHTMLDOMNode_previousSibling
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170030CF RID: 12495
		// (get) Token: 0x0600914E RID: 37198
		public virtual extern IHTMLDOMNode IHTMLDOMNode_nextSibling
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170030D0 RID: 12496
		// (get) Token: 0x0600914F RID: 37199
		public virtual extern object IHTMLDOMNode2_ownerDocument
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x170030D1 RID: 12497
		// (get) Token: 0x06009151 RID: 37201
		// (set) Token: 0x06009150 RID: 37200
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

		// Token: 0x170030D2 RID: 12498
		// (get) Token: 0x06009153 RID: 37203
		// (set) Token: 0x06009152 RID: 37202
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

		// Token: 0x170030D3 RID: 12499
		// (get) Token: 0x06009155 RID: 37205
		// (set) Token: 0x06009154 RID: 37204
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

		// Token: 0x170030D4 RID: 12500
		// (get) Token: 0x06009157 RID: 37207
		// (set) Token: 0x06009156 RID: 37206
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

		// Token: 0x06009158 RID: 37208
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLControlElement_focus();

		// Token: 0x170030D5 RID: 12501
		// (get) Token: 0x0600915A RID: 37210
		// (set) Token: 0x06009159 RID: 37209
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

		// Token: 0x170030D6 RID: 12502
		// (get) Token: 0x0600915C RID: 37212
		// (set) Token: 0x0600915B RID: 37211
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

		// Token: 0x170030D7 RID: 12503
		// (get) Token: 0x0600915E RID: 37214
		// (set) Token: 0x0600915D RID: 37213
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

		// Token: 0x170030D8 RID: 12504
		// (get) Token: 0x06009160 RID: 37216
		// (set) Token: 0x0600915F RID: 37215
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

		// Token: 0x06009161 RID: 37217
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLControlElement_blur();

		// Token: 0x06009162 RID: 37218
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLControlElement_addFilter([MarshalAs(25)] [In] object pUnk);

		// Token: 0x06009163 RID: 37219
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLControlElement_removeFilter([MarshalAs(25)] [In] object pUnk);

		// Token: 0x170030D9 RID: 12505
		// (get) Token: 0x06009164 RID: 37220
		public virtual extern int IHTMLControlElement_clientHeight
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170030DA RID: 12506
		// (get) Token: 0x06009165 RID: 37221
		public virtual extern int IHTMLControlElement_clientWidth
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170030DB RID: 12507
		// (get) Token: 0x06009166 RID: 37222
		public virtual extern int IHTMLControlElement_clientTop
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170030DC RID: 12508
		// (get) Token: 0x06009167 RID: 37223
		public virtual extern int IHTMLControlElement_clientLeft
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170030DD RID: 12509
		// (get) Token: 0x06009169 RID: 37225
		// (set) Token: 0x06009168 RID: 37224
		public virtual extern string IHTMLInputElement_type
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x170030DE RID: 12510
		// (get) Token: 0x0600916B RID: 37227
		// (set) Token: 0x0600916A RID: 37226
		public virtual extern string IHTMLInputElement_value
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

		// Token: 0x170030DF RID: 12511
		// (get) Token: 0x0600916D RID: 37229
		// (set) Token: 0x0600916C RID: 37228
		public virtual extern string IHTMLInputElement_name
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

		// Token: 0x170030E0 RID: 12512
		// (get) Token: 0x0600916F RID: 37231
		// (set) Token: 0x0600916E RID: 37230
		public virtual extern bool IHTMLInputElement_status
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x170030E1 RID: 12513
		// (get) Token: 0x06009171 RID: 37233
		// (set) Token: 0x06009170 RID: 37232
		public virtual extern bool IHTMLInputElement_disabled
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x170030E2 RID: 12514
		// (get) Token: 0x06009172 RID: 37234
		public virtual extern IHTMLFormElement IHTMLInputElement_form
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170030E3 RID: 12515
		// (get) Token: 0x06009174 RID: 37236
		// (set) Token: 0x06009173 RID: 37235
		public virtual extern int IHTMLInputElement_size
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x170030E4 RID: 12516
		// (get) Token: 0x06009176 RID: 37238
		// (set) Token: 0x06009175 RID: 37237
		public virtual extern int IHTMLInputElement_maxLength
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x06009177 RID: 37239
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLInputElement_select();

		// Token: 0x170030E5 RID: 12517
		// (get) Token: 0x06009179 RID: 37241
		// (set) Token: 0x06009178 RID: 37240
		public virtual extern object IHTMLInputElement_onchange
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x170030E6 RID: 12518
		// (get) Token: 0x0600917B RID: 37243
		// (set) Token: 0x0600917A RID: 37242
		public virtual extern object IHTMLInputElement_onselect
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x170030E7 RID: 12519
		// (get) Token: 0x0600917D RID: 37245
		// (set) Token: 0x0600917C RID: 37244
		public virtual extern string IHTMLInputElement_defaultValue
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

		// Token: 0x170030E8 RID: 12520
		// (get) Token: 0x0600917F RID: 37247
		// (set) Token: 0x0600917E RID: 37246
		public virtual extern bool IHTMLInputElement_readOnly
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x06009180 RID: 37248
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLTxtRange IHTMLInputElement_createTextRange();

		// Token: 0x170030E9 RID: 12521
		// (get) Token: 0x06009182 RID: 37250
		// (set) Token: 0x06009181 RID: 37249
		public virtual extern bool IHTMLInputElement_indeterminate
		{
			[TypeLibFunc(4)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[TypeLibFunc(4)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x170030EA RID: 12522
		// (get) Token: 0x06009184 RID: 37252
		// (set) Token: 0x06009183 RID: 37251
		public virtual extern bool IHTMLInputElement_defaultChecked
		{
			[TypeLibFunc(4)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[TypeLibFunc(4)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x170030EB RID: 12523
		// (get) Token: 0x06009186 RID: 37254
		// (set) Token: 0x06009185 RID: 37253
		public virtual extern bool IHTMLInputElement_checked
		{
			[TypeLibFunc(4)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[TypeLibFunc(4)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x170030EC RID: 12524
		// (get) Token: 0x06009188 RID: 37256
		// (set) Token: 0x06009187 RID: 37255
		public virtual extern object IHTMLInputElement_border
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x170030ED RID: 12525
		// (get) Token: 0x0600918A RID: 37258
		// (set) Token: 0x06009189 RID: 37257
		public virtual extern int IHTMLInputElement_vspace
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x170030EE RID: 12526
		// (get) Token: 0x0600918C RID: 37260
		// (set) Token: 0x0600918B RID: 37259
		public virtual extern int IHTMLInputElement_hspace
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x170030EF RID: 12527
		// (get) Token: 0x0600918E RID: 37262
		// (set) Token: 0x0600918D RID: 37261
		public virtual extern string IHTMLInputElement_alt
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

		// Token: 0x170030F0 RID: 12528
		// (get) Token: 0x06009190 RID: 37264
		// (set) Token: 0x0600918F RID: 37263
		public virtual extern string IHTMLInputElement_src
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

		// Token: 0x170030F1 RID: 12529
		// (get) Token: 0x06009192 RID: 37266
		// (set) Token: 0x06009191 RID: 37265
		public virtual extern string IHTMLInputElement_lowsrc
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

		// Token: 0x170030F2 RID: 12530
		// (get) Token: 0x06009194 RID: 37268
		// (set) Token: 0x06009193 RID: 37267
		public virtual extern string IHTMLInputElement_vrml
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

		// Token: 0x170030F3 RID: 12531
		// (get) Token: 0x06009196 RID: 37270
		// (set) Token: 0x06009195 RID: 37269
		public virtual extern string IHTMLInputElement_dynsrc
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

		// Token: 0x170030F4 RID: 12532
		// (get) Token: 0x06009197 RID: 37271
		public virtual extern string IHTMLInputElement_readyState
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170030F5 RID: 12533
		// (get) Token: 0x06009198 RID: 37272
		public virtual extern bool IHTMLInputElement_complete
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170030F6 RID: 12534
		// (get) Token: 0x0600919A RID: 37274
		// (set) Token: 0x06009199 RID: 37273
		public virtual extern object IHTMLInputElement_loop
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x170030F7 RID: 12535
		// (get) Token: 0x0600919C RID: 37276
		// (set) Token: 0x0600919B RID: 37275
		public virtual extern string IHTMLInputElement_align
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

		// Token: 0x170030F8 RID: 12536
		// (get) Token: 0x0600919E RID: 37278
		// (set) Token: 0x0600919D RID: 37277
		public virtual extern object IHTMLInputElement_onload
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x170030F9 RID: 12537
		// (get) Token: 0x060091A0 RID: 37280
		// (set) Token: 0x0600919F RID: 37279
		public virtual extern object IHTMLInputElement_onerror
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x170030FA RID: 12538
		// (get) Token: 0x060091A2 RID: 37282
		// (set) Token: 0x060091A1 RID: 37281
		public virtual extern object IHTMLInputElement_onabort
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x170030FB RID: 12539
		// (get) Token: 0x060091A4 RID: 37284
		// (set) Token: 0x060091A3 RID: 37283
		public virtual extern int IHTMLInputElement_width
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x170030FC RID: 12540
		// (get) Token: 0x060091A6 RID: 37286
		// (set) Token: 0x060091A5 RID: 37285
		public virtual extern int IHTMLInputElement_height
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x170030FD RID: 12541
		// (get) Token: 0x060091A8 RID: 37288
		// (set) Token: 0x060091A7 RID: 37287
		public virtual extern string IHTMLInputElement_Start
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

		// Token: 0x170030FE RID: 12542
		// (get) Token: 0x060091AA RID: 37290
		// (set) Token: 0x060091A9 RID: 37289
		public virtual extern string IHTMLInputElement2_accept
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

		// Token: 0x170030FF RID: 12543
		// (get) Token: 0x060091AC RID: 37292
		// (set) Token: 0x060091AB RID: 37291
		public virtual extern string IHTMLInputElement2_useMap
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

		// Token: 0x17003100 RID: 12544
		// (get) Token: 0x060091AD RID: 37293
		public virtual extern string IHTMLInputTextElement_type
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17003101 RID: 12545
		// (get) Token: 0x060091AF RID: 37295
		// (set) Token: 0x060091AE RID: 37294
		public virtual extern string IHTMLInputTextElement_value
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

		// Token: 0x17003102 RID: 12546
		// (get) Token: 0x060091B1 RID: 37297
		// (set) Token: 0x060091B0 RID: 37296
		public virtual extern string IHTMLInputTextElement_name
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

		// Token: 0x17003103 RID: 12547
		// (get) Token: 0x060091B3 RID: 37299
		// (set) Token: 0x060091B2 RID: 37298
		public virtual extern object IHTMLInputTextElement_status
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17003104 RID: 12548
		// (get) Token: 0x060091B5 RID: 37301
		// (set) Token: 0x060091B4 RID: 37300
		public virtual extern bool IHTMLInputTextElement_disabled
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x17003105 RID: 12549
		// (get) Token: 0x060091B6 RID: 37302
		public virtual extern IHTMLFormElement IHTMLInputTextElement_form
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17003106 RID: 12550
		// (get) Token: 0x060091B8 RID: 37304
		// (set) Token: 0x060091B7 RID: 37303
		public virtual extern string IHTMLInputTextElement_defaultValue
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

		// Token: 0x17003107 RID: 12551
		// (get) Token: 0x060091BA RID: 37306
		// (set) Token: 0x060091B9 RID: 37305
		public virtual extern int IHTMLInputTextElement_size
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x17003108 RID: 12552
		// (get) Token: 0x060091BC RID: 37308
		// (set) Token: 0x060091BB RID: 37307
		public virtual extern int IHTMLInputTextElement_maxLength
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x060091BD RID: 37309
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLInputTextElement_select();

		// Token: 0x17003109 RID: 12553
		// (get) Token: 0x060091BF RID: 37311
		// (set) Token: 0x060091BE RID: 37310
		public virtual extern object IHTMLInputTextElement_onchange
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x1700310A RID: 12554
		// (get) Token: 0x060091C1 RID: 37313
		// (set) Token: 0x060091C0 RID: 37312
		public virtual extern object IHTMLInputTextElement_onselect
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x1700310B RID: 12555
		// (get) Token: 0x060091C3 RID: 37315
		// (set) Token: 0x060091C2 RID: 37314
		public virtual extern bool IHTMLInputTextElement_readOnly
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x060091C4 RID: 37316
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLTxtRange IHTMLInputTextElement_createTextRange();

		// Token: 0x1700310C RID: 12556
		// (get) Token: 0x060091C5 RID: 37317
		public virtual extern string IHTMLInputHiddenElement_type
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x1700310D RID: 12557
		// (get) Token: 0x060091C7 RID: 37319
		// (set) Token: 0x060091C6 RID: 37318
		public virtual extern string IHTMLInputHiddenElement_value
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

		// Token: 0x1700310E RID: 12558
		// (get) Token: 0x060091C9 RID: 37321
		// (set) Token: 0x060091C8 RID: 37320
		public virtual extern string IHTMLInputHiddenElement_name
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

		// Token: 0x1700310F RID: 12559
		// (get) Token: 0x060091CB RID: 37323
		// (set) Token: 0x060091CA RID: 37322
		public virtual extern object IHTMLInputHiddenElement_status
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17003110 RID: 12560
		// (get) Token: 0x060091CD RID: 37325
		// (set) Token: 0x060091CC RID: 37324
		public virtual extern bool IHTMLInputHiddenElement_disabled
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x17003111 RID: 12561
		// (get) Token: 0x060091CE RID: 37326
		public virtual extern IHTMLFormElement IHTMLInputHiddenElement_form
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x060091CF RID: 37327
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLTxtRange IHTMLInputHiddenElement_createTextRange();

		// Token: 0x17003112 RID: 12562
		// (get) Token: 0x060091D0 RID: 37328
		public virtual extern string IHTMLInputButtonElement_type
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17003113 RID: 12563
		// (get) Token: 0x060091D2 RID: 37330
		// (set) Token: 0x060091D1 RID: 37329
		public virtual extern string IHTMLInputButtonElement_value
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

		// Token: 0x17003114 RID: 12564
		// (get) Token: 0x060091D4 RID: 37332
		// (set) Token: 0x060091D3 RID: 37331
		public virtual extern string IHTMLInputButtonElement_name
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

		// Token: 0x17003115 RID: 12565
		// (get) Token: 0x060091D6 RID: 37334
		// (set) Token: 0x060091D5 RID: 37333
		public virtual extern object IHTMLInputButtonElement_status
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17003116 RID: 12566
		// (get) Token: 0x060091D8 RID: 37336
		// (set) Token: 0x060091D7 RID: 37335
		public virtual extern bool IHTMLInputButtonElement_disabled
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x17003117 RID: 12567
		// (get) Token: 0x060091D9 RID: 37337
		public virtual extern IHTMLFormElement IHTMLInputButtonElement_form
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x060091DA RID: 37338
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLTxtRange IHTMLInputButtonElement_createTextRange();

		// Token: 0x17003118 RID: 12568
		// (get) Token: 0x060091DB RID: 37339
		public virtual extern string IHTMLInputFileElement_type
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17003119 RID: 12569
		// (get) Token: 0x060091DD RID: 37341
		// (set) Token: 0x060091DC RID: 37340
		public virtual extern string IHTMLInputFileElement_name
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

		// Token: 0x1700311A RID: 12570
		// (get) Token: 0x060091DF RID: 37343
		// (set) Token: 0x060091DE RID: 37342
		public virtual extern object IHTMLInputFileElement_status
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x1700311B RID: 12571
		// (get) Token: 0x060091E1 RID: 37345
		// (set) Token: 0x060091E0 RID: 37344
		public virtual extern bool IHTMLInputFileElement_disabled
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x1700311C RID: 12572
		// (get) Token: 0x060091E2 RID: 37346
		public virtual extern IHTMLFormElement IHTMLInputFileElement_form
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x1700311D RID: 12573
		// (get) Token: 0x060091E4 RID: 37348
		// (set) Token: 0x060091E3 RID: 37347
		public virtual extern int IHTMLInputFileElement_size
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x1700311E RID: 12574
		// (get) Token: 0x060091E6 RID: 37350
		// (set) Token: 0x060091E5 RID: 37349
		public virtual extern int IHTMLInputFileElement_maxLength
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x060091E7 RID: 37351
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLInputFileElement_select();

		// Token: 0x1700311F RID: 12575
		// (get) Token: 0x060091E9 RID: 37353
		// (set) Token: 0x060091E8 RID: 37352
		public virtual extern object IHTMLInputFileElement_onchange
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17003120 RID: 12576
		// (get) Token: 0x060091EB RID: 37355
		// (set) Token: 0x060091EA RID: 37354
		public virtual extern object IHTMLInputFileElement_onselect
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17003121 RID: 12577
		// (get) Token: 0x060091ED RID: 37357
		// (set) Token: 0x060091EC RID: 37356
		public virtual extern string IHTMLInputFileElement_value
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

		// Token: 0x17003122 RID: 12578
		// (get) Token: 0x060091EF RID: 37359
		// (set) Token: 0x060091EE RID: 37358
		public virtual extern string IHTMLOptionButtonElement_value
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

		// Token: 0x17003123 RID: 12579
		// (get) Token: 0x060091F0 RID: 37360
		public virtual extern string IHTMLOptionButtonElement_type
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17003124 RID: 12580
		// (get) Token: 0x060091F2 RID: 37362
		// (set) Token: 0x060091F1 RID: 37361
		public virtual extern string IHTMLOptionButtonElement_name
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

		// Token: 0x17003125 RID: 12581
		// (get) Token: 0x060091F4 RID: 37364
		// (set) Token: 0x060091F3 RID: 37363
		public virtual extern bool IHTMLOptionButtonElement_checked
		{
			[TypeLibFunc(4)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[TypeLibFunc(4)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x17003126 RID: 12582
		// (get) Token: 0x060091F6 RID: 37366
		// (set) Token: 0x060091F5 RID: 37365
		public virtual extern bool IHTMLOptionButtonElement_defaultChecked
		{
			[TypeLibFunc(4)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[TypeLibFunc(4)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x17003127 RID: 12583
		// (get) Token: 0x060091F8 RID: 37368
		// (set) Token: 0x060091F7 RID: 37367
		public virtual extern object IHTMLOptionButtonElement_onchange
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17003128 RID: 12584
		// (get) Token: 0x060091FA RID: 37370
		// (set) Token: 0x060091F9 RID: 37369
		public virtual extern bool IHTMLOptionButtonElement_disabled
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x17003129 RID: 12585
		// (get) Token: 0x060091FC RID: 37372
		// (set) Token: 0x060091FB RID: 37371
		public virtual extern bool IHTMLOptionButtonElement_status
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x1700312A RID: 12586
		// (get) Token: 0x060091FE RID: 37374
		// (set) Token: 0x060091FD RID: 37373
		public virtual extern bool IHTMLOptionButtonElement_indeterminate
		{
			[TypeLibFunc(4)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[TypeLibFunc(4)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x1700312B RID: 12587
		// (get) Token: 0x060091FF RID: 37375
		public virtual extern IHTMLFormElement IHTMLOptionButtonElement_form
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x1700312C RID: 12588
		// (get) Token: 0x06009200 RID: 37376
		public virtual extern string IHTMLInputImage_type
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x1700312D RID: 12589
		// (get) Token: 0x06009202 RID: 37378
		// (set) Token: 0x06009201 RID: 37377
		public virtual extern bool IHTMLInputImage_disabled
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x1700312E RID: 12590
		// (get) Token: 0x06009204 RID: 37380
		// (set) Token: 0x06009203 RID: 37379
		public virtual extern object IHTMLInputImage_border
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x1700312F RID: 12591
		// (get) Token: 0x06009206 RID: 37382
		// (set) Token: 0x06009205 RID: 37381
		public virtual extern int IHTMLInputImage_vspace
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x17003130 RID: 12592
		// (get) Token: 0x06009208 RID: 37384
		// (set) Token: 0x06009207 RID: 37383
		public virtual extern int IHTMLInputImage_hspace
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x17003131 RID: 12593
		// (get) Token: 0x0600920A RID: 37386
		// (set) Token: 0x06009209 RID: 37385
		public virtual extern string IHTMLInputImage_alt
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

		// Token: 0x17003132 RID: 12594
		// (get) Token: 0x0600920C RID: 37388
		// (set) Token: 0x0600920B RID: 37387
		public virtual extern string IHTMLInputImage_src
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

		// Token: 0x17003133 RID: 12595
		// (get) Token: 0x0600920E RID: 37390
		// (set) Token: 0x0600920D RID: 37389
		public virtual extern string IHTMLInputImage_lowsrc
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

		// Token: 0x17003134 RID: 12596
		// (get) Token: 0x06009210 RID: 37392
		// (set) Token: 0x0600920F RID: 37391
		public virtual extern string IHTMLInputImage_vrml
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

		// Token: 0x17003135 RID: 12597
		// (get) Token: 0x06009212 RID: 37394
		// (set) Token: 0x06009211 RID: 37393
		public virtual extern string IHTMLInputImage_dynsrc
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

		// Token: 0x17003136 RID: 12598
		// (get) Token: 0x06009213 RID: 37395
		public virtual extern string IHTMLInputImage_readyState
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17003137 RID: 12599
		// (get) Token: 0x06009214 RID: 37396
		public virtual extern bool IHTMLInputImage_complete
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17003138 RID: 12600
		// (get) Token: 0x06009216 RID: 37398
		// (set) Token: 0x06009215 RID: 37397
		public virtual extern object IHTMLInputImage_loop
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17003139 RID: 12601
		// (get) Token: 0x06009218 RID: 37400
		// (set) Token: 0x06009217 RID: 37399
		public virtual extern string IHTMLInputImage_align
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

		// Token: 0x1700313A RID: 12602
		// (get) Token: 0x0600921A RID: 37402
		// (set) Token: 0x06009219 RID: 37401
		public virtual extern object IHTMLInputImage_onload
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x1700313B RID: 12603
		// (get) Token: 0x0600921C RID: 37404
		// (set) Token: 0x0600921B RID: 37403
		public virtual extern object IHTMLInputImage_onerror
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x1700313C RID: 12604
		// (get) Token: 0x0600921E RID: 37406
		// (set) Token: 0x0600921D RID: 37405
		public virtual extern object IHTMLInputImage_onabort
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x1700313D RID: 12605
		// (get) Token: 0x06009220 RID: 37408
		// (set) Token: 0x0600921F RID: 37407
		public virtual extern string IHTMLInputImage_name
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

		// Token: 0x1700313E RID: 12606
		// (get) Token: 0x06009222 RID: 37410
		// (set) Token: 0x06009221 RID: 37409
		public virtual extern int IHTMLInputImage_width
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x1700313F RID: 12607
		// (get) Token: 0x06009224 RID: 37412
		// (set) Token: 0x06009223 RID: 37411
		public virtual extern int IHTMLInputImage_height
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x17003140 RID: 12608
		// (get) Token: 0x06009226 RID: 37414
		// (set) Token: 0x06009225 RID: 37413
		public virtual extern string IHTMLInputImage_Start
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

		// Token: 0x1400104A RID: 4170
		// (add) Token: 0x06009227 RID: 37415
		// (remove) Token: 0x06009228 RID: 37416
		public virtual extern event HTMLInputTextElementEvents_onhelpEventHandler HTMLInputTextElementEvents_Event_onhelp;

		// Token: 0x1400104B RID: 4171
		// (add) Token: 0x06009229 RID: 37417
		// (remove) Token: 0x0600922A RID: 37418
		public virtual extern event HTMLInputTextElementEvents_onclickEventHandler HTMLInputTextElementEvents_Event_onclick;

		// Token: 0x1400104C RID: 4172
		// (add) Token: 0x0600922B RID: 37419
		// (remove) Token: 0x0600922C RID: 37420
		public virtual extern event HTMLInputTextElementEvents_ondblclickEventHandler HTMLInputTextElementEvents_Event_ondblclick;

		// Token: 0x1400104D RID: 4173
		// (add) Token: 0x0600922D RID: 37421
		// (remove) Token: 0x0600922E RID: 37422
		public virtual extern event HTMLInputTextElementEvents_onkeypressEventHandler HTMLInputTextElementEvents_Event_onkeypress;

		// Token: 0x1400104E RID: 4174
		// (add) Token: 0x0600922F RID: 37423
		// (remove) Token: 0x06009230 RID: 37424
		public virtual extern event HTMLInputTextElementEvents_onkeydownEventHandler HTMLInputTextElementEvents_Event_onkeydown;

		// Token: 0x1400104F RID: 4175
		// (add) Token: 0x06009231 RID: 37425
		// (remove) Token: 0x06009232 RID: 37426
		public virtual extern event HTMLInputTextElementEvents_onkeyupEventHandler HTMLInputTextElementEvents_Event_onkeyup;

		// Token: 0x14001050 RID: 4176
		// (add) Token: 0x06009233 RID: 37427
		// (remove) Token: 0x06009234 RID: 37428
		public virtual extern event HTMLInputTextElementEvents_onmouseoutEventHandler HTMLInputTextElementEvents_Event_onmouseout;

		// Token: 0x14001051 RID: 4177
		// (add) Token: 0x06009235 RID: 37429
		// (remove) Token: 0x06009236 RID: 37430
		public virtual extern event HTMLInputTextElementEvents_onmouseoverEventHandler HTMLInputTextElementEvents_Event_onmouseover;

		// Token: 0x14001052 RID: 4178
		// (add) Token: 0x06009237 RID: 37431
		// (remove) Token: 0x06009238 RID: 37432
		public virtual extern event HTMLInputTextElementEvents_onmousemoveEventHandler HTMLInputTextElementEvents_Event_onmousemove;

		// Token: 0x14001053 RID: 4179
		// (add) Token: 0x06009239 RID: 37433
		// (remove) Token: 0x0600923A RID: 37434
		public virtual extern event HTMLInputTextElementEvents_onmousedownEventHandler HTMLInputTextElementEvents_Event_onmousedown;

		// Token: 0x14001054 RID: 4180
		// (add) Token: 0x0600923B RID: 37435
		// (remove) Token: 0x0600923C RID: 37436
		public virtual extern event HTMLInputTextElementEvents_onmouseupEventHandler HTMLInputTextElementEvents_Event_onmouseup;

		// Token: 0x14001055 RID: 4181
		// (add) Token: 0x0600923D RID: 37437
		// (remove) Token: 0x0600923E RID: 37438
		public virtual extern event HTMLInputTextElementEvents_onselectstartEventHandler HTMLInputTextElementEvents_Event_onselectstart;

		// Token: 0x14001056 RID: 4182
		// (add) Token: 0x0600923F RID: 37439
		// (remove) Token: 0x06009240 RID: 37440
		public virtual extern event HTMLInputTextElementEvents_onfilterchangeEventHandler HTMLInputTextElementEvents_Event_onfilterchange;

		// Token: 0x14001057 RID: 4183
		// (add) Token: 0x06009241 RID: 37441
		// (remove) Token: 0x06009242 RID: 37442
		public virtual extern event HTMLInputTextElementEvents_ondragstartEventHandler HTMLInputTextElementEvents_Event_ondragstart;

		// Token: 0x14001058 RID: 4184
		// (add) Token: 0x06009243 RID: 37443
		// (remove) Token: 0x06009244 RID: 37444
		public virtual extern event HTMLInputTextElementEvents_onbeforeupdateEventHandler HTMLInputTextElementEvents_Event_onbeforeupdate;

		// Token: 0x14001059 RID: 4185
		// (add) Token: 0x06009245 RID: 37445
		// (remove) Token: 0x06009246 RID: 37446
		public virtual extern event HTMLInputTextElementEvents_onafterupdateEventHandler HTMLInputTextElementEvents_Event_onafterupdate;

		// Token: 0x1400105A RID: 4186
		// (add) Token: 0x06009247 RID: 37447
		// (remove) Token: 0x06009248 RID: 37448
		public virtual extern event HTMLInputTextElementEvents_onerrorupdateEventHandler HTMLInputTextElementEvents_Event_onerrorupdate;

		// Token: 0x1400105B RID: 4187
		// (add) Token: 0x06009249 RID: 37449
		// (remove) Token: 0x0600924A RID: 37450
		public virtual extern event HTMLInputTextElementEvents_onrowexitEventHandler HTMLInputTextElementEvents_Event_onrowexit;

		// Token: 0x1400105C RID: 4188
		// (add) Token: 0x0600924B RID: 37451
		// (remove) Token: 0x0600924C RID: 37452
		public virtual extern event HTMLInputTextElementEvents_onrowenterEventHandler HTMLInputTextElementEvents_Event_onrowenter;

		// Token: 0x1400105D RID: 4189
		// (add) Token: 0x0600924D RID: 37453
		// (remove) Token: 0x0600924E RID: 37454
		public virtual extern event HTMLInputTextElementEvents_ondatasetchangedEventHandler HTMLInputTextElementEvents_Event_ondatasetchanged;

		// Token: 0x1400105E RID: 4190
		// (add) Token: 0x0600924F RID: 37455
		// (remove) Token: 0x06009250 RID: 37456
		public virtual extern event HTMLInputTextElementEvents_ondataavailableEventHandler HTMLInputTextElementEvents_Event_ondataavailable;

		// Token: 0x1400105F RID: 4191
		// (add) Token: 0x06009251 RID: 37457
		// (remove) Token: 0x06009252 RID: 37458
		public virtual extern event HTMLInputTextElementEvents_ondatasetcompleteEventHandler HTMLInputTextElementEvents_Event_ondatasetcomplete;

		// Token: 0x14001060 RID: 4192
		// (add) Token: 0x06009253 RID: 37459
		// (remove) Token: 0x06009254 RID: 37460
		public virtual extern event HTMLInputTextElementEvents_onlosecaptureEventHandler HTMLInputTextElementEvents_Event_onlosecapture;

		// Token: 0x14001061 RID: 4193
		// (add) Token: 0x06009255 RID: 37461
		// (remove) Token: 0x06009256 RID: 37462
		public virtual extern event HTMLInputTextElementEvents_onpropertychangeEventHandler HTMLInputTextElementEvents_Event_onpropertychange;

		// Token: 0x14001062 RID: 4194
		// (add) Token: 0x06009257 RID: 37463
		// (remove) Token: 0x06009258 RID: 37464
		public virtual extern event HTMLInputTextElementEvents_onscrollEventHandler HTMLInputTextElementEvents_Event_onscroll;

		// Token: 0x14001063 RID: 4195
		// (add) Token: 0x06009259 RID: 37465
		// (remove) Token: 0x0600925A RID: 37466
		public virtual extern event HTMLInputTextElementEvents_onfocusEventHandler HTMLInputTextElementEvents_Event_onfocus;

		// Token: 0x14001064 RID: 4196
		// (add) Token: 0x0600925B RID: 37467
		// (remove) Token: 0x0600925C RID: 37468
		public virtual extern event HTMLInputTextElementEvents_onblurEventHandler HTMLInputTextElementEvents_Event_onblur;

		// Token: 0x14001065 RID: 4197
		// (add) Token: 0x0600925D RID: 37469
		// (remove) Token: 0x0600925E RID: 37470
		public virtual extern event HTMLInputTextElementEvents_onresizeEventHandler HTMLInputTextElementEvents_Event_onresize;

		// Token: 0x14001066 RID: 4198
		// (add) Token: 0x0600925F RID: 37471
		// (remove) Token: 0x06009260 RID: 37472
		public virtual extern event HTMLInputTextElementEvents_ondragEventHandler HTMLInputTextElementEvents_Event_ondrag;

		// Token: 0x14001067 RID: 4199
		// (add) Token: 0x06009261 RID: 37473
		// (remove) Token: 0x06009262 RID: 37474
		public virtual extern event HTMLInputTextElementEvents_ondragendEventHandler HTMLInputTextElementEvents_Event_ondragend;

		// Token: 0x14001068 RID: 4200
		// (add) Token: 0x06009263 RID: 37475
		// (remove) Token: 0x06009264 RID: 37476
		public virtual extern event HTMLInputTextElementEvents_ondragenterEventHandler HTMLInputTextElementEvents_Event_ondragenter;

		// Token: 0x14001069 RID: 4201
		// (add) Token: 0x06009265 RID: 37477
		// (remove) Token: 0x06009266 RID: 37478
		public virtual extern event HTMLInputTextElementEvents_ondragoverEventHandler HTMLInputTextElementEvents_Event_ondragover;

		// Token: 0x1400106A RID: 4202
		// (add) Token: 0x06009267 RID: 37479
		// (remove) Token: 0x06009268 RID: 37480
		public virtual extern event HTMLInputTextElementEvents_ondragleaveEventHandler HTMLInputTextElementEvents_Event_ondragleave;

		// Token: 0x1400106B RID: 4203
		// (add) Token: 0x06009269 RID: 37481
		// (remove) Token: 0x0600926A RID: 37482
		public virtual extern event HTMLInputTextElementEvents_ondropEventHandler HTMLInputTextElementEvents_Event_ondrop;

		// Token: 0x1400106C RID: 4204
		// (add) Token: 0x0600926B RID: 37483
		// (remove) Token: 0x0600926C RID: 37484
		public virtual extern event HTMLInputTextElementEvents_onbeforecutEventHandler HTMLInputTextElementEvents_Event_onbeforecut;

		// Token: 0x1400106D RID: 4205
		// (add) Token: 0x0600926D RID: 37485
		// (remove) Token: 0x0600926E RID: 37486
		public virtual extern event HTMLInputTextElementEvents_oncutEventHandler HTMLInputTextElementEvents_Event_oncut;

		// Token: 0x1400106E RID: 4206
		// (add) Token: 0x0600926F RID: 37487
		// (remove) Token: 0x06009270 RID: 37488
		public virtual extern event HTMLInputTextElementEvents_onbeforecopyEventHandler HTMLInputTextElementEvents_Event_onbeforecopy;

		// Token: 0x1400106F RID: 4207
		// (add) Token: 0x06009271 RID: 37489
		// (remove) Token: 0x06009272 RID: 37490
		public virtual extern event HTMLInputTextElementEvents_oncopyEventHandler HTMLInputTextElementEvents_Event_oncopy;

		// Token: 0x14001070 RID: 4208
		// (add) Token: 0x06009273 RID: 37491
		// (remove) Token: 0x06009274 RID: 37492
		public virtual extern event HTMLInputTextElementEvents_onbeforepasteEventHandler HTMLInputTextElementEvents_Event_onbeforepaste;

		// Token: 0x14001071 RID: 4209
		// (add) Token: 0x06009275 RID: 37493
		// (remove) Token: 0x06009276 RID: 37494
		public virtual extern event HTMLInputTextElementEvents_onpasteEventHandler HTMLInputTextElementEvents_Event_onpaste;

		// Token: 0x14001072 RID: 4210
		// (add) Token: 0x06009277 RID: 37495
		// (remove) Token: 0x06009278 RID: 37496
		public virtual extern event HTMLInputTextElementEvents_oncontextmenuEventHandler HTMLInputTextElementEvents_Event_oncontextmenu;

		// Token: 0x14001073 RID: 4211
		// (add) Token: 0x06009279 RID: 37497
		// (remove) Token: 0x0600927A RID: 37498
		public virtual extern event HTMLInputTextElementEvents_onrowsdeleteEventHandler HTMLInputTextElementEvents_Event_onrowsdelete;

		// Token: 0x14001074 RID: 4212
		// (add) Token: 0x0600927B RID: 37499
		// (remove) Token: 0x0600927C RID: 37500
		public virtual extern event HTMLInputTextElementEvents_onrowsinsertedEventHandler HTMLInputTextElementEvents_Event_onrowsinserted;

		// Token: 0x14001075 RID: 4213
		// (add) Token: 0x0600927D RID: 37501
		// (remove) Token: 0x0600927E RID: 37502
		public virtual extern event HTMLInputTextElementEvents_oncellchangeEventHandler HTMLInputTextElementEvents_Event_oncellchange;

		// Token: 0x14001076 RID: 4214
		// (add) Token: 0x0600927F RID: 37503
		// (remove) Token: 0x06009280 RID: 37504
		public virtual extern event HTMLInputTextElementEvents_onreadystatechangeEventHandler HTMLInputTextElementEvents_Event_onreadystatechange;

		// Token: 0x14001077 RID: 4215
		// (add) Token: 0x06009281 RID: 37505
		// (remove) Token: 0x06009282 RID: 37506
		public virtual extern event HTMLInputTextElementEvents_onbeforeeditfocusEventHandler HTMLInputTextElementEvents_Event_onbeforeeditfocus;

		// Token: 0x14001078 RID: 4216
		// (add) Token: 0x06009283 RID: 37507
		// (remove) Token: 0x06009284 RID: 37508
		public virtual extern event HTMLInputTextElementEvents_onlayoutcompleteEventHandler HTMLInputTextElementEvents_Event_onlayoutcomplete;

		// Token: 0x14001079 RID: 4217
		// (add) Token: 0x06009285 RID: 37509
		// (remove) Token: 0x06009286 RID: 37510
		public virtual extern event HTMLInputTextElementEvents_onpageEventHandler HTMLInputTextElementEvents_Event_onpage;

		// Token: 0x1400107A RID: 4218
		// (add) Token: 0x06009287 RID: 37511
		// (remove) Token: 0x06009288 RID: 37512
		public virtual extern event HTMLInputTextElementEvents_onbeforedeactivateEventHandler HTMLInputTextElementEvents_Event_onbeforedeactivate;

		// Token: 0x1400107B RID: 4219
		// (add) Token: 0x06009289 RID: 37513
		// (remove) Token: 0x0600928A RID: 37514
		public virtual extern event HTMLInputTextElementEvents_onbeforeactivateEventHandler HTMLInputTextElementEvents_Event_onbeforeactivate;

		// Token: 0x1400107C RID: 4220
		// (add) Token: 0x0600928B RID: 37515
		// (remove) Token: 0x0600928C RID: 37516
		public virtual extern event HTMLInputTextElementEvents_onmoveEventHandler HTMLInputTextElementEvents_Event_onmove;

		// Token: 0x1400107D RID: 4221
		// (add) Token: 0x0600928D RID: 37517
		// (remove) Token: 0x0600928E RID: 37518
		public virtual extern event HTMLInputTextElementEvents_oncontrolselectEventHandler HTMLInputTextElementEvents_Event_oncontrolselect;

		// Token: 0x1400107E RID: 4222
		// (add) Token: 0x0600928F RID: 37519
		// (remove) Token: 0x06009290 RID: 37520
		public virtual extern event HTMLInputTextElementEvents_onmovestartEventHandler HTMLInputTextElementEvents_Event_onmovestart;

		// Token: 0x1400107F RID: 4223
		// (add) Token: 0x06009291 RID: 37521
		// (remove) Token: 0x06009292 RID: 37522
		public virtual extern event HTMLInputTextElementEvents_onmoveendEventHandler HTMLInputTextElementEvents_Event_onmoveend;

		// Token: 0x14001080 RID: 4224
		// (add) Token: 0x06009293 RID: 37523
		// (remove) Token: 0x06009294 RID: 37524
		public virtual extern event HTMLInputTextElementEvents_onresizestartEventHandler HTMLInputTextElementEvents_Event_onresizestart;

		// Token: 0x14001081 RID: 4225
		// (add) Token: 0x06009295 RID: 37525
		// (remove) Token: 0x06009296 RID: 37526
		public virtual extern event HTMLInputTextElementEvents_onresizeendEventHandler HTMLInputTextElementEvents_Event_onresizeend;

		// Token: 0x14001082 RID: 4226
		// (add) Token: 0x06009297 RID: 37527
		// (remove) Token: 0x06009298 RID: 37528
		public virtual extern event HTMLInputTextElementEvents_onmouseenterEventHandler HTMLInputTextElementEvents_Event_onmouseenter;

		// Token: 0x14001083 RID: 4227
		// (add) Token: 0x06009299 RID: 37529
		// (remove) Token: 0x0600929A RID: 37530
		public virtual extern event HTMLInputTextElementEvents_onmouseleaveEventHandler HTMLInputTextElementEvents_Event_onmouseleave;

		// Token: 0x14001084 RID: 4228
		// (add) Token: 0x0600929B RID: 37531
		// (remove) Token: 0x0600929C RID: 37532
		public virtual extern event HTMLInputTextElementEvents_onmousewheelEventHandler HTMLInputTextElementEvents_Event_onmousewheel;

		// Token: 0x14001085 RID: 4229
		// (add) Token: 0x0600929D RID: 37533
		// (remove) Token: 0x0600929E RID: 37534
		public virtual extern event HTMLInputTextElementEvents_onactivateEventHandler HTMLInputTextElementEvents_Event_onactivate;

		// Token: 0x14001086 RID: 4230
		// (add) Token: 0x0600929F RID: 37535
		// (remove) Token: 0x060092A0 RID: 37536
		public virtual extern event HTMLInputTextElementEvents_ondeactivateEventHandler HTMLInputTextElementEvents_Event_ondeactivate;

		// Token: 0x14001087 RID: 4231
		// (add) Token: 0x060092A1 RID: 37537
		// (remove) Token: 0x060092A2 RID: 37538
		public virtual extern event HTMLInputTextElementEvents_onfocusinEventHandler HTMLInputTextElementEvents_Event_onfocusin;

		// Token: 0x14001088 RID: 4232
		// (add) Token: 0x060092A3 RID: 37539
		// (remove) Token: 0x060092A4 RID: 37540
		public virtual extern event HTMLInputTextElementEvents_onfocusoutEventHandler HTMLInputTextElementEvents_Event_onfocusout;

		// Token: 0x14001089 RID: 4233
		// (add) Token: 0x060092A5 RID: 37541
		// (remove) Token: 0x060092A6 RID: 37542
		public virtual extern event HTMLInputTextElementEvents_onchangeEventHandler HTMLInputTextElementEvents_Event_onchange;

		// Token: 0x1400108A RID: 4234
		// (add) Token: 0x060092A7 RID: 37543
		// (remove) Token: 0x060092A8 RID: 37544
		public virtual extern event HTMLInputTextElementEvents_onselectEventHandler HTMLInputTextElementEvents_Event_onselect;

		// Token: 0x1400108B RID: 4235
		// (add) Token: 0x060092A9 RID: 37545
		// (remove) Token: 0x060092AA RID: 37546
		public virtual extern event HTMLInputTextElementEvents_onloadEventHandler HTMLInputTextElementEvents_Event_onload;

		// Token: 0x1400108C RID: 4236
		// (add) Token: 0x060092AB RID: 37547
		// (remove) Token: 0x060092AC RID: 37548
		public virtual extern event HTMLInputTextElementEvents_onerrorEventHandler HTMLInputTextElementEvents_Event_onerror;

		// Token: 0x1400108D RID: 4237
		// (add) Token: 0x060092AD RID: 37549
		// (remove) Token: 0x060092AE RID: 37550
		public virtual extern event HTMLInputTextElementEvents_onabortEventHandler HTMLInputTextElementEvents_Event_onabort;

		// Token: 0x1400108E RID: 4238
		// (add) Token: 0x060092AF RID: 37551
		// (remove) Token: 0x060092B0 RID: 37552
		public virtual extern event HTMLInputTextElementEvents2_onhelpEventHandler HTMLInputTextElementEvents2_Event_onhelp;

		// Token: 0x1400108F RID: 4239
		// (add) Token: 0x060092B1 RID: 37553
		// (remove) Token: 0x060092B2 RID: 37554
		public virtual extern event HTMLInputTextElementEvents2_onclickEventHandler HTMLInputTextElementEvents2_Event_onclick;

		// Token: 0x14001090 RID: 4240
		// (add) Token: 0x060092B3 RID: 37555
		// (remove) Token: 0x060092B4 RID: 37556
		public virtual extern event HTMLInputTextElementEvents2_ondblclickEventHandler HTMLInputTextElementEvents2_Event_ondblclick;

		// Token: 0x14001091 RID: 4241
		// (add) Token: 0x060092B5 RID: 37557
		// (remove) Token: 0x060092B6 RID: 37558
		public virtual extern event HTMLInputTextElementEvents2_onkeypressEventHandler HTMLInputTextElementEvents2_Event_onkeypress;

		// Token: 0x14001092 RID: 4242
		// (add) Token: 0x060092B7 RID: 37559
		// (remove) Token: 0x060092B8 RID: 37560
		public virtual extern event HTMLInputTextElementEvents2_onkeydownEventHandler HTMLInputTextElementEvents2_Event_onkeydown;

		// Token: 0x14001093 RID: 4243
		// (add) Token: 0x060092B9 RID: 37561
		// (remove) Token: 0x060092BA RID: 37562
		public virtual extern event HTMLInputTextElementEvents2_onkeyupEventHandler HTMLInputTextElementEvents2_Event_onkeyup;

		// Token: 0x14001094 RID: 4244
		// (add) Token: 0x060092BB RID: 37563
		// (remove) Token: 0x060092BC RID: 37564
		public virtual extern event HTMLInputTextElementEvents2_onmouseoutEventHandler HTMLInputTextElementEvents2_Event_onmouseout;

		// Token: 0x14001095 RID: 4245
		// (add) Token: 0x060092BD RID: 37565
		// (remove) Token: 0x060092BE RID: 37566
		public virtual extern event HTMLInputTextElementEvents2_onmouseoverEventHandler HTMLInputTextElementEvents2_Event_onmouseover;

		// Token: 0x14001096 RID: 4246
		// (add) Token: 0x060092BF RID: 37567
		// (remove) Token: 0x060092C0 RID: 37568
		public virtual extern event HTMLInputTextElementEvents2_onmousemoveEventHandler HTMLInputTextElementEvents2_Event_onmousemove;

		// Token: 0x14001097 RID: 4247
		// (add) Token: 0x060092C1 RID: 37569
		// (remove) Token: 0x060092C2 RID: 37570
		public virtual extern event HTMLInputTextElementEvents2_onmousedownEventHandler HTMLInputTextElementEvents2_Event_onmousedown;

		// Token: 0x14001098 RID: 4248
		// (add) Token: 0x060092C3 RID: 37571
		// (remove) Token: 0x060092C4 RID: 37572
		public virtual extern event HTMLInputTextElementEvents2_onmouseupEventHandler HTMLInputTextElementEvents2_Event_onmouseup;

		// Token: 0x14001099 RID: 4249
		// (add) Token: 0x060092C5 RID: 37573
		// (remove) Token: 0x060092C6 RID: 37574
		public virtual extern event HTMLInputTextElementEvents2_onselectstartEventHandler HTMLInputTextElementEvents2_Event_onselectstart;

		// Token: 0x1400109A RID: 4250
		// (add) Token: 0x060092C7 RID: 37575
		// (remove) Token: 0x060092C8 RID: 37576
		public virtual extern event HTMLInputTextElementEvents2_onfilterchangeEventHandler HTMLInputTextElementEvents2_Event_onfilterchange;

		// Token: 0x1400109B RID: 4251
		// (add) Token: 0x060092C9 RID: 37577
		// (remove) Token: 0x060092CA RID: 37578
		public virtual extern event HTMLInputTextElementEvents2_ondragstartEventHandler HTMLInputTextElementEvents2_Event_ondragstart;

		// Token: 0x1400109C RID: 4252
		// (add) Token: 0x060092CB RID: 37579
		// (remove) Token: 0x060092CC RID: 37580
		public virtual extern event HTMLInputTextElementEvents2_onbeforeupdateEventHandler HTMLInputTextElementEvents2_Event_onbeforeupdate;

		// Token: 0x1400109D RID: 4253
		// (add) Token: 0x060092CD RID: 37581
		// (remove) Token: 0x060092CE RID: 37582
		public virtual extern event HTMLInputTextElementEvents2_onafterupdateEventHandler HTMLInputTextElementEvents2_Event_onafterupdate;

		// Token: 0x1400109E RID: 4254
		// (add) Token: 0x060092CF RID: 37583
		// (remove) Token: 0x060092D0 RID: 37584
		public virtual extern event HTMLInputTextElementEvents2_onerrorupdateEventHandler HTMLInputTextElementEvents2_Event_onerrorupdate;

		// Token: 0x1400109F RID: 4255
		// (add) Token: 0x060092D1 RID: 37585
		// (remove) Token: 0x060092D2 RID: 37586
		public virtual extern event HTMLInputTextElementEvents2_onrowexitEventHandler HTMLInputTextElementEvents2_Event_onrowexit;

		// Token: 0x140010A0 RID: 4256
		// (add) Token: 0x060092D3 RID: 37587
		// (remove) Token: 0x060092D4 RID: 37588
		public virtual extern event HTMLInputTextElementEvents2_onrowenterEventHandler HTMLInputTextElementEvents2_Event_onrowenter;

		// Token: 0x140010A1 RID: 4257
		// (add) Token: 0x060092D5 RID: 37589
		// (remove) Token: 0x060092D6 RID: 37590
		public virtual extern event HTMLInputTextElementEvents2_ondatasetchangedEventHandler HTMLInputTextElementEvents2_Event_ondatasetchanged;

		// Token: 0x140010A2 RID: 4258
		// (add) Token: 0x060092D7 RID: 37591
		// (remove) Token: 0x060092D8 RID: 37592
		public virtual extern event HTMLInputTextElementEvents2_ondataavailableEventHandler HTMLInputTextElementEvents2_Event_ondataavailable;

		// Token: 0x140010A3 RID: 4259
		// (add) Token: 0x060092D9 RID: 37593
		// (remove) Token: 0x060092DA RID: 37594
		public virtual extern event HTMLInputTextElementEvents2_ondatasetcompleteEventHandler HTMLInputTextElementEvents2_Event_ondatasetcomplete;

		// Token: 0x140010A4 RID: 4260
		// (add) Token: 0x060092DB RID: 37595
		// (remove) Token: 0x060092DC RID: 37596
		public virtual extern event HTMLInputTextElementEvents2_onlosecaptureEventHandler HTMLInputTextElementEvents2_Event_onlosecapture;

		// Token: 0x140010A5 RID: 4261
		// (add) Token: 0x060092DD RID: 37597
		// (remove) Token: 0x060092DE RID: 37598
		public virtual extern event HTMLInputTextElementEvents2_onpropertychangeEventHandler HTMLInputTextElementEvents2_Event_onpropertychange;

		// Token: 0x140010A6 RID: 4262
		// (add) Token: 0x060092DF RID: 37599
		// (remove) Token: 0x060092E0 RID: 37600
		public virtual extern event HTMLInputTextElementEvents2_onscrollEventHandler HTMLInputTextElementEvents2_Event_onscroll;

		// Token: 0x140010A7 RID: 4263
		// (add) Token: 0x060092E1 RID: 37601
		// (remove) Token: 0x060092E2 RID: 37602
		public virtual extern event HTMLInputTextElementEvents2_onfocusEventHandler HTMLInputTextElementEvents2_Event_onfocus;

		// Token: 0x140010A8 RID: 4264
		// (add) Token: 0x060092E3 RID: 37603
		// (remove) Token: 0x060092E4 RID: 37604
		public virtual extern event HTMLInputTextElementEvents2_onblurEventHandler HTMLInputTextElementEvents2_Event_onblur;

		// Token: 0x140010A9 RID: 4265
		// (add) Token: 0x060092E5 RID: 37605
		// (remove) Token: 0x060092E6 RID: 37606
		public virtual extern event HTMLInputTextElementEvents2_onresizeEventHandler HTMLInputTextElementEvents2_Event_onresize;

		// Token: 0x140010AA RID: 4266
		// (add) Token: 0x060092E7 RID: 37607
		// (remove) Token: 0x060092E8 RID: 37608
		public virtual extern event HTMLInputTextElementEvents2_ondragEventHandler HTMLInputTextElementEvents2_Event_ondrag;

		// Token: 0x140010AB RID: 4267
		// (add) Token: 0x060092E9 RID: 37609
		// (remove) Token: 0x060092EA RID: 37610
		public virtual extern event HTMLInputTextElementEvents2_ondragendEventHandler HTMLInputTextElementEvents2_Event_ondragend;

		// Token: 0x140010AC RID: 4268
		// (add) Token: 0x060092EB RID: 37611
		// (remove) Token: 0x060092EC RID: 37612
		public virtual extern event HTMLInputTextElementEvents2_ondragenterEventHandler HTMLInputTextElementEvents2_Event_ondragenter;

		// Token: 0x140010AD RID: 4269
		// (add) Token: 0x060092ED RID: 37613
		// (remove) Token: 0x060092EE RID: 37614
		public virtual extern event HTMLInputTextElementEvents2_ondragoverEventHandler HTMLInputTextElementEvents2_Event_ondragover;

		// Token: 0x140010AE RID: 4270
		// (add) Token: 0x060092EF RID: 37615
		// (remove) Token: 0x060092F0 RID: 37616
		public virtual extern event HTMLInputTextElementEvents2_ondragleaveEventHandler HTMLInputTextElementEvents2_Event_ondragleave;

		// Token: 0x140010AF RID: 4271
		// (add) Token: 0x060092F1 RID: 37617
		// (remove) Token: 0x060092F2 RID: 37618
		public virtual extern event HTMLInputTextElementEvents2_ondropEventHandler HTMLInputTextElementEvents2_Event_ondrop;

		// Token: 0x140010B0 RID: 4272
		// (add) Token: 0x060092F3 RID: 37619
		// (remove) Token: 0x060092F4 RID: 37620
		public virtual extern event HTMLInputTextElementEvents2_onbeforecutEventHandler HTMLInputTextElementEvents2_Event_onbeforecut;

		// Token: 0x140010B1 RID: 4273
		// (add) Token: 0x060092F5 RID: 37621
		// (remove) Token: 0x060092F6 RID: 37622
		public virtual extern event HTMLInputTextElementEvents2_oncutEventHandler HTMLInputTextElementEvents2_Event_oncut;

		// Token: 0x140010B2 RID: 4274
		// (add) Token: 0x060092F7 RID: 37623
		// (remove) Token: 0x060092F8 RID: 37624
		public virtual extern event HTMLInputTextElementEvents2_onbeforecopyEventHandler HTMLInputTextElementEvents2_Event_onbeforecopy;

		// Token: 0x140010B3 RID: 4275
		// (add) Token: 0x060092F9 RID: 37625
		// (remove) Token: 0x060092FA RID: 37626
		public virtual extern event HTMLInputTextElementEvents2_oncopyEventHandler HTMLInputTextElementEvents2_Event_oncopy;

		// Token: 0x140010B4 RID: 4276
		// (add) Token: 0x060092FB RID: 37627
		// (remove) Token: 0x060092FC RID: 37628
		public virtual extern event HTMLInputTextElementEvents2_onbeforepasteEventHandler HTMLInputTextElementEvents2_Event_onbeforepaste;

		// Token: 0x140010B5 RID: 4277
		// (add) Token: 0x060092FD RID: 37629
		// (remove) Token: 0x060092FE RID: 37630
		public virtual extern event HTMLInputTextElementEvents2_onpasteEventHandler HTMLInputTextElementEvents2_Event_onpaste;

		// Token: 0x140010B6 RID: 4278
		// (add) Token: 0x060092FF RID: 37631
		// (remove) Token: 0x06009300 RID: 37632
		public virtual extern event HTMLInputTextElementEvents2_oncontextmenuEventHandler HTMLInputTextElementEvents2_Event_oncontextmenu;

		// Token: 0x140010B7 RID: 4279
		// (add) Token: 0x06009301 RID: 37633
		// (remove) Token: 0x06009302 RID: 37634
		public virtual extern event HTMLInputTextElementEvents2_onrowsdeleteEventHandler HTMLInputTextElementEvents2_Event_onrowsdelete;

		// Token: 0x140010B8 RID: 4280
		// (add) Token: 0x06009303 RID: 37635
		// (remove) Token: 0x06009304 RID: 37636
		public virtual extern event HTMLInputTextElementEvents2_onrowsinsertedEventHandler HTMLInputTextElementEvents2_Event_onrowsinserted;

		// Token: 0x140010B9 RID: 4281
		// (add) Token: 0x06009305 RID: 37637
		// (remove) Token: 0x06009306 RID: 37638
		public virtual extern event HTMLInputTextElementEvents2_oncellchangeEventHandler HTMLInputTextElementEvents2_Event_oncellchange;

		// Token: 0x140010BA RID: 4282
		// (add) Token: 0x06009307 RID: 37639
		// (remove) Token: 0x06009308 RID: 37640
		public virtual extern event HTMLInputTextElementEvents2_onreadystatechangeEventHandler HTMLInputTextElementEvents2_Event_onreadystatechange;

		// Token: 0x140010BB RID: 4283
		// (add) Token: 0x06009309 RID: 37641
		// (remove) Token: 0x0600930A RID: 37642
		public virtual extern event HTMLInputTextElementEvents2_onlayoutcompleteEventHandler HTMLInputTextElementEvents2_Event_onlayoutcomplete;

		// Token: 0x140010BC RID: 4284
		// (add) Token: 0x0600930B RID: 37643
		// (remove) Token: 0x0600930C RID: 37644
		public virtual extern event HTMLInputTextElementEvents2_onpageEventHandler HTMLInputTextElementEvents2_Event_onpage;

		// Token: 0x140010BD RID: 4285
		// (add) Token: 0x0600930D RID: 37645
		// (remove) Token: 0x0600930E RID: 37646
		public virtual extern event HTMLInputTextElementEvents2_onmouseenterEventHandler HTMLInputTextElementEvents2_Event_onmouseenter;

		// Token: 0x140010BE RID: 4286
		// (add) Token: 0x0600930F RID: 37647
		// (remove) Token: 0x06009310 RID: 37648
		public virtual extern event HTMLInputTextElementEvents2_onmouseleaveEventHandler HTMLInputTextElementEvents2_Event_onmouseleave;

		// Token: 0x140010BF RID: 4287
		// (add) Token: 0x06009311 RID: 37649
		// (remove) Token: 0x06009312 RID: 37650
		public virtual extern event HTMLInputTextElementEvents2_onactivateEventHandler HTMLInputTextElementEvents2_Event_onactivate;

		// Token: 0x140010C0 RID: 4288
		// (add) Token: 0x06009313 RID: 37651
		// (remove) Token: 0x06009314 RID: 37652
		public virtual extern event HTMLInputTextElementEvents2_ondeactivateEventHandler HTMLInputTextElementEvents2_Event_ondeactivate;

		// Token: 0x140010C1 RID: 4289
		// (add) Token: 0x06009315 RID: 37653
		// (remove) Token: 0x06009316 RID: 37654
		public virtual extern event HTMLInputTextElementEvents2_onbeforedeactivateEventHandler HTMLInputTextElementEvents2_Event_onbeforedeactivate;

		// Token: 0x140010C2 RID: 4290
		// (add) Token: 0x06009317 RID: 37655
		// (remove) Token: 0x06009318 RID: 37656
		public virtual extern event HTMLInputTextElementEvents2_onbeforeactivateEventHandler HTMLInputTextElementEvents2_Event_onbeforeactivate;

		// Token: 0x140010C3 RID: 4291
		// (add) Token: 0x06009319 RID: 37657
		// (remove) Token: 0x0600931A RID: 37658
		public virtual extern event HTMLInputTextElementEvents2_onfocusinEventHandler HTMLInputTextElementEvents2_Event_onfocusin;

		// Token: 0x140010C4 RID: 4292
		// (add) Token: 0x0600931B RID: 37659
		// (remove) Token: 0x0600931C RID: 37660
		public virtual extern event HTMLInputTextElementEvents2_onfocusoutEventHandler HTMLInputTextElementEvents2_Event_onfocusout;

		// Token: 0x140010C5 RID: 4293
		// (add) Token: 0x0600931D RID: 37661
		// (remove) Token: 0x0600931E RID: 37662
		public virtual extern event HTMLInputTextElementEvents2_onmoveEventHandler HTMLInputTextElementEvents2_Event_onmove;

		// Token: 0x140010C6 RID: 4294
		// (add) Token: 0x0600931F RID: 37663
		// (remove) Token: 0x06009320 RID: 37664
		public virtual extern event HTMLInputTextElementEvents2_oncontrolselectEventHandler HTMLInputTextElementEvents2_Event_oncontrolselect;

		// Token: 0x140010C7 RID: 4295
		// (add) Token: 0x06009321 RID: 37665
		// (remove) Token: 0x06009322 RID: 37666
		public virtual extern event HTMLInputTextElementEvents2_onmovestartEventHandler HTMLInputTextElementEvents2_Event_onmovestart;

		// Token: 0x140010C8 RID: 4296
		// (add) Token: 0x06009323 RID: 37667
		// (remove) Token: 0x06009324 RID: 37668
		public virtual extern event HTMLInputTextElementEvents2_onmoveendEventHandler HTMLInputTextElementEvents2_Event_onmoveend;

		// Token: 0x140010C9 RID: 4297
		// (add) Token: 0x06009325 RID: 37669
		// (remove) Token: 0x06009326 RID: 37670
		public virtual extern event HTMLInputTextElementEvents2_onresizestartEventHandler HTMLInputTextElementEvents2_Event_onresizestart;

		// Token: 0x140010CA RID: 4298
		// (add) Token: 0x06009327 RID: 37671
		// (remove) Token: 0x06009328 RID: 37672
		public virtual extern event HTMLInputTextElementEvents2_onresizeendEventHandler HTMLInputTextElementEvents2_Event_onresizeend;

		// Token: 0x140010CB RID: 4299
		// (add) Token: 0x06009329 RID: 37673
		// (remove) Token: 0x0600932A RID: 37674
		public virtual extern event HTMLInputTextElementEvents2_onmousewheelEventHandler HTMLInputTextElementEvents2_Event_onmousewheel;

		// Token: 0x140010CC RID: 4300
		// (add) Token: 0x0600932B RID: 37675
		// (remove) Token: 0x0600932C RID: 37676
		public virtual extern event HTMLInputTextElementEvents2_onchangeEventHandler HTMLInputTextElementEvents2_Event_onchange;

		// Token: 0x140010CD RID: 4301
		// (add) Token: 0x0600932D RID: 37677
		// (remove) Token: 0x0600932E RID: 37678
		public virtual extern event HTMLInputTextElementEvents2_onselectEventHandler HTMLInputTextElementEvents2_Event_onselect;

		// Token: 0x140010CE RID: 4302
		// (add) Token: 0x0600932F RID: 37679
		// (remove) Token: 0x06009330 RID: 37680
		public virtual extern event HTMLInputTextElementEvents2_onloadEventHandler HTMLInputTextElementEvents2_Event_onload;

		// Token: 0x140010CF RID: 4303
		// (add) Token: 0x06009331 RID: 37681
		// (remove) Token: 0x06009332 RID: 37682
		public virtual extern event HTMLInputTextElementEvents2_onerrorEventHandler HTMLInputTextElementEvents2_Event_onerror;

		// Token: 0x140010D0 RID: 4304
		// (add) Token: 0x06009333 RID: 37683
		// (remove) Token: 0x06009334 RID: 37684
		public virtual extern event HTMLInputTextElementEvents2_onabortEventHandler HTMLInputTextElementEvents2_Event_onabort;

		// Token: 0x140010D1 RID: 4305
		// (add) Token: 0x06009335 RID: 37685
		// (remove) Token: 0x06009336 RID: 37686
		public virtual extern event HTMLOptionButtonElementEvents_onhelpEventHandler HTMLOptionButtonElementEvents_Event_onhelp;

		// Token: 0x140010D2 RID: 4306
		// (add) Token: 0x06009337 RID: 37687
		// (remove) Token: 0x06009338 RID: 37688
		public virtual extern event HTMLOptionButtonElementEvents_onclickEventHandler HTMLOptionButtonElementEvents_Event_onclick;

		// Token: 0x140010D3 RID: 4307
		// (add) Token: 0x06009339 RID: 37689
		// (remove) Token: 0x0600933A RID: 37690
		public virtual extern event HTMLOptionButtonElementEvents_ondblclickEventHandler HTMLOptionButtonElementEvents_Event_ondblclick;

		// Token: 0x140010D4 RID: 4308
		// (add) Token: 0x0600933B RID: 37691
		// (remove) Token: 0x0600933C RID: 37692
		public virtual extern event HTMLOptionButtonElementEvents_onkeypressEventHandler HTMLOptionButtonElementEvents_Event_onkeypress;

		// Token: 0x140010D5 RID: 4309
		// (add) Token: 0x0600933D RID: 37693
		// (remove) Token: 0x0600933E RID: 37694
		public virtual extern event HTMLOptionButtonElementEvents_onkeydownEventHandler HTMLOptionButtonElementEvents_Event_onkeydown;

		// Token: 0x140010D6 RID: 4310
		// (add) Token: 0x0600933F RID: 37695
		// (remove) Token: 0x06009340 RID: 37696
		public virtual extern event HTMLOptionButtonElementEvents_onkeyupEventHandler HTMLOptionButtonElementEvents_Event_onkeyup;

		// Token: 0x140010D7 RID: 4311
		// (add) Token: 0x06009341 RID: 37697
		// (remove) Token: 0x06009342 RID: 37698
		public virtual extern event HTMLOptionButtonElementEvents_onmouseoutEventHandler HTMLOptionButtonElementEvents_Event_onmouseout;

		// Token: 0x140010D8 RID: 4312
		// (add) Token: 0x06009343 RID: 37699
		// (remove) Token: 0x06009344 RID: 37700
		public virtual extern event HTMLOptionButtonElementEvents_onmouseoverEventHandler HTMLOptionButtonElementEvents_Event_onmouseover;

		// Token: 0x140010D9 RID: 4313
		// (add) Token: 0x06009345 RID: 37701
		// (remove) Token: 0x06009346 RID: 37702
		public virtual extern event HTMLOptionButtonElementEvents_onmousemoveEventHandler HTMLOptionButtonElementEvents_Event_onmousemove;

		// Token: 0x140010DA RID: 4314
		// (add) Token: 0x06009347 RID: 37703
		// (remove) Token: 0x06009348 RID: 37704
		public virtual extern event HTMLOptionButtonElementEvents_onmousedownEventHandler HTMLOptionButtonElementEvents_Event_onmousedown;

		// Token: 0x140010DB RID: 4315
		// (add) Token: 0x06009349 RID: 37705
		// (remove) Token: 0x0600934A RID: 37706
		public virtual extern event HTMLOptionButtonElementEvents_onmouseupEventHandler HTMLOptionButtonElementEvents_Event_onmouseup;

		// Token: 0x140010DC RID: 4316
		// (add) Token: 0x0600934B RID: 37707
		// (remove) Token: 0x0600934C RID: 37708
		public virtual extern event HTMLOptionButtonElementEvents_onselectstartEventHandler HTMLOptionButtonElementEvents_Event_onselectstart;

		// Token: 0x140010DD RID: 4317
		// (add) Token: 0x0600934D RID: 37709
		// (remove) Token: 0x0600934E RID: 37710
		public virtual extern event HTMLOptionButtonElementEvents_onfilterchangeEventHandler HTMLOptionButtonElementEvents_Event_onfilterchange;

		// Token: 0x140010DE RID: 4318
		// (add) Token: 0x0600934F RID: 37711
		// (remove) Token: 0x06009350 RID: 37712
		public virtual extern event HTMLOptionButtonElementEvents_ondragstartEventHandler HTMLOptionButtonElementEvents_Event_ondragstart;

		// Token: 0x140010DF RID: 4319
		// (add) Token: 0x06009351 RID: 37713
		// (remove) Token: 0x06009352 RID: 37714
		public virtual extern event HTMLOptionButtonElementEvents_onbeforeupdateEventHandler HTMLOptionButtonElementEvents_Event_onbeforeupdate;

		// Token: 0x140010E0 RID: 4320
		// (add) Token: 0x06009353 RID: 37715
		// (remove) Token: 0x06009354 RID: 37716
		public virtual extern event HTMLOptionButtonElementEvents_onafterupdateEventHandler HTMLOptionButtonElementEvents_Event_onafterupdate;

		// Token: 0x140010E1 RID: 4321
		// (add) Token: 0x06009355 RID: 37717
		// (remove) Token: 0x06009356 RID: 37718
		public virtual extern event HTMLOptionButtonElementEvents_onerrorupdateEventHandler HTMLOptionButtonElementEvents_Event_onerrorupdate;

		// Token: 0x140010E2 RID: 4322
		// (add) Token: 0x06009357 RID: 37719
		// (remove) Token: 0x06009358 RID: 37720
		public virtual extern event HTMLOptionButtonElementEvents_onrowexitEventHandler HTMLOptionButtonElementEvents_Event_onrowexit;

		// Token: 0x140010E3 RID: 4323
		// (add) Token: 0x06009359 RID: 37721
		// (remove) Token: 0x0600935A RID: 37722
		public virtual extern event HTMLOptionButtonElementEvents_onrowenterEventHandler HTMLOptionButtonElementEvents_Event_onrowenter;

		// Token: 0x140010E4 RID: 4324
		// (add) Token: 0x0600935B RID: 37723
		// (remove) Token: 0x0600935C RID: 37724
		public virtual extern event HTMLOptionButtonElementEvents_ondatasetchangedEventHandler HTMLOptionButtonElementEvents_Event_ondatasetchanged;

		// Token: 0x140010E5 RID: 4325
		// (add) Token: 0x0600935D RID: 37725
		// (remove) Token: 0x0600935E RID: 37726
		public virtual extern event HTMLOptionButtonElementEvents_ondataavailableEventHandler HTMLOptionButtonElementEvents_Event_ondataavailable;

		// Token: 0x140010E6 RID: 4326
		// (add) Token: 0x0600935F RID: 37727
		// (remove) Token: 0x06009360 RID: 37728
		public virtual extern event HTMLOptionButtonElementEvents_ondatasetcompleteEventHandler HTMLOptionButtonElementEvents_Event_ondatasetcomplete;

		// Token: 0x140010E7 RID: 4327
		// (add) Token: 0x06009361 RID: 37729
		// (remove) Token: 0x06009362 RID: 37730
		public virtual extern event HTMLOptionButtonElementEvents_onlosecaptureEventHandler HTMLOptionButtonElementEvents_Event_onlosecapture;

		// Token: 0x140010E8 RID: 4328
		// (add) Token: 0x06009363 RID: 37731
		// (remove) Token: 0x06009364 RID: 37732
		public virtual extern event HTMLOptionButtonElementEvents_onpropertychangeEventHandler HTMLOptionButtonElementEvents_Event_onpropertychange;

		// Token: 0x140010E9 RID: 4329
		// (add) Token: 0x06009365 RID: 37733
		// (remove) Token: 0x06009366 RID: 37734
		public virtual extern event HTMLOptionButtonElementEvents_onscrollEventHandler HTMLOptionButtonElementEvents_Event_onscroll;

		// Token: 0x140010EA RID: 4330
		// (add) Token: 0x06009367 RID: 37735
		// (remove) Token: 0x06009368 RID: 37736
		public virtual extern event HTMLOptionButtonElementEvents_onfocusEventHandler HTMLOptionButtonElementEvents_Event_onfocus;

		// Token: 0x140010EB RID: 4331
		// (add) Token: 0x06009369 RID: 37737
		// (remove) Token: 0x0600936A RID: 37738
		public virtual extern event HTMLOptionButtonElementEvents_onblurEventHandler HTMLOptionButtonElementEvents_Event_onblur;

		// Token: 0x140010EC RID: 4332
		// (add) Token: 0x0600936B RID: 37739
		// (remove) Token: 0x0600936C RID: 37740
		public virtual extern event HTMLOptionButtonElementEvents_onresizeEventHandler HTMLOptionButtonElementEvents_Event_onresize;

		// Token: 0x140010ED RID: 4333
		// (add) Token: 0x0600936D RID: 37741
		// (remove) Token: 0x0600936E RID: 37742
		public virtual extern event HTMLOptionButtonElementEvents_ondragEventHandler HTMLOptionButtonElementEvents_Event_ondrag;

		// Token: 0x140010EE RID: 4334
		// (add) Token: 0x0600936F RID: 37743
		// (remove) Token: 0x06009370 RID: 37744
		public virtual extern event HTMLOptionButtonElementEvents_ondragendEventHandler HTMLOptionButtonElementEvents_Event_ondragend;

		// Token: 0x140010EF RID: 4335
		// (add) Token: 0x06009371 RID: 37745
		// (remove) Token: 0x06009372 RID: 37746
		public virtual extern event HTMLOptionButtonElementEvents_ondragenterEventHandler HTMLOptionButtonElementEvents_Event_ondragenter;

		// Token: 0x140010F0 RID: 4336
		// (add) Token: 0x06009373 RID: 37747
		// (remove) Token: 0x06009374 RID: 37748
		public virtual extern event HTMLOptionButtonElementEvents_ondragoverEventHandler HTMLOptionButtonElementEvents_Event_ondragover;

		// Token: 0x140010F1 RID: 4337
		// (add) Token: 0x06009375 RID: 37749
		// (remove) Token: 0x06009376 RID: 37750
		public virtual extern event HTMLOptionButtonElementEvents_ondragleaveEventHandler HTMLOptionButtonElementEvents_Event_ondragleave;

		// Token: 0x140010F2 RID: 4338
		// (add) Token: 0x06009377 RID: 37751
		// (remove) Token: 0x06009378 RID: 37752
		public virtual extern event HTMLOptionButtonElementEvents_ondropEventHandler HTMLOptionButtonElementEvents_Event_ondrop;

		// Token: 0x140010F3 RID: 4339
		// (add) Token: 0x06009379 RID: 37753
		// (remove) Token: 0x0600937A RID: 37754
		public virtual extern event HTMLOptionButtonElementEvents_onbeforecutEventHandler HTMLOptionButtonElementEvents_Event_onbeforecut;

		// Token: 0x140010F4 RID: 4340
		// (add) Token: 0x0600937B RID: 37755
		// (remove) Token: 0x0600937C RID: 37756
		public virtual extern event HTMLOptionButtonElementEvents_oncutEventHandler HTMLOptionButtonElementEvents_Event_oncut;

		// Token: 0x140010F5 RID: 4341
		// (add) Token: 0x0600937D RID: 37757
		// (remove) Token: 0x0600937E RID: 37758
		public virtual extern event HTMLOptionButtonElementEvents_onbeforecopyEventHandler HTMLOptionButtonElementEvents_Event_onbeforecopy;

		// Token: 0x140010F6 RID: 4342
		// (add) Token: 0x0600937F RID: 37759
		// (remove) Token: 0x06009380 RID: 37760
		public virtual extern event HTMLOptionButtonElementEvents_oncopyEventHandler HTMLOptionButtonElementEvents_Event_oncopy;

		// Token: 0x140010F7 RID: 4343
		// (add) Token: 0x06009381 RID: 37761
		// (remove) Token: 0x06009382 RID: 37762
		public virtual extern event HTMLOptionButtonElementEvents_onbeforepasteEventHandler HTMLOptionButtonElementEvents_Event_onbeforepaste;

		// Token: 0x140010F8 RID: 4344
		// (add) Token: 0x06009383 RID: 37763
		// (remove) Token: 0x06009384 RID: 37764
		public virtual extern event HTMLOptionButtonElementEvents_onpasteEventHandler HTMLOptionButtonElementEvents_Event_onpaste;

		// Token: 0x140010F9 RID: 4345
		// (add) Token: 0x06009385 RID: 37765
		// (remove) Token: 0x06009386 RID: 37766
		public virtual extern event HTMLOptionButtonElementEvents_oncontextmenuEventHandler HTMLOptionButtonElementEvents_Event_oncontextmenu;

		// Token: 0x140010FA RID: 4346
		// (add) Token: 0x06009387 RID: 37767
		// (remove) Token: 0x06009388 RID: 37768
		public virtual extern event HTMLOptionButtonElementEvents_onrowsdeleteEventHandler HTMLOptionButtonElementEvents_Event_onrowsdelete;

		// Token: 0x140010FB RID: 4347
		// (add) Token: 0x06009389 RID: 37769
		// (remove) Token: 0x0600938A RID: 37770
		public virtual extern event HTMLOptionButtonElementEvents_onrowsinsertedEventHandler HTMLOptionButtonElementEvents_Event_onrowsinserted;

		// Token: 0x140010FC RID: 4348
		// (add) Token: 0x0600938B RID: 37771
		// (remove) Token: 0x0600938C RID: 37772
		public virtual extern event HTMLOptionButtonElementEvents_oncellchangeEventHandler HTMLOptionButtonElementEvents_Event_oncellchange;

		// Token: 0x140010FD RID: 4349
		// (add) Token: 0x0600938D RID: 37773
		// (remove) Token: 0x0600938E RID: 37774
		public virtual extern event HTMLOptionButtonElementEvents_onreadystatechangeEventHandler HTMLOptionButtonElementEvents_Event_onreadystatechange;

		// Token: 0x140010FE RID: 4350
		// (add) Token: 0x0600938F RID: 37775
		// (remove) Token: 0x06009390 RID: 37776
		public virtual extern event HTMLOptionButtonElementEvents_onbeforeeditfocusEventHandler HTMLOptionButtonElementEvents_Event_onbeforeeditfocus;

		// Token: 0x140010FF RID: 4351
		// (add) Token: 0x06009391 RID: 37777
		// (remove) Token: 0x06009392 RID: 37778
		public virtual extern event HTMLOptionButtonElementEvents_onlayoutcompleteEventHandler HTMLOptionButtonElementEvents_Event_onlayoutcomplete;

		// Token: 0x14001100 RID: 4352
		// (add) Token: 0x06009393 RID: 37779
		// (remove) Token: 0x06009394 RID: 37780
		public virtual extern event HTMLOptionButtonElementEvents_onpageEventHandler HTMLOptionButtonElementEvents_Event_onpage;

		// Token: 0x14001101 RID: 4353
		// (add) Token: 0x06009395 RID: 37781
		// (remove) Token: 0x06009396 RID: 37782
		public virtual extern event HTMLOptionButtonElementEvents_onbeforedeactivateEventHandler HTMLOptionButtonElementEvents_Event_onbeforedeactivate;

		// Token: 0x14001102 RID: 4354
		// (add) Token: 0x06009397 RID: 37783
		// (remove) Token: 0x06009398 RID: 37784
		public virtual extern event HTMLOptionButtonElementEvents_onbeforeactivateEventHandler HTMLOptionButtonElementEvents_Event_onbeforeactivate;

		// Token: 0x14001103 RID: 4355
		// (add) Token: 0x06009399 RID: 37785
		// (remove) Token: 0x0600939A RID: 37786
		public virtual extern event HTMLOptionButtonElementEvents_onmoveEventHandler HTMLOptionButtonElementEvents_Event_onmove;

		// Token: 0x14001104 RID: 4356
		// (add) Token: 0x0600939B RID: 37787
		// (remove) Token: 0x0600939C RID: 37788
		public virtual extern event HTMLOptionButtonElementEvents_oncontrolselectEventHandler HTMLOptionButtonElementEvents_Event_oncontrolselect;

		// Token: 0x14001105 RID: 4357
		// (add) Token: 0x0600939D RID: 37789
		// (remove) Token: 0x0600939E RID: 37790
		public virtual extern event HTMLOptionButtonElementEvents_onmovestartEventHandler HTMLOptionButtonElementEvents_Event_onmovestart;

		// Token: 0x14001106 RID: 4358
		// (add) Token: 0x0600939F RID: 37791
		// (remove) Token: 0x060093A0 RID: 37792
		public virtual extern event HTMLOptionButtonElementEvents_onmoveendEventHandler HTMLOptionButtonElementEvents_Event_onmoveend;

		// Token: 0x14001107 RID: 4359
		// (add) Token: 0x060093A1 RID: 37793
		// (remove) Token: 0x060093A2 RID: 37794
		public virtual extern event HTMLOptionButtonElementEvents_onresizestartEventHandler HTMLOptionButtonElementEvents_Event_onresizestart;

		// Token: 0x14001108 RID: 4360
		// (add) Token: 0x060093A3 RID: 37795
		// (remove) Token: 0x060093A4 RID: 37796
		public virtual extern event HTMLOptionButtonElementEvents_onresizeendEventHandler HTMLOptionButtonElementEvents_Event_onresizeend;

		// Token: 0x14001109 RID: 4361
		// (add) Token: 0x060093A5 RID: 37797
		// (remove) Token: 0x060093A6 RID: 37798
		public virtual extern event HTMLOptionButtonElementEvents_onmouseenterEventHandler HTMLOptionButtonElementEvents_Event_onmouseenter;

		// Token: 0x1400110A RID: 4362
		// (add) Token: 0x060093A7 RID: 37799
		// (remove) Token: 0x060093A8 RID: 37800
		public virtual extern event HTMLOptionButtonElementEvents_onmouseleaveEventHandler HTMLOptionButtonElementEvents_Event_onmouseleave;

		// Token: 0x1400110B RID: 4363
		// (add) Token: 0x060093A9 RID: 37801
		// (remove) Token: 0x060093AA RID: 37802
		public virtual extern event HTMLOptionButtonElementEvents_onmousewheelEventHandler HTMLOptionButtonElementEvents_Event_onmousewheel;

		// Token: 0x1400110C RID: 4364
		// (add) Token: 0x060093AB RID: 37803
		// (remove) Token: 0x060093AC RID: 37804
		public virtual extern event HTMLOptionButtonElementEvents_onactivateEventHandler HTMLOptionButtonElementEvents_Event_onactivate;

		// Token: 0x1400110D RID: 4365
		// (add) Token: 0x060093AD RID: 37805
		// (remove) Token: 0x060093AE RID: 37806
		public virtual extern event HTMLOptionButtonElementEvents_ondeactivateEventHandler HTMLOptionButtonElementEvents_Event_ondeactivate;

		// Token: 0x1400110E RID: 4366
		// (add) Token: 0x060093AF RID: 37807
		// (remove) Token: 0x060093B0 RID: 37808
		public virtual extern event HTMLOptionButtonElementEvents_onfocusinEventHandler HTMLOptionButtonElementEvents_Event_onfocusin;

		// Token: 0x1400110F RID: 4367
		// (add) Token: 0x060093B1 RID: 37809
		// (remove) Token: 0x060093B2 RID: 37810
		public virtual extern event HTMLOptionButtonElementEvents_onfocusoutEventHandler HTMLOptionButtonElementEvents_Event_onfocusout;

		// Token: 0x14001110 RID: 4368
		// (add) Token: 0x060093B3 RID: 37811
		// (remove) Token: 0x060093B4 RID: 37812
		public virtual extern event HTMLOptionButtonElementEvents_onchangeEventHandler HTMLOptionButtonElementEvents_Event_onchange;

		// Token: 0x14001111 RID: 4369
		// (add) Token: 0x060093B5 RID: 37813
		// (remove) Token: 0x060093B6 RID: 37814
		public virtual extern event HTMLOptionButtonElementEvents_onselectEventHandler HTMLOptionButtonElementEvents_Event_onselect;

		// Token: 0x14001112 RID: 4370
		// (add) Token: 0x060093B7 RID: 37815
		// (remove) Token: 0x060093B8 RID: 37816
		public virtual extern event HTMLOptionButtonElementEvents_onloadEventHandler HTMLOptionButtonElementEvents_Event_onload;

		// Token: 0x14001113 RID: 4371
		// (add) Token: 0x060093B9 RID: 37817
		// (remove) Token: 0x060093BA RID: 37818
		public virtual extern event HTMLOptionButtonElementEvents_onerrorEventHandler HTMLOptionButtonElementEvents_Event_onerror;

		// Token: 0x14001114 RID: 4372
		// (add) Token: 0x060093BB RID: 37819
		// (remove) Token: 0x060093BC RID: 37820
		public virtual extern event HTMLOptionButtonElementEvents_onabortEventHandler HTMLOptionButtonElementEvents_Event_onabort;

		// Token: 0x14001115 RID: 4373
		// (add) Token: 0x060093BD RID: 37821
		// (remove) Token: 0x060093BE RID: 37822
		public virtual extern event HTMLButtonElementEvents_onhelpEventHandler HTMLButtonElementEvents_Event_onhelp;

		// Token: 0x14001116 RID: 4374
		// (add) Token: 0x060093BF RID: 37823
		// (remove) Token: 0x060093C0 RID: 37824
		public virtual extern event HTMLButtonElementEvents_onclickEventHandler HTMLButtonElementEvents_Event_onclick;

		// Token: 0x14001117 RID: 4375
		// (add) Token: 0x060093C1 RID: 37825
		// (remove) Token: 0x060093C2 RID: 37826
		public virtual extern event HTMLButtonElementEvents_ondblclickEventHandler HTMLButtonElementEvents_Event_ondblclick;

		// Token: 0x14001118 RID: 4376
		// (add) Token: 0x060093C3 RID: 37827
		// (remove) Token: 0x060093C4 RID: 37828
		public virtual extern event HTMLButtonElementEvents_onkeypressEventHandler HTMLButtonElementEvents_Event_onkeypress;

		// Token: 0x14001119 RID: 4377
		// (add) Token: 0x060093C5 RID: 37829
		// (remove) Token: 0x060093C6 RID: 37830
		public virtual extern event HTMLButtonElementEvents_onkeydownEventHandler HTMLButtonElementEvents_Event_onkeydown;

		// Token: 0x1400111A RID: 4378
		// (add) Token: 0x060093C7 RID: 37831
		// (remove) Token: 0x060093C8 RID: 37832
		public virtual extern event HTMLButtonElementEvents_onkeyupEventHandler HTMLButtonElementEvents_Event_onkeyup;

		// Token: 0x1400111B RID: 4379
		// (add) Token: 0x060093C9 RID: 37833
		// (remove) Token: 0x060093CA RID: 37834
		public virtual extern event HTMLButtonElementEvents_onmouseoutEventHandler HTMLButtonElementEvents_Event_onmouseout;

		// Token: 0x1400111C RID: 4380
		// (add) Token: 0x060093CB RID: 37835
		// (remove) Token: 0x060093CC RID: 37836
		public virtual extern event HTMLButtonElementEvents_onmouseoverEventHandler HTMLButtonElementEvents_Event_onmouseover;

		// Token: 0x1400111D RID: 4381
		// (add) Token: 0x060093CD RID: 37837
		// (remove) Token: 0x060093CE RID: 37838
		public virtual extern event HTMLButtonElementEvents_onmousemoveEventHandler HTMLButtonElementEvents_Event_onmousemove;

		// Token: 0x1400111E RID: 4382
		// (add) Token: 0x060093CF RID: 37839
		// (remove) Token: 0x060093D0 RID: 37840
		public virtual extern event HTMLButtonElementEvents_onmousedownEventHandler HTMLButtonElementEvents_Event_onmousedown;

		// Token: 0x1400111F RID: 4383
		// (add) Token: 0x060093D1 RID: 37841
		// (remove) Token: 0x060093D2 RID: 37842
		public virtual extern event HTMLButtonElementEvents_onmouseupEventHandler HTMLButtonElementEvents_Event_onmouseup;

		// Token: 0x14001120 RID: 4384
		// (add) Token: 0x060093D3 RID: 37843
		// (remove) Token: 0x060093D4 RID: 37844
		public virtual extern event HTMLButtonElementEvents_onselectstartEventHandler HTMLButtonElementEvents_Event_onselectstart;

		// Token: 0x14001121 RID: 4385
		// (add) Token: 0x060093D5 RID: 37845
		// (remove) Token: 0x060093D6 RID: 37846
		public virtual extern event HTMLButtonElementEvents_onfilterchangeEventHandler HTMLButtonElementEvents_Event_onfilterchange;

		// Token: 0x14001122 RID: 4386
		// (add) Token: 0x060093D7 RID: 37847
		// (remove) Token: 0x060093D8 RID: 37848
		public virtual extern event HTMLButtonElementEvents_ondragstartEventHandler HTMLButtonElementEvents_Event_ondragstart;

		// Token: 0x14001123 RID: 4387
		// (add) Token: 0x060093D9 RID: 37849
		// (remove) Token: 0x060093DA RID: 37850
		public virtual extern event HTMLButtonElementEvents_onbeforeupdateEventHandler HTMLButtonElementEvents_Event_onbeforeupdate;

		// Token: 0x14001124 RID: 4388
		// (add) Token: 0x060093DB RID: 37851
		// (remove) Token: 0x060093DC RID: 37852
		public virtual extern event HTMLButtonElementEvents_onafterupdateEventHandler HTMLButtonElementEvents_Event_onafterupdate;

		// Token: 0x14001125 RID: 4389
		// (add) Token: 0x060093DD RID: 37853
		// (remove) Token: 0x060093DE RID: 37854
		public virtual extern event HTMLButtonElementEvents_onerrorupdateEventHandler HTMLButtonElementEvents_Event_onerrorupdate;

		// Token: 0x14001126 RID: 4390
		// (add) Token: 0x060093DF RID: 37855
		// (remove) Token: 0x060093E0 RID: 37856
		public virtual extern event HTMLButtonElementEvents_onrowexitEventHandler HTMLButtonElementEvents_Event_onrowexit;

		// Token: 0x14001127 RID: 4391
		// (add) Token: 0x060093E1 RID: 37857
		// (remove) Token: 0x060093E2 RID: 37858
		public virtual extern event HTMLButtonElementEvents_onrowenterEventHandler HTMLButtonElementEvents_Event_onrowenter;

		// Token: 0x14001128 RID: 4392
		// (add) Token: 0x060093E3 RID: 37859
		// (remove) Token: 0x060093E4 RID: 37860
		public virtual extern event HTMLButtonElementEvents_ondatasetchangedEventHandler HTMLButtonElementEvents_Event_ondatasetchanged;

		// Token: 0x14001129 RID: 4393
		// (add) Token: 0x060093E5 RID: 37861
		// (remove) Token: 0x060093E6 RID: 37862
		public virtual extern event HTMLButtonElementEvents_ondataavailableEventHandler HTMLButtonElementEvents_Event_ondataavailable;

		// Token: 0x1400112A RID: 4394
		// (add) Token: 0x060093E7 RID: 37863
		// (remove) Token: 0x060093E8 RID: 37864
		public virtual extern event HTMLButtonElementEvents_ondatasetcompleteEventHandler HTMLButtonElementEvents_Event_ondatasetcomplete;

		// Token: 0x1400112B RID: 4395
		// (add) Token: 0x060093E9 RID: 37865
		// (remove) Token: 0x060093EA RID: 37866
		public virtual extern event HTMLButtonElementEvents_onlosecaptureEventHandler HTMLButtonElementEvents_Event_onlosecapture;

		// Token: 0x1400112C RID: 4396
		// (add) Token: 0x060093EB RID: 37867
		// (remove) Token: 0x060093EC RID: 37868
		public virtual extern event HTMLButtonElementEvents_onpropertychangeEventHandler HTMLButtonElementEvents_Event_onpropertychange;

		// Token: 0x1400112D RID: 4397
		// (add) Token: 0x060093ED RID: 37869
		// (remove) Token: 0x060093EE RID: 37870
		public virtual extern event HTMLButtonElementEvents_onscrollEventHandler HTMLButtonElementEvents_Event_onscroll;

		// Token: 0x1400112E RID: 4398
		// (add) Token: 0x060093EF RID: 37871
		// (remove) Token: 0x060093F0 RID: 37872
		public virtual extern event HTMLButtonElementEvents_onfocusEventHandler HTMLButtonElementEvents_Event_onfocus;

		// Token: 0x1400112F RID: 4399
		// (add) Token: 0x060093F1 RID: 37873
		// (remove) Token: 0x060093F2 RID: 37874
		public virtual extern event HTMLButtonElementEvents_onblurEventHandler HTMLButtonElementEvents_Event_onblur;

		// Token: 0x14001130 RID: 4400
		// (add) Token: 0x060093F3 RID: 37875
		// (remove) Token: 0x060093F4 RID: 37876
		public virtual extern event HTMLButtonElementEvents_onresizeEventHandler HTMLButtonElementEvents_Event_onresize;

		// Token: 0x14001131 RID: 4401
		// (add) Token: 0x060093F5 RID: 37877
		// (remove) Token: 0x060093F6 RID: 37878
		public virtual extern event HTMLButtonElementEvents_ondragEventHandler HTMLButtonElementEvents_Event_ondrag;

		// Token: 0x14001132 RID: 4402
		// (add) Token: 0x060093F7 RID: 37879
		// (remove) Token: 0x060093F8 RID: 37880
		public virtual extern event HTMLButtonElementEvents_ondragendEventHandler HTMLButtonElementEvents_Event_ondragend;

		// Token: 0x14001133 RID: 4403
		// (add) Token: 0x060093F9 RID: 37881
		// (remove) Token: 0x060093FA RID: 37882
		public virtual extern event HTMLButtonElementEvents_ondragenterEventHandler HTMLButtonElementEvents_Event_ondragenter;

		// Token: 0x14001134 RID: 4404
		// (add) Token: 0x060093FB RID: 37883
		// (remove) Token: 0x060093FC RID: 37884
		public virtual extern event HTMLButtonElementEvents_ondragoverEventHandler HTMLButtonElementEvents_Event_ondragover;

		// Token: 0x14001135 RID: 4405
		// (add) Token: 0x060093FD RID: 37885
		// (remove) Token: 0x060093FE RID: 37886
		public virtual extern event HTMLButtonElementEvents_ondragleaveEventHandler HTMLButtonElementEvents_Event_ondragleave;

		// Token: 0x14001136 RID: 4406
		// (add) Token: 0x060093FF RID: 37887
		// (remove) Token: 0x06009400 RID: 37888
		public virtual extern event HTMLButtonElementEvents_ondropEventHandler HTMLButtonElementEvents_Event_ondrop;

		// Token: 0x14001137 RID: 4407
		// (add) Token: 0x06009401 RID: 37889
		// (remove) Token: 0x06009402 RID: 37890
		public virtual extern event HTMLButtonElementEvents_onbeforecutEventHandler HTMLButtonElementEvents_Event_onbeforecut;

		// Token: 0x14001138 RID: 4408
		// (add) Token: 0x06009403 RID: 37891
		// (remove) Token: 0x06009404 RID: 37892
		public virtual extern event HTMLButtonElementEvents_oncutEventHandler HTMLButtonElementEvents_Event_oncut;

		// Token: 0x14001139 RID: 4409
		// (add) Token: 0x06009405 RID: 37893
		// (remove) Token: 0x06009406 RID: 37894
		public virtual extern event HTMLButtonElementEvents_onbeforecopyEventHandler HTMLButtonElementEvents_Event_onbeforecopy;

		// Token: 0x1400113A RID: 4410
		// (add) Token: 0x06009407 RID: 37895
		// (remove) Token: 0x06009408 RID: 37896
		public virtual extern event HTMLButtonElementEvents_oncopyEventHandler HTMLButtonElementEvents_Event_oncopy;

		// Token: 0x1400113B RID: 4411
		// (add) Token: 0x06009409 RID: 37897
		// (remove) Token: 0x0600940A RID: 37898
		public virtual extern event HTMLButtonElementEvents_onbeforepasteEventHandler HTMLButtonElementEvents_Event_onbeforepaste;

		// Token: 0x1400113C RID: 4412
		// (add) Token: 0x0600940B RID: 37899
		// (remove) Token: 0x0600940C RID: 37900
		public virtual extern event HTMLButtonElementEvents_onpasteEventHandler HTMLButtonElementEvents_Event_onpaste;

		// Token: 0x1400113D RID: 4413
		// (add) Token: 0x0600940D RID: 37901
		// (remove) Token: 0x0600940E RID: 37902
		public virtual extern event HTMLButtonElementEvents_oncontextmenuEventHandler HTMLButtonElementEvents_Event_oncontextmenu;

		// Token: 0x1400113E RID: 4414
		// (add) Token: 0x0600940F RID: 37903
		// (remove) Token: 0x06009410 RID: 37904
		public virtual extern event HTMLButtonElementEvents_onrowsdeleteEventHandler HTMLButtonElementEvents_Event_onrowsdelete;

		// Token: 0x1400113F RID: 4415
		// (add) Token: 0x06009411 RID: 37905
		// (remove) Token: 0x06009412 RID: 37906
		public virtual extern event HTMLButtonElementEvents_onrowsinsertedEventHandler HTMLButtonElementEvents_Event_onrowsinserted;

		// Token: 0x14001140 RID: 4416
		// (add) Token: 0x06009413 RID: 37907
		// (remove) Token: 0x06009414 RID: 37908
		public virtual extern event HTMLButtonElementEvents_oncellchangeEventHandler HTMLButtonElementEvents_Event_oncellchange;

		// Token: 0x14001141 RID: 4417
		// (add) Token: 0x06009415 RID: 37909
		// (remove) Token: 0x06009416 RID: 37910
		public virtual extern event HTMLButtonElementEvents_onreadystatechangeEventHandler HTMLButtonElementEvents_Event_onreadystatechange;

		// Token: 0x14001142 RID: 4418
		// (add) Token: 0x06009417 RID: 37911
		// (remove) Token: 0x06009418 RID: 37912
		public virtual extern event HTMLButtonElementEvents_onbeforeeditfocusEventHandler HTMLButtonElementEvents_Event_onbeforeeditfocus;

		// Token: 0x14001143 RID: 4419
		// (add) Token: 0x06009419 RID: 37913
		// (remove) Token: 0x0600941A RID: 37914
		public virtual extern event HTMLButtonElementEvents_onlayoutcompleteEventHandler HTMLButtonElementEvents_Event_onlayoutcomplete;

		// Token: 0x14001144 RID: 4420
		// (add) Token: 0x0600941B RID: 37915
		// (remove) Token: 0x0600941C RID: 37916
		public virtual extern event HTMLButtonElementEvents_onpageEventHandler HTMLButtonElementEvents_Event_onpage;

		// Token: 0x14001145 RID: 4421
		// (add) Token: 0x0600941D RID: 37917
		// (remove) Token: 0x0600941E RID: 37918
		public virtual extern event HTMLButtonElementEvents_onbeforedeactivateEventHandler HTMLButtonElementEvents_Event_onbeforedeactivate;

		// Token: 0x14001146 RID: 4422
		// (add) Token: 0x0600941F RID: 37919
		// (remove) Token: 0x06009420 RID: 37920
		public virtual extern event HTMLButtonElementEvents_onbeforeactivateEventHandler HTMLButtonElementEvents_Event_onbeforeactivate;

		// Token: 0x14001147 RID: 4423
		// (add) Token: 0x06009421 RID: 37921
		// (remove) Token: 0x06009422 RID: 37922
		public virtual extern event HTMLButtonElementEvents_onmoveEventHandler HTMLButtonElementEvents_Event_onmove;

		// Token: 0x14001148 RID: 4424
		// (add) Token: 0x06009423 RID: 37923
		// (remove) Token: 0x06009424 RID: 37924
		public virtual extern event HTMLButtonElementEvents_oncontrolselectEventHandler HTMLButtonElementEvents_Event_oncontrolselect;

		// Token: 0x14001149 RID: 4425
		// (add) Token: 0x06009425 RID: 37925
		// (remove) Token: 0x06009426 RID: 37926
		public virtual extern event HTMLButtonElementEvents_onmovestartEventHandler HTMLButtonElementEvents_Event_onmovestart;

		// Token: 0x1400114A RID: 4426
		// (add) Token: 0x06009427 RID: 37927
		// (remove) Token: 0x06009428 RID: 37928
		public virtual extern event HTMLButtonElementEvents_onmoveendEventHandler HTMLButtonElementEvents_Event_onmoveend;

		// Token: 0x1400114B RID: 4427
		// (add) Token: 0x06009429 RID: 37929
		// (remove) Token: 0x0600942A RID: 37930
		public virtual extern event HTMLButtonElementEvents_onresizestartEventHandler HTMLButtonElementEvents_Event_onresizestart;

		// Token: 0x1400114C RID: 4428
		// (add) Token: 0x0600942B RID: 37931
		// (remove) Token: 0x0600942C RID: 37932
		public virtual extern event HTMLButtonElementEvents_onresizeendEventHandler HTMLButtonElementEvents_Event_onresizeend;

		// Token: 0x1400114D RID: 4429
		// (add) Token: 0x0600942D RID: 37933
		// (remove) Token: 0x0600942E RID: 37934
		public virtual extern event HTMLButtonElementEvents_onmouseenterEventHandler HTMLButtonElementEvents_Event_onmouseenter;

		// Token: 0x1400114E RID: 4430
		// (add) Token: 0x0600942F RID: 37935
		// (remove) Token: 0x06009430 RID: 37936
		public virtual extern event HTMLButtonElementEvents_onmouseleaveEventHandler HTMLButtonElementEvents_Event_onmouseleave;

		// Token: 0x1400114F RID: 4431
		// (add) Token: 0x06009431 RID: 37937
		// (remove) Token: 0x06009432 RID: 37938
		public virtual extern event HTMLButtonElementEvents_onmousewheelEventHandler HTMLButtonElementEvents_Event_onmousewheel;

		// Token: 0x14001150 RID: 4432
		// (add) Token: 0x06009433 RID: 37939
		// (remove) Token: 0x06009434 RID: 37940
		public virtual extern event HTMLButtonElementEvents_onactivateEventHandler HTMLButtonElementEvents_Event_onactivate;

		// Token: 0x14001151 RID: 4433
		// (add) Token: 0x06009435 RID: 37941
		// (remove) Token: 0x06009436 RID: 37942
		public virtual extern event HTMLButtonElementEvents_ondeactivateEventHandler HTMLButtonElementEvents_Event_ondeactivate;

		// Token: 0x14001152 RID: 4434
		// (add) Token: 0x06009437 RID: 37943
		// (remove) Token: 0x06009438 RID: 37944
		public virtual extern event HTMLButtonElementEvents_onfocusinEventHandler HTMLButtonElementEvents_Event_onfocusin;

		// Token: 0x14001153 RID: 4435
		// (add) Token: 0x06009439 RID: 37945
		// (remove) Token: 0x0600943A RID: 37946
		public virtual extern event HTMLButtonElementEvents_onfocusoutEventHandler HTMLButtonElementEvents_Event_onfocusout;
	}
}
