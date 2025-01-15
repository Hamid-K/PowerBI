using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002B71 RID: 11121
	[ChildElementInfo(typeof(ExtensionList))]
	[ChildElementInfo(typeof(PivotAreaReferences))]
	[GeneratedCode("DomGen", "2.0")]
	internal class PivotArea : OpenXmlCompositeElement
	{
		// Token: 0x1700795E RID: 31070
		// (get) Token: 0x06016E7B RID: 93819 RVA: 0x002E964B File Offset: 0x002E784B
		public override string LocalName
		{
			get
			{
				return "pivotArea";
			}
		}

		// Token: 0x1700795F RID: 31071
		// (get) Token: 0x06016E7C RID: 93820 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17007960 RID: 31072
		// (get) Token: 0x06016E7D RID: 93821 RVA: 0x0033058B File Offset: 0x0032E78B
		internal override int ElementTypeId
		{
			get
			{
				return 11101;
			}
		}

		// Token: 0x06016E7E RID: 93822 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17007961 RID: 31073
		// (get) Token: 0x06016E7F RID: 93823 RVA: 0x00330592 File Offset: 0x0032E792
		internal override string[] AttributeTagNames
		{
			get
			{
				return PivotArea.attributeTagNames;
			}
		}

		// Token: 0x17007962 RID: 31074
		// (get) Token: 0x06016E80 RID: 93824 RVA: 0x00330599 File Offset: 0x0032E799
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return PivotArea.attributeNamespaceIds;
			}
		}

		// Token: 0x17007963 RID: 31075
		// (get) Token: 0x06016E81 RID: 93825 RVA: 0x002BF6A0 File Offset: 0x002BD8A0
		// (set) Token: 0x06016E82 RID: 93826 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "field")]
		public Int32Value Field
		{
			get
			{
				return (Int32Value)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17007964 RID: 31076
		// (get) Token: 0x06016E83 RID: 93827 RVA: 0x002E9667 File Offset: 0x002E7867
		// (set) Token: 0x06016E84 RID: 93828 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "type")]
		public EnumValue<PivotAreaValues> Type
		{
			get
			{
				return (EnumValue<PivotAreaValues>)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x17007965 RID: 31077
		// (get) Token: 0x06016E85 RID: 93829 RVA: 0x002C8F66 File Offset: 0x002C7166
		// (set) Token: 0x06016E86 RID: 93830 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "dataOnly")]
		public BooleanValue DataOnly
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

		// Token: 0x17007966 RID: 31078
		// (get) Token: 0x06016E87 RID: 93831 RVA: 0x002CB9D9 File Offset: 0x002C9BD9
		// (set) Token: 0x06016E88 RID: 93832 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "labelOnly")]
		public BooleanValue LabelOnly
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

		// Token: 0x17007967 RID: 31079
		// (get) Token: 0x06016E89 RID: 93833 RVA: 0x002CBE2D File Offset: 0x002CA02D
		// (set) Token: 0x06016E8A RID: 93834 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "grandRow")]
		public BooleanValue GrandRow
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

		// Token: 0x17007968 RID: 31080
		// (get) Token: 0x06016E8B RID: 93835 RVA: 0x002D7D70 File Offset: 0x002D5F70
		// (set) Token: 0x06016E8C RID: 93836 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "grandCol")]
		public BooleanValue GrandColumn
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

		// Token: 0x17007969 RID: 31081
		// (get) Token: 0x06016E8D RID: 93837 RVA: 0x002CCC33 File Offset: 0x002CAE33
		// (set) Token: 0x06016E8E RID: 93838 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "cacheIndex")]
		public BooleanValue CacheIndex
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

		// Token: 0x1700796A RID: 31082
		// (get) Token: 0x06016E8F RID: 93839 RVA: 0x002CA736 File Offset: 0x002C8936
		// (set) Token: 0x06016E90 RID: 93840 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(0, "outline")]
		public BooleanValue Outline
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

		// Token: 0x1700796B RID: 31083
		// (get) Token: 0x06016E91 RID: 93841 RVA: 0x002BDB16 File Offset: 0x002BBD16
		// (set) Token: 0x06016E92 RID: 93842 RVA: 0x002BD530 File Offset: 0x002BB730
		[SchemaAttr(0, "offset")]
		public StringValue Offset
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

		// Token: 0x1700796C RID: 31084
		// (get) Token: 0x06016E93 RID: 93843 RVA: 0x002C92EA File Offset: 0x002C74EA
		// (set) Token: 0x06016E94 RID: 93844 RVA: 0x002BD54B File Offset: 0x002BB74B
		[SchemaAttr(0, "collapsedLevelsAreSubtotals")]
		public BooleanValue CollapsedLevelsAreSubtotals
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

		// Token: 0x1700796D RID: 31085
		// (get) Token: 0x06016E95 RID: 93845 RVA: 0x002E9676 File Offset: 0x002E7876
		// (set) Token: 0x06016E96 RID: 93846 RVA: 0x002BDB45 File Offset: 0x002BBD45
		[SchemaAttr(0, "axis")]
		public EnumValue<PivotTableAxisValues> Axis
		{
			get
			{
				return (EnumValue<PivotTableAxisValues>)base.Attributes[10];
			}
			set
			{
				base.Attributes[10] = value;
			}
		}

		// Token: 0x1700796E RID: 31086
		// (get) Token: 0x06016E97 RID: 93847 RVA: 0x002E9686 File Offset: 0x002E7886
		// (set) Token: 0x06016E98 RID: 93848 RVA: 0x002BDB61 File Offset: 0x002BBD61
		[SchemaAttr(0, "fieldPosition")]
		public UInt32Value FieldPosition
		{
			get
			{
				return (UInt32Value)base.Attributes[11];
			}
			set
			{
				base.Attributes[11] = value;
			}
		}

		// Token: 0x06016E99 RID: 93849 RVA: 0x00293ECF File Offset: 0x002920CF
		public PivotArea()
		{
		}

		// Token: 0x06016E9A RID: 93850 RVA: 0x00293ED7 File Offset: 0x002920D7
		public PivotArea(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06016E9B RID: 93851 RVA: 0x00293EE0 File Offset: 0x002920E0
		public PivotArea(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06016E9C RID: 93852 RVA: 0x00293EE9 File Offset: 0x002920E9
		public PivotArea(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06016E9D RID: 93853 RVA: 0x002E9696 File Offset: 0x002E7896
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "references" == name)
			{
				return new PivotAreaReferences();
			}
			if (22 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x1700796F RID: 31087
		// (get) Token: 0x06016E9E RID: 93854 RVA: 0x003305A0 File Offset: 0x0032E7A0
		internal override string[] ElementTagNames
		{
			get
			{
				return PivotArea.eleTagNames;
			}
		}

		// Token: 0x17007970 RID: 31088
		// (get) Token: 0x06016E9F RID: 93855 RVA: 0x003305A7 File Offset: 0x0032E7A7
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return PivotArea.eleNamespaceIds;
			}
		}

		// Token: 0x17007971 RID: 31089
		// (get) Token: 0x06016EA0 RID: 93856 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17007972 RID: 31090
		// (get) Token: 0x06016EA1 RID: 93857 RVA: 0x002E96D7 File Offset: 0x002E78D7
		// (set) Token: 0x06016EA2 RID: 93858 RVA: 0x002E96E0 File Offset: 0x002E78E0
		public PivotAreaReferences PivotAreaReferences
		{
			get
			{
				return base.GetElement<PivotAreaReferences>(0);
			}
			set
			{
				base.SetElement<PivotAreaReferences>(0, value);
			}
		}

		// Token: 0x17007973 RID: 31091
		// (get) Token: 0x06016EA3 RID: 93859 RVA: 0x002E96EA File Offset: 0x002E78EA
		// (set) Token: 0x06016EA4 RID: 93860 RVA: 0x002E96F3 File Offset: 0x002E78F3
		public ExtensionList ExtensionList
		{
			get
			{
				return base.GetElement<ExtensionList>(1);
			}
			set
			{
				base.SetElement<ExtensionList>(1, value);
			}
		}

		// Token: 0x06016EA5 RID: 93861 RVA: 0x003305B0 File Offset: 0x0032E7B0
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "field" == name)
			{
				return new Int32Value();
			}
			if (namespaceId == 0 && "type" == name)
			{
				return new EnumValue<PivotAreaValues>();
			}
			if (namespaceId == 0 && "dataOnly" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "labelOnly" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "grandRow" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "grandCol" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "cacheIndex" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "outline" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "offset" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "collapsedLevelsAreSubtotals" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "axis" == name)
			{
				return new EnumValue<PivotTableAxisValues>();
			}
			if (namespaceId == 0 && "fieldPosition" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06016EA6 RID: 93862 RVA: 0x003306CD File Offset: 0x0032E8CD
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<PivotArea>(deep);
		}

		// Token: 0x06016EA7 RID: 93863 RVA: 0x003306D8 File Offset: 0x0032E8D8
		// Note: this type is marked as 'beforefieldinit'.
		static PivotArea()
		{
			byte[] array = new byte[12];
			PivotArea.attributeNamespaceIds = array;
			PivotArea.eleTagNames = new string[] { "references", "extLst" };
			PivotArea.eleNamespaceIds = new byte[] { 22, 22 };
		}

		// Token: 0x04009A6D RID: 39533
		private const string tagName = "pivotArea";

		// Token: 0x04009A6E RID: 39534
		private const byte tagNsId = 22;

		// Token: 0x04009A6F RID: 39535
		internal const int ElementTypeIdConst = 11101;

		// Token: 0x04009A70 RID: 39536
		private static string[] attributeTagNames = new string[]
		{
			"field", "type", "dataOnly", "labelOnly", "grandRow", "grandCol", "cacheIndex", "outline", "offset", "collapsedLevelsAreSubtotals",
			"axis", "fieldPosition"
		};

		// Token: 0x04009A71 RID: 39537
		private static byte[] attributeNamespaceIds;

		// Token: 0x04009A72 RID: 39538
		private static readonly string[] eleTagNames;

		// Token: 0x04009A73 RID: 39539
		private static readonly byte[] eleNamespaceIds;
	}
}
