using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Packaging;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002B16 RID: 11030
	[ChildElementInfo(typeof(Authors))]
	[ChildElementInfo(typeof(CommentList))]
	[ChildElementInfo(typeof(ExtensionList))]
	[GeneratedCode("DomGen", "2.0")]
	internal class Comments : OpenXmlPartRootElement
	{
		// Token: 0x170075BA RID: 30138
		// (get) Token: 0x06016623 RID: 91683 RVA: 0x002A4733 File Offset: 0x002A2933
		public override string LocalName
		{
			get
			{
				return "comments";
			}
		}

		// Token: 0x170075BB RID: 30139
		// (get) Token: 0x06016624 RID: 91684 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x170075BC RID: 30140
		// (get) Token: 0x06016625 RID: 91685 RVA: 0x00329783 File Offset: 0x00327983
		internal override int ElementTypeId
		{
			get
			{
				return 11028;
			}
		}

		// Token: 0x06016626 RID: 91686 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06016627 RID: 91687 RVA: 0x002CEBD9 File Offset: 0x002CCDD9
		internal Comments(WorksheetCommentsPart ownerPart)
			: base(ownerPart)
		{
		}

		// Token: 0x06016628 RID: 91688 RVA: 0x002CEBE2 File Offset: 0x002CCDE2
		public void Load(WorksheetCommentsPart openXmlPart)
		{
			base.LoadFromPart(openXmlPart);
		}

		// Token: 0x170075BD RID: 30141
		// (get) Token: 0x06016629 RID: 91689 RVA: 0x0032978A File Offset: 0x0032798A
		// (set) Token: 0x0601662A RID: 91690 RVA: 0x002CEC01 File Offset: 0x002CCE01
		public WorksheetCommentsPart WorksheetCommentsPart
		{
			get
			{
				return base.OpenXmlPart as WorksheetCommentsPart;
			}
			internal set
			{
				base.OpenXmlPart = value;
			}
		}

		// Token: 0x0601662B RID: 91691 RVA: 0x002CEB18 File Offset: 0x002CCD18
		public Comments(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601662C RID: 91692 RVA: 0x002CEB21 File Offset: 0x002CCD21
		public Comments(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601662D RID: 91693 RVA: 0x002CEB2A File Offset: 0x002CCD2A
		public Comments(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601662E RID: 91694 RVA: 0x002CEB10 File Offset: 0x002CCD10
		public Comments()
		{
		}

		// Token: 0x0601662F RID: 91695 RVA: 0x002CEBEB File Offset: 0x002CCDEB
		public void Save(WorksheetCommentsPart openXmlPart)
		{
			base.SaveToPart(openXmlPart);
		}

		// Token: 0x06016630 RID: 91696 RVA: 0x00329798 File Offset: 0x00327998
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "authors" == name)
			{
				return new Authors();
			}
			if (22 == namespaceId && "commentList" == name)
			{
				return new CommentList();
			}
			if (22 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x170075BE RID: 30142
		// (get) Token: 0x06016631 RID: 91697 RVA: 0x003297EE File Offset: 0x003279EE
		internal override string[] ElementTagNames
		{
			get
			{
				return Comments.eleTagNames;
			}
		}

		// Token: 0x170075BF RID: 30143
		// (get) Token: 0x06016632 RID: 91698 RVA: 0x003297F5 File Offset: 0x003279F5
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return Comments.eleNamespaceIds;
			}
		}

		// Token: 0x170075C0 RID: 30144
		// (get) Token: 0x06016633 RID: 91699 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x170075C1 RID: 30145
		// (get) Token: 0x06016634 RID: 91700 RVA: 0x003297FC File Offset: 0x003279FC
		// (set) Token: 0x06016635 RID: 91701 RVA: 0x00329805 File Offset: 0x00327A05
		public Authors Authors
		{
			get
			{
				return base.GetElement<Authors>(0);
			}
			set
			{
				base.SetElement<Authors>(0, value);
			}
		}

		// Token: 0x170075C2 RID: 30146
		// (get) Token: 0x06016636 RID: 91702 RVA: 0x0032980F File Offset: 0x00327A0F
		// (set) Token: 0x06016637 RID: 91703 RVA: 0x00329818 File Offset: 0x00327A18
		public CommentList CommentList
		{
			get
			{
				return base.GetElement<CommentList>(1);
			}
			set
			{
				base.SetElement<CommentList>(1, value);
			}
		}

		// Token: 0x170075C3 RID: 30147
		// (get) Token: 0x06016638 RID: 91704 RVA: 0x00329822 File Offset: 0x00327A22
		// (set) Token: 0x06016639 RID: 91705 RVA: 0x0032982B File Offset: 0x00327A2B
		public ExtensionList ExtensionList
		{
			get
			{
				return base.GetElement<ExtensionList>(2);
			}
			set
			{
				base.SetElement<ExtensionList>(2, value);
			}
		}

		// Token: 0x0601663A RID: 91706 RVA: 0x00329835 File Offset: 0x00327A35
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Comments>(deep);
		}

		// Token: 0x040098CD RID: 39117
		private const string tagName = "comments";

		// Token: 0x040098CE RID: 39118
		private const byte tagNsId = 22;

		// Token: 0x040098CF RID: 39119
		internal const int ElementTypeIdConst = 11028;

		// Token: 0x040098D0 RID: 39120
		private static readonly string[] eleTagNames = new string[] { "authors", "commentList", "extLst" };

		// Token: 0x040098D1 RID: 39121
		private static readonly byte[] eleNamespaceIds = new byte[] { 22, 22, 22 };
	}
}
