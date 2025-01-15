using System;
using System.Collections;
using System.Collections.Generic;
using AngleSharp.Attributes;

namespace AngleSharp.Dom.Css
{
	// Token: 0x0200032D RID: 813
	[DomName("CSSStyleDeclaration")]
	public interface ICssStyleDeclaration : ICssProperties, IEnumerable<ICssProperty>, IEnumerable, ICssNode, IStyleFormattable
	{
		// Token: 0x1700061E RID: 1566
		[DomName("item")]
		[DomAccessor(Accessors.Getter)]
		string this[int index] { get; }

		// Token: 0x1700061F RID: 1567
		// (get) Token: 0x06001732 RID: 5938
		// (set) Token: 0x06001733 RID: 5939
		[DomName("cssText")]
		string CssText { get; set; }

		// Token: 0x17000620 RID: 1568
		// (get) Token: 0x06001734 RID: 5940
		[DomName("parentRule")]
		ICssRule Parent { get; }

		// Token: 0x17000621 RID: 1569
		// (get) Token: 0x06001735 RID: 5941
		// (set) Token: 0x06001736 RID: 5942
		[DomName("accelerator")]
		string Accelerator { get; set; }

		// Token: 0x17000622 RID: 1570
		// (get) Token: 0x06001737 RID: 5943
		// (set) Token: 0x06001738 RID: 5944
		[DomName("alignContent")]
		string AlignContent { get; set; }

		// Token: 0x17000623 RID: 1571
		// (get) Token: 0x06001739 RID: 5945
		// (set) Token: 0x0600173A RID: 5946
		[DomName("alignItems")]
		string AlignItems { get; set; }

		// Token: 0x17000624 RID: 1572
		// (get) Token: 0x0600173B RID: 5947
		// (set) Token: 0x0600173C RID: 5948
		[DomName("alignmentBaseline")]
		string AlignmentBaseline { get; set; }

		// Token: 0x17000625 RID: 1573
		// (get) Token: 0x0600173D RID: 5949
		// (set) Token: 0x0600173E RID: 5950
		[DomName("alignSelf")]
		string AlignSelf { get; set; }

		// Token: 0x17000626 RID: 1574
		// (get) Token: 0x0600173F RID: 5951
		// (set) Token: 0x06001740 RID: 5952
		[DomName("animation")]
		string Animation { get; set; }

		// Token: 0x17000627 RID: 1575
		// (get) Token: 0x06001741 RID: 5953
		// (set) Token: 0x06001742 RID: 5954
		[DomName("animationDelay")]
		string AnimationDelay { get; set; }

		// Token: 0x17000628 RID: 1576
		// (get) Token: 0x06001743 RID: 5955
		// (set) Token: 0x06001744 RID: 5956
		[DomName("animationDirection")]
		string AnimationDirection { get; set; }

		// Token: 0x17000629 RID: 1577
		// (get) Token: 0x06001745 RID: 5957
		// (set) Token: 0x06001746 RID: 5958
		[DomName("animationDuration")]
		string AnimationDuration { get; set; }

		// Token: 0x1700062A RID: 1578
		// (get) Token: 0x06001747 RID: 5959
		// (set) Token: 0x06001748 RID: 5960
		[DomName("animationFillMode")]
		string AnimationFillMode { get; set; }

		// Token: 0x1700062B RID: 1579
		// (get) Token: 0x06001749 RID: 5961
		// (set) Token: 0x0600174A RID: 5962
		[DomName("animationIterationCount")]
		string AnimationIterationCount { get; set; }

		// Token: 0x1700062C RID: 1580
		// (get) Token: 0x0600174B RID: 5963
		// (set) Token: 0x0600174C RID: 5964
		[DomName("animationName")]
		string AnimationName { get; set; }

		// Token: 0x1700062D RID: 1581
		// (get) Token: 0x0600174D RID: 5965
		// (set) Token: 0x0600174E RID: 5966
		[DomName("animationPlayState")]
		string AnimationPlayState { get; set; }

		// Token: 0x1700062E RID: 1582
		// (get) Token: 0x0600174F RID: 5967
		// (set) Token: 0x06001750 RID: 5968
		[DomName("animationTimingFunction")]
		string AnimationTimingFunction { get; set; }

		// Token: 0x1700062F RID: 1583
		// (get) Token: 0x06001751 RID: 5969
		// (set) Token: 0x06001752 RID: 5970
		[DomName("backfaceVisibility")]
		string BackfaceVisibility { get; set; }

		// Token: 0x17000630 RID: 1584
		// (get) Token: 0x06001753 RID: 5971
		// (set) Token: 0x06001754 RID: 5972
		[DomName("background")]
		string Background { get; set; }

		// Token: 0x17000631 RID: 1585
		// (get) Token: 0x06001755 RID: 5973
		// (set) Token: 0x06001756 RID: 5974
		[DomName("backgroundAttachment")]
		string BackgroundAttachment { get; set; }

		// Token: 0x17000632 RID: 1586
		// (get) Token: 0x06001757 RID: 5975
		// (set) Token: 0x06001758 RID: 5976
		[DomName("backgroundClip")]
		string BackgroundClip { get; set; }

		// Token: 0x17000633 RID: 1587
		// (get) Token: 0x06001759 RID: 5977
		// (set) Token: 0x0600175A RID: 5978
		[DomName("backgroundColor")]
		string BackgroundColor { get; set; }

		// Token: 0x17000634 RID: 1588
		// (get) Token: 0x0600175B RID: 5979
		// (set) Token: 0x0600175C RID: 5980
		[DomName("backgroundImage")]
		string BackgroundImage { get; set; }

		// Token: 0x17000635 RID: 1589
		// (get) Token: 0x0600175D RID: 5981
		// (set) Token: 0x0600175E RID: 5982
		[DomName("backgroundOrigin")]
		string BackgroundOrigin { get; set; }

		// Token: 0x17000636 RID: 1590
		// (get) Token: 0x0600175F RID: 5983
		// (set) Token: 0x06001760 RID: 5984
		[DomName("backgroundPosition")]
		string BackgroundPosition { get; set; }

		// Token: 0x17000637 RID: 1591
		// (get) Token: 0x06001761 RID: 5985
		// (set) Token: 0x06001762 RID: 5986
		[DomName("backgroundPositionX")]
		string BackgroundPositionX { get; set; }

		// Token: 0x17000638 RID: 1592
		// (get) Token: 0x06001763 RID: 5987
		// (set) Token: 0x06001764 RID: 5988
		[DomName("backgroundPositionY")]
		string BackgroundPositionY { get; set; }

