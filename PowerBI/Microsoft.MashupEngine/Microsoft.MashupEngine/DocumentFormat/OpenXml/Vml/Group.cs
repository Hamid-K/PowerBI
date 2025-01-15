using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Vml.Office;
using DocumentFormat.OpenXml.Vml.Spreadsheet;
using DocumentFormat.OpenXml.Vml.Wordprocessing;

namespace DocumentFormat.OpenXml.Vml
{
	// Token: 0x0200224D RID: 8781
	[ChildElementInfo(typeof(ClientData))]
	[ChildElementInfo(typeof(Shape))]
	[ChildElementInfo(typeof(Shapetype))]
	[ChildElementInfo(typeof(Arc))]
	[ChildElementInfo(typeof(Curve))]
	[ChildElementInfo(typeof(ImageFile))]
	[ChildElementInfo(typeof(Line))]
	[ChildElementInfo(typeof(Oval))]
	[ChildElementInfo(typeof(PolyLine))]
	[ChildElementInfo(typeof(Rectangle))]
	[ChildElementInfo(typeof(RoundRectangle))]
	[ChildElementInfo(typeof(Diagram))]
	[ChildElementInfo(typeof(Lock))]
	[ChildElementInfo(typeof(ClipPath))]
	[ChildElementInfo(typeof(TextWrap))]
	[ChildElementInfo(typeof(AnchorLock))]
	[ChildElementInfo(typeof(Group))]
	[GeneratedCode("DomGen", "2.0")]
	internal class Group : OpenXmlCompositeElement
	{
		// Token: 0x17003A97 RID: 14999
		// (get) Token: 0x0600E2F4 RID: 58100 RVA: 0x002C29FF File Offset: 0x002C0BFF
		public override string LocalName
		{
			get
			{
				return "group";
			}
		}

		// Token: 0x17003A98 RID: 15000
		// (get) Token: 0x0600E2F5 RID: 58101 RVA: 0x00243C87 File Offset: 0x00241E87
		internal override byte NamespaceId
		{
			get
			{
				return 26;
			}
		}

		// Token: 0x17003A99 RID: 15001
		// (get) Token: 0x0600E2F6 RID: 58102 RVA: 0x002C2A06 File Offset: 0x002C0C06
		internal override int ElementTypeId
		{
			get
			{
				return 12517;
			}
		}

		// Token: 0x0600E2F7 RID: 58103 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17003A9A RID: 15002
		// (get) Token: 0x0600E2F8 RID: 58104 RVA: 0x002C2A0D File Offset: 0x002C0C0D
		internal override string[] AttributeTagNames
		{
			get
			{
				return Group.attributeTagNames;
			}
		}

