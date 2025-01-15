using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000D15 RID: 3349
	[TypeLibType(2)]
	[ClassInterface(0)]
	[ComSourceInterfaces("mshtml.HTMLInputFileElementEvents\0\0")]
	[Guid("3050F2AE-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public class HTMLInputFileElementClass : DispIHTMLInputFileElement, HTMLInputFileElement, HTMLInputFileElementEvents_Event, IHTMLInputFileElement, IHTMLControlElement, IHTMLElement
	{
		// Token: 0x06016CD4 RID: 93396
		[MethodImpl(4096, MethodCodeType = 3)]
		public extern HTMLInputFileElementClass();

		// Token: 0x06016CD5 RID: 93397
		[DispId(-2147417611)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void setAttribute([MarshalAs(19)] [In] string strAttributeName, [MarshalAs(27)] [In] object AttributeValue, [In] int lFlags = 1);

		// Token: 0x06016CD6 RID: 93398
		[DispId(-2147417610)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		public virtual extern object getAttribute([MarshalAs(19)] [In] string strAttributeName, [In] int lFlags = 0);

		// Token: 0x06016CD7 RID: 93399
		[DispId(-2147417609)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool removeAttribute([MarshalAs(19)] [In] string strAttributeName, [In] int lFlags = 1);

		// Token: 0x170079A6 RID: 31142
		// (get) Token: 0x06016CD9 RID: 93401
		// (set) Token: 0x06016CD8 RID: 93400
		[DispId(-2147417111)]
		public virtual extern string className
		{
			[TypeLibFunc(4)]
			[DispId(-2147417111)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(4)]
			[DispId(-2147417111)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x170079A7 RID: 31143
		// (get) Token: 0x06016CDB RID: 93403
		// (set) Token: 0x06016CDA RID: 93402
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

		// Token: 0x170079A8 RID: 31144
		// (get) Token: 0x06016CDC RID: 93404
		[DispId(-2147417108)]
		public virtual extern string tagName
		{
			[DispId(-2147417108)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170079A9 RID: 31145
		// (get) Token: 0x06016CDD RID: 93405
		[DispId(-2147418104)]
		public virtual extern IHTMLElement parentElement
		{
			[DispId(-2147418104)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170079AA RID: 31146
		// (get) Token: 0x06016CDE RID: 93406
		[DispId(-2147418038)]
		public virtual extern IHTMLStyle style
		{
			[DispId(-2147418038)]
			[TypeLibFunc(1024)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170079AB RID: 31147
		// (get) Token: 0x06016CE0 RID: 93408
		// (set) Token: 0x06016CDF RID: 93407
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

		// Token: 0x170079AC RID: 31148
		// (get) Token: 0x06016CE2 RID: 93410
		// (set) Token: 0x06016CE1 RID: 93409
		[DispId(-2147412104)]
		public virtual extern object onclick
		{
			[DispId(-2147412104)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412104)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170079AD RID: 31149
		// (get) Token: 0x06016CE4 RID: 93412
		// (set) Token: 0x06016CE3 RID: 93411
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

		// Token: 0x170079AE RID: 31150
		// (get) Token: 0x06016CE6 RID: 93414
		// (set) Token: 0x06016CE5 RID: 93413
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

		// Token: 0x170079AF RID: 31151
		// (get) Token: 0x06016CE8 RID: 93416
		// (set) Token: 0x06016CE7 RID: 93415
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

		// Token: 0x170079B0 RID: 31152
		// (get) Token: 0x06016CEA RID: 93418
		// (set) Token: 0x06016CE9 RID: 93417
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

		// Token: 0x170079B1 RID: 31153
		// (get) Token: 0x06016CEC RID: 93420
		// (set) Token: 0x06016CEB RID: 93419
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

		// Token: 0x170079B2 RID: 31154
		// (get) Token: 0x06016CEE RID: 93422
		// (set) Token: 0x06016CED RID: 93421
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

		// Token: 0x170079B3 RID: 31155
		// (get) Token: 0x06016CF0 RID: 93424
		// (set) Token: 0x06016CEF RID: 93423
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

		// Token: 0x170079B4 RID: 31156
		// (get) Token: 0x06016CF2 RID: 93426
		// (set) Token: 0x06016CF1 RID: 93425
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

		// Token: 0x170079B5 RID: 31157
		// (get) Token: 0x06016CF4 RID: 93428
		// (set) Token: 0x06016CF3 RID: 93427
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

		// Token: 0x170079B6 RID: 31158
		// (get) Token: 0x06016CF5 RID: 93429
		[DispId(-2147417094)]
		public virtual extern object document
		{
			[DispId(-2147417094)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x170079B7 RID: 31159
		// (get) Token: 0x06016CF7 RID: 93431
		// (set) Token: 0x06016CF6 RID: 93430
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

		// Token: 0x170079B8 RID: 31160
		// (get) Token: 0x06016CF9 RID: 93433
		// (set) Token: 0x06016CF8 RID: 93432
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

		// Token: 0x170079B9 RID: 31161
		// (get) Token: 0x06016CFB RID: 93435
		// (set) Token: 0x06016CFA RID: 93434
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

		// Token: 0x06016CFC RID: 93436
		[DispId(-2147417093)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void scrollIntoView([MarshalAs(27)] [In] [Optional] object varargStart);

		// Token: 0x06016CFD RID: 93437
		[DispId(-2147417092)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool contains([MarshalAs(28)] [In] IHTMLElement pChild);

		// Token: 0x170079BA RID: 31162
		// (get) Token: 0x06016CFE RID: 93438
		[DispId(-2147417088)]
		public virtual extern int sourceIndex
		{
			[DispId(-2147417088)]
			[TypeLibFunc(4)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170079BB RID: 31163
		// (get) Token: 0x06016CFF RID: 93439
		[DispId(-2147417087)]
		public virtual extern object recordNumber
		{
			[DispId(-2147417087)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x170079BC RID: 31164
		// (get) Token: 0x06016D01 RID: 93441
		// (set) Token: 0x06016D00 RID: 93440
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

		// Token: 0x170079BD RID: 31165
		// (get) Token: 0x06016D02 RID: 93442
		[DispId(-2147417104)]
		public virtual extern int offsetLeft
		{
			[DispId(-2147417104)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170079BE RID: 31166
		// (get) Token: 0x06016D03 RID: 93443
		[DispId(-2147417103)]
		public virtual extern int offsetTop
		{
			[DispId(-2147417103)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170079BF RID: 31167
		// (get) Token: 0x06016D04 RID: 93444
		[DispId(-2147417102)]
		public virtual extern int offsetWidth
		{
			[DispId(-2147417102)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170079C0 RID: 31168
		// (get) Token: 0x06016D05 RID: 93445
		[DispId(-2147417101)]
		public virtual extern int offsetHeight
		{
			[DispId(-2147417101)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170079C1 RID: 31169
		// (get) Token: 0x06016D06 RID: 93446
		[DispId(-2147417100)]
		public virtual extern IHTMLElement offsetParent
		{
			[DispId(-2147417100)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170079C2 RID: 31170
		// (get) Token: 0x06016D08 RID: 93448
		// (set) Token: 0x06016D07 RID: 93447
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

		// Token: 0x170079C3 RID: 31171
		// (get) Token: 0x06016D0A RID: 93450
		// (set) Token: 0x06016D09 RID: 93449
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

		// Token: 0x170079C4 RID: 31172
		// (get) Token: 0x06016D0C RID: 93452
		// (set) Token: 0x06016D0B RID: 93451
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

		// Token: 0x170079C5 RID: 31173
		// (get) Token: 0x06016D0E RID: 93454
		// (set) Token: 0x06016D0D RID: 93453
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

		// Token: 0x06016D0F RID: 93455
		[DispId(-2147417082)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void insertAdjacentHTML([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string html);

		// Token: 0x06016D10 RID: 93456
		[DispId(-2147417081)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void insertAdjacentText([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string text);

		// Token: 0x170079C6 RID: 31174
		// (get) Token: 0x06016D11 RID: 93457
		[DispId(-2147417080)]
		public virtual extern IHTMLElement parentTextEdit
		{
			[DispId(-2147417080)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170079C7 RID: 31175
		// (get) Token: 0x06016D12 RID: 93458
		[DispId(-2147417078)]
		public virtual extern bool isTextEdit
		{
			[DispId(-2147417078)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x06016D13 RID: 93459
		[DispId(-2147417079)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void click();

		// Token: 0x170079C8 RID: 31176
		// (get) Token: 0x06016D14 RID: 93460
		[DispId(-2147417077)]
		public virtual extern IHTMLFiltersCollection filters
		{
			[DispId(-2147417077)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170079C9 RID: 31177
		// (get) Token: 0x06016D16 RID: 93462
		// (set) Token: 0x06016D15 RID: 93461
		[DispId(-2147412077)]
		public virtual extern object ondragstart
		{
			[TypeLibFunc(20)]
			[DispId(-2147412077)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412077)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x06016D17 RID: 93463
		[DispId(-2147417076)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string toString();

		// Token: 0x170079CA RID: 31178
		// (get) Token: 0x06016D19 RID: 93465
		// (set) Token: 0x06016D18 RID: 93464
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

		// Token: 0x170079CB RID: 31179
		// (get) Token: 0x06016D1B RID: 93467
		// (set) Token: 0x06016D1A RID: 93466
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

		// Token: 0x170079CC RID: 31180
		// (get) Token: 0x06016D1D RID: 93469
		// (set) Token: 0x06016D1C RID: 93468
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

		// Token: 0x170079CD RID: 31181
		// (get) Token: 0x06016D1F RID: 93471
		// (set) Token: 0x06016D1E RID: 93470
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

		// Token: 0x170079CE RID: 31182
		// (get) Token: 0x06016D21 RID: 93473
		// (set) Token: 0x06016D20 RID: 93472
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

		// Token: 0x170079CF RID: 31183
		// (get) Token: 0x06016D23 RID: 93475
		// (set) Token: 0x06016D22 RID: 93474
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

		// Token: 0x170079D0 RID: 31184
		// (get) Token: 0x06016D25 RID: 93477
		// (set) Token: 0x06016D24 RID: 93476
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

		// Token: 0x170079D1 RID: 31185
		// (get) Token: 0x06016D27 RID: 93479
		// (set) Token: 0x06016D26 RID: 93478
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

		// Token: 0x170079D2 RID: 31186
		// (get) Token: 0x06016D29 RID: 93481
		// (set) Token: 0x06016D28 RID: 93480
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

		// Token: 0x170079D3 RID: 31187
		// (get) Token: 0x06016D2A RID: 93482
		[DispId(-2147417075)]
		public virtual extern object children
		{
			[DispId(-2147417075)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x170079D4 RID: 31188
		// (get) Token: 0x06016D2B RID: 93483
		[DispId(-2147417074)]
		public virtual extern object all
		{
			[DispId(-2147417074)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x170079D5 RID: 31189
		// (get) Token: 0x06016D2D RID: 93485
		// (set) Token: 0x06016D2C RID: 93484
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

		// Token: 0x06016D2E RID: 93486
		[DispId(-2147416112)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void focus();

		// Token: 0x170079D6 RID: 31190
		// (get) Token: 0x06016D30 RID: 93488
		// (set) Token: 0x06016D2F RID: 93487
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

		// Token: 0x170079D7 RID: 31191
		// (get) Token: 0x06016D32 RID: 93490
		// (set) Token: 0x06016D31 RID: 93489
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

		// Token: 0x170079D8 RID: 31192
		// (get) Token: 0x06016D34 RID: 93492
		// (set) Token: 0x06016D33 RID: 93491
		[DispId(-2147412098)]
		public virtual extern object onfocus
		{
			[DispId(-2147412098)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412098)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170079D9 RID: 31193
		// (get) Token: 0x06016D36 RID: 93494
		// (set) Token: 0x06016D35 RID: 93493
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

		// Token: 0x06016D37 RID: 93495
		[DispId(-2147416110)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void blur();

		// Token: 0x06016D38 RID: 93496
		[DispId(-2147416095)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void addFilter([MarshalAs(25)] [In] object pUnk);

		// Token: 0x06016D39 RID: 93497
		[DispId(-2147416094)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void removeFilter([MarshalAs(25)] [In] object pUnk);

		// Token: 0x170079DA RID: 31194
		// (get) Token: 0x06016D3A RID: 93498
		[DispId(-2147416093)]
		public virtual extern int clientHeight
		{
			[DispId(-2147416093)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170079DB RID: 31195
		// (get) Token: 0x06016D3B RID: 93499
		[DispId(-2147416092)]
		public virtual extern int clientWidth
		{
			[DispId(-2147416092)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170079DC RID: 31196
		// (get) Token: 0x06016D3C RID: 93500
		[DispId(-2147416091)]
		public virtual extern int clientTop
		{
			[DispId(-2147416091)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170079DD RID: 31197
		// (get) Token: 0x06016D3D RID: 93501
		[DispId(-2147416090)]
		public virtual extern int clientLeft
		{
			[DispId(-2147416090)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170079DE RID: 31198
		// (get) Token: 0x06016D3E RID: 93502
		[DispId(2000)]
		public virtual extern string type
		{
			[DispId(2000)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170079DF RID: 31199
		// (get) Token: 0x06016D40 RID: 93504
		// (set) Token: 0x06016D3F RID: 93503
		[DispId(-2147418112)]
		public virtual extern string name
		{
			[TypeLibFunc(20)]
			[DispId(-2147418112)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147418112)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x170079E0 RID: 31200
		// (get) Token: 0x06016D42 RID: 93506
		// (set) Token: 0x06016D41 RID: 93505
		[DispId(2021)]
		public virtual extern object status
		{
			[DispId(2021)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(2021)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170079E1 RID: 31201
		// (get) Token: 0x06016D44 RID: 93508
		// (set) Token: 0x06016D43 RID: 93507
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

		// Token: 0x170079E2 RID: 31202
		// (get) Token: 0x06016D45 RID: 93509
		[DispId(-2147416108)]
		public virtual extern IHTMLFormElement form
		{
			[DispId(-2147416108)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170079E3 RID: 31203
		// (get) Token: 0x06016D47 RID: 93511
		// (set) Token: 0x06016D46 RID: 93510
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

		// Token: 0x170079E4 RID: 31204
		// (get) Token: 0x06016D49 RID: 93513
		// (set) Token: 0x06016D48 RID: 93512
		[DispId(2003)]
		public virtual extern int maxLength
		{
			[DispId(2003)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[DispId(2003)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x06016D4A RID: 93514
		[DispId(2004)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void select();

		// Token: 0x170079E5 RID: 31205
		// (get) Token: 0x06016D4C RID: 93516
		// (set) Token: 0x06016D4B RID: 93515
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

		// Token: 0x170079E6 RID: 31206
		// (get) Token: 0x06016D4E RID: 93518
		// (set) Token: 0x06016D4D RID: 93517
		[DispId(-2147412102)]
		public virtual extern object onselect
		{
			[DispId(-2147412102)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412102)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170079E7 RID: 31207
		// (get) Token: 0x06016D50 RID: 93520
		// (set) Token: 0x06016D4F RID: 93519
		[DispId(-2147413011)]
		public virtual extern string value
		{
			[DispId(-2147413011)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413011)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x170079E8 RID: 31208
		// (get) Token: 0x06016D51 RID: 93521
		public virtual extern string IHTMLInputFileElement_type
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170079E9 RID: 31209
		// (get) Token: 0x06016D53 RID: 93523
		// (set) Token: 0x06016D52 RID: 93522
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

		// Token: 0x170079EA RID: 31210
		// (get) Token: 0x06016D55 RID: 93525
		// (set) Token: 0x06016D54 RID: 93524
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

		// Token: 0x170079EB RID: 31211
		// (get) Token: 0x06016D57 RID: 93527
		// (set) Token: 0x06016D56 RID: 93526
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

		// Token: 0x170079EC RID: 31212
		// (get) Token: 0x06016D58 RID: 93528
		public virtual extern IHTMLFormElement IHTMLInputFileElement_form
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170079ED RID: 31213
		// (get) Token: 0x06016D5A RID: 93530
		// (set) Token: 0x06016D59 RID: 93529
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

		// Token: 0x170079EE RID: 31214
		// (get) Token: 0x06016D5C RID: 93532
		// (set) Token: 0x06016D5B RID: 93531
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

		// Token: 0x06016D5D RID: 93533
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLInputFileElement_select();

		// Token: 0x170079EF RID: 31215
		// (get) Token: 0x06016D5F RID: 93535
		// (set) Token: 0x06016D5E RID: 93534
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

		// Token: 0x170079F0 RID: 31216
		// (get) Token: 0x06016D61 RID: 93537
		// (set) Token: 0x06016D60 RID: 93536
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

		// Token: 0x170079F1 RID: 31217
		// (get) Token: 0x06016D63 RID: 93539
		// (set) Token: 0x06016D62 RID: 93538
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

		// Token: 0x170079F2 RID: 31218
		// (get) Token: 0x06016D65 RID: 93541
		// (set) Token: 0x06016D64 RID: 93540
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

		// Token: 0x06016D66 RID: 93542
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLControlElement_focus();

		// Token: 0x170079F3 RID: 31219
		// (get) Token: 0x06016D68 RID: 93544
		// (set) Token: 0x06016D67 RID: 93543
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

		// Token: 0x170079F4 RID: 31220
		// (get) Token: 0x06016D6A RID: 93546
		// (set) Token: 0x06016D69 RID: 93545
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

		// Token: 0x170079F5 RID: 31221
		// (get) Token: 0x06016D6C RID: 93548
		// (set) Token: 0x06016D6B RID: 93547
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

		// Token: 0x170079F6 RID: 31222
		// (get) Token: 0x06016D6E RID: 93550
		// (set) Token: 0x06016D6D RID: 93549
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

		// Token: 0x06016D6F RID: 93551
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLControlElement_blur();

		// Token: 0x06016D70 RID: 93552
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLControlElement_addFilter([MarshalAs(25)] [In] object pUnk);

		// Token: 0x06016D71 RID: 93553
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLControlElement_removeFilter([MarshalAs(25)] [In] object pUnk);

		// Token: 0x170079F7 RID: 31223
		// (get) Token: 0x06016D72 RID: 93554
		public virtual extern int IHTMLControlElement_clientHeight
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170079F8 RID: 31224
		// (get) Token: 0x06016D73 RID: 93555
		public virtual extern int IHTMLControlElement_clientWidth
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170079F9 RID: 31225
		// (get) Token: 0x06016D74 RID: 93556
		public virtual extern int IHTMLControlElement_clientTop
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170079FA RID: 31226
		// (get) Token: 0x06016D75 RID: 93557
		public virtual extern int IHTMLControlElement_clientLeft
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x06016D76 RID: 93558
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement_setAttribute([MarshalAs(19)] [In] string strAttributeName, [MarshalAs(27)] [In] object AttributeValue, [In] int lFlags = 1);

		// Token: 0x06016D77 RID: 93559
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		public virtual extern object IHTMLElement_getAttribute([MarshalAs(19)] [In] string strAttributeName, [In] int lFlags = 0);

		// Token: 0x06016D78 RID: 93560
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement_removeAttribute([MarshalAs(19)] [In] string strAttributeName, [In] int lFlags = 1);

		// Token: 0x170079FB RID: 31227
		// (get) Token: 0x06016D7A RID: 93562
		// (set) Token: 0x06016D79 RID: 93561
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

		// Token: 0x170079FC RID: 31228
		// (get) Token: 0x06016D7C RID: 93564
		// (set) Token: 0x06016D7B RID: 93563
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

		// Token: 0x170079FD RID: 31229
		// (get) Token: 0x06016D7D RID: 93565
		public virtual extern string IHTMLElement_tagName
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170079FE RID: 31230
		// (get) Token: 0x06016D7E RID: 93566
		public virtual extern IHTMLElement IHTMLElement_parentElement
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170079FF RID: 31231
		// (get) Token: 0x06016D7F RID: 93567
		public virtual extern IHTMLStyle IHTMLElement_style
		{
			[TypeLibFunc(1024)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17007A00 RID: 31232
		// (get) Token: 0x06016D81 RID: 93569
		// (set) Token: 0x06016D80 RID: 93568
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

		// Token: 0x17007A01 RID: 31233
		// (get) Token: 0x06016D83 RID: 93571
		// (set) Token: 0x06016D82 RID: 93570
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

		// Token: 0x17007A02 RID: 31234
		// (get) Token: 0x06016D85 RID: 93573
		// (set) Token: 0x06016D84 RID: 93572
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

		// Token: 0x17007A03 RID: 31235
		// (get) Token: 0x06016D87 RID: 93575
		// (set) Token: 0x06016D86 RID: 93574
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

		// Token: 0x17007A04 RID: 31236
		// (get) Token: 0x06016D89 RID: 93577
		// (set) Token: 0x06016D88 RID: 93576
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

		// Token: 0x17007A05 RID: 31237
		// (get) Token: 0x06016D8B RID: 93579
		// (set) Token: 0x06016D8A RID: 93578
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

		// Token: 0x17007A06 RID: 31238
		// (get) Token: 0x06016D8D RID: 93581
		// (set) Token: 0x06016D8C RID: 93580
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

		// Token: 0x17007A07 RID: 31239
		// (get) Token: 0x06016D8F RID: 93583
		// (set) Token: 0x06016D8E RID: 93582
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

		// Token: 0x17007A08 RID: 31240
		// (get) Token: 0x06016D91 RID: 93585
		// (set) Token: 0x06016D90 RID: 93584
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

		// Token: 0x17007A09 RID: 31241
		// (get) Token: 0x06016D93 RID: 93587
		// (set) Token: 0x06016D92 RID: 93586
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

		// Token: 0x17007A0A RID: 31242
		// (get) Token: 0x06016D95 RID: 93589
		// (set) Token: 0x06016D94 RID: 93588
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

		// Token: 0x17007A0B RID: 31243
		// (get) Token: 0x06016D96 RID: 93590
		public virtual extern object IHTMLElement_document
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17007A0C RID: 31244
		// (get) Token: 0x06016D98 RID: 93592
		// (set) Token: 0x06016D97 RID: 93591
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

		// Token: 0x17007A0D RID: 31245
		// (get) Token: 0x06016D9A RID: 93594
		// (set) Token: 0x06016D99 RID: 93593
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

		// Token: 0x17007A0E RID: 31246
		// (get) Token: 0x06016D9C RID: 93596
		// (set) Token: 0x06016D9B RID: 93595
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

		// Token: 0x06016D9D RID: 93597
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement_scrollIntoView([MarshalAs(27)] [In] [Optional] object varargStart);

		// Token: 0x06016D9E RID: 93598
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLElement_contains([MarshalAs(28)] [In] IHTMLElement pChild);

		// Token: 0x17007A0F RID: 31247
		// (get) Token: 0x06016D9F RID: 93599
		public virtual extern int IHTMLElement_sourceIndex
		{
			[TypeLibFunc(4)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17007A10 RID: 31248
		// (get) Token: 0x06016DA0 RID: 93600
		public virtual extern object IHTMLElement_recordNumber
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x17007A11 RID: 31249
		// (get) Token: 0x06016DA2 RID: 93602
		// (set) Token: 0x06016DA1 RID: 93601
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

		// Token: 0x17007A12 RID: 31250
		// (get) Token: 0x06016DA3 RID: 93603
		public virtual extern int IHTMLElement_offsetLeft
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17007A13 RID: 31251
		// (get) Token: 0x06016DA4 RID: 93604
		public virtual extern int IHTMLElement_offsetTop
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17007A14 RID: 31252
		// (get) Token: 0x06016DA5 RID: 93605
		public virtual extern int IHTMLElement_offsetWidth
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17007A15 RID: 31253
		// (get) Token: 0x06016DA6 RID: 93606
		public virtual extern int IHTMLElement_offsetHeight
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17007A16 RID: 31254
		// (get) Token: 0x06016DA7 RID: 93607
		public virtual extern IHTMLElement IHTMLElement_offsetParent
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17007A17 RID: 31255
		// (get) Token: 0x06016DA9 RID: 93609
		// (set) Token: 0x06016DA8 RID: 93608
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

		// Token: 0x17007A18 RID: 31256
		// (get) Token: 0x06016DAB RID: 93611
		// (set) Token: 0x06016DAA RID: 93610
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

		// Token: 0x17007A19 RID: 31257
		// (get) Token: 0x06016DAD RID: 93613
		// (set) Token: 0x06016DAC RID: 93612
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

		// Token: 0x17007A1A RID: 31258
		// (get) Token: 0x06016DAF RID: 93615
		// (set) Token: 0x06016DAE RID: 93614
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

		// Token: 0x06016DB0 RID: 93616
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement_insertAdjacentHTML([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string html);

		// Token: 0x06016DB1 RID: 93617
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement_insertAdjacentText([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string text);

		// Token: 0x17007A1B RID: 31259
		// (get) Token: 0x06016DB2 RID: 93618
		public virtual extern IHTMLElement IHTMLElement_parentTextEdit
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17007A1C RID: 31260
		// (get) Token: 0x06016DB3 RID: 93619
		public virtual extern bool IHTMLElement_isTextEdit
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x06016DB4 RID: 93620
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLElement_click();

		// Token: 0x17007A1D RID: 31261
		// (get) Token: 0x06016DB5 RID: 93621
		public virtual extern IHTMLFiltersCollection IHTMLElement_filters
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17007A1E RID: 31262
		// (get) Token: 0x06016DB7 RID: 93623
		// (set) Token: 0x06016DB6 RID: 93622
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

		// Token: 0x06016DB8 RID: 93624
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		public virtual extern string IHTMLElement_toString();

		// Token: 0x17007A1F RID: 31263
		// (get) Token: 0x06016DBA RID: 93626
		// (set) Token: 0x06016DB9 RID: 93625
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

		// Token: 0x17007A20 RID: 31264
		// (get) Token: 0x06016DBC RID: 93628
		// (set) Token: 0x06016DBB RID: 93627
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

		// Token: 0x17007A21 RID: 31265
		// (get) Token: 0x06016DBE RID: 93630
		// (set) Token: 0x06016DBD RID: 93629
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

		// Token: 0x17007A22 RID: 31266
		// (get) Token: 0x06016DC0 RID: 93632
		// (set) Token: 0x06016DBF RID: 93631
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

		// Token: 0x17007A23 RID: 31267
		// (get) Token: 0x06016DC2 RID: 93634
		// (set) Token: 0x06016DC1 RID: 93633
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

		// Token: 0x17007A24 RID: 31268
		// (get) Token: 0x06016DC4 RID: 93636
		// (set) Token: 0x06016DC3 RID: 93635
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

		// Token: 0x17007A25 RID: 31269
		// (get) Token: 0x06016DC6 RID: 93638
		// (set) Token: 0x06016DC5 RID: 93637
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

		// Token: 0x17007A26 RID: 31270
		// (get) Token: 0x06016DC8 RID: 93640
		// (set) Token: 0x06016DC7 RID: 93639
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

		// Token: 0x17007A27 RID: 31271
		// (get) Token: 0x06016DCA RID: 93642
		// (set) Token: 0x06016DC9 RID: 93641
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

		// Token: 0x17007A28 RID: 31272
		// (get) Token: 0x06016DCB RID: 93643
		public virtual extern object IHTMLElement_children
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17007A29 RID: 31273
		// (get) Token: 0x06016DCC RID: 93644
		public virtual extern object IHTMLElement_all
		{
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x14002BF2 RID: 11250
		// (add) Token: 0x06016DCD RID: 93645
		// (remove) Token: 0x06016DCE RID: 93646
		public virtual extern event HTMLInputFileElementEvents_onhelpEventHandler HTMLInputFileElementEvents_Event_onhelp;

		// Token: 0x14002BF3 RID: 11251
		// (add) Token: 0x06016DCF RID: 93647
		// (remove) Token: 0x06016DD0 RID: 93648
		public virtual extern event HTMLInputFileElementEvents_onclickEventHandler HTMLInputFileElementEvents_Event_onclick;

		// Token: 0x14002BF4 RID: 11252
		// (add) Token: 0x06016DD1 RID: 93649
		// (remove) Token: 0x06016DD2 RID: 93650
		public virtual extern event HTMLInputFileElementEvents_ondblclickEventHandler HTMLInputFileElementEvents_Event_ondblclick;

		// Token: 0x14002BF5 RID: 11253
		// (add) Token: 0x06016DD3 RID: 93651
		// (remove) Token: 0x06016DD4 RID: 93652
		public virtual extern event HTMLInputFileElementEvents_onkeypressEventHandler HTMLInputFileElementEvents_Event_onkeypress;

		// Token: 0x14002BF6 RID: 11254
		// (add) Token: 0x06016DD5 RID: 93653
		// (remove) Token: 0x06016DD6 RID: 93654
		public virtual extern event HTMLInputFileElementEvents_onkeydownEventHandler HTMLInputFileElementEvents_Event_onkeydown;

		// Token: 0x14002BF7 RID: 11255
		// (add) Token: 0x06016DD7 RID: 93655
		// (remove) Token: 0x06016DD8 RID: 93656
		public virtual extern event HTMLInputFileElementEvents_onkeyupEventHandler HTMLInputFileElementEvents_Event_onkeyup;

		// Token: 0x14002BF8 RID: 11256
		// (add) Token: 0x06016DD9 RID: 93657
		// (remove) Token: 0x06016DDA RID: 93658
		public virtual extern event HTMLInputFileElementEvents_onmouseoutEventHandler HTMLInputFileElementEvents_Event_onmouseout;

		// Token: 0x14002BF9 RID: 11257
		// (add) Token: 0x06016DDB RID: 93659
		// (remove) Token: 0x06016DDC RID: 93660
		public virtual extern event HTMLInputFileElementEvents_onmouseoverEventHandler HTMLInputFileElementEvents_Event_onmouseover;

		// Token: 0x14002BFA RID: 11258
		// (add) Token: 0x06016DDD RID: 93661
		// (remove) Token: 0x06016DDE RID: 93662
		public virtual extern event HTMLInputFileElementEvents_onmousemoveEventHandler HTMLInputFileElementEvents_Event_onmousemove;

		// Token: 0x14002BFB RID: 11259
		// (add) Token: 0x06016DDF RID: 93663
		// (remove) Token: 0x06016DE0 RID: 93664
		public virtual extern event HTMLInputFileElementEvents_onmousedownEventHandler HTMLInputFileElementEvents_Event_onmousedown;

		// Token: 0x14002BFC RID: 11260
		// (add) Token: 0x06016DE1 RID: 93665
		// (remove) Token: 0x06016DE2 RID: 93666
		public virtual extern event HTMLInputFileElementEvents_onmouseupEventHandler HTMLInputFileElementEvents_Event_onmouseup;

		// Token: 0x14002BFD RID: 11261
		// (add) Token: 0x06016DE3 RID: 93667
		// (remove) Token: 0x06016DE4 RID: 93668
		public virtual extern event HTMLInputFileElementEvents_onselectstartEventHandler HTMLInputFileElementEvents_Event_onselectstart;

		// Token: 0x14002BFE RID: 11262
		// (add) Token: 0x06016DE5 RID: 93669
		// (remove) Token: 0x06016DE6 RID: 93670
		public virtual extern event HTMLInputFileElementEvents_onfilterchangeEventHandler HTMLInputFileElementEvents_Event_onfilterchange;

		// Token: 0x14002BFF RID: 11263
		// (add) Token: 0x06016DE7 RID: 93671
		// (remove) Token: 0x06016DE8 RID: 93672
		public virtual extern event HTMLInputFileElementEvents_ondragstartEventHandler HTMLInputFileElementEvents_Event_ondragstart;

		// Token: 0x14002C00 RID: 11264
		// (add) Token: 0x06016DE9 RID: 93673
		// (remove) Token: 0x06016DEA RID: 93674
		public virtual extern event HTMLInputFileElementEvents_onbeforeupdateEventHandler HTMLInputFileElementEvents_Event_onbeforeupdate;

		// Token: 0x14002C01 RID: 11265
		// (add) Token: 0x06016DEB RID: 93675
		// (remove) Token: 0x06016DEC RID: 93676
		public virtual extern event HTMLInputFileElementEvents_onafterupdateEventHandler HTMLInputFileElementEvents_Event_onafterupdate;

		// Token: 0x14002C02 RID: 11266
		// (add) Token: 0x06016DED RID: 93677
		// (remove) Token: 0x06016DEE RID: 93678
		public virtual extern event HTMLInputFileElementEvents_onerrorupdateEventHandler HTMLInputFileElementEvents_Event_onerrorupdate;

		// Token: 0x14002C03 RID: 11267
		// (add) Token: 0x06016DEF RID: 93679
		// (remove) Token: 0x06016DF0 RID: 93680
		public virtual extern event HTMLInputFileElementEvents_onrowexitEventHandler HTMLInputFileElementEvents_Event_onrowexit;

		// Token: 0x14002C04 RID: 11268
		// (add) Token: 0x06016DF1 RID: 93681
		// (remove) Token: 0x06016DF2 RID: 93682
		public virtual extern event HTMLInputFileElementEvents_onrowenterEventHandler HTMLInputFileElementEvents_Event_onrowenter;

		// Token: 0x14002C05 RID: 11269
		// (add) Token: 0x06016DF3 RID: 93683
		// (remove) Token: 0x06016DF4 RID: 93684
		public virtual extern event HTMLInputFileElementEvents_ondatasetchangedEventHandler HTMLInputFileElementEvents_Event_ondatasetchanged;

		// Token: 0x14002C06 RID: 11270
		// (add) Token: 0x06016DF5 RID: 93685
		// (remove) Token: 0x06016DF6 RID: 93686
		public virtual extern event HTMLInputFileElementEvents_ondataavailableEventHandler HTMLInputFileElementEvents_Event_ondataavailable;

		// Token: 0x14002C07 RID: 11271
		// (add) Token: 0x06016DF7 RID: 93687
		// (remove) Token: 0x06016DF8 RID: 93688
		public virtual extern event HTMLInputFileElementEvents_ondatasetcompleteEventHandler HTMLInputFileElementEvents_Event_ondatasetcomplete;

		// Token: 0x14002C08 RID: 11272
		// (add) Token: 0x06016DF9 RID: 93689
		// (remove) Token: 0x06016DFA RID: 93690
		public virtual extern event HTMLInputFileElementEvents_onlosecaptureEventHandler onlosecapture;

		// Token: 0x14002C09 RID: 11273
		// (add) Token: 0x06016DFB RID: 93691
		// (remove) Token: 0x06016DFC RID: 93692
		public virtual extern event HTMLInputFileElementEvents_onpropertychangeEventHandler onpropertychange;

		// Token: 0x14002C0A RID: 11274
		// (add) Token: 0x06016DFD RID: 93693
		// (remove) Token: 0x06016DFE RID: 93694
		public virtual extern event HTMLInputFileElementEvents_onscrollEventHandler onscroll;

		// Token: 0x14002C0B RID: 11275
		// (add) Token: 0x06016DFF RID: 93695
		// (remove) Token: 0x06016E00 RID: 93696
		public virtual extern event HTMLInputFileElementEvents_onfocusEventHandler HTMLInputFileElementEvents_Event_onfocus;

		// Token: 0x14002C0C RID: 11276
		// (add) Token: 0x06016E01 RID: 93697
		// (remove) Token: 0x06016E02 RID: 93698
		public virtual extern event HTMLInputFileElementEvents_onblurEventHandler HTMLInputFileElementEvents_Event_onblur;

		// Token: 0x14002C0D RID: 11277
		// (add) Token: 0x06016E03 RID: 93699
		// (remove) Token: 0x06016E04 RID: 93700
		public virtual extern event HTMLInputFileElementEvents_onresizeEventHandler HTMLInputFileElementEvents_Event_onresize;

		// Token: 0x14002C0E RID: 11278
		// (add) Token: 0x06016E05 RID: 93701
		// (remove) Token: 0x06016E06 RID: 93702
		public virtual extern event HTMLInputFileElementEvents_ondragEventHandler ondrag;

		// Token: 0x14002C0F RID: 11279
		// (add) Token: 0x06016E07 RID: 93703
		// (remove) Token: 0x06016E08 RID: 93704
		public virtual extern event HTMLInputFileElementEvents_ondragendEventHandler ondragend;

		// Token: 0x14002C10 RID: 11280
		// (add) Token: 0x06016E09 RID: 93705
		// (remove) Token: 0x06016E0A RID: 93706
		public virtual extern event HTMLInputFileElementEvents_ondragenterEventHandler ondragenter;

		// Token: 0x14002C11 RID: 11281
		// (add) Token: 0x06016E0B RID: 93707
		// (remove) Token: 0x06016E0C RID: 93708
		public virtual extern event HTMLInputFileElementEvents_ondragoverEventHandler ondragover;

		// Token: 0x14002C12 RID: 11282
		// (add) Token: 0x06016E0D RID: 93709
		// (remove) Token: 0x06016E0E RID: 93710
		public virtual extern event HTMLInputFileElementEvents_ondragleaveEventHandler ondragleave;

		// Token: 0x14002C13 RID: 11283
		// (add) Token: 0x06016E0F RID: 93711
		// (remove) Token: 0x06016E10 RID: 93712
		public virtual extern event HTMLInputFileElementEvents_ondropEventHandler ondrop;

		// Token: 0x14002C14 RID: 11284
		// (add) Token: 0x06016E11 RID: 93713
		// (remove) Token: 0x06016E12 RID: 93714
		public virtual extern event HTMLInputFileElementEvents_onbeforecutEventHandler onbeforecut;

		// Token: 0x14002C15 RID: 11285
		// (add) Token: 0x06016E13 RID: 93715
		// (remove) Token: 0x06016E14 RID: 93716
		public virtual extern event HTMLInputFileElementEvents_oncutEventHandler oncut;

		// Token: 0x14002C16 RID: 11286
		// (add) Token: 0x06016E15 RID: 93717
		// (remove) Token: 0x06016E16 RID: 93718
		public virtual extern event HTMLInputFileElementEvents_onbeforecopyEventHandler onbeforecopy;

		// Token: 0x14002C17 RID: 11287
		// (add) Token: 0x06016E17 RID: 93719
		// (remove) Token: 0x06016E18 RID: 93720
		public virtual extern event HTMLInputFileElementEvents_oncopyEventHandler oncopy;

		// Token: 0x14002C18 RID: 11288
		// (add) Token: 0x06016E19 RID: 93721
		// (remove) Token: 0x06016E1A RID: 93722
		public virtual extern event HTMLInputFileElementEvents_onbeforepasteEventHandler onbeforepaste;

		// Token: 0x14002C19 RID: 11289
		// (add) Token: 0x06016E1B RID: 93723
		// (remove) Token: 0x06016E1C RID: 93724
		public virtual extern event HTMLInputFileElementEvents_onpasteEventHandler onpaste;

		// Token: 0x14002C1A RID: 11290
		// (add) Token: 0x06016E1D RID: 93725
		// (remove) Token: 0x06016E1E RID: 93726
		public virtual extern event HTMLInputFileElementEvents_oncontextmenuEventHandler oncontextmenu;

		// Token: 0x14002C1B RID: 11291
		// (add) Token: 0x06016E1F RID: 93727
		// (remove) Token: 0x06016E20 RID: 93728
		public virtual extern event HTMLInputFileElementEvents_onrowsdeleteEventHandler onrowsdelete;

		// Token: 0x14002C1C RID: 11292
		// (add) Token: 0x06016E21 RID: 93729
		// (remove) Token: 0x06016E22 RID: 93730
		public virtual extern event HTMLInputFileElementEvents_onrowsinsertedEventHandler onrowsinserted;

		// Token: 0x14002C1D RID: 11293
		// (add) Token: 0x06016E23 RID: 93731
		// (remove) Token: 0x06016E24 RID: 93732
		public virtual extern event HTMLInputFileElementEvents_oncellchangeEventHandler oncellchange;

		// Token: 0x14002C1E RID: 11294
		// (add) Token: 0x06016E25 RID: 93733
		// (remove) Token: 0x06016E26 RID: 93734
		public virtual extern event HTMLInputFileElementEvents_onreadystatechangeEventHandler onreadystatechange;

		// Token: 0x14002C1F RID: 11295
		// (add) Token: 0x06016E27 RID: 93735
		// (remove) Token: 0x06016E28 RID: 93736
		public virtual extern event HTMLInputFileElementEvents_onbeforeeditfocusEventHandler onbeforeeditfocus;

		// Token: 0x14002C20 RID: 11296
		// (add) Token: 0x06016E29 RID: 93737
		// (remove) Token: 0x06016E2A RID: 93738
		public virtual extern event HTMLInputFileElementEvents_onlayoutcompleteEventHandler onlayoutcomplete;

		// Token: 0x14002C21 RID: 11297
		// (add) Token: 0x06016E2B RID: 93739
		// (remove) Token: 0x06016E2C RID: 93740
		public virtual extern event HTMLInputFileElementEvents_onpageEventHandler onpage;

		// Token: 0x14002C22 RID: 11298
		// (add) Token: 0x06016E2D RID: 93741
		// (remove) Token: 0x06016E2E RID: 93742
		public virtual extern event HTMLInputFileElementEvents_onbeforedeactivateEventHandler onbeforedeactivate;

		// Token: 0x14002C23 RID: 11299
		// (add) Token: 0x06016E2F RID: 93743
		// (remove) Token: 0x06016E30 RID: 93744
		public virtual extern event HTMLInputFileElementEvents_onbeforeactivateEventHandler onbeforeactivate;

		// Token: 0x14002C24 RID: 11300
		// (add) Token: 0x06016E31 RID: 93745
		// (remove) Token: 0x06016E32 RID: 93746
		public virtual extern event HTMLInputFileElementEvents_onmoveEventHandler onmove;

		// Token: 0x14002C25 RID: 11301
		// (add) Token: 0x06016E33 RID: 93747
		// (remove) Token: 0x06016E34 RID: 93748
		public virtual extern event HTMLInputFileElementEvents_oncontrolselectEventHandler oncontrolselect;

		// Token: 0x14002C26 RID: 11302
		// (add) Token: 0x06016E35 RID: 93749
		// (remove) Token: 0x06016E36 RID: 93750
		public virtual extern event HTMLInputFileElementEvents_onmovestartEventHandler onmovestart;

		// Token: 0x14002C27 RID: 11303
		// (add) Token: 0x06016E37 RID: 93751
		// (remove) Token: 0x06016E38 RID: 93752
		public virtual extern event HTMLInputFileElementEvents_onmoveendEventHandler onmoveend;

		// Token: 0x14002C28 RID: 11304
		// (add) Token: 0x06016E39 RID: 93753
		// (remove) Token: 0x06016E3A RID: 93754
		public virtual extern event HTMLInputFileElementEvents_onresizestartEventHandler onresizestart;

		// Token: 0x14002C29 RID: 11305
		// (add) Token: 0x06016E3B RID: 93755
		// (remove) Token: 0x06016E3C RID: 93756
		public virtual extern event HTMLInputFileElementEvents_onresizeendEventHandler onresizeend;

		// Token: 0x14002C2A RID: 11306
		// (add) Token: 0x06016E3D RID: 93757
		// (remove) Token: 0x06016E3E RID: 93758
		public virtual extern event HTMLInputFileElementEvents_onmouseenterEventHandler onmouseenter;

		// Token: 0x14002C2B RID: 11307
		// (add) Token: 0x06016E3F RID: 93759
		// (remove) Token: 0x06016E40 RID: 93760
		public virtual extern event HTMLInputFileElementEvents_onmouseleaveEventHandler onmouseleave;

		// Token: 0x14002C2C RID: 11308
		// (add) Token: 0x06016E41 RID: 93761
		// (remove) Token: 0x06016E42 RID: 93762
		public virtual extern event HTMLInputFileElementEvents_onmousewheelEventHandler onmousewheel;

		// Token: 0x14002C2D RID: 11309
		// (add) Token: 0x06016E43 RID: 93763
		// (remove) Token: 0x06016E44 RID: 93764
		public virtual extern event HTMLInputFileElementEvents_onactivateEventHandler onactivate;

		// Token: 0x14002C2E RID: 11310
		// (add) Token: 0x06016E45 RID: 93765
		// (remove) Token: 0x06016E46 RID: 93766
		public virtual extern event HTMLInputFileElementEvents_ondeactivateEventHandler ondeactivate;

		// Token: 0x14002C2F RID: 11311
		// (add) Token: 0x06016E47 RID: 93767
		// (remove) Token: 0x06016E48 RID: 93768
		public virtual extern event HTMLInputFileElementEvents_onfocusinEventHandler onfocusin;

		// Token: 0x14002C30 RID: 11312
		// (add) Token: 0x06016E49 RID: 93769
		// (remove) Token: 0x06016E4A RID: 93770
		public virtual extern event HTMLInputFileElementEvents_onfocusoutEventHandler onfocusout;

		// Token: 0x14002C31 RID: 11313
		// (add) Token: 0x06016E4B RID: 93771
		// (remove) Token: 0x06016E4C RID: 93772
		public virtual extern event HTMLInputFileElementEvents_onchangeEventHandler HTMLInputFileElementEvents_Event_onchange;

		// Token: 0x14002C32 RID: 11314
		// (add) Token: 0x06016E4D RID: 93773
		// (remove) Token: 0x06016E4E RID: 93774
		public virtual extern event HTMLInputFileElementEvents_onselectEventHandler HTMLInputFileElementEvents_Event_onselect;

		// Token: 0x14002C33 RID: 11315
		// (add) Token: 0x06016E4F RID: 93775
		// (remove) Token: 0x06016E50 RID: 93776
		public virtual extern event HTMLInputFileElementEvents_onloadEventHandler onload;

		// Token: 0x14002C34 RID: 11316
		// (add) Token: 0x06016E51 RID: 93777
		// (remove) Token: 0x06016E52 RID: 93778
		public virtual extern event HTMLInputFileElementEvents_onerrorEventHandler onerror;

		// Token: 0x14002C35 RID: 11317
		// (add) Token: 0x06016E53 RID: 93779
		// (remove) Token: 0x06016E54 RID: 93780
		public virtual extern event HTMLInputFileElementEvents_onabortEventHandler onabort;
	}
}