		// Token: 0x17000639 RID: 1593
		// (get) Token: 0x06001765 RID: 5989
		// (set) Token: 0x06001766 RID: 5990
		[DomName("backgroundRepeat")]
		string BackgroundRepeat { get; set; }

		// Token: 0x1700063A RID: 1594
		// (get) Token: 0x06001767 RID: 5991
		// (set) Token: 0x06001768 RID: 5992
		[DomName("backgroundSize")]
		string BackgroundSize { get; set; }

		// Token: 0x1700063B RID: 1595
		// (get) Token: 0x06001769 RID: 5993
		// (set) Token: 0x0600176A RID: 5994
		[DomName("baselineShift")]
		string BaselineShift { get; set; }

		// Token: 0x1700063C RID: 1596
		// (get) Token: 0x0600176B RID: 5995
		// (set) Token: 0x0600176C RID: 5996
		[DomName("behavior")]
		string Behavior { get; set; }

		// Token: 0x1700063D RID: 1597
		// (get) Token: 0x0600176D RID: 5997
		// (set) Token: 0x0600176E RID: 5998
		[DomName("border")]
		string Border { get; set; }

		// Token: 0x1700063E RID: 1598
		// (get) Token: 0x0600176F RID: 5999
		// (set) Token: 0x06001770 RID: 6000
		[DomName("bottom")]
		string Bottom { get; set; }

		// Token: 0x1700063F RID: 1599
		// (get) Token: 0x06001771 RID: 6001
		// (set) Token: 0x06001772 RID: 6002
		[DomName("borderBottom")]
		string BorderBottom { get; set; }

		// Token: 0x17000640 RID: 1600
		// (get) Token: 0x06001773 RID: 6003
		// (set) Token: 0x06001774 RID: 6004
		[DomName("borderBottomColor")]
		string BorderBottomColor { get; set; }

		// Token: 0x17000641 RID: 1601
		// (get) Token: 0x06001775 RID: 6005
		// (set) Token: 0x06001776 RID: 6006
		[DomName("borderBottomLeftRadius")]
		string BorderBottomLeftRadius { get; set; }

		// Token: 0x17000642 RID: 1602
		// (get) Token: 0x06001777 RID: 6007
		// (set) Token: 0x06001778 RID: 6008
		[DomName("borderBottomRightRadius")]
		string BorderBottomRightRadius { get; set; }

		// Token: 0x17000643 RID: 1603
		// (get) Token: 0x06001779 RID: 6009
		// (set) Token: 0x0600177A RID: 6010
		[DomName("borderBottomStyle")]
		string BorderBottomStyle { get; set; }

		// Token: 0x17000644 RID: 1604
		// (get) Token: 0x0600177B RID: 6011
		// (set) Token: 0x0600177C RID: 6012
		[DomName("borderBottomWidth")]
		string BorderBottomWidth { get; set; }

		// Token: 0x17000645 RID: 1605
		// (get) Token: 0x0600177D RID: 6013
		// (set) Token: 0x0600177E RID: 6014
		[DomName("borderCollapse")]
		string BorderCollapse { get; set; }

		// Token: 0x17000646 RID: 1606
		// (get) Token: 0x0600177F RID: 6015
		// (set) Token: 0x06001780 RID: 6016
		[DomName("borderColor")]
		string BorderColor { get; set; }

		// Token: 0x17000647 RID: 1607
		// (get) Token: 0x06001781 RID: 6017
		// (set) Token: 0x06001782 RID: 6018
		[DomName("borderImage")]
		string BorderImage { get; set; }

		// Token: 0x17000648 RID: 1608
		// (get) Token: 0x06001783 RID: 6019
		// (set) Token: 0x06001784 RID: 6020
		[DomName("borderImageOutset")]
		string BorderImageOutset { get; set; }

		// Token: 0x17000649 RID: 1609
		// (get) Token: 0x06001785 RID: 6021
		// (set) Token: 0x06001786 RID: 6022
		[DomName("borderImageRepeat")]
		string BorderImageRepeat { get; set; }

		// Token: 0x1700064A RID: 1610
		// (get) Token: 0x06001787 RID: 6023
		// (set) Token: 0x06001788 RID: 6024
		[DomName("borderImageSlice")]
		string BorderImageSlice { get; set; }

		// Token: 0x1700064B RID: 1611
		// (get) Token: 0x06001789 RID: 6025
		// (set) Token: 0x0600178A RID: 6026
		[DomName("borderImageSource")]
		string BorderImageSource { get; set; }

		// Token: 0x1700064C RID: 1612
		// (get) Token: 0x0600178B RID: 6027
		// (set) Token: 0x0600178C RID: 6028
		[DomName("borderImageWidth")]
		string BorderImageWidth { get; set; }

		// Token: 0x1700064D RID: 1613
		// (get) Token: 0x0600178D RID: 6029
		// (set) Token: 0x0600178E RID: 6030
		[DomName("borderLeft")]
		string BorderLeft { get; set; }

		// Token: 0x1700064E RID: 1614
		// (get) Token: 0x0600178F RID: 6031
		// (set) Token: 0x06001790 RID: 6032
		[DomName("borderLeftColor")]
		string BorderLeftColor { get; set; }

		// Token: 0x1700064F RID: 1615
		// (get) Token: 0x06001791 RID: 6033
		// (set) Token: 0x06001792 RID: 6034
		[DomName("borderLeftStyle")]
		string BorderLeftStyle { get; set; }

		// Token: 0x17000650 RID: 1616
		// (get) Token: 0x06001793 RID: 6035
		// (set) Token: 0x06001794 RID: 6036
		[DomName("borderLeftWidth")]
		string BorderLeftWidth { get; set; }

		// Token: 0x17000651 RID: 1617
		// (get) Token: 0x06001795 RID: 6037
		// (set) Token: 0x06001796 RID: 6038
		[DomName("borderRadius")]
		string BorderRadius { get; set; }

		// Token: 0x17000652 RID: 1618
		// (get) Token: 0x06001797 RID: 6039
		// (set) Token: 0x06001798 RID: 6040
		[DomName("borderRight")]
		string BorderRight { get; set; }

		// Token: 0x17000653 RID: 1619
		// (get) Token: 0x06001799 RID: 6041
		// (set) Token: 0x0600179A RID: 6042
		[DomName("borderRightColor")]
		string BorderRightColor { get; set; }

		// Token: 0x17000654 RID: 1620
		// (get) Token: 0x0600179B RID: 6043
		// (set) Token: 0x0600179C RID: 6044
		[DomName("borderRightStyle")]
		string BorderRightStyle { get; set; }

		// Token: 0x17000655 RID: 1621
		// (get) Token: 0x0600179D RID: 6045
		// (set) Token: 0x0600179E RID: 6046
		[DomName("borderRightWidth")]
		string BorderRightWidth { get; set; }

