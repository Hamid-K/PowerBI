using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Office2010.Drawing;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x0200283C RID: 10300
	[ChildElementInfo(typeof(VisualGroupShapeProperties))]
	[ChildElementInfo(typeof(NonVisualGroupShapeProperties))]
	[ChildElementInfo(typeof(GroupShape))]
	[ChildElementInfo(typeof(GraphicFrame))]
	[ChildElementInfo(typeof(Shape))]
	[ChildElementInfo(typeof(ConnectionShape))]
	[ChildElementInfo(typeof(Picture))]
	[ChildElementInfo(typeof(GvmlContentPart), FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(GvmlGroupShapeExtensionList))]
	[ChildElementInfo(typeof(TextShape))]
	internal class GroupShape : OpenXmlCompositeElement
	{
		// Token: 0x1700665B RID: 26203
		// (get) Token: 0x06014356 RID: 82774 RVA: 0x002DF94C File Offset: 0x002DDB4C
		public override string LocalName
		{
			get
			{
				return "grpSp";
			}
		}

		// Token: 0x1700665C RID: 26204
		// (get) Token: 0x06014357 RID: 82775 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x1700665D RID: 26205
		// (get) Token: 0x06014358 RID: 82776 RVA: 0x003106BC File Offset: 0x0030E8BC
		internal override int ElementTypeId
		{
			get
			{
				return 10336;
			}
		}

		// Token: 0x06014359 RID: 82777 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601435A RID: 82778 RVA: 0x00293ECF File Offset: 0x002920CF
		public GroupShape()
		{
		}

		// Token: 0x0601435B RID: 82779 RVA: 0x00293ED7 File Offset: 0x002920D7
		public GroupShape(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601435C RID: 82780 RVA: 0x00293EE0 File Offset: 0x002920E0
		public GroupShape(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601435D RID: 82781 RVA: 0x00293EE9 File Offset: 0x002920E9
		public GroupShape(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601435E RID: 82782 RVA: 0x003106C4 File Offset: 0x0030E8C4
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (10 == namespaceId && "nvGrpSpPr" == name)
			{
				return new NonVisualGroupShapeProperties();
			}
			if (10 == namespaceId && "grpSpPr" == name)
			{
				return new VisualGroupShapeProperties();
			}
			if (10 == namespaceId && "txSp" == name)
			{
				return new TextShape();
			}
			if (10 == namespaceId && "sp" == name)
			{
				return new Shape();
			}
			if (10 == namespaceId && "cxnSp" == name)
			{
				return new ConnectionShape();
			}
			if (10 == namespaceId && "pic" == name)
			{
				return new Picture();
			}
			if (48 == namespaceId && "contentPart" == name)
			{
				return new GvmlContentPart();
			}
			if (10 == namespaceId && "graphicFrame" == name)
			{
				return new GraphicFrame();
			}
			if (10 == namespaceId && "grpSp" == name)
			{
				return new GroupShape();
			}
			if (10 == namespaceId && "extLst" == name)
			{
				return new GvmlGroupShapeExtensionList();
			}
			return null;
		}

		// Token: 0x1700665E RID: 26206
		// (get) Token: 0x0601435F RID: 82783 RVA: 0x003107C2 File Offset: 0x0030E9C2
		internal override string[] ElementTagNames
		{
			get
			{
				return GroupShape.eleTagNames;
			}
		}

		// Token: 0x1700665F RID: 26207
		// (get) Token: 0x06014360 RID: 82784 RVA: 0x003107C9 File Offset: 0x0030E9C9
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return GroupShape.eleNamespaceIds;
			}
		}

		// Token: 0x17006660 RID: 26208
		// (get) Token: 0x06014361 RID: 82785 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17006661 RID: 26209
		// (get) Token: 0x06014362 RID: 82786 RVA: 0x00301178 File Offset: 0x002FF378
		// (set) Token: 0x06014363 RID: 82787 RVA: 0x00301181 File Offset: 0x002FF381
		public NonVisualGroupShapeProperties NonVisualGroupShapeProperties
		{
			get
			{
				return base.GetElement<NonVisualGroupShapeProperties>(0);
			}
			set
			{
				base.SetElement<NonVisualGroupShapeProperties>(0, value);
			}
		}

		// Token: 0x17006662 RID: 26210
		// (get) Token: 0x06014364 RID: 82788 RVA: 0x0030118B File Offset: 0x002FF38B
		// (set) Token: 0x06014365 RID: 82789 RVA: 0x00301194 File Offset: 0x002FF394
		public VisualGroupShapeProperties VisualGroupShapeProperties
		{
			get
			{
				return base.GetElement<VisualGroupShapeProperties>(1);
			}
			set
			{
				base.SetElement<VisualGroupShapeProperties>(1, value);
			}
		}

		// Token: 0x06014366 RID: 82790 RVA: 0x003107D0 File Offset: 0x0030E9D0
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<GroupShape>(deep);
		}

		// Token: 0x04008984 RID: 35204
		private const string tagName = "grpSp";

		// Token: 0x04008985 RID: 35205
		private const byte tagNsId = 10;

		// Token: 0x04008986 RID: 35206
		internal const int ElementTypeIdConst = 10336;

		// Token: 0x04008987 RID: 35207
		private static readonly string[] eleTagNames = new string[] { "nvGrpSpPr", "grpSpPr", "txSp", "sp", "cxnSp", "pic", "contentPart", "graphicFrame", "grpSp", "extLst" };

		// Token: 0x04008988 RID: 35208
		private static readonly byte[] eleNamespaceIds = new byte[] { 10, 10, 10, 10, 10, 10, 48, 10, 10, 10 };
	}
}
