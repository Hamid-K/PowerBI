using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Vml.Office;
using DocumentFormat.OpenXml.Vml.Presentation;
using DocumentFormat.OpenXml.Vml.Spreadsheet;
using DocumentFormat.OpenXml.Vml.Wordprocessing;

namespace DocumentFormat.OpenXml.Vml
{
	// Token: 0x02002254 RID: 8788
	[ChildElementInfo(typeof(Shadow))]
	[ChildElementInfo(typeof(Formulas))]
	[ChildElementInfo(typeof(Stroke))]
	[ChildElementInfo(typeof(Path))]
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
	[ChildElementInfo(typeof(Ink))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(ShapeHandles))]
	[ChildElementInfo(typeof(Fill))]
	internal class PolyLine : OpenXmlCompositeElement
	{
		// Token: 0x17003BF5 RID: 15349
		// (get) Token: 0x0600E5C9 RID: 58825 RVA: 0x002C64EC File Offset: 0x002C46EC
		public override string LocalName
		{
			get
			{
				return "polyline";
			}
		}

		// Token: 0x17003BF6 RID: 15350
		// (get) Token: 0x0600E5CA RID: 58826 RVA: 0x00243C87 File Offset: 0x00241E87
		internal override byte NamespaceId
		{
			get
			{
				return 26;
			}
		}

		// Token: 0x17003BF7 RID: 15351
		// (get) Token: 0x0600E5CB RID: 58827 RVA: 0x002C64F3 File Offset: 0x002C46F3
		internal override int ElementTypeId
		{
			get
			{
				return 12524;
			}
		}

		// Token: 0x0600E5CC RID: 58828 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17003BF8 RID: 15352
		// (get) Token: 0x0600E5CD RID: 58829 RVA: 0x002C64FA File Offset: 0x002C46FA
		internal override string[] AttributeTagNames
		{
			get
			{
				return PolyLine.attributeTagNames;
			}
		}

		// Token: 0x17003BF9 RID: 15353
		// (get) Token: 0x0600E5CE RID: 58830 RVA: 0x002C6501 File Offset: 0x002C4701
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return PolyLine.attributeNamespaceIds;
			}
		}

		// Token: 0x17003BFA RID: 15354
		// (get) Token: 0x0600E5CF RID: 58831 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0600E5D0 RID: 58832 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x17003BFB RID: 15355
		// (get) Token: 0x0600E5D1 RID: 58833 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x0600E5D2 RID: 58834 RVA: 0x002BD47A File Offset: 0x002BB67A
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

		// Token: 0x17003BFC RID: 15356
		// (get) Token: 0x0600E5D3 RID: 58835 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x0600E5D4 RID: 58836 RVA: 0x002BD494 File Offset: 0x002BB694
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

		// Token: 0x17003BFD RID: 15357
		// (get) Token: 0x0600E5D5 RID: 58837 RVA: 0x002BDADA File Offset: 0x002BBCDA
		// (set) Token: 0x0600E5D6 RID: 58838 RVA: 0x002BD4AE File Offset: 0x002BB6AE
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

		// Token: 0x17003BFE RID: 15358
		// (get) Token: 0x0600E5D7 RID: 58839 RVA: 0x002BD4B9 File Offset: 0x002BB6B9
		// (set) Token: 0x0600E5D8 RID: 58840 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
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

		// Token: 0x17003BFF RID: 15359
		// (get) Token: 0x0600E5D9 RID: 58841 RVA: 0x002BE081 File Offset: 0x002BC281
		// (set) Token: 0x0600E5DA RID: 58842 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
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

		// Token: 0x17003C00 RID: 15360
		// (get) Token: 0x0600E5DB RID: 58843 RVA: 0x002BD4ED File Offset: 0x002BB6ED
		// (set) Token: 0x0600E5DC RID: 58844 RVA: 0x002BD4FC File Offset: 0x002BB6FC
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

		// Token: 0x17003C01 RID: 15361
		// (get) Token: 0x0600E5DD RID: 58845 RVA: 0x002BDB07 File Offset: 0x002BBD07
		// (set) Token: 0x0600E5DE RID: 58846 RVA: 0x002BD516 File Offset: 0x002BB716
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

		// Token: 0x17003C02 RID: 15362
		// (get) Token: 0x0600E5DF RID: 58847 RVA: 0x002BDB16 File Offset: 0x002BBD16
		// (set) Token: 0x0600E5E0 RID: 58848 RVA: 0x002BD530 File Offset: 0x002BB730
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

		// Token: 0x17003C03 RID: 15363
		// (get) Token: 0x0600E5E1 RID: 58849 RVA: 0x002BDB25 File Offset: 0x002BBD25
		// (set) Token: 0x0600E5E2 RID: 58850 RVA: 0x002BD54B File Offset: 0x002BB74B
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

		// Token: 0x17003C04 RID: 15364
		// (get) Token: 0x0600E5E3 RID: 58851 RVA: 0x002BE827 File Offset: 0x002BCA27
		// (set) Token: 0x0600E5E4 RID: 58852 RVA: 0x002BDB45 File Offset: 0x002BBD45
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

		// Token: 0x17003C05 RID: 15365
		// (get) Token: 0x0600E5E5 RID: 58853 RVA: 0x002BDB51 File Offset: 0x002BBD51
		// (set) Token: 0x0600E5E6 RID: 58854 RVA: 0x002BDB61 File Offset: 0x002BBD61
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

		// Token: 0x17003C06 RID: 15366
		// (get) Token: 0x0600E5E7 RID: 58855 RVA: 0x002BE1E9 File Offset: 0x002BC3E9
		// (set) Token: 0x0600E5E8 RID: 58856 RVA: 0x002BDB7D File Offset: 0x002BBD7D
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

		// Token: 0x17003C07 RID: 15367
		// (get) Token: 0x0600E5E9 RID: 58857 RVA: 0x002C1380 File Offset: 0x002BF580
		// (set) Token: 0x0600E5EA RID: 58858 RVA: 0x002BE209 File Offset: 0x002BC409
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

		// Token: 0x17003C08 RID: 15368
		// (get) Token: 0x0600E5EB RID: 58859 RVA: 0x002BFFE2 File Offset: 0x002BE1E2
		// (set) Token: 0x0600E5EC RID: 58860 RVA: 0x002BE225 File Offset: 0x002BC425
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

		// Token: 0x17003C09 RID: 15369
		// (get) Token: 0x0600E5ED RID: 58861 RVA: 0x002C02BC File Offset: 0x002BE4BC
		// (set) Token: 0x0600E5EE RID: 58862 RVA: 0x002BE241 File Offset: 0x002BC441
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

		// Token: 0x17003C0A RID: 15370
		// (get) Token: 0x0600E5EF RID: 58863 RVA: 0x002BEF3F File Offset: 0x002BD13F
		// (set) Token: 0x0600E5F0 RID: 58864 RVA: 0x002BE25D File Offset: 0x002BC45D
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

		// Token: 0x17003C0B RID: 15371
		// (get) Token: 0x0600E5F1 RID: 58865 RVA: 0x002C1390 File Offset: 0x002BF590
		// (set) Token: 0x0600E5F2 RID: 58866 RVA: 0x002BE279 File Offset: 0x002BC479
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

		// Token: 0x17003C0C RID: 15372
		// (get) Token: 0x0600E5F3 RID: 58867 RVA: 0x002C13A0 File Offset: 0x002BF5A0
		// (set) Token: 0x0600E5F4 RID: 58868 RVA: 0x002BE295 File Offset: 0x002BC495
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

		// Token: 0x17003C0D RID: 15373
		// (get) Token: 0x0600E5F5 RID: 58869 RVA: 0x002C13B0 File Offset: 0x002BF5B0
		// (set) Token: 0x0600E5F6 RID: 58870 RVA: 0x002BE2B1 File Offset: 0x002BC4B1
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

		// Token: 0x17003C0E RID: 15374
		// (get) Token: 0x0600E5F7 RID: 58871 RVA: 0x002BE2BD File Offset: 0x002BC4BD
		// (set) Token: 0x0600E5F8 RID: 58872 RVA: 0x002BE2CD File Offset: 0x002BC4CD
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

		// Token: 0x17003C0F RID: 15375
		// (get) Token: 0x0600E5F9 RID: 58873 RVA: 0x002C13C0 File Offset: 0x002BF5C0
		// (set) Token: 0x0600E5FA RID: 58874 RVA: 0x002BE2E9 File Offset: 0x002BC4E9
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

		// Token: 0x17003C10 RID: 15376
		// (get) Token: 0x0600E5FB RID: 58875 RVA: 0x002C13D0 File Offset: 0x002BF5D0
		// (set) Token: 0x0600E5FC RID: 58876 RVA: 0x002BE305 File Offset: 0x002BC505
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

		// Token: 0x17003C11 RID: 15377
		// (get) Token: 0x0600E5FD RID: 58877 RVA: 0x002BE311 File Offset: 0x002BC511
		// (set) Token: 0x0600E5FE RID: 58878 RVA: 0x002BE321 File Offset: 0x002BC521
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

		// Token: 0x17003C12 RID: 15378
		// (get) Token: 0x0600E5FF RID: 58879 RVA: 0x002C02EC File Offset: 0x002BE4EC
		// (set) Token: 0x0600E600 RID: 58880 RVA: 0x002BE33D File Offset: 0x002BC53D
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

		// Token: 0x17003C13 RID: 15379
		// (get) Token: 0x0600E601 RID: 58881 RVA: 0x002C0793 File Offset: 0x002BE993
		// (set) Token: 0x0600E602 RID: 58882 RVA: 0x002BE359 File Offset: 0x002BC559
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

		// Token: 0x17003C14 RID: 15380
		// (get) Token: 0x0600E603 RID: 58883 RVA: 0x002BE365 File Offset: 0x002BC565
		// (set) Token: 0x0600E604 RID: 58884 RVA: 0x002BE375 File Offset: 0x002BC575
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

		// Token: 0x17003C15 RID: 15381
		// (get) Token: 0x0600E605 RID: 58885 RVA: 0x002C13E0 File Offset: 0x002BF5E0
		// (set) Token: 0x0600E606 RID: 58886 RVA: 0x002BE391 File Offset: 0x002BC591
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

		// Token: 0x17003C16 RID: 15382
		// (get) Token: 0x0600E607 RID: 58887 RVA: 0x002BE39D File Offset: 0x002BC59D
		// (set) Token: 0x0600E608 RID: 58888 RVA: 0x002BE3AD File Offset: 0x002BC5AD
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

		// Token: 0x17003C17 RID: 15383
		// (get) Token: 0x0600E609 RID: 58889 RVA: 0x002BE3B9 File Offset: 0x002BC5B9
		// (set) Token: 0x0600E60A RID: 58890 RVA: 0x002BE3C9 File Offset: 0x002BC5C9
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

		// Token: 0x17003C18 RID: 15384
		// (get) Token: 0x0600E60B RID: 58891 RVA: 0x002C13F0 File Offset: 0x002BF5F0
		// (set) Token: 0x0600E60C RID: 58892 RVA: 0x002BE3E5 File Offset: 0x002BC5E5
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

		// Token: 0x17003C19 RID: 15385
		// (get) Token: 0x0600E60D RID: 58893 RVA: 0x002C1400 File Offset: 0x002BF600
		// (set) Token: 0x0600E60E RID: 58894 RVA: 0x002C1410 File Offset: 0x002BF610
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

		// Token: 0x17003C1A RID: 15386
		// (get) Token: 0x0600E60F RID: 58895 RVA: 0x002C141C File Offset: 0x002BF61C
		// (set) Token: 0x0600E610 RID: 58896 RVA: 0x002C142C File Offset: 0x002BF62C
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

		// Token: 0x17003C1B RID: 15387
		// (get) Token: 0x0600E611 RID: 58897 RVA: 0x002C1438 File Offset: 0x002BF638
		// (set) Token: 0x0600E612 RID: 58898 RVA: 0x002C1448 File Offset: 0x002BF648
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

		// Token: 0x17003C1C RID: 15388
		// (get) Token: 0x0600E613 RID: 58899 RVA: 0x002C1454 File Offset: 0x002BF654
		// (set) Token: 0x0600E614 RID: 58900 RVA: 0x002C1464 File Offset: 0x002BF664
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

		// Token: 0x17003C1D RID: 15389
		// (get) Token: 0x0600E615 RID: 58901 RVA: 0x002C1470 File Offset: 0x002BF670
		// (set) Token: 0x0600E616 RID: 58902 RVA: 0x002C1480 File Offset: 0x002BF680
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

		// Token: 0x17003C1E RID: 15390
		// (get) Token: 0x0600E617 RID: 58903 RVA: 0x002C148C File Offset: 0x002BF68C
		// (set) Token: 0x0600E618 RID: 58904 RVA: 0x002C149C File Offset: 0x002BF69C
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

		// Token: 0x17003C1F RID: 15391
		// (get) Token: 0x0600E619 RID: 58905 RVA: 0x002C14A8 File Offset: 0x002BF6A8
		// (set) Token: 0x0600E61A RID: 58906 RVA: 0x002C14B8 File Offset: 0x002BF6B8
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

		// Token: 0x17003C20 RID: 15392
		// (get) Token: 0x0600E61B RID: 58907 RVA: 0x002C14C4 File Offset: 0x002BF6C4
		// (set) Token: 0x0600E61C RID: 58908 RVA: 0x002C14D4 File Offset: 0x002BF6D4
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

		// Token: 0x17003C21 RID: 15393
		// (get) Token: 0x0600E61D RID: 58909 RVA: 0x002C14E0 File Offset: 0x002BF6E0
		// (set) Token: 0x0600E61E RID: 58910 RVA: 0x002C14F0 File Offset: 0x002BF6F0
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

		// Token: 0x17003C22 RID: 15394
		// (get) Token: 0x0600E61F RID: 58911 RVA: 0x002C14FC File Offset: 0x002BF6FC
		// (set) Token: 0x0600E620 RID: 58912 RVA: 0x002C150C File Offset: 0x002BF70C
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

		// Token: 0x17003C23 RID: 15395
		// (get) Token: 0x0600E621 RID: 58913 RVA: 0x002C1518 File Offset: 0x002BF718
		// (set) Token: 0x0600E622 RID: 58914 RVA: 0x002C1528 File Offset: 0x002BF728
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

		// Token: 0x17003C24 RID: 15396
		// (get) Token: 0x0600E623 RID: 58915 RVA: 0x002C1534 File Offset: 0x002BF734
		// (set) Token: 0x0600E624 RID: 58916 RVA: 0x002C1544 File Offset: 0x002BF744
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

		// Token: 0x17003C25 RID: 15397
		// (get) Token: 0x0600E625 RID: 58917 RVA: 0x002C1550 File Offset: 0x002BF750
		// (set) Token: 0x0600E626 RID: 58918 RVA: 0x002C1560 File Offset: 0x002BF760
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

		// Token: 0x17003C26 RID: 15398
		// (get) Token: 0x0600E627 RID: 58919 RVA: 0x002C156C File Offset: 0x002BF76C
		// (set) Token: 0x0600E628 RID: 58920 RVA: 0x002C157C File Offset: 0x002BF77C
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

		// Token: 0x17003C27 RID: 15399
		// (get) Token: 0x0600E629 RID: 58921 RVA: 0x002C1588 File Offset: 0x002BF788
		// (set) Token: 0x0600E62A RID: 58922 RVA: 0x002C1598 File Offset: 0x002BF798
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

		// Token: 0x17003C28 RID: 15400
		// (get) Token: 0x0600E62B RID: 58923 RVA: 0x002C15A4 File Offset: 0x002BF7A4
		// (set) Token: 0x0600E62C RID: 58924 RVA: 0x002C15B4 File Offset: 0x002BF7B4
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

		// Token: 0x17003C29 RID: 15401
		// (get) Token: 0x0600E62D RID: 58925 RVA: 0x002C15C0 File Offset: 0x002BF7C0
		// (set) Token: 0x0600E62E RID: 58926 RVA: 0x002C15D0 File Offset: 0x002BF7D0
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

		// Token: 0x17003C2A RID: 15402
		// (get) Token: 0x0600E62F RID: 58927 RVA: 0x002C15DC File Offset: 0x002BF7DC
		// (set) Token: 0x0600E630 RID: 58928 RVA: 0x002C15EC File Offset: 0x002BF7EC
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

		// Token: 0x17003C2B RID: 15403
		// (get) Token: 0x0600E631 RID: 58929 RVA: 0x002C15F8 File Offset: 0x002BF7F8
		// (set) Token: 0x0600E632 RID: 58930 RVA: 0x002C1608 File Offset: 0x002BF808
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

		// Token: 0x17003C2C RID: 15404
		// (get) Token: 0x0600E633 RID: 58931 RVA: 0x002C1614 File Offset: 0x002BF814
		// (set) Token: 0x0600E634 RID: 58932 RVA: 0x002C1624 File Offset: 0x002BF824
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

		// Token: 0x17003C2D RID: 15405
		// (get) Token: 0x0600E635 RID: 58933 RVA: 0x002C1630 File Offset: 0x002BF830
		// (set) Token: 0x0600E636 RID: 58934 RVA: 0x002C1640 File Offset: 0x002BF840
		[SchemaAttr(0, "points")]
		public StringValue Points
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

		// Token: 0x0600E637 RID: 58935 RVA: 0x00293ECF File Offset: 0x002920CF
		public PolyLine()
		{
		}

		// Token: 0x0600E638 RID: 58936 RVA: 0x00293ED7 File Offset: 0x002920D7
		public PolyLine(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600E639 RID: 58937 RVA: 0x00293EE0 File Offset: 0x002920E0
		public PolyLine(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600E63A RID: 58938 RVA: 0x00293EE9 File Offset: 0x002920E9
		public PolyLine(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0600E63B RID: 58939 RVA: 0x002C6508 File Offset: 0x002C4708
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
			if (27 == namespaceId && "ink" == name)
			{
				return new Ink();
			}
			return null;
		}

		// Token: 0x0600E63C RID: 58940 RVA: 0x002C6758 File Offset: 0x002C4958
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
			if (namespaceId == 0 && "points" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0600E63D RID: 58941 RVA: 0x002C6C29 File Offset: 0x002C4E29
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<PolyLine>(deep);
		}

		// Token: 0x04006EEC RID: 28396
		private const string tagName = "polyline";

		// Token: 0x04006EED RID: 28397
		private const byte tagNsId = 26;

		// Token: 0x04006EEE RID: 28398
		internal const int ElementTypeIdConst = 12524;

		// Token: 0x04006EEF RID: 28399
		private static string[] attributeTagNames = new string[]
		{
			"id", "style", "href", "target", "class", "title", "alt", "coordsize", "coordorigin", "wrapcoords",
			"print", "spid", "oned", "regroupid", "doubleclicknotify", "button", "userhidden", "bullet", "hr", "hrstd",
			"hrnoshade", "hrpct", "hralign", "allowincell", "allowoverlap", "userdrawn", "bordertopcolor", "borderleftcolor", "borderbottomcolor", "borderrightcolor",
			"dgmlayout", "dgmnodekind", "dgmlayoutmru", "insetmode", "filled", "fillcolor", "stroked", "strokecolor", "strokeweight", "insetpen",
			"spt", "connectortype", "bwmode", "bwpure", "bwnormal", "forcedash", "oleicon", "ole", "preferrelative", "cliptowrap",
			"clip", "points"
		};

		// Token: 0x04006EF0 RID: 28400
		private static byte[] attributeNamespaceIds = new byte[]
		{
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 27, 27, 27, 27, 27, 27, 27, 27, 27,
			27, 27, 27, 27, 27, 27, 27, 27, 27, 27,
			27, 27, 27, 27, 0, 0, 0, 0, 0, 0,
			27, 27, 27, 27, 27, 27, 27, 27, 27, 27,
			27, 0
		};
	}
}