		// Token: 0x17000656 RID: 1622
		// (get) Token: 0x0600179F RID: 6047
		// (set) Token: 0x060017A0 RID: 6048
		[DomName("borderSpacing")]
		string BorderSpacing { get; set; }

		// Token: 0x17000657 RID: 1623
		// (get) Token: 0x060017A1 RID: 6049
		// (set) Token: 0x060017A2 RID: 6050
		[DomName("borderStyle")]
		string BorderStyle { get; set; }

		// Token: 0x17000658 RID: 1624
		// (get) Token: 0x060017A3 RID: 6051
		// (set) Token: 0x060017A4 RID: 6052
		[DomName("borderTop")]
		string BorderTop { get; set; }

		// Token: 0x17000659 RID: 1625
		// (get) Token: 0x060017A5 RID: 6053
		// (set) Token: 0x060017A6 RID: 6054
		[DomName("borderTopColor")]
		string BorderTopColor { get; set; }

		// Token: 0x1700065A RID: 1626
		// (get) Token: 0x060017A7 RID: 6055
		// (set) Token: 0x060017A8 RID: 6056
		[DomName("borderTopLeftRadius")]
		string BorderTopLeftRadius { get; set; }

		// Token: 0x1700065B RID: 1627
		// (get) Token: 0x060017A9 RID: 6057
		// (set) Token: 0x060017AA RID: 6058
		[DomName("borderTopRightRadius")]
		string BorderTopRightRadius { get; set; }

		// Token: 0x1700065C RID: 1628
		// (get) Token: 0x060017AB RID: 6059
		// (set) Token: 0x060017AC RID: 6060
		[DomName("borderTopStyle")]
		string BorderTopStyle { get; set; }

		// Token: 0x1700065D RID: 1629
		// (get) Token: 0x060017AD RID: 6061
		// (set) Token: 0x060017AE RID: 6062
		[DomName("borderTopWidth")]
		string BorderTopWidth { get; set; }

		// Token: 0x1700065E RID: 1630
		// (get) Token: 0x060017AF RID: 6063
		// (set) Token: 0x060017B0 RID: 6064
		[DomName("borderWidth")]
		string BorderWidth { get; set; }

		// Token: 0x1700065F RID: 1631
		// (get) Token: 0x060017B1 RID: 6065
		// (set) Token: 0x060017B2 RID: 6066
		[DomName("boxShadow")]
		string BoxShadow { get; set; }

		// Token: 0x17000660 RID: 1632
		// (get) Token: 0x060017B3 RID: 6067
		// (set) Token: 0x060017B4 RID: 6068
		[DomName("boxSizing")]
		string BoxSizing { get; set; }

		// Token: 0x17000661 RID: 1633
		// (get) Token: 0x060017B5 RID: 6069
		// (set) Token: 0x060017B6 RID: 6070
		[DomName("breakAfter")]
		string BreakAfter { get; set; }

		// Token: 0x17000662 RID: 1634
		// (get) Token: 0x060017B7 RID: 6071
		// (set) Token: 0x060017B8 RID: 6072
		[DomName("breakBefore")]
		string BreakBefore { get; set; }

		// Token: 0x17000663 RID: 1635
		// (get) Token: 0x060017B9 RID: 6073
		// (set) Token: 0x060017BA RID: 6074
		[DomName("breakInside")]
		string BreakInside { get; set; }

		// Token: 0x17000664 RID: 1636
		// (get) Token: 0x060017BB RID: 6075
		// (set) Token: 0x060017BC RID: 6076
		[DomName("captionSide")]
		string CaptionSide { get; set; }

		// Token: 0x17000665 RID: 1637
		// (get) Token: 0x060017BD RID: 6077
		// (set) Token: 0x060017BE RID: 6078
		[DomName("clear")]
		string Clear { get; set; }

		// Token: 0x17000666 RID: 1638
		// (get) Token: 0x060017BF RID: 6079
		// (set) Token: 0x060017C0 RID: 6080
		[DomName("clip")]
		string Clip { get; set; }

		// Token: 0x17000667 RID: 1639
		// (get) Token: 0x060017C1 RID: 6081
		// (set) Token: 0x060017C2 RID: 6082
		[DomName("clipBottom")]
		string ClipBottom { get; set; }

		// Token: 0x17000668 RID: 1640
		// (get) Token: 0x060017C3 RID: 6083
		// (set) Token: 0x060017C4 RID: 6084
		[DomName("clipLeft")]
		string ClipLeft { get; set; }

		// Token: 0x17000669 RID: 1641
		// (get) Token: 0x060017C5 RID: 6085
		// (set) Token: 0x060017C6 RID: 6086
		[DomName("clipPath")]
		string ClipPath { get; set; }

		// Token: 0x1700066A RID: 1642
		// (get) Token: 0x060017C7 RID: 6087
		// (set) Token: 0x060017C8 RID: 6088
		[DomName("clipRight")]
		string ClipRight { get; set; }

		// Token: 0x1700066B RID: 1643
		// (get) Token: 0x060017C9 RID: 6089
		// (set) Token: 0x060017CA RID: 6090
		[DomName("clipRule")]
		string ClipRule { get; set; }

		// Token: 0x1700066C RID: 1644
		// (get) Token: 0x060017CB RID: 6091
		// (set) Token: 0x060017CC RID: 6092
		[DomName("clipTop")]
		string ClipTop { get; set; }

		// Token: 0x1700066D RID: 1645
		// (get) Token: 0x060017CD RID: 6093
		// (set) Token: 0x060017CE RID: 6094
		[DomName("color")]
		string Color { get; set; }

		// Token: 0x1700066E RID: 1646
		// (get) Token: 0x060017CF RID: 6095
		// (set) Token: 0x060017D0 RID: 6096
		[DomName("colorInterpolationFilters")]
		string ColorInterpolationFilters { get; set; }

		// Token: 0x1700066F RID: 1647
		// (get) Token: 0x060017D1 RID: 6097
		// (set) Token: 0x060017D2 RID: 6098
		[DomName("columnCount")]
		string ColumnCount { get; set; }

		// Token: 0x17000670 RID: 1648
		// (get) Token: 0x060017D3 RID: 6099
		// (set) Token: 0x060017D4 RID: 6100
		[DomName("columnFill")]
		string ColumnFill { get; set; }

		// Token: 0x17000671 RID: 1649
		// (get) Token: 0x060017D5 RID: 6101
		// (set) Token: 0x060017D6 RID: 6102
		[DomName("columnGap")]
		string ColumnGap { get; set; }

