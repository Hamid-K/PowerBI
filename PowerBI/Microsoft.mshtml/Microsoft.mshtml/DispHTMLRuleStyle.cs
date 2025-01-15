using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000053 RID: 83
	[TypeLibType(4112)]
	[InterfaceType(2)]
	[Guid("3050F55C-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public interface DispHTMLRuleStyle
	{
		// Token: 0x170002E0 RID: 736
		// (get) Token: 0x060005D6 RID: 1494
		// (set) Token: 0x060005D5 RID: 1493
		[DispId(-2147413094)]
		string fontFamily
		{
			[TypeLibFunc(20)]
			[DispId(-2147413094)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413094)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x170002E1 RID: 737
		// (get) Token: 0x060005D8 RID: 1496
		// (set) Token: 0x060005D7 RID: 1495
		[DispId(-2147413088)]
		string fontStyle
		{
			[DispId(-2147413088)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413088)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x060005DA RID: 1498
		// (set) Token: 0x060005D9 RID: 1497
		[DispId(-2147413087)]
		string fontVariant
		{
			[TypeLibFunc(20)]
			[DispId(-2147413087)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147413087)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x170002E3 RID: 739
		// (get) Token: 0x060005DC RID: 1500
		// (set) Token: 0x060005DB RID: 1499
		[DispId(-2147413085)]
		string fontWeight
		{
			[TypeLibFunc(20)]
			[DispId(-2147413085)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413085)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x170002E4 RID: 740
		// (get) Token: 0x060005DE RID: 1502
		// (set) Token: 0x060005DD RID: 1501
		[DispId(-2147413093)]
		object fontSize
		{
			[DispId(-2147413093)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413093)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170002E5 RID: 741
		// (get) Token: 0x060005E0 RID: 1504
		// (set) Token: 0x060005DF RID: 1503
		[DispId(-2147413071)]
		string font
		{
			[DispId(-2147413071)]
			[TypeLibFunc(1044)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(1044)]
			[DispId(-2147413071)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x170002E6 RID: 742
		// (get) Token: 0x060005E2 RID: 1506
		// (set) Token: 0x060005E1 RID: 1505
		[DispId(-2147413110)]
		object color
		{
			[DispId(-2147413110)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147413110)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170002E7 RID: 743
		// (get) Token: 0x060005E4 RID: 1508
		// (set) Token: 0x060005E3 RID: 1507
		[DispId(-2147413080)]
		string background
		{
			[TypeLibFunc(1044)]
			[DispId(-2147413080)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(1044)]
			[DispId(-2147413080)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x170002E8 RID: 744
		// (get) Token: 0x060005E6 RID: 1510
		// (set) Token: 0x060005E5 RID: 1509
		[DispId(-501)]
		object backgroundColor
		{
			[TypeLibFunc(20)]
			[DispId(-501)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-501)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170002E9 RID: 745
		// (get) Token: 0x060005E8 RID: 1512
		// (set) Token: 0x060005E7 RID: 1511
		[DispId(-2147413111)]
		string backgroundImage
		{
			[TypeLibFunc(20)]
			[DispId(-2147413111)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147413111)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x170002EA RID: 746
		// (get) Token: 0x060005EA RID: 1514
		// (set) Token: 0x060005E9 RID: 1513
		[DispId(-2147413068)]
		string backgroundRepeat
		{
			[TypeLibFunc(20)]
			[DispId(-2147413068)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147413068)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x170002EB RID: 747
		// (get) Token: 0x060005EC RID: 1516
		// (set) Token: 0x060005EB RID: 1515
		[DispId(-2147413067)]
		string backgroundAttachment
		{
			[DispId(-2147413067)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413067)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x170002EC RID: 748
		// (get) Token: 0x060005EE RID: 1518
		// (set) Token: 0x060005ED RID: 1517
		[DispId(-2147413066)]
		string backgroundPosition
		{
			[TypeLibFunc(1044)]
			[DispId(-2147413066)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(1044)]
			[DispId(-2147413066)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x170002ED RID: 749
		// (get) Token: 0x060005F0 RID: 1520
		// (set) Token: 0x060005EF RID: 1519
		[DispId(-2147413079)]
		object backgroundPositionX
		{
			[DispId(-2147413079)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413079)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170002EE RID: 750
		// (get) Token: 0x060005F2 RID: 1522
		// (set) Token: 0x060005F1 RID: 1521
		[DispId(-2147413078)]
		object backgroundPositionY
		{
			[TypeLibFunc(20)]
			[DispId(-2147413078)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413078)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170002EF RID: 751
		// (get) Token: 0x060005F4 RID: 1524
		// (set) Token: 0x060005F3 RID: 1523
		[DispId(-2147413065)]
		object wordSpacing
		{
			[DispId(-2147413065)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147413065)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170002F0 RID: 752
		// (get) Token: 0x060005F6 RID: 1526
		// (set) Token: 0x060005F5 RID: 1525
		[DispId(-2147413104)]
		object letterSpacing
		{
			[DispId(-2147413104)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147413104)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170002F1 RID: 753
		// (get) Token: 0x060005F8 RID: 1528
		// (set) Token: 0x060005F7 RID: 1527
		[DispId(-2147413077)]
		string textDecoration
		{
			[TypeLibFunc(20)]
			[DispId(-2147413077)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413077)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x170002F2 RID: 754
		// (get) Token: 0x060005FA RID: 1530
		// (set) Token: 0x060005F9 RID: 1529
		[DispId(-2147413089)]
		bool textDecorationNone
		{
			[TypeLibFunc(20)]
			[DispId(-2147413089)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[DispId(-2147413089)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x170002F3 RID: 755
		// (get) Token: 0x060005FC RID: 1532
		// (set) Token: 0x060005FB RID: 1531
		[DispId(-2147413091)]
		bool textDecorationUnderline
		{
			[DispId(-2147413091)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413091)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x170002F4 RID: 756
		// (get) Token: 0x060005FE RID: 1534
		// (set) Token: 0x060005FD RID: 1533
		[DispId(-2147413043)]
		bool textDecorationOverline
		{
			[DispId(-2147413043)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[DispId(-2147413043)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x170002F5 RID: 757
		// (get) Token: 0x06000600 RID: 1536
		// (set) Token: 0x060005FF RID: 1535
		[DispId(-2147413092)]
		bool textDecorationLineThrough
		{
			[TypeLibFunc(20)]
			[DispId(-2147413092)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[DispId(-2147413092)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x170002F6 RID: 758
		// (get) Token: 0x06000602 RID: 1538
		// (set) Token: 0x06000601 RID: 1537
		[DispId(-2147413090)]
		bool textDecorationBlink
		{
			[TypeLibFunc(20)]
			[DispId(-2147413090)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413090)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x170002F7 RID: 759
		// (get) Token: 0x06000604 RID: 1540
		// (set) Token: 0x06000603 RID: 1539
		[DispId(-2147413064)]
		object verticalAlign
		{
			[DispId(-2147413064)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413064)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170002F8 RID: 760
		// (get) Token: 0x06000606 RID: 1542
		// (set) Token: 0x06000605 RID: 1541
		[DispId(-2147413108)]
		string textTransform
		{
			[DispId(-2147413108)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147413108)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x170002F9 RID: 761
		// (get) Token: 0x06000608 RID: 1544
		// (set) Token: 0x06000607 RID: 1543
		[DispId(-2147418040)]
		string textAlign
		{
			[DispId(-2147418040)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147418040)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x170002FA RID: 762
		// (get) Token: 0x0600060A RID: 1546
		// (set) Token: 0x06000609 RID: 1545
		[DispId(-2147413105)]
		object textIndent
		{
			[DispId(-2147413105)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147413105)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170002FB RID: 763
		// (get) Token: 0x0600060C RID: 1548
		// (set) Token: 0x0600060B RID: 1547
		[DispId(-2147413106)]
		object lineHeight
		{
			[TypeLibFunc(20)]
			[DispId(-2147413106)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413106)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170002FC RID: 764
		// (get) Token: 0x0600060E RID: 1550
		// (set) Token: 0x0600060D RID: 1549
		[DispId(-2147413075)]
		object marginTop
		{
			[TypeLibFunc(20)]
			[DispId(-2147413075)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413075)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170002FD RID: 765
		// (get) Token: 0x06000610 RID: 1552
		// (set) Token: 0x0600060F RID: 1551
		[DispId(-2147413074)]
		object marginRight
		{
			[TypeLibFunc(20)]
			[DispId(-2147413074)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413074)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170002FE RID: 766
		// (get) Token: 0x06000612 RID: 1554
		// (set) Token: 0x06000611 RID: 1553
		[DispId(-2147413073)]
		object marginBottom
		{
			[TypeLibFunc(20)]
			[DispId(-2147413073)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413073)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170002FF RID: 767
		// (get) Token: 0x06000614 RID: 1556
		// (set) Token: 0x06000613 RID: 1555
		[DispId(-2147413072)]
		object marginLeft
		{
			[TypeLibFunc(20)]
			[DispId(-2147413072)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147413072)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17000300 RID: 768
		// (get) Token: 0x06000616 RID: 1558
		// (set) Token: 0x06000615 RID: 1557
		[DispId(-2147413076)]
		string margin
		{
			[DispId(-2147413076)]
			[TypeLibFunc(1044)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147413076)]
			[TypeLibFunc(1044)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17000301 RID: 769
		// (get) Token: 0x06000618 RID: 1560
		// (set) Token: 0x06000617 RID: 1559
		[DispId(-2147413100)]
		object paddingTop
		{
			[TypeLibFunc(20)]
			[DispId(-2147413100)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413100)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17000302 RID: 770
		// (get) Token: 0x0600061A RID: 1562
		// (set) Token: 0x06000619 RID: 1561
		[DispId(-2147413099)]
		object paddingRight
		{
			[TypeLibFunc(20)]
			[DispId(-2147413099)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147413099)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17000303 RID: 771
		// (get) Token: 0x0600061C RID: 1564
		// (set) Token: 0x0600061B RID: 1563
		[DispId(-2147413098)]
		object paddingBottom
		{
			[DispId(-2147413098)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413098)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17000304 RID: 772
		// (get) Token: 0x0600061E RID: 1566
		// (set) Token: 0x0600061D RID: 1565
		[DispId(-2147413097)]
		object paddingLeft
		{
			[TypeLibFunc(20)]
			[DispId(-2147413097)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413097)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17000305 RID: 773
		// (get) Token: 0x06000620 RID: 1568
		// (set) Token: 0x0600061F RID: 1567
		[DispId(-2147413101)]
		string padding
		{
			[TypeLibFunc(1044)]
			[DispId(-2147413101)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147413101)]
			[TypeLibFunc(1044)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17000306 RID: 774
		// (get) Token: 0x06000622 RID: 1570
		// (set) Token: 0x06000621 RID: 1569
		[DispId(-2147413063)]
		string border
		{
			[DispId(-2147413063)]
			[TypeLibFunc(1044)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(1044)]
			[DispId(-2147413063)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17000307 RID: 775
		// (get) Token: 0x06000624 RID: 1572
		// (set) Token: 0x06000623 RID: 1571
		[DispId(-2147413062)]
		string borderTop
		{
			[TypeLibFunc(20)]
			[DispId(-2147413062)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147413062)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17000308 RID: 776
		// (get) Token: 0x06000626 RID: 1574
		// (set) Token: 0x06000625 RID: 1573
		[DispId(-2147413061)]
		string borderRight
		{
			[TypeLibFunc(20)]
			[DispId(-2147413061)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413061)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17000309 RID: 777
		// (get) Token: 0x06000628 RID: 1576
		// (set) Token: 0x06000627 RID: 1575
		[DispId(-2147413060)]
		string borderBottom
		{
			[TypeLibFunc(20)]
			[DispId(-2147413060)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147413060)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x1700030A RID: 778
		// (get) Token: 0x0600062A RID: 1578
		// (set) Token: 0x06000629 RID: 1577
		[DispId(-2147413059)]
		string borderLeft
		{
			[TypeLibFunc(20)]
			[DispId(-2147413059)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147413059)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x1700030B RID: 779
		// (get) Token: 0x0600062C RID: 1580
		// (set) Token: 0x0600062B RID: 1579
		[DispId(-2147413058)]
		string borderColor
		{
			[TypeLibFunc(20)]
			[DispId(-2147413058)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413058)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x1700030C RID: 780
		// (get) Token: 0x0600062E RID: 1582
		// (set) Token: 0x0600062D RID: 1581
		[DispId(-2147413057)]
		object borderTopColor
		{
			[DispId(-2147413057)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147413057)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x1700030D RID: 781
		// (get) Token: 0x06000630 RID: 1584
		// (set) Token: 0x0600062F RID: 1583
		[DispId(-2147413056)]
		object borderRightColor
		{
			[DispId(-2147413056)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413056)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x1700030E RID: 782
		// (get) Token: 0x06000632 RID: 1586
		// (set) Token: 0x06000631 RID: 1585
		[DispId(-2147413055)]
		object borderBottomColor
		{
			[TypeLibFunc(20)]
			[DispId(-2147413055)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147413055)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x1700030F RID: 783
		// (get) Token: 0x06000634 RID: 1588
		// (set) Token: 0x06000633 RID: 1587
		[DispId(-2147413054)]
		object borderLeftColor
		{
			[TypeLibFunc(20)]
			[DispId(-2147413054)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147413054)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17000310 RID: 784
		// (get) Token: 0x06000636 RID: 1590
		// (set) Token: 0x06000635 RID: 1589
		[DispId(-2147413053)]
		string borderWidth
		{
			[DispId(-2147413053)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413053)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17000311 RID: 785
		// (get) Token: 0x06000638 RID: 1592
		// (set) Token: 0x06000637 RID: 1591
		[DispId(-2147413052)]
		object borderTopWidth
		{
			[TypeLibFunc(20)]
			[DispId(-2147413052)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147413052)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17000312 RID: 786
		// (get) Token: 0x0600063A RID: 1594
		// (set) Token: 0x06000639 RID: 1593
		[DispId(-2147413051)]
		object borderRightWidth
		{
			[TypeLibFunc(20)]
			[DispId(-2147413051)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413051)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17000313 RID: 787
		// (get) Token: 0x0600063C RID: 1596
		// (set) Token: 0x0600063B RID: 1595
		[DispId(-2147413050)]
		object borderBottomWidth
		{
			[TypeLibFunc(20)]
			[DispId(-2147413050)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147413050)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17000314 RID: 788
		// (get) Token: 0x0600063E RID: 1598
		// (set) Token: 0x0600063D RID: 1597
		[DispId(-2147413049)]
		object borderLeftWidth
		{
			[TypeLibFunc(20)]
			[DispId(-2147413049)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413049)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17000315 RID: 789
		// (get) Token: 0x06000640 RID: 1600
		// (set) Token: 0x0600063F RID: 1599
		[DispId(-2147413048)]
		string borderStyle
		{
			[TypeLibFunc(20)]
			[DispId(-2147413048)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413048)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17000316 RID: 790
		// (get) Token: 0x06000642 RID: 1602
		// (set) Token: 0x06000641 RID: 1601
		[DispId(-2147413047)]
		string borderTopStyle
		{
			[TypeLibFunc(20)]
			[DispId(-2147413047)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147413047)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17000317 RID: 791
		// (get) Token: 0x06000644 RID: 1604
		// (set) Token: 0x06000643 RID: 1603
		[DispId(-2147413046)]
		string borderRightStyle
		{
			[DispId(-2147413046)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413046)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17000318 RID: 792
		// (get) Token: 0x06000646 RID: 1606
		// (set) Token: 0x06000645 RID: 1605
		[DispId(-2147413045)]
		string borderBottomStyle
		{
			[TypeLibFunc(20)]
			[DispId(-2147413045)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147413045)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17000319 RID: 793
		// (get) Token: 0x06000648 RID: 1608
		// (set) Token: 0x06000647 RID: 1607
		[DispId(-2147413044)]
		string borderLeftStyle
		{
			[TypeLibFunc(20)]
			[DispId(-2147413044)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147413044)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x1700031A RID: 794
		// (get) Token: 0x0600064A RID: 1610
		// (set) Token: 0x06000649 RID: 1609
		[DispId(-2147418107)]
		object width
		{
			[TypeLibFunc(20)]
			[DispId(-2147418107)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147418107)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x1700031B RID: 795
		// (get) Token: 0x0600064C RID: 1612
		// (set) Token: 0x0600064B RID: 1611
		[DispId(-2147418106)]
		object height
		{
			[DispId(-2147418106)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147418106)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x1700031C RID: 796
		// (get) Token: 0x0600064E RID: 1614
		// (set) Token: 0x0600064D RID: 1613
		[DispId(-2147413042)]
		string styleFloat
		{
			[DispId(-2147413042)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147413042)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x1700031D RID: 797
		// (get) Token: 0x06000650 RID: 1616
		// (set) Token: 0x0600064F RID: 1615
		[DispId(-2147413096)]
		string clear
		{
			[TypeLibFunc(20)]
			[DispId(-2147413096)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147413096)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x1700031E RID: 798
		// (get) Token: 0x06000652 RID: 1618
		// (set) Token: 0x06000651 RID: 1617
		[DispId(-2147413041)]
		string display
		{
			[DispId(-2147413041)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413041)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x1700031F RID: 799
		// (get) Token: 0x06000654 RID: 1620
		// (set) Token: 0x06000653 RID: 1619
		[DispId(-2147413032)]
		string visibility
		{
			[TypeLibFunc(20)]
			[DispId(-2147413032)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147413032)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17000320 RID: 800
		// (get) Token: 0x06000656 RID: 1622
		// (set) Token: 0x06000655 RID: 1621
		[DispId(-2147413040)]
		string listStyleType
		{
			[DispId(-2147413040)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147413040)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17000321 RID: 801
		// (get) Token: 0x06000658 RID: 1624
		// (set) Token: 0x06000657 RID: 1623
		[DispId(-2147413039)]
		string listStylePosition
		{
			[DispId(-2147413039)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147413039)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17000322 RID: 802
		// (get) Token: 0x0600065A RID: 1626
		// (set) Token: 0x06000659 RID: 1625
		[DispId(-2147413038)]
		string listStyleImage
		{
			[TypeLibFunc(20)]
			[DispId(-2147413038)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413038)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17000323 RID: 803
		// (get) Token: 0x0600065C RID: 1628
		// (set) Token: 0x0600065B RID: 1627
		[DispId(-2147413037)]
		string listStyle
		{
			[DispId(-2147413037)]
			[TypeLibFunc(1044)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(1044)]
			[DispId(-2147413037)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17000324 RID: 804
		// (get) Token: 0x0600065E RID: 1630
		// (set) Token: 0x0600065D RID: 1629
		[DispId(-2147413036)]
		string whiteSpace
		{
			[TypeLibFunc(20)]
			[DispId(-2147413036)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413036)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17000325 RID: 805
		// (get) Token: 0x06000660 RID: 1632
		// (set) Token: 0x0600065F RID: 1631
		[DispId(-2147418108)]
		object top
		{
			[DispId(-2147418108)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147418108)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17000326 RID: 806
		// (get) Token: 0x06000662 RID: 1634
		// (set) Token: 0x06000661 RID: 1633
		[DispId(-2147418109)]
		object left
		{
			[DispId(-2147418109)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147418109)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17000327 RID: 807
		// (get) Token: 0x06000664 RID: 1636
		// (set) Token: 0x06000663 RID: 1635
		[DispId(-2147413021)]
		object zIndex
		{
			[TypeLibFunc(20)]
			[DispId(-2147413021)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413021)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17000328 RID: 808
		// (get) Token: 0x06000666 RID: 1638
		// (set) Token: 0x06000665 RID: 1637
		[DispId(-2147413102)]
		string overflow
		{
			[TypeLibFunc(20)]
			[DispId(-2147413102)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413102)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17000329 RID: 809
		// (get) Token: 0x06000668 RID: 1640
		// (set) Token: 0x06000667 RID: 1639
		[DispId(-2147413035)]
		string pageBreakBefore
		{
			[TypeLibFunc(20)]
			[DispId(-2147413035)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413035)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x1700032A RID: 810
		// (get) Token: 0x0600066A RID: 1642
		// (set) Token: 0x06000669 RID: 1641
		[DispId(-2147413034)]
		string pageBreakAfter
		{
			[TypeLibFunc(20)]
			[DispId(-2147413034)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413034)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x1700032B RID: 811
		// (get) Token: 0x0600066C RID: 1644
		// (set) Token: 0x0600066B RID: 1643
		[DispId(-2147413013)]
		string cssText
		{
			[DispId(-2147413013)]
			[TypeLibFunc(1044)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(1044)]
			[DispId(-2147413013)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x1700032C RID: 812
		// (get) Token: 0x0600066E RID: 1646
		// (set) Token: 0x0600066D RID: 1645
		[DispId(-2147413010)]
		string cursor
		{
			[TypeLibFunc(20)]
			[DispId(-2147413010)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147413010)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x1700032D RID: 813
		// (get) Token: 0x06000670 RID: 1648
		// (set) Token: 0x0600066F RID: 1647
		[DispId(-2147413020)]
		string clip
		{
			[DispId(-2147413020)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147413020)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x1700032E RID: 814
		// (get) Token: 0x06000672 RID: 1650
		// (set) Token: 0x06000671 RID: 1649
		[DispId(-2147413030)]
		string filter
		{
			[TypeLibFunc(20)]
			[DispId(-2147413030)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413030)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x06000673 RID: 1651
		[DispId(-2147417611)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void setAttribute([MarshalAs(19)] [In] string strAttributeName, [MarshalAs(27)] [In] object AttributeValue, [In] int lFlags = 1);

		// Token: 0x06000674 RID: 1652
		[DispId(-2147417610)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		object getAttribute([MarshalAs(19)] [In] string strAttributeName, [In] int lFlags = 0);

		// Token: 0x06000675 RID: 1653
		[DispId(-2147417609)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool removeAttribute([MarshalAs(19)] [In] string strAttributeName, [In] int lFlags = 1);

		// Token: 0x1700032F RID: 815
		// (get) Token: 0x06000677 RID: 1655
		// (set) Token: 0x06000676 RID: 1654
		[DispId(-2147413014)]
		string tableLayout
		{
			[TypeLibFunc(20)]
			[DispId(-2147413014)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147413014)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17000330 RID: 816
		// (get) Token: 0x06000679 RID: 1657
		// (set) Token: 0x06000678 RID: 1656
		[DispId(-2147413028)]
		string borderCollapse
		{
			[TypeLibFunc(20)]
			[DispId(-2147413028)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413028)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17000331 RID: 817
		// (get) Token: 0x0600067B RID: 1659
		// (set) Token: 0x0600067A RID: 1658
		[DispId(-2147412993)]
		string direction
		{
			[TypeLibFunc(20)]
			[DispId(-2147412993)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412993)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17000332 RID: 818
		// (get) Token: 0x0600067D RID: 1661
		// (set) Token: 0x0600067C RID: 1660
		[DispId(-2147412997)]
		string behavior
		{
			[TypeLibFunc(20)]
			[DispId(-2147412997)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412997)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17000333 RID: 819
		// (get) Token: 0x0600067F RID: 1663
		// (set) Token: 0x0600067E RID: 1662
		[DispId(-2147413022)]
		string position
		{
			[TypeLibFunc(20)]
			[DispId(-2147413022)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413022)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17000334 RID: 820
		// (get) Token: 0x06000681 RID: 1665
		// (set) Token: 0x06000680 RID: 1664
		[DispId(-2147412994)]
		string unicodeBidi
		{
			[DispId(-2147412994)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412994)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17000335 RID: 821
		// (get) Token: 0x06000683 RID: 1667
		// (set) Token: 0x06000682 RID: 1666
		[DispId(-2147418034)]
		object bottom
		{
			[TypeLibFunc(20)]
			[DispId(-2147418034)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147418034)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17000336 RID: 822
		// (get) Token: 0x06000685 RID: 1669
		// (set) Token: 0x06000684 RID: 1668
		[DispId(-2147418035)]
		object right
		{
			[TypeLibFunc(20)]
			[DispId(-2147418035)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147418035)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17000337 RID: 823
		// (get) Token: 0x06000687 RID: 1671
		// (set) Token: 0x06000686 RID: 1670
		[DispId(-2147414103)]
		int pixelBottom
		{
			[DispId(-2147414103)]
			[TypeLibFunc(84)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[TypeLibFunc(84)]
			[DispId(-2147414103)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x17000338 RID: 824
		// (get) Token: 0x06000689 RID: 1673
		// (set) Token: 0x06000688 RID: 1672
		[DispId(-2147414102)]
		int pixelRight
		{
			[TypeLibFunc(84)]
			[DispId(-2147414102)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[TypeLibFunc(84)]
			[DispId(-2147414102)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x17000339 RID: 825
		// (get) Token: 0x0600068B RID: 1675
		// (set) Token: 0x0600068A RID: 1674
		[DispId(-2147414101)]
		float posBottom
		{
			[TypeLibFunc(20)]
			[DispId(-2147414101)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147414101)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x1700033A RID: 826
		// (get) Token: 0x0600068D RID: 1677
		// (set) Token: 0x0600068C RID: 1676
		[DispId(-2147414100)]
		float posRight
		{
			[TypeLibFunc(20)]
			[DispId(-2147414100)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147414100)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x1700033B RID: 827
		// (get) Token: 0x0600068F RID: 1679
		// (set) Token: 0x0600068E RID: 1678
		[DispId(-2147412992)]
		string imeMode
		{
			[TypeLibFunc(20)]
			[DispId(-2147412992)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147412992)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x1700033C RID: 828
		// (get) Token: 0x06000691 RID: 1681
		// (set) Token: 0x06000690 RID: 1680
		[DispId(-2147412991)]
		string rubyAlign
		{
			[TypeLibFunc(20)]
			[DispId(-2147412991)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147412991)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x1700033D RID: 829
		// (get) Token: 0x06000693 RID: 1683
		// (set) Token: 0x06000692 RID: 1682
		[DispId(-2147412990)]
		string rubyPosition
		{
			[DispId(-2147412990)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412990)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x1700033E RID: 830
		// (get) Token: 0x06000695 RID: 1685
		// (set) Token: 0x06000694 RID: 1684
		[DispId(-2147412989)]
		string rubyOverhang
		{
			[TypeLibFunc(20)]
			[DispId(-2147412989)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412989)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x1700033F RID: 831
		// (get) Token: 0x06000697 RID: 1687
		// (set) Token: 0x06000696 RID: 1686
		[DispId(-2147412985)]
		object layoutGridChar
		{
			[TypeLibFunc(20)]
			[DispId(-2147412985)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412985)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17000340 RID: 832
		// (get) Token: 0x06000699 RID: 1689
		// (set) Token: 0x06000698 RID: 1688
		[DispId(-2147412984)]
		object layoutGridLine
		{
			[TypeLibFunc(20)]
			[DispId(-2147412984)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412984)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17000341 RID: 833
		// (get) Token: 0x0600069B RID: 1691
		// (set) Token: 0x0600069A RID: 1690
		[DispId(-2147412983)]
		string layoutGridMode
		{
			[TypeLibFunc(20)]
			[DispId(-2147412983)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147412983)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17000342 RID: 834
		// (get) Token: 0x0600069D RID: 1693
		// (set) Token: 0x0600069C RID: 1692
		[DispId(-2147412982)]
		string layoutGridType
		{
			[TypeLibFunc(20)]
			[DispId(-2147412982)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412982)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17000343 RID: 835
		// (get) Token: 0x0600069F RID: 1695
		// (set) Token: 0x0600069E RID: 1694
		[DispId(-2147412981)]
		string layoutGrid
		{
			[TypeLibFunc(1044)]
			[DispId(-2147412981)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(1044)]
			[DispId(-2147412981)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17000344 RID: 836
		// (get) Token: 0x060006A1 RID: 1697
		// (set) Token: 0x060006A0 RID: 1696
		[DispId(-2147412980)]
		string textAutospace
		{
			[DispId(-2147412980)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412980)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17000345 RID: 837
		// (get) Token: 0x060006A3 RID: 1699
		// (set) Token: 0x060006A2 RID: 1698
		[DispId(-2147412978)]
		string wordBreak
		{
			[DispId(-2147412978)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147412978)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17000346 RID: 838
		// (get) Token: 0x060006A5 RID: 1701
		// (set) Token: 0x060006A4 RID: 1700
		[DispId(-2147412979)]
		string lineBreak
		{
			[TypeLibFunc(20)]
			[DispId(-2147412979)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147412979)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17000347 RID: 839
		// (get) Token: 0x060006A7 RID: 1703
		// (set) Token: 0x060006A6 RID: 1702
		[DispId(-2147412977)]
		string textJustify
		{
			[TypeLibFunc(20)]
			[DispId(-2147412977)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412977)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17000348 RID: 840
		// (get) Token: 0x060006A9 RID: 1705
		// (set) Token: 0x060006A8 RID: 1704
		[DispId(-2147412976)]
		string textJustifyTrim
		{
			[DispId(-2147412976)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412976)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17000349 RID: 841
		// (get) Token: 0x060006AB RID: 1707
		// (set) Token: 0x060006AA RID: 1706
		[DispId(-2147412975)]
		object textKashida
		{
			[DispId(-2147412975)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412975)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x1700034A RID: 842
		// (get) Token: 0x060006AD RID: 1709
		// (set) Token: 0x060006AC RID: 1708
		[DispId(-2147412973)]
		string overflowX
		{
			[DispId(-2147412973)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412973)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x1700034B RID: 843
		// (get) Token: 0x060006AF RID: 1711
		// (set) Token: 0x060006AE RID: 1710
		[DispId(-2147412972)]
		string overflowY
		{
			[TypeLibFunc(20)]
			[DispId(-2147412972)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412972)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x1700034C RID: 844
		// (get) Token: 0x060006B1 RID: 1713
		// (set) Token: 0x060006B0 RID: 1712
		[DispId(-2147412965)]
		string accelerator
		{
			[DispId(-2147412965)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412965)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x1700034D RID: 845
		// (get) Token: 0x060006B3 RID: 1715
		// (set) Token: 0x060006B2 RID: 1714
		[DispId(-2147412957)]
		string layoutFlow
		{
			[DispId(-2147412957)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147412957)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x1700034E RID: 846
		// (get) Token: 0x060006B5 RID: 1717
		// (set) Token: 0x060006B4 RID: 1716
		[DispId(-2147412959)]
		object zoom
		{
			[DispId(-2147412959)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412959)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x1700034F RID: 847
		// (get) Token: 0x060006B7 RID: 1719
		// (set) Token: 0x060006B6 RID: 1718
		[DispId(-2147412954)]
		string wordWrap
		{
			[TypeLibFunc(20)]
			[DispId(-2147412954)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412954)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17000350 RID: 848
		// (get) Token: 0x060006B9 RID: 1721
		// (set) Token: 0x060006B8 RID: 1720
		[DispId(-2147412953)]
		string textUnderlinePosition
		{
			[DispId(-2147412953)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147412953)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17000351 RID: 849
		// (get) Token: 0x060006BB RID: 1723
		// (set) Token: 0x060006BA RID: 1722
		[DispId(-2147412932)]
		object scrollbarBaseColor
		{
			[TypeLibFunc(20)]
			[DispId(-2147412932)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412932)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17000352 RID: 850
		// (get) Token: 0x060006BD RID: 1725
		// (set) Token: 0x060006BC RID: 1724
		[DispId(-2147412931)]
		object scrollbarFaceColor
		{
			[DispId(-2147412931)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412931)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17000353 RID: 851
		// (get) Token: 0x060006BF RID: 1727
		// (set) Token: 0x060006BE RID: 1726
		[DispId(-2147412930)]
		object scrollbar3dLightColor
		{
			[TypeLibFunc(20)]
			[DispId(-2147412930)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412930)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17000354 RID: 852
		// (get) Token: 0x060006C1 RID: 1729
		// (set) Token: 0x060006C0 RID: 1728
		[DispId(-2147412929)]
		object scrollbarShadowColor
		{
			[DispId(-2147412929)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412929)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17000355 RID: 853
		// (get) Token: 0x060006C3 RID: 1731
		// (set) Token: 0x060006C2 RID: 1730
		[DispId(-2147412928)]
		object scrollbarHighlightColor
		{
			[TypeLibFunc(20)]
			[DispId(-2147412928)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412928)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17000356 RID: 854
		// (get) Token: 0x060006C5 RID: 1733
		// (set) Token: 0x060006C4 RID: 1732
		[DispId(-2147412927)]
		object scrollbarDarkShadowColor
		{
			[DispId(-2147412927)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412927)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17000357 RID: 855
		// (get) Token: 0x060006C7 RID: 1735
		// (set) Token: 0x060006C6 RID: 1734
		[DispId(-2147412926)]
		object scrollbarArrowColor
		{
			[DispId(-2147412926)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412926)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17000358 RID: 856
		// (get) Token: 0x060006C9 RID: 1737
		// (set) Token: 0x060006C8 RID: 1736
		[DispId(-2147412916)]
		object scrollbarTrackColor
		{
			[TypeLibFunc(20)]
			[DispId(-2147412916)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412916)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17000359 RID: 857
		// (get) Token: 0x060006CB RID: 1739
		// (set) Token: 0x060006CA RID: 1738
		[DispId(-2147412920)]
		string writingMode
		{
			[DispId(-2147412920)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147412920)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x1700035A RID: 858
		// (get) Token: 0x060006CD RID: 1741
		// (set) Token: 0x060006CC RID: 1740
		[DispId(-2147412909)]
		string textAlignLast
		{
			[TypeLibFunc(20)]
			[DispId(-2147412909)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147412909)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x1700035B RID: 859
		// (get) Token: 0x060006CF RID: 1743
		// (set) Token: 0x060006CE RID: 1742
		[DispId(-2147412908)]
		object textKashidaSpace
		{
			[TypeLibFunc(20)]
			[DispId(-2147412908)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412908)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x1700035C RID: 860
		// (get) Token: 0x060006D1 RID: 1745
		// (set) Token: 0x060006D0 RID: 1744
		[DispId(-2147412903)]
		string textOverflow
		{
			[TypeLibFunc(20)]
			[DispId(-2147412903)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147412903)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x1700035D RID: 861
		// (get) Token: 0x060006D3 RID: 1747
		// (set) Token: 0x060006D2 RID: 1746
		[DispId(-2147412901)]
		object minHeight
		{
			[DispId(-2147412901)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412901)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}
	}
}
