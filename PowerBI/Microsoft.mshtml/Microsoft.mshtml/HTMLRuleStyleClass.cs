using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000054 RID: 84
	[ClassInterface(0)]
	[TypeLibType(2)]
	[Guid("3050F3D0-98B5-11CF-BB82-00AA00BDCE0B")]
	[ComImport]
	public class HTMLRuleStyleClass : DispHTMLRuleStyle, HTMLRuleStyle, IHTMLRuleStyle, IHTMLRuleStyle2, IHTMLRuleStyle3, IHTMLRuleStyle4
	{
		// Token: 0x060006D4 RID: 1748
		[MethodImpl(4096, MethodCodeType = 3)]
		public extern HTMLRuleStyleClass();

		// Token: 0x1700035E RID: 862
		// (get) Token: 0x060006D6 RID: 1750
		// (set) Token: 0x060006D5 RID: 1749
		[DispId(-2147413094)]
		public virtual extern string fontFamily
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

		// Token: 0x1700035F RID: 863
		// (get) Token: 0x060006D8 RID: 1752
		// (set) Token: 0x060006D7 RID: 1751
		[DispId(-2147413088)]
		public virtual extern string fontStyle
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

		// Token: 0x17000360 RID: 864
		// (get) Token: 0x060006DA RID: 1754
		// (set) Token: 0x060006D9 RID: 1753
		[DispId(-2147413087)]
		public virtual extern string fontVariant
		{
			[TypeLibFunc(20)]
			[DispId(-2147413087)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413087)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17000361 RID: 865
		// (get) Token: 0x060006DC RID: 1756
		// (set) Token: 0x060006DB RID: 1755
		[DispId(-2147413085)]
		public virtual extern string fontWeight
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

		// Token: 0x17000362 RID: 866
		// (get) Token: 0x060006DE RID: 1758
		// (set) Token: 0x060006DD RID: 1757
		[DispId(-2147413093)]
		public virtual extern object fontSize
		{
			[TypeLibFunc(20)]
			[DispId(-2147413093)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147413093)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17000363 RID: 867
		// (get) Token: 0x060006E0 RID: 1760
		// (set) Token: 0x060006DF RID: 1759
		[DispId(-2147413071)]
		public virtual extern string font
		{
			[TypeLibFunc(1044)]
			[DispId(-2147413071)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147413071)]
			[TypeLibFunc(1044)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17000364 RID: 868
		// (get) Token: 0x060006E2 RID: 1762
		// (set) Token: 0x060006E1 RID: 1761
		[DispId(-2147413110)]
		public virtual extern object color
		{
			[TypeLibFunc(20)]
			[DispId(-2147413110)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413110)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17000365 RID: 869
		// (get) Token: 0x060006E4 RID: 1764
		// (set) Token: 0x060006E3 RID: 1763
		[DispId(-2147413080)]
		public virtual extern string background
		{
			[TypeLibFunc(1044)]
			[DispId(-2147413080)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147413080)]
			[TypeLibFunc(1044)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17000366 RID: 870
		// (get) Token: 0x060006E6 RID: 1766
		// (set) Token: 0x060006E5 RID: 1765
		[DispId(-501)]
		public virtual extern object backgroundColor
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

		// Token: 0x17000367 RID: 871
		// (get) Token: 0x060006E8 RID: 1768
		// (set) Token: 0x060006E7 RID: 1767
		[DispId(-2147413111)]
		public virtual extern string backgroundImage
		{
			[DispId(-2147413111)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147413111)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17000368 RID: 872
		// (get) Token: 0x060006EA RID: 1770
		// (set) Token: 0x060006E9 RID: 1769
		[DispId(-2147413068)]
		public virtual extern string backgroundRepeat
		{
			[TypeLibFunc(20)]
			[DispId(-2147413068)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413068)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17000369 RID: 873
		// (get) Token: 0x060006EC RID: 1772
		// (set) Token: 0x060006EB RID: 1771
		[DispId(-2147413067)]
		public virtual extern string backgroundAttachment
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

		// Token: 0x1700036A RID: 874
		// (get) Token: 0x060006EE RID: 1774
		// (set) Token: 0x060006ED RID: 1773
		[DispId(-2147413066)]
		public virtual extern string backgroundPosition
		{
			[DispId(-2147413066)]
			[TypeLibFunc(1044)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147413066)]
			[TypeLibFunc(1044)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x1700036B RID: 875
		// (get) Token: 0x060006F0 RID: 1776
		// (set) Token: 0x060006EF RID: 1775
		[DispId(-2147413079)]
		public virtual extern object backgroundPositionX
		{
			[TypeLibFunc(20)]
			[DispId(-2147413079)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147413079)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x1700036C RID: 876
		// (get) Token: 0x060006F2 RID: 1778
		// (set) Token: 0x060006F1 RID: 1777
		[DispId(-2147413078)]
		public virtual extern object backgroundPositionY
		{
			[DispId(-2147413078)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413078)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x1700036D RID: 877
		// (get) Token: 0x060006F4 RID: 1780
		// (set) Token: 0x060006F3 RID: 1779
		[DispId(-2147413065)]
		public virtual extern object wordSpacing
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

		// Token: 0x1700036E RID: 878
		// (get) Token: 0x060006F6 RID: 1782
		// (set) Token: 0x060006F5 RID: 1781
		[DispId(-2147413104)]
		public virtual extern object letterSpacing
		{
			[TypeLibFunc(20)]
			[DispId(-2147413104)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413104)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x1700036F RID: 879
		// (get) Token: 0x060006F8 RID: 1784
		// (set) Token: 0x060006F7 RID: 1783
		[DispId(-2147413077)]
		public virtual extern string textDecoration
		{
			[TypeLibFunc(20)]
			[DispId(-2147413077)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147413077)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17000370 RID: 880
		// (get) Token: 0x060006FA RID: 1786
		// (set) Token: 0x060006F9 RID: 1785
		[DispId(-2147413089)]
		public virtual extern bool textDecorationNone
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

		// Token: 0x17000371 RID: 881
		// (get) Token: 0x060006FC RID: 1788
		// (set) Token: 0x060006FB RID: 1787
		[DispId(-2147413091)]
		public virtual extern bool textDecorationUnderline
		{
			[TypeLibFunc(20)]
			[DispId(-2147413091)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413091)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x17000372 RID: 882
		// (get) Token: 0x060006FE RID: 1790
		// (set) Token: 0x060006FD RID: 1789
		[DispId(-2147413043)]
		public virtual extern bool textDecorationOverline
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

		// Token: 0x17000373 RID: 883
		// (get) Token: 0x06000700 RID: 1792
		// (set) Token: 0x060006FF RID: 1791
		[DispId(-2147413092)]
		public virtual extern bool textDecorationLineThrough
		{
			[DispId(-2147413092)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[DispId(-2147413092)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x17000374 RID: 884
		// (get) Token: 0x06000702 RID: 1794
		// (set) Token: 0x06000701 RID: 1793
		[DispId(-2147413090)]
		public virtual extern bool textDecorationBlink
		{
			[DispId(-2147413090)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413090)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x17000375 RID: 885
		// (get) Token: 0x06000704 RID: 1796
		// (set) Token: 0x06000703 RID: 1795
		[DispId(-2147413064)]
		public virtual extern object verticalAlign
		{
			[DispId(-2147413064)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147413064)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17000376 RID: 886
		// (get) Token: 0x06000706 RID: 1798
		// (set) Token: 0x06000705 RID: 1797
		[DispId(-2147413108)]
		public virtual extern string textTransform
		{
			[DispId(-2147413108)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413108)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17000377 RID: 887
		// (get) Token: 0x06000708 RID: 1800
		// (set) Token: 0x06000707 RID: 1799
		[DispId(-2147418040)]
		public virtual extern string textAlign
		{
			[TypeLibFunc(20)]
			[DispId(-2147418040)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147418040)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17000378 RID: 888
		// (get) Token: 0x0600070A RID: 1802
		// (set) Token: 0x06000709 RID: 1801
		[DispId(-2147413105)]
		public virtual extern object textIndent
		{
			[TypeLibFunc(20)]
			[DispId(-2147413105)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147413105)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17000379 RID: 889
		// (get) Token: 0x0600070C RID: 1804
		// (set) Token: 0x0600070B RID: 1803
		[DispId(-2147413106)]
		public virtual extern object lineHeight
		{
			[TypeLibFunc(20)]
			[DispId(-2147413106)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147413106)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x1700037A RID: 890
		// (get) Token: 0x0600070E RID: 1806
		// (set) Token: 0x0600070D RID: 1805
		[DispId(-2147413075)]
		public virtual extern object marginTop
		{
			[TypeLibFunc(20)]
			[DispId(-2147413075)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147413075)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x1700037B RID: 891
		// (get) Token: 0x06000710 RID: 1808
		// (set) Token: 0x0600070F RID: 1807
		[DispId(-2147413074)]
		public virtual extern object marginRight
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

		// Token: 0x1700037C RID: 892
		// (get) Token: 0x06000712 RID: 1810
		// (set) Token: 0x06000711 RID: 1809
		[DispId(-2147413073)]
		public virtual extern object marginBottom
		{
			[DispId(-2147413073)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147413073)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x1700037D RID: 893
		// (get) Token: 0x06000714 RID: 1812
		// (set) Token: 0x06000713 RID: 1811
		[DispId(-2147413072)]
		public virtual extern object marginLeft
		{
			[DispId(-2147413072)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147413072)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x1700037E RID: 894
		// (get) Token: 0x06000716 RID: 1814
		// (set) Token: 0x06000715 RID: 1813
		[DispId(-2147413076)]
		public virtual extern string margin
		{
			[TypeLibFunc(1044)]
			[DispId(-2147413076)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147413076)]
			[TypeLibFunc(1044)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x1700037F RID: 895
		// (get) Token: 0x06000718 RID: 1816
		// (set) Token: 0x06000717 RID: 1815
		[DispId(-2147413100)]
		public virtual extern object paddingTop
		{
			[DispId(-2147413100)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147413100)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17000380 RID: 896
		// (get) Token: 0x0600071A RID: 1818
		// (set) Token: 0x06000719 RID: 1817
		[DispId(-2147413099)]
		public virtual extern object paddingRight
		{
			[DispId(-2147413099)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147413099)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17000381 RID: 897
		// (get) Token: 0x0600071C RID: 1820
		// (set) Token: 0x0600071B RID: 1819
		[DispId(-2147413098)]
		public virtual extern object paddingBottom
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

		// Token: 0x17000382 RID: 898
		// (get) Token: 0x0600071E RID: 1822
		// (set) Token: 0x0600071D RID: 1821
		[DispId(-2147413097)]
		public virtual extern object paddingLeft
		{
			[TypeLibFunc(20)]
			[DispId(-2147413097)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147413097)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17000383 RID: 899
		// (get) Token: 0x06000720 RID: 1824
		// (set) Token: 0x0600071F RID: 1823
		[DispId(-2147413101)]
		public virtual extern string padding
		{
			[DispId(-2147413101)]
			[TypeLibFunc(1044)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147413101)]
			[TypeLibFunc(1044)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17000384 RID: 900
		// (get) Token: 0x06000722 RID: 1826
		// (set) Token: 0x06000721 RID: 1825
		[DispId(-2147413063)]
		public virtual extern string border
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

		// Token: 0x17000385 RID: 901
		// (get) Token: 0x06000724 RID: 1828
		// (set) Token: 0x06000723 RID: 1827
		[DispId(-2147413062)]
		public virtual extern string borderTop
		{
			[DispId(-2147413062)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413062)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17000386 RID: 902
		// (get) Token: 0x06000726 RID: 1830
		// (set) Token: 0x06000725 RID: 1829
		[DispId(-2147413061)]
		public virtual extern string borderRight
		{
			[DispId(-2147413061)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413061)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17000387 RID: 903
		// (get) Token: 0x06000728 RID: 1832
		// (set) Token: 0x06000727 RID: 1831
		[DispId(-2147413060)]
		public virtual extern string borderBottom
		{
			[DispId(-2147413060)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147413060)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17000388 RID: 904
		// (get) Token: 0x0600072A RID: 1834
		// (set) Token: 0x06000729 RID: 1833
		[DispId(-2147413059)]
		public virtual extern string borderLeft
		{
			[DispId(-2147413059)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413059)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17000389 RID: 905
		// (get) Token: 0x0600072C RID: 1836
		// (set) Token: 0x0600072B RID: 1835
		[DispId(-2147413058)]
		public virtual extern string borderColor
		{
			[DispId(-2147413058)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147413058)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x1700038A RID: 906
		// (get) Token: 0x0600072E RID: 1838
		// (set) Token: 0x0600072D RID: 1837
		[DispId(-2147413057)]
		public virtual extern object borderTopColor
		{
			[TypeLibFunc(20)]
			[DispId(-2147413057)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413057)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x1700038B RID: 907
		// (get) Token: 0x06000730 RID: 1840
		// (set) Token: 0x0600072F RID: 1839
		[DispId(-2147413056)]
		public virtual extern object borderRightColor
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

		// Token: 0x1700038C RID: 908
		// (get) Token: 0x06000732 RID: 1842
		// (set) Token: 0x06000731 RID: 1841
		[DispId(-2147413055)]
		public virtual extern object borderBottomColor
		{
			[DispId(-2147413055)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147413055)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x1700038D RID: 909
		// (get) Token: 0x06000734 RID: 1844
		// (set) Token: 0x06000733 RID: 1843
		[DispId(-2147413054)]
		public virtual extern object borderLeftColor
		{
			[DispId(-2147413054)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147413054)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x1700038E RID: 910
		// (get) Token: 0x06000736 RID: 1846
		// (set) Token: 0x06000735 RID: 1845
		[DispId(-2147413053)]
		public virtual extern string borderWidth
		{
			[TypeLibFunc(20)]
			[DispId(-2147413053)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413053)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x1700038F RID: 911
		// (get) Token: 0x06000738 RID: 1848
		// (set) Token: 0x06000737 RID: 1847
		[DispId(-2147413052)]
		public virtual extern object borderTopWidth
		{
			[TypeLibFunc(20)]
			[DispId(-2147413052)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413052)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17000390 RID: 912
		// (get) Token: 0x0600073A RID: 1850
		// (set) Token: 0x06000739 RID: 1849
		[DispId(-2147413051)]
		public virtual extern object borderRightWidth
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

		// Token: 0x17000391 RID: 913
		// (get) Token: 0x0600073C RID: 1852
		// (set) Token: 0x0600073B RID: 1851
		[DispId(-2147413050)]
		public virtual extern object borderBottomWidth
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

		// Token: 0x17000392 RID: 914
		// (get) Token: 0x0600073E RID: 1854
		// (set) Token: 0x0600073D RID: 1853
		[DispId(-2147413049)]
		public virtual extern object borderLeftWidth
		{
			[DispId(-2147413049)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147413049)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17000393 RID: 915
		// (get) Token: 0x06000740 RID: 1856
		// (set) Token: 0x0600073F RID: 1855
		[DispId(-2147413048)]
		public virtual extern string borderStyle
		{
			[TypeLibFunc(20)]
			[DispId(-2147413048)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147413048)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17000394 RID: 916
		// (get) Token: 0x06000742 RID: 1858
		// (set) Token: 0x06000741 RID: 1857
		[DispId(-2147413047)]
		public virtual extern string borderTopStyle
		{
			[TypeLibFunc(20)]
			[DispId(-2147413047)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413047)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17000395 RID: 917
		// (get) Token: 0x06000744 RID: 1860
		// (set) Token: 0x06000743 RID: 1859
		[DispId(-2147413046)]
		public virtual extern string borderRightStyle
		{
			[TypeLibFunc(20)]
			[DispId(-2147413046)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413046)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17000396 RID: 918
		// (get) Token: 0x06000746 RID: 1862
		// (set) Token: 0x06000745 RID: 1861
		[DispId(-2147413045)]
		public virtual extern string borderBottomStyle
		{
			[DispId(-2147413045)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147413045)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17000397 RID: 919
		// (get) Token: 0x06000748 RID: 1864
		// (set) Token: 0x06000747 RID: 1863
		[DispId(-2147413044)]
		public virtual extern string borderLeftStyle
		{
			[DispId(-2147413044)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413044)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x17000398 RID: 920
		// (get) Token: 0x0600074A RID: 1866
		// (set) Token: 0x06000749 RID: 1865
		[DispId(-2147418107)]
		public virtual extern object width
		{
			[DispId(-2147418107)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147418107)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x17000399 RID: 921
		// (get) Token: 0x0600074C RID: 1868
		// (set) Token: 0x0600074B RID: 1867
		[DispId(-2147418106)]
		public virtual extern object height
		{
			[TypeLibFunc(20)]
			[DispId(-2147418106)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147418106)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x1700039A RID: 922
		// (get) Token: 0x0600074E RID: 1870
		// (set) Token: 0x0600074D RID: 1869
		[DispId(-2147413042)]
		public virtual extern string styleFloat
		{
			[TypeLibFunc(20)]
			[DispId(-2147413042)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147413042)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x1700039B RID: 923
		// (get) Token: 0x06000750 RID: 1872
		// (set) Token: 0x0600074F RID: 1871
		[DispId(-2147413096)]
		public virtual extern string clear
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

		// Token: 0x1700039C RID: 924
		// (get) Token: 0x06000752 RID: 1874
		// (set) Token: 0x06000751 RID: 1873
		[DispId(-2147413041)]
		public virtual extern string display
		{
			[TypeLibFunc(20)]
			[DispId(-2147413041)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147413041)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x1700039D RID: 925
		// (get) Token: 0x06000754 RID: 1876
		// (set) Token: 0x06000753 RID: 1875
		[DispId(-2147413032)]
		public virtual extern string visibility
		{
			[DispId(-2147413032)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147413032)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x1700039E RID: 926
		// (get) Token: 0x06000756 RID: 1878
		// (set) Token: 0x06000755 RID: 1877
		[DispId(-2147413040)]
		public virtual extern string listStyleType
		{
			[TypeLibFunc(20)]
			[DispId(-2147413040)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147413040)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x1700039F RID: 927
		// (get) Token: 0x06000758 RID: 1880
		// (set) Token: 0x06000757 RID: 1879
		[DispId(-2147413039)]
		public virtual extern string listStylePosition
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

		// Token: 0x170003A0 RID: 928
		// (get) Token: 0x0600075A RID: 1882
		// (set) Token: 0x06000759 RID: 1881
		[DispId(-2147413038)]
		public virtual extern string listStyleImage
		{
			[DispId(-2147413038)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147413038)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x170003A1 RID: 929
		// (get) Token: 0x0600075C RID: 1884
		// (set) Token: 0x0600075B RID: 1883
		[DispId(-2147413037)]
		public virtual extern string listStyle
		{
			[TypeLibFunc(1044)]
			[DispId(-2147413037)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(1044)]
			[DispId(-2147413037)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x170003A2 RID: 930
		// (get) Token: 0x0600075E RID: 1886
		// (set) Token: 0x0600075D RID: 1885
		[DispId(-2147413036)]
		public virtual extern string whiteSpace
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

		// Token: 0x170003A3 RID: 931
		// (get) Token: 0x06000760 RID: 1888
		// (set) Token: 0x0600075F RID: 1887
		[DispId(-2147418108)]
		public virtual extern object top
		{
			[TypeLibFunc(20)]
			[DispId(-2147418108)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147418108)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170003A4 RID: 932
		// (get) Token: 0x06000762 RID: 1890
		// (set) Token: 0x06000761 RID: 1889
		[DispId(-2147418109)]
		public virtual extern object left
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

		// Token: 0x170003A5 RID: 933
		// (get) Token: 0x06000764 RID: 1892
		// (set) Token: 0x06000763 RID: 1891
		[DispId(-2147413021)]
		public virtual extern object zIndex
		{
			[DispId(-2147413021)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147413021)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170003A6 RID: 934
		// (get) Token: 0x06000766 RID: 1894
		// (set) Token: 0x06000765 RID: 1893
		[DispId(-2147413102)]
		public virtual extern string overflow
		{
			[DispId(-2147413102)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147413102)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x170003A7 RID: 935
		// (get) Token: 0x06000768 RID: 1896
		// (set) Token: 0x06000767 RID: 1895
		[DispId(-2147413035)]
		public virtual extern string pageBreakBefore
		{
			[TypeLibFunc(20)]
			[DispId(-2147413035)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147413035)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x170003A8 RID: 936
		// (get) Token: 0x0600076A RID: 1898
		// (set) Token: 0x06000769 RID: 1897
		[DispId(-2147413034)]
		public virtual extern string pageBreakAfter
		{
			[DispId(-2147413034)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147413034)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x170003A9 RID: 937
		// (get) Token: 0x0600076C RID: 1900
		// (set) Token: 0x0600076B RID: 1899
		[DispId(-2147413013)]
		public virtual extern string cssText
		{
			[TypeLibFunc(1044)]
			[DispId(-2147413013)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147413013)]
			[TypeLibFunc(1044)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x170003AA RID: 938
		// (get) Token: 0x0600076E RID: 1902
		// (set) Token: 0x0600076D RID: 1901
		[DispId(-2147413010)]
		public virtual extern string cursor
		{
			[TypeLibFunc(20)]
			[DispId(-2147413010)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413010)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x170003AB RID: 939
		// (get) Token: 0x06000770 RID: 1904
		// (set) Token: 0x0600076F RID: 1903
		[DispId(-2147413020)]
		public virtual extern string clip
		{
			[TypeLibFunc(20)]
			[DispId(-2147413020)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413020)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x170003AC RID: 940
		// (get) Token: 0x06000772 RID: 1906
		// (set) Token: 0x06000771 RID: 1905
		[DispId(-2147413030)]
		public virtual extern string filter
		{
			[DispId(-2147413030)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413030)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x06000773 RID: 1907
		[DispId(-2147417611)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern void setAttribute([MarshalAs(19)] [In] string strAttributeName, [MarshalAs(27)] [In] object AttributeValue, [In] int lFlags = 1);

		// Token: 0x06000774 RID: 1908
		[DispId(-2147417610)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		public virtual extern object getAttribute([MarshalAs(19)] [In] string strAttributeName, [In] int lFlags = 0);

		// Token: 0x06000775 RID: 1909
		[DispId(-2147417609)]
		[MethodImpl(4224, MethodCodeType = 3)]
		public virtual extern bool removeAttribute([MarshalAs(19)] [In] string strAttributeName, [In] int lFlags = 1);

		// Token: 0x170003AD RID: 941
		// (get) Token: 0x06000777 RID: 1911
		// (set) Token: 0x06000776 RID: 1910
		[DispId(-2147413014)]
		public virtual extern string tableLayout
		{
			[TypeLibFunc(20)]
			[DispId(-2147413014)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413014)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x170003AE RID: 942
		// (get) Token: 0x06000779 RID: 1913
		// (set) Token: 0x06000778 RID: 1912
		[DispId(-2147413028)]
		public virtual extern string borderCollapse
		{
			[DispId(-2147413028)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147413028)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x170003AF RID: 943
		// (get) Token: 0x0600077B RID: 1915
		// (set) Token: 0x0600077A RID: 1914
		[DispId(-2147412993)]
		public virtual extern string direction
		{
			[DispId(-2147412993)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147412993)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x170003B0 RID: 944
		// (get) Token: 0x0600077D RID: 1917
		// (set) Token: 0x0600077C RID: 1916
		[DispId(-2147412997)]
		public virtual extern string behavior
		{
			[DispId(-2147412997)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147412997)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x170003B1 RID: 945
		// (get) Token: 0x0600077F RID: 1919
		// (set) Token: 0x0600077E RID: 1918
		[DispId(-2147413022)]
		public virtual extern string position
		{
			[DispId(-2147413022)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147413022)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x170003B2 RID: 946
		// (get) Token: 0x06000781 RID: 1921
		// (set) Token: 0x06000780 RID: 1920
		[DispId(-2147412994)]
		public virtual extern string unicodeBidi
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

		// Token: 0x170003B3 RID: 947
		// (get) Token: 0x06000783 RID: 1923
		// (set) Token: 0x06000782 RID: 1922
		[DispId(-2147418034)]
		public virtual extern object bottom
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

		// Token: 0x170003B4 RID: 948
		// (get) Token: 0x06000785 RID: 1925
		// (set) Token: 0x06000784 RID: 1924
		[DispId(-2147418035)]
		public virtual extern object right
		{
			[DispId(-2147418035)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147418035)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170003B5 RID: 949
		// (get) Token: 0x06000787 RID: 1927
		// (set) Token: 0x06000786 RID: 1926
		[DispId(-2147414103)]
		public virtual extern int pixelBottom
		{
			[TypeLibFunc(84)]
			[DispId(-2147414103)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
			[TypeLibFunc(84)]
			[DispId(-2147414103)]
			[MethodImpl(4224, MethodCodeType = 3)]
			set;
		}

		// Token: 0x170003B6 RID: 950
		// (get) Token: 0x06000789 RID: 1929
		// (set) Token: 0x06000788 RID: 1928
		[DispId(-2147414102)]
		public virtual extern int pixelRight
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

		// Token: 0x170003B7 RID: 951
		// (get) Token: 0x0600078B RID: 1931
		// (set) Token: 0x0600078A RID: 1930
		[DispId(-2147414101)]
		public virtual extern float posBottom
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

		// Token: 0x170003B8 RID: 952
		// (get) Token: 0x0600078D RID: 1933
		// (set) Token: 0x0600078C RID: 1932
		[DispId(-2147414100)]
		public virtual extern float posRight
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

		// Token: 0x170003B9 RID: 953
		// (get) Token: 0x0600078F RID: 1935
		// (set) Token: 0x0600078E RID: 1934
		[DispId(-2147412992)]
		public virtual extern string imeMode
		{
			[TypeLibFunc(20)]
			[DispId(-2147412992)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412992)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x170003BA RID: 954
		// (get) Token: 0x06000791 RID: 1937
		// (set) Token: 0x06000790 RID: 1936
		[DispId(-2147412991)]
		public virtual extern string rubyAlign
		{
			[TypeLibFunc(20)]
			[DispId(-2147412991)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412991)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x170003BB RID: 955
		// (get) Token: 0x06000793 RID: 1939
		// (set) Token: 0x06000792 RID: 1938
		[DispId(-2147412990)]
		public virtual extern string rubyPosition
		{
			[TypeLibFunc(20)]
			[DispId(-2147412990)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147412990)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x170003BC RID: 956
		// (get) Token: 0x06000795 RID: 1941
		// (set) Token: 0x06000794 RID: 1940
		[DispId(-2147412989)]
		public virtual extern string rubyOverhang
		{
			[DispId(-2147412989)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412989)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x170003BD RID: 957
		// (get) Token: 0x06000797 RID: 1943
		// (set) Token: 0x06000796 RID: 1942
		[DispId(-2147412985)]
		public virtual extern object layoutGridChar
		{
			[DispId(-2147412985)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412985)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170003BE RID: 958
		// (get) Token: 0x06000799 RID: 1945
		// (set) Token: 0x06000798 RID: 1944
		[DispId(-2147412984)]
		public virtual extern object layoutGridLine
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

		// Token: 0x170003BF RID: 959
		// (get) Token: 0x0600079B RID: 1947
		// (set) Token: 0x0600079A RID: 1946
		[DispId(-2147412983)]
		public virtual extern string layoutGridMode
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

		// Token: 0x170003C0 RID: 960
		// (get) Token: 0x0600079D RID: 1949
		// (set) Token: 0x0600079C RID: 1948
		[DispId(-2147412982)]
		public virtual extern string layoutGridType
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

		// Token: 0x170003C1 RID: 961
		// (get) Token: 0x0600079F RID: 1951
		// (set) Token: 0x0600079E RID: 1950
		[DispId(-2147412981)]
		public virtual extern string layoutGrid
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

		// Token: 0x170003C2 RID: 962
		// (get) Token: 0x060007A1 RID: 1953
		// (set) Token: 0x060007A0 RID: 1952
		[DispId(-2147412980)]
		public virtual extern string textAutospace
		{
			[DispId(-2147412980)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147412980)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x170003C3 RID: 963
		// (get) Token: 0x060007A3 RID: 1955
		// (set) Token: 0x060007A2 RID: 1954
		[DispId(-2147412978)]
		public virtual extern string wordBreak
		{
			[TypeLibFunc(20)]
			[DispId(-2147412978)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147412978)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x170003C4 RID: 964
		// (get) Token: 0x060007A5 RID: 1957
		// (set) Token: 0x060007A4 RID: 1956
		[DispId(-2147412979)]
		public virtual extern string lineBreak
		{
			[TypeLibFunc(20)]
			[DispId(-2147412979)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412979)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x170003C5 RID: 965
		// (get) Token: 0x060007A7 RID: 1959
		// (set) Token: 0x060007A6 RID: 1958
		[DispId(-2147412977)]
		public virtual extern string textJustify
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

		// Token: 0x170003C6 RID: 966
		// (get) Token: 0x060007A9 RID: 1961
		// (set) Token: 0x060007A8 RID: 1960
		[DispId(-2147412976)]
		public virtual extern string textJustifyTrim
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

		// Token: 0x170003C7 RID: 967
		// (get) Token: 0x060007AB RID: 1963
		// (set) Token: 0x060007AA RID: 1962
		[DispId(-2147412975)]
		public virtual extern object textKashida
		{
			[TypeLibFunc(20)]
			[DispId(-2147412975)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412975)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170003C8 RID: 968
		// (get) Token: 0x060007AD RID: 1965
		// (set) Token: 0x060007AC RID: 1964
		[DispId(-2147412973)]
		public virtual extern string overflowX
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

		// Token: 0x170003C9 RID: 969
		// (get) Token: 0x060007AF RID: 1967
		// (set) Token: 0x060007AE RID: 1966
		[DispId(-2147412972)]
		public virtual extern string overflowY
		{
			[DispId(-2147412972)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147412972)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x170003CA RID: 970
		// (get) Token: 0x060007B1 RID: 1969
		// (set) Token: 0x060007B0 RID: 1968
		[DispId(-2147412965)]
		public virtual extern string accelerator
		{
			[TypeLibFunc(20)]
			[DispId(-2147412965)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412965)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x170003CB RID: 971
		// (get) Token: 0x060007B3 RID: 1971
		// (set) Token: 0x060007B2 RID: 1970
		[DispId(-2147412957)]
		public virtual extern string layoutFlow
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

		// Token: 0x170003CC RID: 972
		// (get) Token: 0x060007B5 RID: 1973
		// (set) Token: 0x060007B4 RID: 1972
		[DispId(-2147412959)]
		public virtual extern object zoom
		{
			[TypeLibFunc(20)]
			[DispId(-2147412959)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412959)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170003CD RID: 973
		// (get) Token: 0x060007B7 RID: 1975
		// (set) Token: 0x060007B6 RID: 1974
		[DispId(-2147412954)]
		public virtual extern string wordWrap
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

		// Token: 0x170003CE RID: 974
		// (get) Token: 0x060007B9 RID: 1977
		// (set) Token: 0x060007B8 RID: 1976
		[DispId(-2147412953)]
		public virtual extern string textUnderlinePosition
		{
			[TypeLibFunc(20)]
			[DispId(-2147412953)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147412953)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x170003CF RID: 975
		// (get) Token: 0x060007BB RID: 1979
		// (set) Token: 0x060007BA RID: 1978
		[DispId(-2147412932)]
		public virtual extern object scrollbarBaseColor
		{
			[DispId(-2147412932)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412932)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170003D0 RID: 976
		// (get) Token: 0x060007BD RID: 1981
		// (set) Token: 0x060007BC RID: 1980
		[DispId(-2147412931)]
		public virtual extern object scrollbarFaceColor
		{
			[DispId(-2147412931)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412931)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170003D1 RID: 977
		// (get) Token: 0x060007BF RID: 1983
		// (set) Token: 0x060007BE RID: 1982
		[DispId(-2147412930)]
		public virtual extern object scrollbar3dLightColor
		{
			[TypeLibFunc(20)]
			[DispId(-2147412930)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412930)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170003D2 RID: 978
		// (get) Token: 0x060007C1 RID: 1985
		// (set) Token: 0x060007C0 RID: 1984
		[DispId(-2147412929)]
		public virtual extern object scrollbarShadowColor
		{
			[DispId(-2147412929)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412929)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170003D3 RID: 979
		// (get) Token: 0x060007C3 RID: 1987
		// (set) Token: 0x060007C2 RID: 1986
		[DispId(-2147412928)]
		public virtual extern object scrollbarHighlightColor
		{
			[DispId(-2147412928)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412928)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170003D4 RID: 980
		// (get) Token: 0x060007C5 RID: 1989
		// (set) Token: 0x060007C4 RID: 1988
		[DispId(-2147412927)]
		public virtual extern object scrollbarDarkShadowColor
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

		// Token: 0x170003D5 RID: 981
		// (get) Token: 0x060007C7 RID: 1991
		// (set) Token: 0x060007C6 RID: 1990
		[DispId(-2147412926)]
		public virtual extern object scrollbarArrowColor
		{
			[DispId(-2147412926)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412926)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170003D6 RID: 982
		// (get) Token: 0x060007C9 RID: 1993
		// (set) Token: 0x060007C8 RID: 1992
		[DispId(-2147412916)]
		public virtual extern object scrollbarTrackColor
		{
			[DispId(-2147412916)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412916)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170003D7 RID: 983
		// (get) Token: 0x060007CB RID: 1995
		// (set) Token: 0x060007CA RID: 1994
		[DispId(-2147412920)]
		public virtual extern string writingMode
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

		// Token: 0x170003D8 RID: 984
		// (get) Token: 0x060007CD RID: 1997
		// (set) Token: 0x060007CC RID: 1996
		[DispId(-2147412909)]
		public virtual extern string textAlignLast
		{
			[DispId(-2147412909)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(-2147412909)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x170003D9 RID: 985
		// (get) Token: 0x060007CF RID: 1999
		// (set) Token: 0x060007CE RID: 1998
		[DispId(-2147412908)]
		public virtual extern object textKashidaSpace
		{
			[TypeLibFunc(20)]
			[DispId(-2147412908)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412908)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170003DA RID: 986
		// (get) Token: 0x060007D1 RID: 2001
		// (set) Token: 0x060007D0 RID: 2000
		[DispId(-2147412903)]
		public virtual extern string textOverflow
		{
			[DispId(-2147412903)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412903)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x170003DB RID: 987
		// (get) Token: 0x060007D3 RID: 2003
		// (set) Token: 0x060007D2 RID: 2002
		[DispId(-2147412901)]
		public virtual extern object minHeight
		{
			[TypeLibFunc(20)]
			[DispId(-2147412901)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412901)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170003DC RID: 988
		// (get) Token: 0x060007D5 RID: 2005
		// (set) Token: 0x060007D4 RID: 2004
		public virtual extern string IHTMLRuleStyle_fontFamily
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

		// Token: 0x170003DD RID: 989
		// (get) Token: 0x060007D7 RID: 2007
		// (set) Token: 0x060007D6 RID: 2006
		public virtual extern string IHTMLRuleStyle_fontStyle
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

		// Token: 0x170003DE RID: 990
		// (get) Token: 0x060007D9 RID: 2009
		// (set) Token: 0x060007D8 RID: 2008
		public virtual extern string IHTMLRuleStyle_fontVariant
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

		// Token: 0x170003DF RID: 991
		// (get) Token: 0x060007DB RID: 2011
		// (set) Token: 0x060007DA RID: 2010
		public virtual extern string IHTMLRuleStyle_fontWeight
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

		// Token: 0x170003E0 RID: 992
		// (get) Token: 0x060007DD RID: 2013
		// (set) Token: 0x060007DC RID: 2012
		public virtual extern object IHTMLRuleStyle_fontSize
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

		// Token: 0x170003E1 RID: 993
		// (get) Token: 0x060007DF RID: 2015
		// (set) Token: 0x060007DE RID: 2014
		public virtual extern string IHTMLRuleStyle_font
		{
			[TypeLibFunc(1044)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(1044)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x170003E2 RID: 994
		// (get) Token: 0x060007E1 RID: 2017
		// (set) Token: 0x060007E0 RID: 2016
		public virtual extern object IHTMLRuleStyle_color
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

		// Token: 0x170003E3 RID: 995
		// (get) Token: 0x060007E3 RID: 2019
		// (set) Token: 0x060007E2 RID: 2018
		public virtual extern string IHTMLRuleStyle_background
		{
			[TypeLibFunc(1044)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(1044)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x170003E4 RID: 996
		// (get) Token: 0x060007E5 RID: 2021
		// (set) Token: 0x060007E4 RID: 2020
		public virtual extern object IHTMLRuleStyle_backgroundColor
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

		// Token: 0x170003E5 RID: 997
		// (get) Token: 0x060007E7 RID: 2023
		// (set) Token: 0x060007E6 RID: 2022
		public virtual extern string IHTMLRuleStyle_backgroundImage
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

		// Token: 0x170003E6 RID: 998
		// (get) Token: 0x060007E9 RID: 2025
		// (set) Token: 0x060007E8 RID: 2024
		public virtual extern string IHTMLRuleStyle_backgroundRepeat
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

		// Token: 0x170003E7 RID: 999
		// (get) Token: 0x060007EB RID: 2027
		// (set) Token: 0x060007EA RID: 2026
		public virtual extern string IHTMLRuleStyle_backgroundAttachment
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

		// Token: 0x170003E8 RID: 1000
		// (get) Token: 0x060007ED RID: 2029
		// (set) Token: 0x060007EC RID: 2028
		public virtual extern string IHTMLRuleStyle_backgroundPosition
		{
			[TypeLibFunc(1044)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(1044)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x170003E9 RID: 1001
		// (get) Token: 0x060007EF RID: 2031
		// (set) Token: 0x060007EE RID: 2030
		public virtual extern object IHTMLRuleStyle_backgroundPositionX
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

		// Token: 0x170003EA RID: 1002
		// (get) Token: 0x060007F1 RID: 2033
		// (set) Token: 0x060007F0 RID: 2032
		public virtual extern object IHTMLRuleStyle_backgroundPositionY
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

		// Token: 0x170003EB RID: 1003
		// (get) Token: 0x060007F3 RID: 2035
		// (set) Token: 0x060007F2 RID: 2034
		public virtual extern object IHTMLRuleStyle_wordSpacing
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

		// Token: 0x170003EC RID: 1004
		// (get) Token: 0x060007F5 RID: 2037
		// (set) Token: 0x060007F4 RID: 2036
		public virtual extern object IHTMLRuleStyle_letterSpacing
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

		// Token: 0x170003ED RID: 1005
		// (get) Token: 0x060007F7 RID: 2039
		// (set) Token: 0x060007F6 RID: 2038
		public virtual extern string IHTMLRuleStyle_textDecoration
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

		// Token: 0x170003EE RID: 1006
		// (get) Token: 0x060007F9 RID: 2041
		// (set) Token: 0x060007F8 RID: 2040
		public virtual extern bool IHTMLRuleStyle_textDecorationNone
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x170003EF RID: 1007
		// (get) Token: 0x060007FB RID: 2043
		// (set) Token: 0x060007FA RID: 2042
		public virtual extern bool IHTMLRuleStyle_textDecorationUnderline
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x170003F0 RID: 1008
		// (get) Token: 0x060007FD RID: 2045
		// (set) Token: 0x060007FC RID: 2044
		public virtual extern bool IHTMLRuleStyle_textDecorationOverline
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x170003F1 RID: 1009
		// (get) Token: 0x060007FF RID: 2047
		// (set) Token: 0x060007FE RID: 2046
		public virtual extern bool IHTMLRuleStyle_textDecorationLineThrough
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x170003F2 RID: 1010
		// (get) Token: 0x06000801 RID: 2049
		// (set) Token: 0x06000800 RID: 2048
		public virtual extern bool IHTMLRuleStyle_textDecorationBlink
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x170003F3 RID: 1011
		// (get) Token: 0x06000803 RID: 2051
		// (set) Token: 0x06000802 RID: 2050
		public virtual extern object IHTMLRuleStyle_verticalAlign
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

		// Token: 0x170003F4 RID: 1012
		// (get) Token: 0x06000805 RID: 2053
		// (set) Token: 0x06000804 RID: 2052
		public virtual extern string IHTMLRuleStyle_textTransform
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

		// Token: 0x170003F5 RID: 1013
		// (get) Token: 0x06000807 RID: 2055
		// (set) Token: 0x06000806 RID: 2054
		public virtual extern string IHTMLRuleStyle_textAlign
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

		// Token: 0x170003F6 RID: 1014
		// (get) Token: 0x06000809 RID: 2057
		// (set) Token: 0x06000808 RID: 2056
		public virtual extern object IHTMLRuleStyle_textIndent
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

		// Token: 0x170003F7 RID: 1015
		// (get) Token: 0x0600080B RID: 2059
		// (set) Token: 0x0600080A RID: 2058
		public virtual extern object IHTMLRuleStyle_lineHeight
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

		// Token: 0x170003F8 RID: 1016
		// (get) Token: 0x0600080D RID: 2061
		// (set) Token: 0x0600080C RID: 2060
		public virtual extern object IHTMLRuleStyle_marginTop
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

		// Token: 0x170003F9 RID: 1017
		// (get) Token: 0x0600080F RID: 2063
		// (set) Token: 0x0600080E RID: 2062
		public virtual extern object IHTMLRuleStyle_marginRight
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

		// Token: 0x170003FA RID: 1018
		// (get) Token: 0x06000811 RID: 2065
		// (set) Token: 0x06000810 RID: 2064
		public virtual extern object IHTMLRuleStyle_marginBottom
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

		// Token: 0x170003FB RID: 1019
		// (get) Token: 0x06000813 RID: 2067
		// (set) Token: 0x06000812 RID: 2066
		public virtual extern object IHTMLRuleStyle_marginLeft
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

		// Token: 0x170003FC RID: 1020
		// (get) Token: 0x06000815 RID: 2069
		// (set) Token: 0x06000814 RID: 2068
		public virtual extern string IHTMLRuleStyle_margin
		{
			[TypeLibFunc(1044)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(1044)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x170003FD RID: 1021
		// (get) Token: 0x06000817 RID: 2071
		// (set) Token: 0x06000816 RID: 2070
		public virtual extern object IHTMLRuleStyle_paddingTop
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

		// Token: 0x170003FE RID: 1022
		// (get) Token: 0x06000819 RID: 2073
		// (set) Token: 0x06000818 RID: 2072
		public virtual extern object IHTMLRuleStyle_paddingRight
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

		// Token: 0x170003FF RID: 1023
		// (get) Token: 0x0600081B RID: 2075
		// (set) Token: 0x0600081A RID: 2074
		public virtual extern object IHTMLRuleStyle_paddingBottom
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

		// Token: 0x17000400 RID: 1024
		// (get) Token: 0x0600081D RID: 2077
		// (set) Token: 0x0600081C RID: 2076
		public virtual extern object IHTMLRuleStyle_paddingLeft
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

		// Token: 0x17000401 RID: 1025
		// (get) Token: 0x0600081F RID: 2079
		// (set) Token: 0x0600081E RID: 2078
		public virtual extern string IHTMLRuleStyle_padding
		{
			[TypeLibFunc(1044)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(1044)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17000402 RID: 1026
		// (get) Token: 0x06000821 RID: 2081
		// (set) Token: 0x06000820 RID: 2080
		public virtual extern string IHTMLRuleStyle_border
		{
			[TypeLibFunc(1044)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(1044)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17000403 RID: 1027
		// (get) Token: 0x06000823 RID: 2083
		// (set) Token: 0x06000822 RID: 2082
		public virtual extern string IHTMLRuleStyle_borderTop
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

		// Token: 0x17000404 RID: 1028
		// (get) Token: 0x06000825 RID: 2085
		// (set) Token: 0x06000824 RID: 2084
		public virtual extern string IHTMLRuleStyle_borderRight
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

		// Token: 0x17000405 RID: 1029
		// (get) Token: 0x06000827 RID: 2087
		// (set) Token: 0x06000826 RID: 2086
		public virtual extern string IHTMLRuleStyle_borderBottom
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

		// Token: 0x17000406 RID: 1030
		// (get) Token: 0x06000829 RID: 2089
		// (set) Token: 0x06000828 RID: 2088
		public virtual extern string IHTMLRuleStyle_borderLeft
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

		// Token: 0x17000407 RID: 1031
		// (get) Token: 0x0600082B RID: 2091
		// (set) Token: 0x0600082A RID: 2090
		public virtual extern string IHTMLRuleStyle_borderColor
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

		// Token: 0x17000408 RID: 1032
		// (get) Token: 0x0600082D RID: 2093
		// (set) Token: 0x0600082C RID: 2092
		public virtual extern object IHTMLRuleStyle_borderTopColor
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

		// Token: 0x17000409 RID: 1033
		// (get) Token: 0x0600082F RID: 2095
		// (set) Token: 0x0600082E RID: 2094
		public virtual extern object IHTMLRuleStyle_borderRightColor
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

		// Token: 0x1700040A RID: 1034
		// (get) Token: 0x06000831 RID: 2097
		// (set) Token: 0x06000830 RID: 2096
		public virtual extern object IHTMLRuleStyle_borderBottomColor
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

		// Token: 0x1700040B RID: 1035
		// (get) Token: 0x06000833 RID: 2099
		// (set) Token: 0x06000832 RID: 2098
		public virtual extern object IHTMLRuleStyle_borderLeftColor
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

		// Token: 0x1700040C RID: 1036
		// (get) Token: 0x06000835 RID: 2101
		// (set) Token: 0x06000834 RID: 2100
		public virtual extern string IHTMLRuleStyle_borderWidth
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

		// Token: 0x1700040D RID: 1037
		// (get) Token: 0x06000837 RID: 2103
		// (set) Token: 0x06000836 RID: 2102
		public virtual extern object IHTMLRuleStyle_borderTopWidth
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

		// Token: 0x1700040E RID: 1038
		// (get) Token: 0x06000839 RID: 2105
		// (set) Token: 0x06000838 RID: 2104
		public virtual extern object IHTMLRuleStyle_borderRightWidth
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

		// Token: 0x1700040F RID: 1039
		// (get) Token: 0x0600083B RID: 2107
		// (set) Token: 0x0600083A RID: 2106
		public virtual extern object IHTMLRuleStyle_borderBottomWidth
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

		// Token: 0x17000410 RID: 1040
		// (get) Token: 0x0600083D RID: 2109
		// (set) Token: 0x0600083C RID: 2108
		public virtual extern object IHTMLRuleStyle_borderLeftWidth
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

		// Token: 0x17000411 RID: 1041
		// (get) Token: 0x0600083F RID: 2111
		// (set) Token: 0x0600083E RID: 2110
		public virtual extern string IHTMLRuleStyle_borderStyle
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

		// Token: 0x17000412 RID: 1042
		// (get) Token: 0x06000841 RID: 2113
		// (set) Token: 0x06000840 RID: 2112
		public virtual extern string IHTMLRuleStyle_borderTopStyle
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

		// Token: 0x17000413 RID: 1043
		// (get) Token: 0x06000843 RID: 2115
		// (set) Token: 0x06000842 RID: 2114
		public virtual extern string IHTMLRuleStyle_borderRightStyle
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

		// Token: 0x17000414 RID: 1044
		// (get) Token: 0x06000845 RID: 2117
		// (set) Token: 0x06000844 RID: 2116
		public virtual extern string IHTMLRuleStyle_borderBottomStyle
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

		// Token: 0x17000415 RID: 1045
		// (get) Token: 0x06000847 RID: 2119
		// (set) Token: 0x06000846 RID: 2118
		public virtual extern string IHTMLRuleStyle_borderLeftStyle
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

		// Token: 0x17000416 RID: 1046
		// (get) Token: 0x06000849 RID: 2121
		// (set) Token: 0x06000848 RID: 2120
		public virtual extern object IHTMLRuleStyle_width
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

		// Token: 0x17000417 RID: 1047
		// (get) Token: 0x0600084B RID: 2123
		// (set) Token: 0x0600084A RID: 2122
		public virtual extern object IHTMLRuleStyle_height
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

		// Token: 0x17000418 RID: 1048
		// (get) Token: 0x0600084D RID: 2125
		// (set) Token: 0x0600084C RID: 2124
		public virtual extern string IHTMLRuleStyle_styleFloat
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

		// Token: 0x17000419 RID: 1049
		// (get) Token: 0x0600084F RID: 2127
		// (set) Token: 0x0600084E RID: 2126
		public virtual extern string IHTMLRuleStyle_clear
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

		// Token: 0x1700041A RID: 1050
		// (get) Token: 0x06000851 RID: 2129
		// (set) Token: 0x06000850 RID: 2128
		public virtual extern string IHTMLRuleStyle_display
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

		// Token: 0x1700041B RID: 1051
		// (get) Token: 0x06000853 RID: 2131
		// (set) Token: 0x06000852 RID: 2130
		public virtual extern string IHTMLRuleStyle_visibility
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

		// Token: 0x1700041C RID: 1052
		// (get) Token: 0x06000855 RID: 2133
		// (set) Token: 0x06000854 RID: 2132
		public virtual extern string IHTMLRuleStyle_listStyleType
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

		// Token: 0x1700041D RID: 1053
		// (get) Token: 0x06000857 RID: 2135
		// (set) Token: 0x06000856 RID: 2134
		public virtual extern string IHTMLRuleStyle_listStylePosition
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

		// Token: 0x1700041E RID: 1054
		// (get) Token: 0x06000859 RID: 2137
		// (set) Token: 0x06000858 RID: 2136
		public virtual extern string IHTMLRuleStyle_listStyleImage
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

		// Token: 0x1700041F RID: 1055
		// (get) Token: 0x0600085B RID: 2139
		// (set) Token: 0x0600085A RID: 2138
		public virtual extern string IHTMLRuleStyle_listStyle
		{
			[TypeLibFunc(1044)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(1044)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17000420 RID: 1056
		// (get) Token: 0x0600085D RID: 2141
		// (set) Token: 0x0600085C RID: 2140
		public virtual extern string IHTMLRuleStyle_whiteSpace
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

		// Token: 0x17000421 RID: 1057
		// (get) Token: 0x0600085F RID: 2143
		// (set) Token: 0x0600085E RID: 2142
		public virtual extern object IHTMLRuleStyle_top
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

		// Token: 0x17000422 RID: 1058
		// (get) Token: 0x06000861 RID: 2145
		// (set) Token: 0x06000860 RID: 2144
		public virtual extern object IHTMLRuleStyle_left
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

		// Token: 0x17000423 RID: 1059
		// (get) Token: 0x06000862 RID: 2146
		public virtual extern string IHTMLRuleStyle_position
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17000424 RID: 1060
		// (get) Token: 0x06000864 RID: 2148
		// (set) Token: 0x06000863 RID: 2147
		public virtual extern object IHTMLRuleStyle_zIndex
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

		// Token: 0x17000425 RID: 1061
		// (get) Token: 0x06000866 RID: 2150
		// (set) Token: 0x06000865 RID: 2149
		public virtual extern string IHTMLRuleStyle_overflow
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

		// Token: 0x17000426 RID: 1062
		// (get) Token: 0x06000868 RID: 2152
		// (set) Token: 0x06000867 RID: 2151
		public virtual extern string IHTMLRuleStyle_pageBreakBefore
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

		// Token: 0x17000427 RID: 1063
		// (get) Token: 0x0600086A RID: 2154
		// (set) Token: 0x06000869 RID: 2153
		public virtual extern string IHTMLRuleStyle_pageBreakAfter
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

		// Token: 0x17000428 RID: 1064
		// (get) Token: 0x0600086C RID: 2156
		// (set) Token: 0x0600086B RID: 2155
		public virtual extern string IHTMLRuleStyle_cssText
		{
			[TypeLibFunc(1044)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(1044)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17000429 RID: 1065
		// (get) Token: 0x0600086E RID: 2158
		// (set) Token: 0x0600086D RID: 2157
		public virtual extern string IHTMLRuleStyle_cursor
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

		// Token: 0x1700042A RID: 1066
		// (get) Token: 0x06000870 RID: 2160
		// (set) Token: 0x0600086F RID: 2159
		public virtual extern string IHTMLRuleStyle_clip
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

		// Token: 0x1700042B RID: 1067
		// (get) Token: 0x06000872 RID: 2162
		// (set) Token: 0x06000871 RID: 2161
		public virtual extern string IHTMLRuleStyle_filter
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

		// Token: 0x06000873 RID: 2163
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern void IHTMLRuleStyle_setAttribute([MarshalAs(19)] [In] string strAttributeName, [MarshalAs(27)] [In] object AttributeValue, [In] int lFlags = 1);

		// Token: 0x06000874 RID: 2164
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		public virtual extern object IHTMLRuleStyle_getAttribute([MarshalAs(19)] [In] string strAttributeName, [In] int lFlags = 0);

		// Token: 0x06000875 RID: 2165
		[MethodImpl(4096, MethodCodeType = 3)]
		public virtual extern bool IHTMLRuleStyle_removeAttribute([MarshalAs(19)] [In] string strAttributeName, [In] int lFlags = 1);

		// Token: 0x1700042C RID: 1068
		// (get) Token: 0x06000877 RID: 2167
		// (set) Token: 0x06000876 RID: 2166
		public virtual extern string IHTMLRuleStyle2_tableLayout
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

		// Token: 0x1700042D RID: 1069
		// (get) Token: 0x06000879 RID: 2169
		// (set) Token: 0x06000878 RID: 2168
		public virtual extern string IHTMLRuleStyle2_borderCollapse
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

		// Token: 0x1700042E RID: 1070
		// (get) Token: 0x0600087B RID: 2171
		// (set) Token: 0x0600087A RID: 2170
		public virtual extern string IHTMLRuleStyle2_direction
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

		// Token: 0x1700042F RID: 1071
		// (get) Token: 0x0600087D RID: 2173
		// (set) Token: 0x0600087C RID: 2172
		public virtual extern string IHTMLRuleStyle2_behavior
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

		// Token: 0x17000430 RID: 1072
		// (get) Token: 0x0600087F RID: 2175
		// (set) Token: 0x0600087E RID: 2174
		public virtual extern string IHTMLRuleStyle2_position
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

		// Token: 0x17000431 RID: 1073
		// (get) Token: 0x06000881 RID: 2177
		// (set) Token: 0x06000880 RID: 2176
		public virtual extern string IHTMLRuleStyle2_unicodeBidi
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

		// Token: 0x17000432 RID: 1074
		// (get) Token: 0x06000883 RID: 2179
		// (set) Token: 0x06000882 RID: 2178
		public virtual extern object IHTMLRuleStyle2_bottom
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

		// Token: 0x17000433 RID: 1075
		// (get) Token: 0x06000885 RID: 2181
		// (set) Token: 0x06000884 RID: 2180
		public virtual extern object IHTMLRuleStyle2_right
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

		// Token: 0x17000434 RID: 1076
		// (get) Token: 0x06000887 RID: 2183
		// (set) Token: 0x06000886 RID: 2182
		public virtual extern int IHTMLRuleStyle2_pixelBottom
		{
			[TypeLibFunc(84)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[TypeLibFunc(84)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x17000435 RID: 1077
		// (get) Token: 0x06000889 RID: 2185
		// (set) Token: 0x06000888 RID: 2184
		public virtual extern int IHTMLRuleStyle2_pixelRight
		{
			[TypeLibFunc(84)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[TypeLibFunc(84)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x17000436 RID: 1078
		// (get) Token: 0x0600088B RID: 2187
		// (set) Token: 0x0600088A RID: 2186
		public virtual extern float IHTMLRuleStyle2_posBottom
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x17000437 RID: 1079
		// (get) Token: 0x0600088D RID: 2189
		// (set) Token: 0x0600088C RID: 2188
		public virtual extern float IHTMLRuleStyle2_posRight
		{
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			get;
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: In]
			set;
		}

		// Token: 0x17000438 RID: 1080
		// (get) Token: 0x0600088F RID: 2191
		// (set) Token: 0x0600088E RID: 2190
		public virtual extern string IHTMLRuleStyle2_imeMode
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

		// Token: 0x17000439 RID: 1081
		// (get) Token: 0x06000891 RID: 2193
		// (set) Token: 0x06000890 RID: 2192
		public virtual extern string IHTMLRuleStyle2_rubyAlign
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

		// Token: 0x1700043A RID: 1082
		// (get) Token: 0x06000893 RID: 2195
		// (set) Token: 0x06000892 RID: 2194
		public virtual extern string IHTMLRuleStyle2_rubyPosition
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

		// Token: 0x1700043B RID: 1083
		// (get) Token: 0x06000895 RID: 2197
		// (set) Token: 0x06000894 RID: 2196
		public virtual extern string IHTMLRuleStyle2_rubyOverhang
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

		// Token: 0x1700043C RID: 1084
		// (get) Token: 0x06000897 RID: 2199
		// (set) Token: 0x06000896 RID: 2198
		public virtual extern object IHTMLRuleStyle2_layoutGridChar
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

		// Token: 0x1700043D RID: 1085
		// (get) Token: 0x06000899 RID: 2201
		// (set) Token: 0x06000898 RID: 2200
		public virtual extern object IHTMLRuleStyle2_layoutGridLine
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

		// Token: 0x1700043E RID: 1086
		// (get) Token: 0x0600089B RID: 2203
		// (set) Token: 0x0600089A RID: 2202
		public virtual extern string IHTMLRuleStyle2_layoutGridMode
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

		// Token: 0x1700043F RID: 1087
		// (get) Token: 0x0600089D RID: 2205
		// (set) Token: 0x0600089C RID: 2204
		public virtual extern string IHTMLRuleStyle2_layoutGridType
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

		// Token: 0x17000440 RID: 1088
		// (get) Token: 0x0600089F RID: 2207
		// (set) Token: 0x0600089E RID: 2206
		public virtual extern string IHTMLRuleStyle2_layoutGrid
		{
			[TypeLibFunc(1044)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[TypeLibFunc(1044)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			[param: In]
			set;
		}

		// Token: 0x17000441 RID: 1089
		// (get) Token: 0x060008A1 RID: 2209
		// (set) Token: 0x060008A0 RID: 2208
		public virtual extern string IHTMLRuleStyle2_textAutospace
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

		// Token: 0x17000442 RID: 1090
		// (get) Token: 0x060008A3 RID: 2211
		// (set) Token: 0x060008A2 RID: 2210
		public virtual extern string IHTMLRuleStyle2_wordBreak
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

		// Token: 0x17000443 RID: 1091
		// (get) Token: 0x060008A5 RID: 2213
		// (set) Token: 0x060008A4 RID: 2212
		public virtual extern string IHTMLRuleStyle2_lineBreak
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

		// Token: 0x17000444 RID: 1092
		// (get) Token: 0x060008A7 RID: 2215
		// (set) Token: 0x060008A6 RID: 2214
		public virtual extern string IHTMLRuleStyle2_textJustify
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

		// Token: 0x17000445 RID: 1093
		// (get) Token: 0x060008A9 RID: 2217
		// (set) Token: 0x060008A8 RID: 2216
		public virtual extern string IHTMLRuleStyle2_textJustifyTrim
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

		// Token: 0x17000446 RID: 1094
		// (get) Token: 0x060008AB RID: 2219
		// (set) Token: 0x060008AA RID: 2218
		public virtual extern object IHTMLRuleStyle2_textKashida
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

		// Token: 0x17000447 RID: 1095
		// (get) Token: 0x060008AD RID: 2221
		// (set) Token: 0x060008AC RID: 2220
		public virtual extern string IHTMLRuleStyle2_overflowX
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

		// Token: 0x17000448 RID: 1096
		// (get) Token: 0x060008AF RID: 2223
		// (set) Token: 0x060008AE RID: 2222
		public virtual extern string IHTMLRuleStyle2_overflowY
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

		// Token: 0x17000449 RID: 1097
		// (get) Token: 0x060008B1 RID: 2225
		// (set) Token: 0x060008B0 RID: 2224
		public virtual extern string IHTMLRuleStyle2_accelerator
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

		// Token: 0x1700044A RID: 1098
		// (get) Token: 0x060008B3 RID: 2227
		// (set) Token: 0x060008B2 RID: 2226
		public virtual extern string IHTMLRuleStyle3_layoutFlow
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

		// Token: 0x1700044B RID: 1099
		// (get) Token: 0x060008B5 RID: 2229
		// (set) Token: 0x060008B4 RID: 2228
		public virtual extern object IHTMLRuleStyle3_zoom
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

		// Token: 0x1700044C RID: 1100
		// (get) Token: 0x060008B7 RID: 2231
		// (set) Token: 0x060008B6 RID: 2230
		public virtual extern string IHTMLRuleStyle3_wordWrap
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

		// Token: 0x1700044D RID: 1101
		// (get) Token: 0x060008B9 RID: 2233
		// (set) Token: 0x060008B8 RID: 2232
		public virtual extern string IHTMLRuleStyle3_textUnderlinePosition
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

		// Token: 0x1700044E RID: 1102
		// (get) Token: 0x060008BB RID: 2235
		// (set) Token: 0x060008BA RID: 2234
		public virtual extern object IHTMLRuleStyle3_scrollbarBaseColor
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

		// Token: 0x1700044F RID: 1103
		// (get) Token: 0x060008BD RID: 2237
		// (set) Token: 0x060008BC RID: 2236
		public virtual extern object IHTMLRuleStyle3_scrollbarFaceColor
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

		// Token: 0x17000450 RID: 1104
		// (get) Token: 0x060008BF RID: 2239
		// (set) Token: 0x060008BE RID: 2238
		public virtual extern object IHTMLRuleStyle3_scrollbar3dLightColor
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

		// Token: 0x17000451 RID: 1105
		// (get) Token: 0x060008C1 RID: 2241
		// (set) Token: 0x060008C0 RID: 2240
		public virtual extern object IHTMLRuleStyle3_scrollbarShadowColor
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

		// Token: 0x17000452 RID: 1106
		// (get) Token: 0x060008C3 RID: 2243
		// (set) Token: 0x060008C2 RID: 2242
		public virtual extern object IHTMLRuleStyle3_scrollbarHighlightColor
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

		// Token: 0x17000453 RID: 1107
		// (get) Token: 0x060008C5 RID: 2245
		// (set) Token: 0x060008C4 RID: 2244
		public virtual extern object IHTMLRuleStyle3_scrollbarDarkShadowColor
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

		// Token: 0x17000454 RID: 1108
		// (get) Token: 0x060008C7 RID: 2247
		// (set) Token: 0x060008C6 RID: 2246
		public virtual extern object IHTMLRuleStyle3_scrollbarArrowColor
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

		// Token: 0x17000455 RID: 1109
		// (get) Token: 0x060008C9 RID: 2249
		// (set) Token: 0x060008C8 RID: 2248
		public virtual extern object IHTMLRuleStyle3_scrollbarTrackColor
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

		// Token: 0x17000456 RID: 1110
		// (get) Token: 0x060008CB RID: 2251
		// (set) Token: 0x060008CA RID: 2250
		public virtual extern string IHTMLRuleStyle3_writingMode
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

		// Token: 0x17000457 RID: 1111
		// (get) Token: 0x060008CD RID: 2253
		// (set) Token: 0x060008CC RID: 2252
		public virtual extern string IHTMLRuleStyle3_textAlignLast
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

		// Token: 0x17000458 RID: 1112
		// (get) Token: 0x060008CF RID: 2255
		// (set) Token: 0x060008CE RID: 2254
		public virtual extern object IHTMLRuleStyle3_textKashidaSpace
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

		// Token: 0x17000459 RID: 1113
		// (get) Token: 0x060008D1 RID: 2257
		// (set) Token: 0x060008D0 RID: 2256
		public virtual extern string IHTMLRuleStyle4_textOverflow
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

		// Token: 0x1700045A RID: 1114
		// (get) Token: 0x060008D3 RID: 2259
		// (set) Token: 0x060008D2 RID: 2258
		public virtual extern object IHTMLRuleStyle4_minHeight
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
	}
}
