using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Vml.Office;
using DocumentFormat.OpenXml.Vml.Presentation;
using DocumentFormat.OpenXml.Vml.Spreadsheet;
using DocumentFormat.OpenXml.Vml.Wordprocessing;

namespace DocumentFormat.OpenXml.Vml
{
	// Token: 0x02002253 RID: 8787
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
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(Formulas))]
	[ChildElementInfo(typeof(Path))]
	internal class Oval : OpenXmlCompositeElement
	{
		// Token: 0x17003BBD RID: 15293
		// (get) Token: 0x0600E555 RID: 58709 RVA: 0x002C5BDE File Offset: 0x002C3DDE
		public override string LocalName
		{
			get
			{
				return "oval";
			}
		}

		// Token: 0x17003BBE RID: 15294
		// (get) Token: 0x0600E556 RID: 58710 RVA: 0x00243C87 File Offset: 0x00241E87
		internal override byte NamespaceId
		{
			get
			{
				return 26;
			}
		}

		// Token: 0x17003BBF RID: 15295
		// (get) Token: 0x0600E557 RID: 58711 RVA: 0x002C5BE5 File Offset: 0x002C3DE5
		internal override int ElementTypeId
		{
			get
			{
				return 12523;
			}
		}

		// Token: 0x0600E558 RID: 58712 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17003BC0 RID: 15296
		// (get) Token: 0x0600E559 RID: 58713 RVA: 0x002C5BEC File Offset: 0x002C3DEC
		internal override string[] AttributeTagNames
		{
			get
			{
				return Oval.attributeTagNames;
			}
		}

		// Token: 0x17003BC1 RID: 15297
		// (get) Token: 0x0600E55A RID: 58714 RVA: 0x002C5BF3 File Offset: 0x002C3DF3
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Oval.attributeNamespaceIds;
			}
		}

		// Token: 0x17003BC2 RID: 15298
		// (get) Token: 0x0600E55B RID: 58715 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0600E55C RID: 58716 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x17003BC3 RID: 15299
		// (get) Token: 0x0600E55D RID: 58717 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x0600E55E RID: 58718 RVA: 0x002BD47A File Offset: 0x002BB67A
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

		// Token: 0x17003BC4 RID: 15300
		// (get) Token: 0x0600E55F RID: 58719 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x0600E560 RID: 58720 RVA: 0x002BD494 File Offset: 0x002BB694
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

		// Token: 0x17003BC5 RID: 15301
		// (get) Token: 0x0600E561 RID: 58721 RVA: 0x002BDADA File Offset: 0x002BBCDA
		// (set) Token: 0x0600E562 RID: 58722 RVA: 0x002BD4AE File Offset: 0x002BB6AE
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

		// Token: 0x17003BC6 RID: 15302
		// (get) Token: 0x0600E563 RID: 58723 RVA: 0x002BD4B9 File Offset: 0x002BB6B9
		// (set) Token: 0x0600E564 RID: 58724 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
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

		// Token: 0x17003BC7 RID: 15303
		// (get) Token: 0x0600E565 RID: 58725 RVA: 0x002BE081 File Offset: 0x002BC281
		// (set) Token: 0x0600E566 RID: 58726 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
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

		// Token: 0x17003BC8 RID: 15304
		// (get) Token: 0x0600E567 RID: 58727 RVA: 0x002BD4ED File Offset: 0x002BB6ED
		// (set) Token: 0x0600E568 RID: 58728 RVA: 0x002BD4FC File Offset: 0x002BB6FC
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

		// Token: 0x17003BC9 RID: 15305
		// (get) Token: 0x0600E569 RID: 58729 RVA: 0x002BDB07 File Offset: 0x002BBD07
		// (set) Token: 0x0600E56A RID: 58730 RVA: 0x002BD516 File Offset: 0x002BB716
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

		// Token: 0x17003BCA RID: 15306
		// (get) Token: 0x0600E56B RID: 58731 RVA: 0x002BDB16 File Offset: 0x002BBD16
		// (set) Token: 0x0600E56C RID: 58732 RVA: 0x002BD530 File Offset: 0x002BB730
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

		// Token: 0x17003BCB RID: 15307
		// (get) Token: 0x0600E56D RID: 58733 RVA: 0x002BDB25 File Offset: 0x002BBD25
		// (set) Token: 0x0600E56E RID: 58734 RVA: 0x002BD54B File Offset: 0x002BB74B
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

		// Token: 0x17003BCC RID: 15308
		// (get) Token: 0x0600E56F RID: 58735 RVA: 0x002BE827 File Offset: 0x002BCA27
		// (set) Token: 0x0600E570 RID: 58736 RVA: 0x002BDB45 File Offset: 0x002BBD45
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

		// Token: 0x17003BCD RID: 15309
		// (get) Token: 0x0600E571 RID: 58737 RVA: 0x002BDB51 File Offset: 0x002BBD51
		// (set) Token: 0x0600E572 RID: 58738 RVA: 0x002BDB61 File Offset: 0x002BBD61
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

		// Token: 0x17003BCE RID: 15310
		// (get) Token: 0x0600E573 RID: 58739 RVA: 0x002BE1E9 File Offset: 0x002BC3E9
		// (set) Token: 0x0600E574 RID: 58740 RVA: 0x002BDB7D File Offset: 0x002BBD7D
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

		// Token: 0x17003BCF RID: 15311
		// (get) Token: 0x0600E575 RID: 58741 RVA: 0x002C1380 File Offset: 0x002BF580
		// (set) Token: 0x0600E576 RID: 58742 RVA: 0x002BE209 File Offset: 0x002BC409
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

		// Token: 0x17003BD0 RID: 15312
		// (get) Token: 0x0600E577 RID: 58743 RVA: 0x002BFFE2 File Offset: 0x002BE1E2
		// (set) Token: 0x0600E578 RID: 58744 RVA: 0x002BE225 File Offset: 0x002BC425
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

		// Token: 0x17003BD1 RID: 15313
		// (get) Token: 0x0600E579 RID: 58745 RVA: 0x002C02BC File Offset: 0x002BE4BC
		// (set) Token: 0x0600E57A RID: 58746 RVA: 0x002BE241 File Offset: 0x002BC441
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

		// Token: 0x17003BD2 RID: 15314
		// (get) Token: 0x0600E57B RID: 58747 RVA: 0x002BEF3F File Offset: 0x002BD13F
		// (set) Token: 0x0600E57C RID: 58748 RVA: 0x002BE25D File Offset: 0x002BC45D
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

		// Token: 0x17003BD3 RID: 15315
		// (get) Token: 0x0600E57D RID: 58749 RVA: 0x002C1390 File Offset: 0x002BF590
		// (set) Token: 0x0600E57E RID: 58750 RVA: 0x002BE279 File Offset: 0x002BC479
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

		// Token: 0x17003BD4 RID: 15316
		// (get) Token: 0x0600E57F RID: 58751 RVA: 0x002C13A0 File Offset: 0x002BF5A0
		// (set) Token: 0x0600E580 RID: 58752 RVA: 0x002BE295 File Offset: 0x002BC495
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

		// Token: 0x17003BD5 RID: 15317
		// (get) Token: 0x0600E581 RID: 58753 RVA: 0x002C13B0 File Offset: 0x002BF5B0
		// (set) Token: 0x0600E582 RID: 58754 RVA: 0x002BE2B1 File Offset: 0x002BC4B1
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

		// Token: 0x17003BD6 RID: 15318
		// (get) Token: 0x0600E583 RID: 58755 RVA: 0x002BE2BD File Offset: 0x002BC4BD
		// (set) Token: 0x0600E584 RID: 58756 RVA: 0x002BE2CD File Offset: 0x002BC4CD
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

		// Token: 0x17003BD7 RID: 15319
		// (get) Token: 0x0600E585 RID: 58757 RVA: 0x002C13C0 File Offset: 0x002BF5C0
		// (set) Token: 0x0600E586 RID: 58758 RVA: 0x002BE2E9 File Offset: 0x002BC4E9
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

		// Token: 0x17003BD8 RID: 15320
		// (get) Token: 0x0600E587 RID: 58759 RVA: 0x002C13D0 File Offset: 0x002BF5D0
		// (set) Token: 0x0600E588 RID: 58760 RVA: 0x002BE305 File Offset: 0x002BC505
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

		// Token: 0x17003BD9 RID: 15321
		// (get) Token: 0x0600E589 RID: 58761 RVA: 0x002BE311 File Offset: 0x002BC511
		// (set) Token: 0x0600E58A RID: 58762 RVA: 0x002BE321 File Offset: 0x002BC521
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

		// Token: 0x17003BDA RID: 15322
		// (get) Token: 0x0600E58B RID: 58763 RVA: 0x002C02EC File Offset: 0x002BE4EC
		// (set) Token: 0x0600E58C RID: 58764 RVA: 0x002BE33D File Offset: 0x002BC53D
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

		// Token: 0x17003BDB RID: 15323
		// (get) Token: 0x0600E58D RID: 58765 RVA: 0x002C0793 File Offset: 0x002BE993
		// (set) Token: 0x0600E58E RID: 58766 RVA: 0x002BE359 File Offset: 0x002BC559
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

		// Token: 0x17003BDC RID: 15324
		// (get) Token: 0x0600E58F RID: 58767 RVA: 0x002BE365 File Offset: 0x002BC565
		// (set) Token: 0x0600E590 RID: 58768 RVA: 0x002BE375 File Offset: 0x002BC575
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

		// Token: 0x17003BDD RID: 15325
		// (get) Token: 0x0600E591 RID: 58769 RVA: 0x002C13E0 File Offset: 0x002BF5E0
		// (set) Token: 0x0600E592 RID: 58770 RVA: 0x002BE391 File Offset: 0x002BC591
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

		// Token: 0x17003BDE RID: 15326
		// (get) Token: 0x0600E593 RID: 58771 RVA: 0x002BE39D File Offset: 0x002BC59D
		// (set) Token: 0x0600E594 RID: 58772 RVA: 0x002BE3AD File Offset: 0x002BC5AD
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

		// Token: 0x17003BDF RID: 15327
		// (get) Token: 0x0600E595 RID: 58773 RVA: 0x002BE3B9 File Offset: 0x002BC5B9
		// (set) Token: 0x0600E596 RID: 58774 RVA: 0x002BE3C9 File Offset: 0x002BC5C9
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

		// Token: 0x17003BE0 RID: 15328
		// (get) Token: 0x0600E597 RID: 58775 RVA: 0x002C13F0 File Offset: 0x002BF5F0
		// (set) Token: 0x0600E598 RID: 58776 RVA: 0x002BE3E5 File Offset: 0x002BC5E5
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

		// Token: 0x17003BE1 RID: 15329
		// (get) Token: 0x0600E599 RID: 58777 RVA: 0x002C1400 File Offset: 0x002BF600
		// (set) Token: 0x0600E59A RID: 58778 RVA: 0x002C1410 File Offset: 0x002BF610
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

		// Token: 0x17003BE2 RID: 15330
		// (get) Token: 0x0600E59B RID: 58779 RVA: 0x002C141C File Offset: 0x002BF61C
		// (set) Token: 0x0600E59C RID: 58780 RVA: 0x002C142C File Offset: 0x002BF62C
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

		// Token: 0x17003BE3 RID: 15331
		// (get) Token: 0x0600E59D RID: 58781 RVA: 0x002C1438 File Offset: 0x002BF638
		// (set) Token: 0x0600E59E RID: 58782 RVA: 0x002C1448 File Offset: 0x002BF648
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

		// Token: 0x17003BE4 RID: 15332
		// (get) Token: 0x0600E59F RID: 58783 RVA: 0x002C1454 File Offset: 0x002BF654
		// (set) Token: 0x0600E5A0 RID: 58784 RVA: 0x002C1464 File Offset: 0x002BF664
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

		// Token: 0x17003BE5 RID: 15333
		// (get) Token: 0x0600E5A1 RID: 58785 RVA: 0x002C1470 File Offset: 0x002BF670
		// (set) Token: 0x0600E5A2 RID: 58786 RVA: 0x002C1480 File Offset: 0x002BF680
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

		// Token: 0x17003BE6 RID: 15334
		// (get) Token: 0x0600E5A3 RID: 58787 RVA: 0x002C148C File Offset: 0x002BF68C
		// (set) Token: 0x0600E5A4 RID: 58788 RVA: 0x002C149C File Offset: 0x002BF69C
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

		// Token: 0x17003BE7 RID: 15335
		// (get) Token: 0x0600E5A5 RID: 58789 RVA: 0x002C14A8 File Offset: 0x002BF6A8
		// (set) Token: 0x0600E5A6 RID: 58790 RVA: 0x002C14B8 File Offset: 0x002BF6B8
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

		// Token: 0x17003BE8 RID: 15336
		// (get) Token: 0x0600E5A7 RID: 58791 RVA: 0x002C14C4 File Offset: 0x002BF6C4
		// (set) Token: 0x0600E5A8 RID: 58792 RVA: 0x002C14D4 File Offset: 0x002BF6D4
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

		// Token: 0x17003BE9 RID: 15337
		// (get) Token: 0x0600E5A9 RID: 58793 RVA: 0x002C14E0 File Offset: 0x002BF6E0
		// (set) Token: 0x0600E5AA RID: 58794 RVA: 0x002C14F0 File Offset: 0x002BF6F0
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

		// Token: 0x17003BEA RID: 15338
		// (get) Token: 0x0600E5AB RID: 58795 RVA: 0x002C14FC File Offset: 0x002BF6FC
		// (set) Token: 0x0600E5AC RID: 58796 RVA: 0x002C150C File Offset: 0x002BF70C
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

		// Token: 0x17003BEB RID: 15339
		// (get) Token: 0x0600E5AD RID: 58797 RVA: 0x002C1518 File Offset: 0x002BF718
		// (set) Token: 0x0600E5AE RID: 58798 RVA: 0x002C1528 File Offset: 0x002BF728
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

		// Token: 0x17003BEC RID: 15340
		// (get) Token: 0x0600E5AF RID: 58799 RVA: 0x002C1534 File Offset: 0x002BF734
		// (set) Token: 0x0600E5B0 RID: 58800 RVA: 0x002C1544 File Offset: 0x002BF744
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

		// Token: 0x17003BED RID: 15341
		// (get) Token: 0x0600E5B1 RID: 58801 RVA: 0x002C1550 File Offset: 0x002BF750
		// (set) Token: 0x0600E5B2 RID: 58802 RVA: 0x002C1560 File Offset: 0x002BF760
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

		// Token: 0x17003BEE RID: 15342
		// (get) Token: 0x0600E5B3 RID: 58803 RVA: 0x002C156C File Offset: 0x002BF76C
		// (set) Token: 0x0600E5B4 RID: 58804 RVA: 0x002C157C File Offset: 0x002BF77C
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

		// Token: 0x17003BEF RID: 15343
		// (get) Token: 0x0600E5B5 RID: 58805 RVA: 0x002C1588 File Offset: 0x002BF788
		// (set) Token: 0x0600E5B6 RID: 58806 RVA: 0x002C1598 File Offset: 0x002BF798
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

		// Token: 0x17003BF0 RID: 15344
		// (get) Token: 0x0600E5B7 RID: 58807 RVA: 0x002C15A4 File Offset: 0x002BF7A4
		// (set) Token: 0x0600E5B8 RID: 58808 RVA: 0x002C15B4 File Offset: 0x002BF7B4
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

		// Token: 0x17003BF1 RID: 15345
		// (get) Token: 0x0600E5B9 RID: 58809 RVA: 0x002C15C0 File Offset: 0x002BF7C0
		// (set) Token: 0x0600E5BA RID: 58810 RVA: 0x002C15D0 File Offset: 0x002BF7D0
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

		// Token: 0x17003BF2 RID: 15346
		// (get) Token: 0x0600E5BB RID: 58811 RVA: 0x002C15DC File Offset: 0x002BF7DC
		// (set) Token: 0x0600E5BC RID: 58812 RVA: 0x002C15EC File Offset: 0x002BF7EC
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

		// Token: 0x17003BF3 RID: 15347
		// (get) Token: 0x0600E5BD RID: 58813 RVA: 0x002C15F8 File Offset: 0x002BF7F8
		// (set) Token: 0x0600E5BE RID: 58814 RVA: 0x002C1608 File Offset: 0x002BF808
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

		// Token: 0x17003BF4 RID: 15348
		// (get) Token: 0x0600E5BF RID: 58815 RVA: 0x002C1614 File Offset: 0x002BF814
		// (set) Token: 0x0600E5C0 RID: 58816 RVA: 0x002C1624 File Offset: 0x002BF824
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

		// Token: 0x0600E5C1 RID: 58817 RVA: 0x00293ECF File Offset: 0x002920CF
		public Oval()
		{
		}

		// Token: 0x0600E5C2 RID: 58818 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Oval(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600E5C3 RID: 58819 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Oval(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600E5C4 RID: 58820 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Oval(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0600E5C5 RID: 58821 RVA: 0x002C5BFC File Offset: 0x002C3DFC
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

		// Token: 0x0600E5C6 RID: 58822 RVA: 0x002C5E34 File Offset: 0x002C4034
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

		// Token: 0x0600E5C7 RID: 58823 RVA: 0x002C62EF File Offset: 0x002C44EF
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Oval>(deep);
		}

		// Token: 0x04006EE7 RID: 28391
		private const string tagName = "oval";

		// Token: 0x04006EE8 RID: 28392
		private const byte tagNsId = 26;

		// Token: 0x04006EE9 RID: 28393
		internal const int ElementTypeIdConst = 12523;

		// Token: 0x04006EEA RID: 28394
		private static string[] attributeTagNames = new string[]
		{
			"id", "style", "href", "target", "class", "title", "alt", "coordsize", "coordorigin", "wrapcoords",
			"print", "spid", "oned", "regroupid", "doubleclicknotify", "button", "userhidden", "bullet", "hr", "hrstd",
			"hrnoshade", "hrpct", "hralign", "allowincell", "allowoverlap", "userdrawn", "bordertopcolor", "borderleftcolor", "borderbottomcolor", "borderrightcolor",
			"dgmlayout", "dgmnodekind", "dgmlayoutmru", "insetmode", "filled", "fillcolor", "stroked", "strokecolor", "strokeweight", "insetpen",
			"spt", "connectortype", "bwmode", "bwpure", "bwnormal", "forcedash", "oleicon", "ole", "preferrelative", "cliptowrap",
			"clip"
		};

		// Token: 0x04006EEB RID: 28395
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
