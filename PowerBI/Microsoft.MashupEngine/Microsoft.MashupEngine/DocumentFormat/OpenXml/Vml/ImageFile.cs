using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Vml.Office;
using DocumentFormat.OpenXml.Vml.Presentation;
using DocumentFormat.OpenXml.Vml.Spreadsheet;
using DocumentFormat.OpenXml.Vml.Wordprocessing;

namespace DocumentFormat.OpenXml.Vml
{
	// Token: 0x02002251 RID: 8785
	[ChildElementInfo(typeof(ClipPath))]
	[ChildElementInfo(typeof(ShapeHandles))]
	[ChildElementInfo(typeof(Stroke))]
	[ChildElementInfo(typeof(Shadow))]
	[ChildElementInfo(typeof(TextBox))]
	[ChildElementInfo(typeof(TextPath))]
	[ChildElementInfo(typeof(ImageData))]
	[ChildElementInfo(typeof(Skew))]
	[ChildElementInfo(typeof(Extrusion))]
	[ChildElementInfo(typeof(Callout))]
	[ChildElementInfo(typeof(Lock))]
	[ChildElementInfo(typeof(Path))]
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
	[ChildElementInfo(typeof(Fill))]
	[ChildElementInfo(typeof(Formulas))]
	internal class ImageFile : OpenXmlCompositeElement
	{
		// Token: 0x17003B42 RID: 15170
		// (get) Token: 0x0600E457 RID: 58455 RVA: 0x002AD9B3 File Offset: 0x002ABBB3
		public override string LocalName
		{
			get
			{
				return "image";
			}
		}

		// Token: 0x17003B43 RID: 15171
		// (get) Token: 0x0600E458 RID: 58456 RVA: 0x00243C87 File Offset: 0x00241E87
		internal override byte NamespaceId
		{
			get
			{
				return 26;
			}
		}

		// Token: 0x17003B44 RID: 15172
		// (get) Token: 0x0600E459 RID: 58457 RVA: 0x002C4700 File Offset: 0x002C2900
		internal override int ElementTypeId
		{
			get
			{
				return 12521;
			}
		}

		// Token: 0x0600E45A RID: 58458 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17003B45 RID: 15173
		// (get) Token: 0x0600E45B RID: 58459 RVA: 0x002C4707 File Offset: 0x002C2907
		internal override string[] AttributeTagNames
		{
			get
			{
				return ImageFile.attributeTagNames;
			}
		}

