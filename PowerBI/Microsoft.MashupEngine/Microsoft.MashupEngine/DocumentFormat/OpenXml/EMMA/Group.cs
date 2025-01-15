using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.EMMA
{
	// Token: 0x02003077 RID: 12407
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(Info))]
	[ChildElementInfo(typeof(DerivedFrom))]
	[ChildElementInfo(typeof(GroupInfo))]
	[ChildElementInfo(typeof(OneOf))]
	[ChildElementInfo(typeof(Sequence))]
	[ChildElementInfo(typeof(Interpretation))]
	[ChildElementInfo(typeof(Group))]
	internal class Group : OpenXmlCompositeElement
	{
		// Token: 0x170096CB RID: 38603
		// (get) Token: 0x0601AE84 RID: 110212 RVA: 0x002C29FF File Offset: 0x002C0BFF
		public override string LocalName
		{
			get
			{
				return "group";
			}
		}

		// Token: 0x170096CC RID: 38604
		// (get) Token: 0x0601AE85 RID: 110213 RVA: 0x0036884A File Offset: 0x00366A4A
		internal override byte NamespaceId
		{
			get
			{
				return 44;
			}
		}

		// Token: 0x170096CD RID: 38605
		// (get) Token: 0x0601AE86 RID: 110214 RVA: 0x003693DA File Offset: 0x003675DA
		internal override int ElementTypeId
		{
			get
			{
				return 12676;
			}
		}

		// Token: 0x0601AE87 RID: 110215 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170096CE RID: 38606
		// (get) Token: 0x0601AE88 RID: 110216 RVA: 0x003693E1 File Offset: 0x003675E1
		internal override string[] AttributeTagNames
		{
			get
			{
				return Group.attributeTagNames;
			}
		}

		// Token: 0x170096CF RID: 38607
		// (get) Token: 0x0601AE89 RID: 110217 RVA: 0x003693E8 File Offset: 0x003675E8
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Group.attributeNamespaceIds;
			}
		}

		// Token: 0x170096D0 RID: 38608
		// (get) Token: 0x0601AE8A RID: 110218 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0601AE8B RID: 110219 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x170096D1 RID: 38609
		// (get) Token: 0x0601AE8C RID: 110220 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x0601AE8D RID: 110221 RVA: 0x002BD47A File Offset: 0x002BB67A
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

		// Token: 0x170096D2 RID: 38610
		// (get) Token: 0x0601AE8E RID: 110222 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x0601AE8F RID: 110223 RVA: 0x002BD494 File Offset: 0x002BB694
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

		// Token: 0x170096D3 RID: 38611
		// (get) Token: 0x0601AE90 RID: 110224 RVA: 0x002BDADA File Offset: 0x002BBCDA
		// (set) Token: 0x0601AE91 RID: 110225 RVA: 0x002BD4AE File Offset: 0x002BB6AE
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

		// Token: 0x170096D4 RID: 38612
		// (get) Token: 0x0601AE92 RID: 110226 RVA: 0x002BD4B9 File Offset: 0x002BB6B9
		// (set) Token: 0x0601AE93 RID: 110227 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
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

		// Token: 0x170096D5 RID: 38613
		// (get) Token: 0x0601AE94 RID: 110228 RVA: 0x002BDE3A File Offset: 0x002BC03A
		// (set) Token: 0x0601AE95 RID: 110229 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
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

		// Token: 0x170096D6 RID: 38614
		// (get) Token: 0x0601AE96 RID: 110230 RVA: 0x002BD4ED File Offset: 0x002BB6ED
		// (set) Token: 0x0601AE97 RID: 110231 RVA: 0x002BD4FC File Offset: 0x002BB6FC
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

		// Token: 0x170096D7 RID: 38615
		// (get) Token: 0x0601AE98 RID: 110232 RVA: 0x002BEEE1 File Offset: 0x002BD0E1
		// (set) Token: 0x0601AE99 RID: 110233 RVA: 0x002BD516 File Offset: 0x002BB716
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

		// Token: 0x170096D8 RID: 38616
		// (get) Token: 0x0601AE9A RID: 110234 RVA: 0x002BDB16 File Offset: 0x002BBD16
		// (set) Token: 0x0601AE9B RID: 110235 RVA: 0x002BD530 File Offset: 0x002BB730
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

		// Token: 0x170096D9 RID: 38617
		// (get) Token: 0x0601AE9C RID: 110236 RVA: 0x00368AB8 File Offset: 0x00366CB8
		// (set) Token: 0x0601AE9D RID: 110237 RVA: 0x002BD54B File Offset: 0x002BB74B
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

		// Token: 0x170096DA RID: 38618
		// (get) Token: 0x0601AE9E RID: 110238 RVA: 0x00368AC8 File Offset: 0x00366CC8
		// (set) Token: 0x0601AE9F RID: 110239 RVA: 0x002BDB45 File Offset: 0x002BBD45
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

		// Token: 0x170096DB RID: 38619
		// (get) Token: 0x0601AEA0 RID: 110240 RVA: 0x002BDB51 File Offset: 0x002BBD51
		// (set) Token: 0x0601AEA1 RID: 110241 RVA: 0x002BDB61 File Offset: 0x002BBD61
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

		// Token: 0x170096DC RID: 38620
		// (get) Token: 0x0601AEA2 RID: 110242 RVA: 0x00368AD8 File Offset: 0x00366CD8
		// (set) Token: 0x0601AEA3 RID: 110243 RVA: 0x002BDB7D File Offset: 0x002BBD7D
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

		// Token: 0x170096DD RID: 38621
		// (get) Token: 0x0601AEA4 RID: 110244 RVA: 0x002C1380 File Offset: 0x002BF580
		// (set) Token: 0x0601AEA5 RID: 110245 RVA: 0x002BE209 File Offset: 0x002BC409
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

		// Token: 0x170096DE RID: 38622
		// (get) Token: 0x0601AEA6 RID: 110246 RVA: 0x002CC6D3 File Offset: 0x002CA8D3
		// (set) Token: 0x0601AEA7 RID: 110247 RVA: 0x002BE225 File Offset: 0x002BC425
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

		// Token: 0x170096DF RID: 38623
		// (get) Token: 0x0601AEA8 RID: 110248 RVA: 0x00368AE8 File Offset: 0x00366CE8
		// (set) Token: 0x0601AEA9 RID: 110249 RVA: 0x002BE241 File Offset: 0x002BC441
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

		// Token: 0x170096E0 RID: 38624
		// (get) Token: 0x0601AEAA RID: 110250 RVA: 0x002C82B1 File Offset: 0x002C64B1
		// (set) Token: 0x0601AEAB RID: 110251 RVA: 0x002BE25D File Offset: 0x002BC45D
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

		// Token: 0x170096E1 RID: 38625
		// (get) Token: 0x0601AEAC RID: 110252 RVA: 0x002C02CC File Offset: 0x002BE4CC
		// (set) Token: 0x0601AEAD RID: 110253 RVA: 0x002BE279 File Offset: 0x002BC479
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

		// Token: 0x170096E2 RID: 38626
		// (get) Token: 0x0601AEAE RID: 110254 RVA: 0x002C8772 File Offset: 0x002C6972
		// (set) Token: 0x0601AEAF RID: 110255 RVA: 0x002BE295 File Offset: 0x002BC495
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

		// Token: 0x170096E3 RID: 38627
		// (get) Token: 0x0601AEB0 RID: 110256 RVA: 0x00368AF8 File Offset: 0x00366CF8
		// (set) Token: 0x0601AEB1 RID: 110257 RVA: 0x002BE2B1 File Offset: 0x002BC4B1
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

		// Token: 0x170096E4 RID: 38628
		// (get) Token: 0x0601AEB2 RID: 110258 RVA: 0x002C8782 File Offset: 0x002C6982
		// (set) Token: 0x0601AEB3 RID: 110259 RVA: 0x002BE2CD File Offset: 0x002BC4CD
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

		// Token: 0x170096E5 RID: 38629
		// (get) Token: 0x0601AEB4 RID: 110260 RVA: 0x002BE2D9 File Offset: 0x002BC4D9
		// (set) Token: 0x0601AEB5 RID: 110261 RVA: 0x002BE2E9 File Offset: 0x002BC4E9
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

		// Token: 0x170096E6 RID: 38630
		// (get) Token: 0x0601AEB6 RID: 110262 RVA: 0x002BE2F5 File Offset: 0x002BC4F5
		// (set) Token: 0x0601AEB7 RID: 110263 RVA: 0x002BE305 File Offset: 0x002BC505
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

		// Token: 0x170096E7 RID: 38631
		// (get) Token: 0x0601AEB8 RID: 110264 RVA: 0x002BEFAF File Offset: 0x002BD1AF
		// (set) Token: 0x0601AEB9 RID: 110265 RVA: 0x002BE321 File Offset: 0x002BC521
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

		// Token: 0x0601AEBA RID: 110266 RVA: 0x00293ECF File Offset: 0x002920CF
		public Group()
		{
		}

		// Token: 0x0601AEBB RID: 110267 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Group(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601AEBC RID: 110268 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Group(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601AEBD RID: 110269 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Group(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601AEBE RID: 110270 RVA: 0x003693F0 File Offset: 0x003675F0
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (44 == namespaceId && "derived-from" == name)
			{
				return new DerivedFrom();
			}
			if (44 == namespaceId && "group-info" == name)
			{
				return new GroupInfo();
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

		// Token: 0x0601AEBF RID: 110271 RVA: 0x003694A8 File Offset: 0x003676A8
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

		// Token: 0x0601AEC0 RID: 110272 RVA: 0x003696FB File Offset: 0x003678FB
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Group>(deep);
		}

		// Token: 0x0400B227 RID: 45607
		private const string tagName = "group";

		// Token: 0x0400B228 RID: 45608
		private const byte tagNsId = 44;

		// Token: 0x0400B229 RID: 45609
		internal const int ElementTypeIdConst = 12676;

		// Token: 0x0400B22A RID: 45610
		private static string[] attributeTagNames = new string[]
		{
			"id", "tokens", "process", "lang", "signal", "signal-size", "media-type", "confidence", "source", "start",
			"end", "time-ref-uri", "time-ref-anchor-point", "offset-to-start", "duration", "medium", "mode", "function", "verbal", "cost",
			"grammar-ref", "endpoint-info-ref", "model-ref", "dialog-turn"
		};

		// Token: 0x0400B22B RID: 45611
		private static byte[] attributeNamespaceIds = new byte[]
		{
			0, 44, 44, 44, 44, 44, 44, 44, 44, 44,
			44, 44, 44, 44, 44, 44, 44, 44, 44, 44,
			44, 44, 44, 44
		};
	}
}