		// Token: 0x17000672 RID: 1650
		// (get) Token: 0x060017D7 RID: 6103
		// (set) Token: 0x060017D8 RID: 6104
		[DomName("columnRule")]
		string ColumnRule { get; set; }

		// Token: 0x17000673 RID: 1651
		// (get) Token: 0x060017D9 RID: 6105
		// (set) Token: 0x060017DA RID: 6106
		[DomName("columnRuleColor")]
		string ColumnRuleColor { get; set; }

		// Token: 0x17000674 RID: 1652
		// (get) Token: 0x060017DB RID: 6107
		// (set) Token: 0x060017DC RID: 6108
		[DomName("columnRuleStyle")]
		string ColumnRuleStyle { get; set; }

		// Token: 0x17000675 RID: 1653
		// (get) Token: 0x060017DD RID: 6109
		// (set) Token: 0x060017DE RID: 6110
		[DomName("columnRuleWidth")]
		string ColumnRuleWidth { get; set; }

		// Token: 0x17000676 RID: 1654
		// (get) Token: 0x060017DF RID: 6111
		// (set) Token: 0x060017E0 RID: 6112
		[DomName("columns")]
		string Columns { get; set; }

		// Token: 0x17000677 RID: 1655
		// (get) Token: 0x060017E1 RID: 6113
		// (set) Token: 0x060017E2 RID: 6114
		[DomName("columnSpan")]
		string ColumnSpan { get; set; }

		// Token: 0x17000678 RID: 1656
		// (get) Token: 0x060017E3 RID: 6115
		// (set) Token: 0x060017E4 RID: 6116
		[DomName("columnWidth")]
		string ColumnWidth { get; set; }

		// Token: 0x17000679 RID: 1657
		// (get) Token: 0x060017E5 RID: 6117
		// (set) Token: 0x060017E6 RID: 6118
		[DomName("content")]
		string Content { get; set; }

		// Token: 0x1700067A RID: 1658
		// (get) Token: 0x060017E7 RID: 6119
		// (set) Token: 0x060017E8 RID: 6120
		[DomName("counterIncrement")]
		string CounterIncrement { get; set; }

		// Token: 0x1700067B RID: 1659
		// (get) Token: 0x060017E9 RID: 6121
		// (set) Token: 0x060017EA RID: 6122
		[DomName("counterReset")]
		string CounterReset { get; set; }

		// Token: 0x1700067C RID: 1660
		// (get) Token: 0x060017EB RID: 6123
		// (set) Token: 0x060017EC RID: 6124
		[DomName("cursor")]
		string Cursor { get; set; }

		// Token: 0x1700067D RID: 1661
		// (get) Token: 0x060017ED RID: 6125
		// (set) Token: 0x060017EE RID: 6126
		[DomName("direction")]
		string Direction { get; set; }

		// Token: 0x1700067E RID: 1662
		// (get) Token: 0x060017EF RID: 6127
		// (set) Token: 0x060017F0 RID: 6128
		[DomName("display")]
		string Display { get; set; }

		// Token: 0x1700067F RID: 1663
		// (get) Token: 0x060017F1 RID: 6129
		// (set) Token: 0x060017F2 RID: 6130
		[DomName("dominantBaseline")]
		string DominantBaseline { get; set; }

		// Token: 0x17000680 RID: 1664
		// (get) Token: 0x060017F3 RID: 6131
		// (set) Token: 0x060017F4 RID: 6132
		[DomName("emptyCells")]
		string EmptyCells { get; set; }

		// Token: 0x17000681 RID: 1665
		// (get) Token: 0x060017F5 RID: 6133
		// (set) Token: 0x060017F6 RID: 6134
		[DomName("enableBackground")]
		string EnableBackground { get; set; }

		// Token: 0x17000682 RID: 1666
		// (get) Token: 0x060017F7 RID: 6135
		// (set) Token: 0x060017F8 RID: 6136
		[DomName("fill")]
		string Fill { get; set; }

		// Token: 0x17000683 RID: 1667
		// (get) Token: 0x060017F9 RID: 6137
		// (set) Token: 0x060017FA RID: 6138
		[DomName("fillOpacity")]
		string FillOpacity { get; set; }

		// Token: 0x17000684 RID: 1668
		// (get) Token: 0x060017FB RID: 6139
		// (set) Token: 0x060017FC RID: 6140
		[DomName("fillRule")]
		string FillRule { get; set; }

		// Token: 0x17000685 RID: 1669
		// (get) Token: 0x060017FD RID: 6141
		// (set) Token: 0x060017FE RID: 6142
		[DomName("filter")]
		string Filter { get; set; }

		// Token: 0x17000686 RID: 1670
		// (get) Token: 0x060017FF RID: 6143
		// (set) Token: 0x06001800 RID: 6144
		[DomName("flex")]
		string Flex { get; set; }

		// Token: 0x17000687 RID: 1671
		// (get) Token: 0x06001801 RID: 6145
		// (set) Token: 0x06001802 RID: 6146
		[DomName("flexBasis")]
		string FlexBasis { get; set; }

		// Token: 0x17000688 RID: 1672
		// (get) Token: 0x06001803 RID: 6147
		// (set) Token: 0x06001804 RID: 6148
		[DomName("flexDirection")]
		string FlexDirection { get; set; }

		// Token: 0x17000689 RID: 1673
		// (get) Token: 0x06001805 RID: 6149
		// (set) Token: 0x06001806 RID: 6150
		[DomName("flexFlow")]
		string FlexFlow { get; set; }

		// Token: 0x1700068A RID: 1674
		// (get) Token: 0x06001807 RID: 6151
		// (set) Token: 0x06001808 RID: 6152
		[DomName("flexGrow")]
		string FlexGrow { get; set; }

		// Token: 0x1700068B RID: 1675
		// (get) Token: 0x06001809 RID: 6153
		// (set) Token: 0x0600180A RID: 6154
		[DomName("flexShrink")]
		string FlexShrink { get; set; }

		// Token: 0x1700068C RID: 1676
		// (get) Token: 0x0600180B RID: 6155
		// (set) Token: 0x0600180C RID: 6156
		[DomName("flexWrap")]
		string FlexWrap { get; set; }

		// Token: 0x1700068D RID: 1677
		// (get) Token: 0x0600180D RID: 6157
		// (set) Token: 0x0600180E RID: 6158
		[DomName("cssFloat")]
		string Float { get; set; }

		// Token: 0x1700068E RID: 1678
		// (get) Token: 0x0600180F RID: 6159
		// (set) Token: 0x06001810 RID: 6160
		[DomName("font")]
		string Font { get; set; }

