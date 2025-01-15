using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Vml.Office;
using DocumentFormat.OpenXml.Vml.Presentation;
using DocumentFormat.OpenXml.Vml.Spreadsheet;
using DocumentFormat.OpenXml.Vml.Wordprocessing;

namespace DocumentFormat.OpenXml.Vml
{
	// Token: 0x0200224F RID: 8783
	[ChildElementInfo(typeof(TextPath))]
	[ChildElementInfo(typeof(Path))]
	[ChildElementInfo(typeof(ShapeHandles))]
	[ChildElementInfo(typeof(Fill))]
	[ChildElementInfo(typeof(Stroke))]
	[ChildElementInfo(typeof(Shadow))]
	[ChildElementInfo(typeof(TextBox))]
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
	internal class Arc : OpenXmlCompositeElement
	{
		// Token: 0x17003ACD RID: 15053
		// (get) Token: 0x0600E365 RID: 58213 RVA: 0x002C3277 File Offset: 0x002C1477
		public override string LocalName
		{
			get
			{
				return "arc";
			}
		}

		// Token: 0x17003ACE RID: 15054
		// (get) Token: 0x0600E366 RID: 58214 RVA: 0x00243C87 File Offset: 0x00241E87
		internal override byte NamespaceId
		{
			get
			{
				return 26;
			}
		}

		// Token: 0x17003ACF RID: 15055
		// (get) Token: 0x0600E367 RID: 58215 RVA: 0x002C327E File Offset: 0x002C147E
		internal override int ElementTypeId
		{
			get
			{
				return 12519;
			}
		}

		// Token: 0x0600E368 RID: 58216 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17003AD0 RID: 15056
		// (get) Token: 0x0600E369 RID: 58217 RVA: 0x002C3285 File Offset: 0x002C1485
		internal override string[] AttributeTagNames
		{
			get
			{
				return Arc.attributeTagNames;
			}
		}

