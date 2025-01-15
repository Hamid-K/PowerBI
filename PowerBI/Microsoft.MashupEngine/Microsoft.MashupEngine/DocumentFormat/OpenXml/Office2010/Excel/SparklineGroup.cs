using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Office.Excel;

namespace DocumentFormat.OpenXml.Office2010.Excel
{
	// Token: 0x020023FB RID: 9211
	[ChildElementInfo(typeof(FirstMarkerColor), FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(Sparklines), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(SeriesColor), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(NegativeColor), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(AxisColor), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(MarkersColor), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(LastMarkerColor), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(HighMarkerColor), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(LowMarkerColor), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(Formula))]
	internal class SparklineGroup : OpenXmlCompositeElement
	{
		// Token: 0x17004E75 RID: 20085
		// (get) Token: 0x06010D71 RID: 68977 RVA: 0x002E7DA9 File Offset: 0x002E5FA9
		public override string LocalName
		{
			get
			{
				return "sparklineGroup";
			}
		}

		// Token: 0x17004E76 RID: 20086
		// (get) Token: 0x06010D72 RID: 68978 RVA: 0x002E5AC2 File Offset: 0x002E3CC2
		internal override byte NamespaceId
		{
			get
			{
				return 53;
			}
		}

		// Token: 0x17004E77 RID: 20087
		// (get) Token: 0x06010D73 RID: 68979 RVA: 0x002E7DB0 File Offset: 0x002E5FB0
		internal override int ElementTypeId
		{
			get
			{
				return 12936;
			}
		}

		// Token: 0x06010D74 RID: 68980 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x17004E78 RID: 20088
		// (get) Token: 0x06010D75 RID: 68981 RVA: 0x002E7DB7 File Offset: 0x002E5FB7
		internal override string[] AttributeTagNames
		{
			get
			{
				return SparklineGroup.attributeTagNames;
			}
		}

