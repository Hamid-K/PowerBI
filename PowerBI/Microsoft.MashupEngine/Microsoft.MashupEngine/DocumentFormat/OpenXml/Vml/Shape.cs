using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Vml.Office;
using DocumentFormat.OpenXml.Vml.Presentation;
using DocumentFormat.OpenXml.Vml.Spreadsheet;
using DocumentFormat.OpenXml.Vml.Wordprocessing;

namespace DocumentFormat.OpenXml.Vml
{
	// Token: 0x0200224B RID: 8779
	[ChildElementInfo(typeof(SignatureLine))]
	[ChildElementInfo(typeof(RightBorder))]
	[ChildElementInfo(typeof(ClientData))]
	[ChildElementInfo(typeof(TextData))]
	[ChildElementInfo(typeof(Ink))]
	[ChildElementInfo(typeof(InkAnnotationFlag))]
	[GeneratedCode("DomGen", "2.0")]
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
	[ChildElementInfo(typeof(Path))]
	[ChildElementInfo(typeof(TextWrap))]
	[ChildElementInfo(typeof(AnchorLock))]
	[ChildElementInfo(typeof(TopBorder))]
	[ChildElementInfo(typeof(BottomBorder))]
	[ChildElementInfo(typeof(LeftBorder))]
	internal class Shape : OpenXmlCompositeElement
	{
		// Token: 0x17003A1F RID: 14879
		// (get) Token: 0x0600E1FC RID: 57852 RVA: 0x002C1364 File Offset: 0x002BF564
		public override string LocalName
		{
			get
			{
				return "shape";
			}
		}

		// Token: 0x17003A20 RID: 14880
		// (get) Token: 0x0600E1FD RID: 57853 RVA: 0x00243C87 File Offset: 0x00241E87
		internal override byte NamespaceId
		{
			get
			{
				return 26;
			}
		}

		// Token: 0x17003A21 RID: 14881
		// (get) Token: 0x0600E1FE RID: 57854 RVA: 0x002C136B File Offset: 0x002BF56B
		internal override int ElementTypeId
		{
			get
			{
				return 12515;
			}
		}

		// Token: 0x0600E1FF RID: 57855 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17003A22 RID: 14882
		// (get) Token: 0x0600E200 RID: 57856 RVA: 0x002C1372 File Offset: 0x002BF572
		internal override string[] AttributeTagNames
		{
			get
			{
				return Shape.attributeTagNames;
			}
		}

