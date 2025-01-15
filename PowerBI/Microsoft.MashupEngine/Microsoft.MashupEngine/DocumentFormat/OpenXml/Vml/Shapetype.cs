using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Vml.Office;
using DocumentFormat.OpenXml.Vml.Presentation;
using DocumentFormat.OpenXml.Vml.Spreadsheet;
using DocumentFormat.OpenXml.Vml.Wordprocessing;

namespace DocumentFormat.OpenXml.Vml
{
	// Token: 0x0200224C RID: 8780
	[ChildElementInfo(typeof(Extrusion))]
	[ChildElementInfo(typeof(LeftBorder))]
	[ChildElementInfo(typeof(ClientData))]
	[ChildElementInfo(typeof(TextData))]
	[ChildElementInfo(typeof(Complex))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(RightBorder))]
	[ChildElementInfo(typeof(Formulas))]
	[ChildElementInfo(typeof(ShapeHandles))]
	[ChildElementInfo(typeof(Fill))]
	[ChildElementInfo(typeof(Stroke))]
	[ChildElementInfo(typeof(Shadow))]
	[ChildElementInfo(typeof(TextBox))]
	[ChildElementInfo(typeof(TextPath))]
	[ChildElementInfo(typeof(ImageData))]
	[ChildElementInfo(typeof(Skew))]
	[ChildElementInfo(typeof(Path))]
	[ChildElementInfo(typeof(Callout))]
	[ChildElementInfo(typeof(Lock))]
	[ChildElementInfo(typeof(ClipPath))]
	[ChildElementInfo(typeof(SignatureLine))]
	[ChildElementInfo(typeof(TextWrap))]
	[ChildElementInfo(typeof(AnchorLock))]
	[ChildElementInfo(typeof(TopBorder))]
	[ChildElementInfo(typeof(BottomBorder))]
	internal class Shapetype : OpenXmlCompositeElement
	{
		// Token: 0x17003A5C RID: 14940
		// (get) Token: 0x0600E27A RID: 57978 RVA: 0x002C2079 File Offset: 0x002C0279
		public override string LocalName
		{
			get
			{
				return "shapetype";
			}
		}

		// Token: 0x17003A5D RID: 14941
		// (get) Token: 0x0600E27B RID: 57979 RVA: 0x00243C87 File Offset: 0x00241E87
		internal override byte NamespaceId
		{
			get
			{
				return 26;
			}
		}

		// Token: 0x17003A5E RID: 14942
		// (get) Token: 0x0600E27C RID: 57980 RVA: 0x002C2080 File Offset: 0x002C0280
		internal override int ElementTypeId
		{
			get
			{
				return 12516;
			}
		}

		// Token: 0x0600E27D RID: 57981 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17003A5F RID: 14943
		// (get) Token: 0x0600E27E RID: 57982 RVA: 0x002C2087 File Offset: 0x002C0287
		internal override string[] AttributeTagNames
		{
			get
			{
				return Shapetype.attributeTagNames;
			}
		}

		// Token: 0x17003A60 RID: 14944
		// (get) Token: 0x0600E27F RID: 57983 RVA: 0x002C208E File Offset: 0x002C028E
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Shapetype.attributeNamespaceIds;
			}
		}

		// Token: 0x17003A61 RID: 14945
		// (get) Token: 0x0600E280 RID: 57984 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0600E281 RID: 57985 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x17003A62 RID: 14946
		// (get) Token: 0x0600E282 RID: 57986 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x0600E283 RID: 57987 RVA: 0x002BD47A File Offset: 0x002BB67A
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

		// Token: 0x17003A63 RID: 14947
		// (get) Token: 0x0600E284 RID: 57988 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x0600E285 RID: 57989 RVA: 0x002BD494 File Offset: 0x002BB694
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

		// Token: 0x17003A64 RID: 14948
		// (get) Token: 0x0600E286 RID: 57990 RVA: 0x002BDADA File Offset: 0x002BBCDA
		// (set) Token: 0x0600E287 RID: 57991 RVA: 0x002BD4AE File Offset: 0x002BB6AE
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

		// Token: 0x17003A65 RID: 14949
		// (get) Token: 0x0600E288 RID: 57992 RVA: 0x002BD4B9 File Offset: 0x002BB6B9
		// (set) Token: 0x0600E289 RID: 57993 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
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

		// Token: 0x17003A66 RID: 14950
		// (get) Token: 0x0600E28A RID: 57994 RVA: 0x002BE081 File Offset: 0x002BC281
		// (set) Token: 0x0600E28B RID: 57995 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
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

		// Token: 0x17003A67 RID: 14951
		// (get) Token: 0x0600E28C RID: 57996 RVA: 0x002BD4ED File Offset: 0x002BB6ED
		// (set) Token: 0x0600E28D RID: 57997 RVA: 0x002BD4FC File Offset: 0x002BB6FC
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

		// Token: 0x17003A68 RID: 14952
		// (get) Token: 0x0600E28E RID: 57998 RVA: 0x002BDB07 File Offset: 0x002BBD07
		// (set) Token: 0x0600E28F RID: 57999 RVA: 0x002BD516 File Offset: 0x002BB716
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

		// Token: 0x17003A69 RID: 14953
		// (get) Token: 0x0600E290 RID: 58000 RVA: 0x002BDB16 File Offset: 0x002BBD16
		// (set) Token: 0x0600E291 RID: 58001 RVA: 0x002BD530 File Offset: 0x002BB730
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

		// Token: 0x17003A6A RID: 14954
		// (get) Token: 0x0600E292 RID: 58002 RVA: 0x002BDB25 File Offset: 0x002BBD25
		// (set) Token: 0x0600E293 RID: 58003 RVA: 0x002BD54B File Offset: 0x002BB74B
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

		// Token: 0x17003A6B RID: 14955
		// (get) Token: 0x0600E294 RID: 58004 RVA: 0x002BE827 File Offset: 0x002BCA27
		// (set) Token: 0x0600E295 RID: 58005 RVA: 0x002BDB45 File Offset: 0x002BBD45
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

		// Token: 0x17003A6C RID: 14956
		// (get) Token: 0x0600E296 RID: 58006 RVA: 0x002BDB51 File Offset: 0x002BBD51
		// (set) Token: 0x0600E297 RID: 58007 RVA: 0x002BDB61 File Offset: 0x002BBD61
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

		// Token: 0x17003A6D RID: 14957
		// (get) Token: 0x0600E298 RID: 58008 RVA: 0x002BE1E9 File Offset: 0x002BC3E9
		// (set) Token: 0x0600E299 RID: 58009 RVA: 0x002BDB7D File Offset: 0x002BBD7D
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

		// Token: 0x17003A6E RID: 14958
		// (get) Token: 0x0600E29A RID: 58010 RVA: 0x002C1380 File Offset: 0x002BF580
		// (set) Token: 0x0600E29B RID: 58011 RVA: 0x002BE209 File Offset: 0x002BC409
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

		// Token: 0x17003A6F RID: 14959
		// (get) Token: 0x0600E29C RID: 58012 RVA: 0x002BFFE2 File Offset: 0x002BE1E2
		// (set) Token: 0x0600E29D RID: 58013 RVA: 0x002BE225 File Offset: 0x002BC425
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

		// Token: 0x17003A70 RID: 14960
		// (get) Token: 0x0600E29E RID: 58014 RVA: 0x002C02BC File Offset: 0x002BE4BC
		// (set) Token: 0x0600E29F RID: 58015 RVA: 0x002BE241 File Offset: 0x002BC441
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

		// Token: 0x17003A71 RID: 14961
		// (get) Token: 0x0600E2A0 RID: 58016 RVA: 0x002BEF3F File Offset: 0x002BD13F
		// (set) Token: 0x0600E2A1 RID: 58017 RVA: 0x002BE25D File Offset: 0x002BC45D
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

		// Token: 0x17003A72 RID: 14962
		// (get) Token: 0x0600E2A2 RID: 58018 RVA: 0x002C1390 File Offset: 0x002BF590
		// (set) Token: 0x0600E2A3 RID: 58019 RVA: 0x002BE279 File Offset: 0x002BC479
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

		// Token: 0x17003A73 RID: 14963
		// (get) Token: 0x0600E2A4 RID: 58020 RVA: 0x002C13A0 File Offset: 0x002BF5A0
		// (set) Token: 0x0600E2A5 RID: 58021 RVA: 0x002BE295 File Offset: 0x002BC495
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

		// Token: 0x17003A74 RID: 14964
		// (get) Token: 0x0600E2A6 RID: 58022 RVA: 0x002C13B0 File Offset: 0x002BF5B0
		// (set) Token: 0x0600E2A7 RID: 58023 RVA: 0x002BE2B1 File Offset: 0x002BC4B1
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

		// Token: 0x17003A75 RID: 14965
		// (get) Token: 0x0600E2A8 RID: 58024 RVA: 0x002BE2BD File Offset: 0x002BC4BD
		// (set) Token: 0x0600E2A9 RID: 58025 RVA: 0x002BE2CD File Offset: 0x002BC4CD
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

		// Token: 0x17003A76 RID: 14966
		// (get) Token: 0x0600E2AA RID: 58026 RVA: 0x002C13C0 File Offset: 0x002BF5C0
		// (set) Token: 0x0600E2AB RID: 58027 RVA: 0x002BE2E9 File Offset: 0x002BC4E9
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

		// Token: 0x17003A77 RID: 14967
		// (get) Token: 0x0600E2AC RID: 58028 RVA: 0x002C13D0 File Offset: 0x002BF5D0
		// (set) Token: 0x0600E2AD RID: 58029 RVA: 0x002BE305 File Offset: 0x002BC505
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

		// Token: 0x17003A78 RID: 14968
		// (get) Token: 0x0600E2AE RID: 58030 RVA: 0x002BE311 File Offset: 0x002BC511
		// (set) Token: 0x0600E2AF RID: 58031 RVA: 0x002BE321 File Offset: 0x002BC521
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

		// Token: 0x17003A79 RID: 14969
		// (get) Token: 0x0600E2B0 RID: 58032 RVA: 0x002C02EC File Offset: 0x002BE4EC
		// (set) Token: 0x0600E2B1 RID: 58033 RVA: 0x002BE33D File Offset: 0x002BC53D
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

		// Token: 0x17003A7A RID: 14970
		// (get) Token: 0x0600E2B2 RID: 58034 RVA: 0x002C0793 File Offset: 0x002BE993
		// (set) Token: 0x0600E2B3 RID: 58035 RVA: 0x002BE359 File Offset: 0x002BC559
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

		// Token: 0x17003A7B RID: 14971
		// (get) Token: 0x0600E2B4 RID: 58036 RVA: 0x002BE365 File Offset: 0x002BC565
		// (set) Token: 0x0600E2B5 RID: 58037 RVA: 0x002BE375 File Offset: 0x002BC575
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

		// Token: 0x17003A7C RID: 14972
		// (get) Token: 0x0600E2B6 RID: 58038 RVA: 0x002C13E0 File Offset: 0x002BF5E0
		// (set) Token: 0x0600E2B7 RID: 58039 RVA: 0x002BE391 File Offset: 0x002BC591
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

		// Token: 0x17003A7D RID: 14973
		// (get) Token: 0x0600E2B8 RID: 58040 RVA: 0x002BE39D File Offset: 0x002BC59D
		// (set) Token: 0x0600E2B9 RID: 58041 RVA: 0x002BE3AD File Offset: 0x002BC5AD
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

		// Token: 0x17003A7E RID: 14974
		// (get) Token: 0x0600E2BA RID: 58042 RVA: 0x002BE3B9 File Offset: 0x002BC5B9
		// (set) Token: 0x0600E2BB RID: 58043 RVA: 0x002BE3C9 File Offset: 0x002BC5C9
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

		// Token: 0x17003A7F RID: 14975
		// (get) Token: 0x0600E2BC RID: 58044 RVA: 0x002C13F0 File Offset: 0x002BF5F0
		// (set) Token: 0x0600E2BD RID: 58045 RVA: 0x002BE3E5 File Offset: 0x002BC5E5
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

		// Token: 0x17003A80 RID: 14976
		// (get) Token: 0x0600E2BE RID: 58046 RVA: 0x002C1400 File Offset: 0x002BF600
		// (set) Token: 0x0600E2BF RID: 58047 RVA: 0x002C1410 File Offset: 0x002BF610
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

		// Token: 0x17003A81 RID: 14977
		// (get) Token: 0x0600E2C0 RID: 58048 RVA: 0x002C141C File Offset: 0x002BF61C
		// (set) Token: 0x0600E2C1 RID: 58049 RVA: 0x002C142C File Offset: 0x002BF62C
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

		// Token: 0x17003A82 RID: 14978
		// (get) Token: 0x0600E2C2 RID: 58050 RVA: 0x002C1438 File Offset: 0x002BF638
		// (set) Token: 0x0600E2C3 RID: 58051 RVA: 0x002C1448 File Offset: 0x002BF648
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

		// Token: 0x17003A83 RID: 14979
		// (get) Token: 0x0600E2C4 RID: 58052 RVA: 0x002C1454 File Offset: 0x002BF654
		// (set) Token: 0x0600E2C5 RID: 58053 RVA: 0x002C1464 File Offset: 0x002BF664
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

		// Token: 0x17003A84 RID: 14980
		// (get) Token: 0x0600E2C6 RID: 58054 RVA: 0x002C1470 File Offset: 0x002BF670
		// (set) Token: 0x0600E2C7 RID: 58055 RVA: 0x002C1480 File Offset: 0x002BF680
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

		// Token: 0x17003A85 RID: 14981
		// (get) Token: 0x0600E2C8 RID: 58056 RVA: 0x002C148C File Offset: 0x002BF68C
		// (set) Token: 0x0600E2C9 RID: 58057 RVA: 0x002C149C File Offset: 0x002BF69C
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

		// Token: 0x17003A86 RID: 14982
		// (get) Token: 0x0600E2CA RID: 58058 RVA: 0x002C14A8 File Offset: 0x002BF6A8
		// (set) Token: 0x0600E2CB RID: 58059 RVA: 0x002C14B8 File Offset: 0x002BF6B8
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

		// Token: 0x17003A87 RID: 14983
		// (get) Token: 0x0600E2CC RID: 58060 RVA: 0x002C14C4 File Offset: 0x002BF6C4
		// (set) Token: 0x0600E2CD RID: 58061 RVA: 0x002C14D4 File Offset: 0x002BF6D4
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

		// Token: 0x17003A88 RID: 14984
		// (get) Token: 0x0600E2CE RID: 58062 RVA: 0x002C14E0 File Offset: 0x002BF6E0
		// (set) Token: 0x0600E2CF RID: 58063 RVA: 0x002C14F0 File Offset: 0x002BF6F0
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

		// Token: 0x17003A89 RID: 14985
		// (get) Token: 0x0600E2D0 RID: 58064 RVA: 0x002C14FC File Offset: 0x002BF6FC
		// (set) Token: 0x0600E2D1 RID: 58065 RVA: 0x002C150C File Offset: 0x002BF70C
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

		// Token: 0x17003A8A RID: 14986
		// (get) Token: 0x0600E2D2 RID: 58066 RVA: 0x002C1518 File Offset: 0x002BF718
		// (set) Token: 0x0600E2D3 RID: 58067 RVA: 0x002C1528 File Offset: 0x002BF728
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

		// Token: 0x17003A8B RID: 14987
		// (get) Token: 0x0600E2D4 RID: 58068 RVA: 0x002C1534 File Offset: 0x002BF734
		// (set) Token: 0x0600E2D5 RID: 58069 RVA: 0x002C1544 File Offset: 0x002BF744
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

		// Token: 0x17003A8C RID: 14988
		// (get) Token: 0x0600E2D6 RID: 58070 RVA: 0x002C1550 File Offset: 0x002BF750
		// (set) Token: 0x0600E2D7 RID: 58071 RVA: 0x002C1560 File Offset: 0x002BF760
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

		// Token: 0x17003A8D RID: 14989
		// (get) Token: 0x0600E2D8 RID: 58072 RVA: 0x002C156C File Offset: 0x002BF76C
		// (set) Token: 0x0600E2D9 RID: 58073 RVA: 0x002C157C File Offset: 0x002BF77C
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

		// Token: 0x17003A8E RID: 14990
		// (get) Token: 0x0600E2DA RID: 58074 RVA: 0x002C1588 File Offset: 0x002BF788
		// (set) Token: 0x0600E2DB RID: 58075 RVA: 0x002C1598 File Offset: 0x002BF798
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

		// Token: 0x17003A8F RID: 14991
		// (get) Token: 0x0600E2DC RID: 58076 RVA: 0x002C15A4 File Offset: 0x002BF7A4
		// (set) Token: 0x0600E2DD RID: 58077 RVA: 0x002C15B4 File Offset: 0x002BF7B4
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

		// Token: 0x17003A90 RID: 14992
		// (get) Token: 0x0600E2DE RID: 58078 RVA: 0x002C15C0 File Offset: 0x002BF7C0
		// (set) Token: 0x0600E2DF RID: 58079 RVA: 0x002C15D0 File Offset: 0x002BF7D0
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

		// Token: 0x17003A91 RID: 14993
		// (get) Token: 0x0600E2E0 RID: 58080 RVA: 0x002C15DC File Offset: 0x002BF7DC
		// (set) Token: 0x0600E2E1 RID: 58081 RVA: 0x002C15EC File Offset: 0x002BF7EC
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

		// Token: 0x17003A92 RID: 14994
		// (get) Token: 0x0600E2E2 RID: 58082 RVA: 0x002C15F8 File Offset: 0x002BF7F8
		// (set) Token: 0x0600E2E3 RID: 58083 RVA: 0x002C1608 File Offset: 0x002BF808
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

		// Token: 0x17003A93 RID: 14995
		// (get) Token: 0x0600E2E4 RID: 58084 RVA: 0x002C1614 File Offset: 0x002BF814
		// (set) Token: 0x0600E2E5 RID: 58085 RVA: 0x002C1624 File Offset: 0x002BF824
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

		// Token: 0x17003A94 RID: 14996
		// (get) Token: 0x0600E2E6 RID: 58086 RVA: 0x002C1630 File Offset: 0x002BF830
		// (set) Token: 0x0600E2E7 RID: 58087 RVA: 0x002C1640 File Offset: 0x002BF840
		[SchemaAttr(0, "adj")]
		public StringValue Adjustment
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

		// Token: 0x17003A95 RID: 14997
		// (get) Token: 0x0600E2E8 RID: 58088 RVA: 0x002C164C File Offset: 0x002BF84C
		// (set) Token: 0x0600E2E9 RID: 58089 RVA: 0x002C165C File Offset: 0x002BF85C
		[SchemaAttr(0, "path")]
		public StringValue EdgePath
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

		// Token: 0x17003A96 RID: 14998
		// (get) Token: 0x0600E2EA RID: 58090 RVA: 0x002C1668 File Offset: 0x002BF868
		// (set) Token: 0x0600E2EB RID: 58091 RVA: 0x002C1678 File Offset: 0x002BF878
		[SchemaAttr(27, "master")]
		public StringValue Master
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

		// Token: 0x0600E2EC RID: 58092 RVA: 0x00293ECF File Offset: 0x002920CF
		public Shapetype()
		{
		}

		// Token: 0x0600E2ED RID: 58093 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Shapetype(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600E2EE RID: 58094 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Shapetype(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600E2EF RID: 58095 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Shapetype(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0600E2F0 RID: 58096 RVA: 0x002C2098 File Offset: 0x002C0298
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
			if (27 == namespaceId && "complex" == name)
			{
				return new Complex();
			}
			return null;
		}

		// Token: 0x0600E2F1 RID: 58097 RVA: 0x002C22E8 File Offset: 0x002C04E8
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
			if (namespaceId == 0 && "adj" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "path" == name)
			{
				return new StringValue();
			}
			if (27 == namespaceId && "master" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0600E2F2 RID: 58098 RVA: 0x002C27E7 File Offset: 0x002C09E7
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Shapetype>(deep);
		}

		// Token: 0x04006EC2 RID: 28354
		private const string tagName = "shapetype";

		// Token: 0x04006EC3 RID: 28355
		private const byte tagNsId = 26;

		// Token: 0x04006EC4 RID: 28356
		internal const int ElementTypeIdConst = 12516;

		// Token: 0x04006EC5 RID: 28357
		private static string[] attributeTagNames = new string[]
		{
			"id", "style", "href", "target", "class", "title", "alt", "coordsize", "coordorigin", "wrapcoords",
			"print", "spid", "oned", "regroupid", "doubleclicknotify", "button", "userhidden", "bullet", "hr", "hrstd",
			"hrnoshade", "hrpct", "hralign", "allowincell", "allowoverlap", "userdrawn", "bordertopcolor", "borderleftcolor", "borderbottomcolor", "borderrightcolor",
			"dgmlayout", "dgmnodekind", "dgmlayoutmru", "insetmode", "filled", "fillcolor", "stroked", "strokecolor", "strokeweight", "insetpen",
			"spt", "connectortype", "bwmode", "bwpure", "bwnormal", "forcedash", "oleicon", "ole", "preferrelative", "cliptowrap",
			"clip", "adj", "path", "master"
		};

		// Token: 0x04006EC6 RID: 28358
		private static byte[] attributeNamespaceIds = new byte[]
		{
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 27, 27, 27, 27, 27, 27, 27, 27, 27,
			27, 27, 27, 27, 27, 27, 27, 27, 27, 27,
			27, 27, 27, 27, 0, 0, 0, 0, 0, 0,
			27, 27, 27, 27, 27, 27, 27, 27, 27, 27,
			27, 0, 0, 27
		};
	}
}
