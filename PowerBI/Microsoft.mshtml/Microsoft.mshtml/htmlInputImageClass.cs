using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000D60 RID: 3424
	[ClassInterface(0)]
	[ComSourceInterfaces("mshtml.HTMLInputImageEvents\0\0")]
	[Guid("3050F2C4-98B5-11CF-BB82-00AA00BDCE0B")]
	[TypeLibType(2)]
	[ComImport]
	public class htmlInputImageClass : DispIHTMLInputImage, htmlInputImage, HTMLInputImageEvents_Event, IHTMLInputImage, IHTMLControlElement, IHTMLElement
	{
		// Token: 0x06017201 RID: 94721
		[MethodImpl(4096, MethodCodeType = 3)]
		public extern htmlInputImageClass();

		// Token: 0x06017202 RID: 94722
		[DispId(-2147417611)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void setAttribute([MarshalAs(19)] [In] string strAttributeName, [MarshalAs(27)] [In] object AttributeValue, [In] int lFlags = 1);

		// Token: 0x06017203 RID: 94723
		[DispId(-2147417610)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		public virtual extern object getAttribute([MarshalAs(19)] [In] string strAttributeName, [In] int lFlags = 0);

		// Token: 0x06017204 RID: 94724
		[DispId(-2147417609)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool removeAttribute([MarshalAs(19)] [In] string strAttributeName, [In] int lFlags = 1);

		// Token: 0x17007B46 RID: 31558
		// (get) Token: 0x06017206 RID: 94726
		// (set) Token: 0x06017205 RID: 94725
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

		// Token: 0x17007B47 RID: 31559
		// (get) Token: 0x06017208 RID: 94728
		// (set) Token: 0x06017207 RID: 94727
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

		// Token: 0x17007B48 RID: 31560
		// (get) Token: 0x06017209 RID: 94729
		[DispId(-2147417108)]
		public virtual extern string tagName
		{
			[DispId(-2147417108)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17007B49 RID: 31561
		// (get) Token: 0x0601720A RID: 94730
		[DispId(-2147418104)]
		public virtual extern IHTMLElement parentElement
		{
			[DispId(-2147418104)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17007B4A RID: 31562
		// (get) Token: 0x0601720B RID: 94731
		[DispId(-2147418038)]
		public virtual extern IHTMLStyle style
		{
			[DispId(-2147418038)]
			[TypeLibFunc(1024)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17007B4B RID: 31563
		// (get) Token: 0x0601720D RID: 94733
		// (set) Token: 0x0601720C RID: 94732
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

		// Token: 0x17007B4C RID: 31564
		// (get) Token: 0x0601720F RID: 94735
		// (set) Token: 0x0601720E RID: 94734
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

		// Token: 0x17007B4D RID: 31565
		// (get) Token: 0x06017211 RID: 94737
		// (set) Token: 0x06017210 RID: 94736
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

		// Token: 0x17007B4E RID: 31566
		// (get) Token: 0x06017213 RID: 94739
		// (set) Token: 0x06017212 RID: 94738
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

		// Token: 0x17007B4F RID: 31567
		// (get) Token: 0x06017215 RID: 94741
		// (set) Token: 0x06017214 RID: 94740
		[DispId(-2147412106)]
		public virtual extern object onkeyup
		{
			[DispId(-2147412106)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412106)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17007B50 RID: 31568
		// (get) Token: 0x06017217 RID: 94743
		// (set) Token: 0x06017216 RID: 94742
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

		// Token: 0x17007B51 RID: 31569
		// (get) Token: 0x06017219 RID: 94745
		// (set) Token: 0x06017218 RID: 94744
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

		// Token: 0x17007B52 RID: 31570
		// (get) Token: 0x0601721B RID: 94747
		// (set) Token: 0x0601721A RID: 94746
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

		// Token: 0x17007B53 RID: 31571
		// (get) Token: 0x0601721D RID: 94749
		// (set) Token: 0x0601721C RID: 94748
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

		// Token: 0x17007B54 RID: 31572
		// (get) Token: 0x0601721F RID: 94751
		// (set) Token: 0x0601721E RID: 94750
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

		// Token: 0x17007B55 RID: 31573
		// (get) Token: 0x06017221 RID: 94753
		// (set) Token: 0x06017220 RID: 94752
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

		// Token: 0x17007B56 RID: 31574
		// (get) Token: 0x06017222 RID: 94754
		[DispId(-2147417094)]
		public virtual extern object document
		{
			[DispId(-2147417094)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17007B57 RID: 31575
		// (get) Token: 0x06017224 RID: 94756
		// (set) Token: 0x06017223 RID: 94755
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

		// Token: 0x17007B58 RID: 31576
		// (get) Token: 0x06017226 RID: 94758
		// (set) Token: 0x06017225 RID: 94757
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

		// Token: 0x17007B59 RID: 31577
		// (get) Token: 0x06017228 RID: 94760
		// (set) Token: 0x06017227 RID: 94759
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

		// Token: 0x06017229 RID: 94761
		[DispId(-2147417093)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void scrollIntoView([MarshalAs(27)] [In] [Optional] object varargStart);

		// Token: 0x0601722A RID: 94762
		[DispId(-2147417092)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool contains([MarshalAs(28)] [In] IHTMLElement pChild);

		// Token: 0x17007B5A RID: 31578
		// (get) Token: 0x0601722B RID: 94763
		[DispId(-2147417088)]
		public virtual extern int sourceIndex
		{
			[DispId(-2147417088)]
			[TypeLibFunc(4)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17007B5B RID: 31579
		// (get) Token: 0x0601722C RID: 94764
		[DispId(-2147417087)]
		public virtual extern object recordNumber
		{
			[DispId(-2147417087)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x17007B5C RID: 31580
		// (get) Token: 0x0601722E RID: 94766
		// (set) Token: 0x0601722D RID: 94765
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

		// Token: 0x17007B5D RID: 31581
		// (get) Token: 0x0601722F RID: 94767
		[DispId(-2147417104)]
		public virtual extern int offsetLeft
		{
			[DispId(-2147417104)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17007B5E RID: 31582
		// (get) Token: 0x06017230 RID: 94768
		[DispId(-2147417103)]
		public virtual extern int offsetTop
		{
			[DispId(-2147417103)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17007B5F RID: 31583
		// (get) Token: 0x06017231 RID: 94769
		[DispId(-2147417102)]
		public virtual extern int offsetWidth
		{
			[DispId(-2147417102)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17007B60 RID: 31584
		// (get) Token: 0x06017232 RID: 94770
		[DispId(-2147417101)]
		public virtual extern int offsetHeight
		{
			[DispId(-2147417101)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17007B61 RID: 31585
		// (get) Token: 0x06017233 RID: 94771
		[DispId(-2147417100)]
		public virtual extern IHTMLElement offsetParent
		{
			[DispId(-2147417100)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17007B62 RID: 31586
		// (get) Token: 0x06017235 RID: 94773
		// (set) Token: 0x06017234 RID: 94772
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

		// Token: 0x17007B63 RID: 31587
		// (get) Token: 0x06017237 RID: 94775
		// (set) Token: 0x06017236 RID: 94774
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

		// Token: 0x17007B64 RID: 31588
		// (get) Token: 0x06017239 RID: 94777
		// (set) Token: 0x06017238 RID: 94776
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

		// Token: 0x17007B65 RID: 31589
		// (get) Token: 0x0601723B RID: 94779
		// (set) Token: 0x0601723A RID: 94778
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

		// Token: 0x0601723C RID: 94780
		[DispId(-2147417082)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void insertAdjacentHTML([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string html);

		// Token: 0x0601723D RID: 94781
		[DispId(-2147417081)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void insertAdjacentText([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string text);

		// Token: 0x17007B66 RID: 31590
		// (get) Token: 0x0601723E RID: 94782
		[DispId(-2147417080)]
		public virtual extern IHTMLElement parentTextEdit
		{
			[DispId(-2147417080)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17007B67 RID: 31591
		// (get) Token: 0x0601723F RID: 94783
		[DispId(-2147417078)]
		public virtual extern bool isTextEdit
		{
			[DispId(-2147417078)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x06017240 RID: 94784
		[DispId(-2147417079)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void click();

		// Token: 0x17007B68 RID: 31592
		// (get) Token: 0x06017241 RID: 94785
		[DispId(-2147417077)]
		public virtual extern IHTMLFiltersCollection filters
		{
			[DispId(-2147417077)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17007B69 RID: 31593
		// (get) Token: 0x06017243 RID: 94787
		// (set) Token: 0x06017242 RID: 94786
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

		// Token: 0x06017244 RID: 94788
		[DispId(-2147417076)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string toString();

		// Token: 0x17007B6A RID: 31594
		// (get) Token: 0x06017246 RID: 94790
		// (set) Token: 0x06017245 RID: 94789
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

		// Token: 0x17007B6B RID: 31595
		// (get) Token: 0x06017248 RID: 94792
		// (set) Token: 0x06017247 RID: 94791
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

		// Token: 0x17007B6C RID: 31596
		// (get) Token: 0x0601724A RID: 94794
		// (set) Token: 0x06017249 RID: 94793
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

		// Token: 0x17007B6D RID: 31597
		// (get) Token: 0x0601724C RID: 94796
		// (set) Token: 0x0601724B RID: 94795
		[DispId(-2147412094)]
		public virtual extern object onrowexit
		{
			[TypeLibFunc(20)]
			[DispId(-2147412094)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412094)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17007B6E RID: 31598
		// (get) Token: 0x0601724E RID: 94798
		// (set) Token: 0x0601724D RID: 94797
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

		// Token: 0x17007B6F RID: 31599
		// (get) Token: 0x06017250 RID: 94800
		// (set) Token: 0x0601724F RID: 94799
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

		// Token: 0x17007B70 RID: 31600
		// (get) Token: 0x06017252 RID: 94802
		// (set) Token: 0x06017251 RID: 94801
		[DispId(-2147412071)]
		public virtual extern object ondataavailable
		{
			[TypeLibFunc(20)]
			[DispId(-2147412071)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412071)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17007B71 RID: 31601
		// (get) Token: 0x06017254 RID: 94804
		// (set) Token: 0x06017253 RID: 94803
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

		// Token: 0x17007B72 RID: 31602
		// (get) Token: 0x06017256 RID: 94806
		// (set) Token: 0x06017255 RID: 94805
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

		// Token: 0x17007B73 RID: 31603
		// (get) Token: 0x06017257 RID: 94807
		[DispId(-2147417075)]
		public virtual extern object children
		{
			[DispId(-2147417075)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17007B74 RID: 31604
		// (get) Token: 0x06017258 RID: 94808
		[DispId(-2147417074)]
		public virtual extern object all
		{
			[DispId(-2147417074)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17007B75 RID: 31605
		// (get) Token: 0x0601725A RID: 94810
		// (set) Token: 0x06017259 RID: 94809
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

		// Token: 0x0601725B RID: 94811
		[DispId(-2147416112)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void focus();

		// Token: 0x17007B76 RID: 31606
		// (get) Token: 0x0601725D RID: 94813
		// (set) Token: 0x0601725C RID: 94812
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

		// Token: 0x17007B77 RID: 31607
		// (get) Token: 0x0601725F RID: 94815
		// (set) Token: 0x0601725E RID: 94814
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

		// Token: 0x17007B78 RID: 31608
		// (get) Token: 0x06017261 RID: 94817
		// (set) Token: 0x06017260 RID: 94816
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

		// Token: 0x17007B79 RID: 31609
		// (get) Token: 0x06017263 RID: 94819
		// (set) Token: 0x06017262 RID: 94818
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

		// Token: 0x06017264 RID: 94820
		[DispId(-2147416110)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void blur();

		// Token: 0x06017265 RID: 94821
		[DispId(-2147416095)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void addFilter([MarshalAs(25)] [In] object pUnk);

		// Token: 0x06017266 RID: 94822
		[DispId(-2147416094)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void removeFilter([MarshalAs(25)] [In] object pUnk);

		// Token: 0x17007B7A RID: 31610
		// (get) Token: 0x06017267 RID: 94823
		[DispId(-2147416093)]
		public virtual extern int clientHeight
		{
			[TypeLibFunc(20)]
			[DispId(-2147416093)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17007B7B RID: 31611
		// (get) Token: 0x06017268 RID: 94824
		[DispId(-2147416092)]
		public virtual extern int clientWidth
		{
			[DispId(-2147416092)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17007B7C RID: 31612
		// (get) Token: 0x06017269 RID: 94825
		[DispId(-2147416091)]
		public virtual extern int clientTop
		{
			[DispId(-2147416091)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17007B7D RID: 31613
		// (get) Token: 0x0601726A RID: 94826
		[DispId(-2147416090)]
		public virtual extern int clientLeft
		{
			[DispId(-2147416090)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17007B7E RID: 31614
		// (get) Token: 0x0601726B RID: 94827
		[DispId(2000)]
		public virtual extern string type
		{
			[DispId(2000)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17007B7F RID: 31615
		// (get) Token: 0x0601726D RID: 94829
		// (set) Token: 0x0601726C RID: 94828
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

		// Token: 0x17007B80 RID: 31616
		// (get) Token: 0x0601726F RID: 94831
		// (set) Token: 0x0601726E RID: 94830
		[DispId(2012)]
		public virtual extern object border
		{
			[DispId(2012)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(2012)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17007B81 RID: 31617
		// (get) Token: 0x06017271 RID: 94833
		// (set) Token: 0x06017270 RID: 94832
		[DispId(2013)]
		public virtual extern int vspace
		{
			[DispId(2013)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[TypeLibFunc(20)]
			[DispId(2013)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x17007B82 RID: 31618
		// (get) Token: 0x06017273 RID: 94835
		// (set) Token: 0x06017272 RID: 94834
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

		// Token: 0x17007B83 RID: 31619
		// (get) Token: 0x06017275 RID: 94837
		// (set) Token: 0x06017274 RID: 94836
		[DispId(2010)]
		public virtual extern string alt
		{
			[TypeLibFunc(20)]
			[DispId(2010)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(2010)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17007B84 RID: 31620
		// (get) Token: 0x06017277 RID: 94839
		// (set) Token: 0x06017276 RID: 94838
		[DispId(2011)]
		public virtual extern string src
		{
			[TypeLibFunc(20)]
			[DispId(2011)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(2011)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17007B85 RID: 31621
		// (get) Token: 0x06017279 RID: 94841
		// (set) Token: 0x06017278 RID: 94840
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

		// Token: 0x17007B86 RID: 31622
		// (get) Token: 0x0601727B RID: 94843
		// (set) Token: 0x0601727A RID: 94842
		[DispId(2016)]
		public virtual extern string vrml
		{
			[DispId(2016)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(2016)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17007B87 RID: 31623
		// (get) Token: 0x0601727D RID: 94845
		// (set) Token: 0x0601727C RID: 94844
		[DispId(2017)]
		public virtual extern string dynsrc
		{
			[TypeLibFunc(20)]
			[DispId(2017)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(2017)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17007B88 RID: 31624
		// (get) Token: 0x0601727E RID: 94846
		[DispId(-2147412996)]
		public virtual extern string readyState
		{
			[DispId(-2147412996)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17007B89 RID: 31625
		// (get) Token: 0x0601727F RID: 94847
		[DispId(2018)]
		public virtual extern bool complete
		{
			[DispId(2018)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17007B8A RID: 31626
		// (get) Token: 0x06017281 RID: 94849
		// (set) Token: 0x06017280 RID: 94848
		[DispId(2019)]
		public virtual extern object loop
		{
			[TypeLibFunc(20)]
			[DispId(2019)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(2019)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17007B8B RID: 31627
		// (get) Token: 0x06017283 RID: 94851
		// (set) Token: 0x06017282 RID: 94850
		[DispId(-2147418039)]
		public virtual extern string align
		{
			[DispId(-2147418039)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147418039)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17007B8C RID: 31628
		// (get) Token: 0x06017285 RID: 94853
		// (set) Token: 0x06017284 RID: 94852
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

		// Token: 0x17007B8D RID: 31629
		// (get) Token: 0x06017287 RID: 94855
		// (set) Token: 0x06017286 RID: 94854
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

		// Token: 0x17007B8E RID: 31630
		// (get) Token: 0x06017289 RID: 94857
		// (set) Token: 0x06017288 RID: 94856
		[DispId(-2147412084)]
		public virtual extern object onabort
		{
			[DispId(-2147412084)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412084)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17007B8F RID: 31631
		// (get) Token: 0x0601728B RID: 94859
		// (set) Token: 0x0601728A RID: 94858
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

		// Token: 0x17007B90 RID: 31632
		// (get) Token: 0x0601728D RID: 94861
		// (set) Token: 0x0601728C RID: 94860
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

		// Token: 0x17007B91 RID: 31633
		// (get) Token: 0x0601728F RID: 94863
		// (set) Token: 0x0601728E RID: 94862
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

		// Token: 0x17007B92 RID: 31634
		// (get) Token: 0x06017291 RID: 94865
		// (set) Token: 0x06017290 RID: 94864
		[DispId(2020)]
		public virtual extern string Start
		{
			[TypeLibFunc(20)]
			[DispId(2020)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(2020)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17007B93 RID: 31635
		// (get) Token: 0x06017292 RID: 94866
		public virtual extern string IHTMLInputImage_type
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17007B94 RID: 31636
		// (get) Token: 0x06017294 RID: 94868
		// (set) Token: 0x06017293 RID: 94867
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

		// Token: 0x17007B95 RID: 31637
		// (get) Token: 0x06017296 RID: 94870
		// (set) Token: 0x06017295 RID: 94869
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

		// Token: 0x17007B96 RID: 31638
		// (get) Token: 0x06017298 RID: 94872
		// (set) Token: 0x06017297 RID: 94871
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

		// Token: 0x17007B97 RID: 31639
		// (get) Token: 0x0601729A RID: 94874
		// (set) Token: 0x06017299 RID: 94873
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

		// Token: 0x17007B98 RID: 31640
		// (get) Token: 0x0601729C RID: 94876
		// (set) Token: 0x0601729B RID: 94875
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

		// Token: 0x17007B99 RID: 31641
		// (get) Token: 0x0601729E RID: 94878
		// (set) Token: 0x0601729D RID: 94877
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

		// Token: 0x17007B9A RID: 31642
		// (get) Token: 0x060172A0 RID: 94880
		// (set) Token: 0x0601729F RID: 94879
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

		// Token: 0x17007B9B RID: 31643
		// (get) Token: 0x060172A2 RID: 94882
		// (set) Token: 0x060172A1 RID: 94881
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

		// Token: 0x17007B9C RID: 31644
		// (get) Token: 0x060172A4 RID: 94884
		// (set) Token: 0x060172A3 RID: 94883
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

		// Token: 0x17007B9D RID: 31645
		// (get) Token: 0x060172A5 RID: 94885
		public virtual extern string IHTMLInputImage_readyState
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17007B9E RID: 31646
		// (get) Token: 0x060172A6 RID: 94886
		public virtual extern bool IHTMLInputImage_complete
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17007B9F RID: 31647
		// (get) Token: 0x060172A8 RID: 94888
		// (set) Token: 0x060172A7 RID: 94887
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

		// Token: 0x17007BA0 RID: 31648
		// (get) Token: 0x060172AA RID: 94890
		// (set) Token: 0x060172A9 RID: 94889
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

		// Token: 0x17007BA1 RID: 31649
		// (get) Token: 0x060172AC RID: 94892
		// (set) Token: 0x060172AB RID: 94891
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

		// Token: 0x17007BA2 RID: 31650
		// (get) Token: 0x060172AE RID: 94894
		// (set) Token: 0x060172AD RID: 94893
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

		// Token: 0x17007BA3 RID: 31651
		// (get) Token: 0x060172B0 RID: 94896
		// (set) Token: 0x060172AF RID: 94895
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

		// Token: 0x17007BA4 RID: 31652
		// (get) Token: 0x060172B2 RID: 94898
		// (set) Token: 0x060172B1 RID: 94897
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

		// Token: 0x17007BA5 RID: 31653
		// (get) Token: 0x060172B4 RID: 94900
		// (set) Token: 0x060172B3 RID: 94899
		public virtual extern int IHTMLInputImage_width
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x17007BA6 RID: 31654
		// (get) Token: 0x060172B6 RID: 94902
		// (set) Token: 0x060172B5 RID: 94901
		public virtual extern int IHTMLInputImage_height
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x17007BA7 RID: 31655
		// (get) Token: 0x060172B8 RID: 94904
		// (set) Token: 0x060172B7 RID: 94903
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

		// Token: 0x17007BA8 RID: 31656
		// (get) Token: 0x060172BA RID: 94906
		// (set) Token: 0x060172B9 RID: 94905
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

		// Token: 0x060172BB RID: 94907
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLControlElement_focus();

		// Token: 0x17007BA9 RID: 31657
		// (get) Token: 0x060172BD RID: 94909
		// (set) Token: 0x060172BC RID: 94908
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

		// Token: 0x17007BAA RID: 31658
		// (get) Token: 0x060172BF RID: 94911
		// (set) Token: 0x060172BE RID: 94910
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

		// Token: 0x17007BAB RID: 31659
		// (get) Token: 0x060172C1 RID: 94913
		// (set) Token: 0x060172C0 RID: 94912
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

		// Token: 0x17007BAC RID: 31660
		// (get) Token: 0x060172C3 RID: 94915
		// (set) Token: 0x060172C2 RID: 94914
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

		// Token: 0x060172C4 RID: 94916
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLControlElement_blur();

		// Token: 0x060172C5 RID: 94917
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLControlElement_addFilter([MarshalAs(25)] [In] object pUnk);

		// Token: 0x060172C6 RID: 94918
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLControlElement_removeFilter([MarshalAs(25)] [In] object pUnk);

		// Token: 0x17007BAD RID: 31661
		// (get) Token: 0x060172C7 RID: 94919
		public virtual extern int IHTMLControlElement_clientHeight
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17007BAE RID: 31662
		// (get) Token: 0x060172C8 RID: 94920
		public virtual extern int IHTMLControlElement_clientWidth
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17007BAF RID: 31663
		// (get) Token: 0x060172C9 RID: 94921
		public virtual extern int IHTMLControlElement_clientTop
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17007BB0 RID: 31664
		// (get) Token: 0x060172CA RID: 94922
		public virtual extern int IHTMLControlElement_clientLeft
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x060172CB RID: 94923
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement_setAttribute([MarshalAs(19)] [In] string strAttributeName, [MarshalAs(27)] [In] object AttributeValue, [In] int lFlags = 1);

		// Token: 0x060172CC RID: 94924
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		public virtual extern object IHTMLElement_getAttribute([MarshalAs(19)] [In] string strAttributeName, [In] int lFlags = 0);

		// Token: 0x060172CD RID: 94925
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement_removeAttribute([MarshalAs(19)] [In] string strAttributeName, [In] int lFlags = 1);

		// Token: 0x17007BB1 RID: 31665
		// (get) Token: 0x060172CF RID: 94927
		// (set) Token: 0x060172CE RID: 94926
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

		// Token: 0x17007BB2 RID: 31666
		// (get) Token: 0x060172D1 RID: 94929
		// (set) Token: 0x060172D0 RID: 94928
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

		// Token: 0x17007BB3 RID: 31667
		// (get) Token: 0x060172D2 RID: 94930
		public virtual extern string IHTMLElement_tagName
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17007BB4 RID: 31668
		// (get) Token: 0x060172D3 RID: 94931
		public virtual extern IHTMLElement IHTMLElement_parentElement
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17007BB5 RID: 31669
		// (get) Token: 0x060172D4 RID: 94932
		public virtual extern IHTMLStyle IHTMLElement_style
		{
			[TypeLibFunc(1024)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17007BB6 RID: 31670
		// (get) Token: 0x060172D6 RID: 94934
		// (set) Token: 0x060172D5 RID: 94933
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

		// Token: 0x17007BB7 RID: 31671
		// (get) Token: 0x060172D8 RID: 94936
		// (set) Token: 0x060172D7 RID: 94935
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

		// Token: 0x17007BB8 RID: 31672
		// (get) Token: 0x060172DA RID: 94938
		// (set) Token: 0x060172D9 RID: 94937
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

		// Token: 0x17007BB9 RID: 31673
		// (get) Token: 0x060172DC RID: 94940
		// (set) Token: 0x060172DB RID: 94939
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

		// Token: 0x17007BBA RID: 31674
		// (get) Token: 0x060172DE RID: 94942
		// (set) Token: 0x060172DD RID: 94941
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

		// Token: 0x17007BBB RID: 31675
		// (get) Token: 0x060172E0 RID: 94944
		// (set) Token: 0x060172DF RID: 94943
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

		// Token: 0x17007BBC RID: 31676
		// (get) Token: 0x060172E2 RID: 94946
		// (set) Token: 0x060172E1 RID: 94945
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

		// Token: 0x17007BBD RID: 31677
		// (get) Token: 0x060172E4 RID: 94948
		// (set) Token: 0x060172E3 RID: 94947
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

		// Token: 0x17007BBE RID: 31678
		// (get) Token: 0x060172E6 RID: 94950
		// (set) Token: 0x060172E5 RID: 94949
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

		// Token: 0x17007BBF RID: 31679
		// (get) Token: 0x060172E8 RID: 94952
		// (set) Token: 0x060172E7 RID: 94951
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

		// Token: 0x17007BC0 RID: 31680
		// (get) Token: 0x060172EA RID: 94954
		// (set) Token: 0x060172E9 RID: 94953
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

		// Token: 0x17007BC1 RID: 31681
		// (get) Token: 0x060172EB RID: 94955
		public virtual extern object IHTMLElement_document
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17007BC2 RID: 31682
		// (get) Token: 0x060172ED RID: 94957
		// (set) Token: 0x060172EC RID: 94956
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

		// Token: 0x17007BC3 RID: 31683
		// (get) Token: 0x060172EF RID: 94959
		// (set) Token: 0x060172EE RID: 94958
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

		// Token: 0x17007BC4 RID: 31684
		// (get) Token: 0x060172F1 RID: 94961
		// (set) Token: 0x060172F0 RID: 94960
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

		// Token: 0x060172F2 RID: 94962
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement_scrollIntoView([MarshalAs(27)] [In] [Optional] object varargStart);

		// Token: 0x060172F3 RID: 94963
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement_contains([MarshalAs(28)] [In] IHTMLElement pChild);

		// Token: 0x17007BC5 RID: 31685
		// (get) Token: 0x060172F4 RID: 94964
		public virtual extern int IHTMLElement_sourceIndex
		{
			[TypeLibFunc(4)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17007BC6 RID: 31686
		// (get) Token: 0x060172F5 RID: 94965
		public virtual extern object IHTMLElement_recordNumber
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x17007BC7 RID: 31687
		// (get) Token: 0x060172F7 RID: 94967
		// (set) Token: 0x060172F6 RID: 94966
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

		// Token: 0x17007BC8 RID: 31688
		// (get) Token: 0x060172F8 RID: 94968
		public virtual extern int IHTMLElement_offsetLeft
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17007BC9 RID: 31689
		// (get) Token: 0x060172F9 RID: 94969
		public virtual extern int IHTMLElement_offsetTop
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17007BCA RID: 31690
		// (get) Token: 0x060172FA RID: 94970
		public virtual extern int IHTMLElement_offsetWidth
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17007BCB RID: 31691
		// (get) Token: 0x060172FB RID: 94971
		public virtual extern int IHTMLElement_offsetHeight
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17007BCC RID: 31692
		// (get) Token: 0x060172FC RID: 94972
		public virtual extern IHTMLElement IHTMLElement_offsetParent
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17007BCD RID: 31693
		// (get) Token: 0x060172FE RID: 94974
		// (set) Token: 0x060172FD RID: 94973
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

		// Token: 0x17007BCE RID: 31694
		// (get) Token: 0x06017300 RID: 94976
		// (set) Token: 0x060172FF RID: 94975
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

		// Token: 0x17007BCF RID: 31695
		// (get) Token: 0x06017302 RID: 94978
		// (set) Token: 0x06017301 RID: 94977
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

		// Token: 0x17007BD0 RID: 31696
		// (get) Token: 0x06017304 RID: 94980
		// (set) Token: 0x06017303 RID: 94979
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

		// Token: 0x06017305 RID: 94981
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement_insertAdjacentHTML([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string html);

		// Token: 0x06017306 RID: 94982
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement_insertAdjacentText([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string text);

		// Token: 0x17007BD1 RID: 31697
		// (get) Token: 0x06017307 RID: 94983
		public virtual extern IHTMLElement IHTMLElement_parentTextEdit
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17007BD2 RID: 31698
		// (get) Token: 0x06017308 RID: 94984
		public virtual extern bool IHTMLElement_isTextEdit
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x06017309 RID: 94985
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement_click();

		// Token: 0x17007BD3 RID: 31699
		// (get) Token: 0x0601730A RID: 94986
		public virtual extern IHTMLFiltersCollection IHTMLElement_filters
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17007BD4 RID: 31700
		// (get) Token: 0x0601730C RID: 94988
		// (set) Token: 0x0601730B RID: 94987
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

		// Token: 0x0601730D RID: 94989
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string IHTMLElement_toString();

		// Token: 0x17007BD5 RID: 31701
		// (get) Token: 0x0601730F RID: 94991
		// (set) Token: 0x0601730E RID: 94990
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

		// Token: 0x17007BD6 RID: 31702
		// (get) Token: 0x06017311 RID: 94993
		// (set) Token: 0x06017310 RID: 94992
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

		// Token: 0x17007BD7 RID: 31703
		// (get) Token: 0x06017313 RID: 94995
		// (set) Token: 0x06017312 RID: 94994
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

		// Token: 0x17007BD8 RID: 31704
		// (get) Token: 0x06017315 RID: 94997
		// (set) Token: 0x06017314 RID: 94996
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

		// Token: 0x17007BD9 RID: 31705
		// (get) Token: 0x06017317 RID: 94999
		// (set) Token: 0x06017316 RID: 94998
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

		// Token: 0x17007BDA RID: 31706
		// (get) Token: 0x06017319 RID: 95001
		// (set) Token: 0x06017318 RID: 95000
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

		// Token: 0x17007BDB RID: 31707
		// (get) Token: 0x0601731B RID: 95003
		// (set) Token: 0x0601731A RID: 95002
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

		// Token: 0x17007BDC RID: 31708
		// (get) Token: 0x0601731D RID: 95005
		// (set) Token: 0x0601731C RID: 95004
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

		// Token: 0x17007BDD RID: 31709
		// (get) Token: 0x0601731F RID: 95007
		// (set) Token: 0x0601731E RID: 95006
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

		// Token: 0x17007BDE RID: 31710
		// (get) Token: 0x06017320 RID: 95008
		public virtual extern object IHTMLElement_children
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17007BDF RID: 31711
		// (get) Token: 0x06017321 RID: 95009
		public virtual extern object IHTMLElement_all
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x14002CBE RID: 11454
		// (add) Token: 0x06017322 RID: 95010
		// (remove) Token: 0x06017323 RID: 95011
		public virtual extern event HTMLInputImageEvents_onhelpEventHandler HTMLInputImageEvents_Event_onhelp;

		// Token: 0x14002CBF RID: 11455
		// (add) Token: 0x06017324 RID: 95012
		// (remove) Token: 0x06017325 RID: 95013
		public virtual extern event HTMLInputImageEvents_onclickEventHandler HTMLInputImageEvents_Event_onclick;

		// Token: 0x14002CC0 RID: 11456
		// (add) Token: 0x06017326 RID: 95014
		// (remove) Token: 0x06017327 RID: 95015
		public virtual extern event HTMLInputImageEvents_ondblclickEventHandler HTMLInputImageEvents_Event_ondblclick;

		// Token: 0x14002CC1 RID: 11457
		// (add) Token: 0x06017328 RID: 95016
		// (remove) Token: 0x06017329 RID: 95017
		public virtual extern event HTMLInputImageEvents_onkeypressEventHandler HTMLInputImageEvents_Event_onkeypress;

		// Token: 0x14002CC2 RID: 11458
		// (add) Token: 0x0601732A RID: 95018
		// (remove) Token: 0x0601732B RID: 95019
		public virtual extern event HTMLInputImageEvents_onkeydownEventHandler HTMLInputImageEvents_Event_onkeydown;

		// Token: 0x14002CC3 RID: 11459
		// (add) Token: 0x0601732C RID: 95020
		// (remove) Token: 0x0601732D RID: 95021
		public virtual extern event HTMLInputImageEvents_onkeyupEventHandler HTMLInputImageEvents_Event_onkeyup;

		// Token: 0x14002CC4 RID: 11460
		// (add) Token: 0x0601732E RID: 95022
		// (remove) Token: 0x0601732F RID: 95023
		public virtual extern event HTMLInputImageEvents_onmouseoutEventHandler HTMLInputImageEvents_Event_onmouseout;

		// Token: 0x14002CC5 RID: 11461
		// (add) Token: 0x06017330 RID: 95024
		// (remove) Token: 0x06017331 RID: 95025
		public virtual extern event HTMLInputImageEvents_onmouseoverEventHandler HTMLInputImageEvents_Event_onmouseover;

		// Token: 0x14002CC6 RID: 11462
		// (add) Token: 0x06017332 RID: 95026
		// (remove) Token: 0x06017333 RID: 95027
		public virtual extern event HTMLInputImageEvents_onmousemoveEventHandler HTMLInputImageEvents_Event_onmousemove;

		// Token: 0x14002CC7 RID: 11463
		// (add) Token: 0x06017334 RID: 95028
		// (remove) Token: 0x06017335 RID: 95029
		public virtual extern event HTMLInputImageEvents_onmousedownEventHandler HTMLInputImageEvents_Event_onmousedown;

		// Token: 0x14002CC8 RID: 11464
		// (add) Token: 0x06017336 RID: 95030
		// (remove) Token: 0x06017337 RID: 95031
		public virtual extern event HTMLInputImageEvents_onmouseupEventHandler HTMLInputImageEvents_Event_onmouseup;

		// Token: 0x14002CC9 RID: 11465
		// (add) Token: 0x06017338 RID: 95032
		// (remove) Token: 0x06017339 RID: 95033
		public virtual extern event HTMLInputImageEvents_onselectstartEventHandler HTMLInputImageEvents_Event_onselectstart;

		// Token: 0x14002CCA RID: 11466
		// (add) Token: 0x0601733A RID: 95034
		// (remove) Token: 0x0601733B RID: 95035
		public virtual extern event HTMLInputImageEvents_onfilterchangeEventHandler HTMLInputImageEvents_Event_onfilterchange;

		// Token: 0x14002CCB RID: 11467
		// (add) Token: 0x0601733C RID: 95036
		// (remove) Token: 0x0601733D RID: 95037
		public virtual extern event HTMLInputImageEvents_ondragstartEventHandler HTMLInputImageEvents_Event_ondragstart;

		// Token: 0x14002CCC RID: 11468
		// (add) Token: 0x0601733E RID: 95038
		// (remove) Token: 0x0601733F RID: 95039
		public virtual extern event HTMLInputImageEvents_onbeforeupdateEventHandler HTMLInputImageEvents_Event_onbeforeupdate;

		// Token: 0x14002CCD RID: 11469
		// (add) Token: 0x06017340 RID: 95040
		// (remove) Token: 0x06017341 RID: 95041
		public virtual extern event HTMLInputImageEvents_onafterupdateEventHandler HTMLInputImageEvents_Event_onafterupdate;

		// Token: 0x14002CCE RID: 11470
		// (add) Token: 0x06017342 RID: 95042
		// (remove) Token: 0x06017343 RID: 95043
		public virtual extern event HTMLInputImageEvents_onerrorupdateEventHandler HTMLInputImageEvents_Event_onerrorupdate;

		// Token: 0x14002CCF RID: 11471
		// (add) Token: 0x06017344 RID: 95044
		// (remove) Token: 0x06017345 RID: 95045
		public virtual extern event HTMLInputImageEvents_onrowexitEventHandler HTMLInputImageEvents_Event_onrowexit;

		// Token: 0x14002CD0 RID: 11472
		// (add) Token: 0x06017346 RID: 95046
		// (remove) Token: 0x06017347 RID: 95047
		public virtual extern event HTMLInputImageEvents_onrowenterEventHandler HTMLInputImageEvents_Event_onrowenter;

		// Token: 0x14002CD1 RID: 11473
		// (add) Token: 0x06017348 RID: 95048
		// (remove) Token: 0x06017349 RID: 95049
		public virtual extern event HTMLInputImageEvents_ondatasetchangedEventHandler HTMLInputImageEvents_Event_ondatasetchanged;

		// Token: 0x14002CD2 RID: 11474
		// (add) Token: 0x0601734A RID: 95050
		// (remove) Token: 0x0601734B RID: 95051
		public virtual extern event HTMLInputImageEvents_ondataavailableEventHandler HTMLInputImageEvents_Event_ondataavailable;

		// Token: 0x14002CD3 RID: 11475
		// (add) Token: 0x0601734C RID: 95052
		// (remove) Token: 0x0601734D RID: 95053
		public virtual extern event HTMLInputImageEvents_ondatasetcompleteEventHandler HTMLInputImageEvents_Event_ondatasetcomplete;

		// Token: 0x14002CD4 RID: 11476
		// (add) Token: 0x0601734E RID: 95054
		// (remove) Token: 0x0601734F RID: 95055
		public virtual extern event HTMLInputImageEvents_onlosecaptureEventHandler onlosecapture;

		// Token: 0x14002CD5 RID: 11477
		// (add) Token: 0x06017350 RID: 95056
		// (remove) Token: 0x06017351 RID: 95057
		public virtual extern event HTMLInputImageEvents_onpropertychangeEventHandler onpropertychange;

		// Token: 0x14002CD6 RID: 11478
		// (add) Token: 0x06017352 RID: 95058
		// (remove) Token: 0x06017353 RID: 95059
		public virtual extern event HTMLInputImageEvents_onscrollEventHandler onscroll;

		// Token: 0x14002CD7 RID: 11479
		// (add) Token: 0x06017354 RID: 95060
		// (remove) Token: 0x06017355 RID: 95061
		public virtual extern event HTMLInputImageEvents_onfocusEventHandler HTMLInputImageEvents_Event_onfocus;

		// Token: 0x14002CD8 RID: 11480
		// (add) Token: 0x06017356 RID: 95062
		// (remove) Token: 0x06017357 RID: 95063
		public virtual extern event HTMLInputImageEvents_onblurEventHandler HTMLInputImageEvents_Event_onblur;

		// Token: 0x14002CD9 RID: 11481
		// (add) Token: 0x06017358 RID: 95064
		// (remove) Token: 0x06017359 RID: 95065
		public virtual extern event HTMLInputImageEvents_onresizeEventHandler HTMLInputImageEvents_Event_onresize;

		// Token: 0x14002CDA RID: 11482
		// (add) Token: 0x0601735A RID: 95066
		// (remove) Token: 0x0601735B RID: 95067
		public virtual extern event HTMLInputImageEvents_ondragEventHandler ondrag;

		// Token: 0x14002CDB RID: 11483
		// (add) Token: 0x0601735C RID: 95068
		// (remove) Token: 0x0601735D RID: 95069
		public virtual extern event HTMLInputImageEvents_ondragendEventHandler ondragend;

		// Token: 0x14002CDC RID: 11484
		// (add) Token: 0x0601735E RID: 95070
		// (remove) Token: 0x0601735F RID: 95071
		public virtual extern event HTMLInputImageEvents_ondragenterEventHandler ondragenter;

		// Token: 0x14002CDD RID: 11485
		// (add) Token: 0x06017360 RID: 95072
		// (remove) Token: 0x06017361 RID: 95073
		public virtual extern event HTMLInputImageEvents_ondragoverEventHandler ondragover;

		// Token: 0x14002CDE RID: 11486
		// (add) Token: 0x06017362 RID: 95074
		// (remove) Token: 0x06017363 RID: 95075
		public virtual extern event HTMLInputImageEvents_ondragleaveEventHandler ondragleave;

		// Token: 0x14002CDF RID: 11487
		// (add) Token: 0x06017364 RID: 95076
		// (remove) Token: 0x06017365 RID: 95077
		public virtual extern event HTMLInputImageEvents_ondropEventHandler ondrop;

		// Token: 0x14002CE0 RID: 11488
		// (add) Token: 0x06017366 RID: 95078
		// (remove) Token: 0x06017367 RID: 95079
		public virtual extern event HTMLInputImageEvents_onbeforecutEventHandler onbeforecut;

		// Token: 0x14002CE1 RID: 11489
		// (add) Token: 0x06017368 RID: 95080
		// (remove) Token: 0x06017369 RID: 95081
		public virtual extern event HTMLInputImageEvents_oncutEventHandler oncut;

		// Token: 0x14002CE2 RID: 11490
		// (add) Token: 0x0601736A RID: 95082
		// (remove) Token: 0x0601736B RID: 95083
		public virtual extern event HTMLInputImageEvents_onbeforecopyEventHandler onbeforecopy;

		// Token: 0x14002CE3 RID: 11491
		// (add) Token: 0x0601736C RID: 95084
		// (remove) Token: 0x0601736D RID: 95085
		public virtual extern event HTMLInputImageEvents_oncopyEventHandler oncopy;

		// Token: 0x14002CE4 RID: 11492
		// (add) Token: 0x0601736E RID: 95086
		// (remove) Token: 0x0601736F RID: 95087
		public virtual extern event HTMLInputImageEvents_onbeforepasteEventHandler onbeforepaste;

		// Token: 0x14002CE5 RID: 11493
		// (add) Token: 0x06017370 RID: 95088
		// (remove) Token: 0x06017371 RID: 95089
		public virtual extern event HTMLInputImageEvents_onpasteEventHandler onpaste;

		// Token: 0x14002CE6 RID: 11494
		// (add) Token: 0x06017372 RID: 95090
		// (remove) Token: 0x06017373 RID: 95091
		public virtual extern event HTMLInputImageEvents_oncontextmenuEventHandler oncontextmenu;

		// Token: 0x14002CE7 RID: 11495
		// (add) Token: 0x06017374 RID: 95092
		// (remove) Token: 0x06017375 RID: 95093
		public virtual extern event HTMLInputImageEvents_onrowsdeleteEventHandler onrowsdelete;

		// Token: 0x14002CE8 RID: 11496
		// (add) Token: 0x06017376 RID: 95094
		// (remove) Token: 0x06017377 RID: 95095
		public virtual extern event HTMLInputImageEvents_onrowsinsertedEventHandler onrowsinserted;

		// Token: 0x14002CE9 RID: 11497
		// (add) Token: 0x06017378 RID: 95096
		// (remove) Token: 0x06017379 RID: 95097
		public virtual extern event HTMLInputImageEvents_oncellchangeEventHandler oncellchange;

		// Token: 0x14002CEA RID: 11498
		// (add) Token: 0x0601737A RID: 95098
		// (remove) Token: 0x0601737B RID: 95099
		public virtual extern event HTMLInputImageEvents_onreadystatechangeEventHandler onreadystatechange;

		// Token: 0x14002CEB RID: 11499
		// (add) Token: 0x0601737C RID: 95100
		// (remove) Token: 0x0601737D RID: 95101
		public virtual extern event HTMLInputImageEvents_onbeforeeditfocusEventHandler onbeforeeditfocus;

		// Token: 0x14002CEC RID: 11500
		// (add) Token: 0x0601737E RID: 95102
		// (remove) Token: 0x0601737F RID: 95103
		public virtual extern event HTMLInputImageEvents_onlayoutcompleteEventHandler onlayoutcomplete;

		// Token: 0x14002CED RID: 11501
		// (add) Token: 0x06017380 RID: 95104
		// (remove) Token: 0x06017381 RID: 95105
		public virtual extern event HTMLInputImageEvents_onpageEventHandler onpage;

		// Token: 0x14002CEE RID: 11502
		// (add) Token: 0x06017382 RID: 95106
		// (remove) Token: 0x06017383 RID: 95107
		public virtual extern event HTMLInputImageEvents_onbeforedeactivateEventHandler onbeforedeactivate;

		// Token: 0x14002CEF RID: 11503
		// (add) Token: 0x06017384 RID: 95108
		// (remove) Token: 0x06017385 RID: 95109
		public virtual extern event HTMLInputImageEvents_onbeforeactivateEventHandler onbeforeactivate;

		// Token: 0x14002CF0 RID: 11504
		// (add) Token: 0x06017386 RID: 95110
		// (remove) Token: 0x06017387 RID: 95111
		public virtual extern event HTMLInputImageEvents_onmoveEventHandler onmove;

		// Token: 0x14002CF1 RID: 11505
		// (add) Token: 0x06017388 RID: 95112
		// (remove) Token: 0x06017389 RID: 95113
		public virtual extern event HTMLInputImageEvents_oncontrolselectEventHandler oncontrolselect;

		// Token: 0x14002CF2 RID: 11506
		// (add) Token: 0x0601738A RID: 95114
		// (remove) Token: 0x0601738B RID: 95115
		public virtual extern event HTMLInputImageEvents_onmovestartEventHandler onmovestart;

		// Token: 0x14002CF3 RID: 11507
		// (add) Token: 0x0601738C RID: 95116
		// (remove) Token: 0x0601738D RID: 95117
		public virtual extern event HTMLInputImageEvents_onmoveendEventHandler onmoveend;

		// Token: 0x14002CF4 RID: 11508
		// (add) Token: 0x0601738E RID: 95118
		// (remove) Token: 0x0601738F RID: 95119
		public virtual extern event HTMLInputImageEvents_onresizestartEventHandler onresizestart;

		// Token: 0x14002CF5 RID: 11509
		// (add) Token: 0x06017390 RID: 95120
		// (remove) Token: 0x06017391 RID: 95121
		public virtual extern event HTMLInputImageEvents_onresizeendEventHandler onresizeend;

		// Token: 0x14002CF6 RID: 11510
		// (add) Token: 0x06017392 RID: 95122
		// (remove) Token: 0x06017393 RID: 95123
		public virtual extern event HTMLInputImageEvents_onmouseenterEventHandler onmouseenter;

		// Token: 0x14002CF7 RID: 11511
		// (add) Token: 0x06017394 RID: 95124
		// (remove) Token: 0x06017395 RID: 95125
		public virtual extern event HTMLInputImageEvents_onmouseleaveEventHandler onmouseleave;

		// Token: 0x14002CF8 RID: 11512
		// (add) Token: 0x06017396 RID: 95126
		// (remove) Token: 0x06017397 RID: 95127
		public virtual extern event HTMLInputImageEvents_onmousewheelEventHandler onmousewheel;

		// Token: 0x14002CF9 RID: 11513
		// (add) Token: 0x06017398 RID: 95128
		// (remove) Token: 0x06017399 RID: 95129
		public virtual extern event HTMLInputImageEvents_onactivateEventHandler onactivate;

		// Token: 0x14002CFA RID: 11514
		// (add) Token: 0x0601739A RID: 95130
		// (remove) Token: 0x0601739B RID: 95131
		public virtual extern event HTMLInputImageEvents_ondeactivateEventHandler ondeactivate;

		// Token: 0x14002CFB RID: 11515
		// (add) Token: 0x0601739C RID: 95132
		// (remove) Token: 0x0601739D RID: 95133
		public virtual extern event HTMLInputImageEvents_onfocusinEventHandler onfocusin;

		// Token: 0x14002CFC RID: 11516
		// (add) Token: 0x0601739E RID: 95134
		// (remove) Token: 0x0601739F RID: 95135
		public virtual extern event HTMLInputImageEvents_onfocusoutEventHandler onfocusout;

		// Token: 0x14002CFD RID: 11517
		// (add) Token: 0x060173A0 RID: 95136
		// (remove) Token: 0x060173A1 RID: 95137
		public virtual extern event HTMLInputImageEvents_onloadEventHandler HTMLInputImageEvents_Event_onload;

		// Token: 0x14002CFE RID: 11518
		// (add) Token: 0x060173A2 RID: 95138
		// (remove) Token: 0x060173A3 RID: 95139
		public virtual extern event HTMLInputImageEvents_onerrorEventHandler HTMLInputImageEvents_Event_onerror;

		// Token: 0x14002CFF RID: 11519
		// (add) Token: 0x060173A4 RID: 95140
		// (remove) Token: 0x060173A5 RID: 95141
		public virtual extern event HTMLInputImageEvents_onabortEventHandler HTMLInputImageEvents_Event_onabort;
	}
}
