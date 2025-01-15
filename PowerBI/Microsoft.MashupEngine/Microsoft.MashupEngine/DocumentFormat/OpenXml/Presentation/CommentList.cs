using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Packaging;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x020029FB RID: 10747
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(Comment))]
	internal class CommentList : OpenXmlPartRootElement
	{
		// Token: 0x17006EBE RID: 28350
		// (get) Token: 0x0601567F RID: 87679 RVA: 0x0031EA61 File Offset: 0x0031CC61
		public override string LocalName
		{
			get
			{
				return "cmLst";
			}
		}

		// Token: 0x17006EBF RID: 28351
		// (get) Token: 0x06015680 RID: 87680 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x17006EC0 RID: 28352
		// (get) Token: 0x06015681 RID: 87681 RVA: 0x0031EA68 File Offset: 0x0031CC68
		internal override int ElementTypeId
		{
			get
			{
				return 12174;
			}
		}

		// Token: 0x06015682 RID: 87682 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06015683 RID: 87683 RVA: 0x002CEBD9 File Offset: 0x002CCDD9
		internal CommentList(SlideCommentsPart ownerPart)
			: base(ownerPart)
		{
		}

		// Token: 0x06015684 RID: 87684 RVA: 0x002CEBE2 File Offset: 0x002CCDE2
		public void Load(SlideCommentsPart openXmlPart)
		{
			base.LoadFromPart(openXmlPart);
		}

		// Token: 0x17006EC1 RID: 28353
		// (get) Token: 0x06015685 RID: 87685 RVA: 0x0031EA6F File Offset: 0x0031CC6F
		// (set) Token: 0x06015686 RID: 87686 RVA: 0x002CEC01 File Offset: 0x002CCE01
		public SlideCommentsPart SlideCommentsPart
		{
			get
			{
				return base.OpenXmlPart as SlideCommentsPart;
			}
			internal set
			{
				base.OpenXmlPart = value;
			}
		}

		// Token: 0x06015687 RID: 87687 RVA: 0x002CEB18 File Offset: 0x002CCD18
		public CommentList(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015688 RID: 87688 RVA: 0x002CEB21 File Offset: 0x002CCD21
		public CommentList(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015689 RID: 87689 RVA: 0x002CEB2A File Offset: 0x002CCD2A
		public CommentList(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601568A RID: 87690 RVA: 0x002CEB10 File Offset: 0x002CCD10
		public CommentList()
		{
		}

		// Token: 0x0601568B RID: 87691 RVA: 0x002CEBEB File Offset: 0x002CCDEB
		public void Save(SlideCommentsPart openXmlPart)
		{
			base.SaveToPart(openXmlPart);
		}

		// Token: 0x0601568C RID: 87692 RVA: 0x0031EA7C File Offset: 0x0031CC7C
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (24 == namespaceId && "cm" == name)
			{
				return new Comment();
			}
			return null;
		}

		// Token: 0x0601568D RID: 87693 RVA: 0x0031EA97 File Offset: 0x0031CC97
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<CommentList>(deep);
		}

		// Token: 0x0400935E RID: 37726
		private const string tagName = "cmLst";

		// Token: 0x0400935F RID: 37727
		private const byte tagNsId = 24;

		// Token: 0x04009360 RID: 37728
		internal const int ElementTypeIdConst = 12174;
	}
}
