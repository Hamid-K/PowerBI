using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Office.Excel;
using DocumentFormat.OpenXml.Spreadsheet;

namespace DocumentFormat.OpenXml.Office2010.Excel
{
	// Token: 0x020023F5 RID: 9205
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(DocumentFormat.OpenXml.Office.Excel.Formula))]
	[ChildElementInfo(typeof(ColorScale), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(DataBar), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(IconSet), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(DifferentialType), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(ExtensionList), FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	internal class ConditionalFormattingRule : OpenXmlCompositeElement
	{
		// Token: 0x17004E3E RID: 20030
		// (get) Token: 0x06010CF1 RID: 68849 RVA: 0x002E76D7 File Offset: 0x002E58D7
		public override string LocalName
		{
			get
			{
				return "cfRule";
			}
		}

		// Token: 0x17004E3F RID: 20031
		// (get) Token: 0x06010CF2 RID: 68850 RVA: 0x002E5AC2 File Offset: 0x002E3CC2
		internal override byte NamespaceId
		{
			get
			{
				return 53;
			}
		}

		// Token: 0x17004E40 RID: 20032
		// (get) Token: 0x06010CF3 RID: 68851 RVA: 0x002E76DE File Offset: 0x002E58DE
		internal override int ElementTypeId
		{
			get
			{
				return 12931;
			}
		}

		// Token: 0x06010CF4 RID: 68852 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x17004E41 RID: 20033
		// (get) Token: 0x06010CF5 RID: 68853 RVA: 0x002E76E5 File Offset: 0x002E58E5
		internal override string[] AttributeTagNames
		{
			get
			{
				return ConditionalFormattingRule.attributeTagNames;
			}
		}

		// Token: 0x17004E42 RID: 20034
		// (get) Token: 0x06010CF6 RID: 68854 RVA: 0x002E76EC File Offset: 0x002E58EC
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return ConditionalFormattingRule.attributeNamespaceIds;
			}
		}

		// Token: 0x17004E43 RID: 20035
		// (get) Token: 0x06010CF7 RID: 68855 RVA: 0x002E76F3 File Offset: 0x002E58F3
		// (set) Token: 0x06010CF8 RID: 68856 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "type")]
		public EnumValue<ConditionalFormatValues> Type
		{
			get
			{
				return (EnumValue<ConditionalFormatValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17004E44 RID: 20036
		// (get) Token: 0x06010CF9 RID: 68857 RVA: 0x002BF6AF File Offset: 0x002BD8AF
		// (set) Token: 0x06010CFA RID: 68858 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "priority")]
		public Int32Value Priority
		{
			get
			{
				return (Int32Value)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x17004E45 RID: 20037
		// (get) Token: 0x06010CFB RID: 68859 RVA: 0x002C8F66 File Offset: 0x002C7166
		// (set) Token: 0x06010CFC RID: 68860 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "stopIfTrue")]
		public BooleanValue StopIfTrue
		{
			get
			{
				return (BooleanValue)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x17004E46 RID: 20038
		// (get) Token: 0x06010CFD RID: 68861 RVA: 0x002CB9D9 File Offset: 0x002C9BD9
		// (set) Token: 0x06010CFE RID: 68862 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "aboveAverage")]
		public BooleanValue AboveAverage
		{
			get
			{
				return (BooleanValue)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x17004E47 RID: 20039
		// (get) Token: 0x06010CFF RID: 68863 RVA: 0x002CBE2D File Offset: 0x002CA02D
		// (set) Token: 0x06010D00 RID: 68864 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "percent")]
		public BooleanValue Percent
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

		// Token: 0x17004E48 RID: 20040
		// (get) Token: 0x06010D01 RID: 68865 RVA: 0x002D7D70 File Offset: 0x002D5F70
		// (set) Token: 0x06010D02 RID: 68866 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "bottom")]
		public BooleanValue Bottom
		{
			get
			{
				return (BooleanValue)base.Attributes[5];
			}
			set
			{
				base.Attributes[5] = value;
			}
		}

		// Token: 0x17004E49 RID: 20041
		// (get) Token: 0x06010D03 RID: 68867 RVA: 0x002E7702 File Offset: 0x002E5902
		// (set) Token: 0x06010D04 RID: 68868 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "operator")]
		public EnumValue<ConditionalFormattingOperatorValues> Operator
		{
			get
			{
				return (EnumValue<ConditionalFormattingOperatorValues>)base.Attributes[6];
			}
			set
			{
				base.Attributes[6] = value;
			}
		}

		// Token: 0x17004E4A RID: 20042
		// (get) Token: 0x06010D05 RID: 68869 RVA: 0x002BDB07 File Offset: 0x002BBD07
		// (set) Token: 0x06010D06 RID: 68870 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(0, "text")]
		public StringValue Text
		{
			get
			{
				return (StringValue)base.Attributes[7];
			}
			set
			{
				base.Attributes[7] = value;
			}
		}

		// Token: 0x17004E4B RID: 20043
		// (get) Token: 0x06010D07 RID: 68871 RVA: 0x002E7711 File Offset: 0x002E5911
		// (set) Token: 0x06010D08 RID: 68872 RVA: 0x002BD530 File Offset: 0x002BB730
		[SchemaAttr(0, "timePeriod")]
		public EnumValue<TimePeriodValues> TimePeriod
		{
			get
			{
				return (EnumValue<TimePeriodValues>)base.Attributes[8];
			}
			set
			{
				base.Attributes[8] = value;
			}
		}

		// Token: 0x17004E4C RID: 20044
		// (get) Token: 0x06010D09 RID: 68873 RVA: 0x002E7720 File Offset: 0x002E5920
		// (set) Token: 0x06010D0A RID: 68874 RVA: 0x002BD54B File Offset: 0x002BB74B
		[SchemaAttr(0, "rank")]
		public UInt32Value Rank
		{
			get
			{
				return (UInt32Value)base.Attributes[9];
			}
			set
			{
				base.Attributes[9] = value;
			}
		}

		// Token: 0x17004E4D RID: 20045
		// (get) Token: 0x06010D0B RID: 68875 RVA: 0x002E7730 File Offset: 0x002E5930
		// (set) Token: 0x06010D0C RID: 68876 RVA: 0x002BDB45 File Offset: 0x002BBD45
		[SchemaAttr(0, "stdDev")]
		public Int32Value StandardDeviation
		{
			get
			{
				return (Int32Value)base.Attributes[10];
			}
			set
			{
				base.Attributes[10] = value;
			}
		}

		// Token: 0x17004E4E RID: 20046
		// (get) Token: 0x06010D0D RID: 68877 RVA: 0x002C92FA File Offset: 0x002C74FA
		// (set) Token: 0x06010D0E RID: 68878 RVA: 0x002BDB61 File Offset: 0x002BBD61
		[SchemaAttr(0, "equalAverage")]
		public BooleanValue EqualAverage
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

		// Token: 0x17004E4F RID: 20047
		// (get) Token: 0x06010D0F RID: 68879 RVA: 0x002CE196 File Offset: 0x002CC396
		// (set) Token: 0x06010D10 RID: 68880 RVA: 0x002BDB7D File Offset: 0x002BBD7D
		[SchemaAttr(0, "activePresent")]
		public BooleanValue ActivePresent
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

		// Token: 0x17004E50 RID: 20048
		// (get) Token: 0x06010D11 RID: 68881 RVA: 0x002BEF1F File Offset: 0x002BD11F
		// (set) Token: 0x06010D12 RID: 68882 RVA: 0x002BE209 File Offset: 0x002BC409
		[SchemaAttr(0, "id")]
		public StringValue Id
		{
			get
			{
				return (StringValue)base.Attributes[13];
			}
			set
			{
				base.Attributes[13] = value;
			}
		}

		// Token: 0x06010D13 RID: 68883 RVA: 0x00293ECF File Offset: 0x002920CF
		public ConditionalFormattingRule()
		{
		}

		// Token: 0x06010D14 RID: 68884 RVA: 0x00293ED7 File Offset: 0x002920D7
		public ConditionalFormattingRule(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06010D15 RID: 68885 RVA: 0x00293EE0 File Offset: 0x002920E0
		public ConditionalFormattingRule(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06010D16 RID: 68886 RVA: 0x00293EE9 File Offset: 0x002920E9
		public ConditionalFormattingRule(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06010D17 RID: 68887 RVA: 0x002E7740 File Offset: 0x002E5940
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (32 == namespaceId && "f" == name)
			{
				return new DocumentFormat.OpenXml.Office.Excel.Formula();
			}
			if (53 == namespaceId && "colorScale" == name)
			{
				return new ColorScale();
			}
			if (53 == namespaceId && "dataBar" == name)
			{
				return new DataBar();
			}
			if (53 == namespaceId && "iconSet" == name)
			{
				return new IconSet();
			}
			if (53 == namespaceId && "dxf" == name)
			{
				return new DifferentialType();
			}
			if (53 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x06010D18 RID: 68888 RVA: 0x002E77E0 File Offset: 0x002E59E0
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "type" == name)
			{
				return new EnumValue<ConditionalFormatValues>();
			}
			if (namespaceId == 0 && "priority" == name)
			{
				return new Int32Value();
			}
			if (namespaceId == 0 && "stopIfTrue" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "aboveAverage" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "percent" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "bottom" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "operator" == name)
			{
				return new EnumValue<ConditionalFormattingOperatorValues>();
			}
			if (namespaceId == 0 && "text" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "timePeriod" == name)
			{
				return new EnumValue<TimePeriodValues>();
			}
			if (namespaceId == 0 && "rank" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "stdDev" == name)
			{
				return new Int32Value();
			}
			if (namespaceId == 0 && "equalAverage" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "activePresent" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "id" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06010D19 RID: 68889 RVA: 0x002E7929 File Offset: 0x002E5B29
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ConditionalFormattingRule>(deep);
		}

		// Token: 0x06010D1A RID: 68890 RVA: 0x002E7934 File Offset: 0x002E5B34
		// Note: this type is marked as 'beforefieldinit'.
		static ConditionalFormattingRule()
		{
			byte[] array = new byte[14];
			ConditionalFormattingRule.attributeNamespaceIds = array;
		}

		// Token: 0x04007666 RID: 30310
		private const string tagName = "cfRule";

		// Token: 0x04007667 RID: 30311
		private const byte tagNsId = 53;

		// Token: 0x04007668 RID: 30312
		internal const int ElementTypeIdConst = 12931;

		// Token: 0x04007669 RID: 30313
		private static string[] attributeTagNames = new string[]
		{
			"type", "priority", "stopIfTrue", "aboveAverage", "percent", "bottom", "operator", "text", "timePeriod", "rank",
			"stdDev", "equalAverage", "activePresent", "id"
		};

		// Token: 0x0400766A RID: 30314
		private static byte[] attributeNamespaceIds;
	}
}