		// Token: 0x1700068F RID: 1679
		// (get) Token: 0x06001811 RID: 6161
		// (set) Token: 0x06001812 RID: 6162
		[DomName("fontFamily")]
		string FontFamily { get; set; }

		// Token: 0x17000690 RID: 1680
		// (get) Token: 0x06001813 RID: 6163
		// (set) Token: 0x06001814 RID: 6164
		[DomName("fontFeatureSettings")]
		string FontFeatureSettings { get; set; }

		// Token: 0x17000691 RID: 1681
		// (get) Token: 0x06001815 RID: 6165
		// (set) Token: 0x06001816 RID: 6166
		[DomName("fontSize")]
		string FontSize { get; set; }

		// Token: 0x17000692 RID: 1682
		// (get) Token: 0x06001817 RID: 6167
		// (set) Token: 0x06001818 RID: 6168
		[DomName("fontSizeAdjust")]
		string FontSizeAdjust { get; set; }

		// Token: 0x17000693 RID: 1683
		// (get) Token: 0x06001819 RID: 6169
		// (set) Token: 0x0600181A RID: 6170
		[DomName("fontStretch")]
		string FontStretch { get; set; }

		// Token: 0x17000694 RID: 1684
		// (get) Token: 0x0600181B RID: 6171
		// (set) Token: 0x0600181C RID: 6172
		[DomName("fontStyle")]
		string FontStyle { get; set; }

		// Token: 0x17000695 RID: 1685
		// (get) Token: 0x0600181D RID: 6173
		// (set) Token: 0x0600181E RID: 6174
		[DomName("fontVariant")]
		string FontVariant { get; set; }

		// Token: 0x17000696 RID: 1686
		// (get) Token: 0x0600181F RID: 6175
		// (set) Token: 0x06001820 RID: 6176
		[DomName("fontWeight")]
		string FontWeight { get; set; }

		// Token: 0x17000697 RID: 1687
		// (get) Token: 0x06001821 RID: 6177
		// (set) Token: 0x06001822 RID: 6178
		[DomName("glyphOrientationHorizontal")]
		string GlyphOrientationHorizontal { get; set; }

		// Token: 0x17000698 RID: 1688
		// (get) Token: 0x06001823 RID: 6179
		// (set) Token: 0x06001824 RID: 6180
		[DomName("glyphOrientationVertical")]
		string GlyphOrientationVertical { get; set; }

		// Token: 0x17000699 RID: 1689
		// (get) Token: 0x06001825 RID: 6181
		// (set) Token: 0x06001826 RID: 6182
		[DomName("height")]
		string Height { get; set; }

		// Token: 0x1700069A RID: 1690
		// (get) Token: 0x06001827 RID: 6183
		// (set) Token: 0x06001828 RID: 6184
		[DomName("imeMode")]
		string ImeMode { get; set; }

		// Token: 0x1700069B RID: 1691
		// (get) Token: 0x06001829 RID: 6185
		// (set) Token: 0x0600182A RID: 6186
		[DomName("justifyContent")]
		string JustifyContent { get; set; }

		// Token: 0x1700069C RID: 1692
		// (get) Token: 0x0600182B RID: 6187
		// (set) Token: 0x0600182C RID: 6188
		[DomName("layoutGrid")]
		string LayoutGrid { get; set; }

		// Token: 0x1700069D RID: 1693
		// (get) Token: 0x0600182D RID: 6189
		// (set) Token: 0x0600182E RID: 6190
		[DomName("layoutGridChar")]
		string LayoutGridChar { get; set; }

		// Token: 0x1700069E RID: 1694
		// (get) Token: 0x0600182F RID: 6191
		// (set) Token: 0x06001830 RID: 6192
		[DomName("layoutGridLine")]
		string LayoutGridLine { get; set; }

		// Token: 0x1700069F RID: 1695
		// (get) Token: 0x06001831 RID: 6193
		// (set) Token: 0x06001832 RID: 6194
		[DomName("layoutGridMode")]
		string LayoutGridMode { get; set; }

		// Token: 0x170006A0 RID: 1696
		// (get) Token: 0x06001833 RID: 6195
		// (set) Token: 0x06001834 RID: 6196
		[DomName("layoutGridType")]
		string LayoutGridType { get; set; }

		// Token: 0x170006A1 RID: 1697
		// (get) Token: 0x06001835 RID: 6197
		// (set) Token: 0x06001836 RID: 6198
		[DomName("left")]
		string Left { get; set; }

		// Token: 0x170006A2 RID: 1698
		// (get) Token: 0x06001837 RID: 6199
		// (set) Token: 0x06001838 RID: 6200
		[DomName("letterSpacing")]
		string LetterSpacing { get; set; }

		// Token: 0x170006A3 RID: 1699
		// (get) Token: 0x06001839 RID: 6201
		// (set) Token: 0x0600183A RID: 6202
		[DomName("lineHeight")]
		string LineHeight { get; set; }

		// Token: 0x170006A4 RID: 1700
		// (get) Token: 0x0600183B RID: 6203
		// (set) Token: 0x0600183C RID: 6204
		[DomName("listStyle")]
		string ListStyle { get; set; }

		// Token: 0x170006A5 RID: 1701
		// (get) Token: 0x0600183D RID: 6205
		// (set) Token: 0x0600183E RID: 6206
		[DomName("listStyleImage")]
		string ListStyleImage { get; set; }

		// Token: 0x170006A6 RID: 1702
		// (get) Token: 0x0600183F RID: 6207
		// (set) Token: 0x06001840 RID: 6208
		[DomName("listStylePosition")]
		string ListStylePosition { get; set; }

		// Token: 0x170006A7 RID: 1703
		// (get) Token: 0x06001841 RID: 6209
		// (set) Token: 0x06001842 RID: 6210
		[DomName("listStyleType")]
		string ListStyleType { get; set; }

		// Token: 0x170006A8 RID: 1704
		// (get) Token: 0x06001843 RID: 6211
		// (set) Token: 0x06001844 RID: 6212
		[DomName("margin")]
		string Margin { get; set; }

		// Token: 0x170006A9 RID: 1705
		// (get) Token: 0x06001845 RID: 6213
		// (set) Token: 0x06001846 RID: 6214
		[DomName("marginBottom")]
		string MarginBottom { get; set; }

		// Token: 0x170006AA RID: 1706
		// (get) Token: 0x06001847 RID: 6215
		// (set) Token: 0x06001848 RID: 6216
		[DomName("marginLeft")]
		string MarginLeft { get; set; }

		// Token: 0x170006AB RID: 1707
		// (get) Token: 0x06001849 RID: 6217
		// (set) Token: 0x0600184A RID: 6218
		[DomName("marginRight")]
		string MarginRight { get; set; }

