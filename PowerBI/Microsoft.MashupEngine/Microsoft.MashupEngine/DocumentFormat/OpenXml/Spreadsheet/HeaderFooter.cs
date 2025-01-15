using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002BDD RID: 11229
	[ChildElementInfo(typeof(OddFooter))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(OddHeader))]
	[ChildElementInfo(typeof(EvenFooter))]
	[ChildElementInfo(typeof(FirstFooter))]
	[ChildElementInfo(typeof(EvenHeader))]
	[ChildElementInfo(typeof(FirstHeader))]
	internal class HeaderFooter : OpenXmlCompositeElement
	{
		// Token: 0x17007D92 RID: 32146
		// (get) Token: 0x06017769 RID: 96105 RVA: 0x002F6477 File Offset: 0x002F4677
		public override string LocalName
		{
			get
			{
				return "headerFooter";
			}
		}

		// Token: 0x17007D93 RID: 32147
		// (get) Token: 0x0601776A RID: 96106 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17007D94 RID: 32148
		// (get) Token: 0x0601776B RID: 96107 RVA: 0x00337197 File Offset: 0x00335397
		internal override int ElementTypeId
		{
			get
			{
				return 11201;
			}
		}

		// Token: 0x0601776C RID: 96108 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17007D95 RID: 32149
		// (get) Token: 0x0601776D RID: 96109 RVA: 0x0033719E File Offset: 0x0033539E
		internal override string[] AttributeTagNames
		{
			get
			{
				return HeaderFooter.attributeTagNames;
			}
		}

		// Token: 0x17007D96 RID: 32150
		// (get) Token: 0x0601776E RID: 96110 RVA: 0x003371A5 File Offset: 0x003353A5
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return HeaderFooter.attributeNamespaceIds;
			}
		}

		// Token: 0x17007D97 RID: 32151
		// (get) Token: 0x0601776F RID: 96111 RVA: 0x002C9F7B File Offset: 0x002C817B
		// (set) Token: 0x06017770 RID: 96112 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "differentOddEven")]
		public BooleanValue DifferentOddEven
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

		// Token: 0x17007D98 RID: 32152
		// (get) Token: 0x06017771 RID: 96113 RVA: 0x002C8B36 File Offset: 0x002C6D36
		// (set) Token: 0x06017772 RID: 96114 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "differentFirst")]
		public BooleanValue DifferentFirst
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

		// Token: 0x17007D99 RID: 32153
		// (get) Token: 0x06017773 RID: 96115 RVA: 0x002C8F66 File Offset: 0x002C7166
		// (set) Token: 0x06017774 RID: 96116 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "scaleWithDoc")]
		public BooleanValue ScaleWithDoc
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

		// Token: 0x17007D9A RID: 32154
		// (get) Token: 0x06017775 RID: 96117 RVA: 0x002CB9D9 File Offset: 0x002C9BD9
		// (set) Token: 0x06017776 RID: 96118 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "alignWithMargins")]
		public BooleanValue AlignWithMargins
		{
			get
			{
				return (BooleanValue)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x06017777 RID: 96119 RVA: 0x00293ECF File Offset: 0x002920CF
		public HeaderFooter()
		{
		}

		// Token: 0x06017778 RID: 96120 RVA: 0x00293ED7 File Offset: 0x002920D7
		public HeaderFooter(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06017779 RID: 96121 RVA: 0x00293EE0 File Offset: 0x002920E0
		public HeaderFooter(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601777A RID: 96122 RVA: 0x00293EE9 File Offset: 0x002920E9
		public HeaderFooter(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601777B RID: 96123 RVA: 0x003371AC File Offset: 0x003353AC
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "oddHeader" == name)
			{
				return new OddHeader();
			}
			if (22 == namespaceId && "oddFooter" == name)
			{
				return new OddFooter();
			}
			if (22 == namespaceId && "evenHeader" == name)
			{
				return new EvenHeader();
			}
			if (22 == namespaceId && "evenFooter" == name)
			{
				return new EvenFooter();
			}
			if (22 == namespaceId && "firstHeader" == name)
			{
				return new FirstHeader();
			}
			if (22 == namespaceId && "firstFooter" == name)
			{
				return new FirstFooter();
			}
			return null;
		}

		// Token: 0x17007D9B RID: 32155
		// (get) Token: 0x0601777C RID: 96124 RVA: 0x0033724A File Offset: 0x0033544A
		internal override string[] ElementTagNames
		{
			get
			{
				return HeaderFooter.eleTagNames;
			}
		}

		// Token: 0x17007D9C RID: 32156
		// (get) Token: 0x0601777D RID: 96125 RVA: 0x00337251 File Offset: 0x00335451
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return HeaderFooter.eleNamespaceIds;
			}
		}

		// Token: 0x17007D9D RID: 32157
		// (get) Token: 0x0601777E RID: 96126 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17007D9E RID: 32158
		// (get) Token: 0x0601777F RID: 96127 RVA: 0x00337258 File Offset: 0x00335458
		// (set) Token: 0x06017780 RID: 96128 RVA: 0x00337261 File Offset: 0x00335461
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

		// Token: 0x17007D9F RID: 32159
		// (get) Token: 0x06017781 RID: 96129 RVA: 0x0033726B File Offset: 0x0033546B
		// (set) Token: 0x06017782 RID: 96130 RVA: 0x00337274 File Offset: 0x00335474
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

		// Token: 0x17007DA0 RID: 32160
		// (get) Token: 0x06017783 RID: 96131 RVA: 0x0033727E File Offset: 0x0033547E
		// (set) Token: 0x06017784 RID: 96132 RVA: 0x00337287 File Offset: 0x00335487
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

		// Token: 0x17007DA1 RID: 32161
		// (get) Token: 0x06017785 RID: 96133 RVA: 0x00337291 File Offset: 0x00335491
		// (set) Token: 0x06017786 RID: 96134 RVA: 0x0033729A File Offset: 0x0033549A
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

		// Token: 0x17007DA2 RID: 32162
		// (get) Token: 0x06017787 RID: 96135 RVA: 0x003372A4 File Offset: 0x003354A4
		// (set) Token: 0x06017788 RID: 96136 RVA: 0x003372AD File Offset: 0x003354AD
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

		// Token: 0x17007DA3 RID: 32163
		// (get) Token: 0x06017789 RID: 96137 RVA: 0x003372B7 File Offset: 0x003354B7
		// (set) Token: 0x0601778A RID: 96138 RVA: 0x003372C0 File Offset: 0x003354C0
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

		// Token: 0x0601778B RID: 96139 RVA: 0x003372CC File Offset: 0x003354CC
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "differentOddEven" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "differentFirst" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "scaleWithDoc" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "alignWithMargins" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601778C RID: 96140 RVA: 0x00337339 File Offset: 0x00335539
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<HeaderFooter>(deep);
		}

		// Token: 0x0601778D RID: 96141 RVA: 0x00337344 File Offset: 0x00335544
		// Note: this type is marked as 'beforefieldinit'.
		static HeaderFooter()
		{
			byte[] array = new byte[4];
			HeaderFooter.attributeNamespaceIds = array;
			HeaderFooter.eleTagNames = new string[] { "oddHeader", "oddFooter", "evenHeader", "evenFooter", "firstHeader", "firstFooter" };
			HeaderFooter.eleNamespaceIds = new byte[] { 22, 22, 22, 22, 22, 22 };
		}

		// Token: 0x04009C6D RID: 40045
		private const string tagName = "headerFooter";

		// Token: 0x04009C6E RID: 40046
		private const byte tagNsId = 22;

		// Token: 0x04009C6F RID: 40047
		internal const int ElementTypeIdConst = 11201;

		// Token: 0x04009C70 RID: 40048
		private static string[] attributeTagNames = new string[] { "differentOddEven", "differentFirst", "scaleWithDoc", "alignWithMargins" };

		// Token: 0x04009C71 RID: 40049
		private static byte[] attributeNamespaceIds;

		// Token: 0x04009C72 RID: 40050
		private static readonly string[] eleTagNames;

		// Token: 0x04009C73 RID: 40051
		private static readonly byte[] eleNamespaceIds;
	}
}
