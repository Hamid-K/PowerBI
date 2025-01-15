using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Vml.Office;
using DocumentFormat.OpenXml.Vml.Presentation;
using DocumentFormat.OpenXml.Vml.Spreadsheet;
using DocumentFormat.OpenXml.Vml.Wordprocessing;

namespace DocumentFormat.OpenXml.Vml
{
	// Token: 0x02002256 RID: 8790
	[ChildElementInfo(typeof(AnchorLock))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(Stroke))]
	[ChildElementInfo(typeof(LeftBorder))]
	[ChildElementInfo(typeof(TextBox))]
	[ChildElementInfo(typeof(TextPath))]
	[ChildElementInfo(typeof(ImageData))]
	[ChildElementInfo(typeof(Skew))]
	[ChildElementInfo(typeof(Extrusion))]
	[ChildElementInfo(typeof(Callout))]
	[ChildElementInfo(typeof(Lock))]
	[ChildElementInfo(typeof(ClipPath))]
	[ChildElementInfo(typeof(ShapeHandles))]
	[ChildElementInfo(typeof(TextWrap))]
	[ChildElementInfo(typeof(Path))]
	[ChildElementInfo(typeof(TopBorder))]
	[ChildElementInfo(typeof(BottomBorder))]
	[ChildElementInfo(typeof(RightBorder))]
	[ChildElementInfo(typeof(ClientData))]
	[ChildElementInfo(typeof(TextData))]
	[ChildElementInfo(typeof(Shadow))]
	[ChildElementInfo(typeof(SignatureLine))]
	[ChildElementInfo(typeof(Fill))]
	[ChildElementInfo(typeof(Formulas))]
	internal class RoundRectangle : OpenXmlCompositeElement
	{
		// Token: 0x17003C66 RID: 15462
		// (get) Token: 0x0600E6B3 RID: 59059 RVA: 0x002C7740 File Offset: 0x002C5940
		public override string LocalName
		{
			get
			{
				return "roundrect";
			}
		}

		// Token: 0x17003C67 RID: 15463
		// (get) Token: 0x0600E6B4 RID: 59060 RVA: 0x00243C87 File Offset: 0x00241E87
		internal override byte NamespaceId
		{
			get
			{
				return 26;
			}
		}

		// Token: 0x17003C68 RID: 15464
		// (get) Token: 0x0600E6B5 RID: 59061 RVA: 0x002C7747 File Offset: 0x002C5947
		internal override int ElementTypeId
		{
			get
			{
				return 12526;
			}
		}

		// Token: 0x0600E6B6 RID: 59062 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17003C69 RID: 15465
		// (get) Token: 0x0600E6B7 RID: 59063 RVA: 0x002C774E File Offset: 0x002C594E
		internal override string[] AttributeTagNames
		{
			get
			{
				return RoundRectangle.attributeTagNames;
			}
		}

		// Token: 0x17003C6A RID: 15466
		// (get) Token: 0x0600E6B8 RID: 59064 RVA: 0x002C7755 File Offset: 0x002C5955
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return RoundRectangle.attributeNamespaceIds;
			}
		}

		// Token: 0x17003C6B RID: 15467
		// (get) Token: 0x0600E6B9 RID: 59065 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0600E6BA RID: 59066 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x17003C6C RID: 15468
		// (get) Token: 0x0600E6BB RID: 59067 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x0600E6BC RID: 59068 RVA: 0x002BD47A File Offset: 0x002BB67A
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

		// Token: 0x17003C6D RID: 15469
		// (get) Token: 0x0600E6BD RID: 59069 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x0600E6BE RID: 59070 RVA: 0x002BD494 File Offset: 0x002BB694
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

		// Token: 0x17003C6E RID: 15470
		// (get) Token: 0x0600E6BF RID: 59071 RVA: 0x002BDADA File Offset: 0x002BBCDA
		// (set) Token: 0x0600E6C0 RID: 59072 RVA: 0x002BD4AE File Offset: 0x002BB6AE
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

		// Token: 0x17003C6F RID: 15471
		// (get) Token: 0x0600E6C1 RID: 59073 RVA: 0x002BD4B9 File Offset: 0x002BB6B9
		// (set) Token: 0x0600E6C2 RID: 59074 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
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

		// Token: 0x17003C70 RID: 15472
		// (get) Token: 0x0600E6C3 RID: 59075 RVA: 0x002BE081 File Offset: 0x002BC281
		// (set) Token: 0x0600E6C4 RID: 59076 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
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

		// Token: 0x17003C71 RID: 15473
		// (get) Token: 0x0600E6C5 RID: 59077 RVA: 0x002BD4ED File Offset: 0x002BB6ED
		// (set) Token: 0x0600E6C6 RID: 59078 RVA: 0x002BD4FC File Offset: 0x002BB6FC
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

		// Token: 0x17003C72 RID: 15474
		// (get) Token: 0x0600E6C7 RID: 59079 RVA: 0x002BDB07 File Offset: 0x002BBD07
		// (set) Token: 0x0600E6C8 RID: 59080 RVA: 0x002BD516 File Offset: 0x002BB716
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

		// Token: 0x17003C73 RID: 15475
		// (get) Token: 0x0600E6C9 RID: 59081 RVA: 0x002BDB16 File Offset: 0x002BBD16
		// (set) Token: 0x0600E6CA RID: 59082 RVA: 0x002BD530 File Offset: 0x002BB730
		[SchemaAttr(0, "wrapcoords")]
		public StringValue WrapCoordinates
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

		// Token: 0x17003C74 RID: 15476
		// (get) Token: 0x0600E6CB RID: 59083 RVA: 0x002BEA5B File Offset: 0x002BCC5B
		// (set) Token: 0x0600E6CC RID: 59084 RVA: 0x002BD54B File Offset: 0x002BB74B
		[SchemaAttr(0, "print")]
		public TrueFalseValue Print
		{
			get
			{
				return (TrueFalseValue)base.Attributes[9];
			}
			set
			{
				base.Attributes[9] = value;
			}
		}

		// Token: 0x17003C75 RID: 15477
		// (get) Token: 0x0600E6CD RID: 59085 RVA: 0x002BDB35 File Offset: 0x002BBD35
		// (set) Token: 0x0600E6CE RID: 59086 RVA: 0x002BDB45 File Offset: 0x002BBD45
		[SchemaAttr(27, "spid")]
		public StringValue OptionalString
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

		// Token: 0x17003C76 RID: 15478
		// (get) Token: 0x0600E6CF RID: 59087 RVA: 0x002BE837 File Offset: 0x002BCA37
		// (set) Token: 0x0600E6D0 RID: 59088 RVA: 0x002BDB61 File Offset: 0x002BBD61
		[SchemaAttr(27, "oned")]
		public TrueFalseValue Oned
		{
			get
			{
				return (TrueFalseValue)base.Attributes[11];
			}
			set
			{
				base.Attributes[11] = value;
			}
		}

		// Token: 0x17003C77 RID: 15479
		// (get) Token: 0x0600E6D1 RID: 59089 RVA: 0x002C4715 File Offset: 0x002C2915
		// (set) Token: 0x0600E6D2 RID: 59090 RVA: 0x002BDB7D File Offset: 0x002BBD7D
		[SchemaAttr(27, "regroupid")]
		public IntegerValue RegroupId
		{
			get
			{
				return (IntegerValue)base.Attributes[12];
			}
			set
			{
				base.Attributes[12] = value;
			}
		}

		// Token: 0x17003C78 RID: 15480
		// (get) Token: 0x0600E6D3 RID: 59091 RVA: 0x002BE1F9 File Offset: 0x002BC3F9
		// (set) Token: 0x0600E6D4 RID: 59092 RVA: 0x002BE209 File Offset: 0x002BC409
		[SchemaAttr(27, "doubleclicknotify")]
		public TrueFalseValue DoubleClickNotify
		{
			get
			{
				return (TrueFalseValue)base.Attributes[13];
			}
			set
			{
				base.Attributes[13] = value;
			}
		}

		// Token: 0x17003C79 RID: 15481
		// (get) Token: 0x0600E6D5 RID: 59093 RVA: 0x002BFFE2 File Offset: 0x002BE1E2
		// (set) Token: 0x0600E6D6 RID: 59094 RVA: 0x002BE225 File Offset: 0x002BC425
		[SchemaAttr(27, "button")]
		public TrueFalseValue Button
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

		// Token: 0x17003C7A RID: 15482
		// (get) Token: 0x0600E6D7 RID: 59095 RVA: 0x002C02BC File Offset: 0x002BE4BC
		// (set) Token: 0x0600E6D8 RID: 59096 RVA: 0x002BE241 File Offset: 0x002BC441
		[SchemaAttr(27, "userhidden")]
		public TrueFalseValue UserHidden
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

		// Token: 0x17003C7B RID: 15483
		// (get) Token: 0x0600E6D9 RID: 59097 RVA: 0x002BEF3F File Offset: 0x002BD13F
		// (set) Token: 0x0600E6DA RID: 59098 RVA: 0x002BE25D File Offset: 0x002BC45D
		[SchemaAttr(27, "bullet")]
		public TrueFalseValue Bullet
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

		// Token: 0x17003C7C RID: 15484
		// (get) Token: 0x0600E6DB RID: 59099 RVA: 0x002C1390 File Offset: 0x002BF590
		// (set) Token: 0x0600E6DC RID: 59100 RVA: 0x002BE279 File Offset: 0x002BC479
		[SchemaAttr(27, "hr")]
		public TrueFalseValue Horizontal
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

		// Token: 0x17003C7D RID: 15485
		// (get) Token: 0x0600E6DD RID: 59101 RVA: 0x002C13A0 File Offset: 0x002BF5A0
		// (set) Token: 0x0600E6DE RID: 59102 RVA: 0x002BE295 File Offset: 0x002BC495
		[SchemaAttr(27, "hrstd")]
		public TrueFalseValue HorizontalStandard
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

		// Token: 0x17003C7E RID: 15486
		// (get) Token: 0x0600E6DF RID: 59103 RVA: 0x002C13B0 File Offset: 0x002BF5B0
		// (set) Token: 0x0600E6E0 RID: 59104 RVA: 0x002BE2B1 File Offset: 0x002BC4B1
		[SchemaAttr(27, "hrnoshade")]
		public TrueFalseValue HorizontalNoShade
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

		// Token: 0x17003C7F RID: 15487
		// (get) Token: 0x0600E6E1 RID: 59105 RVA: 0x002C4725 File Offset: 0x002C2925
		// (set) Token: 0x0600E6E2 RID: 59106 RVA: 0x002BE2CD File Offset: 0x002BC4CD
		[SchemaAttr(27, "hrpct")]
		public SingleValue HorizontalPercentage
		{
			get
			{
				return (SingleValue)base.Attributes[20];
			}
			set
			{
				base.Attributes[20] = value;
			}
		}

		// Token: 0x17003C80 RID: 15488
		// (get) Token: 0x0600E6E3 RID: 59107 RVA: 0x002C4735 File Offset: 0x002C2935
		// (set) Token: 0x0600E6E4 RID: 59108 RVA: 0x002BE2E9 File Offset: 0x002BC4E9
		[SchemaAttr(27, "hralign")]
		public EnumValue<HorizontalRuleAlignmentValues> HorizontalAlignment
		{
			get
			{
				return (EnumValue<HorizontalRuleAlignmentValues>)base.Attributes[21];
			}
			set
			{
				base.Attributes[21] = value;
			}
		}

		// Token: 0x17003C81 RID: 15489
		// (get) Token: 0x0600E6E5 RID: 59109 RVA: 0x002C4745 File Offset: 0x002C2945
		// (set) Token: 0x0600E6E6 RID: 59110 RVA: 0x002BE305 File Offset: 0x002BC505
		[SchemaAttr(27, "allowincell")]
		public TrueFalseValue AllowInCell
		{
			get
			{
				return (TrueFalseValue)base.Attributes[22];
			}
			set
			{
				base.Attributes[22] = value;
			}
		}

		// Token: 0x17003C82 RID: 15490
		// (get) Token: 0x0600E6E7 RID: 59111 RVA: 0x002BE311 File Offset: 0x002BC511
		// (set) Token: 0x0600E6E8 RID: 59112 RVA: 0x002BE321 File Offset: 0x002BC521
		[SchemaAttr(27, "allowoverlap")]
		public TrueFalseValue AllowOverlap
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

		// Token: 0x17003C83 RID: 15491
		// (get) Token: 0x0600E6E9 RID: 59113 RVA: 0x002C02EC File Offset: 0x002BE4EC
		// (set) Token: 0x0600E6EA RID: 59114 RVA: 0x002BE33D File Offset: 0x002BC53D
		[SchemaAttr(27, "userdrawn")]
		public TrueFalseValue UserDrawn
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

		// Token: 0x17003C84 RID: 15492
		// (get) Token: 0x0600E6EB RID: 59115 RVA: 0x002BE349 File Offset: 0x002BC549
		// (set) Token: 0x0600E6EC RID: 59116 RVA: 0x002BE359 File Offset: 0x002BC559
		[SchemaAttr(27, "bordertopcolor")]
		public StringValue BorderTopColor
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

		// Token: 0x17003C85 RID: 15493
		// (get) Token: 0x0600E6ED RID: 59117 RVA: 0x002BE365 File Offset: 0x002BC565
		// (set) Token: 0x0600E6EE RID: 59118 RVA: 0x002BE375 File Offset: 0x002BC575
		[SchemaAttr(27, "borderleftcolor")]
		public StringValue BorderLeftColor
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

		// Token: 0x17003C86 RID: 15494
		// (get) Token: 0x0600E6EF RID: 59119 RVA: 0x002C13E0 File Offset: 0x002BF5E0
		// (set) Token: 0x0600E6F0 RID: 59120 RVA: 0x002BE391 File Offset: 0x002BC591
		[SchemaAttr(27, "borderbottomcolor")]
		public StringValue BorderBottomColor
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

		// Token: 0x17003C87 RID: 15495
		// (get) Token: 0x0600E6F1 RID: 59121 RVA: 0x002BE39D File Offset: 0x002BC59D
		// (set) Token: 0x0600E6F2 RID: 59122 RVA: 0x002BE3AD File Offset: 0x002BC5AD
		[SchemaAttr(27, "borderrightcolor")]
		public StringValue BorderRightColor
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

		// Token: 0x17003C88 RID: 15496
		// (get) Token: 0x0600E6F3 RID: 59123 RVA: 0x002C4755 File Offset: 0x002C2955
		// (set) Token: 0x0600E6F4 RID: 59124 RVA: 0x002BE3C9 File Offset: 0x002BC5C9
		[SchemaAttr(27, "dgmlayout")]
		public IntegerValue DiagramLayout
		{
			get
			{
				return (IntegerValue)base.Attributes[29];
			}
			set
			{
				base.Attributes[29] = value;
			}
		}

		// Token: 0x17003C89 RID: 15497
		// (get) Token: 0x0600E6F5 RID: 59125 RVA: 0x002C13F0 File Offset: 0x002BF5F0
		// (set) Token: 0x0600E6F6 RID: 59126 RVA: 0x002BE3E5 File Offset: 0x002BC5E5
		[SchemaAttr(27, "dgmnodekind")]
		public IntegerValue DiagramNodeKind
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

		// Token: 0x17003C8A RID: 15498
		// (get) Token: 0x0600E6F7 RID: 59127 RVA: 0x002C1400 File Offset: 0x002BF600
		// (set) Token: 0x0600E6F8 RID: 59128 RVA: 0x002C1410 File Offset: 0x002BF610
		[SchemaAttr(27, "dgmlayoutmru")]
		public IntegerValue DiagramLayoutMostRecentUsed
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

		// Token: 0x17003C8B RID: 15499
		// (get) Token: 0x0600E6F9 RID: 59129 RVA: 0x002C4765 File Offset: 0x002C2965
		// (set) Token: 0x0600E6FA RID: 59130 RVA: 0x002C142C File Offset: 0x002BF62C
		[SchemaAttr(27, "insetmode")]
		public EnumValue<InsetMarginValues> InsetMode
		{
			get
			{
				return (EnumValue<InsetMarginValues>)base.Attributes[32];
			}
			set
			{
				base.Attributes[32] = value;
			}
		}

		// Token: 0x17003C8C RID: 15500
		// (get) Token: 0x0600E6FB RID: 59131 RVA: 0x002C4775 File Offset: 0x002C2975
		// (set) Token: 0x0600E6FC RID: 59132 RVA: 0x002C1448 File Offset: 0x002BF648
		[SchemaAttr(0, "filled")]
		public TrueFalseValue Filled
		{
			get
			{
				return (TrueFalseValue)base.Attributes[33];
			}
			set
			{
				base.Attributes[33] = value;
			}
		}

		// Token: 0x17003C8D RID: 15501
		// (get) Token: 0x0600E6FD RID: 59133 RVA: 0x002C4785 File Offset: 0x002C2985
		// (set) Token: 0x0600E6FE RID: 59134 RVA: 0x002C1464 File Offset: 0x002BF664
		[SchemaAttr(0, "fillcolor")]
		public StringValue FillColor
		{
			get
			{
				return (StringValue)base.Attributes[34];
			}
			set
			{
				base.Attributes[34] = value;
			}
		}

		// Token: 0x17003C8E RID: 15502
		// (get) Token: 0x0600E6FF RID: 59135 RVA: 0x002C3362 File Offset: 0x002C1562
		// (set) Token: 0x0600E700 RID: 59136 RVA: 0x002C1480 File Offset: 0x002BF680
		[SchemaAttr(0, "stroked")]
		public TrueFalseValue Stroked
		{
			get
			{
				return (TrueFalseValue)base.Attributes[35];
			}
			set
			{
				base.Attributes[35] = value;
			}
		}

		// Token: 0x17003C8F RID: 15503
		// (get) Token: 0x0600E701 RID: 59137 RVA: 0x002C4795 File Offset: 0x002C2995
		// (set) Token: 0x0600E702 RID: 59138 RVA: 0x002C149C File Offset: 0x002BF69C
		[SchemaAttr(0, "strokecolor")]
		public StringValue StrokeColor
		{
			get
			{
				return (StringValue)base.Attributes[36];
			}
			set
			{
				base.Attributes[36] = value;
			}
		}

		// Token: 0x17003C90 RID: 15504
		// (get) Token: 0x0600E703 RID: 59139 RVA: 0x002C14A8 File Offset: 0x002BF6A8
		// (set) Token: 0x0600E704 RID: 59140 RVA: 0x002C14B8 File Offset: 0x002BF6B8
		[SchemaAttr(0, "strokeweight")]
		public StringValue StrokeWeight
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

		// Token: 0x17003C91 RID: 15505
		// (get) Token: 0x0600E705 RID: 59141 RVA: 0x002C3392 File Offset: 0x002C1592
		// (set) Token: 0x0600E706 RID: 59142 RVA: 0x002C14D4 File Offset: 0x002BF6D4
		[SchemaAttr(0, "insetpen")]
		public TrueFalseValue InsetPen
		{
			get
			{
				return (TrueFalseValue)base.Attributes[38];
			}
			set
			{
				base.Attributes[38] = value;
			}
		}

		// Token: 0x17003C92 RID: 15506
		// (get) Token: 0x0600E707 RID: 59143 RVA: 0x002C47A5 File Offset: 0x002C29A5
		// (set) Token: 0x0600E708 RID: 59144 RVA: 0x002C14F0 File Offset: 0x002BF6F0
		[SchemaAttr(27, "spt")]
		public Int32Value OptionalNumber
		{
			get
			{
				return (Int32Value)base.Attributes[39];
			}
			set
			{
				base.Attributes[39] = value;
			}
		}

		// Token: 0x17003C93 RID: 15507
		// (get) Token: 0x0600E709 RID: 59145 RVA: 0x002C47B5 File Offset: 0x002C29B5
		// (set) Token: 0x0600E70A RID: 59146 RVA: 0x002C150C File Offset: 0x002BF70C
		[SchemaAttr(27, "connectortype")]
		public EnumValue<ConnectorValues> ConnectorType
		{
			get
			{
				return (EnumValue<ConnectorValues>)base.Attributes[40];
			}
			set
			{
				base.Attributes[40] = value;
			}
		}

		// Token: 0x17003C94 RID: 15508
		// (get) Token: 0x0600E70B RID: 59147 RVA: 0x002C47C5 File Offset: 0x002C29C5
		// (set) Token: 0x0600E70C RID: 59148 RVA: 0x002C1528 File Offset: 0x002BF728
		[SchemaAttr(27, "bwmode")]
		public EnumValue<BlackAndWhiteModeValues> BlackWhiteMode
		{
			get
			{
				return (EnumValue<BlackAndWhiteModeValues>)base.Attributes[41];
			}
			set
			{
				base.Attributes[41] = value;
			}
		}

		// Token: 0x17003C95 RID: 15509
		// (get) Token: 0x0600E70D RID: 59149 RVA: 0x002C1534 File Offset: 0x002BF734
		// (set) Token: 0x0600E70E RID: 59150 RVA: 0x002C1544 File Offset: 0x002BF744
		[SchemaAttr(27, "bwpure")]
		public EnumValue<BlackAndWhiteModeValues> PureBlackWhiteMode
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

		// Token: 0x17003C96 RID: 15510
		// (get) Token: 0x0600E70F RID: 59151 RVA: 0x002C1550 File Offset: 0x002BF750
		// (set) Token: 0x0600E710 RID: 59152 RVA: 0x002C1560 File Offset: 0x002BF760
		[SchemaAttr(27, "bwnormal")]
		public EnumValue<BlackAndWhiteModeValues> NormalBlackWhiteMode
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

		// Token: 0x17003C97 RID: 15511
		// (get) Token: 0x0600E711 RID: 59153 RVA: 0x002C47D5 File Offset: 0x002C29D5
		// (set) Token: 0x0600E712 RID: 59154 RVA: 0x002C157C File Offset: 0x002BF77C
		[SchemaAttr(27, "forcedash")]
		public TrueFalseValue ForceDash
		{
			get
			{
				return (TrueFalseValue)base.Attributes[44];
			}
			set
			{
				base.Attributes[44] = value;
			}
		}

		// Token: 0x17003C98 RID: 15512
		// (get) Token: 0x0600E713 RID: 59155 RVA: 0x002C1588 File Offset: 0x002BF788
		// (set) Token: 0x0600E714 RID: 59156 RVA: 0x002C1598 File Offset: 0x002BF798
		[SchemaAttr(27, "oleicon")]
		public TrueFalseValue OleIcon
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

		// Token: 0x17003C99 RID: 15513
		// (get) Token: 0x0600E715 RID: 59157 RVA: 0x002C47E5 File Offset: 0x002C29E5
		// (set) Token: 0x0600E716 RID: 59158 RVA: 0x002C15B4 File Offset: 0x002BF7B4
		[SchemaAttr(27, "ole")]
		public TrueFalseBlankValue Ole
		{
			get
			{
				return (TrueFalseBlankValue)base.Attributes[46];
			}
			set
			{
				base.Attributes[46] = value;
			}
		}

		// Token: 0x17003C9A RID: 15514
		// (get) Token: 0x0600E717 RID: 59159 RVA: 0x002C47F5 File Offset: 0x002C29F5
		// (set) Token: 0x0600E718 RID: 59160 RVA: 0x002C15D0 File Offset: 0x002BF7D0
		[SchemaAttr(27, "preferrelative")]
		public TrueFalseValue PreferRelative
		{
			get
			{
				return (TrueFalseValue)base.Attributes[47];
			}
			set
			{
				base.Attributes[47] = value;
			}
		}

		// Token: 0x17003C9B RID: 15515
		// (get) Token: 0x0600E719 RID: 59161 RVA: 0x002C15DC File Offset: 0x002BF7DC
		// (set) Token: 0x0600E71A RID: 59162 RVA: 0x002C15EC File Offset: 0x002BF7EC
		[SchemaAttr(27, "cliptowrap")]
		public TrueFalseValue ClipToWrap
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

		// Token: 0x17003C9C RID: 15516
		// (get) Token: 0x0600E71B RID: 59163 RVA: 0x002C15F8 File Offset: 0x002BF7F8
		// (set) Token: 0x0600E71C RID: 59164 RVA: 0x002C1608 File Offset: 0x002BF808
		[SchemaAttr(27, "clip")]
		public TrueFalseValue Clip
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

		// Token: 0x17003C9D RID: 15517
		// (get) Token: 0x0600E71D RID: 59165 RVA: 0x002C4805 File Offset: 0x002C2A05
		// (set) Token: 0x0600E71E RID: 59166 RVA: 0x002C1624 File Offset: 0x002BF824
		[SchemaAttr(0, "arcsize")]
		public StringValue ArcSize
		{
			get
			{
				return (StringValue)base.Attributes[50];
			}
			set
			{
				base.Attributes[50] = value;
			}
		}

		// Token: 0x0600E71F RID: 59167 RVA: 0x00293ECF File Offset: 0x002920CF
		public RoundRectangle()
		{
		}

		// Token: 0x0600E720 RID: 59168 RVA: 0x00293ED7 File Offset: 0x002920D7
		public RoundRectangle(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600E721 RID: 59169 RVA: 0x00293EE0 File Offset: 0x002920E0
		public RoundRectangle(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600E722 RID: 59170 RVA: 0x00293EE9 File Offset: 0x002920E9
		public RoundRectangle(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0600E723 RID: 59171 RVA: 0x002C775C File Offset: 0x002C595C
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

		// Token: 0x0600E724 RID: 59172 RVA: 0x002C7994 File Offset: 0x002C5B94
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
			if (namespaceId == 0 && "arcsize" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0600E725 RID: 59173 RVA: 0x002C7E4F File Offset: 0x002C604F
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<RoundRectangle>(deep);
		}

		// Token: 0x04006EF6 RID: 28406
		private const string tagName = "roundrect";

		// Token: 0x04006EF7 RID: 28407
		private const byte tagNsId = 26;

		// Token: 0x04006EF8 RID: 28408
		internal const int ElementTypeIdConst = 12526;

		// Token: 0x04006EF9 RID: 28409
		private static string[] attributeTagNames = new string[]
		{
			"id", "style", "href", "target", "class", "title", "alt", "coordsize", "wrapcoords", "print",
			"spid", "oned", "regroupid", "doubleclicknotify", "button", "userhidden", "bullet", "hr", "hrstd", "hrnoshade",
			"hrpct", "hralign", "allowincell", "allowoverlap", "userdrawn", "bordertopcolor", "borderleftcolor", "borderbottomcolor", "borderrightcolor", "dgmlayout",
			"dgmnodekind", "dgmlayoutmru", "insetmode", "filled", "fillcolor", "stroked", "strokecolor", "strokeweight", "insetpen", "spt",
			"connectortype", "bwmode", "bwpure", "bwnormal", "forcedash", "oleicon", "ole", "preferrelative", "cliptowrap", "clip",
			"arcsize"
		};

		// Token: 0x04006EFA RID: 28410
		private static byte[] attributeNamespaceIds = new byte[]
		{
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			27, 27, 27, 27, 27, 27, 27, 27, 27, 27,
			27, 27, 27, 27, 27, 27, 27, 27, 27, 27,
			27, 27, 27, 0, 0, 0, 0, 0, 0, 27,
			27, 27, 27, 27, 27, 27, 27, 27, 27, 27,
			0
		};
	}
}