		// Token: 0x17003A9B RID: 15003
		// (get) Token: 0x0600E2F9 RID: 58105 RVA: 0x002C2A14 File Offset: 0x002C0C14
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Group.attributeNamespaceIds;
			}
		}

		// Token: 0x17003A9C RID: 15004
		// (get) Token: 0x0600E2FA RID: 58106 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0600E2FB RID: 58107 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x17003A9D RID: 15005
		// (get) Token: 0x0600E2FC RID: 58108 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x0600E2FD RID: 58109 RVA: 0x002BD47A File Offset: 0x002BB67A
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

		// Token: 0x17003A9E RID: 15006
		// (get) Token: 0x0600E2FE RID: 58110 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x0600E2FF RID: 58111 RVA: 0x002BD494 File Offset: 0x002BB694
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

		// Token: 0x17003A9F RID: 15007
		// (get) Token: 0x0600E300 RID: 58112 RVA: 0x002BDADA File Offset: 0x002BBCDA
		// (set) Token: 0x0600E301 RID: 58113 RVA: 0x002BD4AE File Offset: 0x002BB6AE
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

		// Token: 0x17003AA0 RID: 15008
		// (get) Token: 0x0600E302 RID: 58114 RVA: 0x002BD4B9 File Offset: 0x002BB6B9
		// (set) Token: 0x0600E303 RID: 58115 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
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

		// Token: 0x17003AA1 RID: 15009
		// (get) Token: 0x0600E304 RID: 58116 RVA: 0x002BE081 File Offset: 0x002BC281
		// (set) Token: 0x0600E305 RID: 58117 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
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

		// Token: 0x17003AA2 RID: 15010
		// (get) Token: 0x0600E306 RID: 58118 RVA: 0x002BD4ED File Offset: 0x002BB6ED
		// (set) Token: 0x0600E307 RID: 58119 RVA: 0x002BD4FC File Offset: 0x002BB6FC
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

		// Token: 0x17003AA3 RID: 15011
		// (get) Token: 0x0600E308 RID: 58120 RVA: 0x002BDB07 File Offset: 0x002BBD07
		// (set) Token: 0x0600E309 RID: 58121 RVA: 0x002BD516 File Offset: 0x002BB716
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

		// Token: 0x17003AA4 RID: 15012
		// (get) Token: 0x0600E30A RID: 58122 RVA: 0x002BDB16 File Offset: 0x002BBD16
		// (set) Token: 0x0600E30B RID: 58123 RVA: 0x002BD530 File Offset: 0x002BB730
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

		// Token: 0x17003AA5 RID: 15013
		// (get) Token: 0x0600E30C RID: 58124 RVA: 0x002BDB25 File Offset: 0x002BBD25
		// (set) Token: 0x0600E30D RID: 58125 RVA: 0x002BD54B File Offset: 0x002BB74B
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

		// Token: 0x17003AA6 RID: 15014
		// (get) Token: 0x0600E30E RID: 58126 RVA: 0x002BE827 File Offset: 0x002BCA27
		// (set) Token: 0x0600E30F RID: 58127 RVA: 0x002BDB45 File Offset: 0x002BBD45
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

		// Token: 0x17003AA7 RID: 15015
		// (get) Token: 0x0600E310 RID: 58128 RVA: 0x002BDB51 File Offset: 0x002BBD51
		// (set) Token: 0x0600E311 RID: 58129 RVA: 0x002BDB61 File Offset: 0x002BBD61
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

		// Token: 0x17003AA8 RID: 15016
		// (get) Token: 0x0600E312 RID: 58130 RVA: 0x002BE1E9 File Offset: 0x002BC3E9
		// (set) Token: 0x0600E313 RID: 58131 RVA: 0x002BDB7D File Offset: 0x002BBD7D
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

		// Token: 0x17003AA9 RID: 15017
		// (get) Token: 0x0600E314 RID: 58132 RVA: 0x002C1380 File Offset: 0x002BF580
		// (set) Token: 0x0600E315 RID: 58133 RVA: 0x002BE209 File Offset: 0x002BC409
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

		// Token: 0x17003AAA RID: 15018
		// (get) Token: 0x0600E316 RID: 58134 RVA: 0x002BFFE2 File Offset: 0x002BE1E2
		// (set) Token: 0x0600E317 RID: 58135 RVA: 0x002BE225 File Offset: 0x002BC425
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

		// Token: 0x17003AAB RID: 15019
		// (get) Token: 0x0600E318 RID: 58136 RVA: 0x002C02BC File Offset: 0x002BE4BC
		// (set) Token: 0x0600E319 RID: 58137 RVA: 0x002BE241 File Offset: 0x002BC441
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

		// Token: 0x17003AAC RID: 15020
		// (get) Token: 0x0600E31A RID: 58138 RVA: 0x002BEF3F File Offset: 0x002BD13F
		// (set) Token: 0x0600E31B RID: 58139 RVA: 0x002BE25D File Offset: 0x002BC45D
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

		// Token: 0x17003AAD RID: 15021
		// (get) Token: 0x0600E31C RID: 58140 RVA: 0x002C1390 File Offset: 0x002BF590
		// (set) Token: 0x0600E31D RID: 58141 RVA: 0x002BE279 File Offset: 0x002BC479
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

		// Token: 0x17003AAE RID: 15022
		// (get) Token: 0x0600E31E RID: 58142 RVA: 0x002C13A0 File Offset: 0x002BF5A0
		// (set) Token: 0x0600E31F RID: 58143 RVA: 0x002BE295 File Offset: 0x002BC495
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

		// Token: 0x17003AAF RID: 15023
		// (get) Token: 0x0600E320 RID: 58144 RVA: 0x002C13B0 File Offset: 0x002BF5B0
		// (set) Token: 0x0600E321 RID: 58145 RVA: 0x002BE2B1 File Offset: 0x002BC4B1
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

		// Token: 0x17003AB0 RID: 15024
		// (get) Token: 0x0600E322 RID: 58146 RVA: 0x002BE2BD File Offset: 0x002BC4BD
		// (set) Token: 0x0600E323 RID: 58147 RVA: 0x002BE2CD File Offset: 0x002BC4CD
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

		// Token: 0x17003AB1 RID: 15025
		// (get) Token: 0x0600E324 RID: 58148 RVA: 0x002C13C0 File Offset: 0x002BF5C0
		// (set) Token: 0x0600E325 RID: 58149 RVA: 0x002BE2E9 File Offset: 0x002BC4E9
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

		// Token: 0x17003AB2 RID: 15026
		// (get) Token: 0x0600E326 RID: 58150 RVA: 0x002C13D0 File Offset: 0x002BF5D0
		// (set) Token: 0x0600E327 RID: 58151 RVA: 0x002BE305 File Offset: 0x002BC505
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

		// Token: 0x17003AB3 RID: 15027
		// (get) Token: 0x0600E328 RID: 58152 RVA: 0x002BE311 File Offset: 0x002BC511
		// (set) Token: 0x0600E329 RID: 58153 RVA: 0x002BE321 File Offset: 0x002BC521
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

		// Token: 0x17003AB4 RID: 15028
		// (get) Token: 0x0600E32A RID: 58154 RVA: 0x002C02EC File Offset: 0x002BE4EC
		// (set) Token: 0x0600E32B RID: 58155 RVA: 0x002BE33D File Offset: 0x002BC53D
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

		// Token: 0x17003AB5 RID: 15029
		// (get) Token: 0x0600E32C RID: 58156 RVA: 0x002C0793 File Offset: 0x002BE993
		// (set) Token: 0x0600E32D RID: 58157 RVA: 0x002BE359 File Offset: 0x002BC559
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

		// Token: 0x17003AB6 RID: 15030
		// (get) Token: 0x0600E32E RID: 58158 RVA: 0x002C2A1B File Offset: 0x002C0C1B
		// (set) Token: 0x0600E32F RID: 58159 RVA: 0x002BE375 File Offset: 0x002BC575
		[SchemaAttr(27, "dgmlayout")]
		public IntegerValue DiagramLayout
		{
			get
			{
				return (IntegerValue)base.Attributes[26];
			}
			set
			{
				base.Attributes[26] = value;
			}
		}

		// Token: 0x17003AB7 RID: 15031
		// (get) Token: 0x0600E330 RID: 58160 RVA: 0x002C2A2B File Offset: 0x002C0C2B
		// (set) Token: 0x0600E331 RID: 58161 RVA: 0x002BE391 File Offset: 0x002BC591
		[SchemaAttr(27, "dgmnodekind")]
		public IntegerValue DiagramNodeKind
		{
			get
			{
				return (IntegerValue)base.Attributes[27];
			}
			set
			{
				base.Attributes[27] = value;
			}
		}

		// Token: 0x17003AB8 RID: 15032
		// (get) Token: 0x0600E332 RID: 58162 RVA: 0x002C2A3B File Offset: 0x002C0C3B
		// (set) Token: 0x0600E333 RID: 58163 RVA: 0x002BE3AD File Offset: 0x002BC5AD
		[SchemaAttr(27, "dgmlayoutmru")]
		public IntegerValue DiagramLayoutMostRecentUsed
		{
			get
			{
				return (IntegerValue)base.Attributes[28];
			}
			set
			{
				base.Attributes[28] = value;
			}
		}

		// Token: 0x17003AB9 RID: 15033
		// (get) Token: 0x0600E334 RID: 58164 RVA: 0x002C2A4B File Offset: 0x002C0C4B
		// (set) Token: 0x0600E335 RID: 58165 RVA: 0x002BE3C9 File Offset: 0x002BC5C9
		[SchemaAttr(27, "insetmode")]
		public EnumValue<InsetMarginValues> InsetMode
		{
			get
			{
				return (EnumValue<InsetMarginValues>)base.Attributes[29];
			}
			set
			{
				base.Attributes[29] = value;
			}
		}

		// Token: 0x17003ABA RID: 15034
		// (get) Token: 0x0600E336 RID: 58166 RVA: 0x002C2A5B File Offset: 0x002C0C5B
		// (set) Token: 0x0600E337 RID: 58167 RVA: 0x002BE3E5 File Offset: 0x002BC5E5
		[SchemaAttr(0, "editas")]
		public EnumValue<EditAsValues> EditAs
		{
			get
			{
				return (EnumValue<EditAsValues>)base.Attributes[30];
			}
			set
			{
				base.Attributes[30] = value;
			}
		}

		// Token: 0x17003ABB RID: 15035
		// (get) Token: 0x0600E338 RID: 58168 RVA: 0x002C2A6B File Offset: 0x002C0C6B
		// (set) Token: 0x0600E339 RID: 58169 RVA: 0x002C1410 File Offset: 0x002BF610
		[SchemaAttr(27, "tableproperties")]
		public StringValue TableProperties
		{
			get
			{
				return (StringValue)base.Attributes[31];
			}
			set
			{
				base.Attributes[31] = value;
			}
		}

		// Token: 0x17003ABC RID: 15036
		// (get) Token: 0x0600E33A RID: 58170 RVA: 0x002C2A7B File Offset: 0x002C0C7B
		// (set) Token: 0x0600E33B RID: 58171 RVA: 0x002C142C File Offset: 0x002BF62C
		[SchemaAttr(27, "tablelimits")]
		public StringValue TableLimits
		{
			get
			{
				return (StringValue)base.Attributes[32];
			}
			set
			{
				base.Attributes[32] = value;
			}
		}

		// Token: 0x0600E33C RID: 58172 RVA: 0x00293ECF File Offset: 0x002920CF
		public Group()
		{
		}

		// Token: 0x0600E33D RID: 58173 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Group(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600E33E RID: 58174 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Group(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600E33F RID: 58175 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Group(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0600E340 RID: 58176 RVA: 0x002C2A8C File Offset: 0x002C0C8C
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (26 == namespaceId && "group" == name)
			{
				return new Group();
			}
			if (26 == namespaceId && "shape" == name)
			{
				return new Shape();
			}
			if (26 == namespaceId && "shapetype" == name)
			{
				return new Shapetype();
			}
			if (26 == namespaceId && "arc" == name)
			{
				return new Arc();
			}
			if (26 == namespaceId && "curve" == name)
			{
				return new Curve();
			}
			if (26 == namespaceId && "image" == name)
			{
				return new ImageFile();
			}
			if (26 == namespaceId && "line" == name)
			{
				return new Line();
			}
			if (26 == namespaceId && "oval" == name)
			{
				return new Oval();
			}
			if (26 == namespaceId && "polyline" == name)
			{
				return new PolyLine();
			}
			if (26 == namespaceId && "rect" == name)
			{
				return new Rectangle();
			}
			if (26 == namespaceId && "roundrect" == name)
			{
				return new RoundRectangle();
			}
			if (27 == namespaceId && "diagram" == name)
			{
				return new Diagram();
			}
			if (27 == namespaceId && "lock" == name)
			{
				return new Lock();
			}
			if (27 == namespaceId && "clippath" == name)
			{
				return new ClipPath();
			}
			if (28 == namespaceId && "wrap" == name)
			{
				return new TextWrap();
			}
			if (28 == namespaceId && "anchorlock" == name)
			{
				return new AnchorLock();
			}
			if (29 == namespaceId && "ClientData" == name)
			{
				return new ClientData();
			}
			return null;
		}

		// Token: 0x0600E341 RID: 58177 RVA: 0x002C2C34 File Offset: 0x002C0E34
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
			if (namespaceId == 0 && "editas" == name)
			{
				return new EnumValue<EditAsValues>();
			}
			if (27 == namespaceId && "tableproperties" == name)
			{
				return new StringValue();
			}
			if (27 == namespaceId && "tablelimits" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0600E342 RID: 58178 RVA: 0x002C2F49 File Offset: 0x002C1149
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Group>(deep);
		}

		// Token: 0x04006EC7 RID: 28359
		private const string tagName = "group";

		// Token: 0x04006EC8 RID: 28360
		private const byte tagNsId = 26;

		// Token: 0x04006EC9 RID: 28361
		internal const int ElementTypeIdConst = 12517;

		// Token: 0x04006ECA RID: 28362
		private static string[] attributeTagNames = new string[]
		{
			"id", "style", "href", "target", "class", "title", "alt", "coordsize", "coordorigin", "wrapcoords",
			"print", "spid", "oned", "regroupid", "doubleclicknotify", "button", "userhidden", "bullet", "hr", "hrstd",
			"hrnoshade", "hrpct", "hralign", "allowincell", "allowoverlap", "userdrawn", "dgmlayout", "dgmnodekind", "dgmlayoutmru", "insetmode",
			"editas", "tableproperties", "tablelimits"
		};

		// Token: 0x04006ECB RID: 28363
		private static byte[] attributeNamespaceIds = new byte[]
		{
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 27, 27, 27, 27, 27, 27, 27, 27, 27,
			27, 27, 27, 27, 27, 27, 27, 27, 27, 27,
			0, 27, 27
		};
	}
}
