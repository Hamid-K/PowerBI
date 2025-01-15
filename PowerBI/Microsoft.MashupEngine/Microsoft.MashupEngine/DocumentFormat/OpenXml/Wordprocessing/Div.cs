using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002FC2 RID: 12226
	[ChildElementInfo(typeof(BottomMarginDiv))]
	[ChildElementInfo(typeof(BlockQuote))]
	[ChildElementInfo(typeof(BodyDiv))]
	[ChildElementInfo(typeof(LeftMarginDiv))]
	[ChildElementInfo(typeof(RightMarginDiv))]
	[ChildElementInfo(typeof(TopMarginDiv))]
	[ChildElementInfo(typeof(DivBorder))]
	[ChildElementInfo(typeof(DivsChild))]
	[GeneratedCode("DomGen", "2.0")]
	internal class Div : OpenXmlCompositeElement
	{
		// Token: 0x170093E4 RID: 37860
		// (get) Token: 0x0601A831 RID: 108593 RVA: 0x003633D0 File Offset: 0x003615D0
		public override string LocalName
		{
			get
			{
				return "div";
			}
		}

		// Token: 0x170093E5 RID: 37861
		// (get) Token: 0x0601A832 RID: 108594 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x170093E6 RID: 37862
		// (get) Token: 0x0601A833 RID: 108595 RVA: 0x003633D7 File Offset: 0x003615D7
		internal override int ElementTypeId
		{
			get
			{
				return 11935;
			}
		}

		// Token: 0x0601A834 RID: 108596 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170093E7 RID: 37863
		// (get) Token: 0x0601A835 RID: 108597 RVA: 0x003633DE File Offset: 0x003615DE
		internal override string[] AttributeTagNames
		{
			get
			{
				return Div.attributeTagNames;
			}
		}

		// Token: 0x170093E8 RID: 37864
		// (get) Token: 0x0601A836 RID: 108598 RVA: 0x003633E5 File Offset: 0x003615E5
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Div.attributeNamespaceIds;
			}
		}

		// Token: 0x170093E9 RID: 37865
		// (get) Token: 0x0601A837 RID: 108599 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0601A838 RID: 108600 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(23, "id")]
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

		// Token: 0x0601A839 RID: 108601 RVA: 0x00293ECF File Offset: 0x002920CF
		public Div()
		{
		}

		// Token: 0x0601A83A RID: 108602 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Div(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601A83B RID: 108603 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Div(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601A83C RID: 108604 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Div(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601A83D RID: 108605 RVA: 0x003633EC File Offset: 0x003615EC
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "blockQuote" == name)
			{
				return new BlockQuote();
			}
			if (23 == namespaceId && "bodyDiv" == name)
			{
				return new BodyDiv();
			}
			if (23 == namespaceId && "marLeft" == name)
			{
				return new LeftMarginDiv();
			}
			if (23 == namespaceId && "marRight" == name)
			{
				return new RightMarginDiv();
			}
			if (23 == namespaceId && "marTop" == name)
			{
				return new TopMarginDiv();
			}
			if (23 == namespaceId && "marBottom" == name)
			{
				return new BottomMarginDiv();
			}
			if (23 == namespaceId && "divBdr" == name)
			{
				return new DivBorder();
			}
			if (23 == namespaceId && "divsChild" == name)
			{
				return new DivsChild();
			}
			return null;
		}

		// Token: 0x170093EA RID: 37866
		// (get) Token: 0x0601A83E RID: 108606 RVA: 0x003634BA File Offset: 0x003616BA
		internal override string[] ElementTagNames
		{
			get
			{
				return Div.eleTagNames;
			}
		}

		// Token: 0x170093EB RID: 37867
		// (get) Token: 0x0601A83F RID: 108607 RVA: 0x003634C1 File Offset: 0x003616C1
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return Div.eleNamespaceIds;
			}
		}

		// Token: 0x170093EC RID: 37868
		// (get) Token: 0x0601A840 RID: 108608 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x170093ED RID: 37869
		// (get) Token: 0x0601A841 RID: 108609 RVA: 0x003634C8 File Offset: 0x003616C8
		// (set) Token: 0x0601A842 RID: 108610 RVA: 0x003634D1 File Offset: 0x003616D1
		public BlockQuote BlockQuote
		{
			get
			{
				return base.GetElement<BlockQuote>(0);
			}
			set
			{
				base.SetElement<BlockQuote>(0, value);
			}
		}

		// Token: 0x170093EE RID: 37870
		// (get) Token: 0x0601A843 RID: 108611 RVA: 0x003634DB File Offset: 0x003616DB
		// (set) Token: 0x0601A844 RID: 108612 RVA: 0x003634E4 File Offset: 0x003616E4
		public BodyDiv BodyDiv
		{
			get
			{
				return base.GetElement<BodyDiv>(1);
			}
			set
			{
				base.SetElement<BodyDiv>(1, value);
			}
		}

		// Token: 0x170093EF RID: 37871
		// (get) Token: 0x0601A845 RID: 108613 RVA: 0x003634EE File Offset: 0x003616EE
		// (set) Token: 0x0601A846 RID: 108614 RVA: 0x003634F7 File Offset: 0x003616F7
		public LeftMarginDiv LeftMarginDiv
		{
			get
			{
				return base.GetElement<LeftMarginDiv>(2);
			}
			set
			{
				base.SetElement<LeftMarginDiv>(2, value);
			}
		}

		// Token: 0x170093F0 RID: 37872
		// (get) Token: 0x0601A847 RID: 108615 RVA: 0x00363501 File Offset: 0x00361701
		// (set) Token: 0x0601A848 RID: 108616 RVA: 0x0036350A File Offset: 0x0036170A
		public RightMarginDiv RightMarginDiv
		{
			get
			{
				return base.GetElement<RightMarginDiv>(3);
			}
			set
			{
				base.SetElement<RightMarginDiv>(3, value);
			}
		}

		// Token: 0x170093F1 RID: 37873
		// (get) Token: 0x0601A849 RID: 108617 RVA: 0x00363514 File Offset: 0x00361714
		// (set) Token: 0x0601A84A RID: 108618 RVA: 0x0036351D File Offset: 0x0036171D
		public TopMarginDiv TopMarginDiv
		{
			get
			{
				return base.GetElement<TopMarginDiv>(4);
			}
			set
			{
				base.SetElement<TopMarginDiv>(4, value);
			}
		}

		// Token: 0x170093F2 RID: 37874
		// (get) Token: 0x0601A84B RID: 108619 RVA: 0x00363527 File Offset: 0x00361727
		// (set) Token: 0x0601A84C RID: 108620 RVA: 0x00363530 File Offset: 0x00361730
		public BottomMarginDiv BottomMarginDiv
		{
			get
			{
				return base.GetElement<BottomMarginDiv>(5);
			}
			set
			{
				base.SetElement<BottomMarginDiv>(5, value);
			}
		}

		// Token: 0x170093F3 RID: 37875
		// (get) Token: 0x0601A84D RID: 108621 RVA: 0x0036353A File Offset: 0x0036173A
		// (set) Token: 0x0601A84E RID: 108622 RVA: 0x00363543 File Offset: 0x00361743
		public DivBorder DivBorder
		{
			get
			{
				return base.GetElement<DivBorder>(6);
			}
			set
			{
				base.SetElement<DivBorder>(6, value);
			}
		}

		// Token: 0x0601A84F RID: 108623 RVA: 0x002EE1BE File Offset: 0x002EC3BE
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "id" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601A850 RID: 108624 RVA: 0x0036354D File Offset: 0x0036174D
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Div>(deep);
		}

		// Token: 0x0400AD4B RID: 44363
		private const string tagName = "div";

		// Token: 0x0400AD4C RID: 44364
		private const byte tagNsId = 23;

		// Token: 0x0400AD4D RID: 44365
		internal const int ElementTypeIdConst = 11935;

		// Token: 0x0400AD4E RID: 44366
		private static string[] attributeTagNames = new string[] { "id" };

		// Token: 0x0400AD4F RID: 44367
		private static byte[] attributeNamespaceIds = new byte[] { 23 };

		// Token: 0x0400AD50 RID: 44368
		private static readonly string[] eleTagNames = new string[] { "blockQuote", "bodyDiv", "marLeft", "marRight", "marTop", "marBottom", "divBdr", "divsChild" };

		// Token: 0x0400AD51 RID: 44369
		private static readonly byte[] eleNamespaceIds = new byte[] { 23, 23, 23, 23, 23, 23, 23, 23 };
	}
}
