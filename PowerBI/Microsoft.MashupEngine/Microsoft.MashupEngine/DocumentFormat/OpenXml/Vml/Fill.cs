using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Vml.Office;

namespace DocumentFormat.OpenXml.Vml
{
	// Token: 0x02002245 RID: 8773
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(FillExtendedProperties))]
	internal class Fill : OpenXmlCompositeElement
	{
		// Token: 0x17003995 RID: 14741
		// (get) Token: 0x0600E0E2 RID: 57570 RVA: 0x002BF458 File Offset: 0x002BD658
		public override string LocalName
		{
			get
			{
				return "fill";
			}
		}

		// Token: 0x17003996 RID: 14742
		// (get) Token: 0x0600E0E3 RID: 57571 RVA: 0x00243C87 File Offset: 0x00241E87
		internal override byte NamespaceId
		{
			get
			{
				return 26;
			}
		}

		// Token: 0x17003997 RID: 14743
		// (get) Token: 0x0600E0E4 RID: 57572 RVA: 0x002C0278 File Offset: 0x002BE478
		internal override int ElementTypeId
		{
			get
			{
				return 12509;
			}
		}

		// Token: 0x0600E0E5 RID: 57573 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17003998 RID: 14744
		// (get) Token: 0x0600E0E6 RID: 57574 RVA: 0x002C027F File Offset: 0x002BE47F
		internal override string[] AttributeTagNames
		{
			get
			{
				return Fill.attributeTagNames;
			}
		}

		// Token: 0x17003999 RID: 14745
		// (get) Token: 0x0600E0E7 RID: 57575 RVA: 0x002C0286 File Offset: 0x002BE486
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Fill.attributeNamespaceIds;
			}
		}

		// Token: 0x1700399A RID: 14746
		// (get) Token: 0x0600E0E8 RID: 57576 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0600E0E9 RID: 57577 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "id")]
		public StringValue Id
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

		// Token: 0x1700399B RID: 14747
		// (get) Token: 0x0600E0EA RID: 57578 RVA: 0x002C028D File Offset: 0x002BE48D
		// (set) Token: 0x0600E0EB RID: 57579 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "type")]
		public EnumValue<FillTypeValues> Type
		{
			get
			{
				return (EnumValue<FillTypeValues>)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x1700399C RID: 14748
		// (get) Token: 0x0600E0EC RID: 57580 RVA: 0x002BDE2B File Offset: 0x002BC02B
		// (set) Token: 0x0600E0ED RID: 57581 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "on")]
		public TrueFalseValue On
		{
			get
			{
				return (TrueFalseValue)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x1700399D RID: 14749
		// (get) Token: 0x0600E0EE RID: 57582 RVA: 0x002BDADA File Offset: 0x002BBCDA
		// (set) Token: 0x0600E0EF RID: 57583 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "color")]
		public StringValue Color
		{
			get
			{
				return (StringValue)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x1700399E RID: 14750
		// (get) Token: 0x0600E0F0 RID: 57584 RVA: 0x002BD4B9 File Offset: 0x002BB6B9
		// (set) Token: 0x0600E0F1 RID: 57585 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "opacity")]
		public StringValue Opacity
		{
			get
			{
				return (StringValue)base.Attributes[4];
			}
			set
			{
				base.Attributes[4] = value;
			}
		}

		// Token: 0x1700399F RID: 14751
		// (get) Token: 0x0600E0F2 RID: 57586 RVA: 0x002BE081 File Offset: 0x002BC281
		// (set) Token: 0x0600E0F3 RID: 57587 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "color2")]
		public StringValue Color2
		{
			get
			{
				return (StringValue)base.Attributes[5];
			}
			set
			{
				base.Attributes[5] = value;
			}
		}

		// Token: 0x170039A0 RID: 14752
		// (get) Token: 0x0600E0F4 RID: 57588 RVA: 0x002BD4ED File Offset: 0x002BB6ED
		// (set) Token: 0x0600E0F5 RID: 57589 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "src")]
		public StringValue Source
		{
			get
			{
				return (StringValue)base.Attributes[6];
			}
			set
			{
				base.Attributes[6] = value;
			}
		}

		// Token: 0x170039A1 RID: 14753
		// (get) Token: 0x0600E0F6 RID: 57590 RVA: 0x002BDB07 File Offset: 0x002BBD07
		// (set) Token: 0x0600E0F7 RID: 57591 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(27, "href")]
		public StringValue Href
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

		// Token: 0x170039A2 RID: 14754
		// (get) Token: 0x0600E0F8 RID: 57592 RVA: 0x002BDB16 File Offset: 0x002BBD16
		// (set) Token: 0x0600E0F9 RID: 57593 RVA: 0x002BD530 File Offset: 0x002BB730
		[SchemaAttr(27, "althref")]
		public StringValue AlternateImageReference
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

		// Token: 0x170039A3 RID: 14755
		// (get) Token: 0x0600E0FA RID: 57594 RVA: 0x002BDB25 File Offset: 0x002BBD25
		// (set) Token: 0x0600E0FB RID: 57595 RVA: 0x002BD54B File Offset: 0x002BB74B
		[SchemaAttr(0, "size")]
		public StringValue Size
		{
			get
			{
				return (StringValue)base.Attributes[9];
			}
			set
			{
				base.Attributes[9] = value;
			}
		}

		// Token: 0x170039A4 RID: 14756
		// (get) Token: 0x0600E0FC RID: 57596 RVA: 0x002BDB35 File Offset: 0x002BBD35
		// (set) Token: 0x0600E0FD RID: 57597 RVA: 0x002BDB45 File Offset: 0x002BBD45
		[SchemaAttr(0, "origin")]
		public StringValue Origin
		{
			get
			{
				return (StringValue)base.Attributes[10];
			}
			set
			{
				base.Attributes[10] = value;
			}
		}

		// Token: 0x170039A5 RID: 14757
		// (get) Token: 0x0600E0FE RID: 57598 RVA: 0x002BDB51 File Offset: 0x002BBD51
		// (set) Token: 0x0600E0FF RID: 57599 RVA: 0x002BDB61 File Offset: 0x002BBD61
		[SchemaAttr(0, "position")]
		public StringValue Position
		{
			get
			{
				return (StringValue)base.Attributes[11];
			}
			set
			{
				base.Attributes[11] = value;
			}
		}

		// Token: 0x170039A6 RID: 14758
		// (get) Token: 0x0600E100 RID: 57600 RVA: 0x002C029C File Offset: 0x002BE49C
		// (set) Token: 0x0600E101 RID: 57601 RVA: 0x002BDB7D File Offset: 0x002BBD7D
		[SchemaAttr(0, "aspect")]
		public EnumValue<ImageAspectValues> Aspect
		{
			get
			{
				return (EnumValue<ImageAspectValues>)base.Attributes[12];
			}
			set
			{
				base.Attributes[12] = value;
			}
		}

		// Token: 0x170039A7 RID: 14759
		// (get) Token: 0x0600E102 RID: 57602 RVA: 0x002BEF1F File Offset: 0x002BD11F
		// (set) Token: 0x0600E103 RID: 57603 RVA: 0x002BE209 File Offset: 0x002BC409
		[SchemaAttr(0, "colors")]
		public StringValue Colors
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

		// Token: 0x170039A8 RID: 14760
		// (get) Token: 0x0600E104 RID: 57604 RVA: 0x002C02AC File Offset: 0x002BE4AC
		// (set) Token: 0x0600E105 RID: 57605 RVA: 0x002BE225 File Offset: 0x002BC425
		[SchemaAttr(0, "angle")]
		public DecimalValue Angle
		{
			get
			{
				return (DecimalValue)base.Attributes[14];
			}
			set
			{
				base.Attributes[14] = value;
			}
		}

		// Token: 0x170039A9 RID: 14761
		// (get) Token: 0x0600E106 RID: 57606 RVA: 0x002C02BC File Offset: 0x002BE4BC
		// (set) Token: 0x0600E107 RID: 57607 RVA: 0x002BE241 File Offset: 0x002BC441
		[SchemaAttr(0, "alignshape")]
		public TrueFalseValue AlignShape
		{
			get
			{
				return (TrueFalseValue)base.Attributes[15];
			}
			set
			{
				base.Attributes[15] = value;
			}
		}

		// Token: 0x170039AA RID: 14762
		// (get) Token: 0x0600E108 RID: 57608 RVA: 0x002BE24D File Offset: 0x002BC44D
		// (set) Token: 0x0600E109 RID: 57609 RVA: 0x002BE25D File Offset: 0x002BC45D
		[SchemaAttr(0, "focus")]
		public StringValue Focus
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

		// Token: 0x170039AB RID: 14763
		// (get) Token: 0x0600E10A RID: 57610 RVA: 0x002C02CC File Offset: 0x002BE4CC
		// (set) Token: 0x0600E10B RID: 57611 RVA: 0x002BE279 File Offset: 0x002BC479
		[SchemaAttr(0, "focussize")]
		public StringValue FocusSize
		{
			get
			{
				return (StringValue)base.Attributes[17];
			}
			set
			{
				base.Attributes[17] = value;
			}
		}

		// Token: 0x170039AC RID: 14764
		// (get) Token: 0x0600E10C RID: 57612 RVA: 0x002BE285 File Offset: 0x002BC485
		// (set) Token: 0x0600E10D RID: 57613 RVA: 0x002BE295 File Offset: 0x002BC495
		[SchemaAttr(0, "focusposition")]
		public StringValue FocusPosition
		{
			get
			{
				return (StringValue)base.Attributes[18];
			}
			set
			{
				base.Attributes[18] = value;
			}
		}

		// Token: 0x170039AD RID: 14765
		// (get) Token: 0x0600E10E RID: 57614 RVA: 0x002C02DC File Offset: 0x002BE4DC
		// (set) Token: 0x0600E10F RID: 57615 RVA: 0x002BE2B1 File Offset: 0x002BC4B1
		[SchemaAttr(0, "method")]
		public EnumValue<FillMethodValues> Method
		{
			get
			{
				return (EnumValue<FillMethodValues>)base.Attributes[19];
			}
			set
			{
				base.Attributes[19] = value;
			}
		}

		// Token: 0x170039AE RID: 14766
		// (get) Token: 0x0600E110 RID: 57616 RVA: 0x002BE2BD File Offset: 0x002BC4BD
		// (set) Token: 0x0600E111 RID: 57617 RVA: 0x002BE2CD File Offset: 0x002BC4CD
		[SchemaAttr(27, "detectmouseclick")]
		public TrueFalseValue DetectMouseClick
		{
			get
			{
				return (TrueFalseValue)base.Attributes[20];
			}
			set
			{
				base.Attributes[20] = value;
			}
		}

		// Token: 0x170039AF RID: 14767
		// (get) Token: 0x0600E112 RID: 57618 RVA: 0x002BE2D9 File Offset: 0x002BC4D9
		// (set) Token: 0x0600E113 RID: 57619 RVA: 0x002BE2E9 File Offset: 0x002BC4E9
		[SchemaAttr(27, "title")]
		public StringValue Title
		{
			get
			{
				return (StringValue)base.Attributes[21];
			}
			set
			{
				base.Attributes[21] = value;
			}
		}

		// Token: 0x170039B0 RID: 14768
		// (get) Token: 0x0600E114 RID: 57620 RVA: 0x002BE2F5 File Offset: 0x002BC4F5
		// (set) Token: 0x0600E115 RID: 57621 RVA: 0x002BE305 File Offset: 0x002BC505
		[SchemaAttr(27, "opacity2")]
		public StringValue Opacity2
		{
			get
			{
				return (StringValue)base.Attributes[22];
			}
			set
			{
				base.Attributes[22] = value;
			}
		}

		// Token: 0x170039B1 RID: 14769
		// (get) Token: 0x0600E116 RID: 57622 RVA: 0x002BE311 File Offset: 0x002BC511
		// (set) Token: 0x0600E117 RID: 57623 RVA: 0x002BE321 File Offset: 0x002BC521
		[SchemaAttr(0, "recolor")]
		public TrueFalseValue Recolor
		{
			get
			{
				return (TrueFalseValue)base.Attributes[23];
			}
			set
			{
				base.Attributes[23] = value;
			}
		}

		// Token: 0x170039B2 RID: 14770
		// (get) Token: 0x0600E118 RID: 57624 RVA: 0x002C02EC File Offset: 0x002BE4EC
		// (set) Token: 0x0600E119 RID: 57625 RVA: 0x002BE33D File Offset: 0x002BC53D
		[SchemaAttr(0, "rotate")]
		public TrueFalseValue Rotate
		{
			get
			{
				return (TrueFalseValue)base.Attributes[24];
			}
			set
			{
				base.Attributes[24] = value;
			}
		}

		// Token: 0x170039B3 RID: 14771
		// (get) Token: 0x0600E11A RID: 57626 RVA: 0x002BE349 File Offset: 0x002BC549
		// (set) Token: 0x0600E11B RID: 57627 RVA: 0x002BE359 File Offset: 0x002BC559
		[SchemaAttr(19, "id")]
		public StringValue RelationshipId
		{
			get
			{
				return (StringValue)base.Attributes[25];
			}
			set
			{
				base.Attributes[25] = value;
			}
		}

		// Token: 0x0600E11C RID: 57628 RVA: 0x00293ECF File Offset: 0x002920CF
		public Fill()
		{
		}

		// Token: 0x0600E11D RID: 57629 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Fill(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600E11E RID: 57630 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Fill(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600E11F RID: 57631 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Fill(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0600E120 RID: 57632 RVA: 0x002C02FC File Offset: 0x002BE4FC
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (27 == namespaceId && "fill" == name)
			{
				return new FillExtendedProperties();
			}
			return null;
		}

		// Token: 0x170039B4 RID: 14772
		// (get) Token: 0x0600E121 RID: 57633 RVA: 0x002C0317 File Offset: 0x002BE517
		internal override string[] ElementTagNames
		{
			get
			{
				return Fill.eleTagNames;
			}
		}

		// Token: 0x170039B5 RID: 14773
		// (get) Token: 0x0600E122 RID: 57634 RVA: 0x002C031E File Offset: 0x002BE51E
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return Fill.eleNamespaceIds;
			}
		}

		// Token: 0x170039B6 RID: 14774
		// (get) Token: 0x0600E123 RID: 57635 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x170039B7 RID: 14775
		// (get) Token: 0x0600E124 RID: 57636 RVA: 0x002C0325 File Offset: 0x002BE525
		// (set) Token: 0x0600E125 RID: 57637 RVA: 0x002C032E File Offset: 0x002BE52E
		public FillExtendedProperties FillExtendedProperties
		{
			get
			{
				return base.GetElement<FillExtendedProperties>(0);
			}
			set
			{
				base.SetElement<FillExtendedProperties>(0, value);
			}
		}

		// Token: 0x0600E126 RID: 57638 RVA: 0x002C0338 File Offset: 0x002BE538
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "id" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "type" == name)
			{
				return new EnumValue<FillTypeValues>();
			}
			if (namespaceId == 0 && "on" == name)
			{
				return new TrueFalseValue();
			}
			if (namespaceId == 0 && "color" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "opacity" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "color2" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "src" == name)
			{
				return new StringValue();
			}
			if (27 == namespaceId && "href" == name)
			{
				return new StringValue();
			}
			if (27 == namespaceId && "althref" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "size" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "origin" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "position" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "aspect" == name)
			{
				return new EnumValue<ImageAspectValues>();
			}
			if (namespaceId == 0 && "colors" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "angle" == name)
			{
				return new DecimalValue();
			}
			if (namespaceId == 0 && "alignshape" == name)
			{
				return new TrueFalseValue();
			}
			if (namespaceId == 0 && "focus" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "focussize" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "focusposition" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "method" == name)
			{
				return new EnumValue<FillMethodValues>();
			}
			if (27 == namespaceId && "detectmouseclick" == name)
			{
				return new TrueFalseValue();
			}
			if (27 == namespaceId && "title" == name)
			{
				return new StringValue();
			}
			if (27 == namespaceId && "opacity2" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "recolor" == name)
			{
				return new TrueFalseValue();
			}
			if (namespaceId == 0 && "rotate" == name)
			{
				return new TrueFalseValue();
			}
			if (19 == namespaceId && "id" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0600E127 RID: 57639 RVA: 0x002C0595 File Offset: 0x002BE795
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Fill>(deep);
		}

		// Token: 0x04006E9B RID: 28315
		private const string tagName = "fill";

		// Token: 0x04006E9C RID: 28316
		private const byte tagNsId = 26;

		// Token: 0x04006E9D RID: 28317
		internal const int ElementTypeIdConst = 12509;

		// Token: 0x04006E9E RID: 28318
		private static string[] attributeTagNames = new string[]
		{
			"id", "type", "on", "color", "opacity", "color2", "src", "href", "althref", "size",
			"origin", "position", "aspect", "colors", "angle", "alignshape", "focus", "focussize", "focusposition", "method",
			"detectmouseclick", "title", "opacity2", "recolor", "rotate", "id"
		};

		// Token: 0x04006E9F RID: 28319
		private static byte[] attributeNamespaceIds = new byte[]
		{
			0, 0, 0, 0, 0, 0, 0, 27, 27, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			27, 27, 27, 0, 0, 19
		};

		// Token: 0x04006EA0 RID: 28320
		private static readonly string[] eleTagNames = new string[] { "fill" };

		// Token: 0x04006EA1 RID: 28321
		private static readonly byte[] eleNamespaceIds = new byte[] { 27 };
	}
}
