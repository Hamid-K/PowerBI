using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002A4D RID: 10829
	[ChildElementInfo(typeof(ExtensionList))]
	[GeneratedCode("DomGen", "2.0")]
	internal class CommentAuthor : OpenXmlCompositeElement
	{
		// Token: 0x170071A6 RID: 29094
		// (get) Token: 0x06015CFC RID: 89340 RVA: 0x00323548 File Offset: 0x00321748
		public override string LocalName
		{
			get
			{
				return "cmAuthor";
			}
		}

		// Token: 0x170071A7 RID: 29095
		// (get) Token: 0x06015CFD RID: 89341 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x170071A8 RID: 29096
		// (get) Token: 0x06015CFE RID: 89342 RVA: 0x0032354F File Offset: 0x0032174F
		internal override int ElementTypeId
		{
			get
			{
				return 12248;
			}
		}

		// Token: 0x06015CFF RID: 89343 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170071A9 RID: 29097
		// (get) Token: 0x06015D00 RID: 89344 RVA: 0x00323556 File Offset: 0x00321756
		internal override string[] AttributeTagNames
		{
			get
			{
				return CommentAuthor.attributeTagNames;
			}
		}

		// Token: 0x170071AA RID: 29098
		// (get) Token: 0x06015D01 RID: 89345 RVA: 0x0032355D File Offset: 0x0032175D
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return CommentAuthor.attributeNamespaceIds;
			}
		}

		// Token: 0x170071AB RID: 29099
		// (get) Token: 0x06015D02 RID: 89346 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x06015D03 RID: 89347 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x170071AC RID: 29100
		// (get) Token: 0x06015D04 RID: 89348 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x06015D05 RID: 89349 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "name")]
		public StringValue Name
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

		// Token: 0x170071AD RID: 29101
		// (get) Token: 0x06015D06 RID: 89350 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x06015D07 RID: 89351 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "initials")]
		public StringValue Initials
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

		// Token: 0x170071AE RID: 29102
		// (get) Token: 0x06015D08 RID: 89352 RVA: 0x002E5B0D File Offset: 0x002E3D0D
		// (set) Token: 0x06015D09 RID: 89353 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "lastIdx")]
		public UInt32Value LastIndex
		{
			get
			{
				return (UInt32Value)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x170071AF RID: 29103
		// (get) Token: 0x06015D0A RID: 89354 RVA: 0x002E6C42 File Offset: 0x002E4E42
		// (set) Token: 0x06015D0B RID: 89355 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "clrIdx")]
		public UInt32Value ColorIndex
		{
			get
			{
				return (UInt32Value)base.Attributes[4];
			}
			set
			{
				base.Attributes[4] = value;
			}
		}

		// Token: 0x06015D0C RID: 89356 RVA: 0x00293ECF File Offset: 0x002920CF
		public CommentAuthor()
		{
		}

		// Token: 0x06015D0D RID: 89357 RVA: 0x00293ED7 File Offset: 0x002920D7
		public CommentAuthor(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015D0E RID: 89358 RVA: 0x00293EE0 File Offset: 0x002920E0
		public CommentAuthor(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015D0F RID: 89359 RVA: 0x00293EE9 File Offset: 0x002920E9
		public CommentAuthor(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06015D10 RID: 89360 RVA: 0x0031FDA2 File Offset: 0x0031DFA2
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (24 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x170071B0 RID: 29104
		// (get) Token: 0x06015D11 RID: 89361 RVA: 0x00323564 File Offset: 0x00321764
		internal override string[] ElementTagNames
		{
			get
			{
				return CommentAuthor.eleTagNames;
			}
		}

		// Token: 0x170071B1 RID: 29105
		// (get) Token: 0x06015D12 RID: 89362 RVA: 0x0032356B File Offset: 0x0032176B
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return CommentAuthor.eleNamespaceIds;
			}
		}

		// Token: 0x170071B2 RID: 29106
		// (get) Token: 0x06015D13 RID: 89363 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x170071B3 RID: 29107
		// (get) Token: 0x06015D14 RID: 89364 RVA: 0x0031FDCB File Offset: 0x0031DFCB
		// (set) Token: 0x06015D15 RID: 89365 RVA: 0x0031FDD4 File Offset: 0x0031DFD4
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

		// Token: 0x06015D16 RID: 89366 RVA: 0x00323574 File Offset: 0x00321774
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "id" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "name" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "initials" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "lastIdx" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "clrIdx" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06015D17 RID: 89367 RVA: 0x003235F7 File Offset: 0x003217F7
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<CommentAuthor>(deep);
		}

		// Token: 0x06015D18 RID: 89368 RVA: 0x00323600 File Offset: 0x00321800
		// Note: this type is marked as 'beforefieldinit'.
		static CommentAuthor()
		{
			byte[] array = new byte[5];
			CommentAuthor.attributeNamespaceIds = array;
			CommentAuthor.eleTagNames = new string[] { "extLst" };
			CommentAuthor.eleNamespaceIds = new byte[] { 24 };
		}

		// Token: 0x040094EE RID: 38126
		private const string tagName = "cmAuthor";

		// Token: 0x040094EF RID: 38127
		private const byte tagNsId = 24;

		// Token: 0x040094F0 RID: 38128
		internal const int ElementTypeIdConst = 12248;

		// Token: 0x040094F1 RID: 38129
		private static string[] attributeTagNames = new string[] { "id", "name", "initials", "lastIdx", "clrIdx" };

		// Token: 0x040094F2 RID: 38130
		private static byte[] attributeNamespaceIds;

		// Token: 0x040094F3 RID: 38131
		private static readonly string[] eleTagNames;

		// Token: 0x040094F4 RID: 38132
		private static readonly byte[] eleNamespaceIds;
	}
}
