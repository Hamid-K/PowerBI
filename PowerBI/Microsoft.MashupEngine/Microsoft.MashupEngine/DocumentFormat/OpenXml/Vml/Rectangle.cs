using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Vml.Office;
using DocumentFormat.OpenXml.Vml.Presentation;
using DocumentFormat.OpenXml.Vml.Spreadsheet;
using DocumentFormat.OpenXml.Vml.Wordprocessing;

namespace DocumentFormat.OpenXml.Vml
{
	// Token: 0x02002255 RID: 8789
	[ChildElementInfo(typeof(Formulas))]
	[ChildElementInfo(typeof(Fill))]
	[ChildElementInfo(typeof(Stroke))]
	[ChildElementInfo(typeof(Shadow))]
	[ChildElementInfo(typeof(TextBox))]
	[ChildElementInfo(typeof(TextPath))]
	[ChildElementInfo(typeof(ImageData))]
	[ChildElementInfo(typeof(Skew))]
	[ChildElementInfo(typeof(Extrusion))]
	[ChildElementInfo(typeof(Callout))]
	[ChildElementInfo(typeof(Lock))]
	[ChildElementInfo(typeof(ClipPath))]
	[ChildElementInfo(typeof(SignatureLine))]
	[ChildElementInfo(typeof(TextWrap))]
	[ChildElementInfo(typeof(AnchorLock))]
	[ChildElementInfo(typeof(TopBorder))]
	[ChildElementInfo(typeof(BottomBorder))]
	[ChildElementInfo(typeof(LeftBorder))]
	[ChildElementInfo(typeof(RightBorder))]
	[ChildElementInfo(typeof(ClientData))]
	[ChildElementInfo(typeof(TextData))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(Path))]
	[ChildElementInfo(typeof(ShapeHandles))]
	internal class Rectangle : OpenXmlCompositeElement
	{
		// Token: 0x17003C2E RID: 15406
		// (get) Token: 0x0600E63F RID: 58943 RVA: 0x002C6E31 File Offset: 0x002C5031
		public override string LocalName
		{
			get
			{
				return "rect";
			}
		}

		// Token: 0x17003C2F RID: 15407
		// (get) Token: 0x0600E640 RID: 58944 RVA: 0x00243C87 File Offset: 0x00241E87
		internal override byte NamespaceId
		{
			get
			{
				return 26;
			}
		}

		// Token: 0x17003C30 RID: 15408
		// (get) Token: 0x0600E641 RID: 58945 RVA: 0x002C6E38 File Offset: 0x002C5038
		internal override int ElementTypeId
		{
			get
			{
				return 12525;
			}
		}

		// Token: 0x0600E642 RID: 58946 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17003C31 RID: 15409
		// (get) Token: 0x0600E643 RID: 58947 RVA: 0x002C6E3F File Offset: 0x002C503F
		internal override string[] AttributeTagNames
		{
			get
			{
				return Rectangle.attributeTagNames;
			}
		}

		// Token: 0x17003C32 RID: 15410
		// (get) Token: 0x0600E644 RID: 58948 RVA: 0x002C6E46 File Offset: 0x002C5046
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Rectangle.attributeNamespaceIds;
			}
		}

		// Token: 0x17003C33 RID: 15411
		// (get) Token: 0x0600E645 RID: 58949 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0600E646 RID: 58950 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x17003C34 RID: 15412
		// (get) Token: 0x0600E647 RID: 58951 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x0600E648 RID: 58952 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "style")]
		public StringValue Style
		{
			get
			{
				return (StringValue)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x17003C35 RID: 15413
		// (get) Token: 0x0600E649 RID: 58953 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x0600E64A RID: 58954 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "href")]
		public StringValue Href
		{
			get
			{
				return (StringValue)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x17003C36 RID: 15414
		// (get) Token: 0x0600E64B RID: 58955 RVA: 0x002BDADA File Offset: 0x002BBCDA
		// (set) Token: 0x0600E64C RID: 58956 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "target")]
		public StringValue Target
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

		// Token: 0x17003C37 RID: 15415
		// (get) Token: 0x0600E64D RID: 58957 RVA: 0x002BD4B9 File Offset: 0x002BB6B9
		// (set) Token: 0x0600E64E RID: 58958 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "class")]
		public StringValue Class
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

		// Token: 0x17003C38 RID: 15416
		// (get) Token: 0x0600E64F RID: 58959 RVA: 0x002BE081 File Offset: 0x002BC281
		// (set) Token: 0x0600E650 RID: 58960 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "title")]
		public StringValue Title
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

		// Token: 0x17003C39 RID: 15417
		// (get) Token: 0x0600E651 RID: 58961 RVA: 0x002BD4ED File Offset: 0x002BB6ED
		// (set) Token: 0x0600E652 RID: 58962 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "alt")]
		public StringValue Alternate
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

		// Token: 0x17003C3A RID: 15418
		// (get) Token: 0x0600E653 RID: 58963 RVA: 0x002BDB07 File Offset: 0x002BBD07
		// (set) Token: 0x0600E654 RID: 58964 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(0, "coordsize")]
		public StringValue CoordinateSize
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

		// Token: 0x17003C3B RID: 15419
		// (get) Token: 0x0600E655 RID: 58965 RVA: 0x002BDB16 File Offset: 0x002BBD16
		// (set) Token: 0x0600E656 RID: 58966 RVA: 0x002BD530 File Offset: 0x002BB730
		[SchemaAttr(0, "coordorigin")]
		public StringValue CoordinateOrigin
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

		// Token: 0x17003C3C RID: 15420
		// (get) Token: 0x0600E657 RID: 58967 RVA: 0x002BDB25 File Offset: 0x002BBD25
		// (set) Token: 0x0600E658 RID: 58968 RVA: 0x002BD54B File Offset: 0x002BB74B
		[SchemaAttr(0, "wrapcoords")]
		public StringValue WrapCoordinates
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

		// Token: 0x17003C3D RID: 15421
		// (get) Token: 0x0600E659 RID: 58969 RVA: 0x002BE827 File Offset: 0x002BCA27
		// (set) Token: 0x0600E65A RID: 58970 RVA: 0x002BDB45 File Offset: 0x002BBD45
		[SchemaAttr(0, "print")]
		public TrueFalseValue Print
		{
			get
			{
				return (TrueFalseValue)base.Attributes[10];
			}
			set
			{
				base.Attributes[10] = value;
			}
		}

		// Token: 0x17003C3E RID: 15422
		// (get) Token: 0x0600E65B RID: 58971 RVA: 0x002BDB51 File Offset: 0x002BBD51
		// (set) Token: 0x0600E65C RID: 58972 RVA: 0x002BDB61 File Offset: 0x002BBD61
		[SchemaAttr(27, "spid")]
		public StringValue OptionalString
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

		// Token: 0x17003C3F RID: 15423
		// (get) Token: 0x0600E65D RID: 58973 RVA: 0x002BE1E9 File Offset: 0x002BC3E9
		// (set) Token: 0x0600E65E RID: 58974 RVA: 0x002BDB7D File Offset: 0x002BBD7D
		[SchemaAttr(27, "oned")]
		public TrueFalseValue Oned
		{
			get
			{
				return (TrueFalseValue)base.Attributes[12];
			}
			set
			{
				base.Attributes[12] = value;
			}
		}

		// Token: 0x17003C40 RID: 15424
		// (get) Token: 0x0600E65F RID: 58975 RVA: 0x002C1380 File Offset: 0x002BF580
		// (set) Token: 0x0600E660 RID: 58976 RVA: 0x002BE209 File Offset: 0x002BC409
		[SchemaAttr(27, "regroupid")]
		public IntegerValue RegroupId
		{
			get
			{
				return (IntegerValue)base.Attributes[13];
			}
			set
			{
				base.Attributes[13] = value;
			}
		}

		// Token: 0x17003C41 RID: 15425
		// (get) Token: 0x0600E661 RID: 58977 RVA: 0x002BFFE2 File Offset: 0x002BE1E2
		// (set) Token: 0x0600E662 RID: 58978 RVA: 0x002BE225 File Offset: 0x002BC425
		[SchemaAttr(27, "doubleclicknotify")]
		public TrueFalseValue DoubleClickNotify
		{
			get
			{
				return (TrueFalseValue)base.Attributes[14];
			}
			set
			{
				base.Attributes[14] = value;
			}
		}

		// Token: 0x17003C42 RID: 15426
		// (get) Token: 0x0600E663 RID: 58979 RVA: 0x002C02BC File Offset: 0x002BE4BC
		// (set) Token: 0x0600E664 RID: 58980 RVA: 0x002BE241 File Offset: 0x002BC441
		[SchemaAttr(27, "button")]
		public TrueFalseValue Button
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

		// Token: 0x17003C43 RID: 15427
		// (get) Token: 0x0600E665 RID: 58981 RVA: 0x002BEF3F File Offset: 0x002BD13F
		// (set) Token: 0x0600E666 RID: 58982 RVA: 0x002BE25D File Offset: 0x002BC45D
		[SchemaAttr(27, "userhidden")]
		public TrueFalseValue UserHidden
		{
			get
			{
				return (TrueFalseValue)base.Attributes[16];
			}
			set
			{
				base.Attributes[16] = value;
			}
		}

		// Token: 0x17003C44 RID: 15428
		// (get) Token: 0x0600E667 RID: 58983 RVA: 0x002C1390 File Offset: 0x002BF590
		// (set) Token: 0x0600E668 RID: 58984 RVA: 0x002BE279 File Offset: 0x002BC479
		[SchemaAttr(27, "bullet")]
		public TrueFalseValue Bullet
		{
			get
			{
				return (TrueFalseValue)base.Attributes[17];
			}
			set
			{
				base.Attributes[17] = value;
			}
		}

		// Token: 0x17003C45 RID: 15429
		// (get) Token: 0x0600E669 RID: 58985 RVA: 0x002C13A0 File Offset: 0x002BF5A0
		// (set) Token: 0x0600E66A RID: 58986 RVA: 0x002BE295 File Offset: 0x002BC495
		[SchemaAttr(27, "hr")]
		public TrueFalseValue Horizontal
		{
			get
			{
				return (TrueFalseValue)base.Attributes[18];
			}
			set
			{
				base.Attributes[18] = value;
			}
		}

		// Token: 0x17003C46 RID: 15430
		// (get) Token: 0x0600E66B RID: 58987 RVA: 0x002C13B0 File Offset: 0x002BF5B0
		// (set) Token: 0x0600E66C RID: 58988 RVA: 0x002BE2B1 File Offset: 0x002BC4B1
		[SchemaAttr(27, "hrstd")]
		public TrueFalseValue HorizontalStandard
		{
			get
			{
				return (TrueFalseValue)base.Attributes[19];
			}
			set
			{
				base.Attributes[19] = value;
			}
		}

		// Token: 0x17003C47 RID: 15431
		// (get) Token: 0x0600E66D RID: 58989 RVA: 0x002BE2BD File Offset: 0x002BC4BD
		// (set) Token: 0x0600E66E RID: 58990 RVA: 0x002BE2CD File Offset: 0x002BC4CD
		[SchemaAttr(27, "hrnoshade")]
		public TrueFalseValue HorizontalNoShade
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

		// Token: 0x17003C48 RID: 15432
		// (get) Token: 0x0600E66F RID: 58991 RVA: 0x002C13C0 File Offset: 0x002BF5C0
		// (set) Token: 0x0600E670 RID: 58992 RVA: 0x002BE2E9 File Offset: 0x002BC4E9
		[SchemaAttr(27, "hrpct")]
		public SingleValue HorizontalPercentage
		{
			get
			{
				return (SingleValue)base.Attributes[21];
			}
			set
			{
				base.Attributes[21] = value;
			}
		}

		// Token: 0x17003C49 RID: 15433
		// (get) Token: 0x0600E671 RID: 58993 RVA: 0x002C13D0 File Offset: 0x002BF5D0
		// (set) Token: 0x0600E672 RID: 58994 RVA: 0x002BE305 File Offset: 0x002BC505
		[SchemaAttr(27, "hralign")]
		public EnumValue<HorizontalRuleAlignmentValues> HorizontalAlignment
		{
			get
			{
				return (EnumValue<HorizontalRuleAlignmentValues>)base.Attributes[22];
			}
			set
			{
				base.Attributes[22] = value;
			}
		}

		// Token: 0x17003C4A RID: 15434
		// (get) Token: 0x0600E673 RID: 58995 RVA: 0x002BE311 File Offset: 0x002BC511
		// (set) Token: 0x0600E674 RID: 58996 RVA: 0x002BE321 File Offset: 0x002BC521
		[SchemaAttr(27, "allowincell")]
		public TrueFalseValue AllowInCell
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

		// Token: 0x17003C4B RID: 15435
		// (get) Token: 0x0600E675 RID: 58997 RVA: 0x002C02EC File Offset: 0x002BE4EC
		// (set) Token: 0x0600E676 RID: 58998 RVA: 0x002BE33D File Offset: 0x002BC53D
		[SchemaAttr(27, "allowoverlap")]
		public TrueFalseValue AllowOverlap
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

		// Token: 0x17003C4C RID: 15436
		// (get) Token: 0x0600E677 RID: 58999 RVA: 0x002C0793 File Offset: 0x002BE993
		// (set) Token: 0x0600E678 RID: 59000 RVA: 0x002BE359 File Offset: 0x002BC559
		[SchemaAttr(27, "userdrawn")]
		public TrueFalseValue UserDrawn
		{
			get
			{
				return (TrueFalseValue)base.Attributes[25];
			}
			set
			{
				base.Attributes[25] = value;
			}
		}

		// Token: 0x17003C4D RID: 15437
		// (get) Token: 0x0600E679 RID: 59001 RVA: 0x002BE365 File Offset: 0x002BC565
		// (set) Token: 0x0600E67A RID: 59002 RVA: 0x002BE375 File Offset: 0x002BC575
		[SchemaAttr(27, "bordertopcolor")]
		public StringValue BorderTopColor
		{
			get
			{
				return (StringValue)base.Attributes[26];
			}
			set
			{
				base.Attributes[26] = value;
			}
		}

		// Token: 0x17003C4E RID: 15438
		// (get) Token: 0x0600E67B RID: 59003 RVA: 0x002C13E0 File Offset: 0x002BF5E0
		// (set) Token: 0x0600E67C RID: 59004 RVA: 0x002BE391 File Offset: 0x002BC591
		[SchemaAttr(27, "borderleftcolor")]
		public StringValue BorderLeftColor
		{
			get
			{
				return (StringValue)base.Attributes[27];
			}
			set
			{
				base.Attributes[27] = value;
			}
		}

		// Token: 0x17003C4F RID: 15439
		// (get) Token: 0x0600E67D RID: 59005 RVA: 0x002BE39D File Offset: 0x002BC59D
		// (set) Token: 0x0600E67E RID: 59006 RVA: 0x002BE3AD File Offset: 0x002BC5AD
		[SchemaAttr(27, "borderbottomcolor")]
		public StringValue BorderBottomColor
		{
			get
			{
				return (StringValue)base.Attributes[28];
			}
			set
			{
				base.Attributes[28] = value;
			}
		}

		// Token: 0x17003C50 RID: 15440
		// (get) Token: 0x0600E67F RID: 59007 RVA: 0x002BE3B9 File Offset: 0x002BC5B9
		// (set) Token: 0x0600E680 RID: 59008 RVA: 0x002BE3C9 File Offset: 0x002BC5C9
		[SchemaAttr(27, "borderrightcolor")]
		public StringValue BorderRightColor
		{
			get
			{
				return (StringValue)base.Attributes[29];
			}
			set
			{
				base.Attributes[29] = value;
			}
		}

		// Token: 0x17003C51 RID: 15441
		// (get) Token: 0x0600E681 RID: 59009 RVA: 0x002C13F0 File Offset: 0x002BF5F0
		// (set) Token: 0x0600E682 RID: 59010 RVA: 0x002BE3E5 File Offset: 0x002BC5E5
		[SchemaAttr(27, "dgmlayout")]
		public IntegerValue DiagramLayout
		{
			get
			{
				return (IntegerValue)base.Attributes[30];
			}
			set
			{
				base.Attributes[30] = value;
			}
		}

		// Token: 0x17003C52 RID: 15442
		// (get) Token: 0x0600E683 RID: 59011 RVA: 0x002C1400 File Offset: 0x002BF600
		// (set) Token: 0x0600E684 RID: 59012 RVA: 0x002C1410 File Offset: 0x002BF610
		[SchemaAttr(27, "dgmnodekind")]
		public IntegerValue DiagramNodeKind
		{
			get
			{
				return (IntegerValue)base.Attributes[31];
			}
			set
			{
				base.Attributes[31] = value;
			}
		}

		// Token: 0x17003C53 RID: 15443
		// (get) Token: 0x0600E685 RID: 59013 RVA: 0x002C141C File Offset: 0x002BF61C
		// (set) Token: 0x0600E686 RID: 59014 RVA: 0x002C142C File Offset: 0x002BF62C
		[SchemaAttr(27, "dgmlayoutmru")]
		public IntegerValue DiagramLayoutMostRecentUsed
		{
			get
			{
				return (IntegerValue)base.Attributes[32];
			}
			set
			{
				base.Attributes[32] = value;
			}
		}

		// Token: 0x17003C54 RID: 15444
		// (get) Token: 0x0600E687 RID: 59015 RVA: 0x002C1438 File Offset: 0x002BF638
		// (set) Token: 0x0600E688 RID: 59016 RVA: 0x002C1448 File Offset: 0x002BF648
		[SchemaAttr(27, "insetmode")]
		public EnumValue<InsetMarginValues> InsetMode
		{
			get
			{
				return (EnumValue<InsetMarginValues>)base.Attributes[33];
			}
			set
			{
				base.Attributes[33] = value;
			}
		}

		// Token: 0x17003C55 RID: 15445
		// (get) Token: 0x0600E689 RID: 59017 RVA: 0x002C1454 File Offset: 0x002BF654
		// (set) Token: 0x0600E68A RID: 59018 RVA: 0x002C1464 File Offset: 0x002BF664
		[SchemaAttr(0, "filled")]
		public TrueFalseValue Filled
		{
			get
			{
				return (TrueFalseValue)base.Attributes[34];
			}
			set
			{
				base.Attributes[34] = value;
			}
		}

		// Token: 0x17003C56 RID: 15446
		// (get) Token: 0x0600E68B RID: 59019 RVA: 0x002C1470 File Offset: 0x002BF670
		// (set) Token: 0x0600E68C RID: 59020 RVA: 0x002C1480 File Offset: 0x002BF680
		[SchemaAttr(0, "fillcolor")]
		public StringValue FillColor
		{
			get
			{
				return (StringValue)base.Attributes[35];
			}
			set
			{
				base.Attributes[35] = value;
			}
		}

		// Token: 0x17003C57 RID: 15447
		// (get) Token: 0x0600E68D RID: 59021 RVA: 0x002C148C File Offset: 0x002BF68C
		// (set) Token: 0x0600E68E RID: 59022 RVA: 0x002C149C File Offset: 0x002BF69C
		[SchemaAttr(0, "stroked")]
		public TrueFalseValue Stroked
		{
			get
			{
				return (TrueFalseValue)base.Attributes[36];
			}
			set
			{
				base.Attributes[36] = value;
			}
		}

		// Token: 0x17003C58 RID: 15448
		// (get) Token: 0x0600E68F RID: 59023 RVA: 0x002C14A8 File Offset: 0x002BF6A8
		// (set) Token: 0x0600E690 RID: 59024 RVA: 0x002C14B8 File Offset: 0x002BF6B8
		[SchemaAttr(0, "strokecolor")]
		public StringValue StrokeColor
		{
			get
			{
				return (StringValue)base.Attributes[37];
			}
			set
			{
				base.Attributes[37] = value;
			}
		}

		// Token: 0x17003C59 RID: 15449
		// (get) Token: 0x0600E691 RID: 59025 RVA: 0x002C14C4 File Offset: 0x002BF6C4
		// (set) Token: 0x0600E692 RID: 59026 RVA: 0x002C14D4 File Offset: 0x002BF6D4
		[SchemaAttr(0, "strokeweight")]
		public StringValue StrokeWeight
		{
			get
			{
				return (StringValue)base.Attributes[38];
			}
			set
			{
				base.Attributes[38] = value;
			}
		}

		// Token: 0x17003C5A RID: 15450
		// (get) Token: 0x0600E693 RID: 59027 RVA: 0x002C14E0 File Offset: 0x002BF6E0
		// (set) Token: 0x0600E694 RID: 59028 RVA: 0x002C14F0 File Offset: 0x002BF6F0
		[SchemaAttr(0, "insetpen")]
		public TrueFalseValue InsetPen
		{
			get
			{
				return (TrueFalseValue)base.Attributes[39];
			}
			set
			{
				base.Attributes[39] = value;
			}
		}

		// Token: 0x17003C5B RID: 15451
		// (get) Token: 0x0600E695 RID: 59029 RVA: 0x002C14FC File Offset: 0x002BF6FC
		// (set) Token: 0x0600E696 RID: 59030 RVA: 0x002C150C File Offset: 0x002BF70C
		[SchemaAttr(27, "spt")]
		public Int32Value OptionalNumber
		{
			get
			{
				return (Int32Value)base.Attributes[40];
			}
			set
			{
				base.Attributes[40] = value;
			}
		}

		// Token: 0x17003C5C RID: 15452
		// (get) Token: 0x0600E697 RID: 59031 RVA: 0x002C1518 File Offset: 0x002BF718
		// (set) Token: 0x0600E698 RID: 59032 RVA: 0x002C1528 File Offset: 0x002BF728
		[SchemaAttr(27, "connectortype")]
		public EnumValue<ConnectorValues> ConnectorType
		{
			get
			{
				return (EnumValue<ConnectorValues>)base.Attributes[41];
			}
			set
			{
				base.Attributes[41] = value;
			}
		}

		// Token: 0x17003C5D RID: 15453
		// (get) Token: 0x0600E699 RID: 59033 RVA: 0x002C1534 File Offset: 0x002BF734
		// (set) Token: 0x0600E69A RID: 59034 RVA: 0x002C1544 File Offset: 0x002BF744
		[SchemaAttr(27, "bwmode")]
		public EnumValue<BlackAndWhiteModeValues> BlackWhiteMode
		{
			get
			{
				return (EnumValue<BlackAndWhiteModeValues>)base.Attributes[42];
			}
			set
			{
				base.Attributes[42] = value;
			}
		}

		// Token: 0x17003C5E RID: 15454
		// (get) Token: 0x0600E69B RID: 59035 RVA: 0x002C1550 File Offset: 0x002BF750
		// (set) Token: 0x0600E69C RID: 59036 RVA: 0x002C1560 File Offset: 0x002BF760
		[SchemaAttr(27, "bwpure")]
		public EnumValue<BlackAndWhiteModeValues> PureBlackWhiteMode
		{
			get
			{
				return (EnumValue<BlackAndWhiteModeValues>)base.Attributes[43];
			}
			set
			{
				base.Attributes[43] = value;
			}
		}

		// Token: 0x17003C5F RID: 15455
		// (get) Token: 0x0600E69D RID: 59037 RVA: 0x002C156C File Offset: 0x002BF76C
		// (set) Token: 0x0600E69E RID: 59038 RVA: 0x002C157C File Offset: 0x002BF77C
		[SchemaAttr(27, "bwnormal")]
		public EnumValue<BlackAndWhiteModeValues> NormalBlackWhiteMode
		{
			get
			{
				return (EnumValue<BlackAndWhiteModeValues>)base.Attributes[44];
			}
			set
			{
				base.Attributes[44] = value;
			}
		}

		// Token: 0x17003C60 RID: 15456
		// (get) Token: 0x0600E69F RID: 59039 RVA: 0x002C1588 File Offset: 0x002BF788
		// (set) Token: 0x0600E6A0 RID: 59040 RVA: 0x002C1598 File Offset: 0x002BF798
		[SchemaAttr(27, "forcedash")]
		public TrueFalseValue ForceDash
		{
			get
			{
				return (TrueFalseValue)base.Attributes[45];
			}
			set
			{
				base.Attributes[45] = value;
			}
		}

		// Token: 0x17003C61 RID: 15457
		// (get) Token: 0x0600E6A1 RID: 59041 RVA: 0x002C15A4 File Offset: 0x002BF7A4
		// (set) Token: 0x0600E6A2 RID: 59042 RVA: 0x002C15B4 File Offset: 0x002BF7B4
		[SchemaAttr(27, "oleicon")]
		public TrueFalseValue OleIcon
		{
			get
			{
				return (TrueFalseValue)base.Attributes[46];
			}
			set
			{
				base.Attributes[46] = value;
			}
		}

		// Token: 0x17003C62 RID: 15458
		// (get) Token: 0x0600E6A3 RID: 59043 RVA: 0x002C15C0 File Offset: 0x002BF7C0
		// (set) Token: 0x0600E6A4 RID: 59044 RVA: 0x002C15D0 File Offset: 0x002BF7D0
		[SchemaAttr(27, "ole")]
		public TrueFalseBlankValue Ole
		{
			get
			{
				return (TrueFalseBlankValue)base.Attributes[47];
			}
			set
			{
				base.Attributes[47] = value;
			}
		}

		// Token: 0x17003C63 RID: 15459
		// (get) Token: 0x0600E6A5 RID: 59045 RVA: 0x002C15DC File Offset: 0x002BF7DC
		// (set) Token: 0x0600E6A6 RID: 59046 RVA: 0x002C15EC File Offset: 0x002BF7EC
		[SchemaAttr(27, "preferrelative")]
		public TrueFalseValue PreferRelative
		{
			get
			{
				return (TrueFalseValue)base.Attributes[48];
			}
			set
			{
				base.Attributes[48] = value;
			}
		}

		// Token: 0x17003C64 RID: 15460
		// (get) Token: 0x0600E6A7 RID: 59047 RVA: 0x002C15F8 File Offset: 0x002BF7F8
		// (set) Token: 0x0600E6A8 RID: 59048 RVA: 0x002C1608 File Offset: 0x002BF808
		[SchemaAttr(27, "cliptowrap")]
		public TrueFalseValue ClipToWrap
		{
			get
			{
				return (TrueFalseValue)base.Attributes[49];
			}
			set
			{
				base.Attributes[49] = value;
			}
		}

		// Token: 0x17003C65 RID: 15461
		// (get) Token: 0x0600E6A9 RID: 59049 RVA: 0x002C1614 File Offset: 0x002BF814
		// (set) Token: 0x0600E6AA RID: 59050 RVA: 0x002C1624 File Offset: 0x002BF824
		[SchemaAttr(27, "clip")]
		public TrueFalseValue Clip
		{
			get
			{
				return (TrueFalseValue)base.Attributes[50];
			}
			set
			{
				base.Attributes[50] = value;
			}
		}

		// Token: 0x0600E6AB RID: 59051 RVA: 0x00293ECF File Offset: 0x002920CF
		public Rectangle()
		{
		}

		// Token: 0x0600E6AC RID: 59052 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Rectangle(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600E6AD RID: 59053 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Rectangle(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600E6AE RID: 59054 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Rectangle(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0600E6AF RID: 59055 RVA: 0x002C6E50 File Offset: 0x002C5050
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (26 == namespaceId && "path" == name)
			{
				return new Path();
			}
			if (26 == namespaceId && "formulas" == name)
			{
				return new Formulas();
			}
			if (26 == namespaceId && "handles" == name)
			{
				return new ShapeHandles();
			}
			if (26 == namespaceId && "fill" == name)
			{
				return new Fill();
			}
			if (26 == namespaceId && "stroke" == name)
			{
				return new Stroke();
			}
			if (26 == namespaceId && "shadow" == name)
			{
				return new Shadow();
			}
			if (26 == namespaceId && "textbox" == name)
			{
				return new TextBox();
			}
			if (26 == namespaceId && "textpath" == name)
			{
				return new TextPath();
			}
			if (26 == namespaceId && "imagedata" == name)
			{
				return new ImageData();
			}
			if (27 == namespaceId && "skew" == name)
			{
				return new Skew();
			}
			if (27 == namespaceId && "extrusion" == name)
			{
				return new Extrusion();
			}
			if (27 == namespaceId && "callout" == name)
			{
				return new Callout();
			}
			if (27 == namespaceId && "lock" == name)
			{
				return new Lock();
			}
			if (27 == namespaceId && "clippath" == name)
			{
				return new ClipPath();
			}
			if (27 == namespaceId && "signatureline" == name)
			{
				return new SignatureLine();
			}
			if (28 == namespaceId && "wrap" == name)
			{
				return new TextWrap();
			}
			if (28 == namespaceId && "anchorlock" == name)
			{
				return new AnchorLock();
			}
			if (28 == namespaceId && "bordertop" == name)
			{
				return new TopBorder();
			}
			if (28 == namespaceId && "borderbottom" == name)
			{
				return new BottomBorder();
			}
			if (28 == namespaceId && "borderleft" == name)
			{
				return new LeftBorder();
			}
			if (28 == namespaceId && "borderright" == name)
			{
				return new RightBorder();
			}
			if (29 == namespaceId && "ClientData" == name)
			{
				return new ClientData();
			}
			if (30 == namespaceId && "textdata" == name)
			{
				return new TextData();
			}
			return null;
		}

		// Token: 0x0600E6B0 RID: 59056 RVA: 0x002C7088 File Offset: 0x002C5288
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "id" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "style" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "href" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "target" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "class" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "title" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "alt" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "coordsize" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "coordorigin" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "wrapcoords" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "print" == name)
			{
				return new TrueFalseValue();
			}
			if (27 == namespaceId && "spid" == name)
			{
				return new StringValue();
			}
			if (27 == namespaceId && "oned" == name)
			{
				return new TrueFalseValue();
			}
			if (27 == namespaceId && "regroupid" == name)
			{
				return new IntegerValue();
			}
			if (27 == namespaceId && "doubleclicknotify" == name)
			{
				return new TrueFalseValue();
			}
			if (27 == namespaceId && "button" == name)
			{
				return new TrueFalseValue();
			}
			if (27 == namespaceId && "userhidden" == name)
			{
				return new TrueFalseValue();
			}
			if (27 == namespaceId && "bullet" == name)
			{
				return new TrueFalseValue();
			}
			if (27 == namespaceId && "hr" == name)
			{
				return new TrueFalseValue();
			}
			if (27 == namespaceId && "hrstd" == name)
			{
				return new TrueFalseValue();
			}
			if (27 == namespaceId && "hrnoshade" == name)
			{
				return new TrueFalseValue();
			}
			if (27 == namespaceId && "hrpct" == name)
			{
				return new SingleValue();
			}
			if (27 == namespaceId && "hralign" == name)
			{
				return new EnumValue<HorizontalRuleAlignmentValues>();
			}
			if (27 == namespaceId && "allowincell" == name)
			{
				return new TrueFalseValue();
			}
			if (27 == namespaceId && "allowoverlap" == name)
			{
				return new TrueFalseValue();
			}
			if (27 == namespaceId && "userdrawn" == name)
			{
				return new TrueFalseValue();
			}
			if (27 == namespaceId && "bordertopcolor" == name)
			{
				return new StringValue();
			}
			if (27 == namespaceId && "borderleftcolor" == name)
			{
				return new StringValue();
			}
			if (27 == namespaceId && "borderbottomcolor" == name)
			{
				return new StringValue();
			}
			if (27 == namespaceId && "borderrightcolor" == name)
			{
				return new StringValue();
			}
			if (27 == namespaceId && "dgmlayout" == name)
			{
				return new IntegerValue();
			}
			if (27 == namespaceId && "dgmnodekind" == name)
			{
				return new IntegerValue();
			}
			if (27 == namespaceId && "dgmlayoutmru" == name)
			{
				return new IntegerValue();
			}
			if (27 == namespaceId && "insetmode" == name)
			{
				return new EnumValue<InsetMarginValues>();
			}
			if (namespaceId == 0 && "filled" == name)
			{
				return new TrueFalseValue();
			}
			if (namespaceId == 0 && "fillcolor" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "stroked" == name)
			{
				return new TrueFalseValue();
			}
			if (namespaceId == 0 && "strokecolor" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "strokeweight" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "insetpen" == name)
			{
				return new TrueFalseValue();
			}
			if (27 == namespaceId && "spt" == name)
			{
				return new Int32Value();
			}
			if (27 == namespaceId && "connectortype" == name)
			{
				return new EnumValue<ConnectorValues>();
			}
			if (27 == namespaceId && "bwmode" == name)
			{
				return new EnumValue<BlackAndWhiteModeValues>();
			}
			if (27 == namespaceId && "bwpure" == name)
			{
				return new EnumValue<BlackAndWhiteModeValues>();
			}
			if (27 == namespaceId && "bwnormal" == name)
			{
				return new EnumValue<BlackAndWhiteModeValues>();
			}
			if (27 == namespaceId && "forcedash" == name)
			{
				return new TrueFalseValue();
			}
			if (27 == namespaceId && "oleicon" == name)
			{
				return new TrueFalseValue();
			}
			if (27 == namespaceId && "ole" == name)
			{
				return new TrueFalseBlankValue();
			}
			if (27 == namespaceId && "preferrelative" == name)
			{
				return new TrueFalseValue();
			}
			if (27 == namespaceId && "cliptowrap" == name)
			{
				return new TrueFalseValue();
			}
			if (27 == namespaceId && "clip" == name)
			{
				return new TrueFalseValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0600E6B1 RID: 59057 RVA: 0x002C7543 File Offset: 0x002C5743
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Rectangle>(deep);
		}

		// Token: 0x04006EF1 RID: 28401
		private const string tagName = "rect";

		// Token: 0x04006EF2 RID: 28402
		private const byte tagNsId = 26;

		// Token: 0x04006EF3 RID: 28403
		internal const int ElementTypeIdConst = 12525;

		// Token: 0x04006EF4 RID: 28404
		private static string[] attributeTagNames = new string[]
		{
			"id", "style", "href", "target", "class", "title", "alt", "coordsize", "coordorigin", "wrapcoords",
			"print", "spid", "oned", "regroupid", "doubleclicknotify", "button", "userhidden", "bullet", "hr", "hrstd",
			"hrnoshade", "hrpct", "hralign", "allowincell", "allowoverlap", "userdrawn", "bordertopcolor", "borderleftcolor", "borderbottomcolor", "borderrightcolor",
			"dgmlayout", "dgmnodekind", "dgmlayoutmru", "insetmode", "filled", "fillcolor", "stroked", "strokecolor", "strokeweight", "insetpen",
			"spt", "connectortype", "bwmode", "bwpure", "bwnormal", "forcedash", "oleicon", "ole", "preferrelative", "cliptowrap",
			"clip"
		};

		// Token: 0x04006EF5 RID: 28405
		private static byte[] attributeNamespaceIds = new byte[]
		{
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 27, 27, 27, 27, 27, 27, 27, 27, 27,
			27, 27, 27, 27, 27, 27, 27, 27, 27, 27,
			27, 27, 27, 27, 0, 0, 0, 0, 0, 0,
			27, 27, 27, 27, 27, 27, 27, 27, 27, 27,
			27
		};
	}
}
