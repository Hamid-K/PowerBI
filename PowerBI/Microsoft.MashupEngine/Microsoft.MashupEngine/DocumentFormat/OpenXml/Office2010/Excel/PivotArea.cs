using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Spreadsheet;

namespace DocumentFormat.OpenXml.Office2010.Excel
{
	// Token: 0x02002427 RID: 9255
	[ChildElementInfo(typeof(ExtensionList))]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(PivotAreaReferences))]
	[GeneratedCode("DomGen", "2.0")]
	internal class PivotArea : OpenXmlCompositeElement
	{
		// Token: 0x17004F87 RID: 20359
		// (get) Token: 0x06010FDC RID: 69596 RVA: 0x002E964B File Offset: 0x002E784B
		public override string LocalName
		{
			get
			{
				return "pivotArea";
			}
		}

		// Token: 0x17004F88 RID: 20360
		// (get) Token: 0x06010FDD RID: 69597 RVA: 0x002E5AC2 File Offset: 0x002E3CC2
		internal override byte NamespaceId
		{
			get
			{
				return 53;
			}
		}

		// Token: 0x17004F89 RID: 20361
		// (get) Token: 0x06010FDE RID: 69598 RVA: 0x002E9652 File Offset: 0x002E7852
		internal override int ElementTypeId
		{
			get
			{
				return 12979;
			}
		}

		// Token: 0x06010FDF RID: 69599 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x17004F8A RID: 20362
		// (get) Token: 0x06010FE0 RID: 69600 RVA: 0x002E9659 File Offset: 0x002E7859
		internal override string[] AttributeTagNames
		{
			get
			{
				return PivotArea.attributeTagNames;
			}
		}

		// Token: 0x17004F8B RID: 20363
		// (get) Token: 0x06010FE1 RID: 69601 RVA: 0x002E9660 File Offset: 0x002E7860
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return PivotArea.attributeNamespaceIds;
			}
		}

		// Token: 0x17004F8C RID: 20364
		// (get) Token: 0x06010FE2 RID: 69602 RVA: 0x002BF6A0 File Offset: 0x002BD8A0
		// (set) Token: 0x06010FE3 RID: 69603 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x17004F8D RID: 20365
		// (get) Token: 0x06010FE4 RID: 69604 RVA: 0x002E9667 File Offset: 0x002E7867
		// (set) Token: 0x06010FE5 RID: 69605 RVA: 0x002BD47A File Offset: 0x002BB67A
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

		// Token: 0x17004F8E RID: 20366
		// (get) Token: 0x06010FE6 RID: 69606 RVA: 0x002C8F66 File Offset: 0x002C7166
		// (set) Token: 0x06010FE7 RID: 69607 RVA: 0x002BD494 File Offset: 0x002BB694
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

		// Token: 0x17004F8F RID: 20367
		// (get) Token: 0x06010FE8 RID: 69608 RVA: 0x002CB9D9 File Offset: 0x002C9BD9
		// (set) Token: 0x06010FE9 RID: 69609 RVA: 0x002BD4AE File Offset: 0x002BB6AE
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

		// Token: 0x17004F90 RID: 20368
		// (get) Token: 0x06010FEA RID: 69610 RVA: 0x002CBE2D File Offset: 0x002CA02D
		// (set) Token: 0x06010FEB RID: 69611 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
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

		// Token: 0x17004F91 RID: 20369
		// (get) Token: 0x06010FEC RID: 69612 RVA: 0x002D7D70 File Offset: 0x002D5F70
		// (set) Token: 0x06010FED RID: 69613 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
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

		// Token: 0x17004F92 RID: 20370
		// (get) Token: 0x06010FEE RID: 69614 RVA: 0x002CCC33 File Offset: 0x002CAE33
		// (set) Token: 0x06010FEF RID: 69615 RVA: 0x002BD4FC File Offset: 0x002BB6FC
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

		// Token: 0x17004F93 RID: 20371
		// (get) Token: 0x06010FF0 RID: 69616 RVA: 0x002CA736 File Offset: 0x002C8936
		// (set) Token: 0x06010FF1 RID: 69617 RVA: 0x002BD516 File Offset: 0x002BB716
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

		// Token: 0x17004F94 RID: 20372
		// (get) Token: 0x06010FF2 RID: 69618 RVA: 0x002BDB16 File Offset: 0x002BBD16
		// (set) Token: 0x06010FF3 RID: 69619 RVA: 0x002BD530 File Offset: 0x002BB730
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

		// Token: 0x17004F95 RID: 20373
		// (get) Token: 0x06010FF4 RID: 69620 RVA: 0x002C92EA File Offset: 0x002C74EA
		// (set) Token: 0x06010FF5 RID: 69621 RVA: 0x002BD54B File Offset: 0x002BB74B
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

		// Token: 0x17004F96 RID: 20374
		// (get) Token: 0x06010FF6 RID: 69622 RVA: 0x002E9676 File Offset: 0x002E7876
		// (set) Token: 0x06010FF7 RID: 69623 RVA: 0x002BDB45 File Offset: 0x002BBD45
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

		// Token: 0x17004F97 RID: 20375
		// (get) Token: 0x06010FF8 RID: 69624 RVA: 0x002E9686 File Offset: 0x002E7886
		// (set) Token: 0x06010FF9 RID: 69625 RVA: 0x002BDB61 File Offset: 0x002BBD61
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

		// Token: 0x06010FFA RID: 69626 RVA: 0x00293ECF File Offset: 0x002920CF
		public PivotArea()
		{
		}

		// Token: 0x06010FFB RID: 69627 RVA: 0x00293ED7 File Offset: 0x002920D7
		public PivotArea(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06010FFC RID: 69628 RVA: 0x00293EE0 File Offset: 0x002920E0
		public PivotArea(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06010FFD RID: 69629 RVA: 0x00293EE9 File Offset: 0x002920E9
		public PivotArea(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06010FFE RID: 69630 RVA: 0x002E9696 File Offset: 0x002E7896
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

		// Token: 0x17004F98 RID: 20376
		// (get) Token: 0x06010FFF RID: 69631 RVA: 0x002E96C9 File Offset: 0x002E78C9
		internal override string[] ElementTagNames
		{
			get
			{
				return PivotArea.eleTagNames;
			}
		}

		// Token: 0x17004F99 RID: 20377
		// (get) Token: 0x06011000 RID: 69632 RVA: 0x002E96D0 File Offset: 0x002E78D0
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return PivotArea.eleNamespaceIds;
			}
		}

		// Token: 0x17004F9A RID: 20378
		// (get) Token: 0x06011001 RID: 69633 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17004F9B RID: 20379
		// (get) Token: 0x06011002 RID: 69634 RVA: 0x002E96D7 File Offset: 0x002E78D7
		// (set) Token: 0x06011003 RID: 69635 RVA: 0x002E96E0 File Offset: 0x002E78E0
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

		// Token: 0x17004F9C RID: 20380
		// (get) Token: 0x06011004 RID: 69636 RVA: 0x002E96EA File Offset: 0x002E78EA
		// (set) Token: 0x06011005 RID: 69637 RVA: 0x002E96F3 File Offset: 0x002E78F3
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

		// Token: 0x06011006 RID: 69638 RVA: 0x002E9700 File Offset: 0x002E7900
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

		// Token: 0x06011007 RID: 69639 RVA: 0x002E981D File Offset: 0x002E7A1D
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<PivotArea>(deep);
		}

		// Token: 0x06011008 RID: 69640 RVA: 0x002E9828 File Offset: 0x002E7A28
		// Note: this type is marked as 'beforefieldinit'.
		static PivotArea()
		{
			byte[] array = new byte[12];
			PivotArea.attributeNamespaceIds = array;
			PivotArea.eleTagNames = new string[] { "references", "extLst" };
			PivotArea.eleNamespaceIds = new byte[] { 22, 22 };
		}

		// Token: 0x04007732 RID: 30514
		private const string tagName = "pivotArea";

		// Token: 0x04007733 RID: 30515
		private const byte tagNsId = 53;

		// Token: 0x04007734 RID: 30516
		internal const int ElementTypeIdConst = 12979;

		// Token: 0x04007735 RID: 30517
		private static string[] attributeTagNames = new string[]
		{
			"field", "type", "dataOnly", "labelOnly", "grandRow", "grandCol", "cacheIndex", "outline", "offset", "collapsedLevelsAreSubtotals",
			"axis", "fieldPosition"
		};

		// Token: 0x04007736 RID: 30518
		private static byte[] attributeNamespaceIds;

		// Token: 0x04007737 RID: 30519
		private static readonly string[] eleTagNames;

		// Token: 0x04007738 RID: 30520
		private static readonly byte[] eleNamespaceIds;
	}
}