		// Token: 0x170006AC RID: 1708
		// (get) Token: 0x0600184B RID: 6219
		// (set) Token: 0x0600184C RID: 6220
		[DomName("marginTop")]
		string MarginTop { get; set; }

		// Token: 0x170006AD RID: 1709
		// (get) Token: 0x0600184D RID: 6221
		// (set) Token: 0x0600184E RID: 6222
		[DomName("marker")]
		string Marker { get; set; }

		// Token: 0x170006AE RID: 1710
		// (get) Token: 0x0600184F RID: 6223
		// (set) Token: 0x06001850 RID: 6224
		[DomName("markerEnd")]
		string MarkerEnd { get; set; }

		// Token: 0x170006AF RID: 1711
		// (get) Token: 0x06001851 RID: 6225
		// (set) Token: 0x06001852 RID: 6226
		[DomName("markerMid")]
		string MarkerMid { get; set; }

		// Token: 0x170006B0 RID: 1712
		// (get) Token: 0x06001853 RID: 6227
		// (set) Token: 0x06001854 RID: 6228
		[DomName("markerStart")]
		string MarkerStart { get; set; }

		// Token: 0x170006B1 RID: 1713
		// (get) Token: 0x06001855 RID: 6229
		// (set) Token: 0x06001856 RID: 6230
		[DomName("mask")]
		string Mask { get; set; }

		// Token: 0x170006B2 RID: 1714
		// (get) Token: 0x06001857 RID: 6231
		// (set) Token: 0x06001858 RID: 6232
		[DomName("maxHeight")]
		string MaxHeight { get; set; }

		// Token: 0x170006B3 RID: 1715
		// (get) Token: 0x06001859 RID: 6233
		// (set) Token: 0x0600185A RID: 6234
		[DomName("maxWidth")]
		string MaxWidth { get; set; }

		// Token: 0x170006B4 RID: 1716
		// (get) Token: 0x0600185B RID: 6235
		// (set) Token: 0x0600185C RID: 6236
		[DomName("minHeight")]
		string MinHeight { get; set; }

		// Token: 0x170006B5 RID: 1717
		// (get) Token: 0x0600185D RID: 6237
		// (set) Token: 0x0600185E RID: 6238
		[DomName("minWidth")]
		string MinWidth { get; set; }

		// Token: 0x170006B6 RID: 1718
		// (get) Token: 0x0600185F RID: 6239
		// (set) Token: 0x06001860 RID: 6240
		[DomName("opacity")]
		string Opacity { get; set; }

		// Token: 0x170006B7 RID: 1719
		// (get) Token: 0x06001861 RID: 6241
		// (set) Token: 0x06001862 RID: 6242
		[DomName("order")]
		string Order { get; set; }

		// Token: 0x170006B8 RID: 1720
		// (get) Token: 0x06001863 RID: 6243
		// (set) Token: 0x06001864 RID: 6244
		[DomName("orphans")]
		string Orphans { get; set; }

		// Token: 0x170006B9 RID: 1721
		// (get) Token: 0x06001865 RID: 6245
		// (set) Token: 0x06001866 RID: 6246
		[DomName("outline")]
		string Outline { get; set; }

		// Token: 0x170006BA RID: 1722
		// (get) Token: 0x06001867 RID: 6247
		// (set) Token: 0x06001868 RID: 6248
		[DomName("outlineColor")]
		string OutlineColor { get; set; }

		// Token: 0x170006BB RID: 1723
		// (get) Token: 0x06001869 RID: 6249
		// (set) Token: 0x0600186A RID: 6250
		[DomName("outlineStyle")]
		string OutlineStyle { get; set; }

		// Token: 0x170006BC RID: 1724
		// (get) Token: 0x0600186B RID: 6251
		// (set) Token: 0x0600186C RID: 6252
		[DomName("outlineWidth")]
		string OutlineWidth { get; set; }

		// Token: 0x170006BD RID: 1725
		// (get) Token: 0x0600186D RID: 6253
		// (set) Token: 0x0600186E RID: 6254
		[DomName("overflow")]
		string Overflow { get; set; }

		// Token: 0x170006BE RID: 1726
		// (get) Token: 0x0600186F RID: 6255
		// (set) Token: 0x06001870 RID: 6256
		[DomName("overflowX")]
		string OverflowX { get; set; }

		// Token: 0x170006BF RID: 1727
		// (get) Token: 0x06001871 RID: 6257
		// (set) Token: 0x06001872 RID: 6258
		[DomName("overflowY")]
		string OverflowY { get; set; }

		// Token: 0x170006C0 RID: 1728
		// (get) Token: 0x06001873 RID: 6259
		// (set) Token: 0x06001874 RID: 6260
		[DomName("overflowWrap")]
		string OverflowWrap { get; set; }

		// Token: 0x170006C1 RID: 1729
		// (get) Token: 0x06001875 RID: 6261
		// (set) Token: 0x06001876 RID: 6262
		[DomName("padding")]
		string Padding { get; set; }

		// Token: 0x170006C2 RID: 1730
		// (get) Token: 0x06001877 RID: 6263
		// (set) Token: 0x06001878 RID: 6264
		[DomName("paddingBottom")]
		string PaddingBottom { get; set; }

		// Token: 0x170006C3 RID: 1731
		// (get) Token: 0x06001879 RID: 6265
		// (set) Token: 0x0600187A RID: 6266
		[DomName("paddingLeft")]
		string PaddingLeft { get; set; }

		// Token: 0x170006C4 RID: 1732
		// (get) Token: 0x0600187B RID: 6267
		// (set) Token: 0x0600187C RID: 6268
		[DomName("paddingRight")]
		string PaddingRight { get; set; }

		// Token: 0x170006C5 RID: 1733
		// (get) Token: 0x0600187D RID: 6269
		// (set) Token: 0x0600187E RID: 6270
		[DomName("paddingTop")]
		string PaddingTop { get; set; }

		// Token: 0x170006C6 RID: 1734
		// (get) Token: 0x0600187F RID: 6271
		// (set) Token: 0x06001880 RID: 6272
		[DomName("pageBreakAfter")]
		string PageBreakAfter { get; set; }

		// Token: 0x170006C7 RID: 1735
		// (get) Token: 0x06001881 RID: 6273
		// (set) Token: 0x06001882 RID: 6274
		[DomName("pageBreakBefore")]
		string PageBreakBefore { get; set; }

		// Token: 0x170006C8 RID: 1736
		// (get) Token: 0x06001883 RID: 6275
		// (set) Token: 0x06001884 RID: 6276
		[DomName("pageBreakInside")]
		string PageBreakInside { get; set; }

