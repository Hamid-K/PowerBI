using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x020025CD RID: 9677
	[ChildElementInfo(typeof(FirstFooter))]
	[ChildElementInfo(typeof(OddFooter))]
	[ChildElementInfo(typeof(EvenHeader))]
	[ChildElementInfo(typeof(EvenFooter))]
	[ChildElementInfo(typeof(FirstHeader))]
	[ChildElementInfo(typeof(OddHeader))]
	[GeneratedCode("DomGen", "2.0")]
	internal class HeaderFooter : OpenXmlCompositeElement
	{
		// Token: 0x170057CB RID: 22475
		// (get) Token: 0x06012263 RID: 74339 RVA: 0x002F6477 File Offset: 0x002F4677
		public override string LocalName
		{
			get
			{
				return "headerFooter";
			}
		}

		// Token: 0x170057CC RID: 22476
		// (get) Token: 0x06012264 RID: 74340 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x170057CD RID: 22477
		// (get) Token: 0x06012265 RID: 74341 RVA: 0x002F647E File Offset: 0x002F467E
		internal override int ElementTypeId
		{
			get
			{
				return 10517;
			}
		}

		// Token: 0x06012266 RID: 74342 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170057CE RID: 22478
		// (get) Token: 0x06012267 RID: 74343 RVA: 0x002F6485 File Offset: 0x002F4685
		internal override string[] AttributeTagNames
		{
			get
			{
				return HeaderFooter.attributeTagNames;
			}
		}

		// Token: 0x170057CF RID: 22479
		// (get) Token: 0x06012268 RID: 74344 RVA: 0x002F648C File Offset: 0x002F468C
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return HeaderFooter.attributeNamespaceIds;
			}
		}

		// Token: 0x170057D0 RID: 22480
		// (get) Token: 0x06012269 RID: 74345 RVA: 0x002C9F7B File Offset: 0x002C817B
		// (set) Token: 0x0601226A RID: 74346 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "alignWithMargins")]
		public BooleanValue AlignWithMargins
		{
			get
			{
				return (BooleanValue)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x170057D1 RID: 22481
		// (get) Token: 0x0601226B RID: 74347 RVA: 0x002C8B36 File Offset: 0x002C6D36
		// (set) Token: 0x0601226C RID: 74348 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "differentOddEven")]
		public BooleanValue DifferentOddEven
		{
			get
			{
				return (BooleanValue)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x170057D2 RID: 22482
		// (get) Token: 0x0601226D RID: 74349 RVA: 0x002C8F66 File Offset: 0x002C7166
		// (set) Token: 0x0601226E RID: 74350 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "differentFirst")]
		public BooleanValue DifferentFirst
		{
			get
			{
				return (BooleanValue)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x0601226F RID: 74351 RVA: 0x00293ECF File Offset: 0x002920CF
		public HeaderFooter()
		{
		}

		// Token: 0x06012270 RID: 74352 RVA: 0x00293ED7 File Offset: 0x002920D7
		public HeaderFooter(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012271 RID: 74353 RVA: 0x00293EE0 File Offset: 0x002920E0
		public HeaderFooter(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012272 RID: 74354 RVA: 0x00293EE9 File Offset: 0x002920E9
		public HeaderFooter(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06012273 RID: 74355 RVA: 0x002F6494 File Offset: 0x002F4694
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (11 == namespaceId && "oddHeader" == name)
			{
				return new OddHeader();
			}
			if (11 == namespaceId && "oddFooter" == name)
			{
				return new OddFooter();
			}
			if (11 == namespaceId && "evenHeader" == name)
			{
				return new EvenHeader();
			}
			if (11 == namespaceId && "evenFooter" == name)
			{
				return new EvenFooter();
			}
			if (11 == namespaceId && "firstHeader" == name)
			{
				return new FirstHeader();
			}
			if (11 == namespaceId && "firstFooter" == name)
			{
				return new FirstFooter();
			}
			return null;
		}

		// Token: 0x170057D3 RID: 22483
		// (get) Token: 0x06012274 RID: 74356 RVA: 0x002F6532 File Offset: 0x002F4732
		internal override string[] ElementTagNames
		{
			get
			{
				return HeaderFooter.eleTagNames;
			}
		}

		// Token: 0x170057D4 RID: 22484
		// (get) Token: 0x06012275 RID: 74357 RVA: 0x002F6539 File Offset: 0x002F4739
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return HeaderFooter.eleNamespaceIds;
			}
		}

		// Token: 0x170057D5 RID: 22485
		// (get) Token: 0x06012276 RID: 74358 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x170057D6 RID: 22486
		// (get) Token: 0x06012277 RID: 74359 RVA: 0x002F6540 File Offset: 0x002F4740
		// (set) Token: 0x06012278 RID: 74360 RVA: 0x002F6549 File Offset: 0x002F4749
		public OddHeader OddHeader
		{
			get
			{
				return base.GetElement<OddHeader>(0);
			}
			set
			{
				base.SetElement<OddHeader>(0, value);
			}
		}

		// Token: 0x170057D7 RID: 22487
		// (get) Token: 0x06012279 RID: 74361 RVA: 0x002F6553 File Offset: 0x002F4753
		// (set) Token: 0x0601227A RID: 74362 RVA: 0x002F655C File Offset: 0x002F475C
		public OddFooter OddFooter
		{
			get
			{
				return base.GetElement<OddFooter>(1);
			}
			set
			{
				base.SetElement<OddFooter>(1, value);
			}
		}

		// Token: 0x170057D8 RID: 22488
		// (get) Token: 0x0601227B RID: 74363 RVA: 0x002F6566 File Offset: 0x002F4766
		// (set) Token: 0x0601227C RID: 74364 RVA: 0x002F656F File Offset: 0x002F476F
		public EvenHeader EvenHeader
		{
			get
			{
				return base.GetElement<EvenHeader>(2);
			}
			set
			{
				base.SetElement<EvenHeader>(2, value);
			}
		}

		// Token: 0x170057D9 RID: 22489
		// (get) Token: 0x0601227D RID: 74365 RVA: 0x002F6579 File Offset: 0x002F4779
		// (set) Token: 0x0601227E RID: 74366 RVA: 0x002F6582 File Offset: 0x002F4782
		public EvenFooter EvenFooter
		{
			get
			{
				return base.GetElement<EvenFooter>(3);
			}
			set
			{
				base.SetElement<EvenFooter>(3, value);
			}
		}

		// Token: 0x170057DA RID: 22490
		// (get) Token: 0x0601227F RID: 74367 RVA: 0x002F658C File Offset: 0x002F478C
		// (set) Token: 0x06012280 RID: 74368 RVA: 0x002F6595 File Offset: 0x002F4795
		public FirstHeader FirstHeader
		{
			get
			{
				return base.GetElement<FirstHeader>(4);
			}
			set
			{
				base.SetElement<FirstHeader>(4, value);
			}
		}

		// Token: 0x170057DB RID: 22491
		// (get) Token: 0x06012281 RID: 74369 RVA: 0x002F659F File Offset: 0x002F479F
		// (set) Token: 0x06012282 RID: 74370 RVA: 0x002F65A8 File Offset: 0x002F47A8
		public FirstFooter FirstFooter
		{
			get
			{
				return base.GetElement<FirstFooter>(5);
			}
			set
			{
				base.SetElement<FirstFooter>(5, value);
			}
		}

		// Token: 0x06012283 RID: 74371 RVA: 0x002F65B4 File Offset: 0x002F47B4
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "alignWithMargins" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "differentOddEven" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "differentFirst" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06012284 RID: 74372 RVA: 0x002F660B File Offset: 0x002F480B
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<HeaderFooter>(deep);
		}

		// Token: 0x06012285 RID: 74373 RVA: 0x002F6614 File Offset: 0x002F4814
		// Note: this type is marked as 'beforefieldinit'.
		static HeaderFooter()
		{
			byte[] array = new byte[3];
			HeaderFooter.attributeNamespaceIds = array;
			HeaderFooter.eleTagNames = new string[] { "oddHeader", "oddFooter", "evenHeader", "evenFooter", "firstHeader", "firstFooter" };
			HeaderFooter.eleNamespaceIds = new byte[] { 11, 11, 11, 11, 11, 11 };
		}

		// Token: 0x04007E73 RID: 32371
		private const string tagName = "headerFooter";

		// Token: 0x04007E74 RID: 32372
		private const byte tagNsId = 11;

		// Token: 0x04007E75 RID: 32373
		internal const int ElementTypeIdConst = 10517;

		// Token: 0x04007E76 RID: 32374
		private static string[] attributeTagNames = new string[] { "alignWithMargins", "differentOddEven", "differentFirst" };

		// Token: 0x04007E77 RID: 32375
		private static byte[] attributeNamespaceIds;

		// Token: 0x04007E78 RID: 32376
		private static readonly string[] eleTagNames;

		// Token: 0x04007E79 RID: 32377
		private static readonly byte[] eleNamespaceIds;
	}
}
