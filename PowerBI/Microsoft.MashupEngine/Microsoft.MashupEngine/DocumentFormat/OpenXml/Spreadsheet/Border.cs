using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002C0F RID: 11279
	[ChildElementInfo(typeof(LeftBorder))]
	[ChildElementInfo(typeof(HorizontalBorder))]
	[ChildElementInfo(typeof(VerticalBorder))]
	[ChildElementInfo(typeof(DiagonalBorder))]
	[ChildElementInfo(typeof(BottomBorder))]
	[ChildElementInfo(typeof(RightBorder))]
	[ChildElementInfo(typeof(TopBorder))]
	[ChildElementInfo(typeof(StartBorder), FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(EndBorder), FileFormatVersions.Office2010)]
	internal class Border : OpenXmlCompositeElement
	{
		// Token: 0x17007FDA RID: 32730
		// (get) Token: 0x06017C45 RID: 97349 RVA: 0x0033AF1D File Offset: 0x0033911D
		public override string LocalName
		{
			get
			{
				return "border";
			}
		}

		// Token: 0x17007FDB RID: 32731
		// (get) Token: 0x06017C46 RID: 97350 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17007FDC RID: 32732
		// (get) Token: 0x06017C47 RID: 97351 RVA: 0x0033AF24 File Offset: 0x00339124
		internal override int ElementTypeId
		{
			get
			{
				return 11260;
			}
		}

		// Token: 0x06017C48 RID: 97352 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17007FDD RID: 32733
		// (get) Token: 0x06017C49 RID: 97353 RVA: 0x0033AF2B File Offset: 0x0033912B
		internal override string[] AttributeTagNames
		{
			get
			{
				return Border.attributeTagNames;
			}
		}

		// Token: 0x17007FDE RID: 32734
		// (get) Token: 0x06017C4A RID: 97354 RVA: 0x0033AF32 File Offset: 0x00339132
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Border.attributeNamespaceIds;
			}
		}

		// Token: 0x17007FDF RID: 32735
		// (get) Token: 0x06017C4B RID: 97355 RVA: 0x002C9F7B File Offset: 0x002C817B
		// (set) Token: 0x06017C4C RID: 97356 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "diagonalUp")]
		public BooleanValue DiagonalUp
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

		// Token: 0x17007FE0 RID: 32736
		// (get) Token: 0x06017C4D RID: 97357 RVA: 0x002C8B36 File Offset: 0x002C6D36
		// (set) Token: 0x06017C4E RID: 97358 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "diagonalDown")]
		public BooleanValue DiagonalDown
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

		// Token: 0x17007FE1 RID: 32737
		// (get) Token: 0x06017C4F RID: 97359 RVA: 0x002C8F66 File Offset: 0x002C7166
		// (set) Token: 0x06017C50 RID: 97360 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "outline")]
		public BooleanValue Outline
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

		// Token: 0x06017C51 RID: 97361 RVA: 0x00293ECF File Offset: 0x002920CF
		public Border()
		{
		}

		// Token: 0x06017C52 RID: 97362 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Border(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06017C53 RID: 97363 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Border(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06017C54 RID: 97364 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Border(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06017C55 RID: 97365 RVA: 0x0033AF3C File Offset: 0x0033913C
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "start" == name)
			{
				return new StartBorder();
			}
			if (22 == namespaceId && "end" == name)
			{
				return new EndBorder();
			}
			if (22 == namespaceId && "left" == name)
			{
				return new LeftBorder();
			}
			if (22 == namespaceId && "right" == name)
			{
				return new RightBorder();
			}
			if (22 == namespaceId && "top" == name)
			{
				return new TopBorder();
			}
			if (22 == namespaceId && "bottom" == name)
			{
				return new BottomBorder();
			}
			if (22 == namespaceId && "diagonal" == name)
			{
				return new DiagonalBorder();
			}
			if (22 == namespaceId && "vertical" == name)
			{
				return new VerticalBorder();
			}
			if (22 == namespaceId && "horizontal" == name)
			{
				return new HorizontalBorder();
			}
			return null;
		}

		// Token: 0x17007FE2 RID: 32738
		// (get) Token: 0x06017C56 RID: 97366 RVA: 0x0033B022 File Offset: 0x00339222
		internal override string[] ElementTagNames
		{
			get
			{
				return Border.eleTagNames;
			}
		}

		// Token: 0x17007FE3 RID: 32739
		// (get) Token: 0x06017C57 RID: 97367 RVA: 0x0033B029 File Offset: 0x00339229
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return Border.eleNamespaceIds;
			}
		}

		// Token: 0x17007FE4 RID: 32740
		// (get) Token: 0x06017C58 RID: 97368 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17007FE5 RID: 32741
		// (get) Token: 0x06017C59 RID: 97369 RVA: 0x0033B030 File Offset: 0x00339230
		// (set) Token: 0x06017C5A RID: 97370 RVA: 0x0033B039 File Offset: 0x00339239
		[OfficeAvailability(FileFormatVersions.Office2010)]
		public StartBorder StartBorder
		{
			get
			{
				return base.GetElement<StartBorder>(0);
			}
			set
			{
				base.SetElement<StartBorder>(0, value);
			}
		}

		// Token: 0x17007FE6 RID: 32742
		// (get) Token: 0x06017C5B RID: 97371 RVA: 0x0033B043 File Offset: 0x00339243
		// (set) Token: 0x06017C5C RID: 97372 RVA: 0x0033B04C File Offset: 0x0033924C
		[OfficeAvailability(FileFormatVersions.Office2010)]
		public EndBorder EndBorder
		{
			get
			{
				return base.GetElement<EndBorder>(1);
			}
			set
			{
				base.SetElement<EndBorder>(1, value);
			}
		}

		// Token: 0x17007FE7 RID: 32743
		// (get) Token: 0x06017C5D RID: 97373 RVA: 0x0033B056 File Offset: 0x00339256
		// (set) Token: 0x06017C5E RID: 97374 RVA: 0x0033B05F File Offset: 0x0033925F
		public LeftBorder LeftBorder
		{
			get
			{
				return base.GetElement<LeftBorder>(2);
			}
			set
			{
				base.SetElement<LeftBorder>(2, value);
			}
		}

		// Token: 0x17007FE8 RID: 32744
		// (get) Token: 0x06017C5F RID: 97375 RVA: 0x0033B069 File Offset: 0x00339269
		// (set) Token: 0x06017C60 RID: 97376 RVA: 0x0033B072 File Offset: 0x00339272
		public RightBorder RightBorder
		{
			get
			{
				return base.GetElement<RightBorder>(3);
			}
			set
			{
				base.SetElement<RightBorder>(3, value);
			}
		}

		// Token: 0x17007FE9 RID: 32745
		// (get) Token: 0x06017C61 RID: 97377 RVA: 0x0033B07C File Offset: 0x0033927C
		// (set) Token: 0x06017C62 RID: 97378 RVA: 0x0033B085 File Offset: 0x00339285
		public TopBorder TopBorder
		{
			get
			{
				return base.GetElement<TopBorder>(4);
			}
			set
			{
				base.SetElement<TopBorder>(4, value);
			}
		}

		// Token: 0x17007FEA RID: 32746
		// (get) Token: 0x06017C63 RID: 97379 RVA: 0x0033B08F File Offset: 0x0033928F
		// (set) Token: 0x06017C64 RID: 97380 RVA: 0x0033B098 File Offset: 0x00339298
		public BottomBorder BottomBorder
		{
			get
			{
				return base.GetElement<BottomBorder>(5);
			}
			set
			{
				base.SetElement<BottomBorder>(5, value);
			}
		}

		// Token: 0x17007FEB RID: 32747
		// (get) Token: 0x06017C65 RID: 97381 RVA: 0x0033B0A2 File Offset: 0x003392A2
		// (set) Token: 0x06017C66 RID: 97382 RVA: 0x0033B0AB File Offset: 0x003392AB
		public DiagonalBorder DiagonalBorder
		{
			get
			{
				return base.GetElement<DiagonalBorder>(6);
			}
			set
			{
				base.SetElement<DiagonalBorder>(6, value);
			}
		}

		// Token: 0x17007FEC RID: 32748
		// (get) Token: 0x06017C67 RID: 97383 RVA: 0x0033B0B5 File Offset: 0x003392B5
		// (set) Token: 0x06017C68 RID: 97384 RVA: 0x0033B0BE File Offset: 0x003392BE
		public VerticalBorder VerticalBorder
		{
			get
			{
				return base.GetElement<VerticalBorder>(7);
			}
			set
			{
				base.SetElement<VerticalBorder>(7, value);
			}
		}

		// Token: 0x17007FED RID: 32749
		// (get) Token: 0x06017C69 RID: 97385 RVA: 0x0033B0C8 File Offset: 0x003392C8
		// (set) Token: 0x06017C6A RID: 97386 RVA: 0x0033B0D1 File Offset: 0x003392D1
		public HorizontalBorder HorizontalBorder
		{
			get
			{
				return base.GetElement<HorizontalBorder>(8);
			}
			set
			{
				base.SetElement<HorizontalBorder>(8, value);
			}
		}

		// Token: 0x06017C6B RID: 97387 RVA: 0x0033B0DC File Offset: 0x003392DC
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "diagonalUp" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "diagonalDown" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "outline" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06017C6C RID: 97388 RVA: 0x0033B133 File Offset: 0x00339333
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Border>(deep);
		}

		// Token: 0x06017C6D RID: 97389 RVA: 0x0033B13C File Offset: 0x0033933C
		// Note: this type is marked as 'beforefieldinit'.
		static Border()
		{
			byte[] array = new byte[3];
			Border.attributeNamespaceIds = array;
			Border.eleTagNames = new string[] { "start", "end", "left", "right", "top", "bottom", "diagonal", "vertical", "horizontal" };
			Border.eleNamespaceIds = new byte[] { 22, 22, 22, 22, 22, 22, 22, 22, 22 };
		}

		// Token: 0x04009D7C RID: 40316
		private const string tagName = "border";

		// Token: 0x04009D7D RID: 40317
		private const byte tagNsId = 22;

		// Token: 0x04009D7E RID: 40318
		internal const int ElementTypeIdConst = 11260;

		// Token: 0x04009D7F RID: 40319
		private static string[] attributeTagNames = new string[] { "diagonalUp", "diagonalDown", "outline" };

		// Token: 0x04009D80 RID: 40320
		private static byte[] attributeNamespaceIds;

		// Token: 0x04009D81 RID: 40321
		private static readonly string[] eleTagNames;

		// Token: 0x04009D82 RID: 40322
		private static readonly byte[] eleNamespaceIds;
	}
}