		// Token: 0x17003B46 RID: 15174
		// (get) Token: 0x0600E45C RID: 58460 RVA: 0x002C470E File Offset: 0x002C290E
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return ImageFile.attributeNamespaceIds;
			}
		}

		// Token: 0x17003B47 RID: 15175
		// (get) Token: 0x0600E45D RID: 58461 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0600E45E RID: 58462 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x17003B48 RID: 15176
		// (get) Token: 0x0600E45F RID: 58463 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x0600E460 RID: 58464 RVA: 0x002BD47A File Offset: 0x002BB67A
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

		// Token: 0x17003B49 RID: 15177
		// (get) Token: 0x0600E461 RID: 58465 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x0600E462 RID: 58466 RVA: 0x002BD494 File Offset: 0x002BB694
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

		// Token: 0x17003B4A RID: 15178
		// (get) Token: 0x0600E463 RID: 58467 RVA: 0x002BDADA File Offset: 0x002BBCDA
		// (set) Token: 0x0600E464 RID: 58468 RVA: 0x002BD4AE File Offset: 0x002BB6AE
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

		// Token: 0x17003B4B RID: 15179
		// (get) Token: 0x0600E465 RID: 58469 RVA: 0x002BD4B9 File Offset: 0x002BB6B9
		// (set) Token: 0x0600E466 RID: 58470 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
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

		// Token: 0x17003B4C RID: 15180
		// (get) Token: 0x0600E467 RID: 58471 RVA: 0x002BE081 File Offset: 0x002BC281
		// (set) Token: 0x0600E468 RID: 58472 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
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

		// Token: 0x17003B4D RID: 15181
		// (get) Token: 0x0600E469 RID: 58473 RVA: 0x002BD4ED File Offset: 0x002BB6ED
		// (set) Token: 0x0600E46A RID: 58474 RVA: 0x002BD4FC File Offset: 0x002BB6FC
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

		// Token: 0x17003B4E RID: 15182
		// (get) Token: 0x0600E46B RID: 58475 RVA: 0x002BDB07 File Offset: 0x002BBD07
		// (set) Token: 0x0600E46C RID: 58476 RVA: 0x002BD516 File Offset: 0x002BB716
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

		// Token: 0x17003B4F RID: 15183
		// (get) Token: 0x0600E46D RID: 58477 RVA: 0x002BDB16 File Offset: 0x002BBD16
		// (set) Token: 0x0600E46E RID: 58478 RVA: 0x002BD530 File Offset: 0x002BB730
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

		// Token: 0x17003B50 RID: 15184
		// (get) Token: 0x0600E46F RID: 58479 RVA: 0x002BEA5B File Offset: 0x002BCC5B
		// (set) Token: 0x0600E470 RID: 58480 RVA: 0x002BD54B File Offset: 0x002BB74B
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

		// Token: 0x17003B51 RID: 15185
		// (get) Token: 0x0600E471 RID: 58481 RVA: 0x002BDB35 File Offset: 0x002BBD35
		// (set) Token: 0x0600E472 RID: 58482 RVA: 0x002BDB45 File Offset: 0x002BBD45
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

		// Token: 0x17003B52 RID: 15186
		// (get) Token: 0x0600E473 RID: 58483 RVA: 0x002BE837 File Offset: 0x002BCA37
		// (set) Token: 0x0600E474 RID: 58484 RVA: 0x002BDB61 File Offset: 0x002BBD61
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

		// Token: 0x17003B53 RID: 15187
		// (get) Token: 0x0600E475 RID: 58485 RVA: 0x002C4715 File Offset: 0x002C2915
		// (set) Token: 0x0600E476 RID: 58486 RVA: 0x002BDB7D File Offset: 0x002BBD7D
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

		// Token: 0x17003B54 RID: 15188
		// (get) Token: 0x0600E477 RID: 58487 RVA: 0x002BE1F9 File Offset: 0x002BC3F9
		// (set) Token: 0x0600E478 RID: 58488 RVA: 0x002BE209 File Offset: 0x002BC409
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

		// Token: 0x17003B55 RID: 15189
		// (get) Token: 0x0600E479 RID: 58489 RVA: 0x002BFFE2 File Offset: 0x002BE1E2
		// (set) Token: 0x0600E47A RID: 58490 RVA: 0x002BE225 File Offset: 0x002BC425
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

		// Token: 0x17003B56 RID: 15190
		// (get) Token: 0x0600E47B RID: 58491 RVA: 0x002C02BC File Offset: 0x002BE4BC
		// (set) Token: 0x0600E47C RID: 58492 RVA: 0x002BE241 File Offset: 0x002BC441
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

		// Token: 0x17003B57 RID: 15191
		// (get) Token: 0x0600E47D RID: 58493 RVA: 0x002BEF3F File Offset: 0x002BD13F
		// (set) Token: 0x0600E47E RID: 58494 RVA: 0x002BE25D File Offset: 0x002BC45D
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

		// Token: 0x17003B58 RID: 15192
		// (get) Token: 0x0600E47F RID: 58495 RVA: 0x002C1390 File Offset: 0x002BF590
		// (set) Token: 0x0600E480 RID: 58496 RVA: 0x002BE279 File Offset: 0x002BC479
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

		// Token: 0x17003B59 RID: 15193
		// (get) Token: 0x0600E481 RID: 58497 RVA: 0x002C13A0 File Offset: 0x002BF5A0
		// (set) Token: 0x0600E482 RID: 58498 RVA: 0x002BE295 File Offset: 0x002BC495
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

		// Token: 0x17003B5A RID: 15194
		// (get) Token: 0x0600E483 RID: 58499 RVA: 0x002C13B0 File Offset: 0x002BF5B0
		// (set) Token: 0x0600E484 RID: 58500 RVA: 0x002BE2B1 File Offset: 0x002BC4B1
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

		// Token: 0x17003B5B RID: 15195
		// (get) Token: 0x0600E485 RID: 58501 RVA: 0x002C4725 File Offset: 0x002C2925
		// (set) Token: 0x0600E486 RID: 58502 RVA: 0x002BE2CD File Offset: 0x002BC4CD
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

		// Token: 0x17003B5C RID: 15196
		// (get) Token: 0x0600E487 RID: 58503 RVA: 0x002C4735 File Offset: 0x002C2935
		// (set) Token: 0x0600E488 RID: 58504 RVA: 0x002BE2E9 File Offset: 0x002BC4E9
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

		// Token: 0x17003B5D RID: 15197
		// (get) Token: 0x0600E489 RID: 58505 RVA: 0x002C4745 File Offset: 0x002C2945
		// (set) Token: 0x0600E48A RID: 58506 RVA: 0x002BE305 File Offset: 0x002BC505
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

		// Token: 0x17003B5E RID: 15198
		// (get) Token: 0x0600E48B RID: 58507 RVA: 0x002BE311 File Offset: 0x002BC511
		// (set) Token: 0x0600E48C RID: 58508 RVA: 0x002BE321 File Offset: 0x002BC521
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

		// Token: 0x17003B5F RID: 15199
		// (get) Token: 0x0600E48D RID: 58509 RVA: 0x002C02EC File Offset: 0x002BE4EC
		// (set) Token: 0x0600E48E RID: 58510 RVA: 0x002BE33D File Offset: 0x002BC53D
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

		// Token: 0x17003B60 RID: 15200
		// (get) Token: 0x0600E48F RID: 58511 RVA: 0x002BE349 File Offset: 0x002BC549
		// (set) Token: 0x0600E490 RID: 58512 RVA: 0x002BE359 File Offset: 0x002BC559
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

		// Token: 0x17003B61 RID: 15201
		// (get) Token: 0x0600E491 RID: 58513 RVA: 0x002BE365 File Offset: 0x002BC565
		// (set) Token: 0x0600E492 RID: 58514 RVA: 0x002BE375 File Offset: 0x002BC575
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

		// Token: 0x17003B62 RID: 15202
		// (get) Token: 0x0600E493 RID: 58515 RVA: 0x002C13E0 File Offset: 0x002BF5E0
		// (set) Token: 0x0600E494 RID: 58516 RVA: 0x002BE391 File Offset: 0x002BC591
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

		// Token: 0x17003B63 RID: 15203
		// (get) Token: 0x0600E495 RID: 58517 RVA: 0x002BE39D File Offset: 0x002BC59D
		// (set) Token: 0x0600E496 RID: 58518 RVA: 0x002BE3AD File Offset: 0x002BC5AD
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

		// Token: 0x17003B64 RID: 15204
		// (get) Token: 0x0600E497 RID: 58519 RVA: 0x002C4755 File Offset: 0x002C2955
		// (set) Token: 0x0600E498 RID: 58520 RVA: 0x002BE3C9 File Offset: 0x002BC5C9
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

		// Token: 0x17003B65 RID: 15205
		// (get) Token: 0x0600E499 RID: 58521 RVA: 0x002C13F0 File Offset: 0x002BF5F0
		// (set) Token: 0x0600E49A RID: 58522 RVA: 0x002BE3E5 File Offset: 0x002BC5E5
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

		// Token: 0x17003B66 RID: 15206
		// (get) Token: 0x0600E49B RID: 58523 RVA: 0x002C1400 File Offset: 0x002BF600
		// (set) Token: 0x0600E49C RID: 58524 RVA: 0x002C1410 File Offset: 0x002BF610
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

		// Token: 0x17003B67 RID: 15207
		// (get) Token: 0x0600E49D RID: 58525 RVA: 0x002C4765 File Offset: 0x002C2965
		// (set) Token: 0x0600E49E RID: 58526 RVA: 0x002C142C File Offset: 0x002BF62C
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

		// Token: 0x17003B68 RID: 15208
		// (get) Token: 0x0600E49F RID: 58527 RVA: 0x002C4775 File Offset: 0x002C2975
		// (set) Token: 0x0600E4A0 RID: 58528 RVA: 0x002C1448 File Offset: 0x002BF648
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

		// Token: 0x17003B69 RID: 15209
		// (get) Token: 0x0600E4A1 RID: 58529 RVA: 0x002C4785 File Offset: 0x002C2985
		// (set) Token: 0x0600E4A2 RID: 58530 RVA: 0x002C1464 File Offset: 0x002BF664
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

		// Token: 0x17003B6A RID: 15210
		// (get) Token: 0x0600E4A3 RID: 58531 RVA: 0x002C3362 File Offset: 0x002C1562
		// (set) Token: 0x0600E4A4 RID: 58532 RVA: 0x002C1480 File Offset: 0x002BF680
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

		// Token: 0x17003B6B RID: 15211
		// (get) Token: 0x0600E4A5 RID: 58533 RVA: 0x002C4795 File Offset: 0x002C2995
		// (set) Token: 0x0600E4A6 RID: 58534 RVA: 0x002C149C File Offset: 0x002BF69C
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

		// Token: 0x17003B6C RID: 15212
		// (get) Token: 0x0600E4A7 RID: 58535 RVA: 0x002C14A8 File Offset: 0x002BF6A8
		// (set) Token: 0x0600E4A8 RID: 58536 RVA: 0x002C14B8 File Offset: 0x002BF6B8
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

		// Token: 0x17003B6D RID: 15213
		// (get) Token: 0x0600E4A9 RID: 58537 RVA: 0x002C3392 File Offset: 0x002C1592
		// (set) Token: 0x0600E4AA RID: 58538 RVA: 0x002C14D4 File Offset: 0x002BF6D4
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

		// Token: 0x17003B6E RID: 15214
		// (get) Token: 0x0600E4AB RID: 58539 RVA: 0x002C47A5 File Offset: 0x002C29A5
		// (set) Token: 0x0600E4AC RID: 58540 RVA: 0x002C14F0 File Offset: 0x002BF6F0
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

		// Token: 0x17003B6F RID: 15215
		// (get) Token: 0x0600E4AD RID: 58541 RVA: 0x002C47B5 File Offset: 0x002C29B5
		// (set) Token: 0x0600E4AE RID: 58542 RVA: 0x002C150C File Offset: 0x002BF70C
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

		// Token: 0x17003B70 RID: 15216
		// (get) Token: 0x0600E4AF RID: 58543 RVA: 0x002C47C5 File Offset: 0x002C29C5
		// (set) Token: 0x0600E4B0 RID: 58544 RVA: 0x002C1528 File Offset: 0x002BF728
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

		// Token: 0x17003B71 RID: 15217
		// (get) Token: 0x0600E4B1 RID: 58545 RVA: 0x002C1534 File Offset: 0x002BF734
		// (set) Token: 0x0600E4B2 RID: 58546 RVA: 0x002C1544 File Offset: 0x002BF744
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

		// Token: 0x17003B72 RID: 15218
		// (get) Token: 0x0600E4B3 RID: 58547 RVA: 0x002C1550 File Offset: 0x002BF750
		// (set) Token: 0x0600E4B4 RID: 58548 RVA: 0x002C1560 File Offset: 0x002BF760
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

		// Token: 0x17003B73 RID: 15219
		// (get) Token: 0x0600E4B5 RID: 58549 RVA: 0x002C47D5 File Offset: 0x002C29D5
		// (set) Token: 0x0600E4B6 RID: 58550 RVA: 0x002C157C File Offset: 0x002BF77C
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

		// Token: 0x17003B74 RID: 15220
		// (get) Token: 0x0600E4B7 RID: 58551 RVA: 0x002C1588 File Offset: 0x002BF788
		// (set) Token: 0x0600E4B8 RID: 58552 RVA: 0x002C1598 File Offset: 0x002BF798
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

		// Token: 0x17003B75 RID: 15221
		// (get) Token: 0x0600E4B9 RID: 58553 RVA: 0x002C47E5 File Offset: 0x002C29E5
		// (set) Token: 0x0600E4BA RID: 58554 RVA: 0x002C15B4 File Offset: 0x002BF7B4
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

		// Token: 0x17003B76 RID: 15222
		// (get) Token: 0x0600E4BB RID: 58555 RVA: 0x002C47F5 File Offset: 0x002C29F5
		// (set) Token: 0x0600E4BC RID: 58556 RVA: 0x002C15D0 File Offset: 0x002BF7D0
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

		// Token: 0x17003B77 RID: 15223
		// (get) Token: 0x0600E4BD RID: 58557 RVA: 0x002C15DC File Offset: 0x002BF7DC
		// (set) Token: 0x0600E4BE RID: 58558 RVA: 0x002C15EC File Offset: 0x002BF7EC
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

		// Token: 0x17003B78 RID: 15224
		// (get) Token: 0x0600E4BF RID: 58559 RVA: 0x002C15F8 File Offset: 0x002BF7F8
		// (set) Token: 0x0600E4C0 RID: 58560 RVA: 0x002C1608 File Offset: 0x002BF808
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

		// Token: 0x17003B79 RID: 15225
		// (get) Token: 0x0600E4C1 RID: 58561 RVA: 0x002C4805 File Offset: 0x002C2A05
		// (set) Token: 0x0600E4C2 RID: 58562 RVA: 0x002C1624 File Offset: 0x002BF824
		[SchemaAttr(0, "src")]
		public StringValue Source
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

		// Token: 0x17003B7A RID: 15226
		// (get) Token: 0x0600E4C3 RID: 58563 RVA: 0x002C1630 File Offset: 0x002BF830
		// (set) Token: 0x0600E4C4 RID: 58564 RVA: 0x002C1640 File Offset: 0x002BF840
		[SchemaAttr(0, "cropleft")]
		public StringValue CropLeft
		{
			get
			{
				return (StringValue)base.Attributes[51];
			}
			set
			{
				base.Attributes[51] = value;
			}
		}

		// Token: 0x17003B7B RID: 15227
		// (get) Token: 0x0600E4C5 RID: 58565 RVA: 0x002C164C File Offset: 0x002BF84C
		// (set) Token: 0x0600E4C6 RID: 58566 RVA: 0x002C165C File Offset: 0x002BF85C
		[SchemaAttr(0, "croptop")]
		public StringValue CropTop
		{
			get
			{
				return (StringValue)base.Attributes[52];
			}
			set
			{
				base.Attributes[52] = value;
			}
		}

		// Token: 0x17003B7C RID: 15228
		// (get) Token: 0x0600E4C7 RID: 58567 RVA: 0x002C1668 File Offset: 0x002BF868
		// (set) Token: 0x0600E4C8 RID: 58568 RVA: 0x002C1678 File Offset: 0x002BF878
		[SchemaAttr(0, "cropright")]
		public StringValue CropRight
		{
			get
			{
				return (StringValue)base.Attributes[53];
			}
			set
			{
				base.Attributes[53] = value;
			}
		}

		// Token: 0x17003B7D RID: 15229
		// (get) Token: 0x0600E4C9 RID: 58569 RVA: 0x002C3D81 File Offset: 0x002C1F81
		// (set) Token: 0x0600E4CA RID: 58570 RVA: 0x002C1694 File Offset: 0x002BF894
		[SchemaAttr(0, "cropbottom")]
		public StringValue CropBottom
		{
			get
			{
				return (StringValue)base.Attributes[54];
			}
			set
			{
				base.Attributes[54] = value;
			}
		}

		// Token: 0x17003B7E RID: 15230
		// (get) Token: 0x0600E4CB RID: 58571 RVA: 0x002C16A0 File Offset: 0x002BF8A0
		// (set) Token: 0x0600E4CC RID: 58572 RVA: 0x002C16B0 File Offset: 0x002BF8B0
		[SchemaAttr(0, "gain")]
		public StringValue Gain
		{
			get
			{
				return (StringValue)base.Attributes[55];
			}
			set
			{
				base.Attributes[55] = value;
			}
		}

		// Token: 0x17003B7F RID: 15231
		// (get) Token: 0x0600E4CD RID: 58573 RVA: 0x002C4815 File Offset: 0x002C2A15
		// (set) Token: 0x0600E4CE RID: 58574 RVA: 0x002C4825 File Offset: 0x002C2A25
		[SchemaAttr(0, "blacklevel")]
		public StringValue BlackLevel
		{
			get
			{
				return (StringValue)base.Attributes[56];
			}
			set
			{
				base.Attributes[56] = value;
			}
		}

		// Token: 0x17003B80 RID: 15232
		// (get) Token: 0x0600E4CF RID: 58575 RVA: 0x002C4831 File Offset: 0x002C2A31
		// (set) Token: 0x0600E4D0 RID: 58576 RVA: 0x002C4841 File Offset: 0x002C2A41
		[SchemaAttr(0, "gamma")]
		public StringValue Gamma
		{
			get
			{
				return (StringValue)base.Attributes[57];
			}
			set
			{
				base.Attributes[57] = value;
			}
		}

		// Token: 0x17003B81 RID: 15233
		// (get) Token: 0x0600E4D1 RID: 58577 RVA: 0x002C484D File Offset: 0x002C2A4D
		// (set) Token: 0x0600E4D2 RID: 58578 RVA: 0x002C485D File Offset: 0x002C2A5D
		[SchemaAttr(0, "grayscale")]
		public TrueFalseValue GrayScale
		{
			get
			{
				return (TrueFalseValue)base.Attributes[58];
			}
			set
			{
				base.Attributes[58] = value;
			}
		}

		// Token: 0x17003B82 RID: 15234
		// (get) Token: 0x0600E4D3 RID: 58579 RVA: 0x002C4869 File Offset: 0x002C2A69
		// (set) Token: 0x0600E4D4 RID: 58580 RVA: 0x002C4879 File Offset: 0x002C2A79
		[SchemaAttr(0, "bilevel")]
		public TrueFalseValue BiLevel
		{
			get
			{
				return (TrueFalseValue)base.Attributes[59];
			}
			set
			{
				base.Attributes[59] = value;
			}
		}

		// Token: 0x0600E4D5 RID: 58581 RVA: 0x00293ECF File Offset: 0x002920CF
		public ImageFile()
		{
		}

		// Token: 0x0600E4D6 RID: 58582 RVA: 0x00293ED7 File Offset: 0x002920D7
		public ImageFile(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600E4D7 RID: 58583 RVA: 0x00293EE0 File Offset: 0x002920E0
		public ImageFile(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600E4D8 RID: 58584 RVA: 0x00293EE9 File Offset: 0x002920E9
		public ImageFile(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0600E4D9 RID: 58585 RVA: 0x002C4888 File Offset: 0x002C2A88
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

		// Token: 0x0600E4DA RID: 58586 RVA: 0x002C4AC0 File Offset: 0x002C2CC0
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
			if (namespaceId == 0 && "src" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "cropleft" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "croptop" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "cropright" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "cropbottom" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "gain" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "blacklevel" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "gamma" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "grayscale" == name)
			{
				return new TrueFalseValue();
			}
			if (namespaceId == 0 && "bilevel" == name)
			{
				return new TrueFalseValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0600E4DB RID: 58587 RVA: 0x002C5041 File Offset: 0x002C3241
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ImageFile>(deep);
		}

		// Token: 0x04006EDD RID: 28381
		private const string tagName = "image";

		// Token: 0x04006EDE RID: 28382
		private const byte tagNsId = 26;

		// Token: 0x04006EDF RID: 28383
		internal const int ElementTypeIdConst = 12521;

		// Token: 0x04006EE0 RID: 28384
		private static string[] attributeTagNames = new string[]
		{
			"id", "style", "href", "target", "class", "title", "alt", "coordsize", "wrapcoords", "print",
			"spid", "oned", "regroupid", "doubleclicknotify", "button", "userhidden", "bullet", "hr", "hrstd", "hrnoshade",
			"hrpct", "hralign", "allowincell", "allowoverlap", "userdrawn", "bordertopcolor", "borderleftcolor", "borderbottomcolor", "borderrightcolor", "dgmlayout",
			"dgmnodekind", "dgmlayoutmru", "insetmode", "filled", "fillcolor", "stroked", "strokecolor", "strokeweight", "insetpen", "spt",
			"connectortype", "bwmode", "bwpure", "bwnormal", "forcedash", "oleicon", "ole", "preferrelative", "cliptowrap", "clip",
			"src", "cropleft", "croptop", "cropright", "cropbottom", "gain", "blacklevel", "gamma", "grayscale", "bilevel"
		};

		// Token: 0x04006EE1 RID: 28385
		private static byte[] attributeNamespaceIds = new byte[]
		{
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			27, 27, 27, 27, 27, 27, 27, 27, 27, 27,
			27, 27, 27, 27, 27, 27, 27, 27, 27, 27,
			27, 27, 27, 0, 0, 0, 0, 0, 0, 27,
			27, 27, 27, 27, 27, 27, 27, 27, 27, 27,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0
		};
	}
}