		// Token: 0x17003A23 RID: 14883
		// (get) Token: 0x0600E201 RID: 57857 RVA: 0x002C1379 File Offset: 0x002BF579
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Shape.attributeNamespaceIds;
			}
		}

		// Token: 0x17003A24 RID: 14884
		// (get) Token: 0x0600E202 RID: 57858 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0600E203 RID: 57859 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x17003A25 RID: 14885
		// (get) Token: 0x0600E204 RID: 57860 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x0600E205 RID: 57861 RVA: 0x002BD47A File Offset: 0x002BB67A
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

		// Token: 0x17003A26 RID: 14886
		// (get) Token: 0x0600E206 RID: 57862 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x0600E207 RID: 57863 RVA: 0x002BD494 File Offset: 0x002BB694
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

		// Token: 0x17003A27 RID: 14887
		// (get) Token: 0x0600E208 RID: 57864 RVA: 0x002BDADA File Offset: 0x002BBCDA
		// (set) Token: 0x0600E209 RID: 57865 RVA: 0x002BD4AE File Offset: 0x002BB6AE
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

		// Token: 0x17003A28 RID: 14888
		// (get) Token: 0x0600E20A RID: 57866 RVA: 0x002BD4B9 File Offset: 0x002BB6B9
		// (set) Token: 0x0600E20B RID: 57867 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
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

		// Token: 0x17003A29 RID: 14889
		// (get) Token: 0x0600E20C RID: 57868 RVA: 0x002BE081 File Offset: 0x002BC281
		// (set) Token: 0x0600E20D RID: 57869 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
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

		// Token: 0x17003A2A RID: 14890
		// (get) Token: 0x0600E20E RID: 57870 RVA: 0x002BD4ED File Offset: 0x002BB6ED
		// (set) Token: 0x0600E20F RID: 57871 RVA: 0x002BD4FC File Offset: 0x002BB6FC
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

		// Token: 0x17003A2B RID: 14891
		// (get) Token: 0x0600E210 RID: 57872 RVA: 0x002BDB07 File Offset: 0x002BBD07
		// (set) Token: 0x0600E211 RID: 57873 RVA: 0x002BD516 File Offset: 0x002BB716
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

		// Token: 0x17003A2C RID: 14892
		// (get) Token: 0x0600E212 RID: 57874 RVA: 0x002BDB16 File Offset: 0x002BBD16
		// (set) Token: 0x0600E213 RID: 57875 RVA: 0x002BD530 File Offset: 0x002BB730
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

		// Token: 0x17003A2D RID: 14893
		// (get) Token: 0x0600E214 RID: 57876 RVA: 0x002BDB25 File Offset: 0x002BBD25
		// (set) Token: 0x0600E215 RID: 57877 RVA: 0x002BD54B File Offset: 0x002BB74B
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

		// Token: 0x17003A2E RID: 14894
		// (get) Token: 0x0600E216 RID: 57878 RVA: 0x002BE827 File Offset: 0x002BCA27
		// (set) Token: 0x0600E217 RID: 57879 RVA: 0x002BDB45 File Offset: 0x002BBD45
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

		// Token: 0x17003A2F RID: 14895
		// (get) Token: 0x0600E218 RID: 57880 RVA: 0x002BDB51 File Offset: 0x002BBD51
		// (set) Token: 0x0600E219 RID: 57881 RVA: 0x002BDB61 File Offset: 0x002BBD61
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

		// Token: 0x17003A30 RID: 14896
		// (get) Token: 0x0600E21A RID: 57882 RVA: 0x002BE1E9 File Offset: 0x002BC3E9
		// (set) Token: 0x0600E21B RID: 57883 RVA: 0x002BDB7D File Offset: 0x002BBD7D
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

		// Token: 0x17003A31 RID: 14897
		// (get) Token: 0x0600E21C RID: 57884 RVA: 0x002C1380 File Offset: 0x002BF580
		// (set) Token: 0x0600E21D RID: 57885 RVA: 0x002BE209 File Offset: 0x002BC409
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

		// Token: 0x17003A32 RID: 14898
		// (get) Token: 0x0600E21E RID: 57886 RVA: 0x002BFFE2 File Offset: 0x002BE1E2
		// (set) Token: 0x0600E21F RID: 57887 RVA: 0x002BE225 File Offset: 0x002BC425
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

		// Token: 0x17003A33 RID: 14899
		// (get) Token: 0x0600E220 RID: 57888 RVA: 0x002C02BC File Offset: 0x002BE4BC
		// (set) Token: 0x0600E221 RID: 57889 RVA: 0x002BE241 File Offset: 0x002BC441
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

		// Token: 0x17003A34 RID: 14900
		// (get) Token: 0x0600E222 RID: 57890 RVA: 0x002BEF3F File Offset: 0x002BD13F
		// (set) Token: 0x0600E223 RID: 57891 RVA: 0x002BE25D File Offset: 0x002BC45D
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

		// Token: 0x17003A35 RID: 14901
		// (get) Token: 0x0600E224 RID: 57892 RVA: 0x002C1390 File Offset: 0x002BF590
		// (set) Token: 0x0600E225 RID: 57893 RVA: 0x002BE279 File Offset: 0x002BC479
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

		// Token: 0x17003A36 RID: 14902
		// (get) Token: 0x0600E226 RID: 57894 RVA: 0x002C13A0 File Offset: 0x002BF5A0
		// (set) Token: 0x0600E227 RID: 57895 RVA: 0x002BE295 File Offset: 0x002BC495
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

		// Token: 0x17003A37 RID: 14903
		// (get) Token: 0x0600E228 RID: 57896 RVA: 0x002C13B0 File Offset: 0x002BF5B0
		// (set) Token: 0x0600E229 RID: 57897 RVA: 0x002BE2B1 File Offset: 0x002BC4B1
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

		// Token: 0x17003A38 RID: 14904
		// (get) Token: 0x0600E22A RID: 57898 RVA: 0x002BE2BD File Offset: 0x002BC4BD
		// (set) Token: 0x0600E22B RID: 57899 RVA: 0x002BE2CD File Offset: 0x002BC4CD
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

		// Token: 0x17003A39 RID: 14905
		// (get) Token: 0x0600E22C RID: 57900 RVA: 0x002C13C0 File Offset: 0x002BF5C0
		// (set) Token: 0x0600E22D RID: 57901 RVA: 0x002BE2E9 File Offset: 0x002BC4E9
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

		// Token: 0x17003A3A RID: 14906
		// (get) Token: 0x0600E22E RID: 57902 RVA: 0x002C13D0 File Offset: 0x002BF5D0
		// (set) Token: 0x0600E22F RID: 57903 RVA: 0x002BE305 File Offset: 0x002BC505
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

		// Token: 0x17003A3B RID: 14907
		// (get) Token: 0x0600E230 RID: 57904 RVA: 0x002BE311 File Offset: 0x002BC511
		// (set) Token: 0x0600E231 RID: 57905 RVA: 0x002BE321 File Offset: 0x002BC521
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

		// Token: 0x17003A3C RID: 14908
		// (get) Token: 0x0600E232 RID: 57906 RVA: 0x002C02EC File Offset: 0x002BE4EC
		// (set) Token: 0x0600E233 RID: 57907 RVA: 0x002BE33D File Offset: 0x002BC53D
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

		// Token: 0x17003A3D RID: 14909
		// (get) Token: 0x0600E234 RID: 57908 RVA: 0x002C0793 File Offset: 0x002BE993
		// (set) Token: 0x0600E235 RID: 57909 RVA: 0x002BE359 File Offset: 0x002BC559
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

		// Token: 0x17003A3E RID: 14910
		// (get) Token: 0x0600E236 RID: 57910 RVA: 0x002BE365 File Offset: 0x002BC565
		// (set) Token: 0x0600E237 RID: 57911 RVA: 0x002BE375 File Offset: 0x002BC575
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

		// Token: 0x17003A3F RID: 14911
		// (get) Token: 0x0600E238 RID: 57912 RVA: 0x002C13E0 File Offset: 0x002BF5E0
		// (set) Token: 0x0600E239 RID: 57913 RVA: 0x002BE391 File Offset: 0x002BC591
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

		// Token: 0x17003A40 RID: 14912
		// (get) Token: 0x0600E23A RID: 57914 RVA: 0x002BE39D File Offset: 0x002BC59D
		// (set) Token: 0x0600E23B RID: 57915 RVA: 0x002BE3AD File Offset: 0x002BC5AD
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

		// Token: 0x17003A41 RID: 14913
		// (get) Token: 0x0600E23C RID: 57916 RVA: 0x002BE3B9 File Offset: 0x002BC5B9
		// (set) Token: 0x0600E23D RID: 57917 RVA: 0x002BE3C9 File Offset: 0x002BC5C9
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

		// Token: 0x17003A42 RID: 14914
		// (get) Token: 0x0600E23E RID: 57918 RVA: 0x002C13F0 File Offset: 0x002BF5F0
		// (set) Token: 0x0600E23F RID: 57919 RVA: 0x002BE3E5 File Offset: 0x002BC5E5
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

		// Token: 0x17003A43 RID: 14915
		// (get) Token: 0x0600E240 RID: 57920 RVA: 0x002C1400 File Offset: 0x002BF600
		// (set) Token: 0x0600E241 RID: 57921 RVA: 0x002C1410 File Offset: 0x002BF610
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

		// Token: 0x17003A44 RID: 14916
		// (get) Token: 0x0600E242 RID: 57922 RVA: 0x002C141C File Offset: 0x002BF61C
		// (set) Token: 0x0600E243 RID: 57923 RVA: 0x002C142C File Offset: 0x002BF62C
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

		// Token: 0x17003A45 RID: 14917
		// (get) Token: 0x0600E244 RID: 57924 RVA: 0x002C1438 File Offset: 0x002BF638
		// (set) Token: 0x0600E245 RID: 57925 RVA: 0x002C1448 File Offset: 0x002BF648
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

		// Token: 0x17003A46 RID: 14918
		// (get) Token: 0x0600E246 RID: 57926 RVA: 0x002C1454 File Offset: 0x002BF654
		// (set) Token: 0x0600E247 RID: 57927 RVA: 0x002C1464 File Offset: 0x002BF664
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

		// Token: 0x17003A47 RID: 14919
		// (get) Token: 0x0600E248 RID: 57928 RVA: 0x002C1470 File Offset: 0x002BF670
		// (set) Token: 0x0600E249 RID: 57929 RVA: 0x002C1480 File Offset: 0x002BF680
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

		// Token: 0x17003A48 RID: 14920
		// (get) Token: 0x0600E24A RID: 57930 RVA: 0x002C148C File Offset: 0x002BF68C
		// (set) Token: 0x0600E24B RID: 57931 RVA: 0x002C149C File Offset: 0x002BF69C
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

		// Token: 0x17003A49 RID: 14921
		// (get) Token: 0x0600E24C RID: 57932 RVA: 0x002C14A8 File Offset: 0x002BF6A8
		// (set) Token: 0x0600E24D RID: 57933 RVA: 0x002C14B8 File Offset: 0x002BF6B8
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

		// Token: 0x17003A4A RID: 14922
		// (get) Token: 0x0600E24E RID: 57934 RVA: 0x002C14C4 File Offset: 0x002BF6C4
		// (set) Token: 0x0600E24F RID: 57935 RVA: 0x002C14D4 File Offset: 0x002BF6D4
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

		// Token: 0x17003A4B RID: 14923
		// (get) Token: 0x0600E250 RID: 57936 RVA: 0x002C14E0 File Offset: 0x002BF6E0
		// (set) Token: 0x0600E251 RID: 57937 RVA: 0x002C14F0 File Offset: 0x002BF6F0
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

		// Token: 0x17003A4C RID: 14924
		// (get) Token: 0x0600E252 RID: 57938 RVA: 0x002C14FC File Offset: 0x002BF6FC
		// (set) Token: 0x0600E253 RID: 57939 RVA: 0x002C150C File Offset: 0x002BF70C
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

		// Token: 0x17003A4D RID: 14925
		// (get) Token: 0x0600E254 RID: 57940 RVA: 0x002C1518 File Offset: 0x002BF718
		// (set) Token: 0x0600E255 RID: 57941 RVA: 0x002C1528 File Offset: 0x002BF728
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

		// Token: 0x17003A4E RID: 14926
		// (get) Token: 0x0600E256 RID: 57942 RVA: 0x002C1534 File Offset: 0x002BF734
		// (set) Token: 0x0600E257 RID: 57943 RVA: 0x002C1544 File Offset: 0x002BF744
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

		// Token: 0x17003A4F RID: 14927
		// (get) Token: 0x0600E258 RID: 57944 RVA: 0x002C1550 File Offset: 0x002BF750
		// (set) Token: 0x0600E259 RID: 57945 RVA: 0x002C1560 File Offset: 0x002BF760
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

		// Token: 0x17003A50 RID: 14928
		// (get) Token: 0x0600E25A RID: 57946 RVA: 0x002C156C File Offset: 0x002BF76C
		// (set) Token: 0x0600E25B RID: 57947 RVA: 0x002C157C File Offset: 0x002BF77C
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

		// Token: 0x17003A51 RID: 14929
		// (get) Token: 0x0600E25C RID: 57948 RVA: 0x002C1588 File Offset: 0x002BF788
		// (set) Token: 0x0600E25D RID: 57949 RVA: 0x002C1598 File Offset: 0x002BF798
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

		// Token: 0x17003A52 RID: 14930
		// (get) Token: 0x0600E25E RID: 57950 RVA: 0x002C15A4 File Offset: 0x002BF7A4
		// (set) Token: 0x0600E25F RID: 57951 RVA: 0x002C15B4 File Offset: 0x002BF7B4
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

		// Token: 0x17003A53 RID: 14931
		// (get) Token: 0x0600E260 RID: 57952 RVA: 0x002C15C0 File Offset: 0x002BF7C0
		// (set) Token: 0x0600E261 RID: 57953 RVA: 0x002C15D0 File Offset: 0x002BF7D0
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

		// Token: 0x17003A54 RID: 14932
		// (get) Token: 0x0600E262 RID: 57954 RVA: 0x002C15DC File Offset: 0x002BF7DC
		// (set) Token: 0x0600E263 RID: 57955 RVA: 0x002C15EC File Offset: 0x002BF7EC
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

		// Token: 0x17003A55 RID: 14933
		// (get) Token: 0x0600E264 RID: 57956 RVA: 0x002C15F8 File Offset: 0x002BF7F8
		// (set) Token: 0x0600E265 RID: 57957 RVA: 0x002C1608 File Offset: 0x002BF808
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

		// Token: 0x17003A56 RID: 14934
		// (get) Token: 0x0600E266 RID: 57958 RVA: 0x002C1614 File Offset: 0x002BF814
		// (set) Token: 0x0600E267 RID: 57959 RVA: 0x002C1624 File Offset: 0x002BF824
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

		// Token: 0x17003A57 RID: 14935
		// (get) Token: 0x0600E268 RID: 57960 RVA: 0x002C1630 File Offset: 0x002BF830
		// (set) Token: 0x0600E269 RID: 57961 RVA: 0x002C1640 File Offset: 0x002BF840
		[SchemaAttr(0, "type")]
		public StringValue Type
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

		// Token: 0x17003A58 RID: 14936
		// (get) Token: 0x0600E26A RID: 57962 RVA: 0x002C164C File Offset: 0x002BF84C
		// (set) Token: 0x0600E26B RID: 57963 RVA: 0x002C165C File Offset: 0x002BF85C
		[SchemaAttr(0, "adj")]
		public StringValue Adjustment
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

		// Token: 0x17003A59 RID: 14937
		// (get) Token: 0x0600E26C RID: 57964 RVA: 0x002C1668 File Offset: 0x002BF868
		// (set) Token: 0x0600E26D RID: 57965 RVA: 0x002C1678 File Offset: 0x002BF878
		[SchemaAttr(0, "path")]
		public StringValue EdgePath
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

		// Token: 0x17003A5A RID: 14938
		// (get) Token: 0x0600E26E RID: 57966 RVA: 0x002C1684 File Offset: 0x002BF884
		// (set) Token: 0x0600E26F RID: 57967 RVA: 0x002C1694 File Offset: 0x002BF894
		[SchemaAttr(27, "gfxdata")]
		public Base64BinaryValue EncodedPackage
		{
			get
			{
				return (Base64BinaryValue)base.Attributes[54];
			}
			set
			{
				base.Attributes[54] = value;
			}
		}

		// Token: 0x17003A5B RID: 14939
		// (get) Token: 0x0600E270 RID: 57968 RVA: 0x002C16A0 File Offset: 0x002BF8A0
		// (set) Token: 0x0600E271 RID: 57969 RVA: 0x002C16B0 File Offset: 0x002BF8B0
		[SchemaAttr(0, "equationxml")]
		public StringValue EquationXml
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

		// Token: 0x0600E272 RID: 57970 RVA: 0x00293ECF File Offset: 0x002920CF
		public Shape()
		{
		}

		// Token: 0x0600E273 RID: 57971 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Shape(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600E274 RID: 57972 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Shape(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600E275 RID: 57973 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Shape(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0600E276 RID: 57974 RVA: 0x002C16BC File Offset: 0x002BF8BC
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
			if (30 == namespaceId && "iscomment" == name)
			{
				return new InkAnnotationFlag();
			}
			return null;
		}

		// Token: 0x0600E277 RID: 57975 RVA: 0x002C1924 File Offset: 0x002BFB24
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
			if (namespaceId == 0 && "type" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "adj" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "path" == name)
			{
				return new StringValue();
			}
			if (27 == namespaceId && "gfxdata" == name)
			{
				return new Base64BinaryValue();
			}
			if (namespaceId == 0 && "equationxml" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0600E278 RID: 57976 RVA: 0x002C1E4F File Offset: 0x002C004F
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Shape>(deep);
		}

		// Token: 0x04006EBD RID: 28349
		private const string tagName = "shape";

		// Token: 0x04006EBE RID: 28350
		private const byte tagNsId = 26;

		// Token: 0x04006EBF RID: 28351
		internal const int ElementTypeIdConst = 12515;

		// Token: 0x04006EC0 RID: 28352
		private static string[] attributeTagNames = new string[]
		{
			"id", "style", "href", "target", "class", "title", "alt", "coordsize", "coordorigin", "wrapcoords",
			"print", "spid", "oned", "regroupid", "doubleclicknotify", "button", "userhidden", "bullet", "hr", "hrstd",
			"hrnoshade", "hrpct", "hralign", "allowincell", "allowoverlap", "userdrawn", "bordertopcolor", "borderleftcolor", "borderbottomcolor", "borderrightcolor",
			"dgmlayout", "dgmnodekind", "dgmlayoutmru", "insetmode", "filled", "fillcolor", "stroked", "strokecolor", "strokeweight", "insetpen",
			"spt", "connectortype", "bwmode", "bwpure", "bwnormal", "forcedash", "oleicon", "ole", "preferrelative", "cliptowrap",
			"clip", "type", "adj", "path", "gfxdata", "equationxml"
		};

		// Token: 0x04006EC1 RID: 28353
		private static byte[] attributeNamespaceIds = new byte[]
		{
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 27, 27, 27, 27, 27, 27, 27, 27, 27,
			27, 27, 27, 27, 27, 27, 27, 27, 27, 27,
			27, 27, 27, 27, 0, 0, 0, 0, 0, 0,
			27, 27, 27, 27, 27, 27, 27, 27, 27, 27,
			27, 0, 0, 0, 27, 0
		};
	}
}
