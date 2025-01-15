using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Office2010.Drawing.ChartDrawing;

namespace DocumentFormat.OpenXml.Drawing.ChartDrawing
{
	// Token: 0x0200262A RID: 9770
	[ChildElementInfo(typeof(GroupShapeProperties))]
	[ChildElementInfo(typeof(NonVisualGroupShapeProperties))]
	[ChildElementInfo(typeof(GraphicFrame))]
	[ChildElementInfo(typeof(Shape))]
	[ChildElementInfo(typeof(GroupShape))]
	[ChildElementInfo(typeof(Picture))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(ConnectionShape))]
	[ChildElementInfo(typeof(ContentPart), FileFormatVersions.Office2010)]
	internal class GroupShape : OpenXmlCompositeElement
	{
		// Token: 0x17005A07 RID: 23047
		// (get) Token: 0x06012746 RID: 75590 RVA: 0x002DF94C File Offset: 0x002DDB4C
		public override string LocalName
		{
			get
			{
				return "grpSp";
			}
		}

		// Token: 0x17005A08 RID: 23048
		// (get) Token: 0x06012747 RID: 75591 RVA: 0x001422C0 File Offset: 0x001404C0
		internal override byte NamespaceId
		{
			get
			{
				return 12;
			}
		}

		// Token: 0x17005A09 RID: 23049
		// (get) Token: 0x06012748 RID: 75592 RVA: 0x002FB5E2 File Offset: 0x002F97E2
		internal override int ElementTypeId
		{
			get
			{
				return 10589;
			}
		}

		// Token: 0x06012749 RID: 75593 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601274A RID: 75594 RVA: 0x00293ECF File Offset: 0x002920CF
		public GroupShape()
		{
		}

		// Token: 0x0601274B RID: 75595 RVA: 0x00293ED7 File Offset: 0x002920D7
		public GroupShape(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601274C RID: 75596 RVA: 0x00293EE0 File Offset: 0x002920E0
		public GroupShape(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601274D RID: 75597 RVA: 0x00293EE9 File Offset: 0x002920E9
		public GroupShape(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601274E RID: 75598 RVA: 0x002FB5EC File Offset: 0x002F97EC
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (12 == namespaceId && "nvGrpSpPr" == name)
			{
				return new NonVisualGroupShapeProperties();
			}
			if (12 == namespaceId && "grpSpPr" == name)
			{
				return new GroupShapeProperties();
			}
			if (12 == namespaceId && "sp" == name)
			{
				return new Shape();
			}
			if (12 == namespaceId && "grpSp" == name)
			{
				return new GroupShape();
			}
			if (12 == namespaceId && "graphicFrame" == name)
			{
				return new GraphicFrame();
			}
			if (12 == namespaceId && "cxnSp" == name)
			{
				return new ConnectionShape();
			}
			if (12 == namespaceId && "pic" == name)
			{
				return new Picture();
			}
			if (47 == namespaceId && "contentPart" == name)
			{
				return new ContentPart();
			}
			return null;
		}

		// Token: 0x17005A0A RID: 23050
		// (get) Token: 0x0601274F RID: 75599 RVA: 0x002FB6BA File Offset: 0x002F98BA
		internal override string[] ElementTagNames
		{
			get
			{
				return GroupShape.eleTagNames;
			}
		}

		// Token: 0x17005A0B RID: 23051
		// (get) Token: 0x06012750 RID: 75600 RVA: 0x002FB6C1 File Offset: 0x002F98C1
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return GroupShape.eleNamespaceIds;
			}
		}

		// Token: 0x17005A0C RID: 23052
		// (get) Token: 0x06012751 RID: 75601 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17005A0D RID: 23053
		// (get) Token: 0x06012752 RID: 75602 RVA: 0x002FB6C8 File Offset: 0x002F98C8
		// (set) Token: 0x06012753 RID: 75603 RVA: 0x002FB6D1 File Offset: 0x002F98D1
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

		// Token: 0x17005A0E RID: 23054
		// (get) Token: 0x06012754 RID: 75604 RVA: 0x002FB6DB File Offset: 0x002F98DB
		// (set) Token: 0x06012755 RID: 75605 RVA: 0x002FB6E4 File Offset: 0x002F98E4
		public GroupShapeProperties GroupShapeProperties
		{
			get
			{
				return base.GetElement<GroupShapeProperties>(1);
			}
			set
			{
				base.SetElement<GroupShapeProperties>(1, value);
			}
		}

		// Token: 0x06012756 RID: 75606 RVA: 0x002FB6EE File Offset: 0x002F98EE
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<GroupShape>(deep);
		}

		// Token: 0x04008036 RID: 32822
		private const string tagName = "grpSp";

		// Token: 0x04008037 RID: 32823
		private const byte tagNsId = 12;

		// Token: 0x04008038 RID: 32824
		internal const int ElementTypeIdConst = 10589;

		// Token: 0x04008039 RID: 32825
		private static readonly string[] eleTagNames = new string[] { "nvGrpSpPr", "grpSpPr", "sp", "grpSp", "graphicFrame", "cxnSp", "pic", "contentPart" };

		// Token: 0x0400803A RID: 32826
		private static readonly byte[] eleNamespaceIds = new byte[] { 12, 12, 12, 12, 12, 12, 12, 47 };
	}
}
