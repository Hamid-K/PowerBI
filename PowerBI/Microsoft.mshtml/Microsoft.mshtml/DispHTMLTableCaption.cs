using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x020009DD RID: 2525
	[InterfaceType(2)]
	[Guid("3050F508-98B5-11CF-BB82-00AA00BDCE0B")]
	[TypeLibType(4112)]
	[ComImport]
	public interface DispHTMLTableCaption
	{
		// Token: 0x0600F337 RID: 62263
		[DispId(-2147417611)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void setAttribute([MarshalAs(19)] [In] string strAttributeName, [MarshalAs(27)] [In] object AttributeValue, [In] int lFlags = 1);

		// Token: 0x0600F338 RID: 62264
		[DispId(-2147417610)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		object getAttribute([MarshalAs(19)] [In] string strAttributeName, [In] int lFlags = 0);

		// Token: 0x0600F339 RID: 62265
		[DispId(-2147417609)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool removeAttribute([MarshalAs(19)] [In] string strAttributeName, [In] int lFlags = 1);

		// Token: 0x17004F0E RID: 20238
		// (get) Token: 0x0600F33B RID: 62267
		// (set) Token: 0x0600F33A RID: 62266
		[DispId(-2147417111)]
		string className
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

		// Token: 0x17004F0F RID: 20239
		// (get) Token: 0x0600F33D RID: 62269
		// (set) Token: 0x0600F33C RID: 62268
		[DispId(-2147417110)]
		string id
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

		// Token: 0x17004F10 RID: 20240
		// (get) Token: 0x0600F33E RID: 62270
		[DispId(-2147417108)]
		string tagName
		{
			[DispId(-2147417108)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17004F11 RID: 20241
		// (get) Token: 0x0600F33F RID: 62271
		[DispId(-2147418104)]
		IHTMLElement parentElement
		{
			[DispId(-2147418104)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17004F12 RID: 20242
		// (get) Token: 0x0600F340 RID: 62272
		[DispId(-2147418038)]
		IHTMLStyle style
		{
			[TypeLibFunc(1024)]
			[DispId(-2147418038)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17004F13 RID: 20243
		// (get) Token: 0x0600F342 RID: 62274
		// (set) Token: 0x0600F341 RID: 62273
		[DispId(-2147412099)]
		object onhelp
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

		// Token: 0x17004F14 RID: 20244
		// (get) Token: 0x0600F344 RID: 62276
		// (set) Token: 0x0600F343 RID: 62275
		[DispId(-2147412104)]
		object onclick
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

		// Token: 0x17004F15 RID: 20245
		// (get) Token: 0x0600F346 RID: 62278
		// (set) Token: 0x0600F345 RID: 62277
		[DispId(-2147412103)]
		object ondblclick
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

		// Token: 0x17004F16 RID: 20246
		// (get) Token: 0x0600F348 RID: 62280
		// (set) Token: 0x0600F347 RID: 62279
		[DispId(-2147412107)]
		object onkeydown
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

		// Token: 0x17004F17 RID: 20247
		// (get) Token: 0x0600F34A RID: 62282
		// (set) Token: 0x0600F349 RID: 62281
		[DispId(-2147412106)]
		object onkeyup
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

		// Token: 0x17004F18 RID: 20248
		// (get) Token: 0x0600F34C RID: 62284
		// (set) Token: 0x0600F34B RID: 62283
		[DispId(-2147412105)]
		object onkeypress
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

		// Token: 0x17004F19 RID: 20249
		// (get) Token: 0x0600F34E RID: 62286
		// (set) Token: 0x0600F34D RID: 62285
		[DispId(-2147412111)]
		object onmouseout
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

		// Token: 0x17004F1A RID: 20250
		// (get) Token: 0x0600F350 RID: 62288
		// (set) Token: 0x0600F34F RID: 62287
		[DispId(-2147412112)]
		object onmouseover
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

		// Token: 0x17004F1B RID: 20251
		// (get) Token: 0x0600F352 RID: 62290
		// (set) Token: 0x0600F351 RID: 62289
		[DispId(-2147412108)]
		object onmousemove
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

		// Token: 0x17004F1C RID: 20252
		// (get) Token: 0x0600F354 RID: 62292
		// (set) Token: 0x0600F353 RID: 62291
		[DispId(-2147412110)]
		object onmousedown
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

		// Token: 0x17004F1D RID: 20253
		// (get) Token: 0x0600F356 RID: 62294
		// (set) Token: 0x0600F355 RID: 62293
		[DispId(-2147412109)]
		object onmouseup
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

		// Token: 0x17004F1E RID: 20254
		// (get) Token: 0x0600F357 RID: 62295
		[DispId(-2147417094)]
		object document
		{
			[DispId(-2147417094)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17004F1F RID: 20255
		// (get) Token: 0x0600F359 RID: 62297
		// (set) Token: 0x0600F358 RID: 62296
		[DispId(-2147418043)]
		string title
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

		// Token: 0x17004F20 RID: 20256
		// (get) Token: 0x0600F35B RID: 62299
		// (set) Token: 0x0600F35A RID: 62298
		[DispId(-2147413012)]
		string language
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

		// Token: 0x17004F21 RID: 20257
		// (get) Token: 0x0600F35D RID: 62301
		// (set) Token: 0x0600F35C RID: 62300
		[DispId(-2147412075)]
		object onselectstart
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

		// Token: 0x0600F35E RID: 62302
		[DispId(-2147417093)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void scrollIntoView([MarshalAs(27)] [In] [Optional] object varargStart);

		// Token: 0x0600F35F RID: 62303
		[DispId(-2147417092)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool contains([MarshalAs(28)] [In] IHTMLElement pChild);

		// Token: 0x17004F22 RID: 20258
		// (get) Token: 0x0600F360 RID: 62304
		[DispId(-2147417088)]
		int sourceIndex
		{
			[DispId(-2147417088)]
			[TypeLibFunc(4)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17004F23 RID: 20259
		// (get) Token: 0x0600F361 RID: 62305
		[DispId(-2147417087)]
		object recordNumber
		{
			[DispId(-2147417087)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x17004F24 RID: 20260
		// (get) Token: 0x0600F363 RID: 62307
		// (set) Token: 0x0600F362 RID: 62306
		[DispId(-2147413103)]
		string lang
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

		// Token: 0x17004F25 RID: 20261
		// (get) Token: 0x0600F364 RID: 62308
		[DispId(-2147417104)]
		int offsetLeft
		{
			[DispId(-2147417104)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17004F26 RID: 20262
		// (get) Token: 0x0600F365 RID: 62309
		[DispId(-2147417103)]
		int offsetTop
		{
			[DispId(-2147417103)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17004F27 RID: 20263
		// (get) Token: 0x0600F366 RID: 62310
		[DispId(-2147417102)]
		int offsetWidth
		{
			[DispId(-2147417102)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17004F28 RID: 20264
		// (get) Token: 0x0600F367 RID: 62311
		[DispId(-2147417101)]
		int offsetHeight
		{
			[DispId(-2147417101)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17004F29 RID: 20265
		// (get) Token: 0x0600F368 RID: 62312
		[DispId(-2147417100)]
		IHTMLElement offsetParent
		{
			[DispId(-2147417100)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17004F2A RID: 20266
		// (get) Token: 0x0600F36A RID: 62314
		// (set) Token: 0x0600F369 RID: 62313
		[DispId(-2147417086)]
		string innerHTML
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

		// Token: 0x17004F2B RID: 20267
		// (get) Token: 0x0600F36C RID: 62316
		// (set) Token: 0x0600F36B RID: 62315
		[DispId(-2147417085)]
		string innerText
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

		// Token: 0x17004F2C RID: 20268
		// (get) Token: 0x0600F36E RID: 62318
		// (set) Token: 0x0600F36D RID: 62317
		[DispId(-2147417084)]
		string outerHTML
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

		// Token: 0x17004F2D RID: 20269
		// (get) Token: 0x0600F370 RID: 62320
		// (set) Token: 0x0600F36F RID: 62319
		[DispId(-2147417083)]
		string outerText
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

		// Token: 0x0600F371 RID: 62321
		[DispId(-2147417082)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void insertAdjacentHTML([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string html);

		// Token: 0x0600F372 RID: 62322
		[DispId(-2147417081)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void insertAdjacentText([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string text);

		// Token: 0x17004F2E RID: 20270
		// (get) Token: 0x0600F373 RID: 62323
		[DispId(-2147417080)]
		IHTMLElement parentTextEdit
		{
			[DispId(-2147417080)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17004F2F RID: 20271
		// (get) Token: 0x0600F374 RID: 62324
		[DispId(-2147417078)]
		bool isTextEdit
		{
			[DispId(-2147417078)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x0600F375 RID: 62325
		[DispId(-2147417079)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void click();

		// Token: 0x17004F30 RID: 20272
		// (get) Token: 0x0600F376 RID: 62326
		[DispId(-2147417077)]
		IHTMLFiltersCollection filters
		{
			[DispId(-2147417077)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17004F31 RID: 20273
		// (get) Token: 0x0600F378 RID: 62328
		// (set) Token: 0x0600F377 RID: 62327
		[DispId(-2147412077)]
		object ondragstart
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

		// Token: 0x0600F379 RID: 62329
		[DispId(-2147417076)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		string toString();

		// Token: 0x17004F32 RID: 20274
		// (get) Token: 0x0600F37B RID: 62331
		// (set) Token: 0x0600F37A RID: 62330
		[DispId(-2147412091)]
		object onbeforeupdate
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

		// Token: 0x17004F33 RID: 20275
		// (get) Token: 0x0600F37D RID: 62333
		// (set) Token: 0x0600F37C RID: 62332
		[DispId(-2147412090)]
		object onafterupdate
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

		// Token: 0x17004F34 RID: 20276
		// (get) Token: 0x0600F37F RID: 62335
		// (set) Token: 0x0600F37E RID: 62334
		[DispId(-2147412074)]
		object onerrorupdate
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

		// Token: 0x17004F35 RID: 20277
		// (get) Token: 0x0600F381 RID: 62337
		// (set) Token: 0x0600F380 RID: 62336
		[DispId(-2147412094)]
		object onrowexit
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

		// Token: 0x17004F36 RID: 20278
		// (get) Token: 0x0600F383 RID: 62339
		// (set) Token: 0x0600F382 RID: 62338
		[DispId(-2147412093)]
		object onrowenter
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

		// Token: 0x17004F37 RID: 20279
		// (get) Token: 0x0600F385 RID: 62341
		// (set) Token: 0x0600F384 RID: 62340
		[DispId(-2147412072)]
		object ondatasetchanged
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

		// Token: 0x17004F38 RID: 20280
		// (get) Token: 0x0600F387 RID: 62343
		// (set) Token: 0x0600F386 RID: 62342
		[DispId(-2147412071)]
		object ondataavailable
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

		// Token: 0x17004F39 RID: 20281
		// (get) Token: 0x0600F389 RID: 62345
		// (set) Token: 0x0600F388 RID: 62344
		[DispId(-2147412070)]
		object ondatasetcomplete
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

		// Token: 0x17004F3A RID: 20282
		// (get) Token: 0x0600F38B RID: 62347
		// (set) Token: 0x0600F38A RID: 62346
		[DispId(-2147412069)]
		object onfilterchange
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

		// Token: 0x17004F3B RID: 20283
		// (get) Token: 0x0600F38C RID: 62348
		[DispId(-2147417075)]
		object children
		{
			[DispId(-2147417075)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17004F3C RID: 20284
		// (get) Token: 0x0600F38D RID: 62349
		[DispId(-2147417074)]
		object all
		{
			[DispId(-2147417074)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17004F3D RID: 20285
		// (get) Token: 0x0600F38E RID: 62350
		[DispId(-2147417073)]
		string scopeName
		{
			[DispId(-2147417073)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x0600F38F RID: 62351
		[DispId(-2147417072)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void setCapture([In] bool containerCapture = true);

		// Token: 0x0600F390 RID: 62352
		[DispId(-2147417071)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void releaseCapture();

		// Token: 0x17004F3E RID: 20286
		// (get) Token: 0x0600F392 RID: 62354
		// (set) Token: 0x0600F391 RID: 62353
		[DispId(-2147412066)]
		object onlosecapture
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

		// Token: 0x0600F393 RID: 62355
		[DispId(-2147417070)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		string componentFromPoint([In] int x, [In] int y);

		// Token: 0x0600F394 RID: 62356
		[DispId(-2147417069)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void doScroll([MarshalAs(27)] [In] [Optional] object component);

		// Token: 0x17004F3F RID: 20287
		// (get) Token: 0x0600F396 RID: 62358
		// (set) Token: 0x0600F395 RID: 62357
		[DispId(-2147412081)]
		object onscroll
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

		// Token: 0x17004F40 RID: 20288
		// (get) Token: 0x0600F398 RID: 62360
		// (set) Token: 0x0600F397 RID: 62359
		[DispId(-2147412063)]
		object ondrag
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

		// Token: 0x17004F41 RID: 20289
		// (get) Token: 0x0600F39A RID: 62362
		// (set) Token: 0x0600F399 RID: 62361
		[DispId(-2147412062)]
		object ondragend
		{
			[TypeLibFunc(20)]
			[DispId(-2147412062)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412062)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17004F42 RID: 20290
		// (get) Token: 0x0600F39C RID: 62364
		// (set) Token: 0x0600F39B RID: 62363
		[DispId(-2147412061)]
		object ondragenter
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

		// Token: 0x17004F43 RID: 20291
		// (get) Token: 0x0600F39E RID: 62366
		// (set) Token: 0x0600F39D RID: 62365
		[DispId(-2147412060)]
		object ondragover
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

		// Token: 0x17004F44 RID: 20292
		// (get) Token: 0x0600F3A0 RID: 62368
		// (set) Token: 0x0600F39F RID: 62367
		[DispId(-2147412059)]
		object ondragleave
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

		// Token: 0x17004F45 RID: 20293
		// (get) Token: 0x0600F3A2 RID: 62370
		// (set) Token: 0x0600F3A1 RID: 62369
		[DispId(-2147412058)]
		object ondrop
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

		// Token: 0x17004F46 RID: 20294
		// (get) Token: 0x0600F3A4 RID: 62372
		// (set) Token: 0x0600F3A3 RID: 62371
		[DispId(-2147412054)]
		object onbeforecut
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

		// Token: 0x17004F47 RID: 20295
		// (get) Token: 0x0600F3A6 RID: 62374
		// (set) Token: 0x0600F3A5 RID: 62373
		[DispId(-2147412057)]
		object oncut
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

		// Token: 0x17004F48 RID: 20296
		// (get) Token: 0x0600F3A8 RID: 62376
		// (set) Token: 0x0600F3A7 RID: 62375
		[DispId(-2147412053)]
		object onbeforecopy
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

		// Token: 0x17004F49 RID: 20297
		// (get) Token: 0x0600F3AA RID: 62378
		// (set) Token: 0x0600F3A9 RID: 62377
		[DispId(-2147412056)]
		object oncopy
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

		// Token: 0x17004F4A RID: 20298
		// (get) Token: 0x0600F3AC RID: 62380
		// (set) Token: 0x0600F3AB RID: 62379
		[DispId(-2147412052)]
		object onbeforepaste
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

		// Token: 0x17004F4B RID: 20299
		// (get) Token: 0x0600F3AE RID: 62382
		// (set) Token: 0x0600F3AD RID: 62381
		[DispId(-2147412055)]
		object onpaste
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

		// Token: 0x17004F4C RID: 20300
		// (get) Token: 0x0600F3AF RID: 62383
		[DispId(-2147417105)]
		IHTMLCurrentStyle currentStyle
		{
			[TypeLibFunc(1024)]
			[DispId(-2147417105)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17004F4D RID: 20301
		// (get) Token: 0x0600F3B1 RID: 62385
		// (set) Token: 0x0600F3B0 RID: 62384
		[DispId(-2147412065)]
		object onpropertychange
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

		// Token: 0x0600F3B2 RID: 62386
		[DispId(-2147417068)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		IHTMLRectCollection getClientRects();

		// Token: 0x0600F3B3 RID: 62387
		[DispId(-2147417067)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		IHTMLRect getBoundingClientRect();

		// Token: 0x0600F3B4 RID: 62388
		[DispId(-2147417608)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void setExpression([MarshalAs(19)] [In] string propname, [MarshalAs(19)] [In] string expression, [MarshalAs(19)] [In] string language = "");

		// Token: 0x0600F3B5 RID: 62389
		[DispId(-2147417607)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		object getExpression([MarshalAs(19)] [In] string propname);

		// Token: 0x0600F3B6 RID: 62390
		[DispId(-2147417606)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool removeExpression([MarshalAs(19)] [In] string propname);

		// Token: 0x17004F4E RID: 20302
		// (get) Token: 0x0600F3B8 RID: 62392
		// (set) Token: 0x0600F3B7 RID: 62391
		[DispId(-2147418097)]
		short tabIndex
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

		// Token: 0x0600F3B9 RID: 62393
		[DispId(-2147416112)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void focus();

		// Token: 0x17004F4F RID: 20303
		// (get) Token: 0x0600F3BB RID: 62395
		// (set) Token: 0x0600F3BA RID: 62394
		[DispId(-2147416107)]
		string accessKey
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

		// Token: 0x17004F50 RID: 20304
		// (get) Token: 0x0600F3BD RID: 62397
		// (set) Token: 0x0600F3BC RID: 62396
		[DispId(-2147412097)]
		object onblur
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

		// Token: 0x17004F51 RID: 20305
		// (get) Token: 0x0600F3BF RID: 62399
		// (set) Token: 0x0600F3BE RID: 62398
		[DispId(-2147412098)]
		object onfocus
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

		// Token: 0x17004F52 RID: 20306
		// (get) Token: 0x0600F3C1 RID: 62401
		// (set) Token: 0x0600F3C0 RID: 62400
		[DispId(-2147412076)]
		object onresize
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

		// Token: 0x0600F3C2 RID: 62402
		[DispId(-2147416110)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void blur();

		// Token: 0x0600F3C3 RID: 62403
		[DispId(-2147416095)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void addFilter([MarshalAs(25)] [In] object pUnk);

		// Token: 0x0600F3C4 RID: 62404
		[DispId(-2147416094)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void removeFilter([MarshalAs(25)] [In] object pUnk);

		// Token: 0x17004F53 RID: 20307
		// (get) Token: 0x0600F3C5 RID: 62405
		[DispId(-2147416093)]
		int clientHeight
		{
			[TypeLibFunc(20)]
			[DispId(-2147416093)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17004F54 RID: 20308
		// (get) Token: 0x0600F3C6 RID: 62406
		[DispId(-2147416092)]
		int clientWidth
		{
			[DispId(-2147416092)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17004F55 RID: 20309
		// (get) Token: 0x0600F3C7 RID: 62407
		[DispId(-2147416091)]
		int clientTop
		{
			[TypeLibFunc(20)]
			[DispId(-2147416091)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17004F56 RID: 20310
		// (get) Token: 0x0600F3C8 RID: 62408
		[DispId(-2147416090)]
		int clientLeft
		{
			[DispId(-2147416090)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x0600F3C9 RID: 62409
		[DispId(-2147417605)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool attachEvent([MarshalAs(19)] [In] string @event, [MarshalAs(26)] [In] object pdisp);

		// Token: 0x0600F3CA RID: 62410
		[DispId(-2147417604)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void detachEvent([MarshalAs(19)] [In] string @event, [MarshalAs(26)] [In] object pdisp);

		// Token: 0x17004F57 RID: 20311
		// (get) Token: 0x0600F3CB RID: 62411
		[DispId(-2147412996)]
		object readyState
		{
			[DispId(-2147412996)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x17004F58 RID: 20312
		// (get) Token: 0x0600F3CD RID: 62413
		// (set) Token: 0x0600F3CC RID: 62412
		[DispId(-2147412087)]
		object onreadystatechange
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

		// Token: 0x17004F59 RID: 20313
		// (get) Token: 0x0600F3CF RID: 62415
		// (set) Token: 0x0600F3CE RID: 62414
		[DispId(-2147412050)]
		object onrowsdelete
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

		// Token: 0x17004F5A RID: 20314
		// (get) Token: 0x0600F3D1 RID: 62417
		// (set) Token: 0x0600F3D0 RID: 62416
		[DispId(-2147412049)]
		object onrowsinserted
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

		// Token: 0x17004F5B RID: 20315
		// (get) Token: 0x0600F3D3 RID: 62419
		// (set) Token: 0x0600F3D2 RID: 62418
		[DispId(-2147412048)]
		object oncellchange
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

		// Token: 0x17004F5C RID: 20316
		// (get) Token: 0x0600F3D5 RID: 62421
		// (set) Token: 0x0600F3D4 RID: 62420
		[DispId(-2147412995)]
		string dir
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

		// Token: 0x0600F3D6 RID: 62422
		[DispId(-2147417056)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(26)]
		object createControlRange();

		// Token: 0x17004F5D RID: 20317
		// (get) Token: 0x0600F3D7 RID: 62423
		[DispId(-2147417055)]
		int scrollHeight
		{
			[TypeLibFunc(20)]
			[DispId(-2147417055)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17004F5E RID: 20318
		// (get) Token: 0x0600F3D8 RID: 62424
		[DispId(-2147417054)]
		int scrollWidth
		{
			[TypeLibFunc(20)]
			[DispId(-2147417054)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17004F5F RID: 20319
		// (get) Token: 0x0600F3DA RID: 62426
		// (set) Token: 0x0600F3D9 RID: 62425
		[DispId(-2147417053)]
		int scrollTop
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

		// Token: 0x17004F60 RID: 20320
		// (get) Token: 0x0600F3DC RID: 62428
		// (set) Token: 0x0600F3DB RID: 62427
		[DispId(-2147417052)]
		int scrollLeft
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

		// Token: 0x0600F3DD RID: 62429
		[DispId(-2147417050)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void clearAttributes();

		// Token: 0x17004F61 RID: 20321
		// (get) Token: 0x0600F3DF RID: 62431
		// (set) Token: 0x0600F3DE RID: 62430
		[DispId(-2147412047)]
		object oncontextmenu
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

		// Token: 0x0600F3E0 RID: 62432
		[DispId(-2147417043)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		IHTMLElement insertAdjacentElement([MarshalAs(19)] [In] string where, [MarshalAs(28)] [In] IHTMLElement insertedElement);

		// Token: 0x0600F3E1 RID: 62433
		[DispId(-2147417047)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		IHTMLElement applyElement([MarshalAs(28)] [In] IHTMLElement apply, [MarshalAs(19)] [In] string where);

		// Token: 0x0600F3E2 RID: 62434
		[DispId(-2147417042)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		string getAdjacentText([MarshalAs(19)] [In] string where);

		// Token: 0x0600F3E3 RID: 62435
		[DispId(-2147417041)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		string replaceAdjacentText([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string newText);

		// Token: 0x17004F62 RID: 20322
		// (get) Token: 0x0600F3E4 RID: 62436
		[DispId(-2147417040)]
		bool canHaveChildren
		{
			[DispId(-2147417040)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x0600F3E5 RID: 62437
		[DispId(-2147417032)]
		[MethodImpl(4224, MethodCodeType = 3)]
		int addBehavior([MarshalAs(19)] [In] string bstrUrl, [MarshalAs(27)] [In] [Optional] ref object pvarFactory);

		// Token: 0x0600F3E6 RID: 62438
		[DispId(-2147417031)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool removeBehavior([In] int cookie);

		// Token: 0x17004F63 RID: 20323
		// (get) Token: 0x0600F3E7 RID: 62439
		[DispId(-2147417048)]
		IHTMLStyle runtimeStyle
		{
			[TypeLibFunc(1024)]
			[DispId(-2147417048)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17004F64 RID: 20324
		// (get) Token: 0x0600F3E8 RID: 62440
		[DispId(-2147417030)]
		object behaviorUrns
		{
			[DispId(-2147417030)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17004F65 RID: 20325
		// (get) Token: 0x0600F3EA RID: 62442
		// (set) Token: 0x0600F3E9 RID: 62441
		[DispId(-2147417029)]
		string tagUrn
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

		// Token: 0x17004F66 RID: 20326
		// (get) Token: 0x0600F3EC RID: 62444
		// (set) Token: 0x0600F3EB RID: 62443
		[DispId(-2147412043)]
		object onbeforeeditfocus
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

		// Token: 0x17004F67 RID: 20327
		// (get) Token: 0x0600F3ED RID: 62445
		[DispId(-2147417028)]
		int readyStateValue
		{
			[DispId(-2147417028)]
			[TypeLibFunc(65)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x0600F3EE RID: 62446
		[DispId(-2147417027)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		IHTMLElementCollection getElementsByTagName([MarshalAs(19)] [In] string v);

		// Token: 0x0600F3EF RID: 62447
		[DispId(-2147417016)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void mergeAttributes([MarshalAs(28)] [In] IHTMLElement mergeThis, [MarshalAs(27)] [In] [Optional] ref object pvarFlags);

		// Token: 0x17004F68 RID: 20328
		// (get) Token: 0x0600F3F0 RID: 62448
		[DispId(-2147417015)]
		bool isMultiLine
		{
			[DispId(-2147417015)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17004F69 RID: 20329
		// (get) Token: 0x0600F3F1 RID: 62449
		[DispId(-2147417014)]
		bool canHaveHTML
		{
			[DispId(-2147417014)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17004F6A RID: 20330
		// (get) Token: 0x0600F3F3 RID: 62451
		// (set) Token: 0x0600F3F2 RID: 62450
		[DispId(-2147412039)]
		object onlayoutcomplete
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

		// Token: 0x17004F6B RID: 20331
		// (get) Token: 0x0600F3F5 RID: 62453
		// (set) Token: 0x0600F3F4 RID: 62452
		[DispId(-2147412038)]
		object onpage
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

		// Token: 0x17004F6C RID: 20332
		// (get) Token: 0x0600F3F7 RID: 62455
		// (set) Token: 0x0600F3F6 RID: 62454
		[DispId(-2147417012)]
		bool inflateBlock
		{
			[DispId(-2147417012)]
			[TypeLibFunc(1089)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[TypeLibFunc(1089)]
			[DispId(-2147417012)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x17004F6D RID: 20333
		// (get) Token: 0x0600F3F9 RID: 62457
		// (set) Token: 0x0600F3F8 RID: 62456
		[DispId(-2147412035)]
		object onbeforedeactivate
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

		// Token: 0x0600F3FA RID: 62458
		[DispId(-2147417011)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void setActive();

		// Token: 0x17004F6E RID: 20334
		// (get) Token: 0x0600F3FC RID: 62460
		// (set) Token: 0x0600F3FB RID: 62459
		[DispId(-2147412950)]
		string contentEditable
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

		// Token: 0x17004F6F RID: 20335
		// (get) Token: 0x0600F3FD RID: 62461
		[DispId(-2147417010)]
		bool isContentEditable
		{
			[DispId(-2147417010)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17004F70 RID: 20336
		// (get) Token: 0x0600F3FF RID: 62463
		// (set) Token: 0x0600F3FE RID: 62462
		[DispId(-2147412949)]
		bool hideFocus
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

		// Token: 0x17004F71 RID: 20337
		// (get) Token: 0x0600F401 RID: 62465
		// (set) Token: 0x0600F400 RID: 62464
		[DispId(-2147418036)]
		bool disabled
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

		// Token: 0x17004F72 RID: 20338
		// (get) Token: 0x0600F402 RID: 62466
		[DispId(-2147417007)]
		bool isDisabled
		{
			[DispId(-2147417007)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17004F73 RID: 20339
		// (get) Token: 0x0600F404 RID: 62468
		// (set) Token: 0x0600F403 RID: 62467
		[DispId(-2147412034)]
		object onmove
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

		// Token: 0x17004F74 RID: 20340
		// (get) Token: 0x0600F406 RID: 62470
		// (set) Token: 0x0600F405 RID: 62469
		[DispId(-2147412033)]
		object oncontrolselect
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

		// Token: 0x0600F407 RID: 62471
		[DispId(-2147417006)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool FireEvent([MarshalAs(19)] [In] string bstrEventName, [MarshalAs(27)] [In] [Optional] ref object pvarEventObject);

		// Token: 0x17004F75 RID: 20341
		// (get) Token: 0x0600F409 RID: 62473
		// (set) Token: 0x0600F408 RID: 62472
		[DispId(-2147412029)]
		object onresizestart
		{
			[TypeLibFunc(20)]
			[DispId(-2147412029)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412029)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17004F76 RID: 20342
		// (get) Token: 0x0600F40B RID: 62475
		// (set) Token: 0x0600F40A RID: 62474
		[DispId(-2147412028)]
		object onresizeend
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

		// Token: 0x17004F77 RID: 20343
		// (get) Token: 0x0600F40D RID: 62477
		// (set) Token: 0x0600F40C RID: 62476
		[DispId(-2147412031)]
		object onmovestart
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

		// Token: 0x17004F78 RID: 20344
		// (get) Token: 0x0600F40F RID: 62479
		// (set) Token: 0x0600F40E RID: 62478
		[DispId(-2147412030)]
		object onmoveend
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

		// Token: 0x17004F79 RID: 20345
		// (get) Token: 0x0600F411 RID: 62481
		// (set) Token: 0x0600F410 RID: 62480
		[DispId(-2147412027)]
		object onmouseenter
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

		// Token: 0x17004F7A RID: 20346
		// (get) Token: 0x0600F413 RID: 62483
		// (set) Token: 0x0600F412 RID: 62482
		[DispId(-2147412026)]
		object onmouseleave
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

		// Token: 0x17004F7B RID: 20347
		// (get) Token: 0x0600F415 RID: 62485
		// (set) Token: 0x0600F414 RID: 62484
		[DispId(-2147412025)]
		object onactivate
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

		// Token: 0x17004F7C RID: 20348
		// (get) Token: 0x0600F417 RID: 62487
		// (set) Token: 0x0600F416 RID: 62486
		[DispId(-2147412024)]
		object ondeactivate
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

		// Token: 0x0600F418 RID: 62488
		[DispId(-2147417005)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool dragDrop();

		// Token: 0x17004F7D RID: 20349
		// (get) Token: 0x0600F419 RID: 62489
		[DispId(-2147417004)]
		int glyphMode
		{
			[DispId(-2147417004)]
			[TypeLibFunc(1089)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17004F7E RID: 20350
		// (get) Token: 0x0600F41B RID: 62491
		// (set) Token: 0x0600F41A RID: 62490
		[DispId(-2147412036)]
		object onmousewheel
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

		// Token: 0x0600F41C RID: 62492
		[DispId(-2147417000)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void normalize();

		// Token: 0x0600F41D RID: 62493
		[DispId(-2147417003)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		IHTMLDOMAttribute getAttributeNode([MarshalAs(19)] [In] string bstrName);

		// Token: 0x0600F41E RID: 62494
		[DispId(-2147417002)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		IHTMLDOMAttribute setAttributeNode([MarshalAs(28)] [In] IHTMLDOMAttribute pattr);

		// Token: 0x0600F41F RID: 62495
		[DispId(-2147417001)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		IHTMLDOMAttribute removeAttributeNode([MarshalAs(28)] [In] IHTMLDOMAttribute pattr);

		// Token: 0x17004F7F RID: 20351
		// (get) Token: 0x0600F421 RID: 62497
		// (set) Token: 0x0600F420 RID: 62496
		[DispId(-2147412022)]
		object onbeforeactivate
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

		// Token: 0x17004F80 RID: 20352
		// (get) Token: 0x0600F423 RID: 62499
		// (set) Token: 0x0600F422 RID: 62498
		[DispId(-2147412021)]
		object onfocusin
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

		// Token: 0x17004F81 RID: 20353
		// (get) Token: 0x0600F425 RID: 62501
		// (set) Token: 0x0600F424 RID: 62500
		[DispId(-2147412020)]
		object onfocusout
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

		// Token: 0x17004F82 RID: 20354
		// (get) Token: 0x0600F426 RID: 62502
		[DispId(-2147417058)]
		int uniqueNumber
		{
			[DispId(-2147417058)]
			[TypeLibFunc(64)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17004F83 RID: 20355
		// (get) Token: 0x0600F427 RID: 62503
		[DispId(-2147417057)]
		string uniqueID
		{
			[DispId(-2147417057)]
			[TypeLibFunc(64)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17004F84 RID: 20356
		// (get) Token: 0x0600F428 RID: 62504
		[DispId(-2147417066)]
		int nodeType
		{
			[DispId(-2147417066)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17004F85 RID: 20357
		// (get) Token: 0x0600F429 RID: 62505
		[DispId(-2147417065)]
		IHTMLDOMNode parentNode
		{
			[DispId(-2147417065)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x0600F42A RID: 62506
		[DispId(-2147417064)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool hasChildNodes();

		// Token: 0x17004F86 RID: 20358
		// (get) Token: 0x0600F42B RID: 62507
		[DispId(-2147417063)]
		object childNodes
		{
			[DispId(-2147417063)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17004F87 RID: 20359
		// (get) Token: 0x0600F42C RID: 62508
		[DispId(-2147417062)]
		object attributes
		{
			[DispId(-2147417062)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x0600F42D RID: 62509
		[DispId(-2147417061)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		IHTMLDOMNode insertBefore([MarshalAs(28)] [In] IHTMLDOMNode newChild, [MarshalAs(27)] [In] [Optional] object refChild);

		// Token: 0x0600F42E RID: 62510
		[DispId(-2147417060)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		IHTMLDOMNode removeChild([MarshalAs(28)] [In] IHTMLDOMNode oldChild);

		// Token: 0x0600F42F RID: 62511
		[DispId(-2147417059)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		IHTMLDOMNode replaceChild([MarshalAs(28)] [In] IHTMLDOMNode newChild, [MarshalAs(28)] [In] IHTMLDOMNode oldChild);

		// Token: 0x0600F430 RID: 62512
		[DispId(-2147417051)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		IHTMLDOMNode cloneNode([In] bool fDeep);

		// Token: 0x0600F431 RID: 62513
		[DispId(-2147417046)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		IHTMLDOMNode removeNode([In] bool fDeep = false);

		// Token: 0x0600F432 RID: 62514
		[DispId(-2147417044)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		IHTMLDOMNode swapNode([MarshalAs(28)] [In] IHTMLDOMNode otherNode);

		// Token: 0x0600F433 RID: 62515
		[DispId(-2147417045)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		IHTMLDOMNode replaceNode([MarshalAs(28)] [In] IHTMLDOMNode replacement);

		// Token: 0x0600F434 RID: 62516
		[DispId(-2147417039)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		IHTMLDOMNode appendChild([MarshalAs(28)] [In] IHTMLDOMNode newChild);

		// Token: 0x17004F88 RID: 20360
		// (get) Token: 0x0600F435 RID: 62517
		[DispId(-2147417038)]
		string nodeName
		{
			[DispId(-2147417038)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17004F89 RID: 20361
		// (get) Token: 0x0600F437 RID: 62519
		// (set) Token: 0x0600F436 RID: 62518
		[DispId(-2147417037)]
		object nodeValue
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

		// Token: 0x17004F8A RID: 20362
		// (get) Token: 0x0600F438 RID: 62520
		[DispId(-2147417036)]
		IHTMLDOMNode firstChild
		{
			[DispId(-2147417036)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17004F8B RID: 20363
		// (get) Token: 0x0600F439 RID: 62521
		[DispId(-2147417035)]
		IHTMLDOMNode lastChild
		{
			[DispId(-2147417035)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17004F8C RID: 20364
		// (get) Token: 0x0600F43A RID: 62522
		[DispId(-2147417034)]
		IHTMLDOMNode previousSibling
		{
			[DispId(-2147417034)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17004F8D RID: 20365
		// (get) Token: 0x0600F43B RID: 62523
		[DispId(-2147417033)]
		IHTMLDOMNode nextSibling
		{
			[DispId(-2147417033)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17004F8E RID: 20366
		// (get) Token: 0x0600F43C RID: 62524
		[DispId(-2147416999)]
		object ownerDocument
		{
			[DispId(-2147416999)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17004F8F RID: 20367
		// (get) Token: 0x0600F43E RID: 62526
		// (set) Token: 0x0600F43D RID: 62525
		[DispId(-2147418040)]
		string align
		{
			[DispId(-2147418040)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147418040)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17004F90 RID: 20368
		// (get) Token: 0x0600F440 RID: 62528
		// (set) Token: 0x0600F43F RID: 62527
		[DispId(-2147413081)]
		string vAlign
		{
			[DispId(-2147413081)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147413081)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}
	}
}
