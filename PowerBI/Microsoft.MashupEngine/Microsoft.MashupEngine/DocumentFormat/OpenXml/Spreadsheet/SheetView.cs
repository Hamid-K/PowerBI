using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002BF0 RID: 11248
	[ChildElementInfo(typeof(Pane))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(Selection))]
	[ChildElementInfo(typeof(PivotSelection))]
	[ChildElementInfo(typeof(ExtensionList))]
	internal class SheetView : OpenXmlCompositeElement
	{
		// Token: 0x17007E7A RID: 32378
		// (get) Token: 0x0601794B RID: 96587 RVA: 0x00337F60 File Offset: 0x00336160
		public override string LocalName
		{
			get
			{
				return "sheetView";
			}
		}

		// Token: 0x17007E7B RID: 32379
		// (get) Token: 0x0601794C RID: 96588 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17007E7C RID: 32380
		// (get) Token: 0x0601794D RID: 96589 RVA: 0x003389AB File Offset: 0x00336BAB
		internal override int ElementTypeId
		{
			get
			{
				return 11220;
			}
		}

		// Token: 0x0601794E RID: 96590 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17007E7D RID: 32381
		// (get) Token: 0x0601794F RID: 96591 RVA: 0x003389B2 File Offset: 0x00336BB2
		internal override string[] AttributeTagNames
		{
			get
			{
				return SheetView.attributeTagNames;
			}
		}

		// Token: 0x17007E7E RID: 32382
		// (get) Token: 0x06017950 RID: 96592 RVA: 0x003389B9 File Offset: 0x00336BB9
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return SheetView.attributeNamespaceIds;
			}
		}

		// Token: 0x17007E7F RID: 32383
		// (get) Token: 0x06017951 RID: 96593 RVA: 0x002C9F7B File Offset: 0x002C817B
		// (set) Token: 0x06017952 RID: 96594 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "windowProtection")]
		public BooleanValue WindowProtection
		{
			get
			{
				return (BooleanValue)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17007E80 RID: 32384
		// (get) Token: 0x06017953 RID: 96595 RVA: 0x002C8B36 File Offset: 0x002C6D36
		// (set) Token: 0x06017954 RID: 96596 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "showFormulas")]
		public BooleanValue ShowFormulas
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

		// Token: 0x17007E81 RID: 32385
		// (get) Token: 0x06017955 RID: 96597 RVA: 0x002C8F66 File Offset: 0x002C7166
		// (set) Token: 0x06017956 RID: 96598 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "showGridLines")]
		public BooleanValue ShowGridLines
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

		// Token: 0x17007E82 RID: 32386
		// (get) Token: 0x06017957 RID: 96599 RVA: 0x002CB9D9 File Offset: 0x002C9BD9
		// (set) Token: 0x06017958 RID: 96600 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "showRowColHeaders")]
		public BooleanValue ShowRowColHeaders
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

		// Token: 0x17007E83 RID: 32387
		// (get) Token: 0x06017959 RID: 96601 RVA: 0x002CBE2D File Offset: 0x002CA02D
		// (set) Token: 0x0601795A RID: 96602 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "showZeros")]
		public BooleanValue ShowZeros
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

		// Token: 0x17007E84 RID: 32388
		// (get) Token: 0x0601795B RID: 96603 RVA: 0x002D7D70 File Offset: 0x002D5F70
		// (set) Token: 0x0601795C RID: 96604 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "rightToLeft")]
		public BooleanValue RightToLeft
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

		// Token: 0x17007E85 RID: 32389
		// (get) Token: 0x0601795D RID: 96605 RVA: 0x002CCC33 File Offset: 0x002CAE33
		// (set) Token: 0x0601795E RID: 96606 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "tabSelected")]
		public BooleanValue TabSelected
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

		// Token: 0x17007E86 RID: 32390
		// (get) Token: 0x0601795F RID: 96607 RVA: 0x002CA736 File Offset: 0x002C8936
		// (set) Token: 0x06017960 RID: 96608 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(0, "showRuler")]
		public BooleanValue ShowRuler
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

		// Token: 0x17007E87 RID: 32391
		// (get) Token: 0x06017961 RID: 96609 RVA: 0x002CB706 File Offset: 0x002C9906
		// (set) Token: 0x06017962 RID: 96610 RVA: 0x002BD530 File Offset: 0x002BB730
		[SchemaAttr(0, "showOutlineSymbols")]
		public BooleanValue ShowOutlineSymbols
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

		// Token: 0x17007E88 RID: 32392
		// (get) Token: 0x06017963 RID: 96611 RVA: 0x002C92EA File Offset: 0x002C74EA
		// (set) Token: 0x06017964 RID: 96612 RVA: 0x002BD54B File Offset: 0x002BB74B
		[SchemaAttr(0, "defaultGridColor")]
		public BooleanValue DefaultGridColor
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

		// Token: 0x17007E89 RID: 32393
		// (get) Token: 0x06017965 RID: 96613 RVA: 0x002C8762 File Offset: 0x002C6962
		// (set) Token: 0x06017966 RID: 96614 RVA: 0x002BDB45 File Offset: 0x002BBD45
		[SchemaAttr(0, "showWhiteSpace")]
		public BooleanValue ShowWhiteSpace
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

		// Token: 0x17007E8A RID: 32394
		// (get) Token: 0x06017967 RID: 96615 RVA: 0x003389C0 File Offset: 0x00336BC0
		// (set) Token: 0x06017968 RID: 96616 RVA: 0x002BDB61 File Offset: 0x002BBD61
		[SchemaAttr(0, "view")]
		public EnumValue<SheetViewValues> View
		{
			get
			{
				return (EnumValue<SheetViewValues>)base.Attributes[11];
			}
			set
			{
				base.Attributes[11] = value;
			}
		}

		// Token: 0x17007E8B RID: 32395
		// (get) Token: 0x06017969 RID: 96617 RVA: 0x002BDB6D File Offset: 0x002BBD6D
		// (set) Token: 0x0601796A RID: 96618 RVA: 0x002BDB7D File Offset: 0x002BBD7D
		[SchemaAttr(0, "topLeftCell")]
		public StringValue TopLeftCell
		{
			get
			{
				return (StringValue)base.Attributes[12];
			}
			set
			{
				base.Attributes[12] = value;
			}
		}

		// Token: 0x17007E8C RID: 32396
		// (get) Token: 0x0601796B RID: 96619 RVA: 0x0032C7AF File Offset: 0x0032A9AF
		// (set) Token: 0x0601796C RID: 96620 RVA: 0x002BE209 File Offset: 0x002BC409
		[SchemaAttr(0, "colorId")]
		public UInt32Value ColorId
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

		// Token: 0x17007E8D RID: 32397
		// (get) Token: 0x0601796D RID: 96621 RVA: 0x003299DA File Offset: 0x00327BDA
		// (set) Token: 0x0601796E RID: 96622 RVA: 0x002BE225 File Offset: 0x002BC425
		[SchemaAttr(0, "zoomScale")]
		public UInt32Value ZoomScale
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

		// Token: 0x17007E8E RID: 32398
		// (get) Token: 0x0601796F RID: 96623 RVA: 0x002E6F0A File Offset: 0x002E510A
		// (set) Token: 0x06017970 RID: 96624 RVA: 0x002BE241 File Offset: 0x002BC441
		[SchemaAttr(0, "zoomScaleNormal")]
		public UInt32Value ZoomScaleNormal
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

		// Token: 0x17007E8F RID: 32399
		// (get) Token: 0x06017971 RID: 96625 RVA: 0x002E6F1A File Offset: 0x002E511A
		// (set) Token: 0x06017972 RID: 96626 RVA: 0x002BE25D File Offset: 0x002BC45D
		[SchemaAttr(0, "zoomScaleSheetLayoutView")]
		public UInt32Value ZoomScaleSheetLayoutView
		{
			get
			{
				return (UInt32Value)base.Attributes[16];
			}
			set
			{
				base.Attributes[16] = value;
			}
		}

		// Token: 0x17007E90 RID: 32400
		// (get) Token: 0x06017973 RID: 96627 RVA: 0x0030F16C File Offset: 0x0030D36C
		// (set) Token: 0x06017974 RID: 96628 RVA: 0x002BE279 File Offset: 0x002BC479
		[SchemaAttr(0, "zoomScalePageLayoutView")]
		public UInt32Value ZoomScalePageLayoutView
		{
			get
			{
				return (UInt32Value)base.Attributes[17];
			}
			set
			{
				base.Attributes[17] = value;
			}
		}

		// Token: 0x17007E91 RID: 32401
		// (get) Token: 0x06017975 RID: 96629 RVA: 0x003389D0 File Offset: 0x00336BD0
		// (set) Token: 0x06017976 RID: 96630 RVA: 0x002BE295 File Offset: 0x002BC495
		[SchemaAttr(0, "workbookViewId")]
		public UInt32Value WorkbookViewId
		{
			get
			{
				return (UInt32Value)base.Attributes[18];
			}
			set
			{
				base.Attributes[18] = value;
			}
		}

		// Token: 0x06017977 RID: 96631 RVA: 0x00293ECF File Offset: 0x002920CF
		public SheetView()
		{
		}

		// Token: 0x06017978 RID: 96632 RVA: 0x00293ED7 File Offset: 0x002920D7
		public SheetView(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06017979 RID: 96633 RVA: 0x00293EE0 File Offset: 0x002920E0
		public SheetView(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601797A RID: 96634 RVA: 0x00293EE9 File Offset: 0x002920E9
		public SheetView(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601797B RID: 96635 RVA: 0x003389E0 File Offset: 0x00336BE0
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "pane" == name)
			{
				return new Pane();
			}
			if (22 == namespaceId && "selection" == name)
			{
				return new Selection();
			}
			if (22 == namespaceId && "pivotSelection" == name)
			{
				return new PivotSelection();
			}
			if (22 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x17007E92 RID: 32402
		// (get) Token: 0x0601797C RID: 96636 RVA: 0x00338A4E File Offset: 0x00336C4E
		internal override string[] ElementTagNames
		{
			get
			{
				return SheetView.eleTagNames;
			}
		}

		// Token: 0x17007E93 RID: 32403
		// (get) Token: 0x0601797D RID: 96637 RVA: 0x00338A55 File Offset: 0x00336C55
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return SheetView.eleNamespaceIds;
			}
		}

		// Token: 0x17007E94 RID: 32404
		// (get) Token: 0x0601797E RID: 96638 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17007E95 RID: 32405
		// (get) Token: 0x0601797F RID: 96639 RVA: 0x00338A5C File Offset: 0x00336C5C
		// (set) Token: 0x06017980 RID: 96640 RVA: 0x00338A65 File Offset: 0x00336C65
		public Pane Pane
		{
			get
			{
				return base.GetElement<Pane>(0);
			}
			set
			{
				base.SetElement<Pane>(0, value);
			}
		}

		// Token: 0x06017981 RID: 96641 RVA: 0x00338A70 File Offset: 0x00336C70
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "windowProtection" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "showFormulas" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "showGridLines" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "showRowColHeaders" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "showZeros" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "rightToLeft" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "tabSelected" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "showRuler" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "showOutlineSymbols" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "defaultGridColor" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "showWhiteSpace" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "view" == name)
			{
				return new EnumValue<SheetViewValues>();
			}
			if (namespaceId == 0 && "topLeftCell" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "colorId" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "zoomScale" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "zoomScaleNormal" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "zoomScaleSheetLayoutView" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "zoomScalePageLayoutView" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "workbookViewId" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06017982 RID: 96642 RVA: 0x00338C27 File Offset: 0x00336E27
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SheetView>(deep);
		}

		// Token: 0x06017983 RID: 96643 RVA: 0x00338C30 File Offset: 0x00336E30
		// Note: this type is marked as 'beforefieldinit'.
		static SheetView()
		{
			byte[] array = new byte[19];
			SheetView.attributeNamespaceIds = array;
			SheetView.eleTagNames = new string[] { "pane", "selection", "pivotSelection", "extLst" };
			SheetView.eleNamespaceIds = new byte[] { 22, 22, 22, 22 };
		}

		// Token: 0x04009CD8 RID: 40152
		private const string tagName = "sheetView";

		// Token: 0x04009CD9 RID: 40153
		private const byte tagNsId = 22;

		// Token: 0x04009CDA RID: 40154
		internal const int ElementTypeIdConst = 11220;

		// Token: 0x04009CDB RID: 40155
		private static string[] attributeTagNames = new string[]
		{
			"windowProtection", "showFormulas", "showGridLines", "showRowColHeaders", "showZeros", "rightToLeft", "tabSelected", "showRuler", "showOutlineSymbols", "defaultGridColor",
			"showWhiteSpace", "view", "topLeftCell", "colorId", "zoomScale", "zoomScaleNormal", "zoomScaleSheetLayoutView", "zoomScalePageLayoutView", "workbookViewId"
		};

		// Token: 0x04009CDC RID: 40156
		private static byte[] attributeNamespaceIds;

		// Token: 0x04009CDD RID: 40157
		private static readonly string[] eleTagNames;

		// Token: 0x04009CDE RID: 40158
		private static readonly byte[] eleNamespaceIds;
	}
}
