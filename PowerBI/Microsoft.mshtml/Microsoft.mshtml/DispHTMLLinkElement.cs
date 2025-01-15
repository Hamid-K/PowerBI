using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000153 RID: 339
	[TypeLibType(4112)]
	[InterfaceType(2)]
	[Guid("3050F524-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface DispHTMLLinkElement
	{
		// Token: 0x060014CC RID: 5324
		[DispId(-2147417611)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void setAttribute([MarshalAs(19)] [In] string strAttributeName, [MarshalAs(27)] [In] object AttributeValue, [In] int lFlags = 1);

		// Token: 0x060014CD RID: 5325
		[DispId(-2147417610)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		object getAttribute([MarshalAs(19)] [In] string strAttributeName, [In] int lFlags = 0);

		// Token: 0x060014CE RID: 5326
		[DispId(-2147417609)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool removeAttribute([MarshalAs(19)] [In] string strAttributeName, [In] int lFlags = 1);

		// Token: 0x17000927 RID: 2343
		// (get) Token: 0x060014D0 RID: 5328
		// (set) Token: 0x060014CF RID: 5327
		[DispId(-2147417111)]
		string className
		{
			[DispId(-2147417111)]
			[TypeLibFunc(4)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(4)]
			[DispId(-2147417111)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17000928 RID: 2344
		// (get) Token: 0x060014D2 RID: 5330
		// (set) Token: 0x060014D1 RID: 5329
		[DispId(-2147417110)]
		string id
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

		// Token: 0x17000929 RID: 2345
		// (get) Token: 0x060014D3 RID: 5331
		[DispId(-2147417108)]
		string tagName
		{
			[DispId(-2147417108)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x1700092A RID: 2346
		// (get) Token: 0x060014D4 RID: 5332
		[DispId(-2147418104)]
		IHTMLElement parentElement
		{
			[DispId(-2147418104)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x1700092B RID: 2347
		// (get) Token: 0x060014D5 RID: 5333
		[DispId(-2147418038)]
		IHTMLStyle style
		{
			[TypeLibFunc(1024)]
			[DispId(-2147418038)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x1700092C RID: 2348
		// (get) Token: 0x060014D7 RID: 5335
		// (set) Token: 0x060014D6 RID: 5334
		[DispId(-2147412099)]
		object onhelp
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

		// Token: 0x1700092D RID: 2349
		// (get) Token: 0x060014D9 RID: 5337
		// (set) Token: 0x060014D8 RID: 5336
		[DispId(-2147412104)]
		object onclick
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

		// Token: 0x1700092E RID: 2350
		// (get) Token: 0x060014DB RID: 5339
		// (set) Token: 0x060014DA RID: 5338
		[DispId(-2147412103)]
		object ondblclick
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

		// Token: 0x1700092F RID: 2351
		// (get) Token: 0x060014DD RID: 5341
		// (set) Token: 0x060014DC RID: 5340
		[DispId(-2147412107)]
		object onkeydown
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

		// Token: 0x17000930 RID: 2352
		// (get) Token: 0x060014DF RID: 5343
		// (set) Token: 0x060014DE RID: 5342
		[DispId(-2147412106)]
		object onkeyup
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

		// Token: 0x17000931 RID: 2353
		// (get) Token: 0x060014E1 RID: 5345
		// (set) Token: 0x060014E0 RID: 5344
		[DispId(-2147412105)]
		object onkeypress
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

		// Token: 0x17000932 RID: 2354
		// (get) Token: 0x060014E3 RID: 5347
		// (set) Token: 0x060014E2 RID: 5346
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

		// Token: 0x17000933 RID: 2355
		// (get) Token: 0x060014E5 RID: 5349
		// (set) Token: 0x060014E4 RID: 5348
		[DispId(-2147412112)]
		object onmouseover
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

		// Token: 0x17000934 RID: 2356
		// (get) Token: 0x060014E7 RID: 5351
		// (set) Token: 0x060014E6 RID: 5350
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

		// Token: 0x17000935 RID: 2357
		// (get) Token: 0x060014E9 RID: 5353
		// (set) Token: 0x060014E8 RID: 5352
		[DispId(-2147412110)]
		object onmousedown
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

		// Token: 0x17000936 RID: 2358
		// (get) Token: 0x060014EB RID: 5355
		// (set) Token: 0x060014EA RID: 5354
		[DispId(-2147412109)]
		object onmouseup
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

		// Token: 0x17000937 RID: 2359
		// (get) Token: 0x060014EC RID: 5356
		[DispId(-2147417094)]
		object document
		{
			[DispId(-2147417094)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17000938 RID: 2360
		// (get) Token: 0x060014EE RID: 5358
		// (set) Token: 0x060014ED RID: 5357
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

		// Token: 0x17000939 RID: 2361
		// (get) Token: 0x060014F0 RID: 5360
		// (set) Token: 0x060014EF RID: 5359
		[DispId(-2147413012)]
		string language
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

		// Token: 0x1700093A RID: 2362
		// (get) Token: 0x060014F2 RID: 5362
		// (set) Token: 0x060014F1 RID: 5361
		[DispId(-2147412075)]
		object onselectstart
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

		// Token: 0x060014F3 RID: 5363
		[DispId(-2147417093)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void scrollIntoView([MarshalAs(27)] [In] [Optional] object varargStart);

		// Token: 0x060014F4 RID: 5364
		[DispId(-2147417092)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool contains([MarshalAs(28)] [In] IHTMLElement pChild);

		// Token: 0x1700093B RID: 2363
		// (get) Token: 0x060014F5 RID: 5365
		[DispId(-2147417088)]
		int sourceIndex
		{
			[TypeLibFunc(4)]
			[DispId(-2147417088)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700093C RID: 2364
		// (get) Token: 0x060014F6 RID: 5366
		[DispId(-2147417087)]
		object recordNumber
		{
			[DispId(-2147417087)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x1700093D RID: 2365
		// (get) Token: 0x060014F8 RID: 5368
		// (set) Token: 0x060014F7 RID: 5367
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

		// Token: 0x1700093E RID: 2366
		// (get) Token: 0x060014F9 RID: 5369
		[DispId(-2147417104)]
		int offsetLeft
		{
			[DispId(-2147417104)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700093F RID: 2367
		// (get) Token: 0x060014FA RID: 5370
		[DispId(-2147417103)]
		int offsetTop
		{
			[DispId(-2147417103)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17000940 RID: 2368
		// (get) Token: 0x060014FB RID: 5371
		[DispId(-2147417102)]
		int offsetWidth
		{
			[DispId(-2147417102)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17000941 RID: 2369
		// (get) Token: 0x060014FC RID: 5372
		[DispId(-2147417101)]
		int offsetHeight
		{
			[DispId(-2147417101)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17000942 RID: 2370
		// (get) Token: 0x060014FD RID: 5373
		[DispId(-2147417100)]
		IHTMLElement offsetParent
		{
			[DispId(-2147417100)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17000943 RID: 2371
		// (get) Token: 0x060014FF RID: 5375
		// (set) Token: 0x060014FE RID: 5374
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

		// Token: 0x17000944 RID: 2372
		// (get) Token: 0x06001501 RID: 5377
		// (set) Token: 0x06001500 RID: 5376
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

		// Token: 0x17000945 RID: 2373
		// (get) Token: 0x06001503 RID: 5379
		// (set) Token: 0x06001502 RID: 5378
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

		// Token: 0x17000946 RID: 2374
		// (get) Token: 0x06001505 RID: 5381
		// (set) Token: 0x06001504 RID: 5380
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

		// Token: 0x06001506 RID: 5382
		[DispId(-2147417082)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void insertAdjacentHTML([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string html);

		// Token: 0x06001507 RID: 5383
		[DispId(-2147417081)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void insertAdjacentText([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string text);

		// Token: 0x17000947 RID: 2375
		// (get) Token: 0x06001508 RID: 5384
		[DispId(-2147417080)]
		IHTMLElement parentTextEdit
		{
			[DispId(-2147417080)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17000948 RID: 2376
		// (get) Token: 0x06001509 RID: 5385
		[DispId(-2147417078)]
		bool isTextEdit
		{
			[DispId(-2147417078)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x0600150A RID: 5386
		[DispId(-2147417079)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void click();

		// Token: 0x17000949 RID: 2377
		// (get) Token: 0x0600150B RID: 5387
		[DispId(-2147417077)]
		IHTMLFiltersCollection filters
		{
			[DispId(-2147417077)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x1700094A RID: 2378
		// (get) Token: 0x0600150D RID: 5389
		// (set) Token: 0x0600150C RID: 5388
		[DispId(-2147412077)]
		object ondragstart
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

		// Token: 0x0600150E RID: 5390
		[DispId(-2147417076)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		string toString();

		// Token: 0x1700094B RID: 2379
		// (get) Token: 0x06001510 RID: 5392
		// (set) Token: 0x0600150F RID: 5391
		[DispId(-2147412091)]
		object onbeforeupdate
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

		// Token: 0x1700094C RID: 2380
		// (get) Token: 0x06001512 RID: 5394
		// (set) Token: 0x06001511 RID: 5393
		[DispId(-2147412090)]
		object onafterupdate
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

		// Token: 0x1700094D RID: 2381
		// (get) Token: 0x06001514 RID: 5396
		// (set) Token: 0x06001513 RID: 5395
		[DispId(-2147412074)]
		object onerrorupdate
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

		// Token: 0x1700094E RID: 2382
		// (get) Token: 0x06001516 RID: 5398
		// (set) Token: 0x06001515 RID: 5397
		[DispId(-2147412094)]
		object onrowexit
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

		// Token: 0x1700094F RID: 2383
		// (get) Token: 0x06001518 RID: 5400
		// (set) Token: 0x06001517 RID: 5399
		[DispId(-2147412093)]
		object onrowenter
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

		// Token: 0x17000950 RID: 2384
		// (get) Token: 0x0600151A RID: 5402
		// (set) Token: 0x06001519 RID: 5401
		[DispId(-2147412072)]
		object ondatasetchanged
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

		// Token: 0x17000951 RID: 2385
		// (get) Token: 0x0600151C RID: 5404
		// (set) Token: 0x0600151B RID: 5403
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

		// Token: 0x17000952 RID: 2386
		// (get) Token: 0x0600151E RID: 5406
		// (set) Token: 0x0600151D RID: 5405
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

		// Token: 0x17000953 RID: 2387
		// (get) Token: 0x06001520 RID: 5408
		// (set) Token: 0x0600151F RID: 5407
		[DispId(-2147412069)]
		object onfilterchange
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

		// Token: 0x17000954 RID: 2388
		// (get) Token: 0x06001521 RID: 5409
		[DispId(-2147417075)]
		object children
		{
			[DispId(-2147417075)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17000955 RID: 2389
		// (get) Token: 0x06001522 RID: 5410
		[DispId(-2147417074)]
		object all
		{
			[DispId(-2147417074)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x17000956 RID: 2390
		// (get) Token: 0x06001523 RID: 5411
		[DispId(-2147417073)]
		string scopeName
		{
			[DispId(-2147417073)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x06001524 RID: 5412
		[DispId(-2147417072)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void setCapture([In] bool containerCapture = true);

		// Token: 0x06001525 RID: 5413
		[DispId(-2147417071)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void releaseCapture();

		// Token: 0x17000957 RID: 2391
		// (get) Token: 0x06001527 RID: 5415
		// (set) Token: 0x06001526 RID: 5414
		[DispId(-2147412066)]
		object onlosecapture
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

		// Token: 0x06001528 RID: 5416
		[DispId(-2147417070)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		string componentFromPoint([In] int x, [In] int y);

		// Token: 0x06001529 RID: 5417
		[DispId(-2147417069)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void doScroll([MarshalAs(27)] [In] [Optional] object component);

		// Token: 0x17000958 RID: 2392
		// (get) Token: 0x0600152B RID: 5419
		// (set) Token: 0x0600152A RID: 5418
		[DispId(-2147412081)]
		object onscroll
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

		// Token: 0x17000959 RID: 2393
		// (get) Token: 0x0600152D RID: 5421
		// (set) Token: 0x0600152C RID: 5420
		[DispId(-2147412063)]
		object ondrag
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

		// Token: 0x1700095A RID: 2394
		// (get) Token: 0x0600152F RID: 5423
		// (set) Token: 0x0600152E RID: 5422
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

		// Token: 0x1700095B RID: 2395
		// (get) Token: 0x06001531 RID: 5425
		// (set) Token: 0x06001530 RID: 5424
		[DispId(-2147412061)]
		object ondragenter
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

		// Token: 0x1700095C RID: 2396
		// (get) Token: 0x06001533 RID: 5427
		// (set) Token: 0x06001532 RID: 5426
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

		// Token: 0x1700095D RID: 2397
		// (get) Token: 0x06001535 RID: 5429
		// (set) Token: 0x06001534 RID: 5428
		[DispId(-2147412059)]
		object ondragleave
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

		// Token: 0x1700095E RID: 2398
		// (get) Token: 0x06001537 RID: 5431
		// (set) Token: 0x06001536 RID: 5430
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

		// Token: 0x1700095F RID: 2399
		// (get) Token: 0x06001539 RID: 5433
		// (set) Token: 0x06001538 RID: 5432
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

		// Token: 0x17000960 RID: 2400
		// (get) Token: 0x0600153B RID: 5435
		// (set) Token: 0x0600153A RID: 5434
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

		// Token: 0x17000961 RID: 2401
		// (get) Token: 0x0600153D RID: 5437
		// (set) Token: 0x0600153C RID: 5436
		[DispId(-2147412053)]
		object onbeforecopy
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

		// Token: 0x17000962 RID: 2402
		// (get) Token: 0x0600153F RID: 5439
		// (set) Token: 0x0600153E RID: 5438
		[DispId(-2147412056)]
		object oncopy
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

		// Token: 0x17000963 RID: 2403
		// (get) Token: 0x06001541 RID: 5441
		// (set) Token: 0x06001540 RID: 5440
		[DispId(-2147412052)]
		object onbeforepaste
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

		// Token: 0x17000964 RID: 2404
		// (get) Token: 0x06001543 RID: 5443
		// (set) Token: 0x06001542 RID: 5442
		[DispId(-2147412055)]
		object onpaste
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

		// Token: 0x17000965 RID: 2405
		// (get) Token: 0x06001544 RID: 5444
		[DispId(-2147417105)]
		IHTMLCurrentStyle currentStyle
		{
			[TypeLibFunc(1024)]
			[DispId(-2147417105)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x17000966 RID: 2406
		// (get) Token: 0x06001546 RID: 5446
		// (set) Token: 0x06001545 RID: 5445
		[DispId(-2147412065)]
		object onpropertychange
		{
			[TypeLibFunc(20)]
			[DispId(-2147412065)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412065)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x06001547 RID: 5447
		[DispId(-2147417068)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		IHTMLRectCollection getClientRects();

		// Token: 0x06001548 RID: 5448
		[DispId(-2147417067)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		IHTMLRect getBoundingClientRect();

		// Token: 0x06001549 RID: 5449
		[DispId(-2147417608)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void setExpression([MarshalAs(19)] [In] string propname, [MarshalAs(19)] [In] string expression, [MarshalAs(19)] [In] string language = "");

		// Token: 0x0600154A RID: 5450
		[DispId(-2147417607)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		object getExpression([MarshalAs(19)] [In] string propname);

		// Token: 0x0600154B RID: 5451
		[DispId(-2147417606)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool removeExpression([MarshalAs(19)] [In] string propname);

		// Token: 0x17000967 RID: 2407
		// (get) Token: 0x0600154D RID: 5453
		// (set) Token: 0x0600154C RID: 5452
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

		// Token: 0x0600154E RID: 5454
		[DispId(-2147416112)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void focus();

		// Token: 0x17000968 RID: 2408
		// (get) Token: 0x06001550 RID: 5456
		// (set) Token: 0x0600154F RID: 5455
		[DispId(-2147416107)]
		string accessKey
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

		// Token: 0x17000969 RID: 2409
		// (get) Token: 0x06001552 RID: 5458
		// (set) Token: 0x06001551 RID: 5457
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

		// Token: 0x1700096A RID: 2410
		// (get) Token: 0x06001554 RID: 5460
		// (set) Token: 0x06001553 RID: 5459
		[DispId(-2147412098)]
		object onfocus
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

		// Token: 0x1700096B RID: 2411
		// (get) Token: 0x06001556 RID: 5462
		// (set) Token: 0x06001555 RID: 5461
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

		// Token: 0x06001557 RID: 5463
		[DispId(-2147416110)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void blur();

		// Token: 0x06001558 RID: 5464
		[DispId(-2147416095)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void addFilter([MarshalAs(25)] [In] object pUnk);

		// Token: 0x06001559 RID: 5465
		[DispId(-2147416094)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void removeFilter([MarshalAs(25)] [In] object pUnk);

		// Token: 0x1700096C RID: 2412
		// (get) Token: 0x0600155A RID: 5466
		[DispId(-2147416093)]
		int clientHeight
		{
			[TypeLibFunc(20)]
			[DispId(-2147416093)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700096D RID: 2413
		// (get) Token: 0x0600155B RID: 5467
		[DispId(-2147416092)]
		int clientWidth
		{
			[DispId(-2147416092)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700096E RID: 2414
		// (get) Token: 0x0600155C RID: 5468
		[DispId(-2147416091)]
		int clientTop
		{
			[TypeLibFunc(20)]
			[DispId(-2147416091)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700096F RID: 2415
		// (get) Token: 0x0600155D RID: 5469
		[DispId(-2147416090)]
		int clientLeft
		{
			[DispId(-2147416090)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x0600155E RID: 5470
		[DispId(-2147417605)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool attachEvent([MarshalAs(19)] [In] string @event, [MarshalAs(26)] [In] object pdisp);

		// Token: 0x0600155F RID: 5471
		[DispId(-2147417604)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void detachEvent([MarshalAs(19)] [In] string @event, [MarshalAs(26)] [In] object pdisp);

		// Token: 0x17000970 RID: 2416
		// (get) Token: 0x06001560 RID: 5472
		[DispId(-2147412996)]
		object readyState
		{
			[DispId(-2147412996)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x17000971 RID: 2417
		// (get) Token: 0x06001562 RID: 5474
		// (set) Token: 0x06001561 RID: 5473
		[DispId(-2147412087)]
		object onreadystatechange
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

		// Token: 0x17000972 RID: 2418
		// (get) Token: 0x06001564 RID: 5476
		// (set) Token: 0x06001563 RID: 5475
		[DispId(-2147412050)]
		object onrowsdelete
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

		// Token: 0x17000973 RID: 2419
		// (get) Token: 0x06001566 RID: 5478
		// (set) Token: 0x06001565 RID: 5477
		[DispId(-2147412049)]
		object onrowsinserted
		{
			[TypeLibFunc(20)]
			[DispId(-2147412049)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412049)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17000974 RID: 2420
		// (get) Token: 0x06001568 RID: 5480
		// (set) Token: 0x06001567 RID: 5479
		[DispId(-2147412048)]
		object oncellchange
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

		// Token: 0x17000975 RID: 2421
		// (get) Token: 0x0600156A RID: 5482
		// (set) Token: 0x06001569 RID: 5481
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

		// Token: 0x0600156B RID: 5483
		[DispId(-2147417056)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(26)]
		object createControlRange();

		// Token: 0x17000976 RID: 2422
		// (get) Token: 0x0600156C RID: 5484
		[DispId(-2147417055)]
		int scrollHeight
		{
			[DispId(-2147417055)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17000977 RID: 2423
		// (get) Token: 0x0600156D RID: 5485
		[DispId(-2147417054)]
		int scrollWidth
		{
			[TypeLibFunc(20)]
			[DispId(-2147417054)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17000978 RID: 2424
		// (get) Token: 0x0600156F RID: 5487
		// (set) Token: 0x0600156E RID: 5486
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

		// Token: 0x17000979 RID: 2425
		// (get) Token: 0x06001571 RID: 5489
		// (set) Token: 0x06001570 RID: 5488
		[DispId(-2147417052)]
		int scrollLeft
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

		// Token: 0x06001572 RID: 5490
		[DispId(-2147417050)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void clearAttributes();

		// Token: 0x1700097A RID: 2426
		// (get) Token: 0x06001574 RID: 5492
		// (set) Token: 0x06001573 RID: 5491
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

		// Token: 0x06001575 RID: 5493
		[DispId(-2147417043)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		IHTMLElement insertAdjacentElement([MarshalAs(19)] [In] string where, [MarshalAs(28)] [In] IHTMLElement insertedElement);

		// Token: 0x06001576 RID: 5494
		[DispId(-2147417047)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		IHTMLElement applyElement([MarshalAs(28)] [In] IHTMLElement apply, [MarshalAs(19)] [In] string where);

		// Token: 0x06001577 RID: 5495
		[DispId(-2147417042)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		string getAdjacentText([MarshalAs(19)] [In] string where);

		// Token: 0x06001578 RID: 5496
		[DispId(-2147417041)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		string replaceAdjacentText([MarshalAs(19)] [In] string where, [MarshalAs(19)] [In] string newText);

		// Token: 0x1700097B RID: 2427
		// (get) Token: 0x06001579 RID: 5497
		[DispId(-2147417040)]
		bool canHaveChildren
		{
			[DispId(-2147417040)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x0600157A RID: 5498
		[DispId(-2147417032)]
		[MethodImpl(4224, MethodCodeType = 3)]
		int addBehavior([MarshalAs(19)] [In] string bstrUrl, [MarshalAs(27)] [In] [Optional] ref object pvarFactory);

		// Token: 0x0600157B RID: 5499
		[DispId(-2147417031)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool removeBehavior([In] int cookie);

		// Token: 0x1700097C RID: 2428
		// (get) Token: 0x0600157C RID: 5500
		[DispId(-2147417048)]
		IHTMLStyle runtimeStyle
		{
			[DispId(-2147417048)]
			[TypeLibFunc(1024)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x1700097D RID: 2429
		// (get) Token: 0x0600157D RID: 5501
		[DispId(-2147417030)]
		object behaviorUrns
		{
			[DispId(-2147417030)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x1700097E RID: 2430
		// (get) Token: 0x0600157F RID: 5503
		// (set) Token: 0x0600157E RID: 5502
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

		// Token: 0x1700097F RID: 2431
		// (get) Token: 0x06001581 RID: 5505
		// (set) Token: 0x06001580 RID: 5504
		[DispId(-2147412043)]
		object onbeforeeditfocus
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

		// Token: 0x17000980 RID: 2432
		// (get) Token: 0x06001582 RID: 5506
		[DispId(-2147417028)]
		int readyStateValue
		{
			[TypeLibFunc(65)]
			[DispId(-2147417028)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x06001583 RID: 5507
		[DispId(-2147417027)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		IHTMLElementCollection getElementsByTagName([MarshalAs(19)] [In] string v);

		// Token: 0x06001584 RID: 5508
		[DispId(-2147417016)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void mergeAttributes([MarshalAs(28)] [In] IHTMLElement mergeThis, [MarshalAs(27)] [In] [Optional] ref object pvarFlags);

		// Token: 0x17000981 RID: 2433
		// (get) Token: 0x06001585 RID: 5509
		[DispId(-2147417015)]
		bool isMultiLine
		{
			[DispId(-2147417015)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17000982 RID: 2434
		// (get) Token: 0x06001586 RID: 5510
		[DispId(-2147417014)]
		bool canHaveHTML
		{
			[DispId(-2147417014)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17000983 RID: 2435
		// (get) Token: 0x06001588 RID: 5512
		// (set) Token: 0x06001587 RID: 5511
		[DispId(-2147412039)]
		object onlayoutcomplete
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

		// Token: 0x17000984 RID: 2436
		// (get) Token: 0x0600158A RID: 5514
		// (set) Token: 0x06001589 RID: 5513
		[DispId(-2147412038)]
		object onpage
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

		// Token: 0x17000985 RID: 2437
		// (get) Token: 0x0600158C RID: 5516
		// (set) Token: 0x0600158B RID: 5515
		[DispId(-2147417012)]
		bool inflateBlock
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

		// Token: 0x17000986 RID: 2438
		// (get) Token: 0x0600158E RID: 5518
		// (set) Token: 0x0600158D RID: 5517
		[DispId(-2147412035)]
		object onbeforedeactivate
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

		// Token: 0x0600158F RID: 5519
		[DispId(-2147417011)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void setActive();

		// Token: 0x17000987 RID: 2439
		// (get) Token: 0x06001591 RID: 5521
		// (set) Token: 0x06001590 RID: 5520
		[DispId(-2147412950)]
		string contentEditable
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

		// Token: 0x17000988 RID: 2440
		// (get) Token: 0x06001592 RID: 5522
		[DispId(-2147417010)]
		bool isContentEditable
		{
			[DispId(-2147417010)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17000989 RID: 2441
		// (get) Token: 0x06001594 RID: 5524
		// (set) Token: 0x06001593 RID: 5523
		[DispId(-2147412949)]
		bool hideFocus
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

		// Token: 0x1700098A RID: 2442
		// (get) Token: 0x06001596 RID: 5526
		// (set) Token: 0x06001595 RID: 5525
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

		// Token: 0x1700098B RID: 2443
		// (get) Token: 0x06001597 RID: 5527
		[DispId(-2147417007)]
		bool isDisabled
		{
			[DispId(-2147417007)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700098C RID: 2444
		// (get) Token: 0x06001599 RID: 5529
		// (set) Token: 0x06001598 RID: 5528
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

		// Token: 0x1700098D RID: 2445
		// (get) Token: 0x0600159B RID: 5531
		// (set) Token: 0x0600159A RID: 5530
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

		// Token: 0x0600159C RID: 5532
		[DispId(-2147417006)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool FireEvent([MarshalAs(19)] [In] string bstrEventName, [MarshalAs(27)] [In] [Optional] ref object pvarEventObject);

		// Token: 0x1700098E RID: 2446
		// (get) Token: 0x0600159E RID: 5534
		// (set) Token: 0x0600159D RID: 5533
		[DispId(-2147412029)]
		object onresizestart
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

		// Token: 0x1700098F RID: 2447
		// (get) Token: 0x060015A0 RID: 5536
		// (set) Token: 0x0600159F RID: 5535
		[DispId(-2147412028)]
		object onresizeend
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

		// Token: 0x17000990 RID: 2448
		// (get) Token: 0x060015A2 RID: 5538
		// (set) Token: 0x060015A1 RID: 5537
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

		// Token: 0x17000991 RID: 2449
		// (get) Token: 0x060015A4 RID: 5540
		// (set) Token: 0x060015A3 RID: 5539
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

		// Token: 0x17000992 RID: 2450
		// (get) Token: 0x060015A6 RID: 5542
		// (set) Token: 0x060015A5 RID: 5541
		[DispId(-2147412027)]
		object onmouseenter
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

		// Token: 0x17000993 RID: 2451
		// (get) Token: 0x060015A8 RID: 5544
		// (set) Token: 0x060015A7 RID: 5543
		[DispId(-2147412026)]
		object onmouseleave
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

		// Token: 0x17000994 RID: 2452
		// (get) Token: 0x060015AA RID: 5546
		// (set) Token: 0x060015A9 RID: 5545
		[DispId(-2147412025)]
		object onactivate
		{
			[DispId(-2147412025)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412025)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17000995 RID: 2453
		// (get) Token: 0x060015AC RID: 5548
		// (set) Token: 0x060015AB RID: 5547
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

		// Token: 0x060015AD RID: 5549
		[DispId(-2147417005)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool dragDrop();

		// Token: 0x17000996 RID: 2454
		// (get) Token: 0x060015AE RID: 5550
		[DispId(-2147417004)]
		int glyphMode
		{
			[DispId(-2147417004)]
			[TypeLibFunc(1089)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x17000997 RID: 2455
		// (get) Token: 0x060015B0 RID: 5552
		// (set) Token: 0x060015AF RID: 5551
		[DispId(-2147412036)]
		object onmousewheel
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

		// Token: 0x060015B1 RID: 5553
		[DispId(-2147417000)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void normalize();

		// Token: 0x060015B2 RID: 5554
		[DispId(-2147417003)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		IHTMLDOMAttribute getAttributeNode([MarshalAs(19)] [In] string bstrName);

		// Token: 0x060015B3 RID: 5555
		[DispId(-2147417002)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		IHTMLDOMAttribute setAttributeNode([MarshalAs(28)] [In] IHTMLDOMAttribute pattr);

		// Token: 0x060015B4 RID: 5556
		[DispId(-2147417001)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		IHTMLDOMAttribute removeAttributeNode([MarshalAs(28)] [In] IHTMLDOMAttribute pattr);

		// Token: 0x17000998 RID: 2456
		// (get) Token: 0x060015B6 RID: 5558
		// (set) Token: 0x060015B5 RID: 5557
		[DispId(-2147412022)]
		object onbeforeactivate
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

		// Token: 0x17000999 RID: 2457
		// (get) Token: 0x060015B8 RID: 5560
		// (set) Token: 0x060015B7 RID: 5559
		[DispId(-2147412021)]
		object onfocusin
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

		// Token: 0x1700099A RID: 2458
		// (get) Token: 0x060015BA RID: 5562
		// (set) Token: 0x060015B9 RID: 5561
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

		// Token: 0x1700099B RID: 2459
		// (get) Token: 0x060015BB RID: 5563
		[DispId(-2147417058)]
		int uniqueNumber
		{
			[DispId(-2147417058)]
			[TypeLibFunc(64)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700099C RID: 2460
		// (get) Token: 0x060015BC RID: 5564
		[DispId(-2147417057)]
		string uniqueID
		{
			[DispId(-2147417057)]
			[TypeLibFunc(64)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x1700099D RID: 2461
		// (get) Token: 0x060015BD RID: 5565
		[DispId(-2147417066)]
		int nodeType
		{
			[DispId(-2147417066)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700099E RID: 2462
		// (get) Token: 0x060015BE RID: 5566
		[DispId(-2147417065)]
		IHTMLDOMNode parentNode
		{
			[DispId(-2147417065)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x060015BF RID: 5567
		[DispId(-2147417064)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool hasChildNodes();

		// Token: 0x1700099F RID: 2463
		// (get) Token: 0x060015C0 RID: 5568
		[DispId(-2147417063)]
		object childNodes
		{
			[DispId(-2147417063)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x170009A0 RID: 2464
		// (get) Token: 0x060015C1 RID: 5569
		[DispId(-2147417062)]
		object attributes
		{
			[DispId(-2147417062)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x060015C2 RID: 5570
		[DispId(-2147417061)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		IHTMLDOMNode insertBefore([MarshalAs(28)] [In] IHTMLDOMNode newChild, [MarshalAs(27)] [In] [Optional] object refChild);

		// Token: 0x060015C3 RID: 5571
		[DispId(-2147417060)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		IHTMLDOMNode removeChild([MarshalAs(28)] [In] IHTMLDOMNode oldChild);

		// Token: 0x060015C4 RID: 5572
		[DispId(-2147417059)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		IHTMLDOMNode replaceChild([MarshalAs(28)] [In] IHTMLDOMNode newChild, [MarshalAs(28)] [In] IHTMLDOMNode oldChild);

		// Token: 0x060015C5 RID: 5573
		[DispId(-2147417051)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		IHTMLDOMNode cloneNode([In] bool fDeep);

		// Token: 0x060015C6 RID: 5574
		[DispId(-2147417046)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		IHTMLDOMNode removeNode([In] bool fDeep = false);

		// Token: 0x060015C7 RID: 5575
		[DispId(-2147417044)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		IHTMLDOMNode swapNode([MarshalAs(28)] [In] IHTMLDOMNode otherNode);

		// Token: 0x060015C8 RID: 5576
		[DispId(-2147417045)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		IHTMLDOMNode replaceNode([MarshalAs(28)] [In] IHTMLDOMNode replacement);

		// Token: 0x060015C9 RID: 5577
		[DispId(-2147417039)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		IHTMLDOMNode appendChild([MarshalAs(28)] [In] IHTMLDOMNode newChild);

		// Token: 0x170009A1 RID: 2465
		// (get) Token: 0x060015CA RID: 5578
		[DispId(-2147417038)]
		string nodeName
		{
			[DispId(-2147417038)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170009A2 RID: 2466
		// (get) Token: 0x060015CC RID: 5580
		// (set) Token: 0x060015CB RID: 5579
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

		// Token: 0x170009A3 RID: 2467
		// (get) Token: 0x060015CD RID: 5581
		[DispId(-2147417036)]
		IHTMLDOMNode firstChild
		{
			[DispId(-2147417036)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170009A4 RID: 2468
		// (get) Token: 0x060015CE RID: 5582
		[DispId(-2147417035)]
		IHTMLDOMNode lastChild
		{
			[DispId(-2147417035)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170009A5 RID: 2469
		// (get) Token: 0x060015CF RID: 5583
		[DispId(-2147417034)]
		IHTMLDOMNode previousSibling
		{
			[DispId(-2147417034)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170009A6 RID: 2470
		// (get) Token: 0x060015D0 RID: 5584
		[DispId(-2147417033)]
		IHTMLDOMNode nextSibling
		{
			[DispId(-2147417033)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170009A7 RID: 2471
		// (get) Token: 0x060015D1 RID: 5585
		[DispId(-2147416999)]
		object ownerDocument
		{
			[DispId(-2147416999)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x170009A8 RID: 2472
		// (get) Token: 0x060015D3 RID: 5587
		// (set) Token: 0x060015D2 RID: 5586
		[DispId(1005)]
		string href
		{
			[DispId(1005)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(1005)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x170009A9 RID: 2473
		// (get) Token: 0x060015D5 RID: 5589
		// (set) Token: 0x060015D4 RID: 5588
		[DispId(1006)]
		string rel
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

		// Token: 0x170009AA RID: 2474
		// (get) Token: 0x060015D7 RID: 5591
		// (set) Token: 0x060015D6 RID: 5590
		[DispId(1007)]
		string rev
		{
			[TypeLibFunc(20)]
			[DispId(1007)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(1007)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x170009AB RID: 2475
		// (get) Token: 0x060015D9 RID: 5593
		// (set) Token: 0x060015D8 RID: 5592
		[DispId(1008)]
		string type
		{
			[TypeLibFunc(20)]
			[DispId(1008)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(1008)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x170009AC RID: 2476
		// (get) Token: 0x060015DB RID: 5595
		// (set) Token: 0x060015DA RID: 5594
		[DispId(-2147412080)]
		object onload
		{
			[DispId(-2147412080)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412080)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170009AD RID: 2477
		// (get) Token: 0x060015DD RID: 5597
		// (set) Token: 0x060015DC RID: 5596
		[DispId(-2147412083)]
		object onerror
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

		// Token: 0x170009AE RID: 2478
		// (get) Token: 0x060015DE RID: 5598
		[DispId(1014)]
		IHTMLStyleSheet styleSheet
		{
			[DispId(1014)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170009AF RID: 2479
		// (get) Token: 0x060015E0 RID: 5600
		// (set) Token: 0x060015DF RID: 5599
		[DispId(1016)]
		string media
		{
			[TypeLibFunc(20)]
			[DispId(1016)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(1016)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x170009B0 RID: 2480
		// (get) Token: 0x060015E2 RID: 5602
		// (set) Token: 0x060015E1 RID: 5601
		[DispId(1017)]
		string target
		{
			[TypeLibFunc(20)]
			[DispId(1017)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(1017)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x170009B1 RID: 2481
		// (get) Token: 0x060015E4 RID: 5604
		// (set) Token: 0x060015E3 RID: 5603
		[DispId(1018)]
		string charset
		{
			[TypeLibFunc(20)]
			[DispId(1018)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(1018)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x170009B2 RID: 2482
		// (get) Token: 0x060015E6 RID: 5606
		// (set) Token: 0x060015E5 RID: 5605
		[DispId(1019)]
		string hreflang
		{
			[TypeLibFunc(20)]
			[DispId(1019)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(1019)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}
	}
}
