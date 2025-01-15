using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002A51 RID: 10833
	[ChildElementInfo(typeof(ExtensionList))]
	[GeneratedCode("DomGen", "2.0")]
	internal class SlideMasterId : OpenXmlCompositeElement
	{
		// Token: 0x170071DD RID: 29149
		// (get) Token: 0x06015D6E RID: 89454 RVA: 0x00323A5F File Offset: 0x00321C5F
		public override string LocalName
		{
			get
			{
				return "sldMasterId";
			}
		}

		// Token: 0x170071DE RID: 29150
		// (get) Token: 0x06015D6F RID: 89455 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x170071DF RID: 29151
		// (get) Token: 0x06015D70 RID: 89456 RVA: 0x00323A66 File Offset: 0x00321C66
		internal override int ElementTypeId
		{
			get
			{
				return 12252;
			}
		}

		// Token: 0x06015D71 RID: 89457 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170071E0 RID: 29152
		// (get) Token: 0x06015D72 RID: 89458 RVA: 0x00323A6D File Offset: 0x00321C6D
		internal override string[] AttributeTagNames
		{
			get
			{
				return SlideMasterId.attributeTagNames;
			}
		}

		// Token: 0x170071E1 RID: 29153
		// (get) Token: 0x06015D73 RID: 89459 RVA: 0x00323A74 File Offset: 0x00321C74
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return SlideMasterId.attributeNamespaceIds;
			}
		}

		// Token: 0x170071E2 RID: 29154
		// (get) Token: 0x06015D74 RID: 89460 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x06015D75 RID: 89461 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "id")]
		public UInt32Value Id
		{
			get
			{
				return (UInt32Value)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x170071E3 RID: 29155
		// (get) Token: 0x06015D76 RID: 89462 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x06015D77 RID: 89463 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(19, "id")]
		public StringValue RelationshipId
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

		// Token: 0x06015D78 RID: 89464 RVA: 0x00293ECF File Offset: 0x002920CF
		public SlideMasterId()
		{
		}

		// Token: 0x06015D79 RID: 89465 RVA: 0x00293ED7 File Offset: 0x002920D7
		public SlideMasterId(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015D7A RID: 89466 RVA: 0x00293EE0 File Offset: 0x002920E0
		public SlideMasterId(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015D7B RID: 89467 RVA: 0x00293EE9 File Offset: 0x002920E9
		public SlideMasterId(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06015D7C RID: 89468 RVA: 0x0031FDA2 File Offset: 0x0031DFA2
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (24 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x170071E4 RID: 29156
		// (get) Token: 0x06015D7D RID: 89469 RVA: 0x00323A7B File Offset: 0x00321C7B
		internal override string[] ElementTagNames
		{
			get
			{
				return SlideMasterId.eleTagNames;
			}
		}

		// Token: 0x170071E5 RID: 29157
		// (get) Token: 0x06015D7E RID: 89470 RVA: 0x00323A82 File Offset: 0x00321C82
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return SlideMasterId.eleNamespaceIds;
			}
		}

		// Token: 0x170071E6 RID: 29158
		// (get) Token: 0x06015D7F RID: 89471 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x170071E7 RID: 29159
		// (get) Token: 0x06015D80 RID: 89472 RVA: 0x0031FDCB File Offset: 0x0031DFCB
		// (set) Token: 0x06015D81 RID: 89473 RVA: 0x0031FDD4 File Offset: 0x0031DFD4
		public ExtensionList ExtensionList
		{
			get
			{
				return base.GetElement<ExtensionList>(0);
			}
			set
			{
				base.SetElement<ExtensionList>(0, value);
			}
		}

		// Token: 0x06015D82 RID: 89474 RVA: 0x003239BB File Offset: 0x00321BBB
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "id" == name)
			{
				return new UInt32Value();
			}
			if (19 == namespaceId && "id" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06015D83 RID: 89475 RVA: 0x00323A89 File Offset: 0x00321C89
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SlideMasterId>(deep);
		}

		// Token: 0x0400950A RID: 38154
		private const string tagName = "sldMasterId";

		// Token: 0x0400950B RID: 38155
		private const byte tagNsId = 24;

		// Token: 0x0400950C RID: 38156
		internal const int ElementTypeIdConst = 12252;

		// Token: 0x0400950D RID: 38157
		private static string[] attributeTagNames = new string[] { "id", "id" };

		// Token: 0x0400950E RID: 38158
		private static byte[] attributeNamespaceIds = new byte[] { 0, 19 };

		// Token: 0x0400950F RID: 38159
		private static readonly string[] eleTagNames = new string[] { "extLst" };

		// Token: 0x04009510 RID: 38160
		private static readonly byte[] eleNamespaceIds = new byte[] { 24 };
	}
}
