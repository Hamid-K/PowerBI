using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002BF1 RID: 11249
	[ChildElementInfo(typeof(ExtensionList))]
	[ChildElementInfo(typeof(PageSetup))]
	[ChildElementInfo(typeof(PrintOptions))]
	[ChildElementInfo(typeof(PageMargins))]
	[ChildElementInfo(typeof(Selection))]
	[ChildElementInfo(typeof(RowBreaks))]
	[ChildElementInfo(typeof(ColumnBreaks))]
	[ChildElementInfo(typeof(HeaderFooter))]
	[ChildElementInfo(typeof(Pane))]
	[ChildElementInfo(typeof(AutoFilter))]
	[GeneratedCode("DomGen", "2.0")]
	internal class CustomSheetView : OpenXmlCompositeElement
	{
		// Token: 0x17007E96 RID: 32406
		// (get) Token: 0x06017984 RID: 96644 RVA: 0x00338072 File Offset: 0x00336272
		public override string LocalName
		{
			get
			{
				return "customSheetView";
			}
		}

		// Token: 0x17007E97 RID: 32407
		// (get) Token: 0x06017985 RID: 96645 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17007E98 RID: 32408
		// (get) Token: 0x06017986 RID: 96646 RVA: 0x00338D3E File Offset: 0x00336F3E
		internal override int ElementTypeId
		{
			get
			{
				return 11221;
			}
		}

		// Token: 0x06017987 RID: 96647 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17007E99 RID: 32409
		// (get) Token: 0x06017988 RID: 96648 RVA: 0x00338D45 File Offset: 0x00336F45
		internal override string[] AttributeTagNames
		{
			get
			{
				return CustomSheetView.attributeTagNames;
			}
		}

		// Token: 0x17007E9A RID: 32410
		// (get) Token: 0x06017989 RID: 96649 RVA: 0x00338D4C File Offset: 0x00336F4C
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return CustomSheetView.attributeNamespaceIds;
			}
		}

		// Token: 0x17007E9B RID: 32411
		// (get) Token: 0x0601798A RID: 96650 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0601798B RID: 96651 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "guid")]
		public StringValue Guid
		{
			get
			{
				return (StringValue)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17007E9C RID: 32412
		// (get) Token: 0x0601798C RID: 96652 RVA: 0x002E33EC File Offset: 0x002E15EC
		// (set) Token: 0x0601798D RID: 96653 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "scale")]
		public UInt32Value Scale
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

		// Token: 0x17007E9D RID: 32413
		// (get) Token: 0x0601798E RID: 96654 RVA: 0x002E5814 File Offset: 0x002E3A14
		// (set) Token: 0x0601798F RID: 96655 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "colorId")]
		public UInt32Value ColorId
		{
			get
			{
				return (UInt32Value)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x17007E9E RID: 32414
		// (get) Token: 0x06017990 RID: 96656 RVA: 0x002CB9D9 File Offset: 0x002C9BD9
		// (set) Token: 0x06017991 RID: 96657 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "showPageBreaks")]
		public BooleanValue ShowPageBreaks
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

		// Token: 0x17007E9F RID: 32415
		// (get) Token: 0x06017992 RID: 96658 RVA: 0x002CBE2D File Offset: 0x002CA02D
		// (set) Token: 0x06017993 RID: 96659 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "showFormulas")]
		public BooleanValue ShowFormulas
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

		// Token: 0x17007EA0 RID: 32416
		// (get) Token: 0x06017994 RID: 96660 RVA: 0x002D7D70 File Offset: 0x002D5F70
		// (set) Token: 0x06017995 RID: 96661 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "showGridLines")]
		public BooleanValue ShowGridLines
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

		// Token: 0x17007EA1 RID: 32417
		// (get) Token: 0x06017996 RID: 96662 RVA: 0x002CCC33 File Offset: 0x002CAE33
		// (set) Token: 0x06017997 RID: 96663 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "showRowCol")]
		public BooleanValue ShowRowColumn
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

		// Token: 0x17007EA2 RID: 32418
		// (get) Token: 0x06017998 RID: 96664 RVA: 0x002CA736 File Offset: 0x002C8936
		// (set) Token: 0x06017999 RID: 96665 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(0, "outlineSymbols")]
		public BooleanValue OutlineSymbols
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

		// Token: 0x17007EA3 RID: 32419
		// (get) Token: 0x0601799A RID: 96666 RVA: 0x002CB706 File Offset: 0x002C9906
		// (set) Token: 0x0601799B RID: 96667 RVA: 0x002BD530 File Offset: 0x002BB730
		[SchemaAttr(0, "zeroValues")]
		public BooleanValue ZeroValues
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

		// Token: 0x17007EA4 RID: 32420
		// (get) Token: 0x0601799C RID: 96668 RVA: 0x002C92EA File Offset: 0x002C74EA
		// (set) Token: 0x0601799D RID: 96669 RVA: 0x002BD54B File Offset: 0x002BB74B
		[SchemaAttr(0, "fitToPage")]
		public BooleanValue FitToPage
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

		// Token: 0x17007EA5 RID: 32421
		// (get) Token: 0x0601799E RID: 96670 RVA: 0x002C8762 File Offset: 0x002C6962
		// (set) Token: 0x0601799F RID: 96671 RVA: 0x002BDB45 File Offset: 0x002BBD45
		[SchemaAttr(0, "printArea")]
		public BooleanValue PrintArea
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

		// Token: 0x17007EA6 RID: 32422
		// (get) Token: 0x060179A0 RID: 96672 RVA: 0x002C92FA File Offset: 0x002C74FA
		// (set) Token: 0x060179A1 RID: 96673 RVA: 0x002BDB61 File Offset: 0x002BBD61
		[SchemaAttr(0, "filter")]
		public BooleanValue Filter
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

		// Token: 0x17007EA7 RID: 32423
		// (get) Token: 0x060179A2 RID: 96674 RVA: 0x002CE196 File Offset: 0x002CC396
		// (set) Token: 0x060179A3 RID: 96675 RVA: 0x002BDB7D File Offset: 0x002BBD7D
		[SchemaAttr(0, "showAutoFilter")]
		public BooleanValue ShowAutoFilter
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

		// Token: 0x17007EA8 RID: 32424
		// (get) Token: 0x060179A4 RID: 96676 RVA: 0x002CD15F File Offset: 0x002CB35F
		// (set) Token: 0x060179A5 RID: 96677 RVA: 0x002BE209 File Offset: 0x002BC409
		[SchemaAttr(0, "hiddenRows")]
		public BooleanValue HiddenRows
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

		// Token: 0x17007EA9 RID: 32425
		// (get) Token: 0x060179A6 RID: 96678 RVA: 0x002C9F8A File Offset: 0x002C818A
		// (set) Token: 0x060179A7 RID: 96679 RVA: 0x002BE225 File Offset: 0x002BC425
		[SchemaAttr(0, "hiddenColumns")]
		public BooleanValue HiddenColumns
		{
			get
			{
				return (BooleanValue)base.Attributes[14];
			}
			set
			{
				base.Attributes[14] = value;
			}
		}

		// Token: 0x17007EAA RID: 32426
		// (get) Token: 0x060179A8 RID: 96680 RVA: 0x00338D53 File Offset: 0x00336F53
		// (set) Token: 0x060179A9 RID: 96681 RVA: 0x002BE241 File Offset: 0x002BC441
		[SchemaAttr(0, "state")]
		public EnumValue<SheetStateValues> State
		{
			get
			{
				return (EnumValue<SheetStateValues>)base.Attributes[15];
			}
			set
			{
				base.Attributes[15] = value;
			}
		}

		// Token: 0x17007EAB RID: 32427
		// (get) Token: 0x060179AA RID: 96682 RVA: 0x002C930A File Offset: 0x002C750A
		// (set) Token: 0x060179AB RID: 96683 RVA: 0x002BE25D File Offset: 0x002BC45D
		[SchemaAttr(0, "filterUnique")]
		public BooleanValue FilterUnique
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

		// Token: 0x17007EAC RID: 32428
		// (get) Token: 0x060179AC RID: 96684 RVA: 0x00338D63 File Offset: 0x00336F63
		// (set) Token: 0x060179AD RID: 96685 RVA: 0x002BE279 File Offset: 0x002BC479
		[SchemaAttr(0, "view")]
		public EnumValue<SheetViewValues> View
		{
			get
			{
				return (EnumValue<SheetViewValues>)base.Attributes[17];
			}
			set
			{
				base.Attributes[17] = value;
			}
		}

		// Token: 0x17007EAD RID: 32429
		// (get) Token: 0x060179AE RID: 96686 RVA: 0x002C8772 File Offset: 0x002C6972
		// (set) Token: 0x060179AF RID: 96687 RVA: 0x002BE295 File Offset: 0x002BC495
		[SchemaAttr(0, "showRuler")]
		public BooleanValue ShowRuler
		{
			get
			{
				return (BooleanValue)base.Attributes[18];
			}
			set
			{
				base.Attributes[18] = value;
			}
		}

		// Token: 0x17007EAE RID: 32430
		// (get) Token: 0x060179B0 RID: 96688 RVA: 0x002BE2A1 File Offset: 0x002BC4A1
		// (set) Token: 0x060179B1 RID: 96689 RVA: 0x002BE2B1 File Offset: 0x002BC4B1
		[SchemaAttr(0, "topLeftCell")]
		public StringValue TopLeftCell
		{
			get
			{
				return (StringValue)base.Attributes[19];
			}
			set
			{
				base.Attributes[19] = value;
			}
		}

		// Token: 0x060179B2 RID: 96690 RVA: 0x00293ECF File Offset: 0x002920CF
		public CustomSheetView()
		{
		}

		// Token: 0x060179B3 RID: 96691 RVA: 0x00293ED7 File Offset: 0x002920D7
		public CustomSheetView(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060179B4 RID: 96692 RVA: 0x00293EE0 File Offset: 0x002920E0
		public CustomSheetView(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060179B5 RID: 96693 RVA: 0x00293EE9 File Offset: 0x002920E9
		public CustomSheetView(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060179B6 RID: 96694 RVA: 0x00338D74 File Offset: 0x00336F74
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
			if (22 == namespaceId && "rowBreaks" == name)
			{
				return new RowBreaks();
			}
			if (22 == namespaceId && "colBreaks" == name)
			{
				return new ColumnBreaks();
			}
			if (22 == namespaceId && "pageMargins" == name)
			{
				return new PageMargins();
			}
			if (22 == namespaceId && "printOptions" == name)
			{
				return new PrintOptions();
			}
			if (22 == namespaceId && "pageSetup" == name)
			{
				return new PageSetup();
			}
			if (22 == namespaceId && "headerFooter" == name)
			{
				return new HeaderFooter();
			}
			if (22 == namespaceId && "autoFilter" == name)
			{
				return new AutoFilter();
			}
			if (22 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x17007EAF RID: 32431
		// (get) Token: 0x060179B7 RID: 96695 RVA: 0x00338E72 File Offset: 0x00337072
		internal override string[] ElementTagNames
		{
			get
			{
				return CustomSheetView.eleTagNames;
			}
		}

		// Token: 0x17007EB0 RID: 32432
		// (get) Token: 0x060179B8 RID: 96696 RVA: 0x00338E79 File Offset: 0x00337079
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return CustomSheetView.eleNamespaceIds;
			}
		}

		// Token: 0x17007EB1 RID: 32433
		// (get) Token: 0x060179B9 RID: 96697 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17007EB2 RID: 32434
		// (get) Token: 0x060179BA RID: 96698 RVA: 0x00338A5C File Offset: 0x00336C5C
		// (set) Token: 0x060179BB RID: 96699 RVA: 0x00338A65 File Offset: 0x00336C65
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

		// Token: 0x17007EB3 RID: 32435
		// (get) Token: 0x060179BC RID: 96700 RVA: 0x00338E80 File Offset: 0x00337080
		// (set) Token: 0x060179BD RID: 96701 RVA: 0x00338E89 File Offset: 0x00337089
		public Selection Selection
		{
			get
			{
				return base.GetElement<Selection>(1);
			}
			set
			{
				base.SetElement<Selection>(1, value);
			}
		}

		// Token: 0x17007EB4 RID: 32436
		// (get) Token: 0x060179BE RID: 96702 RVA: 0x00338E93 File Offset: 0x00337093
		// (set) Token: 0x060179BF RID: 96703 RVA: 0x00338E9C File Offset: 0x0033709C
		public RowBreaks RowBreaks
		{
			get
			{
				return base.GetElement<RowBreaks>(2);
			}
			set
			{
				base.SetElement<RowBreaks>(2, value);
			}
		}

		// Token: 0x17007EB5 RID: 32437
		// (get) Token: 0x060179C0 RID: 96704 RVA: 0x00338EA6 File Offset: 0x003370A6
		// (set) Token: 0x060179C1 RID: 96705 RVA: 0x00338EAF File Offset: 0x003370AF
		public ColumnBreaks ColumnBreaks
		{
			get
			{
				return base.GetElement<ColumnBreaks>(3);
			}
			set
			{
				base.SetElement<ColumnBreaks>(3, value);
			}
		}

		// Token: 0x17007EB6 RID: 32438
		// (get) Token: 0x060179C2 RID: 96706 RVA: 0x0032BD64 File Offset: 0x00329F64
		// (set) Token: 0x060179C3 RID: 96707 RVA: 0x0032BD6D File Offset: 0x00329F6D
		public PageMargins PageMargins
		{
			get
			{
				return base.GetElement<PageMargins>(4);
			}
			set
			{
				base.SetElement<PageMargins>(4, value);
			}
		}

		// Token: 0x17007EB7 RID: 32439
		// (get) Token: 0x060179C4 RID: 96708 RVA: 0x0032C0E4 File Offset: 0x0032A2E4
		// (set) Token: 0x060179C5 RID: 96709 RVA: 0x0032C0ED File Offset: 0x0032A2ED
		public PrintOptions PrintOptions
		{
			get
			{
				return base.GetElement<PrintOptions>(5);
			}
			set
			{
				base.SetElement<PrintOptions>(5, value);
			}
		}

		// Token: 0x17007EB8 RID: 32440
		// (get) Token: 0x060179C6 RID: 96710 RVA: 0x00338EB9 File Offset: 0x003370B9
		// (set) Token: 0x060179C7 RID: 96711 RVA: 0x00338EC2 File Offset: 0x003370C2
		public PageSetup PageSetup
		{
			get
			{
				return base.GetElement<PageSetup>(6);
			}
			set
			{
				base.SetElement<PageSetup>(6, value);
			}
		}

		// Token: 0x17007EB9 RID: 32441
		// (get) Token: 0x060179C8 RID: 96712 RVA: 0x00338ECC File Offset: 0x003370CC
		// (set) Token: 0x060179C9 RID: 96713 RVA: 0x00338ED5 File Offset: 0x003370D5
		public HeaderFooter HeaderFooter
		{
			get
			{
				return base.GetElement<HeaderFooter>(7);
			}
			set
			{
				base.SetElement<HeaderFooter>(7, value);
			}
		}

		// Token: 0x17007EBA RID: 32442
		// (get) Token: 0x060179CA RID: 96714 RVA: 0x00338EDF File Offset: 0x003370DF
		// (set) Token: 0x060179CB RID: 96715 RVA: 0x00338EE8 File Offset: 0x003370E8
		public AutoFilter AutoFilter
		{
			get
			{
				return base.GetElement<AutoFilter>(8);
			}
			set
			{
				base.SetElement<AutoFilter>(8, value);
			}
		}

		// Token: 0x17007EBB RID: 32443
		// (get) Token: 0x060179CC RID: 96716 RVA: 0x00338EF2 File Offset: 0x003370F2
		// (set) Token: 0x060179CD RID: 96717 RVA: 0x00338EFC File Offset: 0x003370FC
		public ExtensionList ExtensionList
		{
			get
			{
				return base.GetElement<ExtensionList>(9);
			}
			set
			{
				base.SetElement<ExtensionList>(9, value);
			}
		}

		// Token: 0x060179CE RID: 96718 RVA: 0x00338F08 File Offset: 0x00337108
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "guid" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "scale" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "colorId" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "showPageBreaks" == name)
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
			if (namespaceId == 0 && "showRowCol" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "outlineSymbols" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "zeroValues" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "fitToPage" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "printArea" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "filter" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "showAutoFilter" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "hiddenRows" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "hiddenColumns" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "state" == name)
			{
				return new EnumValue<SheetStateValues>();
			}
			if (namespaceId == 0 && "filterUnique" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "view" == name)
			{
				return new EnumValue<SheetViewValues>();
			}
			if (namespaceId == 0 && "showRuler" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "topLeftCell" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060179CF RID: 96719 RVA: 0x003390D5 File Offset: 0x003372D5
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<CustomSheetView>(deep);
		}

		// Token: 0x060179D0 RID: 96720 RVA: 0x003390E0 File Offset: 0x003372E0
		// Note: this type is marked as 'beforefieldinit'.
		static CustomSheetView()
		{
			byte[] array = new byte[20];
			CustomSheetView.attributeNamespaceIds = array;
			CustomSheetView.eleTagNames = new string[] { "pane", "selection", "rowBreaks", "colBreaks", "pageMargins", "printOptions", "pageSetup", "headerFooter", "autoFilter", "extLst" };
			CustomSheetView.eleNamespaceIds = new byte[] { 22, 22, 22, 22, 22, 22, 22, 22, 22, 22 };
		}

		// Token: 0x04009CDF RID: 40159
		private const string tagName = "customSheetView";

		// Token: 0x04009CE0 RID: 40160
		private const byte tagNsId = 22;

		// Token: 0x04009CE1 RID: 40161
		internal const int ElementTypeIdConst = 11221;

		// Token: 0x04009CE2 RID: 40162
		private static string[] attributeTagNames = new string[]
		{
			"guid", "scale", "colorId", "showPageBreaks", "showFormulas", "showGridLines", "showRowCol", "outlineSymbols", "zeroValues", "fitToPage",
			"printArea", "filter", "showAutoFilter", "hiddenRows", "hiddenColumns", "state", "filterUnique", "view", "showRuler", "topLeftCell"
		};

		// Token: 0x04009CE3 RID: 40163
		private static byte[] attributeNamespaceIds;

		// Token: 0x04009CE4 RID: 40164
		private static readonly string[] eleTagNames;

		// Token: 0x04009CE5 RID: 40165
		private static readonly byte[] eleNamespaceIds;
	}
}
