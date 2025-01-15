using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002FC3 RID: 12227
	[ChildElementInfo(typeof(CustomXmlBlock))]
	[ChildElementInfo(typeof(AltChunk))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(SdtBlock))]
	[ChildElementInfo(typeof(Paragraph))]
	[ChildElementInfo(typeof(Table))]
	[ChildElementInfo(typeof(ProofError))]
	[ChildElementInfo(typeof(PermStart))]
	[ChildElementInfo(typeof(PermEnd))]
	[ChildElementInfo(typeof(BookmarkStart))]
	[ChildElementInfo(typeof(BookmarkEnd))]
	[ChildElementInfo(typeof(CommentRangeStart))]
	[ChildElementInfo(typeof(CommentRangeEnd))]
	internal class Comment : OpenXmlCompositeElement
	{
		// Token: 0x170093F4 RID: 37876
		// (get) Token: 0x0601A852 RID: 108626 RVA: 0x002A98F7 File Offset: 0x002A7AF7
		public override string LocalName
		{
			get
			{
				return "comment";
			}
		}

		// Token: 0x170093F5 RID: 37877
		// (get) Token: 0x0601A853 RID: 108627 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x170093F6 RID: 37878
		// (get) Token: 0x0601A854 RID: 108628 RVA: 0x003635EF File Offset: 0x003617EF
		internal override int ElementTypeId
		{
			get
			{
				return 11936;
			}
		}

		// Token: 0x0601A855 RID: 108629 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170093F7 RID: 37879
		// (get) Token: 0x0601A856 RID: 108630 RVA: 0x003635F6 File Offset: 0x003617F6
		internal override string[] AttributeTagNames
		{
			get
			{
				return Comment.attributeTagNames;
			}
		}

		// Token: 0x170093F8 RID: 37880
		// (get) Token: 0x0601A857 RID: 108631 RVA: 0x003635FD File Offset: 0x003617FD
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Comment.attributeNamespaceIds;
			}
		}

		// Token: 0x170093F9 RID: 37881
		// (get) Token: 0x0601A858 RID: 108632 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0601A859 RID: 108633 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(23, "initials")]
		public StringValue Initials
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

		// Token: 0x170093FA RID: 37882
		// (get) Token: 0x0601A85A RID: 108634 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x0601A85B RID: 108635 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(23, "author")]
		public StringValue Author
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

		// Token: 0x170093FB RID: 37883
		// (get) Token: 0x0601A85C RID: 108636 RVA: 0x0031FD86 File Offset: 0x0031DF86
		// (set) Token: 0x0601A85D RID: 108637 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(23, "date")]
		public DateTimeValue Date
		{
			get
			{
				return (DateTimeValue)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x170093FC RID: 37884
		// (get) Token: 0x0601A85E RID: 108638 RVA: 0x002BDADA File Offset: 0x002BBCDA
		// (set) Token: 0x0601A85F RID: 108639 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(23, "id")]
		public StringValue Id
		{
			get
			{
				return (StringValue)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x0601A860 RID: 108640 RVA: 0x00293ECF File Offset: 0x002920CF
		public Comment()
		{
		}

		// Token: 0x0601A861 RID: 108641 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Comment(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601A862 RID: 108642 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Comment(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601A863 RID: 108643 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Comment(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601A864 RID: 108644 RVA: 0x00363604 File Offset: 0x00361804
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "altChunk" == name)
			{
				return new AltChunk();
			}
			if (23 == namespaceId && "customXml" == name)
			{
				return new CustomXmlBlock();
			}
			if (23 == namespaceId && "sdt" == name)
			{
				return new SdtBlock();
			}
			if (23 == namespaceId && "p" == name)
			{
				return new Paragraph();
			}
			if (23 == namespaceId && "tbl" == name)
			{
				return new Table();
			}
			if (23 == namespaceId && "proofErr" == name)
			{
				return new ProofError();
			}
			if (23 == namespaceId && "permStart" == name)
			{
				return new PermStart();
			}
			if (23 == namespaceId && "permEnd" == name)
			{
				return new PermEnd();
			}
			if (23 == namespaceId && "bookmarkStart" == name)
			{
				return new BookmarkStart();
			}
			if (23 == namespaceId && "bookmarkEnd" == name)
			{
				return new BookmarkEnd();
			}
			if (23 == namespaceId && "commentRangeStart" == name)
			{
				return new CommentRangeStart();
			}
			if (23 == namespaceId && "commentRangeEnd" == name)
			{
				return new CommentRangeEnd();
			}
			return null;
		}

		// Token: 0x0601A865 RID: 108645 RVA: 0x00363734 File Offset: 0x00361934
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "initials" == name)
			{
				return new StringValue();
			}
			if (23 == namespaceId && "author" == name)
			{
				return new StringValue();
			}
			if (23 == namespaceId && "date" == name)
			{
				return new DateTimeValue();
			}
			if (23 == namespaceId && "id" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601A866 RID: 108646 RVA: 0x003637A9 File Offset: 0x003619A9
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Comment>(deep);
		}

		// Token: 0x0400AD52 RID: 44370
		private const string tagName = "comment";

		// Token: 0x0400AD53 RID: 44371
		private const byte tagNsId = 23;

		// Token: 0x0400AD54 RID: 44372
		internal const int ElementTypeIdConst = 11936;

		// Token: 0x0400AD55 RID: 44373
		private static string[] attributeTagNames = new string[] { "initials", "author", "date", "id" };

		// Token: 0x0400AD56 RID: 44374
		private static byte[] attributeNamespaceIds = new byte[] { 23, 23, 23, 23 };
	}
}
