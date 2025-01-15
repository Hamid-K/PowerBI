using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002A50 RID: 10832
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(ExtensionList))]
	internal class SlideId : OpenXmlCompositeElement
	{
		// Token: 0x170071D2 RID: 29138
		// (get) Token: 0x06015D57 RID: 89431 RVA: 0x002E552E File Offset: 0x002E372E
		public override string LocalName
		{
			get
			{
				return "sldId";
			}
		}

		// Token: 0x170071D3 RID: 29139
		// (get) Token: 0x06015D58 RID: 89432 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x170071D4 RID: 29140
		// (get) Token: 0x06015D59 RID: 89433 RVA: 0x00323998 File Offset: 0x00321B98
		internal override int ElementTypeId
		{
			get
			{
				return 12251;
			}
		}

		// Token: 0x06015D5A RID: 89434 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170071D5 RID: 29141
		// (get) Token: 0x06015D5B RID: 89435 RVA: 0x0032399F File Offset: 0x00321B9F
		internal override string[] AttributeTagNames
		{
			get
			{
				return SlideId.attributeTagNames;
			}
		}

		// Token: 0x170071D6 RID: 29142
		// (get) Token: 0x06015D5C RID: 89436 RVA: 0x003239A6 File Offset: 0x00321BA6
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return SlideId.attributeNamespaceIds;
			}
		}

		// Token: 0x170071D7 RID: 29143
		// (get) Token: 0x06015D5D RID: 89437 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x06015D5E RID: 89438 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x170071D8 RID: 29144
		// (get) Token: 0x06015D5F RID: 89439 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x06015D60 RID: 89440 RVA: 0x002BD47A File Offset: 0x002BB67A
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

		// Token: 0x06015D61 RID: 89441 RVA: 0x00293ECF File Offset: 0x002920CF
		public SlideId()
		{
		}

		// Token: 0x06015D62 RID: 89442 RVA: 0x00293ED7 File Offset: 0x002920D7
		public SlideId(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015D63 RID: 89443 RVA: 0x00293EE0 File Offset: 0x002920E0
		public SlideId(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015D64 RID: 89444 RVA: 0x00293EE9 File Offset: 0x002920E9
		public SlideId(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06015D65 RID: 89445 RVA: 0x0031FDA2 File Offset: 0x0031DFA2
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (24 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x170071D9 RID: 29145
		// (get) Token: 0x06015D66 RID: 89446 RVA: 0x003239AD File Offset: 0x00321BAD
		internal override string[] ElementTagNames
		{
			get
			{
				return SlideId.eleTagNames;
			}
		}

		// Token: 0x170071DA RID: 29146
		// (get) Token: 0x06015D67 RID: 89447 RVA: 0x003239B4 File Offset: 0x00321BB4
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return SlideId.eleNamespaceIds;
			}
		}

		// Token: 0x170071DB RID: 29147
		// (get) Token: 0x06015D68 RID: 89448 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x170071DC RID: 29148
		// (get) Token: 0x06015D69 RID: 89449 RVA: 0x0031FDCB File Offset: 0x0031DFCB
		// (set) Token: 0x06015D6A RID: 89450 RVA: 0x0031FDD4 File Offset: 0x0031DFD4
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

		// Token: 0x06015D6B RID: 89451 RVA: 0x003239BB File Offset: 0x00321BBB
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

		// Token: 0x06015D6C RID: 89452 RVA: 0x003239F3 File Offset: 0x00321BF3
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SlideId>(deep);
		}

		// Token: 0x04009503 RID: 38147
		private const string tagName = "sldId";

		// Token: 0x04009504 RID: 38148
		private const byte tagNsId = 24;

		// Token: 0x04009505 RID: 38149
		internal const int ElementTypeIdConst = 12251;

		// Token: 0x04009506 RID: 38150
		private static string[] attributeTagNames = new string[] { "id", "id" };

		// Token: 0x04009507 RID: 38151
		private static byte[] attributeNamespaceIds = new byte[] { 0, 19 };

		// Token: 0x04009508 RID: 38152
		private static readonly string[] eleTagNames = new string[] { "extLst" };

		// Token: 0x04009509 RID: 38153
		private static readonly byte[] eleNamespaceIds = new byte[] { 24 };
	}
}
