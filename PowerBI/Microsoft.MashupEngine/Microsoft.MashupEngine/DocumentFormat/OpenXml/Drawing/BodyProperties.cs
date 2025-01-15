using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x020027DE RID: 10206
	[ChildElementInfo(typeof(NoAutoFit))]
	[ChildElementInfo(typeof(PresetTextWrap))]
	[ChildElementInfo(typeof(Scene3DType))]
	[ChildElementInfo(typeof(NormalAutoFit))]
	[ChildElementInfo(typeof(ShapeAutoFit))]
	[ChildElementInfo(typeof(FlatText))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(Shape3DType))]
	[ChildElementInfo(typeof(ExtensionList))]
	internal class BodyProperties : OpenXmlCompositeElement
	{
		// Token: 0x17006414 RID: 25620
		// (get) Token: 0x06013DC7 RID: 81351 RVA: 0x002F090E File Offset: 0x002EEB0E
		public override string LocalName
		{
			get
			{
				return "bodyPr";
			}
		}

		// Token: 0x17006415 RID: 25621
		// (get) Token: 0x06013DC8 RID: 81352 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17006416 RID: 25622
		// (get) Token: 0x06013DC9 RID: 81353 RVA: 0x0030C6AA File Offset: 0x0030A8AA
		internal override int ElementTypeId
		{
			get
			{
				return 10239;
			}
		}

		// Token: 0x06013DCA RID: 81354 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17006417 RID: 25623
		// (get) Token: 0x06013DCB RID: 81355 RVA: 0x0030C6B1 File Offset: 0x0030A8B1
		internal override string[] AttributeTagNames
		{
			get
			{
				return BodyProperties.attributeTagNames;
			}
		}

		// Token: 0x17006418 RID: 25624
		// (get) Token: 0x06013DCC RID: 81356 RVA: 0x0030C6B8 File Offset: 0x0030A8B8
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return BodyProperties.attributeNamespaceIds;
			}
		}

		// Token: 0x17006419 RID: 25625
		// (get) Token: 0x06013DCD RID: 81357 RVA: 0x002BF6A0 File Offset: 0x002BD8A0
		// (set) Token: 0x06013DCE RID: 81358 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "rot")]
		public Int32Value Rotation
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

		// Token: 0x1700641A RID: 25626
		// (get) Token: 0x06013DCF RID: 81359 RVA: 0x002C8B36 File Offset: 0x002C6D36
		// (set) Token: 0x06013DD0 RID: 81360 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "spcFirstLastPara")]
		public BooleanValue UseParagraphSpacing
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

		// Token: 0x1700641B RID: 25627
		// (get) Token: 0x06013DD1 RID: 81361 RVA: 0x002F092A File Offset: 0x002EEB2A
		// (set) Token: 0x06013DD2 RID: 81362 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "vertOverflow")]
		public EnumValue<TextVerticalOverflowValues> VerticalOverflow
		{
			get
			{
				return (EnumValue<TextVerticalOverflowValues>)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x1700641C RID: 25628
		// (get) Token: 0x06013DD3 RID: 81363 RVA: 0x002F0939 File Offset: 0x002EEB39
		// (set) Token: 0x06013DD4 RID: 81364 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "horzOverflow")]
		public EnumValue<TextHorizontalOverflowValues> HorizontalOverflow
		{
			get
			{
				return (EnumValue<TextHorizontalOverflowValues>)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x1700641D RID: 25629
		// (get) Token: 0x06013DD5 RID: 81365 RVA: 0x002F0948 File Offset: 0x002EEB48
		// (set) Token: 0x06013DD6 RID: 81366 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "vert")]
		public EnumValue<TextVerticalValues> Vertical
		{
			get
			{
				return (EnumValue<TextVerticalValues>)base.Attributes[4];
			}
			set
			{
				base.Attributes[4] = value;
			}
		}

		// Token: 0x1700641E RID: 25630
		// (get) Token: 0x06013DD7 RID: 81367 RVA: 0x002F0957 File Offset: 0x002EEB57
		// (set) Token: 0x06013DD8 RID: 81368 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "wrap")]
		public EnumValue<TextWrappingValues> Wrap
		{
			get
			{
				return (EnumValue<TextWrappingValues>)base.Attributes[5];
			}
			set
			{
				base.Attributes[5] = value;
			}
		}

		// Token: 0x1700641F RID: 25631
		// (get) Token: 0x06013DD9 RID: 81369 RVA: 0x002ED380 File Offset: 0x002EB580
		// (set) Token: 0x06013DDA RID: 81370 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "lIns")]
		public Int32Value LeftInset
		{
			get
			{
				return (Int32Value)base.Attributes[6];
			}
			set
			{
				base.Attributes[6] = value;
			}
		}

		// Token: 0x17006420 RID: 25632
		// (get) Token: 0x06013DDB RID: 81371 RVA: 0x002D14EB File Offset: 0x002CF6EB
		// (set) Token: 0x06013DDC RID: 81372 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(0, "tIns")]
		public Int32Value TopInset
		{
			get
			{
				return (Int32Value)base.Attributes[7];
			}
			set
			{
				base.Attributes[7] = value;
			}
		}

		// Token: 0x17006421 RID: 25633
		// (get) Token: 0x06013DDD RID: 81373 RVA: 0x002ED55B File Offset: 0x002EB75B
		// (set) Token: 0x06013DDE RID: 81374 RVA: 0x002BD530 File Offset: 0x002BB730
		[SchemaAttr(0, "rIns")]
		public Int32Value RightInset
		{
			get
			{
				return (Int32Value)base.Attributes[8];
			}
			set
			{
				base.Attributes[8] = value;
			}
		}

		// Token: 0x17006422 RID: 25634
		// (get) Token: 0x06013DDF RID: 81375 RVA: 0x002D14FA File Offset: 0x002CF6FA
		// (set) Token: 0x06013DE0 RID: 81376 RVA: 0x002BD54B File Offset: 0x002BB74B
		[SchemaAttr(0, "bIns")]
		public Int32Value BottomInset
		{
			get
			{
				return (Int32Value)base.Attributes[9];
			}
			set
			{
				base.Attributes[9] = value;
			}
		}

		// Token: 0x17006423 RID: 25635
		// (get) Token: 0x06013DE1 RID: 81377 RVA: 0x002E7730 File Offset: 0x002E5930
		// (set) Token: 0x06013DE2 RID: 81378 RVA: 0x002BDB45 File Offset: 0x002BBD45
		[SchemaAttr(0, "numCol")]
		public Int32Value ColumnCount
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

		// Token: 0x17006424 RID: 25636
		// (get) Token: 0x06013DE3 RID: 81379 RVA: 0x002ED56A File Offset: 0x002EB76A
		// (set) Token: 0x06013DE4 RID: 81380 RVA: 0x002BDB61 File Offset: 0x002BBD61
		[SchemaAttr(0, "spcCol")]
		public Int32Value ColumnSpacing
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

		// Token: 0x17006425 RID: 25637
		// (get) Token: 0x06013DE5 RID: 81381 RVA: 0x002CE196 File Offset: 0x002CC396
		// (set) Token: 0x06013DE6 RID: 81382 RVA: 0x002BDB7D File Offset: 0x002BBD7D
		[SchemaAttr(0, "rtlCol")]
		public BooleanValue RightToLeftColumns
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

		// Token: 0x17006426 RID: 25638
		// (get) Token: 0x06013DE7 RID: 81383 RVA: 0x002CD15F File Offset: 0x002CB35F
		// (set) Token: 0x06013DE8 RID: 81384 RVA: 0x002BE209 File Offset: 0x002BC409
		[SchemaAttr(0, "fromWordArt")]
		public BooleanValue FromWordArt
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

		// Token: 0x17006427 RID: 25639
		// (get) Token: 0x06013DE9 RID: 81385 RVA: 0x002F0966 File Offset: 0x002EEB66
		// (set) Token: 0x06013DEA RID: 81386 RVA: 0x002BE225 File Offset: 0x002BC425
		[SchemaAttr(0, "anchor")]
		public EnumValue<TextAnchoringTypeValues> Anchor
		{
			get
			{
				return (EnumValue<TextAnchoringTypeValues>)base.Attributes[14];
			}
			set
			{
				base.Attributes[14] = value;
			}
		}

		// Token: 0x17006428 RID: 25640
		// (get) Token: 0x06013DEB RID: 81387 RVA: 0x002CA745 File Offset: 0x002C8945
		// (set) Token: 0x06013DEC RID: 81388 RVA: 0x002BE241 File Offset: 0x002BC441
		[SchemaAttr(0, "anchorCtr")]
		public BooleanValue AnchorCenter
		{
			get
			{
				return (BooleanValue)base.Attributes[15];
			}
			set
			{
				base.Attributes[15] = value;
			}
		}

		// Token: 0x17006429 RID: 25641
		// (get) Token: 0x06013DED RID: 81389 RVA: 0x002C930A File Offset: 0x002C750A
		// (set) Token: 0x06013DEE RID: 81390 RVA: 0x002BE25D File Offset: 0x002BC45D
		[SchemaAttr(0, "forceAA")]
		public BooleanValue ForceAntiAlias
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

		// Token: 0x1700642A RID: 25642
		// (get) Token: 0x06013DEF RID: 81391 RVA: 0x002CDD31 File Offset: 0x002CBF31
		// (set) Token: 0x06013DF0 RID: 81392 RVA: 0x002BE279 File Offset: 0x002BC479
		[SchemaAttr(0, "upright")]
		public BooleanValue UpRight
		{
			get
			{
				return (BooleanValue)base.Attributes[17];
			}
			set
			{
				base.Attributes[17] = value;
			}
		}

		// Token: 0x1700642B RID: 25643
		// (get) Token: 0x06013DF1 RID: 81393 RVA: 0x002C8772 File Offset: 0x002C6972
		// (set) Token: 0x06013DF2 RID: 81394 RVA: 0x002BE295 File Offset: 0x002BC495
		[SchemaAttr(0, "compatLnSpc")]
		public BooleanValue CompatibleLineSpacing
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

		// Token: 0x06013DF3 RID: 81395 RVA: 0x00293ECF File Offset: 0x002920CF
		public BodyProperties()
		{
		}

		// Token: 0x06013DF4 RID: 81396 RVA: 0x00293ED7 File Offset: 0x002920D7
		public BodyProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013DF5 RID: 81397 RVA: 0x00293EE0 File Offset: 0x002920E0
		public BodyProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013DF6 RID: 81398 RVA: 0x00293EE9 File Offset: 0x002920E9
		public BodyProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06013DF7 RID: 81399 RVA: 0x0030C6C0 File Offset: 0x0030A8C0
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (10 == namespaceId && "prstTxWarp" == name)
			{
				return new PresetTextWrap();
			}
			if (10 == namespaceId && "noAutofit" == name)
			{
				return new NoAutoFit();
			}
			if (10 == namespaceId && "normAutofit" == name)
			{
				return new NormalAutoFit();
			}
			if (10 == namespaceId && "spAutoFit" == name)
			{
				return new ShapeAutoFit();
			}
			if (10 == namespaceId && "scene3d" == name)
			{
				return new Scene3DType();
			}
			if (10 == namespaceId && "sp3d" == name)
			{
				return new Shape3DType();
			}
			if (10 == namespaceId && "flatTx" == name)
			{
				return new FlatText();
			}
			if (10 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x1700642C RID: 25644
		// (get) Token: 0x06013DF8 RID: 81400 RVA: 0x0030C78E File Offset: 0x0030A98E
		internal override string[] ElementTagNames
		{
			get
			{
				return BodyProperties.eleTagNames;
			}
		}

		// Token: 0x1700642D RID: 25645
		// (get) Token: 0x06013DF9 RID: 81401 RVA: 0x0030C795 File Offset: 0x0030A995
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return BodyProperties.eleNamespaceIds;
			}
		}

		// Token: 0x1700642E RID: 25646
		// (get) Token: 0x06013DFA RID: 81402 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x1700642F RID: 25647
		// (get) Token: 0x06013DFB RID: 81403 RVA: 0x002F0A54 File Offset: 0x002EEC54
		// (set) Token: 0x06013DFC RID: 81404 RVA: 0x002F0A5D File Offset: 0x002EEC5D
		public PresetTextWrap PresetTextWrap
		{
			get
			{
				return base.GetElement<PresetTextWrap>(0);
			}
			set
			{
				base.SetElement<PresetTextWrap>(0, value);
			}
		}

		// Token: 0x06013DFD RID: 81405 RVA: 0x0030C79C File Offset: 0x0030A99C
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "rot" == name)
			{
				return new Int32Value();
			}
			if (namespaceId == 0 && "spcFirstLastPara" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "vertOverflow" == name)
			{
				return new EnumValue<TextVerticalOverflowValues>();
			}
			if (namespaceId == 0 && "horzOverflow" == name)
			{
				return new EnumValue<TextHorizontalOverflowValues>();
			}
			if (namespaceId == 0 && "vert" == name)
			{
				return new EnumValue<TextVerticalValues>();
			}
			if (namespaceId == 0 && "wrap" == name)
			{
				return new EnumValue<TextWrappingValues>();
			}
			if (namespaceId == 0 && "lIns" == name)
			{
				return new Int32Value();
			}
			if (namespaceId == 0 && "tIns" == name)
			{
				return new Int32Value();
			}
			if (namespaceId == 0 && "rIns" == name)
			{
				return new Int32Value();
			}
			if (namespaceId == 0 && "bIns" == name)
			{
				return new Int32Value();
			}
			if (namespaceId == 0 && "numCol" == name)
			{
				return new Int32Value();
			}
			if (namespaceId == 0 && "spcCol" == name)
			{
				return new Int32Value();
			}
			if (namespaceId == 0 && "rtlCol" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "fromWordArt" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "anchor" == name)
			{
				return new EnumValue<TextAnchoringTypeValues>();
			}
			if (namespaceId == 0 && "anchorCtr" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "forceAA" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "upright" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "compatLnSpc" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06013DFE RID: 81406 RVA: 0x0030C953 File Offset: 0x0030AB53
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<BodyProperties>(deep);
		}

		// Token: 0x06013DFF RID: 81407 RVA: 0x0030C95C File Offset: 0x0030AB5C
		// Note: this type is marked as 'beforefieldinit'.
		static BodyProperties()
		{
			byte[] array = new byte[19];
			BodyProperties.attributeNamespaceIds = array;
			BodyProperties.eleTagNames = new string[] { "prstTxWarp", "noAutofit", "normAutofit", "spAutoFit", "scene3d", "sp3d", "flatTx", "extLst" };
			BodyProperties.eleNamespaceIds = new byte[] { 10, 10, 10, 10, 10, 10, 10, 10 };
		}

		// Token: 0x0400881E RID: 34846
		private const string tagName = "bodyPr";

		// Token: 0x0400881F RID: 34847
		private const byte tagNsId = 10;

		// Token: 0x04008820 RID: 34848
		internal const int ElementTypeIdConst = 10239;

		// Token: 0x04008821 RID: 34849
		private static string[] attributeTagNames = new string[]
		{
			"rot", "spcFirstLastPara", "vertOverflow", "horzOverflow", "vert", "wrap", "lIns", "tIns", "rIns", "bIns",
			"numCol", "spcCol", "rtlCol", "fromWordArt", "anchor", "anchorCtr", "forceAA", "upright", "compatLnSpc"
		};

		// Token: 0x04008822 RID: 34850
		private static byte[] attributeNamespaceIds;

		// Token: 0x04008823 RID: 34851
		private static readonly string[] eleTagNames;

		// Token: 0x04008824 RID: 34852
		private static readonly byte[] eleNamespaceIds;
	}
}
