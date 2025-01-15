using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Vml.Office;
using DocumentFormat.OpenXml.Vml.Presentation;
using DocumentFormat.OpenXml.Vml.Spreadsheet;
using DocumentFormat.OpenXml.Vml.Wordprocessing;

namespace DocumentFormat.OpenXml.Vml
{
	// Token: 0x02002250 RID: 8784
	[ChildElementInfo(typeof(Formulas))]
	[ChildElementInfo(typeof(ShapeHandles))]
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
	[ChildElementInfo(typeof(Path))]
	[GeneratedCode("DomGen", "2.0")]
	internal class Curve : OpenXmlCompositeElement
	{
		// Token: 0x17003B06 RID: 15110
		// (get) Token: 0x0600E3DB RID: 58331 RVA: 0x002C3D65 File Offset: 0x002C1F65
		public override string LocalName
		{
			get
			{
				return "curve";
			}
		}

		// Token: 0x17003B07 RID: 15111
		// (get) Token: 0x0600E3DC RID: 58332 RVA: 0x00243C87 File Offset: 0x00241E87
		internal override byte NamespaceId
		{
			get
			{
				return 26;
			}
		}

		// Token: 0x17003B08 RID: 15112
		// (get) Token: 0x0600E3DD RID: 58333 RVA: 0x002C3D6C File Offset: 0x002C1F6C
		internal override int ElementTypeId
		{
			get
			{
				return 12520;
			}
		}

		// Token: 0x0600E3DE RID: 58334 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17003B09 RID: 15113
		// (get) Token: 0x0600E3DF RID: 58335 RVA: 0x002C3D73 File Offset: 0x002C1F73
		internal override string[] AttributeTagNames
		{
			get
			{
				return Curve.attributeTagNames;
			}
		}

