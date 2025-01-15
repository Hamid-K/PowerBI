using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000064 RID: 100
	[TypeLibType(2)]
	[ClassInterface(0)]
	[Guid("3050F3DC-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public class HTMLCurrentStyleClass : DispHTMLCurrentStyle, HTMLCurrentStyle, IHTMLCurrentStyle, IHTMLCurrentStyle2, IHTMLCurrentStyle3
	{
		// Token: 0x060009FF RID: 2559
		[MethodImpl(4096, MethodCodeType = 3)]
		public extern HTMLCurrentStyleClass();

		// Token: 0x1700055F RID: 1375
		// (get) Token: 0x06000A00 RID: 2560
		[DispId(-2147413022)]
		public virtual extern string position
		{
			[DispId(-2147413022)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17000560 RID: 1376
		// (get) Token: 0x06000A01 RID: 2561
		[DispId(-2147413042)]
		public virtual extern string styleFloat
		{
			[TypeLibFunc(20)]
			[DispId(-2147413042)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17000561 RID: 1377
		// (get) Token: 0x06000A02 RID: 2562
		[DispId(-2147413110)]
		public virtual extern object color
		{
			[TypeLibFunc(20)]
			[DispId(-2147413110)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x17000562 RID: 1378
		// (get) Token: 0x06000A03 RID: 2563
		[DispId(-501)]
		public virtual extern object backgroundColor
		{
			[DispId(-501)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x17000563 RID: 1379
		// (get) Token: 0x06000A04 RID: 2564
		[DispId(-2147413094)]
		public virtual extern string fontFamily
		{
			[TypeLibFunc(20)]
			[DispId(-2147413094)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17000564 RID: 1380
		// (get) Token: 0x06000A05 RID: 2565
		[DispId(-2147413088)]
		public virtual extern string fontStyle
		{
			[DispId(-2147413088)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17000565 RID: 1381
		// (get) Token: 0x06000A06 RID: 2566
		[DispId(-2147413087)]
		public virtual extern string fontVariant
		{
			[DispId(-2147413087)]
			[TypeLibFunc(84)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17000566 RID: 1382
		// (get) Token: 0x06000A07 RID: 2567
		[DispId(-2147413085)]
		public virtual extern object fontWeight
		{
			[DispId(-2147413085)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x17000567 RID: 1383
		// (get) Token: 0x06000A08 RID: 2568
		[DispId(-2147413093)]
		public virtual extern object fontSize
		{
			[DispId(-2147413093)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x17000568 RID: 1384
		// (get) Token: 0x06000A09 RID: 2569
		[DispId(-2147413111)]
		public virtual extern string backgroundImage
		{
			[DispId(-2147413111)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17000569 RID: 1385
		// (get) Token: 0x06000A0A RID: 2570
		[DispId(-2147413079)]
		public virtual extern object backgroundPositionX
		{
			[DispId(-2147413079)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x1700056A RID: 1386
		// (get) Token: 0x06000A0B RID: 2571
		[DispId(-2147413078)]
		public virtual extern object backgroundPositionY
		{
			[DispId(-2147413078)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x1700056B RID: 1387
		// (get) Token: 0x06000A0C RID: 2572
		[DispId(-2147413068)]
		public virtual extern string backgroundRepeat
		{
			[DispId(-2147413068)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x1700056C RID: 1388
		// (get) Token: 0x06000A0D RID: 2573
		[DispId(-2147413054)]
		public virtual extern object borderLeftColor
		{
			[DispId(-2147413054)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x1700056D RID: 1389
		// (get) Token: 0x06000A0E RID: 2574
		[DispId(-2147413057)]
		public virtual extern object borderTopColor
		{
			[TypeLibFunc(20)]
			[DispId(-2147413057)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x1700056E RID: 1390
		// (get) Token: 0x06000A0F RID: 2575
		[DispId(-2147413056)]
		public virtual extern object borderRightColor
		{
			[DispId(-2147413056)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x1700056F RID: 1391
		// (get) Token: 0x06000A10 RID: 2576
		[DispId(-2147413055)]
		public virtual extern object borderBottomColor
		{
			[DispId(-2147413055)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x17000570 RID: 1392
		// (get) Token: 0x06000A11 RID: 2577
		[DispId(-2147413047)]
		public virtual extern string borderTopStyle
		{
			[DispId(-2147413047)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17000571 RID: 1393
		// (get) Token: 0x06000A12 RID: 2578
		[DispId(-2147413046)]
		public virtual extern string borderRightStyle
		{
			[DispId(-2147413046)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17000572 RID: 1394
		// (get) Token: 0x06000A13 RID: 2579
		[DispId(-2147413045)]
		public virtual extern string borderBottomStyle
		{
			[TypeLibFunc(20)]
			[DispId(-2147413045)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17000573 RID: 1395
		// (get) Token: 0x06000A14 RID: 2580
		[DispId(-2147413044)]
		public virtual extern string borderLeftStyle
		{
			[DispId(-2147413044)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17000574 RID: 1396
		// (get) Token: 0x06000A15 RID: 2581
		[DispId(-2147413052)]
		public virtual extern object borderTopWidth
		{
			[DispId(-2147413052)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x17000575 RID: 1397
		// (get) Token: 0x06000A16 RID: 2582
		[DispId(-2147413051)]
		public virtual extern object borderRightWidth
		{
			[DispId(-2147413051)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x17000576 RID: 1398
		// (get) Token: 0x06000A17 RID: 2583
		[DispId(-2147413050)]
		public virtual extern object borderBottomWidth
		{
			[TypeLibFunc(20)]
			[DispId(-2147413050)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x17000577 RID: 1399
		// (get) Token: 0x06000A18 RID: 2584
		[DispId(-2147413049)]
		public virtual extern object borderLeftWidth
		{
			[DispId(-2147413049)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x17000578 RID: 1400
		// (get) Token: 0x06000A19 RID: 2585
		[DispId(-2147418109)]
		public virtual extern object left
		{
			[DispId(-2147418109)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x17000579 RID: 1401
		// (get) Token: 0x06000A1A RID: 2586
		[DispId(-2147418108)]
		public virtual extern object top
		{
			[DispId(-2147418108)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x1700057A RID: 1402
		// (get) Token: 0x06000A1B RID: 2587
		[DispId(-2147418107)]
		public virtual extern object width
		{
			[DispId(-2147418107)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x1700057B RID: 1403
		// (get) Token: 0x06000A1C RID: 2588
		[DispId(-2147418106)]
		public virtual extern object height
		{
			[TypeLibFunc(20)]
			[DispId(-2147418106)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x1700057C RID: 1404
		// (get) Token: 0x06000A1D RID: 2589
		[DispId(-2147413097)]
		public virtual extern object paddingLeft
		{
			[TypeLibFunc(20)]
			[DispId(-2147413097)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x1700057D RID: 1405
		// (get) Token: 0x06000A1E RID: 2590
		[DispId(-2147413100)]
		public virtual extern object paddingTop
		{
			[TypeLibFunc(20)]
			[DispId(-2147413100)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x1700057E RID: 1406
		// (get) Token: 0x06000A1F RID: 2591
		[DispId(-2147413099)]
		public virtual extern object paddingRight
		{
			[DispId(-2147413099)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x1700057F RID: 1407
		// (get) Token: 0x06000A20 RID: 2592
		[DispId(-2147413098)]
		public virtual extern object paddingBottom
		{
			[TypeLibFunc(20)]
			[DispId(-2147413098)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x17000580 RID: 1408
		// (get) Token: 0x06000A21 RID: 2593
		[DispId(-2147418040)]
		public virtual extern string textAlign
		{
			[TypeLibFunc(20)]
			[DispId(-2147418040)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17000581 RID: 1409
		// (get) Token: 0x06000A22 RID: 2594
		[DispId(-2147413077)]
		public virtual extern string textDecoration
		{
			[DispId(-2147413077)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17000582 RID: 1410
		// (get) Token: 0x06000A23 RID: 2595
		[DispId(-2147413041)]
		public virtual extern string display
		{
			[DispId(-2147413041)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17000583 RID: 1411
		// (get) Token: 0x06000A24 RID: 2596
		[DispId(-2147413032)]
		public virtual extern string visibility
		{
			[DispId(-2147413032)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17000584 RID: 1412
		// (get) Token: 0x06000A25 RID: 2597
		[DispId(-2147413021)]
		public virtual extern object zIndex
		{
			[TypeLibFunc(20)]
			[DispId(-2147413021)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x17000585 RID: 1413
		// (get) Token: 0x06000A26 RID: 2598
		[DispId(-2147413104)]
		public virtual extern object letterSpacing
		{
			[DispId(-2147413104)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x17000586 RID: 1414
		// (get) Token: 0x06000A27 RID: 2599
		[DispId(-2147413106)]
		public virtual extern object lineHeight
		{
			[DispId(-2147413106)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x17000587 RID: 1415
		// (get) Token: 0x06000A28 RID: 2600
		[DispId(-2147413105)]
		public virtual extern object textIndent
		{
			[TypeLibFunc(20)]
			[DispId(-2147413105)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x17000588 RID: 1416
		// (get) Token: 0x06000A29 RID: 2601
		[DispId(-2147413064)]
		public virtual extern object verticalAlign
		{
			[DispId(-2147413064)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x17000589 RID: 1417
		// (get) Token: 0x06000A2A RID: 2602
		[DispId(-2147413067)]
		public virtual extern string backgroundAttachment
		{
			[TypeLibFunc(20)]
			[DispId(-2147413067)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x1700058A RID: 1418
		// (get) Token: 0x06000A2B RID: 2603
		[DispId(-2147413075)]
		public virtual extern object marginTop
		{
			[TypeLibFunc(20)]
			[DispId(-2147413075)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x1700058B RID: 1419
		// (get) Token: 0x06000A2C RID: 2604
		[DispId(-2147413074)]
		public virtual extern object marginRight
		{
			[TypeLibFunc(20)]
			[DispId(-2147413074)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x1700058C RID: 1420
		// (get) Token: 0x06000A2D RID: 2605
		[DispId(-2147413073)]
		public virtual extern object marginBottom
		{
			[DispId(-2147413073)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x1700058D RID: 1421
		// (get) Token: 0x06000A2E RID: 2606
		[DispId(-2147413072)]
		public virtual extern object marginLeft
		{
			[TypeLibFunc(20)]
			[DispId(-2147413072)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x1700058E RID: 1422
		// (get) Token: 0x06000A2F RID: 2607
		[DispId(-2147413096)]
		public virtual extern string clear
		{
			[DispId(-2147413096)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x1700058F RID: 1423
		// (get) Token: 0x06000A30 RID: 2608
		[DispId(-2147413040)]
		public virtual extern string listStyleType
		{
			[DispId(-2147413040)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17000590 RID: 1424
		// (get) Token: 0x06000A31 RID: 2609
		[DispId(-2147413039)]
		public virtual extern string listStylePosition
		{
			[DispId(-2147413039)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17000591 RID: 1425
		// (get) Token: 0x06000A32 RID: 2610
		[DispId(-2147413038)]
		public virtual extern string listStyleImage
		{
			[DispId(-2147413038)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17000592 RID: 1426
		// (get) Token: 0x06000A33 RID: 2611
		[DispId(-2147413019)]
		public virtual extern object clipTop
		{
			[TypeLibFunc(20)]
			[DispId(-2147413019)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x17000593 RID: 1427
		// (get) Token: 0x06000A34 RID: 2612
		[DispId(-2147413018)]
		public virtual extern object clipRight
		{
			[TypeLibFunc(20)]
			[DispId(-2147413018)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x17000594 RID: 1428
		// (get) Token: 0x06000A35 RID: 2613
		[DispId(-2147413017)]
		public virtual extern object clipBottom
		{
			[TypeLibFunc(20)]
			[DispId(-2147413017)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x17000595 RID: 1429
		// (get) Token: 0x06000A36 RID: 2614
		[DispId(-2147413016)]
		public virtual extern object clipLeft
		{
			[TypeLibFunc(20)]
			[DispId(-2147413016)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x17000596 RID: 1430
		// (get) Token: 0x06000A37 RID: 2615
		[DispId(-2147413102)]
		public virtual extern string overflow
		{
			[DispId(-2147413102)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17000597 RID: 1431
		// (get) Token: 0x06000A38 RID: 2616
		[DispId(-2147413035)]
		public virtual extern string pageBreakBefore
		{
			[DispId(-2147413035)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17000598 RID: 1432
		// (get) Token: 0x06000A39 RID: 2617
		[DispId(-2147413034)]
		public virtual extern string pageBreakAfter
		{
			[DispId(-2147413034)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17000599 RID: 1433
		// (get) Token: 0x06000A3A RID: 2618
		[DispId(-2147413010)]
		public virtual extern string cursor
		{
			[TypeLibFunc(20)]
			[DispId(-2147413010)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x1700059A RID: 1434
		// (get) Token: 0x06000A3B RID: 2619
		[DispId(-2147413014)]
		public virtual extern string tableLayout
		{
			[TypeLibFunc(20)]
			[DispId(-2147413014)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x1700059B RID: 1435
		// (get) Token: 0x06000A3C RID: 2620
		[DispId(-2147413028)]
		public virtual extern string borderCollapse
		{
			[TypeLibFunc(20)]
			[DispId(-2147413028)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x1700059C RID: 1436
		// (get) Token: 0x06000A3D RID: 2621
		[DispId(-2147412993)]
		public virtual extern string direction
		{
			[TypeLibFunc(20)]
			[DispId(-2147412993)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x1700059D RID: 1437
		// (get) Token: 0x06000A3E RID: 2622
		[DispId(-2147412997)]
		public virtual extern string behavior
		{
			[TypeLibFunc(20)]
			[DispId(-2147412997)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x06000A3F RID: 2623
		[DispId(-2147417610)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		public virtual extern object getAttribute([MarshalAs(19)] [In] string strAttributeName, [In] int lFlags = 0);

		// Token: 0x1700059E RID: 1438
		// (get) Token: 0x06000A40 RID: 2624
		[DispId(-2147412994)]
		public virtual extern string unicodeBidi
		{
			[DispId(-2147412994)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x1700059F RID: 1439
		// (get) Token: 0x06000A41 RID: 2625
		[DispId(-2147418035)]
		public virtual extern object right
		{
			[DispId(-2147418035)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x170005A0 RID: 1440
		// (get) Token: 0x06000A42 RID: 2626
		[DispId(-2147418034)]
		public virtual extern object bottom
		{
			[TypeLibFunc(20)]
			[DispId(-2147418034)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x170005A1 RID: 1441
		// (get) Token: 0x06000A43 RID: 2627
		[DispId(-2147412992)]
		public virtual extern string imeMode
		{
			[DispId(-2147412992)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170005A2 RID: 1442
		// (get) Token: 0x06000A44 RID: 2628
		[DispId(-2147412991)]
		public virtual extern string rubyAlign
		{
			[TypeLibFunc(20)]
			[DispId(-2147412991)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170005A3 RID: 1443
		// (get) Token: 0x06000A45 RID: 2629
		[DispId(-2147412990)]
		public virtual extern string rubyPosition
		{
			[DispId(-2147412990)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170005A4 RID: 1444
		// (get) Token: 0x06000A46 RID: 2630
		[DispId(-2147412989)]
		public virtual extern string rubyOverhang
		{
			[DispId(-2147412989)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170005A5 RID: 1445
		// (get) Token: 0x06000A47 RID: 2631
		[DispId(-2147412980)]
		public virtual extern string textAutospace
		{
			[TypeLibFunc(20)]
			[DispId(-2147412980)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170005A6 RID: 1446
		// (get) Token: 0x06000A48 RID: 2632
		[DispId(-2147412979)]
		public virtual extern string lineBreak
		{
			[DispId(-2147412979)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170005A7 RID: 1447
		// (get) Token: 0x06000A49 RID: 2633
		[DispId(-2147412978)]
		public virtual extern string wordBreak
		{
			[DispId(-2147412978)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170005A8 RID: 1448
		// (get) Token: 0x06000A4A RID: 2634
		[DispId(-2147412977)]
		public virtual extern string textJustify
		{
			[TypeLibFunc(20)]
			[DispId(-2147412977)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170005A9 RID: 1449
		// (get) Token: 0x06000A4B RID: 2635
		[DispId(-2147412976)]
		public virtual extern string textJustifyTrim
		{
			[TypeLibFunc(20)]
			[DispId(-2147412976)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170005AA RID: 1450
		// (get) Token: 0x06000A4C RID: 2636
		[DispId(-2147412975)]
		public virtual extern object textKashida
		{
			[DispId(-2147412975)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x170005AB RID: 1451
		// (get) Token: 0x06000A4D RID: 2637
		[DispId(-2147412995)]
		public virtual extern string blockDirection
		{
			[TypeLibFunc(20)]
			[DispId(-2147412995)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170005AC RID: 1452
		// (get) Token: 0x06000A4E RID: 2638
		[DispId(-2147412985)]
		public virtual extern object layoutGridChar
		{
			[TypeLibFunc(20)]
			[DispId(-2147412985)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x170005AD RID: 1453
		// (get) Token: 0x06000A4F RID: 2639
		[DispId(-2147412984)]
		public virtual extern object layoutGridLine
		{
			[TypeLibFunc(20)]
			[DispId(-2147412984)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x170005AE RID: 1454
		// (get) Token: 0x06000A50 RID: 2640
		[DispId(-2147412983)]
		public virtual extern string layoutGridMode
		{
			[TypeLibFunc(20)]
			[DispId(-2147412983)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170005AF RID: 1455
		// (get) Token: 0x06000A51 RID: 2641
		[DispId(-2147412982)]
		public virtual extern string layoutGridType
		{
			[DispId(-2147412982)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170005B0 RID: 1456
		// (get) Token: 0x06000A52 RID: 2642
		[DispId(-2147413048)]
		public virtual extern string borderStyle
		{
			[TypeLibFunc(20)]
			[DispId(-2147413048)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170005B1 RID: 1457
		// (get) Token: 0x06000A53 RID: 2643
		[DispId(-2147413058)]
		public virtual extern string borderColor
		{
			[DispId(-2147413058)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170005B2 RID: 1458
		// (get) Token: 0x06000A54 RID: 2644
		[DispId(-2147413053)]
		public virtual extern string borderWidth
		{
			[DispId(-2147413053)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170005B3 RID: 1459
		// (get) Token: 0x06000A55 RID: 2645
		[DispId(-2147413101)]
		public virtual extern string padding
		{
			[DispId(-2147413101)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170005B4 RID: 1460
		// (get) Token: 0x06000A56 RID: 2646
		[DispId(-2147413076)]
		public virtual extern string margin
		{
			[TypeLibFunc(20)]
			[DispId(-2147413076)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170005B5 RID: 1461
		// (get) Token: 0x06000A57 RID: 2647
		[DispId(-2147412965)]
		public virtual extern string accelerator
		{
			[DispId(-2147412965)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170005B6 RID: 1462
		// (get) Token: 0x06000A58 RID: 2648
		[DispId(-2147412973)]
		public virtual extern string overflowX
		{
			[TypeLibFunc(20)]
			[DispId(-2147412973)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170005B7 RID: 1463
		// (get) Token: 0x06000A59 RID: 2649
		[DispId(-2147412972)]
		public virtual extern string overflowY
		{
			[TypeLibFunc(20)]
			[DispId(-2147412972)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170005B8 RID: 1464
		// (get) Token: 0x06000A5A RID: 2650
		[DispId(-2147413108)]
		public virtual extern string textTransform
		{
			[TypeLibFunc(20)]
			[DispId(-2147413108)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170005B9 RID: 1465
		// (get) Token: 0x06000A5B RID: 2651
		[DispId(-2147412957)]
		public virtual extern string layoutFlow
		{
			[TypeLibFunc(20)]
			[DispId(-2147412957)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170005BA RID: 1466
		// (get) Token: 0x06000A5C RID: 2652
		[DispId(-2147412954)]
		public virtual extern string wordWrap
		{
			[DispId(-2147412954)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170005BB RID: 1467
		// (get) Token: 0x06000A5D RID: 2653
		[DispId(-2147412953)]
		public virtual extern string textUnderlinePosition
		{
			[DispId(-2147412953)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170005BC RID: 1468
		// (get) Token: 0x06000A5E RID: 2654
		[DispId(-2147412952)]
		public virtual extern bool hasLayout
		{
			[TypeLibFunc(20)]
			[DispId(-2147412952)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170005BD RID: 1469
		// (get) Token: 0x06000A5F RID: 2655
		[DispId(-2147412932)]
		public virtual extern object scrollbarBaseColor
		{
			[DispId(-2147412932)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x170005BE RID: 1470
		// (get) Token: 0x06000A60 RID: 2656
		[DispId(-2147412931)]
		public virtual extern object scrollbarFaceColor
		{
			[TypeLibFunc(20)]
			[DispId(-2147412931)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x170005BF RID: 1471
		// (get) Token: 0x06000A61 RID: 2657
		[DispId(-2147412930)]
		public virtual extern object scrollbar3dLightColor
		{
			[DispId(-2147412930)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x170005C0 RID: 1472
		// (get) Token: 0x06000A62 RID: 2658
		[DispId(-2147412929)]
		public virtual extern object scrollbarShadowColor
		{
			[TypeLibFunc(20)]
			[DispId(-2147412929)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x170005C1 RID: 1473
		// (get) Token: 0x06000A63 RID: 2659
		[DispId(-2147412928)]
		public virtual extern object scrollbarHighlightColor
		{
			[DispId(-2147412928)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x170005C2 RID: 1474
		// (get) Token: 0x06000A64 RID: 2660
		[DispId(-2147412927)]
		public virtual extern object scrollbarDarkShadowColor
		{
			[TypeLibFunc(20)]
			[DispId(-2147412927)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x170005C3 RID: 1475
		// (get) Token: 0x06000A65 RID: 2661
		[DispId(-2147412926)]
		public virtual extern object scrollbarArrowColor
		{
			[TypeLibFunc(20)]
			[DispId(-2147412926)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x170005C4 RID: 1476
		// (get) Token: 0x06000A66 RID: 2662
		[DispId(-2147412916)]
		public virtual extern object scrollbarTrackColor
		{
			[TypeLibFunc(20)]
			[DispId(-2147412916)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x170005C5 RID: 1477
		// (get) Token: 0x06000A67 RID: 2663
		[DispId(-2147412920)]
		public virtual extern string writingMode
		{
			[TypeLibFunc(20)]
			[DispId(-2147412920)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170005C6 RID: 1478
		// (get) Token: 0x06000A68 RID: 2664
		[DispId(-2147412959)]
		public virtual extern object zoom
		{
			[DispId(-2147412959)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x170005C7 RID: 1479
		// (get) Token: 0x06000A69 RID: 2665
		[DispId(-2147413030)]
		public virtual extern string filter
		{
			[TypeLibFunc(20)]
			[DispId(-2147413030)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170005C8 RID: 1480
		// (get) Token: 0x06000A6A RID: 2666
		[DispId(-2147412909)]
		public virtual extern string textAlignLast
		{
			[TypeLibFunc(20)]
			[DispId(-2147412909)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170005C9 RID: 1481
		// (get) Token: 0x06000A6B RID: 2667
		[DispId(-2147412908)]
		public virtual extern object textKashidaSpace
		{
			[TypeLibFunc(20)]
			[DispId(-2147412908)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x170005CA RID: 1482
		// (get) Token: 0x06000A6C RID: 2668
		[DispId(-2147412904)]
		public virtual extern bool isBlock
		{
			[DispId(-2147412904)]
			[TypeLibFunc(1109)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170005CB RID: 1483
		// (get) Token: 0x06000A6D RID: 2669
		[DispId(-2147412903)]
		public virtual extern string textOverflow
		{
			[DispId(-2147412903)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170005CC RID: 1484
		// (get) Token: 0x06000A6E RID: 2670
		[DispId(-2147412901)]
		public virtual extern object minHeight
		{
			[DispId(-2147412901)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x170005CD RID: 1485
		// (get) Token: 0x06000A6F RID: 2671
		[DispId(-2147413065)]
		public virtual extern object wordSpacing
		{
			[TypeLibFunc(20)]
			[DispId(-2147413065)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x170005CE RID: 1486
		// (get) Token: 0x06000A70 RID: 2672
		[DispId(-2147413036)]
		public virtual extern string whiteSpace
		{
			[DispId(-2147413036)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170005CF RID: 1487
		// (get) Token: 0x06000A71 RID: 2673
		public virtual extern string IHTMLCurrentStyle_position
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170005D0 RID: 1488
		// (get) Token: 0x06000A72 RID: 2674
		public virtual extern string IHTMLCurrentStyle_styleFloat
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170005D1 RID: 1489
		// (get) Token: 0x06000A73 RID: 2675
		public virtual extern object IHTMLCurrentStyle_color
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x170005D2 RID: 1490
		// (get) Token: 0x06000A74 RID: 2676
		public virtual extern object IHTMLCurrentStyle_backgroundColor
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x170005D3 RID: 1491
		// (get) Token: 0x06000A75 RID: 2677
		public virtual extern string IHTMLCurrentStyle_fontFamily
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170005D4 RID: 1492
		// (get) Token: 0x06000A76 RID: 2678
		public virtual extern string IHTMLCurrentStyle_fontStyle
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170005D5 RID: 1493
		// (get) Token: 0x06000A77 RID: 2679
		public virtual extern string IHTMLCurrentStyle_fontVariant
		{
			[TypeLibFunc(84)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170005D6 RID: 1494
		// (get) Token: 0x06000A78 RID: 2680
		public virtual extern object IHTMLCurrentStyle_fontWeight
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x170005D7 RID: 1495
		// (get) Token: 0x06000A79 RID: 2681
		public virtual extern object IHTMLCurrentStyle_fontSize
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x170005D8 RID: 1496
		// (get) Token: 0x06000A7A RID: 2682
		public virtual extern string IHTMLCurrentStyle_backgroundImage
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170005D9 RID: 1497
		// (get) Token: 0x06000A7B RID: 2683
		public virtual extern object IHTMLCurrentStyle_backgroundPositionX
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x170005DA RID: 1498
		// (get) Token: 0x06000A7C RID: 2684
		public virtual extern object IHTMLCurrentStyle_backgroundPositionY
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x170005DB RID: 1499
		// (get) Token: 0x06000A7D RID: 2685
		public virtual extern string IHTMLCurrentStyle_backgroundRepeat
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170005DC RID: 1500
		// (get) Token: 0x06000A7E RID: 2686
		public virtual extern object IHTMLCurrentStyle_borderLeftColor
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x170005DD RID: 1501
		// (get) Token: 0x06000A7F RID: 2687
		public virtual extern object IHTMLCurrentStyle_borderTopColor
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x170005DE RID: 1502
		// (get) Token: 0x06000A80 RID: 2688
		public virtual extern object IHTMLCurrentStyle_borderRightColor
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x170005DF RID: 1503
		// (get) Token: 0x06000A81 RID: 2689
		public virtual extern object IHTMLCurrentStyle_borderBottomColor
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x170005E0 RID: 1504
		// (get) Token: 0x06000A82 RID: 2690
		public virtual extern string IHTMLCurrentStyle_borderTopStyle
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170005E1 RID: 1505
		// (get) Token: 0x06000A83 RID: 2691
		public virtual extern string IHTMLCurrentStyle_borderRightStyle
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170005E2 RID: 1506
		// (get) Token: 0x06000A84 RID: 2692
		public virtual extern string IHTMLCurrentStyle_borderBottomStyle
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170005E3 RID: 1507
		// (get) Token: 0x06000A85 RID: 2693
		public virtual extern string IHTMLCurrentStyle_borderLeftStyle
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170005E4 RID: 1508
		// (get) Token: 0x06000A86 RID: 2694
		public virtual extern object IHTMLCurrentStyle_borderTopWidth
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x170005E5 RID: 1509
		// (get) Token: 0x06000A87 RID: 2695
		public virtual extern object IHTMLCurrentStyle_borderRightWidth
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x170005E6 RID: 1510
		// (get) Token: 0x06000A88 RID: 2696
		public virtual extern object IHTMLCurrentStyle_borderBottomWidth
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x170005E7 RID: 1511
		// (get) Token: 0x06000A89 RID: 2697
		public virtual extern object IHTMLCurrentStyle_borderLeftWidth
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x170005E8 RID: 1512
		// (get) Token: 0x06000A8A RID: 2698
		public virtual extern object IHTMLCurrentStyle_left
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x170005E9 RID: 1513
		// (get) Token: 0x06000A8B RID: 2699
		public virtual extern object IHTMLCurrentStyle_top
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x170005EA RID: 1514
		// (get) Token: 0x06000A8C RID: 2700
		public virtual extern object IHTMLCurrentStyle_width
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x170005EB RID: 1515
		// (get) Token: 0x06000A8D RID: 2701
		public virtual extern object IHTMLCurrentStyle_height
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x170005EC RID: 1516
		// (get) Token: 0x06000A8E RID: 2702
		public virtual extern object IHTMLCurrentStyle_paddingLeft
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x170005ED RID: 1517
		// (get) Token: 0x06000A8F RID: 2703
		public virtual extern object IHTMLCurrentStyle_paddingTop
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x170005EE RID: 1518
		// (get) Token: 0x06000A90 RID: 2704
		public virtual extern object IHTMLCurrentStyle_paddingRight
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x170005EF RID: 1519
		// (get) Token: 0x06000A91 RID: 2705
		public virtual extern object IHTMLCurrentStyle_paddingBottom
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x170005F0 RID: 1520
		// (get) Token: 0x06000A92 RID: 2706
		public virtual extern string IHTMLCurrentStyle_textAlign
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170005F1 RID: 1521
		// (get) Token: 0x06000A93 RID: 2707
		public virtual extern string IHTMLCurrentStyle_textDecoration
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170005F2 RID: 1522
		// (get) Token: 0x06000A94 RID: 2708
		public virtual extern string IHTMLCurrentStyle_display
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170005F3 RID: 1523
		// (get) Token: 0x06000A95 RID: 2709
		public virtual extern string IHTMLCurrentStyle_visibility
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170005F4 RID: 1524
		// (get) Token: 0x06000A96 RID: 2710
		public virtual extern object IHTMLCurrentStyle_zIndex
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x170005F5 RID: 1525
		// (get) Token: 0x06000A97 RID: 2711
		public virtual extern object IHTMLCurrentStyle_letterSpacing
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x170005F6 RID: 1526
		// (get) Token: 0x06000A98 RID: 2712
		public virtual extern object IHTMLCurrentStyle_lineHeight
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x170005F7 RID: 1527
		// (get) Token: 0x06000A99 RID: 2713
		public virtual extern object IHTMLCurrentStyle_textIndent
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x170005F8 RID: 1528
		// (get) Token: 0x06000A9A RID: 2714
		public virtual extern object IHTMLCurrentStyle_verticalAlign
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x170005F9 RID: 1529
		// (get) Token: 0x06000A9B RID: 2715
		public virtual extern string IHTMLCurrentStyle_backgroundAttachment
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170005FA RID: 1530
		// (get) Token: 0x06000A9C RID: 2716
		public virtual extern object IHTMLCurrentStyle_marginTop
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x170005FB RID: 1531
		// (get) Token: 0x06000A9D RID: 2717
		public virtual extern object IHTMLCurrentStyle_marginRight
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x170005FC RID: 1532
		// (get) Token: 0x06000A9E RID: 2718
		public virtual extern object IHTMLCurrentStyle_marginBottom
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x170005FD RID: 1533
		// (get) Token: 0x06000A9F RID: 2719
		public virtual extern object IHTMLCurrentStyle_marginLeft
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x170005FE RID: 1534
		// (get) Token: 0x06000AA0 RID: 2720
		public virtual extern string IHTMLCurrentStyle_clear
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170005FF RID: 1535
		// (get) Token: 0x06000AA1 RID: 2721
		public virtual extern string IHTMLCurrentStyle_listStyleType
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17000600 RID: 1536
		// (get) Token: 0x06000AA2 RID: 2722
		public virtual extern string IHTMLCurrentStyle_listStylePosition
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17000601 RID: 1537
		// (get) Token: 0x06000AA3 RID: 2723
		public virtual extern string IHTMLCurrentStyle_listStyleImage
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17000602 RID: 1538
		// (get) Token: 0x06000AA4 RID: 2724
		public virtual extern object IHTMLCurrentStyle_clipTop
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x17000603 RID: 1539
		// (get) Token: 0x06000AA5 RID: 2725
		public virtual extern object IHTMLCurrentStyle_clipRight
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x17000604 RID: 1540
		// (get) Token: 0x06000AA6 RID: 2726
		public virtual extern object IHTMLCurrentStyle_clipBottom
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x17000605 RID: 1541
		// (get) Token: 0x06000AA7 RID: 2727
		public virtual extern object IHTMLCurrentStyle_clipLeft
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x17000606 RID: 1542
		// (get) Token: 0x06000AA8 RID: 2728
		public virtual extern string IHTMLCurrentStyle_overflow
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17000607 RID: 1543
		// (get) Token: 0x06000AA9 RID: 2729
		public virtual extern string IHTMLCurrentStyle_pageBreakBefore
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17000608 RID: 1544
		// (get) Token: 0x06000AAA RID: 2730
		public virtual extern string IHTMLCurrentStyle_pageBreakAfter
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17000609 RID: 1545
		// (get) Token: 0x06000AAB RID: 2731
		public virtual extern string IHTMLCurrentStyle_cursor
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x1700060A RID: 1546
		// (get) Token: 0x06000AAC RID: 2732
		public virtual extern string IHTMLCurrentStyle_tableLayout
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x1700060B RID: 1547
		// (get) Token: 0x06000AAD RID: 2733
		public virtual extern string IHTMLCurrentStyle_borderCollapse
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x1700060C RID: 1548
		// (get) Token: 0x06000AAE RID: 2734
		public virtual extern string IHTMLCurrentStyle_direction
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x1700060D RID: 1549
		// (get) Token: 0x06000AAF RID: 2735
		public virtual extern string IHTMLCurrentStyle_behavior
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x06000AB0 RID: 2736
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		public virtual extern object IHTMLCurrentStyle_getAttribute([MarshalAs(19)] [In] string strAttributeName, [In] int lFlags = 0);

		// Token: 0x1700060E RID: 1550
		// (get) Token: 0x06000AB1 RID: 2737
		public virtual extern string IHTMLCurrentStyle_unicodeBidi
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x1700060F RID: 1551
		// (get) Token: 0x06000AB2 RID: 2738
		public virtual extern object IHTMLCurrentStyle_right
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x17000610 RID: 1552
		// (get) Token: 0x06000AB3 RID: 2739
		public virtual extern object IHTMLCurrentStyle_bottom
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x17000611 RID: 1553
		// (get) Token: 0x06000AB4 RID: 2740
		public virtual extern string IHTMLCurrentStyle_imeMode
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17000612 RID: 1554
		// (get) Token: 0x06000AB5 RID: 2741
		public virtual extern string IHTMLCurrentStyle_rubyAlign
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17000613 RID: 1555
		// (get) Token: 0x06000AB6 RID: 2742
		public virtual extern string IHTMLCurrentStyle_rubyPosition
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17000614 RID: 1556
		// (get) Token: 0x06000AB7 RID: 2743
		public virtual extern string IHTMLCurrentStyle_rubyOverhang
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17000615 RID: 1557
		// (get) Token: 0x06000AB8 RID: 2744
		public virtual extern string IHTMLCurrentStyle_textAutospace
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17000616 RID: 1558
		// (get) Token: 0x06000AB9 RID: 2745
		public virtual extern string IHTMLCurrentStyle_lineBreak
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17000617 RID: 1559
		// (get) Token: 0x06000ABA RID: 2746
		public virtual extern string IHTMLCurrentStyle_wordBreak
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17000618 RID: 1560
		// (get) Token: 0x06000ABB RID: 2747
		public virtual extern string IHTMLCurrentStyle_textJustify
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17000619 RID: 1561
		// (get) Token: 0x06000ABC RID: 2748
		public virtual extern string IHTMLCurrentStyle_textJustifyTrim
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x1700061A RID: 1562
		// (get) Token: 0x06000ABD RID: 2749
		public virtual extern object IHTMLCurrentStyle_textKashida
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x1700061B RID: 1563
		// (get) Token: 0x06000ABE RID: 2750
		public virtual extern string IHTMLCurrentStyle_blockDirection
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x1700061C RID: 1564
		// (get) Token: 0x06000ABF RID: 2751
		public virtual extern object IHTMLCurrentStyle_layoutGridChar
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x1700061D RID: 1565
		// (get) Token: 0x06000AC0 RID: 2752
		public virtual extern object IHTMLCurrentStyle_layoutGridLine
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x1700061E RID: 1566
		// (get) Token: 0x06000AC1 RID: 2753
		public virtual extern string IHTMLCurrentStyle_layoutGridMode
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x1700061F RID: 1567
		// (get) Token: 0x06000AC2 RID: 2754
		public virtual extern string IHTMLCurrentStyle_layoutGridType
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17000620 RID: 1568
		// (get) Token: 0x06000AC3 RID: 2755
		public virtual extern string IHTMLCurrentStyle_borderStyle
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17000621 RID: 1569
		// (get) Token: 0x06000AC4 RID: 2756
		public virtual extern string IHTMLCurrentStyle_borderColor
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17000622 RID: 1570
		// (get) Token: 0x06000AC5 RID: 2757
		public virtual extern string IHTMLCurrentStyle_borderWidth
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17000623 RID: 1571
		// (get) Token: 0x06000AC6 RID: 2758
		public virtual extern string IHTMLCurrentStyle_padding
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17000624 RID: 1572
		// (get) Token: 0x06000AC7 RID: 2759
		public virtual extern string IHTMLCurrentStyle_margin
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17000625 RID: 1573
		// (get) Token: 0x06000AC8 RID: 2760
		public virtual extern string IHTMLCurrentStyle_accelerator
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17000626 RID: 1574
		// (get) Token: 0x06000AC9 RID: 2761
		public virtual extern string IHTMLCurrentStyle_overflowX
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17000627 RID: 1575
		// (get) Token: 0x06000ACA RID: 2762
		public virtual extern string IHTMLCurrentStyle_overflowY
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17000628 RID: 1576
		// (get) Token: 0x06000ACB RID: 2763
		public virtual extern string IHTMLCurrentStyle_textTransform
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17000629 RID: 1577
		// (get) Token: 0x06000ACC RID: 2764
		public virtual extern string IHTMLCurrentStyle2_layoutFlow
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x1700062A RID: 1578
		// (get) Token: 0x06000ACD RID: 2765
		public virtual extern string IHTMLCurrentStyle2_wordWrap
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x1700062B RID: 1579
		// (get) Token: 0x06000ACE RID: 2766
		public virtual extern string IHTMLCurrentStyle2_textUnderlinePosition
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x1700062C RID: 1580
		// (get) Token: 0x06000ACF RID: 2767
		public virtual extern bool IHTMLCurrentStyle2_hasLayout
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700062D RID: 1581
		// (get) Token: 0x06000AD0 RID: 2768
		public virtual extern object IHTMLCurrentStyle2_scrollbarBaseColor
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x1700062E RID: 1582
		// (get) Token: 0x06000AD1 RID: 2769
		public virtual extern object IHTMLCurrentStyle2_scrollbarFaceColor
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x1700062F RID: 1583
		// (get) Token: 0x06000AD2 RID: 2770
		public virtual extern object IHTMLCurrentStyle2_scrollbar3dLightColor
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x17000630 RID: 1584
		// (get) Token: 0x06000AD3 RID: 2771
		public virtual extern object IHTMLCurrentStyle2_scrollbarShadowColor
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x17000631 RID: 1585
		// (get) Token: 0x06000AD4 RID: 2772
		public virtual extern object IHTMLCurrentStyle2_scrollbarHighlightColor
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x17000632 RID: 1586
		// (get) Token: 0x06000AD5 RID: 2773
		public virtual extern object IHTMLCurrentStyle2_scrollbarDarkShadowColor
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x17000633 RID: 1587
		// (get) Token: 0x06000AD6 RID: 2774
		public virtual extern object IHTMLCurrentStyle2_scrollbarArrowColor
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x17000634 RID: 1588
		// (get) Token: 0x06000AD7 RID: 2775
		public virtual extern object IHTMLCurrentStyle2_scrollbarTrackColor
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x17000635 RID: 1589
		// (get) Token: 0x06000AD8 RID: 2776
		public virtual extern string IHTMLCurrentStyle2_writingMode
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17000636 RID: 1590
		// (get) Token: 0x06000AD9 RID: 2777
		public virtual extern object IHTMLCurrentStyle2_zoom
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x17000637 RID: 1591
		// (get) Token: 0x06000ADA RID: 2778
		public virtual extern string IHTMLCurrentStyle2_filter
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17000638 RID: 1592
		// (get) Token: 0x06000ADB RID: 2779
		public virtual extern string IHTMLCurrentStyle2_textAlignLast
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17000639 RID: 1593
		// (get) Token: 0x06000ADC RID: 2780
		public virtual extern object IHTMLCurrentStyle2_textKashidaSpace
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x1700063A RID: 1594
		// (get) Token: 0x06000ADD RID: 2781
		public virtual extern bool IHTMLCurrentStyle2_isBlock
		{
			[TypeLibFunc(1109)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
		}

		// Token: 0x1700063B RID: 1595
		// (get) Token: 0x06000ADE RID: 2782
		public virtual extern string IHTMLCurrentStyle3_textOverflow
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x1700063C RID: 1596
		// (get) Token: 0x06000ADF RID: 2783
		public virtual extern object IHTMLCurrentStyle3_minHeight
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x1700063D RID: 1597
		// (get) Token: 0x06000AE0 RID: 2784
		public virtual extern object IHTMLCurrentStyle3_wordSpacing
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x1700063E RID: 1598
		// (get) Token: 0x06000AE1 RID: 2785
		public virtual extern string IHTMLCurrentStyle3_whiteSpace
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}
	}
}
