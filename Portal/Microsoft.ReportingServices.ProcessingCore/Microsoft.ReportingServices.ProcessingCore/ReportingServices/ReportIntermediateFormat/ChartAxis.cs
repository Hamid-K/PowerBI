using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.OnDemandProcessing;
using Microsoft.ReportingServices.OnDemandReportRendering;
using Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportProcessing.OnDemandReportObjectModel;
using Microsoft.ReportingServices.ReportPublishing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x020004A2 RID: 1186
	[Serializable]
	internal class ChartAxis : ChartStyleContainer, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable, ICustomPropertiesHolder
	{
		// Token: 0x060039AB RID: 14763 RVA: 0x000FA757 File Offset: 0x000F8957
		internal ChartAxis()
		{
		}

		// Token: 0x060039AC RID: 14764 RVA: 0x000FA774 File Offset: 0x000F8974
		internal ChartAxis(Microsoft.ReportingServices.ReportIntermediateFormat.Chart chart)
			: base(chart)
		{
		}

		// Token: 0x170018FD RID: 6397
		// (get) Token: 0x060039AD RID: 14765 RVA: 0x000FA792 File Offset: 0x000F8992
		// (set) Token: 0x060039AE RID: 14766 RVA: 0x000FA79A File Offset: 0x000F899A
		internal string AxisName
		{
			get
			{
				return this.m_name;
			}
			set
			{
				this.m_name = value;
			}
		}

		// Token: 0x170018FE RID: 6398
		// (get) Token: 0x060039AF RID: 14767 RVA: 0x000FA7A3 File Offset: 0x000F89A3
		// (set) Token: 0x060039B0 RID: 14768 RVA: 0x000FA7AB File Offset: 0x000F89AB
		internal ChartAxisTitle Title
		{
			get
			{
				return this.m_title;
			}
			set
			{
				this.m_title = value;
			}
		}

		// Token: 0x170018FF RID: 6399
		// (get) Token: 0x060039B1 RID: 14769 RVA: 0x000FA7B4 File Offset: 0x000F89B4
		// (set) Token: 0x060039B2 RID: 14770 RVA: 0x000FA7BC File Offset: 0x000F89BC
		internal ChartGridLines MajorGridLines
		{
			get
			{
				return this.m_majorGridLines;
			}
			set
			{
				this.m_majorGridLines = value;
			}
		}

		// Token: 0x17001900 RID: 6400
		// (get) Token: 0x060039B3 RID: 14771 RVA: 0x000FA7C5 File Offset: 0x000F89C5
		// (set) Token: 0x060039B4 RID: 14772 RVA: 0x000FA7CD File Offset: 0x000F89CD
		internal ChartGridLines MinorGridLines
		{
			get
			{
				return this.m_minorGridLines;
			}
			set
			{
				this.m_minorGridLines = value;
			}
		}

		// Token: 0x17001901 RID: 6401
		// (get) Token: 0x060039B5 RID: 14773 RVA: 0x000FA7D6 File Offset: 0x000F89D6
		// (set) Token: 0x060039B6 RID: 14774 RVA: 0x000FA7DE File Offset: 0x000F89DE
		internal List<ChartStripLine> StripLines
		{
			get
			{
				return this.m_chartStripLines;
			}
			set
			{
				this.m_chartStripLines = value;
			}
		}

		// Token: 0x17001902 RID: 6402
		// (get) Token: 0x060039B7 RID: 14775 RVA: 0x000FA7E7 File Offset: 0x000F89E7
		// (set) Token: 0x060039B8 RID: 14776 RVA: 0x000FA7EF File Offset: 0x000F89EF
		public DataValueList CustomProperties
		{
			get
			{
				return this.m_customProperties;
			}
			set
			{
				this.m_customProperties = value;
			}
		}

		// Token: 0x17001903 RID: 6403
		// (get) Token: 0x060039B9 RID: 14777 RVA: 0x000FA7F8 File Offset: 0x000F89F8
		// (set) Token: 0x060039BA RID: 14778 RVA: 0x000FA800 File Offset: 0x000F8A00
		internal bool Scalar
		{
			get
			{
				return this.m_scalar;
			}
			set
			{
				this.m_scalar = value;
			}
		}

		// Token: 0x17001904 RID: 6404
		// (get) Token: 0x060039BB RID: 14779 RVA: 0x000FA809 File Offset: 0x000F8A09
		// (set) Token: 0x060039BC RID: 14780 RVA: 0x000FA811 File Offset: 0x000F8A11
		internal ExpressionInfo Minimum
		{
			get
			{
				return this.m_min;
			}
			set
			{
				this.m_min = value;
			}
		}

		// Token: 0x17001905 RID: 6405
		// (get) Token: 0x060039BD RID: 14781 RVA: 0x000FA81A File Offset: 0x000F8A1A
		// (set) Token: 0x060039BE RID: 14782 RVA: 0x000FA822 File Offset: 0x000F8A22
		internal ExpressionInfo Maximum
		{
			get
			{
				return this.m_max;
			}
			set
			{
				this.m_max = value;
			}
		}

		// Token: 0x17001906 RID: 6406
		// (get) Token: 0x060039BF RID: 14783 RVA: 0x000FA82B File Offset: 0x000F8A2B
		// (set) Token: 0x060039C0 RID: 14784 RVA: 0x000FA833 File Offset: 0x000F8A33
		internal ExpressionInfo CrossAt
		{
			get
			{
				return this.m_crossAt;
			}
			set
			{
				this.m_crossAt = value;
			}
		}

		// Token: 0x17001907 RID: 6407
		// (get) Token: 0x060039C1 RID: 14785 RVA: 0x000FA83C File Offset: 0x000F8A3C
		// (set) Token: 0x060039C2 RID: 14786 RVA: 0x000FA844 File Offset: 0x000F8A44
		internal bool AutoCrossAt
		{
			get
			{
				return this.m_autoCrossAt;
			}
			set
			{
				this.m_autoCrossAt = value;
			}
		}

		// Token: 0x17001908 RID: 6408
		// (get) Token: 0x060039C3 RID: 14787 RVA: 0x000FA84D File Offset: 0x000F8A4D
		// (set) Token: 0x060039C4 RID: 14788 RVA: 0x000FA855 File Offset: 0x000F8A55
		internal bool AutoScaleMin
		{
			get
			{
				return this.m_autoScaleMin;
			}
			set
			{
				this.m_autoScaleMin = value;
			}
		}

		// Token: 0x17001909 RID: 6409
		// (get) Token: 0x060039C5 RID: 14789 RVA: 0x000FA85E File Offset: 0x000F8A5E
		// (set) Token: 0x060039C6 RID: 14790 RVA: 0x000FA866 File Offset: 0x000F8A66
		internal bool AutoScaleMax
		{
			get
			{
				return this.m_autoScaleMax;
			}
			set
			{
				this.m_autoScaleMax = value;
			}
		}

		// Token: 0x1700190A RID: 6410
		// (get) Token: 0x060039C7 RID: 14791 RVA: 0x000FA86F File Offset: 0x000F8A6F
		// (set) Token: 0x060039C8 RID: 14792 RVA: 0x000FA877 File Offset: 0x000F8A77
		internal ExpressionInfo Visible
		{
			get
			{
				return this.m_visible;
			}
			set
			{
				this.m_visible = value;
			}
		}

		// Token: 0x1700190B RID: 6411
		// (get) Token: 0x060039C9 RID: 14793 RVA: 0x000FA880 File Offset: 0x000F8A80
		// (set) Token: 0x060039CA RID: 14794 RVA: 0x000FA888 File Offset: 0x000F8A88
		internal ExpressionInfo Margin
		{
			get
			{
				return this.m_margin;
			}
			set
			{
				this.m_margin = value;
			}
		}

		// Token: 0x1700190C RID: 6412
		// (get) Token: 0x060039CB RID: 14795 RVA: 0x000FA891 File Offset: 0x000F8A91
		// (set) Token: 0x060039CC RID: 14796 RVA: 0x000FA899 File Offset: 0x000F8A99
		internal ExpressionInfo Interval
		{
			get
			{
				return this.m_interval;
			}
			set
			{
				this.m_interval = value;
			}
		}

		// Token: 0x1700190D RID: 6413
		// (get) Token: 0x060039CD RID: 14797 RVA: 0x000FA8A2 File Offset: 0x000F8AA2
		// (set) Token: 0x060039CE RID: 14798 RVA: 0x000FA8AA File Offset: 0x000F8AAA
		internal ExpressionInfo IntervalType
		{
			get
			{
				return this.m_intervalType;
			}
			set
			{
				this.m_intervalType = value;
			}
		}

		// Token: 0x1700190E RID: 6414
		// (get) Token: 0x060039CF RID: 14799 RVA: 0x000FA8B3 File Offset: 0x000F8AB3
		// (set) Token: 0x060039D0 RID: 14800 RVA: 0x000FA8BB File Offset: 0x000F8ABB
		internal ExpressionInfo IntervalOffset
		{
			get
			{
				return this.m_intervalOffset;
			}
			set
			{
				this.m_intervalOffset = value;
			}
		}

		// Token: 0x1700190F RID: 6415
		// (get) Token: 0x060039D1 RID: 14801 RVA: 0x000FA8C4 File Offset: 0x000F8AC4
		// (set) Token: 0x060039D2 RID: 14802 RVA: 0x000FA8CC File Offset: 0x000F8ACC
		internal ExpressionInfo IntervalOffsetType
		{
			get
			{
				return this.m_intervalOffsetType;
			}
			set
			{
				this.m_intervalOffsetType = value;
			}
		}

		// Token: 0x17001910 RID: 6416
		// (get) Token: 0x060039D3 RID: 14803 RVA: 0x000FA8D5 File Offset: 0x000F8AD5
		// (set) Token: 0x060039D4 RID: 14804 RVA: 0x000FA8DD File Offset: 0x000F8ADD
		internal ChartTickMarks MajorTickMarks
		{
			get
			{
				return this.m_majorTickMarks;
			}
			set
			{
				this.m_majorTickMarks = value;
			}
		}

		// Token: 0x17001911 RID: 6417
		// (get) Token: 0x060039D5 RID: 14805 RVA: 0x000FA8E6 File Offset: 0x000F8AE6
		// (set) Token: 0x060039D6 RID: 14806 RVA: 0x000FA8EE File Offset: 0x000F8AEE
		internal ChartTickMarks MinorTickMarks
		{
			get
			{
				return this.m_minorTickMarks;
			}
			set
			{
				this.m_minorTickMarks = value;
			}
		}

		// Token: 0x17001912 RID: 6418
		// (get) Token: 0x060039D7 RID: 14807 RVA: 0x000FA8F7 File Offset: 0x000F8AF7
		// (set) Token: 0x060039D8 RID: 14808 RVA: 0x000FA8FF File Offset: 0x000F8AFF
		internal ExpressionInfo MarksAlwaysAtPlotEdge
		{
			get
			{
				return this.m_marksAlwaysAtPlotEdge;
			}
			set
			{
				this.m_marksAlwaysAtPlotEdge = value;
			}
		}

		// Token: 0x17001913 RID: 6419
		// (get) Token: 0x060039D9 RID: 14809 RVA: 0x000FA908 File Offset: 0x000F8B08
		// (set) Token: 0x060039DA RID: 14810 RVA: 0x000FA910 File Offset: 0x000F8B10
		internal ExpressionInfo Reverse
		{
			get
			{
				return this.m_reverse;
			}
			set
			{
				this.m_reverse = value;
			}
		}

		// Token: 0x17001914 RID: 6420
		// (get) Token: 0x060039DB RID: 14811 RVA: 0x000FA919 File Offset: 0x000F8B19
		// (set) Token: 0x060039DC RID: 14812 RVA: 0x000FA921 File Offset: 0x000F8B21
		internal ExpressionInfo Location
		{
			get
			{
				return this.m_location;
			}
			set
			{
				this.m_location = value;
			}
		}

		// Token: 0x17001915 RID: 6421
		// (get) Token: 0x060039DD RID: 14813 RVA: 0x000FA92A File Offset: 0x000F8B2A
		// (set) Token: 0x060039DE RID: 14814 RVA: 0x000FA932 File Offset: 0x000F8B32
		internal ExpressionInfo Interlaced
		{
			get
			{
				return this.m_interlaced;
			}
			set
			{
				this.m_interlaced = value;
			}
		}

		// Token: 0x17001916 RID: 6422
		// (get) Token: 0x060039DF RID: 14815 RVA: 0x000FA93B File Offset: 0x000F8B3B
		// (set) Token: 0x060039E0 RID: 14816 RVA: 0x000FA943 File Offset: 0x000F8B43
		internal ExpressionInfo InterlacedColor
		{
			get
			{
				return this.m_interlacedColor;
			}
			set
			{
				this.m_interlacedColor = value;
			}
		}

		// Token: 0x17001917 RID: 6423
		// (get) Token: 0x060039E1 RID: 14817 RVA: 0x000FA94C File Offset: 0x000F8B4C
		// (set) Token: 0x060039E2 RID: 14818 RVA: 0x000FA954 File Offset: 0x000F8B54
		internal ExpressionInfo LogScale
		{
			get
			{
				return this.m_logScale;
			}
			set
			{
				this.m_logScale = value;
			}
		}

		// Token: 0x17001918 RID: 6424
		// (get) Token: 0x060039E3 RID: 14819 RVA: 0x000FA95D File Offset: 0x000F8B5D
		// (set) Token: 0x060039E4 RID: 14820 RVA: 0x000FA965 File Offset: 0x000F8B65
		internal ExpressionInfo LogBase
		{
			get
			{
				return this.m_logBase;
			}
			set
			{
				this.m_logBase = value;
			}
		}

		// Token: 0x17001919 RID: 6425
		// (get) Token: 0x060039E5 RID: 14821 RVA: 0x000FA96E File Offset: 0x000F8B6E
		// (set) Token: 0x060039E6 RID: 14822 RVA: 0x000FA976 File Offset: 0x000F8B76
		internal ExpressionInfo HideLabels
		{
			get
			{
				return this.m_hideLabels;
			}
			set
			{
				this.m_hideLabels = value;
			}
		}

		// Token: 0x1700191A RID: 6426
		// (get) Token: 0x060039E7 RID: 14823 RVA: 0x000FA97F File Offset: 0x000F8B7F
		// (set) Token: 0x060039E8 RID: 14824 RVA: 0x000FA987 File Offset: 0x000F8B87
		internal ExpressionInfo Angle
		{
			get
			{
				return this.m_angle;
			}
			set
			{
				this.m_angle = value;
			}
		}

		// Token: 0x1700191B RID: 6427
		// (get) Token: 0x060039E9 RID: 14825 RVA: 0x000FA990 File Offset: 0x000F8B90
		// (set) Token: 0x060039EA RID: 14826 RVA: 0x000FA998 File Offset: 0x000F8B98
		internal ExpressionInfo Arrows
		{
			get
			{
				return this.m_arrows;
			}
			set
			{
				this.m_arrows = value;
			}
		}

		// Token: 0x1700191C RID: 6428
		// (get) Token: 0x060039EB RID: 14827 RVA: 0x000FA9A1 File Offset: 0x000F8BA1
		// (set) Token: 0x060039EC RID: 14828 RVA: 0x000FA9A9 File Offset: 0x000F8BA9
		internal ExpressionInfo PreventFontShrink
		{
			get
			{
				return this.m_preventFontShrink;
			}
			set
			{
				this.m_preventFontShrink = value;
			}
		}

		// Token: 0x1700191D RID: 6429
		// (get) Token: 0x060039ED RID: 14829 RVA: 0x000FA9B2 File Offset: 0x000F8BB2
		// (set) Token: 0x060039EE RID: 14830 RVA: 0x000FA9BA File Offset: 0x000F8BBA
		internal ExpressionInfo PreventFontGrow
		{
			get
			{
				return this.m_preventFontGrow;
			}
			set
			{
				this.m_preventFontGrow = value;
			}
		}

		// Token: 0x1700191E RID: 6430
		// (get) Token: 0x060039EF RID: 14831 RVA: 0x000FA9C3 File Offset: 0x000F8BC3
		// (set) Token: 0x060039F0 RID: 14832 RVA: 0x000FA9CB File Offset: 0x000F8BCB
		internal ExpressionInfo PreventLabelOffset
		{
			get
			{
				return this.m_preventLabelOffset;
			}
			set
			{
				this.m_preventLabelOffset = value;
			}
		}

		// Token: 0x1700191F RID: 6431
		// (get) Token: 0x060039F1 RID: 14833 RVA: 0x000FA9D4 File Offset: 0x000F8BD4
		// (set) Token: 0x060039F2 RID: 14834 RVA: 0x000FA9DC File Offset: 0x000F8BDC
		internal ExpressionInfo PreventWordWrap
		{
			get
			{
				return this.m_preventWordWrap;
			}
			set
			{
				this.m_preventWordWrap = value;
			}
		}

		// Token: 0x17001920 RID: 6432
		// (get) Token: 0x060039F3 RID: 14835 RVA: 0x000FA9E5 File Offset: 0x000F8BE5
		// (set) Token: 0x060039F4 RID: 14836 RVA: 0x000FA9ED File Offset: 0x000F8BED
		internal ExpressionInfo AllowLabelRotation
		{
			get
			{
				return this.m_allowLabelRotation;
			}
			set
			{
				this.m_allowLabelRotation = value;
			}
		}

		// Token: 0x17001921 RID: 6433
		// (get) Token: 0x060039F5 RID: 14837 RVA: 0x000FA9F6 File Offset: 0x000F8BF6
		// (set) Token: 0x060039F6 RID: 14838 RVA: 0x000FA9FE File Offset: 0x000F8BFE
		internal ExpressionInfo IncludeZero
		{
			get
			{
				return this.m_includeZero;
			}
			set
			{
				this.m_includeZero = value;
			}
		}

		// Token: 0x17001922 RID: 6434
		// (get) Token: 0x060039F7 RID: 14839 RVA: 0x000FAA07 File Offset: 0x000F8C07
		// (set) Token: 0x060039F8 RID: 14840 RVA: 0x000FAA0F File Offset: 0x000F8C0F
		internal ExpressionInfo LabelsAutoFitDisabled
		{
			get
			{
				return this.m_labelsAutoFitDisabled;
			}
			set
			{
				this.m_labelsAutoFitDisabled = value;
			}
		}

		// Token: 0x17001923 RID: 6435
		// (get) Token: 0x060039F9 RID: 14841 RVA: 0x000FAA18 File Offset: 0x000F8C18
		// (set) Token: 0x060039FA RID: 14842 RVA: 0x000FAA20 File Offset: 0x000F8C20
		internal ExpressionInfo MinFontSize
		{
			get
			{
				return this.m_minFontSize;
			}
			set
			{
				this.m_minFontSize = value;
			}
		}

		// Token: 0x17001924 RID: 6436
		// (get) Token: 0x060039FB RID: 14843 RVA: 0x000FAA29 File Offset: 0x000F8C29
		// (set) Token: 0x060039FC RID: 14844 RVA: 0x000FAA31 File Offset: 0x000F8C31
		internal ExpressionInfo MaxFontSize
		{
			get
			{
				return this.m_maxFontSize;
			}
			set
			{
				this.m_maxFontSize = value;
			}
		}

		// Token: 0x17001925 RID: 6437
		// (get) Token: 0x060039FD RID: 14845 RVA: 0x000FAA3A File Offset: 0x000F8C3A
		// (set) Token: 0x060039FE RID: 14846 RVA: 0x000FAA42 File Offset: 0x000F8C42
		internal ExpressionInfo OffsetLabels
		{
			get
			{
				return this.m_offsetLabels;
			}
			set
			{
				this.m_offsetLabels = value;
			}
		}

		// Token: 0x17001926 RID: 6438
		// (get) Token: 0x060039FF RID: 14847 RVA: 0x000FAA4B File Offset: 0x000F8C4B
		// (set) Token: 0x06003A00 RID: 14848 RVA: 0x000FAA53 File Offset: 0x000F8C53
		internal ExpressionInfo HideEndLabels
		{
			get
			{
				return this.m_hideEndLabels;
			}
			set
			{
				this.m_hideEndLabels = value;
			}
		}

		// Token: 0x17001927 RID: 6439
		// (get) Token: 0x06003A01 RID: 14849 RVA: 0x000FAA5C File Offset: 0x000F8C5C
		// (set) Token: 0x06003A02 RID: 14850 RVA: 0x000FAA64 File Offset: 0x000F8C64
		internal ChartAxisScaleBreak AxisScaleBreak
		{
			get
			{
				return this.m_axisScaleBreak;
			}
			set
			{
				this.m_axisScaleBreak = value;
			}
		}

		// Token: 0x17001928 RID: 6440
		// (get) Token: 0x06003A03 RID: 14851 RVA: 0x000FAA6D File Offset: 0x000F8C6D
		// (set) Token: 0x06003A04 RID: 14852 RVA: 0x000FAA75 File Offset: 0x000F8C75
		internal ExpressionInfo VariableAutoInterval
		{
			get
			{
				return this.m_variableAutoInterval;
			}
			set
			{
				this.m_variableAutoInterval = value;
			}
		}

		// Token: 0x17001929 RID: 6441
		// (get) Token: 0x06003A05 RID: 14853 RVA: 0x000FAA7E File Offset: 0x000F8C7E
		// (set) Token: 0x06003A06 RID: 14854 RVA: 0x000FAA86 File Offset: 0x000F8C86
		internal ExpressionInfo LabelInterval
		{
			get
			{
				return this.m_labelInterval;
			}
			set
			{
				this.m_labelInterval = value;
			}
		}

		// Token: 0x1700192A RID: 6442
		// (get) Token: 0x06003A07 RID: 14855 RVA: 0x000FAA8F File Offset: 0x000F8C8F
		// (set) Token: 0x06003A08 RID: 14856 RVA: 0x000FAA97 File Offset: 0x000F8C97
		internal ExpressionInfo LabelIntervalType
		{
			get
			{
				return this.m_labelIntervalType;
			}
			set
			{
				this.m_labelIntervalType = value;
			}
		}

		// Token: 0x1700192B RID: 6443
		// (get) Token: 0x06003A09 RID: 14857 RVA: 0x000FAAA0 File Offset: 0x000F8CA0
		// (set) Token: 0x06003A0A RID: 14858 RVA: 0x000FAAA8 File Offset: 0x000F8CA8
		internal ExpressionInfo LabelIntervalOffset
		{
			get
			{
				return this.m_labelIntervalOffset;
			}
			set
			{
				this.m_labelIntervalOffset = value;
			}
		}

		// Token: 0x1700192C RID: 6444
		// (get) Token: 0x06003A0B RID: 14859 RVA: 0x000FAAB1 File Offset: 0x000F8CB1
		// (set) Token: 0x06003A0C RID: 14860 RVA: 0x000FAAB9 File Offset: 0x000F8CB9
		internal ExpressionInfo LabelIntervalOffsetType
		{
			get
			{
				return this.m_labelIntervalOffsetType;
			}
			set
			{
				this.m_labelIntervalOffsetType = value;
			}
		}

		// Token: 0x1700192D RID: 6445
		// (get) Token: 0x06003A0D RID: 14861 RVA: 0x000FAAC2 File Offset: 0x000F8CC2
		internal ChartAxisExprHost ExprHost
		{
			get
			{
				return this.m_exprHost;
			}
		}

		// Token: 0x1700192E RID: 6446
		// (get) Token: 0x06003A0E RID: 14862 RVA: 0x000FAACA File Offset: 0x000F8CCA
		internal int ExpressionHostID
		{
			get
			{
				return this.m_exprHostID;
			}
		}

		// Token: 0x06003A0F RID: 14863 RVA: 0x000FAAD4 File Offset: 0x000F8CD4
		internal void SetExprHost(ChartAxisExprHost exprHost, ObjectModelImpl reportObjectModel)
		{
			Global.Tracer.Assert(exprHost != null && reportObjectModel != null);
			base.SetExprHost(exprHost, reportObjectModel);
			this.m_exprHost = exprHost;
			if (this.m_title != null && this.m_exprHost.TitleHost != null)
			{
				this.m_title.SetExprHost(this.m_exprHost.TitleHost, reportObjectModel);
			}
			if (this.m_majorGridLines != null && this.m_exprHost.MajorGridLinesHost != null)
			{
				this.m_majorGridLines.SetExprHost(this.m_exprHost.MajorGridLinesHost, reportObjectModel);
			}
			if (this.m_minorGridLines != null && this.m_exprHost.MinorGridLinesHost != null)
			{
				this.m_minorGridLines.SetExprHost(this.m_exprHost.MinorGridLinesHost, reportObjectModel);
			}
			if (this.m_exprHost.CustomPropertyHostsRemotable != null)
			{
				Global.Tracer.Assert(this.m_customProperties != null, "(null != m_customProperties)");
				this.m_customProperties.SetExprHost(this.m_exprHost.CustomPropertyHostsRemotable, reportObjectModel);
			}
			IList<ChartStripLineExprHost> chartStripLinesHostsRemotable = this.m_exprHost.ChartStripLinesHostsRemotable;
			if (this.m_chartStripLines != null && chartStripLinesHostsRemotable != null)
			{
				for (int i = 0; i < this.m_chartStripLines.Count; i++)
				{
					ChartStripLine chartStripLine = this.m_chartStripLines[i];
					if (chartStripLine != null && chartStripLine.ExpressionHostID > -1)
					{
						chartStripLine.SetExprHost(chartStripLinesHostsRemotable[chartStripLine.ExpressionHostID], reportObjectModel);
					}
				}
			}
			if (this.m_majorTickMarks != null && this.m_exprHost.MajorTickMarksHost != null)
			{
				this.m_majorTickMarks.SetExprHost(this.m_exprHost.MajorTickMarksHost, reportObjectModel);
			}
			if (this.m_minorTickMarks != null && this.m_exprHost.MinorTickMarksHost != null)
			{
				this.m_minorTickMarks.SetExprHost(this.m_exprHost.MinorTickMarksHost, reportObjectModel);
			}
			if (this.m_axisScaleBreak != null && this.m_exprHost.AxisScaleBreakHost != null)
			{
				this.m_axisScaleBreak.SetExprHost(this.m_exprHost.AxisScaleBreakHost, reportObjectModel);
			}
		}

		// Token: 0x06003A10 RID: 14864 RVA: 0x000FACA4 File Offset: 0x000F8EA4
		internal virtual void Initialize(InitializationContext context, bool isValueAxis)
		{
			string propertyName = this.GetPropertyName(isValueAxis);
			context.ExprHostBuilder.ChartAxisStart(this.m_name, isValueAxis);
			if (this.m_styleClass != null)
			{
				this.m_styleClass.Initialize(context);
			}
			if (this.m_title != null)
			{
				this.m_title.Initialize(context);
			}
			if (this.m_minorGridLines != null)
			{
				this.m_minorGridLines.Initialize(context, false);
			}
			if (this.m_majorGridLines != null)
			{
				this.m_majorGridLines.Initialize(context, true);
			}
			if (this.m_min != null)
			{
				if (this.m_min.InitializeAxisExpression(propertyName + ".Minimum", context))
				{
					context.ExprHostBuilder.AxisMin(this.m_min);
				}
				else
				{
					this.m_min = null;
				}
			}
			if (this.m_max != null)
			{
				if (this.m_max.InitializeAxisExpression(propertyName + ".Maximum", context))
				{
					context.ExprHostBuilder.AxisMax(this.m_max);
				}
				else
				{
					this.m_max = null;
				}
			}
			if (this.m_crossAt != null)
			{
				if (this.m_crossAt.InitializeAxisExpression(propertyName + ".CrossAt", context))
				{
					context.ExprHostBuilder.AxisCrossAt(this.m_crossAt);
				}
				else
				{
					this.m_crossAt = null;
				}
			}
			if (this.m_customProperties != null)
			{
				this.m_customProperties.Initialize(propertyName + ".", context);
			}
			if (this.m_chartStripLines != null)
			{
				for (int i = 0; i < this.m_chartStripLines.Count; i++)
				{
					this.m_chartStripLines[i].Initialize(context, i);
				}
			}
			if (this.m_visible != null)
			{
				this.m_visible.Initialize("Visible", context);
				context.ExprHostBuilder.ChartAxisVisible(this.m_visible);
			}
			if (this.m_margin != null)
			{
				this.m_margin.Initialize("Margin", context);
				context.ExprHostBuilder.ChartAxisMargin(this.m_margin);
			}
			if (this.m_interval != null)
			{
				this.m_interval.Initialize("Interval", context);
				context.ExprHostBuilder.ChartAxisInterval(this.m_interval);
			}
			if (this.m_intervalType != null)
			{
				this.m_intervalType.Initialize("IntervalType", context);
				context.ExprHostBuilder.ChartAxisIntervalType(this.m_intervalType);
			}
			if (this.m_intervalOffset != null)
			{
				this.m_intervalOffset.Initialize("IntervalOffset", context);
				context.ExprHostBuilder.ChartAxisIntervalOffset(this.m_intervalOffset);
			}
			if (this.m_intervalOffsetType != null)
			{
				this.m_intervalOffsetType.Initialize("IntervalOffsetType", context);
				context.ExprHostBuilder.ChartAxisIntervalOffsetType(this.m_intervalOffsetType);
			}
			if (this.m_majorTickMarks != null)
			{
				this.m_majorTickMarks.Initialize(context, true);
			}
			if (this.m_minorTickMarks != null)
			{
				this.m_minorTickMarks.Initialize(context, false);
			}
			if (this.m_marksAlwaysAtPlotEdge != null)
			{
				this.m_marksAlwaysAtPlotEdge.Initialize("MarksAlwaysAtPlotEdge", context);
				context.ExprHostBuilder.ChartAxisMarksAlwaysAtPlotEdge(this.m_marksAlwaysAtPlotEdge);
			}
			if (this.m_reverse != null)
			{
				this.m_reverse.Initialize("Reverse", context);
				context.ExprHostBuilder.ChartAxisReverse(this.m_reverse);
			}
			if (this.m_location != null)
			{
				this.m_location.Initialize("Location", context);
				context.ExprHostBuilder.ChartAxisLocation(this.m_location);
			}
			if (this.m_interlaced != null)
			{
				this.m_interlaced.Initialize("Interlaced", context);
				context.ExprHostBuilder.ChartAxisInterlaced(this.m_interlaced);
			}
			if (this.m_interlacedColor != null)
			{
				this.m_interlacedColor.Initialize("InterlacedColor", context);
				context.ExprHostBuilder.ChartAxisInterlacedColor(this.m_interlacedColor);
			}
			if (this.m_logScale != null)
			{
				this.m_logScale.Initialize("LogScale", context);
				context.ExprHostBuilder.ChartAxisLogScale(this.m_logScale);
			}
			if (this.m_logBase != null)
			{
				this.m_logBase.Initialize("LogBase", context);
				context.ExprHostBuilder.ChartAxisLogBase(this.m_logBase);
			}
			if (this.m_hideLabels != null)
			{
				this.m_hideLabels.Initialize("HideLabels", context);
				context.ExprHostBuilder.ChartAxisHideLabels(this.m_hideLabels);
			}
			if (this.m_angle != null)
			{
				this.m_angle.Initialize("Angle", context);
				context.ExprHostBuilder.ChartAxisAngle(this.m_angle);
			}
			if (this.m_arrows != null)
			{
				this.m_arrows.Initialize("Arrows", context);
				context.ExprHostBuilder.ChartAxisArrows(this.m_arrows);
			}
			if (this.m_preventFontShrink != null)
			{
				this.m_preventFontShrink.Initialize("PreventFontShrink", context);
				context.ExprHostBuilder.ChartAxisPreventFontShrink(this.m_preventFontShrink);
			}
			if (this.m_preventFontGrow != null)
			{
				this.m_preventFontGrow.Initialize("PreventFontGrow", context);
				context.ExprHostBuilder.ChartAxisPreventFontGrow(this.m_preventFontGrow);
			}
			if (this.m_preventLabelOffset != null)
			{
				this.m_preventLabelOffset.Initialize("PreventLabelOffset", context);
				context.ExprHostBuilder.ChartAxisPreventLabelOffset(this.m_preventLabelOffset);
			}
			if (this.m_preventWordWrap != null)
			{
				this.m_preventWordWrap.Initialize("PreventWordWrap", context);
				context.ExprHostBuilder.ChartAxisPreventWordWrap(this.m_preventWordWrap);
			}
			if (this.m_allowLabelRotation != null)
			{
				this.m_allowLabelRotation.Initialize("AllowLabelRotation", context);
				context.ExprHostBuilder.ChartAxisAllowLabelRotation(this.m_allowLabelRotation);
			}
			if (this.m_includeZero != null)
			{
				this.m_includeZero.Initialize("IncludeZero", context);
				context.ExprHostBuilder.ChartAxisIncludeZero(this.m_includeZero);
			}
			if (this.m_labelsAutoFitDisabled != null)
			{
				this.m_labelsAutoFitDisabled.Initialize("LabelsAutoFitDisabled", context);
				context.ExprHostBuilder.ChartAxisLabelsAutoFitDisabled(this.m_labelsAutoFitDisabled);
			}
			if (this.m_minFontSize != null)
			{
				this.m_minFontSize.Initialize("MinFontSize", context);
				context.ExprHostBuilder.ChartAxisMinFontSize(this.m_minFontSize);
			}
			if (this.m_maxFontSize != null)
			{
				this.m_maxFontSize.Initialize("MaxFontSize", context);
				context.ExprHostBuilder.ChartAxisMaxFontSize(this.m_maxFontSize);
			}
			if (this.m_offsetLabels != null)
			{
				this.m_offsetLabels.Initialize("OffsetLabels", context);
				context.ExprHostBuilder.ChartAxisOffsetLabels(this.m_offsetLabels);
			}
			if (this.m_hideEndLabels != null)
			{
				this.m_hideEndLabels.Initialize("HideEndLabels", context);
				context.ExprHostBuilder.ChartAxisHideEndLabels(this.m_hideEndLabels);
			}
			if (this.m_axisScaleBreak != null)
			{
				this.m_axisScaleBreak.Initialize(context);
			}
			if (this.m_variableAutoInterval != null)
			{
				this.m_variableAutoInterval.Initialize("VariableAutoInterval", context);
				context.ExprHostBuilder.ChartAxisVariableAutoInterval(this.m_variableAutoInterval);
			}
			if (this.m_labelInterval != null)
			{
				this.m_labelInterval.Initialize("LabelInterval", context);
				context.ExprHostBuilder.ChartAxisLabelInterval(this.m_labelInterval);
			}
			if (this.m_labelIntervalType != null)
			{
				this.m_labelIntervalType.Initialize("LabelIntervalType", context);
				context.ExprHostBuilder.ChartAxisLabelIntervalType(this.m_labelIntervalType);
			}
			if (this.m_labelIntervalOffset != null)
			{
				this.m_labelIntervalOffset.Initialize("LabelIntervalOffset", context);
				context.ExprHostBuilder.ChartAxisLabelIntervalOffset(this.m_labelIntervalOffset);
			}
			if (this.m_labelIntervalOffsetType != null)
			{
				this.m_labelIntervalOffsetType.Initialize("LabelIntervalOffsetType", context);
				context.ExprHostBuilder.ChartAxisLabelIntervalOffsetType(this.m_labelIntervalOffsetType);
			}
			this.m_exprHostID = context.ExprHostBuilder.ChartAxisEnd(isValueAxis);
		}

		// Token: 0x06003A11 RID: 14865 RVA: 0x000FB3D4 File Offset: 0x000F95D4
		private string GetPropertyName(bool isValueAxis)
		{
			string text;
			if (!isValueAxis)
			{
				if (this.m_name == null)
				{
					this.m_name = "CategoryAxis";
					text = this.m_name;
				}
				else
				{
					text = "CategoryAxis_" + this.m_name;
				}
			}
			else if (this.m_name == null)
			{
				this.m_name = "ValueAxis";
				text = this.m_name;
			}
			else
			{
				text = "ValueAxis_" + this.m_name;
			}
			return text;
		}

		// Token: 0x06003A12 RID: 14866 RVA: 0x000FB444 File Offset: 0x000F9644
		internal override object PublishClone(AutomaticSubtotalContext context)
		{
			ChartAxis chartAxis = (ChartAxis)base.PublishClone(context);
			if (this.m_title != null)
			{
				chartAxis.m_title = (ChartAxisTitle)this.m_title.PublishClone(context);
			}
			if (this.m_majorGridLines != null)
			{
				chartAxis.m_majorGridLines = (ChartGridLines)this.m_majorGridLines.PublishClone(context);
			}
			if (this.m_minorGridLines != null)
			{
				chartAxis.m_minorGridLines = (ChartGridLines)this.m_minorGridLines.PublishClone(context);
			}
			if (this.m_crossAt != null)
			{
				chartAxis.m_crossAt = (ExpressionInfo)this.m_crossAt.PublishClone(context);
			}
			if (this.m_min != null)
			{
				chartAxis.m_min = (ExpressionInfo)this.m_min.PublishClone(context);
			}
			if (this.m_max != null)
			{
				chartAxis.m_max = (ExpressionInfo)this.m_max.PublishClone(context);
			}
			if (this.m_customProperties != null)
			{
				chartAxis.m_customProperties = new DataValueList(this.m_customProperties.Count);
				foreach (object obj in this.m_customProperties)
				{
					Microsoft.ReportingServices.ReportIntermediateFormat.DataValue dataValue = (Microsoft.ReportingServices.ReportIntermediateFormat.DataValue)obj;
					chartAxis.m_customProperties.Add(dataValue.PublishClone(context));
				}
			}
			if (this.m_chartStripLines != null)
			{
				chartAxis.m_chartStripLines = new List<ChartStripLine>(this.m_chartStripLines.Count);
				foreach (ChartStripLine chartStripLine in this.m_chartStripLines)
				{
					chartAxis.m_chartStripLines.Add((ChartStripLine)chartStripLine.PublishClone(context));
				}
			}
			if (this.m_visible != null)
			{
				chartAxis.m_visible = (ExpressionInfo)this.m_visible.PublishClone(context);
			}
			if (this.m_margin != null)
			{
				chartAxis.m_margin = (ExpressionInfo)this.m_margin.PublishClone(context);
			}
			if (this.m_interval != null)
			{
				chartAxis.m_interval = (ExpressionInfo)this.m_interval.PublishClone(context);
			}
			if (this.m_intervalType != null)
			{
				chartAxis.m_intervalType = (ExpressionInfo)this.m_intervalType.PublishClone(context);
			}
			if (this.m_intervalOffset != null)
			{
				chartAxis.m_intervalOffset = (ExpressionInfo)this.m_intervalOffset.PublishClone(context);
			}
			if (this.m_intervalOffsetType != null)
			{
				chartAxis.m_intervalOffsetType = (ExpressionInfo)this.m_intervalOffsetType.PublishClone(context);
			}
			if (this.m_majorTickMarks != null)
			{
				chartAxis.m_majorTickMarks = (ChartTickMarks)this.m_majorTickMarks.PublishClone(context);
			}
			if (this.m_minorTickMarks != null)
			{
				chartAxis.m_minorTickMarks = (ChartTickMarks)this.m_minorTickMarks.PublishClone(context);
			}
			if (this.m_marksAlwaysAtPlotEdge != null)
			{
				chartAxis.m_marksAlwaysAtPlotEdge = (ExpressionInfo)this.m_marksAlwaysAtPlotEdge.PublishClone(context);
			}
			if (this.m_reverse != null)
			{
				chartAxis.m_reverse = (ExpressionInfo)this.m_reverse.PublishClone(context);
			}
			if (this.m_location != null)
			{
				chartAxis.m_location = (ExpressionInfo)this.m_location.PublishClone(context);
			}
			if (this.m_interlaced != null)
			{
				chartAxis.m_interlaced = (ExpressionInfo)this.m_interlaced.PublishClone(context);
			}
			if (this.m_interlacedColor != null)
			{
				chartAxis.m_interlacedColor = (ExpressionInfo)this.m_interlacedColor.PublishClone(context);
			}
			if (this.m_logScale != null)
			{
				chartAxis.m_logScale = (ExpressionInfo)this.m_logScale.PublishClone(context);
			}
			if (this.m_logBase != null)
			{
				chartAxis.m_logBase = (ExpressionInfo)this.m_logBase.PublishClone(context);
			}
			if (this.m_hideLabels != null)
			{
				chartAxis.m_hideLabels = (ExpressionInfo)this.m_hideLabels.PublishClone(context);
			}
			if (this.m_angle != null)
			{
				chartAxis.m_angle = (ExpressionInfo)this.m_angle.PublishClone(context);
			}
			if (this.m_arrows != null)
			{
				chartAxis.m_arrows = (ExpressionInfo)this.m_arrows.PublishClone(context);
			}
			if (this.m_preventFontShrink != null)
			{
				chartAxis.m_preventFontShrink = (ExpressionInfo)this.m_preventFontShrink.PublishClone(context);
			}
			if (this.m_preventFontGrow != null)
			{
				chartAxis.m_preventFontGrow = (ExpressionInfo)this.m_preventFontGrow.PublishClone(context);
			}
			if (this.m_preventLabelOffset != null)
			{
				chartAxis.m_preventLabelOffset = (ExpressionInfo)this.m_preventLabelOffset.PublishClone(context);
			}
			if (this.m_preventWordWrap != null)
			{
				chartAxis.m_preventWordWrap = (ExpressionInfo)this.m_preventWordWrap.PublishClone(context);
			}
			if (this.m_allowLabelRotation != null)
			{
				chartAxis.m_allowLabelRotation = (ExpressionInfo)this.m_allowLabelRotation.PublishClone(context);
			}
			if (this.m_includeZero != null)
			{
				chartAxis.m_includeZero = (ExpressionInfo)this.m_includeZero.PublishClone(context);
			}
			if (this.m_labelsAutoFitDisabled != null)
			{
				chartAxis.m_labelsAutoFitDisabled = (ExpressionInfo)this.m_labelsAutoFitDisabled.PublishClone(context);
			}
			if (this.m_minFontSize != null)
			{
				chartAxis.m_minFontSize = (ExpressionInfo)this.m_minFontSize.PublishClone(context);
			}
			if (this.m_maxFontSize != null)
			{
				chartAxis.m_maxFontSize = (ExpressionInfo)this.m_maxFontSize.PublishClone(context);
			}
			if (this.m_offsetLabels != null)
			{
				chartAxis.m_offsetLabels = (ExpressionInfo)this.m_offsetLabels.PublishClone(context);
			}
			if (this.m_hideEndLabels != null)
			{
				chartAxis.m_hideEndLabels = (ExpressionInfo)this.m_hideEndLabels.PublishClone(context);
			}
			if (this.m_axisScaleBreak != null)
			{
				chartAxis.m_axisScaleBreak = (ChartAxisScaleBreak)this.m_axisScaleBreak.PublishClone(context);
			}
			if (this.m_variableAutoInterval != null)
			{
				chartAxis.m_variableAutoInterval = (ExpressionInfo)this.m_variableAutoInterval.PublishClone(context);
			}
			if (this.m_labelInterval != null)
			{
				chartAxis.m_labelInterval = (ExpressionInfo)this.m_hideEndLabels.PublishClone(context);
			}
			if (this.m_labelIntervalType != null)
			{
				chartAxis.m_labelIntervalType = (ExpressionInfo)this.m_hideEndLabels.PublishClone(context);
			}
			if (this.m_labelIntervalOffset != null)
			{
				chartAxis.m_labelIntervalOffset = (ExpressionInfo)this.m_labelIntervalOffset.PublishClone(context);
			}
			if (this.m_labelIntervalOffsetType != null)
			{
				chartAxis.m_labelIntervalOffsetType = (ExpressionInfo)this.m_labelIntervalOffsetType.PublishClone(context);
			}
			return chartAxis;
		}

		// Token: 0x06003A13 RID: 14867 RVA: 0x000FBA40 File Offset: 0x000F9C40
		internal new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ChartAxis, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ChartStyleContainer, new List<MemberInfo>
			{
				new MemberInfo(MemberName.Name, Token.String),
				new MemberInfo(MemberName.AxisTitle, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ChartAxisTitle),
				new MemberInfo(MemberName.MajorGridLines, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.GridLines),
				new MemberInfo(MemberName.MinorGridLines, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.GridLines),
				new MemberInfo(MemberName.CrossAt, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.Scalar, Token.Boolean),
				new MemberInfo(MemberName.Minimum, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.Maximum, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.ChartStripLines, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ChartStripLine),
				new MemberInfo(MemberName.CustomProperties, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.DataValue),
				new MemberInfo(MemberName.Visible, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.Margin, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.Interval, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.IntervalType, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.IntervalOffset, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.IntervalOffsetType, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.MajorTickMarks, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ChartTickMarks),
				new MemberInfo(MemberName.MinorTickMarks, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ChartTickMarks),
				new MemberInfo(MemberName.MarksAlwaysAtPlotEdge, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.Reverse, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.Location, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.Interlaced, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.InterlacedColor, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.LogScale, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.LogBase, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.HideLabels, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.Angle, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.Arrows, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.PreventFontShrink, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.PreventFontGrow, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.PreventLabelOffset, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.PreventWordWrap, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.AllowLabelRotation, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.IncludeZero, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.LabelsAutoFitDisabled, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.MinFontSize, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.MaxFontSize, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.OffsetLabels, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.HideEndLabels, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.AxisScaleBreak, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ChartAxisScaleBreak),
				new MemberInfo(MemberName.AutoCrossAt, Token.Boolean),
				new MemberInfo(MemberName.AutoScaleMin, Token.Boolean),
				new MemberInfo(MemberName.AutoScaleMax, Token.Boolean),
				new MemberInfo(MemberName.ExprHostID, Token.Int32),
				new MemberInfo(MemberName.VariableAutoInterval, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.LabelInterval, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.LabelIntervalType, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.LabelIntervalOffset, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.LabelIntervalOffsetType, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo)
			});
		}

		// Token: 0x06003A14 RID: 14868 RVA: 0x000FBE68 File Offset: 0x000FA068
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(ChartAxis.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName <= MemberName.ExprHostID)
				{
					if (memberName <= MemberName.AxisTitle)
					{
						if (memberName == MemberName.Name)
						{
							writer.Write(this.m_name);
							continue;
						}
						if (memberName == MemberName.AxisTitle)
						{
							writer.Write(this.m_title);
							continue;
						}
					}
					else
					{
						switch (memberName)
						{
						case MemberName.Visible:
							writer.Write(this.m_visible);
							continue;
						case MemberName.Margin:
							writer.Write(this.m_margin);
							continue;
						case MemberName.Interval:
							writer.Write(this.m_interval);
							continue;
						case MemberName.IntervalType:
							writer.Write(this.m_intervalType);
							continue;
						case MemberName.IntervalOffset:
							writer.Write(this.m_intervalOffset);
							continue;
						case MemberName.IntervalOffsetType:
							writer.Write(this.m_intervalOffsetType);
							continue;
						case MemberName.MajorTickMarks:
							writer.Write(this.m_majorTickMarks);
							continue;
						case MemberName.MinorTickMarks:
							writer.Write(this.m_minorTickMarks);
							continue;
						case MemberName.MarksNextToAxis:
						case MemberName.Series:
						case MemberName.SourceChartSeriesName:
						case MemberName.DerivedSeriesFormula:
						case MemberName.DataLabel:
						case MemberName.AxisLabel:
						case MemberName.ChartItemInLegend:
						case MemberName.LegendText:
						case MemberName.Length:
						case MemberName.CustomPaletteColors:
						case MemberName.CustomPaletteColor:
						case MemberName.Color:
						case MemberName.CodeParameters:
						case MemberName.NoDataMessage:
						case MemberName.LegendColumn:
						case MemberName.LegendColumnHeader:
						case MemberName.LegendCustomItems:
						case MemberName.LegendCustomCells:
							break;
						case MemberName.Reverse:
							writer.Write(this.m_reverse);
							continue;
						case MemberName.Location:
							writer.Write(this.m_location);
							continue;
						case MemberName.Interlaced:
							writer.Write(this.m_interlaced);
							continue;
						case MemberName.InterlacedColor:
							writer.Write(this.m_interlacedColor);
							continue;
						case MemberName.LogScale:
							writer.Write(this.m_logScale);
							continue;
						case MemberName.LogBase:
							writer.Write(this.m_logBase);
							continue;
						case MemberName.Angle:
							writer.Write(this.m_angle);
							continue;
						case MemberName.Arrows:
							writer.Write(this.m_arrows);
							continue;
						case MemberName.AllowLabelRotation:
							writer.Write(this.m_allowLabelRotation);
							continue;
						case MemberName.IncludeZero:
							writer.Write(this.m_includeZero);
							continue;
						case MemberName.MinFontSize:
							writer.Write(this.m_minFontSize);
							continue;
						case MemberName.MaxFontSize:
							writer.Write(this.m_maxFontSize);
							continue;
						case MemberName.OffsetLabels:
							writer.Write(this.m_offsetLabels);
							continue;
						case MemberName.AxisScaleBreak:
							writer.Write(this.m_axisScaleBreak);
							continue;
						case MemberName.ChartStripLines:
							writer.Write<ChartStripLine>(this.m_chartStripLines);
							continue;
						default:
							if (memberName == MemberName.ExprHostID)
							{
								writer.Write(this.m_exprHostID);
								continue;
							}
							break;
						}
					}
				}
				else if (memberName <= MemberName.Scalar)
				{
					switch (memberName)
					{
					case MemberName.MajorGridLines:
						writer.Write(this.m_majorGridLines);
						continue;
					case MemberName.MinorGridLines:
						writer.Write(this.m_minorGridLines);
						continue;
					case MemberName.MajorInterval:
					case MemberName.MinorInterval:
					case MemberName.ShowGridLines:
						break;
					case MemberName.Minimum:
						writer.Write(this.m_min);
						continue;
					case MemberName.Maximum:
						writer.Write(this.m_max);
						continue;
					case MemberName.AutoScaleMin:
						writer.Write(this.m_autoScaleMin);
						continue;
					case MemberName.AutoScaleMax:
						writer.Write(this.m_autoScaleMax);
						continue;
					case MemberName.CrossAt:
						writer.Write(this.m_crossAt);
						continue;
					case MemberName.AutoCrossAt:
						writer.Write(this.m_autoCrossAt);
						continue;
					default:
						if (memberName == MemberName.Scalar)
						{
							writer.Write(this.m_scalar);
							continue;
						}
						break;
					}
				}
				else
				{
					if (memberName == MemberName.CustomProperties)
					{
						writer.Write(this.m_customProperties);
						continue;
					}
					switch (memberName)
					{
					case MemberName.MarksAlwaysAtPlotEdge:
						writer.Write(this.m_marksAlwaysAtPlotEdge);
						continue;
					case MemberName.HideLabels:
						writer.Write(this.m_hideLabels);
						continue;
					case MemberName.PreventFontShrink:
						writer.Write(this.m_preventFontShrink);
						continue;
					case MemberName.PreventFontGrow:
						writer.Write(this.m_preventFontGrow);
						continue;
					case MemberName.PreventLabelOffset:
						writer.Write(this.m_preventLabelOffset);
						continue;
					case MemberName.PreventWordWrap:
						writer.Write(this.m_preventWordWrap);
						continue;
					case MemberName.LabelsAutoFitDisabled:
						writer.Write(this.m_labelsAutoFitDisabled);
						continue;
					case MemberName.HideEndLabels:
						writer.Write(this.m_hideEndLabels);
						continue;
					case MemberName.VariableAutoInterval:
						writer.Write(this.m_variableAutoInterval);
						continue;
					case MemberName.LabelInterval:
						writer.Write(this.m_labelInterval);
						continue;
					case MemberName.LabelIntervalType:
						writer.Write(this.m_labelIntervalType);
						continue;
					case MemberName.LabelIntervalOffset:
						writer.Write(this.m_labelIntervalOffset);
						continue;
					case MemberName.LabelIntervalOffsetType:
						writer.Write(this.m_labelIntervalOffsetType);
						continue;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x06003A15 RID: 14869 RVA: 0x000FC398 File Offset: 0x000FA598
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(ChartAxis.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName <= MemberName.ExprHostID)
				{
					if (memberName <= MemberName.AxisTitle)
					{
						if (memberName == MemberName.Name)
						{
							this.m_name = reader.ReadString();
							continue;
						}
						if (memberName == MemberName.AxisTitle)
						{
							this.m_title = (ChartAxisTitle)reader.ReadRIFObject();
							continue;
						}
					}
					else
					{
						switch (memberName)
						{
						case MemberName.Visible:
							this.m_visible = (ExpressionInfo)reader.ReadRIFObject();
							continue;
						case MemberName.Margin:
							this.m_margin = (ExpressionInfo)reader.ReadRIFObject();
							continue;
						case MemberName.Interval:
							this.m_interval = (ExpressionInfo)reader.ReadRIFObject();
							continue;
						case MemberName.IntervalType:
							this.m_intervalType = (ExpressionInfo)reader.ReadRIFObject();
							continue;
						case MemberName.IntervalOffset:
							this.m_intervalOffset = (ExpressionInfo)reader.ReadRIFObject();
							continue;
						case MemberName.IntervalOffsetType:
							this.m_intervalOffsetType = (ExpressionInfo)reader.ReadRIFObject();
							continue;
						case MemberName.MajorTickMarks:
							this.m_majorTickMarks = (ChartTickMarks)reader.ReadRIFObject();
							continue;
						case MemberName.MinorTickMarks:
							this.m_minorTickMarks = (ChartTickMarks)reader.ReadRIFObject();
							continue;
						case MemberName.MarksNextToAxis:
						case MemberName.Series:
						case MemberName.SourceChartSeriesName:
						case MemberName.DerivedSeriesFormula:
						case MemberName.DataLabel:
						case MemberName.AxisLabel:
						case MemberName.ChartItemInLegend:
						case MemberName.LegendText:
						case MemberName.Length:
						case MemberName.CustomPaletteColors:
						case MemberName.CustomPaletteColor:
						case MemberName.Color:
						case MemberName.CodeParameters:
						case MemberName.NoDataMessage:
						case MemberName.LegendColumn:
						case MemberName.LegendColumnHeader:
						case MemberName.LegendCustomItems:
						case MemberName.LegendCustomCells:
							break;
						case MemberName.Reverse:
							this.m_reverse = (ExpressionInfo)reader.ReadRIFObject();
							continue;
						case MemberName.Location:
							this.m_location = (ExpressionInfo)reader.ReadRIFObject();
							continue;
						case MemberName.Interlaced:
							this.m_interlaced = (ExpressionInfo)reader.ReadRIFObject();
							continue;
						case MemberName.InterlacedColor:
							this.m_interlacedColor = (ExpressionInfo)reader.ReadRIFObject();
							continue;
						case MemberName.LogScale:
							this.m_logScale = (ExpressionInfo)reader.ReadRIFObject();
							continue;
						case MemberName.LogBase:
							this.m_logBase = (ExpressionInfo)reader.ReadRIFObject();
							continue;
						case MemberName.Angle:
							this.m_angle = (ExpressionInfo)reader.ReadRIFObject();
							continue;
						case MemberName.Arrows:
							this.m_arrows = (ExpressionInfo)reader.ReadRIFObject();
							continue;
						case MemberName.AllowLabelRotation:
							this.m_allowLabelRotation = (ExpressionInfo)reader.ReadRIFObject();
							continue;
						case MemberName.IncludeZero:
							this.m_includeZero = (ExpressionInfo)reader.ReadRIFObject();
							continue;
						case MemberName.MinFontSize:
							this.m_minFontSize = (ExpressionInfo)reader.ReadRIFObject();
							continue;
						case MemberName.MaxFontSize:
							this.m_maxFontSize = (ExpressionInfo)reader.ReadRIFObject();
							continue;
						case MemberName.OffsetLabels:
							this.m_offsetLabels = (ExpressionInfo)reader.ReadRIFObject();
							continue;
						case MemberName.AxisScaleBreak:
							this.m_axisScaleBreak = (ChartAxisScaleBreak)reader.ReadRIFObject();
							continue;
						case MemberName.ChartStripLines:
							this.m_chartStripLines = reader.ReadGenericListOfRIFObjects<ChartStripLine>();
							continue;
						default:
							if (memberName == MemberName.ExprHostID)
							{
								this.m_exprHostID = reader.ReadInt32();
								continue;
							}
							break;
						}
					}
				}
				else if (memberName <= MemberName.Scalar)
				{
					switch (memberName)
					{
					case MemberName.MajorGridLines:
						this.m_majorGridLines = (ChartGridLines)reader.ReadRIFObject();
						continue;
					case MemberName.MinorGridLines:
						this.m_minorGridLines = (ChartGridLines)reader.ReadRIFObject();
						continue;
					case MemberName.MajorInterval:
					case MemberName.MinorInterval:
					case MemberName.ShowGridLines:
						break;
					case MemberName.Minimum:
						this.m_min = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					case MemberName.Maximum:
						this.m_max = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					case MemberName.AutoScaleMin:
						this.m_autoScaleMin = reader.ReadBoolean();
						continue;
					case MemberName.AutoScaleMax:
						this.m_autoScaleMax = reader.ReadBoolean();
						continue;
					case MemberName.CrossAt:
						this.m_crossAt = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					case MemberName.AutoCrossAt:
						this.m_autoCrossAt = reader.ReadBoolean();
						continue;
					default:
						if (memberName == MemberName.Scalar)
						{
							this.m_scalar = reader.ReadBoolean();
							continue;
						}
						break;
					}
				}
				else
				{
					if (memberName == MemberName.CustomProperties)
					{
						this.m_customProperties = reader.ReadListOfRIFObjects<DataValueList>();
						continue;
					}
					switch (memberName)
					{
					case MemberName.MarksAlwaysAtPlotEdge:
						this.m_marksAlwaysAtPlotEdge = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					case MemberName.HideLabels:
						this.m_hideLabels = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					case MemberName.PreventFontShrink:
						this.m_preventFontShrink = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					case MemberName.PreventFontGrow:
						this.m_preventFontGrow = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					case MemberName.PreventLabelOffset:
						this.m_preventLabelOffset = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					case MemberName.PreventWordWrap:
						this.m_preventWordWrap = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					case MemberName.LabelsAutoFitDisabled:
						this.m_labelsAutoFitDisabled = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					case MemberName.HideEndLabels:
						this.m_hideEndLabels = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					case MemberName.VariableAutoInterval:
						this.m_variableAutoInterval = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					case MemberName.LabelInterval:
						this.m_labelInterval = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					case MemberName.LabelIntervalType:
						this.m_labelIntervalType = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					case MemberName.LabelIntervalOffset:
						this.m_labelIntervalOffset = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					case MemberName.LabelIntervalOffsetType:
						this.m_labelIntervalOffsetType = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x06003A16 RID: 14870 RVA: 0x000FC996 File Offset: 0x000FAB96
		public override void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			base.ResolveReferences(memberReferencesCollection, referenceableItems);
		}

		// Token: 0x06003A17 RID: 14871 RVA: 0x000FC9A0 File Offset: 0x000FABA0
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ChartAxis;
		}

		// Token: 0x06003A18 RID: 14872 RVA: 0x000FC9A7 File Offset: 0x000FABA7
		internal object EvaluateCrossAt(IReportScopeInstance instance, OnDemandProcessingContext context)
		{
			if (this.m_crossAt == null)
			{
				return null;
			}
			context.SetupContext(this.m_chart, instance);
			return context.ReportRuntime.EvaluateChartAxisValueExpression(this.m_exprHost, this.m_crossAt, base.Name, "CrossAt", ChartAxis.ExpressionType.CrossAt);
		}

		// Token: 0x06003A19 RID: 14873 RVA: 0x000FC9E3 File Offset: 0x000FABE3
		internal object EvaluateMin(IReportScopeInstance instance, OnDemandProcessingContext context)
		{
			if (this.m_min == null)
			{
				return null;
			}
			context.SetupContext(this.m_chart, instance);
			return context.ReportRuntime.EvaluateChartAxisValueExpression(this.m_exprHost, this.m_min, base.Name, "Minimum", ChartAxis.ExpressionType.Min);
		}

		// Token: 0x06003A1A RID: 14874 RVA: 0x000FCA1F File Offset: 0x000FAC1F
		internal object EvaluateMax(IReportScopeInstance instance, OnDemandProcessingContext context)
		{
			if (this.m_max == null)
			{
				return null;
			}
			context.SetupContext(this.m_chart, instance);
			return context.ReportRuntime.EvaluateChartAxisValueExpression(this.m_exprHost, this.m_max, base.Name, "Maximum", ChartAxis.ExpressionType.Max);
		}

		// Token: 0x06003A1B RID: 14875 RVA: 0x000FCA5B File Offset: 0x000FAC5B
		internal ChartAxisArrow EvaluateArrows(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_chart, reportScopeInstance);
			return EnumTranslator.TranslateChartAxisArrow(context.ReportRuntime.EvaluateChartAxisArrowsExpression(this, this.m_chart.Name), context.ReportRuntime);
		}

		// Token: 0x06003A1C RID: 14876 RVA: 0x000FCA8C File Offset: 0x000FAC8C
		internal string EvaluateVisible(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_chart, reportScopeInstance);
			return context.ReportRuntime.EvaluateChartAxisVisibleExpression(this, this.m_chart.Name);
		}

		// Token: 0x06003A1D RID: 14877 RVA: 0x000FCAB2 File Offset: 0x000FACB2
		internal string EvaluateMargin(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_chart, reportScopeInstance);
			return context.ReportRuntime.EvaluateChartAxisMarginExpression(this, this.m_chart.Name);
		}

		// Token: 0x06003A1E RID: 14878 RVA: 0x000FCAD8 File Offset: 0x000FACD8
		internal double EvaluateInterval(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_chart, reportScopeInstance);
			return context.ReportRuntime.EvaluateChartAxisIntervalExpression(this, this.m_chart.Name);
		}

		// Token: 0x06003A1F RID: 14879 RVA: 0x000FCAFE File Offset: 0x000FACFE
		internal ChartIntervalType EvaluateIntervalType(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_chart, reportScopeInstance);
			return EnumTranslator.TranslateChartIntervalType(context.ReportRuntime.EvaluateChartAxisIntervalTypeExpression(this, this.m_chart.Name), context.ReportRuntime);
		}

		// Token: 0x06003A20 RID: 14880 RVA: 0x000FCB2F File Offset: 0x000FAD2F
		internal double EvaluateIntervalOffset(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_chart, reportScopeInstance);
			return context.ReportRuntime.EvaluateChartAxisIntervalOffsetExpression(this, this.m_chart.Name);
		}

		// Token: 0x06003A21 RID: 14881 RVA: 0x000FCB55 File Offset: 0x000FAD55
		internal ChartIntervalType EvaluateIntervalOffsetType(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_chart, reportScopeInstance);
			return EnumTranslator.TranslateChartIntervalType(context.ReportRuntime.EvaluateChartAxisIntervalOffsetTypeExpression(this, this.m_chart.Name), context.ReportRuntime);
		}

		// Token: 0x06003A22 RID: 14882 RVA: 0x000FCB86 File Offset: 0x000FAD86
		internal bool EvaluateMarksAlwaysAtPlotEdge(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_chart, reportScopeInstance);
			return context.ReportRuntime.EvaluateChartAxisMarksAlwaysAtPlotEdgeExpression(this, this.m_chart.Name);
		}

		// Token: 0x06003A23 RID: 14883 RVA: 0x000FCBAC File Offset: 0x000FADAC
		internal bool EvaluateReverse(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_chart, reportScopeInstance);
			return context.ReportRuntime.EvaluateChartAxisReverseExpression(this, this.m_chart.Name);
		}

		// Token: 0x06003A24 RID: 14884 RVA: 0x000FCBD2 File Offset: 0x000FADD2
		internal ChartAxisLocation EvaluateLocation(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_chart, reportScopeInstance);
			return EnumTranslator.TranslateChartAxisLocation(context.ReportRuntime.EvaluateChartAxisLocationExpression(this, this.m_chart.Name), context.ReportRuntime);
		}

		// Token: 0x06003A25 RID: 14885 RVA: 0x000FCC03 File Offset: 0x000FAE03
		internal bool EvaluateInterlaced(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_chart, reportScopeInstance);
			return context.ReportRuntime.EvaluateChartAxisInterlacedExpression(this, this.m_chart.Name);
		}

		// Token: 0x06003A26 RID: 14886 RVA: 0x000FCC29 File Offset: 0x000FAE29
		internal string EvaluateInterlacedColor(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_chart, reportScopeInstance);
			return context.ReportRuntime.EvaluateChartAxisInterlacedColorExpression(this, this.m_chart.Name);
		}

		// Token: 0x06003A27 RID: 14887 RVA: 0x000FCC4F File Offset: 0x000FAE4F
		internal bool EvaluateLogScale(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_chart, reportScopeInstance);
			return context.ReportRuntime.EvaluateChartAxisLogScaleExpression(this, this.m_chart.Name);
		}

		// Token: 0x06003A28 RID: 14888 RVA: 0x000FCC75 File Offset: 0x000FAE75
		internal double EvaluateLogBase(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_chart, reportScopeInstance);
			return context.ReportRuntime.EvaluateChartAxisLogBaseExpression(this, this.m_chart.Name);
		}

		// Token: 0x06003A29 RID: 14889 RVA: 0x000FCC9B File Offset: 0x000FAE9B
		internal bool EvaluateHideLabels(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_chart, reportScopeInstance);
			return context.ReportRuntime.EvaluateChartAxisHideLabelsExpression(this, this.m_chart.Name);
		}

		// Token: 0x06003A2A RID: 14890 RVA: 0x000FCCC1 File Offset: 0x000FAEC1
		internal double EvaluateAngle(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_chart, reportScopeInstance);
			return context.ReportRuntime.EvaluateChartAxisAngleExpression(this, this.m_chart.Name);
		}

		// Token: 0x06003A2B RID: 14891 RVA: 0x000FCCE7 File Offset: 0x000FAEE7
		internal bool EvaluatePreventFontShrink(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_chart, reportScopeInstance);
			return context.ReportRuntime.EvaluateChartAxisPreventFontShrinkExpression(this, this.m_chart.Name);
		}

		// Token: 0x06003A2C RID: 14892 RVA: 0x000FCD0D File Offset: 0x000FAF0D
		internal bool EvaluatePreventFontGrow(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_chart, reportScopeInstance);
			return context.ReportRuntime.EvaluateChartAxisPreventFontGrowExpression(this, this.m_chart.Name);
		}

		// Token: 0x06003A2D RID: 14893 RVA: 0x000FCD33 File Offset: 0x000FAF33
		internal bool EvaluatePreventLabelOffset(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_chart, reportScopeInstance);
			return context.ReportRuntime.EvaluateChartAxisPreventLabelOffsetExpression(this, this.m_chart.Name);
		}

		// Token: 0x06003A2E RID: 14894 RVA: 0x000FCD59 File Offset: 0x000FAF59
		internal bool EvaluatePreventWordWrap(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_chart, reportScopeInstance);
			return context.ReportRuntime.EvaluateChartAxisPreventWordWrapExpression(this, this.m_chart.Name);
		}

		// Token: 0x06003A2F RID: 14895 RVA: 0x000FCD7F File Offset: 0x000FAF7F
		internal ChartAxisLabelRotation EvaluateAllowLabelRotation(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_chart, reportScopeInstance);
			return EnumTranslator.TranslateChartAxisLabelRotation(context.ReportRuntime.EvaluateChartAxisAllowLabelRotationExpression(this, this.m_chart.Name), context.ReportRuntime);
		}

		// Token: 0x06003A30 RID: 14896 RVA: 0x000FCDB0 File Offset: 0x000FAFB0
		internal bool EvaluateIncludeZero(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_chart, reportScopeInstance);
			return context.ReportRuntime.EvaluateChartAxisIncludeZeroExpression(this, this.m_chart.Name);
		}

		// Token: 0x06003A31 RID: 14897 RVA: 0x000FCDD6 File Offset: 0x000FAFD6
		internal bool EvaluateLabelsAutoFitDisabled(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_chart, reportScopeInstance);
			return context.ReportRuntime.EvaluateChartAxisLabelsAutoFitDisabledExpression(this, this.m_chart.Name);
		}

		// Token: 0x06003A32 RID: 14898 RVA: 0x000FCDFC File Offset: 0x000FAFFC
		internal string EvaluateMinFontSize(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_chart, reportScopeInstance);
			return context.ReportRuntime.EvaluateChartAxisMinFontSizeExpression(this, this.m_chart.Name);
		}

		// Token: 0x06003A33 RID: 14899 RVA: 0x000FCE22 File Offset: 0x000FB022
		internal string EvaluateMaxFontSize(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_chart, reportScopeInstance);
			return context.ReportRuntime.EvaluateChartAxisMaxFontSizeExpression(this, this.m_chart.Name);
		}

		// Token: 0x06003A34 RID: 14900 RVA: 0x000FCE48 File Offset: 0x000FB048
		internal bool EvaluateOffsetLabels(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_chart, reportScopeInstance);
			return context.ReportRuntime.EvaluateChartAxisOffsetLabelsExpression(this, this.m_chart.Name);
		}

		// Token: 0x06003A35 RID: 14901 RVA: 0x000FCE6E File Offset: 0x000FB06E
		internal bool EvaluateHideEndLabels(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_chart, reportScopeInstance);
			return context.ReportRuntime.EvaluateChartAxisHideEndLabelsExpression(this, this.m_chart.Name);
		}

		// Token: 0x06003A36 RID: 14902 RVA: 0x000FCE94 File Offset: 0x000FB094
		internal bool EvaluateVariableAutoInterval(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_chart, reportScopeInstance);
			return context.ReportRuntime.EvaluateChartAxisVariableAutoIntervalExpression(this, this.m_chart.Name);
		}

		// Token: 0x06003A37 RID: 14903 RVA: 0x000FCEBA File Offset: 0x000FB0BA
		internal double EvaluateLabelInterval(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_chart, reportScopeInstance);
			return context.ReportRuntime.EvaluateChartAxisLabelIntervalExpression(this, this.m_chart.Name);
		}

		// Token: 0x06003A38 RID: 14904 RVA: 0x000FCEE0 File Offset: 0x000FB0E0
		internal ChartIntervalType EvaluateLabelIntervalType(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_chart, reportScopeInstance);
			return EnumTranslator.TranslateChartIntervalType(context.ReportRuntime.EvaluateChartAxisLabelIntervalTypeExpression(this, this.m_chart.Name), context.ReportRuntime);
		}

		// Token: 0x06003A39 RID: 14905 RVA: 0x000FCF11 File Offset: 0x000FB111
		internal double EvaluateLabelIntervalOffset(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_chart, reportScopeInstance);
			return context.ReportRuntime.EvaluateChartAxisLabelIntervalOffsetsExpression(this, this.m_chart.Name);
		}

		// Token: 0x06003A3A RID: 14906 RVA: 0x000FCF37 File Offset: 0x000FB137
		internal ChartIntervalType EvaluateLabelIntervalOffsetType(IReportScopeInstance reportScopeInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_chart, reportScopeInstance);
			return EnumTranslator.TranslateChartIntervalType(context.ReportRuntime.EvaluateChartAxisLabelIntervalOffsetTypeExpression(this, this.m_chart.Name), context.ReportRuntime);
		}

		// Token: 0x04001BA2 RID: 7074
		protected string m_name;

		// Token: 0x04001BA3 RID: 7075
		private ChartAxisTitle m_title;

		// Token: 0x04001BA4 RID: 7076
		private ChartGridLines m_majorGridLines;

		// Token: 0x04001BA5 RID: 7077
		private ChartGridLines m_minorGridLines;

		// Token: 0x04001BA6 RID: 7078
		private DataValueList m_customProperties;

		// Token: 0x04001BA7 RID: 7079
		private List<ChartStripLine> m_chartStripLines;

		// Token: 0x04001BA8 RID: 7080
		private ExpressionInfo m_visible;

		// Token: 0x04001BA9 RID: 7081
		private ExpressionInfo m_margin;

		// Token: 0x04001BAA RID: 7082
		private ExpressionInfo m_interval;

		// Token: 0x04001BAB RID: 7083
		private ExpressionInfo m_intervalType;

		// Token: 0x04001BAC RID: 7084
		private ExpressionInfo m_intervalOffset;

		// Token: 0x04001BAD RID: 7085
		private ExpressionInfo m_intervalOffsetType;

		// Token: 0x04001BAE RID: 7086
		private ChartTickMarks m_majorTickMarks;

		// Token: 0x04001BAF RID: 7087
		private ChartTickMarks m_minorTickMarks;

		// Token: 0x04001BB0 RID: 7088
		private ExpressionInfo m_marksAlwaysAtPlotEdge;

		// Token: 0x04001BB1 RID: 7089
		private ExpressionInfo m_reverse;

		// Token: 0x04001BB2 RID: 7090
		private ExpressionInfo m_location;

		// Token: 0x04001BB3 RID: 7091
		private ExpressionInfo m_interlaced;

		// Token: 0x04001BB4 RID: 7092
		private ExpressionInfo m_interlacedColor;

		// Token: 0x04001BB5 RID: 7093
		private ExpressionInfo m_logScale;

		// Token: 0x04001BB6 RID: 7094
		private ExpressionInfo m_logBase;

		// Token: 0x04001BB7 RID: 7095
		private ExpressionInfo m_hideLabels;

		// Token: 0x04001BB8 RID: 7096
		private ExpressionInfo m_angle;

		// Token: 0x04001BB9 RID: 7097
		private ExpressionInfo m_arrows;

		// Token: 0x04001BBA RID: 7098
		private ExpressionInfo m_preventFontShrink;

		// Token: 0x04001BBB RID: 7099
		private ExpressionInfo m_preventFontGrow;

		// Token: 0x04001BBC RID: 7100
		private ExpressionInfo m_preventLabelOffset;

		// Token: 0x04001BBD RID: 7101
		private ExpressionInfo m_preventWordWrap;

		// Token: 0x04001BBE RID: 7102
		private ExpressionInfo m_allowLabelRotation;

		// Token: 0x04001BBF RID: 7103
		private ExpressionInfo m_includeZero;

		// Token: 0x04001BC0 RID: 7104
		private ExpressionInfo m_labelsAutoFitDisabled;

		// Token: 0x04001BC1 RID: 7105
		private ExpressionInfo m_minFontSize;

		// Token: 0x04001BC2 RID: 7106
		private ExpressionInfo m_maxFontSize;

		// Token: 0x04001BC3 RID: 7107
		private ExpressionInfo m_offsetLabels;

		// Token: 0x04001BC4 RID: 7108
		private ExpressionInfo m_hideEndLabels;

		// Token: 0x04001BC5 RID: 7109
		private ChartAxisScaleBreak m_axisScaleBreak;

		// Token: 0x04001BC6 RID: 7110
		private ExpressionInfo m_crossAt;

		// Token: 0x04001BC7 RID: 7111
		private ExpressionInfo m_min;

		// Token: 0x04001BC8 RID: 7112
		private ExpressionInfo m_max;

		// Token: 0x04001BC9 RID: 7113
		private bool m_scalar;

		// Token: 0x04001BCA RID: 7114
		private bool m_autoCrossAt = true;

		// Token: 0x04001BCB RID: 7115
		private bool m_autoScaleMin = true;

		// Token: 0x04001BCC RID: 7116
		private bool m_autoScaleMax = true;

		// Token: 0x04001BCD RID: 7117
		private int m_exprHostID;

		// Token: 0x04001BCE RID: 7118
		private ExpressionInfo m_variableAutoInterval;

		// Token: 0x04001BCF RID: 7119
		private ExpressionInfo m_labelInterval;

		// Token: 0x04001BD0 RID: 7120
		private ExpressionInfo m_labelIntervalType;

		// Token: 0x04001BD1 RID: 7121
		private ExpressionInfo m_labelIntervalOffset;

		// Token: 0x04001BD2 RID: 7122
		private ExpressionInfo m_labelIntervalOffsetType;

		// Token: 0x04001BD3 RID: 7123
		[NonSerialized]
		private ChartAxisExprHost m_exprHost;

		// Token: 0x04001BD4 RID: 7124
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = ChartAxis.GetDeclaration();

		// Token: 0x02000970 RID: 2416
		internal enum Mode
		{
			// Token: 0x040040C5 RID: 16581
			CategoryAxis,
			// Token: 0x040040C6 RID: 16582
			ValueAxis
		}

		// Token: 0x02000971 RID: 2417
		internal enum ExpressionType
		{
			// Token: 0x040040C8 RID: 16584
			Min,
			// Token: 0x040040C9 RID: 16585
			Max,
			// Token: 0x040040CA RID: 16586
			CrossAt
		}
	}
}
