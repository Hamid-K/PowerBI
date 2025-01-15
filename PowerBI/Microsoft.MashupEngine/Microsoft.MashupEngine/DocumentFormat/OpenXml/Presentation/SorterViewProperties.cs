using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002A8C RID: 10892
	[ChildElementInfo(typeof(ExtensionList))]
	[ChildElementInfo(typeof(CommonViewProperties))]
	[GeneratedCode("DomGen", "2.0")]
	internal class SorterViewProperties : OpenXmlCompositeElement
	{
		// Token: 0x170073AC RID: 29612
		// (get) Token: 0x06016166 RID: 90470 RVA: 0x0032671D File Offset: 0x0032491D
		public override string LocalName
		{
			get
			{
				return "sorterViewPr";
			}
		}

		// Token: 0x170073AD RID: 29613
		// (get) Token: 0x06016167 RID: 90471 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x170073AE RID: 29614
		// (get) Token: 0x06016168 RID: 90472 RVA: 0x00326724 File Offset: 0x00324924
		internal override int ElementTypeId
		{
			get
			{
				return 12305;
			}
		}

		// Token: 0x06016169 RID: 90473 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170073AF RID: 29615
		// (get) Token: 0x0601616A RID: 90474 RVA: 0x0032672B File Offset: 0x0032492B
		internal override string[] AttributeTagNames
		{
			get
			{
				return SorterViewProperties.attributeTagNames;
			}
		}

		// Token: 0x170073B0 RID: 29616
		// (get) Token: 0x0601616B RID: 90475 RVA: 0x00326732 File Offset: 0x00324932
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return SorterViewProperties.attributeNamespaceIds;
			}
		}

		// Token: 0x170073B1 RID: 29617
		// (get) Token: 0x0601616C RID: 90476 RVA: 0x002C9F7B File Offset: 0x002C817B
		// (set) Token: 0x0601616D RID: 90477 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "showFormatting")]
		public BooleanValue ShowFormatting
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

		// Token: 0x0601616E RID: 90478 RVA: 0x00293ECF File Offset: 0x002920CF
		public SorterViewProperties()
		{
		}

		// Token: 0x0601616F RID: 90479 RVA: 0x00293ED7 File Offset: 0x002920D7
		public SorterViewProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06016170 RID: 90480 RVA: 0x00293EE0 File Offset: 0x002920E0
		public SorterViewProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06016171 RID: 90481 RVA: 0x00293EE9 File Offset: 0x002920E9
		public SorterViewProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06016172 RID: 90482 RVA: 0x00326692 File Offset: 0x00324892
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (24 == namespaceId && "cViewPr" == name)
			{
				return new CommonViewProperties();
			}
			if (24 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x170073B2 RID: 29618
		// (get) Token: 0x06016173 RID: 90483 RVA: 0x00326739 File Offset: 0x00324939
		internal override string[] ElementTagNames
		{
			get
			{
				return SorterViewProperties.eleTagNames;
			}
		}

		// Token: 0x170073B3 RID: 29619
		// (get) Token: 0x06016174 RID: 90484 RVA: 0x00326740 File Offset: 0x00324940
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return SorterViewProperties.eleNamespaceIds;
			}
		}

		// Token: 0x170073B4 RID: 29620
		// (get) Token: 0x06016175 RID: 90485 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x170073B5 RID: 29621
		// (get) Token: 0x06016176 RID: 90486 RVA: 0x00326212 File Offset: 0x00324412
		// (set) Token: 0x06016177 RID: 90487 RVA: 0x0032621B File Offset: 0x0032441B
		public CommonViewProperties CommonViewProperties
		{
			get
			{
				return base.GetElement<CommonViewProperties>(0);
			}
			set
			{
				base.SetElement<CommonViewProperties>(0, value);
			}
		}

		// Token: 0x170073B6 RID: 29622
		// (get) Token: 0x06016178 RID: 90488 RVA: 0x00323F93 File Offset: 0x00322193
		// (set) Token: 0x06016179 RID: 90489 RVA: 0x00323F9C File Offset: 0x0032219C
		public ExtensionList ExtensionList
		{
			get
			{
				return base.GetElement<ExtensionList>(1);
			}
			set
			{
				base.SetElement<ExtensionList>(1, value);
			}
		}

		// Token: 0x0601617A RID: 90490 RVA: 0x00326747 File Offset: 0x00324947
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "showFormatting" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601617B RID: 90491 RVA: 0x00326767 File Offset: 0x00324967
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SorterViewProperties>(deep);
		}

		// Token: 0x0601617C RID: 90492 RVA: 0x00326770 File Offset: 0x00324970
		// Note: this type is marked as 'beforefieldinit'.
		static SorterViewProperties()
		{
			byte[] array = new byte[1];
			SorterViewProperties.attributeNamespaceIds = array;
			SorterViewProperties.eleTagNames = new string[] { "cViewPr", "extLst" };
			SorterViewProperties.eleNamespaceIds = new byte[] { 24, 24 };
		}

		// Token: 0x04009625 RID: 38437
		private const string tagName = "sorterViewPr";

		// Token: 0x04009626 RID: 38438
		private const byte tagNsId = 24;

		// Token: 0x04009627 RID: 38439
		internal const int ElementTypeIdConst = 12305;

		// Token: 0x04009628 RID: 38440
		private static string[] attributeTagNames = new string[] { "showFormatting" };

		// Token: 0x04009629 RID: 38441
		private static byte[] attributeNamespaceIds;

		// Token: 0x0400962A RID: 38442
		private static readonly string[] eleTagNames;

		// Token: 0x0400962B RID: 38443
		private static readonly byte[] eleNamespaceIds;
	}
}