		// Token: 0x170006C9 RID: 1737
		// (get) Token: 0x06001885 RID: 6277
		// (set) Token: 0x06001886 RID: 6278
		[DomName("perspective")]
		string Perspective { get; set; }

		// Token: 0x170006CA RID: 1738
		// (get) Token: 0x06001887 RID: 6279
		// (set) Token: 0x06001888 RID: 6280
		[DomName("perspectiveOrigin")]
		string PerspectiveOrigin { get; set; }

		// Token: 0x170006CB RID: 1739
		// (get) Token: 0x06001889 RID: 6281
		// (set) Token: 0x0600188A RID: 6282
		[DomName("pointerEvents")]
		string PointerEvents { get; set; }

		// Token: 0x170006CC RID: 1740
		// (get) Token: 0x0600188B RID: 6283
		// (set) Token: 0x0600188C RID: 6284
		[DomName("position")]
		string Position { get; set; }

		// Token: 0x170006CD RID: 1741
		// (get) Token: 0x0600188D RID: 6285
		// (set) Token: 0x0600188E RID: 6286
		[DomName("quotes")]
		string Quotes { get; set; }

		// Token: 0x170006CE RID: 1742
		// (get) Token: 0x0600188F RID: 6287
		// (set) Token: 0x06001890 RID: 6288
		[DomName("right")]
		string Right { get; set; }

		// Token: 0x170006CF RID: 1743
		// (get) Token: 0x06001891 RID: 6289
		// (set) Token: 0x06001892 RID: 6290
		[DomName("rubyAlign")]
		string RubyAlign { get; set; }

		// Token: 0x170006D0 RID: 1744
		// (get) Token: 0x06001893 RID: 6291
		// (set) Token: 0x06001894 RID: 6292
		[DomName("rubyOverhang")]
		string RubyOverhang { get; set; }

		// Token: 0x170006D1 RID: 1745
		// (get) Token: 0x06001895 RID: 6293
		// (set) Token: 0x06001896 RID: 6294
		[DomName("rubyPosition")]
		string RubyPosition { get; set; }

		// Token: 0x170006D2 RID: 1746
		// (get) Token: 0x06001897 RID: 6295
		// (set) Token: 0x06001898 RID: 6296
		[DomName("scrollbar3dLightColor")]
		string Scrollbar3dLightColor { get; set; }

		// Token: 0x170006D3 RID: 1747
		// (get) Token: 0x06001899 RID: 6297
		// (set) Token: 0x0600189A RID: 6298
		[DomName("scrollbarArrowColor")]
		string ScrollbarArrowColor { get; set; }

		// Token: 0x170006D4 RID: 1748
		// (get) Token: 0x0600189B RID: 6299
		// (set) Token: 0x0600189C RID: 6300
		[DomName("scrollbarDarkShadowColor")]
		string ScrollbarDarkShadowColor { get; set; }

		// Token: 0x170006D5 RID: 1749
		// (get) Token: 0x0600189D RID: 6301
		// (set) Token: 0x0600189E RID: 6302
		[DomName("scrollbarFaceColor")]
		string ScrollbarFaceColor { get; set; }

		// Token: 0x170006D6 RID: 1750
		// (get) Token: 0x0600189F RID: 6303
		// (set) Token: 0x060018A0 RID: 6304
		[DomName("scrollbarHighlightColor")]
		string ScrollbarHighlightColor { get; set; }

		// Token: 0x170006D7 RID: 1751
		// (get) Token: 0x060018A1 RID: 6305
		// (set) Token: 0x060018A2 RID: 6306
		[DomName("scrollbarShadowColor")]
		string ScrollbarShadowColor { get; set; }

		// Token: 0x170006D8 RID: 1752
		// (get) Token: 0x060018A3 RID: 6307
		// (set) Token: 0x060018A4 RID: 6308
		[DomName("scrollbarTrackColor")]
		string ScrollbarTrackColor { get; set; }

		// Token: 0x170006D9 RID: 1753
		// (get) Token: 0x060018A5 RID: 6309
		// (set) Token: 0x060018A6 RID: 6310
		[DomName("stroke")]
		string Stroke { get; set; }

		// Token: 0x170006DA RID: 1754
		// (get) Token: 0x060018A7 RID: 6311
		// (set) Token: 0x060018A8 RID: 6312
		[DomName("strokeDasharray")]
		string StrokeDasharray { get; set; }

		// Token: 0x170006DB RID: 1755
		// (get) Token: 0x060018A9 RID: 6313
		// (set) Token: 0x060018AA RID: 6314
		[DomName("strokeDashoffset")]
		string StrokeDashoffset { get; set; }

		// Token: 0x170006DC RID: 1756
		// (get) Token: 0x060018AB RID: 6315
		// (set) Token: 0x060018AC RID: 6316
		[DomName("strokeLinecap")]
		string StrokeLinecap { get; set; }

		// Token: 0x170006DD RID: 1757
		// (get) Token: 0x060018AD RID: 6317
		// (set) Token: 0x060018AE RID: 6318
		[DomName("strokeLinejoin")]
		string StrokeLinejoin { get; set; }

		// Token: 0x170006DE RID: 1758
		// (get) Token: 0x060018AF RID: 6319
		// (set) Token: 0x060018B0 RID: 6320
		[DomName("strokeMiterlimit")]
		string StrokeMiterlimit { get; set; }

		// Token: 0x170006DF RID: 1759
		// (get) Token: 0x060018B1 RID: 6321
		// (set) Token: 0x060018B2 RID: 6322
		[DomName("strokeOpacity")]
		string StrokeOpacity { get; set; }

		// Token: 0x170006E0 RID: 1760
		// (get) Token: 0x060018B3 RID: 6323
		// (set) Token: 0x060018B4 RID: 6324
		[DomName("strokeWidth")]
		string StrokeWidth { get; set; }

		// Token: 0x170006E1 RID: 1761
		// (get) Token: 0x060018B5 RID: 6325
		// (set) Token: 0x060018B6 RID: 6326
		[DomName("tableLayout")]
		string TableLayout { get; set; }

		// Token: 0x170006E2 RID: 1762
		// (get) Token: 0x060018B7 RID: 6327
		// (set) Token: 0x060018B8 RID: 6328
		[DomName("textAlign")]
		string TextAlign { get; set; }

		// Token: 0x170006E3 RID: 1763
		// (get) Token: 0x060018B9 RID: 6329
		// (set) Token: 0x060018BA RID: 6330
		[DomName("textAlignLast")]
		string TextAlignLast { get; set; }