		// Token: 0x17003AD1 RID: 15057
		// (get) Token: 0x0600E36A RID: 58218 RVA: 0x002C328C File Offset: 0x002C148C
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Arc.attributeNamespaceIds;
			}
		}

		// Token: 0x17003AD2 RID: 15058
		// (get) Token: 0x0600E36B RID: 58219 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0600E36C RID: 58220 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(27, "spid")]
		public StringValue OptionalString
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

		// Token: 0x17003AD3 RID: 15059
		// (get) Token: 0x0600E36D RID: 58221 RVA: 0x002BDACB File Offset: 0x002BBCCB
		// (set) Token: 0x0600E36E RID: 58222 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(27, "oned")]
		public TrueFalseValue Oned
		{
			get
			{
				return (TrueFalseValue)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x17003AD4 RID: 15060
		// (get) Token: 0x0600E36F RID: 58223 RVA: 0x002C3293 File Offset: 0x002C1493
		// (set) Token: 0x0600E370 RID: 58224 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(27, "regroupid")]
		public IntegerValue RegroupId
		{
			get
			{
				return (IntegerValue)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x17003AD5 RID: 15061
		// (get) Token: 0x0600E371 RID: 58225 RVA: 0x002BD49F File Offset: 0x002BB69F
		// (set) Token: 0x0600E372 RID: 58226 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(27, "doubleclicknotify")]
		public TrueFalseValue DoubleClickNotify
		{
			get
			{
				return (TrueFalseValue)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x17003AD6 RID: 15062
		// (get) Token: 0x0600E373 RID: 58227 RVA: 0x002BDAE9 File Offset: 0x002BBCE9
		// (set) Token: 0x0600E374 RID: 58228 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(27, "button")]
		public TrueFalseValue Button
		{
			get
			{
				return (TrueFalseValue)base.Attributes[4];
			}
			set
			{
				base.Attributes[4] = value;
			}
		}

		// Token: 0x17003AD7 RID: 15063
		// (get) Token: 0x0600E375 RID: 58229 RVA: 0x002BD4D3 File Offset: 0x002BB6D3
		// (set) Token: 0x0600E376 RID: 58230 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(27, "userhidden")]
		public TrueFalseValue UserHidden
		{
			get
			{
				return (TrueFalseValue)base.Attributes[5];
			}
			set
			{
				base.Attributes[5] = value;
			}
		}

		// Token: 0x17003AD8 RID: 15064
		// (get) Token: 0x0600E377 RID: 58231 RVA: 0x002BDAF8 File Offset: 0x002BBCF8
		// (set) Token: 0x0600E378 RID: 58232 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(27, "bullet")]
		public TrueFalseValue Bullet
		{
			get
			{
				return (TrueFalseValue)base.Attributes[6];
			}
			set
			{
				base.Attributes[6] = value;
			}
		}

		// Token: 0x17003AD9 RID: 15065
		// (get) Token: 0x0600E379 RID: 58233 RVA: 0x002BD507 File Offset: 0x002BB707
		// (set) Token: 0x0600E37A RID: 58234 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(27, "hr")]
		public TrueFalseValue Horizontal
		{
			get
			{
				return (TrueFalseValue)base.Attributes[7];
			}
			set
			{
				base.Attributes[7] = value;
			}
		}

		// Token: 0x17003ADA RID: 15066
		// (get) Token: 0x0600E37B RID: 58235 RVA: 0x002BD521 File Offset: 0x002BB721
		// (set) Token: 0x0600E37C RID: 58236 RVA: 0x002BD530 File Offset: 0x002BB730
		[SchemaAttr(27, "hrstd")]
		public TrueFalseValue HorizontalStandard
		{
			get
			{
				return (TrueFalseValue)base.Attributes[8];
			}
			set
			{
				base.Attributes[8] = value;
			}
		}

		// Token: 0x17003ADB RID: 15067
		// (get) Token: 0x0600E37D RID: 58237 RVA: 0x002BEA5B File Offset: 0x002BCC5B
		// (set) Token: 0x0600E37E RID: 58238 RVA: 0x002BD54B File Offset: 0x002BB74B
		[SchemaAttr(27, "hrnoshade")]
		public TrueFalseValue HorizontalNoShade
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

		// Token: 0x17003ADC RID: 15068
		// (get) Token: 0x0600E37F RID: 58239 RVA: 0x002C32A2 File Offset: 0x002C14A2
		// (set) Token: 0x0600E380 RID: 58240 RVA: 0x002BDB45 File Offset: 0x002BBD45
		[SchemaAttr(27, "hrpct")]
		public SingleValue HorizontalPercentage
		{
			get
			{
				return (SingleValue)base.Attributes[10];
			}
			set
			{
				base.Attributes[10] = value;
			}
		}

		// Token: 0x17003ADD RID: 15069
		// (get) Token: 0x0600E381 RID: 58241 RVA: 0x002C32B2 File Offset: 0x002C14B2
		// (set) Token: 0x0600E382 RID: 58242 RVA: 0x002BDB61 File Offset: 0x002BBD61
		[SchemaAttr(27, "hralign")]
		public EnumValue<HorizontalRuleAlignmentValues> HorizontalAlignment
		{
			get
			{
				return (EnumValue<HorizontalRuleAlignmentValues>)base.Attributes[11];
			}
			set
			{
				base.Attributes[11] = value;
			}
		}

		// Token: 0x17003ADE RID: 15070
		// (get) Token: 0x0600E383 RID: 58243 RVA: 0x002BE1E9 File Offset: 0x002BC3E9
		// (set) Token: 0x0600E384 RID: 58244 RVA: 0x002BDB7D File Offset: 0x002BBD7D
		[SchemaAttr(27, "allowincell")]
		public TrueFalseValue AllowInCell
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

		// Token: 0x17003ADF RID: 15071
		// (get) Token: 0x0600E385 RID: 58245 RVA: 0x002BE1F9 File Offset: 0x002BC3F9
		// (set) Token: 0x0600E386 RID: 58246 RVA: 0x002BE209 File Offset: 0x002BC409
		[SchemaAttr(27, "allowoverlap")]
		public TrueFalseValue AllowOverlap
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

		// Token: 0x17003AE0 RID: 15072
		// (get) Token: 0x0600E387 RID: 58247 RVA: 0x002BFFE2 File Offset: 0x002BE1E2
		// (set) Token: 0x0600E388 RID: 58248 RVA: 0x002BE225 File Offset: 0x002BC425
		[SchemaAttr(27, "userdrawn")]
		public TrueFalseValue UserDrawn
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

		// Token: 0x17003AE1 RID: 15073
		// (get) Token: 0x0600E389 RID: 58249 RVA: 0x002BE231 File Offset: 0x002BC431
		// (set) Token: 0x0600E38A RID: 58250 RVA: 0x002BE241 File Offset: 0x002BC441
		[SchemaAttr(27, "bordertopcolor")]
		public StringValue BorderTopColor
		{
			get
			{
				return (StringValue)base.Attributes[15];
			}
			set
			{
				base.Attributes[15] = value;
			}
		}

		// Token: 0x17003AE2 RID: 15074
		// (get) Token: 0x0600E38B RID: 58251 RVA: 0x002BE24D File Offset: 0x002BC44D
		// (set) Token: 0x0600E38C RID: 58252 RVA: 0x002BE25D File Offset: 0x002BC45D
		[SchemaAttr(27, "borderleftcolor")]
		public StringValue BorderLeftColor
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

		// Token: 0x17003AE3 RID: 15075
		// (get) Token: 0x0600E38D RID: 58253 RVA: 0x002C02CC File Offset: 0x002BE4CC
		// (set) Token: 0x0600E38E RID: 58254 RVA: 0x002BE279 File Offset: 0x002BC479
		[SchemaAttr(27, "borderbottomcolor")]
		public StringValue BorderBottomColor
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

		// Token: 0x17003AE4 RID: 15076
		// (get) Token: 0x0600E38F RID: 58255 RVA: 0x002BE285 File Offset: 0x002BC485
		// (set) Token: 0x0600E390 RID: 58256 RVA: 0x002BE295 File Offset: 0x002BC495
		[SchemaAttr(27, "borderrightcolor")]
		public StringValue BorderRightColor
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

		// Token: 0x17003AE5 RID: 15077
		// (get) Token: 0x0600E391 RID: 58257 RVA: 0x002C32C2 File Offset: 0x002C14C2
		// (set) Token: 0x0600E392 RID: 58258 RVA: 0x002BE2B1 File Offset: 0x002BC4B1
		[SchemaAttr(27, "dgmlayout")]
		public IntegerValue DiagramLayout
		{
			get
			{
				return (IntegerValue)base.Attributes[19];
			}
			set
			{
				base.Attributes[19] = value;
			}
		}

		// Token: 0x17003AE6 RID: 15078
		// (get) Token: 0x0600E393 RID: 58259 RVA: 0x002C32D2 File Offset: 0x002C14D2
		// (set) Token: 0x0600E394 RID: 58260 RVA: 0x002BE2CD File Offset: 0x002BC4CD
		[SchemaAttr(27, "dgmnodekind")]
		public IntegerValue DiagramNodeKind
		{
			get
			{
				return (IntegerValue)base.Attributes[20];
			}
			set
			{
				base.Attributes[20] = value;
			}
		}

		// Token: 0x17003AE7 RID: 15079
		// (get) Token: 0x0600E395 RID: 58261 RVA: 0x002C32E2 File Offset: 0x002C14E2
		// (set) Token: 0x0600E396 RID: 58262 RVA: 0x002BE2E9 File Offset: 0x002BC4E9
		[SchemaAttr(27, "dgmlayoutmru")]
		public IntegerValue DiagramLayoutMostRecentUsed
		{
			get
			{
				return (IntegerValue)base.Attributes[21];
			}
			set
			{
				base.Attributes[21] = value;
			}
		}

		// Token: 0x17003AE8 RID: 15080
		// (get) Token: 0x0600E397 RID: 58263 RVA: 0x002C32F2 File Offset: 0x002C14F2
		// (set) Token: 0x0600E398 RID: 58264 RVA: 0x002BE305 File Offset: 0x002BC505
		[SchemaAttr(27, "insetmode")]
		public EnumValue<InsetMarginValues> InsetMode
		{
			get
			{
				return (EnumValue<InsetMarginValues>)base.Attributes[22];
			}
			set
			{
				base.Attributes[22] = value;
			}
		}

		// Token: 0x17003AE9 RID: 15081
		// (get) Token: 0x0600E399 RID: 58265 RVA: 0x002BE311 File Offset: 0x002BC511
		// (set) Token: 0x0600E39A RID: 58266 RVA: 0x002BE321 File Offset: 0x002BC521
		[SchemaAttr(0, "filled")]
		public TrueFalseValue Filled
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

		// Token: 0x17003AEA RID: 15082
		// (get) Token: 0x0600E39B RID: 58267 RVA: 0x002BE32D File Offset: 0x002BC52D
		// (set) Token: 0x0600E39C RID: 58268 RVA: 0x002BE33D File Offset: 0x002BC53D
		[SchemaAttr(0, "fillcolor")]
		public StringValue FillColor
		{
			get
			{
				return (StringValue)base.Attributes[24];
			}
			set
			{
				base.Attributes[24] = value;
			}
		}

		// Token: 0x17003AEB RID: 15083
		// (get) Token: 0x0600E39D RID: 58269 RVA: 0x002C0793 File Offset: 0x002BE993
		// (set) Token: 0x0600E39E RID: 58270 RVA: 0x002BE359 File Offset: 0x002BC559
		[SchemaAttr(0, "stroked")]
		public TrueFalseValue Stroked
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

		// Token: 0x17003AEC RID: 15084
		// (get) Token: 0x0600E39F RID: 58271 RVA: 0x002BE365 File Offset: 0x002BC565
		// (set) Token: 0x0600E3A0 RID: 58272 RVA: 0x002BE375 File Offset: 0x002BC575
		[SchemaAttr(0, "strokecolor")]
		public StringValue StrokeColor
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

		// Token: 0x17003AED RID: 15085
		// (get) Token: 0x0600E3A1 RID: 58273 RVA: 0x002C13E0 File Offset: 0x002BF5E0
		// (set) Token: 0x0600E3A2 RID: 58274 RVA: 0x002BE391 File Offset: 0x002BC591
		[SchemaAttr(0, "strokeweight")]
		public StringValue StrokeWeight
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

		// Token: 0x17003AEE RID: 15086
		// (get) Token: 0x0600E3A3 RID: 58275 RVA: 0x002C3302 File Offset: 0x002C1502
		// (set) Token: 0x0600E3A4 RID: 58276 RVA: 0x002BE3AD File Offset: 0x002BC5AD
		[SchemaAttr(0, "insetpen")]
		public TrueFalseValue InsetPen
		{
			get
			{
				return (TrueFalseValue)base.Attributes[28];
			}
			set
			{
				base.Attributes[28] = value;
			}
		}

		// Token: 0x17003AEF RID: 15087
		// (get) Token: 0x0600E3A5 RID: 58277 RVA: 0x002C3312 File Offset: 0x002C1512
		// (set) Token: 0x0600E3A6 RID: 58278 RVA: 0x002BE3C9 File Offset: 0x002BC5C9
		[SchemaAttr(27, "spt")]
		public Int32Value OptionalNumber
		{
			get
			{
				return (Int32Value)base.Attributes[29];
			}
			set
			{
				base.Attributes[29] = value;
			}
		}

		// Token: 0x17003AF0 RID: 15088
		// (get) Token: 0x0600E3A7 RID: 58279 RVA: 0x002C3322 File Offset: 0x002C1522
		// (set) Token: 0x0600E3A8 RID: 58280 RVA: 0x002BE3E5 File Offset: 0x002BC5E5
		[SchemaAttr(27, "connectortype")]
		public EnumValue<ConnectorValues> ConnectorType
		{
			get
			{
				return (EnumValue<ConnectorValues>)base.Attributes[30];
			}
			set
			{
				base.Attributes[30] = value;
			}
		}

		// Token: 0x17003AF1 RID: 15089
		// (get) Token: 0x0600E3A9 RID: 58281 RVA: 0x002C3332 File Offset: 0x002C1532
		// (set) Token: 0x0600E3AA RID: 58282 RVA: 0x002C1410 File Offset: 0x002BF610
		[SchemaAttr(27, "bwmode")]
		public EnumValue<BlackAndWhiteModeValues> BlackWhiteMode
		{
			get
			{
				return (EnumValue<BlackAndWhiteModeValues>)base.Attributes[31];
			}
			set
			{
				base.Attributes[31] = value;
			}
		}

		// Token: 0x17003AF2 RID: 15090
		// (get) Token: 0x0600E3AB RID: 58283 RVA: 0x002C3342 File Offset: 0x002C1542
		// (set) Token: 0x0600E3AC RID: 58284 RVA: 0x002C142C File Offset: 0x002BF62C
		[SchemaAttr(27, "bwpure")]
		public EnumValue<BlackAndWhiteModeValues> PureBlackWhiteMode
		{
			get
			{
				return (EnumValue<BlackAndWhiteModeValues>)base.Attributes[32];
			}
			set
			{
				base.Attributes[32] = value;
			}
		}

		// Token: 0x17003AF3 RID: 15091
		// (get) Token: 0x0600E3AD RID: 58285 RVA: 0x002C3352 File Offset: 0x002C1552
		// (set) Token: 0x0600E3AE RID: 58286 RVA: 0x002C1448 File Offset: 0x002BF648
		[SchemaAttr(27, "bwnormal")]
		public EnumValue<BlackAndWhiteModeValues> NormalBlackWhiteMode
		{
			get
			{
				return (EnumValue<BlackAndWhiteModeValues>)base.Attributes[33];
			}
			set
			{
				base.Attributes[33] = value;
			}
		}

		// Token: 0x17003AF4 RID: 15092
		// (get) Token: 0x0600E3AF RID: 58287 RVA: 0x002C1454 File Offset: 0x002BF654
		// (set) Token: 0x0600E3B0 RID: 58288 RVA: 0x002C1464 File Offset: 0x002BF664
		[SchemaAttr(27, "forcedash")]
		public TrueFalseValue ForceDash
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

		// Token: 0x17003AF5 RID: 15093
		// (get) Token: 0x0600E3B1 RID: 58289 RVA: 0x002C3362 File Offset: 0x002C1562
		// (set) Token: 0x0600E3B2 RID: 58290 RVA: 0x002C1480 File Offset: 0x002BF680
		[SchemaAttr(27, "oleicon")]
		public TrueFalseValue OleIcon
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

		// Token: 0x17003AF6 RID: 15094
		// (get) Token: 0x0600E3B3 RID: 58291 RVA: 0x002C3372 File Offset: 0x002C1572
		// (set) Token: 0x0600E3B4 RID: 58292 RVA: 0x002C149C File Offset: 0x002BF69C
		[SchemaAttr(27, "ole")]
		public TrueFalseBlankValue Ole
		{
			get
			{
				return (TrueFalseBlankValue)base.Attributes[36];
			}
			set
			{
				base.Attributes[36] = value;
			}
		}

		// Token: 0x17003AF7 RID: 15095
		// (get) Token: 0x0600E3B5 RID: 58293 RVA: 0x002C3382 File Offset: 0x002C1582
		// (set) Token: 0x0600E3B6 RID: 58294 RVA: 0x002C14B8 File Offset: 0x002BF6B8
		[SchemaAttr(27, "preferrelative")]
		public TrueFalseValue PreferRelative
		{
			get
			{
				return (TrueFalseValue)base.Attributes[37];
			}
			set
			{
				base.Attributes[37] = value;
			}
		}

		// Token: 0x17003AF8 RID: 15096
		// (get) Token: 0x0600E3B7 RID: 58295 RVA: 0x002C3392 File Offset: 0x002C1592
		// (set) Token: 0x0600E3B8 RID: 58296 RVA: 0x002C14D4 File Offset: 0x002BF6D4
		[SchemaAttr(27, "cliptowrap")]
		public TrueFalseValue ClipToWrap
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

		// Token: 0x17003AF9 RID: 15097
		// (get) Token: 0x0600E3B9 RID: 58297 RVA: 0x002C14E0 File Offset: 0x002BF6E0
		// (set) Token: 0x0600E3BA RID: 58298 RVA: 0x002C14F0 File Offset: 0x002BF6F0
		[SchemaAttr(27, "clip")]
		public TrueFalseValue Clip
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

		// Token: 0x17003AFA RID: 15098
		// (get) Token: 0x0600E3BB RID: 58299 RVA: 0x002C33A2 File Offset: 0x002C15A2
		// (set) Token: 0x0600E3BC RID: 58300 RVA: 0x002C150C File Offset: 0x002BF70C
		[SchemaAttr(0, "id")]
		public StringValue Id
		{
			get
			{
				return (StringValue)base.Attributes[40];
			}
			set
			{
				base.Attributes[40] = value;
			}
		}

		// Token: 0x17003AFB RID: 15099
		// (get) Token: 0x0600E3BD RID: 58301 RVA: 0x002C33B2 File Offset: 0x002C15B2
		// (set) Token: 0x0600E3BE RID: 58302 RVA: 0x002C1528 File Offset: 0x002BF728
		[SchemaAttr(0, "style")]
		public StringValue Style
		{
			get
			{
				return (StringValue)base.Attributes[41];
			}
			set
			{
				base.Attributes[41] = value;
			}
		}

		// Token: 0x17003AFC RID: 15100
		// (get) Token: 0x0600E3BF RID: 58303 RVA: 0x002C33C2 File Offset: 0x002C15C2
		// (set) Token: 0x0600E3C0 RID: 58304 RVA: 0x002C1544 File Offset: 0x002BF744
		[SchemaAttr(0, "href")]
		public StringValue Href
		{
			get
			{
				return (StringValue)base.Attributes[42];
			}
			set
			{
				base.Attributes[42] = value;
			}
		}

		// Token: 0x17003AFD RID: 15101
		// (get) Token: 0x0600E3C1 RID: 58305 RVA: 0x002C33D2 File Offset: 0x002C15D2
		// (set) Token: 0x0600E3C2 RID: 58306 RVA: 0x002C1560 File Offset: 0x002BF760
		[SchemaAttr(0, "target")]
		public StringValue Target
		{
			get
			{
				return (StringValue)base.Attributes[43];
			}
			set
			{
				base.Attributes[43] = value;
			}
		}

		// Token: 0x17003AFE RID: 15102
		// (get) Token: 0x0600E3C3 RID: 58307 RVA: 0x002C33E2 File Offset: 0x002C15E2
		// (set) Token: 0x0600E3C4 RID: 58308 RVA: 0x002C157C File Offset: 0x002BF77C
		[SchemaAttr(0, "title")]
		public StringValue Title
		{
			get
			{
				return (StringValue)base.Attributes[44];
			}
			set
			{
				base.Attributes[44] = value;
			}
		}

		// Token: 0x17003AFF RID: 15103
		// (get) Token: 0x0600E3C5 RID: 58309 RVA: 0x002C33F2 File Offset: 0x002C15F2
		// (set) Token: 0x0600E3C6 RID: 58310 RVA: 0x002C1598 File Offset: 0x002BF798
		[SchemaAttr(0, "alt")]
		public StringValue Alternate
		{
			get
			{
				return (StringValue)base.Attributes[45];
			}
			set
			{
				base.Attributes[45] = value;
			}
		}

		// Token: 0x17003B00 RID: 15104
		// (get) Token: 0x0600E3C7 RID: 58311 RVA: 0x002C3402 File Offset: 0x002C1602
		// (set) Token: 0x0600E3C8 RID: 58312 RVA: 0x002C15B4 File Offset: 0x002BF7B4
		[SchemaAttr(0, "coordsize")]
		public StringValue CoordinateSize
		{
			get
			{
				return (StringValue)base.Attributes[46];
			}
			set
			{
				base.Attributes[46] = value;
			}
		}

		// Token: 0x17003B01 RID: 15105
		// (get) Token: 0x0600E3C9 RID: 58313 RVA: 0x002C3412 File Offset: 0x002C1612
		// (set) Token: 0x0600E3CA RID: 58314 RVA: 0x002C15D0 File Offset: 0x002BF7D0
		[SchemaAttr(0, "coordorigin")]
		public StringValue CoordinateOrigin
		{
			get
			{
				return (StringValue)base.Attributes[47];
			}
			set
			{
				base.Attributes[47] = value;
			}
		}

		// Token: 0x17003B02 RID: 15106
		// (get) Token: 0x0600E3CB RID: 58315 RVA: 0x002C3422 File Offset: 0x002C1622
		// (set) Token: 0x0600E3CC RID: 58316 RVA: 0x002C15EC File Offset: 0x002BF7EC
		[SchemaAttr(0, "wrapcoords")]
		public StringValue Wrapcoords
		{
			get
			{
				return (StringValue)base.Attributes[48];
			}
			set
			{
				base.Attributes[48] = value;
			}
		}

		// Token: 0x17003B03 RID: 15107
		// (get) Token: 0x0600E3CD RID: 58317 RVA: 0x002C15F8 File Offset: 0x002BF7F8
		// (set) Token: 0x0600E3CE RID: 58318 RVA: 0x002C1608 File Offset: 0x002BF808
		[SchemaAttr(0, "print")]
		public TrueFalseValue Print
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

		// Token: 0x17003B04 RID: 15108
		// (get) Token: 0x0600E3CF RID: 58319 RVA: 0x002C3432 File Offset: 0x002C1632
		// (set) Token: 0x0600E3D0 RID: 58320 RVA: 0x002C1624 File Offset: 0x002BF824
		[SchemaAttr(0, "startangle")]
		public DecimalValue StartAngle
		{
			get
			{
				return (DecimalValue)base.Attributes[50];
			}
			set
			{
				base.Attributes[50] = value;
			}
		}

		// Token: 0x17003B05 RID: 15109
		// (get) Token: 0x0600E3D1 RID: 58321 RVA: 0x002C3442 File Offset: 0x002C1642
		// (set) Token: 0x0600E3D2 RID: 58322 RVA: 0x002C1640 File Offset: 0x002BF840
		[SchemaAttr(0, "endangle")]
		public DecimalValue EndAngle
		{
			get
			{
				return (DecimalValue)base.Attributes[51];
			}
			set
			{
				base.Attributes[51] = value;
			}
		}

		// Token: 0x0600E3D3 RID: 58323 RVA: 0x00293ECF File Offset: 0x002920CF
		public Arc()
		{
		}

		// Token: 0x0600E3D4 RID: 58324 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Arc(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600E3D5 RID: 58325 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Arc(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600E3D6 RID: 58326 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Arc(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0600E3D7 RID: 58327 RVA: 0x002C3454 File Offset: 0x002C1654
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

		// Token: 0x0600E3D8 RID: 58328 RVA: 0x002C368C File Offset: 0x002C188C
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
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
			if (namespaceId == 0 && "startangle" == name)
			{
				return new DecimalValue();
			}
			if (namespaceId == 0 && "endangle" == name)
			{
				return new DecimalValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0600E3D9 RID: 58329 RVA: 0x002C3B5D File Offset: 0x002C1D5D
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Arc>(deep);
		}

		// Token: 0x04006ED3 RID: 28371
		private const string tagName = "arc";

		// Token: 0x04006ED4 RID: 28372
		private const byte tagNsId = 26;

		// Token: 0x04006ED5 RID: 28373
		internal const int ElementTypeIdConst = 12519;

		// Token: 0x04006ED6 RID: 28374
		private static string[] attributeTagNames = new string[]
		{
			"spid", "oned", "regroupid", "doubleclicknotify", "button", "userhidden", "bullet", "hr", "hrstd", "hrnoshade",
			"hrpct", "hralign", "allowincell", "allowoverlap", "userdrawn", "bordertopcolor", "borderleftcolor", "borderbottomcolor", "borderrightcolor", "dgmlayout",
			"dgmnodekind", "dgmlayoutmru", "insetmode", "filled", "fillcolor", "stroked", "strokecolor", "strokeweight", "insetpen", "spt",
			"connectortype", "bwmode", "bwpure", "bwnormal", "forcedash", "oleicon", "ole", "preferrelative", "cliptowrap", "clip",
			"id", "style", "href", "target", "title", "alt", "coordsize", "coordorigin", "wrapcoords", "print",
			"startangle", "endangle"
		};

		// Token: 0x04006ED7 RID: 28375
		private static byte[] attributeNamespaceIds = new byte[]
		{
			27, 27, 27, 27, 27, 27, 27, 27, 27, 27,
			27, 27, 27, 27, 27, 27, 27, 27, 27, 27,
			27, 27, 27, 0, 0, 0, 0, 0, 0, 27,
			27, 27, 27, 27, 27, 27, 27, 27, 27, 27,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0
		};
	}
}
