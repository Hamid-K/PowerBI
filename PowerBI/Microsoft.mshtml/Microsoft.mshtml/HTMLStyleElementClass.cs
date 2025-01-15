using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000BF9 RID: 3065
	[Guid("3050F37D-98B5-11CF-BB82-00AA00BDCE0B")]
	[TypeLibType(2)]
	[ClassInterface(0)]
	[ComSourceInterfaces("mshtml.HTMLStyleElementEvents\0mshtml.HTMLStyleElementEvents2\0\0")]
	[ComImport]
	public class HTMLStyleElementClass : DispHTMLStyleElement, HTMLStyleElement, HTMLStyleElementEvents_Event, IHTMLElement, IHTMLElement2, IHTMLElement3, IHTMLElement4, IHTMLUniqueName, IHTMLDOMNode, IHTMLDOMNode2, IHTMLStyleElement, HTMLStyleElementEvents2_Event
	{
		// Token: 0x06015CBA RID: 89274
		[MethodImpl(4096, MethodCodeType = 3)]
		public extern HTMLStyleElementClass();

		// Token: 0x06015CBB RID: 89275
		[DispId(-2147417611)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void setAttribute([MarshalAs(19)] [In] string strAttributeName, [MarshalAs(27)] [In] object AttributeValue, [In] int lFlags = 1);

		// Token: 0x06015CBC RID: 89276
		[DispId(-2147417610)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		public virtual extern object getAttribute([MarshalAs(19)] [In] string strAttributeName, [In] int lFlags = 0);

		// Token: 0x06015CBD RID: 89277
		[DispId(-2147417609)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool removeAttribute([MarshalAs(19)] [In] string strAttributeName, [In] int lFlags = 1);

		// Token: 0x17007489 RID: 29833
		// (get) Token: 0x06015CBF RID: 89279
		// (set) Token: 0x06015CBE RID: 89278
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

		// Token: 0x1700748A RID: 29834
		// (get) Token: 0x06015CC1 RID: 89281
		// (set) Token: 0x06015CC0 RID: 89280
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

		// Token: 0x1700748B RID: 29835
		// (get) Token: 0x06015CC2 RID: 89282
		[DispId(-2147417108)]
		public virtual extern string tagName
		{
			[DispId(-2147417108)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x1700748C RID: 29836
		// (get) Token: 0x06015CC3 RID: 89283
		[DispId(-2147418104)]
		public virtual extern IHTMLElement parentElement
		{
			[DispId(-2147418104)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x1700748D RID: 29837
		// (get) Token: 0x06015CC4 RID: 89284
		[DispId(-2147418038)]
		public virtual extern IHTMLStyle style
		{
			[DispId(-2147418038)]
			[TypeLibFunc(1024)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x1700748E RID: 29838
		// (get) Token: 0x06015CC6 RID: 89286
		// (set) Token: 0x06015CC5 RID: 89285
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

		// Token: 0x1700748F RID: 29839
		// (get) Token: 0x06015CC8 RID: 89288
		// (set) Token: 0x06015CC7 RID: 89287
		[DispId(-2147412104)]
		public virtual extern object onclick
		{
			[DispId(-2147412104)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412104)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17007490 RID: 29840
		// (get) Token: 0x06015CCA RID: 89290
		// (set) Token: 0x06015CC9 RID: 89289
		[DispId(-2147412103)]
		public virtual extern object ondblclick
		{
			[TypeLibFunc(20)]
			[DispId(-2147412103)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412103)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17007491 RID: 29841
		// (get) Token: 0x06015CCC RID: 89292
		// (set) Token: 0x06015CCB RID: 89291
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

		// Token: 0x17007492 RID: 29842
		// (get) Token: 0x06015CCE RID: 89294
		// (set) Token: 0x06015CCD RID: 89293
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

		// Token: 0x17007493 RID: 29843
		// (get) Token: 0x06015CD0 RID: 89296
		// (set) Token: 0x06015CCF RID: 89295
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

		// Token: 0x17007494 RID: 29844
		// (get) Token: 0x06015CD2 RID: 89298
		// (set) Token: 0x06015CD1 RID: 89297
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

		// Token: 0x17007495 RID: 29845
		// (get) Token: 0x06015CD4 RID: 89300
		// (set) Token: 0x06015CD3 RID: 89299
		[DispId(-2147412112)]
		public virtual extern object onmouseover
		{
			[DispId(-2147412112)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412112)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17007496 RID: 29846
		// (get) Token: 0x06015CD6 RID: 89302
		// (set) Token: 0x06015CD5 RID: 89301
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

		// Token: 0x17007497 RID: 29847
		// (get) Token: 0x06015CD8 RID: 89304
		// (set) Token: 0x06015CD7 RID: 89303
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

		// Token: 0x17007498 RID: 29848
		// (get) Token: 0x06015CDA RID: 89306
		// (set) Token: 0x06015CD9 RID: 89305
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

		// Token: 0x17007499 RID: 29849
		// (get) Token: 0x06015CDB RID: 89307
		[DispId(-2147417094)]
		public virtual extern object document
		{
			[DispId(-2147417094)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x1700749A RID: 29850
		// (get) Token: 0x06015CDD RID: 89309
		// (set) Token: 0x06015CDC RID: 89308
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

		// Token: 0x1700749B RID: 29851
		// (get) Token: 0x06015CDF RID: 89311
		// (set) Token: 0x06015CDE RID: 89310
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

		// Token: 0x1700749C RID: 29852
		// (get) Token: 0x06015CE1 RID: 89313
		// (set) Token: 0x06015CE0 RID: 89312
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

		// Token: 0x06015CE2 RID: 89314
		[DispId(-2147417093)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void scrollIntoView([MarshalAs(27)] [In] [Optional] object varargStart);

		// Token: 0x06015CE3 RID: 89315
		[DispId(-2147417092)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool contains([MarshalAs(28)] [In] IHTMLElement pChild);

		// Token: 0x1700749D RID: 29853
		// (get) Token: 0x06015CE4 RID: 89316
		[DispId(-2147417088)]
		public virtual extern int sourceIndex
		{
			[TypeLibFunc(4)]
			[DispId(-2147417088)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700749E RID: 29854
		// (get) Token: 0x06015CE5 RID: 89317
		[DispId(-2147417087)]
		public virtual extern object recordNumber
		{
			[DispId(-2147417087)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x1700749F RID: 29855
		// (get) Token: 0x06015CE7 RID: 89319
		// (set) Token: 0x06015CE6 RID: 89318
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

		// Token: 0x170074A0 RID: 29856
		// (get) Token: 0x06015CE8 RID: 89320
		[DispId(-2147417104)]
		public virtual extern int offsetLeft
		{
			[DispId(-2147417104)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170074A1 RID: 29857
		// (get) Token: 0x06015CE9 RID: 89321
		[DispId(-2147417103)]
		public virtual extern int offsetTop
		{
			[DispId(-2147417103)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170074A2 RID: 29858
		// (get) Token: 0x06015CEA RID: 89322
		[DispId(-2147417102)]
		public virtual extern int offsetWidth
		{
			[DispId(-2147417102)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170074A3 RID: 29859
		// (get) Token: 0x06015CEB RID: 89323
		[DispId(-2147417101)]
		public virtual extern int offsetHeight
		{
			[DispId(-2147417101)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170074A4 RID: 29860
		// (get) Token: 0x06015CEC RID: 89324
		[DispId(-2147417100)]
		public virtual extern IHTMLElement offsetParent
		{
			[DispId(-2147417100)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170074A5 RID: 29861
		// (get) Token: 0x06015CEE RID: 89326
		// (set) Token: 0x06015CED RID: 89325
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

		// Token: 0x170074A6 RID: 29862
		// (get) Token: 0x06015CF0 RID: 89328
		// (set) Token: 0x06015CEF RID: 89327
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

		// Token: 0x170074A7 RID: 29863
		// (get) Token: 0x06015CF2 RID: 89330
		// (set) Token: 0x06015CF1 RID: 89329
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

		// Token: 0x170074A8 RID: 29864
		// (get) Token: 0x06015CF4 RID: 89332
		// (set) Token: 0x06015CF3 RID: 89331
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

		// Token: 0x06015CF5 RID: 89333
		[DispId(-2147417082)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void insertAdjacentHTML([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string html);

		// Token: 0x06015CF6 RID: 89334
		[DispId(-2147417081)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void insertAdjacentText([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string text);

		// Token: 0x170074A9 RID: 29865
		// (get) Token: 0x06015CF7 RID: 89335
		[DispId(-2147417080)]
		public virtual extern IHTMLElement parentTextEdit
		{
			[DispId(-2147417080)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170074AA RID: 29866
		// (get) Token: 0x06015CF8 RID: 89336
		[DispId(-2147417078)]
		public virtual extern bool isTextEdit
		{
			[DispId(-2147417078)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x06015CF9 RID: 89337
		[DispId(-2147417079)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void click();

		// Token: 0x170074AB RID: 29867
		// (get) Token: 0x06015CFA RID: 89338
		[DispId(-2147417077)]
		public virtual extern IHTMLFiltersCollection filters
		{
			[DispId(-2147417077)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170074AC RID: 29868
		// (get) Token: 0x06015CFC RID: 89340
		// (set) Token: 0x06015CFB RID: 89339
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

		// Token: 0x06015CFD RID: 89341
		[DispId(-2147417076)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string toString();

		// Token: 0x170074AD RID: 29869
		// (get) Token: 0x06015CFF RID: 89343
		// (set) Token: 0x06015CFE RID: 89342
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

		// Token: 0x170074AE RID: 29870
		// (get) Token: 0x06015D01 RID: 89345
		// (set) Token: 0x06015D00 RID: 89344
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

		// Token: 0x170074AF RID: 29871
		// (get) Token: 0x06015D03 RID: 89347
		// (set) Token: 0x06015D02 RID: 89346
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

		// Token: 0x170074B0 RID: 29872
		// (get) Token: 0x06015D05 RID: 89349
		// (set) Token: 0x06015D04 RID: 89348
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

		// Token: 0x170074B1 RID: 29873
		// (get) Token: 0x06015D07 RID: 89351
		// (set) Token: 0x06015D06 RID: 89350
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

		// Token: 0x170074B2 RID: 29874
		// (get) Token: 0x06015D09 RID: 89353
		// (set) Token: 0x06015D08 RID: 89352
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

		// Token: 0x170074B3 RID: 29875
		// (get) Token: 0x06015D0B RID: 89355
		// (set) Token: 0x06015D0A RID: 89354
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

		// Token: 0x170074B4 RID: 29876
		// (get) Token: 0x06015D0D RID: 89357
		// (set) Token: 0x06015D0C RID: 89356
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

		// Token: 0x170074B5 RID: 29877
		// (get) Token: 0x06015D0F RID: 89359
		// (set) Token: 0x06015D0E RID: 89358
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

		// Token: 0x170074B6 RID: 29878
		// (get) Token: 0x06015D10 RID: 89360
		[DispId(-2147417075)]
		public virtual extern object children
		{
			[DispId(-2147417075)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x170074B7 RID: 29879
		// (get) Token: 0x06015D11 RID: 89361
		[DispId(-2147417074)]
		public virtual extern object all
		{
			[DispId(-2147417074)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x170074B8 RID: 29880
		// (get) Token: 0x06015D12 RID: 89362
		[DispId(-2147417073)]
		public virtual extern string scopeName
		{
			[DispId(-2147417073)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x06015D13 RID: 89363
		[DispId(-2147417072)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void setCapture([In] bool containerCapture = true);

		// Token: 0x06015D14 RID: 89364
		[DispId(-2147417071)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void releaseCapture();

		// Token: 0x170074B9 RID: 29881
		// (get) Token: 0x06015D16 RID: 89366
		// (set) Token: 0x06015D15 RID: 89365
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

		// Token: 0x06015D17 RID: 89367
		[DispId(-2147417070)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string componentFromPoint([In] int x, [In] int y);

		// Token: 0x06015D18 RID: 89368
		[DispId(-2147417069)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void doScroll([MarshalAs(27)] [In] [Optional] object component);

		// Token: 0x170074BA RID: 29882
		// (get) Token: 0x06015D1A RID: 89370
		// (set) Token: 0x06015D19 RID: 89369
		[DispId(-2147412081)]
		public virtual extern object onscroll
		{
			[DispId(-2147412081)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412081)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170074BB RID: 29883
		// (get) Token: 0x06015D1C RID: 89372
		// (set) Token: 0x06015D1B RID: 89371
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

		// Token: 0x170074BC RID: 29884
		// (get) Token: 0x06015D1E RID: 89374
		// (set) Token: 0x06015D1D RID: 89373
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

		// Token: 0x170074BD RID: 29885
		// (get) Token: 0x06015D20 RID: 89376
		// (set) Token: 0x06015D1F RID: 89375
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

		// Token: 0x170074BE RID: 29886
		// (get) Token: 0x06015D22 RID: 89378
		// (set) Token: 0x06015D21 RID: 89377
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

		// Token: 0x170074BF RID: 29887
		// (get) Token: 0x06015D24 RID: 89380
		// (set) Token: 0x06015D23 RID: 89379
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

		// Token: 0x170074C0 RID: 29888
		// (get) Token: 0x06015D26 RID: 89382
		// (set) Token: 0x06015D25 RID: 89381
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

		// Token: 0x170074C1 RID: 29889
		// (get) Token: 0x06015D28 RID: 89384
		// (set) Token: 0x06015D27 RID: 89383
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

		// Token: 0x170074C2 RID: 29890
		// (get) Token: 0x06015D2A RID: 89386
		// (set) Token: 0x06015D29 RID: 89385
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

		// Token: 0x170074C3 RID: 29891
		// (get) Token: 0x06015D2C RID: 89388
		// (set) Token: 0x06015D2B RID: 89387
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

		// Token: 0x170074C4 RID: 29892
		// (get) Token: 0x06015D2E RID: 89390
		// (set) Token: 0x06015D2D RID: 89389
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

		// Token: 0x170074C5 RID: 29893
		// (get) Token: 0x06015D30 RID: 89392
		// (set) Token: 0x06015D2F RID: 89391
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

		// Token: 0x170074C6 RID: 29894
		// (get) Token: 0x06015D32 RID: 89394
		// (set) Token: 0x06015D31 RID: 89393
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

		// Token: 0x170074C7 RID: 29895
		// (get) Token: 0x06015D33 RID: 89395
		[DispId(-2147417105)]
		public virtual extern IHTMLCurrentStyle currentStyle
		{
			[TypeLibFunc(1024)]
			[DispId(-2147417105)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170074C8 RID: 29896
		// (get) Token: 0x06015D35 RID: 89397
		// (set) Token: 0x06015D34 RID: 89396
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

		// Token: 0x06015D36 RID: 89398
		[DispId(-2147417068)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLRectCollection getClientRects();

		// Token: 0x06015D37 RID: 89399
		[DispId(-2147417067)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLRect getBoundingClientRect();

		// Token: 0x06015D38 RID: 89400
		[DispId(-2147417608)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void setExpression([MarshalAs(19)] [In] string propname, [MarshalAs(19)] [In] string expression, [MarshalAs(19)] [In] string language = "");

		// Token: 0x06015D39 RID: 89401
		[DispId(-2147417607)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		public virtual extern object getExpression([MarshalAs(19)] [In] string propname);

		// Token: 0x06015D3A RID: 89402
		[DispId(-2147417606)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool removeExpression([MarshalAs(19)] [In] string propname);

		// Token: 0x170074C9 RID: 29897
		// (get) Token: 0x06015D3C RID: 89404
		// (set) Token: 0x06015D3B RID: 89403
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

		// Token: 0x06015D3D RID: 89405
		[DispId(-2147416112)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void focus();

		// Token: 0x170074CA RID: 29898
		// (get) Token: 0x06015D3F RID: 89407
		// (set) Token: 0x06015D3E RID: 89406
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

		// Token: 0x170074CB RID: 29899
		// (get) Token: 0x06015D41 RID: 89409
		// (set) Token: 0x06015D40 RID: 89408
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

		// Token: 0x170074CC RID: 29900
		// (get) Token: 0x06015D43 RID: 89411
		// (set) Token: 0x06015D42 RID: 89410
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

		// Token: 0x170074CD RID: 29901
		// (get) Token: 0x06015D45 RID: 89413
		// (set) Token: 0x06015D44 RID: 89412
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

		// Token: 0x06015D46 RID: 89414
		[DispId(-2147416110)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void blur();

		// Token: 0x06015D47 RID: 89415
		[DispId(-2147416095)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void addFilter([MarshalAs(25)] [In] object pUnk);

		// Token: 0x06015D48 RID: 89416
		[DispId(-2147416094)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void removeFilter([MarshalAs(25)] [In] object pUnk);

		// Token: 0x170074CE RID: 29902
		// (get) Token: 0x06015D49 RID: 89417
		[DispId(-2147416093)]
		public virtual extern int clientHeight
		{
			[TypeLibFunc(20)]
			[DispId(-2147416093)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170074CF RID: 29903
		// (get) Token: 0x06015D4A RID: 89418
		[DispId(-2147416092)]
		public virtual extern int clientWidth
		{
			[DispId(-2147416092)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170074D0 RID: 29904
		// (get) Token: 0x06015D4B RID: 89419
		[DispId(-2147416091)]
		public virtual extern int clientTop
		{
			[DispId(-2147416091)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170074D1 RID: 29905
		// (get) Token: 0x06015D4C RID: 89420
		[DispId(-2147416090)]
		public virtual extern int clientLeft
		{
			[TypeLibFunc(20)]
			[DispId(-2147416090)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x06015D4D RID: 89421
		[DispId(-2147417605)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool attachEvent([MarshalAs(19)] [In] string @event, [MarshalAs(26)] [In] object pdisp);

		// Token: 0x06015D4E RID: 89422
		[DispId(-2147417604)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void detachEvent([MarshalAs(19)] [In] string @event, [MarshalAs(26)] [In] object pdisp);

		// Token: 0x170074D2 RID: 29906
		// (get) Token: 0x06015D4F RID: 89423
		[DispId(-2147412996)]
		public virtual extern object readyState
		{
			[DispId(-2147412996)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x170074D3 RID: 29907
		// (get) Token: 0x06015D51 RID: 89425
		// (set) Token: 0x06015D50 RID: 89424
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

		// Token: 0x170074D4 RID: 29908
		// (get) Token: 0x06015D53 RID: 89427
		// (set) Token: 0x06015D52 RID: 89426
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

		// Token: 0x170074D5 RID: 29909
		// (get) Token: 0x06015D55 RID: 89429
		// (set) Token: 0x06015D54 RID: 89428
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

		// Token: 0x170074D6 RID: 29910
		// (get) Token: 0x06015D57 RID: 89431
		// (set) Token: 0x06015D56 RID: 89430
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

		// Token: 0x170074D7 RID: 29911
		// (get) Token: 0x06015D59 RID: 89433
		// (set) Token: 0x06015D58 RID: 89432
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

		// Token: 0x06015D5A RID: 89434
		[DispId(-2147417056)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(26)]
		public virtual extern object createControlRange();

		// Token: 0x170074D8 RID: 29912
		// (get) Token: 0x06015D5B RID: 89435
		[DispId(-2147417055)]
		public virtual extern int scrollHeight
		{
			[DispId(-2147417055)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170074D9 RID: 29913
		// (get) Token: 0x06015D5C RID: 89436
		[DispId(-2147417054)]
		public virtual extern int scrollWidth
		{
			[TypeLibFunc(20)]
			[DispId(-2147417054)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170074DA RID: 29914
		// (get) Token: 0x06015D5E RID: 89438
		// (set) Token: 0x06015D5D RID: 89437
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

		// Token: 0x170074DB RID: 29915
		// (get) Token: 0x06015D60 RID: 89440
		// (set) Token: 0x06015D5F RID: 89439
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

		// Token: 0x06015D61 RID: 89441
		[DispId(-2147417050)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void clearAttributes();

		// Token: 0x170074DC RID: 29916
		// (get) Token: 0x06015D63 RID: 89443
		// (set) Token: 0x06015D62 RID: 89442
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

		// Token: 0x06015D64 RID: 89444
		[DispId(-2147417043)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElement insertAdjacentElement([MarshalAs(19)] [In] string where, [MarshalAs(28)] [In] IHTMLElement insertedElement);

		// Token: 0x06015D65 RID: 89445
		[DispId(-2147417047)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElement applyElement([MarshalAs(28)] [In] IHTMLElement apply, [MarshalAs(19)] [In] string where);

		// Token: 0x06015D66 RID: 89446
		[DispId(-2147417042)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string getAdjacentText([MarshalAs(19)] [In] string where);

		// Token: 0x06015D67 RID: 89447
		[DispId(-2147417041)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string replaceAdjacentText([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string newText);

		// Token: 0x170074DD RID: 29917
		// (get) Token: 0x06015D68 RID: 89448
		[DispId(-2147417040)]
		public virtual extern bool canHaveChildren
		{
			[DispId(-2147417040)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x06015D69 RID: 89449
		[DispId(-2147417032)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern int addBehavior([MarshalAs(19)] [In] string bstrUrl, [MarshalAs(27)] [In] [Optional] ref object pvarFactory);

		// Token: 0x06015D6A RID: 89450
		[DispId(-2147417031)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool removeBehavior([In] int cookie);

		// Token: 0x170074DE RID: 29918
		// (get) Token: 0x06015D6B RID: 89451
		[DispId(-2147417048)]
		public virtual extern IHTMLStyle runtimeStyle
		{
			[TypeLibFunc(1024)]
			[DispId(-2147417048)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170074DF RID: 29919
		// (get) Token: 0x06015D6C RID: 89452
		[DispId(-2147417030)]
		public virtual extern object behaviorUrns
		{
			[DispId(-2147417030)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x170074E0 RID: 29920
		// (get) Token: 0x06015D6E RID: 89454
		// (set) Token: 0x06015D6D RID: 89453
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

		// Token: 0x170074E1 RID: 29921
		// (get) Token: 0x06015D70 RID: 89456
		// (set) Token: 0x06015D6F RID: 89455
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

		// Token: 0x170074E2 RID: 29922
		// (get) Token: 0x06015D71 RID: 89457
		[DispId(-2147417028)]
		public virtual extern int readyStateValue
		{
			[TypeLibFunc(65)]
			[DispId(-2147417028)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x06015D72 RID: 89458
		[DispId(-2147417027)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElementCollection getElementsByTagName([MarshalAs(19)] [In] string v);

		// Token: 0x06015D73 RID: 89459
		[DispId(-2147417016)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void mergeAttributes([MarshalAs(28)] [In] IHTMLElement mergeThis, [MarshalAs(27)] [In] [Optional] ref object pvarFlags);

		// Token: 0x170074E3 RID: 29923
		// (get) Token: 0x06015D74 RID: 89460
		[DispId(-2147417015)]
		public virtual extern bool isMultiLine
		{
			[DispId(-2147417015)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170074E4 RID: 29924
		// (get) Token: 0x06015D75 RID: 89461
		[DispId(-2147417014)]
		public virtual extern bool canHaveHTML
		{
			[DispId(-2147417014)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170074E5 RID: 29925
		// (get) Token: 0x06015D77 RID: 89463
		// (set) Token: 0x06015D76 RID: 89462
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

		// Token: 0x170074E6 RID: 29926
		// (get) Token: 0x06015D79 RID: 89465
		// (set) Token: 0x06015D78 RID: 89464
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

		// Token: 0x170074E7 RID: 29927
		// (get) Token: 0x06015D7B RID: 89467
		// (set) Token: 0x06015D7A RID: 89466
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

		// Token: 0x170074E8 RID: 29928
		// (get) Token: 0x06015D7D RID: 89469
		// (set) Token: 0x06015D7C RID: 89468
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

		// Token: 0x06015D7E RID: 89470
		[DispId(-2147417011)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void setActive();

		// Token: 0x170074E9 RID: 29929
		// (get) Token: 0x06015D80 RID: 89472
		// (set) Token: 0x06015D7F RID: 89471
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

		// Token: 0x170074EA RID: 29930
		// (get) Token: 0x06015D81 RID: 89473
		[DispId(-2147417010)]
		public virtual extern bool isContentEditable
		{
			[DispId(-2147417010)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170074EB RID: 29931
		// (get) Token: 0x06015D83 RID: 89475
		// (set) Token: 0x06015D82 RID: 89474
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

		// Token: 0x170074EC RID: 29932
		// (get) Token: 0x06015D85 RID: 89477
		// (set) Token: 0x06015D84 RID: 89476
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

		// Token: 0x170074ED RID: 29933
		// (get) Token: 0x06015D86 RID: 89478
		[DispId(-2147417007)]
		public virtual extern bool isDisabled
		{
			[DispId(-2147417007)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170074EE RID: 29934
		// (get) Token: 0x06015D88 RID: 89480
		// (set) Token: 0x06015D87 RID: 89479
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

		// Token: 0x170074EF RID: 29935
		// (get) Token: 0x06015D8A RID: 89482
		// (set) Token: 0x06015D89 RID: 89481
		[DispId(-2147412033)]
		public virtual extern object oncontrolselect
		{
			[TypeLibFunc(20)]
			[DispId(-2147412033)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412033)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x06015D8B RID: 89483
		[DispId(-2147417006)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool FireEvent([MarshalAs(19)] [In] string bstrEventName, [MarshalAs(27)] [In] [Optional] ref object pvarEventObject);

		// Token: 0x170074F0 RID: 29936
		// (get) Token: 0x06015D8D RID: 89485
		// (set) Token: 0x06015D8C RID: 89484
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

		// Token: 0x170074F1 RID: 29937
		// (get) Token: 0x06015D8F RID: 89487
		// (set) Token: 0x06015D8E RID: 89486
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

		// Token: 0x170074F2 RID: 29938
		// (get) Token: 0x06015D91 RID: 89489
		// (set) Token: 0x06015D90 RID: 89488
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

		// Token: 0x170074F3 RID: 29939
		// (get) Token: 0x06015D93 RID: 89491
		// (set) Token: 0x06015D92 RID: 89490
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

		// Token: 0x170074F4 RID: 29940
		// (get) Token: 0x06015D95 RID: 89493
		// (set) Token: 0x06015D94 RID: 89492
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

		// Token: 0x170074F5 RID: 29941
		// (get) Token: 0x06015D97 RID: 89495
		// (set) Token: 0x06015D96 RID: 89494
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

		// Token: 0x170074F6 RID: 29942
		// (get) Token: 0x06015D99 RID: 89497
		// (set) Token: 0x06015D98 RID: 89496
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

		// Token: 0x170074F7 RID: 29943
		// (get) Token: 0x06015D9B RID: 89499
		// (set) Token: 0x06015D9A RID: 89498
		[DispId(-2147412024)]
		public virtual extern object ondeactivate
		{
			[TypeLibFunc(20)]
			[DispId(-2147412024)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412024)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x06015D9C RID: 89500
		[DispId(-2147417005)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool dragDrop();

		// Token: 0x170074F8 RID: 29944
		// (get) Token: 0x06015D9D RID: 89501
		[DispId(-2147417004)]
		public virtual extern int glyphMode
		{
			[TypeLibFunc(1089)]
			[DispId(-2147417004)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170074F9 RID: 29945
		// (get) Token: 0x06015D9F RID: 89503
		// (set) Token: 0x06015D9E RID: 89502
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

		// Token: 0x06015DA0 RID: 89504
		[DispId(-2147417000)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void normalize();

		// Token: 0x06015DA1 RID: 89505
		[DispId(-2147417003)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute getAttributeNode([MarshalAs(19)] [In] string bstrName);

		// Token: 0x06015DA2 RID: 89506
		[DispId(-2147417002)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute setAttributeNode([MarshalAs(28)] [In] IHTMLDOMAttribute pattr);

		// Token: 0x06015DA3 RID: 89507
		[DispId(-2147417001)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute removeAttributeNode([MarshalAs(28)] [In] IHTMLDOMAttribute pattr);

		// Token: 0x170074FA RID: 29946
		// (get) Token: 0x06015DA5 RID: 89509
		// (set) Token: 0x06015DA4 RID: 89508
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

		// Token: 0x170074FB RID: 29947
		// (get) Token: 0x06015DA7 RID: 89511
		// (set) Token: 0x06015DA6 RID: 89510
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

		// Token: 0x170074FC RID: 29948
		// (get) Token: 0x06015DA9 RID: 89513
		// (set) Token: 0x06015DA8 RID: 89512
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

		// Token: 0x170074FD RID: 29949
		// (get) Token: 0x06015DAA RID: 89514
		[DispId(-2147417058)]
		public virtual extern int uniqueNumber
		{
			[TypeLibFunc(64)]
			[DispId(-2147417058)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170074FE RID: 29950
		// (get) Token: 0x06015DAB RID: 89515
		[DispId(-2147417057)]
		public virtual extern string uniqueID
		{
			[TypeLibFunc(64)]
			[DispId(-2147417057)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170074FF RID: 29951
		// (get) Token: 0x06015DAC RID: 89516
		[DispId(-2147417066)]
		public virtual extern int nodeType
		{
			[DispId(-2147417066)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17007500 RID: 29952
		// (get) Token: 0x06015DAD RID: 89517
		[DispId(-2147417065)]
		public virtual extern IHTMLDOMNode parentNode
		{
			[DispId(-2147417065)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x06015DAE RID: 89518
		[DispId(-2147417064)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool hasChildNodes();

		// Token: 0x17007501 RID: 29953
		// (get) Token: 0x06015DAF RID: 89519
		[DispId(-2147417063)]
		public virtual extern object childNodes
		{
			[DispId(-2147417063)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17007502 RID: 29954
		// (get) Token: 0x06015DB0 RID: 89520
		[DispId(-2147417062)]
		public virtual extern object attributes
		{
			[DispId(-2147417062)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x06015DB1 RID: 89521
		[DispId(-2147417061)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode insertBefore([MarshalAs(28)] [In] IHTMLDOMNode newChild, [MarshalAs(27)] [In] [Optional] object refChild);

		// Token: 0x06015DB2 RID: 89522
		[DispId(-2147417060)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode removeChild([MarshalAs(28)] [In] IHTMLDOMNode oldChild);

		// Token: 0x06015DB3 RID: 89523
		[DispId(-2147417059)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode replaceChild([MarshalAs(28)] [In] IHTMLDOMNode newChild, [MarshalAs(28)] [In] IHTMLDOMNode oldChild);

		// Token: 0x06015DB4 RID: 89524
		[DispId(-2147417051)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode cloneNode([In] bool fDeep);

		// Token: 0x06015DB5 RID: 89525
		[DispId(-2147417046)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode removeNode([In] bool fDeep = false);

		// Token: 0x06015DB6 RID: 89526
		[DispId(-2147417044)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode swapNode([MarshalAs(28)] [In] IHTMLDOMNode otherNode);

		// Token: 0x06015DB7 RID: 89527
		[DispId(-2147417045)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode replaceNode([MarshalAs(28)] [In] IHTMLDOMNode replacement);

		// Token: 0x06015DB8 RID: 89528
		[DispId(-2147417039)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode appendChild([MarshalAs(28)] [In] IHTMLDOMNode newChild);

		// Token: 0x17007503 RID: 29955
		// (get) Token: 0x06015DB9 RID: 89529
		[DispId(-2147417038)]
		public virtual extern string nodeName
		{
			[DispId(-2147417038)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17007504 RID: 29956
		// (get) Token: 0x06015DBB RID: 89531
		// (set) Token: 0x06015DBA RID: 89530
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

		// Token: 0x17007505 RID: 29957
		// (get) Token: 0x06015DBC RID: 89532
		[DispId(-2147417036)]
		public virtual extern IHTMLDOMNode firstChild
		{
			[DispId(-2147417036)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17007506 RID: 29958
		// (get) Token: 0x06015DBD RID: 89533
		[DispId(-2147417035)]
		public virtual extern IHTMLDOMNode lastChild
		{
			[DispId(-2147417035)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17007507 RID: 29959
		// (get) Token: 0x06015DBE RID: 89534
		[DispId(-2147417034)]
		public virtual extern IHTMLDOMNode previousSibling
		{
			[DispId(-2147417034)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17007508 RID: 29960
		// (get) Token: 0x06015DBF RID: 89535
		[DispId(-2147417033)]
		public virtual extern IHTMLDOMNode nextSibling
		{
			[DispId(-2147417033)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17007509 RID: 29961
		// (get) Token: 0x06015DC0 RID: 89536
		[DispId(-2147416999)]
		public virtual extern object ownerDocument
		{
			[DispId(-2147416999)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x1700750A RID: 29962
		// (get) Token: 0x06015DC2 RID: 89538
		// (set) Token: 0x06015DC1 RID: 89537
		[DispId(1002)]
		public virtual extern string type
		{
			[TypeLibFunc(20)]
			[DispId(1002)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(1002)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x1700750B RID: 29963
		// (get) Token: 0x06015DC4 RID: 89540
		// (set) Token: 0x06015DC3 RID: 89539
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

		// Token: 0x1700750C RID: 29964
		// (get) Token: 0x06015DC6 RID: 89542
		// (set) Token: 0x06015DC5 RID: 89541
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

		// Token: 0x1700750D RID: 29965
		// (get) Token: 0x06015DC7 RID: 89543
		[DispId(1004)]
		public virtual extern IHTMLStyleSheet styleSheet
		{
			[DispId(1004)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x1700750E RID: 29966
		// (get) Token: 0x06015DC9 RID: 89545
		// (set) Token: 0x06015DC8 RID: 89544
		[DispId(1006)]
		public virtual extern string media
		{
			[TypeLibFunc(20)]
			[DispId(1006)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(1006)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x06015DCA RID: 89546
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement_setAttribute([MarshalAs(19)] [In] string strAttributeName, [MarshalAs(27)] [In] object AttributeValue, [In] int lFlags = 1);

		// Token: 0x06015DCB RID: 89547
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		public virtual extern object IHTMLElement_getAttribute([MarshalAs(19)] [In] string strAttributeName, [In] int lFlags = 0);

		// Token: 0x06015DCC RID: 89548
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement_removeAttribute([MarshalAs(19)] [In] string strAttributeName, [In] int lFlags = 1);

		// Token: 0x1700750F RID: 29967
		// (get) Token: 0x06015DCE RID: 89550
		// (set) Token: 0x06015DCD RID: 89549
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

		// Token: 0x17007510 RID: 29968
		// (get) Token: 0x06015DD0 RID: 89552
		// (set) Token: 0x06015DCF RID: 89551
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

		// Token: 0x17007511 RID: 29969
		// (get) Token: 0x06015DD1 RID: 89553
		public virtual extern string IHTMLElement_tagName
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17007512 RID: 29970
		// (get) Token: 0x06015DD2 RID: 89554
		public virtual extern IHTMLElement IHTMLElement_parentElement
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17007513 RID: 29971
		// (get) Token: 0x06015DD3 RID: 89555
		public virtual extern IHTMLStyle IHTMLElement_style
		{
			[TypeLibFunc(1024)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17007514 RID: 29972
		// (get) Token: 0x06015DD5 RID: 89557
		// (set) Token: 0x06015DD4 RID: 89556
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

		// Token: 0x17007515 RID: 29973
		// (get) Token: 0x06015DD7 RID: 89559
		// (set) Token: 0x06015DD6 RID: 89558
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

		// Token: 0x17007516 RID: 29974
		// (get) Token: 0x06015DD9 RID: 89561
		// (set) Token: 0x06015DD8 RID: 89560
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

		// Token: 0x17007517 RID: 29975
		// (get) Token: 0x06015DDB RID: 89563
		// (set) Token: 0x06015DDA RID: 89562
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

		// Token: 0x17007518 RID: 29976
		// (get) Token: 0x06015DDD RID: 89565
		// (set) Token: 0x06015DDC RID: 89564
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

		// Token: 0x17007519 RID: 29977
		// (get) Token: 0x06015DDF RID: 89567
		// (set) Token: 0x06015DDE RID: 89566
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

		// Token: 0x1700751A RID: 29978
		// (get) Token: 0x06015DE1 RID: 89569
		// (set) Token: 0x06015DE0 RID: 89568
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

		// Token: 0x1700751B RID: 29979
		// (get) Token: 0x06015DE3 RID: 89571
		// (set) Token: 0x06015DE2 RID: 89570
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

		// Token: 0x1700751C RID: 29980
		// (get) Token: 0x06015DE5 RID: 89573
		// (set) Token: 0x06015DE4 RID: 89572
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

		// Token: 0x1700751D RID: 29981
		// (get) Token: 0x06015DE7 RID: 89575
		// (set) Token: 0x06015DE6 RID: 89574
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

		// Token: 0x1700751E RID: 29982
		// (get) Token: 0x06015DE9 RID: 89577
		// (set) Token: 0x06015DE8 RID: 89576
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

		// Token: 0x1700751F RID: 29983
		// (get) Token: 0x06015DEA RID: 89578
		public virtual extern object IHTMLElement_document
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17007520 RID: 29984
		// (get) Token: 0x06015DEC RID: 89580
		// (set) Token: 0x06015DEB RID: 89579
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

		// Token: 0x17007521 RID: 29985
		// (get) Token: 0x06015DEE RID: 89582
		// (set) Token: 0x06015DED RID: 89581
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

		// Token: 0x17007522 RID: 29986
		// (get) Token: 0x06015DF0 RID: 89584
		// (set) Token: 0x06015DEF RID: 89583
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

		// Token: 0x06015DF1 RID: 89585
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement_scrollIntoView([MarshalAs(27)] [In] [Optional] object varargStart);

		// Token: 0x06015DF2 RID: 89586
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement_contains([MarshalAs(28)] [In] IHTMLElement pChild);

		// Token: 0x17007523 RID: 29987
		// (get) Token: 0x06015DF3 RID: 89587
		public virtual extern int IHTMLElement_sourceIndex
		{
			[TypeLibFunc(4)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17007524 RID: 29988
		// (get) Token: 0x06015DF4 RID: 89588
		public virtual extern object IHTMLElement_recordNumber
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x17007525 RID: 29989
		// (get) Token: 0x06015DF6 RID: 89590
		// (set) Token: 0x06015DF5 RID: 89589
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

		// Token: 0x17007526 RID: 29990
		// (get) Token: 0x06015DF7 RID: 89591
		public virtual extern int IHTMLElement_offsetLeft
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17007527 RID: 29991
		// (get) Token: 0x06015DF8 RID: 89592
		public virtual extern int IHTMLElement_offsetTop
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17007528 RID: 29992
		// (get) Token: 0x06015DF9 RID: 89593
		public virtual extern int IHTMLElement_offsetWidth
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17007529 RID: 29993
		// (get) Token: 0x06015DFA RID: 89594
		public virtual extern int IHTMLElement_offsetHeight
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700752A RID: 29994
		// (get) Token: 0x06015DFB RID: 89595
		public virtual extern IHTMLElement IHTMLElement_offsetParent
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x1700752B RID: 29995
		// (get) Token: 0x06015DFD RID: 89597
		// (set) Token: 0x06015DFC RID: 89596
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

		// Token: 0x1700752C RID: 29996
		// (get) Token: 0x06015DFF RID: 89599
		// (set) Token: 0x06015DFE RID: 89598
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

		// Token: 0x1700752D RID: 29997
		// (get) Token: 0x06015E01 RID: 89601
		// (set) Token: 0x06015E00 RID: 89600
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

		// Token: 0x1700752E RID: 29998
		// (get) Token: 0x06015E03 RID: 89603
		// (set) Token: 0x06015E02 RID: 89602
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

		// Token: 0x06015E04 RID: 89604
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement_insertAdjacentHTML([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string html);

		// Token: 0x06015E05 RID: 89605
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement_insertAdjacentText([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string text);

		// Token: 0x1700752F RID: 29999
		// (get) Token: 0x06015E06 RID: 89606
		public virtual extern IHTMLElement IHTMLElement_parentTextEdit
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17007530 RID: 30000
		// (get) Token: 0x06015E07 RID: 89607
		public virtual extern bool IHTMLElement_isTextEdit
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x06015E08 RID: 89608
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement_click();

		// Token: 0x17007531 RID: 30001
		// (get) Token: 0x06015E09 RID: 89609
		public virtual extern IHTMLFiltersCollection IHTMLElement_filters
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17007532 RID: 30002
		// (get) Token: 0x06015E0B RID: 89611
		// (set) Token: 0x06015E0A RID: 89610
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

		// Token: 0x06015E0C RID: 89612
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string IHTMLElement_toString();

		// Token: 0x17007533 RID: 30003
		// (get) Token: 0x06015E0E RID: 89614
		// (set) Token: 0x06015E0D RID: 89613
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

		// Token: 0x17007534 RID: 30004
		// (get) Token: 0x06015E10 RID: 89616
		// (set) Token: 0x06015E0F RID: 89615
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

		// Token: 0x17007535 RID: 30005
		// (get) Token: 0x06015E12 RID: 89618
		// (set) Token: 0x06015E11 RID: 89617
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

		// Token: 0x17007536 RID: 30006
		// (get) Token: 0x06015E14 RID: 89620
		// (set) Token: 0x06015E13 RID: 89619
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

		// Token: 0x17007537 RID: 30007
		// (get) Token: 0x06015E16 RID: 89622
		// (set) Token: 0x06015E15 RID: 89621
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

		// Token: 0x17007538 RID: 30008
		// (get) Token: 0x06015E18 RID: 89624
		// (set) Token: 0x06015E17 RID: 89623
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

		// Token: 0x17007539 RID: 30009
		// (get) Token: 0x06015E1A RID: 89626
		// (set) Token: 0x06015E19 RID: 89625
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

		// Token: 0x1700753A RID: 30010
		// (get) Token: 0x06015E1C RID: 89628
		// (set) Token: 0x06015E1B RID: 89627
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

		// Token: 0x1700753B RID: 30011
		// (get) Token: 0x06015E1E RID: 89630
		// (set) Token: 0x06015E1D RID: 89629
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

		// Token: 0x1700753C RID: 30012
		// (get) Token: 0x06015E1F RID: 89631
		public virtual extern object IHTMLElement_children
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x1700753D RID: 30013
		// (get) Token: 0x06015E20 RID: 89632
		public virtual extern object IHTMLElement_all
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x1700753E RID: 30014
		// (get) Token: 0x06015E21 RID: 89633
		public virtual extern string IHTMLElement2_scopeName
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x06015E22 RID: 89634
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_setCapture([In] bool containerCapture = true);

		// Token: 0x06015E23 RID: 89635
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_releaseCapture();

		// Token: 0x1700753F RID: 30015
		// (get) Token: 0x06015E25 RID: 89637
		// (set) Token: 0x06015E24 RID: 89636
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

		// Token: 0x06015E26 RID: 89638
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string IHTMLElement2_componentFromPoint([In] int x, [In] int y);

		// Token: 0x06015E27 RID: 89639
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_doScroll([MarshalAs(27)] [In] [Optional] object component);

		// Token: 0x17007540 RID: 30016
		// (get) Token: 0x06015E29 RID: 89641
		// (set) Token: 0x06015E28 RID: 89640
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

		// Token: 0x17007541 RID: 30017
		// (get) Token: 0x06015E2B RID: 89643
		// (set) Token: 0x06015E2A RID: 89642
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

		// Token: 0x17007542 RID: 30018
		// (get) Token: 0x06015E2D RID: 89645
		// (set) Token: 0x06015E2C RID: 89644
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

		// Token: 0x17007543 RID: 30019
		// (get) Token: 0x06015E2F RID: 89647
		// (set) Token: 0x06015E2E RID: 89646
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

		// Token: 0x17007544 RID: 30020
		// (get) Token: 0x06015E31 RID: 89649
		// (set) Token: 0x06015E30 RID: 89648
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

		// Token: 0x17007545 RID: 30021
		// (get) Token: 0x06015E33 RID: 89651
		// (set) Token: 0x06015E32 RID: 89650
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

		// Token: 0x17007546 RID: 30022
		// (get) Token: 0x06015E35 RID: 89653
		// (set) Token: 0x06015E34 RID: 89652
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

		// Token: 0x17007547 RID: 30023
		// (get) Token: 0x06015E37 RID: 89655
		// (set) Token: 0x06015E36 RID: 89654
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

		// Token: 0x17007548 RID: 30024
		// (get) Token: 0x06015E39 RID: 89657
		// (set) Token: 0x06015E38 RID: 89656
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

		// Token: 0x17007549 RID: 30025
		// (get) Token: 0x06015E3B RID: 89659
		// (set) Token: 0x06015E3A RID: 89658
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

		// Token: 0x1700754A RID: 30026
		// (get) Token: 0x06015E3D RID: 89661
		// (set) Token: 0x06015E3C RID: 89660
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

		// Token: 0x1700754B RID: 30027
		// (get) Token: 0x06015E3F RID: 89663
		// (set) Token: 0x06015E3E RID: 89662
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

		// Token: 0x1700754C RID: 30028
		// (get) Token: 0x06015E41 RID: 89665
		// (set) Token: 0x06015E40 RID: 89664
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

		// Token: 0x1700754D RID: 30029
		// (get) Token: 0x06015E42 RID: 89666
		public virtual extern IHTMLCurrentStyle IHTMLElement2_currentStyle
		{
			[TypeLibFunc(1024)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x1700754E RID: 30030
		// (get) Token: 0x06015E44 RID: 89668
		// (set) Token: 0x06015E43 RID: 89667
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

		// Token: 0x06015E45 RID: 89669
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLRectCollection IHTMLElement2_getClientRects();

		// Token: 0x06015E46 RID: 89670
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLRect IHTMLElement2_getBoundingClientRect();

		// Token: 0x06015E47 RID: 89671
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_setExpression([MarshalAs(19)] [In] string propname, [MarshalAs(19)] [In] string expression, [MarshalAs(19)] [In] string language = "");

		// Token: 0x06015E48 RID: 89672
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		public virtual extern object IHTMLElement2_getExpression([MarshalAs(19)] [In] string propname);

		// Token: 0x06015E49 RID: 89673
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement2_removeExpression([MarshalAs(19)] [In] string propname);

		// Token: 0x1700754F RID: 30031
		// (get) Token: 0x06015E4B RID: 89675
		// (set) Token: 0x06015E4A RID: 89674
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

		// Token: 0x06015E4C RID: 89676
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_focus();

		// Token: 0x17007550 RID: 30032
		// (get) Token: 0x06015E4E RID: 89678
		// (set) Token: 0x06015E4D RID: 89677
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

		// Token: 0x17007551 RID: 30033
		// (get) Token: 0x06015E50 RID: 89680
		// (set) Token: 0x06015E4F RID: 89679
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

		// Token: 0x17007552 RID: 30034
		// (get) Token: 0x06015E52 RID: 89682
		// (set) Token: 0x06015E51 RID: 89681
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

		// Token: 0x17007553 RID: 30035
		// (get) Token: 0x06015E54 RID: 89684
		// (set) Token: 0x06015E53 RID: 89683
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

		// Token: 0x06015E55 RID: 89685
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_blur();

		// Token: 0x06015E56 RID: 89686
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_addFilter([MarshalAs(25)] [In] object pUnk);

		// Token: 0x06015E57 RID: 89687
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_removeFilter([MarshalAs(25)] [In] object pUnk);

		// Token: 0x17007554 RID: 30036
		// (get) Token: 0x06015E58 RID: 89688
		public virtual extern int IHTMLElement2_clientHeight
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17007555 RID: 30037
		// (get) Token: 0x06015E59 RID: 89689
		public virtual extern int IHTMLElement2_clientWidth
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17007556 RID: 30038
		// (get) Token: 0x06015E5A RID: 89690
		public virtual extern int IHTMLElement2_clientTop
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17007557 RID: 30039
		// (get) Token: 0x06015E5B RID: 89691
		public virtual extern int IHTMLElement2_clientLeft
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x06015E5C RID: 89692
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement2_attachEvent([MarshalAs(19)] [In] string @event, [MarshalAs(26)] [In] object pdisp);

		// Token: 0x06015E5D RID: 89693
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_detachEvent([MarshalAs(19)] [In] string @event, [MarshalAs(26)] [In] object pdisp);

		// Token: 0x17007558 RID: 30040
		// (get) Token: 0x06015E5E RID: 89694
		public virtual extern object IHTMLElement2_readyState
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x17007559 RID: 30041
		// (get) Token: 0x06015E60 RID: 89696
		// (set) Token: 0x06015E5F RID: 89695
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

		// Token: 0x1700755A RID: 30042
		// (get) Token: 0x06015E62 RID: 89698
		// (set) Token: 0x06015E61 RID: 89697
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

		// Token: 0x1700755B RID: 30043
		// (get) Token: 0x06015E64 RID: 89700
		// (set) Token: 0x06015E63 RID: 89699
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

		// Token: 0x1700755C RID: 30044
		// (get) Token: 0x06015E66 RID: 89702
		// (set) Token: 0x06015E65 RID: 89701
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

		// Token: 0x1700755D RID: 30045
		// (get) Token: 0x06015E68 RID: 89704
		// (set) Token: 0x06015E67 RID: 89703
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

		// Token: 0x06015E69 RID: 89705
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(26)]
		public virtual extern object IHTMLElement2_createControlRange();

		// Token: 0x1700755E RID: 30046
		// (get) Token: 0x06015E6A RID: 89706
		public virtual extern int IHTMLElement2_scrollHeight
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700755F RID: 30047
		// (get) Token: 0x06015E6B RID: 89707
		public virtual extern int IHTMLElement2_scrollWidth
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17007560 RID: 30048
		// (get) Token: 0x06015E6D RID: 89709
		// (set) Token: 0x06015E6C RID: 89708
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

		// Token: 0x17007561 RID: 30049
		// (get) Token: 0x06015E6F RID: 89711
		// (set) Token: 0x06015E6E RID: 89710
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

		// Token: 0x06015E70 RID: 89712
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_clearAttributes();

		// Token: 0x06015E71 RID: 89713
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void mergeAttributes([MarshalAs(28)] [In] IHTMLElement mergeThis);

		// Token: 0x17007562 RID: 30050
		// (get) Token: 0x06015E73 RID: 89715
		// (set) Token: 0x06015E72 RID: 89714
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

		// Token: 0x06015E74 RID: 89716
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElement IHTMLElement2_insertAdjacentElement([MarshalAs(19)] [In] string where, [MarshalAs(28)] [In] IHTMLElement insertedElement);

		// Token: 0x06015E75 RID: 89717
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElement IHTMLElement2_applyElement([MarshalAs(28)] [In] IHTMLElement apply, [MarshalAs(19)] [In] string where);

		// Token: 0x06015E76 RID: 89718
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string IHTMLElement2_getAdjacentText([MarshalAs(19)] [In] string where);

		// Token: 0x06015E77 RID: 89719
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string IHTMLElement2_replaceAdjacentText([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string newText);

		// Token: 0x17007563 RID: 30051
		// (get) Token: 0x06015E78 RID: 89720
		public virtual extern bool IHTMLElement2_canHaveChildren
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x06015E79 RID: 89721
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern int IHTMLElement2_addBehavior([MarshalAs(19)] [In] string bstrUrl, [MarshalAs(27)] [In] [Optional] ref object pvarFactory);

		// Token: 0x06015E7A RID: 89722
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement2_removeBehavior([In] int cookie);

		// Token: 0x17007564 RID: 30052
		// (get) Token: 0x06015E7B RID: 89723
		public virtual extern IHTMLStyle IHTMLElement2_runtimeStyle
		{
			[TypeLibFunc(1024)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17007565 RID: 30053
		// (get) Token: 0x06015E7C RID: 89724
		public virtual extern object IHTMLElement2_behaviorUrns
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17007566 RID: 30054
		// (get) Token: 0x06015E7E RID: 89726
		// (set) Token: 0x06015E7D RID: 89725
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

		// Token: 0x17007567 RID: 30055
		// (get) Token: 0x06015E80 RID: 89728
		// (set) Token: 0x06015E7F RID: 89727
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

		// Token: 0x17007568 RID: 30056
		// (get) Token: 0x06015E81 RID: 89729
		public virtual extern int IHTMLElement2_readyStateValue
		{
			[TypeLibFunc(65)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x06015E82 RID: 89730
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElementCollection IHTMLElement2_getElementsByTagName([MarshalAs(19)] [In] string v);

		// Token: 0x06015E83 RID: 89731
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void mergeAttributes([MarshalAs(28)] [In] IHTMLElement mergeThis, [MarshalAs(27)] [In] [Optional] ref object pvarFlags);

		// Token: 0x17007569 RID: 30057
		// (get) Token: 0x06015E84 RID: 89732
		public virtual extern bool IHTMLElement3_isMultiLine
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700756A RID: 30058
		// (get) Token: 0x06015E85 RID: 89733
		public virtual extern bool IHTMLElement3_canHaveHTML
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700756B RID: 30059
		// (get) Token: 0x06015E87 RID: 89735
		// (set) Token: 0x06015E86 RID: 89734
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

		// Token: 0x1700756C RID: 30060
		// (get) Token: 0x06015E89 RID: 89737
		// (set) Token: 0x06015E88 RID: 89736
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

		// Token: 0x1700756D RID: 30061
		// (get) Token: 0x06015E8B RID: 89739
		// (set) Token: 0x06015E8A RID: 89738
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

		// Token: 0x1700756E RID: 30062
		// (get) Token: 0x06015E8D RID: 89741
		// (set) Token: 0x06015E8C RID: 89740
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

		// Token: 0x06015E8E RID: 89742
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement3_setActive();

		// Token: 0x1700756F RID: 30063
		// (get) Token: 0x06015E90 RID: 89744
		// (set) Token: 0x06015E8F RID: 89743
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

		// Token: 0x17007570 RID: 30064
		// (get) Token: 0x06015E91 RID: 89745
		public virtual extern bool IHTMLElement3_isContentEditable
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17007571 RID: 30065
		// (get) Token: 0x06015E93 RID: 89747
		// (set) Token: 0x06015E92 RID: 89746
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

		// Token: 0x17007572 RID: 30066
		// (get) Token: 0x06015E95 RID: 89749
		// (set) Token: 0x06015E94 RID: 89748
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

		// Token: 0x17007573 RID: 30067
		// (get) Token: 0x06015E96 RID: 89750
		public virtual extern bool IHTMLElement3_isDisabled
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17007574 RID: 30068
		// (get) Token: 0x06015E98 RID: 89752
		// (set) Token: 0x06015E97 RID: 89751
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

		// Token: 0x17007575 RID: 30069
		// (get) Token: 0x06015E9A RID: 89754
		// (set) Token: 0x06015E99 RID: 89753
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

		// Token: 0x06015E9B RID: 89755
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement3_FireEvent([MarshalAs(19)] [In] string bstrEventName, [MarshalAs(27)] [In] [Optional] ref object pvarEventObject);

		// Token: 0x17007576 RID: 30070
		// (get) Token: 0x06015E9D RID: 89757
		// (set) Token: 0x06015E9C RID: 89756
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

		// Token: 0x17007577 RID: 30071
		// (get) Token: 0x06015E9F RID: 89759
		// (set) Token: 0x06015E9E RID: 89758
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

		// Token: 0x17007578 RID: 30072
		// (get) Token: 0x06015EA1 RID: 89761
		// (set) Token: 0x06015EA0 RID: 89760
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

		// Token: 0x17007579 RID: 30073
		// (get) Token: 0x06015EA3 RID: 89763
		// (set) Token: 0x06015EA2 RID: 89762
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

		// Token: 0x1700757A RID: 30074
		// (get) Token: 0x06015EA5 RID: 89765
		// (set) Token: 0x06015EA4 RID: 89764
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

		// Token: 0x1700757B RID: 30075
		// (get) Token: 0x06015EA7 RID: 89767
		// (set) Token: 0x06015EA6 RID: 89766
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

		// Token: 0x1700757C RID: 30076
		// (get) Token: 0x06015EA9 RID: 89769
		// (set) Token: 0x06015EA8 RID: 89768
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

		// Token: 0x1700757D RID: 30077
		// (get) Token: 0x06015EAB RID: 89771
		// (set) Token: 0x06015EAA RID: 89770
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

		// Token: 0x06015EAC RID: 89772
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement3_dragDrop();

		// Token: 0x1700757E RID: 30078
		// (get) Token: 0x06015EAD RID: 89773
		public virtual extern int IHTMLElement3_glyphMode
		{
			[TypeLibFunc(1089)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700757F RID: 30079
		// (get) Token: 0x06015EAF RID: 89775
		// (set) Token: 0x06015EAE RID: 89774
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

		// Token: 0x06015EB0 RID: 89776
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement4_normalize();

		// Token: 0x06015EB1 RID: 89777
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute IHTMLElement4_getAttributeNode([MarshalAs(19)] [In] string bstrName);

		// Token: 0x06015EB2 RID: 89778
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute IHTMLElement4_setAttributeNode([MarshalAs(28)] [In] IHTMLDOMAttribute pattr);

		// Token: 0x06015EB3 RID: 89779
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute IHTMLElement4_removeAttributeNode([MarshalAs(28)] [In] IHTMLDOMAttribute pattr);

		// Token: 0x17007580 RID: 30080
		// (get) Token: 0x06015EB5 RID: 89781
		// (set) Token: 0x06015EB4 RID: 89780
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

		// Token: 0x17007581 RID: 30081
		// (get) Token: 0x06015EB7 RID: 89783
		// (set) Token: 0x06015EB6 RID: 89782
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

		// Token: 0x17007582 RID: 30082
		// (get) Token: 0x06015EB9 RID: 89785
		// (set) Token: 0x06015EB8 RID: 89784
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

		// Token: 0x17007583 RID: 30083
		// (get) Token: 0x06015EBA RID: 89786
		public virtual extern int IHTMLUniqueName_uniqueNumber
		{
			[TypeLibFunc(64)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17007584 RID: 30084
		// (get) Token: 0x06015EBB RID: 89787
		public virtual extern string IHTMLUniqueName_uniqueID
		{
			[TypeLibFunc(64)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17007585 RID: 30085
		// (get) Token: 0x06015EBC RID: 89788
		public virtual extern int IHTMLDOMNode_nodeType
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17007586 RID: 30086
		// (get) Token: 0x06015EBD RID: 89789
		public virtual extern IHTMLDOMNode IHTMLDOMNode_parentNode
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x06015EBE RID: 89790
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLDOMNode_hasChildNodes();

		// Token: 0x17007587 RID: 30087
		// (get) Token: 0x06015EBF RID: 89791
		public virtual extern object IHTMLDOMNode_childNodes
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17007588 RID: 30088
		// (get) Token: 0x06015EC0 RID: 89792
		public virtual extern object IHTMLDOMNode_attributes
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x06015EC1 RID: 89793
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_insertBefore([MarshalAs(28)] [In] IHTMLDOMNode newChild, [MarshalAs(27)] [In] [Optional] object refChild);

		// Token: 0x06015EC2 RID: 89794
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_removeChild([MarshalAs(28)] [In] IHTMLDOMNode oldChild);

		// Token: 0x06015EC3 RID: 89795
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_replaceChild([MarshalAs(28)] [In] IHTMLDOMNode newChild, [MarshalAs(28)] [In] IHTMLDOMNode oldChild);

		// Token: 0x06015EC4 RID: 89796
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_cloneNode([In] bool fDeep);

		// Token: 0x06015EC5 RID: 89797
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_removeNode([In] bool fDeep = false);

		// Token: 0x06015EC6 RID: 89798
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_swapNode([MarshalAs(28)] [In] IHTMLDOMNode otherNode);

		// Token: 0x06015EC7 RID: 89799
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_replaceNode([MarshalAs(28)] [In] IHTMLDOMNode replacement);

		// Token: 0x06015EC8 RID: 89800
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_appendChild([MarshalAs(28)] [In] IHTMLDOMNode newChild);

		// Token: 0x17007589 RID: 30089
		// (get) Token: 0x06015EC9 RID: 89801
		public virtual extern string IHTMLDOMNode_nodeName
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x1700758A RID: 30090
		// (get) Token: 0x06015ECB RID: 89803
		// (set) Token: 0x06015ECA RID: 89802
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

		// Token: 0x1700758B RID: 30091
		// (get) Token: 0x06015ECC RID: 89804
		public virtual extern IHTMLDOMNode IHTMLDOMNode_firstChild
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x1700758C RID: 30092
		// (get) Token: 0x06015ECD RID: 89805
		public virtual extern IHTMLDOMNode IHTMLDOMNode_lastChild
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x1700758D RID: 30093
		// (get) Token: 0x06015ECE RID: 89806
		public virtual extern IHTMLDOMNode IHTMLDOMNode_previousSibling
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x1700758E RID: 30094
		// (get) Token: 0x06015ECF RID: 89807
		public virtual extern IHTMLDOMNode IHTMLDOMNode_nextSibling
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x1700758F RID: 30095
		// (get) Token: 0x06015ED0 RID: 89808
		public virtual extern object IHTMLDOMNode2_ownerDocument
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17007590 RID: 30096
		// (get) Token: 0x06015ED2 RID: 89810
		// (set) Token: 0x06015ED1 RID: 89809
		public virtual extern string IHTMLStyleElement_type
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

		// Token: 0x17007591 RID: 30097
		// (get) Token: 0x06015ED3 RID: 89811
		public virtual extern string IHTMLStyleElement_readyState
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17007592 RID: 30098
		// (get) Token: 0x06015ED5 RID: 89813
		// (set) Token: 0x06015ED4 RID: 89812
		public virtual extern object IHTMLStyleElement_onreadystatechange
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17007593 RID: 30099
		// (get) Token: 0x06015ED7 RID: 89815
		// (set) Token: 0x06015ED6 RID: 89814
		public virtual extern object IHTMLStyleElement_onload
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17007594 RID: 30100
		// (get) Token: 0x06015ED9 RID: 89817
		// (set) Token: 0x06015ED8 RID: 89816
		public virtual extern object IHTMLStyleElement_onerror
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17007595 RID: 30101
		// (get) Token: 0x06015EDA RID: 89818
		public virtual extern IHTMLStyleSheet IHTMLStyleElement_styleSheet
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17007596 RID: 30102
		// (get) Token: 0x06015EDC RID: 89820
		// (set) Token: 0x06015EDB RID: 89819
		public virtual extern bool IHTMLStyleElement_disabled
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x17007597 RID: 30103
		// (get) Token: 0x06015EDE RID: 89822
		// (set) Token: 0x06015EDD RID: 89821
		public virtual extern string IHTMLStyleElement_media
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

		// Token: 0x14002A04 RID: 10756
		// (add) Token: 0x06015EDF RID: 89823
		// (remove) Token: 0x06015EE0 RID: 89824
		public virtual extern event HTMLStyleElementEvents_onhelpEventHandler HTMLStyleElementEvents_Event_onhelp;

		// Token: 0x14002A05 RID: 10757
		// (add) Token: 0x06015EE1 RID: 89825
		// (remove) Token: 0x06015EE2 RID: 89826
		public virtual extern event HTMLStyleElementEvents_onclickEventHandler HTMLStyleElementEvents_Event_onclick;

		// Token: 0x14002A06 RID: 10758
		// (add) Token: 0x06015EE3 RID: 89827
		// (remove) Token: 0x06015EE4 RID: 89828
		public virtual extern event HTMLStyleElementEvents_ondblclickEventHandler HTMLStyleElementEvents_Event_ondblclick;

		// Token: 0x14002A07 RID: 10759
		// (add) Token: 0x06015EE5 RID: 89829
		// (remove) Token: 0x06015EE6 RID: 89830
		public virtual extern event HTMLStyleElementEvents_onkeypressEventHandler HTMLStyleElementEvents_Event_onkeypress;

		// Token: 0x14002A08 RID: 10760
		// (add) Token: 0x06015EE7 RID: 89831
		// (remove) Token: 0x06015EE8 RID: 89832
		public virtual extern event HTMLStyleElementEvents_onkeydownEventHandler HTMLStyleElementEvents_Event_onkeydown;

		// Token: 0x14002A09 RID: 10761
		// (add) Token: 0x06015EE9 RID: 89833
		// (remove) Token: 0x06015EEA RID: 89834
		public virtual extern event HTMLStyleElementEvents_onkeyupEventHandler HTMLStyleElementEvents_Event_onkeyup;

		// Token: 0x14002A0A RID: 10762
		// (add) Token: 0x06015EEB RID: 89835
		// (remove) Token: 0x06015EEC RID: 89836
		public virtual extern event HTMLStyleElementEvents_onmouseoutEventHandler HTMLStyleElementEvents_Event_onmouseout;

		// Token: 0x14002A0B RID: 10763
		// (add) Token: 0x06015EED RID: 89837
		// (remove) Token: 0x06015EEE RID: 89838
		public virtual extern event HTMLStyleElementEvents_onmouseoverEventHandler HTMLStyleElementEvents_Event_onmouseover;

		// Token: 0x14002A0C RID: 10764
		// (add) Token: 0x06015EEF RID: 89839
		// (remove) Token: 0x06015EF0 RID: 89840
		public virtual extern event HTMLStyleElementEvents_onmousemoveEventHandler HTMLStyleElementEvents_Event_onmousemove;

		// Token: 0x14002A0D RID: 10765
		// (add) Token: 0x06015EF1 RID: 89841
		// (remove) Token: 0x06015EF2 RID: 89842
		public virtual extern event HTMLStyleElementEvents_onmousedownEventHandler HTMLStyleElementEvents_Event_onmousedown;

		// Token: 0x14002A0E RID: 10766
		// (add) Token: 0x06015EF3 RID: 89843
		// (remove) Token: 0x06015EF4 RID: 89844
		public virtual extern event HTMLStyleElementEvents_onmouseupEventHandler HTMLStyleElementEvents_Event_onmouseup;

		// Token: 0x14002A0F RID: 10767
		// (add) Token: 0x06015EF5 RID: 89845
		// (remove) Token: 0x06015EF6 RID: 89846
		public virtual extern event HTMLStyleElementEvents_onselectstartEventHandler HTMLStyleElementEvents_Event_onselectstart;

		// Token: 0x14002A10 RID: 10768
		// (add) Token: 0x06015EF7 RID: 89847
		// (remove) Token: 0x06015EF8 RID: 89848
		public virtual extern event HTMLStyleElementEvents_onfilterchangeEventHandler HTMLStyleElementEvents_Event_onfilterchange;

		// Token: 0x14002A11 RID: 10769
		// (add) Token: 0x06015EF9 RID: 89849
		// (remove) Token: 0x06015EFA RID: 89850
		public virtual extern event HTMLStyleElementEvents_ondragstartEventHandler HTMLStyleElementEvents_Event_ondragstart;

		// Token: 0x14002A12 RID: 10770
		// (add) Token: 0x06015EFB RID: 89851
		// (remove) Token: 0x06015EFC RID: 89852
		public virtual extern event HTMLStyleElementEvents_onbeforeupdateEventHandler HTMLStyleElementEvents_Event_onbeforeupdate;

		// Token: 0x14002A13 RID: 10771
		// (add) Token: 0x06015EFD RID: 89853
		// (remove) Token: 0x06015EFE RID: 89854
		public virtual extern event HTMLStyleElementEvents_onafterupdateEventHandler HTMLStyleElementEvents_Event_onafterupdate;

		// Token: 0x14002A14 RID: 10772
		// (add) Token: 0x06015EFF RID: 89855
		// (remove) Token: 0x06015F00 RID: 89856
		public virtual extern event HTMLStyleElementEvents_onerrorupdateEventHandler HTMLStyleElementEvents_Event_onerrorupdate;

		// Token: 0x14002A15 RID: 10773
		// (add) Token: 0x06015F01 RID: 89857
		// (remove) Token: 0x06015F02 RID: 89858
		public virtual extern event HTMLStyleElementEvents_onrowexitEventHandler HTMLStyleElementEvents_Event_onrowexit;

		// Token: 0x14002A16 RID: 10774
		// (add) Token: 0x06015F03 RID: 89859
		// (remove) Token: 0x06015F04 RID: 89860
		public virtual extern event HTMLStyleElementEvents_onrowenterEventHandler HTMLStyleElementEvents_Event_onrowenter;

		// Token: 0x14002A17 RID: 10775
		// (add) Token: 0x06015F05 RID: 89861
		// (remove) Token: 0x06015F06 RID: 89862
		public virtual extern event HTMLStyleElementEvents_ondatasetchangedEventHandler HTMLStyleElementEvents_Event_ondatasetchanged;

		// Token: 0x14002A18 RID: 10776
		// (add) Token: 0x06015F07 RID: 89863
		// (remove) Token: 0x06015F08 RID: 89864
		public virtual extern event HTMLStyleElementEvents_ondataavailableEventHandler HTMLStyleElementEvents_Event_ondataavailable;

		// Token: 0x14002A19 RID: 10777
		// (add) Token: 0x06015F09 RID: 89865
		// (remove) Token: 0x06015F0A RID: 89866
		public virtual extern event HTMLStyleElementEvents_ondatasetcompleteEventHandler HTMLStyleElementEvents_Event_ondatasetcomplete;

		// Token: 0x14002A1A RID: 10778
		// (add) Token: 0x06015F0B RID: 89867
		// (remove) Token: 0x06015F0C RID: 89868
		public virtual extern event HTMLStyleElementEvents_onlosecaptureEventHandler HTMLStyleElementEvents_Event_onlosecapture;

		// Token: 0x14002A1B RID: 10779
		// (add) Token: 0x06015F0D RID: 89869
		// (remove) Token: 0x06015F0E RID: 89870
		public virtual extern event HTMLStyleElementEvents_onpropertychangeEventHandler HTMLStyleElementEvents_Event_onpropertychange;

		// Token: 0x14002A1C RID: 10780
		// (add) Token: 0x06015F0F RID: 89871
		// (remove) Token: 0x06015F10 RID: 89872
		public virtual extern event HTMLStyleElementEvents_onscrollEventHandler HTMLStyleElementEvents_Event_onscroll;

		// Token: 0x14002A1D RID: 10781
		// (add) Token: 0x06015F11 RID: 89873
		// (remove) Token: 0x06015F12 RID: 89874
		public virtual extern event HTMLStyleElementEvents_onfocusEventHandler HTMLStyleElementEvents_Event_onfocus;

		// Token: 0x14002A1E RID: 10782
		// (add) Token: 0x06015F13 RID: 89875
		// (remove) Token: 0x06015F14 RID: 89876
		public virtual extern event HTMLStyleElementEvents_onblurEventHandler HTMLStyleElementEvents_Event_onblur;

		// Token: 0x14002A1F RID: 10783
		// (add) Token: 0x06015F15 RID: 89877
		// (remove) Token: 0x06015F16 RID: 89878
		public virtual extern event HTMLStyleElementEvents_onresizeEventHandler HTMLStyleElementEvents_Event_onresize;

		// Token: 0x14002A20 RID: 10784
		// (add) Token: 0x06015F17 RID: 89879
		// (remove) Token: 0x06015F18 RID: 89880
		public virtual extern event HTMLStyleElementEvents_ondragEventHandler HTMLStyleElementEvents_Event_ondrag;

		// Token: 0x14002A21 RID: 10785
		// (add) Token: 0x06015F19 RID: 89881
		// (remove) Token: 0x06015F1A RID: 89882
		public virtual extern event HTMLStyleElementEvents_ondragendEventHandler HTMLStyleElementEvents_Event_ondragend;

		// Token: 0x14002A22 RID: 10786
		// (add) Token: 0x06015F1B RID: 89883
		// (remove) Token: 0x06015F1C RID: 89884
		public virtual extern event HTMLStyleElementEvents_ondragenterEventHandler HTMLStyleElementEvents_Event_ondragenter;

		// Token: 0x14002A23 RID: 10787
		// (add) Token: 0x06015F1D RID: 89885
		// (remove) Token: 0x06015F1E RID: 89886
		public virtual extern event HTMLStyleElementEvents_ondragoverEventHandler HTMLStyleElementEvents_Event_ondragover;

		// Token: 0x14002A24 RID: 10788
		// (add) Token: 0x06015F1F RID: 89887
		// (remove) Token: 0x06015F20 RID: 89888
		public virtual extern event HTMLStyleElementEvents_ondragleaveEventHandler HTMLStyleElementEvents_Event_ondragleave;

		// Token: 0x14002A25 RID: 10789
		// (add) Token: 0x06015F21 RID: 89889
		// (remove) Token: 0x06015F22 RID: 89890
		public virtual extern event HTMLStyleElementEvents_ondropEventHandler HTMLStyleElementEvents_Event_ondrop;

		// Token: 0x14002A26 RID: 10790
		// (add) Token: 0x06015F23 RID: 89891
		// (remove) Token: 0x06015F24 RID: 89892
		public virtual extern event HTMLStyleElementEvents_onbeforecutEventHandler HTMLStyleElementEvents_Event_onbeforecut;

		// Token: 0x14002A27 RID: 10791
		// (add) Token: 0x06015F25 RID: 89893
		// (remove) Token: 0x06015F26 RID: 89894
		public virtual extern event HTMLStyleElementEvents_oncutEventHandler HTMLStyleElementEvents_Event_oncut;

		// Token: 0x14002A28 RID: 10792
		// (add) Token: 0x06015F27 RID: 89895
		// (remove) Token: 0x06015F28 RID: 89896
		public virtual extern event HTMLStyleElementEvents_onbeforecopyEventHandler HTMLStyleElementEvents_Event_onbeforecopy;

		// Token: 0x14002A29 RID: 10793
		// (add) Token: 0x06015F29 RID: 89897
		// (remove) Token: 0x06015F2A RID: 89898
		public virtual extern event HTMLStyleElementEvents_oncopyEventHandler HTMLStyleElementEvents_Event_oncopy;

		// Token: 0x14002A2A RID: 10794
		// (add) Token: 0x06015F2B RID: 89899
		// (remove) Token: 0x06015F2C RID: 89900
		public virtual extern event HTMLStyleElementEvents_onbeforepasteEventHandler HTMLStyleElementEvents_Event_onbeforepaste;

		// Token: 0x14002A2B RID: 10795
		// (add) Token: 0x06015F2D RID: 89901
		// (remove) Token: 0x06015F2E RID: 89902
		public virtual extern event HTMLStyleElementEvents_onpasteEventHandler HTMLStyleElementEvents_Event_onpaste;

		// Token: 0x14002A2C RID: 10796
		// (add) Token: 0x06015F2F RID: 89903
		// (remove) Token: 0x06015F30 RID: 89904
		public virtual extern event HTMLStyleElementEvents_oncontextmenuEventHandler HTMLStyleElementEvents_Event_oncontextmenu;

		// Token: 0x14002A2D RID: 10797
		// (add) Token: 0x06015F31 RID: 89905
		// (remove) Token: 0x06015F32 RID: 89906
		public virtual extern event HTMLStyleElementEvents_onrowsdeleteEventHandler HTMLStyleElementEvents_Event_onrowsdelete;

		// Token: 0x14002A2E RID: 10798
		// (add) Token: 0x06015F33 RID: 89907
		// (remove) Token: 0x06015F34 RID: 89908
		public virtual extern event HTMLStyleElementEvents_onrowsinsertedEventHandler HTMLStyleElementEvents_Event_onrowsinserted;

		// Token: 0x14002A2F RID: 10799
		// (add) Token: 0x06015F35 RID: 89909
		// (remove) Token: 0x06015F36 RID: 89910
		public virtual extern event HTMLStyleElementEvents_oncellchangeEventHandler HTMLStyleElementEvents_Event_oncellchange;

		// Token: 0x14002A30 RID: 10800
		// (add) Token: 0x06015F37 RID: 89911
		// (remove) Token: 0x06015F38 RID: 89912
		public virtual extern event HTMLStyleElementEvents_onreadystatechangeEventHandler HTMLStyleElementEvents_Event_onreadystatechange;

		// Token: 0x14002A31 RID: 10801
		// (add) Token: 0x06015F39 RID: 89913
		// (remove) Token: 0x06015F3A RID: 89914
		public virtual extern event HTMLStyleElementEvents_onbeforeeditfocusEventHandler HTMLStyleElementEvents_Event_onbeforeeditfocus;

		// Token: 0x14002A32 RID: 10802
		// (add) Token: 0x06015F3B RID: 89915
		// (remove) Token: 0x06015F3C RID: 89916
		public virtual extern event HTMLStyleElementEvents_onlayoutcompleteEventHandler HTMLStyleElementEvents_Event_onlayoutcomplete;

		// Token: 0x14002A33 RID: 10803
		// (add) Token: 0x06015F3D RID: 89917
		// (remove) Token: 0x06015F3E RID: 89918
		public virtual extern event HTMLStyleElementEvents_onpageEventHandler HTMLStyleElementEvents_Event_onpage;

		// Token: 0x14002A34 RID: 10804
		// (add) Token: 0x06015F3F RID: 89919
		// (remove) Token: 0x06015F40 RID: 89920
		public virtual extern event HTMLStyleElementEvents_onbeforedeactivateEventHandler HTMLStyleElementEvents_Event_onbeforedeactivate;

		// Token: 0x14002A35 RID: 10805
		// (add) Token: 0x06015F41 RID: 89921
		// (remove) Token: 0x06015F42 RID: 89922
		public virtual extern event HTMLStyleElementEvents_onbeforeactivateEventHandler HTMLStyleElementEvents_Event_onbeforeactivate;

		// Token: 0x14002A36 RID: 10806
		// (add) Token: 0x06015F43 RID: 89923
		// (remove) Token: 0x06015F44 RID: 89924
		public virtual extern event HTMLStyleElementEvents_onmoveEventHandler HTMLStyleElementEvents_Event_onmove;

		// Token: 0x14002A37 RID: 10807
		// (add) Token: 0x06015F45 RID: 89925
		// (remove) Token: 0x06015F46 RID: 89926
		public virtual extern event HTMLStyleElementEvents_oncontrolselectEventHandler HTMLStyleElementEvents_Event_oncontrolselect;

		// Token: 0x14002A38 RID: 10808
		// (add) Token: 0x06015F47 RID: 89927
		// (remove) Token: 0x06015F48 RID: 89928
		public virtual extern event HTMLStyleElementEvents_onmovestartEventHandler HTMLStyleElementEvents_Event_onmovestart;

		// Token: 0x14002A39 RID: 10809
		// (add) Token: 0x06015F49 RID: 89929
		// (remove) Token: 0x06015F4A RID: 89930
		public virtual extern event HTMLStyleElementEvents_onmoveendEventHandler HTMLStyleElementEvents_Event_onmoveend;

		// Token: 0x14002A3A RID: 10810
		// (add) Token: 0x06015F4B RID: 89931
		// (remove) Token: 0x06015F4C RID: 89932
		public virtual extern event HTMLStyleElementEvents_onresizestartEventHandler HTMLStyleElementEvents_Event_onresizestart;

		// Token: 0x14002A3B RID: 10811
		// (add) Token: 0x06015F4D RID: 89933
		// (remove) Token: 0x06015F4E RID: 89934
		public virtual extern event HTMLStyleElementEvents_onresizeendEventHandler HTMLStyleElementEvents_Event_onresizeend;

		// Token: 0x14002A3C RID: 10812
		// (add) Token: 0x06015F4F RID: 89935
		// (remove) Token: 0x06015F50 RID: 89936
		public virtual extern event HTMLStyleElementEvents_onmouseenterEventHandler HTMLStyleElementEvents_Event_onmouseenter;

		// Token: 0x14002A3D RID: 10813
		// (add) Token: 0x06015F51 RID: 89937
		// (remove) Token: 0x06015F52 RID: 89938
		public virtual extern event HTMLStyleElementEvents_onmouseleaveEventHandler HTMLStyleElementEvents_Event_onmouseleave;

		// Token: 0x14002A3E RID: 10814
		// (add) Token: 0x06015F53 RID: 89939
		// (remove) Token: 0x06015F54 RID: 89940
		public virtual extern event HTMLStyleElementEvents_onmousewheelEventHandler HTMLStyleElementEvents_Event_onmousewheel;

		// Token: 0x14002A3F RID: 10815
		// (add) Token: 0x06015F55 RID: 89941
		// (remove) Token: 0x06015F56 RID: 89942
		public virtual extern event HTMLStyleElementEvents_onactivateEventHandler HTMLStyleElementEvents_Event_onactivate;

		// Token: 0x14002A40 RID: 10816
		// (add) Token: 0x06015F57 RID: 89943
		// (remove) Token: 0x06015F58 RID: 89944
		public virtual extern event HTMLStyleElementEvents_ondeactivateEventHandler HTMLStyleElementEvents_Event_ondeactivate;

		// Token: 0x14002A41 RID: 10817
		// (add) Token: 0x06015F59 RID: 89945
		// (remove) Token: 0x06015F5A RID: 89946
		public virtual extern event HTMLStyleElementEvents_onfocusinEventHandler HTMLStyleElementEvents_Event_onfocusin;

		// Token: 0x14002A42 RID: 10818
		// (add) Token: 0x06015F5B RID: 89947
		// (remove) Token: 0x06015F5C RID: 89948
		public virtual extern event HTMLStyleElementEvents_onfocusoutEventHandler HTMLStyleElementEvents_Event_onfocusout;

		// Token: 0x14002A43 RID: 10819
		// (add) Token: 0x06015F5D RID: 89949
		// (remove) Token: 0x06015F5E RID: 89950
		public virtual extern event HTMLStyleElementEvents_onloadEventHandler HTMLStyleElementEvents_Event_onload;

		// Token: 0x14002A44 RID: 10820
		// (add) Token: 0x06015F5F RID: 89951
		// (remove) Token: 0x06015F60 RID: 89952
		public virtual extern event HTMLStyleElementEvents_onerrorEventHandler HTMLStyleElementEvents_Event_onerror;

		// Token: 0x14002A45 RID: 10821
		// (add) Token: 0x06015F61 RID: 89953
		// (remove) Token: 0x06015F62 RID: 89954
		public virtual extern event HTMLStyleElementEvents2_onhelpEventHandler HTMLStyleElementEvents2_Event_onhelp;

		// Token: 0x14002A46 RID: 10822
		// (add) Token: 0x06015F63 RID: 89955
		// (remove) Token: 0x06015F64 RID: 89956
		public virtual extern event HTMLStyleElementEvents2_onclickEventHandler HTMLStyleElementEvents2_Event_onclick;

		// Token: 0x14002A47 RID: 10823
		// (add) Token: 0x06015F65 RID: 89957
		// (remove) Token: 0x06015F66 RID: 89958
		public virtual extern event HTMLStyleElementEvents2_ondblclickEventHandler HTMLStyleElementEvents2_Event_ondblclick;

		// Token: 0x14002A48 RID: 10824
		// (add) Token: 0x06015F67 RID: 89959
		// (remove) Token: 0x06015F68 RID: 89960
		public virtual extern event HTMLStyleElementEvents2_onkeypressEventHandler HTMLStyleElementEvents2_Event_onkeypress;

		// Token: 0x14002A49 RID: 10825
		// (add) Token: 0x06015F69 RID: 89961
		// (remove) Token: 0x06015F6A RID: 89962
		public virtual extern event HTMLStyleElementEvents2_onkeydownEventHandler HTMLStyleElementEvents2_Event_onkeydown;

		// Token: 0x14002A4A RID: 10826
		// (add) Token: 0x06015F6B RID: 89963
		// (remove) Token: 0x06015F6C RID: 89964
		public virtual extern event HTMLStyleElementEvents2_onkeyupEventHandler HTMLStyleElementEvents2_Event_onkeyup;

		// Token: 0x14002A4B RID: 10827
		// (add) Token: 0x06015F6D RID: 89965
		// (remove) Token: 0x06015F6E RID: 89966
		public virtual extern event HTMLStyleElementEvents2_onmouseoutEventHandler HTMLStyleElementEvents2_Event_onmouseout;

		// Token: 0x14002A4C RID: 10828
		// (add) Token: 0x06015F6F RID: 89967
		// (remove) Token: 0x06015F70 RID: 89968
		public virtual extern event HTMLStyleElementEvents2_onmouseoverEventHandler HTMLStyleElementEvents2_Event_onmouseover;

		// Token: 0x14002A4D RID: 10829
		// (add) Token: 0x06015F71 RID: 89969
		// (remove) Token: 0x06015F72 RID: 89970
		public virtual extern event HTMLStyleElementEvents2_onmousemoveEventHandler HTMLStyleElementEvents2_Event_onmousemove;

		// Token: 0x14002A4E RID: 10830
		// (add) Token: 0x06015F73 RID: 89971
		// (remove) Token: 0x06015F74 RID: 89972
		public virtual extern event HTMLStyleElementEvents2_onmousedownEventHandler HTMLStyleElementEvents2_Event_onmousedown;

		// Token: 0x14002A4F RID: 10831
		// (add) Token: 0x06015F75 RID: 89973
		// (remove) Token: 0x06015F76 RID: 89974
		public virtual extern event HTMLStyleElementEvents2_onmouseupEventHandler HTMLStyleElementEvents2_Event_onmouseup;

		// Token: 0x14002A50 RID: 10832
		// (add) Token: 0x06015F77 RID: 89975
		// (remove) Token: 0x06015F78 RID: 89976
		public virtual extern event HTMLStyleElementEvents2_onselectstartEventHandler HTMLStyleElementEvents2_Event_onselectstart;

		// Token: 0x14002A51 RID: 10833
		// (add) Token: 0x06015F79 RID: 89977
		// (remove) Token: 0x06015F7A RID: 89978
		public virtual extern event HTMLStyleElementEvents2_onfilterchangeEventHandler HTMLStyleElementEvents2_Event_onfilterchange;

		// Token: 0x14002A52 RID: 10834
		// (add) Token: 0x06015F7B RID: 89979
		// (remove) Token: 0x06015F7C RID: 89980
		public virtual extern event HTMLStyleElementEvents2_ondragstartEventHandler HTMLStyleElementEvents2_Event_ondragstart;

		// Token: 0x14002A53 RID: 10835
		// (add) Token: 0x06015F7D RID: 89981
		// (remove) Token: 0x06015F7E RID: 89982
		public virtual extern event HTMLStyleElementEvents2_onbeforeupdateEventHandler HTMLStyleElementEvents2_Event_onbeforeupdate;

		// Token: 0x14002A54 RID: 10836
		// (add) Token: 0x06015F7F RID: 89983
		// (remove) Token: 0x06015F80 RID: 89984
		public virtual extern event HTMLStyleElementEvents2_onafterupdateEventHandler HTMLStyleElementEvents2_Event_onafterupdate;

		// Token: 0x14002A55 RID: 10837
		// (add) Token: 0x06015F81 RID: 89985
		// (remove) Token: 0x06015F82 RID: 89986
		public virtual extern event HTMLStyleElementEvents2_onerrorupdateEventHandler HTMLStyleElementEvents2_Event_onerrorupdate;

		// Token: 0x14002A56 RID: 10838
		// (add) Token: 0x06015F83 RID: 89987
		// (remove) Token: 0x06015F84 RID: 89988
		public virtual extern event HTMLStyleElementEvents2_onrowexitEventHandler HTMLStyleElementEvents2_Event_onrowexit;

		// Token: 0x14002A57 RID: 10839
		// (add) Token: 0x06015F85 RID: 89989
		// (remove) Token: 0x06015F86 RID: 89990
		public virtual extern event HTMLStyleElementEvents2_onrowenterEventHandler HTMLStyleElementEvents2_Event_onrowenter;

		// Token: 0x14002A58 RID: 10840
		// (add) Token: 0x06015F87 RID: 89991
		// (remove) Token: 0x06015F88 RID: 89992
		public virtual extern event HTMLStyleElementEvents2_ondatasetchangedEventHandler HTMLStyleElementEvents2_Event_ondatasetchanged;

		// Token: 0x14002A59 RID: 10841
		// (add) Token: 0x06015F89 RID: 89993
		// (remove) Token: 0x06015F8A RID: 89994
		public virtual extern event HTMLStyleElementEvents2_ondataavailableEventHandler HTMLStyleElementEvents2_Event_ondataavailable;

		// Token: 0x14002A5A RID: 10842
		// (add) Token: 0x06015F8B RID: 89995
		// (remove) Token: 0x06015F8C RID: 89996
		public virtual extern event HTMLStyleElementEvents2_ondatasetcompleteEventHandler HTMLStyleElementEvents2_Event_ondatasetcomplete;

		// Token: 0x14002A5B RID: 10843
		// (add) Token: 0x06015F8D RID: 89997
		// (remove) Token: 0x06015F8E RID: 89998
		public virtual extern event HTMLStyleElementEvents2_onlosecaptureEventHandler HTMLStyleElementEvents2_Event_onlosecapture;

		// Token: 0x14002A5C RID: 10844
		// (add) Token: 0x06015F8F RID: 89999
		// (remove) Token: 0x06015F90 RID: 90000
		public virtual extern event HTMLStyleElementEvents2_onpropertychangeEventHandler HTMLStyleElementEvents2_Event_onpropertychange;

		// Token: 0x14002A5D RID: 10845
		// (add) Token: 0x06015F91 RID: 90001
		// (remove) Token: 0x06015F92 RID: 90002
		public virtual extern event HTMLStyleElementEvents2_onscrollEventHandler HTMLStyleElementEvents2_Event_onscroll;

		// Token: 0x14002A5E RID: 10846
		// (add) Token: 0x06015F93 RID: 90003
		// (remove) Token: 0x06015F94 RID: 90004
		public virtual extern event HTMLStyleElementEvents2_onfocusEventHandler HTMLStyleElementEvents2_Event_onfocus;

		// Token: 0x14002A5F RID: 10847
		// (add) Token: 0x06015F95 RID: 90005
		// (remove) Token: 0x06015F96 RID: 90006
		public virtual extern event HTMLStyleElementEvents2_onblurEventHandler HTMLStyleElementEvents2_Event_onblur;

		// Token: 0x14002A60 RID: 10848
		// (add) Token: 0x06015F97 RID: 90007
		// (remove) Token: 0x06015F98 RID: 90008
		public virtual extern event HTMLStyleElementEvents2_onresizeEventHandler HTMLStyleElementEvents2_Event_onresize;

		// Token: 0x14002A61 RID: 10849
		// (add) Token: 0x06015F99 RID: 90009
		// (remove) Token: 0x06015F9A RID: 90010
		public virtual extern event HTMLStyleElementEvents2_ondragEventHandler HTMLStyleElementEvents2_Event_ondrag;

		// Token: 0x14002A62 RID: 10850
		// (add) Token: 0x06015F9B RID: 90011
		// (remove) Token: 0x06015F9C RID: 90012
		public virtual extern event HTMLStyleElementEvents2_ondragendEventHandler HTMLStyleElementEvents2_Event_ondragend;

		// Token: 0x14002A63 RID: 10851
		// (add) Token: 0x06015F9D RID: 90013
		// (remove) Token: 0x06015F9E RID: 90014
		public virtual extern event HTMLStyleElementEvents2_ondragenterEventHandler HTMLStyleElementEvents2_Event_ondragenter;

		// Token: 0x14002A64 RID: 10852
		// (add) Token: 0x06015F9F RID: 90015
		// (remove) Token: 0x06015FA0 RID: 90016
		public virtual extern event HTMLStyleElementEvents2_ondragoverEventHandler HTMLStyleElementEvents2_Event_ondragover;

		// Token: 0x14002A65 RID: 10853
		// (add) Token: 0x06015FA1 RID: 90017
		// (remove) Token: 0x06015FA2 RID: 90018
		public virtual extern event HTMLStyleElementEvents2_ondragleaveEventHandler HTMLStyleElementEvents2_Event_ondragleave;

		// Token: 0x14002A66 RID: 10854
		// (add) Token: 0x06015FA3 RID: 90019
		// (remove) Token: 0x06015FA4 RID: 90020
		public virtual extern event HTMLStyleElementEvents2_ondropEventHandler HTMLStyleElementEvents2_Event_ondrop;

		// Token: 0x14002A67 RID: 10855
		// (add) Token: 0x06015FA5 RID: 90021
		// (remove) Token: 0x06015FA6 RID: 90022
		public virtual extern event HTMLStyleElementEvents2_onbeforecutEventHandler HTMLStyleElementEvents2_Event_onbeforecut;

		// Token: 0x14002A68 RID: 10856
		// (add) Token: 0x06015FA7 RID: 90023
		// (remove) Token: 0x06015FA8 RID: 90024
		public virtual extern event HTMLStyleElementEvents2_oncutEventHandler HTMLStyleElementEvents2_Event_oncut;

		// Token: 0x14002A69 RID: 10857
		// (add) Token: 0x06015FA9 RID: 90025
		// (remove) Token: 0x06015FAA RID: 90026
		public virtual extern event HTMLStyleElementEvents2_onbeforecopyEventHandler HTMLStyleElementEvents2_Event_onbeforecopy;

		// Token: 0x14002A6A RID: 10858
		// (add) Token: 0x06015FAB RID: 90027
		// (remove) Token: 0x06015FAC RID: 90028
		public virtual extern event HTMLStyleElementEvents2_oncopyEventHandler HTMLStyleElementEvents2_Event_oncopy;

		// Token: 0x14002A6B RID: 10859
		// (add) Token: 0x06015FAD RID: 90029
		// (remove) Token: 0x06015FAE RID: 90030
		public virtual extern event HTMLStyleElementEvents2_onbeforepasteEventHandler HTMLStyleElementEvents2_Event_onbeforepaste;

		// Token: 0x14002A6C RID: 10860
		// (add) Token: 0x06015FAF RID: 90031
		// (remove) Token: 0x06015FB0 RID: 90032
		public virtual extern event HTMLStyleElementEvents2_onpasteEventHandler HTMLStyleElementEvents2_Event_onpaste;

		// Token: 0x14002A6D RID: 10861
		// (add) Token: 0x06015FB1 RID: 90033
		// (remove) Token: 0x06015FB2 RID: 90034
		public virtual extern event HTMLStyleElementEvents2_oncontextmenuEventHandler HTMLStyleElementEvents2_Event_oncontextmenu;

		// Token: 0x14002A6E RID: 10862
		// (add) Token: 0x06015FB3 RID: 90035
		// (remove) Token: 0x06015FB4 RID: 90036
		public virtual extern event HTMLStyleElementEvents2_onrowsdeleteEventHandler HTMLStyleElementEvents2_Event_onrowsdelete;

		// Token: 0x14002A6F RID: 10863
		// (add) Token: 0x06015FB5 RID: 90037
		// (remove) Token: 0x06015FB6 RID: 90038
		public virtual extern event HTMLStyleElementEvents2_onrowsinsertedEventHandler HTMLStyleElementEvents2_Event_onrowsinserted;

		// Token: 0x14002A70 RID: 10864
		// (add) Token: 0x06015FB7 RID: 90039
		// (remove) Token: 0x06015FB8 RID: 90040
		public virtual extern event HTMLStyleElementEvents2_oncellchangeEventHandler HTMLStyleElementEvents2_Event_oncellchange;

		// Token: 0x14002A71 RID: 10865
		// (add) Token: 0x06015FB9 RID: 90041
		// (remove) Token: 0x06015FBA RID: 90042
		public virtual extern event HTMLStyleElementEvents2_onreadystatechangeEventHandler HTMLStyleElementEvents2_Event_onreadystatechange;

		// Token: 0x14002A72 RID: 10866
		// (add) Token: 0x06015FBB RID: 90043
		// (remove) Token: 0x06015FBC RID: 90044
		public virtual extern event HTMLStyleElementEvents2_onlayoutcompleteEventHandler HTMLStyleElementEvents2_Event_onlayoutcomplete;

		// Token: 0x14002A73 RID: 10867
		// (add) Token: 0x06015FBD RID: 90045
		// (remove) Token: 0x06015FBE RID: 90046
		public virtual extern event HTMLStyleElementEvents2_onpageEventHandler HTMLStyleElementEvents2_Event_onpage;

		// Token: 0x14002A74 RID: 10868
		// (add) Token: 0x06015FBF RID: 90047
		// (remove) Token: 0x06015FC0 RID: 90048
		public virtual extern event HTMLStyleElementEvents2_onmouseenterEventHandler HTMLStyleElementEvents2_Event_onmouseenter;

		// Token: 0x14002A75 RID: 10869
		// (add) Token: 0x06015FC1 RID: 90049
		// (remove) Token: 0x06015FC2 RID: 90050
		public virtual extern event HTMLStyleElementEvents2_onmouseleaveEventHandler HTMLStyleElementEvents2_Event_onmouseleave;

		// Token: 0x14002A76 RID: 10870
		// (add) Token: 0x06015FC3 RID: 90051
		// (remove) Token: 0x06015FC4 RID: 90052
		public virtual extern event HTMLStyleElementEvents2_onactivateEventHandler HTMLStyleElementEvents2_Event_onactivate;

		// Token: 0x14002A77 RID: 10871
		// (add) Token: 0x06015FC5 RID: 90053
		// (remove) Token: 0x06015FC6 RID: 90054
		public virtual extern event HTMLStyleElementEvents2_ondeactivateEventHandler HTMLStyleElementEvents2_Event_ondeactivate;

		// Token: 0x14002A78 RID: 10872
		// (add) Token: 0x06015FC7 RID: 90055
		// (remove) Token: 0x06015FC8 RID: 90056
		public virtual extern event HTMLStyleElementEvents2_onbeforedeactivateEventHandler HTMLStyleElementEvents2_Event_onbeforedeactivate;

		// Token: 0x14002A79 RID: 10873
		// (add) Token: 0x06015FC9 RID: 90057
		// (remove) Token: 0x06015FCA RID: 90058
		public virtual extern event HTMLStyleElementEvents2_onbeforeactivateEventHandler HTMLStyleElementEvents2_Event_onbeforeactivate;

		// Token: 0x14002A7A RID: 10874
		// (add) Token: 0x06015FCB RID: 90059
		// (remove) Token: 0x06015FCC RID: 90060
		public virtual extern event HTMLStyleElementEvents2_onfocusinEventHandler HTMLStyleElementEvents2_Event_onfocusin;

		// Token: 0x14002A7B RID: 10875
		// (add) Token: 0x06015FCD RID: 90061
		// (remove) Token: 0x06015FCE RID: 90062
		public virtual extern event HTMLStyleElementEvents2_onfocusoutEventHandler HTMLStyleElementEvents2_Event_onfocusout;

		// Token: 0x14002A7C RID: 10876
		// (add) Token: 0x06015FCF RID: 90063
		// (remove) Token: 0x06015FD0 RID: 90064
		public virtual extern event HTMLStyleElementEvents2_onmoveEventHandler HTMLStyleElementEvents2_Event_onmove;

		// Token: 0x14002A7D RID: 10877
		// (add) Token: 0x06015FD1 RID: 90065
		// (remove) Token: 0x06015FD2 RID: 90066
		public virtual extern event HTMLStyleElementEvents2_oncontrolselectEventHandler HTMLStyleElementEvents2_Event_oncontrolselect;

		// Token: 0x14002A7E RID: 10878
		// (add) Token: 0x06015FD3 RID: 90067
		// (remove) Token: 0x06015FD4 RID: 90068
		public virtual extern event HTMLStyleElementEvents2_onmovestartEventHandler HTMLStyleElementEvents2_Event_onmovestart;

		// Token: 0x14002A7F RID: 10879
		// (add) Token: 0x06015FD5 RID: 90069
		// (remove) Token: 0x06015FD6 RID: 90070
		public virtual extern event HTMLStyleElementEvents2_onmoveendEventHandler HTMLStyleElementEvents2_Event_onmoveend;

		// Token: 0x14002A80 RID: 10880
		// (add) Token: 0x06015FD7 RID: 90071
		// (remove) Token: 0x06015FD8 RID: 90072
		public virtual extern event HTMLStyleElementEvents2_onresizestartEventHandler HTMLStyleElementEvents2_Event_onresizestart;

		// Token: 0x14002A81 RID: 10881
		// (add) Token: 0x06015FD9 RID: 90073
		// (remove) Token: 0x06015FDA RID: 90074
		public virtual extern event HTMLStyleElementEvents2_onresizeendEventHandler HTMLStyleElementEvents2_Event_onresizeend;

		// Token: 0x14002A82 RID: 10882
		// (add) Token: 0x06015FDB RID: 90075
		// (remove) Token: 0x06015FDC RID: 90076
		public virtual extern event HTMLStyleElementEvents2_onmousewheelEventHandler HTMLStyleElementEvents2_Event_onmousewheel;

		// Token: 0x14002A83 RID: 10883
		// (add) Token: 0x06015FDD RID: 90077
		// (remove) Token: 0x06015FDE RID: 90078
		public virtual extern event HTMLStyleElementEvents2_onloadEventHandler HTMLStyleElementEvents2_Event_onload;

		// Token: 0x14002A84 RID: 10884
		// (add) Token: 0x06015FDF RID: 90079
		// (remove) Token: 0x06015FE0 RID: 90080
		public virtual extern event HTMLStyleElementEvents2_onerrorEventHandler HTMLStyleElementEvents2_Event_onerror;
	}
}