		// Token: 0x170006E4 RID: 1764
		// (get) Token: 0x060018BB RID: 6331
		// (set) Token: 0x060018BC RID: 6332
		[DomName("textAnchor")]
		string TextAnchor { get; set; }

		// Token: 0x170006E5 RID: 1765
		// (get) Token: 0x060018BD RID: 6333
		// (set) Token: 0x060018BE RID: 6334
		[DomName("textAutospace")]
		string TextAutospace { get; set; }

		// Token: 0x170006E6 RID: 1766
		// (get) Token: 0x060018BF RID: 6335
		// (set) Token: 0x060018C0 RID: 6336
		[DomName("textDecoration")]
		string TextDecoration { get; set; }

		// Token: 0x170006E7 RID: 1767
		// (get) Token: 0x060018C1 RID: 6337
		// (set) Token: 0x060018C2 RID: 6338
		[DomName("textIndent")]
		string TextIndent { get; set; }

		// Token: 0x170006E8 RID: 1768
		// (get) Token: 0x060018C3 RID: 6339
		// (set) Token: 0x060018C4 RID: 6340
		[DomName("textJustify")]
		string TextJustify { get; set; }

		// Token: 0x170006E9 RID: 1769
		// (get) Token: 0x060018C5 RID: 6341
		// (set) Token: 0x060018C6 RID: 6342
		[DomName("textOverflow")]
		string TextOverflow { get; set; }

		// Token: 0x170006EA RID: 1770
		// (get) Token: 0x060018C7 RID: 6343
		// (set) Token: 0x060018C8 RID: 6344
		[DomName("textShadow")]
		string TextShadow { get; set; }

		// Token: 0x170006EB RID: 1771
		// (get) Token: 0x060018C9 RID: 6345
		// (set) Token: 0x060018CA RID: 6346
		[DomName("textTransform")]
		string TextTransform { get; set; }

		// Token: 0x170006EC RID: 1772
		// (get) Token: 0x060018CB RID: 6347
		// (set) Token: 0x060018CC RID: 6348
		[DomName("textUnderlinePosition")]
		string TextUnderlinePosition { get; set; }

		// Token: 0x170006ED RID: 1773
		// (get) Token: 0x060018CD RID: 6349
		// (set) Token: 0x060018CE RID: 6350
		[DomName("top")]
		string Top { get; set; }

		// Token: 0x170006EE RID: 1774
		// (get) Token: 0x060018CF RID: 6351
		// (set) Token: 0x060018D0 RID: 6352
		[DomName("transform")]
		string Transform { get; set; }

		// Token: 0x170006EF RID: 1775
		// (get) Token: 0x060018D1 RID: 6353
		// (set) Token: 0x060018D2 RID: 6354
		[DomName("transformOrigin")]
		string TransformOrigin { get; set; }

		// Token: 0x170006F0 RID: 1776
		// (get) Token: 0x060018D3 RID: 6355
		// (set) Token: 0x060018D4 RID: 6356
		[DomName("transformStyle")]
		string TransformStyle { get; set; }

		// Token: 0x170006F1 RID: 1777
		// (get) Token: 0x060018D5 RID: 6357
		// (set) Token: 0x060018D6 RID: 6358
		[DomName("transition")]
		string Transition { get; set; }

		// Token: 0x170006F2 RID: 1778
		// (get) Token: 0x060018D7 RID: 6359
		// (set) Token: 0x060018D8 RID: 6360
		[DomName("transitionDelay")]
		string TransitionDelay { get; set; }

		// Token: 0x170006F3 RID: 1779
		// (get) Token: 0x060018D9 RID: 6361
		// (set) Token: 0x060018DA RID: 6362
		[DomName("transitionDuration")]
		string TransitionDuration { get; set; }

		// Token: 0x170006F4 RID: 1780
		// (get) Token: 0x060018DB RID: 6363
		// (set) Token: 0x060018DC RID: 6364
		[DomName("transitionProperty")]
		string TransitionProperty { get; set; }

		// Token: 0x170006F5 RID: 1781
		// (get) Token: 0x060018DD RID: 6365
		// (set) Token: 0x060018DE RID: 6366
		[DomName("transitionTimingFunction")]
		string TransitionTimingFunction { get; set; }

		// Token: 0x170006F6 RID: 1782
		// (get) Token: 0x060018DF RID: 6367
		// (set) Token: 0x060018E0 RID: 6368
		[DomName("unicodeBidi")]
		string UnicodeBidi { get; set; }

		// Token: 0x170006F7 RID: 1783
		// (get) Token: 0x060018E1 RID: 6369
		// (set) Token: 0x060018E2 RID: 6370
		[DomName("verticalAlign")]
		string VerticalAlign { get; set; }

		// Token: 0x170006F8 RID: 1784
		// (get) Token: 0x060018E3 RID: 6371
		// (set) Token: 0x060018E4 RID: 6372
		[DomName("visibility")]
		string Visibility { get; set; }

		// Token: 0x170006F9 RID: 1785
		// (get) Token: 0x060018E5 RID: 6373
		// (set) Token: 0x060018E6 RID: 6374
		[DomName("whiteSpace")]
		string WhiteSpace { get; set; }

		// Token: 0x170006FA RID: 1786
		// (get) Token: 0x060018E7 RID: 6375
		// (set) Token: 0x060018E8 RID: 6376
		[DomName("widows")]
		string Widows { get; set; }

		// Token: 0x170006FB RID: 1787
		// (get) Token: 0x060018E9 RID: 6377
		// (set) Token: 0x060018EA RID: 6378
		[DomName("width")]
		string Width { get; set; }

		// Token: 0x170006FC RID: 1788
		// (get) Token: 0x060018EB RID: 6379
		// (set) Token: 0x060018EC RID: 6380
		[DomName("wordBreak")]
		string WordBreak { get; set; }

		// Token: 0x170006FD RID: 1789
		// (get) Token: 0x060018ED RID: 6381
		// (set) Token: 0x060018EE RID: 6382
		[DomName("wordSpacing")]
		string WordSpacing { get; set; }

		// Token: 0x170006FE RID: 1790
		// (get) Token: 0x060018EF RID: 6383
		// (set) Token: 0x060018F0 RID: 6384
		[DomName("writingMode")]
		string WritingMode { get; set; }

		// Token: 0x170006FF RID: 1791
		// (get) Token: 0x060018F1 RID: 6385
		// (set) Token: 0x060018F2 RID: 6386
		[DomName("zIndex")]
		string ZIndex { get; set; }

		// Token: 0x17000700 RID: 1792
		// (get) Token: 0x060018F3 RID: 6387
		// (set) Token: 0x060018F4 RID: 6388
		[DomName("zoom")]
		string Zoom { get; set; }
	}
}
