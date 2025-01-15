using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.EMMA
{
	// Token: 0x02003076 RID: 12406
	[ChildElementInfo(typeof(Sequence))]
	[ChildElementInfo(typeof(Group))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(OneOf))]
	[ChildElementInfo(typeof(DerivedFrom))]
	[ChildElementInfo(typeof(Info))]
	[ChildElementInfo(typeof(Interpretation))]
	internal class OneOf : OpenXmlCompositeElement
	{
		// Token: 0x170096AD RID: 38573
		// (get) Token: 0x0601AE44 RID: 110148 RVA: 0x00368F2F File Offset: 0x0036712F
		public override string LocalName
		{
			get
			{
				return "one-of";
			}
		}

		// Token: 0x170096AE RID: 38574
		// (get) Token: 0x0601AE45 RID: 110149 RVA: 0x0036884A File Offset: 0x00366A4A
		internal override byte NamespaceId
		{
			get
			{
				return 44;
			}
		}

		// Token: 0x170096AF RID: 38575
		// (get) Token: 0x0601AE46 RID: 110150 RVA: 0x00368F36 File Offset: 0x00367136
		internal override int ElementTypeId
		{
			get
			{
				return 12675;
			}
		}

		// Token: 0x0601AE47 RID: 110151 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170096B0 RID: 38576
		// (get) Token: 0x0601AE48 RID: 110152 RVA: 0x00368F3D File Offset: 0x0036713D
		internal override string[] AttributeTagNames
		{
			get
			{
				return OneOf.attributeTagNames;
			}
		}

		// Token: 0x170096B1 RID: 38577
		// (get) Token: 0x0601AE49 RID: 110153 RVA: 0x00368F44 File Offset: 0x00367144
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return OneOf.attributeNamespaceIds;
			}
		}

		// Token: 0x170096B2 RID: 38578
		// (get) Token: 0x0601AE4A RID: 110154 RVA: 0x00368F4B File Offset: 0x0036714B
		// (set) Token: 0x0601AE4B RID: 110155 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "disjunction-type")]
		public EnumValue<DisjunctionTypeValues> DisjunctionType
		{
			get
			{
				return (EnumValue<DisjunctionTypeValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x170096B3 RID: 38579
		// (get) Token: 0x0601AE4C RID: 110156 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x0601AE4D RID: 110157 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "id")]
		public StringValue Id
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

		// Token: 0x170096B4 RID: 38580
		// (get) Token: 0x0601AE4E RID: 110158 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x0601AE4F RID: 110159 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(44, "tokens")]
		public StringValue Tokens
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

		// Token: 0x170096B5 RID: 38581
		// (get) Token: 0x0601AE50 RID: 110160 RVA: 0x002BDADA File Offset: 0x002BBCDA
		// (set) Token: 0x0601AE51 RID: 110161 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(44, "process")]
		public StringValue Process
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

		// Token: 0x170096B6 RID: 38582
		// (get) Token: 0x0601AE52 RID: 110162 RVA: 0x002BD4B9 File Offset: 0x002BB6B9
		// (set) Token: 0x0601AE53 RID: 110163 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(44, "lang")]
		public StringValue Language
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

		// Token: 0x170096B7 RID: 38583
		// (get) Token: 0x0601AE54 RID: 110164 RVA: 0x002BE081 File Offset: 0x002BC281
		// (set) Token: 0x0601AE55 RID: 110165 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(44, "signal")]
		public StringValue Signal
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

		// Token: 0x170096B8 RID: 38584
		// (get) Token: 0x0601AE56 RID: 110166 RVA: 0x002BDE49 File Offset: 0x002BC049
		// (set) Token: 0x0601AE57 RID: 110167 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(44, "signal-size")]
		public IntegerValue SignalSize
		{
			get
			{
				return (IntegerValue)base.Attributes[6];
			}
			set
			{
				base.Attributes[6] = value;
			}
		}

		// Token: 0x170096B9 RID: 38585
		// (get) Token: 0x0601AE58 RID: 110168 RVA: 0x002BDB07 File Offset: 0x002BBD07
		// (set) Token: 0x0601AE59 RID: 110169 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(44, "media-type")]
		public StringValue MediaType
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

		// Token: 0x170096BA RID: 38586
		// (get) Token: 0x0601AE5A RID: 110170 RVA: 0x00368F5A File Offset: 0x0036715A
		// (set) Token: 0x0601AE5B RID: 110171 RVA: 0x002BD530 File Offset: 0x002BB730
		[SchemaAttr(44, "confidence")]
		public DecimalValue Confidence
		{
			get
			{
				return (DecimalValue)base.Attributes[8];
			}
			set
			{
				base.Attributes[8] = value;
			}
		}

		// Token: 0x170096BB RID: 38587
		// (get) Token: 0x0601AE5C RID: 110172 RVA: 0x002BDB25 File Offset: 0x002BBD25
		// (set) Token: 0x0601AE5D RID: 110173 RVA: 0x002BD54B File Offset: 0x002BB74B
		[SchemaAttr(44, "source")]
		public StringValue Source
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

		// Token: 0x170096BC RID: 38588
		// (get) Token: 0x0601AE5E RID: 110174 RVA: 0x00368AC8 File Offset: 0x00366CC8
		// (set) Token: 0x0601AE5F RID: 110175 RVA: 0x002BDB45 File Offset: 0x002BBD45
		[SchemaAttr(44, "start")]
		public UInt64Value Start
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

		// Token: 0x170096BD RID: 38589
		// (get) Token: 0x0601AE60 RID: 110176 RVA: 0x00368F69 File Offset: 0x00367169
		// (set) Token: 0x0601AE61 RID: 110177 RVA: 0x002BDB61 File Offset: 0x002BBD61
		[SchemaAttr(44, "end")]
		public UInt64Value End
		{
			get
			{
				return (UInt64Value)base.Attributes[11];
			}
			set
			{
				base.Attributes[11] = value;
			}
		}

		// Token: 0x170096BE RID: 38590
		// (get) Token: 0x0601AE62 RID: 110178 RVA: 0x002BDB6D File Offset: 0x002BBD6D
		// (set) Token: 0x0601AE63 RID: 110179 RVA: 0x002BDB7D File Offset: 0x002BBD7D
		[SchemaAttr(44, "time-ref-uri")]
		public StringValue TimeReference
		{
			get
			{
				return (StringValue)base.Attributes[12];
			}
			set
			{
				base.Attributes[12] = value;
			}
		}

		// Token: 0x170096BF RID: 38591
		// (get) Token: 0x0601AE64 RID: 110180 RVA: 0x00368F79 File Offset: 0x00367179
		// (set) Token: 0x0601AE65 RID: 110181 RVA: 0x002BE209 File Offset: 0x002BC409
		[SchemaAttr(44, "time-ref-anchor-point")]
		public EnumValue<AnchorPointValues> TimeReferenceAnchorPoint
		{
			get
			{
				return (EnumValue<AnchorPointValues>)base.Attributes[13];
			}
			set
			{
				base.Attributes[13] = value;
			}
		}

		// Token: 0x170096C0 RID: 38592
		// (get) Token: 0x0601AE66 RID: 110182 RVA: 0x002CC6D3 File Offset: 0x002CA8D3
		// (set) Token: 0x0601AE67 RID: 110183 RVA: 0x002BE225 File Offset: 0x002BC425
		[SchemaAttr(44, "offset-to-start")]
		public IntegerValue OffsetToStart
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

		// Token: 0x170096C1 RID: 38593
		// (get) Token: 0x0601AE68 RID: 110184 RVA: 0x002D8867 File Offset: 0x002D6A67
		// (set) Token: 0x0601AE69 RID: 110185 RVA: 0x002BE241 File Offset: 0x002BC441
		[SchemaAttr(44, "duration")]
		public IntegerValue Duration
		{
			get
			{
				return (IntegerValue)base.Attributes[15];
			}
			set
			{
				base.Attributes[15] = value;
			}
		}

		// Token: 0x170096C2 RID: 38594
		// (get) Token: 0x0601AE6A RID: 110186 RVA: 0x00368F89 File Offset: 0x00367189
		// (set) Token: 0x0601AE6B RID: 110187 RVA: 0x002BE25D File Offset: 0x002BC45D
		[SchemaAttr(44, "medium")]
		public ListValue<EnumValue<MediumValues>> Medium
		{
			get
			{
				return (ListValue<EnumValue<MediumValues>>)base.Attributes[16];
			}
			set
			{
				base.Attributes[16] = value;
			}
		}

		// Token: 0x170096C3 RID: 38595
		// (get) Token: 0x0601AE6C RID: 110188 RVA: 0x00368F99 File Offset: 0x00367199
		// (set) Token: 0x0601AE6D RID: 110189 RVA: 0x002BE279 File Offset: 0x002BC479
		[SchemaAttr(44, "mode")]
		public ListValue<StringValue> Mode
		{
			get
			{
				return (ListValue<StringValue>)base.Attributes[17];
			}
			set
			{
				base.Attributes[17] = value;
			}
		}

		// Token: 0x170096C4 RID: 38596
		// (get) Token: 0x0601AE6E RID: 110190 RVA: 0x002BE285 File Offset: 0x002BC485
		// (set) Token: 0x0601AE6F RID: 110191 RVA: 0x002BE295 File Offset: 0x002BC495
		[SchemaAttr(44, "function")]
		public StringValue Function
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

		// Token: 0x170096C5 RID: 38597
		// (get) Token: 0x0601AE70 RID: 110192 RVA: 0x002D6080 File Offset: 0x002D4280
		// (set) Token: 0x0601AE71 RID: 110193 RVA: 0x002BE2B1 File Offset: 0x002BC4B1
		[SchemaAttr(44, "verbal")]
		public BooleanValue Verbal
		{
			get
			{
				return (BooleanValue)base.Attributes[19];
			}
			set
			{
				base.Attributes[19] = value;
			}
		}

		// Token: 0x170096C6 RID: 38598
		// (get) Token: 0x0601AE72 RID: 110194 RVA: 0x00368FA9 File Offset: 0x003671A9
		// (set) Token: 0x0601AE73 RID: 110195 RVA: 0x002BE2CD File Offset: 0x002BC4CD
		[SchemaAttr(44, "cost")]
		public DecimalValue Cost
		{
			get
			{
				return (DecimalValue)base.Attributes[20];
			}
			set
			{
				base.Attributes[20] = value;
			}
		}

		// Token: 0x170096C7 RID: 38599
		// (get) Token: 0x0601AE74 RID: 110196 RVA: 0x002BE2D9 File Offset: 0x002BC4D9
		// (set) Token: 0x0601AE75 RID: 110197 RVA: 0x002BE2E9 File Offset: 0x002BC4E9
		[SchemaAttr(44, "grammar-ref")]
		public StringValue GrammarRef
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

		// Token: 0x170096C8 RID: 38600
		// (get) Token: 0x0601AE76 RID: 110198 RVA: 0x002BE2F5 File Offset: 0x002BC4F5
		// (set) Token: 0x0601AE77 RID: 110199 RVA: 0x002BE305 File Offset: 0x002BC505
		[SchemaAttr(44, "endpoint-info-ref")]
		public StringValue EndpointInfoRef
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

		// Token: 0x170096C9 RID: 38601
		// (get) Token: 0x0601AE78 RID: 110200 RVA: 0x002BEFAF File Offset: 0x002BD1AF
		// (set) Token: 0x0601AE79 RID: 110201 RVA: 0x002BE321 File Offset: 0x002BC521
		[SchemaAttr(44, "model-ref")]
		public StringValue ModelRef
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

		// Token: 0x170096CA RID: 38602
		// (get) Token: 0x0601AE7A RID: 110202 RVA: 0x002BE32D File Offset: 0x002BC52D
		// (set) Token: 0x0601AE7B RID: 110203 RVA: 0x002BE33D File Offset: 0x002BC53D
		[SchemaAttr(44, "dialog-turn")]
		public StringValue DialogTurn
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

		// Token: 0x0601AE7C RID: 110204 RVA: 0x00293ECF File Offset: 0x002920CF
		public OneOf()
		{
		}

		// Token: 0x0601AE7D RID: 110205 RVA: 0x00293ED7 File Offset: 0x002920D7
		public OneOf(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601AE7E RID: 110206 RVA: 0x00293EE0 File Offset: 0x002920E0
		public OneOf(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601AE7F RID: 110207 RVA: 0x00293EE9 File Offset: 0x002920E9
		public OneOf(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601AE80 RID: 110208 RVA: 0x00368FBC File Offset: 0x003671BC
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
			if (44 == namespaceId && "interpretation" == name)
			{
				return new Interpretation();
			}
			if (44 == namespaceId && "one-of" == name)
			{
				return new OneOf();
			}
			if (44 == namespaceId && "group" == name)
			{
				return new Group();
			}
			if (44 == namespaceId && "sequence" == name)
			{
				return new Sequence();
			}
			return null;
		}

		// Token: 0x0601AE81 RID: 110209 RVA: 0x0036905C File Offset: 0x0036725C
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "disjunction-type" == name)
			{
				return new EnumValue<DisjunctionTypeValues>();
			}
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
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601AE82 RID: 110210 RVA: 0x003692C5 File Offset: 0x003674C5
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<OneOf>(deep);
		}

		// Token: 0x0400B222 RID: 45602
		private const string tagName = "one-of";

		// Token: 0x0400B223 RID: 45603
		private const byte tagNsId = 44;

		// Token: 0x0400B224 RID: 45604
		internal const int ElementTypeIdConst = 12675;

		// Token: 0x0400B225 RID: 45605
		private static string[] attributeTagNames = new string[]
		{
			"disjunction-type", "id", "tokens", "process", "lang", "signal", "signal-size", "media-type", "confidence", "source",
			"start", "end", "time-ref-uri", "time-ref-anchor-point", "offset-to-start", "duration", "medium", "mode", "function", "verbal",
			"cost", "grammar-ref", "endpoint-info-ref", "model-ref", "dialog-turn"
		};

		// Token: 0x0400B226 RID: 45606
		private static byte[] attributeNamespaceIds = new byte[]
		{
			0, 0, 44, 44, 44, 44, 44, 44, 44, 44,
			44, 44, 44, 44, 44, 44, 44, 44, 44, 44,
			44, 44, 44, 44, 44
		};
	}
}
