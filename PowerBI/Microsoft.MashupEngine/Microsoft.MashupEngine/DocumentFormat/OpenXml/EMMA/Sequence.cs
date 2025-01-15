using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.EMMA
{
	// Token: 0x02003078 RID: 12408
	[ChildElementInfo(typeof(Interpretation))]
	[ChildElementInfo(typeof(Info))]
	[ChildElementInfo(typeof(OneOf))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(DerivedFrom))]
	[ChildElementInfo(typeof(Group))]
	[ChildElementInfo(typeof(Sequence))]
	internal class Sequence : OpenXmlCompositeElement
	{
		// Token: 0x170096E8 RID: 38632
		// (get) Token: 0x0601AEC2 RID: 110274 RVA: 0x00369805 File Offset: 0x00367A05
		public override string LocalName
		{
			get
			{
				return "sequence";
			}
		}

		// Token: 0x170096E9 RID: 38633
		// (get) Token: 0x0601AEC3 RID: 110275 RVA: 0x0036884A File Offset: 0x00366A4A
		internal override byte NamespaceId
		{
			get
			{
				return 44;
			}
		}

		// Token: 0x170096EA RID: 38634
		// (get) Token: 0x0601AEC4 RID: 110276 RVA: 0x0036980C File Offset: 0x00367A0C
		internal override int ElementTypeId
		{
			get
			{
				return 12677;
			}
		}

		// Token: 0x0601AEC5 RID: 110277 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170096EB RID: 38635
		// (get) Token: 0x0601AEC6 RID: 110278 RVA: 0x00369813 File Offset: 0x00367A13
		internal override string[] AttributeTagNames
		{
			get
			{
				return Sequence.attributeTagNames;
			}
		}

		// Token: 0x170096EC RID: 38636
		// (get) Token: 0x0601AEC7 RID: 110279 RVA: 0x0036981A File Offset: 0x00367A1A
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Sequence.attributeNamespaceIds;
			}
		}

		// Token: 0x170096ED RID: 38637
		// (get) Token: 0x0601AEC8 RID: 110280 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0601AEC9 RID: 110281 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x170096EE RID: 38638
		// (get) Token: 0x0601AECA RID: 110282 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x0601AECB RID: 110283 RVA: 0x002BD47A File Offset: 0x002BB67A
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

		// Token: 0x170096EF RID: 38639
		// (get) Token: 0x0601AECC RID: 110284 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x0601AECD RID: 110285 RVA: 0x002BD494 File Offset: 0x002BB694
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

		// Token: 0x170096F0 RID: 38640
		// (get) Token: 0x0601AECE RID: 110286 RVA: 0x002BDADA File Offset: 0x002BBCDA
		// (set) Token: 0x0601AECF RID: 110287 RVA: 0x002BD4AE File Offset: 0x002BB6AE
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

		// Token: 0x170096F1 RID: 38641
		// (get) Token: 0x0601AED0 RID: 110288 RVA: 0x002BD4B9 File Offset: 0x002BB6B9
		// (set) Token: 0x0601AED1 RID: 110289 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
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

		// Token: 0x170096F2 RID: 38642
		// (get) Token: 0x0601AED2 RID: 110290 RVA: 0x002BDE3A File Offset: 0x002BC03A
		// (set) Token: 0x0601AED3 RID: 110291 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
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

		// Token: 0x170096F3 RID: 38643
		// (get) Token: 0x0601AED4 RID: 110292 RVA: 0x002BD4ED File Offset: 0x002BB6ED
		// (set) Token: 0x0601AED5 RID: 110293 RVA: 0x002BD4FC File Offset: 0x002BB6FC
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

		// Token: 0x170096F4 RID: 38644
		// (get) Token: 0x0601AED6 RID: 110294 RVA: 0x002BEEE1 File Offset: 0x002BD0E1
		// (set) Token: 0x0601AED7 RID: 110295 RVA: 0x002BD516 File Offset: 0x002BB716
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

		// Token: 0x170096F5 RID: 38645
		// (get) Token: 0x0601AED8 RID: 110296 RVA: 0x002BDB16 File Offset: 0x002BBD16
		// (set) Token: 0x0601AED9 RID: 110297 RVA: 0x002BD530 File Offset: 0x002BB730
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

		// Token: 0x170096F6 RID: 38646
		// (get) Token: 0x0601AEDA RID: 110298 RVA: 0x00368AB8 File Offset: 0x00366CB8
		// (set) Token: 0x0601AEDB RID: 110299 RVA: 0x002BD54B File Offset: 0x002BB74B
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

		// Token: 0x170096F7 RID: 38647
		// (get) Token: 0x0601AEDC RID: 110300 RVA: 0x00368AC8 File Offset: 0x00366CC8
		// (set) Token: 0x0601AEDD RID: 110301 RVA: 0x002BDB45 File Offset: 0x002BBD45
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

		// Token: 0x170096F8 RID: 38648
		// (get) Token: 0x0601AEDE RID: 110302 RVA: 0x002BDB51 File Offset: 0x002BBD51
		// (set) Token: 0x0601AEDF RID: 110303 RVA: 0x002BDB61 File Offset: 0x002BBD61
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

		// Token: 0x170096F9 RID: 38649
		// (get) Token: 0x0601AEE0 RID: 110304 RVA: 0x00368AD8 File Offset: 0x00366CD8
		// (set) Token: 0x0601AEE1 RID: 110305 RVA: 0x002BDB7D File Offset: 0x002BBD7D
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

		// Token: 0x170096FA RID: 38650
		// (get) Token: 0x0601AEE2 RID: 110306 RVA: 0x002C1380 File Offset: 0x002BF580
		// (set) Token: 0x0601AEE3 RID: 110307 RVA: 0x002BE209 File Offset: 0x002BC409
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

		// Token: 0x170096FB RID: 38651
		// (get) Token: 0x0601AEE4 RID: 110308 RVA: 0x002CC6D3 File Offset: 0x002CA8D3
		// (set) Token: 0x0601AEE5 RID: 110309 RVA: 0x002BE225 File Offset: 0x002BC425
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

		// Token: 0x170096FC RID: 38652
		// (get) Token: 0x0601AEE6 RID: 110310 RVA: 0x00368AE8 File Offset: 0x00366CE8
		// (set) Token: 0x0601AEE7 RID: 110311 RVA: 0x002BE241 File Offset: 0x002BC441
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

		// Token: 0x170096FD RID: 38653
		// (get) Token: 0x0601AEE8 RID: 110312 RVA: 0x002C82B1 File Offset: 0x002C64B1
		// (set) Token: 0x0601AEE9 RID: 110313 RVA: 0x002BE25D File Offset: 0x002BC45D
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

		// Token: 0x170096FE RID: 38654
		// (get) Token: 0x0601AEEA RID: 110314 RVA: 0x002C02CC File Offset: 0x002BE4CC
		// (set) Token: 0x0601AEEB RID: 110315 RVA: 0x002BE279 File Offset: 0x002BC479
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

		// Token: 0x170096FF RID: 38655
		// (get) Token: 0x0601AEEC RID: 110316 RVA: 0x002C8772 File Offset: 0x002C6972
		// (set) Token: 0x0601AEED RID: 110317 RVA: 0x002BE295 File Offset: 0x002BC495
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

		// Token: 0x17009700 RID: 38656
		// (get) Token: 0x0601AEEE RID: 110318 RVA: 0x00368AF8 File Offset: 0x00366CF8
		// (set) Token: 0x0601AEEF RID: 110319 RVA: 0x002BE2B1 File Offset: 0x002BC4B1
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

		// Token: 0x17009701 RID: 38657
		// (get) Token: 0x0601AEF0 RID: 110320 RVA: 0x002C8782 File Offset: 0x002C6982
		// (set) Token: 0x0601AEF1 RID: 110321 RVA: 0x002BE2CD File Offset: 0x002BC4CD
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

		// Token: 0x17009702 RID: 38658
		// (get) Token: 0x0601AEF2 RID: 110322 RVA: 0x002BE2D9 File Offset: 0x002BC4D9
		// (set) Token: 0x0601AEF3 RID: 110323 RVA: 0x002BE2E9 File Offset: 0x002BC4E9
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

		// Token: 0x17009703 RID: 38659
		// (get) Token: 0x0601AEF4 RID: 110324 RVA: 0x002BE2F5 File Offset: 0x002BC4F5
		// (set) Token: 0x0601AEF5 RID: 110325 RVA: 0x002BE305 File Offset: 0x002BC505
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

		// Token: 0x17009704 RID: 38660
		// (get) Token: 0x0601AEF6 RID: 110326 RVA: 0x002BEFAF File Offset: 0x002BD1AF
		// (set) Token: 0x0601AEF7 RID: 110327 RVA: 0x002BE321 File Offset: 0x002BC521
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

		// Token: 0x0601AEF8 RID: 110328 RVA: 0x00293ECF File Offset: 0x002920CF
		public Sequence()
		{
		}

		// Token: 0x0601AEF9 RID: 110329 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Sequence(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601AEFA RID: 110330 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Sequence(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601AEFB RID: 110331 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Sequence(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601AEFC RID: 110332 RVA: 0x00369824 File Offset: 0x00367A24
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

		// Token: 0x0601AEFD RID: 110333 RVA: 0x003698C4 File Offset: 0x00367AC4
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
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601AEFE RID: 110334 RVA: 0x00369B17 File Offset: 0x00367D17
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Sequence>(deep);
		}

		// Token: 0x0400B22C RID: 45612
		private const string tagName = "sequence";

		// Token: 0x0400B22D RID: 45613
		private const byte tagNsId = 44;

		// Token: 0x0400B22E RID: 45614
		internal const int ElementTypeIdConst = 12677;

		// Token: 0x0400B22F RID: 45615
		private static string[] attributeTagNames = new string[]
		{
			"id", "tokens", "process", "lang", "signal", "signal-size", "media-type", "confidence", "source", "start",
			"end", "time-ref-uri", "time-ref-anchor-point", "offset-to-start", "duration", "medium", "mode", "function", "verbal", "cost",
			"grammar-ref", "endpoint-info-ref", "model-ref", "dialog-turn"
		};

		// Token: 0x0400B230 RID: 45616
		private static byte[] attributeNamespaceIds = new byte[]
		{
			0, 44, 44, 44, 44, 44, 44, 44, 44, 44,
			44, 44, 44, 44, 44, 44, 44, 44, 44, 44,
			44, 44, 44, 44
		};
	}
}
