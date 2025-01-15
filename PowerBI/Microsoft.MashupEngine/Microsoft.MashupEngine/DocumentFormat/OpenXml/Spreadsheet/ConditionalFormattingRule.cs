using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002BDE RID: 11230
	[ChildElementInfo(typeof(ColorScale))]
	[ChildElementInfo(typeof(ConditionalFormattingRuleExtensionList))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(Formula))]
	[ChildElementInfo(typeof(DataBar))]
	[ChildElementInfo(typeof(IconSet))]
	internal class ConditionalFormattingRule : OpenXmlCompositeElement
	{
		// Token: 0x17007DA4 RID: 32164
		// (get) Token: 0x0601778E RID: 96142 RVA: 0x002E76D7 File Offset: 0x002E58D7
		public override string LocalName
		{
			get
			{
				return "cfRule";
			}
		}

		// Token: 0x17007DA5 RID: 32165
		// (get) Token: 0x0601778F RID: 96143 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17007DA6 RID: 32166
		// (get) Token: 0x06017790 RID: 96144 RVA: 0x003373DE File Offset: 0x003355DE
		internal override int ElementTypeId
		{
			get
			{
				return 11202;
			}
		}

		// Token: 0x06017791 RID: 96145 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17007DA7 RID: 32167
		// (get) Token: 0x06017792 RID: 96146 RVA: 0x003373E5 File Offset: 0x003355E5
		internal override string[] AttributeTagNames
		{
			get
			{
				return ConditionalFormattingRule.attributeTagNames;
			}
		}

		// Token: 0x17007DA8 RID: 32168
		// (get) Token: 0x06017793 RID: 96147 RVA: 0x003373EC File Offset: 0x003355EC
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return ConditionalFormattingRule.attributeNamespaceIds;
			}
		}

		// Token: 0x17007DA9 RID: 32169
		// (get) Token: 0x06017794 RID: 96148 RVA: 0x002E76F3 File Offset: 0x002E58F3
		// (set) Token: 0x06017795 RID: 96149 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x17007DAA RID: 32170
		// (get) Token: 0x06017796 RID: 96150 RVA: 0x002E33EC File Offset: 0x002E15EC
		// (set) Token: 0x06017797 RID: 96151 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "dxfId")]
		public UInt32Value FormatId
		{
			get
			{
				return (UInt32Value)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x17007DAB RID: 32171
		// (get) Token: 0x06017798 RID: 96152 RVA: 0x002E1683 File Offset: 0x002DF883
		// (set) Token: 0x06017799 RID: 96153 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "priority")]
		public Int32Value Priority
		{
			get
			{
				return (Int32Value)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x17007DAC RID: 32172
		// (get) Token: 0x0601779A RID: 96154 RVA: 0x002CB9D9 File Offset: 0x002C9BD9
		// (set) Token: 0x0601779B RID: 96155 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "stopIfTrue")]
		public BooleanValue StopIfTrue
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

		// Token: 0x17007DAD RID: 32173
		// (get) Token: 0x0601779C RID: 96156 RVA: 0x002CBE2D File Offset: 0x002CA02D
		// (set) Token: 0x0601779D RID: 96157 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "aboveAverage")]
		public BooleanValue AboveAverage
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

		// Token: 0x17007DAE RID: 32174
		// (get) Token: 0x0601779E RID: 96158 RVA: 0x002D7D70 File Offset: 0x002D5F70
		// (set) Token: 0x0601779F RID: 96159 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "percent")]
		public BooleanValue Percent
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

		// Token: 0x17007DAF RID: 32175
		// (get) Token: 0x060177A0 RID: 96160 RVA: 0x002CCC33 File Offset: 0x002CAE33
		// (set) Token: 0x060177A1 RID: 96161 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "bottom")]
		public BooleanValue Bottom
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

		// Token: 0x17007DB0 RID: 32176
		// (get) Token: 0x060177A2 RID: 96162 RVA: 0x003373F3 File Offset: 0x003355F3
		// (set) Token: 0x060177A3 RID: 96163 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(0, "operator")]
		public EnumValue<ConditionalFormattingOperatorValues> Operator
		{
			get
			{
				return (EnumValue<ConditionalFormattingOperatorValues>)base.Attributes[7];
			}
			set
			{
				base.Attributes[7] = value;
			}
		}

		// Token: 0x17007DB1 RID: 32177
		// (get) Token: 0x060177A4 RID: 96164 RVA: 0x002BDB16 File Offset: 0x002BBD16
		// (set) Token: 0x060177A5 RID: 96165 RVA: 0x002BD530 File Offset: 0x002BB730
		[SchemaAttr(0, "text")]
		public StringValue Text
		{
			get
			{
				return (StringValue)base.Attributes[8];
			}
			set
			{
				base.Attributes[8] = value;
			}
		}

		// Token: 0x17007DB2 RID: 32178
		// (get) Token: 0x060177A6 RID: 96166 RVA: 0x00337402 File Offset: 0x00335602
		// (set) Token: 0x060177A7 RID: 96167 RVA: 0x002BD54B File Offset: 0x002BB74B
		[SchemaAttr(0, "timePeriod")]
		public EnumValue<TimePeriodValues> TimePeriod
		{
			get
			{
				return (EnumValue<TimePeriodValues>)base.Attributes[9];
			}
			set
			{
				base.Attributes[9] = value;
			}
		}

		// Token: 0x17007DB3 RID: 32179
		// (get) Token: 0x060177A8 RID: 96168 RVA: 0x0031EC49 File Offset: 0x0031CE49
		// (set) Token: 0x060177A9 RID: 96169 RVA: 0x002BDB45 File Offset: 0x002BBD45
		[SchemaAttr(0, "rank")]
		public UInt32Value Rank
		{
			get
			{
				return (UInt32Value)base.Attributes[10];
			}
			set
			{
				base.Attributes[10] = value;
			}
		}

		// Token: 0x17007DB4 RID: 32180
		// (get) Token: 0x060177AA RID: 96170 RVA: 0x002ED56A File Offset: 0x002EB76A
		// (set) Token: 0x060177AB RID: 96171 RVA: 0x002BDB61 File Offset: 0x002BBD61
		[SchemaAttr(0, "stdDev")]
		public Int32Value StdDev
		{
			get
			{
				return (Int32Value)base.Attributes[11];
			}
			set
			{
				base.Attributes[11] = value;
			}
		}

		// Token: 0x17007DB5 RID: 32181
		// (get) Token: 0x060177AC RID: 96172 RVA: 0x002CE196 File Offset: 0x002CC396
		// (set) Token: 0x060177AD RID: 96173 RVA: 0x002BDB7D File Offset: 0x002BBD7D
		[SchemaAttr(0, "equalAverage")]
		public BooleanValue EqualAverage
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

		// Token: 0x060177AE RID: 96174 RVA: 0x00293ECF File Offset: 0x002920CF
		public ConditionalFormattingRule()
		{
		}

		// Token: 0x060177AF RID: 96175 RVA: 0x00293ED7 File Offset: 0x002920D7
		public ConditionalFormattingRule(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060177B0 RID: 96176 RVA: 0x00293EE0 File Offset: 0x002920E0
		public ConditionalFormattingRule(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060177B1 RID: 96177 RVA: 0x00293EE9 File Offset: 0x002920E9
		public ConditionalFormattingRule(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060177B2 RID: 96178 RVA: 0x00337414 File Offset: 0x00335614
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "formula" == name)
			{
				return new Formula();
			}
			if (22 == namespaceId && "colorScale" == name)
			{
				return new ColorScale();
			}
			if (22 == namespaceId && "dataBar" == name)
			{
				return new DataBar();
			}
			if (22 == namespaceId && "iconSet" == name)
			{
				return new IconSet();
			}
			if (22 == namespaceId && "extLst" == name)
			{
				return new ConditionalFormattingRuleExtensionList();
			}
			return null;
		}

		// Token: 0x060177B3 RID: 96179 RVA: 0x0033749C File Offset: 0x0033569C
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "type" == name)
			{
				return new EnumValue<ConditionalFormatValues>();
			}
			if (namespaceId == 0 && "dxfId" == name)
			{
				return new UInt32Value();
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
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060177B4 RID: 96180 RVA: 0x003375CF File Offset: 0x003357CF
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ConditionalFormattingRule>(deep);
		}

		// Token: 0x060177B5 RID: 96181 RVA: 0x003375D8 File Offset: 0x003357D8
		// Note: this type is marked as 'beforefieldinit'.
		static ConditionalFormattingRule()
		{
			byte[] array = new byte[13];
			ConditionalFormattingRule.attributeNamespaceIds = array;
		}

		// Token: 0x04009C74 RID: 40052
		private const string tagName = "cfRule";

		// Token: 0x04009C75 RID: 40053
		private const byte tagNsId = 22;

		// Token: 0x04009C76 RID: 40054
		internal const int ElementTypeIdConst = 11202;

		// Token: 0x04009C77 RID: 40055
		private static string[] attributeTagNames = new string[]
		{
			"type", "dxfId", "priority", "stopIfTrue", "aboveAverage", "percent", "bottom", "operator", "text", "timePeriod",
			"rank", "stdDev", "equalAverage"
		};

		// Token: 0x04009C78 RID: 40056
		private static byte[] attributeNamespaceIds;
	}
}
