using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Office2010.Word.Drawing;

namespace DocumentFormat.OpenXml.Drawing.Wordprocessing
{
	// Token: 0x020028A2 RID: 10402
	[ChildElementInfo(typeof(Graphic))]
	[ChildElementInfo(typeof(WrapTopBottom))]
	[ChildElementInfo(typeof(EffectExtent))]
	[ChildElementInfo(typeof(WrapNone))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(SimplePosition))]
	[ChildElementInfo(typeof(HorizontalPosition))]
	[ChildElementInfo(typeof(VerticalPosition))]
	[ChildElementInfo(typeof(Extent))]
	[ChildElementInfo(typeof(WrapSquare))]
	[ChildElementInfo(typeof(WrapTight))]
	[ChildElementInfo(typeof(WrapThrough))]
	[ChildElementInfo(typeof(DocProperties))]
	[ChildElementInfo(typeof(NonVisualGraphicFrameDrawingProperties))]
	[ChildElementInfo(typeof(RelativeWidth), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(RelativeHeight), FileFormatVersions.Office2010)]
	internal class Anchor : OpenXmlCompositeElement
	{
		// Token: 0x1700684B RID: 26699
		// (get) Token: 0x06014791 RID: 83857 RVA: 0x0030B3F0 File Offset: 0x003095F0
		public override string LocalName
		{
			get
			{
				return "anchor";
			}
		}

		// Token: 0x1700684C RID: 26700
		// (get) Token: 0x06014792 RID: 83858 RVA: 0x00227072 File Offset: 0x00225272
		internal override byte NamespaceId
		{
			get
			{
				return 16;
			}
		}

		// Token: 0x1700684D RID: 26701
		// (get) Token: 0x06014793 RID: 83859 RVA: 0x00313A40 File Offset: 0x00311C40
		internal override int ElementTypeId
		{
			get
			{
				return 10700;
			}
		}

		// Token: 0x06014794 RID: 83860 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x1700684E RID: 26702
		// (get) Token: 0x06014795 RID: 83861 RVA: 0x00313A47 File Offset: 0x00311C47
		internal override string[] AttributeTagNames
		{
			get
			{
				return Anchor.attributeTagNames;
			}
		}

		// Token: 0x1700684F RID: 26703
		// (get) Token: 0x06014796 RID: 83862 RVA: 0x00313A4E File Offset: 0x00311C4E
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Anchor.attributeNamespaceIds;
			}
		}

		// Token: 0x17006850 RID: 26704
		// (get) Token: 0x06014797 RID: 83863 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x06014798 RID: 83864 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "distT")]
		public UInt32Value DistanceFromTop
		{
			get
			{
				return (UInt32Value)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17006851 RID: 26705
		// (get) Token: 0x06014799 RID: 83865 RVA: 0x002E33EC File Offset: 0x002E15EC
		// (set) Token: 0x0601479A RID: 83866 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "distB")]
		public UInt32Value DistanceFromBottom
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

		// Token: 0x17006852 RID: 26706
		// (get) Token: 0x0601479B RID: 83867 RVA: 0x002E5814 File Offset: 0x002E3A14
		// (set) Token: 0x0601479C RID: 83868 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "distL")]
		public UInt32Value DistanceFromLeft
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

		// Token: 0x17006853 RID: 26707
		// (get) Token: 0x0601479D RID: 83869 RVA: 0x002E5B0D File Offset: 0x002E3D0D
		// (set) Token: 0x0601479E RID: 83870 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "distR")]
		public UInt32Value DistanceFromRight
		{
			get
			{
				return (UInt32Value)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x17006854 RID: 26708
		// (get) Token: 0x0601479F RID: 83871 RVA: 0x002CBE2D File Offset: 0x002CA02D
		// (set) Token: 0x060147A0 RID: 83872 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "simplePos")]
		public BooleanValue SimplePos
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

		// Token: 0x17006855 RID: 26709
		// (get) Token: 0x060147A1 RID: 83873 RVA: 0x002E6EEB File Offset: 0x002E50EB
		// (set) Token: 0x060147A2 RID: 83874 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "relativeHeight")]
		public UInt32Value RelativeHeight
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

		// Token: 0x17006856 RID: 26710
		// (get) Token: 0x060147A3 RID: 83875 RVA: 0x002CCC33 File Offset: 0x002CAE33
		// (set) Token: 0x060147A4 RID: 83876 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "behindDoc")]
		public BooleanValue BehindDoc
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

		// Token: 0x17006857 RID: 26711
		// (get) Token: 0x060147A5 RID: 83877 RVA: 0x002CA736 File Offset: 0x002C8936
		// (set) Token: 0x060147A6 RID: 83878 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(0, "locked")]
		public BooleanValue Locked
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

		// Token: 0x17006858 RID: 26712
		// (get) Token: 0x060147A7 RID: 83879 RVA: 0x002CB706 File Offset: 0x002C9906
		// (set) Token: 0x060147A8 RID: 83880 RVA: 0x002BD530 File Offset: 0x002BB730
		[SchemaAttr(0, "layoutInCell")]
		public BooleanValue LayoutInCell
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

		// Token: 0x17006859 RID: 26713
		// (get) Token: 0x060147A9 RID: 83881 RVA: 0x002C92EA File Offset: 0x002C74EA
		// (set) Token: 0x060147AA RID: 83882 RVA: 0x002BD54B File Offset: 0x002BB74B
		[SchemaAttr(0, "hidden")]
		public BooleanValue Hidden
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

		// Token: 0x1700685A RID: 26714
		// (get) Token: 0x060147AB RID: 83883 RVA: 0x002C8762 File Offset: 0x002C6962
		// (set) Token: 0x060147AC RID: 83884 RVA: 0x002BDB45 File Offset: 0x002BBD45
		[SchemaAttr(0, "allowOverlap")]
		public BooleanValue AllowOverlap
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

		// Token: 0x1700685B RID: 26715
		// (get) Token: 0x060147AD RID: 83885 RVA: 0x00313A55 File Offset: 0x00311C55
		// (set) Token: 0x060147AE RID: 83886 RVA: 0x002BDB61 File Offset: 0x002BBD61
		[OfficeAvailability(FileFormatVersions.Office2010)]
		[SchemaAttr(51, "editId")]
		public HexBinaryValue EditId
		{
			get
			{
				return (HexBinaryValue)base.Attributes[11];
			}
			set
			{
				base.Attributes[11] = value;
			}
		}

		// Token: 0x1700685C RID: 26716
		// (get) Token: 0x060147AF RID: 83887 RVA: 0x00313A65 File Offset: 0x00311C65
		// (set) Token: 0x060147B0 RID: 83888 RVA: 0x002BDB7D File Offset: 0x002BBD7D
		[OfficeAvailability(FileFormatVersions.Office2010)]
		[SchemaAttr(51, "anchorId")]
		public HexBinaryValue AnchorId
		{
			get
			{
				return (HexBinaryValue)base.Attributes[12];
			}
			set
			{
				base.Attributes[12] = value;
			}
		}

		// Token: 0x060147B1 RID: 83889 RVA: 0x00293ECF File Offset: 0x002920CF
		public Anchor()
		{
		}

		// Token: 0x060147B2 RID: 83890 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Anchor(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060147B3 RID: 83891 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Anchor(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060147B4 RID: 83892 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Anchor(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060147B5 RID: 83893 RVA: 0x00313A78 File Offset: 0x00311C78
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (16 == namespaceId && "simplePos" == name)
			{
				return new SimplePosition();
			}
			if (16 == namespaceId && "positionH" == name)
			{
				return new HorizontalPosition();
			}
			if (16 == namespaceId && "positionV" == name)
			{
				return new VerticalPosition();
			}
			if (16 == namespaceId && "extent" == name)
			{
				return new Extent();
			}
			if (16 == namespaceId && "effectExtent" == name)
			{
				return new EffectExtent();
			}
			if (16 == namespaceId && "wrapNone" == name)
			{
				return new WrapNone();
			}
			if (16 == namespaceId && "wrapSquare" == name)
			{
				return new WrapSquare();
			}
			if (16 == namespaceId && "wrapTight" == name)
			{
				return new WrapTight();
			}
			if (16 == namespaceId && "wrapThrough" == name)
			{
				return new WrapThrough();
			}
			if (16 == namespaceId && "wrapTopAndBottom" == name)
			{
				return new WrapTopBottom();
			}
			if (16 == namespaceId && "docPr" == name)
			{
				return new DocProperties();
			}
			if (16 == namespaceId && "cNvGraphicFramePr" == name)
			{
				return new NonVisualGraphicFrameDrawingProperties();
			}
			if (10 == namespaceId && "graphic" == name)
			{
				return new Graphic();
			}
			if (51 == namespaceId && "sizeRelH" == name)
			{
				return new RelativeWidth();
			}
			if (51 == namespaceId && "sizeRelV" == name)
			{
				return new RelativeHeight();
			}
			return null;
		}

		// Token: 0x1700685D RID: 26717
		// (get) Token: 0x060147B6 RID: 83894 RVA: 0x00313BEE File Offset: 0x00311DEE
		internal override string[] ElementTagNames
		{
			get
			{
				return Anchor.eleTagNames;
			}
		}

		// Token: 0x1700685E RID: 26718
		// (get) Token: 0x060147B7 RID: 83895 RVA: 0x00313BF5 File Offset: 0x00311DF5
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return Anchor.eleNamespaceIds;
			}
		}

		// Token: 0x1700685F RID: 26719
		// (get) Token: 0x060147B8 RID: 83896 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17006860 RID: 26720
		// (get) Token: 0x060147B9 RID: 83897 RVA: 0x00313BFC File Offset: 0x00311DFC
		// (set) Token: 0x060147BA RID: 83898 RVA: 0x00313C05 File Offset: 0x00311E05
		public SimplePosition SimplePosition
		{
			get
			{
				return base.GetElement<SimplePosition>(0);
			}
			set
			{
				base.SetElement<SimplePosition>(0, value);
			}
		}

		// Token: 0x17006861 RID: 26721
		// (get) Token: 0x060147BB RID: 83899 RVA: 0x00313C0F File Offset: 0x00311E0F
		// (set) Token: 0x060147BC RID: 83900 RVA: 0x00313C18 File Offset: 0x00311E18
		public HorizontalPosition HorizontalPosition
		{
			get
			{
				return base.GetElement<HorizontalPosition>(1);
			}
			set
			{
				base.SetElement<HorizontalPosition>(1, value);
			}
		}

		// Token: 0x17006862 RID: 26722
		// (get) Token: 0x060147BD RID: 83901 RVA: 0x00313C22 File Offset: 0x00311E22
		// (set) Token: 0x060147BE RID: 83902 RVA: 0x00313C2B File Offset: 0x00311E2B
		public VerticalPosition VerticalPosition
		{
			get
			{
				return base.GetElement<VerticalPosition>(2);
			}
			set
			{
				base.SetElement<VerticalPosition>(2, value);
			}
		}

		// Token: 0x17006863 RID: 26723
		// (get) Token: 0x060147BF RID: 83903 RVA: 0x00313C35 File Offset: 0x00311E35
		// (set) Token: 0x060147C0 RID: 83904 RVA: 0x00313C3E File Offset: 0x00311E3E
		public Extent Extent
		{
			get
			{
				return base.GetElement<Extent>(3);
			}
			set
			{
				base.SetElement<Extent>(3, value);
			}
		}

		// Token: 0x17006864 RID: 26724
		// (get) Token: 0x060147C1 RID: 83905 RVA: 0x00313C48 File Offset: 0x00311E48
		// (set) Token: 0x060147C2 RID: 83906 RVA: 0x00313C51 File Offset: 0x00311E51
		public EffectExtent EffectExtent
		{
			get
			{
				return base.GetElement<EffectExtent>(4);
			}
			set
			{
				base.SetElement<EffectExtent>(4, value);
			}
		}

		// Token: 0x060147C3 RID: 83907 RVA: 0x00313C5C File Offset: 0x00311E5C
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "distT" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "distB" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "distL" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "distR" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "simplePos" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "relativeHeight" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "behindDoc" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "locked" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "layoutInCell" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "hidden" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "allowOverlap" == name)
			{
				return new BooleanValue();
			}
			if (51 == namespaceId && "editId" == name)
			{
				return new HexBinaryValue();
			}
			if (51 == namespaceId && "anchorId" == name)
			{
				return new HexBinaryValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060147C4 RID: 83908 RVA: 0x00313D93 File Offset: 0x00311F93
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Anchor>(deep);
		}

		// Token: 0x04008E43 RID: 36419
		private const string tagName = "anchor";

		// Token: 0x04008E44 RID: 36420
		private const byte tagNsId = 16;

		// Token: 0x04008E45 RID: 36421
		internal const int ElementTypeIdConst = 10700;

		// Token: 0x04008E46 RID: 36422
		private static string[] attributeTagNames = new string[]
		{
			"distT", "distB", "distL", "distR", "simplePos", "relativeHeight", "behindDoc", "locked", "layoutInCell", "hidden",
			"allowOverlap", "editId", "anchorId"
		};

		// Token: 0x04008E47 RID: 36423
		private static byte[] attributeNamespaceIds = new byte[]
		{
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 51, 51
		};

		// Token: 0x04008E48 RID: 36424
		private static readonly string[] eleTagNames = new string[]
		{
			"simplePos", "positionH", "positionV", "extent", "effectExtent", "wrapNone", "wrapSquare", "wrapTight", "wrapThrough", "wrapTopAndBottom",
			"docPr", "cNvGraphicFramePr", "graphic", "sizeRelH", "sizeRelV"
		};

		// Token: 0x04008E49 RID: 36425
		private static readonly byte[] eleNamespaceIds = new byte[]
		{
			16, 16, 16, 16, 16, 16, 16, 16, 16, 16,
			16, 16, 10, 51, 51
		};
	}
}
