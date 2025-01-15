using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002A4E RID: 10830
	[ChildElementInfo(typeof(Text))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(Position))]
	[ChildElementInfo(typeof(ExtensionListWithModification))]
	internal class Comment : OpenXmlCompositeElement
	{
		// Token: 0x170071B4 RID: 29108
		// (get) Token: 0x06015D19 RID: 89369 RVA: 0x00323676 File Offset: 0x00321876
		public override string LocalName
		{
			get
			{
				return "cm";
			}
		}

		// Token: 0x170071B5 RID: 29109
		// (get) Token: 0x06015D1A RID: 89370 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x170071B6 RID: 29110
		// (get) Token: 0x06015D1B RID: 89371 RVA: 0x0032367D File Offset: 0x0032187D
		internal override int ElementTypeId
		{
			get
			{
				return 12249;
			}
		}

		// Token: 0x06015D1C RID: 89372 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170071B7 RID: 29111
		// (get) Token: 0x06015D1D RID: 89373 RVA: 0x00323684 File Offset: 0x00321884
		internal override string[] AttributeTagNames
		{
			get
			{
				return Comment.attributeTagNames;
			}
		}

		// Token: 0x170071B8 RID: 29112
		// (get) Token: 0x06015D1E RID: 89374 RVA: 0x0032368B File Offset: 0x0032188B
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Comment.attributeNamespaceIds;
			}
		}

		// Token: 0x170071B9 RID: 29113
		// (get) Token: 0x06015D1F RID: 89375 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x06015D20 RID: 89376 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "authorId")]
		public UInt32Value AuthorId
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

		// Token: 0x170071BA RID: 29114
		// (get) Token: 0x06015D21 RID: 89377 RVA: 0x002EC18F File Offset: 0x002EA38F
		// (set) Token: 0x06015D22 RID: 89378 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "dt")]
		public DateTimeValue DateTime
		{
			get
			{
				return (DateTimeValue)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x170071BB RID: 29115
		// (get) Token: 0x06015D23 RID: 89379 RVA: 0x002E5814 File Offset: 0x002E3A14
		// (set) Token: 0x06015D24 RID: 89380 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "idx")]
		public UInt32Value Index
		{
			get
			{
				return (UInt32Value)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x06015D25 RID: 89381 RVA: 0x00293ECF File Offset: 0x002920CF
		public Comment()
		{
		}

		// Token: 0x06015D26 RID: 89382 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Comment(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015D27 RID: 89383 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Comment(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015D28 RID: 89384 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Comment(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06015D29 RID: 89385 RVA: 0x00323694 File Offset: 0x00321894
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (24 == namespaceId && "pos" == name)
			{
				return new Position();
			}
			if (24 == namespaceId && "text" == name)
			{
				return new Text();
			}
			if (24 == namespaceId && "extLst" == name)
			{
				return new ExtensionListWithModification();
			}
			return null;
		}

		// Token: 0x170071BC RID: 29116
		// (get) Token: 0x06015D2A RID: 89386 RVA: 0x003236EA File Offset: 0x003218EA
		internal override string[] ElementTagNames
		{
			get
			{
				return Comment.eleTagNames;
			}
		}

		// Token: 0x170071BD RID: 29117
		// (get) Token: 0x06015D2B RID: 89387 RVA: 0x003236F1 File Offset: 0x003218F1
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return Comment.eleNamespaceIds;
			}
		}

		// Token: 0x170071BE RID: 29118
		// (get) Token: 0x06015D2C RID: 89388 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x170071BF RID: 29119
		// (get) Token: 0x06015D2D RID: 89389 RVA: 0x003236F8 File Offset: 0x003218F8
		// (set) Token: 0x06015D2E RID: 89390 RVA: 0x00323701 File Offset: 0x00321901
		public Position Position
		{
			get
			{
				return base.GetElement<Position>(0);
			}
			set
			{
				base.SetElement<Position>(0, value);
			}
		}

		// Token: 0x170071C0 RID: 29120
		// (get) Token: 0x06015D2F RID: 89391 RVA: 0x0032370B File Offset: 0x0032190B
		// (set) Token: 0x06015D30 RID: 89392 RVA: 0x00323714 File Offset: 0x00321914
		public Text Text
		{
			get
			{
				return base.GetElement<Text>(1);
			}
			set
			{
				base.SetElement<Text>(1, value);
			}
		}

		// Token: 0x170071C1 RID: 29121
		// (get) Token: 0x06015D31 RID: 89393 RVA: 0x0031FCA4 File Offset: 0x0031DEA4
		// (set) Token: 0x06015D32 RID: 89394 RVA: 0x0031FCAD File Offset: 0x0031DEAD
		public ExtensionListWithModification ExtensionListWithModification
		{
			get
			{
				return base.GetElement<ExtensionListWithModification>(2);
			}
			set
			{
				base.SetElement<ExtensionListWithModification>(2, value);
			}
		}

		// Token: 0x06015D33 RID: 89395 RVA: 0x00323720 File Offset: 0x00321920
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "authorId" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "dt" == name)
			{
				return new DateTimeValue();
			}
			if (namespaceId == 0 && "idx" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06015D34 RID: 89396 RVA: 0x00323777 File Offset: 0x00321977
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Comment>(deep);
		}

		// Token: 0x06015D35 RID: 89397 RVA: 0x00323780 File Offset: 0x00321980
		// Note: this type is marked as 'beforefieldinit'.
		static Comment()
		{
			byte[] array = new byte[3];
			Comment.attributeNamespaceIds = array;
			Comment.eleTagNames = new string[] { "pos", "text", "extLst" };
			Comment.eleNamespaceIds = new byte[] { 24, 24, 24 };
		}

		// Token: 0x040094F5 RID: 38133
		private const string tagName = "cm";

		// Token: 0x040094F6 RID: 38134
		private const byte tagNsId = 24;

		// Token: 0x040094F7 RID: 38135
		internal const int ElementTypeIdConst = 12249;

		// Token: 0x040094F8 RID: 38136
		private static string[] attributeTagNames = new string[] { "authorId", "dt", "idx" };

		// Token: 0x040094F9 RID: 38137
		private static byte[] attributeNamespaceIds;

		// Token: 0x040094FA RID: 38138
		private static readonly string[] eleTagNames;

		// Token: 0x040094FB RID: 38139
		private static readonly byte[] eleNamespaceIds;
	}
}
