using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Packaging;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x020029FA RID: 10746
	[ChildElementInfo(typeof(CommentAuthor))]
	[GeneratedCode("DomGen", "2.0")]
	internal class CommentAuthorList : OpenXmlPartRootElement
	{
		// Token: 0x17006EBA RID: 28346
		// (get) Token: 0x06015670 RID: 87664 RVA: 0x0031EA22 File Offset: 0x0031CC22
		public override string LocalName
		{
			get
			{
				return "cmAuthorLst";
			}
		}

		// Token: 0x17006EBB RID: 28347
		// (get) Token: 0x06015671 RID: 87665 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x17006EBC RID: 28348
		// (get) Token: 0x06015672 RID: 87666 RVA: 0x0031EA29 File Offset: 0x0031CC29
		internal override int ElementTypeId
		{
			get
			{
				return 12173;
			}
		}

		// Token: 0x06015673 RID: 87667 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06015674 RID: 87668 RVA: 0x002CEBD9 File Offset: 0x002CCDD9
		internal CommentAuthorList(CommentAuthorsPart ownerPart)
			: base(ownerPart)
		{
		}

		// Token: 0x06015675 RID: 87669 RVA: 0x002CEBE2 File Offset: 0x002CCDE2
		public void Load(CommentAuthorsPart openXmlPart)
		{
			base.LoadFromPart(openXmlPart);
		}

		// Token: 0x17006EBD RID: 28349
		// (get) Token: 0x06015676 RID: 87670 RVA: 0x0031EA30 File Offset: 0x0031CC30
		// (set) Token: 0x06015677 RID: 87671 RVA: 0x002CEC01 File Offset: 0x002CCE01
		public CommentAuthorsPart CommentAuthorsPart
		{
			get
			{
				return base.OpenXmlPart as CommentAuthorsPart;
			}
			internal set
			{
				base.OpenXmlPart = value;
			}
		}

		// Token: 0x06015678 RID: 87672 RVA: 0x002CEB18 File Offset: 0x002CCD18
		public CommentAuthorList(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015679 RID: 87673 RVA: 0x002CEB21 File Offset: 0x002CCD21
		public CommentAuthorList(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601567A RID: 87674 RVA: 0x002CEB2A File Offset: 0x002CCD2A
		public CommentAuthorList(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601567B RID: 87675 RVA: 0x002CEB10 File Offset: 0x002CCD10
		public CommentAuthorList()
		{
		}

		// Token: 0x0601567C RID: 87676 RVA: 0x002CEBEB File Offset: 0x002CCDEB
		public void Save(CommentAuthorsPart openXmlPart)
		{
			base.SaveToPart(openXmlPart);
		}

		// Token: 0x0601567D RID: 87677 RVA: 0x0031EA3D File Offset: 0x0031CC3D
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (24 == namespaceId && "cmAuthor" == name)
			{
				return new CommentAuthor();
			}
			return null;
		}

		// Token: 0x0601567E RID: 87678 RVA: 0x0031EA58 File Offset: 0x0031CC58
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<CommentAuthorList>(deep);
		}

		// Token: 0x0400935B RID: 37723
		private const string tagName = "cmAuthorLst";

		// Token: 0x0400935C RID: 37724
		private const byte tagNsId = 24;

		// Token: 0x0400935D RID: 37725
		internal const int ElementTypeIdConst = 12173;
	}
}
