using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002BD2 RID: 11218
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(PivotArea))]
	internal class PivotSelection : OpenXmlCompositeElement
	{
		// Token: 0x17007D1F RID: 32031
		// (get) Token: 0x06017676 RID: 95862 RVA: 0x003365FF File Offset: 0x003347FF
		public override string LocalName
		{
			get
			{
				return "pivotSelection";
			}
		}

		// Token: 0x17007D20 RID: 32032
		// (get) Token: 0x06017677 RID: 95863 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17007D21 RID: 32033
		// (get) Token: 0x06017678 RID: 95864 RVA: 0x00336606 File Offset: 0x00334806
		internal override int ElementTypeId
		{
			get
			{
				return 11191;
			}
		}

		// Token: 0x06017679 RID: 95865 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17007D22 RID: 32034
		// (get) Token: 0x0601767A RID: 95866 RVA: 0x0033660D File Offset: 0x0033480D
		internal override string[] AttributeTagNames
		{
			get
			{
				return PivotSelection.attributeTagNames;
			}
		}

		// Token: 0x17007D23 RID: 32035
		// (get) Token: 0x0601767B RID: 95867 RVA: 0x00336614 File Offset: 0x00334814
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return PivotSelection.attributeNamespaceIds;
			}
		}

		// Token: 0x17007D24 RID: 32036
		// (get) Token: 0x0601767C RID: 95868 RVA: 0x00336530 File Offset: 0x00334730
		// (set) Token: 0x0601767D RID: 95869 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "pane")]
		public EnumValue<PaneValues> Pane
		{
			get
			{
				return (EnumValue<PaneValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17007D25 RID: 32037
		// (get) Token: 0x0601767E RID: 95870 RVA: 0x002C8B36 File Offset: 0x002C6D36
		// (set) Token: 0x0601767F RID: 95871 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "showHeader")]
		public BooleanValue ShowHeader
		{
			get
			{
				return (BooleanValue)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x17007D26 RID: 32038
		// (get) Token: 0x06017680 RID: 95872 RVA: 0x002C8F66 File Offset: 0x002C7166
		// (set) Token: 0x06017681 RID: 95873 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "label")]
		public BooleanValue Label
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

		// Token: 0x17007D27 RID: 32039
		// (get) Token: 0x06017682 RID: 95874 RVA: 0x002CB9D9 File Offset: 0x002C9BD9
		// (set) Token: 0x06017683 RID: 95875 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "data")]
		public BooleanValue Data
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

		// Token: 0x17007D28 RID: 32040
		// (get) Token: 0x06017684 RID: 95876 RVA: 0x002CBE2D File Offset: 0x002CA02D
		// (set) Token: 0x06017685 RID: 95877 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "extendable")]
		public BooleanValue Extendable
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

		// Token: 0x17007D29 RID: 32041
		// (get) Token: 0x06017686 RID: 95878 RVA: 0x002E6EEB File Offset: 0x002E50EB
		// (set) Token: 0x06017687 RID: 95879 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "count")]
		public UInt32Value Count
		{
			get
			{
				return (UInt32Value)base.Attributes[5];
			}
			set
			{
				base.Attributes[5] = value;
			}
		}

		// Token: 0x17007D2A RID: 32042
		// (get) Token: 0x06017688 RID: 95880 RVA: 0x0033661B File Offset: 0x0033481B
		// (set) Token: 0x06017689 RID: 95881 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "axis")]
		public EnumValue<PivotTableAxisValues> Axis
		{
			get
			{
				return (EnumValue<PivotTableAxisValues>)base.Attributes[6];
			}
			set
			{
				base.Attributes[6] = value;
			}
		}

		// Token: 0x17007D2B RID: 32043
		// (get) Token: 0x0601768A RID: 95882 RVA: 0x0032B268 File Offset: 0x00329468
		// (set) Token: 0x0601768B RID: 95883 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(0, "dimension")]
		public UInt32Value Dimension
		{
			get
			{
				return (UInt32Value)base.Attributes[7];
			}
			set
			{
				base.Attributes[7] = value;
			}
		}

		// Token: 0x17007D2C RID: 32044
		// (get) Token: 0x0601768C RID: 95884 RVA: 0x002F6806 File Offset: 0x002F4A06
		// (set) Token: 0x0601768D RID: 95885 RVA: 0x002BD530 File Offset: 0x002BB730
		[SchemaAttr(0, "start")]
		public UInt32Value Start
		{
			get
			{
				return (UInt32Value)base.Attributes[8];
			}
			set
			{
				base.Attributes[8] = value;
			}
		}

		// Token: 0x17007D2D RID: 32045
		// (get) Token: 0x0601768E RID: 95886 RVA: 0x002E7720 File Offset: 0x002E5920
		// (set) Token: 0x0601768F RID: 95887 RVA: 0x002BD54B File Offset: 0x002BB74B
		[SchemaAttr(0, "min")]
		public UInt32Value Min
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

		// Token: 0x17007D2E RID: 32046
		// (get) Token: 0x06017690 RID: 95888 RVA: 0x0031EC49 File Offset: 0x0031CE49
		// (set) Token: 0x06017691 RID: 95889 RVA: 0x002BDB45 File Offset: 0x002BBD45
		[SchemaAttr(0, "max")]
		public UInt32Value Max
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

		// Token: 0x17007D2F RID: 32047
		// (get) Token: 0x06017692 RID: 95890 RVA: 0x002E9686 File Offset: 0x002E7886
		// (set) Token: 0x06017693 RID: 95891 RVA: 0x002BDB61 File Offset: 0x002BBD61
		[SchemaAttr(0, "activeRow")]
		public UInt32Value ActiveRow
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

		// Token: 0x17007D30 RID: 32048
		// (get) Token: 0x06017694 RID: 95892 RVA: 0x002E6EFA File Offset: 0x002E50FA
		// (set) Token: 0x06017695 RID: 95893 RVA: 0x002BDB7D File Offset: 0x002BBD7D
		[SchemaAttr(0, "activeCol")]
		public UInt32Value ActiveColumn
		{
			get
			{
				return (UInt32Value)base.Attributes[12];
			}
			set
			{
				base.Attributes[12] = value;
			}
		}

		// Token: 0x17007D31 RID: 32049
		// (get) Token: 0x06017696 RID: 95894 RVA: 0x0032C7AF File Offset: 0x0032A9AF
		// (set) Token: 0x06017697 RID: 95895 RVA: 0x002BE209 File Offset: 0x002BC409
		[SchemaAttr(0, "previousRow")]
		public UInt32Value PreviousRow
		{
			get
			{
				return (UInt32Value)base.Attributes[13];
			}
			set
			{
				base.Attributes[13] = value;
			}
		}

		// Token: 0x17007D32 RID: 32050
		// (get) Token: 0x06017698 RID: 95896 RVA: 0x003299DA File Offset: 0x00327BDA
		// (set) Token: 0x06017699 RID: 95897 RVA: 0x002BE225 File Offset: 0x002BC425
		[SchemaAttr(0, "previousCol")]
		public UInt32Value PreviousColumn
		{
			get
			{
				return (UInt32Value)base.Attributes[14];
			}
			set
			{
				base.Attributes[14] = value;
			}
		}

		// Token: 0x17007D33 RID: 32051
		// (get) Token: 0x0601769A RID: 95898 RVA: 0x002E6F0A File Offset: 0x002E510A
		// (set) Token: 0x0601769B RID: 95899 RVA: 0x002BE241 File Offset: 0x002BC441
		[SchemaAttr(0, "click")]
		public UInt32Value Click
		{
			get
			{
				return (UInt32Value)base.Attributes[15];
			}
			set
			{
				base.Attributes[15] = value;
			}
		}

		// Token: 0x17007D34 RID: 32052
		// (get) Token: 0x0601769C RID: 95900 RVA: 0x002BE24D File Offset: 0x002BC44D
		// (set) Token: 0x0601769D RID: 95901 RVA: 0x002BE25D File Offset: 0x002BC45D
		[SchemaAttr(19, "id")]
		public StringValue Id
		{
			get
			{
				return (StringValue)base.Attributes[16];
			}
			set
			{
				base.Attributes[16] = value;
			}
		}

		// Token: 0x0601769E RID: 95902 RVA: 0x00293ECF File Offset: 0x002920CF
		public PivotSelection()
		{
		}

		// Token: 0x0601769F RID: 95903 RVA: 0x00293ED7 File Offset: 0x002920D7
		public PivotSelection(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060176A0 RID: 95904 RVA: 0x00293EE0 File Offset: 0x002920E0
		public PivotSelection(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060176A1 RID: 95905 RVA: 0x00293EE9 File Offset: 0x002920E9
		public PivotSelection(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060176A2 RID: 95906 RVA: 0x002E9D93 File Offset: 0x002E7F93
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "pivotArea" == name)
			{
				return new PivotArea();
			}
			return null;
		}

		// Token: 0x17007D35 RID: 32053
		// (get) Token: 0x060176A3 RID: 95907 RVA: 0x0033662A File Offset: 0x0033482A
		internal override string[] ElementTagNames
		{
			get
			{
				return PivotSelection.eleTagNames;
			}
		}

		// Token: 0x17007D36 RID: 32054
		// (get) Token: 0x060176A4 RID: 95908 RVA: 0x00336631 File Offset: 0x00334831
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return PivotSelection.eleNamespaceIds;
			}
		}

		// Token: 0x17007D37 RID: 32055
		// (get) Token: 0x060176A5 RID: 95909 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17007D38 RID: 32056
		// (get) Token: 0x060176A6 RID: 95910 RVA: 0x003304CB File Offset: 0x0032E6CB
		// (set) Token: 0x060176A7 RID: 95911 RVA: 0x003304D4 File Offset: 0x0032E6D4
		public PivotArea PivotArea
		{
			get
			{
				return base.GetElement<PivotArea>(0);
			}
			set
			{
				base.SetElement<PivotArea>(0, value);
			}
		}

		// Token: 0x060176A8 RID: 95912 RVA: 0x00336638 File Offset: 0x00334838
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "pane" == name)
			{
				return new EnumValue<PaneValues>();
			}
			if (namespaceId == 0 && "showHeader" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "label" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "data" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "extendable" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "count" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "axis" == name)
			{
				return new EnumValue<PivotTableAxisValues>();
			}
			if (namespaceId == 0 && "dimension" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "start" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "min" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "max" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "activeRow" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "activeCol" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "previousRow" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "previousCol" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "click" == name)
			{
				return new UInt32Value();
			}
			if (19 == namespaceId && "id" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060176A9 RID: 95913 RVA: 0x003367C5 File Offset: 0x003349C5
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<PivotSelection>(deep);
		}

		// Token: 0x04009C39 RID: 39993
		private const string tagName = "pivotSelection";

		// Token: 0x04009C3A RID: 39994
		private const byte tagNsId = 22;

		// Token: 0x04009C3B RID: 39995
		internal const int ElementTypeIdConst = 11191;

		// Token: 0x04009C3C RID: 39996
		private static string[] attributeTagNames = new string[]
		{
			"pane", "showHeader", "label", "data", "extendable", "count", "axis", "dimension", "start", "min",
			"max", "activeRow", "activeCol", "previousRow", "previousCol", "click", "id"
		};

		// Token: 0x04009C3D RID: 39997
		private static byte[] attributeNamespaceIds = new byte[]
		{
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 19
		};

		// Token: 0x04009C3E RID: 39998
		private static readonly string[] eleTagNames = new string[] { "pivotArea" };

		// Token: 0x04009C3F RID: 39999
		private static readonly byte[] eleNamespaceIds = new byte[] { 22 };
	}
}
