using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Office2010.Ink;

namespace DocumentFormat.OpenXml.EMMA
{
	// Token: 0x02003075 RID: 12405
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(Info))]
	[ChildElementInfo(typeof(DerivedFrom))]
	[ChildElementInfo(typeof(Lattice))]
	[ChildElementInfo(typeof(Literal))]
	[ChildElementInfo(typeof(ContextNode))]
	internal class Interpretation : OpenXmlCompositeElement
	{
		// Token: 0x1700968E RID: 38542
		// (get) Token: 0x0601AE02 RID: 110082 RVA: 0x00368A9C File Offset: 0x00366C9C
		public override string LocalName
		{
			get
			{
				return "interpretation";
			}
		}

		// Token: 0x1700968F RID: 38543
		// (get) Token: 0x0601AE03 RID: 110083 RVA: 0x0036884A File Offset: 0x00366A4A
		internal override byte NamespaceId
		{
			get
			{
				return 44;
			}
		}

		// Token: 0x17009690 RID: 38544
		// (get) Token: 0x0601AE04 RID: 110084 RVA: 0x00368AA3 File Offset: 0x00366CA3
		internal override int ElementTypeId
		{
			get
			{
				return 12674;
			}
		}

		// Token: 0x0601AE05 RID: 110085 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17009691 RID: 38545
		// (get) Token: 0x0601AE06 RID: 110086 RVA: 0x00368AAA File Offset: 0x00366CAA
		internal override string[] AttributeTagNames
		{
			get
			{
				return Interpretation.attributeTagNames;
			}
		}

		// Token: 0x17009692 RID: 38546
		// (get) Token: 0x0601AE07 RID: 110087 RVA: 0x00368AB1 File Offset: 0x00366CB1
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Interpretation.attributeNamespaceIds;
			}
		}

		// Token: 0x17009693 RID: 38547
		// (get) Token: 0x0601AE08 RID: 110088 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0601AE09 RID: 110089 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x17009694 RID: 38548
		// (get) Token: 0x0601AE0A RID: 110090 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x0601AE0B RID: 110091 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(44, "tokens")]
		public StringValue Tokens
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

		// Token: 0x17009695 RID: 38549
		// (get) Token: 0x0601AE0C RID: 110092 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x0601AE0D RID: 110093 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(44, "process")]
		public StringValue Process
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

		// Token: 0x17009696 RID: 38550
		// (get) Token: 0x0601AE0E RID: 110094 RVA: 0x002BDADA File Offset: 0x002BBCDA
		// (set) Token: 0x0601AE0F RID: 110095 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(44, "lang")]
		public StringValue Language
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

		// Token: 0x17009697 RID: 38551
		// (get) Token: 0x0601AE10 RID: 110096 RVA: 0x002BD4B9 File Offset: 0x002BB6B9
		// (set) Token: 0x0601AE11 RID: 110097 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(44, "signal")]
		public StringValue Signal
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

		// Token: 0x17009698 RID: 38552
		// (get) Token: 0x0601AE12 RID: 110098 RVA: 0x002BDE3A File Offset: 0x002BC03A
		// (set) Token: 0x0601AE13 RID: 110099 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(44, "signal-size")]
		public IntegerValue SignalSize
		{
			get
			{
				return (IntegerValue)base.Attributes[5];
			}
			set
			{
				base.Attributes[5] = value;
			}
		}

		// Token: 0x17009699 RID: 38553
		// (get) Token: 0x0601AE14 RID: 110100 RVA: 0x002BD4ED File Offset: 0x002BB6ED
		// (set) Token: 0x0601AE15 RID: 110101 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(44, "media-type")]
		public StringValue MediaType
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

		// Token: 0x1700969A RID: 38554
		// (get) Token: 0x0601AE16 RID: 110102 RVA: 0x002BEEE1 File Offset: 0x002BD0E1
		// (set) Token: 0x0601AE17 RID: 110103 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(44, "confidence")]
		public DecimalValue Confidence
		{
			get
			{
				return (DecimalValue)base.Attributes[7];
			}
			set
			{
				base.Attributes[7] = value;
			}
		}

		// Token: 0x1700969B RID: 38555
		// (get) Token: 0x0601AE18 RID: 110104 RVA: 0x002BDB16 File Offset: 0x002BBD16
		// (set) Token: 0x0601AE19 RID: 110105 RVA: 0x002BD530 File Offset: 0x002BB730
		[SchemaAttr(44, "source")]
		public StringValue Source
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

		// Token: 0x1700969C RID: 38556
		// (get) Token: 0x0601AE1A RID: 110106 RVA: 0x00368AB8 File Offset: 0x00366CB8
		// (set) Token: 0x0601AE1B RID: 110107 RVA: 0x002BD54B File Offset: 0x002BB74B
		[SchemaAttr(44, "start")]
		public UInt64Value Start
		{
			get
			{
				return (UInt64Value)base.Attributes[9];
			}
			set
			{
				base.Attributes[9] = value;
			}
		}

		// Token: 0x1700969D RID: 38557
		// (get) Token: 0x0601AE1C RID: 110108 RVA: 0x00368AC8 File Offset: 0x00366CC8
		// (set) Token: 0x0601AE1D RID: 110109 RVA: 0x002BDB45 File Offset: 0x002BBD45
		[SchemaAttr(44, "end")]
		public UInt64Value End
		{
			get
			{
				return (UInt64Value)base.Attributes[10];
			}
			set
			{
				base.Attributes[10] = value;
			}
		}

		// Token: 0x1700969E RID: 38558
		// (get) Token: 0x0601AE1E RID: 110110 RVA: 0x002BDB51 File Offset: 0x002BBD51
		// (set) Token: 0x0601AE1F RID: 110111 RVA: 0x002BDB61 File Offset: 0x002BBD61
		[SchemaAttr(44, "time-ref-uri")]
		public StringValue TimeReference
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

		// Token: 0x1700969F RID: 38559
		// (get) Token: 0x0601AE20 RID: 110112 RVA: 0x00368AD8 File Offset: 0x00366CD8
		// (set) Token: 0x0601AE21 RID: 110113 RVA: 0x002BDB7D File Offset: 0x002BBD7D
		[SchemaAttr(44, "time-ref-anchor-point")]
		public EnumValue<AnchorPointValues> TimeReferenceAnchorPoint
		{
			get
			{
				return (EnumValue<AnchorPointValues>)base.Attributes[12];
			}
			set
			{
				base.Attributes[12] = value;
			}
		}

		// Token: 0x170096A0 RID: 38560
		// (get) Token: 0x0601AE22 RID: 110114 RVA: 0x002C1380 File Offset: 0x002BF580
		// (set) Token: 0x0601AE23 RID: 110115 RVA: 0x002BE209 File Offset: 0x002BC409
		[SchemaAttr(44, "offset-to-start")]
		public IntegerValue OffsetToStart
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

		// Token: 0x170096A1 RID: 38561
		// (get) Token: 0x0601AE24 RID: 110116 RVA: 0x002CC6D3 File Offset: 0x002CA8D3
		// (set) Token: 0x0601AE25 RID: 110117 RVA: 0x002BE225 File Offset: 0x002BC425
		[SchemaAttr(44, "duration")]
		public IntegerValue Duration
		{
			get
			{
				return (IntegerValue)base.Attributes[14];
			}
			set
			{
				base.Attributes[14] = value;
			}
		}

		// Token: 0x170096A2 RID: 38562
		// (get) Token: 0x0601AE26 RID: 110118 RVA: 0x00368AE8 File Offset: 0x00366CE8
		// (set) Token: 0x0601AE27 RID: 110119 RVA: 0x002BE241 File Offset: 0x002BC441
		[SchemaAttr(44, "medium")]
		public ListValue<EnumValue<MediumValues>> Medium
		{
			get
			{
				return (ListValue<EnumValue<MediumValues>>)base.Attributes[15];
			}
			set
			{
				base.Attributes[15] = value;
			}
		}

		// Token: 0x170096A3 RID: 38563
		// (get) Token: 0x0601AE28 RID: 110120 RVA: 0x002C82B1 File Offset: 0x002C64B1
		// (set) Token: 0x0601AE29 RID: 110121 RVA: 0x002BE25D File Offset: 0x002BC45D
		[SchemaAttr(44, "mode")]
		public ListValue<StringValue> Mode
		{
			get
			{
				return (ListValue<StringValue>)base.Attributes[16];
			}
			set
			{
				base.Attributes[16] = value;
			}
		}

		// Token: 0x170096A4 RID: 38564
		// (get) Token: 0x0601AE2A RID: 110122 RVA: 0x002C02CC File Offset: 0x002BE4CC
		// (set) Token: 0x0601AE2B RID: 110123 RVA: 0x002BE279 File Offset: 0x002BC479
		[SchemaAttr(44, "function")]
		public StringValue Function
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

		// Token: 0x170096A5 RID: 38565
		// (get) Token: 0x0601AE2C RID: 110124 RVA: 0x002C8772 File Offset: 0x002C6972
		// (set) Token: 0x0601AE2D RID: 110125 RVA: 0x002BE295 File Offset: 0x002BC495
		[SchemaAttr(44, "verbal")]
		public BooleanValue Verbal
		{
			get
			{
				return (BooleanValue)base.Attributes[18];
			}
			set
			{
				base.Attributes[18] = value;
			}
		}

		// Token: 0x170096A6 RID: 38566
		// (get) Token: 0x0601AE2E RID: 110126 RVA: 0x00368AF8 File Offset: 0x00366CF8
		// (set) Token: 0x0601AE2F RID: 110127 RVA: 0x002BE2B1 File Offset: 0x002BC4B1
		[SchemaAttr(44, "cost")]
		public DecimalValue Cost
		{
			get
			{
				return (DecimalValue)base.Attributes[19];
			}
			set
			{
				base.Attributes[19] = value;
			}
		}

		// Token: 0x170096A7 RID: 38567
		// (get) Token: 0x0601AE30 RID: 110128 RVA: 0x002C8782 File Offset: 0x002C6982
		// (set) Token: 0x0601AE31 RID: 110129 RVA: 0x002BE2CD File Offset: 0x002BC4CD
		[SchemaAttr(44, "grammar-ref")]
		public StringValue GrammarRef
		{
			get
			{
				return (StringValue)base.Attributes[20];
			}
			set
			{
				base.Attributes[20] = value;
			}
		}

		// Token: 0x170096A8 RID: 38568
		// (get) Token: 0x0601AE32 RID: 110130 RVA: 0x002BE2D9 File Offset: 0x002BC4D9
		// (set) Token: 0x0601AE33 RID: 110131 RVA: 0x002BE2E9 File Offset: 0x002BC4E9
		[SchemaAttr(44, "endpoint-info-ref")]
		public StringValue EndpointInfoRef
		{
			get
			{
				return (StringValue)base.Attributes[21];
			}
			set
			{
				base.Attributes[21] = value;
			}
		}

		// Token: 0x170096A9 RID: 38569
		// (get) Token: 0x0601AE34 RID: 110132 RVA: 0x002BE2F5 File Offset: 0x002BC4F5
		// (set) Token: 0x0601AE35 RID: 110133 RVA: 0x002BE305 File Offset: 0x002BC505
		[SchemaAttr(44, "model-ref")]
		public StringValue ModelRef
		{
			get
			{
				return (StringValue)base.Attributes[22];
			}
			set
			{
				base.Attributes[22] = value;
			}
		}

		// Token: 0x170096AA RID: 38570
		// (get) Token: 0x0601AE36 RID: 110134 RVA: 0x002BEFAF File Offset: 0x002BD1AF
		// (set) Token: 0x0601AE37 RID: 110135 RVA: 0x002BE321 File Offset: 0x002BC521
		[SchemaAttr(44, "dialog-turn")]
		public StringValue DialogTurn
		{
			get
			{
				return (StringValue)base.Attributes[23];
			}
			set
			{
				base.Attributes[23] = value;
			}
		}

		// Token: 0x170096AB RID: 38571
		// (get) Token: 0x0601AE38 RID: 110136 RVA: 0x002C87A2 File Offset: 0x002C69A2
		// (set) Token: 0x0601AE39 RID: 110137 RVA: 0x002BE33D File Offset: 0x002BC53D
		[SchemaAttr(44, "no-input")]
		public BooleanValue NoInput
		{
			get
			{
				return (BooleanValue)base.Attributes[24];
			}
			set
			{
				base.Attributes[24] = value;
			}
		}

		// Token: 0x170096AC RID: 38572
		// (get) Token: 0x0601AE3A RID: 110138 RVA: 0x002CBE3C File Offset: 0x002CA03C
		// (set) Token: 0x0601AE3B RID: 110139 RVA: 0x002BE359 File Offset: 0x002BC559
		[SchemaAttr(44, "uninterpreted")]
		public BooleanValue Uninterpreted
		{
			get
			{
				return (BooleanValue)base.Attributes[25];
			}
			set
			{
				base.Attributes[25] = value;
			}
		}

		// Token: 0x0601AE3C RID: 110140 RVA: 0x00293ECF File Offset: 0x002920CF
		public Interpretation()
		{
		}

		// Token: 0x0601AE3D RID: 110141 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Interpretation(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601AE3E RID: 110142 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Interpretation(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601AE3F RID: 110143 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Interpretation(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601AE40 RID: 110144 RVA: 0x00368B08 File Offset: 0x00366D08
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (44 == namespaceId && "derived-from" == name)
			{
				return new DerivedFrom();
			}
			if (44 == namespaceId && "info" == name)
			{
				return new Info();
			}
			if (44 == namespaceId && "lattice" == name)
			{
				return new Lattice();
			}
			if (44 == namespaceId && "literal" == name)
			{
				return new Literal();
			}
			if (45 == namespaceId && "context" == name)
			{
				return new ContextNode();
			}
			return null;
		}

		// Token: 0x0601AE41 RID: 110145 RVA: 0x00368B90 File Offset: 0x00366D90
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "id" == name)
			{
				return new StringValue();
			}
			if (44 == namespaceId && "tokens" == name)
			{
				return new StringValue();
			}
			if (44 == namespaceId && "process" == name)
			{
				return new StringValue();
			}
			if (44 == namespaceId && "lang" == name)
			{
				return new StringValue();
			}
			if (44 == namespaceId && "signal" == name)
			{
				return new StringValue();
			}
			if (44 == namespaceId && "signal-size" == name)
			{
				return new IntegerValue();
			}
			if (44 == namespaceId && "media-type" == name)
			{
				return new StringValue();
			}
			if (44 == namespaceId && "confidence" == name)
			{
				return new DecimalValue();
			}
			if (44 == namespaceId && "source" == name)
			{
				return new StringValue();
			}
			if (44 == namespaceId && "start" == name)
			{
				return new UInt64Value();
			}
			if (44 == namespaceId && "end" == name)
			{
				return new UInt64Value();
			}
			if (44 == namespaceId && "time-ref-uri" == name)
			{
				return new StringValue();
			}
			if (44 == namespaceId && "time-ref-anchor-point" == name)
			{
				return new EnumValue<AnchorPointValues>();
			}
			if (44 == namespaceId && "offset-to-start" == name)
			{
				return new IntegerValue();
			}
			if (44 == namespaceId && "duration" == name)
			{
				return new IntegerValue();
			}
			if (44 == namespaceId && "medium" == name)
			{
				return new ListValue<EnumValue<MediumValues>>();
			}
			if (44 == namespaceId && "mode" == name)
			{
				return new ListValue<StringValue>();
			}
			if (44 == namespaceId && "function" == name)
			{
				return new StringValue();
			}
			if (44 == namespaceId && "verbal" == name)
			{
				return new BooleanValue();
			}
			if (44 == namespaceId && "cost" == name)
			{
				return new DecimalValue();
			}
			if (44 == namespaceId && "grammar-ref" == name)
			{
				return new StringValue();
			}
			if (44 == namespaceId && "endpoint-info-ref" == name)
			{
				return new StringValue();
			}
			if (44 == namespaceId && "model-ref" == name)
			{
				return new StringValue();
			}
			if (44 == namespaceId && "dialog-turn" == name)
			{
				return new StringValue();
			}
			if (44 == namespaceId && "no-input" == name)
			{
				return new BooleanValue();
			}
			if (44 == namespaceId && "uninterpreted" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601AE42 RID: 110146 RVA: 0x00368E13 File Offset: 0x00367013
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Interpretation>(deep);
		}

		// Token: 0x0400B21D RID: 45597
		private const string tagName = "interpretation";

		// Token: 0x0400B21E RID: 45598
		private const byte tagNsId = 44;

		// Token: 0x0400B21F RID: 45599
		internal const int ElementTypeIdConst = 12674;

		// Token: 0x0400B220 RID: 45600
		private static string[] attributeTagNames = new string[]
		{
			"id", "tokens", "process", "lang", "signal", "signal-size", "media-type", "confidence", "source", "start",
			"end", "time-ref-uri", "time-ref-anchor-point", "offset-to-start", "duration", "medium", "mode", "function", "verbal", "cost",
			"grammar-ref", "endpoint-info-ref", "model-ref", "dialog-turn", "no-input", "uninterpreted"
		};

		// Token: 0x0400B221 RID: 45601
		private static byte[] attributeNamespaceIds = new byte[]
		{
			0, 44, 44, 44, 44, 44, 44, 44, 44, 44,
			44, 44, 44, 44, 44, 44, 44, 44, 44, 44,
			44, 44, 44, 44, 44, 44
		};
	}
}