		// Token: 0x17003B0A RID: 15114
		// (get) Token: 0x0600E3E0 RID: 58336 RVA: 0x002C3D7A File Offset: 0x002C1F7A
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Curve.attributeNamespaceIds;
			}
		}

		// Token: 0x17003B0B RID: 15115
		// (get) Token: 0x0600E3E1 RID: 58337 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0600E3E2 RID: 58338 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x17003B0C RID: 15116
		// (get) Token: 0x0600E3E3 RID: 58339 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x0600E3E4 RID: 58340 RVA: 0x002BD47A File Offset: 0x002BB67A
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

		// Token: 0x17003B0D RID: 15117
		// (get) Token: 0x0600E3E5 RID: 58341 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x0600E3E6 RID: 58342 RVA: 0x002BD494 File Offset: 0x002BB694
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

		// Token: 0x17003B0E RID: 15118
		// (get) Token: 0x0600E3E7 RID: 58343 RVA: 0x002BDADA File Offset: 0x002BBCDA
		// (set) Token: 0x0600E3E8 RID: 58344 RVA: 0x002BD4AE File Offset: 0x002BB6AE
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

		// Token: 0x17003B0F RID: 15119
		// (get) Token: 0x0600E3E9 RID: 58345 RVA: 0x002BD4B9 File Offset: 0x002BB6B9
		// (set) Token: 0x0600E3EA RID: 58346 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
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

		// Token: 0x17003B10 RID: 15120
		// (get) Token: 0x0600E3EB RID: 58347 RVA: 0x002BE081 File Offset: 0x002BC281
		// (set) Token: 0x0600E3EC RID: 58348 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
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

		// Token: 0x17003B11 RID: 15121
		// (get) Token: 0x0600E3ED RID: 58349 RVA: 0x002BD4ED File Offset: 0x002BB6ED
		// (set) Token: 0x0600E3EE RID: 58350 RVA: 0x002BD4FC File Offset: 0x002BB6FC
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

		// Token: 0x17003B12 RID: 15122
		// (get) Token: 0x0600E3EF RID: 58351 RVA: 0x002BDB07 File Offset: 0x002BBD07
		// (set) Token: 0x0600E3F0 RID: 58352 RVA: 0x002BD516 File Offset: 0x002BB716
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

		// Token: 0x17003B13 RID: 15123
		// (get) Token: 0x0600E3F1 RID: 58353 RVA: 0x002BDB16 File Offset: 0x002BBD16
		// (set) Token: 0x0600E3F2 RID: 58354 RVA: 0x002BD530 File Offset: 0x002BB730
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

		// Token: 0x17003B14 RID: 15124
		// (get) Token: 0x0600E3F3 RID: 58355 RVA: 0x002BDB25 File Offset: 0x002BBD25
		// (set) Token: 0x0600E3F4 RID: 58356 RVA: 0x002BD54B File Offset: 0x002BB74B
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

		// Token: 0x17003B15 RID: 15125
		// (get) Token: 0x0600E3F5 RID: 58357 RVA: 0x002BE827 File Offset: 0x002BCA27
		// (set) Token: 0x0600E3F6 RID: 58358 RVA: 0x002BDB45 File Offset: 0x002BBD45
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

		// Token: 0x17003B16 RID: 15126
		// (get) Token: 0x0600E3F7 RID: 58359 RVA: 0x002BDB51 File Offset: 0x002BBD51
		// (set) Token: 0x0600E3F8 RID: 58360 RVA: 0x002BDB61 File Offset: 0x002BBD61
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

		// Token: 0x17003B17 RID: 15127
		// (get) Token: 0x0600E3F9 RID: 58361 RVA: 0x002BE1E9 File Offset: 0x002BC3E9
		// (set) Token: 0x0600E3FA RID: 58362 RVA: 0x002BDB7D File Offset: 0x002BBD7D
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

		// Token: 0x17003B18 RID: 15128
		// (get) Token: 0x0600E3FB RID: 58363 RVA: 0x002C1380 File Offset: 0x002BF580
		// (set) Token: 0x0600E3FC RID: 58364 RVA: 0x002BE209 File Offset: 0x002BC409
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

		// Token: 0x17003B19 RID: 15129
		// (get) Token: 0x0600E3FD RID: 58365 RVA: 0x002BFFE2 File Offset: 0x002BE1E2
		// (set) Token: 0x0600E3FE RID: 58366 RVA: 0x002BE225 File Offset: 0x002BC425
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

		// Token: 0x17003B1A RID: 15130
		// (get) Token: 0x0600E3FF RID: 58367 RVA: 0x002C02BC File Offset: 0x002BE4BC
		// (set) Token: 0x0600E400 RID: 58368 RVA: 0x002BE241 File Offset: 0x002BC441
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

		// Token: 0x17003B1B RID: 15131
		// (get) Token: 0x0600E401 RID: 58369 RVA: 0x002BEF3F File Offset: 0x002BD13F
		// (set) Token: 0x0600E402 RID: 58370 RVA: 0x002BE25D File Offset: 0x002BC45D
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

		// Token: 0x17003B1C RID: 15132
		// (get) Token: 0x0600E403 RID: 58371 RVA: 0x002C1390 File Offset: 0x002BF590
		// (set) Token: 0x0600E404 RID: 58372 RVA: 0x002BE279 File Offset: 0x002BC479
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

		// Token: 0x17003B1D RID: 15133
		// (get) Token: 0x0600E405 RID: 58373 RVA: 0x002C13A0 File Offset: 0x002BF5A0
		// (set) Token: 0x0600E406 RID: 58374 RVA: 0x002BE295 File Offset: 0x002BC495
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

		// Token: 0x17003B1E RID: 15134
		// (get) Token: 0x0600E407 RID: 58375 RVA: 0x002C13B0 File Offset: 0x002BF5B0
		// (set) Token: 0x0600E408 RID: 58376 RVA: 0x002BE2B1 File Offset: 0x002BC4B1
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

		// Token: 0x17003B1F RID: 15135
		// (get) Token: 0x0600E409 RID: 58377 RVA: 0x002BE2BD File Offset: 0x002BC4BD
		// (set) Token: 0x0600E40A RID: 58378 RVA: 0x002BE2CD File Offset: 0x002BC4CD
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

		// Token: 0x17003B20 RID: 15136
		// (get) Token: 0x0600E40B RID: 58379 RVA: 0x002C13C0 File Offset: 0x002BF5C0
		// (set) Token: 0x0600E40C RID: 58380 RVA: 0x002BE2E9 File Offset: 0x002BC4E9
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

		// Token: 0x17003B21 RID: 15137
		// (get) Token: 0x0600E40D RID: 58381 RVA: 0x002C13D0 File Offset: 0x002BF5D0
		// (set) Token: 0x0600E40E RID: 58382 RVA: 0x002BE305 File Offset: 0x002BC505
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

		// Token: 0x17003B22 RID: 15138
		// (get) Token: 0x0600E40F RID: 58383 RVA: 0x002BE311 File Offset: 0x002BC511
		// (set) Token: 0x0600E410 RID: 58384 RVA: 0x002BE321 File Offset: 0x002BC521
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

		// Token: 0x17003B23 RID: 15139
		// (get) Token: 0x0600E411 RID: 58385 RVA: 0x002C02EC File Offset: 0x002BE4EC
		// (set) Token: 0x0600E412 RID: 58386 RVA: 0x002BE33D File Offset: 0x002BC53D
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

		// Token: 0x17003B24 RID: 15140
		// (get) Token: 0x0600E413 RID: 58387 RVA: 0x002C0793 File Offset: 0x002BE993
		// (set) Token: 0x0600E414 RID: 58388 RVA: 0x002BE359 File Offset: 0x002BC559
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

		// Token: 0x17003B25 RID: 15141
		// (get) Token: 0x0600E415 RID: 58389 RVA: 0x002BE365 File Offset: 0x002BC565
		// (set) Token: 0x0600E416 RID: 58390 RVA: 0x002BE375 File Offset: 0x002BC575
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

		// Token: 0x17003B26 RID: 15142
		// (get) Token: 0x0600E417 RID: 58391 RVA: 0x002C13E0 File Offset: 0x002BF5E0
		// (set) Token: 0x0600E418 RID: 58392 RVA: 0x002BE391 File Offset: 0x002BC591
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

		// Token: 0x17003B27 RID: 15143
		// (get) Token: 0x0600E419 RID: 58393 RVA: 0x002BE39D File Offset: 0x002BC59D
		// (set) Token: 0x0600E41A RID: 58394 RVA: 0x002BE3AD File Offset: 0x002BC5AD
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

		// Token: 0x17003B28 RID: 15144
		// (get) Token: 0x0600E41B RID: 58395 RVA: 0x002BE3B9 File Offset: 0x002BC5B9
		// (set) Token: 0x0600E41C RID: 58396 RVA: 0x002BE3C9 File Offset: 0x002BC5C9
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

		// Token: 0x17003B29 RID: 15145
		// (get) Token: 0x0600E41D RID: 58397 RVA: 0x002C13F0 File Offset: 0x002BF5F0
		// (set) Token: 0x0600E41E RID: 58398 RVA: 0x002BE3E5 File Offset: 0x002BC5E5
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

		// Token: 0x17003B2A RID: 15146
		// (get) Token: 0x0600E41F RID: 58399 RVA: 0x002C1400 File Offset: 0x002BF600
		// (set) Token: 0x0600E420 RID: 58400 RVA: 0x002C1410 File Offset: 0x002BF610
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

		// Token: 0x17003B2B RID: 15147
		// (get) Token: 0x0600E421 RID: 58401 RVA: 0x002C141C File Offset: 0x002BF61C
		// (set) Token: 0x0600E422 RID: 58402 RVA: 0x002C142C File Offset: 0x002BF62C
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

		// Token: 0x17003B2C RID: 15148
		// (get) Token: 0x0600E423 RID: 58403 RVA: 0x002C1438 File Offset: 0x002BF638
		// (set) Token: 0x0600E424 RID: 58404 RVA: 0x002C1448 File Offset: 0x002BF648
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

		// Token: 0x17003B2D RID: 15149
		// (get) Token: 0x0600E425 RID: 58405 RVA: 0x002C1454 File Offset: 0x002BF654
		// (set) Token: 0x0600E426 RID: 58406 RVA: 0x002C1464 File Offset: 0x002BF664
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

		// Token: 0x17003B2E RID: 15150
		// (get) Token: 0x0600E427 RID: 58407 RVA: 0x002C1470 File Offset: 0x002BF670
		// (set) Token: 0x0600E428 RID: 58408 RVA: 0x002C1480 File Offset: 0x002BF680
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

		// Token: 0x17003B2F RID: 15151
		// (get) Token: 0x0600E429 RID: 58409 RVA: 0x002C148C File Offset: 0x002BF68C
		// (set) Token: 0x0600E42A RID: 58410 RVA: 0x002C149C File Offset: 0x002BF69C
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

		// Token: 0x17003B30 RID: 15152
		// (get) Token: 0x0600E42B RID: 58411 RVA: 0x002C14A8 File Offset: 0x002BF6A8
		// (set) Token: 0x0600E42C RID: 58412 RVA: 0x002C14B8 File Offset: 0x002BF6B8
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

		// Token: 0x17003B31 RID: 15153
		// (get) Token: 0x0600E42D RID: 58413 RVA: 0x002C14C4 File Offset: 0x002BF6C4
		// (set) Token: 0x0600E42E RID: 58414 RVA: 0x002C14D4 File Offset: 0x002BF6D4
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

		// Token: 0x17003B32 RID: 15154
		// (get) Token: 0x0600E42F RID: 58415 RVA: 0x002C14E0 File Offset: 0x002BF6E0
		// (set) Token: 0x0600E430 RID: 58416 RVA: 0x002C14F0 File Offset: 0x002BF6F0
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

		// Token: 0x17003B33 RID: 15155
		// (get) Token: 0x0600E431 RID: 58417 RVA: 0x002C14FC File Offset: 0x002BF6FC
		// (set) Token: 0x0600E432 RID: 58418 RVA: 0x002C150C File Offset: 0x002BF70C
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

		// Token: 0x17003B34 RID: 15156
		// (get) Token: 0x0600E433 RID: 58419 RVA: 0x002C1518 File Offset: 0x002BF718
		// (set) Token: 0x0600E434 RID: 58420 RVA: 0x002C1528 File Offset: 0x002BF728
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

		// Token: 0x17003B35 RID: 15157
		// (get) Token: 0x0600E435 RID: 58421 RVA: 0x002C1534 File Offset: 0x002BF734
		// (set) Token: 0x0600E436 RID: 58422 RVA: 0x002C1544 File Offset: 0x002BF744
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

		// Token: 0x17003B36 RID: 15158
		// (get) Token: 0x0600E437 RID: 58423 RVA: 0x002C1550 File Offset: 0x002BF750
		// (set) Token: 0x0600E438 RID: 58424 RVA: 0x002C1560 File Offset: 0x002BF760
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

		// Token: 0x17003B37 RID: 15159
		// (get) Token: 0x0600E439 RID: 58425 RVA: 0x002C156C File Offset: 0x002BF76C
		// (set) Token: 0x0600E43A RID: 58426 RVA: 0x002C157C File Offset: 0x002BF77C
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

		// Token: 0x17003B38 RID: 15160
		// (get) Token: 0x0600E43B RID: 58427 RVA: 0x002C1588 File Offset: 0x002BF788
		// (set) Token: 0x0600E43C RID: 58428 RVA: 0x002C1598 File Offset: 0x002BF798
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

		// Token: 0x17003B39 RID: 15161
		// (get) Token: 0x0600E43D RID: 58429 RVA: 0x002C15A4 File Offset: 0x002BF7A4
		// (set) Token: 0x0600E43E RID: 58430 RVA: 0x002C15B4 File Offset: 0x002BF7B4
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

		// Token: 0x17003B3A RID: 15162
		// (get) Token: 0x0600E43F RID: 58431 RVA: 0x002C15C0 File Offset: 0x002BF7C0
		// (set) Token: 0x0600E440 RID: 58432 RVA: 0x002C15D0 File Offset: 0x002BF7D0
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

		// Token: 0x17003B3B RID: 15163
		// (get) Token: 0x0600E441 RID: 58433 RVA: 0x002C15DC File Offset: 0x002BF7DC
		// (set) Token: 0x0600E442 RID: 58434 RVA: 0x002C15EC File Offset: 0x002BF7EC
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

		// Token: 0x17003B3C RID: 15164
		// (get) Token: 0x0600E443 RID: 58435 RVA: 0x002C15F8 File Offset: 0x002BF7F8
		// (set) Token: 0x0600E444 RID: 58436 RVA: 0x002C1608 File Offset: 0x002BF808
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

		// Token: 0x17003B3D RID: 15165
		// (get) Token: 0x0600E445 RID: 58437 RVA: 0x002C1614 File Offset: 0x002BF814
		// (set) Token: 0x0600E446 RID: 58438 RVA: 0x002C1624 File Offset: 0x002BF824
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

		// Token: 0x17003B3E RID: 15166
		// (get) Token: 0x0600E447 RID: 58439 RVA: 0x002C1630 File Offset: 0x002BF830
		// (set) Token: 0x0600E448 RID: 58440 RVA: 0x002C1640 File Offset: 0x002BF840
		[SchemaAttr(0, "from")]
		public StringValue From
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

		// Token: 0x17003B3F RID: 15167
		// (get) Token: 0x0600E449 RID: 58441 RVA: 0x002C164C File Offset: 0x002BF84C
		// (set) Token: 0x0600E44A RID: 58442 RVA: 0x002C165C File Offset: 0x002BF85C
		[SchemaAttr(0, "control1")]
		public StringValue Control1
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

		// Token: 0x17003B40 RID: 15168
		// (get) Token: 0x0600E44B RID: 58443 RVA: 0x002C1668 File Offset: 0x002BF868
		// (set) Token: 0x0600E44C RID: 58444 RVA: 0x002C1678 File Offset: 0x002BF878
		[SchemaAttr(0, "control2")]
		public StringValue Control2
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

		// Token: 0x17003B41 RID: 15169
		// (get) Token: 0x0600E44D RID: 58445 RVA: 0x002C3D81 File Offset: 0x002C1F81
		// (set) Token: 0x0600E44E RID: 58446 RVA: 0x002C1694 File Offset: 0x002BF894
		[SchemaAttr(0, "to")]
		public StringValue To
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

		// Token: 0x0600E44F RID: 58447 RVA: 0x00293ECF File Offset: 0x002920CF
		public Curve()
		{
		}

		// Token: 0x0600E450 RID: 58448 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Curve(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600E451 RID: 58449 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Curve(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600E452 RID: 58450 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Curve(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0600E453 RID: 58451 RVA: 0x002C3D94 File Offset: 0x002C1F94
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

		// Token: 0x0600E454 RID: 58452 RVA: 0x002C3FCC File Offset: 0x002C21CC
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
			if (namespaceId == 0 && "from" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "control1" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "control2" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "to" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0600E455 RID: 58453 RVA: 0x002C44DF File Offset: 0x002C26DF
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Curve>(deep);
		}

		// Token: 0x04006ED8 RID: 28376
		private const string tagName = "curve";

		// Token: 0x04006ED9 RID: 28377
		private const byte tagNsId = 26;

		// Token: 0x04006EDA RID: 28378
		internal const int ElementTypeIdConst = 12520;

		// Token: 0x04006EDB RID: 28379
		private static string[] attributeTagNames = new string[]
		{
			"id", "style", "href", "target", "class", "title", "alt", "coordsize", "coordorigin", "wrapcoords",
			"print", "spid", "oned", "regroupid", "doubleclicknotify", "button", "userhidden", "bullet", "hr", "hrstd",
			"hrnoshade", "hrpct", "hralign", "allowincell", "allowoverlap", "userdrawn", "bordertopcolor", "borderleftcolor", "borderbottomcolor", "borderrightcolor",
			"dgmlayout", "dgmnodekind", "dgmlayoutmru", "insetmode", "filled", "fillcolor", "stroked", "strokecolor", "strokeweight", "insetpen",
			"spt", "connectortype", "bwmode", "bwpure", "bwnormal", "forcedash", "oleicon", "ole", "preferrelative", "cliptowrap",
			"clip", "from", "control1", "control2", "to"
		};

		// Token: 0x04006EDC RID: 28380
		private static byte[] attributeNamespaceIds = new byte[]
		{
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 27, 27, 27, 27, 27, 27, 27, 27, 27,
			27, 27, 27, 27, 27, 27, 27, 27, 27, 27,
			27, 27, 27, 27, 0, 0, 0, 0, 0, 0,
			27, 27, 27, 27, 27, 27, 27, 27, 27, 27,
			27, 0, 0, 0, 0
		};
	}
}
