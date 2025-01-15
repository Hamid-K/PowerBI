using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.EMMA
{
	// Token: 0x02003080 RID: 12416
	[ChildElementInfo(typeof(Info))]
	[GeneratedCode("DomGen", "2.0")]
	internal class Arc : OpenXmlCompositeElement
	{
		// Token: 0x1700973A RID: 38714
		// (get) Token: 0x0601AF82 RID: 110466 RVA: 0x002C3277 File Offset: 0x002C1477
		public override string LocalName
		{
			get
			{
				return "arc";
			}
		}

		// Token: 0x1700973B RID: 38715
		// (get) Token: 0x0601AF83 RID: 110467 RVA: 0x0036884A File Offset: 0x00366A4A
		internal override byte NamespaceId
		{
			get
			{
				return 44;
			}
		}

		// Token: 0x1700973C RID: 38716
		// (get) Token: 0x0601AF84 RID: 110468 RVA: 0x0036A161 File Offset: 0x00368361
		internal override int ElementTypeId
		{
			get
			{
				return 12685;
			}
		}

		// Token: 0x0601AF85 RID: 110469 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x1700973D RID: 38717
		// (get) Token: 0x0601AF86 RID: 110470 RVA: 0x0036A168 File Offset: 0x00368368
		internal override string[] AttributeTagNames
		{
			get
			{
				return Arc.attributeTagNames;
			}
		}

		// Token: 0x1700973E RID: 38718
		// (get) Token: 0x0601AF87 RID: 110471 RVA: 0x0036A16F File Offset: 0x0036836F
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Arc.attributeNamespaceIds;
			}
		}

		// Token: 0x1700973F RID: 38719
		// (get) Token: 0x0601AF88 RID: 110472 RVA: 0x002EC050 File Offset: 0x002EA250
		// (set) Token: 0x0601AF89 RID: 110473 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "from")]
		public IntegerValue From
		{
			get
			{
				return (IntegerValue)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17009740 RID: 38720
		// (get) Token: 0x0601AF8A RID: 110474 RVA: 0x002BD46B File Offset: 0x002BB66B
		// (set) Token: 0x0601AF8B RID: 110475 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "to")]
		public IntegerValue To
		{
			get
			{
				return (IntegerValue)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x17009741 RID: 38721
		// (get) Token: 0x0601AF8C RID: 110476 RVA: 0x0036A176 File Offset: 0x00368376
		// (set) Token: 0x0601AF8D RID: 110477 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(44, "start")]
		public UInt64Value Start
		{
			get
			{
				return (UInt64Value)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x17009742 RID: 38722
		// (get) Token: 0x0601AF8E RID: 110478 RVA: 0x0036A185 File Offset: 0x00368385
		// (set) Token: 0x0601AF8F RID: 110479 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(44, "end")]
		public UInt64Value End
		{
			get
			{
				return (UInt64Value)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x17009743 RID: 38723
		// (get) Token: 0x0601AF90 RID: 110480 RVA: 0x002C92DB File Offset: 0x002C74DB
		// (set) Token: 0x0601AF91 RID: 110481 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(44, "offset-to-start")]
		public IntegerValue OffsetToStart
		{
			get
			{
				return (IntegerValue)base.Attributes[4];
			}
			set
			{
				base.Attributes[4] = value;
			}
		}

		// Token: 0x17009744 RID: 38724
		// (get) Token: 0x0601AF92 RID: 110482 RVA: 0x002BDE3A File Offset: 0x002BC03A
		// (set) Token: 0x0601AF93 RID: 110483 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(44, "duration")]
		public IntegerValue Duration
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

		// Token: 0x17009745 RID: 38725
		// (get) Token: 0x0601AF94 RID: 110484 RVA: 0x0036A194 File Offset: 0x00368394
		// (set) Token: 0x0601AF95 RID: 110485 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(44, "confidence")]
		public DecimalValue Confidence
		{
			get
			{
				return (DecimalValue)base.Attributes[6];
			}
			set
			{
				base.Attributes[6] = value;
			}
		}

		// Token: 0x17009746 RID: 38726
		// (get) Token: 0x0601AF96 RID: 110486 RVA: 0x002BEEE1 File Offset: 0x002BD0E1
		// (set) Token: 0x0601AF97 RID: 110487 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(44, "cost")]
		public DecimalValue Cost
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

		// Token: 0x17009747 RID: 38727
		// (get) Token: 0x0601AF98 RID: 110488 RVA: 0x002BDB16 File Offset: 0x002BBD16
		// (set) Token: 0x0601AF99 RID: 110489 RVA: 0x002BD530 File Offset: 0x002BB730
		[SchemaAttr(44, "lang")]
		public StringValue Language
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

		// Token: 0x17009748 RID: 38728
		// (get) Token: 0x0601AF9A RID: 110490 RVA: 0x00369E8A File Offset: 0x0036808A
		// (set) Token: 0x0601AF9B RID: 110491 RVA: 0x002BD54B File Offset: 0x002BB74B
		[SchemaAttr(44, "medium")]
		public ListValue<EnumValue<MediumValues>> Medium
		{
			get
			{
				return (ListValue<EnumValue<MediumValues>>)base.Attributes[9];
			}
			set
			{
				base.Attributes[9] = value;
			}
		}

		// Token: 0x17009749 RID: 38729
		// (get) Token: 0x0601AF9C RID: 110492 RVA: 0x00369E9A File Offset: 0x0036809A
		// (set) Token: 0x0601AF9D RID: 110493 RVA: 0x002BDB45 File Offset: 0x002BBD45
		[SchemaAttr(44, "mode")]
		public ListValue<StringValue> Mode
		{
			get
			{
				return (ListValue<StringValue>)base.Attributes[10];
			}
			set
			{
				base.Attributes[10] = value;
			}
		}

		// Token: 0x1700974A RID: 38730
		// (get) Token: 0x0601AF9E RID: 110494 RVA: 0x002BDB51 File Offset: 0x002BBD51
		// (set) Token: 0x0601AF9F RID: 110495 RVA: 0x002BDB61 File Offset: 0x002BBD61
		[SchemaAttr(44, "source")]
		public StringValue Source
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

		// Token: 0x0601AFA0 RID: 110496 RVA: 0x00293ECF File Offset: 0x002920CF
		public Arc()
		{
		}

		// Token: 0x0601AFA1 RID: 110497 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Arc(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601AFA2 RID: 110498 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Arc(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601AFA3 RID: 110499 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Arc(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601AFA4 RID: 110500 RVA: 0x0036A096 File Offset: 0x00368296
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (44 == namespaceId && "info" == name)
			{
				return new Info();
			}
			return null;
		}

		// Token: 0x0601AFA5 RID: 110501 RVA: 0x0036A1A4 File Offset: 0x003683A4
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "from" == name)
			{
				return new IntegerValue();
			}
			if (namespaceId == 0 && "to" == name)
			{
				return new IntegerValue();
			}
			if (44 == namespaceId && "start" == name)
			{
				return new UInt64Value();
			}
			if (44 == namespaceId && "end" == name)
			{
				return new UInt64Value();
			}
			if (44 == namespaceId && "offset-to-start" == name)
			{
				return new IntegerValue();
			}
			if (44 == namespaceId && "duration" == name)
			{
				return new IntegerValue();
			}
			if (44 == namespaceId && "confidence" == name)
			{
				return new DecimalValue();
			}
			if (44 == namespaceId && "cost" == name)
			{
				return new DecimalValue();
			}
			if (44 == namespaceId && "lang" == name)
			{
				return new StringValue();
			}
			if (44 == namespaceId && "medium" == name)
			{
				return new ListValue<EnumValue<MediumValues>>();
			}
			if (44 == namespaceId && "mode" == name)
			{
				return new ListValue<StringValue>();
			}
			if (44 == namespaceId && "source" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601AFA6 RID: 110502 RVA: 0x0036A2D5 File Offset: 0x003684D5
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Arc>(deep);
		}

		// Token: 0x0400B252 RID: 45650
		private const string tagName = "arc";

		// Token: 0x0400B253 RID: 45651
		private const byte tagNsId = 44;

		// Token: 0x0400B254 RID: 45652
		internal const int ElementTypeIdConst = 12685;

		// Token: 0x0400B255 RID: 45653
		private static string[] attributeTagNames = new string[]
		{
			"from", "to", "start", "end", "offset-to-start", "duration", "confidence", "cost", "lang", "medium",
			"mode", "source"
		};

		// Token: 0x0400B256 RID: 45654
		private static byte[] attributeNamespaceIds = new byte[]
		{
			0, 0, 44, 44, 44, 44, 44, 44, 44, 44,
			44, 44
		};
	}
}
