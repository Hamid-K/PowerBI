using System;
using System.Collections;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x020001E7 RID: 487
	[TypeLibType(2)]
	[ComSourceInterfaces("mshtml.HTMLFormElementEvents\0mshtml.HTMLFormElementEvents2\0\0")]
	[DefaultMember("item")]
	[ClassInterface(0)]
	[Guid("3050F251-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public class HTMLFormElementClass : DispHTMLFormElement, HTMLFormElement, HTMLFormElementEvents_Event, IHTMLElement, IHTMLElement2, IHTMLElement3, IHTMLElement4, IHTMLUniqueName, IHTMLDOMNode, IHTMLDOMNode2, IHTMLFormElement, IHTMLFormElement2, IHTMLFormElement3, IHTMLSubmitData, HTMLFormElementEvents2_Event
	{
		// Token: 0x06001D16 RID: 7446
		[MethodImpl(4096, MethodCodeType = 3)]
		public extern HTMLFormElementClass();

		// Token: 0x06001D17 RID: 7447
		[DispId(-2147417611)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void setAttribute([MarshalAs(19)] [In] string strAttributeName, [MarshalAs(27)] [In] object AttributeValue, [In] int lFlags = 1);

		// Token: 0x06001D18 RID: 7448
		[DispId(-2147417610)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		public virtual extern object getAttribute([MarshalAs(19)] [In] string strAttributeName, [In] int lFlags = 0);

		// Token: 0x06001D19 RID: 7449
		[DispId(-2147417609)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool removeAttribute([MarshalAs(19)] [In] string strAttributeName, [In] int lFlags = 1);

		// Token: 0x17000B6D RID: 2925
		// (get) Token: 0x06001D1B RID: 7451
		// (set) Token: 0x06001D1A RID: 7450
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

		// Token: 0x17000B6E RID: 2926
		// (get) Token: 0x06001D1D RID: 7453
		// (set) Token: 0x06001D1C RID: 7452
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

		// Token: 0x17000B6F RID: 2927
		// (get) Token: 0x06001D1E RID: 7454
		[DispId(-2147417108)]
		public virtual extern string tagName
		{
			[DispId(-2147417108)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17000B70 RID: 2928
		// (get) Token: 0x06001D1F RID: 7455
		[DispId(-2147418104)]
		public virtual extern IHTMLElement parentElement
		{
			[DispId(-2147418104)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17000B71 RID: 2929
		// (get) Token: 0x06001D20 RID: 7456
		[DispId(-2147418038)]
		public virtual extern IHTMLStyle style
		{
			[DispId(-2147418038)]
			[TypeLibFunc(1024)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17000B72 RID: 2930
		// (get) Token: 0x06001D22 RID: 7458
		// (set) Token: 0x06001D21 RID: 7457
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

		// Token: 0x17000B73 RID: 2931
		// (get) Token: 0x06001D24 RID: 7460
		// (set) Token: 0x06001D23 RID: 7459
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

		// Token: 0x17000B74 RID: 2932
		// (get) Token: 0x06001D26 RID: 7462
		// (set) Token: 0x06001D25 RID: 7461
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

		// Token: 0x17000B75 RID: 2933
		// (get) Token: 0x06001D28 RID: 7464
		// (set) Token: 0x06001D27 RID: 7463
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

		// Token: 0x17000B76 RID: 2934
		// (get) Token: 0x06001D2A RID: 7466
		// (set) Token: 0x06001D29 RID: 7465
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

		// Token: 0x17000B77 RID: 2935
		// (get) Token: 0x06001D2C RID: 7468
		// (set) Token: 0x06001D2B RID: 7467
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

		// Token: 0x17000B78 RID: 2936
		// (get) Token: 0x06001D2E RID: 7470
		// (set) Token: 0x06001D2D RID: 7469
		[DispId(-2147412111)]
		public virtual extern object onmouseout
		{
			[TypeLibFunc(20)]
			[DispId(-2147412111)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412111)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17000B79 RID: 2937
		// (get) Token: 0x06001D30 RID: 7472
		// (set) Token: 0x06001D2F RID: 7471
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

		// Token: 0x17000B7A RID: 2938
		// (get) Token: 0x06001D32 RID: 7474
		// (set) Token: 0x06001D31 RID: 7473
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

		// Token: 0x17000B7B RID: 2939
		// (get) Token: 0x06001D34 RID: 7476
		// (set) Token: 0x06001D33 RID: 7475
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

		// Token: 0x17000B7C RID: 2940
		// (get) Token: 0x06001D36 RID: 7478
		// (set) Token: 0x06001D35 RID: 7477
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

		// Token: 0x17000B7D RID: 2941
		// (get) Token: 0x06001D37 RID: 7479
		[DispId(-2147417094)]
		public virtual extern object document
		{
			[DispId(-2147417094)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17000B7E RID: 2942
		// (get) Token: 0x06001D39 RID: 7481
		// (set) Token: 0x06001D38 RID: 7480
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

		// Token: 0x17000B7F RID: 2943
		// (get) Token: 0x06001D3B RID: 7483
		// (set) Token: 0x06001D3A RID: 7482
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

		// Token: 0x17000B80 RID: 2944
		// (get) Token: 0x06001D3D RID: 7485
		// (set) Token: 0x06001D3C RID: 7484
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

		// Token: 0x06001D3E RID: 7486
		[DispId(-2147417093)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void scrollIntoView([MarshalAs(27)] [In] [Optional] object varargStart);

		// Token: 0x06001D3F RID: 7487
		[DispId(-2147417092)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool contains([MarshalAs(28)] [In] IHTMLElement pChild);

		// Token: 0x17000B81 RID: 2945
		// (get) Token: 0x06001D40 RID: 7488
		[DispId(-2147417088)]
		public virtual extern int sourceIndex
		{
			[TypeLibFunc(4)]
			[DispId(-2147417088)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17000B82 RID: 2946
		// (get) Token: 0x06001D41 RID: 7489
		[DispId(-2147417087)]
		public virtual extern object recordNumber
		{
			[DispId(-2147417087)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x17000B83 RID: 2947
		// (get) Token: 0x06001D43 RID: 7491
		// (set) Token: 0x06001D42 RID: 7490
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

		// Token: 0x17000B84 RID: 2948
		// (get) Token: 0x06001D44 RID: 7492
		[DispId(-2147417104)]
		public virtual extern int offsetLeft
		{
			[DispId(-2147417104)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17000B85 RID: 2949
		// (get) Token: 0x06001D45 RID: 7493
		[DispId(-2147417103)]
		public virtual extern int offsetTop
		{
			[DispId(-2147417103)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17000B86 RID: 2950
		// (get) Token: 0x06001D46 RID: 7494
		[DispId(-2147417102)]
		public virtual extern int offsetWidth
		{
			[DispId(-2147417102)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17000B87 RID: 2951
		// (get) Token: 0x06001D47 RID: 7495
		[DispId(-2147417101)]
		public virtual extern int offsetHeight
		{
			[DispId(-2147417101)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17000B88 RID: 2952
		// (get) Token: 0x06001D48 RID: 7496
		[DispId(-2147417100)]
		public virtual extern IHTMLElement offsetParent
		{
			[DispId(-2147417100)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17000B89 RID: 2953
		// (get) Token: 0x06001D4A RID: 7498
		// (set) Token: 0x06001D49 RID: 7497
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

		// Token: 0x17000B8A RID: 2954
		// (get) Token: 0x06001D4C RID: 7500
		// (set) Token: 0x06001D4B RID: 7499
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

		// Token: 0x17000B8B RID: 2955
		// (get) Token: 0x06001D4E RID: 7502
		// (set) Token: 0x06001D4D RID: 7501
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

		// Token: 0x17000B8C RID: 2956
		// (get) Token: 0x06001D50 RID: 7504
		// (set) Token: 0x06001D4F RID: 7503
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

		// Token: 0x06001D51 RID: 7505
		[DispId(-2147417082)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void insertAdjacentHTML([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string html);

		// Token: 0x06001D52 RID: 7506
		[DispId(-2147417081)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void insertAdjacentText([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string text);

		// Token: 0x17000B8D RID: 2957
		// (get) Token: 0x06001D53 RID: 7507
		[DispId(-2147417080)]
		public virtual extern IHTMLElement parentTextEdit
		{
			[DispId(-2147417080)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17000B8E RID: 2958
		// (get) Token: 0x06001D54 RID: 7508
		[DispId(-2147417078)]
		public virtual extern bool isTextEdit
		{
			[DispId(-2147417078)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x06001D55 RID: 7509
		[DispId(-2147417079)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void click();

		// Token: 0x17000B8F RID: 2959
		// (get) Token: 0x06001D56 RID: 7510
		[DispId(-2147417077)]
		public virtual extern IHTMLFiltersCollection filters
		{
			[DispId(-2147417077)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17000B90 RID: 2960
		// (get) Token: 0x06001D58 RID: 7512
		// (set) Token: 0x06001D57 RID: 7511
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

		// Token: 0x06001D59 RID: 7513
		[DispId(-2147417076)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string toString();

		// Token: 0x17000B91 RID: 2961
		// (get) Token: 0x06001D5B RID: 7515
		// (set) Token: 0x06001D5A RID: 7514
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

		// Token: 0x17000B92 RID: 2962
		// (get) Token: 0x06001D5D RID: 7517
		// (set) Token: 0x06001D5C RID: 7516
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

		// Token: 0x17000B93 RID: 2963
		// (get) Token: 0x06001D5F RID: 7519
		// (set) Token: 0x06001D5E RID: 7518
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

		// Token: 0x17000B94 RID: 2964
		// (get) Token: 0x06001D61 RID: 7521
		// (set) Token: 0x06001D60 RID: 7520
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

		// Token: 0x17000B95 RID: 2965
		// (get) Token: 0x06001D63 RID: 7523
		// (set) Token: 0x06001D62 RID: 7522
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

		// Token: 0x17000B96 RID: 2966
		// (get) Token: 0x06001D65 RID: 7525
		// (set) Token: 0x06001D64 RID: 7524
		[DispId(-2147412072)]
		public virtual extern object ondatasetchanged
		{
			[TypeLibFunc(20)]
			[DispId(-2147412072)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412072)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17000B97 RID: 2967
		// (get) Token: 0x06001D67 RID: 7527
		// (set) Token: 0x06001D66 RID: 7526
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

		// Token: 0x17000B98 RID: 2968
		// (get) Token: 0x06001D69 RID: 7529
		// (set) Token: 0x06001D68 RID: 7528
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

		// Token: 0x17000B99 RID: 2969
		// (get) Token: 0x06001D6B RID: 7531
		// (set) Token: 0x06001D6A RID: 7530
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

		// Token: 0x17000B9A RID: 2970
		// (get) Token: 0x06001D6C RID: 7532
		[DispId(-2147417075)]
		public virtual extern object children
		{
			[DispId(-2147417075)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17000B9B RID: 2971
		// (get) Token: 0x06001D6D RID: 7533
		[DispId(-2147417074)]
		public virtual extern object all
		{
			[DispId(-2147417074)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17000B9C RID: 2972
		// (get) Token: 0x06001D6E RID: 7534
		[DispId(-2147417073)]
		public virtual extern string scopeName
		{
			[DispId(-2147417073)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x06001D6F RID: 7535
		[DispId(-2147417072)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void setCapture([In] bool containerCapture = true);

		// Token: 0x06001D70 RID: 7536
		[DispId(-2147417071)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void releaseCapture();

		// Token: 0x17000B9D RID: 2973
		// (get) Token: 0x06001D72 RID: 7538
		// (set) Token: 0x06001D71 RID: 7537
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

		// Token: 0x06001D73 RID: 7539
		[DispId(-2147417070)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string componentFromPoint([In] int x, [In] int y);

		// Token: 0x06001D74 RID: 7540
		[DispId(-2147417069)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void doScroll([MarshalAs(27)] [In] [Optional] object component);

		// Token: 0x17000B9E RID: 2974
		// (get) Token: 0x06001D76 RID: 7542
		// (set) Token: 0x06001D75 RID: 7541
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

		// Token: 0x17000B9F RID: 2975
		// (get) Token: 0x06001D78 RID: 7544
		// (set) Token: 0x06001D77 RID: 7543
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

		// Token: 0x17000BA0 RID: 2976
		// (get) Token: 0x06001D7A RID: 7546
		// (set) Token: 0x06001D79 RID: 7545
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

		// Token: 0x17000BA1 RID: 2977
		// (get) Token: 0x06001D7C RID: 7548
		// (set) Token: 0x06001D7B RID: 7547
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

		// Token: 0x17000BA2 RID: 2978
		// (get) Token: 0x06001D7E RID: 7550
		// (set) Token: 0x06001D7D RID: 7549
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

		// Token: 0x17000BA3 RID: 2979
		// (get) Token: 0x06001D80 RID: 7552
		// (set) Token: 0x06001D7F RID: 7551
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

		// Token: 0x17000BA4 RID: 2980
		// (get) Token: 0x06001D82 RID: 7554
		// (set) Token: 0x06001D81 RID: 7553
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

		// Token: 0x17000BA5 RID: 2981
		// (get) Token: 0x06001D84 RID: 7556
		// (set) Token: 0x06001D83 RID: 7555
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

		// Token: 0x17000BA6 RID: 2982
		// (get) Token: 0x06001D86 RID: 7558
		// (set) Token: 0x06001D85 RID: 7557
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

		// Token: 0x17000BA7 RID: 2983
		// (get) Token: 0x06001D88 RID: 7560
		// (set) Token: 0x06001D87 RID: 7559
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

		// Token: 0x17000BA8 RID: 2984
		// (get) Token: 0x06001D8A RID: 7562
		// (set) Token: 0x06001D89 RID: 7561
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

		// Token: 0x17000BA9 RID: 2985
		// (get) Token: 0x06001D8C RID: 7564
		// (set) Token: 0x06001D8B RID: 7563
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

		// Token: 0x17000BAA RID: 2986
		// (get) Token: 0x06001D8E RID: 7566
		// (set) Token: 0x06001D8D RID: 7565
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

		// Token: 0x17000BAB RID: 2987
		// (get) Token: 0x06001D8F RID: 7567
		[DispId(-2147417105)]
		public virtual extern IHTMLCurrentStyle currentStyle
		{
			[TypeLibFunc(1024)]
			[DispId(-2147417105)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17000BAC RID: 2988
		// (get) Token: 0x06001D91 RID: 7569
		// (set) Token: 0x06001D90 RID: 7568
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

		// Token: 0x06001D92 RID: 7570
		[DispId(-2147417068)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLRectCollection getClientRects();

		// Token: 0x06001D93 RID: 7571
		[DispId(-2147417067)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLRect getBoundingClientRect();

		// Token: 0x06001D94 RID: 7572
		[DispId(-2147417608)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void setExpression([MarshalAs(19)] [In] string propname, [MarshalAs(19)] [In] string expression, [MarshalAs(19)] [In] string language = "");

		// Token: 0x06001D95 RID: 7573
		[DispId(-2147417607)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		public virtual extern object getExpression([MarshalAs(19)] [In] string propname);

		// Token: 0x06001D96 RID: 7574
		[DispId(-2147417606)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool removeExpression([MarshalAs(19)] [In] string propname);

		// Token: 0x17000BAD RID: 2989
		// (get) Token: 0x06001D98 RID: 7576
		// (set) Token: 0x06001D97 RID: 7575
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

		// Token: 0x06001D99 RID: 7577
		[DispId(-2147416112)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void focus();

		// Token: 0x17000BAE RID: 2990
		// (get) Token: 0x06001D9B RID: 7579
		// (set) Token: 0x06001D9A RID: 7578
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

		// Token: 0x17000BAF RID: 2991
		// (get) Token: 0x06001D9D RID: 7581
		// (set) Token: 0x06001D9C RID: 7580
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

		// Token: 0x17000BB0 RID: 2992
		// (get) Token: 0x06001D9F RID: 7583
		// (set) Token: 0x06001D9E RID: 7582
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

		// Token: 0x17000BB1 RID: 2993
		// (get) Token: 0x06001DA1 RID: 7585
		// (set) Token: 0x06001DA0 RID: 7584
		[DispId(-2147412076)]
		public virtual extern object onresize
		{
			[TypeLibFunc(20)]
			[DispId(-2147412076)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412076)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x06001DA2 RID: 7586
		[DispId(-2147416110)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void blur();

		// Token: 0x06001DA3 RID: 7587
		[DispId(-2147416095)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void addFilter([MarshalAs(25)] [In] object pUnk);

		// Token: 0x06001DA4 RID: 7588
		[DispId(-2147416094)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void removeFilter([MarshalAs(25)] [In] object pUnk);

		// Token: 0x17000BB2 RID: 2994
		// (get) Token: 0x06001DA5 RID: 7589
		[DispId(-2147416093)]
		public virtual extern int clientHeight
		{
			[TypeLibFunc(20)]
			[DispId(-2147416093)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17000BB3 RID: 2995
		// (get) Token: 0x06001DA6 RID: 7590
		[DispId(-2147416092)]
		public virtual extern int clientWidth
		{
			[DispId(-2147416092)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17000BB4 RID: 2996
		// (get) Token: 0x06001DA7 RID: 7591
		[DispId(-2147416091)]
		public virtual extern int clientTop
		{
			[DispId(-2147416091)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17000BB5 RID: 2997
		// (get) Token: 0x06001DA8 RID: 7592
		[DispId(-2147416090)]
		public virtual extern int clientLeft
		{
			[TypeLibFunc(20)]
			[DispId(-2147416090)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x06001DA9 RID: 7593
		[DispId(-2147417605)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool attachEvent([MarshalAs(19)] [In] string @event, [MarshalAs(26)] [In] object pdisp);

		// Token: 0x06001DAA RID: 7594
		[DispId(-2147417604)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void detachEvent([MarshalAs(19)] [In] string @event, [MarshalAs(26)] [In] object pdisp);

		// Token: 0x17000BB6 RID: 2998
		// (get) Token: 0x06001DAB RID: 7595
		[DispId(-2147412996)]
		public virtual extern object readyState
		{
			[DispId(-2147412996)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x17000BB7 RID: 2999
		// (get) Token: 0x06001DAD RID: 7597
		// (set) Token: 0x06001DAC RID: 7596
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

		// Token: 0x17000BB8 RID: 3000
		// (get) Token: 0x06001DAF RID: 7599
		// (set) Token: 0x06001DAE RID: 7598
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

		// Token: 0x17000BB9 RID: 3001
		// (get) Token: 0x06001DB1 RID: 7601
		// (set) Token: 0x06001DB0 RID: 7600
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

		// Token: 0x17000BBA RID: 3002
		// (get) Token: 0x06001DB3 RID: 7603
		// (set) Token: 0x06001DB2 RID: 7602
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

		// Token: 0x17000BBB RID: 3003
		// (get) Token: 0x06001DB5 RID: 7605
		// (set) Token: 0x06001DB4 RID: 7604
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

		// Token: 0x06001DB6 RID: 7606
		[DispId(-2147417056)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(26)]
		public virtual extern object createControlRange();

		// Token: 0x17000BBC RID: 3004
		// (get) Token: 0x06001DB7 RID: 7607
		[DispId(-2147417055)]
		public virtual extern int scrollHeight
		{
			[TypeLibFunc(20)]
			[DispId(-2147417055)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17000BBD RID: 3005
		// (get) Token: 0x06001DB8 RID: 7608
		[DispId(-2147417054)]
		public virtual extern int scrollWidth
		{
			[TypeLibFunc(20)]
			[DispId(-2147417054)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17000BBE RID: 3006
		// (get) Token: 0x06001DBA RID: 7610
		// (set) Token: 0x06001DB9 RID: 7609
		[DispId(-2147417053)]
		public virtual extern int scrollTop
		{
			[TypeLibFunc(20)]
			[DispId(-2147417053)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[DispId(-2147417053)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x17000BBF RID: 3007
		// (get) Token: 0x06001DBC RID: 7612
		// (set) Token: 0x06001DBB RID: 7611
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

		// Token: 0x06001DBD RID: 7613
		[DispId(-2147417050)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void clearAttributes();

		// Token: 0x17000BC0 RID: 3008
		// (get) Token: 0x06001DBF RID: 7615
		// (set) Token: 0x06001DBE RID: 7614
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

		// Token: 0x06001DC0 RID: 7616
		[DispId(-2147417043)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElement insertAdjacentElement([MarshalAs(19)] [In] string where, [MarshalAs(28)] [In] IHTMLElement insertedElement);

		// Token: 0x06001DC1 RID: 7617
		[DispId(-2147417047)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElement applyElement([MarshalAs(28)] [In] IHTMLElement apply, [MarshalAs(19)] [In] string where);

		// Token: 0x06001DC2 RID: 7618
		[DispId(-2147417042)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string getAdjacentText([MarshalAs(19)] [In] string where);

		// Token: 0x06001DC3 RID: 7619
		[DispId(-2147417041)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string replaceAdjacentText([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string newText);

		// Token: 0x17000BC1 RID: 3009
		// (get) Token: 0x06001DC4 RID: 7620
		[DispId(-2147417040)]
		public virtual extern bool canHaveChildren
		{
			[DispId(-2147417040)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x06001DC5 RID: 7621
		[DispId(-2147417032)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern int addBehavior([MarshalAs(19)] [In] string bstrUrl, [MarshalAs(27)] [In] [Optional] ref object pvarFactory);

		// Token: 0x06001DC6 RID: 7622
		[DispId(-2147417031)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool removeBehavior([In] int cookie);

		// Token: 0x17000BC2 RID: 3010
		// (get) Token: 0x06001DC7 RID: 7623
		[DispId(-2147417048)]
		public virtual extern IHTMLStyle runtimeStyle
		{
			[TypeLibFunc(1024)]
			[DispId(-2147417048)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17000BC3 RID: 3011
		// (get) Token: 0x06001DC8 RID: 7624
		[DispId(-2147417030)]
		public virtual extern object behaviorUrns
		{
			[DispId(-2147417030)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17000BC4 RID: 3012
		// (get) Token: 0x06001DCA RID: 7626
		// (set) Token: 0x06001DC9 RID: 7625
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

		// Token: 0x17000BC5 RID: 3013
		// (get) Token: 0x06001DCC RID: 7628
		// (set) Token: 0x06001DCB RID: 7627
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

		// Token: 0x17000BC6 RID: 3014
		// (get) Token: 0x06001DCD RID: 7629
		[DispId(-2147417028)]
		public virtual extern int readyStateValue
		{
			[TypeLibFunc(65)]
			[DispId(-2147417028)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x06001DCE RID: 7630
		[DispId(-2147417027)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElementCollection getElementsByTagName([MarshalAs(19)] [In] string v);

		// Token: 0x06001DCF RID: 7631
		[DispId(-2147417016)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void mergeAttributes([MarshalAs(28)] [In] IHTMLElement mergeThis, [MarshalAs(27)] [In] [Optional] ref object pvarFlags);

		// Token: 0x17000BC7 RID: 3015
		// (get) Token: 0x06001DD0 RID: 7632
		[DispId(-2147417015)]
		public virtual extern bool isMultiLine
		{
			[DispId(-2147417015)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17000BC8 RID: 3016
		// (get) Token: 0x06001DD1 RID: 7633
		[DispId(-2147417014)]
		public virtual extern bool canHaveHTML
		{
			[DispId(-2147417014)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17000BC9 RID: 3017
		// (get) Token: 0x06001DD3 RID: 7635
		// (set) Token: 0x06001DD2 RID: 7634
		[DispId(-2147412039)]
		public virtual extern object onlayoutcomplete
		{
			[TypeLibFunc(20)]
			[DispId(-2147412039)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412039)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17000BCA RID: 3018
		// (get) Token: 0x06001DD5 RID: 7637
		// (set) Token: 0x06001DD4 RID: 7636
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

		// Token: 0x17000BCB RID: 3019
		// (get) Token: 0x06001DD7 RID: 7639
		// (set) Token: 0x06001DD6 RID: 7638
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

		// Token: 0x17000BCC RID: 3020
		// (get) Token: 0x06001DD9 RID: 7641
		// (set) Token: 0x06001DD8 RID: 7640
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

		// Token: 0x06001DDA RID: 7642
		[DispId(-2147417011)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void setActive();

		// Token: 0x17000BCD RID: 3021
		// (get) Token: 0x06001DDC RID: 7644
		// (set) Token: 0x06001DDB RID: 7643
		[DispId(-2147412950)]
		public virtual extern string contentEditable
		{
			[DispId(-2147412950)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412950)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17000BCE RID: 3022
		// (get) Token: 0x06001DDD RID: 7645
		[DispId(-2147417010)]
		public virtual extern bool isContentEditable
		{
			[DispId(-2147417010)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17000BCF RID: 3023
		// (get) Token: 0x06001DDF RID: 7647
		// (set) Token: 0x06001DDE RID: 7646
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

		// Token: 0x17000BD0 RID: 3024
		// (get) Token: 0x06001DE1 RID: 7649
		// (set) Token: 0x06001DE0 RID: 7648
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

		// Token: 0x17000BD1 RID: 3025
		// (get) Token: 0x06001DE2 RID: 7650
		[DispId(-2147417007)]
		public virtual extern bool isDisabled
		{
			[DispId(-2147417007)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17000BD2 RID: 3026
		// (get) Token: 0x06001DE4 RID: 7652
		// (set) Token: 0x06001DE3 RID: 7651
		[DispId(-2147412034)]
		public virtual extern object onmove
		{
			[DispId(-2147412034)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412034)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17000BD3 RID: 3027
		// (get) Token: 0x06001DE6 RID: 7654
		// (set) Token: 0x06001DE5 RID: 7653
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

		// Token: 0x06001DE7 RID: 7655
		[DispId(-2147417006)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool FireEvent([MarshalAs(19)] [In] string bstrEventName, [MarshalAs(27)] [In] [Optional] ref object pvarEventObject);

		// Token: 0x17000BD4 RID: 3028
		// (get) Token: 0x06001DE9 RID: 7657
		// (set) Token: 0x06001DE8 RID: 7656
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

		// Token: 0x17000BD5 RID: 3029
		// (get) Token: 0x06001DEB RID: 7659
		// (set) Token: 0x06001DEA RID: 7658
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

		// Token: 0x17000BD6 RID: 3030
		// (get) Token: 0x06001DED RID: 7661
		// (set) Token: 0x06001DEC RID: 7660
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

		// Token: 0x17000BD7 RID: 3031
		// (get) Token: 0x06001DEF RID: 7663
		// (set) Token: 0x06001DEE RID: 7662
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

		// Token: 0x17000BD8 RID: 3032
		// (get) Token: 0x06001DF1 RID: 7665
		// (set) Token: 0x06001DF0 RID: 7664
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

		// Token: 0x17000BD9 RID: 3033
		// (get) Token: 0x06001DF3 RID: 7667
		// (set) Token: 0x06001DF2 RID: 7666
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

		// Token: 0x17000BDA RID: 3034
		// (get) Token: 0x06001DF5 RID: 7669
		// (set) Token: 0x06001DF4 RID: 7668
		[DispId(-2147412025)]
		public virtual extern object onactivate
		{
			[DispId(-2147412025)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412025)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17000BDB RID: 3035
		// (get) Token: 0x06001DF7 RID: 7671
		// (set) Token: 0x06001DF6 RID: 7670
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

		// Token: 0x06001DF8 RID: 7672
		[DispId(-2147417005)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool dragDrop();

		// Token: 0x17000BDC RID: 3036
		// (get) Token: 0x06001DF9 RID: 7673
		[DispId(-2147417004)]
		public virtual extern int glyphMode
		{
			[DispId(-2147417004)]
			[TypeLibFunc(1089)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17000BDD RID: 3037
		// (get) Token: 0x06001DFB RID: 7675
		// (set) Token: 0x06001DFA RID: 7674
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

		// Token: 0x06001DFC RID: 7676
		[DispId(-2147417000)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void normalize();

		// Token: 0x06001DFD RID: 7677
		[DispId(-2147417003)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute getAttributeNode([MarshalAs(19)] [In] string bstrName);

		// Token: 0x06001DFE RID: 7678
		[DispId(-2147417002)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute setAttributeNode([MarshalAs(28)] [In] IHTMLDOMAttribute pattr);

		// Token: 0x06001DFF RID: 7679
		[DispId(-2147417001)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute removeAttributeNode([MarshalAs(28)] [In] IHTMLDOMAttribute pattr);

		// Token: 0x17000BDE RID: 3038
		// (get) Token: 0x06001E01 RID: 7681
		// (set) Token: 0x06001E00 RID: 7680
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

		// Token: 0x17000BDF RID: 3039
		// (get) Token: 0x06001E03 RID: 7683
		// (set) Token: 0x06001E02 RID: 7682
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

		// Token: 0x17000BE0 RID: 3040
		// (get) Token: 0x06001E05 RID: 7685
		// (set) Token: 0x06001E04 RID: 7684
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

		// Token: 0x17000BE1 RID: 3041
		// (get) Token: 0x06001E06 RID: 7686
		[DispId(-2147417058)]
		public virtual extern int uniqueNumber
		{
			[TypeLibFunc(64)]
			[DispId(-2147417058)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17000BE2 RID: 3042
		// (get) Token: 0x06001E07 RID: 7687
		[DispId(-2147417057)]
		public virtual extern string uniqueID
		{
			[TypeLibFunc(64)]
			[DispId(-2147417057)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17000BE3 RID: 3043
		// (get) Token: 0x06001E08 RID: 7688
		[DispId(-2147417066)]
		public virtual extern int nodeType
		{
			[DispId(-2147417066)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17000BE4 RID: 3044
		// (get) Token: 0x06001E09 RID: 7689
		[DispId(-2147417065)]
		public virtual extern IHTMLDOMNode parentNode
		{
			[DispId(-2147417065)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x06001E0A RID: 7690
		[DispId(-2147417064)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool hasChildNodes();

		// Token: 0x17000BE5 RID: 3045
		// (get) Token: 0x06001E0B RID: 7691
		[DispId(-2147417063)]
		public virtual extern object childNodes
		{
			[DispId(-2147417063)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17000BE6 RID: 3046
		// (get) Token: 0x06001E0C RID: 7692
		[DispId(-2147417062)]
		public virtual extern object attributes
		{
			[DispId(-2147417062)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x06001E0D RID: 7693
		[DispId(-2147417061)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode insertBefore([MarshalAs(28)] [In] IHTMLDOMNode newChild, [MarshalAs(27)] [In] [Optional] object refChild);

		// Token: 0x06001E0E RID: 7694
		[DispId(-2147417060)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode removeChild([MarshalAs(28)] [In] IHTMLDOMNode oldChild);

		// Token: 0x06001E0F RID: 7695
		[DispId(-2147417059)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode replaceChild([MarshalAs(28)] [In] IHTMLDOMNode newChild, [MarshalAs(28)] [In] IHTMLDOMNode oldChild);

		// Token: 0x06001E10 RID: 7696
		[DispId(-2147417051)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode cloneNode([In] bool fDeep);

		// Token: 0x06001E11 RID: 7697
		[DispId(-2147417046)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode removeNode([In] bool fDeep = false);

		// Token: 0x06001E12 RID: 7698
		[DispId(-2147417044)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode swapNode([MarshalAs(28)] [In] IHTMLDOMNode otherNode);

		// Token: 0x06001E13 RID: 7699
		[DispId(-2147417045)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode replaceNode([MarshalAs(28)] [In] IHTMLDOMNode replacement);

		// Token: 0x06001E14 RID: 7700
		[DispId(-2147417039)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode appendChild([MarshalAs(28)] [In] IHTMLDOMNode newChild);

		// Token: 0x17000BE7 RID: 3047
		// (get) Token: 0x06001E15 RID: 7701
		[DispId(-2147417038)]
		public virtual extern string nodeName
		{
			[DispId(-2147417038)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17000BE8 RID: 3048
		// (get) Token: 0x06001E17 RID: 7703
		// (set) Token: 0x06001E16 RID: 7702
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

		// Token: 0x17000BE9 RID: 3049
		// (get) Token: 0x06001E18 RID: 7704
		[DispId(-2147417036)]
		public virtual extern IHTMLDOMNode firstChild
		{
			[DispId(-2147417036)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17000BEA RID: 3050
		// (get) Token: 0x06001E19 RID: 7705
		[DispId(-2147417035)]
		public virtual extern IHTMLDOMNode lastChild
		{
			[DispId(-2147417035)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17000BEB RID: 3051
		// (get) Token: 0x06001E1A RID: 7706
		[DispId(-2147417034)]
		public virtual extern IHTMLDOMNode previousSibling
		{
			[DispId(-2147417034)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17000BEC RID: 3052
		// (get) Token: 0x06001E1B RID: 7707
		[DispId(-2147417033)]
		public virtual extern IHTMLDOMNode nextSibling
		{
			[DispId(-2147417033)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17000BED RID: 3053
		// (get) Token: 0x06001E1C RID: 7708
		[DispId(-2147416999)]
		public virtual extern object ownerDocument
		{
			[DispId(-2147416999)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17000BEE RID: 3054
		// (get) Token: 0x06001E1E RID: 7710
		// (set) Token: 0x06001E1D RID: 7709
		[DispId(1001)]
		public virtual extern string action
		{
			[TypeLibFunc(20)]
			[DispId(1001)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(1001)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17000BEF RID: 3055
		// (get) Token: 0x06001E20 RID: 7712
		// (set) Token: 0x06001E1F RID: 7711
		[DispId(1003)]
		public virtual extern string encoding
		{
			[DispId(1003)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(1003)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17000BF0 RID: 3056
		// (get) Token: 0x06001E22 RID: 7714
		// (set) Token: 0x06001E21 RID: 7713
		[DispId(1004)]
		public virtual extern string method
		{
			[TypeLibFunc(20)]
			[DispId(1004)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(1004)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17000BF1 RID: 3057
		// (get) Token: 0x06001E23 RID: 7715
		[DispId(1005)]
		public virtual extern object elements
		{
			[DispId(1005)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17000BF2 RID: 3058
		// (get) Token: 0x06001E25 RID: 7717
		// (set) Token: 0x06001E24 RID: 7716
		[DispId(1006)]
		public virtual extern string target
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

		// Token: 0x17000BF3 RID: 3059
		// (get) Token: 0x06001E27 RID: 7719
		// (set) Token: 0x06001E26 RID: 7718
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

		// Token: 0x17000BF4 RID: 3060
		// (get) Token: 0x06001E29 RID: 7721
		// (set) Token: 0x06001E28 RID: 7720
		[DispId(-2147412101)]
		public virtual extern object onsubmit
		{
			[DispId(-2147412101)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412101)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17000BF5 RID: 3061
		// (get) Token: 0x06001E2B RID: 7723
		// (set) Token: 0x06001E2A RID: 7722
		[DispId(-2147412100)]
		public virtual extern object onreset
		{
			[TypeLibFunc(20)]
			[DispId(-2147412100)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412100)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x06001E2C RID: 7724
		[DispId(1009)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void submit();

		// Token: 0x06001E2D RID: 7725
		[DispId(1010)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void reset();

		// Token: 0x17000BF6 RID: 3062
		// (get) Token: 0x06001E2F RID: 7727
		// (set) Token: 0x06001E2E RID: 7726
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

		// Token: 0x06001E30 RID: 7728
		[TypeLibFunc(65)]
		[DispId(-4)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(44, MarshalTypeRef = System.Runtime.InteropServices.CustomMarshalers.EnumeratorToEnumVariantMarshaler)]
		public virtual extern IEnumerator GetEnumerator();

		// Token: 0x06001E31 RID: 7729
		[DispId(0)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(26)]
		public virtual extern object item([MarshalAs(27)] [In] [Optional] object name, [MarshalAs(27)] [In] [Optional] object index);

		// Token: 0x06001E32 RID: 7730
		[DispId(1502)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(26)]
		public virtual extern object tags([MarshalAs(27)] [In] object tagName);

		// Token: 0x17000BF7 RID: 3063
		// (get) Token: 0x06001E34 RID: 7732
		// (set) Token: 0x06001E33 RID: 7731
		[DispId(1011)]
		public virtual extern string acceptCharset
		{
			[TypeLibFunc(20)]
			[DispId(1011)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(1011)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x06001E35 RID: 7733
		[DispId(1505)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(26)]
		public virtual extern object urns([MarshalAs(27)] [In] object urn);

		// Token: 0x06001E36 RID: 7734
		[DispId(1506)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(26)]
		public virtual extern object namedItem([MarshalAs(19)] [In] string name);

		// Token: 0x06001E37 RID: 7735
		[DispId(1012)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void appendNameValuePair([MarshalAs(19)] [In] string name = "", [MarshalAs(19)] [In] string value = "");

		// Token: 0x06001E38 RID: 7736
		[DispId(1013)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void appendNameFilePair([MarshalAs(19)] [In] string name = "", [MarshalAs(19)] [In] string filename = "");

		// Token: 0x06001E39 RID: 7737
		[DispId(1014)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void appendItemSeparator();

		// Token: 0x06001E3A RID: 7738
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement_setAttribute([MarshalAs(19)] [In] string strAttributeName, [MarshalAs(27)] [In] object AttributeValue, [In] int lFlags = 1);

		// Token: 0x06001E3B RID: 7739
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		public virtual extern object IHTMLElement_getAttribute([MarshalAs(19)] [In] string strAttributeName, [In] int lFlags = 0);

		// Token: 0x06001E3C RID: 7740
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement_removeAttribute([MarshalAs(19)] [In] string strAttributeName, [In] int lFlags = 1);

		// Token: 0x17000BF8 RID: 3064
		// (get) Token: 0x06001E3E RID: 7742
		// (set) Token: 0x06001E3D RID: 7741
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

		// Token: 0x17000BF9 RID: 3065
		// (get) Token: 0x06001E40 RID: 7744
		// (set) Token: 0x06001E3F RID: 7743
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

		// Token: 0x17000BFA RID: 3066
		// (get) Token: 0x06001E41 RID: 7745
		public virtual extern string IHTMLElement_tagName
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17000BFB RID: 3067
		// (get) Token: 0x06001E42 RID: 7746
		public virtual extern IHTMLElement IHTMLElement_parentElement
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17000BFC RID: 3068
		// (get) Token: 0x06001E43 RID: 7747
		public virtual extern IHTMLStyle IHTMLElement_style
		{
			[TypeLibFunc(1024)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17000BFD RID: 3069
		// (get) Token: 0x06001E45 RID: 7749
		// (set) Token: 0x06001E44 RID: 7748
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

		// Token: 0x17000BFE RID: 3070
		// (get) Token: 0x06001E47 RID: 7751
		// (set) Token: 0x06001E46 RID: 7750
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

		// Token: 0x17000BFF RID: 3071
		// (get) Token: 0x06001E49 RID: 7753
		// (set) Token: 0x06001E48 RID: 7752
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

		// Token: 0x17000C00 RID: 3072
		// (get) Token: 0x06001E4B RID: 7755
		// (set) Token: 0x06001E4A RID: 7754
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

		// Token: 0x17000C01 RID: 3073
		// (get) Token: 0x06001E4D RID: 7757
		// (set) Token: 0x06001E4C RID: 7756
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

		// Token: 0x17000C02 RID: 3074
		// (get) Token: 0x06001E4F RID: 7759
		// (set) Token: 0x06001E4E RID: 7758
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

		// Token: 0x17000C03 RID: 3075
		// (get) Token: 0x06001E51 RID: 7761
		// (set) Token: 0x06001E50 RID: 7760
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

		// Token: 0x17000C04 RID: 3076
		// (get) Token: 0x06001E53 RID: 7763
		// (set) Token: 0x06001E52 RID: 7762
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

		// Token: 0x17000C05 RID: 3077
		// (get) Token: 0x06001E55 RID: 7765
		// (set) Token: 0x06001E54 RID: 7764
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

		// Token: 0x17000C06 RID: 3078
		// (get) Token: 0x06001E57 RID: 7767
		// (set) Token: 0x06001E56 RID: 7766
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

		// Token: 0x17000C07 RID: 3079
		// (get) Token: 0x06001E59 RID: 7769
		// (set) Token: 0x06001E58 RID: 7768
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

		// Token: 0x17000C08 RID: 3080
		// (get) Token: 0x06001E5A RID: 7770
		public virtual extern object IHTMLElement_document
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17000C09 RID: 3081
		// (get) Token: 0x06001E5C RID: 7772
		// (set) Token: 0x06001E5B RID: 7771
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

		// Token: 0x17000C0A RID: 3082
		// (get) Token: 0x06001E5E RID: 7774
		// (set) Token: 0x06001E5D RID: 7773
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

		// Token: 0x17000C0B RID: 3083
		// (get) Token: 0x06001E60 RID: 7776
		// (set) Token: 0x06001E5F RID: 7775
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

		// Token: 0x06001E61 RID: 7777
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement_scrollIntoView([MarshalAs(27)] [In] [Optional] object varargStart);

		// Token: 0x06001E62 RID: 7778
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement_contains([MarshalAs(28)] [In] IHTMLElement pChild);

		// Token: 0x17000C0C RID: 3084
		// (get) Token: 0x06001E63 RID: 7779
		public virtual extern int IHTMLElement_sourceIndex
		{
			[TypeLibFunc(4)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17000C0D RID: 3085
		// (get) Token: 0x06001E64 RID: 7780
		public virtual extern object IHTMLElement_recordNumber
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x17000C0E RID: 3086
		// (get) Token: 0x06001E66 RID: 7782
		// (set) Token: 0x06001E65 RID: 7781
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

		// Token: 0x17000C0F RID: 3087
		// (get) Token: 0x06001E67 RID: 7783
		public virtual extern int IHTMLElement_offsetLeft
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17000C10 RID: 3088
		// (get) Token: 0x06001E68 RID: 7784
		public virtual extern int IHTMLElement_offsetTop
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17000C11 RID: 3089
		// (get) Token: 0x06001E69 RID: 7785
		public virtual extern int IHTMLElement_offsetWidth
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17000C12 RID: 3090
		// (get) Token: 0x06001E6A RID: 7786
		public virtual extern int IHTMLElement_offsetHeight
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17000C13 RID: 3091
		// (get) Token: 0x06001E6B RID: 7787
		public virtual extern IHTMLElement IHTMLElement_offsetParent
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17000C14 RID: 3092
		// (get) Token: 0x06001E6D RID: 7789
		// (set) Token: 0x06001E6C RID: 7788
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

		// Token: 0x17000C15 RID: 3093
		// (get) Token: 0x06001E6F RID: 7791
		// (set) Token: 0x06001E6E RID: 7790
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

		// Token: 0x17000C16 RID: 3094
		// (get) Token: 0x06001E71 RID: 7793
		// (set) Token: 0x06001E70 RID: 7792
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

		// Token: 0x17000C17 RID: 3095
		// (get) Token: 0x06001E73 RID: 7795
		// (set) Token: 0x06001E72 RID: 7794
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

		// Token: 0x06001E74 RID: 7796
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement_insertAdjacentHTML([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string html);

		// Token: 0x06001E75 RID: 7797
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement_insertAdjacentText([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string text);

		// Token: 0x17000C18 RID: 3096
		// (get) Token: 0x06001E76 RID: 7798
		public virtual extern IHTMLElement IHTMLElement_parentTextEdit
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17000C19 RID: 3097
		// (get) Token: 0x06001E77 RID: 7799
		public virtual extern bool IHTMLElement_isTextEdit
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x06001E78 RID: 7800
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement_click();

		// Token: 0x17000C1A RID: 3098
		// (get) Token: 0x06001E79 RID: 7801
		public virtual extern IHTMLFiltersCollection IHTMLElement_filters
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17000C1B RID: 3099
		// (get) Token: 0x06001E7B RID: 7803
		// (set) Token: 0x06001E7A RID: 7802
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

		// Token: 0x06001E7C RID: 7804
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string IHTMLElement_toString();

		// Token: 0x17000C1C RID: 3100
		// (get) Token: 0x06001E7E RID: 7806
		// (set) Token: 0x06001E7D RID: 7805
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

		// Token: 0x17000C1D RID: 3101
		// (get) Token: 0x06001E80 RID: 7808
		// (set) Token: 0x06001E7F RID: 7807
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

		// Token: 0x17000C1E RID: 3102
		// (get) Token: 0x06001E82 RID: 7810
		// (set) Token: 0x06001E81 RID: 7809
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

		// Token: 0x17000C1F RID: 3103
		// (get) Token: 0x06001E84 RID: 7812
		// (set) Token: 0x06001E83 RID: 7811
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

		// Token: 0x17000C20 RID: 3104
		// (get) Token: 0x06001E86 RID: 7814
		// (set) Token: 0x06001E85 RID: 7813
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

		// Token: 0x17000C21 RID: 3105
		// (get) Token: 0x06001E88 RID: 7816
		// (set) Token: 0x06001E87 RID: 7815
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

		// Token: 0x17000C22 RID: 3106
		// (get) Token: 0x06001E8A RID: 7818
		// (set) Token: 0x06001E89 RID: 7817
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

		// Token: 0x17000C23 RID: 3107
		// (get) Token: 0x06001E8C RID: 7820
		// (set) Token: 0x06001E8B RID: 7819
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

		// Token: 0x17000C24 RID: 3108
		// (get) Token: 0x06001E8E RID: 7822
		// (set) Token: 0x06001E8D RID: 7821
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

		// Token: 0x17000C25 RID: 3109
		// (get) Token: 0x06001E8F RID: 7823
		public virtual extern object IHTMLElement_children
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17000C26 RID: 3110
		// (get) Token: 0x06001E90 RID: 7824
		public virtual extern object IHTMLElement_all
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17000C27 RID: 3111
		// (get) Token: 0x06001E91 RID: 7825
		public virtual extern string IHTMLElement2_scopeName
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x06001E92 RID: 7826
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_setCapture([In] bool containerCapture = true);

		// Token: 0x06001E93 RID: 7827
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_releaseCapture();

		// Token: 0x17000C28 RID: 3112
		// (get) Token: 0x06001E95 RID: 7829
		// (set) Token: 0x06001E94 RID: 7828
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

		// Token: 0x06001E96 RID: 7830
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string IHTMLElement2_componentFromPoint([In] int x, [In] int y);

		// Token: 0x06001E97 RID: 7831
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_doScroll([MarshalAs(27)] [In] [Optional] object component);

		// Token: 0x17000C29 RID: 3113
		// (get) Token: 0x06001E99 RID: 7833
		// (set) Token: 0x06001E98 RID: 7832
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

		// Token: 0x17000C2A RID: 3114
		// (get) Token: 0x06001E9B RID: 7835
		// (set) Token: 0x06001E9A RID: 7834
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

		// Token: 0x17000C2B RID: 3115
		// (get) Token: 0x06001E9D RID: 7837
		// (set) Token: 0x06001E9C RID: 7836
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

		// Token: 0x17000C2C RID: 3116
		// (get) Token: 0x06001E9F RID: 7839
		// (set) Token: 0x06001E9E RID: 7838
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

		// Token: 0x17000C2D RID: 3117
		// (get) Token: 0x06001EA1 RID: 7841
		// (set) Token: 0x06001EA0 RID: 7840
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

		// Token: 0x17000C2E RID: 3118
		// (get) Token: 0x06001EA3 RID: 7843
		// (set) Token: 0x06001EA2 RID: 7842
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

		// Token: 0x17000C2F RID: 3119
		// (get) Token: 0x06001EA5 RID: 7845
		// (set) Token: 0x06001EA4 RID: 7844
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

		// Token: 0x17000C30 RID: 3120
		// (get) Token: 0x06001EA7 RID: 7847
		// (set) Token: 0x06001EA6 RID: 7846
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

		// Token: 0x17000C31 RID: 3121
		// (get) Token: 0x06001EA9 RID: 7849
		// (set) Token: 0x06001EA8 RID: 7848
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

		// Token: 0x17000C32 RID: 3122
		// (get) Token: 0x06001EAB RID: 7851
		// (set) Token: 0x06001EAA RID: 7850
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

		// Token: 0x17000C33 RID: 3123
		// (get) Token: 0x06001EAD RID: 7853
		// (set) Token: 0x06001EAC RID: 7852
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

		// Token: 0x17000C34 RID: 3124
		// (get) Token: 0x06001EAF RID: 7855
		// (set) Token: 0x06001EAE RID: 7854
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

		// Token: 0x17000C35 RID: 3125
		// (get) Token: 0x06001EB1 RID: 7857
		// (set) Token: 0x06001EB0 RID: 7856
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

		// Token: 0x17000C36 RID: 3126
		// (get) Token: 0x06001EB2 RID: 7858
		public virtual extern IHTMLCurrentStyle IHTMLElement2_currentStyle
		{
			[TypeLibFunc(1024)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17000C37 RID: 3127
		// (get) Token: 0x06001EB4 RID: 7860
		// (set) Token: 0x06001EB3 RID: 7859
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

		// Token: 0x06001EB5 RID: 7861
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLRectCollection IHTMLElement2_getClientRects();

		// Token: 0x06001EB6 RID: 7862
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLRect IHTMLElement2_getBoundingClientRect();

		// Token: 0x06001EB7 RID: 7863
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_setExpression([MarshalAs(19)] [In] string propname, [MarshalAs(19)] [In] string expression, [MarshalAs(19)] [In] string language = "");

		// Token: 0x06001EB8 RID: 7864
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		public virtual extern object IHTMLElement2_getExpression([MarshalAs(19)] [In] string propname);

		// Token: 0x06001EB9 RID: 7865
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement2_removeExpression([MarshalAs(19)] [In] string propname);

		// Token: 0x17000C38 RID: 3128
		// (get) Token: 0x06001EBB RID: 7867
		// (set) Token: 0x06001EBA RID: 7866
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

		// Token: 0x06001EBC RID: 7868
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_focus();

		// Token: 0x17000C39 RID: 3129
		// (get) Token: 0x06001EBE RID: 7870
		// (set) Token: 0x06001EBD RID: 7869
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

		// Token: 0x17000C3A RID: 3130
		// (get) Token: 0x06001EC0 RID: 7872
		// (set) Token: 0x06001EBF RID: 7871
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

		// Token: 0x17000C3B RID: 3131
		// (get) Token: 0x06001EC2 RID: 7874
		// (set) Token: 0x06001EC1 RID: 7873
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

		// Token: 0x17000C3C RID: 3132
		// (get) Token: 0x06001EC4 RID: 7876
		// (set) Token: 0x06001EC3 RID: 7875
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

		// Token: 0x06001EC5 RID: 7877
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_blur();

		// Token: 0x06001EC6 RID: 7878
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_addFilter([MarshalAs(25)] [In] object pUnk);

		// Token: 0x06001EC7 RID: 7879
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_removeFilter([MarshalAs(25)] [In] object pUnk);

		// Token: 0x17000C3D RID: 3133
		// (get) Token: 0x06001EC8 RID: 7880
		public virtual extern int IHTMLElement2_clientHeight
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17000C3E RID: 3134
		// (get) Token: 0x06001EC9 RID: 7881
		public virtual extern int IHTMLElement2_clientWidth
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17000C3F RID: 3135
		// (get) Token: 0x06001ECA RID: 7882
		public virtual extern int IHTMLElement2_clientTop
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17000C40 RID: 3136
		// (get) Token: 0x06001ECB RID: 7883
		public virtual extern int IHTMLElement2_clientLeft
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x06001ECC RID: 7884
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement2_attachEvent([MarshalAs(19)] [In] string @event, [MarshalAs(26)] [In] object pdisp);

		// Token: 0x06001ECD RID: 7885
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_detachEvent([MarshalAs(19)] [In] string @event, [MarshalAs(26)] [In] object pdisp);

		// Token: 0x17000C41 RID: 3137
		// (get) Token: 0x06001ECE RID: 7886
		public virtual extern object IHTMLElement2_readyState
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x17000C42 RID: 3138
		// (get) Token: 0x06001ED0 RID: 7888
		// (set) Token: 0x06001ECF RID: 7887
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

		// Token: 0x17000C43 RID: 3139
		// (get) Token: 0x06001ED2 RID: 7890
		// (set) Token: 0x06001ED1 RID: 7889
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

		// Token: 0x17000C44 RID: 3140
		// (get) Token: 0x06001ED4 RID: 7892
		// (set) Token: 0x06001ED3 RID: 7891
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

		// Token: 0x17000C45 RID: 3141
		// (get) Token: 0x06001ED6 RID: 7894
		// (set) Token: 0x06001ED5 RID: 7893
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

		// Token: 0x17000C46 RID: 3142
		// (get) Token: 0x06001ED8 RID: 7896
		// (set) Token: 0x06001ED7 RID: 7895
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

		// Token: 0x06001ED9 RID: 7897
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(26)]
		public virtual extern object IHTMLElement2_createControlRange();

		// Token: 0x17000C47 RID: 3143
		// (get) Token: 0x06001EDA RID: 7898
		public virtual extern int IHTMLElement2_scrollHeight
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17000C48 RID: 3144
		// (get) Token: 0x06001EDB RID: 7899
		public virtual extern int IHTMLElement2_scrollWidth
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17000C49 RID: 3145
		// (get) Token: 0x06001EDD RID: 7901
		// (set) Token: 0x06001EDC RID: 7900
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

		// Token: 0x17000C4A RID: 3146
		// (get) Token: 0x06001EDF RID: 7903
		// (set) Token: 0x06001EDE RID: 7902
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

		// Token: 0x06001EE0 RID: 7904
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement2_clearAttributes();

		// Token: 0x06001EE1 RID: 7905
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void mergeAttributes([MarshalAs(28)] [In] IHTMLElement mergeThis);

		// Token: 0x17000C4B RID: 3147
		// (get) Token: 0x06001EE3 RID: 7907
		// (set) Token: 0x06001EE2 RID: 7906
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

		// Token: 0x06001EE4 RID: 7908
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElement IHTMLElement2_insertAdjacentElement([MarshalAs(19)] [In] string where, [MarshalAs(28)] [In] IHTMLElement insertedElement);

		// Token: 0x06001EE5 RID: 7909
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElement IHTMLElement2_applyElement([MarshalAs(28)] [In] IHTMLElement apply, [MarshalAs(19)] [In] string where);

		// Token: 0x06001EE6 RID: 7910
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string IHTMLElement2_getAdjacentText([MarshalAs(19)] [In] string where);

		// Token: 0x06001EE7 RID: 7911
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string IHTMLElement2_replaceAdjacentText([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string newText);

		// Token: 0x17000C4C RID: 3148
		// (get) Token: 0x06001EE8 RID: 7912
		public virtual extern bool IHTMLElement2_canHaveChildren
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x06001EE9 RID: 7913
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern int IHTMLElement2_addBehavior([MarshalAs(19)] [In] string bstrUrl, [MarshalAs(27)] [In] [Optional] ref object pvarFactory);

		// Token: 0x06001EEA RID: 7914
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement2_removeBehavior([In] int cookie);

		// Token: 0x17000C4D RID: 3149
		// (get) Token: 0x06001EEB RID: 7915
		public virtual extern IHTMLStyle IHTMLElement2_runtimeStyle
		{
			[TypeLibFunc(1024)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17000C4E RID: 3150
		// (get) Token: 0x06001EEC RID: 7916
		public virtual extern object IHTMLElement2_behaviorUrns
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17000C4F RID: 3151
		// (get) Token: 0x06001EEE RID: 7918
		// (set) Token: 0x06001EED RID: 7917
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

		// Token: 0x17000C50 RID: 3152
		// (get) Token: 0x06001EF0 RID: 7920
		// (set) Token: 0x06001EEF RID: 7919
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

		// Token: 0x17000C51 RID: 3153
		// (get) Token: 0x06001EF1 RID: 7921
		public virtual extern int IHTMLElement2_readyStateValue
		{
			[TypeLibFunc(65)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x06001EF2 RID: 7922
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLElementCollection IHTMLElement2_getElementsByTagName([MarshalAs(19)] [In] string v);

		// Token: 0x06001EF3 RID: 7923
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void mergeAttributes([MarshalAs(28)] [In] IHTMLElement mergeThis, [MarshalAs(27)] [In] [Optional] ref object pvarFlags);

		// Token: 0x17000C52 RID: 3154
		// (get) Token: 0x06001EF4 RID: 7924
		public virtual extern bool IHTMLElement3_isMultiLine
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17000C53 RID: 3155
		// (get) Token: 0x06001EF5 RID: 7925
		public virtual extern bool IHTMLElement3_canHaveHTML
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17000C54 RID: 3156
		// (get) Token: 0x06001EF7 RID: 7927
		// (set) Token: 0x06001EF6 RID: 7926
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

		// Token: 0x17000C55 RID: 3157
		// (get) Token: 0x06001EF9 RID: 7929
		// (set) Token: 0x06001EF8 RID: 7928
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

		// Token: 0x17000C56 RID: 3158
		// (get) Token: 0x06001EFB RID: 7931
		// (set) Token: 0x06001EFA RID: 7930
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

		// Token: 0x17000C57 RID: 3159
		// (get) Token: 0x06001EFD RID: 7933
		// (set) Token: 0x06001EFC RID: 7932
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

		// Token: 0x06001EFE RID: 7934
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement3_setActive();

		// Token: 0x17000C58 RID: 3160
		// (get) Token: 0x06001F00 RID: 7936
		// (set) Token: 0x06001EFF RID: 7935
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

		// Token: 0x17000C59 RID: 3161
		// (get) Token: 0x06001F01 RID: 7937
		public virtual extern bool IHTMLElement3_isContentEditable
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17000C5A RID: 3162
		// (get) Token: 0x06001F03 RID: 7939
		// (set) Token: 0x06001F02 RID: 7938
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

		// Token: 0x17000C5B RID: 3163
		// (get) Token: 0x06001F05 RID: 7941
		// (set) Token: 0x06001F04 RID: 7940
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

		// Token: 0x17000C5C RID: 3164
		// (get) Token: 0x06001F06 RID: 7942
		public virtual extern bool IHTMLElement3_isDisabled
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17000C5D RID: 3165
		// (get) Token: 0x06001F08 RID: 7944
		// (set) Token: 0x06001F07 RID: 7943
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

		// Token: 0x17000C5E RID: 3166
		// (get) Token: 0x06001F0A RID: 7946
		// (set) Token: 0x06001F09 RID: 7945
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

		// Token: 0x06001F0B RID: 7947
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement3_FireEvent([MarshalAs(19)] [In] string bstrEventName, [MarshalAs(27)] [In] [Optional] ref object pvarEventObject);

		// Token: 0x17000C5F RID: 3167
		// (get) Token: 0x06001F0D RID: 7949
		// (set) Token: 0x06001F0C RID: 7948
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

		// Token: 0x17000C60 RID: 3168
		// (get) Token: 0x06001F0F RID: 7951
		// (set) Token: 0x06001F0E RID: 7950
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

		// Token: 0x17000C61 RID: 3169
		// (get) Token: 0x06001F11 RID: 7953
		// (set) Token: 0x06001F10 RID: 7952
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

		// Token: 0x17000C62 RID: 3170
		// (get) Token: 0x06001F13 RID: 7955
		// (set) Token: 0x06001F12 RID: 7954
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

		// Token: 0x17000C63 RID: 3171
		// (get) Token: 0x06001F15 RID: 7957
		// (set) Token: 0x06001F14 RID: 7956
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

		// Token: 0x17000C64 RID: 3172
		// (get) Token: 0x06001F17 RID: 7959
		// (set) Token: 0x06001F16 RID: 7958
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

		// Token: 0x17000C65 RID: 3173
		// (get) Token: 0x06001F19 RID: 7961
		// (set) Token: 0x06001F18 RID: 7960
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

		// Token: 0x17000C66 RID: 3174
		// (get) Token: 0x06001F1B RID: 7963
		// (set) Token: 0x06001F1A RID: 7962
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

		// Token: 0x06001F1C RID: 7964
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement3_dragDrop();

		// Token: 0x17000C67 RID: 3175
		// (get) Token: 0x06001F1D RID: 7965
		public virtual extern int IHTMLElement3_glyphMode
		{
			[TypeLibFunc(1089)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17000C68 RID: 3176
		// (get) Token: 0x06001F1F RID: 7967
		// (set) Token: 0x06001F1E RID: 7966
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

		// Token: 0x06001F20 RID: 7968
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement4_normalize();

		// Token: 0x06001F21 RID: 7969
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute IHTMLElement4_getAttributeNode([MarshalAs(19)] [In] string bstrName);

		// Token: 0x06001F22 RID: 7970
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute IHTMLElement4_setAttributeNode([MarshalAs(28)] [In] IHTMLDOMAttribute pattr);

		// Token: 0x06001F23 RID: 7971
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMAttribute IHTMLElement4_removeAttributeNode([MarshalAs(28)] [In] IHTMLDOMAttribute pattr);

		// Token: 0x17000C69 RID: 3177
		// (get) Token: 0x06001F25 RID: 7973
		// (set) Token: 0x06001F24 RID: 7972
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

		// Token: 0x17000C6A RID: 3178
		// (get) Token: 0x06001F27 RID: 7975
		// (set) Token: 0x06001F26 RID: 7974
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

		// Token: 0x17000C6B RID: 3179
		// (get) Token: 0x06001F29 RID: 7977
		// (set) Token: 0x06001F28 RID: 7976
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

		// Token: 0x17000C6C RID: 3180
		// (get) Token: 0x06001F2A RID: 7978
		public virtual extern int IHTMLUniqueName_uniqueNumber
		{
			[TypeLibFunc(64)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17000C6D RID: 3181
		// (get) Token: 0x06001F2B RID: 7979
		public virtual extern string IHTMLUniqueName_uniqueID
		{
			[TypeLibFunc(64)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17000C6E RID: 3182
		// (get) Token: 0x06001F2C RID: 7980
		public virtual extern int IHTMLDOMNode_nodeType
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17000C6F RID: 3183
		// (get) Token: 0x06001F2D RID: 7981
		public virtual extern IHTMLDOMNode IHTMLDOMNode_parentNode
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x06001F2E RID: 7982
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLDOMNode_hasChildNodes();

		// Token: 0x17000C70 RID: 3184
		// (get) Token: 0x06001F2F RID: 7983
		public virtual extern object IHTMLDOMNode_childNodes
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17000C71 RID: 3185
		// (get) Token: 0x06001F30 RID: 7984
		public virtual extern object IHTMLDOMNode_attributes
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x06001F31 RID: 7985
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_insertBefore([MarshalAs(28)] [In] IHTMLDOMNode newChild, [MarshalAs(27)] [In] [Optional] object refChild);

		// Token: 0x06001F32 RID: 7986
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_removeChild([MarshalAs(28)] [In] IHTMLDOMNode oldChild);

		// Token: 0x06001F33 RID: 7987
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_replaceChild([MarshalAs(28)] [In] IHTMLDOMNode newChild, [MarshalAs(28)] [In] IHTMLDOMNode oldChild);

		// Token: 0x06001F34 RID: 7988
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_cloneNode([In] bool fDeep);

		// Token: 0x06001F35 RID: 7989
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_removeNode([In] bool fDeep = false);

		// Token: 0x06001F36 RID: 7990
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_swapNode([MarshalAs(28)] [In] IHTMLDOMNode otherNode);

		// Token: 0x06001F37 RID: 7991
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_replaceNode([MarshalAs(28)] [In] IHTMLDOMNode replacement);

		// Token: 0x06001F38 RID: 7992
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		public virtual extern IHTMLDOMNode IHTMLDOMNode_appendChild([MarshalAs(28)] [In] IHTMLDOMNode newChild);

		// Token: 0x17000C72 RID: 3186
		// (get) Token: 0x06001F39 RID: 7993
		public virtual extern string IHTMLDOMNode_nodeName
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17000C73 RID: 3187
		// (get) Token: 0x06001F3B RID: 7995
		// (set) Token: 0x06001F3A RID: 7994
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

		// Token: 0x17000C74 RID: 3188
		// (get) Token: 0x06001F3C RID: 7996
		public virtual extern IHTMLDOMNode IHTMLDOMNode_firstChild
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17000C75 RID: 3189
		// (get) Token: 0x06001F3D RID: 7997
		public virtual extern IHTMLDOMNode IHTMLDOMNode_lastChild
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17000C76 RID: 3190
		// (get) Token: 0x06001F3E RID: 7998
		public virtual extern IHTMLDOMNode IHTMLDOMNode_previousSibling
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17000C77 RID: 3191
		// (get) Token: 0x06001F3F RID: 7999
		public virtual extern IHTMLDOMNode IHTMLDOMNode_nextSibling
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17000C78 RID: 3192
		// (get) Token: 0x06001F40 RID: 8000
		public virtual extern object IHTMLDOMNode2_ownerDocument
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17000C79 RID: 3193
		// (get) Token: 0x06001F42 RID: 8002
		// (set) Token: 0x06001F41 RID: 8001
		public virtual extern string IHTMLFormElement_action
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

		// Token: 0x17000C7A RID: 3194
		// (get) Token: 0x06001F44 RID: 8004
		// (set) Token: 0x06001F43 RID: 8003
		public virtual extern string IHTMLFormElement_dir
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

		// Token: 0x17000C7B RID: 3195
		// (get) Token: 0x06001F46 RID: 8006
		// (set) Token: 0x06001F45 RID: 8005
		public virtual extern string IHTMLFormElement_encoding
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

		// Token: 0x17000C7C RID: 3196
		// (get) Token: 0x06001F48 RID: 8008
		// (set) Token: 0x06001F47 RID: 8007
		public virtual extern string IHTMLFormElement_method
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

		// Token: 0x17000C7D RID: 3197
		// (get) Token: 0x06001F49 RID: 8009
		public virtual extern object IHTMLFormElement_elements
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17000C7E RID: 3198
		// (get) Token: 0x06001F4B RID: 8011
		// (set) Token: 0x06001F4A RID: 8010
		public virtual extern string IHTMLFormElement_target
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

		// Token: 0x17000C7F RID: 3199
		// (get) Token: 0x06001F4D RID: 8013
		// (set) Token: 0x06001F4C RID: 8012
		public virtual extern string IHTMLFormElement_name
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

		// Token: 0x17000C80 RID: 3200
		// (get) Token: 0x06001F4F RID: 8015
		// (set) Token: 0x06001F4E RID: 8014
		public virtual extern object IHTMLFormElement_onsubmit
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x17000C81 RID: 3201
		// (get) Token: 0x06001F51 RID: 8017
		// (set) Token: 0x06001F50 RID: 8016
		public virtual extern object IHTMLFormElement_onreset
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			[param: In]
			set;
		}

		// Token: 0x06001F52 RID: 8018
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLFormElement_submit();

		// Token: 0x06001F53 RID: 8019
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLFormElement_reset();

		// Token: 0x17000C82 RID: 3202
		// (get) Token: 0x06001F55 RID: 8021
		// (set) Token: 0x06001F54 RID: 8020
		public virtual extern int IHTMLFormElement_length
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x06001F56 RID: 8022
		[TypeLibFunc(65)]
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(44, MarshalTypeRef = System.Runtime.InteropServices.CustomMarshalers.EnumeratorToEnumVariantMarshaler)]
		public virtual extern IEnumerator IHTMLFormElement_GetEnumerator();

		// Token: 0x06001F57 RID: 8023
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(26)]
		public virtual extern object IHTMLFormElement_item([MarshalAs(27)] [In] [Optional] object name, [MarshalAs(27)] [In] [Optional] object index);

		// Token: 0x06001F58 RID: 8024
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(26)]
		public virtual extern object IHTMLFormElement_tags([MarshalAs(27)] [In] object tagName);

		// Token: 0x17000C83 RID: 3203
		// (get) Token: 0x06001F5A RID: 8026
		// (set) Token: 0x06001F59 RID: 8025
		public virtual extern string IHTMLFormElement2_acceptCharset
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

		// Token: 0x06001F5B RID: 8027
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(26)]
		public virtual extern object IHTMLFormElement2_urns([MarshalAs(27)] [In] object urn);

		// Token: 0x06001F5C RID: 8028
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(26)]
		public virtual extern object IHTMLFormElement3_namedItem([MarshalAs(19)] [In] string name);

		// Token: 0x06001F5D RID: 8029
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLSubmitData_appendNameValuePair([MarshalAs(19)] [In] string name = "", [MarshalAs(19)] [In] string value = "");

		// Token: 0x06001F5E RID: 8030
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLSubmitData_appendNameFilePair([MarshalAs(19)] [In] string name = "", [MarshalAs(19)] [In] string filename = "");

		// Token: 0x06001F5F RID: 8031
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLSubmitData_appendItemSeparator();

		// Token: 0x140001FD RID: 509
		// (add) Token: 0x06001F60 RID: 8032
		// (remove) Token: 0x06001F61 RID: 8033
		public virtual extern event HTMLFormElementEvents_onhelpEventHandler HTMLFormElementEvents_Event_onhelp;

		// Token: 0x140001FE RID: 510
		// (add) Token: 0x06001F62 RID: 8034
		// (remove) Token: 0x06001F63 RID: 8035
		public virtual extern event HTMLFormElementEvents_onclickEventHandler HTMLFormElementEvents_Event_onclick;

		// Token: 0x140001FF RID: 511
		// (add) Token: 0x06001F64 RID: 8036
		// (remove) Token: 0x06001F65 RID: 8037
		public virtual extern event HTMLFormElementEvents_ondblclickEventHandler HTMLFormElementEvents_Event_ondblclick;

		// Token: 0x14000200 RID: 512
		// (add) Token: 0x06001F66 RID: 8038
		// (remove) Token: 0x06001F67 RID: 8039
		public virtual extern event HTMLFormElementEvents_onkeypressEventHandler HTMLFormElementEvents_Event_onkeypress;

		// Token: 0x14000201 RID: 513
		// (add) Token: 0x06001F68 RID: 8040
		// (remove) Token: 0x06001F69 RID: 8041
		public virtual extern event HTMLFormElementEvents_onkeydownEventHandler HTMLFormElementEvents_Event_onkeydown;

		// Token: 0x14000202 RID: 514
		// (add) Token: 0x06001F6A RID: 8042
		// (remove) Token: 0x06001F6B RID: 8043
		public virtual extern event HTMLFormElementEvents_onkeyupEventHandler HTMLFormElementEvents_Event_onkeyup;

		// Token: 0x14000203 RID: 515
		// (add) Token: 0x06001F6C RID: 8044
		// (remove) Token: 0x06001F6D RID: 8045
		public virtual extern event HTMLFormElementEvents_onmouseoutEventHandler HTMLFormElementEvents_Event_onmouseout;

		// Token: 0x14000204 RID: 516
		// (add) Token: 0x06001F6E RID: 8046
		// (remove) Token: 0x06001F6F RID: 8047
		public virtual extern event HTMLFormElementEvents_onmouseoverEventHandler HTMLFormElementEvents_Event_onmouseover;

		// Token: 0x14000205 RID: 517
		// (add) Token: 0x06001F70 RID: 8048
		// (remove) Token: 0x06001F71 RID: 8049
		public virtual extern event HTMLFormElementEvents_onmousemoveEventHandler HTMLFormElementEvents_Event_onmousemove;

		// Token: 0x14000206 RID: 518
		// (add) Token: 0x06001F72 RID: 8050
		// (remove) Token: 0x06001F73 RID: 8051
		public virtual extern event HTMLFormElementEvents_onmousedownEventHandler HTMLFormElementEvents_Event_onmousedown;

		// Token: 0x14000207 RID: 519
		// (add) Token: 0x06001F74 RID: 8052
		// (remove) Token: 0x06001F75 RID: 8053
		public virtual extern event HTMLFormElementEvents_onmouseupEventHandler HTMLFormElementEvents_Event_onmouseup;

		// Token: 0x14000208 RID: 520
		// (add) Token: 0x06001F76 RID: 8054
		// (remove) Token: 0x06001F77 RID: 8055
		public virtual extern event HTMLFormElementEvents_onselectstartEventHandler HTMLFormElementEvents_Event_onselectstart;

		// Token: 0x14000209 RID: 521
		// (add) Token: 0x06001F78 RID: 8056
		// (remove) Token: 0x06001F79 RID: 8057
		public virtual extern event HTMLFormElementEvents_onfilterchangeEventHandler HTMLFormElementEvents_Event_onfilterchange;

		// Token: 0x1400020A RID: 522
		// (add) Token: 0x06001F7A RID: 8058
		// (remove) Token: 0x06001F7B RID: 8059
		public virtual extern event HTMLFormElementEvents_ondragstartEventHandler HTMLFormElementEvents_Event_ondragstart;

		// Token: 0x1400020B RID: 523
		// (add) Token: 0x06001F7C RID: 8060
		// (remove) Token: 0x06001F7D RID: 8061
		public virtual extern event HTMLFormElementEvents_onbeforeupdateEventHandler HTMLFormElementEvents_Event_onbeforeupdate;

		// Token: 0x1400020C RID: 524
		// (add) Token: 0x06001F7E RID: 8062
		// (remove) Token: 0x06001F7F RID: 8063
		public virtual extern event HTMLFormElementEvents_onafterupdateEventHandler HTMLFormElementEvents_Event_onafterupdate;

		// Token: 0x1400020D RID: 525
		// (add) Token: 0x06001F80 RID: 8064
		// (remove) Token: 0x06001F81 RID: 8065
		public virtual extern event HTMLFormElementEvents_onerrorupdateEventHandler HTMLFormElementEvents_Event_onerrorupdate;

		// Token: 0x1400020E RID: 526
		// (add) Token: 0x06001F82 RID: 8066
		// (remove) Token: 0x06001F83 RID: 8067
		public virtual extern event HTMLFormElementEvents_onrowexitEventHandler HTMLFormElementEvents_Event_onrowexit;

		// Token: 0x1400020F RID: 527
		// (add) Token: 0x06001F84 RID: 8068
		// (remove) Token: 0x06001F85 RID: 8069
		public virtual extern event HTMLFormElementEvents_onrowenterEventHandler HTMLFormElementEvents_Event_onrowenter;

		// Token: 0x14000210 RID: 528
		// (add) Token: 0x06001F86 RID: 8070
		// (remove) Token: 0x06001F87 RID: 8071
		public virtual extern event HTMLFormElementEvents_ondatasetchangedEventHandler HTMLFormElementEvents_Event_ondatasetchanged;

		// Token: 0x14000211 RID: 529
		// (add) Token: 0x06001F88 RID: 8072
		// (remove) Token: 0x06001F89 RID: 8073
		public virtual extern event HTMLFormElementEvents_ondataavailableEventHandler HTMLFormElementEvents_Event_ondataavailable;

		// Token: 0x14000212 RID: 530
		// (add) Token: 0x06001F8A RID: 8074
		// (remove) Token: 0x06001F8B RID: 8075
		public virtual extern event HTMLFormElementEvents_ondatasetcompleteEventHandler HTMLFormElementEvents_Event_ondatasetcomplete;

		// Token: 0x14000213 RID: 531
		// (add) Token: 0x06001F8C RID: 8076
		// (remove) Token: 0x06001F8D RID: 8077
		public virtual extern event HTMLFormElementEvents_onlosecaptureEventHandler HTMLFormElementEvents_Event_onlosecapture;

		// Token: 0x14000214 RID: 532
		// (add) Token: 0x06001F8E RID: 8078
		// (remove) Token: 0x06001F8F RID: 8079
		public virtual extern event HTMLFormElementEvents_onpropertychangeEventHandler HTMLFormElementEvents_Event_onpropertychange;

		// Token: 0x14000215 RID: 533
		// (add) Token: 0x06001F90 RID: 8080
		// (remove) Token: 0x06001F91 RID: 8081
		public virtual extern event HTMLFormElementEvents_onscrollEventHandler HTMLFormElementEvents_Event_onscroll;

		// Token: 0x14000216 RID: 534
		// (add) Token: 0x06001F92 RID: 8082
		// (remove) Token: 0x06001F93 RID: 8083
		public virtual extern event HTMLFormElementEvents_onfocusEventHandler HTMLFormElementEvents_Event_onfocus;

		// Token: 0x14000217 RID: 535
		// (add) Token: 0x06001F94 RID: 8084
		// (remove) Token: 0x06001F95 RID: 8085
		public virtual extern event HTMLFormElementEvents_onblurEventHandler HTMLFormElementEvents_Event_onblur;

		// Token: 0x14000218 RID: 536
		// (add) Token: 0x06001F96 RID: 8086
		// (remove) Token: 0x06001F97 RID: 8087
		public virtual extern event HTMLFormElementEvents_onresizeEventHandler HTMLFormElementEvents_Event_onresize;

		// Token: 0x14000219 RID: 537
		// (add) Token: 0x06001F98 RID: 8088
		// (remove) Token: 0x06001F99 RID: 8089
		public virtual extern event HTMLFormElementEvents_ondragEventHandler HTMLFormElementEvents_Event_ondrag;

		// Token: 0x1400021A RID: 538
		// (add) Token: 0x06001F9A RID: 8090
		// (remove) Token: 0x06001F9B RID: 8091
		public virtual extern event HTMLFormElementEvents_ondragendEventHandler HTMLFormElementEvents_Event_ondragend;

		// Token: 0x1400021B RID: 539
		// (add) Token: 0x06001F9C RID: 8092
		// (remove) Token: 0x06001F9D RID: 8093
		public virtual extern event HTMLFormElementEvents_ondragenterEventHandler HTMLFormElementEvents_Event_ondragenter;

		// Token: 0x1400021C RID: 540
		// (add) Token: 0x06001F9E RID: 8094
		// (remove) Token: 0x06001F9F RID: 8095
		public virtual extern event HTMLFormElementEvents_ondragoverEventHandler HTMLFormElementEvents_Event_ondragover;

		// Token: 0x1400021D RID: 541
		// (add) Token: 0x06001FA0 RID: 8096
		// (remove) Token: 0x06001FA1 RID: 8097
		public virtual extern event HTMLFormElementEvents_ondragleaveEventHandler HTMLFormElementEvents_Event_ondragleave;

		// Token: 0x1400021E RID: 542
		// (add) Token: 0x06001FA2 RID: 8098
		// (remove) Token: 0x06001FA3 RID: 8099
		public virtual extern event HTMLFormElementEvents_ondropEventHandler HTMLFormElementEvents_Event_ondrop;

		// Token: 0x1400021F RID: 543
		// (add) Token: 0x06001FA4 RID: 8100
		// (remove) Token: 0x06001FA5 RID: 8101
		public virtual extern event HTMLFormElementEvents_onbeforecutEventHandler HTMLFormElementEvents_Event_onbeforecut;

		// Token: 0x14000220 RID: 544
		// (add) Token: 0x06001FA6 RID: 8102
		// (remove) Token: 0x06001FA7 RID: 8103
		public virtual extern event HTMLFormElementEvents_oncutEventHandler HTMLFormElementEvents_Event_oncut;

		// Token: 0x14000221 RID: 545
		// (add) Token: 0x06001FA8 RID: 8104
		// (remove) Token: 0x06001FA9 RID: 8105
		public virtual extern event HTMLFormElementEvents_onbeforecopyEventHandler HTMLFormElementEvents_Event_onbeforecopy;

		// Token: 0x14000222 RID: 546
		// (add) Token: 0x06001FAA RID: 8106
		// (remove) Token: 0x06001FAB RID: 8107
		public virtual extern event HTMLFormElementEvents_oncopyEventHandler HTMLFormElementEvents_Event_oncopy;

		// Token: 0x14000223 RID: 547
		// (add) Token: 0x06001FAC RID: 8108
		// (remove) Token: 0x06001FAD RID: 8109
		public virtual extern event HTMLFormElementEvents_onbeforepasteEventHandler HTMLFormElementEvents_Event_onbeforepaste;

		// Token: 0x14000224 RID: 548
		// (add) Token: 0x06001FAE RID: 8110
		// (remove) Token: 0x06001FAF RID: 8111
		public virtual extern event HTMLFormElementEvents_onpasteEventHandler HTMLFormElementEvents_Event_onpaste;

		// Token: 0x14000225 RID: 549
		// (add) Token: 0x06001FB0 RID: 8112
		// (remove) Token: 0x06001FB1 RID: 8113
		public virtual extern event HTMLFormElementEvents_oncontextmenuEventHandler HTMLFormElementEvents_Event_oncontextmenu;

		// Token: 0x14000226 RID: 550
		// (add) Token: 0x06001FB2 RID: 8114
		// (remove) Token: 0x06001FB3 RID: 8115
		public virtual extern event HTMLFormElementEvents_onrowsdeleteEventHandler HTMLFormElementEvents_Event_onrowsdelete;

		// Token: 0x14000227 RID: 551
		// (add) Token: 0x06001FB4 RID: 8116
		// (remove) Token: 0x06001FB5 RID: 8117
		public virtual extern event HTMLFormElementEvents_onrowsinsertedEventHandler HTMLFormElementEvents_Event_onrowsinserted;

		// Token: 0x14000228 RID: 552
		// (add) Token: 0x06001FB6 RID: 8118
		// (remove) Token: 0x06001FB7 RID: 8119
		public virtual extern event HTMLFormElementEvents_oncellchangeEventHandler HTMLFormElementEvents_Event_oncellchange;

		// Token: 0x14000229 RID: 553
		// (add) Token: 0x06001FB8 RID: 8120
		// (remove) Token: 0x06001FB9 RID: 8121
		public virtual extern event HTMLFormElementEvents_onreadystatechangeEventHandler HTMLFormElementEvents_Event_onreadystatechange;

		// Token: 0x1400022A RID: 554
		// (add) Token: 0x06001FBA RID: 8122
		// (remove) Token: 0x06001FBB RID: 8123
		public virtual extern event HTMLFormElementEvents_onbeforeeditfocusEventHandler HTMLFormElementEvents_Event_onbeforeeditfocus;

		// Token: 0x1400022B RID: 555
		// (add) Token: 0x06001FBC RID: 8124
		// (remove) Token: 0x06001FBD RID: 8125
		public virtual extern event HTMLFormElementEvents_onlayoutcompleteEventHandler HTMLFormElementEvents_Event_onlayoutcomplete;

		// Token: 0x1400022C RID: 556
		// (add) Token: 0x06001FBE RID: 8126
		// (remove) Token: 0x06001FBF RID: 8127
		public virtual extern event HTMLFormElementEvents_onpageEventHandler HTMLFormElementEvents_Event_onpage;

		// Token: 0x1400022D RID: 557
		// (add) Token: 0x06001FC0 RID: 8128
		// (remove) Token: 0x06001FC1 RID: 8129
		public virtual extern event HTMLFormElementEvents_onbeforedeactivateEventHandler HTMLFormElementEvents_Event_onbeforedeactivate;

		// Token: 0x1400022E RID: 558
		// (add) Token: 0x06001FC2 RID: 8130
		// (remove) Token: 0x06001FC3 RID: 8131
		public virtual extern event HTMLFormElementEvents_onbeforeactivateEventHandler HTMLFormElementEvents_Event_onbeforeactivate;

		// Token: 0x1400022F RID: 559
		// (add) Token: 0x06001FC4 RID: 8132
		// (remove) Token: 0x06001FC5 RID: 8133
		public virtual extern event HTMLFormElementEvents_onmoveEventHandler HTMLFormElementEvents_Event_onmove;

		// Token: 0x14000230 RID: 560
		// (add) Token: 0x06001FC6 RID: 8134
		// (remove) Token: 0x06001FC7 RID: 8135
		public virtual extern event HTMLFormElementEvents_oncontrolselectEventHandler HTMLFormElementEvents_Event_oncontrolselect;

		// Token: 0x14000231 RID: 561
		// (add) Token: 0x06001FC8 RID: 8136
		// (remove) Token: 0x06001FC9 RID: 8137
		public virtual extern event HTMLFormElementEvents_onmovestartEventHandler HTMLFormElementEvents_Event_onmovestart;

		// Token: 0x14000232 RID: 562
		// (add) Token: 0x06001FCA RID: 8138
		// (remove) Token: 0x06001FCB RID: 8139
		public virtual extern event HTMLFormElementEvents_onmoveendEventHandler HTMLFormElementEvents_Event_onmoveend;

		// Token: 0x14000233 RID: 563
		// (add) Token: 0x06001FCC RID: 8140
		// (remove) Token: 0x06001FCD RID: 8141
		public virtual extern event HTMLFormElementEvents_onresizestartEventHandler HTMLFormElementEvents_Event_onresizestart;

		// Token: 0x14000234 RID: 564
		// (add) Token: 0x06001FCE RID: 8142
		// (remove) Token: 0x06001FCF RID: 8143
		public virtual extern event HTMLFormElementEvents_onresizeendEventHandler HTMLFormElementEvents_Event_onresizeend;

		// Token: 0x14000235 RID: 565
		// (add) Token: 0x06001FD0 RID: 8144
		// (remove) Token: 0x06001FD1 RID: 8145
		public virtual extern event HTMLFormElementEvents_onmouseenterEventHandler HTMLFormElementEvents_Event_onmouseenter;

		// Token: 0x14000236 RID: 566
		// (add) Token: 0x06001FD2 RID: 8146
		// (remove) Token: 0x06001FD3 RID: 8147
		public virtual extern event HTMLFormElementEvents_onmouseleaveEventHandler HTMLFormElementEvents_Event_onmouseleave;

		// Token: 0x14000237 RID: 567
		// (add) Token: 0x06001FD4 RID: 8148
		// (remove) Token: 0x06001FD5 RID: 8149
		public virtual extern event HTMLFormElementEvents_onmousewheelEventHandler HTMLFormElementEvents_Event_onmousewheel;

		// Token: 0x14000238 RID: 568
		// (add) Token: 0x06001FD6 RID: 8150
		// (remove) Token: 0x06001FD7 RID: 8151
		public virtual extern event HTMLFormElementEvents_onactivateEventHandler HTMLFormElementEvents_Event_onactivate;

		// Token: 0x14000239 RID: 569
		// (add) Token: 0x06001FD8 RID: 8152
		// (remove) Token: 0x06001FD9 RID: 8153
		public virtual extern event HTMLFormElementEvents_ondeactivateEventHandler HTMLFormElementEvents_Event_ondeactivate;

		// Token: 0x1400023A RID: 570
		// (add) Token: 0x06001FDA RID: 8154
		// (remove) Token: 0x06001FDB RID: 8155
		public virtual extern event HTMLFormElementEvents_onfocusinEventHandler HTMLFormElementEvents_Event_onfocusin;

		// Token: 0x1400023B RID: 571
		// (add) Token: 0x06001FDC RID: 8156
		// (remove) Token: 0x06001FDD RID: 8157
		public virtual extern event HTMLFormElementEvents_onfocusoutEventHandler HTMLFormElementEvents_Event_onfocusout;

		// Token: 0x1400023C RID: 572
		// (add) Token: 0x06001FDE RID: 8158
		// (remove) Token: 0x06001FDF RID: 8159
		public virtual extern event HTMLFormElementEvents_onsubmitEventHandler HTMLFormElementEvents_Event_onsubmit;

		// Token: 0x1400023D RID: 573
		// (add) Token: 0x06001FE0 RID: 8160
		// (remove) Token: 0x06001FE1 RID: 8161
		public virtual extern event HTMLFormElementEvents_onresetEventHandler HTMLFormElementEvents_Event_onreset;

		// Token: 0x1400023E RID: 574
		// (add) Token: 0x06001FE2 RID: 8162
		// (remove) Token: 0x06001FE3 RID: 8163
		public virtual extern event HTMLFormElementEvents2_onhelpEventHandler HTMLFormElementEvents2_Event_onhelp;

		// Token: 0x1400023F RID: 575
		// (add) Token: 0x06001FE4 RID: 8164
		// (remove) Token: 0x06001FE5 RID: 8165
		public virtual extern event HTMLFormElementEvents2_onclickEventHandler HTMLFormElementEvents2_Event_onclick;

		// Token: 0x14000240 RID: 576
		// (add) Token: 0x06001FE6 RID: 8166
		// (remove) Token: 0x06001FE7 RID: 8167
		public virtual extern event HTMLFormElementEvents2_ondblclickEventHandler HTMLFormElementEvents2_Event_ondblclick;

		// Token: 0x14000241 RID: 577
		// (add) Token: 0x06001FE8 RID: 8168
		// (remove) Token: 0x06001FE9 RID: 8169
		public virtual extern event HTMLFormElementEvents2_onkeypressEventHandler HTMLFormElementEvents2_Event_onkeypress;

		// Token: 0x14000242 RID: 578
		// (add) Token: 0x06001FEA RID: 8170
		// (remove) Token: 0x06001FEB RID: 8171
		public virtual extern event HTMLFormElementEvents2_onkeydownEventHandler HTMLFormElementEvents2_Event_onkeydown;

		// Token: 0x14000243 RID: 579
		// (add) Token: 0x06001FEC RID: 8172
		// (remove) Token: 0x06001FED RID: 8173
		public virtual extern event HTMLFormElementEvents2_onkeyupEventHandler HTMLFormElementEvents2_Event_onkeyup;

		// Token: 0x14000244 RID: 580
		// (add) Token: 0x06001FEE RID: 8174
		// (remove) Token: 0x06001FEF RID: 8175
		public virtual extern event HTMLFormElementEvents2_onmouseoutEventHandler HTMLFormElementEvents2_Event_onmouseout;

		// Token: 0x14000245 RID: 581
		// (add) Token: 0x06001FF0 RID: 8176
		// (remove) Token: 0x06001FF1 RID: 8177
		public virtual extern event HTMLFormElementEvents2_onmouseoverEventHandler HTMLFormElementEvents2_Event_onmouseover;

		// Token: 0x14000246 RID: 582
		// (add) Token: 0x06001FF2 RID: 8178
		// (remove) Token: 0x06001FF3 RID: 8179
		public virtual extern event HTMLFormElementEvents2_onmousemoveEventHandler HTMLFormElementEvents2_Event_onmousemove;

		// Token: 0x14000247 RID: 583
		// (add) Token: 0x06001FF4 RID: 8180
		// (remove) Token: 0x06001FF5 RID: 8181
		public virtual extern event HTMLFormElementEvents2_onmousedownEventHandler HTMLFormElementEvents2_Event_onmousedown;

		// Token: 0x14000248 RID: 584
		// (add) Token: 0x06001FF6 RID: 8182
		// (remove) Token: 0x06001FF7 RID: 8183
		public virtual extern event HTMLFormElementEvents2_onmouseupEventHandler HTMLFormElementEvents2_Event_onmouseup;

		// Token: 0x14000249 RID: 585
		// (add) Token: 0x06001FF8 RID: 8184
		// (remove) Token: 0x06001FF9 RID: 8185
		public virtual extern event HTMLFormElementEvents2_onselectstartEventHandler HTMLFormElementEvents2_Event_onselectstart;

		// Token: 0x1400024A RID: 586
		// (add) Token: 0x06001FFA RID: 8186
		// (remove) Token: 0x06001FFB RID: 8187
		public virtual extern event HTMLFormElementEvents2_onfilterchangeEventHandler HTMLFormElementEvents2_Event_onfilterchange;

		// Token: 0x1400024B RID: 587
		// (add) Token: 0x06001FFC RID: 8188
		// (remove) Token: 0x06001FFD RID: 8189
		public virtual extern event HTMLFormElementEvents2_ondragstartEventHandler HTMLFormElementEvents2_Event_ondragstart;

		// Token: 0x1400024C RID: 588
		// (add) Token: 0x06001FFE RID: 8190
		// (remove) Token: 0x06001FFF RID: 8191
		public virtual extern event HTMLFormElementEvents2_onbeforeupdateEventHandler HTMLFormElementEvents2_Event_onbeforeupdate;

		// Token: 0x1400024D RID: 589
		// (add) Token: 0x06002000 RID: 8192
		// (remove) Token: 0x06002001 RID: 8193
		public virtual extern event HTMLFormElementEvents2_onafterupdateEventHandler HTMLFormElementEvents2_Event_onafterupdate;

		// Token: 0x1400024E RID: 590
		// (add) Token: 0x06002002 RID: 8194
		// (remove) Token: 0x06002003 RID: 8195
		public virtual extern event HTMLFormElementEvents2_onerrorupdateEventHandler HTMLFormElementEvents2_Event_onerrorupdate;

		// Token: 0x1400024F RID: 591
		// (add) Token: 0x06002004 RID: 8196
		// (remove) Token: 0x06002005 RID: 8197
		public virtual extern event HTMLFormElementEvents2_onrowexitEventHandler HTMLFormElementEvents2_Event_onrowexit;

		// Token: 0x14000250 RID: 592
		// (add) Token: 0x06002006 RID: 8198
		// (remove) Token: 0x06002007 RID: 8199
		public virtual extern event HTMLFormElementEvents2_onrowenterEventHandler HTMLFormElementEvents2_Event_onrowenter;

		// Token: 0x14000251 RID: 593
		// (add) Token: 0x06002008 RID: 8200
		// (remove) Token: 0x06002009 RID: 8201
		public virtual extern event HTMLFormElementEvents2_ondatasetchangedEventHandler HTMLFormElementEvents2_Event_ondatasetchanged;

		// Token: 0x14000252 RID: 594
		// (add) Token: 0x0600200A RID: 8202
		// (remove) Token: 0x0600200B RID: 8203
		public virtual extern event HTMLFormElementEvents2_ondataavailableEventHandler HTMLFormElementEvents2_Event_ondataavailable;

		// Token: 0x14000253 RID: 595
		// (add) Token: 0x0600200C RID: 8204
		// (remove) Token: 0x0600200D RID: 8205
		public virtual extern event HTMLFormElementEvents2_ondatasetcompleteEventHandler HTMLFormElementEvents2_Event_ondatasetcomplete;

		// Token: 0x14000254 RID: 596
		// (add) Token: 0x0600200E RID: 8206
		// (remove) Token: 0x0600200F RID: 8207
		public virtual extern event HTMLFormElementEvents2_onlosecaptureEventHandler HTMLFormElementEvents2_Event_onlosecapture;

		// Token: 0x14000255 RID: 597
		// (add) Token: 0x06002010 RID: 8208
		// (remove) Token: 0x06002011 RID: 8209
		public virtual extern event HTMLFormElementEvents2_onpropertychangeEventHandler HTMLFormElementEvents2_Event_onpropertychange;

		// Token: 0x14000256 RID: 598
		// (add) Token: 0x06002012 RID: 8210
		// (remove) Token: 0x06002013 RID: 8211
		public virtual extern event HTMLFormElementEvents2_onscrollEventHandler HTMLFormElementEvents2_Event_onscroll;

		// Token: 0x14000257 RID: 599
		// (add) Token: 0x06002014 RID: 8212
		// (remove) Token: 0x06002015 RID: 8213
		public virtual extern event HTMLFormElementEvents2_onfocusEventHandler HTMLFormElementEvents2_Event_onfocus;

		// Token: 0x14000258 RID: 600
		// (add) Token: 0x06002016 RID: 8214
		// (remove) Token: 0x06002017 RID: 8215
		public virtual extern event HTMLFormElementEvents2_onblurEventHandler HTMLFormElementEvents2_Event_onblur;

		// Token: 0x14000259 RID: 601
		// (add) Token: 0x06002018 RID: 8216
		// (remove) Token: 0x06002019 RID: 8217
		public virtual extern event HTMLFormElementEvents2_onresizeEventHandler HTMLFormElementEvents2_Event_onresize;

		// Token: 0x1400025A RID: 602
		// (add) Token: 0x0600201A RID: 8218
		// (remove) Token: 0x0600201B RID: 8219
		public virtual extern event HTMLFormElementEvents2_ondragEventHandler HTMLFormElementEvents2_Event_ondrag;

		// Token: 0x1400025B RID: 603
		// (add) Token: 0x0600201C RID: 8220
		// (remove) Token: 0x0600201D RID: 8221
		public virtual extern event HTMLFormElementEvents2_ondragendEventHandler HTMLFormElementEvents2_Event_ondragend;

		// Token: 0x1400025C RID: 604
		// (add) Token: 0x0600201E RID: 8222
		// (remove) Token: 0x0600201F RID: 8223
		public virtual extern event HTMLFormElementEvents2_ondragenterEventHandler HTMLFormElementEvents2_Event_ondragenter;

		// Token: 0x1400025D RID: 605
		// (add) Token: 0x06002020 RID: 8224
		// (remove) Token: 0x06002021 RID: 8225
		public virtual extern event HTMLFormElementEvents2_ondragoverEventHandler HTMLFormElementEvents2_Event_ondragover;

		// Token: 0x1400025E RID: 606
		// (add) Token: 0x06002022 RID: 8226
		// (remove) Token: 0x06002023 RID: 8227
		public virtual extern event HTMLFormElementEvents2_ondragleaveEventHandler HTMLFormElementEvents2_Event_ondragleave;

		// Token: 0x1400025F RID: 607
		// (add) Token: 0x06002024 RID: 8228
		// (remove) Token: 0x06002025 RID: 8229
		public virtual extern event HTMLFormElementEvents2_ondropEventHandler HTMLFormElementEvents2_Event_ondrop;

		// Token: 0x14000260 RID: 608
		// (add) Token: 0x06002026 RID: 8230
		// (remove) Token: 0x06002027 RID: 8231
		public virtual extern event HTMLFormElementEvents2_onbeforecutEventHandler HTMLFormElementEvents2_Event_onbeforecut;

		// Token: 0x14000261 RID: 609
		// (add) Token: 0x06002028 RID: 8232
		// (remove) Token: 0x06002029 RID: 8233
		public virtual extern event HTMLFormElementEvents2_oncutEventHandler HTMLFormElementEvents2_Event_oncut;

		// Token: 0x14000262 RID: 610
		// (add) Token: 0x0600202A RID: 8234
		// (remove) Token: 0x0600202B RID: 8235
		public virtual extern event HTMLFormElementEvents2_onbeforecopyEventHandler HTMLFormElementEvents2_Event_onbeforecopy;

		// Token: 0x14000263 RID: 611
		// (add) Token: 0x0600202C RID: 8236
		// (remove) Token: 0x0600202D RID: 8237
		public virtual extern event HTMLFormElementEvents2_oncopyEventHandler HTMLFormElementEvents2_Event_oncopy;

		// Token: 0x14000264 RID: 612
		// (add) Token: 0x0600202E RID: 8238
		// (remove) Token: 0x0600202F RID: 8239
		public virtual extern event HTMLFormElementEvents2_onbeforepasteEventHandler HTMLFormElementEvents2_Event_onbeforepaste;

		// Token: 0x14000265 RID: 613
		// (add) Token: 0x06002030 RID: 8240
		// (remove) Token: 0x06002031 RID: 8241
		public virtual extern event HTMLFormElementEvents2_onpasteEventHandler HTMLFormElementEvents2_Event_onpaste;

		// Token: 0x14000266 RID: 614
		// (add) Token: 0x06002032 RID: 8242
		// (remove) Token: 0x06002033 RID: 8243
		public virtual extern event HTMLFormElementEvents2_oncontextmenuEventHandler HTMLFormElementEvents2_Event_oncontextmenu;

		// Token: 0x14000267 RID: 615
		// (add) Token: 0x06002034 RID: 8244
		// (remove) Token: 0x06002035 RID: 8245
		public virtual extern event HTMLFormElementEvents2_onrowsdeleteEventHandler HTMLFormElementEvents2_Event_onrowsdelete;

		// Token: 0x14000268 RID: 616
		// (add) Token: 0x06002036 RID: 8246
		// (remove) Token: 0x06002037 RID: 8247
		public virtual extern event HTMLFormElementEvents2_onrowsinsertedEventHandler HTMLFormElementEvents2_Event_onrowsinserted;

		// Token: 0x14000269 RID: 617
		// (add) Token: 0x06002038 RID: 8248
		// (remove) Token: 0x06002039 RID: 8249
		public virtual extern event HTMLFormElementEvents2_oncellchangeEventHandler HTMLFormElementEvents2_Event_oncellchange;

		// Token: 0x1400026A RID: 618
		// (add) Token: 0x0600203A RID: 8250
		// (remove) Token: 0x0600203B RID: 8251
		public virtual extern event HTMLFormElementEvents2_onreadystatechangeEventHandler HTMLFormElementEvents2_Event_onreadystatechange;

		// Token: 0x1400026B RID: 619
		// (add) Token: 0x0600203C RID: 8252
		// (remove) Token: 0x0600203D RID: 8253
		public virtual extern event HTMLFormElementEvents2_onlayoutcompleteEventHandler HTMLFormElementEvents2_Event_onlayoutcomplete;

		// Token: 0x1400026C RID: 620
		// (add) Token: 0x0600203E RID: 8254
		// (remove) Token: 0x0600203F RID: 8255
		public virtual extern event HTMLFormElementEvents2_onpageEventHandler HTMLFormElementEvents2_Event_onpage;

		// Token: 0x1400026D RID: 621
		// (add) Token: 0x06002040 RID: 8256
		// (remove) Token: 0x06002041 RID: 8257
		public virtual extern event HTMLFormElementEvents2_onmouseenterEventHandler HTMLFormElementEvents2_Event_onmouseenter;

		// Token: 0x1400026E RID: 622
		// (add) Token: 0x06002042 RID: 8258
		// (remove) Token: 0x06002043 RID: 8259
		public virtual extern event HTMLFormElementEvents2_onmouseleaveEventHandler HTMLFormElementEvents2_Event_onmouseleave;

		// Token: 0x1400026F RID: 623
		// (add) Token: 0x06002044 RID: 8260
		// (remove) Token: 0x06002045 RID: 8261
		public virtual extern event HTMLFormElementEvents2_onactivateEventHandler HTMLFormElementEvents2_Event_onactivate;

		// Token: 0x14000270 RID: 624
		// (add) Token: 0x06002046 RID: 8262
		// (remove) Token: 0x06002047 RID: 8263
		public virtual extern event HTMLFormElementEvents2_ondeactivateEventHandler HTMLFormElementEvents2_Event_ondeactivate;

		// Token: 0x14000271 RID: 625
		// (add) Token: 0x06002048 RID: 8264
		// (remove) Token: 0x06002049 RID: 8265
		public virtual extern event HTMLFormElementEvents2_onbeforedeactivateEventHandler HTMLFormElementEvents2_Event_onbeforedeactivate;

		// Token: 0x14000272 RID: 626
		// (add) Token: 0x0600204A RID: 8266
		// (remove) Token: 0x0600204B RID: 8267
		public virtual extern event HTMLFormElementEvents2_onbeforeactivateEventHandler HTMLFormElementEvents2_Event_onbeforeactivate;

		// Token: 0x14000273 RID: 627
		// (add) Token: 0x0600204C RID: 8268
		// (remove) Token: 0x0600204D RID: 8269
		public virtual extern event HTMLFormElementEvents2_onfocusinEventHandler HTMLFormElementEvents2_Event_onfocusin;

		// Token: 0x14000274 RID: 628
		// (add) Token: 0x0600204E RID: 8270
		// (remove) Token: 0x0600204F RID: 8271
		public virtual extern event HTMLFormElementEvents2_onfocusoutEventHandler HTMLFormElementEvents2_Event_onfocusout;

		// Token: 0x14000275 RID: 629
		// (add) Token: 0x06002050 RID: 8272
		// (remove) Token: 0x06002051 RID: 8273
		public virtual extern event HTMLFormElementEvents2_onmoveEventHandler HTMLFormElementEvents2_Event_onmove;

		// Token: 0x14000276 RID: 630
		// (add) Token: 0x06002052 RID: 8274
		// (remove) Token: 0x06002053 RID: 8275
		public virtual extern event HTMLFormElementEvents2_oncontrolselectEventHandler HTMLFormElementEvents2_Event_oncontrolselect;

		// Token: 0x14000277 RID: 631
		// (add) Token: 0x06002054 RID: 8276
		// (remove) Token: 0x06002055 RID: 8277
		public virtual extern event HTMLFormElementEvents2_onmovestartEventHandler HTMLFormElementEvents2_Event_onmovestart;

		// Token: 0x14000278 RID: 632
		// (add) Token: 0x06002056 RID: 8278
		// (remove) Token: 0x06002057 RID: 8279
		public virtual extern event HTMLFormElementEvents2_onmoveendEventHandler HTMLFormElementEvents2_Event_onmoveend;

		// Token: 0x14000279 RID: 633
		// (add) Token: 0x06002058 RID: 8280
		// (remove) Token: 0x06002059 RID: 8281
		public virtual extern event HTMLFormElementEvents2_onresizestartEventHandler HTMLFormElementEvents2_Event_onresizestart;

		// Token: 0x1400027A RID: 634
		// (add) Token: 0x0600205A RID: 8282
		// (remove) Token: 0x0600205B RID: 8283
		public virtual extern event HTMLFormElementEvents2_onresizeendEventHandler HTMLFormElementEvents2_Event_onresizeend;

		// Token: 0x1400027B RID: 635
		// (add) Token: 0x0600205C RID: 8284
		// (remove) Token: 0x0600205D RID: 8285
		public virtual extern event HTMLFormElementEvents2_onmousewheelEventHandler HTMLFormElementEvents2_Event_onmousewheel;

		// Token: 0x1400027C RID: 636
		// (add) Token: 0x0600205E RID: 8286
		// (remove) Token: 0x0600205F RID: 8287
		public virtual extern event HTMLFormElementEvents2_onsubmitEventHandler HTMLFormElementEvents2_Event_onsubmit;

		// Token: 0x1400027D RID: 637
		// (add) Token: 0x06002060 RID: 8288
		// (remove) Token: 0x06002061 RID: 8289
		public virtual extern event HTMLFormElementEvents2_onresetEventHandler HTMLFormElementEvents2_Event_onreset;
	}
}
