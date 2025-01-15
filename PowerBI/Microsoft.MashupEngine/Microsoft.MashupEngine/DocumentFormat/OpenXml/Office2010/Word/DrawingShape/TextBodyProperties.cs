using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Drawing;

namespace DocumentFormat.OpenXml.Office2010.Word.DrawingShape
{
	// Token: 0x02002502 RID: 9474
	[ChildElementInfo(typeof(Scene3DType))]
	[ChildElementInfo(typeof(NoAutoFit))]
	[ChildElementInfo(typeof(NormalAutoFit))]
	[ChildElementInfo(typeof(ShapeAutoFit))]
	[ChildElementInfo(typeof(PresetTextWrap))]
	[ChildElementInfo(typeof(Shape3DType))]
	[ChildElementInfo(typeof(FlatText))]
	[ChildElementInfo(typeof(ExtensionList))]
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class TextBodyProperties : OpenXmlCompositeElement
	{
		// Token: 0x170053F6 RID: 21494
		// (get) Token: 0x060119DB RID: 72155 RVA: 0x002F090E File Offset: 0x002EEB0E
		public override string LocalName
		{
			get
			{
				return "bodyPr";
			}
		}

		// Token: 0x170053F7 RID: 21495
		// (get) Token: 0x060119DC RID: 72156 RVA: 0x002EFE53 File Offset: 0x002EE053
		internal override byte NamespaceId
		{
			get
			{
				return 61;
			}
		}

		// Token: 0x170053F8 RID: 21496
		// (get) Token: 0x060119DD RID: 72157 RVA: 0x002F0915 File Offset: 0x002EEB15
		internal override int ElementTypeId
		{
			get
			{
				return 13140;
			}
		}

		// Token: 0x060119DE RID: 72158 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x170053F9 RID: 21497
		// (get) Token: 0x060119DF RID: 72159 RVA: 0x002F091C File Offset: 0x002EEB1C
		internal override string[] AttributeTagNames
		{
			get
			{
				return TextBodyProperties.attributeTagNames;
			}
		}

		// Token: 0x170053FA RID: 21498
		// (get) Token: 0x060119E0 RID: 72160 RVA: 0x002F0923 File Offset: 0x002EEB23
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return TextBodyProperties.attributeNamespaceIds;
			}
		}

		// Token: 0x170053FB RID: 21499
		// (get) Token: 0x060119E1 RID: 72161 RVA: 0x002BF6A0 File Offset: 0x002BD8A0
		// (set) Token: 0x060119E2 RID: 72162 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x170053FC RID: 21500
		// (get) Token: 0x060119E3 RID: 72163 RVA: 0x002C8B36 File Offset: 0x002C6D36
		// (set) Token: 0x060119E4 RID: 72164 RVA: 0x002BD47A File Offset: 0x002BB67A
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

		// Token: 0x170053FD RID: 21501
		// (get) Token: 0x060119E5 RID: 72165 RVA: 0x002F092A File Offset: 0x002EEB2A
		// (set) Token: 0x060119E6 RID: 72166 RVA: 0x002BD494 File Offset: 0x002BB694
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

		// Token: 0x170053FE RID: 21502
		// (get) Token: 0x060119E7 RID: 72167 RVA: 0x002F0939 File Offset: 0x002EEB39
		// (set) Token: 0x060119E8 RID: 72168 RVA: 0x002BD4AE File Offset: 0x002BB6AE
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

		// Token: 0x170053FF RID: 21503
		// (get) Token: 0x060119E9 RID: 72169 RVA: 0x002F0948 File Offset: 0x002EEB48
		// (set) Token: 0x060119EA RID: 72170 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
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

		// Token: 0x17005400 RID: 21504
		// (get) Token: 0x060119EB RID: 72171 RVA: 0x002F0957 File Offset: 0x002EEB57
		// (set) Token: 0x060119EC RID: 72172 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
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

		// Token: 0x17005401 RID: 21505
		// (get) Token: 0x060119ED RID: 72173 RVA: 0x002ED380 File Offset: 0x002EB580
		// (set) Token: 0x060119EE RID: 72174 RVA: 0x002BD4FC File Offset: 0x002BB6FC
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

		// Token: 0x17005402 RID: 21506
		// (get) Token: 0x060119EF RID: 72175 RVA: 0x002D14EB File Offset: 0x002CF6EB
		// (set) Token: 0x060119F0 RID: 72176 RVA: 0x002BD516 File Offset: 0x002BB716
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

		// Token: 0x17005403 RID: 21507
		// (get) Token: 0x060119F1 RID: 72177 RVA: 0x002ED55B File Offset: 0x002EB75B
		// (set) Token: 0x060119F2 RID: 72178 RVA: 0x002BD530 File Offset: 0x002BB730
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

		// Token: 0x17005404 RID: 21508
		// (get) Token: 0x060119F3 RID: 72179 RVA: 0x002D14FA File Offset: 0x002CF6FA
		// (set) Token: 0x060119F4 RID: 72180 RVA: 0x002BD54B File Offset: 0x002BB74B
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

		// Token: 0x17005405 RID: 21509
		// (get) Token: 0x060119F5 RID: 72181 RVA: 0x002E7730 File Offset: 0x002E5930
		// (set) Token: 0x060119F6 RID: 72182 RVA: 0x002BDB45 File Offset: 0x002BBD45
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

		// Token: 0x17005406 RID: 21510
		// (get) Token: 0x060119F7 RID: 72183 RVA: 0x002ED56A File Offset: 0x002EB76A
		// (set) Token: 0x060119F8 RID: 72184 RVA: 0x002BDB61 File Offset: 0x002BBD61
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

		// Token: 0x17005407 RID: 21511
		// (get) Token: 0x060119F9 RID: 72185 RVA: 0x002CE196 File Offset: 0x002CC396
		// (set) Token: 0x060119FA RID: 72186 RVA: 0x002BDB7D File Offset: 0x002BBD7D
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

		// Token: 0x17005408 RID: 21512
		// (get) Token: 0x060119FB RID: 72187 RVA: 0x002CD15F File Offset: 0x002CB35F
		// (set) Token: 0x060119FC RID: 72188 RVA: 0x002BE209 File Offset: 0x002BC409
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

		// Token: 0x17005409 RID: 21513
		// (get) Token: 0x060119FD RID: 72189 RVA: 0x002F0966 File Offset: 0x002EEB66
		// (set) Token: 0x060119FE RID: 72190 RVA: 0x002BE225 File Offset: 0x002BC425
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

		// Token: 0x1700540A RID: 21514
		// (get) Token: 0x060119FF RID: 72191 RVA: 0x002CA745 File Offset: 0x002C8945
		// (set) Token: 0x06011A00 RID: 72192 RVA: 0x002BE241 File Offset: 0x002BC441
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

		// Token: 0x1700540B RID: 21515
		// (get) Token: 0x06011A01 RID: 72193 RVA: 0x002C930A File Offset: 0x002C750A
		// (set) Token: 0x06011A02 RID: 72194 RVA: 0x002BE25D File Offset: 0x002BC45D
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

		// Token: 0x1700540C RID: 21516
		// (get) Token: 0x06011A03 RID: 72195 RVA: 0x002CDD31 File Offset: 0x002CBF31
		// (set) Token: 0x06011A04 RID: 72196 RVA: 0x002BE279 File Offset: 0x002BC479
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

		// Token: 0x1700540D RID: 21517
		// (get) Token: 0x06011A05 RID: 72197 RVA: 0x002C8772 File Offset: 0x002C6972
		// (set) Token: 0x06011A06 RID: 72198 RVA: 0x002BE295 File Offset: 0x002BC495
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

		// Token: 0x06011A07 RID: 72199 RVA: 0x00293ECF File Offset: 0x002920CF
		public TextBodyProperties()
		{
		}

		// Token: 0x06011A08 RID: 72200 RVA: 0x00293ED7 File Offset: 0x002920D7
		public TextBodyProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06011A09 RID: 72201 RVA: 0x00293EE0 File Offset: 0x002920E0
		public TextBodyProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06011A0A RID: 72202 RVA: 0x00293EE9 File Offset: 0x002920E9
		public TextBodyProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06011A0B RID: 72203 RVA: 0x002F0978 File Offset: 0x002EEB78
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

		// Token: 0x1700540E RID: 21518
		// (get) Token: 0x06011A0C RID: 72204 RVA: 0x002F0A46 File Offset: 0x002EEC46
		internal override string[] ElementTagNames
		{
			get
			{
				return TextBodyProperties.eleTagNames;
			}
		}

		// Token: 0x1700540F RID: 21519
		// (get) Token: 0x06011A0D RID: 72205 RVA: 0x002F0A4D File Offset: 0x002EEC4D
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return TextBodyProperties.eleNamespaceIds;
			}
		}

		// Token: 0x17005410 RID: 21520
		// (get) Token: 0x06011A0E RID: 72206 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17005411 RID: 21521
		// (get) Token: 0x06011A0F RID: 72207 RVA: 0x002F0A54 File Offset: 0x002EEC54
		// (set) Token: 0x06011A10 RID: 72208 RVA: 0x002F0A5D File Offset: 0x002EEC5D
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

		// Token: 0x06011A11 RID: 72209 RVA: 0x002F0A68 File Offset: 0x002EEC68
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

		// Token: 0x06011A12 RID: 72210 RVA: 0x002F0C1F File Offset: 0x002EEE1F
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<TextBodyProperties>(deep);
		}

		// Token: 0x06011A13 RID: 72211 RVA: 0x002F0C28 File Offset: 0x002EEE28
		// Note: this type is marked as 'beforefieldinit'.
		static TextBodyProperties()
		{
			byte[] array = new byte[19];
			TextBodyProperties.attributeNamespaceIds = array;
			TextBodyProperties.eleTagNames = new string[] { "prstTxWarp", "noAutofit", "normAutofit", "spAutoFit", "scene3d", "sp3d", "flatTx", "extLst" };
			TextBodyProperties.eleNamespaceIds = new byte[] { 10, 10, 10, 10, 10, 10, 10, 10 };
		}

		// Token: 0x04007B90 RID: 31632
		private const string tagName = "bodyPr";

		// Token: 0x04007B91 RID: 31633
		private const byte tagNsId = 61;

		// Token: 0x04007B92 RID: 31634
		internal const int ElementTypeIdConst = 13140;

		// Token: 0x04007B93 RID: 31635
		private static string[] attributeTagNames = new string[]
		{
			"rot", "spcFirstLastPara", "vertOverflow", "horzOverflow", "vert", "wrap", "lIns", "tIns", "rIns", "bIns",
			"numCol", "spcCol", "rtlCol", "fromWordArt", "anchor", "anchorCtr", "forceAA", "upright", "compatLnSpc"
		};

		// Token: 0x04007B94 RID: 31636
		private static byte[] attributeNamespaceIds;

		// Token: 0x04007B95 RID: 31637
		private static readonly string[] eleTagNames;

		// Token: 0x04007B96 RID: 31638
		private static readonly byte[] eleNamespaceIds;
	}
}
