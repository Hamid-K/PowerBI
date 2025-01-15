using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x020008D4 RID: 2260
	[TypeLibType(2)]
	[ComSourceInterfaces("mshtml.HTMLMapEvents\0mshtml.HTMLMapEvents2\0\0")]
	[ClassInterface(0)]
	[Guid("3050F271-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public class HTMLMapElementClass : DispHTMLMapElement, HTMLMapElement, HTMLMapEvents_Event, IHTMLElement, IHTMLElement2, IHTMLElement3, IHTMLElement4, IHTMLUniqueName, IHTMLDOMNode, IHTMLDOMNode2, IHTMLMapElement, HTMLMapEvents2_Event
	{
		// Token: 0x0600E73A RID: 59194
		[MethodImpl(4096, MethodCodeType = 3)]
		public extern HTMLMapElementClass();

		// Token: 0x0600E73B RID: 59195
		[DispId(-2147417611)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void setAttribute([MarshalAs(19)] [In] string strAttributeName, [MarshalAs(27)] [In] object AttributeValue, [In] int lFlags = 1);

		// Token: 0x0600E73C RID: 59196
		[DispId(-2147417610)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		public virtual extern object getAttribute([MarshalAs(19)] [In] string strAttributeName, [In] int lFlags = 0);

		// Token: 0x0600E73D RID: 59197
		[DispId(-2147417609)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool removeAttribute([MarshalAs(19)] [In] string strAttributeName, [In] int lFlags = 1);

		// Token: 0x17004C49 RID: 19529
		// (get) Token: 0x0600E73F RID: 59199
		// (set) Token: 0x0600E73E RID: 59198
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

		// Token: 0x17004C4A RID: 19530
		// (get) Token: 0x0600E741 RID: 59201
		// (set) Token: 0x0600E740 RID: 59200
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

		// Token: 0x17004C4B RID: 19531
		// (get) Token: 0x0600E742 RID: 59202
		[DispId(-2147417108)]
		public virtual extern string tagName
		{
			[DispId(-2147417108)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17004C4C RID: 19532
		// (get) Token: 0x0600E743 RID: 59203
		[DispId(-2147418104)]
		public virtual extern IHTMLElement parentElement
		{
			[DispId(-2147418104)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17004C4D RID: 19533
		// (get) Token: 0x0600E744 RID: 59204
		[DispId(-2147418038)]
		public virtual extern IHTMLStyle style
		{
			[TypeLibFunc(1024)]
			[DispId(-2147418038)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17004C4E RID: 19534
		// (get) Token: 0x0600E746 RID: 59206
		// (set) Token: 0x0600E745 RID: 59205
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

		// Token: 0x17004C4F RID: 19535
		// (get) Token: 0x0600E748 RID: 59208
		// (set) Token: 0x0600E747 RID: 59207
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

		// Token: 0x17004C50 RID: 19536
		// (get) Token: 0x0600E74A RID: 59210
		// (set) Token: 0x0600E749 RID: 59209
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

		// Token: 0x17004C51 RID: 19537
		// (get) Token: 0x0600E74C RID: 59212
		// (set) Token: 0x0600E74B RID: 59211
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

		// Token: 0x17004C52 RID: 19538
		// (get) Token: 0x0600E74E RID: 59214
		// (set) Token: 0x0600E74D RID: 59213
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

		// Token: 0x17004C53 RID: 19539
		// (get) Token: 0x0600E750 RID: 59216
		// (set) Token: 0x0600E74F RID: 59215
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

		// Token: 0x17004C54 RID: 19540
		// (get) Token: 0x0600E752 RID: 59218
		// (set) Token: 0x0600E751 RID: 59217
		[DispId(-2147412111)]
		public virtual extern object onmouseout
		{
			[DispId(-2147412111)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412111)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17004C55 RID: 19541
		// (get) Token: 0x0600E754 RID: 59220
		// (set) Token: 0x0600E753 RID: 59219
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

		// Token: 0x17004C56 RID: 19542
		// (get) Token: 0x0600E756 RID: 59222
		// (set) Token: 0x0600E755 RID: 59221
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

		// Token: 0x17004C57 RID: 19543
		// (get) Token: 0x0600E758 RID: 59224
		// (set) Token: 0x0600E757 RID: 59223
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

		// Token: 0x17004C58 RID: 19544
		// (get) Token: 0x0600E75A RID: 59226
		// (set) Token: 0x0600E759 RID: 59225
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

		// Token: 0x17004C59 RID: 19545
		// (get) Token: 0x0600E75B RID: 59227
		[DispId(-2147417094)]
		public virtual extern object document
		{
			[DispId(-2147417094)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17004C5A RID: 19546
		// (get) Token: 0x0600E75D RID: 59229
		// (set) Token: 0x0600E75C RID: 59228
		[DispId(-2147418043)]
		public virtual extern string title
		{
			[DispId(-2147418043)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147418043)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17004C5B RID: 19547
		// (get) Token: 0x0600E75F RID: 59231
		// (set) Token: 0x0600E75E RID: 59230
		[DispId(-2147413012)]
		public virtual extern string language
		{
			[TypeLibFunc(20)]
			[DispId(-2147413012)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413012)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17004C5C RID: 19548
		// (get) Token: 0x0600E761 RID: 59233
		// (set) Token: 0x0600E760 RID: 59232
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

		// Token: 0x0600E762 RID: 59234
		[DispId(-2147417093)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void scrollIntoView([MarshalAs(27)] [In] [Optional] object varargStart);

		// Token: 0x0600E763 RID: 59235
		[DispId(-2147417092)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool contains([MarshalAs(28)] [In] IHTMLElement pChild);

		// Token: 0x17004C5D RID: 19549
		// (get) Token: 0x0600E764 RID: 59236
		[DispId(-2147417088)]
		public virtual extern int sourceIndex
		{
			[DispId(-2147417088)]
			[TypeLibFunc(4)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17004C5E RID: 19550
		// (get) Token: 0x0600E765 RID: 59237
		[DispId(-2147417087)]
		public virtual extern object recordNumber
		{
			[DispId(-2147417087)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x17004C5F RID: 19551
		// (get) Token: 0x0600E767 RID: 59239
		// (set) Token: 0x0600E766 RID: 59238
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

		// Token: 0x17004C60 RID: 19552
		// (get) Token: 0x0600E768 RID: 59240
		[DispId(-2147417104)]
		public virtual extern int offsetLeft
		{
			[DispId(-2147417104)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17004C61 RID: 19553
		// (get) Token: 0x0600E769 RID: 59241
		[DispId(-2147417103)]
		public virtual extern int offsetTop
		{
			[DispId(-2147417103)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17004C62 RID: 19554
		// (get) Token: 0x0600E76A RID: 59242
		[DispId(-2147417102)]
		public virtual extern int offsetWidth
		{
			[DispId(-2147417102)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17004C63 RID: 19555
		// (get) Token: 0x0600E76B RID: 59243
		[DispId(-2147417101)]
		public virtual extern int offsetHeight
		{
			[DispId(-2147417101)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17004C64 RID: 19556
		// (get) Token: 0x0600E76C RID: 59244
		[DispId(-2147417100)]
		public virtual extern IHTMLElement offsetParent
		{
			[DispId(-2147417100)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17004C65 RID: 19557
		// (get) Token: 0x0600E76E RID: 59246
		// (set) Token: 0x0600E76D RID: 59245
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

		// Token: 0x17004C66 RID: 19558
		// (get) Token: 0x0600E770 RID: 59248
		// (set) Token: 0x0600E76F RID: 59247
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

		// Token: 0x17004C67 RID: 19559
		// (get) Token: 0x0600E772 RID: 59250
		// (set) Token: 0x0600E771 RID: 59249
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

		// Token: 0x17004C68 RID: 19560
		// (get) Token: 0x0600E774 RID: 59252
		// (set) Token: 0x0600E773 RID: 59251
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

		// Token: 0x0600E775 RID: 59253
		[DispId(-2147417082)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void insertAdjacentHTML([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string html);

		// Token: 0x0600E776 RID: 59254
		[DispId(-2147417081)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void insertAdjacentText([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string text);

		// Token: 0x17004C69 RID: 19561
		// (get) Token: 0x0600E777 RID: 59255
		[DispId(-2147417080)]
		public virtual extern IHTMLElement parentTextEdit
		{
			[DispId(-2147417080)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17004C6A RID: 19562
		// (get) Token: 0x0600E778 RID: 59256
		[DispId(-2147417078)]
		public virtual extern bool isTextEdit
		{
			[DispId(-2147417078)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x0600E779 RID: 59257
		[DispId(-2147417079)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void click();

		// Token: 0x17004C6B RID: 19563
		// (get) Token: 0x0600E77A RID: 59258
		[DispId(-2147417077)]
		public virtual extern IHTMLFiltersCollection filters
		{
			[DispId(-2147417077)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17004C6C RID: 19564
		// (get) Token: 0x0600E77C RID: 59260
		// (set) Token: 0x0600E77B RID: 59259
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

		// Token: 0x0600E77D RID: 59261
		[DispId(-2147417076)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string toString();

		// Token: 0x17004C6D RID: 19565
		// (get) Token: 0x0600E77F RID: 59263
		// (set) Token: 0x0600E77E RID: 59262
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

		// Token: 0x17004C6E RID: 19566
		// (get) Token: 0x0600E781 RID: 59265
		// (set) Token: 0x0600E780 RID: 59264
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

		// Token: 0x17004C6F RID: 19567
		// (get) Token: 0x0600E783 RID: 59267
		// (set) Token: 0x0600E782 RID: 59266
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

		// Token: 0x17004C70 RID: 19568
		// (get) Token: 0x0600E785 RID: 59269
		// (set) Token: 0x0600E784 RID: 59268
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

		// Token: 0x17004C71 RID: 19569
		// (get) Token: 0x0600E787 RID: 59271
		// (set) Token: 0x0600E786 RID: 59270
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

		// Token: 0x17004C72 RID: 19570
		// (get) Token: 0x0600E789 RID: 59273
		// (set) Token: 0x0600E788 RID: 59272
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

		// Token: 0x17004C73 RID: 19571
		// (get) Token: 0x0600E78B RID: 59275
		// (set) Token: 0x0600E78A RID: 59274
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

		// Token: 0x17004C74 RID: 19572
		// (get) Token: 0x0600E78D RID: 59277
		// (set) Token: 0x0600E78C RID: 59276
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

		// Token: 0x17004C75 RID: 19573
		// (get) Token: 0x0600E78F RID: 59279
		// (set) Token: 0x0600E78E RID: 59278
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

		// Token: 0x17004C76 RID: 19574
		// (get) Token: 0x0600E790 RID: 59280
		[DispId(-2147417075)]
		public virtual extern object children
		{
			[DispId(-2147417075)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17004C77 RID: 19575
		// (get) Token: 0x0600E791 RID: 59281
		[DispId(-2147417074)]
		public virtual extern object all
		{
			[DispId(-2147417074)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17004C78 RID: 19576
		// (get) Token: 0x0600E792 RID: 59282
		[DispId(-2147417073)]
		public virtual extern string scopeName
		{
			[DispId(-2147417073)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x0600E793 RID: 59283
		[DispId(-2147417072)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void setCapture([In] bool containerCapture = true);

		// Token: 0x0600E794 RID: 59284
		[DispId(-2147417071)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void releaseCapture();

		// Token: 0x17004C79 RID: 19577
		// (get) Token: 0x0600E796 RID: 59286
		// (set) Token: 0x0600E795 RID: 59285
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

		// Token: 0x0600E797 RID: 59287
		[DispId(-2147417070)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string componentFromPoint([In] int x, [In] int y);

		// Token: 0x0600E798 RID: 59288
		[DispId(-2147417069)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void doScroll([MarshalAs(27)] [In] [Optional] object component);

		// Token: 0x17004C7A RID: 19578
		// (get) Token: 0x0600E79A RID: 59290
		// (set) Token: 0x0600E799 RID: 59289
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

		// Token: 0x17004C7B RID: 19579
		// (get) Token: 0x0600E79C RID: 59292
		// (set) Token: 0x0600E79B RID: 59291
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

		// Token: 0x17004C7C RID: 19580
		// (get) Token: 0x0600E79E RID: 59294
		// (set) Token: 0x0600E79D RID: 59293
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

		// Token: 0x17004C7D RID: 19581
		// (get) Token: 0x0600E7A0 RID: 59296
		// (set) Token: 0x0600E79F RID: 59295
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

		// Token: 0x17004C7E RID: 19582
		// (get) Token: 0x0600E7A2 RID: 59298
		// (set) Token: 0x0600E7A1 RID: 59297
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

		// Token: 0x17004C7F RID: 19583
		// (get) Token: 0x0600E7A4 RID: 59300
		// (set) Token: 0x0600E7A3 RID: 59299
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

		// Token: 0x17004C80 RID: 19584
		// (get) Token: 0x0600E7A6 RID: 59302
		// (set) Token: 0x0600E7A5 RID: 59301
		[DispId(-2147412058)]
		public virtual extern object ondrop
		{
			[DispId(-2147412058)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412058)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17004C81 RID: 19585
		// (get) Token: 0x0600E7A8 RID: 59304
		// (set) Token: 0x0600E7A7 RID: 59303
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

		// Token: 0x17004C82 RID: 19586
		// (get) Token: 0x0600E7AA RID: 59306
		// (set) Token: 0x0600E7A9 RID: 59305
		[DispId(-2147412057)]
		public virtual extern object oncut
		{
			[TypeLibFunc(20)]
			[DispId(-2147412057)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412057)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17004C83 RID: 19587
		// (get) Token: 0x0600E7AC RID: 59308
		// (set) Token: 0x0600E7AB RID: 59307
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

		// Token: 0x17004C84 RID: 19588
		// (get) Token: 0x0600E7AE RID: 59310
		// (set) Token: 0x0600E7AD RID: 59309
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

		// Token: 0x17004C85 RID: 19589
		// (get) Token: 0x0600E7B0 RID: 59312
		// (set) Token: 0x0600E7AF RID: 59311
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

		// Token: 0x17004C86 RID: 19590
		// (get) Token: 0x0600E7B2 RID: 59314
		// (set) Token: 0x0600E7B1 RID: 59313
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

		// Token: 0x17004C87 RID: 19591
		// (get) Token: 0x0600E7B3 RID: 59315
		[DispId(-2147417105)]
		public virtual extern IHTMLCurrentStyle currentStyle
		{
			[TypeLibFunc(1024)]
			[DispId(-2147417105)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17004C88 RID: 19592
		// (get) Token: 0x0600E7B5 RID: 59317
		// (set) Token: 0x0600E7B4 RID: 59316
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

		// Token: 0x0600E7B6 RID: 59318
		[DispId(-2147417068)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLRectCollection getClientRects();

		// Token: 0x0600E7B7 RID: 59319
		[DispId(-2147417067)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLRect getBoundingClientRect();

		// Token: 0x0600E7B8 RID: 59320
		[DispId(-2147417608)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void setExpression([MarshalAs(19)] [In] string propname, [MarshalAs(19)] [In] string expression, [MarshalAs(19)] [In] string language = "");

		// Token: 0x0600E7B9 RID: 59321
		[DispId(-2147417607)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		public virtual extern object getExpression([MarshalAs(19)] [In] string propname);

		// Token: 0x0600E7BA RID: 59322
		[DispId(-2147417606)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool removeExpression([MarshalAs(19)] [In] string propname);

		// Token: 0x17004C89 RID: 19593
		// (get) Token: 0x0600E7BC RID: 59324
		// (set) Token: 0x0600E7BB RID: 59323
		[DispId(-2147418097)]
		public virtual extern short tabIndex
		{
			[TypeLibFunc(20)]
			[DispId(-2147418097)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[DispId(-2147418097)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x0600E7BD RID: 59325
		[DispId(-2147416112)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void focus();

		// Token: 0x17004C8A RID: 19594
		// (get) Token: 0x0600E7BF RID: 59327
		// (set) Token: 0x0600E7BE RID: 59326
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

		// Token: 0x17004C8B RID: 19595
		// (get) Token: 0x0600E7C1 RID: 59329
		// (set) Token: 0x0600E7C0 RID: 59328
		[DispId(-2147412097)]
		public virtual extern object onblur
		{
			[TypeLibFunc(20)]
			[DispId(-2147412097)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412097)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17004C8C RID: 19596
		// (get) Token: 0x0600E7C3 RID: 59331
		// (set) Token: 0x0600E7C2 RID: 59330
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

		// Token: 0x17004C8D RID: 19597
		// (get) Token: 0x0600E7C5 RID: 59333
		// (set) Token: 0x0600E7C4 RID: 59332
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

		// Token: 0x0600E7C6 RID: 59334
		[DispId(-2147416110)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void blur();

		// Token: 0x0600E7C7 RID: 59335
		[DispId(-2147416095)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void addFilter([MarshalAs(25)] [In] object pUnk);

		// Token: 0x0600E7C8 RID: 59336
		[DispId(-2147416094)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void removeFilter([MarshalAs(25)] [In] object pUnk);

		// Token: 0x17004C8E RID: 19598
		// (get) Token: 0x0600E7C9 RID: 59337
		[DispId(-2147416093)]
		public virtual extern int clientHeight
		{
			[DispId(-2147416093)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17004C8F RID: 19599
		// (get) Token: 0x0600E7CA RID: 59338
		[DispId(-2147416092)]
		public virtual extern int clientWidth
		{
			[DispId(-2147416092)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17004C90 RID: 19600
		// (get) Token: 0x0600E7CB RID: 59339
		[DispId(-2147416091)]
		public virtual extern int clientTop
		{
			[TypeLibFunc(20)]
			[DispId(-2147416091)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17004C91 RID: 19601
		// (get) Token: 0x0600E7CC RID: 59340
		[DispId(-2147416090)]
		public virtual extern int clientLeft
		{
			[DispId(-2147416090)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x0600E7CD RID: 59341
		[DispId(-2147417605)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool attachEvent([MarshalAs(19)] [In] string @event, [MarshalAs(26)] [In] object pdisp);

		// Token: 0x0600E7CE RID: 59342
		[DispId(-2147417604)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void detachEvent([MarshalAs(19)] [In] string @event, [MarshalAs(26)] [In] object pdisp);

		// Token: 0x17004C92 RID: 19602
		// (get) Token: 0x0600E7CF RID: 59343
		[DispId(-2147412996)]
		public virtual extern object readyState
		{
			[DispId(-2147412996)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x17004C93 RID: 19603
		// (get) Token: 0x0600E7D1 RID: 59345
		// (set) Token: 0x0600E7D0 RID: 59344
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

		// Token: 0x17004C94 RID: 19604
		// (get) Token: 0x0600E7D3 RID: 59347
		// (set) Token: 0x0600E7D2 RID: 59346
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

		// Token: 0x17004C95 RID: 19605
		// (get) Token: 0x0600E7D5 RID: 59349
		// (set) Token: 0x0600E7D4 RID: 59348
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

		// Token: 0x17004C96 RID: 19606
		// (get) Token: 0x0600E7D7 RID: 59351
		// (set) Token: 0x0600E7D6 RID: 59350
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

		// Token: 0x17004C97 RID: 19607
		// (get) Token: 0x0600E7D9 RID: 59353
		// (set) Token: 0x0600E7D8 RID: 59352
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

		// Token: 0x0600E7DA RID: 59354
		[DispId(-2147417056)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(26)]
		public virtual extern object createControlRange();

		// Token: 0x17004C98 RID: 19608
		// (get) Token: 0x0600E7DB RID: 59355
		[DispId(-2147417055)]
		public virtual extern int scrollHeight
		{
			[DispId(-2147417055)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17004C99 RID: 19609
		// (get) Token: 0x0600E7DC RID: 59356
		[DispId(-2147417054)]
		public virtual extern int scrollWidth
		{
			[DispId(-2147417054)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17004C9A RID: 19610
		// (get) Token: 0x0600E7DE RID: 59358
		// (set) Token: 0x0600E7DD RID: 59357
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

		// Token: 0x17004C9B RID: 19611
		// (get) Token: 0x0600E7E0 RID: 59360
		// (set) Token: 0x0600E7DF RID: 59359
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

		// Token: 0x0600E7E1 RID: 59361
		[DispId(-2147417050)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void clearAttributes();

		// Token: 0x17004C9C RID: 19612
		// (get) Token: 0x0600E7E3 RID: 59363
		// (set) Token: 0x0600E7E2 RID: 59362
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

		// Token: 0x0600E7E4 RID: 59364
		[DispId(-2147417043)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElement insertAdjacentElement([MarshalAs(19)] [In] string where, [MarshalAs(28)] [In] IHTMLElement insertedElement);

		// Token: 0x0600E7E5 RID: 59365
		[DispId(-2147417047)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElement applyElement([MarshalAs(28)] [In] IHTMLElement apply, [MarshalAs(19)] [In] string where);

		// Token: 0x0600E7E6 RID: 59366
		[DispId(-2147417042)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string getAdjacentText([MarshalAs(19)] [In] string where);

		// Token: 0x0600E7E7 RID: 59367
		[DispId(-2147417041)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string replaceAdjacentText([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string newText);

		// Token: 0x17004C9D RID: 19613
		// (get) Token: 0x0600E7E8 RID: 59368
		[DispId(-2147417040)]
		public virtual extern bool canHaveChildren
		{
			[DispId(-2147417040)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x0600E7E9 RID: 59369
		[DispId(-2147417032)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern int addBehavior([MarshalAs(19)] [In] string bstrUrl, [MarshalAs(27)] [In] [Optional] ref object pvarFactory);

		// Token: 0x0600E7EA RID: 59370
		[DispId(-2147417031)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool removeBehavior([In] int cookie);

		// Token: 0x17004C9E RID: 19614
		// (get) Token: 0x0600E7EB RID: 59371
		[DispId(-2147417048)]
		public virtual extern IHTMLStyle runtimeStyle
		{
			[DispId(-2147417048)]
			[TypeLibFunc(1024)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17004C9F RID: 19615
		// (get) Token: 0x0600E7EC RID: 59372
		[DispId(-2147417030)]
		public virtual extern object behaviorUrns
		{
			[DispId(-2147417030)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17004CA0 RID: 19616
		// (get) Token: 0x0600E7EE RID: 59374
		// (set) Token: 0x0600E7ED RID: 59373
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

		// Token: 0x17004CA1 RID: 19617
		// (get) Token: 0x0600E7F0 RID: 59376
		// (set) Token: 0x0600E7EF RID: 59375
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

		// Token: 0x17004CA2 RID: 19618
		// (get) Token: 0x0600E7F1 RID: 59377
		[DispId(-2147417028)]
		public virtual extern int readyStateValue
		{
			[DispId(-2147417028)]
			[TypeLibFunc(65)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x0600E7F2 RID: 59378
		[DispId(-2147417027)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElementCollection getElementsByTagName([MarshalAs(19)] [In] string v);

		// Token: 0x0600E7F3 RID: 59379
		[DispId(-2147417016)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void mergeAttributes([MarshalAs(28)] [In] IHTMLElement mergeThis, [MarshalAs(27)] [In] [Optional] ref object pvarFlags);

		// Token: 0x17004CA3 RID: 19619
		// (get) Token: 0x0600E7F4 RID: 59380
		[DispId(-2147417015)]
		public virtual extern bool isMultiLine
		{
			[DispId(-2147417015)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17004CA4 RID: 19620
		// (get) Token: 0x0600E7F5 RID: 59381
		[DispId(-2147417014)]
		public virtual extern bool canHaveHTML
		{
			[DispId(-2147417014)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17004CA5 RID: 19621
		// (get) Token: 0x0600E7F7 RID: 59383
		// (set) Token: 0x0600E7F6 RID: 59382
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

		// Token: 0x17004CA6 RID: 19622
		// (get) Token: 0x0600E7F9 RID: 59385
		// (set) Token: 0x0600E7F8 RID: 59384
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

		// Token: 0x17004CA7 RID: 19623
		// (get) Token: 0x0600E7FB RID: 59387
		// (set) Token: 0x0600E7FA RID: 59386
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

		// Token: 0x17004CA8 RID: 19624
		// (get) Token: 0x0600E7FD RID: 59389
		// (set) Token: 0x0600E7FC RID: 59388
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

		// Token: 0x0600E7FE RID: 59390
		[DispId(-2147417011)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void setActive();

		// Token: 0x17004CA9 RID: 19625
		// (get) Token: 0x0600E800 RID: 59392
		// (set) Token: 0x0600E7FF RID: 59391
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

		// Token: 0x17004CAA RID: 19626
		// (get) Token: 0x0600E801 RID: 59393
		[DispId(-2147417010)]
		public virtual extern bool isContentEditable
		{
			[DispId(-2147417010)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17004CAB RID: 19627
		// (get) Token: 0x0600E803 RID: 59395
		// (set) Token: 0x0600E802 RID: 59394
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

		// Token: 0x17004CAC RID: 19628
		// (get) Token: 0x0600E805 RID: 59397
		// (set) Token: 0x0600E804 RID: 59396
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

		// Token: 0x17004CAD RID: 19629
		// (get) Token: 0x0600E806 RID: 59398
		[DispId(-2147417007)]
		public virtual extern bool isDisabled
		{
			[DispId(-2147417007)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17004CAE RID: 19630
		// (get) Token: 0x0600E808 RID: 59400
		// (set) Token: 0x0600E807 RID: 59399
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

		// Token: 0x17004CAF RID: 19631
		// (get) Token: 0x0600E80A RID: 59402
		// (set) Token: 0x0600E809 RID: 59401
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

		// Token: 0x0600E80B RID: 59403
		[DispId(-2147417006)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool FireEvent([MarshalAs(19)] [In] string bstrEventName, [MarshalAs(27)] [In] [Optional] ref object pvarEventObject);

		// Token: 0x17004CB0 RID: 19632
		// (get) Token: 0x0600E80D RID: 59405
		// (set) Token: 0x0600E80C RID: 59404
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

		// Token: 0x17004CB1 RID: 19633
		// (get) Token: 0x0600E80F RID: 59407
		// (set) Token: 0x0600E80E RID: 59406
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

		// Token: 0x17004CB2 RID: 19634
		// (get) Token: 0x0600E811 RID: 59409
		// (set) Token: 0x0600E810 RID: 59408
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

		// Token: 0x17004CB3 RID: 19635
		// (get) Token: 0x0600E813 RID: 59411
		// (set) Token: 0x0600E812 RID: 59410
		[DispId(-2147412030)]
		public virtual extern object onmoveend
		{
			[DispId(-2147412030)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412030)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17004CB4 RID: 19636
		// (get) Token: 0x0600E815 RID: 59413
		// (set) Token: 0x0600E814 RID: 59412
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

		// Token: 0x17004CB5 RID: 19637
		// (get) Token: 0x0600E817 RID: 59415
		// (set) Token: 0x0600E816 RID: 59414
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

		// Token: 0x17004CB6 RID: 19638
		// (get) Token: 0x0600E819 RID: 59417
		// (set) Token: 0x0600E818 RID: 59416
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

		// Token: 0x17004CB7 RID: 19639
		// (get) Token: 0x0600E81B RID: 59419
		// (set) Token: 0x0600E81A RID: 59418
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

		// Token: 0x0600E81C RID: 59420
		[DispId(-2147417005)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool dragDrop();

		// Token: 0x17004CB8 RID: 19640
		// (get) Token: 0x0600E81D RID: 59421
		[DispId(-2147417004)]
		public virtual extern int glyphMode
		{
			[TypeLibFunc(1089)]
			[DispId(-2147417004)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17004CB9 RID: 19641
		// (get) Token: 0x0600E81F RID: 59423
		// (set) Token: 0x0600E81E RID: 59422
		[DispId(-2147412036)]
		public virtual extern object onmousewheel
		{
			[DispId(-2147412036)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412036)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x0600E820 RID: 59424
		[DispId(-2147417000)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void normalize();

		// Token: 0x0600E821 RID: 59425
		[DispId(-2147417003)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute getAttributeNode([MarshalAs(19)] [In] string bstrName);

		// Token: 0x0600E822 RID: 59426
		[DispId(-2147417002)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute setAttributeNode([MarshalAs(28)] [In] IHTMLDOMAttribute pattr);

		// Token: 0x0600E823 RID: 59427
		[DispId(-2147417001)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute removeAttributeNode([MarshalAs(28)] [In] IHTMLDOMAttribute pattr);

		// Token: 0x17004CBA RID: 19642
		// (get) Token: 0x0600E825 RID: 59429
		// (set) Token: 0x0600E824 RID: 59428
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

		// Token: 0x17004CBB RID: 19643
		// (get) Token: 0x0600E827 RID: 59431
		// (set) Token: 0x0600E826 RID: 59430
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

		// Token: 0x17004CBC RID: 19644
		// (get) Token: 0x0600E829 RID: 59433
		// (set) Token: 0x0600E828 RID: 59432
		[DispId(-2147412020)]
		public virtual extern object onfocusout
		{
			[DispId(-2147412020)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412020)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17004CBD RID: 19645
		// (get) Token: 0x0600E82A RID: 59434
		[DispId(-2147417058)]
		public virtual extern int uniqueNumber
		{
			[DispId(-2147417058)]
			[TypeLibFunc(64)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17004CBE RID: 19646
		// (get) Token: 0x0600E82B RID: 59435
		[DispId(-2147417057)]
		public virtual extern string uniqueID
		{
			[DispId(-2147417057)]
			[TypeLibFunc(64)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17004CBF RID: 19647
		// (get) Token: 0x0600E82C RID: 59436
		[DispId(-2147417066)]
		public virtual extern int nodeType
		{
			[DispId(-2147417066)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17004CC0 RID: 19648
		// (get) Token: 0x0600E82D RID: 59437
		[DispId(-2147417065)]
		public virtual extern IHTMLDOMNode parentNode
		{
			[DispId(-2147417065)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x0600E82E RID: 59438
		[DispId(-2147417064)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool hasChildNodes();

		// Token: 0x17004CC1 RID: 19649
		// (get) Token: 0x0600E82F RID: 59439
		[DispId(-2147417063)]
		public virtual extern object childNodes
		{
			[DispId(-2147417063)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17004CC2 RID: 19650
		// (get) Token: 0x0600E830 RID: 59440
		[DispId(-2147417062)]
		public virtual extern object attributes
		{
			[DispId(-2147417062)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x0600E831 RID: 59441
		[DispId(-2147417061)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode insertBefore([MarshalAs(28)] [In] IHTMLDOMNode newChild, [MarshalAs(27)] [In] [Optional] object refChild);

		// Token: 0x0600E832 RID: 59442
		[DispId(-2147417060)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode removeChild([MarshalAs(28)] [In] IHTMLDOMNode oldChild);

		// Token: 0x0600E833 RID: 59443
		[DispId(-2147417059)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode replaceChild([MarshalAs(28)] [In] IHTMLDOMNode newChild, [MarshalAs(28)] [In] IHTMLDOMNode oldChild);

		// Token: 0x0600E834 RID: 59444
		[DispId(-2147417051)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode cloneNode([In] bool fDeep);

		// Token: 0x0600E835 RID: 59445
		[DispId(-2147417046)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode removeNode([In] bool fDeep = false);

		// Token: 0x0600E836 RID: 59446
		[DispId(-2147417044)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode swapNode([MarshalAs(28)] [In] IHTMLDOMNode otherNode);

		// Token: 0x0600E837 RID: 59447
		[DispId(-2147417045)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode replaceNode([MarshalAs(28)] [In] IHTMLDOMNode replacement);

		// Token: 0x0600E838 RID: 59448
		[DispId(-2147417039)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode appendChild([MarshalAs(28)] [In] IHTMLDOMNode newChild);

		// Token: 0x17004CC3 RID: 19651
		// (get) Token: 0x0600E839 RID: 59449
		[DispId(-2147417038)]
		public virtual extern string nodeName
		{
			[DispId(-2147417038)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17004CC4 RID: 19652
		// (get) Token: 0x0600E83B RID: 59451
		// (set) Token: 0x0600E83A RID: 59450
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

		// Token: 0x17004CC5 RID: 19653
		// (get) Token: 0x0600E83C RID: 59452
		[DispId(-2147417036)]
		public virtual extern IHTMLDOMNode firstChild
		{
			[DispId(-2147417036)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17004CC6 RID: 19654
		// (get) Token: 0x0600E83D RID: 59453
		[DispId(-2147417035)]
		public virtual extern IHTMLDOMNode lastChild
		{
			[DispId(-2147417035)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17004CC7 RID: 19655
		// (get) Token: 0x0600E83E RID: 59454
		[DispId(-2147417034)]
		public virtual extern IHTMLDOMNode previousSibling
		{
			[DispId(-2147417034)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17004CC8 RID: 19656
		// (get) Token: 0x0600E83F RID: 59455
		[DispId(-2147417033)]
		public virtual extern IHTMLDOMNode nextSibling
		{
			[DispId(-2147417033)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17004CC9 RID: 19657
		// (get) Token: 0x0600E840 RID: 59456
		[DispId(-2147416999)]
		public virtual extern object ownerDocument
		{
			[DispId(-2147416999)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17004CCA RID: 19658
		// (get) Token: 0x0600E841 RID: 59457
		[DispId(1002)]
		public virtual extern IHTMLAreasCollection areas
		{
			[DispId(1002)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17004CCB RID: 19659
		// (get) Token: 0x0600E843 RID: 59459
		// (set) Token: 0x0600E842 RID: 59458
		[DispId(-2147418112)]
		public virtual extern string name
		{
			[DispId(-2147418112)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147418112)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x0600E844 RID: 59460
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement_setAttribute([MarshalAs(19)] [In] string strAttributeName, [MarshalAs(27)] [In] object AttributeValue, [In] int lFlags = 1);

		// Token: 0x0600E845 RID: 59461
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		public virtual extern object IHTMLElement_getAttribute([MarshalAs(19)] [In] string strAttributeName, [In] int lFlags = 0);

		// Token: 0x0600E846 RID: 59462
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement_removeAttribute([MarshalAs(19)] [In] string strAttributeName, [In] int lFlags = 1);

		// Token: 0x17004CCC RID: 19660
		// (get) Token: 0x0600E848 RID: 59464
		// (set) Token: 0x0600E847 RID: 59463
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

		// Token: 0x17004CCD RID: 19661
		// (get) Token: 0x0600E84A RID: 59466
		// (set) Token: 0x0600E849 RID: 59465
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

		// Token: 0x17004CCE RID: 19662
		// (get) Token: 0x0600E84B RID: 59467
		public virtual extern string IHTMLElement_tagName
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17004CCF RID: 19663
		// (get) Token: 0x0600E84C RID: 59468
		public virtual extern IHTMLElement IHTMLElement_parentElement
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17004CD0 RID: 19664
		// (get) Token: 0x0600E84D RID: 59469
		public virtual extern IHTMLStyle IHTMLElement_style
		{
			[TypeLibFunc(1024)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17004CD1 RID: 19665
		// (get) Token: 0x0600E84F RID: 59471
		// (set) Token: 0x0600E84E RID: 59470
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

		// Token: 0x17004CD2 RID: 19666
		// (get) Token: 0x0600E851 RID: 59473
		// (set) Token: 0x0600E850 RID: 59472
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

		// Token: 0x17004CD3 RID: 19667
		// (get) Token: 0x0600E853 RID: 59475
		// (set) Token: 0x0600E852 RID: 59474
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

		// Token: 0x17004CD4 RID: 19668
		// (get) Token: 0x0600E855 RID: 59477
		// (set) Token: 0x0600E854 RID: 59476
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

		// Token: 0x17004CD5 RID: 19669
		// (get) Token: 0x0600E857 RID: 59479
		// (set) Token: 0x0600E856 RID: 59478
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

		// Token: 0x17004CD6 RID: 19670
		// (get) Token: 0x0600E859 RID: 59481
		// (set) Token: 0x0600E858 RID: 59480
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

		// Token: 0x17004CD7 RID: 19671
		// (get) Token: 0x0600E85B RID: 59483
		// (set) Token: 0x0600E85A RID: 59482
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

		// Token: 0x17004CD8 RID: 19672
		// (get) Token: 0x0600E85D RID: 59485
		// (set) Token: 0x0600E85C RID: 59484
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

		// Token: 0x17004CD9 RID: 19673
		// (get) Token: 0x0600E85F RID: 59487
		// (set) Token: 0x0600E85E RID: 59486
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

		// Token: 0x17004CDA RID: 19674
		// (get) Token: 0x0600E861 RID: 59489
		// (set) Token: 0x0600E860 RID: 59488
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

		// Token: 0x17004CDB RID: 19675
		// (get) Token: 0x0600E863 RID: 59491
		// (set) Token: 0x0600E862 RID: 59490
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

		// Token: 0x17004CDC RID: 19676
		// (get) Token: 0x0600E864 RID: 59492
		public virtual extern object IHTMLElement_document
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17004CDD RID: 19677
		// (get) Token: 0x0600E866 RID: 59494
		// (set) Token: 0x0600E865 RID: 59493
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

		// Token: 0x17004CDE RID: 19678
		// (get) Token: 0x0600E868 RID: 59496
		// (set) Token: 0x0600E867 RID: 59495
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

		// Token: 0x17004CDF RID: 19679
		// (get) Token: 0x0600E86A RID: 59498
		// (set) Token: 0x0600E869 RID: 59497
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

		// Token: 0x0600E86B RID: 59499
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement_scrollIntoView([MarshalAs(27)] [In] [Optional] object varargStart);

		// Token: 0x0600E86C RID: 59500
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement_contains([MarshalAs(28)] [In] IHTMLElement pChild);

		// Token: 0x17004CE0 RID: 19680
		// (get) Token: 0x0600E86D RID: 59501
		public virtual extern int IHTMLElement_sourceIndex
		{
			[TypeLibFunc(4)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17004CE1 RID: 19681
		// (get) Token: 0x0600E86E RID: 59502
		public virtual extern object IHTMLElement_recordNumber
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x17004CE2 RID: 19682
		// (get) Token: 0x0600E870 RID: 59504
		// (set) Token: 0x0600E86F RID: 59503
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

		// Token: 0x17004CE3 RID: 19683
		// (get) Token: 0x0600E871 RID: 59505
		public virtual extern int IHTMLElement_offsetLeft
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17004CE4 RID: 19684
		// (get) Token: 0x0600E872 RID: 59506
		public virtual extern int IHTMLElement_offsetTop
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17004CE5 RID: 19685
		// (get) Token: 0x0600E873 RID: 59507
		public virtual extern int IHTMLElement_offsetWidth
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17004CE6 RID: 19686
		// (get) Token: 0x0600E874 RID: 59508
		public virtual extern int IHTMLElement_offsetHeight
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17004CE7 RID: 19687
		// (get) Token: 0x0600E875 RID: 59509
		public virtual extern IHTMLElement IHTMLElement_offsetParent
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17004CE8 RID: 19688
		// (get) Token: 0x0600E877 RID: 59511
		// (set) Token: 0x0600E876 RID: 59510
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

		// Token: 0x17004CE9 RID: 19689
		// (get) Token: 0x0600E879 RID: 59513
		// (set) Token: 0x0600E878 RID: 59512
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

		// Token: 0x17004CEA RID: 19690
		// (get) Token: 0x0600E87B RID: 59515
		// (set) Token: 0x0600E87A RID: 59514
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

		// Token: 0x17004CEB RID: 19691
		// (get) Token: 0x0600E87D RID: 59517
		// (set) Token: 0x0600E87C RID: 59516
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

		// Token: 0x0600E87E RID: 59518
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement_insertAdjacentHTML([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string html);

		// Token: 0x0600E87F RID: 59519
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement_insertAdjacentText([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string text);

		// Token: 0x17004CEC RID: 19692
		// (get) Token: 0x0600E880 RID: 59520
		public virtual extern IHTMLElement IHTMLElement_parentTextEdit
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17004CED RID: 19693
		// (get) Token: 0x0600E881 RID: 59521
		public virtual extern bool IHTMLElement_isTextEdit
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x0600E882 RID: 59522
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement_click();

		// Token: 0x17004CEE RID: 19694
		// (get) Token: 0x0600E883 RID: 59523
		public virtual extern IHTMLFiltersCollection IHTMLElement_filters
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17004CEF RID: 19695
		// (get) Token: 0x0600E885 RID: 59525
		// (set) Token: 0x0600E884 RID: 59524
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

		// Token: 0x0600E886 RID: 59526
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string IHTMLElement_toString();

		// Token: 0x17004CF0 RID: 19696
		// (get) Token: 0x0600E888 RID: 59528
		// (set) Token: 0x0600E887 RID: 59527
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

		// Token: 0x17004CF1 RID: 19697
		// (get) Token: 0x0600E88A RID: 59530
		// (set) Token: 0x0600E889 RID: 59529
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

		// Token: 0x17004CF2 RID: 19698
		// (get) Token: 0x0600E88C RID: 59532
		// (set) Token: 0x0600E88B RID: 59531
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

		// Token: 0x17004CF3 RID: 19699
		// (get) Token: 0x0600E88E RID: 59534
		// (set) Token: 0x0600E88D RID: 59533
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

		// Token: 0x17004CF4 RID: 19700
		// (get) Token: 0x0600E890 RID: 59536
		// (set) Token: 0x0600E88F RID: 59535
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

		// Token: 0x17004CF5 RID: 19701
		// (get) Token: 0x0600E892 RID: 59538
		// (set) Token: 0x0600E891 RID: 59537
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

		// Token: 0x17004CF6 RID: 19702
		// (get) Token: 0x0600E894 RID: 59540
		// (set) Token: 0x0600E893 RID: 59539
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

		// Token: 0x17004CF7 RID: 19703
		// (get) Token: 0x0600E896 RID: 59542
		// (set) Token: 0x0600E895 RID: 59541
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

		// Token: 0x17004CF8 RID: 19704
		// (get) Token: 0x0600E898 RID: 59544
		// (set) Token: 0x0600E897 RID: 59543
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

		// Token: 0x17004CF9 RID: 19705
		// (get) Token: 0x0600E899 RID: 59545
		public virtual extern object IHTMLElement_children
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17004CFA RID: 19706
		// (get) Token: 0x0600E89A RID: 59546
		public virtual extern object IHTMLElement_all
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17004CFB RID: 19707
		// (get) Token: 0x0600E89B RID: 59547
		public virtual extern string IHTMLElement2_scopeName
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x0600E89C RID: 59548
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_setCapture([In] bool containerCapture = true);

		// Token: 0x0600E89D RID: 59549
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_releaseCapture();

		// Token: 0x17004CFC RID: 19708
		// (get) Token: 0x0600E89F RID: 59551
		// (set) Token: 0x0600E89E RID: 59550
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

		// Token: 0x0600E8A0 RID: 59552
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string IHTMLElement2_componentFromPoint([In] int x, [In] int y);

		// Token: 0x0600E8A1 RID: 59553
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_doScroll([MarshalAs(27)] [In] [Optional] object component);

		// Token: 0x17004CFD RID: 19709
		// (get) Token: 0x0600E8A3 RID: 59555
		// (set) Token: 0x0600E8A2 RID: 59554
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

		// Token: 0x17004CFE RID: 19710
		// (get) Token: 0x0600E8A5 RID: 59557
		// (set) Token: 0x0600E8A4 RID: 59556
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

		// Token: 0x17004CFF RID: 19711
		// (get) Token: 0x0600E8A7 RID: 59559
		// (set) Token: 0x0600E8A6 RID: 59558
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

		// Token: 0x17004D00 RID: 19712
		// (get) Token: 0x0600E8A9 RID: 59561
		// (set) Token: 0x0600E8A8 RID: 59560
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

		// Token: 0x17004D01 RID: 19713
		// (get) Token: 0x0600E8AB RID: 59563
		// (set) Token: 0x0600E8AA RID: 59562
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

		// Token: 0x17004D02 RID: 19714
		// (get) Token: 0x0600E8AD RID: 59565
		// (set) Token: 0x0600E8AC RID: 59564
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

		// Token: 0x17004D03 RID: 19715
		// (get) Token: 0x0600E8AF RID: 59567
		// (set) Token: 0x0600E8AE RID: 59566
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

		// Token: 0x17004D04 RID: 19716
		// (get) Token: 0x0600E8B1 RID: 59569
		// (set) Token: 0x0600E8B0 RID: 59568
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

		// Token: 0x17004D05 RID: 19717
		// (get) Token: 0x0600E8B3 RID: 59571
		// (set) Token: 0x0600E8B2 RID: 59570
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

		// Token: 0x17004D06 RID: 19718
		// (get) Token: 0x0600E8B5 RID: 59573
		// (set) Token: 0x0600E8B4 RID: 59572
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

		// Token: 0x17004D07 RID: 19719
		// (get) Token: 0x0600E8B7 RID: 59575
		// (set) Token: 0x0600E8B6 RID: 59574
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

		// Token: 0x17004D08 RID: 19720
		// (get) Token: 0x0600E8B9 RID: 59577
		// (set) Token: 0x0600E8B8 RID: 59576
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

		// Token: 0x17004D09 RID: 19721
		// (get) Token: 0x0600E8BB RID: 59579
		// (set) Token: 0x0600E8BA RID: 59578
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

		// Token: 0x17004D0A RID: 19722
		// (get) Token: 0x0600E8BC RID: 59580
		public virtual extern IHTMLCurrentStyle IHTMLElement2_currentStyle
		{
			[TypeLibFunc(1024)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17004D0B RID: 19723
		// (get) Token: 0x0600E8BE RID: 59582
		// (set) Token: 0x0600E8BD RID: 59581
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

		// Token: 0x0600E8BF RID: 59583
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLRectCollection IHTMLElement2_getClientRects();

		// Token: 0x0600E8C0 RID: 59584
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLRect IHTMLElement2_getBoundingClientRect();

		// Token: 0x0600E8C1 RID: 59585
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_setExpression([MarshalAs(19)] [In] string propname, [MarshalAs(19)] [In] string expression, [MarshalAs(19)] [In] string language = "");

		// Token: 0x0600E8C2 RID: 59586
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		public virtual extern object IHTMLElement2_getExpression([MarshalAs(19)] [In] string propname);

		// Token: 0x0600E8C3 RID: 59587
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement2_removeExpression([MarshalAs(19)] [In] string propname);

		// Token: 0x17004D0C RID: 19724
		// (get) Token: 0x0600E8C5 RID: 59589
		// (set) Token: 0x0600E8C4 RID: 59588
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

		// Token: 0x0600E8C6 RID: 59590
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_focus();

		// Token: 0x17004D0D RID: 19725
		// (get) Token: 0x0600E8C8 RID: 59592
		// (set) Token: 0x0600E8C7 RID: 59591
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

		// Token: 0x17004D0E RID: 19726
		// (get) Token: 0x0600E8CA RID: 59594
		// (set) Token: 0x0600E8C9 RID: 59593
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

		// Token: 0x17004D0F RID: 19727
		// (get) Token: 0x0600E8CC RID: 59596
		// (set) Token: 0x0600E8CB RID: 59595
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

		// Token: 0x17004D10 RID: 19728
		// (get) Token: 0x0600E8CE RID: 59598
		// (set) Token: 0x0600E8CD RID: 59597
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

		// Token: 0x0600E8CF RID: 59599
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_blur();

		// Token: 0x0600E8D0 RID: 59600
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_addFilter([MarshalAs(25)] [In] object pUnk);

		// Token: 0x0600E8D1 RID: 59601
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_removeFilter([MarshalAs(25)] [In] object pUnk);

		// Token: 0x17004D11 RID: 19729
		// (get) Token: 0x0600E8D2 RID: 59602
		public virtual extern int IHTMLElement2_clientHeight
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17004D12 RID: 19730
		// (get) Token: 0x0600E8D3 RID: 59603
		public virtual extern int IHTMLElement2_clientWidth
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17004D13 RID: 19731
		// (get) Token: 0x0600E8D4 RID: 59604
		public virtual extern int IHTMLElement2_clientTop
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17004D14 RID: 19732
		// (get) Token: 0x0600E8D5 RID: 59605
		public virtual extern int IHTMLElement2_clientLeft
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x0600E8D6 RID: 59606
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement2_attachEvent([MarshalAs(19)] [In] string @event, [MarshalAs(26)] [In] object pdisp);

		// Token: 0x0600E8D7 RID: 59607
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_detachEvent([MarshalAs(19)] [In] string @event, [MarshalAs(26)] [In] object pdisp);

		// Token: 0x17004D15 RID: 19733
		// (get) Token: 0x0600E8D8 RID: 59608
		public virtual extern object IHTMLElement2_readyState
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x17004D16 RID: 19734
		// (get) Token: 0x0600E8DA RID: 59610
		// (set) Token: 0x0600E8D9 RID: 59609
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

		// Token: 0x17004D17 RID: 19735
		// (get) Token: 0x0600E8DC RID: 59612
		// (set) Token: 0x0600E8DB RID: 59611
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

		// Token: 0x17004D18 RID: 19736
		// (get) Token: 0x0600E8DE RID: 59614
		// (set) Token: 0x0600E8DD RID: 59613
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

		// Token: 0x17004D19 RID: 19737
		// (get) Token: 0x0600E8E0 RID: 59616
		// (set) Token: 0x0600E8DF RID: 59615
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

		// Token: 0x17004D1A RID: 19738
		// (get) Token: 0x0600E8E2 RID: 59618
		// (set) Token: 0x0600E8E1 RID: 59617
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

		// Token: 0x0600E8E3 RID: 59619
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(26)]
		public virtual extern object IHTMLElement2_createControlRange();

		// Token: 0x17004D1B RID: 19739
		// (get) Token: 0x0600E8E4 RID: 59620
		public virtual extern int IHTMLElement2_scrollHeight
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17004D1C RID: 19740
		// (get) Token: 0x0600E8E5 RID: 59621
		public virtual extern int IHTMLElement2_scrollWidth
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17004D1D RID: 19741
		// (get) Token: 0x0600E8E7 RID: 59623
		// (set) Token: 0x0600E8E6 RID: 59622
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

		// Token: 0x17004D1E RID: 19742
		// (get) Token: 0x0600E8E9 RID: 59625
		// (set) Token: 0x0600E8E8 RID: 59624
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

		// Token: 0x0600E8EA RID: 59626
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_clearAttributes();

		// Token: 0x0600E8EB RID: 59627
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void mergeAttributes([MarshalAs(28)] [In] IHTMLElement mergeThis);

		// Token: 0x17004D1F RID: 19743
		// (get) Token: 0x0600E8ED RID: 59629
		// (set) Token: 0x0600E8EC RID: 59628
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

		// Token: 0x0600E8EE RID: 59630
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElement IHTMLElement2_insertAdjacentElement([MarshalAs(19)] [In] string where, [MarshalAs(28)] [In] IHTMLElement insertedElement);

		// Token: 0x0600E8EF RID: 59631
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElement IHTMLElement2_applyElement([MarshalAs(28)] [In] IHTMLElement apply, [MarshalAs(19)] [In] string where);

		// Token: 0x0600E8F0 RID: 59632
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string IHTMLElement2_getAdjacentText([MarshalAs(19)] [In] string where);

		// Token: 0x0600E8F1 RID: 59633
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string IHTMLElement2_replaceAdjacentText([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string newText);

		// Token: 0x17004D20 RID: 19744
		// (get) Token: 0x0600E8F2 RID: 59634
		public virtual extern bool IHTMLElement2_canHaveChildren
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x0600E8F3 RID: 59635
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern int IHTMLElement2_addBehavior([MarshalAs(19)] [In] string bstrUrl, [MarshalAs(27)] [In] [Optional] ref object pvarFactory);

		// Token: 0x0600E8F4 RID: 59636
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement2_removeBehavior([In] int cookie);

		// Token: 0x17004D21 RID: 19745
		// (get) Token: 0x0600E8F5 RID: 59637
		public virtual extern IHTMLStyle IHTMLElement2_runtimeStyle
		{
			[TypeLibFunc(1024)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17004D22 RID: 19746
		// (get) Token: 0x0600E8F6 RID: 59638
		public virtual extern object IHTMLElement2_behaviorUrns
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17004D23 RID: 19747
		// (get) Token: 0x0600E8F8 RID: 59640
		// (set) Token: 0x0600E8F7 RID: 59639
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

		// Token: 0x17004D24 RID: 19748
		// (get) Token: 0x0600E8FA RID: 59642
		// (set) Token: 0x0600E8F9 RID: 59641
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

		// Token: 0x17004D25 RID: 19749
		// (get) Token: 0x0600E8FB RID: 59643
		public virtual extern int IHTMLElement2_readyStateValue
		{
			[TypeLibFunc(65)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x0600E8FC RID: 59644
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElementCollection IHTMLElement2_getElementsByTagName([MarshalAs(19)] [In] string v);

		// Token: 0x0600E8FD RID: 59645
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void mergeAttributes([MarshalAs(28)] [In] IHTMLElement mergeThis, [MarshalAs(27)] [In] [Optional] ref object pvarFlags);

		// Token: 0x17004D26 RID: 19750
		// (get) Token: 0x0600E8FE RID: 59646
		public virtual extern bool IHTMLElement3_isMultiLine
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17004D27 RID: 19751
		// (get) Token: 0x0600E8FF RID: 59647
		public virtual extern bool IHTMLElement3_canHaveHTML
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17004D28 RID: 19752
		// (get) Token: 0x0600E901 RID: 59649
		// (set) Token: 0x0600E900 RID: 59648
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

		// Token: 0x17004D29 RID: 19753
		// (get) Token: 0x0600E903 RID: 59651
		// (set) Token: 0x0600E902 RID: 59650
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

		// Token: 0x17004D2A RID: 19754
		// (get) Token: 0x0600E905 RID: 59653
		// (set) Token: 0x0600E904 RID: 59652
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

		// Token: 0x17004D2B RID: 19755
		// (get) Token: 0x0600E907 RID: 59655
		// (set) Token: 0x0600E906 RID: 59654
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

		// Token: 0x0600E908 RID: 59656
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement3_setActive();

		// Token: 0x17004D2C RID: 19756
		// (get) Token: 0x0600E90A RID: 59658
		// (set) Token: 0x0600E909 RID: 59657
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

		// Token: 0x17004D2D RID: 19757
		// (get) Token: 0x0600E90B RID: 59659
		public virtual extern bool IHTMLElement3_isContentEditable
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17004D2E RID: 19758
		// (get) Token: 0x0600E90D RID: 59661
		// (set) Token: 0x0600E90C RID: 59660
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

		// Token: 0x17004D2F RID: 19759
		// (get) Token: 0x0600E90F RID: 59663
		// (set) Token: 0x0600E90E RID: 59662
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

		// Token: 0x17004D30 RID: 19760
		// (get) Token: 0x0600E910 RID: 59664
		public virtual extern bool IHTMLElement3_isDisabled
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17004D31 RID: 19761
		// (get) Token: 0x0600E912 RID: 59666
		// (set) Token: 0x0600E911 RID: 59665
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

		// Token: 0x17004D32 RID: 19762
		// (get) Token: 0x0600E914 RID: 59668
		// (set) Token: 0x0600E913 RID: 59667
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

		// Token: 0x0600E915 RID: 59669
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement3_FireEvent([MarshalAs(19)] [In] string bstrEventName, [MarshalAs(27)] [In] [Optional] ref object pvarEventObject);

		// Token: 0x17004D33 RID: 19763
		// (get) Token: 0x0600E917 RID: 59671
		// (set) Token: 0x0600E916 RID: 59670
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

		// Token: 0x17004D34 RID: 19764
		// (get) Token: 0x0600E919 RID: 59673
		// (set) Token: 0x0600E918 RID: 59672
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

		// Token: 0x17004D35 RID: 19765
		// (get) Token: 0x0600E91B RID: 59675
		// (set) Token: 0x0600E91A RID: 59674
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

		// Token: 0x17004D36 RID: 19766
		// (get) Token: 0x0600E91D RID: 59677
		// (set) Token: 0x0600E91C RID: 59676
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

		// Token: 0x17004D37 RID: 19767
		// (get) Token: 0x0600E91F RID: 59679
		// (set) Token: 0x0600E91E RID: 59678
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

		// Token: 0x17004D38 RID: 19768
		// (get) Token: 0x0600E921 RID: 59681
		// (set) Token: 0x0600E920 RID: 59680
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

		// Token: 0x17004D39 RID: 19769
		// (get) Token: 0x0600E923 RID: 59683
		// (set) Token: 0x0600E922 RID: 59682
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

		// Token: 0x17004D3A RID: 19770
		// (get) Token: 0x0600E925 RID: 59685
		// (set) Token: 0x0600E924 RID: 59684
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

		// Token: 0x0600E926 RID: 59686
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement3_dragDrop();

		// Token: 0x17004D3B RID: 19771
		// (get) Token: 0x0600E927 RID: 59687
		public virtual extern int IHTMLElement3_glyphMode
		{
			[TypeLibFunc(1089)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17004D3C RID: 19772
		// (get) Token: 0x0600E929 RID: 59689
		// (set) Token: 0x0600E928 RID: 59688
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

		// Token: 0x0600E92A RID: 59690
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement4_normalize();

		// Token: 0x0600E92B RID: 59691
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute IHTMLElement4_getAttributeNode([MarshalAs(19)] [In] string bstrName);

		// Token: 0x0600E92C RID: 59692
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute IHTMLElement4_setAttributeNode([MarshalAs(28)] [In] IHTMLDOMAttribute pattr);

		// Token: 0x0600E92D RID: 59693
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute IHTMLElement4_removeAttributeNode([MarshalAs(28)] [In] IHTMLDOMAttribute pattr);

		// Token: 0x17004D3D RID: 19773
		// (get) Token: 0x0600E92F RID: 59695
		// (set) Token: 0x0600E92E RID: 59694
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

		// Token: 0x17004D3E RID: 19774
		// (get) Token: 0x0600E931 RID: 59697
		// (set) Token: 0x0600E930 RID: 59696
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

		// Token: 0x17004D3F RID: 19775
		// (get) Token: 0x0600E933 RID: 59699
		// (set) Token: 0x0600E932 RID: 59698
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

		// Token: 0x17004D40 RID: 19776
		// (get) Token: 0x0600E934 RID: 59700
		public virtual extern int IHTMLUniqueName_uniqueNumber
		{
			[TypeLibFunc(64)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17004D41 RID: 19777
		// (get) Token: 0x0600E935 RID: 59701
		public virtual extern string IHTMLUniqueName_uniqueID
		{
			[TypeLibFunc(64)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17004D42 RID: 19778
		// (get) Token: 0x0600E936 RID: 59702
		public virtual extern int IHTMLDOMNode_nodeType
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17004D43 RID: 19779
		// (get) Token: 0x0600E937 RID: 59703
		public virtual extern IHTMLDOMNode IHTMLDOMNode_parentNode
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x0600E938 RID: 59704
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLDOMNode_hasChildNodes();

		// Token: 0x17004D44 RID: 19780
		// (get) Token: 0x0600E939 RID: 59705
		public virtual extern object IHTMLDOMNode_childNodes
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17004D45 RID: 19781
		// (get) Token: 0x0600E93A RID: 59706
		public virtual extern object IHTMLDOMNode_attributes
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x0600E93B RID: 59707
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_insertBefore([MarshalAs(28)] [In] IHTMLDOMNode newChild, [MarshalAs(27)] [In] [Optional] object refChild);

		// Token: 0x0600E93C RID: 59708
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_removeChild([MarshalAs(28)] [In] IHTMLDOMNode oldChild);

		// Token: 0x0600E93D RID: 59709
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_replaceChild([MarshalAs(28)] [In] IHTMLDOMNode newChild, [MarshalAs(28)] [In] IHTMLDOMNode oldChild);

		// Token: 0x0600E93E RID: 59710
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_cloneNode([In] bool fDeep);

		// Token: 0x0600E93F RID: 59711
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_removeNode([In] bool fDeep = false);

		// Token: 0x0600E940 RID: 59712
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_swapNode([MarshalAs(28)] [In] IHTMLDOMNode otherNode);

		// Token: 0x0600E941 RID: 59713
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_replaceNode([MarshalAs(28)] [In] IHTMLDOMNode replacement);

		// Token: 0x0600E942 RID: 59714
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_appendChild([MarshalAs(28)] [In] IHTMLDOMNode newChild);

		// Token: 0x17004D46 RID: 19782
		// (get) Token: 0x0600E943 RID: 59715
		public virtual extern string IHTMLDOMNode_nodeName
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17004D47 RID: 19783
		// (get) Token: 0x0600E945 RID: 59717
		// (set) Token: 0x0600E944 RID: 59716
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

		// Token: 0x17004D48 RID: 19784
		// (get) Token: 0x0600E946 RID: 59718
		public virtual extern IHTMLDOMNode IHTMLDOMNode_firstChild
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17004D49 RID: 19785
		// (get) Token: 0x0600E947 RID: 59719
		public virtual extern IHTMLDOMNode IHTMLDOMNode_lastChild
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17004D4A RID: 19786
		// (get) Token: 0x0600E948 RID: 59720
		public virtual extern IHTMLDOMNode IHTMLDOMNode_previousSibling
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17004D4B RID: 19787
		// (get) Token: 0x0600E949 RID: 59721
		public virtual extern IHTMLDOMNode IHTMLDOMNode_nextSibling
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17004D4C RID: 19788
		// (get) Token: 0x0600E94A RID: 59722
		public virtual extern object IHTMLDOMNode2_ownerDocument
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17004D4D RID: 19789
		// (get) Token: 0x0600E94B RID: 59723
		public virtual extern IHTMLAreasCollection IHTMLMapElement_areas
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17004D4E RID: 19790
		// (get) Token: 0x0600E94D RID: 59725
		// (set) Token: 0x0600E94C RID: 59724
		public virtual extern string IHTMLMapElement_name
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x14001C03 RID: 7171
		// (add) Token: 0x0600E94E RID: 59726
		// (remove) Token: 0x0600E94F RID: 59727
		public virtual extern event HTMLMapEvents_onhelpEventHandler HTMLMapEvents_Event_onhelp;

		// Token: 0x14001C04 RID: 7172
		// (add) Token: 0x0600E950 RID: 59728
		// (remove) Token: 0x0600E951 RID: 59729
		public virtual extern event HTMLMapEvents_onclickEventHandler HTMLMapEvents_Event_onclick;

		// Token: 0x14001C05 RID: 7173
		// (add) Token: 0x0600E952 RID: 59730
		// (remove) Token: 0x0600E953 RID: 59731
		public virtual extern event HTMLMapEvents_ondblclickEventHandler HTMLMapEvents_Event_ondblclick;

		// Token: 0x14001C06 RID: 7174
		// (add) Token: 0x0600E954 RID: 59732
		// (remove) Token: 0x0600E955 RID: 59733
		public virtual extern event HTMLMapEvents_onkeypressEventHandler HTMLMapEvents_Event_onkeypress;

		// Token: 0x14001C07 RID: 7175
		// (add) Token: 0x0600E956 RID: 59734
		// (remove) Token: 0x0600E957 RID: 59735
		public virtual extern event HTMLMapEvents_onkeydownEventHandler HTMLMapEvents_Event_onkeydown;

		// Token: 0x14001C08 RID: 7176
		// (add) Token: 0x0600E958 RID: 59736
		// (remove) Token: 0x0600E959 RID: 59737
		public virtual extern event HTMLMapEvents_onkeyupEventHandler HTMLMapEvents_Event_onkeyup;

		// Token: 0x14001C09 RID: 7177
		// (add) Token: 0x0600E95A RID: 59738
		// (remove) Token: 0x0600E95B RID: 59739
		public virtual extern event HTMLMapEvents_onmouseoutEventHandler HTMLMapEvents_Event_onmouseout;

		// Token: 0x14001C0A RID: 7178
		// (add) Token: 0x0600E95C RID: 59740
		// (remove) Token: 0x0600E95D RID: 59741
		public virtual extern event HTMLMapEvents_onmouseoverEventHandler HTMLMapEvents_Event_onmouseover;

		// Token: 0x14001C0B RID: 7179
		// (add) Token: 0x0600E95E RID: 59742
		// (remove) Token: 0x0600E95F RID: 59743
		public virtual extern event HTMLMapEvents_onmousemoveEventHandler HTMLMapEvents_Event_onmousemove;

		// Token: 0x14001C0C RID: 7180
		// (add) Token: 0x0600E960 RID: 59744
		// (remove) Token: 0x0600E961 RID: 59745
		public virtual extern event HTMLMapEvents_onmousedownEventHandler HTMLMapEvents_Event_onmousedown;

		// Token: 0x14001C0D RID: 7181
		// (add) Token: 0x0600E962 RID: 59746
		// (remove) Token: 0x0600E963 RID: 59747
		public virtual extern event HTMLMapEvents_onmouseupEventHandler HTMLMapEvents_Event_onmouseup;

		// Token: 0x14001C0E RID: 7182
		// (add) Token: 0x0600E964 RID: 59748
		// (remove) Token: 0x0600E965 RID: 59749
		public virtual extern event HTMLMapEvents_onselectstartEventHandler HTMLMapEvents_Event_onselectstart;

		// Token: 0x14001C0F RID: 7183
		// (add) Token: 0x0600E966 RID: 59750
		// (remove) Token: 0x0600E967 RID: 59751
		public virtual extern event HTMLMapEvents_onfilterchangeEventHandler HTMLMapEvents_Event_onfilterchange;

		// Token: 0x14001C10 RID: 7184
		// (add) Token: 0x0600E968 RID: 59752
		// (remove) Token: 0x0600E969 RID: 59753
		public virtual extern event HTMLMapEvents_ondragstartEventHandler HTMLMapEvents_Event_ondragstart;

		// Token: 0x14001C11 RID: 7185
		// (add) Token: 0x0600E96A RID: 59754
		// (remove) Token: 0x0600E96B RID: 59755
		public virtual extern event HTMLMapEvents_onbeforeupdateEventHandler HTMLMapEvents_Event_onbeforeupdate;

		// Token: 0x14001C12 RID: 7186
		// (add) Token: 0x0600E96C RID: 59756
		// (remove) Token: 0x0600E96D RID: 59757
		public virtual extern event HTMLMapEvents_onafterupdateEventHandler HTMLMapEvents_Event_onafterupdate;

		// Token: 0x14001C13 RID: 7187
		// (add) Token: 0x0600E96E RID: 59758
		// (remove) Token: 0x0600E96F RID: 59759
		public virtual extern event HTMLMapEvents_onerrorupdateEventHandler HTMLMapEvents_Event_onerrorupdate;

		// Token: 0x14001C14 RID: 7188
		// (add) Token: 0x0600E970 RID: 59760
		// (remove) Token: 0x0600E971 RID: 59761
		public virtual extern event HTMLMapEvents_onrowexitEventHandler HTMLMapEvents_Event_onrowexit;

		// Token: 0x14001C15 RID: 7189
		// (add) Token: 0x0600E972 RID: 59762
		// (remove) Token: 0x0600E973 RID: 59763
		public virtual extern event HTMLMapEvents_onrowenterEventHandler HTMLMapEvents_Event_onrowenter;

		// Token: 0x14001C16 RID: 7190
		// (add) Token: 0x0600E974 RID: 59764
		// (remove) Token: 0x0600E975 RID: 59765
		public virtual extern event HTMLMapEvents_ondatasetchangedEventHandler HTMLMapEvents_Event_ondatasetchanged;

		// Token: 0x14001C17 RID: 7191
		// (add) Token: 0x0600E976 RID: 59766
		// (remove) Token: 0x0600E977 RID: 59767
		public virtual extern event HTMLMapEvents_ondataavailableEventHandler HTMLMapEvents_Event_ondataavailable;

		// Token: 0x14001C18 RID: 7192
		// (add) Token: 0x0600E978 RID: 59768
		// (remove) Token: 0x0600E979 RID: 59769
		public virtual extern event HTMLMapEvents_ondatasetcompleteEventHandler HTMLMapEvents_Event_ondatasetcomplete;

		// Token: 0x14001C19 RID: 7193
		// (add) Token: 0x0600E97A RID: 59770
		// (remove) Token: 0x0600E97B RID: 59771
		public virtual extern event HTMLMapEvents_onlosecaptureEventHandler HTMLMapEvents_Event_onlosecapture;

		// Token: 0x14001C1A RID: 7194
		// (add) Token: 0x0600E97C RID: 59772
		// (remove) Token: 0x0600E97D RID: 59773
		public virtual extern event HTMLMapEvents_onpropertychangeEventHandler HTMLMapEvents_Event_onpropertychange;

		// Token: 0x14001C1B RID: 7195
		// (add) Token: 0x0600E97E RID: 59774
		// (remove) Token: 0x0600E97F RID: 59775
		public virtual extern event HTMLMapEvents_onscrollEventHandler HTMLMapEvents_Event_onscroll;

		// Token: 0x14001C1C RID: 7196
		// (add) Token: 0x0600E980 RID: 59776
		// (remove) Token: 0x0600E981 RID: 59777
		public virtual extern event HTMLMapEvents_onfocusEventHandler HTMLMapEvents_Event_onfocus;

		// Token: 0x14001C1D RID: 7197
		// (add) Token: 0x0600E982 RID: 59778
		// (remove) Token: 0x0600E983 RID: 59779
		public virtual extern event HTMLMapEvents_onblurEventHandler HTMLMapEvents_Event_onblur;

		// Token: 0x14001C1E RID: 7198
		// (add) Token: 0x0600E984 RID: 59780
		// (remove) Token: 0x0600E985 RID: 59781
		public virtual extern event HTMLMapEvents_onresizeEventHandler HTMLMapEvents_Event_onresize;

		// Token: 0x14001C1F RID: 7199
		// (add) Token: 0x0600E986 RID: 59782
		// (remove) Token: 0x0600E987 RID: 59783
		public virtual extern event HTMLMapEvents_ondragEventHandler HTMLMapEvents_Event_ondrag;

		// Token: 0x14001C20 RID: 7200
		// (add) Token: 0x0600E988 RID: 59784
		// (remove) Token: 0x0600E989 RID: 59785
		public virtual extern event HTMLMapEvents_ondragendEventHandler HTMLMapEvents_Event_ondragend;

		// Token: 0x14001C21 RID: 7201
		// (add) Token: 0x0600E98A RID: 59786
		// (remove) Token: 0x0600E98B RID: 59787
		public virtual extern event HTMLMapEvents_ondragenterEventHandler HTMLMapEvents_Event_ondragenter;

		// Token: 0x14001C22 RID: 7202
		// (add) Token: 0x0600E98C RID: 59788
		// (remove) Token: 0x0600E98D RID: 59789
		public virtual extern event HTMLMapEvents_ondragoverEventHandler HTMLMapEvents_Event_ondragover;

		// Token: 0x14001C23 RID: 7203
		// (add) Token: 0x0600E98E RID: 59790
		// (remove) Token: 0x0600E98F RID: 59791
		public virtual extern event HTMLMapEvents_ondragleaveEventHandler HTMLMapEvents_Event_ondragleave;

		// Token: 0x14001C24 RID: 7204
		// (add) Token: 0x0600E990 RID: 59792
		// (remove) Token: 0x0600E991 RID: 59793
		public virtual extern event HTMLMapEvents_ondropEventHandler HTMLMapEvents_Event_ondrop;

		// Token: 0x14001C25 RID: 7205
		// (add) Token: 0x0600E992 RID: 59794
		// (remove) Token: 0x0600E993 RID: 59795
		public virtual extern event HTMLMapEvents_onbeforecutEventHandler HTMLMapEvents_Event_onbeforecut;

		// Token: 0x14001C26 RID: 7206
		// (add) Token: 0x0600E994 RID: 59796
		// (remove) Token: 0x0600E995 RID: 59797
		public virtual extern event HTMLMapEvents_oncutEventHandler HTMLMapEvents_Event_oncut;

		// Token: 0x14001C27 RID: 7207
		// (add) Token: 0x0600E996 RID: 59798
		// (remove) Token: 0x0600E997 RID: 59799
		public virtual extern event HTMLMapEvents_onbeforecopyEventHandler HTMLMapEvents_Event_onbeforecopy;

		// Token: 0x14001C28 RID: 7208
		// (add) Token: 0x0600E998 RID: 59800
		// (remove) Token: 0x0600E999 RID: 59801
		public virtual extern event HTMLMapEvents_oncopyEventHandler HTMLMapEvents_Event_oncopy;

		// Token: 0x14001C29 RID: 7209
		// (add) Token: 0x0600E99A RID: 59802
		// (remove) Token: 0x0600E99B RID: 59803
		public virtual extern event HTMLMapEvents_onbeforepasteEventHandler HTMLMapEvents_Event_onbeforepaste;

		// Token: 0x14001C2A RID: 7210
		// (add) Token: 0x0600E99C RID: 59804
		// (remove) Token: 0x0600E99D RID: 59805
		public virtual extern event HTMLMapEvents_onpasteEventHandler HTMLMapEvents_Event_onpaste;

		// Token: 0x14001C2B RID: 7211
		// (add) Token: 0x0600E99E RID: 59806
		// (remove) Token: 0x0600E99F RID: 59807
		public virtual extern event HTMLMapEvents_oncontextmenuEventHandler HTMLMapEvents_Event_oncontextmenu;

		// Token: 0x14001C2C RID: 7212
		// (add) Token: 0x0600E9A0 RID: 59808
		// (remove) Token: 0x0600E9A1 RID: 59809
		public virtual extern event HTMLMapEvents_onrowsdeleteEventHandler HTMLMapEvents_Event_onrowsdelete;

		// Token: 0x14001C2D RID: 7213
		// (add) Token: 0x0600E9A2 RID: 59810
		// (remove) Token: 0x0600E9A3 RID: 59811
		public virtual extern event HTMLMapEvents_onrowsinsertedEventHandler HTMLMapEvents_Event_onrowsinserted;

		// Token: 0x14001C2E RID: 7214
		// (add) Token: 0x0600E9A4 RID: 59812
		// (remove) Token: 0x0600E9A5 RID: 59813
		public virtual extern event HTMLMapEvents_oncellchangeEventHandler HTMLMapEvents_Event_oncellchange;

		// Token: 0x14001C2F RID: 7215
		// (add) Token: 0x0600E9A6 RID: 59814
		// (remove) Token: 0x0600E9A7 RID: 59815
		public virtual extern event HTMLMapEvents_onreadystatechangeEventHandler HTMLMapEvents_Event_onreadystatechange;

		// Token: 0x14001C30 RID: 7216
		// (add) Token: 0x0600E9A8 RID: 59816
		// (remove) Token: 0x0600E9A9 RID: 59817
		public virtual extern event HTMLMapEvents_onbeforeeditfocusEventHandler HTMLMapEvents_Event_onbeforeeditfocus;

		// Token: 0x14001C31 RID: 7217
		// (add) Token: 0x0600E9AA RID: 59818
		// (remove) Token: 0x0600E9AB RID: 59819
		public virtual extern event HTMLMapEvents_onlayoutcompleteEventHandler HTMLMapEvents_Event_onlayoutcomplete;

		// Token: 0x14001C32 RID: 7218
		// (add) Token: 0x0600E9AC RID: 59820
		// (remove) Token: 0x0600E9AD RID: 59821
		public virtual extern event HTMLMapEvents_onpageEventHandler HTMLMapEvents_Event_onpage;

		// Token: 0x14001C33 RID: 7219
		// (add) Token: 0x0600E9AE RID: 59822
		// (remove) Token: 0x0600E9AF RID: 59823
		public virtual extern event HTMLMapEvents_onbeforedeactivateEventHandler HTMLMapEvents_Event_onbeforedeactivate;

		// Token: 0x14001C34 RID: 7220
		// (add) Token: 0x0600E9B0 RID: 59824
		// (remove) Token: 0x0600E9B1 RID: 59825
		public virtual extern event HTMLMapEvents_onbeforeactivateEventHandler HTMLMapEvents_Event_onbeforeactivate;

		// Token: 0x14001C35 RID: 7221
		// (add) Token: 0x0600E9B2 RID: 59826
		// (remove) Token: 0x0600E9B3 RID: 59827
		public virtual extern event HTMLMapEvents_onmoveEventHandler HTMLMapEvents_Event_onmove;

		// Token: 0x14001C36 RID: 7222
		// (add) Token: 0x0600E9B4 RID: 59828
		// (remove) Token: 0x0600E9B5 RID: 59829
		public virtual extern event HTMLMapEvents_oncontrolselectEventHandler HTMLMapEvents_Event_oncontrolselect;

		// Token: 0x14001C37 RID: 7223
		// (add) Token: 0x0600E9B6 RID: 59830
		// (remove) Token: 0x0600E9B7 RID: 59831
		public virtual extern event HTMLMapEvents_onmovestartEventHandler HTMLMapEvents_Event_onmovestart;

		// Token: 0x14001C38 RID: 7224
		// (add) Token: 0x0600E9B8 RID: 59832
		// (remove) Token: 0x0600E9B9 RID: 59833
		public virtual extern event HTMLMapEvents_onmoveendEventHandler HTMLMapEvents_Event_onmoveend;

		// Token: 0x14001C39 RID: 7225
		// (add) Token: 0x0600E9BA RID: 59834
		// (remove) Token: 0x0600E9BB RID: 59835
		public virtual extern event HTMLMapEvents_onresizestartEventHandler HTMLMapEvents_Event_onresizestart;

		// Token: 0x14001C3A RID: 7226
		// (add) Token: 0x0600E9BC RID: 59836
		// (remove) Token: 0x0600E9BD RID: 59837
		public virtual extern event HTMLMapEvents_onresizeendEventHandler HTMLMapEvents_Event_onresizeend;

		// Token: 0x14001C3B RID: 7227
		// (add) Token: 0x0600E9BE RID: 59838
		// (remove) Token: 0x0600E9BF RID: 59839
		public virtual extern event HTMLMapEvents_onmouseenterEventHandler HTMLMapEvents_Event_onmouseenter;

		// Token: 0x14001C3C RID: 7228
		// (add) Token: 0x0600E9C0 RID: 59840
		// (remove) Token: 0x0600E9C1 RID: 59841
		public virtual extern event HTMLMapEvents_onmouseleaveEventHandler HTMLMapEvents_Event_onmouseleave;

		// Token: 0x14001C3D RID: 7229
		// (add) Token: 0x0600E9C2 RID: 59842
		// (remove) Token: 0x0600E9C3 RID: 59843
		public virtual extern event HTMLMapEvents_onmousewheelEventHandler HTMLMapEvents_Event_onmousewheel;

		// Token: 0x14001C3E RID: 7230
		// (add) Token: 0x0600E9C4 RID: 59844
		// (remove) Token: 0x0600E9C5 RID: 59845
		public virtual extern event HTMLMapEvents_onactivateEventHandler HTMLMapEvents_Event_onactivate;

		// Token: 0x14001C3F RID: 7231
		// (add) Token: 0x0600E9C6 RID: 59846
		// (remove) Token: 0x0600E9C7 RID: 59847
		public virtual extern event HTMLMapEvents_ondeactivateEventHandler HTMLMapEvents_Event_ondeactivate;

		// Token: 0x14001C40 RID: 7232
		// (add) Token: 0x0600E9C8 RID: 59848
		// (remove) Token: 0x0600E9C9 RID: 59849
		public virtual extern event HTMLMapEvents_onfocusinEventHandler HTMLMapEvents_Event_onfocusin;

		// Token: 0x14001C41 RID: 7233
		// (add) Token: 0x0600E9CA RID: 59850
		// (remove) Token: 0x0600E9CB RID: 59851
		public virtual extern event HTMLMapEvents_onfocusoutEventHandler HTMLMapEvents_Event_onfocusout;

		// Token: 0x14001C42 RID: 7234
		// (add) Token: 0x0600E9CC RID: 59852
		// (remove) Token: 0x0600E9CD RID: 59853
		public virtual extern event HTMLMapEvents2_onhelpEventHandler HTMLMapEvents2_Event_onhelp;

		// Token: 0x14001C43 RID: 7235
		// (add) Token: 0x0600E9CE RID: 59854
		// (remove) Token: 0x0600E9CF RID: 59855
		public virtual extern event HTMLMapEvents2_onclickEventHandler HTMLMapEvents2_Event_onclick;

		// Token: 0x14001C44 RID: 7236
		// (add) Token: 0x0600E9D0 RID: 59856
		// (remove) Token: 0x0600E9D1 RID: 59857
		public virtual extern event HTMLMapEvents2_ondblclickEventHandler HTMLMapEvents2_Event_ondblclick;

		// Token: 0x14001C45 RID: 7237
		// (add) Token: 0x0600E9D2 RID: 59858
		// (remove) Token: 0x0600E9D3 RID: 59859
		public virtual extern event HTMLMapEvents2_onkeypressEventHandler HTMLMapEvents2_Event_onkeypress;

		// Token: 0x14001C46 RID: 7238
		// (add) Token: 0x0600E9D4 RID: 59860
		// (remove) Token: 0x0600E9D5 RID: 59861
		public virtual extern event HTMLMapEvents2_onkeydownEventHandler HTMLMapEvents2_Event_onkeydown;

		// Token: 0x14001C47 RID: 7239
		// (add) Token: 0x0600E9D6 RID: 59862
		// (remove) Token: 0x0600E9D7 RID: 59863
		public virtual extern event HTMLMapEvents2_onkeyupEventHandler HTMLMapEvents2_Event_onkeyup;

		// Token: 0x14001C48 RID: 7240
		// (add) Token: 0x0600E9D8 RID: 59864
		// (remove) Token: 0x0600E9D9 RID: 59865
		public virtual extern event HTMLMapEvents2_onmouseoutEventHandler HTMLMapEvents2_Event_onmouseout;

		// Token: 0x14001C49 RID: 7241
		// (add) Token: 0x0600E9DA RID: 59866
		// (remove) Token: 0x0600E9DB RID: 59867
		public virtual extern event HTMLMapEvents2_onmouseoverEventHandler HTMLMapEvents2_Event_onmouseover;

		// Token: 0x14001C4A RID: 7242
		// (add) Token: 0x0600E9DC RID: 59868
		// (remove) Token: 0x0600E9DD RID: 59869
		public virtual extern event HTMLMapEvents2_onmousemoveEventHandler HTMLMapEvents2_Event_onmousemove;

		// Token: 0x14001C4B RID: 7243
		// (add) Token: 0x0600E9DE RID: 59870
		// (remove) Token: 0x0600E9DF RID: 59871
		public virtual extern event HTMLMapEvents2_onmousedownEventHandler HTMLMapEvents2_Event_onmousedown;

		// Token: 0x14001C4C RID: 7244
		// (add) Token: 0x0600E9E0 RID: 59872
		// (remove) Token: 0x0600E9E1 RID: 59873
		public virtual extern event HTMLMapEvents2_onmouseupEventHandler HTMLMapEvents2_Event_onmouseup;

		// Token: 0x14001C4D RID: 7245
		// (add) Token: 0x0600E9E2 RID: 59874
		// (remove) Token: 0x0600E9E3 RID: 59875
		public virtual extern event HTMLMapEvents2_onselectstartEventHandler HTMLMapEvents2_Event_onselectstart;

		// Token: 0x14001C4E RID: 7246
		// (add) Token: 0x0600E9E4 RID: 59876
		// (remove) Token: 0x0600E9E5 RID: 59877
		public virtual extern event HTMLMapEvents2_onfilterchangeEventHandler HTMLMapEvents2_Event_onfilterchange;

		// Token: 0x14001C4F RID: 7247
		// (add) Token: 0x0600E9E6 RID: 59878
		// (remove) Token: 0x0600E9E7 RID: 59879
		public virtual extern event HTMLMapEvents2_ondragstartEventHandler HTMLMapEvents2_Event_ondragstart;

		// Token: 0x14001C50 RID: 7248
		// (add) Token: 0x0600E9E8 RID: 59880
		// (remove) Token: 0x0600E9E9 RID: 59881
		public virtual extern event HTMLMapEvents2_onbeforeupdateEventHandler HTMLMapEvents2_Event_onbeforeupdate;

		// Token: 0x14001C51 RID: 7249
		// (add) Token: 0x0600E9EA RID: 59882
		// (remove) Token: 0x0600E9EB RID: 59883
		public virtual extern event HTMLMapEvents2_onafterupdateEventHandler HTMLMapEvents2_Event_onafterupdate;

		// Token: 0x14001C52 RID: 7250
		// (add) Token: 0x0600E9EC RID: 59884
		// (remove) Token: 0x0600E9ED RID: 59885
		public virtual extern event HTMLMapEvents2_onerrorupdateEventHandler HTMLMapEvents2_Event_onerrorupdate;

		// Token: 0x14001C53 RID: 7251
		// (add) Token: 0x0600E9EE RID: 59886
		// (remove) Token: 0x0600E9EF RID: 59887
		public virtual extern event HTMLMapEvents2_onrowexitEventHandler HTMLMapEvents2_Event_onrowexit;

		// Token: 0x14001C54 RID: 7252
		// (add) Token: 0x0600E9F0 RID: 59888
		// (remove) Token: 0x0600E9F1 RID: 59889
		public virtual extern event HTMLMapEvents2_onrowenterEventHandler HTMLMapEvents2_Event_onrowenter;

		// Token: 0x14001C55 RID: 7253
		// (add) Token: 0x0600E9F2 RID: 59890
		// (remove) Token: 0x0600E9F3 RID: 59891
		public virtual extern event HTMLMapEvents2_ondatasetchangedEventHandler HTMLMapEvents2_Event_ondatasetchanged;

		// Token: 0x14001C56 RID: 7254
		// (add) Token: 0x0600E9F4 RID: 59892
		// (remove) Token: 0x0600E9F5 RID: 59893
		public virtual extern event HTMLMapEvents2_ondataavailableEventHandler HTMLMapEvents2_Event_ondataavailable;

		// Token: 0x14001C57 RID: 7255
		// (add) Token: 0x0600E9F6 RID: 59894
		// (remove) Token: 0x0600E9F7 RID: 59895
		public virtual extern event HTMLMapEvents2_ondatasetcompleteEventHandler HTMLMapEvents2_Event_ondatasetcomplete;

		// Token: 0x14001C58 RID: 7256
		// (add) Token: 0x0600E9F8 RID: 59896
		// (remove) Token: 0x0600E9F9 RID: 59897
		public virtual extern event HTMLMapEvents2_onlosecaptureEventHandler HTMLMapEvents2_Event_onlosecapture;

		// Token: 0x14001C59 RID: 7257
		// (add) Token: 0x0600E9FA RID: 59898
		// (remove) Token: 0x0600E9FB RID: 59899
		public virtual extern event HTMLMapEvents2_onpropertychangeEventHandler HTMLMapEvents2_Event_onpropertychange;

		// Token: 0x14001C5A RID: 7258
		// (add) Token: 0x0600E9FC RID: 59900
		// (remove) Token: 0x0600E9FD RID: 59901
		public virtual extern event HTMLMapEvents2_onscrollEventHandler HTMLMapEvents2_Event_onscroll;

		// Token: 0x14001C5B RID: 7259
		// (add) Token: 0x0600E9FE RID: 59902
		// (remove) Token: 0x0600E9FF RID: 59903
		public virtual extern event HTMLMapEvents2_onfocusEventHandler HTMLMapEvents2_Event_onfocus;

		// Token: 0x14001C5C RID: 7260
		// (add) Token: 0x0600EA00 RID: 59904
		// (remove) Token: 0x0600EA01 RID: 59905
		public virtual extern event HTMLMapEvents2_onblurEventHandler HTMLMapEvents2_Event_onblur;

		// Token: 0x14001C5D RID: 7261
		// (add) Token: 0x0600EA02 RID: 59906
		// (remove) Token: 0x0600EA03 RID: 59907
		public virtual extern event HTMLMapEvents2_onresizeEventHandler HTMLMapEvents2_Event_onresize;

		// Token: 0x14001C5E RID: 7262
		// (add) Token: 0x0600EA04 RID: 59908
		// (remove) Token: 0x0600EA05 RID: 59909
		public virtual extern event HTMLMapEvents2_ondragEventHandler HTMLMapEvents2_Event_ondrag;

		// Token: 0x14001C5F RID: 7263
		// (add) Token: 0x0600EA06 RID: 59910
		// (remove) Token: 0x0600EA07 RID: 59911
		public virtual extern event HTMLMapEvents2_ondragendEventHandler HTMLMapEvents2_Event_ondragend;

		// Token: 0x14001C60 RID: 7264
		// (add) Token: 0x0600EA08 RID: 59912
		// (remove) Token: 0x0600EA09 RID: 59913
		public virtual extern event HTMLMapEvents2_ondragenterEventHandler HTMLMapEvents2_Event_ondragenter;

		// Token: 0x14001C61 RID: 7265
		// (add) Token: 0x0600EA0A RID: 59914
		// (remove) Token: 0x0600EA0B RID: 59915
		public virtual extern event HTMLMapEvents2_ondragoverEventHandler HTMLMapEvents2_Event_ondragover;

		// Token: 0x14001C62 RID: 7266
		// (add) Token: 0x0600EA0C RID: 59916
		// (remove) Token: 0x0600EA0D RID: 59917
		public virtual extern event HTMLMapEvents2_ondragleaveEventHandler HTMLMapEvents2_Event_ondragleave;

		// Token: 0x14001C63 RID: 7267
		// (add) Token: 0x0600EA0E RID: 59918
		// (remove) Token: 0x0600EA0F RID: 59919
		public virtual extern event HTMLMapEvents2_ondropEventHandler HTMLMapEvents2_Event_ondrop;

		// Token: 0x14001C64 RID: 7268
		// (add) Token: 0x0600EA10 RID: 59920
		// (remove) Token: 0x0600EA11 RID: 59921
		public virtual extern event HTMLMapEvents2_onbeforecutEventHandler HTMLMapEvents2_Event_onbeforecut;

		// Token: 0x14001C65 RID: 7269
		// (add) Token: 0x0600EA12 RID: 59922
		// (remove) Token: 0x0600EA13 RID: 59923
		public virtual extern event HTMLMapEvents2_oncutEventHandler HTMLMapEvents2_Event_oncut;

		// Token: 0x14001C66 RID: 7270
		// (add) Token: 0x0600EA14 RID: 59924
		// (remove) Token: 0x0600EA15 RID: 59925
		public virtual extern event HTMLMapEvents2_onbeforecopyEventHandler HTMLMapEvents2_Event_onbeforecopy;

		// Token: 0x14001C67 RID: 7271
		// (add) Token: 0x0600EA16 RID: 59926
		// (remove) Token: 0x0600EA17 RID: 59927
		public virtual extern event HTMLMapEvents2_oncopyEventHandler HTMLMapEvents2_Event_oncopy;

		// Token: 0x14001C68 RID: 7272
		// (add) Token: 0x0600EA18 RID: 59928
		// (remove) Token: 0x0600EA19 RID: 59929
		public virtual extern event HTMLMapEvents2_onbeforepasteEventHandler HTMLMapEvents2_Event_onbeforepaste;

		// Token: 0x14001C69 RID: 7273
		// (add) Token: 0x0600EA1A RID: 59930
		// (remove) Token: 0x0600EA1B RID: 59931
		public virtual extern event HTMLMapEvents2_onpasteEventHandler HTMLMapEvents2_Event_onpaste;

		// Token: 0x14001C6A RID: 7274
		// (add) Token: 0x0600EA1C RID: 59932
		// (remove) Token: 0x0600EA1D RID: 59933
		public virtual extern event HTMLMapEvents2_oncontextmenuEventHandler HTMLMapEvents2_Event_oncontextmenu;

		// Token: 0x14001C6B RID: 7275
		// (add) Token: 0x0600EA1E RID: 59934
		// (remove) Token: 0x0600EA1F RID: 59935
		public virtual extern event HTMLMapEvents2_onrowsdeleteEventHandler HTMLMapEvents2_Event_onrowsdelete;

		// Token: 0x14001C6C RID: 7276
		// (add) Token: 0x0600EA20 RID: 59936
		// (remove) Token: 0x0600EA21 RID: 59937
		public virtual extern event HTMLMapEvents2_onrowsinsertedEventHandler HTMLMapEvents2_Event_onrowsinserted;

		// Token: 0x14001C6D RID: 7277
		// (add) Token: 0x0600EA22 RID: 59938
		// (remove) Token: 0x0600EA23 RID: 59939
		public virtual extern event HTMLMapEvents2_oncellchangeEventHandler HTMLMapEvents2_Event_oncellchange;

		// Token: 0x14001C6E RID: 7278
		// (add) Token: 0x0600EA24 RID: 59940
		// (remove) Token: 0x0600EA25 RID: 59941
		public virtual extern event HTMLMapEvents2_onreadystatechangeEventHandler HTMLMapEvents2_Event_onreadystatechange;

		// Token: 0x14001C6F RID: 7279
		// (add) Token: 0x0600EA26 RID: 59942
		// (remove) Token: 0x0600EA27 RID: 59943
		public virtual extern event HTMLMapEvents2_onlayoutcompleteEventHandler HTMLMapEvents2_Event_onlayoutcomplete;

		// Token: 0x14001C70 RID: 7280
		// (add) Token: 0x0600EA28 RID: 59944
		// (remove) Token: 0x0600EA29 RID: 59945
		public virtual extern event HTMLMapEvents2_onpageEventHandler HTMLMapEvents2_Event_onpage;

		// Token: 0x14001C71 RID: 7281
		// (add) Token: 0x0600EA2A RID: 59946
		// (remove) Token: 0x0600EA2B RID: 59947
		public virtual extern event HTMLMapEvents2_onmouseenterEventHandler HTMLMapEvents2_Event_onmouseenter;

		// Token: 0x14001C72 RID: 7282
		// (add) Token: 0x0600EA2C RID: 59948
		// (remove) Token: 0x0600EA2D RID: 59949
		public virtual extern event HTMLMapEvents2_onmouseleaveEventHandler HTMLMapEvents2_Event_onmouseleave;

		// Token: 0x14001C73 RID: 7283
		// (add) Token: 0x0600EA2E RID: 59950
		// (remove) Token: 0x0600EA2F RID: 59951
		public virtual extern event HTMLMapEvents2_onactivateEventHandler HTMLMapEvents2_Event_onactivate;

		// Token: 0x14001C74 RID: 7284
		// (add) Token: 0x0600EA30 RID: 59952
		// (remove) Token: 0x0600EA31 RID: 59953
		public virtual extern event HTMLMapEvents2_ondeactivateEventHandler HTMLMapEvents2_Event_ondeactivate;

		// Token: 0x14001C75 RID: 7285
		// (add) Token: 0x0600EA32 RID: 59954
		// (remove) Token: 0x0600EA33 RID: 59955
		public virtual extern event HTMLMapEvents2_onbeforedeactivateEventHandler HTMLMapEvents2_Event_onbeforedeactivate;

		// Token: 0x14001C76 RID: 7286
		// (add) Token: 0x0600EA34 RID: 59956
		// (remove) Token: 0x0600EA35 RID: 59957
		public virtual extern event HTMLMapEvents2_onbeforeactivateEventHandler HTMLMapEvents2_Event_onbeforeactivate;

		// Token: 0x14001C77 RID: 7287
		// (add) Token: 0x0600EA36 RID: 59958
		// (remove) Token: 0x0600EA37 RID: 59959
		public virtual extern event HTMLMapEvents2_onfocusinEventHandler HTMLMapEvents2_Event_onfocusin;

		// Token: 0x14001C78 RID: 7288
		// (add) Token: 0x0600EA38 RID: 59960
		// (remove) Token: 0x0600EA39 RID: 59961
		public virtual extern event HTMLMapEvents2_onfocusoutEventHandler HTMLMapEvents2_Event_onfocusout;

		// Token: 0x14001C79 RID: 7289
		// (add) Token: 0x0600EA3A RID: 59962
		// (remove) Token: 0x0600EA3B RID: 59963
		public virtual extern event HTMLMapEvents2_onmoveEventHandler HTMLMapEvents2_Event_onmove;

		// Token: 0x14001C7A RID: 7290
		// (add) Token: 0x0600EA3C RID: 59964
		// (remove) Token: 0x0600EA3D RID: 59965
		public virtual extern event HTMLMapEvents2_oncontrolselectEventHandler HTMLMapEvents2_Event_oncontrolselect;

		// Token: 0x14001C7B RID: 7291
		// (add) Token: 0x0600EA3E RID: 59966
		// (remove) Token: 0x0600EA3F RID: 59967
		public virtual extern event HTMLMapEvents2_onmovestartEventHandler HTMLMapEvents2_Event_onmovestart;

		// Token: 0x14001C7C RID: 7292
		// (add) Token: 0x0600EA40 RID: 59968
		// (remove) Token: 0x0600EA41 RID: 59969
		public virtual extern event HTMLMapEvents2_onmoveendEventHandler HTMLMapEvents2_Event_onmoveend;

		// Token: 0x14001C7D RID: 7293
		// (add) Token: 0x0600EA42 RID: 59970
		// (remove) Token: 0x0600EA43 RID: 59971
		public virtual extern event HTMLMapEvents2_onresizestartEventHandler HTMLMapEvents2_Event_onresizestart;

		// Token: 0x14001C7E RID: 7294
		// (add) Token: 0x0600EA44 RID: 59972
		// (remove) Token: 0x0600EA45 RID: 59973
		public virtual extern event HTMLMapEvents2_onresizeendEventHandler HTMLMapEvents2_Event_onresizeend;

		// Token: 0x14001C7F RID: 7295
		// (add) Token: 0x0600EA46 RID: 59974
		// (remove) Token: 0x0600EA47 RID: 59975
		public virtual extern event HTMLMapEvents2_onmousewheelEventHandler HTMLMapEvents2_Event_onmousewheel;
	}
}
