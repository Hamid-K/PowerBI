using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002B32 RID: 11058
	[ChildElementInfo(typeof(CommentProperties), FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(CommentText))]
	internal class Comment : OpenXmlCompositeElement
	{
		// Token: 0x17007772 RID: 30578
		// (get) Token: 0x06016A1D RID: 92701 RVA: 0x002A98F7 File Offset: 0x002A7AF7
		public override string LocalName
		{
			get
			{
				return "comment";
			}
		}

		// Token: 0x17007773 RID: 30579
		// (get) Token: 0x06016A1E RID: 92702 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17007774 RID: 30580
		// (get) Token: 0x06016A1F RID: 92703 RVA: 0x0032D64F File Offset: 0x0032B84F
		internal override int ElementTypeId
		{
			get
			{
				return 11056;
			}
		}

		// Token: 0x06016A20 RID: 92704 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17007775 RID: 30581
		// (get) Token: 0x06016A21 RID: 92705 RVA: 0x0032D656 File Offset: 0x0032B856
		internal override string[] AttributeTagNames
		{
			get
			{
				return Comment.attributeTagNames;
			}
		}

		// Token: 0x17007776 RID: 30582
		// (get) Token: 0x06016A22 RID: 92706 RVA: 0x0032D65D File Offset: 0x0032B85D
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Comment.attributeNamespaceIds;
			}
		}

		// Token: 0x17007777 RID: 30583
		// (get) Token: 0x06016A23 RID: 92707 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06016A24 RID: 92708 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "ref")]
		public StringValue Reference
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

		// Token: 0x17007778 RID: 30584
		// (get) Token: 0x06016A25 RID: 92709 RVA: 0x002E33EC File Offset: 0x002E15EC
		// (set) Token: 0x06016A26 RID: 92710 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "authorId")]
		public UInt32Value AuthorId
		{
			get
			{
				return (UInt32Value)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x17007779 RID: 30585
		// (get) Token: 0x06016A27 RID: 92711 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x06016A28 RID: 92712 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "guid")]
		public StringValue Guid
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

		// Token: 0x1700777A RID: 30586
		// (get) Token: 0x06016A29 RID: 92713 RVA: 0x002E5B0D File Offset: 0x002E3D0D
		// (set) Token: 0x06016A2A RID: 92714 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[OfficeAvailability(FileFormatVersions.Office2010)]
		[SchemaAttr(0, "shapeId")]
		public UInt32Value ShapeId
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

		// Token: 0x06016A2B RID: 92715 RVA: 0x00293ECF File Offset: 0x002920CF
		public Comment()
		{
		}

		// Token: 0x06016A2C RID: 92716 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Comment(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06016A2D RID: 92717 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Comment(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06016A2E RID: 92718 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Comment(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06016A2F RID: 92719 RVA: 0x0032D664 File Offset: 0x0032B864
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "text" == name)
			{
				return new CommentText();
			}
			if (22 == namespaceId && "commentPr" == name)
			{
				return new CommentProperties();
			}
			return null;
		}

		// Token: 0x1700777B RID: 30587
		// (get) Token: 0x06016A30 RID: 92720 RVA: 0x0032D697 File Offset: 0x0032B897
		internal override string[] ElementTagNames
		{
			get
			{
				return Comment.eleTagNames;
			}
		}

		// Token: 0x1700777C RID: 30588
		// (get) Token: 0x06016A31 RID: 92721 RVA: 0x0032D69E File Offset: 0x0032B89E
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return Comment.eleNamespaceIds;
			}
		}

		// Token: 0x1700777D RID: 30589
		// (get) Token: 0x06016A32 RID: 92722 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x1700777E RID: 30590
		// (get) Token: 0x06016A33 RID: 92723 RVA: 0x0032D6A5 File Offset: 0x0032B8A5
		// (set) Token: 0x06016A34 RID: 92724 RVA: 0x0032D6AE File Offset: 0x0032B8AE
		public CommentText CommentText
		{
			get
			{
				return base.GetElement<CommentText>(0);
			}
			set
			{
				base.SetElement<CommentText>(0, value);
			}
		}

		// Token: 0x1700777F RID: 30591
		// (get) Token: 0x06016A35 RID: 92725 RVA: 0x0032D6B8 File Offset: 0x0032B8B8
		// (set) Token: 0x06016A36 RID: 92726 RVA: 0x0032D6C1 File Offset: 0x0032B8C1
		[OfficeAvailability(FileFormatVersions.Office2010)]
		public CommentProperties CommentProperties
		{
			get
			{
				return base.GetElement<CommentProperties>(1);
			}
			set
			{
				base.SetElement<CommentProperties>(1, value);
			}
		}

		// Token: 0x06016A37 RID: 92727 RVA: 0x0032D6CC File Offset: 0x0032B8CC
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "ref" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "authorId" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "guid" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "shapeId" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06016A38 RID: 92728 RVA: 0x0032D739 File Offset: 0x0032B939
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Comment>(deep);
		}

		// Token: 0x06016A39 RID: 92729 RVA: 0x0032D744 File Offset: 0x0032B944
		// Note: this type is marked as 'beforefieldinit'.
		static Comment()
		{
			byte[] array = new byte[4];
			Comment.attributeNamespaceIds = array;
			Comment.eleTagNames = new string[] { "text", "commentPr" };
			Comment.eleNamespaceIds = new byte[] { 22, 22 };
		}

		// Token: 0x04009953 RID: 39251
		private const string tagName = "comment";

		// Token: 0x04009954 RID: 39252
		private const byte tagNsId = 22;

		// Token: 0x04009955 RID: 39253
		internal const int ElementTypeIdConst = 11056;

		// Token: 0x04009956 RID: 39254
		private static string[] attributeTagNames = new string[] { "ref", "authorId", "guid", "shapeId" };

		// Token: 0x04009957 RID: 39255
		private static byte[] attributeNamespaceIds;

		// Token: 0x04009958 RID: 39256
		private static readonly string[] eleTagNames;

		// Token: 0x04009959 RID: 39257
		private static readonly byte[] eleNamespaceIds;
	}
}
