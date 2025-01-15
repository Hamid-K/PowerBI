using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000063 RID: 99
	[TypeLibType(4112)]
	[InterfaceType(2)]
	[Guid("3050F557-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface DispHTMLCurrentStyle
	{
		// Token: 0x170004EF RID: 1263
		// (get) Token: 0x0600098E RID: 2446
		[DispId(-2147413022)]
		string position
		{
			[DispId(-2147413022)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170004F0 RID: 1264
		// (get) Token: 0x0600098F RID: 2447
		[DispId(-2147413042)]
		string styleFloat
		{
			[DispId(-2147413042)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170004F1 RID: 1265
		// (get) Token: 0x06000990 RID: 2448
		[DispId(-2147413110)]
		object color
		{
			[TypeLibFunc(20)]
			[DispId(-2147413110)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x170004F2 RID: 1266
		// (get) Token: 0x06000991 RID: 2449
		[DispId(-501)]
		object backgroundColor
		{
			[DispId(-501)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x170004F3 RID: 1267
		// (get) Token: 0x06000992 RID: 2450
		[DispId(-2147413094)]
		string fontFamily
		{
			[DispId(-2147413094)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170004F4 RID: 1268
		// (get) Token: 0x06000993 RID: 2451
		[DispId(-2147413088)]
		string fontStyle
		{
			[TypeLibFunc(20)]
			[DispId(-2147413088)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170004F5 RID: 1269
		// (get) Token: 0x06000994 RID: 2452
		[DispId(-2147413087)]
		string fontVariant
		{
			[TypeLibFunc(84)]
			[DispId(-2147413087)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170004F6 RID: 1270
		// (get) Token: 0x06000995 RID: 2453
		[DispId(-2147413085)]
		object fontWeight
		{
			[TypeLibFunc(20)]
			[DispId(-2147413085)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x170004F7 RID: 1271
		// (get) Token: 0x06000996 RID: 2454
		[DispId(-2147413093)]
		object fontSize
		{
			[DispId(-2147413093)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x170004F8 RID: 1272
		// (get) Token: 0x06000997 RID: 2455
		[DispId(-2147413111)]
		string backgroundImage
		{
			[DispId(-2147413111)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170004F9 RID: 1273
		// (get) Token: 0x06000998 RID: 2456
		[DispId(-2147413079)]
		object backgroundPositionX
		{
			[DispId(-2147413079)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x170004FA RID: 1274
		// (get) Token: 0x06000999 RID: 2457
		[DispId(-2147413078)]
		object backgroundPositionY
		{
			[TypeLibFunc(20)]
			[DispId(-2147413078)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x170004FB RID: 1275
		// (get) Token: 0x0600099A RID: 2458
		[DispId(-2147413068)]
		string backgroundRepeat
		{
			[TypeLibFunc(20)]
			[DispId(-2147413068)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170004FC RID: 1276
		// (get) Token: 0x0600099B RID: 2459
		[DispId(-2147413054)]
		object borderLeftColor
		{
			[TypeLibFunc(20)]
			[DispId(-2147413054)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x170004FD RID: 1277
		// (get) Token: 0x0600099C RID: 2460
		[DispId(-2147413057)]
		object borderTopColor
		{
			[TypeLibFunc(20)]
			[DispId(-2147413057)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x170004FE RID: 1278
		// (get) Token: 0x0600099D RID: 2461
		[DispId(-2147413056)]
		object borderRightColor
		{
			[DispId(-2147413056)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x170004FF RID: 1279
		// (get) Token: 0x0600099E RID: 2462
		[DispId(-2147413055)]
		object borderBottomColor
		{
			[DispId(-2147413055)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x17000500 RID: 1280
		// (get) Token: 0x0600099F RID: 2463
		[DispId(-2147413047)]
		string borderTopStyle
		{
			[TypeLibFunc(20)]
			[DispId(-2147413047)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17000501 RID: 1281
		// (get) Token: 0x060009A0 RID: 2464
		[DispId(-2147413046)]
		string borderRightStyle
		{
			[TypeLibFunc(20)]
			[DispId(-2147413046)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17000502 RID: 1282
		// (get) Token: 0x060009A1 RID: 2465
		[DispId(-2147413045)]
		string borderBottomStyle
		{
			[TypeLibFunc(20)]
			[DispId(-2147413045)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17000503 RID: 1283
		// (get) Token: 0x060009A2 RID: 2466
		[DispId(-2147413044)]
		string borderLeftStyle
		{
			[DispId(-2147413044)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17000504 RID: 1284
		// (get) Token: 0x060009A3 RID: 2467
		[DispId(-2147413052)]
		object borderTopWidth
		{
			[TypeLibFunc(20)]
			[DispId(-2147413052)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x17000505 RID: 1285
		// (get) Token: 0x060009A4 RID: 2468
		[DispId(-2147413051)]
		object borderRightWidth
		{
			[DispId(-2147413051)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x17000506 RID: 1286
		// (get) Token: 0x060009A5 RID: 2469
		[DispId(-2147413050)]
		object borderBottomWidth
		{
			[TypeLibFunc(20)]
			[DispId(-2147413050)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x17000507 RID: 1287
		// (get) Token: 0x060009A6 RID: 2470
		[DispId(-2147413049)]
		object borderLeftWidth
		{
			[TypeLibFunc(20)]
			[DispId(-2147413049)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x17000508 RID: 1288
		// (get) Token: 0x060009A7 RID: 2471
		[DispId(-2147418109)]
		object left
		{
			[TypeLibFunc(20)]
			[DispId(-2147418109)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x17000509 RID: 1289
		// (get) Token: 0x060009A8 RID: 2472
		[DispId(-2147418108)]
		object top
		{
			[DispId(-2147418108)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x1700050A RID: 1290
		// (get) Token: 0x060009A9 RID: 2473
		[DispId(-2147418107)]
		object width
		{
			[TypeLibFunc(20)]
			[DispId(-2147418107)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x1700050B RID: 1291
		// (get) Token: 0x060009AA RID: 2474
		[DispId(-2147418106)]
		object height
		{
			[DispId(-2147418106)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x1700050C RID: 1292
		// (get) Token: 0x060009AB RID: 2475
		[DispId(-2147413097)]
		object paddingLeft
		{
			[DispId(-2147413097)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x1700050D RID: 1293
		// (get) Token: 0x060009AC RID: 2476
		[DispId(-2147413100)]
		object paddingTop
		{
			[TypeLibFunc(20)]
			[DispId(-2147413100)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x1700050E RID: 1294
		// (get) Token: 0x060009AD RID: 2477
		[DispId(-2147413099)]
		object paddingRight
		{
			[DispId(-2147413099)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x1700050F RID: 1295
		// (get) Token: 0x060009AE RID: 2478
		[DispId(-2147413098)]
		object paddingBottom
		{
			[TypeLibFunc(20)]
			[DispId(-2147413098)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x17000510 RID: 1296
		// (get) Token: 0x060009AF RID: 2479
		[DispId(-2147418040)]
		string textAlign
		{
			[TypeLibFunc(20)]
			[DispId(-2147418040)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17000511 RID: 1297
		// (get) Token: 0x060009B0 RID: 2480
		[DispId(-2147413077)]
		string textDecoration
		{
			[TypeLibFunc(20)]
			[DispId(-2147413077)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17000512 RID: 1298
		// (get) Token: 0x060009B1 RID: 2481
		[DispId(-2147413041)]
		string display
		{
			[DispId(-2147413041)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17000513 RID: 1299
		// (get) Token: 0x060009B2 RID: 2482
		[DispId(-2147413032)]
		string visibility
		{
			[DispId(-2147413032)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17000514 RID: 1300
		// (get) Token: 0x060009B3 RID: 2483
		[DispId(-2147413021)]
		object zIndex
		{
			[DispId(-2147413021)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x17000515 RID: 1301
		// (get) Token: 0x060009B4 RID: 2484
		[DispId(-2147413104)]
		object letterSpacing
		{
			[DispId(-2147413104)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x17000516 RID: 1302
		// (get) Token: 0x060009B5 RID: 2485
		[DispId(-2147413106)]
		object lineHeight
		{
			[TypeLibFunc(20)]
			[DispId(-2147413106)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x17000517 RID: 1303
		// (get) Token: 0x060009B6 RID: 2486
		[DispId(-2147413105)]
		object textIndent
		{
			[TypeLibFunc(20)]
			[DispId(-2147413105)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x17000518 RID: 1304
		// (get) Token: 0x060009B7 RID: 2487
		[DispId(-2147413064)]
		object verticalAlign
		{
			[TypeLibFunc(20)]
			[DispId(-2147413064)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x17000519 RID: 1305
		// (get) Token: 0x060009B8 RID: 2488
		[DispId(-2147413067)]
		string backgroundAttachment
		{
			[DispId(-2147413067)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x1700051A RID: 1306
		// (get) Token: 0x060009B9 RID: 2489
		[DispId(-2147413075)]
		object marginTop
		{
			[TypeLibFunc(20)]
			[DispId(-2147413075)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x1700051B RID: 1307
		// (get) Token: 0x060009BA RID: 2490
		[DispId(-2147413074)]
		object marginRight
		{
			[TypeLibFunc(20)]
			[DispId(-2147413074)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x1700051C RID: 1308
		// (get) Token: 0x060009BB RID: 2491
		[DispId(-2147413073)]
		object marginBottom
		{
			[DispId(-2147413073)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x1700051D RID: 1309
		// (get) Token: 0x060009BC RID: 2492
		[DispId(-2147413072)]
		object marginLeft
		{
			[DispId(-2147413072)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x1700051E RID: 1310
		// (get) Token: 0x060009BD RID: 2493
		[DispId(-2147413096)]
		string clear
		{
			[DispId(-2147413096)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x1700051F RID: 1311
		// (get) Token: 0x060009BE RID: 2494
		[DispId(-2147413040)]
		string listStyleType
		{
			[TypeLibFunc(20)]
			[DispId(-2147413040)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17000520 RID: 1312
		// (get) Token: 0x060009BF RID: 2495
		[DispId(-2147413039)]
		string listStylePosition
		{
			[TypeLibFunc(20)]
			[DispId(-2147413039)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17000521 RID: 1313
		// (get) Token: 0x060009C0 RID: 2496
		[DispId(-2147413038)]
		string listStyleImage
		{
			[TypeLibFunc(20)]
			[DispId(-2147413038)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17000522 RID: 1314
		// (get) Token: 0x060009C1 RID: 2497
		[DispId(-2147413019)]
		object clipTop
		{
			[TypeLibFunc(20)]
			[DispId(-2147413019)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x17000523 RID: 1315
		// (get) Token: 0x060009C2 RID: 2498
		[DispId(-2147413018)]
		object clipRight
		{
			[DispId(-2147413018)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x17000524 RID: 1316
		// (get) Token: 0x060009C3 RID: 2499
		[DispId(-2147413017)]
		object clipBottom
		{
			[TypeLibFunc(20)]
			[DispId(-2147413017)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x17000525 RID: 1317
		// (get) Token: 0x060009C4 RID: 2500
		[DispId(-2147413016)]
		object clipLeft
		{
			[TypeLibFunc(20)]
			[DispId(-2147413016)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x17000526 RID: 1318
		// (get) Token: 0x060009C5 RID: 2501
		[DispId(-2147413102)]
		string overflow
		{
			[TypeLibFunc(20)]
			[DispId(-2147413102)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17000527 RID: 1319
		// (get) Token: 0x060009C6 RID: 2502
		[DispId(-2147413035)]
		string pageBreakBefore
		{
			[TypeLibFunc(20)]
			[DispId(-2147413035)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17000528 RID: 1320
		// (get) Token: 0x060009C7 RID: 2503
		[DispId(-2147413034)]
		string pageBreakAfter
		{
			[TypeLibFunc(20)]
			[DispId(-2147413034)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17000529 RID: 1321
		// (get) Token: 0x060009C8 RID: 2504
		[DispId(-2147413010)]
		string cursor
		{
			[TypeLibFunc(20)]
			[DispId(-2147413010)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x1700052A RID: 1322
		// (get) Token: 0x060009C9 RID: 2505
		[DispId(-2147413014)]
		string tableLayout
		{
			[TypeLibFunc(20)]
			[DispId(-2147413014)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x1700052B RID: 1323
		// (get) Token: 0x060009CA RID: 2506
		[DispId(-2147413028)]
		string borderCollapse
		{
			[TypeLibFunc(20)]
			[DispId(-2147413028)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x1700052C RID: 1324
		// (get) Token: 0x060009CB RID: 2507
		[DispId(-2147412993)]
		string direction
		{
			[TypeLibFunc(20)]
			[DispId(-2147412993)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x1700052D RID: 1325
		// (get) Token: 0x060009CC RID: 2508
		[DispId(-2147412997)]
		string behavior
		{
			[TypeLibFunc(20)]
			[DispId(-2147412997)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x060009CD RID: 2509
		[DispId(-2147417610)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		object getAttribute([MarshalAs(19)] [In] string strAttributeName, [In] int lFlags = 0);

		// Token: 0x1700052E RID: 1326
		// (get) Token: 0x060009CE RID: 2510
		[DispId(-2147412994)]
		string unicodeBidi
		{
			[TypeLibFunc(20)]
			[DispId(-2147412994)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x1700052F RID: 1327
		// (get) Token: 0x060009CF RID: 2511
		[DispId(-2147418035)]
		object right
		{
			[DispId(-2147418035)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x17000530 RID: 1328
		// (get) Token: 0x060009D0 RID: 2512
		[DispId(-2147418034)]
		object bottom
		{
			[TypeLibFunc(20)]
			[DispId(-2147418034)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x17000531 RID: 1329
		// (get) Token: 0x060009D1 RID: 2513
		[DispId(-2147412992)]
		string imeMode
		{
			[DispId(-2147412992)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17000532 RID: 1330
		// (get) Token: 0x060009D2 RID: 2514
		[DispId(-2147412991)]
		string rubyAlign
		{
			[TypeLibFunc(20)]
			[DispId(-2147412991)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17000533 RID: 1331
		// (get) Token: 0x060009D3 RID: 2515
		[DispId(-2147412990)]
		string rubyPosition
		{
			[DispId(-2147412990)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17000534 RID: 1332
		// (get) Token: 0x060009D4 RID: 2516
		[DispId(-2147412989)]
		string rubyOverhang
		{
			[TypeLibFunc(20)]
			[DispId(-2147412989)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17000535 RID: 1333
		// (get) Token: 0x060009D5 RID: 2517
		[DispId(-2147412980)]
		string textAutospace
		{
			[TypeLibFunc(20)]
			[DispId(-2147412980)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17000536 RID: 1334
		// (get) Token: 0x060009D6 RID: 2518
		[DispId(-2147412979)]
		string lineBreak
		{
			[DispId(-2147412979)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17000537 RID: 1335
		// (get) Token: 0x060009D7 RID: 2519
		[DispId(-2147412978)]
		string wordBreak
		{
			[DispId(-2147412978)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17000538 RID: 1336
		// (get) Token: 0x060009D8 RID: 2520
		[DispId(-2147412977)]
		string textJustify
		{
			[TypeLibFunc(20)]
			[DispId(-2147412977)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17000539 RID: 1337
		// (get) Token: 0x060009D9 RID: 2521
		[DispId(-2147412976)]
		string textJustifyTrim
		{
			[DispId(-2147412976)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x1700053A RID: 1338
		// (get) Token: 0x060009DA RID: 2522
		[DispId(-2147412975)]
		object textKashida
		{
			[DispId(-2147412975)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x1700053B RID: 1339
		// (get) Token: 0x060009DB RID: 2523
		[DispId(-2147412995)]
		string blockDirection
		{
			[DispId(-2147412995)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x1700053C RID: 1340
		// (get) Token: 0x060009DC RID: 2524
		[DispId(-2147412985)]
		object layoutGridChar
		{
			[DispId(-2147412985)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x1700053D RID: 1341
		// (get) Token: 0x060009DD RID: 2525
		[DispId(-2147412984)]
		object layoutGridLine
		{
			[TypeLibFunc(20)]
			[DispId(-2147412984)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x1700053E RID: 1342
		// (get) Token: 0x060009DE RID: 2526
		[DispId(-2147412983)]
		string layoutGridMode
		{
			[TypeLibFunc(20)]
			[DispId(-2147412983)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x1700053F RID: 1343
		// (get) Token: 0x060009DF RID: 2527
		[DispId(-2147412982)]
		string layoutGridType
		{
			[TypeLibFunc(20)]
			[DispId(-2147412982)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17000540 RID: 1344
		// (get) Token: 0x060009E0 RID: 2528
		[DispId(-2147413048)]
		string borderStyle
		{
			[TypeLibFunc(20)]
			[DispId(-2147413048)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17000541 RID: 1345
		// (get) Token: 0x060009E1 RID: 2529
		[DispId(-2147413058)]
		string borderColor
		{
			[TypeLibFunc(20)]
			[DispId(-2147413058)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17000542 RID: 1346
		// (get) Token: 0x060009E2 RID: 2530
		[DispId(-2147413053)]
		string borderWidth
		{
			[TypeLibFunc(20)]
			[DispId(-2147413053)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17000543 RID: 1347
		// (get) Token: 0x060009E3 RID: 2531
		[DispId(-2147413101)]
		string padding
		{
			[DispId(-2147413101)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17000544 RID: 1348
		// (get) Token: 0x060009E4 RID: 2532
		[DispId(-2147413076)]
		string margin
		{
			[TypeLibFunc(20)]
			[DispId(-2147413076)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17000545 RID: 1349
		// (get) Token: 0x060009E5 RID: 2533
		[DispId(-2147412965)]
		string accelerator
		{
			[TypeLibFunc(20)]
			[DispId(-2147412965)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17000546 RID: 1350
		// (get) Token: 0x060009E6 RID: 2534
		[DispId(-2147412973)]
		string overflowX
		{
			[TypeLibFunc(20)]
			[DispId(-2147412973)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17000547 RID: 1351
		// (get) Token: 0x060009E7 RID: 2535
		[DispId(-2147412972)]
		string overflowY
		{
			[DispId(-2147412972)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17000548 RID: 1352
		// (get) Token: 0x060009E8 RID: 2536
		[DispId(-2147413108)]
		string textTransform
		{
			[TypeLibFunc(20)]
			[DispId(-2147413108)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17000549 RID: 1353
		// (get) Token: 0x060009E9 RID: 2537
		[DispId(-2147412957)]
		string layoutFlow
		{
			[DispId(-2147412957)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x1700054A RID: 1354
		// (get) Token: 0x060009EA RID: 2538
		[DispId(-2147412954)]
		string wordWrap
		{
			[DispId(-2147412954)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x1700054B RID: 1355
		// (get) Token: 0x060009EB RID: 2539
		[DispId(-2147412953)]
		string textUnderlinePosition
		{
			[TypeLibFunc(20)]
			[DispId(-2147412953)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x1700054C RID: 1356
		// (get) Token: 0x060009EC RID: 2540
		[DispId(-2147412952)]
		bool hasLayout
		{
			[DispId(-2147412952)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700054D RID: 1357
		// (get) Token: 0x060009ED RID: 2541
		[DispId(-2147412932)]
		object scrollbarBaseColor
		{
			[TypeLibFunc(20)]
			[DispId(-2147412932)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x1700054E RID: 1358
		// (get) Token: 0x060009EE RID: 2542
		[DispId(-2147412931)]
		object scrollbarFaceColor
		{
			[TypeLibFunc(20)]
			[DispId(-2147412931)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x1700054F RID: 1359
		// (get) Token: 0x060009EF RID: 2543
		[DispId(-2147412930)]
		object scrollbar3dLightColor
		{
			[TypeLibFunc(20)]
			[DispId(-2147412930)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x17000550 RID: 1360
		// (get) Token: 0x060009F0 RID: 2544
		[DispId(-2147412929)]
		object scrollbarShadowColor
		{
			[TypeLibFunc(20)]
			[DispId(-2147412929)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x17000551 RID: 1361
		// (get) Token: 0x060009F1 RID: 2545
		[DispId(-2147412928)]
		object scrollbarHighlightColor
		{
			[TypeLibFunc(20)]
			[DispId(-2147412928)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x17000552 RID: 1362
		// (get) Token: 0x060009F2 RID: 2546
		[DispId(-2147412927)]
		object scrollbarDarkShadowColor
		{
			[TypeLibFunc(20)]
			[DispId(-2147412927)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x17000553 RID: 1363
		// (get) Token: 0x060009F3 RID: 2547
		[DispId(-2147412926)]
		object scrollbarArrowColor
		{
			[TypeLibFunc(20)]
			[DispId(-2147412926)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x17000554 RID: 1364
		// (get) Token: 0x060009F4 RID: 2548
		[DispId(-2147412916)]
		object scrollbarTrackColor
		{
			[DispId(-2147412916)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x17000555 RID: 1365
		// (get) Token: 0x060009F5 RID: 2549
		[DispId(-2147412920)]
		string writingMode
		{
			[DispId(-2147412920)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17000556 RID: 1366
		// (get) Token: 0x060009F6 RID: 2550
		[DispId(-2147412959)]
		object zoom
		{
			[DispId(-2147412959)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x17000557 RID: 1367
		// (get) Token: 0x060009F7 RID: 2551
		[DispId(-2147413030)]
		string filter
		{
			[DispId(-2147413030)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17000558 RID: 1368
		// (get) Token: 0x060009F8 RID: 2552
		[DispId(-2147412909)]
		string textAlignLast
		{
			[DispId(-2147412909)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17000559 RID: 1369
		// (get) Token: 0x060009F9 RID: 2553
		[DispId(-2147412908)]
		object textKashidaSpace
		{
			[TypeLibFunc(20)]
			[DispId(-2147412908)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x1700055A RID: 1370
		// (get) Token: 0x060009FA RID: 2554
		[DispId(-2147412904)]
		bool isBlock
		{
			[TypeLibFunc(1109)]
			[DispId(-2147412904)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700055B RID: 1371
		// (get) Token: 0x060009FB RID: 2555
		[DispId(-2147412903)]
		string textOverflow
		{
			[DispId(-2147412903)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x1700055C RID: 1372
		// (get) Token: 0x060009FC RID: 2556
		[DispId(-2147412901)]
		object minHeight
		{
			[TypeLibFunc(20)]
			[DispId(-2147412901)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x1700055D RID: 1373
		// (get) Token: 0x060009FD RID: 2557
		[DispId(-2147413065)]
		object wordSpacing
		{
			[DispId(-2147413065)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x1700055E RID: 1374
		// (get) Token: 0x060009FE RID: 2558
		[DispId(-2147413036)]
		string whiteSpace
		{
			[DispId(-2147413036)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}
	}
}
