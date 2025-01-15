using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002B31 RID: 11057
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(Comment))]
	internal class CommentList : OpenXmlCompositeElement
	{
		// Token: 0x1700776F RID: 30575
		// (get) Token: 0x06016A13 RID: 92691 RVA: 0x0032D61D File Offset: 0x0032B81D
		public override string LocalName
		{
			get
			{
				return "commentList";
			}
		}

		// Token: 0x17007770 RID: 30576
		// (get) Token: 0x06016A14 RID: 92692 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17007771 RID: 30577
		// (get) Token: 0x06016A15 RID: 92693 RVA: 0x0032D624 File Offset: 0x0032B824
		internal override int ElementTypeId
		{
			get
			{
				return 11055;
			}
		}

		// Token: 0x06016A16 RID: 92694 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06016A17 RID: 92695 RVA: 0x00293ECF File Offset: 0x002920CF
		public CommentList()
		{
		}

		// Token: 0x06016A18 RID: 92696 RVA: 0x00293ED7 File Offset: 0x002920D7
		public CommentList(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06016A19 RID: 92697 RVA: 0x00293EE0 File Offset: 0x002920E0
		public CommentList(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06016A1A RID: 92698 RVA: 0x00293EE9 File Offset: 0x002920E9
		public CommentList(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06016A1B RID: 92699 RVA: 0x0032D62B File Offset: 0x0032B82B
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "comment" == name)
			{
				return new Comment();
			}
			return null;
		}

		// Token: 0x06016A1C RID: 92700 RVA: 0x0032D646 File Offset: 0x0032B846
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<CommentList>(deep);
		}

		// Token: 0x04009950 RID: 39248
		private const string tagName = "commentList";

		// Token: 0x04009951 RID: 39249
		private const byte tagNsId = 22;

		// Token: 0x04009952 RID: 39250
		internal const int ElementTypeIdConst = 11055;
	}
}