		// Token: 0x17004E79 RID: 20089
		// (get) Token: 0x06010D76 RID: 68982 RVA: 0x002E7DBE File Offset: 0x002E5FBE
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return SparklineGroup.attributeNamespaceIds;
			}
		}

		// Token: 0x17004E7A RID: 20090
		// (get) Token: 0x06010D77 RID: 68983 RVA: 0x002E7DC5 File Offset: 0x002E5FC5
		// (set) Token: 0x06010D78 RID: 68984 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "manualMax")]
		public DoubleValue ManualMax
		{
			get
			{
				return (DoubleValue)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17004E7B RID: 20091
		// (get) Token: 0x06010D79 RID: 68985 RVA: 0x002E7DD4 File Offset: 0x002E5FD4
		// (set) Token: 0x06010D7A RID: 68986 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "manualMin")]
		public DoubleValue ManualMin
		{
			get
			{
				return (DoubleValue)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x17004E7C RID: 20092
		// (get) Token: 0x06010D7B RID: 68987 RVA: 0x002E7DE3 File Offset: 0x002E5FE3
		// (set) Token: 0x06010D7C RID: 68988 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "lineWeight")]
		public DoubleValue LineWeight
		{
			get
			{
				return (DoubleValue)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x17004E7D RID: 20093
		// (get) Token: 0x06010D7D RID: 68989 RVA: 0x002E7DF2 File Offset: 0x002E5FF2
		// (set) Token: 0x06010D7E RID: 68990 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "type")]
		public EnumValue<SparklineTypeValues> Type
		{
			get
			{
				return (EnumValue<SparklineTypeValues>)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x17004E7E RID: 20094
		// (get) Token: 0x06010D7F RID: 68991 RVA: 0x002CBE2D File Offset: 0x002CA02D
		// (set) Token: 0x06010D80 RID: 68992 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "dateAxis")]
		public BooleanValue DateAxis
		{
			get
			{
				return (BooleanValue)base.Attributes[4];
			}
			set
			{
				base.Attributes[4] = value;
			}
		}

		// Token: 0x17004E7F RID: 20095
		// (get) Token: 0x06010D81 RID: 68993 RVA: 0x002E7E01 File Offset: 0x002E6001
		// (set) Token: 0x06010D82 RID: 68994 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "displayEmptyCellsAs")]
		public EnumValue<DisplayBlanksAsValues> DisplayEmptyCellsAs
		{
			get
			{
				return (EnumValue<DisplayBlanksAsValues>)base.Attributes[5];
			}
			set
			{
				base.Attributes[5] = value;
			}
		}

		// Token: 0x17004E80 RID: 20096
		// (get) Token: 0x06010D83 RID: 68995 RVA: 0x002CCC33 File Offset: 0x002CAE33
		// (set) Token: 0x06010D84 RID: 68996 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "markers")]
		public BooleanValue Markers
		{
			get
			{
				return (BooleanValue)base.Attributes[6];
			}
			set
			{
				base.Attributes[6] = value;
			}
		}

		// Token: 0x17004E81 RID: 20097
		// (get) Token: 0x06010D85 RID: 68997 RVA: 0x002CA736 File Offset: 0x002C8936
		// (set) Token: 0x06010D86 RID: 68998 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(0, "high")]
		public BooleanValue High
		{
			get
			{
				return (BooleanValue)base.Attributes[7];
			}
			set
			{
				base.Attributes[7] = value;
			}
		}

		// Token: 0x17004E82 RID: 20098
		// (get) Token: 0x06010D87 RID: 68999 RVA: 0x002CB706 File Offset: 0x002C9906
		// (set) Token: 0x06010D88 RID: 69000 RVA: 0x002BD530 File Offset: 0x002BB730
		[SchemaAttr(0, "low")]
		public BooleanValue Low
		{
			get
			{
				return (BooleanValue)base.Attributes[8];
			}
			set
			{
				base.Attributes[8] = value;
			}
		}

		// Token: 0x17004E83 RID: 20099
		// (get) Token: 0x06010D89 RID: 69001 RVA: 0x002C92EA File Offset: 0x002C74EA
		// (set) Token: 0x06010D8A RID: 69002 RVA: 0x002BD54B File Offset: 0x002BB74B
		[SchemaAttr(0, "first")]
		public BooleanValue First
		{
			get
			{
				return (BooleanValue)base.Attributes[9];
			}
			set
			{
				base.Attributes[9] = value;
			}
		}

		// Token: 0x17004E84 RID: 20100
		// (get) Token: 0x06010D8B RID: 69003 RVA: 0x002C8762 File Offset: 0x002C6962
		// (set) Token: 0x06010D8C RID: 69004 RVA: 0x002BDB45 File Offset: 0x002BBD45
		[SchemaAttr(0, "last")]
		public BooleanValue Last
		{
			get
			{
				return (BooleanValue)base.Attributes[10];
			}
			set
			{
				base.Attributes[10] = value;
			}
		}

		// Token: 0x17004E85 RID: 20101
		// (get) Token: 0x06010D8D RID: 69005 RVA: 0x002C92FA File Offset: 0x002C74FA
		// (set) Token: 0x06010D8E RID: 69006 RVA: 0x002BDB61 File Offset: 0x002BBD61
		[SchemaAttr(0, "negative")]
		public BooleanValue Negative
		{
			get
			{
				return (BooleanValue)base.Attributes[11];
			}
			set
			{
				base.Attributes[11] = value;
			}
		}

		// Token: 0x17004E86 RID: 20102
		// (get) Token: 0x06010D8F RID: 69007 RVA: 0x002CE196 File Offset: 0x002CC396
		// (set) Token: 0x06010D90 RID: 69008 RVA: 0x002BDB7D File Offset: 0x002BBD7D
		[SchemaAttr(0, "displayXAxis")]
		public BooleanValue DisplayXAxis
		{
			get
			{
				return (BooleanValue)base.Attributes[12];
			}
			set
			{
				base.Attributes[12] = value;
			}
		}

		// Token: 0x17004E87 RID: 20103
		// (get) Token: 0x06010D91 RID: 69009 RVA: 0x002CD15F File Offset: 0x002CB35F
		// (set) Token: 0x06010D92 RID: 69010 RVA: 0x002BE209 File Offset: 0x002BC409
		[SchemaAttr(0, "displayHidden")]
		public BooleanValue DisplayHidden
		{
			get
			{
				return (BooleanValue)base.Attributes[13];
			}
			set
			{
				base.Attributes[13] = value;
			}
		}

		// Token: 0x17004E88 RID: 20104
		// (get) Token: 0x06010D93 RID: 69011 RVA: 0x002E7E10 File Offset: 0x002E6010
		// (set) Token: 0x06010D94 RID: 69012 RVA: 0x002BE225 File Offset: 0x002BC425
		[SchemaAttr(0, "minAxisType")]
		public EnumValue<SparklineAxisMinMaxValues> MinAxisType
		{
			get
			{
				return (EnumValue<SparklineAxisMinMaxValues>)base.Attributes[14];
			}
			set
			{
				base.Attributes[14] = value;
			}
		}

		// Token: 0x17004E89 RID: 20105
		// (get) Token: 0x06010D95 RID: 69013 RVA: 0x002E7E20 File Offset: 0x002E6020
		// (set) Token: 0x06010D96 RID: 69014 RVA: 0x002BE241 File Offset: 0x002BC441
		[SchemaAttr(0, "maxAxisType")]
		public EnumValue<SparklineAxisMinMaxValues> MaxAxisType
		{
			get
			{
				return (EnumValue<SparklineAxisMinMaxValues>)base.Attributes[15];
			}
			set
			{
				base.Attributes[15] = value;
			}
		}

		// Token: 0x17004E8A RID: 20106
		// (get) Token: 0x06010D97 RID: 69015 RVA: 0x002C930A File Offset: 0x002C750A
		// (set) Token: 0x06010D98 RID: 69016 RVA: 0x002BE25D File Offset: 0x002BC45D
		[SchemaAttr(0, "rightToLeft")]
		public BooleanValue RightToLeft
		{
			get
			{
				return (BooleanValue)base.Attributes[16];
			}
			set
			{
				base.Attributes[16] = value;
			}
		}

		// Token: 0x06010D99 RID: 69017 RVA: 0x00293ECF File Offset: 0x002920CF
		public SparklineGroup()
		{
		}

		// Token: 0x06010D9A RID: 69018 RVA: 0x00293ED7 File Offset: 0x002920D7
		public SparklineGroup(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06010D9B RID: 69019 RVA: 0x00293EE0 File Offset: 0x002920E0
		public SparklineGroup(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06010D9C RID: 69020 RVA: 0x00293EE9 File Offset: 0x002920E9
		public SparklineGroup(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06010D9D RID: 69021 RVA: 0x002E7E30 File Offset: 0x002E6030
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (53 == namespaceId && "colorSeries" == name)
			{
				return new SeriesColor();
			}
			if (53 == namespaceId && "colorNegative" == name)
			{
				return new NegativeColor();
			}
			if (53 == namespaceId && "colorAxis" == name)
			{
				return new AxisColor();
			}
			if (53 == namespaceId && "colorMarkers" == name)
			{
				return new MarkersColor();
			}
			if (53 == namespaceId && "colorFirst" == name)
			{
				return new FirstMarkerColor();
			}
			if (53 == namespaceId && "colorLast" == name)
			{
				return new LastMarkerColor();
			}
			if (53 == namespaceId && "colorHigh" == name)
			{
				return new HighMarkerColor();
			}
			if (53 == namespaceId && "colorLow" == name)
			{
				return new LowMarkerColor();
			}
			if (32 == namespaceId && "f" == name)
			{
				return new Formula();
			}
			if (53 == namespaceId && "sparklines" == name)
			{
				return new Sparklines();
			}
			return null;
		}

		// Token: 0x17004E8B RID: 20107
		// (get) Token: 0x06010D9E RID: 69022 RVA: 0x002E7F2E File Offset: 0x002E612E
		internal override string[] ElementTagNames
		{
			get
			{
				return SparklineGroup.eleTagNames;
			}
		}

		// Token: 0x17004E8C RID: 20108
		// (get) Token: 0x06010D9F RID: 69023 RVA: 0x002E7F35 File Offset: 0x002E6135
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return SparklineGroup.eleNamespaceIds;
			}
		}

		// Token: 0x17004E8D RID: 20109
		// (get) Token: 0x06010DA0 RID: 69024 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17004E8E RID: 20110
		// (get) Token: 0x06010DA1 RID: 69025 RVA: 0x002E7F3C File Offset: 0x002E613C
		// (set) Token: 0x06010DA2 RID: 69026 RVA: 0x002E7F45 File Offset: 0x002E6145
		public SeriesColor SeriesColor
		{
			get
			{
				return base.GetElement<SeriesColor>(0);
			}
			set
			{
				base.SetElement<SeriesColor>(0, value);
			}
		}

		// Token: 0x17004E8F RID: 20111
		// (get) Token: 0x06010DA3 RID: 69027 RVA: 0x002E7F4F File Offset: 0x002E614F
		// (set) Token: 0x06010DA4 RID: 69028 RVA: 0x002E7F58 File Offset: 0x002E6158
		public NegativeColor NegativeColor
		{
			get
			{
				return base.GetElement<NegativeColor>(1);
			}
			set
			{
				base.SetElement<NegativeColor>(1, value);
			}
		}

		// Token: 0x17004E90 RID: 20112
		// (get) Token: 0x06010DA5 RID: 69029 RVA: 0x002E7F62 File Offset: 0x002E6162
		// (set) Token: 0x06010DA6 RID: 69030 RVA: 0x002E7F6B File Offset: 0x002E616B
		public AxisColor AxisColor
		{
			get
			{
				return base.GetElement<AxisColor>(2);
			}
			set
			{
				base.SetElement<AxisColor>(2, value);
			}
		}

		// Token: 0x17004E91 RID: 20113
		// (get) Token: 0x06010DA7 RID: 69031 RVA: 0x002E7F75 File Offset: 0x002E6175
		// (set) Token: 0x06010DA8 RID: 69032 RVA: 0x002E7F7E File Offset: 0x002E617E
		public MarkersColor MarkersColor
		{
			get
			{
				return base.GetElement<MarkersColor>(3);
			}
			set
			{
				base.SetElement<MarkersColor>(3, value);
			}
		}

		// Token: 0x17004E92 RID: 20114
		// (get) Token: 0x06010DA9 RID: 69033 RVA: 0x002E7F88 File Offset: 0x002E6188
		// (set) Token: 0x06010DAA RID: 69034 RVA: 0x002E7F91 File Offset: 0x002E6191
		public FirstMarkerColor FirstMarkerColor
		{
			get
			{
				return base.GetElement<FirstMarkerColor>(4);
			}
			set
			{
				base.SetElement<FirstMarkerColor>(4, value);
			}
		}

		// Token: 0x17004E93 RID: 20115
		// (get) Token: 0x06010DAB RID: 69035 RVA: 0x002E7F9B File Offset: 0x002E619B
		// (set) Token: 0x06010DAC RID: 69036 RVA: 0x002E7FA4 File Offset: 0x002E61A4
		public LastMarkerColor LastMarkerColor
		{
			get
			{
				return base.GetElement<LastMarkerColor>(5);
			}
			set
			{
				base.SetElement<LastMarkerColor>(5, value);
			}
		}

		// Token: 0x17004E94 RID: 20116
		// (get) Token: 0x06010DAD RID: 69037 RVA: 0x002E7FAE File Offset: 0x002E61AE
		// (set) Token: 0x06010DAE RID: 69038 RVA: 0x002E7FB7 File Offset: 0x002E61B7
		public HighMarkerColor HighMarkerColor
		{
			get
			{
				return base.GetElement<HighMarkerColor>(6);
			}
			set
			{
				base.SetElement<HighMarkerColor>(6, value);
			}
		}

		// Token: 0x17004E95 RID: 20117
		// (get) Token: 0x06010DAF RID: 69039 RVA: 0x002E7FC1 File Offset: 0x002E61C1
		// (set) Token: 0x06010DB0 RID: 69040 RVA: 0x002E7FCA File Offset: 0x002E61CA
		public LowMarkerColor LowMarkerColor
		{
			get
			{
				return base.GetElement<LowMarkerColor>(7);
			}
			set
			{
				base.SetElement<LowMarkerColor>(7, value);
			}
		}

		// Token: 0x17004E96 RID: 20118
		// (get) Token: 0x06010DB1 RID: 69041 RVA: 0x002E7FD4 File Offset: 0x002E61D4
		// (set) Token: 0x06010DB2 RID: 69042 RVA: 0x002E7FDD File Offset: 0x002E61DD
		public Formula Formula
		{
			get
			{
				return base.GetElement<Formula>(8);
			}
			set
			{
				base.SetElement<Formula>(8, value);
			}
		}

		// Token: 0x17004E97 RID: 20119
		// (get) Token: 0x06010DB3 RID: 69043 RVA: 0x002E7FE7 File Offset: 0x002E61E7
		// (set) Token: 0x06010DB4 RID: 69044 RVA: 0x002E7FF1 File Offset: 0x002E61F1
		public Sparklines Sparklines
		{
			get
			{
				return base.GetElement<Sparklines>(9);
			}
			set
			{
				base.SetElement<Sparklines>(9, value);
			}
		}

		// Token: 0x06010DB5 RID: 69045 RVA: 0x002E7FFC File Offset: 0x002E61FC
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "manualMax" == name)
			{
				return new DoubleValue();
			}
			if (namespaceId == 0 && "manualMin" == name)
			{
				return new DoubleValue();
			}
			if (namespaceId == 0 && "lineWeight" == name)
			{
				return new DoubleValue();
			}
			if (namespaceId == 0 && "type" == name)
			{
				return new EnumValue<SparklineTypeValues>();
			}
			if (namespaceId == 0 && "dateAxis" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "displayEmptyCellsAs" == name)
			{
				return new EnumValue<DisplayBlanksAsValues>();
			}
			if (namespaceId == 0 && "markers" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "high" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "low" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "first" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "last" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "negative" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "displayXAxis" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "displayHidden" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "minAxisType" == name)
			{
				return new EnumValue<SparklineAxisMinMaxValues>();
			}
			if (namespaceId == 0 && "maxAxisType" == name)
			{
				return new EnumValue<SparklineAxisMinMaxValues>();
			}
			if (namespaceId == 0 && "rightToLeft" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06010DB6 RID: 69046 RVA: 0x002E8187 File Offset: 0x002E6387
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SparklineGroup>(deep);
		}

		// Token: 0x06010DB7 RID: 69047 RVA: 0x002E8190 File Offset: 0x002E6390
		// Note: this type is marked as 'beforefieldinit'.
		static SparklineGroup()
		{
			byte[] array = new byte[17];
			SparklineGroup.attributeNamespaceIds = array;
			SparklineGroup.eleTagNames = new string[] { "colorSeries", "colorNegative", "colorAxis", "colorMarkers", "colorFirst", "colorLast", "colorHigh", "colorLow", "f", "sparklines" };
			SparklineGroup.eleNamespaceIds = new byte[] { 53, 53, 53, 53, 53, 53, 53, 53, 32, 53 };
		}

		// Token: 0x0400767D RID: 30333
		private const string tagName = "sparklineGroup";

		// Token: 0x0400767E RID: 30334
		private const byte tagNsId = 53;

		// Token: 0x0400767F RID: 30335
		internal const int ElementTypeIdConst = 12936;

		// Token: 0x04007680 RID: 30336
		private static string[] attributeTagNames = new string[]
		{
			"manualMax", "manualMin", "lineWeight", "type", "dateAxis", "displayEmptyCellsAs", "markers", "high", "low", "first",
			"last", "negative", "displayXAxis", "displayHidden", "minAxisType", "maxAxisType", "rightToLeft"
		};

		// Token: 0x04007681 RID: 30337
		private static byte[] attributeNamespaceIds;

		// Token: 0x04007682 RID: 30338
		private static readonly string[] eleTagNames;

		// Token: 0x04007683 RID: 30339
		private static readonly byte[] eleNamespaceIds;
	}
}
